import { useSelector } from "react-redux";
import { Fieldset } from "primereact/fieldset";
import { Dropdown } from "primereact/dropdown";
import { Button } from "primereact/button";
import { DataTable } from "primereact/datatable";
import { Column, ColumnEditorOptions } from "primereact/column";
import { InputNumber } from "primereact/inputnumber";
import { ConfirmDialog } from "primereact/confirmdialog";

import { getEnumValueDetails, getEnumDetailsList } from 'mapcore-lib';
import { TabInfo } from "../../../../shared/tabCtrls/tabModels";
import { Properties } from "../../../../dialog";
import { AppState } from "../../../../../../redux/combineReducer";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";

export class FiltersPropertiesState implements Properties {
    selectedFilterOperation: any;
    bias: number;
    divider: number;
    filterXSize: number;
    filterYSize: number;
    filterTable: any[];

    static getDefault(p: any): FiltersPropertiesState {
        let { currentViewport } = p;
        let mcCurrentViewport: MapCore.IMcMapViewport = currentViewport.nodeMcContent;

        return {
            selectedFilterOperation: null,
            bias: 0,
            divider: 1,
            filterXSize: 0,
            filterYSize: 0,
            filterTable: [],
        }
    }
}
export class FiltersProperties extends FiltersPropertiesState {
    filterOperations: { name: string, code: number, theEnum: any }[];
    isConfirmDialog: false;

    static getDefault(p: any): FiltersProperties {
        let stateDefaults = super.getDefault(p);

        let defaults: FiltersProperties = {
            ...stateDefaults,
            filterOperations: getEnumDetailsList(MapCore.IMcImageProcessing.EFilterProccessingOperation),
            isConfirmDialog: false,
        }
        return defaults;
    }
};

