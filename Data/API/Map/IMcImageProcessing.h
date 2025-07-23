#pragma once

//===========================================================================
/// \file IMcImageProcessing.h 
/// Interfaces for Image Processing
//===========================================================================

#include "IMcErrors.h"
#include "McCommonTypes.h"
#include "CMcDataArray.h"

class IMcRasterMapLayer;

//===========================================================================
// Interface Name: IMcImageProcessing
//---------------------------------------------------------------------------
/// Base interface for viewport Image Processing
//===========================================================================
class IMcImageProcessing
{
protected:
	virtual ~IMcImageProcessing() {}

public:

	/// the type for a color table for each channel
	typedef		BYTE	MC_COLORTABLE[256];


	/// Describes on which color channel to operate when applying image processing operations
	typedef enum {
		ECC_RED				= 0,	///< red channel
		ECC_GREEN			= 1,	///< green channel 
		ECC_BLUE			= 2,	///< blue channel 
		ECC_MULTI_CHANNEL	= 3		///< all channels together
	} EColorChannel;

	/// Describes which image processing filter operation to apply
	typedef enum {	
		EFPO_NO_FILTER		= 0,	///< no filter
		EFPO_SMOOTH_LOW		= 1,	///< low-power smoothing filter
		EFPO_SMOOTH_MID		= 2,	///< mid-power smoothing filter
		EFPO_SMOOTH_HIGH	= 3,	///< high-power smoothing filter
		EFPO_SHARP_LOW		= 4,	///< low-power sharpening filter
		EFPO_SHARP_MID		= 5,	///< mid-power sharpening filter
		EFPO_SHARP_HIGH		= 6,	///< high-power sharpening filter
		EFPO_CUSTOM_FILTER	= 7		///< custom filter
	} EFilterProccessingOperation;


	virtual IMcErrors::ECode SetFilterImageProcessing(IMcRasterMapLayer *pLayer, EFilterProccessingOperation eOperation) = 0;

	virtual IMcErrors::ECode GetFilterImageProcessing(IMcRasterMapLayer *pLayer, EFilterProccessingOperation *peOperation) const = 0;

	virtual IMcErrors::ECode SetCustomFilter(IMcRasterMapLayer *pLayer, UINT uFilterXsize, UINT uFilterYsize, 
		float aFilter[], float fBias, float fDivider) = 0;

	virtual IMcErrors::ECode GetCustomFilter(IMcRasterMapLayer *pLayer, UINT *puFilterXsize, UINT *puFilterYsize,
		                                     const float **paFilter, float *pfBias, float *pfDivider) = 0;

	virtual IMcErrors::ECode SetEnableColorTableImageProcessing(IMcRasterMapLayer *pLayer, bool bEnable) = 0;

	virtual IMcErrors::ECode GetEnableColorTableImageProcessing(IMcRasterMapLayer *pLayer, bool *pbEnabled) const = 0;

	virtual IMcErrors::ECode IsOriginalHistogramSet(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, bool *pbIsSet) = 0;

	virtual IMcErrors::ECode  SetOriginalHistogram(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, const MC_HISTOGRAM &aHistogram) = 0;

	virtual IMcErrors::ECode  GetOriginalHistogram(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, MC_HISTOGRAM &aHistogram) const = 0;

	virtual IMcErrors::ECode  SetVisibleAreaOriginalHistogram(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, bool bUse) = 0;

	virtual IMcErrors::ECode  GetVisibleAreaOriginalHistogram(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, bool *pbUse) const = 0;

	virtual IMcErrors::ECode  GetCurrentHistogram(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, MC_HISTOGRAM  &aCurrHistogram) = 0;

	virtual IMcErrors::ECode  SetUserColorValues(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, const MC_COLORTABLE &aColorValues, bool bUse) = 0;

	virtual IMcErrors::ECode GetUserColorValues(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, MC_COLORTABLE &aColorValues, bool *pbUse) const = 0;

	virtual IMcErrors::ECode  SetColorValuesToDefault(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel) = 0;

	virtual IMcErrors::ECode  GetCurrentColorValues(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, MC_COLORTABLE &aColorValues) = 0;

	virtual IMcErrors::ECode  SetWhiteBalanceBrightness(IMcRasterMapLayer *pLayer, BYTE r, BYTE g, BYTE b) = 0;

	virtual IMcErrors::ECode  GetWhiteBalanceBrightness(IMcRasterMapLayer *pLayer, BYTE *pR, BYTE *pG, BYTE *pB) const = 0;

	virtual IMcErrors::ECode  SetColorTableBrightness(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, double dBrightness) = 0;

	virtual IMcErrors::ECode  GetColorTableBrightness(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, double *pdBrightness) const = 0;

	virtual IMcErrors::ECode  SetContrast(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, double dContrast) = 0;

	virtual IMcErrors::ECode  GetContrast(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, double *pdContrast) const = 0;

	virtual IMcErrors::ECode  SetNegative(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, bool bUse) = 0;

	virtual IMcErrors::ECode  GetNegative(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, bool *pbUse) const = 0;

	virtual IMcErrors::ECode  SetGamma(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, double dGamma) = 0;

	virtual IMcErrors::ECode  GetGamma(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, double *pdGamma) const = 0;

	virtual IMcErrors::ECode  SetHistogramEqualization(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, bool bUse) = 0;

	virtual IMcErrors::ECode  GetHistogramEqualization(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, bool *pbUse) const = 0;

	virtual IMcErrors::ECode  SetHistogramFit(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, bool bUse,
		const MC_HISTOGRAM  &aReferenceHistogram) = 0;

	virtual IMcErrors::ECode  GetHistogramFit(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, bool *pbUse,
		MC_HISTOGRAM  &aReferenceHistogram) const = 0;

	virtual IMcErrors::ECode  SetHistogramNormalization(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, bool bUse,
		double dMean = 127.5, double dStandardDeviation = 42.5) = 0;

	virtual IMcErrors::ECode  GetHistogramNormalization(IMcRasterMapLayer *pLayer, EColorChannel eColorChannel, bool *pbUse,
		double *pdMean, double *pdStandardDeviation) const = 0;
};

