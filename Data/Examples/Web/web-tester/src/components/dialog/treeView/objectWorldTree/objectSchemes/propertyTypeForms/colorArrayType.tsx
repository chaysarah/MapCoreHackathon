import { faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";
import { ChangeEvent, useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../../redux/combineReducer";
import ColorPickerCtrl from "../../../../../shared/colorPicker";

export default function ColorArrayType(props: { value: any, onOk: (newValue: any) => void }) {

    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let [array, setArray] = useState(props.value == null ? [] : props.value.aElements);

    const numChange = (num: any, index: number) => {
        runCodeSafely(() => {
            let val = [...array];
            val[index] = { ...val[index], ...num };
            setArray(val)
        }, "IntArrayType.numChange")
    }
    const addItemToList = () => {
        runCodeSafely(() => {
            setArray([...array, { r: 0, g: 0, b: 0, a: 0 }])
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
                        return <div style={{ display: "flex", alignItems: "center" }}> <label style={{ whiteSpace: "nowrap" }}>color {index + 1}:</label>
                            <ColorPickerCtrl value={item} onChange={(e) => { numChange(e.target.value, index) }} ></ColorPickerCtrl>
                            <label >Alpha:</label>
                            <InputNumber name='form__narrow-input' style={{ width: `${globalSizeFactor * 9.5}vh` }} mode="decimal" value={item.a} onValueChange={(e) => { numChange({ a: e.target.value }, index) }} />
                            <FontAwesomeIcon size="xl" style={{ color: '#6366F1', marginLeft: '3%' }} icon={faTrash} onClick={() => { deleteitem(index) }} />
                        </div>
                    }) :
                    <label>No Item</label>}
            </div>
            <br></br>
            <Button label="OK" onClick={() => { props.onOk(new MapCore.IMcProperty.SArrayPropertyBColor(array)) }}></Button>
        </div>
    </>
    );
}
