// External libraries
import { useEffect, useRef, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

// UI/component libraries
import { Checkbox } from 'primereact/checkbox';
import { RadioButton } from 'primereact/radiobutton';
import { InputNumber } from 'primereact/inputnumber';
import { Dropdown } from 'primereact/dropdown';
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import { Fieldset } from 'primereact/fieldset';

// Project-specific imports
import { LayerDetails, ViewportParams, Layerslist, StandardViewportWindow, MapCoreData, getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import ServerRequestParams from './serverRequestParams';
import TilingSchemeParams from './tilingSchemeParams';
import QuerySecondaryDtmLayers from './querySecondaryDtmLayers';
import { ParametersMode } from './layersParams';
import ManualLayers from './manualLayers';
import SelectExistingLayer from '../../shared/ControlsForMapcoreObjects/layerCtrl/selectExistingLayer';
import SelectCoordinateSystem from '../../shared/ControlsForMapcoreObjects/coordinateSystemCtrl/selectCoordinateSystem';
import SelectOverlayManager from '../../shared/ControlsForMapcoreObjects/overlayManagerCtrl/selectOverlayManager';
import Footer from '../../footerDialog';
import { closeDialog } from '../../../../redux/MapCore/mapCoreAction';
import { addViewportStandard } from '../../../../redux/mapWindow/mapWindowAction';
import { selectMaxViewportId } from '../../../../redux/mapWindow/mapWindowReducer';
import { AppState } from '../../../../redux/combineReducer';
import generalService from '../../../../services/general.service';
import { runCodeSafely } from '../../../../common/services/error-handling/errorHandler';
import objectWorldTreeService from '../../../../services/objectWorldTree.service';
import { DialogTypesEnum } from '../../../../tools/enum/enums';

const OpenViewportWithSeveralLayers = () => {
    const dispatch = useDispatch();
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let maxLayerId: number = useSelector(selectMaxViewportId);
    const [serverSelectedLayers, setServerSelectedLayers] = useState<LayerDetails[]>([]);
    const [typeD, setTypeD] = useState<string>('2D');
    const [terrainFactor, setTerrainFactor] = useState<number>(1);
    const [viewportParams, setViewportParams] = useState<ViewportParams>(new ViewportParams(
        MapCore.IMcMapCamera.EMapType.EMT_2D, 1, true, null, null, null, null, new Layerslist([], [])));
    const [existOverlayManagersList, setExistOverlayManagersList] = useState([]);
    const layersArrayRef = useRef(null);
    const [selectedCorSys, setSelectedCorSys] = useState(null);
    const [existsDevice, setExistsDevice] = useState(MapCoreData.device ? true : false);
    const [requestParams, setRequestParams] = useState<MapCore.SMcKeyStringValue[]>([]);
    const [body, setBody] = useState<MapCore.SMcKeyStringValue>(null);
    const [typeDialog, setTypeDialog] = useState("");
    const [deviceInitParams, setDeviceInitParams] = useState(generalService.mapCorePropertiesBase.deviceInitParams);
    const [selectExistingLayers, setSelectExistingLayers] = useState<{ mcLayer: MapCore.IMcMapLayer, isNew: boolean }[]>([]);
    const [waitForTheLayersToInitialize, setWaitForTheLayersToInitialize] = useState<boolean>(true);

    const [enumDetails] = useState({
        LoggingLevel: getEnumDetailsList(MapCore.IMcMapDevice.ELoggingLevel),
        AntiAliasingLevel: getEnumDetailsList(MapCore.IMcMapDevice.EAntiAliasingLevel),
    });

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.8 * globalSizeFactor;
        root.style.setProperty('--choose-layers-dialog-width', `${pixelWidth}px`);
    }, [])

    useEffect(() => {
        setViewportParams({ ...viewportParams, coordinateSystem: selectedCorSys })
    }, [selectedCorSys])

    const saveDeviceData = (event: any) => {
        runCodeSafely(() => {
            let value = event.target.type === "checkbox" ? event.target.checked : event.target.value;
            let class_name: string = event.originalEvent?.currentTarget?.className;
            if (class_name?.includes("p-dropdown-item")) {
                value = event.target.value.theEnum;
            }
            setDeviceInitParams({ ...deviceInitParams, [event.target.name]: value })
        }, "EditModePropertiesDialog/General.saveData => onChange")
    }

    useEffect(() => {
        generalService.mapCorePropertiesBase.deviceInitParams = deviceInitParams
    }, [deviceInitParams])

    const onOk = () => {
        if ((viewportParams.coordinateSystem == null) && (!waitForTheLayersToInitialize)) {
            alert('When "Open viewport without waiting until all layers are initialized" is checked, it is mandatory to enter a coordinate.')
            return;
        }
        let interval;
        runCodeSafely(() => {
            let layersArr: LayerDetails[] = layersArrayRef.current.getLayersArray();
            layersArr.map(l => {
                l.layerParams.targetCoordSys = viewportParams.coordinateSystem;
                if (l.layerParams.rawParams)
                    l.layerParams.rawParams.pCoordinateSystem = viewportParams.coordinateSystem;
            })
            let selectedNewLayers: LayerDetails[] = [...serverSelectedLayers, ...layersArr]
            let layerslist = new Layerslist(selectExistingLayers.map(layer => layer.mcLayer), selectedNewLayers)
            if (typeD == "2D/3D") {
                let firstVP = maxLayerId + 1
                let VP = new StandardViewportWindow(firstVP, layerslist, { x: 1, y: 1 }, { ...viewportParams, mapType: MapCore.IMcMapCamera.EMapType.EMT_2D })
                VP.waitForTheLayersToInitialize = waitForTheLayersToInitialize;
                dispatch(addViewportStandard(VP));
                let timer = 0;
                interval = setInterval(() => {
                    timer++;
                    const foundVP = MapCoreData.findViewport(firstVP);
                    if (!foundVP) {
                        clearInterval(interval);
                    }
                    else if (foundVP.viewport) {
                        clearInterval(interval);
                        let overlayManager = MapCoreData.findViewport(firstVP).viewport.GetOverlayManager();
                        let VP = new StandardViewportWindow(maxLayerId + 2, layerslist, { x: 1, y: 1 }, { ...viewportParams, mapType: MapCore.IMcMapCamera.EMapType.EMT_3D, overlayManager: overlayManager })
                        VP.waitForTheLayersToInitialize = waitForTheLayersToInitialize;
                        dispatch(addViewportStandard(VP));
                    }
                }, 100)
            }
            else {
                let VP = new StandardViewportWindow(maxLayerId + 1, layerslist, { x: 1, y: 1 }, viewportParams)
                VP.waitForTheLayersToInitialize = waitForTheLayersToInitialize;
                dispatch(addViewportStandard(VP));
            }
        }, "SeveralLayersDialog.onOk", () => { clearInterval(interval) })
    }

    useEffect(() => {
        runCodeSafely(() => {
            let viewportsOM = MapCoreData.overlayManagerArr.map((om) => {
                let omLabel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, om.overlayManager);
                return { mcOverlayManager: om.overlayManager, label: omLabel?.label }
            }).flat();
            setExistOverlayManagersList(viewportsOM);
        }, "SeveralLayersDialog.useEffect")
    }, [treeRedux])

    const dialogComp = () => {
        switch (typeDialog) {
            case "Server Request Params":
                return <ServerRequestParams initRequestParams={requestParams} initBody={body} getRequestParams={(requestParams, body) => { setTypeDialog(""); setRequestParams(requestParams); setBody(body); setViewportParams({ ...viewportParams, requestParams: requestParams }) }} showCSWBody={true}></ServerRequestParams>
            case "Tiling Scheme":
                return <TilingSchemeParams getTilingScheme={(tilingScheme) => {
                    setTypeDialog(""); setViewportParams({ ...viewportParams, tilingScheme: tilingScheme })
                }}></TilingSchemeParams>
            case "Query Secondary Dtm Layers":
                return <QuerySecondaryDtmLayers body={body} requestParams={requestParams} DTMlayerslist={viewportParams.querySecondaryDtmLayers} getSelectedLayer={(DTMlayerslist: Layerslist) => {
                    setViewportParams({ ...viewportParams, querySecondaryDtmLayers: DTMlayerslist }); setTypeDialog("");
                }} getRequestParams={(requestParams: MapCore.SMcKeyStringValue[], body: MapCore.SMcKeyStringValue) => { setRequestParams(requestParams); setBody(body) }} />
        }
    }
    return (
        <div>
            <div >
                <div style={{ display: 'flex' }}>
                    <ManualLayers ref={layersArrayRef} onlyDtm={false} returnLayers={(SL: LayerDetails[]) => { setServerSelectedLayers([...serverSelectedLayers, ...SL]) }} requestParams={requestParams} body={body} parametersMode={ParametersMode.MinimumParameters} />
                </div>
                <div>
                    <div style={{ display: 'flex' }}>
                        <Fieldset legend="Viewport">
                            <div style={{ marginRight: `${globalSizeFactor * 8}vh` }}>
                                <div>
                                    <Checkbox onChange={e => { setViewportParams({ ...viewportParams, showGeo: e.checked }); }} checked={viewportParams.showGeo}></Checkbox>
                                    <label className="ml-2">Show Geo In Metric Propotion</label>
                                </div>
                                <div className="form__space-between">
                                    <label>Terrain Precision Factor</label>
                                    <InputNumber minFractionDigits={0} maxFractionDigits={5} value={viewportParams.terrainFactor} onValueChange={(e) => setViewportParams({ ...viewportParams, terrainFactor: e.value })} mode="decimal" />
                                </div>
                                <div className="form__space-between">
                                    {/* To Fix */}
                                    <label>Overlay Manager Scale Factor</label>
                                    <InputNumber minFractionDigits={0} maxFractionDigits={5} value={terrainFactor} onValueChange={(e) => setTerrainFactor(e.value!)} mode="decimal" />
                                </div>
                                <div style={{ display: 'flex' }}>
                                    <div><label>Viewport Type:</label></div>
                                    <div>
                                        <div className="flex align-items-center"> <RadioButton value="2D" onChange={(e: { value: string; }) => { setTypeD(e.value); setViewportParams({ ...viewportParams, mapType: MapCore.IMcMapCamera.EMapType.EMT_2D }) }} checked={typeD === '2D'} /> <label htmlFor="ingredient1" className="ml-2">2D</label> </div>
                                        <div className="flex align-items-center"> <RadioButton value="3D" onChange={(e: { value: string; }) => { setTypeD(e.value); setViewportParams({ ...viewportParams, mapType: MapCore.IMcMapCamera.EMapType.EMT_3D }) }} checked={typeD === '3D'} /> <label htmlFor="ingredient2" className="ml-2">3D</label> </div>
                                        <div className="flex align-items-center"> <RadioButton value="2D/3D" onChange={(e: { value: string; }) => setTypeD(e.value)} checked={typeD === '2D/3D'} /> <label htmlFor="ingredient3" className="ml-2">2D/3D</label> </div>
                                    </div>
                                </div>
                            </div>
                        </Fieldset>
                        <Fieldset className={`form__space-between form__column-fieldset existsDevice ${existsDevice && "form__disabled"}`} legend="Device">
                            <div>
                                <label className="ml-2">Logging Level</label>
                                <Dropdown name='eLoggingLevel' value={getEnumValueDetails(deviceInitParams.eLoggingLevel, enumDetails.LoggingLevel)}
                                    onChange={saveDeviceData} options={enumDetails.LoggingLevel} optionLabel="name" />
                            </div>
                            <div>
                                <label className="ml-2">Viewport Anti Aliasing Level:</label>
                                <Dropdown name='eViewportAntiAliasingLevel' value={getEnumValueDetails(deviceInitParams.eViewportAntiAliasingLevel, enumDetails.AntiAliasingLevel)} onChange={saveDeviceData} options={enumDetails.AntiAliasingLevel} optionLabel="name" />
                            </div>
                            <div>
                                <label className="ml-2">Terrain Objects Anti Aliasing Level:</label>
                                <Dropdown name='eTerrainObjectsAntiAliasingLevel' value={getEnumValueDetails(deviceInitParams.eTerrainObjectsAntiAliasingLevel, enumDetails.AntiAliasingLevel)}
                                    onChange={saveDeviceData} options={enumDetails.AntiAliasingLevel} optionLabel="name" />
                            </div>
                            <div >
                                <label> Number Background Threads:</label>
                                <InputNumber useGrouping={false} minFractionDigits={0} maxFractionDigits={5} value={deviceInitParams.uNumBackgroundThreads} name="uNumBackgroundThreads"
                                    onValueChange={saveDeviceData} mode="decimal" />
                            </div>
                            <div>
                                <Checkbox name='bFullScreen' onChange={saveDeviceData} checked={deviceInitParams.bFullScreen}></Checkbox>
                                <label className="ml-2">Multi Screen</label>
                            </div>
                        </Fieldset>
                    </div>
                    <div style={{ display: 'flex' }}>
                        <SelectOverlayManager getSelectedOM={(selectedOMs: MapCore.IMcOverlayManager) => { setViewportParams({ ...viewportParams, overlayManager: selectedOMs }) }} initSelectedOMs={viewportParams.overlayManager}></SelectOverlayManager>
                        <SelectExistingLayer initSelectedLayers={selectExistingLayers.map(layer => layer.mcLayer)} enableCreateNewLayer={false} getSelectedLayer={(layers: { mcLayer: MapCore.IMcMapLayer, isNew: boolean }[]) => { setSelectExistingLayers(layers) }} />
                        <SelectCoordinateSystem getSelectedCorSys={(coor: MapCore.IMcGridCoordinateSystem) => { setSelectedCorSys(coor) }} header="Grid Coordinate System"></SelectCoordinateSystem>
                        <div className='form__column-container' style={{ marginLeft: `${globalSizeFactor * 1}vh` }}>
                            <Button onClick={() => { setTypeDialog("Server Request Params") }}>Server Request Params</Button>
                            {/* <Button onClick={() => { setTypeDialog("Tiling Scheme") }}>Tiling Scheme</Button> */}
                            <Button onClick={() => { setTypeDialog("Query Secondary Dtm Layers") }} >Query Secondary Dtm Layers</Button>
                            <div>
                                <Checkbox onChange={e => { setViewportParams({ ...viewportParams, displayItemsAttachedToStaticObjectsWithoutDtm: e.checked }); }} checked={viewportParams.displayItemsAttachedToStaticObjectsWithoutDtm}></Checkbox>
                                <label className="ml-2">Display Items Attached To Static Objects Without Dtm</label>
                            </div>
                            <div>
                                <Checkbox onChange={(e) => { setWaitForTheLayersToInitialize(!e.checked) }} name="WaitForTheLayersToInitialize" inputId="WaitForTheLayersToInitializeCheckbox" checked={!waitForTheLayersToInitialize}></Checkbox>
                                <label className="ml-2" htmlFor="WaitForTheLayersToInitializeCheckbox">Open viewport without waiting until all layers are initialized</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <Footer onOk={onOk} onCancel={() => dispatch(closeDialog(DialogTypesEnum.chooseLayers))} label="Ok"></Footer>
            <Dialog onHide={() => { setTypeDialog("") }} visible={typeDialog != ""} header={typeDialog} >
                {dialogComp()}
            </Dialog>
        </div >
    );
}
export default OpenViewportWithSeveralLayers;