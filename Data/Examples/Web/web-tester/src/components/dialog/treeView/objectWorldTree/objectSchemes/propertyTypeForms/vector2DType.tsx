import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";
import { ChangeEvent, useEffect, useState } from "react";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";

export default function Vector2DType(props: { value: any, onOk: (newValue: number) => void }) {
    let [value, setValue] = useState(props.value);

    const saveData = (event: any) => {
        runCodeSafely(() => {
            setValue({ ...value, [event.target.name]: event.target.value })
        }, "Vector2DType.saveData");
    }


    return (<>
        <div style={{ display: "flex", flexDirection: "column" }}>
            <div><label>Vector 2D: </label>
                <label>X:</label> <InputNumber name="x" value={value.x} onValueChange={saveData}></InputNumber>
                <label>Y:</label> <InputNumber name="y" value={value.y} onValueChange={saveData}></InputNumber>
            </div>
            <br></br>

        </div><Button label="OK" onClick={() => { props.onOk(value) }}></Button>
    </>
    );
}
