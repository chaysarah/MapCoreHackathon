import { LayerService, MapCoreData, MapCoreService, ViewportData, ViewportService } from 'mapcore-lib'
import TreeService, { nodeType } from './tree.service';
import { runCodeSafely, runMapCoreSafely } from '../common/services/error-handling/errorHandler';
import { SaveLoadTypes } from '../components/dialog/treeView/mapWorldTree/shared/saveLoadCtrl';
import MapTreeNode from '../components/shared/models/map-tree-node.model';
import MapWorldTreeNodeModel, { mapWorldNodeType } from '../components/shared/models/map-tree-node.model';
import { setMapWorldTree, setSelectedNodeInTree, setTypeMapWorldDialogSecond } from '../redux/MapWorldTree/mapWorldTreeActions';
import store from '../redux/store';

class mapWorldTreeService extends TreeService {
    //Members
    private terrainBuffer: Uint8Array = new Uint8Array;
    private layerBuffer: Uint8Array = new Uint8Array;
    public dialogClassNames: Map<string, string> = new Map([
        ["More Details", 'overlay-manager__scroll-dialog-more-details'],
        ['Select Existing Item', 'scroll-dialog-select-existing-item'],
        ['Render Screen Rect To Buffer Image', 'scroll-dialog-render-screen-to-buf-img'],
        ['Spatial Queries', "scroll-dialog-spatial-queries"],
        ['Save to File', 'scroll-dialog-save-to-file'],
        ['Smart Reality Building History', 'scroll-dialog-building-history'],
        ['Add Layer', 'scroll-dialog-select-existing-layer'],
        ['Create New Layer', 'scroll-dialog-select-existing-layer'],
        ['Add Terrain', 'scroll-dialog-select-existing-terrain'],
        ['Create New Terrain', 'scroll-dialog-select-existing-terrain'],
        ['Save to a File', 'scroll-dialog-save-load-ctrl'],
        ['Backgroung Thread Index Layers', 'scroll-dialog-background-thread-index'],
        ['Set Grid', 'scroll-dialog-select-existing-grid'],
        ['Set Height Lines', 'scroll-dialog-select-existing-height-lines'],
    ]);

    public saveLoadFunctionsMap: Map<SaveLoadTypes, (fileName: string, baseDirectory: string, isSaveUserData: boolean) => void> = new Map([
        [SaveLoadTypes.SAVE_TERRAIN_TO_FILE, (...args) => this.handleSaveTerrainToFileOKClick(...args)],
        [SaveLoadTypes.SAVE_TERRAIN_TO_BUFFER, (...args) => this.handleSaveTerrainToBufferOKClick(...args)],
        [SaveLoadTypes.LOAD_TERRAIN_FROM_FILE, (...args) => this.handleLoadTerrainFromFileOKClick(...args)],
        [SaveLoadTypes.LOAD_TERRAIN_FROM_BUFFER, (...args) => this.handleLoadTerrainFromBufferOKClick(...args)],
        [SaveLoadTypes.SAVE_LAYER_TO_FILE, (...args) => this.handleSaveLayerToFileOKClick(...args)],
        [SaveLoadTypes.SAVE_LAYER_TO_BUFFER, (...args) => this.handleSaveLayerToBufferOKClick(...args)],
        [SaveLoadTypes.LOAD_LAYER_FROM_FILE, (...args) => this.handleLoadLayerFromFileOKClick(...args)],
        [SaveLoadTypes.LOAD_LAYER_FROM_BUFFER, (...args) => this.handleLoadLayerFromBufferOKClick(...args)],
    ]);

