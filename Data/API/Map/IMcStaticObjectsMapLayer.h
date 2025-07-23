#pragma once

//===========================================================================
/// \file IMcStaticObjectsMapLayer.h
/// Interfaces for static-objects map layers
//===========================================================================

#include "McExports.h"
#include "McCommonTypes.h"
#include "SMcColor.h"
#include "Map/IMcVectorMapLayer.h"

class IMcMapViewport;
//===========================================================================
// Interface Name: IMcStaticObjectsMapLayer
//---------------------------------------------------------------------------
/// The base interface for static-objects layers
//===========================================================================
class IMcStaticObjectsMapLayer : virtual public IMcMapLayer
{
protected:
    virtual ~IMcStaticObjectsMapLayer() {}

public:

	/// Object ID along with its user-defined color
	struct SObjectColor
	{
		SMcVariantID	uObjectID;	///< Object's ID
		SMcBColor		Color;		///< object's color

		/// \n
		SObjectColor() {}
		/// \n
		SObjectColor(const SMcVariantID& _uObjectID, SMcBColor _Color) : uObjectID(_uObjectID), Color(_Color) {}
	};

/// \name Object Colors
//@{
	//==============================================================================
	// Method Name: SetObjectsColors()
	//------------------------------------------------------------------------------
	/// Sets user-defined colors of the specified objects.
	/// 
	/// The default color of an object is white opaque.
	///
	/// \param[in]	aObjectsColors		array of objects IDs along with their colors
	/// \param[in]	uNumObjectsColors	number of objects in the array
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectsColors(const SObjectColor aObjectsColors[], UINT uNumObjectsColors) = 0;

	//==============================================================================
	// Method Name: SetsObjectsColor()
	//------------------------------------------------------------------------------
	/// Sets one user-defined color of the specified objects.
	/// 
	/// The default color of an object is white opaque.
	///
	/// \param[in]	Color			color to set
	/// \param[in]	auObjectsIDs	array of objects IDs to update
	/// \param[in]	uNumObjectsIDs	number of objects in the array
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectsColor(const SMcBColor& Color, const SMcVariantID auObjectsIDs[], UINT uNumObjectsIDs) = 0;

	//==============================================================================
	// Method Name: RemoveAllObjectsColors()
	//------------------------------------------------------------------------------
	/// Remove user-defined colors of all the objects by restoring the defaults (white opaque).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RemoveAllObjectsColors() = 0;

	//==============================================================================
	// Method Name: GetObjectsColors()
	//------------------------------------------------------------------------------
	/// Retrieves user-defined colors of all the objects.
	///
	/// \param[out]	paObjectsColors		array of objects IDs along with their colors
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectsColors(CMcDataArray<SObjectColor>* paObjectsColors) const = 0;

