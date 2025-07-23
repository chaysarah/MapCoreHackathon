#pragma once
//===========================================================================
/// \file IMcGeographicCalculations.h
/// The Geographic and world-based calculations interface
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "SMcVector.h"
#include "IMcDestroyable.h"
#include "IMcGridCoordinateSystem.h"
#include "CMcDataArray.h"
#include "CMcTime.h"


//===========================================================================
// Interface Name: IMcGeographicCalculations
//---------------------------------------------------------------------------
/// Interface for Geographical Calculations
//===========================================================================
class IMcGeographicCalculations : public IMcDestroyable
{
protected:
	virtual ~IMcGeographicCalculations() {};
public:

	/// Rectangle corner meaning
	enum ERectangleCorner
	{
		ERC_LEFT_UP,	///< left-up
		ERC_RIGHT_UP,	///< right-up
		ERC_RIGHT_DOWN,	///< right-down
		ERC_LEFT_DOWN	///< left-down
	};

	/// Ellipse or arc parameters structure
	struct SEllipseArc
	{
		/// \n
		SEllipseArc(
			SMcVector3D _Center = v3Zero, double _dRadiusX = 1, double _dRadiusY = 1,
			double _dRotationAngle = 0, double _dInnerRadiusFactor = 0, bool _bClockWise = true,
			double _dStartAzimuth = 0, double _dEndAzimuth = 360) :
			Center(_Center), 
			dRadiusX(_dRadiusX),
			dRadiusY(_dRadiusY),
			dRotationAngle(_dRotationAngle),
			dInnerRadiusFactor(_dInnerRadiusFactor),
			bClockWise(_bClockWise),
			dStartAzimuth(_dStartAzimuth),
			dEndAzimuth(_dEndAzimuth)
		{}

		SMcVector3D Center;				///< the point coordinate of the center point.				
		double		dRadiusX;			///< the radius of the ellipse in x axis direction.		
		double      dRadiusY;			///< the radius of the ellipse in Y axis direction.
		double      dRotationAngle;		///< ellipse Yaw angle. The azimuth of the ellipse Y axis
		double      dInnerRadiusFactor;	///< inner radius factor. Must be between 0.0 to 1.0.
		bool		bClockWise;			///< is the sample direction is clockWise or not.
		double      dStartAzimuth;		///< azimuth of first sampled point before a rotation with the ellipse Yaw angle.
		double      dEndAzimuth;		///< azimuth of last sampled point before a rotation with the ellipse Yaw angle.
	};

	struct SMagneticElements {
		double dDecl;       /// Angle between the magnetic field vector and true north, positive east
		double dIncl;       /// Angle between the magnetic field vector and the horizontal plane, positive down
		double dF;          /// Magnetic Field Strength
		double dH;          /// Horizontal Magnetic Field Strength
		double dX;          /// Northern component of the magnetic field vector
		double dY;          /// Eastern component of the magnetic field vector
		double dZ;          /// Downward component of the magnetic field vector
		double dDecldot;    /// Yearly Rate of change in declination
		double dIncldot;    /// Yearly Rate of change in inclination
		double dFdot;       /// Yearly rate of change in Magnetic field strength
		double dHdot;       /// Yearly rate of change in horizontal field strength
		double dXdot;       /// Yearly rate of change in the northern component
		double dYdot;       /// Yearly rate of change in the eastern component
		double dZdot;       /// Yearly rate of change in the downward component
		double dGVdot;      /// Yearly rate of change in grid variation
	};

	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates Geographical Calculations interface
	///
	/// \param[out]	ppGeoCalc					Geographical Calculations interface created
	/// \param[in]	pGridCoordinateSystem		Grid coordinate system
	///
	/// \return
	///     - status result
	//==============================================================================
	static GEOGRAPHICCALCULATIONS_API IMcErrors::ECode Create(IMcGeographicCalculations **ppGeoCalc,
		const IMcGridCoordinateSystem *pGridCoordinateSystem);

	//@}

	/// \name Geographical Calculations
	//@{

	//=================================================================================================
	// 
	// Function name: CalcMagneticElements(...)
	// 
	//-------------------------------------------------------------------------------------------------
	/// Returns the calculated magnetic elements for the given point and date.
	///
	/// \param [in]		Point						the point.
	/// \param [in]		Date						the Date.			
	/// \param [out]	pGeoMagneticElements		The calculated magnetic elements.
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode CalcMagneticElements(const SMcVector3D &Point, const CMcTime &Date, SMagneticElements *pGeoMagneticElements) = 0;

	//=================================================================================================
	// 
	// Function name: SetCheckGridLimits(...)
	// 
	// Author     : Omer Shelef
	// Date       : 19/12/2010
	//-------------------------------------------------------------------------------------------------
	/// Sets whether to disallow calculations beyond grid limits (disallow extended zones).
	///
	/// \param [in]		bCheckGridLimits	whether to disallow calculations beyond grid limits;
	///										the default is false (allow calculations beyond grid limits)
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetCheckGridLimits(bool bCheckGridLimits) = 0;

	//=================================================================================================
	// 
	// Function name: GetCheckGridLimits(...)
	// 
	// Author     : Omer Shelef
	// Date       : 19/12/2010
	//-------------------------------------------------------------------------------------------------
	/// Retrieves whether calculations beyond grid limits are disallowed (extended zones are disallowed) 
	///
	/// \param [out]	pbCheckGridLimits	whether calculations beyond grid limits are disallowed
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetCheckGridLimits(bool *pbCheckGridLimits) const = 0;

