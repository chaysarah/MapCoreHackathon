import { faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";
import { ChangeEvent, useEffect, useState } from "react";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";

export default function Vector2DArrayType(props: { value: any, onOk: (newValue: any) => void }) {

    let [array, setArray] = useState(props.value == null ? [] : props.value.aElements);

    const numChange = (num: any, index: number) => {
        runCodeSafely(() => {
            let val = [...array];
            val[index] = { ...val[index], ...num };
            setArray(val)
        }, "Vector2DArrayType.numChange")
    }
    const addItemToList = () => {
        runCodeSafely(() => {
            setArray([...array, { x: null, y: null }])
        }, "Vector2DArrayType.addItemToList")
    }
    const deleteitem = (index: number) => {
        runCodeSafely(() => {
            let val = [...array];
            val.splice(index, 1)
            setArray(val)
        }, "Vector2DArrayType.deleteitem")
    }
    return (<>
        <div style={{ display: "flex", flexDirection: "column" }}>
            <Button style={{ margin: "5%" }} onClick={addItemToList}>add</Button>
            <div >
                {array.length > 0 ?
                    array.map((item: any, index: number) => {
                        return <div style={{ display: "flex", alignItems: "center" }}> <label style={{ whiteSpace: "nowrap" }}> {index + 1})</label>
                            <label>X:</label> <InputNumber name="x" value={item.x} onValueChange={(e) => { numChange({ x: e.target.value }, index) }}></InputNumber>
                            <label>Y:</label> <InputNumber name="y" value={item.y} onValueChange={(e) => { numChange({ y: e.target.value }, index) }}></InputNumber>
                            <FontAwesomeIcon size="xl" style={{ color: '#6366F1', marginLeft: '3%' }} icon={faTrash} onClick={() => { deleteitem(index) }} />
                        </div>
                    }) :
                    <label>No Item</label>}
            </div>
            <br></br>
            <Button label="OK" onClick={() => {
                if (array.every((element: { x: any; y: any; }) => element.x != null && element.y != null))
                    props.onOk(new MapCore.IMcProperty.SArrayPropertyFVector2D(array))
                else
                    alert("the point is not valid")
            }}></Button>
        </div>
    </>
    );
}
