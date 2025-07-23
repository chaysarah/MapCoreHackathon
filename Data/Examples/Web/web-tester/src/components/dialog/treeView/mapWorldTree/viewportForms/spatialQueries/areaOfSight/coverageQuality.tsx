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
import { InputNumber } from "primereact/inputnumber";
import Vector3DGrid from "../../../../objectWorldTree/shared/vector3DGrid";
import DrawLineCtrl from "../../../../../shared/drawLineCtrl";
import { DialogTypesEnum } from "../../../../../../../tools/enum/enums";
import { AOSOperationProperties } from "./aOSOperation";

export class CoverageQualityProperties implements Properties {
    mcCurrentSpatialQueries: MapCore.IMcSpatialQueries;
    activeOverlay: MapCore.IMcOverlay;
    currentViewportData: ViewportData;
    standingRadius: number;
    walkingRadius: number;
    vehicleRadius: number;
    cellFactor: number;
    targetTypeList: { name: string; code: number; theEnum: any; }[]
    selectedTargetType: { name: string; code: number; theEnum: any; };
    linePoints: MapCore.SMcVector3D[];
    coverageQualityResult: string;

    static getDefault(p: any): CoverageQualityProperties {
        let { mcCurrentSpatialQueries } = p;

        let targetTypeList = getEnumDetailsList(MapCore.IMcSpatialQueries.ICoverageQuality.ETargetType);
        let selectedTargetType = getEnumValueDetails(MapCore.IMcSpatialQueries.ICoverageQuality.ETargetType.ETT_STANDING, targetTypeList);

        let defaults: CoverageQualityProperties = {
            mcCurrentSpatialQueries: mcCurrentSpatialQueries,
            activeOverlay: null,
            currentViewportData: null,
            standingRadius: 0,
            walkingRadius: 0,
            vehicleRadius: 0,
            cellFactor: 1,
            targetTypeList: targetTypeList,
            selectedTargetType: selectedTargetType,
            linePoints: [],
            coverageQualityResult: '',
        }
        return defaults;
    }
};