	//=================================================================================================
	// 
	// Function name: AzimuthAndDistanceBetweenTwoLocations(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/3/2009
	//-------------------------------------------------------------------------------------------------
	/// Measures the geodetic distance and azimuth between two points along the ellipsoid sphere.
	///
	/// \param [in]		SourceLocation		the location of the source point.
	/// \param [in]		TargetLocation		the location of the destination point.
	/// \param [out]	pdAzimuth			reference to the returned azimuth between the two points (pass `NULL` if unnecessary).
	/// \param [out]	pdDistance			reference to the returned geodetic distance between 
	///										the two points along the ellipsoid sphere (pass `NULL` if unnecessary).
	/// \param [in]		bUseHeights			Uses locations heights for distance if TRUE.
	/// \return
	///     - status result
	/// \note
	/// If heights are not used than distance is calculated along the ellipsoid (as if height is 0).
	/// If heights are used than distance is calculated along an ellipsoid that passes 
	///	along the average height of the two points.
	//=================================================================================================
	virtual IMcErrors::ECode AzimuthAndDistanceBetweenTwoLocations( 
		const SMcVector3D &SourceLocation, 
		const SMcVector3D &TargetLocation,
		double *pdAzimuth, double *pdDistance = NULL,
		bool bUseHeights = false) const = 0;

	//=================================================================================================
	// 
	// Function name: LocationFromAzimuthAndDistance(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/3/2009
	//-------------------------------------------------------------------------------------------------
	/// Measures the point reached when moving from a given point in a given distance and direction, 
	///	along the ellipsoid sphere.
	///
	/// \param [in]		SourceLocation		the location of the source point.
	/// \param [in]		dAzimuth			the azimuth between the two points. 
	/// \param [in]		dDistance			the geodetic distance between 
	///										the two points along the ellipsoid sphere.
	/// \param [out]	pTargetLocation		reference to the returned location of the destination point.
	/// \param [in]		bUseHeights	Uses locations heights for distance if TRUE.
	/// \return
	///     - status result
	/// \note
	/// If heights are not used than distance is calculated along the ellipsoid (as if height is 0).
	/// If heights are used than distance is calculated along an ellipsoid that passes 
	///	along the height of the \a SourceLocation.
	//=================================================================================================
	virtual IMcErrors::ECode LocationFromAzimuthAndDistance(const SMcVector3D &SourceLocation, 
		double dAzimuth,
		double dDistance,
		SMcVector3D *pTargetLocation,
		bool bUseHeights = false) const = 0;

	//=================================================================================================
	// 
	// Function name: ArcSample(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/3/2009
	//-------------------------------------------------------------------------------------------------
	/// Returns the arc points according to the arc parameters.
	///
	/// \param [in]		sArc							the arc parameters structure.
	/// \param [in]		uFullEllipseReqPoints			the number of points to sample for
	///													a full ellipse, if the arc is partial the 
	///													points sampled are proportional.			
	/// \param [out]	paArcPoints						The sampled points.
	/// \param [in]		bIsGeometric					Whether geometric or geographic arc should be sampled;
	///													the default is false (geographic)
	/// \return
	///     - status result
	/// \note
	/// - The result can be used as a polyline for geometric calculations.
	//=================================================================================================
	virtual IMcErrors::ECode ArcSample(const SEllipseArc &sArc,
		UINT uFullEllipseReqPoints,
		CMcDataArray<SMcVector3D> *paArcPoints, bool bIsGeometric = false) const = 0;


	//=================================================================================================
	// 
	// Function name: IsPointInArc(...)
	// 
	// Author     : Omer Shelef
	// Date       : 10/10/2010
	//-------------------------------------------------------------------------------------------------
	/// Returns if a point is inside an arc.
	///
	/// \param [in]		Point							the Point.
	/// \param [in]		sArc							the arc parameters structure.
	/// \param [out]	pIsIn							Returns if the point is inside an arc.
	/// \return
	///     - status result
	/// \note
	//=================================================================================================
	virtual IMcErrors::ECode IsPointInArc(const SMcVector3D &Point,
		const IMcGeographicCalculations::SEllipseArc &sArc,
		bool *pIsIn) const = 0;

	//=================================================================================================
	// 
	// Function name: LineSample(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/3/2009
	//-------------------------------------------------------------------------------------------------
	/// Samples points along a great circle (geodetic line) connecting 2 given points.
	///
	/// \param [in]	    StartPoint			Great circle's start point.			
	/// \param [in]		EndPoint			Great circle's end point.	
	/// \param [in]		dMaxError			The maximal error in meters allowed between the returned 
	///										polyline and the actual great circle line. 
	///										This parameters affects the number of points that will be sampled.
	/// \param [out]	paLinePoints		The sampled points.
	/// \return
	///     - status result
	/// \note
	/// - The result can be used as a polyline for geometric calculations.
	//=================================================================================================
	virtual IMcErrors::ECode LineSample(const SMcVector3D &StartPoint, const SMcVector3D &EndPoint,
										double dMaxError, CMcDataArray<SMcVector3D> *paLinePoints) const = 0;


	//=================================================================================================
	// 
	// Function name: PolyLineLength(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/3/2009
	//-------------------------------------------------------------------------------------------------
	/// Calculates polyline's distance over the ellipsoid, with or without point's heights. (but without DTM)
	///
	/// \param [in]	    aLineVertices	The polyline's vertices
	/// \param[in]		uNumVertices	number of line vertices
	/// \param [out]	pDist			The returned Polyline's length.
	/// \param [in]		bUseHeights		Uses locations heights for distance if TRUE.
	/// \return
	///     - status result
	/// \note
	/// If heights are not used than length is calculated along the ellipsoid (as if height is 0).
	/// If heights are used than length is calculated along an ellipsoid that passes 
	///	along the average height of each two following points of the polyline. 
	/// In both cases Calculations do not consider DTM data.
	//=================================================================================================
	virtual IMcErrors::ECode PolyLineLength(const SMcVector3D aLineVertices[], 
		UINT	uNumVertices,
		double *pDist,
		bool	bUseHeights = false) const = 0;

