import { faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import { InputTextarea } from "primereact/inputtextarea";
import { useState } from "react";
import { useSelector } from "react-redux";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../../redux/combineReducer";

export default function StringType(props: { value: any, onOk: (newValue: any) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    let [value__astrStrings, setValue__astrStrings] = useState(props.value.astrStrings);
    let [value__bIsUnicode, setValue__bIsUnicode] = useState(props.value.bIsUnicode);

    const stringChange = (str: string, index: number) => {
        runCodeSafely(() => {
            let val = [...value__astrStrings];
            val[index] = str;
            setValue__astrStrings(val)
        }, "StringType.stringChange")
    }
    const addItemToList = () => {
        setValue__astrStrings([...value__astrStrings, ""])
    }
    const deleteitem = (index: number) => {
        runCodeSafely(() => {
            let val = [...value__astrStrings];
            val.splice(index, 1)
            setValue__astrStrings(val)
        }, "StringType.deleteitem")
    }
    return (<>
        <div style={{ display: "flex", flexDirection: "column", width: `${globalSizeFactor *1.5 * 25}vh` }}>
            <Button onClick={addItemToList}>add</Button>
            <br></br>
            <div style={{ maxHeight: `${globalSizeFactor * 20}vh`, overflow: 'auto' }}>
                {value__astrStrings.map((item: any, index: number) => {
                    return <div key={index} style={{ display: "flex", alignItems: "center", margin: `${globalSizeFactor * 0.6}vh` }}>
                        <label>{index})</label>
                        <InputTextarea value={item} onChange={(e) => { stringChange(e.target.value, index) }}></InputTextarea>
                        <FontAwesomeIcon size="xl" style={{ color: '#6366F1', marginLeft: '3%' }} icon={faTrash} onClick={() => { deleteitem(index) }} />
                    </div>
                })}
            </div>
            <br></br>
            <div> <Checkbox checked={value__bIsUnicode} onChange={(event) => { setValue__bIsUnicode(event.target.checked) }} className="overlay-manager__margin-right"></Checkbox><label>Is Unicode</label></div>
            <br></br>
            <Button label="OK" onClick={() => { props.onOk({ astrStrings: value__astrStrings, bIsUnicode: value__bIsUnicode }) }}></Button>
        </div>
    </>
    );
}
