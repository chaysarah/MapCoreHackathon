#pragma once
//==================================================================================
/// \file IMcObjectScheme.h
/// Interface for object scheme (template used to create objects)
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcBase.h"
#include "CMcDataArray.h"
#include "OverlayManager/IMcProperty.h"
#include "OverlayManager/IMcObjectSchemeNode.h"
#include "McCommonTypes.h"
#include "IMcEditMode.h"

struct SMcBColor;
struct SMcFColor;
struct SMcAttenuation;
struct SMcVariantString;
struct SMcRotation;
struct SMcAnimation;

class IMcFont;
class IMcTexture;
class IMcMesh;

class IMcObjectLocation;
class IMcObjectSchemeNode;
class IMcObjectSchemeItem;
class IMcObjectScheme;
class IMcConditionalSelector;
class IMcMapViewport;
class IMcObject;
class IMcOverlayManager;

//==================================================================================
// Interface Name: IMcObjectScheme
//----------------------------------------------------------------------------------
/// Interface for object scheme (template used to create objects).
/// 
/// Object scheme defines the object's schematic graph with its private properties and state modifiers.
//==================================================================================
class IMcObjectScheme : public virtual IMcBase
{
public:

	/// Flags defining which terrain objects should be taken into consideration by object locations 
	/// with relative-to-terrain heights (used as a bit field in Create() and SetTerrainObjectsConsideration()).
	enum ETerrainObjectsConsiderationFlags
	{
		ETOCF_NONE					= 0x0000,	///< None
		ETOCF_STATIC_OBJECTS_LAYER	= 0x0001	///< Static objects layer
	};

	/// Object scheme component kind (category) used in SaveSchemeComponentInterface().
	enum ESchemeComponentKind
	{
		ESCK_OBJECT_SCHEME_NODE,	///< IMcObjectSchemeNode and its descendants
		ESCK_CONDITIONAL_SELECTOR,	///< IMcConditionalSelector and its descendants
		ESCK_FONT,					///< IMcFont and its descendants
		ESCK_MESH,					///< IMcMesh and its descendants
		ESCK_TEXTURE,				///< IMcTexture and its descendants
		ESCK_ENUMERATION			///< All enumeration types used in object scheme definition
	};

	/// Object state modifier used in SetObjectStateModifiers() to define changing object state automatically according to a conditional selector's result
	struct SObjectStateModifier
	{
		SObjectStateModifier() : pConditionalSelector(NULL), bActionOnResult(false), uObjectState(0) {} ///< \n

		IMcConditionalSelector *pConditionalSelector;	///< The conditional selector to check
		bool					bActionOnResult;		///< The selector's result used to set object state
		BYTE					uObjectState;			///< The object state to set when selector's result is equal to \p bActionOnResult
	};

protected:

	virtual ~IMcObjectScheme() {};

	virtual IMcErrors::ECode SetPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, IMcProperty::EPropertyType eType, const void *pValue) = 0;
	virtual IMcErrors::ECode GetPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, IMcProperty::EPropertyType eType, void *pValue) const = 0;

