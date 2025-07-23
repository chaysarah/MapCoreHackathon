#pragma once
//==================================================================================
/// \file IMcPhysicalItem.h
/// Interface for object scheme node of type Physical Item
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "OverlayManager/IMcObjectSchemeItem.h"

//==================================================================================
// Interface Name: IMcPhysicalItem
//----------------------------------------------------------------------------------
/// Interface for object scheme node of type Physical Item. 
///
//==================================================================================
class IMcPhysicalItem : public virtual IMcObjectSchemeItem
{
protected:

	virtual ~IMcPhysicalItem() {}

public:

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
		IMcPhysicalItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}

	/// \name Connect
	//@{

	//==============================================================================
	// Method Name: Connect(...)
	//------------------------------------------------------------------------------
	/// Connects an item to the specified parent.
	///
	/// If the item node is already connected, it will be disconnected first.
	///
	/// \param[in] pParentNode		The parent node or NULL
	///								(NULL means the first location)
	/// \param[out]	peErrorStatus	The error status (if not NULL, the return value will always be IMcErrors::SUCCESS)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Connect(IMcObjectSchemeNode *pParentNode, IMcErrors::ECode *peErrorStatus = NULL) = 0;

	//@}

	/// \name Attach Points
	//@{

	//==============================================================================
	// Method Name: SetAttachPoint()
	//------------------------------------------------------------------------------
	/// Sets the item's attach point as a shared or private property.
	///
	/// Allows to define which point and orientation is taken from the parent node.
	///
	/// \param[in]	uAttachPoint		The attach point taken from the parent node: 
	///									- if the parent is mesh item: attach point ID 
	///									  defined in IMcMesh or #MC_EMPTY_ID to use mesh itself;
	///									- if the parent is object location: index of its point.
	///									The default is #MC_EMPTY_ID. 
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
	virtual IMcErrors::ECode SetAttachPoint(UINT uAttachPoint, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetAttachPoint()
	//------------------------------------------------------------------------------
	/// Retrieves the item's attach point property defined by SetXXX().
	///
	/// \param[out]	puAttachPoint		The attach point taken from the parent node: 
	///									- if the parent is mesh item: attach point ID 
	///									  defined in IMcMesh or #MC_EMPTY_ID to use mesh itself;
	///									- if the parent is object location: index of its point.
	///									The default is #MC_EMPTY_ID. 
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
	virtual IMcErrors::ECode GetAttachPoint(UINT *puAttachPoint,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Transforms (applied according to the order of functions below)
	//@{

	//==============================================================================
	// Method Name: SetOffset()
	//------------------------------------------------------------------------------
	/// Sets offset transform as a shared or private property.
	///
	/// The offset is relative to the parent transform.
	///
	/// \param[in]	Offset				The offset value.
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
	/// Retrieves offset transform property defined by SetXXX().
	///
	/// \param[out]	pOffset				The offset value.
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
	// Method Name: SetRotation()
	//------------------------------------------------------------------------------
	/// Sets item's rotation as a shared or private property.
	///
	/// - The rotation can be either absolute or relative to the current orientation of 
	///   the item according to \a Rotation.bRelativeToCurrent
	/// - The rotation set can be either absolute or relative to the parent orientation 
	///   according to \a Rotation.bRelativeToParent
	///
	/// \param[in]	Rotation			The rotation value (see #SMcRotation for details).
	///									The parameter's meaning depends on \a uPropertyID (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
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
	virtual IMcErrors::ECode SetRotation(const SMcRotation &Rotation, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID) = 0;

	//==============================================================================
	// Method Name: GetRotation()
	//------------------------------------------------------------------------------
	/// Retrieves item's rotation property defined by SetXXX().
	///
	/// - The rotation retrieved is either absolute or relative to the previous orientation 
	///   of the item (see \a Rotation.bRelativeToCurrent)
	/// - The rotation retrieved is either absolute or relative to the parent orientation 
	///   (see \a Rotation.bRelativeToParent)
	///
	/// \param[out]	pRotation			The rotation value (see #SMcRotation for details).
	///									The parameter's meaning depends on \a puPropertyID (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
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
	virtual IMcErrors::ECode GetRotation(SMcRotation *pRotation, UINT *puPropertyID = NULL) const = 0;

	//==============================================================================
	// Method Name: GetCurrRotation()
	//------------------------------------------------------------------------------
	/// Retrieves item's current rotation value for the specific object.
	///
	/// - The retrieved value is the current rotation, so \a Rotation.bRelativeToCurrent 
	///   will always be false
	/// - The rotation retrieved is either absolute or relative to the parent orientation 
	///   (see \a Rotation.bRelativeToParent)
	///
	/// \param[in]	pMapViewport				The map viewport
	/// \param[in]	pObject						The object to retrieve rotation for.
	/// \param[in]	bRelativeToParentRotation	Whether or not the rotation is relative to parent.
	/// \param[out]	pRotation					The rotation value (see #SMcRotation for details).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCurrRotation(IMcMapViewport *pMapViewport, IMcObject *pObject, 
		bool bRelativeToParentRotation, SMcRotation *pRotation) const = 0;

	//==============================================================================
	// Method Name: GetCurrRotation()
	//------------------------------------------------------------------------------
	/// Retrieves item's current rotation value for the specific object 
	/// (a property ID is specified instead of an item).
	///
	/// - The function can be used in case of a private rotation property having an ID when
	///	  there is no other property in the object scheme using this ID.
	/// - The retrieved value is the current rotation, so \a Rotation.bRelativeToCurrent 
	///   will always be false.
	/// - The rotation retrieved is either absolute or relative to the parent orientation 
	///   (see \a Rotation.bRelativeToParent).
	///
	/// \param[in]	pMapViewport				The map viewport
	/// \param[in]	uPropertyID					The ID of the appropriate rotation property.
	/// \param[in]	pObject						The object to retrieve rotation for.
	/// \param[in]	bRelativeToParentRotation	Whether or not the rotation is relative to parent.
	/// \param[out]	pRotation					The rotation value (see #SMcRotation for details).
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API GetCurrRotation(IMcMapViewport *pMapViewport, 
		UINT uPropertyID, IMcObject *pObject, bool bRelativeToParentRotation, SMcRotation *pRotation);

	//==============================================================================
	// Method Name: SetInheritsParentRotation(...)
	//------------------------------------------------------------------------------
	/// Sets item's InheritsParentRotation as a shared or private property.
	///
	/// \param[in] bInheritsParentRotation	Whether or not the item inherits its parent rotation.
	///										The default is true.
	///										The parameter's meaning depends on \a uPropertyID (see note below).
	/// \param[in] uPropertyID				The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetInheritsParentRotation(bool bInheritsParentRotation, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID) = 0;

	//==============================================================================
	// Method Name: GetInheritsParentRotation(...)
	//------------------------------------------------------------------------------
	/// Retrieves item's InheritsParentRotation property defined by SetXXX().
	///
	/// \param[out] pbInheritsParentRotation	Whether or not the item inherits its parent rotation.
	///											The default is true.
	///											The parameter's meaning depends on \a puPropertyID (see SetXXX()).
	/// \param[out] puPropertyID				The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
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
	virtual IMcErrors::ECode GetInheritsParentRotation(bool *pbInheritsParentRotation, UINT *puPropertyID = NULL) const = 0;

	//==============================================================================
	// Method Name: SetScale(...)
	//------------------------------------------------------------------------------
	/// Sets item's Scale as a shared or private property.
	///
	/// \param[in] Scale				The scale value.
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
	virtual IMcErrors::ECode SetScale(const SMcFVector3D& Scale,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID, 
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetScale(...)
	//------------------------------------------------------------------------------
	/// Retrieves item's Scale property defined by SetXXX().
	///
	/// \param[out] pScale				The scale value.
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
	virtual IMcErrors::ECode GetScale(SMcFVector3D *pScale,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Parallel Terrain Attachment
	//@{

	//==============================================================================
	// Method Name: SetParallelToTerrain(...)
	//------------------------------------------------------------------------------
	/// Defines the parallel-to-terrain flag as a shared or private property.
	///
	/// \param[in] bParallelToTerrain	Whether the default orientation of the mesh is 
	///									parallel to the terrain. In this case, the item's rotation 
	///									is relative to the parallel-to-terrain orientation.
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
	virtual IMcErrors::ECode SetParallelToTerrain(
		bool bParallelToTerrain,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetParallelToTerrain(...)
	//------------------------------------------------------------------------------
	/// Retrieves the parallel-to-terrain flag property defined by SetXXX().
	///
	/// \param[out] pbParallelToTerrain	Whether the default orientation of the mesh is 
	///									parallel to the terrain. In this case, the item's rotation 
	///									is relative to the parallel-to-terrain orientation.
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
	virtual IMcErrors::ECode GetParallelToTerrain(
		bool *pbParallelToTerrain,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Effects
	//@{

	virtual IMcErrors::ECode SetColorModulateEffect(
		IMcObject *pObject, SMcFColor Color, float fFadeTimeMS) = 0;

	virtual IMcErrors::ECode GetColorModulateEffect(
		IMcObject *pObject, bool *pbEnabled, SMcFColor *pColor, float *pfFadeTimeMS) const = 0;

	virtual IMcErrors::ECode RemoveColorModulateEffect(IMcObject *pObject) = 0;

	virtual IMcErrors::ECode SetWireFrameEffect(
		IMcObject *pObject, SMcFColor Color, float fFadeTimeMS, bool bWireFrameOnly) = 0;

	virtual IMcErrors::ECode GetWireFrameEffect(
		IMcObject *pObject, bool *pbEnabled, SMcFColor *pColor, float *pfFadeTimeMS, 
		bool *pbWireFrameOnly) const = 0;

	virtual IMcErrors::ECode RemoveWireFrameEffect(IMcObject *pObject) = 0;
	//@}
};

//==================================================================================
// Interface Name: IMcEmptyPhysicalItem
//----------------------------------------------------------------------------------
///	Interface for empty physical item.
///
/// The item that has no visual presentation, but attach points and transform data can be defined.
/// The item can be used as a transform-only node, parent for other physical or symbolic items.
//==================================================================================
class IMcEmptyPhysicalItem : public virtual IMcPhysicalItem
{
protected:

	virtual ~IMcEmptyPhysicalItem() {};

public:

	enum
	{
		//==============================================================================
		/// Node unique ID for this interface
		//==============================================================================
		NODE_TYPE = 51
	};

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Create an empty physical item.
	///
	/// The item will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Release() method is called,
	/// when its (or its parents) Disconnect() method is called,
	/// or when its object scheme is destroyed.
	///
	/// \param[out] ppItem	The newly created item
	///
	/// \return
	///     - Status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcEmptyPhysicalItem **ppItem);

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
		IMcEmptyPhysicalItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}

};
