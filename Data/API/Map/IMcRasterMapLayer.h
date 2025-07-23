#pragma once
class IMcGridCoordinateSystem;

//===========================================================================
/// \file IMcRasterMapLayer.h
/// Interfaces for raster map layers
//===========================================================================

#include "McExports.h"
#include "McCommonTypes.h"
#include "Map/IMcMapLayer.h"
#include "OverlayManager/IMcTexture.h"

//===========================================================================
// Interface Name: IMcRasterMapLayer
//---------------------------------------------------------------------------
/// The base interface for raster layers
//===========================================================================
class IMcRasterMapLayer : virtual public IMcMapLayer
{
public:
	virtual IMcErrors::ECode CalcHistogram(MC_HISTOGRAM aHistogram[3]) = 0;

	//==============================================================================
	// Method Name: GetTileBitmapByKey()
	//------------------------------------------------------------------------------
	/// Retrieves the tile's bitmap by its key (unique ID).
	///
	/// \param[in]	TileKey					key (unique ID) of the tile in interest
	/// \param[in]	bDecompress				whether or not to decompress compressed bitmap or keep it compressed
	/// \param[out]	peBitmapPixelFormat		tile's bitmap pixel format
	/// \param[out] pbBitmapFromTopToBottom whether or not tile's bitmap is from top to bottom
	/// \param[out]	pBitmapSize				tile's bitmap dimensions in pixels
	/// \param[out]	pBitmapMargins			tile's bitmap margins in pixels
	/// \param[out]	paBitmapBits			tile's bitmap bits
	///
	/// \note
	///		The tile is always loaded every time the function is called without using terrain cache. 
	///     If layer was added to terrain, it is better to use cache-based IMcSpatialQueries::GetRasterLayerTileBitmapByKey() 
	///     (or IMcSpatialQueries::GetRasterLayerColorByPoint() if one pixel is needed)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTileBitmapByKey(const SLayerTileKey &TileKey, bool bDecompress, 
		IMcTexture::EPixelFormat *peBitmapPixelFormat, bool *pbBitmapFromTopToBottom,
		SMcSize *pBitmapSize, SMcSize *pBitmapMargins,CMcDataArray<BYTE> *paBitmapBits) = 0;

protected:
    virtual ~IMcRasterMapLayer() {}

public:
};

//===========================================================================
// Interface Name: IMcNativeRasterMapLayer
//---------------------------------------------------------------------------
/// The interface for raster layer in MapCore format (converted) 
//===========================================================================
class IMcNativeRasterMapLayer : virtual public IMcRasterMapLayer
{
protected:
    virtual ~IMcNativeRasterMapLayer() {}

public:
    enum
    {
        //================================================================
        /// Map layer unique ID for this interface
        //================================================================
        LAYER_TYPE = 11
    };
/// \name Create
//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native raster layer
	///
	/// \param[out]	ppLayer					native raster layer created
	/// \param[in]	strDirectory			a directory containing the layer's files
	/// \param[in]	uFirstLowerQualityLevel	first level of detail (zero-based) that can be loaded 
	///										in a quality (256 x 256 textures) lower than the original 
	///										one (512 x 512 textures); the default, UINT(-1) means no 
	///										low-quality levels; the recommended value other than the 
	///										default is 1 (means the best level is loaded in the 
	///										original quality, the rest in lower quality) - 
	///										recommended in low memory computers
	/// \param[in]	bThereAreMissingFiles	whether some layer's files are missing
	/// \param[in]	uNumLevelsToIgnore		number of highest levels of detail to ignore (can be used
	///										to check the layer without highest levels of detail); 
	///										the default is 0 (ignore nothing)
	/// \param[in]	bEnhanceBorderOverlap	whether to enhance border quality (may prevent black border lines 
	///										in case of overlapping layers, but affects performance)
	/// \param[in]  pReadCallback			Callback receiving read layer events
	/// \param[in]	pLocalCacheLayerParams	parameters of optional cache on local disk or NULL if 
	///										should not be used
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcNativeRasterMapLayer **ppLayer,
		PCSTR strDirectory, UINT uFirstLowerQualityLevel = UINT(-1), bool bThereAreMissingFiles = false, 
		UINT uNumLevelsToIgnore = 0, bool bEnhanceBorderOverlap = false, 
		IReadCallback *pReadCallback = NULL, const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);

	//==============================================================================
	// Method Name: GetCreateParams()
	//------------------------------------------------------------------------------
	/// Retrieves paramters set in Create()
	///
	/// \param[out]	puFirstLowerQualityLevel	first level of detail (zero-based) that can be loaded 
	///											in a quality (256 x 256 textures) lower than the original 
	///											one (512 x 512 textures);
	///											UINT(-1) means no low-quality levels;
	///											1 means the best level is loaded in the original quality,
	///											the rest in lower quality.
	/// \param[out]	pbThereAreMissingFiles		whether some layer's files are missing
	/// \param[out]	puNumLevelsToIgnore			number of highest levels of detail to ignore (can be used
	///											to check the layer without highest levels of detail); 
	/// \param[out]	pbEnhanceBorderOverlap		whether to enhance border quality (may prevent black border lines 
	///											in case of overlapping layers, but affects performance.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCreateParams(
		UINT *puFirstLowerQualityLevel, bool *pbThereAreMissingFiles,
		UINT *puNumLevelsToIgnore, bool *pbEnhanceBorderOverlap) const = 0;
