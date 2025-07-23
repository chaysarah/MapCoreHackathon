#pragma once
//==================================================================================
/// \file IMcObjectSchemeNode.h
/// Base interface for object scheme nodes (interfaces derived from IMcObjectLocation, and IMcObjectSchemeItem).
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcBase.h"
#include "McCommonTypes.h"
#include "CMcDataArray.h"
#include "OverlayManager/IMcProperty.h"
#include "OverlayManager/IMcConditionalSelector.h"

class IMcObjectLocation;
class IMcObjectSchemeItem;
class IMcPhysicalItem;
class IMcSymbolicItem;
class IMcEmptyPhysicalItem;
class IMcEmptySymbolicItem;
class IMcLineBasedItem;
class IMcClosedShapeItem;
class IMcLightBasedItem;
class IMcLocationBasedLightItem;
class IMcProceduralGeometryItem;
class IMcArcItem;
class IMcArrowItem;
class IMcEllipseItem;
class IMcPolygonItem;
class IMcRectangleItem;
class IMcLineItem;
class IMcLineExpansionItem;
class IMcMeshItem;
class IMcPictureItem;
class IMcTextItem;
class IMcDirectionalLightItem;
class IMcPointLightItem;
class IMcSpotLightItem;
class IMcParticleEffectItem;
class IMcSoundItem;
class IMcProjectorItem;
class IMcManualGeometryItem;
class IMcObjectScheme;
class IMcObject;
class IMcConditionalSelector;
class IMcMapViewport;

//==================================================================================
// Interface Name: IMcObjectSchemeNode
//----------------------------------------------------------------------------------
/// Base interface for object scheme nodes (interfaces derived from IMcObjectLocation, and IMcObjectSchemeItem).
//==================================================================================
class IMcObjectSchemeNode : public virtual IMcBase
{
protected:

	virtual ~IMcObjectSchemeNode() {}

public:

	//==============================================================================
	// Enum Name: ENodeKindFlags
	//------------------------------------------------------------------------------
	/// Bits of kinds (categories) of object scheme nodes.
	///
	/// Used as one bit in GetNodeKind() and IMcObjectScheme::GetNodes(), as a bit field in IMcSpatialQueries::SQueryParams::uItemKindsBitField
	//==============================================================================
	enum ENodeKindFlags
	{
		/// None
		ENKF_NONE				= 0x0000,

		/// An object location: IMcObjectLocation
		ENKF_OBJECT_LOCATION	= 0x0001,

		/// A physical item derived from IMcPhysicalItem
		ENKF_PHYSICAL_ITEM		= 0x0002,

		/// a symbolic item derived from IMcSymbolicItem
		ENKF_SYMBOLIC_ITEM		= 0x0004,

		/// Any object scheme item (symbolic or physical) derived from IMcObjectSchemeItem 
		/// (a combination of #ENKF_PHYSICAL_ITEM and #ENKF_SYMBOLIC_ITEM bits)
		ENKF_ANY_ITEM			= ENKF_PHYSICAL_ITEM | ENKF_SYMBOLIC_ITEM,

		/// Any object scheme node (object location or object scheme item) derived from IMcObjectSchemeNode 
		/// (a combination of #ENKF_OBJECT_LOCATION, #ENKF_PHYSICAL_ITEM and #ENKF_SYMBOLIC_ITEM bits)
		ENKF_ANY_NODE			= ENKF_OBJECT_LOCATION | ENKF_ANY_ITEM
	};

public:

	/// \name Node Kind, Node Type and Casting
	//@{

	//==============================================================================
	// Method Name: GetNodeKind()
	//------------------------------------------------------------------------------
	/// Retrieves the node kind (category): object location, physical item or symbolic item, see #ENodeKindFlags.
	///
	/// \return
	///     The node kind (one bit of #ENodeKindFlags)
	//==============================================================================
	virtual ENodeKindFlags GetNodeKind() const = 0;

	//==============================================================================
	// Method Name: GetNodeType()
	//------------------------------------------------------------------------------
	/// Retrieves the node type unique ID.
	///
	/// \return
	///		The node type unique ID (e.g. IMcLineItem::NODE_TYPE).
	///
	/// \remark
	///		Use the cast methods in order to get the correct type.
	//==============================================================================
	virtual UINT GetNodeType() const = 0;

