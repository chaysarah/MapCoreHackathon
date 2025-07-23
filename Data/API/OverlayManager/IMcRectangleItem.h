#pragma once
//==================================================================================
/// \file IMcRectangleItem.h
/// Interface for rectangle/square item
//==================================================================================

#include "McExports.h"
#include "OverlayManager/IMcLineBasedItem.h"

//==================================================================================
// Interface Name: IMcRectangleItem
//----------------------------------------------------------------------------------
///	Interface for rectangle/square item.
/// 
//==================================================================================
class IMcRectangleItem : public virtual IMcClosedShapeItem
{
protected:

	virtual ~IMcRectangleItem() {};

public:

    enum
    {
        //==============================================================================
        /// Node unique ID for this interface
        //==============================================================================
        NODE_TYPE = 65
    };

	/// Type of rectangle definition that specifies how it is calculated based on its point(s) and properties
	enum ERectangleDefinition 
	{
		/// rectangle by one of its diagonals; 
		/// two location points define the diagonal of axes-aligned (not rotated) rectangle
		/// (x/y-radiuses are ignored)
		ERD_RECTANGLE_DIAGONAL_POINTS,

		/// rectangle by center, half-width and half-height; 
		/// one location point defines the center, x-radius defines half-width, y-radius defines half-height 
		ERD_RECTANGLE_CENTER_DIMENSIONS,

		/// square by center and half-edge; 
		/// two location points define the center, x-radius defines half-edge;
		/// (y-radius is ignored)
		ERD_SQUARE_CENTER_DIMENSION
	};

public:

    /// \name Create
    //@{

    //==============================================================================
    // Method Name: Create(...)
    //------------------------------------------------------------------------------
    /// Create a rectangle item
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
	/// \param[in] eRectangleCoordinateSystem	The item coordinate system (see #EMcPointCoordSystem)
	/// \param[in] eRectangleType				The rectangle geometry type (must be IMcObjectSchemeItem::EGT_GEOMETRIC_IN_VIEWPORT 
	///											for eRectangleCoordinateSystem == EPCS_SCREEN), 
	///											see IMcObjectSchemeItem::EGeometryType
	/// \param[in] eRectangleDefinition			The rectangle definition 
	/// \param[in] fDefaultRadiusX				The default rectangle's X radius (=half width)
	/// \param[in] fDefaultRadiusY				The default rectangle's Y radius (=half height)
    /// \param[in] eDefaultLineStyle			The default line style
    /// \param[in] DefaultLineColor				The default line color
    /// \param[in] fDefaultLineWidth			The default line width
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
		IMcRectangleItem **ppItem,
		UINT uItemSubTypeBitField,
		EMcPointCoordSystem eRectangleCoordinateSystem,
		EGeometryType eRectangleType = EGT_GEOMETRIC_IN_VIEWPORT,
		ERectangleDefinition eRectangleDefinition = ERD_RECTANGLE_DIAGONAL_POINTS, 
		float fDefaultRadiusX = 1.0,
		float fDefaultRadiusY = 1.0,
        ELineStyle eDefaultLineStyle = ELS_SOLID,
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
		IMcRectangleItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}

	/// \name Rectangle coordinate system
	//@{

	//==============================================================================
	// Method Name: GetRectangleCoordinateSystem(...)
	//------------------------------------------------------------------------------
	/// Retrieves the rectangle's coordinate system.
	///
	/// \param[out] peRectangleCoordinateSystem		The rectangle's coordinate system
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRectangleCoordinateSystem(
		EMcPointCoordSystem *peRectangleCoordinateSystem) const = 0;
	
	//@}

	/// \name Rectangle Type and Definition
	//@{

	//==============================================================================
	// Method Name: SetRectangleType(...)
	//------------------------------------------------------------------------------
	/// Sets the rectangle type.
	///
	/// Applicable when its point(s) are in world/image coordinate system.
	///
	/// \param[in] eRectangleType	The rectangle's type
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetRectangleType(EGeometryType eRectangleType) = 0;

	//==============================================================================
	// Method Name: GetRectangleType(...)
	//------------------------------------------------------------------------------
	/// Retrieves the rectangle type.
	///
	/// Applicable when its point(s) are in world/image coordinate system.
	///
	/// \param[out] peRectangleType	The rectangle's type
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetRectangleType(EGeometryType *peRectangleType) const = 0;
	
	//==============================================================================
	// Method Name: SetRectangleDefinition(...)
	//------------------------------------------------------------------------------
	/// Sets rectangle definition specifying how it is calculated based on its point(s) and properties
	///
	/// \param[in]	eRectangleDefinition	Rectangle definition
	///										(the default is #ERD_RECTANGLE_DIAGONAL_POINTS)
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetRectangleDefinition(ERectangleDefinition eRectangleDefinition) = 0;

	//==============================================================================
	// Method Name: GetRectangleDefinition(...)
	//------------------------------------------------------------------------------
	/// Retrieves rectangle definition specifying how it is calculated based on its point(s) and properties
	///
	/// \param[out] peRectangleDefinition	Rectangle definition
	///										(the default is #ERD_RECTANGLE_DIAGONAL_POINTS)
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetRectangleDefinition(ERectangleDefinition *peRectangleDefinition) const = 0;

	//@}

	/// \name Rectangle sizes
	//@{

	//==============================================================================
	// Method Name: SetRadiusX(...)
	//------------------------------------------------------------------------------
	/// Defines the rectangle X radius (half-width) as a shared or private property.
	///
	/// \param[in] fRadiusX				The rectangle X radius.
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
	/// Retrieves the recangle X radius (half-width) property defined by SetXXX().
	///
	/// \param[out] pfRadiusX			The rectangle X radius.
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
	/// Defines the rectangle Y radius (half-height) as a shared or private property.
	///
	/// \param[in] fRadiusY				The rectangle Y radius.
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
	/// Retrieves the rectangle Y radius (half-height) property defined by SetXXX().
	///
	/// \param[out] pfRadiusY			The rectangle Y radius
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
