import { useEffect, useState } from "react";
import { Properties } from "../../../../../dialog";
import { TabInfo } from "../../../../../shared/tabCtrls/tabModels";
import Vector3DFromMap from "../../../../objectWorldTree/shared/Vector3DFromMap";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../../common/services/error-handling/errorHandler";
import { InputText } from "primereact/inputtext";
import { MapCoreData, ObjectWorldService, getEnumValueDetails, getEnumDetailsList, ViewportData } from 'mapcore-lib';
import { Dropdown } from "primereact/dropdown";
import ColorPickerCtrl from "../../../../../../shared/colorPicker";
import { Button } from "primereact/button";
import { AreaOfSightProperties, AreaOfSightResult } from "./structs";
import { SpatialQueryName } from "../spatialQueriesFooter";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../../../redux/combineReducer";
import _ from 'lodash';
import { ListBox } from "primereact/listbox";
import { Checkbox } from "primereact/checkbox";
import { ConfirmDialog } from "primereact/confirmdialog";

export class MatrixOperationProperties implements Properties {
    mcCurrentSpatialQueries: MapCore.IMcSpatialQueries;
    matricesList: { index: number, label: string, areaOfSightMatrix: MapCore.IMcSpatialQueries.SAreaOfSightMatrix }[];
    selectedMatrices: { index: number, label: string, areaOfSightMatrix: MapCore.IMcSpatialQueries.SAreaOfSightMatrix }[];
    isFillPointsVis: boolean;
    scoutersSumTypeList: { name: string, code: number, theEnum: MapCore.IMcSpatialQueries.EScoutersSumType }[];
    selectedScoutersSumType: { name: string, code: number, theEnum: MapCore.IMcSpatialQueries.EScoutersSumType };
    areSameRect: boolean;
    confirmDialogVisible: boolean;
    confirmDialogMessage: string;

    static getDefault(p: any): MatrixOperationProperties {
        let { mcCurrentSpatialQueries } = p;
        let scoutersSumTypeEnumList = getEnumDetailsList(MapCore.IMcSpatialQueries.EScoutersSumType);
        let selectedScoutersSumType = getEnumValueDetails(MapCore.IMcSpatialQueries.EScoutersSumType.ESST_ALL, scoutersSumTypeEnumList);

        let defaults: MatrixOperationProperties = {
            mcCurrentSpatialQueries: mcCurrentSpatialQueries,
            matricesList: [],
            selectedMatrices: [],
            isFillPointsVis: false,
            scoutersSumTypeList: scoutersSumTypeEnumList,
            selectedScoutersSumType: selectedScoutersSumType,
            areSameRect: false,
            confirmDialogVisible: false,
            confirmDialogMessage: '',
        }
        return defaults;
    }
};

