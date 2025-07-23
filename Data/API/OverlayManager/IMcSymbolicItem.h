#pragma once
//==================================================================================
/// \file IMcSymbolicItem.h
/// Base interface for object scheme items of symbolic types
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "OverlayManager/IMcObjectSchemeItem.h"
#include "OverlayManager/IMcProperty.h"

//==================================================================================
// Interface Name: IMcSymbolicItem
//----------------------------------------------------------------------------------
/// Base interface for object scheme items of symbolic types
//==================================================================================
class IMcSymbolicItem : public virtual IMcObjectSchemeItem
{
protected:

	virtual ~IMcSymbolicItem() {}

public:

	//==============================================================================
	// Enum name: EAttachPointType
	//------------------------------------------------------------------------------
	/// The attach point type defining which points are taken from the scheme node the item is connected to.
	///
	/// Used in:
	/// - SetAttachPointType() to define attachment of the item to points of each its parent node in the scheme (this node is called below *the parent*)
	/// - IMcObject::SObjectToObjectAttachmentParams() (as a part of SAttachPointParams) to define attachment of object's location to 
	///   another object's scheme node  (this node is called below *the parent*).
	//==============================================================================
	enum EAttachPointType 
	{
		EAPT_ALL_POINTS,			///< All the points the parent is based on, namely the points the parent is connected to 
									///<  after performing the parent's transformations (rotations and offsets); this is the default.

		EAPT_NONE,					///< None of the parent's points should be taken; 
									///<  can be defined when the parent is used only as a parameter for a vector offset or vector rotation transforms.

		EAPT_BOUNDING_BOX_POINT,	///< Points on the parent's bounding box; defined by a combination of #EBoundingBoxPointFlags 
									///<  (set by SetBoundingBoxAttachPointType()).

		EAPT_INDEX_POINTS,			///< The parent's points defined by a starting index (set by SetAttachPointIndex()) and a number of points
									///<  (set by SetNumAttachPoints()).
									///< In case of rectangle parent, rectangle vertices are taken in the following order: 
									///< top-left, top-right, bottom-right, bottom-left.

		EAPT_EXCEPT_INDEX_POINTS,	///< The parent's all points except those defined by a starting index (set by SetAttachPointIndex()) and 
									///<  a number of points (set by SetNumAttachPoints()).
									///<
									///< In case of rectangle parent, rectangle vertices are taken in the following order: 
									///<  top-left, top-right, bottom-right, bottom-left.

		EAPT_SEGMENTS_INTERP,		///< Interpolation points defined by an interpolation ratio (set by SetAttachPointPositionValue()) 
									///<  along the parent's segments (their starting index is set by SetAttachPointIndex(), 
									///<  a number of segments is set by SetNumAttachPoints()).
									///<  - In case of arc/ellipse parent, arc segment is used.
									///<  - In case of rectangle parent, rectangle segments are used in the following order: top, right, bottom, left.

		EAPT_ALL_SEGMENTS_INTERPS,	///< Interpolation points defined by an interpolation ratio (set by SetAttachPointPositionValue()) 
									///<  along the parent's all segments.
									///<  - In case of arc/ellipse parent, arc segments are used.
									///<  - In case of rectangle parent, rectangle segments are used in the following order: top, right, bottom, left.

		EAPT_FIRST_POINTS,			///< The parent's first points (their number is set by SetNumAttachPoints()).
									///< In case of arc/ellipse parent, arc segment's first point is used.

		EAPT_LAST_POINTS,			///< The parent's last points (their number is set by SetNumAttachPoints()).
									///< In case of arc/ellipse parent, arc segment's last is used.

		EAPT_MID_POINT,				///< In case of arc/ellipse parent: arc's mid point; 
									///<  otherwise: mid point (for an odd number of the parent's points) or the mid segment middle (for an even number).

		EAPT_ALL_MIDDLES,			///< Mid points of all segments of the parent's points.
									///< - In case of arc/ellipse parent: arc's mid point.
									///< - In case of rectangle parent: rectangle segments are used in the following order: top, right, bottom, left.

		EAPT_CENTER_POINT,			///< Average of all the points the parent is based on.

		EAPT_SCREEN_TOP_MOST,		///< The parent's screen-topmost point (the actual coordinates will be automatically updated 
									///<  when the the parent/viewport orientation is updated).

		EAPT_SCREEN_BOTTOM_MOST,	///< The parent's screen-bottommost point (the actual coordinates will be automatically updated 
									///<  when the the parent/viewport orientation is updated).

		EAPT_SCREEN_LEFT_MOST,		///< The parent's screen-leftmost point (the actual coordinates will be automatically updated 
									///<  when the the parent/viewport orientation is updated).

		EAPT_SCREEN_RIGHT_MOST,		///< The parent's screen-rightmost point (the actual coordinates will be automatically updated 
									///<  when the the parent/viewport orientation is updated).

		EAPT_SCREEN_EQUIDISTANT,	///< Points equidistantly distributed along all the segments of the parent's points 
									///<  (invalid if the parent's points are in screen coordinates).
									///<
									///<  The distance in pixels between neighbor points is set by by SetAttachPointPositionValue() 
									///<  (the actual positions will be automatically updated when viewport scale is updated; 
									///<  in scales smaller than that set by IMcOverlayManager::SetEquidistantAttachPointsMinScale() the positions 
									///<  will be taken from the minimal scale specified).

		EAPT_INDEX_CALC_POINTS,		///< The calculated points of the parent defined by a starting index (set by SetAttachPointIndex()) and a number of 
									///<  points (set by SetNumAttachPoints()). The calculated points of an arrow item are the points returned by 
									///<  GetAllCalculatedPoints(), but in the order from head to tail; in double arrow (with non-zero gap) the order 
									///<  is the head tip, pairs of points in two sides (first the left one as seen from the head) from head to tail.
									///<  The calculated points of any other symbolic item are same points returned by GetAllCalculatedPoints().
									///<  Relevant only when the parent is a symbolic item.

		EAPT_POLY_INTERP,			///< For future use
		EAPT_LONGEST_SEGMENT_INTERP,///< For future use
		EAPT_POLE_OF_INACCESSIBILITY,///< The center of a maximally large circle that can be drawn inside the polygon based on the parent's points.
		EAPT_NUM					///< The number of the enum's members (not to be used as a valid attach point type).
	};

	//==============================================================================
    // Enum name: EBoundingRectanglePoint
    //------------------------------------------------------------------------------
    /// The bounding rectangle point.
	//==============================================================================
	enum EBoundingRectanglePoint	
	{
        EBRP_TOP_LEFT,       ///< Bounding rectangle's top-left corner
        EBRP_TOP_MIDDLE,     ///< Bounding rectangle's top edge middle
        EBRP_TOP_RIGHT,      ///< Bounding rectangle's top-right corner
        EBRP_MIDDLE_RIGHT,   ///< Bounding rectangle's right edge middle
        EBRP_BOTTOM_RIGHT,   ///< Bounding rectangle's bottom-right corner
        EBRP_BOTTOM_MIDDLE,  ///< Bounding rectangle's bottom edge middle
        EBRP_BOTTOM_LEFT,    ///< Bounding rectangle's bottom-left corner
        EBRP_MIDDLE_LEFT,    ///< Bounding rectangle's left edge middle
        EBRP_CENTER          ///< Bounding rectangle's center point
	};

