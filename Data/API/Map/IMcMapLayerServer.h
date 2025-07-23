#pragma once

//===========================================================================
/// \file IMcMapLayerServer.h
/// Interface for Map Layer Server
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcBase.h"
#include "CMcDataArray.h"
#include "Map/IMcMapLayer.h"
#include "Production/IMcMapProduction.h"

class IMcOverlayManager;
class IMcSpatialQueries;
class IMcRawVector3DExtrusionMapLayer;

#define MAPCORE_HTTP_SERVER_COMMAND_DELIMETER '|'
#define MAPCORE_HTTP_SERVER_UNKNOWN_LAYER_VERSION "UnknownVer"

#define MAPCORE_HTTP_SERVER_INIT_SESSION_CMD "InitSession"
#define MAPCORE_HTTP_SERVER_GET_LAYER_VERSION_CMD "GetLayerVersion"
#define MAPCORE_HTTP_SERVER_GET_INFO_CMD "GetInfo"
#define MAPCORE_HTTP_SERVER_GET_FILE_CMD "GetFile"
#define MAPCORE_HTTP_SERVER_GET_TILE_CMD "GetTile"
#define MAPCORE_HTTP_SERVER_GET_CAPABILITIES_CMD "GetCapabilities"

#define MAPCORE_HTTP_SERVER_GET_VECTOR_ITEM_POINTS_CMD "GetVectorItemPoints"
#define MAPCORE_HTTP_SERVER_GET_SCAN_EXTENDED_DATA_CMD "GetScanExtendedData"
#define MAPCORE_HTTP_SERVER_GET_VECTOR_ITEM_FIELD_VAL_CMD "GetVectorItemFieldVal"
#define MAPCORE_HTTP_SERVER_GET_FIELD_UNIQUE_VALUES_CMD "GetFieldUniqueValues"
#define MAPCORE_HTTP_SERVER_GET_VECTOR_QUERY_CMD "GetVectorQuery"

#define MAPCORE_HTTP_SERVER_GET_TERRAIN_HEIGHT_CMD "GetTerrainHeight"
#define MAPCORE_HTTP_SERVER_GET_TERRAIN_HEIGHT_ALONG_LINE_CMD "GetTerrainHeightAlongLine"
#define MAPCORE_HTTP_SERVER_GET_EXTREME_HEIGHT_POINTS_IN_POLY_CMD "GetExtremeHeightPointsInPolygon"
#define MAPCORE_HTTP_SERVER_GET_TERRAIN_HEIGHT_MATRIX_CMD "GetTerrainHeightMatrix"
#define MAPCORE_HTTP_SERVER_GET_TERRAIN_ANGLES_CMD "GetTerrainAngles"
#define MAPCORE_HTTP_SERVER_GET_LINE_OF_SIGHT_CMD "GetLineOfSight"
#define MAPCORE_HTTP_SERVER_GET_POINT_VISIBILITY_CMD "GetPointVisibility"
#define MAPCORE_HTTP_SERVER_GET_AREA_OF_SIGHT_CMD "GetAreaOfSight"
#define MAPCORE_HTTP_SERVER_GET_BEST_SCOUTERS_LOC_IN_ELLIPSE_CMD "GetBestScoutersLocationsInEllipse"
#define MAPCORE_HTTP_SERVER_GET_LOC_FROM_TWO_DIST_AND_AZIMUTH_CMD "GetLocationFromTwoDistancesAndAzimuth"
#define MAPCORE_HTTP_SERVER_GET_RAY_INTERSECTION_CMD "GetRayIntersection"
#define MAPCORE_HTTP_SERVER_GET_RAY_INTERSECTION_TARGETS_CMD "GetRayIntersectionTargets"

#define MAPCORE_HTTP_SERVER_GET_DTM_LAYER_TILE_GEOMETRY_BY_KEY_CMD "GetDtmLayerTileGeometryByKey"
#define MAPCORE_HTTP_SERVER_GET_RASTER_LAYER_TILE_BITMAP_BY_KEY_CMD "GetRasterLayerTileBitmapByKey"
#define MAPCORE_HTTP_SERVER_GET_RASTER_LAYER_COLOR_BY_POINT_CMD "GetRasterLayerColorByPoint"
#define MAPCORE_HTTP_SERVER_GET_TRAVERSABILITY_ALONG_LINE_CMD "GetTraversabilityAlongLine"

