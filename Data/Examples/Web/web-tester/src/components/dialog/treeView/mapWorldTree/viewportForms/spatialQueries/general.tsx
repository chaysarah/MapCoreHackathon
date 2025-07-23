import { useEffect, useRef } from "react";
import { Fieldset } from "primereact/fieldset";
import { InputNumber } from "primereact/inputnumber";
import { Checkbox } from "primereact/checkbox";
import { Button } from "primereact/button";
import { useDispatch, useSelector } from "react-redux";
import { ListBox } from "primereact/listbox";
import { Dropdown } from "primereact/dropdown";
import { ConfirmDialog } from "primereact/confirmdialog";
import _ from 'lodash';

import { MapCoreData, ObjectWorldService, getEnumValueDetails, getEnumDetailsList, ViewportData } from 'mapcore-lib';
import './styles/spatialQueries.css'
import { ParamsProperties } from "./params";
import { QueryResult, SpatialQueryName } from "./spatialQueriesFooter";
import Vector3DGrid from "../../../objectWorldTree/shared/vector3DGrid";
import Vector3DFromMap from "../../../objectWorldTree/shared/Vector3DFromMap";
import DrawLineCtrl from "../../../../shared/drawLineCtrl";
import { TabInfo } from "../../../../shared/tabCtrls/tabModels";
import { Properties } from "../../../../dialog";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { TreeNodeModel } from "../../../../../../services/tree.service";
import { addQueryResultsTableRow, setSpatialQueriesResultsObjects } from "../../../../../../redux/MapWorldTree/mapWorldTreeActions";
import spatialQueriesService from "../../../../../../services/spatialQueries.service";
import InputMaxNumber from "../../../../../shared/inputMaxNumber";
import { AppState } from "../../../../../../redux/combineReducer";
import mapWorldTreeService from "../../../../../../services/mapWorldTreeService";

export class GeneralProperties implements Properties {
    isPrecisionHighest: boolean;
    mcCurrentSpatialQueries: MapCore.IMcSpatialQueries;
    activeOverlay: MapCore.IMcOverlay;
    currentViewportData: ViewportData;
    confirmDialogMessage: string;
    confirmDialogVisible: boolean;
    // LocationFromTwoDistancesAndAzimuthParams
    firstOrigin: MapCore.SMcVector3D;
    firstDistance: number;
    firstAzimuth: number;
    secondOrigin: MapCore.SMcVector3D;
    secondDistance: number;
    targetHeightAboveGround: number;
    isLocationAsync: boolean;
    // result
    locationResult: MapCore.SMcVector3D;
    distanceFromFirstLocationToResult: number;
    distanceFromSecondLocationToResult: number;
    differenceHeightFromResultToTerrain: number;
    isHeightFound: boolean;
    //Terrain Queries
    terrainsList: TreeNodeModel[];
    selectedTerrain: TreeNodeModel;
    layerKindArr: any[];
    selectedLayerKind: any;
    numTiles: number;
    //Track Smoother
    smoothDistance: number;
    pathPoints: MapCore.SMcVector3D[];
    addPoints: MapCore.SMcVector3D[];
    //Terrain Angles
    anglesPoint: MapCore.SMcVector3D;
    anglesLinePoints: MapCore.SMcVector3D[];
    azimuth: number;
    pitch: number;
    roll: number;
    isTerrainAnglesAsync: boolean;
    //Multi Thread Testing
    firstPoint: MapCore.SMcVector3D;
    numberOfTestingPoints: number;
    numberOfThreads: number;
    testDurationTime: number;
    isLogTest: boolean;
    isThrowException: boolean;

    static getDefault(p: any): GeneralProperties {
        let { mcCurrentSpatialQueries, mapWorldTree } = p;
        let layerKindArr = getEnumDetailsList(MapCore.IMcMapLayer.ELayerKind);
        layerKindArr = layerKindArr.filter(kind => kind.theEnum == MapCore.IMcMapLayer.ELayerKind.ELK_DTM || kind.theEnum == MapCore.IMcMapLayer.ELayerKind.ELK_RASTER || kind.theEnum == MapCore.IMcMapLayer.ELayerKind.ELK_STATIC_OBJECTS)
        let selectedLayerKind = getEnumValueDetails(MapCore.IMcMapLayer.ELayerKind.ELK_DTM, layerKindArr);

        let terrains: MapCore.IMcMapTerrain[] = mcCurrentSpatialQueries.GetTerrains();
        let terrainsNodesArr: TreeNodeModel[] = [];
        terrains.forEach((terrain, i) => {
            let terrainNode: any = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, terrain);
            terrainNode = terrainNode ? terrainNode : { nodeMcContent: terrain, label: `Map Terrain (${i + 1})` }
            terrainsNodesArr = [...terrainsNodesArr, terrainNode];
        });

