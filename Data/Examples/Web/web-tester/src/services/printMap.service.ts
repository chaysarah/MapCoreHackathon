import { runCodeSafely, runMapCoreSafely } from "../common/services/error-handling/errorHandler";
import { MapCoreData, ViewportData } from "mapcore-lib";

export class PrintMapData {
    viewportData: ViewportData;
    lastPrintCallback: any;
    printObjectsList: MapCore.IMcObject[];

    constructor(viewportData: ViewportData) {
        this.viewportData = viewportData;
        this.lastPrintCallback = null;
        this.printObjectsList = [];
    }
}
/**
   NOTE:
  - limited to one printMapData per viewport
  - Using new PrintMapData can interfere with proper operation
 */

class PrintMapService {
    printMapsArr: PrintMapData[] = [];

    createInstance(viewportData: ViewportData): PrintMapData {
        let printMapData = new PrintMapData(viewportData);
        this.printMapsArr = [...this.printMapsArr, printMapData];
        return printMapData;
    }
    deleteObjectFromLists = (mcObject: MapCore.IMcObject) => {
        runCodeSafely(() => {
            let updatePrintMapsArr = [];
            this.printMapsArr.forEach(printMapData => {
                let updatedList = printMapData.printObjectsList.filter(obj => obj !== mcObject)
                printMapData.printObjectsList = updatedList;
                updatePrintMapsArr.push(printMapData);
            });
            this.printMapsArr = updatePrintMapsArr;
        }, 'PrintMapService.deleteObjectFromLists')
    }
    addPrintObject = (printMapData: PrintMapData, printObj: MapCore.IMcObject) => {
        runCodeSafely(() => {
            printMapData.printObjectsList = [...printMapData.printObjectsList, printObj];
            //update printMapsArr
            let filteredPrintMapsArr = this.printMapsArr.filter(printMapData => printMapData.viewportData.viewport !== printMapData.viewportData.viewport)
            this.printMapsArr = [...filteredPrintMapsArr, printMapData];
        }, 'PrintMapService.addPrintObject')
    }
    cancelAsyncPrint = (printMapData: PrintMapData) => {
        runCodeSafely(() => {
            if (printMapData.lastPrintCallback) {
                runMapCoreSafely(() => {
                    printMapData.viewportData.viewport.CancelAsyncPrint(printMapData.lastPrintCallback);
                }, 'PrintMapService.cancelAsyncPrint => IMcPrintMap.CancelAsyncPrint', true)
            }
        }, 'PrintMapService.cancelAsyncPrint')
    }
    deletePrintRects = (printMapData: PrintMapData) => {
        runCodeSafely(() => {
            printMapData.printObjectsList.map((object: MapCore.IMcObject) => {
                runMapCoreSafely(() => {
                    object.Remove();
                }, 'PrintMapService.deletePrintRects => IMcObject.Remove', true);
            })
            printMapData.printObjectsList = [];
            //update printMapsArr
            let filteredPrintMapsArr = this.printMapsArr.filter(printMapData => printMapData.viewportData.viewport !== printMapData.viewportData.viewport)
            this.printMapsArr = [...filteredPrintMapsArr, printMapData];
        }, 'PrintMapService.deletePrintRects')
    }
    goToLocation = (printMapData: PrintMapData, centerPoint: MapCore.SMcVector3D) => {
        runCodeSafely(() => {
            let mcViewport = printMapData.viewportData.viewport;
            if (mcViewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_2D) {
                runMapCoreSafely(() => { mcViewport.SetCameraPosition(centerPoint, false); }, 'PrintMapService.goToLocation => IMcMapCamera.SetCameraPosition', true)
            }
            else if (mcViewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                let pbHeightFound = false;
                let pHeight: { Value?: number } = {};
                let pNormal: { Value?: MapCore.SMcVector3D } = {};
                runMapCoreSafely(() => { pbHeightFound = mcViewport.GetTerrainHeight(centerPoint, pHeight, pNormal); }, 'PrintMapService.goToLocation => IMcSpatialQueries.GetTerrainHeight', true)
                centerPoint.z = pbHeightFound ? pHeight.Value : centerPoint.z;
                if (pbHeightFound) {
                    centerPoint.z = centerPoint.z + 500;
                    runMapCoreSafely(() => { mcViewport.SetCameraPosition(centerPoint, false); }, 'PrintMapService.goToLocation => IMcMapCamera.SetCameraPosition', true)
                    runMapCoreSafely(() => { mcViewport.SetCameraOrientation(0, -90, 0, false); }, 'PrintMapService.goToLocation => IMcMapCamera.SetCameraPosition', true)
                }
            }
        }, 'PrintMapService.goToLocation')
    }
}
export default new PrintMapService;


