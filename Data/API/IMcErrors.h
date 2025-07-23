#pragma once

//===========================================================================
/// \file IMcErrors.h
/// Error definitions
//===========================================================================

#include "McExports.h"
#include "McBasicTypes.h"

//===========================================================================
// Interface Name: IMcErrors
//---------------------------------------------------------------------------
/// Error definitions
//===========================================================================

class IMcErrors
{
public:

    /// MapCore error codes
	enum ECode
    {
		/// \name General Errors
		//@{

        SUCCESS = 1,						///< No Error
	    FAILURE = -2100,					///< Unspecified error
		NOT_IMPLEMENTED,					///< The method will be implemented in future versions
		INVALID_PARAMETERS,					///< Invalid input parameters
		INVALID_ARGUMENT,					///< An invalid argument is passed to the method
		NOT_INITIALIZED,					///< not initialized
		CANNOT_ALLOC_BUFFER,				///< Error in buffer allocation
		GDI_FAILURE,						///< A GDI Error occurred
		FILE_NOT_FOUND,						///< File not found
		RESOURCE_FILE_NOT_LOADED,			///< Resource file cannot be loaded
		RESOURCE_NOT_FOUND,					///< Resource not found
		FONT_ATLAS_FAILURE,					///< Font atlas failure
		COORDINATE_SYSTEMS_MISMATCH,		///< Coordinate systems don't match 
		COORDINATES_NOT_CONVERTED,			///< Can't convert coordinate to the required unit
		COORDINATE_NOT_IN_AREA,				///< Coordinate is not in area
		RENDERING_DEVICE_LOST,				///< Direct3D device lost
		CONFIGURATION_FILE_NOT_FOUND,		///< Some configuration files are not found in the working directory
		RESOURCE_LOCATION_NOT_FOUND,		///< Some resource locations defined in 'resources.cfg' file 
											///<  (usually the whole 'Media' directory) are not found
		ILLEGAL_COORDINATE,					///< Coordinate is illegal
		CANT_READ_FILE,						///< Cannot read file because of I/O error or invalid format
		PREV_ASYNC_OPERATION_NOT_FINISHED,	///< An asynchronous operation cannot be started before the previous one of the same type is finished
		ASYNC_OPERATION_CANCELED,			///< An asynchronous operation was canceled by the user

		//@}

		/// \name Production Errors
		//@{

		PRODUCTION_OUT_OF_MEMORY = -2000,	///< Out of memory
		PRODUCTION_CANT_EMPTY_DEST_DIR ,	///< Cannot empty destination directory
		PRODUCTION_CANT_CREATE_DEST_DIR,	///< Cannot create destination directory
		PRODUCTION_MISSING_SRC_IMAGES,		///< Missing source images in new conversion
		PRODUCTION_CANT_READ_FILE,			///< Cannot read file
		PRODUCTION_CANT_WRITE_FILE,			///< Cannot write file
		PRODUCTION_CANT_READ_IMAGE_FILE,	///< Cannot read image file
		PRODUCTION_CANT_RESIZE_IMAGE_FILE,	///< Cannot resize image file
		PRODUCTION_CANT_READ_DTM_FILE,		///< Cannot read DTM file
		PRODUCTION_CANT_ADD_IMAGE_FILE,		///< Cannot add image file
		PRODUCTION_CANT_ADD_DTM_FILE,		///< Cannot add DTM file
		PRODUCTION_SRC_FILES_NOT_FOUND,		///< No relevant source files found
		PRODUCTION_CANT_GET_TERRAIN_RES,	///< Cannot determine terrain highest resolution
		PRODUCTION_DIFFERENT_TILE_SIZE,		///< Tile size required doesn't match the present one
		PRODUCTION_DIFFERENT_TEX_MARGIN,	///< Tile texture margin required doesn't match the present one
		PRODUCTION_CANT_GET_IMAGE_SIZE,		///< Cannot determine image size
		PRODUCTION_INVALID_TEX_RESOLUTION,	///< Invalid texture resolution
		PRODUCTION_INVALID_DTM_RESOLUTION,	///< Invalid DTM resolution
		PRODUCTION_NO_TERRAIN_IN_DEST_DIR,	///< Terrain to be updated isn't found in the destination directory
		PRODUCTION_CANT_READ_TILES_FILE,	///< Cannot read tiles file
		PRODUCTION_INVALID_SRC_FILE_PARAMS, ///< Invalid or not matching source file parameters
		PRODUCTION_MORE_THAN_ONE_IMAGE_FILE, ///< Cannot convert more than one image file without geo-referencing
		PRODUCTION_FILE_FORMAT_NOT_SUPPORTED,		///< File format is not supported
		PRODUCTION_CANT_PROCESS_STATIC_OBJECTS,		///< Cannot process static objects (X-file meshes and fences)
		PRODUCTION_STATIC_OBJECTS_NO_DEST_FILES,	///< First destination file index is out of range (no more files to update)
		PRODUCTION_INCOMPATIBLE_RECOVERY_DATA,		///< Existing recovery data is incompatible with the current conversion
		PRODUCTION_CANT_WRITE_RECOVERY_DATA,		///< Cannot write recovery data
		PRODUCTION_CANT_READ_RECOVERY_DATA,			///< Cannot read recovery data
		
