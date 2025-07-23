#pragma once

//===========================================================================
/// \file IMcGlobalMap.h
/// Interfaces for global map
//===========================================================================
 
#include "IMcErrors.h"
#include "CMcDataArray.h"
#include "McCommonTypes.h"

class IMcMapViewport;
class IMcLineItem;

//===========================================================================
// Interface Name: IMcGlobalMap
//---------------------------------------------------------------------------
/// Base interface for global map
///
/// Global map shows footprints of regular map viewports registered and allows their 
/// navigation by editing their footprints in the global map
//===========================================================================
class IMcGlobalMap
{
protected:
	virtual ~IMcGlobalMap() {}

public:

	/// Cursor types
	enum ECursorType
	{
		ECT_DEFAULT_CURSOR,	///< the cursor used when local map's footprint is untouched, 
							///<  for example: IDC_ARROW
		ECT_DRAG_CURSOR,	///< the cursor used when local map's footprint is dragged, 
							///<  for example: IDC_HAND
		ECT_RESIZE_CURSOR	///< the cursor used when local map's footprint is resized, 
							///<  for example: IDC_SIZEALL, IDC_SIZENWSE or IDC_SIZENESW
	};

	/// Mouse event type.
	///
	/// \note	The relevant button is a mouse button used to edit a local map footprint.
	enum EMouseEvent
	{
		EME_BUTTON_PRESSED,				///< the relevant button is pressed
		EME_BUTTON_RELEASED,			///< the relevant button is released
		EME_MOUSE_MOVED_BUTTON_DOWN,	///< the mouse is moved when the relevant button is down
		EME_MOUSE_MOVED_BUTTON_UP		///< the mouse is moved when the relevant button is up
	};

	//==============================================================================
	// Method Name: SetGlobalMapAutoCenterMode()
	//------------------------------------------------------------------------------
	/// Switches global map auto-center mode on and off
	///
	/// In auto-center mode, the global map is automatically centered according to the active 
	/// local map movements
	///
	/// \param[in]	bAutoCenter		true if auto-center mode is on
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetGlobalMapAutoCenterMode(bool bAutoCenter) = 0;

	//==============================================================================
	// Method Name: GetGlobalMapAutoCenterMode()
	//------------------------------------------------------------------------------
	/// Retrieves global map auto-center mode
	///
	/// In auto-center mode, the global map is automatically centered according to the active 
	/// local map movements
	///
	/// \param[out]	pbAutoCenter		true if auto-center mode is on
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetGlobalMapAutoCenterMode(bool *pbAutoCenter) = 0;

	//==============================================================================
	// Method Name: RegisterLocalMap()
	//------------------------------------------------------------------------------
	/// Registers a local map viewport to be traced by the global map viewport.
	///
	/// \param[in]	pLocalMap		local map viewport to be traced
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RegisterLocalMap(IMcMapViewport *pLocalMap) = 0;

	//==============================================================================
	// Method Name: GetRegisteredLocalMaps()
	//------------------------------------------------------------------------------
	/// Retrieves all registered local maps
	///
	/// \param[out]	papLocalMaps		array of registered local maps
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRegisteredLocalMaps(CMcDataArray<IMcMapViewport*> *papLocalMaps) = 0;

	//==============================================================================
	// Method Name: UnRegisterLocalMap()
	//------------------------------------------------------------------------------
	/// Unregisters a local map viewport canceling tracing by the global map viewport.
	///
	/// \param[in]	pLocalMap		local map viewport to be traced
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode UnRegisterLocalMap(IMcMapViewport *pLocalMap) = 0;

	//==============================================================================
	// Method Name: SetActiveLocalMap()
	//------------------------------------------------------------------------------
	/// Sets an active local map for the global map.
	///
	/// \param[in]	pLocalMap		local map viewport (or NULL to cancel the active local map)
	///
	/// \note
	///		- Different items are used to draw active and inactive local map footprints
	///		- When in auto-center mode, the global map is automatically centered according 
	///		  to the active local map movements
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetActiveLocalMap(IMcMapViewport *pLocalMap) = 0;

