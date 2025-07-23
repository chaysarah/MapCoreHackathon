import React, { useState } from 'react';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { MapCoreData } from 'mapcore-lib';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { closeDialog, setDialogType } from '../../../../../../redux/MapCore/mapCoreAction';
import { useDispatch } from 'react-redux';
import { Checkbox } from 'primereact/checkbox';
import generalService from '../../../../../../services/general.service';
import { runCodeSafely } from '../../../../../../common/services/error-handling/errorHandler';
import { DialogTypesEnum } from '../../../../../../tools/enum/enums';
import TexturePropertiesBase from '../../../../../../propertiesBase/texturePropertiesBase';


export default function HIcon(props: {
    defaultTexture: MapCore.IMcIconHandleTexture,
    tabInfo: {
        properties: TexturePropertiesBase,
        setPropertiesCallback: (tab: string, key: string, value: any) => void,
    }
}) {

    return (
        <div style={{ height: '100%' }}>
            <h4>Currently Not Supported</h4>

        </div >
    )
}