		//@}

      	/// \name Collection Errors
		//@{

		OBJECT_EXISTS_IN_COLLECTION = -1900,	///< The object already exists in the collection
		OVERLAY_EXISTS_IN_COLLECTION,			///< The overlay already exists in the collection
		OBJECT_NOT_FOUND_IN_COLLECTION,			///< The object is not found in the collection
		OVERLAY_NOT_FOUND_IN_COLLECTION,		///< The overlay is not found in the collection
		
		//@}

		/// \name Overlay Manager Errors
		//@{

		NOT_THE_SAME_OVERLAY_MANAGER = -1800,	///< Different overlay managers in either viewports sharing the same vector layer, 
												///<  items, schemes, objects, overlays or collections
		//@}

		/// \name Overlay Errors
		//@{

		NO_OVERLAY = -1700,							///< The Overlay does not exist
		OVERLAY_ALREADY_REMOVED,					///< Cannot perform the operation on overlay 
													///<   already removed from overlay manager
		OBJECTS_NOT_FOUND,							///< Save operation failed- no objects to save as vector layer
		
		//@}

		/// \name Object and Property Errors
		//@{

		PROPERTY_DOES_NOT_EXIST_IN_TABLE = -1600,			///< The request property doesn't exist in the property table
		CANT_SET_RESERVED_PROPERTY_ID,					///< Can't set reserved property ID
		PROPERTY_TYPE_MISMATCH,							///< The property that uses this ID is of different type
														///<   or its value is forbidden for private properties
		RELATIVE_TO_DTM_CANNOT_BE_USED,					///< Only world-space base points can be 
														///< relative to DTM
		ID_ALREADY_EXISTS,								///< The ID already exists
		ID_NOT_FOUND,									///< The ID is not found
		NAME_NOT_FOUND,									///< The name is not found
		NO_OVERLAY_MANAGER,								///< The object doesn't have overlay manager
		CONDITIONAL_SELECTOR_DOES_NOT_EXIST,					///< The requested conditional selector doesn't exist
		CANNOT_SET_CONDITIONAL_SELECTOR,				///< Can't set conditional selector
		PRODUCING_NODES_CONNECTION_LOOP,				///< Producing nodes connection loop: 
														///<   trying to connect a node to another node
														///<   depending on it.
		CANNOT_CONVERT_VERTEX,							///< Vertex cannot be converted from one 
														///<   coordinate system to another one
		THE_PROPERTY_CANT_BE_SET_PER_VIEWPORT,			///< Some properties must be identical in all viewports, their special 
														///<  states cannot be set per viewport or used in state modifiers
		OBJECT_STATE_CONDITIONAL_SELECTOR_CANT_BE_USED,	///< The object-state conditional selector cannot be used here
		SYMBOLOGY_SUPPORT_INIT_FAILED,					///< Can't initialize a symbology standard's support (problems with the symbology standard's files)
		SYMBOLOGY_SYMBOL_ID_NOT_FOUND,					///< The specified symbol ID is not found in the symbology standard's model
		SYMBOLOGY_SYMBOL_ID_INVALID_UPDATE,				///< The part of the symbology standard's symbol ID identifying the symbol type can't be updated
		OBJECT_WAS_NOT_CREATED_VIA_SYMBOLOGY,			///< The object was not created by symbology standard's API
		SYMBOLOGY_SCHEME_FILE_NOT_FOUND,				///< The object scheme file required by the symbol ID is not found
		SYMBOLOGY_INVALID_NUM_POINTS,					///< The number of anchor points or object points is out of range of the symbology standard's model
		SYMBOLOGY_CANT_CALCULATE_POINTS,				///< Can't calculate the object points from the symbology standard's anchor points or vice versa
		SYMBOLOGY_INVALID_MODEL_INDICES,				///< The point indices defined in the symbology standard's model are invalid
		SYMBOLOGY_INVALID_MODEL_PARAMETERS,				///< Some parameters in the symbology standard's model are invalid
		SYMBOLOGY_INVALID_SCHEME_PARAMETERS,			///< Parameters of the object scheme (of the symbol ID in the symbology standard's model) are invalid
		SYMBOLOGY_AMPLIFIER_NOT_FOUND,					///< The specified amplifier name is not found for the symbol ID in the symbology standard's model

