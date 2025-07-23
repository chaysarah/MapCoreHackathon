#pragma once
//==================================================================================
/// \file IMcOverlayManager.h
/// Interface for overlay manager (world of graphical objects)
//==================================================================================
 
#include "McExports.h"
#include "IMcBase.h"
#include "McCommonTypes.h"
#include "CMcDataArray.h"
#include "OverlayManager/IMcObject.h"
#include "Calculations/IMcSpatialQueries.h"

class IMcOverlay;
class IMcCollection;
class IMcGridCoordinateSystem;

//==================================================================================
// Interface Name: IMcOverlayManager
//----------------------------------------------------------------------------------
/// Interface for overlay manager (world of graphical objects)
//==================================================================================
class IMcOverlayManager : public virtual IMcBase
{
protected:
    virtual ~IMcOverlayManager() {}

public:

	//==============================================================================
	// Enum Name: ECollectionsMode
	//------------------------------------------------------------------------------
	/// The collection visibility mode controlling the visibility of overlays/objects belonging to several collections.
	//==============================================================================
	enum ECollectionsMode 
	{
		ECM_OR, ///< The collection visibility of an overlay/object will be true if at least one its collection is visible.
		ECM_AND	///< The collection visibility of an overlay/object will be true if all its collections are visible.
	};

	//==============================================================================
	// Enum Name: ESavingVersionCompatibility
	//------------------------------------------------------------------------------
	/// The compatibility of saved objects, object schemes and native vector map layers to MapCore's previous versions 
	//==============================================================================
	enum ESavingVersionCompatibility
	{
		ESVC_7_6_1	= 57,	///< Compatible to versions starting with 7.6.1
		ESVC_7_7_3	= 64,	///< Compatible to versions starting with 7.7.3
		ESVC_7_7_7	= 67,	///< Compatible to versions starting with 7.7.7
		ESVC_7_7_8	= 68,	///< Compatible to versions starting with 7.7.8
		ESVC_7_7_9	= 69,	///< Compatible to versions starting with 7.7.9
		ESVC_7_7_10	= 71,	///< Compatible to versions starting with 7.7.10
		ESVC_7_7_11	= 72,	///< Compatible to versions starting with 7.7.11
		ESVC_7_8_1	= 73,	///< Compatible to versions starting with 7.8.1
		ESVC_7_8_2	= 74,	///< Compatible to versions starting with 7.8.2
		ESVC_7_8_3	= 75,	///< Compatible to versions starting with 7.8.3
		ESVC_7_8_4	= 76,	///< Compatible to versions starting with 7.8.4
		ESVC_7_9_0	= 78,	///< Compatible to versions starting with 7.9.0
		ESVC_7_9_1	= 79,	///< Compatible to versions starting with 7.9.1
		ESVC_7_9_3	= 80,	///< Compatible to versions starting with 7.9.3
		ESVC_7_9_4  = 81,	///< Compatible to versions starting with 7.9.4
		ESVC_7_9_5  = 82,	///< Compatible to versions starting with 7.9.5
		ESVC_7_9_6  = 83,	///< Compatible to versions starting with 7.9.6
		ESVC_7_9_7  = 84,	///< Compatible to versions starting with 7.9.7
		ESVC_7_10_0 = 85,	///< Compatible to versions starting with 7.10.0
		ESVC_7_11_2 = 86,	///< Compatible to versions starting with 7.11.2
		ESVC_7_11_3 = 88,	///< Compatible to versions starting with 7.11.3
		ESVC_7_11_4 = 90,	///< Compatible to versions starting with 7.11.4
		ESVC_7_11_5 = 92,	///< Compatible to versions starting with 7.11.5
		ESVC_7_11_6 = 94,	///< Compatible to versions starting with 7.11.6
		ESVC_7_11_7 = 95,	///< Compatible to versions starting with 7.11.7
		ESVC_7_11_8 = 96,	///< Compatible to versions starting with 7.11.8
		ESVC_7_11_10 = 97,	///< Compatible to versions starting with 7.11.10
		ESVC_7_11_11 = 100,	///< Compatible to versions starting with 7.11.11
		ESVC_12_1_4 = 101,	///< Compatible to versions starting with 12.1.4
		ESVC_12_2_0 = 102,	///< Compatible to versions starting with 12.2.0
		ESVC_LATEST = 0		///< Compatible to versions starting with 12.2.0
	};

	/// The format of file or memory buffer for saved objects and object schemes 
	enum EStorageFormat
	{
		ESF_MAPCORE_BINARY,	///< MapCore's binary format
		ESF_JSON			///< JSON format
	};

	enum ESizePropertyIndex
	{
		ESPI_LINE_WIDTH = 0,
		ESPI_LINE_OUTLINE_WIDTH,
		ESPI_TEXT_SCALE,
		ESPI_TEXT_OUTLINE_WIDTH,
		ESPI_TEXT_MARGIN,
		ESPI_PICTURE_SIZE,
		ESPI_FILL_TEXTURE_SCALE,
		ESPI_ARROW,
		ESPI_ELLIPSE_ARC_RADIUS,
		ESPI_RECTANGLE_RADIUS,
		ESPI_LINE_EXPANSION_RADIUS,
		ESPI_MANUAL_GEOMETRY,
		ESPI_OFFSET,
		ESPI_EQUIDISTANT_DISTANCE,
		ESPI_NUM
	};

