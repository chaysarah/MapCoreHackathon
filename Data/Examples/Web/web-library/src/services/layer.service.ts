import _ from 'lodash';

import { MapCoreData, ViewportData } from "..";
import { LayerNameEnum, LayerSourceEnum } from "../model/enums";
import { LayerDetails } from "../model/layerDetails";
import { runMapCoreSafely } from "./error-handler.service";

class LayerService {

    getAllLayers() {
        let allLayers: MapCore.IMcMapLayer[] = [];
        let allViewportsLayers: MapCore.IMcMapLayer[] = [];
        MapCoreData.viewportsData.forEach((viewportData: ViewportData) => {
            if (viewportData.viewport) {
                let terrains = viewportData.viewport.GetTerrains();
                terrains.forEach((terrain) => {
                    let terrainLayers = terrain.GetLayers();
                    allViewportsLayers = [...allViewportsLayers, ...terrainLayers];
                })
            }
        })
        allLayers = [...allViewportsLayers, ...MapCoreData.standAloneLayers];
        let uniqueLayersArr = _.uniqWith(allLayers, (a: MapCore.IMcMapLayer, b: MapCore.IMcMapLayer) => a == b);
        return uniqueLayersArr;
    }
    releaseStandAloneLayers(layers: MapCore.IMcMapLayer[]) {
        layers.forEach((currentLayer) => {
            let isStandAloneExist = MapCoreData.standAloneLayers.find(p => p == currentLayer);
            if (isStandAloneExist) {
                MapCoreData.standAloneLayers = MapCoreData.standAloneLayers.filter(p => p != currentLayer);
                runMapCoreSafely(() => { isStandAloneExist.Release() }, "LayerService.releaseStandAloneLayers => IMcBase.Release", true)
            }
        })
    }
    createLayers(layersDetails: LayerDetails[]): MapCore.IMcMapLayer[] {
        let newLayer: MapCore.IMcMapLayer[] = [];
        layersDetails.forEach(layerDetails => {
            let layer: MapCore.IMcMapLayer;
            switch (layerDetails.layerSource) {
                case LayerSourceEnum.Native:
                    layer = this.createNativeLocalLayer(layerDetails);
                    break;
                case LayerSourceEnum.Raw:
                    layer = this.createRawLocalLayer(layerDetails);
                    break;
                case LayerSourceEnum.MAPCORE:
                    layer = this.createMapCoreServerLayer(layerDetails)
                    break;
                case LayerSourceEnum.WCS:
                    layer = this.createWCS_WMS_WMTS_ServerLayer(layerDetails.name, layerDetails.layerParams.WCS);
                    break;
                case LayerSourceEnum.WMS:
                    layer = this.createWCS_WMS_WMTS_ServerLayer(layerDetails.name, layerDetails.layerParams.WMS);
                    break;
                case LayerSourceEnum.WMTS:
                case LayerSourceEnum.CSW_WMTS:
                    layer = this.createWCS_WMS_WMTS_ServerLayer(layerDetails.name, layerDetails.layerParams.WMTS);
                    break;
                case LayerSourceEnum.CSW:
                    layer = this.createCSWServerLayer(layerDetails);
                    break;
            }
            MapCoreData.standAloneLayers.push(layer)
            layer.AddRef();
            newLayer.push(layer)
        })
        return newLayer;
    }
    createNativeLocalLayer(layer: LayerDetails): MapCore.IMcMapLayer {
        let l;
        switch (layer.name) {
            case LayerNameEnum.NativeRaster:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNativeRasterMapLayer.Create(layer.path, layer.layerParams.firstLowerQualityLevel, layer.layerParams.thereAreMissingFiles,
                        layer.layerParams.numLevelsToIgnore, layer.layerParams.enhanceBorder, layer.layerIReadCallback);
                }, "LayerService.createNativeLocalLayer=>IMcNativeRasterMapLayer.Create", true)
                break;
            case LayerNameEnum.NativeDtm:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNativeDtmMapLayer.Create(layer.path, layer.layerParams.numLevelsToIgnore, layer.layerIReadCallback);
                }, "LayerService.createNativeLocalLayer=>IMcNativeDtmMapLayer.Create", true)
                break;
            case LayerNameEnum.NativeVector:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNativeVectorMapLayer.Create(layer.path, layer.layerIReadCallback);
                }, "LayerService.createNativeLocalLayer=>IMcNativeVectorMapLayer.Create", true)
                break;
            case LayerNameEnum.NativeVector3DExtrrusion:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNativeVector3DExtrusionMapLayer.Create(layer.path, layer.layerParams.numLevelsToIgnore,
                        layer.layerParams.extrusionHeight, layer.layerIReadCallback);
                }, "LayerService.createNativeLocalLayer=>IMcNativeVector3DExtrusionMapLayer.Create", true)
                break;
            case LayerNameEnum.NativeHeat:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNativeHeatMapLayer.Create(layer.path, layer.layerParams.firstLowerQualityLevel, layer.layerParams.thereAreMissingFiles, layer.layerParams.numLevelsToIgnore, layer.layerIReadCallback, layer.layerParams.enhanceBorder);
                }, "LayerService.createNativeLocalLayer=>IMcNativeHeatMapLayer.Create", true)
                break;
            case LayerNameEnum.NativeTraversability:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNativeTraversabilityMapLayer.Create(layer.path, layer.layerParams.thereAreMissingFiles, layer.layerIReadCallback);
                }, "LayerService.createNativeLocalLayer=>IMcNativeTraversabilityMapLayer.Create", true)
                break;
            case LayerNameEnum.Native3DModel:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNative3DModelMapLayer.Create(layer.path, layer.layerParams.numLevelsToIgnore, layer.layerIReadCallback);
                }, "LayerService.createNativeLocalLayer=>IMcNative3DModelMapLayer.Create", true)
                break;
            case LayerNameEnum.NativeMaterial:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNativeMaterialMapLayer.Create(layer.path, layer.layerParams.thereAreMissingFiles, layer.layerIReadCallback);
                }, "LayerService.createNativeLocalLayer=>IMcNativeMaterialMapLayer.Create", true)
                break;
        }
        return l
    }
    createRawLocalLayer(layerDetails: LayerDetails): MapCore.IMcMapLayer {
        layerDetails.layerParams.rawParams.strDirectory = layerDetails.path;
        let l;
        switch (layerDetails.name) {
            case LayerNameEnum.RawRaster:
                runMapCoreSafely(() => {
                    l = MapCore.IMcRawRasterMapLayer.Create(layerDetails.layerParams.rawParams, layerDetails.layerParams.imageCoordinateSystem);
                }, "LayerService.createRawLocalLayer=>IMcRawRasterMapLayer.Create", true)
                break;
            case LayerNameEnum.RawDtm:
                runMapCoreSafely(() => {
                    l = MapCore.IMcRawDtmMapLayer.Create(layerDetails.layerParams.rawParams);
                }, "LayerService.createRawLocalLayer=>IMcRawDtmMapLayer.Create", true)
                break;
            case LayerNameEnum.RawVector:
                layerDetails.layerParams.rawVectorParams.strDataSource = layerDetails.path;
                // layerDetails.layerParams.rawVectorParams.pSourceCoordinateSystem = layerDetails.layerParams.targetCoordSys
                runMapCoreSafely(() => {
                    l = MapCore.IMcRawVectorMapLayer.Create(layerDetails.layerParams.rawVectorParams, layerDetails.layerParams.targetCoordSys, layerDetails.layerParams.rawParams.pTilingScheme, layerDetails.layerIReadCallback);
                }, "LayerService.createRawLocalLayer=>IMcRawVectorMapLayer.Create", true)
                break;
            case LayerNameEnum.RawVector3DExtrusion:
                runMapCoreSafely(() => {
                    let params: MapCore.IMcRawVector3DExtrusionMapLayer.SParams = layerDetails.layerParams.threeDExtrusionParams;
                    if (layerDetails.layerParams.indexingData == null) {
                        // Without index
                        l = MapCore.IMcRawVector3DExtrusionMapLayer.Create(params, layerDetails.layerParams.extrusionHeight, layerDetails.layerIReadCallback);
                    }
                    else {
                        if (layerDetails.layerParams.indexingData.strIndexingDataDirectory = "...") {
                            throw Error("Must upload an index file");
                        }
                        // With index
                        if (layerDetails.layerParams.indexingData.isDefaultIndex) {
                            // Default index 
                            l = MapCore.IMcRawVector3DExtrusionMapLayer.Create(layerDetails.path, params, layerDetails.layerParams.extrusionHeight, layerDetails.layerIReadCallback, "");
                        }
                        else {
                            //Non default index 
                            l = MapCore.IMcRawVector3DExtrusionMapLayer.Create(layerDetails.path, params, layerDetails.layerParams.extrusionHeight, layerDetails.layerIReadCallback, layerDetails.layerParams.indexingData.strIndexingDataDirectory);
                        }
                    }
                }, "LayerService.createRawLocalLayer=>IMcRawVector3DExtrusionMapLayer.Create", true)
                break;
            case LayerNameEnum.RawTraversability:
                runMapCoreSafely(() => {
                    l = MapCore.IMcRawTraversabilityMapLayer.Create(layerDetails.layerParams.rawParams);
                }, "LayerService.createRawLocalLayer=>IMcRawTraversabilityMapLayer.Create", true)
                break;
            case LayerNameEnum.Raw3DModel:
                runMapCoreSafely(() => {
                    if (layerDetails.layerParams.indexingData == null) {
                        // Without index
                        l = MapCore.IMcRaw3DModelMapLayer.Create(layerDetails.path, layerDetails.layerParams.targetCoordSys, layerDetails.layerParams.orthometricHeights,
                            layerDetails.layerParams.clipRect, layerDetails.layerParams.targetHighestResolution, layerDetails.layerIReadCallback, layerDetails.layerParams.requestParams);
                    }
                    else {
                        if (layerDetails.layerParams.indexingData.strIndexingDataDirectory == "...") {
                            throw Error("Must upload an index file");
                        }
                        // With index
                        if (layerDetails.layerParams.indexingData.isDefaultIndex) {
                            // Default index 
                            l = MapCore.IMcRaw3DModelMapLayer.Create(layerDetails.path, layerDetails.layerParams.orthometricHeights, layerDetails.layerParams.numLevelsToIgnore,
                                layerDetails.layerIReadCallback, "")
                        }
                        else {
                            //Non default index 
                            l = MapCore.IMcRaw3DModelMapLayer.Create(layerDetails.path, layerDetails.layerParams.orthometricHeights, layerDetails.layerParams.numLevelsToIgnore,
                                layerDetails.layerIReadCallback, layerDetails.layerParams.indexingData.strIndexingDataDirectory)
                        }
                    }
                }, "LayerService.createRawLocalLayer=>IMcRaw3DModelMapLayer.Create", true)
                break;
            case LayerNameEnum.RawMaterial:
                runMapCoreSafely(() => {
                    l = MapCore.IMcRawMaterialMapLayer.Create(layerDetails.layerParams.rawParams);
                }, "LayerService.createRawLocalLayer=>IMcRawMaterialMapLayer.Create", true)
                break;
        }
        return l
    }
    createMapCoreServerLayer(layerDetails: LayerDetails): MapCore.IMcMapLayer {
        let layerStr: string;
        if (!(layerDetails as any).layerParams.urlServer)
            layerStr = layerDetails.path;
        else
            layerStr = `${(layerDetails as any).layerParams.urlServer}/${layerDetails.path}`;
        let l;
        switch (layerDetails.name) {
            case LayerNameEnum.NativeServerRaster:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNativeServerRasterMapLayer.Create(layerStr, layerDetails.layerIReadCallback);
                }, "LayerService.createMapCoreServerLayer=>IMcNativeServerRasterMapLayer.Create", true)
                break;
            case LayerNameEnum.NativeServerDtm:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNativeServerDtmMapLayer.Create(layerStr, layerDetails.layerIReadCallback);
                }, "LayerService.createMapCoreServerLayer=>IMcNativeServerDtmMapLayer.Create", true)
                break;
            case LayerNameEnum.NativeServer3DModel:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNativeServer3DModelMapLayer.Create(layerStr, layerDetails.layerIReadCallback);
                }, "LayerService.createMapCoreServerLayer=>IMcNativeServer3DModelMapLayer.Create", true)
                break;
            case LayerNameEnum.NativeServerVector:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNativeServerVectorMapLayer.Create(layerStr, layerDetails.layerIReadCallback);
                }, "LayerService.createMapCoreServerLayer=>IMcNativeServerVectorMapLayer.Create", true)
                break;
            case LayerNameEnum.NativeServerVector3DExtrusion:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNativeServerVector3DExtrusionMapLayer.Create(layerStr, layerDetails.layerIReadCallback);
                }, "LayerService.createMapCoreServerLayer=>IMcNativeServerVector3DExtrusionMapLayer.Create", true)
                break;
            case LayerNameEnum.NativeServerTraversability:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNativeServerTraversabilityMapLayer.Create(layerStr, layerDetails.layerIReadCallback);
                }, "LayerService.createMapCoreServerLayer=>IMcNativeServerTraversabilityMapLayer.Create", true)
                break;
            case LayerNameEnum.NativeServerMaterial:
                runMapCoreSafely(() => {
                    l = MapCore.IMcNativeServerMaterialMapLayer.Create(layerStr, layerDetails.layerIReadCallback);
                }, "LayerService.createMapCoreServerLayer=>IMcNativeServerMaterialMapLayer.Create", true)
                break;
        }
        return l
    }
    createWCS_WMS_WMTS_ServerLayer(name: LayerNameEnum, serverParams: any): MapCore.IMcMapLayer {
        let l
        if (name == LayerNameEnum.NativeServerRaster || name == LayerNameEnum.WebServiceRaster)
            l = MapCore.IMcWebServiceRasterMapLayer.Create(serverParams);
        if (name == LayerNameEnum.NativeServerDtm || name == LayerNameEnum.WebServiceDTM)
            l = MapCore.IMcWebServiceDtmMapLayer.Create(serverParams);
        return l
    }
    createCSWServerLayer(layer: LayerDetails): MapCore.IMcMapLayer {
        let l
        if (layer.layerParams.serverLayerInfo.strLayerType == "RAW_3D_MODEL") {
            l = MapCore.IMcRaw3DModelMapLayer.Create(layer.path, layer.layerParams.CSW.pCoordinateSystem, layer.layerParams.orthometricHeights,
                layer.layerParams.CSW.BoundingBox, layer.layerParams.targetHighestResolution, layer.layerIReadCallback, layer.layerParams.CSW.aRequestParams);
        }
        return l
    }

}
export default new LayerService;