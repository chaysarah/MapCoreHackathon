#pragma once
class IMcGridCoordinateSystem;
//===========================================================================
/// \file IMcDtmMapLayer.h
/// Interfaces for DTM map layers
//===========================================================================

#include "McExports.h"
#include "SMcVector.h"
#include "Map/IMcMapLayer.h"

//===========================================================================
// Interface Name: IMcDtmMapLayer
//---------------------------------------------------------------------------
/// The base interface for DTM map layers used as a part of a terrain and for direct queries
//===========================================================================
class IMcDtmMapLayer : virtual public IMcMapLayer
{
protected:
    virtual ~IMcDtmMapLayer() {}

public:

	/// DTM tile geometry data
	struct STileGeometry
	{
		CMcDataArray<SMcFVector3D>	aPointsCoordinates;	///< points' coordinates (including additional points for skirt bottom)
		CMcDataArray<SMcFVector3D>	aPointsNormals;		///< points' normals (including additional points for skirt bottom)
		CMcDataArray<USHORT>		auConnectionIndices;///< connection indices containing triplets of indices of points
		UINT						uNumSkirtPoints;	///< number of points added at the end for skirt bottom
		UINT						uNumSkirtIndices;	///< number of indices added at the end for skirt bottom
		float						fMinHeight;			///< height of the lowest point (except for additional points)
		float						fMaxHeight;			///< height of the highest point
	};

	struct SPointCloudGridCell 
	{
		float		fHeight;
		SMcBColor	color;
		bool		isCoveredByPointCloudPolygon;
		bool		isCoveredByCurrentPointCloudPolygon;
		bool		isCoveredByPointCloudGeometry;
		bool		isOnRemovedMaterial;
	};

	struct SPointCloudGrid
	{
		SPointCloudGridCell* aDTMTileGridCell;
		SMcBox worldBoundingBox;
		int currentIndex;
	};

	class IMcPointCloudGridGenerator
	{
	public:
		virtual void Generate(SPointCloudGrid* pPointCloudGrid) = 0;
		virtual void Update(SPointCloudGrid* pPointCloudGrid) = 0;
	};




	//==============================================================================
	// Method Name: GetTileGeometryByKey()
	//------------------------------------------------------------------------------
	/// Retrieves the tile's geometry data by its key (unique ID).
	///
	/// \param[in]	TileKey				key (unique ID) of the tile in interest
	/// \param[in]	bBuildIfPossible	what to do if if the required tile does not exist but the coarser one does: 
	///									whether to build the geometry from the appropriate portion of a coarser tile or to fail
	/// \param[out]	pTileGeometry		tile's geometry data
	///
	/// \note
	///		The tile is always loaded every time the function is called without using terrain cache. 
	///     If layer was added to terrain, it is better to use cache-based IMcSpatialQueries::GetDtmLayerTileGeometryByKey().
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTileGeometryByKey(const SLayerTileKey &TileKey, bool bBuildIfPossible, 
		STileGeometry *pTileGeometry) = 0;


	virtual IMcErrors::ECode InsertPointCloud(const SMcVector3D aBoundingPolygonPoints[], UINT uNumgPolygonPoints, 
		const SMcVector3D aCloudPoints[], const SMcBColor aCloudPointsColor[], UINT uNumCloudPoints) = 0;

	virtual IMcErrors::ECode InsertPointCloud(const SMcVector3D aBoundingPolygonPoints[], UINT uNumgPolygonPoints,
												IMcPointCloudGridGenerator* pPointCloudGenerator) = 0;
	virtual int GetNumGridCells() = 0;
	virtual SMcBColor GetTileColorCode() = 0;

	virtual bool HasPointCloud() const = 0;

};

//===========================================================================
// Interface Name: IMcNativeDtmMapLayer
//---------------------------------------------------------------------------
/// The interface for DTM map layer in MapCore format (converted)
//===========================================================================
class IMcNativeDtmMapLayer : virtual public IMcDtmMapLayer
{
protected:
    virtual ~IMcNativeDtmMapLayer() {}

public:
    enum
    {
        //================================================================
        /// Map layer unique ID for this interface
        //================================================================
        LAYER_TYPE = 1
    };

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native DTM layer
	///
	/// \param[out]	ppLayer					native DTM layer created
	/// \param[in]	strDirectory			a directory containing the layer's files
	/// \param[in]	uNumLevelsToIgnore		number of highest levels of detail to ignore (can be used
	///										to check the layer without highest levels of detail); 
	///										the default is 0 (ignore nothing)
	/// \param[in]  pReadCallback			Callback receiving read layer events
	/// \param[in]	pLocalCacheLayerParams	parameters of optional cache on local disk or NULL if 
	///										should not be used
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcNativeDtmMapLayer **ppLayer, 
		PCSTR strDirectory, UINT uNumLevelsToIgnore = 0,
		IReadCallback *pReadCallback = NULL, const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);

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
};

