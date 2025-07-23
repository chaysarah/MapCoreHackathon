import React, { useState } from 'react';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import Footer from '../../../footerDialog';
import EditTextureCtrl from '../../../shared/editTextureCtrl';
import { useDispatch, useSelector } from 'react-redux';
import { closeDialog, setDialogType } from '../../../../../redux/MapCore/mapCoreAction';
import generalService from '../../../../../services/general.service';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';
import { DialogTypesEnum } from '../../../../../tools/enum/enums';
import ObjectPropertiesBase from '../../../../../propertiesBase/objectPropertiesBase';
import TexturePropertiesBase from '../../../../../propertiesBase/texturePropertiesBase';
import { AppState } from '../../../../../redux/combineReducer';
import ColorPickerCtrl from '../../../../shared/colorPicker';

export default function CloseShape(props: { dialogProperties: ObjectPropertiesBase, setDialogPropertiesCallback: (key: string, value: any) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [enumDetails] = useState({
        ItemFillStyle: getEnumDetailsList(MapCore.IMcLineBasedItem.EFillStyle),
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
        }, "general.saveData => onChange")
    }

    return (
        <div className='object-props__general-container'>
            <div className='object-props__flex-and-row-between'>
                <label >fill Color:</label>
                <ColorPickerCtrl className='object-props__input-width' alpha={true} name=' FillColor' value={props.dialogProperties.FillColor} onChange={saveData} />
            </div>
            <EditTextureCtrl label='fill Texture:' value={props.dialogProperties.FillTexture} saveTexturePropertiesCB={(textureProp: TexturePropertiesBase) => { generalService.ObjectProperties.ShapeTextureProperties = textureProp }}
                onOk={(texture) => { props.setDialogPropertiesCallback("FillTexture", texture); }} texturePropertiesBase={generalService.ObjectProperties.ShapeTextureProperties} ></EditTextureCtrl>
            <div className='object-props__flex-and-row-between'>
                <label >Fill Style:</label>
                <Dropdown className='object-props__input-width' name="FillStyle" value={getEnumValueDetails(props.dialogProperties.FillStyle, enumDetails.ItemFillStyle)} onChange={saveData} options={enumDetails.ItemFillStyle} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Text Scale:</label>
                <div className='object-props__flex-and-row-between object-props__input-width'>
                    <label htmlFor="SidesFillTextureScale.x"> X </label>
                    <InputNumber className='form__medium-width-input' name='SidesFillTextureScale.x' value={props.dialogProperties.SidesFillTextureScale.x} onValueChange={saveData} mode="decimal" min={-20} max={10000} />
                    <label htmlFor="SidesFillTextureScale.y"> Y </label>
                    <InputNumber className='form__medium-width-input' name='SidesFillTextureScale.y' value={props.dialogProperties.SidesFillTextureScale.y} onValueChange={saveData} mode="decimal" min={0} max={10000} />
                </div>
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Text Scale:</label>
                <div className='object-props__flex-and-row-between object-props__input-width'>
                    <label htmlFor="FillTextureScale.x"> X </label>
                    <InputNumber className='form__medium-width-input' name='FillTextureScale.x' value={props.dialogProperties.FillTextureScale.x} onValueChange={saveData} mode="decimal" min={-20} max={10000} />
                    <label htmlFor="FillTextureScale.y"> Y </label>
                    <InputNumber className='form__medium-width-input' name='FillTextureScale.y' value={props.dialogProperties.FillTextureScale.y} onValueChange={saveData} mode="decimal" min={0} max={10000} />
                </div>
            </div>
        </div>
    )
}