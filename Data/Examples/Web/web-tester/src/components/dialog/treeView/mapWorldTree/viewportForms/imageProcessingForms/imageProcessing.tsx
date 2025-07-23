import { ListBox } from "primereact/listbox";
import { useSelector } from "react-redux";

import Filters, { FiltersProperties, FiltersPropertiesState } from "./filters";
import ColorTable, { ColorTableProperties, ColorTablePropertiesState } from "./colorTable";
import { MapViewportFormTabInfo } from "../mapViewportForm";
import { TabInfo, TabType } from "../../../../shared/tabCtrls/tabModels";
import { Properties } from "../../../../dialog";
import NestedTabsCtrl from "../../../../shared/tabCtrls/nestedTabsCtrl";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../../redux/combineReducer";
import dialogStateService from "../../../../../../services/dialogStateService";
import mapWorldTreeService from "../../../../../../services/mapWorldTreeService";

export class ImageProcessingPropertiesState implements Properties {
    static getDefault(p: any): ImageProcessingPropertiesState {
        return {
        }
    }
}
export class ImageProcessingProperties extends ImageProcessingPropertiesState {
    FiltersProperties: any;
    ColorTableProperties: any;
    viewportLayers: { label: string, layer: MapCore.IMcRasterMapLayer }[];
    selectedLayer: { label: string, layer: MapCore.IMcRasterMapLayer };

    static getDefault(p: any): ImageProcessingProperties {
        let stateDefaults = super.getDefault(p);
        let { currentViewport, cursorPos, mapWorldTree } = p;
        let mcCurrentViewport: MapCore.IMcMapViewport = currentViewport.nodeMcContent;

        let layersArr: { label: string, layer: MapCore.IMcRasterMapLayer }[] = []
        let terrains = mcCurrentViewport.GetTerrains();
        terrains.forEach(terrain => {
            let layers = terrain.GetLayers();
            layers.forEach(layer => {
                if ([MapCore.IMcNativeRasterMapLayer.LAYER_TYPE, MapCore.IMcNativeServerRasterMapLayer.LAYER_TYPE,
                MapCore.IMcRawRasterMapLayer.LAYER_TYPE, MapCore.IMcWebServiceRasterMapLayer.LAYER_TYPE].includes(layer.GetLayerType())) {
                    let layerNode = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, layer);
                    layersArr = [...layersArr, { label: layerNode.label, layer: layerNode.nodeMcContent as MapCore.IMcRasterMapLayer }]
                }
            });
        });

        let defaults: ImageProcessingProperties = {
            ...stateDefaults,
            FiltersProperties: { ...FiltersProperties.getDefault(p), selectedLayer: null },
            ColorTableProperties: { ...ColorTableProperties.getDefault({ ...p, viewportLayers: layersArr }), selectedLayer: null, viewportLayers: layersArr },
            viewportLayers: layersArr,
            selectedLayer: null,
        }
        return defaults;
    }
};

export default function ImageProcessing(props: { tabInfo: MapViewportFormTabInfo }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const tabTypes: TabType[] = [
        { index: 0, header: 'Filters', statePropertiesClass: FiltersPropertiesState, propertiesClass: FiltersProperties, component: Filters },
        { index: 1, header: 'Color Table', statePropertiesClass: ColorTablePropertiesState, propertiesClass: ColorTableProperties, component: ColorTable },
    ]
    //NOTE:
    //if MapViewportForm was implementes by parentTabsCtrl, need to send for lastTabInfo={props.tabInfo} and all this region is unnecessary!
    //#region NewTabInfo 
    const setTabApplyCallBack = (Callback: () => void) => {
        runCodeSafely(() => {
            props.tabInfo.setApplyCallBack('ImageProcessing', Callback)
        }, `MapViewportForm/ImageProcessing => setTabApplyCallBack`)
    }
    const setLocalPropertiesCallback = (key: { [key: string]: any } | string, value?: any) => {
        runCodeSafely(() => {
            if (key && typeof key == 'object') {
                Object.entries(key).forEach(([key, value]) => {
                    props.tabInfo.setPropertiesCallback('imageProcessingProperties', key, value)
                })
            }
            else {
                props.tabInfo.setPropertiesCallback('imageProcessingProperties', key as string, value)
            }
        }, `MapViewportForm/ImageProcessing => setLocalPropertiesCallback`)
    }
    const setCurrPropertiesCallback = (key: string, value: any) => {
        runCodeSafely(() => {
            dialogStateService.setDialogState(['ImageProcessingProperties', key].join('.'), value);
        }, `MapViewportForm/ImageProcessing => setCurrPropertiesCallback`)
    }
    const applyCurrPropertiesCallback = (pathesToApply?: string[]) => {
        runCodeSafely(() => {
            pathesToApply = pathesToApply?.map(path => `ImageProcessingProperties.${path}`);
            dialogStateService.applyDialogState(pathesToApply);
        }, `MapViewportForm/ImageProcessing => applyCurrPropertiesCallback`)
    }
    const saveData = (event: any) => {
        runCodeSafely(() => {
            setLocalPropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in new ImageProcessingPropertiesState) {
                setCurrPropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            }
        }, "MapViewportForm/ImageProcessing.saveData => onChange")
    }
    const lastTabInfo: TabInfo = {
        tabProperties: props.tabInfo.properties.imageProcessingProperties,
        setPropertiesCallback: setLocalPropertiesCallback,
        setCurrStatePropertiesCallback: setCurrPropertiesCallback,
        applyCurrStatePropertiesCallback: applyCurrPropertiesCallback,
        setApplyCallBack: setTabApplyCallBack,
        saveData: saveData,
        getTabPropertiesByTabPropertiesClass: (tabPropertiesClass: any) => { alert('Not Implemented!') },
        setSiblingProperty: (tabPropertiesClass: any, key: string, value: any) => { alert('tester function not implemented!') },
    }
    //#endregion

    const handleLayerChange = (e: any) => {
        runCodeSafely(() => {
            saveData(e);
            let filtersProps = { ...props.tabInfo.properties.imageProcessingProperties.FiltersProperties, selectedLayer: e.value }
            setLocalPropertiesCallback('FiltersProperties', filtersProps);
            let updatedColorTableProps = props.tabInfo.properties.imageProcessingProperties.ColorTableProperties;
            updatedColorTableProps['DirectProperties']['selectedLayer'] = e.value;
            updatedColorTableProps['ByHistogramProperties']['selectedLayer'] = e.value;
            setLocalPropertiesCallback('ColorTableProperties', updatedColorTableProps);
        }, 'MapViewportForm/ImageProcessing.handleLayerChange')
    }

    return (
        <div className='form__flex-and-row'>
            <div style={{ width: '80%' }}>
                <NestedTabsCtrl
                    tabTypes={tabTypes}
                    nestedTabName='ImageProcessing'
                    lastTabInfo={lastTabInfo}
                />
            </div>
            <ListBox style={{ width: '20%' }} emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 50}vh`, maxHeight: `${globalSizeFactor * 50}vh` }} name='selectedLayer' optionLabel="label" value={props.tabInfo.properties.imageProcessingProperties.selectedLayer} onChange={handleLayerChange} options={props.tabInfo.properties.imageProcessingProperties.viewportLayers} />
        </div>
    )
}
