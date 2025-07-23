import { ListBox } from "primereact/listbox";
import { useDispatch, useSelector } from "react-redux";
import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";

import { Properties } from "../../../dialog";
import { MapViewportFormTabInfo } from "./mapViewportForm";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../redux/combineReducer";
import mapWorldTreeService from "../../../../../services/mapWorldTreeService";
import dialogStateService from "../../../../../services/dialogStateService";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";

export class TerrainsPropertiesState implements Properties {
    drawPriority: number;
    maxNumTile: number;
    maxNumPending: number;
    numCacheTiles: number;
    numCacheTilesStaticObjs: number;

    static getDefault(p: any): TerrainsPropertiesState {
        return {
            drawPriority: null,
            maxNumTile: null,
            maxNumPending: null,
            numCacheTiles: null,
            numCacheTilesStaticObjs: null,
        }
    }
}
export class TerrainsProperties extends TerrainsPropertiesState {
    terrainsArr: { terrain: MapCore.IMcMapTerrain, label: string }[];
    selectedTerrain: { terrain: MapCore.IMcMapTerrain, label: string };
    overlaysArr: { label: string, overlay: MapCore.IMcOverlay }[];
    layersArr: { label: string, layer: MapCore.IMcMapLayer }[];

    static getDefault(p: any): TerrainsProperties {
        let stateDefaults = super.getDefault(p);
        let { currentViewport, cursorPos, mapWorldTree } = p;
        let mcCurrentViewport: MapCore.IMcMapViewport = currentViewport.nodeMcContent;
        let mcTerrainsArr = mcCurrentViewport.GetTerrains();
        let terrainsArr = mcTerrainsArr.map((mcTerrain) => {
            let terrainNode = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, mcTerrain);
            return { terrain: mcTerrain, label: terrainNode.label }
        })

        let defaults: TerrainsProperties = {
            ...stateDefaults,
            terrainsArr: terrainsArr,
            selectedTerrain: null,
            overlaysArr: [],
            layersArr: [],
        }
        return defaults;
    }
};

