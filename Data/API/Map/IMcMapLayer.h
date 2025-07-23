#pragma once

//===========================================================================
/// \file IMcMapLayer.h
/// Interfaces for map layers
//===========================================================================

#include <float.h>
#include <stdlib.h>
#include "McExports.h"
#include "IMcErrors.h"
#include "IMcBase.h"
#include "SMcVector.h"
#include "CMcDataArray.h"
#include "SMcSizePointRect.h"
#include "SMcColor.h"
#include "McCommonTypes.h"
#include "CMcTime.h"
#include "OverlayManager/IMcTexture.h"
#include "CMcDataArray.h"

class IMcDtmMapLayer;
class IMcNativeDtmMapLayer;
class IMcNativeServerDtmMapLayer;
class IMcRawDtmMapLayer;
class IMcRasterMapLayer;
class IMcNativeRasterMapLayer;
class IMcNativeServerRasterMapLayer;
class IMcRawRasterMapLayer;
class IMcWebServiceDtmMapLayer;
class IMcWebServiceRasterMapLayer;
class IMcVectorBasedMapLayer;
class IMcVectorMapLayer;
class IMcNativeVectorMapLayer;
class IMcNativeServerVectorMapLayer;
class IMcRawVectorMapLayer;
class IMcMapGrid;
class IMcHeatMapLayer;
class IMcNativeHeatMapLayer;
class IMcCodeMapLayer;
class IMcMaterialMapLayer;
class IMcRawMaterialMapLayer;
class IMcNativeMaterialMapLayer;
class IMcNativeServerMaterialMapLayer;
class IMcTraversabilityMapLayer;
class IMcRawTraversabilityMapLayer;
class IMcNativeTraversabilityMapLayer;
class IMcNativeServerTraversabilityMapLayer;
class IMcStaticObjectsMapLayer;
class IMc3DModelMapLayer;
class IMcNative3DModelMapLayer;
class IMcNativeServer3DModelMapLayer;
class IMcRaw3DModelMapLayer;
class IMcVector3DExtrusionMapLayer;
class IMcNativeVector3DExtrusionMapLayer;
class IMcNativeServerVector3DExtrusionMapLayer;
class IMcRawVector3DExtrusionMapLayer;

class IMcUserData;
class IMcUserDataFactory;
class IMcGridCoordinateSystem;
class IMcMapViewport;
class IMcMapTerrain;

//===========================================================================
// Interface Name: IMcMapLayer
//---------------------------------------------------------------------------
/// Base interface for map layers of different types
//===========================================================================
class IMcMapLayer : virtual public IMcBase
{
protected:
    virtual ~IMcMapLayer() {}

public:

	/// Layer kind
	enum ELayerKind : BYTE
	{
		// Note: for backwards compatibility (having bool bRaster), enum must be BYTE size and first 2 values must be: dtm=0,raster=1

		ELK_DTM = 0,				///< DTM (elevation data)
		ELK_RASTER = 1,				///< Raster
		ELK_VECTOR = 2,				///< Vector
		ELK_HEAT_MAP = 3,			///< Heat Map
		ELK_STATIC_OBJECTS = 4,		///< Static Objects
		ELK_CODE_MAP = 5			///< Material & Traversability
	};

	/// Raw component type, used to define a raw map layer's component in IMcMapLayer::SComponentParams
	enum EComponentType
	{
		ECT_FILE,						///< the component is a single file with a name specified
		ECT_DIRECTORY					///< the component is a directory with a name specified
	};

	/// Returned type of vector layer's metadata field
	enum EVectorFieldReturnedType
	{
		EVFRT_INT,					///< 32bit integer (int)
		EVFRT_DOUBLE,				///< Double precision floating point (double)
		EVFRT_STRING,				///< String of ASCII characters (CMcString)
		EVFRT_WSTRING				///< String of Unicode characters (CMcWString)
	};

	/// Standard types of tiling schemes used in raw and native map layers
	enum ETilingSchemeType
	{
		/// MapCore's classic scheme; desined for both metric and geographic projections
		ETST_MAPCORE,

		/// The whole world is too largest tiles (256 pixels for raster); desined for geographic projection
		ETST_GLOBAL_LOGICAL,

		/// The whole world (extended in y to +-18000000 range to be square) is one largest tile (256 pixels for raster); desined for geographic projection
		ETST_GOOGLE_CRS84_QUAD,

		/// The whole world is one largest tile (256 pixels for raster); desined for pseudo-mercator metric projection
		ETST_GOOGLE_MAPS_COMPATIBLE
	};	
	///  Building surface types used in #SSmartRealityBuildingSurface	
	enum EBuildingSurfaceType
	{
		EBST_WALL,			///< Wall surface
		EBST_ROOF,			///< Roof surface
		EBST_GROUND,		///< Ground surface
		EBST_WINDOW			///< Window surface
	};
	/// Definition of tiling scheme used in raw and native map layers
	struct STilingScheme
	{
		SMcVector2D	TilingOrigin;				///< Tiling origin
		double		dLargestTileSizeInMapUnits;	///< Largest tile size in map units
		UINT        uNumLargestTilesX;         ///< Number of largest tiles along X axis
		UINT        uNumLargestTilesY;         ///< Number of largest tiles along Y axis
		UINT		uTileSizeInPixels;			///< Tile size in pixels when it is shown in its natural scale (its natural scale is a ratio between 
												///<	size in map units and size in pixels); For raster layer: tile texture size in pixels
		UINT		uRasterTileMarginInPixels;	///< For raster layer: number of pixels outside the tile added to each side of its texture for overlapping)

		STilingScheme() {}

		STilingScheme(const SMcVector2D &_TilingOrigin, double _dLargestTileSizeInMapUnits, UINT _uNumLargestTilesX, UINT _uNumLargestTilesY, UINT _uTileSizeInPixels, UINT _uRasterTileMarginInPixels)
		  : TilingOrigin(_TilingOrigin), dLargestTileSizeInMapUnits (_dLargestTileSizeInMapUnits), uNumLargestTilesX(_uNumLargestTilesX), uNumLargestTilesY(_uNumLargestTilesY),
			uTileSizeInPixels(_uTileSizeInPixels), uRasterTileMarginInPixels(_uRasterTileMarginInPixels) {}

		bool operator == (const STilingScheme &Other) const { return (TilingOrigin == Other.TilingOrigin && dLargestTileSizeInMapUnits == Other.dLargestTileSizeInMapUnits &&
			uNumLargestTilesX == Other.uNumLargestTilesX && uNumLargestTilesY == Other.uNumLargestTilesY &&
			uTileSizeInPixels == Other.uTileSizeInPixels && uRasterTileMarginInPixels == Other.uRasterTileMarginInPixels ); }
		
		STilingScheme& operator=(const STilingScheme &source)
		{
			TilingOrigin = source.TilingOrigin;
			dLargestTileSizeInMapUnits = source.dLargestTileSizeInMapUnits;
			uNumLargestTilesX = source.uNumLargestTilesX;
			uNumLargestTilesY = source.uNumLargestTilesY;
			uTileSizeInPixels = source.uTileSizeInPixels;
			uRasterTileMarginInPixels = source.uRasterTileMarginInPixels;

			return *this;
		}
		
	};

	/// Raw component parameters
	struct SComponentParams
	{
		/// File/directory name
		char			strName[MAX_PATH];

		/// Component's type (file of directory)
		EComponentType	eType;

		/// Component's world limit (x and y only are relevant);
		/// When the struct is used as input parameter and world limit should be taken from the files,
		///	fill all limits with DBL_MAX values
		SMcBox			WorldLimit;


		SComponentParams() { memset(this, 0, sizeof(*this)); }

		bool operator==(const SComponentParams &ComponentParams) const
		{
			return ((strcmp(strName, ComponentParams.strName) == 0)
				&&
				(eType == ComponentParams.eType) &&
				(WorldLimit == ComponentParams.WorldLimit));
		}
		SComponentParams& operator=(const SComponentParams &source)
		{
			strcpy(strName,source.strName);
			eType = source.eType;
			WorldLimit = source.WorldLimit;

			return *this;
		}
	};

	/// WMTS tile matrix set's data or WCS/WMS CRS data
	struct STileMatrixSet
	{
		PCSTR strName;									///< unique name (for #EWMS_WMTS service: name of TileMatrixSet,
														///< for #EWMS_WCS and and #EWMS_WMS services: name of CRS)
		IMcGridCoordinateSystem *pCoordinateSystem;		///< coordinate system
		SMcBox BoundingBox;								///< bounding box
		bool bHasBoundingBox;							///< whether or not oBoundingBox is valid

