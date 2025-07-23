import _ from "lodash"

import { ADD_SAVED_OBJECT_DATA_TO_MAP, SET_OBJECT_WORLD_TREE, SET_OBJECTS_MEMORY_BUFFER, SET_SELECTED_NODE_IN_TREE, SET_TYPE_OBJECT_WORLD_DIALOG_SECOND, SET_SCHEMES_MEMORY_BUFFER, SET_SHOW_DIALOG, SET_HANDLE_APPLY_FUNC, SET_LOCATION_SELECTOR_OBJECT, SET_LOCATION_SELECTOR_OBJECT_ARR, REMOVE_SAVED_OBJECT_FILE_PATH_FROM_MAP, SET_FOOTER_VERSION, ADD_SAVED_SCHEME_DATA_TO_MAP } from "../actionTypes";
import TreeNodeModel, { hideFormReasons, objectWorldNodeType } from '../../components/shared/models/tree-node.model'
import { DialogTypesEnum } from "../../tools/enum/enums";
import SecondDialogModel from "../../components/shared/models/second-dialog.model";
import { SavedFileData } from "../../services/savedFiles.service";

export interface ObjectWorldTreeState {
    objectWorldTree: TreeNodeModel,
    footerVersion: { name: string; code: number; theEnum: any; },
    objectsMemoryBuffer: Uint8Array,
    schemesMemoryBuffer: Uint8Array,
    selectedNodeInTree: TreeNodeModel,
    savedObjectDataMap: Map<MapCore.IMcObject, SavedFileData>,
    savedSchemeDataMap: Map<MapCore.IMcObjectScheme, SavedFileData>,
    typeObjectWorldDialogSecond: SecondDialogModel,
    showDialog: { hideFormReason: hideFormReasons, dialogType: DialogTypesEnum },
    handleApplyFunc: () => void,
    locationSelectorObjects: { selector: MapCore.IMcLocationConditionalSelector, object: MapCore.IMcObject }[],
    standAloneItems: { item: MapCore.IMcObjectSchemeNode, itemType: number }[],
}

const initialState: ObjectWorldTreeState = {
    objectWorldTree: { key: '', id: '', label: '', nodeType: objectWorldNodeType.ROOT, nodeMcContent: '' },
    footerVersion: { name: '', code: -1, theEnum: null },
    objectsMemoryBuffer: null,
    selectedNodeInTree: { key: '', id: '', label: '', nodeType: objectWorldNodeType.ROOT, nodeMcContent: '' },
    savedObjectDataMap: new Map(),
    savedSchemeDataMap: new Map(),
    typeObjectWorldDialogSecond: undefined,
    schemesMemoryBuffer: null,
    showDialog: { hideFormReason: null, dialogType: null },
    handleApplyFunc: () => { },
    locationSelectorObjects: [],
    standAloneItems: [],
}

const objectWorldTreeReducer = (state = initialState, action: { type: string; payload: any; }) => {
    switch (action.type) {
        case SET_OBJECT_WORLD_TREE:
            return {
                ...state,
                objectWorldTree: action.payload,
            };
        case SET_FOOTER_VERSION:
            return {
                ...state,
                footerVersion: action.payload,
            };
        case SET_OBJECTS_MEMORY_BUFFER:
            return {
                ...state,
                objectsMemoryBuffer: action.payload,
            };
        case SET_SELECTED_NODE_IN_TREE:
            return {
                ...state,
                selectedNodeInTree: action.payload,
            };
        case ADD_SAVED_OBJECT_DATA_TO_MAP:
            const mcObject = action.payload[0];
            const objectSavedFileData = action.payload[1];
            state.savedObjectDataMap.set(mcObject, objectSavedFileData);
            return {
                ...state,
            };
        case ADD_SAVED_SCHEME_DATA_TO_MAP:
            const mcScheme = action.payload[0];
            const schemeSavedFileData = action.payload[1];
            state.savedSchemeDataMap.set(mcScheme, schemeSavedFileData);
            return {
                ...state,
            };
        case REMOVE_SAVED_OBJECT_FILE_PATH_FROM_MAP:
            const objectData = state.savedObjectDataMap.get(action.payload);
            const newObjectData = new SavedFileData(objectData.version, objectData.format, null)
            state.savedObjectDataMap.set(action.payload, newObjectData);
            return {
                ...state
            }
        case SET_TYPE_OBJECT_WORLD_DIALOG_SECOND:
            return {
                ...state,
                typeObjectWorldDialogSecond: action.payload,
            };
        case SET_SCHEMES_MEMORY_BUFFER:
            return {
                ...state,
                schemesMemoryBuffer: action.payload,
            };
        case SET_SHOW_DIALOG:
            return {
                ...state,
                showDialog: action.payload,
            };
        case SET_HANDLE_APPLY_FUNC:
            return {
                ...state,
                handleApplyFunc: action.payload,
            };
        case SET_LOCATION_SELECTOR_OBJECT:
            let foundedIndex = state.locationSelectorObjects.find(selectorObj => action.payload.selector === selectorObj.selector)
            if (!foundedIndex) {
                state.locationSelectorObjects.push(action.payload);
            }
            else {
                state.locationSelectorObjects = state.locationSelectorObjects.filter(selectorObj => selectorObj.selector != action.payload.selector);
                state.locationSelectorObjects.push(action.payload);
            }
            return {
                ...state,
                locationSelectorObjects: state.locationSelectorObjects,
            };
        case SET_LOCATION_SELECTOR_OBJECT_ARR:
            return {
                ...state,
                locationSelectorObjects: action.payload,
            };
    }
    return state;
};


export default objectWorldTreeReducer;



