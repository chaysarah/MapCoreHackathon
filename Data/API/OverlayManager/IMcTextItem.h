#pragma once
//==================================================================================
/// \file IMcTextItem.h
/// Interface for text item
//==================================================================================

#include "McExports.h"
#include "McCommonTypes.h"
#include "OverlayManager/IMcSymbolicItem.h"

class IMcFont;

//==================================================================================
// Interface Name: IMcTextItem
//----------------------------------------------------------------------------------
///	Interface for text item.
/// 
//==================================================================================
class IMcTextItem : public virtual IMcSymbolicItem
{
protected:

	virtual ~IMcTextItem() {};

public:

	/// Mode of optional preventing upside-down orientation by 180-degree rotation when necessarily
	enum ENeverUpsideDownMode
	{
		ENUDM_NONE,				///< disabled (no prevention)
		ENUDM_ROTATE_TEXT,		///< when necessarily, the whole text is rotated
		ENUDM_ROTATE_EACH_LINE	///< when necessarily, each text line is rotated
	};

	/// Background shape
	enum EBackgroundShape
	{
		EBS_RECTANGLE,					///< rectangle
		EBS_RECTANGLE_ROUNDED_SIDES,	///< rectangle with rounded sides (half-circles on the left and right edges)
		EBS_RECTANGLE_ROUNDED_CORNERS	///< rectangle with rounded corners (quarter-circles with diameters of rectangle's half-height)
	};
	
	enum
    {
        //==============================================================================
        /// Node unique ID for this interface
        //==============================================================================
        NODE_TYPE = 66
    };

public:

	/// \name Create
    //@{

