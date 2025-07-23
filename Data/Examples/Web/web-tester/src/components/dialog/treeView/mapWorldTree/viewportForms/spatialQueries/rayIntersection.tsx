import { useEffect } from "react";
import { Button } from "primereact/button";
import { useDispatch, useSelector } from "react-redux";
import { Fieldset } from "primereact/fieldset";
import { RadioButton } from "primereact/radiobutton";
import { InputNumber } from "primereact/inputnumber";
import { Checkbox } from "primereact/checkbox";
import { Paginator, PaginatorPageChangeEvent } from "primereact/paginator";
import { InputText } from "primereact/inputtext";

import { getEnumDetailsList, getEnumValueDetails, MapCoreData, ObjectWorldService, ScanService } from 'mapcore-lib';
import { ParamsProperties } from "./params";
import { QueryResult, SpatialQueryName } from "./spatialQueriesFooter";
import Vector3DFromMap from "../../../objectWorldTree/shared/Vector3DFromMap";
import { Properties } from "../../../../dialog";
import { TabInfo } from "../../../../shared/tabCtrls/tabModels";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../../redux/combineReducer";
import { addQueryResultsTableRow } from "../../../../../../redux/MapWorldTree/mapWorldTreeActions";
import spatialQueriesService from "../../../../../../services/spatialQueries.service";
import mapWorldTreeService from "../../../../../../services/mapWorldTreeService";

type TargetResult = {
    targetType: string,
    intersectionPoint: MapCore.SMcPoint,
    coordinateSystem: string,
    terrainHashCode: string,
    layerHashCode: string,
    targetIndex: string,
    objectHashCode: string,
    itemHashCode: string,
    itemPart: string,
    partIndex: string,
}
export enum RaysLocationTypeEnum {
    destination = 1,
    direction,
}
export class RayIntersectionProperties implements Properties {
    mcCurrentSpatialQueries: MapCore.IMcSpatialQueries;
    activeOverlay: MapCore.IMcOverlay;
    rayOrigin: MapCore.SMcVector3D;
    rayLocationSelectedOption: RaysLocationTypeEnum;
    rayDestination: MapCore.SMcVector3D;
    rayDirection: MapCore.SMcVector3D;
    maxDistance: number;
    isAsync: boolean;
    // results
    isIntersectionFound: boolean;
    intersection: MapCore.SMcVector3D;
    normal: MapCore.SMcVector3D;
    distance: number;
    //target result
    intersectionsArr: TargetResult[];
    currentIntersectionInd: number;

    static getDefault(p: any): RayIntersectionProperties {
        let { mcCurrentSpatialQueries } = p;

        let defaults: RayIntersectionProperties = {
            mcCurrentSpatialQueries: mcCurrentSpatialQueries,
            activeOverlay: null,
            rayOrigin: MapCore.v3Zero,
            rayLocationSelectedOption: RaysLocationTypeEnum.destination,
            rayDestination: MapCore.v3Zero,
            rayDirection: MapCore.v3Zero,
            maxDistance: 10000,
            isAsync: true,
            // results
            isIntersectionFound: false,
            intersection: MapCore.v3Zero,
            normal: MapCore.v3Zero,
            distance: 0,
            //target result
            intersectionsArr: [],
            currentIntersectionInd: 0,
        }

        return defaults;
    }
};

