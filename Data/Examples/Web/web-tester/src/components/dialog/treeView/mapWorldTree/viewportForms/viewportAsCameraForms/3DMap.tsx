import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { InputNumber } from "primereact/inputnumber";
import { ConfirmDialog } from "primereact/confirmdialog";

import { ViewportAsCameraFormTabInfo } from "./viewportAsCamera";
import { Properties } from "../../../../dialog";
import Vector3DFromMap from "../../../objectWorldTree/shared/Vector3DFromMap";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../../redux/combineReducer";

export class Map3DPropertiesState implements Properties {
    lookAtPoint: MapCore.SMcVector3D;
    forwardVector: MapCore.SMcVector3D;
    isRelativeToOrientation: boolean;
    fieldOfView: number;
    yaw: number;
    pitch: number;
    roll: number;
    min: number;
    max: number;
    enabled: boolean;
    clipMin: number;
    clipMax: number;
    isRenderInTwoSessions: boolean;

    static getDefault(p: any): Map3DPropertiesState {
        return map3DGetDefaultsByUserValues(null, p);
    }
}
export class Map3DProperties extends Map3DPropertiesState {
    isConfirmDialog: boolean;

    static getDefault(p: any): Map3DProperties {
        let stateDefaults = super.getDefault(p);
        let defaults: Map3DProperties = {
            ...stateDefaults,
            isConfirmDialog: false,
        }
        return defaults;
    }
};
export const map3DGetDefaultsByUserValues = (currentProperties: Map3DPropertiesState, p): Map3DPropertiesState => {
    let { currentViewport } = p;
    //#region Declare vars
    let mcCurrentViewport = currentViewport.nodeMcContent as MapCore.IMcMapViewport;
    let forwardVector = MapCore.v3Zero;
    let mcFieldOfView = 0;
    let pYaw: { Value?: number } = { Value: 0 };
    let pPitch: { Value?: number } = { Value: 0 };
    let pRoll: { Value?: number } = { Value: 0 };
    let pMinHeight: { Value?: number } = { Value: 0 };
    let pMaxHeight: { Value?: number } = { Value: 0 };
    let mcIsEnabled = false;
    let pClipMin: { Value?: number } = { Value: 0 };
    let pClipMax: { Value?: number } = { Value: 0 };
    let mcIsRenderInTwoSessions = false;
    let vpType = mcCurrentViewport.GetMapType();
    //#endregion
    if (vpType == MapCore.IMcMapCamera.EMapType.EMT_3D) {
        runMapCoreSafely(() => {
            mcCurrentViewport.GetCameraOrientation(pYaw, pPitch, pRoll);
        }, 'Map3DPropertiesState.getDefault => IMcMapCamera.GetCameraOrientation', true)
        runMapCoreSafely(() => {
            forwardVector = mcCurrentViewport.GetCameraForwardVector()
        }, 'Map3DPropertiesState.getDefault => IMcMapCamera.GetCameraForwardVector', true)
        runMapCoreSafely(() => {
            mcFieldOfView = mcCurrentViewport.GetCameraFieldOfView();
        }, 'Map3DPropertiesState.getDefault => IMcMapCamera.GetCameraFieldOfView', true)
        runMapCoreSafely(() => {
            mcIsEnabled = mcCurrentViewport.GetCameraRelativeHeightLimits(pMinHeight, pMaxHeight);
        }, 'Map3DPropertiesState.getDefault => IMcMapCamera.GetCameraRelativeHeightLimits', true)
        runMapCoreSafely(() => {
            mcIsRenderInTwoSessions = mcCurrentViewport.GetCameraClipDistances(pClipMin, pClipMax);
        }, 'Map3DPropertiesState.getDefault => IMcMapCamera.GetCameraClipDistances', true)
    }

    return {
        lookAtPoint: new MapCore.SMcVector3D(MapCore.v3Zero),
        forwardVector: new MapCore.SMcVector3D(forwardVector.x, forwardVector.y, forwardVector.z),
        isRelativeToOrientation: false,
        fieldOfView: mcFieldOfView,
        yaw: pYaw.Value,
        pitch: pPitch.Value,
        roll: pRoll.Value,
        min: pMinHeight.Value,
        max: pMaxHeight.Value,
        enabled: mcIsEnabled,
        clipMin: pClipMin.Value,
        clipMax: pClipMax.Value,
        isRenderInTwoSessions: mcIsRenderInTwoSessions,
    }
}

