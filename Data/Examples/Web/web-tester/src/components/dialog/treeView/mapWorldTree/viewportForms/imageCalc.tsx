import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";

import { MapViewportFormTabInfo } from "./mapViewportForm";
import { Properties } from "../../../dialog";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../redux/combineReducer";

export class ImageCalcPropertiesState implements Properties {
    static getDefault(p: any): ImageCalcPropertiesState {
        return {

        }
    }
}
export class ImageCalcProperties extends ImageCalcPropertiesState {
    static getDefault(p: any): ImageCalcProperties {
        let stateDefaults = super.getDefault(p);
        let defaults: ImageCalcProperties = {
            ...stateDefaults,

        }
        return defaults;
    }
};


export default function ImageCalc(props: { tabInfo: MapViewportFormTabInfo }) {
    const dispatch = useDispatch();
    let treeRedux = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        currentViewport: false,
    })
    //UseEffects
    // useEffect(() => {
    //     runCodeSafely(() => {
    //         // initialize first time the tab is loaded only
    //         if (!props.tabInfo.properties.imageCalcProperties) {
    //             props.tabInfo.setInitialStatePropertiesCallback("imageCalcProperties", null, ImageCalcPropertiesState.getDefault({}));
    //             props.tabInfo.setPropertiesCallback("imageCalcProperties", null, ImageCalcProperties.getDefault({}));
    //         }
    //     }, 'MapViewportForm/ImageCalc.useEffect');
    // }, [])

    // useEffect(() => {
    //     runCodeSafely(() => {
    //         props.tabInfo.setApplyCallBack("ImageCalc", applyAll);
    //     }, 'MapViewportForm/ImageCalc.useEffect => imageCalcProperties');
    // }, [props.tabInfo.properties.imageCalcProperties])

    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.currentViewport) {
                props.tabInfo.setInitialStatePropertiesCallback("imageCalcProperties", null, ImageCalcPropertiesState.getDefault({}));
                props.tabInfo.setPropertiesCallback("imageCalcProperties", null, ImageCalcProperties.getDefault({}));
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, currentViewport: true })
            }

        }, 'MapViewportForm/ImageCalc.useEffect => currentViewport');
    }, [props.tabInfo.currentViewport])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("imageCalcProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
        }, "MapViewportForm/ImageCalc.saveData => onChange")
    }
    const applyAll = () => {
        runCodeSafely(() => {

        }, 'MapViewportForm/ImageCalc.applyAll');
    }
    //DOM Functions

    return (
        <div>

        </div>
    )
}
