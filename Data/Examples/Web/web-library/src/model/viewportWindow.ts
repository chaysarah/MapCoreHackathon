import { Layerslist, ViewportParams } from "./layerDetails";
import { ViewportData } from "./viewportData";

enum ViewportWindowType {
    fromUI = 1,
    fromJson,
    detailedWindow,
    detailedSectionMapViewport,
    oppositeDimensionViewport,
}
class ViewportWindow {
    id: number;
    position: { x: number, y: number };
    type: ViewportWindowType;
    waitForTheLayersToInitialize: boolean;

    constructor(id: number, position: { x: number, y: number }) {
        this.id = id;
        this.position = position;
        this.waitForTheLayersToInitialize = true
    }
}
class JsonViewportWindow extends ViewportWindow {
    jsonFilePath: string;
    jsonDataBuffer: { fileName: string, fileBuffer: Uint8Array }[];
    printViewportPath: string;
    constructor(id: number, position: { x: number, y: number }, jsonFilePath: string, jsonDataBuffer: { fileName: string, fileBuffer: Uint8Array }[], printViewportPath: string) {
        super(id, position);
        this.type = ViewportWindowType.fromJson;
        this.jsonFilePath = jsonFilePath;
        this.jsonDataBuffer = jsonDataBuffer;
        this.printViewportPath = printViewportPath;
    }
}
class StandardViewportWindow extends ViewportWindow {
    layerslist: Layerslist;
    ViewportParams: ViewportParams;

    constructor(id: number, layerslist: Layerslist, position: { x: number, y: number }, viewportParams: ViewportParams) {
        super(id, position);
        this.type = ViewportWindowType.fromUI;
        this.layerslist = layerslist;
        this.ViewportParams = viewportParams;
    }
}
class OppositeDimensionViewportWindow extends ViewportWindow {
    originalViewportData: ViewportData;

    constructor(id: number, position: { x: number, y: number }, originalViewportData: ViewportData) {
        super(id, position);
        this.type = ViewportWindowType.oppositeDimensionViewport;
        this.originalViewportData = originalViewportData;
    }
}
class DetailedViewportWindow extends ViewportWindow {
    terrains: MapCore.IMcMapTerrain[];
    cameraPosition: MapCore.SMcVector3D;
    querySecondaryDtmLayers: MapCore.IMcDtmMapLayer[];

    constructor(id: number, position: { x: number, y: number }, terrains: MapCore.IMcMapTerrain[], cameraPosition: MapCore.SMcVector3D, querySecondaryDtmLayers?: MapCore.IMcDtmMapLayer[]) {
        super(id, position);
        this.type = ViewportWindowType.detailedWindow;
        this.terrains = terrains;
        this.cameraPosition = cameraPosition;
        this.querySecondaryDtmLayers = querySecondaryDtmLayers;
    }
}
class DetailedSectionMapViewportWindow extends DetailedViewportWindow {
    sectionRoutePoints: MapCore.SMcVector3D[]
    pointsWithHeights: MapCore.SMcVector3D[]

    constructor(id: number, position: { x: number, y: number }, terrains: MapCore.IMcMapTerrain[], cameraPosition: MapCore.SMcVector3D, sectionRoutePoints: MapCore.SMcVector3D[], pointsWithHeights?: MapCore.SMcVector3D[]) {
        super(id, position, terrains, cameraPosition);
        this.type = ViewportWindowType.detailedSectionMapViewport;
        this.sectionRoutePoints = sectionRoutePoints;
        this.pointsWithHeights = pointsWithHeights
    }
}

export { ViewportWindowType, ViewportWindow, JsonViewportWindow, StandardViewportWindow, OppositeDimensionViewportWindow, DetailedViewportWindow, DetailedSectionMapViewportWindow }