	/// Bits of types of item size properties that can be multiplied by factor (used as a bit field in SetItemSizeFactors()).
	enum ESizePropertyType
	{
		ESPT_LINE_WIDTH = (1 << ESPI_LINE_WIDTH),							///< The line width of line-based items
		ESPT_LINE_OUTLINE_WIDTH = (1 << ESPI_LINE_OUTLINE_WIDTH),			///< The outline width of line-based items
		ESPT_TEXT_SCALE = (1 << ESPI_TEXT_SCALE),							///< The text scale of text items
		ESPT_TEXT_OUTLINE_WIDTH = (1 << ESPI_TEXT_OUTLINE_WIDTH),			///< The outline width of text items
		ESPT_TEXT_MARGIN = (1 << ESPI_TEXT_MARGIN),							///< The margin of text items
		ESPT_PICTURE_SIZE = (1 << ESPI_PICTURE_SIZE),						///< The width and height of picture items
		ESPT_FILL_TEXTURE_SCALE = (1 << ESPI_FILL_TEXTURE_SCALE),			///< The fill-texture scale of closed-shape items
		ESPT_ARROW = (1 << ESPI_ARROW),										///< The head size and gap size of arrow items
		ESPT_ELLIPSE_ARC_RADIUS = (1 << ESPI_ELLIPSE_ARC_RADIUS),			///< The X/Y radius of ellipse and arc items
		ESPT_RECTANGLE_RADIUS = (1 << ESPI_RECTANGLE_RADIUS),				///< The X/Y radius of rectangle items
		ESPT_LINE_EXPANSION_RADIUS = (1 << ESPI_LINE_EXPANSION_RADIUS),		///< The radius of line-expansion items
		ESPT_MANUAL_GEOMETRY = (1 << ESPI_MANUAL_GEOMETRY),					///< The points coordinates (offsets relative to item�s location) manual-geometry items
		ESPT_OFFSET = (1 << ESPI_OFFSET),									///< The regular offset and distance-based vector-offset of symbolic items
		ESPT_EQUIDISTANT_DISTANCE = (1 << ESPI_EQUIDISTANT_DISTANCE),		///< The distance for equidistant attached-point of symbolic items

		ESPT_ALL_LINE = ESPT_LINE_WIDTH | ESPT_LINE_OUTLINE_WIDTH,			///< The line width and its outline width together (a combination of 
																			///<  #ESPT_LINE_WIDTH and #ESPT_LINE_OUTLINE_WIDTH bits)
		ESPT_ALL_TEXT = 
			ESPT_TEXT_SCALE | ESPT_TEXT_OUTLINE_WIDTH | ESPT_TEXT_MARGIN,	///< The text scale, its outline width and margins together (a combination of 
																			///<  #ESPT_TEXT_SCALE, #ESPT_TEXT_OUTLINE_WIDTH and #ESPT_TEXT_MARGIN bits)

		ESPT_ALL_RADIUS = 
			ESPT_ELLIPSE_ARC_RADIUS | ESPT_RECTANGLE_RADIUS | 
			ESPT_LINE_EXPANSION_RADIUS,										///< The radius of ellipse, arc, rectangle and line expension together 
																			///<  (a combination of #ESPT_ELLIPSE_ARC_RADIUS, #ESPT_RECTANGLE_RADIUS 
																			///<  and #ESPT_LINE_EXPANSION_RADIUS bits)

		ESPT_ALL = 0xFFFF													///< All the sizes together (a combination of all the bits)
	};

	/// asynchronous operation callback receiving operation results
	class IAsyncOperationCallback
#ifdef __EMSCRIPTEN__
		: public IMcEmscriptenCallbackRef
#endif
	{
	public:

		virtual ~IAsyncOperationCallback() {}

		//==============================================================================
		// Method Name: OnSaveObjectsAsRawVectorToFileResult()
		//------------------------------------------------------------------------------
		/// Called when asynchronous operation of IMcOverlay::SaveAllObjectsAsRawVectorData() or IMcOverlay::SaveObjectsAsRawVectorData() has been 
		/// completed successfully; returns the results.
		///
		/// \param[in]	strFileName			The name of the main file of raw vector data to save to (the file's extension defines the format, e.g. "kml", "kmz", "dxf").
		/// \param[in]	eStatus				The operation's status: success (IMcErrors::SUCCESS) or error.
		/// \param[in]	aAdditionalFiles   The array of names of the additional files saved required by the format.
		//==============================================================================
		virtual void OnSaveObjectsAsRawVectorToFileResult(PCSTR strFileName, IMcErrors::ECode eStatus,
			const CMcDataArray<CMcString> aAdditionalFiles) {}

		//==============================================================================
		// Method Name: OnSaveObjectsAsRawVectorToBufferResult()
		//------------------------------------------------------------------------------
		/// Called when asynchronous operation of IMcOverlay::SaveAllObjectsAsRawVectorData() or IMcOverlay::SaveObjectsAsRawVectorData() has been 
		/// completed successfully; returns the results.
		///
		/// \param[in]	strFileName			The name (with an extension, without a path) of the main file of raw vector data to save to 
		///									(the file's name defines the names of additional files required by the format, 
		///									the file's extension defines the format, e.g. "kml", "kmz", "dxf").
		/// \param[in]	eStatus				The operation's status: success (IMcErrors::SUCCESS) or error.
		/// \param[in]	auFileMemoryBuffer	The memory buffer for the main file.
		/// \param[in]	aAdditionalFiles	The array of relative names and memory buffers of the additional files saved required by the format.
		//==============================================================================
		virtual void OnSaveObjectsAsRawVectorToBufferResult(PCSTR strFileName, IMcErrors::ECode eStatus,
			const CMcDataArray<BYTE> &auFileMemoryBuffer, const CMcDataArray<SMcFileInMemory> &aAdditionalFiles) {}
	};

public:

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Create an overlay manager.
	///
	/// The overlay manager will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// it will be destroyed when its Release() method is called.
	///
	/// \param[out]	ppOverlayManager	The newly created overlay manager
	/// \param[in]	pCoordinateSystem	The coordinate system definition
	///
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(IMcOverlayManager **ppOverlayManager,
		IMcGridCoordinateSystem *pCoordinateSystem);

	//@}

	/// \name Viewports, Overlays And Collections
	//@{

