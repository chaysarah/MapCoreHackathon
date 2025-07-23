#pragma once

//===========================================================================
/// \file IMcMapHeightLines.h
/// Interface for map height-lines definition
//===========================================================================

#include "McExports.h"
#include "McCommonTypes.h"
#include "CMcDataArray.h"
#include "IMcBase.h"
#include "SMcVector.h"
#include "IMcErrors.h"


//===========================================================================
// Interface Name: IMcMapHeightLines
//---------------------------------------------------------------------------
/// Interface for map height-lines definition
//===========================================================================
class IMcMapHeightLines : virtual public IMcBase
{
protected:
    virtual ~IMcMapHeightLines() {}

public:
	
	/// height-lines display parameters for each scale step
	struct SScaleStep
	{
		/// the upper scale of the current grid step parameters
		float fMaxScale;

		/// height gap between one line to the following one
		float fLineHeightGap;

		/// line colors array (the meaning depends on color interpolation mode, see 
		///  SetColorInterpolationMode() for details)
		const SMcBColor *aColors;
		
		/// number of line colors
		UINT uNumColors;
				
		SScaleStep() : aColors(NULL), uNumColors(0) {}
		SScaleStep(float _fMaxScale, float _fLineHeightGap = 100, SMcBColor _aColors[] = NULL,
				   UINT _uNumColors = 0)				   
		{
			fMaxScale = _fMaxScale;
			fLineHeightGap = _fLineHeightGap;
			aColors = _aColors;
			uNumColors = _uNumColors;		
		}
	};

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates map height-lines interface
	///
	/// \param[out]	ppHeightLines			the height-lines interface created
	/// \param[in]	aScaleSteps[]			the parameters for each scale step
	/// \param[in]	uNumScaleSteps			the number of scale steps
	/// \param[in]	fLineWidth				the width in pixels of height lines
	///
	/// \note Color interpolation mode is initially disabled (see SetColorInterpolationMode())
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode Create(IMcMapHeightLines **ppHeightLines, 
		const SScaleStep aScaleSteps[], UINT uNumScaleSteps, float fLineWidth = 2);

	//==============================================================================
	// Method Name: SetScaleSteps()
	//------------------------------------------------------------------------------
	/// Updates scale steps along with their parameters
	///
	/// \param[in]	aScaleSteps[]		new parameters for each scale step
	/// \param[in]	uNumScaleSteps		new number of scale steps
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetScaleSteps(const SScaleStep aScaleSteps[], UINT uNumScaleSteps) = 0;

	//==============================================================================
	// Method Name: GetScaleSteps()
	//------------------------------------------------------------------------------
	/// Retrieves scale steps
	///
	/// \param[out]	paScaleSteps		array of parameters for each scale step
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetScaleSteps(CMcDataArray<SScaleStep> *paScaleSteps) const = 0;

	//==============================================================================
	// Method Name: SetColorInterpolationMode()
	//------------------------------------------------------------------------------
	/// Enables/disables the color interpolation mode.
	///
	/// - When disabled, the line colors (starting with a line at height 0) are 
	///   SScaleStep::aColors[0], SScaleStep::aColors[1], ... and so on periodically up and down.
	/// - When enabled, the line colors are interpolated between SScaleStep::aColors[] mapped 
	///   to a height range between \a fMinHeight and \a fMaxHeight; lines with heights below 
	///   \a fMinHeight and above \a fMaxHeight have the first and the last color from SScaleStep::aColors[] 
	///   respectively.
	///
	/// \param[in]	bEnabled		whether color interpolation mode is enabled
	///								(the default is false)
	/// \param[in]	fMinHeight		the height for the first color in SScaleStep::aColors[]
	/// \param[in]	fMaxHeight		the height for the last  color in SScaleStep::aColors[]
	///
	/// \note \a fMinHeight and \a fMaxHeight parameters are ignored if \a bEnabled is false
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetColorInterpolationMode(bool bEnabled, 
		float fMinHeight = 0, float fMaxHeight = 1000) = 0;

	//==============================================================================
	// Method Name: GetColorInterpolationMode()
	//------------------------------------------------------------------------------
	/// Retrieves the color interpolation mode set by SetColorInterpolationMode().
	///
	/// \param[out]	pbEnabled		whether color interpolation mode is enabled
	/// \param[out]	pfMinHeight		the height for the first color in SScaleStep::aColors[]
	///								(can be NULL if unnecessary)
	/// \param[out]	pfMaxHeight		the height for the last  color in SScaleStep::aColors[]
	///								(can be NULL if unnecessary)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetColorInterpolationMode(bool *pbEnabled, 
		float *pfMinHeight = NULL, float *pfMaxHeight = NULL) const = 0;

	//==============================================================================
	// Method Name: SetLineWidth()
	//------------------------------------------------------------------------------
	/// Sets width in pixels for all height lines.
	///
	/// \param[in]	fWidth		the width in pixels of height lines
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetLineWidth(float fWidth) = 0;

	//==============================================================================
	// Method Name: GetLineWidth()
	//------------------------------------------------------------------------------
	/// Retrieves width in pixels for all height lines.
	///
	/// \param[out]	pfWidth		the width in pixels of height lines
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLineWidth(float *pfWidth) const = 0;
};
