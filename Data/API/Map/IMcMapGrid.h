#pragma once

//===========================================================================
/// \file IMcMapGrid.h
/// Interface for map grid definition
//===========================================================================

#include "McExports.h"
#include "McCommonTypes.h"
#include "CMcDataArray.h"
#include "IMcBase.h"
#include "SMcVector.h"
#include "IMcErrors.h"

class IMcLineItem;
class IMcTextItem;
class IMcGridCoordinateSystem;
//===========================================================================
// Interface Name: IMcMapGrid
//---------------------------------------------------------------------------
/// Interface for map grid definition
/// 
/// The IMcMapGrid enables to display a scalable coordinate system grid on the viewport.
/// It has the following properties:
///  - Line grids for major and minor axes in any desirable coordinate system.
///  - Coordinates in any desirable format are displayed on the grid to enhance the orientation.
///  - The grid appearance is scalable - i.e. the gaps between the lines and text are scale related.
///  - The grid line style, width and color as well as the grid text's font and color are fully controlled by the application.
/// 
/// The following image is a snapshot of a metric (UTM) IMapGrid displayed on to of a Geographical Map.
/// \image html IMcMapGridExample.png
//===========================================================================
class IMcMapGrid : virtual public IMcBase
{
protected:
    virtual ~IMcMapGrid() {}

public:

	/// Angle value format
	///
	/// Used to set/retrieve the format used to represent an angle value. 
	enum EAngleFormat
	{
		EAF_DECIMAL_DEG,	///< A decimal degrees is repesented as fixed-point number (e.g. 36.5&deg;).
		EAF_DEG_MIN_SEC,	///< A triplet of whole numbers represents the value's degree, minute and second (e.g. 36&deg;30'30").
		EAF_DEG_MIN			///< A pair of whole numbers represents the value's degree and minute(e.g. 36&deg;30').
	};
	
	/// \struct SGridRegion
	/// Grid region parameters
	/// \see SScaleStep
	struct SGridRegion
	{
		/// The Grid region's coordinate system (can be different from that of viewport);
		IMcGridCoordinateSystem			*pCoordinateSystem;

		/// The grid region's bounding box in GEO coordinate system (`x` and `y` components only are relevant);
		/// all-zeros box means infinite limits
		SMcBox							GeoLimit;

		/// The grid region's line item (in case `bUseBasicItemPropertiesOnly == true` in IMcMapGrid::Create(): color, width and line style only are used)
		IMcLineItem						*pGridLine;

		/// The grid region's text item (in case `bUseBasicItemPropertiesOnly == true` in IMcMapGrid::Create(): color and height only are used, 
		/// text scale's `y` component is used as height, font is not used)
		IMcTextItem						*pGridText;

		/// The first index within the `IMcMapGrid::SScaleStep` array that this region will apply to
		UINT							uFirstScaleStepIndex;

		/// The last index in the `IMcMapGrid::SScaleStep` array that this region will apply to
		UINT							uLastScaleStepIndex;

		SGridRegion() : GeoLimit(0,0,0,0,0,0), uFirstScaleStepIndex(0), uLastScaleStepIndex(UINT_MAX),
			pCoordinateSystem(NULL), pGridLine(NULL), pGridText(NULL) {}
	};

	/// \struct SScaleStep
	/// Grid display parameters for each scale step
	/// \see SScaleRegion
	struct SScaleStep
	{
		/// The upper scale of the current grid step parameters
		float			fMaxScale;			

		/// The gap between one line to the following one along each axis
		SMcVector2D		NextLineGap;
		
		/// The number of lines between one text to the following one along X axis:
		/// (1 - means that each line has a text, 2 - means one that each second line has a text and so on... )
		UINT				uNumOfLinesBetweenDifferentTextX;

		/// The number of lines between one text to the following one along Y axis:
		/// (1 - means that each line has a text, 2 - means one that each second line has a text and so on... )
		UINT				uNumOfLinesBetweenDifferentTextY;

		/// The number of lines between texts with the same values along X axis
		/// (1 means exactly one text is located the middle of each segment between neighbor lines, 2 means one text 
		/// is located in every second segment, and so on...)
		UINT				uNumOfLinesBetweenSameTextX;

		/// The number of lines between texts with the same values along Y axis
		/// (1 means exactly one text is located the middle of each segment between neighbor lines, 2 means one text 
		/// is located in every second segment, and so on...)
		UINT				uNumOfLinesBetweenSameTextY;


		/// The number of trailing digits to truncate for metric coordinate systems (for example, 
		/// UTM coordinate 3123456 with 3 digits truncated will be 3123 - namely kilometers)
		UINT			uNumMetricDigitsToTruncate;
		
		/// The format of angle values for geographical coordinate systems 
		EAngleFormat	eAngleValuesFormat;

