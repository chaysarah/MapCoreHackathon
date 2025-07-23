import React, { useState } from 'react';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import Footer from '../../../footerDialog';
import { closeDialog, setDialogType } from '../../../../../redux/MapCore/mapCoreAction';
import { useDispatch, useSelector } from 'react-redux';
import { enums } from 'mapcore-lib';
import generalService from '../../../../../services/general.service';
import { DialogTypesEnum } from '../../../../../tools/enum/enums';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';
import { Dialog } from 'primereact/dialog';
import { Button } from 'primereact/button';
import ObjectPropertiesBase from '../../../../../propertiesBase/objectPropertiesBase';
import EditTextureCtrl from '../../../shared/editTextureCtrl';
import TexturePropertiesBase from '../../../../../propertiesBase/texturePropertiesBase';
import { AppState } from '../../../../../redux/combineReducer';
import ColorPickerCtrl from '../../../../shared/colorPicker';

export default function LineBased(props: { dialogProperties: ObjectPropertiesBase, setDialogPropertiesCallback: (key: string, value: any) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [enumDetails] = useState({
        ItemLineStyle: getEnumDetailsList(MapCore.IMcLineBasedItem.ELineStyle),
        ItemShapeType: getEnumDetailsList(MapCore.IMcLineBasedItem.EShapeType),
        ItemFillStyle: getEnumDetailsList(MapCore.IMcLineBasedItem.EFillStyle),
        PointOrderReverseMode: getEnumDetailsList(MapCore.IMcLineBasedItem.EPointOrderReverseMode),
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
                <label>Line Width:</label>
                <InputNumber className='object-props__input-width' name="LineWidth" value={props.dialogProperties.LineWidth} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Line Style:</label>
                <Dropdown className='object-props__input-width' name="LineStyle" value={getEnumValueDetails(props.dialogProperties.LineStyle, enumDetails.ItemLineStyle)} onChange={saveData} options={enumDetails.ItemLineStyle} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Line Color:</label>
                <ColorPickerCtrl className='object-props__input-width' alpha={true} name='LineColor' value={props.dialogProperties.LineColor} onChange={saveData} />
            </div>
            <div style={{ whiteSpace: 'nowrap' }}>
                {/* <Button onClick={createLineTexture}>Create Texture</Button> */}
                <EditTextureCtrl label='Line Texture:' value={props.dialogProperties.LineTexture} saveTexturePropertiesCB={(textureProp: TexturePropertiesBase) => { generalService.ObjectProperties.LineTextureProperties = textureProp }}
                    onOk={(texture) => { props.setDialogPropertiesCallback("LineTexture", texture); }} texturePropertiesBase={generalService.ObjectProperties.LineTextureProperties} />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label> Line Smoothing Levels:</label>
                <InputNumber className='object-props__input-width' name="LineBasedSmoothingLevels" value={props.dialogProperties.LineBasedSmoothingLevels} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Outline Color:</label>
                <ColorPickerCtrl className='object-props__input-width' alpha={true} name='LineOutlineColor' value={props.dialogProperties.LineOutlineColor} onChange={saveData} />
            </div>
            <div className='object-props__flex-and-row-between'>
                <div>Outline Width:</div>
                <InputNumber className='object-props__input-width' name='LineOutlineWidth' value={props.dialogProperties.LineOutlineWidth} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label> Shape Type:</label>
                <Dropdown className='object-props__input-width' name="ShapeType" value={getEnumValueDetails(props.dialogProperties.ShapeType, enumDetails.ItemShapeType)} onChange={saveData} options={enumDetails.ItemShapeType} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label > Sides Fill Style:</label>
                <Dropdown className='object-props__input-width' name="SidesFillStyle" value={getEnumValueDetails(props.dialogProperties.SidesFillStyle, enumDetails.ItemFillStyle)} onChange={saveData} options={enumDetails.ItemFillStyle} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Sides Fill Color:</label>
                <ColorPickerCtrl className='object-props__input-width' alpha={true} name='SidesFillColor' value={props.dialogProperties.SidesFillColor} onChange={saveData} />
            </div>
            <EditTextureCtrl label='Sides Fill Texture:' value={props.dialogProperties.SidesFillTexture} saveTexturePropertiesCB={(textureProp: TexturePropertiesBase) => { generalService.ObjectProperties.SidesFillTextureProperties = textureProp }}
                texturePropertiesBase={generalService.ObjectProperties.SidesFillTextureProperties} onOk={(texture) => { props.setDialogPropertiesCallback("SidesFillTexture", texture); }}></EditTextureCtrl>

            <div className='object-props__flex-and-row-between'>
                <label >Vertical Height:</label>
                <InputNumber className='object-props__input-width' name="VerticalHeight" value={props.dialogProperties.VerticalHeight} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Point Order Reverse Mode:</label>
                <Dropdown className='object-props__input-width' name="PointOrderReverseMode" value={getEnumValueDetails(props.dialogProperties.PointOrderReverseMode, enumDetails.PointOrderReverseMode)} onChange={saveData} options={enumDetails.PointOrderReverseMode} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Line Texture Scale:</label>
                <InputNumber className='object-props__input-width' name="LineTextureScale" value={props.dialogProperties.LineTextureScale} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
        </div>

    )
}