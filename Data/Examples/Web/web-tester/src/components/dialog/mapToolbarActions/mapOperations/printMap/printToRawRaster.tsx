import { Fieldset } from "primereact/fieldset";
import { Properties } from "../../../dialog";
import { InputNumber } from "primereact/inputnumber";
import { Checkbox } from "primereact/checkbox";
import { TabInfo } from "../../../shared/tabCtrls/tabModels";
import UploadFilesCtrl, { UploadTypeEnum } from "../../../shared/uploadFilesCtrl";
import { Button } from "primereact/button";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { ConfirmDialog } from "primereact/confirmdialog";
import { getEnumDetailsList, getEnumValueDetails, MapCoreData, ObjectWorldService, ViewportData } from "mapcore-lib";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import { Dialog } from "primereact/dialog";
import SaveToFile from "../../../treeView/objectWorldTree/overlayForms/saveToFile";
import GdalOptions from "./gdalOptions";
import Vector2DFromMap from "../../../treeView/objectWorldTree/shared/Vector2DFromMap";
import printMapService, { PrintMapData } from "../../../../../services/printMap.service";

export class PrintToRawRasterProperties implements Properties {
    currentViewport: ViewportData;
    printMapData: PrintMapData;
    confirmDialogMessage: string;
    confirmDialogVisible: boolean;
    gdalOptions: string[];
    //Second Dialog
    secondDialogVisible: boolean;
    secondDialogType: string;
    //Save To File
    savedFileTypeOptions: any[];
    handleSaveToFileOkFunc: () => void;
    //Print Screen
    resolutionFactor: number;
    isPrintToFileInMemory: boolean;
    isPrintScreenAsync: boolean;
    //Print 2D
    rectangleCenter: MapCore.SMcVector2D;
    rectangleSize: MapCore.SMcVector2D;
    rectangleAngle: number;
    cameraScale: number;
    resolutionFactor2D: number;
    isPrintScreenAsync2D: boolean;
    isPrintToFileInMemory2D: boolean;
    isPrintGeoInMetricPropotion: boolean;

    static getDefault(p: any): PrintToRawRasterProperties {
        let { currentViewport, printMapData } = p;

        let defaults: PrintToRawRasterProperties = {
            currentViewport: currentViewport,
            printMapData: printMapData,
            confirmDialogMessage: '',
            confirmDialogVisible: false,
            gdalOptions: [],
            //Second Dialog
            secondDialogVisible: false,
            secondDialogType: '',
            //Save To File
            savedFileTypeOptions: [
                { name: 'TIFF files (*.tif, *.tiff)', extension: '.tif' }, { name: 'Pdf files (*.pdf)', extension: '.pdf', },
                { name: 'Bitmap files (*.bmp)', extension: '.bmp' }, { name: 'JPEG files (*.jpg, *.jpeg)', extension: '.jpg;.jpeg' },
                { name: 'PNG files (*.png)', extension: '.png' }, { name: 'All Files', extension: '' }],
            handleSaveToFileOkFunc: () => { },
            //Print Screen
            resolutionFactor: 1,
            isPrintToFileInMemory: false,
            isPrintScreenAsync: false,
            //Print 2D
            rectangleCenter: MapCore.v2Zero,
            rectangleSize: MapCore.v2Zero,
            rectangleAngle: 0,
            cameraScale: 0,
            resolutionFactor2D: 1,
            isPrintScreenAsync2D: false,
            isPrintToFileInMemory2D: false,
            isPrintGeoInMetricPropotion: true,
        }

        return defaults;
    }
};

