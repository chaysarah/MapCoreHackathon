#pragma once
class IMcGridCoordinateSystem;
class IMapViewport;

//===========================================================================
/// \file IMcCodeMapLayer.h
/// Interfaces for code map layers
//===========================================================================

#include "McExports.h"
#include "McCommonTypes.h"
#include "Map/IMcMapLayer.h"
#include "Map/IMcRasterMapLayer.h"
//===========================================================================
// Interface Name: IMcCodeMapLayer
//---------------------------------------------------------------------------
/// The base interface for code-map layers (raster-based layers when each pixel codes some information other than a color)
//===========================================================================
class IMcCodeMapLayer : virtual public IMcRasterMapLayer
{
protected:
	virtual ~IMcCodeMapLayer() {}

public:

	/// Code-map data
	struct SCodeMapData
	{
		UINT		uCode;		///< For IMcTraversabilityMapLayer: the number of pixel's traversable directions; for the rest: the code ID (pixel value)
		SMcBColor	CodeColor;	///< The color used to visualize the above code
	};

	/// \name Color Table
	//@{

	//==============================================================================
	// Method Name: SetColorTable()
	//------------------------------------------------------------------------------
	/// Sets the code-map layer's color table mapping each code (pixel value) into its visualization color.
	/// 
	/// - For IMcTraversabilityMapLayer only: SCodeMapData::uCode should be the number of pixel's traversable directions (0 through the number 
	///	  retrieved by IMcTraversabilityMapLayer::GetNumTraversabilityDirections().
	/// - Othrewise: SCodeMapData::uCode should be the code ID (pixel value).
	///
	/// \param[in]	aCodeMapColors		The array of code-color pairs
	/// \param[in]	uNumCodeMapColors	The number of elements in the above array
	///
	/// \note Empty array means no color mapping, the layer will be rendered by interpreting pixel codes as colors 
	/// (can be used to retrieve pixel codes by in rendering to a buffer)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetColorTable(const SCodeMapData aCodeMapColors[], UINT uNumCodeMapColors) = 0;

	//==============================================================================
	// Method Name: GetColorTable()
	//------------------------------------------------------------------------------
	/// Retrieves the code-map layer's color table mapping each code (pixel value) into its visualization color.
	///
	/// - For IMcTraversabilityMapLayer only: SCodeMapData::uCode is the number of pixel's traversable directions (0 through the number 
	///	  retrieved by IMcTraversabilityMapLayer::GetNumTraversabilityDirections().
	/// - Othrewise: SCodeMapData::uCode is the code ID (pixel value).
	///
	/// \param[out]	paCodeMapColors		The array of code-color pairs
	///
	/// \note Empty array means no color mapping, the layer is rendered by interpreting pixel codes as colors.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetColorTable(CMcDataArray<SCodeMapData> *paCodeMapColors) const = 0;
	//@}
};

//===========================================================================
// Interface Name: IMcMaterialMapLayer
//---------------------------------------------------------------------------
/// The base interface for material layers
//===========================================================================
class IMcMaterialMapLayer : virtual public IMcCodeMapLayer
{
protected:
    virtual ~IMcMaterialMapLayer() {}

public:

	struct SMaterialSubDef
	{
		PCSTR strSubDefName;
		UINT uNumBytes;

		SMaterialSubDef() : strSubDefName(NULL), uNumBytes(0)
		{
		}
	};

	struct SMaterialDef
	{
		PCSTR strDefName;
		UINT *aSubDefIndices; // index in aMaterialsSubDefs
		UINT uNumSubDefIndices;

		SMaterialDef() : strDefName(NULL), aSubDefIndices(NULL), uNumSubDefIndices(0)
		{
		}
	};

