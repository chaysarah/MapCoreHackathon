#pragma once

//===========================================================================
/// \file IMcMapDevice.h
/// Interface for a device (video adapter) used to render a map
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcBase.h"
#include "IMcMapLayer.h"
#include "McCommonTypes.h"

/// A special key to be used in the **aRequestParams** list passed in IMcMapDevice::GetWebServerLayers(),
/// for IMcMapLayer::EWMS_WCS service. The value of this key is the Http request body.
const PCSTR MC_CSW_QUERY_BODY_KEY = "McCswQueryBody";

//===========================================================================
// Interface Name: IMcMapCamera
//---------------------------------------------------------------------------
/// Interface for a rendering device (video adapter) used in map viewport creation
//===========================================================================
class IMcMapDevice : virtual public IMcBase
{
protected:
    virtual ~IMcMapDevice() {}

public:

	/// MapCore logging level	
	enum ELoggingLevel
	{
		ELL_NONE	= 0,	///< no logging
		ELL_LOW		= 1,	///< only critical messages will be logged
		ELL_MEDIUM	= 2,	///< critical and regular messages will be logged
		ELL_HIGH	= 3		///< all messages will be logged
	};

	/// anti-aliasing level with optional quality hint
	enum EAntiAliasingLevel
	{
		EAAL_NONE		= 0,	///< no anti-aliasing
		EAAL_1			= 2,	///< 1-sample anti-aliasing level
		EAAL_2			= 4,	///< 2-samples anti-aliasing level
		EAAL_3			= 6,	///< 3-samples anti-aliasing level
		EAAL_4			= 8,	///< 4-samples anti-aliasing level
		EAAL_5			= 10,	///< 5-samples anti-aliasing level
		EAAL_6			= 12,	///< 6-samples anti-aliasing level
		EAAL_7			= 14,	///< 7-samples anti-aliasing level
		EAAL_8			= 16,	///< 8-samples anti-aliasing level
		EAAL_8_QUALITY	= 17,	///< 8-samples anti-aliasing level with quality hint
		EAAL_9			= 18,	///< 9-samples anti-aliasing level
		EAAL_9_QUALITY	= 19,	///< 9-samples anti-aliasing level with quality hint
		EAAL_10			= 20,	///< 10-samples anti-aliasing level
		EAAL_10_QUALITY	= 21,	///< 10-samples anti-aliasing level with quality hint
		EAAL_11			= 22,	///< 11-samples anti-aliasing level
		EAAL_11_QUALITY	= 23,	///< 11-samples anti-aliasing level with quality hint
		EAAL_12			= 24,	///< 12-samples anti-aliasing level
		EAAL_12_QUALITY	= 25,	///< 12-samples anti-aliasing level with quality hint
		EAAL_13			= 26,	///< 13-samples anti-aliasing level
		EAAL_13_QUALITY	= 27,	///< 13-samples anti-aliasing level with quality hint
		EAAL_14			= 28,	///< 14-samples anti-aliasing level
		EAAL_14_QUALITY	= 29,	///< 14-samples anti-aliasing level with quality hint
		EAAL_15			= 30,	///< 15-samples anti-aliasing level
		EAAL_15_QUALITY	= 31,	///< 15-samples anti-aliasing level with quality hint
		EAAL_16			= 32,	///< 16-samples anti-aliasing level
		EAAL_16_QUALITY	= 33 	///< 16-samples anti-aliasing level with quality hint
	};

	/// quality of objects attached to terrain and lite vector layer objects; 
	/// lower quality means usage of lower-resolution textures that will reduce memory usage
	enum ETerrainObjectsQuality
	{
		ETOQ_HIGH,			///< high quality (512 x 512 textures)
		ETOQ_MEDIUM,		///< medium quality (256 x 256 textures) - the default
		ETOQ_LOW,			///< low quality (128 x 128 textures)
		ETOQ_EXTRA_LOW		///< extra low quality (64 x 64 textures) - not recommended
	};

	/// precision of static-objects visibility calculation in sight query; 
	/// lower precision means usage of lower-resolution texture that will improve performance and reduce memory usage
	enum EStaticObjectsVisibilityQueryPrecision
	{
		ESOVQP_HIGH,		///< high precision (based on 1024 x 1024 texture)
		ESOVQP_MEDIUM,		///< medium precision (based on 512 x 512 texture) - the default
		ESOVQP_LOW,			///< low precision (based on 256 x 256 texture)
		ESOVQP_EXTRA_LOW	///< extra low precision (based on 128 x 128 texture) - not recommended
	};

