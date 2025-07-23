import { runMapCoreSafely } from "../error-handler.service";
import mapcoreData from "../../mapcore-data";
import { ViewportData } from "../../model/viewportData";
class ScanPointGeometry {

    ScanPointGeometry: MapCore.SMcScanPointGeometry;
    EditMode: MapCore.IMcEditMode;
    SelectedPoint: { x: number; y: number };
    PointItem: MapCore.SMcVector3D;
    SpatialQueryParams: MapCore.IMcSpatialQueries.SQueryParams;
    CompletelyInside: boolean;
    Tolerance: number;
    ScanCoordSys: MapCore.EMcPointCoordSystem;
    ManualPoint: MapCore.SMcVector3D;

    constructor(coordSys: MapCore.EMcPointCoordSystem, SQParams: MapCore.IMcSpatialQueries.SQueryParams,
        CompletelyInsideOnly: boolean, Tolerance: number, point: MapCore.SMcVector3D, viewportData: ViewportData) {
        this.ScanCoordSys = coordSys;
        this.SpatialQueryParams = SQParams;
        this.CompletelyInside = CompletelyInsideOnly;
        this.Tolerance = Tolerance;
        this.ManualPoint = point;
        this.EditMode = viewportData.editMode;
    }

    StartPointScan(viewportData: ViewportData, PointOnMap: MapCore.SMcFVector3D
        , PointIn3D?: MapCore.SMcVector3D, PointInImage?: MapCore.SMcVector3D,
        IsHasIntersection?: Boolean) {
        let ScanPointGeometry: MapCore.SMcScanPointGeometry;
        ScanPointGeometry = new MapCore.SMcScanPointGeometry(this.ScanCoordSys, PointOnMap, this.Tolerance);
        let TargetFound = null;
        runMapCoreSafely(() => { TargetFound = viewportData.viewport.ScanInGeometry(ScanPointGeometry, this.CompletelyInside, this.SpatialQueryParams); }, "ScanPointGeometry.StartPointScan => IMcSpatialQueries.ScanInGeometry", true)
        mapcoreData.TargetFound = TargetFound;
        mapcoreData.whenTargetFoundFilled();
        mapcoreData.ScanGeometry = ScanPointGeometry
    }
    StartManualPointScan(viewportData: ViewportData) {
        let ScanPointGeometry = new MapCore.SMcScanPointGeometry(this.ScanCoordSys, this.ManualPoint, this.Tolerance);
        let TargetFound = null;
        runMapCoreSafely(() => { TargetFound = viewportData.viewport.ScanInGeometry(ScanPointGeometry, this.CompletelyInside, this.SpatialQueryParams); }, "ScanPointGeometry.StartPointScan => IMcSpatialQueries.ScanInGeometry", true)
        mapcoreData.TargetFound = TargetFound;
        mapcoreData.whenTargetFoundFilled()
    }
}
export default ScanPointGeometry
