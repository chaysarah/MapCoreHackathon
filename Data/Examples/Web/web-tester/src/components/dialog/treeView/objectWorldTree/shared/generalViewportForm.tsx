// External libraries
import { useState, useEffect, ReactElement, useMemo } from 'react';
import { useDispatch, useSelector } from 'react-redux';

// UI/component libraries
import { Button } from "primereact/button"
import { Checkbox } from "primereact/checkbox"
import { Dropdown } from "primereact/dropdown"
import { Fieldset } from "primereact/fieldset"
import { InputNumber } from "primereact/inputnumber"
import { InputText } from "primereact/inputtext"
import { ListBox, ListBoxChangeEvent } from "primereact/listbox"

// Project-specific imports
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { Properties } from "../../../dialog"
import ThreeStateCheckbox from "../../../../shared/threeStateCheckbox"
import TreeNodeModel from "../../../../shared/models/tree-node.model"
import { objectWorldNodeType } from "../../../../shared/models/tree-node.model"
import objectWorldTreeService from "../../../../../services/objectWorldTree.service"
import { AppState } from "../../../../../redux/combineReducer"
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler"
import generalService from "../../../../../services/general.service"
import dialogStateService from "../../../../../services/dialogStateService"

export class GeneralViewportFormPropertiesState implements Properties {
    isDetectibilityChecked: boolean;
    drawPriority: number;
    state: string;
    isEffectiveVisibilityChecked: boolean;
    visibilityDropDownValue: any;

    static getDefault(props: any): GeneralViewportFormPropertiesState {
        let EVisibility = getEnumDetailsList(MapCore.IMcConditionalSelector.EActionOptions);

        return {
            isDetectibilityChecked: props.selectedNodeInTree.nodeMcContent.GetDetectibility(null),
            drawPriority: props.selectedNodeInTree.nodeMcContent.GetDrawPriority(null),
            state: Array.from(props.selectedNodeInTree.nodeMcContent.GetState()).join(' '),
            isEffectiveVisibilityChecked: null,
            visibilityDropDownValue: getEnumValueDetails(props.selectedNodeInTree.nodeMcContent.GetVisibilityOption(), EVisibility),
        }
    }
}

export class GeneralViewportFormProperties extends GeneralViewportFormPropertiesState {
    viewportsOfOMArr: any[];
    viewportToSave: any;

    effectiveState: string;

    static getDefault(props: any): GeneralViewportFormProperties {
        let EVisibility = getEnumDetailsList(MapCore.IMcConditionalSelector.EActionOptions);

        let stateDefaults = super.getDefault(props);
        let defaults: GeneralViewportFormProperties = {
            ...stateDefaults,
            viewportsOfOMArr: getViewports(props.selectedNodeInTree, props.treeRedux),
            viewportToSave: null,
            effectiveState: '',
        }

        return defaults;
    }
};

const getViewports = (selectedNodeInTree: TreeNodeModel, treeRedux: TreeNodeModel) => {
    let overlay = selectedNodeInTree.nodeType == objectWorldNodeType.OBJECT ? (objectWorldTreeService.getParentByChildKey(treeRedux, selectedNodeInTree.key) as TreeNodeModel) : selectedNodeInTree;
    let mcVps = Array.from(objectWorldTreeService.getOMMCViewportsByOverlay(treeRedux, overlay))
    let vpList = mcVps.map(vp => { return { viewport: vp.viewport, label: generalService.getObjectName(vp, "Viewport") } });
    return vpList;
}

export class GeneralViewportFormInfo {
    properties: GeneralViewportFormProperties;
    setPropertiesCallback: (key: string, value: any) => void; // null key for update the object itself
    setCurrStatePropertiesCallback: (key: string, value: any) => void;
    setApplyCallBack: (Callback: () => void) => void;
};

