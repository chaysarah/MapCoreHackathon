import { getEnumDetailsList, getEnumValueDetails, MapCoreData, OverlayManager, TypeToStringService } from "..";
import { runCodeLibrarySafely, runMapCoreSafely } from "./error-handler.service";
import GeneralCallbacksService from './generalCallbacks.service';

class MapCoreService {
    serverLayers: MapCore.IMcMapLayer.SServerLayerInfo[] = [];
    lastLayersValidityCheckTime: number = (new Date).getTime();
    lastMemUsageLogTime: number = (new Date).getTime();

    public initMapCore() {
        GeneralCallbacksService.initCallbackService();
    }

    public initDevice(deviceInitparams: MapCore.IMcMapDevice.SInitParams,) {
        if (MapCoreData.device == null) {
            const init = deviceInitparams;
            runMapCoreSafely(() => { MapCoreData.device = MapCore.IMcMapDevice.Create(init) }, "initDevice=> IMcMapDevice.Create", true)
            runMapCoreSafely(() => { MapCoreData.device.AddRef() }, "MapCoreService.initDevice=> IMcBase.AddRef", true)
            setTimeout(() => {
                runCodeLibrarySafely(() => {
                    requestAnimationFrame(this.RenderOrPerformPendingCalcultions)
                }, "MapCoreService.initDevice=> setTimeout");
            }, 100);
            setInterval(() => {
                runCodeLibrarySafely(() => {
                    runMapCoreSafely(() => { MapCore.IMcMapDevice.PerformPendingCalculations() }, "initDevice=> IMcMapDevice.PerformPendingCalculations", true)
                }, "MapCoreService.initDevice=> setInterval")
            }, 100);
            console.log('dispatch init device to true');
        }
    }
    public createTerrain(terrainLayers: MapCore.IMcMapLayer[], coordinateSystem: MapCore.IMcGridCoordinateSystem, displayItemsAttachedToStaticObjectsWithoutDtm: boolean): MapCore.IMcMapTerrain {
        let terrain: MapCore.IMcMapTerrain;
        runMapCoreSafely(() => {
            terrain = MapCore.IMcMapTerrain.Create(coordinateSystem, terrainLayers, null, null, displayItemsAttachedToStaticObjectsWithoutDtm)
        }, "MapCoreService.createTerrain => IMcMapTerrain.Create", true)
        runMapCoreSafely(() => {
            terrain.AddRef();
        }, "MapCoreService.createTerrain => IMcBase.AddRef", true)
        MapCoreData.standAloneTerrains.push(terrain)
        return terrain;
    }
    public createOverlayManager(coordinateSystem: MapCore.IMcGridCoordinateSystem): MapCore.IMcOverlayManager {
        let overlayManager: MapCore.IMcOverlayManager;
        runMapCoreSafely(() => {
            overlayManager = MapCore.IMcOverlayManager.Create(coordinateSystem);
            overlayManager.AddRef();
        }, "MapCoreService.createOverlayManager=> IMcOverlayManager.Create", true)
        MapCoreData.overlayManagerArr.push(new OverlayManager(overlayManager, null))
        return overlayManager;
    }
    public createOverlay(overlayManager: MapCore.IMcOverlayManager): MapCore.IMcOverlay {
        let overlay: MapCore.IMcOverlay
        runMapCoreSafely(() => {
            overlay = MapCore.IMcOverlay.Create(overlayManager);
            // overlay.AddRef();
        }, "MapCoreService.createOverlay=> IMcOverlay.Create", true)
        return overlay;
    }
    public releaseStandAloneTerrains = (terrains: MapCore.IMcMapTerrain[]) => {
        terrains.forEach((currentTerrain) => {
            let isTerrainExist = MapCoreData.standAloneTerrains.find(terrain => terrain == currentTerrain);
            if (isTerrainExist) {
                MapCoreData.standAloneTerrains = MapCoreData.standAloneTerrains.filter(terrain => terrain != currentTerrain)
                runMapCoreSafely(() => { currentTerrain.Release() }, "MapcoreService.releaseStandAloneTerrains => IMcBase.Release", true)
            }
        })
    }
    public loadServerLayers(afterLoadServerCB: (aLayers: MapCore.IMcMapLayer.SServerLayerInfo[]) => void, urlServer: string, serviceType: MapCore.IMcMapLayer.EWebMapServiceType, requestParams: MapCore.SMcKeyStringValue[]) {
        for (let index: number = 0; index < this.serverLayers.length; index++) {
            if (this.serverLayers[index].pCoordinateSystem)
                runMapCoreSafely(() => {
                    this.serverLayers[index].pCoordinateSystem.AddRef();
                }, "MapCoreService.loadServerLayers=>IMcBase.AddRef", true)
        }
        let AsyncOperationCallback = new MapCoreData.asyncOperationCallBacksClass(
            (eStatus: MapCore.IMcErrors.ECode, strServerURL: string, eWebMapServiceType: MapCore.IMcMapLayer.EWebMapServiceType,
                aLayers: MapCore.IMcMapLayer.SServerLayerInfo[], astrServiceMetadataURLs: string, strServiceProviderName: string) => {
                if (eStatus != MapCore.IMcErrors.ECode.SUCCESS) {
                    const enumDetails = getEnumValueDetails(eStatus, getEnumDetailsList(MapCore.IMcErrors.ECode));
                    alert(`Failed to load server layer. IAsyncOperationCallback.OnWebServerLayersResults estatus = ${enumDetails.name}`);
                }
                else {
                    this.ProcessWebServerLayersResults(strServerURL, astrServiceMetadataURLs)
                    for (let index: number = 0; index < aLayers.length; index++) {
                        if (aLayers[index].pCoordinateSystem)
                            runMapCoreSafely(() => { aLayers[index].pCoordinateSystem.AddRef(); }, "loadServerLayers=>IMcBase.AddRef", true)
                    }
                    this.serverLayers = aLayers;
                    afterLoadServerCB(aLayers);
                }
            }
        );
        runMapCoreSafely(() => {
            MapCore.IMcMapDevice.GetWebServerLayers(urlServer, serviceType, requestParams ? requestParams : [], AsyncOperationCallback)
        }, "MapCoreService.loadServerLayers=>IMcMapDevice.GetWebServerLayers", true)
    }
    public getStrServiceProviderName(resultCB: (providerName: string) => void, urlServer: string) {
        let AsyncOperationCallback = new MapCoreData.asyncOperationCallBacksClass(
            (eStatus: MapCore.IMcErrors.ECode, strServerURL: string, eWebMapServiceType: MapCore.IMcMapLayer.EWebMapServiceType,
                aLayers: MapCore.IMcMapLayer.SServerLayerInfo[], astrServiceMetadataURLs: string, strServiceProviderName: string) => {
                resultCB(strServiceProviderName);
            }
        );
        runMapCoreSafely(() => {
            MapCore.IMcMapDevice.GetWebServerLayers(urlServer, MapCore.IMcMapLayer.EWebMapServiceType.EWMS_MAPCORE, [], AsyncOperationCallback);
        }, "MapCoreService.getStrServiceProviderName=>IMcMapDevice.GetWebServerLayers", true)
    }
    ProcessWebServerLayersResults = (strServerURL: string, astrServiceMetadataURLs: string) => {
        let MapLayerServerURL = astrServiceMetadataURLs[0];
        if (MapLayerServerURL == null || MapLayerServerURL == "") {
            MapLayerServerURL = strServerURL;
        }
        let lastSlashIndex = MapLayerServerURL.lastIndexOf("?");
        if (lastSlashIndex < 0) {
            lastSlashIndex = MapLayerServerURL.lastIndexOf("/");
        }
        if (lastSlashIndex < 0) {
            return;
        }
        MapCoreData.astrServiceMetadataURLs = MapLayerServerURL.substring(0, lastSlashIndex);
        const { protocol, port, pathname, host } = window.location;
        if (!port && pathname && pathname !== '/') {
            const trimmedPathname = pathname.endsWith('/') ? pathname.substring(0, pathname.length - 1) : pathname;
            MapCoreData.astrServiceMetadataURLs = MapCoreData.astrServiceMetadataURLs.replace(host, host + trimmedPathname);
        }
        if (protocol === 'https:') {
            MapCoreData.astrServiceMetadataURLs = MapCoreData.astrServiceMetadataURLs.replace('http:', 'https:');
        }
        console.log(`astrServiceMetadataURLs:${astrServiceMetadataURLs}`);
    }
    //#endregion
    public resizeAllViewport() {
        for (let index: number = 0; index < MapCoreData.viewportsData.length; index++) {
            MapCoreData.viewportsData[index].viewport?.ViewportResized();
        }
    }
    private RenderOrPerformPendingCalcultions = () => {
        runCodeLibrarySafely(() => {
            let bRendered: boolean = false;
            // render viewports
            for (let i = 0; i < MapCoreData.viewportsData.length; ++i) {
                if (MapCoreData.viewportsData[i].viewport?.HasPendingUpdates()) {
                    runMapCoreSafely(() => {
                        MapCoreData.viewportsData[i].viewport.Render();
                    }, "MapCoreService.RenderOrPerformPendingCalcultions=> IMcMapViewport.Render", true)
                    bRendered = true;
                }
            }
            if (!bRendered) {
                runMapCoreSafely(() => {
                    MapCore.IMcMapDevice.PerformPendingCalculations();
                }, "MapCoreService.RenderOrPerformPendingCalcultions=>IMcMapDevice.PerformPendingCalculations", true)

            }
            let currtRenderTime: number = (new Date).getTime();
            const uMemUsageLoggingFrequency: number = 10;
            // log memory usage and heap size
            if (uMemUsageLoggingFrequency != 0 && currtRenderTime >= this.lastMemUsageLogTime + uMemUsageLoggingFrequency * 1000) {
                this.lastMemUsageLogTime = currtRenderTime;
            }
            if (currtRenderTime >= this.lastLayersValidityCheckTime + 15000) // 15 seconds
            {
                runMapCoreSafely(() => {
                    window.MapCore.IMcMapLayer.CheckAllNativeServerLayersValidityAsync();
                }, "MapCoreService.RenderOrPerformPendingCalcultions=>IMcMapLayer.CheckAllNativeServerLayersValidityAsync", true)

                this.lastLayersValidityCheckTime = currtRenderTime;
            }
            requestAnimationFrame(this.RenderOrPerformPendingCalcultions);
        }, "MapCoreService.RenderOrPerformPendingCalcultions");
    }

