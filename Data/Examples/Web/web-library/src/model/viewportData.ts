import { runMapCoreSafely } from "../services/error-handler.service";

export class ViewportData {
    id: number;
    viewport: MapCore.IMcMapViewport;
    mapType: MapCore.IMcMapCamera.EMapType;
    editMode: MapCore.IMcEditMode;
    canvas: HTMLCanvasElement;
    gridType: string;

    constructor(mapType: MapCore.IMcMapCamera.EMapType, id: number, viewport: MapCore.IMcMapViewport, canvas: HTMLCanvasElement) {
        this.id = id;
        this.viewport = viewport;
        this.canvas = canvas;
        this.mapType = mapType;
        this.gridType= null;
    }
}