export default function Terrains(props: { tabInfo: MapViewportFormTabInfo }) {
    const dispatch = useDispatch();
    let treeRedux = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    let mapWorldTree = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    let objectWorldTree = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const applyCurrentState = (pathesToApply?: string[]) => {
        runCodeSafely(() => {
            pathesToApply = pathesToApply?.map(path => `TerrainsProperties.${path}`);
            dialogStateService.applyDialogState(pathesToApply);
        }, `MapViewportForm/Terrains => applyCurrPropertiesCallback`)
    }
    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("terrainsProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in new TerrainsPropertiesState) {
                props.tabInfo.setCurrStatePropertiesCallback('TerrainsProperties', event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            }
        }, "MapViewportForm/Terrains.saveData => onChange")
    }
    //#region Handle Functions
    const handleDrawPriorityClick = () => {
        runCodeSafely(() => {
            applyCurrentState(['drawPriority'])
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            if (props.tabInfo.properties.terrainsProperties.selectedTerrain) {
                runMapCoreSafely(() => {
                    mcCurrentViewport.SetTerrainDrawPriority(props.tabInfo.properties.terrainsProperties.selectedTerrain.terrain,
                        props.tabInfo.properties.terrainsProperties.drawPriority);
                }, 'MapViewportForm/Terrains.handleDrawPriorityClick => IMcMapViewport.SetTerrainDrawPriority', true)
            }
        }, 'MapViewportForm/Terrains.handleDrawPriorityClick => onClick')
    }
    const handleMaxNumTileClick = () => {
        runCodeSafely(() => {
            applyCurrentState(['maxNumTile'])
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            if (props.tabInfo.properties.terrainsProperties.selectedTerrain) {
                runMapCoreSafely(() => {
                    mcCurrentViewport.SetTerrainMaxNumTileRequestsPerRender(props.tabInfo.properties.terrainsProperties.selectedTerrain.terrain,
                        props.tabInfo.properties.terrainsProperties.maxNumTile);
                }, 'MapViewportForm/Terrains.handleDrawPriorityClick => IMcMapViewport.SetTerrainMaxNumTileRequestsPerRender', true)
            }
        }, 'MapViewportForm/Terrains.handleMaxNumTileClick => onClick')
    }
    const handleMaxNumPendingClick = () => {
        runCodeSafely(() => {
            applyCurrentState(['maxNumPending'])
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            if (props.tabInfo.properties.terrainsProperties.selectedTerrain) {
                runMapCoreSafely(() => {
                    mcCurrentViewport.SetTerrainMaxNumPendingTileRequests(props.tabInfo.properties.terrainsProperties.selectedTerrain.terrain,
                        props.tabInfo.properties.terrainsProperties.maxNumPending);
                }, 'MapViewportForm/Terrains.handleMaxNumPendingClick => IMcMapViewport.SetTerrainMaxNumPendingTileRequests', true)
            }
        }, 'MapViewportForm/Terrains.handleMaxNumPendingClick => onClick')
    }
    const handleNumCacheTilesClick = () => {
        runCodeSafely(() => {
            applyCurrentState(['numCacheTiles'])
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            if (props.tabInfo.properties.terrainsProperties.selectedTerrain) {
                runMapCoreSafely(() => {
                    mcCurrentViewport.SetTerrainNumCacheTiles(props.tabInfo.properties.terrainsProperties.selectedTerrain.terrain,
                        false, props.tabInfo.properties.terrainsProperties.numCacheTiles);
                }, 'MapViewportForm/Terrains.handleNumCacheTilesClick => IMcMapViewport.SetTerrainNumCacheTiles', true)
            }
        }, 'MapViewportForm/Terrains.handleNumCacheTilesClick => onClick')
    }
    const handleNumCacheTilesStaticObjsClick = () => {
        runCodeSafely(() => {
            applyCurrentState(['numCacheTilesStaticObjs'])
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            if (props.tabInfo.properties.terrainsProperties.selectedTerrain) {
                runMapCoreSafely(() => {
                    mcCurrentViewport.SetTerrainNumCacheTiles(props.tabInfo.properties.terrainsProperties.selectedTerrain.terrain,
                        true, props.tabInfo.properties.terrainsProperties.numCacheTilesStaticObjs);
                }, 'MapViewportForm/Terrains.handleNumCacheTilesStaticObjsClick => IMcMapViewport.SetTerrainNumCacheTiles', true)
            }
        }, 'MapViewportForm/Terrains.handleNumCacheTilesStaticObjsClick => onClick')
    }
    const handleGetLayersClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            if (props.tabInfo.properties.terrainsProperties.selectedTerrain) {
                let mcLayers: MapCore.IMcMapLayer[] = [];
                runMapCoreSafely(() => {
                    mcLayers = mcCurrentViewport.GetVisibleLayers(props.tabInfo.properties.terrainsProperties.selectedTerrain.terrain);
                }, 'MapViewportForm/Terrains.handleGetLayersClick => IMcMapViewport.GetVisibleLayers', true)
                let layersArr = mcLayers.map(mcLayer => {
                    let layerNode = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, mcLayer);
                    return { layer: mcLayer, label: layerNode.label };
                })
                props.tabInfo.setPropertiesCallback('terrainsProperties', 'layersArr', layersArr)
            }
        }, 'MapViewportForm/Terrains.handleGetLayersClick => onClick')
    }
    const handleGetOverlaysClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let mcOverlays: MapCore.IMcOverlay[] = [];
            runMapCoreSafely(() => {
                mcOverlays = mcCurrentViewport.GetVisibleOverlays();
            }, 'MapViewportForm/Terrains.handleGetOverlaysClick => IMcMapViewport.GetVisibleOverlays', true)
            let overlaysArr = mcOverlays.map(mcOverlay => {
                let overlayNode = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(objectWorldTree, mcOverlay);
                return { overlay: mcOverlay, label: overlayNode.label };
            })
            props.tabInfo.setPropertiesCallback('terrainsProperties', 'overlaysArr', overlaysArr)
        }, 'MapViewportForm/Terrains.handleGetOverlaysClick => onClick')
    }
    const handleSelectedTerrainChange = (e) => {
        runCodeSafely(() => {
            if (e.value) {
                saveData(e);
                let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;

                let drawPriority = 0;
                runMapCoreSafely(() => {
                    drawPriority = mcCurrentViewport.GetTerrainDrawPriority(e.value.terrain);
                }, 'MapViewportForm/Terrains.handleSelectedTerrainChange => IMcMapViewport.GetTerrainDrawPriority', true)
                props.tabInfo.setPropertiesCallback('terrainsProperties', 'drawPriority', drawPriority)

                let numCacheTiles = 0;
                runMapCoreSafely(() => {
                    numCacheTiles = mcCurrentViewport.GetTerrainNumCacheTiles(e.value.terrain, false);
                }, 'MapViewportForm/Terrains.handleSelectedTerrainChange => IMcMapViewport.GetTerrainNumCacheTiles', true)
                props.tabInfo.setPropertiesCallback('terrainsProperties', 'numCacheTiles', numCacheTiles)

                let numCacheTilesStaticObjs = 0;
                runMapCoreSafely(() => {
                    numCacheTilesStaticObjs = mcCurrentViewport.GetTerrainNumCacheTiles(e.value.terrain, true);
                }, 'MapViewportForm/Terrains.handleSelectedTerrainChange => IMcMapViewport.GetTerrainNumCacheTiles', true)
                props.tabInfo.setPropertiesCallback('terrainsProperties', 'numCacheTilesStaticObjs', numCacheTilesStaticObjs)

                let maxNumPending = 0;
                runMapCoreSafely(() => {
                    maxNumPending = mcCurrentViewport.GetTerrainMaxNumPendingTileRequests(e.value.terrain);
                }, 'MapViewportForm/Terrains.handleSelectedTerrainChange => IMcMapViewport.GetTerrainMaxNumPendingTileRequests', true)
                props.tabInfo.setPropertiesCallback('terrainsProperties', 'maxNumPending', maxNumPending)

                let maxNumTile = 0;
                runMapCoreSafely(() => {
                    maxNumTile = mcCurrentViewport.GetTerrainMaxNumTileRequestsPerRender(e.value.terrain);
                }, 'MapViewportForm/Terrains.handleSelectedTerrainChange => IMcMapViewport.GetTerrainMaxNumTileRequestsPerRender', true)
                props.tabInfo.setPropertiesCallback('terrainsProperties', 'maxNumTile', maxNumTile)
            }
        }, 'MapViewportForm/Terrains.handleSelectedTerrainChange => onChange')
    }
    //#endregion

    return (
        <div className="form__row-container">
            <ListBox style={{ width: '25%' }} emptyMessage={() => { return <div></div> }} optionLabel="label" name='selectedTerrain' value={props.tabInfo.properties.terrainsProperties.selectedTerrain}
                onChange={handleSelectedTerrainChange} options={props.tabInfo.properties.terrainsProperties.terrainsArr} />

            <div className="form__column-container" style={{ width: '75%' }}>
                <div className="form__column-container">
                    <div className="form__flex-and-row-between">
                        <div style={{ width: '85%' }} className="form__flex-and-row-between">
                            <label htmlFor="drawPriority">Draw Priority: (Between -8 and 7)</label>
                            <InputNumber id='drawPriority' value={props.tabInfo.properties.terrainsProperties.drawPriority} name="drawPriority" onValueChange={saveData} />
                        </div>
                        <Button label="Apply" onClick={handleDrawPriorityClick} />
                    </div>
                    <div className="form__flex-and-row-between">
                        <div style={{ width: '85%' }} className="form__flex-and-row-between">
                            <label htmlFor="maxNumTile">Max Num Tile Requests Per Render:</label>
                            <InputNumber id='maxNumTile' value={props.tabInfo.properties.terrainsProperties.maxNumTile} name="maxNumTile" onValueChange={saveData} />
                        </div>
                        <Button label="Apply" onClick={handleMaxNumTileClick} />
                    </div>
                    <div className="form__flex-and-row-between">
                        <div style={{ width: '85%' }} className="form__flex-and-row-between">
                            <label htmlFor="maxNumPending">Max Num Pending Tile Requests:</label>
                            <InputNumber id='maxNumPending' value={props.tabInfo.properties.terrainsProperties.maxNumPending} name="maxNumPending" onValueChange={saveData} />
                        </div>
                        <Button label="Apply" onClick={handleMaxNumPendingClick} />
                    </div>
                    <div className="form__flex-and-row-between">
                        <div style={{ width: '85%' }} className="form__flex-and-row-between">
                            <label htmlFor="numCacheTiles">Num Cache Tiles (except static objects):</label>
                            <InputNumber id='numCacheTiles' value={props.tabInfo.properties.terrainsProperties.numCacheTiles} name="numCacheTiles" onValueChange={saveData} />
                        </div>
                        <Button label="Apply" onClick={handleNumCacheTilesClick} />
                    </div>
                    <div className="form__flex-and-row-between">
                        <div style={{ width: '85%' }} className="form__flex-and-row-between">
                            <label htmlFor="numCacheTilesStaticObjs">Num Cache Tiles For Static Objects:</label>
                            <InputNumber id='numCacheTilesStaticObjs' value={props.tabInfo.properties.terrainsProperties.numCacheTilesStaticObjs} name="numCacheTilesStaticObjs" onValueChange={saveData} />
                        </div>
                        <Button label="Apply" onClick={handleNumCacheTilesStaticObjsClick} />
                    </div>
                </div>
                <div className="form__row-container">
                    <div className="form__column-container" style={{ width: '50%' }}>
                        <ListBox disabled emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 12}vh`, maxHeight: `${globalSizeFactor * 12}vh` }}
                            optionLabel="label" options={props.tabInfo.properties.terrainsProperties.layersArr} />
                        <Button label="Get Visible Layers" onClick={handleGetLayersClick} />
                    </div>
                    <div className="form__column-container" style={{ width: '50%' }}>
                        <ListBox disabled emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 12}vh`, maxHeight: `${globalSizeFactor * 12}vh` }}
                            optionLabel="label" options={props.tabInfo.properties.terrainsProperties.overlaysArr} />
                        <Button label="Get Visible Overlays" onClick={handleGetOverlaysClick} />
                    </div>
                </div>
            </div>
        </div>
    )
}