	//==============================================================================
	// Enum name: EBoundingBoxPointFlags
	//------------------------------------------------------------------------------
	/// The bounding box points bits defining point(s) on an item's bounding box.
	///
	/// Used as bit field in SetBoundingBoxAttachPointType() to define the connection to the parent's bounding box:
	/// - the bits #EBBPF_TOP_LEFT through #EBBPF_CENTER define points on the parent's bounding rectangle in XY plane
	/// - the bit #EBBPF_REVERSED_ORDER defines order of points (without the bit: the order of enum members, with the bit: the reversed order)
	/// - the bits #EBBPF_LOWER_PLANE through #EBBPF_LOWER_PLANE define which bounding box's z-plane should be taken (relevant only in 
	///   world attach points in case of location/physical-item parents in 3D, if nothing or both are specified, middle plane is taken)
	//==============================================================================
	enum EBoundingBoxPointFlags
	{
		EBBPF_NONE			= 0x0000,	///< No points

		EBBPF_TOP_LEFT		= 0x0001,	///< Bounding rectangle's top-left corner
		EBBPF_TOP_MIDDLE	= 0x0002,	///< Bounding rectangle's top edge middle
		EBBPF_TOP_RIGHT		= 0x0004,	///< Bounding rectangle's top-right corner
		EBBPF_MIDDLE_RIGHT	= 0x0008,	///< Bounding rectangle's right edge middle
		EBBPF_BOTTOM_RIGHT	= 0x0010,	///< Bounding rectangle's bottom-right corner
		EBBPF_BOTTOM_MIDDLE	= 0x0020,	///< Bounding rectangle's bottom edge middle
		EBBPF_BOTTOM_LEFT	= 0x0040,	///< Bounding rectangle's bottom-left corner
		EBBPF_MIDDLE_LEFT	= 0x0080,	///< Bounding rectangle's left edge middle
		EBBPF_CENTER		= 0x0100,	///< Bounding rectangle's center point

		EBBPF_REVERSED_ORDER= 0x0200,	///< If the bit is set, the points defined by the previous bits are taken in the reversed order

		EBBPF_UPPER_PLANE	= 0x1000,	///< Points on bounding box's upper plane
		EBBPF_LOWER_PLANE	= 0x2000,	///< Points on bounding box's lower plane
	};

	//==============================================================================
	// Struct name: SAttachPointParams
	//------------------------------------------------------------------------------
	/// The attach point parameters used to define attachment of object's location to another object's scheme node.
	///
	/// Used in IMcObject::SetObjectToObjectAttachment() (as a part of IMcObject::SObjectToObjectAttachmentParams).
	//==============================================================================
	struct SAttachPointParams
	{
		SAttachPointParams() 
			: eType(EAPT_ALL_POINTS), nPointIndex(0), nNumPoints(1), fPositionValue(0), 
			  uBoundingBoxPointTypeBitField(EBBPF_CENTER) {}

		/// Attach point type (see #EAttachPointType); the default is #EAPT_ALL_POINTS
		IMcSymbolicItem::EAttachPointType	eType;

		/// Starting index of item's attach points/segments; relevant only for attach points 
		/// of type #EAPT_INDEX_POINTS, #EAPT_EXCEPT_INDEX_POINTS, #EAPT_SEGMENTS_INTERP, #EAPT_INDEX_CALC_POINTS; 
		/// the default is 0
		int									nPointIndex;

		/// Number of attach points; relevant only for attach points of type #EAPT_INDEX_POINTS, 
		/// #EAPT_EXCEPT_INDEX_POINTS, #EAPT_SEGMENTS_INTERP, #EAPT_FIRST_POINTS, #EAPT_LAST_POINTS, #EAPT_INDEX_CALC_POINTS; 
		/// the default is 1
		int									nNumPoints;

		/// Attach point position value; its meaning depends on attach point type and its is relevant only 
		/// for the following types: #EAPT_SEGMENTS_INTERP, #EAPT_ALL_SEGMENTS_INTERPS, #EAPT_SCREEN_EQUIDISTANT; 
		/// the default is 0
		float								fPositionValue;

		/// Attach points on parent's bounding box (a bit field based on #EBoundingBoxPointFlags); 
		/// relevant only for attach points of type #EAPT_BOUNDING_BOX_POINT;
		/// the default is #EBBPF_CENTER
		UINT								uBoundingBoxPointTypeBitField;
	};

	//==============================================================================
	// Enum name: ESegmentType
	//------------------------------------------------------------------------------
	/// The segment type used in SetVectorTransformSegment() to select a segment defining vector rotation and vector offset 
	//==============================================================================
	enum ESegmentType : UINT
	{
		/// The first segment (the same as index 0)
		EST_FIRST_SEGMENT			= 0,
		
		/// The last segment
		EST_LAST_SEGMENT			= UINT_MAX,
		
		/// The middle segment
		EST_MIDDLE_SEGMENT			= UINT_MAX - 1,
		
		/// All the segments used in attach points (applicable to attach points of the following types: 
		/// #EAPT_ALL_POINTS, #EAPT_INDEX_POINTS, #EAPT_EXCEPT_INDEX_POINTS, #EAPT_SEGMENTS_INTERP, #EAPT_ALL_SEGMENTS_INTERPS, 
		/// #EAPT_FIRST_POINTS, #EAPT_LAST_POINTS, #EAPT_MID_POINT, #EAPT_ALL_MIDDLES, 
		/// #EAPT_SCREEN_TOP_MOST, #EAPT_SCREEN_BOTTOM_MOST, #EAPT_SCREEN_LEFT_MOST, #EAPT_SCREEN_RIGHT_MOST, 
		/// #EAPT_SCREEN_EQUIDISTANT, #EAPT_INDEX_CALC_POINTS)
		EST_ATTACH_POINT_SEGMENTS	= UINT_MAX - 2
	};

	//==============================================================================
	// Enum name: EOffsetOrientation
	//------------------------------------------------------------------------------
	/// The offset orientation definition used in SetOffsetOrientation() to define the orientation of a value offset defined by SetOffset().
	///
	/// - Applicable only for value offset defined by SetOffset() and **not** vector offset defined by SetVectorOffsetValue().
	/// - Defines whether the offset is absolute or relative (rotated according to the rotation of either the item itself or its parent node).
	//==============================================================================
	enum EOffsetOrientation 
	{
		EOO_RELATIVE_TO_PARENT_ROTATION,	///< The offset is relative to the parent's rotation
		EOO_RELATIVE_TO_ITEM_ROTATION,		///< The offset is relative to this item's rotation
		EOO_ABSOLUTE						///< The offset is absolute and doesn't depend on any rotation
	};

	//==============================================================================
	// Enum name: EVectorOffsetCalc
	//------------------------------------------------------------------------------
	/// The vector offset calculation mode used in SetVectorOffsetCalc() to define how offset direction and distance are calculated.
	///
	/// The calculation is based on:
	/// - the vector offset segment (defined by SetVectorTransformParentIndex() and SetVectorTransformSegment()), 
	/// - the vector offset value (defined by SetVectorOffsetValue(), positive or negative),
	/// - the item's point (for perpendicular types only).
	//==============================================================================
	enum EVectorOffsetCalc
	{
		/// The offset direction is parallel to the segment;\n
		/// the offset distance is equal to vector offset value
		EVOC_PARALLEL_DISTANCE,

		/// The offset direction is parallel to the segment;\n
		/// the offset distance is equal to vector offset value multiplied by the segment's length
		EVOC_PARALLEL_RATIO,

		/// The offset direction is perpendicular to the segment, from the item's point towards the segment;\n
		/// the offset distance is equal to vector offset value
		EVOC_PERPENDICULAR_DISTANCE,

		/// The offset direction is perpendicular to the segment, from the item's point towards the segment;\n
		/// the offset distance is equal to vector offset value multiplied by the length of the line section between the item's point and the segment
		EVOC_PERPENDICULAR_RATIO,

