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
import { Checkbox } from "primereact/checkbox";
import { DataTable } from "primereact/datatable";
import { Column, ColumnEditorOptions } from "primereact/column";

export class ColorsSurroundingProperties implements Properties {
    mcCurrentSpatialQueries: MapCore.IMcSpatialQueries;
    activeOverlay: MapCore.IMcOverlay;
    point: MapCore.SMcVector3D;
    numVisX: number;
    numVisY: number;
    isHex: boolean;
    pointVisibilityColorsTable: any[];

    static getDefault(p: any): ColorsSurroundingProperties {
        let { mcCurrentSpatialQueries } = p;

        let defaults: ColorsSurroundingProperties = {
            mcCurrentSpatialQueries: mcCurrentSpatialQueries,
            activeOverlay: null,
            point: MapCore.v3Zero,
            numVisX: 0,
            numVisY: 0,
            isHex: false,
            pointVisibilityColorsTable: [],

        }
        return defaults;
    }
};

export default function ColorsSurrounding(props: { tabInfo: TabInfo }) {
    let { saveData, setApplyCallBack, setPropertiesCallback, tabProperties, getTabPropertiesByTabPropertiesClass } = props.tabInfo;
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const activeCard: number = useSelector((state: AppState) => state.mapWindowReducer.activeCard);

    useEffect(() => {
        runCodeSafely(() => {
            let activeOverlay = getActiveOverlay();
            setPropertiesCallback('activeOverlay', activeOverlay);
        }, 'SpatialQueriesForm/AreaOfSight/ColorsSurroundingProperties.useEffect');
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
        }, 'SpatialQueriesForm/AreaOfSight/ColorsSurroundingProperties.getActiveOverlay');
        return activeOverlay;
    }
    const save3DVector = (...args) => {
        runCodeSafely(() => {
            const [point, flagNull, sectionPointType] = args;
            setPropertiesCallback(sectionPointType, point);
        }, 'SpatialQueriesForm/AreaOfSight/ColorsSurroundingProperties.save3DVector');
    }
    const getColumns = () => {
        let columns = [{ field: `No`, header: '.No' }];
        runCodeSafely(() => {
            for (let index = 0; index < tabProperties.numVisX; index++) {
                columns = [...columns, { field: `${index}`, header: `${index + 1}` }]
            }
        }, 'SpatialQueriesForm/AreaOfSight/ColorsSurroundingProperties.getColumns')
        return columns;
    }
    function generateObjectArray(arrayLength, objectKeyLength, valuesArray) {
        const arrayOfObjects = [];

        for (let i = 0; i < arrayLength; i++) {
            const objectKeys = Array.from({ length: objectKeyLength }, (_, index) => (index).toString());
            let object = Object.fromEntries(objectKeys.map((key, index) => [key, valuesArray[index % valuesArray.length]]));
            object = { ...object, 'No': i + 1 }
            arrayOfObjects.push(object);
        }

        return arrayOfObjects;
    }
    const handleGetColorsSurroundingClick = () => {
        runCodeSafely(() => {
            let activeOverlay = ObjectWorldService.findActiveOverlayByMcViewport(tabProperties.mcCurrentSpatialQueries as MapCore.IMcMapViewport);
            let surroundingPoint = activeOverlay ? tabProperties.mcCurrentSpatialQueries.OverlayManagerToViewportWorld(tabProperties.point) : tabProperties.point;
            let areaOfSightParams = getTabPropertiesByTabPropertiesClass(AreaOfSightProperties);
            let mapcoreRes: AreaOfSightResult = areaOfSightParams.mapcoreResults;
            let pointVisibilityColors: Uint32Array = new Uint32Array();
            runMapCoreSafely(() => {
                pointVisibilityColors = mapcoreRes.ppAreaOfSight.GetPointVisibilityColorsSurrounding(surroundingPoint, tabProperties.numVisX, tabProperties.numVisY);
            }, 'SpatialQueriesForm/AreaOfSight/ColorsSurroundingProperties.handleCalcCoverageClick => IMcSpatialQueries.IAreaOfSight.GetPointVisibilityColorsSurrounding', true)
            let pointVisibilityColorsTable = generateObjectArray(tabProperties.numVisY, tabProperties.numVisX, pointVisibilityColors);
            setPropertiesCallback('pointVisibilityColorsTable', pointVisibilityColorsTable);
        }, 'SpatialQueriesForm/AreaOfSight/ColorsSurroundingProperties.handleGetColorsSurroundingClick');
    }
    const getFieldTemplate = (rowData: any, column: any) => {
        return column.field == 'No' ? rowData[column.field] :
            <InputText style={{ width: `${globalSizeFactor * 27 / tabProperties.numVisX}vh` }} type="text" value={rowData[column.field]} />;
    }
    return (
        <div style={{ paddingLeft: '2%' }} className={`form__flex-and-row-between ${getTabPropertiesByTabPropertiesClass(AreaOfSightProperties).mapcoreResults.ppAreaOfSight ? '' : 'form__disabled'}`}>
            <div style={{ width: '50%' }} className="form__column-container">
                <div className="form__flex-and-row-between form__items-center">
                    <span style={{ width: '30%' }}>Point:</span>
                    <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.point} saveTheValue={(...args) => { save3DVector(...args, 'point') }} lastPoint={true} />
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <label>Num Visibility Colors X: </label>
                    <InputNumber className="form__narrow-input" name='numVisX' value={tabProperties.numVisX} onValueChange={saveData} />
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <label>Num Visibility Colors Y: </label>
                    <InputNumber className="form__narrow-input" name='numVisY' value={tabProperties.numVisY} onValueChange={saveData} />
                </div>
                <div className="form__flex-and-row form__items-center">
                    <Checkbox id="isHex" name="isHex" checked={tabProperties.isHex} onChange={saveData} />
                    <label htmlFor="isHex">Hexadecimal Number</label>
                </div>
                <Button label="Get Point Visibility Colors Surrounding" onClick={handleGetColorsSurroundingClick} />
            </div>
            <div style={{ width: '48%' }}>
                <DataTable scrollable scrollHeight={`${globalSizeFactor * 15}vh`} tableStyle={{ width: `${globalSizeFactor * 30}vh`, maxWidth: `${globalSizeFactor * 30}vh` }} value={tabProperties.pointVisibilityColorsTable}>
                    {getColumns().map(({ field, header }) => {
                        return <Column style={{ width: `${globalSizeFactor * (30 / tabProperties.numVisX)}vh` }} key={field} field={field} header={header} body={getFieldTemplate} />;
                    })}
                </DataTable>
            </div>
        </div>
    )
}



