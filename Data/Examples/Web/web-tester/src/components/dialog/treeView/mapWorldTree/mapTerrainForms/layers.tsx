import { ListBox } from "primereact/listbox";
import { Properties } from "../../../dialog";
import { TabInfo } from "../../../shared/tabCtrls/tabModels";
import { Fieldset } from "primereact/fieldset";
import { InputNumber } from "primereact/inputnumber";
import { Button } from "primereact/button";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import { TreeNodeModel } from "../../../../../services/tree.service";
import mapWorldTreeService from "../../../../../services/mapWorldTreeService";
import { Checkbox } from "primereact/checkbox";
import InputMaxNumber from "../../../../shared/inputMaxNumber";
import { ConfirmDialog } from "primereact/confirmdialog";
import { setMapWorldTree } from "../../../../../redux/MapWorldTree/mapWorldTreeActions";
import { useEffect } from "react";

export class LayersPropertiesState implements Properties {
    numTiles: number;
    isVisibility: boolean;
    minScaleVisibility: number;
    maxScaleVisibility: number;
    drawPriority: number;
    transparency: number;
    isNearest: boolean;
    selectedLayerToRemoveArr: TreeNodeModel[];

    static getDefault(p: any): LayersPropertiesState {
        let { currentTerrain, mapWorldTree } = p;
        let mcCurrentTerrain = currentTerrain.nodeMcContent as MapCore.IMcMapTerrain;
        let mcLayerParams = new MapCore.IMcMapTerrain.SLayerParams();

        return {
            numTiles: 0,
            isVisibility: mcLayerParams.bVisibility,
            minScaleVisibility: mcLayerParams.fMinScale,
            maxScaleVisibility: mcLayerParams.fMaxScale,
            drawPriority: mcLayerParams.nDrawPriority,
            transparency: mcLayerParams.byTransparency,
            isNearest: mcLayerParams.bNearestPixelMagFilter,
            selectedLayerToRemoveArr: [],
        }
    }
}
export class LayersProperties extends LayersPropertiesState {
    layersArr: TreeNodeModel[];
    selectedLayer: TreeNodeModel;
    isConfirmDialog: boolean;
    removedLayers: MapCore.IMcMapLayer[];

    static getDefault(p: any): LayersProperties {
        let stateDefaults = super.getDefault(p);
        let { currentTerrain, mapWorldTree } = p;
        let mcCurrentTerrain = currentTerrain.nodeMcContent as MapCore.IMcMapTerrain;
        let mcLayersArr: MapCore.IMcMapLayer[] = [];
        runMapCoreSafely(() => {
            mcLayersArr = mcCurrentTerrain.GetLayers();
        }, 'MapTerrainForm/Layers.getDefault => IMcMapTerrain.GetLayers', true)
        let layersArr = mcLayersArr.map(mcLayer => mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, mcLayer))


        let defaults: LayersProperties = {
            ...stateDefaults,
            layersArr: layersArr,
            selectedLayer: null,
            isConfirmDialog: false,
            removedLayers: [],
        }
        return defaults;
    }
};