#define MAPCORE_HTTP_SERVER_LAYER_ID_NOT_FOUND_ERROR	1
#define MAPCORE_HTTP_SERVER_DIFF_LAYER_VERSION_ERROR	2
#define MAPCORE_HTTP_SERVER_LAYER_INVALID_PARAMS_ERROR	3
#define MAPCORE_HTTP_SERVER_LAYER_TYPE_MISMATCH_ERROR	4
#define MAPCORE_HTTP_SERVER_LAYER_INTERNAL_ERROR		5
#define MAPCORE_HTTP_SERVER_LAYER_NOT_SUPPORTED_ERROR	6
#define MAPCORE_HTTP_SERVER_LAYER_TILE_NOT_FOUND_ERROR	7
#define MAPCORE_HTTP_SERVER_AUTHENTICATION_REQUIRED_ERROR	8
#define MAPCORE_HTTP_SERVER_UNAUTHENTICATED_ERROR			9
#define MAPCORE_HTTP_SERVER_AUTHENTICATION_EXPIRED_ERROR	10
#define MAPCORE_HTTP_SERVER_UNAUTHORIZED_ERROR				11

//===========================================================================
// Interface Name: IMcMapLayerServer
//---------------------------------------------------------------------------
/// Interface for Map Layer Server
//===========================================================================
class IMcMapLayerServer : virtual public IMcBase
{
protected:
    virtual ~IMcMapLayerServer() {}

public:

	struct SOutVectorLayerData
	{
		MAPTERRAIN_API SOutVectorLayerData(IMcRawVectorMapLayer *_pRawLayer = NULL, PCSTR _sName = NULL, PCSTR _sXml = NULL, PCSTR _sDesc = NULL);

		CMcString sName;
		CMcString sXml;
		CMcString sDesc; // currently supported only for s57 layers...

		IMcRawVectorMapLayer *pRawLayer;
	};

	/// Layer info needed for generation of Capabilities XML
	struct SLayerInfo
	{
		IMcMapLayer	*pLayer;		///< The layer itself.
		PCSTR		strID;			///< The layer's unique ID to be used by a client to connect to the layer.
		PCSTR		strTitle;		///< The layer's title (display name) that can be used by a client.
		PCSTR		strGroup;		///< The layer's group that can be used by a client.
		int			nDrawPriority;	///< The layer's draw priority that can be used by a client.
	};

	class ICallback
	{
	public:
		virtual ~ICallback() {}

		//==============================================================================
		// Method Name: OnMainThreadPendingCalculationsNeeded()
		//------------------------------------------------------------------------------
		/// Called by ProcessSpatialQueryRequest() when pending calculations are to be performed on the main thread; the main thread should be awoken 
		/// here to perform IMcMapDevice::PerformPendingCalculations() in its context.
		//==============================================================================
		virtual void OnMainThreadPendingCalculationsNeeded() = 0;
	};

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates Map Layer Server interface
	///
	/// \param[out]	ppServer					Map Layer Server interface created.
	/// \param[in]	pMapLayer					The map layer to use for requesting data.
	/// \param[in]  strLayerID					The layer's unique ID to be used by a client to connect to the layer.
	/// \param[in]  strLayerVersion				The layer version's unique string; should be updated every time the layer is updated.
	/// \param[in]  pRawVectorOverlayManager	The overlay manager (relevant only for raw vector layers, should be created with the layer's target 
	///											coordinate system; for other layer types pass NULL).
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode Create(
		IMcMapLayerServer **ppServer, IMcMapLayer *pMapLayer, 
		PCSTR strLayerID, PCSTR strLayerVersion,
		IMcOverlayManager *pRawVectorOverlayManager);

	//@}

	/// \name Cache Configuration
	//@{

	//==============================================================================
	// Method Name: SetCacheParams()
	//------------------------------------------------------------------------------
	/// Sets the mutual cache parameters for all server instances
	///
	/// \param[in]	uMemoryCacheSizeMB		The memory cache size in MB.
	/// \param[in]	uDiskCacheSizeMB		The disk cache size in MB.
	/// \param[in]	strDiskCacheFolder		The path to a folder to use for the disk cache; 
	///										will not be considered if already set by a previous call to SetCacheParams().
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode SetCacheParams(UINT uMemoryCacheSizeMB, UINT uDiskCacheSizeMB, PCSTR strDiskCacheFolder);

	//==============================================================================
	// Method Name: GetDiskCacheMapLayers()
	//------------------------------------------------------------------------------
	/// Retrieves IDs (previously set in Create()) of all the layers that currently exist in the disk cache.
	///
	/// \param[in]  pastrLayerIDs	The array of IDs (previously set in Create()) of all the layers that currently exist in the disk cache.
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode GetDiskCacheMapLayers(CMcDataArray<CMcString> *pastrLayerIDs);

