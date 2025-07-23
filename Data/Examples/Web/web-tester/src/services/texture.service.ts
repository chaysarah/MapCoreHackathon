import { runCodeSafely, runMapCoreSafely } from "../common/services/error-handling/errorHandler";
import { MemoryBufferTextureOptionsEnum } from "../components/dialog/mapToolbarActions/symbolicItemsDialogs/texture/SubTexture/memoryBuffer";
import TexturePropertiesBase from '../propertiesBase/texturePropertiesBase';
import { setVirtualFSSerialNumber } from "../redux/MapCore/mapCoreAction";
import store from "../redux/store";
import objectWorldTreeService from "./objectWorldTree.service";

export enum TextureTypeEnum {
    FromList,
    HBitmap,
    HIcon,
    ImageFile,
    MemoryBuffer,
    TextureArray,
    Video
};


class TextureService {
    textureType: TextureTypeEnum;

    //create
    public async createTexture(textureProperties: TexturePropertiesBase) {
        let texture: MapCore.IMcTexture = null;
        switch (this.textureType) {
            case TextureTypeEnum.ImageFile:
                texture = await this.createImageFileTexture(textureProperties);
                break;
            case TextureTypeEnum.Video:
                texture = this.createVideoTexture(textureProperties);
                break;
            case TextureTypeEnum.MemoryBuffer:
                texture = this.createMemoryBufferTexture(textureProperties);
                break;
            case TextureTypeEnum.TextureArray:
                texture = this.createTextureArrayTexture(textureProperties);
                break;
            default:
                break;
        }
        return texture
    }
    private createMemoryBufferTexture(textureProperties: TexturePropertiesBase) {
        let texture: MapCore.IMcTexture = null;
        runCodeSafely(() => {
            let finalBuffer: Uint8Array = textureProperties.memoryBufferProperties.rowPitch > textureProperties.memoryBufferProperties.width ?
                this.addRowPitchToBuffer(textureProperties) : textureProperties.memoryBufferProperties.mBtextureBuffer;
            runMapCoreSafely(() => {
                if (textureProperties.memoryBufferProperties.selectedOption == MemoryBufferTextureOptionsEnum.BUFFER) {
                    texture = MapCore.IMcMemoryBufferTexture.Create(textureProperties.memoryBufferProperties.width,
                        textureProperties.memoryBufferProperties.height,
                        textureProperties.memoryBufferProperties.pixelFormat.theEnum,
                        textureProperties.memoryBufferProperties.textureUseage.theEnum,
                        textureProperties.memoryBufferProperties.autoMipmap,
                        finalBuffer,
                        textureProperties.memoryBufferProperties.rowPitch,
                        textureProperties.textureFooterProperties.uniqueName)
                }
                else {
                    texture = MapCore.IMcMemoryBufferTexture.Create(textureProperties.memoryBufferProperties.width,
                        textureProperties.memoryBufferProperties.height,
                        textureProperties.memoryBufferProperties.colorArrayColors,
                        textureProperties.memoryBufferProperties.colorPositions,
                        textureProperties.memoryBufferProperties.isColorInterpolation,
                        textureProperties.memoryBufferProperties.colorColumns,
                        textureProperties.memoryBufferProperties.colorArrPixelFormat.theEnum,
                        textureProperties.memoryBufferProperties.textureUseage.theEnum,
                        textureProperties.memoryBufferProperties.autoMipmap);
                }
            }, "TextureService.createMemoryBufferTexture => IMcMemoryBufferTexture.Create", true);
        }, "TextureService.createMemoryBufferTexture => TextureType.MemoryBuffer")
        return texture;
    }
    private createVideoTexture(textureProperties: TexturePropertiesBase) {
        let texture: MapCore.IMcTexture = null;
        runMapCoreSafely(() => {
            texture = MapCore.IMcHtmlVideoTexture.Create(textureProperties.videoProperties.NameSource,
                textureProperties.videoProperties.PlayInLoop,
                textureProperties.videoProperties.IsReadable)
        }, "TextureService.createVideoTexture => IMcHtmlVideoTexture.Create", true);
        this.setVideoState(texture as MapCore.IMcHtmlVideoTexture, textureProperties.videoProperties.State);
        return texture;
    }
    private createTextureArrayTexture(textureProperties: TexturePropertiesBase) {
        let texture: MapCore.IMcTexture = null;
        runMapCoreSafely(() => {
            texture = MapCore.IMcTextureArray.Create(textureProperties.textureArrayProperties.textureArray);
        }, "TextureService.createTextureArrayTexture => IMcHtmlVideoTexture.Create", true);
        return texture;
    }
    private async createImageFileTexture(textureProperties: TexturePropertiesBase) {
        let picTexture: MapCore.IMcImageFileTexture;
        let filesource: MapCore.SMcFileSource = await this.getFileSource(textureProperties);

        runMapCoreSafely(() => {
            picTexture = MapCore.IMcImageFileTexture.Create(filesource,
                textureProperties.textureFooterProperties.FillPattern,
                textureProperties.textureFooterProperties.IgnoreTransparentMargin,
                textureProperties.textureFooterProperties.TransparentColor,
                textureProperties.textureFooterProperties.colorSubstitutionsArr,
                textureProperties.textureFooterProperties.UseExisting
            );
        }, "TextureService.createImageFileTexture => IMcImageFileTexture.Create", true);
        return picTexture;
    }
    setVideoState(texture: MapCore.IMcHtmlVideoTexture, state: MapCore.IMcVideoTexture.EState) {
        if (texture != null) {
            try {
                runMapCoreSafely(() => { texture.SetState(state); }, "TextureService.setVideoState => IMcVideoTexture.SetState", true);
            }
            catch (err) {
                alert(err);
            }
        }
    }
    addRowPitchToBuffer = (textureProperties: TexturePropertiesBase) => {
        let pixelFormatSize = MapCore.IMcTexture.GetPixelFormatByteCount(textureProperties.memoryBufferProperties.pixelFormat.theEnum);
        let numWidthBytes = textureProperties.memoryBufferProperties.width * pixelFormatSize;
        let numRowPitchBytes = textureProperties.memoryBufferProperties.rowPitch * pixelFormatSize;
        let newBufferSize = textureProperties.memoryBufferProperties.rowPitch * textureProperties.memoryBufferProperties.height * pixelFormatSize;
        let newBuffer = new Uint8Array(newBufferSize);
        for (let i = 0; i < textureProperties.memoryBufferProperties.height; i++) {
            let currentSlicedBuffer = textureProperties.memoryBufferProperties.mBtextureBuffer.slice(i * numWidthBytes, (i + 1) * numWidthBytes);
            newBuffer.set(currentSlicedBuffer, i * numRowPitchBytes);
        }
        return newBuffer;
    };
    readFileAsByteArray = (file: File): Promise<Uint8Array> => {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onload = () => {
                const arrayBuffer = reader.result as ArrayBuffer;
                const byteArray = new Uint8Array(arrayBuffer);
                resolve(byteArray);
            };
            reader.onerror = () => {
                reject(new Error('Failed to read file as byte array.'));
            };
            reader.readAsArrayBuffer(file);
        });
    };
    getFileSource = async (textureProperties: TexturePropertiesBase) => {
        const virtualFSSerialNumber = store.getState().mapCoreReducer.virtualFSSerialNumber;
        const useFileExtension = textureProperties.imageFileProperties.useFileExtension;
        let fileSource: MapCore.SMcFileSource
        let file: File;
        let fileByteArray: Uint8Array;
        if (textureProperties.imageFileProperties.inputFile) {
            file = textureProperties.imageFileProperties.inputFile;
            fileByteArray = await this.readFileAsByteArray(file)
            if (textureProperties.imageFileProperties.isMemoryBuffer) {
                const extension = useFileExtension ? objectWorldTreeService.getFileExtension(textureProperties.imageFileProperties.nameOfFile) : '';
                fileSource = new MapCore.SMcFileSource(fileByteArray, true, extension);
            }
            else {
                runMapCoreSafely(() => { MapCore.IMcMapDevice.CreateFileSystemDirectory(`${virtualFSSerialNumber}`) }, "TextureService.createImageFileTexture => IMcMapDevice.CreateFileSystemDirectory", true);
                runMapCoreSafely(() => { MapCore.IMcMapDevice.CreateFileSystemFile(`${virtualFSSerialNumber}/${file.name}`, fileByteArray) }, "TextureService.createImageFileTexture => IMcMapDevice.CreateFileSystemFile", true);
                store.dispatch(setVirtualFSSerialNumber(virtualFSSerialNumber + 1))
                fileSource = new MapCore.SMcFileSource(`${virtualFSSerialNumber}/${file.name}`, false);
            }
        }
        if (textureProperties.imageFileProperties.fileUrl) {
            if (textureProperties.imageFileProperties.isMemoryBuffer) {
                const response = await fetch(textureProperties.imageFileProperties.fileUrl);
                const fileBlob = await response.blob();
                file = new File([fileBlob], 'filename');
                fileByteArray = await this.readFileAsByteArray(file)
                const extension = objectWorldTreeService.getFileExtension(textureProperties.imageFileProperties.fileUrl);
                fileSource = new MapCore.SMcFileSource(fileByteArray, true, useFileExtension ? extension : '');
            }
            else {
                fileSource = new MapCore.SMcFileSource(textureProperties.imageFileProperties.fileUrl, false);
            }
        }
        return fileSource;
    }

    //#region Update Texture
    public async updateTexture(textureProperties: TexturePropertiesBase, oldTexture: MapCore.IMcTexture) {
        let texture: MapCore.IMcTexture = null;
        switch (this.textureType) {
            case TextureTypeEnum.ImageFile:
                texture = await this.updateImageFileTexture(textureProperties, oldTexture as MapCore.IMcImageFileTexture);
                break;
            case TextureTypeEnum.Video:
                texture = this.createVideoTexture(textureProperties);
                break;
            case TextureTypeEnum.MemoryBuffer:
                texture = this.updateMemoryBufferTexture(textureProperties, oldTexture as MapCore.IMcMemoryBufferTexture);
                break;
            case TextureTypeEnum.TextureArray:
                texture = this.createTextureArrayTexture(textureProperties);
                break;
            default:
                break;
        }
        return texture
    }
    private updateMemoryBufferTexture = (textureProperties: TexturePropertiesBase, oldTexture: MapCore.IMcMemoryBufferTexture) => {
        runCodeSafely(() => {
            let finalBuffer: Uint8Array = textureProperties.memoryBufferProperties.rowPitch > textureProperties.memoryBufferProperties.width ?
                this.addRowPitchToBuffer(textureProperties) : textureProperties.memoryBufferProperties.mBtextureBuffer;
            if (textureProperties.memoryBufferProperties.selectedOption == MemoryBufferTextureOptionsEnum.BUFFER) {
                runMapCoreSafely(() => {
                    oldTexture.UpdateFromMemoryBuffer(textureProperties.memoryBufferProperties.width,
                        textureProperties.memoryBufferProperties.height,
                        textureProperties.memoryBufferProperties.pixelFormat.theEnum,
                        textureProperties.memoryBufferProperties.rowPitch, finalBuffer);
                }, "TextureService.updateMemoryBufferTexture => IMcMemoryBufferTexture.UpdateFromMemoryBuffer", true);
            }
            else {
                runMapCoreSafely(() => {
                    oldTexture.UpdateFromColorData(textureProperties.memoryBufferProperties.colorArrayColors,
                        textureProperties.memoryBufferProperties.colorPositions,
                        textureProperties.memoryBufferProperties.isColorInterpolation,
                        textureProperties.memoryBufferProperties.colorColumns);
                }, "TextureService.updateMemoryBufferTexture => IMcMemoryBufferTexture.UpdateFromColorData", true);
            }
        }, "TextureService.updateMemoryBufferTexture")
        return oldTexture;
    }
    private updateImageFileTexture = (textureProperties: TexturePropertiesBase, oldTexture: MapCore.IMcImageFileTexture) => {
        runCodeSafely(async () => {
            let filesource: MapCore.SMcFileSource = await this.getFileSource(textureProperties);

            runMapCoreSafely(() => {
                oldTexture.SetImageFile(filesource,
                    textureProperties.textureFooterProperties.TransparentColor,
                    textureProperties.textureFooterProperties.colorSubstitutionsArr);
            }, "TextureService.updateImageFileTexture => IMcImageFileTexture.SetImageFile", true);
        }, "TextureService.updateImageFileTexture")
        return oldTexture;
    }
    //#endregion
}
export default new TextureService();
