import React, { useState, useEffect } from 'react';
import { Checkbox } from 'primereact/checkbox';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { MultiSelect } from 'primereact/multiselect';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import Footer from '../../../footerDialog';
import { useDispatch, useSelector } from 'react-redux';
import { closeDialog, setDialogType } from '../../../../../redux/MapCore/mapCoreAction';
import generalService from '../../../../../services/general.service';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';
import { InputText } from 'primereact/inputtext';
import { DialogTypesEnum } from '../../../../../tools/enum/enums';
import ObjectPropertiesBase from '../../../../../propertiesBase/objectPropertiesBase';
import { AppState } from '../../../../../redux/combineReducer';

export default function General(props: { dialogProperties: ObjectPropertiesBase, setDialogPropertiesCallback: (key: string, value: any) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [enumDetails] = useState({
        itemsSubType: getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags),
        drawPriority: getEnumDetailsList(MapCore.IMcSymbolicItem.EDrawPriorityGroup),
        pointCoordSystem: getEnumDetailsList(MapCore.EMcPointCoordSystem),
        actionOptions: getEnumDetailsList(MapCore.IMcConditionalSelector.EActionOptions)
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
                let color = new MapCore.SMcBColor(event.target.value.r, event.target.value.g, event.target.value.b, props.dialogProperties[event.target.name].a);
                props.setDialogPropertiesCallback(event.target.name, color)
                return;
            }
            props.setDialogPropertiesCallback(event.target.name, event.target.value)
        }, "general.saveData => onChange")
    }
    let [value_id, setValue_id] = useState<any>("");
    let [value_startIndex, setValue_startIndex] = useState<any>("");
    let [newValue, setNewValue] = useState<MapCore.IMcProperty.SArrayPropertySubItemData>();

    useEffect(() => {
        props.setDialogPropertiesCallback("SubItemsData", newValue);
    }, [newValue])

    useEffect(() => {
        runCodeSafely(() => { }, "ObjectProperties.general.useEffect")
        let s1 = "";
        let s2 = "";
        for (let index = 0; index < props.dialogProperties.SubItemsData?.aElements?.length; index++) {
            s1 += props.dialogProperties.SubItemsData.aElements[index].uSubItemID + " ";
            s2 += props.dialogProperties.SubItemsData.aElements[index].nPointsStartIndex + " ";
        }
        setValue_id(s1)
        setValue_startIndex(s2)
    }, [])

    const saveSubItemsData = (id: any, startIndex: any) => {
        let newValue = new MapCore.IMcProperty.SArrayPropertySubItemData();
        runCodeSafely(() => {
            let idArray = id.trim().split(/\s+/).map(Number)
            let startIndexArray = startIndex.trim().split(/\s+/).map(Number)
            for (let index = 0; index < idArray.length; index++) {
                newValue.aElements[index] = new MapCore.SMcSubItemData(idArray[index], startIndexArray[index])
            }
        }, "SubItemArrayType.onOk")
        setNewValue(newValue)
    }

    return (
        <div className='object-props__general-container'>
            <div className='object-props__flex-and-row-between'>
                <label >Item Sub Type Bit Field:</label>
                <MultiSelect name="ItemSubTypeFlags" value={getEnumValueDetails(props.dialogProperties.ItemSubTypeFlags, enumDetails.itemsSubType)} onChange={saveData} options={enumDetails.itemsSubType} optionLabel="name"
                    placeholder="Select SubType" className="object-props__dropdown" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Location Coordinate System:</label>
                <Dropdown name="LocationCoordSys" value={getEnumValueDetails(props.dialogProperties.LocationCoordSys, enumDetails.pointCoordSystem)} onChange={saveData} options={enumDetails.pointCoordSystem} optionLabel="name" className="object-props__dropdown" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label > Draw Priority Group:</label>
                <Dropdown name="DrawPriorityGroup" value={getEnumValueDetails(props.dialogProperties.DrawPriorityGroup, enumDetails.drawPriority)} onChange={saveData} options={enumDetails.drawPriority} optionLabel="name" className="object-props__dropdown" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Texture Height Range:</label>
                <div className='object-props__flex-and-row-between' style={{ width: `${globalSizeFactor *1.5 * 9.5}vh` }}>
                    <label htmlFor="ingredient1" > Min: </label>
                    <InputNumber className='form__slim-input' name='LineTextureHeightRange.y' value={props.dialogProperties.LineTextureHeightRange.y} onValueChange={saveData} mode="decimal" min={-20} max={100} />
                    <label htmlFor="ingredient1"> Max: </label>
                    <InputNumber className='form__slim-input' name='LineTextureHeightRange.x' value={props.dialogProperties.LineTextureHeightRange.x} onValueChange={saveData} mode="decimal" min={0} max={100} />
                </div>
            </div>
            <div className='object-props__flex-and-row'>
                <Checkbox inputId="ingredient1" name="LocationRelativeToDtm" onChange={saveData} checked={props.dialogProperties.LocationRelativeToDtm} />
                <label htmlFor="ingredient1">Location Relative To DTM</label>
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Move If Blocked Max Change:</label>
                <InputNumber name="MoveIfBlockedMaxChange" value={props.dialogProperties.MoveIfBlockedMaxChange} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Move If Blocked Height Above Obstacle:</label>
                <InputNumber name="MoveIfBlockedHeightAboveObstacle" value={props.dialogProperties.MoveIfBlockedHeightAboveObstacle} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Blocked Transparency:</label>
                <InputNumber name="BlockedTransparency" value={props.dialogProperties.BlockedTransparency} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>

            <div className='object-props__flex-and-row-between'>
                <label >Move If Blocked Height:</label>
                <InputNumber name="MoveIfBlockedHeight" value={props.dialogProperties.MoveIfBlockedHeight} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Move If Blocked Max:</label>
                <InputNumber name="MoveIfBlockedMax" value={props.dialogProperties.MoveIfBlockedMax} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label > Transparency:</label>
                <InputNumber name="Transparency" value={props.dialogProperties.Transparency} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Draw Priority:</label>
                <InputNumber name="DrawPriority" value={props.dialogProperties.DrawPriority} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Conplanar 3d Priority:</label>
                <InputNumber name="Conplanar3dPriority" value={props.dialogProperties.Conplanar3dPriority} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Location Max Points:</label>
                <InputNumber name="LocationMaxPoints" value={props.dialogProperties.LocationMaxPoints} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Visibility Option:</label>
                <Dropdown name="VisibilityOption" value={getEnumValueDetails(props.dialogProperties.VisibilityOption, enumDetails.actionOptions)} onChange={saveData} options={enumDetails.actionOptions} optionLabel="name" className="object-props__dropdown" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Transform Option:</label>
                <Dropdown name="TransformOption" value={getEnumValueDetails(props.dialogProperties.TransformOption, enumDetails.actionOptions)} onChange={saveData} options={enumDetails.actionOptions} optionLabel="name" className="object-props__dropdown" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Sub Items Data:</label>
                <div style={{ display: 'flex', flexDirection: 'row', width: `${globalSizeFactor *1.5 * 18.5}vh` }}>
                    <span style={{ paddingRight: `${globalSizeFactor * 0.4}vh` }} className='object-props__flex-and-row-between'>
                        <label>ID: </label>
                        <InputText style={{ width: `${globalSizeFactor *1.5 * 7}vh` }} name='SubItemsData' value={value_id} onChange={(event) => { setValue_id(event.target.value); saveSubItemsData(event.target.value, value_startIndex) }} />
                    </span>
                    <span className='object-props__flex-and-row-between'>
                        <label>Start Index: </label>
                        <InputText style={{ width: `${globalSizeFactor *1.5 * 7}vh` }} name='SubItemsData' value={value_startIndex} onChange={(event) => { setValue_startIndex(event.target.value); saveSubItemsData(value_id, event.target.value) }}></InputText>
                    </span>
                </div>
            </div>
        </div>
    )
} 