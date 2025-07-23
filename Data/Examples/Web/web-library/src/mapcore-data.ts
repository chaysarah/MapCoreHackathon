import { OverlayManager } from "./model/overlayManager";
import { ViewportData } from "./model/viewportData";

class MapCoreData {
    astrServiceMetadataURLs: string = "";
    asyncOperationCallBacksClass: any;
    iMcOverlayManagerAsyncOperationCallbackClass: any;
    iMcEditModeCallbacksClass: any;
    iMcQueryCallbackClass: any;
    iPrintCallbackClass: any;
    iMcUserDataClass: any;
    layersCallback: any;

    device: MapCore.IMcMapDevice = null;
    viewportsData: ViewportData[] = [];
    overlayManagerArr: OverlayManager[] = [];
    standAloneLayers: MapCore.IMcMapLayer[] = [];
    standAloneTerrains: MapCore.IMcMapTerrain[] = [];
    gridArr: MapCore.IMcMapGrid[] = [];
    heightLinesArr: MapCore.IMcMapHeightLines[] = [];
    coordinateSystemArr: { pCoordinateSystem: MapCore.IMcGridCoordinateSystem, numInstancePointers: number }[] = [];
    textureArr: MapCore.IMcTexture[] = [];
    imageCalcArr: MapCore.IMcImageCalc[] = [];
    //To Do
    //להעביר מקום 
    TargetFound: MapCore.IMcSpatialQueries.STargetFound[] = null;
    whenTargetFoundFilled: () => void = null
    ScanGeometry: MapCore.SMcScanGeometry = null;

    onErrorCallback: (error: any, occuredAt: string) => void = null
    isCatchErrors: boolean = true;

    public findViewport(id: number) {
        return this.viewportsData.find(vp => vp.id === id);
    }
}

export default new MapCoreData()