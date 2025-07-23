import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { useState } from "react";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../../redux/combineReducer";

export default function BoolType(props: { value: any, onOk: (newValue: number) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let [value, setValue] = useState(props.value);

    return (<>
        <div style={{ display: "flex", flexDirection: "column", width: `${globalSizeFactor *1.5 * 10}vh` }}>
            <div> <Checkbox checked={value} onChange={(event) => { setValue(event.target.checked) }} className="overlay-manager__margin-right"></Checkbox><label>Is Checked</label></div>
            <br></br>
            <Button label="OK" onClick={() => { props.onOk(value) }}></Button>
        </div>
    </>
    );
}
