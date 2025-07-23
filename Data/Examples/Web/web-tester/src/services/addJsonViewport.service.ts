import { MapCoreData, ViewportService, OverlayManager, ViewportData, MapCoreService, JsonViewportWindow } from 'mapcore-lib';
import generalService from "./general.service";
import { runCodeSafely, runMapCoreSafely } from "../common/services/error-handling/errorHandler";
import { resizeViewport } from '../redux/mapWindow/mapWindowAction';
import store from "../redux/store";

var filterProccessingOperation: string[] = [
    'EFPO_NO_FILTER',
    'EFPO_SMOOTH_LOW',
    'EFPO_SMOOTH_MID',
    'EFPO_SMOOTH_HIGH',
    'EFPO_SHARP_LOW',
    'EFPO_SHARP_MID',
    'EFPO_SHARP_HIGH',
    'EFPO_CUSTOM_FILTER'];

var colorChannel: string[] = [
    'ECC_RED',
    'ECC_GREEN',
    'ECC_BLUE',
    'ECC_MULTI_CHANNEL'];

export type GridCrsType = {
    GridCoordinateSystemType: string;
    Datum: string;
    Zone: number;
    EpsgCode: string
}

export type MaterialSchemeDefinition = {
    Key: string;
    Value: string;
}

export type DebugOption = {
    Key: number;
    Value: number;
}

class addJsonViewportService {
    uri: any;
    jsonData: any;
    printViewportPath: string;
    folderName: string;
    overlayManager: MapCore.IMcOverlayManager = null;
    viewport: MapCore.IMcMapViewport = null;
    overlay: MapCore.IMcOverlay = null;
    moveObjects: boolean = false;                // Are random objects move?

    async readDeviceParamsFromJson(jsonFilePathOrBuffer: any, isBuffer = false) {
        let jsonData;
        if (!isBuffer) {
            let response = await fetch(jsonFilePathOrBuffer)
            if (response.status !== 200) {
                return (`Failed to load json file ${jsonFilePathOrBuffer}`);
            }
            jsonData = await response.json();
        }
        else {
            //convert from Uint8Array to JSON
            let decoder = new TextDecoder();
            let stringJsonBuffer = decoder.decode(jsonFilePathOrBuffer)
            jsonData = JSON.parse(stringJsonBuffer);
        }
        this.initDeviceParams(jsonData);
    }

