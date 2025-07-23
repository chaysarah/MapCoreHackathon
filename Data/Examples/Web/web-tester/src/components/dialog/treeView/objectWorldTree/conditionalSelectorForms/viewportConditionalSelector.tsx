import { useEffect, useState } from "react";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import { objectWorldNodeType } from '../../../../shared/models/tree-node.model';
import { getEnumDetailsList, getEnumValueDetails, getBitFieldByEnumArr } from 'mapcore-lib';
import { ListBox } from "primereact/listbox";
import { Checkbox } from "primereact/checkbox";
import { InputTextarea } from "primereact/inputtextarea";

export default function ViewportConditionalSelector(props: { setFieldsValues: (func: () => void) => void }) {
    let currentTreeNode = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [enumDetails] = useState({
        EViewportCoordinateSystem: getEnumDetailsList(MapCore.IMcViewportConditionalSelector.EViewportCoordinateSystem),
        EViewportTypeFlags: getEnumDetailsList(MapCore.IMcViewportConditionalSelector.EViewportTypeFlags),
    });
    const getIDInclusive = (stringOrChecbox: string) => {
        let pbIDsInclusive: { Value?: any } = {};
        let mcCuurentSelector = currentTreeNode.nodeMcContent as MapCore.IMcViewportConditionalSelector;
        let IDsBuffer = mcCuurentSelector.GetSpecificViewports(pbIDsInclusive);
        if (stringOrChecbox == 'IDsString') {
            let stringIDs = Array.from(IDsBuffer).join(' ');
            return stringIDs;
        }
        else {
            return pbIDsInclusive.Value;
        }
    }
    const getSelectedTypes = () => {
        let typeEnums = getEnumValueDetails(currentTreeNode.nodeMcContent.GetViewportTypeBitField(), enumDetails.EViewportTypeFlags);
        return typeEnums;
    }

    const [viewportConditionalSelectorData, setViewportConditionalSelectorData] = useState({
        selectedCs: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? enumDetails.EViewportCoordinateSystem : getEnumValueDetails(currentTreeNode.nodeMcContent.GetViewportCoordinateSystemBitField(), enumDetails.EViewportCoordinateSystem),
        selectedTypeFlag: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? enumDetails.EViewportTypeFlags : getSelectedTypes(),
        IDsString: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? '' : getIDInclusive('IDsString'),
        IDInclusiveCheckbox: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? false : getIDInclusive('IDInclusiveCheckbox'),
    })

    const saveData = (event: any) => {
        runCodeSafely(() => {
            setViewportConditionalSelectorData({ ...viewportConditionalSelectorData, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
        }, "viewportConditionalSelector.saveData => onChange")
    }

    useEffect(() => {
        runCodeSafely(() => {
            setViewportConditionalSelectorData({
                ...viewportConditionalSelectorData,
                selectedCs: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? enumDetails.EViewportCoordinateSystem : getEnumValueDetails(currentTreeNode.nodeMcContent.GetViewportCoordinateSystemBitField(), enumDetails.EViewportCoordinateSystem),
                selectedTypeFlag: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? enumDetails.EViewportTypeFlags : getSelectedTypes(),
                IDsString: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? '' : getIDInclusive('IDsString'),
                IDInclusiveCheckbox: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? false : getIDInclusive('IDInclusiveCheckbox'),
            })
        }, 'viewportConditionalSelector.useEffect => currentTreeNode');
    }, [currentTreeNode])
    useEffect(() => {
        props.setFieldsValues(handleOKClick);
    }, [viewportConditionalSelectorData])

    const handleOKClick = () => {
        let returnedValue: MapCore.IMcViewportConditionalSelector | string = '';
        runCodeSafely(() => {
            let CSBitField = getBitFieldByEnumArr(viewportConditionalSelectorData.selectedCs);
            let typeFlagBitField = getBitFieldByEnumArr(viewportConditionalSelectorData.selectedTypeFlag);
            let numericArrIDs = viewportConditionalSelectorData.IDsString != '' ? viewportConditionalSelectorData.IDsString.split(' ').map(Number) : [];
            let uint8ArrIDs = numericArrIDs.includes(NaN) ? Uint8Array.from([]) : Uint8Array.from(numericArrIDs);
            if (currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER) {
                runMapCoreSafely(() => {
                    let mcCurrentOM = currentTreeNode.nodeMcContent as MapCore.IMcOverlayManager;
                    returnedValue = MapCore.IMcViewportConditionalSelector.Create(mcCurrentOM, typeFlagBitField, CSBitField, uint8ArrIDs, viewportConditionalSelectorData.IDInclusiveCheckbox);
                }, 'viewportConditionalSelector.handleOKClick => IMcViewportConditionalSelector.Create', true)
            }
            else {
                let mcCurrentSelector = currentTreeNode.nodeMcContent as MapCore.IMcViewportConditionalSelector;
                runMapCoreSafely(() => {
                    mcCurrentSelector.SetSpecificViewports(uint8ArrIDs, viewportConditionalSelectorData.IDInclusiveCheckbox);
                    mcCurrentSelector.SetViewportCoordinateSystemBitField(CSBitField);
                    mcCurrentSelector.SetViewportTypeBitField(typeFlagBitField);
                }, 'viewportConditionalSelector.handleOKClick => IMcViewportConditionalSelector.SetViewportTypeBitField | SetViewportCoordinateSystemBitField | SetSpecificViewports', true)
            }
        }, 'viewportConditionalSelector.handleOKClick')
        return returnedValue;
    }
    //Handle Funs
    const handleTypeFlagCheckboxChange = (e: any, option: { name: string, code: number, theEnum: any }) => {
        let finalSelectedTypes: { name: string, code: number, theEnum: any }[] = [];
        let isChecked = viewportConditionalSelectorData.selectedTypeFlag.find((en: { name: string, code: number, theEnum: any, checked: boolean }) => en.code == option.code);
        if (isChecked) {
            finalSelectedTypes = viewportConditionalSelectorData.selectedTypeFlag.filter((en: any) => en.code !== option.code);
        }
        else {
            finalSelectedTypes = [...viewportConditionalSelectorData.selectedTypeFlag, option]
        }
        setViewportConditionalSelectorData({ ...viewportConditionalSelectorData, selectedTypeFlag: finalSelectedTypes })
    }
    const handleCSCheckboxChange = (e: any, option: { name: string, code: number, theEnum: any }) => {
        let finalSelectedCS: { name: string, code: number, theEnum: any }[] = [];
        let isChecked = viewportConditionalSelectorData.selectedCs.find((en: { name: string, code: number, theEnum: any, checked: boolean }) => en.code == option.code);
        if (isChecked) {
            finalSelectedCS = viewportConditionalSelectorData.selectedCs.filter((en: any) => en.code !== option.code);
        }
        else {
            finalSelectedCS = [...viewportConditionalSelectorData.selectedCs, option]
        }
        setViewportConditionalSelectorData({ ...viewportConditionalSelectorData, selectedCs: finalSelectedCS })
    }
    //DOM Funcs
    const getTypeFlagTemplate = (option: { name: string, code: number, theEnum: any }) => {
        return <div>
            <Checkbox name={`${option.code}`} inputId={`${option.code}`} onChange={e => handleTypeFlagCheckboxChange(e, option)} checked={viewportConditionalSelectorData.selectedTypeFlag.find((en: any) => en.code == option.code) ? true : false} />
            <label htmlFor={`${option.code}`}>{option.name}</label>
        </div>
    }
    const getCsTemplate = (option: { name: string, code: number, theEnum: any }) => {
        let isChecked = viewportConditionalSelectorData.selectedCs.find((en: any) => en.code == option.code) ? true : false;
        return <div>
            <Checkbox name={`cs${option.code}`} inputId={`id${option.code}`} onChange={e => handleCSCheckboxChange(e, option)} checked={isChecked} />
            <label htmlFor={`id${option.code}`}>{option.name}</label>
        </div>
    }

    return <div>
        <ListBox itemTemplate={getTypeFlagTemplate} listStyle={{ minHeight: `${globalSizeFactor * 24}vh`, maxHeight: `${globalSizeFactor * 24}vh` }} style={{ width: '100%', marginTop: `${globalSizeFactor * 2}vh` }} optionLabel='name' options={enumDetails.EViewportTypeFlags} />
        <ListBox itemTemplate={getCsTemplate} listStyle={{ minHeight: `${globalSizeFactor * 12}vh`, maxHeight: `${globalSizeFactor * 12}vh` }} style={{ width: '100%', marginTop: `${globalSizeFactor * 2}vh` }} optionLabel='name' options={enumDetails.EViewportCoordinateSystem} />
        <p>Viewport IDs (seperated by space)</p>
        <InputTextarea style={{ width: '100%', height: `${globalSizeFactor * 10}vh` }} name='IDsString' value={viewportConditionalSelectorData.IDsString} onChange={saveData} />
        <div>
            <Checkbox name='IDInclusiveCheckbox' inputId='IDInclusiveCheckbox' onChange={saveData} checked={viewportConditionalSelectorData.IDInclusiveCheckbox} />
            <label htmlFor='IDInclusiveCheckbox'>IDs Inclusive</label>
        </div>
    </div>

}