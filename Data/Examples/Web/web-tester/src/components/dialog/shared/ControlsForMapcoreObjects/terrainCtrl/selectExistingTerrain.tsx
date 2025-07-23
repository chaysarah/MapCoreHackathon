import { useEffect, useState } from "react";
import { Fieldset } from "primereact/fieldset";
import { Button } from "primereact/button";
import { ListBox } from "primereact/listbox";
import { Dialog } from "primereact/dialog";
import { useSelector } from "react-redux";
import _ from 'lodash';

import { MapCoreData, ViewportData } from 'mapcore-lib';
import './styles/selectExistingTerrain.css';
import CreateTerrain from "./createTerrain";
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";

interface BaseSelectExistingTerrainProps {
    getSelectedTerrains?: (selectedTerrains: { mcTerrain: MapCore.IMcMapTerrain, isNew: boolean }[]) => void,
    saveStandAlone: boolean
    externalTerrains?: MapCore.IMcMapTerrain[],
}
interface FinalActionButtonProps extends BaseSelectExistingTerrainProps {
    finalActionButton: true;
    finalActionLabel: string;
    handleFinalActionButtonClick: (selectedTerrains: { mcTerrain: MapCore.IMcMapTerrain; isNew: boolean; }[]) => void;
}
interface NoFinalActionButtonProps extends BaseSelectExistingTerrainProps {
    finalActionButton?: false | undefined; // optional
}
type SelectExistingTerrainProps = FinalActionButtonProps | NoFinalActionButtonProps;

export default function SelectExistingTerrains(props: SelectExistingTerrainProps) {
    const [selectedTerrains, setSelectedTerrains] = useState<{ index: number, terrain: MapCore.IMcMapTerrain, name: string }[]>([]);
    const [isNewTerrains, setIsNewTerrains] = useState<boolean>(false);
    const [newTerrain, setNewTerrain] = useState<MapCore.IMcMapTerrain>(null);
    const [allNewTerrains, setAllNewTerrains] = useState<MapCore.IMcMapTerrain[]>([]);

    const getTerrainWithNewInfoArray = () => {
        let terrains: { mcTerrain: MapCore.IMcMapTerrain; isNew: boolean; }[] = [];
        runCodeSafely(() => {
            terrains = selectedTerrains.map((selectedTerrain: { index: number, terrain: MapCore.IMcMapTerrain, name: string }) => {
                let isNewLayer = allNewTerrains.find(newTerrain => newTerrain == selectedTerrain.terrain)
                return { mcTerrain: selectedTerrain.terrain, isNew: isNewLayer ? true : false }
            })
        }, 'SelectExistingLayer.getLayerWithNewInfoArray')
        return terrains;
    }

    const getAllTerrains = () => {
        let terrainsObjectsArr: { index: number, terrain: MapCore.IMcMapTerrain, name: string }[] = [];
        runCodeSafely(() => {
            let viewportTerrains: MapCore.IMcMapTerrain[] = [];
            MapCoreData.viewportsData.forEach((viewportData: ViewportData) => {
                let terrains = viewportData.viewport ? viewportData.viewport.GetTerrains() : [];
                viewportTerrains = [...viewportTerrains, ...terrains];
            })
            viewportTerrains = [...viewportTerrains, ...MapCoreData.standAloneTerrains];
            if (props.externalTerrains) {
                viewportTerrains = [...viewportTerrains, ...props.externalTerrains];
            }
            const uniqueViewportTerrains = _.uniqWith(viewportTerrains, (a, b) => a == b);
            terrainsObjectsArr = uniqueViewportTerrains.map((terrain: MapCore.IMcMapTerrain, i) => {
                return {
                    index: i + 1,
                    terrain: terrain,
                    name: i + 1 + ") " + "Terrain"
                }
            })
        }, 'selectExistingTerrain.getAllTerrains')
        return terrainsObjectsArr
    }

    const existingTerrainArr = getAllTerrains()
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [openDialog, setOpenDialog] = useState(false);

    useEffect(() => {
        runCodeSafely(() => {
            if (props.finalActionButton) {
                const root = document.documentElement;
                let pixelWidth = window.innerHeight * 0.35 * globalSizeFactor;
                root.style.setProperty('--select-existing-terrain-dialog-width', `${pixelWidth}px`);
            }
        }, 'selectExistingTerrain.useEffect')
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            let terrains = getTerrainWithNewInfoArray();
            props.getSelectedTerrains && props.getSelectedTerrains(terrains);
        }, 'selectExistingTerrain.useEffect[selectedTerrains]')
    }, [selectedTerrains])

    useEffect(() => {
        runCodeSafely(() => {
            if (isNewTerrains) {
                setIsNewTerrains(false);
                let t = existingTerrainArr.find(t => t.terrain == newTerrain)
                if (t)
                    setSelectedTerrains([t])
            }
        }, 'selectExistingTerrain.useEffect[existingTerrainArr]')
    }, [existingTerrainArr])

    return (<div className="form__column-container">
        <Fieldset className="form__space-between form__column-fieldset " style={{ marginLeft: '2%' }} legend="Terrain">
            <ListBox options={existingTerrainArr} optionLabel="name" multiple
                value={selectedTerrains} onChange={(event) => setSelectedTerrains(event.target.value)} />
            <Button onClick={() => setOpenDialog(true)}>Create New Terrain</Button>
        </Fieldset>
        {props.finalActionButton && <Button className="form__align-self-end" label={props.finalActionLabel} onClick={e => props.handleFinalActionButtonClick(getTerrainWithNewInfoArray())} />}

        <Dialog className="scroll-dialog-coordinate-system" onHide={() => setOpenDialog(false)} visible={openDialog} header="Create Terrain" >
            <CreateTerrain getNewTerrain={(newTerrain: MapCore.IMcMapTerrain) => {
                setIsNewTerrains(true);
                setNewTerrain(newTerrain);
                setAllNewTerrains([...allNewTerrains, newTerrain]);
                setOpenDialog(false);
            }} saveStandAlone={props.saveStandAlone} />
        </Dialog>

    </div>)
}