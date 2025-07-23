// External libraries
import { useState, useEffect, ReactElement, useMemo } from 'react';
import { useDispatch, useSelector } from 'react-redux';

// UI/component libraries
import { Button } from "primereact/button"
import { ConfirmDialog } from "primereact/confirmdialog"
import { Dropdown } from "primereact/dropdown"
import { Fieldset } from "primereact/fieldset"
import { InputText } from "primereact/inputtext"
import { ListBox, ListBoxChangeEvent } from "primereact/listbox"
import { Checkbox } from "primereact/checkbox"
import { InputNumber } from "primereact/inputnumber"

// Project-specific imports
import { MapCoreData, ViewportData, getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import SaveToFile from './saveToFile';
import LoadObjsFromRawVectorData from "./LoadObjsFromRawVectorData";
import ViewportsList from "./viewportsList";
import { OverlayFormTabInfo } from "./overlayForm";
import { Properties } from "../../../dialog";
import UploadFilesCtrl, { UploadTypeEnum } from "../../../shared/uploadFilesCtrl";
import TreeNodeModel, { objectWorldNodeType } from '../../../../shared/models/tree-node.model';
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import objectWorldTreeService, { fileExplorerSelectionInListBoxService } from "../../../../../services/objectWorldTree.service";
import { AppState } from "../../../../../redux/combineReducer";
import { setObjectWorldTree, setObjectsMemoryBuffer, addSavedObjectDataToMap, setTypeObjectWorldDialogSecond } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import generalService from "../../../../../services/general.service";
import dialogStateService from "../../../../../services/dialogStateService";
import savedFilesService, { SavedFileData } from "../../../../../services/savedFiles.service"

export class ObjectsPropertiesState implements Properties {
    minSizeFactor: number;
    maxSizeFactor: number;
    numTilesInFileEdge: number;

    //Raw Vector Map Layer Properties
    cameraYawAngle: number;
    cameraScale: number;
    layerName: string;
    geometryFilter: any;
    //Sub Items Ids
    subItemsIds: string;
    subItemsIdsVisible: boolean;

    static getDefault(props: any): ObjectsPropertiesState {
        let EGeometryFilter = getEnumDetailsList(MapCore.EGeometry);
        let bVisibility: any = {};
        props.selectedNodeInTree.nodeMcContent.GetSubItemsVisibility(bVisibility)
        return {
            minSizeFactor: generalService.ObjectWorldTreeProperties.minSizeFactor,
            maxSizeFactor: generalService.ObjectWorldTreeProperties.maxSizeFactor,
            numTilesInFileEdge: generalService.ObjectWorldTreeProperties.numTilesInFileEdge,
            cameraYawAngle: generalService.ObjectWorldTreeProperties.cameraYawAngle,
            cameraScale: generalService.ObjectWorldTreeProperties.cameraScale,
            layerName: generalService.ObjectWorldTreeProperties.layerName,
            geometryFilter: getEnumValueDetails(generalService.ObjectWorldTreeProperties.geometryFilter, EGeometryFilter),
            subItemsIds: '',
            subItemsIdsVisible: bVisibility.Value,
        }
    }
}
export class ObjectsProperties extends ObjectsPropertiesState {
    viewportsOfOMArr: any[];
    viewportToSave: any;
    allObjectsOfOverlay: TreeNodeModel[];

    objectsToSave: any[];
    isSecondDialogVisible: boolean;
    isSaveAllFile: boolean;
    rawVectorBuffer: { Value: any };
    objectsListSelectionService: fileExplorerSelectionInListBoxService;
    asyncCBSaveAsRawVector: MapCore.IMcOverlayManager.IAsyncOperationCallback;
    //File and Memory Buffer Properties
    isVersionChecked: boolean;
    isStorageFormat: boolean;
    isContinueSaveChecked: boolean;
    formatDropDownValue: any;

    isAsMemoryBufferChecked: boolean;
    isAsyncChecked: boolean;
    additionalFiles: string[] | MapCore.SMcFileInMemory[];

    static getDefault(props: any): ObjectsProperties {
        let EStorageFormat = getEnumDetailsList(MapCore.IMcOverlayManager.EStorageFormat);
        let currentUpdatedOverLay = objectWorldTreeService.getNodeByKey(props.treeRedux, props.props.tabInfo.currentOverlay.key) as TreeNodeModel;
        const objectsNodes = currentUpdatedOverLay.children.filter(c => c.nodeType == objectWorldNodeType.OBJECT).map(node => ({
            ...node, label: savedFilesService.getSchemeOrObjectLabel(node)
        }));

        let stateDefaults = super.getDefault(props);
        let defaults: ObjectsProperties = {
            ...stateDefaults,
            viewportsOfOMArr: Array.from(objectWorldTreeService.getOMMCViewportsByOverlay(props.treeRedux, props.props.tabInfo.currentOverlay)).map(vp => { return { viewport: vp.viewport, label: generalService.getObjectName(vp, "Viewport") } }),
            viewportToSave: null,
            allObjectsOfOverlay: objectsNodes,
            objectsToSave: [],
            isSecondDialogVisible: false,
            isSaveAllFile: false,
            rawVectorBuffer: { Value: null },
            objectsListSelectionService: new fileExplorerSelectionInListBoxService(),
            asyncCBSaveAsRawVector: null,

            isVersionChecked: generalService.ObjectWorldTreeProperties.isVersionChecked,
            isStorageFormat: generalService.ObjectWorldTreeProperties.isStorageFormat,
            isContinueSaveChecked: generalService.ObjectWorldTreeProperties.isContinueSaveChecked,
            formatDropDownValue: getEnumValueDetails(generalService.ObjectWorldTreeProperties.formatDropDownValue, EStorageFormat),

            isAsMemoryBufferChecked: generalService.ObjectWorldTreeProperties.isAsMemoryBufferChecked,
            isAsyncChecked: generalService.ObjectWorldTreeProperties.isAsyncChecked,
            additionalFiles: generalService.ObjectWorldTreeProperties.additionalFiles,
        }

        return defaults;
    }
};

export default function Objects(props: { tabInfo: OverlayFormTabInfo }) {
    let { objectsProperties } = props.tabInfo.properties;

    const dispatch = useDispatch();

    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    let selectedNodeInTree = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    let objectsMemoryBuffer = useSelector((state: AppState) => state.objectWorldTreeReducer.objectsMemoryBuffer);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [confirmDialogVisible, setConfirmDialogVisible] = useState(false);
    const [confirmDialogMessage, setConfirmDialogMessage] = useState('');
    const [confirmDialogHeader, setConfirmDialogHeader] = useState('');
    const [isConfirmDialogFooter, setIsConfirmDialogFooter] = useState(false);
    const [continueSaveFunction, setContinueSaveFunction] = useState<() => void>(() => { });
    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        objectsProperties: false,
        currentOverlay: false,
        treeRedux: false,
        reloadParams: false,
    })

    const enumDetails = useMemo(() => ({
        EStorageFormat: getEnumDetailsList(MapCore.IMcOverlayManager.EStorageFormat),
        EVersion: getEnumDetailsList(MapCore.IMcOverlayManager.ESavingVersionCompatibility),
        EGeometryFilter: getEnumDetailsList(MapCore.EGeometry),
    }), []);
    const rawVectorTypeOptions = useMemo(() => [
        { name: 'Raw Vector Formats With Styling Support (*.kmz, *.kml, *.dxf)', extension: '.kmz' },
        { name: 'All Files', extension: '' },
    ], []);
    const savedFileTypeOptions = useMemo(() => [
        { name: 'MapCore Object Files (*.mcobj, *.mcobj.json , *.m, *.json)', extension: '.mcobj' },
        { name: 'MapCore Object Binary Files (*.mcobj,*.m)', extension: '.mcobj' },
        { name: 'MapCore Object Json Files (*.mcobj.json, *.json)', extension: '.mcobj.json' },
        { name: 'All Files', extension: '' },
    ], []);

    //#region useEffects
    useEffect(() => {
        runCodeSafely(() => {
            // initialize first time the tab is loaded only
            if (!objectsProperties) {
                props.tabInfo.setInitialStatePropertiesCallback("objectsProperties", null, ObjectsPropertiesState.getDefault({ props, treeRedux, selectedNodeInTree }));
                props.tabInfo.setPropertiesCallback("objectsProperties", null, ObjectsProperties.getDefault({ props, treeRedux, selectedNodeInTree }));
            }
        }, 'OverlayForm/Objects.useEffect');
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.objectsProperties) {
                props.tabInfo.setApplyCallBack("Objects", saveObjectsProperties);
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, objectsProperties: true })
            }
        }, 'OverlayForm/Objects.useEffect => objectsProperties');
    }, [objectsProperties])
    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.currentOverlay) {
                props.tabInfo.setInitialStatePropertiesCallback("objectsProperties", null, ObjectsPropertiesState.getDefault({ props, treeRedux, selectedNodeInTree }));
                props.tabInfo.setPropertiesCallback("objectsProperties", null, ObjectsProperties.getDefault({ props, treeRedux, selectedNodeInTree }));
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, currentOverlay: true })
            }
        }, "OverlayForms/objects.useEffect => props.tabInfo.currentOverlay")
    }, [props.tabInfo.currentOverlay])
    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.treeRedux) {
                setListBoxArraysValues();
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, treeRedux: true })
            }
        }, "OverlayForms/objects.useEffect => treeRedux")
    }, [treeRedux])
    //#endregion
    //#region Save Functions
    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("objectsProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in ObjectsPropertiesState.getDefault({ props, treeRedux, selectedNodeInTree })) {
                props.tabInfo.setCurrStatePropertiesCallback("objectsProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value)
            }
        }, "OverlayForms/objects.saveData => onChange")
    }
    const saveObjectsProperties = () => {
        runCodeSafely(() => {
            //File and Memory Buffer
            generalService.ObjectWorldTreeProperties.isVersionChecked = objectsProperties?.isVersionChecked;
            generalService.ObjectWorldTreeProperties.isStorageFormat = objectsProperties?.isStorageFormat;
            generalService.ObjectWorldTreeProperties.isContinueSaveChecked = objectsProperties?.isContinueSaveChecked;
            generalService.ObjectWorldTreeProperties.formatDropDownValue = objectsProperties?.formatDropDownValue?.theEnum;
            generalService.ObjectWorldTreeProperties.minSizeFactor = objectsProperties?.minSizeFactor;
            generalService.ObjectWorldTreeProperties.maxSizeFactor = objectsProperties?.maxSizeFactor;
            generalService.ObjectWorldTreeProperties.numTilesInFileEdge = objectsProperties?.numTilesInFileEdge;
            //Raw Vector Map Layer
            generalService.ObjectWorldTreeProperties.cameraYawAngle = objectsProperties?.cameraYawAngle;
            generalService.ObjectWorldTreeProperties.cameraScale = objectsProperties?.cameraScale;
            generalService.ObjectWorldTreeProperties.layerName = objectsProperties?.layerName;
            generalService.ObjectWorldTreeProperties.geometryFilter = objectsProperties?.geometryFilter?.theEnum
            generalService.ObjectWorldTreeProperties.isAsMemoryBufferChecked = objectsProperties?.isAsMemoryBufferChecked;
            generalService.ObjectWorldTreeProperties.isAsyncChecked = objectsProperties?.isAsyncChecked;

            dialogStateService.applyDialogState(["objectsProperties.minSizeFactor",
                "objectsProperties.maxSizeFactor",
                "objectsProperties.numTilesInFileEdge",
                "objectsProperties.cameraYawAngle",
                "objectsProperties.cameraScale",
                "objectsProperties.layerName",
                "objectsProperties.geometryFilter",
            ]);

        }, "Objects.saveObjectsProperties => unMountUseEffect");
    }
    //#endregion
    const setListBoxArraysValues = () => {
        runCodeSafely(() => {
            let currentUpdatedOverLay = objectWorldTreeService.getNodeByKey(treeRedux, props.tabInfo.currentOverlay.key) as TreeNodeModel;
            let updatedObjectsProperties = { ...objectsProperties }
            const objectsNodes = currentUpdatedOverLay.children.filter(c => c.nodeType == objectWorldNodeType.OBJECT).map(node => ({
                ...node, label: savedFilesService.getSchemeOrObjectLabel(node)
            }));
            updatedObjectsProperties.allObjectsOfOverlay = objectsNodes;
            let selectedObjects = [];
            objectsProperties?.objectsToSave.forEach((selectedObjectNode: TreeNodeModel) => {
                const foundedNode = objectsNodes.find(node => node.nodeMcContent == selectedObjectNode.nodeMcContent);
                selectedObjects = [...selectedObjects, foundedNode];
            });
            updatedObjectsProperties.objectsToSave = selectedObjects;
            updatedObjectsProperties.viewportsOfOMArr = Array.from(objectWorldTreeService.getOMMCViewportsByOverlay(treeRedux, props.tabInfo.currentOverlay)).map(vp => { return { viewport: vp.viewport, label: generalService.getObjectName(vp, "Viewport") } });
            props.tabInfo.setPropertiesCallback("objectsProperties", null, updatedObjectsProperties);
        }, "Objects.setListBoxArraysValues");
    }
    const refreshTree = () => {
        let tree: TreeNodeModel = objectWorldTreeService.buildTree()
        dispatch(setObjectWorldTree(tree))
    }
    //#region Handle Functions
    //#region List Handles
    const handleObjectsListChange = (e: ListBoxChangeEvent) => {
        runCodeSafely(() => {
            let selectedObjects = objectsProperties?.objectsListSelectionService.handleClick(objectsProperties?.allObjectsOfOverlay, e);
            props.tabInfo.setPropertiesCallback("objectsProperties", "objectsToSave", selectedObjects);
        }, "OverlayForms/objects.handleObjectsListChange => onChange")
    }
    //#endregion
    //#region FileFieldSet Handles
    const handleFileUpload = (virtualFSPath: string, selectedOption: UploadTypeEnum) => {
        runCodeSafely(() => {
            let overlay: MapCore.IMcOverlay = props.tabInfo.currentOverlay.nodeMcContent;
            let storageFormat: { Value?: MapCore.IMcOverlayManager.EStorageFormat } = {};
            let version: { Value?: number } = {};
            let loadedObjects: MapCore.IMcObject[] = [];
            runMapCoreSafely(() => {
                loadedObjects = overlay.LoadObjectsFromFile(virtualFSPath, null, storageFormat, version)
            }, 'objects.handleFileUpload => IMcOverlay.LoadObjectsFromFile', true)
            objectWorldTreeService.deleteSystemDirectories([virtualFSPath.split('/')[0]])

            const versionEnum = enumDetails.EVersion.find(versionEnum => versionEnum.code == version.Value);
            const finalVersionEnum = versionEnum ? versionEnum.theEnum : getEnumValueDetails(MapCore.IMcOverlayManager.ESavingVersionCompatibility.ESVC_LATEST, enumDetails.EVersion).theEnum;
            const objectSaveData = new SavedFileData(finalVersionEnum, storageFormat.Value, null)
            loadedObjects.forEach(object => {
                dispatch(addSavedObjectDataToMap(object, objectSaveData));
            })
            refreshTree();
        }, "objects.handleFileUpload => onChange")
    }
    const handleSaveObjectsToFileOK = (fileName: string, fileType: string) => {
        runCodeSafely(() => {
            const overlay: MapCore.IMcOverlay = props.tabInfo.currentOverlay.nodeMcContent;
            const mcObjsToSave: MapCore.IMcObject[] = objectsProperties?.objectsToSave.map(obj => obj.nodeMcContent);
            const format = fileType.endsWith('.json') ?
                MapCore.IMcOverlayManager.EStorageFormat.ESF_JSON :
                MapCore.IMcOverlayManager.EStorageFormat.ESF_MAPCORE_BINARY;
            const { version, isAllSameVersion } = savedFilesService.getObjectsOrSchemesVersion(mcObjsToSave, true);

            const continueSaveFunc = () => {
                let uint8ArrayData: Uint8Array = null;
                runMapCoreSafely(() => {
                    uint8ArrayData = objectsProperties?.isSaveAllFile ?
                        overlay.SaveAllObjects(format, version.theEnum) :
                        overlay.SaveObjects(mcObjsToSave, format, version.theEnum);
                }, 'objects.handleSaveObjectsToFileOK => IMcOverlay.SaveObjects', true)
                const objToSaveFileName = objectsProperties?.isSaveAllFile ? objectsProperties?.allObjectsOfOverlay : objectsProperties?.objectsToSave;
                let objectSaveData = new SavedFileData(version.theEnum, format, null);
                const finalFileName = `${fileName}${fileType}`;
                if (objectsProperties?.isContinueSaveChecked) {
                    objectSaveData = new SavedFileData(version.theEnum, format, finalFileName)
                }
                MapCore.IMcMapDevice.DownloadBufferAsFile(uint8ArrayData, finalFileName);
                objToSaveFileName.forEach(obj => {
                    dispatch(addSavedObjectDataToMap(obj.nodeMcContent, objectSaveData));
                });
                refreshTree();
            }

            if (isAllSameVersion) {
                continueSaveFunc();
            }
            else {
                setConfirmDialogVisible(true);
                setConfirmDialogHeader('Conflict Versions')
                setConfirmDialogMessage(`The objects are of different versions. Save using the most recent detected version (${version.name}), or cancel?`)
                setIsConfirmDialogFooter(true);
                setContinueSaveFunction(() => continueSaveFunc);
            }
            dispatch(setTypeObjectWorldDialogSecond(undefined))
        }, 'SaveObjectsToFile.handleSaveToFileOk => onClick')
    }
    const handleSaveFileClick = (isSaveAll: boolean) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("objectsProperties", 'isSaveAllFile', isSaveAll);

            dispatch(setTypeObjectWorldDialogSecond({
                secondDialogHeader: 'Save to File',
                secondDialogComponent: <SaveToFile
                    savedFileTypeOptions={savedFileTypeOptions}
                    handleSaveToFileOk={handleSaveObjectsToFileOK} />
            }))
        }, "objects.handleSaveFileClick => onClick")
    }
    //#endregion
    //#region MemoryBufferFieldSet Handles
    const handleMemoryBufferLoad = () => {
        runCodeSafely(() => {
            if (!objectsMemoryBuffer) {
                setConfirmDialogVisible(true);
                setConfirmDialogMessage('Objects were not saved to memory buffer.');
                setConfirmDialogHeader('Empty buffer');
                setIsConfirmDialogFooter(false);
            }
            else {
                let overlay: MapCore.IMcOverlay = props.tabInfo.currentOverlay.nodeMcContent;
                let storageFormat: { Value?: MapCore.IMcOverlayManager.EStorageFormat } = {}
                let version: { Value?: number } = {}
                let loadedObjects: MapCore.IMcObject[] = [];
                runMapCoreSafely(() => {
                    loadedObjects = overlay.LoadObjects(objectsMemoryBuffer, null, storageFormat, version)
                }, 'objects.handleMemoryBufferLoad => IMcOverlay.LoadObjects', true)
                const versionEnum = enumDetails.EVersion.filter(v => v.code === version.Value)[0].theEnum;
                const objectSaveData = new SavedFileData(versionEnum, storageFormat.Value, null)
                loadedObjects.forEach(mcObj => {
                    dispatch(addSavedObjectDataToMap(mcObj, objectSaveData));
                })
                refreshTree()
            }
        }, "objects.handleMemoryBufferLoad => onClick")
    }
    const handleSaveMemoryBufferClick = (isSaveAll: boolean) => {
        runCodeSafely(() => {
            let overlay: MapCore.IMcOverlay = props.tabInfo.currentOverlay.nodeMcContent;
            let mcObjsToSave: MapCore.IMcObject[] = isSaveAll ? objectsProperties?.allObjectsOfOverlay.map(objectNode => objectNode.nodeMcContent) :
                objectsProperties?.objectsToSave.map(obj => obj.nodeMcContent);
            let format: MapCore.IMcOverlayManager.EStorageFormat = objectsProperties?.formatDropDownValue.theEnum;
            const { version, isAllSameVersion } = savedFilesService.getObjectsOrSchemesVersion(mcObjsToSave, true);

            const continueSaveFunc = () => {
                let uint8ArrayData: Uint8Array = null;
                runMapCoreSafely(() => {
                    uint8ArrayData = isSaveAll ? overlay.SaveAllObjects(format, version.theEnum) :
                        overlay.SaveObjects(mcObjsToSave, format, version.theEnum);
                }, 'objects.handleSaveMemoryBufferClick => IMcOverlay.SaveObjects', true)
                dispatch(setObjectsMemoryBuffer(uint8ArrayData));
                const objectSaveData = new SavedFileData(version.theEnum, format, null)
                mcObjsToSave.forEach(object => {
                    dispatch(addSavedObjectDataToMap(object, objectSaveData));
                })
                refreshTree();
            }

            if (isAllSameVersion) {
                continueSaveFunc();
            }
            else {
                setConfirmDialogVisible(true);
                setConfirmDialogHeader('Conflict Versions')
                setConfirmDialogMessage(`The objects are of different versions. Save using the most recent detected version (${version.name}), or cancel?`)
                setIsConfirmDialogFooter(true);
                setContinueSaveFunction(() => continueSaveFunc);
            }
        }, "objects.handleSaveMemoryBufferClick => onClick")
    }
    //#endregion
    //#region Raw Vector MapLayer Fieldset Handles
    const handleVpToSaveChange = (e: ListBoxChangeEvent) => {
        runCodeSafely(() => {
            console.log(e);
            if (e.value?.viewport) {
                let vpScale = e.value?.viewport.GetCameraScale();
                let yaw: { Value?: number } = {};
                e.value?.viewport.GetCameraOrientation(yaw);
                props.tabInfo.setPropertiesCallback("objectsProperties", "cameraScale", vpScale);
                props.tabInfo.setPropertiesCallback("objectsProperties", "cameraYawAngle", yaw.Value);
                props.tabInfo.setPropertiesCallback("objectsProperties", "viewportToSave", e.value);
                props.tabInfo.setCurrStatePropertiesCallback("objectsProperties", "cameraScale", vpScale);
                props.tabInfo.setCurrStatePropertiesCallback("objectsProperties", "cameraYawAngle", yaw.Value);
                props.tabInfo.setCurrStatePropertiesCallback("objectsProperties", "viewportToSave", e.value);
            }
        }, "objects.handleVpToSaveChange");
    }
    const handleSaveAsRawVectorMapLayer = (isSaveAll: boolean) => {
        runCodeSafely(() => {
            dispatch(setTypeObjectWorldDialogSecond({
                secondDialogHeader: 'Save to File',
                secondDialogComponent: <SaveToFile
                    savedFileTypeOptions={rawVectorTypeOptions}
                    handleSaveToFileOk={(fileName: string, fileType: string) => {
                        dispatch(setTypeObjectWorldDialogSecond(undefined));
                        objectsProperties?.isAsMemoryBufferChecked ?
                            handleSaveRawVectorToBufferOK(fileName, fileType, isSaveAll) : handleSaveRawVectorToFileOK(fileName, fileType, isSaveAll)
                    }} />
            }));
        }, "objects.handleSaveAsRawVectorMapLayer => onClick");
    }
    const handleSaveRawVectorToBufferOK = (fileName: string, fileType: string, isSaveAll: boolean) => {
        let overlay = props.tabInfo.currentOverlay.nodeMcContent as MapCore.IMcOverlay;
        let additionalFiles: MapCore.SMcFileInMemory[] = null;
        let asyncOperationCallBack: MapCore.IMcOverlayManager.IAsyncOperationCallback = null;
        if (objectsProperties?.isAsyncChecked) {
            asyncOperationCallBack = new MapCoreData.iMcOverlayManagerAsyncOperationCallbackClass((strFileName: string, eStatus: MapCore.IMcErrors.ECode, auFileMemoryBuffer: Uint8Array, aAdditionalFiles: MapCore.SMcFileInMemory[]) => {
                props.tabInfo.setPropertiesCallback("objectsProperties", 'additionalFiles', aAdditionalFiles);
                props.tabInfo.setPropertiesCallback("objectsProperties", 'asyncCBSaveAsRawVector', null);
                console.log('came to cb and set asyncCBSaveAsRawVector to null and set aAdditionalFiles to buffer')
                downloadFileByMCDownload(fileName, fileType, true);
            });
            props.tabInfo.setPropertiesCallback("objectsProperties", "asyncCBSaveAsRawVector", asyncOperationCallBack);
            console.log('set asyncCBSaveAsRawVector in save to buffer')
        }

        if (isSaveAll) {
            runMapCoreSafely(() => {
                overlay.SaveAllObjectsAsRawVectorData(objectsProperties?.viewportToSave.viewport, objectsProperties?.cameraYawAngle, objectsProperties?.cameraScale,
                    objectsProperties?.layerName, `rawVectorMapLayerSB${fileType}`, objectsProperties?.rawVectorBuffer, additionalFiles, asyncOperationCallBack, objectsProperties?.geometryFilter?.theEnum);
                console.log(additionalFiles)
            }, 'Objects.saveAsRawVectorMapLayerToBuffer => IMcOverlay.SaveAllObjectsAsRawVectorData', true);
        }
        else {
            let mcObjsToSave: MapCore.IMcObject[] = objectsProperties?.objectsToSave.map(obj => obj.nodeMcContent);
            runMapCoreSafely(() => {
                overlay.SaveObjectsAsRawVectorData(mcObjsToSave, objectsProperties?.viewportToSave.viewport, objectsProperties?.cameraYawAngle, objectsProperties?.cameraScale, objectsProperties?.layerName,
                    `rawVectorMapLayerSB${fileType}`, objectsProperties?.rawVectorBuffer, additionalFiles, asyncOperationCallBack, objectsProperties?.geometryFilter?.theEnum);
                console.log(additionalFiles)
            }, 'Objects.saveAsRawVectorMapLayerToBuffer => IMcOverlay.SaveAllObjectsAsRawVectorData', true);
        }

        if (!objectsProperties?.isAsyncChecked) {
            downloadFileByMCDownload(fileName, fileType, true);
            props.tabInfo.setPropertiesCallback("objectsProperties", 'additionalFiles', additionalFiles);
            console.log('download and set additional in not async')
        }

        dialogStateService.applyDialogState([
            "objectsProperties.cameraYawAngle",
            "objectsProperties.cameraScale",
            "objectsProperties.layerName",
            "objectsProperties.geometryFilter",
        ]);
    }
    const handleSaveRawVectorToFileOK = (fileName: string, fileType: string, isSaveAll: boolean) => {
        let overlay = props.tabInfo.currentOverlay.nodeMcContent as MapCore.IMcOverlay;
        let additionalFiles: string[] = [];
        let asyncOperationCallBack: MapCore.IMcOverlayManager.IAsyncOperationCallback = null;
        if (objectsProperties?.isAsyncChecked) {
            asyncOperationCallBack = new MapCoreData.iMcOverlayManagerAsyncOperationCallbackClass((strFileName: string, eStatus: MapCore.IMcErrors.ECode, aAdditionalFiles: string[]) => {
                props.tabInfo.setPropertiesCallback("objectsProperties", 'additionalFiles', aAdditionalFiles);
                props.tabInfo.setPropertiesCallback("objectsProperties", 'asyncCBSaveAsRawVector', null);
                console.log('came to cb and set asyncCBSaveAsRawVector to null and set aAdditionalFiles to file')
                downloadFileByMCDownload(fileName, fileType, false);
            });
            props.tabInfo.setPropertiesCallback("objectsProperties", 'asyncCBSaveAsRawVector', asyncOperationCallBack);
            console.log('set asyncCBSaveAsRawVector in save to file')
        }

        if (isSaveAll) {
            runMapCoreSafely(() => {
                overlay.SaveAllObjectsAsRawVectorDataToFile(objectsProperties?.viewportToSave.viewport, objectsProperties?.cameraYawAngle, objectsProperties?.cameraScale,
                    objectsProperties?.layerName, `rawVectorMapLayerSF${fileType}`, additionalFiles, asyncOperationCallBack, objectsProperties?.geometryFilter?.theEnum
                );
            }, 'objects.saveAsRawVectorMapLayerToFile => IMcOverlay.SaveAllObjectsAsRawVectorDataToFile', true);
        }
        else {
            let mcObjsToSave: MapCore.IMcObject[] = objectsProperties?.objectsToSave.map(obj => obj.nodeMcContent);
            runMapCoreSafely(() => {
                overlay.SaveObjectsAsRawVectorDataToFile(mcObjsToSave, objectsProperties?.viewportToSave.viewport, objectsProperties?.cameraYawAngle, objectsProperties?.cameraScale,
                    objectsProperties?.layerName, `rawVectorMapLayerSF${fileType}`, additionalFiles, asyncOperationCallBack, objectsProperties?.geometryFilter?.theEnum
                );
            }, 'objects.saveAsRawVectorMapLayerToFile => IMcOverlay.SaveObjectsAsRawVectorDataToFile', true);
        }

        if (!objectsProperties?.isAsyncChecked) {
            downloadFileByMCDownload(fileName, fileType, false);
            props.tabInfo.setPropertiesCallback("objectsProperties", 'additionalFiles', additionalFiles);
            console.log('download and set additional in not async')
        }

        dialogStateService.applyDialogState([
            "objectsProperties.cameraYawAngle",
            "objectsProperties.cameraScale",
            "objectsProperties.layerName",
            "objectsProperties.geometryFilter",
        ]);
    }
    const downloadFileByMCDownload = (fileName: string, fileType: string, isFromBuffer: boolean) => {
        let finalName = `${fileName}${fileType}`;
        if (isFromBuffer) {
            runMapCoreSafely(() => {
                MapCore.IMcMapDevice.DownloadBufferAsFile(objectsProperties?.rawVectorBuffer.Value, finalName)
            }, 'objects.downloadFileByMCDownload => IMcMapDevice.DownloadBufferAsFile', true);
        }
        else {
            runMapCoreSafely(() => {
                MapCore.IMcMapDevice.DownloadFileSystemFile(`rawVectorMapLayerSF${fileType}`, finalName);
                MapCore.IMcMapDevice.DeleteFileSystemFile(`rawVectorMapLayerSF${fileType}`);
            }, 'objects.downloadFileByMCDownload => IMcMapDevice.DownloadFileSystemFile && IMcMapDevice.DeleteFileSystemFile', true)
        }
    }
    const handleCancelAsyncSavingClick = () => {
        runCodeSafely(() => {
            let mcOverlay = props.tabInfo.currentOverlay.nodeMcContent as MapCore.IMcOverlay;
            runMapCoreSafely(() => {
                mcOverlay.CancelAsyncSavingObjects(objectsProperties?.asyncCBSaveAsRawVector);
            }, 'objects.handleCancelAsyncSavingClick => IMcOverlay.CancelAsyncSavingObjects', true)
        }, 'objects.handleCancelAsyncSavingClick => onClick')
    }
    //#endregion
    //#region Items Ids Handles
    const getSelectedViewports = (selectedViewport: ViewportData, isSetAction: boolean) => {
        let mcOverlay = props.tabInfo.currentOverlay.nodeMcContent as MapCore.IMcOverlay;
        if (isSetAction) {
            let itemIds32Buffer = null;
            if (objectsProperties?.subItemsIds) {
                let numericArrIds = objectsProperties?.subItemsIds.split(' ').map(Number);
                itemIds32Buffer = Uint32Array.from(numericArrIds);
            }
            mcOverlay.SetSubItemsVisibility(itemIds32Buffer, objectsProperties?.subItemsIdsVisible, selectedViewport.viewport);
            dialogStateService.applyDialogState(["objectsProperties.subItemsIdsVisible",
                "objectsProperties.subItemsIds"]);
        }
        else { //Get action
            let isVisible: { Value?: boolean } = {};
            let idsBuffer: Uint32Array = mcOverlay.GetSubItemsVisibility(isVisible, selectedViewport.viewport);
            let idsString = Array.from(idsBuffer).join(' ');
            props.tabInfo.setPropertiesCallback("objectsProperties", "subItemsIds", idsString);
            props.tabInfo.setPropertiesCallback("objectsProperties", "subItemsIdsVisible", isVisible.Value);
            props.tabInfo.setCurrStatePropertiesCallback("objectsProperties", "subItemsIds", idsString);
            props.tabInfo.setCurrStatePropertiesCallback("objectsProperties", "subItemsIdsVisible", isVisible.Value);
        }
    }
    const handleSetSubItemsIdsClick = (isSetAction: boolean) => {
        dispatch(setTypeObjectWorldDialogSecond({
            secondDialogHeader: 'Viewports List',
            secondDialogComponent: <ViewportsList getSelectedViewports={(selectedViewport: ViewportData) => { getSelectedViewports(selectedViewport, isSetAction) }} />
        }))
    }
    //#endregion
    //#endregion

    //#region DOM functions
    const getFileFieldSet = () => {
        return <Fieldset className="form__column-fieldset" legend="File" >
            <div style={{ gap: `${globalSizeFactor * 0.75}vh` }} className="form__flex-and-row">
                <UploadFilesCtrl isDirectoryUpload={false} uploadOptions={[UploadTypeEnum.upload]} getVirtualFSPath={handleFileUpload} buttonOnly={true} />
                <Button disabled={objectsProperties?.objectsToSave?.length == 0} label="Save Selected" onClick={e => handleSaveFileClick(false)} />
                <Button label="Save All" onClick={e => handleSaveFileClick(true)}></Button>
            </div>
            <div className="form__flex-and-row">
                <Checkbox name='isContinueSaveChecked' inputId="continueSaveChecked" onChange={saveData} checked={objectsProperties?.isContinueSaveChecked} />
                <label htmlFor="continueSaveChecked" className="ml-2">Continue saving to this file</label>
            </div>
        </Fieldset>
    }
    const getMemoryBufferFieldSet = () => {
        return <Fieldset className="form__column-fieldset" legend={<legend>Memory Buffer</legend>}>
            <div style={{ gap: `${globalSizeFactor * 0.75}vh` }} className="form__flex-and-row">
                <Button style={{ width: `${globalSizeFactor * 8.5}vh` }} label="Load" icon="pi pi-upload" onClick={handleMemoryBufferLoad} />
                <Button disabled={objectsProperties?.objectsToSave?.length == 0} label="Save Selected" onClick={e => handleSaveMemoryBufferClick(false)} />
                <Button label="Save All" onClick={e => handleSaveMemoryBufferClick(true)}></Button>
            </div>
            <div style={{ gap: `${globalSizeFactor * 0.75}vh` }} className="form__flex-and-row">
                <label>Saving Format</label>
                <Dropdown id="formatDropDown" name='formatDropDownValue' value={objectsProperties?.formatDropDownValue ?? ""} onChange={saveData} options={enumDetails.EStorageFormat} optionLabel="name" />
            </div>
        </Fieldset>
    }
    const getRawVectorMapLayerFieldset = () => {
        return (
            <Fieldset className="form__row-fieldset" legend={<legend>Save Raw Vector Map Layer</legend>}>
                <div style={{ width: '60%' }}>
                    <div style={{ height: `${globalSizeFactor * 7.5}vh`, overflowY: 'auto', width: '100%' }}>
                        <ListBox listStyle={{ minHeight: `${globalSizeFactor * 7}vh`, maxHeight: `${globalSizeFactor * 7}vh` }} name='viewportToSave' value={objectsProperties?.viewportToSave ?? ""} onChange={handleVpToSaveChange} optionLabel='label' options={objectsProperties?.viewportsOfOMArr} />
                    </div>
                    <div style={{ padding: `${globalSizeFactor * 0.15}vh`, display: 'flex', justifyContent: 'space-between', width: '100%' }}>
                        <label htmlFor='YawAngle'>Camera Yaw Angle: </label>
                        <InputNumber className="form__narrow-input" id='YawAngle' value={objectsProperties?.cameraYawAngle ?? 0} name='cameraYawAngle' onValueChange={saveData} />
                    </div>
                    <div style={{ padding: `${globalSizeFactor * 0.15}vh`, display: 'flex', justifyContent: 'space-between', width: '100%' }}>
                        <label htmlFor='cameraScale'>Camera Scale: </label>
                        <InputNumber className="form__narrow-input" id='cameraScale' value={objectsProperties?.cameraScale ?? 0} name='cameraScale' onValueChange={saveData} />
                    </div>
                    <div style={{ padding: `${globalSizeFactor * 0.15}vh`, display: 'flex', justifyContent: 'space-between', width: '100%' }}>
                        <label htmlFor='layerName'>Layer Name </label>
                        <InputText className="form__narrow-input" id='layerName' value={objectsProperties?.layerName ?? ""} name='layerName' onChange={saveData} />
                    </div>
                    <div style={{ padding: `${globalSizeFactor * 0.15}vh`, display: 'flex', flexDirection: 'column', width: '100%' }} >
                        <label htmlFor='geometryFilter'>Geometry Filter </label>
                        <Dropdown id='geometryFilter' name='geometryFilter' value={objectsProperties?.geometryFilter ?? ""} onChange={saveData} options={enumDetails.EGeometryFilter} optionLabel="name" />
                    </div>
                    <span style={{ display: 'flex', flexDirection: 'row' }}>
                        <div style={{ padding: `${globalSizeFactor * 0.4}vh` }}>
                            <Checkbox name='isAsMemoryBufferChecked' inputId="asMemoryBufferChecked" onChange={saveData} checked={objectsProperties?.isAsMemoryBufferChecked} />
                            <label htmlFor="asMemoryBufferChecked">As Memory Buffer</label>
                        </div>
                        <div style={{ padding: `${globalSizeFactor * 0.4}vh` }}>
                            <Checkbox name='isAsyncChecked' inputId="asyncChecked" onChange={saveData} checked={objectsProperties?.isAsyncChecked} />
                            <label htmlFor="asyncChecked" >Async</label>
                        </div>
                    </span>
                    {objectsProperties?.objectsToSave?.length > 0 ? <Button style={{ width: `${globalSizeFactor * 1.5 * 13}vh`, margin: `${globalSizeFactor * 0.3}vh` }} label='Save Selected As Raw Vector Map Layer' onClick={() => { handleSaveAsRawVectorMapLayer(false) }} /> :
                        <Button style={{ width: `${globalSizeFactor * 1.5 * 13}vh`, margin: `${globalSizeFactor * 0.3}vh` }} disabled label='Save Selected As Raw Vector Map Layer' />}
                    <Button style={{ width: `${globalSizeFactor * 1.5 * 13}vh`, margin: `${globalSizeFactor * 0.3}vh` }} label='Save All As Raw Vector Map Layer' onClick={() => { handleSaveAsRawVectorMapLayer(true) }} />
                </div>
                <div style={{ display: 'flex', flexDirection: 'column', justifyContent: 'space-between', alignItems: 'flex-end', width: '40%' }}>
                    <Button label='Cancel Async Saving Objects' style={{ width: `${globalSizeFactor * 1.5 * 6}vh`, margin: `${globalSizeFactor * 0.3}vh` }} onClick={handleCancelAsyncSavingClick} />
                    <span style={{ width: '100%' }}>
                        <label htmlFor="AdditionalFiles">Additional Files:</label>
                        <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 7}vh`, maxHeight: `${globalSizeFactor * 7}vh` }} id='AdditionalFiles' options={objectsProperties?.additionalFiles} ></ListBox>
                    </span>
                </div>
            </Fieldset>)
    }
    const getLoadObjsFromRawVectorData = () => {
        return <div style={{ display: 'flex', justifyContent: 'center' }}>
            <Button label='Load Objects from Raw Vector Data' onClick={() => {
                dispatch(setTypeObjectWorldDialogSecond({
                    secondDialogHeader: 'Load Objects from Raw Vector Data',
                    secondDialogComponent: <LoadObjsFromRawVectorData />
                }));
            }} />
        </div>
    }
    const getSubItemsIdsFieldSet = () => {
        return (<Fieldset style={{ width: `${globalSizeFactor * 1.5 * 20}vh` }} className="form__space-around form__column-fieldset" legend="Sub Items Visibility">
            Separate Input By Spaces:
            <div style={{ display: "flex", flexDirection: 'row', justifyContent: 'space-between', width: '100%' }}>
                <label htmlFor="subItemsIds">Sub Items Ids :</label>
                <InputText id='subItemsIds' value={objectsProperties?.subItemsIds ?? ""} name='subItemsIds' onChange={saveData} />
            </div>
            <div style={{ display: "flex", flexDirection: 'row', justifyContent: 'space-between' }}>
                <div>
                    <Checkbox name='subItemsIdsVisible' inputId="subItemsIdsVisible" onChange={saveData} checked={objectsProperties?.subItemsIdsVisible} />
                    <label style={{ paddingLeft: `${globalSizeFactor * 1.5 * 0.3}vh` }} htmlFor="subItemsIdsVisible">Visible</label>
                </div>
                <div>
                    <Button label='Set' onClick={e => handleSetSubItemsIdsClick(true)} />
                    <Button label='Get' onClick={e => handleSetSubItemsIdsClick(false)} />
                </div>
            </div>
        </Fieldset>)
    }
    //#endregion

    return (
        <div style={{ display: "flex", flexDirection: 'column' }}>
            <div style={{ display: "flex", flexDirection: 'row', justifyContent: 'space-between' }}>
                <div style={{ width: '45%' }}>
                    <ListBox emptyMessage={() => { return <div></div> }} style={{ userSelect: 'none' }} listStyle={{ minHeight: `${globalSizeFactor * 55}vh`, maxHeight: `${globalSizeFactor * 55}vh` }} multiple name="objectsToSave" value={objectsProperties?.objectsToSave} onChange={handleObjectsListChange} options={objectsProperties?.allObjectsOfOverlay} optionLabel="label" />
                    <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
                        <Button label='Clear' onClick={() => {
                            props.tabInfo.setPropertiesCallback("objectsProperties", "objectsToSave", []);
                        }} />
                    </div>
                </div>
                <div style={{ width: '55%' }}>
                    {getFileFieldSet()}
                    {getMemoryBufferFieldSet()}
                    {getRawVectorMapLayerFieldset()}
                    {getLoadObjsFromRawVectorData()}
                </div>
            </div>
            {getSubItemsIdsFieldSet()}

            <ConfirmDialog
                rejectLabel={isConfirmDialogFooter ? 'Cancel' : undefined}
                acceptLabel={isConfirmDialogFooter ? 'OK' : undefined}
                reject={isConfirmDialogFooter ? () => { } : undefined}
                accept={isConfirmDialogFooter ? continueSaveFunction : undefined}
                contentClassName={isConfirmDialogFooter ? '' : 'form__confirm-dialog-content'}
                message={confirmDialogMessage}
                header={confirmDialogHeader}
                footer={isConfirmDialogFooter ? undefined : <div></div>}
                visible={confirmDialogVisible}
                onHide={e => {
                    setConfirmDialogVisible(false);
                }}
            />
        </div>)
}