		SScaleStep() {}
		SScaleStep(float _fMaxScale, SMcVector2D _NextLineGap, 
			UINT _uNumOfLinesBetweenDifferentTextX, UINT _uNumOfLinesBetweenDifferentTextY,
			UINT _uNumOfLinesBetweenSameTextX, UINT _uNumOfLinesBetweenSameTextY,
			UINT _uNumMetricDigitsToTruncate, EAngleFormat _eAngleValuesFormat)
				   
		{
			fMaxScale = _fMaxScale;	NextLineGap = _NextLineGap; 
			uNumOfLinesBetweenDifferentTextX = _uNumOfLinesBetweenDifferentTextX; 
			uNumOfLinesBetweenDifferentTextY = _uNumOfLinesBetweenDifferentTextY;
			uNumOfLinesBetweenSameTextX = _uNumOfLinesBetweenSameTextX; 
			uNumOfLinesBetweenSameTextY = _uNumOfLinesBetweenSameTextY;
			uNumMetricDigitsToTruncate = _uNumMetricDigitsToTruncate;
			eAngleValuesFormat = _eAngleValuesFormat;
		};
	};

	/// \name Create
	//@{
		//==============================================================================
		// Method Name: Create()
		//------------------------------------------------------------------------------
		/// Creates map grid
		///
		/// \param[out]	ppGrid						The newly map grid's interface created.
		/// \param[in]	aGridRegions[]				Parameters for each grid region.
		/// \param[in]	uNumGridRegions				Number of grid regions to create.
		/// \param[in]	aScaleSteps[]				Parameters for each scale step.
		/// \param[in]	uNumScaleSteps				Number of scale steps to create.
		/// \param[in]  bUseBasicItemPropertiesOnly	Whether to use only basic properties of line/text items (line style/width/color and text color/scale.y, 
		///											see also SGridRegion::pGridLine and SGridRegion::pGridText) or to use all their properties 
		///											(except draw priority and draw priority group).
		///
		/// \return
		///     - status result
		//==============================================================================
		static SCENEMANAGER_API IMcErrors::ECode Create(IMcMapGrid **ppGrid, 
			const SGridRegion aGridRegions[], UINT uNumGridRegions, const SScaleStep aScaleSteps[], 
			UINT uNumScaleSteps, bool bUseBasicItemPropertiesOnly = true);

		//==============================================================================
		// Method Name: IsUsingBasicItemPropertiesOnly()
		//------------------------------------------------------------------------------
		/// Checks whether grid line/text use only basic properties of line/text items, as set in Create().
		///
		/// \param[out]	pbUsingBasicItemPropertiesOnly		Whether grid line/text use only basic properties of line/text items (line style/width/color 
		///													and text color/scale.y, see also SGridRegion::pGridLine and SGridRegion::pGridText) or 
		///													all their properties (except draw priority and draw priority group).
		///
		/// \return
		///     - status result
		//==============================================================================
		virtual IMcErrors::ECode IsUsingBasicItemPropertiesOnly(bool *pbUsingBasicItemPropertiesOnly) const = 0;
	//@}

	/// \name Controlling the regions
	//{@
		//==============================================================================
		// Method Name: SetGridRegions()
		//------------------------------------------------------------------------------
		/// Updates grid regions along with their parameters
		///
		/// \param[in]	aGridRegions[]		Parameters for each grid region to set.
		/// \param[in]	uNumGridRegions		Number of grid regions to reallocate.
		///
		/// \see GetGridRegions()
		/// \return
		///     - status result
		//==============================================================================
		virtual IMcErrors::ECode SetGridRegions(const SGridRegion aGridRegions[], UINT uNumGridRegions) = 0;

		//==============================================================================
		// Method Name: GetGridRegions()
		//------------------------------------------------------------------------------
		/// Retrieves grid regions
		///
		/// \param[out]	paGridRegions		An array of parameters per each grid region.
		///
		/// \see SetGridRegions()
		/// \return
		///     - status result
		//==============================================================================
		virtual IMcErrors::ECode GetGridRegions(CMcDataArray<SGridRegion> *paGridRegions) const = 0;
	//@}

	/// \name Controlling the steps
	//{@
		//==============================================================================
		// Method Name: SetScaleSteps()
		//------------------------------------------------------------------------------
		/// Updates scale steps along with their parameters
		///
		/// \param[in]	aScaleSteps[]		new parameters for each scale step
		/// \param[in]	uNumScaleSteps		new number of scale steps
		///
		/// \see GetScaleSteps()
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
		/// \see SetScaleSteps()
		/// \return
		///     - status result
		//==============================================================================
		virtual IMcErrors::ECode GetScaleSteps(CMcDataArray<SScaleStep> *paScaleSteps) const = 0;
	//@}
};
