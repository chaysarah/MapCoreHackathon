#pragma once
//===========================================================================
/// \file IMcGeometricCalculations.h
/// The Geometric calculations interface
//===========================================================================


#include "McGeometricCalculationsTypes.h"
#include "McExports.h"
#include "IMcErrors.h"
#include "SMcVector.h"
#include "IMcGridCoordinateSystem.h"
#include "CMcDataArray.h"

//===========================================================================
// Interface Name: IMcGeometricCalculations
//---------------------------------------------------------------------------
/// Interface for Geometrical Calculations
//===========================================================================
class IMcGeometricCalculations
{
public:

	///////////////////////////// LINES //////////////////////////////////////////
	//=================================================================================================
	/// \name Line Functions
	//@{
	//=================================================================================================

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EGParallelLine(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Creates a Line parralel to a given base line.
	/// \param [in]		stBaseLine		base line for new parallel line
	/// \param [in]		dDist		distance from base to parallel line
	/// \param [out]	pParallelLine		the new parallel line
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// pParallelLine's points must be allocated for atleast 2 points by user
	/// Negative distance is left when looking from first point of line towards the second point.
	/// Function works in 3D mode. If base line's points can both have same height value for 2D calculation. 
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EGParallelLine(const SMcVector3D stBaseLine[2], 
		double dDist, 
		SMcVector3D pParallelLine[2]);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EGPerpendicularLine(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Creates a Line perpendicular to a given base line.
	/// \param [in]		stBaseLine		base line for new perpendicular line
	/// \param [in]		dDist		distance of perpendicular line
	/// \param [in]		stThroughPoint		point through which perpendicular line (or its continuing vector) will pass
	/// \param [out]	pPerpendicularLine	the new perpendicular line
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// pPerpendicularLine's points must be allocated for atleast 2 points by user
	/// Function works in 3D mode. If base line's points can both have same height value for 2D calculation. 
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EGPerpendicularLine(const SMcVector3D	 stBaseLine[2], 
		double	 dDist, 
		SMcVector3D stThroughPoint, 
		SMcVector3D	 pPerpendicularLine[2]);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode, EG2DLineMove(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Moves a line by a 2D vector.
	/// \param [in, out]  stLine	Line to be moved
	/// \param [in]		  dX		Delta X
	/// \param [in]		  dY		Delta Y
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DLineMove(	SMcVector3D stLine[2], 
		double dX, 
		double dY);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DLineRotate(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Rotates a line.
	/// \param [in, out]  stLine		Line to be rotated
	/// \param [in]		  dAngle		Angles to rotatate by [degrees]
	/// \param [in]		  stBasePoint	Base point to rotate around
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	///  Positive angles rotate counter clockwise.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DLineRotate(SMcVector3D		stLine[2], 
		double		dAngle, 
		SMcVector3D stBasePoint);



	//=================================================================================================
	// 
	// Function name: EG2DIsPointOnLine(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Figures point and line relations.
	/// \param [in]		  stPoint	The point.
	/// \param [in]		  stLine	The Line.
	/// \param [in]		  eLineType	Specifies Line type (see following remark).
	/// \param [in]		  dAccuracy	If point's distance from line is shorter than accuracy, it is considered to be on the line.
	/// \param [out]	  peIsOn	returns the relation between the line and the point.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eLineType are: #EG_LINE, #EG_RAY, #EG_SEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DIsPointOnLine(SMcVector3D	stPoint, 
		SMcVector3D		stLine[2], 
		GEOMETRIC_SHAPE eLineType,
		double		dAccuracy, 
		POINT_LINE_STATUS		*peIsOn);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EGDistancePointLine(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates Distance between point and line
	/// \param [in]		  stLine	The Line.
	/// \param [in]		  eLineType	Specifies Line type (see following remark).
	/// \param [in]		  stPoint	The point.
	/// \param [out]	  pDist		The distance between the point and the line.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eLineType are: #EG_LINE, #EG_RAY, #EG_SEGMENT
	/// When distance is Zero than point is on line.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EGDistancePointLine(SMcVector3D		stLine[2], 
		GEOMETRIC_SHAPE eLineType,
		SMcVector3D   stPoint, 
		double		*pDist);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EGLineDistance(...)
	// 
	// Author     : Omer Shelef
	// Date       : 29/4/2007
	//-------------------------------------------------------------------------------------------------
	/// Calculates Distance between two lines
	/// \param [in]		  stLine1		The first Line.
	/// \param [in]		  eLineType1	Specifies 1st Line type (see following remark).
	/// \param [in]		  stLine2		The second Line.
	/// \param [in]		  eLineType2	Specifies 2nd Line type (see following remark).
	/// \param [out]	  pstClosestPointOn1	A point on first shape which is closest to the second shape.
	/// \param [out]	  pstClosestPointOn2	A point on second shape which is closest to the first shape.
	/// \param [out]	  pDist					The distance between the two shapes.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eLineType1, eLineType2 are: #EG_LINE, #EG_RAY, #EG_SEGMENT
	/// When distance is Zero than point is on line.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EGLineDistance(SMcVector3D		stLine1[2], 
		GEOMETRIC_SHAPE eLineType1,
		SMcVector3D		stLine2[2], 
		GEOMETRIC_SHAPE eLineType2,
		SMcVector3D	*pstClosestPointOn1,
		SMcVector3D	*pstClosestPointOn2,
		double		*pDist);



	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EGSegmentsDistance(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates distance between two segments
	/// \param [in]		  stSegment1			First segment
	/// \param [in]		  stSegment2			Second segment
	/// \param [out]	  pstClosestPointOn1	A point on first shape which is closest to the second shape.
	/// \param [out]	  pstClosestPointOn2	A point on second shape which is closest to the first shape.
	/// \param [out]	  pDist					The distance between the two shapes.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for the two Closest points must be allocated by user.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EGSegmentsDistance(SMcVector3D	 stSegment1[2], 
		SMcVector3D	 stSegment2[2], 
		SMcVector3D *pstClosestPointOn1,
		SMcVector3D *pstClosestPointOn2,
		double	 *pDist);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DSegmentsRelation(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Figures the relation between two segments
	/// \param [in]		  stSegment1			First segment
	/// \param [in]		  stSegment2			Second segment
	/// \param [out]	  pnIntersectPointsNum	Returns numer of intersecting points [0-2]
	/// \param [out]	  pstFstPoint			The 1st intersecting point.
	/// \param [out]	  pstScdPoint			The 2nd intersecting point.
	/// \param [out]	  peStatus				The relation between the segments.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Memory for the two Intersecting points must be allocated by user.
	/// pnIntersectPointsNum equals 0 when lines are parallel or seperate. pnIntersectPointsNum equals 2 when they overlap or intersect parrallel.
	/// Otherwise pnIntersectPointsNum equals 1.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DSegmentsRelation(SMcVector3D		stSegment1[2],
		SMcVector3D		stSegment2[2], 
		int		    *pnIntersectPointsNum,
		SMcVector3D   *pstFstPoint,
		SMcVector3D   *pstScdPoint,
		SL_SL_STATUS *peStatus);

	//@}


	//////////////////////////// ANGLES //////////////////////////////////////////////		

	//=================================================================================================
	/// \name Angles Functions
	//@{
	//=================================================================================================

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EGAngleBetween3Points(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates angle between three points
	/// \param [in]		  stFstPoint
	/// \param [in]		  stMidPoint	Head of the angle
	/// \param [in]		  stScdPoint
	/// \param [out]	  pdAngle		The angle between the three points [degrees]
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Function works in 3D mode. the threee points can both have same height value for 2D calculation. 
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EGAngleBetween3Points(SMcVector3D stFstPoint,
		SMcVector3D stMidPoint,
		SMcVector3D stScdPoint,
		double	   *pdAngle);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DAngleFromX(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates 2D angle from X axis to line.				
	/// \param [in]		  stLine
	/// \param [out]	  pAngle		The angle between the line and X axis [degrees].
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DAngleFromX(SMcVector3D stLine[2], double *pAngle);



	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG3DAngleFromXY(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates 3D angle from XY plane to line.				
	/// \param [in]		  stLine
	/// \param [out]	  pAngle		The angle between the line and XY plane [degrees].
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG3DAngleFromXY(SMcVector3D stLine[2], double *pAngle);


	//@}

	//////////////////////////// CIRCLES ////////////////////////////////
	//=================================================================================================
	/// \name Circles functions
	//@{
	//=================================================================================================


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DTangentsThroughPoint(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates tangents to a circle that pass through a given point.
	/// \param [in]		  stCircleCenter
	/// \param [in]		  dRadius
	/// \param [in]		  stThroughPoint	The point the tangents pass through
	/// \param [out]	  pnTangentsNum		The number of tangents calculated [0-2]
	/// \param [out]	  Tangent1		The 1st tangent calculated.
	/// \param [out]	  Tangent2		The 2nd tangent calculated.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Both tangents points must be allocated for atleast 2 points each by user.
	/// pnTangentsNum equals 0 when point is inside the circle. pnTangentsNum equals 1 when point is on the circle.
	/// Otherwise pnIntersectPointsNum equals 2.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DTangentsThroughPoint( SMcVector3D	stCircleCenter,
		double		dRadius,
		SMcVector3D	stThroughPoint,
		int			*pnTangentsNum,
		SMcVector3D	    Tangent1[2],
		SMcVector3D	    Tangent2[2]);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DTangents2Circles(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates tangents to two circles
	/// \param [in]		     stCircleCenter1
	/// \param [in]		     dRadius1
	/// \param [in]		     stCircleCenter2
	/// \param [in]			 dRadius2
	/// \param [out]	     pnTangentsNum		The number of tangents calculated [0-4]
	/// \param [out]	     Tangent1		The 1st tangent calculated.
	/// \param [out]	     Tangent2		The 2nd tangent calculated.
	/// \param [out]	     Tangent3		The 3rd tangent calculated.
	/// \param [out]	     Tangent4		The 4th tangent calculated.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// All four tangents points must be allocated for atleast 2 points each by user.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DTangents2Circles(	SMcVector3D	stCircleCenter1,
		double		dRadius1,
		SMcVector3D	stCircleCenter2,
		double		dRadius2,
		int			*pnTangentsNum,
		SMcVector3D	    Tangent1[2],
		SMcVector3D	    Tangent2[2],
		SMcVector3D	    Tangent3[2],
		SMcVector3D	    Tangent4[2]);



	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EGArcLengthFromAngle(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates arc length from a given angle
	/// \param [in]		     dAngleDegrees	Angle of the arc [degrees 0-360]
	/// \param [in]			 dRadius		Radius of the arc
	/// \param [out]	     pdLength		The arc's length
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EGArcLengthFromAngle( double		dAngleDegrees,
		double		dRadius, 
		double		*pdLength);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EGArcAngleFromLength(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates arc angle from a given length 
	/// \param [in]		     dLength		The arc's length
	/// \param [in]			 dRadius		Radius of the arc
	/// \param [out]	     pdAngleDegrees	Angle of the arc [degrees 0-360]
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EGArcAngleFromLength( double		dLength, 
		double		dRadius, 
		double		*pdAngleDegrees);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DCircleFrom3Points(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates Center and Radius from given 3 points.
	/// \param [in]		     stCircle1stPoint
	/// \param [in]		     stCircle2ndPoint
	/// \param [in]			 stCircle3rdPoint
	/// \param [out]	     pCenter
	/// \param [out]	     pRadius
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Function can be used to translate between the two forms of circles used in this module.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DCircleFrom3Points(SMcVector3D	stCircle1stPoint,
		SMcVector3D	stCircle2ndPoint,
		SMcVector3D	stCircle3rdPoint,
		SMcVector3D	*pCenter,
		double		*pRadius);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2D3PointsFromCircle(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates 3 points from given Center and Radius.
	/// \param [out]	     pstCircle1stPoint
	/// \param [out]	     pstCircle2ndPoint
	/// \param [out]	     pstCircle3rdPoint
	/// \param [in]		     stCenter
	/// \param [in]			 dRadius
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Function can be used to translate between the two forms of circles used in this module.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2D3PointsFromCircle(SMcVector3D *pstCircle1stPoint,
		SMcVector3D *pstCircle2ndPoint,
		SMcVector3D *pstCircle3rdPoint,
		SMcVector3D stCenter,
		double	 dRadius);



	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DCircleCircleIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Finds intersection points between two circles.
	/// \param [in]		     st1stCircle1stPoint
	/// \param [in]		     st1stCircle2ndPoint
	/// \param [in]		     st1stCircle3rdPoint
	/// \param [in]			 e1stCircleType			Specifies Circle type (see following remark).
	/// \param [in]		     st2ndCircle1stPoint
	/// \param [in]		     st2ndCircle2ndPoint
	/// \param [in]		     st2ndCircle3rdPoint
	/// \param [in]			 e2ndCircleType			Specifies Circle type (see following remark).
	/// \param [out]	     pstIntersectionPoints	Points of intersection.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	///   if pstIntersectionPoints size is 0 - there are no intersections.
	///   if pstIntersectionPoints size is 1 - circles are tangent.
	///   if pstIntersectionPoints size is 2 - there are two intersection points
	///   if pstIntersectionPoints size is 3 - the two circles overlap, 
	///										   the points returned are the same as the first circle's points
	/// \remarks
	/// Legal values for e1stCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	/// Legal values for e2ndCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DCircleCircleIntersection(SMcVector3D	st1stCircle1stPoint,
		SMcVector3D	st1stCircle2ndPoint,
		SMcVector3D	st1stCircle3rdPoint,
		GEOMETRIC_SHAPE e1stCircleType, //for circle-arc-sector-segment
		SMcVector3D	st2ndCircle1stPoint,
		SMcVector3D	st2ndCircle2ndPoint,
		SMcVector3D	st2ndCircle3rdPoint,
		GEOMETRIC_SHAPE e2ndCircleType, // for circle-arc-sector-segment
		CMcDataArray<SMcVector3D>		 *pstIntersectionPoints);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DCircleCircleDistance(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates distance between two circles.
	/// \param [in]		     st1stCircle1stPoint
	/// \param [in]		     st1stCircle2ndPoint
	/// \param [in]		     st1stCircle3rdPoint
	/// \param [in]			 e1stCircleType			Specifies Circle type (see following remark).
	/// \param [in]		     st2ndCircle1stPoint
	/// \param [in]		     st2ndCircle2ndPoint
	/// \param [in]		     st2ndCircle3rdPoint
	/// \param [in]			 e2ndCircleType			Specifies Circle type (see following remark).
	/// \param [out]		 pstClosestOn1st		A point on first shape which is closest to the second shape.
	/// \param [out]		 pstClosestOn2nd		A point on second shape which is closest to the first shape.
	/// \param [out]		 pdDistance				The distance between the two shapes.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for the two Closest points must be allocated by user.
	/// Legal values for e1stCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	/// Legal values for e2ndCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DCircleCircleDistance(SMcVector3D	st1stCircle1stPoint,
		SMcVector3D	st1stCircle2ndPoint,
		SMcVector3D	st1stCircle3rdPoint,
		GEOMETRIC_SHAPE e1stCircleType, //for circle-arc-sector-segment
		SMcVector3D	st2ndCircle1stPoint,
		SMcVector3D	st2ndCircle2ndPoint,
		SMcVector3D	st2ndCircle3rdPoint,
		GEOMETRIC_SHAPE e2ndCircleType, // for circle-arc-sector-segment
		SMcVector3D	*pstClosestOn1st,
		SMcVector3D	*pstClosestOn2nd,
		double		 *pdDistance);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DCircleLineIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Finds intersection points between Circle and Line.
	/// \param [in]		     st1stCircle1stPoint
	/// \param [in]		     st1stCircle2ndPoint
	/// \param [in]		     st1stCircle3rdPoint
	/// \param [in]			 e1stCircleType			Specifies Circle type (see following remark).
	/// \param [in]		     stLine
	/// \param [in]		     eLineType				Specifies Line type (see following remark).
	/// \param [out]		 pstIntersectionPoints	Points of intersection.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for e1stCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	/// Legal values for eLineType are: #EG_LINE, #EG_RAY, #EG_SEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DCircleLineIntersection(SMcVector3D	st1stCircle1stPoint,
		SMcVector3D	st1stCircle2ndPoint,
		SMcVector3D	st1stCircle3rdPoint,
		GEOMETRIC_SHAPE e1stCircleType, //for circle-arc-sector-segment
		SMcVector3D		stLine[2], 
		GEOMETRIC_SHAPE eLineType,// for Ray LineSegment Segment
		CMcDataArray<SMcVector3D>		 *pstIntersectionPoints);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DCircleLineDistance(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates distance between circle and line.
	/// \param [in]		     st1stCircle1stPoint
	/// \param [in]		     st1stCircle2ndPoint
	/// \param [in]		     st1stCircle3rdPoint
	/// \param [in]			 e1stCircleType			Specifies Circle type (see following remark).
	/// \param [in]		     stLine
	/// \param [in]		     eLineType				Specifies Line type (see following remark).
	/// \param [out]		 pstClosestOnCircle		A point on Circle which is closest to the Line.
	/// \param [out]		 pstClosestOnLine		A point on Line which is closest to the Circle.
	/// \param [out]		 pdDistance				The distance between the two shapes.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for the two Closest points must be allocated by user.
	/// Legal values for e1stCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	/// Legal values for eLineType are: #EG_LINE, #EG_RAY, #EG_SEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DCircleLineDistance(SMcVector3D	st1stCircle1stPoint,
		SMcVector3D	st1stCircle2ndPoint,
		SMcVector3D	st1stCircle3rdPoint,
		GEOMETRIC_SHAPE e1stCircleType, //for circle-arc-sector-segment
		SMcVector3D		stLine[2], 
		GEOMETRIC_SHAPE eLineType,// for Ray LineSegment Segment
		SMcVector3D	*pstClosestOnCircle,
		SMcVector3D	*pstClosestOnLine,
		double		 *pdDistance);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DCirclePointDistance(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates distance between circle and point.
	/// \param [in]		     st1stCircle1stPoint
	/// \param [in]		     st1stCircle2ndPoint
	/// \param [in]		     st1stCircle3rdPoint
	/// \param [in]			 e1stCircleType			Specifies Circle type (see following remark).
	/// \param [in]		     stPoint
	/// \param [out]		 pstClosestOnCirc		A point on Circle which is closest to the Line.
	/// \param [out]		 pdDistance				The distance between circle and point.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for pstClosestOnCircle must be allocated by user.
	/// Legal values for e1stCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DCirclePointDistance(SMcVector3D	st1stCircle1stPoint,
		SMcVector3D	st1stCircle2ndPoint,
		SMcVector3D	st1stCircle3rdPoint,
		GEOMETRIC_SHAPE e1stCircleType, //for circle-arc-sector-segment
		SMcVector3D	stPoint,
		SMcVector3D	*pstClosestOnCirc,
		double		 *pdDistance);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DIsPointOnCircle(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Figures if point is on circle's circumference.
	/// \param   stPoint
	/// \param [in]		     stCircle1st
	/// \param [in]		     stCircle2nd
	/// \param [in]		     stCircle3rd
	/// \param [in]			 eCircleType			Specifies Circle type (see following remark).
	/// \param [in]			 dAccuracy				If point's distance from line is shorter than accuracy, it is considered to be on the line.
	/// \param [out]		 pbIsOn					Returns TRUE if point is on circle.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DIsPointOnCircle(SMcVector3D	stPoint, 
		SMcVector3D	stCircle1st,
		SMcVector3D	stCircle2nd,
		SMcVector3D	stCircle3rd,
		GEOMETRIC_SHAPE eCircleType, // for circle-arc-sector-segment
		double		dAccuracy, 
		bool			*pbIsOn);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DIsPointInCircle(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Figures point and circle relations.
	/// \param   stPoint
	/// \param [in]		     stCircle1st
	/// \param [in]		     stCircle2nd
	/// \param [in]		     stCircle3rd
	/// \param [in]			 eCircleType			Specifies Circle type (see following remark).
	/// \param [out]		 peIsIn					returns the relation between the circle and the point.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DIsPointInCircle( SMcVector3D		stPoint, 
		SMcVector3D	stCircle1st,
		SMcVector3D	stCircle2nd,
		SMcVector3D	stCircle3rd,
		GEOMETRIC_SHAPE eCircleType, // for circle-arc-sector-segment
		POINT_PG_STATUS	*peIsIn);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DCircleBoundingRect(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates circle's bounding rectangle.
	/// \param [in]		     stCircle1st
	/// \param [in]		     stCircle2nd
	/// \param [in]		     stCircle3rd
	/// \param [in]			 eCircleType			Specifies Circle type (see following remark).
	/// \param [out]		 pdLeft
	/// \param [out]		 pdRight
	/// \param [out]		 pdDown
	/// \param [out]		 pdUp
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DCircleBoundingRect(SMcVector3D	stCircle1st,
		SMcVector3D	stCircle2nd,
		SMcVector3D	stCircle3rd,
		GEOMETRIC_SHAPE eCircleType, // for circle-arc-sector-segment
		double *pdLeft, 
		double *pdRight, 
		double *pdDown, 
		double *pdUp );

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DCircleSample(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Samples points on circle's circumference.
	/// \param [in]		     stCircle1st
	/// \param [in]		     stCircle2nd
	/// \param [in]		     stCircle3rd
	/// \param [in]			 eCircleType			Specifies Circle type (see following remark).
	/// \param [in]			 unNumOfPoints			Number of points to sample
	/// \param [out]		 pstSamplingPoints		The sampled points.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DCircleSample(SMcVector3D	stCircle1st,
		SMcVector3D	stCircle2nd,
		SMcVector3D	stCircle3rd,
		GEOMETRIC_SHAPE eCircleType, // for circle-arc-sector-segment
		unsigned int unNumOfPoints, // at least 2
		CMcDataArray<SMcVector3D> *pstSamplingPoints);

	//@}

	/////////////////////////// POLYLINE //////////////////////////////
	//=================================================================================================
	/// \name PolyLine Functions
	//@{
	//=================================================================================================


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode, EG2DPolyLineMove(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Moves a PolyLine by a 2D vector.
	/// \param [in]		  uPointsNum	Number of points in pstPolyLine
	/// \param [in, out]  pstPolyLine	Line to be moved
	/// \param [in]		  dX			Delta X
	/// \param [in]		  dY			Delta Y
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DPolyLineMove( UINT uPointsNum, SMcVector3D pstPolyLine[], 
		double dX, 
		double dY);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolyLineRotate(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Rotates a PolyLine.
	/// \param [in]		  uPointsNum	Number of points in pstPolyLine
	/// \param [in, out]  pstPolyLine		Line to be rotated
	/// \param [in]		  dAngle			Angles to rotatate by [degrees]
	/// \param [in]		  stBasePoint		Base point to rotate around
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	///  Positive angles rotate counter clockwise.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DPolyLineRotate(UINT uPointsNum, SMcVector3D pstPolyLine[],
		double		dAngle, 
		SMcVector3D	stBasePoint);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EGPolyLinesRelation(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Figures the relation between two PolyLines.
	/// \param [in]		  uPointsNum	Number of points in stPolyLine1
	/// \param [in]		  stPolyLine1
	/// \param [in]		  uPointsNum2	Number of points in stPolyLine2
	/// \param [in]		  stPolyLine2
	/// \param [out]	  pstIntersectionPoints		Points of intersection.
	/// \param [out]	  peCrossResult				The relation between the PolyLines.
	/// \param [in]		  unDimension				The dimensions mode [2-3]
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EGPolyLinesRelation(UINT uPointsNum, const SMcVector3D 		 stPolyLine1[], 
		UINT uPointsNum2, const SMcVector3D 		 stPolyLine2[], 
		CMcDataArray<SMcVector3D>		 *pstIntersectionPoints,
		PL_PL_STATUS *peCrossResult,
		unsigned int unDimension);

	//@}

	//////////////////////// POLYLINES & POLYGONS ///////////////////////////////////
	//=================================================================================================
	/// \name Polylines & Polygons Functions
	//@{
	//=================================================================================================


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DIsPointOnPoly(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Figures if point is on Poly.
	/// \param [in]		     stPoint
	/// \param [in]			 uPointsNum	Number of points in stPoly
	/// \param [in]		     stPoly
	/// \param [in]		     ePolyType			Specifies Poly type (see following remark).
	/// \param [in]			 dAccuracy			If point's distance from line is shorter than accuracy, it is considered to be on the line.
	/// \param [out]		 pbIsOn
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for ePolyType are: #EG_POLYLINE, #EG_POLYGON
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DIsPointOnPoly(SMcVector3D	stPoint, 
		UINT uPointsNum, const SMcVector3D 		stPoly[], 
		GEOMETRIC_SHAPE ePolyType, //for polyline-polygon
		double		dAccuracy, 
		bool			*pbIsOn);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EGDistancePoint2Poly(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	///  Calculates Distance between point and Poly.
	/// \param [in]		     stPoint
	/// \param [in]			 uPointsNum	Number of points in stPoly
	/// \param [in]		     stPoly
	/// \param [in]		     ePolyType			Specifies Poly type (see following remark).
	/// \param [out]		 pstClosest			A point on Poly which is closest to the Point.
	/// \param [out]		 puSegment			The segment on the line that contains pstClosest.
	/// \param [out]		 pdDistance			The distance between the point and the line.
	/// \param [in]		     unDimension		The dimensions mode [2-3]
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for pstClosest must be allocated by user.
	/// Legal values for ePolyType are: #EG_POLYLINE, #EG_POLYGON
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EGDistancePoint2Poly(SMcVector3D	stPoint, 
		UINT uPointsNum, const SMcVector3D 		stPoly[], 
		GEOMETRIC_SHAPE ePolyType, //for polyline-polygon
		SMcVector3D	*pstClosest,
		UINT		*puSegment, 
		double		*pdDistance,
		unsigned int unDimension); 


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EGDistancePoly2Poly(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates Distance between two Polys.
	/// \param [in]			 uPointsNum	Number of points in stPoly1
	/// \param [in]		     stPoly1
	/// \param [in]		     ePolyType1		Specifies 1st Poly's type (see following remark).
	/// \param [in]			 uPointsNum2	Number of points in stPoly2
	/// \param [in]		     stPoly2
	/// \param [in]		     ePolyType2		Specifies 2nd Poly's type (see following remark).
	/// \param [out]		 pstClosest1	A point on 1st Poly which is closest to the 2nd poly.
	/// \param [out]		 pstClosest2	A point on 2nd Poly which is closest to the 1st poly.
	/// \param [out]		 pdDistance		The distance between the point and the line.
	/// \param [in]		     unDimension	The dimensions mode [2-3]
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for the two Closest points must be allocated by user.
	/// Legal values for ePolyType1 are: #EG_POLYLINE, #EG_POLYGON
	/// Legal values for ePolyType2 are: #EG_POLYLINE, #EG_POLYGON
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EGDistancePoly2Poly(UINT uPointsNum, const SMcVector3D 		stPoly1[], 
		GEOMETRIC_SHAPE ePolyType1, //for polyline-polygon
		UINT uPointsNum2, const SMcVector3D 		stPoly2[], 
		GEOMETRIC_SHAPE ePolyType2, //for polyline-polygon
		SMcVector3D	*pstClosest1,
		SMcVector3D	*pstClosest2,
		double		*pdDistance,
		unsigned int unDimension);



	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EGPolyLength(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates Poly's length.
	/// \param [in]			 uPointsNum	Number of points in stPoly
	/// \param [in]		     stPoly
	/// \param [in]		     ePolyType		Specifies 1st Poly's type (see following remark).
	/// \param [out]		 pdLength		The Poly's length.
	/// \param [in]		     unDimension	The dimensions mode [2-3]
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Length is in straight lines (no use of DTM) 
	/// Legal values for ePolyType are: #EG_POLYLINE, #EG_POLYGON
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EGPolyLength(UINT uPointsNum, const SMcVector3D 		stPoly[], 
		GEOMETRIC_SHAPE ePolyType, //for polyline-polygon
		double *pdLength,
		unsigned int unDimension);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolySelfIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Figures if Poly intrersects itself.
	/// \param [in]			 uPointsNum	Number of points in stPoly
	/// \param [in]		     stPoly
	/// \param [in]		     ePolyType		  Specifies 1st Poly's type (see following remark).
	/// \param [out]		 pbIsSelfInterect Returns TRUE if Poly intrersects itself.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for ePolyType are: #EG_POLYLINE, #EG_POLYGON
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DPolySelfIntersection(	UINT uPointsNum, const SMcVector3D  stPoly[], 
		GEOMETRIC_SHAPE ePolyType, //for polyline-polygon
		bool *pbIsSelfInterect);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DLinePolyIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Finds intersection points between Poly and Line.
	/// \param [in]			 uPointsNum				Number of points in stPoly
	/// \param [in]		     stPoly
	/// \param [in]		     ePolyType				Specifies 1st Poly's type (see following remark).
	/// \param [in]		     stLine
	/// \param [in]		     eLineType				Specifies Line type (see following remark).
	/// \param [out]		 pstIntersectionPoints	Points of intersection.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for ePolyType are: #EG_POLYLINE, #EG_POLYGON
	/// Legal values for eLineType are: #EG_LINE, #EG_RAY, #EG_SEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DLinePolyIntersection(UINT uPointsNum, const SMcVector3D  stPoly[], 
		GEOMETRIC_SHAPE ePolyType, //for polyline-polygon
		const SMcVector3D 		stLine[2], 
		GEOMETRIC_SHAPE eLineType, // for line-ray-linesegment
		CMcDataArray<SMcVector3D>		 *pstIntersectionPoints);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DLinePolyDistance(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates distance between poly and line.
	/// \param [in]			 uPointsNum				Number of points in stPoly
	/// \param [in]		     stPoly
	/// \param [in]		     ePolyType				Specifies 1st Poly's type (see following remark).
	/// \param [in]		     stLine
	/// \param [in]		     eLineType				Specifies Line type (see following remark).
	/// \param [out]		 pstClosestOnPoly		A point on poly which is closest to the Line.
	/// \param [out]		 pstClosestOnLine		A point on Line which is closest to the poly.
	/// \param [out]		 pdDistance				The distance between the two shapes.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for the two Closest points must be allocated by user.
	/// Legal values for ePolyType are: #EG_POLYLINE, #EG_POLYGON
	/// Legal values for eLineType are: #EG_LINE, #EG_RAY, #EG_SEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DLinePolyDistance(UINT uPointsNum, const SMcVector3D  stPoly[], 
		GEOMETRIC_SHAPE ePolyType, //for polyline-polygon
		const SMcVector3D 		stLine[2], 
		GEOMETRIC_SHAPE eLineType, // for line-ray-linesegment
		SMcVector3D	*pstClosestOnPoly,
		SMcVector3D	*pstClosestOnLine,
		double			*pdDistance);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolyCircleIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Finds intersection points between Poly and circle.
	/// \param [in]			 uPointsNum				Number of points in stPoly
	/// \param [in]		     stPoly
	/// \param [in]		     ePolyType				Specifies 1st Poly's type (see following remark).
	/// \param [in]		     stCircle1st
	/// \param [in]		     stCircle2nd
	/// \param [in]			 stCircle3rd
	/// \param [in]			 eCircleType			Specifies Circle type (see following remark).
	/// \param [out]		 pstIntersectionPoints	Points of intersection.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for ePolyType are: #EG_POLYLINE, #EG_POLYGON
	/// Legal values for eCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DPolyCircleIntersection(UINT uPointsNum, const SMcVector3D  stPoly[], 
		GEOMETRIC_SHAPE ePolyType, //for polyline-polygon
		SMcVector3D	stCircle1st,
		SMcVector3D	stCircle2nd,
		SMcVector3D	stCircle3rd,
		GEOMETRIC_SHAPE eCircleType, // for circle-arc-sector-segment
		CMcDataArray<SMcVector3D>		 *pstIntersectionPoints);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolyCircleDistance(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates distance between poly and circle.
	/// \param [in]			 uPointsNum				Number of points in stPoly
	/// \param [in]		     stPoly
	/// \param [in]		     ePolyType				Specifies 1st Poly's type (see following remark).
	/// \param [in]		     stCircle1st
	/// \param [in]		     stCircle2nd
	/// \param [in]			 stCircle3rd
	/// \param [in]			 eCircleType			Specifies Circle type (see following remark).
	/// \param [out]		 pstClosestOnPoly		A point on poly which is closest to the Line.
	/// \param [out]		 pstClosestOnCircle		A point on Circle which is closest to the Line.
	/// \param [out]		 pdDistance				The distance between the two shapes.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for the two Closest points must be allocated by user.
	/// Legal values for ePolyType are: #EG_POLYLINE, #EG_POLYGON
	/// Legal values for eCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DPolyCircleDistance(UINT uPointsNum, const SMcVector3D  stPoly[], 
		GEOMETRIC_SHAPE ePolyType, //for polyline-polygon
		SMcVector3D	stCircle1st,
		SMcVector3D	stCircle2nd,
		SMcVector3D	stCircle3rd,
		GEOMETRIC_SHAPE eCircleType, // for circle-arc-sector-segment
		SMcVector3D	*pstClosestOnPoly,
		SMcVector3D	*pstClosestOnCircle,
		double			*pdDistance);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolyBoundingRect(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates poly's bounding rectangle.
	/// \param [in]			 uPointsNum				Number of points in stPoly
	/// \param [in]		     stPoly
	/// \param [in]		     ePolyType				Specifies 1st Poly's type (see following remark).
	/// \param [out]		 pdLeft
	/// \param [out]		 pdRight
	/// \param [out]		 pdDown
	/// \param [out]		 pdUp
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for ePolyType are: #EG_POLYLINE, #EG_POLYGON
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DPolyBoundingRect(UINT uPointsNum, const SMcVector3D  stPoly[], 
		GEOMETRIC_SHAPE ePolyType, //for polyline-polygon
		double *pdLeft, 
		double *pdRight, 
		double *pdDown, 
		double *pdUp );

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolySmoothingSample(...)
	// 
	// Author     : Pavel Shenkman
	// Date       : 12/21/2010
	//-------------------------------------------------------------------------------------------------
	/// Samples a smooth curve passing through poly's original points
	///
	/// \param[in]	aPolyPoints					Poly's points
	/// \param[in]	uNumPolyPoints				Number of poly's points (at least 3)
	/// \param[in]	ePolyType					Poly's type (legal values are #EG_POLYLINE, #EG_POLYGON only)
	/// \param[in]	uNumSmoothingLevels			Number of times to halve each line segment for smoothing,
	///											new number of segments will be greater by 2 in power of 
	///											\a uNumSmoothingLevels; should not be equal to zero;
	///											values between 2 and 5 are recommended
	/// \param[out]	paSamplingPoints			Array of sampled points
	/// \param[out]	pauOriginalPointsIndices	Optional array of indices of the original points in \a paSamplingPoints;
	///											empty array means there is no smoothing (in case \a uNumPolyPoints < 3)
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DPolySmoothingSample(
		const SMcVector3D aPolyPoints[], UINT uNumPolyPoints, GEOMETRIC_SHAPE ePolyType, 
		UINT uNumSmoothingLevels, CMcDataArray<SMcVector3D> *paSamplingPoints,
		CMcDataArray<UINT> *pauOriginalPointsIndices = NULL);

	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DClipPolyInRect(
		const SMcVector3D aPolyPoints[], UINT uNumPolyPoints, 
		GEOMETRIC_SHAPE ePolyType, //for polyline-polygon
		const SMcBox *pPolyBoundingRect, 
		const  SMcBox &Rect2D, CMcDataArray<UINT> *aOriginalLineSegmentsIDs);

	//@}

	//////////////////////////// POLYGONS ///////////////////////////////////

	//=================================================================================================
	/// \name PolyGons Functions
	//@{
	//=================================================================================================

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode, EG2DPolyGonMove(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Moves a PolyGon by a 2D vector.
	/// \param [in]		  uPointsNum	Number of points in stPolyGon
	/// \param [in, out]  stPolyGon	Line to be moved
	/// \param [in]		  dX			Delta X
	/// \param [in]		  dY			Delta Y
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DPolyGonMove(UINT uPointsNum,  SMcVector3D  stPolyGon[], 
		double dX, 
		double dY);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolyGonRotate(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Rotates a PolyGon.
	/// \param [in]		  uPointsNum	Number of points in stPolyGon
	/// \param [in, out]  stPolyGon		Line to be rotated
	/// \param [in]		  dAngle			Angles to rotatate by [degrees]
	/// \param [in]		  stBasePoint		Base point to rotate around
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	///  Positive angles rotate counter clockwise.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DPolyGonRotate(UINT uPointsNum, SMcVector3D stPolyGon[], 
		double		dAngle, 
		SMcVector3D	stBasePoint);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DIsPointInPolyGon(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Figures point and polygon relations.
	/// \param [in]		  stPoint
	/// \param [in]		  uPointsNum	Number of points in stPolyGon
	/// \param [in]		  stPolyGon
	/// \param [out]	  peIsIn			returns the relation between the polygon and the point.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DIsPointInPolyGon( SMcVector3D		stPoint, 
		UINT uPointsNum, const SMcVector3D 			stPolyGon[], 
		POINT_PG_STATUS	*peIsIn);



	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolyGonsRelation(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Figures the relation between two PolyLines.
	/// \param [in]		  uPointsNum	Number of points in stPolyGon1
	/// \param [in]		  stPolyGon1
	/// \param [in]		  uPointsNum2	Number of points in stPolyGon2
	/// \param [in]		  stPolyGon2
	/// \param [out]	  pstIntersectionPoints		Points of intersection.
	/// \param [out]	  peCrossResult				The relation between the PolyLines.
	/// \param [out]	  pePolygonStatus			The relation between the PolyGons.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// The following list describes the  combinations of the results from pePolygonStatus and peCrossResult
	///   -# #SEPARATE_PG 
	///      - #SEPARATE_PL	Polygons are completely separate 
	///      - #OVERLAP_PL	OPTION NOT POSSIBLE 
	///      - #INTERSECT_PL OPTION NOT POSSIBLE 
	///      - #RESERVED_PL	OPTION NOT POSSIBLE 
	///      - #TANGENT_PL	Polygons are one outside the other but have at least one tangent segment 
	///      - #TOUCHES_PL	Polygons are one outside the other but have at least one touching segment 
	///   -# #A_IN_B_PG 
	///      - #SEPARATE_PL	First polygon is completely inside the second. 
	///      - #OVERLAP_PL	OPTION NOT POSSIBLE 
	///      - #INTERSECT_PL OPTION NOT POSSIBLE 
	///      - #RESERVED_PL	OPTION NOT POSSIBLE 
	///      - #TANGENT_PL	First polygon is completely inside the second but has at least one tangent segment 
	///      - #TOUCHES_PL	First polygon is completely inside the second but has at least one touching segment 
	///   -# #B_IN_A_PG 
	///      - #SEPARATE_PL	Second polygon is completely inside the first  
	///      - #OVERLAP_PL	OPTION NOT POSSIBLE 
	///      - #INTERSECT_PL OPTION NOT POSSIBLE 
	///      - #RESERVED_PL	OPTION NOT POSSIBLE 
	///      - #TANGENT_PL	Second polygon is completely inside the first but has at least one tangent segment 
	///      - #TOUCHES_PL	Second polygon is completely inside the first but has at least one tangent segment 
	///   -# #SAME_PG 
	///      - #SEPARATE_PL	OPTION NOT POSSIBLE 
	///      - #OVERLAP_PL	Polygons have exactly the same points 
	///      - #INTERSECT_PL OPTION NOT POSSIBLE 
	///      - #RESERVED_PL	OPTION NOT POSSIBLE 
	///      - #TANGENT_PL	Polygons have exactly the same route, although the points are not the same (additional points on segments or different direction) 
	///      - #TOUCHES_PL	OPTION NOT POSSIBLE 
	///   -# #INTERSECT_PG 
	///      - #SEPARATE_PL	OPTION NOT POSSIBLE 
	///      - #OVERLAP_PL	OPTION NOT POSSIBLE 
	///      - #INTERSECT_PL Polygons intersect 
	///      - #RESERVED_PL	OPTION NOT POSSIBLE 
	///      - #TANGENT_PL	OPTION NOT POSSIBLE 
	///      - #TOUCHES_PL	OPTION NOT POSSIBLE 

	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DPolyGonsRelation( UINT uPointsNum, const SMcVector3D  stPolyGon1[], 
		UINT uPointsNum2, const SMcVector3D 			stPolyGon2[], 
		CMcDataArray<SMcVector3D>			*pstIntersectionPoints,
		PL_PL_STATUS	*peCrossResult,
		PG_PG_STATUS	*pePolygonStatus);



	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolyGonArea(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates the polygons area and ensures there is no self-intersection.
	///
	/// In case of self-intersection returns false in pbNoSelfIntersection and DBL_MAX in pdArea.
	///
	/// \param [in]		  uPointsNum			Number of points in stPolyGon
	/// \param [in]		  stPolyGon
	/// \param [out]	  pbNoSelfIntersection	Whether no-self-intersection test succeeded
	/// \param [out]	  pdArea				The polygons area
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Area is always positive.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DPolyGonArea(UINT uPointsNum, const SMcVector3D stPolyGon[], 
		bool *pbNoSelfIntersection, double *pdArea);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolyGonInflate(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Inflates a polygon by a given proportion.
	/// \param [in]		  uPointsNum	Number of points in stPolyGon
	/// \param [in, out]  stPolyGon	The polygon to be inflated
	/// \param [in]		  dProportion	The proportion in which to inflate.
	/// \param [in]		  stBasePoint	The base point from which to inflate.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// dProportions must have a positive value. If dProportions equals 1 than the polygon remains intact.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DPolyGonInflate(UINT uPointsNum, SMcVector3D stPolyGon[], 
		double		dProportion, 
		SMcVector3D	stBasePoint);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolyGonCenterOfGravity(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates Center of Gravity of a polygon.
	/// \param [in]		  uPointsNum	Number of points in stPolyGon
	/// \param [in]		  stPolyGon
	/// \param [out]	  pstCoG
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DPolyGonCenterOfGravity(UINT uPointsNum, const SMcVector3D stPolyGon[], 
		SMcVector3D *pstCoG);



	//=================================================================================================
	//
	// Function name: IMcErrors::ECode,   EG2DPolyPoleOfInaccessibility(...)
	//
	// Author     : Fredi Fincheli
	// Date       : 5/21/2025
	//-------------------------------------------------------------------------------------------------
	/// Calculates the pole of inaccessibility of a polygon.
	/// \param [in]		  uPointsNum	Number of points in stPolyGon
	/// \param [in]		  stPolyGon     The polygon for which Pole of Inaccessibility calculated 
	/// \param [in]		  dPrecision    Precision of the calculation
	/// \param [out]	  pPole			The pole of inaccessibility
	/// \return
	/// - #IMcErrors::SUCCESS if the function succeeds
	/// - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	static IMcErrors::ECode GEOMETRICCALCULATIONS_API EG2DPolyPoleOfInaccessibility(UINT uPointsNum,
		const SMcVector3D stPolyGon[], const double dPrecision, SMcVector3D *pPole);



	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolyGonTriangulation(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Triangulate a polygon.
	/// \param [in]		  uPointsNum	Number of points in stPolyGon
	/// \param [in]		  stPolyGon		Polygon to triangulate.
	/// \param [out]	  pstStrips[]	The array of triangle strips.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DPolyGonTriangulation(	UINT uPointsNum, const SMcVector3D  stPolyGon[], 
	    CMcDataArray<CMcDataArray<SMcVector3D> > *pstStrips);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DClipPolyGon(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Clips two polygons to different polygons, determining for each polygon if it is inside the first or second source polygons, or inside both.
	/// \param [in]		  uPointsNum	Number of points in stPolyGon1
	/// \param [in]		  PolyGon1				the first source polygon points.
	/// \param [in]		  uPointsNum2	Number of points in stPolyGon2
	/// \param [in]		  PolyGon2				the second source polygon points.
	/// \param [out]	  arrAminB				array of polygons that are only inside the first source polygon.
	/// \param [out]	  arrBminA				array of polygons that are only inside the second source polygon.
	/// \param [out]	  arrAandB				array of polygons that are inside both first and second source polygons.
	/// \param [out]	  arrAorB				array of polygons that are inside the first or the second source polygons.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DClipPolyGon(UINT uPointsNum, const SMcVector3D  PolyGon1[],
		UINT uPointsNum2, const SMcVector3D  PolyGon2[],
		CMcDataArray<CMcDataArray<SMcVector3D> > *arrAminB,
		CMcDataArray<CMcDataArray<SMcVector3D> > *arrBminA,
		CMcDataArray<CMcDataArray<SMcVector3D> > *arrAandB,
		CMcDataArray<CMcDataArray<SMcVector3D> > *arrAorB );

	//==============================================================================
	// Method Name: EG2DPolyGonsUnion()
	//------------------------------------------------------------------------------
	/// Calculates geometric union of 2 collections of polygons (each collection is not be mutually intersecting, polygons can contain 'holes')
	///
	/// \param[in]	aPolyGon1Points					array of 1st collection's points; `z` values are ignored
	/// \param[in]	uNumPolyGon1Points				number of 1st collection's points
	/// \param[in]	aPolyGon1ContourStarts			array of start indices of 1st collection's contours with `UINT_MAX` value as polygon delimiter 
	///												(each polygon consists of one outer contour optionally followed by inner ones - 'holes')
	/// \param[in]	uNumPolyGon1ContourStarts		number of start indices of 1st collection's contours
	/// \param[in]	aPolyGon2Points					array of 2nd collection's points; `z` values are ignored
	/// \param[in]	uNumPolyGon2Points				number of 2nd collection's points
	/// \param[in]	aPolyGon2ContourStarts			array of start indices of 2nd collection's contours with `UINT_MAX` value as polygon delimiter 
	///												(each polygon consists of one outer contour optionally followed by inner ones - 'holes')
	/// \param[in]	uNumPolyGon2ContourStarts		number of start indices of 2nd collection's contours
	/// \param[out]	paUnionPolyGonsPoints			array of points of one or several resulting polygons that are the union of 1st and 2nd collections; 
	///												`z` values are zeros
	/// \param[out]	paUnionPolyGonsContourStarts	array of start indices of resulting polygons' contours with `UINT_MAX` value as polygon delimiter 
	///												(each polygon consists of one outer contour optionally followed by inner ones - 'holes')
	/// \return
	///     - status result
	/// 
	//==============================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DPolyGonsUnion(
		const SMcVector3D aPolyGon1Points[], UINT uNumPolyGon1Points, const UINT aPolyGon1ContourStarts[], UINT uNumPolyGon1ContourStarts, 
		const SMcVector3D aPolyGon2Points[], UINT uNumPolyGon2Points, const UINT aPolyGon2ContourStarts[], UINT uNumPolyGon2ContourStarts, 
		CMcDataArray<SMcVector3D> *paUnionPolyGonsPoints, CMcDataArray<UINT> *paUnionPolyGonsContourStarts);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolyGonDirection(...)
	// 
	// Author     : Gidon Gold
	// Date       : 13/9/05
	//-------------------------------------------------------------------------------------------------
	/// determine the general marching direction of the polygon vertices's sequence. 
	/// in case the polygon intersects itself, the marching direction is undefined and self intersect will be returnd   
	/// \param [in]		uPointsNum	Number of points in stPolyGon
	/// \param [in]		stPolyGon						the polygon points.
	/// \param [in]		bCheckForSelfIntersection	specifies wether or not to check if the polygon intersects itself.
	/// \param [out]	pPolyGonDir					the polygon direction 
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================

	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DPolyGonDirection(UINT uPointsNum, const SMcVector3D  stPolyGon[], 
		bool	bCheckForSelfIntersection,
		PG_DIRECTION *pPolyGonDir);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolyGonIsConvex(...)
	// 
	// Author     : Gidon Gold
	// Date       : 6/10/05
	//-------------------------------------------------------------------------------------------------
	/// check if a polygon is convex
	/// \param [in]		  uPointsNum	Number of points in stPolyGon
	/// \param [in]		  stPolyGon			the polygon points.
	/// \param [out]	  pbConvexPoly		true if the polygon is convex
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================

	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DPolyGonIsConvex(UINT uPointsNum, const SMcVector3D  stPolyGon[],
		bool *pbConvexPoly);	

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolyGonConvexHull(...)
	// 
	// Author     : Gidon Gold
	// Date       : 14/9/05
	//-------------------------------------------------------------------------------------------------
	/// calculate the smallest convex polygon containing the input polygon
	/// \param [in]		  uPointsNum	Number of points in stPolyGon
	/// \param [in]		  stPolyGon			the polygon points.
	/// \param [out]	  pstPolyGon		the polygons convex hull points
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	//=================================================================================================

	static GEOMETRICCALCULATIONS_API IMcErrors::ECode	  EG2DPolyGonConvexHull(UINT uPointsNum, const SMcVector3D  stPolyGon[],
		CMcDataArray<SMcVector3D> *pstPolyGon);
	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolygonExpandWithCurves(...)
	// 
	// Author     : Omer Shelef
	// Date       : 10/26/05
	//-------------------------------------------------------------------------------------------------
	/// calculates the expanded polygon's shape.
	/// \param [in]		  uPointsNum	Number of points in stPolyGon
	/// \param [in]		  stPolyGon			the polygon points.
	/// \param [in]		  dExpansionDistance	the expansion distance [larger than zero]
	/// \param [out]	  pstCGS				The epanded polygon as a Closed General Shape
	//// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// - The procedure works on convexed polygons only. If a non convex polygon is inputed, the procedure will
	/// first calculate the smallest convex polygon containing the input polygon and than expand it.
	/// - The polygon's turning point will be expanded to arcs having dExpansionDistance as a radius.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode	  EG2DPolygonExpandWithCurves(UINT uPointsNum, const SMcVector3D  stPolyGon[],
		double dExpansionDistance,
		CMcDataArray<STGeneralShapePoint> *pstCGS);
	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolygonExpandWithCorners(...)
	// 
	// Author     : meir Gabbay
	// Date       : 13/11/03
	//-------------------------------------------------------------------------------------------------
	/// calculates the expanded polygon's shape.
	/// \param [in]		  uPointsNum	Number of points in stPolyGon
	/// \param [in]		  stPolyGon			the polygon points.
	/// \param [in]		  dExpansionDistance	the expansion distance [larger than zero]
	/// \param [out]	  stPoly				The epanded polygon as a Closed CMcDataArray<SMcVector3D>
	//// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// - The procedure works on convexed polygons only. If a non convex polygon is inputed, the procedure will
	/// first calculate the smallest convex polygon containing the input polygon and than expand it.
	/// - The polygon's turning point will be expanded to arcs having dExpansionDistance as a radius.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode	  EG2DPolygonExpandWithCorners(UINT uPointsNum, const SMcVector3D  stPolyGon[],
		double dExpansionDistance,
		CMcDataArray<SMcVector3D> * stPoly);


	//@}

	////////////////// OPEN GENERAL SHAPE ///////////////////////////
	//=================================================================================================
	/// \name Open General Shape Functions
	//@{
	//=================================================================================================


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode, EG2DOpenShapeMove(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Moves an Open General Shape by a 2D vector.
	/// \param [in, out]  pstOGS		Open General Shape to be moved
	/// \param [in]		  dX			Delta X
	/// \param [in]		  dY			Delta Y
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DOpenShapeMove(STGeneralShape  *pstOGS,
		double dX, 
		double dY);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DOpenShapeRotate(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Rotates an Open General Shape.
	/// \param [in, out]  pstOGS		Open General Shape to be rotated
	/// \param [in]		  dAngle			Angles to rotatate by [degrees]
	/// \param [in]		  stBasePoint		Base point to rotate around
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	///  Positive angles rotate counter clockwise.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DOpenShapeRotate(STGeneralShape *pstOGS,
		double		dAngle, 
		SMcVector3D	stBasePoint);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode  , EG2DOpenShapeOpenShapeIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Finds intersection points between two General Open Shapes.
	/// \param [in]	     stOGS1
	/// \param [in]	     stOGS2
	/// \param [out]		 pstIntersectionPoints	Points of intersection.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DOpenShapeOpenShapeIntersection(STGeneralShape stOGS1,
		STGeneralShape stOGS2,
		CMcDataArray<SMcVector3D>		 *pstIntersectionPoints);

	//@}

	////////////////// OPEN & CLOSED GENERAL SHAPE ///////////////////////////
	//=================================================================================================
	/// \name Open & CLosed General Shape Functions
	//@{
	//=================================================================================================


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DGeneralShapeSelfIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Figures if General Shape intrersects itself.
	/// \param [in]		     stGS
	/// \param [in]		     eGeneralShapeType		  Specifies General Shape type (see following remark).
	/// \param [out]		 pbIsSelfInterect Returns TRUE if General Shape intersects itself.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eGeneralShapeType are: #EG_GENERAL_OPENSHAPE, #EG_GENERAL_CLOSEDSHAPE
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DGeneralShapeSelfIntersection(STGeneralShape stGS,
		GEOMETRIC_SHAPE eGeneralShapeType, //for closed-open
		bool *pbIsSelfInterect);	



	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DIsPointOnGeneralShape(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Figures if point is on General Shape.
	/// \param [in]		     stPoint
	/// \param [in]		     stGS
	/// \param [in]		     eGeneralShapeType	Specifies General Shape type (see following remark).
	/// \param [in]			 dAccuracy			If point's distance from General Shape is shorter than accuracy, it is considered to be on the line.
	/// \param [out]		 pbIsOn
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eGeneralShapeType are: #EG_GENERAL_OPENSHAPE, #EG_GENERAL_CLOSEDSHAPE
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DIsPointOnGeneralShape(SMcVector3D	stPoint, 
		STGeneralShape stGS,
		GEOMETRIC_SHAPE eGeneralShapeType, //for closed-open
		double		dAccuracy, 
		bool		*pbIsOn);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode  , EG2DGeneralShapeLength(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates General shapes length.
	/// \param [in]		     stGS
	/// \param [in]		     eGeneralShapeType	Specifies General Shape type (see following remark).
	/// \param [out]		 pdLength
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eGeneralShapeType are: #EG_GENERAL_OPENSHAPE, #EG_GENERAL_CLOSEDSHAPE
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DGeneralShapeLength(STGeneralShape stGS,
		GEOMETRIC_SHAPE eGeneralShapeType, //for closed-open
		double *pdLength);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode  , EG2DGeneralShapeDistance2Point(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates distance between General shape and point.
	/// \param [in]		     stGS
	/// \param [in]		     eGeneralShapeType		Specifies General Shape type (see following remark).
	/// \param [in]		     stPoint
	/// \param [out]		 pstClosestOnShape		A point on Shape which is closest to the Point.
	/// \param [out]		 pdDistance				The distance between circle and point.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for pstClosestOnShape must be allocated by user.
	/// Legal values for eGeneralShapeType are: #EG_GENERAL_OPENSHAPE, #EG_GENERAL_CLOSEDSHAPE
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DGeneralShapeDistance2Point(STGeneralShape stGS,
		GEOMETRIC_SHAPE eGeneralShapeType, //for closed-open
		SMcVector3D	stPoint,
		SMcVector3D	*pstClosestOnShape,
		double		*pdDistance);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode  , EG2DGeneralShapeDistance2Circle(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates distance between General shape and circle.
	/// \param [in]		     stGS
	/// \param [in]		     eGeneralShapeType		Specifies General Shape type (see following remark).
	/// \param [in]		     stCircle1st
	/// \param [in]		     stCircle2nd
	/// \param [in]			 stCircle3rd
	/// \param [in]			 eCircleType			Specifies Circle type (see following remark).
	/// \param [out]		 pstClosestOnShape		A point on General shape which is closest to the Circle.
	/// \param [out]		 pstClosestOnCirc		A point on Circle which is closest to the General shape.
	/// \param [out]		 pdDistance				The distance between the two shapes.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for the two Closest points must be allocated by user.
	/// Legal values for eGeneralShapeType are: #EG_GENERAL_OPENSHAPE, #EG_GENERAL_CLOSEDSHAPE
	/// Legal values for eCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DGeneralShapeDistance2Circle(STGeneralShape stGS,
		GEOMETRIC_SHAPE eGeneralShapeType, //for closed-open
		SMcVector3D	stCircle1st,
		SMcVector3D	stCircle2nd,
		SMcVector3D	stCircle3rd,
		GEOMETRIC_SHAPE eCircleType, // for circle-arc-sector-segment
		SMcVector3D	*pstClosestOnShape,
		SMcVector3D	*pstClosestOnCirc,
		double		*pdDistance);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode  , EG2DGeneralShapeDistance2Poly(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates distance between General shape and poly.
	/// \param [in]		     stGS
	/// \param [in]		     eGeneralShapeType		Specifies General Shape type (see following remark).
	/// \param [in]			 uPointsNum				Number of points in stPoly
	/// \param [in]			 stPoly					the poly points.
	/// \param [in]		     ePolyType				Specifies Poly's type (see following remark).
	/// \param [out]		 pstClosestOnShape		A point on General shape which is closest to the Poly.
	/// \param [out]		 pstClosestOnPoly		A point on Poly which is closest to the General shape.
	/// \param [out]		 pdDistance				The distance between the two shapes.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for the two Closest points must be allocated by user.
	/// Legal values for eGeneralShapeType are: #EG_GENERAL_OPENSHAPE, #EG_GENERAL_CLOSEDSHAPE
	/// Legal values for ePolyType are: #EG_POLYLINE, #EG_POLYGON
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DGeneralShapeDistance2Poly(STGeneralShape stGS,
		GEOMETRIC_SHAPE eGeneralShapeType, //for closed-open
		UINT uPointsNum, const SMcVector3D 		stPoly[], 
		GEOMETRIC_SHAPE ePolyType, //for polyline-polygon
		SMcVector3D	*pstClosestOnShape,
		SMcVector3D	*pstClosestOnPoly,
		double		*pdDistance);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode  , EG2DGeneralShapeDistance2GeneralShape(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates distance between two General shapes.
	/// \param [in]		     stGS1
	/// \param [in]		     eGeneralShapeType1		Specifies 1st General Shape type (see following remark).
	/// \param [in]		     stGS2
	/// \param [in]		     eGeneralShapeType2		Specifies 2nd General Shape type (see following remark).
	/// \param [out]		 pstClosestOnShape		A point on General shape which is closest to the 2nd shape.
	/// \param [out]		 pstClosestOnOtherShape	A point on 2nd shape which is closest to the General shape.
	/// \param [out]		 pdDistance				The distance between the two shapes.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for the two Closest points must be allocated by user.
	/// Legal values for eGeneralShapeType1 are: #EG_GENERAL_OPENSHAPE, #EG_GENERAL_CLOSEDSHAPE
	/// Legal values for eGeneralShapeType2 are: #EG_GENERAL_OPENSHAPE, #EG_GENERAL_CLOSEDSHAPE
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DGeneralShapeDistance2GeneralShape(STGeneralShape stGS1,
		GEOMETRIC_SHAPE eGeneralShapeType1, //for closed-open
		STGeneralShape stGS2, 
		GEOMETRIC_SHAPE eGeneralShapeType2, //for closed-open
		SMcVector3D	*pstClosestOnShape,
		SMcVector3D	*pstClosestOnOtherShape,
		double		*pdDistance);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DGeneralShapeBoundingRect(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates GeneralShape's bounding rectangle.
	/// \param [in]		     stGS
	/// \param [in]		     eGeneralShapeType		Specifies GeneralShape's type (see following remark).
	/// \param [out]		 pdLeft
	/// \param [out]		 pdRight
	/// \param [out]		 pdDown
	/// \param [out]		 pdUp
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eGeneralShapeType are: #EG_GENERAL_OPENSHAPE, #EG_GENERAL_CLOSEDSHAPE
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DGeneralShapeBoundingRect(STGeneralShape stGS,
		GEOMETRIC_SHAPE eGeneralShapeType, //for closed-open
		double *pdLeft, 
		double *pdRight, 
		double *pdDown, 
		double *pdUp );

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DLineGeneralShapeIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Finds intersection points between GeneralShape and Line.
	/// \param [in]		     stGS
	/// \param [in]		     eGeneralShapeType		Specifies GeneralShape's type (see following remark).
	/// \param [in]		     stLine
	/// \param [in]		     eLineType				Specifies Line type (see following remark).
	/// \param [out]		 pstIntersectionPoints	Points of intersection.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eGeneralShapeType are: #EG_GENERAL_OPENSHAPE, #EG_GENERAL_CLOSEDSHAPE
	/// Legal values for eLineType are: #EG_LINE, #EG_RAY, #EG_SEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DLineGeneralShapeIntersection(STGeneralShape stGS,
		GEOMETRIC_SHAPE eGeneralShapeType, //for closed-open
		SMcVector3D		stLine[2], 
		GEOMETRIC_SHAPE eLineType, // for line-ray-linesegment
		CMcDataArray<SMcVector3D>		 *pstIntersectionPoints);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DPolyCircleIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Finds intersection points between GeneralShape and circle.
	/// \param [in]		     stGS
	/// \param [in]		     eGeneralShapeType		Specifies GeneralShape's type (see following remark).
	/// \param [in]		     stCircle1st
	/// \param [in]		     stCircle2nd
	/// \param [in]			 stCircle3rd
	/// \param [in]			 eCircleType			Specifies Circle type (see following remark).
	/// \param [out]		 pstIntersectionPoints	Points of intersection.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eGeneralShapeType are: #EG_GENERAL_OPENSHAPE, #EG_GENERAL_CLOSEDSHAPE
	/// Legal values for eCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DGeneralShapeCircleIntersection(STGeneralShape stGS,
		GEOMETRIC_SHAPE eGeneralShapeType, //for closed-open
		SMcVector3D	stCircle1st,
		SMcVector3D	stCircle2nd,
		SMcVector3D	stCircle3rd,
		GEOMETRIC_SHAPE eCircleType, // for circle-arc-sector-segment
		CMcDataArray<SMcVector3D>		 *pstIntersectionPoints);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DGeneralShapePolyIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Finds intersection points between GeneralShape and Poly.
	/// \param [in]		     stGS
	/// \param [in]		     eGeneralShapeType		Specifies GeneralShape's type (see following remark).
	/// \param [in]			 uPointsNum				Number of points in stPoly
	/// \param [in]			 stPoly					the poly points.
	/// \param [in]		     ePolyType				Specifies 1st Poly's type (see following remark).
	/// \param [out]		 pstIntersectionPoints	Points of intersection.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eGeneralShapeType are: #EG_GENERAL_OPENSHAPE, #EG_GENERAL_CLOSEDSHAPE
	/// Legal values for ePolyType are: #EG_POLYLINE, #EG_POLYGON
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DGeneralShapePolyIntersection(STGeneralShape stGS,
		GEOMETRIC_SHAPE eGeneralShapeType, //for closed-open
		UINT uPointsNum, const SMcVector3D 		stPoly[], 
		GEOMETRIC_SHAPE ePolyType, //for polyline-polygon
		CMcDataArray<SMcVector3D>		 *pstIntersectionPoints);															  

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode  , EG2DGeneralShapeSample(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Samples points on GeneralShape's circumference.
	/// \param [in]		     stGS
	/// \param [in]		     eGeneralShapeType		Specifies GeneralShape's type (see following remark).
	/// \param [in]		     unPointsNumPerArc		Number of points to be sampled on each arc in General shape.
	/// \param [out]		 pstSampledPoints		The sampled points.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// unMaxPointsNum optimal value should be set by EG2DGeneralShapeSampleMaxPoints() .
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DGeneralShapeSample(STGeneralShape stGS,
		GEOMETRIC_SHAPE eGeneralShapeType, //for closed-open
		unsigned int unPointsNumPerArc,
		CMcDataArray<SMcVector3D> *pstSampledPoints);


	//@}
	////////////////// CLOSED GENERAL SHAPE ///////////////////////////
	//=================================================================================================
	/// \name Closed General Shape Functions
	//@{
	//=================================================================================================

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode, EG2DClosedShapeMove(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Moves an Closed General Shape by a 2D vector.
	/// \param [in, out]  pstCGS		Closed General Shape to be moved
	/// \param [in]		  dX			Delta X
	/// \param [in]		  dY			Delta Y
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DClosedShapeMove(STGeneralShape *pstCGS,
		double dX, 
		double dY);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DClosedShapeRotate(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Rotates an Closed General Shape.
	/// \param [in, out]  pstCGS		Closed General Shape to be rotated
	/// \param [in]		  dAngle			Angles to rotatate by [degrees]
	/// \param [in]		  stBasePoint		Base point to rotate around
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	///  Positive angles rotate counter clockwise.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DClosedShapeRotate(STGeneralShape *pstCGS,
		double		dAngle, 
		SMcVector3D	stBasePoint);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DIsPointInClosedShape(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Figures point and ClosedShape relations.
	/// \param [in]		  stPoint
	/// \param [in]		  stCGS
	/// \param [out]	  peIsIn			returns the relation between the ClosedShape and the point.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DIsPointInClosedShape(SMcVector3D			stPoint, 
		STGeneralShape stCGS,
		POINT_PG_STATUS	*peIsIn);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DClosedShapeOpenShapeIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Finds intersection points between Closed GeneralShape and Open GeneralShape.
	/// \param [in]		     stCGS1
	/// \param [in]		     stOGS2
	/// \param [out]		 pstIntersectionPoints	Points of intersection.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DClosedShapeOpenShapeIntersection(STGeneralShape stCGS1,
		STGeneralShape stOGS2,
		CMcDataArray<SMcVector3D>		 *pstIntersectionPoints);



	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DClosedShapeClosedShapeIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Finds intersection points between two Closed GeneralShapes.
	/// \param [in]		     stCGS1
	/// \param [in]		     stCGS2
	/// \param [out]		 pstIntersectionPoints	Points of intersection.
	/// \param [out]		 pShapesRelation		The shapes relation.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DClosedShapeClosedShapeIntersection(STGeneralShape stCGS1,
		STGeneralShape stCGS2,
		CMcDataArray<SMcVector3D>		 *pstIntersectionPoints,
		PG_PG_STATUS *pShapesRelation);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DClosedShapeArea(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/30/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates the Closed shape area.
	/// \param [in]		     stCGS1
	/// \param [out]		 pdArea
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DClosedShapeArea(STGeneralShape stCGS1, 
		double *pdArea);


	//@}

	////////////////// CLOSED GENERAL SHAPE WITH HOLES ///////////////////////////

	//=================================================================================================
	/// \name General Shape With Holes Functions
	//@{
	//=================================================================================================

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode	 , EG2DClosedShapeWithHolesMove(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/30/2004
	//-------------------------------------------------------------------------------------------------
	/// Moves an Closed Shape With Holes by a 2D vector
	/// \param [in]		  unContoursNum	Number of contours in array.
	/// \param [in, out]  astContours	Closed Shape With Holes array of contours to be moved
	/// \param [in]		  dX			Delta X
	/// \param [in]		  dY			Delta Y
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DClosedShapeWithHolesMove(unsigned int unContoursNum, 
		STGeneralShape *astContours,
		double dX, 
		double dY);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode	 , EG2DClosedShapeWithHolesRotate(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/30/2004
	//-------------------------------------------------------------------------------------------------
	/// Rotates an Closed Shape With Holes.
	/// \param [in]		  unContoursNum	Number of contours in array.
	/// \param [in, out]  astContours	Closed Shape With Holes array of contours to be moved
	/// \param [in]		  dAngle			Angles to rotatate by [degrees]
	/// \param [in]		  stBasePoint		Base point to rotate around
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	///  Positive angles rotate counter clockwise.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DClosedShapeWithHolesRotate(unsigned int unContoursNum, 
		STGeneralShape *astContours,
		double		dAngle, 
		SMcVector3D	stBasePoint);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DIsPointInClosedShapeWithHoles(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Figures point and ClosedShape With Holes relations.
	/// \param [in]		  stPoint
	/// \param [in]		  unContoursNum	Number of contours in array.
	/// \param [in]		  astContours	Closed Shape With Holes array of contours.
	/// \param [out]	  peIsIn		returns the relation between the ClosedShape and the point.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DIsPointInClosedShapeWithHoles(SMcVector3D			stPoint, 
		unsigned int unContoursNum, 
		STGeneralShape *astContours,
		POINT_PG_STATUS	*peIsIn);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DIsPointOnClosedShapeWithHoles(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Figures if point is on Closed Shape With Holes.
	/// \param [in]		  stPoint
	/// \param [in]		  unContoursNum	Number of contours in array.
	/// \param [in]		  astContours	Closed Shape With Holes array of contours.
	/// \param [in]		  dAccuracy		If point's distance from Shape is shorter than accuracy, it is considered to be on the line.
	/// \param [out]	  pbIsOn		Returns TRUE if point is on Closed Shape With Holes.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DIsPointOnClosedShapeWithHoles(SMcVector3D			stPoint, 
		unsigned int unContoursNum, 
		STGeneralShape *astContours,
		double		dAccuracy, 
		bool	*pbIsOn);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DClosedShapeWithHolesArea(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/30/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates the Closed shape With Holes area.
	/// \param [in]		  unContoursNum	Number of contours in array.
	/// \param [in]		  astContours	Closed Shape With Holes array of contours.
	/// \param [out]	  pdArea
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode   EG2DClosedShapeWithHolesArea(unsigned int unContoursNum, 
		STGeneralShape *astContours,
		double *pdArea);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode  , EG2DClosedShapeWithHolesLength(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates Closed shapes With Holes length.
	/// \param [in]		  unContoursNum	Number of contours in array.
	/// \param [in]		  astContours	Closed Shape With Holes array of contours.
	/// \param [out]	  pdLength
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DClosedShapeWithHolesLength(unsigned int unContoursNum, 
		STGeneralShape *astContours,
		double *pdLength);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode  , EG2DClosedShapeWithHolesDistance2Point(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates distance between Closed shape With Holes and point.
	/// \param [in]		  unContoursNum	Number of contours in array.
	/// \param [in]		  astContours	Closed Shape With Holes array of contours.
	/// \param [in]		     stPoint
	/// \param [out]		 pstClosestOnShape		A point on Shape which is closest to the Point.
	/// \param [out]		 pdDistance				The distance between circle and point.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for pstClosestOnShape must be allocated by user.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DClosedShapeWithHolesDistance2Point(unsigned int unContoursNum, 
		STGeneralShape *astContours,
		SMcVector3D	stPoint,
		SMcVector3D	*pstClosestOnShape,
		double		*pdDistance);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode  , EG2DClosedShapeWithHolesDistance2Circle(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates distance between Closed shape With Holes and circle.
	/// \param [in]		  unContoursNum	Number of contours in array.
	/// \param [in]		  astContours	Closed Shape With Holes array of contours.
	/// \param [in]		     stCircle1st
	/// \param [in]		     stCircle2nd
	/// \param [in]			 stCircle3rd
	/// \param [in]			 eCircleType			Specifies Circle type (see following remark).
	/// \param [out]		 pstClosestOnShape		A point on shape which is closest to the Circle.
	/// \param [out]		 pstClosestOnCirc		A point on Circle which is closest to the shape.
	/// \param [out]		 pdDistance				The distance between the two shapes.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for the two Closest points must be allocated by user.
	/// Legal values for eCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DClosedShapeWithHolesDistance2Circle(unsigned int unContoursNum, 
		STGeneralShape *astContours,
		SMcVector3D	stCircle1st,
		SMcVector3D	stCircle2nd,
		SMcVector3D	stCircle3rd,
		GEOMETRIC_SHAPE eCircleType, // for circle-arc-sector-segment
		SMcVector3D	*pstClosestOnShape,
		SMcVector3D	*pstClosestOnCirc,
		double		*pdDistance);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode  , EG2DClosedShapeWithHolesDistance2Poly(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates distance between Closed shape With Holes and poly.
	/// \param [in]		  unContoursNum	Number of contours in array.
	/// \param [in]		  astContours	Closed Shape With Holes array of contours.
	/// \param [in]			 uPointsNum				Number of points in stPoly
	/// \param [in]			 stPoly					the poly points.
	/// \param [in]		     ePolyType				Specifies Poly's type (see following remark).
	/// \param [out]		 pstClosestOnShape		A point on shape which is closest to the Poly.
	/// \param [out]		 pstClosestOnPolyLine	A point on Poly which is closest to the shape.
	/// \param [out]		 pdDistance				The distance between the two shapes.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for the two Closest points must be allocated by user.
	/// Legal values for ePolyType are: #EG_POLYLINE, #EG_POLYGON
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DClosedShapeWithHolesDistance2Poly(unsigned int unContoursNum, 
		STGeneralShape *astContours,
		UINT uPointsNum, const SMcVector3D 		stPoly[], 
		GEOMETRIC_SHAPE ePolyType, //for polyline-polygon
		SMcVector3D	*pstClosestOnShape,
		SMcVector3D	*pstClosestOnPolyLine,
		double		*pdDistance);


	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode  , EG2DClosedShapeWithHolesDistance2GeneralShape(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates distance between Closed shape With Holes and a General shape.
	/// \param [in]		  unContoursNum	Number of contours in array.
	/// \param [in]		  astContours	Closed Shape With Holes array of contours.
	/// \param [in]		     stGS2
	/// \param [in]		     eGeneralShapeType2		Specifies 2nd General Shape type (see following remark).
	/// \param [out]		 pstClosestOnShape		A point on shape which is closest to the 2nd shape.
	/// \param [out]		 pstClosestOnOtherShape	A point on 2nd shape which is closest to the shape.
	/// \param [out]		 pdDistance				The distance between the two shapes.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// When distance equals zero - the shapes intersect and closest points are the intersection points.
	/// Memory for the two Closest points must be allocated by user.
	/// Legal values for eGeneralShapeType2 are: #EG_GENERAL_OPENSHAPE, #EG_GENERAL_CLOSEDSHAPE
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DClosedShapeWithHolesDistance2GeneralShape(unsigned int unContoursNum, 
		STGeneralShape *astContours,
		STGeneralShape stGS2, 
		GEOMETRIC_SHAPE eGeneralShapeType2, //for closed-open
		SMcVector3D	*pstClosestOnShape,
		SMcVector3D	*pstClosestOnOtherShape,
		double		*pdDistance);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DClosedShapeWithHolesCircleIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Finds intersection points between a Closed shape With Holes and circle.
	/// \param [in]		  unContoursNum	Number of contours in array.
	/// \param [in]		  astContours	Closed Shape With Holes array of contours.
	/// \param [in]		     stCircle1st
	/// \param [in]		     stCircle2nd
	/// \param [in]			 stCircle3rd
	/// \param [in]			 eCircleType			Specifies Circle type (see following remark).
	/// \param [out]		 pstIntersectionPoints	Points of intersection.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eCircleType are: #EG_CIRCLE, #EG_ARC, #EG_CIRCLESECTOR, #EG_CIRCLESEGMENT
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DClosedShapeWithHolesCircleIntersection(unsigned int unContoursNum, 
		STGeneralShape *astContours,
		SMcVector3D	stCircle1st,
		SMcVector3D	stCircle2nd,
		SMcVector3D	stCircle3rd,
		GEOMETRIC_SHAPE eCircleType, // for circle-arc-sector-segment
		CMcDataArray<SMcVector3D>		 *pstIntersectionPoints);

	//=================================================================================================
	// 
	// Function name: IMcErrors::ECode,   EG2DClosedShapeWithHolesPolyIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Finds intersection points between a Closed shape With Holes and Poly.
	/// \param [in]		  unContoursNum	Number of contours in array.
	/// \param [in]		  astContours	Closed Shape With Holes array of contours.
	/// \param [in]			 uPointsNum				Number of points in stPoly
	/// \param [in]			 stPoly					the poly points.
	/// \param [in]		     ePolyType				Specifies 1st Poly's type (see following remark).
	/// \param [out]		 pstIntersectionPoints	Points of intersection.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	/// Legal values for eGeneralShapeType are: #EG_GENERAL_OPENSHAPE, #EG_GENERAL_CLOSEDSHAPE
	/// Legal values for ePolyType are: #EG_POLYLINE, #EG_POLYGON
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DClosedShapeWithHolesPolyIntersection(unsigned int unContoursNum, 
		STGeneralShape *astContours,
		UINT uPointsNum, const SMcVector3D 		stPoly[], 
		GEOMETRIC_SHAPE ePolyType, //for polyline-polygon
		CMcDataArray<SMcVector3D>		 *pstIntersectionPoints);															  

	// 
	// Function name: IMcErrors::ECode,   EG2DClosedShapeWithHolesGeneralShapeIntersection(...)
	// 
	// Author     : Omer Shelef
	// Date       : 5/29/2004
	//-------------------------------------------------------------------------------------------------
	/// Finds intersection points between a Closed shape With Holes and a General Shape.
	/// \param [in]		  unContoursNum	Number of contours in array.
	/// \param [in]		  astContours	Closed Shape With Holes array of contours.
	/// \param [in]		     stGS2
	/// \param [in]		     eGeneralShapeType2		Specifies 2nd General Shape type (see following remark).
	/// \param [out]		 pstIntersectionPoints	Points of intersection.
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes
	/// \remarks
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DClosedShapeWithHolesGeneralShapeIntersection(unsigned int unContoursNum, 
		STGeneralShape *astContours,
		STGeneralShape stGS2, 
		GEOMETRIC_SHAPE eGeneralShapeType2, //for closed-open
		CMcDataArray<SMcVector3D>		 *pstIntersectionPoints);															  


	//@}

	//=================================================================================================
	/// \name Circles Union
	//@{
	//=================================================================================================

	//=================================================================================================
	//
	// Function Name: EG2DCirclesUnion(...)
	//
	// Author:     Pavel Shenkman
	// Date:       18.05.2004
	//-------------------------------------------------------------------------------------------------
	/// Calculates geometric union of a set of circles
	///
	/// The result is a set	of shapes; each shape consists of one or more contours
	/// (outer contour and maybe several inner ones - 'holes');
	/// each contour consists of arcs of input circles
	/// \param[in]   astCircles[] input circles
	/// \param[in]   unNumOfCircles number of input circles
	/// \param[in]   bAddParticipatingCircles specifies whether each shape returned
	///                                              should include participating circles
	///                                              (see description below)
	/// \param[out]  pastArcs linear array of all arcs forming the result, can be NULL
	/// \param[out]  punNumOfArcs number of arcs returned, can be NULL
	/// \param[out]  pastShapes array of resulting shapes; each shape is a general
	///                                          shape with holes and optionally IDs of participating
	///                                          circles (those circles that their areas are sufficient to
	///                                          form the shape's area), can be NULL
	/// \param[out]  punNumOfShapes number of shapes returned, can be NULL
	///	\return      
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes 
	/// \remarks
	///   - IDs of arcs/circles filled by the function are indices of their input circles.
	///   - Returned arrays of arcs and shapes are allocated and filled by the function (along
	///             with all arrays contained inside shapes) and should be released by calling to
	///             EG2DDeleteUnionArcs() and EG2DDeleteUnionShapes() respectively.
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DCirclesUnion(const STCircle astCircles[], unsigned int unNumOfCircles,
		bool bAddParticipatingCircles, 
		STUnionArc **pastArcs, unsigned int *punNumOfArcs,
		STUnionShape **pastShapes, unsigned int *punNumOfShapes);

	//=================================================================================================
	//
	// Function Name: EG2DDeleteUnionArcs(...)
	//
	// Author:     Pavel Shenkman
	// Date:       18.05.2004
	//-------------------------------------------------------------------------------------------------
	/// Releases array of arcs allocated by EG2DCirclesUnion() function
	/// \param[in]  astArcs array of arcs to be released, can be NULL
	///	\return      
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes 
	/// \remarks
	/// Only for C++ usage
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DDeleteUnionArcs(STUnionArc *astArcs);

	//=================================================================================================
	//
	// Function Name: EG2DDeleteUnionShapes(...)
	//
	// Author: Pavel Shenkman
	// Date:   18.05.2004
	//-------------------------------------------------------------------------------------------------
	/// Releases array of shapes allocated by EG2DCirclesUnion() function
	/// \param[in, out] astShapes array of shapes to be released (along with
	///                                        all arrays contained inside them that will also be
	///                                        assigned to NULL), can be NULL
	/// \param[in] unNumOfShapes number of shapes in the above array, can be 0
	///	\return      
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes 
	/// \remarks
	/// Only for C++ usage
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EG2DDeleteUnionShapes(STUnionShape *astShapes, unsigned int unNumOfShapes);

	//@}




	//=================================================================================================
	//
	// Function Name: EGGetRectanglePoints(...)
	//
	// Author: Meir Gabbay
	// Date:   31.10.2006
	//-------------------------------------------------------------------------------------------------
	/// Returns the four corners of a rotated rectangle
	/// \param[in] firstCornerInDiagonal	first Corner In Diagonal of the rectangle when Aligned to north
	/// \param[in] secondCornerInDiagonal	second Corner In same Diagonal of the input rectangle when Aligned to north
	/// \param[in] rotationAzimDeg			rotation azimuth of the output rectangle 
	/// \param[out] RotatedUpperLeft		output Rotated Upper Left corner 
	/// \param[out] RotatedUpperRight		output Rotated Upper Right corner
	/// \param[out] RotatedLowerRight		output Rotated Lower Right corner
	/// \param[out] RotatedLowerLeft		output Rotated Lower Left corner
	///	\return      
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes 
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode EGGetRectanglePoints(SMcVector3D   firstCornerInDiagonal, 
		SMcVector3D   secondCornerInDiagonal,
		double	     rotationAzimDeg,
		SMcVector3D * RotatedUpperLeft,
		SMcVector3D * RotatedUpperRight,
		SMcVector3D * RotatedLowerRight,
		SMcVector3D * RotatedLowerLeft
		);

	//=================================================================================================
	//
	// Function Name: EGGetRectangleParameters(...)
	//
	// Author: Meir Gabbay
	// Date:   31.10.2006
	//-------------------------------------------------------------------------------------------------
	/// Receives 4 points of a rotated rectangle and retrieves the diagonal points and azimuth of the rectangle 
	/// \param[in] RotatedUpperLeft		input Rotated Upper Left corner 
	/// \param[in] RotatedUpperRight	input Rotated Upper Right corner
	/// \param[in] RotatedLowerRight	input Rotated Lower Right corner
	/// \param[in] RotatedLowerLeft		input Rotated Lower Left corner
	/// \param[out] upperLeft			upper Left corner of the rectangle when Aligned to north
	/// \param[out] LowerRight			lower Right corner of the rectangle when Aligned to north
	/// \param[out] rotationAzim		rotation azimuth of the input rectangle 
	///	\return           
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes 
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode  EGGetRectangleParameters(SMcVector3D  RotatedUpperLeft,
		SMcVector3D  RotatedUpperRight,
		SMcVector3D  RotatedLowerRight,
		SMcVector3D  RotatedLowerLeft,
		SMcVector3D * upperLeft, 
		SMcVector3D * LowerRight,
		double * rotationAzim
		);


	//=================================================================================================
	//
	// Function Name: EGGetRectangleParameters(...)
	//
	// Author: Omer Shelef
	// Date:   25.10.2011
	//-------------------------------------------------------------------------------------------------
	/// Receives 2 points of a rotated rectangle and its rotation azimuth and retrieves the diagonal points of the rectangle 
	/// \param[in] RotatedUpperLeft		input Rotated Upper Left corner 
	/// \param[in] RotatedLowerRight	input Rotated Lower Right corner
	/// \param[in] rotationAzim			rotation azimuth of the input rectangle 
	/// \param[out] upperLeft			upper Left corner of the rectangle when Aligned to north
	/// \param[out] LowerRight			lower Right corner of the rectangle when Aligned to north
	///	\return           
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes 
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode  EGGetRectangleParameters(SMcVector3D  RotatedUpperLeft,
		SMcVector3D  RotatedLowerRight,
		double		 rotationAzim,
		SMcVector3D * upperLeft, 
		SMcVector3D * LowerRight
		);



	//=================================================================================================
	//
	// Function Name: EGCalcRotationDeltaAngles(...)
	//
	// Author: Omer Shelef
	// Date:   5.7.2012
	//-------------------------------------------------------------------------------------------------
	/// Finds the delta rotation angles from a current vector state to a target state along a known platform axis. 
	/// \param[in] dGunbarrelYaw							The gun barrel's yaw angle in world axis 
	/// \param[in] dGunbarrelPitch							The gun barrel's Pitch angle in world axis 
	/// \param[in] dGunbarrelRoll							The gun barrel's Roll angle in world axis  
	/// \param[in] dCurrentGunbarrelYawInPlatformSpace		The current gun barrel's yaw angle relative to the platform
	/// \param[in] dCurrentGunbarrelPitchInPlatformSpace	The current gun barrel's Pitch angle relative to the platform 
	/// \param[in] dTargetYaw							The Target vector's yaw angle in world axis  
	/// \param[in] dTargetPitch							The Target vector's Pitch angle in world axis  
	/// \param[out] pdDeltaYawInPlatformSpace			The delta yaw in platform axis needed to rotate the gun barrel from current to target vector
	/// \param[out] pdDeltaPitchInPlatformSpace			The delta pitch in platform axis needed to rotate the gun barrel from current to target vector
	///	\return           
	///   - #IMcErrors::SUCCESS if the function succeeds
	///   - error code if the function fails, see #IMcErrors::ECode for a complete list of error codes 
	//=================================================================================================
	static GEOMETRICCALCULATIONS_API IMcErrors::ECode  EGCalcRotationDeltaAngles(		
	double dGunbarrelYaw, double dGunbarrelPitch, double dGunbarrelRoll,
	double dCurrentGunbarrelYawInPlatformSpace, double dCurrentGunbarrelPitchInPlatformSpace, 
	double dTargetYaw,  double dTargetPitch,
	double *pdDeltaYawInPlatformSpace, double *pdDeltaPitchInPlatformSpace);
};