	//=================================================================================================
	// 
	// Function name: PolygonSphericArea(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/3/2009
	//-------------------------------------------------------------------------------------------------
	/// Calculates polygon's area over the ellipsoid and ensures there is no self-intersection.
	///
	/// In case of self-intersection returns false in pbNoSelfIntersection and DBL_MAX in pdArea.
	///
	/// \param [in]	    aPolygonVertices		The polygon.
	/// \param[in]		uNumVertices			The number of line vertices.
	/// \param [in]		dEarthLocalRadius		The Earth radius to use for calculation; CalcLocalRadius() can be used to calculate it.
	///											If smaller or equal to zero, the radius used will be calculated 
	///											using the local radius of the average point of the polygon.
	/// \param [out]	pbNoSelfIntersection	Whether no-self-intersection test succeeded
	/// \param [out]	pdArea					The polygon's area calculated.
	/// \return
	///     - status result
	/// \note
	/// Calculations do not consider DTM data.
	//=================================================================================================
	virtual IMcErrors::ECode PolygonSphericArea(const SMcVector3D aPolygonVertices[], UINT uNumVertices, double dEarthLocalRadius, 
		bool *pbNoSelfIntersection, double *pdArea) const = 0;

	//=================================================================================================
	// 
	// Function name: STATUS_t , CalcLocalRadius(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/3/2009
	//-------------------------------------------------------------------------------------------------
	/// Calculates a local mean radius of the Earth ellipsoid at a specified point.
	///
	/// \param [in]	    Point				The point.
	/// \param [out]	pdLocalMeanRadius	The Earth local mean radius at the point.
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode CalcLocalRadius(const SMcVector3D &Point,
		double *pdLocalMeanRadius) const = 0;

	//=================================================================================================
	// 
	// Function name: STATUS_t , SGCalcLocalRadius(...)
	// 
	// Author     : Omer Shelef
	// Date       : 23/8/2009
	//-------------------------------------------------------------------------------------------------
	/// Calculates a local radius of the Earth ellipsoid at a specified point in a direction 
	/// defined by a specified azimuth.
	///
	/// \param [in]	    Point					The point.
	/// \param [in]	    dAzimuth				The azimuth defining the direction the radius 
	///											is requested in.
	/// \param [out]	pdLocalAzimuthRadius	The Earth local radius at the point in a direction 
	///											of the azimuth.
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode CalcLocalAzimuthRadius (const SMcVector3D &Point,
		double dAzimuth, double *pdLocalAzimuthRadius) const = 0;

	//=================================================================================================
	// 
	// Function name: CalcSunDirection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/3/2009
	//-------------------------------------------------------------------------------------------------
	/// Checks if polyline crosses the date line (180 degrees Meridian) and if so splits to western and eastern parts.
	///
	/// \param [in]	    nYear			The Year		
	/// \param [in]	    nMonth			The Month		
	/// \param [in]	    nDay			The Day		
	/// \param [in]	    nHour			The Hour		
	/// \param [in]	    nMin			The Minutes		
	/// \param [in]	    nSec			The Seconds		
	/// \param [in]	    fTimeZone		The TimeZone	
	/// \param [in]	    Location		The Location
	/// \param [out]	pdSunAzimuth	The calculated sun azimuth (pass `NULL` if unnecessary)
	/// \param [out]	pdSunElevation	The calculated sun elevation (pass `NULL` if unnecessary).
	/// \return
	///     - status result
	/// \note
	///		Unimplemented.
	/// The input coordinates is in MapCore's degrees format, meaning decimal degrees times 100000.
	//=================================================================================================
	virtual IMcErrors::ECode CalcSunDirection(int nYear, int nMonth, int nDay,
		int nHour, int nMin, int nSec, float fTimeZone, 
		const SMcVector3D &Location, 
		double *pdSunAzimuth, double *pdSunElevation = NULL) const = 0;



	//=================================================================================================
	// 
	// Function name: LocationFromLocationAndVector(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/3/2009
	//-------------------------------------------------------------------------------------------------
	/// Measures the point reached when moving from a given point in a given distance azimuth and elevation.
	///
	/// \param [in]		SourceLocation		the location of the source point.
	/// \param [in]		dVectorLengthInMeters	the length of the vector in meters.
	/// \param [in]		dVectorAzimuth			The vectors Azimuth angle in degrees. 
	/// \param [in]		dVectorElevation		The vectors elevation angle in degrees. 
	/// \param [out]	pTargetLocation			the location of the target point.
	/// \return
	///     - status result
	/// \note
	/// The azimuth and elevation are local to the source location.
	/// The calculation is preformed on a straight vector without DTM information.
	/// elevationAngle = 0: horizon, 
	/// elevationAngle > 0: above the horizon
	///	elevationAngle < 0: below the horizon.
	//=================================================================================================
	virtual IMcErrors::ECode LocationFromLocationAndVector(const SMcVector3D &SourceLocation, 
		double	 dVectorLengthInMeters,
		double	 dVectorAzimuth,
		double	 dVectorElevation,	
		SMcVector3D *pTargetLocation) const = 0;

