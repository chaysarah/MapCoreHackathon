import * as actionTypes from "../actionTypes";
import TreeNodeModel from '../../components/shared/models/map-tree-node.model'
import SecondDialogModel from "../../components/shared/models/second-dialog.model";
import { QueryResult, SpatialQueryName } from "../../components/dialog/treeView/mapWorldTree/viewportForms/spatialQueries/spatialQueriesFooter";

const setMapWorldTree = (tree: TreeNodeModel) => {
    return {
        type: actionTypes.SET_MAP_WORLD_TREE,
        payload: tree,
    };
};
const setTypeMapWorldDialogSecond = (typeDialog: SecondDialogModel) => {
    return {
        type: actionTypes.SET_TYPE_MAP_WORLD_DIALOG_SECOND,
        payload: typeDialog
    };
};
const setSelectedNodeInTree = (currentNode: TreeNodeModel) => {
    return {
        type: actionTypes.SET_SELECTED_NODE_IN_MAP_WORLD_TREE,
        payload: currentNode,
    };
};
const addQueryResultsTableRow = (queryResult: QueryResult) => {
    return {
        type: actionTypes.ADD_QUERY_RESULTS_TABLE_ROW,
        payload: queryResult,
    };
};
const resetQueryResultsTableRow = () => {
    return {
        type: actionTypes.RESET_QUERY_RESULTS_TABLE_ROW,
        payload: [],
    };
};
const setHandleApplyFunc = (handleApplyFunc: () => void) => {
    return {
        type: actionTypes.SET_HANDLE_APPLY_FUNC,
        payload: handleApplyFunc
    };
};
const setSpatialQueriesResultsObjects = (spatialQueriesResultsObjects: { queryName: SpatialQueryName, objects: any[], removeObjectsCB: (objects: any[]) => void }) => {
    return {
        type: actionTypes.SET_SPATIAL_QUERIES_RESULTS_OBJECTS,
        payload: spatialQueriesResultsObjects
    };
};

export {
    setMapWorldTree,
    setTypeMapWorldDialogSecond,
    setSelectedNodeInTree,
    setHandleApplyFunc,
    addQueryResultsTableRow,
    setSpatialQueriesResultsObjects,
    resetQueryResultsTableRow,
};