	struct SMaterialData : SCodeMapData
	{
		SMaterialData(
			UINT _uCode = UINT(-1), const SMcBColor&_CodeColor = SMcBColor(0, 0, 0, 0), PCSTR _strMaterialName = NULL,
			UINT _uMaterialDefIndex = UINT(-1), BYTE *_aMaterialData = NULL, UINT _uMaterialDataNumBytes = 0)
		{
			uCode = _uCode;
			CodeColor = _CodeColor;
			strMaterialName = _strMaterialName;
			uMaterialDefIndex = _uMaterialDefIndex;
			aMaterialData = _aMaterialData;
			uMaterialDataNumBytes = _uMaterialDataNumBytes;
		}

		PCSTR strMaterialName;
		UINT uMaterialDefIndex; // index in aMaterialsDefs (Note: can be common to several materials!)
		BYTE *aMaterialData; // to be interpreted by relevant SMaterialDef
		UINT uMaterialDataNumBytes; // must be equal to sum of uNumBytes as defined in SMaterialDef
	};

	// *puMaterialID == UINT_MAX means the color code of unknown material
	virtual IMcErrors::ECode GetMaterialIDFromColorCode(const SMcBColor &ColorCode, UINT *puMaterialID) const = 0;

	virtual IMcErrors::ECode SetMaterialsData(
		const SMaterialSubDef aMaterialsSubDefs[] = NULL, UINT uNumMaterialsSubDefs = 0,
		const SMaterialDef aMaterialsDefs[] = NULL, UINT uNumMaterialsDefs = 0,
		const SMaterialData aMaterialsData[] = NULL, UINT uNumMaterials = 0) = 0;

	virtual IMcErrors::ECode GetMaterialsData(
		CMcDataArray<SMaterialSubDef> *paMaterialsSubDefs = NULL,
		CMcDataArray<SMaterialDef> *paMaterialsDefs = NULL,
		CMcDataArray<SMaterialData> *paMaterialsData = NULL) const = 0;

	virtual IMcErrors::ECode GetMaterialsSubDefIndexByName(PCSTR strName, UINT *puIndex) const = 0;
	virtual IMcErrors::ECode GetMaterialsSubDefNameByIndex(UINT uIndex, PCSTR *pstrName) const = 0;
	virtual IMcErrors::ECode GetMaterialsDefIndexByName(PCSTR strName, UINT *puIndex) const = 0;
	virtual IMcErrors::ECode GetMaterialsDefNameByIndex(UINT uIndex, PCSTR *pstrName) const = 0;

	virtual IMcErrors::ECode GetMaterialSubDef(UINT uIndex, SMaterialSubDef *pMaterialSubDef) const = 0;
	virtual IMcErrors::ECode GetMaterialDef(UINT uIndex, SMaterialDef *pMaterialDef) const = 0;
	virtual IMcErrors::ECode GetMaterialData(UINT uMaterialID, SMaterialData *pMaterialData) const = 0;

	virtual IMcErrors::ECode GetMaterialSubDefData(const SMaterialData *pMaterialData, UINT uSubDefIndex,
		CMcDataArray<BYTE> *paMaterialSubDefData) const = 0;
};

//===========================================================================
// Interface Name: IMcNativeMaterialMapLayer
//---------------------------------------------------------------------------
/// The interface for material layer in MapCore format (converted) 
//===========================================================================
class IMcNativeMaterialMapLayer : virtual public IMcMaterialMapLayer
{
protected:
    virtual ~IMcNativeMaterialMapLayer() {}

public:
    enum
    {
        //================================================================
        /// Map layer unique ID for this interface
        //================================================================
        LAYER_TYPE = 61
    };

/// \name Create
//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native material layer
	///
	/// \param[out]	ppLayer					native material layer created
	/// \param[in]	strDirectory			a directory containing the layer's files
	/// \param[in]	bThereAreMissingFiles	whether some layer's files are missing
	/// \param[in]  pReadCallback			Callback receiving read layer events
	/// \param[in]	pLocalCacheLayerParams	parameters of optional cache on local disk or NULL if 
	///										should not be used
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcNativeMaterialMapLayer **ppLayer,
		PCSTR strDirectory, bool bThereAreMissingFiles = false, IReadCallback *pReadCallback = NULL, 
		const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);

	//==============================================================================
	// Method Name: GetCreateParams()
	//------------------------------------------------------------------------------
	/// Retrieves paramters set in Create()
	///
	/// \param[out]	pbThereAreMissingFiles		whether some layer's files are missing
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCreateParams(bool *pbThereAreMissingFiles) const = 0;
//@}

};

