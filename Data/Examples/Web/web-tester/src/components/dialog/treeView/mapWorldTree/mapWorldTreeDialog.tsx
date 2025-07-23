import React, { ReactElement, useEffect, useMemo, useRef, useState } from 'react';
import { Tree, TreeExpandedKeysType } from 'primereact/tree';
import { ContextMenu } from 'primereact/contextmenu';
import { useDispatch, useSelector } from 'react-redux';
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';

import './styles/mapWorldTreeDialog.css';
import '../sharedStyles/tree.css'
import SaveLoadCtrl, { SaveLoadTypes } from './shared/saveLoadCtrl';
import MapTerrainForm from './mapTerrainForms/mapTerrainForm';
import MapViewportForm from './viewportForms/mapViewportForm';
import MapLayerForm from './mapLayerForms/mapLayerForm';
import BackgroundThreadIndex from './shared/backgroundThreadIndex';
import SelectExistingHeightLines from '../../shared/ControlsForMapcoreObjects/selectExistingHeightLines/selectExistingHeightLines';
import SelectExistingGrid from '../../shared/ControlsForMapcoreObjects/gridCtrl/selectExistingGrid';
import SelectExistingTerrain from '../../shared/ControlsForMapcoreObjects/terrainCtrl/selectExistingTerrain';
import ScanItem3DModelSmartReality from '../../mapToolbarActions/mapOperations/scan/SubScanResult/ScanItem3DModelSmartReality';
import SelectExistingLayer from '../../shared/ControlsForMapcoreObjects/layerCtrl/selectExistingLayer';
import TreeNodeModel, { mapWorldNodeType, treeNodeActions } from '../../../shared/models/map-tree-node.model';
import ObjectTreeNode, { hideFormReasons } from '../../../shared/models/tree-node.model';
import SecondDialogModel from '../../../shared/models/second-dialog.model';
import MapTreeNode from '../../../shared/models/map-tree-node.model';
import { AppState } from '../../../../redux/combineReducer';
import { setMapWorldTree, setSelectedNodeInTree, setTypeMapWorldDialogSecond } from '../../../../redux/MapWorldTree/mapWorldTreeActions';
import { runCodeSafely } from '../../../../common/services/error-handling/errorHandler';
import mapWorldTreeService from '../../../../services/mapWorldTreeService';
import { closeDialog } from '../../../../redux/MapCore/mapCoreAction';
import { DialogTypesEnum } from '../../../../tools/enum/enums';
import objectWorldTreeService from '../../../../services/objectWorldTree.service';
import { setObjectWorldTree } from '../../../../redux/ObjectWorldTree/objectWorldTreeActions';