export default function RayIntersection(props: { tabInfo: TabInfo }) {
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
        }, 'SpatialQueriesForm/RayIntersection.useEffect');
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
        }, 'SpatialQueriesForm/RayIntersection.getActiveOverlay');
        return activeOverlay;
    }
    const save3DVector = (...args) => {
        runCodeSafely(() => {
            const [point, flagNull, sectionPointType] = args;
            setPropertiesCallback(sectionPointType, point);
            if (sectionPointType == 'rayDestination') {
                let vector: MapCore.SMcVector3D = MapCore.SMcVector3D.Minus(tabProperties.rayDestination, tabProperties.rayOrigin);
                let rayDirection = MapCore.SMcVector3D.GetNormalized(vector);
                setPropertiesCallback('rayDirection', rayDirection);
            }
        }, 'SpatialQueriesForm/RayIntersection.save3DVector');
    }
    const getTargetResultValueByField = (field: string) => {
        let targetRes = tabProperties.intersectionsArr[tabProperties.currentIntersectionInd];
        if (targetRes) {
            return targetRes[field];
        }
        return '';
    }
    //#region Set Results
    const setRayIntersectionQueryResults = (rayOrigin: MapCore.SMcVector3D, rayDirection: MapCore.SMcVector3D, maxDistance: number,
        isIntersectionFound: boolean, pIntersection: MapCore.SMcVector3D, pNormal: MapCore.SMcVector3D, pDistance: number,
        isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            setPropertiesCallback('rayLocationSelectedOption', RaysLocationTypeEnum.direction)
            setPropertiesCallback('rayOrigin', rayOrigin)
            setPropertiesCallback('rayDirection', rayDirection)
            setPropertiesCallback('maxDistance', maxDistance)
            setPropertiesCallback('isIntersectionFound', isIntersectionFound)
            setPropertiesCallback('intersection', isIntersectionFound ? pIntersection : null)
            setPropertiesCallback('normal', pNormal)
            setPropertiesCallback('distance', pDistance)
            setPropertiesCallback('isAsync', isAsync)

            spatialQueriesService.manageQueryResultObjects(tabProperties.mcCurrentSpatialQueries, SpatialQueryName.GetRayIntersection, rayOrigin, rayDirection, maxDistance);
        }, 'SpatialQueriesForm/RayIntersection.setRayIntersectionQueryResults')
        if (!isFromTable) {
            let args = [rayOrigin, rayDirection, maxDistance, isIntersectionFound, pIntersection, pNormal, pDistance, isAsync, true, errorMessage];
            let queryResult = new QueryResult(SpatialQueryName.GetRayIntersection,
                setRayIntersectionQueryResults, args, tabProperties.isAsync, errorMessage);
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    const setRayIntersectionTargetsQueryResults = (rayOrigin: MapCore.SMcVector3D, rayDirection: MapCore.SMcVector3D,
        maxDistance: number, aIntersections: MapCore.IMcSpatialQueries.STargetFound[],
        isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            setPropertiesCallback('rayLocationSelectedOption', RaysLocationTypeEnum.direction)
            setPropertiesCallback('rayOrigin', rayOrigin)
            setPropertiesCallback('rayDirection', rayDirection)
            setPropertiesCallback('maxDistance', maxDistance)
            setPropertiesCallback('isAsync', isAsync)
            let intersectionsArr: TargetResult[] = [];
            aIntersections.forEach(intersectionRes => {
                let targetTypesEnum = getEnumDetailsList(MapCore.IMcSpatialQueries.EIntersectionTargetType);
                let strTargetType = getEnumValueDetails(intersectionRes.eTargetType, targetTypesEnum).name;
                let coordSysEnum = getEnumDetailsList(MapCore.EMcPointCoordSystem);
                let strCoordSys = getEnumValueDetails(intersectionRes.eIntersectionCoordSystem, coordSysEnum).name;
                let tarrainNode = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, intersectionRes.pTerrain);
                let layerNode = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, intersectionRes.pTerrainLayer);
                let targetInd = ScanService.GetTargetIdByBitCount(intersectionRes);
                let objectNode = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, intersectionRes.ObjectItemData?.pObject);
                let itemNode = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, intersectionRes.ObjectItemData?.pItem);
                let itemPartEnum = getEnumDetailsList(MapCore.IMcSpatialQueries.EItemPart);
                let itemPart = getEnumValueDetails(intersectionRes.ObjectItemData?.ePartFound, itemPartEnum)?.name;

                let resObj: TargetResult = {
                    targetType: strTargetType,
                    intersectionPoint: intersectionRes.IntersectionPoint,
                    coordinateSystem: strCoordSys,
                    terrainHashCode: tarrainNode?.label || 'Null',
                    layerHashCode: layerNode?.label || 'Null',
                    targetIndex: `${targetInd}`,
                    objectHashCode: objectNode?.label || 'Null',
                    itemHashCode: itemNode?.label || 'Null',
                    itemPart: itemPart || '',
                    partIndex: `${intersectionRes.ObjectItemData?.uPartIndex}` || '0',
                };
                intersectionsArr = [...intersectionsArr, resObj];
            })
            setPropertiesCallback('intersectionsArr', intersectionsArr)

            spatialQueriesService.manageQueryResultObjects(tabProperties.mcCurrentSpatialQueries, SpatialQueryName.GetRayIntersectionTargets, rayOrigin, rayDirection, maxDistance)
        }, 'SpatialQueriesForm/RayIntersection.setRayIntersectionTargetsQueryResults')
        if (!isFromTable) {
            let args = [rayOrigin, rayDirection, maxDistance, aIntersections, isAsync, true, errorMessage];
            let queryResult = new QueryResult(SpatialQueryName.GetRayIntersectionTargets,
                setRayIntersectionTargetsQueryResults, args, tabProperties.isAsync, errorMessage);
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    //#endregion
    //#region Handle Query Error
    const handleAsyncRayIntersectionQueryError = (eErrorCode: MapCore.IMcErrors.ECode, rayOrigin: MapCore.SMcVector3D, rayDirection: MapCore.SMcVector3D, maxDistance: number) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/RayIntersection.handleAsyncRayIntersectionQueryError => IMcErrors.ErrorCodeToString', true)
            setRayIntersectionQueryResults(rayOrigin, rayDirection, maxDistance, false, MapCore.v3Zero, MapCore.v3Zero,
                0, true, false, errorMessage);
        }, 'SpatialQueriesForm/RayIntersection.handleAsyncRayIntersectionQueryError')
    }
    const handleAsyncRayIntersectionTargetsQueryError = (eErrorCode: MapCore.IMcErrors.ECode, rayOrigin: MapCore.SMcVector3D, rayDirection: MapCore.SMcVector3D, maxDistance: number) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/RayIntersection.handleAsyncRayIntersectionTargetsQueryError => IMcErrors.ErrorCodeToString', true)
            setRayIntersectionTargetsQueryResults(rayOrigin, rayDirection, maxDistance, [], true, false, errorMessage);
        }, 'SpatialQueriesForm/RayIntersection.handleAsyncRayIntersectionTargetsQueryError')
    }
    //#endregion
    //#region Handle Functoins
    const handleGetRayIntersectionClick = () => {
        let isIntersectionFound = false;
        let pDistance: { Value?: number } = {};
        let pNormal: { Value?: MapCore.SMcVector3D } = {};
        let pIntersection: { Value?: MapCore.SMcVector3D } = {};
        let errorMessage = '';
        runCodeSafely(() => {
            let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
            if (tabProperties.isAsync) {
                queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((bIntersectionFound: boolean, pIntersection: MapCore.SMcVector3D, pNormal: MapCore.SMcVector3D, pdDistance: number) => {
                    setRayIntersectionQueryResults(tabProperties.rayOrigin, tabProperties.rayDirection, tabProperties.maxDistance,
                        bIntersectionFound, pIntersection, pNormal, pdDistance, tabProperties.isAsync, false, errorMessage);
                }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                    handleAsyncRayIntersectionQueryError(eErrorCode, tabProperties.rayOrigin, tabProperties.rayDirection, tabProperties.maxDistance)
                }, 'SpatialQueriesForm/RayIntersection.handleGetRayIntersectionClick => IMcSpatialQueries.GetRayIntersection');
            }
            else {
                queryParams.pAsyncQueryCallback = null;
            }
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            runMapCoreSafely(() => {
                isIntersectionFound = mcCurrentSpatialQueries.GetRayIntersection(tabProperties.rayOrigin,
                    tabProperties.rayDirection, tabProperties.maxDistance, pIntersection, pNormal, pDistance, queryParams);
            }, 'SpatialQueriesForm/RayIntersection.handleGetRayIntersectionClick => IMcSpatialQueries.GetRayIntersection', true, (error) => { errorMessage = String(error) })
        }, 'SpatialQueriesForm/RayIntersection.handleGetRayIntersectionClick');
        if (!tabProperties.isAsync) {
            setRayIntersectionQueryResults(tabProperties.rayOrigin, tabProperties.rayDirection, tabProperties.maxDistance,
                isIntersectionFound, pIntersection.Value, pNormal.Value, pDistance.Value, tabProperties.isAsync, false, errorMessage);
        }
    }
    const handleGetRayIntersectionTargetsClick = () => {
        let aIntersections: MapCore.IMcSpatialQueries.STargetFound[];
        let errorMessage = '';
        runCodeSafely(() => {
            let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
            if (tabProperties.isAsync) {
                queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((aIntersections: MapCore.IMcSpatialQueries.STargetFound[]) => {
                    setRayIntersectionTargetsQueryResults(tabProperties.rayOrigin, tabProperties.rayDestination, tabProperties.maxDistance,
                        aIntersections, tabProperties.isAsync, false, errorMessage);
                }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                    handleAsyncRayIntersectionTargetsQueryError(eErrorCode, tabProperties.rayOrigin, tabProperties.rayDirection, tabProperties.maxDistance)
                }, 'SpatialQueriesForm/RayIntersection.handleGetRayIntersectionTargetsClick => IMcSpatialQueries.GetRayIntersectionTargets');
            }
            else {
                queryParams.pAsyncQueryCallback = null;
            }
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            runMapCoreSafely(() => {
                aIntersections = mcCurrentSpatialQueries.GetRayIntersectionTargets(tabProperties.rayOrigin,
                    tabProperties.rayDirection, tabProperties.maxDistance, queryParams);
            }, 'SpatialQueriesForm/RayIntersection.handleGetRayIntersectionTargetsClick => IMcSpatialQueries.GetRayIntersectionTargets', true, (error) => { errorMessage = String(error) })
        }, 'SpatialQueriesForm/RayIntersection.handleGetRayIntersectionTargetsClick');
        if (!tabProperties.isAsync) {
            setRayIntersectionTargetsQueryResults(tabProperties.rayOrigin, tabProperties.rayDirection, tabProperties.maxDistance,
                aIntersections, tabProperties.isAsync, false, errorMessage);
        }
    }
    const handleRayLocationSelectedOptionChange = (e) => {
        runCodeSafely(() => {
            setPropertiesCallback('rayLocationSelectedOption', e.value)
            setPropertiesCallback('rayDirection', MapCore.v3Zero)
            setPropertiesCallback('rayDestination', MapCore.v3Zero)
        }, 'SpatialQueriesForm/RayIntersection.handleRayLocationSelectedOptionChange');
    }
    //#endregion
    //#region DOM Functions
    const getDestDirPointsFieldset = () => {
        return <Fieldset className="form__column-fieldset">
            <div className="form__row-container form__justify-space-around">
                <div className="form__flex-and-row form__items-center">
                    <RadioButton value={RaysLocationTypeEnum.destination} onChange={handleRayLocationSelectedOptionChange} checked={tabProperties.rayLocationSelectedOption === RaysLocationTypeEnum.destination} />
                    <label className={tabProperties.rayLocationSelectedOption === RaysLocationTypeEnum.destination ? '' : 'form__disabled'}> Select Ray Destination</label>
                </div>
                <div className="form__flex-and-row form__items-center">
                    <RadioButton value={RaysLocationTypeEnum.direction} onChange={handleRayLocationSelectedOptionChange} checked={tabProperties.rayLocationSelectedOption === RaysLocationTypeEnum.direction} />
                    <label className={tabProperties.rayLocationSelectedOption === RaysLocationTypeEnum.direction ? '' : 'form__disabled'}> Select Ray Direction</label>
                </div>
            </div>
            <div className={`form__flex-and-row-between form__items-center ${tabProperties.rayLocationSelectedOption !== RaysLocationTypeEnum.destination && 'form__disabled'}`}>
                <span>Ray Destination:</span>
                <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.rayDestination} saveTheValue={(...args) => { save3DVector(...args, 'rayDestination') }} lastPoint={true} />
            </div>
            <div className={`form__flex-and-row-between form__items-center ${tabProperties.rayLocationSelectedOption !== RaysLocationTypeEnum.direction && 'form__disabled'}`}>
                <span>Ray Direction:</span>
                <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.rayDirection} saveTheValue={(...args) => { save3DVector(...args, 'rayDirection') }} lastPoint={true} />
            </div>
        </Fieldset>
    }
    const getSharedInputs = () => {
        return <div style={{ width: '55%' }} className="form__column-container">
            <div className="form__flex-and-row-between form__items-center">
                <span> Ray Origin:</span>
                <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.rayOrigin} saveTheValue={(...args) => { save3DVector(...args, 'rayOrigin') }} lastPoint={true} />
            </div>
            {getDestDirPointsFieldset()}
            <div className="form__flex-and-row-between form__items-center">
                <label>Max Distance: </label>
                <InputNumber name='maxDistance' value={tabProperties.maxDistance} onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row form__items-center">
                <Checkbox id="isAsync" name="isAsync" checked={tabProperties.isAsync} onChange={saveData} />
                <label htmlFor="isAsync">Async</label>
            </div>
        </div>
    }
    const getRayIntersectionTargetResultFieldset = () => {
        return <Fieldset className='form__column-fieldset' legend={<span style={{ opacity: '0.6' }}>Ray Intersection Target Result</span>}>
            <div className="form__column-container form__disabled">
                <div className="form__flex-and-row-between">
                    <label>Intersection Target Type:</label>
                    <InputText value={getTargetResultValueByField('targetType')} />
                </div>
                <div className="form__flex-and-row-between">
                    <label>Intersection Point:</label>
                    <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={getTargetResultValueByField('intersectionPoint')} saveTheValue={(...args) => { }} lastPoint={true} />
                </div>
                <div className="form__flex-and-row-between">
                    <label>Coordinate System: </label>
                    <InputText value={getTargetResultValueByField('coordinateSystem')} />
                </div>
                <div className="form__flex-and-row-between">
                    <label>Terrain Hash Code: </label>
                    <InputText value={getTargetResultValueByField('terrainHashCode')} />
                </div>
                <div className="form__flex-and-row-between">
                    <label>Layer Hash Code: </label>
                    <InputText value={getTargetResultValueByField('layerHashCode')} />
                </div>
                <div className="form__flex-and-row-between">
                    <label>Target Index: </label>
                    <InputText value={getTargetResultValueByField('targetIndex')} />
                </div>
                <span style={{ textDecoration: 'underline', padding: `${globalSizeFactor * 0.7}vh` }}>Object Item Found: </span>
                <div className="form__flex-and-row-between">
                    <label>Object Hash Code: </label>
                    <InputText value={getTargetResultValueByField('objectHashCode')} />
                </div>
                <div className="form__flex-and-row-between">
                    <label>Item Hash Code: </label>
                    <InputText value={getTargetResultValueByField('itemHashCode')} />
                </div>
                <div className="form__flex-and-row-between">
                    <label>Item Part: </label>
                    <InputText value={getTargetResultValueByField('itemPart')} />
                </div>
                <div className="form__flex-and-row-between">
                    <label>Part Index: </label>
                    <InputText value={getTargetResultValueByField('partIndex')} />
                </div>
            </div>
            <Paginator first={tabProperties.currentIntersectionInd} rows={1} totalRecords={tabProperties.intersectionsArr.length || 1} onPageChange={(e: PaginatorPageChangeEvent) => { setPropertiesCallback('currentIntersectionInd', e.first) }} template={{ layout: 'PrevPageLink CurrentPageReport NextPageLink' }} />
        </Fieldset>
    }
    const getRayIntersectionResultFieldset = () => {
        return <Fieldset className='form__column-fieldset form__disabled' legend='Ray Intersection Result'>
            <div className="form__flex-and-row-between form__items-center">
                <label>Normal:</label>
                <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.normal} saveTheValue={(...args) => { }} lastPoint={true} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <label>Intersection:</label>
                <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.intersection} saveTheValue={(...args) => { }} lastPoint={true} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '57%' }} className="form__flex-and-row-between form__items-center">
                    <span>Distance:</span>
                    <InputNumber value={tabProperties.distance} />
                </div>
                <div className="form__flex-and-row form__items-center">
                    <Checkbox checked={tabProperties.isIntersectionFound} />
                    <label>Is Intersection Found</label>
                </div>
            </div>
        </Fieldset>
    }
    //#endregion
    return (
        <div className="form__column-container">
            <div className="form__flex-and-row form__justify-center">{getSharedInputs()}</div>
            <div className="form__flex-and-row-between">
                <div className="form__column-container">
                    <Button label='Ray Intersection Targets' onClick={handleGetRayIntersectionTargetsClick} />
                    {getRayIntersectionTargetResultFieldset()}
                </div>
                <div className="form__column-container">
                    <Button label="Ray Intersection" onClick={handleGetRayIntersectionClick} />
                    {getRayIntersectionResultFieldset()}
                </div>
            </div>
        </div>
    )
}
