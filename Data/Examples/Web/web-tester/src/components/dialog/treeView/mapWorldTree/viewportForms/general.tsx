// External libraries
import { useState, useEffect, ReactElement } from 'react';
import { useDispatch, useSelector } from 'react-redux';

// UI/component libraries
import { InputText } from "primereact/inputtext";
import { Fieldset } from "primereact/fieldset";
import { Checkbox } from "primereact/checkbox";
import { InputNumber } from "primereact/inputnumber";
import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import { Dropdown } from "primereact/dropdown";

// Project-specific imports
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import './styles/globalMapVpList.css';
import { MapViewportFormTabInfo } from "./mapViewportForm";
import LocalMapFootprintItem from "./localMapFootprintItem/localMapFootprintItem";
import SpatialQueriesForm from "./spatialQueries/spatialQueriesForm";
import SpatialQueriesFooter from "./spatialQueries/spatialQueriesFooter";
import GlobalMapViewportList, { globalMapActionType } from "./globalMapViewportList";
import '../viewportForms/spatialQueries/styles/spatialQueries.css';
import { Properties } from "../../../dialog";
import GridCoordinateSystemDataset from "../../../shared/ControlsForMapcoreObjects/coordinateSystemCtrl/gridCoordinateSystemDataset";
import ColorPickerCtrl from "../../../../shared/colorPicker";
import TreeNodeModel from '../../../../shared/models/map-tree-node.model';
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import { AppState } from "../../../../../redux/combineReducer"; import RectFromMapCtrl from "../../../shared/RectFromMapCtrl";
import dialogStateService from "../../../../../services/dialogStateService";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { setSelectedNodeInTree, setTypeMapWorldDialogSecond } from "../../../../../redux/MapWorldTree/mapWorldTreeActions";

export class GeneralPropertiesState implements Properties {
    // Map Params
    oneBitAlphaMode: number;
    gridVisibility: boolean;
    gridAboveVectorLayers: boolean;
    transparencyOrderingMode: boolean;
    fullScreenMode: boolean;
    spatialPartitionNumCacheNodes: number;

    // Post Process
    postProcess: string;

    // Background Color
    backgroundColor: MapCore.SMcBColor;

    // Scan
    // showCompletelyInsideOnly: boolean;
    // SQParams: MapCore.IMcSpatialQueries.SQueryParams;

    // Global Map
    globalMapAutoCenterMode: boolean;

    // Height Lines
    heightLinesVisibility: boolean;


    // Brightness
    stageBrightness: { imageProcessingStage: MapCore.IMcMapViewport.EImageProcessingStage, brightness: number }[];

    static getDefault(p: any): GeneralPropertiesState {
        let { selectedNodeInTree } = p;
        let viewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;

        let puViewportDimensionWidth: any = {}, puViewportDimensionHeight: any = {};
        viewport.GetViewportSize(puViewportDimensionWidth, puViewportDimensionHeight);

        let brightness: { imageProcessingStage: MapCore.IMcMapViewport.EImageProcessingStage, brightness: number }[] = []
        getEnumDetailsList(MapCore.IMcMapViewport.EImageProcessingStage).map(imageProcessingStage =>
            brightness.push({
                imageProcessingStage: imageProcessingStage.theEnum,
                brightness: viewport.GetBrightness(imageProcessingStage.theEnum)
            })
        )

        return {
            // Map Params
            oneBitAlphaMode: viewport.GetOneBitAlphaMode(),
            gridVisibility: viewport.GetGridVisibility(),
            gridAboveVectorLayers: viewport.GetGridAboveVectorLayers(),
            transparencyOrderingMode: viewport.GetTransparencyOrderingMode(),
            fullScreenMode: viewport.GetFullScreenMode(),
            spatialPartitionNumCacheNodes: viewport.GetSpatialPartitionNumCacheNodes(),
            // Post Process
            postProcess: "",
            // Background Color
            backgroundColor: viewport.GetBackgroundColor(),
            // Scan
            // showCompletelyInsideOnly: false, // generalService?
            // SQParams: null,
            // Global Map
            globalMapAutoCenterMode: viewport.GetGlobalMapAutoCenterMode(),
            // Height Lines
            heightLinesVisibility: viewport.GetHeightLinesVisibility(),
            // Brightness
            stageBrightness: [...brightness],
        }
    }
};

