class ObjectWorldTreePropertiesBase {
    // @ OVERLAY Properties
    //Objects Form
    //--File
    isVersionChecked: boolean = false;
    isStorageFormat: boolean = false;
    isContinueSaveChecked: boolean = false;
    //--Memory Buffer
    formatDropDownValue: MapCore.IMcOverlayManager.EStorageFormat = MapCore.IMcOverlayManager.EStorageFormat.ESF_JSON;
   //--Native vector 
    minSizeFactor: number = 1;
    maxSizeFactor: number = 1;
    numTilesInFileEdge: number = 1;
    //--Raw Vector
    geometryFilter: MapCore.EGeometry = MapCore.EGeometry.UnSupportedGeometry;
    layerName: string = '';
    cameraScale: number = 12.8;
    cameraYawAngle: number = 0;
    isAsyncChecked: boolean = false;
    isAsMemoryBufferChecked: boolean = false;
    additionalFiles: string[] | MapCore.SMcFileInMemory[] = [];

    //Tiling Scheme Params
    tilingOriginX: number = 1;
    tilingOriginY: number = 1;
    tilingSchemeTypeValue: MapCore.IMcMapLayer.ETilingSchemeType = MapCore.IMcMapLayer.ETilingSchemeType.ETST_MAPCORE;
    largestTileSize: number = 66584576;
    tileSizeInPx: number = 508;
    numLargestTilesX: number = 1;
    numLargestTilesY: number = 1;
    rasterTileMarginInPx: number = 2;

    //General Form
    // @ OVERLAY_MANAGERR Properties
    isShowGeo: boolean = false;
    isNonExistentAmplifiers: boolean = false;
    symbologyStandard: MapCore.IMcObject.ESymbologyStandard = MapCore.IMcObject.ESymbologyStandard.ESS_APP6D;
}
export default ObjectWorldTreePropertiesBase;