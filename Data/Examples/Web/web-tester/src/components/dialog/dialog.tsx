import React, { ReactElement, useEffect, useRef, useState } from 'react';
import SeveralLayersDialog from './openMap/layerSelector/openViewportWithSeveralLayers';
import OpenObjectPropertiesDialog from './openMap/objectProperties/objectPropertiesDialog';
import OpenObjectWorldTreeDialog from './treeView/objectWorldTree/objectWorldTreeDialog';
import { Dialog } from 'primereact/dialog';
import Header from './headerDialog';
import { useDispatch, useSelector } from 'react-redux';
import { addMinimizedDialog, closeDialog, setDialogType } from '../../redux/MapCore/mapCoreAction';
import { AppState } from '../../redux/combineReducer';
import TextureDialog, { TextureDialogActionMode } from './mapToolbarActions/symbolicItemsDialogs/texture/textureDialog';
import AddJsonViewport from './openMap/addJsonViewport/addJsonViewport';
import ScanGeometry from './mapToolbarActions/mapOperations/scan/scanGeometry';
import ScanResult from './mapToolbarActions/mapOperations/scan/scanResult';
import { DialogTypesEnum } from '../../tools/enum/enums';
import { hideFormReasons } from '../shared/models/tree-node.model';
import Font, { FontDialogActionMode } from './mapToolbarActions/symbolicItemsDialogs/font/font';
import generalService from '../../services/general.service';
import EditModePropertiesDialog from './mapToolbarActions/editMode/editModeProperties/editModePropertiesDialog';
import CheckRenderNeeded from './mapToolbarActions/editMode/checkRenderNeeded/checkRenderNeeded';
import { ObjectWorldService, ViewportService, MapCoreData, ViewportData, getEnumValueDetails, getEnumDetailsList } from 'mapcore-lib';
import OpenObjectItemsSelectListForm from './mapToolbarActions/editMode/sharedComponents/objectItemsSelectList'
import { Id } from '../../tools/enum/enumsOfScheme'
import EventCallbackDialog from './mapToolbarActions/editMode/eventCallback/eventCallbackDialog';
import store from '../../redux/store';
import { setCreateText, setNewGeneratedObject } from '../../redux/mapWindow/mapWindowAction';
import symbolicItemsService from '../../services/symbolicItems.service';
import { runAsyncCodeSafely, runCodeSafely } from '../../common/services/error-handling/errorHandler';
// css imports
import './styles/forms.css'
import MapWorldTreeDialog from './treeView/mapWorldTree/mapWorldTreeDialog';
import TexturePropertiesBase from '../../propertiesBase/texturePropertiesBase';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMinus } from "@fortawesome/free-solid-svg-icons";
import OpenMapManually from './openMap/objectProperties/openMapManually/openMapManully';
import StandAloneSpatialQueries from './calculations/standAloneSpatialQueries/standAloneSpatialQueries';
import PrintMap from './mapToolbarActions/mapOperations/printMap/printMap';