		//@}

		/// \name Object Scheme Node and Storage Errors
		//@{

		ITEM_DOESNT_EXIST = -1500,						///< The item doesn't exist
		ITEM_CANT_BE_NULL,								///< The item can't be null
		ITEM_CANT_CONNECT_CONNECTED_ITEM,				///< Can't connect already connected item
		ITEM_CANT_CONNECT_PHYSICAL_TO_SYMBOLIC,			///< Can't connect a physical item to a symbolic item
		ITEM_CANT_CONNECT_PHYSICAL_TO_SCREEN_LOCATION,	///< Can't connect a physical item to a screen location
		ITEM_NOT_CONNECTED_CANT_SET_PROP_ID,			///< Can't set property ID in item not connected to scheme
		WRITE_TO_STORAGE_FAILED,						///< Error in write to storage
		READ_FROM_STORAGE_FAILED,						///< Error in read from storage
		INVALID_STORAGE_FORMAT,							///< Storage format doesn't match the required type (object or object scheme)
		WRONG_STORAGE_FORMAT,							///< Wrong storage format (object scheme instead of object)
		STORAGE_VERSION_MISMATCH,						///< Storage version is newer than current MapCore version
		CONDITIONAL_SELECTOR_STORAGE_ERROR,				///< Error in reading conditional selectors from storage
		FONT_STORAGE_ERROR,								///< Error in reading fonts from storage
		TEXTURE_STORAGE_ERROR,							///< Error in reading textures from storage
		MESH_STORAGE_ERROR,								///< Error in reading meshes from storage
		IMAGE_CALC_STORAGE_ERROR,						///< Error in reading image-calc. from storage
		NOT_COMPATIBLE_ATTACH_POINT_PARENT,				///< The item's parent is not compatible
														///<  with the attach point type
		INVALID_SUB_TYPE_FLAGS,							///< Invalid combination of sub-type flags
		ITEM_SHOULD_BE_WORLD_AND_ATTACHED_TO_TERRAIN,	///< Geo-referenced-texture picture's sub-type should include world and attached-to-terrain flags
		ITEM_SHOULD_BE_ATTACHED_TO_TERRAIN,				///< Slope-presentation Item's sub-type should include attached-to-terrain flag
		INVALID_ORDER_OF_SLOPE_COLORS,					///< Slope colors should be in ascending order of maximal slopes
		INVALID_TEXTURE_RESOLUTION,						///< Texture resolution should be positive
		INVALID_POINT_INDICES_AND_DUPLICATES,			///< Point indices and duplicates should be an array of pairs
		SUB_ITEMS_NOT_SUPPORTED,						///< Sub-items are not supported for this item type
		INVALID_ORDER_OF_SUB_ITEMS,						///< Sub-items should be in ascending order of start indices
		INVALID_ITEM_DRAW_PRIORITY,						///< Item draw priority should not be equal to -128
		DYNAMIC_MESH_CANT_DISPLAY_ITEMS_ATTACHED_TO_TERRAIN, ///< Dynamic mesh can't display items attached to terrain
		LICENSED_FONT_CANT_BE_EMBEDDED,					///< The font is licensed, it can't be embedded

