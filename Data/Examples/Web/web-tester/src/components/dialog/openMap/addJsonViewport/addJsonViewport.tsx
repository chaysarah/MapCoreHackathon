import { InputText } from "primereact/inputtext";
import { ChangeEvent, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import Footer from "../../footerDialog";
import { closeDialog, setDialogType } from "../../../../redux/MapCore/mapCoreAction";
import { addViewportJson } from "../../../../redux/mapWindow/mapWindowAction"
import { selectMaxViewportId } from "../../../../redux/mapWindow/mapWindowReducer";
import addJsonViewportService from "../../../../services/addJsonViewport.service";
import { runAsyncCodeSafely } from "../../../../common/services/error-handling/errorHandler";
import { Button } from "primereact/button";
import { RadioButton } from "primereact/radiobutton";
import './styles/addJsonViewport.css';
import { DialogTypesEnum } from "../../../../tools/enum/enums";
import { AppState } from "../../../../redux/combineReducer";

export default function AddJsonViewport() {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [selectedOption, setSelectedOption] = useState('JSON');
    const [jsonPath, setJsonPath] = useState<string>("");
    const [dirName, setDirName] = useState<string>("");
    const [fileBufferArr, setFileBufferArr] = useState<{ fileName: string; fileBuffer: Uint8Array; }[]>(null);
    const dispatch = useDispatch();
    let maxLayerId: number = useSelector(selectMaxViewportId);

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.5 * globalSizeFactor;
        root.style.setProperty('--add-json-vp-dialog-width', `${pixelWidth}px`);

    }, [])
    const addViewportFromJson = async () => {
        runAsyncCodeSafely(async () => {
            if (fileBufferArr) {
                let jsonFileBuffer = fileBufferArr.find(f => f.fileName == 'FullViewportDataParams_GLES_Master.json').fileBuffer;
                await addJsonViewportService.readDeviceParamsFromJson(jsonFileBuffer, true);
                dispatch(addViewportJson(maxLayerId + 1, null, fileBufferArr, null));
                dispatch(closeDialog(DialogTypesEnum.addJsonViewport));
            }
            if (jsonPath) {
                await addJsonViewportService.readDeviceParamsFromJson(jsonPath);
                dispatch(addViewportJson(maxLayerId + 1, jsonPath, null, null));
            }
            dispatch(closeDialog(DialogTypesEnum.addJsonViewport));
        }, 'addJsonViewport.addViewportFromJson');
    }
    const handleJsonUpload = async (event: ChangeEvent<HTMLInputElement>) => {
        let relativePath = event.target.files[0].webkitRelativePath;
        let slashIndex = event.target.files[0].webkitRelativePath.indexOf('/')
        setDirName(relativePath.substring(0, slashIndex));
        let files = Array.from(event.target.files);
        const localFileBufferArr: { fileName: string, fileBuffer: Uint8Array }[] = [];
        for (const file of files) {
            let buffer = await file.arrayBuffer();
            let fileUint8Buffer = new Uint8Array(buffer);
            let fileBufferObj = { fileName: file.name, fileBuffer: fileUint8Buffer };
            localFileBufferArr.push(fileBufferObj);
        }
        setFileBufferArr(localFileBufferArr);
    }

    return (<div >
        <div style={{ display: 'flex', flexDirection: 'column' }}>
            <div >
                <RadioButton inputId="radioJSON" value="JSON" onChange={(e) => setSelectedOption(e.value)} checked={selectedOption === 'JSON'} />
                <label className={selectedOption === 'JSON' ? 'add-json-vp__radio-button-label' : 'add-json-vp__disabled-radio-button-label'}> URL:</label>
            </div>
            <InputText disabled={selectedOption !== 'JSON'} style={{ marginTop: `${globalSizeFactor * 1}vh`, width: '80%' }} onChange={(e) => setJsonPath(e.target.value)}></InputText>
            <br />
            <div>
                <RadioButton inputId="radioFOLDER" value="FOLDER" onChange={(e) => setSelectedOption(e.value)} checked={selectedOption === 'FOLDER'} />
                <label className={selectedOption === 'FOLDER' ? 'add-json-vp__radio-button-label' : 'add-json-vp__disabled-radio-button-label'}>Folder Name:</label>
            </div>
            <div style={{ display: 'flex', flexDirection: 'row', marginTop: `${globalSizeFactor * 1}vh` }}>
                <InputText style={{ width: '80%' }} disabled value={dirName} />
                <input type="file"
                    //@ts-ignore
                    webkitdirectory=""
                    //@ts-ignore
                    directory=""
                    accept='directory'
                    id="fileInput" style={{ display: 'none' }} onChange={handleJsonUpload} />
                <Button disabled={selectedOption !== 'FOLDER'} label="Load" icon="pi pi-upload" onClick={() => document.getElementById('fileInput').click()} />
            </div>
            <br />
            <Footer onOk={addViewportFromJson} label="Ok"></Footer>
        </div>
    </div>
    )
}