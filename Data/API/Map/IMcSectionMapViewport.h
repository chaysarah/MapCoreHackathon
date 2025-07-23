#pragma once

//===========================================================================
/// \file IMcSectionMapViewport.h
/// Interface for vertical section map viewport.
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcMapViewport.h"

class IMcMapTerrain;

//===========================================================================
// Interface Name: IMcSectionMapViewport
//---------------------------------------------------------------------------
/// Interface for vertical section map viewport.
///
/// This is a 2D viewport that shows a terrain vertical section passing through given route points.
//===========================================================================
class IMcSectionMapViewport : public virtual IMcMapViewport
{
protected:
    virtual ~IMcSectionMapViewport() {}

public:
	enum
	{
		//==============================================================================
		/// Unique ID for IMcSpatialQueries-derived interface
		//==============================================================================
		INTERFACE_TYPE = 3
	};

/// \name Create
//@{
	//==============================================================================
	// Method Name: CreateSection()
	//------------------------------------------------------------------------------
	/// Creates a vertical section viewport.
	///
	/// \param[out]	ppViewport				viewport created.
	/// \param[out]	ppCamera				default viewport camera created.
	/// \param[in]	CreateData				parameters used for the creation of the viewport.
	/// \param[in]	apTerrains				list of terrains to attach to the viewport.
	/// \param[in]	uNumTerrains			number of terrains to attach.
	/// \param[in]	aSectionRoutePoints[]	section route points (\a x and \a y only are used).
	/// \param[in]	uNumSectionRoutePoints	number of section route points.
	/// \param[in]	aSectionHeightPoints[]	optional section height points calculated from \a aSectionRoutePoints by calling 
	///										IMcSpatialQueries::GetTerrainHeightsAlongLine()); if not specified
	/// \param[in]	uNumSectionHeightPoints	number of section height points.
	///
	/// \note
	///		ppCamera can be NULL.
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode CreateSection(IMcSectionMapViewport **ppViewport, 
		IMcMapCamera **ppCamera, const IMcMapViewport::SCreateData &CreateData, 
		IMcMapTerrain *const apTerrains[], UINT uNumTerrains,
		const SMcVector3D aSectionRoutePoints[], UINT uNumSectionRoutePoints,
		const SMcVector3D aSectionHeightPoints[] = NULL, UINT uNumSectionHeightPoints = 0);
//@}

/// \name Update Section Parameters
//@{
	//==============================================================================
	// Method Name: SetSectionRoutePoints()
	//------------------------------------------------------------------------------
	/// Updates section route points.
	///
	/// \param[in]	aSectionRoutePoints[]	section route points (\a x and \a y only are used).
	/// \param[in]	uNumSectionRoutePoints	number of section route points.
	/// \param[in]	aSectionHeightPoints[]	optional section height points calculated from \a aSectionRoutePoints by calling 
	///										IMcSpatialQueries::GetTerrainHeightsAlongLine()).
	/// \param[in]	uNumSectionHeightPoints	number of section height points.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSectionRoutePoints(
		const SMcVector3D aSectionRoutePoints[], UINT uNumSectionRoutePoints,
		const SMcVector3D aSectionHeightPoints[] = NULL, UINT uNumSectionHeightPoints = 0) = 0;

	//==============================================================================
	// Method Name: GetSectionRoutePoints()
	//------------------------------------------------------------------------------
	/// Retrieves section route points.
	///
	/// \param[out]	paSectionRoutePoints	section route points (\a x and \a y only are used).
	/// \param[out]	paSectionHeightPoints	optional section height points previously set in Create() or SetSectionRoutePoints().
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSectionRoutePoints(
		CMcDataArray<SMcVector3D> *paSectionRoutePoints, CMcDataArray<SMcVector3D> *paSectionHeightPoints = NULL) = 0;

	//==============================================================================
	// Method Name: SetAxesRatio()
	//------------------------------------------------------------------------------
	/// Sets ratio between screen lengths of units of \a Y and \a X axes
	///
	/// \param[in]	fYXRatio	\a Y/X ratio
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetAxesRatio(float fYXRatio) = 0;

	//==============================================================================
	// Method Name: GetAxesRatio()
	//------------------------------------------------------------------------------
	/// Retrieves ratio between screen lengths of units of \a Y and \a X axes
	///
	/// \param[out]	pfYXRatio	\a Y/X ratio
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAxesRatio(float *pfYXRatio) = 0;

	//==============================================================================
	// Method Name: SetSectionPolygonItem()
	//------------------------------------------------------------------------------
	/// Sets a polygon item to be used to draw the section polygon
	///
	/// \param[in]	pPolygonItem		polygon item to be used to draw the section polygon
	///									(line color and fill color only are used).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSectionPolygonItem(IMcPolygonItem *pPolygonItem) = 0;

	//==============================================================================
	// Method Name: GetSectionPolygonItem()
	//------------------------------------------------------------------------------
	/// Retrieves a polygon item used to draw the section polygon
	///
	/// \param[out]	ppPolygonItem		polygon item used to draw the section polygon
	///									(line color and fill color only are relevant).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSectionPolygonItem(IMcPolygonItem **ppPolygonItem) = 0;
//@}

/// \name Section Coordinate Conversions
//@{
	//==============================================================================
	// Method Name: SectionToWorld()
	//------------------------------------------------------------------------------
	/// Converts a section coordinate system point into a regular world point
	///
	/// \param[in]	SectionPoint	section coordinate system point (\a x and \a y only are used).
	/// \param[out]	pWorldPoint		regular world point.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SectionToWorld(const SMcVector3D &SectionPoint, SMcVector3D *pWorldPoint) = 0;

	//==============================================================================
	// Method Name: WorldToSection()
	//------------------------------------------------------------------------------
	/// Converts a regular world point into a section coordinate system point 
	///
	/// \param[in]	WorldPoint		regular world point.
	/// \param[out]	pSectionX		\a X coordinate of a section's point 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode WorldToSection(const SMcVector3D &WorldPoint, double *pSectionX) = 0;

	//==============================================================================
	// Method Name: GetSectionHeightAtPoint()
	//------------------------------------------------------------------------------
	/// Retrieves a section's height (\a Y) and slope at given point (\a X)
	///
	/// \param[in]	dX		\a X coordinate of a section's point to query
	/// \param[out]	pdY		height (\a Y coordinate) of the point
	/// \param[out]	pdSlope	optional slope at the point; can be NULL (default) if unnecessary
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSectionHeightAtPoint(double dX, double *pdY, double *pdSlope = NULL) = 0;

	//==============================================================================
	// Method Name: GetHeightLimits()
	//------------------------------------------------------------------------------
	/// Retrieves height limits of a section's part interval by \a X limits
	///
	/// \param[in]	dMinX	\a X left limit
	/// \param[in]	dMaxX	\a X right limit
	/// \param[out]	pdMinY	\a Y lower limit
	/// \param[out]	pdMaxY	\a Y upper limit
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetHeightLimits(double dMinX, double dMaxX, double *pdMinY, double *pdMaxY) = 0;
//@}
};