	/// rendering system API to use
	enum ERenderingSystem
	{
		ERS_AUTO_SELECT		=  0,	///< Use rendering system listed in plugins config file (in Windows, Direct3D 9 will be preferred)
		ERS_NONE			=  1,	///< No rendering system (neither map viewports nor GPU-based calculations will be used)
		ERS_DIRECT_3D_9		= 10,	///< Direct3D 9 (DirectX 9) rendering system
		ERS_DIRECT_3D_9EX	= 11,	///< Direct3D 9Ex (DirectX 9Ex) rendering system (Windows Vista and newer)
		ERS_DIRECT_3D_11    = 12,	///< Direct3D 11 rendering system 
		ERS_OPEN_GL			= 20,	///< OpenGL rendering system
		ERS_OPEN_GL_3PLUS   = 21,	///< Modern OpenGL rendering system 
		ERS_OPEN_GLES       = 22	///< OpenGL-ES rendering system
	};

	/// Resource location type (meaning of resource location string)
	enum EResourceLocationType
	{
		ERLT_FOLDER,			///< resource location is a path of a file-system folder without sub-folders
		ERLT_FOLDER_RECURSIVE,	///< resource location is a path of a file-system folder with sub-folders
		ERLT_ZIP_FILE,			///< resource location is a path of a zip file without its folders
		ERLT_ZIP_FILE_RECURSIVE	///< resource location is a path of a zip file with all its folders
	};

    /// GPU vendors
	enum EGpuVendor
	{
		EGV_UNKNOWN = 0,				///< Unknown device
		EGV_NVIDIA,						///< NVIDIA
		EGV_AMD,						///< AMD
		EGV_INTEL,						///< Intel
		EGV_IMAGINATION_TECHNOLOGIES,	///< Information technologies
		EGV_APPLE,						///< Apple Software Renderer
		EGV_NOKIA,						///< Nokia
		EGV_MS_SOFTWARE,				///< Microsoft software device
		EGV_MS_WARP,					///< Microsoft WARP (Windows Advanced Rasterization Platform) software device
		EGV_ARM,						///< Mali chipset
		EGV_QUALCOMM,					///< Qualcomm
		EGV_MOZILLA,					///< WebGL on Mozilla/Firefox based browser
		EGV_WEBKIT						///< WebGL on WebKit/Chrome based browser
	};

	/// Types of Smart Reality database queries
	enum ESmartRealityQuery
	{
		ESRQ_BUILDING_HISTORY			///< querying building history
	};

	/// MapCore rendering and spatial queries engine initialization parameters; 
	/// considered only when creating the first device
	struct SInitParams
	{
		/// number of background threads to use for loading map layers (0 means loading map layers in the main MapCore's thread); 
		/// the default is `UINT_MAX` (means the number of hardware thread contexts available minus 1)
		UINT					uNumBackgroundThreads;

		/// MapCore logging level (messages according to the specified level will be sent to the 
		/// application's debugger and written to 'MapCore.log' file); the default is #ELL_LOW
		ELoggingLevel			eLoggingLevel;

		/// whether to open graphics configuration window (for advanced users only)
		bool					bOpenConfigWindow;

		/// directory of plugins.cfg, resources.cfg, fxplugin.cfg files and of graphics.cfg file (if exists);
		/// NULL or empty means current working directory
		PCSTR					strConfigFilesDirectory;

		/// directory for device caps files used in device creation: the files are written when device caps are available, 
		/// are read when device caps are not available (for example running before/without log-in)
		/// NULL or empty means do not write/read device caps
		PCSTR					strDeviceCapsFilesDirectory;

		/// directory to place MapCore.log file into; NULL or empty means current working directory
		PCSTR					strLogFileDirectory;

		/// optional prefix to be added to all paths in resources.cfg file
		PCSTR					strPrefixForPathsInResourceFile;

		/// anti-aliasing level with optional quality hint for the whole viewport; 
		/// not applicable for non-window viewports (for example WPF); 
		/// higher level improves visual quality but affects performance; the default is #EAAL_NONE
		EAntiAliasingLevel		eViewportAntiAliasingLevel;

		/// anti-aliasing level with optional quality hint for objects attached to terrain and lite vector 
		/// layer objects; higher level improves visual quality but affects performance; the default is #EAAL_NONE
		EAntiAliasingLevel		eTerrainObjectsAntiAliasingLevel;

