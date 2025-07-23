import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";
import { ChangeEvent, useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../../redux/combineReducer";
import ColorPickerCtrl from "../../../../../shared/colorPicker";

export default function ColorType(props: { value: any, onOk: (newValue: number) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let [value, setValue] = useState(props.value);

    const setColor = (event: { value: any; target: { value: any; }; }) => {
        let color = event.value
        setValue({ ...value, r: color.r, g: color.g, b: color.b, a: color.a });
    }
    return (<>
        <div style={{ display: "flex", flexDirection: "column", width: `${globalSizeFactor * 1.5 * 20}vh` }}>
            <div><label>color: </label>
                <ColorPickerCtrl alpha={true} value={value} onChange={setColor} />
            </div>
            <br></br>
            <Button label="OK" onClick={() => { props.onOk(value) }}></Button>
        </div>
    </>
    );
}