export default function MapWorldTreeDialog(props: { FooterHook: (footer: () => ReactElement) => void }) {
    let cm = useRef<ContextMenu>();
    let selectedNodeRef = useRef(null);
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const treeRedux: TreeNodeModel = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    const showDialog: { hideFormReason: hideFormReasons, dialogType: DialogTypesEnum } = useSelector((state: AppState) => state.objectWorldTreeReducer.showDialog);
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    const typeMapWorldDialogSecond: SecondDialogModel = useSelector((state: AppState) => state.mapWorldTreeReducer.typeMapWorldDialogSecond);

    let [dynamicCM, setDynamicCM] = useState<{ separator?: boolean, label?: string, command?: () => void, items?: any[] }[]>([]);
    // let [selectedNodeBoundingBox, setSelectedNodeBoundingBox] = useState<DOMRect>(); --connect to rename
    // let [renameValue, setRenameValue] = useState<string>(''); --connect to rename
    // let [renameMode, setRenameMode] = useState<boolean>(false); --connect to rename
    // let [isInputDrag, setIsInputDrag] = useState<boolean>(false); --connect to rename
    let [desiredNodeType, setDesiredNodeType] = useState<string>(null);
    // let [isConfirmDialog, setIsConfirmDialog] = useState<boolean>(false);
    // let [confirmDialogMessage, setConfirmDialogMessage] = useState<string>(null);
    let [expandedKeys, setExpandedKeys] = useState<TreeExpandedKeysType>({});
    const handleApplyFunc = useSelector((state: AppState) => state.mapWorldTreeReducer.handleApplyFunc);

    const menuItemsCommand = useMemo(() => ({
        //Shared Actions
        'Expand': () => { runCodeSafely(() => { setExpandedKeys({ ...expandedKeys, ...mapWorldTreeService.expandNode(treeRedux, selectedNodeInTree.key) }) }, "mapWorldTreeDialog.expand => onClick") },
        'Collapse': () => { runCodeSafely(() => { setExpandedKeys(mapWorldTreeService.collapseNode(treeRedux, selectedNodeInTree.key, expandedKeys)) }, "mapWorldTreeDialog.collpse => onClick") },
        // 'Rename': () => { --connect to rename
        //     let end = selectedNodeInTree.label.lastIndexOf(')');
        //     setRenameValue(`${selectedNodeInTree.label.substring(1, end)}`);
        //     setRenameMode(true);
        // },

        //Root Actions
        'Root': {
            'Viewport': {
                // 'New Grid': () => { },
                // 'New Map Height Lines': () => { },
                // 'Load Session From Folder': () => { },
                // 'Load Session From Folder Without Viewport Size': () => { },
                // 'Add All Open Map Forms To Schemes': () => { },
            },

            'Terrain': {
                'Load from a File': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Load from a File',
                        secondDialogComponent: <SaveLoadCtrl saveLoadType={SaveLoadTypes.LOAD_TERRAIN_FROM_FILE} />
                    }))
                },
                'Load from a Buffer': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Load from a Buffer',
                        secondDialogComponent: <SaveLoadCtrl saveLoadType={SaveLoadTypes.LOAD_TERRAIN_FROM_BUFFER} />
                    }))
                },
                'Create New': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Create New Terrain',
                        secondDialogComponent: <SelectExistingTerrain saveStandAlone={true} finalActionButton={true} finalActionLabel='OK' handleFinalActionButtonClick={(...args) => mapWorldTreeService.handleCreateNewTerrainClick(...args)} />
                    }))
                },
            },
            'Layer': {
                'Load from a File': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Load from a File',
                        secondDialogComponent: <SaveLoadCtrl saveLoadType={SaveLoadTypes.LOAD_LAYER_FROM_FILE} />
                    }))
                },
                'Load from a Buffer': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Load from a Buffer',
                        secondDialogComponent: <SaveLoadCtrl saveLoadType={SaveLoadTypes.LOAD_LAYER_FROM_BUFFER} />
                    }))
                },
                'Create New': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Create New Layer',
                        secondDialogComponent: <SelectExistingLayer enableCreateNewLayer={true} finalActionButton={true}
                            finalActionLabel='OK' handleFinalActionButtonClick={(...args) => mapWorldTreeService.handleCreateNewLayerClick(...args)} />
                    }))
                },
                'Backgroung Thread Index': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Backgroung Thread Index Layers',
                        secondDialogComponent: <BackgroundThreadIndex />
                    }))
                },
            },
            'Server Layers': {
                // 'Check All Native Server Layers Validity Async': () => { },
                // 'Check Validity Before Each Render': () => { },
            },
            'Raw 3D Model': {
                // 'Build Indexing Data For Raw 3D Model': () => { },
                // 'Delete Indexing Data For Raw 3D Model': () => { },
                'Smart Reality Jump To Building': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Smart Reality Building History',
                        secondDialogComponent: <ScanItem3DModelSmartReality />
                    }))
                },
            },
            'Raw Vector 3D Extrusion': {
                // 'Build Indexing Data For Raw Vector 3D Extrusion': () => { },
                // 'Delete Indexing Data For Raw Vector 3D Extrusion': () => { },
            },
        },
        // Viewport Actions
        'Map Viewport': {
            'Create Camera': () => { mapWorldTreeService.handleCreateCameraClick() },
            'Add Terrain': () => {
                dispatch(setTypeMapWorldDialogSecond({
                    secondDialogHeader: 'Add Terrain',
                    secondDialogComponent: <SelectExistingTerrain saveStandAlone={true} finalActionButton={true} finalActionLabel='Add' handleFinalActionButtonClick={(...args) => mapWorldTreeService.handleAddTerrainToViewportClick(...args)} />
                }))
            },
            'Grid': {
                'Set Grid': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Set Grid',
                        secondDialogComponent: <SelectExistingGrid handleOKClick={(...args) => mapWorldTreeService.handleSetGridClick(...args)} />
                    }))
                },
                'Remove Grid': () => { mapWorldTreeService.handleRemoveGrid() },
            },
            'Height Lines': {
                'Set Height Lines': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Set Height Lines',
                        secondDialogComponent: <SelectExistingHeightLines handleOKClick={(...args) => mapWorldTreeService.handleSetHeightLinesClick(...args)} />
                    }))
                },
                'Remove Height Lines': () => { mapWorldTreeService.handleRemoveHeightLines() },
            },
            // 'Save Session To Folder': () => { },
            // 'Add Map Form To Schemes': () => { },
        },
        // Terrain Actions
        'Map Terrain': {
            'Layers': {
                'Add Layer': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Add Layer',
                        secondDialogComponent: <SelectExistingLayer enableCreateNewLayer={true} finalActionButton={true}
                            finalActionLabel='Add' handleFinalActionButtonClick={(...args) => mapWorldTreeService.handleAddLayerToTerrainClick(...args)}
                        />
                    }))
                },
                'Remove Layer': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Add Layer',
                        secondDialogComponent: <SelectExistingLayer enableCreateNewLayer={false} finalActionButton={true}
                            finalActionLabel='Remove' handleFinalActionButtonClick={(...args) => mapWorldTreeService.handleRemoveLayerFromTerrainClick(...args)}
                        />
                    }))
                }
            },
            'Save Terrain': {
                'Save to a File': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Save to a File',
                        secondDialogComponent: <SaveLoadCtrl saveLoadType={SaveLoadTypes.SAVE_TERRAIN_TO_FILE} />
                    }))
                },
                'Save to a Buffer': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Save to a Buffer',
                        secondDialogComponent: <SaveLoadCtrl saveLoadType={SaveLoadTypes.SAVE_TERRAIN_TO_BUFFER} />
                    }))
                },
            },
            'Remove Terrain': () => {
                dispatch(setTypeMapWorldDialogSecond({
                    secondDialogHeader: '',
                    secondDialogComponent: <div style={{ width: `${globalSizeFactor * 20}vh` }} className='form__column-container'>
                        <div>Remove Terrain?</div>
                        <br />
                        <div className='form__apply-buttons-container'>
                            <Button label='Yes' onClick={() => { mapWorldTreeService.handleRemoveTerrainClick() }} />
                            <Button label='No' onClick={() => dispatch(setTypeMapWorldDialogSecond(undefined))} />
                        </div>
                    </div>
                }))
            },
        },
        // Camera Actions
        'Map Camera': {
            // 'Destroy Camera': () => { },
            // 'Set Active': () => { },
        },
        // Layer Actions
        'Map Layer': {
            'Save Layer': {
                'Save to a File': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Save to a File',
                        secondDialogComponent: <SaveLoadCtrl saveLoadType={SaveLoadTypes.SAVE_LAYER_TO_FILE} />
                    }))
                },
                'Save to a Buffer': () => {
                    dispatch(setTypeMapWorldDialogSecond({
                        secondDialogHeader: 'Save to a Buffer',
                        secondDialogComponent: <SaveLoadCtrl saveLoadType={SaveLoadTypes.SAVE_LAYER_TO_BUFFER} />
                    }))
                },
            },
            'Remove Layer': () => { mapWorldTreeService.handleRemoveLayerClick() },
            'Move To Center': () => { mapWorldTreeService.handleMoveToCenterClick() },
        },
        // Grid Actions
        'Map Grid': {
            // 'Delete': () => { },
        },
        // Height Lines Actions
        'Map Height Lines': {
            // 'Delete': () => { },
        },
    }), [treeRedux, selectedNodeInTree, expandedKeys, mapWorldTreeService])

    const formsMap = new Map([
        [mapWorldNodeType.MAP_VIEWPORT, <MapViewportForm />],
        [mapWorldNodeType.MAP_TERRAIN, <MapTerrainForm />],
        [mapWorldNodeType.MAP_CAMERA, null/*<MapCameraSchemes />*/],
        [mapWorldNodeType.MAP_GRID, null/*<MapGridForm />*/],
        [mapWorldNodeType.MAP_HEIGHT_LINES, null/*<MapHeightLinesForm />*/],
        [mapWorldNodeType.MAP_LAYER, <MapLayerForm />],
    ]);

    const getMapWorldTreeFooter = () => {
        return <div className='tree__form-footer'>
            <div className='form__apply-buttons-container tree__form-content'>
                <Button label='OK' tooltip="Apply + Close" tooltipOptions={{ position: 'top', showDelay: 500, hideDelay: 500 }} onClick={e => {
                    handleApplyFunc()
                    dispatch(closeDialog(DialogTypesEnum.objectWorldTree))
                }} />
                <Button label='Apply' onClick={() => {
                    handleApplyFunc()
                }} />
                <Button label='Close' onClick={e => {
                    dispatch(closeDialog(DialogTypesEnum.objectWorldTree))
                }} />
            </div>
        </div>
    }
    //UseEffects
    useEffect(() => {
        setMapWorldTreeDialogWidth();
        updateTreeRedux();
        return () => {
            dispatch(setSelectedNodeInTree(null));
            // unShowNodePoints();
        }
    }, []);
    useEffect(() => {
        props.FooterHook(getMapWorldTreeFooter);
    }, [handleApplyFunc]);

    useEffect(() => {
        if (desiredNodeType) {
            selectedNodeRef.current?.scrollIntoView({
                behavior: 'smooth'
            })
            setDesiredNodeType(null);
        }
    }, [expandedKeys])

    let nodeActions: any;
    let nodeMenuItemsCommand: any;
    useEffect(() => {//fill the dynamic contextMenu whenever selectedNode changes
        runCodeSafely(() => {
            // let bb = selectedNodeRef.current?.getBoundingClientRect() --connect to rename
            // setSelectedNodeBoundingBox(bb); --connect to rename
            let contextMenu: { separator?: boolean, label?: string, command?: () => void, items?: any[] }[] = [];
            switch (selectedNodeInTree?.nodeType) {
                // degree 1
                case mapWorldNodeType.ROOT:
                    nodeActions = treeNodeActions.ROOT.actions;
                    nodeMenuItemsCommand = menuItemsCommand[mapWorldNodeType.ROOT];
                    contextMenu = [...contextMenu,
                    // { label: nodeActions.VIEWPORT.label, items: [
                    // { label: nodeActions.VIEWPORT.actions.NEW_GRID, command: () => { nodeMenuItemsCommand[nodeActions.VIEWPORT.label](nodeActions.VIEWPORT.actions.NEW_GRID) } },
                    // { label: nodeActions.VIEWPORT.actions.NEW_MAP_HEIGHT_LINES, command: () => { nodeMenuItemsCommand[nodeActions.VIEWPORT.label](nodeActions.VIEWPORT.actions.NEW_MAP_HEIGHT_LINES) } },
                    // { label: nodeActions.VIEWPORT.actions.LOAD_SESSION_FROM_FOLDER, command: () => { nodeMenuItemsCommand[nodeActions.VIEWPORT.label](nodeActions.VIEWPORT.actions.LOAD_SESSION_FROM_FOLDER) } },
                    // { label: nodeActions.VIEWPORT.actions.LOAD_SESSION_FORM_FOLDER_WITHOUT_VIEWPORT_SIZE, command: () => { nodeMenuItemsCommand[nodeActions.VIEWPORT.label](nodeActions.VIEWPORT.actions.LOAD_SESSION_FORM_FOLDER_WITHOUT_VIEWPORT_SIZE) } },
                    // { label: nodeActions.VIEWPORT.actions.ADD_ALL_OPEN_MAP_FORMS_TO_SCHEMES, command: () => { nodeMenuItemsCommand[nodeActions.VIEWPORT.label](nodeActions.VIEWPORT.actions.ADD_ALL_OPEN_MAP_FORMS_TO_SCHEMES) } },
                    // ] },
                    {
                        label: nodeActions.TERRAIN.label, items: [
                            { label: nodeActions.TERRAIN.actions.LOAD_FROM_A_FILE, command: () => { nodeMenuItemsCommand[nodeActions.TERRAIN.label][nodeActions.TERRAIN.actions.LOAD_FROM_A_FILE]() } },
                            { label: nodeActions.TERRAIN.actions.LOAD_FROM_A_BUFFER, command: () => { nodeMenuItemsCommand[nodeActions.TERRAIN.label][nodeActions.TERRAIN.actions.LOAD_FROM_A_BUFFER]() } },
                            { label: nodeActions.TERRAIN.actions.CREATE_NEW, command: () => { nodeMenuItemsCommand[nodeActions.TERRAIN.label][nodeActions.TERRAIN.actions.CREATE_NEW]() } },
                        ]
                    },
                    {
                        label: nodeActions.LAYER.label, items: [
                            { label: nodeActions.LAYER.actions.LOAD_FROM_A_FILE, command: () => { nodeMenuItemsCommand[nodeActions.LAYER.label][nodeActions.LAYER.actions.LOAD_FROM_A_FILE]() } },
                            { label: nodeActions.LAYER.actions.LOAD_FROM_A_BUFFER, command: () => { nodeMenuItemsCommand[nodeActions.LAYER.label][nodeActions.LAYER.actions.LOAD_FROM_A_BUFFER]() } },
                            { label: nodeActions.LAYER.actions.CREATE_NEW, command: () => { nodeMenuItemsCommand[nodeActions.LAYER.label][nodeActions.LAYER.actions.CREATE_NEW]() } },
                            { label: nodeActions.LAYER.actions.BACKGROUNG_THREAD_INDEX, command: () => { nodeMenuItemsCommand[nodeActions.LAYER.label][nodeActions.LAYER.actions.BACKGROUNG_THREAD_INDEX]() } },
                        ]
                    },
                    // { label: nodeActions.SERVER_LAYERS.label, items: [
                    // { label: nodeActions.SERVER_LAYERS.actions.CHECK_ALL_NATIVE_SERVER_LAYERS_VALIDITY_ASYNC, command: () => { nodeMenuItemsCommand[nodeActions.SERVER_LAYERS.label](nodeActions.SERVER_LAYERS.actions.CHECK_ALL_NATIVE_SERVER_LAYERS_VALIDITY_ASYNC) } },
                    // { label: nodeActions.SERVER_LAYERS.actions.CHECK_VALIDITY_BEFORE_EACH_RENDER, command: () => { nodeMenuItemsCommand[nodeActions.SERVER_LAYERS.label](nodeActions.SERVER_LAYERS.actions.CHECK_VALIDITY_BEFORE_EACH_RENDER) } },
                    //    ] },
                    {
                        label: nodeActions.RAW_3D_MODEL.label, items: [
                            // { label: nodeActions.RAW_3D_MODEL.actions.BUILD_INDEXING_DATA_FOR_RAW_3D_MODEL, command: () => { nodeMenuItemsCommand[nodeActions.RAW_3D_MODEL.label](nodeActions.RAW_3D_MODEL.actions.BUILD_INDEXING_DATA_FOR_RAW_3D_MODEL) } },
                            // { label: nodeActions.RAW_3D_MODEL.actions.DELETE_INDEXING_DATA_FOR_RAW_3D_MODEL, command: () => { nodeMenuItemsCommand[nodeActions.RAW_3D_MODEL.label](nodeActions.RAW_3D_MODEL.actions.DELETE_INDEXING_DATA_FOR_RAW_3D_MODEL) } },
                            { label: nodeActions.RAW_3D_MODEL.actions.SMART_REALITY_JUMP_TO_BUILDING, command: () => { nodeMenuItemsCommand[nodeActions.RAW_3D_MODEL.label][nodeActions.RAW_3D_MODEL.actions.SMART_REALITY_JUMP_TO_BUILDING]() } },
                        ]
                    },
                    //     { label: nodeActions.RAW_VECTOR_3D_EXTRUSION.label, items: [
                    // { label: nodeActions.RAW_VECTOR_3D_EXTRUSION.actions.BUILD_NDEXING_DATA_FOR_RAW_VECTOR_3D_EXTRUSION, command: () => { nodeMenuItemsCommand[nodeActions.RAW_VECTOR_3D_EXTRUSION.label](nodeActions.RAW_VECTOR_3D_EXTRUSION.actions.BUILD_NDEXING_DATA_FOR_RAW_VECTOR_3D_EXTRUSION) } },
                    // { label: nodeActions.RAW_VECTOR_3D_EXTRUSION.actions.DELETE_INDEXING_DATA_FOR_RAW_VECTOR_3D_EXTRUSION, command: () => { nodeMenuItemsCommand[nodeActions.RAW_VECTOR_3D_EXTRUSION.label](nodeActions.RAW_VECTOR_3D_EXTRUSION.actions.DELETE_INDEXING_DATA_FOR_RAW_VECTOR_3D_EXTRUSION) } },
                    //    ] },
                    { separator: true },
                    { label: treeNodeActions.EXPAND.label, command: () => { menuItemsCommand[treeNodeActions.EXPAND.label]() } },
                    { label: treeNodeActions.COLLAPSE.label, command: () => { menuItemsCommand[treeNodeActions.COLLAPSE.label]() } },
                    ];
                    break;
                // degree 2
                case mapWorldNodeType.MAP_VIEWPORT:
                    nodeActions = treeNodeActions.MAP_VIEWPORT.actions;
                    nodeMenuItemsCommand = menuItemsCommand[mapWorldNodeType.MAP_VIEWPORT];
                    contextMenu = [...contextMenu,
                    { label: nodeActions.CREATE_CAMERA.label, command: () => { nodeMenuItemsCommand[nodeActions.CREATE_CAMERA.label]() } },
                    { label: nodeActions.ADD_TERRAIN.label, command: () => { nodeMenuItemsCommand[nodeActions.ADD_TERRAIN.label]() } },
                    {
                        label: nodeActions.GRID.label, items: [
                            { label: nodeActions.GRID.actions.SET_GRID, command: () => { nodeMenuItemsCommand[nodeActions.GRID.label][nodeActions.GRID.actions.SET_GRID]() } },
                            { label: nodeActions.GRID.actions.REMOVE_GRID, command: () => { nodeMenuItemsCommand[nodeActions.GRID.label][nodeActions.GRID.actions.REMOVE_GRID]() } },
                        ]
                    },
                    {
                        label: nodeActions.HEIGHT_LINES.label, items: [
                            { label: nodeActions.HEIGHT_LINES.actions.SET_HEIGHT_LINES, command: () => { nodeMenuItemsCommand[nodeActions.HEIGHT_LINES.label][nodeActions.HEIGHT_LINES.actions.SET_HEIGHT_LINES]() } },
                            { label: nodeActions.HEIGHT_LINES.actions.REMOVE_HEIGHT_LINES, command: () => { nodeMenuItemsCommand[nodeActions.HEIGHT_LINES.label][nodeActions.HEIGHT_LINES.actions.REMOVE_HEIGHT_LINES]() } },
                        ]
                    },
                    // { label: nodeActions.SAVE_SESSION_TO_FOLDER.label, command: () => { nodeMenuItemsCommand[nodeActions.SAVE_SESSION_TO_FOLDER.label](nodeActions.SAVE_SESSION_TO_FOLDER.label) } },
                    // { label: nodeActions.ADD_MAP_FORM_TO_SCHEMES.label, command: () => { nodeMenuItemsCommand[nodeActions.ADD_MAP_FORM_TO_SCHEMES.label](nodeActions.ADD_MAP_FORM_TO_SCHEMES.label) } },
                    { separator: true },
                    // { label: treeNodeActions.RENAME.label, command: () => { menuItemsCommand[treeNodeActions.RENAME.label](treeNodeActions.RENAME.label) } },
                    // { label: treeNodeActions.DELETE_NAME.label, command: () => { menuItemsCommand[treeNodeActions.DELETE_NAME.label](treeNodeActions.DELETE_NAME.label) } },
                    { separator: true },
                    { label: treeNodeActions.EXPAND.label, command: () => { menuItemsCommand[treeNodeActions.EXPAND.label]() } },
                    { label: treeNodeActions.COLLAPSE.label, command: () => { menuItemsCommand[treeNodeActions.COLLAPSE.label]() } },
                    ];
                    break;
                // degree 3
                case mapWorldNodeType.MAP_TERRAIN:
                    nodeActions = treeNodeActions.MAP_TERRAIN.actions;
                    nodeMenuItemsCommand = menuItemsCommand[mapWorldNodeType.MAP_TERRAIN];
                    contextMenu = [...contextMenu,
                    {
                        label: nodeActions.LAYERS.label, items: [
                            { label: nodeActions.LAYERS.actions.ADD_LAYER, command: () => { nodeMenuItemsCommand[nodeActions.LAYERS.label][nodeActions.LAYERS.actions.ADD_LAYER]() } },
                            { label: nodeActions.LAYERS.actions.REMOVE_LAYER, command: () => { nodeMenuItemsCommand[nodeActions.LAYERS.label][nodeActions.LAYERS.actions.REMOVE_LAYER]() } },
                        ]
                    },
                    {
                        label: nodeActions.SAVE_TERRAIN.label, items: [
                            { label: nodeActions.SAVE_TERRAIN.actions.SAVE_TO_A_FILE, command: () => { nodeMenuItemsCommand[nodeActions.SAVE_TERRAIN.label][nodeActions.SAVE_TERRAIN.actions.SAVE_TO_A_FILE]() } },
                            { label: nodeActions.SAVE_TERRAIN.actions.SAVE_TO_A_BUFFER, command: () => { nodeMenuItemsCommand[nodeActions.SAVE_TERRAIN.label][nodeActions.SAVE_TERRAIN.actions.SAVE_TO_A_BUFFER]() } },
                        ]
                    },
                    { label: nodeActions.REMOVE_TERRAIN.label, command: () => { nodeMenuItemsCommand[nodeActions.REMOVE_TERRAIN.label]() } },
                    // { separator: true },
                    // { label: treeNodeActions.RENAME.label, command: () => { nodeMenuItemsCommand[treeNodeActions.RENAME.label][treeNodeActions.RENAME.label] } },
                    // { label: treeNodeActions.DELETE_NAME.label, command: () => { nodeMenuItemsCommand[treeNodeActions.DELETE_NAME.label][treeNodeActions.DELETE_NAME.label] } },
                    { separator: true },
                    { label: treeNodeActions.EXPAND.label, command: () => { menuItemsCommand[treeNodeActions.EXPAND.label]() } },
                    { label: treeNodeActions.COLLAPSE.label, command: () => { menuItemsCommand[treeNodeActions.COLLAPSE.label]() } },
                    ];
                    break;
                case mapWorldNodeType.MAP_CAMERA:
                    nodeActions = treeNodeActions.MAP_CAMERA.actions;
                    nodeMenuItemsCommand = menuItemsCommand[mapWorldNodeType.MAP_CAMERA];
                    contextMenu = [...contextMenu,
                        // { label: nodeActions.DESTROY_CAMERA.label, command: () => { nodeMenuItemsCommand[nodeActions.DESTROY_CAMERA.label](nodeActions.DESTROY_CAMERA.label) } },
                        // { label: nodeActions.SET_ACTIVE.label, command: () => { nodeMenuItemsCommand[nodeActions.SET_ACTIVE.label](nodeActions.SET_ACTIVE.label) } },
                    ];
                    break;
                case mapWorldNodeType.MAP_GRID:
                    nodeActions = treeNodeActions.MAP_GRID.actions;
                    nodeMenuItemsCommand = menuItemsCommand[mapWorldNodeType.MAP_GRID];
                    contextMenu = [...contextMenu,
                        // { label: nodeActions.DELETE.label, command: () => { nodeMenuItemsCommand[nodeActions.DELETE.label](nodeActions.DELETE.label) } },
                    ];
                    break;
                case mapWorldNodeType.MAP_HEIGHT_LINES:
                    nodeActions = treeNodeActions.MAP_HEIGHT_LINES.actions;
                    nodeMenuItemsCommand = menuItemsCommand[mapWorldNodeType.MAP_HEIGHT_LINES];
                    contextMenu = [...contextMenu,
                        // { label: nodeActions.DELETE.label, command: () => { nodeMenuItemsCommand[nodeActions.DELETE.label](nodeActions.DELETE.label) } },
                    ];
                    break;
                // degree 4
                case mapWorldNodeType.MAP_LAYER:
                    nodeActions = treeNodeActions.MAP_LAYER.actions;
                    nodeMenuItemsCommand = menuItemsCommand[mapWorldNodeType.MAP_LAYER];
                    contextMenu = [...contextMenu,
                    {
                        label: nodeActions.SAVE_LAYER.label, items: [
                            { label: nodeActions.SAVE_LAYER.actions.SAVE_TO_A_FILE, command: () => { nodeMenuItemsCommand[nodeActions.SAVE_LAYER.label][nodeActions.SAVE_LAYER.actions.SAVE_TO_A_FILE]() } },
                            { label: nodeActions.SAVE_LAYER.actions.SAVE_TO_A_BUFFER, command: () => { nodeMenuItemsCommand[nodeActions.SAVE_LAYER.label][nodeActions.SAVE_LAYER.actions.SAVE_TO_A_BUFFER]() } },
                        ]
                    },
                    { label: nodeActions.REMOVE_LAYER.label, command: () => { nodeMenuItemsCommand[nodeActions.REMOVE_LAYER.label]() } },
                    { separator: true },
                    { label: nodeActions.MOVE_TO_CENTER.label, command: () => { nodeMenuItemsCommand[nodeActions.MOVE_TO_CENTER.label]() } },
                    { separator: true },
                        // { label: treeNodeActions.RENAME.label, command: () => { menuItemsCommand[treeNodeActions.RENAME.label](treeNodeActions.RENAME.label) } },
                        // { label: treeNodeActions.DELETE_NAME.label, command: () => { menuItemsCommand[treeNodeActions.DELETE_NAME.label](treeNodeActions.DELETE_NAME.label) } },
                    ];
                    break;
                default:
                    break;
            }
            setDynamicCM(contextMenu);
        }, "mapWorldTreeDialog.useEffect");
    }, [selectedNodeInTree, treeRedux])

    //Handle Events
    const handleKeyUp = (e: any) => {
        if (e.ctrlKey && e.key == 's') {
            // saveExistObjectInFileNameMapToFile();
            //  e.preventDefault();
        }
    }
    const handleKeyDown = (e: any) => {
        if (e.ctrlKey || e.key == 'Control') {
            e.preventDefault();
        }
    }
    // const handleKeyDown: React.KeyboardEventHandler<HTMLDivElement> = (e) => { --connect to rename
    //     runCodeSafely(() => {
    //         if (e.key == 'Enter' && renameMode) {
    //             let renamedTree = mapWorldTreeService.renameNode(treeRedux, selectedNodeInTree.key, renameValue);
    //             dispatch(setObjectWorldTree(renamedTree));
    //             setRenameMode(false);
    //         }
    //     }, "mapWorldTreeDialog.handleKeyDown => onKeyDown")
    // }
    // const handleClick: React.MouseEventHandler<HTMLDivElement> = (e) => { --connect to rename
    //     runCodeSafely(() => {
    //         if (!isInputDrag) {
    //             setRenameMode(false)
    //         }
    //         else {
    //             setIsInputDrag(false)
    //         }
    //     }, "mapWorldTreeDialog.handleClick => onClick")
    // }

    //Funcs
    function setMapWorldTreeDialogWidth() {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 1 * globalSizeFactor;
        root.style.setProperty('--map-world-tree-dialog-width', `${pixelWidth}px`);
    }
    function updateTreeRedux() {
        runCodeSafely(() => {
            let buildedMapTree: MapTreeNode = mapWorldTreeService.buildTree();
            dispatch(setMapWorldTree(buildedMapTree));
            let buildedObjectTree: ObjectTreeNode = objectWorldTreeService.buildTree();
            dispatch(setObjectWorldTree(buildedObjectTree));
        }, "mapWorldTreeDialog.useEffect");
    }
    function getContextMenuByNodeType(event: React.MouseEvent<HTMLDivElement>, node: TreeNodeModel): void {
        runCodeSafely(() => {
            // setSelectedNode(node); --connect to rename
            dispatch(setSelectedNodeInTree(node));
            event.preventDefault();
        }, "mapWorldTreeDialog.getContextMenuByNodeType => onContextMenu")
    }

    return (
        <div>
            {/* <Menubar style={{ zIndex: '4', padding: `${globalSizeFactor * 0}vh` }} model={menuBarItems} /> */}
            <div
                className='tree__container'
            //   onKeyDown={handleKeyDown} --connect to rename
            // onClick={handleClick} --connect to rename
            >
                <div className='tree__tree'>
                    <Tree style={{ minHeight: `${globalSizeFactor * 70}vh`, zIndex: 4, padding: `${globalSizeFactor * 0}vh` }}
                        value={[treeRedux]}
                        onContextMenu={(e: any) => {
                            // setRenameMode(false); --connect to rename
                            getContextMenuByNodeType(e.originalEvent, e.node); cm.current.show(e.originalEvent)
                        }}
                        selectionMode="single"
                        selectionKeys={selectedNodeInTree?.key}
                        onSelectionChange={(e: any) => {
                            if (e.value !== selectedNodeInTree?.key) {
                                // dispatch(setIsFormOpen(true));
                                dispatch(setSelectedNodeInTree(mapWorldTreeService.getNodeByKey(treeRedux, e.value) as TreeNodeModel));
                            }
                        }}
                        expandedKeys={expandedKeys}
                        onToggle={(e: any) => { setExpandedKeys(e.value) }}
                        nodeTemplate={(node) => (
                            <div ref={node.key === selectedNodeInTree?.key ? selectedNodeRef : null}>{node.label}</div>
                        )}
                    />
                </div>

                {/* {renameMode && getRenameInput()} --connect to rename*/}

                <div className='tree__form'>
                    {formsMap.get(selectedNodeInTree?.nodeType)}
                </div>

                <ContextMenu model={dynamicCM} ref={cm} />

                {typeMapWorldDialogSecond && <Dialog
                    className={showDialog.hideFormReason !== null ? `object-props__hidden-dialog ${mapWorldTreeService.dialogClassNames.get(typeMapWorldDialogSecond.secondDialogHeader)}` : `${mapWorldTreeService.dialogClassNames.get(typeMapWorldDialogSecond.secondDialogHeader)}`}
                    header={typeMapWorldDialogSecond.secondDialogHeader}
                    onHide={() => {
                        dispatch(setTypeMapWorldDialogSecond(undefined));
                        typeMapWorldDialogSecond.onHide && typeMapWorldDialogSecond.onHide();
                    }}
                    modal={!showDialog.hideFormReason ? typeMapWorldDialogSecond.modal : false}
                    footer={typeMapWorldDialogSecond.footerComponent}
                    visible>
                    {typeMapWorldDialogSecond.secondDialogComponent}
                </Dialog>}
                {/* {}
                <ConfirmDialog
                contentClassName='form__confirm-dialog-content'
                    message={confirmDialogMessage}
                    header=''
                    footer={<div></div>}
                    visible={isConfirmDialog}
                    onHide={e => { setIsConfirmDialog(false) }}
                /> */}
            </div>
        </div>
    );
}

