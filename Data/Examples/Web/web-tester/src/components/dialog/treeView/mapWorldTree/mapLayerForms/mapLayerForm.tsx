import { useSelector } from "react-redux";

import Raster, { RasterProperties, RasterPropertiesState } from "./rasterForms/raster";
import CodeLayer, { CodeLayerProperties, CodeLayerPropertiesState } from "./codeLayer";
import Vectorial, { VectorialProperties, VectorialPropertiesState } from "./vectorForms/vectorial";
import StaticObjects, { StaticObjectsProperties, StaticObjectsPropertiesState } from "./staticObjects";
import Model3D, { Model3DPropertiesState, Model3DProperties } from "./model3D";
import MapLayer, { MapLayerProperties, MapLayerPropertiesState } from "./mapLayer";
import TabsParentCtrl from "../../../shared/tabCtrls/tabsParentCtrl";
import { TabType } from "../../../shared/tabCtrls/tabModels";
import { AppState } from "../../../../../redux/combineReducer";
import { runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";

export default function MapLayerForm() {
    const cursorPos = useSelector((state: AppState) => state.mapWindowReducer.cursorPos);
    let currentLayer = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    let mapWorldTree = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);

    const getLayerTabs = () => {
        let tabs: TabType[] = [{ index: 0, header: 'MapLayer', statePropertiesClass: MapLayerPropertiesState, propertiesClass: MapLayerProperties, component: MapLayer }];
        let tabIndex = 1;
        let layerType: number = null;
        runMapCoreSafely(() => {
            layerType = currentLayer.nodeMcContent.GetLayerType();
        }, 'MapLayerForm.getLayerTab => IMcMapLayer.GetLayerType', true);
        switch (layerType) {
            //#region RASTER
            case MapCore.IMcRawRasterMapLayer.LAYER_TYPE:
                tabs = [...tabs, { index: tabIndex++, header: 'Raster', statePropertiesClass: RasterPropertiesState, propertiesClass: RasterProperties, component: Raster }];
                break;
            case MapCore.IMcRawTraversabilityMapLayer.LAYER_TYPE:
                tabs = [...tabs, { index: tabIndex++, header: 'Raster', statePropertiesClass: RasterPropertiesState, propertiesClass: RasterProperties, component: Raster },
                { index: tabIndex++, header: 'Code Map Layer', statePropertiesClass: CodeLayerPropertiesState, propertiesClass: CodeLayerProperties, component: CodeLayer },];
                break;
            case MapCore.IMcRawMaterialMapLayer.LAYER_TYPE:
                tabs = [...tabs, { index: tabIndex++, header: 'Raster', statePropertiesClass: RasterPropertiesState, propertiesClass: RasterProperties, component: Raster },
                { index: tabIndex++, header: 'Code Map Layer', statePropertiesClass: CodeLayerPropertiesState, propertiesClass: CodeLayerProperties, component: CodeLayer }
                ];
                break;
            case MapCore.IMcNativeRasterMapLayer.LAYER_TYPE://Checked
                tabs = [...tabs, { index: tabIndex++, header: 'Raster', statePropertiesClass: RasterPropertiesState, propertiesClass: RasterProperties, component: Raster }];
                break;
            case MapCore.IMcNativeMaterialMapLayer.LAYER_TYPE:
                tabs = [...tabs, { index: tabIndex++, header: 'Raster', statePropertiesClass: RasterPropertiesState, propertiesClass: RasterProperties, component: Raster },
                { index: tabIndex++, header: 'Code Map Layer', statePropertiesClass: CodeLayerPropertiesState, propertiesClass: CodeLayerProperties, component: CodeLayer }
                ];
                break;
            case MapCore.IMcNativeTraversabilityMapLayer.LAYER_TYPE://Checked
                tabs = [...tabs, { index: tabIndex++, header: 'Raster', statePropertiesClass: RasterPropertiesState, propertiesClass: RasterProperties, component: Raster },
                { index: tabIndex++, header: 'Code Map Layer', statePropertiesClass: CodeLayerPropertiesState, propertiesClass: CodeLayerProperties, component: CodeLayer }];
                break;
            case MapCore.IMcNativeServerTraversabilityMapLayer.LAYER_TYPE://Checked
                tabs = [...tabs, { index: tabIndex++, header: 'Raster', statePropertiesClass: RasterPropertiesState, propertiesClass: RasterProperties, component: Raster },
                { index: tabIndex++, header: 'Code Map Layer', statePropertiesClass: CodeLayerPropertiesState, propertiesClass: CodeLayerProperties, component: CodeLayer }];
                break;
            case MapCore.IMcNativeServerMaterialMapLayer.LAYER_TYPE:
                tabs = [...tabs, { index: tabIndex++, header: 'Raster', statePropertiesClass: RasterPropertiesState, propertiesClass: RasterProperties, component: Raster },
                { index: tabIndex++, header: 'Code Map Layer', statePropertiesClass: CodeLayerPropertiesState, propertiesClass: CodeLayerProperties, component: CodeLayer }];
                break;
            case MapCore.IMcNativeServerRasterMapLayer.LAYER_TYPE:
                tabs = [...tabs, { index: tabIndex++, header: 'Raster', statePropertiesClass: RasterPropertiesState, propertiesClass: RasterProperties, component: Raster }];
                break;
            case MapCore.IMcWebServiceRasterMapLayer.LAYER_TYPE:
                tabs = [...tabs, { index: tabIndex++, header: 'Raster', statePropertiesClass: RasterPropertiesState, propertiesClass: RasterProperties, component: Raster }];
                break;
            //#endregion
            //#region VECTOR
            case MapCore.IMcNativeVectorMapLayer.LAYER_TYPE:
                // tabs = [...tabs, { index: tabIndex++, header: 'Vectorial', statePropertiesClass: VectorialPropertiesState, propertiesClass: VectorialProperties, component: Vectorial }
                // ];
                break;
            case MapCore.IMcRawVectorMapLayer.LAYER_TYPE:
                // tabs = [];
                break;
            case MapCore.IMcNativeServerVectorMapLayer.LAYER_TYPE:
                // tabs = [];
                break;
            //#endregion
            //#region 3D Model
            case MapCore.IMcNative3DModelMapLayer.LAYER_TYPE:
                tabs = [...tabs,
                { index: tabIndex++, header: 'Static Objects', statePropertiesClass: StaticObjectsPropertiesState, propertiesClass: StaticObjectsProperties, component: StaticObjects },
                { index: tabIndex++, header: '3D Model', statePropertiesClass: Model3DPropertiesState, propertiesClass: Model3DProperties, component: Model3D },
                ];
                break;
            case MapCore.IMcRaw3DModelMapLayer.LAYER_TYPE:
                tabs = [...tabs,
                { index: tabIndex++, header: 'Static Objects', statePropertiesClass: StaticObjectsPropertiesState, propertiesClass: StaticObjectsProperties, component: StaticObjects },
                { index: tabIndex++, header: '3D Model', statePropertiesClass: Model3DPropertiesState, propertiesClass: Model3DProperties, component: Model3D },
                ];
                break;
            case MapCore.IMcNativeServer3DModelMapLayer.LAYER_TYPE:
                tabs = [...tabs,
                { index: tabIndex++, header: 'Static Objects', statePropertiesClass: StaticObjectsPropertiesState, propertiesClass: StaticObjectsProperties, component: StaticObjects },
                { index: tabIndex++, header: '3D Model', statePropertiesClass: Model3DPropertiesState, propertiesClass: Model3DProperties, component: Model3D },
                ];
                break;
            //#endregion
            //#region Vector 3D Extrusion
            case MapCore.IMcNativeVector3DExtrusionMapLayer.LAYER_TYPE:
                tabs = [...tabs, { index: tabIndex++, header: 'Static Objects', statePropertiesClass: StaticObjectsPropertiesState, propertiesClass: StaticObjectsProperties, component: StaticObjects }];
                break;
            case MapCore.IMcRawVector3DExtrusionMapLayer.LAYER_TYPE:
                tabs = [...tabs, { index: tabIndex++, header: 'Static Objects', statePropertiesClass: StaticObjectsPropertiesState, propertiesClass: StaticObjectsProperties, component: StaticObjects }];
                break;
            case MapCore.IMcNativeServerVector3DExtrusionMapLayer.LAYER_TYPE:
                tabs = [...tabs, { index: tabIndex++, header: 'Static Objects', statePropertiesClass: StaticObjectsPropertiesState, propertiesClass: StaticObjectsProperties, component: StaticObjects }];
                break;
            //#endregion
            case MapCore.IMcNativeHeatMapLayer.LAYER_TYPE:
                // tabs = [];
                break;
            case MapCore.IMcNativeDtmMapLayer.LAYER_TYPE:
                // tabs = [];
                break;
            case MapCore.IMcRawDtmMapLayer.LAYER_TYPE:
                // tabs = [];
                break;
            case MapCore.IMcNativeServerDtmMapLayer.LAYER_TYPE:
                // tabs = [];
                break;
            case MapCore.IMcWebServiceDtmMapLayer.LAYER_TYPE:
                // tabs = [];
                break;
            default:
                break;
        }

        return tabs;
    }

    return <TabsParentCtrl
        parentName='MapLayerForm'
        tabTypes={getLayerTabs()}
        getDefaultFuncProps={{ currentLayer, cursorPos, mapWorldTree }}
        selectedNode={currentLayer}
    />
}