export default function CoverageQuality(props: { tabInfo: TabInfo }) {
    let { saveData, setApplyCallBack, setPropertiesCallback, tabProperties, getTabPropertiesByTabPropertiesClass } = props.tabInfo;
    const activeCard: number = useSelector((state: AppState) => state.mapWindowReducer.activeCard);

    useEffect(() => {
        runCodeSafely(() => {
            let activeOverlay = getActiveOverlay();
            setPropertiesCallback('activeOverlay', activeOverlay);
            let currentViewportData: ViewportData = getViewportData();
            setPropertiesCallback('currentViewportData', currentViewportData);
        }, 'SpatialQueriesForm/AreaOfSight/coverageQuality.useEffect');
    }, [activeCard])

    const getActiveOverlay = () => {
        let activeOverlay = null;
        let activeViewport = MapCoreData.findViewport(activeCard);
        runCodeSafely(() => {
            let typedMcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            if (typedMcCurrentSpatialQueries.GetInterfaceType() == MapCore.IMcMapViewport.INTERFACE_TYPE) {
                let mcCurrentViewport = tabProperties.mcCurrentSpatialQueries as MapCore.IMcMapViewport;
                activeOverlay = ObjectWorldService.findActiveOverlayByMcViewport(mcCurrentViewport);
            }
            else if (activeViewport) {
                activeOverlay = ObjectWorldService.findActiveOverlayByMcViewport(activeViewport.viewport);
            }
        }, 'SpatialQueriesForm/AreaOfSight/coverageQuality.getActiveOverlay');
        return activeOverlay;
    }
    const getViewportData = () => {
        let viewportData = null;
        runCodeSafely(() => {
            let typedMcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            if (typedMcCurrentSpatialQueries.GetInterfaceType() == MapCore.IMcMapViewport.INTERFACE_TYPE) {
                let mcCurrentViewport = tabProperties.mcCurrentSpatialQueries as MapCore.IMcMapViewport;
                viewportData = MapCoreData.viewportsData.find(testerVp => testerVp.viewport == mcCurrentViewport);
            }
            else if (activeCard) {
                viewportData = MapCoreData.findViewport(activeCard);
            }
        }, 'SpatialQueriesForm/AreaOfSight/coverageQuality.getViewportData');
        return viewportData;
    }
    const getLineObject = (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) => {
        runCodeSafely(() => {
            if (nExitCode == 1) {
                let points: MapCore.SMcVector3D[] = pObject.GetLocationPoints(0);
                pObject.Remove();
                setPropertiesCallback('linePoints', points);
            }
        }, 'SpatialQueriesForm/AreaOfSight/coverageQuality.getLineObject');
    }
    const savePointsTable = (...args) => {
        runCodeSafely(() => {
            const [locationPointsList, valid, selectedPoint, pointsType] = args;
            if (!_.isEqual(tabProperties[pointsType], locationPointsList)) {
                setPropertiesCallback(pointsType, locationPointsList);
            }
        }, 'SpatialQueriesForm/AreaOfSight/coverageQuality.savePointsTable');
    }
    const handleCalcCoverageClick = () => {
        runCodeSafely(() => {
            if (tabProperties.linePoints.length == 2) {
                let qualityParams = new MapCore.IMcSpatialQueries.ICoverageQuality.SQualityParams()
                qualityParams.fStandingRadius = tabProperties.standingRadius
                qualityParams.fWalkingRadius = tabProperties.walkingRadius
                qualityParams.fVehicleRadius = tabProperties.vehicleRadius
                qualityParams.uCellFactor = tabProperties.cellFactor
                let areaOfSightParams = getTabPropertiesByTabPropertiesClass(AreaOfSightProperties);
                let mapcoreRes: AreaOfSightResult = areaOfSightParams.mapcoreResults;
                let aOSMatrix = null;
                runMapCoreSafely(() => {
                    aOSMatrix = mapcoreRes.ppAreaOfSight.GetAreaOfSightMatrix(true);
                }, 'SpatialQueriesForm/AreaOfSight/coverageQuality.handleCalcCoverageClick => IMcSpatialQueries.IAreaOfSight.GetAreaOfSightMatrix', true)
                let visibilityColors = null;
                runMapCoreSafely(() => {
                    visibilityColors = mapcoreRes.ppAreaOfSight.GetVisibilityColors();
                }, 'SpatialQueriesForm/AreaOfSight/coverageQuality.handleCalcCoverageClick => IMcSpatialQueries.IAreaOfSight.GetVisibilityColors', true)
                let coverageQuality = null;
                runMapCoreSafely(() => {
                    coverageQuality = MapCore.IMcSpatialQueries.ICoverageQuality.Create(aOSMatrix, visibilityColors, qualityParams);
                }, 'SpatialQueriesForm/AreaOfSight/coverageQuality.handleCalcCoverageClick => IMcSpatialQueries.ICoverageQuality.Create', true)
                let minusVector = MapCore.SMcVector3D.Minus(tabProperties.linePoints[1], tabProperties.linePoints[0])
                let pYaw: { Value?: number } = {};
                let pPitch: { Value?: number } = {};
                MapCore.SMcVector3D.GetDegreeYawPitchFromForwardVector(minusVector, pYaw, pPitch);
                let coverageQualityResult: number = null;
                runMapCoreSafely(() => {
                    coverageQualityResult = coverageQuality.GetQuality(tabProperties.linePoints[0], tabProperties.selectedTargetType.theEnum, pYaw.Value);
                }, 'SpatialQueriesForm/AreaOfSight/coverageQuality.handleCalcCoverageClick => IMcSpatialQueries.ICoverageQuality.GetQuality', true)
                setPropertiesCallback('coverageQualityResult', coverageQualityResult);
            }
        }, 'SpatialQueriesForm/AreaOfSight/coverageQuality.handleCalcCoverageClick');
    }

    return (
        <div style={{ paddingLeft: '2%' }} className={`form__column-container ${getTabPropertiesByTabPropertiesClass(AreaOfSightProperties).mapcoreResults.ppAreaOfSight ? '' : 'form__disabled'}`}>
            <div className="form__flex-and-row-between">
                <div style={{ width: '35%' }} className="form__flex-and-row-between form__items-center">
                    <label>Standing Radius: </label>
                    <InputNumber className="form__narrow-input" name='standingRadius' value={tabProperties.standingRadius} onValueChange={saveData} />
                </div>
                <div style={{ width: '30%' }} className="form__flex-and-row-between form__items-center">
                    <label>Walking Radius: </label>
                    <InputNumber className="form__narrow-input" name='walkingRadius' value={tabProperties.walkingRadius} onValueChange={saveData} />
                </div>
                <div style={{ width: '30%' }} className="form__flex-and-row-between form__items-center">
                    <label>Vehicle Radius: </label>
                    <InputNumber className="form__narrow-input" name='vehicleRadius' value={tabProperties.vehicleRadius} onValueChange={saveData} />
                </div>
            </div>
            <div className="form__flex-and-row-between">
                <div style={{ width: '35%' }} className="form__column-container">
                    <div className="form__flex-and-row-between form__items-center">
                        <label>Cell Factor: </label>
                        <InputNumber className="form__narrow-input" name='cellFactor' value={tabProperties.cellFactor} onValueChange={saveData} />
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <label>Target Type: </label>
                        <Dropdown name="selectedTargetType" value={tabProperties.selectedTargetType} onChange={saveData} options={tabProperties.targetTypeList} optionLabel="name" />
                    </div>
                    <Button label="Calc Coverage" onClick={handleCalcCoverageClick} />
                    <div className="form__flex-and-row-between form__items-center form__disabled">
                        <label>Coverage Quality Result: </label>
                        <InputText className="form__narrow-input" name='coverageQualityResult' value={tabProperties.coverageQualityResult} />
                    </div>
                </div>
                <div style={{ width: '63%' }} className="form__column-container">
                    <Vector3DGrid disabledPointFromMap={tabProperties.activeOverlay ? false : true} ctrlHeight={5} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initLocationPointsList={tabProperties.linePoints} sendPointList={(...args) => savePointsTable(...args, 'linePoints')} />
                    <DrawLineCtrl label="Draw Line (2 points only)" className="form__aligm-self-center" disabled={tabProperties.activeOverlay ? false : true} handleDrawLineButtonClick={() => { }} activeViewport={tabProperties.currentViewportData} initItemResultsCB={getLineObject} />
                </div>
            </div>
        </div>
    )
}