    buildTree = (): MapWorldTreeNodeModel => {
        const finalTree: MapWorldTreeNodeModel = this.createTreeNodeRecursive(mapWorldNodeType.ROOT, mapWorldNodeType.ROOT);
        return finalTree;
    }
    createTreeNodeRecursive = (node: any, nodeType_: nodeType, fatherKey = '-1', ind = -1): MapWorldTreeNodeModel => {
        let currentNode: MapWorldTreeNodeModel | any = {};
        currentNode.key = nodeType_ === mapWorldNodeType.ROOT ? '0' : `${fatherKey}_${ind !== -1 ? ind : '0'}`;
        currentNode.nodeMcContent = node;
        currentNode.nodeType = nodeType_;
        currentNode.id = this.getObjectHash(currentNode);
        currentNode.label = nodeType_ != 'Root' ? `(${currentNode.id}) ${this.getTreeNodeLabel(node, nodeType_)}` : this.getTreeNodeLabel(node, nodeType_);//actually this property has to be taken from mapCore, it has to be the name that the user called to this node||currentNode.key.
        currentNode.children = [];

        switch (nodeType_) {
            //degree 1
            case mapWorldNodeType.ROOT:
                //viewport
                const viewportDataArr: ViewportData[] = MapCoreData.viewportsData;
                viewportDataArr.forEach((viewportData: ViewportData, ind: number) => {
                    if (viewportData.viewport) {
                        const tmpNode = this.createTreeNodeRecursive(viewportData.viewport, mapWorldNodeType.MAP_VIEWPORT, currentNode.key, ind);
                        currentNode.children.push(tmpNode)
                    }
                })
                //grid
                const gridArr: MapCore.IMcMapGrid[] = MapCoreData.gridArr;
                gridArr.forEach((grid: MapCore.IMcMapGrid, ind: number) => {
                    const tmpNode = this.createTreeNodeRecursive(grid, mapWorldNodeType.MAP_GRID, currentNode.key, viewportDataArr.length + ind);
                    currentNode.children.push(tmpNode)
                })
                //height lines
                const heightLinesArr: MapCore.IMcMapHeightLines[] = MapCoreData.heightLinesArr;
                heightLinesArr.forEach((heightLines: MapCore.IMcMapHeightLines, ind: number) => {
                    const tmpNode = this.createTreeNodeRecursive(heightLines, mapWorldNodeType.MAP_HEIGHT_LINES, currentNode.key, viewportDataArr.length + gridArr.length + ind);
                    currentNode.children.push(tmpNode)
                })
                //stand-alone terrains
                const standAloneTerrains = MapCoreData.standAloneTerrains;
                standAloneTerrains.forEach((terrain: MapCore.IMcMapTerrain, ind: number) => {
                    const tmpNode = this.createTreeNodeRecursive(terrain, mapWorldNodeType.MAP_TERRAIN, currentNode.key, viewportDataArr.length + gridArr.length + heightLinesArr.length + ind);
                    currentNode.children.push(tmpNode)
                })
                //stand-alone layers
                const standAloneLayers = MapCoreData.standAloneLayers;
                standAloneLayers.forEach((layer: MapCore.IMcMapLayer, ind: number) => {
                    const tmpNode = this.createTreeNodeRecursive(layer, mapWorldNodeType.MAP_LAYER, currentNode.key, viewportDataArr.length + gridArr.length + heightLinesArr.length + standAloneTerrains.length + ind);
                    currentNode.children.push(tmpNode)
                })
                break;
            //degree 2
            case mapWorldNodeType.MAP_VIEWPORT:
                // tarrain
                const terrainArr: MapCore.IMcMapTerrain[] = node.GetTerrains();
                terrainArr.forEach((terrain: MapCore.IMcMapTerrain, ind: number) => {
                    const tmpNode = this.createTreeNodeRecursive(terrain, mapWorldNodeType.MAP_TERRAIN, currentNode.key, ind);
                    currentNode.children.push(tmpNode)
                })
                // camera
                const cameras: MapCore.IMcMapCamera[] = node.GetCameras();
                cameras.forEach((camera: MapCore.IMcMapCamera, ind: number) => {
                    const tmpNode = this.createTreeNodeRecursive(camera, mapWorldNodeType.MAP_CAMERA, currentNode.key, terrainArr.length + ind);
                    currentNode.children.push(tmpNode)
                })
                // grid
                const grid = node.GetGrid();
                if (grid) {
                    const tmpNode = this.createTreeNodeRecursive(grid, mapWorldNodeType.MAP_GRID, currentNode.key, terrainArr.length + cameras.length);
                    currentNode.children.push(tmpNode)
                }
                // height lines
                const heightLines = node.GetHeightLines();
                if (heightLines) {
                    const tmpNode = this.createTreeNodeRecursive(heightLines, mapWorldNodeType.MAP_HEIGHT_LINES, currentNode.key, terrainArr.length + cameras.length + 1);
                    currentNode.children.push(tmpNode)
                }
                // query secondary dtm
                const querySecondaryDtms: MapCore.IMcDtmMapLayer[] = node.GetQuerySecondaryDtmLayers();
                querySecondaryDtms.forEach((querySecondary: MapCore.IMcDtmMapLayer, ind: number) => {
                    const tmpNode = this.createTreeNodeRecursive(querySecondary, mapWorldNodeType.MAP_LAYER, currentNode.key, terrainArr.length + cameras.length + 2 + ind);
                    currentNode.children.push(tmpNode)
                })
                break;
            //degree 3
            case mapWorldNodeType.MAP_TERRAIN:
                // layer
                const layerArr: MapCore.IMcMapLayer[] = node.GetLayers();
                layerArr.forEach((layer: MapCore.IMcMapLayer, ind: number) => {
                    const tmpNode = this.createTreeNodeRecursive(layer, mapWorldNodeType.MAP_LAYER, currentNode.key, ind);
                    currentNode.children.push(tmpNode)
                })
                break;
            case mapWorldNodeType.MAP_CAMERA:

                break;
            case mapWorldNodeType.MAP_GRID:

                break;
            //degree 4
            case mapWorldNodeType.MAP_HEIGHT_LINES:

                break;
            case mapWorldNodeType.MAP_LAYER:

                break;
            default:
                break;
        }
        return currentNode;
    }