		//@}

		/// \name Local Cache Errors
		//@{

		LOCAL_CACHE_NOT_INIT = -1400,					///< Map layers local cache has not been initialized in device
		LOCAL_CACHE_HAS_ACTIVE_LAYER,					///< There is an active local cache of some map layer, no remove is allowed

		//@}

		/// \name Edit Mode Errors
		//@{

		EDIT_MODE_UTILITY_ITEM_SHOULD_BE_SCREEN = -1300, 
											///< EditMode utility item should be 
											///<   of screen subtype and (for rectangle) 
											///<   based on screen coordinate system
		EDIT_MODE_IMAGE_CALC_MISMATCH,		///< Items based on locations in image coordinate system 
											///<   can be initialized/edited only in image maps
											///<   with the same IMcImageCalc
		EDIT_MODE_IS_ALREADY_ACTIVE,		///< EditMode is already active
		EDIT_MODE_IS_NOT_ACTIVE,			///< EditMode is not active
		EDIT_MODE_IS_NOT_CONNECTED,			///< EditMode was not initialized properly
		EDIT_MODE_AUTO_REFRESH_IS_NOT_ACTIVE, ///< Auto refresh ability was not activated
		EDIT_MODE_ITEM_IS_NOT_SUPPORTED,	///< The item is not supported by EditMode

		//@}

		/// \name Terrain Errors
		//@{

		TERRAIN_ALREADY_EXISTS = -1200,				///< The viewport already has this terrain, 
													///<   it cannot be added twice
		TERRAIN_NOT_FOUND,							///< The specified terrain is not found
		LAYER_ALREADY_EXISTS,						///< The terrain already has this layer, 
													///<   it cannot be added twice
		LAYER_NOT_FOUND,							///< The specified layer is not found
		LAYER_TILING_SCHEME_MISMATCH,				///< The layer tiling schemes don't match
		DTM_LAYER_ALREADY_EXISTS,					///< The terrain already has a DTM layer, 
													///<   another one cannot be added
		DTM_LAYER_DOES_NOT_EXIST,					///< The terrain does not have this DTM layer
		DTM_LAYER_CANT_BE_REMOVED,					///< A DTM layer cannot be removed from a terrain used in a viewport
		VIEWPORT_CANT_HAVE_EMPTY_TERRAIN,			///< A viewport cannot have empty terrain (without layers)
		DTM_LAYER_CANT_BE_ADDED,					///< A DTM layer cannot be added to a terrain used in a viewport
		NATIVE_SERVER_LAYER_NOT_VALID,				///< MapCore's Map Layer Server notifies on layer version update or layer not found
		LAYER_WEB_REQUEST_FAILURE,					///< Map layer Web request failure
		SYNC_OPERATION_ON_NATIVE_SERVER_LAYER,		///< This operation on native-server layer can't be synchronous
		RAW_3D_EXTRUSION_LAYER_DTM_MISMATCH,		///< Raw vector 3D-extrusion layer should be used with the same non-NULL synchronous DTM layer
		NATIVE_SERVER_LAYER_AUTHENTICATION_REQUIRED,///< An authentication is required by MapCore's Map Layer Server, a token should be provided
		NATIVE_SERVER_LAYER_UNAUTHENTICATED,		///< MapCore's Map Layer Server cannot authenticate the client (the token provided is not valid)
		NATIVE_SERVER_LAYER_AUTHENTICATION_EXPIRED,	///< MapCore's Map Layer Server notifies on expired authentication, a new token should be provided
		NATIVE_SERVER_LAYER_UNAUTHORIZED,			///< MapCore's Map Layer Server notifies the client is not authorized to use the requested service
		LAYER_INDEXING_FAILURE,						///< Map layer indexing failure
		SYNC_OPERATION_WITH_SERVER_LAYER,			///< This operation depending on server layer can't be synchronous
		OPERATION_WITH_NON_NATIVE_SERVER_LAYER,		///< This operation depending on non-native server layer can't be performed

