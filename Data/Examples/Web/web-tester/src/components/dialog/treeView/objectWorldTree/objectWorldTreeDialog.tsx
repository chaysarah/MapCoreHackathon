import React, { ReactElement, useEffect, useMemo, useRef, useState } from 'react';
import { Tree, TreeExpandedKeysType } from 'primereact/tree';
import { ContextMenu } from 'primereact/contextmenu';
import { useDispatch, useSelector } from 'react-redux';
import { Dialog } from 'primereact/dialog';
import { ConfirmDialog } from 'primereact/confirmdialog';
import { Menubar } from 'primereact/menubar';
import { Checkbox } from 'primereact/checkbox';
import { Button } from 'primereact/button';
import { Dropdown } from 'primereact/dropdown';

import { ObjectWorldService, MapCoreData, ViewportData, getEnumDetailsList } from 'mapcore-lib';
import SchemeObjectsList from './shared/schemeObjectsList';
import ItemForm from './itemForms/itemForm';
import CloneObject from './objectForms/cloneObject';
import MoveObjectToOtherOverlay from './objectForms/moveObjectToOtherOverlay';
import ReplaceScheme from './objectForms/replaceScheme';
import OverlayManagerForm from './overlayManagerForms/overlayManagerForm';
import ObjectSchemes from './objectSchemes/objectSchemes';
import AddObjectFromScheme from './overlayForms/addObjectFromScheme';
import SaveToFile from './overlayForms/saveToFile';
import ObjectForm from './objectForms/objectForm';
import ObjectLocationForm from './objectLocationForms/objectLocationForm';
import BaseConditionalSelector from './conditionalSelectorForms/baseConditionalSelector';
import OverlayForm from './overlayForms/overlayForm';
import '../sharedStyles/tree.css'
import TreeNodeModel, { objectWorldNodeType, treeNodeActions, conditionalSelectorActions, hideFormReasons } from '../../../shared/models/tree-node.model';
import SecondDialogModel from '../../../shared/models/second-dialog.model';
import { runCodeSafely, runMapCoreSafely } from '../../../../common/services/error-handling/errorHandler';
import { AppState } from '../../../../redux/combineReducer';
import { addSavedObjectDataToMap, removeSavedObjectFilePathFromMap, setFooterVersion, setLocationSelectorObjectArr, setObjectWorldTree, setSelectedNodeInTree, setShowDialog, setTypeObjectWorldDialogSecond } from '../../../../redux/ObjectWorldTree/objectWorldTreeActions';
import { closeDialog } from '../../../../redux/MapCore/mapCoreAction';
import objectWorldTreeService from '../../../../services/objectWorldTree.service';
import { DialogTypesEnum } from '../../../../tools/enum/enums';
import savedFilesService, { SavedFileData } from '../../../../services/savedFiles.service';

