import { useEffect, useRef, useState } from "react";
import { TabPanel, TabView } from "primereact/tabview";
import OverlayManagerGeneralForm, { OverlayManagerGeneralFormProperties } from "./overlayManagerGeneralForm";
import ConvertPoint, { ConvertPointProperties } from "./convertPoint";
import SizeFactor, { SizeFactorProperties } from "./sizeFactor";
import { Button } from "primereact/button";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import { useDispatch } from 'react-redux';
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { setHandleApplyFunc } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import TreeNodeModel from "../../../../shared/models/tree-node.model"
import _ from "lodash";
import generalService from "../../../../../services/general.service";
import dialogStateService from "../../../../../services/dialogStateService";

export class OverlayManagerFormProperties {
    generalProperties: OverlayManagerGeneralFormProperties;
    sizeFactorProperties: SizeFactorProperties;
    convertPointProperties: ConvertPointProperties;
}

export class OverlayManagerFormTabInfo {
    properties: OverlayManagerFormProperties;
    setPropertiesCallback: (tab: string, key: string, value: any/*, isInitValue?: boolean*/) => void;
    setCurrStatePropertiesCallback: (tab: string, key: string, value: any) => void;
    setInitialStatePropertiesCallback: (tab: string, key: string, value: any) => void;
    setApplyCallBack: (tabName: string, Callback: () => void) => void;
};

export default function OverlayManagerForm() {
    const [types, setType] = useState<string[]>(['General', 'Size Factor', 'Convert Points'])
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [initialStateProperties, setInitialStateProperties] = useState<OverlayManagerFormProperties>(new OverlayManagerFormProperties)
    const [localProperties, setLocalProperties] = useState<OverlayManagerFormProperties>(new OverlayManagerFormProperties)
    const [applyCallbacks, setApplyCallbacks] = useState(new Map())

    let Comps = {
        'General': OverlayManagerGeneralForm,
        'Size Factor': SizeFactor,
        'Convert Points': ConvertPoint,
    }

    useEffect(() => {
        runCodeSafely(() => {
            dispatch(setHandleApplyFunc(handleApplyFunc));
        }, 'OverlayManagerForm/useEffect => applyCallbacks');
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
        const newApplyCallbacks = { ...applyCallbacks };
        newApplyCallbacks[tabName] = Callback;
        setApplyCallbacks(newApplyCallbacks);
    }

    const handleApplyFunc = () => {
        runCodeSafely(() => {
            for (let applyCallback of Object.values(applyCallbacks)) {
                applyCallback();
            }
            dialogStateService.applyDialogState(); // remove - move to lacal apply funcs
        }, 'OverlayManagerForm => handleApplyFunc');
    }

    const tabInfo: OverlayManagerFormTabInfo = {
        properties: localProperties,
        setPropertiesCallback: setLocalPropertiesCallback,
        setCurrStatePropertiesCallback: setCurrPropertiesCallback,
        setInitialStatePropertiesCallback: setInitialPropertiesCallback,
        setApplyCallBack: setApplyCallBack,
    }

    return (
        <div style={{ height: `${globalSizeFactor * 70}vh` }}>
            {<TabView scrollable style={{ height: '100%', borderBottom: `${globalSizeFactor * 0.15}vh solid #dee2e6`, marginBottom: '1%' }} >
                {types.map((type: string, key: number) => {
                    const Item = Comps[type];
                    return <TabPanel header={type} key={key} style={{ height: '100%' }}>
                        <span>
                            <div>
                                <Item tabInfo={tabInfo} />
                            </div>
                            {/* {getCloseButton()} */}
                        </span>
                    </TabPanel>
                })}
            </TabView>}
        </div>
    );
}


