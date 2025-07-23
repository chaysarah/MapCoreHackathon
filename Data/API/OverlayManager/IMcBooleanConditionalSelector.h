#pragma once
//==================================================================================
/// \file IMcBooleanConditionalSelector.h
/// The interface for Boolean Conditional Selector -
/// a conditional selector based on boolean operations on other conditional selectors.
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "CMcDataArray.h"
#include "OverlayManager/IMcConditionalSelector.h"
#include "OverlayManager/IMcOverlayManager.h"

//==================================================================================
// Interface Name: IMcBooleanConditionalSelector
//----------------------------------------------------------------------------------
/// The interface for Boolean Conditional Selector -
/// a conditional selector based on boolean operations on other conditional selectors.
/// A boolean conditional selector will perform an AND/OR/NOT criteria
/// of all the selectors specified as input.
/// 
/// \note In case of NOT operation the input list should contain
/// exactly one conditional selector
//==================================================================================
class IMcBooleanConditionalSelector : public virtual IMcConditionalSelector
{
protected:

	virtual ~IMcBooleanConditionalSelector() {};

public:

    enum
    {
        //==============================================================================
        /// Conditional Selector unique ID for this interface
        //==============================================================================
        CONDITIONAL_SELECTOR_TYPE = 1
    };

public:

	//==============================================================================
	// Enum Name: EBooleanOp
	//------------------------------------------------------------------------------
	/// Boolean Operator
	//==============================================================================
	enum EBooleanOp
    {
        EB_AND,
        EB_OR,
        EB_NOT
    };

public:

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a boolean conditional selector.
	///
	/// The selector will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when the last of its object scheme nodes is destroyed.
	///
	/// \param[in]  pOverlayManager	The selector's overlay manager
	/// \param[out] ppSelector		The newly created selector
	/// \param[in] ppSelectorList	The list of Selectors to AND/OR/NOT
	/// \param[in] uNumSelectors	The number of elements in the previous list
	/// \param[in] eOperation		The boolean operation to use
	///
	/// \return
	///     - status result
	///     - Error in case a not operation doesn't have a single input
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcOverlayManager *pOverlayManager,
		IMcBooleanConditionalSelector **ppSelector,
		IMcConditionalSelector* const ppSelectorList[],
		UINT uNumSelectors,
		EBooleanOp eOperation);

	//@}

	/// \name Properties
	//@{

    //==============================================================================
    // Method Name: SetListOfSelectors(...)
    //------------------------------------------------------------------------------
    /// Sets the list of selectors.
    ///
    /// \param[in] ppSelectorList	The list of selectors to AND/OR/NOT
    /// \param[in] uNumSelectors	The number of elements in the previous list
    ///
    /// \return
    ///     - status result
    ///     - Error in case a not operation doesn't have a single input
    //==============================================================================
	virtual IMcErrors::ECode SetListOfSelectors(
		IMcConditionalSelector* const ppSelectorList[],
		UINT uNumSelectors) = 0;

    //==============================================================================
    // Method Name: GetListOfSelectors(...)
    //------------------------------------------------------------------------------
    /// Gets the list of selectors.
    ///
    /// \param[out] papSelectors	The list of selectors to AND/OR/NOT
    ///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode GetListOfSelectors(
		CMcDataArray<IMcConditionalSelector*> *papSelectors) const = 0;

	//==============================================================================
	// Method Name: SetBooleanOperation(...)
	//------------------------------------------------------------------------------
	/// Sets the boolean operation to use.
	///
	/// \param[in] eOperation	The boolean operation to use
	///
	/// \return
	///     - status result
	///     - Error in case a not operation doesn;t have a single input
	//==============================================================================
	virtual IMcErrors::ECode SetBooleanOperation (EBooleanOp eOperation) = 0;

	//==============================================================================
	// Method Name: GetBooleanOperation(...)
	//------------------------------------------------------------------------------
	/// Gets the boolean operation to use.
	///
	/// \param[out] eOperation	The boolean operation to use
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetBooleanOperation(EBooleanOp *eOperation) const = 0;

	//@}
};


