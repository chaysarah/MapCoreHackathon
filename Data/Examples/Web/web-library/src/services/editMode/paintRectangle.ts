import objectWorldService from "../objectWorld.service";
import { ViewportData } from "../../model/viewportData";
import mapcoreData from "../../mapcore-data";
import { runMapCoreSafely } from "../error-handler.service";

// paint temporary rectangle for defining bounding box (Scan, FindObjects & HeatMapPoints)
// TODO:
// 1. more generic for all geometries
// 2. remove immediately and return coordinates / return the object
export class PaintRectangle {
    viewportData: ViewportData
    rectCoordSys: MapCore.EMcPointCoordSystem;
    prevCallback: any;
    onRectResultsCallback: (Coords: MapCore.SMcVector3D[]) => void;

    constructor(coordSys: MapCore.EMcPointCoordSystem, onRectResultsCallback: (Coords: MapCore.SMcVector3D[]) => void) {
        this.viewportData = null;
        this.rectCoordSys = coordSys;
        this.prevCallback = null;
        this.onRectResultsCallback = onRectResultsCallback;
    } 
    public StartPaintRect(viewportData: ViewportData) {
        this.viewportData = viewportData

        let activeOverlay = objectWorldService.findActiveOverlayByMcViewport(viewportData.viewport)
        let locationPoints: MapCore.SMcVector3D[] = [];
        let ObjSchemeItem: MapCore.IMcRectangleItem = null;
        runMapCoreSafely(() => {
            ObjSchemeItem = MapCore.IMcRectangleItem.Create((MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN as any).value,
                MapCore.EMcPointCoordSystem.EPCS_SCREEN,
                MapCore.IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_VIEWPORT,
                MapCore.IMcRectangleItem.ERectangleDefinition.ERD_RECTANGLE_DIAGONAL_POINTS,
                0,
                0,
                MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID,
                MapCore.bcBlackOpaque,
                2,
                null,
                new MapCore.SMcFVector2D(0, -1),
                1,
                MapCore.IMcLineBasedItem.EFillStyle.EFS_SOLID,
                new MapCore.SMcBColor(0, 255, 100, 100));
        }, "PaintRectangle.StartPaintRect => IMcRectangleItem.Create", true)

        let obj: MapCore.IMcObject = null;
        runMapCoreSafely(() => {
            obj = MapCore.IMcObject.Create(activeOverlay,
                ObjSchemeItem,
                MapCore.EMcPointCoordSystem.EPCS_SCREEN,
                locationPoints,
                false);
        }, "PaintRectangle.StartPaintRect => IMcObject.Create", true)

        //In order to prevent retrieval of the polygon in the scan 
        runMapCoreSafely(() => { ObjSchemeItem.SetDetectibility(false); }, "PaintRectangle.StartPaintRect => IMcObjectSchemeItem.SetDetectibility", true)
        runMapCoreSafely(() => {
            ObjSchemeItem.SetDrawPriorityGroup(MapCore.IMcSymbolicItem.EDrawPriorityGroup.EDPG_TOP_MOST
                , (MapCore.IMcProperty.EPredefinedPropertyIDs.EPPI_SHARED_PROPERTY_ID as any).value
            );
        }, "PaintRectangle.StartPaintRect => IMcSymbolicItem.SetDrawPriorityGroup", true)
        runMapCoreSafely(() => { ObjSchemeItem.SetDrawPriority(127); }, "PaintRectangle.StartPaintRect => IMcSymbolicItem.SetDrawPriority", true)
        let viewportSelector: MapCore.IMcConditionalSelector = null;
        runMapCoreSafely(() => {
            viewportSelector = MapCore.IMcViewportConditionalSelector.Create(viewportData.viewport.GetOverlayManager());
        }, "PaintRectangle.StartPaintRect => IMcViewportConditionalSelector.Create", true)
        runMapCoreSafely(() => {
            obj.SetConditionalSelector(MapCore.IMcConditionalSelector.EActionType.EAT_VISIBILITY,
                true,
                viewportSelector);
        }, "PaintRectangle.StartPaintRect => IMcObject.SetConditionalSelector", true)
        runMapCoreSafely(() => { obj.SetDrawPriority(127); }, "PaintRectangle.StartPaintRect => IMcObject.SetDrawPriority", true)
        runMapCoreSafely(() => { viewportData.editMode.StartInitObject(obj, ObjSchemeItem); }, "PaintRectangle.StartPaintRect => IMcEditMode.StartInitObject", true)
        runMapCoreSafely(() => { this.prevCallback = viewportData.editMode.GetEventsCallback(); }, "PaintRectangle.StartPaintRect => IMcEditMode.GetEventsCallback", true)
        runMapCoreSafely(() => { viewportData.editMode.SetEventsCallback(new this.IMcEditModeCallbacks()); }, "PaintRectangle.StartPaintRect => IMcEditMode.SetEventsCallback", true)
    }
    IMcEditModeCallbacks: any = this.createIMcEditModeCallbacksClass(this);
    private createIMcEditModeCallbacksClass(data: PaintRectangle): MapCore.IMcEditMode.ICallback {
        return MapCore.IMcEditMode.ICallback.extend("IMcEditMode.ICallback",
            {
                // optional
                ExitAction: function () {
                    runMapCoreSafely(() => { data.viewportData.editMode.SetEventsCallback(data.prevCallback); }, "PaintRectangle.createIMcEditModeCallbacksClass => IMcEditMode.SetEventsCallback", true)
                },
                // optional
                Release: function () {
                    // this.delete();
                },
                InitItemResults(pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) {
                    if (nExitCode != 0) {
                        let objScheme: MapCore.IMcObjectScheme = null;
                        objScheme = pObject.GetScheme();
                        let objSchemeNode: MapCore.IMcObjectSchemeNode[] = null;
                        objSchemeNode = objScheme.GetNodes((MapCore.IMcObjectSchemeNode.ENodeKindFlags.ENKF_ANY_ITEM as any).value);
                        let Coords: MapCore.SMcVector3D[] = null;
                        Coords = objSchemeNode[0].GetCoordinates(data.viewportData.viewport, data.rectCoordSys, pObject);
                        for (let idx = 0; idx < Coords.length; ++idx) { Coords[idx].z = 0; }
                        //Remove rectangle Item from the map
                        runMapCoreSafely(() => { pObject.Remove(); }, "PaintRectangle.createIMcEditModeCallbacksClass => IMcObject.Remove", true)
                        data.onRectResultsCallback(Coords)
                    }
                }
            });
    }
};
