import { useEffect, useState } from "react";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { Dropdown } from "primereact/dropdown";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import TreeNodeModel, { objectWorldNodeType } from '../../../../shared/models/tree-node.model';
import { ListBox } from "primereact/listbox";


export default function BooleanConditionalSelector(props: { setFieldsValues: (func: () => void) => void }) {
    let currentTreeNode = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const getSelectors = () => {
        let selectors: TreeNodeModel[];
        runCodeSafely(() => {
            selectors = currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ?
                currentTreeNode.children.filter(node => node.nodeType == objectWorldNodeType.CONDITIONAL_SELECTOR) :
                (objectWorldTreeService.getNodeByKey(treeRedux, currentTreeNode.key.substring(0, 3)) as TreeNodeModel).children.filter(node => node.nodeType == objectWorldNodeType.CONDITIONAL_SELECTOR);
        }, 'booleanConditionalSelector.getSelectors')
        return selectors;
    }

    const getOperation = () => {
        let operationEnum: { name: string, code: number, theEnum: any };
        runCodeSafely(() => {
            let operation = currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ?
                MapCore.IMcBooleanConditionalSelector.EBooleanOp.EB_OR :
                currentTreeNode.nodeMcContent.GetBooleanOperation();
            operationEnum = getEnumValueDetails(operation, enumDetails.EBooleanOp);
        }, 'booleanConditionalSelector.getOperation')
        return operationEnum;
    }
    const getSelectedSelectors = () => {
        let selectors: TreeNodeModel[] = [];
        runCodeSafely(() => {
            if (currentTreeNode.nodeType !== objectWorldNodeType.OVERLAY_MANAGER) {
                let allSelectors = getSelectors();
                let selectedSelectors = currentTreeNode.nodeMcContent.GetListOfSelectors();
                selectedSelectors.forEach((selectedSelector: TreeNodeModel) => {
                    let testerSelecetedSelector = allSelectors.find(selector => selector.nodeMcContent == selectedSelector)
                    selectors = [...selectors, testerSelecetedSelector];
                });
            }
        }, 'booleanConditionalSelector.getSelectedSelectors')
        return selectors;
    }

    const [enumDetails] = useState({
        EBooleanOp: getEnumDetailsList(MapCore.IMcBooleanConditionalSelector.EBooleanOp),
    });
    const [booleanConditionalSelectorData, setBooleanConditionalSelectorData] = useState({
        operation: getOperation(),
        allSelectors: getSelectors(),
        selectedSelectors: getSelectedSelectors(),
    })

    const saveData = (event: any) => {
        runCodeSafely(() => {
            setBooleanConditionalSelectorData({ ...booleanConditionalSelectorData, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
        }, "booleanConditionalSelector.saveData => onChange")
    }

    useEffect(() => {
        runCodeSafely(() => {
            setBooleanConditionalSelectorData({
                ...booleanConditionalSelectorData,
                operation: getOperation(),
                allSelectors: getSelectors(),
                selectedSelectors: getSelectedSelectors(),
            })
        }, 'booleanConditionalSelector.useEffect');
    }, [currentTreeNode])
    useEffect(() => {
        runCodeSafely(() => {
            setBooleanConditionalSelectorData({
                ...booleanConditionalSelectorData,
                allSelectors: getSelectors(),
                selectedSelectors: getSelectedSelectors(),
            })
        }, 'booleanConditionalSelector.useEffect');
    }, [treeRedux])
    useEffect(() => {
        props.setFieldsValues(handleOKClick);
    }, [booleanConditionalSelectorData])

    const handleOKClick = () => {
        let returnedValue: MapCore.IMcBooleanConditionalSelector | string = '';
        runCodeSafely(() => {
            let mcSelectedSelectors: MapCore.IMcConditionalSelector[] = booleanConditionalSelectorData.selectedSelectors.map(sel => sel.nodeMcContent as MapCore.IMcConditionalSelector)
            let operation = booleanConditionalSelectorData.operation!.theEnum as MapCore.IMcBooleanConditionalSelector.EBooleanOp;
            if (currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER) {
                let mcCurrentOM = currentTreeNode.nodeMcContent as MapCore.IMcOverlayManager;
                returnedValue = MapCore.IMcBooleanConditionalSelector.Create(mcCurrentOM, mcSelectedSelectors, operation);
            }
            else {
                let mcCurrentSelector = currentTreeNode.nodeMcContent as MapCore.IMcBooleanConditionalSelector;
                mcCurrentSelector.SetBooleanOperation(operation)
                mcCurrentSelector.SetListOfSelectors(mcSelectedSelectors);
            }
        }, 'booleanConditionalSelector.handleOKClick')
        return returnedValue;
    }

    return <div>
        <div style={{ width: '100%', display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
            <label htmlFor='operation'>Operation :</label>
            <Dropdown style={{ margin: `${globalSizeFactor * 1}vh` }} id='operation' value={booleanConditionalSelectorData.operation} name='operation' onChange={saveData} options={enumDetails.EBooleanOp} optionLabel="name" />
        </div>
        <ListBox emptyMessage={() => { return <div></div> }} multiple listStyle={{ minHeight: `${globalSizeFactor * 20}vh`, maxHeight: `${globalSizeFactor * 20}vh`, }} style={{ width: '100%' }} name='selectedSelectors' optionLabel='label' value={booleanConditionalSelectorData.selectedSelectors} onChange={saveData} options={booleanConditionalSelectorData.allSelectors} />
    </div>

}