	//=================================================================================================
	// 
	// Function name: VectorFromTwoLocations(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/3/2009
	//-------------------------------------------------------------------------------------------------
	/// Measures the vector between two points.
	///
	/// \param [in]		SourceLocation			the location of the source point.
	/// \param [in]		TargetLocation			the location of the destination point.
	/// \param [out]	pdVectorLengthInMeters	the length of the vector in meters (pass `NULL` if unnecessary).
	/// \param [out]	pdVectorAzimuth			The vectors Azimuth angle in degrees (pass `NULL` if unnecessary). 
	/// \param [out]	pdVectorElevation		The vectors elevation angle in degrees (pass `NULL` if unnecessary). 
	/// \return
	///     - status result
	/// \note
	/// The azimuth and elevation are local to the source location.
	/// The calculation is preformed on a straight vector without DTM information.
	/// elevationAngle = 0: horizon, 
	/// elevationAngle > 0: above the horizon
	///	elevationAngle < 0: below the horizon.
	//=================================================================================================
	virtual IMcErrors::ECode VectorFromTwoLocations(const SMcVector3D &SourceLocation,
		const SMcVector3D &TargetLocation,	
		double	 *pdVectorLengthInMeters,
		double	 *pdVectorAzimuth = NULL,
		double	 *pdVectorElevation = NULL) const = 0;


	//=================================================================================================
	// 
	// Function name: LocationFromTwoRays(...)
	// 
	// Author     : Omer Shelef
	// Date       : 6/2/2013
	//-------------------------------------------------------------------------------------------------
	/// Calculates a location from two rays intersection, or if they do not intersect their closest point.
	///
	/// \param [in]		FstRayOrigin			the origin of the first ray.
	/// \param [in]		FstRayOrientation		the orientation of the first ray.
	/// \param [in]		SndRayOrigin			the origin of the second ray.
	/// \param [in]		SndRayOrientation		the orientation of the second ray.
	/// \param [out]	pdRaysOriginDistance	the distance between the two rays origins in meters (pass `NULL` if unnecessary).
	/// \param [out]	pdRaysShortestDistance	the distance between the two rays closest points in meters (pass `NULL` if unnecessary).
	/// \param [out]	pLocation				The Location of the two rays intersection or closest point (pass `NULL` if unnecessary). 
	/// \param [in]		bOrientationsAsLocations If true: orientations are given as locations along the ray,
	///											 If false: orientations are vectors in 3D space.
	/// \return
	///     - status result
	/// \note
	/// The orientations are local to the source location.
	/// The calculation is preformed on a straight vector without DTM information.
	//=================================================================================================
	virtual IMcErrors::ECode LocationFromTwoRays(
		const SMcVector3D &FstRayOrigin,
		const SMcVector3D &FstRayOrientation,
		const SMcVector3D &SndRayOrigin,
		const SMcVector3D &SndRayOrientation,
		double		*pdRaysOriginDistance,
		double		*pdRaysShortestDistance = NULL,
		SMcVector3D	*pLocation = NULL,
		bool		bOrientationsAsLocations  = false ) const =0;

	//=================================================================================================
	// 
	// Function name: CirclesIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 11/3/2013
	//-------------------------------------------------------------------------------------------------
	/// Calculates intersection for two geographical circles.
	///
	/// \param [in]		FstCenter				the center of the first circle.
	/// \param [in]		dFstRadius				the radius of the first circle.
	/// \param [in]		SndCenter				the center of the second circle.
	/// \param [in]		dSndRadius				the radius of the second circle.
	/// \param [in]		bCheckOnlyFstAzimuth	If true, only the intersection point closer to \a dFstAzimuth will return.
	/// \param [in]		dFstAzimuth				If bCheckOnlyFstAzimuth true, determines which intersection point will be returned
	/// \param [out]	pnNumOfIntersections	returns the number of intersection points found: 0, 1 or 2 (pass `NULL` if unnecessary). 
	/// \param [out]	pFstIntersection		The first intersection point (pass `NULL` if unnecessary).
	/// \param [out]	pSndIntersection		The second intersection point (pass `NULL` if unnecessary).
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode CirclesIntersection(
		const SMcVector3D	&FstCenter,
		double				dFstRadius,
		const SMcVector3D	&SndCenter,
		double				dSndRadius,
		bool				bCheckOnlyFstAzimuth,
		double				dFstAzimuth,
		UINT				*pnNumOfIntersections,
		SMcVector3D			*pFstIntersection = NULL,
		SMcVector3D			*pSndIntersection = NULL)  const =0;

	//=================================================================================================
	// 
	// Function name: CalcRectangleFromCenterAndLengths(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/3/2009
	//-------------------------------------------------------------------------------------------------
	/// Calculates rectangle's vertices according to its center, width, height and azimuth.
	///
	/// \param [in]		RectangleCenterPoint		Rectangle center point
	/// \param [in]		dRectangletHeight			Rectangle height
	/// \param [in]		dRectangleWidth				Rectangle width
	/// \param [in]		dRotationAzimutDeg			Rotation azimuth of the height in degrees  
	/// \param [out]	pLeftUp						rectangle left upper point
	/// \param [out]	pRightUp					rectangle right upper point
	/// \param [out]	pRightDown					rectangle right lower point	
	/// \param [out]	pLeftDown					rectangle left lower point
	/// \param [in]		bIsGeometric				When true calculation is geometrical  
	/// \return
	///     - status result
	/// \note
	///	
	//=================================================================================================
	virtual IMcErrors::ECode CalcRectangleFromCenterAndLengths(
		const SMcVector3D	&RectangleCenterPoint,
		double 				dRectangletHeight,
		double 				dRectangleWidth, 
		double 				dRotationAzimutDeg,
		SMcVector3D 		*pLeftUp,
		SMcVector3D 		*pRightUp,
		SMcVector3D 		*pRightDown,
		SMcVector3D 		*pLeftDown,
		bool	bIsGeometric = false) const = 0;



