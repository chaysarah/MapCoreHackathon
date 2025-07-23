import { useEffect, useState } from "react";
import { InputNumber } from "primereact/inputnumber";
import { useDispatch, useSelector } from "react-redux";
import { Dropdown } from "primereact/dropdown";
import { Fieldset } from "primereact/fieldset";
import { Checkbox } from "primereact/checkbox";
import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";

import QuerySecondaryDtmLayers from "../../layerSelector/querySecondaryDtmLayers";
import DeviceAndSpatialQueriesTabs from "../../../shared/deviceAndSpatialQueriesTabs";import SelectExistingTerrains from "../../../shared/ControlsForMapcoreObjects/terrainCtrl/selectExistingTerrain";
import Vector3DFromMap from "../../../treeView/objectWorldTree/shared/Vector3DFromMap";
import Vector3DGrid from "../../../treeView/objectWorldTree/shared/vector3DGrid";
import DrawLineCtrl from "../../../shared/drawLineCtrl";import { DetailedViewportWindow, getEnumDetailsList, getEnumValueDetails, Layerslist, MapCoreData, ViewportData, DetailedSectionMapViewportWindow } from 'mapcore-lib';
import { AppState } from "../../../../../redux/combineReducer";
import { addSectionMapViewportDetailed, addViewportDetailed } from "../../../../../redux/mapWindow/mapWindowAction";
import { selectMaxViewportId } from '../../../../../redux/mapWindow/mapWindowReducer';
import generalService from "../../../../../services/general.service";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";

