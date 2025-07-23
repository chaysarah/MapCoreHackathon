// External libraries
import { useState, useEffect, ReactElement } from 'react';
import { useDispatch, useSelector } from 'react-redux';

// UI/component libraries
import { Fieldset } from "primereact/fieldset";
import { ListBox } from "primereact/listbox";
import { Dropdown } from "primereact/dropdown";
import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";
import { Dialog } from "primereact/dialog";
import { ConfirmDialog } from "primereact/confirmdialog";

// Project-specific imports
import { getEnumDetailsList, getEnumValueDetails, ViewportData, MapCoreData, ObjectWorldService } from 'mapcore-lib'
import { MapViewportFormTabInfo } from "./mapViewportForm";
import SaveToFile from "../../objectWorldTree/overlayForms/saveToFile";
import { Properties } from "../../../dialog";
import RectFromMapCtrl, { WORLD_RECT_CTRL_OBJECT_NAME } from "../../../shared/RectFromMapCtrl";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../redux/combineReducer";
import dialogStateService from "../../../../../services/dialogStateService";

export class ObjectWorldPropertiesState implements Properties {
    overlaysActionOption: MapCore.IMcConditionalSelector.EActionOptions;
    objectsActionOption: MapCore.IMcConditionalSelector.EActionOptions;
    cameraScale: number;
    worldRectangleBox: MapCore.SMcBox;
    showRectangle: boolean;

    static getDefault(p: any): ObjectWorldPropertiesState {
        return {
            overlaysActionOption: MapCore.IMcConditionalSelector.EActionOptions.EAO_FORCE_TRUE,
            objectsActionOption: MapCore.IMcConditionalSelector.EActionOptions.EAO_FORCE_TRUE,
            cameraScale: null,
            worldRectangleBox: null,
            showRectangle: true,
        }
    }
}
export class ObjectWorldProperties extends ObjectWorldPropertiesState {
    allOverlays: MapCore.IMcOverlay[];
    allObjects: MapCore.IMcObject[];
    selectedOverlays: MapCore.IMcOverlay[];
    selectedObjects: MapCore.IMcObject[];

    static getDefault(p: any): ObjectWorldProperties {
        const { viewport } = p;
        let overlayManager: MapCore.IMcOverlayManager = viewport.GetOverlayManager()
        let overlays: MapCore.IMcOverlay[] = overlayManager.GetOverlays()
        let objects: MapCore.IMcObject[] = [];
        overlays.map(
            overlay => {
                let overlayObjs: MapCore.IMcObject[] = overlay.GetObjects().filter(obj => {
                    let pstrName: any = {}, pstrDescription: any = {};
                    obj.GetNameAndDescription(pstrName, pstrDescription);
                    return pstrName.Value !== WORLD_RECT_CTRL_OBJECT_NAME;
                })
                objects = [...objects, ...overlayObjs]
            }
        )

        let stateDefaults = super.getDefault(p);
        let defaults: ObjectWorldProperties = {
            ...stateDefaults,
            allOverlays: overlays,
            allObjects: objects,
            selectedOverlays: [],
            selectedObjects: [],
        }
        return defaults;
    }
};

