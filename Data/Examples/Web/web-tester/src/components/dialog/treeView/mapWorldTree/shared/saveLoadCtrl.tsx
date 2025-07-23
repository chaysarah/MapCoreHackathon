import { Checkbox } from "primereact/checkbox";
import { InputText } from "primereact/inputtext";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { Button } from "primereact/button";

import './styles/saveLoadCtrl.css';
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../redux/combineReducer";
import mapWorldTreeService from "../../../../../services/mapWorldTreeService";
import UploadFilesCtrl, { UploadTypeEnum } from "../../../shared/uploadFilesCtrl";

export enum SaveLoadTypes {
    SAVE_LAYER_TO_FILE,
    SAVE_LAYER_TO_BUFFER,
    SAVE_TERRAIN_TO_FILE,
    SAVE_TERRAIN_TO_BUFFER,
    LOAD_LAYER_FROM_FILE,
    LOAD_LAYER_FROM_BUFFER,
    LOAD_TERRAIN_FROM_FILE,
    LOAD_TERRAIN_FROM_BUFFER,
}

export default function SaveLoadCtrl(props: {
    saveLoadType: SaveLoadTypes,
}) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [fileName, setFileName] = useState('');
    const [baseDirectory, setBaseDirectory] = useState('');
    const [isSaveUserData, setIsSaveUserData] = useState(false);

    const saveTypesArr = [SaveLoadTypes.SAVE_LAYER_TO_BUFFER, SaveLoadTypes.SAVE_LAYER_TO_FILE, SaveLoadTypes.SAVE_TERRAIN_TO_BUFFER, SaveLoadTypes.SAVE_TERRAIN_TO_FILE];
    const saveLoadFileTypesArr = [SaveLoadTypes.SAVE_LAYER_TO_FILE, SaveLoadTypes.SAVE_TERRAIN_TO_FILE, SaveLoadTypes.LOAD_TERRAIN_FROM_FILE, SaveLoadTypes.LOAD_LAYER_FROM_FILE];
    const loadFromBufferTypesArr = [SaveLoadTypes.LOAD_TERRAIN_FROM_FILE, SaveLoadTypes.LOAD_LAYER_FROM_FILE];

    useEffect(() => {
        runCodeSafely(() => {
            const root = document.documentElement;
            let pixelWidth = window.innerHeight * 0.35 * globalSizeFactor;
            root.style.setProperty('--save-load-ctrl-dialog-width', `${pixelWidth}px`);
        }, 'saveLoadCtrl.useEffect')
    }, [])

    const handleOKClick = () => {
        runCodeSafely(() => {
            let handleFunction = mapWorldTreeService.saveLoadFunctionsMap.get(props.saveLoadType);
            handleFunction(fileName, baseDirectory, isSaveUserData);
        }, 'saveLoadCtrl.handleOKClick')
    }

    return <div className="form__column-container">
        <div className={`form__flex-and-row-between form__items-center ${saveLoadFileTypesArr.includes(props.saveLoadType) ? '' : 'form__disabled'}`}>
            <label style={{ width: '40%' }} htmlFor="fileName">File Name: </label>
            {loadFromBufferTypesArr.includes(props.saveLoadType) ?
                <UploadFilesCtrl isDirectoryUpload={false} uploadOptions={[UploadTypeEnum.upload]}
                    getVirtualFSPath={(virtualFSPath: string, selectedOption: UploadTypeEnum) => { setFileName(virtualFSPath) }} />
                :
                <InputText id='fileName' value={fileName} onChange={e => setFileName(e.target.value)} />
            }
        </div>
        <div className={`form__flex-and-row-between form__items-center`}>
            <label htmlFor="baseDirectory">Base Directory: </label>
            <InputText id='baseDirectory' value={baseDirectory} onChange={e => setBaseDirectory(e.target.value)} />
        </div>
        <div className={`form__flex-and-row form__items-center ${saveTypesArr.includes(props.saveLoadType) ? '' : 'form__disabled'}`}>
            <Checkbox inputId="isSaveUserData" onChange={e => setIsSaveUserData(e.checked)} checked={isSaveUserData} />
            <label htmlFor="isSaveUserData">Save User Data</label>
        </div>

        <Button className="form__align-self-end" label='OK' onClick={handleOKClick} />
    </div>
}