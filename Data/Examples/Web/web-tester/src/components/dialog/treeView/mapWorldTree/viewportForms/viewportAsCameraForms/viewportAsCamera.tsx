import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { TabPanel, TabView } from "primereact/tabview";
import _ from 'lodash';

import './styles/viewportAsCameraForms.css';
import { anyMapTypeGetDefaultsByUserValues } from "./anyMapType";
import Map2D, { Map2DProperties, Map2DPropertiesState } from "./2DMap";
import Map3D, { Map3DProperties, Map3DPropertiesState, map3DGetDefaultsByUserValues } from "./3DMap";
import AnyMapType, { AnyMapTypeProperties, AnyMapTypePropertiesState } from "./anyMapType";
import CameraAttachment, { CameraAttachmentProperties, CameraAttachmentPropertiesState } from "./cameraAttachment";
import CameraConversions, { CameraConversionsProperties, CameraConversionsPropertiesState } from "./cameraConversions";
import { MapViewportFormTabInfo } from "../mapViewportForm";
import { Properties } from "../../../../dialog";
import dialogStateService from "../../../../../../services/dialogStateService";
import store from "../../../../../../redux/store";
import { AppState } from "../../../../../../redux/combineReducer";
import { TreeNodeModel } from "../../../../../../services/tree.service";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";

export class ViewportAsCameraPropertiesState implements Properties {
    setInitialTabsStateCB: () => void;

    static getDefault(p: any): ViewportAsCameraPropertiesState {
        let { currentViewport, cursorPos } = p;
        return {
            setInitialTabsStateCB: () => { setInitialStatePropertiesForAllTabsCB(currentViewport, cursorPos) },
        }
    }
}
export class ViewportAsCameraProperties extends ViewportAsCameraPropertiesState {
    cameraConversionsProperties: CameraConversionsProperties;
    anyMapTypeProperties: AnyMapTypeProperties;
    map3DProperties: Map3DProperties;
    map2DProperties: Map2DProperties;
    cameraAttachmentProperties: CameraAttachmentProperties;

    static getDefault(p: any): ViewportAsCameraProperties {
        let stateDefaults = super.getDefault(p);
        let defaults: ViewportAsCameraProperties = {
            ...stateDefaults,
            cameraConversionsProperties: CameraConversionsProperties.getDefault(p),
            anyMapTypeProperties: AnyMapTypeProperties.getDefault(p),
            map3DProperties: Map3DProperties.getDefault(p),
            map2DProperties: Map2DProperties.getDefault(p),
            cameraAttachmentProperties: CameraAttachmentProperties.getDefault(p),
        }
        return defaults;
    }
};
export class ViewportAsCameraFormTabInfo {
    tabProperties: any;//CameraConversionsProperties | AnyMapTypeProperties | Map3DProperties | Map2DProperties | CameraAttachmentProperties;
    loadProperties: (currentViewport: TreeNodeModel) => void;
    setPropertiesCallback: (key: string, value: any) => void;
    setCurrStatePropertiesCallback: (key: string, value: any) => void;
    applyCurrStatePropertiesCallback: (pathesToApply?: string[]) => void;
    setApplyCallBack: (callback: () => void) => void;
};

let currentViewport = store.getState().objectWorldTreeReducer.selectedNodeInTree;
const tabTypes = [
    { index: 0, header: 'Camera Conversions', propertiesType: 'cameraConversionsProperties', statePropertiesClass: CameraConversionsPropertiesState, propertiesClass: CameraConversionsProperties, component: CameraConversions },
    { index: 1, header: 'Any Map Type', propertiesType: 'anyMapTypeProperties', statePropertiesClass: AnyMapTypePropertiesState, propertiesClass: AnyMapTypeProperties, component: AnyMapType },
    { index: 2, header: '2D Map', propertiesType: 'map2DProperties', statePropertiesClass: Map2DPropertiesState, propertiesClass: Map2DProperties, component: Map2D },
    { index: 3, header: '3D Map', propertiesType: 'map3DProperties', statePropertiesClass: Map3DPropertiesState, propertiesClass: Map3DProperties, component: Map3D },
    { index: 4, header: 'Camera Attachment', propertiesType: 'cameraAttachmentProperties', statePropertiesClass: CameraAttachmentPropertiesState, propertiesClass: CameraAttachmentProperties, component: CameraAttachment },
]
const setInitialStatePropertiesForAllTabsCB = (currentViewport: TreeNodeModel, cursorPos: any) => {
    for (let tabType of tabTypes) {
        let stateDefaults = tabType.statePropertiesClass.getDefault({ currentViewport, cursorPos })
        dialogStateService.initDialogState(`viewportAsCameraProperties.${tabType.propertiesType}`, stateDefaults);
    }
}