export default function OpenObjectWorldDialog(props: { FooterHook: (footer: () => ReactElement) => void }) {
    let cm = useRef<ContextMenu>();
    let selectedNodeRef = useRef(null);
    const dispatch = useDispatch();
    const treeRedux: TreeNodeModel = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let selectedNodeInTree = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const typeObjectWorldDialogSecond: SecondDialogModel = useSelector((state: AppState) => state.objectWorldTreeReducer.typeObjectWorldDialogSecond);
    let savedObjectDataMap = useSelector((state: AppState) => state.objectWorldTreeReducer.savedObjectDataMap);
    const showDialog: { hideFormReason: hideFormReasons, dialogType: DialogTypesEnum } = useSelector((state: AppState) => state.objectWorldTreeReducer.showDialog);
    const activeViewport: ViewportData = useSelector((state: AppState) => MapCoreData.findViewport(state.mapWindowReducer.activeCard));
    const screenPos: any = useSelector((state: AppState) => state.mapWindowReducer.screenPos);
    let locationSelectorObjects = useSelector((state: AppState) => state.objectWorldTreeReducer.locationSelectorObjects);
    let selectedFooterVersion = useSelector((state: AppState) => state.objectWorldTreeReducer.footerVersion);
    let currentVersion: MapCore.IMcOverlayManager.ESavingVersionCompatibility = useSelector((state: AppState) => state.objectWorldTreeReducer.footerVersion).theEnum;
    const handleApplyFunc = useSelector((state: AppState) => state.objectWorldTreeReducer.handleApplyFunc);

    let [dynamicCM, setDynamicCM] = useState<{ separator?: boolean, label?: string, command?: () => void, items?: any[] }[]>([]);
    // let [selectedNodeBoundingBox, setSelectedNodeBoundingBox] = useState<DOMRect>(); --connect to rename
    // let [renameValue, setRenameValue] = useState<string>(''); --connect to rename
    // let [renameMode, setRenameMode] = useState<boolean>(false); --connect to rename
    // let [isInputDrag, setIsInputDrag] = useState<boolean>(false); --connect to rename
    let [desiredNodeType, setDesiredNodeType] = useState<string>(null);
    let [isConfirmDialog, setIsConfirmDialog] = useState<boolean>(false);
    let [confirmDialogMessage, setConfirmDialogMessage] = useState<string>(null);
    let [expandedKeys, setExpandedKeys] = useState<TreeExpandedKeysType>({});
    let [showNodePointsType, setShowNodePointsType] = useState(treeNodeActions.OFF);
    let [showNodePointsOverlayAndObject, setShowNodePointsOverlayAndObject] = useState<{ overlay: MapCore.IMcOverlay, object: MapCore.IMcObject }>(null);
    let showNodePointsOverlayAndObjectRef = useRef<{ overlay: MapCore.IMcOverlay, object: MapCore.IMcObject }>(null);

    const savedFileTypeOptions = useMemo(() => ([
        { name: 'MapCore Object Files (*.mcobj, *.mcobj.json , *.m, *.json)', extension: '.mcobj' },
        { name: 'MapCore Object Binary Files (*.mcobj,*.m)', extension: '.mcobj' },
        { name: 'MapCore Object Json Files (*.mcobj.json, *.json)', extension: '.mcobj.json' },
        { name: 'All Files', extension: '' },
    ]), [])
    const originalVersionEnum = { name: 'Objects/scheme\'s current version', code: -1, theEnum: null }
    const EVersionList = useMemo(() => [...getEnumDetailsList(MapCore.IMcOverlayManager.ESavingVersionCompatibility), originalVersionEnum], []);

    const getMenuBarTemplate = (menuBar: any) => {
        return <a className="flex align-items-center p-menuitem-link">
            <Checkbox inputId="menuBarCheckBox" onChange={() => { setShowNodePointsType(menuBar.label) }} checked={showNodePointsType == menuBar.label} />
            <label htmlFor="menuBarCheckBox">{menuBar.label}</label>
        </a>
    }
    const menuItemsCommand = useMemo(() => ({
        //Shared Actions
        'Expand': () => { runCodeSafely(() => { setExpandedKeys({ ...expandedKeys, ...objectWorldTreeService.expandNode(treeRedux, selectedNodeInTree.key) }) }, "objectWorldTreeDialog.expand => onClick") },
        'Collapse': () => { runCodeSafely(() => { setExpandedKeys(objectWorldTreeService.collapseNode(treeRedux, selectedNodeInTree.key, expandedKeys)) }, "objectWorldTreeDialog.collpse => onClick") },
        // 'Rename': () => { --connect to rename
        //     let end = selectedNodeInTree.label.lastIndexOf(')');
        //     setRenameValue(`${selectedNodeInTree.label.substring(1, end)}`);
        //     setRenameMode(true);
        // },

        //Root Actions
        'Find Overlay Manager': () => {
            setDesiredNodeType(objectWorldNodeType.OVERLAY_MANAGER)
            dispatch(setShowDialog({ hideFormReason: hideFormReasons.CHOOSE_POINT, dialogType: DialogTypesEnum.objectWorldTree }));
        },
        'Find Object': () => {
            setDesiredNodeType(objectWorldNodeType.OBJECT)
            dispatch(setShowDialog({ hideFormReason: hideFormReasons.CHOOSE_POINT, dialogType: DialogTypesEnum.objectWorldTree }));
        },
        'Find Item': () => {
            setDesiredNodeType(objectWorldNodeType.ITEM)
            dispatch(setShowDialog({ hideFormReason: hideFormReasons.CHOOSE_POINT, dialogType: DialogTypesEnum.objectWorldTree }));
        },
        //Overlay Manager Actions
        'Conditional Selector': (type: any) => {
            dispatch(setTypeObjectWorldDialogSecond({
                secondDialogHeader: 'Conditional Selector Parameters',
                secondDialogComponent: <BaseConditionalSelector conditionalSType={type} />
            }))
        },
        'Create Overlay': () => {
            let overlay = MapCore.IMcOverlay.Create(selectedNodeInTree.nodeMcContent);
            updateTreeRedux();
            dispatch(setSelectedNodeInTree(treeRedux));
        },
        //Overlay Actions
        'New Object Based On Existing Scheme': () => {
            runCodeSafely(() => {
                dispatch(setTypeObjectWorldDialogSecond({
                    secondDialogHeader: "Add Object",
                    secondDialogComponent: <AddObjectFromScheme />
                }))
            }, "overlayManagerGeneralForm.showMoreDetails")
        },
        'Remove Overlay': () => {
            runCodeSafely(() => {
                objectWorldTreeService.removeOverlay(treeRedux, selectedNodeInTree);
                updateTreeRedux();
                dispatch(setSelectedNodeInTree(treeRedux));
            }, "objectWorldTreeDialog.removeOverlay => onClick");
        },
        'Remove All Objects': () => {
            let overlayObjects = selectedNodeInTree.children.filter(child => child.nodeType == objectWorldNodeType.OBJECT);
            overlayObjects.forEach((child => {
                objectWorldTreeService.removeObject(treeRedux, child);
                removeObjectFromSelectorsObjects(child.nodeMcContent);
            }));
            updateTreeRedux();
        },
        'Set Active': () => {
            let OM = objectWorldTreeService.getParentByChildKey(treeRedux, selectedNodeInTree.key).nodeMcContent;
            ObjectWorldService.setActiveOverlayInOverlayManager(OM, selectedNodeInTree.nodeMcContent);
        },
        //Object Actions
        'Clone': () => {
            runCodeSafely(() => {
                dispatch(setTypeObjectWorldDialogSecond({
                    secondDialogHeader: "Clone Object",
                    secondDialogComponent: <CloneObject />
                }))
            }, "ObjectWorldTreeService.Clone")
        },
        'Remove Object': () => {
            runCodeSafely(() => {
                objectWorldTreeService.removeObject(treeRedux, selectedNodeInTree);
                removeObjectFromSelectorsObjects(selectedNodeInTree.nodeMcContent);
                updateTreeRedux();
                dispatch(setSelectedNodeInTree(treeRedux));
            }, "objectWorldTreeDialog.removeObject => onClick");
        },
        'Move To Other Overlay': () => {
            runCodeSafely(() => {
                dispatch(setTypeObjectWorldDialogSecond({
                    secondDialogHeader: "Move To Other Overlay",
                    secondDialogComponent: <MoveObjectToOtherOverlay />
                }))
            }, "objectWorldTreeDialog.MoveToOtherOverlay")
        },
        'Replace Scheme': () => {
            runCodeSafely(() => {
                dispatch(setTypeObjectWorldDialogSecond({
                    secondDialogHeader: "Replace Scheme",
                    secondDialogComponent: <ReplaceScheme />
                }))
            }, "objectWorldTreeDialog.ReplaceScheme")
        },
        'Move To Location': (locationIndex: number) => {
            runCodeSafely(() => {
                let returnedMessage = objectWorldTreeService.moveToLocation(selectedNodeInTree, locationIndex, activeViewport?.viewport);
                if (returnedMessage) {
                    setConfirmDialogMessage(returnedMessage)
                    setIsConfirmDialog(true);
                }
            }, 'objectWorldTreeDialog.MoveToLocation')
        },
        'Jump To Scheme': () => {
            let mcCurrentObject = selectedNodeInTree.nodeMcContent as MapCore.IMcObject;
            let scheme = mcCurrentObject.GetScheme();
            let schemeTesterNode = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, scheme);
            dispatch(setSelectedNodeInTree(schemeTesterNode as TreeNodeModel));
        },
        'Save': () => {
            saveObject();
        },
        'Save As': () => {
            runCodeSafely(() => {
                dispatch(setTypeObjectWorldDialogSecond({
                    secondDialogHeader: 'Save to File',
                    secondDialogComponent: <SaveToFile
                        savedFileTypeOptions={savedFileTypeOptions}
                        handleSaveToFileOk={handleSaveObjectToFileOK} />
                }))
            }, "objectWorldTreeDialog.saveAs => onClick");
        },
        'Stop Save': () => {
            dispatch(removeSavedObjectFilePathFromMap(selectedNodeInTree.nodeMcContent));
        },
        //Object Location Actions
        'Remove object location': () => {
            runCodeSafely(() => {
                objectWorldTreeService.removeObjectLocation(treeRedux, selectedNodeInTree.key)
                updateTreeRedux();
                dispatch(setSelectedNodeInTree(treeRedux));
            }, "objectWorldTreeDialog.removeObjectLocation => onClick")
        },
    }), [objectWorldTreeService, treeRedux, expandedKeys, selectedNodeInTree, savedFileTypeOptions, savedObjectDataMap]);
    const menuBarItems = [{
        label: treeNodeActions.SHOW_NODE_POINTS, items: [
            { label: treeNodeActions.BASE_POINTS, template: getMenuBarTemplate },
            { label: treeNodeActions.CALCULATED_POINTS, template: getMenuBarTemplate },
            { label: treeNodeActions.CALCULATED_AND_ADDED_POINTS, template: getMenuBarTemplate },
            { label: treeNodeActions.OFF, template: getMenuBarTemplate },
        ]
    }]
    const formsMap = new Map([
        [objectWorldNodeType.OVERLAY_MANAGER, <OverlayManagerForm />],
        [objectWorldNodeType.OVERLAY, <OverlayForm />],
        [objectWorldNodeType.OBJECT_SCHEME, <ObjectSchemes />],
        [objectWorldNodeType.OBJECT, <ObjectForm />],
        [objectWorldNodeType.OBJECT_LOCATION, <ObjectLocationForm />],
        [objectWorldNodeType.ITEM, <ItemForm />],
        [objectWorldNodeType.CONDITIONAL_SELECTOR, <BaseConditionalSelector />],
    ]);
    const getObjectWorldTreeFooter = () => {
        return <div className='tree__form-footer'>
            <div className='tree__form-content'>
                <div style={{ display: 'flex', justifyContent: 'flex-start' }}>
                    <div style={{ display: 'flex', flexDirection: 'row', alignItems: 'center' }}>
                        <label>Version Compatibility:</label>
                        <Dropdown value={selectedFooterVersion} onChange={(e) => {
                            dispatch(setFooterVersion(e.value))
                        }} options={EVersionList} optionLabel="name" />
                    </div>
                </div>
                <div style={{ display: 'flex', justifyContent: 'space-around' }}>
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
        </div>
    }
    //UseEffects
    useEffect(() => {
        setObjectWorldTreeDialogWidth();
        updateTreeRedux();
        dispatch(setFooterVersion(originalVersionEnum));

        return () => {
            dispatch(setSelectedNodeInTree(null));
            unShowNodePoints();
        }
    }, []);
    useEffect(() => {
        props.FooterHook(getObjectWorldTreeFooter);
    }, [handleApplyFunc, selectedFooterVersion]);
    useEffect(() => {
        showNodePointsOverlayAndObjectRef.current = showNodePointsOverlayAndObject;
    }, [showNodePointsOverlayAndObject])
    useEffect(() => {
        document.addEventListener('keyup', handleKeyUp);
        document.addEventListener('keydown', handleKeyDown);

        return () => {
            document.removeEventListener('keyup', handleKeyUp)
            document.removeEventListener('keydown', handleKeyDown)
        }
    }, [savedObjectDataMap, selectedNodeInTree, treeRedux]);
    useEffect(() => {
        if (desiredNodeType) {
            let foundedMcNode = ObjectWorldService.findNodeByChosenScreenPoint(screenPos, activeViewport.viewport, desiredNodeType) as MapCore.IMcBase;
            let foundedTreeNode = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, foundedMcNode);
            dispatch(setSelectedNodeInTree(foundedTreeNode as TreeNodeModel));
            menuItemsCommand[treeNodeActions.EXPAND]()
        }
    }, [screenPos])
    useEffect(() => {
        if (showNodePointsType == treeNodeActions.OFF) {
            unShowNodePoints();
        } else {
            showNodePoints();
        }
    }, [showNodePointsType])
    useEffect(() => {
        if (desiredNodeType) {
            selectedNodeRef.current?.scrollIntoView({
                behavior: 'smooth'
            })
            setDesiredNodeType(null);
        }
    }, [expandedKeys])
    useEffect(() => {
        if (showNodePointsType != treeNodeActions.OFF) {
            showNodePoints();
        }
    }, [selectedNodeInTree])
    useEffect(() => {//fill the dynamic contextMenu whenever selectedNode changes
        runCodeSafely(() => {
            // let bb = selectedNodeRef.current?.getBoundingClientRect() --connect to rename
            // setSelectedNodeBoundingBox(bb); --connect to rename
            let contextMenu: { separator?: boolean, label?: string, command?: () => void, items?: any[] }[] = getDynamicContextMenu();
            setDynamicCM(contextMenu);
        }, "objectWorldTreeDialog.useEffect");
    }, [selectedNodeInTree, treeRedux])

    //Handle Events
    const handleKeyUp = (e: any) => {
        runCodeSafely(() => {
            if (e.ctrlKey && e.key == 's') {
                saveObject();
            }
        }, "objectWorldTreeDialog.handleKeyUp");
    }
    const handleKeyDown = (e: any) => {
        runCodeSafely(() => {
            if (e.ctrlKey || e.key == 'Control') {
                e.preventDefault();
            }
        }, "objectWorldTreeDialog.handleKeyDown");
    }
    // const handleKeyDown: React.KeyboardEventHandler<HTMLDivElement> = (e) => { --connect to rename
    //     runCodeSafely(() => {
    //         if (e.key == 'Enter' && renameMode) {
    //             let renamedTree = objectWorldTreeService.renameNode(treeRedux, selectedNodeInTree.key, renameValue);
    //             dispatch(setObjectWorldTree(renamedTree));
    //             setRenameMode(false);
    //         }
    //     }, "objectWorldTreeDialog.handleKeyDown => onKeyDown")
    // }
    // const handleClick: React.MouseEventHandler<HTMLDivElement> = (e) => { --connect to rename
    //     runCodeSafely(() => {
    //         if (!isInputDrag) {
    //             setRenameMode(false)
    //         }
    //         else {
    //             setIsInputDrag(false)
    //         }
    //     }, "objectWorldTreeDialog.handleClick => onClick")
    // }
    const saveObject = () => {
        runCodeSafely(() => {
            if (!savedObjectDataMap.get(selectedNodeInTree.nodeMcContent)?.filePath) {
                dispatch(setTypeObjectWorldDialogSecond({
                    secondDialogHeader: 'Save to File',
                    secondDialogComponent: <SaveToFile
                        savedFileTypeOptions={savedFileTypeOptions}
                        handleSaveToFileOk={handleSaveObjectToFileOK} />
                }))
            }
            else {
                saveExistObjectInFileNameMapToFile();
            }
        }, "objectWorldTreeDialog.saveObject");
    }
    const saveExistObjectInFileNameMapToFile = (objToSave: TreeNodeModel = selectedNodeInTree) => {
        runCodeSafely(() => {
            let finalFileName = savedObjectDataMap.get(selectedNodeInTree.nodeMcContent)?.filePath;
            if (finalFileName) {
                let overlay: MapCore.IMcOverlay = objectWorldTreeService.getParentByChildKey(treeRedux, objToSave.key).nodeMcContent;
                let mcObjsToSave: MapCore.IMcObject[] = [objToSave.nodeMcContent];
                const { version } = savedFilesService.getObjectsOrSchemesVersion(mcObjsToSave, true);
                let format = finalFileName.endsWith('.json') ?
                    MapCore.IMcOverlayManager.EStorageFormat.ESF_JSON :
                    MapCore.IMcOverlayManager.EStorageFormat.ESF_MAPCORE_BINARY;
                let uint8ArrayData = null;
                runMapCoreSafely(() => {
                    uint8ArrayData = overlay.SaveObjects(mcObjsToSave, format, version.theEnum);
                }, 'objectWorldTreeDialog.saveExistObjectInFileNameMapToFile => IMcOverlay.SaveObjects', true)
                runMapCoreSafely(() => { MapCore.IMcMapDevice.DownloadBufferAsFile(uint8ArrayData, finalFileName); }, 'objectWorldTreeDialog.saveExistObjectInFileNameMapToFile => IMcMapDevice.DownloadBufferAsFile', true)
                const objectSaveData = new SavedFileData(version.theEnum, format, finalFileName)
                dispatch(addSavedObjectDataToMap(selectedNodeInTree.nodeMcContent, objectSaveData));
            }
        }, "objectWorldTreeDialog.saveExistObjectInFileNameMapToFile");
    }
    const removeObjectFromSelectorsObjects = (object: MapCore.IMcObject) => {
        let filteredSelectorArr = locationSelectorObjects.filter(sel => sel.object !== object);
        dispatch(setLocationSelectorObjectArr(filteredSelectorArr))
    }
    const handleSaveObjectToFileOK = (fileName: string, fileType: string) => {
        runCodeSafely(() => {
            let overlay: MapCore.IMcOverlay = objectWorldTreeService.getParentByChildKey(treeRedux, selectedNodeInTree.key).nodeMcContent;
            let mcObjsToSave: MapCore.IMcObject[] = [selectedNodeInTree.nodeMcContent];
            const { version } = savedFilesService.getObjectsOrSchemesVersion(mcObjsToSave, true);
            let format = fileType.endsWith('.json') ?
                MapCore.IMcOverlayManager.EStorageFormat.ESF_JSON :
                MapCore.IMcOverlayManager.EStorageFormat.ESF_MAPCORE_BINARY;
            let uint8ArrayData = null;
            runMapCoreSafely(() => { uint8ArrayData = overlay.SaveObjects(mcObjsToSave, format, version.theEnum); }, 'objectWorldTreeDialog.saveExistObjectInFileNameMapToFile => IMcOverlay.SaveObjects', true)
            let finalFileName = `${fileName}${fileType}`;
            runMapCoreSafely(() => { MapCore.IMcMapDevice.DownloadBufferAsFile(uint8ArrayData, finalFileName); }, 'objectWorldTreeDialog.saveExistObjectInFileNameMapToFile => IMcMapDevice.DownloadBufferAsFile', true)

            const objectSaveData = new SavedFileData(version.theEnum, format, finalFileName)
            dispatch(addSavedObjectDataToMap(selectedNodeInTree.nodeMcContent, objectSaveData));
            dispatch(setTypeObjectWorldDialogSecond(undefined))
        }, "objectWorldTreeDialog.handleSaveObjectToFileOK");
    }
    //Funcs 
    function setObjectWorldTreeDialogWidth() {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 1 * globalSizeFactor;
        root.style.setProperty('--object-world-tree-dialog-width', `${pixelWidth}px`);
    }
    function getDynamicContextMenu(treeNode?: TreeNodeModel) {
        let contextMenu: { separator?: boolean, label?: string, command?: () => void, items?: any[] }[] = [];
        runCodeSafely(() => {
            let finalNode = treeNode ? treeNode : selectedNodeInTree;
            switch (finalNode?.nodeType) {
                // degree 1
                case objectWorldNodeType.ROOT:
                    contextMenu = [...contextMenu,
                    { label: treeNodeActions.FIND_OVERLAY_MANAGER, command: () => { menuItemsCommand[treeNodeActions.FIND_OVERLAY_MANAGER]() } },
                    { label: treeNodeActions.FIND_ITEM, command: () => { menuItemsCommand[treeNodeActions.FIND_ITEM]() } },
                    { label: treeNodeActions.FIND_OBJECT, command: () => { menuItemsCommand[treeNodeActions.FIND_OBJECT]() } },
                    { separator: true },
                    { label: treeNodeActions.EXPAND, command: () => { menuItemsCommand[treeNodeActions.EXPAND]() } },
                    { label: treeNodeActions.COLLAPSE, command: () => { menuItemsCommand[treeNodeActions.COLLAPSE]() } },
                    ];
                    break;
                // degree 2
                case objectWorldNodeType.OVERLAY_MANAGER:
                    contextMenu = [...contextMenu,
                    { label: treeNodeActions.CREATE_OVERLAY, command: () => { menuItemsCommand[treeNodeActions.CREATE_OVERLAY]() } },
                    {
                        label: treeNodeActions.CREATE_CONDITIONAL_SELECTOR, items: [
                            { label: conditionalSelectorActions.BLOCKED_CONDITIONAL_SELECTOR, command: () => { menuItemsCommand[conditionalSelectorActions.CONDITIONAL_SELECTOR](MapCore.IMcBlockedConditionalSelector) } },
                            { label: conditionalSelectorActions.BOOLEAN_CONDITIONAL_SELECTOR, command: () => { menuItemsCommand[conditionalSelectorActions.CONDITIONAL_SELECTOR](MapCore.IMcBooleanConditionalSelector) } },
                            { label: conditionalSelectorActions.SCALE_CONDITIONAL_SELECTOR, command: () => { menuItemsCommand[conditionalSelectorActions.CONDITIONAL_SELECTOR](MapCore.IMcScaleConditionalSelector) } },
                            { label: conditionalSelectorActions.OBJECT_STATE_CONDITIONAL_SELECTOR, command: () => { menuItemsCommand[conditionalSelectorActions.CONDITIONAL_SELECTOR](MapCore.IMcObjectStateConditionalSelector) } },
                            { label: conditionalSelectorActions.VIEWPORT_CONDITIONAL_SELECTOR, command: () => { menuItemsCommand[conditionalSelectorActions.CONDITIONAL_SELECTOR](MapCore.IMcViewportConditionalSelector) } },
                            { label: conditionalSelectorActions.LOCATION_CONDITIONAL_SELECTOR, command: () => { menuItemsCommand[conditionalSelectorActions.CONDITIONAL_SELECTOR](MapCore.IMcLocationConditionalSelector) } }
                        ]
                    },
                    // { label: treeNodeActions.RENAME, command: () => { menuItemsCommand[treeNodeActions.RENAME]() } },
                    { separator: true },
                    { label: treeNodeActions.EXPAND, command: () => { menuItemsCommand[treeNodeActions.EXPAND]() } },
                    { label: treeNodeActions.COLLAPSE, command: () => { menuItemsCommand[treeNodeActions.COLLAPSE]() } },
                    ];
                    break;
                // degree 3
                case objectWorldNodeType.OVERLAY:
                    contextMenu = [...contextMenu,
                    { label: treeNodeActions.ADD_OBJECT, command: () => { menuItemsCommand[treeNodeActions.ADD_OBJECT]() } },
                    { label: treeNodeActions.SET_ACTIVE, command: () => { menuItemsCommand[treeNodeActions.SET_ACTIVE]() } },
                    { label: treeNodeActions.REMOVE_ALL_OBJECTS, command: () => { menuItemsCommand[treeNodeActions.REMOVE_ALL_OBJECTS]() } },
                    { label: treeNodeActions.REMOVE_OVERLAY, command: () => { menuItemsCommand[treeNodeActions.REMOVE_OVERLAY]() } },
                    // { label: treeNodeActions.RENAME, command: () => { menuItemsCommand[treeNodeActions.RENAME]() } },
                    { separator: true },
                    { label: treeNodeActions.EXPAND, command: () => { menuItemsCommand[treeNodeActions.EXPAND]() } },
                    { label: treeNodeActions.COLLAPSE, command: () => { menuItemsCommand[treeNodeActions.COLLAPSE]() } },
                    ];
                    break;
                case objectWorldNodeType.OBJECT_SCHEME:
                    contextMenu = [...contextMenu,
                    // { label: treeNodeActions.RENAME, command: () => { menuItemsCommand[treeNodeActions.RENAME]() } },
                    { label: treeNodeActions.EXPAND, command: () => { menuItemsCommand[treeNodeActions.EXPAND]() } },
                    { label: treeNodeActions.COLLAPSE, command: () => { menuItemsCommand[treeNodeActions.COLLAPSE]() } },
                    ];
                    break;
                // degree 4
                case objectWorldNodeType.OBJECT:
                    contextMenu = [...contextMenu,
                    { label: treeNodeActions.CLONE, command: () => { menuItemsCommand[treeNodeActions.CLONE]() } },
                    { label: treeNodeActions.REMOVE_OBJECT, command: () => { menuItemsCommand[treeNodeActions.REMOVE_OBJECT]() } },
                    { label: treeNodeActions.MOVE_TO_OTHER_OVERLAY, command: () => { menuItemsCommand[treeNodeActions.MOVE_TO_OTHER_OVERLAY]() } },
                    { label: treeNodeActions.REPLACE_SCHEME, command: () => { menuItemsCommand[treeNodeActions.REPLACE_SCHEME]() } },
                    { separator: true },
                    { label: treeNodeActions.MOVE_TO_LOCATION, command: () => { menuItemsCommand[treeNodeActions.MOVE_TO_LOCATION](0) }, items: !treeNode && getMoveToLocationSubItems() },
                    { label: treeNodeActions.JUMP_TO_SCHEME, command: () => { menuItemsCommand[treeNodeActions.JUMP_TO_SCHEME]() } },
                    { separator: true },
                    { label: treeNodeActions.SAVE, command: () => { menuItemsCommand[treeNodeActions.SAVE]() } },
                    { label: treeNodeActions.SAVE_AS, command: () => { menuItemsCommand[treeNodeActions.SAVE_AS]() } },
                    { label: treeNodeActions.STOP_SAVE, command: () => { menuItemsCommand[treeNodeActions.STOP_SAVE]() } },
                        // { label: treeNodeActions.RENAME, command: () => { menuItemsCommand[treeNodeActions.RENAME]() } }
                    ];
                    break;
                case objectWorldNodeType.OBJECT_LOCATION:
                    contextMenu = [...contextMenu,
                    { label: treeNodeActions.REMOVE_OBJECT_LOCATION, command: () => { menuItemsCommand[treeNodeActions.REMOVE_OBJECT_LOCATION]() } },
                    // { label: treeNodeActions.RENAME, command: () => { menuItemsCommand[treeNodeActions.RENAME]() } },
                    { label: treeNodeActions.EXPAND, command: () => { menuItemsCommand[treeNodeActions.EXPAND]() } },
                    { label: treeNodeActions.COLLAPSE, command: () => { menuItemsCommand[treeNodeActions.COLLAPSE]() } },
                    ];
                    break;
                default:
                    break;
            }
        }, 'objectWorldTreeDialog.getDynamicContextMenu')
        return contextMenu;
    }
    function updateTreeRedux() {
        runCodeSafely(() => {
            let buildedTree: TreeNodeModel = objectWorldTreeService.buildTree();
            dispatch(setObjectWorldTree(buildedTree));
        }, "objectWorldTreeDialog.updateTreeRedux");
    }
    function onContextMenuHandler(event: React.MouseEvent<HTMLDivElement>, selectedNode: TreeNodeModel): void {
        runCodeSafely(() => {
            // setSelectedNode(node); --connect to rename
            dispatch(setSelectedNodeInTree(selectedNode));
            let contextMenu = getDynamicContextMenu(selectedNode);
            cm.current.show(event)
            contextMenu.length == 0 && cm.current.hide(event)
            event.preventDefault();
        }, "objectWorldTreeDialog.onContextMenuHandler => onContextMenu")
    }
    function getMoveToLocationSubItems() {
        let mcCurrentObject = selectedNodeInTree.nodeMcContent as MapCore.IMcObject;
        let numLocation = mcCurrentObject.GetNumLocations();
        let items: { label: string, command: () => void }[] = [];
        for (let i = 0; i < numLocation; i++) {
            let item = { label: `${i}`, command: () => { menuItemsCommand[treeNodeActions.MOVE_TO_LOCATION](i) } };
            items = [...items, item];
        }
        return numLocation > 1 ? items : null;
    }
    function showNodePoints() {
        //remove last showNodePoints results
        unShowNodePoints();
        //find mcCurrentOverlayManager
        if ([objectWorldNodeType.OBJECT, objectWorldNodeType.OBJECT_SCHEME, objectWorldNodeType.OBJECT_LOCATION, objectWorldNodeType.ITEM].includes(selectedNodeInTree.nodeType)) {
            let overlayManagerKey = selectedNodeInTree.key.substring(0, 3);
            let mcCurrentOverlayManager = objectWorldTreeService.getNodeByKey(treeRedux, overlayManagerKey).nodeMcContent;

            //find the object to draw the points on it
            let schemeAndObjects = objectWorldTreeService.getSchemeAndObjectOfSchemeNodeTreeNode(selectedNodeInTree);
            if (schemeAndObjects.schemeObjects.length > 1) {
                dispatch(setTypeObjectWorldDialogSecond({
                    secondDialogHeader: 'Scheme Objects List',
                    secondDialogComponent: <SchemeObjectsList mcObjects={schemeAndObjects.schemeObjects} getSelectedObject={(selectedObject: MapCore.IMcObject) => {
                        let overlayAndObject = objectWorldTreeService.showNodePoints(selectedNodeInTree, showNodePointsType, activeViewport, mcCurrentOverlayManager, selectedObject)
                        setShowNodePointsOverlayAndObject(overlayAndObject);
                    }} />
                }))
            }
            else if (schemeAndObjects.schemeObjects.length == 1) {
                let overlayAndObject = objectWorldTreeService.showNodePoints(selectedNodeInTree, showNodePointsType, activeViewport, mcCurrentOverlayManager, schemeAndObjects.schemeObjects[0])
                setShowNodePointsOverlayAndObject(overlayAndObject);
            }
            // IMcObjectSchemeNode.ENodeKindFlags
        }
    }
    function unShowNodePoints() {
        if (showNodePointsOverlayAndObjectRef.current) {
            objectWorldTreeService.unShowNodePoints(showNodePointsOverlayAndObjectRef.current.overlay, showNodePointsOverlayAndObjectRef.current.object);
            setShowNodePointsOverlayAndObject(null);
        }
    }
    // function getRenameInput() { --connect to rename
    //     let top = `${selectedNodeBoundingBox.bottom + 4}px`;
    //     let left = `${selectedNodeBoundingBox.left}px`;
    //     let treeRight = selectedNodeRef.current?.getBoundingClientRect().right;
    //     let width = `${treeRight - selectedNodeBoundingBox.left}px`;

    //     return (
    //         <InputText
    //             value={renameValue}
    //             onChange={(e) => { setRenameValue(e.target.value); e.stopPropagation() }}
    //             onMouseDown={(e) => { setIsInputDrag(true); e.stopPropagation() }}
    //             style={{ position: 'fixed', padding: `${globalSizeFactor * 0.75}vh`, top: top, left: left, height: `${globalSizeFactor * 4.5}vh`, width: width }}
    //         />
    //     )
    // }

    return (
        <div>
            <Menubar style={{ zIndex: '4', padding: `${globalSizeFactor * 0.4}vh` }} model={menuBarItems} />
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
                            onContextMenuHandler(e.originalEvent, e.node);
                        }}
                        selectionMode="single"
                        selectionKeys={selectedNodeInTree?.key}
                        onSelectionChange={(e: any) => {
                            if (e.value !== selectedNodeInTree?.key) {
                                // dispatch(setIsFormOpen(true));
                                dispatch(setSelectedNodeInTree(objectWorldTreeService.getNodeByKey(treeRedux, e.value) as TreeNodeModel));
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

                {typeObjectWorldDialogSecond && <Dialog
                    className={showDialog.hideFormReason !== null ? `object-props__hidden-dialog ${objectWorldTreeService.dialogClassNames.get(typeObjectWorldDialogSecond.secondDialogHeader)}` : objectWorldTreeService.dialogClassNames.get(typeObjectWorldDialogSecond.secondDialogHeader)}
                    header={typeObjectWorldDialogSecond.secondDialogHeader}
                    onHide={() => dispatch(setTypeObjectWorldDialogSecond(undefined))}
                    modal={showDialog.hideFormReason !== null ? false : true}
                    visible>
                    {typeObjectWorldDialogSecond.secondDialogComponent}
                </Dialog>}
                <ConfirmDialog
                    contentClassName='form__confirm-dialog-content'
                    message={confirmDialogMessage}
                    header=''
                    footer={<div></div>}
                    visible={isConfirmDialog}
                    onHide={e => { setIsConfirmDialog(false) }}
                />
            </div>
        </div>
    );
}