public:

	/// \name Create, Clone, Getters
	//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates an object scheme with one location.
	///
	/// The scheme will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when the last of its objects is destroyed.
	///
	/// \param[out] ppObjectScheme							The created object scheme
	/// \param[out] ppLocation								The first location created (pass NULL if unnecessary)
	/// \param[in]  pOverlayManager							The object scheme's overlay manager
	/// \param[in]  eLocationCoordSystem					The location points' coordinate system
	/// \param[in]	bLocationRelativeToDTM					Whether location points' heights are relative to DTM
	/// \param[in]	uTerrainObjectsConsiderationBitField	Bit field (based on #ETerrainObjectsConsiderationFlags) defining 
	///														which terrain objects should be taken into consideration 
	///														in object locations with relative-to-terrain heights
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcObjectScheme **ppObjectScheme,
		IMcObjectLocation **ppLocation,
		IMcOverlayManager *pOverlayManager,
		EMcPointCoordSystem eLocationCoordSystem,
		bool bLocationRelativeToDTM = false,
		UINT uTerrainObjectsConsiderationBitField = ETOCF_STATIC_OBJECTS_LAYER);

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates an object scheme with one location and an item connected to it.
	///
	/// The scheme will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// if the scheme was created internally by an object,
	/// it will be destroyed when the object is destroyed.
	/// otherwise, if the scheme was created directly,
	/// it should be destroyed by calling its Release() method.
	///
	/// \param[out] ppObjectScheme							The object scheme created
	/// \param[in]  pOverlayManager							The object scheme's overlay manager
	/// \param[in]	pItem									The item to connect to first location
	/// \param[in]  eLocationCoordSystem					The location points' coordinate system
	/// \param[in]	bLocationRelativeToDTM					Whether location points' heights are relative to DTM
	/// \param[in]	uTerrainObjectsConsiderationBitField	Bit field (based on #ETerrainObjectsConsiderationFlags) defining 
	///														which terrain objects should be taken into consideration 
	///														in object locations with relative-to-terrain heights
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcObjectScheme **ppObjectScheme,
		IMcOverlayManager *pOverlayManager,
		IMcObjectSchemeItem *pItem,
		EMcPointCoordSystem eLocationCoordSystem,
		bool bLocationRelativeToDTM = false,
		UINT uTerrainObjectsConsiderationBitField = ETOCF_STATIC_OBJECTS_LAYER);

	//==============================================================================
	// Method Name: Clone()
	//------------------------------------------------------------------------------
	/// Clones the object scheme.
	///
	/// \param[out] ppClonedObjectScheme	The created clone
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Clone(IMcObjectScheme **ppClonedObjectScheme) = 0;

	//==============================================================================
	// Method Name: GetOverlayManager()
	//------------------------------------------------------------------------------
	/// Retrieves the object scheme's overlay manager.
	/// 
	/// \param[out]	ppOverlayManager	The overlay manager.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetOverlayManager(IMcOverlayManager **ppOverlayManager) const = 0;

	//@}

	/// \name Terrain Objects' Consideration and Attached-to-terrain Items' Consistency
	//@{

	//==============================================================================
	// Method Name: SetTerrainObjectsConsideration()
	//------------------------------------------------------------------------------
	/// Sets the bit field defining which terrain objects should be taken into consideration in object locations with relative-to-terrain heights.
	/// 
	/// \param[in]	uTerrainObjectsConsiderationBitField	The bit field (based on #ETerrainObjectsConsiderationFlags) defining 
	///														which terrain objects should be taken into consideration 
	///														in object locations with relative-to-terrain heights.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTerrainObjectsConsideration(UINT uTerrainObjectsConsiderationBitField) = 0;

	//==============================================================================
	// Method Name: GetTerrainObjectsConsideration()
	//------------------------------------------------------------------------------
	/// Retrieves the bit field defining which terrain objects should be taken into consideration in object locations with relative-to-terrain heights.
	///
	/// \param[out]	puTerrainObjectsConsiderationBitField	The bit field (based on #ETerrainObjectsConsiderationFlags) defining 
	///														which terrain objects should be taken into consideration 
	///														in object locations with relative-to-terrain heights.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTerrainObjectsConsideration(UINT *puTerrainObjectsConsiderationBitField) const = 0;

	//==============================================================================
	// Method Name: SetTerrainItemsConsistency()
	//------------------------------------------------------------------------------
	/// Enables/disables the mode of consistency of items attached to terrain.
	/// 
	/// Relevant only for items attached to terrain in 3D viewport. When the mode is enabled:
	/// - Screen-size items will preserve their size, 
	///   width, line texture mapping, offset across terrain tiles (relevant only when scale 
	///   steps are defined by IMcOverlayManager::SetScreenTerrainItemsConsistencyScaleSteps()).
	/// - Any items will preserve their scale condition across terrain tiles.
	/// Can affect the performance, not to be used in large number of objects.
	///
	/// \param[in]		bEnabled	Whether or not the mode is enabled; the default is false.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTerrainItemsConsistency(bool bEnabled) = 0;

	//==============================================================================
	// Method Name: GetTerrainItemsConsistency()
	//------------------------------------------------------------------------------
	/// Retrieves the mode of consistency of items attached to terrain set by SetTerrainItemsConsistency().
	/// 
	/// \param[out]		pbEnabled	Whether or not the mode is enabled; the default is false.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTerrainItemsConsistency(bool *pbEnabled) const = 0;


		//==============================================================================
		// Method Name: SetGroupingItemsByDrawPriorityWithinObjects ()
		//------------------------------------------------------------------------------
		/// Enables/disables the mode of grouping items within their containing objects.
		/// 
		/// The rendering drawing order of object scheme items is determined by: 
		/// 1. First , the drawing priority of the overlay containing the object the item belongs to.
		/// 2. Second, the drawing priority of the object the item belongs to.
		/// 3. Third, the drawing priority of the item.
		///
		/// When this mode is disabled (the default), the item’s rendering drawing order is not influenced by the specific object it belongs to and 
		/// items are not grouped by objects. Therefore items with higher drawing priority will always be drawn on top of other items with lower 
		/// drawing priority, as long as their containing objects and overlays have equal priorities. This can result in items from the same object 
		/// being mixed with items from other objects according to their drawing priority.
		///
		/// When this mode is enabled, all items belonging to the same object will be rendered in the order of their drawing priorities, but as a 
		/// group above or below items of other objects. Therefore items from one object will never mix between items from a different object even 
		/// when both objects and their overlays have the same drawing priority.
		///
		/// Relevant for any items in 2D viewports, for screen-coordinate items and items attached to terrain in 3D viewports.
		///
		/// \param[in]             bEnabled      Whether or not the mode is enabled; the default is false.
		///
		/// \return
		///     - status result
		//==============================================================================
 
 
	virtual IMcErrors::ECode SetGroupingItemsByDrawPriorityWithinObjects(bool bEnabled) = 0;

	//==============================================================================
	// Method Name: GetGroupingItemsByDrawPriorityWithinObjects()
	//------------------------------------------------------------------------------
	/// Retrieves the mode of grouping items within their containing objects set by SetGroupingItemsByDrawPriorityWithinObjects().
	/// 
	/// \param[out]		pbEnabled	Whether or not the mode is enabled; the default is false.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetGroupingItemsByDrawPriorityWithinObjects(bool *pbEnabled) const  = 0;

	//@}

	/// \name Object Locations
	//@{

	//==============================================================================
	// Method Name: GetNumObjectLocations()
	//------------------------------------------------------------------------------
	/// Retrieves the number of location nodes in the scheme.
	///
	/// \param[out]	puNumLocations	The number of location nodes.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetNumObjectLocations(UINT *puNumLocations) const = 0;

	//==============================================================================
	// Method Name: GetObjectLocationIndexByID()
	//------------------------------------------------------------------------------
	/// Retrieves the location nodes's index in the scheme (see IMcObjectLocation::GetIndex()) from its node ID set by IMcObjectSchemeNode::SetID().
	///
	/// \param[in]	uNodeID		The location's node ID to look for
	/// \param[out]	puIndex		The location's index found
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectLocationIndexByID(UINT uNodeID, UINT *puIndex) const = 0;

	//==============================================================================
	// Method Name: AddObjectLocation()
	//------------------------------------------------------------------------------
	/// Creates and adds a location node to the scheme.
	///
	/// \param[out] ppLocation				The location created.
	/// \param[in]	eLocationCoordSystem	The location points' coordinate system.
	/// \param[in]	bLocationRelativeToDTM	Whether the location points' heights are relative to DTM.
	/// \param[out] puLocationIndex			The index of the location created (NULL can be passed if it is not needed).
	/// \param[in]	uInsertAtIndex			The optional index to insert the location at; if not specified or out of range (e.g. `UINT_MAX`), 
	///										the location will be appended at the end.
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode AddObjectLocation(IMcObjectLocation **ppLocation,
		EMcPointCoordSystem eLocationCoordSystem, bool bLocationRelativeToDTM = false, 
		UINT *puLocationIndex = NULL, UINT uInsertAtIndex = UINT_MAX) = 0;

	//==============================================================================
	// Method Name: RemoveObjectLocation()
	//------------------------------------------------------------------------------
	/// Remove a location node from the scheme according to its index in the scheme.
	///
	/// \param[in] uLocationIndex			The index of the location to remove; the default is 0 - the first location.
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RemoveObjectLocation(UINT uLocationIndex = 0) = 0;

	//==============================================================================
	// Method Name: GetObjectLocation()
	//------------------------------------------------------------------------------
	/// Retrieves a location node from its index in the scheme.
	///
	/// \param[out] ppLocation		The location
	/// \param[in]  uLocationIndex	The index of the location to retrieve; the default is 0 - the first location.
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectLocation(IMcObjectLocation **ppLocation,
												UINT uLocationIndex = 0) const = 0;
	//@}

	/// \name Object Scheme Nodes
	//@{

	//==============================================================================
	// Method Name: GetNodeByID()
	//------------------------------------------------------------------------------
	/// Retrieves the scheme's node based on its ID defined by IMcObjectSchemeNode::SetNodeID().
	///
	/// \param[in] uNodeID		The node's ID to look for
	/// \param[out] ppNode      The node found
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetNodeByID(UINT uNodeID, IMcObjectSchemeNode **ppNode) const = 0;

	//==============================================================================
	// Method Name: GetNodeByName()
	//------------------------------------------------------------------------------
	/// Retrieves the scheme's node based on its name defined by IMcObjectSchemeNode::SetName().
	///
	/// \param[in] strNodeName	The node's name to look for
	/// \param[out] ppNode      The node found
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetNodeByName(PCSTR strNodeName, IMcObjectSchemeNode **ppNode) const = 0;

	//==============================================================================
	// Method Name: GetNodes()
	//------------------------------------------------------------------------------
	/// Retrieves all the nodes of the scheme of the specified kinds (categories).
	///
	/// \param[out] papNodes			The array of nodes.
	/// \param[in]  uNodeKindBitField	The node kinds (categories) to retrieve (a bit field based on #IMcObjectSchemeNode::ENodeKindFlags); 
	///									the default is IMcObjectSchemeNode::ENKF_ANY_NODE.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetNodes(CMcDataArray<IMcObjectSchemeNode*> *papNodes, 
		UINT uNodeKindBitField = IMcObjectSchemeNode::ENKF_ANY_NODE) const = 0;

	//==============================================================================
	// Method Name: GetNodesByPropertyID()
	//------------------------------------------------------------------------------
	/// Retrieves all the nodes of the scheme that use a specified property.
	///
	/// \param[in]	uPropertyID		The property ID to look for.
	/// \param[out]	papNodes		The array of nodes using the property.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetNodesByPropertyID(UINT uPropertyID, CMcDataArray<IMcObjectSchemeNode*> *papNodes) const = 0;

	//==============================================================================
	// Method Name: SaveSchemeComponentInterface()
	//------------------------------------------------------------------------------
	/// Saves the interface of the object scheme component defined by the a component's kind (category) and specific type 
	/// (or a list of all possible component types of the specified kind) into Json file.
	///
	/// \param[in]	eComponentKind			component's kind (category)
	/// \param[in]	uComponentType			component's specific type according to the specified kind (IMcLineItem::NODE_TYPE etc.) 
	///										or zero to get a list of all possible component types of the specified kind
	/// \param[in]	strJsonFileName			Json file name
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API SaveSchemeComponentInterface(ESchemeComponentKind eComponentKind, UINT uComponentType, 
		PCSTR strJsonFileName);

	//==============================================================================
	// Method Name: SaveSchemeComponentInterface()
	//------------------------------------------------------------------------------
	/// Saves the interface of the object scheme component defined by the a component's kind (category) and specific type 
	/// (or a list of all possible component types of the specified kind) into Json memory buffer.
	///
	/// \param[in]	eComponentKind			component's kind (category)
	/// \param[in]	uComponentType			component's specific type according to the specified kind (IMcLineItem::NODE_TYPE etc.) 
	///										or zero to get a list of all possible component types of the specified kind
	/// \param[out]	paJsonMemoryBuffer		Json memory buffer
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API SaveSchemeComponentInterface(ESchemeComponentKind eComponentKind, UINT uComponentType, 
		CMcDataArray<BYTE> *paJsonMemoryBuffer);

	//@}

	/// \name Objects
	//@{

	//==============================================================================
	// Method Name: GetObjects()
	//------------------------------------------------------------------------------
	/// Retrieves all the objects based on the scheme.
	///
	/// \param[out] papObjects			The array of objects
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjects(CMcDataArray<IMcObject*> *papObjects) const = 0;

	//@}

	/// \name Object State
	//@{

	//==============================================================================
	// Method Name: SetObjectStateName()
	//------------------------------------------------------------------------------
	/// Sets the object state's unique name.
	///
	/// \param[in]	strStateName		The state's name to be set (or NULL to remove an existing name).
	/// \param[in]	uState				The state.
	///        
	/// \return
	///     - status result (error if the name is already in use)
	//==============================================================================
	virtual IMcErrors::ECode SetObjectStateName(PCSTR strStateName, BYTE uState) = 0;

	//==============================================================================
	// Method Name: GetObjectStateName()
	//------------------------------------------------------------------------------
	/// Retrieves an object state's name set by IMcObjectScheme::SetObjectStateName() 
	///
	/// \param[in] uState			The state to look for
	/// \param[out] pstrStateName	The state's name found
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectStateName(BYTE uState, PCSTR *pstrStateName) const = 0;

	//==============================================================================
	// Method Name: GetObjectStateByName()
	//------------------------------------------------------------------------------
	/// Retrieves an object state from its name defined by SetObjectStateName().
	///
	/// \param[in] strStateName		The state's name to look for.
	/// \param[out] puState			The state found.
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectStateByName(PCSTR strStateName, BYTE *puState) const = 0;

	//==============================================================================
	// Method Name: SetObjectsState()
	//------------------------------------------------------------------------------
	/// Sets the state (as a single sub-state) of all the objects based on the scheme by calling IMcObject::SetState() for the scheme's every object.
	///
	/// Either the state in one specific viewport or the state default in all viewports is set. Setting the state default in all viewports 
	/// overrides a state in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] uState			The objects' new sub-state
	/// \param[in] pMapViewport		The viewport to set the state for or `NULL` for the state default in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectsState(BYTE uState, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: SetObjectsStates()
	//------------------------------------------------------------------------------
	/// Sets the state (as an array of sub-states) of all the objects based on the scheme by calling IMcObject::SetState() for the scheme's every object.
	///
	/// Either the state in one specific viewport or the state default in all viewports is set. Setting the state default in all viewports 
	/// overrides a state in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] auStates			The array of the objects' new sub-states.
	/// \param[in] uNumStates		The number of sub-states in the above array.
	/// \param[in] pMapViewport		The viewport to set the state for or `NULL` for the state default in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectsState(const BYTE auStates[], UINT uNumStates, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: SetObjectStateModifiers()
	//------------------------------------------------------------------------------
	/// Sets the object-state modifiers used to change object-state automatically according to selectors' results.
	///
	/// The sub-states calculated by the modifiers are added to sub-states defined by IMcOverlay::SetState() and IMcObject::SetState() as described 
	/// in IMcObject.GetEffectiveState().
	///
	/// \param[in] aObjectStateModifiers		The array of new object-state modifiers.
	/// \param[in] uNumObjectStateModifiers		The number of object-state modifiers in the above array.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectStateModifiers(const SObjectStateModifier aObjectStateModifiers[], UINT uNumObjectStateModifiers) = 0;

	//==============================================================================
	// Method Name: GetObjectStateModifiers()
	//------------------------------------------------------------------------------
	/// Retrieves the object-state modifiers previously set by SetObjectStateModifiers().
	///
	/// \param[out] paObjectStateModifiers		Array of object-state modifiers
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectStateModifiers(CMcDataArray<SObjectStateModifier> *paObjectStateModifiers) const = 0;

	//@}

	/// \name Item for Object Rotation and Screen Arrangement
	//@{

	//==============================================================================
	// Method Name: SetObjectRotationItem()
	//------------------------------------------------------------------------------
	/// Sets the object's rotation item used to rotate the object in IMcObject::RotateByItem().
	///
	/// \param[in]	pItem	The object scheme item to be used to rotate the object in IMcObject::RotateByItem()
	///						(or `NULL` to cancel the object rotation item).
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectRotationItem(IMcObjectSchemeItem *pItem) = 0;

	//==============================================================================
	// Method Name: GetObjectRotationItem()
	//------------------------------------------------------------------------------
	/// Retrieves the object's rotation item set by SetObjectRotationItem()
	///
	/// \param[in]	ppItem	The object scheme item to be used to rotate the object in IMcObject::RotateByItem() (or `NULL` if not defined).
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectRotationItem(IMcObjectSchemeItem **ppItem) const = 0;

	//==============================================================================
	// Method Name: SetObjectScreenArrangementItem()
	//------------------------------------------------------------------------------
	/// Sets the object's screen arrangement item to be used in screen arrangements defined by 
	/// IMcOverlayManager::SetScreenArrangement() or IMcObject::SetScreenArrangementOffset().
	///
	/// \see IMcOverlayManager::SetScreenArrangement()
	///
	/// \param[in]	pItem	The object scheme item to be used in screen arrangements
	///						(or `NULL` to cancel the object screen arrangement item).
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectScreenArrangementItem(IMcObjectSchemeItem *pItem) = 0;
	
	//==============================================================================
	// Method Name: GetObjectScreenArrangementItem()
	//------------------------------------------------------------------------------
	/// Retrieves the object's screen arrangement item set by SetObjectScreenArrangementItem().
	///
	/// \param[in]	ppItem	The object scheme item to be used in screen arrangements (or `NULL` if not defined)
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectScreenArrangementItem(IMcObjectSchemeItem **ppItem) const = 0;

	//@}

	/// \name ID, Name and User Data
	//@{

	//==============================================================================
	// Method Name: SetID()
	//------------------------------------------------------------------------------
	/// Sets the scheme's user-defined unique ID that allows retrieving the scheme by IMcOverlayManager::GetObjectSchemeByID().
	///
	/// \param[in]	uID		The scheme's ID to be set (specify ID not equal to #MC_EMPTY_ID to set ID,
	///						equal to #MC_EMPTY_ID to remove ID).
	/// \note
	/// The ID should be unique in the scheme's overlay manager, otherwise the function returns IMcErrors::ID_ALREADY_EXISTS
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetID(UINT uID) = 0;

	//==============================================================================
	// Method Name: GetID()
	//------------------------------------------------------------------------------
	/// Retrieves the scheme'e user-defined unique ID set by SetID().
	///        
	/// \param [out] puID	The scheme ID (or #MC_EMPTY_ID if the ID is not set).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetID(UINT *puID) const = 0;

	//==============================================================================
	// Method Name: SetName()
	//------------------------------------------------------------------------------
	/// Sets the scheme's user-defined unique name that allows retrieving the scheme by IMcOverlayManager::GetObjectSchemeByName().
	///
	/// \param[in]	strName		The scheme's name to be set (or NULL to remove the current name)
	///
	/// \note
	/// The name should be unique in the scheme's overlay manager, otherwise the function returns IMcErrors::ID_ALREADY_EXISTS
	/// 
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetName(PCSTR strName) = 0;

	//==============================================================================
	// Method Name: GetName()
	//------------------------------------------------------------------------------
	/// Retrieves the scheme'e user-defined unique name set by SetName().
	///        
	/// \param [out] pstrName	The scheme's name (or NULL if the name is not set)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetName(PCSTR *pstrName) const = 0;

	//==============================================================================
	// Method Name: SetUserData()
	//------------------------------------------------------------------------------
	/// Sets an optional user-defined data that can be retrieved later by GetUserData().
	///
	/// \param[in]	pUserData	An instance of a user-defined class implementing IMcUserData interface.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetUserData(IMcUserData *pUserData) = 0;

	//==============================================================================
	// Method Name: GetUserData()
	//------------------------------------------------------------------------------
	/// Retrieves an optional user-defined data set by SetUserData().
	///
	/// \param[out]	ppUserData	An instance of a user-defined class implementing IMcUserData interface.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetUserData(IMcUserData **ppUserData) const = 0;

	//@}

	/// \name Properties
	//@{

    //==============================================================================
    // Method Name: GetProperties()
    //------------------------------------------------------------------------------
	/// Retrieves all the properties of the scheme.
	///
	/// \param[out] paProperties	The array of property IDs and types.
	///
	/// \return
	///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetProperties(CMcDataArray<IMcProperty::SPropertyNameIDType> *paProperties) const = 0;

    //==============================================================================
    // Method Name: GetPropertyType()
    //------------------------------------------------------------------------------
    /// Retrieves the type of a property defined by an ID.
    ///
	/// \param[in]	NameOrID				The property's unique name or ID (name if it is not empty, ID otherwise); 
	///										name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[out]	peType					The property type.
	/// \param[in]	bNoFailOnNonExistent	In case of non-existent property ID: whether to return IMcProperty::EPT_NUM or to fail 
	///										with IMcErrors::PROPERTY_DOES_NOT_EXIST_IN_TABLE error code; the default is false (i.e. to fail).
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode GetPropertyType(const IMcProperty::SPropertyNameID &NameOrID, IMcProperty::EPropertyType *peType, 
		bool bNoFailOnNonExistent = false) const = 0;

    //==============================================================================
    // Method Name: GetEnumPropertyActualType()
    //------------------------------------------------------------------------------
    /// Retrieves the actual type name of the property defined by a specified ID.
    ///
    /// Useful for properties of numeric types (IMcProperty::EPT_BYTE or IMcProperty::EPT_UINT) that can contain enums instead of numbers.
    ///
	/// \param[in]	NameOrID		The property's unique name or ID (name if it is not empty, ID otherwise); 
	///								name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[out]	pstrTypeName	The  actual enum name of the property (NULL or empty string if the property doesn't exist or isn't of enum type).
	///								The type name will be something like "IMcSymbolicItem.EDrawPriorityGroup" 
	///								(or "IMcSymbolicItem.ESegmentType|number" if the property value can be either an enum or a number).
	/// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode GetEnumPropertyActualType(const IMcProperty::SPropertyNameID &NameOrID, PCSTR *pstrTypeName) const = 0;

	//==============================================================================
	// Method Name: SetPropertyName()
	//------------------------------------------------------------------------------
	/// Sets or removes the name (alias) of a property.
	///
	/// \param[in]	strPropertyName		The property name (or NULL to remove the name associated with an ID given by \p uID).
	/// \param[in]	uID					The property ID (or #MC_EMPTY_ID to remove a name given by \p strPropertyName).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetPropertyName(PCSTR strPropertyName, UINT uID) = 0;

	//==============================================================================
	// Method Name: GetPropertyIDByName()
	//------------------------------------------------------------------------------
	/// Retrieves the property ID by its name (alias) set by SetPropertyName() / SetPropertyNames().
	///
	/// \param[in]	strPropertyName		The property name.
	/// \param[out]	puID				The property ID (or #MC_EMPTY_ID if no property has the name given by \p strPropertyName).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetPropertyIDByName(PCSTR strPropertyName, UINT *puID) const = 0;

	//==============================================================================
	// Method Name: GetPropertyNameByID()
	//------------------------------------------------------------------------------
	/// Retrieves the property name (alias) by its ID set by SetPropertyName() / SetPropertyNames().
	///
	/// \param[in]	uID					The property ID
	/// \param[out]	pstrPropertyName	The property name (or NULL if no property has the ID given by \p uID).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetPropertyNameByID(UINT uID, PCSTR *pstrPropertyName) const = 0;

	//==============================================================================
	// Method Name: SetPropertyNames()
	//------------------------------------------------------------------------------
	/// Removes all the names (aliases) of properties and sets the new ones.
	///
	/// \param[in]	aProperties			The array of property IDs and names.
	/// \param[in]	uNumProperties		The number of properties in the above array.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetPropertyNames(IMcProperty::SPropertyNameID aProperties[], 
											   UINT uNumProperties) = 0;

	//==============================================================================
	// Method Name: GetPropertyNames()
	//------------------------------------------------------------------------------
	/// Retrieves all the names (aliases) of all properties set by SetPropertyName() / SetPropertyNames().
	///
	/// \param[out]	pastrNames			The array of property IDs and names.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetPropertyNames(
		CMcDataArray<IMcProperty::SPropertyNameID> *pastrNames) const = 0;

	//==============================================================================
	// Method Name: SetIgnoreUpdatingNonExistentProperty()
	//------------------------------------------------------------------------------
	/// Sets whether updating non-existent property of scheme/object should be ignored or should fail.
	///
	/// \param[in]	bIgnore		Whether updating non-existent property should be ignored or should fail 
	///							with IMcErrors::PROPERTY_DOES_NOT_EXIST_IN_TABLE error code; the default is false (i.e. to fail).
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API SetIgnoreUpdatingNonExistentProperty(bool bIgnore);

	//==============================================================================
	// Method Name: GetIgnoreUpdatingNonExistentProperty()
	//------------------------------------------------------------------------------
	/// Retrieves whether updating non-existent property of scheme/object is ignored or fails.
	///
	/// \param[out]	pbIgnore	Whether updating non-existent property is ignored or fails 
	///							with IMcErrors::PROPERTY_DOES_NOT_EXIST_IN_TABLE error code; the default is false (i.e. fails).
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API GetIgnoreUpdatingNonExistentProperty(bool *pbIgnore);

    //==============================================================================
    // Method Name: SetBoolPropertyDefault()
    //------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetBoolPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, bool Value) = 0;

    //==============================================================================
    // Method Name: GetBoolPropertyDefault()
    //------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetBoolPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, bool *pValue) const = 0;

    //==============================================================================
    // Method Name: SetBytePropertyDefault()
    //------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetBytePropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, BYTE Value) = 0;

    //==============================================================================
    // Method Name: GetBytePropertyDefault()
    //------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetBytePropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, BYTE *pValue) const = 0;

	//==============================================================================
	// Method Name: SetSBytePropertyDefault()
	//------------------------------------------------------------------------------
	/// Updates the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[in]	Value		The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSBytePropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, signed char Value) = 0;

	//==============================================================================
	// Method Name: GetSBytePropertyDefault()
	//------------------------------------------------------------------------------
	/// Retrieves the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[out]	pValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSBytePropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, signed char *pValue) const = 0;

	//==============================================================================
	// Method Name: SetEnumPropertyDefault()
	//------------------------------------------------------------------------------
	/// Updates the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetEnumPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, UINT Value) = 0;

	//==============================================================================
	// Method Name: GetEnumPropertyDefault()
	//------------------------------------------------------------------------------
	/// Retrieves the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetEnumPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, UINT *pValue) const = 0;

    //==============================================================================
    // Method Name: SetIntPropertyDefault()
    //------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetIntPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, int Value) = 0;

    //==============================================================================
    // Method Name: GetIntPropertyDefault()
    //------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetIntPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, int *pValue) const = 0;

    //==============================================================================
    // Method Name: SetUIntPropertyDefault()
    //------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetUIntPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, UINT Value) = 0;

    //==============================================================================
    // Method Name: GetUIntPropertyDefault()
    //------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetUIntPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, UINT *pValue) const = 0;

	//==============================================================================
    // Method Name: SetFloatPropertyDefault()
	//------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetFloatPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, float Value) = 0;

    //==============================================================================
    // Method Name: GetFloatPropertyDefault()
    //------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
    virtual IMcErrors::ECode GetFloatPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, float *pValue) const = 0;

    //==============================================================================
    // Method Name: SetDoublePropertyDefault()
    //------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetDoublePropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, double Value) = 0;

    //==============================================================================
    // Method Name: GetDoublePropertyDefault()
    //------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetDoublePropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, double *pValue) const = 0;

    //==============================================================================
    // Method Name: SetVector2DPropertyDefault()
    //------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetVector2DPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, const SMcVector2D &Value) = 0;

    //==============================================================================
    // Method Name: GetVector2DPropertyDefault()
    //------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetVector2DPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, SMcVector2D *pValue) const = 0;

	//==============================================================================
    // Method Name: SetVector2DPropertyDefault()
    //------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetVector2DPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, const SMcFVector2D &Value) = 0;

    //==============================================================================
    // Method Name: GetVector2DPropertyDefault()
	//------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
    virtual IMcErrors::ECode GetVector2DPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, SMcFVector2D *pValue) const = 0;

    //==============================================================================
    // Method Name: SetVector3DPropertyDefault()
    //------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetVector3DPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, const SMcVector3D &Value) = 0;

    //==============================================================================
    // Method Name: GetVector3DPropertyDefault()
    //------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetVector3DPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, SMcVector3D *pValue) const = 0;

	//==============================================================================
    // Method Name: SetVector3DPropertyDefault()
	//------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetVector3DPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, const SMcFVector3D &Value) = 0;

    //==============================================================================
    // Method Name: GetVector3DPropertyDefault()
    //------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
    virtual IMcErrors::ECode GetVector3DPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, SMcFVector3D *pValue) const = 0;

    //==============================================================================
    // Method Name: SetColorPropertyDefault()
    //------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetColorPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, const SMcBColor &Value) = 0;

    //==============================================================================
    // Method Name: GetColorPropertyDefault()
    //------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetColorPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, SMcBColor *pValue) const = 0;

	//==============================================================================
    // Method Name: SetColorPropertyDefault()
    //------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetColorPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, const SMcFColor &Value) = 0;

    //==============================================================================
    // Method Name: GetColorPropertyDefault()
    //------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetColorPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, SMcFColor *pValue) const = 0;

    //==============================================================================
    // Method Name: SetAttenuationPropertyDefault()
    //------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetAttenuationPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, const SMcAttenuation &Value) = 0;

    //==============================================================================
    // Method Name: GetAttenuationPropertyDefault()
    //------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetAttenuationPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, SMcAttenuation *pValue) const = 0;

    //==============================================================================
    // Method Name: SetStringPropertyDefault()
    //------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetStringPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, const SMcVariantString &Value) = 0;

    //==============================================================================
    // Method Name: GetStringPropertyDefault()
    //------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetStringPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, SMcVariantString *pValue) const = 0;

	//==============================================================================
	// Method Name: SetFontPropertyDefault()
	//------------------------------------------------------------------------------
	/// Updates the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	pValue		The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetFontPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, IMcFont *pValue) = 0;

	//==============================================================================
	// Method Name: GetFontPropertyDefault()
	//------------------------------------------------------------------------------
	/// Retrieves the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	ppValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetFontPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, IMcFont **ppValue) const = 0;

    //==============================================================================
    // Method Name: SetTexturePropertyDefault()
    //------------------------------------------------------------------------------
    /// Updates the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	pValue		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetTexturePropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, IMcTexture *pValue) = 0;

    //==============================================================================
    // Method Name: GetTexturePropertyDefault()
    //------------------------------------------------------------------------------
    /// Retrieves the default value of the scheme's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	ppValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetTexturePropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, IMcTexture **ppValue) const = 0;

	//==============================================================================
	// Method Name: SetMeshPropertyDefault()
	//------------------------------------------------------------------------------
	/// Updates the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	pValue		The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetMeshPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, IMcMesh *pValue) = 0;

	//==============================================================================
	// Method Name: GetMeshPropertyDefault()
	//------------------------------------------------------------------------------
	/// Retrieves the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	ppValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetMeshPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, IMcMesh **ppValue) const = 0;

	//==============================================================================
	// Method Name: SetConditionalSelectorPropertyDefault()
	//------------------------------------------------------------------------------
	/// Updates the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	pValue		The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetConditionalSelectorPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, 
		IMcConditionalSelector *pValue) = 0;

	//==============================================================================
	// Method Name: GetConditionalSelectorPropertyDefault()
	//------------------------------------------------------------------------------
	/// Retrieves the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	ppValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetConditionalSelectorPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, 
		IMcConditionalSelector **ppValue) const = 0;

	//==============================================================================
	// Method Name: SetRotationPropertyDefault()
	//------------------------------------------------------------------------------
	/// Updates the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetRotationPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, const SMcRotation &Value) = 0;

	//==============================================================================
	// Method Name: GetRotationPropertyDefault()
	//------------------------------------------------------------------------------
	/// Retrieves the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRotationPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, SMcRotation *pValue) const = 0;

	//==============================================================================
	// Method Name: SetAnimationPropertyDefault()
	//------------------------------------------------------------------------------
	/// Updates the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetAnimationPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, const SMcAnimation &Value) = 0;

	//==============================================================================
	// Method Name: GetAnimationPropertyDefault()
	//------------------------------------------------------------------------------
	/// Retrieves the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAnimationPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, SMcAnimation *pValue) const = 0;

	//==============================================================================
	// Method Name: SetArrayPropertyDefault()
	//------------------------------------------------------------------------------
	/// Updates the default value of the scheme's property.
	///
	/// \param[in]	NameOrID		The property's unique name or ID (name if it is not empty, ID otherwise); 
	///								name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[in] ePropertyType	The property type (IMcProperty::EPT_XXX_ARRAY)
    /// \param[in]	Value			The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	template <typename T>
	inline IMcErrors::ECode SetArrayPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, IMcProperty::EPropertyType ePropertyType, 
		const IMcProperty::SArrayProperty<T> &Value)
	{
		return SetPropertyDefault(NameOrID, ePropertyType, &Value);
	}

	//==============================================================================
	// Method Name: GetArrayPropertyDefault()
	//------------------------------------------------------------------------------
	/// Retrieves the default value of the scheme's property.
	///
	/// \param[in]	NameOrID		The property's unique name or ID (name if it is not empty, ID otherwise); 
	///								name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[in] ePropertyType	The property type (IMcProperty::EPT_XXX_ARRAY)
    /// \param[out]	pValue			The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	template <typename T>
	inline IMcErrors::ECode GetArrayPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, IMcProperty::EPropertyType ePropertyType, 
		IMcProperty::SArrayProperty<T> *pValue) const
	{
		return GetPropertyDefault(NameOrID, ePropertyType, pValue);
	}

	//==============================================================================
	// Method Name: SetPropertyDefault()
	//------------------------------------------------------------------------------
	/// Updates the default value of the scheme's property.
	///
	/// \param[in] Property   The property name or ID (name if it is not empty, ID otherwise) along with type and value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetPropertyDefault(const IMcProperty::SVariantProperty &Property) = 0;

	//==============================================================================
	// Method Name: GetPropertyDefault()
	//------------------------------------------------------------------------------
	/// Retrieves the default value of the scheme's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise).
	/// \param[out]	pProperty	The property name, ID, type and default value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, IMcProperty::SVariantProperty *pProperty) const = 0;

	//==============================================================================
	// Method Name: SetPropertyDefaults()
	//------------------------------------------------------------------------------
	/// Updates the default values of several property.
	///
	/// \param[in] aProperties[]	The array of properties, each one is identified by name or ID (name if it is not empty, ID otherwise) and 
	///								contains type and value.
	/// \param[in] uNumProperties	the number of properties in the above array.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetPropertyDefaults(const IMcProperty::SVariantProperty aProperties[], 
												 UINT uNumProperties) = 0;

	//==============================================================================
	// Method Name: GetPropertyDefaults()
	//------------------------------------------------------------------------------
	/// Retrieves all the properties with their default values.
	///
	/// \param[out] paProperties	The array of properties, each one contains name, ID, type and default value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetPropertyDefaults(CMcDataArray<IMcProperty::SVariantProperty> *paProperties) const = 0;

	//@}

	/// \name Edit Mode parameters
	//@{

	//==============================================================================
	// Method Name: SetEditModeParams()
	//------------------------------------------------------------------------------
	/// Stores the parameters for EditMode's object initialization/editing operations.
	/// 
	/// Before starting EditMode's operation, retrieve the paramters by GetEditModeParams() and give them to EditMode by 
	/// IMcEditMode::ChangeObjectOperationsParams().
	///
	/// \param[in]	Params					The parameters for EditMode's object initialization/editing operations 
	///										(see IMcEditMode::SObjectOperationsParams for details).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetEditModeParams(const IMcEditMode::SObjectOperationsParams &Params) = 0;

	//==============================================================================
	// Method Name: GetEditModeParams()
	//------------------------------------------------------------------------------
	/// Retrieves the parameters for EditMode's object initialization/editing operations set by SetEditModeParams().
	///
	/// Before starting EditMode's operation, retrieve the paramters and give them to EditMode by IMcEditMode::ChangeObjectOperationsParams().
	///
	/// \param[out]	pParams					The parameters for EditMode's object initialization/editing operations 
	///										(see IMcEditMode::SObjectOperationsParams for details).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetEditModeParams(IMcEditMode::SObjectOperationsParams *pParams) const = 0;

	//==============================================================================
	// Method Name: SetEditModeDefaultItem()
	//------------------------------------------------------------------------------
	/// Sets the default item to be used in EditMode's object initialization/editing operations.
	/// 
	/// When IMcEditMode::StartInitObject() or IMcEditMode::StartEditObject() is called without specifying an item, this item will be use.
	///
	/// \param[in]	pItem	The object scheme item to be used by IMcEditMode::StartInitObject() or IMcEditMode::StartEditObject() when called without 
	///						specifying an item (or `NULL` to cancel EditMode's default item)
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetEditModeDefaultItem(IMcObjectSchemeItem *pItem) = 0;

	//==============================================================================
	// Method Name: GetEditModeDefaultItem()
	//------------------------------------------------------------------------------
	/// Retrieves the default item to be used in EditMode's object initialization/editing operations set by SetEditModeDefaultItem().
	///
	/// When IMcEditMode::StartInitObject() or IMcEditMode::StartEditObject() is called without specifying an item, this item will be use.
	///
	/// \param[in]	ppItem	The object scheme item used by IMcEditMode::StartInitObject() or IMcEditMode::StartEditObject() when called without 
	///						specifying an item (or `NULL` if not defined)
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetEditModeDefaultItem(IMcObjectSchemeItem **ppItem) const = 0;

	//@}
};