    // ./MapCore_VTest/2D_DTMVis_1/FullViewportDataParams_GLES_Master.json
    private initDeviceParams(jsonData: any) {
        if (!MapCoreData.device) {
            const init = generalService.mapCorePropertiesBase.deviceInitParams;
            init.uNumBackgroundThreads = jsonData.MapDevice.BackgroundThreads;
            init.eLoggingLevel = MapCore.IMcMapDevice.ELoggingLevel[jsonData.MapDevice.LoggingLevelText.slice(1)] as any;
            init.eViewportAntiAliasingLevel = MapCore.IMcMapDevice.EAntiAliasingLevel[jsonData.MapDevice.ViewportAntiAliasingLevelText.slice(1)] as any
            init.eTerrainObjectsAntiAliasingLevel = MapCore.IMcMapDevice.EAntiAliasingLevel[jsonData.MapDevice.TerrainObjectsAntiAliasingLevelText.slice(1)] as any
            init.eTerrainObjectsQuality = MapCore.IMcMapDevice.ETerrainObjectsQuality[jsonData.MapDevice.TerrainObjectsQualityText.slice(1)] as any
            init.uDtmVisualizationPrecision = jsonData.MapDevice.DtmVisualizationPrecision;
            init.fObjectsBatchGrowthRatio = jsonData.MapDevice.ObjectsBatchGrowthRatio;
            init.uObjectsTexturesAtlasSize = jsonData.MapDevice.ObjectsTexturesAtlasSize;
            init.bObjectsTexturesAtlas16bit = jsonData.MapDevice.ObjectsTexturesAtlas16bit;
            init.bDisableDepthBuffer = jsonData.MapDevice.DisableDepthBuffer;
            init.eRenderingSystem = MapCore.IMcMapDevice.ERenderingSystem[jsonData.MapDevice.RenderingSystemText.slice(1)] as any;
            init.bIgnoreRasterLayerMipmaps = jsonData.MapDevice.IgnoreRasterLayerMipmaps;
            init.bFullScreen = jsonData.MapDevice.MultiScreenDevice;
            init.uNumTerrainTileRenderTargets = jsonData.MapDevice.NumTerrainTileRenderTargets;
            init.bPreferUseTerrainTileRenderTargets = jsonData.MapDevice.PreferUseTerrainTileRenderTargets;
            init.uObjectsBatchInitialNumVertices = jsonData.MapDevice.ObjectsBatchInitialNumVertices;
            init.uObjectsBatchInitialNumIndices = jsonData.MapDevice.ObjectsBatchInitialNumIndices;
            init.bEnableObjectsBatchEnlarging = jsonData.MapDevice.EnableObjectsBatchEnlarging;
            init.bAlignScreenSizeObjects = jsonData.MapDevice.AlignScreenSizeObjects;
            init.uWebRequestRetryCount = jsonData.MapDevice.WebRequestRetryCount ?? 10;
            init.uAsyncQueryTilesMaxActiveWebRequests = jsonData.MapDevice.AsyncQueryTilesMaxActiveWebRequests ?? 10;
            // init.strConfigFilesDirectory = 'dfdfd'
        }
    }
    private display = async (jsonVp: JsonViewportWindow, jsonData: any, currCanvas: HTMLCanvasElement) => {
        let jsonFilePath = jsonVp.jsonFilePath;
        this.jsonData = jsonData;
        let createData: MapCore.IMcMapViewport.SCreateData;
        let eMapType = jsonData.MapViewport.ViewportMapType.slice(1);
        runMapCoreSafely(() => createData = new MapCore.IMcMapViewport.SCreateData(MapCore.IMcMapCamera.EMapType[eMapType] as any), "addJsonViewportService.display => IMcMapViewport.SCreateData", false)
        createData.pDevice = MapCoreData.device;
        createData.pCoordinateSystem = this.createGridCoordinateSystem(jsonData.MapViewport.GridCoordinateSystem) as any;
        MapCoreService.addOnlyNewCoordinateSystemToList(createData.pCoordinateSystem);
        if (createData.pCoordinateSystem == null) {
            throw new Error("jsonData.MapViewport.GridCoordinateSystem return null coordinate sysem");
        }

        createData.uViewportID = jsonData.MapViewport.ViewportID;
        createData.hWnd = currCanvas;
        currCanvas.width = jsonData.MapViewport.ViewportSize.ViewportWidth;
        currCanvas.height = jsonData.MapViewport.ViewportSize.ViewportHeight;
        createData.bShowGeoInMetricProportion = jsonData.MapViewport.ViewportShowGeoInMetricProportion;

        if (jsonData.MapViewport.Terrains != null) {
            jsonFilePath && this.sliceFolder(jsonFilePath);
            const terrainArr: MapCore.IMcMapTerrain[] = await this.openTerrain(jsonVp, jsonData)
            let overlayManagerCoordSys: MapCore.IMcGridCoordSystemGeographic;
            if (jsonData.OverlayManager.GridCoordinateSystem.Datum == null) {
                this.overlayManager = null;
            }
            else {
                overlayManagerCoordSys = this.createGridCoordinateSystem(jsonData.OverlayManager.GridCoordinateSystem)
                MapCoreService.addOnlyNewCoordinateSystemToList(overlayManagerCoordSys);
                if (overlayManagerCoordSys == null) {
                    throw new Error("jsonData.OverlayManager.GridCoordinateSystem return null coordinate sysem");
                }
                runMapCoreSafely(() => this.overlayManager = MapCore.IMcOverlayManager.Create(overlayManagerCoordSys), "addJsonViewportService.display => IMcOverlayManager.Create", false)
                runMapCoreSafely(() => this.overlayManager.AddRef(), "addJsonViewportService.display => overlayManager.AddRef", false)
                await this.createOverlay(jsonVp);
            }
            createData.pOverlayManager = this.overlayManager;
            let querySecondaryDtmLayers: MapCore.IMcDtmMapLayer[] = await this.getQuerySecondaryDtmLayersFromJson(jsonVp, jsonData);
            const viewportData: ViewportData = new ViewportData(null, jsonVp.id, null, currCanvas);
            if (!jsonData.MapViewport.IsSectionMapViewport)
                this.viewport = ViewportService.createViewport(createData, terrainArr, viewportData, querySecondaryDtmLayers);
            else {
                this.viewport = MapCore.IMcSectionMapViewport.CreateSection(
                    null,
                    createData,
                    terrainArr,
                    jsonData.MapViewport.SectionMapViewportData.SectionRoutePoints,
                    jsonData.MapViewport.SectionMapViewportData.SectionHeightPoints.Value);
            }
            viewportData.viewport = this.viewport;
            viewportData.mapType = this.viewport.GetMapType();
            MapCoreData.viewportsData.push(viewportData);
            this.waitForLayersInitializing();

            if (jsonData.MapViewport.MapTerrainsData != null && jsonData.MapViewport.MapTerrainsData.length > 0) {
                for (let i = 0; i < terrainArr.length; i++) {
                    if (jsonData.MapViewport.MapTerrainsData[i].Key == i) {
                        let mapTerrainData = jsonData.MapViewport.MapTerrainsData[i].Value;
                        runMapCoreSafely(() => this.viewport.SetTerrainDrawPriority(terrainArr[i], mapTerrainData.DrawPriority), "addJsonViewportService.display => IMcMapViewport.SetTerrainDrawPriority", false)
                        let x = this.viewport.GetTerrainDrawPriority(terrainArr[i]);
                        runMapCoreSafely(() => this.viewport.SetTerrainNumCacheTiles(terrainArr[i], false, mapTerrainData.NumCacheTiles), "addJsonViewportService.display => IMcMapViewport.SetTerrainNumCacheTiles", false)
                        let f = this.viewport.GetTerrainNumCacheTiles(terrainArr[i], false);
                        runMapCoreSafely(() => this.viewport.SetTerrainNumCacheTiles(terrainArr[i], true, mapTerrainData.NumCacheTilesForStaticObjects), "addJsonViewportService.display => IMcMapViewport.SetTerrainNumCacheTiles", false)
                        let y = this.viewport.GetTerrainNumCacheTiles(terrainArr[i], true);
                    }
                }
            }
        }
        store.dispatch(resizeViewport({ width: jsonData.MapViewport.ViewportSize.ViewportWidth, height: jsonData.MapViewport.ViewportSize.ViewportHeight }))
    }
    public async addNewViewportFromJson(jsonVp: JsonViewportWindow, canvas: HTMLCanvasElement) {
        let jsonFilePath: string = jsonVp.jsonFilePath;
        let printViewportPath: string = jsonVp.printViewportPath;
        let jsonDataBuffer: { fileName: string, fileBuffer: Uint8Array }[] = jsonVp.jsonDataBuffer;

        let jsonData: any
        if (jsonDataBuffer) {//json data imported by upload
            let jsonBuffer = jsonDataBuffer.find(f => f.fileName == 'FullViewportDataParams_GLES_Master.json').fileBuffer;
            let decoder = new TextDecoder();
            let stringJsonBuffer = decoder.decode(jsonBuffer)
            jsonData = JSON.parse(stringJsonBuffer);
        }
        else {//json data imports by url/fetch
            let response = await fetch(jsonFilePath!)
            let responseJson;
            if (response.status === 200) {
                responseJson = await response.json();
                jsonData = responseJson;
            }
            this.printViewportPath = printViewportPath;
        }
        await this.display(jsonVp, jsonData, canvas)
    }
    private sliceFolder = (urlJsonFile: string) => {
        this.folderName = urlJsonFile.slice(
            urlJsonFile.indexOf('/') + 1,
            urlJsonFile.lastIndexOf('/')
        )
    }
    private getFileBufferFromJson = async (jsonVp: JsonViewportWindow, fileName: string) => {
        let fileBuffer: Uint8Array = null;
        if (jsonVp.jsonDataBuffer) {
            fileBuffer = jsonVp.jsonDataBuffer.find(bufName => bufName.fileName == fileName).fileBuffer;
        }
        else {
            let uri: string = `./${this.folderName}/${fileName}`;
            const response: Response = await fetch(uri);
            const arrayBuffer: ArrayBuffer = response.ok ? await response.arrayBuffer() : null
            fileBuffer = new Uint8Array(arrayBuffer);
        }
        return fileBuffer;
    }
    private createOverlay = async (jsonVp: JsonViewportWindow) => {
        if (this.jsonData.OverlayManager.Overlays != null) {
            let arrayBuffer: Uint8Array = await this.getFileBufferFromJson(jsonVp, this.jsonData.OverlayManager.Overlays[0])
            let uri = `./${this.folderName}/${this.jsonData.OverlayManager.Overlays[0]}`;
            // Load the file butes into a scheme and start edit it
            if (arrayBuffer != null) {
                let numOverlays: number = this.jsonData.OverlayManager.Overlays?.Count || 1;
                if (numOverlays > 0) {
                    for (let i: number = 0; i < numOverlays; i++) {
                        runMapCoreSafely(() => this.overlay = MapCore.IMcOverlay.Create(this.overlayManager), 'MapCore.IMcOverlay.Create', false);
                        runMapCoreSafely(() => this.overlay.AddRef(), 'createOverlay=> IMcOverlay.AddRef', false);
                        runMapCoreSafely(() => this.overlay.SetDrawPriority(100), 'IMcOverlay.SetDrawPriority', false);
                        runMapCoreSafely(() => this.overlay.LoadObjects(arrayBuffer), 'IMcOverlay.LoadObjects', false);
                    }
                }
                else {
                    runMapCoreSafely(() => this.overlay = MapCore.IMcOverlay.Create(this.overlayManager), 'MapCore.IMcOverlay.Create', false);
                    runMapCoreSafely(() => this.overlay.AddRef(), 'IMcOverlay.AddRef', false);

                }
                MapCoreData.overlayManagerArr.push(new OverlayManager(this.overlayManager, this.overlay))
            }
            else if (!jsonVp.jsonDataBuffer) {
                alert(`Fetch error - (${uri})`)
            }

        }
    }
    private openTerrain = async (jsonVp: JsonViewportWindow, jsonData: any) => {
        let terrainArr: MapCore.IMcMapTerrain[] = [];
        let terrainBuffer: Uint8Array = await this.getFileBufferFromJson(jsonVp, jsonData.MapViewport.Terrains);
        let numTerrains: number = jsonData.MapViewport.Terrains?.Count || 1;
        if (numTerrains > 0) {
            for (let i: number = 0; i < numTerrains; i++) {
                let mapsUrlPrefix: string = "http:MapsToJson"
                if (jsonData.MapsBaseDirectory != null)
                    mapsUrlPrefix = jsonData.MapsBaseDirectory.Web;
                runMapCoreSafely(() => terrainArr[i] = MapCore.IMcMapTerrain.Load(terrainBuffer, mapsUrlPrefix, null), "addJsonViewportService.openTerrain => IMcMapTerrain.Load", false)
                runMapCoreSafely(() => terrainArr[i].AddRef(), "addJsonViewportService.openTerrain => IMcMapTerrain.AddRef", false)
            }
        }
        return terrainArr;
    }
    private getQuerySecondaryDtmLayersFromJson = async (jsonVp: JsonViewportWindow, jsonData: any) => {
        let querySecondaryDtmLayers = [];
        if (jsonData.MapViewport.QuerySecondaryDTMLayers) {
            let mapsUrlPrefix: string = "http:MapsToJson"
            if (jsonData.MapsBaseDirectory != null)
                mapsUrlPrefix = jsonData.MapsBaseDirectory.Web;

            for (let i = 0; i < jsonData.MapViewport.QuerySecondaryDTMLayers.length; i++) {
                const dtmLayer = jsonData.MapViewport.QuerySecondaryDTMLayers[i];
                let dtmLayerBuffer: Uint8Array = await this.getFileBufferFromJson(jsonVp, dtmLayer);
                let mcLayer: MapCore.IMcMapLayer
                runMapCoreSafely(() => mcLayer = MapCore.IMcMapLayer.Load(dtmLayerBuffer, mapsUrlPrefix, null), "addJsonViewportService.getQuerySecondaryDtmLayersFromJson => IMcMapLayer.Load", false)
                querySecondaryDtmLayers.push(mcLayer as MapCore.IMcDtmMapLayer)
            }
        }
        return querySecondaryDtmLayers;
    }
    private createGridCoordinateSystem = (gridCoordinateSystem: GridCrsType) => {
        let gridCoordSys: string = gridCoordinateSystem.GridCoordinateSystemType;
        let datum: string = gridCoordinateSystem.Datum;
        let sliceDatum: string = datum.slice(1);
        let CoordSys;
        switch (gridCoordSys) {
            case "_EGCS_GEOGRAPHIC":
                runMapCoreSafely(() => CoordSys = MapCore.IMcGridCoordSystemGeographic.Create(MapCore.IMcGridCoordinateSystem.EDatumType[sliceDatum] as any), "addJsonViewportService.createGridCoordinateSystem => IMcGridCoordSystemGeographic.Create", false)
                return CoordSys
            case "_EGCS_UTM":
                runMapCoreSafely(() => CoordSys = MapCore.IMcGridUTM.Create(gridCoordinateSystem.Zone, MapCore.IMcGridCoordinateSystem.EDatumType[sliceDatum] as any), "addJsonViewportService.createGridCoordinateSystem => IMcGridUTM.Create", false)
                return CoordSys
            case "_EGCS_GENERIC_GRID":
                if (gridCoordinateSystem.EpsgCode != null && gridCoordinateSystem.EpsgCode != "") {
                    runMapCoreSafely(() => CoordSys = MapCore.IMcGridGeneric.Create(gridCoordinateSystem.EpsgCode), "addJsonViewportService.createGridCoordinateSystem => IMcGridUTM.Create", false)
                    return CoordSys
                }
                else
                    return null;
            case "_EGCS_NEW_ISRAEL":
                runMapCoreSafely(() => CoordSys = MapCore.IMcGridNewIsrael.Create(), "addJsonViewportService.createGridCoordinateSystem => IMcGridNewIsrael.Create", false)
                return CoordSys
            default:
                alert("Current viewport coordinate system not supported in load viewport Viewport coordinate system not supported");
                return null;
        }
    }

