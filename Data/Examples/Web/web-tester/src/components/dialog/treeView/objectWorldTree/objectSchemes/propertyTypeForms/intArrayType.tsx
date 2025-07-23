import { faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";
import { useState } from "react";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";

export default function IntArrayType(props: { value: MapCore.IMcProperty.SArrayPropertyInt, onOk: (newValue: any) => void }) {

    let [array, setArray] = useState(props.value == null ? [] : props.value.aElements);

    const numChange = (num: number, index: number) => {
        runCodeSafely(() => {
            let val = [...array];
            val[index] = num;
            setArray(val)
        }, "IntArrayType.numChange")
    }
    const addItemToList = () => {
        runCodeSafely(() => {
            setArray([...array, 0])
        }
            , "IntArrayType.numChange")
    }
    const deleteitem = (index: number) => {
        runCodeSafely(() => {
            let val = [...array];
            val.splice(index, 1)
            setArray(val)
        }
            , "IntArrayType.numChange")
    }
    return (<>
        <div style={{ display: "flex", flexDirection: "column" }}>
            <Button style={{ margin: "5%" }} onClick={addItemToList}>add</Button>
            <div >
                {array.length > 0 ?
                    array.map((item: any, index: number) => {
                        return <div key={index} style={{ display: "flex", alignItems: "center" }}>
                            <label>{index + 1})</label>
                            <InputNumber style={{ marginBottom: "3%" }} value={item} onValueChange={(e) => { numChange(e.target.value, index) }}></InputNumber>
                            <FontAwesomeIcon size="xl" style={{ color: '#6366F1', marginLeft: '3%' }} icon={faTrash} onClick={() => { deleteitem(index) }} />
                        </div>
                    }) :
                    <label>No Item</label>}
            </div>
            <br></br>
            <Button label="OK" onClick={() => { props.onOk(new MapCore.IMcProperty.SArrayPropertyInt(array)) }}></Button>
        </div>
    </>
    );
}
