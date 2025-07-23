import React, { useState } from 'react';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';
import ObjectPropertiesBase from '../../../../../propertiesBase/objectPropertiesBase';
import { InputText } from 'primereact/inputtext';
import EditFontCtrl from '../../../shared/editFontCtrl';
import { Checkbox } from 'primereact/checkbox';
import { useSelector } from 'react-redux';
import { AppState } from '../../../../../redux/combineReducer';
import ColorPickerCtrl from '../../../../shared/colorPicker';

export default function Text(props: { dialogProperties: ObjectPropertiesBase, setDialogPropertiesCallback: (key: string, value: any) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [enumDetails] = useState({
        BackgroundShape: getEnumDetailsList(MapCore.IMcTextItem.EBackgroundShape),
        NeverUpsideDown: getEnumDetailsList(MapCore.IMcTextItem.ENeverUpsideDownMode),
        PointCoordSystem: getEnumDetailsList(MapCore.EMcPointCoordSystem),
        EAxisXAlignment: getEnumDetailsList(MapCore.EAxisXAlignment),
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

    const handleGetFont = (font: MapCore.IMcFont, isSetAsDefault: boolean) => {
        runCodeSafely(() => {
            props.setDialogPropertiesCallback("TextFont", font);
            let isDefault = font == null ? false : true;
            props.setDialogPropertiesCallback("isFontSetAsDefault", isDefault);
        }, 'text.handleGetFont')
    }

    return (
        <div>
            <div className='object-props__general-container'>
                <EditFontCtrl
                    defaultFont={props.dialogProperties.isFontSetAsDefault ? props.dialogProperties.TextFont : null}
                    handleGetFont={(font: MapCore.IMcFont, isSetAsDefault: boolean) => handleGetFont(font, isSetAsDefault)}
                    label='Default Font:'
                />
                <div className='object-props__flex-and-row-between'>
                    <label>Coordinate System:</label>
                    <Dropdown className='object-props__input-width' name="TextCoordSys" value={getEnumValueDetails(props.dialogProperties.TextCoordSys, enumDetails.PointCoordSystem)} onChange={saveData} options={enumDetails.PointCoordSystem} optionLabel="name" />
                </div>
                <div className='object-props__flex-and-row-between'>
                    <label> Never Upside Down Mode:</label>
                    <Dropdown className='object-props__input-width' name="NeverUpsideDown" value={getEnumValueDetails(props.dialogProperties.NeverUpsideDown, enumDetails.NeverUpsideDown)} onChange={saveData} options={enumDetails.NeverUpsideDown} optionLabel="name" />
                </div>
                <div className='object-props__flex-and-row-between'>
                    <label>Text Scale:</label>
                    <div className='object-props__input-width object-props__flex-and-row-between form__items-center'>
                        <label htmlFor="TextScale.x"> X </label>
                        <InputNumber className='form__medium-width-input' name='TextScale.x' value={props.dialogProperties.TextScale.x} onValueChange={saveData} mode="decimal" min={-20} max={10000} />
                        <label htmlFor="TextScale.y"> Y </label>
                        <InputNumber className='form__medium-width-input' name='TextScale.y' value={props.dialogProperties.TextScale.y} onValueChange={saveData} mode="decimal" min={0} max={10000} />
                    </div>
                </div>
                <div className='object-props__flex-and-row-between'>
                    <label htmlFor="TextMargin" > Margin: </label>
                    <InputNumber className='object-props__input-width' name='TextMargin' value={props.dialogProperties.TextMargin} onValueChange={saveData} mode="decimal" />
                </div>
                <div className='object-props__flex-and-row-between'>
                    <label htmlFor="TextMarginY" > Margin Y: </label>
                    <InputNumber className='object-props__input-width' name='TextMarginY' value={props.dialogProperties.TextMarginY} onValueChange={saveData} mode="decimal" />
                </div>
                <div className='object-props__flex-and-row-between'>
                    <label > Text Color:</label>
                    <ColorPickerCtrl className='object-props__input-width' alpha={true} name='TextColor' value={props.dialogProperties.TextColor} onChange={saveData} />
                </div>
                <div className='object-props__flex-and-row-between'>
                    <label >Background Color:</label>
                    <ColorPickerCtrl className='object-props__input-width' alpha={true} name='TextBackgroundColor' value={props.dialogProperties.TextBackgroundColor} onChange={saveData} />
                </div>
                <div className='object-props__flex-and-row'>
                    <Checkbox inputId="ingredient1" name="isFontSetAsDefault" onChange={saveData} checked={props.dialogProperties.isFontSetAsDefault} />
                    <label htmlFor="ingredient1">is Font Set As Default</label>
                </div>
                <div className='object-props__flex-and-row-between'>
                    <label >Outline Color:</label>
                    <ColorPickerCtrl className='object-props__input-width' alpha={true} name='TextOutlineColor' value={props.dialogProperties.TextOutlineColor} onChange={saveData} />
                </div>
                <div className='object-props__flex-and-row-between'>
                    <label>Background Shape:</label>
                    <Dropdown className='object-props__input-width' name="BackgroundShape" value={getEnumValueDetails(props.dialogProperties.BackgroundShape, enumDetails.BackgroundShape)} onChange={saveData} options={enumDetails.BackgroundShape} optionLabel="name" />
                </div>
                <div className='object-props__flex-and-row-between'>
                    <label>Text</label>
                    <InputText className='object-props__input-width' name="TextString" value={props.dialogProperties.TextString} onChange={saveData}></InputText>
                </div>
            </div>
        </div>
    )
}