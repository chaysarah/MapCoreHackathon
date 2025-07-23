//===========================================================================
/// \file McGeometricCalculationsTypes.h
/// Types for geometric calculations
//===========================================================================
#ifndef __GEOMETRICCALCTYPES_H__
#define __GEOMETRICCALCTYPES_H__

#include "../SMcVector.h"
#include <memory.h>


/// Describes the polygon marching direction
/// Used as  return value of EG2DPolyGonDirection()
//
typedef enum
{
	CLOCKWISE			= 0,	///< polygon vertices's direction is clockwise
	COUNTER_CLOCKWISE	= 1,	///< polygon vertices's direction is counter clockwise
	SELF_INTERSECT		= 2		///< polygon intersects itself
} PG_DIRECTION;

/// Describes the relation between a point and a line.
/// Used as return value of EG2DIsPointOnLine()
//
typedef enum //PointOnLine
{ 
	BEFORE_EDGE			    =  0,	///< Point is on continuation of the infinite line, before the 1st point. Relevant for #EG_RAY , #EG_SEGMENT
	AFTER_EDGE			    =  1,	///< Point is on continuation of the infinite line, after the 2nd point. Relevant for #EG_SEGMENT
	NOT_ON_LINE				=  2,	///< Point is not on line. Relevant for #EG_LINE , #EG_RAY , #EG_SEGMENT
	IS_ON_LINE				=  3,	///< Point is on line. Relevant for #EG_LINE , #EG_RAY , #EG_SEGMENT 
	IS_1st_EDGE 			=  4,	///< Point is on the line's 1st point. Relevant for #EG_RAY , #EG_SEGMENT
	IS_2cd_EDGE 			=  5 	///< Point is on the line's 2nd point. Relevant for #EG_SEGMENT
} POINT_LINE_STATUS;

/// Describes the relation between two segments.
/// Used as return value of EG2DSegmentsRelation()
//
typedef enum //LineLine
{ 
	SEPARATE_SL				= 0,	///< The segments are seperate
	OVERLAP_SL				= 1,	///< The segments overlap completly.
	INTERSECT_SL			= 2,	///< The segments intersect.
	INTERSECT_PARALLEL_SL	= 3,	///< The segments have an overlaping section.
	PARALLEL_SL				= 4,	///< The segments are parallel.
	LINE1_1st_TOUCHES_SL	= 5,	///< The 1st segment's 1st point touches the 2nd segment. 
	LINE1_2cd_TOUCHES_SL	= 6,	///< The 1st segment's 2nd point touches the 1st segment. 
	LINE2_1st_TOUCHES_SL	= 7,	///< The 2nd segment's 1st point touches the 2nd segment. 
	LINE2_2cd_TOUCHES_SL	= 8,	///< The 2nd segment's 2nd point touches the 1st segment. 
	SAME_POINT11_SL			= 9,	///< The 1st segment's 1st point is the same as the 2nd segment 1st point. 
	SAME_POINT12_SL			= 10,	///< The 1st segment's 1st point is the same as the 2nd segment 2nd point.  
	SAME_POINT21_SL			= 11,	///< The 1st segment's 2nd point is the same as the 2nd segment 1st point. 
	SAME_POINT22_SL			= 12,	///< The 1st segment's 2nd point is the same as the 2nd segment 2nd point.  
} SL_SL_STATUS;


/// Describes the relation between two PolyLines.
/// Used as return value of EGPolyLinesRelation() EG2DPolyGonsRelation()
//
typedef enum //PolyLinePolyLine
{
        SEPARATE_PL	= 0,	///< The PolyLines are seperate.
        OVERLAP_PL	= 1,	///< The PolyLines overlap completly.
        INTERSECT_PL= 2,	///< The PolyLines intersect.
        RESERVED_PL = 3,	///< Reserved fro future versions.
        TANGENT_PL	= 4,	///< The PolyLines have an overlaping section.
        TOUCHES_PL	= 5		///< The PolyLines touch each other without intersecting.
} PL_PL_STATUS;


/// Describes the relation between two PolyGons.
/// Used as return value of EG2DPolyGonsRelation()
//
typedef enum //PolyGonPolyGon
{
        SEPARATE_PG  = 0,	///< The PolyGons are seperate.
        A_IN_B_PG    = 1,	///< The first polygon is completely inside the second polygon.
        B_IN_A_PG    = 2,	///< The second polygon is completely inside the first polygon.
        SAME_PG      = 3,	///< The PolyGons overlap completly.
        INTERSECT_PG = 4	///< The PolyGons intersect.
} PG_PG_STATUS;


/// Describes the relation between a point and a closed shape such as polygons, circles etc'.
/// Used as return value of EG2DIsPointInCircle() , EG2DIsPointInPolyGon() , EG2DIsPointInClosedShape() , EG2DIsPointInClosedShapeWithHoles()
//
typedef enum //PointPolyGon
{
        POINT_NOT_IN_PG = 0,	///< Point is not inside the shape. 
        POINT_IN_PG     = 1,	///< Point is inside the shape. 
        POINT_ON_PG     = 2		///< Point is on the shape. 
} POINT_PG_STATUS;

