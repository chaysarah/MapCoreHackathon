import { useState } from "react";
import { Fieldset } from "primereact/fieldset";
import { Button } from "primereact/button";
import { useSelector } from "react-redux";
import { Checkbox } from "primereact/checkbox";

import { MapCoreData, MapCoreService } from 'mapcore-lib';
import SelectCoordinateSystem from "../coordinateSystemCtrl/selectCoordinateSystem";
import SelectExistingLayer from "../layerCtrl/selectExistingLayer";
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";

export default function CreateTerrain(props: { getNewTerrain: (newTerrain: MapCore.IMcMapTerrain) => void, saveStandAlone: boolean }) {
    const [selectedCorSys, setSelectedCorSys] = useState<MapCore.IMcGridCoordinateSystem>(null);
    const [selectedLayers, setSelectedLayers] = useState<{ mcLayer: MapCore.IMcMapLayer, isNew: boolean }[]>([]);
    const [displayItemsAttachedToStaticObjectsWithoutDtm, setDisplayItemsAttachedToStaticObjectsWithoutDtm] = useState<boolean>(false);
    const [newTerrain, setNewTerrain] = useState<MapCore.IMcMapTerrain>(null);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const handleCreateTerrainClick = () => {
        runCodeSafely(() => {
            let mcSelectedLayers = selectedLayers.map(layer => layer.mcLayer)
            let terrain = MapCoreService.createTerrain(mcSelectedLayers, selectedCorSys, displayItemsAttachedToStaticObjectsWithoutDtm);

            selectedLayers.forEach(currentLayer => {
                let isStandAloneExist = MapCoreData.standAloneLayers.find(layer => layer == currentLayer.mcLayer);
                if (isStandAloneExist) {
                    MapCoreData.standAloneLayers = MapCoreData.standAloneLayers.filter(layer => layer != currentLayer.mcLayer);
                    runMapCoreSafely(() => { isStandAloneExist.Release() }, "CreateTerrain.handleCreateTerrainClick => IMcBase.Release", true)
                }
                else if (!props.saveStandAlone && currentLayer.isNew) {//Handles the case of layer that was filtered from MapCoreData.standAloneLayers after its creation (Spatial queries, e.g.)
                    runMapCoreSafely(() => { currentLayer.mcLayer.Release() }, "CreateTerrain.handleCreateTerrainClick => IMcBase.Release", true)
                }
            })
            setNewTerrain(terrain);
            props.getNewTerrain(terrain)
        }, "CreateTerrain.handleCreateTerrainClick")
    }
    const getSelectedLayers = (selectedLayers: { mcLayer: MapCore.IMcMapLayer, isNew: boolean }[]) => {
        runCodeSafely(() => {
            selectedLayers.forEach(currentLayer => {
                if (!props.saveStandAlone && currentLayer.isNew) {
                    MapCoreData.standAloneLayers = MapCoreData.standAloneLayers.filter(layer => layer != currentLayer.mcLayer);
                }
            })
            setSelectedLayers(selectedLayers)
        }, 'CreateTerrain.getSelectedLayers')
    }
    const getExternalLayers = () => {
        let newLayers: MapCore.IMcMapLayer[] = [];
        runCodeSafely(() => {
            selectedLayers.forEach(layerObj => {
                if (!props.saveStandAlone && layerObj.isNew) {
                    newLayers = [...newLayers, layerObj.mcLayer];
                }
            })
        }, 'CreateTerrain.getExternalLayers')
        return newLayers;
    }

    return (<div >
        <Fieldset className="form__space-between form__column-fieldset">
            <div>
                <SelectCoordinateSystem getSelectedCorSys={(selectedCorSys: MapCore.IMcGridCoordinateSystem) => { setSelectedCorSys(selectedCorSys) }} header="Grid Coordinate System"></SelectCoordinateSystem>
                <SelectExistingLayer getSelectedLayer={getSelectedLayers} externalLayers={getExternalLayers()} listBoxHeight={14} enableCreateNewLayer={true} />
            </div>
            <div>
                <Checkbox onChange={e => { setDisplayItemsAttachedToStaticObjectsWithoutDtm(e.checked) }} checked={displayItemsAttachedToStaticObjectsWithoutDtm}></Checkbox>
                <label className="ml-2">Display Items Attached To Static Objects Without Dtm</label>
            </div>
        </Fieldset>
        <div style={{ direction: 'rtl', marginTop: `${globalSizeFactor * 2}vh` }}> <Button onClick={handleCreateTerrainClick}>Create Terrain</Button></div>

    </div>)
}