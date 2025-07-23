import React, { useEffect } from 'react';
import { Checkbox } from 'primereact/checkbox';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { MapCoreData, enums } from 'mapcore-lib';
import { useDispatch, useSelector } from 'react-redux';
import generalService from '../../../../../../services/general.service';
import { PickList } from 'primereact/picklist';
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import TexturePropertiesBase from '../../../../../../propertiesBase/texturePropertiesBase';
import { Properties } from "../../../../dialog";
import TextureDialog, { TextureDialogActionMode } from '../textureDialog';
import TextureService, { TextureTypeEnum } from '../../../../../../services/texture.service';
import { AppState } from '../../../../../../redux/combineReducer';

export class TextureArrayProperties implements Properties {
    selectItem: any;
    source: any;
    target: any;
    counter: number;
    dialogOpen: boolean;
    textureArray: any;
    textureFooter: any;

    static getDefault(props: any): TextureArrayProperties {
        let { tabInfo } = props;

        return {
            selectItem: tabInfo.properties.textureArrayProperties.selectItem || null,
            source: tabInfo.properties.textureArrayProperties.source || [],
            target: tabInfo.properties.textureArrayProperties.target || [],
            counter: tabInfo.properties.textureArrayProperties.counter || 0,
            dialogOpen: false,
            textureArray: tabInfo.properties.textureArrayProperties.textureArray || null,
            textureFooter: <div></div>,
        }
    }
}

export default function TextureArray(props: {
    defaultTexture: MapCore.IMcTextureArray,
    tabInfo: {
        properties: TexturePropertiesBase,
        setPropertiesCallback: (tab: string, key: string, value: any) => void,
    }
}) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    useEffect(() => {
        let defaultProperties = TextureArrayProperties.getDefault(props)
        props.tabInfo.setPropertiesCallback("textureArrayProperties", null, defaultProperties);
        TextureService.textureType = TextureTypeEnum.TextureArray;
        let list = MapCoreData.textureArr.map((MCTexture: MapCore.IMcTexture, i) => { return { MCTexture, name: MCTexture.constructor.name, index: i } })
        props.tabInfo.setPropertiesCallback("textureArrayProperties", 'source', list)
    }, []);
    // לפעמים לא נוצר texture
    useEffect(() => {
        let updateTextureArray = props.tabInfo.properties.textureArrayProperties.target?.map((t: any) => { return t.MCTexture });
        props.tabInfo.setPropertiesCallback("textureArrayProperties", 'textureArray', updateTextureArray);
    }, [props.tabInfo.properties.textureArrayProperties.target]);

    const onChange = (event: any) => {
        let c = props.tabInfo.properties.textureArrayProperties.counter;
        let newTarget = event.target.map((t: any, i: any) => {
            return { ...t, index: c++ }
        });
        props.tabInfo.setPropertiesCallback("textureArrayProperties", 'target', newTarget)
        props.tabInfo.setPropertiesCallback("textureArrayProperties", 'counter', c + 1)
    };
    const duplicateTexture = (item: any) => {
        let newTarget = [...props.tabInfo.properties.textureArrayProperties.target, { ...props.tabInfo.properties.textureArrayProperties.selectItem[0], index: props.tabInfo.properties.textureArrayProperties.counter }];
        props.tabInfo.setPropertiesCallback("textureArrayProperties", 'target', newTarget)
        props.tabInfo.setPropertiesCallback("textureArrayProperties", 'counter', props.tabInfo.properties.textureArrayProperties.counter + 1)
    };

    const removeTexture = (item: any) => {
        let newTarget = props.tabInfo.properties.textureArrayProperties.target.filter((t: any) => t != props.tabInfo.properties.textureArrayProperties.selectItem[0]);
        props.tabInfo.setPropertiesCallback("textureArrayProperties", 'target', newTarget)
    };
    const addTexture = (item: any) => {
        props.tabInfo.setPropertiesCallback("textureArrayProperties", 'dialogOpen', true)
    };

    const itemTemplate = (item: any) => {
        return (
            <div className="flex flex-wrap p-2 align-items-center gap-3">
                <div className="flex-1 flex flex-column gap-2">
                    <span className="font-bold">{item?.name}</span>
                </div>
            </div>
        );
    };

    const handleTextureClose = (texture: MapCore.IMcTexture) => {
        let newTarget = [...props.tabInfo.properties.textureArrayProperties.target, { MCTexture: texture, name: texture.constructor.name, index: props.tabInfo.properties.textureArrayProperties.counter }];
        let newSource = [...props.tabInfo.properties.textureArrayProperties.source, { MCTexture: texture, name: texture.constructor.name, index: props.tabInfo.properties.textureArrayProperties.counter }];
        props.tabInfo.setPropertiesCallback("textureArrayProperties", 'source', newSource)
        props.tabInfo.setPropertiesCallback("textureArrayProperties", 'target', newTarget)
        props.tabInfo.setPropertiesCallback("textureArrayProperties", 'counter', props.tabInfo.properties.textureArrayProperties.counter + 1)
        props.tabInfo.setPropertiesCallback("textureArrayProperties", 'dialogOpen', false)
    }

    return (
        <div style={{ height: '100%' }}>
            <div className="card">
                <PickList dataKey="index" source={props.tabInfo.properties.textureArrayProperties.source ?? []} target={props.tabInfo.properties.textureArrayProperties.target ?? []} onChange={onChange} itemTemplate={itemTemplate}
                    onTargetSelectionChange={(e) => props.tabInfo.setPropertiesCallback("textureArrayProperties", 'selectItem', e.value)}
                    sourceHeader="Available" targetHeader="Selected" sourceStyle={{ height: `${globalSizeFactor * 25}vh` }} targetStyle={{ height: `${globalSizeFactor * 25}vh` }} />
            </div>
            <div className="form__row-container form__justify-end" style={{ marginRight: '6%' }}>
                <Button onClick={duplicateTexture} >Duplicate Texture</Button>
                <Button onClick={removeTexture}>Remove From List</Button>
                <Button onClick={addTexture}>Add</Button>
            </div>

            <Dialog className='scroll-dialog-3' footer={props.tabInfo.properties.textureArrayProperties.textureFooter ?? <div></div>} onHide={() => { props.tabInfo.setPropertiesCallback("textureArrayProperties", 'dialogOpen', false) }} visible={props.tabInfo.properties.textureArrayProperties.dialogOpen ?? false}>
                <TextureDialog footerHook={footer => props.tabInfo.setPropertiesCallback("textureArrayProperties", 'textureFooter', footer)} texturePropertiesBase={generalService.TextureProperties} textureClose={handleTextureClose} defaultTexture={null} isSetAsDefault={false} actionMode={TextureDialogActionMode.create} saveTexturePropertiesCB={(textureProp: TexturePropertiesBase) => { generalService.TextureProperties = textureProp }} />
            </Dialog>
        </div >
    )
}