export default function OpenMapManually() {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [SCreateData, setSCreateData] = useState(generalService.mapCorePropertiesBase.SCreateData);
    const [selectedTerrains, setSelectedTerrains] = useState<{ mcTerrain: MapCore.IMcMapTerrain, isNew: boolean }[]>([]);
    const [cameraPosition, setCameraPosition] = useState<MapCore.SMcVector3D>(new MapCore.SMcVector3D(0, 0, 0));
    const [boundingBoxTerrain, setBoundingBoxTerrain] = useState<MapCore.SMcBox>(new MapCore.SMcBox());
    const [isSectionMapViewport, setIsSectionMapViewports] = useState(false);
    const [locationPointsList, setLocationPointsList] = useState<MapCore.SMcVector3D[]>([]);
    const [isCalculateSectionHeightPoints, setIsCalculateSectionHeightPoints] = useState<boolean>(false);
    const activeViewport: ViewportData = useSelector((state: AppState) => MapCoreData.findViewport(state.mapWindowReducer.activeCard));
    const [waitForTheLayersToInitialize, setWaitForTheLayersToInitialize] = useState<boolean>(true);
    const [openDialog, setOpenDialog] = useState<boolean>(false);
    const [querySecondaryDtmLayers, setQuerySecondaryDtmLayers] = useState<Layerslist>(new Layerslist([], []));
    const [requestParams, setrRequestParams] = useState<MapCore.SMcKeyStringValue[]>(null);
    const [body, setBody] = useState<MapCore.SMcKeyStringValue>(null);

    useEffect(() => {
        generalService.mapCorePropertiesBase.SCreateData = SCreateData;
    }, [SCreateData])

    useEffect(() => {
        runCodeSafely(() => {
            let boundingBox: MapCore.SMcBox
            if (selectedTerrains.length > 0) {
                let mcSelectedTerrains = selectedTerrains.map(terrain => terrain.mcTerrain)
                runMapCoreSafely(() => {
                    boundingBox = mcSelectedTerrains[0].GetBoundingBox();
                    for (let i = 1; i < mcSelectedTerrains.length; i++) {
                        MapCore.SMcBox.Union(boundingBox, boundingBox, mcSelectedTerrains[i].GetBoundingBox());
                    }
                }, 'useEffect => GetBoundingBox', true)
                if (boundingBox) {
                    setBoundingBoxTerrain(boundingBox)
                    let position: MapCore.SMcVector3D = MapCore.SMcBox.CenterPoint(boundingBox)
                    setCameraPosition(position)
                }
            }
        }, "OpenMapManually.useEffect")
    }, [selectedTerrains])

    const saveSCreateData = (event: any) => {
        runCodeSafely(() => {
            let value = event.target.type === "checkbox" ? event.target.checked : event.target.value;
            let class_name: string = event.originalEvent?.currentTarget?.className;
            if (class_name?.includes("p-dropdown-item")) {
                value = event.target.value.theEnum;
            }
            setSCreateData({ ...SCreateData, [event.target.name]: value })
        }, "")
    }

    const [enumDetails] = useState({
        EMapType: getEnumDetailsList(MapCore.IMcMapCamera.EMapType),
        EQueryPrecision: getEnumDetailsList(MapCore.IMcSpatialQueries.EQueryPrecision),
    });

    const dispatch = useDispatch();
    let maxLayerId: number = useSelector(selectMaxViewportId);

    const createMap = () => {
        runCodeSafely(() => {
            let mcSelectedTerrains = selectedTerrains.map(terrain => terrain.mcTerrain)
            let VP;
            if (!isSectionMapViewport) {
                let typedDtmLayers = querySecondaryDtmLayers.existingLayers.map(l => l as MapCore.IMcDtmMapLayer)
                VP = new DetailedViewportWindow(maxLayerId + 1, { x: 1, y: 1 }, mcSelectedTerrains, cameraPosition, typedDtmLayers)
                VP.waitForTheLayersToInitialize = waitForTheLayersToInitialize;
                dispatch(addViewportDetailed(VP));
            }
            else {
                if (isCalculateSectionHeightPoints) {
                    let queryParams = new MapCore.IMcSpatialQueries.SQueryParams();
                    queryParams.eTerrainPrecision = MapCore.IMcSpatialQueries.EQueryPrecision.EQP_HIGHEST;
                    queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((aPointsWithHeights: MapCore.SMcVector3D[], afSlopes: Float32Array, pSlopesData: MapCore.IMcSpatialQueries.SSlopesData) => {
                        VP = new DetailedSectionMapViewportWindow(maxLayerId + 1, { x: 1, y: 1 }, mcSelectedTerrains, cameraPosition, locationPointsList, aPointsWithHeights)
                        VP.waitForTheLayersToInitialize = waitForTheLayersToInitialize;
                        dispatch(addSectionMapViewportDetailed(VP));
                    })
                    let slopesData = new MapCore.IMcSpatialQueries.SSlopesData();
                    let slopes: number[] = [0];
                    runMapCoreSafely(() => {
                        activeViewport.viewport.GetTerrainHeightsAlongLine(locationPointsList, slopes, slopesData, queryParams);
                    }, "createMap=> viewport.GetTerrainHeightsAlongLine", true)
                }
                else {
                    VP = new DetailedSectionMapViewportWindow(maxLayerId + 1, { x: 1, y: 1 }, mcSelectedTerrains, cameraPosition, locationPointsList, null)
                    VP.waitForTheLayersToInitialize = waitForTheLayersToInitialize;
                    dispatch(addSectionMapViewportDetailed(VP));
                }
            }
        }, "OpenMapManually.createMap")
    }
    useEffect(() => {
        runCodeSafely(() => {
            if (isSectionMapViewport) {
                if (MapCoreData.viewportsData.length > 0) {
                    setCameraPosition(new MapCore.SMcVector3D(0, 0, 0))
                }
                else {
                    alert("You can't capture points from map because there is no open map.\nYou can only insert points manually .");
                    setIsSectionMapViewports(false)
                }
            }
        }, 'OpenMapManually.useEffect')
    }, [isSectionMapViewport])


    const getLineObject = (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) => {
        runCodeSafely(() => {
            let points: MapCore.SMcVector3D[];
            if (nExitCode == 1) {
                runMapCoreSafely(() => {
                    points = pObject.GetLocationPoints(0);
                    pObject.Remove();
                }, 'getLineObject=> pObject.GetLocationPoints / Remove()', true)
                setLocationPointsList(points)
            }
        }, 'OpenMapManually.getLineObject');
    }

    return (<>
        <div style={{ display: 'flex' }}>
            <div>
                <DeviceAndSpatialQueriesTabs initialSCreateData={generalService.mapCorePropertiesBase.SCreateData} setSCreateData={(SCreateDataLocal: MapCore.IMcMapViewport.SCreateData) => {
                    generalService.mapCorePropertiesBase.SCreateData.pCoordinateSystem = SCreateDataLocal.pCoordinateSystem;
                    generalService.mapCorePropertiesBase.SCreateData.pOverlayManager = SCreateDataLocal.pOverlayManager;
                }} />
                <Fieldset className="form__column-fieldset" style={{ marginTop: `${globalSizeFactor * 1}vh` }}>
                    <div className='object-props__flex-and-row-between'>
                        <label>Map Type: </label>
                        <Dropdown className="object-props__dropdown" name="eMapType" value={getEnumValueDetails(SCreateData.eMapType, enumDetails.EMapType)} onChange={saveSCreateData} options={enumDetails.EMapType} optionLabel="name" />
                    </div>
                    <div className='object-props__flex-and-row-between'>
                        <label>DTM Usage and Precision: </label>
                        <Dropdown className="object-props__dropdown" name="eDtmUsageAndPrecision" value={getEnumValueDetails(SCreateData.eDtmUsageAndPrecision, enumDetails.EQueryPrecision)} onChange={saveSCreateData} options={enumDetails.EQueryPrecision} optionLabel="name" />
                    </div>
                    <div>
                        <Checkbox onChange={saveSCreateData} name="bShowGeoInMetricProportion" checked={SCreateData.bShowGeoInMetricProportion}></Checkbox>
                        <label className="ml-2">Show Geo In Metric Propotion</label>
                    </div>
                    <div>
                        <Checkbox onChange={saveSCreateData} name="bTerrainObjectsCache" checked={SCreateData.bTerrainObjectsCache}></Checkbox>
                        <label className="ml-2">Terrain Objects Cache</label>
                    </div>
                    <div >
                        <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>Terrain Resolution Factor:</label>
                        <InputNumber className="form__narrow-input" id='Max' value={SCreateData.fTerrainResolutionFactor} name='fTerrainResolutionFactor' onValueChange={saveSCreateData} />
                    </div>
                    <div >
                        <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>Terrain Object Best Resolution:</label>
                        <InputNumber className="form__narrow-input" id='Max' value={SCreateData.fTerrainObjectsBestResolution} name='fTerrainObjectsBestResolution' onValueChange={saveSCreateData} />
                    </div>
                </Fieldset>
            </div>
            <div style={{ width: `${globalSizeFactor * 43}vh` }}>
                <SelectExistingTerrains getSelectedTerrains={(selectedTerrains: { mcTerrain: MapCore.IMcMapTerrain, isNew: boolean }[]) => {
                    setSelectedTerrains(selectedTerrains)
                }} saveStandAlone={true} />
                <Fieldset legend="Terrain Bounding Box" className='form__column-fieldset '>
                    <div className="form__disabled"> <Vector3DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false, z: false }} name="MIN:" disabledPointFromMap={false}
                        initValue={boundingBoxTerrain.MinVertex} saveTheValue={(point) => { setBoundingBoxTerrain({ ...boundingBoxTerrain, MinVertex: point }) }}></Vector3DFromMap>
                        <Vector3DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false, z: false }} name="MAX:" disabledPointFromMap={false}
                            initValue={boundingBoxTerrain.MaxVertex} saveTheValue={(point) => { setBoundingBoxTerrain({ ...boundingBoxTerrain, MaxVertex: point }) }}></Vector3DFromMap></div>
                </Fieldset>
                <Vector3DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false, z: false }} name="Camera Position" disabledPointFromMap={false}
                    initValue={cameraPosition} saveTheValue={(point: MapCore.SMcVector3D) => { setCameraPosition(point) }}></Vector3DFromMap>
                <Button onClick={() => setOpenDialog(true)}>Add Query Secondary Dtm Layers</Button>
                <Fieldset className="form__column-fieldset"
                    legend={<><Checkbox onChange={(e) => { setIsSectionMapViewports(e.checked) }} checked={isSectionMapViewport}></Checkbox><label>Section Map Viewport</label></>}>
                    <div className={`${!isSectionMapViewport && "form__disabled"}`} >
                        <div>
                            <label style={{ textDecoration: 'underline' }}> Section Route Points:</label>
                            <Vector3DGrid ctrlHeight={10} initLocationPointsList={locationPointsList} sendPointList={(point) => { setLocationPointsList(point) }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD}></Vector3DGrid>
                            <div style={{ display: 'flex' }}>
                                <label>Sample route points:</label>
                                <DrawLineCtrl activeViewport={activeViewport} initItemResultsCB={getLineObject} />
                            </div>
                            <div>
                                <Checkbox onChange={(e) => { setIsCalculateSectionHeightPoints(e.target.checked) }} name="bShowGeoInMetricProportion" checked={isCalculateSectionHeightPoints}></Checkbox>
                                <label className="ml-2"> Is Calculate Section Height Points</label>
                            </div>
                        </div>
                    </div>
                </Fieldset>
                <div>
                    <Checkbox onChange={(e) => { setWaitForTheLayersToInitialize(!e.checked) }} name="WaitForTheLayersToInitialize" checked={!waitForTheLayersToInitialize}></Checkbox>
                    <label className="ml-2">Open viewport without waiting until all layers are initialized</label>
                </div>
                <div style={{ direction: 'rtl', marginTop: `${globalSizeFactor * 0.5}vh` }}>
                    <Button onClick={createMap}>Create Map</Button></div>
            </div>
            <Dialog onHide={() => { setOpenDialog(false) }} visible={openDialog}>
                <QuerySecondaryDtmLayers body={body} requestParams={requestParams} DTMlayerslist={querySecondaryDtmLayers} getSelectedLayer={(dtmLayersList: Layerslist) => {
                    let initializedLayers = generalService.OpenMapService.initLayers(dtmLayersList);
                    dtmLayersList.newLayers = [];
                    dtmLayersList.existingLayers = [...dtmLayersList.existingLayers, ...initializedLayers];
                    setQuerySecondaryDtmLayers(dtmLayersList);
                    setOpenDialog(false);
                }} />
            </Dialog>
        </div>
    </>
    )
}