		STileMatrixSet()
		{
			memset(this, 0, sizeof(*this));
		}
	};

	/// Server's layer info
	struct SServerLayerInfo
	{
		PCSTR strLayerId;							///< The layer's ID
		PCSTR strTitle;								///< The layer's title
		PCSTR strLayerType;							///< The layer's type (for #EWMS_MAPCORE and #EWMS_CSW services only)
		const PCSTR *astrImageFormats;				///< The supported image formats (for servers other than #EWMS_MAPCORE)
		UINT uNumImageFormats;						///< Number of image formats in **astrImageFormats** array
		bool bTransparent;							///< Whether or not image format supports alpha; the default is false (for #EWMS_WMS service only)
		const PCSTR *astrStyles;					///< The supported styles (for servers other than #EWMS_MAPCORE)
		UINT uNumStyles;							///< Number of styles in **astrStyles** array
		const PCSTR *astrGroups;					///< An array of groups that the layer belongs to (for #EWMS_MAPCORE service only)
		UINT uNumGroups;							///< Number of groups in **astrGroups** array
		int nDrawPriority;							///< the layer's draw priority (for #EWMS_MAPCORE service only)
		SMcBox BoundingBox;							///< the layer's bounding box
													///< (for #EWMS_WCS and #EWMS_WMS services, the specifically defined GeoWgs84 BoundingBox, or if not found, zeros.
													///< for #EWMS_WMTS service, the CRS BoundingBox, or if not found, the specifically defined GeoWgs84 BoundingBox.)
													///< Note: consider using bounding box and coordinate system of first entry in aTileMatrixSets, if exist.
		IMcGridCoordinateSystem *pCoordinateSystem;	///< The layer's coordinate system
													///< (for #EWMS_WCS and #EWMS_WMS services, EPSG:4326 or NULL. for #EWMS_WMTS service, CRS or EPSG:4326.)
													///< Note: consider using bounding box and coordinate system of first entry in aTileMatrixSets, if exist.
		const STileMatrixSet *aTileMatrixSets;		///< An array of tile matrix sets defined for the layer (for #EWMS_WMTS, #EWMS_WCS and #EWMS_WMS services only)
		UINT uNumTileMatrixSets;					///< Number of tile matrix sets in **aTileMatrixSets** array
		const SMcKeyStringValue *aMetadataValues;	///< An array of metadata key and value pairs defined for the layer
		UINT uNumMetadataValues;					///< Number of metadata pairs in **aMetadataValues** array

		SServerLayerInfo()
		{
			memset(this, 0, sizeof(*this));
		}
	};
	
	/// Smart Reality building surface
	struct SSmartRealityBuildingSurface
	{
		SMcVariantID uSurfaceID;					///< building surface ID
		CMcDataArray<SMcVector3D> aSurfaceContour;	///< building surface contour
		SMcVector3D	SurfaceNormal;					///< building surface normal
		SMcVector3D SurfaceCenter;					///< building surface center (pole of inaccessibility of its contour)
		double dSurfaceArea;						///< building surface area
		EBuildingSurfaceType eSurfaceType;			///< building surface type (wall, roof, ground, window)

		SSmartRealityBuildingSurface()
		{
			memset((void*)this, 0, sizeof(*this));
		}
	};

	
	/// Smart Reality building history record
	struct SSmartRealityBuildingHistory
	{		
		CMcTime	FlightDate;									       	  ///< flight date
		double	dHeight;										      ///< building height
		SMcBox  BoundingBox;										  ///< building bounding box 	
		IMcGridCoordinateSystem *pCoordinateSystem;					  ///< bounding box coordinate system
		CMcDataArray<SSmartRealityBuildingSurface> aBuildingSurfaces; ///< building surfaces  

		SSmartRealityBuildingHistory()
		{
			memset((void*)this, 0, sizeof(*this));
		}
	};

	/// Type of web map service
	enum EWebMapServiceType : BYTE
	{
		EWMS_WMS,			///< WMS server
		EWMS_WMTS,			///< WMTS server
		EWMS_WCS,			///< WCS server
		EWMS_MAPCORE,       ///< MapCore's Map Layer Server
		EWMS_CSW			///< CSW server
	};

	/// Extended result of intersection with single vector item
	struct SVectorItemFound
	{
		UINT64 uVectorItemID;				///< single vector item's ID
		UINT uVectorItemFirstPointIndex;	///< index of the item's first point in the unified item
		UINT uVectorItemLastPointIndex;		///< index of the item's last point in the unified item
	};

	/// Parameters of layer's one level of detail
	struct SLayerLodParams
	{
		int			nLevelIndex;			///< Index of the level (can be negative; finer levels have smaller indices)
		SMcVector2D	TileWorldSize;			///< Tile size in world units
		SMcSize		TileImagePixelSize;		///< For image-based layers only: size of tile's image in pixels
		double		dTileImageResolution;	///< For image-based layers only: resolution of tile's image (world units per pixel)
	};

	/// Layer's tile unique ID
	struct SLayerTileKey
	{
		int		nLOD;		///< level of detail
		UINT	uX;			///< X index
		UINT	uY;			///< Y index

		/// Get the tile's key of the tile's parent
		void GetParentKey(SLayerTileKey *pParentKey) const
		{
			pParentKey->nLOD = nLOD + 1;
			pParentKey->uX = uX / 2;
			pParentKey->uY = uY / 2;
		}

		/// Get the tile's key of the tile's first child 
		void GetFirstChildKey(SLayerTileKey *pChildKey) const
		{
			pChildKey->nLOD = nLOD - 1;
			pChildKey->uX = uX * 2;
			pChildKey->uY = uY * 2;
		}
	};

	/// Tile's post-process data that includes the original pixel data
	struct STilePostProcessData
	{
		IMcMapLayer::SLayerTileKey TileKey;
		IMcTexture::EPixelFormat eTilePixelFormat;	///< The tile's pixel format.
		SMcSize TileSizeInPixels;					///< The tile's size in pixels.

		const BYTE *pSrcBuffer;						///< The original pixel data, but number and order of channels
													///< are according to eTilePixelFormat.
		UINT uSrcBytesPerChannel;					///< The original bytes per channel value.
		BYTE *pTileBuffer;							///< The tile's buffer that needs to be filled by OnPostProcessSourceData() callback; 
													///< it's bytes per channel value is always 1.
	};

