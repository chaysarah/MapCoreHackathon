import { Fieldset } from "primereact/fieldset";
import { RadioButton } from "primereact/radiobutton";
import { useDispatch, useSelector } from "react-redux";
import { InputNumber } from "primereact/inputnumber";
import { useEffect } from "react";
import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { Checkbox } from "primereact/checkbox";
import { Dropdown } from "primereact/dropdown";
import { ConfirmDialog } from "primereact/confirmdialog";
import _ from 'lodash';

import { MapCoreData, ObjectWorldService, getEnumValueDetails, getEnumDetailsList, ViewportData } from 'mapcore-lib';
import { AreaOfSightOptionResultObjects, AreaOfSightOptionsEnum, AreaOfSightResult, AreaOfSightSingleScouterGeneralParams } from "./structs";
import AOSOperation, { AOSOperationProperties } from "./aOSOperation";
import PointVisibilityColor, { PointVisibilityColorProperties } from "./pointVisibilityColor";
import CoverageQuality, { CoverageQualityProperties } from "./coverageQuality";
import ColorsSurrounding, { ColorsSurroundingProperties } from "./colorsSurrounding";
import MatrixOperation, { MatrixOperationProperties } from "./matrixOperation";
import { ParamsProperties } from "../params";
import { QueryResult, SpatialQueryName } from "../spatialQueriesFooter";
import Vector3DFromMap from "../../../../objectWorldTree/shared/Vector3DFromMap";
import Vector3DGrid from "../../../../objectWorldTree/shared/vector3DGrid";
import NestedTabsCtrl from "../../../../../shared/tabCtrls/nestedTabsCtrl";
import UploadFilesCtrl, { UploadTypeEnum } from "../../../../../shared/uploadFilesCtrl";
import { TabInfo } from "../../../../../shared/tabCtrls/tabModels";
import ColorPickerCtrl from "../../../../../../shared/colorPicker";
import { AppState } from "../../../../../../../redux/combineReducer";
import DrawObjectCtrl, { createSchemeItemByType, getItemTypeByObject, ObjectTypeEnum } from "../../../../../shared/drawObjectCtrl";
import spatialQueriesService from "../../../../../../../services/spatialQueries.service";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../../common/services/error-handling/errorHandler";
import { addQueryResultsTableRow, setSpatialQueriesResultsObjects } from "../../../../../../../redux/MapWorldTree/mapWorldTreeActions";
import objectWorldTreeService from "../../../../../../../services/objectWorldTree.service";

