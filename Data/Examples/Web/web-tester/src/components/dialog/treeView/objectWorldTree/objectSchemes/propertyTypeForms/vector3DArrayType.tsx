import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { useState } from "react";
import _ from 'lodash';
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";
import Vector3DGrid from "../../shared/vector3DGrid";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../../redux/combineReducer";

export default function Vector3DArrayType(props: { value: any, onOk: (newValue: any) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [validPoints, setValidPoints] = useState([])
    let [value, setValue] = useState(props.value?.aElements);

    const saveLocationPointsTable = (locationPointsList: any, validPoints: boolean) => {
        runCodeSafely(() => {
            if (!_.isEqual(value, locationPointsList))
                setValue(locationPointsList)
            if (!validPoints)
                throw new Error("There is an invalid point");
        }, "Vector3DArrayType.saveLocationPointsTable")
    }
    return (<>
        <div style={{ display: "flex", flexDirection: "column", width: `${globalSizeFactor *1.5 * 33}vh` }}>
            <div>

                <Vector3DGrid pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} sendPointList={(l, v) => { saveLocationPointsTable(l, v) }} initLocationPointsList={value} ></Vector3DGrid>
            </div>
            <br></br>
            <Button label="OK" onClick={() => { props.onOk(new MapCore.IMcProperty.SArrayPropertyFVector3D(value)) }}></Button>
        </div>
    </>
    );
}