		/// quality of objects attached to terrain and lite vector layer objects; lower quality means usage 
		/// of lower-resolution textures that will reduce memory usage; the default is #ETOQ_MEDIUM
		ETerrainObjectsQuality	eTerrainObjectsQuality;

		/// precision of visible static-objects visibility calculation in sight query; lower precision means usage of 
		/// lower-resolution texture that will improve performance and reduce memory usage; the default is #ESOVQP_MEDIUM
		EStaticObjectsVisibilityQueryPrecision
								eStaticObjectsVisibilityQueryPrecision;

		/// precision of DTM used for DTM visualization (shading); possible values are 
		/// 0 (low - as usually used in the viewport for rendering and queries), 1 (medium), 2 (high);
		/// higher precision requires loading and rendering more DTM tiles; the default is 1
		UINT					uDtmVisualizationPrecision;

		/// growth ratio of buffers of objects batches; should be greater than 1; the default is 4
		float					fObjectsBatchGrowthRatio;

		/// width and height of objects textures atlases; will be rounded to a power of 2; 
		/// the default is 1024
		UINT					uObjectsTexturesAtlasSize;

		/// whether color resolution of objects textures atlases is 16 bit (true) or 32 bit (false)
		/// the default is 32 bit (false)
		bool					bObjectsTexturesAtlas16bit;

		/// whether to disable depth buffer (if disabled: 3D viewports cannot be used, 
		/// overlay's draw priority consistency is disabled); the default is false
		bool					bDisableDepthBuffer;

		/// rendering system API to use; the default is #ERS_AUTO_SELECT
		ERenderingSystem		eRenderingSystem;

		/// whether the rendering system should be fully shader-based (programmable pipeline only) or fixed pipelines 
		/// can be used when it is possible; (programmable pipeline is to be used in devices without fixed pipeline support); 
		/// the default is false
		bool					bShaderBasedRenderingSystem;

		/// whether texture mipmaps of native raster map layer should not be loaded to save memory 
		/// (used true only in low memory computers when 3D is not used at all because it is affects 
		/// visual quality, especially in 3D); the default is false
		bool					bIgnoreRasterLayerMipmaps;

		/// whether viewports will be created in full-screen mode
		bool					bFullScreen;

		/// whether some viewports will use OpenGL's quad-buffer stereo mode
		bool					bEnableGLQuadBufferStereo;

		/// number of render targets (allocated on GPU) used for rendering objects attached to terrain and lite vector 
		/// layer objects; the size of each render target is a function of the terrain objects quality and may 
		/// reach 1MB (when using high object quality), use this parameter carefully according to the 
		/// memory limitation of the GPU; the default is 5
		UINT					uNumTerrainTileRenderTargets;

		/// whether render targets (allocated on GPU and used for rendering objects attached to terrain and lite vector 
		/// layer objects) should not be copied to system memory when possible but used directly; 
		/// use this parameter carefully: may improve performance in certain GPUs and by-pass 
		/// problems in GPUs that cannot copy to system memory, but preventing copying to system memory 
		/// requires significant increasing of \b uNumTerrainTileRenderTargets (up to several hundreds 
		/// depending on the maximal dimensions of viewports and limited by the amount of GPU memory); 
		/// the default is false
		bool					bPreferUseTerrainTileRenderTargets;

		/// whether rendering device should be initialized as multi-threaded; must be true in WPF mode 
		/// and in any case when textures returned by MapCore are used in threads other than 
		/// MapCore's main thread; otherwise false is recommended to achive better performance
		bool					bMultiThreadedDevice;

		/// whether rendering device should be initialized as multi-screen; must be true if there are 
		/// several screens (monitors) connected and different viewport windows can be opened in different screens; 
		/// otherwise false is recommended to achive better performance
		bool					bMultiScreenDevice;

		/// index of the main monitor used to initialize the rendering system; the default is UINT_MAX (system default monitor); 
		/// - for multi-screen devices it will be the monitor for rendering non-window viewports, 
		/// - for single-screen devices it should be the monitor of all viewports' windows
		UINT					uMainMonitorIndex;

		/// The initial number of vertices in each batch of objects
		UINT					uObjectsBatchInitialNumVertices;

		/// The initial number of indices in each batch of objects
		UINT					uObjectsBatchInitialNumIndices;

		/// Whether to enable dynamic enlarging of batches of objects
		bool					bEnableObjectsBatchEnlarging;