//===========================================================================
// Interface Name: IMcNativeServerMaterialMapLayer
//---------------------------------------------------------------------------
/// The interface for material layer used to display data from MapCore's Map Layer Server
//===========================================================================
class IMcNativeServerMaterialMapLayer : virtual public IMcMaterialMapLayer
{
protected:
	virtual ~IMcNativeServerMaterialMapLayer() {}

public:
	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 64
	};
	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native-server material layer
	///
	/// \param[out]	ppLayer					native-server material layer created
	/// \param[in]	strLayerURL				layer's URL in the server
	/// \param[in]  pReadCallback			Callback receiving read layer events
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcNativeServerMaterialMapLayer **ppLayer,
		PCSTR strLayerURL, IReadCallback *pReadCallback = NULL);
	//@}
};

//===========================================================================
// Interface Name: IMcRawMaterialMapLayer
//---------------------------------------------------------------------------
/// The interface for raw material layer (unconverted)
//===========================================================================
class IMcRawMaterialMapLayer : virtual public IMcMaterialMapLayer
{
protected:
	virtual ~IMcRawMaterialMapLayer() {}

public:
	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 65
	};

	/// \name Create
	//@{
		//==============================================================================
		// Method Name: Create()
		//------------------------------------------------------------------------------
		/// Creates raw material layer
		///
		/// \param[out]	ppLayer					raw material layer created
		/// \param[in]	Params					parameters used for creating the layer
		/// \param[in]	pLocalCacheLayerParams	parameters of optional cache on local disk or NULL if 
		///										should not be used
		///
		/// \return
		///     - status result
		//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcRawMaterialMapLayer **ppLayer,
		const SRawParams &Params,
		const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);

	//==============================================================================
	// Method Name: GetCreateParams()
	//------------------------------------------------------------------------------
	/// Retrieves paramters set in Create()
	///
	/// \param[out]	pParams					parameters used for creating the layer
	///
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
	/// 
	/// \param[out]	paComponents		array of layer's components
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetComponents(CMcDataArray<SComponentParams> *paComponents) const = 0;
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
		float *pfFirstPyramidResolution) = 0;
	//@}
};

//===========================================================================
// Interface Name: IMcTraversabilityMapLayer
//---------------------------------------------------------------------------
/// The base interface for traversability layers
//===========================================================================
class IMcTraversabilityMapLayer : virtual public IMcCodeMapLayer
{
public:

	/// Traversability in one direction defined by its angle
	struct STraversabilityDirection
	{
		float	fDirectionAngle;	///< the direction angle in degrees
		bool	bTraversable;		///< the traversability in this direction
	};

	/// \name Traversability Data
//@{
	//==============================================================================
	// Method Name: GetNumTraversabilityDirections()
	//------------------------------------------------------------------------------
	/// Retrieves the number of traversability directions stored for each pixel
	///
	/// \param[out]	puNumDirections		the number of traversability directions stored for each pixel
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetNumTraversabilityDirections(UINT *puNumDirections) const = 0;

	//==============================================================================
	// Method Name: GetTraversabilityFromColorCode()
	//------------------------------------------------------------------------------
	/// Retrieves the traversability data from the color code
	///
	/// \param[in]	ColorCode					The color code
	/// \param[out]	paTraversabilityDirections	The array of directions along with their traversability flags 
	///											(an empty array means the color code of unknown traversability)
	///										
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTraversabilityFromColorCode(
		const SMcBColor &ColorCode, CMcDataArray<STraversabilityDirection> *paTraversabilityDirections) = 0;
//@}

protected:
	virtual ~IMcTraversabilityMapLayer() {}
};