	//==============================================================================
	// Method Name: RemoveMapLayerFromDiskCache()
	//------------------------------------------------------------------------------
	/// Removes the specified map layer or all existing layers from the disk cache.
	///
	/// \param[in]  strLayerID			The ID of the layer (previously set in Create()) to be removed from the disk cache;
	///										pass NULL or empty string to remove all existing layers.
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode RemoveMapLayerFromDiskCache(PCSTR strLayerID);

	//@}

	/// \name Layer Server
	//@{

	//==============================================================================
	// Method Name: GetMapLayer()
	//------------------------------------------------------------------------------
	/// Retrieves the map layer used for requesting data.
	///
	/// \param[out]	ppMapLayer		The map layer used for requesting data.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetMapLayer(IMcMapLayer **ppMapLayer) const = 0;

	//==============================================================================
	// Method Name: Invalidate()
	//------------------------------------------------------------------------------
	/// Notifies the Map Layer Server interface that it will be deleted soon and should stop using its disk cache.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Invalidate() = 0;

	//==============================================================================
	// Method Name: ProcessClientRequest()
	//------------------------------------------------------------------------------
	/// Processes a request received from a client.
	/// 
	/// Can be called from any thread.
	///
	/// \param[in]	strCommand				The command received from a client.
	/// \param[out]	paResult				The memory buffer with encoded result to be sent to a client
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ProcessClientRequest(PCSTR strCommand, CMcDataArray<BYTE> *paResult) = 0;

	//==============================================================================
	// Method Name: GetRasterTileImage()
	//------------------------------------------------------------------------------
	/// Retrieves the tile's image as PNG or JPEG buffer
	///
	/// \param[in]	TileKey					The Key (unique ID) of the tile in interest
	/// \param[in]	bIsPng					Whether PNG or JPEG is needed
	/// \param[out]	paBuffer				The image as PNG or JPEG buffer
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRasterTileImage(const IMcMapLayer::SLayerTileKey &TileKey, bool bIsPng, CMcDataArray<BYTE> *paBuffer, bool bIfNotFoundReturnEmptyTile = false) = 0;

	//==============================================================================
	// Method Name: ProcessSpatialQueryRequest()
	//------------------------------------------------------------------------------
	/// Processes a spatial query request received from a client.
	/// 
	/// Can be called from any thread.
	///
	/// \param[in]	pSpatialQueries			The interface for the spatial query.
	/// \param[in]  pCallback				The callback used to perform thread synchronization tasks.
	/// \param[in]	strCommand				The command received from a client.
	/// \param[out]	paResult				The memory buffer with encoded result to be sent to a client
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode ProcessSpatialQueryRequest(IMcSpatialQueries *pSpatialQueries, ICallback *pCallback, PCSTR strCommand, 
		CMcDataArray<BYTE> *paResult);

	//==============================================================================
	// Method Name: ProcessClientRequestError()
	//------------------------------------------------------------------------------
	/// Processes a request received from a client in case of an error
	/// 
	/// Can be called from any thread.
	///
	/// \param[in]	eErrorCode				The error code
	/// \param[out]	paResult				The memory buffer with encoded result to be sent to a client
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode ProcessClientRequestError(BYTE eErrorCode, CMcDataArray<BYTE> *paResult);

	//==============================================================================
	// Method Name: GenerateCapabilitiesXML()
	//------------------------------------------------------------------------------
	/// Generates capabilities XML in WMTS-capabilities style.
	///
	/// Can be called from any thread.
	///
	/// \param[in]	aLayers						The array of layers along with their parameters
	/// \param[in]	uNumLayers					The number of layers in the above array
	/// \param[in]	strServiceMetadataURL		The service metadata URL to be written into XML
	/// \param[in]	bOldStaticObjectsSupport	Whether to generate capabilities XML with vector-3D-extrusion/3D-model layers shown as "StaticObjects" 
	///											(for clients of versions older than 7.11.4.0)
	/// \param[out]	paCapabilitiesXMLBuffer		The capabilities XML generated (as memory buffer)
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode GenerateCapabilitiesXML(const SLayerInfo aLayers[], UINT uNumLayers, PCSTR strServiceMetadataURL,
		bool bOldStaticObjectsSupport, CMcDataArray<BYTE> *paCapabilitiesXMLBuffer);

