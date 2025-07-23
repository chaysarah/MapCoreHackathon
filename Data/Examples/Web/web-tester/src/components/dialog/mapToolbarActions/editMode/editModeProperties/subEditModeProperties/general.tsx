import React, { useState, useEffect } from 'react';
import { Checkbox } from 'primereact/checkbox';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { MultiSelect } from 'primereact/multiselect';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { useDispatch, useSelector } from 'react-redux';
import { runCodeSafely } from '../../../../../../common/services/error-handling/errorHandler';
import { Fieldset } from 'primereact/fieldset';
import '../styles/editModePropertiesDialog.css';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { EditModeProperties, KeyStepType } from '../editModePropertiesDialog';


// export default 
export default function General(props: {dialogProperties: EditModeProperties, setDialogPropertiesCallback: (key: string, value: any) => void}) {
    const dispatch = useDispatch();
    const [enumDetails] = useState({
        mouseMoveUsage: getEnumDetailsList(MapCore.IMcEditMode.EMouseMoveUsage),
        intersectionTargetType: getEnumDetailsList(MapCore.IMcSpatialQueries.EIntersectionTargetType).slice(1),
        keyStepType: getEnumDetailsList(MapCore.IMcEditMode.EKeyStepType)
    });

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.setDialogPropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value)
        }, "EditModePropertiesDialog/General.saveData => onChange")
    }

    const onKeyStepTypeEditorValueChange = (KeyStepProps: any, value: number) => {
        runCodeSafely(() => {
            let updatedKeyStepTypes = [...props.dialogProperties.KeyStepTypes];
            updatedKeyStepTypes[KeyStepProps.rowIndex][KeyStepProps.field] = value;
            props.setDialogPropertiesCallback("KeyStepTypes", updatedKeyStepTypes)
        }, "EditModePropertiesDialog/General.keyStepTypeInputTextEditor => onKeyStepTypeEditorValueChange")
    };

    const keyStepTypeInputTextEditor = (props: any) => {
        return (
            <InputNumber type="text" value={props.rowData[props.field]} 
                onChange={(e) => onKeyStepTypeEditorValueChange(props, e.value)} />
        );
    };

    return (
        <div style={{ height: '100%' }}>
            <div className="em-props__horizontal-fieldsets">
                <div>
                <Fieldset className="form__space-between form__column-fieldset " legend="Any Map Type">                
                <div className='form__lower-margin'>
                    <Checkbox inputId="ingredient1" name="AutoScroll" onChange={saveData} checked={props.dialogProperties.AutoScroll} />
                    <label htmlFor="autoScroll" className="ml-2 props__input-label">Auto Scroll</label>
                </div>
                <div className='form__lower-margin'>
                    <Checkbox inputId="ingredient1" name="DiscardChanges" onChange={saveData} checked={props.dialogProperties.DiscardChanges} />
                    <label htmlFor="discardChanges" className="ml-2 props__input-label">Discard Changes</label>
                </div>
                <div className="form__lower-margin">
                    <label htmlFor="autoScrollMargineSize" className="props__input-label">Margine Size:</label>
                    <InputNumber className="em-props__input-field em-props__left-offset" name="AutoScrollMargineSize" id="autoScrollMargineSize" onValueChange={(e) => saveData(e)} mode="decimal" value={props.dialogProperties.AutoScrollMargineSize} />
                </div>
                <div className="form__lower-margin">
                    <label htmlFor="rotatePictureOffset" className="props__input-label">Rotate Picture Offset:</label>
                    <InputNumber className="em-props__input-field em-props__left-offset" name="RotatePictureOffset" id="rotatePictureOffset" onValueChange={(e) => saveData(e)} mode="decimal"  value={props.dialogProperties.RotatePictureOffset} />
                </div>
                <div className="form__lower-margin">
                    <label htmlFor="pointAndLineClickTolerance" className="props__input-label">Point And Line Click Tolerance:</label>
                    <InputNumber className="em-props__input-field em-props__left-offset" name="PointAndLineClickTolerance" id="pointAndLineClickTolerance" onValueChange={(e) => saveData(e)} mode="decimal"  value={props.dialogProperties.PointAndLineClickTolerance} />
                </div>
                <div className='form__lower-margin'>
                    <Checkbox inputId="ingredient1" name="RectangleResizeRelativeToCenter" onChange={saveData} checked={props.dialogProperties.RectangleResizeRelativeToCenter} />
                    <label htmlFor="rectangleResizeRelativeToCenter" className="ml-2 props__input-label">Rectangle Resize Relative To Center</label>
                </div>
                <div className="form__lower-margin">
                    <label htmlFor="lastExitStatus" className="props__input-label">Last Exit Status: {props.dialogProperties.LastExitStatus}</label>
                    {/* <InputNumber className="em-props__input-field em-props__left-offset" name="LastExitStatus" id="lastExitStatus" onValueChange={(e) => saveData(e)} mode="decimal"  value={props.dialogProperties.LastExitStatus}/> */}
                </div>
                <div className='form__lower-margin'>
                    <Checkbox inputId="ingredient1" name="AutoSuppressSightPresentationMapTilesWebRequests" onChange={saveData} checked={props.dialogProperties.AutoSuppressSightPresentationMapTilesWebRequests} />
                    <label htmlFor="autoSuppressSightPresentationMapTilesWebRequests" className="ml-2 props__input-label">Auto Suppress Sight Presentation Map Tiles Web Requests</label>
                </div>
                <Fieldset className="form__space-between" legend="Max Radius">      
                    <div className="form__lower-margin">
                        <label htmlFor="maxRadiusScreen" className="props__input-label">Max Radius Screen:</label>
                        <InputNumber className="em-props__input-field em-props__left-offset" name="MaxRadiusScreen" id="maxRadiusScreen" onValueChange={(e) => saveData(e)} mode="decimal"  value={props.dialogProperties.MaxRadiusScreen}/>
                    </div>
                    <div className="form__lower-margin">
                        <label htmlFor="maxRadiusImage" className="props__input-label">Max Radius Image:</label>
                        <InputNumber className="em-props__input-field em-props__left-offset" name="MaxRadiusImage" id="maxRadiusImage" onValueChange={(e) => saveData(e)} mode="decimal"  value={props.dialogProperties.MaxRadiusImage}/>
                    </div>
                    <div className="form__lower-margin">
                        <label htmlFor="maxRadiusWorld" className="props__input-label">Max Radius World:</label>
                        <InputNumber className="em-props__input-field em-props__left-offset" name="MaxRadiusWorld" id="maxRadiusWorld" onValueChange={(e) => saveData(e)} mode="decimal"  value={props.dialogProperties.MaxRadiusWorld}/>
                    </div>          
                </Fieldset>
                <Fieldset className="form__space-between" legend="Multi Point Item">      
                    <div className="form__lower-margin">
                        <label htmlFor="maxNumOfPoints" className="props__input-label">Max Num Of Item Points:</label>
                        <InputNumber className="em-props__input-field em-props__left-offset" name="MaxNumOfPoints" id="maxNumOfPoints" onValueChange={(e) => saveData(e)} mode="decimal"  value={props.dialogProperties.MaxNumOfPoints}/>
                    </div> 
                    <div className='form__lower-margin'>
                        <Checkbox inputId="ingredient1" name="ForceMaxPoints" onChange={saveData} checked={props.dialogProperties.ForceMaxPoints} />
                        <label htmlFor="forceMaxPoints" className="ml-2 props__input-label">Force Max Points</label>
                    </div>        
                    <div className='form__lower-margin'>
                        <label className="props__input-label">Mouse Move Usage:</label>
                        <Dropdown name="MouseMoveUsage" value={props.dialogProperties.MouseMoveUsage} onChange={saveData} options={enumDetails.mouseMoveUsage} optionLabel="name" />
                    </div>
                    <div className='form__lower-margin'>
                        <Checkbox inputId="ingredient1" name="EnableAddingNewPoints" onChange={saveData} checked={props.dialogProperties.EnableAddingNewPoints} />
                        <label htmlFor="enableAddingNewPoints" className="props__input-label">Enable Adding New Points</label>
                    </div>
                    <div className='form__lower-margin'>
                        <Checkbox inputId="ingredient1" name="EnableDistanceDirectionMeasure" onChange={saveData} checked={props.dialogProperties.EnableDistanceDirectionMeasure} />
                        <label htmlFor="enableDistanceDirectionMeasure" className="ml-2 props__input-label">Enable Distance Direction Measure</label>
                    </div> 
                </Fieldset>
            </Fieldset></div><div>
            <Fieldset className="form__space-between form__column-fieldset " legend="3D Map">
                <div className='form__lower-margin'>
                    <label className="props__input-label">Intersection Targets:</label>
                    <MultiSelect name="IntersectionTargets" value={props.dialogProperties.IntersectionTargets} onChange={saveData} options={enumDetails.intersectionTargetType} optionLabel="name"
                        placeholder="Choose Targets"/>
                </div>
                <div className="form__lower-margin">
                    <label htmlFor="cameraMinPitchRange" className="props__input-label">Camera Min Pitch Range:</label>
                    <InputNumber className="em-props__input-field em-props__left-offset" name="CameraMinPitchRange" id="cameraMinPitchRange" onValueChange={(e) => saveData(e)} mode="decimal"  value={props.dialogProperties.CameraMinPitchRange}/>
                </div> 
                <div className="form__lower-margin">
                    <label htmlFor="cmeraMaxPitchRange" className="props__input-label">Camera Max Pitch Range:</label>
                    <InputNumber className="em-props__input-field em-props__left-offset" name="CameraMaxPitchRange" id="cameraMaxPitchRange" onValueChange={(e) => saveData(e)} mode="decimal"  value={props.dialogProperties.CameraMaxPitchRange}/>
                </div> 
                <div className="form__lower-margin">
                    <label htmlFor="UtilityItemsOptionalScreenSize" className="props__input-label">Utility Items Optional Screen Size:</label>
                    <InputNumber className="em-props__input-field em-props__left-offset" name="UtilityItemsOptionalScreenSize" id="utilityItemsOptionalScreenSize" onValueChange={(e) => saveData(e)} mode="decimal"  value={props.dialogProperties.UtilityItemsOptionalScreenSize}/>
                </div> 
                <div className='form__lower-margin'>
                    <Checkbox inputId="ingredient1" name="UseLocalAxesAtEditing" onChange={saveData} checked={props.dialogProperties.UseLocalAxesAtEditing} />
                    <label htmlFor="useLocalAxesAtEditing" className="ml-2 props__input-label">Use Local Axes At Editing</label>
                </div>
                <div className='form__lower-margin'>
                    <Checkbox inputId="ingredient1" name="KeepScaleRatioAtEditing" onChange={saveData} checked={props.dialogProperties.KeepScaleRatioAtEditing} />
                    <label htmlFor="keepScaleRatioAtEditing" className="ml-2 props__input-label">Keep Scale Ratio Along Different Axes At Editing</label>
                </div>
            </Fieldset>
                <DataTable size='small' value={props.dialogProperties.KeyStepTypes} >
                    <Column field="stepType" header="Key step type" body={(rowData: KeyStepType) => { return enumDetails.keyStepType.find(enumDetails => enumDetails.code === rowData.stepType )?.name}}></Column>
                    <Column field="step" header="Step" body={(rowData: KeyStepType) => { return rowData.step }} editor={keyStepTypeInputTextEditor}></Column>
                </DataTable>
            </div>
            </div>
        </div >
    )
}