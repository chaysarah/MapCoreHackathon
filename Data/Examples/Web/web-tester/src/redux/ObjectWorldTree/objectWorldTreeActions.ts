import * as actionTypes from "../actionTypes";
import TreeNodeModel, { hideFormReasons } from '../../components/shared/models/tree-node.model'
import { DialogTypesEnum } from "../../tools/enum/enums";
import SecondDialogModel from "../../components/shared/models/second-dialog.model";
import { SavedFileData } from "../../services/savedFiles.service";

const setObjectWorldTree = (tree: TreeNodeModel) => {
    return {
        type: actionTypes.SET_OBJECT_WORLD_TREE,
        payload: tree,
    };
};
const setFooterVersion = (footerVersion: { name: string; code: number; theEnum: any; }) => {
    return {
        type: actionTypes.SET_FOOTER_VERSION,
        payload: footerVersion,
    };
};
const setObjectsMemoryBuffer = (uint8ArrayData: Uint8Array) => {
    return {
        type: actionTypes.SET_OBJECTS_MEMORY_BUFFER,
        payload: uint8ArrayData,
    };
};
const setSelectedNodeInTree = (currentNode: TreeNodeModel) => {
    return {
        type: actionTypes.SET_SELECTED_NODE_IN_TREE,
        payload: currentNode,
    };
};
const addSavedObjectDataToMap = (mcObject: MapCore.IMcObject, objectSavedFileData: SavedFileData) => {
    return {
        type: actionTypes.ADD_SAVED_OBJECT_DATA_TO_MAP,
        payload: [mcObject, objectSavedFileData],
    };
};
const addSavedSchemeDataToMap = (mcScheme: MapCore.IMcObjectScheme, schemeSavedFileData: SavedFileData) => {
    return {
        type: actionTypes.ADD_SAVED_SCHEME_DATA_TO_MAP,
        payload: [mcScheme, schemeSavedFileData],
    };
};
const removeSavedObjectFilePathFromMap = (mcObject: MapCore.IMcObject) => {
    return {
        type: actionTypes.REMOVE_SAVED_OBJECT_FILE_PATH_FROM_MAP,
        payload: mcObject,
    };
};
const setTypeObjectWorldDialogSecond = (typeDialog: SecondDialogModel) => {
    return {
        type: actionTypes.SET_TYPE_OBJECT_WORLD_DIALOG_SECOND,
        payload: typeDialog
    };
};
const setSchemesMemoryBuffer = (uint8ArrayData: Uint8Array) => {
    return {
        type: actionTypes.SET_SCHEMES_MEMORY_BUFFER,
        payload: uint8ArrayData
    };
};
const setShowDialog = (hideFormReasonObj: { hideFormReason: hideFormReasons, dialogType: DialogTypesEnum }) => {
    return {
        type: actionTypes.SET_SHOW_DIALOG,
        payload: hideFormReasonObj
    };
};
const setHandleApplyFunc = (handleApplyFunc: () => void) => {
    return {
        type: actionTypes.SET_HANDLE_APPLY_FUNC,
        payload: handleApplyFunc
    };
};
const setLocationSelectorObject = (selectorObj: { selector: MapCore.IMcLocationConditionalSelector, object: MapCore.IMcObject }) => {
    return {
        type: actionTypes.SET_LOCATION_SELECTOR_OBJECT,
        payload: selectorObj
    };
};
const setLocationSelectorObjectArr = (selectorObjArr: { selector: MapCore.IMcLocationConditionalSelector, object: MapCore.IMcObject }[]) => {
    return {
        type: actionTypes.SET_LOCATION_SELECTOR_OBJECT_ARR,
        payload: selectorObjArr
    };
};
export {
    setObjectWorldTree,
    setFooterVersion,
    setObjectsMemoryBuffer,
    setSelectedNodeInTree,
    addSavedObjectDataToMap,
    addSavedSchemeDataToMap,
    removeSavedObjectFilePathFromMap,
    setTypeObjectWorldDialogSecond,
    setSchemesMemoryBuffer,
    setShowDialog,
    setHandleApplyFunc,
    setLocationSelectorObject,
    setLocationSelectorObjectArr,
};
