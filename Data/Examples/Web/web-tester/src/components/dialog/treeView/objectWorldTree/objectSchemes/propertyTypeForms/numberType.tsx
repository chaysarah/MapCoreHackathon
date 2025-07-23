import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";
import { ChangeEvent, useEffect, useState } from "react";

export default function NumberType(props: { value: any, onOk: (newValue: number) => void }) {
    let [value, setValue] = useState(props.value);

    return (<>
        <div style={{ display: "flex", flexDirection: "column" }}>
            <InputNumber value={value} onValueChange={(event) => { setValue(event.target.value) }}></InputNumber>
            <br></br>
            <Button label="OK" onClick={() => { props.onOk(value) }}></Button>
        </div>
    </>
    );
}