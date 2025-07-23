#pragma once
//==================================================================================
/// \file IMcLightBasedItem.h
/// Base interface for light based item (for 3D only)
//==================================================================================

#include "McExports.h"
#include "OverlayManager/IMcPhysicalItem.h"

//==================================================================================
// Interface Name: IMcLightBasedItem
//----------------------------------------------------------------------------------
///	Base interface for light based item (for 3D only).
/// 
//==================================================================================
class IMcLightBasedItem : public virtual IMcPhysicalItem
{
protected:

	virtual ~IMcLightBasedItem () {};

public:
	
	/// \name Diffuse Color
	//@{

    //==============================================================================
    // Method Name: SetDiffuseColor(...)
    //------------------------------------------------------------------------------
    /// Defines the diffuse color as a shared or private property.
    ///
    /// \param[in] DiffuseColor			The diffuse color emitted by the light.
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
    virtual IMcErrors::ECode SetDiffuseColor(
		const SMcFColor &DiffuseColor,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetDiffuseColor(...)
    //------------------------------------------------------------------------------
    /// Retrieves the diffuse color property defined by SetXXX().
    ///
    /// \param[out] pDiffuseColor		The diffuse color emitted by the light.
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
	virtual IMcErrors::ECode GetDiffuseColor(
		SMcFColor *pDiffuseColor,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Specular color
	//@{

	//==============================================================================
	// Method Name: SetSpecularColor(...)
	//------------------------------------------------------------------------------
	/// Defines the specular color as a shared or private property.
	///
	/// \param[in] SpecularColor		The specular color emitted by the light.
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
	virtual IMcErrors::ECode SetSpecularColor(
		const SMcFColor &SpecularColor,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetSpecularColor(...)
	//------------------------------------------------------------------------------
	/// Retrieves the specular color property defined by SetXXX().
	///
	/// \param[out] pSpecularColor		The specular color emitted by the light.
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
	virtual IMcErrors::ECode GetSpecularColor(
		SMcFColor *pSpecularColor,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}
};

//==================================================================================
// Interface Name: IMcLocationBasedLightItem
//----------------------------------------------------------------------------------
///	Base interface for location based light items.
///
//==================================================================================
class IMcLocationBasedLightItem : public virtual IMcLightBasedItem
{
protected:

	virtual ~IMcLocationBasedLightItem() {};

public:

	/// \name Attenuation
	//@{
	//==============================================================================
	// Method Name: SetAttenuation(...)
	//------------------------------------------------------------------------------
	/// Defines the attenuation as a shared or private property.
	///
	/// \param[in] Attenuation			Specifying how the light intensity changes over distance.
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
	virtual IMcErrors::ECode SetAttenuation(
		const SMcAttenuation &Attenuation,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetAttenuation(...)
	//------------------------------------------------------------------------------
	/// Retrieves the attenuation property defined by SetXXX().
	///
	/// \param[out] pAttenuation		Specifying how the light intensity changes over distance.
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
	virtual IMcErrors::ECode GetAttenuation(
		SMcAttenuation *pAttenuation,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}
};


//==================================================================================
// Interface Name: IMcPointLightItem
//----------------------------------------------------------------------------------
///	Interface for point light items.
/// 
//==================================================================================
class IMcPointLightItem : public virtual IMcLocationBasedLightItem
{
protected:

	virtual ~IMcPointLightItem() {};

public:

	enum
	{
		//==============================================================================
		/// Node unique ID for this interface
		//==============================================================================
		NODE_TYPE = 57
	};

public:

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a point light item
	///
	/// The item will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Release() method is called,
	/// when its (or its parents) Disconnect() method is called,
	/// or when its object scheme is destroyed.
	///
	/// \param[out] ppItem				The newly created item
	/// \param[in] DefaultDiffusColor	The default diffuse color emitted by the light
	/// \param[in] DefaultSpecularColor	The default specular color emitted by the light
	/// \param[in] DefaultAttenuation	The default attenuation -
	///									Specifies how the light intensity changes over distance
	///
	/// \return
	///     - Status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcPointLightItem **ppItem,
		const SMcFColor &DefaultDiffusColor = fcWhiteOpaque,
		const SMcFColor &DefaultSpecularColor = fcBlackTransparent,
		const SMcAttenuation &DefaultAttenuation = aLinearAttenuation);

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
		IMcPointLightItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}
};
//==================================================================================
// Interface Name: IMcSpotLightItem
//----------------------------------------------------------------------------------
///	Interface for spot light items.
/// 
//==================================================================================
class IMcSpotLightItem : public virtual IMcLocationBasedLightItem
{
protected:

	virtual ~IMcSpotLightItem() {};

public:

	enum
	{
		//==============================================================================
		/// Node unique ID for this interface
		//==============================================================================
		NODE_TYPE = 58
	};

public:

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a spot light item.
	///
	/// The item will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Release() method is called,
	/// when its (or its parents) Disconnect() method is called,
	/// or when its object scheme is destroyed.
	///
	/// \param[out] ppItem					The newly created item
	/// \param[in] DefaultDiffusColor		The default diffuse color emitted by the light
	/// \param[in] DefaultSpecularColor		The default specular color emitted by the light
	/// \param[in] DefaultAttenuation		The default attenuation -
	///										Specifies how the light intensity changes over distance
	/// \param[in] DefaultDirection			The default direction that the light is pointing. 
	///										The vector need not be normalized, but it should have a nonzero length.
	/// \param[in] fDefaultHalfOuterAngle	The default half outer angle (in degrees), defining the outer edge of the spotlight's 
	///										outer cone. Points outside this cone are not lit by 
	///										the spotlight. The value must be between 0 and 90. 
	/// \param[in] fDefaultHalfInnerAngle	The default half inner angle (in degrees) of a spotlight's inner cone — the fully illuminated 
	///										spotlight cone. The value must be from 0 through fDefaultOuterAngle.
	///
	/// \return
	///     - Status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcSpotLightItem **ppItem,
		const SMcFColor &DefaultDiffusColor = fcWhiteOpaque,
		const SMcFColor &DefaultSpecularColor = fcBlackTransparent,
		const SMcAttenuation &DefaultAttenuation = aLinearAttenuation,
		const SMcFVector3D &DefaultDirection = SMcFVector3D(0.0, 0.0, -1.0),
		float fDefaultHalfOuterAngle = 22.5,
		float fDefaultHalfInnerAngle = 2.5);

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
		IMcSpotLightItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}

