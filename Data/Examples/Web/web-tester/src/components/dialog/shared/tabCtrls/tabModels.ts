
export class TabType {
    index: number;
    header: string;
    statePropertiesClass?: any;
    propertiesClass: any;
    component: React.FC<{ tabInfo: TabInfo }>;
}
export class TabInfo {
    tabProperties: any;
    setPropertiesCallback: {
        (data: { [key: string]: any }): void;
        (key: string, value: any): void;
    };
    setCurrStatePropertiesCallback?: (key: string, value: any) => void;
    applyCurrStatePropertiesCallback?: (pathesToApply?: string[]) => void;
    setApplyCallBack: (callback: () => void) => void;
    saveData: (event: any) => void;
    getTabPropertiesByTabPropertiesClass: (tabPropertiesClass: any) => any;
    setSiblingProperty: (tabPropertiesClass: any, key: string, value: any) => any;
};