//===========================================================================
// Interface Name: IMcNativeTraversabilityMapLayer
//---------------------------------------------------------------------------
/// The interface for traversability layer in MapCore format (converted) 
//===========================================================================
class IMcNativeTraversabilityMapLayer : virtual public IMcTraversabilityMapLayer
{
protected:
	virtual ~IMcNativeTraversabilityMapLayer() {}

public:
	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 62
	};

	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native traversability layer
	///
	/// \param[out]	ppLayer					native traversability layer created
	/// \param[in]	strDirectory			a directory containing the layer's files
	/// \param[in]	bThereAreMissingFiles	whether some layer's files are missing
	/// \param[in]  pReadCallback			Callback receiving read layer events
	/// \param[in]	pLocalCacheLayerParams	parameters of optional cache on local disk or NULL if 
	///										should not be used
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcNativeTraversabilityMapLayer **ppLayer,
		PCSTR strDirectory, bool bThereAreMissingFiles = false, IReadCallback *pReadCallback = NULL, 
		const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);

	//==============================================================================
	// Method Name: GetCreateParams()
	//------------------------------------------------------------------------------
	/// Retrieves paramters set in Create()
	///
	/// \param[out]	pbThereAreMissingFiles		whether some layer's files are missing
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCreateParams(bool *pbThereAreMissingFiles) const = 0;
	//@}

};

//===========================================================================
// Interface Name: IMcNativeServerTraversabilityMapLayer
//---------------------------------------------------------------------------
/// The interface for traversability layer used to display data from MapCore's Map Layer Server
//===========================================================================
class IMcNativeServerTraversabilityMapLayer : virtual public IMcTraversabilityMapLayer
{
protected:
	virtual ~IMcNativeServerTraversabilityMapLayer() {}

public:
	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 63
	};
	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native-server traversability layer
	///
	/// \param[out]	ppLayer					native-server traversability layer created
	/// \param[in]	strLayerURL				layer's URL in the server
	/// \param[in]  pReadCallback			Callback receiving read layer events
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcNativeServerTraversabilityMapLayer **ppLayer,
		PCSTR strLayerURL, IReadCallback *pReadCallback = NULL);
	//@}
};

//===========================================================================
// Interface Name: IMcRawTraversabilityMapLayer
//---------------------------------------------------------------------------
/// The interface for raw traversability layer (unconverted)
//===========================================================================
class IMcRawTraversabilityMapLayer : virtual public IMcTraversabilityMapLayer
{
protected:
	virtual ~IMcRawTraversabilityMapLayer() {}

public:
	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 66
	};

	/// \name Create
	//@{
		//==============================================================================
		// Method Name: Create()
		//------------------------------------------------------------------------------
		/// Creates raw traversability layer
		///
		/// \param[out]	ppLayer					raw traversability layer created
		/// \param[in]	Params					parameters used for creating the layer
		/// \param[in]	pLocalCacheLayerParams	parameters of optional cache on local disk or NULL if 
		///										should not be used
		///
		/// \return
		///     - status result
		//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcRawTraversabilityMapLayer **ppLayer,
		const SRawParams &Params,
		const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);

	//==============================================================================
	// Method Name: GetCreateParams()
	//------------------------------------------------------------------------------
	/// Retrieves paramters set in Create()
	///
	/// \param[out]	pParams					parameters used for creating the layer
	///
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
	/// 
	/// \param[out]	paComponents		array of layer's components
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetComponents(CMcDataArray<SComponentParams> *paComponents) const = 0;
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
		float *pfFirstPyramidResolution) = 0;
	//@}
};