export class GeneralProperties extends GeneralPropertiesState {
    // Map Params
    /* immutable values (display only) */
    mapType: MapCore.IMcMapCamera.EMapType;
    viewportID: number;
    windowHandle: HTMLCanvasElement;
    viewportDimensionWidth: number;
    viewportDimensionHeight: number;
    // World Bounding Box - display only
    worldRectangleBox: MapCore.SMcBox;
    showRectangle: boolean;
    // Brightness
    imageProcessingStage: MapCore.IMcMapViewport.EImageProcessingStage;
    // Get Terrain By ID
    terrainID: number;
    terrainLink: any;
    //Debug Options
    debugOptionsKey: number;
    debugOptionsValue: number;
    incrementKey: number;
    // Viewprt Spatial Queries
    spatialQueriesFooter: any;

    static getDefault(p: any): GeneralProperties {
        let { selectedNodeInTree } = p;
        let viewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;

        let puViewportDimensionWidth: any = {}, puViewportDimensionHeight: any = {};
        viewport.GetViewportSize(puViewportDimensionWidth, puViewportDimensionHeight);

        let brightness: { imageProcessingStage: MapCore.IMcMapViewport.EImageProcessingStage, brightness: number }[] = []
        getEnumDetailsList(MapCore.IMcMapViewport.EImageProcessingStage.EIPS_ALL).map(imageProcessingStage =>
            brightness.push({
                imageProcessingStage: imageProcessingStage.theEnum,
                brightness: viewport.GetBrightness(imageProcessingStage.theEnum)
            })
        )

        let terrainBoundingBox = null;
        runMapCoreSafely(() => { terrainBoundingBox = viewport.GetTerrainsBoundingBox(); }, 'MapViewportForm/GeneralProperties.getDefault => IMcSpatialQueries.GetTerrainsBoundingBox', true)
        let stateDefaults = super.getDefault(p);
        let defaults: GeneralProperties = {
            ...stateDefaults,
            // Map Params
            /* immutable values (display only) */
            mapType: viewport.GetMapType(),
            viewportID: viewport.GetViewportID(),
            windowHandle: viewport.GetWindowHandle(),
            viewportDimensionWidth: puViewportDimensionWidth.Value,
            viewportDimensionHeight: puViewportDimensionHeight.Value,
            // World Bounding Box
            worldRectangleBox: terrainBoundingBox,
            showRectangle: false,
            // Brightness
            imageProcessingStage: MapCore.IMcMapViewport.EImageProcessingStage.EIPS_ALL,
            // Get Terrain By ID
            terrainID: null,
            terrainLink: "",
            //Debug Options
            debugOptionsKey: 0,
            debugOptionsValue: 0,
            incrementKey: 0,
            // Viewprt Spatial Queries
            spatialQueriesFooter: <div></div>,
        }

        return defaults;
    }
};

