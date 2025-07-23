import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import { Dropdown } from "primereact/dropdown";
import { InputText } from "primereact/inputtext";
import { ChangeEvent, useEffect, useState } from "react";
import TreeNodeModel from '../../../../shared/models/tree-node.model'
import { AppState } from "../../../../../redux/combineReducer"
import { useDispatch, useSelector } from "react-redux";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { max } from "lodash";
import './styles/saveToFile.css';

export default function SaveToFile(props: { savedFileTypeOptions: { name: string, extension: string }[], handleSaveToFileOk: (fileName: string, fileType: string) => void }) {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    let [saveToFileFormData, setSaveToFileFormData] = useState({
        savedFileTypeValue: props.savedFileTypeOptions[0],
        tmpName: '',
        okClicked: false,
        isInvalidForm: '',
    })

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.3 * globalSizeFactor;
        root.style.setProperty('--save-to-file-dialog-width', `${pixelWidth}px`);
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            let isInvalid = !saveToFileFormData.tmpName && saveToFileFormData.okClicked ? 'p-invalid' : '';
            setSaveToFileFormData({ ...saveToFileFormData, isInvalidForm: isInvalid })
        }, 'SaveToFile.useEffect')
    }, [saveToFileFormData.tmpName, saveToFileFormData.okClicked])

    const handleSaveToFileOk = () => {
        runCodeSafely(() => {
            if (saveToFileFormData.tmpName) {
                setSaveToFileFormData({ ...saveToFileFormData, 'okClicked': false })
                props.handleSaveToFileOk(saveToFileFormData.tmpName, saveToFileFormData.savedFileTypeValue.extension);
            }
            else {
                setSaveToFileFormData({ ...saveToFileFormData, 'okClicked': true })
            }
        }, 'SaveToFile.handleSaveToFileOk => onClick')
    }

    return (
        <span>
            <div style={{ display: 'flex', flexDirection: 'column', width: `${globalSizeFactor * 37.5}vh` }}>
                <span style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', padding: `${globalSizeFactor * 1.2}vh` }}>
                    <label htmlFor="fileN">Enter file name:</label>
                    <InputText tooltipOptions={{ position: 'top' }} tooltip={!saveToFileFormData.tmpName && saveToFileFormData.okClicked ? "required field" : ''}
                        style={{ width: `${globalSizeFactor * 1.5 * 12.1}vh` }} className={saveToFileFormData.isInvalidForm} required id="fileN" onInput={(e: ChangeEvent<HTMLInputElement>) => {
                            setSaveToFileFormData({ ...saveToFileFormData, 'tmpName': e.target.value })
                        }} />
                </span>
                <span style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', padding: `${globalSizeFactor * 1.2}vh` }}>
                    <label htmlFor="savedFileType">Save file as type:</label>
                    <Dropdown id="savedFileType" name='savedFileTypeValue' value={saveToFileFormData.savedFileTypeValue} optionLabel="name" onChange={(e) => {
                        setSaveToFileFormData({ ...saveToFileFormData, 'savedFileTypeValue': e.target.value })
                    }} options={props.savedFileTypeOptions} placeholder={saveToFileFormData.savedFileTypeValue.name} />
                </span>
            </div>
            <br />
            <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
                <Button label='OK' onClick={handleSaveToFileOk} />
            </div>
        </span>
    )
}
