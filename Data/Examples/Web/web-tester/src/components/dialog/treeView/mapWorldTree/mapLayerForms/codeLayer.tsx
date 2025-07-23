import { Fieldset } from "primereact/fieldset";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";
import { Properties } from "../../../dialog";
import { TabInfo } from "../../../shared/tabCtrls/tabModels";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { AppState } from "../../../../../redux/combineReducer";
import { useSelector } from "react-redux";
import ColorPickerCtrl from "../../../../shared/colorPicker";
import { InputNumber } from "primereact/inputnumber";
import { useEffect } from "react";

export class CodeLayerPropertiesState implements Properties {
    colorCodeTable: MapCore.IMcCodeMapLayer.SCodeMapData[];

    static getDefault(p: any): CodeLayerPropertiesState {
        let { currentLayer, mapWorldTree } = p;
        let mcCurrentLayer = currentLayer.nodeMcContent as MapCore.IMcCodeMapLayer;
        let colorCodeTable: MapCore.IMcCodeMapLayer.SCodeMapData[] = null;

        runMapCoreSafely(() => {
            colorCodeTable = mcCurrentLayer.GetColorTable();
        }, 'MapLayerForm/CodeLayer.getDefault => IMcCodeMapLayer.GetColorTable', true);
        let finalColorCodeTable = colorCodeTable.map((colorCodeObj, ind) => { return { index: ind, uCode: colorCodeObj.uCode, CodeColor: colorCodeObj.CodeColor } })

        return {
            colorCodeTable: [...finalColorCodeTable, { index: finalColorCodeTable.length, uCode: finalColorCodeTable.length, CodeColor: { r: 255, g: 255, b: 255, a: 255 } }],
        }
    }
}
export class CodeLayerProperties extends CodeLayerPropertiesState {
    mcCodeLayer: MapCore.IMcCodeMapLayer;
    numTraversabilityDirections: number;

    static getDefault(p: any): CodeLayerProperties {
        let stateDefaults = super.getDefault(p);
        let { currentLayer, mapWorldTree } = p;
        let mcCurrentLayer = currentLayer.nodeMcContent as MapCore.IMcCodeMapLayer;

        return {
            ...stateDefaults,
            mcCodeLayer: mcCurrentLayer,
            numTraversabilityDirections: 0,
        }
    }
}

