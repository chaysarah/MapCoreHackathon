import { useState } from 'react';
import { Checkbox } from 'primereact/checkbox';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { getEnumDetailsList, getEnumValueDetails, MapCoreService, OpenMapService } from 'mapcore-lib';
import { useDispatch, useSelector } from 'react-redux';
import { InputText } from 'primereact/inputtext';
import { Fieldset } from 'primereact/fieldset';
import { Button } from 'primereact/button';
import { ListBox } from 'primereact/listbox';
import { Calendar } from 'primereact/calendar';
import { RadioButton } from 'primereact/radiobutton';

import '../styles/editModePropertiesDialog.css';
import { EditModeProperties } from '../editModePropertiesDialog';
import SelectExistingItem from '../../../../shared/ControlsForMapcoreObjects/itemCtrl/selectExistingItem';
import { runCodeSafely } from '../../../../../../common/services/error-handling/errorHandler';
import { setTypeEditModeDialogSecond } from '../../../../../../redux/EditMode/editModeAction';
import { AppState } from '../../../../../../redux/combineReducer';

export default function MapManipulationsOperations(props: { dialogProperties: EditModeProperties, setDialogPropertiesCallback: (key: string, value: any) => void }) {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    // const activeViewport: ViewportData = useSelector((state: AppState) => MapCoreData.findViewport(state.mapWindowReducer.activeCard));

    const [enumDetails] = useState({
        setVisibleArea3DOperation: getEnumDetailsList(MapCore.IMcMapCamera.ESetVisibleArea3DOperation),
    });

    const saveData = (event: any) => {
        runCodeSafely(() => {
            let value = event.target.type === "checkbox" ? event.target.checked : event.target.value;
            let class_name: string = event.originalEvent?.currentTarget?.className;
            if (class_name?.includes("p-dropdown-item")) {
                value = event.target.value.theEnum;
            }
            props.setDialogPropertiesCallback(event.target.name, value)
        }, "EditModePropertiesDialog/MapManipulationsOperations.saveData => onChange")
    }

    const showSelectExistingItemsDialog = (itemType: number, setSelectedItem: (item: MapCore.IMcObjectSchemeItem | null) => void) => {
        runCodeSafely(() => {
            dispatch(setTypeEditModeDialogSecond({
                secondDialogHeader: 'Select Existing Items',
                secondDialogComponent: <SelectExistingItem finalActionButton itemType={itemType} handleOKClick={(selectedItem: MapCore.IMcObjectSchemeItem) => {
                    dispatch(setTypeEditModeDialogSecond(undefined));
                    setSelectedItem(selectedItem);
                }} />
            }));
        }, "EditModePropertiesDialog/MapManipulationsOperations => selectStandAloneRectangle");
    }

    const setRectangleItemCallback = (propertyName: string) => {
        return function (item: MapCore.IMcObjectSchemeItem | null) {
            props.setDialogPropertiesCallback(propertyName, item as MapCore.IMcRectangleItem)
        }
    }

    const setLineItemCallback = (propertyName: string) => {
        return function (item: MapCore.IMcObjectSchemeItem | null) {
            props.setDialogPropertiesCallback(propertyName, item as MapCore.IMcLineItem)
        }
    }

    const setTextItemCallback = (propertyName: string) => {
        return function (item: MapCore.IMcObjectSchemeItem | null) {
            props.setDialogPropertiesCallback(propertyName, item as MapCore.IMcTextItem)
        }
    }

    return (
        <div style={{ height: '100%' }}>
            <div className="em-props__horizontal-fieldsets">
                <div>
                    <Fieldset className="form__space-between form__column-fieldset " legend="Navigate Map">
                        <div className='form__lower-margin'>
                            <Checkbox name="DrawLine" onChange={saveData} checked={props.dialogProperties.DrawLine} />
                            <label htmlFor="drawLine" className="ml-2 props__input-label">Draw Line</label>
                        </div>
                        <div className='form__lower-margin'>
                            <Checkbox name="OneOperationOnly" onChange={saveData} checked={props.dialogProperties.OneOperationOnly} />
                            <label htmlFor="oneOperationOnly" className="ml-2 props__input-label">One Operation Only</label>
                        </div>
                        <div className='form__lower-margin'>
                            <Checkbox name="NavigateMapWaitForMouseClick" onChange={saveData} checked={props.dialogProperties.NavigateMapWaitForMouseClick} />
                            <label htmlFor="navigateMapWaitForMouseClick" className="ml-2 props__input-label">Wait For Mouse Click</label>
                        </div>
                    </Fieldset>
                    <Fieldset className="form__space-between form__column-fieldset " legend="Dynamic Zoom">
                        <div className="form__lower-margin">
                            <label htmlFor="dynamicZoomMinScale" className="props__input-label">Min Scale:</label>
                            <InputNumber className="em-props__input-field em-props__left-offset" name="DynamicZoomMinScale" id="dynamicZoomMinScale" onValueChange={(e) => saveData(e)} mode="decimal" value={props.dialogProperties.DynamicZoomMinScale} />
                        </div>
                        <div className='form__lower-margin'>
                            <label className="props__input-label">Operation:</label>
                            <Dropdown name='DynamicZoomOperation' value={getEnumValueDetails(props.dialogProperties.DynamicZoomOperation, enumDetails.setVisibleArea3DOperation)} onChange={saveData} options={enumDetails.setVisibleArea3DOperation} optionLabel="name" />
                        </div>
                        <div className='form__lower-margin'>
                            <Checkbox name="DynamicZoomWaitForMouseClick" onChange={saveData} checked={props.dialogProperties.DynamicZoomWaitForMouseClick} />
                            <label htmlFor="dynamicZoomWaitForMouseClick" className="ml-2 props__input-label">Wait For Mouse Click</label>
                        </div>
                        <div>
                            <Button label='Select Rectangle' style={{ width: '30%', margin: `${globalSizeFactor * 0.3}vh` }} onClick={() => showSelectExistingItemsDialog(MapCore.IMcRectangleItem.NODE_TYPE, setRectangleItemCallback("DynamicZoomRectangle"))} />
                            <span className='em-props__offset'>{props.dialogProperties.DynamicZoomRectangle ? props.dialogProperties.DynamicZoomRectangle.GetName() : "Default"}</span>
                        </div>

                    </Fieldset>
                </div>
                <Fieldset className="form__space-between form__column-fieldset" legend="Distance Direction Measure">
                    <div className='form__lower-margin'>
                        <Checkbox name="ShowResult" onChange={saveData} checked={props.dialogProperties.ShowResult} />
                        <label htmlFor="showResult" className="ml-2 props__input-label">Show Result</label>
                    </div>
                    <div className='form__lower-margin'>
                        <Checkbox name="DistanceDirectionWaitForMouseClick" onChange={saveData} checked={props.dialogProperties.DistanceDirectionWaitForMouseClick} />
                        <label htmlFor="distanceDirectionWaitForMouseClick" className="ml-2 props__input-label">Wait For Mouse Click</label>
                    </div>
                    <div className='form__lower-margin'>
                        <Button label='Select Line' style={{ width: '30%', margin: `${globalSizeFactor * 0.3}vh` }} onClick={() => showSelectExistingItemsDialog(MapCore.IMcLineItem.NODE_TYPE, setLineItemCallback("DistanceDirectionMeasureLine"))} />
                    </div>
                    <div className='form__lower-margin'>
                        <Button label='Select Text' style={{ width: '30%', margin: `${globalSizeFactor * 0.3}vh` }} onClick={() => showSelectExistingItemsDialog(MapCore.IMcTextItem.NODE_TYPE, setTextItemCallback("DistanceDirectionMeasureText"))} />
                    </div>
                    <Fieldset className="form__space-between form__column-fieldset" legend={
                        <div style={{ display: 'flex', alignItems: 'center' }}>
                            <Checkbox name="EnableDistanceTextParams" onChange={saveData} checked={props.dialogProperties.EnableDistanceTextParams} />
                            <label className='ml-2 props__input-label' htmlFor="disableFieldset">Distance Text Params</label>
                        </div>} toggleable={false}>

                        <div className="form__lower-margin">
                            <label htmlFor="distanceFactor" className="props__input-label">Factor:</label>
                            <InputNumber className="em-props__input-field em-props__left-offset" name="DistanceFactor" id="distanceFactor" onValueChange={(e) => saveData(e)} mode="decimal"
                                value={props.dialogProperties.DistanceFactor}
                                disabled={!props.dialogProperties.EnableDistanceTextParams} />
                        </div>
                        <div className="form__lower-margin">
                            <label htmlFor="distanceDigits" className="props__input-label">Num Digits After Decimal Point:</label>
                            <InputNumber className="em-props__input-field em-props__left-offset" name="DistanceDigits" id="distanceDigits" onValueChange={(e) => saveData(e)} mode="decimal"
                                value={props.dialogProperties.DistanceDigits}
                                disabled={!props.dialogProperties.EnableDistanceTextParams} />
                        </div>
                        <div className="form__lower-margin">
                            <label htmlFor="distanceUnitsName">Units Name:</label>
                            <InputText className='offset em-props__input-field em-props__left-offset' name="DistanceUnitsName" id="distanceUnitsName" onChange={(e) => saveData(e)} value={props.dialogProperties.DistanceUnitsName}
                                disabled={!props.dialogProperties.EnableDistanceTextParams} />
                            <span className='em-props__offset'>
                                <Checkbox name="DistanceIsUnicode" onChange={saveData} checked={props.dialogProperties.DistanceIsUnicode} disabled={!props.dialogProperties.EnableDistanceTextParams} />
                                <label htmlFor="distanceIsUnicode" className="ml-2 props__input-label">Is Unicode</label>
                            </span>
                        </div>
                    </Fieldset>
                    <Fieldset className="form__space-between form__column-fieldset " legend={
                        <div style={{ display: 'flex', alignItems: 'center' }}>
                            <Checkbox name="EnableAngleTextParams" onChange={saveData} checked={props.dialogProperties.EnableAngleTextParams} />
                            <label className='ml-2 props__input-label' htmlFor="disableFieldset">Angle Text Params</label>
                        </div>} toggleable={false}>

                        <div className="form__lower-margin">
                            <label htmlFor="angleFactor" className="props__input-label">Factor:</label>
                            <InputNumber className="em-props__input-field em-props__left-offset" name="AngleFactor" id="angleFactor" onValueChange={(e) => saveData(e)} mode="decimal"
                                value={props.dialogProperties.AngleFactor}
                                disabled={!props.dialogProperties.EnableAngleTextParams} />
                        </div>
                        <div className="form__lower-margin">
                            <label htmlFor="angleDigits" className="props__input-label">Num Digits After Decimal Point:</label>
                            <InputNumber className="em-props__input-field em-props__left-offset" name="AngleDigits" id="angleDigits" onValueChange={(e) => saveData(e)} mode="decimal"
                                value={props.dialogProperties.AngleDigits}
                                disabled={!props.dialogProperties.EnableAngleTextParams} />
                        </div>
                        <div className="form__lower-margin">
                            <label htmlFor="angleUnitsName">Units Name:</label>
                            <InputText className='em-props__offset em-props__input-field em-props__left-offset' name="AngleUnitsName" id="angleUnitsName" onChange={(e) => saveData(e)} value={props.dialogProperties.AngleUnitsName}
                                disabled={!props.dialogProperties.EnableAngleTextParams} />
                            <span className='em-props__offset'>
                                <Checkbox name="AngleIsUnicode" onChange={saveData} checked={props.dialogProperties.AngleIsUnicode} disabled={!props.dialogProperties.EnableAngleTextParams} />
                                <label htmlFor="angleIsUnicode" className="ml-2 props__input-label">Is Unicode</label>
                            </span>
                        </div>
                    </Fieldset>
                    <Fieldset className="form__space-between form__column-fieldset " legend={
                        <div style={{ display: 'flex', alignItems: 'center' }}>
                            <Checkbox name="EnableHeightTextParams" onChange={saveData} checked={props.dialogProperties.EnableHeightTextParams} />
                            <label className='ml-2 props__input-label' htmlFor="disableFieldset">Height Text Params</label>
                        </div>} toggleable={false}>

                        <div className="form__lower-margin">
                            <label htmlFor="heightFactor" className="props__input-label">Factor:</label>
                            <InputNumber className="em-props__input-field em-props__left-offset" name="HeightFactor" id="heightFactor" onValueChange={(e) => saveData(e)} mode="decimal"
                                value={props.dialogProperties.HeightFactor}
                                disabled={!props.dialogProperties.EnableHeightTextParams} />
                        </div>
                        <div className="form__lower-margin">
                            <label htmlFor="heightDigits" className="props__input-label">Num Digits After Decimal Point:</label>
                            <InputNumber className="em-props__input-field em-props__left-offset" name="HeightDigits" id="heightDigits" onValueChange={(e) => saveData(e)} mode="decimal"
                                value={props.dialogProperties.HeightDigits}
                                disabled={!props.dialogProperties.EnableHeightTextParams} />
                        </div>
                        <div className="form__lower-margin">
                            <label htmlFor="heightUnitsName">Units Name:</label>
                            <InputText className='offset em-props__input-field em-props__left-offset' name="HeightUnitsName" id="heightUnitsName" onChange={(e) => saveData(e)} value={props.dialogProperties.HeightUnitsName}
                                disabled={!props.dialogProperties.EnableHeightTextParams} />
                            <span className='em-props__offset'>
                                <Checkbox name="HeightIsUnicode" onChange={saveData} checked={props.dialogProperties.HeightIsUnicode} disabled={!props.dialogProperties.EnableHeightTextParams} />
                                <label htmlFor="heightIsUnicode" className="ml-2 props__input-label">Is Unicode</label>
                            </span>
                        </div>
                    </Fieldset>
                    <Fieldset className="form__column-fieldset" legend="Azimuth Type">
                        <div className="flex align-items-center form__lower-margin">
                            <RadioButton className='em-props__radio-button' name='Azimuth' value="GEOGRAPHIC" onChange={saveData} checked={props.dialogProperties.Azimuth === 'GEOGRAPHIC'} />
                            <label className="ml-2 props__input-label">Geographic Azimuth</label>
                        </div>
                        <div className="flex align-items-center form__lower-margin">
                            <RadioButton className='em-props__radio-button' name='Azimuth' value="GRID COORDINATE SYSTEM" onChange={saveData} checked={props.dialogProperties.Azimuth === 'GRID COORDINATE SYSTEM'} />
                            <label className="ml-2 props__input-label">Grid Coordinate System Azimuth</label>
                        </div>
                        <Fieldset className="form__column-fieldset">
                            <div>Select From List:</div>
                            <ListBox name='rawVecParamSourceGridCS' optionLabel='name' value={props.dialogProperties.SelectedGridCoordinateSystem} onChange={saveData} options={props.dialogProperties.GridCoordinateSystemOptions}
                                disabled={props.dialogProperties.Azimuth !== 'GRID COORDINATE SYSTEM'} />
                            <div className="form__row-container form__justify-end">
                                <Button label="Create New" />
                                <Button name='GridCoordinateSystemOptions' label="Refresh" onClick={() =>
                                    props.setDialogPropertiesCallback("GridCoordinateSystemOptions", MapCoreService.getGridCoordinateSystems())} />
                            </div>
                        </Fieldset>
                        <div className="flex align-items-center form__lower-margin">
                            <RadioButton className='em-props__radio-button' name='Azimuth' inputId="magnetic" value="MAGNETIC" onChange={saveData} checked={props.dialogProperties.Azimuth === 'MAGNETIC'} />
                            <label htmlFor="magnetic" className="ml-2 props__input-label">Magnetic Azimuth</label>
                        </div>
                        <Fieldset className='form__column-fieldset'>
                            <div className="form__lower-margin">
                                <RadioButton className='em-props__radio-button' name='MagneticAzimuthTime' value="CURRENT" onChange={saveData} checked={props.dialogProperties.MagneticAzimuthTime === 'CURRENT'}
                                    disabled={props.dialogProperties.Azimuth !== 'MAGNETIC'} />
                                <label className="ml-2 props__input-label">Select Current Time</label>
                            </div>
                            <div className="em-props__form-group form__lower-margin">
                                <RadioButton className='em-props__radio-button' name='MagneticAzimuthTime' value="CUSTOM" onChange={saveData} checked={props.dialogProperties.MagneticAzimuthTime === 'CUSTOM'}
                                    disabled={props.dialogProperties.Azimuth !== 'MAGNETIC'} />
                                <label className="ml-2 props__input-label">Select Custom Time:</label>
                                <Calendar className='em-props__input-field' name='CustomTime' value={props.dialogProperties.CustomTime}
                                    onChange={(e) => saveData(e)} showIcon dateFormat="mm/dd/yy" placeholder="Select a date" />
                            </div>
                        </Fieldset>
                    </Fieldset>
                </Fieldset>
            </div>
        </div >
    )
}