export default function AreaOfSight(props: { tabInfo: TabInfo }) {
    let { saveData, setApplyCallBack, setPropertiesCallback, tabProperties, getTabPropertiesByTabPropertiesClass } = props.tabInfo;
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const spatialQueriesResultsObjects = useSelector((state: AppState) => state.mapWorldTreeReducer.spatialQueriesResultsObjects)
    const mapWorldTree = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree)
    const activeCard: number = useSelector((state: AppState) => state.mapWindowReducer.activeCard);

    //#region UseEffect
    useEffect(() => {
        runCodeSafely(() => {
            if (spatialQueriesResultsObjects.queryName == SpatialQueryName.DrawAreaOfSightObject) {
                let itemDetails = getItemTypeByObject(spatialQueriesResultsObjects.objects[0]);
                if (MapCore.IMcEllipseItem.NODE_TYPE == itemDetails.itemType) {
                    let item = itemDetails.mcItem as MapCore.IMcEllipseItem;
                    setPropertiesCallback('endAngle', item.GetEndAngle())
                    setPropertiesCallback('startAngle', item.GetStartAngle())
                    setPropertiesCallback('radiusX', item.GetRadiusX())
                    setPropertiesCallback('radiusY', item.GetRadiusY())
                }
                else if (MapCore.IMcRectangleItem.NODE_TYPE == itemDetails.itemType) {
                    let item = itemDetails.mcItem as MapCore.IMcRectangleItem;
                    setPropertiesCallback('width', item.GetRadiusX() * 2)
                    setPropertiesCallback('height', item.GetRadiusY() * 2)
                }
            }
        }, 'SpatialQueriesForm/AreaOfSight.UseEffect => spatialQueriesResultsObjects')
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
            if (tabProperties.sightObjectPoints.length > 0 && tabProperties.selectedObjectType != ObjectTypeEnum.polygon) {
                let centerPoint = new MapCore.SMcVector3D(tabProperties.sightObjectPoints[0]);
                centerPoint.z = tabProperties.isScouterHeightAbsolute ? centerPoint.z : 1.7;
                setPropertiesCallback('scouter', centerPoint)
                if (tabProperties.sightObjectPoints[0].z !== centerPoint.z) {
                    setPropertiesCallback('sightObjectPoints', [centerPoint])
                }
            }
        }, 'SpatialQueriesForm/AreaOfSight.UseEffect => sightObjectPoints')
    }, [tabProperties.sightObjectPoints])
    useEffect(() => {
        runCodeSafely(() => {
            let isCreateAutomaticLocal = tabProperties.calculationOptions.includes(AreaOfSightOptionsEnum.isAreaOfSight);
            let aOsOperationProps = { ...tabProperties.AOSOperationProperties, isCreateAutomatic: isCreateAutomaticLocal };
            setPropertiesCallback('AOSOperationProperties', aOsOperationProps)
        }, 'SpatialQueriesForm/AreaOfSight.UseEffect => calculationOptions')
    }, [tabProperties.calculationOptions])
    //#endregion
    //#region Save Functions
    const save3DVector = (...args) => {
        runCodeSafely(() => {
            const [point, flagNull, sectionPointType] = args;
            if (sectionPointType == 'scouter') {
                point.z = tabProperties.isScouterHeightAbsolute ? point.z : 1.7;
            }
            setPropertiesCallback(sectionPointType, point);
        }, 'SpatialQueriesForm/AreaOfSight.save3DVector');
    }
    const savePointsTable = (...args) => {
        runCodeSafely(() => {
            const [locationPointsList, valid, selectedPoint, sectionPointsType] = args;
            setPropertiesCallback(sectionPointsType, locationPointsList);
        }, 'SpatialQueriesForm/AreaOfSight.saveLocationPointsTable');
    }
    const saveAreaOfSightOptions = (e: any, optionsType: string) => {
        runCodeSafely(() => {
            let arr = [...tabProperties[optionsType]]
            let isExist = arr.includes(e.target.name);
            let newArr = isExist ? arr.filter(option => option !== e.target.name) : [...arr, e.target.name];
            setPropertiesCallback(optionsType, newArr)
        }, 'SpatialQueriesForm/AreaOfSight.saveAreaOfSightOptions');
    }
    //#endregion
    //#region Help Functions
    const getActiveOverlay = () => {
        let activeOverlay: MapCore.IMcOverlay = null;
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
        }, 'SpatialQueriesForm/AreaOfSight.getActiveOverlay');
        return activeOverlay;
    }
    const getViewportData = (): ViewportData | null => {
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
        }, 'SpatialQueriesForm/AreaOfSight.getViewportData');
        return viewportData;
    }
    const getObject = (pObject: MapCore.IMcObject) => {
        runCodeSafely(() => {
            let points: MapCore.SMcVector3D[] = pObject.GetLocationPoints(0);
            setPropertiesCallback('sightObjectPoints', points);
            dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.DrawAreaOfSightObject, objects: [pObject], removeObjectsCB: (objects: MapCore.IMcObject[]) => { objects[0].Remove() } }))
        }, 'SpatialQueriesForm/AreaOfSight.getObject');
    }
    const getObjectPoints = (objectPoints: MapCore.SMcVector3D[]) => {
        runCodeSafely(() => {
            setPropertiesCallback('sightObjectPoints', objectPoints);
        }, 'SpatialQueriesForm/AreaOfSight.getObjectPoints');
    }
    const isExistObjectMatchSelectedObjectType = (): boolean => {
        if (spatialQueriesResultsObjects.queryName == SpatialQueryName.DrawAreaOfSightObject) {
            let itemType = getItemTypeByObject(spatialQueriesResultsObjects.objects[0]).itemType;
            if ((MapCore.IMcPolygonItem.NODE_TYPE == itemType && tabProperties.selectedObjectType == ObjectTypeEnum.polygon) ||
                (MapCore.IMcEllipseItem.NODE_TYPE == itemType && tabProperties.selectedObjectType == ObjectTypeEnum.ellipse) ||
                (MapCore.IMcRectangleItem.NODE_TYPE == itemType && tabProperties.selectedObjectType == ObjectTypeEnum.rectangle)) {
                return true;
            }
            return false;
        }
        return false;
    }
    const getExistObject = () => {
        let existObject = null;
        runCodeSafely(() => {
            if (isExistObjectMatchSelectedObjectType()) {
                let obj = spatialQueriesResultsObjects.objects[0] as MapCore.IMcObject;
                let isViewportStillExist = MapCoreData.viewportsData.find((vp: ViewportData) => vp.viewport.GetOverlayManager() == obj.GetOverlayManager())
                if (isViewportStillExist) {
                    existObject = spatialQueriesResultsObjects.objects[0];
                }
                else {
                    spatialQueriesService.removeExistObjects();
                }
            }
        }, 'SpatialQueriesForm/AreaOfSight.getExistObject');
        return existObject;
    }
    const getSchemeItemAttributesByType = (objectType?: ObjectTypeEnum, data?: any) => {
        let attributesObj: any = {};
        let objectTypeLocal = objectType ? objectType : tabProperties.selectedObjectType;
        runCodeSafely(() => {
            switch (objectTypeLocal) {
                case ObjectTypeEnum.ellipse:
                    attributesObj = {
                        StartAngle: data?.StartAngle || tabProperties.startAngle,
                        EndAngle: data?.EndAngle || tabProperties.endAngle,
                        RadiusX: data?.RadiusX || tabProperties.radiusX,
                        RadiusY: data?.RadiusY || tabProperties.radiusY
                    }
                    break;
                case ObjectTypeEnum.rectangle:
                    attributesObj = {
                        RadiusX: data?.RadiusX || tabProperties.width,
                        RadiusY: data?.RadiusY || tabProperties.height
                    }
                    break;
            }
        }, 'SpatialQueriesForm/AreaOfSight.getSchemeItemAttributesByType');
        return attributesObj;
    }
    const setSightObjectAttributes = (attributes: any) => {
        let objectTypeName = attributes.EndAngle ? ObjectTypeEnum.ellipse : attributes.RadiusX ? ObjectTypeEnum.rectangle : ObjectTypeEnum.polygon;
        switch (objectTypeName) {
            case ObjectTypeEnum.ellipse:
                setPropertiesCallback('startAngle', attributes.StartAngle);
                setPropertiesCallback('endAngle', attributes.EndAngle);
                setPropertiesCallback('radiusX', attributes.RadiusX);
                setPropertiesCallback('radiusY', attributes.RadiusY);
                setPropertiesCallback('selectedObjectType', ObjectTypeEnum.ellipse);
                break;
            case ObjectTypeEnum.rectangle:
                setPropertiesCallback('width', attributes.RadiusX);
                setPropertiesCallback('height', attributes.RadiusY);
                setPropertiesCallback('selectedObjectType', ObjectTypeEnum.rectangle);
                break;
            case ObjectTypeEnum.polygon:
                setPropertiesCallback('selectedObjectType', ObjectTypeEnum.polygon);
                break;
        }
    }
    const getCurrentSingleScouterGeneralParams = (): AreaOfSightSingleScouterGeneralParams => {
        let generalParams = new AreaOfSightSingleScouterGeneralParams(tabProperties.scouter, tabProperties.isScouterHeightAbsolute, tabProperties.targetHeight,
            tabProperties.isTargetHeightAbsolute, tabProperties.targetResolution, tabProperties.rotationAzimuth, tabProperties.numberOfRays,
            tabProperties.visibilityColors, tabProperties.maxPitchAngle, tabProperties.minPitchAngle, tabProperties.isGPUBased,
            tabProperties.nameCalc, tabProperties.calculationOptions, tabProperties.AOSOperationProperties.isCreateAutomatic, tabProperties.AOSOperationProperties.bPointVisibility);
        return generalParams;
    }
    const isMcResultExistByOption = (option: AreaOfSightOptionsEnum, areaOfSightResults: AreaOfSightResult, calculationOptions: AreaOfSightOptionsEnum[]) => {
        switch (option) {
            case AreaOfSightOptionsEnum.isLineOfSight:
                return calculationOptions.includes(option) && areaOfSightResults.paLinesOfSight?.length;
            case AreaOfSightOptionsEnum.isAreaOfSight:
                return calculationOptions.includes(option) && areaOfSightResults.ppAreaOfSight;
            case AreaOfSightOptionsEnum.isSeenPolygons:
                return calculationOptions.includes(option) && areaOfSightResults.pSeenPolygons?.aaContoursPoints?.length;
            case AreaOfSightOptionsEnum.isUnseenPolygons:
                return calculationOptions.includes(option) && areaOfSightResults.pUnseenPolygons?.aaContoursPoints?.length;
            case AreaOfSightOptionsEnum.isStaticObjects:
                return calculationOptions.includes(option) && areaOfSightResults.paSeenStaticObjects?.length;
            default:
                break;
        }
    }
    //#endregion
    //#region Set Results
    const setBestScouterQueryResults = ( /*inputs*/ isTargetHeightAbsolute: boolean, scoutersRadiusX: number, scoutersRadiusY: number, targetHeight: number, isScouterHeightAbsolute: boolean, maxNumOfScouters: number, attributes: { [key: string]: any }, sightObjectPoints: MapCore.SMcVector3D[],
        /*result*/ scoutersPoints: MapCore.SMcVector3D[], isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            //inputs
            setPropertiesCallback('scoutersAmount', 'multiple')
            setPropertiesCallback('isTargetHeightAbsolute', isTargetHeightAbsolute)
            setSightObjectAttributes(attributes);
            setPropertiesCallback('scoutersRadiusX', scoutersRadiusX)
            setPropertiesCallback('scoutersRadiusY', scoutersRadiusY)
            setPropertiesCallback('targetHeight', targetHeight)
            setPropertiesCallback('isScouterHeightAbsolute', isScouterHeightAbsolute)
            setPropertiesCallback('maxNumOfScouters', maxNumOfScouters)
            setPropertiesCallback('sightObjectPoints', sightObjectPoints)
            //result
            setPropertiesCallback('scoutersPoints', scoutersPoints)
            setPropertiesCallback('isBestScoutersAsync', isAsync)

            spatialQueriesService.manageQueryResultObjects(tabProperties.mcCurrentSpatialQueries, SpatialQueryName.GetBestScoutersLocationsInEllipse, scoutersPoints, sightObjectPoints, attributes);
        }, 'SpatialQueriesForm/AreaOfSight.setBestScouterQueryResults')
        if (!isFromTable) {
            let args = [isTargetHeightAbsolute, scoutersRadiusX, scoutersRadiusY, targetHeight, isScouterHeightAbsolute, maxNumOfScouters, attributes, sightObjectPoints,
                scoutersPoints, isAsync, true, errorMessage];
            let queryResult = new QueryResult(SpatialQueryName.GetBestScoutersLocationsInEllipse,
                setBestScouterQueryResults, args, tabProperties.isBestScoutersAsync, errorMessage);
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    const setEllipseAreaOfSightMultipleQueryResults = (
        /*inputs*/
        scoutersPoints: MapCore.SMcVector3D[], isScouterHeightAbsolute: boolean, targetHeight: number, isTargetHeightAbsolute: boolean, targetResolution: number,
        sightObjectPoints: MapCore.SMcVector3D[], ellipseAttributes: any, numberOfRays: number, selectedScoutersSumType: MapCore.IMcSpatialQueries.EScoutersSumType,
        maxPitchAngle: number, minPitchAngle: number, isGPUBased: boolean, nameCalc: string, maxNumOfScouters: number, isCreateAutomatic: boolean, bPointVisibility: boolean,
        /*result*/
        areaOfSight: MapCore.IMcSpatialQueries.IAreaOfSight, isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            let scoutersSumTypeEnumList = getEnumDetailsList(MapCore.IMcSpatialQueries.EScoutersSumType);
            let selectedScoutersSumTypeEnum = getEnumValueDetails(selectedScoutersSumType, scoutersSumTypeEnumList);

            setPropertiesCallback('scoutersAmount', 'multiple')
            setPropertiesCallback('scoutersPoints', scoutersPoints)
            setPropertiesCallback('isScouterHeightAbsolute', isScouterHeightAbsolute)
            setPropertiesCallback('targetHeight', targetHeight)
            setPropertiesCallback('isTargetHeightAbsolute', isTargetHeightAbsolute)
            setPropertiesCallback('targetResolution', targetResolution)
            setPropertiesCallback('sightObjectPoints', sightObjectPoints)
            setSightObjectAttributes(ellipseAttributes);
            setPropertiesCallback('numberOfRays', numberOfRays)
            setPropertiesCallback('selectedScoutersSumType', selectedScoutersSumTypeEnum)
            setPropertiesCallback('maxPitchAngle', maxPitchAngle)
            setPropertiesCallback('minPitchAngle', minPitchAngle)
            setPropertiesCallback('isGPUBased', isGPUBased)
            setPropertiesCallback('nameCalc', nameCalc)
            setPropertiesCallback('maxNumOfScouters', maxNumOfScouters)
            setPropertiesCallback('isAsync', isAsync)
            //Results
            let areaOfSightResult = new AreaOfSightResult(areaOfSight);
            setPropertiesCallback('mapcoreResults', areaOfSightResult)
            let existOptionsSelection = [{ option: AreaOfSightOptionsEnum.isDrawObject, isSelected: true }]
            if (areaOfSight && isCreateAutomatic) {
                existOptionsSelection = [...existOptionsSelection, { option: AreaOfSightOptionsEnum.isAreaOfSight, isSelected: true }]
            }
            setPropertiesCallback('existOptionsSelection', existOptionsSelection)
            let aOsOperationProps = { ...tabProperties.AOSOperationProperties, isCreateAutomatic: isCreateAutomatic, bPointVisibility: bPointVisibility, mapcoreResults: areaOfSightResult, selectedScoutersSumType: selectedScoutersSumTypeEnum, maxNumOfScouters: maxNumOfScouters };
            setPropertiesCallback('AOSOperationProperties', aOsOperationProps)
            //Draw Results Objects
            let ellipseArgs = [sightObjectPoints, ellipseAttributes];
            spatialQueriesService.manageQueryResultObjects(tabProperties.mcCurrentSpatialQueries, SpatialQueryName.GetAreaOfSightForMultipleScouters, areaOfSight, selectedScoutersSumType, maxNumOfScouters, ellipseArgs, isCreateAutomatic, bPointVisibility);
        }, 'SpatialQueriesForm/AreaOfSight.setEllipseAreaOfSightMultipleQueryResults')
        if (!isFromTable) {
            let matrix = areaOfSight?.GetAreaOfSightMatrix(bPointVisibility);
            let matrixOperationProps = { ...tabProperties.MatrixOperationProperties, matricesList: [...tabProperties.MatrixOperationProperties.matricesList, { index: tabProperties.MatrixOperationProperties.matricesList.length, label: `Area Of Sight of ${nameCalc} (${isGPUBased ? 'GPU' : 'CPU'})`, areaOfSightMatrix: matrix }] };
            areaOfSight && setPropertiesCallback('MatrixOperationProperties', matrixOperationProps)

            let args = [
                /*inputs*/
                scoutersPoints, isScouterHeightAbsolute, targetHeight, isTargetHeightAbsolute, targetResolution, sightObjectPoints, ellipseAttributes,
                numberOfRays, selectedScoutersSumType, maxPitchAngle, minPitchAngle, isGPUBased, nameCalc, maxNumOfScouters, isCreateAutomatic, bPointVisibility,
                /*result*/
                areaOfSight, isAsync, true, errorMessage];
            let finalName = `${nameCalc} (${isGPUBased ? 'GPU' : 'CPU'})`;
            let queryResult = new QueryResult(SpatialQueryName.GetAreaOfSightForMultipleScouters,
                setEllipseAreaOfSightMultipleQueryResults, args, tabProperties.isAsync, errorMessage, finalName);
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    const setSingleAreaOfSightQueryResults = (
        generalParams: AreaOfSightSingleScouterGeneralParams, sightObjectsParams: any, areaOfSightResults: AreaOfSightResult,
        isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            setPropertiesCallback('scoutersAmount', 'single')
            setPropertiesCallback('scouter', generalParams.scouter)
            setPropertiesCallback('isScouterHeightAbsolute', generalParams.bIsScouterHeightAbsolute)
            setPropertiesCallback('targetHeight', generalParams.dTargetHeight)
            setPropertiesCallback('isTargetHeightAbsolute', generalParams.bTargetsHeightAbsolute)
            setPropertiesCallback('targetResolution', generalParams.fTargetResolutionInMeters)
            setPropertiesCallback('rotationAzimuth', generalParams.dRotationAngle)
            setPropertiesCallback('numberOfRays', generalParams.uNumRaysPer360Degrees)
            setPropertiesCallback('visibilityColors', generalParams.aVisibilityColors)
            setPropertiesCallback('maxPitchAngle', generalParams.dMaxPitchAngle)
            setPropertiesCallback('minPitchAngle', generalParams.dMinPitchAngle)
            setPropertiesCallback('isGPUBased', generalParams.bGPUBase)
            setPropertiesCallback('nameCalc', generalParams.nameCalc)
            setPropertiesCallback('calculationOptions', generalParams.calculationOptions)
            setPropertiesCallback('sightObjectPoints', sightObjectsParams.locationPoints)
            setSightObjectAttributes(sightObjectsParams);
            setPropertiesCallback('isAsync', isAsync)
            //Results
            setPropertiesCallback('mapcoreResults', areaOfSightResults)
            let existOptionsSelection = [{ option: AreaOfSightOptionsEnum.isDrawObject, isSelected: true }];
            generalParams.calculationOptions.map(option => {
                let isMcResultExist = isMcResultExistByOption(option, areaOfSightResults, generalParams.calculationOptions);
                let isManualAreaOfSight = option == AreaOfSightOptionsEnum.isAreaOfSight && !generalParams.isCreateAutomatic
                if (isMcResultExist && !isManualAreaOfSight) {
                    existOptionsSelection = [...existOptionsSelection, { option: option, isSelected: true }]
                } else if (isMcResultExist) {
                    existOptionsSelection = [...existOptionsSelection, { option: option, isSelected: false }]
                }
            })
            setPropertiesCallback('existOptionsSelection', existOptionsSelection)
            let aOsOperationProps = { ...tabProperties.AOSOperationProperties, isCreateAutomatic: generalParams.isCreateAutomatic, bPointVisibility: generalParams.bPointVisibility, mapcoreResults: areaOfSightResults };
            setPropertiesCallback('AOSOperationProperties', aOsOperationProps)
            //Result Objects
            let visibilityColors = generalParams.aVisibilityColors;
            let isCreateAutomatic = generalParams.isCreateAutomatic;
            let bPointVisibility = generalParams.bPointVisibility;
            spatialQueriesService.manageQueryResultObjects(tabProperties.mcCurrentSpatialQueries, SpatialQueryName.GetAreaOfSight, areaOfSightResults, sightObjectsParams, visibilityColors, isCreateAutomatic, bPointVisibility);
        }, 'SpatialQueriesForm/AreaOfSight.setSingleAreaOfSightQueryResults')
        if (!isFromTable) {
            let matrix = areaOfSightResults.ppAreaOfSight?.GetAreaOfSightMatrix(generalParams.bPointVisibility);
            let matrixOperationProps = { ...tabProperties.MatrixOperationProperties, matricesList: [...tabProperties.MatrixOperationProperties.matricesList, { index: tabProperties.MatrixOperationProperties.matricesList.length, label: `Area Of Sight of ${generalParams.nameCalc} (${generalParams.bGPUBase ? 'GPU' : 'CPU'})`, areaOfSightMatrix: matrix }] };
            areaOfSightResults.ppAreaOfSight && setPropertiesCallback('MatrixOperationProperties', matrixOperationProps)

            let args = [generalParams, sightObjectsParams, areaOfSightResults, isAsync, true, errorMessage];
            let finalName = `${generalParams.nameCalc} (${generalParams.bGPUBase ? 'GPU' : 'CPU'})`
            let queryResult = new QueryResult(SpatialQueryName.GetAreaOfSight, setSingleAreaOfSightQueryResults, args, tabProperties.isAsync, errorMessage, finalName);
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    //#endregion
    //#region Handle Query Error
    const handleAsyncBestScouterQueryError = (eErrorCode: MapCore.IMcErrors.ECode, /*inputs*/  isTargetHeightAbsolute: boolean, scoutersRadiusX: number, scoutersRadiusY: number, targetHeight: number, isScouterHeightAbsolute: boolean, maxNumOfScouters: number, attributes: { [key: string]: any }, sightObjectPoints: MapCore.SMcVector3D[]) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/AreaOfSight.handleAsyncBestScouterQueryError => IMcErrors.ErrorCodeToString', true)
            setBestScouterQueryResults(/*inputs*/  isTargetHeightAbsolute, scoutersRadiusX, scoutersRadiusY, targetHeight, isScouterHeightAbsolute, maxNumOfScouters, attributes, sightObjectPoints,
                /*result*/[], true, false, errorMessage);
        }, 'SpatialQueriesForm/AreaOfSight.handleAsyncBestScouterQueryError')
    }
    const handleAsyncEllipseAreaOfSightMultipleQueryError = (eErrorCode: MapCore.IMcErrors.ECode, /*inputs*/ scoutersPoints: MapCore.SMcVector3D[], isScouterHeightAbsolute: boolean, targetHeight: number,
        isTargetHeightAbsolute: boolean, targetResolution: number, sightObjectPoints: MapCore.SMcVector3D[], ellipseAttributes: any,
        numberOfRays: number, selectedScoutersSumType: MapCore.IMcSpatialQueries.EScoutersSumType, maxPitchAngle: number, minPitchAngle: number, isGPUBased: boolean, nameCalc: string, maxNumOfScouters: number, isCreateAutomatic: boolean, bPointVisibility: boolean
    ) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/AreaOfSight.handleAsyncEllipseAreaOfSightMultipleQueryError => IMcErrors.ErrorCodeToString', true)
            setEllipseAreaOfSightMultipleQueryResults( /*inputs*/ scoutersPoints, isScouterHeightAbsolute, targetHeight, isTargetHeightAbsolute, targetResolution,
                sightObjectPoints, ellipseAttributes, numberOfRays, selectedScoutersSumType, maxPitchAngle, minPitchAngle, isGPUBased, nameCalc, maxNumOfScouters, isCreateAutomatic, bPointVisibility,
                /*result*/ null, true, false, errorMessage);
        }, 'SpatialQueriesForm/AreaOfSight.handleAsyncEllipseAreaOfSightMultipleQueryError')
    }
    const handleAsyncSingleAreaOfSightQueryError = (eErrorCode: MapCore.IMcErrors.ECode, generalParams: AreaOfSightSingleScouterGeneralParams, sightObjectsParams: any) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/AreaOfSight.handleAsyncSingleAreaOfSightQueryError => IMcErrors.ErrorCodeToString', true)
            let areaOfSightEmptyResults = new AreaOfSightResult(null, [], null, null, []);
            setSingleAreaOfSightQueryResults(generalParams, sightObjectsParams, areaOfSightEmptyResults, true, false, errorMessage);
        }, 'SpatialQueriesForm/AreaOfSight.handleAsyncSingleAreaOfSightQueryError')
    }
    //#endregion
    //#region Handle Query Functions
    const handleGetBestScoutersClick = () => {
        let attributes = getSchemeItemAttributesByType(ObjectTypeEnum.ellipse);
        let scoutersPoints = [];
        let errorMessage = '';
        runCodeSafely(() => {
            let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
            if (tabProperties.isBestScoutersAsync) {
                queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((aScouters: MapCore.SMcVector3D[]) => {
                    setBestScouterQueryResults(
                        /*inputs*/ tabProperties.isTargetHeightAbsolute,
                        tabProperties.scoutersRadiusX, tabProperties.scoutersRadiusY, tabProperties.targetHeight, tabProperties.isScouterHeightAbsolute, tabProperties.maxNumOfScouters, attributes, tabProperties.sightObjectPoints,
                        /*results*/ aScouters, tabProperties.isBestScoutersAsync, false, errorMessage);
                }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                    handleAsyncBestScouterQueryError(eErrorCode,
                       /*inputs*/ tabProperties.isTargetHeightAbsolute, tabProperties.scoutersRadiusX,
                        tabProperties.scoutersRadiusY, tabProperties.targetHeight, tabProperties.isScouterHeightAbsolute, tabProperties.maxNumOfScouters, attributes, tabProperties.sightObjectPoints)
                }, 'SpatialQueriesForm/AreaOfSight.handleGetBestScoutersClick => IMcSpatialQueries.GetBestScoutersLocationsInEllipse');
            }
            else {
                queryParams.pAsyncQueryCallback = null;
            }
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            runMapCoreSafely(() => {
                scoutersPoints = mcCurrentSpatialQueries.GetBestScoutersLocationsInEllipse(tabProperties.sightObjectPoints[0], tabProperties.targetHeight, tabProperties.isTargetHeightAbsolute, tabProperties.radiusX,
                    tabProperties.radiusY, tabProperties.sightObjectPoints[0], tabProperties.scoutersRadiusX, tabProperties.scoutersRadiusY,
                    tabProperties.targetHeight, tabProperties.isScouterHeightAbsolute, tabProperties.maxNumOfScouters, queryParams);
            }, 'SpatialQueriesForm/AreaOfSight.handleGetBestScoutersClick => IMcSpatialQueries.GetBestScoutersLocationsInEllipse', true, (error) => { errorMessage = String(error) })
        }, 'SpatialQueriesForm/AreaOfSight.handleGetBestScoutersClick');
        if (!tabProperties.isBestScoutersAsync) {
            setBestScouterQueryResults(
                /*inputs*/  tabProperties.isTargetHeightAbsolute,
                tabProperties.scoutersRadiusX, tabProperties.scoutersRadiusY, tabProperties.targetHeight, tabProperties.isScouterHeightAbsolute, tabProperties.maxNumOfScouters, attributes, tabProperties.sightObjectPoints,
                /*results*/ scoutersPoints, tabProperties.isBestScoutersAsync, false, errorMessage);
        }
    }
    const handleEllipseAreaOfSightMultiple = () => {
        let ppAreaOfSight: { Value?: MapCore.IMcSpatialQueries.IAreaOfSight } = {};
        let errorMessage = '';
        let ellipseAttributes = getSchemeItemAttributesByType(ObjectTypeEnum.ellipse);
        runCodeSafely(() => {
            let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
            if (tabProperties.isAsync) {
                queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((pAreaOfSight: MapCore.IMcSpatialQueries.IAreaOfSight, aLinesOfSight: MapCore.IMcSpatialQueries.SLineOfSightPoint[][], pSeenPolygons: MapCore.IMcSpatialQueries.SPolygonsOfSight, pUnseenPolygons: MapCore.IMcSpatialQueries.SPolygonsOfSight, aSeenStaticObjects: MapCore.IMcSpatialQueries.SStaticObjectsIDs[]) => {
                    setEllipseAreaOfSightMultipleQueryResults(
                        /*inputs*/
                        tabProperties.scoutersPoints, tabProperties.isScouterHeightAbsolute, tabProperties.targetHeight, tabProperties.isTargetHeightAbsolute, tabProperties.targetResolution,
                        tabProperties.sightObjectPoints, ellipseAttributes, tabProperties.numberOfRays, tabProperties.selectedScoutersSumType.theEnum,
                        tabProperties.maxPitchAngle, tabProperties.minPitchAngle, tabProperties.isGPUBased, tabProperties.nameCalc, tabProperties.maxNumOfScouters, tabProperties.AOSOperationProperties.isCreateAutomatic, tabProperties.AOSOperationProperties.bPointVisibility,
                        /*result*/
                        pAreaOfSight, tabProperties.isAsync, false, errorMessage);
                }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                    handleAsyncEllipseAreaOfSightMultipleQueryError(eErrorCode,
                        /*inputs*/
                        tabProperties.scoutersPoints, tabProperties.isScouterHeightAbsolute, tabProperties.targetHeight, tabProperties.isTargetHeightAbsolute, tabProperties.targetResolution,
                        tabProperties.sightObjectPoints, ellipseAttributes, tabProperties.numberOfRays, tabProperties.selectedScoutersSumType.theEnum,
                        tabProperties.maxPitchAngle, tabProperties.minPitchAngle, tabProperties.isGPUBased, tabProperties.nameCalc, tabProperties.maxNumOfScouters, tabProperties.AOSOperationProperties.isCreateAutomatic, tabProperties.AOSOperationProperties.bPointVisibility)
                }, 'SpatialQueriesForm/AreaOfSight.handleEllipseAreaOfSightMultiple => IMcSpatialQueries.GetEllipseAreaOfSightForMultipleScouters');
            }
            else {
                queryParams.pAsyncQueryCallback = null;
            }
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            runMapCoreSafely(() => {
                mcCurrentSpatialQueries.GetEllipseAreaOfSightForMultipleScouters(
                    tabProperties.scoutersPoints, tabProperties.isScouterHeightAbsolute, tabProperties.targetHeight, tabProperties.isTargetHeightAbsolute, tabProperties.targetResolution,
                    tabProperties.sightObjectPoints[0], tabProperties.radiusX, tabProperties.radiusY, tabProperties.numberOfRays, tabProperties.selectedScoutersSumType.theEnum,
                    ppAreaOfSight, tabProperties.maxPitchAngle, tabProperties.minPitchAngle, queryParams, tabProperties.isGPUBased);
            }, 'SpatialQueriesForm/AreaOfSight.handleEllipseAreaOfSightMultiple => IMcSpatialQueries.GetEllipseAreaOfSightForMultipleScouters', true, (error) => { errorMessage = String(error) })
        }, 'SpatialQueriesForm/AreaOfSight.handleEllipseAreaOfSightMultiple');
        if (!tabProperties.isAsync) {
            setEllipseAreaOfSightMultipleQueryResults(
                /*inputs*/
                tabProperties.scoutersPoints, tabProperties.isScouterHeightAbsolute, tabProperties.targetHeight, tabProperties.isTargetHeightAbsolute, tabProperties.targetResolution,
                tabProperties.sightObjectPoints, ellipseAttributes, tabProperties.numberOfRays, tabProperties.selectedScoutersSumType.theEnum,
                tabProperties.maxPitchAngle, tabProperties.minPitchAngle, tabProperties.isGPUBased, tabProperties.nameCalc, tabProperties.maxNumOfScouters, tabProperties.AOSOperationProperties.isCreateAutomatic, tabProperties.AOSOperationProperties.bPointVisibility,
                /*result*/
                ppAreaOfSight?.Value, tabProperties.isAsync, false, errorMessage);
        }
    }
    const handleSingleAreaOfSight = () => {
        let areaOfSightOptions = tabProperties.calculationOptions as AreaOfSightOptionsEnum[];
        let ppAreaOfSight: { Value?: MapCore.IMcSpatialQueries.IAreaOfSight } = areaOfSightOptions.includes(AreaOfSightOptionsEnum.isAreaOfSight) ? {} : null;
        let paLinesOfSight: MapCore.IMcSpatialQueries.SLineOfSightPoint[][] = areaOfSightOptions.includes(AreaOfSightOptionsEnum.isLineOfSight) ? [] : null;
        let pSeenPolygons: { Value?: MapCore.IMcSpatialQueries.SPolygonsOfSight } = areaOfSightOptions.includes(AreaOfSightOptionsEnum.isSeenPolygons) ? {} : null;
        let pUnseenPolygons: { Value?: MapCore.IMcSpatialQueries.SPolygonsOfSight } = areaOfSightOptions.includes(AreaOfSightOptionsEnum.isUnseenPolygons) ? {} : null;
        let paSeenStaticObjects: MapCore.IMcSpatialQueries.SStaticObjectsIDs[] = areaOfSightOptions.includes(AreaOfSightOptionsEnum.isStaticObjects) ? [] : null;
        let errorMessage = '';
        let generalParams = getCurrentSingleScouterGeneralParams();
        let sightObjectsParams = { ...getSchemeItemAttributesByType(), locationPoints: tabProperties.sightObjectPoints };
        runCodeSafely(() => {
            let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
            if (tabProperties.isAsync) {
                queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((pAreaOfSight: MapCore.IMcSpatialQueries.IAreaOfSight, aLinesOfSight: MapCore.IMcSpatialQueries.SLineOfSightPoint[][], pSeenPolygons: MapCore.IMcSpatialQueries.SPolygonsOfSight, pUnseenPolygons: MapCore.IMcSpatialQueries.SPolygonsOfSight, aSeenStaticObjects: MapCore.IMcSpatialQueries.SStaticObjectsIDs[]) => {
                    let areaOfSightResults = new AreaOfSightResult(pAreaOfSight, aLinesOfSight, pSeenPolygons, pUnseenPolygons, aSeenStaticObjects);
                    setSingleAreaOfSightQueryResults(generalParams, sightObjectsParams, areaOfSightResults, tabProperties.isAsync, false, errorMessage);
                }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                    handleAsyncSingleAreaOfSightQueryError(eErrorCode, generalParams, sightObjectsParams)
                }, 'SpatialQueriesForm/AreaOfSight.handleSingleAreaOfSight => IMcSpatialQueries.GetEllipseAreaOfSightForMultipleScouters');
            }
            else {
                queryParams.pAsyncQueryCallback = null;
            }
            switch (tabProperties.selectedObjectType) {
                case ObjectTypeEnum.ellipse:
                    errorMessage = handleEllipseAreaOfSight(ppAreaOfSight, paLinesOfSight, pSeenPolygons, pUnseenPolygons, paSeenStaticObjects, queryParams);
                    break;
                case ObjectTypeEnum.rectangle:
                    errorMessage = handleRectangleAreaOfSight(ppAreaOfSight, paLinesOfSight, pSeenPolygons, pUnseenPolygons, paSeenStaticObjects, queryParams);
                    break;
                case ObjectTypeEnum.polygon:
                    errorMessage = handlePolygonAreaOfSight(ppAreaOfSight, paLinesOfSight, pSeenPolygons, pUnseenPolygons, paSeenStaticObjects, queryParams);
                    break;
            }
        }, 'SpatialQueriesForm/AreaOfSight.handleSingleAreaOfSight');
        if (!tabProperties.isAsync) {
            let areaOfSightResults = new AreaOfSightResult(ppAreaOfSight?.Value, paLinesOfSight, pSeenPolygons?.Value, pUnseenPolygons?.Value, paSeenStaticObjects);
            setSingleAreaOfSightQueryResults(generalParams, sightObjectsParams, areaOfSightResults, tabProperties.isAsync, false, errorMessage);
        }
    }
    const handleRectangleAreaOfSight = (ppAreaOfSight, paLinesOfSight, pSeenPolygons, pUnseenPolygons, paSeenStaticObjects, queryParams) => {
        let errorMessage = '';
        runCodeSafely(() => {
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            runMapCoreSafely(() => {
                mcCurrentSpatialQueries.GetRectangleAreaOfSight(
                    tabProperties.scouter, tabProperties.isScouterHeightAbsolute, tabProperties.height, tabProperties.width, tabProperties.rotationAzimuth,
                    tabProperties.targetHeight, tabProperties.isTargetHeightAbsolute, tabProperties.targetResolution, tabProperties.numberOfRays,
                    [...tabProperties.visibilityColors.values(), new MapCore.SMcBColor(0, 0, 0, 0)],
                    ppAreaOfSight, paLinesOfSight, pSeenPolygons, pUnseenPolygons, paSeenStaticObjects,
                    tabProperties.maxPitchAngle, tabProperties.minPitchAngle, queryParams, tabProperties.isGPUBased);
            }, 'SpatialQueriesForm/AreaOfSight.handleRectangleAreaOfSight => IMcSpatialQueries.GetRectangleAreaOfSight', true, (error) => { errorMessage = String(error) })
        }, 'SpatialQueriesForm/AreaOfSight.handleRectangleAreaOfSight')
        return errorMessage
    }
    const handlePolygonAreaOfSight = (ppAreaOfSight, paLinesOfSight, pSeenPolygons, pUnseenPolygons, paSeenStaticObjects, queryParams) => {
        let errorMessage = '';
        runCodeSafely(() => {
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            runMapCoreSafely(() => {
                mcCurrentSpatialQueries.GetPolygonAreaOfSight(
                    tabProperties.scouter, tabProperties.isScouterHeightAbsolute, tabProperties.sightObjectPoints, tabProperties.targetHeight, tabProperties.isTargetHeightAbsolute, tabProperties.targetResolution,
                    tabProperties.rotationAzimuth, tabProperties.numberOfRays, [...tabProperties.visibilityColors.values(), new MapCore.SMcBColor(0, 0, 0, 0)],
                    ppAreaOfSight, paLinesOfSight, pSeenPolygons, pUnseenPolygons, paSeenStaticObjects,
                    tabProperties.maxPitchAngle, tabProperties.minPitchAngle, queryParams, tabProperties.isGPUBased);
            }, 'SpatialQueriesForm/AreaOfSight.handlePolygonAreaOfSight => IMcSpatialQueries.GetPolygonAreaOfSight', true, (error) => { errorMessage = String(error) })
        }, 'SpatialQueriesForm/AreaOfSight.handlePolygonAreaOfSight')
        return errorMessage
    }
    const handleEllipseAreaOfSight = (ppAreaOfSight, paLinesOfSight, pSeenPolygons, pUnseenPolygons, paSeenStaticObjects, queryParams) => {
        let errorMessage = '';
        runCodeSafely(() => {
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            runMapCoreSafely(() => {
                mcCurrentSpatialQueries.GetEllipseAreaOfSight(
                    tabProperties.scouter, tabProperties.isScouterHeightAbsolute, tabProperties.targetHeight, tabProperties.isTargetHeightAbsolute, tabProperties.targetResolution,
                    tabProperties.radiusX, tabProperties.radiusY, tabProperties.startAngle, tabProperties.endAngle,
                    tabProperties.rotationAzimuth, tabProperties.numberOfRays, [...tabProperties.visibilityColors.values(), new MapCore.SMcBColor(0, 0, 0, 0)],
                    ppAreaOfSight, paLinesOfSight, pSeenPolygons, pUnseenPolygons, paSeenStaticObjects,
                    tabProperties.maxPitchAngle, tabProperties.minPitchAngle, queryParams, tabProperties.isGPUBased);
            }, 'SpatialQueriesForm/AreaOfSight.handleEllipseAreaOfSight => IMcSpatialQueries.GetEllipseAreaOfSight', true, (error) => { errorMessage = String(error) })
        }, 'SpatialQueriesForm/AreaOfSight.handleEllipseAreaOfSight')
        return errorMessage
    }
    const handleCalcAndDrawAreaOfSightClick = () => {
        runCodeSafely(() => {
            if (tabProperties.scoutersAmount == 'multiple')
                handleEllipseAreaOfSightMultiple();
            else
                handleSingleAreaOfSight();
        }, 'SpatialQueriesForm/AreaOfSight.handleCalcAndDrawAreaOfSightClick');
    }
    //#endregion
    //#region Handle Functions
    const handleSelectedObjectTypeChange = (e) => {
        runCodeSafely(() => {
            setPropertiesCallback('selectedObjectType', e.value);
            setPropertiesCallback('sightObjectPoints', []);
            if (e.value != ObjectTypeEnum.ellipse && tabProperties.scoutersAmount == 'multiple') {
                setPropertiesCallback('scoutersAmount', 'single');
            }
            setPropertiesCallback('nameCalc', e.value + tabProperties.nameCalc.substring(tabProperties.nameCalc.indexOf(' ')))
        }, 'SpatialQueriesForm/AreaOfSight.handleSelectedObjectTypeChange');
    }
    const handleDrawObjectButtonClick = () => {
        runCodeSafely(() => {
            let isExistObjectMatchSelectedObjType = isExistObjectMatchSelectedObjectType();
            if (!isExistObjectMatchSelectedObjType) {
                let calcNameArr = tabProperties.nameCalc.split(' ');
                calcNameArr[1] = tabProperties.objSerialNum;
                setPropertiesCallback('nameCalc', calcNameArr.join(' '))
                setPropertiesCallback('objSerialNum', tabProperties.objSerialNum + 1)

                spatialQueriesService.removeExistObjects();
            }
        }, 'SpatialQueriesForm/AreaOfSight.handleDrawObjectButtonClick');
    }
    const handleFileSelected = (virtualFSPath: string, selectedOption: UploadTypeEnum) => {
        runCodeSafely(() => {
            let uint8Array = MapCore.IMcMapDevice.GetFileSystemFileContents(virtualFSPath);
            const decoder = new TextDecoder('utf-8');
            const jsonString = decoder.decode(uint8Array as Uint8Array);
            const jsonData = JSON.parse(jsonString);
            objectWorldTreeService.deleteSystemDirectories([virtualFSPath.split('/')[0]])
            setPropertiesCallback('sightObjectPoints', jsonData.LocationPoints);
            if (jsonData.ObjectTypeName != ObjectTypeEnum.ellipse && tabProperties.scoutersAmount == 'multiple') {
                setPropertiesCallback('scoutersAmount', 'single');
            }
            //Set New Object Attributes
            setSightObjectAttributes(jsonData);
            //Update Drawed Object
            if (spatialQueriesResultsObjects.queryName == SpatialQueryName.DrawAreaOfSightObject && tabProperties.activeOverlay) {
                let updatedScheme: MapCore.IMcObjectScheme = null;
                let attributesObj: any = getSchemeItemAttributesByType(jsonData.ObjectTypeName, jsonData);
                let schemeItem = createSchemeItemByType(jsonData.ObjectTypeName, attributesObj)
                let overlayManager = tabProperties.activeOverlay.GetOverlayManager();
                runMapCoreSafely(() => {
                    updatedScheme = MapCore.IMcObjectScheme.Create(overlayManager, schemeItem, MapCore.EMcPointCoordSystem.EPCS_WORLD)
                }, 'SpatialQueriesForm/AreaOfSight.handleFileSelected => IMcObjectScheme.Create', true);
                runMapCoreSafely(() => { spatialQueriesResultsObjects.objects[0].SetScheme(updatedScheme, true); }, 'SpatialQueriesForm/AreaOfSight.handleFileSelected => IMcObject.SetScheme', true);
                runMapCoreSafely(() => { spatialQueriesResultsObjects.objects[0].SetLocationPoints(jsonData.LocationPoints); }, 'SpatialQueriesForm/AreaOfSight.handleFileSelected => IMcObject.SetScheme', true);
            }
        }, "SpatialQueriesForm/AreaOfSight.handleFileSelected")
    }
    const handleExportSightObjectClick = () => {
        runCodeSafely(() => {
            let types = [ObjectTypeEnum.ellipse, ObjectTypeEnum.polygon, ObjectTypeEnum.rectangle]
            let attributesObj: any = getSchemeItemAttributesByType(tabProperties.selectedObjectType);
            let objToExport: { [key: string]: string | number } = {
                ObjectType: types.findIndex(val => val == tabProperties.selectedObjectType),
                ObjectTypeName: tabProperties.selectedObjectType,
                LocationPoints: tabProperties.sightObjectPoints,
                ...attributesObj
            }
            const jsonData = JSON.stringify(objToExport);
            const encoder = new TextEncoder();
            const uint8Array = encoder.encode(jsonData);
            MapCore.IMcMapDevice.DownloadBufferAsFile(uint8Array, `${tabProperties.selectedObjectType}.json`);
        }, "SpatialQueriesForm/AreaOfSight.handleExportSightObjectClick")
    }
    const handleScoutersAmountChange = (e, scoutersAmount) => {
        runCodeSafely(() => {
            if ((tabProperties.selectedObjectType == ObjectTypeEnum.ellipse && scoutersAmount == 'multiple') || scoutersAmount == 'single') {
                setPropertiesCallback('scoutersAmount', e.value)
            }
            else {//multiple not in Ellipse
                setPropertiesCallback('confirmDialogMessage', 'Multiple Scouters Available In Ellipse Object Only');
                setPropertiesCallback('confirmDialogVisible', true);
            }
        }, "SpatialQueriesForm/AreaOfSight.handleScoutersAmountChange")
    }
    const handleVisibilityColorChange = (e) => {
        runCodeSafely(() => {
            saveData(e);
            let newMap = new Map([...tabProperties.visibilityColors]);
            newMap.set(tabProperties.selectedColorVisibility.theEnum, e.target.value)
            setPropertiesCallback('visibilityColors', newMap);
        }, "SpatialQueriesForm/AreaOfSight.handleVisibilityColorChange")
    }
    const handleColorDropDownChange = (e) => {
        runCodeSafely(() => {
            saveData(e);
            let currentVis = e.target.value.theEnum;
            let currentColor = tabProperties.visibilityColors.get(currentVis)
            setPropertiesCallback('color', currentColor);
        }, "SpatialQueriesForm/AreaOfSight.handleColorDropDownChange")
    }
    const handleOptionResultsChange = (e) => {
        runCodeSafely(() => {
            let updatedExistOptionsSelection = _.cloneDeep(tabProperties.existOptionsSelection);
            let currOption = updatedExistOptionsSelection.find(option => option.option == e.target.name);
            currOption.isSelected = e.checked;
            updatedExistOptionsSelection = [...updatedExistOptionsSelection.filter(option => option.option !== e.target.name), currOption]
            setPropertiesCallback('existOptionsSelection', updatedExistOptionsSelection);
            if (spatialQueriesResultsObjects.queryName == SpatialQueryName.GetAreaOfSight || spatialQueriesResultsObjects.queryName == SpatialQueryName.GetAreaOfSightForMultipleScouters) {
                let currentOptionResult: AreaOfSightOptionResultObjects = spatialQueriesResultsObjects.objects.find((AreaOfSightOptionResultObjects: AreaOfSightOptionResultObjects) => AreaOfSightOptionResultObjects.optionName == e.target.name);
                currentOptionResult?.setVisibilityFunction(currentOptionResult.resultObject, e.checked)
            }
        }, "SpatialQueriesForm/AreaOfSight.handleOptionResultsChange")
    }
    //#endregion
    //#region DOM Functions
    const getSightObjectLegend = (sightObjectType: ObjectTypeEnum) => {
        return <span className="form__flex-and-row form__items-center">
            <RadioButton style={{ pointerEvents: 'auto' }} value={sightObjectType} onChange={handleSelectedObjectTypeChange} checked={tabProperties.selectedObjectType == sightObjectType} />
            <span>{sightObjectType}</span>
        </span>
    }
    const getMultipleScoutersLegend = (scoutersAmount: string) => {
        return <span className="form__flex-and-row form__items-center">
            <RadioButton style={{ pointerEvents: 'auto' }} value={scoutersAmount} onChange={e => handleScoutersAmountChange(e, scoutersAmount)} checked={tabProperties.scoutersAmount == scoutersAmount} />
            {scoutersAmount == 'multiple' ? <span>Multiple Scouters (Only For Ellipse Object)</span> : <span>Single Scouter</span>}
        </span>
    }
    const getSightObjectFieldset = () => {
        return <Fieldset legend='Sight Object'>
            <div style={{ width: '50%' }} className="form__column-container">
                <Fieldset className={`${tabProperties.selectedObjectType == ObjectTypeEnum.ellipse ? '' : 'form__disabled'} form__column-fieldset`} legend={getSightObjectLegend(ObjectTypeEnum.ellipse)}>
                    <div className="form__flex-and-row-between form__items-center">
                        <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                            <label>Radius X: </label>
                            <InputNumber className="form__narrow-input" name='radiusX' value={tabProperties.radiusX} onValueChange={saveData} />
                        </div>
                        <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                            <label>Radius Y: </label>
                            <InputNumber className="form__narrow-input" name='radiusY' value={tabProperties.radiusY} onValueChange={saveData} />
                        </div>
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                            <label>Start Angle: </label>
                            <InputNumber className="form__narrow-input" name='startAngle' value={tabProperties.startAngle} onValueChange={saveData} />
                        </div>
                        <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                            <label>End Angle: </label>
                            <InputNumber className="form__narrow-input" name='endAngle' value={tabProperties.endAngle} onValueChange={saveData} />
                        </div>
                    </div>
                </Fieldset>
                <Fieldset className={`${tabProperties.selectedObjectType == ObjectTypeEnum.rectangle ? '' : 'form__disabled'} form__row-fieldset form__space-between`} legend={getSightObjectLegend(ObjectTypeEnum.rectangle)}>
                    <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                        <label>Height: </label>
                        <InputNumber className="form__narrow-input" name='height' value={tabProperties.height} onValueChange={saveData} />
                    </div>
                    <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                        <label>Width: </label>
                        <InputNumber className="form__narrow-input" name='width' value={tabProperties.width} onValueChange={saveData} />
                    </div>
                </Fieldset>
                <span className={tabProperties.selectedObjectType == ObjectTypeEnum.polygon ? '' : 'form__disabled'} style={{ paddingLeft: `${globalSizeFactor * 1.5}vh` }}>
                    {getSightObjectLegend(ObjectTypeEnum.polygon)}
                </span>
            </div>
            <div style={{ width: '50%' }} className="form__column-container form__justify-center form__items-center">
                <DrawObjectCtrl disabled={tabProperties.activeOverlay ? false : true}
                    activeViewport={tabProperties.currentViewportData}
                    getObject={getObject}
                    objectType={tabProperties.selectedObjectType}
                    //Optional
                    schemeItemAttributes={getSchemeItemAttributesByType(tabProperties.selectedObjectType)}
                    pointsTableVisible={true}
                    getObjectPoints={getObjectPoints}
                    existObjectPoints={tabProperties.sightObjectPoints}
                    existObject={getExistObject()}
                    handleDrawObjectButtonClick={handleDrawObjectButtonClick}
                    ctrlHeight={10}
                    className="form__column-container form__justify-center form__items-center"
                />
                <div className="form__row-container">
                    <UploadFilesCtrl accept=".json" isDirectoryUpload={false} uploadOptions={[UploadTypeEnum.upload]} getVirtualFSPath={handleFileSelected} buttonOnly={true} label="Import Sight Object" />
                    <Button label="Export Sight Object" onClick={handleExportSightObjectClick} />
                </div>
            </div>
        </Fieldset>
    }
    const getSingleScouterFieldset = () => {
        return <Fieldset style={{ width: '49%' }} legend={getMultipleScoutersLegend('single')} className={`${tabProperties.scoutersAmount == 'multiple' ? 'form__disabled' : ''} form__column-fieldset-no-gap`}>
            <Fieldset legend='Define Colors' className="form__column-fieldset">
                <Dropdown style={{ width: '40%' }} name="selectedColorVisibility" value={tabProperties.selectedColorVisibility} onChange={handleColorDropDownChange} options={tabProperties.colorsVisibilityList} optionLabel="name" />
                <div className='object-props__flex-and-row-between'>
                    <label>Color:</label>
                    <ColorPickerCtrl style={{ width: '40%' }} name='color' value={tabProperties.color} onChange={handleVisibilityColorChange} alpha={true} />
                </div>
            </Fieldset>
            <Fieldset legend='Calculation Options' className="form__column-fieldset">
                {Object.entries(AreaOfSightOptionsEnum).map(([key, value]) => {
                    let isExist = tabProperties.calculationOptions.includes(value);
                    return (
                        value !== AreaOfSightOptionsEnum.isDrawObject && <div key={value} className="form__flex-and-row form__items-center">
                            <Checkbox id={value} name={value} checked={isExist} onChange={e => saveAreaOfSightOptions(e, 'calculationOptions')} />
                            <label style={{ paddingLeft: '0.5%' }} htmlFor={value}>{value}</label>
                        </div>
                    )
                })}
            </Fieldset>
        </Fieldset>
    }
    const getMultipleScouterFieldset = () => {
        return <Fieldset style={{ width: '49%' }} legend={getMultipleScoutersLegend('multiple')} className={`${tabProperties.scoutersAmount == 'multiple' ? '' : 'form__disabled'} form__column-fieldset-no-gap`}>
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                    <label>Radius X: </label>
                    <InputNumber className="form__medium-width-input" name='scoutersRadiusX' value={tabProperties.scoutersRadiusX} onValueChange={saveData} />
                </div>
                <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                    <label>Radius Y: </label>
                    <InputNumber className="form__medium-width-input" name='scoutersRadiusY' value={tabProperties.scoutersRadiusY} onValueChange={saveData} />
                </div>
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                    <label>Max Num Of Scouters: </label>
                    <InputNumber className="form__medium-width-input" name='maxNumOfScouters' value={tabProperties.maxNumOfScouters} onValueChange={saveData} />
                </div>
                <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                    <span>Scouters Sum Type:</span>
                    <Dropdown style={{ width: '55%' }} name="selectedScoutersSumType" value={tabProperties.selectedScoutersSumType} onChange={saveData} options={tabProperties.scoutersSumTypeList} optionLabel="name" />
                </div>
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <Button style={{ width: '60%' }} label="Get Best Scouters Location" onClick={handleGetBestScoutersClick} />
                <div style={{ width: '38%' }} className="form__flex-and-row form__items-center">
                    <Checkbox id="isBestScoutersAsync" name="isBestScoutersAsync" checked={tabProperties.isBestScoutersAsync} onChange={saveData} />
                    <label htmlFor="isBestScoutersAsync">Async</label>
                </div>
            </div>
            <div style={{ height: `${globalSizeFactor * 14}vh` }} className="form__column-container">
                <span style={{ textDecoration: 'underline' }}>Scouters Points: </span>
                <Vector3DGrid disabledPointFromMap={tabProperties.activeOverlay ? false : true} style={{ pointerEvents: 'auto' }} disabled={tabProperties.scoutersAmount !== 'multiple'} ctrlHeight={11} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initLocationPointsList={tabProperties.scoutersPoints} sendPointList={(...args) => savePointsTable(...args, 'scoutersPoints')} />
            </div>
        </Fieldset>
    }
    const getGeneralParamsFieldset = () => {
        return <Fieldset legend='General Params' className="form__column-fieldset-no-gap">
            <div className="form__flex-and-row-between">
                {getMultipleScouterFieldset()}
                {getSingleScouterFieldset()}
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '49%' }} className="form__column-container">
                    <div className="form__flex-and-row-between form__items-center form__disables">
                        <span>Scouter:</span>
                        <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.scouter} saveTheValue={(...args) => { save3DVector(...args, 'scouter') }} lastPoint={true} />
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                            <label>Min Pitch Angle: </label>
                            <InputNumber className="form__medium-width-input" name='minPitchAngle' value={tabProperties.minPitchAngle} onValueChange={saveData} />
                        </div>
                        <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                            <label>Max Pitch Angle: </label>
                            <InputNumber className="form__medium-width-input" name='maxPitchAngle' value={tabProperties.maxPitchAngle} onValueChange={saveData} />
                        </div>
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                            <label>Target Height: </label>
                            <InputNumber maxFractionDigits={5} className="form__medium-width-input" name='targetHeight' value={tabProperties.targetHeight} onValueChange={saveData} />
                        </div>
                        <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                            <label>Target Resolution: </label>
                            <InputNumber className="form__medium-width-input" name='targetResolution' value={tabProperties.targetResolution} onValueChange={saveData} />
                        </div>
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <div style={{ width: '49%' }} className="form__flex-and-row form__items-center">
                            <Checkbox id="isTargetHeightAbsolute" name="isTargetHeightAbsolute" checked={tabProperties.isTargetHeightAbsolute} onChange={saveData} />
                            <label htmlFor="isTargetHeightAbsolute">Is Target Height Absolute</label>
                        </div>
                        <div style={{ width: '49%' }} className="form__flex-and-row form__items-center">
                            <Checkbox id="isScouterHeightAbsolute" name="isScouterHeightAbsolute" checked={tabProperties.isScouterHeightAbsolute} onChange={saveData} />
                            <label htmlFor="isScouterHeightAbsolute">Is Scouter Height Absolute</label>
                        </div>
                    </div>
                </div>
                <div style={{ width: '49%' }} className="form__column-container">
                    <br />
                    <br />
                    <div className="form__flex-and-row-between form__items-center">
                        <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                            <label>Rotation Azimuth Degree: </label>
                            <InputNumber className="form__medium-width-input" name='rotationAzimuth' value={tabProperties.rotationAzimuth} onValueChange={saveData} />
                        </div>
                        <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                            <label>Number Of Rays: </label>
                            <InputNumber className="form__medium-width-input" name='numberOfRays' value={tabProperties.numberOfRays} onValueChange={saveData} />
                        </div>
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                            <label>Num Calling Function: </label>
                            <InputNumber maxFractionDigits={5} className="form__medium-width-input" name='numCallingFunction' value={tabProperties.numCallingFunction} onValueChange={saveData} />
                        </div>
                        <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                            <label>Name Calc: </label>
                            <InputText name='nameCalc' value={tabProperties.nameCalc} onChange={saveData} />
                        </div>
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <div style={{ width: '49%' }} className="form__flex-and-row form__items-center">
                            <Checkbox id="isGPUBased" name="isGPUBased" checked={tabProperties.isGPUBased} onChange={saveData} />
                            <label htmlFor="isGPUBased">Is GPU Based</label>
                        </div>
                        <div style={{ width: '49%' }} className="form__flex-and-row form__items-center">
                            <Checkbox id="isAsync" name="isAsync" checked={tabProperties.isAsync} onChange={saveData} />
                            <label htmlFor="isAsync">Async</label>
                        </div>
                    </div>
                </div>
            </div>
        </Fieldset>
    }
    const getResultsFieldset = () => {
        return <Fieldset className={spatialQueriesResultsObjects.queryName == SpatialQueryName.GetBestScoutersLocationsInEllipse ? 'form__disabled' : ''} legend='Results'>
            <div style={{ width: '20%' }} className="form___column-container">
                <Fieldset legend='Show Options' className="form__column-fieldset">
                    {Object.entries(AreaOfSightOptionsEnum).map(([key, value]) => {
                        let optionResult: { option: AreaOfSightOptionsEnum, isSelected: boolean } = tabProperties.existOptionsSelection.find((optionSelection: { option: AreaOfSightOptionsEnum, isSelected: boolean }) => optionSelection.option == value);
                        return optionResult ?
                            <div key={key} className="form__flex-and-row form__items-center">
                                <Checkbox id={optionResult.option} name={optionResult.option} checked={optionResult.isSelected} onChange={handleOptionResultsChange} />
                                <label style={{ paddingLeft: '0.5%' }} htmlFor={optionResult.option}>{key}</label>
                            </div> :
                            <div key={key} className="form__flex-and-row form__items-center form__disabled">
                                <Checkbox checked={false} />
                                <label style={{ paddingLeft: '0.5%' }}>{value}</label>
                            </div>
                    })}
                </Fieldset>
            </div>
            <span style={{ width: '80%' }}>
                <NestedTabsCtrl tabTypes={[
                    { index: 0, header: 'AOS Operation', propertiesClass: AOSOperationProperties, component: AOSOperation },
                    { index: 1, header: 'Point Visibility Color', propertiesClass: PointVisibilityColorProperties, component: PointVisibilityColor },
                    { index: 2, header: 'Coverage Quality', propertiesClass: CoverageQualityProperties, component: CoverageQuality },
                    { index: 3, header: 'Get Point Visibility Colors Surrounding', propertiesClass: ColorsSurroundingProperties, component: ColorsSurrounding },
                    { index: 4, header: 'Matrix Opertaions', propertiesClass: MatrixOperationProperties, component: MatrixOperation },
                ]} nestedTabName="areaOfSight" lastTabInfo={props.tabInfo} />
            </span>
        </Fieldset>
    }
    //#endregion
    return (
        <div className="form__column-container">
            {getSightObjectFieldset()}
            {getGeneralParamsFieldset()}
            <Button label="Calc And Draw Area Of Sight" onClick={handleCalcAndDrawAreaOfSightClick} />
            {getResultsFieldset()}

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
