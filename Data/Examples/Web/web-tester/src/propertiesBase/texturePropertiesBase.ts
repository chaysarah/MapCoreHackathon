import { FromListProperties } from "../components/dialog/mapToolbarActions/symbolicItemsDialogs/texture/SubTexture/fromList";
import { ImageFileProperties } from "../components/dialog/mapToolbarActions/symbolicItemsDialogs/texture/SubTexture/imageFile";
import { MemoryBufferProperties } from "../components/dialog/mapToolbarActions/symbolicItemsDialogs/texture/SubTexture/memoryBuffer";
import { TextureArrayProperties } from "../components/dialog/mapToolbarActions/symbolicItemsDialogs/texture/SubTexture/textureArray";
import { VideoProperties } from "../components/dialog/mapToolbarActions/symbolicItemsDialogs/texture/SubTexture/video";
import { TextureFooterProperties } from "../components/dialog/mapToolbarActions/symbolicItemsDialogs/texture/textureFooter";

export default class TexturePropertiesBase {
    imageFileProperties: ImageFileProperties = new ImageFileProperties();
    memoryBufferProperties: MemoryBufferProperties = new MemoryBufferProperties();
    videoProperties: VideoProperties = new VideoProperties();
    fromListProperties: FromListProperties = new FromListProperties();
    textureArrayProperties: TextureArrayProperties = new TextureArrayProperties();
    textureFooterProperties: TextureFooterProperties = new TextureFooterProperties();


    constructor();
    constructor(props: TexturePropertiesBase);

    constructor(props?: TexturePropertiesBase) {
        if (props) {
            this.imageFileProperties = props.imageFileProperties;
            this.memoryBufferProperties = props.memoryBufferProperties;
            this.videoProperties = props.videoProperties;
            this.fromListProperties = props.fromListProperties;
            this.textureArrayProperties = props.textureArrayProperties;
            this.textureFooterProperties = props.textureFooterProperties;
        }
    }


}