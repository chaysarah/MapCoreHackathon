#pragma once
//==================================================================================
/// \file IMcOverlay.h
/// Interface for overlay of graphical objects
//==================================================================================

#include "McExports.h"
#include "IMcBase.h"
#include "McCommonTypes.h"
#include "CMcDataArray.h"
#include "OverlayManager/IMcOverlayManager.h"
#include "OverlayManager/IMcConditionalSelector.h"
#include "Map/IMcMapLayer.h"
#include "Map/IMcVectorMapLayer.h"

class IMcObject;
class IMcOverlayManager;
class IMcCollection;
class IMcMapViewport;

//==================================================================================
// Interface Name: IMcOverlay
//----------------------------------------------------------------------------------
/// Interface for overlay of graphical objects
//==================================================================================
class IMcOverlay : public virtual IMcBase
{
public:
	/// Flags of color components that can be overridden in SetColorOverriding() (as a part of SColorPropertyOverriding).
	///
	/// For each component type (RGB or alpha), only a single flag can be turned on at the same time,
	/// but it is possible to combine an ECCF_XXX_RGB flag with an ECCF_XXX_ALPHA flag.
	enum EColorComponentFlags
	{
		ECCF_NONE = 0x0000,							///< None is overridden.

		ECCF_REPLACE_RGB = 0x0001,					///< Replace RGB components with overriding values.
		ECCF_REPLACE_ALPHA = 0x0002,				///< Replace an alpha component with overriding values.

		ECCF_ADD_RGB = 0x0100,						///< RGB components to override are calculated as
													///<  original plus overriding values.
		ECCF_SUB_RGB = 0x0200,						///< RGB components to override are calculated as
													///<  original minus overriding values.

		ECCF_ADD_ALPHA = 0x0400,					///< Alpha components to override are calculated as
													///<  original plus overriding values.
		ECCF_SUB_ALPHA = 0x0800,					///< Alpha components to override are calculated as
													///<  original minus overriding values.

		ECCF_MODULATE_RGB = 0x1000,					///< RGB components to override are calculated as
													///<  a modulation (multiplication) of original by overriding values
		ECCF_MODULATE_ALPHA = 0x2000,				///< Alpha component to override is calculated as
													///<  a modulation (multiplication) of original by overriding values.

		ECCF_POSTPROCESS_ADD_RGB = 0x4000,			///< RGB components to override at PostProcess are calculated as
													///<  original plus overriding values; 
													///<  not relevant for sight presentation colors other than #ECPT_SIGHT_SEEN.
		ECCF_POSTPROCESS_SUB_RGB = 0x8000,			///< RGB components to override at PostProcess are calculated as
													///<  original minus overriding values; 
													///<  not relevant for sight presentation colors other than #ECPT_SIGHT_SEEN.

		ECCF_RGB_FLAGS = (ECCF_REPLACE_RGB | ECCF_ADD_RGB | ECCF_SUB_RGB | ECCF_MODULATE_RGB | ECCF_POSTPROCESS_ADD_RGB | ECCF_POSTPROCESS_SUB_RGB),
													///< A combination of all RGB flags.
		ECCF_ALPHA_FLAGS = (ECCF_REPLACE_ALPHA | ECCF_ADD_ALPHA | ECCF_SUB_ALPHA | ECCF_MODULATE_ALPHA)
													///< A combination of all alpha flags.
	};

	/// Types of color properties that can be overridden in SetColorOverriding().
	enum EColorPropertyType
	{
		ECPT_LINE,						///< The line color of line-based items.
		ECPT_FILL,						///< The fill color of closed-shape items.
		ECPT_TEXT,						///< The text color of text items.
		ECPT_TEXT_BACKGROUND,			///< The text background color of text items.
		ECPT_PICTURE,					///< The texture-modulation color of picture items.
		ECPT_SIGHT_SEEN,				///< The seen area color of sight presentation.
		ECPT_SIGHT_UNSEEN,				///< The unseen area color of sight presentation.
		ECPT_SIGHT_UNKNOWN,				///< The unknown area color of sight presentation.
		ECPT_SIGHT_OUT_OF_QUERY_AREA,	///< The out-of-query-area color of sight presentation.
		ECPT_SIGHT_SEEN_STATIC_OBJECT,	///< The seen static object color of sight presentation.
		ECPT_SIGHT_ASYNC_CALCULATING,	///< The async calculating color of sight presentation.
		ECPT_TRAVERSABILITY_TRAVERSABLE,		///< The traversable color of traversability presentation.
		ECPT_TRAVERSABILITY_UNTRAVERSABLE,		///< The untraversable area color of traversability presentation.
		ECPT_TRAVERSABILITY_UNKNOWN,			///< The unknown traversabilty color of traversabilty presentation.
		ECPT_TRAVERSABILITY_ASYNC_CALCULATING,	///< The async calculating color of traversabilty presentation.
		ECPT_LINE_OUTLINE,				///< The line outline color of line-based items.
		ECPT_TEXT_OUTLINE,				///< The text outline color of text items.
		ECPT_MANUAL_GEOMETRY,			///< The point color of manual geometry items.
		ECPT_NUM						///< The number of the enum's members (not to be used as a color type).
	};