	//=================================================================================================
	// 
	// Function name: CalcCenterAndLengthsFromRectangle(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/3/2009
	//-------------------------------------------------------------------------------------------------
	/// Calculates rectangle's center, width, height and azimuth according to its vertexes.
	///
	/// \param [in]		LeftUp						rectangle left upper point
	/// \param [in]		RightUp						rectangle right upper point
	/// \param [in]		RightDown					rectangle right lower point	
	/// \param [in]		LeftDown					rectangle left lower point
	/// \param [out]	pRectangleCenterPoint		Rectangle center point
	/// \param [out]	pdRectangletHeight			Rectangle height
	/// \param [out]	pdRectangleWidth			Rectangle width
	/// \param [out]	pdRotationAzimutDeg			Rotation azimuth of the rectangle in degrees  
	/// \param [in]		bIsGeometric				When true calculation is geometrical  
	/// \return
	///     - status result
	/// \note
	///	
	//=================================================================================================
	virtual IMcErrors::ECode CalcCenterAndLengthsFromRectangle(
		const SMcVector3D	&LeftUp,
		const SMcVector3D	&RightUp,
		const SMcVector3D	&RightDown,
		const SMcVector3D	&LeftDown,
		SMcVector3D			*pRectangleCenterPoint,
		double	 			*pdRectangletHeight,
		double 	 			*pdRectangleWidth, 
		double 	 			*pdRotationAzimutDeg,
		bool	bIsGeometric = false) const = 0;

	//=================================================================================================
	// 
	// Function name: CalcCenterAndLengthsFromRectangle(...)
	// 
	// Author     : Omer Shelef
	// Date       : 25/10/2011
	//-------------------------------------------------------------------------------------------------
	/// Calculates rectangle's center, width, height according to two of its vertexes and azimuth .
	///
	/// \param [in]		LeftUp						rectangle left upper point
	/// \param [in]		RightDown					rectangle right lower point	
	/// \param [in]		dRotationAzimutDeg			Rotation azimuth of the rectangle in degrees  
	/// \param [out]	pRectangleCenterPoint		Rectangle center point
	/// \param [out]	pdRectangletHeight			Rectangle height
	/// \param [out]	pdRectangleWidth			Rectangle width
	/// \param [in]		bIsGeometric				When true calculation is geometrical  
	/// \return
	///     - status result
	/// \note
	///	
	//=================================================================================================
	virtual IMcErrors::ECode CalcCenterAndLengthsFromRectangle(
		const SMcVector3D	&LeftUp,
		const SMcVector3D	&RightDown, 
		double 				dRotationAzimutDeg,
		SMcVector3D			*pRectangleCenterPoint,
		double				*pdRectangletHeight,
		double 				*pdRectangleWidth,
		bool				bIsGeometric = false) const =0;

	//==============================================================================
	// Method Name: CalcRectangleCenterFromCornerAndLengths()
	//------------------------------------------------------------------------------
	/// Calculates rectangle center according to one specified corner, lengths and azimuth
	///
	/// \param[in]	RectangleCornerPoint	rectangle corner point
	/// \param[in]	dRectangletHeight		rectangle height
	/// \param[in]	dRectangleWidth			rectangle width
	/// \param[in]	dRotationAzimutDeg		rectangle azimuth in degrees  
	/// \param[in]	eCornerMeaning			rectangle corner meaning
	/// \param[out]	pCenterPoint			rectangle center
	/// \param[in]	bIsGeometric			when true calculation is geometrical  
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode CalcRectangleCenterFromCornerAndLengths(
		const SMcVector3D &RectangleCornerPoint,
		double dRectangletHeight,
		double dRectangleWidth, 
		double dRotationAzimutDeg,
		ERectangleCorner eCornerMeaning,
		SMcVector3D *pCenterPoint,
		bool bIsGeometric = false) const =0;

	//=================================================================================================
	// 
	// Function name: PolygonExpand(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/3/2009
	//-------------------------------------------------------------------------------------------------
	/// Builds polygon expansion.
	///
	/// \param [in]	    aPolygonVertices			The polygon
	/// \param[in]		uNumVertices				number of polygon's vertices
	/// \param [in]		dExpansionDistance			The distance to expand the polygon
	/// \param [in]		uNumPointsInArc				Number of point per arc.
	/// \param [out]	paExpandedPolygon			output expanded polygon (with curves) points 
	/// \return
	///     - status result 
	/// \note
	///	the expansion is done only on convex polygons.
	/// If the input polygon is concave than the function calculates the convex hull of the polygon
	///  and than expands it
	//=================================================================================================
	virtual IMcErrors::ECode PolygonExpand(
		const SMcVector3D			aPolygonVertices[], 
		UINT						uNumVertices,
		double						dExpansionDistance,
		UINT						uNumPointsInArc,	
		CMcDataArray<SMcVector3D>	*paExpandedPolygon) const = 0;


