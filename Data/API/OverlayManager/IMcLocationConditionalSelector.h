#pragma once
//==================================================================================
/// \file IMcLocationConditionalSelector.h
/// The interface for Location Conditional Selector -
/// a conditional selector based on the current object location.
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "OverlayManager/IMcConditionalSelector.h"
#include "OverlayManager/IMcOverlayManager.h"

//==================================================================================
// Interface Name: IMcLocationConditionalSelector
//----------------------------------------------------------------------------------
/// The interface for Location Conditional Selector -
/// a conditional selector based on the current object location.
//==================================================================================
class IMcLocationConditionalSelector : public virtual IMcConditionalSelector
{
protected:

	virtual ~IMcLocationConditionalSelector() {};

public:

    enum
    {
        //==============================================================================
        /// Conditional Selector unique ID for this interface
        //==============================================================================
        CONDITIONAL_SELECTOR_TYPE = 6
    };

public:

	/// \name Create 
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a location conditional selector.
	/// 
	/// The selector will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when the last of its object scheme nodes is destroyed.
	///
	/// \param[in]  pOverlayManager				The selector's overlay manager
	/// \param[out] ppSelector					The newly created selector
	/// \param[in]  aPoints						The location's points
	/// \param[in]  uNumPoints					The number of location's points
	///
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcOverlayManager *pOverlayManager,
		IMcLocationConditionalSelector **ppSelector,
		const SMcVector3D aPoints[],
		UINT uNumPoints);

	//@}

	/// \name Properties
	//@{

	//==============================================================================
	// Method Name: SetPolygonPoints(...)
	//------------------------------------------------------------------------------
	/// Sets polygon points.
	///
	/// \param[in] aPoints		The polygon's points to set, in OverlayManager's coordinate system
	/// \param[in] uNumPoints	The number of polygon's points to set
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetPolygonPoints(
		const SMcVector3D aPoints[],
		UINT uNumPoints) = 0;

	//==============================================================================
	// Method Name: GetPolygonPoints(...)
	//------------------------------------------------------------------------------
	/// Retrieves polygon points.
	///
	/// \param[out] paLocationPoints	The polygon's points, in OverlayManager's coordinate system
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetPolygonPoints(
		CMcDataArray<SMcVector3D> *paLocationPoints) const = 0;

	//@}
};
