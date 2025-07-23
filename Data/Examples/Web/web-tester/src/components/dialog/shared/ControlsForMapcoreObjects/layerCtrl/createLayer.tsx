import { useEffect, useRef, useState } from "react";
import { Fieldset } from "primereact/fieldset";
import { LayerCreationTargets, LayerDetails, Layerslist, MapCoreData, ViewportParams } from 'mapcore-lib';
import { Button } from "primereact/button";
import { ListBox } from "primereact/listbox";
import { Dialog } from "primereact/dialog";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import ManualLayers from "../../../openMap/layerSelector/manualLayers";
import ServerRequestParams from "../../../openMap/layerSelector/serverRequestParams";
import { ParametersMode } from "../../../openMap/layerSelector/layersParams";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import generalService from "../../../../../services/general.service";


export default function CreateLayer(props: { getSelectedLayer: (selectedLayers: MapCore.IMcMapLayer[]) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [layersDetails, setlayersDetails] = useState<LayerDetails[]>([])
    const [typeDialog, setTypeDialog] = useState("");
    const [requestParams, setRequestParams] = useState<MapCore.SMcKeyStringValue[]>([]);
    const [body, setBody] = useState<MapCore.SMcKeyStringValue>(null);

    const layersArrayRef3 = useRef(null);
    const [serverSelectedLayers, setServerSelectedLayers] = useState<LayerDetails[]>([]);

    const createLayer = () => {
        runCodeSafely(() => {
            let layersArr: LayerDetails[] = layersArrayRef3.current.getLayersArray()
            let selectedNewLayers: LayerDetails[] = [...serverSelectedLayers, ...layersArr]
            let selectedNewLayers2 = [];
            selectedNewLayers.forEach(l => {
                selectedNewLayers2.push(l)
                for (let index = 0; index < l.layerParams?.NO_OfLayers - 1; index++) {
                    selectedNewLayers2.push(l)
                }
            })
            setlayersDetails(selectedNewLayers2)
            if (MapCoreData.device) {
                let layerslist: Layerslist = new Layerslist([], selectedNewLayers2)
                let layers: MapCore.IMcMapLayer[] = generalService.OpenMapService.initLayers(layerslist);
                props.getSelectedLayer(layers)
            }
            else {
                throw Error("You can't create a layer without a device.");
            }
        }, "CreateLayer=> createLayer")
    }

    return (<div>
        <div style={{ display: 'flex' }}>
            <ManualLayers ref={layersArrayRef3} onlyDtm={false} returnLayers={(SL: LayerDetails[]) => { setServerSelectedLayers([...serverSelectedLayers, ...SL]) }} requestParams={requestParams} body={body} parametersMode={ParametersMode.AllParameters} />
        </div>
        <Button onClick={() => { setTypeDialog("Server Request Params") }}>Server Request Params</Button>
        <div style={{ direction: 'rtl', marginTop: `${globalSizeFactor * 2}vh` }}>
            <Button onClick={createLayer}>Create</Button></div>
        <Dialog onHide={() => { setTypeDialog("") }} visible={typeDialog != ""} header={typeDialog} >
            <ServerRequestParams initRequestParams={requestParams} initBody={body} showCSWBody={true}
                getRequestParams={(requestParams, body) => { setTypeDialog(""); setRequestParams(requestParams); setBody(body); }}></ServerRequestParams>
        </Dialog>
    </div>)
}


