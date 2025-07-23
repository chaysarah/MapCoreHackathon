#pragma once
//==================================================================================
/// \file IMcObjectSchemeItem.h
/// Base interface for object scheme items (interfaces derived from IMcSymbolicItem and IMcPhysicalItem)
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "McCommonTypes.h"
#include "OverlayManager/IMcObjectSchemeNode.h"

//==================================================================================
// Interface Name: IMcObjectSchemeItem
//----------------------------------------------------------------------------------
/// Base interface for object scheme items (interfaces derived from  IMcSymbolicItem and IMcPhysicalItem)
//==================================================================================
class IMcObjectSchemeItem : public virtual IMcObjectSchemeNode
{
protected:

	virtual ~IMcObjectSchemeItem() {}

public:

	//==============================================================================
	// Enum name: EItemSubTypeFlags
	//------------------------------------------------------------------------------
	/// Item sub-type bits (exactly one of #EISTF_WORLD or #EISTF_SCREEN bits should be present, other bits are optional).
	//==============================================================================
	enum EItemSubTypeFlags 
	{
		/// World sub-type: 
		/// - For line-based items means world-unit line width and line texture mapping.
		/// - For text/picture items means world-unit size.
		EISTF_WORLD							= 0x0001,

		/// Screen sub-type: 
		/// - For line-based items means screen-unit line width and line texture mapping.
		/// - For text/picture items means screen-unit size.
		EISTF_SCREEN						= 0x0002,

		/// Whether the item is attached to terrain in 3D viewport (rendered as terrain's layer 
		/// above all its raster layers).
		EISTF_ATTACHED_TO_TERRAIN			= 0x0004,

		/// Whether the item is rendered with accurate width in pixels in 3D viewport (can affect the performance, not to be used in all lines). 
		/// Can be used only in screen-width line-based items that are not attached to terrain 
		/// (with #EISTF_SCREEN and without #EISTF_ATTACHED_TO_TERRAIN flag) other than filled closed shapes.
		EISTF_ACCURATE_3D_SCREEN_WIDTH		= 0x0008
	};

	//==============================================================================
	// Enum name: EGeometryType
	//------------------------------------------------------------------------------
	/// Type of a geometry of an item or item's offset; defined by a meaning of its length/angle properties.
	///
	/// \note
	/// The only geometry type allowed for screen-size items and offsets is #EGT_GEOMETRIC_IN_VIEWPORT.
	//==============================================================================
	enum EGeometryType 
	{
		/// Geometric in overlay manager's coordinate system:
		/// - Length/angle properties are interpreted in overlay manager's coordinate system.
		/// - If map viewport's and overlay manager's coordinate system differ, the item is deformed 
		///   in that map viewport to cover the same area in its coordinate system.
		EGT_GEOMETRIC_IN_OVERLAY_MANAGER,

		/// Geometric in map viewport's coordinate system:
		/// - Length/angle properties are interpreted by every map viewport in its coordinate system.
		/// - The area covered by the item may differ in different map viewports.
		EGT_GEOMETRIC_IN_VIEWPORT,

		/// Geographic:
		/// - Length properties are interpreted in meters on Earth ellipsoid surface, 
		///   angle properties are interpreted as geographic azimuths.
		/// - The item is always deformed in every map viewport to cover the same geographic area in its coordinate system.
		EGT_GEOGRAPHIC
	};

	//==============================================================================
	// Enum name: EGeometryType
	//------------------------------------------------------------------------------
	/// Type of ellipse/arc item definition used in IMcEllipseItem and IMcArcItem; 
	/// Specifies how the item is calculated based on its point(s) and properties.
	///
	/// \note
	/// The arc direction is always clockwise.
	//==============================================================================
	enum EEllipseDefinition 
	{
		/// Elliptical arc by center, x-radius, y-radius, start/end angles; 
		/// one location point defines the center.
		EED_ELLIPSE_CENTER_RADIUSES_ANGLES,
		
		/// Circular arc by center, radius, start/end angles; 
		/// one location point defines the center; 
		/// x-radius is used, y-radius is ignored.
		EED_CIRCLE_CENTER_RADIUS_ANGLES,
		
		/// Circular arc by center, any point on the arc, start/end angles; 
		/// two location points define the center and point on the arc accordingly; 
		/// x/y-radiuses are ignored.
		EED_CIRCLE_CENTER_POINT_ANGLES,

		/// Circular arc by start point, center, end point (start point should lie on the arc); 
		/// three location points define the start point, center, end point accordingly; 
		/// x/y-radiuses and start/end angles are ignored.
		EED_CIRCLE_START_POINT_CENTER_END_POINT
	};

	/// \name Item Sub Type
	//@{

	//==============================================================================
	// Method Name: GetItemSubType()
	//------------------------------------------------------------------------------
	/// Returns the item's sub type bit field (based on #EItemSubTypeFlags).
	//==============================================================================
	virtual UINT GetItemSubType() const = 0;

	//@}

	/// \name Clone
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
	virtual IMcErrors::ECode Clone(
		IMcObjectSchemeItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}

	/// \name Disconnect
	//@{