		/// Whether to align screen-size text and pictures to prevent positioning at multiples of 
		/// half a pixel (prevents blurring, but affects performance); the default is true
		bool					bAlignScreenSizeObjects;

		/// path to local folder to use for map layers local cache
		/// or NULL (the default) if the cache should not be used
		/// (can be enabled per layer; to be used for layer accessed over a network)
		PCSTR					strMapLayersLocalCacheFolder;

		/// maximum size of map layers local cache in MB;
		/// 0 (the default) means the cache is disabled, `UINT_MAX` means unlimited cache size
		UINT					uMapLayersLocalCacheSizeInMB;

		/// maximum number of retries for MapCore client's web requests; the default is 10; 0 means the retries are disabled
		UINT					uWebRequestRetryCount;

		/// maximum number of MapCore client's active web requests for map layer tiles needed for asynchronous queries; the default is 10
		UINT					uAsyncQueryTilesMaxActiveWebRequests;

		SInitParams()
			: uNumBackgroundThreads(UINT_MAX), eLoggingLevel(ELL_LOW), bOpenConfigWindow(false), 
			  strConfigFilesDirectory(NULL), strDeviceCapsFilesDirectory(NULL), strLogFileDirectory(NULL), strPrefixForPathsInResourceFile(NULL), 
			  eViewportAntiAliasingLevel(EAAL_NONE), eTerrainObjectsAntiAliasingLevel(EAAL_NONE), 
			  eTerrainObjectsQuality(ETOQ_MEDIUM), eStaticObjectsVisibilityQueryPrecision(ESOVQP_MEDIUM), 
			  uDtmVisualizationPrecision(1), fObjectsBatchGrowthRatio(4), 
			  uObjectsTexturesAtlasSize(1024), bObjectsTexturesAtlas16bit(false), 
			  bDisableDepthBuffer(false), eRenderingSystem(ERS_AUTO_SELECT), bShaderBasedRenderingSystem(false), 
			  bIgnoreRasterLayerMipmaps(false), bFullScreen(false), bEnableGLQuadBufferStereo(false), 
			  uNumTerrainTileRenderTargets(5), bPreferUseTerrainTileRenderTargets(false), 
			  bMultiThreadedDevice(false), bMultiScreenDevice(false), uMainMonitorIndex(UINT_MAX), 
			  uObjectsBatchInitialNumVertices(32), uObjectsBatchInitialNumIndices(64),
			  bEnableObjectsBatchEnlarging(true), bAlignScreenSizeObjects(true),
			  strMapLayersLocalCacheFolder(NULL), uMapLayersLocalCacheSizeInMB(0),
			  uWebRequestRetryCount(10), uAsyncQueryTilesMaxActiveWebRequests(10)
			  {}
	};

	/// GPU information
	struct SGpuInfo
	{
		EGpuVendor	eVendor;			///< vendor
		CMcString	strVendorName;		///< vendor name
		CMcString	strDeviceName;		///< GPU device name

		SGpuInfo() : eVendor(EGV_UNKNOWN) {}
	};
	