	//==============================================================================
	// Method Name: GetObjectColor()
	//------------------------------------------------------------------------------
	/// Retrieves user-defined colors of the specified object.
	///
	/// \param[in]	uObjectID		ID of object to retrieve
	/// \param[out]	pColor			color of the object
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectColor(const SMcVariantID& uObjectID, SMcBColor* pColor) const = 0;
//@}

/// \name Layer Parameters
//@{
	//==============================================================================
	// Method Name: SetDisplayingItemsAttachedToTerrain()
	//------------------------------------------------------------------------------
	/// Sets the option of displaying object scheme items attached to terrain on top of static-objects 
	/// for all viewports or for one specific viewport.
	///
	/// \param[in]	bDisplaysItemsAttachedToTerrain	Whether or not object scheme items attached to terrain and 
	///												lite vector layer objects are displayed on top of static-objects
	/// \param[in]	pMapViewport					The viewport to set the option for or NULL for all viewports
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetDisplayingItemsAttachedToTerrain(bool bDisplaysItemsAttachedToTerrain, 
		IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: SetDisplayingDtmVisualization()
	//------------------------------------------------------------------------------
	/// Sets the option for DTM visualization to consider static-objects and to be displayed on top of them
	/// for all viewports or for one specific viewport.
	///
	/// \param[in]	bDisplaysDtmVisualization		Whether or not DTM visualization considers static-objects and 
	///												is displayed on top of them
	/// \param[in]	pMapViewport					The viewport to set the option for or NULL for all viewports
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetDisplayingDtmVisualization(bool bDisplaysDtmVisualization, 
		IMcMapViewport *pMapViewport = NULL) = 0;
//@}
};

//===========================================================================
// Interface Name: IMc3DModelMapLayer
//---------------------------------------------------------------------------
/// The interface for 3D model layer
//===========================================================================
class IMc3DModelMapLayer : virtual public IMcStaticObjectsMapLayer
{
protected:
    virtual ~IMc3DModelMapLayer() {}

public:

/// \name Layer Parameters
//@{
	//==============================================================================
	// Method Name: SetResolutionFactor()
	//------------------------------------------------------------------------------
	/// Sets the factor affecting the selection of resolutions displayed for all viewports or for one specific viewport.
	///
	/// \param[in]	fResolutionFactor	The factor affecting the selection of the resolution (of the layer's textures and geometry) to be displayed in 
	///									each area according to the area's local scale; 1 (the default) means the resolution intended for the scale, 
	///									values greater than 1 mean lower (coarser) resolutions, values smaller than 1 mean higher (finer) resolutions 
	///									(the minimal allowed value is 0.1)
	/// \param[in]	pMapViewport		The viewport to set the parameter for or NULL for all viewports
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetResolutionFactor(float fResolutionFactor, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: SetResolvingConflictsWithDtmAndRaster()
	//------------------------------------------------------------------------------
	/// Sets the option of resolving conflicts between this layer and DTM/raster layers (by hiding DTM/raster layers in areas covered by this layer) 
	/// for all viewports or for one specific viewport.
	///
	/// \param[in]	bResolvesConflictsWithDtmAndRaster		Whether or not DTM/raster layers should be hidden in areas covered by this layer
	/// \param[in]	pMapViewport							The viewport to set the option for or NULL for all viewports
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetResolvingConflictsWithDtmAndRaster(bool bResolvesConflictsWithDtmAndRaster, 
		IMcMapViewport *pMapViewport = NULL) = 0;
//@}
};

//===========================================================================
// Interface Name: IMcNative3DModelMapLayer
//---------------------------------------------------------------------------
/// The interface for 3D model layer in MapCore format (converted) 
//===========================================================================
class IMcNative3DModelMapLayer : virtual public IMc3DModelMapLayer
{
protected:
    virtual ~IMcNative3DModelMapLayer() {}

public:


    enum
    {
        //================================================================
        /// Map layer unique ID for this interface
        //================================================================
        LAYER_TYPE = 41
    };

/// \name Create
//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native 3D model layer
	///
	/// \param[out]	ppLayer						native 3D model layer created
	/// \param[in]	strDirectory				a directory containing the layer's files
	/// \param[in]	uNumLevelsToIgnore			number of highest levels of detail to ignore (can be used
	///											to check the layer without highest levels of detail); 
	///											the default is 0 (ignore nothing)
	/// \param[in]  pReadCallback				Callback receiving read layer events
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcNative3DModelMapLayer **ppLayer, PCSTR strDirectory, UINT uNumLevelsToIgnore = 0, 
		IReadCallback *pReadCallback = NULL);

	//==============================================================================
	// Method Name: GetCreateParams()
	//------------------------------------------------------------------------------
	/// Retrieves paramters set in Create()
	///
	/// \param[out]	puNumLevelsToIgnore		number of highest levels of detail to ignore (can be used
	///										to check the layer without highest levels of detail); 
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCreateParams(UINT *puNumLevelsToIgnore) const = 0;
//@}
};

//===========================================================================
// Interface Name: IMcNativeServer3DModelMapLayer
//---------------------------------------------------------------------------
/// The interface for 3D model layer used to display data from MapCore's Map Layer Server
//===========================================================================
class IMcNativeServer3DModelMapLayer : virtual public IMc3DModelMapLayer
{
protected:
    virtual ~IMcNativeServer3DModelMapLayer() {}

public:
    enum
    {
        //================================================================
        /// Map layer unique ID for this interface
        //================================================================
        LAYER_TYPE = 43
    };

/// \name Create
//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native-server 3D model layer
	///
	/// \param[out]	ppLayer				native-server 3D model layer created
	/// \param[in]	strLayerURL			layer's URL in the server
	/// \param[in]  pReadCallback		Callback receiving read layer events
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcNativeServer3DModelMapLayer **ppLayer, PCSTR strLayerURL, IReadCallback *pReadCallback = NULL);
//@}
};

