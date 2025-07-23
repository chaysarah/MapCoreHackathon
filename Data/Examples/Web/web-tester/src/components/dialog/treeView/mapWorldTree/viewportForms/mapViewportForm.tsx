import { useEffect, useRef, useState } from "react";
import { TabPanel, TabView } from "primereact/tabview";
import { Button } from "primereact/button";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import { useDispatch } from 'react-redux';
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { setHandleApplyFunc } from "../../../../../redux/MapWorldTree/mapWorldTreeActions";
import TreeNodeModel from "../../../../shared/models/map-tree-node.model"
import _ from "lodash";
import '../../../styles/forms.css'
import generalService from "../../../../../services/general.service";
import General, { GeneralProperties } from "./general";
import DTMVisualization, { DTMVisualizationProperties } from "./DTMVisualization";
import ViewportConversions, { ViewportConversionsProperties } from "./viewportConversions";
import Terrains, { TerrainsProperties } from "./terrains";
import ObjectWorld, { ObjectWorldProperties } from "./objectWorld";
import Rendering, { RenderingProperties } from "./rendering/rendering";
import ViewportAsCamera, { ViewportAsCameraProperties, ViewportAsCameraPropertiesState } from "./viewportAsCameraForms/viewportAsCamera";
import ImageProcessing, { ImageProcessingProperties, ImageProcessingPropertiesState } from "./imageProcessingForms/imageProcessing";
import SectionMap, { SectionMapProperties, SectionMapPropertiesState } from "./sectionMap/sectionMap";
import ImageCalc, { ImageCalcProperties } from "./imageCalc";
import dialogStateService from "../../../../../services/dialogStateService";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import { setObjectWorldTree } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import ObjectTreeNode from "../../../../shared/models/tree-node.model";

export class MapViewportFormProperties {
    generalProperties: GeneralProperties;
    DTMVisualizationProperties: DTMVisualizationProperties;
    viewportConversionsProperties: ViewportConversionsProperties;
    terrainsProperties: TerrainsProperties = new TerrainsProperties();
    objectWorldProperties: ObjectWorldProperties;
    renderingProperties: RenderingProperties;
    viewportAsCameraProperties: ViewportAsCameraProperties = new ViewportAsCameraProperties();
    imageProcessingProperties: ImageProcessingProperties = new ImageProcessingProperties();
    sectionMapProperties: SectionMapProperties = new SectionMapProperties();
    // imageCalcProperties: ImageCalcProperties;
};

export class MapViewportFormTabInfo {
    properties: MapViewportFormProperties;
    setPropertiesCallback: (tab: string, key: string, value: any) => void;
    setCurrStatePropertiesCallback: (tab: string, key: string, value: any) => void;
    setInitialStatePropertiesCallback: (tab: string, key: string, value: any) => void;
    setApplyCallBack: (tabName: string, Callback: () => void) => void;
    currentViewport: TreeNodeModel;
};