export default function PrintToRawRaster(props: { tabInfo: TabInfo }) {
    let { tabProperties, setPropertiesCallback, setApplyCallBack, saveData } = props.tabInfo;

    const save2DVector = (...args) => {
        runCodeSafely(() => {
            const [point, flugNull, pointType] = args;
            setPropertiesCallback(pointType, point);
        }, 'PrintMap/PrintToRawRaster.save2DVector');
    }
    const getCalcPrintRectScreenScheme = (color: MapCore.SMcBColor): MapCore.IMcObjectSchemeItem => {
        let rectangleItem: MapCore.IMcRectangleItem = null;
        runCodeSafely(() => {
            let EItemSubTypeFlags = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
            let uItemSubTypeBitField: number;
            let eRectangleCoordinateSystem: MapCore.EMcPointCoordSystem;
            let eRectangleType: MapCore.IMcObjectSchemeItem.EGeometryType;
            if (tabProperties.currentViewport.viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_2D) {
                uItemSubTypeBitField = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code |
                    getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ATTACHED_TO_TERRAIN, EItemSubTypeFlags).code;
                eRectangleCoordinateSystem = MapCore.EMcPointCoordSystem.EPCS_WORLD;
                eRectangleType = MapCore.IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_OVERLAY_MANAGER;
            }
            else {
                uItemSubTypeBitField = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code;
                eRectangleCoordinateSystem = MapCore.EMcPointCoordSystem.EPCS_SCREEN;
                eRectangleType = MapCore.IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_VIEWPORT;
            }
            runMapCoreSafely(() => {
                rectangleItem = MapCore.IMcRectangleItem.Create(uItemSubTypeBitField, eRectangleCoordinateSystem, eRectangleType, MapCore.IMcRectangleItem.ERectangleDefinition.ERD_RECTANGLE_DIAGONAL_POINTS,
                    0, 0, MapCore.IMcLineBasedItem.ELineStyle.ELS_DASH_DOT, color, 3, null, new MapCore.SMcFVector2D(0, -1), 1, MapCore.IMcLineBasedItem.EFillStyle.EFS_NONE);
            }, 'PrintMap/PrintToRawRaster.getCalcPrintRectScreenScheme => IMcRectangleItem.Create', true)
        }, 'PrintMap/PrintToRawRaster.getCalcPrintRectScreenScheme')
        return rectangleItem;
    }
    const getCalcPrintRect2DScheme = (): MapCore.IMcSymbolicItem => {
        let objSchemeItem: MapCore.IMcSymbolicItem = null;
        runCodeSafely(() => {
            let EItemSubTypeFlags = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
            let uItemSubTypeBitField = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code |
                getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ATTACHED_TO_TERRAIN, EItemSubTypeFlags).code;
            if (tabProperties.isPrintGeoInMetricPropotion) {
                runMapCoreSafely(() => {
                    objSchemeItem = MapCore.IMcRectangleItem.Create(uItemSubTypeBitField, MapCore.EMcPointCoordSystem.EPCS_WORLD, MapCore.IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_VIEWPORT,
                        MapCore.IMcRectangleItem.ERectangleDefinition.ERD_RECTANGLE_CENTER_DIMENSIONS, tabProperties.rectangleSize.x / 2, tabProperties.rectangleSize.y / 2,
                        MapCore.IMcLineBasedItem.ELineStyle.ELS_DASH_DOT, new MapCore.SMcBColor(255, 0, 0, 255), 3, null, new MapCore.SMcFVector2D(0, -1), 1, MapCore.IMcLineBasedItem.EFillStyle.EFS_NONE);
                }, 'PrintMap/PrintToRawRaster.getCalcPrintRect2DScheme => IMcRectangleItem.Create', true)
                runMapCoreSafely(() => { objSchemeItem.SetRotationYaw(tabProperties.rectangleAngle); }, 'PrintMap/PrintToRawRaster.getCalcPrintRect2DScheme => IMcSymbolicItem.SetRotationYaw', true)
            }
            else {
                runMapCoreSafely(() => {
                    objSchemeItem = MapCore.IMcPolygonItem.Create(uItemSubTypeBitField, MapCore.IMcLineBasedItem.ELineStyle.ELS_DASH_DOT, new MapCore.SMcBColor(255, 0, 0, 255),
                        3, null, new MapCore.SMcFVector2D(0, -1), 1, MapCore.IMcLineBasedItem.EFillStyle.EFS_NONE,
                        new MapCore.SMcBColor(238, 130, 238, 100), null, new MapCore.SMcFVector2D(0, 0));
                }, 'PrintMap/PrintToRawRaster.getCalcPrintRect2DScheme => IMcPolygonItem.Create', true)
            }
        }, 'PrintMap/PrintToRawRaster.getCalcPrintRect2DScheme')
        return objSchemeItem;
    }
    const getCalcPrintRectScreenObject = (pagesCalc: MapCore.SMcBox, objSchemeItem: MapCore.IMcObjectSchemeItem, isScreenPoint: boolean): MapCore.IMcObject => {
        let obj: MapCore.IMcObject = null;
        runCodeSafely(() => {
            let activeOverlay = tabProperties.currentViewport ? ObjectWorldService.findActiveOverlayByMcViewport(tabProperties.currentViewport.viewport) : null;
            let locationPoints: MapCore.SMcVector3D[] = [];
            let eRectangleCoordinateSystem: MapCore.EMcPointCoordSystem;
            if (tabProperties.currentViewport.viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_2D) {
                locationPoints = [convertScreenToWorld(pagesCalc.MinVertex, isScreenPoint), convertScreenToWorld(pagesCalc.MaxVertex, isScreenPoint)];
                eRectangleCoordinateSystem = MapCore.EMcPointCoordSystem.EPCS_WORLD;

            }
            else {
                locationPoints[0].x = pagesCalc.MinVertex.x;
                locationPoints[0].y = pagesCalc.MinVertex.y;
                locationPoints[1].x = pagesCalc.MaxVertex.x;
                locationPoints[1].y = pagesCalc.MaxVertex.y;

                eRectangleCoordinateSystem = MapCore.EMcPointCoordSystem.EPCS_SCREEN;
            }
            runMapCoreSafely(() => {
                obj = MapCore.IMcObject.Create(activeOverlay,
                    objSchemeItem,
                    eRectangleCoordinateSystem,
                    locationPoints,
                    false);
            }, 'PrintMap/PrintToRawRaster.getCalcPrintRectScreenScheme => IMcObject.Create', true)
        }, 'PrintMap/PrintToRawRaster.getCalcPrintRectScreenObject')
        return obj;
    }
    const getCalcPrintRect2DObject = (activeOverlay: MapCore.IMcOverlay, objSchemeItem: MapCore.IMcSymbolicItem) => {
        let obj: MapCore.IMcObject = null;
        runCodeSafely(() => {
            let pointInOMCoord: MapCore.SMcVector3D[] = [];
            if (tabProperties.isPrintGeoInMetricPropotion) {
                runMapCoreSafely(() => { pointInOMCoord = [tabProperties.currentViewport.viewport.ViewportToOverlayManagerWorld(new MapCore.SMcVector3D(tabProperties.rectangleCenter.x, tabProperties.rectangleCenter.y, 0))] }, 'PrintMap/PrintToRawRaster.handleCalcPrintRect2DClick =>IMcMapViewport.ViewportToOverlayManagerWorld', true)
            }
            else {
                let vector3D1 = new MapCore.SMcVector3D(tabProperties.rectangleSize.x, 0, 0);
                let vector3D2 = new MapCore.SMcVector3D(0, tabProperties.rectangleSize.y, 0);
                runMapCoreSafely(() => { MapCore.SMcVector3D.RotateByDegreeYawAngle(vector3D1, tabProperties.rectangleAngle); }, 'PrintMap/PrintToRawRaster.getCalcPrintRect2DObject => SMcVector3D.RotateByDegreeYawAngle', true)
                runMapCoreSafely(() => { MapCore.SMcVector3D.RotateByDegreeYawAngle(vector3D2, tabProperties.rectangleAngle); }, 'PrintMap/PrintToRawRaster.getCalcPrintRect2DObject => SMcVector3D.RotateByDegreeYawAngle', true)
                let polypoints: MapCore.SMcVector3D[] = [];
                let center3DVec = new MapCore.SMcVector3D(tabProperties.rectangleCenter);
                runMapCoreSafely(() => { polypoints.push(MapCore.SMcVector3D.Minus(MapCore.SMcVector3D.Minus(center3DVec, vector3D1), vector3D2)) }, 'PrintMap/PrintToRawRaster.getCalcPrintRect2DObject => SMcVector3D.Minus', true)
                runMapCoreSafely(() => { polypoints.push(MapCore.SMcVector3D.Minus(MapCore.SMcVector3D.Plus(center3DVec, vector3D1), vector3D2)) }, 'PrintMap/PrintToRawRaster.getCalcPrintRect2DObject => SMcVector3D.Minus', true)
                runMapCoreSafely(() => { polypoints.push(MapCore.SMcVector3D.Plus(MapCore.SMcVector3D.Plus(center3DVec, vector3D1), vector3D2)) }, 'PrintMap/PrintToRawRaster.getCalcPrintRect2DObject => SMcVector3D.Minus', true)
                runMapCoreSafely(() => { polypoints.push(MapCore.SMcVector3D.Plus(MapCore.SMcVector3D.Minus(center3DVec, vector3D1), vector3D2)) }, 'PrintMap/PrintToRawRaster.getCalcPrintRect2DObject => SMcVector3D.Minus', true)
                pointInOMCoord = polypoints.map(point => tabProperties.currentViewport.viewport.ViewportToOverlayManagerWorld(point))
            }
            runMapCoreSafely(() => {
                obj = MapCore.IMcObject.Create(activeOverlay, objSchemeItem, MapCore.EMcPointCoordSystem.EPCS_WORLD, pointInOMCoord, false);
            }, 'PrintMap/PrintToRawRaster.getCalcPrintRect2DObject => IMcObject.Create', true)
        }, 'PrintMap/PrintToRawRaster.getCalcPrintRect2DObject')
        return obj;
    }
    const convertScreenToWorld = (screenPoint: MapCore.SMcVector3D, isScreenPoint: boolean): MapCore.SMcVector3D => {
        let worldPoint: { Value?: MapCore.SMcVector3D } = {};
        runCodeSafely(() => {
            let isIntersect: boolean = false;
            if (isScreenPoint) {
                runMapCoreSafely(() => {
                    isIntersect = tabProperties.currentViewport.viewport.ScreenToWorldOnPlane(screenPoint, worldPoint);
                }, 'PrintMap/PrintToRawRaster.convertScreenToWorld => IMcMapCamera.ScreenToWorldOnPlane', true)
            }
            else {
                worldPoint.Value = screenPoint;
            }
            runMapCoreSafely(() => {
                worldPoint.Value = tabProperties.currentViewport.viewport.ViewportToOverlayManagerWorld(worldPoint.Value);
            }, 'PrintMap/PrintToRawRaster.convertScreenToWorld => IMcMapViewport.ViewportToOverlayManagerWorld', true)
        }, 'PrintMap/PrintToRawRaster.convertScreenToWorld')
        return worldPoint.Value;
    }
    const getCalcPrintRectScreen = (pagesCalc: MapCore.SMcBox, color: MapCore.SMcBColor, drawPriority: number, isScreenPoint: boolean) => {
        runCodeSafely(() => {
            let objSchemeItem: MapCore.IMcObjectSchemeItem = getCalcPrintRectScreenScheme(color);
            let obj: MapCore.IMcObject = getCalcPrintRectScreenObject(pagesCalc, objSchemeItem, isScreenPoint);
            if (obj != null) {
                printMapService.addPrintObject(tabProperties.printMapData, obj);
                runMapCoreSafely(() => {
                    obj.SetDrawPriority(drawPriority);
                }, 'PrintMap/PrintToRawRaster.getCalcPrintRectScreen => IMcObject.SetDrawPriority', true)
            }
        }, 'PrintMap/PrintToRawRaster.getCalcPrintRectScreen')
    }
    const downloadFileByMCDownload = (fileName: string, auFileMemoryBuffer: Uint8Array, auWorldFileMemoryBuffer: Uint8Array, isPrintToFileInMemory: boolean) => {
        runCodeSafely(() => {
            let wldFileName = fileName.replace(/\.[^/.]+$/, `.wld`)
            if (isPrintToFileInMemory && auFileMemoryBuffer.length > 0) {
                runMapCoreSafely(() => { MapCore.IMcMapDevice.DownloadBufferAsFile(auFileMemoryBuffer, fileName); }, 'PrintMap/PrintToRawRaster.downloadFileByMCDownload => IMcMapDevice.DownloadBufferAsFile', true)
                if (auWorldFileMemoryBuffer.length > 0) {
                    runMapCoreSafely(() => { MapCore.IMcMapDevice.DownloadBufferAsFile(auWorldFileMemoryBuffer, wldFileName); }, 'PrintMap/PrintToRawRaster.downloadFileByMCDownload => IMcMapDevice.DownloadBufferAsFile', true)
                }
            }
            else {
                runMapCoreSafely(() => { MapCore.IMcMapDevice.DownloadFileSystemFile(fileName); }, 'PrintMap/PrintToRawRaster.downloadFileByMCDownload => IMcMapDevice.DownloadFileSystemFile', true)
                runMapCoreSafely(() => { MapCore.IMcMapDevice.DownloadFileSystemFile(wldFileName); }, 'PrintMap/PrintToRawRaster.downloadFileByMCDownload => IMcMapDevice.DownloadFileSystemFile', true)
            }
            setPropertiesCallback({ secondDialogVisible: false, secondDialogType: '' })
        }, 'PrintMap/PrintToRawRaster.downloadFileByMCDownload', (error) => setPropertiesCallback({ secondDialogVisible: false, secondDialogType: '' }))
    }
    //#region Handle Functions
    //#region Print Screen
    const handleCalcPrintRectClick = () => {
        runCodeSafely(() => {
            const activeOverlay = tabProperties.currentViewport ? ObjectWorldService.findActiveOverlayByMcViewport(tabProperties.currentViewport.viewport) : null;
            if (activeOverlay) {
                let pagesCalc: MapCore.SMcBox;
                if (tabProperties.currentViewport.viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_2D) {
                    pagesCalc = tabProperties.currentViewport.viewport.GetCameraWorldVisibleArea();
                }
                else {
                    let width: { Value?: number } = {};
                    let height: { Value?: number } = {};
                    tabProperties.currentViewport.viewport.GetViewportSize(width, height);
                    pagesCalc.MinVertex = new MapCore.SMcVector3D(0, 0, 0);
                    pagesCalc.MaxVertex = new MapCore.SMcVector3D(width.Value - 1, height.Value - 1, 0);
                }
                getCalcPrintRectScreen(pagesCalc, new MapCore.SMcBColor(255, 0, 0, 255), 1, false);
            }
            else {
                setPropertiesCallback({ confirmDialogVisible: true, confirmDialogMessage: 'Missing active overlay' })
            }
        }, 'PrintMap/PrintToRawRaster.handleCalcPrintRectClick')
    }
    const handleSaveToFilePrintScreenOKClick = (fileName: string, fileType: string) => {
        runCodeSafely(() => {
            let finalFileName = `${fileName}${fileType}`;
            let fileMemoryBuffer: { Value?: Uint8Array } = {};
            let worldFileMemoryBuffer: { Value?: Uint8Array } = {};
            let printCallback = null;
            if (tabProperties.isPrintScreenAsync) {
                printCallback = new MapCoreData.iPrintCallbackClass((eStatus: MapCore.IMcErrors.ECode, strFileNameOrRasterDataFormat: string, auFileMemoryBuffer: Uint8Array, auWorldFileMemoryBuffer: Uint8Array) => {
                    if (eStatus == MapCore.IMcErrors.ECode.SUCCESS) {
                        downloadFileByMCDownload(finalFileName, auFileMemoryBuffer, auWorldFileMemoryBuffer, tabProperties.isPrintToFileInMemory)
                    }
                    else {
                        alert(`MapCore Error From Callback: ${MapCore.IMcErrors.ErrorCodeToString(eStatus)}, \n Error code: ${eStatus}, \n funcName: PrintMap/PrintToRawRaster.handleSaveToFilePrintScreenOKClick => IMcPrintMap.PrintScreenToRawRasterData`)
                        setPropertiesCallback({ secondDialogVisible: false, secondDialogType: '' })
                    }
                });
                tabProperties.printMapData.lastPrintCallback = printCallback;
            }
            let finalFileType = fileType.slice(1);
            runMapCoreSafely(() => {
                tabProperties.currentViewport.viewport.PrintScreenToRawRasterData(
                    tabProperties.resolutionFactor,
                    tabProperties.isPrintToFileInMemory ? finalFileType : finalFileName,
                    tabProperties.isPrintToFileInMemory ? fileMemoryBuffer : null,
                    tabProperties.isPrintToFileInMemory ? worldFileMemoryBuffer : null,
                    printCallback, tabProperties.gdalOptions);
            }, 'PrintMap/PrintToRawRaster.handleSaveToFilePrintScreenOKClick => IMcPrintMap.PrintScreenToRawRasterData', true, (error => {
                setPropertiesCallback({ secondDialogVisible: false, secondDialogType: '' })
            }))
            if (!tabProperties.isPrintScreenAsync) {
                downloadFileByMCDownload(finalFileName, fileMemoryBuffer.Value, worldFileMemoryBuffer.Value, tabProperties.isPrintToFileInMemory)
            }
        }, 'PrintMap/PrintToRawRaster.handleSaveToFilePrintScreenOKClick')
    }
    const handlePrintScreenToRawVectorClick = () => {
        runCodeSafely(() => {
            setPropertiesCallback({ secondDialogVisible: true, secondDialogType: 'SaveToFile', handleSaveToFileOkFunc: handleSaveToFilePrintScreenOKClick })
        }, 'PrintMap/PrintToRawRaster.handlePrintScreenToRawVectorClick')
    }
    //#endregion
    //#region Print Rect 2D
    const handleCalcPrintRect2DClick = () => {
        runCodeSafely(() => {
            const activeOverlay = tabProperties.currentViewport ? ObjectWorldService.findActiveOverlayByMcViewport(tabProperties.currentViewport.viewport) : null;
            if (activeOverlay) {
                let objSchemeItem: MapCore.IMcSymbolicItem = getCalcPrintRect2DScheme();
                let obj = getCalcPrintRect2DObject(activeOverlay, objSchemeItem);
                if (obj != null) {
                    printMapService.addPrintObject(tabProperties.printMapData, obj);
                }
            }
            else {
                setPropertiesCallback({ confirmDialogVisible: true, confirmDialogMessage: 'Missing active overlay' })
            }
        }, 'PrintMap/PrintToRawRaster.handleCalcPrintRect2DClick')
    }
    const handleSaveToFilePrintRect2DOKClick = (fileName: string, fileType: string) => {
        runCodeSafely(() => {
            let finalFileName = `${fileName}${fileType}`;
            let fileMemoryBuffer: { Value?: Uint8Array } = {};
            let worldFileMemoryBuffer: { Value?: Uint8Array } = {};
            let printCallback = null;
            if (tabProperties.isPrintScreenAsync2D) {
                printCallback = new MapCoreData.iPrintCallbackClass((eStatus: MapCore.IMcErrors.ECode, strFileNameOrRasterDataFormat: string, auFileMemoryBuffer: Uint8Array, auWorldFileMemoryBuffer: Uint8Array) => {
                    if (eStatus == MapCore.IMcErrors.ECode.SUCCESS) {
                        downloadFileByMCDownload(finalFileName, auFileMemoryBuffer, auWorldFileMemoryBuffer, tabProperties.isPrintToFileInMemory2D)
                    }
                    else {
                        alert(`MapCore Error From Callback: ${MapCore.IMcErrors.ErrorCodeToString(eStatus)}, \n Error code: ${eStatus}, \n funcName: PrintMap/PrintToRawRaster.handleSaveToFilePrintRect2DOKClick => IMcPrintMap.PrintRect2DToRawRasterData`)
                        setPropertiesCallback({ secondDialogVisible: false, secondDialogType: '' })
                    }
                });
                tabProperties.printMapData.lastPrintCallback = printCallback;
            }
            let finalFileType = fileType.slice(1);
            runMapCoreSafely(() => {
                tabProperties.currentViewport.viewport.PrintRect2DToRawRasterData(tabProperties.rectangleCenter, tabProperties.rectangleSize,
                    tabProperties.rectangleAngle, tabProperties.cameraScale, tabProperties.resolutionFactor2D,
                    tabProperties.isPrintToFileInMemory2D ? finalFileType : finalFileName,
                    tabProperties.isPrintToFileInMemory2D ? fileMemoryBuffer : null,
                    tabProperties.isPrintToFileInMemory2D ? worldFileMemoryBuffer : null,
                    printCallback, tabProperties.gdalOptions, tabProperties.isPrintGeoInMetricPropotion);
            }, 'PrintMap/PrintToRawRaster.handleSaveToFilePrintRect2DOKClick => IMcPrintMap.PrintRect2DToRawRasterData', true, (error => {
                setPropertiesCallback({ secondDialogVisible: false, secondDialogType: '' })
            }))
            if (!tabProperties.isPrintScreenAsync2D) {
                downloadFileByMCDownload(finalFileName, fileMemoryBuffer.Value, worldFileMemoryBuffer.Value, tabProperties.isPrintToFileInMemory2D)
            }
        }, 'PrintMap/PrintToRawRaster.handleSaveToFilePrintRect2DOKClick')
    }
    const handlePrintRect2DToRawRasterClick = () => {
        runCodeSafely(() => {
            setPropertiesCallback({ secondDialogVisible: true, secondDialogType: 'SaveToFile', handleSaveToFileOkFunc: handleSaveToFilePrintRect2DOKClick })
        }, 'PrintMap/PrintToRawRaster.handlePrintRect2DToRawRasterClick')
    }
    const handleGoToLocationClick = () => {
        runCodeSafely(() => {
            let center3Dvector = new MapCore.SMcVector3D(tabProperties.rectangleCenter);
            printMapService.goToLocation(tabProperties.printMapData, center3Dvector);
        }, 'PrintMap/PrintToRawRaster.handleGoToLocationClick')
    }
    //#endregion
    //#endregion

    //#region DOM Functions
    const getSecondDialog = () => {
        switch (tabProperties.secondDialogType) {
            case 'SaveToFile':
                return <SaveToFile savedFileTypeOptions={tabProperties.savedFileTypeOptions} handleSaveToFileOk={tabProperties.handleSaveToFileOkFunc} />
            case 'GdalOptions':
                return <GdalOptions existGdalOptions={tabProperties.gdalOptions} getGdalOptions={(gdalOptions: string[]) => {
                    setPropertiesCallback({ gdalOptions: gdalOptions, secondDialogVisible: false, secondDialogType: '' })
                }} />
            default:
                return <div></div>
        }

    }
    const getPrintScreenFieldset = () => {
        return <Fieldset legend='Print Screen' className="form__column-fieldset">
            <div className="form__flex-and-row-between form__items-center">
                <label htmlFor="resolutionFactor">Resolution factor: </label>
                <InputNumber maxFractionDigits={5} id='resolutionFactor' value={tabProperties.resolutionFactor} name="resolutionFactor" onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '65%' }} className="form__flex-and-row form__items-center">
                    <Checkbox name='isPrintToFileInMemory' inputId="isPrintToFileInMemory" onChange={saveData} checked={tabProperties.isPrintToFileInMemory} />
                    <label htmlFor="isPrintToFileInMemory">Print To File In Memory</label>
                </div>
                <div style={{ width: '33%' }} className="form__flex-and-row form__items-center">
                    <Checkbox name='isPrintScreenAsync' inputId="isPrintScreenAsync" onChange={saveData} checked={tabProperties.isPrintScreenAsync} />
                    <label htmlFor="isPrintScreenAsync">Async</label>
                </div>
            </div>
            <div className="form__flex-and-row-between">
                <Button label="Calculate Print Rectangle" onClick={handleCalcPrintRectClick} />
                <Button label="Print Screen To Raw Vector Data" onClick={handlePrintScreenToRawVectorClick} />
            </div>
        </Fieldset>
    }
    const getPrintRect2DFieldset = () => {
        return <Fieldset legend='Print Rect 2D' className="form__column-fieldset">
            <div className="form__flex-and-row-between form__items-center">
                <span> Rectangle Center:</span>
                <Vector2DFromMap pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.rectangleCenter} lastPoint={true} saveTheValue={(...args) => save2DVector(...args, 'rectangleCenter')} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <span> Rectangle Size:</span>
                <Vector2DFromMap disabledPointFromMap={false} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.rectangleSize} lastPoint={true} saveTheValue={(...args) => save2DVector(...args, 'rectangleSize')} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <label htmlFor="rectangleAngle">Rectangle Angle: </label>
                <InputNumber maxFractionDigits={5} id='rectangleAngle' value={tabProperties.rectangleAngle} name="rectangleAngle" onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <label htmlFor="cameraScale">Camera Scale: </label>
                <InputNumber maxFractionDigits={5} id='cameraScale' value={tabProperties.cameraScale} name="cameraScale" onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <label htmlFor="resolutionFactor2D">Resolution factor: </label>
                <InputNumber maxFractionDigits={5} id='resolutionFactor2D' value={tabProperties.resolutionFactor2D} name="resolutionFactor2D" onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '65%' }} className="form__flex-and-row form__items-center">
                    <Checkbox name='isPrintToFileInMemory2D' inputId="isPrintToFileInMemory2D" onChange={saveData} checked={tabProperties.isPrintToFileInMemory2D} />
                    <label htmlFor="isPrintToFileInMemory2D">Print To File In Memory</label>
                </div>
                <div style={{ width: '33%' }} className="form__flex-and-row form__items-center">
                    <Checkbox name='isPrintScreenAsync2D' inputId="isPrintScreenAsync2D" onChange={saveData} checked={tabProperties.isPrintScreenAsync2D} />
                    <label htmlFor="isPrintScreenAsync2D">Async</label>
                </div>
            </div>
            <div className="form__flex-and-row form__items-center">
                <Checkbox name='isPrintGeoInMetricPropotion' inputId="isPrintGeoInMetricPropotion" onChange={saveData} checked={tabProperties.isPrintGeoInMetricPropotion} />
                <label htmlFor="isPrintGeoInMetricPropotion">Print Geo In Metric Propotion</label>
            </div>
            <div className="form__flex-and-row-between">
                <Button label="Calculate Print Rectangle" onClick={handleCalcPrintRect2DClick} />
                <Button label="Print Rect 2D To Raw Vector Data" onClick={handlePrintRect2DToRawRasterClick} />
            </div>
            <Button label="Go To Location" onClick={handleGoToLocationClick} />
        </Fieldset>
    }
    //#endregion

    return <div className="form__column-container">
        {getPrintScreenFieldset()}
        {getPrintRect2DFieldset()}
        <Button style={{ width: '30%' }} className="form__aligm-self-center" label="Gdal Options" onClick={e => setPropertiesCallback({ secondDialogVisible: true, secondDialogType: 'GdalOptions' })} />

        <ConfirmDialog
            contentClassName='form__confirm-dialog-content'
            message={tabProperties.confirmDialogMessage}
            header=''
            footer={<div></div>}
            visible={tabProperties.confirmDialogVisible}
            onHide={e => { setPropertiesCallback('confirmDialogVisible', false) }}
        />

        <Dialog visible={tabProperties.secondDialogVisible} onHide={() => setPropertiesCallback({ secondDialogVisible: false, secondDialogType: '' })}>
            {getSecondDialog()}
        </Dialog>
    </div>
}