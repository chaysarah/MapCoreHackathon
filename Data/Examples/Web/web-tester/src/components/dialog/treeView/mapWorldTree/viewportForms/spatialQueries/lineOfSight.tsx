import { Fieldset } from "primereact/fieldset";
import { useDispatch, useSelector } from "react-redux";
import { Checkbox } from "primereact/checkbox";
import { InputNumber } from "primereact/inputnumber";
import { Divider } from "primereact/divider";
import { Button } from "primereact/button";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { useEffect } from "react";

import { MapCoreData, ObjectWorldService } from 'mapcore-lib';
import { ParamsProperties } from "./params";
import { QueryResult, SpatialQueryName } from "./spatialQueriesFooter";
import { TabInfo } from "../../../../shared/tabCtrls/tabModels";
import { Properties } from "../../../../dialog";
import Vector3DFromMap from "../../../objectWorldTree/shared/Vector3DFromMap";
import { AppState } from "../../../../../../redux/combineReducer";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { addQueryResultsTableRow } from "../../../../../../redux/MapWorldTree/mapWorldTreeActions";
import spatialQueriesService from "../../../../../../services/spatialQueries.service";

export class LineOfSightProperties implements Properties {
    mcCurrentSpatialQueries: MapCore.IMcSpatialQueries;
    activeOverlay: MapCore.IMcOverlay;
    target: MapCore.SMcVector3D;
    scouter: MapCore.SMcVector3D;
    isTarget: boolean;
    isScouter: boolean;
    maxPitchAngle: number;
    minPitchAngle: number;
    //Point visibility
    isMinimalScouter: boolean;
    isMinimalTarget: boolean;
    isPointVisibilityAsync: boolean;
    isTargetVisible: boolean;
    minimalTarget: number;
    minimalScouter: number;
    //Line Of Sight
    isLineAsync: boolean;
    crestClearenceAngle: number;
    crestClearenceDistance: number;
    lineOfSightPoints: MapCore.IMcSpatialQueries.SLineOfSightPoint[];


    static getDefault(p: any): LineOfSightProperties {
        let { mcCurrentSpatialQueries } = p;

        let defaults: LineOfSightProperties = {
            mcCurrentSpatialQueries: mcCurrentSpatialQueries,
            activeOverlay: null,
            target: MapCore.v3Zero,
            scouter: MapCore.v3Zero,
            isTarget: false,
            isScouter: false,
            maxPitchAngle: 90,
            minPitchAngle: -90,
            //Point visibility
            isMinimalScouter: false,
            isMinimalTarget: false,
            isPointVisibilityAsync: true,
            isTargetVisible: false,
            minimalTarget: 0,
            minimalScouter: 0,
            //Line Of Sight
            isLineAsync: true,
            crestClearenceAngle: 0,
            crestClearenceDistance: 0,
            lineOfSightPoints: [],
        }

        return defaults;
    }
};

