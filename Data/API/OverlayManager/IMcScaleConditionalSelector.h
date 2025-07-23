#pragma once
//==================================================================================
/// \file IMcScaleConditionalSelector.h
/// The interface for Scale Conditional Selector -
/// a conditional selector based on the current map scale.
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "OverlayManager/IMcConditionalSelector.h"
#include "OverlayManager/IMcOverlayManager.h"

//==================================================================================
// Interface Name: IMcScaleConditionalSelector
//----------------------------------------------------------------------------------
/// The interface for Scale Conditional Selector -
/// a conditional selector based on the current map scale.
//==================================================================================
class IMcScaleConditionalSelector : public virtual IMcConditionalSelector
{
protected:

	virtual ~IMcScaleConditionalSelector() {};

public:

    enum
    {
        //==============================================================================
        /// Conditional Selector unique ID for this interface
        //==============================================================================
        CONDITIONAL_SELECTOR_TYPE = 2
    };

public:

	/// \name Create 
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a scale conditional selector.
	/// 
	/// The selector will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when the last of its object scheme nodes is destroyed.
	///
	/// \param[in]  pOverlayManager				The selector's overlay manager
	/// \param[out] ppSelector					The newly created selector
	/// \param[in] fMinScale					The minimum scale
	/// \param[in] fMaxScale					The maximum scale
	/// \param[in] uCancelScaleMode				The user defined bit mask to define in which 
	///											modes scale ranges are ignored
	/// \param[in] uCancelScaleModeResult		If the mode bit is set on \a uCancelScaleMode 
	///                                         then this bit specifies whether the
	///											condition result is true or false
	///
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcOverlayManager *pOverlayManager,
		IMcScaleConditionalSelector **ppSelector,
		float fMinScale,
		float fMaxScale,
		UINT uCancelScaleMode,
		UINT uCancelScaleModeResult);

	//@}

	/// \name Properties
	//@{

    //==============================================================================
    // Method Name: SetMinScale(...)
    //------------------------------------------------------------------------------
    /// Sets the min scale.
    ///
    /// \param[in] fMinScale	The minimum scale
    ///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode SetMinScale(float fMinScale) = 0;

    //==============================================================================
    // Method Name: GetMinScale(...)
    //------------------------------------------------------------------------------
    /// Gets the min scale.
    ///
    /// \param[out] pfMinScale	The minimum scale
    ///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode GetMinScale(float *pfMinScale) const = 0;

    //==============================================================================
    // Method Name: SetMaxScale(...)
    //------------------------------------------------------------------------------
    /// Sets the max scale.
    ///
    /// \param[in] fMaxScale	The maximum scale
    ///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode SetMaxScale(float fMaxScale) = 0;

    //==============================================================================
    // Method Name: GetMaxScale(...)
    //------------------------------------------------------------------------------
    /// Gets the max scale.
    ///
    /// \param[out] pfMaxScale	The maximum scale
    ///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode GetMaxScale(float *pfMaxScale) const = 0;

    //==============================================================================
    // Method Name: SetCancelScaleMode(...)
    //------------------------------------------------------------------------------
    /// Sets bit mask to define in which modes scale ranges are ignored.
    ///
    /// \param[in] uCancelScaleMode		The user defined bit mask to define in which 
    ///                                 modes scale ranges are ignored
    ///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode SetCancelScaleMode(UINT uCancelScaleMode) = 0;

    //==============================================================================
    // Method Name: GetCancelScaleMode(...)
    //------------------------------------------------------------------------------
    /// Gets bit mask to define in which modes scale ranges are ignored.
    ///
    /// \param[out] puCancelScaleMode	The user defined bit mask to define in which 
    ///                                 modes scale ranges are ignored
    ///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode GetCancelScaleMode(UINT *puCancelScaleMode) const = 0;

    //==============================================================================
    // Method Name: SetCancelScaleModeResult(...)
    //------------------------------------------------------------------------------
	/// Sets bit mask to define whether the condition result is true or false,
	/// in case the mode bit is set on CancelScaleMode.
    ///
    /// \param[in] uCancelScaleModeResult   If the mode bit is set on CancelScaleMode 
    ///                                     then this bit specifies whether the 
    ///                                     condition result is true or false
    ///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode SetCancelScaleModeResult(UINT uCancelScaleModeResult) = 0;

    //==============================================================================
    // Method Name: GetCancelScaleModeResult(...)
    //------------------------------------------------------------------------------
	/// Gets bit mask to define whether the condition result is true or false,
	/// in case the mode bit is set on CancelScaleMode.
    ///
	/// \param[out] puCancelScaleModeResult	If the mode bit is set on CancelScaleMode 
	///                                     then this bit specifies whether the 
	///                                     condition result is true or false
    ///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode GetCancelScaleModeResult(UINT *puCancelScaleModeResult) const = 0;

	//@}
};
