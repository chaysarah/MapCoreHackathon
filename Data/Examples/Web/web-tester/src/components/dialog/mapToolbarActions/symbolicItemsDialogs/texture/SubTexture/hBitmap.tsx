import React, { useState } from 'react';
import { MapCoreData } from 'mapcore-lib';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import Footer from '../../../../footerDialog';
import { closeDialog, setDialogType } from '../../../../../../redux/MapCore/mapCoreAction';
import { useDispatch } from 'react-redux';
import { Checkbox } from 'primereact/checkbox';
import { FileUpload } from 'primereact/fileupload';
import generalService from '../../../../../../services/general.service';
import { runCodeSafely } from '../../../../../../common/services/error-handling/errorHandler';
import { DialogTypesEnum } from '../../../../../../tools/enum/enums';
import TexturePropertiesBase from '../../../../../../propertiesBase/texturePropertiesBase';

export default function HBitmap(props: {
    defaultTexture: MapCore.IMcBitmapHandleTexture,
    tabInfo: {
        properties: TexturePropertiesBase,
        setPropertiesCallback: (tab: string, key: string, value: any) => void,
    }
}) {
    const dispatch = useDispatch();

    return (
        <div style={{ height: '100%' }}>
            <h4>Currently Not Supported</h4>
        </div >
    )
}