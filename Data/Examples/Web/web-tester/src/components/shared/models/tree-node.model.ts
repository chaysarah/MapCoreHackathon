export enum objectWorldNodeType {
    ROOT = 'Root', OVERLAY_MANAGER = 'Overlay Manager', OVERLAY = 'Overlay'
    , OBJECT_SCHEME = 'Object Scheme', OBJECT_LOCATION = 'Object Location', OBJECT = 'Object', ITEM = 'Item', CONDITIONAL_SELECTOR = 'Conditional Selector'
};
export enum hideFormReasons {
    CHOOSE_POINT = 'Choose Point', CHOOSE_POLYGON = 'Choose Polygon', PAINT_RECTANGLE = 'Paint Rectangle', PAINT_LINE = 'Paint Line',
};
//sort of enum 
//to all there is GetID() function in mapcore but: Root and Overlay Manager

export default interface ObjectTreeNode {
    key: string;
    id: string;
    label: string;
    nodeType: objectWorldNodeType;
    nodeMcContent: any;
    children?: ObjectTreeNode[];
    contextMenu?: string[]; // in orit's tester calls: HandlerPanelType
}

// export interface ItemNode { // merge with sara's definition of holding items (as SchemeNode ? )
//     treeNode: TreeNode;
//     item: MapCore.IMcSymbolicItem;
// }

export const treeNodeActions = {
    //MenuToolBar Actions
    SHOW_NODE_POINTS: 'Show node points',
    BASE_POINTS: 'Base points',
    CALCULATED_POINTS: 'Calculated points',
    CALCULATED_AND_ADDED_POINTS: 'Calculated and added points',
    OFF: 'Off',
    //Shared Actions
    EXPAND: 'Expand',
    COLLAPSE: 'Collapse',
    // RENAME: 'Rename',
    //Root Actions
    FIND_OVERLAY_MANAGER: 'Find Overlay Manager',
    FIND_OBJECT: 'Find Object',
    FIND_ITEM: 'Find Item',
    //Overlay Manager Actions
    CREATE_CONDITIONAL_SELECTOR: 'Create Conditional Selector',
    CREATE_OVERLAY: 'Create Overlay',
    //Overlay Actions
    ADD_OBJECT: 'New Object Based On Existing Scheme',
    REMOVE_OVERLAY: 'Remove Overlay',
    REMOVE_ALL_OBJECTS: 'Remove All Objects',
    SET_ACTIVE: 'Set Active',
    //Object Actions
    CLONE: 'Clone',
    REMOVE_OBJECT: 'Remove Object',
    MOVE_TO_OTHER_OVERLAY: 'Move To Other Overlay',
    REPLACE_SCHEME: 'Replace Scheme',
    MOVE_TO_LOCATION: 'Move To Location',
    JUMP_TO_SCHEME: 'Jump To Scheme',
    SAVE: 'Save',
    SAVE_AS: 'Save As',
    STOP_SAVE: 'Stop Save',
    //Object Location Actions
    REMOVE_OBJECT_LOCATION: 'Remove object location',
}
export const conditionalSelectorActions = {
    CONDITIONAL_SELECTOR: 'Conditional Selector',
    BLOCKED_CONDITIONAL_SELECTOR: 'Blocked Conditional Selector',
    BOOLEAN_CONDITIONAL_SELECTOR: 'Boolean Conditional Selector',
    SCALE_CONDITIONAL_SELECTOR: 'Scale Conditional Selector',
    OBJECT_STATE_CONDITIONAL_SELECTOR: 'Object State Conditional Selector',
    VIEWPORT_CONDITIONAL_SELECTOR: 'Viewport Conditional Selector',
    LOCATION_CONDITIONAL_SELECTOR: 'Location Conditional Selector',

}