export default function MatrixOperation(props: { tabInfo: TabInfo }) {
    let { saveData, setApplyCallBack, setPropertiesCallback, tabProperties, getTabPropertiesByTabPropertiesClass } = props.tabInfo;
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const handleCloneClick = () => {
        runCodeSafely(() => {
            if (tabProperties.selectedMatrices.length == 1) {
                let cloneMatrix: MapCore.IMcSpatialQueries.SAreaOfSightMatrix = null;
                runMapCoreSafely(() => {
                    cloneMatrix = MapCore.IMcSpatialQueries.CloneAreaOfSightMatrix(tabProperties.selectedMatrices[0].areaOfSightMatrix, tabProperties.isFillPointsVis);
                }, 'SpatialQueriesForm/AreaOfSight/MatrixOperation.handleCloneClick => IMcSpatialQueries.CloneAreaOfSightMatrix', true)
                let matrixToList = { index: tabProperties.matricesList.length, label: `Clone of ${tabProperties.selectedMatrices[0].label}`, areaOfSightMatrix: cloneMatrix }
                setPropertiesCallback('matricesList', [...tabProperties.matricesList, matrixToList]);
            }
            else {
                setPropertiesCallback('confirmDialogVisible', true)
                setPropertiesCallback('confirmDialogMessage', 'Need checked 1 item from list')
            }
        }, 'SpatialQueriesForm/AreaOfSight/MatrixOperation.handleCloneClick');
    }
    const handleSumMatricesClick = () => {
        runCodeSafely(() => {
            if (tabProperties.selectedMatrices.length == 2) {
                runMapCoreSafely(() => {
                    MapCore.IMcSpatialQueries.SumAreaOfSightMatrices(tabProperties.selectedMatrices[0].areaOfSightMatrix, tabProperties.selectedMatrices[1].areaOfSightMatrix, tabProperties.selectedScoutersSumType.theEnum);
                }, 'SpatialQueriesForm/AreaOfSight/MatrixOperation.handleSumMatricesClick => IMcSpatialQueries.SumAreaOfSightMatrices', true)
                let matrixToList = { index: tabProperties.matricesList.length, label: `Sum of ${tabProperties.selectedMatrices[0].label} And ${tabProperties.selectedMatrices[0].label}`, areaOfSightMatrix: tabProperties.selectedMatrices[0].areaOfSightMatrix }
                setPropertiesCallback('matricesList', [...tabProperties.matricesList, matrixToList]);
            }
            else {
                setPropertiesCallback('confirmDialogVisible', true)
                setPropertiesCallback('confirmDialogMessage', 'Need checked 2 items from list')
            }
        }, 'SpatialQueriesForm/AreaOfSight/MatrixOperation.handleSumMatricesClick');
    }
    const handleAreSameMatricesClick = () => {
        runCodeSafely(() => {
            if (tabProperties.selectedMatrices.length == 2) {
                let areSame = null;
                runMapCoreSafely(() => {
                    areSame = MapCore.IMcSpatialQueries.AreSameRectAreaOfSightMatrices(tabProperties.selectedMatrices[0].areaOfSightMatrix, tabProperties.selectedMatrices[1].areaOfSightMatrix);
                }, 'SpatialQueriesForm/AreaOfSight/MatrixOperation.handleAreSameMatricesClick => IMcSpatialQueries.AreSameRectAreaOfSightMatrices', true);
                setPropertiesCallback('areSameRect', areSame)
            }
            else {
                setPropertiesCallback('confirmDialogVisible', true)
                setPropertiesCallback('confirmDialogMessage', 'Need checked 2 items from list')
            }
        }, 'SpatialQueriesForm/AreaOfSight/MatrixOperation.handleAreSameMatricesClick');
    }

    return (
        <div style={{ paddingLeft: '2%' }} className={`form__flex-and-row-between ${getTabPropertiesByTabPropertiesClass(AreaOfSightProperties).mapcoreResults.ppAreaOfSight ? '' : 'form__disabled'}`}>
            <div style={{ width: '39%' }}>
                <span style={{ textDecoration: 'underline', padding: `${globalSizeFactor * 0.7}vh` }}>Area OF Sight Matrix List: </span>
                <ListBox emptyMessage={() => { return <div></div> }} multiple listStyle={{ minHeight: `${globalSizeFactor * 13.7}vh`, maxHeight: `${globalSizeFactor * 13.7}vh`, }} style={{ width: `${globalSizeFactor * 30}vh` }} name='selectedMatrices' optionLabel='label' value={tabProperties.selectedMatrices} onChange={saveData} options={tabProperties.matricesList} />
            </div>
            <div style={{ width: '59%' }} className="form__column-container form__justify-center">
                <div className="form__flex-and-row-between form__items-center">
                    <div style={{ width: '49%' }} className="form__flex-and-row form__items-center">
                        <Checkbox id="isFillPointsVis" name="isFillPointsVis" checked={tabProperties.isFillPointsVis} onChange={saveData} />
                        <label htmlFor="isFillPointsVis">Fill Points Visibility</label>
                    </div>
                    <Button style={{ width: '49%' }} label='Clone Area Of Sight Matrix' onClick={handleCloneClick} />
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                        <span>Scouters Sum Type:</span>
                        <Dropdown style={{ width: '55%' }} name="selectedScoutersSumType" value={tabProperties.selectedScoutersSumType} onChange={saveData} options={tabProperties.scoutersSumTypeList} optionLabel="name" />
                    </div>
                    <Button style={{ width: '49%' }} label='Sum Area Of Sight Matrices' onClick={handleSumMatricesClick} />
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <Button style={{ width: '49%' }} label='Are Smae Rect Area Of Sight' onClick={handleAreSameMatricesClick} />
                    <div style={{ width: '49%' }} className="form__flex-and-row form__items-center">
                        <Checkbox id="areSameRect" name="areSameRect" checked={tabProperties.areSameRect} onChange={saveData} />
                        <label htmlFor="areSameRect">Are Same Rect</label>
                    </div>
                </div>
            </div>

            <ConfirmDialog
                contentClassName='form__confirm-dialog-content'
                message={tabProperties.confirmDialogMessage}
                header=''
                footer={<div></div>}
                visible={tabProperties.confirmDialogVisible}
                onHide={e => { setPropertiesCallback('confirmDialogVisible', false) }}
            />
        </div>
    )
}