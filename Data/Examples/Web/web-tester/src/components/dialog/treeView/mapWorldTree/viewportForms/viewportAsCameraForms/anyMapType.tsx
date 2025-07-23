import { useEffect, useState } from "react";
import { InputText } from "primereact/inputtext";
import { Fieldset } from "primereact/fieldset";
import { useDispatch, useSelector } from "react-redux";
import { Button } from "primereact/button";
import { Checkbox, CheckboxChangeEvent } from "primereact/checkbox";
import { InputNumber } from "primereact/inputnumber";
import { Dropdown } from "primereact/dropdown";

import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { ViewportAsCameraFormTabInfo } from "./viewportAsCamera";
import Vector2DFromMap from "../../../objectWorldTree/shared/Vector2DFromMap";
import Vector3DFromMap from "../../../objectWorldTree/shared/Vector3DFromMap";
import { Properties } from "../../../../dialog";
import RectFromMapCtrl, { RectFromMapCtrlInfo } from "../../../../shared/RectFromMapCtrl";
import { AppState } from "../../../../../../redux/combineReducer";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";

export class AnyMapTypePropertiesState implements Properties {
    cameraPosition: MapCore.SMcVector3D;
    cameraUpVector: MapCore.SMcVector3D;
    yaw: number;
    pitch: number;
    roll: number;
    moveRelativeToOrientation: MapCore.SMcVector3D;
    isRelativePosition: boolean;
    isRelativeToOrientationUp: boolean;
    isRelativeOrientation: boolean;
    isXYDirectionOnly: boolean;
    scaleWorldPoint: MapCore.SMcVector3D;
    cameraScale: number;
    bottomRightVector: MapCore.SMcVector2D;
    topLeftVector: MapCore.SMcVector2D;
    selectedOperation: { name: string; code: number; theEnum: any; }
    worldRectangleBox: MapCore.SMcBox;
    rectangleYaw: number;
    screenMargin: number;
    offset: MapCore.SMcVector2D;
    privotPoint: MapCore.SMcVector3D;
    deltaYaw: number;
    deltaPitch: number;
    deltaRoll: number;
    isRotateRelative: boolean;

    static getDefault(p: any): AnyMapTypePropertiesState {
        return anyMapTypeGetDefaultsByUserValues(null, p);
    }
}
export class AnyMapTypeProperties extends AnyMapTypePropertiesState {
    mapType: string;
    showRectangle: boolean;

