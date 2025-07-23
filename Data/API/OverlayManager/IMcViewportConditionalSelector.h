#pragma once
//==================================================================================
/// \file IMcViewportConditionalSelector.h
/// The interface for Viewport Conditional Selector -
/// A Conditional Selector based on the map viewport's definitions.
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "CMcDataArray.h"
#include "OverlayManager/IMcConditionalSelector.h"
#include "OverlayManager/IMcOverlayManager.h"

//==================================================================================
// Interface Name: IMcViewportConditionalSelector
//----------------------------------------------------------------------------------
/// The interface for Viewport Conditional Selector -
/// A Conditional Selector based on the map viewport's definitions.
//==================================================================================
class IMcViewportConditionalSelector : public virtual IMcConditionalSelector
{
protected:

	virtual ~IMcViewportConditionalSelector() {};

public:

    enum
    {
        //==============================================================================
        /// Conditional Selector unique ID for this interface
        //==============================================================================
        CONDITIONAL_SELECTOR_TYPE = 4
    };

public:

    //==============================================================================
    // Enum Name: EViewportTypeFlags
    //------------------------------------------------------------------------------
    /// Viewport Type bits
    //==============================================================================
    enum EViewportTypeFlags
    {
		/// none (any viewport type)
		EVT_NONE					= 0x0000,

		/// 2D regular viewport
        EVT_2D_REGULAR_VIEWPORT		= 0x0001, 

		/// 2D image viewport
        EVT_2D_IMAGE_VIEWPORT		= 0x0002, 

		/// 2D section viewport
        EVT_2D_SECTION_VIEWPORT		= 0x0004, 

		/// any 2D viewport (combines EVT_2D_REGULAR_VIEWPORT, EVT_2D_IMAGE_VIEWPORT and EVT_2D_SECTION_VIEWPORT)
        EVT_2D_VIEWPORT				= (EVT_2D_REGULAR_VIEWPORT | EVT_2D_IMAGE_VIEWPORT | EVT_2D_SECTION_VIEWPORT),

        /// 3D viewport
        EVT_3D_VIEWPORT				= 0x0010,  

		/// Any viewport type
		EVT_ALL_VIEWPORTS			= (EVT_2D_VIEWPORT | EVT_3D_VIEWPORT)
    };

    //==============================================================================
    // Enum Name: EViewportCoordinateSystem
    //------------------------------------------------------------------------------
    /// Viewport Coordinate System bits
    //==============================================================================
	enum EViewportCoordinateSystem 
	{
		/// GEO coordinate system
        EVCS_GEO_COORDINATE_SYSTEM		= 0x0001,
        /// UTM coordinate system
        EVCS_UTM_COORDINATE_SYSTEM		= 0x0002,
		/// All coordinate systems
		EVCS_ALL_COORDINATE_SYSTEMS		= (EVCS_GEO_COORDINATE_SYSTEM | EVCS_UTM_COORDINATE_SYSTEM)
	};

public:

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a viewport conditional selector.
	///
	/// The selector will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when the last of its object scheme nodes is destroyed.
	///
	/// \param[in]  pOverlayManager						The selector's overlay manager
	/// \param[out] ppSelector							The newly created selector
	/// \param[in] uViewportTypeBitField				The (optional) viewports types (based on #EViewportTypeFlags)
	/// \param[in] uViewportCoordinateSystemBitField	The (optional) viewports coordinate systems 
	///													(based on #EViewportCoordinateSystem)
	/// \param[in] uViewportsIDs						The (optional) list of specific viewports' IDs
	/// \param[in] uNumViewportsIDs						The number of specific viewports' IDs in the list
	/// \param[in] bIDsInclusive						Whether the list is inclusive or exclusive
	///													(the condition result is true for a viewport if 
	///													- \p bInclusive == true and viewport's ID is in the list or 
	///													- \p bInclusive == false and viewport's ID is not in the list)
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcOverlayManager *pOverlayManager,
		IMcViewportConditionalSelector **ppSelector,
		UINT uViewportTypeBitField = IMcViewportConditionalSelector::EVT_ALL_VIEWPORTS,
		UINT uViewportCoordinateSystemBitField = IMcViewportConditionalSelector::EVCS_ALL_COORDINATE_SYSTEMS,
		UINT uViewportsIDs[] = NULL,
		UINT uNumViewportsIDs = 0,
		bool bIDsInclusive = false);

	//@}

	/// \name Properties
	//@{

    //==============================================================================
    // Method Name: SetViewportTypeBitField(...)
    //------------------------------------------------------------------------------
    /// Sets bit mask to define viewports types.
    ///
    /// \param[in] uViewportTypeBitField    The viewports types (based on #EViewportTypeFlags)
    ///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode SetViewportTypeBitField(UINT uViewportTypeBitField) = 0;

    //==============================================================================
    // Method Name: GetViewportTypeBitField(...)
    //------------------------------------------------------------------------------
    /// Gets bit mask to define viewports types.
    ///
    /// \param[out] puViewportTypeBitField	The viewport types (based on #EViewportTypeFlags)
    ///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode GetViewportTypeBitField(UINT *puViewportTypeBitField) const = 0;

	//==============================================================================
	// Method Name: SetViewportCoordinateSystemBitField(...)
	//------------------------------------------------------------------------------
	/// Sets bit mask to define viewports coordinate systems.
	///
	/// \param[in] uViewportCoordinateSystemBitField	The viewports coordinate systems (based on #EViewportCoordinateSystem)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetViewportCoordinateSystemBitField(UINT uViewportCoordinateSystemBitField) = 0;

    //==============================================================================
    // Method Name: GetViewportCoordinateSystemBitField(...)
    //------------------------------------------------------------------------------
    /// Gets bit mask to define viewports coordinate systems
    ///
    /// \param[out] puViewportCoordinateSystemBitField    The viewports coordinate systems (based on #EViewportCoordinateSystem)
    ///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode GetViewportCoordinateSystemBitField(UINT *puViewportCoordinateSystemBitField) const = 0;

    //==============================================================================
    // Method Name: SetSpecificViewports(...)
    //------------------------------------------------------------------------------
    /// Sets inclusive or exclusive list of specific viewports' IDs.
    ///
	/// The condition result is true for a viewport if 
	///	- \a bIDsInclusive == true and viewport's ID is in the list or
	///	- \a bIDsInclusive == false and viewport's ID is not in the list
	///
	/// \param[in] auViewportsIDs		The list of viewports' IDs
	/// \param[in] uNumViewportsIDs		The number of viewports' IDs
	/// \param[in] bIDsInclusive		Whether the list is inclusive or exclusive
    ///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode SetSpecificViewports(const UINT auViewportsIDs[],
												  UINT uNumViewportsIDs, bool bIDsInclusive) = 0;

    //==============================================================================
    // Method Name: GetSpecificViewports(...)
    //------------------------------------------------------------------------------
	/// Sets inclusive or exclusive list of specific viewports' IDs defined by SetSpecificViewports().
    ///
    /// \param[out] pauViewportsIDs		The list of viewports' IDs
	/// \param[out] pbIDsInclusive		Whether the list is inclusive or exclusive
	///									(can be NULL if unnecessary)
    ///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode GetSpecificViewports(CMcDataArray<UINT> *pauViewportsIDs,
												  bool *pbIDsInclusive = NULL) const = 0;

	//@}
};