//===========================================================================
// Interface Name: IMcNativeServerDtmMapLayer
//---------------------------------------------------------------------------
/// The interface for DTM map layer  used to display data from MapCore's Map Layer Server
//===========================================================================
class IMcNativeServerDtmMapLayer : virtual public IMcDtmMapLayer
{
protected:
    virtual ~IMcNativeServerDtmMapLayer() {}

public:
    enum
    {
        //================================================================
        /// Map layer unique ID for this interface
        //================================================================
        LAYER_TYPE = 4
    };

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native-server DTM layer
	///
	/// \param[out]	ppLayer					native-server DTM layer created
	/// \param[in]	strLayerURL				layer's URL in the server
	/// \param[in]  pReadCallback			Callback receiving read layer events
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcNativeServerDtmMapLayer **ppLayer, PCSTR strLayerURL, IReadCallback *pReadCallback = NULL);
};

//===========================================================================
// Interface Name: IMcRawDtmMapLayer
//---------------------------------------------------------------------------
/// The interface for raw DTM layer (unconverted) 
//===========================================================================
class IMcRawDtmMapLayer : virtual public IMcDtmMapLayer
{
protected:
	virtual ~IMcRawDtmMapLayer() {}

public:
	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 2
	};

	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates raw DTM layer
	///
	/// \param[out]	ppLayer					raw DTM layer created
	/// \param[in]	Params					parameters used for creating the layer
	/// \param[in]	pLocalCacheLayerParams	parameters of optional cache on local disk or NULL if 
	///										should not be used
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcRawDtmMapLayer **ppLayer,
		const SRawParams &Params, const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);

	//==============================================================================
	// Method Name: GetCreateParams()
	//------------------------------------------------------------------------------
	/// Retrieves paramters set in Create()
	///
	/// \param[out]	pParams					parameters used for creating the layer
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCreateParams(SRawParams *pParams) const = 0;
	//@}

	/// \name Components
	//@{

	//==============================================================================
	// Method Name: GetComponents()
	//------------------------------------------------------------------------------
	/// Retrieves all layer's components
	///
	/// \param[out]	papComponents		array of layer's components
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetComponents(CMcDataArray<SComponentParams> *papComponents) const = 0;

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
};

//===========================================================================
// Interface Name: IMcWebServiceDtmMapLayer
//---------------------------------------------------------------------------
/// The interface for DTM layer used to display data from OGC Web Mapping Services (WMS server)
//===========================================================================
class IMcWebServiceDtmMapLayer : virtual public IMcDtmMapLayer
{
protected:
	virtual ~IMcWebServiceDtmMapLayer() {}

public:
	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 3
	};

	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates WMS Dtm layer
	///
	/// \param[out]	ppLayer					WMS Dtm layer created
	/// \param[in]	Params					parameters used for building temporary XML file and accessing the WMS server
	/// \param[in]	pLocalCacheLayerParams	parameters of optional cache on local disk or NULL if 
	///										should not be used
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcWebServiceDtmMapLayer **ppLayer,
		const SWMSParams &Params, const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates WMTS Dtm layer
	///
	/// \param[out]	ppLayer					WMTS Dtm layer created
	/// \param[in]	Params					parameters used for building temporary XML file and accessing the WMTS server
	/// \param[in]	pLocalCacheLayerParams	parameters of optional cache on local disk or NULL if 
	///										should not be used
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcWebServiceDtmMapLayer **ppLayer,
		const SWMTSParams &Params, const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates WCS Dtm layer
	///
	/// \param[out]	ppLayer					WCS Dtm layer created
	/// \param[in]	Params					parameters used for building temporary XML file and accessing the WCS server
	/// \param[in]	pLocalCacheLayerParams	parameters of optional cache on local disk or NULL if 
	///										should not be used
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcWebServiceDtmMapLayer **ppLayer,
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
	/// \param[out]	pParams		parameters used for building temporary XML file and accessing the WMTS server
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetWMTSParams(SWMTSParams *pParams) const = 0;

	//==============================================================================
	// Method Name: GetWCSParams()
	//------------------------------------------------------------------------------
	/// Retrieves layer's WMS parameters
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
