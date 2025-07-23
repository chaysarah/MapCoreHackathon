import { enums } from "mapcore-lib";
import { ADD_QUERY_RESULTS_TABLE_ROW, RESET_QUERY_RESULTS_TABLE_ROW, SET_HANDLE_APPLY_FUNC, SET_MAP_WORLD_TREE, SET_SELECTED_NODE_IN_MAP_WORLD_TREE, SET_SPATIAL_QUERIES_RESULTS_OBJECTS, SET_TYPE_MAP_WORLD_DIALOG_SECOND } from "../actionTypes";
import { AppState } from "../combineReducer";
import TreeNodeModel, { mapWorldNodeType } from '../../components/shared/models/map-tree-node.model'
import objectWorldTreeService from "../../services/objectWorldTree.service";
import _ from "lodash"
import { DialogTypesEnum } from "../../tools/enum/enums";
import SecondDialogModel from "../../components/shared/models/second-dialog.model";
import { QueryResult, SpatialQueryName } from "../../components/dialog/treeView/mapWorldTree/viewportForms/spatialQueries/spatialQueriesFooter";

export interface MapWorldTreeState {
    mapWorldTree: TreeNodeModel;
    selectedNodeInTree: TreeNodeModel;
    typeMapWorldDialogSecond: SecondDialogModel,
    handleApplyFunc: () => void,
    queryResultsTable: QueryResult[];
    spatialQueriesResultsObjects: { queryName: SpatialQueryName, objects: any[], removeObjectsCB: (objects:any[]) => void },
}
const initialState: MapWorldTreeState = {
    mapWorldTree: { key: '', id: '', label: '', nodeType: mapWorldNodeType.ROOT, nodeMcContent: '' },
    selectedNodeInTree: { key: '', id: '', label: '', nodeType: mapWorldNodeType.ROOT, nodeMcContent: '' },
    typeMapWorldDialogSecond: undefined,
    handleApplyFunc: () => { },
    queryResultsTable: [],
    spatialQueriesResultsObjects: { queryName: null, objects: [], removeObjectsCB: ([]) => { } },
}

const objectWorldTreeReducer = (state = initialState, action: { type: string; payload: any; }) => {
    switch (action.type) {
        case SET_MAP_WORLD_TREE:
            return {
                ...state,
                mapWorldTree: action.payload,
            };
        case SET_TYPE_MAP_WORLD_DIALOG_SECOND:
            return {
                ...state,
                typeMapWorldDialogSecond: action.payload,
            };
        case SET_SELECTED_NODE_IN_MAP_WORLD_TREE:
            return {
                ...state,
                selectedNodeInTree: action.payload,
            };
        case SET_HANDLE_APPLY_FUNC:
            return {
                ...state,
                handleApplyFunc: action.payload,
            };
        case ADD_QUERY_RESULTS_TABLE_ROW:
            action.payload.index = state.queryResultsTable.length + 1
            return {
                ...state,
                queryResultsTable: [...state.queryResultsTable, action.payload],
            };
        case RESET_QUERY_RESULTS_TABLE_ROW:
            return {
                ...state,
                queryResultsTable: [],
            };
        case SET_SPATIAL_QUERIES_RESULTS_OBJECTS:
            return {
                ...state,
                spatialQueriesResultsObjects: action.payload,
            };
    }
    return state;
};


export default objectWorldTreeReducer;



