export enum mapWorldNodeType {
    ROOT = 'Root', MAP_VIEWPORT = 'Map Viewport', MAP_TERRAIN = 'Map Terrain'
    , MAP_CAMERA = 'Map Camera', MAP_GRID = 'Map Grid', MAP_HEIGHT_LINES = 'Map Height Lines', MAP_LAYER = 'Map Layer'
};

export default interface MapTreeNode {
    key: string;
    id: string;
    label: string;
    nodeType: mapWorldNodeType;
    nodeMcContent: any;
    children?: MapTreeNode[];
    contextMenu?: string[]; // in orit's tester calls: HandlerPanelType
}

export const treeNodeActions = {
    //Shared Actions
    EXPAND:  { 
        label: 'Expand'
    },
    COLLAPSE:  { 
        label: 'Collapse'
    },
    RENAME: {
        label: 'Rename',
    },
    DELETE_NAME: {
        label: 'Delete Name',
    },
    //Root Actions
    ROOT: {
        label: mapWorldNodeType.ROOT,
        actions: {
            VIEWPORT: { 
                label: 'Viewport',
                actions: {
                    NEW_GRID: 'New Grid',
                    NEW_MAP_HEIGHT_LINES: 'New Map Height Lines',
                    LOAD_SESSION_FROM_FOLDER: 'Load Session From Folder',
                    LOAD_SESSION_FORM_FOLDER_WITHOUT_VIEWPORT_SIZE: 'Load Session From Folder Without Viewport Size',
                    ADD_ALL_OPEN_MAP_FORMS_TO_SCHEMES: 'Add All Open Map Forms To Schemes',
                }
            },
            TERRAIN: { 
                label: 'Terrain',
                actions: {
                    LOAD_FROM_A_FILE: 'Load from a File',
                    LOAD_FROM_A_BUFFER: 'Load from a Buffer',
                    CREATE_NEW: 'Create New'
                }
            },
            LAYER: { 
                label: 'Layer',
                actions: {
                    LOAD_FROM_A_FILE: 'Load from a File',
                    LOAD_FROM_A_BUFFER: 'Load from a Buffer',
                    CREATE_NEW: 'Create New',
                    BACKGROUNG_THREAD_INDEX: 'Backgroung Thread Index',
                }
            },
            SERVER_LAYERS: { 
                label: 'Server Layers',
                actions: {
                    CHECK_ALL_NATIVE_SERVER_LAYERS_VALIDITY_ASYNC: 'Check All Native Server Layers Validity Async',
                    CHECK_VALIDITY_BEFORE_EACH_RENDER: 'Check Validity Before Each Render',
                }
            },
            RAW_3D_MODEL: { 
                label: 'Raw 3D Model',
                actions: {
                    BUILD_INDEXING_DATA_FOR_RAW_3D_MODEL: 'Build Indexing Data For Raw 3D Model',
                    DELETE_INDEXING_DATA_FOR_RAW_3D_MODEL: 'Delete Indexing Data For Raw 3D Model',
                    SMART_REALITY_JUMP_TO_BUILDING: 'Smart Reality Jump To Building',
                }
            },
            RAW_VECTOR_3D_EXTRUSION: { 
                label: 'Raw Vector 3D Extrusion',
                actions: {
                    BUILD_NDEXING_DATA_FOR_RAW_VECTOR_3D_EXTRUSION: 'Build Indexing Data For Raw Vector 3D Extrusion',
                    DELETE_INDEXING_DATA_FOR_RAW_VECTOR_3D_EXTRUSION: 'Delete Indexing Data For Raw Vector 3D Extrusion',
                }
            },
        }
    },
    // viewport
    MAP_VIEWPORT: {
        label: mapWorldNodeType.MAP_VIEWPORT,
        actions: {
            CREATE_CAMERA: {
                label: 'Create Camera',
            },
            ADD_TERRAIN: {
                label: 'Add Terrain',
            },

            GRID: { 
                label: 'Grid',
                actions: {
                    SET_GRID: 'Set Grid',
                    REMOVE_GRID: 'Remove Grid',
                }
            },
            HEIGHT_LINES: { 
                label: 'Height Lines',
                actions: {
                    SET_HEIGHT_LINES: 'Set Height Lines',
                    REMOVE_HEIGHT_LINES: 'Remove Height Lines',
                }
            },
            SAVE_SESSION_TO_FOLDER: {
                label: 'Save Session To Folder',
            },
            ADD_MAP_FORM_TO_SCHEMES: {
                label: 'Add Map Form To Schemes',
            },
        }
    },
    // terrain
    MAP_TERRAIN: {
        label: mapWorldNodeType.MAP_TERRAIN,
        actions: {
            LAYERS: { 
                label: 'Layers',
                actions: {
                    ADD_LAYER: 'Add Layer',
                    REMOVE_LAYER: 'Remove Layer',
                    ADD_AND_REMOVE_VECTOR_LAYERS: 'Add and Remove Vector Layers',
                }
            },
            SAVE_TERRAIN: { 
                label: 'Save Terrain',
                actions: {
                    SAVE_TO_A_FILE: 'Save to a File',
                    SAVE_TO_A_BUFFER: 'Save to a Buffer',
                }
            },
            REMOVE_TERRAIN: {
                label: 'Remove Terrain',
            },
        }
    },
    // camera
    MAP_CAMERA: {
        label: mapWorldNodeType.MAP_CAMERA,
        actions: {
            DESTROY_CAMERA: {
                label: 'Destroy Camera',
            },
            SET_ACTIVE: {
                label: 'Set Active',
            },
        }
    },
    // layer
    MAP_LAYER: {
        label: mapWorldNodeType.MAP_LAYER,
        actions: {
            SAVE_LAYER: { 
                label: 'Save Layer',
                actions: {
                    SAVE_TO_A_FILE: 'Save to a File',
                    SAVE_TO_A_BUFFER: 'Save to a Buffer',
                }
            },
            REMOVE_LAYER: {
                label: 'Remove Layer',
            },
            MOVE_TO_CENTER: {
                label: 'Move To Center',
            },
        }
    },
    // grid
    MAP_GRID: {    
        label: mapWorldNodeType.MAP_GRID,
        actions: {
            DELETE:  {
                label: 'Delete',
            }
        }
    },
    // height lines
    MAP_HEIGHT_LINES: {
        label: mapWorldNodeType.MAP_HEIGHT_LINES,
        actions: {
            DELETE:  {
                label: 'Delete',
            }
        }
    },
}
