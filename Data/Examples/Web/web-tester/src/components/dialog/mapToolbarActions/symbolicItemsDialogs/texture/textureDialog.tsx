import { TabView, TabPanel } from 'primereact/tabview';
import { useSelector } from 'react-redux';
import { ReactElement, useEffect, useMemo, useState } from 'react';
import _ from 'lodash';

import './styles/texture.css';
import HBitmap from './SubTexture/hBitmap';
import ImageFile from './SubTexture/imageFile';
import HIcon from './SubTexture/hIcon';
import MemoryBuffer from './SubTexture/memoryBuffer';
import Video from './SubTexture/video';
import FromList from './SubTexture/fromList';
import TextureArray from './SubTexture/textureArray';
import TextureFooter from './textureFooter';
import { AppState } from '../../../../../redux/combineReducer';
import TexturePropertiesBase from '../../../../../propertiesBase/texturePropertiesBase';
import generalService from '../../../../../services/general.service';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';

export enum TextureDialogActionMode {
    create = 1,
    reCreate,
    update,
}

export default function TextureDialog(props: {
    footerHook: (footer: () => ReactElement) => void
    textureClose: (texture: MapCore.IMcTexture) => void,
    defaultTexture: MapCore.IMcTexture,
    isSetAsDefault: boolean,
    actionMode: TextureDialogActionMode,
    texturePropertiesBase: TexturePropertiesBase,
    saveTexturePropertiesCB: (textureProp: TexturePropertiesBase) => void
}) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const textureTabsInfo = useMemo(() => [
        { type: MapCore.IMcBitmapHandleTexture.TEXTURE_TYPE, header: 'HBitmap', component: HBitmap, tabIndex: 0 },
        { type: MapCore.IMcImageFileTexture.TEXTURE_TYPE, header: 'Image File', component: ImageFile, tabIndex: 1 },
        { type: MapCore.IMcIconHandleTexture.TEXTURE_TYPE, header: 'HIcon', component: HIcon, tabIndex: 2 },
        { type: MapCore.IMcMemoryBufferTexture.TEXTURE_TYPE, header: 'Memory Buffer', component: MemoryBuffer, tabIndex: 3 },
        { type: MapCore.IMcHtmlVideoTexture.TEXTURE_TYPE, header: 'Video', component: Video, tabIndex: 4 },
        { type: 'FromList', header: 'From List', component: FromList, tabIndex: 5 },
        { type: MapCore.IMcTextureArray.TEXTURE_TYPE, header: 'Texture Array', component: TextureArray, tabIndex: 6 }
    ], [])

    const getActivetab = () => {
        let activeTabIndex = 1;
        runCodeSafely(() => {
            if (props.defaultTexture) {
                const textureType = props.defaultTexture.GetTextureType();
                activeTabIndex = textureTabsInfo.find(textureInfo => textureInfo.type == textureType).tabIndex;
            }
        }, 'TextureDialog.getActivetab')
        return activeTabIndex;
    }

    const [activeTabIndex, setActivetabIndex] = useState(getActivetab());
    const [localProperties, setLocalProperties] = useState<TexturePropertiesBase>(props.defaultTexture ?
        new TexturePropertiesBase(props.texturePropertiesBase || generalService.TextureProperties) : new TexturePropertiesBase())

    useEffect(() => {
        runCodeSafely(() => {
            const root = document.documentElement;
            let pixelWidth = window.innerHeight * 0.75 * globalSizeFactor;
            root.style.setProperty('--texture-dialog-width', `${pixelWidth}px`);
        }, 'TextureDialog.useEffect[mounting]')
    }, []);
    useEffect(() => {
        runCodeSafely(() => {
            props.footerHook(getFooter);
        }, 'TextureDialog.useEffect[localProperties]')
    }, [localProperties, activeTabIndex])

    const setLocalPropertiesCallback = (tab: string, key: string, value: any) => {
        runCodeSafely(() => {
            if (key === null && !_.isEqual(localProperties[tab], value)) {
                setLocalProperties(properties => ({ ...properties, [tab]: value }))
            }
            else if (localProperties[tab] && !_.isEqual(localProperties[tab][key], value)) {
                let updatedProperties = { ...localProperties }
                updatedProperties[tab][key] = value;
                setLocalProperties(updatedProperties);
            }
        }, 'TextureDialog.setLocalPropertiesCallback')
    }
    const tabInfo = useMemo(() => ({
        properties: localProperties,
        setPropertiesCallback: setLocalPropertiesCallback,
    }), [localProperties])

    const getFooter = () => {
        return <TextureFooter defaultTexture={props.defaultTexture}
            textureClose={(texture: MapCore.IMcTexture) => {
                props.textureClose(texture)
            }}
            isSetAsDefault={props.isSetAsDefault}
            tabInfo={tabInfo}
            activeTab={activeTabIndex}
            textureTabsInfo={textureTabsInfo}
            actionMode={props.actionMode}
            saveTexturePropertiesCB={(textureProp: TexturePropertiesBase) => { props.saveTexturePropertiesCB(textureProp) }}
        />
    }

    return (
        <div style={{ height: `${globalSizeFactor * 30}vh` }}>
            <TabView scrollable style={{ height: '100%', borderBottom: '0.15vh solid #dee2e6', marginBottom: '1%' }} activeIndex={activeTabIndex} onTabChange={e => {
                setActivetabIndex(e.index)
            }}>
                {textureTabsInfo.map((textureTabInfo, key) => {
                    const Item = textureTabInfo.component as any;
                    return <TabPanel header={textureTabInfo.header} key={key} style={{ height: '100%' }} disabled={props.actionMode == TextureDialogActionMode.update && key !== activeTabIndex} >
                        <Item defaultTexture={props.defaultTexture} tabInfo={tabInfo} />
                    </TabPanel>
                })}
            </TabView>
        </div>
    );
}
