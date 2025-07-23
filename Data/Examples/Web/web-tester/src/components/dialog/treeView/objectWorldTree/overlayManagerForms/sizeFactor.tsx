import { useEffect, useState } from "react";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { getEnumDetailsList } from 'mapcore-lib';
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { ListBox } from "primereact/listbox";
import { Button } from "primereact/button";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import TreeNodeModel, { objectWorldNodeType } from '../../../../shared/models/tree-node.model'
import { InputNumber } from "primereact/inputnumber";
import { MapCoreData, ViewportData } from 'mapcore-lib';
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import generalService from "../../../../../services/general.service";
import { OverlayManagerFormTabInfo } from "./overlayManagerForm";
import { Properties } from "../../../dialog";
import dialogStateService from "../../../../../services/dialogStateService";

export class SizeFactorPropertiesState implements Properties {
    objectNumSizeFactor: number | null;
    vectorNumSizeFactor: number | null;
    listVectorToChange: any[];
    listObjectToChange: any[];

    static getDefault(p: any): SizeFactorPropertiesState {
        let { props, selectedNodeInTree } = p;
        return {
            objectNumSizeFactor: null,
            vectorNumSizeFactor: null,
            listVectorToChange: [],
            listObjectToChange: [],
        }
    }
}

export class SizeFactorProperties extends SizeFactorPropertiesState {
    objectsPropertyType: any[];
    vectorPropertyType: any[];
    chooseViewports: { name: string, viewport: ViewportData };
    listToVectorTable: any[];
    listToObjectsTable: any[];
    listViewportOfOverlayManger: any[];

    static getDefault(p: any): SizeFactorProperties {
        let { props, selectedNodeInTree } = p;
        let stateDefaults = super.getDefault(p);
        let defaults: SizeFactorProperties = {
            ...stateDefaults,
            objectsPropertyType: [],
            vectorPropertyType: [],
            chooseViewports: null,
            listToVectorTable: getDataTable(props, selectedNodeInTree, true),
            listToObjectsTable: getDataTable(props, selectedNodeInTree, false),
            listViewportOfOverlayManger: getListViewportOfOverlayManager(selectedNodeInTree),

        }

        return defaults;
    }
};

const getDataTable = (props: any, selectedNodeInTree: TreeNodeModel, bVectorItems: boolean) => {
    let ESizePropertyType = getEnumDetailsList(MapCore.IMcOverlayManager.ESizePropertyType)

    let list = ESizePropertyType.map(e => {
        let factor = null;
        try { factor = selectedNodeInTree.nodeMcContent.GetItemSizeFactor(e.theEnum, null, bVectorItems) }
        catch { factor = null }
        return {
            name: e.name,
            factor: factor,
            enum: e.theEnum,
            code: e.code
        }
    })
    return list;
}


