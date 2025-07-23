import objectWorldService from "../objectWorld.service";
import { ViewportData } from "../../model/viewportData";
import mapcoreData from "../../mapcore-data";
import { runMapCoreSafely } from "../error-handler.service";
// import scanService from "../../scan.service";

class ScanPolygonGeometry {
    ScanPolygonGeometry: MapCore.SMcScanPolygonGeometry;
    EditMode: MapCore.IMcEditMode;
    SpatialQueryParams: MapCore.IMcSpatialQueries.SQueryParams;
    CompletelyInside: boolean;
    ScanCoordSys: MapCore.EMcPointCoordSystem;
    prevCallback: any;
    viewportData: ViewportData;

    constructor(coordSys: MapCore.EMcPointCoordSystem, EditMode: MapCore.IMcEditMode, CompletelyInsideOnly?: boolean, Tolerance?: Number, SQParams?: MapCore.IMcSpatialQueries.SQueryParams) {
        this.CompletelyInside = CompletelyInsideOnly;
        this.SpatialQueryParams = SQParams;
        this.EditMode = EditMode;
        this.ScanCoordSys = coordSys;
        this.prevCallback = new this.IMcEditModeCallbacks();
    }
    public StartPolyScan(viewportData: ViewportData) {
        this.viewportData = viewportData;
        let activeOverlay = objectWorldService.findActiveOverlayByMcViewport(viewportData.viewport)
        let locationPoints: MapCore.SMcVector3D[] = [];
        let ObjSchemeItem: MapCore.IMcPolygonItem = null;
        runMapCoreSafely(() => {
            ObjSchemeItem = MapCore.IMcPolygonItem.Create((MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN as any).value,
                MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID,
                MapCore.bcBlackOpaque,
                2,
                null,
                new MapCore.SMcFVector2D(0, -1),
                1,
                MapCore.IMcLineBasedItem.EFillStyle.EFS_SOLID,
                new MapCore.SMcBColor(0, 255, 100, 100)
            );
        }, "scanPolygonGeometry.StartPolyScan => IMcPolygonItem.Create", true)

        let obj: MapCore.IMcObject = null;
        runMapCoreSafely(() => {
            obj = MapCore.IMcObject.Create(activeOverlay,
                ObjSchemeItem,
                MapCore.EMcPointCoordSystem.EPCS_SCREEN,
                locationPoints,
                false);
        }, "scanPolygonGeometry.StartPolyScan => IMcObject.Create", true)
        //In order to prevent retrieval of the polygon in the scan 
        runMapCoreSafely(() => { ObjSchemeItem.SetDetectibility(false); }, "scanPolygonGeometry.StartPolyScan => IMcObjectSchemeItem.SetDetectibility", true)
        runMapCoreSafely(() => {
            ObjSchemeItem.SetDrawPriorityGroup(MapCore.IMcSymbolicItem.EDrawPriorityGroup.EDPG_TOP_MOST
                , 2
            );
        }, "scanPolygonGeometry.StartPolyScan => IMcSymbolicItem.SetDrawPriorityGroup", true)
        runMapCoreSafely(() => { ObjSchemeItem.SetDrawPriority(127); }, "scanPolygonGeometry.StartPolyScan => IMcSymbolicItem.SetDrawPriority", true)

        // let activeViewportID: Uint8Array;
        // activeViewportID[0] = viewportData.id;
        let viewportSelector: MapCore.IMcConditionalSelector = null;
        runMapCoreSafely(() => {
            viewportSelector = MapCore.IMcViewportConditionalSelector.Create(viewportData.viewport.GetOverlayManager(),
                // MapCore.IMcViewportConditionalSelector.EViewportTypeFlags.EVT_ALL_VIEWPORTS,
                // MapCore.IMcViewportConditionalSelector.EViewportCoordinateSystem.EVCS_ALL_COORDINATE_SYSTEMS,
                // activeViewportID,
                // true
            );
        }, "scanPolygonGeometry.StartPolyScan => IMcViewportConditionalSelector.Create", true)
        runMapCoreSafely(() => {
            obj.SetConditionalSelector(MapCore.IMcConditionalSelector.EActionType.EAT_VISIBILITY,
                true,
                viewportSelector);
        }, "scanPolygonGeometry.StartPolyScan => IMcObject.SetConditionalSelector", true)
        runMapCoreSafely(() => { obj.SetDrawPriority(127); }, "scanPolygonGeometry.StartPolyScan => IMcObject.SetDrawPriority", true)
        runMapCoreSafely(() => { viewportData.editMode.StartInitObject(obj, ObjSchemeItem); }, "scanPolygonGeometry.StartPolyScan => IMcEditMode.StartInitObject", true)
        runMapCoreSafely(() => { this.prevCallback = this.EditMode.GetEventsCallback(); }, "scanPolygonGeometry.StartPolyScan => IMcEditMode.GetEventsCallback", true)
        runMapCoreSafely(() => { viewportData.editMode.SetEventsCallback(new this.IMcEditModeCallbacks()); }, "scanPolygonGeometry.StartPolyScan => IMcEditMode.SetEventsCallback", true)
    }
    IMcEditModeCallbacks: any = this.createIMcEditModeCallbacksClass(this);
    private createIMcEditModeCallbacksClass(data: ScanPolygonGeometry): MapCore.IMcEditMode.ICallback {
        return MapCore.IMcEditMode.ICallback.extend("IMcEditMode.ICallback",
            {
                // optional
                ExitAction: function () {
                    data.EditMode.SetEventsCallback(data.prevCallback);
                },

                // optional
                Release: function () {
                    // this.delete();
                },
                InitItemResults(pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) {
                    if (nExitCode != 0) {
                        let objScheme: MapCore.IMcObjectScheme = null;
                        runMapCoreSafely(() => { objScheme = pObject.GetScheme(); }, "scanPolygonGeometry.createIMcEditModeCallbacksClass => IMcObject.GetScheme", true)
                        let objSchemeNode: MapCore.IMcObjectSchemeNode[] = null;
                        runMapCoreSafely(() => { objSchemeNode = objScheme.GetNodes((MapCore.IMcObjectSchemeNode.ENodeKindFlags.ENKF_ANY_ITEM as any).value); }, "scanPolygonGeometry.createIMcEditModeCallbacksClass => IMcObjectScheme.GetNodes", true)
                        let Coords: MapCore.SMcVector3D[] = null;
                        runMapCoreSafely(() => { Coords = objSchemeNode[0].GetCoordinates(data.viewportData.viewport, data.ScanCoordSys, pObject); }, "scanPolygonGeometry.createIMcEditModeCallbacksClass => IMcObjectSchemeNode.GetCoordinates", true)
                        for (let idx = 0; idx < Coords.length; ++idx) { Coords[idx].z = 0; }
                        //Remove rectangle Item from the map
                        runMapCoreSafely(() => { pObject.Remove(); }, "scanPolygonGeometry.createIMcEditModeCallbacksClass => IMcObject.Remove", true)
                        // if (Coords.length == 2) {
                        let boxCoord: MapCore.SMcBox = new MapCore.SMcBox(Coords[0], Coords[1]);
                        data.ScanPolygonGeometry = new MapCore.SMcScanBoxGeometry(data.ScanCoordSys, boxCoord);
                        data.ScanPolygonGeometry.uGeometryType = MapCore.SMcScanBoxGeometry.GEOMETRY_TYPE;
                        let TargetFound: MapCore.IMcSpatialQueries.STargetFound[];
                        runMapCoreSafely(() => { TargetFound = data.viewportData.viewport.ScanInGeometry(data.ScanPolygonGeometry, data.CompletelyInside, data.SpatialQueryParams); }, "scanPolygonGeometry.createIMcEditModeCallbacksClass => IMcSpatialQueries.ScanInGeometry", true)
                        mapcoreData.TargetFound = TargetFound;
                        mapcoreData.whenTargetFoundFilled();
                        mapcoreData.ScanGeometry = data.ScanPolygonGeometry;
                    }
                }
            });
    }
}

export default ScanPolygonGeometry