    public addCoordinateSystemToList(coordinateSystem: MapCore.IMcGridCoordinateSystem) {
        MapCoreData.coordinateSystemArr.push({ pCoordinateSystem: coordinateSystem, numInstancePointers: 1 })
        coordinateSystem.AddRef();
    }
    addOnlyNewCoordinateSystemToList(coordinateSystem: MapCore.IMcGridCoordinateSystem) {
        if (!this.isCoordinateSystemExist(coordinateSystem)) {
            this.addCoordinateSystemToList(coordinateSystem);
        }
        else {
            this.addPointerToCoordinateSystem(coordinateSystem);
        }
    }
    private isCoordinateSystemExist(coordinateSystem: MapCore.IMcGridCoordinateSystem) {
        let isFound = MapCoreData.coordinateSystemArr.find(cs => cs.pCoordinateSystem == coordinateSystem ||
            (coordinateSystem.IsEqual(cs.pCoordinateSystem) && cs.pCoordinateSystem.GetGridCoorSysType() == coordinateSystem.GetGridCoorSysType()));
        return isFound;
    }
    private addPointerToCoordinateSystem(coordinateSystem: MapCore.IMcGridCoordinateSystem) {
        let foundedMatchCsIndex = MapCoreData.coordinateSystemArr.findIndex(cs => cs.pCoordinateSystem == coordinateSystem ||
            (coordinateSystem.IsEqual(cs.pCoordinateSystem) && cs.pCoordinateSystem.GetGridCoorSysType() == coordinateSystem.GetGridCoorSysType()));
        MapCoreData.coordinateSystemArr[foundedMatchCsIndex].numInstancePointers++;
    }
    public getGridCoordinateSystems = () => {
        let sourceGridCSOptions = new Set<{ coordinateSystem: MapCore.IMcGridCoordinateSystem, name: string }>();
        MapCoreData.viewportsData.forEach((vp) => {
            let cs = vp.viewport.GetCoordinateSystem();
            let csType = TypeToStringService.convertNumberToGridString(cs.GetGridCoorSysType());
            sourceGridCSOptions.add({ coordinateSystem: cs, name: `${csType}` });
        });
        return Array.from(sourceGridCSOptions);
    }

}
export default new MapCoreService;