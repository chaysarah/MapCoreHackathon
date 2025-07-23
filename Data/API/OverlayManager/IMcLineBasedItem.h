#pragma once
//==================================================================================
/// \file IMcLineBasedItem.h
/// Base interface for line based item
//==================================================================================

#include "McExports.h"
#include "McCommonTypes.h"
#include "OverlayManager/IMcSymbolicItem.h"
#include "OverlayManager/IMcSightPresentationItemParams.h"

//==================================================================================
// Interface Name: IMcLineBasedItem
//----------------------------------------------------------------------------------
///	Base interface for line based item.
/// 
//==================================================================================
class IMcLineBasedItem : public virtual IMcSymbolicItem,
						 public virtual IMcSightPresentationItemParams
{
protected:

	virtual ~IMcLineBasedItem() {};

public:

	//==============================================================================
	// Enum Name: ELineStyle
	//------------------------------------------------------------------------------
	/// Line style
	//==============================================================================
	enum ELineStyle
	{
		ELS_SOLID,			///< Solid line
		ELS_DASH,			///< Dash line
		ELS_DOT,			///< Dotted line
		ELS_DASH_DOT,		///< Dash and dots line
		ELS_DASH_DOT_DOT,	///< Dash and double dots line
		ELS_TEXTURE,		///< Use texture instead of the line style and color
		ELS_NO_LINE			///< No line
	};

	//==============================================================================
	// Enum Name: EPointOrderReverseMode
	//------------------------------------------------------------------------------
	/// The mode defining when item's point order should be reversed
	//==============================================================================
	enum EPointOrderReverseMode
	{
		EPORM_NONE,							///< No reversing
		EPORM_REVERSE_ALWAYS,				///< Reverse always
		EPORM_REVERSE_IF_CLOCKWISE,			///< Reverse for clockwise polygon items
		EPORM_REVERSE_IF_COUNTER_CLOCKWISE 	///< Reverse for counter-clockwise polygon items
	};

	//==============================================================================
	// Enum Name: EFillStyle
	//------------------------------------------------------------------------------
	/// Fill style
	//==============================================================================
	enum EFillStyle
	{
		EFS_HORIZONTAL,	///< Horizontal hatch	  ----- style
		EFS_VERTICAL,	///< Vertical hatch		  ||||| style
		EFS_FDIAGONAL,	///< FDiagonal hatch	  \\\\\ style
		EFS_BDIAGONAL,	///< BDiagonal hatch	  ///// style
		EFS_CROSS,		///< Cross hatch		  +++++ style
		EFS_DIAGCROSS,	///< Diagonal cross hatch xxxxx style
		EFS_SOLID,		///< Solid fill
		EFS_TEXTURE,	///< Texture fill
		EFS_NONE		///< No fill
	};

	//==============================================================================
	// Enum Name: EShapeType
	//------------------------------------------------------------------------------
	/// Shape type; the default is #EST_2D
	//==============================================================================
	enum EShapeType
	{
		/// 2D shape 
		///  (vertical height and side fill properties are ignored)
		EST_2D,

		/// 3D shape built by extruding the underlying 2D shape 
		///  (vertical height and side fill properties are used)
		EST_3D_EXTRUSION
	};

	/// slope-presentation color
	struct SSlopePresentationColor
	{
		float fMaxSlope;	///< maximum slope angle (between -90 and +90)
		SMcBColor Color;	///< color for a line segments of slopes between the maximum slope of 
							///<  the previous color (exclusively) and that of this color (inclusively
	};

public:

	/// \name Shape Type
	//@{

	//==============================================================================
	// Method Name: SetShapeType(...)
	//------------------------------------------------------------------------------
	/// Sets the shape type (2D, 3D etc)
	///
	/// \param[in] eShapeType				The shape type; the default is #EST_2D
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetShapeType(EShapeType eShapeType) = 0;

	//==============================================================================
	// Method Name: GetShapeType(...)
	//------------------------------------------------------------------------------
	/// Retrieves the shape type (2D, 3D etc)
	///
	/// \param[out] peShapeType				The shape type; the default is #EST_2D
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetShapeType(EShapeType *peShapeType) const = 0;

	//@}

	/// \name Line Style
	//@{

    //==============================================================================
    // Method Name: SetLineStyle(...)
    //------------------------------------------------------------------------------
    /// Defines the line style as a shared or private property.
    ///
    /// \param[in] LineStyle			The line style.
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
    virtual IMcErrors::ECode SetLineStyle(
		IMcLineBasedItem::ELineStyle LineStyle,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetLineStyle(...)
    //------------------------------------------------------------------------------
    /// Retrieves the line style property defined by SetXXX().
    ///
    /// \param[out] pLineStyle			The line style.
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
	virtual IMcErrors::ECode GetLineStyle(
		IMcLineBasedItem::ELineStyle *pLineStyle,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Line Color
	//@{

	//==============================================================================
    // Method Name: SetLineColor(...)
    //------------------------------------------------------------------------------
    /// Defines the line color as a shared or private property.
    ///
    /// \param[in] LineColor			The line color.
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
	virtual IMcErrors::ECode SetLineColor(
		const SMcBColor &LineColor,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetLineColor(...)
    //------------------------------------------------------------------------------
    /// Retrieves the line color property defined by SetXXX().
    ///
    /// \param[out] pLineColor			The line color.
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
	virtual IMcErrors::ECode GetLineColor(
		SMcBColor *pLineColor,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

    //@}

	/// \name Outline Color
	//@{

	//==============================================================================
	// Method Name: SetLineColor(...)
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

	/// \name Line Width
	//@{

	//==============================================================================
    // Method Name: SetLineWidth(...)
    //------------------------------------------------------------------------------
    /// Defines the line width as a shared or private property.
    ///
    /// \param[in] fLineWidth			The line width.
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
	virtual IMcErrors::ECode SetLineWidth(
		float fLineWidth,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetLineWidth(...)
    //------------------------------------------------------------------------------
    /// Retrieves the line width property defined by SetXXX().
    ///
    /// \param[out] pfLineWidth			The line width.
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
	virtual IMcErrors::ECode GetLineWidth(
		float *pfLineWidth,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Outline Width
	//@{

	//==============================================================================
	// Method Name: SetOutlineWidth(...)
	//------------------------------------------------------------------------------
	/// Defines the outline width as a shared or private property.
	///
	/// \param[in] fOutlineWidth		The outline width.
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
	virtual IMcErrors::ECode SetOutlineWidth(
		float fOutlineWidth,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetOutlineWidth(...)
	//------------------------------------------------------------------------------
	/// Retrieves the outline width property defined by SetXXX().
	///
	/// \param[out] pfOutlineWidth		The outline width.
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
	virtual IMcErrors::ECode GetOutlineWidth(
		float *pfOutlineWidth,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Line Texture
	//@{

    //==============================================================================
    // Method Name: SetLineTexture(...)
    //------------------------------------------------------------------------------
    /// Defines the line texture resource as a shared or private property.
	/// (To be used only when LineStyle is ELS_TEXTURE, ignored otherwise).
    ///
    /// \param[in] pLineTexture			The line texture resource.
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
	virtual IMcErrors::ECode SetLineTexture(
		IMcTexture *pLineTexture,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetLineTexture(...)
    //------------------------------------------------------------------------------
    /// Retrieves the line texture resource property defined by SetXXX().
    ///
    /// \param[out] ppLineTexture		The line texture resource.
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
	virtual IMcErrors::ECode GetLineTexture(
		IMcTexture **ppLineTexture,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetLineTextureHeightRange()
	//------------------------------------------------------------------------------
	/// Defines the line texture height range as a shared or private property
	/// (to be used only when LineStyle is ELS_TEXTURE, ignored otherwise).
	///
	/// Allows to use one texture for several types of lines, each line uses one or several 
	/// rows of the texture. Building one texture for for several types of lines improves 
	/// performance significantly.
	///
	/// \param[in] LineTextureHeightRange	The line texture height range: \a x means the first row index,
	///										\a y means the last row index (zero-based). To use the whole 
	///										texture, set \a x > \a y, e.g. the default value is (0, -1).
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
	virtual IMcErrors::ECode SetLineTextureHeightRange(
		const SMcFVector2D &LineTextureHeightRange,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetLineTextureHeightRange()
	//------------------------------------------------------------------------------
	/// Retrieves the line texture height range property defined by SetXXX().
	///
	/// \param[out] pLineTextureHeightRange		The line texture height range.
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
	virtual IMcErrors::ECode GetLineTextureHeightRange(
		SMcFVector2D *pLineTextureHeightRange,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

    //==============================================================================
    // Method Name: SetLineTextureScale()
    //------------------------------------------------------------------------------
    /// Defines the line texture scale as a shared or private property
	/// (to be used only when LineStyle is ELS_TEXTURE, ignored otherwise).
    ///
    /// \param[in] fLineTextureScale	The line texture scale.
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
    virtual IMcErrors::ECode SetLineTextureScale(
		float fLineTextureScale,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetLineTextureScale()
    //------------------------------------------------------------------------------
    /// Retrieves the line texture scale property defined by SetXXX().
    ///
    /// \param[out] pfLineTextureScale	The line texture scale.
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
    virtual IMcErrors::ECode GetLineTextureScale(
		float *pfLineTextureScale,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

    //==============================================================================
    // Method Name: SetPointOrderReverseMode(...)
    //------------------------------------------------------------------------------
    /// Defines the point order reverse mode as a shared or private property.
    ///
    /// \param[in] ePointOrderReverseMode	The point order reverse mode; the default is #EPORM_NONE.
	///										The parameter's meaning depends on \a uPropertyID
	///										and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID				The private property ID or one of the following special values:
	///										IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe		The property version to be dealt with (index of object-state to be served by this property version).
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
	virtual IMcErrors::ECode SetPointOrderReverseMode(
		EPointOrderReverseMode ePointOrderReverseMode,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetPointOrderReverseMode(...)
    //------------------------------------------------------------------------------
    /// Retrieves the point order reverse mode property defined by SetXXX().
    ///
    /// \param[out] pePointOrderReverseMode	The point order reverse mode; the default is #EPORM_NONE.
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
	virtual IMcErrors::ECode GetPointOrderReverseMode(
		EPointOrderReverseMode *pePointOrderReverseMode,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

    //@}

	/// \name 3D Geometric Parameters (Ignored in Shape Type EST_2D)
	//@{

	//==============================================================================
	// Method Name: SetVerticalHeight(...)
	//------------------------------------------------------------------------------
	/// Defines the item's vertical height as a shared or private property.
	///
	/// \param[in] fVerticalHeight		The item's vertical height.
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
	virtual IMcErrors::ECode SetVerticalHeight(
		float fVerticalHeight,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetVerticalHeight(...)
	//------------------------------------------------------------------------------
	/// Retrieves the item's vertical height property defined by SetXXX().
	///
	/// \param[out] pfVerticalHeight	The item's vertical height.
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
	virtual IMcErrors::ECode GetVerticalHeight(
		float *pfVerticalHeight,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Sides' Fill style (Ignored in Shape Type EST_2D)
	//@{

	//==============================================================================
	// Method Name: SetSidesFillStyle(...)
	//------------------------------------------------------------------------------
	/// Defines the sides' fill style as a shared or private property.
	///
	/// \param[in] eSidesFillStyle		The sides' fill style.
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
	virtual IMcErrors::ECode SetSidesFillStyle(
		EFillStyle eSidesFillStyle,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetSidesFillStyle(...)
	//------------------------------------------------------------------------------
	/// Retrieves the sides' fill style property defined by SetXXX().
	///
	/// \param[out] pSidesFillStyle		The sides' fill style.
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
	virtual IMcErrors::ECode GetSidesFillStyle(
		EFillStyle *pSidesFillStyle,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Sides' Fill Color (Ignored in Shape Type EST_2D)
	//@{

	//==============================================================================
	// Method Name: SetSidesFillColor(...)
	//------------------------------------------------------------------------------
	/// Defines the sides' fill color as a shared or private property.
	///
	/// \param[in] SidesFillColor		The sides' fill color.
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
	virtual IMcErrors::ECode SetSidesFillColor(
		const SMcBColor &SidesFillColor,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetSidesFillColor(...)
	//------------------------------------------------------------------------------
	/// Retrieves the sides' fill color property defined by SetXXX().
	///
	/// \param[out] pSidesFillColor		The sides' fill color.
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
	virtual IMcErrors::ECode GetSidesFillColor(
		SMcBColor *pSidesFillColor,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Sides' Fill Texture (Ignored in Shape Type EST_2D)
	//@{

	//==============================================================================
	// Method Name: SetSidesFillTexture(...)
	//------------------------------------------------------------------------------
	/// Defines the sides' fill texture resource as a shared or private property
	/// (to be used only when SidesFillStyle is EFS_TEXTURE, ignored otherwise).
	///
	/// \param[in] pSidesFillTexture	The sides' fill texture resource.
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
	virtual IMcErrors::ECode SetSidesFillTexture(
		IMcTexture *pSidesFillTexture,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetSidesFillTexture(...)
	//------------------------------------------------------------------------------
	/// Retrieves the sides' fill texture resource property defined by SetXXX().
	///
	/// \param[out] ppSidesFillTexture	The sides' fill texture resource.
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
	virtual IMcErrors::ECode GetSidesFillTexture(
		IMcTexture **ppSidesFillTexture,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Sides' Fill Texture Scale (Ignored in Shape Type EST_2D)
	//@{

	//==============================================================================
	// Method Name: SetSidesFillTextureScale()
	//------------------------------------------------------------------------------
	/// Defines the sides' fill texture scale as a shared or private property
	/// (to be used only when SidesFillStyle is EFS_TEXTURE, ignored otherwise).
	///
	/// \param[in] SidesFillTextureScale	The sides' fill texture scale.
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
	virtual IMcErrors::ECode SetSidesFillTextureScale(
		const SMcFVector2D &SidesFillTextureScale,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetSidesFillTextureScale()
	//------------------------------------------------------------------------------
	/// Retrieves the sides' fill texture scale property defined by SetXXX().
	///
	/// \param[out] pSidesFillTextureScale	The sides' fill texture scale.
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
	virtual IMcErrors::ECode GetSidesFillTextureScale(
		SMcFVector2D *pSidesFillTextureScale,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Great Circle
	//@{
	//================================================================
	// Method Name: SetGreatCirclePrecision(...)
	//----------------------------------------------------------------
	/// Sets the optional precision of line sampling as a great circle (geodetic line).
	///
	/// \param[in] fGreatCirclePrecision	The maximal error in meters allowed between the 
	///										rendered polyline and the great circle; zero 
	///										(the default) means straight geometric lines; 
	///										nonzero values smaller than 1 considered as equal to 1.
	///
	/// \note Applicable to items consisting of straight line segments and to rays in calculation 
	///		  of sight presentation ellipses; ignored in arcs and outlines of ellipses.
	///
	/// \return
	///     - Status result
	//================================================================
	virtual IMcErrors::ECode SetGreatCirclePrecision(float fGreatCirclePrecision) = 0;

	//================================================================
	// Method Name: GetGreatCirclePrecision(...)
	//----------------------------------------------------------------
	/// Retrieves the optional precision of line sampling as a great circle (geodetic line).
	///
	/// \param[out] pfGreatCirclePrecision	The maximal error in meters allowed between the 
	///										rendered polyline and the great circle;
	///										zero (the default) means straight geometric lines.
	/// \return
	///     - Status result
	//================================================================
	virtual IMcErrors::ECode GetGreatCirclePrecision(float *pfGreatCirclePrecision) const = 0;
	//@}

	/// \name Smoothing
	//@{
	//================================================================
	// Method Name: SetNumSmoothingLevels(...)
	//----------------------------------------------------------------
	/// Defines the number of smoothing levels as a shared or private property.
	/// (It is an optional number for smoothing line by sampling a smooth curve
	///	passing through lines' original points).
	///
	/// \param[in]	uNumSmoothingLevels		Number of times to halve each line segment for smoothing,
	///										new number of segments will be greater by 2 in power of 
	///										\a uNumSmoothingLevels; 
	///										zero (the default) means original line without smoothing;
	///										values greater than 5 considered as equal to 5.
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
	/// \note Applicable to items consisting of straight line segments; ignored in arcs
	///		  outlines of ellipses and great-circle lines.
	///
	/// \return
	///     - Status result
	//================================================================
	virtual IMcErrors::ECode SetNumSmoothingLevels(BYTE uNumSmoothingLevels,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//================================================================
	// Method Name: GetNumSmoothingLevels(...)
	//----------------------------------------------------------------
	/// Retrieves the number of smoothing levels property defined by SetXXX().
	/// (It is an optional number for smoothing line by sampling a smooth curve
	///	passing through lines' original points).
	///
	/// \param[out]	puNumSmoothingLevels	Number of times to halve each line segment for smoothing; 
	///										zero (the default) means original line without smoothing.
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
	//================================================================
	virtual IMcErrors::ECode GetNumSmoothingLevels(
		BYTE *puNumSmoothingLevels,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;
	//@}

	/// \name Cutting Items
	//@{

	//==============================================================================
	// Method Name: SetClippingItems()
	//------------------------------------------------------------------------------
	/// Sets clipping the item by other items (picture or text items only) regardless of 
	/// draw priority.
	///
	/// \param[in]	apClippingItems		The array of other items (picture or text items only) 
	///									there rectangles should clip this item.
	/// \param[in]	uNumClippingItems	The number of clipping items in the array.
	/// \param[in]	bSelfClippingOnly	Whether the clipping items should clip this item only 
	///									or also other clipped items of the same draw priority (if there 
	///									are intersections between them). Using true can significantly 
	///									affect performance but prevents undesirable clippings 
	///									of other clipped items if there are intersections between them.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetClippingItems(IMcObjectSchemeItem *const apClippingItems[], 
		UINT uNumClippingItems, bool bSelfClippingOnly = false) = 0;

	//==============================================================================
	// Method Name: GetClippingItems()
	//------------------------------------------------------------------------------
	/// Retrieves the clipping items set by SetClippingItems().
	///
	/// \param[out]	papClippingItems		The array of other items (picture or text items only) 
	///									there rectangles should clip this item.
	/// \param[out]	pbSelfClippingOnly	Whether the clipping items should clip this item only 
	///									or also other clipped items of the same draw priority (if there 
	///									are intersections between them).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetClippingItems(CMcDataArray<IMcObjectSchemeItem*> *papClippingItems, 
		bool *pbSelfClippingOnly) const = 0;
	//@}
};

//==================================================================================
// Interface Name: IMcLineItem
//----------------------------------------------------------------------------------
///	Interface for line items.
///
//==================================================================================
class IMcLineItem : public virtual IMcLineBasedItem
{
protected:

	virtual ~IMcLineItem() {};

public:

    enum
    {
        //==============================================================================
        /// Node unique ID for this interface
        //==============================================================================
        NODE_TYPE = 60
    };

public:

    /// \name Create
    //@{

    //==============================================================================
    // Method Name: Create(...)
    //------------------------------------------------------------------------------
    /// Create a poly-line item.
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
		IMcLineItem **ppItem,
		UINT uItemSubTypeBitField,
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
		IMcLineItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

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

//==================================================================================
// Interface Name: IMcClosedShapeItem
//----------------------------------------------------------------------------------
///	Base interface for closed shaped items.
/// 
//==================================================================================
class IMcClosedShapeItem : public virtual IMcLineBasedItem
{
protected:

	virtual ~IMcClosedShapeItem() {};

public:

    /// \name Fill style
	//@{

    //==============================================================================
    // Method Name: SetFillStyle(...)
    //------------------------------------------------------------------------------
    /// Defines the fill style as a shared or private property.
    ///
    /// \param[in] eFillStyle			The fill style.
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
    virtual IMcErrors::ECode SetFillStyle(
		EFillStyle eFillStyle,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetFillStyle(...)
    //------------------------------------------------------------------------------
    /// Retrieves the fill style property defined by SetXXX().
    ///
    /// \param[out] pFillStyle			The fill style.
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
	virtual IMcErrors::ECode GetFillStyle(
		EFillStyle *pFillStyle,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Fill Color
	//@{

    //==============================================================================
    // Method Name: SetFillColor(...)
    //------------------------------------------------------------------------------
    /// Defines the fill color as a shared or private property.
    ///
    /// \param[in] FillColor			The fill color.
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
	virtual IMcErrors::ECode SetFillColor(
		const SMcBColor &FillColor,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetFillColor(...)
    //------------------------------------------------------------------------------
    /// Retrieves the fill color property defined by SetXXX().
    ///
    /// \param[out] pFillColor			The fill color.
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
	virtual IMcErrors::ECode GetFillColor(
		SMcBColor *pFillColor,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Fill Texture
	//@{

    //==============================================================================
    // Method Name: SetFillTexture(...)
    //------------------------------------------------------------------------------
    /// Defines the fill texture resource as a shared or private property
	/// (to be used only when FillStyle is EFS_TEXTURE, ignored otherwise).
    ///
    /// \param[in] pFillTexture			The fill texture resource.
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
	virtual IMcErrors::ECode SetFillTexture(
		IMcTexture *pFillTexture,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetFillTexture(...)
    //------------------------------------------------------------------------------
    /// Retrieves the fill texture resource property defined by SetXXX().
    ///
    /// \param[out] ppFillTexture	The fill texture resource.
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
	virtual IMcErrors::ECode GetFillTexture(
		IMcTexture **ppFillTexture,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

    /// \name Fill Texture Scale
    //@{

    //==============================================================================
    // Method Name: SetFillTextureScale()
    //------------------------------------------------------------------------------
    /// Defines the fill texture scale as a shared or private property
	/// (to be used only when FillStyle is EFS_TEXTURE, ignored otherwise).
    ///
    /// \param[in] FillTextureScale		The fill texture scale.
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
    virtual IMcErrors::ECode SetFillTextureScale(
        const SMcFVector2D &FillTextureScale,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

    //==============================================================================
    // Method Name: GetFillTextureScale()
    //------------------------------------------------------------------------------
    /// Retrieves the fill texture scale property defined by SetXXX().
    ///
    /// \param[out] pFillTextureScale	The fill texture scale.
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
    virtual IMcErrors::ECode GetFillTextureScale(
		SMcFVector2D *pFillTextureScale,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

    //@}   
};
