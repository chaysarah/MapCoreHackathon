
import { ChangeEvent, useEffect, useMemo, useState } from 'react';
import { Checkbox } from 'primereact/checkbox';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { useSelector } from 'react-redux';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { RadioButton } from 'primereact/radiobutton';
import { SelectButton } from 'primereact/selectbutton';
import { Fieldset } from 'primereact/fieldset';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';

import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { Properties } from "../../../../dialog";
import ColorPickerCtrl from '../../../../../shared/colorPicker';
import { runCodeSafely } from '../../../../../../common/services/error-handling/errorHandler';
import { runMapCoreSafely } from '../../../../../../common/services/error-handling/errorHandler';
import TexturePropertiesBase from '../../../../../../propertiesBase/texturePropertiesBase';
import TextureService, { TextureTypeEnum } from '../../../../../../services/texture.service';
import { AppState } from '../../../../../../redux/combineReducer';

export enum MemoryBufferTextureOptionsEnum {
    BUFFER,
    COLOR_ARRAY
}
export class MemoryBufferProperties implements Properties {
    selectedOption: MemoryBufferTextureOptionsEnum;
    //Buffer Properties
    bmpFile: string;
    srcFormat: string;
    srcWidth: string;
    srcHeight: string;
    textureUseage: { name: string; code: number; theEnum: any; };
    autoMipmap: boolean;
    pixelFormat: { name: string; code: number; theEnum: any; };
    width: number;
    height: number;
    rowPitch: number;
    mBtextureBuffer: Uint8Array;
    //Color Array Properties
    isColorInterpolation: boolean;
    colorColumns: boolean;
    colorArrPixelFormat: { name: string; code: number; theEnum: any; };
    colorArrayColors: MapCore.SMcBColor[];
    colorPositions: Float32Array;

    static getDefault(props: any): MemoryBufferProperties {
        let { tabInfo } = props;
        const enumDetails = {
            pixelFormats: getEnumDetailsList(MapCore.IMcTexture.EPixelFormat),
            usage: getEnumDetailsList(MapCore.IMcTexture.EUsage),
        };

        return {
            selectedOption: tabInfo.properties.memoryBufferProperties.selectedOption || MemoryBufferTextureOptionsEnum.BUFFER,
            //Buffer Properties
            bmpFile: tabInfo.properties.memoryBufferProperties.bmpFile || "",
            srcFormat: tabInfo.properties.memoryBufferProperties.srcFormat || "",
            srcWidth: tabInfo.properties.memoryBufferProperties.srcWidth || "",
            srcHeight: tabInfo.properties.memoryBufferProperties.srcHeight || "",
            textureUseage: tabInfo.properties.memoryBufferProperties.textureUseage || getEnumValueDetails(MapCore.IMcTexture.EUsage.EU_STATIC, enumDetails.usage),
            autoMipmap: tabInfo.properties.memoryBufferProperties.autoMipmap || true,
            pixelFormat: tabInfo.properties.memoryBufferProperties.pixelFormat || { code: -1, name: '', theEnum: null },
            width: tabInfo.properties.memoryBufferProperties.width || 0,
            height: tabInfo.properties.memoryBufferProperties.height || 0,
            rowPitch: tabInfo.properties.memoryBufferProperties.rowPitch || 0,
            mBtextureBuffer: tabInfo.properties.mBtextureBuffer || new Uint8Array(),
            //Color Array Properties
            isColorInterpolation: tabInfo.properties.isColorInterpolation || false,
            colorColumns: false,
            colorArrPixelFormat: tabInfo.properties.memoryBufferProperties.colorArrPixelFormat || getEnumValueDetails(MapCore.IMcTexture.EPixelFormat.EPF_A8R8G8B8, enumDetails.pixelFormats),
            colorArrayColors: tabInfo.properties.memoryBufferProperties.colorArrayColors || [],
            colorPositions: tabInfo.properties.memoryBufferProperties.colorPositions || new Float32Array,
        }
    }
}

