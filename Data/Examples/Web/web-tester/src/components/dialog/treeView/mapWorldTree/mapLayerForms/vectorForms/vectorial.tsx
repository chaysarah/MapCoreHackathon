import { Fieldset } from "primereact/fieldset";
import { Properties } from "../../../../dialog";
import { TabInfo } from "../../../../shared/tabCtrls/tabModels";
import { Dropdown } from "primereact/dropdown";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../../redux/combineReducer";
import { Checkbox } from "primereact/checkbox";
import { InputNumber } from "primereact/inputnumber";
import { Button } from "primereact/button";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { useEffect } from "react";
import { ListBox } from "primereact/listbox";
import { TreeNodeModel } from "../../../../../../services/tree.service";
import mapWorldTreeService from "../../../../../../services/mapWorldTreeService";
import { mapWorldNodeType } from "../../../../../shared/models/map-tree-node.model";
import { InputText } from "primereact/inputtext";

export class VectorialPropertiesState implements Properties {
    mcVectorLayer: MapCore.IMcVectorMapLayer;
    isEnabled: boolean;
    priority: number;
    isConsistency: boolean;
    tolerance: number;
    //Viewport Actions
    isCollisionPrevention: boolean;
    brightness: number;
    overlayState: number;

    static getDefault(p: any): VectorialPropertiesState {
        let { currentLayer, mapWorldTree } = p;
        let mcCurrentLayer = currentLayer.nodeMcContent as MapCore.IMcVectorMapLayer;

        let priority: { Value?: number } = {};
        let isEnabled = false;
        runMapCoreSafely(() => {
            isEnabled = mcCurrentLayer.GetOverlayDrawPriority(priority);
        }, 'MapLayerForm/MapLayer.getDefault => IMcVectorMapLayer.GetOverlayDrawPriority', true);

        let isConsistency = false;
        runMapCoreSafely(() => {
            isConsistency = mcCurrentLayer.GetDrawPriorityConsistency();
        }, 'MapLayerForm/MapLayer.getDefault => IMcVectorMapLayer.GetDrawPriorityConsistency', true);

        let tolerance = 0;
        runMapCoreSafely(() => {
            tolerance = mcCurrentLayer.GetToleranceForPoint();
        }, 'MapLayerForm/MapLayer.getDefault => IMcVectorMapLayer.GetToleranceForPoint', true);

        let isCollisionPrevention = false;
        runMapCoreSafely(() => {
            isCollisionPrevention = mcCurrentLayer.GetCollisionPrevention();
        }, 'MapLayerForm/MapLayer.getDefault => IMcVectorMapLayer.GetCollisionPrevention', true);

        return {
            mcVectorLayer: mcCurrentLayer,
            isEnabled: isEnabled,
            priority: priority.Value || 0,
            isConsistency: isConsistency,
            tolerance: tolerance,
            //Viewport Actions
            isCollisionPrevention: isCollisionPrevention,
            brightness: null,
            overlayState: null,
        }
    }
}
export class VectorialProperties extends VectorialPropertiesState {
    mcVectorLayer: MapCore.IMcVectorMapLayer;
    viewportsList: TreeNodeModel[];
    selectedViewport: null;

    static getDefault(p: any): VectorialProperties {
        // let stateDefaults = super.getDefault(p);
        let { currentLayer, mapWorldTree } = p;
        let mcCurrentLayer = currentLayer.nodeMcContent as MapCore.IMcVectorMapLayer;
        let stateDefaults = super.getDefault(p);
        let parentViewport = mapWorldTreeService.getNearestParentByType(mapWorldTree, currentLayer, mapWorldNodeType.MAP_VIEWPORT);

        return {
            ...stateDefaults,
            mcVectorLayer: mcCurrentLayer,
            viewportsList: [parentViewport],
            selectedViewport: null,

        }
    }
}

