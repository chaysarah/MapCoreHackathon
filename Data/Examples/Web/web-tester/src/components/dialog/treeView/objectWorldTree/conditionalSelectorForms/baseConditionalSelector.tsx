import { Fieldset } from "primereact/fieldset";
import { InputText } from "primereact/inputtext";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Button } from "primereact/button";

import { TypeToStringService } from 'mapcore-lib';
import BooleanConditionalSelector from "./booleanConditionalSelector";
import ScaleConditionalSelector from "./scaleConditionalSelector";
import ObjectConditionalSelector from "./objectConditionalSelector";
import ViewportConditionalSelector from "./viewportConditionalSelector";
import LocationConditionalSelector from "./locationSelector";
import TreeNodeModel, { objectWorldNodeType } from '../../../../shared/models/tree-node.model';
import InputMaxNumber from "../../../../shared/inputMaxNumber";
import { AppState } from "../../../../../redux/combineReducer";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { setObjectWorldTree, setTypeObjectWorldDialogSecond } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";

export default function BaseConditionalSelector(props: { conditionalSType?: any }) {
    const dispatch = useDispatch();
    let currentTreeNode = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [baseConditionalSelectorData, setBaseConditionalSelectorData] = useState({
        setFieldsValuesFunc: (): any => { },
        //Fields
        ID: currentTreeNode.nodeType == objectWorldNodeType.OVERLAY_MANAGER || currentTreeNode.nodeMcContent.GetID() == MapCore.UINT_MAX ? MapCore.UINT_MAX : currentTreeNode.nodeMcContent.GetID(),
        TypeValue: TypeToStringService.getSelectorTypeNameByTypeNumber(currentTreeNode.nodeType != objectWorldNodeType.OVERLAY_MANAGER ? currentTreeNode.nodeMcContent.GetConditionalSelectorType() : props.conditionalSType.CONDITIONAL_SELECTOR_TYPE),
    })

    const selectorTypesFormsMap = new Map([
        [MapCore.IMcBlockedConditionalSelector.CONDITIONAL_SELECTOR_TYPE, <div></div>],
        [MapCore.IMcLocationConditionalSelector.CONDITIONAL_SELECTOR_TYPE, <LocationConditionalSelector setFieldsValues={(func: () => any) => { setFieldsValueFunc(func) }} />],
        [MapCore.IMcObjectStateConditionalSelector.CONDITIONAL_SELECTOR_TYPE, <ObjectConditionalSelector setFieldsValues={(func: () => any) => { setFieldsValueFunc(func) }} />],
        [MapCore.IMcScaleConditionalSelector.CONDITIONAL_SELECTOR_TYPE, <ScaleConditionalSelector setFieldsValues={(func: () => any) => { setFieldsValueFunc(func) }} />],
        [MapCore.IMcViewportConditionalSelector.CONDITIONAL_SELECTOR_TYPE, <ViewportConditionalSelector setFieldsValues={(func: () => any) => { setFieldsValueFunc(func) }} />],
        [MapCore.IMcBooleanConditionalSelector.CONDITIONAL_SELECTOR_TYPE, <BooleanConditionalSelector setFieldsValues={(func: () => any) => { setFieldsValueFunc(func) }} />],
    ]);

    const saveID = (value: number) => {
        runCodeSafely(() => {
            setBaseConditionalSelectorData({ ...baseConditionalSelectorData, ID: value })
        }, "baseConditionalSelector.saveID => onChange")
    }

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.4 * globalSizeFactor;
        root.style.setProperty('--base-conditional-selector-dialog-width', `${pixelWidth}px`);
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            setBaseConditionalSelectorData({
                ...baseConditionalSelectorData,
                ID: currentTreeNode.nodeType == objectWorldNodeType.OVERLAY_MANAGER || currentTreeNode.nodeMcContent.GetID() == MapCore.UINT_MAX ? MapCore.UINT_MAX : currentTreeNode.nodeMcContent.GetID(),
                TypeValue: TypeToStringService.getSelectorTypeNameByTypeNumber(currentTreeNode.nodeType != objectWorldNodeType.OVERLAY_MANAGER ? currentTreeNode.nodeMcContent.GetConditionalSelectorType() : props.conditionalSType.CONDITIONAL_SELECTOR_TYPE),
            })
        }, 'baseConditionalSelector.useEffect => currentTreeNode');
    }, [currentTreeNode])

    const setFieldsValueFunc = (func: () => any) => {
        setBaseConditionalSelectorData({
            ...baseConditionalSelectorData,
            setFieldsValuesFunc: func,
        })
    }

    //Handle Funcs
    const handleCancelClick = () => {
        if (currentTreeNode.nodeType == objectWorldNodeType.OVERLAY_MANAGER) {
            dispatch(setTypeObjectWorldDialogSecond(undefined));
        }
        else {
            // dispatch(setIsFormOpen(false)) close external
        }
    }
    const handleOKClick = () => {
        if (currentTreeNode.nodeType == objectWorldNodeType.OVERLAY_MANAGER) {
            let createdSelector: MapCore.IMcConditionalSelector;
            let mcCurrentOM = currentTreeNode.nodeMcContent as MapCore.IMcOverlayManager;
            if (props.conditionalSType === MapCore.IMcBlockedConditionalSelector) {
                createdSelector = MapCore.IMcBlockedConditionalSelector.Create(mcCurrentOM);
            }
            else {
                createdSelector = baseConditionalSelectorData.setFieldsValuesFunc();
            }
            //set local ID
            createdSelector.SetID(parseInt(baseConditionalSelectorData.ID));
            //need for all types
            mcCurrentOM.SetConditionalSelectorLock(createdSelector, true);//ask orit : do we need it here? what it makes? - sm
            //render tree
            let tree: TreeNodeModel = objectWorldTreeService.buildTree()
            dispatch(setObjectWorldTree(tree))
            //close form
            dispatch(setTypeObjectWorldDialogSecond(undefined));
        }
        else {
            let noNeedParam = baseConditionalSelectorData.setFieldsValuesFunc();
            let mcConditionalS = currentTreeNode.nodeMcContent as MapCore.IMcConditionalSelector;
            mcConditionalS.SetID(baseConditionalSelectorData.ID);
        }
    }

    return <Fieldset className="form__column-fieldset form__space-around" legend="Conditional Selector Parameters">
        <div style={{ width: `${globalSizeFactor * 30}vh` }}>
            <div style={{ width: '100%', display: 'flex', flexDirection: 'row', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.3}vh` }}>
                <label htmlFor='ID'>ID: </label>
                <InputMaxNumber value={baseConditionalSelectorData.ID} maxValue={MapCore.UINT_MAX} getUpdatedMaxInput={saveID} id='ID' />
            </div>
            <div style={{ width: '100%', display: 'flex', flexDirection: 'row', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.3}vh` }}>
                <label htmlFor='Type'>Type : </label>
                <InputText disabled id='Type' value={baseConditionalSelectorData.TypeValue} />
            </div>
        </div>
        {selectorTypesFormsMap.get(currentTreeNode.nodeType != objectWorldNodeType.OVERLAY_MANAGER ? currentTreeNode.nodeMcContent.GetConditionalSelectorType() : props.conditionalSType.CONDITIONAL_SELECTOR_TYPE)}

        <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'flex-end', marginTop: `${globalSizeFactor * 1}vh` }}>
            <Button label='Cancel' onClick={handleCancelClick} />
            <Button label='OK' onClick={handleOKClick} />
        </div>
    </Fieldset>
}