export default function ObjectWorld(props: { tabInfo: MapViewportFormTabInfo }) {
    const dispatch = useDispatch();
    let treeRedux = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    // let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    let viewport: MapCore.IMcMapViewport = props.tabInfo.currentViewport.nodeMcContent;
    let viewportData: ViewportData = MapCoreData.viewportsData.filter(v => v.viewport == viewport)[0]
    let activeOverlay = ObjectWorldService.findActiveOverlayByMcViewport(viewportData.viewport)
    let [isConfirmDialog, setIsConfirmDialog] = useState<boolean>(false);
    let [confirmDialogMessage, setConfirmDialogMessage] = useState<string>(null);

    const [enumDetails] = useState({
        EActionOptions: getEnumDetailsList(MapCore.IMcConditionalSelector.EActionOptions),
    });
    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        objectWorldProperties: false,
        currentViewport: false,
    })

    let [openSecondDialog, setOpenSecondDialog] = useState({
        saveToFile: false,
    })

    //UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            // initialize first time the tab is loaded only
            if (!props.tabInfo.properties.objectWorldProperties) {
                props.tabInfo.setInitialStatePropertiesCallback("objectWorldProperties", null, ObjectWorldPropertiesState.getDefault({ viewport }));
                props.tabInfo.setPropertiesCallback("objectWorldProperties", null, ObjectWorldProperties.getDefault({ viewport }));
            }
        }, 'MapViewportForm/ObjectWorld.useEffect');
    }, [])

    useEffect(() => {
        runCodeSafely(() => {
            props.tabInfo.setApplyCallBack("ObjectWorld", applyAll);
        }, 'MapViewportForm/ObjectWorld.useEffect => objectWorldProperties');
    }, [props.tabInfo.properties.objectWorldProperties])

    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.currentViewport) {
                props.tabInfo.setInitialStatePropertiesCallback("objectWorldProperties", null, ObjectWorldPropertiesState.getDefault({ viewport }));
                props.tabInfo.setPropertiesCallback("objectWorldProperties", null, ObjectWorldProperties.getDefault({ viewport }));
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, currentViewport: true })
            }
        }, 'MapViewportForm/ObjectWorld.useEffect => currentViewport');
    }, [props.tabInfo.currentViewport])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            let class_name: string = event.originalEvent?.currentTarget?.className;
            let value = event.target.type === "checkbox" ? event.target.checked : event.target.value;
            if (class_name?.includes("p-dropdown-item")) {
                value = event.target.value.theEnum;
            }
            props.tabInfo.setPropertiesCallback("objectWorldProperties", event.target.name, value);
            if (event.target.name in ObjectWorldPropertiesState.getDefault({ viewport })) {
                props.tabInfo.setCurrStatePropertiesCallback("objectWorldProperties", event.target.name, value)
            }
        }, "MapViewportForm/ObjectWorld.saveData => onChange")
    }
    const applyAll = () => {
        runCodeSafely(() => {

        }, 'MapViewportForm/ObjectWorld.applyAll');
    }

    const applyOverlaysAction = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                viewport.SetOverlaysVisibilityOption(props.tabInfo.properties.objectWorldProperties.overlaysActionOption,
                    props.tabInfo.properties.objectWorldProperties.selectedOverlays
                );
            }, "MapViewportForm/ObjectWorld/applyOverlaysAction/SetOverlaysVisibilityOption", true)
            dialogStateService.applyDialogState([
                "objectWorldProperties.overlaysActionOption",
            ])
        }, "MapViewportForm/ObjectWorld/applyOverlaysAction => onClick")
    }
    const applyObjectsAction = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                viewport.SetObjectsVisibilityOption(props.tabInfo.properties.objectWorldProperties.objectsActionOption,
                    props.tabInfo.properties.objectWorldProperties.selectedObjects
                );
            }, "MapViewportForm/ObjectWorld/applyObjectsAction/SetObjectsVisibilityOption", true)
            dialogStateService.applyDialogState([
                "objectWorldProperties.objectsActionOption",
            ])
        }, "MapViewportForm/ObjectWorld/applyObjectsAction => onClick")
    }
    // const handleViewportsListChange = (e) => {
    //     runCodeSafely(() => {
    //         props.tabInfo.setPropertiesCallback("objectWorldProperties", "selectedOverlays",  event.target.value);
    //     }, "OverlayForms/objects.handleObjectsListChange => onChange")
    // }
    const getOverlayLabel = (overlay: MapCore.IMcOverlay) => {
        return `[${overlay.GetID()}] Overlay` // TODO: replace by hash when supported
    }
    const getObjectLabel = (object: MapCore.IMcObject) => {
        return `[${object.GetID()}] Object` // TODO: replace by hash when supported
    }

    const confirmSaveObjectsEmptyValues = () => {
        if ((props.tabInfo.properties.objectWorldProperties.worldRectangleBox.MinVertex.x === 0 &&
            props.tabInfo.properties.objectWorldProperties.worldRectangleBox.MinVertex.y === 0 &&
            props.tabInfo.properties.objectWorldProperties.worldRectangleBox.MinVertex.z === 0) ||
            (props.tabInfo.properties.objectWorldProperties.worldRectangleBox.MaxVertex.x === 0 &&
                props.tabInfo.properties.objectWorldProperties.worldRectangleBox.MaxVertex.y === 0 &&
                props.tabInfo.properties.objectWorldProperties.worldRectangleBox.MaxVertex.z === 0)) {
            setConfirmDialogMessage("Missing Points Values")
            setIsConfirmDialog(true)
            return false;
        }
        else if (props.tabInfo.properties.objectWorldProperties.cameraScale === null) {
            setConfirmDialogMessage("Missing Camera Scale")
            setIsConfirmDialog(true)
            return false;
        }
        return true;
    }
    const onGetObjectsVisibleClick = () => {
        if (confirmSaveObjectsEmptyValues()) {
            setOpenSecondDialog({ ...openSecondDialog, saveToFile: true })
        }
    }

    const handleSaveObjectsToFileOK = (fileName: string, fileType: string) => {
        let objectsToSave: MapCore.IMcObject[];
        runMapCoreSafely(() => {
            objectsToSave = viewport.GetObjectsVisibleInWorldRectAndScale2D(
                props.tabInfo.properties.objectWorldProperties.worldRectangleBox,
                props.tabInfo.properties.objectWorldProperties.cameraScale);
        }, "MapViewportForm/ObjectWorld/handleSaveObjectsToFileOK/GetObjectsVisibleInWorldRectAndScale2D", true)

        let format = fileType.endsWith('.json') ?
            MapCore.IMcOverlayManager.EStorageFormat.ESF_JSON :
            MapCore.IMcOverlayManager.EStorageFormat.ESF_MAPCORE_BINARY;
        let uint8ArrayData: Uint8Array = activeOverlay.SaveObjects(objectsToSave, format/*, version?? */)
        let finalFileName = `${fileName}${fileType}`;
        MapCore.IMcMapDevice.DownloadBufferAsFile(uint8ArrayData, finalFileName);
        setOpenSecondDialog({ ...openSecondDialog, saveToFile: false })

        dialogStateService.applyDialogState([
            "objectWorldProperties.cameraScale",
            "objectWorldProperties.worldRectCtrlProperties",
        ])
    }
    let savedFileTypeOptions = [
        { name: 'MapCore schemes Files (*.mcsch, *.mcsch.json , *.m, *.json)', extension: '.mcsch' },
        { name: 'MapCore schemes Binary Files (*.mcsch,*.m)', extension: '.mcsch' },
        { name: 'MapCore schemes Json Files (*.mcsch.json, *.json)', extension: '.mcsch.json' },
        { name: 'All Files', extension: '' },
    ]

    return (
        <div className="form__column-container">
            <div className="form__row-container">
                <Fieldset className="form__column-fieldset" legend="Overlays Visibility">
                    <ListBox name="selectedOverlays" multiple options={props.tabInfo.properties.objectWorldProperties?.allOverlays}
                        value={props.tabInfo.properties.objectWorldProperties?.selectedOverlays}
                        itemTemplate={getOverlayLabel}
                        style={{ maxHeight: '40%' }}
                        onChange={saveData} optionLabel="label" />
                    <div className="form__center-aligned-row">
                        <label htmlFor='overlaysActionOption'>Action Option:</label>
                        <Dropdown className="form__dropdown-input" id='overlaysActionOption' name='overlaysActionOption' value={props.tabInfo.properties.objectWorldProperties ? getEnumValueDetails(props.tabInfo.properties.objectWorldProperties?.overlaysActionOption, enumDetails.EActionOptions) : null} onChange={saveData} options={enumDetails.EActionOptions} optionLabel="name" />
                    </div>
                    <div className="form__apply-buttons-container">
                        <Button onClick={applyOverlaysAction}>Apply</Button>
                    </div>
                </Fieldset>
                <Fieldset className="form__column-fieldset" legend="Objects Visibility">
                    <ListBox name="selectedObjects" multiple options={props.tabInfo.properties.objectWorldProperties?.allObjects}
                        value={props.tabInfo.properties.objectWorldProperties?.selectedObjects}
                        itemTemplate={getObjectLabel}
                        style={{ maxHeight: '40%' }}
                        onChange={saveData} optionLabel="label" />
                    <div className="form__center-aligned-row">
                        <label htmlFor='objectsActionOption'>Action Option:</label>
                        <Dropdown className="form__dropdown-input" id='objectsActionOption' name='objectsActionOption' value={props.tabInfo.properties.objectWorldProperties ? getEnumValueDetails(props.tabInfo.properties.objectWorldProperties?.objectsActionOption, enumDetails.EActionOptions) : null} onChange={saveData} options={enumDetails.EActionOptions} optionLabel="name" />
                    </div>
                    <div className="form__apply-buttons-container">
                        <Button onClick={applyObjectsAction}>Apply</Button>
                    </div>
                </Fieldset>
            </div>
            <Fieldset className="form__column-fieldset" legend="Get Objects Visible In World Rect And Scale 2D">
                <Fieldset legend="World Rect Points">
                    <RectFromMapCtrl info={{
                        rectangleBox: props.tabInfo.properties.objectWorldProperties?.worldRectangleBox,
                        showRectangle: props.tabInfo.properties.objectWorldProperties?.showRectangle,
                        setProperty: (name: string, value: any) => {
                            props.tabInfo.setPropertiesCallback("objectWorldProperties", name, value);
                            props.tabInfo.setCurrStatePropertiesCallback("objectWorldProperties", name, value);
                        },
                        readOnly: false,
                        rectCoordSystem: MapCore.EMcPointCoordSystem.EPCS_WORLD,
                        currViewport: viewport,
                    }}
                    /></Fieldset>
                <div>
                    <div className="form__center-aligned-row">
                        <label htmlFor='cameraScale'>Camera Scale:</label>
                        <InputNumber className="form__narrow-input" id='cameraScale' value={props.tabInfo.properties.objectWorldProperties?.cameraScale ?? 0} name='cameraScale' onValueChange={saveData} />
                    </div>
                    <div className="form__apply-buttons-container">
                        <Button onClick={onGetObjectsVisibleClick}>Get Objects Visible</Button>
                    </div>
                </div>
            </Fieldset>

            <Dialog className="scroll-dialog-save-to-file" header="Save Objects to File" visible={openSecondDialog.saveToFile} onHide={() => { setOpenSecondDialog({ ...openSecondDialog, saveToFile: false }) }}>
                <SaveToFile
                    savedFileTypeOptions={savedFileTypeOptions}
                    handleSaveToFileOk={handleSaveObjectsToFileOK} />
            </Dialog>
            <ConfirmDialog
                message={confirmDialogMessage}
                header=''
                footer={<div></div>}
                visible={isConfirmDialog}
                onHide={e => { setIsConfirmDialog(false) }}
            />
        </div>
    )
}
