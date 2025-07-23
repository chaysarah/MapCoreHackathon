import { useState, useEffect } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Button } from 'primereact/button';
import { Fieldset } from 'primereact/fieldset';
import { ListBox } from 'primereact/listbox';
import { InputNumber } from 'primereact/inputnumber';
import { Checkbox } from 'primereact/checkbox';
import { InputText } from 'primereact/inputtext';
import { Dropdown } from 'primereact/dropdown';
import { Dialog } from 'primereact/dialog';
import { RadioButton } from 'primereact/radiobutton';
import { useSelector } from 'react-redux';

import { LayerDetails, LayerSourceEnum, LayerNameEnum, MapCoreService, enums } from 'mapcore-lib';
import './styles/manualLayers.css';
import TilingSchemeParams from './tilingSchemeParams';
import WebMapServiceType from './webMapServiceType';
import ServerRequestParams from './serverRequestParams';
import SelectCoordinateSystem from '../../shared/ControlsForMapcoreObjects/coordinateSystemCtrl/selectCoordinateSystem';
import Vector3DFromMap from '../../treeView/objectWorldTree/shared/Vector3DFromMap';
import ColorPickerCtrl from '../../../shared/colorPicker';
import { AppState } from '../../../../redux/combineReducer';
import generalService from '../../../../services/general.service';