//@}
};

//===========================================================================
// Interface Name: IMcNativeServerRasterMapLayer
//---------------------------------------------------------------------------
/// The interface for raster layer used to display data from MapCore's Map Layer Server
//===========================================================================
class IMcNativeServerRasterMapLayer : virtual public IMcRasterMapLayer
{
protected:
    virtual ~IMcNativeServerRasterMapLayer() {}

public:
    enum
    {
        //================================================================
        /// Map layer unique ID for this interface
        //================================================================
        LAYER_TYPE = 14
    };
/// \name Create
//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native-server raster layer
	///
	/// \param[out]	ppLayer					native-server raster layer created
	/// \param[in]	strLayerURL				layer's URL in the server
	/// \param[in]  pReadCallback			Callback receiving read layer events
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcNativeServerRasterMapLayer **ppLayer,
		PCSTR strLayerURL, IReadCallback *pReadCallback = NULL);
//@}
};

//===========================================================================
// Interface Name: IMcRawRasterMapLayer
//---------------------------------------------------------------------------
/// The interface for raw raster layer (unconverted) 
//===========================================================================
class IMcRawRasterMapLayer : virtual public IMcRasterMapLayer
{
protected:
	virtual ~IMcRawRasterMapLayer() {}

public:
	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 12
	};

	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates raw raster layer
	///
	/// \param[out]	ppLayer					raw raster layer created
	/// \param[in]	Params					parameters used for creating the layer
	/// \param[in]	bImageCoordinateSystem	whether this is image without coordinate system
	/// \param[in]	pLocalCacheLayerParams	parameters of optional cache on local disk or NULL if 
	///										should not be used
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcRawRasterMapLayer **ppLayer,
		const SRawParams &Params, bool bImageCoordinateSystem = false,
		const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);

	//==============================================================================
	// Method Name: GetCreateParams()
	//------------------------------------------------------------------------------
	/// Retrieves paramters set in Create()
	///
	/// \param[out]	pParams					parameters used for creating the layer
	/// \param[out]	pbImageCoordinateSystem	whether this is image without coordinate system
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCreateParams(
		SRawParams *pParams, bool *pbImageCoordinateSystem) const = 0;

	//@}

	/// \name Components
	//@{

	//==============================================================================
	// Method Name: GetComponents()
	//------------------------------------------------------------------------------
	/// Retrieves all layer's components
	///
	/// 
	/// \param[out]	paComponents		array of layer's components
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetComponents(CMcDataArray<SComponentParams> *paComponents) const = 0;

	//==============================================================================
	// Method Name: AddComponents()
	//------------------------------------------------------------------------------
	/// Add additional layer's components to a dynamic layer.
	/// 
	/// Can be used only if non-default IMcMapLayer::SRawParams::MaxWorldLimit and IMcMapLayer::SRawParams::fHighestResolution
	/// were specified in Create())
	///
	/// \param[in]	aComponents			Layer's components (files and directories, relative to IMcMapLayer::SRawParams::strDirectory specified in Create())
	/// \param[in]  uNumComponents		Number of components
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode AddComponents(const SComponentParams aComponents[], UINT uNumComponents) = 0;

	//@}

	/// \name Resolutions
	//@{
	
	//=================================================================================
	// Method Name: GetResolutions()
	//---------------------------------------------------------------------------------
	/// Retrieves resolutions pyramid.
	///
	/// \param[out]	pfFirstPyramidResolution	first (finest) resolution of optional resolutions' pyramid in 
	///											map units per pixel; 0 if resolutions' pyramid is not used
	/// \param[out]	pauPyramidResolutions		optional indices of components in \a aComponents 
	///											starting each resolution; each resolution is 
	///											twice coarser than the previous one
	/// \return 
	///		-status result
	//=================================================================================
	virtual IMcErrors::ECode GetResolutions(CMcDataArray<UINT> *pauPyramidResolutions,
		float *pfFirstPyramidResolution) =0;

	//@}

	/// \name Color Channels
	//@{

	//==============================================================================
	// Method Name: SetColorChannels()
	//------------------------------------------------------------------------------
	/// Changes color channels' indices.
	///
	/// Each index can be any valid channel index of the raw data, or:
	/// - `UINT_MAX`: the original index of the channel in the raw data (the default);
	/// - `UINT_MAX - 1`: the channel is not used.
	///
	/// \param[in]	uRChanelIndex				index for red channel
	/// \param[in]	uGChanelIndex				index for green channel
	/// \param[in]	uBChanelIndex				index for blue channel
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetColorChannels(UINT uRChanelIndex, UINT uGChanelIndex, UINT uBChanelIndex) = 0;

	//==============================================================================
	// Method Name: GetColorChannels()
	//------------------------------------------------------------------------------
	/// Retrieves color channels' indices previously set by SetColorChannels().
	///
	/// Each index can be any valid channel index of the raw data, or:
	/// - `UINT_MAX`: the original index of the channel in the raw data (the default);
	/// - `UINT_MAX - 1`: the channel is not used.
	///
	/// \param[out]	puRChanelIndex				index for red channel
	/// \param[out]	puGChanelIndex				index for green channel
	/// \param[out]	puBChanelIndex				index for blue channel
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetColorChannels(UINT *puRChanelIndex, UINT *puGChanelIndex, UINT *puBChanelIndex) const = 0;

	//@}
};