    static getDefault(p: any): AnyMapTypeProperties {
        let stateDefaults = super.getDefault(p);

        let { currentViewport } = p;
        let mapCamera: MapCore.IMcMapCamera = currentViewport.nodeMcContent;
        let vpTypeFullName = mapCamera.GetMapType().constructor.name;
        let arr = vpTypeFullName.split('_');
        let vpTypeFinalName = [arr[arr.length - 2], arr[arr.length - 1]].join('_');

        let defaults: AnyMapTypeProperties = {
            ...stateDefaults,
            mapType: `${vpTypeFinalName}`,
            showRectangle: false,
        }
        return defaults;
    }
};
export const anyMapTypeGetDefaultsByUserValues = (currentProperties: AnyMapTypePropertiesState, p): AnyMapTypePropertiesState => {
    let { currentViewport, cursorPos } = p;
    //#region Declare vars
    let mcCurrentViewport = currentViewport.nodeMcContent as MapCore.IMcMapViewport;
    let mcCameraPos = null;
    let yaw: { Value?: number } = {}
    let pitch: { Value?: number } = { Value: 0 }
    let roll: { Value?: number } = { Value: 0 }
    let vpType = mcCurrentViewport.GetMapType();
    let mcCameraUpVector = null;
    let mcCameraScale = null;
    let ESetVisibleArea3DOperationList = getEnumDetailsList(MapCore.IMcMapCamera.ESetVisibleArea3DOperation)
    let worldRectangleBox: MapCore.SMcBox = null;
    let mcOffset = null;
    //#endregion
    runCodeSafely(() => {
        if (!currentProperties || !currentProperties.isRelativePosition) {
            runMapCoreSafely(() => {
                mcCameraPos = mcCurrentViewport.GetCameraPosition();
            }, 'ViewportAsCamera/AnyMapType.getDefault => IMcMapCamera.GetCameraPosition', true)
        }
        if (!currentProperties || !currentProperties.isRelativeOrientation) {
            runMapCoreSafely(() => {
                vpType == MapCore.IMcMapCamera.EMapType.EMT_2D ?
                    mcCurrentViewport.GetCameraOrientation(yaw) :
                    mcCurrentViewport.GetCameraOrientation(yaw, pitch, roll);
            }, 'ViewportAsCamera/AnyMapType.getDefault => IMcMapCamera.GetCameraOrientation', true)
        }
        runMapCoreSafely(() => {
            mcCameraUpVector = mcCurrentViewport.GetCameraUpVector();
        }, 'ViewportAsCamera/AnyMapType.getDefault => IMcMapCamera.GetCameraUpVector', true)
        let mcFieldOfView = null;
        if (vpType == MapCore.IMcMapCamera.EMapType.EMT_3D) {
            runMapCoreSafely(() => {
                mcFieldOfView = mcCurrentViewport.GetCameraFieldOfView();
            }, 'ViewportAsCamera/AnyMapType.getDefault => IMcMapCamera.GetCameraFieldOfView', true)
        }
        if (vpType == MapCore.IMcMapCamera.EMapType.EMT_2D || (vpType == MapCore.IMcMapCamera.EMapType.EMT_3D && mcFieldOfView == 0)) {
            runMapCoreSafely(() => {
                mcCameraScale = mcCurrentViewport.GetCameraScale();
            }, 'ViewportAsCamera/AnyMapType.getDefault => IMcMapCamera.GetCameraScale', true)
        }
        if (vpType == MapCore.IMcMapCamera.EMapType.EMT_2D) {// || (vpType == MapCore.IMcMapCamera.EMapType.EMT_3D && mcFieldOfView == 0)) {
            runMapCoreSafely(() => {
                worldRectangleBox = mcCurrentViewport.GetCameraWorldVisibleArea(0, 0);
            }, 'ViewportAsCamera/AnyMapType.getDefault => IMcMapCamera.GetCameraWorldVisibleArea', true)
        }
        runMapCoreSafely(() => {
            mcOffset = mcCurrentViewport.GetCameraCenterOffset();
        }, 'ViewportAsCamera/AnyMapType.getDefault => IMcMapCamera.GetCameraCenterOffset', true)
    }, 'ViewportAsCamera/AnyMapType.anyMapTypeGetDefaultsByUserValues')

    return {
        cameraPosition: mcCameraPos,
        cameraUpVector: mcCameraUpVector,
        yaw: yaw.Value,
        pitch: pitch.Value,
        roll: roll.Value,
        moveRelativeToOrientation: new MapCore.SMcVector3D(MapCore.v3Zero),
        isRelativePosition: false,
        isRelativeToOrientationUp: false,
        isRelativeOrientation: false,
        isXYDirectionOnly: false,
        scaleWorldPoint: new MapCore.SMcVector3D(MapCore.v3Zero),
        cameraScale: mcCameraScale,
        bottomRightVector: new MapCore.SMcVector2D(MapCore.v3Zero),
        topLeftVector: new MapCore.SMcVector2D(MapCore.v3Zero),
        selectedOperation: getEnumValueDetails(MapCore.IMcMapCamera.ESetVisibleArea3DOperation.ESVAO_ROTATE_AND_MOVE, ESetVisibleArea3DOperationList),
        rectangleYaw: 0,
        screenMargin: 0,
        worldRectangleBox: worldRectangleBox,
        offset: mcOffset,
        privotPoint: new MapCore.SMcVector3D(MapCore.v3Zero),
        deltaYaw: 0,
        deltaPitch: 0,
        deltaRoll: 0,
        isRotateRelative: false,

    }
}