	/// Parameters of color overriding used in SetColorOverriding().
	struct SColorPropertyOverriding
	{
		SColorPropertyOverriding()
		{
			memset(this, 0, sizeof(*this));
			//bEnabled = false;
		}

		bool operator != (const SColorPropertyOverriding &Other) const
		{ 
			return (Color != Other.Color ||
					uColorComponentsBitField != Other.uColorComponentsBitField ||
					bEnabled != Other.bEnabled);
		}

		SMcBColor			Color;						///< The overriding color's value.
		UINT				uColorComponentsBitField;	///< The color components to override (a bit field based on #EColorComponentFlags).
		bool				bEnabled;					///< Whether or not the overriding is enabled.
	};

protected:
	virtual ~IMcOverlay() {};

public:

	/// \name Create and Remove
	//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates an overlay.
	///
	/// The overlay will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Remove() method is called,
	/// or when its overlay manger is destroyed.
	///
	/// \param[out] ppCreatedOverlay	The newly created overlay.
	/// \param[in]  pOverlayManager		The overlay's overlay manager.
	/// \param[in]  bForInternalUse		Whether the overlay is for internal use only.
	///									(internal-use overlay is not returned by 
	///									IMcOverlayManager::GetOverlays()).
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcOverlay **ppCreatedOverlay,
		IMcOverlayManager *pOverlayManager,
		bool bForInternalUse = false);

	//==============================================================================
	// Method Name: Remove()
	//------------------------------------------------------------------------------
	/// Removes the overlay from its overlay manager
	/// and destroys it (with all its objects).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Remove() = 0;

	//@}

	/// \name Load/Save
	//@{

    //==============================================================================
    // Method Name: LoadObjects()
    //------------------------------------------------------------------------------
	/// Loads overlay's objects previously saved by SaveAllObjects() or SaveObjects() 
	/// from a file. 
	///
	/// \param[in]	strFileName			The name of the file.
	/// \param[in]	pUserDataFactory	The optional user-defined factory that creates user data instances.
	/// \param[out]	papLoadedObjects	The optional array of loaded objects; filled by the function (pass `NULL` if unnecessary).
	/// \param[out]	peStorageFormat		The optional storage format; filled by the function (pass `NULL` if unnecessary).
	/// \param[out]	puVersion			The optional storage version number; filled by the function (pass `NULL` if unnecessary); 
	///									for objects saved by an official release of MapCore, the version number will be equal to a numerical value 
	///									of one of the enumerators of IMcOverlayManager::ESavingVersionCompatibility.
    /// \return
    ///     - Status result
    //==============================================================================
	virtual IMcErrors::ECode LoadObjects(
		PCSTR strFileName, 
		IMcUserDataFactory *pUserDataFactory = NULL, 
		CMcDataArray<IMcObject*> *papLoadedObjects = NULL,
		IMcOverlayManager::EStorageFormat *peStorageFormat = NULL,
		UINT *puVersion = NULL) = 0;

    //==============================================================================
    // Method Name: LoadObjects()
    //------------------------------------------------------------------------------
	/// Loads overlay's objects previously saved by SaveAllObjects() or SaveObjects() 
	/// from a memory buffer. 
	///
    /// \param[in]  abMemoryBuffer		The memory buffer to read from.
    /// \param[in]  uBufferSize			The size of the above buffer.
	/// \param[in]	pUserDataFactory	The optional user-defined factory that creates user data instances.
	/// \param[out] papLoadedObjects	The optional array of loaded objects; filled by the function (pass `NULL` if unnecessary).
	/// \param[out]	peStorageFormat		The optional storage format; filled by the function (pass `NULL` if unnecessary).
	/// \param[out]	puVersion			The optional storage version number; filled by the function (pass `NULL` if unnecessary); 
	///									for objects saved by an official release of MapCore, the version number will be equal to a numerical value 
	///									of one of the enumerators of IMcOverlayManager::ESavingVersionCompatibility.
    /// \return
    ///     - Status result
    //==============================================================================
	virtual IMcErrors::ECode LoadObjects(
		const BYTE abMemoryBuffer[], UINT uBufferSize, 
		IMcUserDataFactory *pUserDataFactory = NULL, 
		CMcDataArray<IMcObject*> *papLoadedObjects = NULL,
		IMcOverlayManager::EStorageFormat *peStorageFormat = NULL,
		UINT *puVersion = NULL) = 0;

	//==============================================================================
	// Method Name: SaveAllObjects()
	//------------------------------------------------------------------------------
	/// Saves all the objects of the overlay to a file. 
	///
	/// \param[in]	strFileName			The name of the file
	/// \param[in]	eStorageFormat		The storage format; the default is IMcOverlayManager::ESF_MAPCORE_BINARY
	/// \param[in]	eVersion			Compatibility to MapCore's previous versions; the default is IMcOverlayManager::ESVC_LATEST (no compatibility)
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SaveAllObjects(
		PCSTR strFileName, 
		IMcOverlayManager::EStorageFormat eStorageFormat = IMcOverlayManager::ESF_MAPCORE_BINARY, 
		IMcOverlayManager::ESavingVersionCompatibility eVersion = IMcOverlayManager::ESVC_LATEST) = 0;

	//==============================================================================
	// Method Name: SaveAllObjects()
	//------------------------------------------------------------------------------
	/// Saves all the objects of the overlay to a memory buffer. 
	///
	/// \param[out]	pauMemoryBuffer		The memory buffer filled by the function
	/// \param[in]	eStorageFormat		The storage format; the default is IMcOverlayManager::ESF_MAPCORE_BINARY
	/// \param[in]	eVersion			the compatibility to MapCore's previous versions; the default is IMcOverlayManager::ESVC_LATEST (no compatibility)
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SaveAllObjects(
		CMcDataArray<BYTE> *pauMemoryBuffer, 
		IMcOverlayManager::EStorageFormat eStorageFormat = IMcOverlayManager::ESF_MAPCORE_BINARY, 
		IMcOverlayManager::ESavingVersionCompatibility eVersion = IMcOverlayManager::ESVC_LATEST) = 0;

	//==============================================================================
	// Method Name: SaveObjects()
	//------------------------------------------------------------------------------
	/// Saves the specified objects of the overlay to a file. 
	///
	/// \param[in] pObjects				The array of objects to save
	/// \param[in] uNumObjects			The number of objects in the above array
	/// \param[in] strFileName			The name of the file
	/// \param[in] eStorageFormat		The storage format; the default is IMcOverlayManager::ESF_MAPCORE_BINARY
	/// \param[in] eVersion				the compatibility to MapCore's previous versions; the default is IMcOverlayManager::ESVC_LATEST (no compatibility)
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SaveObjects(
		IMcObject* const pObjects[], UINT uNumObjects,
		PCSTR strFileName, 
		IMcOverlayManager::EStorageFormat eStorageFormat = IMcOverlayManager::ESF_MAPCORE_BINARY, 
		IMcOverlayManager::ESavingVersionCompatibility eVersion = IMcOverlayManager::ESVC_LATEST) = 0;

	//==============================================================================
	// Method Name: SaveObjects()
	//------------------------------------------------------------------------------
	/// Saves the specified objects of the overlay to a memory buffer. 
	///
	/// \param[in]	pObjects			The array of objects to save
	/// \param[in]	uNumObjects			The number of objects in the above array
	/// \param[out]	pauMemoryBuffer		The memory buffer filled by the function
	/// \param[in]	eStorageFormat		The storage format; the default is IMcOverlayManager::ESF_MAPCORE_BINARY
	/// \param[in]	eVersion			The compatibility to MapCore's previous versions; the default is IMcOverlayManager::ESVC_LATEST (no compatibility)
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SaveObjects(
		IMcObject* const pObjects[], UINT uNumObjects,
		CMcDataArray<BYTE> *pauMemoryBuffer, 
		IMcOverlayManager::EStorageFormat eStorageFormat = IMcOverlayManager::ESF_MAPCORE_BINARY, 
		IMcOverlayManager::ESavingVersionCompatibility eVersion = IMcOverlayManager::ESVC_LATEST) = 0;
	
	//==============================================================================
	// Method Name: SaveAllObjectsAsNativeVectorMapLayer()
	//------------------------------------------------------------------------------
	/// Converts all the objects of the overlay to a native vector map layer.
	///
	/// \param[in]	strFolderName			The destination folder for a native vector map layer.
	/// \param[in]	pMapViewport			The map viewport needed for a conversion.
	/// \param[in]	fMinScale				The minimal scale for displaying the layer; will be considered only if \p fMinScale < \p fMaxScale.
	/// \param[in]	fMaxScale				The maximal scale for displaying the layer; will be considered only if \p fMinScale < \p fMaxScale.
	/// \param[in]	fMaxScaleFactor			The maximal limit of scale factors used in IMcOverlayManager::SetScaleFactor() when displaying raw vector layer.
	/// \param[in]	fMinSizeFactor			The minimal limit of size factors used in IMcOverlayManager::SetItemSizeFactors() when displaying raw vector layer; 
	///										the default is 1.
	/// \param[in]	fMaxSizeFactor			The maximal limit of size factors used in IMcOverlayManager::SetItemSizeFactors() when displaying raw vector layer; 
	///										the default is 1.
	/// \param[in]	pTilingScheme			The layer's tiling scheme; default is `NULL` (means MapCore's scheme)
	/// \param[in]	uNumTilesInFileEdge		The number of tiles in each output file edge (\p uNumTilesInFileEdge x \p uNumTilesInFileEdge tiles per file);
	///										0 means 8 x 8 tiles (the default)
	/// \param[in]	eVersion				The compatibility to MapCore's previous versions; the default is IMcOverlayManager::ESVC_LATEST (no compatibility)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SaveAllObjectsAsNativeVectorMapLayer(
		PCSTR strFolderName, IMcMapViewport *pMapViewport, float fMinScale, float fMaxScale,
		float fMaxScaleFactor, float fMinSizeFactor = 1.0f, float fMaxSizeFactor = 1.0f,
		const IMcMapLayer::STilingScheme *pTilingScheme = NULL, UINT uNumTilesInFileEdge = 0,
		IMcOverlayManager::ESavingVersionCompatibility eVersion = IMcOverlayManager::ESVC_LATEST) = 0;

	//==============================================================================
	// Method Name: SaveObjectsAsNativeVectorMapLayer()
	//------------------------------------------------------------------------------
	/// Saves the specified objects of the overlay as native vector map layer.
	///
	/// \param[in]	pObjects				The array of objects to save
	/// \param[in]	uNumObjects				The number of objects in the above array
	/// \param[in]	strFolderName			The destination folder for a native vector map layer.
	/// \param[in]	pMapViewport			The map viewport needed for a conversion.
	/// \param[in]	fMinScale				The minimal scale for displaying the layer; will be considered only if \p fMinScale < \p fMaxScale.
	/// \param[in]	fMaxScale				The maximal scale for displaying the layer; will be considered only if \p fMinScale < \p fMaxScale.
	/// \param[in]	fMaxScaleFactor			The maximal limit of scale factors used in IMcOverlayManager::SetScaleFactor() when displaying raw vector layer.
	/// \param[in]	fMinSizeFactor			The minimal limit of size factors used in IMcOverlayManager::SetItemSizeFactors() when displaying raw vector layer; 
	///										the default is 1.
	/// \param[in]	fMaxSizeFactor			The maximal limit of size factors used in IMcOverlayManager::SetItemSizeFactors() when displaying raw vector layer; 
	///										the default is 1.
	/// \param[in]	pTilingScheme			The layer's tiling scheme; default is `NULL` (means MapCore's scheme)
	/// \param[in]	uNumTilesInFileEdge		The number of tiles in each output file edge (\p uNumTilesInFileEdge x \p uNumTilesInFileEdge tiles per file);
	///										0 means 8 x 8 tiles (the default)
	/// \param[in]	eVersion				The compatibility to MapCore's previous versions; the default is IMcOverlayManager::ESVC_LATEST (no compatibility)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SaveObjectsAsNativeVectorMapLayer(
		IMcObject* const pObjects[], UINT uNumObjects, PCSTR strFolderName, IMcMapViewport *pMapViewport,
		float fMinScale, float fMaxScale, float fMaxScaleFactor, float fMinSizeFactor = 1.0f, float fMaxSizeFactor = 1.0f,
		const IMcMapLayer::STilingScheme *pTilingScheme = NULL, UINT uNumTilesInFileEdge = 0,
		IMcOverlayManager::ESavingVersionCompatibility eVersion = IMcOverlayManager::ESVC_LATEST) = 0;

	//==============================================================================
	// Method Name: SaveAllObjectsAsRawVectorData()
	//------------------------------------------------------------------------------
	/// Saves all the objects of the overlay as raw vector data files. 
	///
	/// \param[in]	pMapViewport		The map viewport needed to save the objects according to.
	/// \param[in]	fCameraYawAngle		The camera yaw angle used to save the objects.
	/// \param[in]	fCameraScale		The camera scale in world units per pixel (the ratio of a distance on the ground to the corresponding distance 
	///									in display pixels) used to save the objects.
	/// \param[in]	strLayerName		The name to be given to the saved vector layer.
	/// \param[in]	strFileName			The name (with an extension, without a path) of the main file of raw vector data to save to 
	///									(the file's name defines also the names of additional files required by the format, 
	///									the file's extension defines the format, e.g. "kml", "kmz", "dxf").
	/// \param[out] paAdditionalFiles   The array (filled by the function) of names of the additional files saved required by the format.
	/// \param[in]  pAsyncCallback		The optional callback for asynchronous operation; if not NULL: the operation will be performed asynchronously 
	///									(the function will return without results, when the results are ready an appropriate callback function with 
	///									the results will be called)
	/// \param[in]	eGeometryFilter		The optional geometry filter (if != #UnSupportedGeometry, only the specified geometry type will be saved); 
	///									the default is #UnSupportedGeometry (means all geometry types).
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SaveAllObjectsAsRawVectorData(
		IMcMapViewport *pMapViewport, float fCameraYawAngle, float fCameraScale, PCSTR strLayerName, 
		PCSTR strFileName, CMcDataArray<CMcString> *paAdditionalFiles,
		IMcOverlayManager::IAsyncOperationCallback *pAsyncCallback = NULL, EGeometry eGeometryFilter = UnSupportedGeometry) = 0;

	//==============================================================================
	// Method Name: SaveAllObjectsAsRawVectorData()
	//------------------------------------------------------------------------------
	/// Saves all the objects of the overlay as raw vector data files' memory buffers. 
	///
	/// \param[in]	pMapViewport		The map viewport needed to save the objects according to.
	/// \param[in]	fCameraYawAngle		The camera yaw angle used to save the objects.
	/// \param[in]	fCameraScale		The camera scale in world units per pixel (the ratio of a distance on the ground to the corresponding distance 
	///									in display pixels) used to save the objects.
	/// \param[in]	strLayerName		The name to be given to the saved vector layer.
	/// \param[in]	strFileName			The name (with an extension, without a path) of the main file of raw vector data to save to 
	///									(the file's name defines the names of additional files required by the format, 
	///									the file's extension defines the format, e.g. "kml", "kmz", "dxf").
	/// \param[out]	pauFileMemoryBuffer	The memory buffer for the main file, filled by the function.
	/// \param[out] paAdditionalFiles	The array (filled by the function) of relative names and memory buffers of the additional files saved required by the format.
	/// \param[in]  pAsyncCallback		The optional callback for asynchronous operation; if not NULL: the operation will be performed asynchronously 
	///									(the function will return without results, when the results are ready an appropriate callback function with 
	///									the results will be called)
	/// \param[in]	eGeometryFilter		The optional geometry filter (if != #UnSupportedGeometry, only the specified geometry type will be saved); 
	///									the default is #UnSupportedGeometry (means all geometry types).
	/// \note
	/// If some objects have names and/or descriptions (see IMcObject::SetNameAndDescription()), they will be saved as fields named "Name"/"Description".
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SaveAllObjectsAsRawVectorData(
		IMcMapViewport *pMapViewport, float fCameraYawAngle, float fCameraScale, PCSTR strLayerName, 
		PCSTR strFileName, CMcDataArray<BYTE> *pauFileMemoryBuffer, CMcDataArray<SMcFileInMemory> *paAdditionalFiles,
		IMcOverlayManager::IAsyncOperationCallback *pAsyncCallback = NULL, EGeometry eGeometryFilter = UnSupportedGeometry) = 0;

	//==============================================================================
	// Method Name: SaveObjectsAsRawVectorData()
	//------------------------------------------------------------------------------
	/// Saves the specified objects of the overlay as raw vector data files. 
	///
	/// \param[in]	apObjects			The array of objects to save.
	/// \param[in]	uNumObjects			The number of objects in the above array.
	/// \param[in]	pMapViewport		The map viewport needed to save the objects according to.
	/// \param[in]	fCameraYawAngle		The camera yaw angle used to save the objects.
	/// \param[in]	fCameraScale		The camera scale in world units per pixel (the ratio of a distance on the ground to the corresponding distance 
	///									in display pixels) used to save the objects.
	/// \param[in]	strLayerName		The name to be given to the saved vector layer.
	/// \param[in]	strFileName			The name of the main file of raw vector data to save to (the file's extension defines the format, e.g. "kml", "kmz", "dxf").
	/// \param[out] paAdditionalFiles   The array (filled by the function) of names of the additional files saved required by the format.
	/// \param[in]  pAsyncCallback		The optional callback for asynchronous operation; if not NULL: the operation will be performed asynchronously 
	///									(the function will return without results, when the results are ready an appropriate callback function with 
	///									the results will be called)
	/// \param[in]	eGeometryFilter		The optional geometry filter (if != #UnSupportedGeometry, only the specified geometry type will be saved); 
	///									the default is #UnSupportedGeometry (means all geometry types).
	/// \note
	/// If some objects have names and/or descriptions (see IMcObject::SetNameAndDescription()), they will be saved as fields named "Name"/"Description".
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SaveObjectsAsRawVectorData(
		IMcObject* const apObjects[], UINT uNumObjects, IMcMapViewport *pMapViewport, 
		float fCameraYawAngle, float fCameraScale, PCSTR strLayerName,
		PCSTR strFileName, CMcDataArray<CMcString> *paAdditionalFiles,
		IMcOverlayManager::IAsyncOperationCallback *pAsyncCallback = NULL, EGeometry eGeometryFilter = UnSupportedGeometry) = 0;

	//==============================================================================
	// Method Name: SaveObjectsAsRawVectorData()
	//------------------------------------------------------------------------------
	/// Saves the specified objects of the overlay as raw vector data files' memory buffers.
	///
	/// \param[in]	apObjects			The array of objects to save.
	/// \param[in]	uNumObjects			The number of objects in the above array.
	/// \param[in]	pMapViewport		The map viewport needed to save the objects according to.
	/// \param[in]	fCameraYawAngle		The camera yaw angle used to save the objects.
	/// \param[in]	fCameraScale		The camera scale in world units per pixel (the ratio of a distance on the ground to the corresponding distance 
	///									in display pixels) used to save the objects.
	/// \param[in]	strLayerName		The name to be given to the saved vector layer.
	/// \param[in]	strFileName			The name (with an extension, without a path) of the main file of raw vector data to save to 
	///									(the file's name defines the names of additional files required by the format, 
	///									the file's extension defines the format, e.g. "kml", "kmz", "dxf").
	/// \param[out]	pauFileMemoryBuffer	The memory buffer for the main file, filled by the function.
	/// \param[out] paAdditionalFiles	The array (filled by the function) of relative names and memory buffers of the additional files saved required by the format.
	/// \param[in]  pAsyncCallback		The optional callback for asynchronous operation; if not NULL: the operation will be performed asynchronously 
	///									(the function will return without results, when the results are ready an appropriate callback function with 
	///									the results will be called)
	/// \param[in]	eGeometryFilter		The optional geometry filter (if != #UnSupportedGeometry, only the specified geometry type will be saved); 
	///									the default is #UnSupportedGeometry (means all geometry types).
	/// \note
	/// If some objects have names and/or descriptions (see IMcObject::SetNameAndDescription()), they will be saved as fields named "Name"/"Description".
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SaveObjectsAsRawVectorData(
		IMcObject* const apObjects[], UINT uNumObjects, IMcMapViewport *pMapViewport,
		float fCameraYawAngle, float fCameraScale, PCSTR strLayerName,
		PCSTR strFileName, CMcDataArray<BYTE> *pauFileMemoryBuffer, CMcDataArray<SMcFileInMemory> *paAdditionalFiles,
		IMcOverlayManager::IAsyncOperationCallback *pAsyncCallback = NULL, EGeometry eGeometryFilter = UnSupportedGeometry) = 0;
	
	//==============================================================================
	// Method Name: CancelAsyncSavingObjects()
	//------------------------------------------------------------------------------
	/// Cancels asynchronous saving objects by \p pAsyncCallback previously specified in SaveObjectsAsRawVectorData() / SaveAllObjectsAsRawVectorData().
	///
	/// \param[in]  pAsyncCallback	asynchronous callback.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode CancelAsyncSavingObjects(IMcOverlayManager::IAsyncOperationCallback *pAsyncCallback) = 0;

	//==============================================================================
	// Method Name: LoadObjectsFromRawVectorData()
	//------------------------------------------------------------------------------
	/// Loads (creates) objects from raw vector data. 
	///
	///	\param[in]	Params						The parameters needed for reading the vector data, see IMcRawVectorMapLayer::SParams for details.
	/// \param[out]	papLoadedObjects			The optional array of loaded objects filled by the function (can be 'NULL' if unneeded).
	/// \param[in]	bClearObjectSchemesCache	Whether the cache of schemes created by previous calls to LoadObjectsFromRawVectorData() should 
	///											be cleared and new schemes should be created or schemes from the cache can be used (if they are 
	///											identical to those needed); the default is false (using the cache).
	/// \note
	///	- If the raw vector data includes fields named "Name" and/or "Description", the objects' names/descriptions will be set 
	///   (see IMcObject::SetNameAndDescription()).
	///	- Convert raw vector data to objects only in case of a small number of objects, otherwise use raw vector map layer (IMcRawVectorMapLayer).
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode LoadObjectsFromRawVectorData(
		const IMcRawVectorMapLayer::SParams &Params, CMcDataArray<IMcObject*> *papLoadedObjects = NULL,
		bool bClearObjectSchemesCache = false) = 0;

	//@}

	/// \name Objects, Overlay Manager, Collections
	//@{

	//==============================================================================
	// Method Name: GetObjectByID()
	//------------------------------------------------------------------------------
	/// Retrieves the overlay's object based on its ID defined by IMcObject::SetID().
	///
	/// \param[in]	uObjectID	The object ID to look for
	/// \param[out]	ppObject	The object found
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectByID(UINT uObjectID, IMcObject **ppObject) const = 0;

	//==============================================================================
	// Method Name: GetObjects()
	//------------------------------------------------------------------------------
	/// Retrieves all the objects in the overlay.
	///
	/// \param[out] papObjects	The array of objects
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjects(
		CMcDataArray<IMcObject*> *papObjects) const = 0;

	//==============================================================================
	// Method Name: SetColorOverriding()
	//------------------------------------------------------------------------------
	/// Enables or disables overriding color properties for the overlay's objects (see SColorPropertyOverriding and #EColorPropertyType for details).
	///
	/// Either the appropriate overriding option in one specific viewport or the appropriate overriding option default in all viewports is set. 
	/// Setting each overriding option for all viewports changes the appropriate option for each 
	/// viewport previously set. On the other hand, it can later be changed for any specific viewport.
	///
	/// \param[in]	aProperties[]	The array (of length #ECPT_NUM) of color overriding parameters per 
	///								#EColorPropertyType as an index (`aProperties[ECPT_LINE]` is for #ECPT_LINE etc.)
	/// \param[in]	pMapViewport	The viewport to override colors in or `NULL` for all viewports.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetColorOverriding(const SColorPropertyOverriding aProperties[ECPT_NUM], 
		IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetColorOverriding()
	//------------------------------------------------------------------------------
	/// Retrieves the parameters of overriding color properties set by SetColorOverriding().
	///
	/// Either the appropriate overriding option in one specific viewport or the appropriate overriding option default in all viewports is retrieved.
	///
	/// \param[out]	aProperties[]	The array (of length #ECPT_NUM) of color overriding parameters per 
	///								#EColorPropertyType as an index (`aProperties[ECPT_LINE]` is for #ECPT_LINE etc.)
	/// \param[in]	pMapViewport	The viewport to retrieve the option for or `NULL` for all viewports.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetColorOverriding(SColorPropertyOverriding aProperties[ECPT_NUM], 
		IMcMapViewport *pMapViewport = NULL) const = 0;

	//==============================================================================
	// Method Name: GetOverlayManager()
	//------------------------------------------------------------------------------
	/// Retrieves the overlay manager that the overlay belongs to
	/// 
	/// \param[out] ppOverlayManager	The overlay manager
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetOverlayManager(
		IMcOverlayManager **ppOverlayManager) const = 0;

	//==============================================================================
	// Method Name: GetCollections()
	//------------------------------------------------------------------------------
	/// Retrieves all the collections that the overlay is a member of.
	///
	/// \param[out] papCollections	The array of collections
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetCollections(
		CMcDataArray<IMcCollection*> *papCollections) const = 0;

	//@}

	/// \name State
	//@{

	//==============================================================================
	// Method Name: SetState()
	//------------------------------------------------------------------------------
	/// Sets the overlay's state of as a single sub-state.
	///
	/// Either the state in one specific viewport or the state default in all viewports is set. Setting the state default in all viewports 
	/// overrides a state in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] uState			The overlay's new state a single sub-state.
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
	/// Sets the overlay's state of as an array of sub-states.
	///
	/// Either the state in one specific viewport or the state default in all viewports is set. Setting the state default in all viewports 
	/// overrides a state in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] auStates			The overlay's new state as an array of sub-states.
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
	/// Retrieves the overlay's state set by SetState().
	///
	/// Either the state in one specific viewport or the state default in all viewports is retrieved.
	///
	/// \param[out] pauStates		The array of the overlay's sub-states.
	/// \param[in]	pMapViewport	The viewport to retrieve the state for or `NULL` for the state default in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetState(CMcDataArray<BYTE> *pauStates, IMcMapViewport *pMapViewport = NULL) const = 0;

	//@}

	/// \name ID and User Data
	//@{

	//==============================================================================
	// Method Name: SetID()
	//------------------------------------------------------------------------------
	/// Sets the overlay's user-defined unique ID that allows retrieving the overlay by IMcOverlayManager::GetOverlayByID().
	///
	/// \param[in]	uID		The overlay's ID to be set (specify ID not equal to #MC_EMPTY_ID to set ID,
	///						equal to #MC_EMPTY_ID to remove ID).
	/// \note
	/// The ID should be unique in the overlay's overlay manager, otherwise the function returns IMcErrors::ID_ALREADY_EXISTS
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetID(UINT uID) = 0;

	//==============================================================================
	// Method Name: GetID()
	//------------------------------------------------------------------------------
	/// Retrieves the overlay'e user-defined unique ID set by SetID().
	///        
	/// \param [out] puID	The overlay ID (or #MC_EMPTY_ID if the ID is not set).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetID(UINT *puID) const = 0;

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

	/// \name Draw Priority
	//@{

	//==============================================================================
	// Method Name: SetDrawPriority()
	//------------------------------------------------------------------------------
	/// Sets the overlay's draw priority defining the drawing order of the overlay's objects relative to objects of other overlays.
	///
	/// Either the draw priority in one specific viewport or the draw priority default in all viewports is set. 
	/// Setting the draw priority default in all viewports overrides a draw priority in each 
	/// viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] nPriority		The overlay's draw priority; the range is -32768 to 32767;
	///								overlays with higher priority are rendered on top of overlays with lower priority; the default is 0.
	/// \param[in] pMapViewport		The viewport to set draw priority for or `NULL` for the draw priority default in all viewports.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetDrawPriority(
		short nPriority, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetDrawPriority()
	//------------------------------------------------------------------------------
	/// Retrieves the overlay's draw priority set by SetDrawPriority().
	///
	/// Either the draw priority in one specific viewport or the draw priority default in all viewports is retrieved.
	///
	/// \param[out] pnPriority		The overlay's draw priority
	/// \param[in]	pMapViewport	The viewport to retrieve the draw priority for or `NULL` for the draw priority default in all viewports.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetDrawPriority(
		short *pnPriority, IMcMapViewport *pMapViewport = NULL) const = 0;

	//@}

	/// \name Visibility
	//@{

	//==============================================================================
	// Method Name: SetVisibilityOption()
	//------------------------------------------------------------------------------
	/// Sets the overlay's visibility option in one specific viewport in the default visibility option in all viewports.
	///
	/// Setting the visibility option default in all viewports overrides a visibility option in each 
	/// viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// The overlay's visibility in a specific viewport depends on its visibility option for this viewport along with 
	/// its visibility conditional selector's result (only if the visibility option is IMcConditionalSelector::EAO_USE_SELECTOR - the default) and
	/// the visibility of collections the overlay belongs to.
	///
	/// \param[in]	eVisibility		The overlay's visibility option; the default is IMcConditionalSelector::EAO_USE_SELECTOR).
	/// \param[in]	pMapViewport	The viewport to set a visibility option for or `NULL` for default visibility in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetVisibilityOption(IMcConditionalSelector::EActionOptions eVisibility, 
												 IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: SetVisibilityOption()
	//------------------------------------------------------------------------------
	/// Sets the overlay's visibility option in the specified viewports.
	///
	/// Just calls SetVisibilityOption() for each viewport specified.
	///
	/// \param[in]	eVisibility			The overlay's visibility option; the default is IMcConditionalSelector::EAO_USE_SELECTOR).
	/// \param[in]	apMapViewports[]	The array of viewports to set a visibility option for.
	/// \param[in]	uNumMapViewports	The number of viewports in the above array.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetVisibilityOption(IMcConditionalSelector::EActionOptions eVisibility, 
												 IMcMapViewport *apMapViewports[], UINT uNumMapViewports) = 0;

	//==============================================================================
	// Method Name: GetVisibilityOption()
	//------------------------------------------------------------------------------
	/// Retrieves the overlay's visibility option in one specific viewport (set by SetVisibilityOption() with specific viewport(s)) or 
	/// the default visibility in all viewports (set by SetVisibilityOption() without viewport).
	///
	/// The overlay's visibility in a specific viewport depends on its visibility option for this viewport along with 
	/// its visibility conditional selector's result (only if the visibility option is IMcConditionalSelector::EAO_USE_SELECTOR - the default) and
	/// the visibility of collections the overlay belongs to.
	///
	/// \param[out]	peVisibility	The overlay's visibility option; the default is IMcConditionalSelector::EAO_USE_SELECTOR).
	/// \param[in]	pMapViewport	The viewport to retrieve a visibility option for or `NULL` for default visibility in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetVisibilityOption(IMcConditionalSelector::EActionOptions *peVisibility, 
												 IMcMapViewport *pMapViewport = NULL) const = 0;

	//==============================================================================
	// Method Name: GetEffectiveVisibilityInViewport()
	//------------------------------------------------------------------------------
	/// Retrieves the overlay's current effective visibility in the specified viewport.
	///
	/// The result is irrespective to the viewport's current visible area. It depends on its visibility option for this viewport along with 
	/// its visibility conditional selector's result (only if the visibility option is IMcConditionalSelector::EAO_USE_SELECTOR - the default) and 
	/// the visibility of collections the overlay belongs to.
	///
	/// \param[in]	pMapViewport	The viewport to check a visibility in.
	/// \param[out]	pbVisible		Whether the overlay is currently visible in the viewport
	///								(irrespectively to the viewport's current visible area).
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetEffectiveVisibilityInViewport(IMcMapViewport *pMapViewport, 
															  bool *pbVisible) const = 0;

	//==============================================================================
	// Method Name: SetConditionalSelector()
	//------------------------------------------------------------------------------
	/// Sets the overlay's conditional selector controlling the overlay's visibility.
	///
	/// \param[in] eActionType		The selector's action type (only IMcConditionalSelector::EAT_VISIBILITY is applicable here).
	/// \param[in] bActionOnResult	Defines which selector result make the overlay visible (in other words: whether the selector serves as either *if* or *else* condition).
	/// \param[in] pSelector		The conditional selector.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetConditionalSelector(
		IMcConditionalSelector::EActionType eActionType,
		bool bActionOnResult,
		IMcConditionalSelector *pSelector) = 0;

	//==============================================================================
	// Method Name: GetConditionalSelector()
	//------------------------------------------------------------------------------
	/// Retrieves the overlay's conditional selector controlling the overlay's visibility set by SetConditionalSelector()
	///
	/// \param[in]  eActionType			The selector's action type (only IMcConditionalSelector::EAT_VISIBILITY is applicable here).
	/// \param[out] pbActionOnResult	Defines which selector result make the overlay visible (in other words: whether the selector serves as either *if* or *else* condition).
	/// \param[out] ppSelector			The conditional selector.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetConditionalSelector(
		IMcConditionalSelector::EActionType eActionType,
		bool *pbActionOnResult,
		IMcConditionalSelector **ppSelector) const = 0;

	//==============================================================================
	// Method Name: SetSubItemsVisibility()
	//------------------------------------------------------------------------------
	/// Sets which sub-items set by IMcSymbolicItem::SetSubItemsData() are visible for the overlay's objects.
	/// 
	/// Each time the function is called, the previous definition is canceled and fully 
	/// replaced by the new one.
	///
	/// \param[in] auSubItemsIDs[]	The IDs of the sub-items to define the visibility for.
	/// \param[in] uNumSubItemsIDs	The number of IDs in the above array.
	/// \param[in] bVisibility		The visibility of the sub-items listed in \p auSubItemsIDs; the visibility 
	///								of the sub-items not listed in \p auSubItemsIDs is opposite.
	/// \param[in] pMapViewport		The viewport or `NULL` to define the default visibility in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetSubItemsVisibility(const UINT auSubItemsIDs[], UINT uNumSubItemsIDs, 
		bool bVisibility, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetSubItemsVisibility()
	//------------------------------------------------------------------------------
	/// Retrieves the sub-items visibility set by SetSubItemsVisibility().
	///
	/// \param[out]	pauSubItemsIDs	The IDs of the sub-items the visibility is defined for.
	/// \param[out] pbVisibility	The visibility of the sub-items listed in \p auSubItemsIDs; the visibility 
	///								of the sub-items not listed in \p pauSubItemsIDs is opposite.
	/// \param[in] pMapViewport		The viewport or `NULL` to retrieve the default visibility in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetSubItemsVisibility(CMcDataArray<UINT> *pauSubItemsIDs, 
		bool *pbVisibility, IMcMapViewport *pMapViewport = NULL) const = 0;

	//@}

	/// \name Detectibility
	//@{

	//==============================================================================
	// Method Name: SetDetectibility()
	//------------------------------------------------------------------------------
	/// Sets whether the overlay will be retrieved by IMcSpatialQueries::ScanInGeometry().
	///
	/// Either the detectibility in one specific viewport or the detectibility default in all viewports is set. 
	/// Setting the detectibility default in all viewports overrides a detectibility in each 
	/// viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] bDetectibility	The overlay's detectibility; the default is true.
	/// \param[in] pMapViewport		The viewport to set a detectibility for or `NULL` for the detectibility default in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetDetectibility(
		bool bDetectibility, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetDetectibility()
	//------------------------------------------------------------------------------
	/// Retrieves the overlay's detectibility set by SetDetectibility().
	///
	/// Either the detectibility in one specific viewport or the detectibility default in all viewports is retrieved.
	///
	/// \param[out] pbDetectibility		The overlay's detectibility; the default is true.
	/// \param[in]	pMapViewport		The viewport to retrieve a detectibility for or `NULL` for the detectibility default in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetDetectibility(
		bool *pbDetectibility, IMcMapViewport *pMapViewport = NULL) const = 0;

	//@}
};
