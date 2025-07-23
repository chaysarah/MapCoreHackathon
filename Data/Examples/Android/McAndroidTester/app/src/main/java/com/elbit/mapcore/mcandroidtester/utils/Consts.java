package com.elbit.mapcore.mcandroidtester.utils;

/**
 * Created by tc99382 on 07/08/2016.
 */
public class Consts {
    public static final int TERRAINS_TAB_INDEX = 1;
    public static final int TERRAINS_COORD_SYS_TAB_INDEX = 2;
    public static final int LAYERS_TAB_INDEX = 1;
    public static final int OVERLAY_MANAGER_TAB_INDEX = 2;
    public static final int SETTINGS_TAB_INDEX = 3;

    public static final int VIEWPORT_COORDINATE_SYS = 1;
    public static final int TERRAIN_COORDINATE_SYS = 2;
    public static final int OVERLAY_MANAGER_COORDINATE_SYS = 2;

    public static final String COORD_SYS_TYPE = "coord_sys_type";

    public static final String FIRST_COLUMN = "First";
    public static final String SECOND_COLUMN = "Second";
    public static final String EMPTY_STRING = "";
    public static final String TERRAIN_ACTIVITY_VIEWPORT_PARAM = "ViewportHashcode";
    public static final String OPEN_NEW_VIEWPORT_PARAM = "OpenNewViewport";

    public class ListType {
        public static final int NON_CHECK = 1;
        public static final int SINGLE_CHECK = 2;
        public static final int MULTIPLE_CHECK = 3;

    }

    public class TextureFragmentsTags {
        public static final String TEXTURE_FROM_PROPERTIES_LIST = "textureFromPropertiesList";
        public static final String TEXTURE_FROM_CREATE_NEW_PICTURE ="textureFromCreateNewPicture";
        public static final String TEXTURE_FROM_LINE ="textureFromLine";
        public static final String TEXTURE_FROM_CLOSED_SHAPE ="textureFromClosedShape";
        public static final String TEXTURE_FROM_CLOSED_SHAPE_SIDES ="textureFromClosedShapeSides";
        public static final String TEXTURE_FROM_PICTURE ="textureFromPicture";
    }

    public class MeshFragmentsTags {
        public static final String MESH_FROM_PROPERTIES_LIST = "MeshFromPropertiesList";
        public static final String MESH_FROM_CREATE_NEW_MESH ="MeshFromCreateNewMesh";
        public static final String MESH_FROM_MESH ="MeshFromMesh";
    }

    public class TextFragmentsTags {
        public static final String TEXT_FROM_PROPERTIES_LIST = "textFromPropertiesList";
        public static final String TEXT_FROM_CREATE_NEW_TEXT ="textFromCreateNewPicture";
        public static final String TEXT_FROM_OBJECT_PROPERTIES ="textFromObjectProperties";
    }

    public class GridType {
        public static final int UTM_GRID = 101;
        public static final int GEO_GRID = 102;
        public static final int MGRS_GRID = 103;
        public static final int NZMG_GRID = 104;
    }
    public static final int OBJECT_PROPERTIES = 1;
    public static final int SCHEME_PROPERTIES = 2;

    public static final String PROPERTY_NAME_PREFIX = "name_";

    public static final int LOAD_FILE_REQUEST_CODE = 1;
    public static final int SAVE_FILE_REQUEST_CODE = 5;
    public static final int SAVE_ALL_FILE_REQUEST_CODE = 10;
    public static final int SAVE_FILE_AS_NATIVE_REQUEST_CODE = 15;
    public static final int SAVE_ALL_FILE_AS_NATIVE_REQUEST_CODE = 20;
    public static final int SAVE_OBJECTS_AS_RAW_VECTOR_REQUEST_CODE = 25;
    public static final int SAVE_ALL_OBJECTS_AS_RAW_VECTOR_REQUEST_CODE = 30;

    public static final int SAVE_OBJECTS_AS_RAW_VECTOR_FILE_REQUEST_CODE = 35;
    public static final int SAVE_ALL_OBJECTS_AS_RAW_VECTOR_FILE_REQUEST_CODE = 40;
    public static final int SAVE_OBJECTS_AS_RAW_VECTOR_MEMORY_BUFFER_REQUEST_CODE = 45;
    public static final int SAVE_ALL_OBJECTS_AS_RAW_VECTOR_MEMORY_BUFFER_REQUEST_CODE = 50;

    public static final String SCHEMES_FOLDERS_MAIN = "schemes/";
    public static final String SCHEMES_FOLDERS_SCREEN = "Screen/";
    public static final String SCHEMES_FOLDERS_WORLD = "World/";
    public static final String SCHEMES_FOLDERS_SCREEN_ATTACH_TO_WORLD = "ScreenAttachToWorld/";
    public static final String SCHEMES_FOLDERS_WORLD_ATTACH_TO_WORLD = "WorldAttachToWorld/";
    public static final String SCHEMES_FOLDERS_WORLD_WORLD = "WorldWorld/";

    public static final String SCHEMES_FILES_LINE = "default_line999_pp.mcsch";
    public static final String SCHEMES_FILES_ELLIPSE = "default_ellipse999_pp.mcsch";
    public static final String SCHEMES_FILES_TEXT = "default_text999_pp.mcsch";
    public static final String SCHEMES_FILES_ARROW = "default_arrow999_pp.mcsch";
    public static final String SCHEMES_FILES_ARC = "default_arc999_pp.mcsch";
    public static final String SCHEMES_FILES_POLYGON = "default_polygon999_pp.mcsch";
    public static final String SCHEMES_FILES_PICTURE = "default_pic999_pp.mcsch";
    public static final String SCHEMES_FILES_LINE_EXPANTION = "default_lineExp999_pp.mcsch";
    public static final String SCHEMES_FILES_RECTANGLE = "default_rect999_pp.mcsch";
    public static final String SCHEMES_FILES_MESH = "default_mesh999_pp.mcsch";

    public static final int SELECT_VIEWPORT_ACTION_SAVE_AS_NATIVE = 0;
    public static final int SELECT_VIEWPORT_ACTION_GET_SUB_ITEM_VISIBILITY = 1;
    public static final int SELECT_VIEWPORT_ACTION_SET_SUB_ITEM_VISIBILITY = 2;

}