	//==============================================================================
	// Method Name: Disconnect()
	//------------------------------------------------------------------------------
	/// Disconnects the item from its parents and destroys it (with all its children).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Disconnect() = 0;

	//@}

	/// \name Detectibility
	//@{

	//==============================================================================
	// Method Name: SetDetectibility()
	//------------------------------------------------------------------------------
	/// Sets whether or not the item will be retrieved by IMcSpatialQueries::ScanInGeometry().
	///
	/// \param[in] bDetectibility		The item's detectibility; the default is true.
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetDetectibility(bool bDetectibility) = 0;

	//==============================================================================
	// Method Name: GetDetectibility()
	//------------------------------------------------------------------------------
	/// Retrieves the item's detectibility set by SetDetectibility().
	///
	/// \param[out] pbDetectibility		The item's detectibility; the default is true.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetDetectibility(bool *pbDetectibility) const = 0;

	//@}

	/// \name Hidden if Viewport is Overloaded
	//@{

	//==============================================================================
	// Method Name: SetHiddenIfViewportOverloaded()
	//------------------------------------------------------------------------------
	/// Sets whether the item should be hidden when a viewport is overloaded.
	///
	/// The default is false.
	///
	/// \param[in] bHiddenIfViewportOverloaded		whether should be hidden
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetHiddenIfViewportOverloaded(bool bHiddenIfViewportOverloaded) = 0;

	//==============================================================================
	// Method Name: GetHiddenIfViewportOverloaded()
	//------------------------------------------------------------------------------
	/// Retrieves whether the item should be hidden when a viewport is overloaded.
	///
	/// \param[out] pbHiddenIfViewportOverloaded	whether should be hidden
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetHiddenIfViewportOverloaded(bool *pbHiddenIfViewportOverloaded) const = 0;

	//@}

	/// \name Blocked Transparency
	//@{

	//==============================================================================
	// Method Name: SetBlockedTransparency()
	//------------------------------------------------------------------------------
	/// Sets the transparency of the item's parts blocked by terrain as shared or private property.
	///
	/// The item parts blocked by the terrain are seen is follows:
	/// - when \a byTransparency == 0 (the default) they are not seen at all;
	/// - when \a byTransparency == 255 they are seen opaque over the terrain;
	/// - when 0 < \a byTransparency < 255 they are seen semi-transparent over the terrain.
	///
	/// The item's blocked transparency considered only if it is enabled in the overlay manager by IMcOverlayManager::SetMoveIfBlockedMode().
	///
	/// \param[in] byTransparency		The transparency to use for the item's parts blocked by terrain; the default is 0.
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
	virtual IMcErrors::ECode SetBlockedTransparency(
		BYTE byTransparency,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetBlockedTransparency()
	//------------------------------------------------------------------------------
	/// Retrieves the blocked transparency property set by SetBlockedTransparency().
	///
	/// The item parts blocked by the terrain are seen is follows:
	/// - when \a byTransparency == 0 (the default) they are not seen at all;
	/// - when \a byTransparency == 255 they are seen opaque over the terrain;
	/// - when 0 < \a byTransparency < 255 they are seen semi-transparent over the terrain.
	///
	/// The item's blocked transparency considered only if it is enabled in the overlay manager by IMcOverlayManager::SetMoveIfBlockedMode().
	///
	/// \param[out] pbyTransparency		The transparency used for the item's parts blocked by terrain; the default is 0.
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
	virtual IMcErrors::ECode GetBlockedTransparency(
		BYTE *pbyTransparency,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Participation in Sight Queries
	//@{

	//==============================================================================
	// Method Name: SetParticipationInSightQueries()
	//------------------------------------------------------------------------------
	/// Sets whether or not the item should be considered in area-of-sight queries.
	/// 
	/// The defaults are:
	///	- true for physical items and symbolic items of the following types: 
	///	  procedural geometry items and 3D-shape close shape items;
	///	- false for the rest of symbolic items.
	///	
	///	Each time IMcClosedShapeItem::SetShapeType() is called, the participation option will be 
	///	reset to its default defined above according to the shape type.
	///
	/// \param[in]	bParticipates		Whether or not the item is considered in area-of-sight queries.
	///
	/// \note
	/// If the item does not participate in area-of-sight queries, items connected to it
	/// do not participate either regardless to their own participation flags
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetParticipationInSightQueries(bool bParticipates) = 0;

	//==============================================================================
	// Method Name: GetParticipationInSightQueries()
	//------------------------------------------------------------------------------
	/// Retrieves the item's participation in area-of-sight queries set by SetParticipationInSightQueries().
	///
	/// The defaults are:
	///	- true for physical items and symbolic items of the following types: 
	///	  procedural geometry items and 3D-shape close shape items;
	///	- false for the rest of symbolic items.
	///	
	///	Each time IMcClosedShapeItem::SetShapeType() is called, the participation option will be 
	///	reset to its default defined above according to the shape type.
	///
	/// \param[out]	pbParticipates		Whether or not the item is considered in area-of-sight queries.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetParticipationInSightQueries(bool *pbParticipates) const = 0;

	//@}
};