export default function LineOfSight(props: { tabInfo: TabInfo }) {
    let { saveData, setApplyCallBack, setPropertiesCallback, tabProperties, getTabPropertiesByTabPropertiesClass } = props.tabInfo;
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const spatialQueriesResultsObjects = useSelector((state: AppState) => state.mapWorldTreeReducer.spatialQueriesResultsObjects)
    const mapWorldTree = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree)
    const activeCard: number = useSelector((state: AppState) => state.mapWindowReducer.activeCard);

    useEffect(() => {
        runCodeSafely(() => {
            let activeOverlay = getActiveOverlay();
            setPropertiesCallback('activeOverlay', activeOverlay);
        }, 'SpatialQueriesForm/LineOfSight.useEffect');
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
        }, 'SpatialQueriesForm/LineOfSight.getActiveOverlay');
        return activeOverlay;
    }
    const save3DVector = (...args) => {
        runCodeSafely(() => {
            const [point, flagNull, sectionPointType] = args;
            setPropertiesCallback(sectionPointType, point);
        }, 'SpatialQueriesForm/LineOfSight.save3DVector');
    }
    const getColumns = () => {
        let columns = [{ field: `no`, header: '.No' },
        { field: `x`, header: 'X' },
        { field: `y`, header: 'Y' },
        { field: `z`, header: 'Z' },
        { field: `visible`, header: 'Visible' }];
        return columns;
    }
    const getFlatLineOfSightPoints = () => {
        let flatLineOfSightArr = [];
        runCodeSafely(() => {
            flatLineOfSightArr = tabProperties.lineOfSightPoints?.map(({ Point: { x, y, z }, bVisible }, i) => ({ 'no': i, x, y, z, bVisible }));
        }, 'SpatialQueriesForm/LineOfSight.getFlatLineOfSightPoints');
        return flatLineOfSightArr;
    }
    //#region Set Results
    const setPointVisibilityQueryResults = (scouter: MapCore.SMcVector3D, isScouter: boolean, target: MapCore.SMcVector3D, isTarget: boolean, maxPitchAngle: number, minPitchAngle: number,
        isTargetVisible: boolean, pMinimalTarget: number, pMinimalScouter: number,
        isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            setPropertiesCallback('scouter', scouter)
            setPropertiesCallback('isScouter', isScouter)
            setPropertiesCallback('target', target)
            setPropertiesCallback('isTarget', isTarget)
            setPropertiesCallback('maxPitchAngle', maxPitchAngle)
            setPropertiesCallback('minPitchAngle', minPitchAngle)
            setPropertiesCallback('isTargetVisible', isTargetVisible)
            setPropertiesCallback('minimalTarget', pMinimalTarget)
            setPropertiesCallback('isMinimalTarget', pMinimalTarget ? true : false)
            setPropertiesCallback('minimalScouter', pMinimalScouter)
            setPropertiesCallback('isMinimalScouter', pMinimalScouter ? true : false)
            setPropertiesCallback('isPointVisibilityAsync', isAsync)

            spatialQueriesService.manageQueryResultObjects(tabProperties.mcCurrentSpatialQueries, SpatialQueryName.GetPointVisibility, scouter, isScouter, target, isTarget);
        }, 'SpatialQueriesForm/LineOfSight.setPointVisibilityQueryResults')
        if (!isFromTable) {
            let args = [scouter, isScouter, target, isTarget, maxPitchAngle, minPitchAngle,
                isTargetVisible, pMinimalTarget, pMinimalScouter, isAsync, true, errorMessage];
            let queryResult = new QueryResult(SpatialQueryName.GetPointVisibility,
                setPointVisibilityQueryResults, args, tabProperties.isPointVisibilityAsync, errorMessage);
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    const setLineOfSightQueryResults = (scouter: MapCore.SMcVector3D, isScouter: boolean, target: MapCore.SMcVector3D, isTarget: boolean, maxPitchAngle: number, minPitchAngle: number,
        lineOfSightPoints: MapCore.IMcSpatialQueries.SLineOfSightPoint[], pCrestAngle: number, pCrestDistance: number,
        isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            setPropertiesCallback('scouter', scouter)
            setPropertiesCallback('isScouter', isScouter)
            setPropertiesCallback('target', target)
            setPropertiesCallback('isTarget', isTarget)
            setPropertiesCallback('maxPitchAngle', maxPitchAngle)
            setPropertiesCallback('minPitchAngle', minPitchAngle)
            setPropertiesCallback('lineOfSightPoints', lineOfSightPoints)
            setPropertiesCallback('crestClearenceAngle', pCrestAngle)
            setPropertiesCallback('crestClearenceDistance', pCrestDistance)
            setPropertiesCallback('isLineAsync', isAsync)

            spatialQueriesService.manageQueryResultObjects(tabProperties.mcCurrentSpatialQueries, SpatialQueryName.GetLineOfSight, scouter, isScouter, target, isTarget, lineOfSightPoints);
        }, 'SpatialQueriesForm/LineOfSight.setLineOfSightQueryResults')
        if (!isFromTable) {
            let args = [scouter, isScouter, target, isTarget, maxPitchAngle, minPitchAngle,
                lineOfSightPoints, pCrestAngle, pCrestDistance, isAsync, true, errorMessage];
            let queryResult = new QueryResult(SpatialQueryName.GetLineOfSight,
                setLineOfSightQueryResults, args, tabProperties.isLineAsync, errorMessage);
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    //#endregion
    //#region Handle Query Error
    const handleAsyncPointVisibilityQueryError = (eErrorCode: MapCore.IMcErrors.ECode, scouter: MapCore.SMcVector3D, isScouter: boolean, target: MapCore.SMcVector3D, isTarget: boolean, maxPitchAngle: number, minPitchAngle: number) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/LineOfSight.handleAsyncPointVisibilityQueryError => IMcErrors.ErrorCodeToString', true)
            setPointVisibilityQueryResults(scouter, isScouter, target, isTarget, maxPitchAngle, minPitchAngle,
                false, 0, 0, true, false, errorMessage);
        }, 'SpatialQueriesForm/LineOfSight.handleAsyncPointVisibilityQueryError')
    }
    const handleAsyncLineOfSightQueryError = (eErrorCode: MapCore.IMcErrors.ECode, scouter: MapCore.SMcVector3D, isScouter: boolean, target: MapCore.SMcVector3D, isTarget: boolean, maxPitchAngle: number, minPitchAngle: number) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/LineOfSight.handleAsyncLineOfSightQueryError => IMcErrors.ErrorCodeToString', true)
            setLineOfSightQueryResults(scouter, isScouter, target, isTarget, maxPitchAngle, minPitchAngle,
                [], 0, 0, true, false, errorMessage);
        }, 'SpatialQueriesForm/LineOfSight.handleAsyncLineOfSightQueryError')
    }
    //#endregion
    //#region Handle Functoins
    const handleGetPointVisibilityClick = () => {
        let isTargetVisible = false;
        let pMinimalScouter: { Value?: number } = tabProperties.isMinimalScouter ? {} : null;
        let pMinimalTarget: { Value?: number } = tabProperties.isMinimalTarget ? {} : null;
        let errorMessage = '';
        runCodeSafely(() => {
            let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
            if (tabProperties.isPointVisibilityAsync) {
                queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((bIsTargetVisible: boolean, pdMinimalTargetHeightForVisibility: number, pdMinimalScouterHeightForVisibility: number) => {
                    setPointVisibilityQueryResults(tabProperties.scouter, tabProperties.isScouter, tabProperties.target, tabProperties.isTarget, tabProperties.maxPitchAngle, tabProperties.minPitchAngle,
                        bIsTargetVisible, pdMinimalTargetHeightForVisibility, pdMinimalScouterHeightForVisibility, tabProperties.isPointVisibilityAsync, false, errorMessage);
                }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                    handleAsyncPointVisibilityQueryError(eErrorCode, tabProperties.scouter, tabProperties.isScouter, tabProperties.target, tabProperties.isTarget, tabProperties.maxPitchAngle, tabProperties.minPitchAngle)
                }, 'SpatialQueriesForm/LineOfSight.handleGetPointVisibilityClick => IMcSpatialQueries.GetPointVisibility');
            }
            else {
                queryParams.pAsyncQueryCallback = null;
            }
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            runMapCoreSafely(() => {
                isTargetVisible = mcCurrentSpatialQueries.GetPointVisibility(tabProperties.scouter, tabProperties.isScouter, tabProperties.target, tabProperties.isTarget,
                    pMinimalTarget, pMinimalScouter, tabProperties.maxPitchAngle, tabProperties.minPitchAngle, queryParams);
            }, 'SpatialQueriesForm/LineOfSight.handleGetPointVisibilityClick => IMcSpatialQueries.GetPointVisibility', true, (error) => { errorMessage = String(error) })
        }, 'SpatialQueriesForm/LineOfSight.handleGetPointVisibilityClick');
        if (!tabProperties.isPointVisibilityAsync) {
            setPointVisibilityQueryResults(tabProperties.scouter, tabProperties.isScouter, tabProperties.target, tabProperties.isTarget, tabProperties.maxPitchAngle, tabProperties.minPitchAngle,
                isTargetVisible, pMinimalTarget?.Value, pMinimalScouter?.Value, tabProperties.isPointVisibilityAsync, false, errorMessage);
        }
    }
    const handleGetLineOfSightClick = () => {
        let lineOfSightPoints: MapCore.IMcSpatialQueries.SLineOfSightPoint[] = [];
        let pCrestDistance: { Value?: number } = {};
        let pCrestAngle: { Value?: number } = {};
        let errorMessage = '';
        runCodeSafely(() => {
            let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
            if (tabProperties.isLineAsync) {
                queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((aPoints: MapCore.IMcSpatialQueries.SLineOfSightPoint[], dCrestClearanceAngle: number, dCrestClearanceDistance: number) => {
                    setLineOfSightQueryResults(tabProperties.scouter, tabProperties.isScouter, tabProperties.target, tabProperties.isTarget, tabProperties.maxPitchAngle, tabProperties.minPitchAngle,
                        aPoints, dCrestClearanceAngle, dCrestClearanceDistance, tabProperties.isLineAsync, false, errorMessage);
                }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                    handleAsyncLineOfSightQueryError(eErrorCode, tabProperties.scouter, tabProperties.isScouter, tabProperties.target, tabProperties.isTarget, tabProperties.maxPitchAngle, tabProperties.minPitchAngle)
                }, 'SpatialQueriesForm/LineOfSight.handleGetLineOfSightClick => IMcSpatialQueries.GetLineOfSight');
            }
            else {
                queryParams.pAsyncQueryCallback = null;
            }
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            runMapCoreSafely(() => {
                lineOfSightPoints = mcCurrentSpatialQueries.GetLineOfSight(tabProperties.scouter, tabProperties.isScouter, tabProperties.target, tabProperties.isTarget,
                    pCrestAngle, pCrestDistance, tabProperties.maxPitchAngle, tabProperties.minPitchAngle, queryParams);
            }, 'SpatialQueriesForm/LineOfSight.handleGetLineOfSightClick => IMcSpatialQueries.GetLineOfSight', true, (error) => { errorMessage = String(error) })
        }, 'SpatialQueriesForm/LineOfSight.handleGetLineOfSightClick');
        if (!tabProperties.isLineAsync) {
            setLineOfSightQueryResults(tabProperties.scouter, tabProperties.isScouter, tabProperties.target, tabProperties.isTarget, tabProperties.maxPitchAngle, tabProperties.minPitchAngle,
                lineOfSightPoints, pCrestAngle?.Value, pCrestDistance?.Value, tabProperties.isLineAsync, false, errorMessage);
        }
    }
    //#endregion
    //#region DOM Functions
    const getColumnTemplate = (rowData: any, column: any, i: number) => {
        switch (column.field) {
            case 'visible':
                return <Checkbox style={{ minWidth: `${globalSizeFactor * 4}vh` }} checked={rowData.bVisible} />
            case 'no':
                return <span style={{ minWidth: `${globalSizeFactor * 1}vh` }}>{rowData[column.field]}</span>
            default:
                return <InputNumber className="form__large-input" value={rowData[column.field]} />
        }
    }
    const getGeneralParamsFieldset = () => {
        return <Fieldset legend='General Params' className="form__column-fieldset">
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '50%' }} className="form__flex-and-row-between form__items-center">
                    <span>Scouter:</span>
                    <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.scouter} saveTheValue={(...args) => { save3DVector(...args, 'scouter') }} lastPoint={true} />
                </div>
                <div style={{ width: '40%' }} className="form__flex-and-row form__items-center">
                    <Checkbox id="isScouter" name="isScouter" checked={tabProperties.isScouter} onChange={saveData} />
                    <label htmlFor="isScouter">Is Scouter Height Absolute</label>
                </div>
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '50%' }} className="form__flex-and-row-between form__items-center">
                    <span>Target:</span>
                    <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.target} saveTheValue={(...args) => { save3DVector(...args, 'target') }} lastPoint={true} />
                </div>
                <div style={{ width: '40%' }} className="form__flex-and-row form__items-center">
                    <Checkbox id="isTarget" name="isTarget" checked={tabProperties.isTarget} onChange={saveData} />
                    <label htmlFor="isTarget">Is Target Height Absolute</label>
                </div>
            </div>
            <div style={{ width: '46.5%' }} className="form__flex-and-row-between form__items-center">
                <label>Max Pitch Angle: </label>
                <InputNumber name='maxPitchAngle' value={tabProperties.maxPitchAngle} onValueChange={saveData} />
            </div>
            <div style={{ width: '46.5%' }} className="form__flex-and-row-between form__items-center">
                <label>Min Pitch Angle: </label>
                <InputNumber name='minPitchAngle' value={tabProperties.minPitchAngle} onValueChange={saveData} />
            </div>
        </Fieldset>
    }
    const getPointVisibilityArea = () => {
        return <div style={{ width: '49%' }} className="form__column-container">
            <span style={{ textDecoration: 'underline', padding: `${globalSizeFactor * 0.7}vh` }}>Get Point Visibility: </span>
            <div className="form__flex-and-row-between">
                <Fieldset style={{ width: '50%' }} legend='Calculation Options' className="form__column-fieldset">
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox id="isMinimalScouter" name="isMinimalScouter" checked={tabProperties.isMinimalScouter} onChange={saveData} />
                        <label htmlFor="isMinimalScouter">Minimal Scouter Height</label>
                    </div>
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox id="isMinimalTarget" name="isMinimalTarget" checked={tabProperties.isMinimalTarget} onChange={saveData} />
                        <label htmlFor="isMinimalTarget">Minimal Target Height</label>
                    </div>
                </Fieldset>
                <div style={{ width: '40%' }} className="form__column-container form__justify-space-around">
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox id="isPointVisibilityAsync" name="isPointVisibilityAsync" checked={tabProperties.isPointVisibilityAsync} onChange={saveData} />
                        <label htmlFor="isPointVisibilityAsync">Async</label>
                    </div>
                    <Button label='Get' onClick={handleGetPointVisibilityClick} />
                </div>
            </div>
            <Fieldset style={{ height: `${globalSizeFactor * 20.3}vh` }} legend='Results' className="form__column-fieldset">
                <div className="form__flex-and-row form__items-center form__disabled">
                    <Checkbox id="isTargetVisible" name="isTargetVisible" checked={tabProperties.isTargetVisible} onChange={saveData} />
                    <label htmlFor="isTargetVisible">Is Target Visible</label>
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <label className={`${tabProperties.isMinimalScouter ? '' : 'form__disabled'}`}>Minimal Scouter Height For Visibility: </label>
                    <InputNumber className="form__disabled" name='minimalScouter' value={tabProperties.minimalScouter} onValueChange={saveData} />
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <label className={`${tabProperties.isMinimalTarget ? '' : 'form__disabled'}`}>Minimal Target Height For Visibility: </label>
                    <InputNumber className="form__disabled" name='minimalTarget' value={tabProperties.minimalTarget} onValueChange={saveData} />
                </div>
            </Fieldset>
        </div>
    }
    const getLineOfSightArea = () => {
        return <div style={{ width: '49%' }} className="form__column-container">
            <span style={{ textDecoration: 'underline', padding: `${globalSizeFactor * 0.7}vh` }}>Get Line Of Sight: </span>
            <div style={{ width: '40%', height: `${globalSizeFactor * 8}vh` }} className="form__column-container form__justify-space-around">
                <div className="form__flex-and-row form__items-center">
                    <Checkbox id="isLineAsync" name="isLineAsync" checked={tabProperties.isLineAsync} onChange={saveData} />
                    <label htmlFor="isLineAsync">Async</label>
                </div>
                <Button label='Get' onClick={handleGetLineOfSightClick} />
            </div>
            <Fieldset legend='Results' className="form__column-fieldset">
                <div className="form__flex-and-row-between form__items-center form__disabled">
                    <label>Crest Clearence Angle: </label>
                    <InputNumber name='crestClearenceAngle' value={tabProperties.crestClearenceAngle} onValueChange={saveData} />
                </div>
                <div className="form__flex-and-row-between form__items-center form__disabled">
                    <label>Crest Clearence Distance: </label>
                    <InputNumber name='crestClearenceDistance' value={tabProperties.crestClearenceDistance} onValueChange={saveData} />
                </div>
                <div className="form__column-container">
                    <span className='form__disabled' style={{ textDecoration: 'underline' }}>Line Of Sight Points: </span>
                    <DataTable style={{ opacity: '0.6', minHeight: `${globalSizeFactor * 9}vh` }} showGridlines size='small' scrollable scrollHeight={`${globalSizeFactor * 9}vh`} value={getFlatLineOfSightPoints()}>
                        {getColumns().map(({ field, header }, i) => {
                            return <Column style={{ minWidth: `${globalSizeFactor * (field == 'visible' ? 4 : field == 'no' ? 1 : 11)}vh` }} key={field} field={field} header={header} body={(...args) => getColumnTemplate(...args, i)} />;
                        })}
                    </DataTable>
                </div>
            </Fieldset>
        </div>
    }
    //#endregion
    return (
        <div className="form__column-container">
            {getGeneralParamsFieldset()}
            <br />
            <div className="form__flex-and-row-between">
                {getPointVisibilityArea()}
                <Divider layout="vertical" />
                {getLineOfSightArea()}
            </div>
        </div>
    )
}