//===========================================================================
// Interface Name: IMcRaw3DModelMapLayer
//---------------------------------------------------------------------------
/// The interface for raw 3D model layer (unconverted) 
//===========================================================================
class IMcRaw3DModelMapLayer : virtual public IMc3DModelMapLayer
{
protected:
	virtual ~IMcRaw3DModelMapLayer() {}

public:
	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 42
	};
	/// \name Create
	//@{
	

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates raw 3D model layer that was not previously indexed by BuildIndexingData()
	///
	/// \param[out]	ppLayer						The raw 3D model layer created.
	/// \param[in]	strRawDataDirectory			The directory containing the raw data.
	/// \param[in]	pTargetCoordinateSystem		The optional layer's target coordinate system; `NULL` (means the same as the layer's source 
	///											coordinate system defined by the layer's files) cannot be used for raw data in 3DTiles format.
	/// \param[in]	bOrthometricHeights			Whether the heights are orthometric (above the geoid / sea level) or ellipsoid heights 
	///											(above input's coordinate system's elipsoid); the default is `false`
	/// \param[in]	pClipRect					The clipping rectangle (x and y only are relevant), in target's coordinate system;
	///											the default is NULL (no clipping will be done).
	/// \param[in]	fTargetHighestResolution	The target highest resolution to limit that of the source (or zero to use that of the source); 
	///											the default is `0.05` (means 5 cm per pixel)
	/// \param[in]	pReadCallback				The optional callback receiving read layer events; the default is NULL.
	/// \param[in]  aRequestParams				The optional array of additional parameters (as key-value pairs) to be added to each HTTP request as "key=value"
	/// \param[in]  uNumRequestParams;          The number of parameters in the above array
	/// \param[in]	PositionOffset				Optional offset to apply to the model (in **pTargetCoordinateSystem**)
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcRaw3DModelMapLayer **ppLayer, PCSTR strRawDataDirectory, 
		IMcGridCoordinateSystem *pTargetCoordinateSystem, bool bOrthometricHeights = false,
	    const SMcBox *pClipRect = NULL, float fTargetHighestResolution = 0.05, IReadCallback *pReadCallback = NULL,
		const SMcKeyStringValue aRequestParams[] = NULL, UINT uNumRequestParams = 0, const SMcVector3D& PositionOffset = v3Zero);

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates raw 3D model layer previously indexed by BuildIndexingData() (the indexing data must exist)
	///
	/// \param[out]	ppLayer						The raw 3D model layer created.
	/// \param[in]	strRawDataDirectory			The directory containing the raw data.
	/// \param[in]	bOrthometricHeights			Whether the heights are orthometric (above the geoid / sea level) or ellipsoid heights 
	///											(above input's coordinate system's elipsoid); the default is `false`
	/// \param[in]	uNumLevelsToIgnore			The number of highest levels of detail to ignore (can be used to check the layer
	///											without highest levels of detail); the default is 0 (ignore nothing).
	/// \param[in]	pReadCallback				The optional callback receiving read layer events; the default is NULL.
	/// \param[in]  strIndexingDataDirectory	The directory containing the the indexing data previously built by BuildIndexingData(); 
	///											`NULL` or empty string indicates default indexing data directory (based on \p strRawDataDirectory).
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcRaw3DModelMapLayer **ppLayer, PCSTR strRawDataDirectory, bool bOrthometricHeights = false, 
		UINT uNumLevelsToIgnore = 0, IReadCallback *pReadCallback = NULL,
		PCSTR strIndexingDataDirectory = NULL);

	//==============================================================================
	// Method Name: GetCreateParams()
	//------------------------------------------------------------------------------
	/// Retrieves paramters set in Create()
	///
	/// \param[out] pstrRawDataDirectory				The directory containing the raw data.
	/// \param[out]	pbOrthometricHeights				Whether the heights are orthometric (above the geoid / sea level) or ellipsoid heights 
	///													(above input's coordinate system's elipsoid);
	/// \param[out]	puNumLevelsToIgnore					The number of highest levels of detail to ignore (can be used
	///													to check the layer without highest levels of detail); 
	/// \param[out]	ppTargetCoordinateSystem			The optional layer's target coordinate system;
	///													(`NULL` means the same as the layer's source coordinate system defined by the layer's files).
	/// \param[out]	ppClipRect							The clipping rectangle (x and y only are relevant), in target's coordinate system;
	///													('NULL' means no clipping is done).
	/// \param[out] pfTargetHighestResolution			The target highest resolution; the value was rounded to the nearest valid resolution of the 
	///													tiling scheme; (`FLT_MAX` means source highest resolution).
	/// \param[out]	pstrIndexingDataDirectory			The directory containing the the indexing data.
	/// \param[out] pbNonDefaultIndexingDataDirectory	Whether or not indexing data directory is non-default.
	/// \param[out]	paRequestParams						list of additional parameters (as key-value pairs) to be added to each HTTP request as "key=value"
	/// \param[out] pPositionOffset						The vector that offsets model position (in x,y,z)  directions
	/// 
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCreateParams(
		PCSTR *pstrRawDataDirectory, bool *pbOrthometricHeights, UINT *puNumLevelsToIgnore,
		IMcGridCoordinateSystem **ppTargetCoordinateSystem, const SMcBox **ppClipRect,
		float *pfTargetHighestResolution,
		PCSTR *pstrIndexingDataDirectory, bool *pbNonDefaultIndexingDataDirectory,
		CMcDataArray<SMcKeyStringValue> *paRequestParams,
		SMcVector3D* pPositionOffset = NULL) const = 0;
	//@}

	/// \name Indexing Raw Data
	//@{
	//==============================================================================
	// Method Name: BuildIndexingData()
	//------------------------------------------------------------------------------
	/// Builds the indexing for the raw data (required for the layer creation).
	///
	/// Use with caution: the function deletes the whole contents of the indexing directory before building the indexing.
	///
	/// \param[in]	strRawDataDirectory			The directory containing the raw data.
	/// \param[in]	pTargetCoordinateSystem		The optional layer's target coordinate system; `NULL` (means the same as the layer's source 
	///											coordinate system defined by the layer's files) cannot be used for raw data in 3DTiles format.
	/// \param[in]	pClipRect					The clipping rectangle (x and y only are relevant), in target's coordinate system;
	///											the default is NULL (no clipping will be done).
	/// \param[in]	pTilingScheme				The tiling scheme to use; the default is NULL (means MapCore scheme).
	/// \param[in]	fTargetHighestResolution	The target highest resolution; the value will be rounded to the nearest valid resolution of the 
	///											tiling scheme (\p pTilingScheme); the default is `0.05` (means approximatly 5 cm per pixel).
	/// \param[in]  bUseExisting				True (default) means do nothing if \p strIndexingDataDirectory already exists with the indexing data 
	///											built with the same parameters.
	/// \param[in]	strIndexingDataDirectory	The directory containing the indexing data to be built.
	///											NULL or empty string indicates default indexing data directory (based on strRawDataDirectory).

	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode BuildIndexingData(PCSTR strRawDataDirectory, 
		IMcGridCoordinateSystem *pTargetCoordinateSystem, const SMcBox *pClipRect = NULL, 
		const IMcMapLayer::STilingScheme *pTilingScheme = NULL, float fTargetHighestResolution = 0.05,
		bool bUseExisting = true, PCSTR strIndexingDataDirectory = NULL);

	//==============================================================================
	// Method Name: DeleteIndexingData()
	//------------------------------------------------------------------------------
	/// Deletes indexing data from the disk.
	/// 
	/// Use with caution: the function deletes the whole contents of the indexing directory.
	///
	/// \param[in]	strRawDataDirectory			The directory containing the raw data; if `NULL` or empty string, \p strIndexingDataDirectory must be specified.
	/// \param[in]	strIndexingDataDirectory	The directory containing the indexing data previously built; 
	///											`NULL` or empty string indicates default indexing data directory (based on \p strRawDataDirectory).
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode DeleteIndexingData(PCSTR strRawDataDirectory,
		PCSTR strIndexingDataDirectory = NULL);
	//@}
};

