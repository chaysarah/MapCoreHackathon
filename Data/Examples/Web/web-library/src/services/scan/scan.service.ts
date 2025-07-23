import ScanPointGeometry from "./ScanPointGeometry";
import { getEnumDetailsList, getEnumValueDetails } from "../tools.service";
import { ViewportData } from "../../model/viewportData";
import ScanPolygonGeometry from "./scanPolygonGeometry";
import ScanRectangleGeometry from "./scanRectangleGeometry";
import mapcoreData from "../../mapcore-data";
import { runMapCoreSafely } from "../error-handler.service";
import { MapCoreData } from "../..";

class ScanService {

    pointScan: ScanPointGeometry = null;
    bPointScan: boolean = false;

    doPointScan(coordSys: MapCore.EMcPointCoordSystem, viewportData: ViewportData,
        CompletelyInside: boolean, Tolerance: number, SpatialQueryParams: MapCore.IMcSpatialQueries.SQueryParams) {
        this.pointScan = new ScanPointGeometry(coordSys, SpatialQueryParams, CompletelyInside,
            Tolerance, new MapCore.SMcVector3D(0, 0, 0), viewportData)
        this.bPointScan = true;
    }
    doSpecificPoint(coordSys: MapCore.EMcPointCoordSystem, viewportData: ViewportData,
        CompletelyInside: boolean, Tolerance: number, point: MapCore.SMcVector3D, SpatialQueryParams: MapCore.IMcSpatialQueries.SQueryParams) {
        this.pointScan = new ScanPointGeometry(coordSys, SpatialQueryParams, CompletelyInside,
            Tolerance, point, viewportData)
        this.pointScan.StartManualPointScan(viewportData)
    }
    doPolyScan(CoordSys: MapCore.EMcPointCoordSystem,
        viewportData: ViewportData,
        CompletelyInside: boolean,
        Tolerance: number,
        SpatialQueryParams: MapCore.IMcSpatialQueries.SQueryParams,
    ) {
        let polyScan: ScanPolygonGeometry = new ScanPolygonGeometry(
            CoordSys,
            viewportData.editMode,
            CompletelyInside,
            Tolerance,
            SpatialQueryParams
        );
        polyScan.StartPolyScan(viewportData);
    }
    doRectScan(coordSys: MapCore.EMcPointCoordSystem, viewportData: ViewportData,
        CompletelyInside: boolean, Tolerance: number, SpatialQueryParams: MapCore.IMcSpatialQueries.SQueryParams,) {
        let BoxScan: ScanRectangleGeometry = new ScanRectangleGeometry(
            coordSys,
            viewportData.editMode,
            CompletelyInside,
            Tolerance,
            SpatialQueryParams);

        BoxScan.StartRectScan(viewportData);
    }
    public GetTargetIdByBitCount = (target: MapCore.IMcSpatialQueries.STargetFound) => {

        let bitCount = 32;
        if (target.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER &&
            this.ifVector3DExtrusion(target.pTerrainLayer)) {
            bitCount = this.ifVector3DExtrusion(target.pTerrainLayer).GetObjectIDBitCount();
        }
        else if (this.ifVectorLayer(target)) {
            bitCount = 64;;
        }

        let sTargetID;
        switch (bitCount) {
            case 32:
                sTargetID = target.uTargetID.u32Bit;
                break;
            case 64:
                let mcVectorMapLayer: MapCore.IMcVectorMapLayer = (this.ifVectorLayer(target))
                if (mcVectorMapLayer) {
                    let pstrDataSourceNames: string[] = [];
                    mcVectorMapLayer.GetLayerDataSources(pstrDataSourceNames);
                    if (pstrDataSourceNames != null && pstrDataSourceNames.length > 1) {
                        let puDataSourceID: any = 0;
                        let pstrDataSourceName: any = {};
                        let puOriginalID: any = 0;
                        mcVectorMapLayer.VectorItemIDToOriginalID(MapCore.SMcVariantID.Get53Bit(target.uTargetID), puOriginalID, pstrDataSourceName, puDataSourceID);
                        sTargetID = MapCore.SMcVariantID.Get53Bit(target.uTargetID);
                        sTargetID = MapCore.SMcVariantID.Get53Bit(target.uTargetID).toString() + " (" + puOriginalID +
                            ", " + puDataSourceID.toString() + ", " + pstrDataSourceName.Value + ")";
                    }
                    else
                        sTargetID = MapCore.SMcVariantID.Get53Bit(target.uTargetID);;
                }
                else
                    sTargetID = MapCore.SMcVariantID.Get53Bit(target.uTargetID);;
                break;
            case 128:
                let buffer: Uint8Array = MapCore.SMcVariantID.Get128Bit(target.uTargetID);
                sTargetID = Buffer.from(buffer).toString();
                break;
            default:
                sTargetID = target.uTargetID.u32Bit; break;
        }
        return sTargetID;
    }



