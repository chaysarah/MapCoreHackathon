#pragma once
//==================================================================================
/// \file IMcCollection.h
/// Interface for collection of objects and overlays
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcBase.h"
#include "McCommonTypes.h"
#include "CMcDataArray.h"
#include "OverlayManager/IMcConditionalSelector.h"

class IMcObject;
class IMcOverlay;
class IMcOverlayManager;
class IMcMapViewport;

//==================================================================================
// Interface Name: IMcCollection
//----------------------------------------------------------------------------------
/// Interface for collection of objects and overlays
//==================================================================================
class IMcCollection : public virtual IMcBase
{
protected:

	virtual ~IMcCollection() {};

public:

	/// \name Create, Remove, Clear, Overlay Manager
	//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates a collection.
	///
	/// The collection will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Remove() method is called,
	/// or when its overlay manger is destroyed.
	///
	/// \param[out] ppCreatedCollection		The newly created collection
	/// \param[in]  pOverlayManager			The collection's overlay manager
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcCollection **ppCreatedCollection,
		IMcOverlayManager *pOverlayManager);

	//==============================================================================
	// Method Name: Remove()
	//------------------------------------------------------------------------------
	/// Removes the collection from its overlay manager
	/// and destroys it (its objects/overlays are NOT destroyed).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Remove() = 0;

	//==============================================================================
	// Method Name: Clear()
	//------------------------------------------------------------------------------
	/// Removes all objects/overlays from the collection.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode Clear() = 0;

	//==============================================================================
	// Method Name: GetOverlayManager()
	//------------------------------------------------------------------------------
	/// Retrieves the collection's overlay manager.
	/// 
	/// \param[out]	ppOverlayManager	The overlay manager
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetOverlayManager(IMcOverlayManager **ppOverlayManager) const = 0;

	//@}

	/// \name Collection Visibility
	//@{

	//==============================================================================
	// Method Name: SetCollectionVisibility()
	//------------------------------------------------------------------------------
	/// Sets the collection's visibility in one specific viewport or the default visibility in all viewports.
	///
	/// Setting the visibility for all viewports overrides a visibility for each viewport previously set.
	/// On the other hand, it can later be changed for any specific viewport.
	///
	///	The collection visibility of overlays/objects belonging to several collections is calculated according to IMcOverlayManager::ECollectionsMode 
	///	set by IMcOverlayManager::SetCollectionsMode().
	///
	/// An objects's visibility in a specific viewport depends on its visibility option for this viewport along with 
	/// its visibility conditional selector's result (only if the visibility option is IMcConditionalSelector::EAO_USE_SELECTOR - the default), 
	/// the visibility of collections the object belongs to and on similar parameters of the object's overlay.
	///
	/// \param[in]	bVisibility		The collection's visibility.
	/// \param[in]	pMapViewport	The viewport to set a visibility for or 'NULL' for the default visibility in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetCollectionVisibility(bool bVisibility, 
													 IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetCollectionVisibility()
	//------------------------------------------------------------------------------
	/// Retrieves the collection 's visibility in one specific viewport (set by SetCollectionVisibility() with specific viewport(s)) or 
	/// the default visibility in all viewports (set by SetCollectionVisibility() without viewport).
	///
	/// \param[out] pbVisibility	The collection's visibility.
	/// \param[in]	pMapViewport	The viewport to retrieve a visibility for or 'NULL' for the default visibility in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetCollectionVisibility(bool *pbVisibility, 
													 IMcMapViewport *pMapViewport = NULL) const = 0;

	//@}

	/// \name Objects
    //@{

	//==============================================================================
    // Method Name: AddObjects()
    //------------------------------------------------------------------------------
    /// Adds objects to the collection.
    ///
    /// \param[in] uNumObjects		The number of objects to add.
    /// \param[in] pObjects			The array of objects of add.
	///
    /// \return
    ///     - Status result
    //==============================================================================
	virtual IMcErrors::ECode AddObjects(
		UINT uNumObjects, IMcObject* const pObjects[]) = 0;

    //==============================================================================
    // Method Name: RemoveObjectFromCollection()
    //------------------------------------------------------------------------------
    /// Removes an object from the collection.
    ///
	/// \param[in] pObject	The object to be removed.
	///
    /// \return
    ///     - Status result
    //==============================================================================
	virtual IMcErrors::ECode RemoveObjectFromCollection(IMcObject *pObject) = 0;

	//==============================================================================
	// Method Name: RemoveObjectsFromTheirOverlays()
	//------------------------------------------------------------------------------
	/// Removes objects from the their overlays by calling IMcObject::Remove() for objects belonging to the collection and 
	/// for objects of the overlays belonging to the collection.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode RemoveObjectsFromTheirOverlays() = 0;
    
    //==============================================================================
    // Method Name: GetObjects()
    //------------------------------------------------------------------------------
    /// Retrieves the collection's objects.
    ///
	/// \param[out] papObjects	The array of the collection's objects.
	///
    /// \return
    ///     - Status result
    //==============================================================================
	virtual IMcErrors::ECode GetObjects(
		CMcDataArray<IMcObject*> *papObjects) const = 0;

    //==============================================================================
    // Method Name: MoveObjects()
    //------------------------------------------------------------------------------
    /// Moves location points of objects belonging to the collection and objects of the overlays belonging to the collection by calling 
	/// IMcObject::MoveAllLocationsPoints().
    ///
    /// \param[in] Offset	The offset
	///
    /// \return
    ///     - Status result
    //==============================================================================
    virtual IMcErrors::ECode MoveObjects(const SMcVector3D &Offset) = 0;

    //==============================================================================
    // Method Name: SetObjectsState()
    //------------------------------------------------------------------------------
	/// Sets the state of objects belonging to the collection and objects of the overlays belonging to the collection by calling IMcObject::SetState()
    ///
	/// Either the state in one specific viewport or the state default in all viewports is set. Setting the state default in all viewports 
	/// overrides a state in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
    /// \param[in] uState			The objects' new state as a single sub-state.
	/// \param[in] pMapViewport		The viewport to set the state for or `NULL` for the state default in all viewports.
	///
    /// \return
    ///     - Status result
    //==============================================================================
	virtual IMcErrors::ECode SetObjectsState(BYTE uState, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: SetObjectsStates()
	//------------------------------------------------------------------------------
	/// Sets the state of objects belonging to the collection and objects of the overlays belonging to the collection by calling IMcObject::SetState()
    ///
	/// Either the state in one specific viewport or the state default in all viewports is set. Setting the state default in all viewports 
	/// overrides a state in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] auStates			The objects' new state as an array of sub-states.
	/// \param[in] uNumStates		The number of sub-states in the above array.
	/// \param[in] pMapViewport		The viewport to set the state for or `NULL` for the state default in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectsState(const BYTE auStates[], UINT uNumStates, IMcMapViewport *pMapViewport = NULL) = 0;

    //==============================================================================
    // Method Name: SetObjectsVisibilityOption()
    //------------------------------------------------------------------------------
    /// Sets a visibility option of objects belonging to the collection and objects of the overlays belonging to the collection by calling 
    /// IMcObject::SetVisibilityOption().
    ///
	/// Setting the visibility option default in all viewports overrides a visibility option in each 
	/// viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in]	eVisibility		The objects' visibility option; the default is IMcConditionalSelector::EAO_USE_SELECTOR.
	/// \param[in]	pMapViewport	The viewport to set a visibility option for or `NULL` for default visibility in all viewports.
	///
    /// \return
    ///     - Status result
    //==============================================================================
    virtual IMcErrors::ECode SetObjectsVisibilityOption(
		IMcConditionalSelector::EActionOptions eVisibility, 
		IMcMapViewport *pMapViewport = NULL) = 0;

    //@}

    /// \name Overlays
    //@{

    //==============================================================================
    // Method Name: AddOverlays()
    //------------------------------------------------------------------------------
    /// Adds overlays to the collection.
    ///
    /// \param[in] uNumOverlays		The number of overlays to add.
    /// \param[in] pOverlays		The array of overlays of add.
	///
    /// \return
    ///     - Status result
    //==============================================================================
    virtual IMcErrors::ECode AddOverlays(
		UINT uNumOverlays, IMcOverlay* const pOverlays[]) = 0;

    //==============================================================================
    // Method Name: RemoveOverlayFromCollection()
    //------------------------------------------------------------------------------
    /// Removes overlay from the collection.
    ///
    /// \param[in] pOverlay		The overlay to be removed
	///
    /// \return
    ///     - Status result
    //==============================================================================
    virtual IMcErrors::ECode RemoveOverlayFromCollection(IMcOverlay *pOverlay) = 0;

	//==============================================================================
	// Method Name: RemoveOverlaysFromTheirOverlayManager()
	//------------------------------------------------------------------------------
	/// Removes overlays from the overlay manager by calling IMcOverlay::Remove() for overlays belonging to the collection.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode RemoveOverlaysFromOverlayManager() = 0;

    //==============================================================================
    // Method Name: GetOverlays()
    //------------------------------------------------------------------------------
    ///Retrieves the collection's overlays.
    ///
    /// \param[out] papOverlays		The array of the collection's overlays.
	///
    /// \return
    ///     - Status result
    //==============================================================================
	virtual IMcErrors::ECode GetOverlays(
		CMcDataArray<IMcOverlay*> *papOverlays) const = 0;

    //==============================================================================
    // Method Name: SetOverlaysVisibilityOption()
    //------------------------------------------------------------------------------
    /// Sets a visibility option of overlays belonging to the collection by calling IMcOverlay::SetVisibilityOption().
    ///
	/// Setting the visibility option default in all viewports overrides a visibility option in each 
	/// viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in]	eVisibility		The overlays' visibility option; the default is IMcConditionalSelector::EAO_USE_SELECTOR.
	/// \param[in]	pMapViewport	The viewport to set a visibility option for or `NULL` for default visibility in all viewports.
	///
    /// \return
    ///     - Status result
    //==============================================================================
    virtual IMcErrors::ECode SetOverlaysVisibilityOption(
		IMcConditionalSelector::EActionOptions eVisibility, 
		IMcMapViewport *pMapViewport = NULL) = 0;

    //@}
};
