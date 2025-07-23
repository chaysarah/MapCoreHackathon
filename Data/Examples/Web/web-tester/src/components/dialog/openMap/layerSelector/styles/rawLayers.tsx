import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { useState } from "react";
import { InputNumber } from "primereact/inputnumber";

export default function ServerRequestParams() {

    let [requestParams, setRequestParams] = useState<MapCore.SMcKeyStringValue[]>([new MapCore.SMcKeyStringValue()])
    const cellEditor = (): any => {

    }

    return (<div>


    </div>);
}