export default function Filters(props: { tabInfo: TabInfo }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree)

    const getColumns = () => {
        let columns = [];
        runCodeSafely(() => {
            for (let index = 0; index < props.tabInfo.tabProperties.filterXSize; index++) {
                columns = [...columns, { field: `${index}`, header: `${index + 1}` }]
            }
        }, 'ImageProcessing/Filters.getColumns')
        return columns;
    }
    const generateTableBySize = (x: number, y: number) => {
        let oldTable = props.tabInfo.tabProperties.filterTable;
        let table = [];

        runCodeSafely(() => {
            for (let row = 0; row < y; row++) {
                let rowData = {};
                for (let col = 0; col < x; col++) {
                    rowData[col] = oldTable[row]?.[col] || null;
                }
                table = [...table, rowData];
            }
        }, 'ImageProcessing/Filters.generateTableBySize')
        return table;
    }
    const cellEditor = (options: ColumnEditorOptions) => {
        return <InputNumber className="form__slim-input" value={options.value} onValueChange={(e) => options.editorCallback(e.target.value)} onKeyDown={(e) => e.stopPropagation()} />;
    };
    const float32ArrayToObjectArray = (float32Array: Float32Array, objectLen: number) => {
        const result = [];
        for (let i = 0; i < float32Array.length; i += objectLen) {
            const obj = {};
            for (let j = 0; j < objectLen; j++) {
                obj[j] = float32Array[i + j];
            }
            result.push(obj);
        }
        return result;
    }
    //#region  Handle Functions
    const handleFilterImageProcessSetClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['selectedFilterOperation']);
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => { mcCurrentViewport.SetFilterImageProcessing(finalLayer, props.tabInfo.tabProperties.selectedFilterOperation); }, 'ImageProcessing/Filters.handleFilterImageProcessSetClick => IMcImageProcessing.SetFilterImageProcessing', true);
        }, 'ImageProcessing/Filters.handleFilterImageProcessSetClick')
    }
    const handleFilterImageProcessGetClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let filterOperation: MapCore.IMcImageProcessing.EFilterProccessingOperation = null;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => { filterOperation = mcCurrentViewport.GetFilterImageProcessing(finalLayer) }, 'ImageProcessing/Filters.handleFilterImageProcessGetClick => IMcImageProcessing.GetFilterImageProcessing', true);
            let selectedFilter = getEnumValueDetails(filterOperation, props.tabInfo.tabProperties.filterOperations)
            props.tabInfo.setPropertiesCallback('selectedFilterOperation', selectedFilter);
        }, 'ImageProcessing/Filters.handleFilterImageProcessGetClick')
    }
    const handleSetCustomFilterClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['filterXSize', 'filterYSize', 'bias', 'divider', 'filterTable']);
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            const flattened = props.tabInfo.tabProperties.filterTable.flatMap(obj => Object.values(obj));
            let isNull = false;
            flattened.forEach(num => { if (!num) isNull = true; });
            if (!isNull) {//dont delete! unequals to null, not undefind
                const float32Array: Float32Array = new Float32Array(flattened);
                let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
                runMapCoreSafely(() => {
                    mcCurrentViewport.SetCustomFilter(finalLayer,
                        props.tabInfo.tabProperties.filterXSize, props.tabInfo.tabProperties.filterYSize,
                        float32Array, props.tabInfo.tabProperties.bias,
                        props.tabInfo.tabProperties.divider,
                    );
                }, 'ImageProcessing/Filters.handleSetCustomFilterClick => IMcImageProcessing.SetCustomFilter', true);
            }
            else {
                props.tabInfo.setPropertiesCallback('isConfirmDialog', true);
            }
        }, 'ImageProcessing/Filters.handleSetCustomFilterClick')
    }
    const handleGetCustomFilterClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let pX: { Value?: number } = {};
            let pY: { Value?: number } = {};
            let pBias: { Value?: number } = {};
            let pDivider: { Value?: number } = {};
            let pFilter: { Value?: Float32Array } = {};
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                mcCurrentViewport.GetCustomFilter(finalLayer, pX, pY, pFilter, pBias, pDivider);
            }, 'ImageProcessing/Filters.handleGetCustomFilterClick => IMcImageProcessing.GetCustomFilter', true);
            let filteredData = float32ArrayToObjectArray(pFilter.Value, pX.Value)
            props.tabInfo.setPropertiesCallback('filterXSize', pX.Value);
            props.tabInfo.setPropertiesCallback('filterYSize', pY.Value);
            props.tabInfo.setPropertiesCallback('bias', pBias.Value);
            props.tabInfo.setPropertiesCallback('divider', pDivider.Value);
            props.tabInfo.setPropertiesCallback('filterTable', filteredData);
        }, 'ImageProcessing/Filters.handleGetCustomFilterClick')
    }
    const handleFiltersSizeChange = (e) => {
        runCodeSafely(() => {
            let val = e.target.value ? parseInt(e.target.value) : null;
            if (!isNaN(val)) {
                props.tabInfo.setPropertiesCallback(e.target.name, val);
                props.tabInfo.setCurrStatePropertiesCallback(e.target.name, val);
                let table = e.target.name == 'filterYSize' ?
                    generateTableBySize(props.tabInfo.tabProperties.filterXSize, val) :
                    generateTableBySize(val, props.tabInfo.tabProperties.filterYSize);
                props.tabInfo.setPropertiesCallback('filterTable', table);
            }
        }, 'ImageProcessing/Filters.handleFiltersSizeChange')
    }
    const onCellEditComplete = (e) => {
        runCodeSafely(() => {
            let { rowData, newValue, field, originalEvent: event } = e;
            rowData[field] = newValue;
            props.tabInfo.setCurrStatePropertiesCallback('filterTable', props.tabInfo.tabProperties.filterTable)
        }, 'ImageProcessing/Filters.onCellEditComplete')
    }
    //#endregion
    //#region DOM Functions
    const getFilterImageProcess = () => {
        return <Fieldset className="form__column-fieldset" legend='Filter Image Process'>
            <div className="form__flex-and-row-between form__items-center">
                <label className="props__input-label">Filter Processing Operation:</label>
                <Dropdown name="selectedFilterOperation" value={props.tabInfo.tabProperties.selectedFilterOperation} onChange={props.tabInfo.saveData} options={props.tabInfo.tabProperties.filterOperations} optionLabel="name" />
            </div>
            <div className="form__row-container form__justify-end">
                <Button label="Set" onClick={handleFilterImageProcessSetClick} />
                <Button label="Get" onClick={handleFilterImageProcessGetClick} />
            </div>
        </Fieldset>
    }
    const getCustomFilter = () => {
        return <Fieldset className="form__column-fieldset" legend='Custom Filter'>
            <div className="form__flex-and-row-between">
                <label htmlFor="bias">Bias: </label>
                <InputNumber id='bias' value={props.tabInfo.tabProperties.bias} name="bias" minFractionDigits={2} onValueChange={props.tabInfo.saveData} />
            </div>
            <div className="form__flex-and-row-between">
                <label htmlFor="divider">Divider: </label>
                <InputNumber id='divider' value={props.tabInfo.tabProperties.divider} name="divider" minFractionDigits={2} onValueChange={props.tabInfo.saveData} />
            </div>
            <div className="form__flex-and-row-between">
                <label htmlFor="filterXSize">Filter X Size: </label>
                <InputNumber id='filterXSize' value={props.tabInfo.tabProperties.filterXSize} name="filterXSize" onValueChange={handleFiltersSizeChange} />
            </div>
            <div className="form__flex-and-row-between">
                <label htmlFor="filterYSize">Filter Y Size: </label>
                <InputNumber id='filterYSize' value={props.tabInfo.tabProperties.filterYSize} name="filterYSize" onValueChange={handleFiltersSizeChange} />
            </div>
            <DataTable tableStyle={{ width: `${globalSizeFactor * 45}vh`, maxWidth: `${globalSizeFactor * 45}vh` }} value={props.tabInfo.tabProperties.filterTable} editMode="cell">
                {getColumns().map(({ field, header }) => {
                    return <Column style={{ width: `${globalSizeFactor * (45 / props.tabInfo.tabProperties.filterXSize)}vh` }} key={field} field={field} header={header} editor={(options) => cellEditor(options)} onCellEditComplete={onCellEditComplete} />;
                })}
            </DataTable>
            <div className="form__row-container form__justify-end">
                <Button label="Set" onClick={handleSetCustomFilterClick} />
                <Button label="Get" onClick={handleGetCustomFilterClick} />
            </div>
        </Fieldset>
    }
    //#endregion

    return <div>
        {getFilterImageProcess()}
        {getCustomFilter()}

        <ConfirmDialog
            contentClassName='form__confirm-dialog-content'
            message='full table is required.'
            header='Invalid Filter Values'
            footer={<div></div>}
            visible={props.tabInfo.tabProperties.isConfirmDialog}
            onHide={e => { props.tabInfo.setPropertiesCallback('isConfirmDialog', false); }}
        />
    </div>
}