	//==============================================================================
	// Method Name: GetActiveLocalMap()
	//------------------------------------------------------------------------------
	/// Retrieves an active local map for the global map.
	///
	/// \param[out]	ppLocalMap		local map viewport (or NULL if no active local map defined)
	///
	/// \note
	///		- Different items are used to draw active and inactive local map footprints
	///		- When in auto-center mode, the global map is automatically centered according 
	///		  to the active local map movements
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetActiveLocalMap(IMcMapViewport **ppLocalMap) const = 0;

	//==============================================================================
	// Method Name: SetLocalMapFootprintItem()
	//------------------------------------------------------------------------------
	/// Sets items to be used to draw local map footprints on the global map.
	///
	/// Line color and width only are used.
	///
	/// Either the items for one specific local map or the items' default for all local maps are set. 
	/// Setting the items' default for all local maps overrides the items for each local map previously set. 
	/// On the other hand, it can be changed later for any specific local map.
	///
	/// \param[in]	pInactiveLine		line item for inactive local map footprints
	/// \param[in]	pActiveLine			line item for active local map footprints
	/// \param[in]	pLocalMap			local map viewport to set the items for or `NULL` for the items' default in all local maps.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetLocalMapFootprintItem(IMcLineItem *pInactiveLine, IMcLineItem *pActiveLine, 
		IMcMapViewport *pLocalMap = NULL) = 0;
	//==============================================================================
	// Method Name: GetLocalMapFootprintItem()
	//------------------------------------------------------------------------------
	/// Retrieves items to be used to draw local map footprints on the global map for one specific local map (set by 
	/// SetLocalMapFootprintItem() with specific local map) or the items' default for all local maps (set by 
	/// SetLocalMapFootprintItem() without local map)
	///
	/// \param[out]	ppInactiveLine		line item for inactive local map footprints
	/// \param[out]	ppActiveLine		line item for active local map footprints
	/// \param[in]	pLocalMap			local map viewport to retrieve the items for or `NULL` for the items' default in all local maps.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLocalMapFootprintItem(IMcLineItem **ppInactiveLine, IMcLineItem **ppActiveLine, 
		IMcMapViewport *pLocalMap = NULL) const = 0;

	//==============================================================================
	// Method Name: GetLocalMapFootprintScreenPositions()
	//------------------------------------------------------------------------------
	/// Retrieves screen positions of the specified local map's footprint
	///
	/// \param[in]	pLocalMap			The local map viewport
	/// \param[out]	paPolygonPoints		The footprint polygon's points (relevant in any type of local map's viewport)
	/// \param[out]	paArrowPoints		The footprint arrow's points (relevant in local map's 3D viewport only, pass `NULL` if unneeded); 
	///									the points' order: 
	///									- points with indicies 0, 1, 2 are arrow's head
	///									- points with indicies 1, 4 are arrow's body
	///									- points with indicies 3, 5 are arrow's tail
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLocalMapFootprintScreenPositions(
		IMcMapViewport* pLocalMap,
		CMcDataArray<SMcVector2D>* paPolygonPoints, CMcDataArray<SMcVector2D>* paArrowPoints = NULL) const = 0;

	//==============================================================================
	// Method Name: OnMouseEvent()
	//------------------------------------------------------------------------------
	/// Should be called by the user when global map mode is true and a relevant mouse event 
	/// occurred inside global map window.
	///
	/// Relevant mouse events are (the relevant button is a mouse button used to edit a local map footprint):
	/// - the relevant button is pressed;
	/// - the relevant button is released;
	/// - the mouse is moved;
	///
	/// \param[in]	eEvent			the mouse event type.
	/// \param[in]	MousePosition	the current mouse position.
	///								(relative to top-left corner of the global map window)
	/// \param[out]	pbRenderNeeded	whether the global map viewport should be rendered immediately 
	///								to update local map footprint.
	/// \param[out]	peCursorType	the mouse cursor type to be set by the application.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode OnMouseEvent(EMouseEvent eEvent, const SMcPoint &MousePosition,
		bool *pbRenderNeeded, ECursorType *peCursorType) = 0;
};