export default function GeneralViewportForm(props: { info: GeneralViewportFormInfo }) {
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let selectedNodeInTree = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);

    const [enumDetails] = useState({
        EVisibility: getEnumDetailsList(MapCore.IMcConditionalSelector.EActionOptions),
    });

    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        treeRedux: false,
        selectedNodeInTree: false,
    })

    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.treeRedux) {
                props.info.setPropertiesCallback("viewportsOfOMArr", Array.from(objectWorldTreeService.getOMMCViewportsByOverlay(treeRedux, selectedNodeInTree)).map(vp => { return { viewport: vp.viewport, label: generalService.getObjectName(vp, "Viewport") } }))
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, treeRedux: true })
            }
        }, 'GeneralViewportForm.useEffect => treeRedux');
    }, [treeRedux])

    // useEffect(() => {
    //     runCodeSafely(() => {
    //         if (isMountedUseEffect.selectedNodeInTree) {
    //             props.info.setPropertiesCallback(null, GeneralViewportFormProperties.getDefault({ treeRedux, selectedNodeInTree }))
    //         }
    //         else {
    //             setIsMountedUseEffect({ ...isMountedUseEffect, selectedNodeInTree: true })
    //         }
    //     }, 'GeneralViewportForm.useEffect => selectedNodeInTree');
    // }, [selectedNodeInTree])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.info.setPropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value)
            if (event.target.name in GeneralViewportFormPropertiesState.getDefault({ treeRedux, selectedNodeInTree })) {
                props.info.setCurrStatePropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value)
            }
        }, "GeneralViewportForm.saveData => onChange")
    }
    //Handle Functions
    const handleClearSelectedVps = () => {
        runCodeSafely(() => {
            let updatedGeneralViewportFormProperties: GeneralViewportFormProperties = {
                ...props.info.properties,
                visibilityDropDownValue: getEnumValueDetails(selectedNodeInTree.nodeMcContent.GetVisibilityOption(), enumDetails.EVisibility),
                drawPriority: selectedNodeInTree.nodeMcContent.GetDrawPriority(null),
                isEffectiveVisibilityChecked: null,
                isDetectibilityChecked: selectedNodeInTree.nodeMcContent.GetDetectibility(null),
                state: Array.from(selectedNodeInTree.nodeMcContent.GetState()).join(' '),
                viewportToSave: null,
            }
            props.info.setPropertiesCallback(null, updatedGeneralViewportFormProperties);
            props.info.setCurrStatePropertiesCallback(null, updatedGeneralViewportFormProperties)
            dialogStateService.applyDialogState(["generalProperties.generalViewportFormPropertiesState"]);
        }, 'GeneralViewportForm => handleClearSelectedVps');
    }
    const handleVpToSaveChange = (e: ListBoxChangeEvent) => {
        runCodeSafely(() => {
            if (e.value?.viewport) {
                let mcCurrentNode = selectedNodeInTree.nodeMcContent;
                let cDetectibility = mcCurrentNode.GetDetectibility(e.value?.viewport)
                let cPriority = mcCurrentNode.GetDrawPriority(e.value?.viewport);
                let x = mcCurrentNode.GetVisibilityOption(e.value?.viewport)
                let cVisibility = getEnumValueDetails(mcCurrentNode.GetVisibilityOption(e.value?.viewport), enumDetails.EVisibility);
                let isEffectiveVisibilityInViewport = mcCurrentNode.GetEffectiveVisibilityInViewport(e.value?.viewport);
                let stateUint8Array = mcCurrentNode.GetState(e.value.viewport);
                let stringState = Array.from(stateUint8Array).join(' ');
                let stringEffectiveState = '';
                if (selectedNodeInTree.nodeType == objectWorldNodeType.OBJECT) {
                    let effectiveStatesBuffer = mcCurrentNode.GetEffectiveState(e.value?.viewport);
                    stringEffectiveState = Array.from(effectiveStatesBuffer).join(' ');
                }

                let updatedGeneralViewportFormProperties: GeneralViewportFormProperties = {
                    ...props.info.properties,
                    viewportToSave: e.value,
                    visibilityDropDownValue: cVisibility,
                    isDetectibilityChecked: cDetectibility,
                    drawPriority: cPriority,
                    isEffectiveVisibilityChecked: isEffectiveVisibilityInViewport,
                    state: stringState,
                    effectiveState: stringEffectiveState != '0' ? stringEffectiveState : '',
                }
                props.info.setPropertiesCallback(null, updatedGeneralViewportFormProperties);
                props.info.setCurrStatePropertiesCallback(null, updatedGeneralViewportFormProperties)
                dialogStateService.applyDialogState(["generalProperties.generalViewportFormPropertiesState"]);
            }
        }, "objects.handleVpToSaveChange");
    }
    const applyDetectibility = () => {
        runMapCoreSafely(() => {
            let mcOverlay = selectedNodeInTree.nodeMcContent as MapCore.IMcOverlay;
            props.info.properties?.viewportToSave ? mcOverlay.SetDetectibility(props.info.properties?.isDetectibilityChecked, props.info.properties?.viewportToSave.viewport)
                : mcOverlay.SetDetectibility(props.info.properties?.isDetectibilityChecked);
            dialogStateService.applyDialogState(["generalProperties.generalViewportFormPropertiesState.isDetectibilityChecked"]);
        }, 'GeneralViewportForm.applyDetectibility => IMcOverlay.SetDetectibility', true)
    }
    const applyDrawPriority = () => {
        runMapCoreSafely(() => {
            let mcOverlay = selectedNodeInTree.nodeMcContent;
            mcOverlay.SetDrawPriority(props.info.properties?.drawPriority, props.info.properties?.viewportToSave ? props.info.properties?.viewportToSave.viewport : null);
            dialogStateService.applyDialogState(["generalProperties.generalViewportFormPropertiesState.drawPriority"]);
        }, 'GeneralViewportForm.applyDrawPriority => IMcOverlay.SetDrawPriority', true)
    }
    const applyState = () => {
        runMapCoreSafely(() => {
            let mcOverlay = selectedNodeInTree.nodeMcContent as MapCore.IMcOverlay;
            let state = null;
            if (props.info.properties?.state) {
                let numericArrState = props.info.properties?.state.split(' ').map(Number);
                let uint8ArrState = Uint8Array.from(numericArrState);
                state = uint8ArrState.length == 1 ? uint8ArrState[0] : uint8ArrState;
            }
            let vp = props.info.properties?.viewportToSave ? props.info.properties?.viewportToSave?.viewport : null;
            mcOverlay.SetState(state, vp);
            dialogStateService.applyDialogState(["generalProperties.generalViewportFormPropertiesState.state"]);
        }, 'GeneralViewportForm.applyState => IMcOverlay.SetState', true)
    }
    const applyVisibility = () => {
        runMapCoreSafely(() => {
            let mcOverlay = selectedNodeInTree.nodeMcContent;
            let vp = props.info.properties?.viewportToSave ? props.info.properties?.viewportToSave.viewport : null;
            mcOverlay.SetVisibilityOption(props.info.properties?.visibilityDropDownValue?.theEnum, vp);
            dialogStateService.applyDialogState(["generalProperties.generalViewportFormPropertiesState.visibilityDropDownValue"]);
        }, 'GeneralViewportForm.applyVisibility => IMcOverlay.SetVisibilityOption', true)
    }
    const getGeneralFieldset = () => {
        return <Fieldset className="form__space-around">
            <div style={{ width: '55%', display: 'flex', flexDirection: 'column' }}>
                <span style={{ display: 'flex', flexDirection: 'row' }}>
                    <Button label='Apply' onClick={applyDetectibility} />
                    <div style={{ paddingLeft: `${globalSizeFactor * 1}vh`, marginTop: `${globalSizeFactor * 0.75}vh` }} >
                        <Checkbox name='isDetectibilityChecked' inputId="isDetectibilityChecked" onChange={saveData} checked={props.info.properties?.isDetectibilityChecked} />
                        <label htmlFor="isDetectibilityChecked" className="ml-2">Detectibility</label>
                    </div>
                </span>
                <span style={{ display: 'flex', justifyContent: 'space-between', width: `${globalSizeFactor * 1.5 * 18}vh` }}>
                    <Button label='Apply' onClick={applyDrawPriority} />
                    <div style={{ padding: `${globalSizeFactor * 0.3}vh`, display: 'flex', justifyContent: 'space-between', width: `${globalSizeFactor * 1.5 * 15}vh` }}>
                        <label style={{ marginTop: `${globalSizeFactor * 0.4}vh`, textAlign: 'left' }} htmlFor='drawPriority'>Draw Priority: </label>
                        <InputNumber style={{ width: `${globalSizeFactor * 1.5 * 10}vh` }} tooltipOptions={{ position: 'top' }} tooltip="Between -32768 to 32767" id='drawPriority' value={props.info.properties?.drawPriority ?? null} name='drawPriority' onValueChange={saveData} />
                    </div>
                </span>
                <span style={{ display: 'flex', justifyContent: 'space-between', width: `${globalSizeFactor * 1.5 * 18}vh` }}>
                    <Button label='Apply' onClick={applyState} />
                    <div style={{ padding: `${globalSizeFactor * 0.3}vh`, display: 'flex', justifyContent: 'space-between', width: `${globalSizeFactor * 1.5 * 15}vh` }}>
                        <label style={{ marginTop: `${globalSizeFactor * 0.4}vh`, textAlign: 'left' }} htmlFor='state'>State: </label>
                        <InputText style={{ width: `${globalSizeFactor * 1.5 * 10}vh` }} id='state' value={props.info.properties?.state ?? ""} name='state' onChange={saveData} />
                    </div>
                </span>
                <span style={{ display: 'flex', justifyContent: 'space-between', width: `${globalSizeFactor * 1.5 * 18}vh` }}>
                    <Button label='Apply' onClick={applyVisibility} />
                    <div style={{ padding: `${globalSizeFactor * 0.3}vh`, display: 'flex', justifyContent: 'space-between', width: `${globalSizeFactor * 1.5 * 15}vh` }}>
                        <label style={{ marginTop: `${globalSizeFactor * 0.4}vh`, textAlign: 'left' }} htmlFor='visibility'>Visibility: </label>
                        <Dropdown style={{ width: `${globalSizeFactor * 1.5 * 10}vh` }} id="visibilityDropDown" name='visibilityDropDownValue' value={props.info.properties?.visibilityDropDownValue ?? null} onChange={saveData} options={enumDetails.EVisibility} optionLabel="name" />
                    </div>
                </span>
                <div style={{ display: 'flex', flexDirection: 'column', paddingLeft: `${globalSizeFactor * 1.5 * 7.5}vh` }}>
                    <span style={{ paddingTop: `${globalSizeFactor * 1.5}vh`, display: 'flex', marginLeft: `${globalSizeFactor * 1.5 * 0.5}vh` }}>
                        <ThreeStateCheckbox value={props.info.properties?.isEffectiveVisibilityChecked} disabled={true} />
                        <label>Effective visibility in viewport</label>
                    </span>
                    {selectedNodeInTree.nodeType == objectWorldNodeType.OBJECT &&
                        <span style={{ display: 'flex', flexDirection: 'column', padding: `${globalSizeFactor * 1}vh` }}>
                            <label style={{ color: 'grey' }} htmlFor='effectiveState'>Effective state in selected viewport: </label>
                            <InputText disabled style={{ width: `${globalSizeFactor * 1.5 * 10}vh` }} id='effectiveState' value={props.info.properties?.effectiveState ?? ""} />
                        </span>
                    }
                </div>
            </div>
            <div style={{ width: '45%' }}>
                Viewports (Clear selection to set for all viewports) :
                <ListBox listStyle={{ minHeight: `${globalSizeFactor * 15}vh`, maxHeight: `${globalSizeFactor * 15}vh` }} name='viewportToSave' value={props.info.properties?.viewportToSave} onChange={handleVpToSaveChange} optionLabel='label' options={props.info.properties?.viewportsOfOMArr} />
                <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
                    <Button label='Clear' onClick={handleClearSelectedVps} />
                </div>
            </div>
        </Fieldset >
    }


    return (
        <span>
            {getGeneralFieldset()}
        </span>
    )

}