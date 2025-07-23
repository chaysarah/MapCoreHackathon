// External libraries
import { useState, useEffect, ReactElement, useMemo } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import cloneDeep from "lodash/cloneDeep";

// UI/component libraries
import { Fieldset } from "primereact/fieldset";
import { Checkbox } from "primereact/checkbox";
import { Messages } from "primereact/messages";
import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { ConfirmDialog } from "primereact/confirmdialog";
import { Dropdown } from "primereact/dropdown";
import { ListBox } from "primereact/listbox";
import { Dialog } from "primereact/dialog";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { InputNumber } from "primereact/inputnumber";

// Project-specific imports
import { getEnumDetailsList, getEnumValueDetails, MapCoreData, ViewportData } from 'mapcore-lib';
import "./styles/overlayManager.css";
import { OverlayManagerFormTabInfo } from "./overlayManagerForm";
import SaveToFile from "../overlayForms/saveToFile";
import { Properties } from "../../../dialog";
import UploadFilesCtrl, { UploadTypeEnum } from "../../../shared/uploadFilesCtrl";
import GridCoordinateSystemDataset from "../../../shared/ControlsForMapcoreObjects/coordinateSystemCtrl/gridCoordinateSystemDataset";
import TreeNodeModel, { objectWorldNodeType } from '../../../../shared/models/tree-node.model'
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler"
import { setObjectWorldTree, setTypeObjectWorldDialogSecond, setSelectedNodeInTree, setSchemesMemoryBuffer, addSavedSchemeDataToMap } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions"
import objectWorldTreeService from "../../../../../services/objectWorldTree.service"
import generalService from "../../../../../services/general.service";
import dialogStateService from "../../../../../services/dialogStateService";
import savedFilesService, { SavedFileData } from "../../../../../services/savedFiles.service";


export class OverlayManagerGeneralFormPropertiesState implements Properties {
    scaleSteps: Float32Array;
    isShowGeo: boolean;
    isNonExistentAmplifiers: boolean;
    symbologyStandard: any;
    /* Overlay Operations */
    isTopMostMode: boolean;
    isBlockMode: boolean;
    isBlockedTransparency: boolean;
    checkboxArr: {
        MASK: number;
        check: boolean;
    }[];
    CollectionsMode: any;
    scaleFactor: number;
    minScale: number;
    state: string;

    static getDefault(p: any): OverlayManagerGeneralFormPropertiesState {
        let returnedDefaults: any = {};
        runCodeSafely(() => {
            let { selectedNodeInTree, overlayManager, mcSelectedViewport } = p;
            let ESymbologyStandard = getEnumDetailsList(MapCore.IMcObject.ESymbologyStandard);
            let ECollectionsMode = getEnumDetailsList(MapCore.IMcOverlayManager.ECollectionsMode);

            let tempCheckboxArr = [{ MASK: 1, check: false }, { MASK: 2, check: false }, { MASK: 4, check: false }, { MASK: 8, check: false }, { MASK: 16, check: false }
                , { MASK: 32, check: false }, { MASK: 64, check: false }, { MASK: 128, check: false }, { MASK: 256, check: false }, { MASK: 512, check: false }]
            let num = overlayManager.GetCancelScaleMode(null)

            const mcOverlayManager = selectedNodeInTree.nodeMcContent as MapCore.IMcOverlayManager;

            let isTopMostMode = false;
            runMapCoreSafely(() => {
                isTopMostMode = mcOverlayManager.GetTopMostMode(mcSelectedViewport);
            }, 'ObjectForms/General.getDefaults => IMcOverlayManager.GetTopMostMode', true)

            let isBlockMode = false;
            runMapCoreSafely(() => {
                isBlockMode = mcOverlayManager.GetMoveIfBlockedMode(mcSelectedViewport);
            }, 'ObjectForms/General.getDefaults => IMcOverlayManager.GetMoveIfBlockedMode', true)

            let isBlockedTransparency = false;
            runMapCoreSafely(() => {
                isBlockedTransparency = mcOverlayManager.GetBlockedTransparencyMode(mcSelectedViewport);
            }, 'ObjectForms/General.getDefaults => IMcOverlayManager.GetBlockedTransparencyMode', true)

            let scaleFactor = 0;
            runMapCoreSafely(() => {
                scaleFactor = mcOverlayManager.GetScaleFactor(mcSelectedViewport)
            }, 'ObjectForms/General.getDefaults => IMcOverlayManager.GetScaleFactor', true)

            let collectionMode: MapCore.IMcOverlayManager.ECollectionsMode = MapCore.IMcOverlayManager.ECollectionsMode.ECM_AND;
            runMapCoreSafely(() => {
                collectionMode = mcOverlayManager.GetCollectionsMode(mcSelectedViewport)
            }, 'ObjectForms/General.getDefaults => IMcOverlayManager.GetTopMostMode', true)
            const collectionModeEnum = getEnumValueDetails(collectionMode, ECollectionsMode)

            let minScale = 0;
            runMapCoreSafely(() => {
                minScale = mcOverlayManager.GetEquidistantAttachPointsMinScale(mcSelectedViewport);
            }, 'ObjectForms/General.getDefaults => IMcOverlayManager.GetEquidistantAttachPointsMinScale', true)

            let state = '';
            runMapCoreSafely(() => {
                state = mcOverlayManager.GetState(mcSelectedViewport).toString().replace(",", " ");
            }, 'ObjectForms/General.getDefaults => IMcOverlayManager.GetState', true)

            let scaleSteps: Float32Array = new Float32Array();
            runMapCoreSafely(() => {
                scaleSteps = mcOverlayManager.GetScreenTerrainItemsConsistencyScaleSteps(mcSelectedViewport)
            }, 'ObjectForms/General.getDefaults => IMcOverlayManager.GetScreenTerrainItemsConsistencyScaleSteps', true)

            returnedDefaults = {
                scaleSteps: scaleSteps,
                isShowGeo: generalService.ObjectWorldTreeProperties.isShowGeo,
                isNonExistentAmplifiers: generalService.ObjectWorldTreeProperties.isNonExistentAmplifiers,
                symbologyStandard: getEnumValueDetails(generalService.ObjectWorldTreeProperties.symbologyStandard, ESymbologyStandard),
                /* Overlay Operations */
                isTopMostMode: isTopMostMode,
                isBlockMode: isBlockMode,
                isBlockedTransparency: isBlockedTransparency,
                checkboxArr: tempCheckboxArr.map(f => {
                    if ((num & f.MASK) == f.MASK) return { MASK: f.MASK, check: true };
                    else return { MASK: f.MASK, check: false }
                }),
                CollectionsMode: collectionModeEnum,
                scaleFactor: scaleFactor,
                minScale: minScale,
                state: state,
            }
        }, 'ObjectForms/General.getDefaults');

        return returnedDefaults;
    }
}