//===========================================================================
// Interface Name: IMcVector3DExtrusionMapLayer
//---------------------------------------------------------------------------
/// The interface for static-objects layer built by extruding contours from a vector data source
//===========================================================================
class IMcVector3DExtrusionMapLayer : virtual public IMcVectorBasedMapLayer, virtual public IMcStaticObjectsMapLayer
{
protected:
	virtual ~IMcVector3DExtrusionMapLayer() {}

public:
	/// Object ID along with its user-defined extrusion height
	struct SObjectExtrusionHeight
	{
		SMcVariantID	uObjectID;	///< Object's ID
		float			fHeight;	///< object's extrusion height; `FLT_MAX` means object's original height

		/// \n
		SObjectExtrusionHeight() {}
		/// \n
		SObjectExtrusionHeight(const SMcVariantID &_uObjectID, float _fHeight) : uObjectID(_uObjectID), fHeight(_fHeight) {}
	};

/// \name Object ID
//@{
	//==============================================================================
	// Method Name: GetObjectIDBitCount()
	//------------------------------------------------------------------------------
	/// Retrieves object ID bit count
	///
	/// \param[out]	puObjectIDBitCount		object ID bit count (the possible values are 32, 64, 128)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectIDBitCount(UINT *puObjectIDBitCount) const = 0;
//@}

/// \name Object Extrusion Heights
//@{
	//==============================================================================
	// Method Name: IsExtrusionHeightChangeSupported()
	//------------------------------------------------------------------------------
	/// Retrieves whether the layer supports changing extrusion heights
	///
	/// \param[out]	pbExtrusionHeightChangeSupported	whether the layer supports changing extrusion heights
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode IsExtrusionHeightChangeSupported(bool *pbExtrusionHeightChangeSupported) const = 0;