	/// A callback interface to be implemented by the user to receive the layer's reading/communication events
	class IReadCallback
#ifdef __EMSCRIPTEN__
		: public IMcEmscriptenCallbackRef
#endif
	{
	public:

		virtual ~IReadCallback() {}

		//==============================================================================
		// Method Name: OnInitialized()
		//------------------------------------------------------------------------------
		/// Called when the layer's initialization has been completed.
		/// 
		/// \param[in]	pLayer					The layer (can be NULL if the initialization failed during layer creation).
		/// \param[in]	eStatus					The initialization status: success (IMcErrors::SUCCESS) or error.
		///										In case of IMcErrors::NATIVE_SERVER_LAYER_NOT_VALID error code OnNativeServerLayerNotValid() will follow.
		/// \param[in]	strAdditionalDataString	The additional data string: a file name that cannot be read, a request that failed etc.
		//==============================================================================
		virtual void OnInitialized(IMcMapLayer *pLayer, IMcErrors::ECode eStatus, PCSTR strAdditionalDataString) = 0;

		//==============================================================================
		// Method Name: OnReadError()
		//------------------------------------------------------------------------------
		/// Called when a reading/communication error has occurred.
		/// 
		/// \param[in]	pLayer					The layer.
		/// \param[in]	eErrorCode				The error code.
		/// \param[in]	strAdditionalDataString	The additional data string: a file name that cannot be read, a request that failed etc.
		//==============================================================================
		virtual void OnReadError(IMcMapLayer *pLayer, IMcErrors::ECode eErrorCode, PCSTR strAdditionalDataString) = 0;

		//==============================================================================
		// Method Name: OnNativeServerLayerNotValid()
		//------------------------------------------------------------------------------
		/// Called when the layer's version has been updated by MapCore's Map Layer Server or the layer has not been found by the server 
		/// (deleted or never existed).
		/// 
		/// Relevant for native-server layers only. Preceded by OnInitialized() if occurred during the layer's initialization process.
		///
		/// The recommended behavior depends on \p bLayerVersionUpdated:
		/// - true (layer's version updated): replace the layer in all its terrains by the newly created one with the same parameters 
		///   (by calling IMcMapLayer::ReplaceNativeServerLayerAsync();
		/// - false (layer not found): remove the layer from all its terrains by calling IMcMapLayer::RemoveLayerAsync().
		/// 
		/// \param[in]	pLayer					The layer that is no longer valid.
		/// \param[in]	bLayerVersionUpdated	Whether the layer's version has been updated by MapCore's Map Layer Server or 
		///										the layer has not been found by the server (deleted or never existed).
		//==============================================================================
		virtual void OnNativeServerLayerNotValid(IMcMapLayer *pLayer, bool bLayerVersionUpdated) = 0;

		//==============================================================================
		// Method Name: OnRemoved()
		//------------------------------------------------------------------------------
		/// Called by IMcMapLayer::RemoveLayerAsync() when the layer has been removed from all its terrains just before it is deleted.
		/// 
		/// Any terrain that becomes empty after removing the layer is removed from all its viewports.
		///
		/// \param[in]	pLayer					The removed layer.
		/// \param[in]	eStatus					The operation's status: success (IMcErrors::SUCCESS) or error.
		/// \param[in]	strAdditionalDataString	The additional data string describing the failure reason (if failed).
		//==============================================================================
		virtual void OnRemoved(IMcMapLayer *pLayer, IMcErrors::ECode eStatus, PCSTR strAdditionalDataString) {}

		//==============================================================================
		// Method Name: OnReplaced()
		//------------------------------------------------------------------------------
		/// Called by IMcMapLayer::ReplaceNativeServerLayerAsync() when the layer has been replaced by a new one all its terrains just before it is deleted.
		/// 
		/// \param[in]	pOldLayer				The old layer that has been replaced by a new one.
		/// \param[out]	pNewLayer				The new layer that replaced the original one.
		/// \param[in]	eStatus					The operation's status: success (IMcErrors::SUCCESS) or error.
		/// \param[in]	strAdditionalDataString	The additional data string describing the failure reason (if failed).
		//==============================================================================
		virtual void OnReplaced(IMcMapLayer *pOldLayer, IMcMapLayer *pNewLayer, IMcErrors::ECode eStatus, PCSTR strAdditionalDataString) {}

		//==============================================================================
		// Method Name: OnPostProcessSourceData()
		//------------------------------------------------------------------------------
		/// Called when there is a change in the layer's tiles that are being rendered
		/// 
		/// \param[in]		pLayer				The layer.
		/// \param[in]		bGrayscaleSource	Whether or not source is grayscale.
		/// \param[in,out]	aVisibleTiles[]		The array of the tiles currently visible in a viewport; the user should fill
		///										the already allocated buffer for each tile in the array.
		/// \param[in]		uNumVisibleTiles	The number of tiles in the above array.
		//==============================================================================
		virtual void OnPostProcessSourceData(IMcMapLayer *pLayer, bool bGrayscaleSource, const STilePostProcessData aVisibleTiles[], UINT uNumVisibleTiles) {}
		
		//==============================================================================
		// Method Name: Release()
		//------------------------------------------------------------------------------
		/// A callback that should release the callback class instance.
		///
		///	Can be implemented by the user to optionally delete callback class instance when 
		/// IMcMapLayer instance is been removed.
		//==============================================================================
		virtual void Release() {}

		//================================================================
		// Method Name: GetSaveBufferSize()
		//----------------------------------------------------------------
		/// A callback that should return the memory buffer size needed to save the data.
		///
		///	Default implementation returns 0 - read callback is not saved.
		///
		/// \return 
		///		- The needed buffer size (zero means the data should not be saved when its 
		///		  container is saved)
		///
		//================================================================
		virtual UINT GetSaveBufferSize() { return 0; }

		//================================================================
		// Method Name: SaveToBuffer()
		//----------------------------------------------------------------
		/// A callback that should save the data to a memory buffer.
		///
		///	Default implementation does nothing - read callback is not saved.
		///
		/// \param[out]	aBuffer			The buffer to save to (the buffer size is equal or greater 
		///								of the return value of GetSaveBufferSize())
		//================================================================
		virtual void SaveToBuffer(void* aBuffer) { (void)aBuffer; }
	};

	//==================================================================================
	// Interface Name: IReadCallbackFactory
	//----------------------------------------------------------------------------------
	/// The base interface for read callback factory. 
	/// 
	/// Used as a base interface for user-defined read callback factory.
	/// Must be inherited in order to define read callback factory.
	//==================================================================================
	class IReadCallbackFactory
#ifdef __EMSCRIPTEN__
		: public IMcEmscriptenCallbackRef
#endif
	{
	public:
		virtual ~IReadCallbackFactory() {}

		//================================================================
		// Method Name: CreateReadCallback()
		//----------------------------------------------------------------
		/// A callback that should create read callback class instance
		///
		/// \param[in]	aBuffer				The buffer to load from
		/// \param[in]	uBufferSize			The buffer size
		/// 
		/// \return 
		///		- user data class instance created 
		//================================================================
		virtual IReadCallback * CreateReadCallback(void* aBuffer, UINT uBufferSize) = 0;
	};

	/// Asynchronous operation callback receiving operation results
	class IAsyncOperationCallback
#ifdef __EMSCRIPTEN__
		: public IMcEmscriptenCallbackRef
#endif
	{
	public:

		virtual ~IAsyncOperationCallback() {}

		//==============================================================================
		// Method Name: OnScanExtendedDataResult()
		//------------------------------------------------------------------------------
		/// Called when asynchronous operation of IMcVectorMapLayer::GetScanExtendedData() has been completed; returns the results.
		///
		/// \param[in]	pLayer						The layer.
		/// \param[in]	eStatus						The operation's status: success (IMcErrors::SUCCESS) or error.
		/// \param[in]	aVectorItems				The vector items data of the unified object found in scan.
		/// \param[in]	aUnifiedVectorItemsPoints	The points of polylines of the unified object found in scan.
		//==============================================================================
		virtual void OnScanExtendedDataResult(IMcMapLayer *pLayer, IMcErrors::ECode eStatus,
			const CMcDataArray<IMcMapLayer::SVectorItemFound> &aVectorItems, const CMcDataArray<SMcVector3D> &aUnifiedVectorItemsPoints) {}

		//==============================================================================
		// Method Name: OnVectorItemPointsResult()
		//------------------------------------------------------------------------------
		/// Called when asynchronous operation of IMcVectorMapLayer::GetVectorItemPoints() has been completed; returns the results.
		///
		/// \param[in]	pLayer					The layer.
		/// \param[in]	eStatus					The operation's status: success (IMcErrors::SUCCESS) or error.
		/// \param[in]	aaPoints				One or more arrays of points (there can be more than one array of points in case of multi-line or multi-polygon).
		//==============================================================================
		virtual void OnVectorItemPointsResult(IMcMapLayer *pLayer, IMcErrors::ECode eStatus,
			const CMcDataArray<CMcDataArray<SMcVector3D> > &aaPoints) {}

		//==============================================================================
		// Method Name: OnFieldUniqueValuesResult()
		//------------------------------------------------------------------------------
		/// Called when asynchronous operation of IMcVectorMapLayer::GetFieldUniqueValues() has been completed; returns the results.
		///
		/// \param[in]	pLayer					The layer.
		/// \param[in]	eStatus					The operation's status: success (IMcErrors::SUCCESS) or error.
		/// \param[in]	eReturnedType			The field's returned type defining the type of array elements of \p paUniqueValues.
		/// \param[out]	paUniqueValues			The retrieved unique values as CMcDataArray with elements of the type defined by \p eReturnedType.
		//==============================================================================
		virtual void OnFieldUniqueValuesResult(IMcMapLayer *pLayer, IMcErrors::ECode eStatus, 
			EVectorFieldReturnedType eReturnedType, const void *paUniqueValues) {}

		//==============================================================================
		// Method Name: OnVectorItemFieldValueResult()
		//------------------------------------------------------------------------------
		/// Called when asynchronous operation of IMcVectorMapLayer::GetVectorItemFieldValue() has been completed; returns the results.
		///
		/// \param[in]	pLayer					The layer.
		/// \param[in]	eStatus					The operation's status: success (IMcErrors::SUCCESS) or error.
		/// \param[in]	eReturnedType			The field's returned type defining the type of \p pValue.
		/// \param[out]	pValue					The retrieved value of the type defined by \p eReturnedType.
		//==============================================================================
		virtual void OnVectorItemFieldValueResult(IMcMapLayer *pLayer, IMcErrors::ECode eStatus, 
			EVectorFieldReturnedType eReturnedType, const void *pValue) {}

		//==============================================================================
		// Method Name: OnVectorQueryResult()
		//------------------------------------------------------------------------------
		/// Called when asynchronous operation of IMcVectorMapLayer::Query() has been completed; returns the results.
		///
		/// \param[in]	pLayer					The layer.
		/// \param[in]	eStatus					The operation's status: success (IMcErrors::SUCCESS) or error.
		/// \param[out]	auVectorItemsID			The retrieved group of vector items IDs.
		//==============================================================================
		virtual void OnVectorQueryResult(IMcMapLayer *pLayer, IMcErrors::ECode eStatus,
			const CMcDataArray<UINT64> &auVectorItemsID) {}

		//==============================================================================
		// Method Name: OnWebServerLayersResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous operation of IMcMapDevice::GetWebServerLayers() has been completed; returns the results.
		///
		/// \param[in]	eStatus						The operation's status: success (IMcErrors::SUCCESS) or error.
		/// \param[in]	strServerURL				The server's URL specified by the user in IMcMapDevice::GetWebServerLayers().
		/// \param[in]	eWebMapServiceType			The web service type specified by the user in IMcMapDevice::GetWebServerLayers().
		/// \param[in]	aLayers						The array of the server's layers along with their information.
		/// \param[in]	uNumLayers					The number of the layers in the above array.
		/// \param[in]	astrServiceMetadataURLs		The array of service metadata URLs.
		/// \param[in]	uNumServiceMetadataURLs		The number URLs in the above array.
		/// \param[in]  strServiceProviderName		The service provider name.
		///
		/// \note
		/// For C++/JS users: if some of `aLayers[i].pCoordinateSystem` or `aLayers[i].aTileMatrixSets[j].pCoordinateSystem` are not `NULL' and 
		/// should be used after OnWebServerLayersResults() returns, call here IMcBase::AddRef() for these coordinate systems (and call 
		/// IMcBase::Release() when they are no longer needed), otherwise they will be released by MapCore after OnWebServerLayersResults() returns.
		//==============================================================================
		virtual void OnWebServerLayersResults(IMcErrors::ECode eStatus,
			PCSTR strServerURL, EWebMapServiceType eWebMapServiceType,
			const IMcMapLayer::SServerLayerInfo aLayers[], UINT uNumLayers,
			const PCSTR astrServiceMetadataURLs[], UINT uNumServiceMetadataURLs,
			PCSTR strServiceProviderName) {}

		//==============================================================================
		// Method Name: On3DModelSmartRealityDataResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous operation of IMcMapDevice::Get3DModelSmartRealityData() has been completed; returns the results.
		///
		/// \param[in]	eStatus						The operation's status: success (IMcErrors::SUCCESS) or error.
		/// \param[in]	strServerURL				The server's URL specified by the user in IMcMapDevice::Get3DModelSmartRealityData().
		/// \param[in]	uObjectID					The UUID of the building sent by user as a request to the Building History server.
		/// \param[in]	aBuildingHistory[]			The array of the building's history records obtained from the server.
		/// \param[in]	uNumBuildingHisoryInstances	The number of the building history records in the above array.		
		///			
		/// \return
		///     - status result
		//==============================================================================
		virtual void On3DModelSmartRealityDataResults(IMcErrors::ECode eStatus,
			PCSTR strServerURL, const SMcVariantID &uObjectID,
			const IMcMapLayer::SSmartRealityBuildingHistory aBuildingHistory[], UINT uNumBuildingHisoryInstances) {}
	};

