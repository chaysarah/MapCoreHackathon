class TypeToStringService {

    getSelectorTypeNameByTypeNumber(typeNumber: number) {
        switch (typeNumber) {
            case MapCore.IMcScaleConditionalSelector.CONDITIONAL_SELECTOR_TYPE:
                return 'Scale Conditional Selector';
            case MapCore.IMcViewportConditionalSelector.CONDITIONAL_SELECTOR_TYPE:
                return 'Viewport Conditional Selector';
            case MapCore.IMcBlockedConditionalSelector.CONDITIONAL_SELECTOR_TYPE:
                return 'Blocked Conditional Selector';
            case MapCore.IMcObjectStateConditionalSelector.CONDITIONAL_SELECTOR_TYPE:
                return 'Object State Conditional Selector';
            case MapCore.IMcLocationConditionalSelector.CONDITIONAL_SELECTOR_TYPE:
                return 'Location Conditional Selector';
            case MapCore.IMcBooleanConditionalSelector.CONDITIONAL_SELECTOR_TYPE:
                return 'Boolean Conditional Selector';
            default:
                break;
        }
    }
    getItemNodeTypeByTypeNumber(typeNumber: number) {
        let nodeTypeString = '';
        let items = ['IMcEmptySymbolicItem', 'IMcPictureItem', 'IMcTextItem', 'IMcManualGeometryItem', 'IMcArcItem',
            'IMcLineItem', 'IMcRectangleItem', 'IMcPolygonItem', 'IMcLineExpansionItem', 'IMcEllipseItem',
            'IMcArrowItem', 'IMcObjectLocation', 'IMcEmptyPhysicalItem', 'IMcParticleEffectItem', 'IMcMeshItem',
            'IMcProjectorItem', 'IMcSoundItem', 'IMcPointLightItem', 'IMcSpotLightItem', 'IMcDirectionalLightItem'];
        items.forEach(item => {
            // if (MapCore[item].NODE_TYPE == typeNumber) {
            //     nodeTypeString = item.substring(3, item.length - 4);
            // }
        });
        return `${nodeTypeString} Item`;
    }
    getLayerTypeByTypeNumber(layerType: number) {
        switch (layerType) {
            case MapCore.IMcNativeDtmMapLayer.LAYER_TYPE:
                return "Native Dtm Map Layer"
            case MapCore.IMcNativeRasterMapLayer.LAYER_TYPE:
                return "Native Raster Map Layer"
            case MapCore.IMcNativeVectorMapLayer.LAYER_TYPE:
                return "Native Vector Map Layer"
            case MapCore.IMcNativeHeatMapLayer.LAYER_TYPE:
                return "Native Heat Map Layer"
            case MapCore.IMcNativeMaterialMapLayer.LAYER_TYPE:
                return "Native Material Map Layer"
            case MapCore.IMcNativeTraversabilityMapLayer.LAYER_TYPE:
                return "Native Traversability Map Layer"
            case MapCore.IMcNative3DModelMapLayer.LAYER_TYPE:
                return "Native 3D Model Map Layer"
            case MapCore.IMcNativeVector3DExtrusionMapLayer.LAYER_TYPE:
                return "Native Vector 3D Extrusion Map Layer"

            case MapCore.IMcRaw3DModelMapLayer.LAYER_TYPE:
                return "Raw 3D Model Map Layer"
            case MapCore.IMcRawVectorMapLayer.LAYER_TYPE:
                return "Raw Vector Map Layer"
            case MapCore.IMcRawRasterMapLayer.LAYER_TYPE:
                return "Raw Raster Map Layer"
            case MapCore.IMcRawTraversabilityMapLayer.LAYER_TYPE:
                return "Raw Traversability Map Layer"
            case MapCore.IMcRawMaterialMapLayer.LAYER_TYPE:
                return "Raw Material Map Layer"
            case MapCore.IMcRawVector3DExtrusionMapLayer.LAYER_TYPE:
                return "Raw Vector 3D Extrusion Map Layer"
            case MapCore.IMcRawDtmMapLayer.LAYER_TYPE:
                return "Raw Dtm Map Layer"

            case MapCore.IMcNativeServer3DModelMapLayer.LAYER_TYPE:
                return "Native Server 3D Model Map Layer"
            case MapCore.IMcNativeServerTraversabilityMapLayer.LAYER_TYPE:
                return "Native Server Traversability Map Layer"
            case MapCore.IMcNativeServerMaterialMapLayer.LAYER_TYPE:
                return "Native Server Material Map Layer"
            case MapCore.IMcNativeServerDtmMapLayer.LAYER_TYPE:
                return "Native Server Dtm Map Layer"
            case MapCore.IMcNativeServerRasterMapLayer.LAYER_TYPE:
                return "Native Server Raster Map Layer"
            case MapCore.IMcNativeServerVector3DExtrusionMapLayer.LAYER_TYPE:
                return "Native Server Vector 3D Extrusion Map Layer"
            case MapCore.IMcNativeServerVectorMapLayer.LAYER_TYPE:
                return "Native Server Vector Map Layer"

            case MapCore.IMcWebServiceRasterMapLayer.LAYER_TYPE:
                return "Web Service Raster Map Layer"
            case MapCore.IMcWebServiceDtmMapLayer.LAYER_TYPE:
                return "Web Service Raster Map Layer"
        }
    }
    convertNumberToGridString(enumNumber: number, typeOnly?: boolean) {
        switch (enumNumber) {
            case MapCore.IMcGridCoordSystemGeographic.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'Geographic' : "GridCoordSystemGeographic"
            case MapCore.IMcGridCoordSystemGeocentric.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'Geocentric' : "GridCoordSystemGeocentric"
            case MapCore.IMcGridGeneric.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'Generic' : "GridGeneric"
            case MapCore.IMcGridTMUserDefined.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'TM User Defined' : "GridTMUserDefined"
            case MapCore.IMcGridUTM.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'UTM' : "GridUTM"
            case MapCore.IMcGridMGRS.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'MGRS' : "GridMGRS"
            case MapCore.IMcGridRSOSingapore.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'RSOSingapore' : "GridRSOSingapore"
            case MapCore.IMcGridGARS.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'GARS' : "GridGARS"
            case MapCore.IMcGridGEOREF.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'GEOREF' : "GridGEOREF"
            case MapCore.IMcGridNZMG.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'NZMG' : "GridNZMG"
            case MapCore.IMcGridBNG.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'BNG' : "GridBNG"
            case MapCore.IMcGridIrish.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'Irish' : "GridIrish"
            case MapCore.IMcGridNewIsrael.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'New Israel' : "GridNewIsrael"
            case MapCore.IMcGridS42.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'S42' : "GridS42"
            case MapCore.IMcGridRT90.GRID_COOR_SYS_TYPE:
                return typeOnly ? 'RT90' : "GridRT90"
        }
    }
}
export default new TypeToStringService;