import { useEffect, useMemo, useState } from 'react';
import { Checkbox } from 'primereact/checkbox';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { useSelector } from 'react-redux';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';

import { MapCoreData } from 'mapcore-lib';
import { MemoryBufferTextureOptionsEnum } from './SubTexture/memoryBuffer';
import { TextureDialogActionMode } from './textureDialog';
import { Properties } from "../../../dialog";
import ColorPickerCtrl from '../../../../shared/colorPicker';
import { runAsyncCodeSafely, runCodeSafely, runMapCoreSafely } from '../../../../../common/services/error-handling/errorHandler';
import TexturePropertiesBase from '../../../../../propertiesBase/texturePropertiesBase';
import TextureService, { TextureTypeEnum } from '../../../../../services/texture.service';
import { AppState } from '../../../../../redux/combineReducer';

export class TextureFooterProperties implements Properties {
    UseExisting: boolean;
    FillPattern: boolean;
    IgnoreTransparentMargin: boolean;
    TransparentColor: MapCore.SMcBColor;
    colorSubstitutionsArr: MapCore.IMcTexture.SColorSubstitution[];
    uniqueName: string;
    isDefault: boolean;

    static getDefault(props: any): TextureFooterProperties {
        let { tabInfo } = props;

        return {
            UseExisting: tabInfo.properties.textureFooterProperties.UseExisting || true,
            FillPattern: tabInfo.properties.textureFooterProperties.FillPattern || false,
            IgnoreTransparentMargin: tabInfo.properties.textureFooterProperties.IgnoreTransparentMargin || false,
            TransparentColor: tabInfo.properties.textureFooterProperties.TransparentColor || new MapCore.SMcBColor(255, 255, 255, 255),
            colorSubstitutionsArr: tabInfo.properties.textureFooterProperties.colorSubstitutionsArr || [],
            uniqueName: tabInfo.properties.textureFooterProperties.uniqueName || "",
            isDefault: tabInfo.properties.textureFooterProperties.isDefault || false,
        }
    }
}

