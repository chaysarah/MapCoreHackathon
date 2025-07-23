#pragma once
//==================================================================================
/// \file IMcArcItem.h
/// Interface for arc item
//==================================================================================

#include "McExports.h"
#include "McCommonTypes.h"
#include "OverlayManager/IMcLineBasedItem.h"

//==================================================================================
// Interface Name: IMcArcItem
//----------------------------------------------------------------------------------
///	Interface for arc item.
/// 
//==================================================================================
class IMcArcItem : public virtual IMcLineBasedItem
{
protected:

	virtual ~IMcArcItem() {};

public:

    enum
    {
        //==============================================================================
        /// Node unique ID for this interface
        //==============================================================================
        NODE_TYPE = 53
    };
	
public:

	/// \name Create
    //@{

    //==============================================================================
    // Method Name: Create(...)
    //------------------------------------------------------------------------------
    /// Create an elliptical arc item.
    ///
	/// The item will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Release() method is called,
	/// when its (or its parents) Disconnect() method is called,
	/// or when its object scheme is destroyed.
	///
	/// \param[out] ppItem						The newly created item
	/// \param[in] uItemSubTypeBitField			The item sub type (see IMcObjectSchemeItem::EItemSubTypeFlags)
	/// \param[in] eEllipseCoordinateSystem		The ellipse coordinate system (see #EMcPointCoordSystem)
	/// \param[in] eEllipseType					The ellipse geometry type (must be IMcObjectSchemeItem::EGT_GEOMETRIC_IN_VIEWPORT 
	///											for \p eEllipseCoordinateSystem == #EPCS_SCREEN), 
	///											see IMcObjectSchemeItem::EGeometryType
    /// \param[in] fDefaultRadiusX				The default X radius 
    /// \param[in] fDefaultRadiusY				The default Y radius 
	/// \param[in] fDefaultStartAngle			The default start angle (in degrees)
    /// \param[in] fDefaultEndAngle				The default end angle (in degrees)
    /// \param[in] eDefaultLineStyle			The default line style
    /// \param[in] DefaultLineColor				The default line color
    /// \param[in] fDefaultLineWidth			The default line width (in meters)
    /// \param[in] pDefaultLineTexture			The default line texture
	/// \param[in] DefaultLineTextureHeightRange The default line texture height range
    /// \param[in] fDefaultLineTextureScale		The default line texture scale
    ///
    /// \return
    ///     - Status result
    //==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcArcItem **ppItem,
		UINT uItemSubTypeBitField,
		EMcPointCoordSystem eEllipseCoordinateSystem,
		EGeometryType eEllipseType = EGT_GEOMETRIC_IN_OVERLAY_MANAGER,
		float fDefaultRadiusX = 1.0,
		float fDefaultRadiusY = 1.0,
        float fDefaultStartAngle = 0.0,
        float fDefaultEndAngle = 360.0,
        ELineStyle eDefaultLineStyle = ELS_SOLID,
        const SMcBColor &DefaultLineColor = bcBlackOpaque,
        float fDefaultLineWidth = 1.0,
        IMcTexture *pDefaultLineTexture = NULL,
		const SMcFVector2D &DefaultLineTextureHeightRange = SMcFVector2D(0.0, -1.0),
        float fDefaultLineTextureScale = 1.0);

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
		IMcArcItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}

	/// \name Ellipse coordinate system
	//@{

	//==============================================================================
	// Method Name: GetEllipseCoordinateSystem(...)
	//------------------------------------------------------------------------------
	/// Retrieves the ellipse's coordinate system.
	///
	/// \param[out] peEllipseCoordinateSystem	The ellipse's coordinate system
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetEllipseCoordinateSystem(
		EMcPointCoordSystem *peEllipseCoordinateSystem) const = 0;

	//@}

    /// \name Ellipse Type and Definition
    //@{

	//==============================================================================
	// Method Name: SetEllipseType(...)
	//------------------------------------------------------------------------------
	/// Sets the ellipse type.
	///
	/// Applicable when the center is in world/image coordinate system.
	///
	/// \param[in] eEllipseType	The ellipse's type
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetEllipseType(EGeometryType eEllipseType) = 0;

    //==============================================================================
    // Method Name: GetEllipseType(...)
    //------------------------------------------------------------------------------
    /// Retrieves the ellipse type.
	///
	/// Applicable when the center is in world/image coordinate system.
	///
	/// \param[out] peEllipseType	The ellipse's type
    ///
    /// \return
    ///     - Status result
    //==============================================================================
    virtual IMcErrors::ECode GetEllipseType(EGeometryType *peEllipseType) const = 0;

	//==============================================================================
	// Method Name: SetEllipseDefinition(...)
	//------------------------------------------------------------------------------
	/// Sets ellipse definition specifying how it is calculated based on its point(s) and properties
	///
	/// \param[in]	eEllipseDefinition		Ellipse definition
	///										(the default is #EED_ELLIPSE_CENTER_RADIUSES_ANGLES)
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetEllipseDefinition(EEllipseDefinition eEllipseDefinition) = 0;

	//==============================================================================
	// Method Name: GetEllipseDefinition(...)
	//------------------------------------------------------------------------------
	/// Retrieves ellipse definition specifying how it is calculated based on its point(s) and properties
	///
	/// \param[out] peEllipseDefinition		Ellipse definition
	///										(the default is #EED_ELLIPSE_CENTER_RADIUSES_ANGLES)
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetEllipseDefinition(EEllipseDefinition *peEllipseDefinition) const = 0;

	//@}

    /// \name Arc Start & End Angles
    //@{

    //==============================================================================
    // Method Name: SetStartAngle(...)
    //------------------------------------------------------------------------------
	/// Defines the arc start angle as a shared or private property.
	///
	/// \param[in] fStartAngle			The arc start angle (in degrees).
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
	virtual IMcErrors::ECode SetStartAngle(
		float fStartAngle, 
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetStartAngle(...)
    //------------------------------------------------------------------------------
	/// Retrieves the arc start angle property defined by SetXXX().
	///
    /// \param[out] pfStartAngle		The arc start angle (in degrees).
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
    virtual IMcErrors::ECode GetStartAngle(
		float *pfStartAngle,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

    //==============================================================================
    // Method Name: SetEndAngle(...)
    //------------------------------------------------------------------------------
    /// Defines the arc end angle as a shared or private property.
	///
    /// \param[in] fEndAngle			The arc end angle (in degrees).
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
    virtual IMcErrors::ECode SetEndAngle(
		float fEndAngle,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetEndAngle(...)
    //------------------------------------------------------------------------------
    /// Retrieves the arc end angle property defined by SetXXX().
    ///
    /// \param[out] pfEndAngle			The arc end angle (in degrees).
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
    virtual IMcErrors::ECode GetEndAngle(
		float *pfEndAngle,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

    //@}

    /// \name Arc Radiuses
    //@{

    //==============================================================================
    // Method Name: SetRadiusX(...)
    //------------------------------------------------------------------------------
    /// Defines the arc X radius as a shared or private property.
    ///
    /// \param[in] fRadiusX				The arc X-axis radius.
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
    virtual IMcErrors::ECode SetRadiusX(
		float fRadiusX,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetRadiusX(...)
    //------------------------------------------------------------------------------
    /// Retrieves the arc X radius property defined by SetXXX().
    ///
    /// \param[out] pfRadiusX			The arc X-axis radius.
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
    virtual IMcErrors::ECode GetRadiusX(
		float *pfRadiusX,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetRadiusY(...)
	//------------------------------------------------------------------------------
	/// Defines the arc Y radius as a shared or private property.
	///
	/// \param[in] fRadiusY				The arc Y-axis radius.
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
	virtual IMcErrors::ECode SetRadiusY(
		float fRadiusY,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetRadiusY(...)
	//------------------------------------------------------------------------------
	/// Retrieves the arc Y radius property defined by SetXXX().
	///
	/// \param[out] pfRadiusY			The src Y-axis radius.
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
	virtual IMcErrors::ECode GetRadiusY(
		float *pfRadiusY,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

    //@}
};
