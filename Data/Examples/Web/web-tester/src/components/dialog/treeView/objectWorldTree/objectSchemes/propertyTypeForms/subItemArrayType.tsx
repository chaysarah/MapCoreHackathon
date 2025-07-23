import { Button } from "primereact/button";
import { useState, useEffect } from "react";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { InputText } from "primereact/inputtext";

export default function SubItemArrayType(props: { value: any, onOk: (newValue: MapCore.IMcProperty.SArrayPropertySubItemData) => void }) {
    let [value_id, setValue_id] = useState<any>("");
    let [value_startIndex, setValue_startIndex] = useState<any>("");

    useEffect(() => {
        let s1 = "";
        let s2 = "";
        for (let index = 0; index < props.value.aElements.length; index++) {
            s1 += props.value.aElements[index].uSubItemID + " ";
            s2 += props.value.aElements[index].nPointsStartIndex + " ";
        }
        setValue_id(s1)
        setValue_startIndex(s2)
    }, [])

    const onOk = () => {
        runCodeSafely(() => {
            let newValue = new MapCore.IMcProperty.SArrayPropertySubItemData();
            let idArray = value_id.trim().split(/\s+/).map(Number)
            let startIndexArray = value_startIndex.trim().split(/\s+/).map(Number)

            for (let index = 0; index < idArray.length; index++) {
                newValue.aElements[index] = new MapCore.SMcSubItemData(idArray[index], startIndexArray[index])
            }
            props.onOk(newValue)
        }, "SubItemArrayType.onOk")
    }

    return (<>
        <div style={{ display: "flex", flexDirection: "column" }}>
            <div><label>Value: </label>
                <label>ID: </label>  <InputText value={value_id} onChange={(event) => { setValue_id(event.target.value) }}></InputText>
                <label>Start Index: </label>   <InputText value={value_startIndex} onChange={(event) => { setValue_startIndex(event.target.value) }}></InputText>
                <Button style={{ margin: '2%' }} label="Save" onClick={onOk}></Button>
            </div>


        </div>
    </>
    );
}