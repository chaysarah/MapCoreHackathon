import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { useEffect, useState } from "react";
import { InputText } from "primereact/inputtext";
import { Fieldset } from "primereact/fieldset";
import { ListBox } from "primereact/listbox";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import { AppState } from "../../../../../redux/combineReducer";
import { useDispatch, useSelector } from "react-redux";
import GeneralViewportForm, { GeneralViewportFormInfo, GeneralViewportFormProperties, GeneralViewportFormPropertiesState } from "../shared/generalViewportForm";
import ConditionalSelectorCtrl, { ConditionalSelectorCtrlProperties, ConditionalSelectorCtrlInfo } from "../ctrls/conditionalSelectorCtrl";
import UserDataCtrl from "../ctrls/userDataCtrl";
import { OverlayFormTabInfo } from "./overlayForm";
import { Properties } from "../../../dialog";
import dialogStateService from "../../../../../services/dialogStateService";
import { MapCoreData } from "mapcore-lib";

export class GeneralPropertiesState implements Properties {
    //Form Fields
    generalViewportFormPropertiesState: GeneralViewportFormPropertiesState;
    ID: string;
    userData: Uint8Array;

    static getDefault(p: any): GeneralPropertiesState {
        let { props, treeRedux, selectedNodeInTree } = p;

        return {
            generalViewportFormPropertiesState: GeneralViewportFormPropertiesState.getDefault(p),
            ID: objectWorldTreeService.getObjIDHeader(props.tabInfo.currentOverlay.nodeMcContent.GetID()),
            userData: selectedNodeInTree.nodeMcContent.GetUserData() ? selectedNodeInTree.nodeMcContent.GetUserData()?.getUserData() : '',
        }
    }
}
export class GeneralProperties extends GeneralPropertiesState {
    //Form Fields
    generalViewportFormProperties: GeneralViewportFormProperties;
    conditionalSelectorCtrlProperties: ConditionalSelectorCtrlProperties;

    static getDefault(p: any): GeneralProperties {
        let stateDefaults = super.getDefault(p);
        let defaults: GeneralProperties = {
            ...stateDefaults,
            generalViewportFormProperties: GeneralViewportFormProperties.getDefault(p),
            conditionalSelectorCtrlProperties: ConditionalSelectorCtrlProperties.getDefault(p),
        }

        return defaults;
    }
};