export default function MapViewportForm() {
    const [types, setType] = useState<string[]>(['General', 'DTM Visualization', 'Viewport Conversions', 'Terrains', 'Object World', 'Rendering',
        'Viewport as Camera', 'Image Processing', 'Section Map'])//, 'Image Calc'
    const dispatch = useDispatch();
    const [localProperties, setLocalProperties] = useState<MapViewportFormProperties>(new MapViewportFormProperties)
    const [applyCallbacks, setApplyCallbacks] = useState(new Map())
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let currentViewport = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    let mapWorldTree = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    const cursorPos: any = useSelector((state: AppState) => state.mapWindowReducer.cursorPos);
    let objectTreeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);

    let Comps = {
        'General': General,
        'DTM Visualization': DTMVisualization,
        'Viewport Conversions': ViewportConversions,
        'Terrains': Terrains,
        'Object World': ObjectWorld,
        'Rendering': Rendering,
        'Viewport as Camera': ViewportAsCamera,
        'Image Processing': ImageProcessing,
        'Section Map': SectionMap,
        // 'Image Calc': ImageCalc,
    }

    useEffect(() => {
        runCodeSafely(() => {
            setInitialPropertiesForAllTabs();
            updateTreeRedux();
        }, 'MapViewportForm/useEffect => mounting');
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            setInitialPropertiesForAllTabs()
        }, 'MapViewportForm/useEffect => currentViewport');
    }, [currentViewport])

    useEffect(() => {
        runCodeSafely(() => {
            dispatch(setHandleApplyFunc(handleApplyFunc));
        }, 'MapViewportForm/useEffect => applyCallbacks');
    }, [applyCallbacks])

    const updateTreeRedux = () => {
        runCodeSafely(() => {
            let buildedTree: ObjectTreeNode = objectWorldTreeService.buildTree();
            dispatch(setObjectWorldTree(buildedTree));
        }, "mapViewportForm.useEffect");
    }
    const setInitialPropertiesForAllTabs = () => {
        //sectionMapProperties
        setInitialPropertiesCallback("sectionMapProperties", null, SectionMapPropertiesState.getDefault({ currentViewport }));
        setLocalPropertiesCallback("sectionMapProperties", null, SectionMapProperties.getDefault({ currentViewport }));
        //viewportAsCameraProperties
        let viewportAsCameraStateDefaults = ViewportAsCameraPropertiesState.getDefault({ currentViewport, cursorPos })
        setInitialPropertiesCallback("viewportAsCameraProperties", null, viewportAsCameraStateDefaults);
        viewportAsCameraStateDefaults.setInitialTabsStateCB && viewportAsCameraStateDefaults.setInitialTabsStateCB();
        setLocalPropertiesCallback("viewportAsCameraProperties", null, ViewportAsCameraProperties.getDefault({ currentViewport, objectTreeRedux }));
        // ImageProcessing
        let ImageProcessingStateDefaults = ImageProcessingPropertiesState.getDefault({ currentViewport, cursorPos, mapWorldTree })
        setInitialPropertiesCallback("ImageProcessingProperties", null, ImageProcessingStateDefaults);
        setLocalPropertiesCallback("imageProcessingProperties", null, ImageProcessingProperties.getDefault({ currentViewport, objectTreeRedux, mapWorldTree }));
        // Terrains
        setInitialPropertiesCallback("TerrainsProperties", null, ImageProcessingProperties.getDefault({ currentViewport, cursorPos, mapWorldTree }));
        setLocalPropertiesCallback("terrainsProperties", null, TerrainsProperties.getDefault({ currentViewport, objectTreeRedux, mapWorldTree }));
    }

    const setLocalPropertiesCallback = (tab: string, key: string, value: any) => {
        if (key === null && !_.isEqual(localProperties[tab], value)) {
            setLocalProperties(properties => ({ ...properties, [tab]: value }))
        }
        else if (localProperties[tab] && !_.isEqual(localProperties[tab][key], value)) {
            let updatedProperties = { ...localProperties }
            updatedProperties[tab][key] = value;
            setLocalProperties(updatedProperties);
        }
        // dialogStateService.setDialogState(key ? [tab, key].join('.') : tab, value)
    }

    const setCurrPropertiesCallback = (tab: string, key: string, value: any) => {
        dialogStateService.setDialogState(key ? [tab, key].join('.') : tab, value);
    }
    const setInitialPropertiesCallback = (tab: string, key: string, value: any) => {
        dialogStateService.initDialogState(key ? [tab, key].join('.') : tab, value);
    }

    const setApplyCallBack = (tabName: string, Callback: () => void) => {
        const newApplyCallbacks = { ...applyCallbacks };
        newApplyCallbacks[tabName] = Callback;
        setApplyCallbacks(newApplyCallbacks);
    }

    const handleApplyFunc = () => {
        runCodeSafely(() => {
            for (let applyCallback of Object.values(applyCallbacks)) {
                applyCallback();
            }
            // dialogStateService.applyDialogState();
        }, 'MapViewportForm => handleApplyFunc');
    }

    const tabInfo: MapViewportFormTabInfo = {
        properties: localProperties,
        setPropertiesCallback: setLocalPropertiesCallback,
        setCurrStatePropertiesCallback: setCurrPropertiesCallback,
        setInitialStatePropertiesCallback: setInitialPropertiesCallback,
        setApplyCallBack: setApplyCallBack,
        currentViewport: currentViewport,
    }

    return (
        <div className="form__tabview-container">
            {<TabView scrollable style={{ height: '100%', borderBottom: `${globalSizeFactor * 0.15}vh solid #dee2e6`, marginBottom: '1%' }} >
                {types.map((type: string, key: number) => {
                    const Item = Comps[type];
                    return <TabPanel header={type} key={key} style={{ height: '100%' }}>
                        <span>
                            <div>
                                <Item tabInfo={tabInfo} />
                            </div>
                        </span>
                    </TabPanel>
                })}
            </TabView>}
        </div>
    );
}