export class OverlayManagerGeneralFormProperties extends OverlayManagerGeneralFormPropertiesState {
    confirmDialogVisible: boolean;
    confirmDialogMessage: string;
    confirmDialogHeader: string;
    isConfirmDialogFooter: boolean;
    continueSaveFunction: () => void;
    schemesToSave: any[];
    isSaveAllFile: boolean | null;
    isContinueSaveChecked: boolean;
    formatDropDownValue: any;
    idToScheme: number | null;
    NameToScheme: string;
    idToConditionalSelectors: number | null;
    NameToConditionalSelectors: string;
    chooseViewportsToOperations: any;
    selectViewportOfOverlayManager: any;
    selectObjectOfViewport: any[];
    link: any;

    static getDefault(p: any): OverlayManagerGeneralFormProperties {
        let { selectedNodeInTree, overlayManager } = p;
        let EStorageFormat = getEnumDetailsList(MapCore.IMcOverlayManager.EStorageFormat);

        let stateDefaults = super.getDefault({ ...p, mcSelectedViewport: null });
        let defaults: OverlayManagerGeneralFormProperties = {
            ...stateDefaults,
            confirmDialogVisible: false,
            confirmDialogMessage: '',
            confirmDialogHeader: '',
            isConfirmDialogFooter: false,
            continueSaveFunction: () => { },
            schemesToSave: [],
            isSaveAllFile: null,
            isContinueSaveChecked: false,
            formatDropDownValue: getEnumValueDetails(MapCore.IMcOverlayManager.EStorageFormat.ESF_JSON, EStorageFormat),
            idToScheme: null,
            NameToScheme: "",
            idToConditionalSelectors: null,
            NameToConditionalSelectors: "",
            chooseViewportsToOperations: null,
            selectViewportOfOverlayManager: null,
            selectObjectOfViewport: [],
            link: "",
        }
        return defaults;
    }
}