export default function ViewportAsCamera(props: { tabInfo: MapViewportFormTabInfo }) {
    const dispatch = useDispatch();
    let treeRedux = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    const cursorPos: any = useSelector((state: AppState) => state.mapWindowReducer.cursorPos);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [viewportAsCameraApplyCallbacks, setViewportAsCameraApplyCallbacks] = useState(new Map())
    let [activeTab, setActiveTab] = useState(0);
    const [viewportAsCameraLocalProperties, setViewportAsCameraLocalProperties] = useState<ViewportAsCameraProperties>(props.tabInfo.properties.viewportAsCameraProperties)

    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        mounting: false,
    })

    //UseEffects
    useEffect(() => {
        setIsMountedUseEffect({ ...isMountedUseEffect, mounting: true })
    }, [])
    useEffect(() => {//update mapViewportForm with the updated properties of all tabs
        runCodeSafely(() => {
            if (isMountedUseEffect.mounting) {
                props.tabInfo.setPropertiesCallback('viewportAsCamera', null, viewportAsCameraLocalProperties);
            }
        }, 'MapViewportForm/ViewportAsCamera.useEffect => currentViewport');
    }, [viewportAsCameraLocalProperties])
    useEffect(() => {
        setViewportAsCameraLocalProperties(props.tabInfo.properties.viewportAsCameraProperties)
    }, [props.tabInfo.properties.viewportAsCameraProperties])
    useEffect(() => {
        runCodeSafely(() => {
            props.tabInfo.setApplyCallBack("ViewportAsCamera", applyAllTabs);
        }, 'MapViewportForm/useEffect => applyCallbacks');
    }, [viewportAsCameraApplyCallbacks])

    const applyAllTabs = () => {
        runCodeSafely(() => {
            for (let applyCallback of Object.values(viewportAsCameraApplyCallbacks)) {
                applyCallback();
            }
        }, 'MapViewportForm/ViewportAsCamera => applyAllTabs');
    }
    //#region TabInfoFuncs
    const setTabApplyCallBack = (Callback: () => void) => {
        let tabName = tabTypes[activeTab].propertiesType;
        const newViewportAsCameraApplyCallbacks = { ...viewportAsCameraApplyCallbacks };
        newViewportAsCameraApplyCallbacks[tabName] = Callback;
        setViewportAsCameraApplyCallbacks(newViewportAsCameraApplyCallbacks);
    }
    const loadProperties = (currentViewport: TreeNodeModel) => {
        runCodeSafely(() => {
            let params = { currentViewport: currentViewport, cursorPos: cursorPos }
            let map3DUpdatedStateProps = map3DGetDefaultsByUserValues(viewportAsCameraLocalProperties.map3DProperties, params);
            let map3DUpdatedProps = { ...viewportAsCameraLocalProperties.map3DProperties, ...map3DUpdatedStateProps }
            let anyMapTypeUpdatedStateProps = anyMapTypeGetDefaultsByUserValues(viewportAsCameraLocalProperties.anyMapTypeProperties, params);
            let anyMapTypeUpdatedProps = { ...viewportAsCameraLocalProperties.anyMapTypeProperties, ...anyMapTypeUpdatedStateProps }
            setViewportAsCameraLocalProperties({ ...viewportAsCameraLocalProperties, map3DProperties: map3DUpdatedProps, anyMapTypeProperties: anyMapTypeUpdatedProps })
            dialogStateService.initDialogState(`viewportAsCameraProperties.${tabTypes[1].propertiesType}`, anyMapTypeUpdatedStateProps)
            dialogStateService.initDialogState(`viewportAsCameraProperties.${tabTypes[3].propertiesType}`, map3DUpdatedStateProps)
        }, 'MapViewportForm/ViewportAsCamera.loadProperties')
    }
    const setLocalPropertiesCallback = (key: string, value: any) => {
        let tab = tabTypes[activeTab].propertiesType;
        if (key === null && !_.isEqual(viewportAsCameraLocalProperties[tab], value)) {
            setViewportAsCameraLocalProperties(properties => ({ ...properties, [tab]: value }))
        }
        else if (viewportAsCameraLocalProperties[tab] && !_.isEqual(viewportAsCameraLocalProperties[tab][key], value)) {
            let updatedProperties = { ...viewportAsCameraLocalProperties }
            updatedProperties[tab][key] = value;
            setViewportAsCameraLocalProperties(updatedProperties);
        }
    }
    const setCurrPropertiesCallback = (key: string, value: any) => {
        let tab = `viewportAsCameraProperties.${tabTypes[activeTab].propertiesType}`;
        dialogStateService.setDialogState(key ? [tab, key].join('.') : tab, value);
    }
    const applyCurrPropertiesCallback = (pathesToApply?: string[]) => {
        let tab = tabTypes[activeTab].propertiesType;
        pathesToApply = pathesToApply?.map(path => `viewportAsCameraProperties.${tab}.${path}`);
        dialogStateService.applyDialogState(pathesToApply);
    }
    //#endregion

    const getTabInfo = (tabType: any) => {
        let tabInfo: ViewportAsCameraFormTabInfo = {
            tabProperties: viewportAsCameraLocalProperties[tabType.propertiesType],
            loadProperties: loadProperties,
            setPropertiesCallback: setLocalPropertiesCallback,
            setCurrStatePropertiesCallback: setCurrPropertiesCallback,
            applyCurrStatePropertiesCallback: applyCurrPropertiesCallback,
            setApplyCallBack: setTabApplyCallBack,
        }
        return tabInfo;
    }

    return <div className="form__tabview-container">
        <TabView activeIndex={activeTab} onTabChange={e => setActiveTab(e.index)} scrollable style={{ height: '100%', borderBottom: `${globalSizeFactor * 0.15}vh solid #dee2e6`, marginBottom: '1%' }} >
            {tabTypes.map((tabType: any) => {
                const Item = tabType.component;
                return <TabPanel header={tabType.header} key={tabType.index} style={{ height: '100%' }}>
                    <span>
                        <div>
                            <Item tabInfo={getTabInfo(tabType)} />
                        </div>
                    </span>
                </TabPanel>
            })}
        </TabView>
    </div>
}
