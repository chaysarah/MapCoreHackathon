#pragma once
//==================================================================================
/// \file IMcProjectorItem.h
/// Interface for projector item
//==================================================================================

#include "McExports.h"
#include "OverlayManager/IMcPhysicalItem.h"

//==================================================================================
// Interface Name: IMcProjectorItem
//----------------------------------------------------------------------------------
///	Interface for projector item.
/// 
//==================================================================================
class IMcProjectorItem : public virtual IMcPhysicalItem
{
protected:

	virtual ~IMcProjectorItem() {};

public:

	enum
	{
		//==============================================================================
		/// Node unique ID for this interface
		//==============================================================================
		NODE_TYPE = 64
	};

	//==============================================================================
	// Enum Name: ETargetTypesFlags
	//------------------------------------------------------------------------------
	/// Target Types bits
	//==============================================================================
	enum ETargetTypesFlags
	{
		ETTF_NONE				= 0x0000,	///< none
		ETTF_DTM				= 0x0001,	///< project onto terrain geometry (DTM)
		ETTF_STATIC_OBJECTS		= 0x0002,	///< project onto static objects layer
		ETTF_MESHES				= 0x0004,	///< project onto mesh items
		ETTF_TERRAIN_OBJECTS	= 0x0008,	///< project onto terrain objects (items attached to terrain and 
											///<  lite-vector lines and polygons)

