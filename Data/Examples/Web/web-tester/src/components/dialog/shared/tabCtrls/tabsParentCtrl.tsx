import { TabPanel, TabView } from "primereact/tabview";
import store from "../../../../redux/store";
import dialogStateService from "../../../../services/dialogStateService";
import { TreeNodeModel } from "../../../../services/tree.service";
import { runCodeSafely } from "../../../../common/services/error-handling/errorHandler";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../redux/combineReducer";
import _ from 'lodash';
import { setHandleApplyFunc } from "../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import { TabInfo, TabType } from "./tabModels";

// -- Component Description: Ctrl fot the tabs' container --
//remembers the values ​​of the tabs' fields even when switching between tabs.
//Optional: a message when switching between nodes in the tree next to the tab container (if exists) without saving the state values (= mapcore values)

export default function TabsParentCtrl(props: {
    tabTypes: TabType[],
    parentName: string,
    getDefaultFuncProps: {},
    selectedNode?: any,
    tabViewHeight?: number,
    getAllTabsProperties?: (tabsProperties: any) => void
}) {

    const getInitialParentProperties = () => {
        let initialParentLocalProperties = {};
        runCodeSafely(() => {
            props.tabTypes.forEach(tab => {
                initialParentLocalProperties[tab.propertiesClass.name] = tab.propertiesClass.getDefault(props.getDefaultFuncProps)
            });
        }, `${props.parentName}.getInitialParentProperties`)
        return initialParentLocalProperties;
    }

    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [applyCallbacks, setApplyCallbacks] = useState(new Map())
    let [activeTab, setActiveTab] = useState(0);
    const [parentLocalProperties, setParentLocalProperties] = useState(getInitialParentProperties())

    //#region  UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            props.tabTypes[activeTab].statePropertiesClass && setInitialStatePropertiesForAllTabsCB();
        }, `${props.parentName}/useEffect => onMounting`)
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            props.getAllTabsProperties && props.getAllTabsProperties(parentLocalProperties);
        }, `${props.parentName}/useEffect => parentLocalProperties`)
    }, [parentLocalProperties])
    useEffect(() => {
        runCodeSafely(() => {
            props.selectedNode && setParentLocalProperties(getInitialParentProperties())
            if (props.selectedNode && props.tabTypes[0].statePropertiesClass) {
                setInitialStatePropertiesForAllTabsCB();
            }
            if (!props.tabTypes[activeTab]) {
                setActiveTab(0);
            }
        }, `${props.parentName}/useEffect => props.selectedNode`)
    }, [props.selectedNode])
    useEffect(() => {
        runCodeSafely(() => {
            dispatch(setHandleApplyFunc(applyAllTabs));
        }, `${props.parentName}/useEffect => applyCallbacks`);
    }, [applyCallbacks])
    //#endregion

    const setInitialStatePropertiesForAllTabsCB = () => {
        for (let tabType of props.tabTypes) {
            let stateDefaults = tabType.statePropertiesClass.getDefault(props.getDefaultFuncProps);
            dialogStateService.initDialogState(`${tabType.propertiesClass.name}`, stateDefaults);
        }
    }
    const applyAllTabs = () => {
        runCodeSafely(() => {
            for (let applyCallback of Object.values(applyCallbacks)) {
                applyCallback();
            }
        }, `${props.parentName} => applyAllTabs`);
    }
    const customIsEqual = (obj1: any, obj2: any): boolean => {
        return _.isEqualWith(obj1, obj2, (val1, val2) => {
            if (val1 instanceof MapCore.IMcSpatialQueries.IAreaOfSight && val2 instanceof MapCore.IMcSpatialQueries.IAreaOfSight) {
                return val1 === val2;
            }
            return undefined;
        });
    }
    //#region TabInfo Functions Implemmention
    const setTabApplyCallBack = (Callback: () => void) => {
        runCodeSafely(() => {
            let tabName = props.tabTypes[activeTab].propertiesClass.name;
            const newApplyCallbacks = { ...applyCallbacks };
            newApplyCallbacks[tabName] = Callback;
            setApplyCallbacks(newApplyCallbacks);
        }, `${props.parentName}\ ${props.tabTypes[activeTab].propertiesClass.name} => setTabApplyCallBack`);
    }
    const setLocalProperty = (key: string, value: any) => {
        let tab = props.tabTypes[activeTab].propertiesClass.name;
        if (key === null && !customIsEqual(parentLocalProperties[tab], value)) {
            setParentLocalProperties(properties => ({ ...properties, [tab]: value }))
        }
        else if (parentLocalProperties[tab] && (!customIsEqual(parentLocalProperties[tab][key], value) || (parentLocalProperties[tab][key] !== value))) {
            let updatedProperties = { ...parentLocalProperties }
            updatedProperties[tab][key] = value;
            setParentLocalProperties(updatedProperties);
        }
    }

    function setLocalPropertiesCallback(data: { [key: string]: any }): void;
    function setLocalPropertiesCallback(key: string, value: any): void;

    function setLocalPropertiesCallback(key: { [key: string]: any } | string, value?: any) {
        runCodeSafely(() => {
            if (key && typeof key == 'object') {
                Object.entries(key).forEach(([key, value]) => {
                    setLocalProperty(key, value);
                })
            }
            else {
                setLocalProperty(key as string, value);
            }
        }, `${props.parentName}/${props.tabTypes[activeTab].propertiesClass.name} => setLocalPropertiesCallback`);
    }
    const setCurrPropertiesCallback = (key: string, value: any) => {
        runCodeSafely(() => {
            let tab = `${props.tabTypes[activeTab].propertiesClass.name}`;
            dialogStateService.setDialogState(key ? [tab, key].join('.') : tab, value);
        }, `${props.parentName}\ ${props.tabTypes[activeTab].propertiesClass.name} => setCurrPropertiesCallback`);
    }
    const applyCurrPropertiesCallback = (pathesToApply?: string[]) => {
        runCodeSafely(() => {
            let tab = props.tabTypes[activeTab].propertiesClass.name;
            pathesToApply = pathesToApply ? pathesToApply.map(path => `${tab}.${path}`) : [tab];
            dialogStateService.applyDialogState(pathesToApply);
        }, `${props.parentName}\ ${props.tabTypes[activeTab].propertiesClass.name} => applyCurrPropertiesCallback`);
    }
    const saveData = (event: any) => {
        runCodeSafely(() => {
            setLocalPropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (props.tabTypes[activeTab].statePropertiesClass && event.target.name in new props.tabTypes[activeTab].statePropertiesClass) {
                setCurrPropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            }
        }, `${props.parentName}\ ${props.tabTypes[activeTab].propertiesClass.name} => onChange`)
    }
    function getTabPropertiesByTabPropertiesClass<T>(tabPropertiesClass: T): T {
        let isTabExist = props.tabTypes.find(tabType => tabType.propertiesClass == tabPropertiesClass);
        if (isTabExist) {
            let tab = (tabPropertiesClass as any).name;
            return parentLocalProperties[tab];
        }
    }
    function setSiblingProperty(tabPropertiesClass: any, key: string, value: any) {
        let tab = tabPropertiesClass.name;
        if (key === null && !customIsEqual(parentLocalProperties[tab], value)) {
            setParentLocalProperties(properties => ({ ...properties, [tab]: value }))
        }
        else if (parentLocalProperties[tab] && (!customIsEqual(parentLocalProperties[tab][key], value) || (parentLocalProperties[tab][key] !== value))) {
            let updatedProperties = { ...parentLocalProperties }
            updatedProperties[tab][key] = value;
            setParentLocalProperties(updatedProperties);
        }
    }

    //#endregion

    const getTabInfo = (tabType: any) => {
        let tabInfo: TabInfo = null;
        if (props.tabTypes[0].statePropertiesClass) {
            tabInfo = {
                tabProperties: parentLocalProperties[tabType.propertiesClass.name] || {},//for initial render that parentLocalProperties not initialized yet
                setPropertiesCallback: setLocalPropertiesCallback,
                setCurrStatePropertiesCallback: setCurrPropertiesCallback,
                applyCurrStatePropertiesCallback: applyCurrPropertiesCallback,
                setApplyCallBack: setTabApplyCallBack,
                saveData: saveData,
                getTabPropertiesByTabPropertiesClass: getTabPropertiesByTabPropertiesClass,
                setSiblingProperty: setSiblingProperty,
            }
        }
        else {
            tabInfo = {
                tabProperties: parentLocalProperties[tabType.propertiesClass.name] || {},
                setPropertiesCallback: setLocalPropertiesCallback,
                setApplyCallBack: setTabApplyCallBack,
                saveData: saveData,
                getTabPropertiesByTabPropertiesClass: getTabPropertiesByTabPropertiesClass,
                setSiblingProperty: setSiblingProperty,
            }
        }
        return tabInfo;
    }

    return <div className="form__tabview-container">
        <TabView activeIndex={activeTab} onTabChange={e => setActiveTab(e.index)} scrollable style={{ height: '100%', borderBottom: `${globalSizeFactor * 0.15}vh solid #dee2e6`, marginBottom: '1%' }} >
            {props.tabTypes.map((tabType: any) => {
                const Item = tabType.component;
                return <TabPanel header={tabType.header} key={tabType.index} style={{ height: '100%' }}>
                    <div style={{ height: `${props.tabViewHeight ? `${globalSizeFactor * props.tabViewHeight}vh` : '100%'}` }}>
                        <Item tabInfo={getTabInfo(tabType)} />
                    </div>
                </TabPanel>
            })}
        </TabView>
    </div>
}