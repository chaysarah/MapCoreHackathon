// External libraries
import { useState, useEffect, ReactElement } from 'react';
import { useDispatch, useSelector } from 'react-redux';

// UI/component libraries
import { Button } from 'primereact/button';
import { Fieldset } from 'primereact/fieldset';
import { ListBox } from 'primereact/listbox';
import { InputNumber } from 'primereact/inputnumber';
import { Checkbox } from 'primereact/checkbox';
import { InputText } from 'primereact/inputtext';
import { Dropdown } from 'primereact/dropdown';
import { Dialog } from 'primereact/dialog';
import { RadioButton } from 'primereact/radiobutton';
import { LayerSourceEnum } from 'mapcore-lib';
import { enums } from 'mapcore-lib';

// Project-specific imports
import WebMapServiceType from '../webMapServiceType';
import ServerRequestParams from '../serverRequestParams';
import TilingSchemeParams from '../tilingSchemeParams';
import SelectCoordinateSystem from '../../../shared/ControlsForMapcoreObjects/coordinateSystemCtrl/selectCoordinateSystem';
import Vector3DFromMap from '../../../treeView/objectWorldTree/shared/Vector3DFromMap';
import { AppState } from '../../../../../redux/combineReducer';
import ColorPickerCtrl from '../../../../shared/colorPicker';