		//@}
 
		/// \name Viewport Errors
		//@{

		VIEWPORT_MAP_TYPE_MISMATCH = -1100,	///< The viewport type in not compatible with the
											///<	function used
		CANNOT_DESTROY_ACTIVE_CAMERA,		///< Active camera cannot be destroyed
		VIEWPORT_SIZE_MISSING,				///< Viewport width/height cannot be zero for 
											///<	non-window viewport (without handle)
		VIEWPORT_INVALID_WINDOW_HANDLE,		///< Window handle is invalid in Web version: it is not HTMLCanvasElement

		//@}

		/// \name Spatial Queries Errors
		//@{

		QUERY_DTM_NOT_FOUND = -1000,		///< No DTM found in the query area
		TOO_MANY_TARGETS,					///< The number of targets found exceeded the user-specified maximum
		ASYNC_QUERY_WITH_CURRENT_LOD,		///< A query in levels of detail currently shown in the viewport can't be asynchronous
		SYNC_QUERY_WITH_NON_CURRENT_LOD,	///< A query with server-based layers in level of detail other than currently shown 
											///<	in the viewport can't be synchronous

		//@}

		/// \name ImageCalc Errors
		//@{

		IC_OUT_OF_LIMIT		= -900,			///< Out of image limit
		IC_OUT_OF_WORKING_AREA,				///< Out of working area
		IC_TOO_MANY_OPEN_IMAGE_CALCS,		///< Too many open image calculations
		IC_IMAGE_CALCS_NOT_OPENED,			///< Image calculations are not opened
		IC_INVALID_IMAGE_ID,				///< Invalid image ID
		IC_INPUT_ERR, 						///< Input data for the function incorrect
		IC_IMPORT_ERR, 						///< Error during the import from parameters
		IC_RELEASE_ERR, 					///< Error during release sensor
		IC_LOAD_ERR, 						///< Error during loading sensor
		IC_UNLOAD_ERR, 						///< Error during unloading sensor
		IC_XML_ERR, 						///< Error creating xml data
		IC_ALLOCATION_ERR,					///< Allocation Error
		IC_CS_ERR, 							///< Error input coordinate system (see CoordSysType for options)
		IC_G2I_ERR, 						///< Error in Ground To image function
		IC_I2G_ERR, 						///< Error in Image To Ground
		IC_I2LOS_ERR, 						///< Error in Image To Los computation
		IC_LOS2G_ERR,						///< Error in Los To Image computation
		IC_IMAGE_ADJUSTMENT_ERR, 			///< Error while trying to adjust an image
		IC_BLOCK_ADJUSTMENT_ERR, 			///< Error while trying to adjust block
		IC_GET_ERR,							///< Error int get function
		IC_SET_ERR,							///< Error int set function
		IC_READ_ERR,	     				///< Error reading file from disk
		IC_WRITE_ERR,						///< Error writing file to disk
		IC_NO_SUPPORT,						///< Errors from calculations module	
		IC_BAD_DTM,							///< Bad DTM

