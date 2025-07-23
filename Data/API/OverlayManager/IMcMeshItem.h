#pragma once
//==================================================================================
/// \file IMcMeshItem.h
/// Interface for 3D mesh item
//==================================================================================

#include "McExports.h"
#include "McCommonTypes.h"
#include "OverlayManager/IMcPhysicalItem.h"

class IMcObject;
class IMcAnimationState;

//==================================================================================
// Interface Name: IMcMeshItem
//----------------------------------------------------------------------------------
///	Interface for 3D mesh item.
/// 
//==================================================================================
class IMcMeshItem : public virtual IMcPhysicalItem
{
protected:

	virtual ~IMcMeshItem() {};

public:

    enum
    {
        //==============================================================================
        /// Node unique ID for this interface
        //==============================================================================
        NODE_TYPE = 61
    };

	//==============================================================================
	// Enum Name: EBasePointAlignment
	//------------------------------------------------------------------------------
	/// Base Point Alignment
	//==============================================================================
	enum EBasePointAlignment 
	{
		EBPA_MESH_ZERO,						///< Mesh model original zero

		EBPA_MESH_ZERO_LOWERED,				///< Mesh model original zero lowered
											///< to the bounding box bottom

		EBPA_BOUNDING_BOX_CENTER ,			///< Mesh model bounding box center

		EBPA_BOUNDING_BOX_CENTER_LOWERED	///< Mesh model bounding box center
											///< lowered to the bounding box bottom 
	};

public:

    /// \name Create 
    //@{

