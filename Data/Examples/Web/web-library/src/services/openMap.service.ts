import _ from 'lodash';

import viewportService from "./viewport.service";
import { runCodeLibrarySafely, runMapCoreSafely } from "./error-handler.service";
import { Layerslist, ViewportParams } from "../model/layerDetails";
import { ViewportData } from "../model/viewportData";
import { OverlayManager } from "../model/overlayManager"
import MapCoreData from '../mapcore-data';
import mapcoreData from "../mapcore-data";
import { DetailedSectionMapViewportWindow, DetailedViewportWindow, OppositeDimensionViewportWindow, ViewportWindowType } from "../model/viewportWindow";
import layerService from './layer.service';
import MapCoreService from './mapCore.service';

class OpenMapService {
    coordinateSystem: any = null;
    viewportParams: ViewportParams;
    calcSizeAndPositioinCanvases: () => void;
    viewportId: number;
    terrains: MapCore.IMcMapTerrain[] = [];

    constructor() {
        MapCoreData.layersCallback = this.createIReadCallbacksClass();
    }

    public addNewViewportStandard(viewportId: number, layerslist: Layerslist, canvas: HTMLCanvasElement, viewportParams: ViewportParams,
        calcSizeAndPositioinCanvases: () => void) {
        this.viewportParams = viewportParams;
        this.calcSizeAndPositioinCanvases = calcSizeAndPositioinCanvases;
        this.viewportId = viewportId;
        this.coordinateSystem = viewportParams.coordinateSystem;

        const viewportData: ViewportData = new ViewportData(viewportParams.mapType, this.viewportId, null, canvas);
        MapCoreData.viewportsData.push(viewportData);

        if ((layerslist.newLayers.length == 0) && (layerslist.existingLayers.length == 0))
            throw new Error("No layers selected to open.")
        let allLayersToOpenViewport = this.initLayers(layerslist)
        this.terrains = [MapCoreService.createTerrain(allLayersToOpenViewport, null, this.viewportParams.displayItemsAttachedToStaticObjectsWithoutDtm)];
        layerService.releaseStandAloneLayers(allLayersToOpenViewport);
    }
    public addNewViewportDetailed(detailedVp: DetailedViewportWindow, vpCreateData: MapCore.IMcMapViewport.SCreateData, calcSizeAndPositioinCanvases: () => void) {
        this.terrains = detailedVp.terrains

        const viewportData: ViewportData = new ViewportData(vpCreateData.eMapType, detailedVp.id, null, vpCreateData.hWnd);
        MapCoreData.viewportsData.push(viewportData);

        let viewport: MapCore.IMcMapViewport;
        if (detailedVp.type == ViewportWindowType.detailedWindow) {
            viewport = viewportService.createViewport(vpCreateData, detailedVp.terrains, viewportData, detailedVp.querySecondaryDtmLayers);
            MapCoreService.releaseStandAloneTerrains(detailedVp.terrains);
            layerService.releaseStandAloneLayers(detailedVp.querySecondaryDtmLayers);
        }
        else if (detailedVp.type == ViewportWindowType.detailedSectionMapViewport) {
            const detailedSectionMapVp = detailedVp as DetailedSectionMapViewportWindow;
            if (detailedSectionMapVp.pointsWithHeights)
                runMapCoreSafely(() => { viewport = MapCore.IMcSectionMapViewport.CreateSection(null, vpCreateData, detailedSectionMapVp.terrains, detailedSectionMapVp.sectionRoutePoints, detailedSectionMapVp.pointsWithHeights) }, "addNewViewportDetailed=>IMcSectionMapViewport.CreateSection", true)
            else
                runMapCoreSafely(() => { viewport = MapCore.IMcSectionMapViewport.CreateSection(null, vpCreateData, detailedSectionMapVp.terrains, detailedSectionMapVp.sectionRoutePoints) }, "addNewViewportDetailed=>IMcSectionMapViewport.CreateSection", true)
            MapCoreService.releaseStandAloneTerrains(detailedSectionMapVp.terrains);
            viewportData.viewport = viewport;
            viewportData.editMode = MapCore.IMcEditMode.Create(viewport)
        }
        calcSizeAndPositioinCanvases && calcSizeAndPositioinCanvases();
    }
    public addNewViewportOppositeDimention(oppositeDimensionViewport: OppositeDimensionViewportWindow, canvas: HTMLCanvasElement, calcSizeAndPositioinCanvases: () => void) {
        let pCamera: { Value?: MapCore.IMcMapCamera } = {};
        let newCreateParams: MapCore.IMcMapViewport.SCreateData = oppositeDimensionViewport.originalViewportData.viewport.GetCreateParams();
        newCreateParams.hWnd = canvas;
        newCreateParams.eMapType = newCreateParams.eMapType == MapCore.IMcMapCamera.EMapType.EMT_2D ? MapCore.IMcMapCamera.EMapType.EMT_3D : MapCore.IMcMapCamera.EMapType.EMT_2D;
        this.terrains = oppositeDimensionViewport.originalViewportData.viewport.GetTerrains();
        let mcNewViewport: MapCore.IMcMapViewport = null;
        runMapCoreSafely(() => { mcNewViewport = MapCore.IMcMapViewport.Create(pCamera, newCreateParams, this.terrains, oppositeDimensionViewport.originalViewportData.viewport.GetQuerySecondaryDtmLayers()); }, "OpenMapService.addNewViewportOppositeDimention => IMcMapViewport.Create", true)
        const viewportData: ViewportData = new ViewportData(newCreateParams.eMapType, oppositeDimensionViewport.id, mcNewViewport, newCreateParams.hWnd);
        runMapCoreSafely(() => { viewportData.editMode = MapCore.IMcEditMode.Create(mcNewViewport) }, "OpenMapService.addNewViewportOppositeDimention => IMcEditMode.Create", true)
        MapCoreData.viewportsData.push(viewportData);
        calcSizeAndPositioinCanvases();
    }
    public continueCreating() {
        for (const t of this.terrains) {
            const layers = t.GetLayers();
            for (const l of layers) {
                if (l.IsInitialized())
                    if (l.GetCoordinateSystem()) {
                        this.coordinateSystem = l.GetCoordinateSystem()
                    }
            }
        }
        let vpCreateData: MapCore.IMcMapViewport.SCreateData = null
        const viewportData: ViewportData = MapCoreData.findViewport(this.viewportId)
        let overlayManager: MapCore.IMcOverlayManager = this.viewportParams?.overlayManager;
        if (overlayManager == null) {
            overlayManager = MapCoreService.createOverlayManager(this.coordinateSystem);
            let overlay = MapCoreService.createOverlay(overlayManager);
            mapcoreData.overlayManagerArr.find((OM: OverlayManager) => OM.overlayManager == overlayManager).overlayActive = overlay;
        }
        vpCreateData = this.createVPCreateData(viewportData.canvas, MapCoreData.device, viewportData.mapType, overlayManager, this.viewportParams);
        let secondaryDtmLayers = this.createQuerySecondaryDtmLayers(this.viewportParams.querySecondaryDtmLayers);
        viewportService.createViewport(vpCreateData, this.terrains, viewportData, secondaryDtmLayers);
        MapCoreService.releaseStandAloneTerrains(this.terrains);
        layerService.releaseStandAloneLayers(secondaryDtmLayers);
        this.calcSizeAndPositioinCanvases && this.calcSizeAndPositioinCanvases()
    }