		/// The offset direction is upward;\n
		/// the offset distance is equal to vector offset value multiplied by the segment's length
		EVOC_SEGMENT_LENGTH_RATIO_UPWARD,

		/// The offset direction is upward;\n
		/// the offset distance is equal to vector offset value multiplied by the length of the line section between the item's point and the segment
		EVOC_PERPENDICULAR_LENGTH_RATIO_UPWARD,

		/// The offset direction is parallel to the segment;\n
		/// the offset distance is equal to vector offset value multiplied by the length of the line section between the item's point and the segment
		EVOC_PERPENDICULAR_RATIO_PARALLEL
	};

	//==============================================================================
	// Enum name: EDrawPriorityGroup
	//------------------------------------------------------------------------------
	/// The item's draw priority group used in SetDrawPriorityGroup() to define the drawing order of the item relative to items of other groups.
	//==============================================================================
	enum EDrawPriorityGroup
	{
		/// The regular draw priority group (the default)
		EDPG_REGULAR,
		
		/// Items of this group are placed in front of items belonging to other groups and (in 3D) in front of terrain;
		/// considered only if top most mode of overlay manager set by IMcOverlayManager::SetTopMostMode() is enabled.
		EDPG_TOP_MOST,

		/// Items of this group are placed on viewport background behind terrain 
		///  (relevant for items based on screen-coordinate locations only)
		EDPG_SCREEN_BACKGROUND,

		/// Items of this group are placed behind items belonging to other groups
		EDPG_BOTTOM_MOST,

		/// Items of this group are placed behind grid and above terrain and participates in terrain image processing defined in #IMcImageProcessing
		///  (relevant for item based on world-coordinate locations only)
		EDPG_WORLD_WITH_TERRAIN
	};

	//==============================================================================
	// Enum Name: ETextureFilter
	//------------------------------------------------------------------------------
	/// The filtering options for rendering textures used in SetTextureFiltering() to define how the item's texture is resized and which mipmap levels are used
	//==============================================================================
	enum ETextureFilter
    {
        /// The default filter selected by MapCore according to the item's type
		ETF_DEFAULT		= 7,

		/// No mipmaping: 
		/// - for mipmap determination: turning off mipmaping; 
		/// - for texture minification and magnification: not valid
		ETF_NONE		= 0,

        /// Using the nearest pixel
		ETF_POINT		= 1,

		/// Linear: 
		/// - for texture minification and magnification: bilinear; 
		/// - for mipmap determination: trilinear
		ETF_LINEAR		= 2,

		/// Anisotropic: 
		/// - for texture minification and magnification: similar to #ETF_LINEAR, but compensates for the angle of the texture plane; 
		/// - for mipmap determination: not valid
		ETF_ANISOTROPIC	= 3
    };

public:

	/// \name Clone and Connect
	//@{

	//==============================================================================
	// Method Name: Clone()
	//------------------------------------------------------------------------------
	/// Clones the item.
	///
	/// \param[out] ppClonedItem	The newly cloned item
	/// \param[in]  pObject			Optional object, to take the values of private properties from
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Clone(IMcSymbolicItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//==============================================================================
	// Method Name: Connect()
	//------------------------------------------------------------------------------
	/// Connects the item to one specified parent.
	///
	/// If the item node is already connected, it will be disconnected first.
	///
	/// \param[in] pParentNode		The parent node or NULL	(NULL means the first location)
	/// \param[out]	peErrorStatus	The error status (if not NULL, the return value will always be IMcErrors::SUCCESS)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Connect(IMcObjectSchemeNode *pParentNode, IMcErrors::ECode *peErrorStatus = NULL) = 0;

	//==============================================================================
	// Method Name: Connect()
	//------------------------------------------------------------------------------
	/// Connects the item to several specified parents.
	///
	/// If the item node is already connected, it will be disconnected first.
	///
	/// \param[in] apParentNodes	The parent nodes, one of them can be NULL (NULL means the first location)
	/// \param[in] uNumParents		The number of nodes in the above array 
	/// \param[out]	peErrorStatus	The error status (if not NULL, the return value will be always IMcErrors::SUCCESS)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Connect(IMcObjectSchemeNode *const apParentNodes[], UINT uNumParents, IMcErrors::ECode *peErrorStatus = NULL) = 0;

	//@}

	/// \name Points
	//@{

	//==============================================================================
	// Method Name: GetAllCalculatedPoints()
	//------------------------------------------------------------------------------
	/// Retrieves all the points calculated by MapCore for the item in the given object currently displayed in the given viewport.
	///
	/// The calculation is based on the following:
	/// - the object's location points converted to the viewport's grid coordinate system;
	/// - the item's attach points for each parent node in the scheme defining which points are taken from the parent node;
	/// - the item's transformations (offsets, rotations, rotation alignments);
	/// - the item's parameters according to the item's type (ellipse radiuses etc.) and the object's private properties overriding these parameters.
	/// - the manual-geometry-item's connection indices.
	///
	/// \see IMcObjectSchemeNode::GetCoordinates()
	///
	/// \param[in]  pMapViewport				The map viewport.
	/// \param[in]  pObject						The object.
	/// \param[out] paPoints					Array of the item's points calculated (empty array if there are no valid points).
	/// \param[out]	peCoordSystem				The points' coordinate system (world/screen/image).
	/// \param[out]	pauOriginalPointsIndices	Optional array of indices of the original points (before adding additional points for smoothing, 
	///											great circle sampling, etc.) in \a paPoints;
	///											empty array means all the points are original (there are no additional points)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAllCalculatedPoints(
		IMcMapViewport *pMapViewport, IMcObject *pObject,
		CMcDataArray<SMcVector3D> *paPoints, EMcPointCoordSystem *peCoordSystem,
		CMcDataArray<UINT> *pauOriginalPointsIndices = NULL) const = 0;

	//@}

	/// \name Attach Points
	//@{

	//==============================================================================
	// Method Name: SetAttachPointType()
	//------------------------------------------------------------------------------
	/// Sets the attachment of the item to points of the specified parent node in the scheme (see #EAttachPointType) as a shared or private property.
	///
	/// \param[in]	uParentIndex		The index (zero-based) of the parent node to define the attachment to.
	/// \param[in]	eType				Attach point type (see #EAttachPointType); 
	///									the default is #EAPT_ALL_POINTS (all the points the parent is based on).
	///									if uPropertyID is not IMcProperty::EPPI_SHARED_PROPERTY_ID, eType can't be EAPT_SCREEN_EQUIDISTANT or
	///									EAPT_BOUNDING_BOX_POINT and the parent defined by uParentIndex can't be physical.
	///									The parameter's meaning depends on \a uPropertyID (see note below).
	/// \param[in] uPropertyID			The private property ID or the special value IMcProperty::EPPI_SHARED_PROPERTY_ID.
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetAttachPointType(
		UINT uParentIndex, EAttachPointType eType,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID) = 0;

	//==============================================================================
	// Method Name: GetAttachPointType()
	//------------------------------------------------------------------------------
	/// Retrieves the item's attach point to points of the specified parent node defined by SetAttachPointType() (see #EAttachPointType).
	///
	/// \param[in]	uParentIndex		The index (zero-based) of the parent node to retrieve the attachment to.
	/// \param[out]	peType				Attach point type (see #EAttachPointType);
	///									the default is #EAPT_ALL_POINTS (all the points the parent is based on).
	///									The parameter's meaning depends on \a puPropertyID (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or the special value IMcProperty::EPPI_SHARED_PROPERTY_ID.
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAttachPointType(
		UINT uParentIndex, EAttachPointType *peType,
		UINT *puPropertyID = NULL) const = 0;