	//=================================================================================================
	// 
	// Function name: PolylineExpand(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/3/2009
	//-------------------------------------------------------------------------------------------------
	/// Builds line expansion polygon.
	///
	/// \param [in]	    aPolylineVertices			The polyline
	/// \param[in]		uNumVertices				number of polyline's vertices
	/// \param [in]		dExpansionDistance			The distance to expand the polyline
	/// \param [in]		uNumPointsInArc				Number of point per arc.
	/// \param [out]	paExpandedPolygon			output expanded polygon (with curves) points 
	/// \return
	///     - status result 
	/// \note
	//=================================================================================================
	virtual IMcErrors::ECode PolylineExpand(
		const SMcVector3D aPolylineVertices[], 
		UINT uNumVertices,
		double dExpansionDistance,
		UINT	uNumPointsInArc,	
		CMcDataArray<SMcVector3D> *paExpandedPolygon) const = 0;


	//=================================================================================================
	// 
	// Function name: IsPointOn2DLine(...)
	// 
	// Author     : Omer Shelef
	// Date       : 22/3/2010
	//-------------------------------------------------------------------------------------------------
	/// Indicates whether a point is on a geodetic line. 
	/// \param [in]		Point				The point to check
	/// \param [in]	    aPolylineVertices	The polyline
	/// \param [in]		uNumVertices		number of polyline's vertices
	/// \param [in]		sLineAccuracy		If point's distance from line is shorter than accuracy, 
	///										it is considered to be on the line.
	/// \param [out]	pbOnLine			TRUE if point is on polyline
	/// \return
	///     - status result 
	/// \note
	//=================================================================================================
	virtual IMcErrors::ECode IsPointOn2DLine (const SMcVector3D &Point,
		const SMcVector3D aPolylineVertices[], 
		UINT uNumVertices,
		short  sLineAccuracy,
		bool *pbOnLine) const = 0;

	//=================================================================================================
	// 
	// Function name: ShortestDistPoint2DLine(...)
	// 
	// Author     : Omer Shelef
	// Date       : 22/3/2010
	//-------------------------------------------------------------------------------------------------
	/// Measures the shortest distance along the ellipsoid sphere, between a point and a polyline.
	/// \param [in]		Point				The point to check
	/// \param [in]	    aPolylineVertices	The polyline
	/// \param [in]		uNumVertices		number of polyline's vertices
	/// \param [out]	pNearestPoint		Returns a point on the polyline which is closest to the point (pass `NULL` if unnecessary)
	/// \param [out]	pDist				Returns the distance between the point and the polyline (pass `NULL` if unnecessary)
	/// \return
	///     - status result 
	/// \note
	//=================================================================================================
	virtual IMcErrors::ECode ShortestDistPoint2DLine(const SMcVector3D &Point,
		const SMcVector3D aPolylineVertices[], 
		UINT uNumVertices,
		SMcVector3D *pNearestPoint,
		double *pDist = NULL) const = 0;

	//=================================================================================================
	// 
	// Function name: ShortestDistPointArc(...)
	// 
	// Author     : Omer Shelef
	// Date       : 22/3/2010
	//-------------------------------------------------------------------------------------------------
	/// Measures the shortest distance along the ellipsoid sphere, between a point and an arc.
	/// \param [in]		Point				the point to be checked.
	/// \param [in]		Arc					the arc parameters structure.
	/// \param [out]	pNearestPoint		Returns a point on the arc which is closest to the point (pass `NULL` if unnecessary).
	/// \param [out]	pDist				Returns the distance between the point and the arc (pass `NULL` if unnecessary).
	/// \return
	///     - status result 
	/// \note
	//=================================================================================================
	virtual IMcErrors::ECode ShortestDistPointArc(const SMcVector3D &Point,
		const SEllipseArc &Arc,
		SMcVector3D *pNearestPoint,
		double *pDist = NULL) const = 0;


	//=================================================================================================
	// 
	// Function name: void,  ConvertAzimuthFromGridToGeo(...)
	// 
	// Author     : Omer Shelef
	// Date       : 18/4/2010
	//-------------------------------------------------------------------------------------------------
	/// Converts azimuth from another coordinate system to this coordinate system azimuth.
	/// \param [in]		  OriginLocation	the  location of the azimuth origin.
	/// \param [in]		  bIsLocationInOtherCoordSys determines if the OriginLocation is in other coordinate system or this coordinate system.
	/// \param [in]		  OtherCoordSys		the coordinate system to convert from.
	/// \param [in]		  dOtherCoordSysAzimuth		the azimuth to convert from.
	/// \param [out]	  pdThisCoordSysAzimuth		the output azimuth in this coordinate system.				
	/// \return
	///     - status result 
	//=================================================================================================
	virtual IMcErrors::ECode ConvertAzimuthFromOtherCoordSys(
		const SMcVector3D &OriginLocation, bool bIsLocationInOtherCoordSys,
		const IMcGridCoordinateSystem *OtherCoordSys, 	double dOtherCoordSysAzimuth, 
		double *pdThisCoordSysAzimuth) const = 0;

	//=================================================================================================
	// 
	// Function name: void,  ConvertAzimuthToOtherCoordSys(...)
	// 
	// Author     : Omer Shelef
	// Date       : 18/4/2010
	//-------------------------------------------------------------------------------------------------
	/// Converts azimuth to another coordinate system from this coordinate system azimuth.
	/// \param [in]		  OriginLocation	the  location of the azimuth origin.
	/// \param [in]		  bIsLocationInOtherCoordSys determines if the OriginLocation is in other coordinate system or this coordinate system.
	/// \param [in]		  OtherCoordSys		the coordinate system to convert to.
	/// \param [in]		  dThisCoordSysAzimuth		the azimuth to convert from.
	/// \param [out]	  pdOtherCoordSysAzimuth		the output azimuth in this coordinate system.				
	/// \return
	///     - status result 
	//=================================================================================================
	virtual IMcErrors::ECode ConvertAzimuthToOtherCoordSys(
		const SMcVector3D &OriginLocation, bool bIsLocationInOtherCoordSys,
		const IMcGridCoordinateSystem *OtherCoordSys, 	double dThisCoordSysAzimuth, 
		double *pdOtherCoordSysAzimuth) const = 0;