export default function Layers(props: { tabInfo: TabInfo }) {
    const dispatch = useDispatch();
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const mapWorldTree = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree)
    //#region UseEffects
    useEffect(() => {
        setUpdatedMcLayers();
    }, [mapWorldTree])
    //#endregion
    const setUpdatedMcLayers = () => {
        runCodeSafely(() => {
            let mcCurrentTerrain = selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
            let mcLayersArr = [];
            runMapCoreSafely(() => {
                mcLayersArr = mcCurrentTerrain.GetLayers();
            }, 'MapTerrainForm/Layers.setUpdatedMcLayers => IMcMapTerrain.GetLayers', true)
            let layersArr = mcLayersArr.map(mcLayer => mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, mcLayer))
            props.tabInfo.setPropertiesCallback('layersArr', layersArr)
            setSelectedLayersInLists();
        }, 'MapTerrainForm/Layers.setUpdatedMcLayers')
    }
    const setSelectedLayersInLists = () => {
        runCodeSafely(() => {
            let newSelectedNode = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, props.tabInfo.tabProperties.selectedLayer?.nodeMcContent);
            props.tabInfo.setPropertiesCallback('selectedLayer', newSelectedNode)
            let newSelectedNodeToRemove = [];
            props.tabInfo.tabProperties.selectedLayerToRemoveArr.forEach((selectedLayerToRemove: TreeNodeModel) => {
                let newSelectedNode = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, selectedLayerToRemove.nodeMcContent);
                newSelectedNodeToRemove = newSelectedNode ? [...newSelectedNodeToRemove, newSelectedNode] : newSelectedNodeToRemove;
            })
            let isFoundLayers = true;
            props.tabInfo.tabProperties.removedLayers.forEach((removedLayer: MapCore.IMcMapLayer) => {
                let newRemovedNode = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, removedLayer);
                if (newRemovedNode) {//if layer added now
                    newSelectedNodeToRemove = [...newSelectedNodeToRemove, newRemovedNode];
                }
                else {//the remove action was now
                    isFoundLayers = false;
                }
            })
            isFoundLayers && props.tabInfo.setPropertiesCallback('removedLayers', []);
            props.tabInfo.setPropertiesCallback('selectedLayerToRemoveArr', newSelectedNodeToRemove)
        }, 'MapTerrainForm/Layers.setSelectedLayersInLists')
    }
    const refreshTree = () => {
        runCodeSafely(() => {
            let tree: TreeNodeModel = mapWorldTreeService.buildTree()
            dispatch(setMapWorldTree(tree))
        }, 'MapTerrainForm/Layers.refreshTree')
    }
    //#region Handle Functions
    const handleSetNumTilesClick = (e) => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['numTiles'])
            let mcCurrentTerrrain = selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
            let mcLayers: MapCore.IMcMapLayer[] = [];
            runMapCoreSafely(() => {
                mcLayers = mcCurrentTerrrain.GetLayers();
            }, 'MapTerrainForm/Layers.handleSetNumTilesClick => IMcMapTerrain.GetLayers', true)
            mcLayers.forEach(layer => {
                runMapCoreSafely(() => {
                    layer.SetNumTilesInNativeServerRequest(props.tabInfo.tabProperties.numTiles)
                }, 'MapTerrainForm/Layers.handleSetNumTilesClick => IMcMapLayer.SetNumTilesInNativeServerRequest', true)
            });
        }, 'MapTerrainForm/Layers.handleSetNumTilesClick')
    }
    const getUpdatedMaxInput = (value: number) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback('maxScaleVisibility', value)
            props.tabInfo.setCurrStatePropertiesCallback('maxScaleVisibility', value)
        }, 'MapTerrainForm/Layers.handleIDChange')
    }
    const handleSetLayerParamsClick = () => {
        runCodeSafely(() => {
            if (props.tabInfo.tabProperties.selectedLayer) {
                props.tabInfo.applyCurrStatePropertiesCallback(['isNearest', 'isVisibility', 'transparency', 'maxScaleVisibility', 'minScaleVisibility', 'drawPriority'])
                let mcCurrentTerrain = selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
                let mcLayerParams = new MapCore.IMcMapTerrain.SLayerParams();
                mcLayerParams.bNearestPixelMagFilter = props.tabInfo.tabProperties.isNearest;
                mcLayerParams.bVisibility = props.tabInfo.tabProperties.isVisibility;
                mcLayerParams.byTransparency = props.tabInfo.tabProperties.transparency;
                mcLayerParams.fMaxScale = props.tabInfo.tabProperties.maxScaleVisibility;
                mcLayerParams.fMinScale = props.tabInfo.tabProperties.minScaleVisibility;
                mcLayerParams.nDrawPriority = props.tabInfo.tabProperties.drawPriority;
                runMapCoreSafely(() => {
                    mcCurrentTerrain.SetLayerParams(props.tabInfo.tabProperties.selectedLayer.nodeMcContent, mcLayerParams);
                }, 'MapTerrainForm/Layers.handleSetLayerParamsClick => IMcMapTerrain.SetLayerParams', true)
            }
            else {
                props.tabInfo.setPropertiesCallback('isConfirmDialog', true);
            }
        }, 'MapTerrainForm/Layers.handleSetLayerParamsClick')
    }
    const handleLayerChange = (e) => {
        runCodeSafely(() => {
            props.tabInfo.saveData(e);
            let mcCurrentTerrain = selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
            let mcLayerParams = new MapCore.IMcMapTerrain.SLayerParams();
            if (e.value) {
                let mcLayer: MapCore.IMcMapLayer = e.value.nodeMcContent;
                runMapCoreSafely(() => {
                    mcLayerParams = mcCurrentTerrain.GetLayerParams(mcLayer);
                }, 'MapTerrainForm/Layers.getDefault => IMcMapTerrain.GetLayerParams', true)
            }
            props.tabInfo.setPropertiesCallback('isNearest', mcLayerParams.bNearestPixelMagFilter);
            props.tabInfo.setPropertiesCallback('isVisibility', mcLayerParams.bVisibility);
            props.tabInfo.setPropertiesCallback('transparency', mcLayerParams.byTransparency);
            props.tabInfo.setPropertiesCallback('maxScaleVisibility', mcLayerParams.fMaxScale);
            props.tabInfo.setPropertiesCallback('minScaleVisibility', mcLayerParams.fMinScale);
            props.tabInfo.setPropertiesCallback('drawPriority', mcLayerParams.nDrawPriority);
        }, 'MapTerrainForm/Layers.handleLayerChange')
    }
    const handleSelectAllClick = () => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback('selectedLayerToRemoveArr', props.tabInfo.tabProperties.layersArr);
        }, 'MapTerrainForm/Layers.handleSelectAllClick')
    }
    const handleRemoveClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['selectedLayerToRemoveArr']);
            let mcCurrentTerrain = selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
            let removedLayersLocal: MapCore.IMcMapLayer[] = [];
            props.tabInfo.tabProperties.selectedLayerToRemoveArr?.forEach((layerNode: TreeNodeModel) => {
                let isRemoved = false;
                runMapCoreSafely(() => {
                    mcCurrentTerrain.RemoveLayer(layerNode.nodeMcContent);
                    isRemoved = true;
                }, 'MapTerrainForm/Layers.handleRemoveClick => IMcMapTerrain.RemoveLayer', true)
                removedLayersLocal = isRemoved ? [...removedLayersLocal, layerNode.nodeMcContent] : removedLayersLocal;
            })
            props.tabInfo.setPropertiesCallback('removedLayers', removedLayersLocal);
            refreshTree();
        }, 'MapTerrainForm/Layers.handleRemoveClick')
    }
    const handleAddClick = () => {
        runCodeSafely(() => {
            let mcCurrentTerrain = selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
            props.tabInfo.tabProperties.removedLayers.forEach((mcLayer: MapCore.IMcMapLayer) => {
                runMapCoreSafely(() => {
                    mcCurrentTerrain.AddLayer(mcLayer);
                }, 'MapTerrainForm/Layers.handleRemoveClick => IMcMapTerrain.AddLayer', true)
            })
            // props.tabInfo.setPropertiesCallback('removedLayers', []);
            refreshTree();
        }, 'MapTerrainForm/Layers.handleAddClick')
    }
    //#endregion
    //#region DOM Functions
    const getLayersFields = () => {
        return <div className="form__flex-and-row-between">
            <ListBox style={{ width: '50%' }} emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 10}vh`, maxHeight: `${globalSizeFactor * 10}vh` }} name='selectedLayer' optionLabel="label" value={props.tabInfo.tabProperties.selectedLayer} onChange={handleLayerChange} options={props.tabInfo.tabProperties.layersArr} />
            <div style={{ width: '45%' }} className="form__column-container">
                <div className="form__flex-and-row form__items-center">
                    <Checkbox name='isVisibility' inputId="isVisibility" onChange={props.tabInfo.saveData} checked={props.tabInfo.tabProperties.isVisibility} />
                    <label htmlFor="isVisibility">Visibility</label>
                </div>
                <div className="form__flex-and-row-between">
                    <label htmlFor="minScaleVisibility">Min Scale Visibility: </label>
                    <InputNumber id='minScaleVisibility' value={props.tabInfo.tabProperties.minScaleVisibility} name="minScaleVisibility" onValueChange={props.tabInfo.saveData} />
                </div>
                <div className="form__flex-and-row-between">
                    <label htmlFor="maxScaleVisibility">Max Scale Visibilty: </label>
                    <InputMaxNumber value={props.tabInfo.tabProperties.maxScaleVisibility} maxValue={MapCore.FLT_MAX} getUpdatedMaxInput={getUpdatedMaxInput} id='maxScaleVisibility' name='maxScaleVisibility' />
                </div>
                <div className="form__flex-and-row-between">
                    <label htmlFor="drawPriority">Draw Prority: </label>
                    <InputNumber id='drawPriority' value={props.tabInfo.tabProperties.drawPriority} name="drawPriority" onValueChange={props.tabInfo.saveData} />
                </div>
                <div className="form__flex-and-row-between">
                    <label htmlFor="transparency">Transparency: </label>
                    <InputNumber id='transparency' value={props.tabInfo.tabProperties.transparency} name="transparency" onValueChange={props.tabInfo.saveData} />
                </div>
                <div className="form__flex-and-row form__items-center">
                    <Checkbox name='isNearest' inputId="isNearest" onChange={props.tabInfo.saveData} checked={props.tabInfo.tabProperties.isNearest} />
                    <label htmlFor="isNearest">Nearest Pixel Mag Filter</label>
                </div>
                <Button label="OK" onClick={handleSetLayerParamsClick} />
            </div>
        </div>
    }
    const getRemoveLayerFields = () => {
        return <div className="form__column-container">
            <br />
            <span style={{ textDecoration: 'underline' }}>Add and Remove Several Layers</span>
            <div className="form__flex-and-row-between">
                <ListBox style={{ width: '50%' }} multiple emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 10}vh`, maxHeight: `${globalSizeFactor * 10}vh` }} name='selectedLayerToRemoveArr' optionLabel="label" value={props.tabInfo.tabProperties.selectedLayerToRemoveArr} onChange={props.tabInfo.saveData} options={props.tabInfo.tabProperties.layersArr} />
                <div className="form__column-container form__justify-end">
                    <Button label='Select All' onClick={handleSelectAllClick} />
                    {props.tabInfo.tabProperties.removedLayers.length > 0 ?
                        <Button label='Add Removed' onClick={handleAddClick} /> :
                        <Button label='Remove' onClick={handleRemoveClick} />}
                </div>
            </div>
        </div>
    }
    const getNumTilesFieldset = () => {
        return <Fieldset legend='Num Tiles In Native Server Request For All Layers' className="form__row-fieldset form__space-between">
            <div style={{ width: '40%' }} className="form__flex-and-row-between">
                <label htmlFor="numTiles">Num Tiles: </label>
                <InputNumber id='numTiles' value={props.tabInfo.tabProperties.numTiles} name="numTiles" onValueChange={props.tabInfo.saveData} />
            </div>
            <Button label='Set' onClick={handleSetNumTilesClick} />
        </Fieldset>
    }
    //#endregion

    return <div className="form__column-container">
        {getLayersFields()}
        {getRemoveLayerFields()}
        {getNumTilesFieldset()}

        <ConfirmDialog
            contentClassName='form__confirm-dialog-content'
            message='You have to choose layer first!'
            header=''
            footer={<div></div>}
            visible={props.tabInfo.tabProperties.isConfirmDialog}
            onHide={e => { props.tabInfo.setPropertiesCallback('isConfirmDialog', false) }}
        />
    </div>
}