	/// \name Direction
	//@{

	//==============================================================================
	// Method Name: SetDirection(...)
	//------------------------------------------------------------------------------
	/// Defines the direction as a shared or private property.
	///
	/// \param[in] Direction			The direction that the light is pointing.
	///									The vector need not be normalized, 
	///									but it should have a nonzero length.
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
	virtual IMcErrors::ECode SetDirection(
		const SMcFVector3D &Direction,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetDirection(...)
	//------------------------------------------------------------------------------
	/// Retrieves the direction property defined by SetXXX().
	///
	/// \param[out] pDirection			The direction that the light is pointing. 
	///									The vector need not be normalized, 
	///									but it should have a nonzero length.
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
	virtual IMcErrors::ECode GetDirection(
		SMcFVector3D *pDirection,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Outer & Inner Angles
	//@{

    //==============================================================================
    // Method Name: SetHalfOuterAngle(...)
    //------------------------------------------------------------------------------
    /// Defines the half outer angle as a shared or private property.
    ///
	/// \param[in] fHalfOuterAngle		The half angle (in degrees), defining the outer edge
	///									of the spotlight's outer cone.
	///									Points outside this cone are not lit by the spotlight.
	///									The value must be between 0 and 90.  
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
    virtual IMcErrors::ECode SetHalfOuterAngle(
		float fHalfOuterAngle,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetHalfOuterAngle(...)
    //------------------------------------------------------------------------------
    /// Retrieves the half outer angle property defined by SetXXX().
    ///
	/// \param[out] pfHalfOuterAngle	The half angle (in degrees), defining the outer edge
	///									of the spotlight's outer cone.
	///									Points outside this cone are not lit by the spotlight.
	///									The value must be between 0 and 90.
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
	virtual IMcErrors::ECode GetHalfOuterAngle(
		float *pfHalfOuterAngle,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SeHalftInnerAngle(...)
	//------------------------------------------------------------------------------
	/// Defines the half inner angle as a shared or private property.
	///
	/// \param[in] fHalfInnerAngle		The half angle (in degrees) of a spotlight's inner cone
	///									the fully illuminated spotlight cone. 
	///									The value must be from 0 through fOuterAngle.
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
	virtual IMcErrors::ECode SetHalfInnerAngle(
		float fHalfInnerAngle,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetHalfInnerAngle(...)
	//------------------------------------------------------------------------------
	/// Retrieves the half inner angle property defined by SetXXX().
	///
	/// \param[out] pfHalfInnerAngle	The half angle (in degrees) of a spotlight's inner cone 
	///									the fully illuminated spotlight cone. 
	///									The value must be from 0 through dOuterAngle.
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
	virtual IMcErrors::ECode GetHalfInnerAngle(
		float *pfHalfInnerAngle,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}
};

//==================================================================================
// Interface Name: IMcDirectionalLightItem
//----------------------------------------------------------------------------------
///	Interface for directional light items.
/// 
//==================================================================================
class IMcDirectionalLightItem : public virtual IMcLightBasedItem
{
protected:

	virtual ~IMcDirectionalLightItem() {};

public:

	enum
	{
		//==============================================================================
		/// Node unique ID for this interface
		//==============================================================================
		NODE_TYPE = 59
	};

public:

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a directional light item
	///
	/// The item will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Release() method is called,
	/// when its (or its parents) Disconnect() method is called,
	/// or when its object scheme is destroyed.
	///
	/// \param[out] ppItem				The newly created item
	/// \param[in] DefaultDiffusColor	The default diffuse color emitted by the light
	/// \param[in] DefaultSpecularColor	The default specular color emitted by the light
	/// \param[in] DefaultDirection		The default direction that the light is pointing. 
	///									The vector need not be normalized, but it should have a nonzero length.
	///
	/// \return
	///     - Status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcDirectionalLightItem **ppItem,
		const SMcFColor &DefaultDiffusColor = fcWhiteOpaque,
		const SMcFColor &DefaultSpecularColor = fcBlackTransparent,
		const SMcFVector3D &DefaultDirection = SMcFVector3D(0.0, 0.0, -1.0));

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
		IMcDirectionalLightItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}

	/// \name Direction
	//@{

	//==============================================================================
	// Method Name: SetDirection(...)
	//------------------------------------------------------------------------------
	/// Defines the direction as a shared or private property.
	///
	/// \param[in] Direction			The direction that the light is pointing.
	///									The vector need not be normalized, 
	///									but it should have a nonzero length.
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
	virtual IMcErrors::ECode SetDirection(
		const SMcFVector3D &Direction,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetDirection(...)
	//------------------------------------------------------------------------------
	/// Retrieves the direction property defined by SetXXX().
	///
	/// \param[out] pDirection			The direction that the light is pointing. 
	///									The vector need not be normalized, 
	///									but it should have a nonzero length.
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
	virtual IMcErrors::ECode GetDirection(
		SMcFVector3D *pDirection,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}
};
