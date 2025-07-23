// External libraries
import { useState, useEffect, ReactElement } from 'react';
import { useDispatch, useSelector } from 'react-redux';

// UI/component libraries
import { Fieldset } from "primereact/fieldset";
import { Dropdown } from "primereact/dropdown";
import { InputNumber } from "primereact/inputnumber";
import { Checkbox } from "primereact/checkbox";
import { Button } from "primereact/button";

// Project-specific imports
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import RenderScreenRectToBufferImage from "./renderScreenRectToBufferImg";
import { MapViewportFormTabInfo } from "../mapViewportForm";
import Vector2DFromMap from "../../../objectWorldTree/shared/Vector2DFromMap";
import { Properties } from "../../../../dialog";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../../redux/combineReducer";
import dialogStateService from "../../../../../../services/dialogStateService";
import addJsonViewportService from "../../../../../../services/addJsonViewport.service";
import { setTypeMapWorldDialogSecond } from "../../../../../../redux/MapWorldTree/mapWorldTreeActions";

interface objectDelay {
    objectDelayType: MapCore.IMcMapViewport.EObjectDelayType;
    numberOfUpdatesPerRender: number;
    delayEnabled: boolean;
}

export class RenderingPropertiesState implements Properties {
    objectDelayArr: objectDelay[];
    freezeObjectsVisualization: boolean;
    minNumItemForOverload: number;
    overloadModeEnabled: boolean;
    vector3DExtrusionVisibilityMaxScale: number;
    model3DVisibilityMaxScale: number;
    objectVisibiltyMaxScale: number;
    thresholdInPixels: number;
    screenSizeTerrainObjectsFactor: number;
    //Render Screen Rect To Buffer Fields
    topLeftVector: MapCore.SMcVector2D;
    bottomRightVector: MapCore.SMcVector2D;
    selectedBufferPixelFormat: { name: string; code: number; theEnum: any; };
    width: number;
    height: number;
    bufferRawPitch: number;
    isRenderToBufferMode: boolean;

