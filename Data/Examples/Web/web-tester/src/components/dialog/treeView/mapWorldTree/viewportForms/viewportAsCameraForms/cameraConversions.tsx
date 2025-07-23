import { useEffect, useState } from "react";
import { Fieldset } from "primereact/fieldset";
import { InputNumber } from "primereact/inputnumber";
import { Checkbox } from "primereact/checkbox";
import { Button } from "primereact/button";
import { useSelector } from "react-redux";

import { ViewportAsCameraFormTabInfo } from "./viewportAsCamera";
import Vector3DFromMap from "../../../objectWorldTree/shared/Vector3DFromMap";
import { Properties } from "../../../../dialog";
import { AppState } from "../../../../../../redux/combineReducer";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";

export class CameraConversionsPropertiesState implements Properties {

    static getDefault(p: any): CameraConversionsPropertiesState {
        let { currentViewport } = p;

        return {

        }
    }
}
export class CameraConversionsProperties extends CameraConversionsPropertiesState {
    worldPoint: MapCore.SMcVector3D;
    screenPoint: MapCore.SMcVector3D;
    planeNormalPoint: MapCore.SMcVector3D;
    planeLocation: number;
    isIntersectionFound: boolean;

    static getDefault(p: any): CameraConversionsProperties {
        let stateDefaults = super.getDefault(p);

        let defaults: CameraConversionsProperties = {
            ...stateDefaults,
            worldPoint: new MapCore.SMcVector3D(MapCore.v3Zero),
            screenPoint: new MapCore.SMcVector3D(MapCore.v3Zero),
            planeNormalPoint: new MapCore.SMcVector3D(0, 0, 1),
            planeLocation: 0,
            isIntersectionFound: false,
        }
        return defaults;
    }
};