	//==============================================================================
	// Method Name: SetObjectExtrusionHeight()
	//------------------------------------------------------------------------------
	/// Sets one user-defined extrusion height of the specified objects.
	/// 
	/// \param[in]	uObjectID		ID of object to update
	/// \param[in]	fHeight			extrusion height to set
	///								(if a required height is higher than the original one by more than \p fExtrusionHeightMaxAddition specified in 
	///								IMcNativeStaticObjectsMapLayer::Create(), the actual height used will be the original one plus 
	///								\p fExtrusionHeightMaxAddition);
	///								`FLT_MAX` means to remove user-defined height restoring the default.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectExtrusionHeight(const SMcVariantID &uObjectID, float fHeight) = 0;

	//==============================================================================
	// Method Name: SetObjectsExtrusionHeights()
	//------------------------------------------------------------------------------
	/// Sets user-defined extrusion heights of the specified objects.
	/// 
	/// \param[in]	aObjectsHeights		array of objects IDs along with their extrusion heights
		///								(if a required height is higher than the original one by more than \p fExtrusionHeightMaxAddition specified in 
		///								IMcNativeStaticObjectsMapLayer::Create(), the actual height used will be the original one plus 
		///								\p fExtrusionHeightMaxAddition);
	/// \param[in]	uNumObjectsHeights	number of objects in the array
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectsExtrusionHeights(const SObjectExtrusionHeight aObjectsHeights[], UINT uNumObjectsHeights) = 0;

	//==============================================================================
	// Method Name: RemoveAllObjectsExtrusionHeights()
	//------------------------------------------------------------------------------
	/// Remove user-defined extrusion heights of all the objects by restoring the defaults.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RemoveAllObjectsExtrusionHeights() = 0;

	//==============================================================================
	// Method Name: GetObjectExtrusionHeight()
	//------------------------------------------------------------------------------
	/// Retrieves user-defined heights of the specified object.
	///
	/// \param[in]	uObjectID			ID of object to retrieve
	/// \param[out]	pfHeight			extrusion height of the object; `FLT_MAX` means object's original height
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectExtrusionHeight(const SMcVariantID &uObjectID, float *pfHeight) const = 0;

	//==============================================================================
	// Method Name: GetObjectsExtrusionHeights()
	//------------------------------------------------------------------------------
	/// Retrieves user-defined extrusion heights of all the objects.
	///
	/// \param[out]	paObjectsHeights	array of objects IDs along with their extrusion heights
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectsExtrusionHeights(CMcDataArray<SObjectExtrusionHeight> *paObjectsHeights) const = 0;
//@}
};

//===========================================================================
// Interface Name: IMcNativeVector3DExtrusionMapLayer
//---------------------------------------------------------------------------
/// The interface for static-objects layer built by extruding contours from a vector data source in MapCore format (converted) 
//===========================================================================
class IMcNativeVector3DExtrusionMapLayer : virtual public IMcVector3DExtrusionMapLayer
{
protected:
    virtual ~IMcNativeVector3DExtrusionMapLayer() {}

public:


