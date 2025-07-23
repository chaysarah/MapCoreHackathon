import { Button } from "primereact/button";
import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import { InputText } from "primereact/inputtext";
import { useEffect, useState } from "react";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";

export default function GdalOptions(props: { existGdalOptions: string[], getGdalOptions: (gdalOptions: string[]) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let [table, setTable] = useState([{ index: 0, key: '', value: '' }]);

    useEffect(() => {
        const arrayOfObjects = props.existGdalOptions.map((str, i) => {
            const [key, value] = str.split("=");
            return { index: i, key: key, value: value };
        });
        setTable(arrayOfObjects);
    }, [])

    const handleEditorValueChange = (props: any, value: string) => {
        runCodeSafely(() => {
            let updatedTable = [...table];
            updatedTable[props.rowIndex][props.field] = value;
            setTable(updatedTable)
        }, 'PrintMap/GdalOptions.onEditorValueChange => onChange')
    };
    const handleOKClick = () => {
        runCodeSafely(() => {
            let gdalOptionsStrArr = table.map((option) => `${option.key}=${option.value}`);
            props.getGdalOptions(gdalOptionsStrArr);
        }, 'PrintMap/GdalOptions.handleOKClick => onClick')
    }

    const cellEditor = (props: any): any => {
        return (
            <InputText style={{ width: `${globalSizeFactor * 10}vh` }} value={table[props.rowIndex][props.field]}
                onChange={(e) => handleEditorValueChange(props, e.target.value)} />
        );
    }

    return <div className="form__column-container">
        <div style={{ width: `${globalSizeFactor * 40}vh`, height: `${globalSizeFactor * 25}vh` }}>
            <DataTable scrollable scrollHeight={`${globalSizeFactor * 25}vh`} value={table} editMode="cell">
                <Column header="Key" field="key" editor={cellEditor} />
                <Column header="Value" field="value" editor={cellEditor} />
                <Column body={(rowData) => {
                    return <Button label="Delete" onClick={() => { setTable(table.filter(row => row != rowData)) }} />
                }} />
            </DataTable>
        </div>

        <div className="form__footer-padding form__flex-and-row-between">
            <Button label='Add Row' onClick={e => setTable([...table, { index: table.length, key: '', value: '' }])} />
            <Button label="OK" onClick={handleOKClick} />
        </div>
    </div>
}