    public ifVectorLayer = (target: MapCore.IMcSpatialQueries.STargetFound): MapCore.IMcVectorMapLayer => {
        let IMcVectorMapLayer;

        let r = getEnumValueDetails(target.eTargetType, getEnumDetailsList(MapCore.IMcSpatialQueries.EIntersectionTargetType))
        if (r.theEnum == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_VISIBLE_VECTOR_LAYER) {

            IMcVectorMapLayer = target.pTerrainLayer as MapCore.IMcVectorMapLayer;
            return IMcVectorMapLayer;
        }
        else
            return null
    }

    public ifVector3DExtrusion = (TerrainLayer: MapCore.IMcMapLayer): MapCore.IMcVector3DExtrusionMapLayer => {
        // if(  TerrainLayer  is ((MapCore.IMcVectorMapLayer)as any ))
        let t = TerrainLayer as MapCore.IMcVectorMapLayer
        let Vector3DExtrusion;
        if ((TerrainLayer?.GetLayerType() == MapCore.IMcNativeVector3DExtrusionMapLayer.LAYER_TYPE) ||
            (TerrainLayer?.GetLayerType() == MapCore.IMcNativeServerVector3DExtrusionMapLayer.LAYER_TYPE) ||
            (TerrainLayer?.GetLayerType() == MapCore.IMcRawVector3DExtrusionMapLayer.LAYER_TYPE)) {
            Vector3DExtrusion = TerrainLayer as MapCore.IMcVector3DExtrusionMapLayer;
            return Vector3DExtrusion;
        }

        return null
    }

    public if3DModel = (TerrainLayer: MapCore.IMcMapLayer): MapCore.IMcRaw3DModelMapLayer => {
        let _3DModel ;
        if (TerrainLayer?.GetLayerType() == MapCore.IMcRaw3DModelMapLayer.LAYER_TYPE){
            _3DModel = TerrainLayer as MapCore.IMcRaw3DModelMapLayer;
            return _3DModel;
        }
        return null
    }

    public SetHeight = (height: number) => {
        mapcoreData.TargetFound.forEach(itemFound => {
            if (itemFound.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER) {
                let layer: MapCore.IMcVector3DExtrusionMapLayer = this.ifVector3DExtrusion(itemFound.pTerrainLayer)
                runMapCoreSafely(() => {
                    layer.SetObjectExtrusionHeight(itemFound.uTargetID, height);
                }, "scanService.SetHeight => IMcVector3DExtrusionMapLayer.SetObjectExtrusionHeight", true)
            }
        })
        return true;
    }