	/// Lost-device callback interface to be inherited by the user
	class ILostDeviceCallback
#ifdef __EMSCRIPTEN__
		: public IMcEmscriptenCallbackRef
#endif
	{
	public:
		virtual ~ILostDeviceCallback() {}

		//==============================================================================
		// Method Name: OnDeviceLost()
		//------------------------------------------------------------------------------
		/// Called before the DirectX device is restored after having been lost as a result of 
		/// either losing keyboard focus in a full-screen application or locking the computer.
		///
		/// When the device is lost, all memory-buffer textures created by the user with 
		/// dynamic of render-target usage are discarded.
		//==============================================================================
		virtual void OnDeviceLost() = 0;

		//==============================================================================
		// Method Name: OnDeviceRestored()
		//------------------------------------------------------------------------------
		/// Called after the DirectX device is restored after having been lost as a result of 
		/// either losing keyboard focus in a full-screen application or locking the computer.
		///
		/// When the device is lost, all memory-buffer textures created by the user with 
		/// dynamic of render-target usage are discarded. When the device is restored, 
		/// all such textures are recreated by MapCore, but their contents are lost and 
		/// they have new DirectX interface pointers. The user should fill such textures, 
		/// and if Direct3D texture pointers are used the user should ask them again by 
		/// calling IMcMemoryBufferTexture::GetDirectXTexture().
		//==============================================================================
		virtual void OnDeviceRestored() = 0;

		//==============================================================================
		// Method Name: Release()
		//------------------------------------------------------------------------------
		/// A callback that should release callback class instance.
		///
		///	Can be implemented by the user to optionally delete callback class instance when 
		/// IMcMapDevice instance is been removed.
		//==============================================================================
		virtual void Release() {}
	};

/// \name Create
//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates rendering device to be used in map viewport creation.
	/// 
	/// The first created device initializes MapCore rendering and spatial queries engine 
	/// with the specified initialization parameters (\a InitParams); 
	/// they are ignored when creating following devices.
	///
	/// \param[out]	ppDevice	device interface created	
	/// \param[in]	InitParams	MapCore rendering and spatial queries engine initialization parameters
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode Create(IMcMapDevice **ppDevice, const SInitParams &InitParams);
//@}

/// \name Version
//@{
//==============================================================================
// Method Name: GetVersion()
//------------------------------------------------------------------------------
/// Retrieves MapCore's version string
///
/// \param[out]	pstrVersionString
///
/// \return
///     - status result
//==============================================================================
static SCENEMANAGER_API IMcErrors::ECode GetVersion(PCSTR *pstrVersionString);
//@}

/// \name Lost-Device Callback
//@{
	//==============================================================================
	// Method Name: AddLostDeviceCallback()
	//------------------------------------------------------------------------------
	/// Adds a callback to be called when the DirectX device is restored after having been lost, 
	/// see ILostDeviceCallback for details.
	///
	/// \param[in]	pCallback		a callback for processing a lost-device event
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode AddLostDeviceCallback(ILostDeviceCallback *pCallback);

	//==============================================================================
	// Method Name: RemoveLostDeviceCallback()
	//------------------------------------------------------------------------------
	/// Removes a callback added by AddLostDeviceCallback().
	///
	/// \param[in]	pCallback		a callback for processing a lost-device event
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode RemoveLostDeviceCallback(ILostDeviceCallback *pCallback);
//@}

/// \name Pending Calculations
//@{
	//==============================================================================
	// Method Name: PerformPendingCalculations()
	//------------------------------------------------------------------------------
	/// Performs pending calculations redirected by MapCore to its main thread 
	/// (for GPU-based calculations called in any other thread).
	/// 
	/// Should be called constantly in MapCore's main thread.
	///
	/// \param[in]	uTimeout							timeout (in ms) for performing pending calculations; 
	///													at least one calculation is always performed; 
	///													the default is 0 (one calculation only)
	/// \param[out]	pbHasRemainingPendingCalculations	whether there are remaining pending calculations
	///													after this call ((NULL if not needed)
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode PerformPendingCalculations(UINT uTimeout = 0, 
		bool *pbHasRemainingPendingCalculations = NULL);
//@}

/// \name Resources
//@{
	//==============================================================================
	// Method Name: LoadResourceGroup()
	//------------------------------------------------------------------------------
	/// Creates and loads a group of resources from the specified locations.
	///
	/// \param[in]	strGroupName			resource group name (should not be equal to any existing group name)
	/// \param[in]	astrResourceLocations	array of locations of the resources to load; the meaning depends
	///										on \a eResourceLocationType (see #EResourceLocationType for details)
	/// \param[in]	uNumResourceLocations	number of locations in the above array
	/// \param[in]	eResourceLocationType	resource location type
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode LoadResourceGroup(PCSTR strGroupName, 
		const char astrResourceLocations[][MAX_PATH], UINT uNumResourceLocations, 
		EResourceLocationType eResourceLocationType = ERLT_FOLDER);

