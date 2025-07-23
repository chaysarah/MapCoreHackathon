import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Fieldset } from "primereact/fieldset";
import { ListBox } from "primereact/listbox";
import { InputNumber } from "primereact/inputnumber";
import { Checkbox } from "primereact/checkbox";
import { Button } from "primereact/button";
import { ConfirmDialog } from "primereact/confirmdialog";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { InputText } from "primereact/inputtext";
import _ from 'lodash';

import { ParamsProperties } from "./params";
import Vector3DGrid from "../../../objectWorldTree/shared/vector3DGrid";
import Vector3DFromMap from "../../../objectWorldTree/shared/Vector3DFromMap";
import { Properties } from "../../../../dialog";
import DrawLineCtrl from "../../../../shared/drawLineCtrl";
import { TabInfo } from "../../../../shared/tabCtrls/tabModels";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../../redux/combineReducer";
import ColorPickerCtrl from "../../../../../shared/colorPicker";
import { MapCoreData, ObjectWorldService, getEnumValueDetails, getEnumDetailsList, ViewportData } from 'mapcore-lib';
import spatialQueriesService from "../../../../../../services/spatialQueries.service";
import { addQueryResultsTableRow, setSpatialQueriesResultsObjects } from "../../../../../../redux/MapWorldTree/mapWorldTreeActions";
import { QueryResult, SpatialQueryName } from "./spatialQueriesFooter";
import mapWorldTreeService from "../../../../../../services/mapWorldTreeService";
import { TreeNodeModel } from "../../../../../../services/tree.service";

export class RasterAndTraversabilityProperties implements Properties {
    mcCurrentSpatialQueries: MapCore.IMcSpatialQueries;
    activeOverlay: MapCore.IMcOverlay;
    currentViewportData: ViewportData;
    confirmDialogMessage: string;
    confirmDialogVisible: boolean;
    //Raster
    rasterLayersList: any[];
    selectedRasterLayer: any;
    point: MapCore.SMcVector3D;
    lod: number;
    isNearestPixel: boolean;
    isRasterAsync: boolean;
    rasterColor: MapCore.SMcBColor;
    traversabilityTable: { directionAngle: number, traversable: boolean }[];
    //traversability
    traversLayersList: any[];
    selectedTraversLayer: any;
    linePoints: MapCore.SMcVector3D[];
    isTraversAsync: boolean;
    traversabilityPointsTable: { x: number, y: boolean, z: number, traversability: string }[];

    static getDefault(p: any): RasterAndTraversabilityProperties {
        let { mcCurrentSpatialQueries } = p;

        return {
            mcCurrentSpatialQueries: mcCurrentSpatialQueries,
            activeOverlay: null,
            currentViewportData: null,
            confirmDialogMessage: '',
            confirmDialogVisible: false,
            //Raster
            rasterLayersList: [],
            selectedRasterLayer: null,
            point: MapCore.v3Zero,
            lod: 0,
            isNearestPixel: false,
            isRasterAsync: true,
            rasterColor: new MapCore.SMcBColor(0, 0, 0, 0),
            traversabilityTable: [],
            //traversability
            traversLayersList: [],
            selectedTraversLayer: null,
            linePoints: [],
            isTraversAsync: true,
            traversabilityPointsTable: [],
        }
    }
};

