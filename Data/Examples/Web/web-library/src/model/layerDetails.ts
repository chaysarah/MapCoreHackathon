import { LayerNameEnum, LayerSourceEnum } from "./enums";

export class Layerslist {
    newLayers: LayerDetails[];
    existingLayers: MapCore.IMcMapLayer[];

    constructor(existingLayers: MapCore.IMcMapLayer[], newLayers: LayerDetails[]) {
        this.existingLayers = existingLayers;
        this.newLayers = newLayers;
    }
}
export class LayerDetails {
    path: string;
    layerSource: LayerSourceEnum;
    layerParams: LayerParams;
    layerIReadCallback: MapCore.IMcMapLayer.IReadCallback;
    name?: LayerNameEnum;
    filePathesObjs?:{ filePath: any; label: any;}[]

    constructor(path: string, layerSource: LayerSourceEnum, layerParams: LayerParams, layerIReadCallback: MapCore.IMcMapLayer.IReadCallback, name?: LayerNameEnum) {
        this.path = path;
        this.layerSource = layerSource;
        this.layerParams = layerParams;
        this.layerIReadCallback = layerIReadCallback;
        this.name = name;
    }
}
export class ViewportParams {
    mapType: MapCore.IMcMapCamera.EMapType;
    terrainFactor: number;
    showGeo: boolean;
    overlayManager?: MapCore.IMcOverlayManager;
    coordinateSystem?: MapCore.IMcGridCoordinateSystem;
    tilingScheme?: MapCore.IMcMapLayer.STilingScheme;
    requestParams?: MapCore.SMcKeyStringValue[];
    querySecondaryDtmLayers?: Layerslist;
    displayItemsAttachedToStaticObjectsWithoutDtm:boolean;

    constructor(mapType: MapCore.IMcMapCamera.EMapType, terrainFactor: number, showGeo: boolean,
        overlayManager?: MapCore.IMcOverlayManager, coordinateSystem?: MapCore.IMcGridCoordinateSystem
        , tilingScheme?: MapCore.IMcMapLayer.STilingScheme, requestParams?: MapCore.SMcKeyStringValue[], querySecondaryDtmLayers?: Layerslist
    ) {
        this.mapType = mapType;
        this.terrainFactor = terrainFactor;
        this.showGeo = showGeo;
        this.overlayManager = overlayManager;
        this.coordinateSystem = coordinateSystem;
        this.tilingScheme = tilingScheme;
        this.requestParams = requestParams;
        this.querySecondaryDtmLayers = querySecondaryDtmLayers;
        this.displayItemsAttachedToStaticObjectsWithoutDtm=false;
    }
}
export class LayerParams {
    rawParams?: MapCore.IMcMapLayer.SRawParams;
    threeDExtrusionParams?: MapCore.IMcRawVector3DExtrusionMapLayer.SParams;
    rawVectorParams?: MapCore.IMcRawVectorMapLayer.SParams;

    urlServer?: string;
    WMS?: MapCore.IMcMapLayer.SWMSParams;
    WMTS?: MapCore.IMcMapLayer.SWMTSParams;
    WCS?: MapCore.IMcMapLayer.SWCSParams;
    CSW?: MapCore.IMcMapLayer.SWebMapServiceParams;
    serverLayerInfo?: MapCore.IMcMapLayer.SServerLayerInfo;

    firstLowerQualityLevel?: number;
    numLevelsToIgnore?: number;
    imageCoordinateSystem?: boolean;
    targetHighestResolution?: number;
    orthometricHeights?: boolean;

    requestParams? : MapCore.SMcKeyStringValue[]; 
    positionOffset? : MapCore.SMcVector3D;
    clipRect?: MapCore.SMcBox;
    targetCoordSys?: MapCore.IMcGridCoordinateSystem;
    extrusionHeight?: number;
    enhanceBorder?: boolean;
    thereAreMissingFiles?: boolean;
    NO_OfLayers?: number;
    indexingData?: IndexingData;

    constructor() {
        this.rawParams = new MapCore.IMcMapLayer.SRawParams();
        this.threeDExtrusionParams = new MapCore.IMcRawVector3DExtrusionMapLayer.SParams();
        this.enhanceBorder = false;
        this.targetCoordSys = null;
        this.extrusionHeight = 0;
        this.rawVectorParams = new MapCore.IMcRawVectorMapLayer.SParams("",null);
        this.orthometricHeights = false;
        this.targetHighestResolution = 0.05;
        this.firstLowerQualityLevel = 0;
        this.imageCoordinateSystem = false;
        this.indexingData = null;
        this.NO_OfLayers = 1;

        this.urlServer = "";
        this.WMS = new MapCore.IMcMapLayer.SWMSParams();
        this.WMTS = new MapCore.IMcMapLayer.SWMTSParams();
        this.WCS = new MapCore.IMcMapLayer.SWCSParams();
        this.CSW = new MapCore.IMcMapLayer.SWebMapServiceParams();
        this.serverLayerInfo = new MapCore.IMcMapLayer.SServerLayerInfo();

         this.requestParams=[]; 
         this.positionOffset=new MapCore.SMcVector3D(0,0,0);
         this.clipRect=new MapCore.SMcBox()
         this.numLevelsToIgnore=0
         this.thereAreMissingFiles=false
    }
}
export class IndexingData {
    strIndexingDataDirectory?: string;
    isDefaultIndex?: boolean;

    constructor(strIndexingDataDirectory: string, isDefaultIndex: boolean) {
        this.strIndexingDataDirectory = strIndexingDataDirectory;
        this.isDefaultIndex = isDefaultIndex;
    }
}