	//==============================================================================
	// Method Name: CastToObjectLocation()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcObjectLocation*
	/// 
	/// \return
	///     - #IMcObjectLocation*
	//==============================================================================
	virtual IMcObjectLocation* CastToObjectLocation() = 0;

	//==============================================================================
	// Method Name: CastToObjectSchemeItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcObjectSchemeItem*
	/// 
	/// \return
	///     - #IMcObjectSchemeItem*
	//==============================================================================
	virtual IMcObjectSchemeItem* CastToObjectSchemeItem() = 0;

	//==============================================================================
	// Method Name: CastToPhysicalItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcPhysicalItem*
	/// 
	/// \return
	///     - #IMcPhysicalItem*
	//==============================================================================
	virtual IMcPhysicalItem* CastToPhysicalItem() = 0;

	//==============================================================================
	// Method Name: CastToSymbolicItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcSymbolicItem*
	/// 
	/// \return
	///     - #IMcSymbolicItem*
	//==============================================================================
	virtual IMcSymbolicItem* CastToSymbolicItem() = 0;

	//==============================================================================
	// Method Name: CastToEmptyPhysicalItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcEmptyPhysicalItem*
	/// 
	/// \return
	///     - #IMcEmptyPhysicalItem*
	//==============================================================================
	virtual IMcEmptyPhysicalItem* CastToEmptyPhysicalItem() = 0;

	//==============================================================================
	// Method Name: CastToEmptySymbolicItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcEmptySymbolicItem*
	/// 
	/// \return
	///     - #IMcEmptySymbolicItem*
	//==============================================================================
	virtual IMcEmptySymbolicItem* CastToEmptySymbolicItem() = 0;

	//==============================================================================
	// Method Name: CastToArcItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcArcItem*
	/// 
	/// \return
	///     - #IMcArcItem*
	//==============================================================================
	virtual IMcArcItem* CastToArcItem() = 0;

	//==============================================================================
	// Method Name: CastToArrowItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcArrowItem*
	/// 
	/// \return
	///     - #IMcArrowItem*
	//==============================================================================
	virtual IMcArrowItem* CastToArrowItem() = 0;

	//==============================================================================
	// Method Name: CastToEllipseItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcEllipseItem*
	/// 
	/// \return
	///     - #IMcEllipseItem*
	//==============================================================================
	virtual IMcEllipseItem* CastToEllipseItem() = 0;

	//==============================================================================
	// Method Name: CastToPolygonItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcPolygonItem*
	/// 
	/// \return
	///     - #IMcPolygonItem*
	//==============================================================================
	virtual IMcPolygonItem* CastToPolygonItem() = 0;

	//==============================================================================
	// Method Name: CastToRectangleItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcRectangleItem*
	/// 
	/// \return
	///     - #IMcRectangleItem*
	//==============================================================================
	virtual IMcRectangleItem* CastToRectangleItem() = 0;

	//==============================================================================
	// Method Name: CastToLineItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcLineItem*
	/// 
	/// \return
	///     - #IMcLineItem*
	//==============================================================================
	virtual IMcLineItem* CastToLineItem() = 0;

	//==============================================================================
	// Method Name: CastToLineExpansionItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcLineExpansionItem*
	/// 
	/// \return
	///     - #IMcLineExpansionItem*
	//==============================================================================
	virtual IMcLineExpansionItem* CastToLineExpansionItem() = 0;

	//==============================================================================
	// Method Name: CastToMeshItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcMeshItem*
	/// 
	/// \return
	///     - #IMcMeshItem*
	//==============================================================================
	virtual IMcMeshItem* CastToMeshItem() = 0;

	//==============================================================================
	// Method Name: CastToPictureItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcPictureItem*
	/// 
	/// \return
	///     - #IMcPictureItem*
	//==============================================================================
	virtual IMcPictureItem* CastToPictureItem() = 0;

	//==============================================================================
	// Method Name: CastToTextItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcTextItem*
	/// 
	/// \return
	///     - #IMcTextItem*
	//==============================================================================
	virtual IMcTextItem* CastToTextItem() = 0;