	//==============================================================================
	// Method Name: UnloadResourceGroup()
	//------------------------------------------------------------------------------
	/// Unloads and destroys a group of resources previously loaded by LoadResourceGroup().
	///
	/// Can be called only when the resources of the group are no longer used.
	///
	/// \param[in]	strGroupName		resource group name
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode UnloadResourceGroup(PCSTR strGroupName);
//@}

/// \name Web Servers
//@{
	//==============================================================================
	// Method Name: GetWebServerLayers()
	//------------------------------------------------------------------------------
	/// Retrieves asynchronously the server's layers by sending a capabilities request (coverage or capabilities for WCS) to the server and 
	/// parsing the response.
	///
	/// \param[in]	strServerURL				the server's URL; either server-only URL (http://foo) or with addition of the capabilities request 
	///											(e.g. http://foo/WMTSCapabilities.xml, http://foo?request=GetCapabilities, http://foo?service=WMTS&request=GetCapabilities, 
	///											http://foo?service=WCS&request=GetCapabilities) or 
	///											(in case of IMcMapLayer::EWMS_WCS service only) with addition of the capabilities or coverage request 
	///											(e.g. http://foo?service=WCS&request=DescribeCoverage&version=1.0.0); in case of server-only URL, MapCore 
	///											will send the request to the server by adding the following to \p strServerURL:
	///											- "?service=WCS&request=DescribeCoverage&version=1.0.0" for IMcMapLayer::EWMS_WCS service; 
	///											- "?service=WMS&request=GetCapabilities" for IMcMapLayer::EWMS_WMS service; 
	///											- "?service=WMTS&request=GetCapabilities" for the IMcMapLayer::EWMS_WMTS and IMcMapLayer::EWMS_MAPCORE services
	///											in case of IMcMapLayer::EWMS_CSW, pass server-only URL (http://foo/csw), with or without additional parameters 
	///											(e.g http://foo/csw?token=XXX). such parameters can also be passed in **aRequestParams**.
	///											however, the request body, if needed, must be passed in **aRequestParams** with the MC_CSW_QUERY_BODY_KEY key.
	/// \param[in]	eWebMapServiceType			the web service type
	/// \param[in]	aRequestParams[]			optional list of additional parameters (as key-value pairs) to be added to the capabilities request as "key=value"
	/// \param[in]	uNumRequestParams			number of parameters in **aRequestParams**
	/// \param[in]  pAsyncOperationCallback		the asynchronous callback; the result will be returned in 
	///											IMcMapLayer::IAsyncOperationCallback::OnWebServerLayersResults()
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode GetWebServerLayers(PCSTR strServerURL, IMcMapLayer::EWebMapServiceType eWebMapServiceType, 
		const SMcKeyStringValue aRequestParams[], UINT uNumRequestParams, IMcMapLayer::IAsyncOperationCallback *pAsyncOperationCallback);

	//==============================================================================
	// Method Name: Get3DModelSmartRealityData()
	//------------------------------------------------------------------------------
	/// Retrieves asynchronously data by sending an appropriate request to Smart Reality server and parsing the response.
	///
	/// \param[in]	strSmartRealityServerURL	the server's URL
	/// \param[in]	eSmartRealityQuery			the type of Smart Reality query
	/// \param[in]	uObjectID					the UUID of Smart Reality object of a type (building, wall, window, etc.)
	///											appropriate to the query type
	/// \param[in]	pAsyncOperationCallback		the asynchronous callback; the result will be returned in 
	///											IMcMapLayer::IAsyncOperationCallback::On3DModelSmartRealityDataResults()
	/// \param[in]	bOrthometricHeights			Whether the heights of points in the server are orthometric (above the geoid / sea level) or ellipsoid heights 
	///											(above input's coordinate system's elipsoid); the default is `false`
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode Get3DModelSmartRealityData(PCSTR strSmartRealityServerURL, ESmartRealityQuery eSmartRealityQuery, const SMcVariantID &uObjectID,
		IMcMapLayer::IAsyncOperationCallback *pAsyncOperationCallback, bool bOrthometricHeights = false);
//@}

/// \name Map Layers Local Cache
//@{
	//==============================================================================
	// Method Name: RemoveMapLayersLocalCache()
	//------------------------------------------------------------------------------
	/// Removes the map layers local cache folder and stops creating/using local cache
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode RemoveMapLayersLocalCache();

	//==============================================================================
	// Method Name: RemoveMapLayerFromLocalCache()
	//------------------------------------------------------------------------------
	/// Removes the specified map layer or all existing layers from the local cache.
	///
	/// \param[in]	strMapLayerLocalCacheSubFolder		layer's sub folder in the local cache or NULL to remove all existing layers
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode RemoveMapLayerFromLocalCache(PCSTR strMapLayerLocalCacheSubFolder);

	//==============================================================================
	// Method Name: SetMapLayersLocalCacheSize()
	//------------------------------------------------------------------------------
	/// Sets the map layers local cache max size in MB.
	///
	/// \param[in]	uMapLayersLocalCacheSizeInMB	local cache max size in MB.
	///												(0 means the cache is disabled, UINT_MAX means unlimited cache size)
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode SetMapLayersLocalCacheSize(UINT uMapLayersLocalCacheSizeInMB);

