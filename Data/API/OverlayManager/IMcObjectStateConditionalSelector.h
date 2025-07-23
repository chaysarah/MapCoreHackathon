#pragma once
//==================================================================================
/// \file IMcObjectStateConditionalSelector.h
/// The interface for Object's State Conditional Selector -
/// a conditional selector based on object's state.
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "OverlayManager/IMcConditionalSelector.h"
#include "OverlayManager/IMcOverlayManager.h"

//==================================================================================
// Interface Name: IMcObjectStateConditionalSelector
//----------------------------------------------------------------------------------
/// The interface for Object's State Conditional Selector -
/// a conditional selector based on object's state.
//==================================================================================
class IMcObjectStateConditionalSelector : public virtual IMcConditionalSelector
{
protected:

	virtual ~IMcObjectStateConditionalSelector() {};

public:

    enum
    {
        //==============================================================================
        /// Conditional Selector unique ID for this interface
        //==============================================================================
        CONDITIONAL_SELECTOR_TYPE = 5
    };

public:

    /// \name Create 
    //@{

    //==============================================================================
    // Method Name: Create(...)
    //------------------------------------------------------------------------------
    /// Creates an object-state conditional selector.
    /// 
	/// \param[in]  pOverlayManager	The selector's overlay manager
    /// \param[out] ppSelector		The newly created selector
	/// \param[in]  uObjectState	The object-state for the selector to check against
    ///
    /// \return
    ///     - status result
    //==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcOverlayManager *pOverlayManager,
		IMcObjectStateConditionalSelector **ppSelector,
		BYTE uObjectState = 1);

	//@}

	/// \name Object State
	//@{

	//==============================================================================
	// Method Name: GetObjectState(...)
	//------------------------------------------------------------------------------
	/// Sets the object-state for the selector to check against
	/// 
	/// \param[in]	uObjectState	The object-state for the selector to check against
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectState(BYTE uObjectState) = 0;

	//==============================================================================
	// Method Name: GetObjectState(...)
	//------------------------------------------------------------------------------
	/// Retrieves the object-state for the selector to check against
	/// 
	/// \param[out]	puObjectState	The object-state for the selector to check against
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectState(BYTE *puObjectState) const = 0;

	//@}
};
