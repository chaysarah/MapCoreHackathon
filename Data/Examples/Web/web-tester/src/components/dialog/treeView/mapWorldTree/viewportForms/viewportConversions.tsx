import { useEffect, useState } from "react";
import { Button } from "primereact/button";
import { Fieldset } from "primereact/fieldset";
import { ListBox } from "primereact/listbox";
import { useDispatch, useSelector } from "react-redux";

import { MapCoreData } from 'mapcore-lib';
import { MapViewportFormTabInfo } from "./mapViewportForm";
import Vector3DFromMap from "../../objectWorldTree/shared/Vector3DFromMap";
import { Properties } from "../../../dialog";
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";

export class ViewportConversionsPropertiesState implements Properties {
    static getDefault(p: any): ViewportConversionsPropertiesState {
        return {

        }
    }
}
export class ViewportConversionsProperties extends ViewportConversionsPropertiesState {
    viewportCoordinate: MapCore.SMcVector3D;
    overlayManagerCoordinate: MapCore.SMcVector3D;
    imageWorldCoordinate: MapCore.SMcVector3D;
    viewportWorldCoordinate: MapCore.SMcVector3D;
    imageCalcArr: MapCore.IMcImageCalc[];
    selectedImageCalc: MapCore.IMcImageCalc;

    static getDefault(p: any): ViewportConversionsProperties {
        let stateDefaults = super.getDefault(p);
        let defaults: ViewportConversionsProperties = {
            ...stateDefaults,
            viewportCoordinate: new MapCore.SMcVector3D(0, 0, 0),
            overlayManagerCoordinate: new MapCore.SMcVector3D(0, 0, 0),
            imageWorldCoordinate: new MapCore.SMcVector3D(0, 0, 0),
            viewportWorldCoordinate: new MapCore.SMcVector3D(0, 0, 0),
            imageCalcArr: MapCoreData.imageCalcArr,
            selectedImageCalc: null,
        }
        return defaults;
    }
};
export default function ViewportConversions(props: { tabInfo: MapViewportFormTabInfo }) {
    const dispatch = useDispatch();
    let treeRedux = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    let viewport: MapCore.IMcMapViewport = props.tabInfo.currentViewport.nodeMcContent;
    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        viewportConversionsProperties: false,
        currentViewport: false,
    })
    //UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            // initialize first time the tab is loaded only
            if (!props.tabInfo.properties.viewportConversionsProperties) {
                props.tabInfo.setInitialStatePropertiesCallback("viewportConversionsProperties", null, ViewportConversionsPropertiesState.getDefault({}));
                props.tabInfo.setPropertiesCallback("viewportConversionsProperties", null, ViewportConversionsProperties.getDefault({}));
            }
        }, 'MapViewportForm/ViewportConversions.useEffect');
    }, [])

    // useEffect(() => {
    //     runCodeSafely(() => {
    //         props.tabInfo.setApplyCallBack("ViewportConversions", applyAll);
    //     }, 'MapViewportForm/ViewportConversions.useEffect => viewportConversionsProperties');
    // }, [props.tabInfo.properties.viewportConversionsProperties])

    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.currentViewport) {
                props.tabInfo.setInitialStatePropertiesCallback("viewportConversionsProperties", null, ViewportConversionsPropertiesState.getDefault({}));
                props.tabInfo.setPropertiesCallback("viewportConversionsProperties", null, ViewportConversionsProperties.getDefault({}));
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, currentViewport: true })
            }
        }, 'MapViewportForm/ViewportConversions.useEffect => currentViewport');
    }, [props.tabInfo.currentViewport])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("viewportConversionsProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
        }, "MapViewportForm/ViewportConversions.saveData => onChange")
    }
    // const applyAll = () => {
    //     runCodeSafely(() => {

    //     }, 'MapViewportForm/ViewportConversions.applyAll');
    // }

    //DOM Functions
    const onOMToViewportClick = () => {
        runCodeSafely(() => {
            let result: MapCore.SMcVector3D;
            runMapCoreSafely(() => {
                result = viewport.OverlayManagerToViewportWorld(props.tabInfo.properties.viewportConversionsProperties?.overlayManagerCoordinate);
            }, "MapViewportForm/ViewportConversions/onOMToViewportWorldClick => OverlayManagerToViewportWorld", true)
            props.tabInfo.setPropertiesCallback("viewportConversionsProperties", "viewportCoordinate", result)
        }, "MapViewportForm/ViewportConversions/onOMToViewportWorldClick => onClick")
    }
    const onViewportToOMClick = () => {
        runCodeSafely(() => {
            let result: MapCore.SMcVector3D;
            runMapCoreSafely(() => {
                result = viewport.ViewportToOverlayManagerWorld(props.tabInfo.properties.viewportConversionsProperties?.viewportCoordinate);
            }, "MapViewportForm/ViewportConversions/onViewportToOMWorldClick => ViewportToOverlayManagerWorld", true)
            props.tabInfo.setPropertiesCallback("viewportConversionsProperties", "overlayManagerCoordinate", result)
        }, "MapViewportForm/ViewportConversions/onViewportToOMWorldClick => onClick")
    }
    const onViewportToImageCalcWorldClick = () => {
        runCodeSafely(() => {
            let result: MapCore.SMcVector3D;
            runMapCoreSafely(() => {
                result = viewport.ViewportToImageCalcWorld(props.tabInfo.properties.viewportConversionsProperties?.viewportWorldCoordinate, props.tabInfo.properties.viewportConversionsProperties?.selectedImageCalc);
            }, "MapViewportForm/ViewportConversions/onViewportToImageCalcWorldClick => ViewportToImageCalcWorld", true)
            props.tabInfo.setPropertiesCallback("viewportConversionsProperties", "imageWorldCoordinate", result)
        }, "MapViewportForm/ViewportConversions/onViewportToImageCalcWorldClick => onClick")
    }
    const onImageCalcWorldToViewportClick = () => {
        runCodeSafely(() => {
            let result: MapCore.SMcVector3D;
            runMapCoreSafely(() => {
                result = viewport.ImageCalcWorldToViewport(props.tabInfo.properties.viewportConversionsProperties?.viewportWorldCoordinate, props.tabInfo.properties.viewportConversionsProperties?.selectedImageCalc);
            }, "MapViewportForm/ViewportConversions/onImageCalcWorldToViewportClick => ImageCalcWorldToViewport", true)
            props.tabInfo.setPropertiesCallback("viewportConversionsProperties", "imageWorldCoordinate", result)
        }, "MapViewportForm/ViewportConversions/onImageCalcWorldToViewportClick => onClick")
    }

    return (
        <div>
            <Fieldset className="form__column-fieldset" legend="Coordinate Conversions">
                <Fieldset className="form__column-fieldset" legend="Viewport & Overlay Manager">
                    <div className="form__center-aligned-row">
                        <label className="viewport__coord-conversion-label">Viewport:</label>
                        <Vector3DFromMap initValue={props.tabInfo.properties.viewportConversionsProperties?.viewportCoordinate} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD}
                            flagNull={{ x: false, y: false, z: false }}
                            saveTheValue={(point: MapCore.SMcVector3D, flagNull: boolean) => {
                                props.tabInfo.setPropertiesCallback("viewportConversionsProperties", "viewportCoordinate", point)
                            }} />
                    </div>
                    <div className="form__center-aligned-row">
                        <label className="viewport__coord-conversion-label">Overlay Manager:</label>
                        <Vector3DFromMap initValue={props.tabInfo.properties.viewportConversionsProperties?.overlayManagerCoordinate} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD}
                            flagNull={{ x: false, y: false, z: false }}
                            saveTheValue={(point: MapCore.SMcVector3D, flagNull: boolean) => {
                                props.tabInfo.setPropertiesCallback("viewportConversionsProperties", "overlayManagerCoordinate", point)
                            }} />
                    </div>
                    <div className="form__apply-buttons-container">
                        <Button onClick={onOMToViewportClick}>OM To Viewport</Button>
                        <Button onClick={onViewportToOMClick}>Viewport To OM</Button>
                    </div>
                </Fieldset>
                <Fieldset className="form__column-fieldset" legend="Viewport & Image Calc World">
                    <div className="form__row-container">
                        <div className="form__column-container">
                            <div className="form__center-aligned-row">
                                <label className="viewport__coord-conversion-label">Image World:</label>
                                <Vector3DFromMap initValue={props.tabInfo.properties.viewportConversionsProperties?.imageWorldCoordinate} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD}
                                    flagNull={{ x: false, y: false, z: false }}
                                    saveTheValue={(point: MapCore.SMcVector3D, flagNull: boolean) => {
                                        props.tabInfo.setPropertiesCallback("viewportConversionsProperties", "imageWorldCoordinate", point)
                                    }} />
                            </div>
                            <div className="form__center-aligned-row">
                                <label className="viewport__coord-conversion-label">Viewport World:</label>
                                <Vector3DFromMap initValue={props.tabInfo.properties.viewportConversionsProperties?.viewportWorldCoordinate} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD}
                                    flagNull={{ x: false, y: false, z: false }}
                                    saveTheValue={(point: MapCore.SMcVector3D, flagNull: boolean) => {
                                        props.tabInfo.setPropertiesCallback("viewportConversionsProperties", "viewportWorldCoordinate", point)
                                    }} />
                            </div>
                        </div>
                        <div style={{ marginLeft: 'auto' }} className="form__column-container">
                            <label>Image Calc:</label>
                            <ListBox optionLabel='label' value={props.tabInfo.properties.viewportConversionsProperties?.selectedImageCalc}
                                options={props.tabInfo.properties.viewportConversionsProperties?.imageCalcArr} onChange={e =>
                                    props.tabInfo.setPropertiesCallback("viewportConversionsProperties", "selectedImageCalc", e.value)} />
                        </div>
                    </div>

                    <div className="form__apply-buttons-container">
                        <Button onClick={onViewportToImageCalcWorldClick}>Viewport To Image Calc World</Button>
                        <Button onClick={onImageCalcWorldToViewportClick}>Image Calc World To Viewport</Button>
                    </div>
                </Fieldset>

            </Fieldset>
        </div>
    )
}
