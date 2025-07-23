import React, { useEffect } from 'react';
import { Checkbox } from 'primereact/checkbox';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { RadioButton } from 'primereact/radiobutton';

import { Properties } from "../../../../dialog";
import { runCodeSafely } from '../../../../../../common/services/error-handling/errorHandler';
import TexturePropertiesBase from '../../../../../../propertiesBase/texturePropertiesBase';
import TextureService, { TextureTypeEnum } from '../../../../../../services/texture.service';

export class ImageFileProperties implements Properties {
    inputFile: any;
    nameOfFile: string;
    fileUrl: string;
    isMemoryBuffer: boolean;
    useFileExtension: boolean;
    isDisabledSave: boolean;
    selectedOption: string;

    static getDefault(props: any): ImageFileProperties {
        let { tabInfo } = props;

        return {
            inputFile: tabInfo.properties.imageFileProperties.inputFile || null,
            nameOfFile: tabInfo.properties.imageFileProperties.nameOfFile || '',
            fileUrl: tabInfo.properties.imageFileProperties.fileUrl || '',
            isMemoryBuffer: tabInfo.properties.imageFileProperties.isMemoryBuffer || true,
            useFileExtension: tabInfo.properties.imageFileProperties.useFileExtension || true,
            isDisabledSave: tabInfo.properties.imageFileProperties.isDisabledSave || true,
            selectedOption: tabInfo.properties.imageFileProperties.selectedOption || 'FOLDER',
        }
    }
}

export default function ImageFile(props: {
    defaultTexture?: MapCore.IMcImageFileTexture,
    tabInfo: {
        properties: TexturePropertiesBase,
        setPropertiesCallback: (tab: string, key: string, value: any) => void,
    }
}) {

    useEffect(() => {
        runCodeSafely(() => {
            let defaultProperties = ImageFileProperties.getDefault(props)
            TextureService.textureType = TextureTypeEnum.ImageFile;
            if (props.defaultTexture) {
                let fileSource: MapCore.SMcFileSource = props.defaultTexture.GetImageFile();
                defaultProperties = {
                    ...defaultProperties,
                    isMemoryBuffer: fileSource.bIsMemoryBuffer,
                    useFileExtension: fileSource.strFormatExtension !== "",
                    isDisabledSave: !fileSource.bIsMemoryBuffer,
                }
            }
            props.tabInfo.setPropertiesCallback("imageFileProperties", null, defaultProperties);
        }, "imageFile.useEffect[mounting]");
    }, [])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("imageFileProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
        }, "imageFile.saveData => onChange");
    }

    //#region Handle Functions
    const handleFileUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
        runCodeSafely(() => {
            const file = event.target.files[0];
            props.tabInfo.setPropertiesCallback("imageFileProperties", event.target.name, file);
            props.tabInfo.setPropertiesCallback("imageFileProperties", 'nameOfFile', event.target.files[0].name);
        }, "imageFile.handleFileUpload => onChange");
    };
    const handleSaveTextureClick = () => {
        runCodeSafely(() => {
            if (props.defaultTexture) {
                let fileSource: MapCore.SMcFileSource = (props.defaultTexture as any as MapCore.IMcImageFileTexture).GetImageFile();
                let fileName = fileSource.strFileName == "" ? props.defaultTexture.GetName() : fileSource.strFileName
                if (fileSource.bIsMemoryBuffer)
                    MapCore.IMcMapDevice.DownloadBufferAsFile(fileSource.aFileMemoryBuffer, fileName + "." + fileSource.strFormatExtension)
            }
        }, "imageFile.handleSaveTextureClick => onClick");
    }
    //#endregion

    return (
        <div style={{ height: '100%' }}>
            <div style={{ height: '85%', marginLeft: '7%' }}>
                <div><RadioButton value="FOLDER" onChange={(e) => {
                    props.tabInfo.setPropertiesCallback("imageFileProperties", 'selectedOption', e.value)
                }} checked={props.tabInfo.properties.imageFileProperties.selectedOption === 'FOLDER'}></RadioButton>
                    <label className={`object-props__bold-label ${props.tabInfo.properties.imageFileProperties.selectedOption == 'PATH' && "disabledDiv"}`}>file name:</label></div>
                <div className={props.tabInfo.properties.imageFileProperties.selectedOption == 'PATH' ? "disabledDiv" : ""}>
                    <input style={{ display: 'none' }} type="file" name='inputFile' id='fileInput'
                        accept=".bmp,.jpg,.jpeg,.tif,.tiff,.gif,.png,.ping,.raw,.mnf,.svg"
                        onChange={handleFileUpload} />
                    <InputText name="nameOfFile" value={props.tabInfo.properties.imageFileProperties.nameOfFile ?? ''} onChange={saveData} readOnly={true} style={{ width: '80%' }} />
                    <Button style={{ margin: '1%' }} label="..." onClick={() => document.getElementById('fileInput').click()} />
                </div>
                <br></br> <div><RadioButton value="PATH" onChange={(e) => {
                    props.tabInfo.setPropertiesCallback("imageFileProperties", 'selectedOption', e.value)
                }} checked={props.tabInfo.properties.imageFileProperties.selectedOption === 'PATH'}></RadioButton>
                    <label className={`object-props__bold-label ${props.tabInfo.properties.imageFileProperties.selectedOption == 'FOLDER' && "disabledDiv"}`}>file path:</label></div>

                <div className={props.tabInfo.properties.imageFileProperties.selectedOption == 'FOLDER' ? "disabledDiv" : ""}>
                    <InputText name="fileUrl" value={props.tabInfo.properties.imageFileProperties.fileUrl ?? ''} onChange={saveData} style={{ width: '80%' }} />
                </div>
                <br />
                <div className='form__flex-and-row form__items-center'>
                    <div className='form__flex-and-row form__items-center'>
                        <Checkbox name="isMemoryBuffer" onChange={saveData} checked={props.tabInfo.properties.imageFileProperties.isMemoryBuffer ?? false} />
                        <label className="ml-2 form__checkbox-div">Is Memory Buffer</label>
                    </div>
                    <div className='form__flex-and-row form__items-center'>
                        <Checkbox disabled={!props.tabInfo.properties.imageFileProperties.isMemoryBuffer} name="useFileExtension" onChange={saveData} checked={props.tabInfo.properties.imageFileProperties.useFileExtension ?? false} />
                        <label className="ml-2 form__checkbox-div">Use File Extension</label>
                    </div>
                </div>
                <br />
                <Button disabled={props.tabInfo.properties.imageFileProperties.isDisabledSave} onClick={handleSaveTextureClick}>Save Texture To File</Button>
            </div>
        </div >
    )
}