#pragma once
class IMcGridCoordinateSystem;

//===========================================================================
/// \file IMcHeatMapLayer.h
/// Interfaces for heat map layers
//===========================================================================

#include "McExports.h"
#include "McCommonTypes.h"
#include "Map/IMcMapLayer.h"
//===========================================================================
// Interface Name: IMcHeatMapLayer
//---------------------------------------------------------------------------
/// The base interface for heat layers
//===========================================================================
class IMcHeatMapLayer : virtual public IMcMapLayer
{
protected:
    virtual ~IMcHeatMapLayer() {}

public:
	/// \name Min/Max Values and Color Table
	//@{

	enum
	{
		//================================================================
		/// number of colors in table
		//================================================================
		NUM_COLORS = 256
	};

	//==============================================================================
	// Method Name: GetMinMaxValues()
	//------------------------------------------------------------------------------
	/// Retrieves the heat map's minimum/maximum values per the given scale.
	///
	/// \param[in] fScale		scale
	/// \param[out]	pdMin		minimum value
	/// \param[out]	pdMax		maximum value
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetMinMaxValues(float fScale, double *pdMin, double *pdMax) const = 0;

	//==============================================================================
	// Method Name: SetColorTable()
	//------------------------------------------------------------------------------
	/// Sets the heat map's color table.
	///
	/// \param[in]	aColors		array of 256 colors used to show values from minimum (\p aColors[0]) to 
	///							maximum (\p aColors[255]); the values can be retrieved by GetMinMaxValues()
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetColorTable(const SMcBColor aColors[IMcHeatMapLayer::NUM_COLORS]) = 0;

	//==============================================================================
	// Method Name: GetColorTable()
	//------------------------------------------------------------------------------
	/// Retrieves the heat map's color table.
	///
	/// \param[out]	ppaColors	array of 256 colors used to show values from minimum (\p aColors[0]) to 
	///							maximum (\p aColors[255])
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetColorTable(const SMcBColor (**ppaColors)[IMcHeatMapLayer::NUM_COLORS]) const = 0;
	//@}
};

//===========================================================================
// Interface Name: IMcNativeHeatMapLayer
//---------------------------------------------------------------------------
/// The interface for heat layer in MapCore format (converted) 
//===========================================================================
class IMcNativeHeatMapLayer : virtual public IMcHeatMapLayer
{
protected:
    virtual ~IMcNativeHeatMapLayer() {}

public:
    enum
    {
        //================================================================
        /// Map layer unique ID for this interface
        //================================================================
        LAYER_TYPE = 31
    };

/// \name Create
//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native heat layer
	///
	/// \param[out]	ppLayer					native heat layer created
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
	static MAPLAYER_API IMcErrors::ECode Create(IMcNativeHeatMapLayer **ppLayer,
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
