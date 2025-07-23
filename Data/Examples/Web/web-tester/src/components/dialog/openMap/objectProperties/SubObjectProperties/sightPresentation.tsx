import React, { useEffect, useState } from 'react';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';
import { Checkbox } from 'primereact/checkbox';
import { DialogTypesEnum } from '../../../../../tools/enum/enums';
import ObjectPropertiesBase from '../../../../../propertiesBase/objectPropertiesBase';
import { useSelector } from 'react-redux';
import { AppState } from '../../../../../redux/combineReducer';
import ColorPickerCtrl from '../../../../shared/colorPicker';

export default function SightPresentation(props: { dialogProperties: ObjectPropertiesBase, setDialogPropertiesCallback: (key: string, value: any) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [enumDetails] = useState({
        EPointVisibility: getEnumDetailsList(MapCore.IMcSpatialQueries.EPointVisibility),
        ESightPresentationType: getEnumDetailsList(MapCore.IMcSightPresentationItemParams.ESightPresentationType),
        EQueryPrecision: getEnumDetailsList(MapCore.IMcSpatialQueries.EQueryPrecision),
    });

    const [colorsByVisibilityEnum, setColorsByVisibilityEnum] = useState(getEnumValueDetails(MapCore.IMcSpatialQueries.EPointVisibility.EPV_SEEN, enumDetails.EPointVisibility))
    const [colorsByVisibility_Color, setColorsByVisibility_Color] = useState(props.dialogProperties.SightPresentation.colorsByVisibility?.get(colorsByVisibilityEnum.theEnum))
    const [colorsByVisibility, setColorsByVisibility] = useState<Map<MapCore.IMcSpatialQueries.EPointVisibility, MapCore.SMcBColor>>()

    const updateItem = () => {
        const updatedItemsMap = new Map(props.dialogProperties.SightPresentation.colorsByVisibility);
        if (updatedItemsMap.has(colorsByVisibilityEnum.theEnum)) {
            updatedItemsMap.set((colorsByVisibilityEnum.theEnum), colorsByVisibility_Color);
        }
        setColorsByVisibility(updatedItemsMap);
    };

    useEffect(() => {
        updateItem()
    }, [colorsByVisibility_Color])

    useEffect(() => {
        props.setDialogPropertiesCallback("SightPresentation.colorsByVisibility", colorsByVisibility)
    }, [colorsByVisibility])

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
                let color

                color = new MapCore.SMcBColor(event.target.value.r, event.target.value.g, event.target.value.b, props.dialogProperties[event.target.name].a);
                props.setDialogPropertiesCallback(event.target.name, color)
                return;
            }
            props.setDialogPropertiesCallback(event.target.name, event.target.value)
        }, "general.saveData => onChange")
    }

    return (
        <div className='object-props__general-container'>
            <div className='object-props__flex-and-row-between'>
                <label> Type:</label>
                <Dropdown className="object-props__dropdown" name="SightPresentation.type" value={getEnumValueDetails(props.dialogProperties.SightPresentation.type, enumDetails.ESightPresentationType)} onChange={saveData} options={enumDetails.ESightPresentationType} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Query Precision:</label>
                <Dropdown className="object-props__dropdown" name="SightPresentation.precision" value={getEnumValueDetails(props.dialogProperties.SightPresentation.precision, enumDetails.EQueryPrecision)} onChange={saveData} options={enumDetails.EQueryPrecision} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Num Ellipse Rays:</label>
                <InputNumber name="SightPresentation.numEllipseRays" value={props.dialogProperties.SightPresentation.numEllipseRays} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Observed Height:</label>
                <InputNumber name="SightPresentation.observedHeight" value={props.dialogProperties.SightPresentation.observedHeight} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Observer Height:</label>
                <InputNumber name="SightPresentation.observerHeight" value={props.dialogProperties.SightPresentation.observerHeight} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Min Pitch:</label>
                <InputNumber name="SightPresentation.minPitch" value={props.dialogProperties.SightPresentation.minPitch} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Max Pitch:</label>
                <InputNumber name="SightPresentation.maxPitch" value={props.dialogProperties.SightPresentation.maxPitch} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Sight Texture Resolution:</label>
                <InputNumber name="SightPresentation.sightTextureResolution" value={props.dialogProperties.SightPresentation.sightTextureResolution} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div>
                <Checkbox inputId="ingredient1" name="SightPresentation.isObserverHeightAbsolute" onChange={saveData} checked={props.dialogProperties.SightPresentation.isObserverHeightAbsolute} />
                <label htmlFor="ingredient1">Is Observer Height Absolute</label>
            </div>
            <div>
                <Checkbox inputId="ingredient1" name="SightPresentation.isObservedHeightAbsolute" onChange={saveData} checked={props.dialogProperties.SightPresentation.isObservedHeightAbsolute} />
                <label htmlFor="ingredient1">Is Observed Height Absolute</label>
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Sight Color:</label>
                <Dropdown className="object-props__dropdown" name="precision" value={colorsByVisibilityEnum} options={enumDetails.EPointVisibility} optionLabel="name"
                    onChange={(e) => {
                        setColorsByVisibilityEnum(e.target.value);
                        setColorsByVisibility_Color(props.dialogProperties.SightPresentation.colorsByVisibility.get(e.target.value.theEnum))
                    }}
                />
            </div>
            <div className='object-props__flex-and-row-between form__items-center'>
                <label></label>
                <div style={{ width: `${globalSizeFactor * 1.5 * 10.5}vh` }} className='object-props__flex-and-row-between'>
                    <ColorPickerCtrl name='colorsByVisibility' value={colorsByVisibility_Color} onChange={(e) => {
                        setColorsByVisibility_Color({ ...colorsByVisibility_Color, r: (e.target.value as any).r, g: (e.target.value as any).g, b: (e.target.value as any).b });
                    }} />
                    <label>Alpha:</label>
                    <InputNumber className='form__medium-width-input' name='colorsByVisibilityAlpha' value={colorsByVisibility_Color.a}
                        onValueChange={(e) => {
                            setColorsByVisibility_Color({ ...colorsByVisibility_Color, a: e.target.value });
                        }} mode="decimal" />
                </div>
            </div>

        </div>
    )
}