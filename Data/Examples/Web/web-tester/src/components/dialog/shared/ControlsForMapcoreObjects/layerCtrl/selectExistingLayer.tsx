import { useEffect, useMemo, useState } from "react";
import { Fieldset } from "primereact/fieldset";
import { ListBox } from "primereact/listbox";
import { Dialog } from "primereact/dialog";
import { useSelector } from "react-redux";
import { Button } from "primereact/button";
import _ from 'lodash'

import './styles/selectExistingLayer.css';
import CreateLayer from "./createLayer";
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { LayerService, TypeToStringService } from "mapcore-lib";

interface BaseSelectExistingLayerProps {
    getSelectedLayer?: (selectedLayers: { mcLayer: MapCore.IMcMapLayer; isNew: boolean; }[]) => void;
    enableCreateNewLayer: boolean;
    initSelectedLayers?: MapCore.IMcMapLayer[];
    initExistingLayerArr?: MapCore.IMcMapLayer[];
    listBoxHeight?: number;
    externalLayers?: MapCore.IMcMapLayer[];
}
interface FinalActionButtonProps extends BaseSelectExistingLayerProps {
    finalActionButton: true;
    finalActionLabel: string;
    handleFinalActionButtonClick: (selectedLayers: { mcLayer: MapCore.IMcMapLayer; isNew: boolean; }[]) => void;
}
interface NoFinalActionButtonProps extends BaseSelectExistingLayerProps {
    finalActionButton?: false | undefined; // optional
}
type SelectExistingLayerProps = FinalActionButtonProps | NoFinalActionButtonProps;

export default function SelectExistingLayer(props: SelectExistingLayerProps) {

    const getAllLayersArr = () => {
        let layersObjectsArr: { index: number, MapLayer: MapCore.IMcMapLayer, name: string }[] = [];
        runCodeSafely(() => {
            let allMcLayersArr: MapCore.IMcMapLayer[] = props.initExistingLayerArr ? props.initExistingLayerArr : LayerService.getAllLayers();
            if (props.externalLayers && props.externalLayers.length > 0) {
                allMcLayersArr = [...allMcLayersArr, ...props.externalLayers];
            }
            let uniqueLayersArr = _.uniqWith(allMcLayersArr, (a: MapCore.IMcMapLayer, b: MapCore.IMcMapLayer) => a == b);

            layersObjectsArr = uniqueLayersArr.map((layer: MapCore.IMcMapLayer, i) => {
                return {
                    index: i + 1,
                    MapLayer: layer,
                    name: i + 1 + ") " + TypeToStringService.getLayerTypeByTypeNumber(layer.GetLayerType())
                }
            })
        }, 'SelectExistingLayer.getAllLayersArr')
        return layersObjectsArr;
    }

    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [selectedlayers, setSelectedlayers] = useState<{ index: number, MapLayer: MapCore.IMcMapLayer, name: string }[]>([]);
    const [newLayers, setNewLayers] = useState<MapCore.IMcMapLayer[]>(null);
    const [allNewLayers, setAllNewLayers] = useState<MapCore.IMcMapLayer[]>([]);
    const [isNewLayers, setIsNewLayers] = useState(false);
    const [openDialog, setOpenDialog] = useState(false);

    let layerArr = useMemo(() => getAllLayersArr(), [newLayers])

    const getLayerWithNewInfoArray = () => {
        let layers: { mcLayer: MapCore.IMcMapLayer; isNew: boolean; }[] = [];
        runCodeSafely(() => {
            layers = selectedlayers.map((selectedlayer: { index: number, MapLayer: MapCore.IMcMapLayer, name: string }) => {
                let isNewLayer = allNewLayers.find(newLayer => newLayer == selectedlayer.MapLayer)
                return { mcLayer: selectedlayer.MapLayer, isNew: isNewLayer ? true : false }
            })
        }, 'SelectExistingLayer.getLayerWithNewInfoArray')
        return layers;
    }
    const setDialogWidth = () => {
        runCodeSafely(() => {
            if (props.finalActionButton) {
                const root = document.documentElement;
                let pixelWidth = window.innerHeight * 0.35 * globalSizeFactor;
                root.style.setProperty('--select-existing-layer-dialog-width', `${pixelWidth}px`);
            }
        }, 'SelectExistingLayer.setDialogWidth')
    }
    //#region UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            setDialogWidth();

            let layersArrObj = getAllLayersArr();
            layerArr = layersArrObj;
            let selectedLayersObjs = [];
            if (props.initSelectedLayers?.length) {
                props.initSelectedLayers.forEach(selectedMcLayer => {
                    let selectedLayerObj = layersArrObj.find(layerObj => layerObj.MapLayer == selectedMcLayer);
                    selectedLayerObj && selectedLayersObjs.push(selectedLayerObj);
                })
            }
            setSelectedlayers(selectedLayersObjs)
        }, 'SelectExistingLayer.UseEffect => mounting')
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            let layers = getLayerWithNewInfoArray();
            props.getSelectedLayer && props.getSelectedLayer(layers)
        }, 'SelectExistingLayer.UseEffect [selectedlayers]')
    }, [selectedlayers])
    useEffect(() => {
        runCodeSafely(() => {
            if (isNewLayers) {
                setIsNewLayers(false)
                let sl = layerArr.filter(item => {
                    return newLayers?.find(NL => NL == item.MapLayer);
                });
                if (!_.isEqual(selectedlayers, sl))
                    setSelectedlayers(sl)
            }
        }, 'SelectExistingLayer.UseEffect [layerArr]')
    }, [layerArr])
    //#endregion

    return (<div className="form__column-container">
        <Fieldset className="form__space-between form__column-fieldset " style={{ marginLeft: '2%' }} legend="Layer">
            <ListBox style={{ height: `${globalSizeFactor * props.listBoxHeight ? props.listBoxHeight : 12}vh`, overflowY: 'auto' }} options={layerArr} optionLabel="name" multiple
                value={selectedlayers} onChange={(event) => setSelectedlayers(event.target.value)}></ListBox>
            {props.enableCreateNewLayer && <Button onClick={() => setOpenDialog(true)}>Create New Layer</Button>}
        </Fieldset>
        {props.finalActionButton && <Button className="form__align-self-end" label={props.finalActionLabel} onClick={e => props.handleFinalActionButtonClick(getLayerWithNewInfoArray())} />}

        <Dialog className="scroll-dialog-coordinate-system" onHide={() => setOpenDialog(false)} visible={openDialog} header="Create Layers" >
            <CreateLayer getSelectedLayer={(layers: MapCore.IMcMapLayer[]) => {
                setNewLayers(layers);
                setAllNewLayers([...allNewLayers, ...layers]);
                setIsNewLayers(true);
                setOpenDialog(false)
            }} />
        </Dialog>
    </div>)
}