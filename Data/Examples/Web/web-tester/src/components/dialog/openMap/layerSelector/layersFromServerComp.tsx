import { useEffect, useState } from 'react';
import { Button } from 'primereact/button';
import { Tooltip } from 'primereact/tooltip';
import { Dialog } from 'primereact/dialog';

import { LayerDetails, LayerSourceEnum, MapCoreData, MapCoreService } from 'mapcore-lib';
import { InputText } from 'primereact/inputtext';
import generalService from '../../../../services/general.service';
import { runCodeSafely } from '../../../../common/services/error-handling/errorHandler';
import LayersSelectionTree from './layersSelectionTree';
import LayersSelectionTable from './layersSelectionTable';


const LayersFromServerComp = (props: { returnLayers: (SL: LayerDetails[]) => void, typeServer: LayerSourceEnum, requestParams: MapCore.SMcKeyStringValue[], body: MapCore.SMcKeyStringValue, onlyDtm: boolean }) => {
    const [displayTreeDialog, setDisplayTreeDialog] = useState<boolean>(false);
    const [selectedLayers, setSelectedLayers] = useState<LayerDetails[]>([]);
    const [serverLayers, setServerLayers] = useState<MapCore.IMcMapLayer.SServerLayerInfo[]>([]);
    const [typeServerEnum, setTypeServerEnum] = useState<LayerSourceEnum>(props.typeServer);
    const [serversUrls, setServersUrls] = useState(
        {
            [LayerSourceEnum.MAPCORE]: "http://localhost:6767/map/opr",
            [LayerSourceEnum.WMTS]: "http://localhost:6767/OGC/WMTS/1.0.0",
            [LayerSourceEnum.WCS]: "http://localhost:8080/geoserver/wcs",
            [LayerSourceEnum.WMS]: "http://localhost:8080/geoserver/wms",
            [LayerSourceEnum.CSW]: ""
            // [LayerSourceEnum.CSW]: "https://catalog.mapcolonies.net/api/raster/v1/csw"
            // [LayerSourceEnum.CSW]: "https://catalog.mapcolonies.net/api/3d/v1/csw"
        });

    useEffect(() => {
        runCodeSafely(() => {
            if (MapCoreData.device == null)
                MapCoreService.initDevice(generalService.mapCorePropertiesBase.deviceInitParams);
        }, "SeveralLayersDialog.useEffect => []")
    }, []);

    const onChooseLayers = (layers: LayerDetails[], isOpenAllLayersAsOne: boolean) => {
        let x = "WCS";
        switch (typeServerEnum) {
            case LayerSourceEnum.WMS:
                x = "WMS";
                break;
            case LayerSourceEnum.WCS:
                x = "WCS";
                break;
            case LayerSourceEnum.WMTS:
            case LayerSourceEnum.CSW_WMTS:
                x = "WMTS";
                break;
            case LayerSourceEnum.CSW:
                x = "CSW";
                break;
            case LayerSourceEnum.MAPCORE:
                break;
        }
        if (isOpenAllLayersAsOne) {
            layers[0].path = layers[0]["layerParams"][x].strLayersList;
            layers = [layers[0]]
        }
        setSelectedLayers([...selectedLayers, ...layers]);
        props.returnLayers(layers)
    }

    let loadServerLayers = (server: LayerSourceEnum) => {
        runCodeSafely(() => {
            setTypeServerEnum(server);
            let url = serversUrls[server];
            if (url) {
                MapCoreService.loadServerLayers((serverLayer: MapCore.IMcMapLayer.SServerLayerInfo[]) => {
                    if (props.onlyDtm)
                        serverLayer = serverLayer.filter(l => l.strLayerType == "MapCoreServerDTM")
                    setServerLayers(serverLayer);
                    setDisplayTreeDialog(true);
                }, url, eval("MapCore.IMcMapLayer.EWebMapServiceType.EWMS_" + server.toString().split("server")[0]), props.body ? [...props.requestParams, props.body] : props.requestParams)
            }
            else
                alert("Server path field is empty, please fill it.")
        }, "SeveralLayersDialog.useEffect => layerParams.urlServer")
    }

    let layersList = () => {
        let str = "";
        selectedLayers?.map((layer: LayerDetails) => {
            str += layer.path + "\n"
        })
        return str;
    }
    return (
        <div>
            <div>
                {typeServerEnum == LayerSourceEnum.MAPCORE && <div className='manual-layers__place-input'>
                    <div> <b >Map Core Layer Server:</b></div>
                    <InputText name='MAPCORE' style={{ width: '80%' }} onChange={(e) => { setServersUrls({ ...serversUrls, [LayerSourceEnum.MAPCORE]: e.target.value.toString() }) }} value={serversUrls[LayerSourceEnum.MAPCORE]}></InputText>
                    <Button label="..." className='buttonAddLayer' onClick={() => { loadServerLayers(LayerSourceEnum.MAPCORE); }}></Button>
                    {selectedLayers && <span className='my-span1' >  <Tooltip content={layersList()} target=".my-span1" /> {selectedLayers.filter(l => l.layerSource == LayerSourceEnum.MAPCORE).length} layers</span>}
                </div>}
                {typeServerEnum == LayerSourceEnum.WMTS && <div className='manual-layers__place-input'>
                    <div> <b >WMTS Layer Server:</b></div>
                    <InputText name='WMTS' style={{ width: '80%' }} onChange={(e) => { setServersUrls({ ...serversUrls, [LayerSourceEnum.WMTS]: e.target.value.toString() }) }} value={serversUrls[LayerSourceEnum.WMTS]}></InputText>
                    <Button label="..." className='buttonAddLayer' onClick={() => { loadServerLayers(LayerSourceEnum.WMTS); }}></Button>
                    {selectedLayers && <span className='my-span2' >  <Tooltip content={layersList()} target=".my-span2" />{selectedLayers.filter(l => l.layerSource == LayerSourceEnum.WMTS).length} layers</span>}
                </div>}
                {typeServerEnum == LayerSourceEnum.WCS && <div className='manual-layers__place-input'>
                    <div> <b >WCS Layer Server:</b></div>
                    <InputText name='WCS' style={{ width: '80%' }} onChange={(e) => { setServersUrls({ ...serversUrls, [LayerSourceEnum.WCS]: e.target.value.toString() }) }} value={serversUrls[LayerSourceEnum.WCS]}></InputText>
                    <Button label="..." className='buttonAddLayer' onClick={() => { loadServerLayers(LayerSourceEnum.WCS); }}></Button>
                    {selectedLayers && <span className='my-span3' >  <Tooltip content={layersList()} target=".my-span3" /> {selectedLayers.filter(l => l.layerSource == LayerSourceEnum.WCS).length} layers</span>}
                </div>}
                {typeServerEnum == LayerSourceEnum.WMS && <div className='manual-layers__place-input'>
                    <div> <b >WMS Layer Server:</b></div>
                    <InputText name='WMS' style={{ width: '80%' }} onChange={(e) => { setServersUrls({ ...serversUrls, [LayerSourceEnum.WMS]: e.target.value.toString() }) }} value={serversUrls[LayerSourceEnum.WMS]}></InputText>
                    <Button label="..." className='buttonAddLayer' onClick={() => { loadServerLayers(LayerSourceEnum.WMS); }}></Button>
                    {selectedLayers && <span className='my-span4' >  <Tooltip content={layersList()} target=".my-span4" />{selectedLayers.filter(l => l.layerSource == LayerSourceEnum.WMS).length} layers</span>}
                </div>}
                {typeServerEnum == LayerSourceEnum.CSW && <div className='manual-layers__place-input'>
                    <div> <b >CSW Layer Server:</b></div>
                    <InputText name='CSW' style={{ width: '80%' }} onChange={(e) => { setServersUrls({ ...serversUrls, [LayerSourceEnum.CSW]: e.target.value.toString() }) }} value={serversUrls[LayerSourceEnum.CSW]}></InputText>
                    <Button
                        label="..." className='buttonAddLayer' onClick={() => { loadServerLayers(LayerSourceEnum.CSW); }}></Button>
                    {selectedLayers && <span className='my-span5' >  <Tooltip content={layersList()} target=".my-span5" /> {selectedLayers.filter(l => ((l.layerSource == LayerSourceEnum.CSW) || (l.layerSource == LayerSourceEnum.CSW_WMTS))).length} layers</span>}
                </div>}
            </div>
            <Dialog className='scroll-dialog-layers-selection-tree' header="Open Map" visible={displayTreeDialog} onHide={() => setDisplayTreeDialog(false)}>
                {
                    typeServerEnum == LayerSourceEnum.MAPCORE ?
                        <LayersSelectionTree onChooseLayers={(layers) => {
                            setSelectedLayers([...selectedLayers, ...layers]);
                            props.returnLayers(layers)
                        }} setDisplayTreeDialog={setDisplayTreeDialog} serverLayers={serverLayers} urlServer={serversUrls[typeServerEnum]}></LayersSelectionTree>
                        :
                        <LayersSelectionTable onChooseLayers={(layers: LayerDetails[], isOpenAllLayersAsOne: boolean) => { onChooseLayers(layers, isOpenAllLayersAsOne) }}
                            setDisplayTreeDialog={setDisplayTreeDialog} serverLayers={serverLayers}
                            urlServer={serversUrls[typeServerEnum]} layerSource={typeServerEnum} requestParams={props.requestParams}></LayersSelectionTable>
                }
            </Dialog>
        </div>
    );
}
export default LayersFromServerComp;