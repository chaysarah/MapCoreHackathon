import { Button } from "primereact/button";
import { runCodeSafely, runMapCoreSafely } from "../../../common/services/error-handling/errorHandler";
import { useDispatch, useSelector } from "react-redux";
import { MapCoreData, ObjectWorldService, getEnumValueDetails, getEnumDetailsList, ViewportData } from 'mapcore-lib';
import { setShowDialog } from "../../../redux/ObjectWorldTree/objectWorldTreeActions";
import { hideFormReasons } from "../../shared/models/tree-node.model";
import { DialogTypesEnum } from "../../../tools/enum/enums";
import { useState } from "react";
import { ConfirmDialog } from "primereact/confirmdialog";
import { AppState } from "../../../redux/combineReducer";

export const createItemAndObj = (activeOverlay: MapCore.IMcOverlay, objectPoints: MapCore.SMcVector3D[]) => {
    let objSchemeItem: MapCore.IMcObjectSchemeItem = null;
    let obj: MapCore.IMcObject = null;
    runCodeSafely(() => {
        let EItemSubTypeFlags = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
        let subItemsFlag = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ATTACHED_TO_TERRAIN, EItemSubTypeFlags).code |
            getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code;

        runMapCoreSafely(() => {
            objSchemeItem = MapCore.IMcLineItem.Create(subItemsFlag
                , MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID,
                MapCore.bcBlackOpaque, 3);
        }, 'DrawLineCtrl.createItemAndObj => IMcLineItem.Create', true)

        runMapCoreSafely(() => {
            obj = MapCore.IMcObject.Create(activeOverlay,
                objSchemeItem,
                MapCore.EMcPointCoordSystem.EPCS_WORLD,
                objectPoints,
                false);
        }, 'DrawLineCtrl.createItemAndObj => IMcObject.Create', true)

    }, 'DrawLineCtrl.createItemAndObj')
    return { obj: obj, objSchemeItem: objSchemeItem };
}

export default function DrawLineCtrl(props: { activeViewport: ViewportData, initItemResultsCB: (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) => void, label?: string, disabled?: boolean, className?: string, style?: any, handleDrawLineButtonClick?: () => void }) {
    const dispatch = useDispatch();
    const dialogTypesArr: DialogTypesEnum[] = useSelector((state: AppState) => state.mapCoreReducer.dialogTypesArr);
    const [confirmDialogVisible, setConfirmDialogVisible] = useState(false);

    const getActiveOverlay = () => {
        let activeOverlay: MapCore.IMcOverlay = null;
        runCodeSafely(() => {
            let typedMcCurrentSpatialQueries = props.activeViewport?.viewport as MapCore.IMcSpatialQueries;
            if (typedMcCurrentSpatialQueries && typedMcCurrentSpatialQueries.GetInterfaceType() == MapCore.IMcMapViewport.INTERFACE_TYPE) {
                let mcCurrentViewport = typedMcCurrentSpatialQueries as MapCore.IMcMapViewport;
                activeOverlay = ObjectWorldService.findActiveOverlayByMcViewport(mcCurrentViewport);
            }
        }, 'DrawLineCtrl.getActiveOverlay');
        return activeOverlay;
    }
    const startDrawOrEditLine = (viewportData: ViewportData, objAndSchemeObject: { obj: MapCore.IMcObject, objSchemeItem: MapCore.IMcObjectSchemeItem }) => {
        runCodeSafely(() => {
            runMapCoreSafely(() => { viewportData.editMode.StartInitObject(objAndSchemeObject.obj, objAndSchemeObject.objSchemeItem); }, "DrawLineCtrl.startDrawOrEditPolygon => IMcEditMode.StartInitObject", true);

            let iMcEditModeCallback: any = new MapCoreData.iMcEditModeCallbacksClass({
                InitItemResults: (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) => {
                    runCodeSafely(() => {
                        props.initItemResultsCB(pObject, pItem, nExitCode)
                        dispatch(setShowDialog({ hideFormReason: null, dialogType: null }));
                    }, 'DrawLineCtrl.startDrawOrEditPolygon => InitItemResults')
                }
            });
            runMapCoreSafely(() => { viewportData.editMode.SetEventsCallback(iMcEditModeCallback); }, "DrawLineCtrl.startDrawOrEditPolygon => IMcEditMode.SetEventsCallback", true)
        }, 'DrawLineCtrl.startDrawOrEditPolygon')
    }

    const handleDrawLineButtonClick = () => {
        runCodeSafely(() => {
            let activeOverlay = getActiveOverlay();
            if (activeOverlay) {
                props.handleDrawLineButtonClick && props.handleDrawLineButtonClick();
                let lastDialogType = dialogTypesArr[dialogTypesArr.length - 1];
                dispatch(setShowDialog({ hideFormReason: hideFormReasons.PAINT_LINE, dialogType: lastDialogType }))
                let objAndSchemeObject = createItemAndObj(activeOverlay, []);

                startDrawOrEditLine(props.activeViewport, objAndSchemeObject);
            }
            else {
                setConfirmDialogVisible(true);
            }
        }, 'DrawLineCtrl.handleDrawLineButtonClick');
    }


    return <div className={props.className} style={props.style}>
        <Button className={!props.disabled && props.activeViewport ? '' : 'form__disabled'} label={props.label ? props.label : 'Draw Line'} onClick={handleDrawLineButtonClick} />

        <ConfirmDialog
            contentClassName='form__confirm-dialog-content'
            message='No active overlay'
            header=''
            footer={<div></div>}
            visible={confirmDialogVisible}
            onHide={e => { setConfirmDialogVisible(false) }}
        />
    </div>
}