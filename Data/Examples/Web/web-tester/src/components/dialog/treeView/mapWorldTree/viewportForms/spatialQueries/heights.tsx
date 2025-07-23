import { useEffect } from "react";
import { Fieldset } from "primereact/fieldset";
import { useDispatch, useSelector } from "react-redux";
import { Checkbox } from "primereact/checkbox";
import { InputNumber } from "primereact/inputnumber";
import { Button } from "primereact/button";
import _ from 'lodash';
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";

import { MapCoreData, ObjectWorldService, ViewportData } from 'mapcore-lib';
import { ParamsProperties } from "./params";
import { SpatialQueryName, QueryResult } from "./spatialQueriesFooter";
import Vector3DGrid from "../../../objectWorldTree/shared/vector3DGrid";
import Vector3DFromMap from "../../../objectWorldTree/shared/Vector3DFromMap";
import { Properties } from "../../../../dialog";
import DrawObjectCtrl, { ObjectTypeEnum } from "../../../../shared/drawObjectCtrl";
import { TabInfo } from "../../../../shared/tabCtrls/tabModels";
import DrawLineCtrl from "../../../../shared/drawLineCtrl";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { addQueryResultsTableRow, setSpatialQueriesResultsObjects } from "../../../../../../redux/MapWorldTree/mapWorldTreeActions";
import { AppState } from "../../../../../../redux/combineReducer";
import spatialQueriesService from "../../../../../../services/spatialQueries.service";

export class HeightsProperties implements Properties {
    mcCurrentSpatialQueries: MapCore.IMcSpatialQueries;
    activeOverlay: MapCore.IMcOverlay;
    currentViewportData: ViewportData;
    relativeHeightPoint: MapCore.SMcVector3D;
    requestedNormal: MapCore.SMcVector3D;
    isTerrainHeightAsync: boolean;
    isHeightFound: boolean;
    resultHeight: number;
    //Along Line
    maxSlope: number;
    minSlope: number;
    heightSlope: number;
    isAlongLineAsync: boolean;
    linePoints: MapCore.SMcVector3D[];
    pointsWithHeightsAlongLine: MapCore.SMcVector3D[];
    //Matrix
    lowerLeftPoint: MapCore.SMcVector3D;
    horizontalResolution: number;
    verticalResolution: number;
    numHorizontalPoints: number;
    numVerticalPoints: number;
    isMatrixAsync: boolean;
    heightMatrix: any[];
    //Extreme
    polygonPoints: MapCore.SMcVector2D[];
    isExtremeAsync: boolean;
    highestPoint: MapCore.SMcVector3D;
    lowestPoint: MapCore.SMcVector3D;
    isPointsFound: boolean;

    static getDefault(p: any): HeightsProperties {
        let { mcCurrentSpatialQueries } = p;

        return {
            mcCurrentSpatialQueries: mcCurrentSpatialQueries,
            activeOverlay: null,
            currentViewportData: null,
            relativeHeightPoint: MapCore.v3Zero,
            requestedNormal: MapCore.v3Zero,
            isTerrainHeightAsync: true,
            isHeightFound: false,
            resultHeight: 0,
            //Along Line
            maxSlope: 0,
            minSlope: 0,
            heightSlope: 0,
            isAlongLineAsync: true,
            linePoints: [],
            pointsWithHeightsAlongLine: [],
            //Matrix
            lowerLeftPoint: MapCore.v3Zero,
            horizontalResolution: 0,
            verticalResolution: 0,
            numHorizontalPoints: 0,
            numVerticalPoints: 0,
            isMatrixAsync: true,
            heightMatrix: [],
            //Extreme
            polygonPoints: [],
            isExtremeAsync: true,
            highestPoint: MapCore.v3Zero,
            lowestPoint: MapCore.v3Zero,
            isPointsFound: false,
        }
    }
};