    static getDefault(p: any): RenderingPropertiesState {
        let { viewport }: { viewport: MapCore.IMcMapViewport } = p;
        let defaults: RenderingPropertiesState = new RenderingPropertiesState();

        defaults.objectDelayArr = [];
        getEnumDetailsList(MapCore.IMcMapViewport.EObjectDelayType).map(objectDelayType => {
            let pbEnabled: any = {}
            let puNumToUpdatePerRender: any = {}
            runMapCoreSafely(() => {
                viewport.GetObjectsDelay(objectDelayType.theEnum, pbEnabled, puNumToUpdatePerRender);
            }, "MapViewportForm/RenderingPropertiesState/getDefault => IMcMapViewport.GetObjectsDelay", true)
            defaults.objectDelayArr.push({
                objectDelayType: objectDelayType.theEnum,
                numberOfUpdatesPerRender: puNumToUpdatePerRender.Value,
                delayEnabled: pbEnabled.Value
            })
        })

        runMapCoreSafely(() => {
            defaults.freezeObjectsVisualization = viewport.GetFreezeObjectsVisualization();
        }, "MapViewportForm/RenderingPropertiesState/getDefault => IMcMapViewport.GetFreezeObjectsVisualization", true)

        let pbEnabled: any = {}
        let puMinNumItemsForOverload: any = {}
        runMapCoreSafely(() => {
            viewport.GetOverloadMode(pbEnabled, puMinNumItemsForOverload);
        }, "MapViewportForm/RenderingPropertiesState/getDefault => IMcMapViewport.GetOverloadMode", true)
        defaults.overloadModeEnabled = pbEnabled.Value;
        defaults.minNumItemForOverload = puMinNumItemsForOverload.Value;

        runMapCoreSafely(() => {
            defaults.vector3DExtrusionVisibilityMaxScale = viewport.GetVector3DExtrusionVisibilityMaxScale();
        }, "MapViewportForm/RenderingPropertiesState/getDefault => IMcMapViewport.GetVector3DExtrusionVisibilityMaxScale", true)

        runMapCoreSafely(() => {
            defaults.model3DVisibilityMaxScale = viewport.Get3DModelVisibilityMaxScale();
        }, "MapViewportForm/RenderingPropertiesState/getDefault => IMcMapViewport.Get3DModelVisibilityMaxScale", true)

        runMapCoreSafely(() => {
            defaults.objectVisibiltyMaxScale = viewport.GetObjectsVisibilityMaxScale();
        }, "MapViewportForm/RenderingPropertiesState/getDefault => IMcMapViewport.GetObjectsVisibilityMaxScale", true)

        runMapCoreSafely(() => {
            defaults.thresholdInPixels = viewport.GetObjectsMovementThreshold();
        }, "MapViewportForm/RenderingPropertiesState/getDefault => IMcMapViewport.GetObjectsMovementThreshold", true)

        runMapCoreSafely(() => {
            if (viewport.GetMapType() === MapCore.IMcMapCamera.EMapType.EMT_3D) {
                defaults.screenSizeTerrainObjectsFactor = viewport.GetScreenSizeTerrainObjectsFactor();
            }
        }, "MapViewportForm/RenderingPropertiesState/getDefault => IMcMapViewport.GetScreenSizeTerrainObjectsFactor", true)

        let ppPixelFormat: { Value?: MapCore.IMcTexture.EPixelFormat } = {};
        runMapCoreSafely(() => {
            viewport.GetRenderToBufferNativePixelFormat(ppPixelFormat)
        }, "MapViewportForm/RenderingPropertiesState/getDefault => IMcMapViewport.GetScreenSizeTerrainObjectsFactor", true)
        let pixelFormatArr = getEnumDetailsList(MapCore.IMcTexture.EPixelFormat);
        defaults.selectedBufferPixelFormat = getEnumValueDetails(ppPixelFormat.Value, pixelFormatArr);

        let pWidth: { Value?: number } = {};
        let pHeight: { Value?: number } = {};
        runMapCoreSafely(() => {
            viewport.GetViewportSize(pWidth, pHeight);
        }, "MapViewportForm/RenderingPropertiesState/getDefault => IMcMapViewport.GetViewportSize", true)
        defaults.bottomRightVector = new MapCore.SMcVector2D(pWidth.Value, pHeight.Value);
        defaults.bufferRawPitch = 0;
        defaults.topLeftVector = new MapCore.SMcVector2D(0, 0);
        defaults.width = pWidth.Value;
        defaults.height = pHeight.Value;

        runMapCoreSafely(() => {
            defaults.isRenderToBufferMode = viewport.GetRenderToBufferMode();
        }, "MapViewportForm/RenderingPropertiesState/getDefault => IMcMapViewport.GetRenderToBufferMode", true)

        return defaults;
    }
}
export class RenderingProperties extends RenderingPropertiesState {
    selectedObjectDelay: MapCore.IMcMapViewport.EObjectDelayType;

    static getDefault(p: any): RenderingProperties {
        let stateDefaults = super.getDefault(p);
        let defaults: RenderingProperties = {
            ...stateDefaults,
            selectedObjectDelay: MapCore.IMcMapViewport.EObjectDelayType.EODT_VIEWPORT_CHANGE_OBJECT_UPDATE
        }
        return defaults;
    }
};

