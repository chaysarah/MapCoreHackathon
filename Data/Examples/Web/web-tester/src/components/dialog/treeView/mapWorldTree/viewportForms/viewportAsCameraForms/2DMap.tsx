import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";
import { ConfirmDialog } from "primereact/confirmdialog";

import { ViewportAsCameraFormTabInfo } from "./viewportAsCamera";
import { Properties } from "../../../../dialog";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../../redux/combineReducer";

export class Map2DPropertiesState implements Properties {
    deltaX: number;
    deltaY: number;

    static getDefault(p: any): Map2DPropertiesState {
        let { currentViewport } = p;

        return {
            deltaX: 0,
            deltaY: 0,
        }
    }
}
export class Map2DProperties extends Map2DPropertiesState {
    isConfirmDialog: false;

    static getDefault(p: any): Map2DProperties {
        let stateDefaults = super.getDefault(p);

        let defaults: Map2DProperties = {
            ...stateDefaults,
            isConfirmDialog: false,
        }
        return defaults;
    }
};

export default function Map2D(props: { tabInfo: ViewportAsCameraFormTabInfo }) {
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    let [map2DLocalProperties, setMap2DLocalProperties] = useState(props.tabInfo.tabProperties);

    //UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            props.tabInfo.setApplyCallBack(applyAll);
            setMap2DLocalProperties(props.tabInfo.tabProperties)
        }, 'ViewportAsCamera/2DMap.useEffect => Map2DProperties');
    }, [props.tabInfo.tabProperties])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in new Map2DPropertiesState) {
                props.tabInfo.setCurrStatePropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            }
        }, "ViewportAsCamera/2DMap.saveData => onChange")
    }
    const applyAll = () => {
        runCodeSafely(() => {
            handleSetClick(false);
        }, 'ViewportAsCamera/2DMap.applyAll');
    }

    const handleSetClick = (e) => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['deltaX', 'deltaY'])
            let mcCurrentViewport = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            let vpType = mcCurrentViewport.GetMapType();
            if (vpType == MapCore.IMcMapCamera.EMapType.EMT_2D) {
                runMapCoreSafely(() => {
                    mcCurrentViewport.ScrollCamera(map2DLocalProperties.deltaX, map2DLocalProperties.deltaY)
                }, 'MapViewportForm/2DMap.handleSetClick => IMcMapCamera.ScrollCamera', true);
                // loadViewport
                props.tabInfo.loadProperties(selectedNodeInTree);
            }
            else if (e) {
                props.tabInfo.setPropertiesCallback('isConfirmDialog', true)
            }
        }, 'MapViewportForm/2DMap.handleSetClick');
    }
    //Handle Functions


    return (
        <div className="form__flex-and-row-between form__items-center">
            <span> Scroll Camera:</span>
            <div className="form__flex-and-row form__items-center">
                <span className="vp-as-camera__r-padding-span">Delta X:</span>
                <InputNumber name="deltaX" value={map2DLocalProperties.deltaX} onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row form__items-center">
                <span className="vp-as-camera__r-padding-span">Delta Y:</span>
                <InputNumber name="deltaY" value={map2DLocalProperties.deltaY} onValueChange={saveData} />
            </div>
            <Button label="Set" onClick={handleSetClick} />

            <ConfirmDialog
                contentClassName='form__confirm-dialog-content'
                message='Only for 2D map.'
                header=''
                footer={<div></div>}
                visible={map2DLocalProperties.isConfirmDialog}
                onHide={e => { props.tabInfo.setPropertiesCallback('isConfirmDialog', false) }}
            />
        </div>
    )
}
