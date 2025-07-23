#pragma once

//===========================================================================
/// \file IMcHeatMapViewport.h
/// Interface for heat map viewport
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcMapViewport.h"

//===========================================================================
// Interface Name: IMcHeatMapViewport
//---------------------------------------------------------------------------
/// The interface for heat map viewport
//===========================================================================
class IMcHeatMapViewport : public virtual IMcMapViewport
{
protected:
	virtual ~IMcHeatMapViewport() {}

public:
	enum
	{
		//==============================================================================
		/// Unique ID for IMcHeatMapViewport-derived interface
		//==============================================================================
		INTERFACE_TYPE = 4
	};
	
	/// Heat map point
	struct SHeatMapPoint
	{
		const SMcVector2D *aLocations;	///< Array of point locations
		UINT uNumLocations;				///< Number of locations in \b aLocations
		BYTE uIntensity;				///< point intensity

		SHeatMapPoint() : aLocations(NULL), uNumLocations(0) {}
	};

/// \name Create
//@{
	//==============================================================================
	// Method Name: CreateHeatMap()
	//------------------------------------------------------------------------------
	/// Creates a vertical section viewport.
	///
	/// \param[out]	ppViewport				viewport created.
	/// \param[out]	ppCamera				default viewport camera created.
	/// \param[in]	CreateData				parameters used for the creation of the viewport.
	/// \param[in]	apTerrains				list of terrains to attach to the viewport.
	/// \param[in]	uNumTerrains			number of terrains to attach.
	/// \param[in]	bCalcAveragePerPoint	whether or not to calculate average per point.
	/// \param[in]	bShowHeatMapPicture		whether or not to show the heat map picture in the viewport.
	///
	/// \note
	///		ppCamera can be NULL.
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode CreateHeatMap(IMcHeatMapViewport **ppViewport,
		IMcMapCamera **ppCamera, const IMcMapViewport::SCreateData &CreateData,
		IMcMapTerrain *const apTerrains[], UINT uNumTerrains,
		bool bCalcAveragePerPoint, bool bShowHeatMapPicture);
//@}

	virtual IMcErrors::ECode UpdateHeatMapPoints(
		bool bRemoveAllPrevPoints, const SHeatMapPoint aPoints[], UINT uNumPoints,
		UINT eItemType, UINT uItemInfluenceRadius, bool bIsRadiusInPixels) = 0;
	
	virtual IMcErrors::ECode GetHeatMapPixelNormalizedValue(
		const SMcVector3D &WorldPos, float *pfPixelValue) /*const*/ = 0; // from aNormalizedPictureTextureBuffer
	virtual IMcErrors::ECode GetHeatMapPixelSumValue(
		const SMcVector3D &WorldPos, float *pfPixelValue) /*const*/ = 0; // from aSumBuffer
	virtual IMcErrors::ECode GetHeatMapPixelCountValue(
		const SMcVector3D &WorldPos, float *pfPixelValue) /*const*/ = 0; // from aCountBuffer (if exist)

	virtual IMcErrors::ECode GetHeatMapNormalizedBuffer(
		const BYTE **ppBytesBuffer, UINT *puNumBytes) /*const*/ = 0;
	virtual IMcErrors::ECode GetHeatMapSumBuffer(
		const float **ppFloatsBuffer, UINT *puNumFloats) /*const*/ = 0;
	virtual IMcErrors::ECode GetHeatMapCountBuffer(
		const float **ppFloatsBuffer, UINT *puNumFloats) /*const*/ = 0;

	virtual IMcErrors::ECode GetHeatMapUnNormalizedMinMaxValue(double *pdMinValue, double *pdMaxValue) /*const*/ = 0;

	virtual IMcErrors::ECode IsHeatMapAverageCalculatedPerPoint(bool *pbAverageCalculated)  const = 0;
	virtual IMcErrors::ECode IsHeatMapPictureShown(bool *pbPictureShown) const = 0;

	virtual IMcErrors::ECode SetHeatMapMinThresholdValues(
		double dMinValThreshold, double dMinValToUse) = 0;
	virtual IMcErrors::ECode GetHeatMapMinThresholdValues(
		double *pdMinValThreshold, double *pdMinValToUse) const = 0;
	virtual IMcErrors::ECode SetHeatMapMaxThresholdValues(
		double dMaxValThreshold, double dMaxValToUse) = 0;
	virtual IMcErrors::ECode GetHeatMapMaxThresholdValues(
		double *pdMaxValThreshold, double *pdMaxValToUse) const = 0;

	virtual IMcErrors::ECode SetHeatMapDrawPriority(short nDrawPriority) = 0;
	virtual IMcErrors::ECode GetHeatMapDrawPriority(short *pnDrawPriority) const = 0;

	virtual IMcErrors::ECode SetHeatMapTransparency(BYTE byTransparency) = 0;
	virtual IMcErrors::ECode GetHeatMapTransparency(BYTE *pbyTransparency) const = 0;

	virtual IMcErrors::ECode SetHeatMapVisibility(bool bVisible) = 0;
	virtual IMcErrors::ECode GetHeatMapVisibility(bool *pbVisible) const = 0;
};