export default function CodeLayer(props: { tabInfo: TabInfo }) {
    let { tabProperties, setPropertiesCallback, applyCurrStatePropertiesCallback, setCurrStatePropertiesCallback, setApplyCallBack, saveData } = props.tabInfo;
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    useEffect(() => {
        runCodeSafely(() => {
            let numTraversabilityDirections: number;
            if (isTraversabilirtyLayer()) {
                runMapCoreSafely(() => {
                    numTraversabilityDirections = tabProperties.mcCodeLayer.GetNumTraversabilityDirections();
                }, 'MapLayerForm/CodeLayer.getDefault => IMcCodeMapLayer.GetColorTable', true);
                setPropertiesCallback('numTraversabilityDirections', numTraversabilityDirections);
            }
        }, 'MapLayerForm/codeLayer.useEffect')
    }, [])
    const isTraversabilirtyLayer = () => {
        let traversabilityTypes = [MapCore.IMcNativeServerTraversabilityMapLayer.LAYER_TYPE, MapCore.IMcRawTraversabilityMapLayer.LAYER_TYPE, MapCore.IMcNativeTraversabilityMapLayer.LAYER_TYPE]
        let layerType: number = null;
        runMapCoreSafely(() => {
            layerType = tabProperties.mcCodeLayer.GetLayerType();
        }, 'MapLayerForm/codeLayer.isTraversabilirtyLayer => IMcMapLayer.GetLayerType', true);
        return traversabilityTypes.includes(layerType);
    }
    //#region Handle Functions
    const handleGetColorTableClick = () => {
        runCodeSafely(() => {
            let colorCodeTable: MapCore.IMcCodeMapLayer.SCodeMapData[] = null;
            runMapCoreSafely(() => {
                colorCodeTable = tabProperties.mcCodeLayer.GetColorTable();
            }, 'MapLayerForm/CodeLayer.handleGetColorTableClick => IMcCodeMapLayer.GetColorTable', true);
            let finalColorCodeTable = colorCodeTable.map((colorCodeObj, ind) => { return { index: ind, uCode: colorCodeObj.uCode, CodeColor: colorCodeObj.CodeColor } })
            setPropertiesCallback('colorCodeTable', [...finalColorCodeTable, { index: finalColorCodeTable.length, uCode: finalColorCodeTable.length, CodeColor: { r: 255, g: 255, b: 255, a: 255 } }]);
        }, 'MapLayerForm/CodeLayer.handleGetColorTableClick')
    }
    const handleSetColorTableClick = () => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['colorCodeTable'])
            let slicesArray = tabProperties.colorCodeTable.slice(0, tabProperties.colorCodeTable.length - 1)
            let mcColorCodeTable = slicesArray.map((colorCodeObj, ind) => { return { uCode: colorCodeObj.uCode, CodeColor: colorCodeObj.CodeColor } });
            runMapCoreSafely(() => {
                tabProperties.mcCodeLayer.SetColorTable(mcColorCodeTable);
            }, 'MapLayerForm/CodeLayer.handleSetColorTableClick => IMcCodeMapLayer.SetColorTable', true);
        }, 'MapLayerForm/CodeLayer.handleSetColorTableClick')
    }
    //#endregion
    const onCellChange = (eValue: any, rowData: any, column: any) => {
        let copiedArr = [...tabProperties.colorCodeTable];
        let newCurrObj = { ...copiedArr[rowData.index], [column.field]: eValue }
        copiedArr[rowData.index] = newCurrObj;
        if (rowData.index == tabProperties.colorCodeTable.length - 1) {
            copiedArr = [...copiedArr, { index: copiedArr.length, uCode: copiedArr.length, CodeColor: { r: 255, g: 255, b: 255, a: 255 } }]
        }
        setPropertiesCallback("colorCodeTable", copiedArr);
        setCurrStatePropertiesCallback("colorCodeTable", copiedArr);
    }
    const getDynamicColumns = () => {
        let finalColumns = [
            { field: 'uCode', header: 'Code' },
            { field: 'CodeColor', header: 'Color' },
        ]
        return finalColumns;
    }
    const getColumnTemplate = (rowData: any, column: any) => {
        switch (column.field) {
            case 'CodeColor':
                return <ColorPickerCtrl alpha={true} value={rowData[column.field]} onChange={(e) => { onCellChange(e.value, rowData, column) }} />
            case 'uCode':
                return <InputNumber className="form__narrow-input" value={rowData[column.field]} onChange={(e) => { onCellChange(e.value, rowData, column) }} />
        }
    }

    return <div className="form__flex-and-row-between">
        <div style={{ width: '50%' }}>
            <DataTable showGridlines scrollable scrollHeight={`${globalSizeFactor * 25}vh`} size='small' value={tabProperties.colorCodeTable} editMode="cell">
                {getDynamicColumns()?.map(({ field, header }) => {
                    return <Column key={header} field={field} header={header} body={getColumnTemplate} />
                })}
            </DataTable>
        </div>
        <div className="form__column-container" style={{ justifyContent: 'space-between' }}>
            {isTraversabilirtyLayer() && <div className="form__flex-and-row-between form__items-center form__disabled">
                <span>Num Traversability Directions:</span>
                <InputNumber value={tabProperties.numTraversabilityDirections} />
            </div>}

            <div className="form__flex-and-row-between">
                <Button label='Clear' onClick={() => { setPropertiesCallback('colorCodeTable', [{ index: 0, uCode: 0, CodeColor: { r: 255, g: 255, b: 255, a: 255 } }]) }} />
                <Button label='Get Color Table' onClick={handleGetColorTableClick} />
                <Button label='Set Color Table' onClick={handleSetColorTableClick} />
            </div>
        </div>
    </div>
}
