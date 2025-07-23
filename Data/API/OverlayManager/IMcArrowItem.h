#pragma once
//==================================================================================
/// \file IMcArrowItem.h
/// Interface for arrow item
//==================================================================================

#include "McExports.h"
#include "OverlayManager/IMcLineBasedItem.h"

//==================================================================================
// Interface Name: IMcArrowItem
//----------------------------------------------------------------------------------
///	Interface for arrow item.
/// 
//==================================================================================
class IMcArrowItem : public virtual IMcClosedShapeItem
{
protected:

	virtual ~IMcArrowItem() {};

public:

    enum
    {
        //==============================================================================
        /// Node unique ID for this interface
        //==============================================================================
        NODE_TYPE = 54
    };

public:

    /// \name Create
    //@{

    //==============================================================================
    // Method Name: Create(...)
    //------------------------------------------------------------------------------
    /// Create an arrow item.
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
	/// \param[in] eArrowCoordinateSystem		The arrow coordinate system (see #EMcPointCoordSystem)
    /// \param[in] fDefaultHeadSize				The default head size
	/// \param[in] fDefaultHeadAngle			The default head angle (in degrees)
	/// \param[in] fDefaultGapSize				The default gap size between the double arrow lines:
	///											zero - single arrow
	///											positive - double arrow with constant gap size
	///											negative - double arrow with changing gap size
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
		IMcArrowItem **ppItem,
		UINT uItemSubTypeBitField,
		EMcPointCoordSystem eArrowCoordinateSystem,
        float fDefaultHeadSize = 10.0,
        float fDefaultHeadAngle = 45.0,
		float fDefaultGapSize = 0.0,
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
		IMcArrowItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}

	/// \name Arrow coordinate system
	//@{

	//==============================================================================
	// Method Name: GetArrowCoordinateSystem(...)
	//------------------------------------------------------------------------------
	/// Retrieves the arrow's coordinate system.
	///
	/// \param[out] peArrowCoordinateSystem		The arrow's coordinate system
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetArrowCoordinateSystem(
		EMcPointCoordSystem *peArrowCoordinateSystem) const = 0;

	//@}

    /// \name Head Size
    //@{

    //==============================================================================
    // Method Name: SetHeadSize(...)
    //------------------------------------------------------------------------------
    /// Defines the arrow head size as a shared or private property.
	///
    /// \param[in] fHeadSize			The arrow head size.
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
    virtual IMcErrors::ECode SetHeadSize(
		float fHeadSize,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetHeadSize(...)
    //------------------------------------------------------------------------------
    /// Retrieves the arrow head size property defined by SetXXX().
    ///
    /// \param[out] pfHeadSize			The arrow head size.
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
    virtual IMcErrors::ECode GetHeadSize(
		float *pfHeadSize,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

    //@}

    /// \name Head Angle
    //@{

    //==============================================================================
    // Method Name: SetHeadAngle(...)
    //------------------------------------------------------------------------------
    /// Defines the arrow head angle as a shared or private property.
	///
    /// \param[in] fHeadAngle			The arrow head angle (in degrees).
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
    virtual IMcErrors::ECode SetHeadAngle(
		float fHeadAngle,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetHeadAngle(...)
    //------------------------------------------------------------------------------
    /// Retrieves the arrow head angle property defined by SetXXX().
    ///
    /// \param[out] pfHeadAngle			The arrow head angle (in degrees).
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
    virtual IMcErrors::ECode GetHeadAngle(
		float *pfHeadAngle,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

    //@}

    /// \name Gap Size
    //@{

    //==============================================================================
    // Method Name: SetGapSize(...)
    //------------------------------------------------------------------------------
    /// Defines the arrow gap size as a shared or private property.
    ///
	/// \param[in] fGapSize				The gap size between the double arrow lines:
	///									zero - single arrow
	///									positive - double arrow with constant gap size
	///									negative - double arrow with changing gap size.
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
    virtual IMcErrors::ECode SetGapSize(
		float fGapSize,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetGapSize(...)
    //------------------------------------------------------------------------------
    /// Retrieves the arrow gap size property defined by SetXXX().
    ///
	/// \param[out] pfGapSize			The gap size between the double arrow lines:
	///									zero - single arrow
	///									positive - double arrow with constant gap size
	///									negative - double arrow with changing gap size.
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
    virtual IMcErrors::ECode GetGapSize(
		float *pfGapSize,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

    //@}

	/// \name Slope presentation
	//@{

	//==============================================================================
	// Method Name: SetSlopePresentationColors()
	//------------------------------------------------------------------------------
	/// Sets slope presentation colors.
	///
	/// \param[in]	aColors[]		Array of colors along with their slope ranges
	/// \param[in]	uNumColors		Number of colors
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSlopePresentationColors(const SSlopePresentationColor aColors[], 
														UINT uNumColors) = 0;

	//==============================================================================
	// Method Name: GetSlopePresentationColors()
	//------------------------------------------------------------------------------
	/// Retrieves slope presentation colors.
	///
	/// \param[out]	paColors		Array of colors along with their slope ranges
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSlopePresentationColors(CMcDataArray<SSlopePresentationColor> *paColors) const = 0;

	//==============================================================================
	// Method Name: SetSlopeQueryPrecision()
	//------------------------------------------------------------------------------
	/// Defines the slope presentation query precision, as a shared or private property.
	///
	/// \param[in] eQueryPrecision			The slope presentation query precision.
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
	virtual IMcErrors::ECode SetSlopeQueryPrecision(
		IMcSpatialQueries::EQueryPrecision eQueryPrecision,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetSlopeQueryPrecision()
	//------------------------------------------------------------------------------
	/// Retrieves the slope presentation query precision property defined by SetXXX().
	///
	/// \param[out] peQueryPrecision	The slope presentation query precision.
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
	virtual IMcErrors::ECode GetSlopeQueryPrecision(
		IMcSpatialQueries::EQueryPrecision *peQueryPrecision,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
    // Method Name: SetShowSlopePresentation(...)
    //------------------------------------------------------------------------------
    /// Defines the show slope presentation flag, as a shared or private property.
	///
	/// If true, do show slope presentation.
	/// 
	/// Setting `true` or making the property private is allowed only if the item's subtype includes IMcObjectSchemeItem::EISTF_ATTACHED_TO_TERRAIN bit.
    ///
    /// \param[in] bShowSlopePresentation	Whether or not to show slope presentation.
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
	virtual IMcErrors::ECode SetShowSlopePresentation(
		bool bShowSlopePresentation,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
    // Method Name: GetShowSlopePresentation(...)
    //------------------------------------------------------------------------------
	/// Retrieves the show slope presentation flag property defined by SetXXX().
	///
	/// If true, do show slope presentation.
    ///
    /// \param[out] pbShowSlopePresentation	Whether or not to show slope presentation.
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
	virtual IMcErrors::ECode GetShowSlopePresentation(
		bool* pbShowSlopePresentation,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}
	/// \name Traversability presentation
	//@{

	//==============================================================================
    // Method Name: SetShowTraversabilityPresentation(...)
    //------------------------------------------------------------------------------
    /// Sets the show traversability presentation flag, as a shared or private property.
    /// 
	/// If true, do show the traversability presentation according to the traversability map layer defined by IMcObject::SetTraversabilityPresentationMapLayer()
    ///
    /// \param[in] bShowTraversabilityPresentation	Whether or not to show traversability presentation.
	///												The parameter's meaning depends on \a uPropertyID
	///												and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID						The private property ID or one of the following special values:
	///												IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe				The property version to be dealt with (index of object-state to be served by this property version).
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
	virtual IMcErrors::ECode SetShowTraversabilityPresentation(
		bool bShowTraversabilityPresentation,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
    // Method Name: GetShowTraversabilityPresentation(...)
    //------------------------------------------------------------------------------
	/// Retrieves the show traversability presentation flag property defined by SetXXX().
	///
	/// If true, the traversability presentation is shown.
    ///
    /// \param[out] pbShowTraversabilityPresentation	Whether or not to show traversability presentation.
	///													The parameter's meaning depends on \a puPropertyID
	///													and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID						The private property ID or one of the following special values:
	///													IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe					The property version to be dealt with (index of object-state to be served by this property version).
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
	virtual IMcErrors::ECode GetShowTraversabilityPresentation(
		bool* pbShowTraversabilityPresentation,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetTraversabilityColor()
	//------------------------------------------------------------------------------
	/// Sets a color for the specified traversability type as a shared or private property.
	///
    /// \param[in]	eTraversabilityType	The traversability type to set a color for; any type other than IMcSpatialQueries::EPT_NUM is applicable.
	/// \param[in]	Color				The color for the specified traversability type.
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in]	uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in]	uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
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
	virtual IMcErrors::ECode SetTraversabilityColor(
		IMcSpatialQueries::EPointTraversability eTraversabilityType, 
		const SMcBColor &Color,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetTraversabilityColor()
	//------------------------------------------------------------------------------
	/// Retrieves the traversability color property defined by SetTraversabilityColor() for the specified traversability type.
	///
    /// \param[in]	eTraversabilityType	The traversability type to retrieve a color for; any type other than IMcSpatialQueries::EPT_NUM is applicable.
	/// \param[out]	pColor				The color for the specified traversability type.
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out]	puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in]	uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
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
	virtual IMcErrors::ECode GetTraversabilityColor(
		IMcSpatialQueries::EPointTraversability eTraversabilityType, 
		SMcBColor *pColor,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}
};
