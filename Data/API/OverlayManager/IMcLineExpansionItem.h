#pragma once
//==================================================================================
/// \file IMcLineExpansionItem.h
/// Interface for line expansion item
//==================================================================================

#include "McExports.h"
#include "McCommonTypes.h"
#include "OverlayManager/IMcLineBasedItem.h"
//==================================================================================
// Interface Name: IMcLineExpansionItem
//----------------------------------------------------------------------------------
///	Interface for line expansion item.
//==================================================================================
class IMcLineExpansionItem : public virtual IMcClosedShapeItem
{
protected:

	virtual ~IMcLineExpansionItem() {};

public:

    enum
    {
        //==============================================================================
        /// Node unique ID for this interface
        //==============================================================================
        NODE_TYPE = 68
    };

public:

    /// \name Create
    //@{

    //==============================================================================
    // Method Name: Create(...)
    //------------------------------------------------------------------------------
    /// Create a line expansion item.
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
	/// \param[in] eLineExpansionCoordinateSystem	The line expansion coordinate system (see #EMcPointCoordSystem)
	/// \param[in] eLineExpansionType			The line expansion geometry type (must be IMcObjectSchemeItem::EGT_GEOMETRIC_IN_VIEWPORT 
	///											for \p eLineExpansionCoordinateSystem == #EPCS_SCREEN), 
	///											see IMcObjectSchemeItem::EGeometryType
	/// \param[in] fDefaultRadius				The default line expansion radius 
    /// \param[in] DefaultLineStyle				The default line style
    /// \param[in] DefaultLineColor				The default line color
    /// \param[in] fDefaultLineWidth			The default line width (in meters)
    /// \param[in] pDefaultLineTexture			The default line texture
	/// \param[in] DefaultLineTextureHeightRange The default line texture height range
    /// \param[in] fDefaultLineTextureScale		The default line texture scale
    /// \param[in] eDefaultFillStyle			The default fill style
    /// \param[in] DefaultFillColor				The default fill color
    /// \param[in] pDefaultFillTexture			The default fill texture
    /// \param[in] DefaultFillTextureScale		The default fill texture scale
    ///
    /// \return
    ///     - Status result
    //==============================================================================
    static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcLineExpansionItem **ppItem,
		UINT uItemSubTypeBitField,
		EMcPointCoordSystem eLineExpansionCoordinateSystem,
		EGeometryType eLineExpansionType = EGT_GEOMETRIC_IN_OVERLAY_MANAGER,
		float fDefaultRadius = 1.0,
        ELineStyle DefaultLineStyle = ELS_SOLID,
        const SMcBColor &DefaultLineColor = bcBlackOpaque,
        float fDefaultLineWidth = 1.0,
        IMcTexture *pDefaultLineTexture = NULL,
		const SMcFVector2D &DefaultLineTextureHeightRange = SMcFVector2D(0.0, -1.0),
        float fDefaultLineTextureScale = 1.0,
        EFillStyle eDefaultFillStyle = EFS_SOLID,
        const SMcBColor &DefaultFillColor = bcBlackOpaque,
        IMcTexture *pDefaultFillTexture = NULL,
        const SMcFVector2D &DefaultFillTextureScale = SMcFVector2D(1.0, 1.0));
 
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
		IMcLineExpansionItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}

	/// \name Line Expansion coordinate system
	//@{

	//==============================================================================
	// Method Name: GetLineExpansionCoordinateSystem(...)
	//------------------------------------------------------------------------------
	/// Retrieves the line expansion's coordinate system.
	///
	/// \param[out] peLineExpansionCoordinateSystem		The line expansion's coordinate system
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLineExpansionCoordinateSystem(
		EMcPointCoordSystem *peLineExpansionCoordinateSystem) const = 0;

	//@}

	/// \name Line Expansion Type
	//@{

	//==============================================================================
	// Method Name: SetLineExpansionType(...)
	//------------------------------------------------------------------------------
	/// Sets the line expansion type.
	///
	/// Applicable when its points are in world/image coordinate system.
	///
	/// \param[in] eLineExpansionType		The line expansion's type
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetLineExpansionType(EGeometryType eLineExpansionType) = 0;

	//==============================================================================
	// Method Name: GetLineExpansionType(...)
	//------------------------------------------------------------------------------
	/// Retrieves the line expansion type.
	///
	/// Applicable when its points are in world/image coordinate system.
	///
	/// \param[out] peLineExpansionType		The line expansion's type
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetLineExpansionType(EGeometryType *peLineExpansionType) const = 0;

	//@}

	/// \name Line Expansion Radius
	//@{

	//==============================================================================
	// Method Name: SetRadius(...)
	//------------------------------------------------------------------------------
	/// Defines the line expansion radius as a shared or private property.
	///
	/// \param[in] fRadius				The line expansion radius.
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
	virtual IMcErrors::ECode SetRadius(
		float fRadius,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetRadius(...)
	//------------------------------------------------------------------------------
	/// Retrieves the line expansion radius property defined by SetXXX().
	///
	/// \param[out] pfRadius			The line expansion radius.
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
	virtual IMcErrors::ECode GetRadius(
		float *pfRadius,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

};