const getListViewportOfOverlayManager = (selectedNodeInTree: TreeNodeModel) => {
    let list: { name: string, viewport: ViewportData }[] = [];
    MapCoreData.viewportsData.forEach((v: ViewportData) => {
        let vpOverlayManager;
        runMapCoreSafely(() => { vpOverlayManager = v.viewport.GetOverlayManager(); }, "SizeFactor.resetlistViewportOfOverlayManger=> GetOverlayManager", true)

        if (vpOverlayManager === selectedNodeInTree.nodeMcContent) {
            let ObjectByName;
            runMapCoreSafely(() => { ObjectByName = generalService.getObjectName(v, "Viewport",) }, "SizeFactor.resetlistViewportOfOverlayManger=> getObjectName", true)
            list.push({ name: ObjectByName, viewport: v })
        }
    })
    return list;
}
export default function SizeFactor(props: { tabInfo: OverlayManagerFormTabInfo }) {
    let selectedNodeInTree: TreeNodeModel = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [overlayManager, setOverlayManager] = useState<MapCore.IMcOverlayManager>(selectedNodeInTree.nodeMcContent)
    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        selectedNodeInTree: false,
    })

    useEffect(() => {
        runCodeSafely(() => {
            if (!props.tabInfo.properties.sizeFactorProperties) {
                props.tabInfo.setInitialStatePropertiesCallback("sizeFactorProperties", null, SizeFactorPropertiesState.getDefault({ props, selectedNodeInTree }));
                props.tabInfo.setPropertiesCallback("sizeFactorProperties", null, SizeFactorProperties.getDefault({ props, selectedNodeInTree }));
                // check if may cause a bug using local state based on common state
                // getDataTable(true);
                // getDataTable(false);
                // resetlistViewportOfOverlayManger()
            }

        }, "SizeFactor.useEffect")
    }, [])

    useEffect(() => {
        if (isMountedUseEffect.selectedNodeInTree) {
            props.tabInfo.setInitialStatePropertiesCallback("sizeFactorProperties", null, SizeFactorPropertiesState.getDefault({ props, selectedNodeInTree }));
            props.tabInfo.setPropertiesCallback("sizeFactorProperties", null, SizeFactorProperties.getDefault({ props, selectedNodeInTree }));
        }
        else {
            setIsMountedUseEffect({ ...isMountedUseEffect, selectedNodeInTree: true })
        }
    }, [selectedNodeInTree])

    const saveData = (event: { target: { name: any; value: any; }; }) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("sizeFactorProperties", event.target.name, event.target.value);
            if (event.target.name in SizeFactorPropertiesState.getDefault({ props, selectedNodeInTree })) {
                props.tabInfo.setCurrStatePropertiesCallback("sizeFactorProperties", event.target.name, event.target.value)
            }
        }, "SizeFactor.saveData => onChange")
    }

    const changeInputFactor = (eventProps: any, event: any, bVectorItems: boolean) => {
        // return {
        //     name: e.name,
        //     factor: factor,
        //     enum: e.theEnum,
        //     code: e.code
        // }
        runCodeSafely(() => {
            let o = { enum: event.target.name, factor: event.value }
            if (o.factor) {
                if (bVectorItems) {
                    let updatedListToVectorTable = [...props.tabInfo.properties.sizeFactorProperties?.listToVectorTable];
                    updatedListToVectorTable[eventProps.rowIndex][eventProps.field] = event.value;
                    props.tabInfo.setPropertiesCallback("sizeFactorProperties", "listToVectorTable", updatedListToVectorTable)
                    props.tabInfo.setPropertiesCallback("sizeFactorProperties", "listVectorToChange", [...props.tabInfo.properties.sizeFactorProperties?.listVectorToChange, o])
                    props.tabInfo.setCurrStatePropertiesCallback("sizeFactorProperties", "listVectorToChange", [...props.tabInfo.properties.sizeFactorProperties?.listVectorToChange, o])
                }
                else {
                    let updatedListToObjectsTable = [...props.tabInfo.properties.sizeFactorProperties?.listToObjectsTable];
                    updatedListToObjectsTable[eventProps.rowIndex][eventProps.field] = event.value;
                    props.tabInfo.setPropertiesCallback("sizeFactorProperties", "listToObjectsTable", updatedListToObjectsTable)
                    props.tabInfo.setPropertiesCallback("sizeFactorProperties", "listObjectToChange", [...props.tabInfo.properties.sizeFactorProperties?.listObjectToChange, o])//listObjectToChange.push(o)
                    props.tabInfo.setCurrStatePropertiesCallback("sizeFactorProperties", "listObjectToChange", [...props.tabInfo.properties.sizeFactorProperties?.listObjectToChange, o])//listObjectToChange.push(o)
                }
            }
        }, "SizeFactor.changeInputFactor")
    }
    const changeVectorInputFactorEditor = (props: any) => {
        return (
            <InputNumber className="form__slim-input" name={props.rowData["code"].toString()} value={props.rowData[props.field]} onValueChange={(e) => {
                changeInputFactor(props, e, true)
            }}></InputNumber>
        );
    }
    const changeObjectInputFactorEditor = (props: any) => {
        return (
            <InputNumber className="form__slim-input" name={props.rowData["code"].toString()} value={props.rowData[props.field]} onValueChange={(e) => {
                changeInputFactor(props, e, false)
            }}></InputNumber>
        );
    }
    // const [listVectorToChange, setVectorListToChange] = useState<any[]>([])
    // const [listObjectToChange, setObjectListToChange] = useState<any[]>([])

    const SetVectorSelectedSizeFactor = (bVectorItems: boolean) => {
        runCodeSafely(() => {
            let enumValues = 0, number: any, PropertyType: any;
            if (bVectorItems) {
                number = props.tabInfo.properties.sizeFactorProperties?.vectorNumSizeFactor;
                PropertyType = props.tabInfo.properties.sizeFactorProperties?.vectorPropertyType
            }
            else {
                number = props.tabInfo.properties.sizeFactorProperties?.objectNumSizeFactor;
                PropertyType = props.tabInfo.properties.sizeFactorProperties?.objectsPropertyType
            }
            if (number != null && number != "") {
                PropertyType.map((element: any) => {
                    enumValues |= element.code;
                });
                let v = props.tabInfo.properties.sizeFactorProperties?.chooseViewports ? props.tabInfo.properties.sizeFactorProperties?.chooseViewports.viewport.viewport : null
                runMapCoreSafely(() => {
                    overlayManager.SetItemSizeFactors(enumValues, number, v, bVectorItems)
                }, "SizeFactor.SetVectorSelectedSizeFactor=> SetItemSizeFactors", true)
            }

            if (bVectorItems) {
                props.tabInfo.setPropertiesCallback("sizeFactorProperties", "listToVectorTable", getDataTable(props, selectedNodeInTree, bVectorItems));
                dialogStateService.applyDialogState(["sizeFactorProperties.vectorNumSizeFactor"]);
            }
            else {
                props.tabInfo.setPropertiesCallback("sizeFactorProperties", "listToObjectsTable", getDataTable(props, selectedNodeInTree, bVectorItems))
                dialogStateService.applyDialogState(["sizeFactorProperties.objectNumSizeFactor"]);
            }
        }, "SizeFactor.SetVectorSelectedSizeFactor")
    }
    const ApplySizeFactor = (bVectorItems: boolean) => {
        runCodeSafely(() => {
            let v = props.tabInfo.properties.sizeFactorProperties?.chooseViewports ? props.tabInfo.properties.sizeFactorProperties?.chooseViewports.viewport.viewport : null
            let list = bVectorItems ? props.tabInfo.properties.sizeFactorProperties?.listVectorToChange : props.tabInfo.properties.sizeFactorProperties?.listObjectToChange
            list.map((element: any) => {

                runMapCoreSafely(() => {
                    overlayManager.SetItemSizeFactors(parseInt(element.enum), element.factor, v, bVectorItems)
                }, "SizeFactor.ApplySizeFactor=> SetItemSizeFactors", true)
            });
            // getDataTable(bVectorItems) // !!!!!!!!!!!!!!!!!! check if needed
            bVectorItems ? dialogStateService.applyDialogState(["sizeFactorProperties.listVectorToChange"]) :
                dialogStateService.applyDialogState(["sizeFactorProperties.listObjectToChange"]);
        }, "SizeFactor.ApplySizeFactor")
    }
    return (
        <div style={{ display: "flex" }}>
            <div style={{ paddingRight: `${globalSizeFactor *1.5 * 0.25}vh` }}>
                <div>Objects </div>
                <DataTable className="overlay-manager__text-center-column" size="small" name="objectsPropertyType" selectionMode="checkbox" value={props.tabInfo.properties.sizeFactorProperties?.listToObjectsTable} selection={props.tabInfo.properties.sizeFactorProperties?.objectsPropertyType}
                    onSelectionChange={(e) => {
                        props.tabInfo.setPropertiesCallback("sizeFactorProperties", "objectsPropertyType", e.value);
                    }}>
                    <Column selectionMode="multiple"></Column>
                    <Column field="name" header="ESPT Type"></Column>
                    <Column field="factor" header="Factor" body={(rowData: any) => { return rowData.factor }} editor={changeObjectInputFactorEditor}></Column>
                </DataTable>
                <div className="overlay-manager__tables-button-container" >
                    <div><Button label="Apply" onClick={() => { ApplySizeFactor(false) }} /></div>
                    <div className="overlay-manager__om-operation">
                        <Button label="Set Selected Size Factor" onClick={() => SetVectorSelectedSizeFactor(false)}></Button>
                        <InputNumber className='form__narrow-input' name="objectNumSizeFactor" onValueChange={saveData} />
                    </div>
                </div>
            </div>
            <div style={{ paddingLeft: `${globalSizeFactor *1.5 * 0.25}vh`, paddingRight: `${globalSizeFactor *1.5 * 0.25}vh` }}>
                <div>Vector Items</div>
                <DataTable className="overlay-manager__text-center-column" size="small" name="vectorPropertyType" selectionMode="checkbox" value={props.tabInfo.properties.sizeFactorProperties?.listToVectorTable} selection={props.tabInfo.properties.sizeFactorProperties?.vectorPropertyType}
                    onSelectionChange={(e) => {
                        props.tabInfo.setPropertiesCallback("sizeFactorProperties", "vectorPropertyType", e.value);
                    }}>
                    <Column selectionMode="multiple"></Column>
                    <Column field="name" header="ESPT Type"></Column>
                    <Column field="factor" header="Factor" body={(rowData: any) => { return rowData.factor }} editor={changeVectorInputFactorEditor}></Column>
                </DataTable>
                <div className="overlay-manager__tables-button-container" >
                    <div><Button label="Apply" onClick={() => { ApplySizeFactor(true) }} /></div>
                    <div className="overlay-manager__om-operation">
                        <Button label="Set Selected Size Factor" onClick={() => SetVectorSelectedSizeFactor(true)} />
                        <InputNumber className='form__narrow-input' name="vectorNumSizeFactor" onValueChange={saveData} />
                    </div>
                </div>
            </div>
            <div style={{ paddingTop: `${globalSizeFactor * 2}vh` }}>
                <ListBox listStyle={{ maxHeight: `${globalSizeFactor * 56.5}vh`, minHeight: `${globalSizeFactor * 56.5}vh`, width: `${globalSizeFactor *1.5 * 8}vh` }} name="chooseViewports" options={props.tabInfo.properties.sizeFactorProperties?.listViewportOfOverlayManger} optionLabel="name" value={props.tabInfo.properties.sizeFactorProperties?.chooseViewports} onChange={saveData}></ListBox>
            </div>
        </div>
    )
}