    public RemoveHeight = (mVector3DExtrusionOldHeight: number[]) => {
        let objectHeightIndex: number = 0
        mapcoreData.TargetFound.forEach(itemFound => {
            if (itemFound.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER) {
                let layer: MapCore.IMcVector3DExtrusionMapLayer = this.ifVector3DExtrusion(itemFound.pTerrainLayer)
                runMapCoreSafely(() => {
                    layer.SetObjectExtrusionHeight(itemFound.uTargetID, mVector3DExtrusionOldHeight[objectHeightIndex]);
                }, "scanService.RemoveHeight => IMcVector3DExtrusionMapLayer.SetObjectExtrusionHeight", true)
                objectHeightIndex++;
            }
        })
    }
    public RemoveAllHeight = () => {
        let objectHeightIndex: number = 0
        mapcoreData.TargetFound.forEach(itemFound => {
            if (itemFound.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER) {
                let layer: MapCore.IMcVector3DExtrusionMapLayer = this.ifVector3DExtrusion(itemFound.pTerrainLayer)
                runMapCoreSafely(() => { layer.RemoveAllObjectsExtrusionHeights() }, "scanService.RemoveAllHeight => MapCore.IMcVector3DExtrusionMapLayer", true)
            }
        })
    }
    public SetColor = (color: MapCore.SMcBColor) => {
        mapcoreData.TargetFound.forEach(itemFound => {
            if (itemFound.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER) {
                let layer = this.ifVector3DExtrusion(itemFound.pTerrainLayer)||this.if3DModel(itemFound.pTerrainLayer)
                let objColor: MapCore.IMcStaticObjectsMapLayer.SObjectColor = new MapCore.IMcStaticObjectsMapLayer.SObjectColor();
                objColor.uObjectID = itemFound.uTargetID
                objColor.Color = color
                runMapCoreSafely(() => { layer?.SetObjectsColors([objColor]) }, "scanService.SetColor => IMcVector3DExtrusionMapLayer.SetObjectsColors", true)
            }
        })
    }
    public RemoveColor = (mVector3DExtrusionOldColors: MapCore.IMcStaticObjectsMapLayer.SObjectColor[]) => {
        let objectColorIndex: number = 0
        mapcoreData.TargetFound.forEach(itemFound => {
            if (itemFound.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER) {
                let layer= this.ifVector3DExtrusion(itemFound.pTerrainLayer)||this.if3DModel(itemFound.pTerrainLayer)
                runMapCoreSafely(() => {
                    layer?.SetObjectsColors([mVector3DExtrusionOldColors[objectColorIndex]])
                }, "scanService.RemoveColor => IMcVector3DExtrusionMapLayer.SetObjectsColors", true)
                objectColorIndex++;
            }
        })
    }
    public RemoveAllColor = () => {
        mapcoreData.TargetFound.forEach(itemFound => {
            if (itemFound.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER) {
                let layer= this.ifVector3DExtrusion(itemFound.pTerrainLayer)||this.if3DModel(itemFound.pTerrainLayer)
                runMapCoreSafely(() => { layer.RemoveAllObjectsColors() }, "scanService.RemoveAllColor => IMcVector3DExtrusionMapLayer.RemoveAllObjectsColors", true)
            }
        })
    }
    public SetContoursColor = (ColorsContours: MapCore.SMcBColor, c: MapCore.IMcSpatialQueries.SStaticObjectContour, activeOverlay: MapCore.IMcOverlay): MapCore.IMcObject => {
        let subTypeFlags: number =
            getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags)).code |
            getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ACCURATE_3D_SCREEN_WIDTH, getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags)).code

        let ObjSchemeItem = MapCore.IMcPolygonItem.Create(subTypeFlags,
            MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID,
            ColorsContours,
            3,
            null,
            new MapCore.SMcFVector2D(0, -1),
            1,
            MapCore.IMcLineBasedItem.EFillStyle.EFS_NONE);

        let obj: MapCore.IMcObject = MapCore.IMcObject.Create(activeOverlay,
            ObjSchemeItem,
            MapCore.EMcPointCoordSystem.EPCS_WORLD,
            c.aPoints,
            false);
        return obj
    }
    public RemoveColorVerctor = (overlay: MapCore.IMcOverlay) => {
        if (overlay) {
            overlay.Remove();
        }
    }
    public SetVectorColor = (viewportId: number, targetsFound: MapCore.IMcSpatialQueries.STargetFound[]): MapCore.IMcOverlay => {
        let viewport = MapCoreData.findViewport(viewportId)?.viewport
        if (!viewport) {
            throw new Error(`Viewport ${viewportId} is not found.`)
        }
        const overlay: MapCore.IMcOverlay = MapCore.IMcOverlay.Create(viewport.GetOverlayManager());
        targetsFound.forEach((itemFound) => {
            if ((itemFound.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_VISIBLE_VECTOR_LAYER)) {
                let vectorLayer: MapCore.IMcVectorMapLayer = itemFound.pTerrainLayer as MapCore.IMcVectorMapLayer;
                let ScanGeometry: MapCore.SMcScanGeometry = MapCoreData.ScanGeometry;

                vectorLayer.GetScanExtendedData(ScanGeometry, itemFound, viewport, {}, {}, new mapcoreData.asyncOperationCallBacksClass(
                    (pLayer: MapCore.IMcMapLayer, eStatus: MapCore.IMcErrors.ECode, VectorItems: MapCore.IMcMapLayer.SVectorItemFound[], aUnifiedVectorItemsPoints: MapCore.SMcVector3D[]) => {
                        let isNeedToDoConnectItems: boolean = true;
                        let unifiedItem: MapCore.IMcSymbolicItem;
                        if (aUnifiedVectorItemsPoints.length > 1) {
                            let bIfDefaultItemIsPolygon = false
                            if (itemFound.ObjectItemData.pItem != null) {
                                bIfDefaultItemIsPolygon = itemFound.ObjectItemData.pItem.GetNodeType() == MapCore.IMcPolygonItem.NODE_TYPE
                                if (!bIfDefaultItemIsPolygon) {
                                    let mcObjectScheme: MapCore.IMcObjectScheme = itemFound.ObjectItemData.pItem.GetScheme();
                                    if (mcObjectScheme != null) {
                                        let defaultItem: any = mcObjectScheme.GetEditModeDefaultItem();
                                        if (defaultItem == null) {
                                            let mcObjectLocation: MapCore.IMcObjectLocation = mcObjectScheme.GetObjectLocation(0);
                                            if (mcObjectLocation != null) {
                                                let mcObjectSchemeNodes: MapCore.IMcObjectSchemeNode[] = mcObjectLocation.GetChildren();
                                                if (mcObjectSchemeNodes != null && mcObjectSchemeNodes.length > 0)
                                                    defaultItem = mcObjectSchemeNodes[0];
                                            }
                                        }
                                        if (defaultItem != null) {
                                            bIfDefaultItemIsPolygon = defaultItem.GetNodeType() == MapCore.IMcPolygonItem.NODE_TYPE;
                                        }
                                    }
                                }
                            }
                            if (bIfDefaultItemIsPolygon) {
                                unifiedItem = MapCore.IMcPolygonItem.Create(
                                    (MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN as any).value,
                                    MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, new MapCore.SMcBColor(0, 0, 255, 255), 2, null,
                                    new MapCore.SMcFVector2D(0, -1), 1, MapCore.IMcLineBasedItem.EFillStyle.EFS_SOLID, new MapCore.SMcBColor(255, 255, 255, 80));
                                if (VectorItems != null && (VectorItems).find(x => x.uVectorItemID == MapCore.MC_EXTRA_CONTOUR_VECTOR_ITEM_ID)) // check if exist empty polygon - to declare them as sub items
                                {
                                    isNeedToDoConnectItems = false;
                                    let mcSubItemData = new Array();
                                    //  MapCore.SMcSubItemData[VectorItems.length];
                                    mcSubItemData[0] = new MapCore.SMcSubItemData();
                                    mcSubItemData[0].uSubItemID = MapCore.UINT_MAX;
                                    mcSubItemData[0].nPointsStartIndex = 0;
                                    for (let i = 1; i < VectorItems.length; i++) {
                                        mcSubItemData[i] = new MapCore.SMcSubItemData();
                                        mcSubItemData[i].uSubItemID = MapCore.MC_EXTRA_CONTOUR_SUB_ITEM_ID;
                                        mcSubItemData[i].nPointsStartIndex = VectorItems[i].uVectorItemFirstPointIndex;
                                    }
                                    unifiedItem.SetSubItemsData(new MapCore.IMcProperty.SArrayPropertySubItemData(mcSubItemData), (MapCore.IMcProperty.EPredefinedPropertyIDs.EPPI_SHARED_PROPERTY_ID as any).value);
                                }
                            }
                            else

                                unifiedItem = MapCore.IMcLineItem.Create(
                                    (MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN as any).value,
                                    MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, new MapCore.SMcBColor(0, 0, 255, 255), 2
                                );
                        }
                        else {
                            unifiedItem = MapCore.IMcEllipseItem.Create(
                                (MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN as any).value,
                                MapCore.EMcPointCoordSystem.EPCS_SCREEN, MapCore.IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_VIEWPORT, 10, 10,
                                0, 360, 0, MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, new MapCore.SMcBColor(0, 0, 255, 255), 2, null,
                                new MapCore.SMcFVector2D(0, -1), 1, MapCore.IMcLineBasedItem.EFillStyle.EFS_NONE);
                        }
                        unifiedItem.SetDrawPriority(1);
                        // let overlay = ObjectWorldService.findActiveOverlay(MapCoreData.findViewport(activeCard))
                        let obj: MapCore.IMcObject = MapCore.IMcObject.Create(overlay, unifiedItem,
                            MapCore.EMcPointCoordSystem.EPCS_WORLD
                            , aUnifiedVectorItemsPoints, false);
                        if (isNeedToDoConnectItems && VectorItems != null && VectorItems.length != 0) {
                            for (let i = 0; i < VectorItems.length; ++i) {

                                let colorDelta = 0;
                                let line: MapCore.IMcLineItem;
                                VectorItems.forEach((vectorItem: any) => {
                                    line = MapCore.IMcLineItem.Create((MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN as any).value,
                                        MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID,
                                        new MapCore.SMcBColor(255 - colorDelta, colorDelta, colorDelta, 255), 10
                                    );
                                    colorDelta = ((colorDelta + 64) % 256);
                                    line.Connect(unifiedItem);
                                    line.SetAttachPointType(0, MapCore.IMcSymbolicItem.EAttachPointType.EAPT_INDEX_POINTS);
                                    line.SetAttachPointIndex(0, vectorItem.uVectorItemFirstPointIndex
                                        , (MapCore.IMcProperty.EPredefinedPropertyIDs.EPPI_SHARED_PROPERTY_ID as any).value);
                                    line.SetNumAttachPoints(0, vectorItem.uVectorItemLastPointIndex + 1 - vectorItem.uVectorItemFirstPointIndex
                                        , (MapCore.IMcProperty.EPredefinedPropertyIDs.EPPI_SHARED_PROPERTY_ID as any).value);
                                })
                            }
                        }
                    }
                ));
            }
        })
        return overlay;
    }
    public getPoint = (target: MapCore.IMcSpatialQueries.STargetFound, currentViewport: number, resultCB: (arr: any, aUnifiedVectorItemsPoints: MapCore.SMcVector3D[]) => void) => {
        let arr: any = []
        if (target)
            if (target.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_VISIBLE_VECTOR_LAYER) {
                let vectorItems: any = {}, unifiedVectorItemsPoints: MapCore.SMcVector3D[] = [];
                let vectorLayer: MapCore.IMcVectorMapLayer = target.pTerrainLayer as MapCore.IMcVectorMapLayer;
                let ScanGeometry: MapCore.SMcScanGeometry = MapCoreData.ScanGeometry;
                let viewport = MapCoreData.findViewport(currentViewport)?.viewport
                vectorLayer.GetScanExtendedData(ScanGeometry, target, viewport
                    , vectorItems, unifiedVectorItemsPoints, new MapCoreData.asyncOperationCallBacksClass(
                        (pLayer: MapCore.IMcMapLayer, eStatus: MapCore.IMcErrors.ECode, VectorItems: MapCore.IMcMapLayer.SVectorItemFound[], aUnifiedVectorItemsPoints: MapCore.SMcVector3D[]) => {
                            aUnifiedVectorItemsPoints = aUnifiedVectorItemsPoints.map((p, index) => { return { ...p, index: index + 1 } })
                            let VectorItemID: string = '0'
                            for (let index = 0; index < VectorItems.length; index++) {
                                if (VectorItems[index].uVectorItemID == MapCore.MC_EXTRA_CONTOUR_VECTOR_ITEM_ID)
                                    VectorItemID = "hole"
                                else
                                    VectorItemID = VectorItems[index].uVectorItemID.toString();
                                let obj = {
                                    index: index + 1,
                                    VectorItemID: VectorItemID,
                                    VectorItemFirstPointIndex: VectorItems[index].uVectorItemFirstPointIndex,
                                    VectorItemLastPointIndex: VectorItems[index].uVectorItemLastPointIndex
                                }
                                arr.push(obj)
                            }
                            resultCB(arr, aUnifiedVectorItemsPoints)
                            return
                        }
                    ))
            }
    }

    public GetObjectLocationData = (location: number, target: MapCore.IMcSpatialQueries.STargetFound): boolean => {
        let PropId = {}, bParam = {}
        let object: MapCore.IMcObject = target.ObjectItemData.pObject;
        let ObjectLocation = object.GetScheme().GetObjectLocation(location);
        return ObjectLocation.GetRelativeToDTM(PropId);
    }

}
export default new ScanService();