		//@}

		/// \name Calculations Errors
		//@{

		CROSSING_POLYGONS		= -800,		///< Polygons intersect
		CANT_INIT_GEOD_MAGNETIC,			///< Cannot initialize geoid/magnetic calculations (e.g. appropriate files not found) 

		//@}

		/// \name Map Layer Errors
		//@{

		MAPLAYER_FILE_WITHOUT_COORDINATES	= -700,	///< Trying to load raw file without geo-referencing
		NOT_SUPPORTED_FOR_THIS_LAYER,				///< Feature not supported for this layer's type or format version
		LOCALE_NOT_FOUND,							///< Locale not found, bad locale string
		ACTIVE_VECTOR_LAYER_NOT_FOUND,				///< Vector layer data-source could not be opened
		VECTOR_3D_EXTRUSION_LAYER_NO_CONTOURS,		///< No contour is found for vector-3D-extrusion map layer
		VECTOR_3D_EXTRUSION_LAYER_NO_DTM_HEIGHTS,	///< No DTM height is found for any contour of vector-3D-extrusion map layer
		LAYER_INDEX_SOURCE_MISMATCH,				///< Layer's source doesn't match the one used to build its indexing data
		MAPLAYER_FILE_WITHOUT_COORDINATE_SYSTEM,	///< No coordinate system is found for raw/source data, 
													///<	in such case source coordinate system must be explicitly specified
		//@}

		/// \name Image Processing/Correlation Errors
		//@{

		HISTOGRAM_NOT_CALCULATED	= -600,		///< Histogram has not been calculated
		INVALID_GUESS,							///< Invalid initial guess

		//@}

		/// \name License Manager Errors
		//@{

		LISENCE_IS_INVALID = -500,			///< License is invalid for the current computer
		LICENSE_EXPIRED,					///< License has been expired
		LICENSE_BAD_FORMAT,					///< The license file is corrupted or does not includes license information
		LICENSE_FEATURE_NOT_FOUND,			///< Feature was not found for this license
		LICENSE_FILE_NOT_FOUND				///< License file is not found

		//@}

	};

	//==============================================================================
	// Method Name: ErrorCodeToString()
	//------------------------------------------------------------------------------
	/// Retrieves an error description according to error code
	///
	/// \param[in]	eErrorCode			The error code, see #ECode
	/// \param[out]	pstrErrorMessage	The error string buffer (valid until a next call to this function)
	//==============================================================================
	static void COMMONUTILS_API ErrorCodeToString(ECode eErrorCode, PCSTR *pstrErrorMessage);

	//==============================================================================
	// Method Name: GetLastStorageErrorDetailedString()
	//------------------------------------------------------------------------------
	/// Retrieves an error returned by a last call to one of save/load storage functions 
	/// in a form of detailed error string.
	///
	/// The string can contain detailed error information including file names etc.
	///
	/// \param[out]   pstrErrorString    The error string buffer (valid until a next call to one of save/load storage functions)
	//==============================================================================
	static void COMMONUTILS_API GetLastStorageErrorDetailedString(PCSTR *pstrErrorString);
};

#define MC_CHECK_ERROR(x) \
    { \
	IMcErrors::ECode IMC_CHECK_ERROR_TMP = (IMcErrors::ECode)x; \
        if (IMC_CHECK_ERROR_TMP != IMcErrors::SUCCESS) \
        { \
            return IMC_CHECK_ERROR_TMP; \
        } \
    }

#define MC_CHECK_NULL(x,r) \
    { \
        if ((x) == NULL) \
        { \
            return (r); \
        } \
    }


#define MC_THROW_IF_ERROR(x,e) \
    { \
        if ((x) != IMcErrors::SUCCESS) \
        { \
            throw e; \
        } \
    }