    public initLayers(layerslist: Layerslist): MapCore.IMcMapLayer[] {
        layerslist.existingLayers.forEach(l => {
            if (l.IsInitialized())
                if (l.GetCoordinateSystem()) {
                    this.coordinateSystem = l.GetCoordinateSystem();
                }
        })
        return [...layerService.createLayers(layerslist.newLayers), ...layerslist.existingLayers];
    }
    public createQuerySecondaryDtmLayers(querySecondaryDtmLayers: Layerslist): MapCore.IMcDtmMapLayer[] {
        if (querySecondaryDtmLayers) {
            let layers: MapCore.IMcMapLayer[] = this.initLayers(querySecondaryDtmLayers)
            return layers.map(i => { return i as MapCore.IMcDtmMapLayer })
        }
        return [];
    }
    public areAllLayersInitialized(): boolean {
        let allInitialized = true;
        let terrains = this.terrains;
        for (const t of terrains) {
            const layers = t.GetLayers();
            for (const l of layers) {
                if (!l.IsInitialized()) {
                    allInitialized = false;
                    break;
                }
            }
            if (!allInitialized) {
                break;
            }
        }
        return allInitialized;
    }
    private createVPCreateData(canvas: HTMLCanvasElement, device: MapCore.IMcMapDevice, mapType: MapCore.IMcMapCamera.EMapType, overlayManager: MapCore.IMcOverlayManager, ViewportParams?: ViewportParams) {
        let vpCreateData: MapCore.IMcMapViewport.SCreateData;
        runMapCoreSafely(() => { vpCreateData = new MapCore.IMcMapViewport.SCreateData(mapType) }, "OpenMapService.createVPCreateData=> IMcMapViewport.SCreateDat", true)
        vpCreateData.pDevice = device;
        vpCreateData.pOverlayManager = overlayManager;
        vpCreateData.uViewportID = 1;
        vpCreateData.pCoordinateSystem = this.coordinateSystem;
        vpCreateData.hWnd = canvas;
        vpCreateData.bShowGeoInMetricProportion = ViewportParams.showGeo;
        // vpCreateData.fTerrainPrecisionFactor = ViewportParams.terrainFactor;
        return vpCreateData;
    }

