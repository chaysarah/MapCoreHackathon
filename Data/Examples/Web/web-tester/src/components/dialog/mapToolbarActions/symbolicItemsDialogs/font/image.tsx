import { useEffect, useMemo, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { InputText } from "primereact/inputtext";
import { InputNumber } from "primereact/inputnumber";
import { RadioButton } from "primereact/radiobutton";
import { Checkbox } from "primereact/checkbox";
import { Button } from "primereact/button";
import { Fieldset } from "primereact/fieldset";
import { ConfirmDialog } from "primereact/confirmdialog";

import { getEnumDetailsList, getEnumValueDetails } from "mapcore-lib";
import './styles/image.css';
import "../../../styles/forms.css";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import { AppState } from "../../../../../redux/combineReducer";
import { setVirtualFSSerialNumber } from "../../../../../redux/MapCore/mapCoreAction";

export default function ImageDialog(props: { getImage: (image: MapCore.IMcImage) => void, defaultImage: MapCore.IMcImage }) {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const virtualFSSerialNumber = useSelector((state: AppState) => state.mapCoreReducer.virtualFSSerialNumber)

    const [confirmDialogVisible, setConfirmDialogVisible] = useState(false);
    const [fileName, setFileName] = useState("");
    const [imageBuffer, setImageBuffer] = useState<Uint8Array | null>(null);
    const [width, setWidth] = useState<number | null>(null);
    const [height, setHeight] = useState<number | null>(null);
    const [pixelFormat, setPixelFormat] = useState("");
    const [createFrom, setCreateFrom] = useState<"file" | "pixel" | "memory">("memory");
    const [useFileExtension, setUseFileExtension] = useState(true);

    const pixelFormatEnumList = useMemo(() => getEnumDetailsList(MapCore.IMcTexture.EPixelFormat), [])

    //#region UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            const root = document.documentElement;
            let pixelWidth = window.innerHeight * 0.45 * globalSizeFactor;
            root.style.setProperty('--image-dialog-width', `${pixelWidth}px`);
        }, 'SelectExistingLayer.setDialogWidth')
    }, []);
    useEffect(() => {
        runCodeSafely(() => {
            if (props.defaultImage) {
                const fileSource = props.defaultImage.GetFileSource();
                setFileName(fileSource.strFileName);
                fileSource.strFileName && setCreateFrom("file");
                setUseFileExtension(fileSource.strFormatExtension !== '');
                let pixelFormatEnum = null;
                runMapCoreSafely(() => { pixelFormatEnum = props.defaultImage.GetPixelFormat(); }, 'ImageDialog.useEffect => IMcImage.GetPixelFormat', true)
                let pixelFormatName = getEnumValueDetails(pixelFormatEnum, pixelFormatEnumList).name;
                let pHeight: { Value?: number } = {};
                let pWidth: { Value?: number } = {};
                runMapCoreSafely(() => { props.defaultImage.GetSize(pWidth, pHeight); }, 'ImageDialog.useEffect => IMcImage.GetSize', true)

                setWidth(pWidth.Value ?? null);
                setHeight(pHeight.Value ?? null);
                setPixelFormat(pixelFormatName);
            }
        }, 'ImageDialog.useEffect');
    }, [props.defaultImage, pixelFormatEnumList]);
    //#endregion
    //#region Handle Functions
    const setImageData = (fileBuffer: Uint8Array) => {
        runCodeSafely(() => {
            const fileSource = new MapCore.SMcFileSource(fileBuffer, true);
            let mcImage: MapCore.IMcImage = null;
            runMapCoreSafely(() => { mcImage = MapCore.IMcImage.Create(fileSource); }, 'Image.setImageData => IMcImage.Create', true)
            const createInterval = setInterval(() => {
                runCodeSafely(() => {
                    let pixelBuffer = null;
                    runMapCoreSafely(() => { pixelBuffer = mcImage.GetPixelBuffer(); }, 'Image.setImageData.setInterval => IMcImage.GetPixelBuffer', false)
                    console.log('Image.setImageData.setInterval', pixelBuffer);
                    if (pixelBuffer.length > 0) {
                        let pixelFormatEnum = null;
                        runMapCoreSafely(() => { pixelFormatEnum = mcImage.GetPixelFormat(); }, 'Image.setImageData => IMcImage.GetPixelFormat', true)
                        let pixelFormatName = getEnumValueDetails(pixelFormatEnum, pixelFormatEnumList).name;
                        let pHeight: { Value?: number } = {};
                        let pWidth: { Value?: number } = {};
                        runMapCoreSafely(() => { mcImage.GetSize(pWidth, pHeight); }, 'Image.setImageData => IMcImage.GetSize', true)

                        setWidth(pWidth.Value ?? null);
                        setHeight(pHeight.Value ?? null);
                        setPixelFormat(pixelFormatName);

                        clearInterval(createInterval);
                    }
                }, 'Image.setImageData.setInterval');
            }, 100);
        }, 'Image.setImageData');
    }
    const handleFileSelected = (event: React.ChangeEvent<HTMLInputElement>) => {
        runCodeSafely(() => {
            const file = event.target.files?.[0];
            if (!file) return;

            const reader = new FileReader();
            reader.onload = () => {
                const arrayBuffer = reader.result as ArrayBuffer;
                const uint8Array = new Uint8Array(arrayBuffer);

                setImageData(uint8Array);
                setFileName(file.name);
                setImageBuffer(uint8Array);
                const extension = objectWorldTreeService.getFileExtension(file.name);
                extension == 'svg' && setCreateFrom("memory");
            };
            reader.readAsArrayBuffer(file);
        }, "imageFile.handleFileUpload => onChange");
    };
    const handleCreateImage = () => {
        runCodeSafely(() => {
            if (fileName) {
                let mcImage: MapCore.IMcImage = null;
                const extension = objectWorldTreeService.getFileExtension(fileName);
                if (createFrom === "file") {
                    runMapCoreSafely(() => { MapCore.IMcMapDevice.CreateFileSystemDirectory(`${virtualFSSerialNumber}`) }, "TextureService.createImageFileTexture => IMcMapDevice.CreateFileSystemDirectory", true);
                    runMapCoreSafely(() => { MapCore.IMcMapDevice.CreateFileSystemFile(`${virtualFSSerialNumber}/${fileName}`, imageBuffer) }, "TextureService.createImageFileTexture => IMcMapDevice.CreateFileSystemFile", true);
                    dispatch(setVirtualFSSerialNumber(virtualFSSerialNumber + 1))
                    const fileSource = new MapCore.SMcFileSource(`${virtualFSSerialNumber}/${fileName}`, false);
                    runMapCoreSafely(() => { mcImage = MapCore.IMcImage.Create(fileSource); }, 'Image.handleCreateImage => IMcImage.Create', true)
                } else if (createFrom === "pixel") {
                    const fileSource = new MapCore.SMcFileSource(imageBuffer, true);
                    runMapCoreSafely(() => { mcImage = MapCore.IMcImage.Create(fileSource); }, 'Image.handleCreateImage => IMcImage.CreateFromPixelBuffer', true)
                    const pixelBuffer = mcImage.GetPixelBuffer();
                    runMapCoreSafely(() => { mcImage = MapCore.IMcImage.Create(pixelBuffer, width, height, mcImage.GetPixelFormat(), mcImage.GetNumMipmaps()); }, 'Image.handleCreateImage => IMcImage.CreateFromPixelBuffer', true)
                } else if (createFrom === "memory") {
                    const fileSource = new MapCore.SMcFileSource(imageBuffer, true, useFileExtension ? extension : '');
                    runMapCoreSafely(() => { mcImage = MapCore.IMcImage.Create(fileSource); }, 'Image.handleCreateImage => IMcImage.Create', true)
                }
                props.getImage(mcImage);
            }
            else {
                setConfirmDialogVisible(true)
            }
        }, 'Image.handleCreateImage');
    }
    //#endregion
    //#region DOM Functions
    const getFileData = () => {
        return <Fieldset legend="File Data" className="form__column-fieldset form__disabled">
            <div className="form__flex-and-row-between">
                <label htmlFor="width">Width:</label>
                <InputNumber id="width" value={width} onValueChange={e => setWidth(e.value ?? null)}
                />
            </div>
            <div className="form__flex-and-row-between">
                <label htmlFor="height">Height:</label>
                <InputNumber id="height" value={height} onValueChange={e => setHeight(e.value ?? null)} />
            </div>
            <div className="form__flex-and-row-between">
                <label htmlFor="pixelFormat">Pixel Format:</label>
                <InputText id="pixelFormat" value={pixelFormat} onChange={e => setPixelFormat(e.target.value)} />
            </div>
        </Fieldset>
    }
    const getFormOptionsFieldset = () => {
        return <div className="form__flex-and-row-between">
            <span>Create From:</span>
            <div className="form__column-container">
                <div className="form__flex-and-row form__items-center">
                    <RadioButton inputId="createFromMemory" name="createFrom" value="memory" checked={createFrom === "memory"} onChange={() => setCreateFrom("memory")} />
                    <label htmlFor="createFromMemory">File as Memory Buffer</label>
                </div>
                <div className="form__flex-and-row form__items-center">
                    <RadioButton inputId="createFromFile" name="createFrom" value="file" checked={createFrom === "file"} onChange={() => setCreateFrom("file")} />
                    <label htmlFor="createFromFile">File</label>
                </div>
                <div className={`form__flex-and-row form__items-center ${objectWorldTreeService.getFileExtension(fileName) == 'svg' && 'form__disabled'}`}>
                    <RadioButton inputId="createFromPixel" name="createFrom" value="pixel" checked={createFrom === "pixel"} onChange={() => setCreateFrom("pixel")} />
                    <label htmlFor="createFromPixel">Pixel Buffer</label>
                </div>
            </div>
            <div className="form__column-container">
                <div className={`form__flex-and-row form__items-center ${createFrom == 'memory' ? '' : 'form__disabled'}`}>
                    <Checkbox inputId="useFileExtension" checked={useFileExtension} onChange={e => setUseFileExtension(e.checked ?? false)} />
                    <label htmlFor="useFileExtension">Use File Extension</label>
                </div>
            </div>
        </div >
    }
    //#endregion

    return (
        <div className="form__column-container">
            <span className="form__flex-and-row-between form__items-center">
                <div style={{ width: '40%' }}>File Name: </div>
                <div className="form__flex-and-row form__items-center" style={{ width: '60%' }}>
                    <input
                        style={{ display: 'none' }}
                        type="file"
                        id='fileInput'
                        accept='.bmp,.jpg,.jpeg,.tif,.tiff,.gif,.png,.ico,.svg'
                        onChange={handleFileSelected}
                    />
                    <InputText name="nameOfFile" value={fileName} disabled style={{ width: '80%' }} />
                    <Button style={{ margin: '1%' }} label="..." onClick={() => document.getElementById('fileInput').click()} />
                </div>
            </span>

            {getFileData()}
            {getFormOptionsFieldset()}
            <br />
            <Button label="Create" className="form__align-self-end" onClick={handleCreateImage} />

            <ConfirmDialog
                contentClassName='form__confirm-dialog-content'
                message='Missing File Name'
                header=''
                footer={<div></div>}
                visible={confirmDialogVisible}
                onHide={e => { setConfirmDialogVisible(false) }}
            />
        </div>
    );
};