export default function Map3D(props: { tabInfo: ViewportAsCameraFormTabInfo }) {
    const dispatch = useDispatch();
    let treeRedux = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    let [map3DLocalProperties, setMap3DLocalProperties] = useState(props.tabInfo.tabProperties);

    const [isMountedUseEffect, setIsMountedUseEffect] = useState({})
    //UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            props.tabInfo.setApplyCallBack(applyAll);
            setMap3DLocalProperties(props.tabInfo.tabProperties)
        }, 'ViewportAsCamera/3DMap.useEffect => Map3DProperties');
    }, [props.tabInfo.tabProperties])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in new Map3DPropertiesState) {
                props.tabInfo.setCurrStatePropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            }
        }, "ViewportAsCamera/3DMap.saveData => onChange")
    }
    const applyAll = () => {
        runCodeSafely(() => {
            // let mapCamera: MapCore.IMcMapCamera = selectedNodeInTree.nodeMcContent;
            // let vpType = mapCamera.GetMapType();
            // if (vpType == MapCore.IMcMapCamera.EMapType.EMT_3D) {
            //     handleForwardVectorSetClick();
            //     handleFieldOfViewSetClick();
            //     handleRotateCameraSetClick();
            //     handleHeightLimitsSetClick();
            //     handleCameraClipDistancesSetClick();
            // }
            // handleLookAtSetClick();
        }, 'ViewportAsCamera/3DMap.applyAll');
    }
    const save3DVector = (...args) => {
        runCodeSafely(() => {
            const [point, flagNull, sectionPointType] = args;
            props.tabInfo.setPropertiesCallback(sectionPointType, point);
            props.tabInfo.setCurrStatePropertiesCallback(sectionPointType, point);
        }, 'MapViewportForm/3DMap.save3DVector');
    }

    //Handle Functions
    const handleLookAtSetClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['lookAtPoint']);
            let mapCamera: MapCore.IMcMapCamera = selectedNodeInTree.nodeMcContent;
            runMapCoreSafely(() => {
                mapCamera.SetCameraLookAtPoint(map3DLocalProperties.lookAtPoint);
            }, 'ViewportAsCamera/CameraConversions.handleLookAtSetClick => IMcMapCamera.SetCameraLookAtPoint', true)
            // loadViewport
            props.tabInfo.loadProperties(selectedNodeInTree);
        }, 'ViewportAsCamera/CameraConversions.handleLookAtSetClick');
    }
    const handleForwardVectorSetClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['forwardVector'])
            let mapCamera: MapCore.IMcMapCamera = selectedNodeInTree.nodeMcContent;
            let vpType = mapCamera.GetMapType();
            if (vpType == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                runMapCoreSafely(() => {
                    mapCamera.SetCameraForwardVector(map3DLocalProperties.forwardVector, map3DLocalProperties.isRelativeToOrientation);
                }, 'ViewportAsCamera/CameraConversions.handleForwardVectorSetClick => IMcMapCamera.SetCameraForwardVector', true)
                // loadViewport
                props.tabInfo.loadProperties(selectedNodeInTree);
            }
            else {
                props.tabInfo.setPropertiesCallback('isConfirmDialog', true)
            }
        }, 'ViewportAsCamera/CameraConversions.handleForwardVectorSetClick');
    }
    const handleFieldOfViewSetClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['fieldOfView'])
            let mapCamera: MapCore.IMcMapCamera = selectedNodeInTree.nodeMcContent;
            let vpType = mapCamera.GetMapType();
            if (vpType == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                runMapCoreSafely(() => {
                    mapCamera.SetCameraFieldOfView(map3DLocalProperties.fieldOfView);
                }, 'ViewportAsCamera/CameraConversions.handleForwardVectorSetClick => IMcMapCamera.SetCameraFieldOfView', true)
                // loadViewport
                props.tabInfo.loadProperties(selectedNodeInTree);
            }
            else {
                props.tabInfo.setPropertiesCallback('isConfirmDialog', true)
            }
        }, 'ViewportAsCamera/CameraConversions.handleFieldOfViewSetClick');
    }
    const handleRotateCameraSetClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['yaw', 'pitch', 'roll'])
            let mapCamera: MapCore.IMcMapCamera = selectedNodeInTree.nodeMcContent;
            let vpType = mapCamera.GetMapType();
            if (vpType == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                runMapCoreSafely(() => {
                    mapCamera.RotateCameraRelativeToOrientation(map3DLocalProperties.yaw, map3DLocalProperties.pitch, map3DLocalProperties.roll);
                }, 'ViewportAsCamera/CameraConversions.handleForwardVectorSetClick => IMcMapCamera.RotateCameraRelativeToOrientation', true)
                // loadViewport
                props.tabInfo.loadProperties(selectedNodeInTree);
            }
            else {
                props.tabInfo.setPropertiesCallback('isConfirmDialog', true)
            }
        }, 'ViewportAsCamera/CameraConversions.handleRotateCameraSetClick');
    }
    const handleHeightLimitsSetClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['min', 'max', 'enabled'])
            let mapCamera: MapCore.IMcMapCamera = selectedNodeInTree.nodeMcContent;
            let vpType = mapCamera.GetMapType();
            if (vpType == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                runMapCoreSafely(() => {
                    mapCamera.SetCameraRelativeHeightLimits(map3DLocalProperties.min, map3DLocalProperties.max, map3DLocalProperties.enabled);
                }, 'ViewportAsCamera/CameraConversions.handleForwardVectorSetClick => IMcMapCamera.SetCameraRelativeHeightLimits', true)
                // loadViewport
                props.tabInfo.loadProperties(selectedNodeInTree);
            }
            else {
                props.tabInfo.setPropertiesCallback('isConfirmDialog', true)
            }
        }, 'ViewportAsCamera/CameraConversions.handleHeightLimitsSetClick');
    }
    const handleCameraClipDistancesSetClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['clipMin', 'clipMax', 'isRenderInTwoSessions'])
            let mapCamera: MapCore.IMcMapCamera = selectedNodeInTree.nodeMcContent;
            let vpType = mapCamera.GetMapType();
            if (vpType == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                runMapCoreSafely(() => {
                    mapCamera.SetCameraClipDistances(map3DLocalProperties.clipMin, map3DLocalProperties.clipMax, map3DLocalProperties.isRenderInTwoSessions);
                }, 'ViewportAsCamera/CameraConversions.handleForwardVectorSetClick => IMcMapCamera.SetCameraClipDistances', true)
                // loadViewport
                props.tabInfo.loadProperties(selectedNodeInTree);
            }
            else {
                props.tabInfo.setPropertiesCallback('isConfirmDialog', true)
            }
        }, 'ViewportAsCamera/CameraConversions.handleCameraClipDistancesSetClick');
    }

    return (
        <div className="form__column-container">
            <div className="form__flex-and-row-between form__items-center">
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-s-span"> Look At:</span>
                    <Vector3DFromMap flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={map3DLocalProperties.lookAtPoint} saveTheValue={(...args) => save3DVector(...args, 'lookAtPoint')} lastPoint={true} />
                </div>
                <Button label="Set" onClick={handleLookAtSetClick} />
            </div>

            <div className="form__flex-and-row-between form__items-center">
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-s-span"> Forward Vector:</span>
                    <Vector3DFromMap flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={map3DLocalProperties.forwardVector} saveTheValue={(...args) => save3DVector(...args, 'forwardVector')} lastPoint={true} />
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox name="isRelativeToOrientation" onChange={saveData} checked={map3DLocalProperties.isRelativeToOrientation} />
                        <span style={{ whiteSpace: 'pre' }} className="form__checkbox-div">Is Relative To Orientation</span>
                    </div>
                </div>
                <Button label="Set" onClick={handleForwardVectorSetClick} />
            </div>

            <div className="form__flex-and-row-between form__items-center">
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-s-span">Field Of View:</span>
                    <InputNumber name="fieldOfView" value={map3DLocalProperties.fieldOfView} onValueChange={saveData} />
                </div>
                <Button label="Set" onClick={handleFieldOfViewSetClick} />
            </div>

            <div className="form__flex-and-row-between form__items-center">
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-l-span"> Rotate Camera Relative To Orientation:</span>
                    <div className="form__flex-and-row form__items-center">
                        <span className="vp-as-camera__r-padding-span">Yaw:</span>
                        <InputNumber className="form__medium-width-input" name="yaw" value={map3DLocalProperties.yaw} onValueChange={saveData} />
                    </div>
                    <div className="form__flex-and-row form__items-center">
                        <span className="vp-as-camera__r-padding-span">Pitch:</span>
                        <InputNumber className="form__medium-width-input" name="pitch" value={map3DLocalProperties.pitch} onValueChange={saveData} />
                    </div>
                    <div className="form__flex-and-row form__items-center">
                        <span className="vp-as-camera__r-padding-span">Roll:</span>
                        <InputNumber className="form__medium-width-input" name="roll" value={map3DLocalProperties.roll} onValueChange={saveData} />
                    </div>
                </div>
                <Button label="Set" onClick={handleRotateCameraSetClick} />
            </div>

            <div className="form__flex-and-row-between form__items-center">
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-l-span"> Height Limits:</span>
                    <div className="form__flex-and-row form__items-center">
                        <span className="vp-as-camera__r-padding-span">Min:</span>
                        <InputNumber className="form__medium-width-input" name="min" value={map3DLocalProperties.min} onValueChange={saveData} />
                    </div>
                    <div className="form__flex-and-row form__items-center">
                        <span className="vp-as-camera__r-padding-span">Max:</span>
                        <InputNumber className="form__medium-width-input" name="max" value={map3DLocalProperties.max} onValueChange={saveData} />
                    </div>
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox name="enabled" onChange={saveData} checked={map3DLocalProperties.enabled} />
                        <span style={{ whiteSpace: 'pre' }} className="form__checkbox-div">Enabled</span>
                    </div>
                </div>
                <Button label="Set" onClick={handleHeightLimitsSetClick} />
            </div>

            <div className="form__flex-and-row-between form__items-center">
                <div className="form__flex-and-row form__items-center">
                    <span className="vp-as-camera__wide-l-span"> Camera Clip Distances:</span>
                    <div className="form__flex-and-row form__items-center">
                        <span className="vp-as-camera__r-padding-span">Min:</span>
                        <InputNumber className="form__medium-width-input" name="clipMin" value={map3DLocalProperties.clipMin} onValueChange={saveData} />
                    </div>
                    <div className="form__flex-and-row form__items-center">
                        <span className="vp-as-camera__r-padding-span">Max:</span>
                        <InputNumber className="form__medium-width-input" name="clipMax" value={map3DLocalProperties.clipMax} onValueChange={saveData} />
                    </div>
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox name="isRenderInTwoSessions" onChange={saveData} checked={map3DLocalProperties.isRenderInTwoSessions} />
                        <span style={{ whiteSpace: 'pre' }} className="form__checkbox-div">Render In Two Sessions</span>
                    </div>
                </div>
                <Button label="Set" onClick={handleCameraClipDistancesSetClick} />
            </div>

            <ConfirmDialog
                contentClassName='form__confirm-dialog-content'
                message='Only for 3D map.'
                header=''
                footer={<div></div>}
                visible={map3DLocalProperties.isConfirmDialog}
                onHide={e => { props.tabInfo.setPropertiesCallback('isConfirmDialog', false) }}
            />
        </div>
    )
}
