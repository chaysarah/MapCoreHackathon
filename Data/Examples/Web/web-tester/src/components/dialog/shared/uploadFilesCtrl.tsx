import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { ChangeEvent, useEffect, useRef, useState } from "react";
import { runCodeSafely, runMapCoreSafely } from "../../../common/services/error-handling/errorHandler";
import objectWorldTreeService from "../../../services/objectWorldTree.service";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../redux/combineReducer";
import { setVirtualFSSerialNumber } from "../../../redux/MapCore/mapCoreAction";
import { RadioButton } from "primereact/radiobutton";
import { ProgressBar } from 'primereact/progressbar';


export enum UploadTypeEnum {
    upload = 1,
    url,
}

//DESCRIPTION: This Ctrl cretaes virtual FS according to the selected file or directory from the local FS.
export default function UploadFilesCtrl(props: { isDirectoryUpload: boolean, uploadOptions: UploadTypeEnum[], accept?: string, label?: string, existLocationPath?: string, buttonOnly?: boolean, existInputValue?: string, getVirtualFSPath: (virtualFSPath: string, selectedOption: UploadTypeEnum) => void }) {
    const dispatch = useDispatch();
    let globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor);
    let virtualFSSerialNumber = useSelector((state: AppState) => state.mapCoreReducer.virtualFSSerialNumber)
    let inputRef = useRef(null);
    let onloadCounter = useRef(null);
    let [directoryName, setDirectoryName] = useState<string>(props.existInputValue ? props.existInputValue : '');
    let [selectedOption, setSelectedOption] = useState<UploadTypeEnum>();
    let [userUrl, setUserUrl] = useState<string>('');
    let [isUrlChange, setIsUrlChange] = useState<boolean>(false);
    let [isLoading, setIsLoading] = useState<boolean>(false);

    //#region UseEffects
    useEffect(() => {
        let selectedOption = props.uploadOptions.length == 1 ? props.uploadOptions[0] : UploadTypeEnum.url;
        setSelectedOption(selectedOption)
    }, [])
    useEffect(() => {
        if (props.existInputValue && props.existInputValue != directoryName) {
            setDirectoryName(props.existInputValue)
        }
    }, [props.existInputValue])
    //#endregion
    //#region Handle Functions
    const handleDirectorySelected = (event: ChangeEvent<HTMLInputElement>) => {
        runCodeSafely(() => {
            const selectedFiles = event.target.files;
            if (selectedFiles) {
                setIsLoading(true);
                let selectedFilesArr = Array.from(selectedFiles);
                let prefix = props.existLocationPath ? `${props.existLocationPath}/` : `${virtualFSSerialNumber}/`;
                let filePaths: string[] = selectedFilesArr.map((f: any) => prefix + f.webkitRelativePath);
                let directories = objectWorldTreeService.getDirectoriesFromFilesPaths(filePaths);
                directories = props.existLocationPath ? directories?.slice(1) : directories;
                directories?.forEach(dir => {
                    runMapCoreSafely(() => { MapCore.IMcMapDevice.CreateFileSystemDirectory(dir); }, 'uploadFilesCtrl.handleDirectorySelected => IMcMapDevice.CreateFileSystemDirectory', true)
                });
                directories && !props.existLocationPath && dispatch(setVirtualFSSerialNumber(virtualFSSerialNumber + 1));
                for (let i = 0; i < selectedFiles.length; i++) {
                    onloadCounter.current = selectedFiles.length;
                    const reader = new FileReader();

                    reader.onload = (event) => {
                        const arrayBuffer = event.target.result;
                        const uint8Array = new Uint8Array(arrayBuffer as ArrayBuffer);
                        runMapCoreSafely(() => { MapCore.IMcMapDevice.CreateFileSystemFile(filePaths[i], uint8Array); }, 'uploadFilesCtrl.handleDirectorySelected => IMcMapDevice.CreateFileSystemFile', true)
                        onloadCounter.current = onloadCounter.current - 1;
                        if (onloadCounter.current == 0) {
                            let finalDir = props.existLocationPath ? directories[0] : directories[1];
                            props.getVirtualFSPath(finalDir, selectedOption);
                            setDirectoryName(finalDir)
                            setIsLoading(false);
                        }
                    };

                    reader.readAsArrayBuffer(selectedFiles[i]);
                }

                if (inputRef.current) {
                    inputRef.current.value = '';
                }
            }
        }, "uploadFilesCtrl.handleDirectorySelected => onChange")
    }
    const handleFileSelected = (event: ChangeEvent<HTMLInputElement>) => {
        runCodeSafely(() => {
            const file = event.target.files[0];
            if (file) {
                setIsLoading(true);
                let rootDirectory = props.existLocationPath ? props.existLocationPath : `${virtualFSSerialNumber}`;
                !props.existLocationPath && runMapCoreSafely(() => { MapCore.IMcMapDevice.CreateFileSystemDirectory(rootDirectory); }, 'uploadFilesCtrl.handleFileSelected => IMcMapDevice.CreateFileSystemFile', true)
                let serialFileName = `${rootDirectory}/${file.name}`;
                !props.existLocationPath && dispatch(setVirtualFSSerialNumber(virtualFSSerialNumber + 1));

                const reader = new FileReader();
                reader.onload = (e) => {
                    const arrayBuffer = e.target.result;
                    const uint8Array = new Uint8Array(arrayBuffer as ArrayBuffer);
                    runMapCoreSafely(() => { MapCore.IMcMapDevice.CreateFileSystemFile(serialFileName, uint8Array); }, 'uploadFilesCtrl.handleFileSelected => IMcMapDevice.CreateFileSystemFile', true)
                    props.getVirtualFSPath(serialFileName, selectedOption);
                    setDirectoryName(serialFileName);
                    setIsLoading(false);
                };
                reader.readAsArrayBuffer(file);

                if (inputRef.current) {
                    inputRef.current.value = '';
                }
            }
        }, "uploadFilesCtrl.handleFileSelected => onChange")
    }
    const handleUrlBlur = (e) => {
        runCodeSafely(() => {
            setUserUrl(e.target.value);
            if (isUrlChange) {
                props.getVirtualFSPath(e.target.value, selectedOption);
                setIsUrlChange(false);
            }
        }, "uploadFilesCtrl.handleUrlBlur => onBlur")
    }
    const handleUrlChange = (e) => {
        runCodeSafely(() => {
            setUserUrl(e.target.value);
            setIsUrlChange(true);
        }, "uploadFilesCtrl.handleUrlChange => onChange")
    }
    //#endregion
    //#region DOM
    const getFileInput = () => {
        return <>
            {props.isDirectoryUpload ?
                <input type="file"
                    //@ts-ignore
                    webkitdirectory=""
                    //@ts-ignore
                    directory=""
                    accept='directory'
                    ref={inputRef}
                    style={{ display: 'none' }} onChange={handleDirectorySelected} /> :
                <input accept={props.accept} type="file" ref={inputRef} style={{ display: 'none' }} onChange={handleFileSelected} />
            }
            {!props.buttonOnly &&
                <div className="form__flex-and-column">
                    <InputText className="form__max-width-input" style={{ width: '80%' }} disabled value={directoryName.split('/').at(-1)} />
                    {isLoading && <ProgressBar mode="indeterminate" style={{ height: `${globalSizeFactor * 0.3}vh` }} />}
                </div>
            }
        </>
    }
    const getUploadFilesElemets = () => {
        let stringOptions = JSON.stringify(props.uploadOptions);
        switch (stringOptions) {
            case JSON.stringify([UploadTypeEnum.upload]):
                return <div className={props.buttonOnly ? 'form__column-container' : `form__flex-and-row-between form__items-center`}>
                    <Button label={props.label || "Upload"} icon="pi pi-upload" onClick={() => inputRef.current.click()} />
                    {getFileInput()}
                </div>
            case JSON.stringify([UploadTypeEnum.url]):
                return <div className="form__flex-and-row-between form__items-center">
                    <label>URL:</label>
                    <InputText className="form__max-width-input" value={userUrl} onBlur={handleUrlBlur} onChange={handleUrlChange} />
                </div>
            default:
                return <div className="form__column-container">
                    <div className="form__flex-and-row-between form__items-center">
                        <div className="form__flex-and-row form__items-center">
                            <RadioButton value={UploadTypeEnum.url} onChange={(e) => { setSelectedOption(e.value); props.getVirtualFSPath(userUrl, UploadTypeEnum.url) }} checked={selectedOption === UploadTypeEnum.url} />
                            <label className={`${selectedOption == UploadTypeEnum.upload ? 'form__disabled' : ''}`}> URL:</label>
                        </div>
                        <InputText className={`${selectedOption == UploadTypeEnum.upload ? 'form__disabled form__max-width-input' : 'form__max-width-input'}`} value={userUrl} onBlur={handleUrlBlur} onChange={handleUrlChange} />
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <div className="form__flex-and-row form__items-center">
                            <RadioButton value={UploadTypeEnum.upload} onChange={(e) => { setSelectedOption(e.value); props.getVirtualFSPath(directoryName, UploadTypeEnum.upload) }} checked={selectedOption === UploadTypeEnum.upload} />
                            <Button disabled={selectedOption == UploadTypeEnum.url} label={props.label || "Upload"} icon="pi pi-upload" onClick={() => inputRef.current.click()} />
                        </div>
                        {getFileInput()}
                    </div>
                </div>
        }
    }
    //#endregion

    return getUploadFilesElemets();
}