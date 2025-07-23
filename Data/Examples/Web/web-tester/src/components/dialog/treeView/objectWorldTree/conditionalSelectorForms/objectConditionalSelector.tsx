import { useEffect, useState } from "react";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import { objectWorldNodeType } from '../../../../shared/models/tree-node.model';
import { InputNumber } from "primereact/inputnumber";


export default function ObjectConditionalSelector(props: { setFieldsValues: (func: () => void) => void }) {
    let currentTreeNode = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);

    const [objectConditionalSelectorData, setObjectConditionalSelectorData] = useState({
        objectState: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? 0 : currentTreeNode.nodeMcContent.GetObjectState(),
    })

    const saveData = (event: any) => {
        runCodeSafely(() => {
            setObjectConditionalSelectorData({ ...objectConditionalSelectorData, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
        }, "objectConditionalSelector.saveData => onChange")
    }

    useEffect(() => {
        runCodeSafely(() => {
            setObjectConditionalSelectorData({
                ...objectConditionalSelectorData,
                objectState: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? 0 : currentTreeNode.nodeMcContent.GetObjectState(),
            })
        }, 'objectConditionalSelector.useEffect');
    }, [currentTreeNode])
    useEffect(() => {
        props.setFieldsValues(handleOKClick);
    }, [objectConditionalSelectorData])

    const handleOKClick = () => {
        let returnedValue: MapCore.IMcObjectStateConditionalSelector | string = '';
        runCodeSafely(() => {
            if (currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER) {
                runMapCoreSafely(() => {
                    let mcCurrentOM = currentTreeNode.nodeMcContent as MapCore.IMcOverlayManager;
                    returnedValue = MapCore.IMcObjectStateConditionalSelector.Create(mcCurrentOM, objectConditionalSelectorData.objectState);
                }, 'objectConditionalSelector.handleOKClick => IMcObjectStateConditionalSelector.Create', true)
            }
            else {
                let mcCurrentSelector = currentTreeNode.nodeMcContent as MapCore.IMcObjectStateConditionalSelector;
                runMapCoreSafely(() => {
                    mcCurrentSelector.SetObjectState(objectConditionalSelectorData.objectState);
                }, 'objectConditionalSelector.handleOKClick => IMcObjectStateConditionalSelector.SetObjectState', true)
            }
        }, 'objectConditionalSelector.handleOKClick')
        return returnedValue;
    }

    return <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
        <label htmlFor='objectState'>Object state :</label>
        <InputNumber id='objectState' value={objectConditionalSelectorData.objectState} name='objectState' onValueChange={saveData} />
    </div>

}