export default function Heights(props: { tabInfo: TabInfo }) {
    let { saveData, setApplyCallBack, setPropertiesCallback, tabProperties, getTabPropertiesByTabPropertiesClass } = props.tabInfo;
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const spatialQueriesResultsObjects = useSelector((state: AppState) => state.mapWorldTreeReducer.spatialQueriesResultsObjects)
    const activeCard: number = useSelector((state: AppState) => state.mapWindowReducer.activeCard);

    useEffect(() => {
        runCodeSafely(() => {
            let activeOverlay = getActiveOverlay();
            setPropertiesCallback('activeOverlay', activeOverlay);
            let currentViewportData: ViewportData = getViewportData();
            setPropertiesCallback('currentViewportData', currentViewportData);
        }, 'SpatialQueriesForm/RasterAndTraversability.useEffect');
    }, [activeCard])
    //#region Help Functions
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
        }, 'SpatialQueriesForm/Heights.getActiveOverlay');
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
        }, 'SpatialQueriesForm/Heights.getViewportData');
        return viewportData;
    }
    const getExistObject = () => {
        let existObject = null;
        runCodeSafely(() => {
            if (spatialQueriesResultsObjects.queryName == SpatialQueryName.GetExtremeHeightPointsInPolygon) {
                let obj = spatialQueriesResultsObjects.objects[0] as MapCore.IMcObject;
                let isViewportStillExist = MapCoreData.viewportsData.find((vp: ViewportData) => vp.viewport.GetOverlayManager() == obj.GetOverlayManager())
                if (isViewportStillExist) {
                    existObject = spatialQueriesResultsObjects.objects[0];
                }
                else {
                    spatialQueriesService.removeExistObjects();
                }
            }
        }, 'SpatialQueriesForm/Heights.getExistObject');
        return existObject;
    }
    const getColumns = () => {
        let columns = [{ field: `No`, header: '.No' }];
        runCodeSafely(() => {
            for (let index = 0; index < tabProperties.numHorizontalPoints; index++) {
                columns = [...columns, { field: `${index}`, header: `${index + 1}` }]
            }
        }, 'SpatialQueriesForm/Heights.getColumns')
        return columns;
    }
    const float64ArrayToObjectArray = (float64Array: Float64Array, objectLen: number) => {
        const result = [];
        for (let i = 0; i < float64Array.length; i += objectLen) {
            const obj = { No: (i / objectLen) + 1 };
            for (let j = 0; j < objectLen; j++) {
                obj[j] = float64Array[i + j];
            }
            result.push(obj);
        }
        return result;
    }
    //#endregion
    //#region Save Functions
    const save3DVector = (...args) => {
        runCodeSafely(() => {
            const [point, flagNull, sectionPointType] = args;
            setPropertiesCallback(sectionPointType, point);
        }, 'SpatialQueriesForm/Heights.save3DVector');
    }
    const savePointsTable = (...args) => {
        runCodeSafely(() => {
            const [locationPointsList, valid, selectedPoint, pointsType] = args;
            if (!_.isEqual(tabProperties[pointsType], locationPointsList)) {
                setPropertiesCallback(pointsType, locationPointsList);
            }
        }, 'SpatialQueriesForm/Heights.savePointsTable');
    }
    const getLineObject = (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) => {
        runCodeSafely(() => {
            if (nExitCode == 1) {
                let points: MapCore.SMcVector3D[] = pObject.GetLocationPoints(0);
                setPropertiesCallback('linePoints', points);
                spatialQueriesService.removeExistObjects();
                dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetTerrainHeightsAlongLine, objects: [pObject], removeObjectsCB: (objects: MapCore.IMcObject[]) => { objects[0].Remove() } }))
            }
        }, 'SpatialQueriesForm/Heights.getLineObject');
    }
    const getPolygonObject = (pObject: MapCore.IMcObject) => {
        runCodeSafely(() => {
            let points: MapCore.SMcVector3D[] = pObject.GetLocationPoints(0);
            setPropertiesCallback('polygonPoints', points);
            if (spatialQueriesResultsObjects.queryName != SpatialQueryName.GetExtremeHeightPointsInPolygon) {
                spatialQueriesService.removeExistObjects();
                dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetExtremeHeightPointsInPolygon, objects: [pObject], removeObjectsCB: (objects: MapCore.IMcObject[]) => { objects[0].Remove() } }))
            }
        }, 'SpatialQueriesForm/Heights.getPolygonObject');
    }
    const getPolygonPoints = (polygonPoints: MapCore.SMcVector3D[]) => {
        runCodeSafely(() => {
            setPropertiesCallback('polygonPoints', polygonPoints);
        }, 'SpatialQueriesForm/Heights.getPolygonPoints');
    }
    //#endregion
    //#region Set Results
    const setTerrainHeightQueryResults = (relativeHeightPoint: MapCore.SMcVector3D, bHeightFound: boolean, dHeight: number, pNormal: MapCore.SMcVector3D,
        isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            setPropertiesCallback('relativeHeightPoint', relativeHeightPoint)
            setPropertiesCallback('isHeightFound', bHeightFound)
            setPropertiesCallback('resultHeight', dHeight)
            setPropertiesCallback('requestedNormal', pNormal)
            setPropertiesCallback('isTerrainHeightAsync', isAsync)
            let pointToDraw = new MapCore.SMcVector3D(relativeHeightPoint);
            pointToDraw.z = bHeightFound ? dHeight : pointToDraw.z;

            spatialQueriesService.manageQueryResultObjects(tabProperties.mcCurrentSpatialQueries, SpatialQueryName.GetTerrainHeight, pointToDraw)
        }, 'SpatialQueriesForm/Heights.setTerrainHeightQueryResults')
        if (!isFromTable) {
            let args = [relativeHeightPoint, bHeightFound, dHeight, pNormal, isAsync, true, errorMessage];
            let queryResult = new QueryResult(SpatialQueryName.GetTerrainHeight,
                setTerrainHeightQueryResults, args, tabProperties.isTerrainHeightAsync, errorMessage);
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    const setTerrainHeightAlongLineQueryResults = (linePoints: MapCore.SMcVector3D[], aPointsWithHeights: MapCore.SMcVector3D[], afSlopes: Float32Array,
        pSlopesData: MapCore.IMcSpatialQueries.SSlopesData, isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            setPropertiesCallback('linePoints', linePoints);
            setPropertiesCallback('maxSlope', pSlopesData ? pSlopesData.fMaxSlope : 0)
            setPropertiesCallback('minSlope', pSlopesData ? pSlopesData.fMinSlope : 0)
            setPropertiesCallback('heightSlope', pSlopesData ? pSlopesData.fHeightDelta : 0)
            setPropertiesCallback('pointsWithHeightsAlongLine', aPointsWithHeights);
            setPropertiesCallback('isAlongLineAsync', isAsync);

            spatialQueriesService.manageQueryResultObjects(tabProperties.mcCurrentSpatialQueries, SpatialQueryName.GetTerrainHeightsAlongLine, aPointsWithHeights, afSlopes)
        }, 'SpatialQueriesForm/Heights.setTerrainHeightAlongLineQueryResults')

        if (!isFromTable) {
            let args = [linePoints, aPointsWithHeights, afSlopes, pSlopesData, isAsync, true, errorMessage]
            let queryResult = new QueryResult(SpatialQueryName.GetTerrainHeightsAlongLine,
                setTerrainHeightAlongLineQueryResults, args, tabProperties.isAlongLineAsync, errorMessage)
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    const setTerrainHeightMatrixQueryResults = (lowerLeftPoint: MapCore.SMcVector3D, horizontalResolution: number,
        verticalResolution: number, numHorizontalPoints: number, numVerticalPoints: number, adHeightMatrix: Float64Array,
        isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            let heightMatrixArr = float64ArrayToObjectArray(adHeightMatrix, numHorizontalPoints);
            setPropertiesCallback('heightMatrix', heightMatrixArr);
            setPropertiesCallback('lowerLeftPoint', lowerLeftPoint);
            setPropertiesCallback('horizontalResolution', horizontalResolution);
            setPropertiesCallback('verticalResolution', verticalResolution);
            setPropertiesCallback('numHorizontalPoints', numHorizontalPoints);
            setPropertiesCallback('numVerticalPoints', numVerticalPoints);
            setPropertiesCallback('isMatrixAsync', isAsync);

            spatialQueriesService.manageQueryResultObjects(tabProperties.mcCurrentSpatialQueries,
                SpatialQueryName.GetTerrainHeightMatrix, lowerLeftPoint, horizontalResolution, verticalResolution,
                numHorizontalPoints, numVerticalPoints, adHeightMatrix);
        }, 'SpatialQueriesForm/Heights.setTerrainHeightMatrixQueryResults')

        if (!isFromTable) {
            let args = [lowerLeftPoint, horizontalResolution, verticalResolution, numHorizontalPoints, numVerticalPoints, adHeightMatrix, isAsync, true, errorMessage]
            let queryResult = new QueryResult(SpatialQueryName.GetTerrainHeightMatrix,
                setTerrainHeightMatrixQueryResults, args, tabProperties.isMatrixAsync, errorMessage)
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    const setExtremeQueryResults = (polygonPoints: MapCore.SMcVector3D[], isPointsFound: boolean,
        highestPoint: MapCore.SMcVector3D, lowestPoint: MapCore.SMcVector3D,
        isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            let highestPointResult = isPointsFound ? highestPoint : null;
            let lowestPointResult = isPointsFound ? lowestPoint : null;
            setPropertiesCallback('polygonPoints', polygonPoints);
            setPropertiesCallback('isPointsFound', isPointsFound);
            setPropertiesCallback('highestPoint', highestPointResult);
            setPropertiesCallback('lowestPoint', lowestPointResult);
            setPropertiesCallback('isExtremeAsync', isAsync);

            spatialQueriesService.manageQueryResultObjects(tabProperties.mcCurrentSpatialQueries, SpatialQueryName.GetExtremeHeightPointsInPolygon,
                polygonPoints, isPointsFound, highestPointResult, lowestPointResult);
        }, 'SpatialQueriesForm/Heights.setExtremeQueryResults')

        if (!isFromTable) {
            let args = [polygonPoints, isPointsFound, highestPoint, lowestPoint, isAsync, true, errorMessage]
            let queryResult = new QueryResult(SpatialQueryName.GetTerrainHeightMatrix,
                setExtremeQueryResults, args, tabProperties.isExtremeAsync, errorMessage)
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    //#endregion
    //#region Handle Query Error
    const handleAsyncTerrainHeightQueryError = (eErrorCode: MapCore.IMcErrors.ECode, relativeHeightPoint: MapCore.SMcVector3D) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/Heights.handleAsyncTerrainHeightQueryError => IMcErrors.ErrorCodeToString', true)
            setTerrainHeightQueryResults(relativeHeightPoint, false, 0, MapCore.v3Zero, true, false, errorMessage);
        }, 'SpatialQueriesForm/Heights.handleAsyncTerrainHeightQueryError')
    }
    const handleAsyncAlongLineQueryError = (eErrorCode: MapCore.IMcErrors.ECode, localLinePoints: MapCore.SMcVector3D[]) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/Heights.handleAsyncAlongLineQueryError => IMcErrors.ErrorCodeToString', true)
            setTerrainHeightAlongLineQueryResults(localLinePoints, [], null, null, true, false, errorMessage);
        }, 'SpatialQueriesForm/Heights.handleAsyncAlongLineQueryError')
    }
    const handleAsyncMatrixQueryError = (eErrorCode: MapCore.IMcErrors.ECode, lowerLeftPoint: MapCore.SMcVector3D, horizontalResolution: number, verticalResolution: number, numHorizontalPoints: number, numVerticalPoints: number) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/Heights.handleAsyncMatrixQueryError => IMcErrors.ErrorCodeToString', true)
            setTerrainHeightMatrixQueryResults(lowerLeftPoint, horizontalResolution, verticalResolution, numHorizontalPoints,
                numVerticalPoints, new Float64Array, true, false, errorMessage);
        }, 'SpatialQueriesForm/Heights.handleAsyncMatrixQueryError')
    }
    const handleAsynExtremeQueryError = (eErrorCode: MapCore.IMcErrors.ECode, polygonPoints: MapCore.SMcVector3D[]) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/Heights.handleAsynExtremeQueryError => IMcErrors.ErrorCodeToString', true)
            setExtremeQueryResults(polygonPoints, false, MapCore.v3Zero, MapCore.v3Zero, true, false, errorMessage);
        }, 'SpatialQueriesForm/Heights.handleAsynExtremeQueryError')
    }
    //#endregion
    //#region Handle Functoins
    const handleGetTerrainHeightClick = () => {
        let isHeightFound = false;
        let pHeight: { Value?: number } = {};
        let pNormal: { Value?: MapCore.SMcVector3D } = {};
        let errorMessage = '';
        runCodeSafely(() => {
            let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
            if (tabProperties.isTerrainHeightAsync) {
                queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((bHeightFound: boolean, dHeight: number, pNormal: MapCore.SMcVector3D) => {
                    setTerrainHeightQueryResults(tabProperties.relativeHeightPoint, bHeightFound, dHeight, pNormal, tabProperties.isTerrainHeightAsync, false, errorMessage);
                }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                    handleAsyncTerrainHeightQueryError(eErrorCode, tabProperties.relativeHeightPoint)
                }, 'SpatialQueriesForm/Heights.handleGetTerrainHeightClick => IMcSpatialQueries.GetTerrainHeight');
            }
            else {
                queryParams.pAsyncQueryCallback = null;
            }
            runMapCoreSafely(() => {
                isHeightFound = tabProperties.mcCurrentSpatialQueries.GetTerrainHeight(tabProperties.relativeHeightPoint, pHeight, pNormal, queryParams)
            }, 'SpatialQueriesForm/Heights.handleGetTerrainHeightClick => IMcSpatialQueries.GetTerrainHeight', true, (error) => { errorMessage = String(error) })
        }, 'SpatialQueriesForm/Heights.handleGetTerrainHeightClick');
        if (!tabProperties.isTerrainHeightAsync) {
            setTerrainHeightQueryResults(tabProperties.relativeHeightPoint, isHeightFound, pHeight.Value, pNormal.Value, tabProperties.isTerrainHeightAsync, false, errorMessage);
        }
    }
    const handleGetHeightAlongLineClick = () => {
        let localLinePoints = tabProperties.linePoints;//this copy is important!
        let pSlopesData: { Value?: MapCore.IMcSpatialQueries.SSlopesData } = {};
        let pafSlopes: { Value?: Float32Array } = {};
        let resultPoints: MapCore.SMcVector3D[];
        let errorMessage = '';
        runCodeSafely(() => {
            if (tabProperties.linePoints.length != 0) {
                let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
                if (tabProperties.isAlongLineAsync) {
                    queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((aPointsWithHeights: MapCore.SMcVector3D[], afSlopes: Float32Array, pSlopesData: MapCore.IMcSpatialQueries.SSlopesData) => {
                        setTerrainHeightAlongLineQueryResults(localLinePoints, aPointsWithHeights, afSlopes, pSlopesData, tabProperties.isAlongLineAsync, false, '');
                    }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                        handleAsyncAlongLineQueryError(eErrorCode, localLinePoints)
                    }, 'SpatialQueriesForm/Heights.handleGetHeightAlongLineClick => IMcSpatialQueries.GetTerrainHeightsAlongLine');
                }
                else {
                    queryParams.pAsyncQueryCallback = null;
                }
                let convertedPoints = localLinePoints.map(point => spatialQueriesService.convertPointFromVPtoOM(point, tabProperties.mcCurrentSpatialQueries));
                runMapCoreSafely(() => {
                    resultPoints = tabProperties.mcCurrentSpatialQueries.GetTerrainHeightsAlongLine(convertedPoints, pafSlopes, pSlopesData, queryParams)
                }, 'SpatialQueriesForm/Heights.handleGetHeightAlongLineClick => IMcSpatialQueries.GetTerrainHeightsAlongLine', true, (error) => {
                    errorMessage = String(error)
                })
            }
        }, 'SpatialQueriesForm/Heights.handleGetHeightAlongLineClick');
        if (!tabProperties.isAlongLineAsync && tabProperties.linePoints.length != 0) {
            setTerrainHeightAlongLineQueryResults(localLinePoints, resultPoints, pafSlopes.Value, pSlopesData.Value, tabProperties.isAlongLineAsync, false, errorMessage);
        }
    }
    const handleGetTerrainHeightMatrixClick = () => {
        let errorMessage = '';
        let heightMatrix: Float64Array = new Float64Array;
        runCodeSafely(() => {
            let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
            if (tabProperties.isMatrixAsync) {
                queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((adHeightMatrix: Float64Array) => {
                    setTerrainHeightMatrixQueryResults(tabProperties.lowerLeftPoint,
                        tabProperties.horizontalResolution, tabProperties.verticalResolution,
                        tabProperties.numHorizontalPoints, tabProperties.numVerticalPoints, adHeightMatrix, true, false, '')
                }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                    handleAsyncMatrixQueryError(eErrorCode, tabProperties.lowerLeftPoint,
                        tabProperties.horizontalResolution, tabProperties.verticalResolution,
                        tabProperties.numHorizontalPoints, tabProperties.numVerticalPoints);
                }, 'SpatialQueriesForm/Heights.handleGetTerrainHeightMatrixClick => IMcSpatialQueries.GetTerrainHeightMatrix');
            }
            else {
                queryParams.pAsyncQueryCallback = null;
            }
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            runMapCoreSafely(() => {
                heightMatrix = mcCurrentSpatialQueries.GetTerrainHeightMatrix(tabProperties.lowerLeftPoint,
                    tabProperties.horizontalResolution, tabProperties.verticalResolution,
                    tabProperties.numHorizontalPoints, tabProperties.numVerticalPoints, queryParams);
            }, 'SpatialQueriesForm/Heights.handleGetTerrainHeightMatrixClick => IMcSpatialQueries.GetTerrainHeightMatrix', true, (error) => { errorMessage = String(error) })
        }, 'SpatialQueriesForm/Heights.handleGetTerrainHeightMatrixClick');
        if (!tabProperties.isMatrixAsync) {
            setTerrainHeightMatrixQueryResults(tabProperties.lowerLeftPoint, tabProperties.horizontalResolution,
                tabProperties.verticalResolution, tabProperties.numHorizontalPoints, tabProperties.numVerticalPoints,
                heightMatrix, tabProperties.isMatrixAsync, false, errorMessage);
        }
    }
    const handleGetExtremePointsClick = () => {
        let errorMessage = '';
        let isPointsFoundLocal: boolean;
        let pHighestPoint: { Value?: MapCore.SMcVector3D } = {};
        let pLowestPoint: { Value?: MapCore.SMcVector3D } = {};
        runCodeSafely(() => {
            let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
            if (tabProperties.isExtremeAsync) {
                queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass(
                    (bPointsFound: boolean, pHighestPoint: MapCore.SMcVector3D, pLowestPoint: MapCore.SMcVector3D) => {
                        setExtremeQueryResults(tabProperties.polygonPoints, bPointsFound, pHighestPoint, pLowestPoint, true, false, '')
                    }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                        handleAsynExtremeQueryError(eErrorCode, tabProperties.polygonPoints);
                    }, 'SpatialQueriesForm/Heights.handleGetExtremePointsClick => IMcSpatialQueries.GetExtremeHeightPointsInPolygon');
            }
            else {
                queryParams.pAsyncQueryCallback = null;
            }
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            runMapCoreSafely(() => {
                isPointsFoundLocal = mcCurrentSpatialQueries.GetExtremeHeightPointsInPolygon(tabProperties.polygonPoints,
                    pHighestPoint, pLowestPoint, queryParams);
            }, 'SpatialQueriesForm/Heights.handleGetExtremePointsClick => IMcSpatialQueries.GetExtremeHeightPointsInPolygon', true, (error) => { errorMessage = String(error) })
        }, 'SpatialQueriesForm/Heights.handleGetExtremePointsClick');
        if (!tabProperties.isExtremeAsync) {
            setExtremeQueryResults(tabProperties.polygonPoints, isPointsFoundLocal, pHighestPoint.Value,
                pLowestPoint.Value, tabProperties.isExtremeAsync, false, errorMessage);
        }
    }
    //#endregion
    //#region DOM Functions
    const getTerrainHeightFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend='Terrain Height'>
            <div style={{ height: `${globalSizeFactor * 10}vh` }} className="form__flex-and-row-between">
                <span style={{ width: `${globalSizeFactor * 9}vh` }}>Relative Height Point:</span>
                <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.relativeHeightPoint} saveTheValue={(...args) => save3DVector(...args, 'relativeHeightPoint')} lastPoint={true} />
            </div>
            <div className="form__flex-and-row-between">
                <div style={{ width: '60%' }} className="form__column-container">
                    <Button label='Get Height' onClick={handleGetTerrainHeightClick} />
                </div>
                <div style={{ width: '28.5%' }} className="form__flex-and-row form__items-center">
                    <Checkbox id="isTerrainHeightAsync" name="isTerrainHeightAsync" checked={tabProperties.isTerrainHeightAsync} onChange={saveData} />
                    <label htmlFor="isTerrainHeightAsync" >Async</label>
                </div>
            </div>
            <Fieldset style={{ height: `${globalSizeFactor * 12.2}vh` }} className="form__column-fieldset" legend='Results'>
                <div className="form__flex-and-row-between form__items-center form__disabled">
                    <span style={{ width: `${globalSizeFactor * 9}vh` }}>Requested Normal:</span>
                    <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.requestedNormal} saveTheValue={(...args) => save3DVector(...args, 'requestedNormal')} lastPoint={true} />
                </div>
                <div className="form__flex-and-row-between form__items-center form__disabled">
                    <div style={{ width: '60%' }} className="form__flex-and-row-between form__items-center">
                        <span>Result Height:</span>
                        <InputNumber value={tabProperties.resultHeight} />
                    </div>
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox id="isHeightFound" name="isHeightFound" checked={tabProperties.isHeightFound} onChange={saveData} />
                        <label htmlFor="isHeightFound">Height Found</label>
                    </div>
                </div>
            </Fieldset>
        </Fieldset>
    }
    const getTerrainHeightAlongLineFieldset = () => {
        return <Fieldset className="form__column-fieldset-no-gap" legend='Terrain Height Along Line'>
            <div style={{ paddingBottom: '0.5%' }} className="form__column-container form__items-center">
                <div className="form__column-container">
                    <span style={{ textDecoration: 'underline', padding: `${globalSizeFactor * 0.7}vh` }}>Line Points: </span>
                    <Vector3DGrid disabledPointFromMap={tabProperties.activeOverlay ? false : true} ctrlHeight={8} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initLocationPointsList={tabProperties.linePoints} sendPointList={(...args) => savePointsTable(...args, 'linePoints')} />
                </div>
                <DrawLineCtrl disabled={tabProperties.activeOverlay ? false : true} handleDrawLineButtonClick={spatialQueriesService.removeExistObjects} activeViewport={tabProperties.currentViewportData} initItemResultsCB={getLineObject} />
            </div>
            <div className="form__column-container form__justify-end">
                <Button label="Get Height Along Line" onClick={handleGetHeightAlongLineClick} />
                <div className="form__flex-and-row form__items-center form__justify-end">
                    <Checkbox id="isAlongLineAsync" name="isAlongLineAsync" checked={tabProperties.isAlongLineAsync} onChange={saveData} />
                    <label htmlFor="isAlongLineAsync">Async</label>
                </div>
            </div>
            <Fieldset legend='Results' className="form__column-fieldset form__space-between">
                <div className="form__flex-and-row-between form__items-center form__disabled">
                    <span>Max Slope:</span>
                    <InputNumber value={tabProperties.maxSlope} />
                </div>
                <div className="form__flex-and-row-between form__items-center form__disabled">
                    <span>Min Slope:</span>
                    <InputNumber value={tabProperties.minSlope} />
                </div>
                <div className="form__flex-and-row-between form__items-center form__disabled">
                    <span>Height Slope:</span>
                    <InputNumber value={tabProperties.heightSlope} />
                </div>
                <div className="form__column-container">
                    <span className='form__disabled' style={{ textDecoration: 'underline' }}>Points With Heights: </span>
                    <Vector3DGrid disabledPointFromMap={tabProperties.activeOverlay ? false : true} disabled={true} ctrlHeight={7} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initLocationPointsList={tabProperties.pointsWithHeightsAlongLine} sendPointList={(...args) => { }} />
                </div>
            </Fieldset>
        </Fieldset>
    }
    const getTerrainHeightMatrixFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend='Terrain Height Matrix'>
            <div className="form__flex-and-row-between  form__items-center">
                <span style={{ width: `${globalSizeFactor * 9}vh` }}>Lower Left Point:</span>
                <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.lowerLeftPoint} saveTheValue={(...args) => save3DVector(...args, 'lowerLeftPoint')} lastPoint={true} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '48%' }} className="form__flex-and-row-between form__items-center">
                    <span>Horizontal Resolution:</span>
                    <InputNumber className="form__medium-width-input" value={tabProperties.horizontalResolution} name='horizontalResolution' onValueChange={saveData} />
                </div>
                <div style={{ width: '48%' }} className="form__flex-and-row-between form__items-center">
                    <span>Num Horizontal Points:</span>
                    <InputNumber className="form__medium-width-input" value={tabProperties.numHorizontalPoints} name='numHorizontalPoints' onValueChange={saveData} />
                </div>
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '48%' }} className="form__flex-and-row-between form__items-center">
                    <span>Vertical Resolution:</span>
                    <InputNumber className="form__medium-width-input" value={tabProperties.verticalResolution} name='verticalResolution' onValueChange={saveData} />
                </div>
                <div style={{ width: '48%' }} className="form__flex-and-row-between form__items-center">
                    <span>Num Vertical Points:</span>
                    <InputNumber className="form__medium-width-input" value={tabProperties.numVerticalPoints} name='numVerticalPoints' onValueChange={saveData} />
                </div>
            </div>
            <div className="form__flex-and-row-between">
                <div style={{ width: '60%' }} className="form__column-container">
                    <Button label='Get Height' onClick={handleGetTerrainHeightMatrixClick} />
                </div>
                <div style={{ width: '28.5%' }} className="form__flex-and-row form__items-center">
                    <Checkbox id="isMatrixAsync" name="isMatrixAsync" checked={tabProperties.isMatrixAsync} onChange={saveData} />
                    <label htmlFor="isMatrixAsync" >Async</label>
                </div>
            </div>
            <Fieldset legend='Results'>
                <div style={{ width: `${globalSizeFactor * 42}vh` }}>
                    <DataTable scrollable scrollHeight={`${globalSizeFactor * 9}vh`} value={tabProperties.heightMatrix}>
                        {getColumns().map(({ field, header }) => {
                            return <Column key={field} field={field} header={header} />;
                        })}
                    </DataTable>
                </div>
            </Fieldset>
        </Fieldset>
    }
    const getExtremeHeightPointsFieldset = () => {
        return <Fieldset className="form__column-fieldset-no-gap" legend='Extreme Height Points in Polygon'>
            <DrawObjectCtrl disabled={tabProperties.activeOverlay ? false : true}
                activeViewport={tabProperties.currentViewportData}
                getObject={getPolygonObject}
                objectType={ObjectTypeEnum.polygon}
                //Optional
                pointsTableVisible={true}
                getObjectPoints={getPolygonPoints}
                existObjectPoints={tabProperties.polygonPoints}
                existObject={getExistObject()}
                handleDrawObjectButtonClick={spatialQueriesResultsObjects.queryName == SpatialQueryName.GetExtremeHeightPointsInPolygon ? null : spatialQueriesService.removeExistObjects}
                ctrlHeight={10}
                className="form__column-container form__justify-center form__items-center"
            />
            <div className="form__column-container form__justify-end">
                <Button label="Get Extreme Points in Polygon" onClick={handleGetExtremePointsClick} />
                <div className="form__flex-and-row form__items-center form__justify-end">
                    <Checkbox id="isExtremeAsync" name="isExtremeAsync" checked={tabProperties.isExtremeAsync} onChange={saveData} />
                    <label htmlFor="isExtremeAsync">Async</label>
                </div>
            </div>
            <Fieldset legend='Results' className="form__column-fieldset form__disabled-fieldset">
                <div style={{ height: `${globalSizeFactor * 19}vh` }}>
                    <div className="form__flex-and-row-between form__items-center">
                        <span style={{ width: `${globalSizeFactor * 9}vh` }}>Highest Point:</span>
                        <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.highestPoint} saveTheValue={(...args) => save3DVector(...args, 'highestPoint')} lastPoint={true} />
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <span style={{ width: `${globalSizeFactor * 9}vh` }}>Lowest Point:</span>
                        <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.lowestPoint} saveTheValue={(...args) => save3DVector(...args, 'lowestPoint')} lastPoint={true} />
                    </div>
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox id="isPointsFound" name="isPointsFound" checked={tabProperties.isPointsFound} onChange={saveData} />
                        <label htmlFor="isPointsFound">Points Found</label>
                    </div>
                </div>
            </Fieldset>
        </Fieldset>
    }

    //#endregion
    return (
        <div className="form__flex-and-row-between">
            <div className="form__column-container">
                {getTerrainHeightFieldset()}
                {getTerrainHeightAlongLineFieldset()}
            </div>
            <div className="form__column-container">
                {getTerrainHeightMatrixFieldset()}
                {getExtremeHeightPointsFieldset()}
            </div>
        </div>
    )
}