	/// Non native layers params - for RawDtm, RawRaster, WebServiceDtm, WebServiceRaster
	struct SNonNativeParams
	{
		IMcGridCoordinateSystem *pCoordinateSystem;		///< Layer's coordinate system
		const IMcMapLayer::STilingScheme *pTilingScheme;///< Tiling scheme to use; default is NULL (means MapCore scheme except for WMTS layer 
														///<	that can optionally use its own scheme)
		float fMaxScale;								///< Layer's typical maximal scale; for source data without levels of detail (built-in or added), 
														///<	best performance will be achieved in camera scales equal to or below this scale
		bool bResolveOverlapConflicts;					///< Whether to resolve conflicts between overlapping sources:\n
														///<	`false` means last source data is always used,\n
														///<	`true` means last non-empty source data is used (affects performance)
		bool bEnhanceBorderOverlap;						///< Whether to enhance raster layer border quality (may prevent black border lines
														///<	in case of overlapping layers, but affects performance)
		bool bFillEmptyTilesByLowerResolutionTiles;		///< Whether to fill empty tiles of WMTS and level-of-detail-based raw raster layer 
														///<	(for example, in GeoPackage format) with the data from lower resolution tiles 
														///<    (use only in case of variable-resolution layers)
		SMcBColor TransparentColor;						///< Pixel color that should be treated as transparent, used only when its `a` channel is not 0
		BYTE byTransparentColorPrecision;				///< Transparent color precision; non-zero value means for each channel `r/g/b` of \a TransparentColor 
														///<	its value plus/minus the precision will be considered transparent
		IReadCallback *pReadCallback;					///< Callback receiving read layer events

		SNonNativeParams()
		{
			memset(this, 0, sizeof(*this));
			fMaxScale = FLT_MAX;
		}
	};

	/// Raw layers params - for RawDtm, RawRaster 
	struct SRawParams : SNonNativeParams
	{
		PCSTR strDirectory;								///< Directory containing the layer's files (can be NULL)
		const SComponentParams *aComponents;			///< Layer's components (files and directories, relative to strDirectory)
		UINT uNumComponents;							///< Number of the above components
		UINT uMaxNumOpenFiles;							///< Maximal number of files open simultaneously; 
														///<  the default is 0 (means MapCore's default: 5000 for Windows, 500 otherwise)
		float fFirstPyramidResolution;					///< First (finest) resolution of optional resolutions' pyramid in 
														///<  map units per pixel; 0 if resolutions' pyramid is not used
		const UINT *auPyramidResolutions;				///< Optional indices of components in \a aComponents 
														///<  starting each resolution; each resolution is twice coarser than the previous one
		UINT uNumPyramidResolutions;					///< Number of resolutions in \a auPyramidResolutions
		bool bIgnoreRasterPalette;						///< If `true`, ignore the raster palette and show as grayscale
		SMcBox MaxWorldLimit;							///< Maximal bounding box in map units for clipping to be used instead of that of the components, 
														///<  otherwise should be filled with `DBL_MAX` values (the default);
														///<  cannot be default for dynamic raster
		float  fHighestResolution;						///< Highest (finest) resolution in map units per pixel (per DTM point for DTM) to be used 
														///<  instead of that of the sources, otherwise should be 0 (the default); 
														///<  cannot be default for dynamic raster
		bool bAddLevelsOfDetailIfAbsent;				///< Whether to add to every source file its levels of detail (as GDAL's *.ovr file); 
														///<  relevant only for source files without levels of detail (built-in or GDAL's *.ovr) 
														///<  if resolutions' pyramid is not used (\a auPyramidResolutions is empty)
		IMcProgressCallback *pLodProgressCallback;		///< Callback used to pass progress messages to user application during adding levels of detail 
														///<  (when \a bAddLevelsOfDetailIfAbsent is `true`)
		bool bPostProcessSourceData;					///< Whether to post-process rendered tiles' buffers when a tile is added or removed.
													    ///< The post-process will be done by calling IReadCallback::OnPostProcessSourceData() callback.
		SRawParams()
		{
			//memset(this, 0, sizeof(*this));
			strDirectory = NULL;
			aComponents = NULL;
			uNumComponents = 0;
			uMaxNumOpenFiles = 0;
			fFirstPyramidResolution = 0;
			auPyramidResolutions = NULL;
			uNumPyramidResolutions = 0;
			bIgnoreRasterPalette = false;
			MaxWorldLimit = SMcBox(DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX);
			fHighestResolution = 0;
			bAddLevelsOfDetailIfAbsent = false;
			pLodProgressCallback = NULL;
			bPostProcessSourceData = false;
		}
	};

	/// Order of WMTS coordinate axes
	enum ECoordinateAxesOrder
	{
		ECAO_DEFAULT,		///< Default order according to coordinate system used
		ECAO_INVERSE,		///< Inverse of default order according to coordinate system used
		ECAO_XY,			///< X, Y order
		ECAO_YX				///< Y, X order
	};

