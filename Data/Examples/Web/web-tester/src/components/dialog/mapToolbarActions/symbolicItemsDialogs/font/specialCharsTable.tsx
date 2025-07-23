import { Button } from "primereact/button"
import { Column } from "primereact/column"
import { DataTable } from "primereact/datatable"
import { Dropdown } from "primereact/dropdown"
import { InputNumber } from "primereact/inputnumber"
import { InputText } from "primereact/inputtext"
import { Fieldset } from "primereact/fieldset"
import { Checkbox } from "primereact/checkbox"
import { useEffect, useMemo, useState } from "react"
import { useSelector } from "react-redux"
import { Dialog } from "primereact/dialog"
import _ from "lodash";

import { getEnumDetailsList, getEnumValueDetails } from "mapcore-lib"
import { FontDialogActionMode } from "./font"
import ImageDialog from "./image"
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler"
import { AppState } from "../../../../../redux/combineReducer"

export default function SpecialCharsTable(props: {
    deaultTable: MapCore.IMcFont.SSpecialChar[],
    useSpecialCharsColor: boolean,
    isFromList: boolean,
    actionMode: FontDialogActionMode,
    getTable: (table: MapCore.IMcFont.SSpecialChar[], useSpecialCharsColors: boolean) => void
}) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [specialChars, setSpecialChars] = useState<MapCore.IMcFont.SSpecialChar[]>(props.deaultTable || []);
    const [imageDialogVisible, setImageDialogVisible] = useState<{ visible: boolean, index: number }>({ visible: false, index: -1 });
    const [useSpecialCharsColors, setUseSpecialCharsColors] = useState(false);

    const sizeParamMeaningEnum = useMemo(() => getEnumDetailsList(MapCore.IMcFont.ESpecialCharSizeMeaning), [])
    const specialCharsColumns = useMemo(() => [
        { field: 'index', header: '.No', columnWidth: 5 },
        { field: 'charCodeLetter', header: 'Char Code (letter)', columnWidth: 6.6 },
        { field: 'uCharCode', header: 'Char Code (decimal value)', columnWidth: 6.6 },
        { field: 'pImage', header: 'Image', columnWidth: 7.6 },
        { field: 'eSizeParamMeaning', header: 'Size Param Meaning', columnWidth: 19 },
        { field: 'fCharWidthParam', header: 'Char Width Param', columnWidth: 6.6 },
        { field: 'fCharHeightParam', header: 'Char Height Param', columnWidth: 6.6 },
        { field: 'nVerticalOffset', header: 'Vertical Offset', columnWidth: 6.6 },
        { field: 'nLeftSpacing', header: 'Left Spacing', columnWidth: 6.6 },
        { field: 'nRightSpacing', header: 'Right Spacing', columnWidth: 6.6 },
        { field: 'actions', header: '', columnWidth: 5 },
    ], [])

    //#region useEffect Hooks
    useEffect(() => {
        runCodeSafely(() => {
            if (props.deaultTable) {
                setSpecialChars(props.deaultTable);
            }
        }, 'SpecialCharsTable.useEffect');
    }, [props.deaultTable]);
    useEffect(() => {
        runCodeSafely(() => {
            if (props.useSpecialCharsColor) {
                setUseSpecialCharsColors(props.useSpecialCharsColor);
            }
        }, 'SpecialCharsTable.useEffect');
    }, [props.useSpecialCharsColor]);
    useEffect(() => {
        runCodeSafely(() => {
            if (!_.isEqualWith(props.deaultTable, specialChars, (objValue, srcValue) => objValue == srcValue)) {
                props.getTable(specialChars, useSpecialCharsColors);
            }
        }, 'SpecialCharsTable.useEffect');
    }, [specialChars]);
    useEffect(() => {
        runCodeSafely(() => {
            if (useSpecialCharsColors !== props.useSpecialCharsColor) {
                props.getTable(specialChars, useSpecialCharsColors);
            }
        }, 'SpecialCharsTable.useEffect');
    }, [useSpecialCharsColors]);
    //#endregion

    // #region Handle Functions
    const handleDeleteRowClick = (index: number) => {
        runCodeSafely(() => {
            setSpecialChars(prev => {
                const arr = [...prev];
                arr.splice(index, 1);
                return arr;
            });
        }, "specialCharsTable.handleDeleteRowClick => onClick");
    }
    const handlePlusRowClick = () => {
        runCodeSafely(() => {
            const newSpecialChar = new MapCore.IMcFont.SSpecialChar();
            setSpecialChars(prev => [...prev, newSpecialChar]);
        }, "specialCharsTable.handlePlusRowClick => onClick");
    }
    const handleCellChange = (eValue: any, rowData: any, column: any) => {
        runCodeSafely(() => {
            let copiedArr = [...specialChars];
            copiedArr[rowData.index][column.field] = eValue;
            setSpecialChars(copiedArr);
        }, "specialCharsTable.handleCellChange => onChange");
    }
    const handleLetterChange = (eValue: string, rowData: any, column: any) => {
        runCodeSafely(() => {
            if (eValue.length <= 1) {
                const charCode = eValue.length == 0 ? 0 : eValue.charCodeAt(0);
                let copiedArr = [...specialChars];
                copiedArr[rowData.index].uCharCode = charCode;
                setSpecialChars(copiedArr);
            }
        }, "specialCharsTable.handleLetterChange => onChange");
    }
    const handleGetImage = (image: MapCore.IMcImage) => {
        runCodeSafely(() => {
            if (imageDialogVisible.index >= 0) {
                let copiedArr = [...specialChars];
                copiedArr[imageDialogVisible.index].pImage = image;
                setSpecialChars(copiedArr);
            }
            setImageDialogVisible({ visible: false, index: -1 });
        }, "specialCharsTable.handleGetImage => onGetImage");
    }
    //#endregion

    //#region Field Template
    function getImageButtonLabel(rowData, columnField) {
        const fieldValue = rowData[columnField];

        if (!fieldValue) return "Create Image";

        const fileSource = (fieldValue as MapCore.IMcImage).GetFileSource?.();

        if (fileSource?.strFileName) {
            return fileSource.strFileName.split('/').at(-1);
        }

        return "Selected";
    }
    const specialCharsFieldTemplate = (rowData: any, column: any) => {
        const isZeroCharCode = column.field == 'charCodeLetter' && rowData[column.field] == String.fromCharCode(0);

        switch (column.field) {
            case 'index':
                return <span style={{ minWidth: `${globalSizeFactor * 1}vh` }} >{rowData[column.field] + 1}</span>
            case 'charCodeLetter':
                return <InputText style={{ width: `${globalSizeFactor * 6}vh` }} value={isZeroCharCode ? '' : rowData[column.field]} onChange={e => handleLetterChange(e.target.value, rowData, column)} />;
            case 'actions':
                return <Button icon='pi pi-trash' onClick={e => handleDeleteRowClick(rowData.index)} />;
            case 'eSizeParamMeaning':
                return <Dropdown options={sizeParamMeaningEnum} value={getEnumValueDetails(rowData[column.field], sizeParamMeaningEnum)} optionLabel="name" onChange={(e) => { handleCellChange(e.value.theEnum, rowData, column) }} />;
            case 'pImage':
                return <Button style={{ width: `${globalSizeFactor * 7.6}vh`, whiteSpace: 'normal', wordBreak: 'break-word' }} onClick={e => setImageDialogVisible({ visible: true, index: rowData.index })} label={getImageButtonLabel(rowData, column.field)} />;
            case 'uCharCode':
            case 'fCharWidthParam':
            case 'fCharHeightParam':
            case 'nVerticalOffset':
            case 'nLeftSpacing':
            case 'nRightSpacing':
                return <InputNumber value={rowData[column.field]} onChange={(e) => { handleCellChange(e.value, rowData, column) }} className="form__medium-width-input" />;
        }
    }
    const getPlusIcon = () => {
        return <Button icon='pi pi-plus-circle' onClick={handlePlusRowClick} />
    }
    const getCharLetterByNum = (num: number) => {
        let letterChar = '';
        runCodeSafely(() => {
            letterChar = String.fromCharCode(num);
        }, 'SpecialCharsTable.getCharLetterByNum');
        return letterChar;
    }
    //#endregion

    return <div className="form__column-container">
        <Fieldset legend="Special Characters" className={`form__column-fieldset ${props.isFromList || props.actionMode === FontDialogActionMode.update ? ' form__disabled' : ''}`}>
            <div className="form__flex-and-row form__items-center">
                <Checkbox name='useSpecialChars' inputId="useSpecialChars" onChange={e => setUseSpecialCharsColors(e.checked)} checked={useSpecialCharsColors} />
                <label htmlFor="useSpecialChars">Use Special Chars Colors</label>
            </div>
            <DataTable tableStyle={{ minWidth: `90%` }} showGridlines scrollable scrollHeight={`${globalSizeFactor * 20}vh`} size='small' value={specialChars.map((obj, i) => ({ ...obj, index: i, actions: null, charCodeLetter: getCharLetterByNum(obj.uCharCode) }))} editMode="cell" >
                {specialCharsColumns.map(({ field, header, columnWidth }) => {
                    return <Column
                        style={{ width: `${globalSizeFactor * columnWidth}vh` }}
                        key={field}
                        field={field}
                        header={field == 'actions' ? getPlusIcon() : <div style={{ width: `${globalSizeFactor * (columnWidth - 0.3)}vh` }} >{header}</div>}
                        body={specialCharsFieldTemplate} />;
                })}
            </DataTable>
        </Fieldset>

        <Dialog
            visible={imageDialogVisible.visible}
            header='Image Dialog'
            onHide={() => setImageDialogVisible({ visible: false, index: -1 })}
            className='scroll-dialog-image'
            modal={true}>
            <ImageDialog getImage={handleGetImage} defaultImage={imageDialogVisible.index != -1 && specialChars[imageDialogVisible.index].pImage} />
        </Dialog>
    </div>

}