		ETTF_UNBLOCKED_ONLY		= 0x0100	///< project onto unblocked areas only (exclude areas occluded from 
											///<  projector's point of view)
	};

public:

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Create a projector item.
	///
	/// The item will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Release() method is called,
	/// when its (or its parents) Disconnect() method is called,
	/// or when its object scheme is destroyed.
	///
	/// \param[out] ppItem								The newly created item
    /// \param[in] pDefaultTexture						The default projected texture resource
	/// \param[in] fDefaultHalfFieldOfViewHorizAngle	The default field of view (as a half of horizontal angle in degrees).
	///													The value must be between 0 and 90 inclusively
	/// \param[in] fDefaultAspectRatio					The default aspect ratio (width / height).
	///													The value must be greater than zero.
	/// \param[in] uDefaultTargetTypesBitField			The default targets to project onto (based on #ETargetTypesFlags)
	/// \param[in] bUseVideoTextureMetadata				
	///
	/// \return
	///     - Status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcProjectorItem **ppItem,
		IMcTexture *pDefaultTexture,
		float fDefaultHalfFieldOfViewHorizAngle,
		float fDefaultAspectRatio,
		UINT uDefaultTargetTypesBitField = ETTF_DTM | ETTF_STATIC_OBJECTS | ETTF_TERRAIN_OBJECTS,
		bool bUseVideoTextureMetadata = false);

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
		IMcProjectorItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}

    /// \name Texture
    //@{

    //==============================================================================
    // Method Name: SetTexture(...)
    //------------------------------------------------------------------------------
    /// Defines the projected texture resource as a shared or private property.
    ///
    /// \param[in] pTexture				The projected texture resource.
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
    virtual IMcErrors::ECode SetTexture(
		IMcTexture *pTexture,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetTexture(...)
    //------------------------------------------------------------------------------
    /// Retrieves the projected texture resource property defined by SetXXX().
    ///
    /// \param[out] ppTexture	The projected texture resource.
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
    virtual IMcErrors::ECode GetTexture(
		IMcTexture **ppTexture,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;


	//==============================================================================
	// Method Name: IsUsingTextureMetadata(...)
	//------------------------------------------------------------------------------
	/// Retrieves whether or not to use video texture metadata.
	///
	/// \param[out] pbIsUsingTextureMetadata	Whether or not video texture metadata is used
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode IsUsingTextureMetadata(bool *pbIsUsingTextureMetadata) const = 0;

    //@}

	/// \name Field Of View
	//@{

    //==============================================================================
    // Method Name: SetFieldOfView(...)
    //------------------------------------------------------------------------------
    /// Defines the projector field-of-view angle as a shared or private property.
	/// (As a half of horizontal angle in degrees).
    ///
	/// \param[in] fHalfFieldOfViewHorizAngle	The field of view (as a half of horizontal angle in degrees).
	///											The value must be between 0 and 90 inclusively.
	///											The parameter's meaning depends on \a uPropertyID
	///											and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID					The private property ID or one of the following special values:
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
    virtual IMcErrors::ECode SetFieldOfView(
		float fHalfFieldOfViewHorizAngle,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetFieldOfView(...)
    //------------------------------------------------------------------------------
    /// Retrieves the projector field-of-view angle property defined by SetXXX().
	/// (As a half of horizontal angle in degrees).
    ///
	/// \param[out] pfHalfFieldOfViewHorizAngle	The field of view (as a half of horizontal angle in degrees).
	///											The value must be between 0 and 90 inclusively.
	///											The parameter's meaning depends on \a puPropertyID
	///											and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID				The private property ID or one of the following special values:
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
    virtual IMcErrors::ECode GetFieldOfView(
		float *pfHalfFieldOfViewHorizAngle,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Aspect Ratio
	//@{

	//==============================================================================
	// Method Name: SetAspetctRatio(...)
	//------------------------------------------------------------------------------
	/// Sets the projector aspect ratio (width / height) as a shared or private property.
	///
	/// \param[in] fAspectRatio			The projector aspect ratio (width / height).
	///									The value must be greater than zero.
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
	virtual IMcErrors::ECode SetAspectRatio(
		float fAspectRatio,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetAspectRatio(...)
	//------------------------------------------------------------------------------
	/// Retrieves the projector aspect ratio property (width / height) defined by SetXXX().
	///
	/// \param[out] pAspectRatio		The projector aspect ratio (width / height).
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
	virtual IMcErrors::ECode GetAspectRatio(
		float *pAspectRatio,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Target Types
	//@{

    //==============================================================================
    // Method Name: SetTargetTypes(...)
    //------------------------------------------------------------------------------
    /// Sets the bit field of target types to project to, as a shared or private property.
    ///
	/// \param[in] uTargetTypesBitField		The bit field of target types to project onto (based on #ETargetTypesFlags).
	///										The parameter's meaning depends on \a uPropertyID
	///										and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID				The private property ID or one of the following special values:
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
    virtual IMcErrors::ECode SetTargetTypes(
		UINT uTargetTypesBitField,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetTargetTypes(...)
    //------------------------------------------------------------------------------
    /// Retrieves the target types property defined by SetXXX().
    ///
	/// \param[out]	puTargetTypesBitField	The bit field of target types to project onto (based on #ETargetTypesFlags).
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
    virtual IMcErrors::ECode GetTargetTypes(
		UINT *puTargetTypesBitField,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Projection Borders
	//@{

	//==============================================================================
	// Method Name: SetProjectionBorders(...)
	//------------------------------------------------------------------------------
	/// Sets the borders of the projected picture relative to the projector's frame.
	///
	/// Allows to define a projector with non-standard picture borders.
	///
	/// \param[in]	fLeft		The left border (the default is -1)
	/// \param[in]	fTop		The top border (the default is -1)
	/// \param[in]	fRight		The right border (the default is 1)
	/// \param[in]	fBottom		The bottom border (the default is 1)
	///
	/// \note
	///		Borders are in the whole projector frame's half-size units when the frame center is (0, 0),
	///		its top-left corner is (-1, -1), its bottom-right corner is (1, 1).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetProjectionBorders(float fLeft, float fTop, 
												  float fRight, float fBottom) = 0;

	//==============================================================================
	// Method Name: GetProjectionBorders(...)
	//------------------------------------------------------------------------------
	/// Retrieves the borders of the projected picture relative to the projector's frame 
	/// set by SetProjectionBorders().
	///
	/// \param[out]	pfLeft		The left border (the default is -1)
	/// \param[out]	pfTop		The top border (the default is -1)
	/// \param[out]	pfRight		The right border (the default is 1)
	/// \param[out]	pfBottom	The bottom border (the default is 1)
	///
	/// \note
	///		Borders are in the whole projector frame's half-size units when the frame center is (0, 0),
	///		its top-left corner is (-1, -1), its bottom-right corner is (1, 1).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetProjectionBorders(float *pfLeft, float *pfTop, 
												  float *pfRight, float *pfBottom) const = 0;
	//@}
};