	/// Parameters used for accessing a Web server
	struct SWebMapServiceParams : IMcMapLayer::SNonNativeParams
	{
		PCSTR strServerURL;							///< Server URL (must be specified).
													///<
													///< For WMTS only: the URL may or may not include the capabilities request, e.g.:
													///<  - http://foo
													///<  - http://foo?service=WMTS
													///<  - http://foo/WMTSCapabilities.xml
													///<  - http://foo?request=GetCapabilities
													///<  - http://foo?service=WMTS&request=GetCapabilities
													///<   If it does not, MapCore will add the following to **strServerURL**: "?service=WMTS&request=GetCapabilities"
													///< For WCS and WMS: the URL should not contain the capabilities request, e.g:
													///<  - http://foo
													///<  - http://foo?service=WCS
													///<  - http://foo?service=WMS
		PCSTR strOptionalUserAndPassword;			///< User and password for HTTP authentication (in user:password format) or NULL (the default).
		bool bSkipSSLCertificateVerification;		///< Skip SSL certificate verification (may be needed if server is 
													///<   using a self signed certificate); the default is false
		const SMcKeyStringValue *aRequestParams;	///< Optional list of additional parameters (as key-value pairs) to be added to each HTTP request as "key=value"
		UINT uNumRequestParams;						///< Number of parameters in **aRequestParams**
		UINT uTimeoutInSec;							///< Connection timeout in seconds; the default is 300 seconds
		PCSTR strLayersList;						///< List of one or more layers (comma separated, URL encoded);
													///<   must be specified
		PCSTR strStylesList;						///< List of one or more styles (comma separated, URL encoded);
													///<   optional
		SMcBox BoundingBox;							///< World bounding box in map units (must be specified for WMS, optional for WMTS, unused in WCS)
		PCSTR strServerCoordinateSystem;			///< For WMS and WCS: requested coordinate system in server format (EPSG:XXXX, CRS:XX, etc.) 
													///<   matching layer coordinate system in MapCore format specified in Create() (must be specified for WMS)\n 
													///< For WMTS: an ID of one of the layer's tile matrix sets listed in the capabilities (must be specified)
		PCSTR strImageFormat;						///< Image format (must be specified for WMS and WMTS);
													///<   for example: png, jpeg, tiff
		bool bTransparent;							///< Whether or not image format supports alpha (unused in WCS); the default is false
		PCSTR strZeroBlockHttpCodes;				///< Comma separated list of HTTP response codes that will be interpreted as a 0 filled image
													///<   (black for 3 bands, transparent for 4 bands) instead of aborting the request; 
													///<   the default is 204,404.
		bool bZeroBlockOnServerException;		    ///< Whether to treat a Service Exception returned by the server as a 0 filled image
													///<   instead of aborting the request; the default is true

		SWebMapServiceParams()
		{
			strServerURL = NULL;
			strOptionalUserAndPassword = NULL;
			bSkipSSLCertificateVerification = false;
			aRequestParams = NULL;
			uNumRequestParams = 0;
			uTimeoutInSec = 300;
			strLayersList = NULL;
			strStylesList = NULL;
			BoundingBox = SMcBox(0, 0, 0, 0, 0, 0);
			strServerCoordinateSystem = NULL;
			strImageFormat = NULL;
			bTransparent = false;
			strZeroBlockHttpCodes = "204,404";
			bZeroBlockOnServerException = true;
		}
	};

	/// Parameters used for accessing a WMS server
	struct SWMSParams : SWebMapServiceParams
	{
		PCSTR strWMSVersion;						///< WMS version, the default is 1.3.0.
		UINT uBlockWidth;							///< Width of block (in pixels) to fetch in each request; the default is 256
		UINT uBlockHeight;							///< Height of block (in pixels) to fetch in each request; the default is 256
		float fMinScale;							///< Layer's min scale (=highest resolution) to request from server

		SWMSParams() : SWebMapServiceParams()
		{
			strWMSVersion = "1.3.0";
			uBlockWidth = 256;
			uBlockHeight = 256;
			fMinScale = 1;
		}
	};

	/// Parameters used for accessing a WMTS server
	struct SWMTSParams : SWebMapServiceParams
	{
		PCSTR strInfoFormat;									///< Info format, used by GetFeatureInfo requests;
																///<   must be one of layer's info formats listed in the server
																///<   (e.g.: application/xml); the default is NULL.
		bool bExtendBeyondDateLine;								///< Whether to make the extent go over dateline and warp tile requests;
																///<	the default is false
		bool bUseServerTilingScheme;							///< Whether to use server's tiling scheme instead of SNonNativeParams::pTilingScheme;
																///<	using tiling scheme non-compatible with that of the server affects performance
		ECoordinateAxesOrder eCapabilitiesBoundingBoxAxesOrder;	///< Axes order of bounding box in capabilities reported by server; 
																///<	the default is #ECAO_DEFAULT

		SWMTSParams() : SWebMapServiceParams()
		{
			strInfoFormat = NULL;
			bExtendBeyondDateLine = false;
			bUseServerTilingScheme = false;
			eCapabilitiesBoundingBoxAxesOrder = ECAO_DEFAULT;
		}
	};

	/// Parameters used for accessing a WCS server
	struct SWCSParams : SWebMapServiceParams
	{
		PCSTR strWCSVersion;									///< WCS version, the default is 1.0.0.
		bool bDontUseServerInterpolation;						///< Whether to use MapCore internal interpolation
																///< instead of server interpolation. Relevant only for DTM layers.
																///< default is false (meaning: use server interpolation)
															

		SWCSParams() : SWebMapServiceParams()
		{
			strWCSVersion = "1.0.0";
			bDontUseServerInterpolation = false;
		}
	};

	/// Parameters of local cache that can be used if the layer is accessed over a network
	struct SLocalCacheLayerParams
	{
		PCSTR	strLocalCacheSubFolder;	///< Path to local sub folder to use for cache
		PCSTR	strOriginalFolder;		///< Path to original folder with layer files; relevant only if used in output, 
										///<  ignored if used as input (in Create() functions)

		SLocalCacheLayerParams() { strLocalCacheSubFolder = NULL; strOriginalFolder = NULL; }
	};

	/// \name Layer Visibility
	//@{

	//==============================================================================
	// Method Name: SetVisibility()
	//------------------------------------------------------------------------------
	/// Sets the layer's visibility in all viewports or in one specific viewport.
	///
	/// Setting the layer's visibility in all viewports overrides its visibility in each viewport 
	/// previously set. On the other hand, it can later be changed in any specific viewport.
	///
	/// \param[in]	bVisibility			The layer's visibility (the default is true)
	/// \param[in]	pMapViewport		The viewport to set a visibility in or NULL for all viewports
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetVisibility(bool bVisibility, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetVisibility()
	//------------------------------------------------------------------------------
	/// Retrieves the default visibility of the layer in all viewports (set by SetVisibility(, NULL)) 
	/// or its visibility in one specific viewport.
	///
	/// \param[out]	pbVisibility		The layer's visibility
	/// \param[in]	pMapViewport		The viewport to retrieve a visibility in or NULL for all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetVisibility(bool *pbVisibility, IMcMapViewport *pMapViewport = NULL) const = 0;

	//==============================================================================
	// Method Name: GetEffectiveVisibility()
	//------------------------------------------------------------------------------
	/// Retrieves the current effective visibility of a layer in a specified terrain in a specified viewport.
	///
	/// The result is irrespective to the viewport's current visible area. It is based on 
	/// per-viewport and per-terrain visibility of the layer and its scale range.
	///
	/// \param[in]	pMapViewport		The viewport to retrieve a visibility in.
	/// \param[in]	pTerrain			The terrain to which the layer belongs.
	/// \param[out]	pbVisibility		Whether the layer is currently visible in the viewport
	///									(irrespectively to the viewport's current visible area).
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetEffectiveVisibility(IMcMapViewport *pMapViewport, IMcMapTerrain *pTerrain,
		bool *pbVisibility) const = 0;

	//@}

	/// \name Layer Parameters
	//@{

	//==============================================================================
	// Method Name: GetCoordinateSystem()
	//------------------------------------------------------------------------------
	/// Retrieves the coordinate system.
	///
	/// \param[out]	pCoordinateSystem	Current coordinate system.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCoordinateSystem(
		IMcGridCoordinateSystem **pCoordinateSystem) const = 0; 

	//==============================================================================
	// Method Name: GetBoundingBox()
	//------------------------------------------------------------------------------
	/// Retrieves the bounding box.
	///
	/// \param[out]	pBoundingBox		Current bounding box.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetBoundingBox(SMcBox *pBoundingBox) const = 0;

	//==============================================================================
	// Method Name: GetDirectory()
	//------------------------------------------------------------------------------
	/// Gets base directory
	///
	/// \param[out]	pstrDirectory			A directory containing the layer's files
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetDirectory(PCSTR *pstrDirectory) = 0;