    //==============================================================================
    // Method Name: Create(...)
    //------------------------------------------------------------------------------
    /// Create a mesh item from an X-File.
    ///
	/// The item will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Release() method is called,
	/// when its (or its parents) Disconnect() method is called,
	/// or when its object scheme is destroyed.
	///
    /// \param[out] ppItem							The newly created item
    /// \param[in]  pMesh							The mesh resource
	/// \param[in]  eBasePointAlignment				The mesh base point alignment of the mesh
	/// \param[in]	bParticipateInTerrainHeight	Whether should participate in terrain height queries
	/// \param[in]  bCastShadows					Whether or not to cast shadows
	/// \param[in]  bStatic							Whether or not the mesh is static
	/// \param[in]  bDisplayItemsAttachedToTerrain	Whether or not object scheme items attached to terrain 
	///												(belonging to any objects) should be displayed 
	///												on top of the mesh in 3D viewport
    /// \return
    ///     - Status result
    //==============================================================================
    static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcMeshItem **ppItem, 
		IMcMesh *pMesh, 
		EBasePointAlignment eBasePointAlignment = EBPA_MESH_ZERO,
		bool bParticipateInTerrainHeight = false,
		bool bCastShadows = true,
		bool bStatic = false,
		bool bDisplayItemsAttachedToTerrain = false);

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
		IMcMeshItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}

    /// \name Mesh
    //@{

	//==============================================================================
	// Method Name: SetMesh(...)
	//------------------------------------------------------------------------------
	/// Defines the mesh resource as a shared or private property.
	///
	/// \param[in] pMesh				The mesh resource.
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
	virtual IMcErrors::ECode SetMesh(
		IMcMesh *pMesh,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetMesh(...)
	//------------------------------------------------------------------------------
	/// Retrieves the mesh resource property defined by SetXXX().
	///
	/// \param[out] ppMesh				The mesh resource.
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
	virtual IMcErrors::ECode GetMesh(
		IMcMesh **ppMesh,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

    //@}

	/// \name Animation
	//@{

	//==============================================================================
	// Method Name: SetAnimation()
	//------------------------------------------------------------------------------
	/// Sets mesh's animation as a shared or private property.
	///
	/// \param[in]	Animation			The animation value (see #SMcAnimation for details).
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
	virtual IMcErrors::ECode SetAnimation(const SMcAnimation &Animation, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID) = 0;

	//==============================================================================
	// Method Name: GetAnimation()
	//------------------------------------------------------------------------------
	/// Retrieves mesh's animation property defined by SetXXX().
	///
	/// \param[out]	pAnimation			The animation value (see #SMcRotation for details).
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
	virtual IMcErrors::ECode GetAnimation(SMcAnimation *pAnimation, UINT *puPropertyID = NULL) const = 0;

	//==============================================================================
	// Method Name: GetAnimationStates()
	//------------------------------------------------------------------------------
	/// Retrieves mesh's animation states of a given object.
	///
	/// \param[in]  pObject				The object.
	/// \param[out]	papAnimationStates	The animation states.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAnimationStates(IMcObject *pObject, CMcDataArray<IMcAnimationState*> *papAnimationStates) const = 0;

	//@}

	/// \name Cast Shadows
	//@{

	//==============================================================================
	// Method Name: SetCastShadows(...)
	//------------------------------------------------------------------------------
	/// Defines whether or not to cast shadows as a shared or private property.
	///
	/// \param[in] bCastShadows			Whether or not to cast shadows.
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
	virtual IMcErrors::ECode SetCastShadows(
		bool bCastShadows,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetCastShadows(...)
	//------------------------------------------------------------------------------
	/// Retrieves the cast shadows property defined by SetXXX().
	///
	/// \param[out] pbCastShadows		Whether or not to cast shadows.
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
	virtual IMcErrors::ECode GetCastShadows(
		bool *pbCastShadows,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Sub-part Transforms
	//@{
	
	//==============================================================================
	// Method Name: SetSubPartOffset()
	//------------------------------------------------------------------------------
	/// Sets sub-part's offset as a shared or private property.
	///
	/// - Separate offsets can be set for different sub-parts identified by their attach 
	///   points.
	/// - The offset is relative to the mesh transform.
	///
	///
	/// \param[in]	uAttachPointID		The ID of the attach point (defined in IMcMesh) 
	///									used to offset the part
	/// \param[in]	Offset				The sub-part's offset value.
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
	virtual IMcErrors::ECode SetSubPartOffset(UINT uAttachPointID, const SMcFVector3D &Offset, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetSubPartOffset()
	//------------------------------------------------------------------------------
	/// Retrieves sub-part's offset property defined by SetXXX().
	///
	/// - Separate offsets can be retrieved for different sub-parts identified by their 
	///	attach points.
	/// - The offset retrieved is relative to the mesh transform.
	///
	/// \param[in]	uAttachPointID		The ID of the attach point (defined in IMcMesh) 
	///									used to rotate the part
	/// \param[out]	pOffset				The sub-part's offset value.
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
	virtual IMcErrors::ECode GetSubPartOffset(UINT uAttachPointID, SMcFVector3D *pOffset, 
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetSubPartRotation()
	//------------------------------------------------------------------------------
	/// Sets sub-part's rotation as a shared or private property.
	///
	/// - Separate rotations can be set for different sub-parts identified by their attach 
	///	  points.
	/// - The rotation can be either absolute or relative to the current orientation of 
	///   the sub-part according to \a Rotation.bRelativeToCurrent
	/// - The rotation can be either absolute or relative to the mesh orientation 
	///   according to \a Rotation.bRelativeToParent
	///
	/// \param[in]	uAttachPointID		The ID of the attach point (defined in IMcMesh) 
	///									used to rotate the part
	/// \param[in]	Rotation			The sub-part's rotation value (see #SMcRotation for details).
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
	virtual IMcErrors::ECode SetSubPartRotation(UINT uAttachPointID, const SMcRotation &Rotation, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID) = 0;

	//==============================================================================
	// Method Name: GetSubPartRotation()
	//------------------------------------------------------------------------------
	/// Retrieves sub-part's rotation property defined by SetXXX().
	///
	/// - Separate rotations can be retrieved for different sub-parts identified by their 
	///	  attach points.
	/// - The rotation retrieved is either absolute or relative to the previous orientation 
	///   of the part (see \a Rotation.bRelativeToCurrent)
	/// - The rotation retrieved is either absolute or relative to the mesh orientation 
	///   (see \a Rotation.bRelativeToParent)
	///
	/// \param[in]	uAttachPointID		The ID of the attach point (defined in IMcMesh) 
	///									used to rotate the part
	/// \param[out]	pRotation			The sub-part's rotation value (see #SMcRotation for details).
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
	virtual IMcErrors::ECode GetSubPartRotation(UINT uAttachPointID, SMcRotation *pRotation, 
		UINT *puPropertyID = NULL) const = 0;

	//==============================================================================
	// Method Name: GetSubPartCurrRotation()
	//------------------------------------------------------------------------------
	/// Retrieves sub-part's current rotation value for the specific object.
	///
	/// - Separate rotations can be retrieved for different sub-parts identified by their 
	///	  attach points.
	/// - The retrieved value is the current rotation, so \a Rotation.bRelativeToCurrent 
	///   will always be false
	/// - The rotation retrieved is either absolute or relative to the mesh orientation 
	///   (see \a Rotation.bRelativeToParent)
	///
	/// \param[in]	pMapViewport			The map viewport
	/// \param[in]	pObject					The object to retrieve rotation for.
	/// \param[in]	uAttachPointID			The ID of the attach point (defined in IMcMesh) 
	///										used to rotate the part.
	/// \param[in]	bRelativeToMeshRotation	Whether or not the rotation is relative to mesh.
	/// \param[out]	pRotation				The sub-part's current rotation value
	///										(see #SMcRotation for details).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSubPartCurrRotation(IMcMapViewport *pMapViewport, IMcObject *pObject, 
		UINT uAttachPointID, bool bRelativeToMeshRotation, SMcRotation *pRotation) const = 0;

	//==============================================================================
	// Method Name: GetSubPartCurrRotation()
	//------------------------------------------------------------------------------
	/// Retrieves sub-part's current rotation value for the specific object 
	/// (a property ID is specified instead of an item and attach point ID).
	///
	/// - The function can be used in case of a private rotation property having an ID when
	///	  there is no other property in the object scheme using this ID.
	/// - Separate rotations can be retrieved for different sub-parts identified by 
	///   different property IDs.
	/// - The retrieved value is the current rotation, so \a Rotation.bRelativeToCurrent 
	///   will always be false.
	/// - The rotation retrieved is either absolute or relative to the mesh orientation 
	///   (see \a Rotation.bRelativeToParent).
	///
	/// \param[in]	pMapViewport			The map viewport
	/// \param[in]	uPropertyID				The ID of the appropriate rotation property
	/// \param[in]	pObject					The object to retrieve rotation for.
	/// \param[in]	bRelativeToMeshRotation	Whether or not the rotation is relative to mesh.
	/// \param[out]	pRotation				The sub-part's current rotation value
	///										(see #SMcRotation for details).
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API GetSubPartCurrRotation(IMcMapViewport *pMapViewport,
		UINT uPropertyID, IMcObject *pObject, bool bRelativeToMeshRotation, SMcRotation *pRotation);

	//==============================================================================
	// Method Name: SetSubPartInheritsParentRotation(...)
	//------------------------------------------------------------------------------
	/// Sets sub-part's InheritsParentRotation as a shared or private property.
	///
	/// - Separate InheritsParentRotations can be set for different sub-parts identified by their attach 
	///	  points.
	///
	/// \param[in]	uAttachPointID			The ID of the attach point (defined in IMcMesh) 
	///										used to rotate the part
	/// \param[in]	bInheritsParentRotation	Whether or not the sub-part inherits the parent rotation.
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
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSubPartInheritsParentRotation(UINT uAttachPointID, bool bInheritsParentRotation, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID) = 0;

	//==============================================================================
	// Method Name: GetSubPartInheritsParentRotation(...)
	//------------------------------------------------------------------------------
	/// Retrieves sub-part's InheritsParentRotation property defined by SetXXX().
	///
	/// - Separate InheritsParentRotations can be retrieved for different sub-parts identified by their 
	///	attach points.
	///
	/// \param[in]	uAttachPointID			The ID of the attach point (defined in IMcMesh) 
	///										used to rotate the part
	/// \param[out]	pbInheritsParentRotation Whether or not the sub-part inherits the parent rotation.
	///										The default is true.
	///										The parameter's meaning depends on \a puPropertyID (see SetXXX()).
	/// \param[out] puPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.///									IMcProperty::EPPI_SHARED_PROPERTY_ID.
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
	virtual IMcErrors::ECode GetSubPartInheritsParentRotation(UINT uAttachPointID, bool *pbInheritsParentRotation, 
		UINT *puPropertyID = NULL) const = 0;

	//@}

	/// \name Mesh Texture Scroll Speed
	//@{

	//==============================================================================
	// Method Name: SetTextureScrollSpeed()
	//------------------------------------------------------------------------------
	/// Sets mesh texture scroll speed as a shared or private property.
	///
	/// \param[in]	uMeshTextureID		The ID of the mesh texture (defined in IMcMesh)
	/// \param[in]	ScrollSpeed			The scroll speed.
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
	virtual IMcErrors::ECode SetTextureScrollSpeed(UINT uMeshTextureID, const SMcFVector2D &ScrollSpeed, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetTextureScrollSpeed()
	//------------------------------------------------------------------------------
	/// Retrieves mesh texture scroll speed property defined by SetXXX().
	///
	/// \param[in]	uMeshTextureID		The ID of the mesh texture (defined in IMcMesh)
	/// \param[out]	pScrollSpeed		The scroll speed.
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
	virtual IMcErrors::ECode GetTextureScrollSpeed(UINT uMeshTextureID, SMcFVector2D *pScrollSpeed, 
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Mesh Base Point Alignment
	//@{

	//==============================================================================
	// Method Name: SetBasePointAlignment(...)
	//------------------------------------------------------------------------------
	/// Defines the mesh base point alignment as a shared or private property.
	///
	/// \param[in] eBasePointAlignment	The mesh base point alignment.
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
	virtual IMcErrors::ECode SetBasePointAlignment(
		EBasePointAlignment eBasePointAlignment,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetBasePointAlignment(...)
	//------------------------------------------------------------------------------
	/// Retrieves the mesh base point alignment property defined by SetXXX().
	///
	/// \param[out] peBasePointAlignment	The mesh base point alignment.
	///										The parameter's meaning depends on \a puPropertyID
	///										and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID			The private property ID or one of the following special values:
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
	virtual IMcErrors::ECode GetBasePointAlignment(
		EBasePointAlignment *peBasePointAlignment,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Read-only Options
	//@{
	//==============================================================================
	// Method Name: GetParticipationInTerrainHeight()
	//------------------------------------------------------------------------------
	/// Retrieves the option of participation in terrain height queries
	///
	/// \param[out]	pbParticipatesInTerrainHeight	Whether participates in terrain height queries
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetParticipationInTerrainHeight(bool *pbParticipatesInTerrainHeight) const = 0;

	//==============================================================================
	// Method Name: GetStatic()
	//------------------------------------------------------------------------------
	/// Retrieves whether or not the mesh is static
	///
	/// \param[out]	pbStatic	Whether or not the mesh is static
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetStatic(bool *pbStatic) const = 0;

	//==============================================================================
	// Method Name: GetDisplayingItemsAttachedToTerrain()
	//------------------------------------------------------------------------------
	/// Retrieves the option of displaying object scheme items attached to terrain on top of the mesh
	///
	/// \param[out]	pbDisplaysItemsAttachedToTerrain	Whether or not object scheme items attached to terrain 
	///													(belonging to any objects) are displayed 
	///													on top of the mesh in 3D viewport
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetDisplayingItemsAttachedToTerrain(bool *pbDisplaysItemsAttachedToTerrain) const = 0;
	//@}
};
