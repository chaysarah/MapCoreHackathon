
import { Button } from 'primereact/button';
import { Image } from 'primereact/image';
import { useEffect, useState } from 'react';
import { runCodeSafely, runMapCoreSafely } from '../../../../../../common/services/error-handling/errorHandler';
import { Dialog } from 'primereact/dialog';
import SaveToFile from '../../../objectWorldTree/overlayForms/saveToFile';
import { AppState } from '../../../../../../redux/combineReducer';
import { useSelector } from 'react-redux';
import "./styles/renderScreenRectToBufferImage.css";

export default function RenderScreenRectToBufferImage(props: { image: MapCore.IMcImage }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let [imageUrl, setImageUrl] = useState('');
    let [saveToFileDialogVisible, setSaveToFileDialogVisible] = useState(false);

    let savedFileTypeOptions: { name: string; extension: string; }[] = [
        { name: 'Bitmap files(*.bmp)', extension: '.bmp' },
        { name: 'JPEG files (*.jpg, *.jpeg)', extension: '.jpg' },
        { name: 'TIFF files (*.tif, *.tiff)', extension: '.tif' },
        { name: 'GIFF files (*.gif)', extension: '.gif' },
        { name: 'PNG files (*.png)', extension: '.png' },
        { name: 'Icon files (*.ico)', extension: '.ico' },
        { name: 'All Files', extension: '' },
    ];

    useEffect(() => {
        runCodeSafely(() => {
            setDialogWidth();
            let imageBuffer;
            runMapCoreSafely(() => {
                imageBuffer = props.image.Save('png')
            }, 'RenderScreenRectToBufferImage.useEffect => IMcImage.Save', true);
            const blob = new Blob([imageBuffer]);
            const url = URL.createObjectURL(blob);
            setImageUrl(url)
        }, 'RenderScreenRectToBufferImage.useEffect')
    }, [])

    const setDialogWidth = () => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.6 * globalSizeFactor;
        root.style.setProperty('--render-screen-to-buf-img-dialog', `${pixelWidth}px`);
    }
    const handleSaveToFileOK = (fileName: string, fileType: string) => {
        runCodeSafely(() => {
            let imageBuffer;
            let extension = fileType != '' ? fileType.split('.')[1] : fileType;
            runMapCoreSafely(() => {
                imageBuffer = props.image.Save(extension)
            }, 'RenderScreenRectToBufferImage.handleSaveToFileClick => IMcImage.Save', true)
            let finalFileName = `${fileName}${fileType}`;
            runMapCoreSafely(() => {
                MapCore.IMcMapDevice.DownloadBufferAsFile(imageBuffer, finalFileName);
            }, 'RenderScreenRectToBufferImage.handleSaveToFileClick => IMcMapDevice.DownloadBufferAsFile', true)
            setSaveToFileDialogVisible(false)
        }, 'RenderScreenRectToBufferImage.handleSaveToFileClick')
    }

    return <div className='form__column-container'>
        <Image src={imageUrl} alt="Image" width="700" preview />
        <Button label='Save To File' onClick={() => setSaveToFileDialogVisible(true)} />

        <Dialog className="scroll-dialog-save-to-file" header="Save to File" visible={saveToFileDialogVisible} onHide={() => { setSaveToFileDialogVisible(false) }}>
            <SaveToFile savedFileTypeOptions={savedFileTypeOptions} handleSaveToFileOk={handleSaveToFileOK} />
        </Dialog>
    </div>
}