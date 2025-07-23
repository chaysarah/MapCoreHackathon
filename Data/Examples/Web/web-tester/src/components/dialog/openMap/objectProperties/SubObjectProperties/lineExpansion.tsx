import React, { useState } from 'react';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';
import ObjectPropertiesBase from '../../../../../propertiesBase/objectPropertiesBase';

export default function LineExpansion(props: { dialogProperties: ObjectPropertiesBase, setDialogPropertiesCallback: (key: string, value: any) => void }) {
    const [enumDetails] = useState({
        ItemCoordinateSystem: getEnumDetailsList(MapCore.EMcPointCoordSystem),
        ItemGeometryType: getEnumDetailsList(MapCore.IMcObjectSchemeItem.EGeometryType),
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
                <Dropdown className="object-props__dropdown" name="LineExpansionCoordinateSystem" value={getEnumValueDetails(props.dialogProperties.LineExpansionCoordinateSystem, enumDetails.ItemCoordinateSystem)} onChange={saveData} options={enumDetails.ItemCoordinateSystem} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Line Expansion Type:</label>
                <Dropdown className="object-props__dropdown" name="LineExpansionType" value={getEnumValueDetails(props.dialogProperties.LineExpansionType, enumDetails.ItemGeometryType)} onChange={saveData} options={enumDetails.ItemGeometryType} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label> Radius:</label>
                <InputNumber name="LineExpansionRadius" value={props.dialogProperties.LineExpansionRadius} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
        </div>
    )
}