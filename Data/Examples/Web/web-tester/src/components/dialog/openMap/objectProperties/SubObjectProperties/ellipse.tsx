import React, { useState } from 'react';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import Footer from '../../../footerDialog';
import { closeDialog, setDialogType } from '../../../../../redux/MapCore/mapCoreAction';
import { useDispatch } from 'react-redux';
import generalService from '../../../../../services/general.service';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';
import { DialogTypesEnum } from '../../../../../tools/enum/enums';
import ObjectPropertiesBase from '../../../../../propertiesBase/objectPropertiesBase';

export default function Ellipse(props: { dialogProperties: ObjectPropertiesBase, setDialogPropertiesCallback: (key: string, value: any) => void }) {

    const [enumDetails] = useState({
        EllipseType: getEnumDetailsList(MapCore.IMcObjectSchemeItem.EGeometryType),
        EllipseDefinition: getEnumDetailsList(MapCore.IMcObjectSchemeItem.EEllipseDefinition),
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
                <Dropdown className="object-props__dropdown" name="EllipseCoordSys" value={getEnumValueDetails(props.dialogProperties.EllipseCoordSys, enumDetails.PointCoordSystem)} onChange={saveData} options={enumDetails.PointCoordSystem} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Ellipse Type:</label>
                <Dropdown className="object-props__dropdown" name="ArcEllipseType" value={getEnumValueDetails(props.dialogProperties.ArcEllipseType, enumDetails.EllipseType)} onChange={saveData} options={enumDetails.EllipseType} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Ellipse Definition:</label>
                <Dropdown className="object-props__dropdown" name="ArcEllipseDefinition" value={getEnumValueDetails(props.dialogProperties.ArcEllipseDefinition, enumDetails.EllipseDefinition)} onChange={saveData} options={enumDetails.EllipseDefinition} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label htmlFor="EllipseStartAngle"> Start Angle </label>
                <InputNumber name='EllipseStartAngle' value={props.dialogProperties.EllipseStartAngle} onValueChange={saveData} mode="decimal" min={-20} max={10000} />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label htmlFor="EllipseEndAngle"> End Angle </label>
                <InputNumber name='EllipseEndAngle' value={props.dialogProperties.EllipseEndAngle} onValueChange={saveData} mode="decimal" min={0} max={10000} />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label htmlFor="EllipseInnerRadiusFactor"> Inner Radius Factor</label>
                <InputNumber name='EllipseInnerRadiusFactor' value={props.dialogProperties.EllipseInnerRadiusFactor} onValueChange={saveData} mode="decimal" min={0} max={10000} />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label htmlFor="EllipseInnerRadiusFactor"> Ellipse Radius X</label>
                <InputNumber name='EllipseRadiusX' value={props.dialogProperties.EllipseRadiusX} onValueChange={saveData} mode="decimal" min={0} />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label htmlFor="EllipseInnerRadiusFactor"> Ellipse Radius Y</label>
                <InputNumber name='EllipseRadiusY' value={props.dialogProperties.EllipseRadiusY} onValueChange={saveData} mode="decimal" min={0} />
            </div>
        </div>
    )
}