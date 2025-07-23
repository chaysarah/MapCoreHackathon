import React, { useState } from 'react';
import { Dropdown } from 'primereact/dropdown';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';
import { InputNumber } from 'primereact/inputnumber';
import { DialogTypesEnum } from '../../../../../tools/enum/enums';
import ObjectPropertiesBase from '../../../../../propertiesBase/objectPropertiesBase';

export default function Rectangle(props: { dialogProperties: ObjectPropertiesBase, setDialogPropertiesCallback: (key: string, value: any) => void }) {

    const [enumDetails] = useState({
        RectangleType: getEnumDetailsList(MapCore.IMcObjectSchemeItem.EGeometryType),
        RectangleDefinition: getEnumDetailsList(MapCore.IMcRectangleItem.ERectangleDefinition),
        PointCoordSystem: getEnumDetailsList(MapCore.EMcPointCoordSystem)

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
                <label>Coordinate System:</label>
                <Dropdown className="object-props__dropdown" name="RectangleCoordSys" value={getEnumValueDetails(props.dialogProperties.RectangleCoordSys, enumDetails.PointCoordSystem)} onChange={saveData} options={enumDetails.PointCoordSystem} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label> Rectangle Type:</label>
                <Dropdown className="object-props__dropdown" name="RectangleType" value={getEnumValueDetails(props.dialogProperties.RectangleType, enumDetails.RectangleType)} onChange={saveData} options={enumDetails.RectangleType} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Rectangle Definition:</label>
                <Dropdown className="object-props__dropdown" name="RectangleDefinition" value={getEnumValueDetails(props.dialogProperties.RectangleDefinition, enumDetails.RectangleDefinition)} onChange={saveData} options={enumDetails.RectangleDefinition} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label htmlFor="ArcEndAngle">Rect Radius X</label>
                <InputNumber name='RectRadiusX' value={props.dialogProperties.RectRadiusX} onValueChange={saveData} mode="decimal" min={0} max={10000} />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label htmlFor="ArcEndAngle"> Rect Radius Y</label>
                <InputNumber name='RectRadiusY' value={props.dialogProperties.RectRadiusY} onValueChange={saveData} mode="decimal" min={0} max={10000} />
            </div>
        </div >
    )
}