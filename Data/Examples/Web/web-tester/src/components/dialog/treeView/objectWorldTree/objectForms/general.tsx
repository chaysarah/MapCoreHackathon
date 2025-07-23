// External libraries
import { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';

// UI/component libraries
import { Fieldset } from "primereact/fieldset";
import { ListBox } from "primereact/listbox";
import { InputNumber } from "primereact/inputnumber";
import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { InputTextarea } from "primereact/inputtextarea";
import { Checkbox } from "primereact/checkbox";

// Project-specific imports
import { MapCoreData } from 'mapcore-lib';
import { ObjectFormTabInfo } from "./objectForm";
import ConditionalSelectorCtrl, { ConditionalSelectorCtrlProperties, ConditionalSelectorCtrlInfo } from "../ctrls/conditionalSelectorCtrl";
import UserDataCtrl from "../ctrls/userDataCtrl";
import GeneralViewportForm, { GeneralViewportFormInfo, GeneralViewportFormProperties, GeneralViewportFormPropertiesState } from "../shared/generalViewportForm";
import PropertiesIDList from '../objectSchemes/propertiesIDList/propertiesIDList';
import { Properties } from "../../../dialog";
import TreeNodeModel from "../../../../shared/models/tree-node.model";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { setSelectedNodeInTree, setTypeObjectWorldDialogSecond } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import { AppState } from "../../../../../redux/combineReducer";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import generalService from "../../../../../services/general.service";
import dialogStateService from "../../../../../services/dialogStateService";


export class GeneralPropertiesState implements Properties {
    generalViewportFormPropertiesState: GeneralViewportFormPropertiesState;
    objectID: string;
    description: string;
    name: string;
    isIgnoreViewport: boolean;
    isSuppress: boolean;
    selectedTraversability: any;
    userData: Uint8Array;

    static getDefault(p: any): GeneralPropertiesState {
        let { props, treeRedux, selectedNodeInTree } = p;

        return {
            generalViewportFormPropertiesState: GeneralViewportFormPropertiesState.getDefault({ treeRedux, selectedNodeInTree }),
            objectID: objectWorldTreeService.getObjIDHeader(props.tabInfo.currentObject.nodeMcContent.GetID()),
            description: getNameAndDescription('description', props.tabInfo.currentObject),
            name: getNameAndDescription('name', props.tabInfo.currentObject),
            isIgnoreViewport: props.tabInfo.currentObject.nodeMcContent.GetIgnoreViewportVisibilityMaxScale(),
            isSuppress: props.tabInfo.currentObject.nodeMcContent.GetSuppressQueryPresentationMapTilesWebRequests(),
            selectedTraversability: null,
            userData: selectedNodeInTree.nodeMcContent.GetUserData() ? selectedNodeInTree.nodeMcContent.GetUserData()?.getUserData() : '',
        }
    }
}
export class GeneralProperties extends GeneralPropertiesState {
    generalViewportFormProperties: GeneralViewportFormProperties;
    idToScheme: number | null;
    nameToScheme: string;
    foundedSchemeNode: any;
    numberOfLocations: number;
    isAttachedToAnotherObject: boolean;
    allTraversability: any;
    conditionalSelectorCtrlProperties: ConditionalSelectorCtrlProperties;

    static getDefault(p: any): GeneralProperties {
        let { props, treeRedux, selectedNodeInTree } = p;

        let stateDefaults = super.getDefault(p);
        let defaults: GeneralProperties = {
            ...stateDefaults,
            generalViewportFormProperties: GeneralViewportFormProperties.getDefault({ props, treeRedux, selectedNodeInTree }),
            idToScheme: null,
            nameToScheme: "",
            foundedSchemeNode: null,
            numberOfLocations: props.tabInfo.currentObject.nodeMcContent.GetNumLocations(),
            isAttachedToAnotherObject: props.tabInfo.currentObject.nodeMcContent.IsAttachedToAnotherObject(),
            allTraversability: getTraversabilityList(treeRedux, props.tabInfo.currentObject),

            conditionalSelectorCtrlProperties: ConditionalSelectorCtrlProperties.getDefault(p),
        }
        return defaults;
    }
};

const getNameAndDescription = (nameOrDesc: string, currentObject: TreeNodeModel): string => {
    let name: { Value?: string } = {};
    let description: { Value?: string } = {};
    let mcCurrentObject = currentObject.nodeMcContent as MapCore.IMcObject;
    mcCurrentObject.GetNameAndDescription(name, description);
    return nameOrDesc == 'name' ? name.Value : description.Value;
}

const getTraversabilityList = (treeRedux: TreeNodeModel, currentObject: TreeNodeModel) => {
    let traversabilityArr: { traversability: MapCore.IMcTraversabilityMapLayer, label: string }[] = [];
    runCodeSafely(() => {
        let mcCurrentObject = currentObject.nodeMcContent as MapCore.IMcObject;
        let traversability: MapCore.IMcTraversabilityMapLayer = mcCurrentObject.GetTraversabilityPresentationMapLayer();
        let overlay = objectWorldTreeService.getParentByChildKey(treeRedux, currentObject.key) as TreeNodeModel
        let viewports = objectWorldTreeService.getOMMCViewportsByOverlay(treeRedux, overlay)
        viewports.forEach((vp) => {
            let terrains: MapCore.IMcMapTerrain[] = vp.viewport.GetTerrains();
            terrains.forEach((terrain) => {
                let layers: MapCore.IMcMapLayer[] = terrain.GetLayers();
                layers.forEach((layer) => {
                    if (layer.GetLayerType() == (MapCore.IMcNativeServerTraversabilityMapLayer.LAYER_TYPE || MapCore.IMcNativeTraversabilityMapLayer.LAYER_TYPE) && layer == traversability) {
                        let traverseLayer = layer as MapCore.IMcTraversabilityMapLayer;
                        traversabilityArr = [...traversabilityArr, { traversability: traverseLayer, label: generalService.getObjectName(traverseLayer, 'IMcTraversabilityMapLayer') }];
                    }
                });
            });
        });
    }, 'ObjectForms/General => getTraversabilityList');
    return traversabilityArr;
}

// export default function General(props: {properties: ObjectFormProperties, setPropertiesCallback: (key: string, value: any) => void, currentObject: TreeNodeModel, setApplyCallBack: (tabName: string, Callback: () => void) => void}) {
export default function General(props: { tabInfo: ObjectFormTabInfo }) {
    const dispatch = useDispatch();
    // let currentObject = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let selectedNodeInTree = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);

    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        generalProperties: false,
        currentObject: false,
    })
    // const [userDataApplyFunc, setUserDataApplyFunc] = useState<any>(()=>{})
    // const [conditionalSelectorApplyFunc, setConditionalSelectorApplyFunc] = useState<any>(()=>{})

    useEffect(() => {
        runCodeSafely(() => {
            // initialize first time the tab is loaded only
            if (!props.tabInfo.properties.generalProperties) {
                props.tabInfo.setInitialStatePropertiesCallback("generalProperties", null, GeneralPropertiesState.getDefault({ props, treeRedux, selectedNodeInTree }));
                props.tabInfo.setPropertiesCallback("generalProperties", null, GeneralProperties.getDefault({ props, treeRedux, selectedNodeInTree }));
            }
        }, 'ObjectForms/General.useEffect');
    }, [])

    useEffect(() => {
        runCodeSafely(() => {
            // if (isMountedUseEffect.generalProperties) {
            // dispatch(setHandleApplyFunc(applyAll));
            props.tabInfo.setApplyCallBack("General", applyAll);
            // }
            // else {
            //     setIsMountedUseEffect({ ...isMountedUseEffect, generalProperties: true })
            // }
        }, 'ObjectForms/General.useEffect => properties');
    }, [props.tabInfo.properties])

    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.currentObject) {
                props.tabInfo.setInitialStatePropertiesCallback("generalProperties", null, GeneralPropertiesState.getDefault({ props, treeRedux, selectedNodeInTree }));
                props.tabInfo.setPropertiesCallback("generalProperties", null, GeneralProperties.getDefault({ props, treeRedux, selectedNodeInTree }));
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, currentObject: true })
            }
        }, "ObjectForms/General.useEffect => props.tabInfo.currentObject")
    }, [props.tabInfo.currentObject])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("generalProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in GeneralPropertiesState.getDefault({ props, treeRedux, selectedNodeInTree })) {
                props.tabInfo.setCurrStatePropertiesCallback("generalProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value)
            }
        }, "ObjectForms/General.saveData => onChange")
    }
    //Handle functions
    const applyAll = () => {
        runCodeSafely(() => {
            // userDataApplyFunc(); //props.tabInfo.properties.generalProperties?.userDataApplyFunc();
            // conditionalSelectorApplyFunc(); //props.tabInfo.properties.generalProperties?.conditionalSelectorApplyFunc();
            let mcCurrentObject = props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
            mcCurrentObject.SetID(objectWorldTreeService.getObjIDValue(props.tabInfo.properties.generalProperties?.objectID ?? null));
            mcCurrentObject.SetNameAndDescription(props.tabInfo.properties.generalProperties?.name, props.tabInfo.properties.generalProperties?.description);
            mcCurrentObject.SetSuppressQueryPresentationMapTilesWebRequests(props.tabInfo.properties.generalProperties?.isSuppress);
            mcCurrentObject.SetIgnoreViewportVisibilityMaxScale(props.tabInfo.properties.generalProperties?.isIgnoreViewport);
            let traversability = props.tabInfo.properties.generalProperties?.selectedTraversability ? props.tabInfo.properties.generalProperties?.selectedTraversability?.traversability : null;
            mcCurrentObject.SetTraversabilityPresentationMapLayer(traversability);
            // Set userData
            let testerUserData = new MapCoreData.iMcUserDataClass();
            testerUserData.setUserData(props.tabInfo.properties.generalProperties?.userData);
            runMapCoreSafely(() => { mcCurrentObject.SetUserData(testerUserData); }, 'OverlayForms/General.applyAll => IMcObject.SetUserData', true)

            dialogStateService.applyDialogState([
                "generalProperties.objectID",
                "generalProperties.name",
                "generalProperties.userData",
                "generalProperties.description",
                "generalProperties.isSuppress",
                "generalProperties.isIgnoreViewport",
                "generalProperties.selectedTraversability",
            ]);

            // TODO seConditionalSelector
        }, 'ObjectForms/General.applyAll');
    }
    const handleLinkClick = () => {
        dispatch(setSelectedNodeInTree(props.tabInfo.properties.generalProperties?.foundedSchemeNode))
    }
    const handleGetSchemeByIdClick = () => {
        runCodeSafely(() => {
            let mcObj = props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
            let schemeNode = mcObj.GetNodeByID(props.tabInfo.properties.generalProperties?.idToScheme);
            if (schemeNode) {
                let treeNode = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, schemeNode);
                props.tabInfo.setPropertiesCallback("generalProperties", "foundedSchemeNode", treeNode)
            }
        }, 'objectForms/General => handleGetSchemeByIdClick')
    }
    const handleGetSchemeByNameClick = () => {
        runCodeSafely(() => {
            let mcObj = props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
            let schemeNode = mcObj.GetNodeByName(props.tabInfo.properties.generalProperties?.nameToScheme);
            if (schemeNode) {
                let treeNode = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, schemeNode);
                props.tabInfo.setPropertiesCallback("generalProperties", "foundedSchemeNode", treeNode)
            }
        }, 'objectForms/General => handleGetSchemeByNameClick')
    }
    const handleClearTraversabilityClick = () => {
        props.tabInfo.setPropertiesCallback("generalProperties", "allTraversability", []);
        props.tabInfo.setPropertiesCallback("generalProperties", "selectedTraversability", null);
        props.tabInfo.setCurrStatePropertiesCallback("generalProperties", "selectedTraversability", null);
    }
    //DOM Functions
    let generalViewportFormInfo: GeneralViewportFormInfo = {
        properties: props.tabInfo.properties.generalProperties?.generalViewportFormProperties,
        setPropertiesCallback: (key: string, value: any) => {
            if (props.tabInfo.properties.generalProperties) {
                if (key) {
                    let updatedGeneralViewportFormProperties = { ...props.tabInfo.properties.generalProperties.generalViewportFormProperties };
                    updatedGeneralViewportFormProperties[key] = value;
                    props.tabInfo.setPropertiesCallback("generalProperties", "generalViewportFormProperties", updatedGeneralViewportFormProperties);
                }
                else {
                    props.tabInfo.setPropertiesCallback("generalProperties", "generalViewportFormProperties", value);
                }
            }
        },
        setCurrStatePropertiesCallback: (key: string, value: any) => {
            if (props.tabInfo.properties.generalProperties) {
                if (key) {
                    let updatedGeneralViewportFormProperties = { ...props.tabInfo.properties.generalProperties.generalViewportFormProperties };
                    updatedGeneralViewportFormProperties[key] = value;
                    props.tabInfo.setCurrStatePropertiesCallback("generalProperties", "generalViewportFormProperties", updatedGeneralViewportFormProperties);
                }
                else {
                    props.tabInfo.setPropertiesCallback("generalProperties", "generalViewportFormProperties", value);
                }
            }
        },
        setApplyCallBack: (Callback: () => void) => { throw new Error("GeneralViewportFormInfo/setApplyCallBack not implemented") }, // TODO - no use for now
    };

    const getGeneraViewportFieldset = () => {
        return <GeneralViewportForm info={generalViewportFormInfo} />
    }
    const getConditionalSelelctor = () => {
        let ConditionalSelectorCtrlInfo: ConditionalSelectorCtrlInfo = {
            properties: props.tabInfo.properties.generalProperties?.conditionalSelectorCtrlProperties,
            setPropertiesCallback: (key: string, value: any) => {
                if (props.tabInfo.properties.generalProperties) {
                    if (key) {
                        let updatedConditionalSelectorCtrlProperties = { ...props.tabInfo.properties.generalProperties?.conditionalSelectorCtrlProperties };
                        updatedConditionalSelectorCtrlProperties[key] = value;
                        props.tabInfo.setPropertiesCallback("generalProperties", "conditionalSelectorCtrlProperties", updatedConditionalSelectorCtrlProperties);
                    }
                    else {
                        props.tabInfo.setPropertiesCallback("generalProperties", "conditionalSelectorCtrlProperties", value);
                    }
                }
            },
            setCurrStatePropertiesCallback: (key: string, value: any) => {
                if (props.tabInfo.properties.generalProperties) {
                    if (key) {
                        let updatedConditionalSelectorCtrlProperties = { ...props.tabInfo.properties.generalProperties?.conditionalSelectorCtrlProperties };
                        updatedConditionalSelectorCtrlProperties[key] = value;
                        props.tabInfo.setCurrStatePropertiesCallback("generalProperties", "conditionalSelectorCtrlProperties", updatedConditionalSelectorCtrlProperties);
                    }
                    else {
                        props.tabInfo.setPropertiesCallback("generalProperties", "conditionalSelectorCtrlProperties", value);
                    }
                }
            },
            setApplyCallBack: (handleApplyAllFunc: () => void) => {
                props.tabInfo.setApplyCallBack("General/conditionalSelectorApplyFunc", handleApplyAllFunc);
            },
            ctrlHeight: 25,
            dialogPath: "generalProperties.conditionalSelectorCtrlProperties"
        };

        return <ConditionalSelectorCtrl info={ConditionalSelectorCtrlInfo} />
    }
    const getUserData = () => {
        const getUserDataBuffer = (userDataBuffer: Uint8Array) => {
            props.tabInfo.setPropertiesCallback("generalProperties", 'userData', userDataBuffer);
            props.tabInfo.setCurrStatePropertiesCallback("generalProperties", 'userData', userDataBuffer)
        }

        return <UserDataCtrl ctrlHeight={20} userData={props.tabInfo.properties.generalProperties?.userData} getUserDataBuffer={getUserDataBuffer} />
    }
    const getCollectionFieldset = () => {
        return <Fieldset style={{ height: `${globalSizeFactor * 25}vh` }} className="form__space-around" legend="Collection">
            <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 18}vh`, maxHeight: `${globalSizeFactor * 18}vh` }} style={{ width: '100%' }} disabled options={props.tabInfo.currentObject.nodeMcContent.GetCollections()?.map((col: any) => `(${objectWorldTreeService.getObjectHash(col)}) Collection`).flat()}></ListBox>
        </Fieldset>
    }

    const getNodeFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend="Get Node">
            <div style={{ height: `${globalSizeFactor * 13.3}vh` }}>
                <div>
                    <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.3}vh` }}>
                        <div style={{ width: '80%', display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
                            <label>By Id: </label>
                            <InputNumber value={props.tabInfo.properties.generalProperties?.idToScheme ?? null} name="idToScheme" onValueChange={saveData} />
                        </div>
                        <Button disabled={!props.tabInfo.properties.generalProperties?.idToScheme} onClick={handleGetSchemeByIdClick}>Get</Button>
                    </div>
                    <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.3}vh` }}>
                        <div style={{ width: '80%', display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
                            <label>By Name: </label>
                            <InputText name="nameToScheme" onChange={saveData} />
                        </div>
                        <Button disabled={!props.tabInfo.properties.generalProperties?.nameToScheme} onClick={handleGetSchemeByNameClick}>Get</Button>
                    </div>
                </div>

                <label onClick={handleLinkClick} style={{ marginTop: `${globalSizeFactor * 1.5}vh`, color: "blue" }}>
                    {props.tabInfo.properties.generalProperties?.foundedSchemeNode?.label}
                </label>
            </div>
        </Fieldset>
    }
    const getGeneralFieldset = () => {
        return <Fieldset className="form__row-fieldset" legend="General">
            <div style={{ display: 'flex', width: '100%' }}>
                <div style={{ display: 'flex', flexDirection: 'column', width: '50%', padding: `${globalSizeFactor * 0.3}vh` }}>
                    <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.15}vh` }}>
                        <label htmlFor='objectID'>Object ID: </label>
                        <InputText id='objectID' value={props.tabInfo.properties.generalProperties?.objectID ?? ""} name='objectID' onChange={saveData} />
                    </div>
                    <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.15}vh` }}>
                        <label htmlFor='numberOfLocations' style={{ color: "grey" }}>Number of Locations: </label>
                        <InputNumber disabled id='numberOfLocations' value={props.tabInfo.properties.generalProperties?.numberOfLocations ?? null} />
                    </div>
                    <Fieldset className="form__space-around" legend="Name and Description">
                        <div style={{ display: 'flex', flexDirection: 'column', width: '100%' }}>
                            <div style={{ display: 'flex', flexDirection: 'row', width: '100%', justifyContent: 'space-between' }}>
                                <label htmlFor='name'>Name: </label>
                                <InputText id='name' value={props.tabInfo.properties.generalProperties?.name ?? ""} name='name' onChange={saveData} />
                            </div>
                            Description:
                            <InputTextarea style={{ width: '100%', height: `${globalSizeFactor * 10}vh` }} name='description' value={props.tabInfo.properties.generalProperties?.description} onChange={saveData} />
                        </div>
                    </Fieldset>
                </div>
                <div style={{ display: 'flex', flexDirection: 'column', width: '50%', padding: `${globalSizeFactor * 0.3}vh` }}>
                    <div style={{ padding: `${globalSizeFactor * 0.15}vh` }}>
                        <Checkbox style={{ padding: `${globalSizeFactor * 0.15}vh` }} disabled inputId="isAttachedToAnotherObject" checked={props.tabInfo.properties.generalProperties?.isAttachedToAnotherObject} />
                        <label style={{ color: 'grey' }} htmlFor="isAttachedToAnotherObject" className="ml-2">Is Attached To Another Object</label>
                    </div>
                    <div style={{ padding: `${globalSizeFactor * 0.15}vh` }}>
                        <Checkbox style={{ padding: `${globalSizeFactor * 0.15}vh` }} name='isSuppress' inputId="isSuppress" onChange={saveData} checked={props.tabInfo.properties.generalProperties?.isSuppress} />
                        <label htmlFor="isSuppress" className="ml-2">Suppress Query Presentation Map Tile Web Requests</label>
                    </div>
                    <div style={{ padding: `${globalSizeFactor * 0.15}vh` }}>
                        <Checkbox style={{ padding: `${globalSizeFactor * 0.15}vh` }} name='isIgnoreViewport' inputId="isIgnoreViewport" onChange={saveData} checked={props.tabInfo.properties.generalProperties?.isIgnoreViewport} />
                        <label htmlFor="isIgnoreViewport" className="ml-2">Ignore Viewport Visibility Max Scale</label>
                    </div>
                    <p style={{ paddingBottom: `${globalSizeFactor * 0}vh`, paddingTop: `${globalSizeFactor * 0.3}vh` }}>Select Traversability Map Layer:</p>
                    <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 10}vh`, maxHeight: `${globalSizeFactor * 10}vh` }} style={{ width: '100%' }} name='selectedTraversability' onChange={saveData} value={props.tabInfo.properties.generalProperties?.selectedTraversability} options={props.tabInfo.properties.generalProperties?.allTraversability} optionLabel="label"></ListBox>
                    <div style={{ display: 'flex', justifyContent: 'right' }}>
                        <Button onClick={handleClearTraversabilityClick}>Clear</Button>
                    </div>
                </div>
            </div>
        </Fieldset>
    }

    return <div>
        <div>{getGeneraViewportFieldset()}</div>
        <div>{getGeneralFieldset()}</div>
        <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-around' }}>
            <div style={{ width: '50%' }}>  {getConditionalSelelctor()}</div>
            <div style={{ width: '50%' }}>{getCollectionFieldset()}</div>
        </div>
        <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-around', height: `${globalSizeFactor * 17}vh` }}>
            <div style={{ width: '50%', height: `${globalSizeFactor * 17}vh` }}>{getUserData()}</div>
            <div style={{ width: '50%', height: `${globalSizeFactor * 17}vh` }}>{getNodeFieldset()}</div>
        </div>
        <div style={{ paddingBottom: `${globalSizeFactor * 1}vh` }}>
            <Button label="-->" onClick={() => {
                dispatch(setTypeObjectWorldDialogSecond({
                    secondDialogHeader: "Private Properties Of Object",
                    secondDialogComponent: <PropertiesIDList propObject={true} />
                }))
            }} />
        </div>
    </div>

}