export default function OverlayManagerGeneralForm(props: { tabInfo: OverlayManagerFormTabInfo }) {
    const dispatch = useDispatch();

    const treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const schemesMemoryBuffer = useSelector((state: AppState) => state.objectWorldTreeReducer.schemesMemoryBuffer);
    const selectedNodeInTree: TreeNodeModel = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [gridCoordinateSystems, setGridCoordinateSystems] = useState<MapCore.IMcGridCoordinateSystem>(selectedNodeInTree.nodeMcContent.GetCoordinateSystemDefinition())
    const [overlayManager, setOverlayManager] = useState<MapCore.IMcOverlayManager>(selectedNodeInTree.nodeMcContent)
    const [allSchemesOfOverlayManager, setAllSchemesOfOverlayManager] = useState(selectedNodeInTree.children.filter(c => c.nodeType == objectWorldNodeType.OBJECT_SCHEME))
    const [VDLockscheme, setVDLockscheme] = useState(false)
    const [schemeOrConditional, setSchemeOrConditional] = useState(0)
    const [lockschemeList, setLockschemeList] = useState([])
    const [list, setList] = useState([])
    const [listHeader, setListHeader] = useState("Lock Conditional Selectors")
    const [listObjectOfViewport, setListObjectOfViewport] = useState([])
    const [listViewportOfOverlayManger, setlistViewportOfOverlayManger] = useState<any[]>([])
    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        chooseViewportsToOperations: false,
        selectedNodeInTree: false,
        treeRedux: false,
    })
    const enumDetails = useMemo(() => ({
        EStorageFormat: getEnumDetailsList(MapCore.IMcOverlayManager.EStorageFormat),
        EVersion: getEnumDetailsList(MapCore.IMcOverlayManager.ESavingVersionCompatibility),
        ESymbologyStandard: getEnumDetailsList(MapCore.IMcObject.ESymbologyStandard),
        ECollectionsMode: getEnumDetailsList(MapCore.IMcOverlayManager.ECollectionsMode),
    }), []);
    const savedFileTypeOptions = useMemo(() => [
        { name: 'MapCore schemes Files (*.mcsch, *.mcsch.json , *.m, *.json)', extension: '.mcsch' },
        { name: 'MapCore schemes Binary Files (*.mcsch,*.m)', extension: '.mcsch' },
        { name: 'MapCore schemes Json Files (*.mcsch.json, *.json)', extension: '.mcsch.json' },
        { name: 'All Files', extension: '' },
    ], []);

    //#region UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            // initialize first time the tab is loaded only
            const root = document.documentElement;
            let pixelWidth = window.innerHeight * 0.6 * globalSizeFactor;
            root.style.setProperty('--lock-scheme-dialog-width', `${pixelWidth}px`);
            if (!props.tabInfo.properties.generalProperties) {
                props.tabInfo.setInitialStatePropertiesCallback("generalProperties", null, OverlayManagerGeneralFormPropertiesState.getDefault({ selectedNodeInTree, overlayManager, mcSelectedViewport: null }));
                props.tabInfo.setPropertiesCallback("generalProperties", null, OverlayManagerGeneralFormProperties.getDefault({ selectedNodeInTree, overlayManager }));
            }
        }, 'OverlayManagerForm/OverlayManagerGeneralForm.useEffect');
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            let currentUpdatedOverLayManager = objectWorldTreeService.getNodeByKey(treeRedux, selectedNodeInTree.key) as TreeNodeModel;//nessecary!
            let treeNodeModels = [...currentUpdatedOverLayManager.children.filter(c => c.nodeType == objectWorldNodeType.OBJECT_SCHEME)];
            treeNodeModels = treeNodeModels.map(node => ({
                ...node, label: savedFilesService.getSchemeOrObjectLabel(node)
            }))
            setAllSchemesOfOverlayManager(treeNodeModels)
            let selectedSchemes = [];
            props.tabInfo.properties.generalProperties?.schemesToSave.forEach((scheme: TreeNodeModel) => {
                const foundedNode = treeNodeModels.find(node => node.nodeMcContent == scheme.nodeMcContent);
                selectedSchemes = [...selectedSchemes, foundedNode];
            });
            props.tabInfo.setPropertiesCallback("generalProperties", 'schemesToSave', selectedSchemes);
        }, 'ObjectForms/General.useEffect => properties.generalProperties');
    }, [treeRedux, selectedNodeInTree])
    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.chooseViewportsToOperations) {
                const mcSelectedViewport = props.tabInfo.properties.generalProperties?.chooseViewportsToOperations ? props.tabInfo.properties.generalProperties?.chooseViewportsToOperations.viewport.viewport : null;

                const defaults = OverlayManagerGeneralFormPropertiesState.getDefault({ selectedNodeInTree, overlayManager, mcSelectedViewport });

                let updatedGeneralProperties = {
                    ...props.tabInfo.properties.generalProperties,
                    isTopMostMode: defaults.isTopMostMode,
                    isBlockMode: defaults.isBlockMode,
                    isBlockedTransparency: defaults.isBlockedTransparency,
                    CollectionsMode: defaults.CollectionsMode,
                    scaleFactor: defaults.scaleFactor,
                    minScale: defaults.minScale,
                    state: defaults.state,
                    scaleSteps: defaults.scaleSteps,
                }
                props.tabInfo.setPropertiesCallback("generalProperties", null, updatedGeneralProperties);
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, chooseViewportsToOperations: true })
            }
        }, 'ObjectForms/General.useEffect => props.tabInfo.properties.generalProperties?.chooseViewportsToOperations');
    }, [props.tabInfo.properties.generalProperties?.chooseViewportsToOperations/*, selectedNodeInTree*/])
    useEffect(() => {
        if (isMountedUseEffect.selectedNodeInTree) {
            setGridCoordinateSystems(selectedNodeInTree.nodeMcContent.GetCoordinateSystemDefinition())
            setOverlayManager(selectedNodeInTree.nodeMcContent)

            props.tabInfo.setInitialStatePropertiesCallback("generalProperties", null, OverlayManagerGeneralFormPropertiesState.getDefault({ selectedNodeInTree, overlayManager, mcSelectedViewport: null }));
            props.tabInfo.setPropertiesCallback("generalProperties", null, OverlayManagerGeneralFormProperties.getDefault({ selectedNodeInTree, overlayManager }));
        }
        else {
            setIsMountedUseEffect({ ...isMountedUseEffect, selectedNodeInTree: true })
        }
    }, [selectedNodeInTree])
    useEffect(() => {
        runCodeSafely(() => {
            let list: { name: string, viewport: ViewportData }[] = [];
            MapCoreData.viewportsData.forEach((v: ViewportData) => {
                let vpOverlayManager = v.viewport.GetOverlayManager();
                if (vpOverlayManager === overlayManager)
                    list.push({ name: generalService.getObjectName(v, "Viewport",), viewport: v })
            })
            setlistViewportOfOverlayManger(list)

            if (isMountedUseEffect.treeRedux) {
                let tempCheckboxArr = props.tabInfo.properties.generalProperties?.checkboxArr ||
                    [{ MASK: 1, check: false }, { MASK: 2, check: false }, { MASK: 4, check: false }, { MASK: 8, check: false }, { MASK: 16, check: false }
                        , { MASK: 32, check: false }, { MASK: 64, check: false }, { MASK: 128, check: false }, { MASK: 256, check: false }, { MASK: 512, check: false }]
                let num = overlayManager.GetCancelScaleMode(props.tabInfo.properties.generalProperties?.chooseViewportsToOperations ? props.tabInfo.properties.generalProperties?.chooseViewportsToOperations.viewport.viewport : null)
                props.tabInfo.setPropertiesCallback("generalProperties", "checkboxArr", tempCheckboxArr.map(f => {
                    if ((num & f.MASK) == f.MASK) return { MASK: f.MASK, check: true };
                    else return { MASK: f.MASK, check: false }
                }));
                props.tabInfo.setCurrStatePropertiesCallback("generalProperties", "checkboxArr", tempCheckboxArr.map(f => {
                    if ((num & f.MASK) == f.MASK) return { MASK: f.MASK, check: true };
                    else return { MASK: f.MASK, check: false }
                }));
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, treeRedux: true })
            }
        }, "OverlayManagerGeneralForm.useEffect")
    }, [treeRedux])
    //#endregion
    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("generalProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in OverlayManagerGeneralFormPropertiesState.getDefault({ selectedNodeInTree, overlayManager, mcSelectedViewport: null })) {
                props.tabInfo.setCurrStatePropertiesCallback("generalProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value)
            }
            if (event.target.name == "selectViewportOfOverlayManager") objectListRefresh(event.target.value)
        }, "OverlayManagerGeneralForm.saveData => onChange")
    }
    const refreshTree = () => {
        let tree: TreeNodeModel = objectWorldTreeService.buildTree()
        dispatch(setObjectWorldTree(tree))
    }

    const handleFileUpload = (virtualFSPath: string, selectedOption: UploadTypeEnum) => {
        runCodeSafely(() => {
            let storageFormat: { Value?: MapCore.IMcOverlayManager.EStorageFormat } = {};
            let version: { Value?: number } = {};
            let loadedObjectScheme: MapCore.IMcObjectScheme[] = [];
            runMapCoreSafely(() => {
                loadedObjectScheme = overlayManager.LoadObjectSchemesFromFile(virtualFSPath, null, null, storageFormat, version)
            }, 'OverlayManagerGeneralForm.handleFileUpload => IMcOverlayManager.LoadObjectSchemesFromFile', true)
            objectWorldTreeService.deleteSystemDirectories([virtualFSPath.split('/')[0]])

            const versionEnum = enumDetails.EVersion.find(versionEnum => versionEnum.code == version.Value);
            const finalVersionEnum = versionEnum ? versionEnum.theEnum : getEnumValueDetails(MapCore.IMcOverlayManager.ESavingVersionCompatibility.ESVC_LATEST, enumDetails.EVersion).theEnum;
            const schemeSaveData = new SavedFileData(finalVersionEnum, storageFormat.Value, null)
            loadedObjectScheme.forEach(scheme => {
                dispatch(addSavedSchemeDataToMap(scheme, schemeSaveData));
            })
            refreshTree();
        }, "OverlayManagerGeneralForm.handleFileUpload => onChange")
    };
    const handleSaveSchemesToFileOK = (fileName: string, fileType: string) => {
        runCodeSafely(() => {
            let schemesToSave: MapCore.IMcObjectScheme[] = props.tabInfo.properties.generalProperties?.schemesToSave.map(sch => sch.nodeMcContent);
            let format = fileType.endsWith('.json') ?
                MapCore.IMcOverlayManager.EStorageFormat.ESF_JSON :
                MapCore.IMcOverlayManager.EStorageFormat.ESF_MAPCORE_BINARY;
            const { version, isAllSameVersion } = savedFilesService.getObjectsOrSchemesVersion(schemesToSave, false);
            const continueSaveFunc = () => {
                let uint8ArrayData;
                runMapCoreSafely(() => {
                    uint8ArrayData = props.tabInfo.properties.generalProperties?.isSaveAllFile ?
                        overlayManager.SaveAllObjectSchemes(format, version.theEnum) :
                        overlayManager.SaveObjectSchemes(schemesToSave, format, version.theEnum);
                }, "OverlayManagerGeneralForm.handleSaveSchemesToFileOK => SaveAllObjectSchemes/SaveObjectSchemes", true)

                const schemesToSaveFileName = props.tabInfo.properties.generalProperties?.isSaveAllFile ? allSchemesOfOverlayManager : props.tabInfo.properties.generalProperties?.schemesToSave;
                let schemeSaveData = new SavedFileData(version.theEnum, format, null)
                const finalFileName = `${fileName}${fileType}`;
                if (props.tabInfo.properties.generalProperties?.isContinueSaveChecked) {
                    schemeSaveData = new SavedFileData(version.theEnum, format, finalFileName)
                }
                MapCore.IMcMapDevice.DownloadBufferAsFile(uint8ArrayData, finalFileName);
                schemesToSaveFileName.forEach((scheme: { nodeMcContent: MapCore.IMcObjectScheme }) => {
                    dispatch(addSavedSchemeDataToMap(scheme.nodeMcContent, schemeSaveData));
                });
                refreshTree();
            }
            if (isAllSameVersion) {
                continueSaveFunc();
            }
            else {
                props.tabInfo.setPropertiesCallback("generalProperties", "confirmDialogVisible", true);
                props.tabInfo.setPropertiesCallback("generalProperties", "confirmDialogHeader", 'Conflict Versions');
                props.tabInfo.setPropertiesCallback("generalProperties", "confirmDialogMessage", `The objects are of different versions. Save using the most recent detected version (${version.name}), or cancel?`);
                props.tabInfo.setPropertiesCallback("generalProperties", "isConfirmDialogFooter", true);
                props.tabInfo.setPropertiesCallback("generalProperties", "continueSaveFunction", continueSaveFunc);
            }
            dispatch(setTypeObjectWorldDialogSecond(undefined))
        }, 'SaveObjectsToFile.handleSaveToFileOk => onClick')

    }
    const handleSaveFileClick = (isSaveAll: boolean) => {
        props.tabInfo.setPropertiesCallback("generalProperties", "isSaveAllFile", isSaveAll);
        runMapCoreSafely(() => {
            dispatch(setTypeObjectWorldDialogSecond({
                secondDialogHeader: isSaveAll ? 'Save All to File' : 'Save to File',
                secondDialogComponent: <SaveToFile
                    savedFileTypeOptions={savedFileTypeOptions}
                    handleSaveToFileOk={handleSaveSchemesToFileOK} />
            }))
        }, "OverlayManagerGeneralForm.handleSaveFileClick => onClick", true)
    }
    const handleMemoryBufferLoad = () => {
        runCodeSafely(() => {
            if (!schemesMemoryBuffer) {
                props.tabInfo.setPropertiesCallback("generalProperties", "confirmDialogVisible", true);
                props.tabInfo.setPropertiesCallback("generalProperties", "confirmDialogMessage", 'schenes wasnt saved to memory buffer.');
                props.tabInfo.setPropertiesCallback("generalProperties", "confirmDialogHeader", 'Empty buffer');
                props.tabInfo.setPropertiesCallback("generalProperties", "isConfirmDialogFooter", false);
            }
            else {
                let storageFormat: { Value?: MapCore.IMcOverlayManager.EStorageFormat } = {}
                let version: { Value?: number } = {}
                let loadedSchemes: MapCore.IMcObjectScheme[] = [];
                runMapCoreSafely(() => {
                    loadedSchemes = overlayManager.LoadObjectSchemes(schemesMemoryBuffer, null, null, storageFormat, version)
                }, 'OverlayManagerGeneralForm.handleMemoryBufferLoad => IMcOverlayManager.LoadObjectSchemes', true)
                let versionEnum = enumDetails.EVersion.filter(v => v.code === version.Value)[0].theEnum;
                const schemeSaveData = new SavedFileData(versionEnum, storageFormat.Value, null)
                loadedSchemes.forEach(scheme => {
                    dispatch(addSavedSchemeDataToMap(scheme, schemeSaveData));
                })
                refreshTree();
            }
        }, "OverlayManagerGeneralForm.handleMemoryBufferLoad => onClick")
    };
    const handleSaveMemoryBufferClick = (isSaveAll: boolean) => {
        runCodeSafely(() => {
            const mcSchemesToSave: MapCore.IMcObjectScheme[] = isSaveAll ? allSchemesOfOverlayManager.map(schemeNode => schemeNode.nodeMcContent) : props.tabInfo.properties.generalProperties?.schemesToSave.map(obj => obj.nodeMcContent);
            const format: MapCore.IMcOverlayManager.EStorageFormat = props.tabInfo.properties.generalProperties?.formatDropDownValue.theEnum;
            const { version, isAllSameVersion } = savedFilesService.getObjectsOrSchemesVersion(mcSchemesToSave, false);
            const continueSaveFunc = () => {
                let uint8ArrayData: Uint8Array
                runMapCoreSafely(() => {
                    uint8ArrayData = isSaveAll ? overlayManager.SaveAllObjectSchemes(format, version.theEnum) :
                        overlayManager.SaveObjectSchemes(mcSchemesToSave, format, version.theEnum);
                }, "OverlayManagerGeneralForm.handleSaveMemoryBufferClick => SaveObjectSchemes", true)
                dispatch(setSchemesMemoryBuffer(uint8ArrayData));
                const schemeSaveData = new SavedFileData(version.theEnum, format, null)
                mcSchemesToSave.forEach(scheme => {
                    dispatch(addSavedSchemeDataToMap(scheme, schemeSaveData));
                })
                refreshTree();
            }

            if (isAllSameVersion) {
                continueSaveFunc();
            }
            else {
                props.tabInfo.setPropertiesCallback("generalProperties", "confirmDialogVisible", true);
                props.tabInfo.setPropertiesCallback("generalProperties", "confirmDialogHeader", 'Conflict Versions');
                props.tabInfo.setPropertiesCallback("generalProperties", "confirmDialogMessage", `The objects are of different versions. Save using the most recent detected version (${version.name}), or cancel?`);
                props.tabInfo.setPropertiesCallback("generalProperties", "isConfirmDialogFooter", true);
                props.tabInfo.setPropertiesCallback("generalProperties", "continueSaveFunction", continueSaveFunc);
            }
        }, "OverlayManagerGeneralForm.handleSaveMemoryBufferClick => onClick")
    }

    const objectListRefresh = (selectViewportOfOverlayManager: { name: string, viewport: ViewportData }) => {
        runCodeSafely(() => {
            let list: any[] = []
            if (selectViewportOfOverlayManager) {
                let Overlays = selectViewportOfOverlayManager.viewport.viewport.GetOverlayManager().GetOverlays();
                Overlays.forEach(o => {
                    let objects = o.GetObjects()
                    objects.forEach((obj: MapCore.IMcObject) => {
                        let TreeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, obj)
                        list.push({ name: generalService.getObjectName(TreeNodeModel, "Objects"), obj: obj });
                    })
                });
                setListObjectOfViewport(list)
            }
        }, "overlayManagerGeneralForm.objectListRefresh")
    }
    const cancelScreen = () => {
        runCodeSafely(() => { overlayManager.CancelScreenArrangements(props.tabInfo.properties.generalProperties?.selectViewportOfOverlayManager.viewport.viewport) }, "overlayManagerGeneralForm.cancelScreen")
    }
    const setScreen = () => {
        runCodeSafely(() => {
            let objects = props.tabInfo.properties.generalProperties?.selectObjectOfViewport.map(o => { return o.obj })
            overlayManager.SetScreenArrangement(props.tabInfo.properties.generalProperties?.selectViewportOfOverlayManager.viewport.viewport, objects)
        }, "overlayManagerGeneralForm.setScreen")
    }

    const getSchemeById = () => {
        runCodeSafely(() => {
            let id: number = props.tabInfo.properties.generalProperties?.idToScheme;
            let Scheme: MapCore.IMcObjectScheme
            // runMapCoreSafely(() => {
            try {
                Scheme = overlayManager.GetObjectSchemeByID(id);

            } catch (error) {
                console.log(error);
                console.error(error);
                throw error

            }
            // }, "OverlayManagerGeneralForm.getSchemeById => GetObjectSchemeByID", true)
            let TreeObj: TreeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, Scheme) as TreeNodeModel
            props.tabInfo.setPropertiesCallback("generalProperties", "link", { objMC: Scheme, TreeObj: TreeObj });
        }, "overlayManagerGeneralForm.getSchemeById")
    }
    const getSchemeByName = () => {
        runCodeSafely(() => {
            let Scheme: MapCore.IMcObjectScheme
            runMapCoreSafely(() => {
                overlayManager.GetObjectSchemeByName(props.tabInfo.properties.generalProperties?.NameToScheme);
            }, "OverlayManagerGeneralForm.getSchemeByName => GetObjectSchemeByName", true)
            let obj: TreeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, Scheme) as TreeNodeModel
            props.tabInfo.setPropertiesCallback("generalProperties", "link", { objMC: Scheme, TreeObj: obj });
        }, "overlayManagerGeneralForm.getSchemeByName")
    }
    const getConditionalSelectorById = () => {
        runCodeSafely(() => {
            let id: number = props.tabInfo.properties.generalProperties?.idToConditionalSelectors;
            let ConditionalSelector: MapCore.IMcConditionalSelector;
            runMapCoreSafely(() => {
                ConditionalSelector = overlayManager.GetConditionalSelectorByID(id);
            }, "OverlayManagerGeneralForm.getConditionalSelectorById => GetConditionalSelectorByID", true)
            let obj: TreeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, ConditionalSelector) as TreeNodeModel
            props.tabInfo.setPropertiesCallback("generalProperties", "link", { objMC: ConditionalSelector, TreeObj: obj });
        }, "overlayManagerGeneralForm.getConditionalSelectorById")
    }
    const getConditionalSelectorByName = () => {
        runCodeSafely(() => {
            let ConditionalSelector: MapCore.IMcConditionalSelector;
            runMapCoreSafely(() => {
                ConditionalSelector = overlayManager.GetConditionalSelectorByName(props.tabInfo.properties.generalProperties?.NameToConditionalSelectors);
            }, "OverlayManagerGeneralForm.getConditionalSelectorByName => GetConditionalSelectorByName", true)
            let obj: TreeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, ConditionalSelector) as TreeNodeModel
            props.tabInfo.setPropertiesCallback("generalProperties", "link", { objMC: ConditionalSelector, TreeObj: obj });
        }, "overlayManagerGeneralForm.getConditionalSelectorByName")
    }
    const resetlockschemeList = (schemeOrConditional: number) => {
        runCodeSafely(() => {
            setSchemeOrConditional(schemeOrConditional);
            let list: any[] = [], lockList;
            if (schemeOrConditional == 1) {
                list = overlayManager.GetConditionalSelectors().map(c => {
                    let TreeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, c)
                    return { name: TreeNodeModel.label, check: overlayManager.IsConditionalSelectorLocked(c) ? true : false, obj: c }
                })
                setListHeader("Lock Conditional Selectors   .")
            }
            else {
                list = overlayManager.GetObjectSchemes().map(s => {
                    let TreeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, s)
                    return { name: TreeNodeModel.label, check: overlayManager.IsObjectSchemeLocked(s) ? true : false, obj: s }
                })
                setListHeader("Lock Object Schemes   .")
            }
            setList(list)
            lockList = list.filter(o => (o.check === true));
            setLockschemeList(lockList);
        }, "overlayManagerGeneralForm.resetlockschemeList")
    }
    function ShowLockscheme() {
        const Oklock = () => {
            runCodeSafely(() => {
                let l = list.filter(i => !lockschemeList.includes(i))
                if (schemeOrConditional == 1) {
                    lockschemeList.map((s: { obj: MapCore.IMcConditionalSelector; }) => { overlayManager.SetConditionalSelectorLock(s.obj, true) });
                    l.map((s: { obj: MapCore.IMcConditionalSelector; }) => { overlayManager.SetConditionalSelectorLock(s.obj, false) });
                }
                else {
                    lockschemeList.map((s: { obj: MapCore.IMcObjectScheme; }) => { overlayManager.SetObjectSchemeLock(s.obj, true) });
                    l.map((s: { obj: MapCore.IMcObjectScheme; }) => { overlayManager.SetObjectSchemeLock(s.obj, false) });
                }
                setVDLockscheme(false)
            }, "overlayManagerGeneralForm.ShowLockscheme.Oklock")
        }
        return <>
            <Dialog className="overlay-manager__scroll-dialog-lock-scheme" onHide={() => { setVDLockscheme(false) }} visible={VDLockscheme} header={listHeader} >
                <DataTable selectionMode="multiple" value={list} size="normal" dataKey="obj" selection={lockschemeList} onSelectionChange={(e) => { setLockschemeList(e.value) }}  >
                    <Column selectionMode="multiple"></Column>
                    <Column field="name" header="name"></Column>
                </DataTable>
                <Button onClick={Oklock}>Ok</Button>
            </Dialog>
        </>
    }

    const initializeSymbology = () => {
        runCodeSafely(() => {
            let success = true
            try {
                overlayManager.InitializeSymbologyStandardSupport(props.tabInfo.properties.generalProperties?.symbologyStandard.theEnum, props.tabInfo.properties.generalProperties?.isShowGeo, props.tabInfo.properties.generalProperties?.isNonExistentAmplifiers);
            } catch (error) {
                success = false
            }
            if (success) {
                generalService.ObjectWorldTreeProperties.isShowGeo = props.tabInfo.properties.generalProperties?.isShowGeo;
                generalService.ObjectWorldTreeProperties.isNonExistentAmplifiers = props.tabInfo.properties.generalProperties?.isNonExistentAmplifiers;
                generalService.ObjectWorldTreeProperties.symbologyStandard = props.tabInfo.properties.generalProperties?.symbologyStandard.theEnum;
                dialogStateService.applyDialogState(["generalProperties.isShowGeo",
                    "generalProperties.isNonExistentAmplifiers",
                    "generalProperties.symbologyStandard"])
            }
        }, "overlayManagerGeneralForm.initializeSymbology")
    }
    const applyTopMostMode = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                overlayManager.SetTopMostMode(props.tabInfo.properties.generalProperties?.isTopMostMode,
                    props.tabInfo.properties.generalProperties?.chooseViewportsToOperations ? props.tabInfo.properties.generalProperties?.chooseViewportsToOperations.viewport.viewport : null);
            }, "overlayManagerGeneralForm.applyTopMostMode => SetTopMostMode", true)
            dialogStateService.applyDialogState(["generalProperties.isTopMostMode"])
        }, "overlayManagerGeneralForm.applyTopMostMode")
    }
    const applyBlockMode = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                overlayManager.SetMoveIfBlockedMode(props.tabInfo.properties.generalProperties?.isBlockMode,
                    props.tabInfo.properties.generalProperties?.chooseViewportsToOperations ? props.tabInfo.properties.generalProperties?.chooseViewportsToOperations.viewport.viewport : null);
            }, "overlayManagerGeneralForm.applyBlockMode => SetMoveIfBlockedMode", true)
            dialogStateService.applyDialogState(["generalProperties.isBlockMode"])
        }, "overlayManagerGeneralForm.applyBlockMode")
    }
    const applyCollectionsMode = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                overlayManager.SetCollectionsMode(props.tabInfo.properties.generalProperties?.CollectionsMode.theEnum,
                    props.tabInfo.properties.generalProperties?.chooseViewportsToOperations ? props.tabInfo.properties.generalProperties?.chooseViewportsToOperations.viewport.viewport : null);
            }, "overlayManagerGeneralForm.applyCollectionsMode => SetCollectionsMode", true)
            dialogStateService.applyDialogState(["generalProperties.CollectionsMode"])
        }, "overlayManagerGeneralForm.applyCollectionsMode")
    }
    const applyBlockedTransparencyMode = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                overlayManager.SetBlockedTransparencyMode(props.tabInfo.properties.generalProperties?.isBlockedTransparency,
                    props.tabInfo.properties.generalProperties?.chooseViewportsToOperations ? props.tabInfo.properties.generalProperties?.chooseViewportsToOperations.viewport.viewport : null);
            }, "overlayManagerGeneralForm.applyBlockedTransparencyMode => SetBlockedTransparencyMode", true)
            dialogStateService.applyDialogState(["generalProperties.isBlockedTransparency"])
        }, "overlayManagerGeneralForm.applyBlockedTransparencyMode")
    }

    const applyScaleFactor = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                overlayManager.SetScaleFactor(props.tabInfo.properties.generalProperties?.scaleFactor, props.tabInfo.properties.generalProperties?.chooseViewportsToOperations ? props.tabInfo.properties.generalProperties?.chooseViewportsToOperations.viewport.viewport : null);
            }, "overlayManagerGeneralForm.applyScaleFactor => SetScaleFactor", true)
            dialogStateService.applyDialogState(["generalProperties.scaleFactor"])
        }, "overlayManagerGeneralForm.applyScaleFactor")

    }
    const applyMinScale = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                overlayManager.SetEquidistantAttachPointsMinScale(props.tabInfo.properties.generalProperties?.minScale, props.tabInfo.properties.generalProperties?.chooseViewportsToOperations ? props.tabInfo.properties.generalProperties?.chooseViewportsToOperations.viewport.viewport : null);
            }, "overlayManagerGeneralForm.applyMinScale => SetEquidistantAttachPointsMinScale", true)
            dialogStateService.applyDialogState(["generalProperties.minScale"])
        }, "overlayManagerGeneralForm.applyMinScale")
    }
    const applyState = () => {
        runCodeSafely(() => {
            let Array = new Uint8Array(props.tabInfo.properties.generalProperties?.state.split(" ").map(Number))
            runMapCoreSafely(() => {
                overlayManager.SetState(Array, props.tabInfo.properties.generalProperties?.chooseViewportsToOperations ? props.tabInfo.properties.generalProperties?.chooseViewportsToOperations.viewport.viewport : null)
            }, "overlayManagerGeneralForm.applyState => SetState", true)
            dialogStateService.applyDialogState(["generalProperties.state"])
        }, "overlayManagerGeneralForm.applyState")
    }
    const applyScaleSteps = () => {
        runCodeSafely(() => {
            const numbersArray = props.tabInfo.properties.generalProperties?.scaleSteps.toString().split(" ").map(Number)
            const floatArray = new Float32Array(numbersArray)
            runMapCoreSafely(() => {
                overlayManager.SetScreenTerrainItemsConsistencyScaleSteps(floatArray, props.tabInfo.properties.generalProperties?.chooseViewportsToOperations ? props.tabInfo.properties.generalProperties?.chooseViewportsToOperations.viewport.viewport : null)
            }, "overlayManagerGeneralForm.applyScaleSteps => IMcOverlayManager.SetScreenTerrainItemsConsistencyScaleSteps", true)
            dialogStateService.applyDialogState(["generalProperties.scaleSteps"])
        }, "overlayManagerGeneralForm.applyScaleSteps")
    }
    //#region DOM Functions
    const getFileFieldSet = () => {
        return <Fieldset className="form__column-fieldset" legend="File" >
            <div style={{ gap: `${globalSizeFactor * 0.75}vh` }} className="form__flex-and-row">
                <UploadFilesCtrl isDirectoryUpload={false} uploadOptions={[UploadTypeEnum.upload]} getVirtualFSPath={handleFileUpload} buttonOnly={true} />
                <Button label="Save Selected" disabled={props.tabInfo.properties.generalProperties?.schemesToSave.length == 0 ? true : false} onClick={e => handleSaveFileClick(false)} />
                <Button label="Save All" onClick={e => handleSaveFileClick(true)}></Button>
            </div>
            <div className="form__flex-and-row">
                <Checkbox name='isContinueSaveChecked' inputId="continueSaveChecked" onChange={saveData} checked={props.tabInfo.properties.generalProperties?.isContinueSaveChecked} />
                <label htmlFor="continueSaveChecked">Continue saving to this file</label>
            </div>
        </Fieldset>
    }
    const getMemoryBufferFieldSet = () => {
        return <Fieldset className="form__column-fieldset" legend={<legend>Memory Buffer</legend>}>
            <div style={{ gap: `${globalSizeFactor * 0.75}vh` }} className="form__flex-and-row">
                <Button style={{ width: `${globalSizeFactor * 8.5}vh` }} label="Load" icon="pi pi-upload" onClick={handleMemoryBufferLoad} />
                <Button label="Save Selected" onClick={e => handleSaveMemoryBufferClick(false)} disabled={props.tabInfo.properties.generalProperties?.schemesToSave.length == 0 ? true : false} ></Button>
                <Button label="Save All" onClick={e => handleSaveMemoryBufferClick(true)} disabled={allSchemesOfOverlayManager.length == 0 ? true : false}></Button>
            </div>
            <div style={{ gap: `${globalSizeFactor * 0.75}vh` }} className="form__flex-and-row">
                <label>Saving Format</label>
                <Dropdown id="formatDropDown" name='formatDropDownValue' value={props.tabInfo.properties.generalProperties?.formatDropDownValue ?? null} onChange={saveData} options={enumDetails.EStorageFormat} optionLabel="name" />
            </div>
        </Fieldset>
    }
    const getGridCoordinateSystemsFieldset = () => {
        return <GridCoordinateSystemDataset gridCoordinateSystem={gridCoordinateSystems} />
    }
    const getOverlayManagerOperationFieldset = () => {
        return <Fieldset style={{ height: '100%' }} legend="Overlay Manager Operation">
            <div style={{ width: '100%' }}>
                <div className="overlay-manager__om-operation">
                    <label>Lock scheme list:</label>
                    <Button onClick={() => { setVDLockscheme(true); resetlockschemeList(2) }}>Show</Button>
                </div>
                <div className="overlay-manager__om-operation">
                    <label>Lock conditional selector list:</label>
                    <Button onClick={() => { setVDLockscheme(true); resetlockschemeList(1) }}>Show</Button>
                </div>
            </div>
        </Fieldset>
    }
    const getNodesByIdOrNameFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend="Get Nodes By Id/Name">
            <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', width: '100%' }} >
                <div className="overlay-manager__get-node overlay-manager__left-get-node">
                    <div style={{ paddingBottom: `${globalSizeFactor * 1}vh` }}>Object Scheme:</div>
                    <div className="overlay-manager__om-operation">
                        <label>By Id: </label>
                        <span>
                            <InputNumber className="overlay-manager__get-node-input-number" value={props.tabInfo.properties.generalProperties?.idToScheme ?? 0} name="idToScheme" onValueChange={saveData} />
                            <Button disabled={!props.tabInfo.properties.generalProperties?.idToScheme} onClick={getSchemeById}>Get</Button>
                        </span>
                    </div>
                    <div className="overlay-manager__om-operation">
                        <label>By Name: </label>
                        <span>
                            <InputText className="overlay-manager__get-node-input-text" name="NameToScheme" value={props.tabInfo.properties.generalProperties?.NameToScheme ?? ""} onChange={saveData} />
                            <Button onClick={getSchemeByName}>Get</Button>
                        </span>
                    </div>
                </div>
                <div className="overlay-manager__get-node overlay-manager__right-get-node">
                    <div style={{ paddingBottom: `${globalSizeFactor * 1}vh` }}>Conditional Selector:</div>
                    <div className="overlay-manager__om-operation">
                        <label>By Id: </label>
                        <span>
                            <InputNumber className="overlay-manager__get-node-input-number" value={props.tabInfo.properties.generalProperties?.idToConditionalSelectors ?? 0} onValueChange={saveData} name="idToConditionalSelectors" />
                            <Button disabled={!props.tabInfo.properties.generalProperties?.idToConditionalSelectors} onClick={getConditionalSelectorById}>Get</Button>
                        </span>
                    </div>
                    <div className="overlay-manager__om-operation">
                        <label>By Name: </label>
                        <span>
                            <InputText className="overlay-manager__get-node-input-text" value={props.tabInfo.properties.generalProperties?.NameToConditionalSelectors ?? ""} name="NameToConditionalSelectors"></InputText>
                            <Button onClick={getConditionalSelectorByName} >Get</Button>
                        </span>
                    </div>
                </div>
            </div>
            <label onClick={() => { dispatch(setSelectedNodeInTree(props.tabInfo.properties.generalProperties?.link.TreeObj)) }} style={{ marginTop: `${globalSizeFactor * 1.5}vh`, color: "blue" }}>{props.tabInfo.properties.generalProperties?.link?.TreeObj?.label}</label>
        </Fieldset>
    }
    const getObjectSchemesFieldset = () => {
        return <Fieldset legend="Object Schemes">
            <ListBox className="overlay-manager__small-list-box-text" listStyle={{ maxHeight: `${globalSizeFactor * 18}vh`, minHeight: `${globalSizeFactor * 18}vh` }} style={{ width: '55%' }} multiple name="schemesToSave" value={props.tabInfo.properties.generalProperties?.schemesToSave} onChange={saveData} options={allSchemesOfOverlayManager} optionLabel="label" />
            <div className="form__column-container" style={{ width: '45%' }}>
                {getFileFieldSet()}
                {getMemoryBufferFieldSet()}
            </div>
        </Fieldset>
    }
    const getOverlayOperationsFieldset = () => {
        return <Fieldset className="form__space-between" legend="Parameters (Optionally Per Viewport)">
            <div style={{ width: '75%' }}>
                <div className="overlay-manager__flex-align-items-center">
                    <Button label="Apply" onClick={applyTopMostMode}></Button>
                    <Checkbox className='overlay-manager__checkbox-left-padding' name='isTopMostMode' inputId="TopMostMode" onChange={saveData} checked={props.tabInfo.properties.generalProperties?.isTopMostMode} />
                    <label htmlFor="TopMostMode">Top Most Mode</label>
                </div>
                <div className="overlay-manager__flex-align-items-center">
                    <Button label="Apply" onClick={applyBlockMode}></Button>
                    <Checkbox className='overlay-manager__checkbox-left-padding' name='isBlockMode' inputId="blockMode" onChange={saveData} checked={props.tabInfo.properties.generalProperties?.isBlockMode} />
                    <label htmlFor="blockMode" >Move If Block Mode</label>
                </div>
                <div className="overlay-manager__flex-align-items-center">
                    <Button label="Apply" onClick={applyBlockedTransparencyMode} ></Button>
                    <Checkbox className='overlay-manager__checkbox-left-padding' name='isBlockedTransparency' inputId="blockedTransparency" onChange={saveData} checked={props.tabInfo.properties.generalProperties?.isBlockedTransparency} />
                    <label htmlFor="blockedTransparency">Blocked Transparency Mode</label>
                </div>
                <div className="overlay-manager__last-overlay-operation-inputs">
                    <span className="overlay-manager__last-overlay-operation-span">
                        <Button label="Apply" onClick={() => {
                            let sum = 0;
                            props.tabInfo.properties.generalProperties?.checkboxArr &&
                                props.tabInfo.properties.generalProperties.checkboxArr.forEach(element => { if (element.check == true) sum += element.MASK });
                            overlayManager.SetCancelScaleMode(sum, props.tabInfo.properties.generalProperties?.chooseViewportsToOperations ? props.tabInfo.properties.generalProperties?.chooseViewportsToOperations.viewport.viewport : null);
                        }}></Button>
                        <label className="overlay-manager__last-overlay-operation-label" htmlFor="showGeo">Cancel Scale Mode:  </label>
                    </span>
                    <div className="form__flex-and-row">
                        {props.tabInfo.properties.generalProperties?.checkboxArr && props.tabInfo.properties.generalProperties?.checkboxArr.map((MASK, i) => {
                            return <div style={{ display: 'flex', flexDirection: 'column' }} key={i} >
                                <label style={{ textAlign: 'center' }}>{i}</label>
                                <Checkbox name={i.toString()} onChange={(e) => {
                                    let x = cloneDeep(props.tabInfo.properties.generalProperties?.checkboxArr);
                                    x[i].check = e.target.checked;
                                    props.tabInfo.setPropertiesCallback("generalProperties", "checkboxArr", x);
                                    props.tabInfo.setCurrStatePropertiesCallback("generalProperties", "checkboxArr", x);
                                }
                                } checked={props.tabInfo.properties.generalProperties?.checkboxArr[i].check}></Checkbox>
                            </div>
                        })}
                    </div>
                </div>
                <div className="overlay-manager__last-overlay-operation-inputs">
                    <span className="overlay-manager__last-overlay-operation-span">
                        <Button label="Apply" onClick={applyCollectionsMode}></Button>
                        <label className="overlay-manager__last-overlay-operation-label">Collection Mode:  </label>
                    </span>
                    <Dropdown style={{ width: `${globalSizeFactor * 1.5 * 9.5}vh` }} name='CollectionsMode' value={props.tabInfo.properties.generalProperties?.CollectionsMode ?? null} onChange={saveData} options={enumDetails.ECollectionsMode} optionLabel="name" />
                </div>
                <div className="overlay-manager__last-overlay-operation-inputs">
                    <span className="overlay-manager__last-overlay-operation-span">
                        <Button label="Apply" onClick={applyScaleFactor} ></Button>
                        <label className="overlay-manager__last-overlay-operation-label" style={{ textAlign: 'center' }} htmlFor="scaleFactor">Scale Factor:</label>
                    </span>
                    <InputNumber name="scaleFactor" id="scaleFactor" onValueChange={saveData} value={props.tabInfo.properties.generalProperties?.scaleFactor ?? null} />
                </div>
                <div className="overlay-manager__last-overlay-operation-inputs">
                    <span className="overlay-manager__last-overlay-operation-span">
                        <Button label="Apply" onClick={applyMinScale}></Button>
                        <label className="overlay-manager__last-overlay-operation-label" htmlFor="minScale">Equidistant Attach Points Min Scale:</label>
                    </span>
                    <InputNumber name="minScale" id="minScale" onValueChange={saveData} value={props.tabInfo.properties.generalProperties?.minScale ?? null} />
                </div>
                <div className="overlay-manager__last-overlay-operation-inputs">
                    <span className="overlay-manager__last-overlay-operation-span">
                        <Button label="Apply" onClick={applyState}></Button>
                        <label className="overlay-manager__last-overlay-operation-label" htmlFor="state">State:</label>
                    </span>
                    <InputText name="state" id="state" onChange={saveData} value={props.tabInfo.properties.generalProperties?.state.toString().replace(/,/g, " ") ?? ""} />
                </div>
                <div className="overlay-manager__last-overlay-operation-inputs">
                    <span className="overlay-manager__last-overlay-operation-span">
                        <Button label='Apply' onClick={applyScaleSteps} />
                        <label style={{ whiteSpace: "nowrap", width: '100%' }}>Screen Terrain Items Consistency Scale Steps:</label>
                    </span>
                    <InputText name="scaleSteps" onChange={saveData} value={props.tabInfo.properties.generalProperties?.scaleSteps.toString().replace(/,/g, " ") ?? ""} />
                </div>
            </div>
            <div style={{ width: '25%', paddingLeft: `${globalSizeFactor * 0.75}vh` }}>
                <div style={{ paddingBottom: `${globalSizeFactor * 1}vh` }}>Viewports (Clear selection to set for all viewport)</div>
                <ListBox className="overlay-manager__small-list-box-text" listStyle={{ maxHeight: `${globalSizeFactor * 20.6}vh`, minHeight: `${globalSizeFactor * 20.6}vh` }} name="chooseViewportsToOperations" options={listViewportOfOverlayManger} optionLabel="name" value={props.tabInfo.properties.generalProperties?.chooseViewportsToOperations} onChange={saveData}></ListBox>
            </div>
        </Fieldset>
    }
    const getSymbologyFieldset = () => {
        return <Fieldset style={{ height: '100%' }} className="form__column-fieldset" legend="Symbology">
            <div className="overlay-manager__last-overlay-operation-inputs">
                <label>Symbology Standard:</label>
                <Dropdown style={{ width: `${globalSizeFactor * 1.5 * 8}vh` }} id="formatDropDown" name='symbologyStandard' value={props.tabInfo.properties.generalProperties?.symbologyStandard ?? null} onChange={saveData} options={enumDetails.ESymbologyStandard} optionLabel="name" />
            </div>
            <div style={{ padding: `${globalSizeFactor * 1}vh 0vh` }}>
                <div className="form__flex-and-row">
                    <Checkbox name='isShowGeo' inputId="showGeo" onChange={saveData} checked={props.tabInfo.properties.generalProperties?.isShowGeo} />
                    <label htmlFor="showGeo">Show Geo In Metric Proportion</label>
                </div>
                <div className="form__flex-and-row">
                    <Checkbox name='isNonExistentAmplifiers' inputId="nonExistentAmplifiers" onChange={saveData} checked={props.tabInfo.properties.generalProperties?.isNonExistentAmplifiers} />
                    <label htmlFor="nonExistentAmplifiers"> Ignore Non Existent Amplifiers</label>
                </div>
            </div>
            <Button onClick={initializeSymbology} label="Initialize Symbology Standard Support"></Button>
        </Fieldset>
    }
    const getScreenArrangementFieldset = () => {
        return <Fieldset style={{ height: '94%', marginTop: `${globalSizeFactor * 1.5}vh` }} className="form__column-fieldset">
            <div >
                <label>Multi Select (Only for Set function):</label>
                <div className="overlay-manager__om-operation">
                    <ListBox className="overlay-manager__small-list-box-text overlay-manager__margin-right" style={{ maxHeight: `${globalSizeFactor * 9}vh`, minHeight: `${globalSizeFactor * 9}vh`, width: '50%' }} name="selectViewportOfOverlayManager" value={props.tabInfo.properties.generalProperties?.selectViewportOfOverlayManager} onChange={saveData} options={listViewportOfOverlayManger} optionLabel="name"></ListBox>
                    <ListBox className="overlay-manager__small-list-box-text" style={{ maxHeight: `${globalSizeFactor * 9}vh`, minHeight: `${globalSizeFactor * 9}vh`, width: '50%' }} multiple name="selectObjectOfViewport" options={listObjectOfViewport} optionLabel="name" value={props.tabInfo.properties.generalProperties?.selectObjectOfViewport} onChange={saveData} ></ListBox>
                </div>
            </div >
            <div className="overlay-manager__om-operation" style={{ paddingBottom: `${globalSizeFactor * 1}vh` }}>
                <Button style={{ width: '49.8%' }} onClick={setScreen} label="Set Screen Arrangement"></Button>
                <Button style={{ width: '49.8%' }} onClick={cancelScreen} label="Cancel Screen Arrangements"></Button>
            </div>
        </Fieldset >
    }
    //#endregion

    return (
        <div style={{ height: `${globalSizeFactor * 80}vh`, width: '100%' }}>
            <div style={{ display: 'flex', width: '100%', height: `${globalSizeFactor * 13}vh` }}>
                <div style={{ width: '50%' }}>{getGridCoordinateSystemsFieldset()}</div>
                <div style={{ width: '50%' }}> {getOverlayManagerOperationFieldset()}</div>
            </div>
            {getNodesByIdOrNameFieldset()}
            {getObjectSchemesFieldset()}
            {getOverlayOperationsFieldset()}
            <div style={{ display: 'flex', width: '100%', height: `${globalSizeFactor * 20}vh` }}>
                <div style={{ width: '45%' }}> {getSymbologyFieldset()}</div>
                <div style={{ width: '55%' }}>  {getScreenArrangementFieldset()}</div>
            </div>
            <Messages />

            {VDLockscheme && ShowLockscheme()}
            <ConfirmDialog
                rejectLabel={props.tabInfo.properties.generalProperties?.isConfirmDialogFooter ? 'Cancel' : undefined}
                acceptLabel={props.tabInfo.properties.generalProperties?.isConfirmDialogFooter ? 'OK' : undefined}
                reject={props.tabInfo.properties.generalProperties?.isConfirmDialogFooter ? () => { } : undefined}
                accept={props.tabInfo.properties.generalProperties?.isConfirmDialogFooter ? props.tabInfo.properties.generalProperties?.continueSaveFunction : undefined}
                contentClassName={props.tabInfo.properties.generalProperties?.isConfirmDialogFooter ? '' : 'form__confirm-dialog-content'}
                message={props.tabInfo.properties.generalProperties?.confirmDialogMessage}
                header={props.tabInfo.properties.generalProperties?.confirmDialogHeader}
                footer={props.tabInfo.properties.generalProperties?.isConfirmDialogFooter ? undefined : <div></div>}
                visible={props.tabInfo.properties.generalProperties?.confirmDialogVisible}
                onHide={e => {
                    props.tabInfo.setPropertiesCallback("generalProperties", "confirmDialogVisible", false);
                }}
            />
        </div>
    );
}