export default function WebServerParams(props: {
    layerSource: LayerSourceEnum;
    getWebServerParams: (webServerParams: any) => void,
    webServerParams: MapCore.IMcMapLayer.SWebMapServiceParams
}) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let [SWebMapServiceParams, setSWebMapServiceParams] = useState<MapCore.IMcMapLayer.SWebMapServiceParams>(props.webServerParams)
    let [SWMSParams, setSWMSParams] = useState<MapCore.IMcMapLayer.SWMSParams>(new MapCore.IMcMapLayer.SWMSParams())
    let [SWCSParams, setSWCSParams] = useState<MapCore.IMcMapLayer.SWCSParams>(new MapCore.IMcMapLayer.SWCSParams())
    let [SWMTSParams, setSWMTSParams] = useState<MapCore.IMcMapLayer.SWMTSParams>(new MapCore.IMcMapLayer.SWMTSParams())
    let [SCSWParams, setSCSWParams] = useState({ OrthometricHeights: false })
    let [selectedlayers, setSelectedlayers] = useState<MapCore.IMcMapLayer.SServerLayerInfo[]>([])
    let [styles, setStyles] = useState(["Default"])
    let [isOpenAllLayersAsOne, setIsOpenAllLayersAsOne] = useState(false)
    let [selectedLayerType, setSelectedLayerType] = useState(enums.LayerNameEnum.NativeServerRaster)
    const [requestParams, setRequestParams] = useState<MapCore.SMcKeyStringValue[]>(SWebMapServiceParams?.aRequestParams);
    const [tilingScheme, setTilingScheme] = useState<MapCore.IMcMapLayer.STilingScheme>(null);
    let [typeDialog, setTypeDialog] = useState(null)

    useEffect(() => {
        setSWebMapServiceParams(props.webServerParams);
        setSWMTSParams(props.webServerParams as any)
    }, [props.webServerParams])

    useEffect(() => {
        props.getWebServerParams({
            ...SWebMapServiceParams,
            bUseServerTilingScheme: SWMTSParams.bUseServerTilingScheme,
            strInfoFormat: SWMTSParams.strInfoFormat,
            bExtendBeyondDateLine: SWMTSParams.bExtendBeyondDateLine,
            eCapabilitiesBoundingBoxAxesOrder: SWMTSParams.eCapabilitiesBoundingBoxAxesOrder,
        })
    }, [SWebMapServiceParams, SWMTSParams])
    // To Do useEffect for  all server
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

        }
        return null
    }

    const changeSWebMapServiceParams = (event: any) => {
        setSWebMapServiceParams({ ...SWebMapServiceParams, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
    }
    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.6 * globalSizeFactor;
        root.style.setProperty('--layers-selection-table-dialog-width', `${pixelWidth}px`);
    }, [])


    useEffect(() => { setSWebMapServiceParams({ ...SWebMapServiceParams, aRequestParams: requestParams }) }
        , [requestParams])

    const itemTemplate = (option: any) => {
        return <span>{option}</span>;
    };

    return (
        <div>
            {props.layerSource != LayerSourceEnum.CSW && <div className="font__is-as-default-div">
                <Checkbox style={{ marginTop: `${globalSizeFactor * 0.6}vh` }} name='isSetAsDefault' onChange={(e) => { setIsOpenAllLayersAsOne(e.target.checked) }} checked={isOpenAllLayersAsOne} />
                <label htmlFor="isSetAsDefault" className="ml-2" >Open All Layers As One</label>
            </div>}
            <SelectCoordinateSystem getSelectedCorSys={(coor: MapCore.IMcGridCoordinateSystem) => { setSWebMapServiceParams({ ...SWebMapServiceParams, pCoordinateSystem: coor }) }} header="Grid Coordinate System" initSelectedCorSys={SWebMapServiceParams?.pCoordinateSystem}></SelectCoordinateSystem>
            {props.layerSource != LayerSourceEnum.CSW && <div>
                <div className="font__is-as-default-div">
                    <Checkbox style={{ marginTop: `${globalSizeFactor * 0.6}vh` }} name='bEnhanceBorderOverlap' checked={SWebMapServiceParams?.bEnhanceBorderOverlap} onChange={changeSWebMapServiceParams} />
                    <label htmlFor="isSetAsDefault" className="ml-2">Enhance Border Overlap</label>
                </div>
                <div className="font__is-as-default-div">
                    <Checkbox style={{ marginTop: `${globalSizeFactor * 0.6}vh` }} name='bFillEmptyTilesByLowerResolutionTiles' checked={SWebMapServiceParams?.bFillEmptyTilesByLowerResolutionTiles} onChange={changeSWebMapServiceParams} />
                    <label htmlFor="isSetAsDefault" className="ml-2">Fill Empty Tiles By Lower Resolution Tiles</label>
                </div>
                <div className="font__is-as-default-div">
                    <Checkbox style={{ marginTop: `${globalSizeFactor * 0.6}vh` }} name='bResolveOverlapConflicts' checked={SWebMapServiceParams?.bResolveOverlapConflicts} onChange={changeSWebMapServiceParams} />
                    <label htmlFor="isSetAsDefault" className="ml-2">Resolve Overlap Conflicts</label>
                    <InputNumber value={SWebMapServiceParams?.fMaxScale ?? null} name='fMaxScale' onValueChange={changeSWebMapServiceParams} mode="decimal" />
                </div>
                <div className='form__flex-and-row-between form__items-center'>
                    <div className='form__flex-and-row-between form__items-center'>
                        <label >Transparent Color:</label>
                        <ColorPickerCtrl style={{ width: '40%' }} alpha={true} name='TransparentColor' value={SWebMapServiceParams?.TransparentColor} onChange={(event) =>
                            setSWebMapServiceParams({ ...SWebMapServiceParams, TransparentColor: event.value })
                        } />
                    </div>
                    <div style={{ width: '40%' }} className='form__flex-and-row-between form__items-center'>
                        <label>Precision:</label>
                        <InputNumber className='form__medium-width-input' value={SWebMapServiceParams?.byTransparentColorPrecision ?? null} name='byTransparentColorPrecision' onValueChange={changeSWebMapServiceParams} mode="decimal" />
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
                <div className='object-props__flex-and-row-between'>
                    <label >Timeout In Sec </label>
                    <InputNumber className='form__medium-width-input' name='uTimeoutInSec' value={SWebMapServiceParams?.uTimeoutInSec ?? null} onValueChange={changeSWebMapServiceParams} mode="decimal" />
                </div>
                <div className='object-props__flex-and-row-between'>
                    <label >Zero Block Http Codes </label>
                    <InputText className='form__medium-width-input' name='strZeroBlockHttpCodes' value={SWebMapServiceParams?.strZeroBlockHttpCodes} onChange={changeSWebMapServiceParams} />
                </div>
                <div className='object-props__flex-and-row-between'>
                    <label>  Styles (Seperated by commas)</label>
                    <InputText name="strStylesList" value={SWebMapServiceParams?.strStylesList} onChange={changeSWebMapServiceParams}></InputText>
                </div>
                <div className='object-props__flex-and-row-between'>
                    <label >Layers (Seperated by commas)</label>
                    <InputText name="strLayersList" value={SWebMapServiceParams?.strLayersList} onChange={changeSWebMapServiceParams} ></InputText>
                </div >
                <div className='object-props__flex-and-row-between'>
                    <label >Server URL </label>
                    <InputText name="strServerURL" value={SWebMapServiceParams?.strServerURL} onChange={changeSWebMapServiceParams}></InputText>
                </div>
                <div className='object-props__flex-and-row-between'>
                    <label >Optional User And Password (in user:password format)</label>
                    <InputText name="strOptionalUserAndPassword" value={SWebMapServiceParams?.strOptionalUserAndPassword} onChange={changeSWebMapServiceParams}></InputText>
                </div>
            </div>}
            {props.layerSource != LayerSourceEnum.CSW && <div>
                <div className='object-props__flex-and-row-between'>
                    <label> Image Format</label>
                    {selectedlayers.length == 0 ?
                        <InputText name="strImageFormat" value={SWebMapServiceParams?.strImageFormat} onChange={changeSWebMapServiceParams}></InputText>
                        :
                        <Dropdown
                            name="strImageFormat"
                            itemTemplate={itemTemplate}
                            options={selectedlayers[0].astrImageFormats}
                            onChange={changeSWebMapServiceParams}
                            value={SWebMapServiceParams?.strImageFormat} />}
                </div>
                <div className="font__is-as-default-div">
                    <Checkbox style={{ marginTop: `${globalSizeFactor * 0.6}vh` }} name='bTransparent' checked={SWebMapServiceParams?.bTransparent} onChange={changeSWebMapServiceParams} />
                    <label htmlFor="isSetAsDefault" className="ml-2">Transparent </label>
                </div>
                <div className="font__is-as-default-div">
                    <Checkbox style={{ marginTop: `${globalSizeFactor * 0.6}vh` }} name='bZeroBlockOnServerException' checked={SWebMapServiceParams?.bZeroBlockOnServerException} onChange={changeSWebMapServiceParams} />
                    <label htmlFor="isSetAsDefault" className="ml-2">Zero Block On Server Exception</label>
                </div>
                <div className="font__is-as-default-div">
                    <Checkbox style={{ marginTop: `${globalSizeFactor * 0.6}vh` }} name='bSkipSSLCertificateVerification' checked={SWebMapServiceParams?.bSkipSSLCertificateVerification} onChange={changeSWebMapServiceParams} />
                    <label htmlFor="isSetAsDefault" className="ml-2">Skip SSL Certificate Verification </label>
                </div>

            </div>}
            {props.layerSource != LayerSourceEnum.WMTS &&
                <div>
                    <Button onClick={() => { setTypeDialog("Server Request Params") }}>Server Request Params</Button>
                    <Button onClick={() => { setTypeDialog("Tiling Scheme") }}>Tiling Scheme</Button>
                    <Fieldset legend="Bounding Box" className='form__column-fieldset'>
                        <Vector3DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false, z: false }} name="MIN:" initValue={SWebMapServiceParams?.BoundingBox.MinVertex} saveTheValue={(point) => { setSWebMapServiceParams({ ...SWebMapServiceParams, BoundingBox: { ...SWebMapServiceParams?.BoundingBox, MinVertex: point } }) }}></Vector3DFromMap>
                        <Vector3DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false, z: false }} name="MAX:" initValue={SWebMapServiceParams?.BoundingBox.MaxVertex} saveTheValue={(point) => { setSWebMapServiceParams({ ...SWebMapServiceParams, BoundingBox: { ...SWebMapServiceParams?.BoundingBox, MaxVertex: point } }) }}></Vector3DFromMap>
                    </Fieldset>
                </div>}
            <h3>
            </h3>
            {<WebMapServiceType setWebMapService={(Params) => {
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
            }
            <Dialog onHide={() => { setTypeDialog(null) }} visible={typeDialog != null} header={typeDialog}>
                {dialogComp()}
            </Dialog>
        </div >
    )
}