export default function Vectorial(props: { tabInfo: TabInfo }) {
    let { tabProperties, setPropertiesCallback, applyCurrStatePropertiesCallback, setCurrStatePropertiesCallback, setApplyCallBack, saveData } = props.tabInfo;
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    useEffect(() => {
        setApplyCallBack(applyAll);
    }, [tabProperties])
    const applyAll = () => {
        runCodeSafely(() => {
            handleOverlayPriorityApplyClick();
            handleConsistencyApplyClick();
            handleToleranceApplyClick();
            //Apply Collision Prevention
            applyCurrStatePropertiesCallback(['isCollisionPrevention']);
            let typedMcVectorLayer = tabProperties.mcVectorLayer as MapCore.IMcVectorMapLayer;
            runMapCoreSafely(() => {
                typedMcVectorLayer.SetCollisionPrevention(tabProperties.isCollisionPrevention);
            }, 'MapLayerForm/Vectorial.applyAll => IMcVectorMapLayer.SetCollisionPrevention', true)

        }, 'MapLayerForm/Vectorial.applyAll')
    }
    //#region Handle Functions
    const handleOverlayPriorityApplyClick = () => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['isEnabled', 'priority']);
            let typedMcVectorLayer = tabProperties.mcVectorLayer as MapCore.IMcVectorMapLayer;
            runMapCoreSafely(() => {
                typedMcVectorLayer.SetOverlayDrawPriority(tabProperties.isEnabled, tabProperties.priority);
            }, 'MapLayerForm/Vectorial.handleOverlayPriorityApplyClick => IMcVectorMapLayer.SetOverlayDrawPriority', true)
        }, 'MapLayerForm/Vectorial.handleOverlayPriorityApplyClick')
    }
    const handleConsistencyApplyClick = () => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['isConsistency']);
            let typedMcVectorLayer = tabProperties.mcVectorLayer as MapCore.IMcVectorMapLayer;
            runMapCoreSafely(() => {
                typedMcVectorLayer.SetDrawPriorityConsistency(tabProperties.isConsistency);
            }, 'MapLayerForm/Vectorial.handleConsistencyApplyClick => IMcVectorMapLayer.SetDrawPriorityConsistency', true)
        }, 'MapLayerForm/Vectorial.handleConsistencyApplyClick')
    }
    const handleToleranceApplyClick = () => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['tolerance']);
            let typedMcVectorLayer = tabProperties.mcVectorLayer as MapCore.IMcVectorMapLayer;
            runMapCoreSafely(() => {
                typedMcVectorLayer.SetToleranceForPoint(tabProperties.tolerance);
            }, 'MapLayerForm/Vectorial.handleToleranceApplyClick => IMcVectorMapLayer.SetToleranceForPoint', true)
        }, 'MapLayerForm/Vectorial.handleToleranceApplyClick')
    }
    const handleSetBrightnessClick = () => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['brightness']);
            let typedMcVectorLayer = tabProperties.mcVectorLayer as MapCore.IMcVectorMapLayer;
            runMapCoreSafely(() => {
                tabProperties.selectedViewport ?
                    typedMcVectorLayer.SetBrightness(tabProperties.brightness, tabProperties.selectedViewport.nodeMcContent) :
                    typedMcVectorLayer.SetBrightness(tabProperties.brightnes);
            }, 'MapLayerForm/Vectorial.handleSetBrightnessClick => IMcVectorMapLayer.SetBrightness', true)
        }, 'MapLayerForm/Vectorial.handleSetBrightnessClick')
    }
    const handleGetBrightnessClick = () => {
        runCodeSafely(() => {
            let typedMcVectorLayer = tabProperties.mcVectorLayer as MapCore.IMcVectorMapLayer;
            let brightness = 0;
            runMapCoreSafely(() => {
                brightness = tabProperties.selectedViewport ?
                    typedMcVectorLayer.GetBrightness(tabProperties.selectedViewport.nodeMcContent) :
                    typedMcVectorLayer.GetBrightness();
            }, 'MapLayerForm/Vectorial.handleGetBrightnessClick => IMcVectorMapLayer.GetBrightness', true)
            setPropertiesCallback('brightness', brightness);
        }, 'MapLayerForm/Vectorial.handleGetBrightnessClick')
    }
    const handleSetStateClick = () => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['overlayState']);
            let typedMcVectorLayer = tabProperties.mcVectorLayer as MapCore.IMcVectorMapLayer;
            let numbersState = tabProperties.overlayState.split(' ').map(Number);
            let overlayStateBuffer = new Uint8Array(numbersState);
            runMapCoreSafely(() => {
                tabProperties.selectedViewport ?
                    typedMcVectorLayer.SetOverlayState(overlayStateBuffer, tabProperties.selectedViewport.nodeMcContent) :
                    typedMcVectorLayer.SetOverlayState(overlayStateBuffer);
            }, 'MapLayerForm/Vectorial.handleSetStateClick => IMcVectorMapLayer.SetOverlayState', true)
        }, 'MapLayerForm/Vectorial.handleSetStateClick')
    }
    const handleGetStateClick = () => {
        runCodeSafely(() => {
            let typedMcVectorLayer = tabProperties.mcVectorLayer as MapCore.IMcVectorMapLayer;
            let overlayState: Uint8Array = new Uint8Array();
            runMapCoreSafely(() => {
                overlayState = tabProperties.selectedViewport ?
                    typedMcVectorLayer.GetOverlayState(tabProperties.selectedViewport.nodeMcContent) :
                    typedMcVectorLayer.GetOverlayState();
            }, 'MapLayerForm/Vectorial.handleGetStateClick => IMcVectorMapLayer.GetOverlayState', true)
            let overlayStateStr = Array.from(overlayState).join(' ');
            setPropertiesCallback('overlayState', overlayStateStr);
        }, 'MapLayerForm/Vectorial.handleGetStateClick')
    }  
    //#endregion
    //#region DOM Functions
    const getOverlayDrawPriorityFieldset = () => {
        return <Fieldset legend='Overlay Draw Priority' className="form__column-fieldset">
            <div className="form__flex-and-row form__items-center">
                <Checkbox name='isEnabled' inputId="isEnabled" onChange={saveData} checked={tabProperties.isEnabled} />
                <label htmlFor="isEnabled">Enabled</label>
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '45%' }} className="form__flex-and-row-between form__items-center">
                    <label htmlFor="priority">Priority: </label>
                    <InputNumber tooltipOptions={{ position: 'top' }} tooltip="Between -32768 to 32767" id='priority' value={tabProperties.priority} name="priority" onValueChange={saveData} />
                </div>
                <Button label='Apply' onClick={handleOverlayPriorityApplyClick} />
            </div>
        </Fieldset>
    }
    const getDrawPriorityConsistencyFielset = () => {
        return <Fieldset legend='Overlay Draw Priority' className="form__row-fieldset form__space-between">
            <div className="form__flex-and-row form__items-center">
                <Checkbox name='isConsistency' inputId="isConsistency" onChange={saveData} checked={tabProperties.isConsistency} />
                <label htmlFor="isConsistency">Is Consistency</label>
            </div>
            <Button label='Apply' onClick={handleConsistencyApplyClick} />
        </Fieldset>
    }
    const getToleranceFielset = () => {
        return <Fieldset legend='Tolerance' className="form__row-fieldset form__space-between">
            <div style={{ width: '45%' }} className="form__flex-and-row-between form__items-center">
                <label htmlFor="tolerance">Tolerance For Point: </label>
                <InputNumber id='tolerance' value={tabProperties.tolerance} name="tolerance" onValueChange={saveData} />
            </div>
            <Button label='Apply' onClick={handleToleranceApplyClick} />
        </Fieldset>
    }
    const getViewportActionsFieldset = () => {
        return <div className="form__column-container">
            <Fieldset legend='Viewport Actions' className="form__column-fieldset">
                <div className="form__column-container">
                    <span style={{ textDecoration: 'underline' }}>Viewports: </span>
                    <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 13.7}vh`, maxHeight: `${globalSizeFactor * 13.7}vh`, }} style={{ width: `${globalSizeFactor * 30}vh` }} name='selectedViewport' optionLabel='label' value={tabProperties.selectedViewport} onChange={saveData} options={tabProperties.viewportsList} />
                </div>
                <div className="form__flex-and-row-between  form__items-center">
                    <div style={{ width: '45%' }} className="form__flex-and-row-between form__items-center">
                        <label htmlFor="brightness">Brightness (-1 till 1): </label>
                        <InputNumber minFractionDigits={0} maxFractionDigits={5} id='brightness' value={tabProperties.brightness} name="brightness" onValueChange={saveData} />
                    </div>
                    <div style={{ width: '40%' }} className="form__flex-and-row-between form__items-center">
                        <Button style={{ width: '49%' }} label="Set Brightness" onClick={handleSetBrightnessClick} />
                        <Button style={{ width: '49%' }} label="Get Brightness" onClick={handleGetBrightnessClick} />
                    </div>
                </div>
                <div className="form__flex-and-row-between  form__items-center">
                    <div style={{ width: '45%' }} className="form__flex-and-row-between form__items-center">
                        <label htmlFor="overlayState">Overlay State: </label>
                        <InputText tooltipOptions={{ position: 'top' }} tooltip="Values must be byte (0..255) separated with blank(s)" id='overlayState' value={tabProperties.overlayState} name="overlayState" onChange={saveData} />
                    </div>
                    <div style={{ width: '40%' }} className="form__flex-and-row-between form__items-center">
                        <Button style={{ width: '49%' }} label="Set State" onClick={handleSetStateClick} />
                        <Button style={{ width: '49%' }} label="Get State" onClick={handleGetStateClick} />
                    </div>
                </div>
            </Fieldset>
            <div style={{ paddingLeft: '1%' }} className="form__flex-and-row form__items-center">
                <Checkbox name='isCollisionPrevention' inputId="isCollisionPrevention" onChange={saveData} checked={tabProperties.isCollisionPrevention} />
                <label htmlFor="isCollisionPrevention">Collision Prevention</label>
            </div>
        </div>
    }
    //#endregion

    return <div className="form__column-container">
        {getOverlayDrawPriorityFieldset()}
        {getDrawPriorityConsistencyFielset()}
        {getToleranceFielset()}
        {getViewportActionsFieldset()}

    </div>
}