    renameNode(treeNode: MapWorldTreeNodeModel, keyToChange: string, newName: string): MapWorldTreeNodeModel {
        if (!treeNode) {
            return null;
        }
        if (treeNode.key === keyToChange) {
            const userNodeName = treeNode.label.substring(treeNode.label.lastIndexOf(')') + 1)
            return { ...treeNode, label: `(${newName}) ${userNodeName}` };
        }
        return {
            ...treeNode,
            children: treeNode.children.map((child) => this.renameNode(child, keyToChange, newName)),
        };
    }
    updateTreeRedux() {
        runCodeSafely(() => {
            const buildedMapTree: MapTreeNode = this.buildTree();
            store.dispatch(setMapWorldTree(buildedMapTree));
        }, "mapWorldTreeService.updateTreeRedux");
    }
    //#region Handle Menu Items Functions
    //#region RootMenu
    handleLoadTerrainFromBufferOKClick(fileName: string, baseDirectory: string, isSaveUserData: boolean) {
        runCodeSafely(() => {
            let mcLoadedTerrain: MapCore.IMcMapTerrain = null;
            runMapCoreSafely(() => { mcLoadedTerrain = MapCore.IMcMapTerrain.Load(this.terrainBuffer, baseDirectory, null); }, "mapWorldTreeService.handleLoadTerrainFromBufferOKClick => IMcMapTerrain.Load", true)
            runMapCoreSafely(() => { mcLoadedTerrain.AddRef(); }, "mapWorldTreeService.handleLoadTerrainFromBufferOKClick => IMcBase.AddRef", true)
            MapCoreData.standAloneTerrains.push(mcLoadedTerrain)
            this.updateTreeRedux();
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleLoadTerrainFromBufferOKClick")
    }
    handleLoadTerrainFromFileOKClick(fileName: string, baseDirectory: string, isSaveUserData: boolean) {
        runCodeSafely(() => {
            let mcLoadedTerrain: MapCore.IMcMapTerrain = null;
            let fileBuffer = null;
            runMapCoreSafely(() => { fileBuffer = MapCore.IMcMapDevice.GetFileSystemFileContents(fileName); }, "mapWorldTreeService.handleLoadTerrainFromBufferOKClick => IMcMapDevice.GetFileSystemFileContents", true)
            runMapCoreSafely(() => { mcLoadedTerrain = MapCore.IMcMapTerrain.Load(fileBuffer, baseDirectory, null); }, "mapWorldTreeService.handleLoadTerrainFromBufferOKClick => IMcMapTerrain.Load", true)
            runMapCoreSafely(() => { mcLoadedTerrain.AddRef(); }, "mapWorldTreeService.handleLoadTerrainFromBufferOKClick => IMcBase.AddRef", true)
            MapCoreData.standAloneTerrains.push(mcLoadedTerrain)
            this.updateTreeRedux();
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleLoadTerrainFromFileOKClick")
    }
    handleCreateNewTerrainClick(selectedTerrains: { mcTerrain: MapCore.IMcMapTerrain; isNew: boolean; }[]) {
        runCodeSafely(() => {
            this.updateTreeRedux();
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleCreateNewTerrainClick")
    }
    handleLoadLayerFromFileOKClick(fileName: string, baseDirectory: string, isSaveUserData: boolean) {
        runCodeSafely(() => {
            let mcLoadedLayer: MapCore.IMcMapLayer = null;
            let fileBuffer = null;
            runMapCoreSafely(() => { fileBuffer = MapCore.IMcMapDevice.GetFileSystemFileContents(fileName); }, "mapWorldTreeService.handleLoadLayerFromFileOKClick => IMcMapDevice.GetFileSystemFileContents", true)
            runMapCoreSafely(() => { mcLoadedLayer = MapCore.IMcMapLayer.Load(fileBuffer, baseDirectory, null); }, "mapWorldTreeService.handleLoadLayerFromFileOKClick => IMcMapLayer.Load", true)
            runMapCoreSafely(() => { mcLoadedLayer.AddRef(); }, "mapWorldTreeService.handleLoadLayerFromFileOKClick => IMcBase.AddRef", true)
            MapCoreData.standAloneLayers.push(mcLoadedLayer)
            this.updateTreeRedux();
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleLoadLayerFromFileOKClick")
    }
    handleLoadLayerFromBufferOKClick(fileName: string, baseDirectory: string, isSaveUserData: boolean) {
        runCodeSafely(() => {
            let mcLoadedLayer: MapCore.IMcMapLayer = null;
            runMapCoreSafely(() => { mcLoadedLayer = MapCore.IMcMapLayer.Load(this.layerBuffer, baseDirectory, null); }, "mapWorldTreeService.handleLoadLayerFromBufferOKClick => IMcMapLayer.Load", true)
            runMapCoreSafely(() => { mcLoadedLayer.AddRef(); }, "mapWorldTreeService.handleLoadLayerFromBufferOKClick => IMcBase.AddRef", true)
            MapCoreData.standAloneLayers.push(mcLoadedLayer)
            this.updateTreeRedux();
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleLoadLayerFromBufferOKClick")
    }
    handleCreateNewLayerClick(selectedLayers: { mcLayer: MapCore.IMcMapLayer; isNew: boolean; }[]) {
        runCodeSafely(() => {
            this.updateTreeRedux();
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleCreateNewLayerClick")
    }
    //#endregion
    //#region Viewport Menu
    handleCreateCameraClick() {
        runCodeSafely(() => {
            const mcCurrentViewport = store.getState().mapWorldTreeReducer.selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            let newCamera: MapCore.IMcMapCamera = null;
            runMapCoreSafely(() => { newCamera = mcCurrentViewport.CreateCamera(); }, "mapWorldTreeService.handleCreateCameraClick => IMcMapViewport.CreateCamera", true)
            let viewportTerrains: MapCore.IMcMapTerrain[] = [];
            runMapCoreSafely(() => { viewportTerrains = mcCurrentViewport.GetTerrains(); }, "mapWorldTreeService.handleCreateCameraClick => IMcSpatialQueries.GetTerrains", true)
            if (viewportTerrains.length > 0) {
                let firstTerrainBoundingBox: MapCore.SMcBox = null;
                runMapCoreSafely(() => { firstTerrainBoundingBox = viewportTerrains[0].GetBoundingBox(); }, "mapWorldTreeService.handleCreateCameraClick => IMcMapTerrain.GetBoundingBox", true)
                let newCameraPosition = new MapCore.SMcVector3D(MapCore.v3Zero);
                newCameraPosition.x = (firstTerrainBoundingBox.MaxVertex.x + firstTerrainBoundingBox.MinVertex.x) / 2;
                newCameraPosition.y = (firstTerrainBoundingBox.MaxVertex.y + firstTerrainBoundingBox.MinVertex.y) / 2;
                newCameraPosition.z = (firstTerrainBoundingBox.MaxVertex.z + firstTerrainBoundingBox.MinVertex.z) / 2;
                runMapCoreSafely(() => { newCamera.SetCameraPosition(newCameraPosition, false); }, "mapWorldTreeService.handleCreateCameraClick => IMcMapCamera.SetCameraPosition", true)
            }
            this.updateTreeRedux();
        }, "mapWorldTreeService.handleCreateCameraClick")
    }
    handleAddTerrainToViewportClick(selectedTerrains: { mcTerrain: MapCore.IMcMapTerrain; isNew: boolean; }[]) {
        runCodeSafely(() => {
            const mcCurrentViewport = store.getState().mapWorldTreeReducer.selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            const mcTerrains = selectedTerrains.map(terrain => terrain.mcTerrain);
            mcTerrains.forEach((currentTerrain: MapCore.IMcMapTerrain) => {
                runMapCoreSafely(() => { mcCurrentViewport.AddTerrain(currentTerrain); }, "mapWorldTreeService.handleAddTerrainToViewportClick => IMcMapViewport.AddTerrain", true)
            })
            MapCoreService.releaseStandAloneTerrains(mcTerrains);
            this.updateTreeRedux();
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleAddTerrainToViewportClick")
    }
    handleSetGridClick(selectedGrid: MapCore.IMcMapGrid) {
        runCodeSafely(() => {
            if (selectedGrid) {
                const mcCurrentViewport = store.getState().mapWorldTreeReducer.selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
                runMapCoreSafely(() => { mcCurrentViewport.SetGrid(selectedGrid); }, "mapWorldTreeService.handleSetGridClick => IMcMapViewport.SetGrid", true)
                runMapCoreSafely(() => { mcCurrentViewport.SetGridVisibility(true); }, "mapWorldTreeService.handleSetGridClick => IMcMapViewport.SetGridVisibility", true)
                this.updateTreeRedux();
            }
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleSetGridClick")
    }
    handleRemoveGrid() {
        runCodeSafely(() => {
            const mcCurrentViewport = store.getState().mapWorldTreeReducer.selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            runMapCoreSafely(() => { mcCurrentViewport.SetGrid(null); }, "mapWorldTreeService.handleRemoveGrid => IMcMapViewport.SetGrid", true)
            this.updateTreeRedux();
        }, "mapWorldTreeService.handleRemoveGrid")
    }
    handleSetHeightLinesClick(selectedHeightLines: MapCore.IMcMapHeightLines) {
        runCodeSafely(() => {
            if (selectedHeightLines) {
                const mcCurrentViewport = store.getState().mapWorldTreeReducer.selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
                runMapCoreSafely(() => { mcCurrentViewport.SetHeightLines(selectedHeightLines); }, "mapWorldTreeService.handleSetHeightLinesClick => IMcMapViewport.SetHeightLines", true)
                runMapCoreSafely(() => { mcCurrentViewport.SetHeightLinesVisibility(true); }, "mapWorldTreeService.handleSetHeightLinesClick => IMcMapViewport.SetHeightLinesVisibility", true)
                this.updateTreeRedux();
            }
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleSetHeightLinesClick")
    }
    handleRemoveHeightLines() {
        runCodeSafely(() => {
            const mcCurrentViewport = store.getState().mapWorldTreeReducer.selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            runMapCoreSafely(() => { mcCurrentViewport.SetHeightLines(null); }, "mapWorldTreeService.handleRemoveHeightLines => IMcMapViewport.SetHeightLines", true)
            this.updateTreeRedux();
        }, "mapWorldTreeService.handleRemoveHeightLines")
    }
    //#endregion
    //#region Terrain Menu
    handleAddLayerToTerrainClick(selectedLayers: { mcLayer: MapCore.IMcMapLayer; isNew: boolean; }[]) {
        runCodeSafely(() => {
            const mcCurrentTerrain = store.getState().mapWorldTreeReducer.selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
            const mcLayers = selectedLayers.map(layer => layer.mcLayer);
            mcLayers.forEach((currentLayer: MapCore.IMcMapLayer) => {
                runMapCoreSafely(() => { mcCurrentTerrain.AddLayer(currentLayer); }, "mapWorldTreeService.handleAddLayerToTerrainClick => IMcMapTerrain.AddLayer", true)
            })
            LayerService.releaseStandAloneLayers(mcLayers);
            this.updateTreeRedux();
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleAddLayerToTerrainClick")
    }
    handleRemoveLayerFromTerrainClick(selectedLayers: { mcLayer: MapCore.IMcMapLayer; isNew: boolean; }[]) {
        runCodeSafely(() => {
            const mcCurrentTerrain = store.getState().mapWorldTreeReducer.selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
            const mcLayers = selectedLayers.map(layer => layer.mcLayer);
            mcLayers.forEach((currentLayer: MapCore.IMcMapLayer) => {
                runMapCoreSafely(() => { mcCurrentTerrain.RemoveLayer(currentLayer); }, "mapWorldTreeService.handleRemoveLayerFromTerrainClick => IMcMapTerrain.RemoveLayer", true)
            })
            this.updateTreeRedux();
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleRemoveLayerFromTerrainClick")
    }
    handleRemoveTerrainClick() {
        runCodeSafely(() => {
            const mapTreeRedux = store.getState().mapWorldTreeReducer.mapWorldTree;
            const selectedTerrainNode = store.getState().mapWorldTreeReducer.selectedNodeInTree;
            const mcCurrentTerrain = selectedTerrainNode.nodeMcContent as MapCore.IMcMapTerrain;
            const parentViewport = this.getParentByChildKey(mapTreeRedux, selectedTerrainNode.key);
            if (parentViewport.nodeType == mapWorldNodeType.MAP_VIEWPORT) {
                const mcParentViewport = parentViewport.nodeMcContent as MapCore.IMcMapViewport;
                runMapCoreSafely(() => { mcParentViewport.RemoveTerrain(mcCurrentTerrain); }, "mapWorldTreeService.handleRemoveTerrainClick => IMcMapViewport.RemoveTerrain", true)
            }
            else {
                MapCoreService.releaseStandAloneTerrains([mcCurrentTerrain]);
            }
            this.updateTreeRedux();
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
            store.dispatch(setSelectedNodeInTree(null))
        }, "mapWorldTreeService.handleRemoveTerrainClick")
    }
    handleSaveTerrainToFileOKClick(fileName: string, baseDirectory: string, isSaveUserData: boolean) {
        runCodeSafely(() => {
            const mcCurrentTerrain = store.getState().mapWorldTreeReducer.selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
            let terrainBuffer: Uint8Array = new Uint8Array();
            runMapCoreSafely(() => { terrainBuffer = mcCurrentTerrain.Save(baseDirectory, isSaveUserData); }, "mapWorldTreeService.handleSaveTerrainToFileOKClick => IMcMapTerrain.Save", true)
            runMapCoreSafely(() => {
                MapCore.IMcMapDevice.DownloadBufferAsFile(terrainBuffer, fileName);
            }, '"mapWorldTreeService.handleSaveTerrainToFileOKClick => IMcMapDevice.DownloadBufferAsFile', true);
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleSaveTerrainToFileOKClick")
    }
    handleSaveTerrainToBufferOKClick(fileName: string, baseDirectory: string, isSaveUserData: boolean) {
        runCodeSafely(() => {
            const mcCurrentTerrain = store.getState().mapWorldTreeReducer.selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
            let terrainBuffer: Uint8Array = new Uint8Array();
            runMapCoreSafely(() => { terrainBuffer = mcCurrentTerrain.Save(baseDirectory, isSaveUserData); }, "mapWorldTreeService.handleSaveTerrainToBufferOKClick => IMcMapTerrain.Save", true)
            this.terrainBuffer = terrainBuffer;
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleSaveTerrainToBufferOKClick")
    }
    //#endregion
    //#endregion
    //#region Layer Menu
    handleSaveLayerToFileOKClick(fileName: string, baseDirectory: string, isSaveUserData: boolean) {
        runCodeSafely(() => {
            const mcCurrentLayer = store.getState().mapWorldTreeReducer.selectedNodeInTree.nodeMcContent as MapCore.IMcMapLayer;
            let layerBuffer: Uint8Array = new Uint8Array();
            runMapCoreSafely(() => { layerBuffer = mcCurrentLayer.Save(baseDirectory, isSaveUserData); }, "mapWorldTreeService.handleSaveTerrainToFileOKClick => IMcMapLayer.Save", true)
            runMapCoreSafely(() => {
                MapCore.IMcMapDevice.DownloadBufferAsFile(layerBuffer, fileName);
            }, '"mapWorldTreeService.handleSaveLayerToFileOKClick => IMcMapDevice.DownloadBufferAsFile', true);
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleSaveLayerToFileOKClick")
    }
    handleSaveLayerToBufferOKClick(fileName: string, baseDirectory: string, isSaveUserData: boolean) {
        runCodeSafely(() => {
            const mcCurrentLayer = store.getState().mapWorldTreeReducer.selectedNodeInTree.nodeMcContent as MapCore.IMcMapLayer;
            let layerBuffer: Uint8Array = new Uint8Array();
            runMapCoreSafely(() => { layerBuffer = mcCurrentLayer.Save(baseDirectory, isSaveUserData); }, "mapWorldTreeService.handleSaveLayerToBufferOKClick => IMcMapLayer.Save", true)
            this.layerBuffer = layerBuffer;
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
        }, "mapWorldTreeService.handleSaveLayerToBufferOKClick")
    }
    handleRemoveLayerClick() {
        runCodeSafely(() => {
            const mapTreeRedux = store.getState().mapWorldTreeReducer.mapWorldTree;
            const selectedLayerNode = store.getState().mapWorldTreeReducer.selectedNodeInTree;
            const mcCurrentLayer = selectedLayerNode.nodeMcContent as MapCore.IMcMapLayer;
            const parentTerrain = this.getParentByChildKey(mapTreeRedux, selectedLayerNode.key);
            if (parentTerrain.nodeType == mapWorldNodeType.MAP_TERRAIN) {
                const mcParentTerrain = parentTerrain.nodeMcContent as MapCore.IMcMapTerrain;
                runMapCoreSafely(() => { mcParentTerrain.RemoveLayer(mcCurrentLayer); }, "mapWorldTreeService.handleRemoveLayerClick => IMcMapTerrain.RemoveLayer", true)
            }
            else {
                LayerService.releaseStandAloneLayers([mcCurrentLayer]);
            }
            this.updateTreeRedux();
            store.dispatch(setTypeMapWorldDialogSecond(undefined))
            store.dispatch(setSelectedNodeInTree(null))
        }, "mapWorldTreeService.handleRemoveLayerClick")
    }
    handleMoveToCenterClick() {
        runCodeSafely(() => {
            const selectedLayerNode = store.getState().mapWorldTreeReducer.selectedNodeInTree;
            const mcCurrentLayer = selectedLayerNode.nodeMcContent as MapCore.IMcMapLayer;
            const layerBoundingBox = mcCurrentLayer.GetBoundingBox();
            if (layerBoundingBox) {
                const centerPoint = MapCore.SMcBox.CenterPoint(layerBoundingBox);
                const activeCard = store.getState().mapWindowReducer.activeCard;
                let activeViewport = activeCard ? MapCoreData.findViewport(activeCard).viewport : null;
                const mapTreeRedux = store.getState().mapWorldTreeReducer.mapWorldTree;
                const parentViewport = this.getParentByChildKey(mapTreeRedux, selectedLayerNode.key.slice(0, -2));
                if (parentViewport.nodeType == mapWorldNodeType.MAP_VIEWPORT) {
                    activeViewport = parentViewport.nodeMcContent as MapCore.IMcMapViewport;
                }
                ViewportService.setCameraPosition(activeViewport, centerPoint);
            }
        }, "mapWorldTreeService.handleMoveToCenterClick")
    }
    //#endregion
}

export default new mapWorldTreeService();