    enum
    {
        //================================================================
        /// Map layer unique ID for this interface
        //================================================================
        LAYER_TYPE = 71
    };

/// \name Create
//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native vector extrusion layer
	///
	/// \param[out]	ppLayer						native vector extrusion layer created
	/// \param[in]	strDirectory				a directory containing the layer's files
	/// \param[in]	uNumLevelsToIgnore			number of highest levels of detail to ignore (can be used
	///											to check the layer without highest levels of detail); 
	///											the default is 0 (ignore nothing)
	/// \param[in]  fExtrusionHeightMaxAddition	the maximal addition to objects' extrusion heights (if a required height in SetObjectExtrusionHeight() 
	///											and SetObjectsExtrusionHeights() will be higher than the original one by more than \p fExtrusionHeightMaxAddition, 
	///											the actual height used will be the original one plus \p fExtrusionHeightMaxAddition); the default is 0
	/// \param[in]  pReadCallback				Callback receiving read layer events
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcNativeVector3DExtrusionMapLayer **ppLayer, PCSTR strDirectory, UINT uNumLevelsToIgnore = 0, 
		float fExtrusionHeightMaxAddition = 0, IReadCallback *pReadCallback = NULL);

	//==============================================================================
	// Method Name: GetCreateParams()
	//------------------------------------------------------------------------------
	/// Retrieves paramters set in Create()
	///
	/// \param[out]	puNumLevelsToIgnore				number of highest levels of detail to ignore (can be used
	///												to check the layer without highest levels of detail); 
	/// \param[out] pfExtrusionHeightMaxAddition	the maximal addition to objects' extrusion heights
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCreateParams(
		UINT *puNumLevelsToIgnore, float *pfExtrusionHeightMaxAddition) const = 0;

//@}
};

//===========================================================================
// Interface Name: IMcNativeServerVector3DExtrusionMapLayer
//---------------------------------------------------------------------------
/// The interface for vector extrusion layer built by extruding contours from a vector data source used to display data from MapCore's Map Layer Server
//===========================================================================
class IMcNativeServerVector3DExtrusionMapLayer : virtual public IMcVector3DExtrusionMapLayer
{
protected:
    virtual ~IMcNativeServerVector3DExtrusionMapLayer() {}

public:
    enum
    {
        //================================================================
        /// Map layer unique ID for this interface
        //================================================================
        LAYER_TYPE = 73
    };

/// \name Create
//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native-server vector extrusion layer
	///
	/// \param[out]	ppLayer				native-server vector extrusion layer created
	/// \param[in]	strLayerURL			layer's URL in the server
	/// \param[in]  pReadCallback		Callback receiving read layer events
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcNativeServerVector3DExtrusionMapLayer **ppLayer, PCSTR strLayerURL, IReadCallback *pReadCallback = NULL);
//@}
};

