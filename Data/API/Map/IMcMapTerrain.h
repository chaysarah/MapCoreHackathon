#pragma once

//===========================================================================
/// \file IMcMapTerrain.h
/// Interface for terrain
//===========================================================================

#include "IMcErrors.h"
#include "IMcBase.h"
#include "SMcVector.h"
#include "CMcDataArray.h"
#include "IMcMapLayer.h"

class IMcMapLayer;
class IMcDtmMapLayer;
class IMcUserData;
class IMcUserDataFactory;
class IMcGridCoordinateSystem;
class IMcMapViewport;

//=========================================================================================
// Interface Name: IMcMapTerrain
//-----------------------------------------------------------------------------------------
/// Interface for terrain (map) consisting of several map layers of different types
//=========================================================================================
class IMcMapTerrain : virtual public IMcBase
{
protected:
    virtual ~IMcMapTerrain() {}

public:

	/// Layer parameters in this terrain.
	///
	/// \note A layer can have different parameters in different terrains.
	struct SLayerParams
	{
		float		fMinScale;			///< start of the layer's visibility scale range (default is 0)
		float		fMaxScale;			///< end of the layer's visibility scale range (default is FLT_MAX)
		signed char	nDrawPriority;		///< the layer's drawing priority (default is 0)
		BYTE		byTransparency;		///< the layer's transparency (default is 255): 
										///< - byTransparency == 0 - transparent,
										///< - byTransparency == 255 - opaque,
										///< - 0 < byTransparency < 255 - semi-transparent.
		bool	bVisibility;			///< the layer's visibility (default is true)
		bool	bNearestPixelMagFilter;	///< whether raster-layer magnification filtering should be nearest-pixel (point) instead of bi-linear (anisotropic); 
										///< the default is false
		SLayerParams()
		{
			fMinScale = 0;
			fMaxScale = FLT_MAX;
			nDrawPriority = 0;
			byTransparency = 255;
			bVisibility = true;
			bNearestPixelMagFilter = false;
		}
		SLayerParams(float	_fMinScale, float _fMaxScale, signed char _nDrawPriority, 
					 BYTE _byTransparency, bool _bVisibility, bool _bNearestPixelMagFilter = false)
			: fMinScale(_fMinScale), fMaxScale(_fMaxScale), nDrawPriority(_nDrawPriority), byTransparency(_byTransparency), 
			  bVisibility(_bVisibility), bNearestPixelMagFilter(_bNearestPixelMagFilter) {}

		bool operator==(const SLayerParams &Other) const
		{
			return (fMinScale == Other.fMinScale && fMaxScale == Other.fMaxScale && nDrawPriority == Other.nDrawPriority && byTransparency == Other.byTransparency && bVisibility == Other.bVisibility && bNearestPixelMagFilter == Other.bNearestPixelMagFilter);
		}
		bool operator!=(const SLayerParams &Other) const
		{
			return (fMinScale != Other.fMinScale || fMaxScale != Other.fMaxScale || nDrawPriority != Other.nDrawPriority || byTransparency != Other.byTransparency || bVisibility != Other.bVisibility || bNearestPixelMagFilter != Other.bNearestPixelMagFilter);
		}
	};

	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates a terrain.
	///
	/// \param[out]	ppTerrain								terrain created.
	/// \param[in]	pCoordinateSystem						coordinate system to use.
	/// \param[in]	apLayers[]								list of layers to attach to the
	///														terrain.
	/// \param[in]	uNumLayers								number of layers.
	/// \param[in]  pTilingScheme							for future use, meanwhile should be `NULL` (the tiling scheme to 
	///														force on the layers; `NULL` means to take it from the first layer).
	/// \param[in]	pBoundingRect							terrain's optional bounding rectangle that can be 
	///														specified if additional layers will be added later
	///														(`NULL` means to take from the layers).
	/// \param[in] bDisplayItemsAttachedTo3DModelWithoutDtm	whether or not to display attached to terrain objects on 3DModel layers, 
	///														even without a DTM layer in the same terrain; the default is `false`
	/// \return
	///     - status result
	//==============================================================================
	static MAPTERRAIN_API IMcErrors::ECode Create(IMcMapTerrain **ppTerrain, 
		IMcGridCoordinateSystem *pCoordinateSystem, 
		IMcMapLayer *const apLayers[], UINT uNumLayers, const IMcMapLayer::STilingScheme *pTilingScheme = NULL, const SMcBox *pBoundingRect = NULL,
		bool bDisplayItemsAttachedTo3DModelWithoutDtm = false);
	//@}