	//=================================================================================================
	// 
	// Function name: void,  ConvertAzimuthFromGridToGeo(...)
	// 
	// Author     : Omer Shelef
	// Date       : 18/4/2010
	//-------------------------------------------------------------------------------------------------
	/// Converts this grid azimuth to Geographic azimuth.
	/// \param [in]		  OriginLocation					the location of the azimuth origin.
	/// \param [in]		  dGridAzimuth						the grid azimuth.
	/// \param [out]	  pdGeoAzimuth						the output geographic azimuth in degrees.				
	/// \param [in]		  IsOriginLocationInGeoCoordinates	whether the location specified is in
	///														Geographic coordinate system (like 
	///														output azimuth) or in that of the input.
	/// \return
	///     - status result 
	/// \remarks
	/// if this coordinate system is geographical than the returned grid-azimuth is equal to the input.
	//=================================================================================================
	virtual IMcErrors::ECode ConvertAzimuthFromGridToGeo(
		const SMcVector3D &OriginLocation,
		double dGridAzimuth, double *pdGeoAzimuth, bool IsOriginLocationInGeoCoordinates) const = 0 ; 

	//=================================================================================================
	// 
	// Function name: void,  ConvertAzimuthFromGeoToGrid(...)
	// 
	// Author     : Omer Shelef
	// Date       : 18/4/2010
	//-------------------------------------------------------------------------------------------------
	/// Converts Geographic azimuth to this grid azimuth.
	/// \param [in]		  OriginLocation					the location of the azimuth origin.
	/// \param [in]		  dGeoAzimuth						the geographic azimuth.
	/// \param [out]	  pdGridAzimuth						the output grid azimuth in degrees.				
	/// \param [in]		  IsOriginLocationInGeoCoordinates	whether the location specified is in
	///														Geographic coordinate system (like 
	///														input azimuth) or in that of the output.
	/// \return
	///     - status result 
	/// \remarks
	/// if this coordinate system is geographical than the returned grid-azimuth is equal to the input.
	//=================================================================================================
	virtual IMcErrors::ECode ConvertAzimuthFromGeoToGrid(
		const SMcVector3D &OriginLocation,
		double dGeoAzimuth, double *pdGridAzimuth, bool IsOriginLocationInGeoCoordinates) const = 0;

	//=================================================================================================
	// 
	// Function name: ConvertHeightFromEllipsoidToGeoid(...)
	// 
	//-------------------------------------------------------------------------------------------------
	/// Converts location's height above this grid's ellipsoid to that above the geoid (above sea level).
	///
	/// \param [in]		EllipsoidLocation	the location with a height above this grid's ellipsoid.
	/// \param [out]	pdGeoidHeight		the location's height above the geoid (above sea level).
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode ConvertHeightFromEllipsoidToGeoid(const SMcVector3D &EllipsoidLocation, double *pdGeoidHeight) = 0;

	//=================================================================================================
	// 
	// Function name: ConvertHeightFromGeoidToEllipsoid(...)
	// 
	//-------------------------------------------------------------------------------------------------
	/// Converts location's height above the geoid (above sea level) to that above this grid's ellipsoid.
	///
	/// \param [in]		GeoidLocation		The location with a height above the geoid (above sea level).
	/// \param [out]	pdEllipsoidHeight	The location's height above this grid's ellipsoid
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode ConvertHeightFromGeoidToEllipsoid(const SMcVector3D &GeoidLocation, double *pdEllipsoidHeight) = 0;

	//=================================================================================================
	// 
	// Function name: void,  LocationsFromTwoLocationsAndDistances(...)
	// 
	// Author     : Omer Shelef
	// Date       : 18/4/2010
	//-------------------------------------------------------------------------------------------------
	/// Given two distances from known locations and one elevation angles, calculates the possible locations.
	/// \param [in]		  FstLocation		the first known location.
	/// \param [in]		  dDistanceFromFst	the first distance from known location.
	/// \param [in]		  dElevationFromFst	the first elevation from known location.
	/// \param [in]		  SndLocation		the second known location.
	/// \param [in]		  dDistanceFromSnd	the second distance from known location.
	/// \param [out]	  pDestLocationsNum	the number of output locations (pass `NULL` if unnecessary).				
	/// \param [out]	  pFstDestLocation	the first output location, if exists (pass `NULL` if unnecessary).				
	/// \param [out]	  pSndDestLocation	the second output location, if exists (pass `NULL` if unnecessary).				
	/// \return
	///     - status result 
	/// \remarks
	/// if this coordinate system is geographical than the returned grid-azimuth is equal to the input.
	//=================================================================================================
	virtual IMcErrors::ECode LocationsFromTwoLocationsAndDistances(const SMcVector3D &FstLocation,
		double dDistanceFromFst, double dElevationFromFst,
		const SMcVector3D &SndLocation, double dDistanceFromSnd,
		UINT *pDestLocationsNum, SMcVector3D *pFstDestLocation = NULL, SMcVector3D *pSndDestLocation = NULL) const = 0;