export default function TextureFooter(props: {
    defaultTexture: MapCore.IMcTexture,
    textureClose: (texture: MapCore.IMcTexture) => void,
    isSetAsDefault: boolean,
    tabInfo: {
        properties: TexturePropertiesBase,
        setPropertiesCallback: (tab: string, key: string, value: any) => void,
    },
    activeTab: number,
    textureTabsInfo: any[],
    actionMode: TextureDialogActionMode,
    saveTexturePropertiesCB: (textureProp: TexturePropertiesBase) => void,
}) {
    const { textureFooterProperties } = props.tabInfo.properties;
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [footerDataVisible, setFooterDataVisible] = useState(false);

    const tableColumns = useMemo(() => [
        { field: 'index', header: '.No' },
        { field: 'ColorToSubstitute', header: 'Color To Sub' },
        { field: 'SubstituteColor', header: 'Sub Color' },
        { field: 'actions', header: '' },
    ], [])

    useEffect(() => {
        runCodeSafely(() => {
            let defaultProperties = TextureFooterProperties.getDefault(props)
            let finalDefaults = defaultProperties;
            if (props.defaultTexture) {
                let transparentColor: any = {}
                props.defaultTexture.GetTransparentColor(transparentColor)
                const mcColorSubstitutionsArr = props.defaultTexture.GetColorSubstitutions();
                let uniqueName = null;
                runMapCoreSafely(() => { uniqueName = props.defaultTexture.GetName() }, 'textureFooter.useEffect => IMcTexture.GetName', true)
                finalDefaults = {
                    ...defaultProperties,
                    uniqueName: uniqueName,
                    TransparentColor: transparentColor.Value,
                    colorSubstitutionsArr: mcColorSubstitutionsArr || [],
                    UseExisting: props.defaultTexture.IsCreatedWithUseExisting(),
                    FillPattern: props.defaultTexture.IsFillPattern(),
                    IgnoreTransparentMargin: props.defaultTexture.IsTransparentMarginIgnored(),
                }
            }
            props.tabInfo.setPropertiesCallback("textureFooterProperties", null, finalDefaults);
        }, "textureFooter.useEffect");
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            const useExistingTabs = props.textureTabsInfo.filter((tabInfo) => [MapCore.IMcBitmapHandleTexture.TEXTURE_TYPE, MapCore.IMcIconHandleTexture.TEXTURE_TYPE, MapCore.IMcImageFileTexture.TEXTURE_TYPE].includes(tabInfo.type))
            const isUseExistingVisible = useExistingTabs.map(checkboxInvisTab => checkboxInvisTab.tabIndex).includes(props.activeTab)
            setFooterDataVisible(isUseExistingVisible);
        }, "textureFooter.useEffect");
    }, [props.activeTab])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("textureFooterProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
        }, "textureFooterData.saveData => onChange");
    }

    const showErrorMessages = () => {
        switch (TextureService.textureType) {
            case TextureTypeEnum.ImageFile:
                if ((props.tabInfo.properties.imageFileProperties.fileUrl == null || props.tabInfo.properties.imageFileProperties.fileUrl == "") &&
                    (props.tabInfo.properties.imageFileProperties.inputFile == null || props.tabInfo.properties.imageFileProperties.inputFile == "")) {
                    return "Missing required parameters";
                }
                break;
            case TextureTypeEnum.Video:
                if (props.tabInfo.properties.videoProperties.NameSource == null || props.tabInfo.properties.videoProperties.NameSource == "") {
                    return "Missing required parameters";

                }
                break;
            case TextureTypeEnum.MemoryBuffer:
                if (props.tabInfo.properties.memoryBufferProperties.selectedOption == MemoryBufferTextureOptionsEnum.BUFFER && props.tabInfo.properties.memoryBufferProperties.rowPitch > 0 &&
                    props.tabInfo.properties.memoryBufferProperties.rowPitch < props.tabInfo.properties.memoryBufferProperties.width) {
                    return "Invalid row pitch, should be greater than width";
                }
                break;
            default:
                return null;
        }
    }
    //#region Handle Functions
    const handleColorChange = (event: { value: any; target: any }) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("textureFooterProperties", event.target.name, { ...event.value, a: 0 });
        }, "textureFooter.handleColorChange => onChange");
    }
    const handleCellChange = (eValue: any, rowData: any, column: any) => {
        runCodeSafely(() => {
            let copiedArr = [...textureFooterProperties.colorSubstitutionsArr];
            let newCurrObj = { ...copiedArr[rowData.index], [column.field]: eValue }
            copiedArr[rowData.index] = newCurrObj;
            props.tabInfo.setPropertiesCallback("textureFooterProperties", "colorSubstitutionsArr", copiedArr);
        }, "textureFooter.handleCellChange => onChange");
    }
    const handleDeleteRowClick = (index: number) => {
        runCodeSafely(() => {
            let copiedArr = [...textureFooterProperties.colorSubstitutionsArr];
            copiedArr.splice(index, 1);
            props.tabInfo.setPropertiesCallback("textureFooterProperties", "colorSubstitutionsArr", copiedArr);
        }, "textureFooter.handleDeleteRowClick => onClick");
    }
    const handlePlusRowClick = () => {
        runCodeSafely(() => {
            const newItem = { ColorToSubstitute: new MapCore.SMcBColor, SubstituteColor: new MapCore.SMcBColor };
            const copiedArr = [...textureFooterProperties.colorSubstitutionsArr, newItem];
            props.tabInfo.setPropertiesCallback("textureFooterProperties", "colorSubstitutionsArr", copiedArr);
        }, "textureFooter.handlePlusRowClick => onClick");
    }
    const handleCreateTextureClick = async () => {
        runAsyncCodeSafely(async () => {
            props.saveTexturePropertiesCB(props.tabInfo.properties);
            let texture;
            let errorMessage = showErrorMessages();
            if (errorMessage) {
                alert(errorMessage);
            }
            else {
                if (TextureService.textureType == TextureTypeEnum.FromList) {
                    texture = props.tabInfo.properties.fromListProperties.textureFromList;
                }
                else {
                    texture = await TextureService.createTexture(props.tabInfo.properties)
                    texture && MapCoreData.textureArr.push(texture)//tthe condition exists for error in texture creation case
                }
                props.textureClose(texture);
            }
        }, 'textureFooter.handleCreateTextureClick')
    }
    const handleUpdateTextureClick = () => {
        runAsyncCodeSafely(async () => {
            props.saveTexturePropertiesCB(props.tabInfo.properties);
            let errorMessage = showErrorMessages();
            if (errorMessage) {
                alert(errorMessage);
            }
            else {
                let texture = await TextureService.updateTexture(props.tabInfo.properties, props.defaultTexture)
                props.textureClose(texture);
            }
        }, 'textureFooter.handleUpdateTextureClick')
    }
    //#endregion
    //#region  DOM Functions
    const getGenerateButton = () => {
        let textureType = props.defaultTexture?.GetTextureType();
        if (textureType !== MapCore.IMcTextureArray.TEXTURE_TYPE) {
            switch (props.actionMode) {
                case TextureDialogActionMode.create:
                    return <Button onClick={handleCreateTextureClick} label="Create" />;
                case TextureDialogActionMode.update:
                    return <Button onClick={handleUpdateTextureClick} label="Update" />;
                case TextureDialogActionMode.reCreate:
                    return <Button onClick={handleCreateTextureClick} label="Re-Create" />;
                default:
                    break;
            }
        }
        return <div />
    }
    const getColumnTemplate = (rowData: any, column: any) => {
        switch (column.field) {
            case 'index':
                return <span style={{ minWidth: `${globalSizeFactor * 1}vh` }} >{rowData[column.field] + 1}</span>
            case 'actions':
                return <Button style={{ minWidth: `${globalSizeFactor * 3}vh` }} icon='pi pi-trash' onClick={e => handleDeleteRowClick(rowData.index)} />
            case 'ColorToSubstitute':
            case 'SubstituteColor':
                return <ColorPickerCtrl name={column.field} value={rowData[column.field]} alpha onChange={(e) => { handleCellChange(e.value, rowData, column) }} />
        }
    }
    const getPlusIcon = () => {
        return <Button icon='pi pi-plus-circle' onClick={handlePlusRowClick} />
    }
    const getColorsArea = () => {
        return <div className={!footerDataVisible && 'form__non-visible'} style={{ width: '55%' }}>
            <div className='form__flex-and-row-between' style={{ width: '100%' }}>
                <label>Transparent Color:</label>
                <ColorPickerCtrl name='TransparentColor' value={textureFooterProperties.TransparentColor ?? { r: 0, g: 0, b: 0 }} onChange={handleColorChange} />
            </div>
            <DataTable tableStyle={{ minWidth: '99%' }} showGridlines scrollable scrollHeight={`${globalSizeFactor * 10}vh`} size='small' value={textureFooterProperties.colorSubstitutionsArr?.map((obj, i) => ({ ...obj, index: i, actions: null }))} editMode="cell">
                {tableColumns.map(({ field, header }) => {
                    return <Column style={{ minWidth: `${globalSizeFactor * (field == 'index' ? 1 : field == 'actions' ? 3 : 14)}vh` }} key={header} field={field} header={field == 'actions' ? getPlusIcon() : header} body={getColumnTemplate} />
                })}
            </DataTable>
        </div>
    }
    const getCheckboxesArea = () => {
        return <div className={`form__column-container ${!footerDataVisible && props.actionMode !== TextureDialogActionMode.update && 'form__non-visible'} ${props.actionMode == TextureDialogActionMode.update && 'form__disabled'}`}>
            <div className={`form__flex-and-row form__items-center`}>
                <Checkbox name="UseExisting" onChange={saveData} checked={textureFooterProperties.UseExisting ?? false} />
                <label style={{ whiteSpace: 'nowrap' }}>{props.actionMode == TextureDialogActionMode.update ? 'Is Created With Use Existing' : 'Use Existing'}</label>
            </div>
            <div className={`form__flex-and-row form__items-center`}>
                <Checkbox name="FillPattern" onChange={saveData} checked={textureFooterProperties.FillPattern ?? false} />
                <label style={{ whiteSpace: 'nowrap' }}>Fill Pattern</label>
            </div>
            <div className={`form__flex-and-row form__items-center`}>
                <Checkbox name="IgnoreTransparentMargin" onChange={saveData} checked={textureFooterProperties.IgnoreTransparentMargin ?? false} />
                <label style={{ whiteSpace: 'nowrap' }}>Ignore Transparent Margin</label>
            </div>
        </div>
    }
    //#endregion

    return (
        <div className='texture__bottom-popup' style={{ padding: `${globalSizeFactor * 1}vh` }}>
            <div style={{ width: '71%' }} className={`form__flex-and-row-between ${props.activeTab != 3 && props.actionMode !== TextureDialogActionMode.update && 'form__non-visible'}`}>
                <label className={props.actionMode == TextureDialogActionMode.update && 'texture__disabled-label'}>Unique Name:</label>
                <InputText disabled={props.actionMode == TextureDialogActionMode.update} style={{ width: '70%' }} name='uniqueName' value={textureFooterProperties.uniqueName ?? ''} onChange={saveData} />
            </div>
            <div className='form__flex-and-row-between' style={{ width: '100%' }}>
                {footerDataVisible || props.actionMode == TextureDialogActionMode.update ? getColorsArea() : <div style={{ width: '55%' }}></div>}
                <div className='form__column-container form__justify-between' style={{ width: '40%' }}>
                    {getCheckboxesArea()}
                    <div className='form__flex-and-row form__items-center'>
                        {getGenerateButton()}
                        <Button onClick={() => { props.textureClose(props.defaultTexture) }} label="Cancel" />
                        {props.isSetAsDefault &&
                            <div className='form__flex-and-row form__items-center'>
                                <Checkbox onChange={() => props.tabInfo.setPropertiesCallback("textureFooterProperties", 'isDefault', !textureFooterProperties.isDefault)}
                                    checked={textureFooterProperties.isDefault ?? false} />
                                <label style={{ whiteSpace: 'nowrap' }}>Set As Default</label>
                            </div>
                        }
                    </div>
                </div>
            </div>
        </div >
    )
}