	//==============================================================================
	// Method Name: CastToLineBasedItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcLineBasedItem*
	/// 
	/// \return
	///     - #IMcLineBasedItem*
	//==============================================================================
	virtual IMcLineBasedItem* CastToLineBasedItem() = 0;

	//==============================================================================
	// Method Name: CastToClosedShapeItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcClosedShapeItem*
	/// 
	/// \return
	///     - #IMcClosedShapeItem*
	//==============================================================================
	virtual IMcClosedShapeItem* CastToClosedShapeItem() = 0;

	//==============================================================================
	// Method Name: CastToLightBasedItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcLightBasedItem*
	/// 
	/// \return
	///     - #IMcLightBasedItem*
	//==============================================================================
	virtual IMcLightBasedItem* CastToLightBasedItem() = 0;

	//==============================================================================
	// Method Name: CastToLocationBasedLightItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcLocationBasedLightItem*
	/// 
	/// \return
	///     - #IMcLocationBasedLightItem*
	//==============================================================================
	virtual IMcLocationBasedLightItem* CastToLocationBasedLightItem() = 0;

	//==============================================================================
	// Method Name: CastToPointLightItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcPointLightItem*
	/// 
	/// \return
	///     - #IMcPointLightItem*
	//==============================================================================
	virtual IMcPointLightItem* CastToPointLightItem() = 0;

	//==============================================================================
	// Method Name: CastToSpotLightItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcSpotLightItem*
	/// 
	/// \return
	///     - #IMcSpotLightItem*
	//==============================================================================
	virtual IMcSpotLightItem* CastToSpotLightItem() = 0;

	//==============================================================================
	// Method Name: CastToDirectionalLightItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcDirectionalLightItem*
	/// 
	/// \return
	///     - #IMcDirectionalLightItem*
	//==============================================================================
	virtual IMcDirectionalLightItem* CastToDirectionalLightItem() = 0;

	//==============================================================================
	// Method Name: CastToParticleEffectItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcParticleEffectItem*
	/// 
	/// \return
	///     - #IMcParticleEffectItem*
	//==============================================================================
	virtual IMcParticleEffectItem* CastToParticleEffectItem() = 0;

	//==============================================================================
	// Method Name: CastToSoundItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcSoundItem*
	/// 
	/// \return
	///     - #IMcSoundItem*
	//==============================================================================
	virtual IMcSoundItem* CastToSoundItem() = 0;

	//==============================================================================
	// Method Name: CastToProjectorItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcProjectorItem*
	/// 
	/// \return
	///     - #IMcProjectorItem*
	//==============================================================================
	virtual IMcProjectorItem* CastToProjectorItem() = 0;

	//==============================================================================
	// Method Name: CastToProceduralGeometryItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcProceduralGeometryItem*
	/// 
	/// \return
	///     - #IMcProceduralGeometryItem*
	//==============================================================================
	virtual IMcProceduralGeometryItem* CastToProceduralGeometryItem() = 0;

	//==============================================================================
	// Method Name: CastToManualGeometryItem()
	//------------------------------------------------------------------------------
	/// Casts the #IMcObjectSchemeNode* To #IMcManualGeometryItem*
	/// 
	/// \return
	///     - #IMcManualGeometryItem*
	//==============================================================================
	virtual IMcManualGeometryItem* CastToManualGeometryItem() = 0;

	//@}