export default function GenericDialog() {
    const dispatch = useDispatch();
    const dialogTypesArr: DialogTypesEnum[] = useSelector((state: AppState) => state.mapCoreReducer.dialogTypesArr);
    const minimizedDialogs: DialogTypesEnum[] = useSelector((state: AppState) => state.mapCoreReducer.minimizedDialogs);
    const hideFormReason: { hideFormReason: hideFormReasons, dialogType: DialogTypesEnum } = useSelector((state: AppState) => state.objectWorldTreeReducer.showDialog);
    const activeCard: number = useSelector((state: AppState) => state.mapWindowReducer.activeCard);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor);
    const [dialogFooterMap, setDialogFooterMap] = useState(new Map());
    const dialogFooterMapRef = useRef(new Map());

    const [dialogsMap] = useState(new Map([
        [DialogTypesEnum.chooseLayers, <SeveralLayersDialog />],
        [DialogTypesEnum.objectProperties, <OpenObjectPropertiesDialog FooterHook={footer => (setDialogFooter(DialogTypesEnum.objectProperties, footer))} />],
        [DialogTypesEnum.createTexture, <TextureDialog
            textureClose={async (texture: MapCore.IMcTexture) => {
                if (texture) {
                    generalService.ObjectProperties.PicTexture = texture;
                    let fileName = symbolicItemsService.getFilePathByProperties("default_pic999_pp.mcsch");
                    let scheme = await symbolicItemsService.FetchObjectScheme(fileName);
                    symbolicItemsService.DoStartInitObject(scheme);
                }
                dispatch(closeDialog(DialogTypesEnum.createTexture));
            }}
            defaultTexture={null}
            isSetAsDefault={true}
            actionMode={TextureDialogActionMode.create}
            texturePropertiesBase={generalService.TextureProperties}
            saveTexturePropertiesCB={(textureProp: TexturePropertiesBase) => {
                generalService.TextureProperties = textureProp;
                generalService.ObjectProperties.PicIsDefaultTexture = textureProp.textureFooterProperties.isDefault
            }}
            footerHook={footer => (setDialogFooter(DialogTypesEnum.createTexture, footer))}
        />],
        [DialogTypesEnum.scanGeometry, <ScanGeometry FooterHook={footer => (setDialogFooter(DialogTypesEnum.scanGeometry, footer))} />],
        [DialogTypesEnum.scanResult, <ScanResult FooterHook={footer => (setDialogFooter(DialogTypesEnum.scanResult, footer))} />],
        [DialogTypesEnum.objectWorldTree, <OpenObjectWorldTreeDialog FooterHook={footer => (setDialogFooter(DialogTypesEnum.objectWorldTree, footer))} />],
        [DialogTypesEnum.mapWorldTree, <MapWorldTreeDialog FooterHook={footer => (setDialogFooter(DialogTypesEnum.mapWorldTree, footer))} />],
        [DialogTypesEnum.font, <Font
            getFont={(font: MapCore.IMcFont, isSetAsDefault: boolean) => {
                runAsyncCodeSafely(async () => {
                    generalService.ObjectProperties.isFontSetAsDefault = isSetAsDefault;
                    generalService.ObjectProperties.TextFont = font;
                    let fileName = symbolicItemsService.getFilePathByProperties("default_text999_pp.mcsch");
                    let lineScheme = await symbolicItemsService.FetchObjectScheme(fileName);
                    store.dispatch(setCreateText(true));
                    symbolicItemsService.DoStartInitObject(lineScheme);
                    dispatch(closeDialog(DialogTypesEnum.font));
                }, 'Dialog.getFont')
            }}
            defaultFont={null}
            isSetAsDefaultCheckbox={true}
            actionMode={FontDialogActionMode.create} />],
        [DialogTypesEnum.addJsonViewport, <AddJsonViewport />],
        [DialogTypesEnum.editObject, <OpenObjectItemsSelectListForm footerHook={footer => (setDialogFooter(DialogTypesEnum.editObject, footer))} handleOKFunc={(object: MapCore.IMcObject, item: MapCore.IMcObjectSchemeItem) => {
            let activeViewport = MapCoreData.findViewport(activeCard);
            ObjectWorldService.doEdit(activeViewport, object, item);
        }} />],
        [DialogTypesEnum.initObject, <OpenObjectItemsSelectListForm footerHook={footer => (setDialogFooter(DialogTypesEnum.initObject, footer))} handleOKFunc={(object: MapCore.IMcObject, item: MapCore.IMcObjectSchemeItem) => {
            let activeViewport = MapCoreData.findViewport(activeCard);
            ObjectWorldService.doInit(activeViewport, object, item);
        }} />],
        [DialogTypesEnum.editModeProperties, <EditModePropertiesDialog FooterHook={footer => (setDialogFooter(DialogTypesEnum.editModeProperties, footer))} />],
        [DialogTypesEnum.checkRenderNeeded, <CheckRenderNeeded footerHook={footer => (setDialogFooter(DialogTypesEnum.checkRenderNeeded, footer))} />],
        [DialogTypesEnum.eventCallback, <EventCallbackDialog footerHook={footer => (setDialogFooter(DialogTypesEnum.eventCallback, footer))} />],
        [DialogTypesEnum.addMapManuly, <OpenMapManually></OpenMapManually>],
        [DialogTypesEnum.standAloneSpatialQueries, <StandAloneSpatialQueries footerHook={footer => (setDialogFooter(DialogTypesEnum.standAloneSpatialQueries, footer))} />],
        [DialogTypesEnum.printMap, <PrintMap FooterHook={footer => (setDialogFooter(DialogTypesEnum.printMap, footer))} />],
    ]));

    const setDialogFooter = (dialogType: DialogTypesEnum, footerFunc: () => ReactElement) => {
        setDialogFooterMap(new Map(dialogFooterMapRef.current).set(dialogType, footerFunc()));
        dialogFooterMapRef.current = dialogFooterMapRef.current.set(dialogType, footerFunc())
    }

    const isModalDialog = (): boolean => {
        let nonModalDialogs = [DialogTypesEnum.scanResult, DialogTypesEnum.printMap, DialogTypesEnum.scanGeometry, DialogTypesEnum.mapWorldTree, DialogTypesEnum.objectWorldTree, DialogTypesEnum.eventCallback, DialogTypesEnum.checkRenderNeeded, DialogTypesEnum.standAloneSpatialQueries];
        let isDialogTypeInclude = nonModalDialogs.includes(dialogTypesArr[dialogTypesArr.length - 1]);
        if (hideFormReason.hideFormReason !== null || isDialogTypeInclude) {
            return false;
        }
        let isLastDialogMinimizeIncludes = minimizedDialogs.includes(dialogTypesArr[dialogTypesArr.length - 1])
        if (isLastDialogMinimizeIncludes) {
            return false;
        }
        return true;
    }

    const isHiddenDialog = (dialogType: DialogTypesEnum) => {
        let lastDialogType = dialogTypesArr[dialogTypesArr.length - 1];
        if (hideFormReason.hideFormReason !== null && lastDialogType == hideFormReason.dialogType) {
            return true;
        }
        else if (minimizedDialogs.includes(dialogType)) {
            return true;
        }
        return false;
    }

    return (
        <div>
            {dialogTypesArr.length != 0 && dialogTypesArr.map((dialogType, index) => {
                return <Dialog
                    key={index}
                    className={isHiddenDialog(dialogType) ? `object-props__hidden-dialog scroll-dialog-${dialogType}` : `scroll-dialog-${dialogType}`}
                    style={{ display: 'flex', justifyContent: 'space-around' }}
                    visible={true}
                    header={<Header dialogType={dialogType}></Header>}
                    icons={<div style={{ width: `${globalSizeFactor * 2.3}vh`, height: `${globalSizeFactor * 2.3}vh`, cursor: 'pointer' }}>
                        <FontAwesomeIcon icon={faMinus} onClick={() => { dispatch(addMinimizedDialog(dialogType)) }} />
                    </div>}
                    onHide={() => {
                        dispatch(closeDialog(dialogType))
                    }}
                    modal={isModalDialog()}
                    footer={dialogFooterMap.get(dialogType)}
                >
                    {dialogsMap.get(dialogType!)}
                </Dialog>
            })}
        </ div>
    )
}

export abstract class Properties {
    static getDefault<T>(props: any): T {
        throw new Error("getDefault not implemented")
    }
};