    private onLayersInitialized = () => {
        this.callImaeProcessing();
        this.callGridMap();
        this.callAfterViewport();
        localStorage.setItem('downloadPermission', 'granted')
        if (this.printViewportPath) {
            console.log('map initializing was finished. wait for last rendering for print the map..');
            runCodeSafely(() => {
                const waitForRenderFinished = setInterval(() => {

                    if (!this.viewport!.HasPendingUpdates()) {
                        clearInterval(waitForRenderFinished);
                        this.printViewport();
                        return;
                    }

                }, 10);
            }, "addJsonViewportService.onLayersInitialized")
            // Move objects if requested
            //  if (this.moveObjects) { this.DoMoveObjects(); }
        }

    }

    private setImageProcessingData = (mapViewport: MapCore.IMcMapViewport, layer: MapCore.IMcRasterMapLayer, imageProcessingData: any) => {
        runMapCoreSafely(() => mapViewport.SetFilterImageProcessing(layer, MapCore.IMcImageProcessing.EFilterProccessingOperation[filterProccessingOperation[imageProcessingData.Filter]]), "addJsonViewportService.setImageProcessingData=> IMcImageProcessing.SetFilterImageProcessing", false)
        if (imageProcessingData.CustomFilter != null) {
            runMapCoreSafely(() =>
                mapViewport.SetCustomFilter(layer, imageProcessingData.CustomFilter.FilterXsize,
                    imageProcessingData.CustomFilter.FilterYsize,
                    imageProcessingData.CustomFilter.Filters,
                    imageProcessingData.CustomFilter.Bias,
                    imageProcessingData.CustomFilter.Divider), "addJsonViewportService.setImageProcessingData => IMcImageProcessing.SetCustomFilter", false)
        }
        runMapCoreSafely(() => mapViewport.SetWhiteBalanceBrightness(layer, imageProcessingData.WhiteBalanceBrightnessR, imageProcessingData.WhiteBalanceBrightnessG, imageProcessingData.WhiteBalanceBrightnessB), "addJsonViewportService.setImageProcessingData => IMcImageProcessing.SetWhiteBalanceBrightness", false)
        runMapCoreSafely(() => mapViewport.SetEnableColorTableImageProcessing(layer, imageProcessingData.IsEnableColorTableImageProcessing), "addJsonViewportService.setImageProcessingData => IMcImageProcessing.SetEnableColorTableImageProcessing", false)
        for (let i: number = 0; i < 3; i++) {
            let Channel: number = imageProcessingData.ChannelDatas[i].Channel;
            runMapCoreSafely(() => mapViewport.SetUserColorValues(layer, MapCore.IMcImageProcessing.EColorChannel[colorChannel[Channel]], imageProcessingData.ChannelDatas[i].UserColorValues, imageProcessingData.ChannelDatas[i].UserColorValuesUse), "setImageProcessingData => IMcImageProcessing.SetUserColorValues", false)
            runMapCoreSafely(() => mapViewport.SetColorTableBrightness(layer, MapCore.IMcImageProcessing.EColorChannel[colorChannel[Channel]], imageProcessingData.ChannelDatas[i].Brightness), "addJsonViewportService.setImageProcessingData=> IMcImageProcessing.SetColorTableBrightness", false)
            runMapCoreSafely(() => mapViewport.SetContrast(layer, MapCore.IMcImageProcessing.EColorChannel[colorChannel[Channel]], imageProcessingData.ChannelDatas[i].Contrast), "addJsonViewportService.setImageProcessingData => IMcImageProcessing.SetContrast", false)
            runMapCoreSafely(() => mapViewport.SetNegative(layer, MapCore.IMcImageProcessing.EColorChannel[colorChannel[Channel]], imageProcessingData.ChannelDatas[i].Negative), "addJsonViewportService.setImageProcessingData => IMcImageProcessing.SetNegative", false)
            runMapCoreSafely(() => mapViewport.SetGamma(layer, MapCore.IMcImageProcessing.EColorChannel[colorChannel[Channel]], imageProcessingData.ChannelDatas[i].Gamma), "addJsonViewportService.setImageProcessingData => IMcImageProcessing.SetGamma", false)
            if (imageProcessingData.ChannelDatas[i].IsOriginalHistogramSet) {
                runMapCoreSafely(() => mapViewport.SetOriginalHistogram(layer, MapCore.IMcImageProcessing.EColorChannel[colorChannel[Channel]], imageProcessingData.ChannelDatas[i].OriginalHistogram), "addJsonViewportService.setImageProcessingData => IMcImageProcessing.SetOriginalHistogram", false)
            }
            runMapCoreSafely(() => mapViewport.SetHistogramNormalization(layer, MapCore.IMcImageProcessing.EColorChannel[colorChannel[Channel]],
                imageProcessingData.ChannelDatas[i].HistogramNormalizationUse,
                imageProcessingData.ChannelDatas[i].HistogramNormalizationMean,
                imageProcessingData.ChannelDatas[i].HistogramNormalizationStdev), "addJsonViewportService.setImageProcessingData => IMcImageProcessing.SetHistogramNormalization", false)
            if (imageProcessingData.ChannelDatas[i].VisibleAreaOriginalHistogram != undefined) {
                runMapCoreSafely(() => mapViewport.SetVisibleAreaOriginalHistogram(layer, MapCore.IMcImageProcessing.EColorChannel[colorChannel[Channel]],
                    imageProcessingData.ChannelDatas[i].VisibleAreaOriginalHistogram), "addJsonViewportService.setImageProcessingData => IMcImageProcessing.SetVisibleAreaOriginalHistogram", false)
            }
            runMapCoreSafely(() => mapViewport.SetHistogramEqualization(layer, MapCore.IMcImageProcessing.EColorChannel[colorChannel[Channel]], imageProcessingData.ChannelDatas[i].HistogramEqualization), "addJsonViewportService.setImageProcessingData => IMcImageProcessing.SetHistogramEqualization", false)
            runMapCoreSafely(() => mapViewport.SetHistogramFit(layer, MapCore.IMcImageProcessing.EColorChannel[colorChannel[Channel]],
                imageProcessingData.ChannelDatas[i].ReferenceHistogramUse, imageProcessingData.ChannelDatas[i].ReferenceHistogram), "addJsonViewportService.setImageProcessingData => IMcImageProcessing.SetHistogramFit", false)
        }
    }
    private callAfterViewport = () => {
        let x: number = this.jsonData.MapViewport.CameraData.CameraPosition.X;
        let y: number = this.jsonData.MapViewport.CameraData.CameraPosition.Y;
        let z: number = this.jsonData.MapViewport.CameraData.CameraPosition.Z;

        let objPosition: MapCore.SMcVector3D = new MapCore.SMcVector3D(x, y, z);
        runMapCoreSafely(() => this.viewport.SetCameraPosition(objPosition, false),
            "addJsonViewportService.SetCameraPosition => IMcMapCamera.SetCameraScale", false)

        if (MapCore.IMcMapCamera.EMapType[this.jsonData.MapViewport.ViewportMapType.slice(1)] as any == MapCore.IMcMapCamera.EMapType.EMT_2D) {
            runMapCoreSafely(() => this.viewport.SetCameraScale(this.jsonData.MapViewport.CameraData.CameraScale), "addJsonViewportService.callAfterViewport => IMcMapCamera.SetCameraScale", false)
        }
        else {
            runMapCoreSafely(() => this.viewport.SetCameraClipDistances(this.jsonData.MapViewport.CameraData.CameraClipDistances.Min,
                this.jsonData.MapViewport.CameraData.CameraClipDistances.Max,
                this.jsonData.MapViewport.CameraData.CameraClipDistances.RenderInTwoSessions), "addJsonViewportService.callAfterViewport => IMcMapCamera.SetCameraClipDistances", false)
            runMapCoreSafely(() => this.viewport!.SetCameraFieldOfView(this.jsonData.MapViewport.CameraData.CameraFieldOfView), "addJsonViewportService.callAfterViewport => IMcMapCamera.SetCameraFieldOfView", false)
        }
        let yaw: number = this.jsonData.MapViewport.CameraData.CameraOrientation.Yaw;
        let pitch: number = this.jsonData.MapViewport.CameraData.CameraOrientation.Pitch;
        let roll: number = this.jsonData.MapViewport.CameraData.CameraOrientation.Roll;
        runMapCoreSafely(() => this.viewport!.SetCameraOrientation(yaw, pitch, roll, false), "addJsonViewportService.callAfterViewport => IMcMapCamera.SetCameraOrientation", false)
        runMapCoreSafely(() => this.viewport!.SetTransparencyOrderingMode(this.jsonData.MapViewport.TransparencyOrderingMode), "addJsonViewportService.callAfterViewport => IMcMapViewport.SetTransparencyOrderingMode", false)
        runMapCoreSafely(() => this.viewport!.SetOneBitAlphaMode(this.jsonData.MapViewport.OneBitAlphaMode), "addJsonViewportService.callAfterViewport => IMcMapViewport.SetOneBitAlphaMode", false)
        runMapCoreSafely(() => this.viewport!.SetShadowMode(this.jsonData.MapViewport.ShadowMode), "addJsonViewportService.callAfterViewport => IMcMapViewport.SetShadowMode", false)
        if (this.jsonData.MapViewport?.PostProcess != null && this.jsonData.MapViewport?.PostProcess.length) {
            this.jsonData.MapViewport.PostProcess.foreach((postProcessName: string) => {
                runMapCoreSafely(() => this.viewport!.AddPostProcess(postProcessName), "addJsonViewportService.callAfterViewport => IMcMapViewport.AddPostProcess", false)
            })
        }
        if (this.jsonData.MapViewport.DebugOptions != null && this.jsonData.MapViewport.DebugOptions?.Count > 0) {
            this.jsonData.MapViewport.DebugOptions.foreach((debugOption: DebugOption) => {
                runMapCoreSafely(() => this.viewport.SetDebugOption(debugOption?.Key, debugOption?.Value), "addJsonViewportService.callAfterViewport => IMcSpatialQueries.SetDebugOption", false)
            })
        }
        if (this.jsonData.MapViewport.MaterialSchemeName != null && this.jsonData.MapViewport.MaterialSchemeName != "") {
            runMapCoreSafely(() => this.viewport.SetMaterialScheme(this.jsonData.MapViewport.MaterialSchemeName), "addJsonViewportService.callAfterViewport => IMcMapViewport.SetMaterialScheme", false)
        }
        if (this.jsonData.MapViewport.MaterialSchemeDefinition != null && this.jsonData.MapViewport.MaterialSchemeDefinition?.Count > 0) {
            this.jsonData.MapViewport.MaterialSchemeDefinition.foreach((materialSchemeDefinition: MaterialSchemeDefinition) => {
                runMapCoreSafely(() => this.viewport.SetMaterialSchemeDefinition(materialSchemeDefinition.Key, materialSchemeDefinition.Value), "addJsonViewportService.callAfterViewport => IMcMapViewport.SetMaterialSchemeDefinition", false)
            })
        }
        runMapCoreSafely(() => this.viewport.SetDtmVisualization(this.jsonData.MapViewport.DtmVisualization.IsEnabled, this.jsonData.MapViewport.DtmVisualization.Params), "addJsonViewportService.callAfterViewport => IMcMapViewport.SetDtmVisualization", false)
        runMapCoreSafely(() => this.viewport.GetDtmVisualization(), "addJsonViewportService.callAfterViewport => IMcMapViewport.GetDtmVisualization", false)
        if (this.jsonData.MapViewport.HeightLines.ScaleSteps.length > 0) {
            let mapHeightLines: MapCore.IMcMapHeightLines = MapCore.IMcMapHeightLines.Create(this.jsonData.MapViewport.HeightLines.ScaleSteps, this.jsonData.MapViewport.HeightLines.LineWidth);
            runMapCoreSafely(() => mapHeightLines.SetColorInterpolationMode(this.jsonData.MapViewport.HeightLines.ColorInterpolationMode.IsEnabled,
                this.jsonData.MapViewport.HeightLines.ColorInterpolationMode.MinHeight,
                this.jsonData.MapViewport.HeightLines.ColorInterpolationMode.MaxHeight), "addJsonViewportService.callAfterViewport => IMcMapHeightLines.SetColorInterpolationMode", false)
            runMapCoreSafely(() => this.viewport.SetHeightLines(mapHeightLines), "addJsonViewportService.callAfterViewport => IMcMapViewport.SetHeightLines ", false)
            runMapCoreSafely(() => this.viewport.SetHeightLinesVisibility(true), "addJsonViewportService.callAfterViewport => IMcMapViewport.SetHeightLinesVisibility", false)
        }
    }
    private callImaeProcessing = () => {
        if (this.jsonData.MapViewport.ViewportImageProccessingData != null)
            this.setImageProcessingData(this.viewport, null, this.jsonData.MapViewport.ViewportImageProccessingData);
        if (this.jsonData.MapViewport.ImageProccessingData != null) {
            let imageProcessing = this.jsonData.MapViewport.ImageProccessingData;
            for (let k: number = 0; k < imageProcessing.length; k++) {
                let mapTerrains: MapCore.IMcMapTerrain[];
                let mapLayers: MapCore.IMcMapLayer[]
                runMapCoreSafely(() => mapTerrains = this.viewport!.GetTerrains(), "addJsonViewportService.callImaeProcessing => IMcSpatialQueries.GetTerrains", false)
                let terrain: MapCore.IMcMapTerrain = mapTerrains[imageProcessing[k].Key.key];
                runMapCoreSafely(() => mapLayers = terrain.GetLayers(), "addJsonViewportService.callImaeProcessing => IMcMapTerrain.GetLayers", false)
                let layer: MapCore.IMcMapLayer = mapLayers[imageProcessing[k].Key.value]
                if (eval("layer instanceof MapCore.IMcRasterMapLayer"))
                    this.setImageProcessingData(this.viewport, layer as MapCore.IMcRasterMapLayer, imageProcessing[k].Value);
            }
        }
    }
    private callGridMap = () => {
        if (this.jsonData.MapViewport.MapGrid != null) {
            runMapCoreSafely(() => this.viewport.SetGridAboveVectorLayers(this.jsonData.MapViewport.MapGrid.GridAboveVectorLayers), "addJsonViewportService.callGridMap => IMcMapViewport.SetGridAboveVectorLayers", false)
            runMapCoreSafely(() => this.viewport.SetGridVisibility(this.jsonData.MapViewport.MapGrid.GridVisibility), "addJsonViewportService.callGridMap => IMcMapViewport.SetGridVisibility", false)
            let dicLineItemsNames: any = {};
            let dicTextItemsNames: any = {};
            let mcGridRegions: MapCore.IMcMapGrid.SGridRegion[] = [];
            if (this.jsonData.MapViewport.MapGrid.GridRegion != null && this.jsonData.MapViewport.MapGrid.GridRegion.Count > 0) {
                let i = 0;
                for (let mctGridRegion of this.jsonData.MapViewport.MapGrid.GridRegion) {
                    {
                        let mcGridRegion: MapCore.IMcMapGrid.SGridRegion;
                        mcGridRegion.pCoordinateSystem = this.createGridCoordinateSystem(mctGridRegion.pCoordinateSystem);
                        MapCoreService.addOnlyNewCoordinateSystemToList(mcGridRegion.pCoordinateSystem);
                        mcGridRegion.GeoLimit = mctGridRegion.GeoLimit;
                        mcGridRegion.uFirstScaleStepIndex = mctGridRegion.uFirstScaleStepIndex;
                        mcGridRegion.uLastScaleStepIndex = mctGridRegion.uLastScaleStepIndex;
                        if (mctGridRegion.pGridLine != null && mctGridRegion.pGridLine != "") {
                            if (Object.keys(dicLineItemsNames).includes(mctGridRegion.pGridLine)) {
                                mcGridRegion.pGridLine = dicLineItemsNames[mctGridRegion.pGridLine];
                            }
                            else {
                                let filePath: any = this.uri + "\\" + mctGridRegion.pGridLine;
                                let objectSchemes: MapCore.IMcObjectScheme[];
                                runMapCoreSafely(() => objectSchemes = this.overlayManager.LoadObjectSchemes(filePath), "addJsonViewportService.callGridMap => IMcOverlayManager.LoadObjectSchemes", false)
                                if (objectSchemes != null && objectSchemes.length > 0) {
                                    let items: MapCore.IMcObjectSchemeNode[]
                                    runMapCoreSafely(() => items = objectSchemes[0].GetNodes(MapCore.IMcObjectSchemeNode.ENodeKindFlags.ENKF_SYMBOLIC_ITEM), "addJsonViewportService.callGridMap => IMcObjectScheme.GetNodes", false)
                                    if (items != null && items.length > 0) {
                                        mcGridRegion.pGridLine = items[0] as any;
                                        dicLineItemsNames[mctGridRegion.pGridLine] = mcGridRegion.pGridLine;
                                    }
                                    runMapCoreSafely(() => objectSchemes[0].Release(), "addJsonViewportService.callGridMap => IMcBase.Release", false)
                                }
                            }
                        }
                        if (mctGridRegion.pGridText != null && mctGridRegion.pGridText != "") {
                            if (Object.keys(dicTextItemsNames).includes(mctGridRegion.pGridText)) {
                                mcGridRegion.pGridText = dicTextItemsNames[mctGridRegion.pGridText];
                            }
                            else {
                                let filePath: any = this.uri + "\\" + mctGridRegion.pGridText;
                                let objectSchemes: MapCore.IMcObjectScheme[];
                                runMapCoreSafely(() => objectSchemes = this.overlayManager.LoadObjectSchemes(filePath), "addJsonViewportService.callGridMap => IMcOverlayManager.LoadObjectSchemes", false)
                                if (objectSchemes != null && objectSchemes.length > 0) {
                                    let items: MapCore.IMcObjectSchemeNode[]
                                    runMapCoreSafely(() => items = objectSchemes[0].GetNodes(MapCore.IMcObjectSchemeNode.ENodeKindFlags.ENKF_SYMBOLIC_ITEM), "addJsonViewportService.callGridMap => IMcObjectScheme.GetNodes", false)
                                    if (items != null && items.length > 0) {
                                        mcGridRegion.pGridText = items[0] as any;
                                        dicTextItemsNames[mctGridRegion.pGridText] = mcGridRegion.pGridText;
                                    }
                                    runMapCoreSafely(() => objectSchemes[0].Release(), "addJsonViewportService.callGridMap => IMcBase.Release", false)
                                }
                            }
                        }
                        mcGridRegions[i] = mcGridRegion;
                        i++;
                    }
                }
                let mcMapGrid: MapCore.IMcMapGrid;
                let mcScaleSteps: MapCore.IMcMapGrid.SScaleStep[] = [];
                if (this.jsonData.MapViewport.MapGrid.ScaleStep != null && this.jsonData.MapViewport.MapGrid.ScaleStep.Count > 0) {
                    mcScaleSteps = this.jsonData.MapViewport.MapGrid.ScaleStep.ToArray();
                }
                if (mcGridRegions != null && mcScaleSteps != null) {
                    runMapCoreSafely(() => this.viewport.SetGrid(MapCore.IMcMapGrid.Create(mcGridRegions, mcScaleSteps)), "addJsonViewportService.callGridMap => IMcMapViewport.SetGrid", false)
                }
            }
        }
    }
    private getLayers(viewport: MapCore.IMcMapViewport): MapCore.IMcMapLayer[] {
        let terrains: MapCore.IMcMapTerrain[]
        runMapCoreSafely(() => terrains = viewport.GetTerrains(), "addJsonViewportService.getLayers => IMcSpatialQueries.GetTerrains", false)
        return terrains.length > 0
            ? terrains[0].GetLayers() : [];
    }
    private waitForLayersInitializing() {
        const interavl = setInterval(() => {
            runCodeSafely(() => {
                let layers: MapCore.IMcMapLayer[] = this.getLayers(this.viewport);
                let allLayersInitialized: boolean = !layers.find(layer => !layer.IsInitialized());
                if (!allLayersInitialized) {
                    return;
                }
                clearInterval(interavl);
                this.onLayersInitialized();
                generalService.calcSizeAndPositionCanvases();
            }, "addJsonViewportService.waitForLayersInitializing")
        }, 60);
    }
    // DoMoveObjects = () => {
    //     if (this.aObjects.length != 0) {
    //         let val: number = Math.round(Math.random() * aObjects.length);
    //         if (val >= aObjects.length) {
    //             val = aObjects.length - 1;
    //         }
    //         aObjects[val].objPosition.x += Math.random() * 20 - 10;
    //         aObjects[val].objPosition.y += Math.random() * 20 - 10;
    //         aObjects[val].theObject.UpdateLocationPoints([aObjects[val].objPosition]);
    //     }
    // }

