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

export class PointVisibilityColorProperties implements Properties {
    mcCurrentSpatialQueries: MapCore.IMcSpatialQueries;
    activeOverlay: MapCore.IMcOverlay;
    point: MapCore.SMcVector3D;
    colorsVisibilityList: { name: string, code: number, theEnum: MapCore.IMcSpatialQueries.EPointVisibility }[];;
    selectedColorVisibility: { name: string, code: number, theEnum: MapCore.IMcSpatialQueries.EPointVisibility };
    color: MapCore.SMcBColor;

    static getDefault(p: any): PointVisibilityColorProperties {
        let { mcCurrentSpatialQueries } = p;

        let colorsVisibilityList = [...getEnumDetailsList(MapCore.IMcSpatialQueries.EPointVisibility), { name: 'Error!', code: -1, theEnum: null }];
        let selectedColorVisibility = getEnumValueDetails(MapCore.IMcSpatialQueries.EPointVisibility.EPV_SEEN, colorsVisibilityList);

        let defaults: PointVisibilityColorProperties = {
            mcCurrentSpatialQueries: mcCurrentSpatialQueries,
            activeOverlay: null,
            point: MapCore.v3Zero,
            colorsVisibilityList: colorsVisibilityList,
            selectedColorVisibility: selectedColorVisibility,
            color: new MapCore.SMcBColor(0, 255, 0, 192),

        }
        return defaults;
    }
};

export default function PointVisibilityColor(props: { tabInfo: TabInfo }) {
    let { saveData, setApplyCallBack, setPropertiesCallback, tabProperties, getTabPropertiesByTabPropertiesClass } = props.tabInfo;
    const activeCard: number = useSelector((state: AppState) => state.mapWindowReducer.activeCard);

    useEffect(() => {
        runCodeSafely(() => {
            let activeOverlay = getActiveOverlay();
            setPropertiesCallback('activeOverlay', activeOverlay);
        }, 'SpatialQueriesForm/AreaOfSight/PointVisibilityColorProperties.useEffect');
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
        }, 'SpatialQueriesForm/AreaOfSight/PointVisibilityColorProperties.getActiveOverlay');
        return activeOverlay;
    }
    const save3DVector = (...args) => {
        runCodeSafely(() => {
            const [point, flagNull, sectionPointType] = args;
            setPropertiesCallback(sectionPointType, point);
        }, 'SpatialQueriesForm/AreaOfSight/PointVisibilityColorProperties.save3DVector');
    }
    const handleGetClick = () => {
        runCodeSafely(() => {
            let areaOfSightParams = getTabPropertiesByTabPropertiesClass(AreaOfSightProperties);
            let activeOverlay = ObjectWorldService.findActiveOverlayByMcViewport(tabProperties.mcCurrentSpatialQueries as MapCore.IMcMapViewport);
            let convertedPoint = activeOverlay ? tabProperties.mcCurrentSpatialQueries.OverlayManagerToViewportWorld(tabProperties.point) : tabProperties.point;
            let mapcoreRes: AreaOfSightResult = areaOfSightParams.mapcoreResults;
            let pointColor: MapCore.SMcBColor = null;
            runMapCoreSafely(() => {
                pointColor = mapcoreRes.ppAreaOfSight.GetPointVisibilityColor(convertedPoint);
            }, 'SpatialQueriesForm/AreaOfSight/PointVisibilityColorProperties.handleGetClick => IMcSpatialQueries.IAreaOfSight.GetPointVisibilityColor', true)
            let colorVisibility = { name: 'Error!', code: -1, theEnum: null }
            let visibility = Array.from(areaOfSightParams.visibilityColors.entries()).find((value) => _.isEqual(value[1], pointColor))?.[0];
            colorVisibility = visibility ? getEnumValueDetails(visibility, tabProperties.colorsVisibilityList) : colorVisibility;
            setPropertiesCallback('selectedColorVisibility', colorVisibility)
            mapcoreRes.ppAreaOfSight && setPropertiesCallback('color', pointColor);
        }, 'SpatialQueriesForm/AreaOfSight/PointVisibilityColorProperties.handleGetClick');
    }

    return (
        <div style={{ paddingLeft: '2%', width: '80%' }} className={`form__column-container ${getTabPropertiesByTabPropertiesClass(AreaOfSightProperties).mapcoreResults.ppAreaOfSight ? '' : 'form__disabled'}`}>
            <div className="form__flex-and-row-between form__items-center">
                <span style={{ width: '30%' }}>Point:</span>
                <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.point} saveTheValue={(...args) => { save3DVector(...args, 'point') }} lastPoint={true} />
            </div>
            <div className="form__flex-and-row-between form__items-center form__disabled">
                <label style={{ width: '45%' }}>Point Visibility Color: </label>
                <div style={{ width: '55%' }} className="form__flex-and-row-between form__items-center">
                    <Dropdown style={{ width: '50%' }} name="selectedColorVisibility" value={tabProperties.selectedColorVisibility} options={tabProperties.colorsVisibilityList} optionLabel="name" />
                    <ColorPickerCtrl style={{ width: '50%' }} name='color' value={tabProperties.color} onChange={e => { }} alpha={true} />
                </div>
            </div>
            <br />
            <Button label='Get' onClick={handleGetClick} style={{ alignSelf: 'flex-end', width: '20%' }} />
        </div>
    )
}