export default function General(props: { tabInfo: MapViewportFormTabInfo }) {
    const dispatch = useDispatch();
    let treeRedux = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let viewport: MapCore.IMcMapViewport = props.tabInfo.currentViewport.nodeMcContent;

    let [openSecondDialog, setOpenSecondDialog] = useState({
        // scanSQParams: false,
        // spatialQueries: false,
        globalMapViewportList: false,
        localMapFootprintItem: false,
    })

    // Global Map - direct actions (not considered for state from external tab... for undo, move to GeneralProperties)
    let [globalMapAction, setGlobalMapAction] = useState<globalMapActionType>(null)

    let enumDetails = {
        EMapType: getEnumDetailsList(MapCore.IMcMapCamera.EMapType),
        EImageProcessingStage: getEnumDetailsList(MapCore.IMcMapViewport.EImageProcessingStage)
    }
    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        generalProperties: false,
        currentViewport: false,
    })

    //UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            // initialize first time the tab is loaded only
            if (!props.tabInfo.properties.generalProperties) {
                props.tabInfo.setInitialStatePropertiesCallback("generalProperties", null, GeneralPropertiesState.getDefault({ selectedNodeInTree }));
                props.tabInfo.setPropertiesCallback("generalProperties", null, GeneralProperties.getDefault({ selectedNodeInTree }));
            }
        }, 'MapViewportForm/General.useEffect');
    }, [])

    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.generalProperties) {
                props.tabInfo.setApplyCallBack("General", applyAll);
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, generalProperties: true })
            }
        }, 'MapViewportForm/General.useEffect[props.tabInfo.properties]');
    }, [props.tabInfo.properties])

    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.currentViewport) {
                // set to default values
                props.tabInfo.setInitialStatePropertiesCallback("generalProperties", null, GeneralPropertiesState.getDefault({ selectedNodeInTree }));
                props.tabInfo.setPropertiesCallback("generalProperties", null, GeneralProperties.getDefault({ selectedNodeInTree }));
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, currentViewport: true })
            }
        }, 'MapViewportForm/General.useEffect => currentViewport');
    }, [props.tabInfo.currentViewport])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            let class_name: string = event.originalEvent?.currentTarget?.className;
            let value = event.target.type === "checkbox" ? event.target.checked : event.target.value;
            if (class_name?.includes("p-dropdown-item")) {
                value = event.target.value.theEnum;
            }
            props.tabInfo.setPropertiesCallback("generalProperties", event.target.name, value);
            if (event.target.name in GeneralPropertiesState.getDefault({ selectedNodeInTree })) {
                props.tabInfo.setCurrStatePropertiesCallback("generalProperties", event.target.name, value)
            }
        }, "MapViewportForm/General.saveData => onChange")
    }
    const setColor = (event: { value: any; target: any }) => {
        runCodeSafely(() => {
            let color = { ...props.tabInfo.properties.generalProperties[event.target.name], r: event.value.r, g: event.value.g, b: event.value.b }
            props.tabInfo.setPropertiesCallback("generalProperties", event.target.name, color);
        }, "MapViewportForm/General.setColor => onChange");
    }
    const applyAll = () => {
        // Map Params
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                viewport.SetOneBitAlphaMode(props.tabInfo.properties.generalProperties?.oneBitAlphaMode)
            }, 'MapViewportForm/General.applyAll => IMcMapViewport.SetOneBitAlphaMode', true);
            runMapCoreSafely(() => {
                viewport.SetGridVisibility(props.tabInfo.properties.generalProperties?.gridVisibility)
            }, 'MapViewportForm/General.applyAll => IMcMapViewport.SetGridVisibility', true);
            runMapCoreSafely(() => {
                viewport.SetGridAboveVectorLayers(props.tabInfo.properties.generalProperties?.gridAboveVectorLayers)
            }, 'MapViewportForm/General.applyAll => IMcMapViewport.SetGridAboveVectorLayers', true);
            runMapCoreSafely(() => {
                viewport.SetTransparencyOrderingMode(props.tabInfo.properties.generalProperties?.transparencyOrderingMode)
            }, 'MapViewportForm/General.applyAll => IMcMapViewport.SetTransparencyOrderingMode', true);
            runMapCoreSafely(() => {
                viewport.SetFullScreenMode(props.tabInfo.properties.generalProperties?.fullScreenMode)
            }, 'MapViewportForm/General.applyAll => IMcMapViewport.SetFullScreenMode', true);
            runMapCoreSafely(() => {
                viewport.SetSpatialPartitionNumCacheNodes(props.tabInfo.properties.generalProperties?.spatialPartitionNumCacheNodes)
            }, 'MapViewportForm/General.applyAll => IMcMapViewport.SetSpatialPartitionNumCacheNodes', true);

            // Post Process - separate
            // Background Color
            runMapCoreSafely(() => {
                viewport.SetBackgroundColor(props.tabInfo.properties.generalProperties?.backgroundColor)
            }, 'MapViewportForm/General.applyAll => IMcMapViewport.SetBackgroundColor', true);

            // Scan
            // showCompletelyInsideOnly: boolean;
            // SQParams: MapCore.IMcSpatialQueries.SQueryParams;
            // runMapCoreSafely(() => {
            //     viewport.ScanInGeometry({}, props.tabInfo.properties.generalProperties?.showCompletelyInsideOnly, props.tabInfo.properties.generalProperties?.SQParams)
            // }, 'MapViewportForm/General.applyAll.ScanInGeometry', true);
            //?????????????????????????????????????????

            // Global Map
            runMapCoreSafely(() => {
                viewport.SetGlobalMapAutoCenterMode(props.tabInfo.properties.generalProperties?.globalMapAutoCenterMode)
            }, 'MapViewportForm/General.applyAll => IMcGlobalMap.SetGlobalMapAutoCenterMode', true);

            // Height Lines
            // heightLinesVisibility: boolean;
            runMapCoreSafely(() => {
                viewport.SetHeightLinesVisibility(props.tabInfo.properties.generalProperties?.heightLinesVisibility)
            }, 'MapViewportForm/General.applyAll => IMcMapViewport.SetHeightLinesVisibility', true);

            // // Brightness
            // stageBrightness: {imageProcessingStage: MapCore.IMcMapViewport.EImageProcessingStage, brightness: number}[];
            props.tabInfo.properties.generalProperties?.stageBrightness.map(sb => {
                runMapCoreSafely(() => {
                    viewport.SetBrightness(sb.imageProcessingStage, sb.brightness);
                }, 'MapViewportForm/General.applyAll => IMcMapViewport.SetBrightness', true);
            })

            dialogStateService.applyDialogState([
                "generalProperties.oneBitAlphaMode",
                "generalProperties.gridVisibility",
                "generalProperties.gridAboveVectorLayers",
                "generalProperties.transparencyOrderingMode",
                "generalProperties.fullScreenMode",
                "generalProperties.spatialPartitionNumCacheNodes",
                "generalProperties.backgroundColor",
                "generalProperties.showCompletelyInsideOnly",
                "generalProperties.SQParams",
                "generalProperties.globalMapAutoCenterMode",
                "generalProperties.heightLinesVisibility",
                "generalProperties.stageBrightness",
            ])
        }, 'MapViewportForm/General.applyAll');
    }

    const getGridCoordinateSystem = () => {
        let gridCoordinateSystem: MapCore.IMcGridCoordinateSystem = null;
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                gridCoordinateSystem = viewport.GetCoordinateSystem()
            }, 'MapViewportForm/General.getGridCoordinateSystem => IMcSpatialQueries.GetCoordinateSystem', true)
        }, 'MapViewportForm/General.getGridCoordinateSystem')
        return gridCoordinateSystem;
    }
    //#region  Handle Functions
    const addPostProcess = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                viewport.AddPostProcess(props.tabInfo.properties.generalProperties?.postProcess);
            }, "MapViewportForm/General => IMcMapViewport.AddPostProcess", true)
        }, 'MapViewportForm/General.addPostProcess')
    }
    const removePostProcess = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                viewport.RemovePostProcess(props.tabInfo.properties.generalProperties?.postProcess);
            }, "MapViewportForm/General => IMcMapViewport.RemovePostProcess", true)
        }, 'MapViewportForm/General.removePostProcess')
    }
    const getTerrainById = () => {
        runCodeSafely(() => {
            let id: number = props.tabInfo.properties.generalProperties?.terrainID;
            let terrain: MapCore.IMcMapTerrain;
            runMapCoreSafely(() => {
                terrain = viewport.GetTerrainByID(id);
            }, "OverlayManagerGeneralForm.getSchemeById => GetObjectSchemeByID", true)
            let TreeObj: TreeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, terrain) as TreeNodeModel
            props.tabInfo.setPropertiesCallback("generalProperties", "link", { objMC: terrain, TreeObj: TreeObj });
            // dialogStateService.applyDialogState(["generalProperties.idToScheme"])
        }, "MapViewportForm/General.getTerrainById")
    }
    const handleGetDebugOptionsClick = () => {
        runCodeSafely(() => {
            let debugValue: number = null;
            runMapCoreSafely(() => {
                debugValue = viewport.GetDebugOption(props.tabInfo.properties.generalProperties?.debugOptionsKey);
            }, 'MapViewportForm/General.handleGetDebugOptionsClick => IMcSpatialQueries.GetDebugOption', true);
            props.tabInfo.setPropertiesCallback("generalProperties", "debugOptionsValue", debugValue);
        }, "MapViewportForm/General.handleGetDebugOptionsClick")
    }
    const handleSetDebugOptionsClick = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                viewport.SetDebugOption(props.tabInfo.properties.generalProperties?.debugOptionsKey, props.tabInfo.properties.generalProperties?.debugOptionsValue);
            }, 'MapViewportForm/General.handleSetDebugOptionsClick => IMcSpatialQueries.SetDebugOption', true);
        }, "MapViewportForm/General.handleSetDebugOptionsClick")
    }
    const handleSetIncrementKeyClick = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                viewport.IncrementDebugOption(props.tabInfo.properties.generalProperties?.incrementKey);
            }, 'MapViewportForm/General.handleGetDebugOptionsClick => IMcSpatialQueries.IncrementDebugOption', true);
        }, "MapViewportForm/General.handleSetIncrementKeyClick")
    }
    //#endregion

    //#region DOM Functions
    const getGeneralFieldset = () => {
        return <Fieldset className="form__column-fieldset">
            <div className="form__center-aligned-row">
                <label htmlFor='mapType'>Map Type:</label>
                <span id='mapType'>{props.tabInfo.properties.generalProperties?.mapType ?
                    getEnumValueDetails(props.tabInfo.properties.generalProperties?.mapType, enumDetails.EMapType).name :
                    ""} </span>
            </div>
            <div className="form__center-aligned-row">
                <label htmlFor='viewportID'>Viewport ID:</label>
                <span id='viewportID'>{props.tabInfo.properties.generalProperties?.viewportID} </span>
            </div>
            <div className="form__center-aligned-row">
                <label htmlFor='windowHandle'>Window Handle:</label>
                <span id='windowHandle'>{props.tabInfo.properties.generalProperties?.windowHandle.TEXT_NODE} </span>
            </div>
            <div className="form__center-aligned-row">
                <label htmlFor='viewportDimensionWidth'>Viewport Dimension Width:</label>
                <span id='viewportDimensionWidth'>{props.tabInfo.properties.generalProperties?.viewportDimensionWidth} </span>
                <label htmlFor='viewportDimensionHeight'>Height: </label>
                <span id='viewportDimensionHeight'>{props.tabInfo.properties.generalProperties?.viewportDimensionHeight} </span>
            </div>

            <div className="form__center-aligned-row">
                <label htmlFor='OneBitAlphaMode'>One Bit Alpha Mode:</label>
                <InputNumber name='oneBitAlphaMode' id='OneBitAlphaMode' className="form__medium-width-input" value={props.tabInfo.properties.generalProperties?.oneBitAlphaMode ?? null} onValueChange={saveData} />
            </div>
            <div className="form__center-aligned-row">
                <Checkbox name="gridVisibility" inputId="gridVisibility" checked={props.tabInfo.properties.generalProperties?.gridVisibility} onChange={saveData} />
                <label htmlFor="gridVisibility">Grid Visibility</label>
            </div>
            <div className="form__center-aligned-row">
                <Checkbox name="gridAboveVectorLayers" inputId="gridAboveVectorLayers" checked={props.tabInfo.properties.generalProperties?.gridAboveVectorLayers} onChange={saveData} />
                <label htmlFor="gridAboveVectorLayers">Grid Above Vector Layers</label>
            </div>
            <div className="form__center-aligned-row">
                <Checkbox name="transparencyOrderingMode" inputId="transparencyOrderingMode" checked={props.tabInfo.properties.generalProperties?.transparencyOrderingMode} onChange={saveData} />
                <label htmlFor="transparencyOrderingMode">Transparency Ordering Mode</label>
            </div>
            <div className="form__center-aligned-row">
                <Checkbox name="fullScreenMode" inputId="fullScreenMode" checked={props.tabInfo.properties.generalProperties?.fullScreenMode} onChange={saveData} />
                <label htmlFor="fullScreenMode">Full Screen Mode</label>
            </div>
            <div className="form__center-aligned-row">
                <label htmlFor='spatialPartitionNumCacheNodes'>Spatial Partition Num Cache Nodes:</label>
                <InputNumber className="form__narrow-input" id='spatialPartitionNumCacheNodes' value={props.tabInfo.properties.generalProperties?.spatialPartitionNumCacheNodes ?? null} name='spatialPartitionNumCacheNodes' onValueChange={saveData} />
            </div>
        </Fieldset>
    }
    const getBrightnessFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend="Brightness">
            <div className="form__center-aligned-row">
                <label htmlFor='imageProcessingStage'>Image Processing Stage:</label>
                <Dropdown className="form__dropdown-input" id='imageProcessingStage' name='imageProcessingStage' value={props.tabInfo.properties.generalProperties ? getEnumValueDetails(props.tabInfo.properties.generalProperties.imageProcessingStage, enumDetails.EImageProcessingStage) : null} onChange={saveData} options={enumDetails.EImageProcessingStage} optionLabel="name" />
            </div>
            <div className="form__center-aligned-row">
                <label htmlFor='stageBrightness'>Brightness (No between -1 to 1):</label>
                <InputNumber minFractionDigits={0} maxFractionDigits={5} className="form__medium-width-input" name='stageBrightness' id='stageBrightness'
                    value={
                        props.tabInfo.properties.generalProperties?.stageBrightness
                            .filter(sb => sb.imageProcessingStage === props.tabInfo.properties.generalProperties?.imageProcessingStage)
                            .map(sb => sb.brightness)[0] || null
                    }
                    onValueChange={
                        (e) => {
                            let updatedStageBrightness = [...props.tabInfo.properties.generalProperties?.stageBrightness].map(sb => {
                                if (sb.imageProcessingStage === props.tabInfo.properties.generalProperties?.imageProcessingStage)
                                    return { imageProcessingStage: sb.imageProcessingStage, brightness: e.target.value };
                                return sb;
                            })
                            props.tabInfo.setPropertiesCallback("generalProperties", "stageBrightness", updatedStageBrightness);
                            props.tabInfo.setCurrStatePropertiesCallback("generalProperties", "stageBrightness", updatedStageBrightness);
                        }
                    } />
            </div>
        </Fieldset >
    }
    const getGlobalMapFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend="Global Map">
            <div><Button onClick={() => {
                setGlobalMapAction("Register")
                setOpenSecondDialog({ ...openSecondDialog, globalMapViewportList: true })
            }}>Register Local Map</Button></div>
            <div><Button onClick={() => {
                setGlobalMapAction("UnRegister")
                setOpenSecondDialog({ ...openSecondDialog, globalMapViewportList: true })
            }}>UnRegister Local Map</Button></div>
            <div><Button onClick={() => {
                setGlobalMapAction("SetActiveLocalMap")
                setOpenSecondDialog({ ...openSecondDialog, globalMapViewportList: true })
            }}>Set Active Local Map</Button></div>
            <div className="form__center-aligned-row">
                <Checkbox name="globalMapAutoCenterMode" inputId="globalMapAutoCenterMode" onChange={saveData} checked={props.tabInfo.properties.generalProperties?.globalMapAutoCenterMode} />
                <label htmlFor="globalMapAutoCenterMode">Global Map Auto Center Mode</label>
            </div>
            <div className="form__center-aligned-row">
                <label>Set Local Map Footprint Item:</label>
                <Button onClick={() => {
                    setOpenSecondDialog({ ...openSecondDialog, localMapFootprintItem: true })
                }} label="Line List" />
            </div>
            <div className="form__center-aligned-row">
                <label>Set Local Map Footprint Screen Positions:</label>
                <Button onClick={() => {
                    setGlobalMapAction("ScreenPositions")
                    setOpenSecondDialog({ ...openSecondDialog, globalMapViewportList: true })
                }} label="OK" />
            </div>
        </Fieldset>
    }
    const getDebugOptionsFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend="Debug Options">
            <div className="form__flex-and-row-between">
                <div style={{ width: '45%' }} className="form__flex-and-row-between">
                    <label htmlFor="debugOptionsKey">Key: </label>
                    <InputNumber className='form__narrow-input' id='debugOptionsKey' value={props.tabInfo.properties.generalProperties?.debugOptionsKey ?? null} name="debugOptionsKey" onValueChange={saveData} />
                </div>
                <div style={{ width: '45%' }} className="form__flex-and-row-between">
                    <label htmlFor="debugOptionsValue">Value: </label>
                    <InputNumber className='form__narrow-input' id='debugOptionsValue' value={props.tabInfo.properties.generalProperties?.debugOptionsValue ?? null} name="debugOptionsValue" onValueChange={saveData} />
                </div>
            </div>
            <div className="form__apply-buttons-container">
                <Button label='Get' onClick={handleGetDebugOptionsClick} />
                <Button label='Set' onClick={handleSetDebugOptionsClick} />
            </div>
            <div className="form__flex-and-row-between">
                <div style={{ width: '45%' }} className="form__flex-and-row-between">
                    <label htmlFor="incrementKey">Key: </label>
                    <InputNumber className='form__narrow-input' id='incrementKey' value={props.tabInfo.properties.generalProperties?.incrementKey ?? null} name="incrementKey" onValueChange={saveData} />
                </div>
                <Button label='Set' onClick={handleSetIncrementKeyClick} />
            </div>
        </Fieldset>
    }
    const getBoundingBoxFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend="World Bounding Box">
            <RectFromMapCtrl info={{
                rectangleBox: props.tabInfo.properties.generalProperties?.worldRectangleBox,
                showRectangle: props.tabInfo.properties.generalProperties?.showRectangle,
                setProperty: (name: string, value: any) => {
                    props.tabInfo.setPropertiesCallback("generalProperties", name, value);
                },
                readOnly: true,
                rectCoordSystem: MapCore.EMcPointCoordSystem.EPCS_WORLD,
                currViewport: viewport,
            }} />
        </Fieldset>
    }
    //#endregion

    return (
        <div className="form__column-container">
            <div className="form__row-container">
                <div className="form__column-container">
                    {getGeneralFieldset()}
                    <Fieldset className="form__column-fieldset" legend="Post Process">
                        <div className="form__center-aligned-row">
                            <label htmlFor='postProcess'>Post Process:</label>
                            <InputText style={{ width: '100%' }} id='postProcess' value={props.tabInfo.properties.generalProperties?.postProcess ?? ""} name='postProcess' onChange={saveData} />
                        </div>
                        <div className="form__apply-buttons-container" >
                            <Button onClick={addPostProcess}>Add</Button>
                            <Button onClick={removePostProcess}>Remove</Button>
                        </div>
                    </Fieldset>
                    <Fieldset className="form__column-fieldset" legend="Background Color">
                        <div className="form__center-aligned-row">
                            <label htmlFor='backgroundColor'>Background Color:</label>
                            <ColorPickerCtrl id='backgroundColor' name='backgroundColor' value={props.tabInfo.properties.generalProperties?.backgroundColor} onChange={setColor} />
                        </div>
                    </Fieldset>
                    {getDebugOptionsFieldset()}
                    {/* <Fieldset className="form__column-fieldset" legend="Scan">
                    <div className="form__center-aligned-row">
                        <Checkbox name="showCompletelyInsideOnly" inputId="showCompletelyInsideOnly" checked={props.tabInfo.properties.generalProperties?.showCompletelyInsideOnly} onChange={saveData}  />
                        <label htmlFor="showCompletelyInsideOnly">Show Completely Inside Only</label>
                    </div>
                    <div><Button onClick={() => { setOpenSecondDialog({...openSecondDialog, scanSQParams: true}) }}>SQParams</Button></div>
               </Fieldset> */}
                </div>
                <div style={{ width: '50%' }} className="form__column-container">
                    <GridCoordinateSystemDataset gridCoordinateSystem={getGridCoordinateSystem()} />
                    <Fieldset className="form__column-fieldset" legend="Height Lines">
                        <div className="form__center-aligned-row">
                            <Checkbox name="heightLinesVisibility" inputId="heightLinesVisibility" checked={props.tabInfo.properties.generalProperties?.heightLinesVisibility} onChange={saveData} />
                            <label htmlFor="heightLinesVisibility">Height Lines Visibility</label>
                        </div>
                    </Fieldset>
                    {getBrightnessFieldset()}
                    <Fieldset className="form__column-fieldset" legend="Get Terrain By ID">
                        <div className="form__center-aligned-row">
                            <label>ID:</label>
                            <span>
                                <InputNumber className="overlay-manager__get-node-input-number" value={props.tabInfo.properties.generalProperties?.terrainID ?? null} name="terrainID" onValueChange={saveData} />
                                <Button disabled={!(props.tabInfo.properties.generalProperties?.terrainID)} onClick={getTerrainById}>Get Terrain</Button>
                            </span>
                        </div>
                        <label onClick={() => { dispatch(setSelectedNodeInTree(props.tabInfo.properties.generalProperties?.terrainLink.TreeObj)) }} style={{ marginTop: `${globalSizeFactor * 1.5}vh`, color: "blue" }}>{props.tabInfo.properties.generalProperties?.terrainLink?.TreeObj?.label ?? ""}</label>
                    </Fieldset>
                    {getGlobalMapFieldset()}
                </div >
            </div >
            {getBoundingBoxFieldset()}
            <Button className="form__full-div-button" onClick={() => {
                dispatch(setTypeMapWorldDialogSecond({
                    secondDialogHeader: "Spatial Queries",
                    secondDialogComponent: <SpatialQueriesForm viewport={props.tabInfo.currentViewport.nodeMcContent} />,
                    footerComponent: <SpatialQueriesFooter />,
                    modal: false,
                }))
            }} label='Viewport Spatial Queries' />

            {/* <Dialog header="Scan SQParams" visible={openSecondDialog.scanSQParams} onHide={() => { setOpenSecondDialog({...openSecondDialog, scanSQParams: false}) }}>
                <ScanSQParams></ScanSQParams>
                <Footer onOk={() => { setOpenSecondDialog({...openSecondDialog, scanSQParams: false}) }} label="OK"></Footer>
            </Dialog> */}
            <Dialog className="scroll-dialog-global-map-vp-list" header="Global Map Viewport List" visible={openSecondDialog.globalMapViewportList} onHide={() => {
                setOpenSecondDialog({ ...openSecondDialog, globalMapViewportList: false })
            }}>
                <GlobalMapViewportList viewport={viewport} action={globalMapAction} closeDialog={() => { setOpenSecondDialog({ ...openSecondDialog, globalMapViewportList: false }) }} />
            </Dialog>
            <Dialog className="scroll-dialog-local-map-footprint" header="Local Map Footprint Item" visible={openSecondDialog.localMapFootprintItem} onHide={() => { setOpenSecondDialog({ ...openSecondDialog, localMapFootprintItem: false }) }}>
                <LocalMapFootprintItem viewport={viewport} closeDialog={() => setOpenSecondDialog({ ...openSecondDialog, localMapFootprintItem: false })} />
            </Dialog>
        </div >
    )
}
