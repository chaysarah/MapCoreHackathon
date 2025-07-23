#pragma once
//===========================================================================
/// \file IMcImagesCorrelator.h
/// The images correlator interface
//===========================================================================

#include <windows.h>
#include "McExports.h"
#include "SMcVector.h"
#include "CMcDataArray.h"
#include "IMcErrors.h"
#include "Production/IMcFileProductions.h"

/// The images correlator static interface
class IMcImagesCorrelator
{

public:

	/// pair of 2D points
	struct SPoints2DPair
	{
		SMcVector2D First;	///< first image point
		SMcVector2D Second;	///< second image point
	};

	/// correlated pair of 2D points with score
	struct SPoints2DPairWithScore : SPoints2DPair
	{
		double dScore;		///< correlation score

		/// \n
		bool operator < (const SPoints2DPairWithScore &Other) const { return dScore < Other.dScore; }
		/// \n
		bool operator > (const SPoints2DPairWithScore &Other) const { return dScore > Other.dScore; }
	};
	
/// \name Calculation functions
//@{

	//=================================================================================================
	// 
	// Function name: IMcErrors,   CorrelateTwoImages (...)
	// 
	// Author     : Marina Gupshpun
	// Date       : 06/06/2007
	//-------------------------------------------------------------------------------------------------
	/// Correlates two images and generates collection of correlated points from one image to another one 
	///
	/// \param[in]	  strFirstImageFullPathName			full path  to first image 
	/// \param[in]    strSecondImageFullPathName		full path  to second image 
	/// \param[in]    aInitialGuessImagePointsPairs[]	array of initial guess points pairs
	/// \param[in]    uNumInitialGuessImagePointsPairs	number of initial correlated points
	/// \param[in]    uPointsDensityInPixels			request density of correlated points
	/// \param[out]   paCorrelatePointsPairs			array of pairs of correlated points with correlation scores 
	/// \param[in]    bDtmMode							whether it is used for DTM production
	///
	/// \return
	///     - status result
	//=================================================================================================
	static IMAGES_CORRELATOR_API IMcErrors::ECode CorrelateTwoImages(
		PCSTR strFirstImageFullPathName,
		PCSTR strSecondImageFullPathName,
		const SPoints2DPair aInitialGuessImagePointsPairs[],
		UINT uNumInitialGuessImagePointsPairs,
		UINT uPointsDensityInPixels,
		CMcDataArray<SPoints2DPairWithScore> *paCorrelatePointsPairs,
		bool bDtmMode = false);


	
	//=================================================================================================
	// 
	// Function name: void,   ProjectImageToImage (...)
	// 
	// Author     : Marina Gupshpun
	// Date       : 02/07/2007
	//-------------------------------------------------------------------------------------------------
	/// Projects one image to another one 
	///
	/// \param[in]    aImagePointsPairs[]		array of points pairs
	/// \param[in]    uNumImagePointsPairs		number of correlated points 
	/// \param[in]    aSrcData			 		array of second image data
	/// \param[in]    uSrcWidht 				width of second image
	/// \param[in]    uSrcHeight				height of second image
	/// \param[out]   aDstData 					array of destination image,	allocated by user
	/// \param[in]    uDstWidht 				width of destination image(as width of first image)
	/// \param[in]    uDstHeight				height of destination image(as height of first image)
	/// \param[in]    uNumBytesPerPixel 		number of channels per pixel, must be 1,3 or 4
	/// \param[in]    aDstBackgroundColor[]		background color (array of values for each channel)
	/// \param[in]    eResamplingMethod 		resampling method, see #IMcFileProductions::EResamplingMethod 
	///											for a complete list of methods
	/// \return
	///     - status result
	//=================================================================================================	
	static IMAGES_CORRELATOR_API IMcErrors::ECode ProjectImageToImage(
		const SPoints2DPair aImagePointsPairs[],
		UINT uNumImagePointsPairs,
		const void *aSrcData,
		UINT uSrcWidht,
		UINT uSrcHeight,
		void *aDstData,
		UINT uDstWidht,
		UINT uDstHeight,
		const BYTE aDstBackgroundColor[],
		UINT uNumBytesPerPixel,
		IMcFileProductions::EResamplingMethod eResamplingMethod);
//@}
};