	/// \name Geometry Coordinate System
	//@{
	//==============================================================================
	// Method Name: GetGeometryCoordinateSystem()
	//------------------------------------------------------------------------------
	/// Retrieves the node's geometry coordinate system (see #EMcPointCoordSystem).
	///
	/// This is the coordinate system of the node's geometry:
	/// - Object location: the location's coordinate system set in IMcObjectScheme::Create() / IMcObjectScheme::AddObjectLocation().
	/// - Physical item: always #EPCS_WORLD.
	/// - Line/polygon item: the coordinate system of the points the item is based on.
	/// - Rectangle item: the rectangle's coordinate system set in IMcRectangleItem::Create().
	/// - Ellipse/arc item: the ellipse's coordinate system set in IMcEllipseItem::Create() / IMcArcItem::Create().
	/// - Arrow item: the arrow's coordinate system set in IMcArrowItem::Create().
	/// - Line expansion item: the line expansion's coordinate system set in set by IMcLineExpansionItem::Create().
	/// - Text item: the text's coordinate system set in set by IMcTextItem::Create().
	/// - Picture item: the picture's coordinate system set in IMcPictureItem::Create() .
	/// - Procedural geometry item: the procedural geometry's coordinate system set in IMcManualGeometryItem::Create().
	/// 
	/// \param[out]	peGeometryCoordinateSystem		The node's geometry coordinate system (see #EMcPointCoordSystem).
	/// \param[in]	pObject							The object (can be `NULL` only if attach point types of this node and of the nodes it depends on 
	///												are shared properties).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetGeometryCoordinateSystem(
		EMcPointCoordSystem *peGeometryCoordinateSystem,
		IMcObject *pObject = NULL) const = 0;

	//@}
	/// \name Object Scheme
	//@{

	//==============================================================================
	// Method Name: GetScheme()
	//------------------------------------------------------------------------------
	/// Retrieves the object scheme the node connected to.
	///
	/// \param [out] ppObjectScheme		The object's scheme (NULL means the node is not connected to an object scheme)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetScheme (IMcObjectScheme **ppObjectScheme) const = 0;

	//@}

	/// \name Parents and Children
	//@{

	//==============================================================================
	// Method Name: GetParents()
	//------------------------------------------------------------------------------
	/// Retrieves the node's direct parents (the nodes this node is connected to)
	/// 
	/// \param[out] papParents		Array the node's direct parents (the nodes this node is connected to).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetParents (
		CMcDataArray<IMcObjectSchemeNode*> *papParents) const = 0;

	//==============================================================================
	// Method Name: GetChildren()
	//------------------------------------------------------------------------------
	/// Retrieves the node's direct children (connected to the node).
	/// 
	/// \param[out] papChildren		Array of the node's direct children (the nodes connected to it).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetChildren (
		CMcDataArray<IMcObjectSchemeNode*> *papChildren) const = 0;

	//@}

	/// \name Calculated Coordinates, Bounding Box/Rectangle
	//@{

	//==============================================================================
	// Method Name: GetCoordinates()
	//------------------------------------------------------------------------------
	/// Retrieves calculated coordinates of all the points the node is based on in a given object currently displayed in a given viewport.
	///	
	/// The calculation is based on the following:
	/// - the object's location points converted to the viewport's grid coordinate system;
	/// - for object scheme item: the attach points for each parent node in the scheme defining which points are taken from the parent node;
	/// - for object scheme item: the transformations (offsets, rotations, rotation alignments);
	///
	/// The calculation does not consider the following:
	/// - the item's parameters according to the item's type (ellipse radiuses etc.) and the object's private properties overriding these parameters.
	///
	/// \see IMcSymbolicItem::GetAllCalculatedPoints()
	///
	/// \param[in]	pMapViewport		The map viewport.
	/// \param[in]	eCoordinateSystem	The coordinate system to convert to.
	/// \param[in]	pObject				The object.
	/// \param[out]	paCoordinates		Array of the calculated coordinates (empty array if there are no valid points or they cannot be converted 
	///									into the specified coordinate system).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCoordinates(
		IMcMapViewport *pMapViewport, 
		EMcPointCoordSystem eCoordinateSystem,
		IMcObject *pObject,
		CMcDataArray<SMcVector3D> *paCoordinates) const = 0;

	//==============================================================================
	// Method Name: GetWorldBoundingBox()
	//------------------------------------------------------------------------------
	/// Retrieves the world bounding box of the node in a given object currently displayed in a given viewport.
	///
	/// \param[in]	pMapViewport		The map viewport.
	/// \param[in]	pObject				The object.
	/// \param[out]	pBoundingBox		The resulting bounding box.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetWorldBoundingBox(
		IMcMapViewport *pMapViewport,
		IMcObject *pObject,
		SMcBox *pBoundingBox) /*const*/ = 0;

	//==============================================================================
	// Method Name: GetScreenBoundingRect()
	//------------------------------------------------------------------------------
	/// Retrieves the screen bounding rectangle of the node in the a object currently displayed in a given viewport.
	///
	/// \param[in]	pMapViewport		The map viewport.
	/// \param[in]	pObject				The object.
	/// \param[out]	pBoundingRect		The resulting bounding rectangle (`x` and `y` only are relevant).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetScreenBoundingRect(
		IMcMapViewport *pMapViewport,
		IMcObject *pObject,
		SMcBox *pBoundingRect) /*const*/ = 0;

	//@}

	/// \name ID, Name and User Data
	//@{

	//==============================================================================
	// Method Name: SetID()
	//------------------------------------------------------------------------------
	/// Sets the node's user-defined unique ID that allows retrieving the node by IMcObjectScheme::GetNodeByID().
	///
	/// \param[in]	uID		The node's ID to be set (specify ID not equal to #MC_EMPTY_ID to set ID,
	///						equal to #MC_EMPTY_ID to remove ID).
	/// \note
	/// If the node is connected to a scheme, the ID should be unique in the scheme, otherwise the function returns IMcErrors::ID_ALREADY_EXISTS
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetID(UINT uID) = 0;

	//==============================================================================
	// Method Name: GetID()
	//------------------------------------------------------------------------------
	/// Retrieves the node'e user-defined unique ID set by SetID().
	///        
	/// \param [out] puID	The node ID (or #MC_EMPTY_ID if the ID is not set).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetID(UINT *puID) const = 0;

	//==============================================================================
	// Method Name: SetName()
	//------------------------------------------------------------------------------
	/// Sets the node's user-defined unique name that allows retrieving the node by IMcObjectScheme::GetNodeByName().
	///
	/// \param[in]	strName		The node's name to be set (or NULL to remove the current name)
	///
	/// \note
	/// If the node is connected to a scheme, the name should be unique in the scheme, otherwise the function returns IMcErrors::ID_ALREADY_EXISTS
	/// 
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetName(PCSTR strName) = 0;

	//==============================================================================
	// Method Name: GetName()
	//------------------------------------------------------------------------------
	/// Retrieves the node'e user-defined unique name set by SetName().
	///        
	/// \param [out] pstrName	The node's name (or NULL if the name is not set)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetName(PCSTR *pstrName) const = 0;

	//==============================================================================
	// Method Name: SetUserData()
	//------------------------------------------------------------------------------
	/// Sets an optional user-defined data that can be retrieved later by GetUserData().
	///
	/// \param[in]	pUserData	An instance of a user-defined class implementing IMcUserData interface.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetUserData(IMcUserData *pUserData) = 0;

	//==============================================================================
	// Method Name: GetUserData()
	//------------------------------------------------------------------------------
	/// Retrieves an optional user-defined data set by SetUserData().
	///
	/// \param[out]	ppUserData	An instance of a user-defined class implementing IMcUserData interface.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetUserData(IMcUserData **ppUserData) const = 0;

	//@}

	/// \name Visibility Option
	//@{

	//==============================================================================
	// Method Name: SetVisibilityOption()
	//------------------------------------------------------------------------------
	/// Sets the node's visibility option as a shared or private property.
	///
	/// The node's visibility in a specific object in a specific viewport depends on its visibility option along with 
	/// its visibility conditional selector's result (only if the visibility option is IMcConditionalSelector::EAO_USE_SELECTOR - the default) and 
	/// on visibility options, visibility conditional selectors and collection visibilities of the node's object and overlay.
	/// 
	/// \param[in] eVisibility			The node's visibility option; the default is IMcConditionalSelector::EAO_USE_SELECTOR.
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
	virtual IMcErrors::ECode SetVisibilityOption(
		IMcConditionalSelector::EActionOptions eVisibility,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetVisibilityOption()
	//------------------------------------------------------------------------------
	/// Retrieves the node's visibility option property set by SetVisibilityOption().
	///
	/// The node's visibility in a specific object in a specific viewport depends on its visibility option along with 
	/// its visibility conditional selector's result (only if the visibility option is IMcConditionalSelector::EAO_USE_SELECTOR - the default) and 
	/// on visibility options, visibility conditional selectors and collection visibilities of the node's object and overlay.
	/// 
	/// \param[out]	peVisibility		The node's visibility option; the default is IMcConditionalSelector::EAO_USE_SELECTOR.
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
	virtual IMcErrors::ECode GetVisibilityOption(
		IMcConditionalSelector::EActionOptions *peVisibility,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: GetEffectiveVisibilityInViewport()
	//------------------------------------------------------------------------------
	/// Retrieves the node's current effective visibility in the specified viewport.
	///
	/// The result is irrespective to the viewport's current visible area, it depends on its visibility option along with 
	/// its visibility conditional selector's result (only if the visibility option is IMcConditionalSelector::EAO_USE_SELECTOR - the default) and 
	/// on visibility options, visibility conditional selectors and collection visibilities of the node's object and overlay.
	///
	/// \param[in]	pObject			The object.
	/// \param[in]	pMapViewport	The viewport to check a visibility in.
	/// \param[out]	pbVisible		Whether the node is currently visible in the viewport
	///								(irrespectively to the viewport's current visible area).
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetEffectiveVisibilityInViewport(IMcObject *pObject, 
		IMcMapViewport *pMapViewport, bool *pbVisible) const = 0;

	/// \name Transform Option
	//@{

	//==============================================================================
	// Method Name: SetTransformOption()
	//------------------------------------------------------------------------------
	/// Sets the node's transform option as a shared or private property.
	///
	/// The transform option (applicable only to symbolic items) controls whether the item's transforms defined in IMcSymbolicItem are performed.
	///
	/// \param[in] eTransform			The transform option; the default is IMcConditionalSelector::EAO_USE_SELECTOR.
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
	virtual IMcErrors::ECode SetTransformOption(
		IMcConditionalSelector::EActionOptions eTransform,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetTransformOption()
	//------------------------------------------------------------------------------
	/// Retrieves the transform option property set by SetTransformOption().
	///
	/// The transform option (applicable only to symbolic items) controls whether the item's transforms defined in IMcSymbolicItem are performed.
	///
	/// \param[out]	peTransform			The transform option; the default is IMcConditionalSelector::EAO_USE_SELECTOR.
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
	virtual IMcErrors::ECode GetTransformOption(
		IMcConditionalSelector::EActionOptions *peTransform,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Conditional Selector
	//@{

	//==============================================================================
	// Method Name: SetConditionalSelector()
	//------------------------------------------------------------------------------
	/// Sets the node's conditional selector (for the specified action from IMcConditionalSelector::EActionType) as a shared or private property 
	/// along with action-on-result parameter.
	///
	/// One selector for each action type can be set.
	///
	/// \param[in] eActionType			The type of an action controlled by the selector.
	/// \param[in] bActionOnResult		Defines which selector result performs the action (in other words: whether the selector serves as either *if* or *else* condition).
	///									The same value is used for all object states regardless of the value passed in \p uObjectStateToServe. 
	///									so if the function is called several times (even with different values of object states), 
	///									the last value of \p bActionOnResult always overwrites the previous ones.
	/// \param[in] pSelector			The conditional selector.
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
	virtual IMcErrors::ECode SetConditionalSelector(
		IMcConditionalSelector::EActionType eActionType, bool bActionOnResult, 
		IMcConditionalSelector *pSelector,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetConditionalSelector()
	//------------------------------------------------------------------------------
	/// Retrieves the conditional selector property (for the specified action from IMcConditionalSelector::EActionType) 
	/// along with action-on-result parameter set by SetConditionalSelector().
	///
	/// \param[in]	eActionType			The type of an action controlled by the selector.
	/// \param[out]	pbActionOnResult	Defines which selector result performs the action (in other words: whether the selector serves as either *if* or *else* condition).
	///									The same value is used for all object states regardless of the value passed in \p uObjectStateToServe. 
	///									So if the function is called with different values of object states, 
	///									the same \p pbActionOnResult value will be returned: the last value set by SetConditionalSelector().
	/// \param[out]	ppSelector			The conditional selector.
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
	virtual IMcErrors::ECode GetConditionalSelector(
		IMcConditionalSelector::EActionType eActionType, bool *pbActionOnResult,
		IMcConditionalSelector **ppSelector,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}
};