/// Describes the type of point in a #STGeneralShapePoint .
typedef enum //GeneralShapePointType
{
        START_ARC_END_ARC	= 0, ///< Point is the joint of 2 arcs
        START_ARC_END_SEG	= 1, ///< Point is the joint of an arc and a segment
        START_SEG_END_ARC	= 2, ///< Point is the joint of a segment and an arc 
        START_SEG_END_SEG	= 3, ///< Point is the joint of 2 segments
		MID_ARC				= 4, ///< Point is the mid point of an arc
        START_SEG			= 5, ///< 1st point of GS is a segment point
        START_ARC			= 6, ///< 1st point of GS is an arc    point
        END_SEG				= 7, ///< Last point of GS is a segment point
        END_ARC				= 8, ///< Last point of GS is an arc    point
		GS_POINT_TYPE_NONE  = 9
} GS_POINT_TYPE;

/// A #STGeneralShape point.
typedef struct {
        SMcVector3D    stPoint;		///< Point's coordinates.
		GS_POINT_TYPE ePointType;	///< Point's Type.
} STGeneralShapePoint;

/// A Geometric General Shape. Can be used to represent open or close shapes.
/// For both open and closed shapes, the order of the point's types has a strict format:
///  - First point type must be either #START_SEG or #START_ARC.
///  - The types #START_ARC_END_ARC #START_ARC_END_SEG #START_ARC must be followed by #MID_ARC .
///  - The type #MID_ARC must be followed by either #START_ARC_END_ARC , #START_SEG_END_ARC , #END_ARC.
///  - The types #START_SEG_END_ARC #START_SEG_END_SEG #START_SEG must be followed by either #START_ARC_END_SEG, #START_SEG_END_SEG, #END_SEG .
///  - Last point type must be either #END_SEG or #END_ARC.
struct STGeneralShape {
        unsigned int   unPointsNum;			///< Number of points in shape
        STGeneralShapePoint    *astPoints;	///< Array of shape's points

		STGeneralShape() : unPointsNum(0), astPoints(NULL) {}
};


/// Geometric circle
///
/// Used as an input parameter of EG2DCirclesUnion() function
typedef struct {
		SMcVector3D	stCenter;           ///< circle center
		double		dRadius;            ///< circle radius
} STCircle;

/// Geometric arc
///
/// Used as an output parameter of EG2DCirclesUnion() function
/// \remarks arc direction is always counter-clockwise
typedef struct {
		unsigned int	unCircleID;		///< index of according input circle
		double			dStartAngle;	///< angle in degrees in the range -180 to 180
		double			dEndAngle;		///< angle in degrees greater than #dStartAngle
} STUnionArc;

/// General shape with holes and participating circles
///
/// Used as an output parameter of EG2DCirclesUnion() function
struct STUnionShape {
        unsigned int	unContoursNum;					///< number of contours in contours array
        STGeneralShape	*astContours;					///< outer contour is the first one, inner ones ('holes') are the rest
		unsigned int	unParticipatingCirclesNum;		///< number of participating circles
		unsigned int	*aunParticipatingCirclesIDs;    ///< IDs (indices of according input circles) of circles that their 
														///<   areas are sufficient to form the shape's area
		STUnionShape() { memset(this, 0, sizeof(*this)); }
};

/// Geometric shapes types. Used to distinguish between different shapes in functions.
typedef enum //GeometricShapeType
{
	EG_LINE								=0,	///< Infinite line, described by two points on line.
	EG_RAY								=1,	///< Infinite ray, described by staring point and a point on the ray.
	EG_SEGMENT 							=2,	///< Segment, described by starting and ending point.
	EG_CIRCLE 							=3,	///< Circle, described by 3 points on circle circumference.
	EG_ARC 								=4,	/**< Circle arc, described by 3 points on the arc. 
											The 1st point is the starting point, 
											the 3rd points is the ending point, 
											and the 2nd point is a point on the arc to determine 
											the direction (clockwise or counter clockwise). */
	EG_CIRCLESECTOR 					=5,	/**< Circle sector, described by 3 points on sector's arc. 
											The 1st point is the starting point, 
											the 3rd points is the ending point, 
											and the 2nd point is a point on the arc to determine 
											the direction (clockwise or counter clockwise). */
	EG_CIRCLESEGMENT 					=6,	/**< Circle segment, described by 3 points on segment's arc.
											The 1st point is the starting point, 
											the 3rd points is the ending point, 
											and the 2nd point is a point on the arc to determine 
											the direction (clockwise or counter clockwise). */
	EG_POLYLINE 						=7,	///< Polyline, described by turning points.
	EG_POLYGON 							=8,	/**< Polygon, described by turning points.
											Functions treat the polygon as closed even if first and last
											points are diffrent. */
	EG_GENERAL_OPENSHAPE 				=9,	/**< General shape consisting of arcs and line-segments, 
											described by start and end points for segment, 
											and 3 points for arc.
											see #STGeneralShape for further details. */
	EG_GENERAL_CLOSEDSHAPE 				=10,/**< General closed shape consisting of arcs and line-segments,
											described by start and end points for segment, 
											and 3 points for arc. 
											If first and last points are diffrent, 
											functions treat the shape as closed by a segment. 
											see #STGeneralShape for further details. */
	EG_GENERAL_CLOSEDSHAPE_WITH_HOLES	=11,/**< General closed shape with holes that are general closed shape. 
											Described by an array of contours of general closed shapes.
											The first contour is the outer contour, 
											all the rest are inner contours (holes). 
											Contours can not intersect with themselves or 
											with other contours. Only one level of holes is possible. */
	EG_GEOMETRIC_SHAPE_TYPE_NONE		=12	
} GEOMETRIC_SHAPE;

#endif //__GEOMETRICCALCTYPES_H__
