import { useEffect, useState } from "react";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import { objectWorldNodeType } from '../../../../shared/models/tree-node.model';
import { InputText } from "primereact/inputtext";
import CancelScaleMode from "../shared/cancelScaleMode";
import InputMaxNumber from "../../../../shared/inputMaxNumber";


export default function ScaleConditionalSelector(props: { setFieldsValues: (func: () => void) => void }) {
    let currentTreeNode = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [scaleConditionalSelectorData, setScaleConditionalSelectorData] = useState({
        minScale: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? '0' : `${currentTreeNode.nodeMcContent.GetMinScale()}`,
        maxScale: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? MapCore.FLT_MAX : currentTreeNode.nodeMcContent.GetMaxScale(),
        scaleMode: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? 0 : currentTreeNode.nodeMcContent.GetCancelScaleMode(),
        scaleModeResult: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? 0 : currentTreeNode.nodeMcContent.GetCancelScaleModeResult(),
    })

    const saveData = (event: any) => {
        runCodeSafely(() => {
            setScaleConditionalSelectorData({ ...scaleConditionalSelectorData, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
        }, "scaleConditionalSelector.saveData => onChange")
    }
    const saveUpdatedMaxScaleInput = (value: number) => {
        runCodeSafely(() => {
            setScaleConditionalSelectorData({ ...scaleConditionalSelectorData, maxScale: value })
        }, "scaleConditionalSelector.saveData => saveUpdatedMaxScaleInput")
    }

    useEffect(() => {
        runCodeSafely(() => {
            setScaleConditionalSelectorData({
                ...scaleConditionalSelectorData,
                minScale: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? '0' : `${currentTreeNode.nodeMcContent.GetMinScale()}`,
                maxScale: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? MapCore.FLT_MAX : currentTreeNode.nodeMcContent.GetMaxScale(),
                scaleMode: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? 0 : currentTreeNode.nodeMcContent.GetCancelScaleMode(),
                scaleModeResult: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? 0 : currentTreeNode.nodeMcContent.GetCancelScaleModeResult(),
            })
        }, 'scaleConditionalSelector.useEffect => currentTreeNode');
    }, [currentTreeNode])
    useEffect(() => {
        props.setFieldsValues(handleOKClick);
    }, [scaleConditionalSelectorData])

    const handleOKClick = () => {
        let returnedValue: MapCore.IMcScaleConditionalSelector | string = '';
        runCodeSafely(() => {
            if (currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER) {
                let mcCurrentOM = currentTreeNode.nodeMcContent as MapCore.IMcOverlayManager;
                let finalMaxScale = scaleConditionalSelectorData.maxScale;
                let finalMinScale = parseInt(scaleConditionalSelectorData.minScale) ?
                    parseInt(scaleConditionalSelectorData.minScale) : 0;
                runMapCoreSafely(() => {
                    returnedValue = MapCore.IMcScaleConditionalSelector.Create(mcCurrentOM,
                        finalMinScale,
                        finalMaxScale,
                        scaleConditionalSelectorData.scaleMode,
                        scaleConditionalSelectorData.scaleModeResult
                    );
                }, 'scaleConditionalSelector.handleOKClick => IMcScaleConditionalSelector.Create', true)
            }
            else {
                runMapCoreSafely(() => {
                    let mcCurrentSelector = currentTreeNode.nodeMcContent as MapCore.IMcScaleConditionalSelector;
                    parseInt(scaleConditionalSelectorData.minScale) && mcCurrentSelector.SetMinScale(parseInt(scaleConditionalSelectorData.minScale))
                    mcCurrentSelector.SetMaxScale(scaleConditionalSelectorData.maxScale)
                    mcCurrentSelector.SetCancelScaleMode(scaleConditionalSelectorData.scaleMode)
                    mcCurrentSelector.SetCancelScaleModeResult(scaleConditionalSelectorData.scaleModeResult)
                }, 'scaleConditionalSelector.handleOKClick => SetCancelScaleMode,SetCancelScaleModeResult', true)
            }
        }, 'scaleConditionalSelector.handleOKClick')
        return returnedValue;
    }

    return <div>
        <div style={{ width: '100%', display: 'flex', flexDirection: 'row', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.3}vh` }}>
            <label htmlFor='minScale'>Min Scale: </label>
            <InputText id='minScale' value={scaleConditionalSelectorData.minScale} name='minScale' onChange={saveData} />
        </div>
        <div style={{ width: '100%', display: 'flex', flexDirection: 'row', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.3}vh` }}>
            <label htmlFor='maxScale'>Max Scale: </label>
            {/* <InputText id='maxScale' value={scaleConditionalSelectorData.maxScale} name='maxScale' onChange={saveData} /> */}
            <InputMaxNumber value={scaleConditionalSelectorData.maxScale} maxValue={MapCore.FLT_MAX} getUpdatedMaxInput={saveUpdatedMaxScaleInput} id='maxScale' />
        </div>
        <CancelScaleMode cancelScaleModeNum={scaleConditionalSelectorData.scaleMode} label={'Cancel Scale Mode'} sendCheckboxesArr={(checkboxesArr: number) => {
            setScaleConditionalSelectorData({ ...scaleConditionalSelectorData, scaleMode: checkboxesArr })
        }} />
        <CancelScaleMode cancelScaleModeNum={scaleConditionalSelectorData.scaleModeResult} label={'Cancel Scale Mode Result'} sendCheckboxesArr={(checkboxesArr: number) => {
            setScaleConditionalSelectorData({ ...scaleConditionalSelectorData, scaleModeResult: checkboxesArr })
        }} />
    </div>

}