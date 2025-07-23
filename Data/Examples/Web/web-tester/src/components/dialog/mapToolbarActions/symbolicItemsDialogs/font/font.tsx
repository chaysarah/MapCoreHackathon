import { Checkbox, CheckboxChangeEvent } from "primereact/checkbox";
import { Fieldset } from "primereact/fieldset";
import { ChangeEvent, useEffect, useState } from "react";
import { InputNumber } from "primereact/inputnumber";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { TabPanel, TabView } from "primereact/tabview";
import { ConfirmDialog } from "primereact/confirmdialog";
import { useDispatch, useSelector } from "react-redux";
import { Divider } from "primereact/divider";

import './styles/font.css';
import SpecialCharsTable from "./specialCharsTable";
import UploadFilesCtrl, { UploadTypeEnum } from "../../../shared/uploadFilesCtrl";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../redux/combineReducer";
import { setVirtualFSSerialNumber } from "../../../../../redux/MapCore/mapCoreAction";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";

export enum FontDialogActionMode {
    create = 1,
    reCreate,
    update,
}

export default function Font(props: {
    getFont: (font: MapCore.IMcFont, isSetAsDefault: boolean) => void,
    defaultFont: MapCore.IMcFileFont,
    isSetAsDefaultCheckbox: boolean,
    actionMode: FontDialogActionMode,
}) {
    const getTable = (tableType: string): { id: number, to: string, from: string }[] => {
        let charRanges: MapCore.IMcFont.SCharactersRange[] = props.defaultFont.GetCharactersRanges()
        let tableRanges: { id: number, to: string, from: string }[] = [];
        charRanges.forEach((row, index) => {
            let newRow = {
                id: index,
                to: tableType == 'lettersRanges' ? String.fromCharCode(row.nTo) : `${row.nTo}`,
                from: tableType == 'lettersRanges' ? String.fromCharCode(row.nFrom) : `${row.nFrom}`
            };
            tableRanges = [...tableRanges, newRow]
        })
        return tableRanges;
    }
    const getFileFontFields = () => {
        let pFontFile: { Value?: MapCore.SMcFileSource } = {};
        let pnFontHeight: { Value?: number } = {};
        props.defaultFont.GetFontFileAndHeight(pFontFile, pnFontHeight);
        return {
            fileFontBuffer: pFontFile.Value?.aFileMemoryBuffer,
            fontHeightFileFont: pnFontHeight.Value,
            isMemoryBuffer: pFontFile.Value?.bIsMemoryBuffer,
            fileNameFileFont: pFontFile.Value?.strFileName,
        };
    }
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const virtualFSSerialNumber = useSelector((state: AppState) => state.mapCoreReducer.virtualFSSerialNumber)
    const [fontFormData, setFontFormData] = useState({
        isConfirmDialogVisible: false,
        confirmDialogHeader: '',
        confirmDialogMessage: '',
        lettersRanges: [{ id: 0, to: '', from: '' }],
        decimalRanges: [{ id: 0, to: '', from: '' }],
        isFromList: false,
        activeTabIndex: 1,
        //File Font fields
        fileFontBuffer: null,
        fileNameFileFont: '',
        fontHeightFileFont: 10,
        isMemoryBuffer: false,
        //Font Fields
        isUseExisting: true,
        isStaticFont: true,
        maxNumCharsInDynamicAtlas: 0,
        outlineWidth: 1,
        isSetAsDefault: false,
        specialCharsTable: [],
        pbUseSpecialCharsColors: false,
    })

    const columns = [{ field: 'from', header: 'From' }, { field: 'to', header: 'To' }];
    const fontTypes = ['Log Font', 'File Font', 'From List'];

    useEffect(() => {
        runCodeSafely(() => {
            const root = document.documentElement;
            let pixelWidth = window.innerHeight * 0.7 * globalSizeFactor;
            root.style.setProperty('--font-dialog-width', `${pixelWidth}px`);
            if ((props.actionMode == FontDialogActionMode.reCreate || props.actionMode == FontDialogActionMode.update) && props.defaultFont) {
                let fileFontFields = getFileFontFields();
                let specialCharsTable = [];
                let pbUseSpecialCharsColors: { Value?: boolean } = {};
                props.defaultFont.GetSpecialChars(specialCharsTable, pbUseSpecialCharsColors);

                let defaultsByFont = {
                    lettersRanges: getTable('lettersRanges'),
                    decimalRanges: getTable('decimalRanges'),
                    //File Font fields
                    fileFontBuffer: fileFontFields.fileFontBuffer,
                    fontHeightFileFont: fileFontFields.fontHeightFileFont,
                    isMemoryBuffer: fileFontFields.isMemoryBuffer,
                    fileNameFileFont: fileFontFields.fileNameFileFont || '',
                    //Font Fields
                    isUseExisting: props.defaultFont.IsCreatedWithUseExisting(),
                    isStaticFont: props.defaultFont.GetIsStaticFont(),
                    maxNumCharsInDynamicAtlas: props.defaultFont.GetMaxNumCharsInDynamicAtlas(),
                    outlineWidth: props.defaultFont.GetTextOutlineWidth(),
                    specialCharsTable: specialCharsTable,
                    pbUseSpecialCharsColors: pbUseSpecialCharsColors.Value,
                }
                setFontFormData({ ...fontFormData, ...defaultsByFont });
            }
        }, 'font.useEffect')
    }, [])
    const saveData = (event: any) => {
        runCodeSafely(() => {
            setFontFormData({ ...fontFormData, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
        }, "OverlayForms/objects.saveData => onChange")
    }

    //Handle Funcs
    const handleCreateFontClick = () => {
        runCodeSafely(() => {
            if (fontFormData.fileNameFileFont) {
                let slicedArr = fontFormData.decimalRanges.slice(0, fontFormData.lettersRanges.length - 1);
                let charRangeArr: MapCore.IMcFont.SCharactersRange[] = slicedArr.map(range => {
                    let charRange = new MapCore.IMcFont.SCharactersRange();
                    charRange.nFrom = parseInt(range.from);
                    charRange.nTo = parseInt(range.to);
                    return charRange;
                });
                let stringOrBuffer = fontFormData.isMemoryBuffer ? fontFormData.fileFontBuffer : fontFormData.fileNameFileFont;
                let SMcFileSource = new MapCore.SMcFileSource(stringOrBuffer, fontFormData.isMemoryBuffer);
                let isExistingUsed: { Value?: boolean } = {};
                let fileFont = null;
                runMapCoreSafely(() => {
                    fileFont = MapCore.IMcFileFont.Create(SMcFileSource,
                        fontFormData.fontHeightFileFont, fontFormData.isStaticFont, charRangeArr,
                        fontFormData.maxNumCharsInDynamicAtlas, fontFormData.isUseExisting, isExistingUsed,
                        fontFormData.outlineWidth, fontFormData.specialCharsTable, fontFormData.pbUseSpecialCharsColors);
                }, 'Font.handleCreateFontClick => IMcFileFont.Create', true)
                if (isExistingUsed.Value) {
                    setFontFormData({ ...fontFormData, isConfirmDialogVisible: true, confirmDialogHeader: 'Font Dialog', confirmDialogMessage: 'This text is existing used based, Existing Used Result' })
                }
                if (fileFont) {
                    props.getFont(fileFont, fontFormData.isSetAsDefault);
                }
                else {
                    setFontFormData({ ...fontFormData, isConfirmDialogVisible: true, confirmDialogHeader: 'Font Dialog', confirmDialogMessage: 'Font not created' })
                }
            }
            else {
                setFontFormData({ ...fontFormData, isConfirmDialogVisible: true, confirmDialogHeader: 'Font Dialog', confirmDialogMessage: 'File Name is Missing' })
            }
        }, 'Font.handleCreateFontClick')
    }
    const handleUpdateFontClick = () => {
        runCodeSafely(() => {
            if (fontFormData.fileNameFileFont) {
                let updatedFileFont = props.defaultFont;
                let stringOrBuffer = fontFormData.isMemoryBuffer ? fontFormData.fileFontBuffer : fontFormData.fileNameFileFont;
                let SMcFileSource = new MapCore.SMcFileSource(stringOrBuffer, fontFormData.isMemoryBuffer);
                updatedFileFont.SetFontFileAndHeight(SMcFileSource, fontFormData.fontHeightFileFont)
                props.getFont(updatedFileFont, fontFormData.isSetAsDefault);
            }
            else {
                setFontFormData({ ...fontFormData, isConfirmDialogVisible: true, confirmDialogHeader: 'Font Dialog', confirmDialogMessage: 'File Name is Missing' })
            }
        }, 'font.handleUpdateFontClick')
    }
    const handleFontFileSelected = (virtualFSPath: string, selectedOption: UploadTypeEnum) => {
        runCodeSafely(() => {
            let uint8Array = MapCore.IMcMapDevice.GetFileSystemFileContents(virtualFSPath);
            setFontFormData({ ...fontFormData, fileFontBuffer: uint8Array, fileNameFileFont: virtualFSPath })
        }, "font.handleFontFileSelected => onChange")
    }
    const handleIsBufferCheckboxChange = (e: CheckboxChangeEvent) => {
        runCodeSafely(() => {
            if (props.defaultFont && fontFormData.fileFontBuffer) {
                let finalSFName = fontFormData.fileNameFileFont != '' ?
                    `${virtualFSSerialNumber}/${fontFormData.fileNameFileFont.split('/')[1]}` :
                    `${virtualFSSerialNumber}/fileFont.ttf`;
                runMapCoreSafely(() => {
                    MapCore.IMcMapDevice.CreateFileSystemDirectory(`${virtualFSSerialNumber}`);
                }, 'font.handleIsBufferCheckboxChange => IMcMapDevice.CreateFileSystemFile', true)
                dispatch(setVirtualFSSerialNumber(virtualFSSerialNumber + 1));
                runMapCoreSafely(() => {
                    MapCore.IMcMapDevice.CreateFileSystemFile(finalSFName, fontFormData.fileFontBuffer);
                }, 'font.handleIsBufferCheckboxChange => IMcMapDevice.CreateFileSystemFile', true)
                setFontFormData({ ...fontFormData, fileNameFileFont: finalSFName, fileFontBuffer: null, isMemoryBuffer: e.checked })
            }
            else if (props.defaultFont) {
                let fileBuffer;
                runMapCoreSafely(() => {
                    fileBuffer = MapCore.IMcMapDevice.GetFileSystemFileContents(fontFormData.fileNameFileFont);
                }, 'font.handleIsBufferCheckboxChange => IMcMapDevice.GetFileSystemFileContents', true)
                runMapCoreSafely(() => {
                    objectWorldTreeService.deleteSystemDirectories([fontFormData.fileNameFileFont])
                }, 'font.handleIsBufferCheckboxChange => IMcMapDevice.DeleteFileSystemFile', true)
                setFontFormData({ ...fontFormData, fileNameFileFont: fontFormData.fileNameFileFont, fileFontBuffer: fileBuffer, isMemoryBuffer: e.checked })
            }
            else {
                setFontFormData({ ...fontFormData, isMemoryBuffer: e.checked })
            }
        }, 'font.handleIsBufferCheckboxChange')
    }
    const handleTabClick = (tabIndex: number) => {
        if (tabIndex == 2) {
            setFontFormData({ ...fontFormData, activeTabIndex: tabIndex, isFromList: true })
        }
        else {
            setFontFormData({ ...fontFormData, activeTabIndex: tabIndex, isFromList: false })
        }
    }

    //#region Font TabViews
    const getLogFontFrm = () => {
        return <div>
            Log Font
        </div>
    }
    const getFileFontFrm = () => {
        return <div style={{ margin: '1%' }} className="form__flex-and-row-between">
            <span style={{ width: '40%' }} className="form__flex-and-row-between form__items-center">
                <div style={{ width: '40%' }}>File Name </div>
                <UploadFilesCtrl isDirectoryUpload={false} uploadOptions={[UploadTypeEnum.upload]} getVirtualFSPath={handleFontFileSelected} accept='.ttf' existInputValue={fontFormData.fileNameFileFont} />
            </span>
            <span style={{ width: '25%' }} className="form__flex-and-row-between form__items-center">
                <div>Font Height: </div>
                <InputNumber className="form__medium-width-input" value={fontFormData.fontHeightFileFont} name='fontHeightFileFont' onChange={e => setFontFormData({ ...fontFormData, fontHeightFileFont: e.value })} />
            </span>
            <div style={{ width: '25%' }} className="form__flex-and-row form__items-center">
                <Checkbox name='isMemoryBuffer' inputId="isMemoryBuffer" onChange={handleIsBufferCheckboxChange} checked={fontFormData.isMemoryBuffer} />
                <label htmlFor="isMemoryBuffer">Is Memory Buffer</label>
            </div>
        </div>
    }
    const getFromListFrm = () => {
        return <div>
            From List
        </div>
    }
    let fontTypesFuncs: any = {
        'Log Font': getLogFontFrm,
        'File Font': getFileFontFrm,
        'From List': getFromListFrm,
    }
    //#endregion
    const onLettersCellEditComplete = (e: ChangeEvent<HTMLInputElement>, rowData: { id: number, to: string, from: string }, column: any) => {
        runCodeSafely(() => {
            if (e.target.value.length <= 1) {
                let isRowAddNeeds = fontFormData.lettersRanges[rowData.id].from == '' &&
                    fontFormData.lettersRanges[rowData.id].to == '' &&
                    !fontFormData.lettersRanges[rowData.id + 1] && e.target.value;
                const updatedData = isRowAddNeeds ? [...fontFormData.lettersRanges, { id: fontFormData.lettersRanges.length, to: '', from: '' }] : [...fontFormData.lettersRanges];
                updatedData[rowData.id][column.field] = e.target.value;
                let newDecimalValue = e.target.value.length > 1 ? '0' : e.target.value.length === 0 ? '' : e.target.value.charCodeAt(0);
                const decimalData = isRowAddNeeds ? [...fontFormData.decimalRanges, { id: fontFormData.decimalRanges.length, to: '', from: '' }] : [...fontFormData.decimalRanges];
                decimalData[rowData.id][column.field] = newDecimalValue;
                setFontFormData({ ...fontFormData, decimalRanges: decimalData, lettersRanges: updatedData })
            }
        }, 'font.onLettersCellEditComplete => onChange')
    };
    const onDecimalCellEditComplete = (e: ChangeEvent<HTMLInputElement>, rowData: { id: number, to: string, from: string }, column: any) => {
        runCodeSafely(() => {
            let isRowAddNeeds = fontFormData.lettersRanges[rowData.id].from == '' &&
                fontFormData.lettersRanges[rowData.id].to == '' &&
                !fontFormData.lettersRanges[rowData.id + 1] && e.target.value;
            const updatedData = isRowAddNeeds ? [...fontFormData.decimalRanges, { id: fontFormData.decimalRanges.length, to: '', from: '' }] : [...fontFormData.decimalRanges];
            updatedData[rowData.id][column.field] = e.target.value;
            let newIntValue = parseInt(e.target.value, 10);
            const canBeConverted = Number.isSafeInteger(newIntValue) && newIntValue >= -32768 && newIntValue <= 32767;
            let newLetterValue = !isNaN(newIntValue) && canBeConverted ? String.fromCharCode(newIntValue) : '0';
            const lettersData = isRowAddNeeds ? [...fontFormData.lettersRanges, { id: fontFormData.lettersRanges.length, to: '', from: '' }] : [...fontFormData.lettersRanges];
            lettersData[rowData.id][column.field] = newLetterValue;
            setFontFormData({ ...fontFormData, decimalRanges: updatedData, lettersRanges: lettersData })
        }, 'font.onDecimalCellEditComplete => onChange')
    }

    //DOM Functions
    const letterFieldTemplate = (rowData: { id: number, to: string, from: string }, column: any) => {
        return <InputText disabled={fontFormData.isFromList || props.actionMode == FontDialogActionMode.update ? true : false} type="text" value={rowData[column.field]} onChange={(e) => { onLettersCellEditComplete(e, rowData, column); }} />;
    }
    const decimalFieldTemplate = (rowData: { id: number, to: string, from: string }, column: any) => {
        return <InputText disabled={fontFormData.isFromList || props.actionMode == FontDialogActionMode.update ? true : false} type="text" value={rowData[column.field]} onChange={(e) => { onDecimalCellEditComplete(e, rowData, column); }} />;
    }
    const getFontParamsFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend={<legend style={{ color: fontFormData.isFromList ? '#808080ba' : 'black' }}>Font Params</legend>}>
            <div className="form__flex-and-row-between">
                <div style={{ width: '48%' }} className="form__column-container">
                    <div className="form__flex-and-row">
                        <Checkbox disabled={fontFormData.isFromList || props.actionMode == FontDialogActionMode.update ? true : false} name='isUseExisting' inputId="useExisting" onChange={saveData} checked={fontFormData.isUseExisting} />
                        <label style={{ color: fontFormData.isFromList || props.actionMode == FontDialogActionMode.update ? '#808080ba' : 'black', paddingLeft: `${globalSizeFactor * 0.5}vh` }} htmlFor="useExisting">Use Existing</label>
                    </div>
                    <div className="form__flex-and-row">
                        <Checkbox disabled={fontFormData.isFromList || props.actionMode == FontDialogActionMode.update ? true : false} name='isStaticFont' inputId="staticFont" onChange={saveData} checked={fontFormData.isStaticFont} />
                        <label style={{ color: fontFormData.isFromList || props.actionMode == FontDialogActionMode.update ? '#808080ba' : 'black', paddingLeft: `${globalSizeFactor * 0.5}vh` }} htmlFor="staticFont">is Static Font</label>
                    </div>
                    <div className="font__params-input">
                        <label style={{ color: fontFormData.isFromList || props.actionMode == FontDialogActionMode.update ? '#808080ba' : 'black' }} htmlFor='characterSpacing'>Character Spacing: </label>
                        <InputNumber className="form__medium-width-input" disabled id='characterSpacing' value={1} />
                    </div>
                    <div className="font__params-input">
                        <label style={{ color: fontFormData.isFromList || props.actionMode == FontDialogActionMode.update ? '#808080ba' : 'black' }} htmlFor='numAntialiasingAlphaLevels'>Num Antialiasing Alpha Levels (2-256): </label>
                        <InputNumber className="form__medium-width-input" disabled id='numAntialiasingAlphaLevels' value={256} />
                    </div>
                    <div className="font__params-input">
                        <label style={{ color: fontFormData.isFromList || props.actionMode == FontDialogActionMode.update ? '#808080ba' : 'black' }} htmlFor='maxNumCharsInDynamicAtlas'>Max Num Chars In Dynamic Atlas: </label>
                        <InputNumber className="form__medium-width-input" disabled={fontFormData.isFromList || props.actionMode == FontDialogActionMode.update ? true : false} id='maxNumCharsInDynamicAtlas' value={fontFormData.maxNumCharsInDynamicAtlas} name='maxNumCharsInDynamicAtlas' onValueChange={saveData} />
                    </div>
                    <div className="font__params-input">
                        <label style={{ color: fontFormData.isFromList || props.actionMode == FontDialogActionMode.update ? '#808080ba' : 'black' }} htmlFor='outlineWidth'>Outline Width: </label>
                        <InputNumber className="form__medium-width-input" disabled={fontFormData.isFromList || props.actionMode == FontDialogActionMode.update ? true : false} id='outlineWidth' value={fontFormData.outlineWidth} name='outlineWidth' onValueChange={saveData} />
                    </div>
                </div>
                <Divider layout="vertical" />
                <div style={{ width: '48%' }} className="form__column-container">
                    <div style={{ paddingBottom: '2%' }}>Characters Ranges (Insert range according to the table type): </div>
                    <div style={{ height: `${globalSizeFactor * 14}vh`, overflowY: 'auto' }} className="card p-fluid form__flex-and-row-between">
                        <div>Ranges using letters :
                            <DataTable size='small' tableStyle={{ minWidth: `${globalSizeFactor * 13}vh`, maxWidth: `${globalSizeFactor * 13}vh` }} value={fontFormData.lettersRanges} editMode="cell" >
                                {columns.map(({ field, header }) => {
                                    return <Column key={field} field={field} header={header} className="font__column" body={letterFieldTemplate} />;
                                })}
                            </DataTable>
                        </div>
                        <div> Ranges using decimal value :
                            <DataTable size='small' tableStyle={{ minWidth: `${globalSizeFactor * 13}vh`, maxWidth: `${globalSizeFactor * 13}vh` }} value={fontFormData.decimalRanges} editMode="cell" >
                                {columns.map(({ field, header }) => {
                                    return <Column key={field} field={field} header={header} className="font__column" body={decimalFieldTemplate} />;
                                })}
                            </DataTable>
                        </div>
                    </div>
                </div>
            </div>
            <SpecialCharsTable deaultTable={fontFormData.specialCharsTable} useSpecialCharsColor={fontFormData.pbUseSpecialCharsColors} isFromList={fontFormData.isFromList} actionMode={props.actionMode} getTable={(table: MapCore.IMcFont.SSpecialChar[], useSpecialCharsColors: boolean) => {
                setFontFormData(prev => ({
                    ...prev,
                    specialCharsTable: table,
                    pbUseSpecialCharsColors: useSpecialCharsColors
                }));
            }} />
        </Fieldset >
    }
    const getGenerateButton = () => {
        switch (props.actionMode) {
            case FontDialogActionMode.create:
                return <Button onClick={handleCreateFontClick} label="Create" />;
            case FontDialogActionMode.update:
                return <Button onClick={handleUpdateFontClick} label="Update" />;
            case FontDialogActionMode.reCreate:
                return <Button onClick={handleCreateFontClick} label="Re-Create" />;
            default:
                break;
        }
    }

    return (
        <div className="form__column-container">
            <TabView scrollable className="font__tab-view" activeIndex={fontFormData.activeTabIndex} onTabChange={e => handleTabClick(e.index)}>
                {fontTypes.map((item: string, key: number) => {
                    return <TabPanel header={item} key={key}>
                        {fontTypesFuncs[item]()}
                    </TabPanel>
                })}
            </TabView>
            {getFontParamsFieldset()}
            <div className="font__footer form__items-center">
                <Button disabled label='Use Selected' />

                {getGenerateButton()}

                {props.isSetAsDefaultCheckbox &&
                    <div className="font__is-as-default-div form__items-center">
                        <Checkbox name='isSetAsDefault' inputId="isSetAsDefault" onChange={saveData} checked={fontFormData.isSetAsDefault} />
                        <label htmlFor="isSetAsDefault" className="ml-2">Set As Default</label>
                    </div>}
            </div>
            <ConfirmDialog
                contentClassName='form__confirm-dialog-content'
                message={fontFormData.confirmDialogMessage}
                header={fontFormData.confirmDialogHeader}
                footer={<div></div>}
                visible={fontFormData.isConfirmDialogVisible}
                onHide={e => { setFontFormData({ ...fontFormData, isConfirmDialogVisible: false }) }}
            />
        </div>
    )
}