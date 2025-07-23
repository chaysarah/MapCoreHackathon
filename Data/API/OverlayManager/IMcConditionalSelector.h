#pragma once
//==================================================================================
/// \file IMcConditionalSelector.h
/// The base interface for conditional selector (a condition that can be attached to several overlays, objects, object scheme nodes and scheme's state modifiers).
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcBase.h"

class IMcBooleanConditionalSelector;
class IMcBlockedConditionalSelector;
class IMcScaleConditionalSelector;
class IMcObjectStateConditionalSelector;
class IMcViewportConditionalSelector;
class IMcLocationConditionalSelector;
class IMcOverlayManager;

//==================================================================================
// Interface Name: IMcConditionalSelector
//----------------------------------------------------------------------------------
/// The base interface for conditional selector (a condition that can be attached to several overlays, objects, object scheme nodes and scheme's state modifiers).
/// 
/// The condition result affects their visibility, transform or (in case of state modifiers) object's state.
//==================================================================================
class IMcConditionalSelector : public virtual IMcBase
{
protected:

	virtual ~IMcConditionalSelector() {};

public:

	//==============================================================================
	// Enum Name: EActionType
	//------------------------------------------------------------------------------
	/// Types of actions controlled by conditional selectors.
	//==============================================================================
	enum EActionType
	{
		EAT_ACTIVITY,	///< No longer in use.
		EAT_VISIBILITY, ///< Controlling the visibility of an object scheme node / object / overlay.
		EAT_TRANSFORM,	///< Controlling whether symbolic item's transforms defined in IMcSymbolicItem are performed.
		EAT_NUM			///< The number of the enum's members (not to be used as a valid action type).
	};

	//==============================================================================
	// Enum Name: EActionOptions
	//------------------------------------------------------------------------------
	/// Options to control performing actions of conditional selectors
	//==============================================================================
	enum EActionOptions 
	{
		EAO_FORCE_FALSE,	///< Do not perform the action regardless of a selector attached.
		EAO_FORCE_TRUE,		///< Perform the action regardless of a selector attached.
		EAO_USE_SELECTOR	///< Perform the action according to the result of a selector attached (the default).
	};

	/// \name Overlay Manager
	//@{

	//==============================================================================
	// Method Name: GetOverlayManager(...)
	//------------------------------------------------------------------------------
	/// Retrieves the overlay manager that the selector belongs to
	/// 
	/// \param[out]	ppOverlayManager	The overlay manager
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetOverlayManager(IMcOverlayManager **ppOverlayManager) const = 0;

	//@}
	/// \name ID, Name and Type
	//@{

	//==============================================================================
	// Method Name: SetID()
	//------------------------------------------------------------------------------
	/// Sets the selector's user-defined unique ID that allows retrieving the selector by IMcOverlayManager::GetConditionalSelectorByID().
	///
	/// \param[in]	uID		The selector's ID to be set (specify ID not equal to #MC_EMPTY_ID to set ID,
	///						equal to #MC_EMPTY_ID to remove ID).
	/// \note
	/// The ID should be unique in the selector's overlay manager, otherwise the function returns IMcErrors::ID_ALREADY_EXISTS
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetID(UINT uID) = 0;

	//==============================================================================
	// Method Name: GetID()
	//------------------------------------------------------------------------------
	/// Retrieves the selector'e user-defined unique ID set by selector().
	///        
	/// \param [out] puID	The selector ID (or #MC_EMPTY_ID if the ID is not set).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetID(UINT *puID) const = 0;

	//==============================================================================
	// Method Name: SetName()
	//------------------------------------------------------------------------------
	/// Sets the selector's user-defined unique name that allows retrieving the selector by IMcOverlayManager::GetConditionalSelectorByName().
	///
	/// \param[in]	strName		The selector's name to be set (or 'NULL' to remove the current name)
	///
	/// \note
	/// The name should be unique in the selector's overlay manager, otherwise the function returns IMcErrors::ID_ALREADY_EXISTS
	/// 
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetName(PCSTR strName) = 0;

	//==============================================================================
	// Method Name: GetName()
	//------------------------------------------------------------------------------
	/// Retrieves the selector'e user-defined unique name set by SetName().
	///        
	/// \param [out] pstrName	The selector's name (or 'NULL' if the name is not set)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetName(PCSTR *pstrName) const = 0;

    //@}

	/// \name Conditional Selector Type and Casting
	//@{

	//==============================================================================
    // Method Name: GetConditionalSelectorType()
    //------------------------------------------------------------------------------
    /// Returns the conditional selector type unique id
    ///
    //==============================================================================
    virtual UINT GetConditionalSelectorType() const = 0;

	//==============================================================================
	// Method Name: CastToBooleanConditionalSelector(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcConditionalSelector* To #IMcBooleanConditionalSelector*
	/// 
	/// \return
	///     - #IMcBooleanConditionalSelector*
	//==============================================================================
	virtual IMcBooleanConditionalSelector* CastToBooleanConditionalSelector() = 0;

	//==============================================================================
	// Method Name: CastToBlockedConditionalSelector(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcConditionalSelector* To #IMcBlockedConditionalSelector*
	/// 
	/// \return
	///     - #IMcBlockedConditionalSelector*
	//==============================================================================
	virtual IMcBlockedConditionalSelector* CastToBlockedConditionalSelector() = 0;

	//==============================================================================
	// Method Name: CastToScaleConditionalSelector(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcConditionalSelector* To #IMcScaleConditionalSelector*
	/// 
	/// \return
	///     - #IMcScaleConditionalSelector*
	//==============================================================================
	virtual IMcScaleConditionalSelector* CastToScaleConditionalSelector() = 0;

	//==============================================================================
	// Method Name: CastToObjectStateConditionalSelector(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcConditionalSelector* To #IMcObjectStateConditionalSelector*
	/// 
	/// \return
	///     - #IMcObjectStateConditionalSelector*
	//==============================================================================
	virtual IMcObjectStateConditionalSelector* CastToObjectStateConditionalSelector() = 0;

	//==============================================================================
	// Method Name: CastToViewportConditionalSelector(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcConditionalSelector* To #IMcViewportConditionalSelector*
	/// 
	/// \return
	///     - #IMcViewportConditionalSelector*
	//==============================================================================
	virtual IMcViewportConditionalSelector* CastToViewportConditionalSelector() = 0;

	//==============================================================================
	// Method Name: CastToLocationConditionalSelector(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcConditionalSelector* To #IMcLocationConditionalSelector*
	/// 
	/// \return
	///     -  #IMcViewportConditionalSelector*
	//==============================================================================
	virtual IMcLocationConditionalSelector* CastToLocationConditionalSelector() = 0;

    //@}
};
