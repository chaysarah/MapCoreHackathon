import { enums, MapCoreData } from "mapcore-lib";
import * as actionTypes from "../actionTypes"
import { DialogTypesEnum } from "../../tools/enum/enums";
import { setShowDialog } from "../ObjectWorldTree/objectWorldTreeActions";
import store from "../store";
import { Dialog } from "primereact/dialog";


const setGlobalSizeFactor = (sizeFactor: number) => {
    return {
        type: actionTypes.SET_GLOBAL_SIZE_FACTOR,
        payload: sizeFactor,
    };
}
const setScreenResolution = (resolutionScreen: any) => {
    return {
        type: actionTypes.SET_SCREEN_RESOLUTION,
        payload: resolutionScreen,
    };
}
const setInitMapCore = (bool: boolean) => {
    return {
        type: actionTypes.SET_INIT_MAPCORE,
        payload: bool,
    };
};

const setDialogType = (dialogType: DialogTypesEnum) => {
    store.dispatch(removeMinimizedDialog(dialogType));
    store.dispatch(setShowDialog({ hideFormReason: null, dialogType: null }))
    return {
        type: actionTypes.SET_DIALOG_TYPE,
        payload: dialogType,
    };
};

const closeDialog = (dialogType: DialogTypesEnum) => {
    store.dispatch(setShowDialog({ hideFormReason: null, dialogType: null }))
    store.dispatch(removeMinimizedDialog(dialogType))
    return {
        type: actionTypes.CLOSE_DIALOG,
        payload: dialogType,
    };
};

const setmaximaizeWindow = (window: number) => {
    return {
        type: actionTypes.SET_MAXIMAIZE_WINDOW,
        payload: window,
    };
};
const addDisplayTarget = (displayTargetAdd: any) => {
    return {
        type: actionTypes.ADD_DISPLAY_TARGET,
        payload: displayTargetAdd,
    };
};
const setDisplayTarget = (displayTarget: []) => {
    return {
        type: actionTypes.SET_DISPLAY_TARGET,
        payload: displayTarget,
    };
};
const setallPolygonContoursArr = (arr: MapCore.IMcObject[]) => {
    return {
        type: actionTypes.SET_All_POLYGON_CONTOURS_ARR,
        payload: arr,
    };
};

const addErrortoErrorList = (errMsg: string) => {
    return {
        type: actionTypes.ADD_ERROR_TO_ERROR_LIST,
        payload: { sticky: true, severity: 'error', summary: 'Error', detail: errMsg },
    };
};
const setErrortoErrorList = (ErrorList: any[]) => {
    return {
        type: actionTypes.SET_ERROR_TO_ERROR_LIST,
        payload: ErrorList,
    };
};
const addMinimizedDialog = (dialogType: DialogTypesEnum) => {
    return {
        type: actionTypes.ADD_MINIMIZED_DIALOG,
        payload: dialogType,
    };
};
const removeMinimizedDialog = (dialogType: DialogTypesEnum) => {
    return {
        type: actionTypes.REMOVE_MINIMIZED_DIALOG,
        payload: dialogType,
    };
};
const setVirtualFSSerialNumber = (virtualFSSerialNumber: number) => {
    return {
        type: actionTypes.SET_VIRTUAL_FS_SERIAL_NUMBER,
        payload: virtualFSSerialNumber,
    };
};
const setIsCatchErrors = (IsCatchErrors: boolean) => {
    MapCoreData.isCatchErrors = IsCatchErrors;
    return {
        type: actionTypes.SET_IS_CATCH_ERRORS,
        payload: IsCatchErrors,
    };
};
export {
    setScreenResolution, setDialogType, closeDialog, setmaximaizeWindow, setInitMapCore,
    addDisplayTarget, setDisplayTarget, setallPolygonContoursArr, addErrortoErrorList, setErrortoErrorList,
    addMinimizedDialog, removeMinimizedDialog, setGlobalSizeFactor, setVirtualFSSerialNumber,setIsCatchErrors
};
