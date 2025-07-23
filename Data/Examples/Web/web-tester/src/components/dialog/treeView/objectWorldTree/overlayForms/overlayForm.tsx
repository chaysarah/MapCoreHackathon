import { useEffect, useState } from "react";
import React from 'react';
import { TabPanel, TabView } from "primereact/tabview";
import Objects, { ObjectsProperties } from "./objects";
import General, { GeneralProperties } from "./general";
import ObjectChanging, { ObjectChangingProperties } from "./objectChanging";
import ColorOverriding, { ColorOverridingProperties } from "./colorOverriding";
import { Button } from "primereact/button";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { setHandleApplyFunc } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import TreeNodeModel from "../../../../shared/models/tree-node.model"
import objectWorldTreeService, { fileExplorerSelectionInListBoxService } from "../../../../../services/objectWorldTree.service";
import generalService from "../../../../../services/general.service";
import { faL } from "@fortawesome/free-solid-svg-icons";
import _ from "lodash";
import dialogStateService from "../../../../../services/dialogStateService";

export class OverlayFormProperties {
    generalProperties: GeneralProperties;
    objectsProperties: ObjectsProperties;
    objectChangingProperties: ObjectChangingProperties;
    colorOverridingProperties: ColorOverridingProperties;
}

export class OverlayFormTabInfo {
    properties: OverlayFormProperties;
    setPropertiesCallback: (tab: string, key: string, value: any/*, isInitValue?: boolean*/) => void;
    setCurrStatePropertiesCallback: (tab: string, key: string, value: any) => void;
    setInitialStatePropertiesCallback: (tab: string, key: string, value: any) => void;
    setApplyCallBack: (tabName: string, Callback: () => void) => void;
    currentOverlay: TreeNodeModel
};

export default function OverlayForm() {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const [types, setType] = useState<string[]>(['General', 'Objects', 'Object Changing', 'Color Overriding'])
    // const handleApplyFunc = useSelector((state: AppState) => state.objectWorldTreeReducer.handleApplyFunc);
    const [localProperties, setLocalProperties] = useState<OverlayFormProperties>(new OverlayFormProperties)
    const [applyCallbacks, setApplyCallbacks] = useState(new Map())
    let currentOverlay = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);

    let Comps: any = {
        'Objects': Objects,
        'General': General,
        'Object Changing': ObjectChanging,
        'Color Overriding': ColorOverriding,
    }

    useEffect(() => {
        runCodeSafely(() => {
            dispatch(setHandleApplyFunc(handleApplyFunc));
        }, 'OverlayForm/useEffect => applyCallbacks');
    }, [applyCallbacks])

    const setLocalPropertiesCallback = (tab: string, key: string, value: any) => {
        if (key == null && !_.isEqual(localProperties[tab], value)) {
            setLocalProperties(properties => ({ ...properties, [tab]: value }))
        }
        else if (localProperties[tab] && !_.isEqual(localProperties[tab][key], value)) {
            let updatedProperties = { ...localProperties }
            updatedProperties[tab][key] = value;
            setLocalProperties(updatedProperties);
        }
    }

    const setCurrPropertiesCallback = (tab: string, key: string, value: any) => {
        dialogStateService.setDialogState(key ? [tab, key].join('.') : tab, value);
    }

    const setInitialPropertiesCallback = (tab: string, key: string, value: any) => {
        dialogStateService.initDialogState(key ? [tab, key].join('.') : tab, value);
    }

    const setApplyCallBack = (tabName: string, Callback: () => void) => {
        setApplyCallbacks(applyCallbacks => {
            return { ...applyCallbacks, [tabName]: Callback }
        });
    }

    const handleApplyFunc = () => {
        runCodeSafely(() => {
            for (let applyCallback of Object.values(applyCallbacks)) {
                applyCallback();
            }
        }, 'OverlayForm => handleApplyFunc');
    }

    const tabInfo: OverlayFormTabInfo = {
        properties: localProperties,
        setPropertiesCallback: setLocalPropertiesCallback,
        setCurrStatePropertiesCallback: setCurrPropertiesCallback,
        setInitialStatePropertiesCallback: setInitialPropertiesCallback,
        setApplyCallBack: setApplyCallBack,
        currentOverlay: currentOverlay,
    }

    return (
        <div style={{ height: `${globalSizeFactor * 70}vh` }}>
            { <TabView style={{ height: '100%', borderBottom: `${globalSizeFactor * 0.15}vh solid #dee2e6`, marginBottom: '1%' }} scrollable >
                {types.map((item: string, key: number) => {
                    const Item = Comps[item];
                    return <TabPanel header={item} key={key} style={{ height: '100%' }}>
                        <div>
                            <Item tabInfo={tabInfo} />
                        </div>
                    </TabPanel>
                })}
            </TabView>}
        </div>
    );
}