    //#region Local callback
    private createIReadCallbacksClass(): MapCore.IMcMapLayer.IReadCallback {
        return MapCore.IMcMapLayer.IReadCallback.extend("IMcMapLayer.IReadCallback",
            {
                __construct: function (OnRemoved: any, OnReplaced: any) {
                    this.__parent.__construct.call(this);
                    this.OnRemoved = OnRemoved;
                    this.OnReplaced = OnReplaced;
                },
                // mandatory
                OnInitialized: (pLayer: MapCore.IMcMapLayer, eStatus: MapCore.IMcErrors.ECode, strAdditionalDataString: string) => {
                    runCodeLibrarySafely(() => {
                        if (eStatus == MapCore.IMcErrors.ECode.SUCCESS) {
                            let layerCoordinateSystem = pLayer.GetCoordinateSystem()
                            if (layerCoordinateSystem) {
                                this.coordinateSystem = layerCoordinateSystem;
                                MapCoreService.addOnlyNewCoordinateSystemToList(layerCoordinateSystem);
                            }
                        }
                        else {
                            pLayer?.RemoveLayerAsync();
                            console.log('OnInitialized layer: status is not success : ' + eStatus.constructor.name);
                            throw new Error('OnInitialized layer: status is not success : ' + eStatus.constructor.name)
                        }
                    }, "OpenMapService.onInitialized")
                },
                // mandatory
                OnReadError: function (pLayer: MapCore.IMcMapLayer, eErrorCode: MapCore.IMcErrors.ECode, strAdditionalDataString: string) {
                    console.log("Layer read error: " + MapCore.IMcErrors.ErrorCodeToString(eErrorCode) + " (" + strAdditionalDataString + ")");
                },
                // mandatory
                OnNativeServerLayerNotValid: function (pLayer: MapCore.IMcMapLayer, bLayerVersionUpdated: boolean) {
                    if (bLayerVersionUpdated) {
                        if (confirm("The layer's version was updated by a server. Do you want to replace the layer?")) {
                            pLayer.ReplaceNativeServerLayerAsync(pLayer.GetCallback());
                        }
                    }
                    else {
                        if (confirm("The layer's ID was not found by a server. Do you want to remove the layer?")) {
                            pLayer.RemoveLayerAsync();
                        }
                    }
                },
                // optional
                OnRemoved: function (pLayer: MapCore.IMcMapLayer, eStatus: MapCore.IMcErrors.ECode, strAdditionalDataString: string) {
                    this.OnRemoved(arguments);
                },
                // optional
                OnReplaced: function (pOldLayer: MapCore.IMcMapLayer, pNewLayer: MapCore.IMcMapLayer, eStatus: MapCore.IMcErrors.ECode, strAdditionalDataString: string) {
                    this.OnReplaced(arguments);
                },
            }
        );
    }
    //#endregion
}

export default OpenMapService