export default function MemoryBuffer(props: {
    defaultTexture: MapCore.IMcTexture,
    tabInfo: {
        properties: TexturePropertiesBase,
        setPropertiesCallback: (tab: string, key: string, value: any) => void,
    }
}) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [enumDetails] = useState({
        pixelFormats: getEnumDetailsList(MapCore.IMcTexture.EPixelFormat),
        usage: getEnumDetailsList(MapCore.IMcTexture.EUsage),
    });
    const colorsTableColumns = useMemo(() => [
        { field: 'index', header: '.No' },
        { field: 'color', header: 'Color' },
        { field: 'actions', header: '' }
    ], [])
    const colorPositionsTableColumns = useMemo(() => [
        { field: 'index', header: '.No' },
        { field: 'colorPosition', header: 'Color Position' },
        { field: 'actions', header: '' }
    ], [])

    useEffect(() => {
        runCodeSafely(() => {
            let defaultProperties = MemoryBufferProperties.getDefault(props)
            props.tabInfo.setPropertiesCallback("memoryBufferProperties", null, defaultProperties);
            TextureService.textureType = TextureTypeEnum.MemoryBuffer;
            if (props.defaultTexture) {
                let pHeight: { Value?: number } = {};
                let pWidth: { Value?: number } = {};
                runMapCoreSafely(() => { props.defaultTexture.GetSize(pWidth, pHeight); }, 'mamoryBuffer.useEffect => IMcTexture.GetSize', true)
                const srcFields = getSrcFields();
                const colorArrayFields = getColorArrayFields();
                const unionFields = {
                    ...srcFields,
                    ...colorArrayFields,
                    height: pHeight.Value,
                    width: pWidth.Value,
                    selectedOption: colorArrayFields.colorArrayColors.length > 0 ? MemoryBufferTextureOptionsEnum.COLOR_ARRAY : MemoryBufferTextureOptionsEnum.BUFFER,
                    bmpFile: colorArrayFields.colorArrayColors.length > 0 ? '' : defaultProperties.bmpFile,
                };
                for (const key of Object.keys(unionFields)) {
                    props.tabInfo.setPropertiesCallback("memoryBufferProperties", key, unionFields[key]);
                }
            }
        }, 'memoryBuffer.useEffect')
    }, [])
    //#region Help Functions
    const getColorArrayFields = () => {
        let finalColorArrayFields: any = {};
        runCodeSafely(() => {
            const memoryBufferTexture = props.defaultTexture as MapCore.IMcMemoryBufferTexture;
            let paColors: MapCore.SMcBColor[] = [];
            let pafColorPositions: { Value?: Float32Array } = {};
            let pbColorInterpolation: { Value?: boolean } = {};
            let pbColorColumns: { Value?: boolean } = {};
            memoryBufferTexture.GetColorData(paColors, pafColorPositions, pbColorInterpolation, pbColorColumns);
            finalColorArrayFields = {
                colorArrayColors: paColors,
                colorPositions: pafColorPositions.Value,
                isColorInterpolation: pbColorInterpolation.Value,
                colorColumns: pbColorColumns.Value
            };
        }, 'memoryBuffer.getColorArrayFields')
        return finalColorArrayFields;
    }
    const getSrcFields = () => {
        let finalSrcParams = {};
        runCodeSafely(() => {
            let memoryBufferTexture = props.defaultTexture as MapCore.IMcMemoryBufferTexture;
            let srcFormatEnum = memoryBufferTexture.GetSourcePixelFormat();
            let srcFormat = getEnumValueDetails(srcFormatEnum, enumDetails.pixelFormats);
            let srcWidth: { Value?: number } = {};
            let srcHeight: { Value?: number } = {};
            memoryBufferTexture.GetSourceSize(srcWidth, srcHeight);
            finalSrcParams = {
                srcHeight: `${srcHeight.Value}`,
                srcWidth: `${srcWidth.Value}`,
                srcFormat: srcFormat.name,
            }
        }, 'memoryBuffer.getSrcFields')
        return finalSrcParams;
    }
    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("memoryBufferProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
        }, "memoryBuffer.saveObjectProperties => onChange");
    }
    const getColorPositionsArray = () => {
        const colorPositionsNumberArr = Array.from(props.tabInfo.properties.memoryBufferProperties.colorPositions || []);
        const externalColorPositionsTable = colorPositionsNumberArr?.map((position, i) => ({ colorPosition: position, index: i, actions: null }))
        return externalColorPositionsTable;
    }
    //#endregion
    //#region Handle Functions
    const handleFileUpload = (e: ChangeEvent<HTMLInputElement>) => {
        runCodeSafely(() => {
            const selectedFile = e.target.files[0];
            if (selectedFile) {
                const reader = new FileReader();

                reader.onload = (event) => {
                    runCodeSafely(() => {
                        const arrayBuffer = event.target.result;
                        const uint8Array = new Uint8Array(arrayBuffer as ArrayBuffer);
                        let finalParams = {};
                        finalParams = { ...finalParams, mBtextureBuffer: uint8Array, bmpFile: e.target.files[0].name }
                        let mcImage: MapCore.IMcImage = null;
                        const fileSource = new MapCore.SMcFileSource(uint8Array, true);
                        runMapCoreSafely(() => { mcImage = MapCore.IMcImage.Create(fileSource); }, 'mamoryBuffer.handleFileUpload => IMcImage.Create', true)
                        if (props.defaultTexture) {
                            let srcFields = getSrcFields();
                            finalParams = { ...finalParams, ...srcFields }
                        }
                        let pixelFormatEnum = null;
                        runMapCoreSafely(() => { pixelFormatEnum = mcImage.GetPixelFormat(); }, 'mamoryBuffer.handleFileUpload => IMcImage.GetPixelFormat', true)
                        let pixelFormat = getEnumValueDetails(pixelFormatEnum, enumDetails.pixelFormats);
                        let pHeight: { Value?: number } = {};
                        let pWidth: { Value?: number } = {};
                        runMapCoreSafely(() => { mcImage.GetSize(pWidth, pHeight); }, 'mamoryBuffer.handleFileUpload => IMcImage.GetSize', true)
                        finalParams = {
                            ...finalParams,
                            width: pWidth.Value,
                            height: pHeight.Value,
                            pixelFormat: pixelFormat,
                        }
                        for (const key of Object.keys(finalParams)) {
                            props.tabInfo.setPropertiesCallback("memoryBufferProperties", key, finalParams[key]);
                        }
                    }, "memoryBuffer.handleFileUpload => onLoad")
                };

                reader.readAsArrayBuffer(selectedFile);
            }
        }, "memoryBuffer.handleFileUpload => onChange")
    }
    const handleColorDirectionChange = (e) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("memoryBufferProperties", 'colorColumns', e.target.value == 'Color Columns' ? true : false);
        }, "memoryBuffer.handleColorDirectionChange => onChange")
    }
    const handleCellChange = (eValue: any, rowData: any, column: any) => {
        runCodeSafely(() => {
            const tableType: string = rowData.color ? 'colorArrayColors' : 'colorPositions';
            let copiedArr = Array.from(props.tabInfo.properties.memoryBufferProperties[tableType]);
            copiedArr[rowData.index] = eValue;
            props.tabInfo.setPropertiesCallback("memoryBufferProperties", tableType, copiedArr);
        }, "memoryBuffer.handleCellChange => onChange");
    }
    const handlePlusRowClick = (tableType: string) => {
        runCodeSafely(() => {
            const newItem = tableType == 'colorPositions' ? 0 : { ...new MapCore.SMcBColor(), a: 255 };
            const copiedArr = Array.from(props.tabInfo.properties.memoryBufferProperties[tableType]);
            copiedArr.push(newItem);
            const finalArr = tableType == 'colorPositions' ? new Float32Array(copiedArr as number[]) : copiedArr;
            props.tabInfo.setPropertiesCallback("memoryBufferProperties", tableType, finalArr);
        }, "memoryBuffer.handlePlusRowClick => onClick");
    }
    const handleDeleteRowClick = (index: number, tableType: string) => {
        runCodeSafely(() => {
            let copiedArr = Array.from(props.tabInfo.properties.memoryBufferProperties[tableType]);
            copiedArr.splice(index, 1);
            const finalArr = tableType == 'colorPositions' ? new Float32Array(copiedArr as number[]) : copiedArr;
            props.tabInfo.setPropertiesCallback("memoryBufferProperties", tableType, finalArr);
        }, "memoryBuffer.handleDeleteRowClick => onClick");
    }
    //#endregion
    //#region DOM Functions
    const getBufferOptionDOM = () => {
        return <div className='form__flex-and-row-between'>
            <div className='form__column-container' style={{ width: '50%' }}>
                <Fieldset className="form__column-fieldset" legend="Buffer">
                    <div className='form__flex-and-row-between'>
                        <label>BMP File: </label>
                        <div>
                            <InputText style={{ marginBottom: `${globalSizeFactor * 0.4}vh` }} name="bmpFile" value={props.tabInfo.properties.memoryBufferProperties.bmpFile ?? ''} disabled />
                            <input accept='.bmp' type="file" id="fileInput" style={{ display: 'none' }} onChange={handleFileUpload} multiple />
                            <Button label="Load" icon="pi pi-upload" onClick={() => document.getElementById('fileInput').click()} />
                        </div>
                    </div>
                </Fieldset>
                <Fieldset className="form__column-fieldset" legend="Source">
                    <div className='form__flex-and-row-between'>
                        <label className='texture__disabled-label'>Src Pixel Format:</label>
                        <InputText disabled name="srcFormat" value={props.tabInfo.properties.memoryBufferProperties.srcFormat ?? ''} onChange={(e) => saveData(e)} />
                    </div>
                    <div className='form__flex-and-row-between'>
                        <label className='texture__disabled-label'>Src Width:</label>
                        <InputText disabled name="srcWidth" value={props.tabInfo.properties.memoryBufferProperties.srcWidth ?? ''} onChange={(e) => saveData(e)} />
                    </div>
                    <div className='form__flex-and-row-between'>
                        <label className='texture__disabled-label'>Src Height:</label>
                        <InputText disabled name="srcHeight" value={props.tabInfo.properties.memoryBufferProperties.srcHeight ?? ''} onChange={(e) => saveData(e)} />
                    </div>
                </Fieldset>
            </div>
            <Fieldset style={{ width: '50%' }} className="form__column-fieldset" legend="Texture">
                <div className={`form__flex-and-row-between ${props.defaultTexture && 'form__disabled'}`}>
                    <label>Texture Usage:</label>
                    <Dropdown className='texture__dropdown' name="textureUseage" value={props.tabInfo.properties.memoryBufferProperties.textureUseage ?? { name: '' }} onChange={saveData} options={enumDetails.usage} optionLabel="name" />
                </div>
                <div className={`form__flex-and-row ${props.defaultTexture && 'form__disabled'}`}>
                    <Checkbox inputId="autoMipmap" name="autoMipmap" onChange={saveData} checked={props.tabInfo.properties.memoryBufferProperties.autoMipmap ?? false} />
                    <label htmlFor="autoMipmap" className="texture__checkbox-div">Auto Mipmap</label>
                </div>
                <div className='form__flex-and-row-between'>
                    <label className='texture__disabled-label'>Pixel Format:</label>
                    <InputText disabled name="pixelFormat" value={props.tabInfo.properties.memoryBufferProperties.pixelFormat?.name ?? ''} />
                </div>
                <div className='form__flex-and-row-between'>
                    <label className='texture__disabled-label'>Width:</label>
                    <InputNumber disabled name="width" value={props.tabInfo.properties.memoryBufferProperties.width ?? 0} onValueChange={(e) => saveData(e)} mode="decimal" />
                </div>
                <div className='form__flex-and-row-between'>
                    <label className='texture__disabled-label'>Height:</label>
                    <InputNumber disabled name="height" value={props.tabInfo.properties.memoryBufferProperties.height ?? 0} onValueChange={(e) => saveData(e)} mode="decimal" />
                </div>
                <div className='form__flex-and-row-between'>
                    <label>Row Pitch:</label>
                    <InputNumber name="rowPitch" value={props.tabInfo.properties.memoryBufferProperties.rowPitch ?? 0} onValueChange={(e) => saveData(e)} mode="decimal" />
                </div>
            </Fieldset>

        </div>
    }
    const getColumnTemplate = (rowData: any, column: any) => {
        const tableType = rowData.color ? 'colorArrayColors' : 'colorPositions';
        switch (column.field) {
            case 'index':
                return <span style={{ minWidth: `${globalSizeFactor * 1}vh` }} >{rowData[column.field] + 1}</span>
            case 'actions':
                return <Button style={{ minWidth: `${globalSizeFactor * 3}vh` }} icon='pi pi-trash' onClick={e => handleDeleteRowClick(rowData.index, tableType)} />
            case 'color':
                return <ColorPickerCtrl style={{ width: '100%' }} name={column.field} value={rowData[column.field]} alpha onChange={(e) => { handleCellChange(e.value, rowData, column) }} />
            case 'colorPosition':
                return <InputNumber name={column.field} value={rowData[column.field]} maxFractionDigits={5} onValueChange={(e) => { handleCellChange(e.value, rowData, column) }} />
        }
    }
    const getPlusIcon = (tableType: string) => {
        return <Button icon='pi pi-plus-circle' onClick={e => handlePlusRowClick(tableType)} />
    }
    const getColorArrayDOM = () => {
        return <div className='form__column-container' style={{ padding: `${globalSizeFactor * 1}vh` }}>
            <div className={`form__flex-and-row-between ${props.defaultTexture && 'form__disabled'}`}>
                <div style={{ width: '48%' }} className='form__column-container'>
                    <div className='form__flex-and-row-between'>
                        <label>Src Width:</label>
                        <InputNumber name="width" value={props.tabInfo.properties.memoryBufferProperties.width ?? 0} onValueChange={saveData} />
                    </div>
                    <div className='form__flex-and-row-between'>
                        <label>Src Height:</label>
                        <InputNumber name="height" value={props.tabInfo.properties.memoryBufferProperties.height ?? 0} onValueChange={saveData} />
                    </div>
                </div>
                <div style={{ width: '48%' }} className='form__column-container'>
                    <div className='form__flex-and-row-between'>
                        <label>Texture Usage:</label>
                        <Dropdown className='texture__dropdown' name="textureUseage" value={props.tabInfo.properties.memoryBufferProperties.textureUseage ?? { name: '' }} onChange={saveData} options={enumDetails.usage} optionLabel="name" />
                    </div>
                    <div className='form__flex-and-row-between'>
                        <label>Pixel Format:</label>
                        <Dropdown className='texture__dropdown' name="colorArrPixelFormat" value={props.tabInfo.properties.memoryBufferProperties.colorArrPixelFormat ?? { name: '' }} onChange={saveData} options={enumDetails.pixelFormats} optionLabel="name" />
                    </div>
                </div>
            </div>
            <div style={{ paddingBottom: '3%' }} className='form__flex-and-row-between'>
                <div style={{ width: '48%' }} className='form__column-container'>
                    <label style={{ textDecoration: 'underline' }}> Colors:</label>
                    <DataTable tableStyle={{ minWidth: '99%' }} showGridlines scrollable scrollHeight={`${globalSizeFactor * 10}vh`} size='small' value={props.tabInfo.properties.memoryBufferProperties.colorArrayColors?.map((color, i) => ({ color: color, index: i, actions: null }))} editMode="cell">
                        {colorsTableColumns.map(({ field, header }) => {
                            return <Column style={{ minWidth: `${globalSizeFactor * (field == 'index' ? 1 : field == 'actions' ? 3 : 20)}vh` }} key={header} field={field} header={field == 'actions' ? getPlusIcon('colorArrayColors') : header} body={getColumnTemplate} />
                        })}
                    </DataTable>
                </div>
                <div style={{ width: '48%' }} className='form__column-container'>
                    <label style={{ textDecoration: 'underline' }}> Color Positions(Optional):</label>
                    <DataTable tableStyle={{ minWidth: '99%' }} showGridlines scrollable scrollHeight={`${globalSizeFactor * 10}vh`} size='small' value={getColorPositionsArray()} editMode="cell">
                        {colorPositionsTableColumns.map(({ field, header }) => {
                            return <Column style={{ minWidth: `${globalSizeFactor * (field == 'index' ? 1 : field == 'actions' ? 3 : 20)}vh` }} key={header} field={field} header={field == 'actions' ? getPlusIcon('colorPositions') : header} body={getColumnTemplate} />
                        })}
                    </DataTable>
                </div>
            </div>

            <div className='form__flex-and-row-between form__items-center'>
                <SelectButton value={props.tabInfo.properties.memoryBufferProperties.colorColumns ? 'Color Columns' : 'Color Rows'} onChange={handleColorDirectionChange} options={['Color Columns', 'Color Rows']} />
                <div className={`form__flex-and-row form__items-center ${props.defaultTexture && 'form__disabled'}`}>
                    <Checkbox inputId="autoMipMap" name="autoMipmap" onChange={saveData} checked={props.tabInfo.properties.memoryBufferProperties.autoMipmap ?? false} />
                    <label htmlFor="autoMipMap" className="texture__checkbox-div">Auto Mipmap</label>
                </div>
                <div className='form__flex-and-row form__items-center'>
                    <Checkbox inputId="isColorInterpolation" name="isColorInterpolation" onChange={saveData} checked={props.tabInfo.properties.memoryBufferProperties.isColorInterpolation ?? false} />
                    <label htmlFor="isColorInterpolation" className="texture__checkbox-div">Color Interpolation</label>
                </div>
            </div>
        </div>
    }
    //#endregion
    return (
        <div className='form__column-container'>
            <div style={{ width: '30%' }} className='form__flex-and-row-between'>
                <div className="form__flex-and-row form__items-center">
                    <RadioButton inputId="BUFFER" name="selectedOption" value={MemoryBufferTextureOptionsEnum.BUFFER} onChange={saveData} checked={props.tabInfo.properties.memoryBufferProperties.selectedOption === MemoryBufferTextureOptionsEnum.BUFFER} />
                    <label htmlFor="BUFFER">Buffer</label>
                </div>
                <div className="form__flex-and-row form__items-center">
                    <RadioButton inputId="COLOR_ARRAY" name="selectedOption" value={MemoryBufferTextureOptionsEnum.COLOR_ARRAY} onChange={saveData} checked={props.tabInfo.properties.memoryBufferProperties.selectedOption === MemoryBufferTextureOptionsEnum.COLOR_ARRAY} />
                    <label htmlFor="COLOR_ARRAY">Color Array</label>
                </div>
            </div>

            {props.tabInfo.properties.memoryBufferProperties.selectedOption === MemoryBufferTextureOptionsEnum.BUFFER ? getBufferOptionDOM() : getColorArrayDOM()}
        </div>
    )
}