import React, { useState } from 'react';
import { Checkbox } from 'primereact/checkbox';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import { DialogTypesEnum } from '../../../../../tools/enum/enums';
import ObjectPropertiesBase from '../../../../../propertiesBase/objectPropertiesBase';
import EditTextureCtrl from '../../../shared/editTextureCtrl';
import TexturePropertiesBase from '../../../../../propertiesBase/texturePropertiesBase';
import generalService from '../../../../../services/general.service';
import { AppState } from '../../../../../redux/combineReducer';
import { useSelector } from 'react-redux';
import ColorPickerCtrl from '../../../../shared/colorPicker';

export default function Picture(props: { dialogProperties: ObjectPropertiesBase, setDialogPropertiesCallback: (key: string, value: any) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [enumDetails] = useState({
        EBoundingBoxPointFlags: getEnumDetailsList(MapCore.IMcSymbolicItem.EBoundingBoxPointFlags),
        EMcPointCoordSystem: getEnumDetailsList(MapCore.EMcPointCoordSystem),
    });

    const saveData = (event: any) => {
        runCodeSafely(() => {
            let class_name: string = event.originalEvent?.currentTarget?.className;
            if (class_name?.includes("p-dropdown-item")) {
                props.setDialogPropertiesCallback(event.target.name, event.target.value.theEnum);
                return;
            }
            if (class_name?.includes("p-multiselect-item")) {
                let bitField: number = 0;
                if (event.target.value?.length > 0) {
                    for (let index: number = 0; index < event.target.value.length; index++) {
                        bitField |= event.target.value[index]!.code;
                    }
                    props.setDialogPropertiesCallback(event.target.name, bitField)
                    return;
                }
            }
            if (event.target.type === "checkbox") {
                props.setDialogPropertiesCallback(event.target.name, event.target.checked)
                return;
            }
            if (event.target.value.r != undefined) {
                let color = new MapCore.SMcBColor(event.target.value.r, event.target.value.g, event.target.value.b, event.target.value.a);
                props.setDialogPropertiesCallback(event.target.name, color)
                return;
            }
            props.setDialogPropertiesCallback(event.target.name, event.target.value)
        }, "picture.saveData => onChange")
    }
    const handleOKClick = (texture: MapCore.IMcTexture) => {
        props.setDialogPropertiesCallback("PicTexture", texture);
        props.setDialogPropertiesCallback("PicIsDefaultTexture", texture ? true : false);
    }

    return (
        <div className='object-props__general-container'>
            <div className='object-props__flex-and-row-between'>
                <label>Coordinate System:</label>
                <Dropdown className='object-props__input-width' name="PictureCoordSys" value={getEnumValueDetails(props.dialogProperties.PictureCoordSys, enumDetails.EMcPointCoordSystem)} onChange={saveData} options={enumDetails.EMcPointCoordSystem} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>width:</label>
                <InputNumber className='object-props__input-width' name="PicWidth" minFractionDigits={0} maxFractionDigits={5} value={props.dialogProperties.PicWidth} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>height:</label>
                <InputNumber className='object-props__input-width' name="PicHeight" minFractionDigits={0} maxFractionDigits={5} value={props.dialogProperties.PicHeight} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <EditTextureCtrl value={props.dialogProperties.PicTexture}
                onOk={(texture) => {
                    handleOKClick(texture)
                }} label='Line Texture:' texturePropertiesBase={generalService.ObjectProperties.PicTextureProperties}
                saveTexturePropertiesCB={(textureProp: TexturePropertiesBase) => { generalService.ObjectProperties.PicTextureProperties = textureProp }} />
            <div className='object-props__flex-and-row-between'>
                <label>Picture Rect Alignment:</label>
                <Dropdown className='object-props__input-width' name="PicRectAlignment" value={getEnumValueDetails(props.dialogProperties.PicRectAlignment, enumDetails.EBoundingBoxPointFlags)} onChange={saveData} options={enumDetails.EBoundingBoxPointFlags} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Sides Fill Color:</label>
                <ColorPickerCtrl className='object-props__input-width' alpha={true} name='PicTextureColor' value={props.dialogProperties.PicTextureColor} onChange={saveData} />
            </div>
            <div>
                <Checkbox inputId="PictureIsSizeFactor" name="PictureIsSizeFactor" onChange={saveData} checked={props.dialogProperties.PictureIsSizeFactor} />
                <label htmlFor="PictureIsSizeFactor">Is Size Factor</label>
            </div>
            <div>
                <Checkbox inputId="PictureIsUseTextureGeoReferencing" name="PictureIsUseTextureGeoReferencing" onChange={saveData} checked={props.dialogProperties.PictureIsUseTextureGeoReferencing} />
                <label htmlFor="PictureIsUseTextureGeoReferencing">Is Use Texture Geo Referencing </label>
            </div>
            <div>
                <Checkbox inputId="PictureNeverUpsideDown" name="PictureNeverUpsideDown" onChange={saveData} checked={props.dialogProperties.PictureNeverUpsideDown} />
                <label htmlFor="PictureNeverUpsideDown">Is Never Upside Down</label>
            </div>
        </div>
    )
}