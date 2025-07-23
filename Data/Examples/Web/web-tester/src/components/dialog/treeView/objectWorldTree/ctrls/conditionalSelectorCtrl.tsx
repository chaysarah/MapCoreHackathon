// External libraries
import { useState, useEffect } from 'react';
import { useSelector } from 'react-redux';

// UI/component libraries
import { Fieldset } from "primereact/fieldset";
import { ListBox } from "primereact/listbox";
import { Dropdown } from "primereact/dropdown";
import { Checkbox } from "primereact/checkbox";

// Project-specific imports (ordered from fewest to most slashes)
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { Properties } from "../../../dialog";
import TreeNodeModel, { objectWorldNodeType } from "../../../../shared/models/tree-node.model";
import { AppState } from "../../../../../redux/combineReducer";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import dialogStateService from "../../../../../services/dialogStateService";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";

export class ConditionalSelectorCtrlPropertiesState implements Properties {
    isActionOnResult: boolean;
    selectedSelector: TreeNodeModel;

    static getDefault(p: any): ConditionalSelectorCtrlPropertiesState {
        let pSelector: { Value?: MapCore.IMcConditionalSelector } = {};
        let actionOnResult: { Value?: boolean } = {};
        let mcNode = p.selectedNodeInTree.nodeMcContent;
        runMapCoreSafely(() => {
            mcNode.GetConditionalSelector(MapCore.IMcConditionalSelector.EActionType.EAT_VISIBILITY, actionOnResult, pSelector);
        }, "ConditionalSelectorCtrlPropertiesState/getDefault", true)
        let testerSelectors = (objectWorldTreeService.getNodeByKey(p.treeRedux, p.selectedNodeInTree.key.substring(0, 3)) as TreeNodeModel).children.filter(node => node.nodeType == objectWorldNodeType.CONDITIONAL_SELECTOR)

        return {
            isActionOnResult: actionOnResult.Value,
            selectedSelector: testerSelectors.find(selector => selector.nodeMcContent == pSelector.Value) ?? null,
        }
    }
}

export class ConditionalSelectorCtrlProperties extends ConditionalSelectorCtrlPropertiesState {
    allConditionalSelectors: TreeNodeModel[];
    actionTypeDropDownValue: MapCore.IMcConditionalSelector.EActionType;

    static getDefault(p: any): ConditionalSelectorCtrlProperties {
        let EActionType = getEnumDetailsList(MapCore.IMcConditionalSelector.EActionType);
        let testerSelectors = (objectWorldTreeService.getNodeByKey(p.treeRedux, p.selectedNodeInTree.key.substring(0, 3)) as TreeNodeModel).children.filter(node => node.nodeType == objectWorldNodeType.CONDITIONAL_SELECTOR)

        let stateDefaults = super.getDefault(p);
        let defaults: ConditionalSelectorCtrlProperties = {
            ...stateDefaults,
            allConditionalSelectors: testerSelectors,
            actionTypeDropDownValue: getEnumValueDetails(MapCore.IMcConditionalSelector.EActionType.EAT_VISIBILITY, EActionType),
        }
        return defaults;
    }
}

export class ConditionalSelectorCtrlInfo {
    properties: ConditionalSelectorCtrlProperties;
    setPropertiesCallback: (key: string, value: any) => void; // null key for update the object itself
    setCurrStatePropertiesCallback: (key: string, value: any) => void;
    setApplyCallBack: (Callback: () => void) => void;
    ctrlHeight: number;
    dialogPath: string;
};

export default function ConditionalSelectorCtrl(props: { info: ConditionalSelectorCtrlInfo }) {
    let selectedNodeInTree = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [enumDetails] = useState({
        EActionType: getEnumDetailsList(MapCore.IMcConditionalSelector.EActionType),
    });


    // useEffect(() => {
    //     setConditionalSelectorCtrlData({
    //         ...conditionalSelectorCtrlData,
    //         actionTypeDropDownValue: getEnumValueDetails(MapCore.IMcConditionalSelector.EActionType.EAT_VISIBILITY, enumDetails.EActionType),
    //         ...getMcDefaultFieldsValues()
    //     })
    // }, [currentTreeNode])
    // useEffect(() => {
    //     setConditionalSelectorCtrlData({
    //         ...conditionalSelectorCtrlData,
    //         ...getMcDefaultFieldsValues()
    //     })
    // }, [treeRedux])
    useEffect(() => {
        runCodeSafely(() => {
            props.info.setApplyCallBack(applyAll);
        }, 'conditionalSelectorCtrl.useEffect => conditionalSelectorCtrlData');
    }, [props.info.properties])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            // setConditionalSelectorCtrlData({ ...conditionalSelectorCtrlData, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
            props.info.setPropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value)
            if (event.target.name in ConditionalSelectorCtrlPropertiesState.getDefault({ selectedNodeInTree, treeRedux })) {
                dialogStateService.setDialogState([props.info.dialogPath, event.target.name].join("."),
                    event.target.type === "checkbox" ? event.target.checked : event.target.value);
            }
        }, "ConditionalSelectorCtrl.saveData => onChange")
    }

    const applyAll = () => {
        props.info.properties.selectedSelector ?
            selectedNodeInTree.nodeMcContent.SetConditionalSelector(MapCore.IMcConditionalSelector.EActionType.EAT_VISIBILITY,
                props.info.properties.isActionOnResult,
                props.info.properties.selectedSelector.nodeMcContent)
            : selectedNodeInTree.nodeMcContent.SetConditionalSelector(MapCore.IMcConditionalSelector.EActionType.EAT_VISIBILITY, props.info.properties.isActionOnResult, null);

        dialogStateService.applyDialogState([props.info.dialogPath]);
    }

    const getConditionalSelelctor = () => {
        return <Fieldset style={{ height: `${globalSizeFactor * props.info.ctrlHeight}vh` }} className="form__column-fieldset form__space-around" legend="Conditional Selector">
            <div style={{ padding: `${globalSizeFactor * 0.3}vh` }}>
                <Checkbox name='isActionOnResult' inputId="isActionOnResult" onChange={saveData} checked={props.info.properties?.isActionOnResult} />
                <label htmlFor="isActionOnResult" className="ml-2">Action On Result</label>
            </div>
            <div style={{ padding: `${globalSizeFactor * 0.3}vh`, width: '100%', display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
                <label htmlFor='actionTypeDropDown'>Action Type: </label>
                <Dropdown disabled id="actionTypeDropDown" name='actionTypeDropDownValue' value={props.info.properties?.actionTypeDropDownValue ?? null} onChange={saveData} options={enumDetails.EActionType} optionLabel="name" />
            </div>
            <ListBox emptyMessage={() => { return <div></div> }} name="selectedSelector" listStyle={{ minHeight: `${(props.info.ctrlHeight - 14) * globalSizeFactor}vh`, maxHeight: `${props.info.ctrlHeight - 14}vh` }}
                style={{ width: '100%' }} value={props.info.properties?.selectedSelector} onChange={saveData} options={props.info.properties?.allConditionalSelectors} optionLabel="label" />
        </Fieldset>
    }

    return (
        <div>
            {getConditionalSelelctor()}
        </div>
    )

}