	//==============================================================================
	// Method Name: GetLocalCacheLayerParams()
	//------------------------------------------------------------------------------
	/// Retrieves parameters of layer's local cache defined in layer creation.
	///
	/// \param[out]	pLocalCacheLayerParams		Parameters of layer's local cache
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLocalCacheLayerParams(SLocalCacheLayerParams *pLocalCacheLayerParams) const = 0;

	//==============================================================================
	// Method Name: GetBackgroundThreadIndex(...)
	//------------------------------------------------------------------------------
	/// Retrieves layer's current background thread index.
	///
	/// \param[out]	puThreadIndex		The current background thread for the layer
	///									or UINT(-1) if no background thread found.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetBackgroundThreadIndex(UINT *puThreadIndex) const = 0;

	//==============================================================================
	// Method Name: GetCallback()
	//------------------------------------------------------------------------------
	/// Retrieves the callback passed during layer creation.
	///
	/// \param[out]	ppReadCallback		The callback passed during layer creation.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCallback(IReadCallback **ppReadCallback) const = 0;

	/// For internal use
	static MAPLAYER_API IMcErrors::ECode SetEarthCurvatureCorrection(const SMcVector3D *pCorrectionPoint);
	/// For internal use
	static MAPLAYER_API IMcErrors::ECode GetEarthCurvatureCorrectionLocalOffset(const SMcVector3D &WorldPoint, double *pdOffset);

	//@}

	/// \name ID and User Data
	//@{

	//==============================================================================
	// Method Name: SetID()
	//------------------------------------------------------------------------------
	/// Sets the layer's user-defined unique ID that allows retrieving the layer by IMcMapTerrain::GetLayerByID().
	///
	/// \param[in]	uID		The layer's ID to be set (specify ID not equal to #MC_EMPTY_ID to set ID,
	///						equal to #MC_EMPTY_ID to remove ID).
	/// \note
	/// If the layer is used in some terains the ID should be unique in every such terrain, otherwise the function returns IMcErrors::ID_ALREADY_EXISTS
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetID(UINT uID) = 0;

	//==============================================================================
	// Method Name: GetID()
	//------------------------------------------------------------------------------
	/// Retrieves the layer'e user-defined unique ID set by SetID().
	///        
	/// \param [out] puID	The layer ID (or #MC_EMPTY_ID if the ID is not set).
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
	//@}

	/// \name Getting Tiles And Tiles Data
	//@{
	
	//================================================================
	// Method Name: IsInitialized() 
	//----------------------------------------------------------------
	/// Retrieves whether or not the layer is initialized and ready to use
	///
	/// \param[out]	pbIsInitialized		Is layer initialized and ready to use
	///
	/// \return
	///     - Status result
	//================================================================
	virtual IMcErrors::ECode IsInitialized(bool *pbIsInitialized) const = 0;