    private printViewport = () => {
        let puWidth: any = {}, puHeight: any = {}, widthDimension: number, heightDimension: number;
        runMapCoreSafely(() => this.viewport!.GetViewportSize(puWidth, puHeight), "addJsonViewportService.printViewport => IMcMapViewport.GetViewportSize", false)
        widthDimension = puWidth.Value
        heightDimension = puHeight.Value
        let pixelformat: MapCore.IMcTexture.EPixelFormat;
        let pePixelFormat: any = {};
        runMapCoreSafely(() => this.viewport!.GetRenderToBufferNativePixelFormat(pePixelFormat), "addJsonViewportService.printViewport => IMcMapViewport.GetRenderToBufferNativePixelFormat", false)
        pixelformat = pePixelFormat.Value;
        let image = this.renderScreenRectToBufferImage(new MapCore.SMcRect(0, 0, widthDimension, heightDimension), pixelformat, widthDimension, heightDimension, widthDimension, this.viewport);

        if (this.printViewportPath.charAt(this.printViewportPath.length - 1) == '/') {
            this.printViewportPath = this.printViewportPath.substr(0, this.printViewportPath.length - 1);
        }
        let imageFileBuffer: Uint8Array;
        runMapCoreSafely(() => imageFileBuffer = image?.Save(this.printViewportPath.split('.').pop()!), "addJsonViewportService.printViewport= > IMcImage.Save", false)
        runMapCoreSafely(() => MapCore.IMcMapDevice.DownloadBufferAsFile(imageFileBuffer, this.printViewportPath), "addJsonViewportService.printViewport => IMcMapDevice.DownloadBufferAsFiler", false)
        const urlParams = new URLSearchParams(window.location.search);
        const printViewportPath = urlParams.get('printViewportPath');
        if (printViewportPath) generalService.createAnddownloadFile(`autorender_success_${this.printViewportPath}`);
    }

    public renderScreenRectToBufferImage = (rect: MapCore.SMcRect, viewportPixelFormat: MapCore.IMcTexture.EPixelFormat, widthDimension: number,
        heightDimension: number, bufferRowPitch: number, viewport: MapCore.IMcMapViewport) => {
        let pixelBuffer: Uint8Array;
        runMapCoreSafely(() => {
            pixelBuffer = viewport.RenderScreenRectToBuffer(rect, widthDimension, heightDimension, viewportPixelFormat, bufferRowPitch)
        }, "addJsonViewportService.renderScreenRectToBufferImage => IMcMapViewport.RenderScreenRectToBuffer", false)
        let image: MapCore.IMcImage;
        runMapCoreSafely(() => image = MapCore.IMcImage.Create(pixelBuffer, widthDimension, heightDimension, viewportPixelFormat), "addJsonViewportService.renderScreenRectToBufferImage => IMcImage.Create", false)
        return image;
    }
}
export default new addJsonViewportService();