    //==============================================================================
    // Method Name: Create(...)
    //------------------------------------------------------------------------------
    /// Create a text item. 
    ///
	/// The item will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Release() method is called,
	/// when its (or its parents) Disconnect() method is called,
	/// or when its object scheme is destroyed.
	///
	/// \param[out] ppItem							The newly created item
	/// \param[in] uItemSubTypeBitField				The item sub type (see IMcObjectSchemeItem::EItemSubTypeFlags)
	/// \param[in] eTextCoordinateSystem			The text coordinate system (see #EMcPointCoordSystem)
	/// \param[in] pDefaultFont						The default text font resource
    /// \param[in] DefaultScale						The default text scale
    /// \param[in] eNeverUpsideDownMode				Optional prevention of text's upside-down orientation 
    ///												by 180-degree rotation of the whole text or each text line
	///												(see #ENeverUpsideDownMode); the default is #ENUDM_NONE
	/// \param[in] eDefaultTextAlignment			The default text alignment inside its bounding rectangle
	/// \param[in] eDefaultRectAlignment			The bounding rectangle alignment: the relation between 
	///												text's location point to its bounding box;
	///												the default is #EBRP_CENTER.
	/// \param[in] bDefaultRightToLeftReadingOrder	The default reading order (the text is laid out 
	///												in right to left)
	///												(for RightToLeftReadingOrder language fonts only)
	/// \param[in] uDefaultMargin					The default size in pixels of margin around the text 
	/// \param[in] DefaultTextColor					The default text color
    /// \param[in] DefaultBackgroundColor			The default background color
	///
    /// \return
    ///     - Status result
    //==============================================================================
    static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcTextItem **ppItem,
		UINT uItemSubTypeBitField,
		EMcPointCoordSystem eTextCoordinateSystem,
		IMcFont * pDefaultFont,
        const SMcFVector2D &DefaultScale = SMcFVector2D(1.0, 1.0),
		ENeverUpsideDownMode eNeverUpsideDownMode = ENUDM_NONE,
		EAxisXAlignment eDefaultTextAlignment = EXA_CENTER,
		IMcSymbolicItem::EBoundingRectanglePoint eDefaultRectAlignment = EBRP_CENTER,
		bool bDefaultRightToLeftReadingOrder = false,
		UINT uDefaultMargin = 0,
        const SMcBColor &DefaultTextColor = bcBlackOpaque,
        const SMcBColor &DefaultBackgroundColor = bcWhiteTransparent);

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
		IMcTextItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}

	/// \name Text coordinate system
	//@{

	//==============================================================================
	// Method Name: GetTextCoordinateSystem(...)
	//------------------------------------------------------------------------------
	/// Retrieves the text's coordinate system.
	///
	/// \param[out] peTextCoordinateSystem	The text's coordinate system
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTextCoordinateSystem(
		EMcPointCoordSystem *peTextCoordinateSystem) const = 0;

	//@}

	/// \name Text
	//@{

	//==============================================================================
	// Method Name: SetText(...)
	//------------------------------------------------------------------------------
	/// Defines the text as a shared or private property.
	///
	/// \param[in] Text					The text.
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
	virtual IMcErrors::ECode SetText(
		const SMcVariantString &Text,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetText(...)
	//------------------------------------------------------------------------------
	/// Retrieves the text property defined by SetXXX().
	///
	/// \param[out] pText				The text.
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
	virtual IMcErrors::ECode GetText(
		SMcVariantString *pText,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Font
	//@{

	//==============================================================================
    // Method Name: SetFont(...)
    //------------------------------------------------------------------------------
    /// Defines the text font resource as a shared or private property.
    ///
    /// \param[in] pFont				The font resource.
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
	virtual IMcErrors::ECode SetFont(
		IMcFont * pFont,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;
	
    //==============================================================================
    // Method Name: GetFont(...)
    //------------------------------------------------------------------------------
    /// Retrieves the text font resource property defined by SetXXX().
    ///
    /// \param[out] ppFont				The font resource.
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
	virtual IMcErrors::ECode GetFont(
		IMcFont **ppFont,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Text Alignment and RightToLeft Reading Order
	//@{

	//==============================================================================
    // Method Name: SetTextAlignment(...)
    //------------------------------------------------------------------------------
    /// Sets the text alignment as a shared or private property.
    ///
	/// \param[in] eTextAlignment		The text alignment inside its bounding rectangle.
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
	virtual IMcErrors::ECode SetTextAlignment(
		EAxisXAlignment eTextAlignment,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;
	
    //==============================================================================
    // Method Name: GetTextAlignment(...)
    //------------------------------------------------------------------------------
    /// Retrieves the text alignment property defined by SetXXX().
    ///
	/// \param[out] *peTextAlignment	The text alignment inside its bounding rectangle.
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
	virtual IMcErrors::ECode GetTextAlignment(
		EAxisXAlignment *peTextAlignment,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetRightToLeftReadingOrder(...)
	//------------------------------------------------------------------------------
	/// Defines the text reading order as a shared or private property.
	/// (for RightToLeftReadingOrder language fonts only).
	///
	/// \param[in] bRightToLeftReadingOrder		Whether or not the text is laid out in right to left.
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
	virtual IMcErrors::ECode SetRightToLeftReadingOrder(
		bool bRightToLeftReadingOrder,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetRightToLeftReadingOrder(...)
	//------------------------------------------------------------------------------
	/// Retrieves the text reading order property defined by SetXXX().
	/// (for RightToLeftReadingOrder language fonts only).
	///
	/// \param[out] pbRightToLeftReadingOrder	Whether or not the text is laid out in right to left.
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
	virtual IMcErrors::ECode GetRightToLeftReadingOrder(
		bool *pbRightToLeftReadingOrder,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Bounding Rectangle Alignment
	//@{

	//==============================================================================
	// Method Name: SetRectAlignment(...)
	//------------------------------------------------------------------------------
	/// Sets the bounding rectangle alignment as a shared or private property.
	///
	/// \param[in] eRectAlignment		The bounding rectangle alignment: the relation between 
	///									text's location point to its bounding box;
	///									the default is #EBRP_CENTER.
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
	virtual IMcErrors::ECode SetRectAlignment(
		IMcSymbolicItem::EBoundingRectanglePoint eRectAlignment,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetRectAlignment(...)
	//------------------------------------------------------------------------------
	/// Retrieves the bounding rectangle alignment property defined by SetXXX().
	///
	/// \param[out] peRectAlignment		The bounding rectangle alignment: the relation 
	///									between text's location point to its bounding box.
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
	virtual IMcErrors::ECode GetRectAlignment(
		IMcSymbolicItem::EBoundingRectanglePoint *peRectAlignment,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Scale
	//@{

	//==============================================================================
	// Method Name: SetScale(...)
	//------------------------------------------------------------------------------
	/// Defines the text scale as a shared or private property.
	///
	/// \param[in] Scale				The text scale.
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
	virtual IMcErrors::ECode SetScale(
		const SMcFVector2D &Scale,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetScale(...)
	//------------------------------------------------------------------------------
	/// Retrieves the text scale property defined by SetXXX().
	///
	/// \param[out] pScale				The text scale.
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
	virtual IMcErrors::ECode GetScale(
		SMcFVector2D *pScale,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Margins
	//@{

	//==============================================================================
	// Method Name: SetMargin(...)
	//------------------------------------------------------------------------------
	/// Defines the text margin as a shared or private property.
	///
	/// \param[in] uMargin				The size in pixels of margin around the text;
	///									used as X-margin only if non-default Y-margin is set SetMarginY().
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
	virtual IMcErrors::ECode SetMargin(
		UINT uMargin,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetMargin(...)
	//------------------------------------------------------------------------------
	/// Retrieves the text margin property defined by SetXXX().
	///
	/// \param[out] puMargin			The size in pixels of margin around the text;
	///									used as X-margin only if non-default Y-margin is set SetMarginY().
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
	virtual IMcErrors::ECode GetMargin(
		UINT *puMargin,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetMarginY(...)
	//------------------------------------------------------------------------------
	/// Defines the text Y-margin as a shared or private property.
	///
	/// \param[in] uMargin				The size in pixels of Y-margin around the text;
	///									the default is `UINT_MAX` (it means that the margin set by SetMargin() is used as both X-margin and Y-margin).
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
	virtual IMcErrors::ECode SetMarginY(
		UINT uMargin,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetMarginY(...)
	//------------------------------------------------------------------------------
	/// Retrieves the text Y-margin property defined by SetXXX().
	///
	/// \param[out] puMargin			The size in pixels of Y-margin around the text;
	///									the default is `UINT_MAX` (it means that the margin set by SetMargin() is used as both X-margin and Y-margin).
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
	virtual IMcErrors::ECode GetMarginY(
		UINT *puMargin,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Text Color
	//@{

	//==============================================================================
	// Method Name: SetTextColor(...)
	//------------------------------------------------------------------------------
	/// Defines the text color as a shared or private property.
	///
	/// \param[in] TextColor			The text color.
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
	virtual IMcErrors::ECode SetTextColor(
		const SMcBColor &TextColor,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetTextColor(...)
	//------------------------------------------------------------------------------
	/// Retrieves the text color property defined by SetXXX().
	///
	/// \param[out] pTextColor			The text color.
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
	virtual IMcErrors::ECode GetTextColor(
		SMcBColor *pTextColor,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Background Style
	//@{

	//==============================================================================
	// Method Name: SetBackgroundColor(...)
	//------------------------------------------------------------------------------
	/// Defines the background color as a shared or private property.
	///
	/// \param[in] BackgroundColor		The Text background color.
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
	virtual IMcErrors::ECode SetBackgroundColor(
		const SMcBColor &BackgroundColor,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetBackgroundColor(...)
	//------------------------------------------------------------------------------
	/// Retrieves the background color property defined by SetXXX().
	///
	/// \param[out] pBackgroundColor	The text background color.
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
	virtual IMcErrors::ECode GetBackgroundColor(
		SMcBColor *pBackgroundColor,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetBackgroundShape(...)
	//------------------------------------------------------------------------------
	/// Defines the background shape as a shared or private property.
	///
	/// \param[in] eBackgroundShape		The Text background shape; the default is #EBS_RECTANGLE.
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
	virtual IMcErrors::ECode SetBackgroundShape(
		EBackgroundShape eBackgroundShape,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetBackgroundShape(...)
	//------------------------------------------------------------------------------
	/// Retrieves the background shape property defined by SetXXX().
	///
	/// \param[out] peBackgroundShape	The text background shape; the default is #EBS_RECTANGLE.
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
	virtual IMcErrors::ECode GetBackgroundShape(
		EBackgroundShape *peBackgroundShape,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Outline Color
	//@{

	//==============================================================================
	// Method Name: SetOutlineColor(...)
	//------------------------------------------------------------------------------
	/// Defines the outline color as a shared or private property.
	///
	/// \param[in] OutlineColor			The outline color.
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
	virtual IMcErrors::ECode SetOutlineColor(
		const SMcBColor &OutlineColor,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetOutlineColor(...)
	//------------------------------------------------------------------------------
	/// Retrieves the outline color property defined by SetXXX().
	///
	/// \param[out] pOutlineColor		The outline color.
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
	virtual IMcErrors::ECode GetOutlineColor(
		SMcBColor *pOutlineColor,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

    /// \name Never Upside Down
	//@{

	//==============================================================================
	// Method Name: SetNeverUpsideDownMode(...)
	//------------------------------------------------------------------------------
	/// Defines the never-upside-down mode as a shared or private property.
	///
	/// \param[in] eNeverUpsideDownMode	Optional prevention of text's upside-down orientation
	///									by 180-degree rotation of the whole text or each text line; the default is #ENUDM_NONE.
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
	virtual IMcErrors::ECode SetNeverUpsideDownMode(
		ENeverUpsideDownMode eNeverUpsideDownMode,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetNeverUpsideDownMode(...)
	//------------------------------------------------------------------------------
	/// Retrieves the never-upside-down mode property defined by SetXXX().
	///
	/// \param[out] peNeverUpsideDownMode	Optional prevention of text's upside-down orientation
	///										by 180-degree rotation of the whole text or each text line; the default is #ENUDM_NONE.
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
	virtual IMcErrors::ECode GetNeverUpsideDownMode(
		ENeverUpsideDownMode *peNeverUpsideDownMode,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}
};