	//=================================================================================================
	// 
	// Function name: void,  SmallestBoundingRect(...)
	// 
	// Author     : Omer Shelef
	// Date       : 2/5/2011
	//-------------------------------------------------------------------------------------------------
	/// Calculates the minimal rotated bounding rectangle for a set of locations
	/// \param [in]		  aPoints				the set of locations.
	/// \param [in]		  uNumPoints			the number of locations.
	/// \param [in]		  dDeltaAngleToCheck	the delta between bounding rectangle possible rotation angles.
	/// \param [out]	  pCenterPoint			the bounding rectangle's center location (pass `NULL` if unnecessary).
	/// \param [out]	  pdAzimuth				the bounding rectangle's azimuth of the longer side (pass `NULL` if unnecessary).	
	/// \param [out]	  pdLength				the bounding rectangle's longer side length (pass `NULL` if unnecessary).		
	/// \param [out]	  pdWidth				the bounding rectangle's shorter side length (pass `NULL` if unnecessary).				
	/// \param [out]	  pdArea				the bounding rectangle's area (pass `NULL` if unnecessary).					
	/// \return
	///     - status result 
	/// \remarks
	/// The minimal bounding rect is determined as the rotated bounding rect with the minimal area
	//=================================================================================================
	virtual IMcErrors::ECode SmallestBoundingRect(
		const SMcVector3D aPoints[], 
		UINT uNumPoints,
		double dDeltaAngleToCheck,
		SMcVector3D *pCenterPoint,
		double		*pdAzimuth = NULL,
		double		*pdLength = NULL,
		double		*pdWidth = NULL,
		double		*pdArea = NULL) const = 0;


	//=================================================================================================
	// 
	// Function name: void,  BoundingRectAtAngle(...)
	// 
	// Author     : Omer Shelef
	// Date       : 30/12/2012
	//-------------------------------------------------------------------------------------------------
	/// Calculates the rotated bounding rectangle for a set of locations at a given angle
	/// \param [in]		  aPoints				the set of locations.
	/// \param [in]		  uNumPoints			the number of locations.
	/// \param [in]		  dAngle				the angle for the bounding rectangle.
	/// \param [out]	  pCenterPoint			the bounding rectangle's center location (pass `NULL` if unnecessary).
	/// \param [out]	  pdAzimuth				the bounding rectangle's azimuth of the longer side (pass `NULL` if unnecessary)
	/// \param [out]	  pdLength				the bounding rectangle's longer side length (pass `NULL` if unnecessary)	
	/// \param [out]	  pdWidth				the bounding rectangle's shorter side length (pass `NULL` if unnecessary)			
	/// \param [out]	  pdArea				the bounding rectangle's area (pass `NULL` if unnecessary)				
	/// \return
	///     - status result 
	/// \remarks
	/// The minimal bounding rect is determined as the rotated bounding rect with the minimal area
	//=================================================================================================
	virtual IMcErrors::ECode BoundingRectAtAngle(
		const SMcVector3D aPoints[], 
		UINT uNumPoints,
		double dAngle,
		SMcVector3D *pCenterPoint,
		double		*pdAzimuth = NULL,
		double		*pdLength = NULL,
		double		*pdWidth = NULL,
		double		*pdArea = NULL) const = 0;


	//@}
};

//===========================================================================
// Interface Name: IMcTrackSmoother
//---------------------------------------------------------------------------
/// Interface for track smoother
//===========================================================================
class IMcTrackSmoother : public IMcDestroyable
{
protected:
	virtual ~IMcTrackSmoother() {};
public:
	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates track smoother interface
	///
	/// \param[out]	ppTrackSmoother				the track smoother interface created
	/// \param[in]	pGeoCalc					the interface for geographical calculations to be used by track smoother;
	///											pGeoCalc is \b NOT destroyed when IMcTrackSmoother is destroyed.
	///											If NULL then calculation is done geometrically in map units.
	/// \param[in]	dSmoothDistance				the distance in meters that affects smoothing 
	///											(points within this distance from the track will be omitted)
	/// \return
	///     - status result
	//==============================================================================
	static GEOGRAPHICCALCULATIONS_API IMcErrors::ECode Create(IMcTrackSmoother **ppTrackSmoother,
		const IMcGeographicCalculations *pGeoCalc, double dSmoothDistance);
	//@}

	/// \name Smoothing Track
	//@{
	//==============================================================================
	// Method Name: AddPoints()
	//------------------------------------------------------------------------------
	/// Adds original track points
	///
	/// \param[in]	aOriginalPoints		the original points
	/// \param[in]	uNumOriginalPoints	the number of the original points
	//
	/// \note
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode AddPoints(const SMcVector3D aOriginalPoints[], UINT uNumOriginalPoints) = 0;

	//==============================================================================
	// Method Name: GetSmoothedTrack()
	//------------------------------------------------------------------------------
	/// Calculates the smoothed track points
	///
	/// \param[out]	paSmoothedTrackPoints		the smoothed track points calculated
	/// \param[out]	puNumSmoothedTrackPoints	the number of smoothed track points
	//
	/// \note
	///		paSmoothedTrackPoints array is valid only until next call to any of the interface's methods
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSmoothedTrack(const SMcVector3D **paSmoothedTrackPoints, UINT *puNumSmoothedTrackPoints) = 0;

	
	//==============================================================================
	// Method Name: ClearTrack()
	//------------------------------------------------------------------------------
	/// Clears the track for a new track
	///
	/// \param[in]	dSmoothDistance				the distance in meters that affects smoothing 
	///											(points within this distance from the track will be omitted).
	///											If zero than old dSmoothDistance remains.
	//
	/// \note
	///		paSmoothedTrackPoints array is valid only until next call to any of the interface's methods
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ClearTrack(double dSmoothDistance) = 0;

	//@}
};