        let defaults: GeneralProperties = {
            isPrecisionHighest: false,
            mcCurrentSpatialQueries: mcCurrentSpatialQueries,
            activeOverlay: null,
            currentViewportData: null,
            confirmDialogMessage: '',
            confirmDialogVisible: false,
            // LocationFromTwoDistancesAndAzimuthParams
            firstOrigin: MapCore.v3Zero,
            firstDistance: 0,
            firstAzimuth: 0,
            secondOrigin: MapCore.v3Zero,
            secondDistance: 0,
            targetHeightAboveGround: 0,
            isLocationAsync: true,
            // result
            locationResult: MapCore.v3Zero,
            distanceFromFirstLocationToResult: 0,
            distanceFromSecondLocationToResult: 0,
            differenceHeightFromResultToTerrain: 0,
            isHeightFound: false,
            //Terrain Queries
            terrainsList: terrainsNodesArr,
            selectedTerrain: terrainsNodesArr[0],
            layerKindArr: layerKindArr,
            selectedLayerKind: selectedLayerKind,
            numTiles: 0,
            //Track Smoother
            smoothDistance: 1,
            pathPoints: [],
            addPoints: [],
            //Terrain Angles
            anglesPoint: MapCore.v3Zero,
            anglesLinePoints: [],
            azimuth: 0,
            pitch: 0,
            roll: 0,
            isTerrainAnglesAsync: true,
            //Multi Thread Testing
            firstPoint: MapCore.v3Zero,
            numberOfTestingPoints: 100,
            numberOfThreads: 3,
            testDurationTime: 1,
            isLogTest: false,
            isThrowException: true,
        }

        return defaults;
    }
};

