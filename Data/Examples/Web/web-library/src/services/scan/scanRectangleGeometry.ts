// import mapCoreService from "../MC/mapCore.service";
// import objectWorldService from "../MC/objectWorld.service";
// import scanService from "../MC/scan.service";
// import { setDialogType } from "../redux/MapCore/mapCoreAction";
// import store from "../redux/store";
// import { DialogTypesEnum } from "./enums";
// import { ViewportData } from "./viewportData";
import objectWorldService from "../objectWorld.service";
import { ViewportData } from "../../model/viewportData";
import mapcoreData from "../../mapcore-data";
import { runMapCoreSafely } from "../error-handler.service";
import { PaintRectangle } from "../editMode/paintRectangle";
// import scanService from "../../scan.service";


class ScanRectangleGeometry {
    ScanBoxGeometry: MapCore.SMcScanBoxGeometry;
    EditMode: MapCore.IMcEditMode;
    SpatialQueryParams: MapCore.IMcSpatialQueries.SQueryParams;
    CompletelyInside: boolean;
    ScanCoordSys: MapCore.EMcPointCoordSystem;
    prevCallback: any;
    viewportData: ViewportData

    constructor(coordSys: MapCore.EMcPointCoordSystem, EditMode: MapCore.IMcEditMode, CompletelyInsideOnly?: boolean, Tolerance?: Number, SQParams?: MapCore.IMcSpatialQueries.SQueryParams) {
        this.CompletelyInside = CompletelyInsideOnly;
        this.SpatialQueryParams = SQParams;
        this.EditMode = EditMode;
        this.ScanCoordSys = coordSys;
        this.prevCallback = null;
    }
    public StartRectScan(viewportData: ViewportData) {
        this.viewportData = viewportData
        let paintRectangle: PaintRectangle = new PaintRectangle(this.ScanCoordSys, this.onPaintRectResults)
        paintRectangle.StartPaintRect(this.viewportData)
     }
    public onPaintRectResults = (Coords: MapCore.SMcVector3D[]) => {
        if (Coords.length == 2) {
            let boxCoord: MapCore.SMcBox = new MapCore.SMcBox(Coords[0], Coords[1]);
            this.ScanBoxGeometry = new MapCore.SMcScanBoxGeometry(this.ScanCoordSys, boxCoord);
            this.ScanBoxGeometry.uGeometryType = MapCore.SMcScanBoxGeometry.GEOMETRY_TYPE;
            let TargetFound: MapCore.IMcSpatialQueries.STargetFound[];
            runMapCoreSafely(() => { TargetFound = this.viewportData.viewport.ScanInGeometry(this.ScanBoxGeometry, this.CompletelyInside, this.SpatialQueryParams); }, "scanRectangleGeometry.createIMcEditModeCallbacksClass => IMcSpatialQueries.ScanInGeometry", true)
            mapcoreData.TargetFound = TargetFound;
            mapcoreData.whenTargetFoundFilled();
            mapcoreData.ScanGeometry = this.ScanBoxGeometry;
        }
    }
 }
export default ScanRectangleGeometry