	//==============================================================================
	// Method Name: GetMapLayersLocalCacheParams()
	//------------------------------------------------------------------------------
	/// Retrieves parameters of the map layers local cache.
	///
	/// \param[out]	pstrMapLayersLocalCacheFolder			local cache folder.
	/// \param[out]	puMapLayersLocalCacheMaxSizeInMB		local cache max size in MB.
	/// \param[out]	puMapLayersLocalCacheCurrentSizeInMB	local cache current size in MB.
	/// \param[out]	paLayersParams							parameters of each map layer (can be NULL if unnecessary)
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode GetMapLayersLocalCacheParams(PCSTR *pstrMapLayersLocalCacheFolder, 
		UINT *puMapLayersLocalCacheMaxSizeInMB, UINT *puMapLayersLocalCacheCurrentSizeInMB, CMcDataArray<IMcMapLayer::SLocalCacheLayerParams> *paLayersParams = NULL);
//@}

/// \name GPU information
//@{
	//==============================================================================
	// Method Name: GetGpuInfo()
	//------------------------------------------------------------------------------
	/// Retrieves the GPU information
	///
	/// \param[out]	pGpuInfo	GPU information returned
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode GetGpuInfo(SGpuInfo *pGpuInfo);
//@}

/// \name Memory and Virtual File System Utilities (JavaScript API only)
//@{

#ifdef JAVA_SCRIPT_API

/// Flags of optional components required for the usage of certain features (used in JavaScript API only as a bit field in FetchOptionalComponents())
	enum EOptionalComponentFlags
	{
		EOCF_NONE				= 0x0000,	///< none
		EOCF_SYMBOLOGY_APP6D	= 0x0001,	///< APP-6 (D) - Allied Procedural Publication Standard: for usage of IMcObject::ESS_APP6D
		EOCF_SYMBOLOGY_2525C	= 0x0002,	///< MIL-STD-2525 (C) - 2525 Military Standard: for usage of IMcObject::ESS_2525C
		EOCF_ALL				= 0xFFFF	///< all the above components
	};

	//==============================================================================
	// Method Name: GetMemorySize()
	//------------------------------------------------------------------------------
	/// Retrieves current memory size (JavaScript API only)
	///
	/// \return		current memory size
	//==============================================================================
	static UINT GetMemorySize();

#endif

#ifdef JAVA_SCRIPT_API_IN_WRAPPER

	//==============================================================================
    // Method Name: FetchOptionalComponents()
    //------------------------------------------------------------------------------
    /// Fetches asynchronously optional components required for the usage of certain features (JavaScript API only)
    ///
    /// \param[in]	uOptionalComponentsBitField		a bit field (based on #EOptionalComponentFlags) defining the optional components to fetch
    ///
    /// \return		Promise without value
    //==============================================================================
    static Promise<void> FetchOptionalComponents(UINT uOptionalComponentsBitField);

	//==============================================================================
	// Method Name: GetMaxMemoryUsage()
	//------------------------------------------------------------------------------
	/// Retrieves maximal memory size used so far (JavaScript API only)
	///
	/// \return		maximal memory size used so far
	//==============================================================================
	static UINT GetMaxMemoryUsage();

	//==============================================================================
	// Method Name: GetHeapSize()
	//------------------------------------------------------------------------------
	/// Retrieves current size of memory heap (JavaScript API only)
	///
	/// \return		current size of memory heap
	//==============================================================================
	static UINT GetHeapSize();

	//==============================================================================
	// Method Name: SetHeapSize()
	//------------------------------------------------------------------------------
	/// Increases the size of memory heap (JavaScript API only)
	///
	/// \param[in]	uSize	new heap size (will be ignored if less than the current one; use UINT_MAX for the maximal possible size of 4GB)
	//==============================================================================
	static void SetHeapSize(UINT uSize);

	//==============================================================================
	// Method Name: MapNodeJsDirectory()
	//------------------------------------------------------------------------------
	/// Creates virtual file system directory mapped to a physical directory (JavaScript API only)
	///
	/// \param[in]	strPhysicalDirectory	the physical directory to map
	/// \param[out]	strVirtualDirectory		the virtual directory to be used to access the physical one
	//==============================================================================
	static void MapNodeJsDirectory(PCSTR strPhysicalDirectory, PCSTR strVirtualDirectory);

	//==============================================================================
	// Method Name: UnMapNodeJsDirectory()
	//------------------------------------------------------------------------------
	/// Deletes virtual file system directory mapping (JavaScript API only)
	///
	/// \param[out]	strVirtualDirectory	the virtual directory to remove
	//==============================================================================
	static void UnMapNodeJsDirectory(PCSTR strVirtualDirectory);