export default function General(props: { tabInfo: TabInfo }) {
    let { saveData, setApplyCallBack, setPropertiesCallback, tabProperties, getTabPropertiesByTabPropertiesClass } = props.tabInfo;
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const mapWorldTree = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree)
    const spatialQueriesResultsObjects = useSelector((state: AppState) => state.mapWorldTreeReducer.spatialQueriesResultsObjects)
    let spatialQueriesResultsObjectsRef = useRef(spatialQueriesResultsObjects);
    const activeCard: number = useSelector((state: AppState) => state.mapWindowReducer.activeCard);

    //#region UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
            setPropertiesCallback('isPrecisionHighest', queryParams.eTerrainPrecision == MapCore.IMcSpatialQueries.EQueryPrecision.EQP_HIGHEST);
        }, 'SpatialQueriesForm/General.useEffect');
    }, [])
    useEffect(() => {
        spatialQueriesResultsObjectsRef.current = spatialQueriesResultsObjects;
    }, [spatialQueriesResultsObjects])
    useEffect(() => {
        runCodeSafely(() => {
            let activeOverlay = getActiveOverlay();
            setPropertiesCallback('activeOverlay', activeOverlay);
            let currentViewportData: ViewportData = getViewportData();
            setPropertiesCallback('currentViewportData', currentViewportData);
        }, 'SpatialQueriesForm/RasterAndTraversability.useEffect');
    }, [activeCard])

    useEffect(() => {
        runCodeSafely(() => {
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            let numTiles = 0;
            runMapCoreSafely(() => {
                numTiles = tabProperties.selectedTerrain ? mcCurrentSpatialQueries.GetTerrainQueriesNumCacheTiles(tabProperties.selectedTerrain.nodeMcContent, tabProperties.selectedLayerKind.theEnum) : 0;
            }, 'SpatialQueriesForm/General.useEffect.selectedTerrain => IMcSpatialQueries.GetTerrainQueriesNumCacheTiles', true)
            setPropertiesCallback('numTiles', numTiles);
        }, 'SpatialQueriesForm/General.useEffect.selectedTerrain');
    }, [tabProperties.selectedTerrain, tabProperties.selectedLayerKind])
    //#endregion
    //#region Save Functions
    const save3DVector = (...args) => {
        runCodeSafely(() => {
            const [point, flagNull, sectionPointType] = args;
            setPropertiesCallback(sectionPointType, point);
        }, 'SpatialQueriesForm/General.save3DVector');
    }
    const savePointsTable = (...args) => {
        runCodeSafely(() => {
            const [locationPointsList, valid, selectedPoint, pointsType] = args;
            if (!_.isEqual(tabProperties[pointsType], locationPointsList)) {
                setPropertiesCallback(pointsType, locationPointsList);
            }
        }, 'SpatialQueriesForm/General.savePointsTable');
    }
    //#endregion
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
        }, 'SpatialQueriesForm/General.getActiveOverlay');
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
        }, 'SpatialQueriesForm/General.getViewportData');
        return viewportData;
    }
    const getAnglesLineObject = (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) => {
        runCodeSafely(() => {
            if (nExitCode == 1) {
                let points: MapCore.SMcVector3D[] = pObject.GetLocationPoints(0);
                pObject.Remove();
                setPropertiesCallback('anglesLinePoints', points);
                setPropertiesCallback('anglesPoint', points[0]);
                if (tabProperties.activeOverlay) {
                    let currCoordSysOM = tabProperties.activeOverlay.GetOverlayManager().GetCoordinateSystemDefinition();
                    let geographicCalculations = currCoordSysOM ? MapCore.IMcGeographicCalculations.Create(currCoordSysOM) : null;
                    if (geographicCalculations) {
                        let pAzimuth: { Value?: number } = {};
                        runMapCoreSafely(() => {
                            geographicCalculations.AzimuthAndDistanceBetweenTwoLocations(points[0], points[1], pAzimuth);
                        }, 'SpatialQueriesForm/General.getAnglesLineObject => IMcGeographicCalculations.AzimuthAndDistanceBetweenTwoLocations', true)
                        setPropertiesCallback('azimuth', pAzimuth.Value)
                    }
                }
            }
        }, 'SpatialQueriesForm/General.getAnglesLineObject');
    }
    const getPathLineObject = (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) => {
        runCodeSafely(() => {
            if (nExitCode == 1) {
                let points: MapCore.SMcVector3D[] = pObject.GetLocationPoints(0);
                setPropertiesCallback('pathPoints', points);
                spatialQueriesService.removeExistObjects();
                dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.InitRandomGPSPointInputs, objects: [pObject], removeObjectsCB: (objects: MapCore.IMcObject[]) => { objects[0].Remove() } }))
            }
        }, 'SpatialQueriesForm/General.getPathLineObject');
    }
    const getAddPointsLineObject = (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) => {
        runCodeSafely(() => {
            if (nExitCode == 1) {
                let points: MapCore.SMcVector3D[] = pObject.GetLocationPoints(0);
                setPropertiesCallback('addPoints', points);
                let allObjects = [...spatialQueriesResultsObjectsRef.current.objects, pObject];
                dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.InitRandomGPSPointInputs, objects: allObjects, removeObjectsCB: spatialQueriesService.removeObjectsCB }))
            }
        }, 'SpatialQueriesForm/General.getAddPointsLineObject');
    }
    const getTerrainHeightAtPoint = (point: MapCore.SMcVector3D, isAsync: boolean, queryParams: any) => {
        runCodeSafely(() => {
            let isHeightFound: boolean = false;
            let pHeight: { Value?: number } = {};
            let pNormal: { Value?: MapCore.SMcVector3D } = {};
            if (isAsync) {
                queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((bHeightFound: boolean, dHeight: number, pNormal: MapCore.SMcVector3D) => {
                    setTerrainHeightAtPointResults(bHeightFound, dHeight, point);
                }, (eErrorCode: MapCore.IMcErrors.ECode) => { }, 'SpatialQueriesForm/General.getTerrainHeightAtPoint => IMcSpatialQueries.GetTerrainHeight');
            }
            else {
                queryParams.pAsyncQueryCallback = null;
            }
            runMapCoreSafely(() => {
                isHeightFound = tabProperties.mcCurrentSpatialQueries.GetTerrainHeight(point, pHeight, pNormal, queryParams)
            }, 'SpatialQueriesForm/General.getTerrainHeightAtPoint => IMcSpatialQueries.GetTerrainHeight', true)
            if (!isAsync) {
                setTerrainHeightAtPointResults(isHeightFound, pHeight.Value, point);
            }
        }, 'SpatialQueriesForm/General.getTerrainHeightAtPoint');
    }
    const setDistancesFromLocationsAndResult = (geographicCalculations: MapCore.IMcGeographicCalculations, locationResult: MapCore.SMcVector3D, firstOrigin: MapCore.SMcVector3D, secondOrigin: MapCore.SMcVector3D) => {
        runCodeSafely(() => {
            let pDistance: { Value?: number } = {};
            runMapCoreSafely(() => {
                geographicCalculations.AzimuthAndDistanceBetweenTwoLocations(
                    spatialQueriesService.convertPointFromOMtoVP(firstOrigin, tabProperties.mcCurrentSpatialQueries),
                    locationResult, null, pDistance, true);
            }, 'SpatialQueriesForm/General.setDistancesFromLocationsAndResult => IMcGeographicCalculations.AzimuthAndDistanceBetweenTwoLocations', true)
            setPropertiesCallback('distanceFromFirstLocationToResult', pDistance.Value)
            runMapCoreSafely(() => {
                geographicCalculations.AzimuthAndDistanceBetweenTwoLocations(
                    spatialQueriesService.convertPointFromOMtoVP(secondOrigin, tabProperties.mcCurrentSpatialQueries),
                    locationResult, null, pDistance, true);
            }, 'SpatialQueriesForm/General.setDistancesFromLocationsAndResult => IMcGeographicCalculations.AzimuthAndDistanceBetweenTwoLocations', true)
            setPropertiesCallback('distanceFromSecondLocationToResult', pDistance.Value)
        }, 'SpatialQueriesForm/General.setDistancesFromLocationsAndResult');
    }
    //#endregion
    //#region Set Results
    const setLocationQueryResults = (firstOrigin: MapCore.SMcVector3D, firstDistance: number, firstAzimuth: number, secondOrigin: MapCore.SMcVector3D, secondDistance: number, targetHeightAboveGround: number, queryParams: any,
        locationResult: MapCore.SMcVector3D, isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            setPropertiesCallback('firstOrigin', firstOrigin)
            setPropertiesCallback('firstDistance', firstDistance)
            setPropertiesCallback('firstAzimuth', firstAzimuth)
            setPropertiesCallback('secondOrigin', secondOrigin)
            setPropertiesCallback('secondDistance', secondDistance)
            setPropertiesCallback('targetHeightAboveGround', targetHeightAboveGround)
            let convertedLocation = spatialQueriesService.convertPointFromVPtoOM(locationResult, tabProperties.mcCurrentSpatialQueries);
            setPropertiesCallback('locationResult', convertedLocation)
            setPropertiesCallback('isLocationAsync', isAsync)
            getTerrainHeightAtPoint(locationResult, isAsync, queryParams);

            if (tabProperties.activeOverlay) {
                let currCoordSysOM = tabProperties.activeOverlay.GetOverlayManager().GetCoordinateSystemDefinition();
                let geographicCalculations = currCoordSysOM ? MapCore.IMcGeographicCalculations.Create(currCoordSysOM) : null;
                if (geographicCalculations) {
                    setDistancesFromLocationsAndResult(geographicCalculations, locationResult, firstOrigin, secondOrigin);
                }
            }

            spatialQueriesService.manageQueryResultObjects(tabProperties.mcCurrentSpatialQueries, SpatialQueryName.LocationFromTwoDistancesAndAzimuth, firstOrigin, firstDistance, secondOrigin, secondDistance, locationResult)
        }, 'SpatialQueriesForm/General.setLocationQueryResults')
        if (!isFromTable) {
            let args = [firstOrigin, firstDistance, firstAzimuth, secondOrigin, secondDistance, targetHeightAboveGround, queryParams, locationResult, isAsync, true, errorMessage];
            let queryResult = new QueryResult(SpatialQueryName.LocationFromTwoDistancesAndAzimuth,
                setLocationQueryResults, args, tabProperties.isLocationAsync, errorMessage);
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    const setTerrainHeightAtPointResults = (bHeightFound: boolean, dHeight: number, locationResult: MapCore.SMcVector3D) => {
        runCodeSafely(() => {
            setPropertiesCallback('isHeightFound', bHeightFound)
            setPropertiesCallback('differenceHeightFromResultToTerrain', bHeightFound ? locationResult.z - dHeight : locationResult.z)
        }, 'SpatialQueriesForm/General.setTerrainHeightAtPointResults')
    }
    const setTerrainAnglesQueryResults = (linePoints: MapCore.SMcVector3D[], point: MapCore.SMcVector3D, azimuth: number, pitch: number, roll: number,
        isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            setPropertiesCallback('anglesLinePoints', linePoints)
            setPropertiesCallback('anglesPoint', point)
            setPropertiesCallback('azimuth', azimuth)
            setPropertiesCallback('pitch', pitch)
            setPropertiesCallback('roll', roll)
            setPropertiesCallback('isTerrainAnglesAsync', isAsync)
        }, 'SpatialQueriesForm/General.setTerrainAnglesQueryResults')
        if (!isFromTable) {
            let args = [linePoints, point, azimuth, pitch, roll, isAsync, true, errorMessage];
            let queryResult = new QueryResult(SpatialQueryName.GetTerrainAngles,
                setTerrainAnglesQueryResults, args, tabProperties.isTerrainAnglesAsync, errorMessage);
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    //#endregion
    //#region Handle Query Error
    const handleAsyncLocationQueryError = (eErrorCode: MapCore.IMcErrors.ECode, firstOrigin: MapCore.SMcVector3D, firstDistance: number, firstAzimuth: number, secondOrigin: MapCore.SMcVector3D, secondDistance: number, targetHeightAboveGround: number, queryParams: any) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/General.handleAsyncLocationQueryError => IMcErrors.ErrorCodeToString', true)
            setLocationQueryResults(firstOrigin, firstDistance, firstAzimuth, secondOrigin, secondDistance, targetHeightAboveGround, queryParams,
                MapCore.v3Zero, true, false, errorMessage);
        }, 'SpatialQueriesForm/General.handleAsyncLocationQueryError')
    }
    const handleAsyncTerrainAnglesQueryError = (eErrorCode: MapCore.IMcErrors.ECode, linePoints: MapCore.SMcVector3D[], point: MapCore.SMcVector3D, azimuth: number) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/General.handleAsyncTerrainAnglesQueryError => IMcErrors.ErrorCodeToString', true)
            setTerrainAnglesQueryResults(linePoints, point, azimuth, 0, 0, true, false, errorMessage);
        }, 'SpatialQueriesForm/General.handleAsyncTerrainAnglesQueryError')
    }
    //#endregion
    //#region Handle Functoins
    const handleGetLocationClick = () => {
        let locationResult = MapCore.v3Zero;
        let errorMessage = '';
        let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
        runCodeSafely(() => {
            if (tabProperties.isLocationAsync) {
                queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((Target: MapCore.SMcVector3D) => {
                    setLocationQueryResults(tabProperties.firstOrigin, tabProperties.firstDistance, tabProperties.firstAzimuth, tabProperties.secondOrigin, tabProperties.secondDistance, tabProperties.targetHeightAboveGround, queryParams,
                        Target, tabProperties.isLocationAsync, false, errorMessage);
                }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                    handleAsyncLocationQueryError(eErrorCode, tabProperties.firstOrigin, tabProperties.firstDistance, tabProperties.firstAzimuth, tabProperties.secondOrigin, tabProperties.secondDistance, tabProperties.targetHeightAboveGround, queryParams)
                }, 'SpatialQueriesForm/General.handleGetLocationClick => IMcSpatialQueries.LocationFromTwoDistancesAndAzimuth');
            }
            else {
                queryParams.pAsyncQueryCallback = null;
            }
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            runMapCoreSafely(() => {
                locationResult = mcCurrentSpatialQueries.LocationFromTwoDistancesAndAzimuth(tabProperties.firstOrigin, tabProperties.firstDistance, tabProperties.firstAzimuth, tabProperties.secondOrigin, tabProperties.secondDistance, tabProperties.targetHeightAboveGround, queryParams)
            }, 'SpatialQueriesForm/General.handleGetLocationClick => IMcSpatialQueries.LocationFromTwoDistancesAndAzimuth', true, (error) => { errorMessage = String(error) })
        }, 'SpatialQueriesForm/General.handleGetLocationClick');
        if (!tabProperties.isLocationAsync) {
            setLocationQueryResults(tabProperties.firstOrigin, tabProperties.firstDistance, tabProperties.firstAzimuth, tabProperties.secondOrigin, tabProperties.secondDistance, tabProperties.targetHeightAboveGround, queryParams,
                locationResult, tabProperties.isLocationAsync, false, errorMessage);
        }
    }
    const handleSetTerrainQueriesClick = () => {
        runCodeSafely(() => {
            if (tabProperties.selectedTerrain) {
                let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
                runMapCoreSafely(() => {
                    mcCurrentSpatialQueries.SetTerrainQueriesNumCacheTiles(tabProperties.selectedTerrain.nodeMcContent, tabProperties.selectedLayerKind.theEnum, tabProperties.numTiles);
                }, 'SpatialQueriesForm/General.handleGetLocationClick => IMcSpatialQueries.SetTerrainQueriesNumCacheTiles', true)
            }
            else {
                setPropertiesCallback('confirmDialogVisible', true);
                setPropertiesCallback('confirmDialogMessage', 'Please choose terrain from the list');
            }
        }, 'SpatialQueriesForm/General.handleSetTerrainQueriesClick');
    }
    const handleGetTerrainAnglesClick = () => {
        let pPitch: { Value?: number } = {};
        let pRoll: { Value?: number } = {};
        let errorMessage = '';
        runCodeSafely(() => {
            let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
            if (tabProperties.isTerrainAnglesAsync) {
                queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((dPitch: number, dRoll: number) => {
                    setTerrainAnglesQueryResults(tabProperties.anglesLinePoints, tabProperties.anglesPoint, tabProperties.azimuth, dPitch, dRoll, tabProperties.isTerrainAnglesAsync, false, errorMessage);
                }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                    handleAsyncTerrainAnglesQueryError(eErrorCode, tabProperties.anglesLinePoints, tabProperties.anglesPoint, tabProperties.azimuth)
                }, 'SpatialQueriesForm/General.handleGetTerrainAnglesClick => IMcSpatialQueries.GetTerrainAngles');
            }
            else {
                queryParams.pAsyncQueryCallback = null;
            }
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            runMapCoreSafely(() => {
                mcCurrentSpatialQueries.GetTerrainAngles(tabProperties.anglesPoint, tabProperties.azimuth, pPitch, pRoll, queryParams)
            }, 'SpatialQueriesForm/General.handleGetTerrainAnglesClick => IMcSpatialQueries.GetTerrainAngles', true, (error) => { errorMessage = String(error) })
        }, 'SpatialQueriesForm/General.handleGetTerrainAnglesClick');
        if (!tabProperties.isTerrainAnglesAsync) {
            setTerrainAnglesQueryResults(tabProperties.anglesLinePoints, tabProperties.anglesPoint, tabProperties.azimuth, pPitch.Value, pRoll.Value, tabProperties.isTerrainAnglesAsync, false, errorMessage);
        }
    }
    const handleDrawAddPointsLineButtonClick = () => {
        runCodeSafely(() => {
            if (spatialQueriesResultsObjectsRef.current.queryName !== SpatialQueryName.InitRandomGPSPointInputs) {
                spatialQueriesService.removeExistObjects();
            }
        }, 'SpatialQueriesForm/General.handleDrawAddPointsLineButtonClick');
    }
    //#endregion
    //#region DOM Functions
    const getLocationFromTwoDistancesAndAzimuthFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend="Location From Two Distances And Azimuth">
            <div className="form__flex-and-row-between form__items-center">
                <label htmlFor='firstOrigin'>First Origin:</label>
                <Vector3DFromMap flagNull={{ x: false, y: false, z: false }} initValue={tabProperties.firstOrigin} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD}
                    disabledPointFromMap={tabProperties.activeOverlay ? true : false} saveTheValue={(...args) => { save3DVector(...args, 'firstOrigin') }} lastPoint={true} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <label htmlFor='firstDistance'>First Distance:</label>
                <InputNumber name='firstDistance' id='firstDistance' value={tabProperties.firstDistance} onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <label htmlFor='firstAzimuth'>First Azimuth:</label>
                <InputNumber name='firstAzimuth' id='firstAzimuth' value={tabProperties.firstAzimuth} onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <label htmlFor='secondOrigin'>Second Origin:</label>
                <Vector3DFromMap flagNull={{ x: false, y: false, z: false }} initValue={tabProperties.secondOrigin} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD}
                    disabledPointFromMap={tabProperties.activeOverlay ? true : false} saveTheValue={(...args) => { save3DVector(...args, 'secondOrigin') }} lastPoint={true} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <label htmlFor='secondDistance'>Second Distance:</label>
                <InputNumber name='secondDistance' id='secondDistance' value={tabProperties.secondDistance}
                    onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <label htmlFor='targetHeightAboveGround'>Target Height Above Ground:</label>
                <InputNumber name='targetHeightAboveGround' id='targetHeightAboveGround' value={tabProperties.targetHeightAboveGround}
                    onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <Button onClick={handleGetLocationClick}>Get Location</Button>
                <div style={{ width: '30%' }} className="form__flex-and-row form__items-center">
                    <Checkbox name='isLocationAsync' checked={tabProperties.isLocationAsync} onChange={saveData} />
                    <label>Async</label>
                </div>
            </div>
            <Fieldset className="form__disabled form__column-fieldset" legend="Result">
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor='locationResult'>Location Result: </label>
                    <Vector3DFromMap flagNull={{ x: false, y: false, z: false }} initValue={tabProperties.locationResult} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD}
                        disabledPointFromMap={tabProperties.activeOverlay ? true : false} saveTheValue={(...args) => { }} lastPoint={true} />
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor='distanceFromFirstLocationToResult'>Distance From First Location To Result: </label>
                    <InputNumber value={tabProperties.distanceFromFirstLocationToResult} />
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor='distanceFromSecondLocationToResult'>Distance From Second Location To Result: </label>
                    <InputNumber value={tabProperties.distanceFromSecondLocationToResult} />
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor='differenceHeightFromResultToTerrain'>Difference Height From Result To Terrain: </label>
                    <InputMaxNumber value={tabProperties.differenceHeightFromResultToTerrain} maxValue={MapCore.DBL_MAX} />
                </div>
                <div className="form__flex-and-row form__items-center">
                    <Checkbox checked={tabProperties.isHeightFound} />
                    <span>Height Found: </span>
                </div>
            </Fieldset>
        </Fieldset>
    }
    const getTerrainQueriesFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend="Terrain Queries Num Cache Tiles">
            <div className="form__flex-and-row-between form__items-center">
                <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 12}vh`, maxHeight: `${globalSizeFactor * 12}vh`, }}
                    style={{ width: '100%' }} name='selectedTerrain' optionLabel='label' value={tabProperties.selectedTerrain} onChange={saveData} options={tabProperties.terrainsList} />
            </div>
            <div style={{ width: '60%' }} className="form__flex-and-row-between form__items-center">
                <span>Layer Kind:</span>
                <Dropdown style={{ width: `${globalSizeFactor * 14}vh` }} name="selectedLayerKind" value={tabProperties.selectedLayerKind} onChange={saveData} options={tabProperties.layerKindArr} optionLabel="name" />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '60%' }} className="form__flex-and-row-between form__items-center">
                    <label htmlFor="numTiles">Num Tiles: </label>
                    <InputNumber id='numTiles' value={tabProperties.numTiles} name="numTiles" onValueChange={saveData} />
                </div>
                <Button style={{ width: '20%' }} label="Set" onClick={handleSetTerrainQueriesClick} />
            </div>
        </Fieldset>
    }
    const getTrackSmootherFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend="Track Smoother">
            <div className="form__column-container">
                <div className="form__column-container">
                    <span style={{ textDecoration: 'underline', padding: `${globalSizeFactor * 0.7}vh`, paddingTop: '0vh' }}>Path Points: </span>
                    <Vector3DGrid disabledPointFromMap={tabProperties.activeOverlay ? false : true} ctrlHeight={8} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initLocationPointsList={tabProperties.pathPoints} sendPointList={(...args) => savePointsTable(...args, 'pathPoints')} />
                </div>
                <DrawLineCtrl className="form__aligm-self-center" disabled={tabProperties.activeOverlay ? false : true} handleDrawLineButtonClick={spatialQueriesService.removeExistObjects} activeViewport={tabProperties.currentViewportData} initItemResultsCB={getPathLineObject} />
            </div>
            <div className="form__column-container">
                <div className="form__column-container">
                    <span style={{ textDecoration: 'underline', padding: `${globalSizeFactor * 0.7}vh`, paddingTop: '0vh' }}>Add Points: </span>
                    <Vector3DGrid disabledPointFromMap={tabProperties.activeOverlay ? false : true} ctrlHeight={8} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initLocationPointsList={tabProperties.addPoints} sendPointList={(...args) => savePointsTable(...args, 'addPoints')} />
                </div>
                <DrawLineCtrl className="form__aligm-self-center" disabled={tabProperties.activeOverlay ? false : true} handleDrawLineButtonClick={handleDrawAddPointsLineButtonClick} activeViewport={tabProperties.currentViewportData} initItemResultsCB={getAddPointsLineObject} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div className="form__column-container">
                    <Button label="Get Path Height Points" />
                    <Button label="Get Add Points" />
                </div>
                <div className="form__column-container">
                    <div className="form__flex-and-row-between form__items-center">
                        <label htmlFor="smoothDistance">Smooth Distance: </label>
                        <InputNumber id='smoothDistance' value={tabProperties.smoothDistance} name="smoothDistance" onValueChange={saveData} />
                    </div>
                    <Button label="Get Smoothed" />
                </div>
            </div>
        </Fieldset>
    }
    const getTerrainAngles = () => {
        return <Fieldset className="form__column-fieldset" legend='Terrain Angles'>
            <div className="form__flex-and-row-between form__items-center">
                <span>Point:</span>
                <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.anglesPoint} saveTheValue={(...args) => save3DVector(...args, 'anglesPoint')} lastPoint={true} />
            </div>
            <div className="form__column-container">
                <div className="form__column-container">
                    <span style={{ textDecoration: 'underline', padding: `${globalSizeFactor * 0.7}vh` }}>Line Points: </span>
                    <Vector3DGrid disabledPointFromMap={tabProperties.activeOverlay ? false : true} ctrlHeight={8} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initLocationPointsList={tabProperties.anglesLinePoints} sendPointList={(...args) => savePointsTable(...args, 'anglesLinePoints')} />
                </div>
                <DrawLineCtrl className="form__aligm-self-center" disabled={tabProperties.activeOverlay ? false : true} handleDrawLineButtonClick={spatialQueriesService.removeExistObjects} activeViewport={tabProperties.currentViewportData} initItemResultsCB={getAnglesLineObject} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '30%' }} className="form__flex-and-row-between form__items-center">
                    <span>Azimuth:</span>
                    <InputNumber className="form__narrow-input" name="azimuth" value={tabProperties.azimuth} onValueChange={saveData} />
                </div>
                <div style={{ width: '30%' }} className="form__flex-and-row-between form__items-center form__disabled">
                    <span>Pitch:</span>
                    <InputNumber className="form__narrow-input" name="pitch" value={tabProperties.pitch} onValueChange={saveData} />
                </div>
                <div style={{ width: '30%' }} className="form__flex-and-row-between form__items-center form__disabled">
                    <span>Roll:</span>
                    <InputNumber className="form__narrow-input" name="roll" value={tabProperties.roll} onValueChange={saveData} />
                </div>
            </div>
            <div className="form__flex-and-row-between">
                <div style={{ width: '65%' }} className="form__column-container">
                    <Button label='Get' onClick={handleGetTerrainAnglesClick} />
                </div>
                <div style={{ width: '30%' }} className="form__flex-and-row form__items-center">
                    <Checkbox id="isTerrainAnglesAsync" name="isTerrainAnglesAsync" checked={tabProperties.isTerrainAnglesAsync} onChange={saveData} />
                    <label htmlFor="isTerrainAnglesAsync">Async</label>
                </div>
            </div>
        </Fieldset>
    }
    const getMultiThreadTestingFieldset = () => {
        return <Fieldset className={`form__column-fieldset ${tabProperties.isPrecisionHighest ? '' : 'form__disabled'}`} legend='Multi Thread Testing'>
            {!tabProperties.isPrecisionHighest && <div style={{ color: 'red', textDecoration: 'underline', textDecorationColor: 'red' }}>
                Compatibility with currently displayed in the viewport is unsupported
            </div>}
            <div className="form__flex-and-row-between form__items-center">
                <span>First Point:</span>
                <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.firstPoint} saveTheValue={(...args) => save3DVector(...args, 'firstPoint')} lastPoint={true} />
            </div>
            <div className="form__flex-and-row-between">
                <div style={{ width: '49%' }} className="form__column-container">
                    <div className="form__flex-and-row-between form__items-center">
                        <span>Number Of Testing Points:</span>
                        <InputNumber className="form__narrow-input" name="numberOfTestingPoints" value={tabProperties.numberOfTestingPoints} onValueChange={saveData} />
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <span>Number Of Threads:</span>
                        <InputNumber className="form__narrow-input" name="numberOfThreads" value={tabProperties.numberOfThreads} onValueChange={saveData} />
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <span>Test Duration Time (min):</span>
                        <InputNumber className="form__narrow-input" name="testDurationTime" value={tabProperties.testDurationTime} onValueChange={saveData} />
                    </div>
                </div>
                <div style={{ width: '49%', justifyContent: 'space-evenly' }} className='form__column-container'>
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox name='isLogTest' checked={tabProperties.isLogTest} onChange={saveData} />
                        <label>Log Test</label>
                    </div>
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox name='isThrowException' checked={tabProperties.isThrowException} onChange={saveData} />
                        <label>Throw Exception</label>
                    </div>
                    <Button> <div style={{ width: '100%', backgroundColor: '#76c776', color: 'black' }}>{'Start'}</div> </Button>
                </div>
            </div>
        </Fieldset>
    }
    //#endregion

    return (
        <div className="form__flex-and-row-between">
            <div className="form__column-container" style={{ width: '50%' }}>
                {getLocationFromTwoDistancesAndAzimuthFieldset()}
                {/* {getMultiThreadTestingFieldset()} */}
            </div>
            <div className="form__column-container" style={{ width: '50%' }}>
                {getTerrainQueriesFieldset()}
                {/* {getTrackSmootherFieldset()} */}
                {getTerrainAngles()}
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
