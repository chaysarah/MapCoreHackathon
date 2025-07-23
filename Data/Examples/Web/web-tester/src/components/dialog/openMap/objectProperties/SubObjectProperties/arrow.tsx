import React, { useState } from 'react';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { useDispatch } from 'react-redux';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';
import { DialogTypesEnum } from '../../../../../tools/enum/enums';
import ObjectPropertiesBase from '../../../../../propertiesBase/objectPropertiesBase';
export default function LineBased(props: { dialogProperties: ObjectPropertiesBase, setDialogPropertiesCallback: (key: string, value: any) => void }) {
    const [enumDetails] = useState({
        ItemCoordinateSystem: getEnumDetailsList(MapCore.EMcPointCoordSystem),
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

    return (
        <div className='object-props__general-container'>
            <div className='object-props__flex-and-row-between'>
                <label> Coordinate System:</label>
                <Dropdown className="object-props__dropdown" name="ArrowCoordSys" value={getEnumValueDetails(props.dialogProperties.ArrowCoordSys, enumDetails.ItemCoordinateSystem)} onChange={saveData} options={enumDetails.ItemCoordinateSystem} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Head Angel:</label>
                <InputNumber name="ArrowHeadAngle" value={props.dialogProperties.ArrowHeadAngle} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Head Size:</label>
                <InputNumber name="ArrowHeadSize" value={props.dialogProperties.ArrowHeadSize} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Gap Size:</label>
                <InputNumber name="ArrowGapSize" value={props.dialogProperties.ArrowGapSize} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
        </div>
    )
}