	//==============================================================================
	// Method Name: GetViewportsIDs()
	//------------------------------------------------------------------------------
	/// Retrieves user-defined IDs (defined in IMcMapViewport::Create()) of all map viewports connected to the overlay manager.
	///
	/// \param[out] pauViewportsIDs		The array of IDs of all map viewports connected to the overlay manager.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetViewportsIDs(
		CMcDataArray<UINT> *pauViewportsIDs) const = 0;

	//==============================================================================
	// Method Name: GetOverlayByID()
	//------------------------------------------------------------------------------
	/// Retrieves the overlay manager's overlay based on its ID defined by IMcOverlay::SetID().
	///
	/// \param[in]	uOverlayID	The overlay ID to look for
	/// \param[out]	ppOverlay	The requested overlay if found
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetOverlayByID(UINT uOverlayID, IMcOverlay **ppOverlay) const = 0;

	//==============================================================================
    // Method Name: GetOverlays()
    //------------------------------------------------------------------------------
    /// Retrieves all the overlays of the overlay manager except for those created with
	///	\p bForInternalUse flag (see IMcOverlay::Create()).
	///
    /// \param[out] papOverlays		The array of overlays.
	///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetOverlays(
		CMcDataArray<IMcOverlay*> *papOverlays) const = 0;

	//==============================================================================
    // Method Name: GetCollections()
    //------------------------------------------------------------------------------
    /// Retrieves all the collections of the overlay manager.
	///
    /// \param[out] papCollections		The array of collections.
	///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetCollections(
		CMcDataArray<IMcCollection*> *papCollections) const = 0;

	//==============================================================================
	// Method Name: SetCollectionsMode()
	//------------------------------------------------------------------------------
	/// Sets the collections visibility mode controlling the visibility of overlays/objects belonging to several collections.
	/// 
	/// Either the mode in one specific viewport or the mode default in all viewports is set. Setting the mode default in all viewports 
	/// overrides a mode in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in]	eCollectionsMode	The collection visibility mode, see #ECollectionsMode.
	/// \param[in]	pMapViewport		The viewport to set the collection visibility mode for or `NULL` the mode default in all viewports.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCollectionsMode(
		ECollectionsMode eCollectionsMode, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetCollectionsMode()
	//------------------------------------------------------------------------------
	/// Retrieves the collections visibility mode set by SetCollectionsMode().
	/// 
	/// \param[out] peCollectionsMode	The collection visibility mode
	/// \param[in]	pMapViewport		The viewport to retrieve the collection visibility mode for or `NULL` the mode default in all viewports.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCollectionsMode(
		ECollectionsMode *peCollectionsMode, IMcMapViewport *pMapViewport = NULL) const = 0;

	//@}

	/// \name Object Schemes
	//@{

	//==============================================================================
	// Method Name: SetObjectSchemeLock()
	//------------------------------------------------------------------------------
	/// Locks or unlocks an object scheme. 
	///
	/// Locking object scheme prevents its deleting when the last object using it is removed.
	///
	/// \param[in]	pObjectScheme		The object scheme to lock.
	/// \param[in]	bLocked				Whether to lock or unlock the object scheme.
	///
	/// \note
	/// - Locking locked scheme or unlocking unlocked one does nothing.
	/// - When overlay manager is deleted all the locked schemes are unlocked and deleted.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectSchemeLock(IMcObjectScheme *pObjectScheme, bool bLocked) const = 0;

	//==============================================================================
	// Method Name: IsObjectSchemeLocked()
	//------------------------------------------------------------------------------
	/// Checks whether an object scheme is locked by SetObjectSchemeLock().
	///
	/// \param[in]	pObjectScheme		The object scheme to check.
	/// \param[out]	pbLocked			Whether the object scheme is locked or unlocked.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode IsObjectSchemeLocked(IMcObjectScheme *pObjectScheme, bool *pbLocked) const = 0;

	//==============================================================================
	// Method Name: GetObjectSchemeByID()
	//------------------------------------------------------------------------------
	/// Retrieves the overlay manager's object scheme based on its ID defined by IMcObjectScheme::SetID().
	///
	/// \param[in]	uObjectSchemeID		The object scheme ID to look for.
	/// \param[out]	ppObjectScheme		The requested object scheme if found.
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectSchemeByID(UINT uObjectSchemeID, 
												 IMcObjectScheme **ppObjectScheme) const = 0;

	//==============================================================================
	// Method Name: GetObjectSchemeByName()
	//------------------------------------------------------------------------------
	/// Retrieves the overlay manager's object scheme based on its name defined by IMcObjectScheme::SetName().
	///
	/// \param[in]	strObjectSchemeName		The object scheme name to look for.
	/// \param[out]	ppObjectScheme			The requested object scheme if found.
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectSchemeByName(PCSTR strObjectSchemeName,
		IMcObjectScheme **ppObjectScheme) const = 0;

	//==============================================================================
    // Method Name: GetObjectSchemes()
    //------------------------------------------------------------------------------
    /// Retrieves all object schemes of the overlay manager.
	///
    /// \param[out] papObjectSchemes	The array of object schemes.
	///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetObjectSchemes(
		CMcDataArray<IMcObjectScheme*> *papObjectSchemes) const = 0;

	//==============================================================================
	// Method Name: SaveAllObjectSchemes()
	//------------------------------------------------------------------------------
	/// Saves all object schemes of the overlay manager to a file. 
	///
	/// \param[in]	strFileName			The name of the file.
	/// \param[in]	eStorageFormat		The storage format; the default is IMcOverlayManager::ESF_MAPCORE_BINARY.
	/// \param[in]	eVersion			The compatibility to MapCore's previous versions; the default is IMcOverlayManager::ESVC_LATEST (no compatibility).
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SaveAllObjectSchemes(PCSTR strFileName, 
		EStorageFormat eStorageFormat = ESF_MAPCORE_BINARY, 
		ESavingVersionCompatibility eVersion = ESVC_LATEST) = 0;

	//==============================================================================
	// Method Name: SaveAllObjectSchemes()
	//------------------------------------------------------------------------------
	/// Saves all object schemes of the overlay manager to a memory buffer. 
	///
	/// \param[out] pauMemoryBuffer		The memory buffer filled by the function.
	/// \param[in]	eStorageFormat		The storage format; the default is IMcOverlayManager::ESF_MAPCORE_BINARY.
	/// \param[in]	eVersion			The compatibility to MapCore's previous versions; the default is IMcOverlayManager::ESVC_LATEST (no compatibility).
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SaveAllObjectSchemes(CMcDataArray<BYTE> *pauMemoryBuffer, 
		EStorageFormat eStorageFormat = ESF_MAPCORE_BINARY, 
		ESavingVersionCompatibility eVersion = ESVC_LATEST) = 0;

	//==============================================================================
	// Method Name: SaveObjectSchemes()
	//------------------------------------------------------------------------------
	/// Saves specified object schemes to a file. 
	///
	/// \param[in]	pSchemes			The array of schemes to save.
	/// \param[in]	uNumSchemes			The number of schemes in the above array.
	/// \param[in]	strFileName			The name of the file.
	/// \param[in]	eStorageFormat		The storage format; the default is IMcOverlayManager::ESF_MAPCORE_BINARY.
	/// \param[in]	eVersion			The compatibility to MapCore's previous versions; the default is IMcOverlayManager::ESVC_LATEST (no compatibility).
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SaveObjectSchemes(IMcObjectScheme* const pSchemes[], UINT uNumSchemes,
		PCSTR strFileName, EStorageFormat eStorageFormat = ESF_MAPCORE_BINARY, 
		ESavingVersionCompatibility eVersion = ESVC_LATEST) = 0;

	//==============================================================================
	// Method Name: SaveObjectSchemes()
	//------------------------------------------------------------------------------
	/// Saves specified object schemes to a memory buffer. 
	///
	/// \param[in]	pSchemes			The array of schemes to save.
	/// \param[in]	uNumSchemes			The number of schemes.
	/// \param[out]	pauMemoryBuffer		The memory buffer filled by the function.
	/// \param[in]	eStorageFormat		The storage format; the default is IMcOverlayManager::ESF_MAPCORE_BINARY.
	/// \param[in]	eVersion			The compatibility to MapCore's previous versions; the default is IMcOverlayManager::ESVC_LATEST (no compatibility).
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SaveObjectSchemes(IMcObjectScheme* const pSchemes[], UINT uNumSchemes,
		CMcDataArray<BYTE> *pauMemoryBuffer, 
		EStorageFormat eStorageFormat = ESF_MAPCORE_BINARY, 
		ESavingVersionCompatibility eVersion = ESVC_LATEST) = 0;

	//==============================================================================
	// Method Name: LoadObjectSchemes()
	//------------------------------------------------------------------------------
	/// Loads object schemes from a file (containing schemes or objects) previously saved by SaveAllObjectSchemes()/SaveObjectSchemes() or 
	/// IMcOverlay::SaveAllObjects()/SaveObjects().
	///
	/// \param[in]	strFileName				The name of the file.
	/// \param[in]	pUserDataFactory		The optional user-defined factory for creation of user data instances saved with the scheme.
	/// \param[out]	papLoadedSchemes		The optional array of loaded schemes; filled by the function (pass `NULL` if unnecessary).
	/// \param[out]	pbObjectDataDetected	The optional indication whether the file contains objects (in this case their schemes are loaded); 
	///										filled by the function (pass `NULL` if unnecessary).
	/// \param[out]	peStorageFormat			The optional storage format; filled by the function (pass `NULL` if unnecessary).
	/// \param[out]	puVersion				The optional storage version number; filled by the function (pass `NULL` if unnecessary); 
	///										for schemes saved by an official release of MapCore, the version number will be equal to a numerical value 
	///										of one of the enumerators of IMcOverlayManager::ESavingVersionCompatibility.
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode LoadObjectSchemes(
		PCSTR strFileName, 
		IMcUserDataFactory *pUserDataFactory = NULL, 
		CMcDataArray<IMcObjectScheme*> *papLoadedSchemes = NULL, 
		bool *pbObjectDataDetected = NULL,
		EStorageFormat *peStorageFormat = NULL,
		UINT *puVersion = NULL) = 0;

	//==============================================================================
	// Method Name: LoadObjectSchemes()
	//------------------------------------------------------------------------------
	/// Loads object schemes from a memory buffer (containing schemes or objects) previously saved by SaveAllObjectSchemes()/SaveObjectSchemes() or 
	/// IMcOverlay::SaveAllObjects()/SaveObjects().
	///
	/// \param[in]  abMemoryBuffer			The memory buffer to read from.
	/// \param[in]  uBufferSize				The buffer size.
	/// \param[in]	pUserDataFactory		The optional user-defined factory for creation of user data instances saved with the scheme.
	/// \param[out]	papLoadedSchemes		The optional array of loaded schemes; filled by the function (pass `NULL` if unnecessary).
	/// \param[out]	pbObjectDataDetected	The optional indication whether the memeory buffer contains objects (in this case their schemes are loaded); 
	///										filled by the function (pass `NULL` if unnecessary).
	/// \param[out]	peStorageFormat			The optional storage format; filled by the function (pass `NULL` if unnecessary).
	/// \param[out]	puVersion				The optional storage version number; filled by the function (pass `NULL` if unnecessary); 
	///										for schemes saved by an official release of MapCore, the version number will be equal to a numerical value 
	///										of one of the enumerators of IMcOverlayManager::ESavingVersionCompatibility.
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode LoadObjectSchemes(
		const BYTE abMemoryBuffer[], UINT uBufferSize, 
		IMcUserDataFactory *pUserDataFactory = NULL, 
		CMcDataArray<IMcObjectScheme*> *papLoadedSchemes = NULL, 
		bool *pbObjectDataDetected = NULL,
		EStorageFormat *peStorageFormat = NULL,
		UINT *puVersion = NULL) = 0;

	//@}

	/// \name Conditional Selectors
	//@{

	//==============================================================================
	// Method Name: SetConditionalSelectorLock()
	//------------------------------------------------------------------------------
	/// Locks or unlocks conditional selector. 
	///
	/// Locking conditional selector prevents its deleting when the last item or object using it is removed.
	///
	/// \param[in]	pSelector		The conditional selector to lock.
	/// \param[in]	bLocked			Whether to lock or unlock the conditional selector.
	///
	/// \note
	/// - Locking locked selector or unlocking unlocked one does nothing.
	/// - When overlay manager is deleted all the locked selectors are unlocked and deleted.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetConditionalSelectorLock(IMcConditionalSelector *pSelector, bool bLocked) const = 0;

	//==============================================================================
	// Method Name: IsConditionalSelectorLocked()
	//------------------------------------------------------------------------------
	/// Checks if conditional selector is locked or unlocked.
	///
	/// \param[in]	pSelector		The conditional selector to check.
	/// \param[out]	pbLocked		Whether the conditional selector is locked or unlocked.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode IsConditionalSelectorLocked(IMcConditionalSelector *pSelector, bool *pbLocked) const = 0;

	//==============================================================================
	// Method Name: GetConditionalSelectorByID()
	//------------------------------------------------------------------------------
	/// Retrieves the conditional selector based on its ID defined by IMcConditionalSelector::SetID().
	///
	/// \param[in] uSelectorID		The selector ID to look for.
	/// \param[out] ppSelector		The requested selector, if found.
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetConditionalSelectorByID (
		UINT uSelectorID, IMcConditionalSelector **ppSelector) const = 0;

	//==============================================================================
	// Method Name: GetConditionalSelectorByName()
	//------------------------------------------------------------------------------
	/// Retrieves the conditional selector based on its name defined by IMcConditionalSelector::SetName().
	///
	/// \param[in] strSelectorName	The selector name to look for.
	/// \param[out] ppSelector		The requested selector, if found.
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetConditionalSelectorByName(
		PCSTR strSelectorName, IMcConditionalSelector **ppSelector) const = 0;

	//==============================================================================
	// Method Name: GetConditionalSelectors()
	//------------------------------------------------------------------------------
    /// Retrieves all conditional selectors of the overlay manager.
	///
	/// \param[out] papSelectors	The array of selectors.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetConditionalSelectors(
		CMcDataArray<IMcConditionalSelector*> *papSelectors) const = 0;

	//@}

	/// \name Factors and States
	//@{

	//==============================================================================
	// Method Name: SetItemSizeFactors()
	//------------------------------------------------------------------------------
	/// Sets the size factor for multiplication of specified item size properties of either vector items or object scheme items.
	///
	/// \param[in] eSizeTypesBitField	The bit field of size property types (based on #ESizePropertyType) to be multiplied by the factor.
	/// \param[in] fSizeFactor			The size factor for multiplication.
	/// \param[in] pMapViewport			The viewport to set size factors for or `NULL` for the size factor default in all viewports.
	/// \param[in] bVectorItems			Which size factor to set: whether affecting only vector items or only object scheme items.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetItemSizeFactors(UINT eSizeTypesBitField, float fSizeFactor, IMcMapViewport *pMapViewport = NULL, bool bVectorItems = false) = 0;

	//==============================================================================
	// Method Name: GetItemSizeFactor()
	//------------------------------------------------------------------------------
	/// Retrieves the size factor for multiplication of a single item size property set by SetItemSizeFactors().
	///
	/// \param[in]	eSizeType			The size property type multiplied by the factor (see #ESizePropertyType).
	/// \param[out]	pfSizeFactor		The size factor for multiplication.
	/// \param[in] pMapViewport			The viewport to set size factors for or `NULL` for the size factor default in all viewports.
	/// \param[in] bVectorItems			Which size factor to retrieve: whether affecting only vector items or only object scheme items.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetItemSizeFactor(ESizePropertyType eSizeType, float *pfSizeFactor, IMcMapViewport *pMapViewport = NULL, bool bVectorItems = false) const = 0;

	//==============================================================================
	// Method Name: SetScaleFactor()
	//------------------------------------------------------------------------------
	/// Sets the scale factor to be applied to scale ranges of scale conditional selectors and 
	/// of map viewport when an object scale is checked against these ranges.
	///
	/// Either the factor in one specific viewport or the factor default in all viewports is set. Setting the factor default in all viewports 
	/// overrides a factor in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] fScaleFactor		The scale factor; the default is 1.
	/// \param[in] pMapViewport		The viewport to set the factor for or `NULL` for the factor default in all viewports.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetScaleFactor(
		float fScaleFactor, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetScaleFactor()
	//------------------------------------------------------------------------------
	/// Retrieves the scale factor set by SetScaleFactor().
	///
	/// Either the factor in one specific viewport or the factor default in all viewports is retrieved.
	///
	/// \param[out]	pfScaleFactor	The scale factor; the default is 1.
	/// \param[in]	pMapViewport	The viewport to retrieve the factor for or `NULL` for the factor default in all viewports.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetScaleFactor(
		float *pfScaleFactor, IMcMapViewport *pMapViewport = NULL) const = 0;

	//==============================================================================
	// Method Name: SetState()
	//------------------------------------------------------------------------------
	/// Sets the overlay manager's state of as a single sub-state.
	///
	/// Either the state in one specific viewport or the state default in all viewports is set. Setting the state default in all viewports 
	/// overrides a state in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] uState			The overlay manager's new state a single sub-state.
	/// \param[in] pMapViewport		The viewport to set state for or `NULL` for the state default in all viewports.
	///
	/// \note
	/// - The object's state determines the versions of the properties to be used. 
	/// - The actual versions of properties used by an object in a specific viewport at specific time are determined according to 
	///   the object's current effective state in the specified viewport at that time, see IMcObject::GetEffectiveState() for details.
	/// - If an object's effective state consists of several sub-states, the property version is that of the first sub-state defined 
	///   for the property.
	/// - If none of object sub-states is defined for the property, zero-state property version is used.
	/// 
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetState(BYTE uState, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: SetState()
	//------------------------------------------------------------------------------
	/// Sets the overlay manager's state of as an array of sub-states.
	///
	/// Either the state in one specific viewport or the state default in all viewports is set. Setting the state default in all viewports 
	/// overrides a state in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] auStates			The overlay manager's new state as an array of sub-states.
	/// \param[in] uNumStates		The number of sub-states in the above array.
	/// \param[in] pMapViewport		The viewport to set the state for or `NULL` for the state default in all viewports.
	///
	/// \note
	/// - The object's state determines the versions of the properties to be used. 
	/// - The actual versions of properties used by an object in a specific viewport at specific time are determined according to 
	///   the object's current effective state in the specified viewport at that time, see IMcObject::GetEffectiveState() for details.
	/// - If an object's effective state consists of several sub-states, the property version is that of the first sub-state defined 
	///   for the property.
	/// - If none of object sub-states is defined for the property, zero-state property version is used.
	/// 
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetState(const BYTE auStates[], UINT uNumStates, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetState()
	//------------------------------------------------------------------------------
	/// Retrieves the overlay manager's state set by SetState().
	///
	/// Either the state in one specific viewport or the state default in all viewports is retrieved.
	///
	/// \param[out] pauStates		The array of the overlay manager's sub-states.
	/// \param[in]	pMapViewport	The viewport to retrieve the state for or `NULL` for the state default in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetState(CMcDataArray<BYTE> *pauStates, IMcMapViewport *pMapViewport = NULL) const = 0;

	//@}

	/// \name Screen Arrangement
	//@{

	//==============================================================================
	// Method Name: SetScreenArrangement()
	//------------------------------------------------------------------------------
	/// Arranges the specified objects in the given viewport by adding screen arrangement offsets to 
	/// their items defined by IMcObjectScheme::SetObjectScreenArrangementItem() to prevent overlapping.
	///
	/// Screen arrangement offsets are calculated by arranging screen bounding rectangles of the defined 
	/// items along with their children to prevent overlapping. An arrangement of every object can be 
	/// later updated or canceled by the user (see IMcObject::SetScreenArrangementOffset()). 
	/// All the arrangements can be canceled by calling CancelScreenArrangements().
	///
	/// \param[in] pMapViewport		The viewport to arrange the objects in.
	/// \param[in] apObjects		The array of objects to arrange.
	/// \param[in] uNumObjects		The number of objects in the above array.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetScreenArrangement(
		IMcMapViewport *pMapViewport, IMcObject *const apObjects[], UINT uNumObjects) = 0;

	//==============================================================================
	// Method Name: CancelScreenArrangements()
	//------------------------------------------------------------------------------
	/// Cancel all the screen arrangements of objects for the given viewport set by 
	/// SetScreenArrangement() or IMcObject::SetScreenArrangementOffset(). 
	///
	/// \param[in] pMapViewport		The viewport to cancel arrangements in.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode CancelScreenArrangements(
		IMcMapViewport *pMapViewport) = 0;

	//@}

	/// \name Parameters for Symbolic Items
	//@{

	//==============================================================================
	// Method Name: SetEquidistantAttachPointsMinScale()
	//------------------------------------------------------------------------------
	/// Sets the minimal scale for calculation of attach points of type IMcSymbolicItem::EAPT_SCREEN_EQUIDISTANT.
	/// 
	/// In smaller scales the positions of attach points will be taken from the minimal scale specified.
	///
	/// Either the scale in one specific viewport or the scale default in all viewports is set. Setting the scale default in all viewports 
	/// overrides a scale in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] fMinScale		The minimal scale; the default is 1.
	/// \param[in] pMapViewport		The viewport to set the scale for or `NULL` for the scale default in all viewports.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetEquidistantAttachPointsMinScale(
		float fMinScale, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetEquidistantAttachPointsMinScale()
	//------------------------------------------------------------------------------
	/// Retrieves the minimal scale for calculation of attach points of type IMcSymbolicItem::EAPT_SCREEN_EQUIDISTANT set by 
	/// SetEquidistantAttachPointsMinScale().
	/// 
	/// Either the scale in one specific viewport or the scale default in all viewports is retrieved.
	///
	/// \param[out] pfMinScale		The minimal scale; the default is 1.
	/// \param[in]	pMapViewport	The viewport to retrieve the scale for or `NULL` for the scale default in all viewports.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetEquidistantAttachPointsMinScale(
		float *pfMinScale, IMcMapViewport *pMapViewport = NULL) const = 0;

	//==============================================================================
	// Method Name: SetScreenTerrainItemsConsistencyScaleSteps()
	//------------------------------------------------------------------------------
	/// Sets scale steps for consistency of screen-size items attached to terrain in 3D viewport in object schemes with terrain-items-consistency mode 
	/// enabled by IMcObjectScheme::SetTerrainItemsConsistency().
	/// 
	/// Defines at which values of objects' local scale their items' world sizes should be updated 
	/// according to the required screen sizes.
	///
	/// \param[in]	afScaleSteps	The array of scale steps (should be positive values); 
	///								the default is 4 steps: 0.5, 1, 2, 4, 8
	/// \param[in]	uNumScaleSteps	The number of scale steps in the above array 
	///								(or zero to disable the whole feature)
	/// \param[in] pMapViewport		The viewport to set the scale steps for or `NULL` for the scale steps default in all viewports.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetScreenTerrainItemsConsistencyScaleSteps(
		const float afScaleSteps[], UINT uNumScaleSteps, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetScreenTerrainItemsConsistencyScaleSteps()
	//------------------------------------------------------------------------------
	/// Retrieves scale steps for consistency of screen-size items attached to terrain in 3D viewport set by SetScreenTerrainItemsConsistencyScaleSteps().
	/// 
	/// \param[out]	pafScaleSteps	The array of scale steps; 
	///								(no scale steps means the whole feature is disabled)
	/// \param[in]	pMapViewport	The viewport to retrieve the scale steps for or `NULL` for the scale steps default in all viewports.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetScreenTerrainItemsConsistencyScaleSteps(
		CMcDataArray<float> *pafScaleSteps, IMcMapViewport *pMapViewport = NULL) const = 0;

	//@}

	/// \name Special Modes for Object Scheme Items
	//@{

	//==============================================================================
	// Method Name: SetCancelScaleMode()
	//------------------------------------------------------------------------------
	/// Sets which cancel-scale modes are on for scale conditional selectors sensitive to these modes.
	///
	/// A scale conditional selector is sensitive to the mode if an appropriate bit is set by IMcScaleConditionalSelector::SetCancelScaleMode(). 
	/// When this bit is set in IMcOverlayManager::SetCancelScaleMode() the selector will ignore its scale range and its result will be forced to be 
	/// true or false according to the appropprite bit set by IMcScaleConditionalSelector::SetCancelScaleModeResult().
	///
	/// Either the mode in one specific viewport or the mode default in all viewports is set. Setting the mode default in all viewports 
	/// overrides a mode in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] uModeBitField	The enabled modes as a mask of 32 bits; the default is 0.
	/// \param[in] pMapViewport		The viewport to set the mode for or `NULL` for the mode default in all viewports.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCancelScaleMode(
		UINT uModeBitField, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetCancelScaleMode()
	//------------------------------------------------------------------------------
	/// Retrieves cancel-scale modes set by SetCancelScaleMode().
	///
	/// A scale conditional selector is sensitive to the mode if an appropriate bit is set by IMcScaleConditionalSelector::SetCancelScaleMode(). 
	/// When this bit is set in IMcOverlayManager::SetCancelScaleMode() the selector will ignore its scale range and its result will be forced to be 
	/// true or false according to the appropprite bit set by IMcScaleConditionalSelector::SetCancelScaleModeResult().
	///
	/// Either the mode in one specific viewport or the mode default in all viewports is retrieved.
	///
	/// \param[out] puModeBitField	The enabled modes as a mask of 32 bits; the default is 0.
	/// \param[in]	pMapViewport	The viewport to retrieve the mode for or `NULL` for the mode default in all viewports.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCancelScaleMode(
		UINT *puModeBitField, IMcMapViewport *pMapViewport = NULL) const = 0;

	//==============================================================================
    // Method Name: SetMoveIfBlockedMode()
    //------------------------------------------------------------------------------
    /// Enables/disables the move-if-blocked mode for symbolic items with non-zero move-if-blocked maximum height change 
    /// defined by IMcSymbolicItem::SetMoveIfBlockedMaxChange().
    ///
	/// Either the mode in one specific viewport or the mode default in all viewports is set. Setting the mode default in all viewports 
	/// overrides a mode in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] bMoveIfBlocked		Whether the move-if-blocked mode should be enabled or disabled; the default is false.
	/// \param[in] pMapViewport			The viewport to set the mode for or `NULL` for the mode default in all viewports.
	///
    /// \return
    ///     - Status result
    //==============================================================================
	virtual IMcErrors::ECode SetMoveIfBlockedMode(
		bool bMoveIfBlocked, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
    // Method Name: GetMoveIfBlockedMode()
    //------------------------------------------------------------------------------
    /// Retrieves the move-if-blocked mode set by SetMoveIfBlockedMode().
    ///
	/// Either the mode in one specific viewport or the mode default in all viewports is retrieved.
	///
	/// \param[out] pbMoveIfBlocked		Whether the move-if-blocked mode is enabled or disabled; the default is false.
	/// \param[in]	pMapViewport		The viewport to retrieve the mode for or `NULL` for the mode default in all viewports.
	///
    /// \return
    ///     - Status result
    //==============================================================================
	virtual IMcErrors::ECode GetMoveIfBlockedMode(
		bool *pbMoveIfBlocked, IMcMapViewport *pMapViewport = NULL) const = 0;

	//==============================================================================
    // Method Name: SetBlockedTransparencyMode()
    //------------------------------------------------------------------------------
    /// Enables/disables the blocked-transparency mode for object scheme items with non-zero blocked transparency 
    /// defined by IMcObjectSchemeItem::SetBlockedTransparency().
    ///
	/// Either the mode in one specific viewport or the mode default in all viewports is set. Setting the mode default in all viewports 
	/// overrides a mode in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] bBlockedTransparency		Whether the blocked-transparency mode should be enabled or disabled; the default is true.
	/// \param[in] pMapViewport				The viewport to set the mode for or `NULL` for the mode default in all viewports.
	///
    /// \return
    ///     - Status result
    //==============================================================================
	virtual IMcErrors::ECode SetBlockedTransparencyMode(
		bool bBlockedTransparency, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
    // Method Name: GetBlockedTransparencyMode()
    //------------------------------------------------------------------------------
    /// Retrieves the blocked-transparency mode set by SetBlockedTransparencyMode().
    ///
	/// Either the mode in one specific viewport or the mode default in all viewports is retrieved.
	///
	/// \param[out] pbBlockedTransparency		Whether the blocked-transparency mode is enabled or disabled; the default is true.
	/// \param[in]	pMapViewport				The viewport to retrieve the mode for or `NULL` for the mode default in all viewports.
	///
    /// \return
    ///     - Status result
    //==============================================================================
	virtual IMcErrors::ECode GetBlockedTransparencyMode(
		bool *pbBlockedTransparency, IMcMapViewport *pMapViewport = NULL) const = 0;

	//==============================================================================
    // Method Name: SetTopMostMode()
    //------------------------------------------------------------------------------
    /// Enables/disables considering top-most draw priority group of symbolic items defined by 
	/// IMcSymbolicItem::SetDrawPriorityGroup() (the default is true).
	///
	/// Either the mode in one specific viewport or the mode default in all viewports is set. Setting the mode default in all viewports 
	/// overrides a mode in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] bTopMost			Whether the top-most mode should be enabled or disabled; the default is false.
	/// \param[in] pMapViewport		The viewport to set the mode for or `NULL` for the mode default in all viewports.
	///
    /// \return
    ///     - Status result
    //==============================================================================
	virtual IMcErrors::ECode SetTopMostMode(
		bool bTopMost, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
    // Method Name: GetTopMostMode()
    //------------------------------------------------------------------------------
	/// Retrieves the top-most mode set by SetTopMostMode().
    ///
	/// Either the mode in one specific viewport or the mode default in all viewports is retrieved.
	///
	/// \param[out] pbTopMost		Whether the top-most mode is enabled or disabled; the default is false.
	/// \param[in]	pMapViewport	The viewport to retrieve the mode for or `NULL` for the mode default in all viewports.
	///
    /// \return
    ///     - Status result
    //==============================================================================
	virtual IMcErrors::ECode GetTopMostMode(
		bool *pbTopMost, IMcMapViewport *pMapViewport = NULL) const = 0;

	//@}

	/// \name Coordinate System
	//@{

    //==============================================================================
    // Method Name: GetCoordinateSystemDefinition()
    //------------------------------------------------------------------------------
    /// Retrieves the overlay manager's coordinate system definition.
	/// 
	/// \param[out] pCoordinateSystem	The coordinate system definition.
	///
    /// \return
    ///     - status result
    //==============================================================================
	virtual IMcErrors::ECode GetCoordinateSystemDefinition(IMcGridCoordinateSystem **pCoordinateSystem) const = 0;

	//==============================================================================
    // Method Name: ConvertWorldToImage()
    //------------------------------------------------------------------------------
	/// Converts point from the overlay manager's coordinate system into an image coordinate system of a specified IMcImageCalc.
	///
	/// \param[in]  WorldPoint		The point in the overlay manager'a coordinate system.
	/// \param[out] *pImagePoint	The resulting point converted into the image coordinate system of \p pImageCalc.
	/// \param[in]  pImageCalc		IMcImageCalc interface defining image coordinate system.
	/// \param[out]	peIntersectionStatus	Whether the image point was found
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode ConvertWorldToImage(
		const SMcVector3D &WorldPoint, SMcVector3D *pImagePoint,
		const IMcImageCalc *pImageCalc,
		IMcErrors::ECode *peIntersectionStatus = NULL) const = 0;

    //==============================================================================
    // Method Name: ConvertImageToWorld()
    //------------------------------------------------------------------------------
	/// Converts point from an image coordinate system of a specified IMcImageCalc into the overlay manager's coordinate system.
	///
	/// \param[in]  ImagePoint	The point to convert from image coordinate system
	/// \param[out] pWorldPoint	The result point in the image coordinate system of \p pImageCalc.
	/// \param[in]  pImageCalc	IMcImageCalc interface defining image coordinate system.
	/// \param[out]	peIntersectionStatus	Whether the world point was found
	/// \param[in]	pAsyncQueryCallback		optional callback for asynchronous query; if not NULL: the query will be performed asynchronously; 
	///										the function will return without results, when the results are ready a callback function 
	///										will be called IMcSpatialQueries::IAsyncQueryCallback::OnRayIntersectionResults(); 
	///										the same callback instance can be used again in another query only after the first query is completed; 
	///										the arguments of the callback function will be:
	///										- `bIntersectionFound = (*peIntersectionStatus == IMcErrors::SUCCESS)`
	///										- `*pIntersection = *pWorldCoord`
	///										- `pNormal`: unused
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode ConvertImageToWorld(
		const SMcVector3D &ImagePoint, SMcVector3D *pWorldPoint,
		const IMcImageCalc *pImageCalc,
		IMcErrors::ECode *peIntersectionStatus = NULL,
		IMcSpatialQueries::IAsyncQueryCallback *pAsyncQueryCallback = NULL) const = 0;
	
	//@}

	//@}

	/// \name Symbology Standard
	//@{

	//==============================================================================
	// Method Name: InitializeSymbologyStandardSupport()
	//------------------------------------------------------------------------------
	/// Initialize the support of the specified symbology standard.
	/// 
	/// For each symbology standard it should be called once before creating objects of that standard.
	/// 
	/// \param[in]	eSymbologyStandard				The symbology standard.
	/// \param[in]	bShowGeoInMetricProportion		Whether to be used in map viewports created with 
	/// \param[in]	bIgnoreNonExistentAmplifiers	Whether setting non-existent amplifiers (geometric and non-geometric) by IMcObject's symbology functions 
	///												should be ignored or should fail with IMcErrors::SYMBOLOGY_AMPLIFIER_NOT_FOUND error code; 
	///												the default is true (i.e. to ignore).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode InitializeSymbologyStandardSupport(
		IMcObject::ESymbologyStandard eSymbologyStandard, bool bShowGeoInMetricProportion, bool bIgnoreNonExistentAmplifiers = true) = 0;

	//==============================================================================
	// Method Name: GetSymbologyStandardNames()
	//------------------------------------------------------------------------------
	/// Retrieves the names of the symbology standard's amplifiers relevant for the specified object type.
	///
	/// \param[in]	eSymbologyStandard		The symbology standard.
	/// \param[in]	strSymbolID				The symbol ID string that identifies the object in the standard and defines its graphical attributes.
	/// \param[out]	paGeometricAmplifiers	The names of the standard's geometric amplifiers relevant for the object type identified by \p strSymbolID
	/// \param[out]	pastrAmplifiers			The names of the standard's non-geometric amplifiers relevant for the object type identified by \p strSymbolID
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSymbologyStandardNames(IMcObject::ESymbologyStandard eSymbologyStandard, PCSTR strSymbolID,
		CMcDataArray<IMcObject::SMultiKeyName> *paGeometricAmplifiers, CMcDataArray<PCSTR> *pastrAmplifiers) const = 0;
	//@}
};