	//==============================================================================
	// Method Name: SetBoundingBoxAttachPointType()
	//------------------------------------------------------------------------------
	/// Sets which point(s) on the bounding box of the item's parent should be taken as attach points of type #EAPT_BOUNDING_BOX_POINT (see SetAttachPointType()) as a shared or private property.
	///
	/// \param[in]	uParentIndex				The index (zero-based) of the parent node to define the attachment to.
	/// \param[in]	uBoundingBoxPointBitField	The point(s) on the parent's bounding box (a bit field based on #EBoundingBoxPointFlags); 
	///											the default is #EBBPF_CENTER.
	///											The parameter's meaning depends on \a uPropertyID
	///											and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID					The private property ID or one of the following special values:
	///											IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe			The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetBoundingBoxAttachPointType(
		UINT uParentIndex, UINT uBoundingBoxPointBitField,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetBoundingBoxAttachPointType()
	//------------------------------------------------------------------------------
	/// Retrieves which point(s) on the bounding box of the item's parent are taken as attach points of type #EAPT_BOUNDING_BOX_POINT (see SetAttachPointType()).
	///
	/// \param[in]	uParentIndex				The index (zero-based) of the parent node to retrieve the attachment to.
	/// \param[out]	puBoundingBoxPointBitField	The point(s) on the parent's bounding box (a bit field based on #EBoundingBoxPointFlags); 
	///											the default is #EBBPF_CENTER.
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetBoundingBoxAttachPointType(
		UINT uParentIndex, UINT *puBoundingBoxPointBitField,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetAttachPointIndex()
	//------------------------------------------------------------------------------
	/// Sets the starting index of item's attach points and attach points' segments (for the specified parent) as a shared or private property.
	///
	/// Relevant only for attach points of types #EAPT_INDEX_POINTS, #EAPT_EXCEPT_INDEX_POINTS, #EAPT_SEGMENTS_INTERP, #EAPT_INDEX_CALC_POINTS 
	/// (see SetAttachPointType()). 
	/// The number of consecutive indices is set by SetNumAttachPoints().
	///
	/// \param[in]	uParentIndex		The index (zero-based) of the parent node to define the attachment to.
	/// \param[in]	nPointIndex			The starting index of attach point/segment (zero-based); 
	///									negative number means indexing from the end: -1 for the last one, -2 for one before the last, etc.;
	///									the default is 0.
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetAttachPointIndex(UINT uParentIndex, int nPointIndex, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetAttachPointIndex()
	//------------------------------------------------------------------------------
	/// Retrieves the item's attach-point-index property (for the specified parent) set by SetAttachPointIndex().
	///
	/// Relevant only for attach points of types #EAPT_INDEX_POINTS, #EAPT_EXCEPT_INDEX_POINTS, #EAPT_SEGMENTS_INTERP, #EAPT_INDEX_CALC_POINTS 
	/// (see SetAttachPointType()). 
	/// The number of consecutive indices is retrieved by GetNumAttachPoints().
	///
	/// \param[in]	uParentIndex		The index (zero-based) of the parent node to retrieve the attachment to.
	/// \param[out]	pnPointIndex		The starting index of attach point/segment (zero-based); 
	///									negative number means indexing from the end: -1 for the last one, -2 for one before the last, etc.;
	///									the default is 0.
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAttachPointIndex(UINT uParentIndex, int *pnPointIndex,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetNumAttachPoints()
	//------------------------------------------------------------------------------
	/// Sets the number of attach points (for the specified parent) as a shared or private property.
	///
	/// Relevant only for attach points of type #EAPT_INDEX_POINTS, #EAPT_EXCEPT_INDEX_POINTS, #EAPT_SEGMENTS_INTERP, #EAPT_FIRST_POINTS, 
	/// #EAPT_LAST_POINTS, #EAPT_INDEX_CALC_POINTS see SetAttachPointType().
	///
	/// \param[in]	uParentIndex		The index (zero-based) of the parent node to define the attachment to.
	/// \param[in]	nNumPoints			The number of attach points; positive number means ascending order 
	///									of indices starting with the start index (if relevant), 
	///									negative one means descending order; the default is 1.
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetNumAttachPoints(UINT uParentIndex, int nNumPoints, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetNumAttachPoints()
	//------------------------------------------------------------------------------
	/// Retrieves the item's number-of-attach-points property (for the specified parent) defined by SetNumAttachPoints().
	///
	/// Relevant only for attach points of type #EAPT_INDEX_POINTS, #EAPT_EXCEPT_INDEX_POINTS, #EAPT_SEGMENTS_INTERP, #EAPT_FIRST_POINTS, 
	/// #EAPT_LAST_POINTS, #EAPT_INDEX_CALC_POINTS, see SetAttachPointType().
	///
	/// \param[in]	uParentIndex		The index (zero-based) of the parent node to retrieve the attachment to.
	/// \param[out]	pnNumPoints			The number of attach points; positive number means ascending order 
	///									of indices starting with the start index (if relevant), 
	///									negative one means descending order; the default is 1. 
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetNumAttachPoints(UINT uParentIndex, int *pnNumPoints,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetAttachPointPositionValue()
	//------------------------------------------------------------------------------
	/// Sets the item's attach point position value (for the specified parent) as a shared or private property.
	///
	/// The value's meaning depends on attach point type and its is relevant only for the following types: 
	/// #EAPT_SEGMENTS_INTERP, #EAPT_ALL_SEGMENTS_INTERPS, #EAPT_SCREEN_EQUIDISTANT (see #EAttachPointType for details).
	///
	/// \param[in]	uParentIndex		The index (zero-based) of the parent node to define the attachment to.
	/// \param[in]	fPositionValue		Attach point position value; the default is 0.
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetAttachPointPositionValue(UINT uParentIndex, float fPositionValue, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetAttachPointPositionValue()
	//------------------------------------------------------------------------------
	/// Retrieves the item's attach-point-position-value property (for the specified parent) defined by SetAttachPointPositionValue().
	///
	/// The value's meaning depends on attach point type and its is relevant only for the following types: 
	/// #EAPT_SEGMENTS_INTERP, #EAPT_ALL_SEGMENTS_INTERPS, #EAPT_SCREEN_EQUIDISTANT (see #EAttachPointType for details).
	///
	/// \param[in]	uParentIndex		The index (zero-based) of the parent node to retrieve the attachment to.
	/// \param[out]	pfPositionValue		Attach point position value.
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAttachPointPositionValue(UINT uParentIndex, float *pfPositionValue,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Transform parameters
	//@{

	//==============================================================================
	// Method Name: SetOffsetType()
	//------------------------------------------------------------------------------
	/// Sets the geometry type of value offset and vector offset in world coordinate system.
	///
	/// Applicable to both value offset (defined by SetOffset()) and vector offset (defined by SetVectorOffsetValue()) in world coordinate system only.
	///
	/// \param[in]	eOffsetType		The offset type, see IMcObjectSchemeItem::EGeometryType;
	///								the default is IMcObjectSchemeItem::EGT_GEOMETRIC_IN_VIEWPORT
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetOffsetType(EGeometryType eOffsetType) = 0;

	//==============================================================================
	// Method Name: GetOffsetType()
	//------------------------------------------------------------------------------
	/// Retrieves the geometry type of value offset and vector offset in world coordinate system set by SetOffsetType().
	///
	/// Applicable to both value offset (defined by SetOffset()) and vector offset (defined by SetVectorOffsetValue()) in world coordinate system only.
	///
	/// \param[out]	peOffsetType	The offset type, see IMcObjectSchemeItem::EGeometryType;
	///								the default is IMcObjectSchemeItem::EGT_GEOMETRIC_IN_VIEWPORT
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetOffsetType(EGeometryType *peOffsetType) const = 0;

	//==============================================================================
	// Method Name: SetOffsetOrientation()
	//------------------------------------------------------------------------------
	/// Defines the orientation of value offset (defined by SetOffset()) as a shared or private property.
	///
	/// - Applicable only for value offset defined by SetOffset() and **not** vector offset set by SetVectorOffsetValue().
	/// - Defines whether the offset is absolute or relative (rotated according to the rotation of either the item itself or its parent node).
	///
	/// \param[in]	eOffsetOrientation	The offset orientation, see #EOffsetOrientation.
	///									The default is #EOO_RELATIVE_TO_PARENT_ROTATION.
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetOffsetOrientation(EOffsetOrientation eOffsetOrientation,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetOffsetOrientation()
	//------------------------------------------------------------------------------
	/// Retrieves the orientation of value offset (defined by SetOffset()) set by SetOffsetOrientation()
	///
	/// - Applicable only for value offset set by SetOffset() and **not** vector offset set by SetVectorOffsetValue().
	/// - Defines whether the offset is absolute or relative (rotated according to the rotation of either the item itself or its parent node).
	///
	/// \param[out]	peOffsetOrientation	The offset orientation, see #EOffsetOrientation.
	///									The default is #EOO_RELATIVE_TO_PARENT_ROTATION.
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetOffsetOrientation(EOffsetOrientation *peOffsetOrientation,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetVectorTransformParentIndex()
	//------------------------------------------------------------------------------
	/// Sets the index of the item's parent whose points along with segment index/type define vector offset/rotation.
	///
	/// Relevant for vector offset (defined by SetVectorOffsetValue()) and vector rotation (defined by SetVectorRotation()).
	///
	/// \param[in]	uParentIndex	The index of the parent node whose points along with segment index/type define vector offset/rotation; the default is 0.
	///								If #EST_ATTACH_POINT_SEGMENTS segment type is defined by SetVectorTransformSegment() a special value of 
	///								`UINT_MAX` can be used instead of the index, it means segments from all the parents.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetVectorTransformParentIndex(UINT uParentIndex) = 0;

	//==============================================================================
	// Method Name: GetVectorTransformParentIndex()
	//------------------------------------------------------------------------------
	/// Retrieves the index of the item's parent node set by SetVectorTransformParentIndex().
	///
	/// Relevant for vector offset (defined by SetVectorOffsetValue()) and vector rotation (defined by SetVectorRotation()).
	///
	/// \param[out]	puParentIndex	The index of the parent node whose points along with segment index/type define vector offset/rotation; the default is 0.
	///								If #EST_ATTACH_POINT_SEGMENTS segment type is defined by SetVectorTransformSegment() a special value of 
	///								`UINT_MAX` can be used instead of the index, it means segments from all the parents.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVectorTransformParentIndex(UINT *puParentIndex) const = 0;

	//==============================================================================
	// Method Name: SetVectorTransformSegment()
	//------------------------------------------------------------------------------
	/// Sets vector transform segment as a shared or private property.
	///
	/// Vector transform segment along with vector transform parent points defines vector offset/rotation. 
	/// Relevant for vector offset (defined by SetVectorOffsetValue()) and vector rotation (defined by SetVectorRotation()).
	///
	/// \param[in]	uSegmentIndexOrType	The segment index or one of values of #ESegmentType.
	///									Selects (between the points of the parent defined by SetVectorTransformParentIndex() a segment 
	///									used to calculate the vector offset/rotation.
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetVectorTransformSegment(UINT uSegmentIndexOrType,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetVectorTransformSegment()
	//------------------------------------------------------------------------------
	/// Retrieves vector transform segment property defined by SetVectorTransformSegment().
	///
	/// Vector transform segment along with vector transform parent points defines vector offset/rotation. 
	/// Relevant for vector offset (defined by SetVectorOffsetValue()) and vector rotation (defined by SetVectorRotation()).
	///
	/// \param[out]	puSegmentIndexOrType	Segment index or one of values of #ESegmentType.
	///										Selects (between the points of the parent defined by SetVectorTransformParentIndex() a segment 
	///										used to calculate the vector offset/rotation.
	///										The parameter's meaning depends on \a puPropertyID
	///										and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID			The private property ID or one of the following special values:
	///										IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe		The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVectorTransformSegment(UINT *puSegmentIndexOrType,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetVectorOffsetCalc()
	//------------------------------------------------------------------------------
	/// Defines the vector offset calculation mode defining calculation of vector offset direction and distance as a shared or private property.
	///
	/// \param[in]	eCalc				Vector offset calculation mode (see #EVectorOffsetCalc).
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetVectorOffsetCalc(EVectorOffsetCalc eCalc,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetVectorOffsetCalc()
	//------------------------------------------------------------------------------
	/// Retrieves vector offset calculation mode set by SetVectorOffsetCalc().
	///
	/// \param[out]	peCalc				Vector offset calculation mode (see #EVectorOffsetCalc).
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVectorOffsetCalc(EVectorOffsetCalc *peCalc,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Geometric and Geographic Transforms
	/// 
	/// The transforms are applied according to the order of functions below after the parent's rotation transform is applied 
	/// (unless it is canceled by SetRotationAlignment())
	//@{

	//==============================================================================
	// Method Name: SetCoordinateSystemConversion()
	//------------------------------------------------------------------------------
	/// Sets coordinate system conversion of the item's points.
	///
	/// \param[in]	eCoordinateSystem	Coordinate system (world/screen/image) to convert points to (see #EMcPointCoordSystem); 
	///									only the following conversions are valid: image -> world, world -> screen.
	///									Applicable only if \p bEnabled is true.
	/// \param[in]	bEnabled			Whether or not coordinate system conversion is enabled; the default is false.
	///
	/// \note
	/// If coordinate system conversion is not enabled, all the points are converted to 
	/// their nearest common coordinate system according to the following order: image->world->screen
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCoordinateSystemConversion(EMcPointCoordSystem eCoordinateSystem, 
														   bool bEnabled = true) = 0;

	//==============================================================================
	// Method Name: GetCoordinateSystemConversion()
	//------------------------------------------------------------------------------
	/// Retrieves coordinate system conversion of the item's points set by SetCoordinateSystemConversion()
	///
	/// \param[out]	peCoordinateSystem	Coordinate system (world/screen/image) to convert points to (see #EMcPointCoordSystem); 
	/// \param[out]	pbEnabled			Whether or not coordinate system conversion is enabled (NULL can be passed if unnecessary)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCoordinateSystemConversion(EMcPointCoordSystem *peCoordinateSystem, 
														   bool *pbEnabled = NULL) const = 0;

	//==============================================================================
	// Method Name: SetRotationAlignment()
	//------------------------------------------------------------------------------
	/// Sets the item's rotation alignment that allows resetting the item's rotation inherited from its parent node or 
	/// aligning it to an orientation of another coordinate system.
	///
	/// The flags \p bAlignYaw, \p bAlignPitch, \p bAlignRoll define which rotation angles should be reset/aligned; the defaults are false.
	/// 
	/// There are 3 cases:
	/// - Both the coordinate system of the item's points and \p eAlignToCoordinateSystem are #EPCS_SCREEN or both coordinate systems are not #EPCS_SCREEN: 
	///   the appropriate rotation angles are reset to zero. Used to cancel rotation.
	/// - The coordinate system of the item's points is not #EPCS_SCREEN and \p eAlignToCoordinateSystem is #EPCS_SCREEN: the appropriate rotation angles 
	///	  will be equal to map camera's orientation angles. Used to define world-coordinate-system items with screen orientation.
	/// - The coordinate system of the item's points is #EPCS_SCREEN and \p eAlignToCoordinateSystem is not #EPCS_SCREEN: the yaw rotation angle 
	///   (the only angle applicable to screen rotation) will the sum of the appropriate angles (usually one angle) of map camera's orientation. 
	///   Used to define screen-coordinate-system items with world orientation e.g. world-oriented screen-size picture, north arrow etc.
	/// 
	/// \param[in]	eAlignToCoordinateSystem	Coordinate system to align to.
	/// \param[in]	bAlignYaw					Whether to reset/align yaw angle.
	/// \param[in]	bAlignPitch					Whether to reset/align pitch angle (relevant in 3D viewports only).
	/// \param[in]	bAlignRoll					Whether to reset/align roll angle (relevant in 3D viewports only).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetRotationAlignment(EMcPointCoordSystem eAlignToCoordinateSystem,
		bool bAlignYaw = true, bool bAlignPitch = true, bool bAlignRoll = true) = 0; 

	//==============================================================================
	// Method Name: GetRotationAlignment()
	//------------------------------------------------------------------------------
	/// Retrieves the item's rotation alignment set by SetRotationAlignment().
	///
	/// \param[out]	peAlignToCoordinateSystem	Coordinate system to align to.
	/// \param[out]	pbAlignYaw					Whether to reset/align yaw angle; can be NULL if unnecessary.
	/// \param[out]	pbAlignPitch				Whether to reset/align pitch angle (relevant in 3D viewports only); can be NULL	if unnecessary.
	/// \param[out]	pbAlignRoll					Whether to reset/align roll angle (relevant in 3D viewports only); can be NULL if unnecessary.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRotationAlignment(EMcPointCoordSystem *peAlignToCoordinateSystem,
		bool *pbAlignYaw = NULL, bool *pbAlignPitch = NULL, bool *pbAlignRoll = NULL) const = 0;

	//==============================================================================
	// Method Name: SetVectorOffsetValue()
	//------------------------------------------------------------------------------
	/// Sets the item's vector offset value as a shared or private property.
	///
	/// The vector offset allows offsetting the item's points by a vector based on segment of the item's parent 
	/// (defined by SetVectorTransformParentIndex() and SetVectorTransformSegment()) and the value defined here; 
	/// the vector is calculated according to #EVectorOffsetCalc defined by SetVectorOffsetCalc().
	///
	/// \param[in]	fVectorOffsetValue	Vector offset value used to calculate a vector offset (see description above).
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetVectorOffsetValue(float fVectorOffsetValue,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID, 
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetVectorOffsetValue()
	//------------------------------------------------------------------------------
	/// Retrieves vector offset value property set by SetVectorOffsetValue().
	///
	/// \param[out]	pfVectorOffsetValue	Offset value.
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVectorOffsetValue(float *pfVectorOffsetValue,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetOffset()
	//------------------------------------------------------------------------------
	/// Sets the item's offset that is added to the item's points as a shared or private property.
	///
	/// The offset can be absolute or relative (rotated according to the rotation of either the item itself or its parent node 
	/// according to SetOffsetOrientation()); the default behavior is #EOO_RELATIVE_TO_PARENT_ROTATION.
	///
	/// \param[in]	Offset				Offset that is added to the item's points.
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetOffset(const SMcFVector3D &Offset,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID, 
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetOffset()
	//------------------------------------------------------------------------------
	/// Retrieves the offset property defined by SetOffset().
	///
	/// The offset can be absolute or relative (rotated according to the rotation of either the item itself or its parent node 
	/// according to SetOffsetOrientation()); the default behavior is #EOO_RELATIVE_TO_PARENT_ROTATION.
	///
	/// \param[out]	pOffset				Offset that is added to the item's points.
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetOffset(SMcFVector3D *pOffset,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetPointsDuplication()
	//------------------------------------------------------------------------------
	/// Defines a replacement of some points of the item by a number of duplicates (in offsets from each other) as a shared or private property.
	///
	/// \param[in]	anPointIndicesAndDuplicates		The even-length array, each two consecutive members of which define accordingly an index of 
	///												an original point to duplicate and a number of duplicates to replace it in offsets defined 
	///												by SetPointsDuplicationOffsets(). 
	///												A negative index means indexing from the end: -1 for the last one, -2 for one before the last, etc.; 
	///												a negative number of points means offsetting in inverse direction, zero number of points means 
	///												canceling the appropriate original point. If there are several duplication groups for the same 
	///												original point index, they will be merged. 
	///												The parameter's meaning depends on \a uPropertyID
	///												and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID						The private property ID or one of the following special values:
	///												IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe				The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetPointsDuplication(const IMcProperty::SArrayProperty<int> &anPointIndicesAndDuplicates, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetPointsDuplication()
	//------------------------------------------------------------------------------
	/// Retrieves the points' duplication property set by SetPointsDuplication().
	///
	/// \param[out]	panPointIndicesAndDuplicates	The even-length array, each two consecutive members of which define accordingly an index of 
	///												an original point to duplicate and a number of duplicates to replace it in offsets defined 
	///												by SetPointsDuplicationOffsets(). 
	///												A negative index means indexing from the end: -1 for the last one, -2 for one before the last, etc.; 
	///												a negative number of points means offsetting in inverse direction, zero number of points means 
	///												canceling the appropriate original point. If there are several duplication groups for the same 
	///												original point index, they will be merged. 
	///												The parameter's meaning depends on \a puPropertyID
	///												and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID					The private property ID or one of the following special values:
	///												IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe				The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetPointsDuplication(IMcProperty::SArrayProperty<int> *panPointIndicesAndDuplicates,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetPointsDuplicationOffsets()
	//------------------------------------------------------------------------------
	/// Sets the offsets for the points' duplication (defined by  SetPointsDuplication()) as a shared or private property.
	///
	/// \param[in]	aDuplicationOffsets		The array of offsets for the duplicate points defined by SetPointsDuplication(): for each duplicate point 
	///										(except for the first duplicate in each group) there should be an offset from the previous one.
	///										If the array length is less than the number of required offsets, the offsets are repeated cyclically 
	///										(e.g., one-element array can be used if all the offsets should be the same).
	///										The parameter's meaning depends on \a uPropertyID
	///										and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID				The private property ID or one of the following special values:
	///										IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe		The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetPointsDuplicationOffsets(const IMcProperty::SArrayProperty<SMcFVector3D> &aDuplicationOffsets, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetPointsDuplicationOffsets()
	//------------------------------------------------------------------------------
	/// Retrieves the property of points' duplication offsets set by SetPointsDuplication().
	///
	/// \param[out]	paDuplicationOffsets	The array of offsets for the duplicate points defined by SetPointsDuplication(): for each duplicate point  
	///										(except for the first duplicate in each group) there should be an offset from the previous one.
	///										If the array length is less than the number of required offsets, the offsets are repeated cyclically 
	///										(e.g., one-element array can be used if all the offsets should be the same).
	///										If the array length is less than the above mentioned number of pairs, the offsets will be repeated 
	///										cyclically (e.g., one-element array can be used if all the offsets should be the same).
	///										The parameter's meaning depends on \a puPropertyID
	///										and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID			The private property ID or one of the following special values:
	///										IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe		The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetPointsDuplicationOffsets(IMcProperty::SArrayProperty<SMcFVector3D> *paDuplicationOffsets,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetVectorRotation()
	//------------------------------------------------------------------------------
	/// Defines the vector rotation flag as a shared or private property.
	///
	/// The vector rotation allows rotating the item by the angle defined by the direction of a vector based on segment of the item's parent 
	/// (defined by SetVectorTransformParentIndex() and SetVectorTransformSegment()).
	///
	/// \param[in]	bEnabled			Whether vector rotation transform is enabled
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetVectorRotation(bool bEnabled,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetVectorRotation()
	//------------------------------------------------------------------------------
	/// Retrieves the vector rotation flag defined by SetVectorRotation().
	///
	/// The vector rotation allows rotating the item by the angle defined by the direction of a vector based on segment of the item's parent 
	/// (defined by SetVectorTransformParentIndex() and SetVectorTransformSegment()).
	///
	/// \param[out]	pbEnabled			Whether vector rotation transform is enabled
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVectorRotation(bool *pbEnabled,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetRotationRoll()
	//------------------------------------------------------------------------------
	/// Sets the item's rotation roll angle as a shared or private property.
	///
	/// The rotation is performed in XZ plane (positive angle for clockwise direction as seen along Y axis).
	///
	/// \param[in]	fRoll				Roll angle value.
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetRotationRoll(float fRoll,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetRotationRoll()
	//------------------------------------------------------------------------------
	/// Retrieves rotation roll angle property set by SetRotationRoll().
	///
	/// The rotation is performed in XZ plane (positive angle for clockwise direction as seen along Y axis).
	///
	/// \param[out]	pfRoll				Roll angle value.
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRotationRoll(float *pfRoll,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetRotationPitch()
	//------------------------------------------------------------------------------
	/// Sets the item's rotation pitch angle as a shared or private property.
	///
	/// The rotation is performed in YZ plane (positive angle for clockwise direction as seen along X axis - elevation angle direction).
	///
	/// \param[in]	fPitch				Pitch angle value.
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetRotationPitch(float fPitch, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetRotationPitch()
	//------------------------------------------------------------------------------
	/// Retrieves rotation pitch angle property set by SetRotationPitch().
	///
	/// The rotation is performed in YZ plane (positive angle for clockwise direction as seen along X axis - elevation angle direction).
	///
	/// \param[out]	pfPitch				Pitch angle value.
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRotationPitch(float *pfPitch,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetRotationYaw()
	//------------------------------------------------------------------------------
	/// Sets the item's rotation yaw angle as a shared or private property.
	///
	/// The rotation is performed in XY plane (positive angle for clockwise direction as seen from above - azimuth angle direction).
	///
	/// \param[in]	fYaw				Yaw angle value.
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetRotationYaw(float fYaw, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetRotationYaw()
	//------------------------------------------------------------------------------
	/// Retrieves rotation yaw angle property set by SetRotationYaw().
	///
	/// The rotation is performed in XY plane (positive angle for clockwise direction as seen from above - azimuth angle direction).
	///
	/// \param[out]	pfYaw				Yaw angle value.
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRotationYaw(float *pfYaw,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Sub-items' Data
	//@{

	//==============================================================================
	// Method Name: SetSubItemsData()
	//------------------------------------------------------------------------------
	/// Sets sub-items' data as a shared or private property.
	///
	/// Sub-items' data defines how a symbolic item is divided into sub-items (by dividing the item's points into sequential groups, 
	/// each one is rendered like independent item). The visibility of sub-items with specific IDs can be set for the whole overlay 
	/// by IMcOverlay::SetSubItemsVisibility().
	/// 
	/// Applicable to straight-segments' line/polygon item, text item and picture item only:
	/// - Straight-segments' line/polygon item: divided into separate polylines/polygons.
	/// - Text item: divided into groups of texts, each group has the same ID (SMcSubItemData::uSubItemID) and use the same sub-text from 
	///   SMcVariantString's array.
	/// - Picture item: divided into groups of pictures, each group has the same ID (SMcSubItemData::uSubItemID) and (in case of IMcTextureArray only) 
	///   the same sub-texture from IMcTextureArray.
	///
	/// \param[in] SubItemsData			The sub-items' data array (see SMcSubItemData for details).
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetSubItemsData(
		const IMcProperty::SArrayProperty<SMcSubItemData> &SubItemsData,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetSubItemsData()
	//------------------------------------------------------------------------------
	/// Retrieves sub-items' data property set by SetSubItemsData().
	///
	/// \param[out] pSubItemsData		The sub-items' data array (see SMcSubItemData for details).
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetSubItemsData(
		IMcProperty::SArrayProperty<SMcSubItemData> *pSubItemsData,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Priority
	//@{

	//==============================================================================
	// Method Name: SetDrawPriorityGroup()
	//------------------------------------------------------------------------------
	/// Defines the item's draw priority group as a shared or private property.
	///
	/// The draw priority group defines the drawing order of the item relative to items of other groups.
	/// 
	/// \param[in] eDrawPriorityGroup	The draw priority group (see IMcSymbolicItem::EDrawPriorityGroup for details); the default is #EDPG_REGULAR.
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetDrawPriorityGroup(
		EDrawPriorityGroup eDrawPriorityGroup,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetDrawPriorityGroup()
	//------------------------------------------------------------------------------
	/// Retrieves the item's draw priority group property set by SetDrawPriorityGroup().
	///
	/// The draw priority group defines the drawing order of the item relative to items of other groups.
	///
	/// \param[out] peDrawPriorityGroup	The draw priority group (see IMcSymbolicItem::EDrawPriorityGroup for details)
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetDrawPriorityGroup(
		EDrawPriorityGroup *peDrawPriorityGroup,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetDrawPriority()
	//------------------------------------------------------------------------------
	/// Defines the item's draw priority as a shared or private property.
	/// 
	/// Not relevant for world-coordinate items not attached to terrain in 3D viewport, use SetCoplanar3DPriority() instead.
	///
	/// \param[in] nPriority			The item's draw priority in the range of -127 to 127; 
	///									items with higher priority are rendered on top of items with lower priority; the default is 0.
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetDrawPriority(
		signed char nPriority,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetDrawPriority()
	//------------------------------------------------------------------------------
	/// Retrieves the item's draw priority property set by SetDrawPriority().
	///
	/// Not relevant for world-coordinate items not attached to terrain in 3D viewport, use GetCoplanar3DPriority() instead.
	///
	/// \param [out] pnPriority			The item's draw priority in the range of -127 to 127; 
	///									items with higher priority are rendered on top of items with lower priority; the default is 0.
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///	
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetDrawPriority(
		signed char *pnPriority,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetCoplanar3DPriority()
	//------------------------------------------------------------------------------
	/// Defines the item's coplanar 3D priority as a shared or private property.
	///
	/// Relevant only for world-coordinate items not attached to terrain in 3D viewport, otherwise use SetDrawPriority().
	///
	/// \param[in] nPriority			The item's coplanar priority in the range of –128 to 127;
	///									items with higher priority are rendered on top of items with lower priority; the default is 0.
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetCoplanar3DPriority(
		signed char nPriority,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetCoplanar3DPriority()
	//------------------------------------------------------------------------------
	/// Retrieves the item's coplanar 3D priority property set by SetCoplanar3DPriority().
	///
	/// Relevant only for world-coordinate items not attached to terrain in 3D viewport, otherwise use GetDrawPriority().
	///
	/// \param[out] pnPriority			The item's coplanar priority in the range of –128 to 127;
	///									items with higher priority are rendered on top of items with lower priority; the default is 0.
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///	
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCoplanar3DPriority(
		signed char *pnPriority,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Transparency and Texture Filtering
	//@{

	//==============================================================================
	// Method Name: SetTransparency()
	//------------------------------------------------------------------------------
	/// Defines the item's transparency as a shared or private property.
	///
	/// The transparency modulates the alpha-component (SMcBColor::a) of the item's each color property by multiplying it by \a byTransparency / 255.
	/// 
	/// \param[in] byTransparency		The item's transparency; the default is 255 (meaning SMcBColor::a is used as is).
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetTransparency(BYTE byTransparency, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetTransparency()
	//------------------------------------------------------------------------------
	/// Retrieves the item's transparency property set by SetTransparency().
	///
	/// The transparency modulates the alpha-component (SMcBColor::a) of the item's each color property by multiplying it by \a byTransparency / 255.
	/// 
	/// \param[out] pbyTransparency		The item's transparency; the default is 255 (meaning SMcBColor::a is used as is).
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetTransparency(BYTE *pbyTransparency,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetTextureFiltering()
	//------------------------------------------------------------------------------
	/// Sets texture filtering options defining how the item's texture is resized and which mipmap levels are used during rendering.
	///
	/// The defaults are #ETF_DEFAULT (default filter selected by MapCore according to the item's type).
	///
	/// \param[in]	eMinFilter		The Filter used in texture minification
	/// \param[in]	eMagFilter		The Filter used in texture magnification
	/// \param[in]	eMipmapFilter	The Filter used in mipmap determination
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTextureFiltering(ETextureFilter eMinFilter, 
		ETextureFilter eMagFilter, ETextureFilter eMipmapFilter) = 0;

	//==============================================================================
	// Method Name: GetTextureFiltering()
	//------------------------------------------------------------------------------
	/// Retrieves texture filtering options defining how the item's texture is resized and which mipmap levels are used during rendering.
	///
	/// The defaults are #ETF_DEFAULT (default filter selected by MapCore according to the item's type).
	///
	/// \param[out]	peMinFilter		The Filter used in texture minification
	/// \param[out]	peMagFilter		The Filter used in texture magnification
	/// \param[out]	peMipmapFilter	The Filter used in mipmap determination
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTextureFiltering(ETextureFilter *peMinFilter, 
		ETextureFilter *peMagFilter, ETextureFilter *peMipmapFilter) const = 0;

	//@}

	/// \name Special Material
	//@{

	//==============================================================================
	// Method Name: SetSpecialMaterial()
	//------------------------------------------------------------------------------
	/// Sets special material which should override the item's standard material.
	///
	/// \param[in]	strSpecialMaterial				The special material name.
	/// \param[in]	bSpecialMaterialUseItemTexture	Whether or not to use the item's texture in the special material.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSpecialMaterial(PCSTR strSpecialMaterial, bool bSpecialMaterialUseItemTexture) = 0;

	//==============================================================================
	// Method Name: GetSpecialMaterial()
	//------------------------------------------------------------------------------
	/// Retrieves special material set by SetSpecialMaterial() overriding the item's standard material.
	///
	/// \param[out]	pstrSpecialMaterial					The special material name.
	/// \param[out]	pbSpecialMaterialUseItemTexture		Whether or not the item's texture is used in the special material.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSpecialMaterial(PCSTR *pstrSpecialMaterial, bool *pbSpecialMaterialUseItemTexture) const = 0;

	//@}

	/// \name Move if Blocked
	//@{

	//==============================================================================
	// Method Name: SetMoveIfBlockedMaxChange()
	//------------------------------------------------------------------------------
	/// Sets the maximum height change for the item's move-if-blocked mode as a shared or private property.
	///
	/// The move-if-blocked mode allows to automatically move the item up when the its line-of-sight is blocked by the terrain 
	/// in order to make it visible above the terrain. Enabled if \p fMaxChange is not zero. The required height above the blocking obstacle 
	/// is set by SetMoveIfBlockedHeightAboveObstacle().
	///
	/// The item's move-if-blocked mode considered only if it is not disabled in the overlay manager by IMcOverlayManager::SetMoveIfBlockedMode().
	///
	/// \param[in] fMaxChange			The maximum height change; the default is zero that means not to move the item. 
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetMoveIfBlockedMaxChange(
		float fMaxChange,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetMoveIfBlockedMaxChange()
	//------------------------------------------------------------------------------
	/// Retrieves move-if-blocked maximum height change property set by SetMoveIfBlockedMaxChange().
	///
	/// The move-if-blocked mode allows to automatically move the item up when the its line-of-sight is blocked by the terrain 
	/// in order to make it visible above the terrain. Enabled if \p fMaxChange is not zero. The required height above the blocking obstacle 
	/// is set by SetMoveIfBlockedHeightAboveObstacle().
	///
	/// The item's move-if-blocked mode considered only if it is enabled in the overlay manager by IMcOverlayManager::SetMoveIfBlockedMode().
	///
	/// \param[out] pfMaxChange			The maximum height change; the default is zero that means not to move the item. 
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetMoveIfBlockedMaxChange(
		float *pfMaxChange,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetMoveIfBlockedHeightAboveObstacle()
	//------------------------------------------------------------------------------
	/// Sets the move-if-blocked mode's height above the blocking obstacle as a shared or private property.
	///
	/// Relevant if the move-if-blocked mode is enabled in both the item by SetMoveIfBlockedMaxChange() 
	/// and the overlay manager by IMcOverlayManager::SetMoveIfBlockedMode().
	///
	/// \param[in] fHeightAboveObstacle	The height above the blocking obstacle;	the default is zero.
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetMoveIfBlockedHeightAboveObstacle(
		float fHeightAboveObstacle,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetMoveIfBlockedHeightAboveObstacle()
	//------------------------------------------------------------------------------
	/// Retrieves the move-if-blocked-height-above-obstacle property set by SetMoveIfBlockedHeightAboveObstacle();
	///
	/// Relevant if the move-if-blocked mode is enabled in both the item by SetMoveIfBlockedMaxChange() 
	/// and the overlay manager by IMcOverlayManager::SetMoveIfBlockedMode().
	///
	/// \param[out] pfHeightAboveObstacle	he height above the blocking obstacle; the default is zero.
	///										The parameter's meaning depends on \a puPropertyID
	///										and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID			The private property ID or one of the following special values:
	///										IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe		The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetMoveIfBlockedHeightAboveObstacle(
		float *pfHeightAboveObstacle,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}
};

//==================================================================================
// Interface Name: IMcEmptySymbolicItem
//----------------------------------------------------------------------------------
///	Interface for empty symbolic item.
///
/// The item that has no visual presentation, but attach points and transform data can be defined.
/// The item can be used as a transform-only node, a parent for other symbolic items.
//==================================================================================
class IMcEmptySymbolicItem : public virtual IMcSymbolicItem
{
protected:

	virtual ~IMcEmptySymbolicItem() {};

public:

	enum
	{
		//==============================================================================
		/// Node unique ID for this interface
		//==============================================================================
		NODE_TYPE = 52
	};

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates an empty symbolic item.
	///
	/// The item will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Release() method is called,
	/// when its (or its parents) Disconnect() method is called,
	/// or when its object scheme is destroyed.
	///
	/// \param[out] ppItem					The newly created item
	///
	/// \return
	///     - Status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(IMcEmptySymbolicItem **ppItem);

	//@}

	/// \name Clone
	//@{

	//==============================================================================
	// Method Name: Clone(...)
	//------------------------------------------------------------------------------
	/// Clone the item.
	///
	/// \param[out] ppClonedItem	The newly cloned item
	/// \param[in]  pObject			Optional object, to take the values of private properties from
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Clone(
		IMcEmptySymbolicItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}
};