export default function General(props: { tabInfo: OverlayFormTabInfo }) {
    const dispatch = useDispatch();
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    let selectedNodeInTree = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        generalProperties: false,
        currentOverlay: false,
    })

    //UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            // initialize first time the tab is loaded only
            if (!props.tabInfo.properties.generalProperties) {
                props.tabInfo.setInitialStatePropertiesCallback("generalProperties", null, GeneralPropertiesState.getDefault({ props, treeRedux, selectedNodeInTree }));
                props.tabInfo.setPropertiesCallback("generalProperties", null, GeneralProperties.getDefault({ props, treeRedux, selectedNodeInTree }));
            }
        }, 'OverlayForm/General.useEffect');
    }, [])

    useEffect(() => {
        runCodeSafely(() => {
            // if (isMountedUseEffect.generalProperties) {
            props.tabInfo.setApplyCallBack("General", applyAll);
            // }
            // else {
            //     setIsMountedUseEffect({ ...isMountedUseEffect, generalProperties: true })
            // }
        }, 'OverlayForm/General.useEffect => generalProperties');
    }, [props.tabInfo.properties.generalProperties])

    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.currentOverlay) {
                // set to default values
                props.tabInfo.setInitialStatePropertiesCallback("generalProperties", null, GeneralPropertiesState.getDefault({ props, treeRedux, selectedNodeInTree }));
                props.tabInfo.setPropertiesCallback("generalProperties", null, GeneralProperties.getDefault({ props, treeRedux, selectedNodeInTree }));
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, currentOverlay: true })
            }
        }, 'overlayForms/General.useEffect => currentOverlay');
    }, [props.tabInfo.currentOverlay])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("generalProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in GeneralPropertiesState.getDefault({ props, treeRedux, selectedNodeInTree })) {
                props.tabInfo.setCurrStatePropertiesCallback("generalProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value)
            }
        }, "OverlayForms/General.saveData => onChange")
    }
    const applyAll = () => {
        runCodeSafely(() => {
            // generalViewportForm has separate apply for each property
            let mcOverlay = props.tabInfo.currentOverlay.nodeMcContent as MapCore.IMcOverlay;
            let numericId = objectWorldTreeService.getObjIDValue(props.tabInfo.properties.generalProperties?.ID);
            runMapCoreSafely(() => { mcOverlay.SetID(numericId); }, 'OverlayForms/General.applyAll => IMcOverlay.SetID', true)
            // TODO seConditionalSelector
            // Set userData
            let testerUserData = new MapCoreData.iMcUserDataClass();
            testerUserData.setUserData(props.tabInfo.properties.generalProperties?.userData);
            runMapCoreSafely(() => { mcOverlay.SetUserData(testerUserData); }, 'OverlayForms/General.applyAll => IMcOverlay.SetUserData', true)
            dialogStateService.applyDialogState(["generalProperties.ID", "generalProperties.userData"]);
        }, 'OverlayForms/General.applyAll');
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

    const getGeneralViewportFieldset = () => {
        return <GeneralViewportForm info={generalViewportFormInfo} />
    }
    const getOverlayManagerFieldset = () => {
        return <Fieldset style={{ height: `${globalSizeFactor * 10}vh` }} className="form__space-around" legend="Overlay Manager">
            <ListBox listStyle={{ minHeight: `${globalSizeFactor * 6}vh`, maxHeight: `${globalSizeFactor * 7}vh`, }} style={{ width: '100%' }} disabled options={[`(${objectWorldTreeService.getObjectHash(props.tabInfo.currentOverlay.nodeMcContent.GetOverlayManager())}) Overlay Manager`]}></ListBox>
        </Fieldset>
    }
    const getCollectionFieldset = () => {
        return <Fieldset style={{ height: `${globalSizeFactor * 10}vh` }} className="form__space-around" legend="Collection">
            <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 6}vh`, maxHeight: `${globalSizeFactor * 7}vh` }} style={{ width: '100%' }} disabled options={props.tabInfo.currentOverlay.nodeMcContent.GetCollections()?.map((col: any) => `(${objectWorldTreeService.getObjectHash(col)}) Collection`).flat()}></ListBox>
        </Fieldset>
    }
    const getUserData = () => {
        const getUserDataBuffer = (userDataBuffer: Uint8Array) => {
            props.tabInfo.setPropertiesCallback("generalProperties", 'userData', userDataBuffer);
            props.tabInfo.setCurrStatePropertiesCallback("generalProperties", 'userData', userDataBuffer)
        }

        return <UserDataCtrl ctrlHeight={20} userData={props.tabInfo.properties.generalProperties?.userData} getUserDataBuffer={getUserDataBuffer} />
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
            ctrlHeight: 32,
            dialogPath: "generalProperties.conditionalSelectorCtrlProperties"
        };

        return <ConditionalSelectorCtrl info={ConditionalSelectorCtrlInfo} />
    }

    return (
        <div>
            <div style={{ padding: `${globalSizeFactor * 0.15}vh` }} >
                <label htmlFor='ID'>ID: </label>
                <InputText id='ID' value={props.tabInfo.properties.generalProperties?.ID ?? ""} name='ID' onChange={saveData} />
            </div>
            {getGeneralViewportFieldset()}
            <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-around' }}>
                <div style={{ width: '50%' }}>
                    {getOverlayManagerFieldset()}
                    {getCollectionFieldset()}
                    {getUserData()}
                </div>
                <div style={{ width: '50%' }}>
                    {getConditionalSelelctor()}
                </div>
            </div>
        </div>
    )
}