export default function CameraConversions(props: { tabInfo: ViewportAsCameraFormTabInfo }) {
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    let [cameraConversionsLocalProperties, setCameraConversionsLocalProperties] = useState(props.tabInfo.tabProperties);

    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        currentViewport: false,
    })
    //UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            setCameraConversionsLocalProperties(props.tabInfo.tabProperties)
        }, 'ViewportAsCamera/CameraConversions.useEffect => CameraConversionsProperties');
    }, [props.tabInfo.tabProperties])

    useEffect(() => {//example to useEffect in son forms with save mechanism - now not in use.
        runCodeSafely(() => {
            if (isMountedUseEffect.currentViewport) {

            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, currentViewport: true })
            }
        }, 'ViewportAsCamera/CameraConversions.useEffect => currentViewport');
    }, [selectedNodeInTree])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in new CameraConversionsPropertiesState) {
                props.tabInfo.setCurrStatePropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            }
        }, "ViewportAsCamera/CameraConversions.saveData => onChange")
    }

    const saveWorldScreenPoint = (...args) => {
        runCodeSafely(() => {
            const [point, flagNull, sectionPointType] = args;
            props.tabInfo.setPropertiesCallback(sectionPointType, point);
        }, 'MapViewportForm/CameraConversions.saveWorldScreenPoint');
    }
    //Handle Functions
    const handleWorldToScreenClick = () => {
        runCodeSafely(() => {
            let mapCamera: MapCore.IMcMapCamera = selectedNodeInTree.nodeMcContent;
            let screenPoint: MapCore.SMcVector3D = null;
            runMapCoreSafely(() => {
                screenPoint = mapCamera.WorldToScreen(cameraConversionsLocalProperties.worldPoint);
            }, 'ViewportAsCamera/CameraConversions.handleWorldToScreenClick => IMcMapCamera.WorldToScreen', true)
            props.tabInfo.setPropertiesCallback('screenPoint', screenPoint);
        }, 'ViewportAsCamera/CameraConversions.handleWorldToScreenClick');
    }
    const handleScreenToWorldOnPlaneClick = () => {
        runCodeSafely(() => {
            let mapCamera: MapCore.IMcMapCamera = selectedNodeInTree.nodeMcContent;
            let pWorldPoint: { Value?: MapCore.SMcVector3D } = {};
            let isIntersection: boolean = false;
            runMapCoreSafely(() => {
                isIntersection = mapCamera.ScreenToWorldOnPlane(cameraConversionsLocalProperties.screenPoint, pWorldPoint,
                    cameraConversionsLocalProperties.planeLocation, cameraConversionsLocalProperties.planeNormalPoint
                );
            }, 'ViewportAsCamera/CameraConversions.handleWorldToScreenClick => IMcMapCamera.ScreenToWorldOnPlane', true)
            props.tabInfo.setPropertiesCallback('worldPoint', pWorldPoint.Value);
            props.tabInfo.setPropertiesCallback('isIntersectionFound', isIntersection);
        }, 'ViewportAsCamera/CameraConversions.handleScreenToWorldOnPlaneClick');
    }
    const handleScreenToWorldOnTerrainClick = () => {
        runCodeSafely(() => {
            let mapCamera: MapCore.IMcMapCamera = selectedNodeInTree.nodeMcContent;
            let pWorldPoint: { Value?: MapCore.SMcVector3D } = {};
            let isIntersection: boolean = false;
            runMapCoreSafely(() => {
                isIntersection = mapCamera.ScreenToWorldOnTerrain(cameraConversionsLocalProperties.screenPoint, pWorldPoint);
            }, 'ViewportAsCamera/CameraConversions.handleWorldToScreenClick => IMcMapCamera.WorldToScreen', true)
            props.tabInfo.setPropertiesCallback('worldPoint', pWorldPoint.Value);
            props.tabInfo.setPropertiesCallback('isIntersectionFound', isIntersection);
        }, 'ViewportAsCamera/CameraConversions.handleScreenToWorldOnTerrainClick');
    }

    return (
        <div>
            <Fieldset className="form__column-fieldset" legend='Coordinates Conversions [To And From Screen] And Footprint'>
                <div>
                    <div className="form__flex-and-row-between form__items-center">
                        <div className="form__flex-and-row form__items-center">
                            <span className="vp-as-camera__wide-s-span"> World:</span>
                            <Vector3DFromMap flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={cameraConversionsLocalProperties.worldPoint} saveTheValue={(...args) => saveWorldScreenPoint(...args, 'worldPoint')} lastPoint={true} />
                        </div>
                        <Button label="World To Screen" onClick={handleWorldToScreenClick} />
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <div className="form__flex-and-row form__items-center">
                            <span className="vp-as-camera__wide-s-span"> Screen:</span>
                            <Vector3DFromMap flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={cameraConversionsLocalProperties.screenPoint} saveTheValue={(...args) => saveWorldScreenPoint(...args, 'screenPoint')} lastPoint={true} />
                        </div>
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <div className="form__flex-and-row form__items-center">
                            <span className="vp-as-camera__wide-s-span"> Plane normal:</span>
                            <Vector3DFromMap flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={cameraConversionsLocalProperties.planeNormalPoint} saveTheValue={(...args) => saveWorldScreenPoint(...args, 'planeNormalPoint')} lastPoint={true} />
                        </div>
                        <Button label="Screen To World On Plane" onClick={handleScreenToWorldOnPlaneClick} />
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <div className="form__flex-and-row form__items-center">
                            <span className="vp-as-camera__wide-s-span">Plane location:</span>
                            <InputNumber name="planeLocation" value={cameraConversionsLocalProperties.planeLocation} onValueChange={saveData} />
                        </div>
                        <Button label="Screen To World On Terrain" onClick={handleScreenToWorldOnTerrainClick} />
                    </div>
                    <div className="form__flex-and-row form__items-center form__justify-end">
                        <div className="form__flex-and-row form__items-center">
                            <Checkbox name="isIntersectionFound" onChange={saveData} checked={cameraConversionsLocalProperties.isIntersectionFound} />
                            <label className="form__checkbox-div">Is Intersection Found</label>
                        </div>
                    </div>
                </div>
            </Fieldset>
        </div>
    )
}