export default function RasterAndTraversability(props: { tabInfo: TabInfo }) {
    let { saveData, setApplyCallBack, setPropertiesCallback, tabProperties, getTabPropertiesByTabPropertiesClass } = props.tabInfo;
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const spatialQueriesResultsObjects = useSelector((state: AppState) => state.mapWorldTreeReducer.spatialQueriesResultsObjects)
    const mapWorldTree = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree)
    const activeCard: number = useSelector((state: AppState) => state.mapWindowReducer.activeCard);

    useEffect(() => {
        runCodeSafely(() => {
            let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            let terrains: MapCore.IMcMapTerrain[] = mcCurrentSpatialQueries.GetTerrains();
            let rasterLayersNodesArr = [];
            let traversLayersNodesArr = [];
            terrains.forEach((terrain) => {
                let layers = terrain.GetLayers();
                layers.forEach((layer, i) => {
                    if ([MapCore.IMcNativeRasterMapLayer.LAYER_TYPE, MapCore.IMcNativeServerRasterMapLayer.LAYER_TYPE,
                    MapCore.IMcRawRasterMapLayer.LAYER_TYPE, MapCore.IMcWebServiceRasterMapLayer.LAYER_TYPE, MapCore.IMcNativeTraversabilityMapLayer.LAYER_TYPE,
                    MapCore.IMcNativeServerTraversabilityMapLayer.LAYER_TYPE, MapCore.IMcRawTraversabilityMapLayer.LAYER_TYPE].includes(layer.GetLayerType())) {
                        let layerNode: any = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, layer);
                        layerNode = layerNode ? layerNode : { nodeMcContent: layer, label: `Raster Layer (${i + 1})` }
                        rasterLayersNodesArr = [...rasterLayersNodesArr, layerNode];
                    }
                    if ([MapCore.IMcNativeTraversabilityMapLayer.LAYER_TYPE, MapCore.IMcNativeServerTraversabilityMapLayer.LAYER_TYPE,
                    MapCore.IMcRawTraversabilityMapLayer.LAYER_TYPE].includes(layer.GetLayerType())) {
                        let layerNode: any = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, layer);
                        layerNode = layerNode ? layerNode : { nodeMcContent: layer, label: `Trvarsability Layer (${i + 1})` }
                        traversLayersNodesArr = [...traversLayersNodesArr, layerNode];
                    }
                });
            });
            setPropertiesCallback('rasterLayersList', rasterLayersNodesArr);
            setPropertiesCallback('traversLayersList', traversLayersNodesArr);
        }, 'SpatialQueriesForm/RasterAndTraversability.useEffect');
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            let activeOverlay = getActiveOverlay();
            setPropertiesCallback('activeOverlay', activeOverlay);
            let currentViewportData: ViewportData = getViewportData();
            setPropertiesCallback('currentViewportData', currentViewportData);
        }, 'SpatialQueriesForm/RasterAndTraversability.useEffect');
    }, [activeCard])
    //#region Save Functions
    const save3DVector = (...args) => {
        runCodeSafely(() => {
            const [point, flagNull, sectionPointType] = args;
            setPropertiesCallback(sectionPointType, point);
        }, 'SpatialQueriesForm/RasterAndTraversability.save3DVector');
    }
    const savePointsTable = (...args) => {
        runCodeSafely(() => {
            const [locationPointsList, valid, selectedPoint, pointsType] = args;
            if (!_.isEqual(tabProperties[pointsType], locationPointsList)) {
                setPropertiesCallback(pointsType, locationPointsList);
            }
        }, 'SpatialQueriesForm/RasterAndTraversability.savePointsTable');
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
        }, 'SpatialQueriesForm/RasterAndTraversability.getActiveOverlay');
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
        }, 'SpatialQueriesForm/RasterAndTraversability.getViewportData');
        return viewportData;
    }
    const getLineObject = (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) => {
        runCodeSafely(() => {
            if (nExitCode == 1) {
                let points: MapCore.SMcVector3D[] = pObject.GetLocationPoints(0);
                setPropertiesCallback('linePoints', points);
                spatialQueriesService.removeExistObjects();
                dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetTraversabilityAlongLine, objects: [pObject], removeObjectsCB: (objects: MapCore.IMcObject[]) => { objects[0].Remove() } }))
            }
        }, 'SpatialQueriesForm/RasterAndTraversability.getLineObject');
    }
    const getTraversabilityFromColorCode = (mapLayer: MapCore.IMcMapLayer, color: MapCore.SMcBColor) => {
        runCodeSafely(() => {
            if ([MapCore.IMcNativeTraversabilityMapLayer.LAYER_TYPE, MapCore.IMcNativeServerTraversabilityMapLayer.LAYER_TYPE,
            MapCore.IMcRawTraversabilityMapLayer.LAYER_TYPE].includes(mapLayer.GetLayerType())) {
                let traverabilityLayer = mapLayer as MapCore.IMcTraversabilityMapLayer;
                let traversabilityDirections: MapCore.IMcTraversabilityMapLayer.STraversabilityDirection[] = null;
                runMapCoreSafely(() => {
                    traversabilityDirections = traverabilityLayer.GetTraversabilityFromColorCode(color);
                }, 'SpatialQueriesForm/RasterAndTraversability.getTraversabilityFromColorCode => IMcTraversabilityMapLayer.GetTraversabilityFromColorCode', true)
                if (traversabilityDirections) {
                    let traversabilityTable = traversabilityDirections.map(direction => { return { directionAngle: direction.fDirectionAngle, traversable: direction.bTraversable } })
                    setPropertiesCallback('traversabilityTable', traversabilityTable);
                }
                else {
                    setPropertiesCallback('confirmDialogVisible', true);
                    setPropertiesCallback('confirmDialogMessage', 'Get Traversability From Color Code return null or color is null');
                }
            }
            else {
                setPropertiesCallback('confirmDialogVisible', true);
                setPropertiesCallback('confirmDialogMessage', 'The Map Layer Should Be Traversability Map Layer');
            }
        }, 'SpatialQueriesForm/RasterAndTraversability.getTraversabilityFromColorCode');
    }
    const getColumns = () => {
        let columns = [{ field: `x`, header: 'X' },
        { field: `y`, header: 'Y' },
        { field: `z`, header: 'Z' },
        { field: `traversability`, header: 'Traversability' }];
        return columns;
    }
    const getFieldTemplate = (rowData: any, column: any) => {
        return <InputText style={{ width: `${globalSizeFactor * (column.field == 'traversability' ? 10 : 23 / 3)}vh` }} type="text" value={rowData[column.field]} />;
    }
    //#endregion
    //#region Set Results
    const setRasterLayerColorQueryResults = (rasterLayer: TreeNodeModel, point: MapCore.SMcVector3D, lod: number, isNearestPixel: boolean, rasterColor: MapCore.SMcBColor,
        isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            setPropertiesCallback('selectedRasterLayer', rasterLayer);
            setPropertiesCallback('point', point)
            setPropertiesCallback('lod', lod)
            setPropertiesCallback('isNearestPixel', isNearestPixel)
            setPropertiesCallback('rasterColor', rasterColor);
            setPropertiesCallback('isRasterAsync', isAsync);
            if ([MapCore.IMcNativeTraversabilityMapLayer.LAYER_TYPE, MapCore.IMcNativeServerTraversabilityMapLayer.LAYER_TYPE,
            MapCore.IMcRawTraversabilityMapLayer.LAYER_TYPE].includes(rasterLayer.nodeMcContent.GetLayerType())) {
                getTraversabilityFromColorCode(rasterLayer.nodeMcContent, rasterColor);
            }
        }, 'SpatialQueriesForm/RasterAndTraversability.setRasterLayerColorQueryResults')

        if (!isFromTable) {
            let args = [rasterLayer, point, lod, isNearestPixel, rasterColor, isAsync, true, errorMessage]
            let queryResult = new QueryResult(SpatialQueryName.GetRasterLayerColorByPoint,
                setRasterLayerColorQueryResults, args, tabProperties.isRasterAsync, errorMessage)
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    const setTraversabilityAlongLineQueryResults = (traversabilityLayer: TreeNodeModel, linePoints: MapCore.SMcVector3D[], traversabilityPointsResult: MapCore.IMcSpatialQueries.STraversabilityPoint[],
        isAsync: boolean, isFromTable: boolean, errorMessage: string) => {
        runCodeSafely(() => {
            setPropertiesCallback('selectedTraversLayer', traversabilityLayer)
            setPropertiesCallback('linePoints', linePoints)
            let traversabilityTable = [];
            traversabilityPointsResult.forEach((traversabilityPoint) => {
                let traversEnumList = getEnumDetailsList(MapCore.IMcSpatialQueries.EPointTraversability);
                let traversabilityString = getEnumValueDetails(traversabilityPoint.eTraversability, traversEnumList)?.name;
                let traverseObj = { x: traversabilityPoint.Point.x, y: traversabilityPoint.Point.y, z: traversabilityPoint.Point.z, traversability: traversabilityString }
                traversabilityTable = [...traversabilityTable, traverseObj]
            })
            setPropertiesCallback('traversabilityPointsTable', traversabilityTable)

            spatialQueriesService.manageQueryResultObjects(tabProperties.mcCurrentSpatialQueries, SpatialQueryName.GetTraversabilityAlongLine, traversabilityPointsResult, linePoints)
        }, 'SpatialQueriesForm/RasterAndTraversability.setTraversabilityAlongLineQueryResults')
        if (!isFromTable) {
            let args = [traversabilityLayer, linePoints, traversabilityPointsResult, isAsync, true, errorMessage];
            let queryResult = new QueryResult(SpatialQueryName.GetTraversabilityAlongLine,
                setTraversabilityAlongLineQueryResults, args, tabProperties.isTraversAsync, errorMessage);
            dispatch(addQueryResultsTableRow(queryResult))
        }
    }
    //#endregion
    //#region Handle Query Error
    const handleAsyncRasterLayerColorQueryError = (eErrorCode: MapCore.IMcErrors.ECode, rasterLayer: TreeNodeModel, point: MapCore.SMcVector3D, lod: number, isNearestPixel: boolean) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/RasterAndTraversability.handleAsyncRasterLayerColorQueryError => IMcErrors.ErrorCodeToString', true)
            setRasterLayerColorQueryResults(rasterLayer, point, lod, isNearestPixel, new MapCore.SMcBColor(0, 0, 0, 0), true, false, errorMessage);
        }, 'SpatialQueriesForm/RasterAndTraversability.handleAsyncRasterLayerColorQueryError')
    }
    const handleAsyncTravesabilityAlongLineQueryError = (eErrorCode: MapCore.IMcErrors.ECode, traversabilityLayer: TreeNodeModel, linePoints: MapCore.SMcVector3D[]) => {
        runCodeSafely(() => {
            let errorMessage = '';
            runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(eErrorCode); }, 'SpatialQueriesForm/RasterAndTraversability.handleAsyncTravesabilityAlongLineQueryError => IMcErrors.ErrorCodeToString', true)
            setTraversabilityAlongLineQueryResults(traversabilityLayer, linePoints, [], true, false, errorMessage);
        }, 'SpatialQueriesForm/RasterAndTraversability.handleAsyncTravesabilityAlongLineQueryError')
    }
    //#endregion
    //#region Handle Functions
    const handleGetRasterLayerColorClick = () => {
        let resultColor: MapCore.SMcBColor;
        let errorMessage = '';
        if (!tabProperties.selectedRasterLayer) {
            setPropertiesCallback('confirmDialogVisible', true);
            setPropertiesCallback('confirmDialogMessage', 'Missing Raster Map Layer');
        }
        else {
            runCodeSafely(() => {
                let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
                if (tabProperties.isRasterAsync) {
                    queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((Color: MapCore.SMcBColor) => {
                        setRasterLayerColorQueryResults(tabProperties.selectedRasterLayer, tabProperties.point, tabProperties.lod, tabProperties.isNearestPixel, Color, tabProperties.isRasterAsync, false, '');
                    }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                        handleAsyncRasterLayerColorQueryError(eErrorCode, tabProperties.selectedRasterLayer, tabProperties.point, tabProperties.lod, tabProperties.isNearestPixel)
                    }, 'SpatialQueriesForm/RasterAndTraversability.handleGetRasterLayerColorClick => IMcSpatialQueries.GetRasterLayerColorByPoint');
                }
                else {
                    queryParams.pAsyncQueryCallback = null;
                }
                let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
                runMapCoreSafely(() => {
                    resultColor = mcCurrentSpatialQueries.GetRasterLayerColorByPoint(tabProperties.selectedRasterLayer.nodeMcContent, tabProperties.point, tabProperties.lod, tabProperties.isNearestPixel, queryParams)
                }, 'SpatialQueriesForm/RasterAndTraversability.handleGetRasterLayerColorClick => IMcSpatialQueries.GetRasterLayerColorByPoint', true, (error) => { errorMessage = String(error) })
            }, 'SpatialQueriesForm/RasterAndTraversability.handleGetRasterLayerColorClick');
            if (!tabProperties.isRasterAsync) {
                setRasterLayerColorQueryResults(tabProperties.selectedRasterLayer, tabProperties.point, tabProperties.lod, tabProperties.isNearestPixel, resultColor, tabProperties.isRasterAsync, false, errorMessage);
            }
        }
    }
    const handleGetTraversabilityAlongLineClick = () => {
        let traversabilityPoints: MapCore.IMcSpatialQueries.STraversabilityPoint[] = [];
        let convertedPoints = tabProperties.linePoints.map(point => spatialQueriesService.convertPointFromOMtoVP(point, tabProperties.mcCurrentSpatialQueries))
        let errorMessage = '';
        if (!tabProperties.selectedTraversLayer) {
            setPropertiesCallback('confirmDialogVisible', true);
            setPropertiesCallback('confirmDialogMessage', 'Missing Traversability Map Layer');
        }
        else if (tabProperties.linePoints.length > 0) {
            runCodeSafely(() => {
                let queryParams = getTabPropertiesByTabPropertiesClass(ParamsProperties).sqParams;
                if (tabProperties.isTraversAsync) {
                    queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((aTraversabilitySegments: MapCore.IMcSpatialQueries.STraversabilityPoint[]) => {
                        setTraversabilityAlongLineQueryResults(tabProperties.selectedTraversLayer, convertedPoints, aTraversabilitySegments, tabProperties.isTraversAsync, false, errorMessage);
                    }, (eErrorCode: MapCore.IMcErrors.ECode) => {
                        handleAsyncTravesabilityAlongLineQueryError(eErrorCode, tabProperties.selectedTraversLayer, convertedPoints)
                    }, 'SpatialQueriesForm/RasterAndTraversability.handleGetTraversabilityAlongLineClick => IMcSpatialQueries.GetTraversabilityAlongLine');
                }
                else {
                    queryParams.pAsyncQueryCallback = null;
                }
                let mcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
                runMapCoreSafely(() => {
                    traversabilityPoints = mcCurrentSpatialQueries.GetTraversabilityAlongLine(tabProperties.selectedTraversLayer.nodeMcContent, convertedPoints, queryParams)
                }, 'SpatialQueriesForm/RasterAndTraversability.handleGetTraversabilityAlongLineClick => IMcSpatialQueries.GetTraversabilityAlongLine', true, (error) => { errorMessage = String(error) })
            }, 'SpatialQueriesForm/RasterAndTraversability.handleGetTraversabilityAlongLineClick');
            if (!tabProperties.isTraversAsync) {
                setTraversabilityAlongLineQueryResults(tabProperties.selectedTraversLayer, convertedPoints, traversabilityPoints, tabProperties.isTraversAsync, false, errorMessage);
            }
        }
    }
    const handleGetTraversabilityFromColorClick = () => {
        runCodeSafely(() => {
            if (!tabProperties.selectedRasterLayer) {
                setPropertiesCallback('confirmDialogVisible', true);
                setPropertiesCallback('confirmDialogMessage', 'Missing Raster Map Layer');
            }
            else {
                getTraversabilityFromColorCode(tabProperties.selectedRasterLayer.nodeMcContent, tabProperties.rasterColor);
            }
        }, 'SpatialQueriesForm/RasterAndTraversability.handleGetTraversabilityFromColorClick');
    }
    //#endregion
    //#region DOM Functions
    const getRasterLayerByPointFieldset = () => {
        return <Fieldset style={{ width: '50%' }} className='form__column-fieldset' legend='Get Raster Layer By Point'>
            <div>
                <span style={{ textDecoration: 'underline' }}>Select Raster Map Layer: </span>
                <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 13.7}vh`, maxHeight: `${globalSizeFactor * 13.7}vh`, }} style={{ width: `${globalSizeFactor * 46}vh` }} name='selectedRasterLayer' optionLabel='label' value={tabProperties.selectedRasterLayer} onChange={saveData} options={tabProperties.rasterLayersList} />
            </div>
            <div style={{ paddingTop: `${globalSizeFactor * 4}vh` }} className="form__flex-and-row-between form__items-center">
                <span style={{ width: '30%' }}>Point:</span>
                <Vector3DFromMap disabledPointFromMap={tabProperties.activeOverlay ? true : false} flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={tabProperties.point} saveTheValue={(...args) => { save3DVector(...args, 'point') }} lastPoint={true} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '33%' }} className="form__flex-and-row-between form__items-center">
                    <label>LOD: </label>
                    <InputNumber className='form__narrow-input' name='lod' value={tabProperties.lod} onValueChange={saveData} />
                </div>
                <div className="form__flex-and-row form__items-center">
                    <Checkbox id="isNearestPixel" name="isNearestPixel" checked={tabProperties.isNearestPixel} onChange={saveData} />
                    <label htmlFor="isNearestPixel">Nearest Pixel</label>
                </div>
                <div className="form__flex-and-row form__items-center">
                    <Checkbox id="isRasterAsync" name="isRasterAsync" checked={tabProperties.isRasterAsync} onChange={saveData} />
                    <label htmlFor="isRasterAsync">Async</label>
                </div>
            </div>
            <Button label='Get Raster Layer Color By Point' onClick={handleGetRasterLayerColorClick} />
            <div className='form__flex-and-row-between'>
                <label>Raster Color:</label>
                <ColorPickerCtrl style={{ width: '40%' }} name='rasterColor' value={tabProperties.rasterColor} onChange={saveData} alpha={true} />
            </div>
            <Button label='Get Traversability From Color Code' onClick={handleGetTraversabilityFromColorClick} />
            <Fieldset legend='Get Traversability From Color Code Results'>
                <DataTable size={'small'} scrollable scrollHeight={`${globalSizeFactor * 13}vh`} tableStyle={{ height: `${globalSizeFactor * 13}vh`, width: `${globalSizeFactor * 30}vh`, maxWidth: `${globalSizeFactor * 30}vh` }} value={tabProperties.traversabilityTable}>
                    <Column field="directionAngle" header="Direction Angle"></Column>
                    <Column field="traversable" header="Traversable"></Column>
                </DataTable>
            </Fieldset>
        </Fieldset>
    }
    const getTraversabilityAlongLineFieldset = () => {
        return <Fieldset style={{ width: '50%' }} className='form__column-fieldset' legend='Traversability Along Line'>
            <div>
                <span style={{ textDecoration: 'underline' }}>Select Traversability Map Layer: </span>
                <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 13.7}vh`, maxHeight: `${globalSizeFactor * 13.7}vh`, }} style={{ width: `${globalSizeFactor * 46}vh` }} name='selectedTraversLayer' optionLabel='label' value={tabProperties.selectedTraversLayer} onChange={saveData} options={tabProperties.traversLayersList} />
            </div>
            <div className="form__column-container">
                <span style={{ textDecoration: 'underline', padding: `${globalSizeFactor * 0.7}vh` }}>Line Points: </span>
                <Vector3DGrid disabledPointFromMap={tabProperties.activeOverlay ? false : true} ctrlHeight={8} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initLocationPointsList={tabProperties.linePoints} sendPointList={(...args) => savePointsTable(...args, 'linePoints')} />
                <DrawLineCtrl className="form__aligm-self-center" disabled={tabProperties.activeOverlay ? false : true} handleDrawLineButtonClick={spatialQueriesService.removeExistObjects} activeViewport={tabProperties.currentViewportData} initItemResultsCB={getLineObject} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <Button style={{ width: '75%' }} label="Get Traversability Along Line" onClick={handleGetTraversabilityAlongLineClick} />
                <div style={{ width: '20%' }} className="form__flex-and-row form__items-center">
                    <Checkbox id="isTraversAsync" name="isTraversAsync" checked={tabProperties.isTraversAsync} onChange={saveData} />
                    <label htmlFor="isTraversAsync">Async</label>
                </div>
            </div>
            <Fieldset legend='Get Traversability Along Line Results'>
                <DataTable size={'small'} scrollable scrollHeight={`${globalSizeFactor * 13}vh`} tableStyle={{ height: `${globalSizeFactor * 13}vh`, width: `${globalSizeFactor * 30}vh`, maxWidth: `${globalSizeFactor * 30}vh` }} value={tabProperties.traversabilityPointsTable}>
                    {getColumns().map(({ field, header }) => {
                        return <Column style={{ width: `${globalSizeFactor * (field == 'traversability' ? 10 : 23 / 3)}vh` }} key={field} field={field} header={header} body={getFieldTemplate} />;
                    })}
                </DataTable>
            </Fieldset>
        </Fieldset>
    }
    //#endregion

    return (
        <div className="form__flex-and-row-between">
            {getRasterLayerByPointFieldset()}
            {getTraversabilityAlongLineFieldset()}
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
