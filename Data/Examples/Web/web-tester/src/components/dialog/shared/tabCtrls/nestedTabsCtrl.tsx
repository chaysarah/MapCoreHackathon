import { useEffect, useState } from "react";
import { AppState } from "../../../../redux/combineReducer";
import { useDispatch, useSelector } from "react-redux";
import { runCodeSafely } from "../../../../common/services/error-handling/errorHandler";
import { TabPanel, TabView } from "primereact/tabview";
import _ from 'lodash';
import { TabInfo, TabType } from "./tabModels";

export default function NestedTabsCtrl(props: {
    tabTypes: TabType[],
    nestedTabName: string,
    lastTabInfo: TabInfo,
    tabViewHeight?: number,
}) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [applyCallbacks, setApplyCallbacks] = useState(new Map())
    let [activeTab, setActiveTab] = useState(0);

    useEffect(() => {
        runCodeSafely(() => {
            props.lastTabInfo.setApplyCallBack(applyAllTabs)
        }, `${props.nestedTabName}/useEffect => applyCallbacks`);
    }, [applyCallbacks])

    const applyAllTabs = () => {
        runCodeSafely(() => {
            for (let applyCallback of Object.values(applyCallbacks)) {
                applyCallback();
            }
        }, `${props.nestedTabName} => applyAllTabs`);
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
    const setLocalProperty = (key: string, value: any) => {
        let tab = props.tabTypes[activeTab].propertiesClass.name;
        let updatedProps = null;
        if (key === null && !customIsEqual(props.lastTabInfo.tabProperties[tab], value)) {
            updatedProps = { ...props.lastTabInfo.tabProperties, [tab]: value }
            props.lastTabInfo.setPropertiesCallback(null, updatedProps)
        }
        else if (props.lastTabInfo.tabProperties[tab] && !customIsEqual(props.lastTabInfo.tabProperties[tab][key], value)) {
            updatedProps = { ...props.lastTabInfo.tabProperties }
            updatedProps[tab][key] = value;
            props.lastTabInfo.setPropertiesCallback(null, updatedProps)
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
        }, `${props.nestedTabName}/${props.tabTypes[activeTab].propertiesClass.name} => setLocalPropertiesCallback`);
    }
    const setTabApplyCallBack = (Callback: () => void) => {
        runCodeSafely(() => {
            let tabName = props.tabTypes[activeTab].propertiesClass.name;
            const newApplyCallbacks = { ...applyCallbacks };
            newApplyCallbacks[tabName] = Callback;
            setApplyCallbacks(newApplyCallbacks);
        }, `${props.nestedTabName}\ ${props.tabTypes[activeTab].propertiesClass.name} => setTabApplyCallBack`);
    }
    const setCurrPropertiesCallback = (key: string, value: any) => {
        runCodeSafely(() => {
            let tab = `${props.tabTypes[activeTab].propertiesClass.name}`;
            let finalKey = key ? [tab, key].join('.') : tab;
            props.lastTabInfo.setCurrStatePropertiesCallback(finalKey, value);
        }, `${props.nestedTabName}\ ${props.tabTypes[activeTab].propertiesClass.name} => setCurrPropertiesCallback`);
    }
    const applyCurrPropertiesCallback = (pathesToApply?: string[]) => {
        runCodeSafely(() => {
            let tab = props.tabTypes[activeTab].propertiesClass.name;
            pathesToApply = pathesToApply ? pathesToApply.map(path => `${tab}.${path}`) : [tab];
            props.lastTabInfo.applyCurrStatePropertiesCallback(pathesToApply);
        }, `${props.nestedTabName}\ ${props.tabTypes[activeTab].propertiesClass.name} => applyCurrPropertiesCallback`);
    }
    const saveData = (event: any) => {
        runCodeSafely(() => {
            setLocalPropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (props.tabTypes[activeTab].statePropertiesClass && event.target.name in new props.tabTypes[activeTab].statePropertiesClass) {
                setCurrPropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            }
        }, `${props.nestedTabName}\ ${props.tabTypes[activeTab].propertiesClass.name} => onChange`)
    }
    function getTabPropertiesByTabPropertiesClass<T>(tabPropertiesClass: T): T {
        let foundedTabProperties = props.lastTabInfo.getTabPropertiesByTabPropertiesClass(tabPropertiesClass);
        let isTabExist = props.tabTypes.find(tabType => tabType.propertiesClass == tabPropertiesClass);
        if (isTabExist) {
            let tab = (tabPropertiesClass as any).name;
            return props.lastTabInfo.tabProperties[tab];
        }
        return foundedTabProperties;
    }
    function setSiblingProperty(tabPropertiesClass: any, key: string, value: any) {
        let tab = tabPropertiesClass.name;
        let isLocalSiblingTabExist = props.tabTypes.find(tabType => tabType.propertiesClass == tabPropertiesClass);
        let foundedTabProperties = props.lastTabInfo.getTabPropertiesByTabPropertiesClass(tabPropertiesClass);
        if (isLocalSiblingTabExist) {
            let updatedProps = null;
            if (key === null && !customIsEqual(props.lastTabInfo.tabProperties[tab], value)) {
                updatedProps = { ...props.lastTabInfo.tabProperties, [tab]: value }
                props.lastTabInfo.setPropertiesCallback(null, updatedProps)
            }
            else if (props.lastTabInfo.tabProperties[tab] && !customIsEqual(props.lastTabInfo.tabProperties[tab][key], value)) {
                updatedProps = { ...props.lastTabInfo.tabProperties }
                updatedProps[tab][key] = value;
                props.lastTabInfo.setPropertiesCallback(null, updatedProps)
            }
        }
        else if (foundedTabProperties) {
            props.lastTabInfo.setSiblingProperty(tabPropertiesClass, key, value);
        }
    }
    //#endregion

    const getTabInfo = (tabType: any) => {
        let tabInfo: TabInfo = null;
        if (props.tabTypes[0].statePropertiesClass) {
            tabInfo = {
                tabProperties: props.lastTabInfo.tabProperties[tabType.propertiesClass.name],
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
                tabProperties: props.lastTabInfo.tabProperties[tabType.propertiesClass.name],
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