//===========================================================================
// Interface Name: IMcRawVector3DExtrusionMapLayer
//---------------------------------------------------------------------------
/// The interface for raw vector extrusion layer built by extruding contours from a vector data source (unconverted) 
//===========================================================================
class IMcRawVector3DExtrusionMapLayer : virtual public IMcVector3DExtrusionMapLayer
{
protected:
	virtual ~IMcRawVector3DExtrusionMapLayer() {}

public:
	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 72
	};

    /// Flags for texture placement along one axis
	enum ETexturePlacementFlags
    {
        /// none; without other flags means repeating whole texture along roof/sides starting at 
		///  - top-left corner of side face/faces for side texture
		///  - north-west corner of roof bounding rectangle for roof texture
		ETPF_NONE				= 0x0000,

		/// beyond texture edge: edge pixels are repeated instead of the whole texture
		ETPF_REPEAT_EDGE_PIXELS	= 0x0001,

		/// texture restarts at each face (irrelevant for roof texture)
        ETPF_RESTART_EACH_FACE	= 0x0002,

		/// place texture center at face/faces center (irrelevant if #ETPF_FIT is set)
        ETPF_ALIGN_CENTER		= 0x0004,

		/// texture alignment is reversed: right instead of left, bottom instead of top (irrelevant if #ETPF_FIT is set)
        ETPF_REVERSE_ALIGNMENT	= 0x0008,

		/// fit texture width/height into face/faces (cancels alignment flags)
		ETPF_FIT				= 0x0010
    };

	/// Parameters for textures of roofs and sides
	struct SExtrusionTexture
	{
		char			strTexturePath[MAX_PATH];	///< Path to texture file
		SMcFVector2D	TextureScale;				///< Texture scale; the default is (0, 0) (means (1, 1))
		UINT			uXPlacementBitField;		///< Texture placement along X axis (based on #ETexturePlacementFlags); the default is #ETPF_NONE
		UINT			uYPlacementBitField;		///< Texture placement along Y axis (based on #ETexturePlacementFlags); the default is #ETPF_NONE

		SExtrusionTexture()
		{
			strTexturePath[0] = '\0';
			TextureScale = vf2Zero;
			uXPlacementBitField = ETPF_NONE;
			uYPlacementBitField = ETPF_NONE;
		}

		SExtrusionTexture(const SExtrusionTexture &other)
		{
			memcpy(strTexturePath, other.strTexturePath, strlen(other.strTexturePath) + 1);
			TextureScale = other.TextureScale;
			uXPlacementBitField = other.uXPlacementBitField;
			uYPlacementBitField = other.uYPlacementBitField;
		}
	};

	/// Graphical parameters for vector 3D extrusion layer
	struct SGraphicalParams
	{
		SExtrusionTexture RoofDefaultTexture;				///< Default texture for roofs of objects without specific texture
		SExtrusionTexture SideDefaultTexture;				///< Default texture for sides of objects without specific texture
		SExtrusionTexture *aSpecificTextures;				///< Textures for specific objects
		UINT uNumSpecificTextures;							///< Number of textures for specific objects in \b aSpecificTextures
		char strHeightColumn[MAX_PATH];						///< name of height column in the metadata
		char strObjectIDColumn[MAX_PATH];					///< Optional name of object ID column in the metadata, if empty - vector item 
															///<  ID is used instead
		char strRoofTextureIndexColumn[MAX_PATH];			///< Optional name of column in the metadata with index of specific texture 
															///<  for roof, if empty - \b strRoofTexturePath is used instead
		char strSideTextureIndexColumn[MAX_PATH];			///< Optional name of column in the metadata with index of specific texture 
															///<  for side, if empty - \b strSideTexturePath is used instead

		SGraphicalParams()
		{
			memset(this, 0, sizeof(*this));
		}
	};

	/// Parameters for vector 3D extrusion layer
	struct SParams : public SGraphicalParams
	{
		PCSTR strDataSource;								///< Vector data source: vector file (*.shp, *.mdb, etc) or data base connection (e.g. PostGIS); 
															///<  in case of a multi-layer data source, the proper suffix should be provided, 
															///<  otherwise the first sub-layer will be taken.
		IMcGridCoordinateSystem *pSourceCoordinateSystem;	///< source files' coordinate system
		IMcGridCoordinateSystem *pTargetCoordinateSystem;	///< target's optional coordinate system if reprojection is required or NULL if not
		const IMcMapLayer::STilingScheme *pTilingScheme;	///< destination tiling scheme; default is NULL (means MapCore scheme)
		const SMcBox *pClipRect;							///< Optional clipping rectangle (x and y only are relevant), in target's coordinate system;
															///< the default is NULL (no clipping will be done).
		SParams(PCSTR _strDataSource = NULL, IMcGridCoordinateSystem *_pSourceCoordSys = NULL)
		{
			strDataSource = _strDataSource;
			pSourceCoordinateSystem = _pSourceCoordSys;
			pTargetCoordinateSystem = NULL;
			pTilingScheme = NULL;
			pClipRect = NULL;
		}
	};

	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates raw vector extrusion layer that was not previously indexed by BuildIndexingData().
	///
	/// \param[out]	ppLayer						The raw vector extrusion layer created.
	/// \param[in]	Params						The layer's main parameters.
	/// \param[in]  fExtrusionHeightMaxAddition	The maximal addition to objects' extrusion heights (if a required height in SetObjectExtrusionHeight() 
	///											and SetObjectsExtrusionHeights() will be higher than the original one by more than \p fExtrusionHeightMaxAddition, 
	///											the actual height used will be the original one plus \p fExtrusionHeightMaxAddition); the default is 0.
	/// \param[in]	pReadCallback				The optional callback receiving read layer events; the default is `NULL`.
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcRawVector3DExtrusionMapLayer **ppLayer, const SParams &Params, float fExtrusionHeightMaxAddition = 0, 
		IReadCallback *pReadCallback = NULL);

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates raw vector extrusion layer previously indexed by BuildIndexingData() (the indexing data must exist).
	///
	/// \param[out]	ppLayer						The raw vector extrusion layer created.
	/// \param[in]	strDataSource				The data source.
	/// \param[in]	Params						The layer's graphical parameters.
	/// \param[in]  fExtrusionHeightMaxAddition	The maximal addition to objects' extrusion heights (if a required height in SetObjectExtrusionHeight() 
	///											and SetObjectsExtrusionHeights() will be higher than the original one by more than \p fExtrusionHeightMaxAddition, 
	///											the actual height used will be the original one plus \p fExtrusionHeightMaxAddition); the default is 0.
	/// \param[in]	pReadCallback				The optional callback receiving read layer events; the default is `NULL`.
	/// \param[in]  strIndexingDataDirectory	The directory containing the the indexing data previously built by BuildIndexingData(); 
	///											`NULL` or empty string indicates default indexing data directory (based on \p strDataSource).
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcRawVector3DExtrusionMapLayer **ppLayer, 
		PCSTR strDataSource, const SGraphicalParams &Params, float fExtrusionHeightMaxAddition = 0,
		IReadCallback *pReadCallback = NULL, PCSTR strIndexingDataDirectory = NULL);

	//==============================================================================
	// Method Name: GetCreateParams()
	//------------------------------------------------------------------------------
	/// Retrieves paramters set in Create()
	///
	/// \param[out]	pParams								The layer's main parameters
	/// \param[out]	pfExtrusionHeightMaxAddition		The maximal addition to objects' extrusion heights (if a required height in SetObjectExtrusionHeight() 
	///													and SetObjectsExtrusionHeights() will be higher than the original one by more than 
	///													\p fExtrusionHeightMaxAddition, the actual height used will be the original one plus 
	///													\p fExtrusionHeightMaxAddition); 
	/// \param[out]	pstrIndexingDataDirectory			The directory containing the the indexing data if was specified.
	/// \param[out] pbNonDefaultIndexingDataDirectory	Whether or not indexing data directory is non-default.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCreateParams(
		SParams *pParams, float *pfExtrusionHeightMaxAddition,
		PCSTR *pstrIndexingDataDirectory, bool *pbNonDefaultIndexingDataDirectory) const = 0;

	//@}

	/// \name Indexing Raw Data
	//@{
	//==============================================================================
	// Method Name: BuildIndexingData()
	//------------------------------------------------------------------------------
	/// Builds the indexing for the raw data (optional for the layer creation).
	///
	/// Use with caution: the function deletes the whole contents of the indexing directory before building the indexing.
	///
	/// \param[in]	strDataSource				The data source.
	/// \param[in]	pSourceCoordinateSystem		The layer's source coordinate system.
	/// \param[in]	pTargetCoordinateSystem		The optional layer's target coordinate system; the default is `NULL`.
	///											(means the same as the layer's source coordinate system).
	/// \param[in]	pClipRect					The clipping rectangle (x and y only are relevant), in target's coordinate system;
	///											the default is NULL (no clipping will be done).
	/// \param[in]	pTilingScheme				The tiling scheme to use; the default is `NULL` (means MapCore scheme).
	/// \param[in]  bUseExisting				True (default) means do nothing if \p strIndexingDataDirectory already exists with the indexing data 
	///											built with the same parameters.
	/// \param[in]	strIndexingDataDirectory	The directory containing the indexing data to be built.
	///											NULL or empty string indicates default indexing data directory (based on strDataSource).

	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode BuildIndexingData(PCSTR strDataSource,
		IMcGridCoordinateSystem *pSourceCoordinateSystem, IMcGridCoordinateSystem *pTargetCoordinateSystem = NULL,
		const SMcBox *pClipRect = NULL, const IMcMapLayer::STilingScheme *pTilingScheme = NULL,
		bool bUseExisting = true, PCSTR strIndexingDataDirectory = NULL);

	//==============================================================================
	// Method Name: DeleteIndexingData()
	//------------------------------------------------------------------------------
	/// Deletes indexing data from the disk.
	/// 
	/// Use with caution: the function deletes the whole contents of the indexing directory.
	///
	/// \param[in]	strDataSource				The data source; if `NULL` or empty string, \p strIndexingDataDirectory must be specified.
	/// \param[in]	strIndexingDataDirectory	The directory containing the indexing data previously built; 
	///											`NULL` or empty string indicates default indexing data directory (based on \p strDataSource).
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode DeleteIndexingData(PCSTR strDataSource,
		PCSTR strIndexingDataDirectory = NULL);
	//@}

};