export default function LayersSelectionTable(props: {
    onChooseLayers: (layerId: LayerDetails[], isOpenAllLayersAsOne: boolean) => void,
    setDisplayTreeDialog: (bool: boolean) => void,
    serverLayers: MapCore.IMcMapLayer.SServerLayerInfo[],
    urlServer: string,
    layerSource: LayerSourceEnum,
    requestParams: MapCore.SMcKeyStringValue[]
}) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [WMTSServerLayer, setWMTSServerLayer] = useState<MapCore.IMcMapLayer.SServerLayerInfo[]>()
    const [layerSelectedWNTS_CSW, setLayerSelectedWNTS_CSW] = useState<LayerDetails[]>()

    let [selectedlayers, setSelectedlayers] = useState<MapCore.IMcMapLayer.SServerLayerInfo[]>([])
    let [SWebMapServiceParams, setSWebMapServiceParams] = useState(new MapCore.IMcMapLayer.SWebMapServiceParams())
    let [SWMSParams, setSWMSParams] = useState(new MapCore.IMcMapLayer.SWMSParams())
    let [SWCSParams, setSWCSParams] = useState(new MapCore.IMcMapLayer.SWCSParams())
    let [SWMTSParams, setSWMTSParams] = useState({ ...new MapCore.IMcMapLayer.SWMTSParams(), bUseServerTilingScheme: true })
    let [SCSWParams, setSCSWParams] = useState({ OrthometricHeights: false })
    let [styles, setStyles] = useState(["Default"])
    let [prevLayer, setPrevLayer] = useState(null)
    let [isOpenAllLayersAsOne, setIsOpenAllLayersAsOne] = useState(false)
    let [selectedLayerType, setSelectedLayerType] = useState(enums.LayerNameEnum.NativeServerRaster)
    const [requestParams, setRequestParams] = useState<MapCore.SMcKeyStringValue[]>(props.requestParams);
    const [tilingScheme, setTilingScheme] = useState<MapCore.IMcMapLayer.STilingScheme>(null);

    let [typeDialog, setTypeDialog] = useState(null)
    const dialogComp = () => {
        switch (typeDialog) {
            case "Server Request Params":
                return <ServerRequestParams initRequestParams={requestParams} getRequestParams={function (RequestParams: MapCore.SMcKeyStringValue[]): void {
                    setRequestParams(RequestParams);
                    setTypeDialog(null);
                }} showCSWBody={false}></ServerRequestParams>

            case "Select Style":
                return <div>
                    <ListBox value={styles} multiple={true} onChange={(e) => {
                        if (e.value?.filter((s: string) => s == "Default").length > 0)
                            setStyles(["Default"])
                        else
                            setStyles(e.value)
                    }} options={selectedlayers[0] ? ["Default", ...selectedlayers[0]?.astrStyles] : []} ></ListBox>
                    <Button onClick={() => {
                        let strStyle = "";
                        if (styles[0] == "Default")
                            selectedlayers[0].astrStyles.map((l, i) => { strStyle += l + "," })
                        else
                            styles.map((l, i) => { strStyle += l + "," })
                        setSWebMapServiceParams({ ...SWebMapServiceParams, strStylesList: strStyle.slice(0, -1) })
                    }}>OK</Button>
                </div>
            case "Tiling Scheme":
                return <TilingSchemeParams getTilingScheme={(tilingScheme) => { setTilingScheme(tilingScheme); setTypeDialog(null) }}></TilingSchemeParams>
            case "Layers Selection Table":
                return <LayersSelectionTable
                    onChooseLayers={(layers: LayerDetails[], isOpenAllLayersAsOne: boolean) => { setLayerSelectedWNTS_CSW(layers) }}
                    setDisplayTreeDialog={(m) => { setTypeDialog(null) }}
                    serverLayers={WMTSServerLayer}
                    urlServer={selectedlayers[0].strLayerId}
                    layerSource={LayerSourceEnum.CSW_WMTS}
                    requestParams={props.requestParams}></LayersSelectionTable>
        }
        return null
    }
    const saveLayer = () => {
        if (selectedlayers.length > 0) {
            let layerSelected_;
            if (props.layerSource == LayerSourceEnum.WMTS || props.layerSource == LayerSourceEnum.CSW_WMTS) {
                layerSelected_ = selectedlayers.map((l: MapCore.IMcMapLayer.SServerLayerInfo) => {
                    let SWMTSP = isOpenAllLayersAsOne ? SWebMapServiceParams : getWebMapServiceParamsLayer(l)
                    let layerDetails = new LayerDetails(l.strLayerId, props.layerSource, { WMTS: { ...SWMTSParams, ...SWMTSP }, urlServer: props.urlServer },
                        generalService.layerPropertiesBase.layerIReadCallback, selectedLayerType)
                    return layerDetails;
                })
            }
            if (props.layerSource == LayerSourceEnum.WMS) {
                layerSelected_ = selectedlayers.map((l: MapCore.IMcMapLayer.SServerLayerInfo) => {
                    let SWMSP = isOpenAllLayersAsOne ? SWebMapServiceParams : getWebMapServiceParamsLayer(l)
                    let layerDetails = new LayerDetails(l.strLayerId, props.layerSource, { WMS: { ...SWMSParams, ...SWMSP as any }, urlServer: props.urlServer },
                        generalService.layerPropertiesBase.layerIReadCallback, selectedLayerType);
                    return layerDetails;
                })
            }
            if (props.layerSource == LayerSourceEnum.WCS) {
                layerSelected_ = selectedlayers.map((l: MapCore.IMcMapLayer.SServerLayerInfo) => {
                    let SWCSP = isOpenAllLayersAsOne ? SWebMapServiceParams : getWebMapServiceParamsLayer(l)
                    let layerDetails = new LayerDetails(l.strLayerId, props.layerSource, { WCS: { ...SWCSParams, ...SWCSP as any }, urlServer: props.urlServer },
                        generalService.layerPropertiesBase.layerIReadCallback, selectedLayerType);
                    return layerDetails;
                })
            }
            if (props.layerSource == LayerSourceEnum.CSW) {
                if (selectedlayers[0].strLayerType == "WMTS_SERVER_URL") {
                    layerSelected_ = layerSelectedWNTS_CSW
                }
                else
                    layerSelected_ = selectedlayers.map((l: MapCore.IMcMapLayer.SServerLayerInfo) => {
                        let layerDetails = new LayerDetails(l.strLayerId, props.layerSource,
                            { CSW: SWebMapServiceParams, urlServer: props.urlServer, serverLayerInfo: l, orthometricHeights: SCSWParams.OrthometricHeights },
                            generalService.layerPropertiesBase.layerIReadCallback, LayerNameEnum.Raw3DModel);
                        return layerDetails;
                    })
            }
            props.onChooseLayers(layerSelected_, isOpenAllLayersAsOne);
            props.setDisplayTreeDialog(false)
        }
        else {
            alert(("No layers selected"))
        }
    }
    const changeSWebMapServiceParams = (event: any) => {
        setSWebMapServiceParams({ ...SWebMapServiceParams, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
    }
    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.6 * globalSizeFactor;
        root.style.setProperty('--layers-selection-table-dialog-width', `${pixelWidth}px`);
    }, [])

    const getWebMapServiceParamsLayer = (layer): MapCore.IMcMapLayer.SWebMapServiceParams => {
        let WebMapServiceParams = { ...SWebMapServiceParams }
        if (layer) {
            WebMapServiceParams.pReadCallback = generalService.layerPropertiesBase.layerIReadCallback;
            WebMapServiceParams.pCoordinateSystem = layer.pCoordinateSystem;
            WebMapServiceParams.BoundingBox = GetBB(layer);
            WebMapServiceParams.strServerURL = props.urlServer;
            WebMapServiceParams.strServerCoordinateSystem = layer.aTileMatrixSets[0].strName;
            WebMapServiceParams.bTransparent = layer.bTransparent;
            WebMapServiceParams.aRequestParams = requestParams;
            WebMapServiceParams.strLayersList = layer.strLayerId
            let strStyle = "";
            layer.astrStyles.map((l, i) => { strStyle += l + "," })
            WebMapServiceParams.strStylesList = strStyle
            WebMapServiceParams.strImageFormat = layer.astrImageFormats[0];
            if (!WebMapServiceParams.strImageFormat)
                WebMapServiceParams.strImageFormat = ""
        }
        return WebMapServiceParams
    }

    useEffect(() => {
        let layer = selectedlayers[0];
        if (layer?.strLayerType == "WMTS_SERVER_URL") {
            MapCoreService.loadServerLayers((serverLayer: any[]) => {
                setWMTSServerLayer(serverLayer);
                setTypeDialog("Layers Selection Table")
            }, layer.strLayerId, MapCore.IMcMapLayer.EWebMapServiceType.EWMS_WMTS, props.requestParams)
        }
        let WebMapServiceParams = { ...SWebMapServiceParams }
        if (layer) {
            WebMapServiceParams.pReadCallback = generalService.layerPropertiesBase.layerIReadCallback;
            WebMapServiceParams.pCoordinateSystem = layer.pCoordinateSystem;
            WebMapServiceParams.BoundingBox = GetBB(layer);
            WebMapServiceParams.strServerURL = props.urlServer;
            WebMapServiceParams.strServerCoordinateSystem = layer.aTileMatrixSets[0].strName;
            WebMapServiceParams.bTransparent = layer.bTransparent;
            WebMapServiceParams.aRequestParams = requestParams;
            let str = "";
            selectedlayers.map((l, i) => { str += l.strLayerId + "," })
            WebMapServiceParams.strLayersList = str.slice(0, -1);
            let strStyle = "";
            layer.astrStyles.map((l, i) => { strStyle += l + "," })
            WebMapServiceParams.strStylesList = strStyle.slice(0, -1);
            if (layer != prevLayer)
                WebMapServiceParams.strImageFormat = layer.astrImageFormats[0];
            setPrevLayer(layer)
            if (!WebMapServiceParams.strImageFormat)
                WebMapServiceParams.strImageFormat = ""
        }
        else {
            WebMapServiceParams.strImageFormat = "";
            WebMapServiceParams.strStylesList = "";
            WebMapServiceParams.strLayersList = "";
            WebMapServiceParams.BoundingBox = new MapCore.SMcBox(0, 0, 0, 0, 0, 0);
            WebMapServiceParams.pCoordinateSystem = null;
        }
        setSWebMapServiceParams({ ...WebMapServiceParams });
    }, [selectedlayers])

    useEffect(() => { setSWebMapServiceParams({ ...SWebMapServiceParams, aRequestParams: requestParams }) }, [requestParams])
    const itemTemplate = (option: any) => {
        return <span>{option}</span>;
    };
    const GetBBStr = (boundingBox: MapCore.SMcBox) => {
        let strBoundingBox = "";
        if (boundingBox != null) {
            strBoundingBox = "((" + boundingBox.MinVertex.x.toFixed(2) + "," + boundingBox.MinVertex.y.toFixed(2)
                + "),(" + boundingBox.MaxVertex.x.toFixed(2) + "," + boundingBox.MaxVertex.y.toFixed(2) + "))";
        }
        return strBoundingBox;
    }
    const GetBB = (layer: MapCore.IMcMapLayer.SServerLayerInfo) => {
        let tileMatrixSet: MapCore.IMcMapLayer.STileMatrixSet = layer.aTileMatrixSets[0];
        return tileMatrixSet.BoundingBox
    }

    return (
        <div>
            <div style={{ height: `${globalSizeFactor * 40}vh`, overflowY: 'auto', maxWidth: `${globalSizeFactor * 90}vh`, overflowX: 'auto' }}>
                <DataTable value={props.serverLayers} selectionMode="multiple" tableStyle={{ minWidth: `${globalSizeFactor * 1.5 * 55}vh`, maxWidth: `${globalSizeFactor * 1.5 * 55}vh` }}
                    selection={selectedlayers} onSelectionChange={(e) => { setSelectedlayers(e.value as MapCore.IMcMapLayer.SServerLayerInfo[]) }}>
                    <Column selectionMode="multiple"></Column>
                    <Column header={(props.serverLayers[0]?.strLayerType == "WMTS_SERVER_URL") ? "Selected Layers From WMTS Server" : "Groups"} field="astrGroups" body={(rowData) => {
                        if ((rowData.strLayerType == "WMTS_SERVER_URL") && (layerSelectedWNTS_CSW?.length > 0)) {
                            let str = "";
                            layerSelectedWNTS_CSW.forEach((layer, index) => {
                                str += layerSelectedWNTS_CSW[index]?.path + ",";
                            })
                            str = str.slice(0, -1);
                            return <>{layerSelectedWNTS_CSW?.length + " : " + str}</>
                        }
                        else return <>{rowData.astrGroups}</>
                    }}></Column>
                    {/* style={{ maxWidth: `${globalSizeFactor * 20.5}vh`,overflowX:'auto'}} */}
                    <Column header="Identifier" field="strLayerId"></Column>
                    <Column header="Title" field="strTitle" ></Column>
                    <Column header="Type" field="strLayerType" ></Column>
                    <Column header="Coordinate System" body={(rowData) => { return <div> {rowData.aTileMatrixSets[0].strName}</div> }}></Column>
                    <Column header="Draw Priority" field="nDrawPriority"></Column>
                    <Column header="Bounding Box" field="BoundingBox"
                        body={(rowData) => { return <div><label>{GetBBStr(GetBB(rowData))}  </label></div> }}></Column>
                    <Column header="Metadata" field="aMetadataValues" body={(rowData) => {
                        if (typeof rowData.aMetadataValues == "string")
                            return <> {rowData.aMetadataValues}</>
                        else {
                            let str = ""
                            for (let index = 0; index < rowData.aMetadataValues.length; index++) {
                                str += "(" + rowData.aMetadataValues[index].strKey + "," + rowData.aMetadataValues[index].strValue + "),"
                            }
                            str = str.slice(0, -1);
                            return <div style={{ overflow: 'auto', maxWidth: `${globalSizeFactor * 30}vh`, maxHeight: `${globalSizeFactor * 10}vh` }}>{str}</div>
                        }
                    }}></Column>
                </DataTable>
            </div>
            {props.layerSource != LayerSourceEnum.CSW && <div className="font__is-as-default-div">
                <Checkbox style={{ marginTop: `${globalSizeFactor * 0.6}vh` }} name='isSetAsDefault' onChange={(e) => { setIsOpenAllLayersAsOne(e.target.checked) }} checked={isOpenAllLayersAsOne} />
                <label htmlFor="isSetAsDefault" className="ml-2" >Open All Layers As One</label>
            </div>}
            <SelectCoordinateSystem getSelectedCorSys={(coor: MapCore.IMcGridCoordinateSystem) => { setSWebMapServiceParams({ ...SWebMapServiceParams, pCoordinateSystem: coor }) }} header="Grid Coordinate System" initSelectedCorSys={SWebMapServiceParams.pCoordinateSystem}></SelectCoordinateSystem>
            {props.layerSource != LayerSourceEnum.CSW && <div>
                <div className="font__is-as-default-div">
                    <Checkbox style={{ marginTop: `${globalSizeFactor * 0.6}vh` }} name='bEnhanceBorderOverlap' checked={SWebMapServiceParams.bEnhanceBorderOverlap} onChange={changeSWebMapServiceParams} />
                    <label htmlFor="isSetAsDefault" className="ml-2">Enhance Border Overlap</label>
                </div>
                <div className="font__is-as-default-div">
                    <Checkbox style={{ marginTop: `${globalSizeFactor * 0.6}vh` }} name='bFillEmptyTilesByLowerResolutionTiles' checked={SWebMapServiceParams.bFillEmptyTilesByLowerResolutionTiles} onChange={changeSWebMapServiceParams} />
                    <label htmlFor="isSetAsDefault" className="ml-2">Fill Empty Tiles By Lower Resolution Tiles</label>
                </div>
                <div className="font__is-as-default-div">
                    <Checkbox style={{ marginTop: `${globalSizeFactor * 0.6}vh` }} name='bResolveOverlapConflicts' checked={SWebMapServiceParams.bResolveOverlapConflicts} onChange={changeSWebMapServiceParams} />
                    <label htmlFor="isSetAsDefault" className="ml-2">Resolve Overlap Conflicts</label>
                    <InputNumber value={SWebMapServiceParams.fMaxScale} name='fMaxScale' onValueChange={changeSWebMapServiceParams} mode="decimal" />
                </div>
                <div className='form__flex-and-row-between form__items-center'>
                    <div style={{ width: '40%' }} className='form__flex-and-row-between form__items-center'>
                        <label>Transparent Color:</label>
                        <ColorPickerCtrl alpha={true} name='TransparentColor' value={SWebMapServiceParams.TransparentColor} onChange={(event) =>
                            setSWebMapServiceParams({ ...SWebMapServiceParams, TransparentColor: event.value })
                        } />
                    </div>
                    <div style={{ width: '40%' }} className='form__flex-and-row-between form__items-center'>
                        <label>Precision:</label>
                        <InputNumber className='form__medium-width-input' value={SWebMapServiceParams.byTransparentColorPrecision} name='byTransparentColorPrecision' onValueChange={changeSWebMapServiceParams} mode="decimal" />
                    </div>
                </div>
                {props.layerSource == LayerSourceEnum.WCS &&
                    <Fieldset className="form__space-between form__column-fieldset" legend="Select Layer Type">
                        <div >
                            <RadioButton value={enums.LayerNameEnum.NativeServerRaster} onChange={(e) => setSelectedLayerType(e.value)} checked={selectedLayerType == enums.LayerNameEnum.NativeServerRaster} />
                            <label> Raster</label>
                        </div>
                        <div>
                            <RadioButton value={enums.LayerNameEnum.NativeServerDtm} onChange={(e) => setSelectedLayerType(e.value)} checked={selectedLayerType == enums.LayerNameEnum.NativeServerDtm} />
                            <label >DTM</label>
                        </div>
                    </Fieldset>}
            </div>}
            {props.layerSource != LayerSourceEnum.CSW && <div>
                <div>
                    <label >Timeout In Sec </label>
                    <InputNumber className='form__medium-width-input' name='uTimeoutInSec' value={SWebMapServiceParams.uTimeoutInSec} onValueChange={changeSWebMapServiceParams} mode="decimal" />
                    <label >Zero Block Http Codes </label>
                    <InputText className='form__medium-width-input' name='strZeroBlockHttpCodes' value={SWebMapServiceParams.strZeroBlockHttpCodes} onChange={changeSWebMapServiceParams} />
                </div>
                <div>
                    <label>  Styles (Seperated by commas)</label>
                    <InputText style={{ width: `${globalSizeFactor * 1.5 * 60}vh` }} name="strStylesList" value={SWebMapServiceParams.strStylesList} onChange={changeSWebMapServiceParams}></InputText>
                    <Button onClick={() => { setTypeDialog("Select Style") }}  >Select Style</Button>
                </div>
                <div>
                    <label >Layers (Seperated by commas)</label>
                    <InputText style={{ width: `${globalSizeFactor * 1.5 * 60}vh` }} name="strLayersList" value={SWebMapServiceParams.strLayersList} onChange={changeSWebMapServiceParams} ></InputText>
                </div >
                <div >
                    <label >Server URL </label>
                    <InputText style={{ width: `${globalSizeFactor * 1.5 * 60}vh` }} name="strServerURL" value={SWebMapServiceParams.strServerURL} onChange={changeSWebMapServiceParams}></InputText>
                </div>
                <div>
                    <label >Optional User And Password (in user:password format)</label>
                    <InputText style={{ width: `${globalSizeFactor * 1.5 * 60}vh` }} name="strOptionalUserAndPassword" value={SWebMapServiceParams.strOptionalUserAndPassword} onChange={changeSWebMapServiceParams}></InputText>
                </div>
            </div>}
            {props.layerSource != LayerSourceEnum.CSW && <div>
                <div>
                    <label> Image Format</label>
                    {selectedlayers.length == 0 ?
                        <InputText style={{ width: `${globalSizeFactor * 1.5 * 60}vh` }} name="strImageFormat" value={SWebMapServiceParams.strImageFormat} onChange={changeSWebMapServiceParams}></InputText>
                        :
                        <Dropdown
                            name="strImageFormat"
                            itemTemplate={itemTemplate}
                            options={selectedlayers[0].astrImageFormats}
                            onChange={changeSWebMapServiceParams}
                            value={SWebMapServiceParams.strImageFormat} />}
                </div>
                <div className="font__is-as-default-div">
                    <Checkbox style={{ marginTop: `${globalSizeFactor * 0.6}vh` }} name='bTransparent' checked={SWebMapServiceParams.bTransparent} onChange={changeSWebMapServiceParams} />
                    <label htmlFor="isSetAsDefault" className="ml-2">Transparent </label>
                </div>
                <div className="font__is-as-default-div">
                    <Checkbox style={{ marginTop: `${globalSizeFactor * 0.6}vh` }} name='bZeroBlockOnServerException' checked={SWebMapServiceParams.bZeroBlockOnServerException} onChange={changeSWebMapServiceParams} />
                    <label htmlFor="isSetAsDefault" className="ml-2">Zero Block On Server Exception</label>
                </div>
                <div className="font__is-as-default-div">
                    <Checkbox style={{ marginTop: `${globalSizeFactor * 0.6}vh` }} name='bSkipSSLCertificateVerification' checked={SWebMapServiceParams.bSkipSSLCertificateVerification} onChange={changeSWebMapServiceParams} />
                    <label htmlFor="isSetAsDefault" className="ml-2">Skip SSL Certificate Verification </label>
                </div>

            </div>}
            {props.serverLayers[0]?.strLayerType != "WMTS_SERVER_URL" &&
                <div>
                    <Button onClick={() => { setTypeDialog("Server Request Params") }}>Server Request Params</Button>
                    <Button onClick={() => { setTypeDialog("Tiling Scheme") }}>Tiling Scheme</Button>
                    <Fieldset legend="Bounding Box" className='form__column-fieldset'>
                        <Vector3DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false, z: false }} name="MIN:" initValue={SWebMapServiceParams.BoundingBox.MinVertex} saveTheValue={(point) => { setSWebMapServiceParams({ ...SWebMapServiceParams, BoundingBox: { ...SWebMapServiceParams.BoundingBox, MinVertex: point } }) }}></Vector3DFromMap>
                        <Vector3DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false, z: false }} name="MAX:" initValue={SWebMapServiceParams.BoundingBox.MaxVertex} saveTheValue={(point) => { setSWebMapServiceParams({ ...SWebMapServiceParams, BoundingBox: { ...SWebMapServiceParams.BoundingBox, MaxVertex: point } }) }}></Vector3DFromMap>
                    </Fieldset>
                </div>}
            <WebMapServiceType setWebMapService={(Params) => {
                switch (props.layerSource) {
                    case LayerSourceEnum.CSW_WMTS:
                    case LayerSourceEnum.WMTS:
                        setSWMTSParams(Params)
                        break;
                    case LayerSourceEnum.CSW:
                        setSCSWParams(Params)
                        break;
                    case LayerSourceEnum.WMS:
                        setSWMSParams(Params)
                        break;
                    case LayerSourceEnum.WCS:
                        setSWCSParams(Params)
                        break;
                }
            }} serverParamsInit={
                () => {
                    switch (props.layerSource) {
                        case LayerSourceEnum.CSW_WMTS:
                        case LayerSourceEnum.WMTS:
                            return SWMTSParams
                        case LayerSourceEnum.CSW:
                            return SCSWParams
                        case LayerSourceEnum.WMS:
                            return SWMSParams
                        case LayerSourceEnum.WCS:
                            return SWCSParams
                    }
                }
            }
                SWebMapServiceParams={SWebMapServiceParams}
                selectedLayer={selectedlayers[0]}
                serviceType={props.layerSource == LayerSourceEnum.CSW_WMTS ? LayerSourceEnum.WMTS : props.layerSource}
                setCoorSys={(coordSys) => { setSWebMapServiceParams({ ...SWebMapServiceParams, strServerCoordinateSystem: coordSys }) }}></WebMapServiceType>
            <Button onClick={saveLayer}>OK</Button>
            <Dialog onHide={() => { setTypeDialog(null) }} visible={typeDialog != null} header={typeDialog}>
                {dialogComp()}
            </Dialog>
        </div >
    )
}