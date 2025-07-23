import { enums } from "mapcore-lib";
import { SET_DIALOG_TYPE, SET_MAXIMAIZE_WINDOW, SET_SCREEN_RESOLUTION, SET_INIT_MAPCORE, SET_All_POLYGON_CONTOURS_ARR, SET_DISPLAY_TARGET, ADD_DISPLAY_TARGET, ADD_ERROR_TO_ERROR_LIST, SET_ERROR_TO_ERROR_LIST, CLOSE_DIALOG, ADD_MINIMIZED_DIALOG, REMOVE_MINIMIZED_DIALOG, SET_GLOBAL_SIZE_FACTOR, SET_VIRTUAL_FS_SERIAL_NUMBER,SET_IS_CATCH_ERRORS } from "../actionTypes";
import { AppState } from "../combineReducer";
import { DialogTypesEnum } from "../../tools/enum/enums";
import store from "../store";
import { removeMinimizedDialog } from "./mapCoreAction";

export interface ScreenResolution {
    innerHeight: number,
    innerWidth: number
}
export interface MapCoreState {
    globalSizeFactor: number,
    mapCoreInitialized: boolean;
    dialogTypesArr: DialogTypesEnum[];
    maximaizeWindow: number;
    screenResolution: ScreenResolution;
    displayTarget: any[];
    allPolygonContoursArr: MapCore.IMcObject[];
    errorList: string[];
    FormHasUnappliedChanges: boolean;
    minimizedDialogs: DialogTypesEnum[];
    virtualFSSerialNumber: number;
    isCatchErrors: boolean
}
const initialState: MapCoreState = {
    globalSizeFactor: 1,
    mapCoreInitialized: false,
    dialogTypesArr: [],
    maximaizeWindow: 0,
    screenResolution: { innerHeight: 0, innerWidth: 0 },
    displayTarget: [],
    allPolygonContoursArr: [],
    errorList: [],
    FormHasUnappliedChanges: false,
    minimizedDialogs: [],
    virtualFSSerialNumber: 1,
    isCatchErrors: true
}

const mapCoreReducer = (state = initialState, action: { type: string; payload: any; }) => {
    switch (action.type) {
        case SET_GLOBAL_SIZE_FACTOR:
            return {
                ...state,
                globalSizeFactor: action.payload,
            };
        case SET_INIT_MAPCORE:
            return {
                ...state,
                mapCoreInitialized: action.payload,
            };
        case SET_DIALOG_TYPE:
            let filteredDialogTypesArr = state.dialogTypesArr.includes(action.payload) ?
                state.dialogTypesArr.filter(dialogTypeEnum => action.payload !== dialogTypeEnum) : state.dialogTypesArr;
            let updatesDialogTypesArr = [...filteredDialogTypesArr, action.payload];
            return {
                ...state,
                dialogTypesArr: updatesDialogTypesArr,
            };
        case CLOSE_DIALOG:
            let isIncludes = state.dialogTypesArr.includes(action.payload);
            let filteredDialogTypes = isIncludes ?
                state.dialogTypesArr.filter(dialogTypeEnum => action.payload !== dialogTypeEnum) : state.dialogTypesArr;
            return {
                ...state,
                dialogTypesArr: filteredDialogTypes,
            };
        case SET_SCREEN_RESOLUTION:
            return {
                ...state,
                screenResolution: action.payload,
            };
        case SET_MAXIMAIZE_WINDOW:
            return {
                ...state,
                maximaizeWindow: action.payload,
            };
        case ADD_DISPLAY_TARGET:
            return {
                ...state,
                displayTarget: [...state.displayTarget, action.payload],
            };
        case SET_DISPLAY_TARGET:
            return {
                ...state,
                displayTarget: action.payload,
            };
        case SET_All_POLYGON_CONTOURS_ARR:
            return {
                ...state,
                allPolygonContoursArr: action.payload,
            };
        case ADD_ERROR_TO_ERROR_LIST:
            return {
                ...state,
                errorList: [...state.errorList, action.payload],
            };
        case SET_ERROR_TO_ERROR_LIST:
            return {
                ...state,
                errorList: action.payload,
            };
        case ADD_MINIMIZED_DIALOG:
            let isMinimizeIncludes = state.minimizedDialogs.includes(action.payload);
            let UpdatedMinimizedDialogTypes = isMinimizeIncludes ?
                state.minimizedDialogs : [...state.minimizedDialogs, action.payload];
            return {
                ...state,
                minimizedDialogs: UpdatedMinimizedDialogTypes,
            };
        case REMOVE_MINIMIZED_DIALOG:
            let isMinimizedIncludes = state.minimizedDialogs.includes(action.payload);
            let filteredMinimizedDialogTypes = isMinimizedIncludes ?
                state.minimizedDialogs.filter(minimizedDialog => action.payload !== minimizedDialog) : state.minimizedDialogs;
            return {
                ...state,
                minimizedDialogs: filteredMinimizedDialogTypes,
            };
        case SET_VIRTUAL_FS_SERIAL_NUMBER:
            return {
                ...state,
                virtualFSSerialNumber: action.payload,
            };
        case SET_IS_CATCH_ERRORS:
            return {
                ...state,
                isCatchErrors: action.payload,
            };
    }

    return state;
};

export const selectMapcoreInitialized = (state: AppState) => {
    return state.mapCoreReducer.mapCoreInitialized;
}

export default mapCoreReducer;



