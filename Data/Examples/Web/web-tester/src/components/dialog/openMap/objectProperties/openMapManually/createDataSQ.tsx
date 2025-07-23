import { ChangeEvent, useEffect, useState } from "react";
import SelectCoordinateSystem from "../../../shared/ControlsForMapcoreObjects/coordinateSystemCtrl/selectCoordinateSystem";
import { InputNumber } from "primereact/inputnumber";
import { AppState } from "../../../../../redux/combineReducer";
import { useSelector } from "react-redux";
import { Fieldset } from "primereact/fieldset";
import Vector3DFromMap from "../../../treeView/objectWorldTree/shared/Vector3DFromMap";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import SelectOverlayManager from "../../../shared/ControlsForMapcoreObjects/overlayManagerCtrl/selectOverlayManager";
import InputMaxNumber from "../../../../shared/inputMaxNumber";

export default function CreateDataSQ(props: { initialSCreateData: MapCore.IMcMapViewport.SCreateData, setSCreateData: (sCreateData: MapCore.IMcMapViewport.SCreateData) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [SCreateData, setSCreateData] = useState(props.initialSCreateData);

    useEffect(() => {
        props.setSCreateData(SCreateData);
    }, [SCreateData])

    return (<div className="form__column-container">
        <SelectCoordinateSystem header="Grid Coordinate System" getSelectedCorSys={(selectedCorSys: MapCore.IMcGridCoordinateSystem) => { setSCreateData({ ...SCreateData, pCoordinateSystem: selectedCorSys }) }}></SelectCoordinateSystem>
        <SelectOverlayManager getSelectedOM={(selectedOMs) => { setSCreateData({ ...SCreateData, pOverlayManager: selectedOMs }) }} initSelectedOMs={SCreateData.pOverlayManager}></SelectOverlayManager>
        <div style={{ marginLeft: `${globalSizeFactor * 1.5}vh` }} className="flex-auto">
            <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>viewport Id:</label>
            <InputMaxNumber maxValue={4294967295} value={SCreateData.uViewportID} getUpdatedMaxInput={(value: number) => setSCreateData({ ...SCreateData, uViewportID: value })}></InputMaxNumber>
        </div>
    </div>
    )
}