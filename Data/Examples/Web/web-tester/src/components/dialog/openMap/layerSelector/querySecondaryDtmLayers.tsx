import { useMemo, useRef, useState } from "react";
import { Button } from "primereact/button";
import { useSelector } from "react-redux";
import { Dialog } from "primereact/dialog";

import { LayerDetails, LayerService, Layerslist } from 'mapcore-lib';
import ManualLayers from "./manualLayers";
import ServerRequestParams from "./serverRequestParams";
import { ParametersMode } from "./layersParams";
import SelectExistingLayer from "../../shared/ControlsForMapcoreObjects/layerCtrl/selectExistingLayer";
import { AppState } from "../../../../redux/combineReducer";
import { runCodeSafely } from "../../../../common/services/error-handling/errorHandler";

export default function QuerySecondaryDtmLayers(props: {
    requestParams: MapCore.SMcKeyStringValue[],
    body: MapCore.SMcKeyStringValue,
    getSelectedLayer: (selectedLayers: Layerslist) => void,
    DTMlayerslist: Layerslist,
    getRequestParams?: (requestParams: MapCore.SMcKeyStringValue[], body: MapCore.SMcKeyStringValue) => void,
}) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [DTMlayerslist, setDTMlayerslist] = useState<Layerslist>(props.DTMlayerslist)
    const [requestParamsDialogVisible, setRequestParamsDialogVisible] = useState(false);
    const [requestParams, setRequestParams] = useState<MapCore.SMcKeyStringValue[]>(props.requestParams);
    const [body, setBody] = useState<MapCore.SMcKeyStringValue>(props.body);

    const getAllLayersArr = () => {
        let allMcLayersArr: MapCore.IMcMapLayer[] = [];
        runCodeSafely(() => {
            allMcLayersArr = LayerService.getAllLayers();
            allMcLayersArr = allMcLayersArr.filter(layer => (layer.GetLayerType() == MapCore.IMcNativeServerDtmMapLayer.LAYER_TYPE)
                || (layer.GetLayerType() == MapCore.IMcRawDtmMapLayer.LAYER_TYPE)
                || (layer.GetLayerType() == MapCore.IMcWebServiceDtmMapLayer.LAYER_TYPE)
                || (layer.GetLayerType() == MapCore.IMcNativeDtmMapLayer.LAYER_TYPE))
        }, 'querySecondaryDtm.getAllLayersArr')
        return allMcLayersArr;
    }

    const existingLayerArr = useMemo(() => getAllLayersArr(), []);

    const layersArray2Ref = useRef(null);
    const [serverSelectedLayers, setServerSelectedLayers] = useState<LayerDetails[]>([]);

    const handleOKClick = () => {
        runCodeSafely(() => {
            let layersArr: LayerDetails[] = layersArray2Ref.current.getLayersArray()
            let selectedNewLayers: LayerDetails[] = [...serverSelectedLayers, ...layersArr]
            let newLayerslist = { ...DTMlayerslist, newLayers: selectedNewLayers }
            setDTMlayerslist(newLayerslist)
            props.getSelectedLayer(newLayerslist)
            props.getRequestParams && props.getRequestParams(requestParams, body)
        }, 'querySecondaryDtmLayers.handleOKClick')
    }
    const getRequestParams = (requestParams, body) => {
        runCodeSafely(() => {
            setRequestParamsDialogVisible(false);
            setRequestParams(requestParams);
            setBody(body);
        }, 'SubSpatialQueries.getRequestParams')
    }

    return (<div className="form__column-container">
        <ManualLayers parametersMode={ParametersMode.MinimumParameters} ref={layersArray2Ref} initArrLayers={DTMlayerslist.newLayers} body={props.body} requestParams={props.requestParams} returnLayers={(SL: LayerDetails[]) => {
            setServerSelectedLayers([...serverSelectedLayers, ...SL])
        }} onlyDtm={true}></ManualLayers>
        <SelectExistingLayer initExistingLayerArr={existingLayerArr} initSelectedLayers={DTMlayerslist?.existingLayers} enableCreateNewLayer={false}
            getSelectedLayer={(selectedLayers: { mcLayer: MapCore.IMcMapLayer, isNew: boolean }[]) => setDTMlayerslist({ ...DTMlayerslist, existingLayers: selectedLayers.map(layer => layer.mcLayer) })} />
        <Button className="form__aligm-self-center" onClick={() => { setRequestParamsDialogVisible(true) }} label="Server Request Params" />
        <br />
        <Button style={{ alignSelf: 'flex-end' }} onClick={handleOKClick} label="OK" />

        <Dialog visible={requestParamsDialogVisible} onHide={() => { setRequestParamsDialogVisible(false) }}>
            <ServerRequestParams
                initRequestParams={requestParams}
                initBody={body}
                getRequestParams={getRequestParams}
                showCSWBody={true} />
        </Dialog>
    </div>)
}