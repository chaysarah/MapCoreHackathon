import { useEffect } from 'react';
import { useSelector } from 'react-redux';
import { ListBox } from 'primereact/listbox';
import { Button } from 'primereact/button';

import { MapCoreData } from 'mapcore-lib';
import { Properties } from "../../../../dialog";
import TexturePropertiesBase from '../../../../../../propertiesBase/texturePropertiesBase';
import { AppState } from '../../../../../../redux/combineReducer';
import TextureService, { TextureTypeEnum } from '../../../../../../services/texture.service';

export class FromListProperties implements Properties {
    textureList: { MCTexture: MapCore.IMcTexture, name: string, index: number }[];
    textureListToDownload: MapCore.IMcTexture[];
    selectTextureList: any;
    textureFromList: MapCore.IMcTexture;

    static getDefault(props: any): FromListProperties {
        let { tabInfo } = props;

        return {
            textureList: tabInfo.properties.fromListProperties.textureList || [],
            textureListToDownload: tabInfo.properties.fromListProperties.textureListToDownload || [],
            selectTextureList: tabInfo.properties.fromListProperties.selectTextureList || null,
            textureFromList: tabInfo.properties.fromListProperties.textureFromList || null,
        }
    }
}

export default function FromList(props: {
    defaultTexture: MapCore.IMcTexture,
    tabInfo: {
        properties: TexturePropertiesBase,
        setPropertiesCallback: (tab: string, key: string, value: any) => void,
    }
}) {
    // const [fromListData, setFromListData] = useState({
    //     textureList: [],
    //     textureListToDownload: [],
    //     selectTextureList: null,
    // })
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    useEffect(() => {
        let defaultProperties = FromListProperties.getDefault(props)
        props.tabInfo.setPropertiesCallback("fromListProperties", null, defaultProperties);
        let list = MapCoreData.textureArr.map((MCTexture: MapCore.IMcTexture, i) => { return { MCTexture, name: MCTexture.constructor.name, index: i } })
        props.tabInfo.setPropertiesCallback("fromListProperties", 'textureList', list)
        TextureService.textureType = TextureTypeEnum.FromList;
    }, [])
    const saveCheckTexture = (event: any) => {
        props.tabInfo.setPropertiesCallback("fromListProperties", 'selectTextureList', event.target.value)
        props.tabInfo.setPropertiesCallback("fromListProperties", 'textureFromList', event.target.value?.MCTexture)
    }
    const deleteTexture = (event: any) => {
        MapCoreData.textureArr = MapCoreData.textureArr.filter(t => t != props.tabInfo.properties.fromListProperties.selectTextureList?.MCTexture)
        let list = MapCoreData.textureArr.map((MCTexture: MapCore.IMcTexture, i) => { return { MCTexture, name: MCTexture.constructor.name, index: i } })
        props.tabInfo.setPropertiesCallback("fromListProperties", 'textureList', list)
    }
    const Save = (event: any) => {
        //  ToFix
        eval("MapCore.IMcHtmlVideoTexture.TEXTURE_TYPE = MapCore.__IMcHtmlVideoTexture_TEXTURE_TYPE")
        let imageTexture: MapCore.IMcImageFileTexture = props.tabInfo.properties.fromListProperties.selectTextureList?.MCTexture;
        let LT: MapCore.IMcTexture[] = []
        props.tabInfo.properties.fromListProperties.textureList.forEach(texture => {
            if (texture.MCTexture.GetTextureType() == MapCore.IMcMemoryBufferTexture.TEXTURE_TYPE)
                LT.push(texture.MCTexture)
            if (texture.MCTexture.GetTextureType() == MapCore.IMcImageFileTexture.TEXTURE_TYPE) {
                let fileSource: MapCore.SMcFileSource = (texture.MCTexture as any as MapCore.IMcImageFileTexture).GetImageFile();
                if (fileSource.bIsMemoryBuffer && fileSource.aFileMemoryBuffer != null)
                    LT.push(texture.MCTexture)
            }
        });
        props.tabInfo.setPropertiesCallback("fromListProperties", 'textureListToDownload', LT)
        props.tabInfo.properties.fromListProperties.textureListToDownload.forEach((texture: MapCore.IMcTexture) => {
            if (texture.GetTextureType() == MapCore.IMcMemoryBufferTexture.TEXTURE_TYPE) {
                let buffer;
                let srcWidth: { Value: number } = { Value: 0 }, srcHeight: { Value: number } = { Value: 0 };

                texture.GetSourceSize(srcWidth, srcHeight);
                let pixelFormat: MapCore.IMcTexture.EPixelFormat = (texture as any as MapCore.IMcMemoryBufferTexture).GetPixelFormat();
                buffer = (texture as any as MapCore.IMcMemoryBufferTexture).GetToMemoryBuffer(srcWidth.Value, srcHeight.Value, pixelFormat, 0,);
                let fileName = texture.GetName()
                MapCore.IMcMapDevice.DownloadBufferAsFile(buffer, fileName + ".bmp")
            }
            if (texture.GetTextureType() == MapCore.IMcImageFileTexture.TEXTURE_TYPE) {
                let fileSource: MapCore.SMcFileSource = (texture as any as MapCore.IMcImageFileTexture).GetImageFile();
                let fileName = fileSource.strFileName == "" ? texture.GetName() : fileSource.strFileName
                if (fileSource.bIsMemoryBuffer)
                    MapCore.IMcMapDevice.DownloadBufferAsFile(fileSource.aFileMemoryBuffer, fileName + "." + fileSource.strFormatExtension)
            }
        });
    }

    return (
        <div style={{ height: '100%' }}>
            <ListBox listStyle={{ minHeight: `${globalSizeFactor * 15}vh`, maxHeight: `${globalSizeFactor * 15}vh` }} options={props.tabInfo.properties.fromListProperties.textureList ?? [{ name: '' }]} optionLabel="name" value={props.tabInfo.properties.fromListProperties.selectTextureList ?? { name: '' }} multiple={false} onChange={saveCheckTexture} > </ListBox>
            <div className='texture__flex-and-row-end-pad'>
                <Button onClick={deleteTexture}>Delete</Button>
                <Button onClick={Save}>Save All Embedded Textures To Folder</Button>
            </div>
        </div >
    )
}