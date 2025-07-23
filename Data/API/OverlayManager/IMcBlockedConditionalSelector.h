#pragma once
//==================================================================================
/// \file IMcBlockedConditionalSelector.h
/// The interface for Blocked Conditional Selector -
/// a conditional selector based on whether item is blocked by the terrain in the current view.
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "OverlayManager/IMcConditionalSelector.h"
#include "OverlayManager/IMcOverlayManager.h"

//==================================================================================
// Interface Name: IMcBlockedConditionalSelector
//----------------------------------------------------------------------------------
/// The interface for Blocked Conditional Selector -
/// a conditional selector based on whether item is blocked by the terrain in the current view.
//==================================================================================
class IMcBlockedConditionalSelector : public virtual IMcConditionalSelector
{
protected:

	virtual ~IMcBlockedConditionalSelector() {};

public:

    enum
    {
        //==============================================================================
        /// Conditional Selector unique ID for this interface
        //==============================================================================
        CONDITIONAL_SELECTOR_TYPE = 3
    };

public:

	/// \name Create
	//@{

    //==============================================================================
    // Method Name: Create(...)
    //------------------------------------------------------------------------------
    /// Creates a blocked conditional selector.
    /// 
	/// The selector will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when the last of its object scheme nodes is destroyed.
	///
	/// \param[in]  pOverlayManager		The selector's overlay manager
    /// \param[out] ppSelector			The newly created selector
    ///
    /// \return
    ///     - status result
    //==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcOverlayManager *pOverlayManager,
		IMcBlockedConditionalSelector **ppSelector);

	//@}
};