	//==============================================================================
	// Method Name: GetTileDataByPoint()
	//------------------------------------------------------------------------------
	/// Retrieves the data of the tile of the specified level of detail at the specified world point.
	///
	/// \param[in]	Point				The point to look at
	/// \param[in]	nLOD				The level of detail required or INT_MIN for the finest possible level at this point
	/// \param[in]	bBuildIfPossible	What to do if if the required tile does not exist but the coarser/finer one does: 
	///									whether to build the data from the appropriate portion of a coarser tile or to fail;
	///									should be false if \a nLOD == INT_MIN
	/// \param[out]	pTileKey			The found tile's key (unique ID); \a pTileKey->nLOD can be other than \a nLOD (see note)
	/// \param[out]	pTileBoundingBox	Found tile's bounding box (NULL if unneeded)
	/// \param[out]	pbDoesTileExist		Whether the required tile exists or the data retrieved is based on 
	///									the appropriate portion of a coarser/finer tile (NULL if unneeded)
	/// \note
	/// If \a bBuildIfPossible == false and at the specified world point the finest level of detail is coarser than the 
	/// specified one, the tile of that level will be returned.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTileDataByPoint(const SMcVector3D &Point, int nLOD, bool bBuildIfPossible, 
		SLayerTileKey *pTileKey, SMcBox *pTileBoundingBox = NULL, bool *pbDoesTileExist = NULL) const = 0;

	//==============================================================================
	// Method Name: GetTileDataByKey()
	//------------------------------------------------------------------------------
	/// Retrieves the data of the layer's tile of the specified level of detail at the specified world point.
	///
	/// \param[in]	TileKey				Key of the required tile
	/// \param[in]	bBuildIfPossible	What to do if if the required tile does not exist but the coarser one does: 
	///									whether to build the data from the appropriate portion of a coarser tile or to fail
	/// \param[out]	pTileBoundingBox	Found tile's bounding box (NULL if unneeded)
	/// \param[out]	pbDoesTileExist		Whether the required tile exists or the data retrieved is based on 
	///									the appropriate portion of a coarser tile (NULL if unneeded)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTileDataByKey(const SLayerTileKey &TileKey, 
		bool bBuildIfPossible, SMcBox *pTileBoundingBox, bool *pbDoesTileExist = NULL) const = 0;

	//==============================================================================
	// Method Name: GetLevelsOfDetail()
	//------------------------------------------------------------------------------
	/// Retrieves parameters of each level of details.
	///
	/// \param[out]	paLevelsParams		The parameters of each level of details from the finest to the coarsest one
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLevelsOfDetail(CMcDataArray<SLayerLodParams> *paLevelsParams) const = 0;

	//==============================================================================
	// Method Name: GetStandardTilingScheme()
	//------------------------------------------------------------------------------
	/// Calculates parameters of standard tiling scheme according to its type
	///
	/// \param[in]	eType				Standard scheme type
	/// \param[out]	pTilingScheme		Tiling scheme parameters
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode GetStandardTilingScheme(ETilingSchemeType eType, STilingScheme *pTilingScheme);

	//@}

	/// \name Native Server Layers And Asynchronous Operations
	//@{

	//==============================================================================
	// Method Name: SetNativeServerCredentials()
	//------------------------------------------------------------------------------
	/// Sets the client's token and session ID to be passed to MapCore's Map Layer Server (if required by the server).
	///
	/// \param[in]	strToken		the client's token
	/// \param[in]	strSessionID	the client's session ID
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode SetNativeServerCredentials(PCSTR strToken, PCSTR strSessionID);

	//==============================================================================
	// Method Name: GetNativeServerCredentials()
	//------------------------------------------------------------------------------
	/// Retrieves the client's token and session ID previously set by SetNativeServerCredentials().
	///
	/// \param[out]	pstrToken		the client's token
	/// \param[out]	pstrSessionID	the client's session ID
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode GetNativeServerCredentials(CMcString *pstrToken, CMcString *pstrSessionID);

	//==============================================================================
	// Method Name: CheckAllNativeServerLayersValidityAsync()
	//------------------------------------------------------------------------------
	/// Checks (asynchronously) each existing native-server layer: whether its version has been updated by MapCore's Map Layer Server or 
	/// it has been deleted by the server.
	///
	/// For each layer updated or deleted by the server: IReadCallback::OnNativeServerLayerNotValid() will be called for each such layer.
	/// See IReadCallback::OnNativeServerLayerNotValid() for the recommended behavior.
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode CheckAllNativeServerLayersValidityAsync();

	//==============================================================================
	// Method Name: RemoveLayerAsync()
	//------------------------------------------------------------------------------
	/// Removes the layer from all its terrains (asynchronously).
	/// 
	/// Should be called when the layer has been deleted by MapCore's Map Layer Server (see IReadCallback::OnNativeServerLayerNotValid()) or 
	/// there is some other reason to remove the layer from inside of IReadCallback's function.
	/// 
	/// Any terrain that becomes empty after removing the layer is removed from all its viewports.
	///
	/// After the layer has been removed, IReadCallback::OnRemoved() is called and immediately after that the layer is no longer valid and 
	/// cannot be used.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RemoveLayerAsync() = 0;

	//==============================================================================
	// Method Name: ReplaceNativeServerLayerAsync()
	//------------------------------------------------------------------------------
	/// Replaces the native-server layer with a new layer instance in all its terrains (asynchronously).
	/// 
	/// Should be called when the layer's version has been updated by MapCore's Map Layer Server (see IReadCallback::OnNativeServerLayerNotValid()).
	///
	/// After the layer has been replaced, IReadCallback::OnReplaced() is called (supplying the newly created layer) and 
	/// immediately after that the old layer is no longer valid and cannot be used.
	///
	/// \param[in]	pNewReadCallback	The callback to be used by the newly created layer for receiving read layer events.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ReplaceNativeServerLayerAsync(IReadCallback *pNewReadCallback) = 0;

	//==============================================================================
	// Method Name: SetNumTilesInNativeServerRequest()
	//------------------------------------------------------------------------------
	/// Sets the number of tiles to be requested together from MapCore's Map Layer Server.
	/// 
	/// Applicable only to native-server layers.
	///
	/// \param[in]	uNumTilesInRequest		The number of tiles to be requested together (0 or 1 mean 1 tile per request); 
	///										the default is set by MapCore according to the layer type and its properties. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetNumTilesInNativeServerRequest(UINT uNumTilesInRequest) = 0;

	//==============================================================================
	// Method Name: GetNumTilesInNativeServerRequest()
	//------------------------------------------------------------------------------
	/// Retrieves the number of tiles to be requested together from MapCore's Map Layer Server.
	/// 
	/// Applicable only to native-server layers.
	///
	/// \param[out]	puNumTilesInRequest		The number of tiles to be requested together (0 or 1 mean 1 tile per request); 
	///										the default is set by MapCore according to the layer type and its properties.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetNumTilesInNativeServerRequest(UINT *puNumTilesInRequest) const = 0;

	//@}

	/// \name Save & Load
	//@{

    //================================================================
    // Method Name: Save(...)
    //----------------------------------------------------------------
	/// Saves the layer definition to a file
	///
    /// \param[in]	strFileName			The name of a file to save to
	/// \param[in]	strBaseDirectory	The optional path of a base directory (can be NULL)
	/// \param[in]	bSaveUserData		Whether user data should be saved
	///
	/// \note
	/// - non-NULL base directory means to save all files/directories in the layer definition 
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
	/// Saves the layer definition to a memory buffer. 
	///
    /// \param[out]	pabyMemoryBuffer	The memory buffer filled by the function
    /// \param[in]	strBaseDirectory	The optional path of a base directory (can be NULL)
	/// \param[in]	bSaveUserData		Whether user data should be saved
	///
	/// \note
	/// - non-NULL base directory means to save all files/directories in the layer definition 
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
	/// Loads a layer definition previously saved by Save() from a file. 
	///
    /// \param[out]	ppLayer					The loaded layer
    /// \param[in]	strFileName				The name of the file to load from
    /// \param[in]	strBaseDirectory		The optional path of a base directory
	///										(NULL means current working directory)
	/// \param[in]	pUserDataFactory		The optional user-defined factory that creates user data instances
	/// \param[in]  pReadCallbackFactory	The optional user-defined factory that creates callback during layer creation
	///
	/// \note
	/// - If the layer definition was saved without a base directory (with absolute paths of 
	///   files/directories), these paths will remain the same after loading and \a strBaseDirectory
	///	  will not be used.
	/// - If the layer definition was saved with a base directory (with paths of files/directories 
	///   relative to it), these paths will be relative to \a strBaseDirectory after loading.
	///
    /// \return
    ///     - Status result
    //================================================================
	static MAPLAYER_API IMcErrors::ECode Load(IMcMapLayer **ppLayer, PCSTR strFileName, 
		PCSTR strBaseDirectory = NULL, IMcUserDataFactory *pUserDataFactory = NULL, IReadCallbackFactory* pReadCallbackFactory = NULL);

    //================================================================
    // Method Name: Load(...)
    //----------------------------------------------------------------
	/// Loads a layer definition previously saved by Save() from a memory buffer. 
	///
    /// \param[out]	ppLayer					The loaded layer
    /// \param[in]	abMemoryBuffer			The memory buffer to load from
    /// \param[in]	uBufferSize				The memory buffer size
    /// \param[in]	strBaseDirectory		The optional path of a base directory
	///										(NULL means current working directory)
	/// \param[in]	pUserDataFactory		The optional user-defined factory that creates user data instances
	/// \param[in]  pReadCallbackFactory	The optional user-defined factory that creates callback during layer creation
	///
	/// \note
	/// - If the layer definition was saved without a base directory (with absolute paths of 
	///   files/directories), these paths will remain the same after loading and \a strBaseDirectory
	///	  will not be used.
	/// - If the layer definition was saved with a base directory (with paths of files/directories 
	///   relative to it), these paths will be relative to \a strBaseDirectory after loading.
	///
    /// \return
    ///     - Status result
    //================================================================
	static MAPLAYER_API IMcErrors::ECode Load(IMcMapLayer **ppLayer, const BYTE abMemoryBuffer[], 
		UINT uBufferSize, PCSTR strBaseDirectory = NULL, IMcUserDataFactory *pUserDataFactory = NULL, IReadCallbackFactory *pReadCallbackFactory = NULL);

	//@}

    /// \name Layer Type And Casting
    //@{

    //================================================================
    // Method Name: GetLayerType() 
    //----------------------------------------------------------------
    /// Returns the layer type unique type ID
    ///
 	/// \remark
	///		Use the cast methods in order to get the correct type.
   //================================================================
    virtual UINT GetLayerType() const = 0;

    //================================================================
    // Method Name: CastToDtmMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcDtmMapLayer*
    /// 
    /// \return
    ///     - #IMcDtmMapLayer*
    //================================================================
	virtual IMcDtmMapLayer* CastToDtmMapLayer() = 0;

    //================================================================
    // Method Name: CastToNativeDtmMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcNativeDtmMapLayer*
    /// 
    /// \return
    ///     - #IMcNativeDtmMapLayer*
    //================================================================
	virtual IMcNativeDtmMapLayer* CastToNativeDtmMapLayer() = 0;

    //================================================================
    // Method Name: CastToNativeServerDtmMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcNativeServerDtmMapLayer*
    /// 
    /// \return
    ///     - #IMcNativeServerDtmMapLayer*
    //================================================================
	virtual IMcNativeServerDtmMapLayer* CastToNativeServerDtmMapLayer() = 0;

    //================================================================
    // Method Name: CastToRawDtmMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcRawDtmMapLayer*
    /// 
    /// \return
    ///     - #IMcRawDtmMapLayer*
    //================================================================
	virtual IMcRawDtmMapLayer* CastToRawDtmMapLayer() = 0;

    //================================================================
    // Method Name: CastToRasterMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcRasterMapLayer*
    /// 
    /// \return
    ///     - #IMcRasterMapLayer*
    //================================================================
	virtual IMcRasterMapLayer* CastToRasterMapLayer() = 0;

    //================================================================
    // Method Name: CastToNativeRasterMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcNativeRasterMapLayer*
    /// 
    /// \return
    ///     - #IMcNativeRasterMapLayer*
    //================================================================
	virtual IMcNativeRasterMapLayer* CastToNativeRasterMapLayer() = 0;

    //================================================================
    // Method Name: CastToNativeServerRasterMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcNativeServerRasterMapLayer*
    /// 
    /// \return
    ///     - #IMcNativeServerRasterMapLayer*
    //================================================================
	virtual IMcNativeServerRasterMapLayer* CastToNativeServerRasterMapLayer() = 0;

    //================================================================
    // Method Name: CastToRawRasterMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcRawRasterMapLayer*
    /// 
    /// \return
    ///     - #IMcRawRasterMapLayer*
    //================================================================
	virtual IMcRawRasterMapLayer* CastToRawRasterMapLayer() = 0;

	//================================================================
	// Method Name: CastToWebServiceDtmMapLayer(...)
	//----------------------------------------------------------------
	/// Casts the #IMcMapLayer* To #IMcWebServiceDtmMapLayer*
	/// 
	/// \return
	///     - #IMcWebServiceDtmMapLayer*
	//================================================================
	virtual IMcWebServiceDtmMapLayer* CastToWebServiceDtmMapLayer() = 0;

	//================================================================
	// Method Name: CastToWebServiceRasterMapLayer(...)
	//----------------------------------------------------------------
	/// Casts the #IMcMapLayer* To #IMcWebServiceRasterMapLayer*
	/// 
	/// \return
	///     - #IMcWebServiceRasterMapLayer*
	//================================================================
	virtual IMcWebServiceRasterMapLayer* CastToWebServiceRasterMapLayer() = 0;

    //================================================================
    // Method Name: CastToVectorMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcVectorMapLayer*
    /// 
    /// \return
    ///     - #IMcVectorMapLayer*
    //================================================================
	virtual IMcVectorMapLayer* CastToVectorMapLayer() = 0;

    //================================================================
    // Method Name: CastToNativeVectorMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcNativeVectorMapLayer*
    /// 
    /// \return
    ///     - #IMcNativeVectorMapLayer*
    //================================================================
	virtual IMcNativeVectorMapLayer* CastToNativeVectorMapLayer() = 0;

    //================================================================
    // Method Name: CastToNativeServerVectorMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcNativeServerVectorMapLayer*
    /// 
    /// \return
    ///     - #IMcNativeServerVectorMapLayer*
    //================================================================
	virtual IMcNativeServerVectorMapLayer* CastToNativeServerVectorMapLayer() = 0;

    //================================================================
    // Method Name: CastToRawVectorMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcRawVectorMapLayer*
    /// 
    /// \return
    ///     - #IMcRawVectorMapLayer*
    //================================================================
	virtual IMcRawVectorMapLayer* CastToRawVectorMapLayer() = 0;

	//================================================================
	// Method Name: CastToHeatMapLayer(...)
	//----------------------------------------------------------------
	/// Casts the #IMcMapLayer* To #IMcHeatMapLayer*
	/// 
	/// \return
	///     - #IMcHeatMapLayer*
	//================================================================
	virtual IMcHeatMapLayer* CastToHeatMapLayer() = 0;

	//================================================================
	// Method Name: CastToNativeHeatMapLayer(...)
	//----------------------------------------------------------------
	/// Casts the #IMcMapLayer* To #IMcNativeHeatMapLayer*
	/// 
	/// \return
	///     - #IMcNativeHeatMapLayer*
	//================================================================
	virtual IMcNativeHeatMapLayer* CastToNativeHeatMapLayer() = 0;

	//================================================================
	// Method Name: CastToCodeMapLayer(...)
	//----------------------------------------------------------------
	/// Casts the #IMcMapLayer* To #IMcCodeMapLayer*
	/// 
	/// \return
	///     - #IMcCodeMapLayer*
	//================================================================
	virtual IMcCodeMapLayer* CastToCodeMapLayer() = 0;

	//================================================================
	// Method Name: CastToMaterialMapLayer(...)
	//----------------------------------------------------------------
	/// Casts the #IMcMapLayer* To #IMcMaterialMapLayer*
	/// 
	/// \return
	///     - #IMcMaterialMapLayer*
	//================================================================
	virtual IMcMaterialMapLayer* CastToMaterialMapLayer() = 0;

	//================================================================
	// Method Name: CastToRawMaterialMapLayer(...)
	//----------------------------------------------------------------
	/// Casts the #IMcMapLayer* To #IMcRawMaterialMapLayer*
	/// 
	/// \return
	///     - #IMcRawMaterialMapLayer*
	//================================================================
	virtual IMcRawMaterialMapLayer* CastToRawMaterialMapLayer() = 0;

	//================================================================
	// Method Name: CastToNativeMaterialMapLayer(...)
	//----------------------------------------------------------------
	/// Casts the #IMcMapLayer* To #IMcNativeMaterialMapLayer*
	/// 
	/// \return
	///     - #IMcNativeMaterialMapLayer*
	//================================================================
	virtual IMcNativeMaterialMapLayer* CastToNativeMaterialMapLayer() = 0;

	//================================================================
	// Method Name: CastToNativeServerMaterialMapLayer(...)
	//----------------------------------------------------------------
	/// Casts the #IMcMapLayer* To #IMcNativeServerMaterialMapLayer*
	/// 
	/// \return
	///     - #IMcNativeServerMaterialMapLayer*
	//================================================================
	virtual IMcNativeServerMaterialMapLayer* CastToNativeServerMaterialMapLayer() = 0;

	//================================================================
	// Method Name: CastToTraversabilityMapLayer(...)
	//----------------------------------------------------------------
	/// Casts the #IMcMapLayer* To #IMcTraversabilityMapLayer*
	/// 
	/// \return
	///     - #IMcTraversabilityMapLayer*
	//================================================================
	virtual IMcTraversabilityMapLayer* CastToTraversabilityMapLayer() = 0;

	//================================================================
	// Method Name: CastToRawTraversabilityMapLayer(...)
	//----------------------------------------------------------------
	/// Casts the #IMcMapLayer* To #IMcRawTraversabilityMapLayer*
	/// 
	/// \return
	///     - #IMcRawTraversabilityMapLayer*
	//================================================================
	virtual IMcRawTraversabilityMapLayer* CastToRawTraversabilityMapLayer() = 0;

	//================================================================
	// Method Name: CastToNativeTraversabilityMapLayer(...)
	//----------------------------------------------------------------
	/// Casts the #IMcMapLayer* To #IMcNativeTraversabilityMapLayer*
	/// 
	/// \return
	///     - #IMcNativeTraversabilityMapLayer*
	//================================================================
	virtual IMcNativeTraversabilityMapLayer* CastToNativeTraversabilityMapLayer() = 0;

	//================================================================
	// Method Name: CastToNativeServerTraversabilityapLayer(...)
	//----------------------------------------------------------------
	/// Casts the #IMcMapLayer* To #IMcNativeServerTraversabilityMapLayer*
	/// 
	/// \return
	///     - #IMcNativeServerTraversabilityMapLayer*
	//================================================================
	virtual IMcNativeServerTraversabilityMapLayer* CastToNativeServerTraversabilityMapLayer() = 0;

	//================================================================
   // Method Name: CastToVectorBasedMapLayer(...)
   //----------------------------------------------------------------
   /// Casts the #IMcMapLayer* To #IMcVectorBasedMapLayer*
   /// 
   /// \return
   ///     - #IMcVectorBasedMapLayer*
   //================================================================
	virtual IMcVectorBasedMapLayer* CastToVectorBasedMapLayer() = 0;

    //================================================================
    // Method Name: CastToStaticObjectsMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcStaticObjectsMapLayer*
    /// 
    /// \return
    ///     - #IMcStaticObjectsMapLayer*
    //================================================================
	virtual IMcStaticObjectsMapLayer* CastToStaticObjectsMapLayer() = 0;

    //================================================================
    // Method Name: CastTo3DModelMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMc3DModelMapLayer*
    /// 
    /// \return
    ///     - #IMc3DModelMapLayer*
    //================================================================
	virtual IMc3DModelMapLayer* CastTo3DModelMapLayer() = 0;

    //================================================================
    // Method Name: CastToNative3DModelMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcNative3DModelMapLayer*
    /// 
    /// \return
    ///     - #IMcNative3DModelMapLayer*
    //================================================================
	virtual IMcNative3DModelMapLayer* CastToNative3DModelMapLayer() = 0;

    //================================================================
    // Method Name: CastToNativeServer3DModelMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcNativeServer3DModelMapLayer*
    /// 
    /// \return
    ///     - #IMcNativeServer3DModelMapLayer*
    //================================================================
	virtual IMcNativeServer3DModelMapLayer* CastToNativeServer3DModelMapLayer() = 0;

    //================================================================
    // Method Name: CastToRaw3DModelMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcRaw3DModelMapLayer*
    /// 
    /// \return
    ///     - #IMcRaw3DModelMapLayer*
    //================================================================
	virtual IMcRaw3DModelMapLayer* CastToRaw3DModelMapLayer() = 0;

    //================================================================
    // Method Name: CastToVector3DExtrusionMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcVector3DExtrusionMapLayer*
    /// 
    /// \return
    ///     - #IMcVector3DExtrusionMapLayer*
    //================================================================
	virtual IMcVector3DExtrusionMapLayer* CastToVector3DExtrusionMapLayer() = 0;

    //================================================================
    // Method Name: CastToNativeVector3DExtrusionMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcNativeVector3DExtrusionMapLayer*
    /// 
    /// \return
    ///     - #IMcNativeVector3DExtrusionMapLayer*
    //================================================================
	virtual IMcNativeVector3DExtrusionMapLayer* CastToNativeVector3DExtrusionMapLayer() = 0;

    //================================================================
    // Method Name: CastToNativeServerVector3DExtrusionMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcNativeServerVector3DExtrusionMapLayer*
    /// 
    /// \return
    ///     - #IMcNativeServerVector3DExtrusionMapLayer*
    //================================================================
	virtual IMcNativeServerVector3DExtrusionMapLayer* CastToNativeServerVector3DExtrusionMapLayer() = 0;

    //================================================================
    // Method Name: CastToRawVector3DExtrusionMapLayer(...)
    //----------------------------------------------------------------
    /// Casts the #IMcMapLayer* To #IMcRawVector3DExtrusionMapLayer*
    /// 
    /// \return
    ///     - #IMcRawVector3DExtrusionMapLayer*
    //================================================================
	virtual IMcRawVector3DExtrusionMapLayer* CastToRawVector3DExtrusionMapLayer() = 0;

	//@}
};