	//==============================================================================
	// Method Name: CreateFileSystemDirectory()
	//------------------------------------------------------------------------------
	/// Creates virtual file system directory (JavaScript API only)
	///
	/// \param[in]	strDirectory			the directory to create (its parent directory must exist)
	//==============================================================================
	static void CreateFileSystemDirectory(PCSTR strDirectory);

	//==============================================================================
	// Method Name: DeleteFileSystemEmptyDirectory()
	//------------------------------------------------------------------------------
	/// Deletes virtual file system directory (JavaScript API only)
	///
	/// \param[in]	strDirectory			the directory to delete (must be empty)
	//==============================================================================
	static void DeleteFileSystemEmptyDirectory(PCSTR strDirectory);

	//==============================================================================
	// Method Name: GetFileSystemDirectoryContents()
	//------------------------------------------------------------------------------
	/// Retrieves virtual file system directory contents - files and subdirectories (JavaScript API only)
	///
	/// \param[in]	strDirectory			the directory to retrieve contents of
	///
	/// \return		array of names of the directory's files and subdirectories (including "." and ".."), 
	///				or `null` if the directory does not exist
	//==============================================================================
	static PCSTR[] GetFileSystemDirectoryContents(PCSTR strDirectory);

	//==============================================================================
	// Method Name: CreateFileSystemFile()
	//------------------------------------------------------------------------------
	/// Creates virtual file system file (JavaScript API only)
	///
	/// \param[in]	strFileFullName			the file to create (its directory must exist)
	/// \param[in]	aFileContentsBuffer		the file contents as `ArrayBufferView` (e.g. `Uint8Array`) or UTF8 `string`
	//==============================================================================
	static void CreateFileSystemFile(PCSTR strFileFullName, const void *aFileContentsBuffer);

	//==============================================================================
	// Method Name: DeleteFileSystemFile()
	//------------------------------------------------------------------------------
	/// Deletes virtual file system file (JavaScript API only)
	///
	/// \param[in]	strFileFullName			the file to delete
	//==============================================================================
	static void DeleteFileSystemFile(PCSTR strFileFullName);

	//==============================================================================
	// Method Name: GetFileSystemFileContents()
	//------------------------------------------------------------------------------
	/// Retrieves virtual file system file contents (JavaScript API only)
	///
	/// \param[in]	strFileFullName			the file to retrieve contents of
	/// \param[in]	bString					whether the contents should be UTF8 `string` or `Uint8Array`; the default is false (`Uint8Array`)
	///
	/// \return		the file contents as `Uint8Arra` or UTF8 `string` (depending on \p bString), 
	///				or `null` if the file does not exist
	//==============================================================================
	static void* GetFileSystemFileContents(PCSTR strFileFullName, bool bString = false);

	//==============================================================================
	// Method Name: DownloadBufferAsFile()
	//------------------------------------------------------------------------------
	/// Downloads binary buffer as a file (JavaScript API only, not for node.js)
	///
	/// \param[in]	Buffer					the buffer contents as `ArrayBuffer` or `Uint8Array`
	/// \param[in]	strDownloadFileName		the file name to be given to the downloaded file
	//==============================================================================
    static void DownloadBufferAsFile(const void *Buffer, PCSTR strDownloadFileName);

	//==============================================================================
	// Method Name: DownloadFileSystemFile()
	//------------------------------------------------------------------------------
	/// Downloads virtual file system file (JavaScript API only, not for node.js)
	///
	/// \param[in]	strFileFullName			the file to download
	/// \param[in]	strDownloadFileName		the file name to be given to the downloaded file or `NULL` (the default) to derive it from \p strFullFileName
	///
	/// \return		whether the file exists and can be downloaded
	//==============================================================================
	static bool DownloadFileSystemFile(PCSTR strFileFullName, PCSTR strDownloadFileName = NULL);

	//==============================================================================
	// Method Name: DoesFileSystemPathExist()
	//------------------------------------------------------------------------------
	/// Checks whether virtual file system path (file or directory) exists (JavaScript API only, not for node.js)
	///
	/// \param[in]	strPathFullName			the file/directory to check
	///
	/// \return		whether the file/directory exists
	//==============================================================================
    function DoesFileSystemPathExist(PCSTR strPathFullName) : boolean;
#endif
//@}

};