//===========================================================================
// Interface Name: IMcWebServiceRasterMapLayer
//---------------------------------------------------------------------------
/// The interface for raster layer used to display data from OGC Web Mapping Services (WMS server)
//===========================================================================
class IMcWebServiceRasterMapLayer : virtual public IMcRasterMapLayer
{
protected:
	virtual ~IMcWebServiceRasterMapLayer() {}

public:
	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 13
	};

	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates WMS raster layer
	///
	/// \param[out]	ppLayer					WMS raster layer created
	/// \param[in]	Params					parameters used for building temporary XML file and accessing the WMS server
	/// \param[in]	pLocalCacheLayerParams	parameters of optional cache on local disk or NULL if 
	///										should not be used
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcWebServiceRasterMapLayer **ppLayer,
		const SWMSParams &Params, const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates WMTS raster layer
	///
	/// \param[out]	ppLayer					WMTS raster layer created
	/// \param[in]	Params					parameters used for building temporary XML file and accessing the WMTS server
	/// \param[in]	pLocalCacheLayerParams	parameters of optional cache on local disk or NULL if 
	///										should not be used
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcWebServiceRasterMapLayer **ppLayer,
		const SWMTSParams &Params, const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates WCS raster layer
	///
	/// \param[out]	ppLayer					WCS raster layer created
	/// \param[in]	Params					parameters used for building temporary XML file and accessing the WCS server
	/// \param[in]	pLocalCacheLayerParams	parameters of optional cache on local disk or NULL if 
	///										should not be used
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcWebServiceRasterMapLayer **ppLayer,
		const SWCSParams &Params, const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);
	//@}

	/// \name Get Web Service Parameters
	//@{
	//==============================================================================
	// Method Name: GetWebMapServiceType()
	//------------------------------------------------------------------------------
	/// Retrieves layer's web service type
	///
	/// 
	/// \param[out]	peType		The layer's web service type
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetWebMapServiceType(EWebMapServiceType *peType) const = 0;

	//==============================================================================
	// Method Name: GetWMSParams()
	//------------------------------------------------------------------------------
	/// Retrieves layer's WMS parameters
	///
	/// 
	/// \param[out]	pParams		parameters used for building temporary XML file and accessing the WMS server
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetWMSParams(SWMSParams *pParams) const = 0;

	//==============================================================================
	// Method Name: GetWMTSParams()
	//------------------------------------------------------------------------------
	/// Retrieves layer's WMTS parameters
	///
	/// 
	/// \param[out]	pParams						The parameters used for building temporary XML file and accessing the WMTS server
	/// \param[out] pbUsedServerTilingScheme	Optional, whether or not server tiling scheme is used
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetWMTSParams(SWMTSParams *pParams, bool *pbUsedServerTilingScheme = NULL) const = 0;

	//==============================================================================
	// Method Name: GetWCSParams()
	//------------------------------------------------------------------------------
	/// Retrieves layer's WCS parameters
	///
	/// 
	/// \param[out]	pParams		parameters used for building temporary XML file and accessing the WCS server
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetWCSParams(SWCSParams *pParams) const = 0;
	//@}
};
