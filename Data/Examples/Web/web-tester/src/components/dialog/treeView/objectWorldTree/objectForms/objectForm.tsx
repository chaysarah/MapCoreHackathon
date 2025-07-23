import { useEffect, useState } from "react";
import React from 'react';
import { TabPanel, TabView } from "primereact/tabview";
import General, { GeneralProperties } from "./general";
import ObjectLocations, { ObjectLocationsProperties } from "./objectLocations";
import { Button } from "primereact/button";
import { setHandleApplyFunc } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import _ from "lodash";
import TreeNodeModel from "../../../../shared/models/tree-node.model"
import { DialogTypesEnum } from "../../../../../tools/enum/enums";
import generalService from "../../../../../services/general.service";
import dialogStateService from "../../../../../services/dialogStateService";

export class ObjectFormProperties {
    generalProperties: GeneralProperties;
    objectLocationsProperties: ObjectLocationsProperties;
}

export class ObjectFormTabInfo {
    properties: ObjectFormProperties;
    setPropertiesCallback: (tab: string, key: string, value: any/*, isInitValue?: boolean*/) => void;
    setCurrStatePropertiesCallback: (tab: string, key: string, value: any) => void;
    setInitialStatePropertiesCallback: (tab: string, key: string, value: any) => void;
    setApplyCallBack: (tabName: string, Callback: () => void) => void;
    currentObject: TreeNodeModel;
};

export default function ObjectForm() {
    const dispatch = useDispatch();
    const types = ['General', 'Object Locations'];
    let currentObject = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    // const [initialStateProperties, setInitialStateProperties] = useState<ObjectFormProperties>(new ObjectFormProperties)
    const [localProperties, setLocalProperties] = useState<ObjectFormProperties>(new ObjectFormProperties);
    const [applyCallbacks, setApplyCallbacks] = useState(new Map());
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    let Comps: any = {
        'General': General,
        'Object Locations': ObjectLocations,
    }

    useEffect(() => {
        runCodeSafely(() => {
            dispatch(setHandleApplyFunc(handleApplyFunc));
        }, 'ObjectForm/useEffect => applyCallbacks');
    }, [applyCallbacks])

    const setLocalPropertiesCallback = (tab: string, key: string, value: any) => {
        if (key === null && !_.isEqual(localProperties[tab], value)) {
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
        }, 'ObjectForm => handleApplyFunc');
    }

    const tabInfo: ObjectFormTabInfo = {
        properties: localProperties,
        setPropertiesCallback: setLocalPropertiesCallback,
        setCurrStatePropertiesCallback: setCurrPropertiesCallback,
        setInitialStatePropertiesCallback: setInitialPropertiesCallback,
        setApplyCallBack: setApplyCallBack,
        currentObject: currentObject,
    }

    return (
        <div style={{ height: `${globalSizeFactor * 70}vh` }}>
            {<TabView style={{ height: '100%', borderBottom: `${globalSizeFactor * 0.15}vh solid #dee2e6`, marginBottom: '1%' }} scrollable >
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