	//==============================================================================
	// Method Name: ParseCapabilitiesXML()
	//------------------------------------------------------------------------------
	/// Retrieves the parsed capabilities XML.
	///
	/// Can be called from any thread.
	///
	/// \param[in] pBuffer						The capabilities XML buffer
	/// \param[in] nBufferLen					The buffer's length
	/// \param[in] strServerURL					The server's capabilities request
	/// \param[in] eWebMapServiceType			The capabilities result format
	/// \param[in] pAsyncOperationCallback		The asynchronous callback
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode ParseCapabilitiesXML(
		const BYTE *pBuffer, int nBufferLen,
		PCSTR strServerURL, IMcMapLayer::EWebMapServiceType eWebMapServiceType,
		IMcMapLayer::IAsyncOperationCallback *pAsyncOperationCallback);

	//@}

	virtual IMcErrors::ECode PrepareGetInfo() = 0;

	static SCENEMANAGER_API bool IsValidNativeServerMapLayer(IMcMapLayer *pLayer);

	static SCENEMANAGER_API const std::map<std::string, IMcOverlayManager::ESavingVersionCompatibility>& GetVersionsMap();
	static SCENEMANAGER_API bool GetVersionEnumByString(const std::string& sVersionString, IMcOverlayManager::ESavingVersionCompatibility* peVersion);

	static SCENEMANAGER_API IMcErrors::ECode PrepareRawVectorLayer(
		PCSTR strDir, PCSTR strDataSource, bool bSuffixWithLayerName, IMcOverlayManager *pOverlayManager,
		IMcGridCoordinateSystem *pSourceCoordinateSystem,
		IMcGridCoordinateSystem *pTargetCoordinateSystem,
		const IMcMapLayer::STilingScheme *pTilingScheme,
		PCSTR strPointTextureFile, IMcRawVectorMapLayer::EAutoStylingType eAutoStylingType, PCSTR strCustomStylingFolder, IMcRawVectorMapLayer::SInternalStylingParams *pStylingParams,
		float fMinScale /*= 0.0f*/, float fMaxScale /*= FLT_MAX*/,
		CMcDataArray <SOutVectorLayerData/*, const SOutVectorLayerData&*/> *pOutLayers,
		IMcProgressCallback *pProgressCallback = NULL);

	static SCENEMANAGER_API IMcErrors::ECode ConvertVectorLayer(
		IMcRawVectorMapLayer *pLayer,
		const IMcMapProduction::SVectorConvertParams& ConvertParams,
		IMcMapDevice *pMapDevice = NULL, IMcProgressCallback *pProgressCallback = NULL);
	static SCENEMANAGER_API IMcErrors::ECode ConvertVectorLayer(
		const IMcMapProduction::SVectorConvertParams& ConvertParams,
		IMcOverlayManager *pOverlayManager, IMcOverlay *pOverlay,
		IMcMapDevice *pMapDevice = NULL, IMcProgressCallback *pProgressCallback = NULL);

	static SCENEMANAGER_API IMcErrors::ECode Convert3DModelLayer(const IMcMapProduction::S3DModelConvertParams &ConvertParams,
		IMcMapDevice *pMapDevice = NULL, IMcProgressCallback *pProgressCallback = NULL);
	static  SCENEMANAGER_API IMcErrors::ECode ConvertVector3DExtrusionLayer(const IMcMapProduction::SVector3DExtrusionConvertParams &ConvertParams,
		IMcMapDevice *pMapDevice = NULL, IMcProgressCallback *pProgressCallback = NULL);

	static SCENEMANAGER_API void SetMatchedDtmLayer(IMcRawVector3DExtrusionMapLayer *pLayer, const std::string &strLoweredDtmLayerId);

	static SCENEMANAGER_API IMcErrors::ECode BuildVectorMapLayerSpatialIndex(const std::string& strIndexDatabasePath, const std::string& layerId,
		const std::string& dataSourcePath, IMcMapLayer::EComponentType fileOrFolder, const std::vector<std::string>& astrFieldNames, bool isLastLayer = true);

	static SCENEMANAGER_API bool RemoveDir(PCSTR strDir);
	static SCENEMANAGER_API void PathSlashesToCanonical(PSTR strPath);
	static SCENEMANAGER_API bool CanonicalizePath(char strCanonicalPath[MAX_PATH], const char strPath[MAX_PATH]);
	static SCENEMANAGER_API bool GetRelativePath(PCSTR strAbsoluteBaseDir, PCSTR strAbsolutePath, char strRelativePath[MAX_PATH]);
	static SCENEMANAGER_API bool GetAutoStylingXmlDir(IMcRawVectorMapLayer::EAutoStylingType eAutoStylingType, PCSTR strCustomStylingFolder, 
		char strAutoStylingXmlBaseDir[MAX_PATH]);
};