	/// \name Terrain Parameters
	//@{
	//==============================================================================
	// Method Name: SetCoordinateSystem()
	//------------------------------------------------------------------------------
	/// Sets a new coordinate system.
	///
	/// \param[in]	pCoordinateSystem	coordinate system to set.
	///
	/// \return
	///     - status result
	//==============================================================================

	virtual IMcErrors::ECode SetCoordinateSystem(
		IMcGridCoordinateSystem *pCoordinateSystem) = 0; 

	//==============================================================================
	// Method Name: GetCoordinateSystem()
	//------------------------------------------------------------------------------
	/// Retrieves the coordinate system.
	///
	/// \param[out]	ppCoordinateSystem	current coordinate system.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCoordinateSystem(
		IMcGridCoordinateSystem **ppCoordinateSystem) const = 0; 

	//==============================================================================
	// Method Name: GetBoundingBox()
	//------------------------------------------------------------------------------
	/// Retrieves the bounding box.
	///
	/// \param[out]	pBoundingBox	current bounding box.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetBoundingBox(SMcBox *pBoundingBox) const = 0;
	//@}

	//==============================================================================
	// Method Name: SetVisibility()
	//------------------------------------------------------------------------------
	/// Sets the terrain's visibility in all viewports or in one specific viewport.
	///
	/// Setting the terrain's visibility in all viewports overrides its visibility in each viewport 
	/// previously set. On the other hand, it can later be changed in any specific viewport.
	///
	/// \param[in]	bVisibility		The terrain's visibility (the default is true)
	/// \param[in]	pMapViewport	The viewport to set a visibility in or NULL for all viewports
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetVisibility(bool bVisibility, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetVisibility()
	//------------------------------------------------------------------------------
	/// Retrieves the default visibility of the terrain in all viewports (set by SetVisibility(, NULL)) 
	/// or its visibility in one specific viewport.
	///
	/// \param[out]	pbVisibility	The terrain's visibility
	/// \param[in]	pMapViewport	The viewport to retrieve a visibility in or NULL for all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetVisibility(bool *pbVisibility, IMcMapViewport *pMapViewport = NULL) const = 0;

	/// \name Terrain ID and User Data
	//@{
	//==============================================================================
	// Method Name: SetID()
	//------------------------------------------------------------------------------
	/// Sets the terrain's user-defined unique ID that allows retrieving the terrain by IMcMapViewport::GetTerrainByID().
	///
	/// \param[in]	uID		The terrain's ID to be set (specify ID not equal to #MC_EMPTY_ID to set ID,
	///						equal to #MC_EMPTY_ID to remove ID).
	/// \note
	/// If the terrain is used in some viewports, the ID should be unique in every such viewport, otherwise the function returns IMcErrors::ID_ALREADY_EXISTS
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetID(UINT uID) = 0;

	//==============================================================================
	// Method Name: GetID()
	//------------------------------------------------------------------------------
	/// Retrieves the terrain'e user-defined unique ID set by SetID().
	///        
	/// \param [out] puID	The terrain ID (or #MC_EMPTY_ID if the ID is not set).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetID(UINT *puID) const = 0;

	//==============================================================================
	// Method Name: SetUserData()
	//------------------------------------------------------------------------------
	/// Sets an optional user-defined data that can be retrieved later by GetUserData().
	///
	/// \param[in]	pUserData	An instance of a user-defined class implementing IMcUserData interface.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetUserData(IMcUserData *pUserData) = 0;

	//==============================================================================
	// Method Name: GetUserData()
	//------------------------------------------------------------------------------
	/// Retrieves an optional user-defined data set by SetUserData().
	///
	/// \param[out]	ppUserData	An instance of a user-defined class implementing IMcUserData interface.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetUserData(IMcUserData **ppUserData) const = 0;

	//==============================================================================
	// Method Name: GetDisplayItemsAttachedTo3DModelWithoutDtm()
	//------------------------------------------------------------------------------
	/// Retrieves whether or not attached to terrain objects are displayed on 3DModel layers, 
	/// even without a DTM layer in the same terrain.
	///
	/// \param[out]	pbDisplayItemsAttachedTo3DModelWithoutDtm	Whether or not attached to terrain objects are displayed.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetDisplayItemsAttachedTo3DModelWithoutDtm(
		bool *pbDisplayItemsAttachedTo3DModelWithoutDtm) const = 0;
	//@}

	/// \name Layers
	//@{
	//==============================================================================
	// Method Name: AddLayer()
	//------------------------------------------------------------------------------
	/// Adds a new layer to the terrain.
	///
	/// \param[in]	pLayer	layer to add.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode AddLayer(IMcMapLayer *pLayer) = 0;

	//==============================================================================
	// Method Name: RemoveLayer()
	//------------------------------------------------------------------------------
	/// Removes a layer from the terrain.
	///
	/// \param[in]	pLayer layer to remove.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RemoveLayer(IMcMapLayer *pLayer) = 0;

	
	//==============================================================================
	// Method Name: SetLayerParams()
	//------------------------------------------------------------------------------
	/// Sets layer parameters for this terrain
	///
	/// \param[in]	pLayer		layer to set parameters
	/// \param[in]	Params		new layer parameters for this terrain
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetLayerParams(IMcMapLayer *pLayer, const SLayerParams &Params) = 0;

	//==============================================================================
	// Method Name: GetLayerParams()
	//------------------------------------------------------------------------------
	/// Retrieves layer parameters for this terrain
	///
	/// \param[in]	pLayer		layer to retrieve parameters
	/// \param[out]	ppParams	layer parameters for this terrain
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLayerParams(IMcMapLayer *pLayer, const SLayerParams **ppParams) const = 0;

	//==============================================================================
	// Method Name: GetLayerByID()
	//------------------------------------------------------------------------------
	/// Retrieve a layer by ID.
	///
	/// \param[in]	uID		The ID of the layer to retrieve.
	/// \param[out]	ppLayer	The retrieved layer.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLayerByID(UINT uID, IMcMapLayer **ppLayer) const = 0;

	//==============================================================================
	// Method Name: GetLayers()
	//------------------------------------------------------------------------------
	/// Retrieves the layers.
	///
	/// \param[out]	papLayers		current layers array.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLayers(CMcDataArray<IMcMapLayer*> *papLayers) const = 0;

	//==============================================================================
	// Method Name: GetDtmLayer()
	//------------------------------------------------------------------------------
	/// Retrieves the DTM layer.
	///
	/// \param[out]	ppDtmLayer	retrieved DTM layer.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetDtmLayer(IMcDtmMapLayer **ppDtmLayer) const = 0;
	//@}

	/// \name Save & Load
	//@{

    //================================================================
    // Method Name: Save(...)
    //----------------------------------------------------------------
	/// Saves the terrain definition to a file
	///
    /// \param[in]	strFileName			The name of a file to save to
	/// \param[in]	strBaseDirectory	The optional path of a base directory (can be NULL)
	/// \param[in]	bSaveUserData		Whether user data should be saved
	///
	/// \note
	/// - non-NULL base directory means to save all files/directories in the terrain definition 
	///   with paths relative to the base directory
	/// - NULL base directory means to save all files/directories with absolute paths
	///
    /// \return
    ///     - Status result
    //================================================================
	virtual IMcErrors::ECode Save(PCSTR strFileName, PCSTR strBaseDirectory = NULL, 
								  bool bSaveUserData = false) = 0;

    //================================================================
    // Method Name: Save(...)
    //----------------------------------------------------------------
	/// Saves the terrain definition to a memory buffer. 
	///
    /// \param[out]	pabyMemoryBuffer	The memory buffer filled by the function
    /// \param[in]	strBaseDirectory	The optional path of a base directory (can be NULL)
	/// \param[in]	bSaveUserData		Whether user data should be saved
	///
	/// \note
	/// - non-NULL base directory means to save all files/directories in the terrain definition 
	///   with paths relative to the base directory
	/// - NULL base directory means to save all files/directories with absolute paths
	///
    /// \return
    ///     - Status result
    //================================================================
	virtual IMcErrors::ECode Save(CMcDataArray<BYTE> *pabyMemoryBuffer, 
								  PCSTR strBaseDirectory = NULL, bool bSaveUserData = false) = 0;

    //================================================================
    // Method Name: Load(...)
    //----------------------------------------------------------------
	/// Loads a terrain definition previously saved by Save() from a file. 
	///
    /// \param[out]	ppTerrain				The loaded terrain
    /// \param[in]	strFileName				The name of the file to load from
    /// \param[in]	strBaseDirectory		The optional path of a base directory
	///										(NULL means current working directory)
	/// \param[in]	pUserDataFactory		The optional user-defined factory that creates user data instances
	/// \param[in]	pReadCallbackFactory	The optional user-defined factory that creates callback during each layer creation
	///
	/// \note
	/// - If the terrain definition was saved without a base directory (with absolute paths of 
	///   files/directories), these paths will remain the same after loading and \a strBaseDirectory
	///	  will not be used.
	/// - If the terrain definition was saved with a base directory (with paths of files/directories 
	///   relative to it), these paths will be relative to \a strBaseDirectory after loading.
	///
    /// \return
    ///     - Status result
    //================================================================
	static MAPTERRAIN_API IMcErrors::ECode Load(IMcMapTerrain **ppTerrain, PCSTR strFileName, 
		PCSTR strBaseDirectory = NULL, IMcUserDataFactory *pUserDataFactory = NULL,
		IMcMapLayer::IReadCallbackFactory* pReadCallbackFactory = NULL);

    //================================================================
    // Method Name: Load(...)
    //----------------------------------------------------------------
	/// Loads a terrain definition previously saved by Save() from a memory buffer. 
	///
    /// \param[out]	ppTerrain				The loaded terrain
    /// \param[in]	abMemoryBuffer			The memory buffer to load from
    /// \param[in]	uBufferSize				The memory buffer size
    /// \param[in]	strBaseDirectory		The optional path of a base directory
	///										(NULL means current working directory)
	/// \param[in]	pUserDataFactory		The optional user-defined factory that creates user data instances
	/// \param[in]	pReadCallbackFactory	The optional user-defined factory that creates callback during each layer creation
	///
	/// \note
	/// - If the terrain definition was saved without a base directory (with absolute paths of 
	///   files/directories), these paths will remain the same after loading and \a strBaseDirectory
	///	  will not be used.
	/// - If the terrain definition was saved with a base directory (with paths of files/directories 
	///   relative to it), these paths will be relative to \a strBaseDirectory after loading.
	///
    /// \return
    ///     - Status result
    //================================================================
	static MAPTERRAIN_API IMcErrors::ECode Load(IMcMapTerrain **ppTerrain, const BYTE abMemoryBuffer[], 
		UINT uBufferSize, PCSTR strBaseDirectory = NULL, IMcUserDataFactory *pUserDataFactory = NULL,
		IMcMapLayer::IReadCallbackFactory* pReadCallbackFactory = NULL);
	//@}
};
