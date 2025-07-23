import { useEffect } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";

export default function DataSourceSubLayers() {

    useEffect(() => {
        // m_VectorDataSourceProperties = DNMcRawVectorMapLayer.GetDataSourceSubLayersProperties(m_StrDataSource, chxSuffixByName.Checked);
        // 

    }, [])

    return (<div >
        to do later - after the shell is repaired..
        <DataTable>
            <Column></Column>
            <Column></Column>
            <Column></Column>
        </DataTable>
    </div>
    )
}