export default function Rendering(props: { tabInfo: MapViewportFormTabInfo }) {
    const dispatch = useDispatch();
    let treeRedux = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    let viewport = selectedNodeInTree.nodeMcContent;
    let enumDetails = {
        EObjectDelayType: getEnumDetailsList(MapCore.IMcMapViewport.EObjectDelayType),
        EPixelFormat: getEnumDetailsList(MapCore.IMcTexture.EPixelFormat),
    }
    const [mockStateObject, setMockStateObject] = useState<RenderingPropertiesState>(new RenderingPropertiesState());
    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        renderingProperties: false,
        currentViewport: false,
    })
    //#region UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            // initialize first time the tab is loaded only
            if (!props.tabInfo.properties.renderingProperties) {
                let defaultProperties =
                    props.tabInfo.setInitialStatePropertiesCallback("renderingProperties", null, RenderingPropertiesState.getDefault({ viewport }));
                props.tabInfo.setPropertiesCallback("renderingProperties", null, RenderingProperties.getDefault({ viewport }));
            }
        }, 'MapViewportForm/Rendering.useEffect');
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            props.tabInfo.setApplyCallBack("Rendering", applyAll);
        }, 'MapViewportForm/Rendering.useEffect => renderingProperties');
    }, [props.tabInfo.properties.renderingProperties])
    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.currentViewport) {
                props.tabInfo.setInitialStatePropertiesCallback("renderingProperties", null, RenderingPropertiesState.getDefault({ viewport }));
                props.tabInfo.setPropertiesCallback("renderingProperties", null, RenderingProperties.getDefault({ viewport }));
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, currentViewport: true })
            }
        }, 'MapViewportForm/Rendering.useEffect => currentViewport');
    }, [props.tabInfo.currentViewport])
    //#endregion
    const applyAll = () => {
        runCodeSafely(() => {

        }, 'MapViewportForm/Rendering.applyAll');
    }
    //#region Save Functions
    const saveData = (event: any) => {
        runCodeSafely(() => {
            let class_name: string = event.originalEvent?.currentTarget?.className;
            let value = event.target.type === "checkbox" ? event.target.checked : event.target.value;
            if (class_name?.includes("p-dropdown-item")) {
                value = event.target.value.theEnum;
            }
            props.tabInfo.setPropertiesCallback("renderingProperties", event.target.name, value);
            if (event.target.name in mockStateObject) {
                props.tabInfo.setCurrStatePropertiesCallback("renderingProperties", event.target.name, value)
            }
        }, "MapViewportForm/Rendering.saveData => onChange")
    }
    const saveObjectDelay = (event: any) => {
        runCodeSafely(() => {
            let value = event.target.type === "checkbox" ? event.target.checked : event.target.value;
            let updatedObjectDelayArr = [];
            props.tabInfo.properties.renderingProperties.objectDelayArr.map(objectDelay => {
                if (objectDelay.objectDelayType === props.tabInfo.properties.renderingProperties.selectedObjectDelay) {
                    updatedObjectDelayArr.push({ ...objectDelay, [event.target.name]: value })
                }
                else {
                    updatedObjectDelayArr.push({ ...objectDelay })
                }
            })
            props.tabInfo.setPropertiesCallback("renderingProperties", "objectDelayArr", updatedObjectDelayArr);
            props.tabInfo.setCurrStatePropertiesCallback("renderingProperties", "objectDelayArr", updatedObjectDelayArr)
        }, "MapViewportForm/Rendering.saveObjectDelay => onChange")
    }
    const save2DVector = (...args) => {
        runCodeSafely(() => {
            const [point, flugNull, pointType] = args;
            props.tabInfo.setPropertiesCallback("renderingProperties", pointType, point);
            props.tabInfo.setCurrStatePropertiesCallback("renderingProperties", pointType, point);
        }, 'MapViewportForm/Rendering.save2DVector');
    }
    const saveDropDown = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("renderingProperties", event.target.name, event.target.value);
            props.tabInfo.setCurrStatePropertiesCallback("renderingProperties", event.target.name, event.target.value);
        }, 'MapViewportForm/Rendering.saveDropDown');
    }
    //#endregion
    //#region  Handle Functions
    const handleApplyRenderScreenRectToBufferClick = () => {
        runCodeSafely(() => {
            dialogStateService.applyDialogState(["renderingProperties.topLeftVector", "renderingProperties.bottomRightVector",
                "renderingProperties.width", "renderingProperties.height",
                "renderingProperties.selectedBufferPixelFormat", "renderingProperties.bufferRawPitch"]);
            let mcCurrentViewport = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            let currentTabProps = props.tabInfo.properties.renderingProperties;
            let rect = new MapCore.SMcRect(currentTabProps.topLeftVector.x, currentTabProps.topLeftVector.y,
                currentTabProps.bottomRightVector.x, currentTabProps.bottomRightVector.y)
            let image: MapCore.IMcImage;
            image = addJsonViewportService.renderScreenRectToBufferImage(rect, currentTabProps.selectedBufferPixelFormat.theEnum,
                currentTabProps.width, currentTabProps.height, currentTabProps.bufferRawPitch, mcCurrentViewport);
            dispatch(setTypeMapWorldDialogSecond({
                secondDialogHeader: 'Render Screen Rect To Buffer Image',
                secondDialogComponent: <RenderScreenRectToBufferImage image={image} />
            }))
        }, 'MapViewportForm/Rendering.handleApplyRenderScreenRectToBufferClick');
    }
    const handleApplyRenderToBufferModeClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            runMapCoreSafely(() => {
                mcCurrentViewport.SetRenderToBufferMode(props.tabInfo.properties.renderingProperties.isRenderToBufferMode);
            }, 'MapViewportForm/Rendering.handleApplyRenderToBufferModeClick => IMcMapViewport.SetRenderToBufferMode', true)
            dialogStateService.applyDialogState(["renderingProperties.isRenderToBufferMode"]);
        }, 'MapViewportForm/Rendering.handleApplyRenderToBufferModeClick');
    }
    //#endregion

    //#region DOM Functions
    const getRenderScreenRectToBufferFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend="Render Screen Rect To Buffer">
            <div className="form__flex-and-row-between form__items-center">
                <span className="vp-as-camera__wide-s-span"> Top Left:</span>
                <Vector2DFromMap flagNull={{ x: false, y: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_SCREEN} initValue={props.tabInfo.properties.renderingProperties?.topLeftVector} lastPoint={true} saveTheValue={(...args) => save2DVector(...args, 'topLeftVector')} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <span className="vp-as-camera__wide-s-span"> Bottom Right:</span>
                <Vector2DFromMap flagNull={{ x: false, y: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_SCREEN} initValue={props.tabInfo.properties.renderingProperties?.bottomRightVector} lastPoint={true} saveTheValue={(...args) => save2DVector(...args, 'bottomRightVector')} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <label htmlFor='bufferPixelFormat'>Buffer Pixel Format:</label>
                <Dropdown className="form__dropdown-input-width" id='bufferPixelFormat' name='selectedBufferPixelFormat' value={props.tabInfo.properties.renderingProperties?.selectedBufferPixelFormat ?? null} onChange={saveDropDown} options={enumDetails.EPixelFormat} optionLabel="name" />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <label htmlFor='width'>Width:</label>
                <InputNumber name='width' id='width' value={props.tabInfo.properties.renderingProperties?.width ?? null} onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <label htmlFor='height'>Height:</label>
                <InputNumber name='height' id='height' value={props.tabInfo.properties.renderingProperties?.height ?? null} onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <label htmlFor='bufferRawPitch'>Buffer Raw Pitch:</label>
                <InputNumber name='bufferRawPitch' id='bufferRawPitch' value={props.tabInfo.properties.renderingProperties?.bufferRawPitch ?? null} onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row form__justify-end">
                <Button label='Apply' onClick={handleApplyRenderScreenRectToBufferClick} />
            </div>
        </Fieldset>
    }
    //#endregion

    return (
        <div className="form__column-container">
            <Fieldset className="form__column-fieldset" legend="Object Rendering">
                <Fieldset className="form__column-fieldset">
                    <div className="form__flex-and-row-between">
                        <label htmlFor='selectedObjectDelay'>Object Delay Type:</label>
                        <Dropdown className="form__dropdown-input" id='selectedObjectDelay' name='selectedObjectDelay' value={props.tabInfo.properties.renderingProperties ? getEnumValueDetails(props.tabInfo.properties.renderingProperties?.selectedObjectDelay, enumDetails.EObjectDelayType) : null} onChange={saveData} options={enumDetails.EObjectDelayType} optionLabel="name" />
                    </div>
                    <div className="form__flex-and-row-between">
                        <div>
                            <label htmlFor='numberOfUpdatesPerRender'>Number Of Updates Per Render:</label>
                        </div>
                        <div>
                            <InputNumber className="form__narrow-input" name='numberOfUpdatesPerRender' id='numberOfUpdatesPerRender' value={props.tabInfo.properties.renderingProperties?.objectDelayArr
                                .filter(od => od.objectDelayType === props.tabInfo.properties.renderingProperties?.selectedObjectDelay)
                                .map(od => od.numberOfUpdatesPerRender)[0] || null} onValueChange={saveObjectDelay} />
                            <Checkbox name="delayEnabled" inputId="delayEnabled" checked={props.tabInfo.properties.renderingProperties?.objectDelayArr
                                .filter(od => od.objectDelayType === props.tabInfo.properties.renderingProperties?.selectedObjectDelay)
                                .map(od => od.delayEnabled)[0] || null} onChange={saveObjectDelay} />
                            <label htmlFor="delayEnabled">Delay Enabled</label>
                        </div>
                    </div>
                </Fieldset>
                <div className="form__center-aligned-row">
                    <Checkbox name="freezeObjectsVisualization" inputId="freezeObjectsVisualization" checked={props.tabInfo.properties.renderingProperties?.freezeObjectsVisualization} onChange={saveData} />
                    <label htmlFor="freezeObjectsVisualization">Freeze Objects Visualization</label>
                </div>
                <div className="form__flex-and-row-between">
                    <label htmlFor='minNumItemForOverload'>Min Num Item For Overload:</label>
                    <div>
                        <InputNumber className="form__narrow-input" name='minNumItemForOverload' id='minNumItemForOverload' value={props.tabInfo.properties.renderingProperties?.minNumItemForOverload ?? null} onValueChange={saveData} />
                        <Checkbox name="overloadModeEnabled" inputId="overloadModeEnabled" checked={props.tabInfo.properties.renderingProperties?.overloadModeEnabled} onChange={saveData} />
                        <label htmlFor="overloadModeEnabled">Overload Mode Enabled</label>
                    </div>
                </div>
                <div className="form__flex-and-row-between">
                    <label htmlFor='vector3DExtrusionVisibilityMaxScale'>Vector 3D Extrusion Visibility Max Scale:</label>
                    <InputNumber name='vector3DExtrusionVisibilityMaxScale' id='vector3DExtrusionVisibilityMaxScale' value={props.tabInfo.properties.renderingProperties?.vector3DExtrusionVisibilityMaxScale ?? null} onValueChange={saveData} />
                </div>
                <div className="form__flex-and-row-between">
                    <label htmlFor='model3DVisibilityMaxScale'>3D Model Visibility Max Scale:</label>
                    <InputNumber name='model3DVisibilityMaxScale' id='model3DVisibilityMaxScale' value={props.tabInfo.properties.renderingProperties?.model3DVisibilityMaxScale ?? null} onValueChange={saveData} />
                </div>
                <div className="form__flex-and-row-between">
                    <label htmlFor='objectVisibiltyMaxScale'>Object Visibilty Max Scale:</label>
                    <InputNumber name='objectVisibiltyMaxScale' id='objectVisibiltyMaxScale' value={props.tabInfo.properties.renderingProperties?.objectVisibiltyMaxScale ?? null} onValueChange={saveData} />
                </div>
                <div className="form__flex-and-row-between">
                    <label htmlFor='thresholdInPixels'>Threshold In Pixels:</label>
                    <InputNumber name='thresholdInPixels' id='thresholdInPixels' value={props.tabInfo.properties.renderingProperties?.thresholdInPixels ?? null} onValueChange={saveData} />
                </div>
                <div className="form__flex-and-row-between">
                    <label htmlFor='screenSizeTerrainObjectsFactor'>Screen Size Terrain Objects Factor:</label>
                    <InputNumber name='screenSizeTerrainObjectsFactor' id='screenSizeTerrainObjectsFactor' value={props.tabInfo.properties.renderingProperties?.screenSizeTerrainObjectsFactor ?? null} onValueChange={saveData} />
                </div>
            </Fieldset>
            {getRenderScreenRectToBufferFieldset()}
            <Fieldset className="form__row-fieldset form__space-between form__items-center" legend="Render To Buffer Mode">
                <div className="form__flex-and-row form__items-center">
                    <Checkbox name="isRenderToBufferMode" inputId="isRenderToBufferMode" checked={props.tabInfo.properties.renderingProperties?.isRenderToBufferMode} onChange={saveData} />
                    <label htmlFor="isRenderToBufferMode">Render To Buffer Mode</label>
                </div>
                <Button label="Apply" onClick={handleApplyRenderToBufferModeClick} />
            </Fieldset>
        </div>
    )
}