export default function AnyMapType(props: { tabInfo: ViewportAsCameraFormTabInfo }) {
    const dispatch = useDispatch();
    let treeRedux = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const cursorPos = useSelector((state: AppState) => state.mapWindowReducer.cursorPos);
    let [anyMapTypeLocalProperties, setAnyMapTypeLocalProperties] = useState(props.tabInfo.tabProperties);
    let ESetVisibleArea3DOperationList = getEnumDetailsList(MapCore.IMcMapCamera.ESetVisibleArea3DOperation)

    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        currentViewport: false,
    })
    //UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            props.tabInfo.setApplyCallBack(applyAll);
            setAnyMapTypeLocalProperties(props.tabInfo.tabProperties)
        }, 'ViewportAsCamera/AnyMapType.useEffect => anyMapTypeProperties');
    }, [props.tabInfo.tabProperties])

    //#region Save Functions
    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in new AnyMapTypePropertiesState) {
                props.tabInfo.setCurrStatePropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            }
        }, "ViewportAsCamera/AnyMapType.saveData => onChange")
    }
    const applyAll = () => {
        runCodeSafely(() => {

        }, 'ViewportAsCamera/AnyMapType.applyAll');
    }
    const save3DVector = (...args) => {
        runCodeSafely(() => {
            const [point, flagNull, sectionPointType] = args;
            props.tabInfo.setPropertiesCallback(sectionPointType, point);
            props.tabInfo.setCurrStatePropertiesCallback(sectionPointType, point);
        }, 'MapViewportForm/AnyMapType.save3DVector');
    }
    const save2DVector = (...args) => {
        runCodeSafely(() => {
            const [point, flugNull, pointType] = args;
            props.tabInfo.setPropertiesCallback(pointType, point);
            props.tabInfo.setCurrStatePropertiesCallback(pointType, point);
        }, 'MapViewportForm/AnyMapType.save2DVector');
    }
    //#endregion

    //#region Handle Functions
    const handleIsRelativeOrientationChange = (event: CheckboxChangeEvent) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback('isRelativeOrientation', event.checked);
            props.tabInfo.setCurrStatePropertiesCallback('isRelativeOrientation', event.checked);
            if (event.checked) {
                props.tabInfo.setPropertiesCallback('yaw', 0);
                props.tabInfo.setPropertiesCallback('pich', 0);
                props.tabInfo.setPropertiesCallback('roll', 0);
            }
            else {
                // loadViewport
                props.tabInfo.loadProperties(selectedNodeInTree);
            }
        }, 'MapViewportForm/AnyMapType.handleIsRelativeOrientationChange');
    }
    const handleIsRelativeCameraPosChange = (event: CheckboxChangeEvent) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback('isRelativePosition', event.checked);
            props.tabInfo.setCurrStatePropertiesCallback('isRelativePosition', event.checked);
            if (event.checked) {
                props.tabInfo.setPropertiesCallback('cameraPosition', new MapCore.SMcVector3D(0, 0, 0));
            }
            else {
                // loadViewport
                props.tabInfo.loadProperties(selectedNodeInTree);
            }
        }, 'MapViewportForm/AnyMapType.handleIsRelativeCameraPosChange');
    }
    const handleSetCameraPositionClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['cameraPosition', 'isRelativePosition']);
            let viewport = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            runMapCoreSafely(() => {
                viewport.SetCameraPosition(anyMapTypeLocalProperties.cameraPosition, anyMapTypeLocalProperties.isRelativePosition);
            }, 'MapViewportForm/AnyMapType.handleSetCameraPositionClick => IMcMapCamera.SetCameraPosition', true)
            // loadViewport
            props.tabInfo.loadProperties(selectedNodeInTree);
        }, 'MapViewportForm/AnyMapType.handleSetCameraPositionClick');
    }
    const handleSetCameraUpVectorClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['cameraUpVector', 'isRelativeToOrientationUp']);
            let currentVp = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            runMapCoreSafely(() => {
                currentVp.SetCameraUpVector(anyMapTypeLocalProperties.cameraUpVector, anyMapTypeLocalProperties.isRelativeToOrientationUp)
            }, 'MapViewportForm/AnyMapType.handleSetCameraUpVectorClick => IMcMapCamera.SetCameraUpVector', true)
            // loadViewport
            props.tabInfo.loadProperties(selectedNodeInTree);
        }, 'MapViewportForm/AnyMapType.handleSetCameraUpVectorClick');
    }
    const handleSetCameraOrientationClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['yaw', 'pitch', 'roll', 'isRelativeOrientation']);
            let currentVp = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            let vpType = currentVp.GetMapType();
            let finalPitch = vpType == MapCore.IMcMapCamera.EMapType.EMT_3D ? anyMapTypeLocalProperties.pitch : null;
            let finalRoll = vpType == MapCore.IMcMapCamera.EMapType.EMT_3D ? anyMapTypeLocalProperties.roll : null;
            runMapCoreSafely(() => {
                currentVp.SetCameraOrientation(anyMapTypeLocalProperties.yaw,
                    finalPitch,
                    finalRoll,
                    anyMapTypeLocalProperties.isRelativeOrientation);
            }, 'MapViewportForm/AnyMapType.handleSetCameraOrientationClick => IMcMapCamera.SetCameraOrientation', true)
            // loadViewport
            props.tabInfo.loadProperties(selectedNodeInTree);
        }, 'MapViewportForm/AnyMapType.handleSetCameraOrientationClick');
    }
    const handleSetMoveRelativeClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['moveRelativeToOrientation', 'isXYDirectionOnly']);
            let currentVp = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            runMapCoreSafely(() => {
                currentVp.MoveCameraRelativeToOrientation(anyMapTypeLocalProperties.moveRelativeToOrientation, anyMapTypeLocalProperties.isXYDirectionOnly)
            }, 'MapViewportForm/AnyMapType.handleSetMoveRelativeClick => IMcMapCamera.MoveCameraRelativeToOrientation', true)
            // loadViewport
            props.tabInfo.loadProperties(selectedNodeInTree);
        }, 'MapViewportForm/AnyMapType.handleSetMoveRelativeClick');
    }
    const handleGetCameraScale = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['cameraScale', 'scaleWorldPoint']);
            let currentVp = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            let mcScale = null;
            runMapCoreSafely(() => {
                mcScale = currentVp.GetCameraScale(anyMapTypeLocalProperties.scaleWorldPoint);
            }, 'MapViewportForm/AnyMapType.handleGetCameraScale => IMcMapCamera.GetCameraScale', true)
            props.tabInfo.setPropertiesCallback('cameraScale', mcScale);
        }, 'MapViewportForm/AnyMapType.handleGetCameraScale');
    }
    const handleSetCameraScale = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['cameraScale', 'scaleWorldPoint']);
            let currentVp = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            let vpType = currentVp.GetMapType();
            runMapCoreSafely(() => {
                vpType == MapCore.IMcMapCamera.EMapType.EMT_3D ?
                    currentVp.SetCameraScale(anyMapTypeLocalProperties.cameraScale, anyMapTypeLocalProperties.scaleWorldPoint) :
                    currentVp.SetCameraScale(anyMapTypeLocalProperties.cameraScale);
            }, 'MapViewportForm/AnyMapType.handleSetCameraScale => IMcMapCamera.SetCameraScale', true)
            // loadViewport
            props.tabInfo.loadProperties(selectedNodeInTree);
        }, 'MapViewportForm/AnyMapType.handleSetCameraScale');
    }
    const handleSetScreenVisibleClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['topLeftVector', 'bottomRightVector', 'selectedOperation']);
            let vpType = selectedNodeInTree.nodeMcContent.GetMapType();
            if (vpType == MapCore.IMcMapCamera.EMapType.EMT_2D) {
                let currentVp = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
                let rectangle = new MapCore.SMcRect(anyMapTypeLocalProperties.topLeftVector.x,
                    anyMapTypeLocalProperties.topLeftVector.y,
                    anyMapTypeLocalProperties.bottomRightVector.x,
                    anyMapTypeLocalProperties.bottomRightVector.y)
                runMapCoreSafely(() => {
                    currentVp.SetCameraScreenVisibleArea(rectangle, anyMapTypeLocalProperties.selectedOperation.theEnum)//MapCore.IMcMapCamera.ESetVisibleArea3DOperation.ESVAO_ROTATE_AND_MOVE);
                }, 'MapViewportForm/AnyMapType.handleSetScreenVisibleClick => IMcMapCamera.SetCameraScreenVisibleArea', true)
                // loadViewport
                props.tabInfo.loadProperties(selectedNodeInTree);
            }
        }, 'MapViewportForm/AnyMapType.handleSetScreenVisibleClick');
    }
    const handleSetCamerWorldAreaScaleClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['screenMargin', 'rectangleYaw', 'worldRectangleBox']);
            let currentVp = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            runMapCoreSafely(() => {
                currentVp.SetCameraWorldVisibleArea(anyMapTypeLocalProperties.worldRectangleBox, anyMapTypeLocalProperties.screenMargin, anyMapTypeLocalProperties.rectangleYaw);
            }, 'MapViewportForm/AnyMapType.handleSetCamerWorldAreaScaleClick => IMcMapCamera.SetCameraWorldVisibleArea', true)
            // loadViewport
            props.tabInfo.loadProperties(selectedNodeInTree);
        }, 'MapViewportForm/AnyMapType.handleSetCamerWorldAreaScaleClick');
    }
    const handleGetCamerWorldAreaScaleClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['screenMargin', 'rectangleYaw']);
            let currentVp = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            let vpType = selectedNodeInTree.nodeMcContent.GetMapType();
            let worldVisibleArea: MapCore.SMcBox = null;
            runMapCoreSafely(() => {
                worldVisibleArea = currentVp.GetCameraWorldVisibleArea(anyMapTypeLocalProperties.screenMargin, anyMapTypeLocalProperties.rectangleYaw);
            }, 'MapViewportForm/AnyMapType.handleGetCamerWorldAreaScaleClick => IMcMapCamera.GetCameraWorldVisibleArea', true)
            props.tabInfo.setPropertiesCallback('worldRectangleBox', worldVisibleArea);
        }, 'MapViewportForm/AnyMapType.handleGetCamerWorldAreaScaleClick');
    }
    const handleSetCamerOffsetClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['offset']);
            let currentVp = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            runMapCoreSafely(() => {
                currentVp.SetCameraCenterOffset(anyMapTypeLocalProperties.offset);
            }, 'MapViewportForm/AnyMapType.handleSetCamerOffsetClick => IMcMapCamera.SetCameraCenterOffset', true)
        }, 'MapViewportForm/AnyMapType.handleSetCamerOffsetClick');
    }
    const handleSetRotateCameraAroundWorldClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['privotPoint', 'deltaYaw', 'deltaPitch', 'deltaRoll', 'isRotateRelative']);
            let currentVp = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            runMapCoreSafely(() => {
                currentVp.RotateCameraAroundWorldPoint(anyMapTypeLocalProperties.privotPoint,
                    anyMapTypeLocalProperties.deltaYaw,
                    anyMapTypeLocalProperties.deltaPitch,
                    anyMapTypeLocalProperties.deltaRoll,
                    anyMapTypeLocalProperties.isRotateRelative
                );
            }, 'MapViewportForm/AnyMapType.handleSetRotateCameraAroundWorldClick => IMcMapCamera.SetCameraCenterOffset', true)
            // loadViewport
            props.tabInfo.loadProperties(selectedNodeInTree);
        }, 'MapViewportForm/AnyMapType.handleSetRotateCameraAroundWorldClick');
    }
    //#endregion

    //#region DOM Functions
    const getGeneralFields = () => {
        return <div>
            <div className="form__flex-and-row form__items-center form__disabled">
                <span className="vp-as-camera__wide-s-span">Map Type:</span>
                <InputText value={anyMapTypeLocalProperties.mapType} onChange={saveData} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-m-span"> Camera Position:</span>
                    <Vector3DFromMap flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={anyMapTypeLocalProperties.cameraPosition} saveTheValue={(...args) => save3DVector(...args, 'cameraPosition')} lastPoint={true} />
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox name="isRelativePosition" onChange={handleIsRelativeCameraPosChange} checked={anyMapTypeLocalProperties.isRelativePosition} />
                        <span style={{ whiteSpace: 'pre' }} className="form__checkbox-div">Is Relative</span>
                    </div>
                </div>
                <Button label="Set" onClick={handleSetCameraPositionClick} />
            </div>

            <div className="form__flex-and-row-between form__items-center">
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-m-span">Camera Up Vector:</span>
                    <Vector3DFromMap flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={anyMapTypeLocalProperties.cameraUpVector} saveTheValue={(...args) => save3DVector(...args, 'cameraUpVector')} lastPoint={true} />
                    <div style={{ width: '23%' }} className="form__flex-and-row form__items-center">
                        <Checkbox name="isRelativeToOrientationUp" onChange={saveData} checked={anyMapTypeLocalProperties.isRelativeToOrientationUp} />
                        <span className="form__checkbox-div">Relative To Orientation</span>
                    </div>
                </div>
                <Button label="Set" onClick={handleSetCameraUpVectorClick} />
            </div>

            <div className="form__flex-and-row-between form__items-center">
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-m-span"> Camera Orientation:</span>
                    <div className="form__row-container">
                        <div className="form__flex-and-row form__items-center">
                            <span className="vp-as-camera__r-padding-span">Yaw:</span>
                            <InputNumber className="form__medium-width-input" name="yaw" value={anyMapTypeLocalProperties.yaw} onValueChange={saveData} />
                        </div>
                        <div className="form__flex-and-row form__items-center">
                            <span className="vp-as-camera__r-padding-span">Pitch:</span>
                            <InputNumber className="form__medium-width-input" name="pitch" value={anyMapTypeLocalProperties.pitch} onValueChange={saveData} />
                        </div>
                        <div className="form__flex-and-row form__items-center">
                            <span className="vp-as-camera__r-padding-span">Roll:</span>
                            <InputNumber className="form__medium-width-input" name="roll" value={anyMapTypeLocalProperties.roll} onValueChange={saveData} />
                        </div>
                    </div>
                    <div style={{ marginLeft: `${globalSizeFactor * 4.4}vh` }} className="form__flex-and-row form__items-center">
                        <Checkbox name="isRelativeOrientation" onChange={handleIsRelativeOrientationChange} checked={anyMapTypeLocalProperties.isRelativeOrientation} />
                        <span style={{ whiteSpace: 'pre' }} className="form__checkbox-div">Is Relative</span>
                    </div>
                </div>
                <Button label="Set" onClick={handleSetCameraOrientationClick} />
            </div>

            <div className="form__flex-and-row-between form__items-center">
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-m-span"> Move Relative To Orientation:</span>
                    <Vector3DFromMap flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={anyMapTypeLocalProperties.moveRelativeToOrientation} saveTheValue={(...args) => save3DVector(...args, 'moveRelativeToOrientation')} lastPoint={true} />
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox name="isXYDirectionOnly" onChange={saveData} checked={anyMapTypeLocalProperties.isXYDirectionOnly} />
                        <span style={{ whiteSpace: 'pre' }} className="form__checkbox-div">XY Direction Only</span>
                    </div>
                </div>
                <Button label="Set" onClick={handleSetMoveRelativeClick} />
            </div>

        </div>
    }
    const getCamerScaleFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend='Camera Scale'>
            <div className="form__flex-and-row form__items-center">
                <span className="vp-as-camera__wide-s-span">Camera Scale:</span>
                <InputText name="cameraScale" value={anyMapTypeLocalProperties.cameraScale} onChange={saveData} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div className={`form__flex-and-row form__items-center ${selectedNodeInTree.nodeMcContent.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_2D && 'form__disabled'}`}>
                    <div className="form__flex-and-row form__items-center">
                        <span className="vp-as-camera__wide-l-span"> In 3D Enter World Point:</span>
                        <Vector3DFromMap flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={anyMapTypeLocalProperties.scaleWorldPoint} saveTheValue={(...args) => save3DVector(...args, 'scaleWorldPoint')} lastPoint={true} />
                    </div>
                    <Button label="Get" onClick={handleGetCameraScale} />
                </div>
                <Button label="Set" onClick={handleSetCameraScale} />
            </div>
        </Fieldset>
    }
    const getScreenVisibleAreaFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend='Screen Visible Area'>
            <div className="form__flex-and-row form__items-center">
                <span className="vp-as-camera__wide-s-span"> Top Left:</span>
                <Vector2DFromMap pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={anyMapTypeLocalProperties.topLeftVector} lastPoint={true} saveTheValue={(...args) => save2DVector(...args, 'topLeftVector')} />
            </div>
            <div className="form__flex-and-row-between">
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-s-span"> Bottom Right:</span>
                    <Vector2DFromMap pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={anyMapTypeLocalProperties.bottomRightVector} lastPoint={true} saveTheValue={(...args) => save2DVector(...args, 'bottomRightVector')} />
                </div>
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-s-span"> Operation:</span>
                    <Dropdown className="vp-as-camera__dropdown" name="selectedOperation" value={anyMapTypeLocalProperties.selectedOperation} onChange={saveData} options={ESetVisibleArea3DOperationList} optionLabel="name" />
                    <Button label="Set" onClick={handleSetScreenVisibleClick} />
                </div>
            </div>
        </Fieldset>
    }
    const getCamerWorldAreaVisibleFieldset = () => {
        let mcCurrentViewport = selectedNodeInTree.nodeMcContent;
        const rectInfo: RectFromMapCtrlInfo = {
            rectangleBox: props.tabInfo.tabProperties.worldRectangleBox,
            showRectangle: props.tabInfo.tabProperties.showRectangle,
            setProperty: (name: string, value: any) => { props.tabInfo.setPropertiesCallback(name, value) },
            readOnly: false,
            currViewport: mcCurrentViewport,
            rectCoordSystem: MapCore.EMcPointCoordSystem.EPCS_WORLD,
        }
        return <Fieldset className="form__row-fieldset form__space-between" legend='Camera World Visible Area'>
            <Fieldset legend='World Visible Area'>
                <RectFromMapCtrl info={rectInfo} />
            </Fieldset>
            <div className="form__column-container form__justify-space-around">
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-s-span">Rectangle Yaw:</span>
                    <InputNumber className="form__narrow-input" name="rectangleYaw" value={anyMapTypeLocalProperties.rectangleYaw} onValueChange={saveData} />
                </div>
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-s-span">Screen Margin:</span>
                    <InputNumber className="form__narrow-input" name="screenMargin" value={anyMapTypeLocalProperties.screenMargin} onValueChange={saveData} />
                </div>
                <div className="form__row-container form__justify-end">
                    <Button label="Get" onClick={handleGetCamerWorldAreaScaleClick} />
                    <Button label="Set" onClick={handleSetCamerWorldAreaScaleClick} />
                </div>
            </div>
        </Fieldset>
    }
    const getCamerCenterOffsetFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend='Camera Center Offset'>
            <div className="form__flex-and-row-between form__items-center">
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-s-span"> Offset:</span>
                    <Vector2DFromMap pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={anyMapTypeLocalProperties.offset} lastPoint={true} saveTheValue={(...args) => save2DVector(...args, 'offset')} />
                </div>
                <Button label="Set" onClick={handleSetCamerOffsetClick} />
            </div>
        </Fieldset>
    }
    const getRotateCameraAroundWorldPointFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend='Rotate Camera Around World Point'>
            <div className="form__flex-and-row form__items-center">
                <span className="vp-as-camera__wide-s-span"> Privot Point:</span>
                <Vector3DFromMap flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={anyMapTypeLocalProperties.privotPoint} saveTheValue={(...args) => save3DVector(...args, 'privotPoint')} lastPoint={true} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-s-span">Delta Yaw:</span>
                    <InputNumber className="form__narrow-input" name="deltaYaw" value={anyMapTypeLocalProperties.deltaYaw} onValueChange={saveData} />
                </div>
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-s-span">Delta Pitch:</span>
                    <InputNumber className="form__narrow-input" name="deltaPitch" value={anyMapTypeLocalProperties.deltaPitch} onValueChange={saveData} />
                </div>
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-s-span">Delta Roll:</span>
                    <InputNumber className="form__narrow-input" name="deltaRoll" value={anyMapTypeLocalProperties.deltaRoll} onValueChange={saveData} />
                </div>
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <div className="form__flex-and-row form__items-center">
                    <Checkbox name="isRotateRelative" checked={anyMapTypeLocalProperties.isRotateRelative} />
                    <span style={{ whiteSpace: 'pre' }} className="form__checkbox-div">Relative To Orientation</span>
                </div>
                <Button label="Set" onClick={handleSetRotateCameraAroundWorldClick} />
            </div>
        </Fieldset>
    }
    //#endregion

    return (
        <div>
            <div className="form__column-container">
                {getGeneralFields()}
                {getCamerScaleFieldset()}
                {getScreenVisibleAreaFieldset()}
                {getCamerWorldAreaVisibleFieldset()}
                {getCamerCenterOffsetFieldset()}
                {getRotateCameraAroundWorldPointFieldset()}
            </div>
        </div>
    )
}
