import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { useRef, useState } from "react";
import { InputNumber } from "primereact/inputnumber";
import { Button } from "primereact/button";
import { runCodeSafely } from "../../../../common/services/error-handling/errorHandler";
import { InputText } from "primereact/inputtext";
import { useSelector } from "react-redux";
import { AppState } from "../../../../redux/combineReducer";
import { Fieldset } from "primereact/fieldset";
import { InputTextarea } from "primereact/inputtextarea";

export default function ServerRequestParams(props: { initRequestParams: any, initBody?: MapCore.SMcKeyStringValue, getRequestParams: (RequestParams: MapCore.SMcKeyStringValue[], body?: MapCore.SMcKeyStringValue) => void, showCSWBody: boolean }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let [body, setBody] = useState<MapCore.SMcKeyStringValue>(props.initBody)
    let jsonUpload = useRef(null);

    let [requestParams, setRequestParams] = useState<MapCore.SMcKeyStringValue[]>(props.initRequestParams)
    const cellEditor = (props: any): any => {
        return (
            <InputText type="text" className="form__slim-input" value={requestParams[props.rowIndex][props.field]}
                onChange={(e) => onKeyStepTypeEditorValueChange(props, e.target.value)} ></InputText>
        );
    }
    const onKeyStepTypeEditorValueChange = (props: any, value: string) => {
        runCodeSafely(() => {
            let updatedKeyStepTypes = [...requestParams];
            updatedKeyStepTypes[props.rowIndex][props.field] = value;
            setRequestParams(updatedKeyStepTypes)
        }, "EditModePropertiesDialog/General.keyStepTypeInputTextEditor => onKeyStepTypeEditorValueChange")
    };

    const JsonObjectToKeyValueArray = (obj) => {
        let aKeyValueArray = [];
        for (const [key, value] of Object.entries(obj)) {

            let KeyValue = new MapCore.SMcKeyStringValue();
            KeyValue.strKey = key;
            KeyValue.strValue = value.toString();
            if (key == "McCswQueryBody")
                setBody(KeyValue)
            else {
                aKeyValueArray.push(KeyValue);
            }
        }
        return aKeyValueArray;
    }
    const handleUpload = async (selectedFile) => {
        if (selectedFile) {
            const reader = new FileReader();
            reader.onload = (e) => {
                if (e.target && e.target.result) {
                    try {
                        const data = JSON.parse(e.target.result as string);
                        console.log('Parsed JSON:', data);
                        console.log(JsonObjectToKeyValueArray(data));
                        setRequestParams(JsonObjectToKeyValueArray(data))
                    } catch (error) {
                        console.error('Error parsing JSON:', error);
                    }
                }
            };
            reader.readAsText(selectedFile);
        }
    };

    return (<>
        <div style={{ width: `${globalSizeFactor * 70}vh` }}>
            <DataTable value={requestParams} editMode="cell" style={{ overflow: 'auto', height: `${globalSizeFactor * 30}vh` }}>
                <Column header="Key" field="strKey" editor={cellEditor}></Column>
                <Column header="Value" field="strValue" editor={cellEditor}
                    style={{ overflow: 'auto', maxWidth: `${globalSizeFactor * 20}vh`, maxHeight: `${globalSizeFactor * 10}vh` }}
                ></Column>
                <Column body={(rowData, f) => {
                    return <div><Button label="Delete"
                        onClick={() => {
                            setRequestParams(requestParams.filter(u => u != rowData))
                        }}></Button></div>
                }}></Column>
            </DataTable>
            <Button onClick={() => { setRequestParams([...requestParams, new MapCore.SMcKeyStringValue()]) }}>Add Row</Button>
            <Fieldset hidden={!props.showCSWBody} legend="CSW Body" style={{ width: '30%' }}>
                <InputTextarea style={{ width: `${globalSizeFactor * 60}vh`, height: `${globalSizeFactor * 10}vh` }} value={body?.strValue} onChange={(e) => {
                    let body = new MapCore.SMcKeyStringValue();
                    body.strValue = e.target.value;
                    body.strKey = "McCswQueryBody"
                    setBody(body)
                }}></InputTextarea>
            </Fieldset>
               <input ref={jsonUpload} type="file" onChange={(event) => { handleUpload(event.target.files?.[0]) }} hidden />
            <Button onClick={() => { jsonUpload.current.click() }}>Load json file</Button>
        </div>
        <div style={{direction:'rtl',marginTop: `${globalSizeFactor * 2}vh`}}><Button onClick={() => { props.getRequestParams(requestParams, props.showCSWBody ? body : null) }}>OK</Button></div>
         
    </>
    );
}
