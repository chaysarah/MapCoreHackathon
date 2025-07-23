// External libraries
import { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import _ from "lodash";

// UI/component libraries
import { Checkbox } from "primereact/checkbox";
import { Fieldset } from "primereact/fieldset";
import { InputNumber } from "primereact/inputnumber";
import { RadioButton } from "primereact/radiobutton";
import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";

// Project-specific imports
import { enums, IndexingData, LayerDetails, LayerParams, LayerSourceEnum } from 'mapcore-lib';
import ServerRequestParams from "./serverRequestParams";
import WebServerParams from "./SubLayersParams/webServerParams";
import TilingSchemeParams from "./tilingSchemeParams";
import RawVector3DExtrusionParams from "./SubLayersParams/rawVector3DExtrusionParams";
import RawParams from "./SubLayersParams/rawParams";
import Vector3DFromMap from "../../treeView/objectWorldTree/shared/Vector3DFromMap";
import SelectCoordinateSystem from "../../shared/ControlsForMapcoreObjects/coordinateSystemCtrl/selectCoordinateSystem";
import UploadFilesCtrl, { UploadTypeEnum } from "../../shared/uploadFilesCtrl";
import RawVectorParamsCtrl from "../../shared/rawVectorParamsCtrl/rawVectorParamsCtrl";
import InputMaxNumber from "../../../shared/inputMaxNumber";
import ColorPickerCtrl from "../../../shared/colorPicker";
import { runCodeSafely } from "../../../../common/services/error-handling/errorHandler";
import { setActiveLayerDetailsLayerParams } from '../../../../redux/LayerParams/layerParamsAction';
import { AppState } from "../../../../redux/combineReducer";

export enum ParametersMode {
    MinimumParameters,
    AllParameters
}

export default function LayersParams(props: { rawType: enums.LayerNameEnum, layerDetails: LayerDetails, parametersMode: ParametersMode }) {
    const dispatch = useDispatch();
    const layerParams: LayerParams = useSelector((state: AppState) => state.layerParamsReducer.activeLayerDetails?.layerParams);
    const [layerDetails, setLayerDetails] = useState<LayerDetails>();
    const [nonDefaultIndex, setNonDefaultIndex] = useState<boolean>(false);
    const [withIndexing, setWithIndexing] = useState<boolean>(layerParams?.indexingData ? true : false);
    const [disabledClipRect, setDisabledClipRect] = useState(false)
    const [typeDialog, setTypeDialog] = useState("");

    //#region Save Functions
    const saveData = (event: any) => {
        runCodeSafely(() => {
            dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, rawParams: { ...layerParams.rawParams, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value } }))
        }, "objectItemsSelectList.saveData => onChange")
    }
    const saveLayerParamsData = (event: any) => {
        runCodeSafely(() => {
            let LP = { ...layerParams, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value }
            dispatch(setActiveLayerDetailsLayerParams(LP))
        }, "objectItemsSelectList.saveData => onChange")
    }
    //#endregion

    //#region DOM Functions
    const getParamsRawRaster = () => {
        return <div>
            <div >
                <Checkbox onChange={saveData} name="bFillEmptyTilesByLowerResolutionTiles" checked={layerParams?.rawParams.bFillEmptyTilesByLowerResolutionTiles}></Checkbox>
                <label className="ml-2">Fill Empty Tiles By Lower Resolution Tiles</label>
            </div>
            <div >
                <Checkbox onChange={saveData} name="bResolveOverlapConflicts" checked={layerParams?.rawParams.bResolveOverlapConflicts}></Checkbox>
                <label className="ml-2">Resolve Overlap Conflicts</label>
            </div>
            <div >
                <label>Max Scale:</label>
                <InputMaxNumber maxValue={340282346638528860000000000000000000000} name="fMaxScale" value={layerParams?.rawParams.fMaxScale} getUpdatedMaxInput={(value) => { saveData({ target: { name: "fMaxScale", value: value, type: "" } }) }}></InputMaxNumber>
            </div>
            <Fieldset legend="Transparent Color">
                <div style={{ width: '100%' }} className='form__flex-and-row-between form__items-center'>
                    <ColorPickerCtrl style={{ width: '40%' }} alpha={true} name='TransparentColor' value={layerParams?.rawParams?.TransparentColor ?? new MapCore.SMcBColor} onChange={(event) => {
                        dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, rawParams: { ...layerParams.rawParams, TransparentColor: event.value } }))
                    }} />
                    <div style={{ width: '40%' }} className='form__flex-and-row-between form__items-center'>
                        <label>Precision:</label>
                        <InputNumber className='form__medium-width-input' name="byTransparentColorPrecision"
                            value={layerParams?.rawParams?.byTransparentColorPrecision ?? 0} mode="decimal" onValueChange={(event) => {
                                dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, rawParams: { ...layerParams.rawParams, byTransparentColorPrecision: event.target.value } }))
                            }} />
                    </div>
                </div>
            </Fieldset>
            {GetParamsTilingScheme()}
        </div>
    }
    const GetParamsTilingScheme = () => {
        return <>
            <Button onClick={() => { setTypeDialog("Tiling Scheme") }}>Tiling Scheme</Button>
            <Dialog onHide={() => { setTypeDialog("") }} visible={typeDialog != ""} header={typeDialog} >
                <TilingSchemeParams getTilingScheme={(tilingScheme) => {
                    setTypeDialog("");
                    //  setLayerParams({ ...layerParams, rawParams: { ...layerParams.rawParams, pTilingScheme: tilingScheme } })
                    dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, rawParams: { ...layerParams.rawParams, pTilingScheme: tilingScheme } }))
                }}></TilingSchemeParams>
            </Dialog>
        </>
    }
    const GetParamsRequestParams = () => {
        return <>
            <Button onClick={() => { setTypeDialog("Request Params") }}> Request Params</Button>
            <Dialog onHide={() => { setTypeDialog("") }} visible={typeDialog != ""} header={typeDialog} >
                <ServerRequestParams initRequestParams={layerParams?.requestParams} getRequestParams={function (requestParams: MapCore.SMcKeyStringValue[]): void {
                    dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, requestParams: requestParams }))
                    setTypeDialog(null);
                }} showCSWBody={false}></ServerRequestParams>
            </Dialog>
        </>
    }
    const GetParamsRaw3DModel = () => {
        return <Fieldset className="form__space-between form__column-fieldset " legend="Raw 3D Model">
            <div >
                <Checkbox onChange={saveLayerParamsData} name="orthometricHeights" checked={layerParams?.orthometricHeights}></Checkbox>
                <label className="ml-2">Orthometric Heights </label>
            </div>
            <Vector3DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false, z: false }} name="Position Offset:" disabledPointFromMap={false}
                initValue={layerParams?.positionOffset} saveTheValue={(point) => { dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, positionOffset: point })) }}></Vector3DFromMap>

            <Fieldset className="form__row-fieldset" legend={<> <Checkbox onChange={(e) => {
                setWithIndexing(e.target.checked);
                if (e.target.checked == false)
                    dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, indexingData: null }))
                else
                    dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, indexingData: new IndexingData("...", true) }))
            }} checked={withIndexing}></Checkbox>
                <label className="ml-2">With Indexing </label></>}>
                <div className={`${!withIndexing && "form__disabled"}`}>
                    <div >
                        <RadioButton onChange={e => {
                            if (!layerDetails?.path)
                                alert("First you must select a layer.")
                            else {
                                setNonDefaultIndex(false);
                                if (e.target.checked == false)
                                    dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, indexingData: new IndexingData("...", true) }))
                            }
                        }} checked={!nonDefaultIndex}></RadioButton>
                        <label className="ml-2"> Default Index Directory: </label>
                        {!nonDefaultIndex && <UploadFilesCtrl isDirectoryUpload={true} accept='directory' uploadOptions={[UploadTypeEnum.upload]}
                            existLocationPath={layerDetails?.path.substring(0, layerDetails?.path.indexOf('/'))} getVirtualFSPath={(virtualFSPath, selectedOption) => {
                                let lp = { ...layerParams, indexingData: new IndexingData("", true) }
                                dispatch(setActiveLayerDetailsLayerParams(lp))
                            }}></UploadFilesCtrl>
                        }
                    </div>
                    <div >
                        <RadioButton onChange={e => {
                            setNonDefaultIndex(true);
                            if (e.target.checked == false)
                                dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, indexingData: new IndexingData("...", false) }))
                        }} checked={nonDefaultIndex}></RadioButton>
                        <label className="ml-2">Non Default Index Directory: </label>
                        {nonDefaultIndex && <UploadFilesCtrl isDirectoryUpload={true} accept='directory' uploadOptions={[UploadTypeEnum.upload]}
                            getVirtualFSPath={(virtualFSPath, selectedOption) => { dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, indexingData: new IndexingData(virtualFSPath, false) })) }}></UploadFilesCtrl>}
                    </div>
                </div>
            </Fieldset>
            <Fieldset legend="Without Indexing">
                <div className={`${withIndexing && "form__disabled"}`}>
                    <div >
                        <label>Target Highest Resolution:</label>
                        <InputNumber name="targetHighestResolution" minFractionDigits={0} maxFractionDigits={5} value={layerParams?.targetHighestResolution ?? 0} onValueChange={saveLayerParamsData} mode="decimal" />
                    </div>
                    {props.parametersMode == ParametersMode.AllParameters && <SelectCoordinateSystem getSelectedCorSys={(coor: MapCore.IMcGridCoordinateSystem) => { dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, targetCoordSys: coor })) }} header="Target Coordinate System" initSelectedCorSys={layerParams?.targetCoordSys}></SelectCoordinateSystem>}
                    <Fieldset
                        legend={<><Checkbox onChange={(e) => { setDisabledClipRect(e.checked) }} checked={disabledClipRect}></Checkbox><label>Clipping Rectangle:(In Target Grig Coordinate System)</label></>}>
                        <div className={`${!disabledClipRect && "form__disabled"}`} >
                            <Vector3DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false, z: false }} name="MIN:"
                                initValue={layerParams?.clipRect?.MinVertex} saveTheValue={(point) => { dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, clipRect: { ...layerParams.clipRect, MinVertex: point } })) }}></Vector3DFromMap>
                            <Vector3DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false, z: false }} name="MAX:"
                                initValue={layerParams?.clipRect?.MaxVertex} saveTheValue={(point) => { dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, clipRect: { ...layerParams.clipRect, MaxVertex: point } })) }}></Vector3DFromMap>
                        </div>
                    </Fieldset>
                    {GetParamsRequestParams()}
                </div>
            </Fieldset>
        </Fieldset>
    }
    const getParamsRaw_NativeVector3DExtrusion = () => {
        return <div>
            <label >Extrusion Height Max Addition</label>
            <InputNumber value={layerParams?.extrusionHeight ?? 0} name="extrusionHeight" onValueChange={saveLayerParamsData} mode="decimal" />
        </div>
    }
    const getParamsEnhanceBorderOverlap = () => {
        return <div>
            <Checkbox name="enhanceBorder" onChange={saveLayerParamsData} checked={layerParams?.enhanceBorder}></Checkbox>
            <label className="ml-2">Enhance Border Overlap</label>
        </div>
    }
    const getParamsThereAreMissingFiles_FirstLowerQualityLevel = () => {
        return <><div>
            <Checkbox name="thereAreMissingFiles" onChange={saveLayerParamsData} checked={layerParams?.thereAreMissingFiles}></Checkbox>
            <label className="ml-2">There Are Missing Files</label>
        </div>
            <div>
                <label className="ml-2">First Lower Quality Level</label>
                <InputNumber name="firstLowerQualityLevel" value={layerParams?.firstLowerQualityLevel ?? 0} onValueChange={saveLayerParamsData} mode="decimal" />
            </div>
        </>
    }
    const getParamsNumLevelsToIgnore = () => {
        return <div>
            <label className="ml-2">Num Levels To Ignore</label>
            <InputNumber name="uNumLevelsToIgnore" value={layerParams?.numLevelsToIgnore ?? 0} onValueChange={saveLayerParamsData} mode="decimal" />
        </div>
    }
    const getParamsNO_OfLayers = () => {
        return <div>
            {props.parametersMode == ParametersMode.AllParameters &&
                <><label className="ml-2">NO. Of Layers</label>
                    <InputNumber name="NO_OfLayers" value={layerParams?.NO_OfLayers ?? 0} onValueChange={saveLayerParamsData} mode="decimal" /></>}
        </div>
    }
    const paramsPerLayerType = () => {
        switch (props.rawType) {
            case enums.LayerNameEnum.RawRaster:
            case enums.LayerNameEnum.RawTraversability:
            case enums.LayerNameEnum.RawMaterial:
            case enums.LayerNameEnum.RawDtm:
                return <>  {getParamsNO_OfLayers()}    {getParamsEnhanceBorderOverlap()}{getParamsRawRaster()}
                    <br></br> <u> <label>Raw Params:</label></u>
                    <RawParams imageCoordinateSystem={layerParams?.imageCoordinateSystem} rawParams={layerParams?.rawParams} getRawParams={(rawParams: MapCore.IMcMapLayer.SRawParams, imageCoordinateSystem: boolean) => {
                        dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, rawParams: rawParams, imageCoordinateSystem: imageCoordinateSystem }))
                    }} rawType={props.rawType} parametersMode={props.parametersMode}  ></RawParams>
                </>
            case enums.LayerNameEnum.Raw3DModel:
                return <> {getParamsNO_OfLayers()}    {GetParamsRaw3DModel()}</>
            case enums.LayerNameEnum.RawVector:
                return <>
                    {getParamsNO_OfLayers()}
                    {GetParamsTilingScheme()}
                    <SelectCoordinateSystem getSelectedCorSys={(coor: MapCore.IMcGridCoordinateSystem) => { dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, targetCoordSys: coor })) }} header="Target Coordinate System" initSelectedCorSys={layerParams?.targetCoordSys}></SelectCoordinateSystem>
                    <RawVectorParamsCtrl getRawVectorParams={(rawVectorParams: MapCore.IMcRawVectorMapLayer.SParams) => {
                        dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, rawVectorParams: rawVectorParams }))
                    }} defaultRawVectorParams={layerParams?.rawVectorParams}></RawVectorParamsCtrl>
                </>
            case enums.LayerNameEnum.RawVector3DExtrusion:
                return <> {getParamsNO_OfLayers()} {getParamsRaw_NativeVector3DExtrusion()}
                    <RawVector3DExtrusionParams></RawVector3DExtrusionParams></>
            case enums.LayerNameEnum.NativeVector3DExtrrusion:
                return <> {getParamsNO_OfLayers()} {getParamsNumLevelsToIgnore()}
                    {getParamsRaw_NativeVector3DExtrusion()} </>
            case enums.LayerNameEnum.NativeRaster:
            case enums.LayerNameEnum.NativeHeat:
                return <>  {getParamsNO_OfLayers()} {getParamsEnhanceBorderOverlap()}
                    {getParamsNumLevelsToIgnore()}
                    {getParamsThereAreMissingFiles_FirstLowerQualityLevel()}</>
            case enums.LayerNameEnum.NativeDtm:
            case enums.LayerNameEnum.Native3DModel:
                return <>  {getParamsNO_OfLayers()} {getParamsNumLevelsToIgnore()}</>
            case enums.LayerNameEnum.NativeTraversability:
            case enums.LayerNameEnum.NativeMaterial:
                return <> {getParamsNO_OfLayers()}
                    {getParamsNumLevelsToIgnore()}
                    {getParamsThereAreMissingFiles_FirstLowerQualityLevel()}
                </>
            case enums.LayerNameEnum.WebServiceRaster:
            case enums.LayerNameEnum.WebServiceDTM:
                return <>
                    {getParamsNO_OfLayers()}{getParamsEnhanceBorderOverlap()}{getParamsRawRaster()}
                    <br></br> <u> <label>Web Server Params:</label></u>
                    <WebServerParams webServerParams={layerParams?.WMTS}
                        layerSource={LayerSourceEnum.WMTS}
                        getWebServerParams={(webServerParams_: any) => {
                            let x = { ...webServerParams_ }
                            dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, WMTS: x }))
                        }}
                    ></WebServerParams>
                </>
            case null:
                return
        }
        return null
    }
    //#endregion

    return (<div>
        {paramsPerLayerType()}
    </div>
    );
}
