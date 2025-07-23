#pragma once
class IMcGridCoordinateSystem;

//===========================================================================
/// \file IMcVectorMapLayer.h
/// Interfaces for vector map layers
//===========================================================================
#include "McCommonTypes.h"
#include "McExports.h"
#include "Map/IMcMapLayer.h"
#include "OverlayManager/IMcOverlayManager.h"
#include "Calculations/SMcScanGeometry.h"
#include "Calculations/IMcSpatialQueries.h"

class IMcObject;
class IMcMapViewport;
class IMcCollection;
class IMcObjectScheme;

//===========================================================================
// Interface Name: IMcVectorBasedMapLayer
//---------------------------------------------------------------------------
/// The base interface for layers based on vector data
//===========================================================================
class IMcVectorBasedMapLayer : virtual public IMcMapLayer
{
protected:
    virtual ~IMcVectorBasedMapLayer() {}
	
public:
};

//===========================================================================
// Interface Name: IMcVectorMapLayer
//---------------------------------------------------------------------------
/// The base interface for vector layers
//===========================================================================
class IMcVectorMapLayer : virtual public IMcVectorBasedMapLayer
{
protected:
    virtual ~IMcVectorMapLayer() {}
	
public:

	/// \name State
	//@{

	//==============================================================================
	// Method Name: SetOverlayState(...)
	//------------------------------------------------------------------------------
	/// Sets the state of the layer's overlay as a single sub-state.
	///
	/// \param[in] uState				The new state
	/// \param[in] pMapViewport			The viewport to set state for or NULL for all viewports.
	///
	/// \note
	/// - The object state determines the versions of the properties to be used.
	/// - If the object state is not defined for the property, zero-state property version is used.
	/// 
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetOverlayState(BYTE uState, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: SetOverlayState(...)
	//------------------------------------------------------------------------------
	/// Sets the state of the layer's overlay as an array of sub-states.
	///
	/// \param[in] auStates				Array of new sub states
	/// \param[in] uNumStates			Number of new sub states
	/// \param[in] pMapViewport			The viewport to set state for or NULL for all viewports.
	///
	/// \note
	/// - The object state determines the versions of the properties to be used. 
	/// - If the object state consists of several sub-states, the property version is that of the first sub-state defined 
	///   for the property.
	/// - If none of the object sub-states is defined for the property, zero-state property version is used.
	/// 
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetOverlayState(const BYTE auStates[], UINT uNumStates, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetOverlayState(...)
	//------------------------------------------------------------------------------
	/// Retrieves the state of the layer's overlay defined by SetSOverlaytate().
	///
	/// \param[out] pauStates			Array of sub states of the state defined by SetOverlayState()
	/// \param[in]	pMapViewport		The viewport to retrieve the state for or NULL for all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetOverlayState(CMcDataArray<BYTE> *pauStates, IMcMapViewport *pMapViewport = NULL) const = 0;

	//==============================================================================
	// Method Name: SetVectorItemObjectState(...)
	//------------------------------------------------------------------------------
	/// Sets the state of the layer's vector item as a single sub-state.
	///
	/// \param[in] uState				The new state
	/// \param[in] uVectorItemID		The vector item's ID.
	/// \param[in] pMapViewport			The viewport to set state for or NULL for all viewports.
	///
	/// \note
	/// - The object state determines the versions of the properties to be used.
	/// - If the object state is not defined for the property, zero-state property version is used.
	/// 
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetVectorItemObjectState(UINT64 uVectorItemID, BYTE uState, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: SetVectorItemObjectState(...)
	//------------------------------------------------------------------------------
	/// Sets the state of the layer's vector item as an array of sub-states.
	///
	/// \param[in] uVectorItemID		The vector item's ID.
	/// \param[in] auStates				Array of new sub states
	/// \param[in] uNumStates			Number of new sub states
	/// \param[in] pMapViewport			The viewport to set state for or NULL for all viewports.
	///
	/// \note
	/// - The object state determines the versions of the properties to be used. 
	/// - If the object state consists of several sub-states, the property version is that of the first sub-state defined 
	///   for the property.
	/// - If none of the object sub-states is defined for the property, zero-state property version is used.
	/// 
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetVectorItemObjectState(UINT64 uVectorItemID, const BYTE auStates[], UINT uNumStates, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetVectorItemObjectState(...)
	//------------------------------------------------------------------------------
	/// Retrieves the state of the layer's vector Item defined by SetSVectorItemtate().
	///
	/// \param[in]	uVectorItemID		The vector item's ID.
	/// \param[out] pauStates			Array of sub states of the state defined by SetVectorItemObjectState()
	/// \param[in]	pMapViewport		The viewport to retrieve the state for or NULL for all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetVectorItemObjectState(UINT64 uVectorItemID, CMcDataArray<BYTE> *pauStates, IMcMapViewport *pMapViewport = NULL) const = 0;

	//@}

	/// \name Draw Priority
	//@{

	//==============================================================================
	// Method Name: SetOverlayDrawPriority()
	//------------------------------------------------------------------------------
	/// Enables or disables the layer's overlay draw priority mode and (if enabled) 
	///	sets its draw priority relative to objects' overlays.
	///
	/// \param[in]	bEnabled			Whether the layer's overlay draw priority mode is enabled: 
	///									- True if the layer should be rendered between objects' overlays 
	///									according to the specified \a nDrawPriority;
	///									- False if the layer should be rendered above all raster layers 
	///									below object's overlays, its priority relative to similar vector 
	///									layers will be according to its terrain's draw priority 
	///									in each viewport and its own draw priority.
	/// \param[in]	nPriority			The layer's draw priority relative to objects' overlays; 
	///									ignored if \a bEnabled is false
	///
	/// \note Map terrain's draw priority is defined by IMcMapViewport::SetTerrainDrawPriority(); 
	///		  map layer draw priority defined by IMcMapTerrain::SetLayerParams().
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetOverlayDrawPriority(bool bEnabled, short nPriority) = 0;

	//==============================================================================
	// Method Name: GetOverlayDrawPriority()
	//------------------------------------------------------------------------------
	/// Retrieves the layer's overlay draw priority mode and its draw priority relative to objects' overlays.
	///
	/// \param[out]	pbEnabled			Whether the layer's overlay draw priority mode is enabled by 
	///									SetOverlayDrawPriority()
	/// \param[out]	pnPriority			The layer's draw priority relative to objects' overlays; 
	///									ignored if the mode is disabled
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetOverlayDrawPriority(bool *pbEnabled, short *pnPriority) const = 0;

	//==============================================================================
	// Method Name: SetDrawPriorityConsistency()
	//------------------------------------------------------------------------------
	/// Allows to disable or enable again draw priority consistency mode which ensures that 
	///	symbolic items with equal priorities belonging to layers with equal 
	/// priorities are rendered in consistent order.
	///
	/// \param[in]	bConsistency		Whether draw priority consistency mode is enabled;
	///									default is false (disabled)
	///
	/// \note Enabling the mode can degrade the performance when there are many items with same draw priorities, 
	///		  it is recommended in cases where there are many overlaps between same priority items of significantly different colors.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetDrawPriorityConsistency(bool bConsistency) = 0;

	//==============================================================================
	// Method Name: GetDrawPriorityConsistency()
	//------------------------------------------------------------------------------
	/// Retrieves draw priority consistency mode set by SetDrawPriorityConsistency().
	///
	/// \param[out]	pbConsistency		Whether draw priority consistency mode is enabled;
	///									Default is true (enabled)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetDrawPriorityConsistency(bool *pbConsistency) const = 0;

	//@}

	/// \name Brightness
	//@{

	//==============================================================================
	// Method Name: SetBrightness()
	//------------------------------------------------------------------------------
	/// Sets the layer's brightness in all viewports or in one specific viewport.
	///
	/// Setting the layer's brightness in all viewports overrides its brightness in each viewport 
	/// previously set. On the other hand, it can later be changed in any specific viewport.
	///
	/// \param[in]	fBrightness			The layer's brightness (the default is true)
	/// \param[in]	pMapViewport		The viewport to set a brightness in or NULL for all viewports
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetBrightness(float fBrightness, IMcMapViewport *pMapViewport = NULL) = 0;

	//==============================================================================
	// Method Name: GetBrightness()
	//------------------------------------------------------------------------------
	/// Retrieves the default brightness of the layer in all viewports (set by SetBrightness(, NULL)) 
	/// or its brightness in one specific viewport.
	///
	/// \param[out]	pfBrightness		The layer's brightness
	/// \param[in]	pMapViewport		The viewport to retrieve a brightness in or NULL for all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetBrightness(float *pfBrightness, IMcMapViewport *pMapViewport = NULL) const = 0;

	//@}

	/// \name Collision Prevention
	//@{

	//==============================================================================
	// Method Name: SetCollisionPrevention()
	//------------------------------------------------------------------------------
	/// Allows to disable or enable again collision prevention mode which ensures that 
	///	labels will not hide each other.
	///
	/// \param[in]	bCollisionPrevention	Whether collision prevention mode is enabled;
	///										Default is false (disabled)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCollisionPrevention(bool bCollisionPrevention) = 0;

	//==============================================================================
	// Method Name: GetCollisionPrevention()
	//------------------------------------------------------------------------------
	/// Retrieves collision prevention mode set by SetCollisionPrevention().
	///
	/// \param[out]	pbCollisionPrevention	Whether collision prevention mode is enabled;
	///										Default is false (disabled)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCollisionPrevention(bool *pbCollisionPrevention) const = 0;

	//@}

	/// \name Tolerance For Point
	//@{

	//==============================================================================
	// Method Name: SetToleranceForPoint()
	//------------------------------------------------------------------------------
	/// Sets the tolerance of the effective area of point obstacles and the tolerance from
	/// the point to the route in find shortest path
	///
	/// \param[in]	nTolerance   		The tolerance
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetToleranceForPoint(int nTolerance) = 0;

	//==============================================================================
	// Method Name: GetToleranceForPoint()
	//------------------------------------------------------------------------------
	/// Gets the tolerance of the effective area of point obstacles and the tolerance from
	/// the point to the route in find shortest path
	///
	/// \param[out]	nTolerance   		The tolerance
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetToleranceForPoint(int *nTolerance) = 0;

	//@}

	/// \name Layer Parameters
	//@{

	//================================================================
	// Method Name: IsLiteVectorLayer() 
	//----------------------------------------------------------------
	/// Retrieves whether or not the vector layer is 'lite'
	///
	/// Only a native layer can be 'lite' (if it was converted this way).
	/// A raw layer is always considered 'non-lite'.
	///
	/// \param[out]	pbIsLite			Is lite vector layer
	///
	/// \return
	///     - Status result
	//================================================================
	virtual IMcErrors::ECode IsLiteVectorLayer(bool *pbIsLite) const = 0;

	//================================================================
	// Method Name: GetMinMaxSizeFactor() 
	//----------------------------------------------------------------
	/// Gets the layer's min size factor and max size factor
	///
	/// The value for these factors are set during convert.
	/// For raw layer they are always equal 1.
	/// These factors are relevant for vector items graphical display,
	/// in case IMcOverlayManager::SetItemSizeFactors() is called (with true value for \p bVectorItems).
	/// In that case, the actual size factor that will be used will be limited by these min and max size factors.
	///
	/// \param[out]	pfMinSizeFactor		The layer's minimum size factor
	/// \param[out]	pfMaxSizeFactor		The layer's maximum size factor
	///
	/// \return
	///     - Status result
	//================================================================
	virtual IMcErrors::ECode GetMinMaxSizeFactor(float *pfMinSizeFactor, float *pfMaxSizeFactor) const = 0;

	//@}

	/// \name The Vecor Data - Attributes, Fields, Vector Items
	//@{

	//==============================================================================
	// Method Name: GetGeometryType()
	//------------------------------------------------------------------------------
	/// Retrieves the geometry of the vector data
	///
	/// This method should be called only for raw layers
	///
	/// \param[out]	peGeometry			The geometry type (see #EGeometry)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetGeometryType(EGeometry *peGeometry) const = 0;

	//==============================================================================
	// Method Name: GetLayerAttributes()
	//------------------------------------------------------------------------------
	/// Retrieves the layer's all attributes' names and/or their corresponding values.
	///
	/// The layer's attributes' data is global data for the layer (do not confuse with layer fields,
	/// defining the vector items). Currently, it exists only for *.pc format layers.
	/// This method should be called only for raw layers or native layers that were converted with layer attributes' data.
	/// (In the last case, the data is taken from Vector.bin file).
	///
	/// \param[out]	pastrAttributesNames	The retrieved layer attributes' names (pass `NULL` if unnecessary).
	/// \param[out]	pastrAttributesValues	The retrieved layer attributes' values (pass `NULL` if unnecessary).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLayerAttributes(CMcDataArray<CMcString> *pastrAttributesNames, CMcDataArray<CMcString> *pastrAttributesValues = NULL) = 0;

	//==============================================================================
	// Method Name: GetLayerDataSources()
	//------------------------------------------------------------------------------
	/// Retrieves the layer's all data sources' names and/or their corresponding IDs.
	///
	/// \param[out]	pastrDataSourcesNames	The retrieved layer data sources' names (pass `NULL` if unnecessary).
	/// \param[out]	pauDataSourcesIDs		The retrieved layer data sources' IDs (pass `NULL` if unnecessary).
	///
	/// \return
	///     - status result
	//=============================================================================
	virtual IMcErrors::ECode GetLayerDataSources(CMcDataArray<CMcString> *pastrDataSourcesNames, CMcDataArray<UINT> *pauDataSourcesIDs = NULL) = 0;

	//==============================================================================
	// Method Name: VectorItemIDToOriginalID()
	//------------------------------------------------------------------------------
	/// Converts MapCore's vector item ID to the original ID of the item in the data source.
	///
	/// Useful for multi-source layer (several source files and/or several sub-layers per file).
	///
	/// \param[in]	uVectorItemID		MapCore's vector item ID.
	/// \param[out]	puOriginalID		The original ID of the item in the data source; for single-source layer it will be equal to \p uVectorItemID.
	/// \param[out]	pstrDataSourceName	The optional data source name (pass `NULL` if unnecessary).
	/// \param[out]	puDataSourceID		The optional data source ID (pass `NULL` if unnecessary); for single-source layer it will be zero.
	///
	/// \note
	///		All data sources' names and IDs can be retrieved by GetLayerDataSources().
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode VectorItemIDToOriginalID(UINT64 uVectorItemID, UINT *puOriginalID, 
		CMcString *pstrDataSourceName, UINT *puDataSourceID = NULL) const = 0;

	//==============================================================================
	// Method Name: VectorItemIDFromOriginalID()
	//------------------------------------------------------------------------------
	/// Retrieves MapCore's vector item ID from the original ID of the item in the data source.
	///
	/// Useful for multi-source layer (several source files and/or several sub-layers per file).
	///
	/// \param[out]	puVectorItemID		MapCore's vector item ID; for single-source layer it will be equal to \p uOriginalID.
	/// \param[in]	uOriginalID			The original ID of the item in the data source
	/// \param[in]	strDataSourceName	The data source name (ignored if \p uDataSourceID != `UINT_MAX`).
	/// \param[in]	uDataSourceID		The data source ID (pass `UINT_MAX` if \p strDataSourceName should be used).
	/// 
	/// \note
	///		The data source is identified either by its name or its ID (one of them must be specified) returned by 
	///		GetLayerDataSources() or VectorItemIDToOriginalID().
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode VectorItemIDFromOriginalID(UINT64 *puVectorItemID, UINT uOriginalID, 
		PCSTR strDataSourceName, UINT uDataSourceID = UINT_MAX) const = 0;

	//==============================================================================
	// Method Name: GetNumFields()
	//------------------------------------------------------------------------------
	/// Retrieves the number of fields
	///
	/// The fields are meta data optionally defined per each vector item.
	/// This method should be called only for raw layers or native layers that were converted with meta data
	/// (In the last case, the data is taken from the meta data file (for example: MetaData.sqlite)).
	///
	/// \param[out]	pnCount				The number of fields
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetNumFields(UINT *pnCount) const = 0;

	//==============================================================================
	// Method Name: GetFieldData()
	//------------------------------------------------------------------------------
	/// Retrieves the field data by the field ID
	///
	/// The fields are meta data optionally defined per each vector item.
	/// This method should be called only for raw layers or native layers that were converted with meta data
	/// (In the last case, the data is taken from the meta data file (for example: MetaData.sqlite)).
	///
	/// \param[in]	nFieldId			The ID of the field.
	/// \param[out]	pstrName			The retrieved field name.
	/// \param[out]	peFieldType			The retrieved field type.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetFieldData(UINT nFieldId, CMcString *pstrName, EFieldType *peFieldType) const = 0;

	//==============================================================================
	// Method Name: GetValidFieldsPerDataSource()
	//------------------------------------------------------------------------------
	/// Retrieves IDs of the fields valid for one data source specified by either one of its vector items, its name or its ID.
	///
	/// The fields are meta data optionally defined per each vector item.
	/// This method should be called only for raw layers or native layers that were converted with meta data
	/// (In the last case, the data is taken from the meta data file (for example: MetaData.sqlite)).
	///
	/// Useful for multi-source layer (several source files and/or several sub-layers per file).
	///
	/// \param[out]	pauValidFieldsIDs	IDs of the fields valid for the specified data source; 
	///									for single-source layer: IDs of all the fields.
	/// \param[in]	uVectorItemID		An ID of one of vector items of the data source 
	///									(ignored if \p strDataSourceName is not `NULL`/empty or \p uDataSourceID != `UINT_MAX`).
	/// \param[in]	strDataSourceName	The data source name (ignored if \p uDataSourceID != `UINT_MAX`).
	/// \param[in]	uDataSourceID		The data source ID (pass `UINT_MAX` if \p uVectorItemID or \p strDataSourceName should be used).
	/// 
	/// \note
	///		The data source is identified by either one of its vector items, its name or its ID (one of them must be specified); 
	///		the data source's name or ID (if specified) should be one of those returned by GetLayerDataSources() or VectorItemIDToOriginalID().
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetValidFieldsPerDataSource(CMcDataArray<UINT> *pauValidFieldsIDs, UINT64 uVectorItemID, 
		PCSTR strDataSourceName = NULL, UINT uDataSourceID = UINT_MAX) const = 0;

	//==============================================================================
	// Method Name: GetVectorItemsCount()
	//------------------------------------------------------------------------------
	/// Retrieves the number of vector items in the layer.
	///
	/// The vector items are the raw entities of the vector data.
	/// Each vector item contains points and optional meta data fields.
	///
	/// \param[out]	pnVectorItemsCount		The total number of vector items
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVectorItemsCount(UINT *pnVectorItemsCount) const = 0;

	//==============================================================================
	// Method Name: GetVectorItemPoints()
	//------------------------------------------------------------------------------
	/// Retrieves the points of a vector item by its ID.
	///
	/// A vector item is a raw entity of the vector data.
	/// Each vector item contains points and optional meta data fields.
	///
	/// \param[in]	nVectorItemId				The ID of the vector item.
	/// \param[out]	paaPoints					An array of one or more arrays of points.
	///											There is more than one array of points, only in case of multi-line or multi-polygon.
	/// \param[in]  pAsyncOperationCallback		Asynchronous callback. 
	///											The result will be returned in IAsyncOperationCallback::OnVectorItemPointsResult()
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVectorItemPoints(UINT64 nVectorItemId, CMcDataArray<CMcDataArray<SMcVector3D> > *paaPoints, IMcMapLayer::IAsyncOperationCallback *pAsyncOperationCallback = NULL) = 0;


	//==============================================================================
	// Method Name: GetScanExtendedData()
	//------------------------------------------------------------------------------
	/// Retrieves extended data of vector target found by the last call to IMcSpatialQueries::ScanInGeometry()
	///
	/// \param[in]	ScanGeometry					Geometry scanned by IMcSpatialQueries::ScanInGeometry()
	/// \param[in]	VectorTargetFound				Vector target found by IMcSpatialQueries::ScanInGeometry()
	/// \param[in]	pMapViewport					The map viewport
	/// \param[out]	paVectorItems					Vector VectorItems
	/// \param[out]	paUnifiedVectorItemsPoints		Points of polylines of unified VectorItems
	/// \param[in]  pAsyncOperationCallback			Asynchronous callback.
	///												The result will be returned in IAsyncOperationCallback::OnScanExtendedDataResult()
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetScanExtendedData(
		const SMcScanGeometry &ScanGeometry, const IMcSpatialQueries::STargetFound &VectorTargetFound, IMcMapViewport *pMapViewport,
		CMcDataArray<IMcMapLayer::SVectorItemFound> *paVectorItems, CMcDataArray<SMcVector3D> *paUnifiedVectorItemsPoints = NULL, IMcMapLayer::IAsyncOperationCallback *pAsyncOperationCallback = NULL) = 0;

	//==============================================================================
	// Method Name: GetFieldUniqueValuesAsInt()
	//------------------------------------------------------------------------------
	/// Retrieves all unique values of field by its ID, as integer.
	///
	/// The fields are meta data optionally defined per each vector item.
	/// This method should be called only for raw layers or native layers that were converted with meta data
	/// (In the last case, the data is taken from the meta data file (for example: MetaData.sqlite)).
	///
	/// \param[in]	nFieldId					The ID of the field.
	/// \param[out]	anUniqueValues				The retrieved unique values (as integer).
	/// \param[in]  pAsyncOperationCallback		Asynchronous callback. 
	///											The result will be returned in IAsyncOperationCallback::OnFieldUniqueValuesResult()
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetFieldUniqueValuesAsInt(UINT nFieldId, CMcDataArray<int> *anUniqueValues, IMcMapLayer::IAsyncOperationCallback *pAsyncOperationCallback = NULL) = 0;

	//==============================================================================
	// Method Name: GetFieldUniqueValuesAsDouble()
	//------------------------------------------------------------------------------
	/// Retrieves all unique values of field by its ID, as double.
	///
	/// The fields are meta data optionally defined per each vector item.
	/// This method should be called only for raw layers or native layers that were converted with meta data
	/// (In the last case, the data is taken from the meta data file (for example: MetaData.sqlite)).
	///
	/// \param[in]	nFieldId					The ID of the field.
	/// \param[out]	adUniqueValues				The retrieved unique values (as double).
	/// \param[in]  pAsyncOperationCallback		Asynchronous callback.
	///											The result will be returned in IAsyncOperationCallback::OnFieldUniqueValuesResult()
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetFieldUniqueValuesAsDouble(UINT nFieldId, CMcDataArray<double> *adUniqueValues, IMcMapLayer::IAsyncOperationCallback *pAsyncOperationCallback = NULL) = 0;

	//==============================================================================
	// Method Name: GetFieldUniqueValuesAsString()
	//------------------------------------------------------------------------------
	/// Retrieves all unique values of field by its ID, as ANSI strings.
	///
	/// The fields are meta data optionally defined per each vector item.
	/// This method should be called only for raw layers or native layers that were converted with meta data
	/// (In the last case, the data is taken from the meta data file (for example: MetaData.sqlite)).
	///
	/// \param[in]	nFieldId					The ID of the field.
	/// \param[out]	astrUniqueValues			The retrieved unique values (as ANSI strings).
	/// \param[in]  pAsyncOperationCallback		Asynchronous callback.
	///											The result will be returned in IAsyncOperationCallback::OnFieldUniqueValuesResult()
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetFieldUniqueValuesAsString(UINT nFieldId, CMcDataArray<CMcString> *astrUniqueValues, 
		IMcMapLayer::IAsyncOperationCallback *pAsyncOperationCallback = NULL) = 0;

	//==============================================================================
	// Method Name: GetFieldUniqueValuesAsWString()
	//------------------------------------------------------------------------------
	/// Retrieves all unique values of field by its ID, as UNICODE strings.
	///
	/// The fields are meta data optionally defined per each vector item.
	/// This method should be called only for raw layers or native layers that were converted with meta data
	/// (In the last case, the data is taken from the meta data file (for example: MetaData.sqlite)).
	///
	/// \param[in]	nFieldId					The ID of the field.
	/// \param[out]	astrUniqueValues			The retrieved unique values (as UNICODE strings).
	/// \param[in]  pAsyncOperationCallback		Asynchronous callback.
	///											The result will be returned in IAsyncOperationCallback::OnFieldUniqueValuesResult()
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetFieldUniqueValuesAsWString(UINT nFieldId, CMcDataArray<CMcWString> *astrUniqueValues, IMcMapLayer::IAsyncOperationCallback *pAsyncOperationCallback = NULL) = 0;

	//==============================================================================
	// Method Name: GetVectorItemFieldValueAsInt()
	//------------------------------------------------------------------------------
	/// Retrieves a specific field value of a specific vector item, as integer.
	///
	/// The fields are meta data optionally defined per each vector item.
	/// This method should be called only for raw layers or native layers that were converted with meta data
	/// (In the last case, the data is taken from the meta data file (for example: MetaData.sqlite)).
	///
	/// \param[in]	nVectorItemId				The ID of the vector item.
	/// \param[in]	nFieldId					The ID of the field.
	/// \param[out]	pnValue						The retrieved value (as integer).
	/// \param[in]  pAsyncOperationCallback		Asynchronous callback. 
	///											The result will be returned in IAsyncOperationCallback::OnVectorItemFieldValueResult()
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVectorItemFieldValueAsInt(UINT64 nVectorItemId, UINT nFieldId, int *pnValue, IMcMapLayer::IAsyncOperationCallback *pAsyncOperationCallback = NULL) = 0;

	//==============================================================================
	// Method Name: GetVectorItemFieldValueAsDouble()
	//------------------------------------------------------------------------------
	/// Retrieves a specific field value of a specific vector item, as double.
	///
	/// The fields are meta data optionally defined per each vector item.
	/// This method should be called only for raw layers or native layers that were converted with meta data
	/// (In the last case, the data is taken from the meta data file (for example: MetaData.sqlite)).
	///
	/// \param[in]	nVectorItemId				The ID of the vector item.
	/// \param[in]	nFieldId					The ID of the field.
	/// \param[out]	pdValue						The retrieved value (as double).
	/// \param[in]  pAsyncOperationCallback		Asynchronous callback.
	///											The result will be returned in IAsyncOperationCallback::OnVectorItemFieldValueResult()
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVectorItemFieldValueAsDouble(UINT64 nVectorItemId, UINT nFieldId, double *pdValue, IMcMapLayer::IAsyncOperationCallback *pAsyncOperationCallback = NULL) = 0;

	//==============================================================================
	// Method Name: GetVectorItemFieldValueAsString()
	//------------------------------------------------------------------------------
	/// Retrieves a specific field value of a specific vector item, as ANSI string.
	///
	/// The fields are meta data optionally defined per each vector item.
	/// This method should be called only for raw layers or native layers that were converted with meta data
	/// (In the last case, the data is taken from the meta data file (for example: MetaData.sqlite)).
	///
	/// \param[in]	nVectorItemId				The ID of the vector item.
	/// \param[in]	nFieldId					The ID of the field.
	/// \param[out]	pstrValue					The retrieved value (as ANSI string).
	/// \param[in]  pAsyncOperationCallback		Asynchronous callback. 
	///											The result will be returned in IAsyncOperationCallback::OnVectorItemFieldValueResult()
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVectorItemFieldValueAsString(UINT64 nVectorItemId, UINT nFieldId,CMcString *pstrValue, IMcMapLayer::IAsyncOperationCallback *pAsyncOperationCallback = NULL) = 0;

	//==============================================================================
	// Method Name: GetVectorItemFieldValueAsWString()
	//------------------------------------------------------------------------------
	/// Retrieves a specific field value of a specific vector item, as UNICODE string.
	///
	/// The fields are meta data optionally defined per each vector item.
	/// This method should be called only for raw layers or native layers that were converted with meta data
	/// (In the last case, the data is taken from the meta data file (for example: MetaData.sqlite)).
	///
	/// \param[in]	nVectorItemId				The ID of the vector item.
	/// \param[in]	nFieldId					The ID of the field.
	/// \param[out]	pstrValue					The retrieved value (as UNICODE string).
	/// \param[in]  pAsyncOperationCallback		Asynchronous callback.
	///											The result will be returned in IAsyncOperationCallback::OnVectorItemFieldValueResult()
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVectorItemFieldValueAsWString(UINT64 nVectorItemId, UINT nFieldId,CMcWString *pstrValue, IMcMapLayer::IAsyncOperationCallback *pAsyncOperationCallback = NULL) = 0;

	//==============================================================================
	// Method Name: Query()
	//------------------------------------------------------------------------------
	/// Retrieves array of all the vector item IDs that answer to the query.
	///
	/// The query should refer to field values. It should be in `SQL WHERE` syntax. 
	/// If a field name has spaces it should be surrounded by " characters. 
	/// If a field value is a string (with or without spaces), it should be surrounded by ' characters, 
	/// for example: "My Field1" = 'My Value' OR "My Field2" > 123.
	///
	/// The fields are meta data optionally defined per each vector item. 
	/// This method should be called only for raw layers or native layers that were converted with meta data, 
	/// in the latter case the data is taken from the meta data file (for example: MetaData.sqlite).
	///
	/// \param[in]	strAttributeFilter   		The query. The query string should be in the format of an SQL WHERE clause
	/// \param[out]	auVectorItemsID				The retrieved group of IDs.
	/// \param[in]  pAsyncOperationCallback		Asynchronous callback.
	///											The result will be returned in IAsyncOperationCallback::OnVectorQueryResult()
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Query(PCSTR strAttributeFilter,CMcDataArray <UINT64> *auVectorItemsID, IMcMapLayer::IAsyncOperationCallback *pAsyncOperationCallback = NULL) = 0;
   
	//@}

	/// \name Extended Geometry Data
	//@{

	//==============================================================================
	// Method Name: GetExtendedGeometryDataSize()
	//------------------------------------------------------------------------------
	/// Retrieves the size of the file containing data used in IMcVectorMapLayer::GetScanExtendedData() & IMcVectorMapLayer::GetVectorItemPoints()
	///
	/// This method should be called only for native layers.
	///
	/// \param[out]	puSizeInBytes		The file size in bytes.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetExtendedGeometryDataSize(UINT *puSizeInBytes) = 0;

	//==============================================================================
	// Method Name: LoadExtendedGeometryDataToMemory()
	//------------------------------------------------------------------------------
	/// Loads into memory, the file containing data used in IMcVectorMapLayer::GetScanExtendedData() & IMcVectorMapLayer::GetVectorItemPoints()
	///
	/// This method should be called only for native layers.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode LoadExtendedGeometryDataToMemory() = 0;

	//@}
};

//===========================================================================
// Interface Name: IMcNativeVectorMapLayer
//---------------------------------------------------------------------------
/// Interface for vector layer in MapCore format (converted) 
//===========================================================================
class IMcNativeVectorMapLayer : virtual public IMcVectorMapLayer
{
protected:
	virtual ~IMcNativeVectorMapLayer() {}

public:
	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 21
	};

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native vector layer
	///
	/// \param[out]	ppLayer					The native vector layer created
	/// \param[in]	strDirectory			A directory containing the layer's files
	/// \param[in]  pReadCallback			Callback receiving read layer events
	/// \param[in]	pLocalCacheLayerParams	Parameters of optional cache on local disk or NULL if 
	///										should not be used
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcNativeVectorMapLayer **ppLayer,
		PCSTR strDirectory,
		IReadCallback *pReadCallback = NULL, const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);

	//@}
};

//===========================================================================
// Interface Name: IMcNativeServerVectorMapLayer
//---------------------------------------------------------------------------
/// Interface for raster layer used to display data from MapCore's Map Layer Server
//===========================================================================
class IMcNativeServerVectorMapLayer : virtual public IMcVectorMapLayer
{
protected:
	virtual ~IMcNativeServerVectorMapLayer() {}

public:
	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 23
	};

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native-server vector layer
	///
	/// \param[out]	ppLayer				The native-server vector layer created
	/// \param[in]	strLayerURL			Layer's URL in the server
	/// \param[in]  pReadCallback		Callback receiving read layer events
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcNativeServerVectorMapLayer **ppLayer,
		PCSTR strLayerURL, IReadCallback *pReadCallback = NULL);

	//@}
};

//===========================================================================
// Interface Name: IMcRawVectorMapLayer
//---------------------------------------------------------------------------
/// Interface for raw vector layer (unconverted) 
//===========================================================================
class IMcRawVectorMapLayer : virtual public IMcVectorMapLayer
{
protected:
	virtual ~IMcRawVectorMapLayer() {}

public:
	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 22
	};

	///  properties of a single sub-layer from data source
	struct SDataSourceSubLayerProperties
	{
		UINT uLayerIndex;							///< layer's index in the data source
		char strLayerName[MAX_PATH];				///< layer's name
		EGeometry eGeometry;						///< layer's geometry
		EExtendedGeometry eExtendedGeometry;		///< layer's extended geometry
		char strMultiGeometriesSuffix[MAX_PATH];	///< suffix to be added to multi-geometry data source name to deal with this layer
		UINT uNumVectorItems;						///< layer's number of VectorItems

		SDataSourceSubLayerProperties() { strLayerName[0] = '\0'; strMultiGeometriesSuffix[0] = '\0'; }
	};

	///  properties of all sub-layers from data source
	struct SDataSourceSubLayersProperties
	{
		/// array of layers with their properties
		CMcDataArray<IMcRawVectorMapLayer::SDataSourceSubLayerProperties> aLayersProperties;
	};

	/// Auto styling type, used to define displaying raw vector data source without styling XML
	enum EAutoStylingType
	{
		EAST_NONE,						///< No automatic styling; use MapCore's default schemes, according to the data source's geometry.
		EAST_INTERNAL,					///< The data source's internal styling (currently supported for KML/KMZ formats only or when 
										///<   SInternalStylingParams::strStylingFile is passed); additional styling parameters data can be 
										///<   specified (see SInternalStylingParams)
		EAST_S52,						///< MapCore's pre-defined styling for S52/S57 format
		EAST_CUSTOM						///< The user-defined styling; a folder containing styling XMLs and MapCore's object schemes must be specified 
										///<   (see SParams::strCustomStylingFolder)
	};

	/// Parameters that enable auto creating MapCore's schemes based on the raw layer's internal styling data
	struct SInternalStylingParams
	{
		PCSTR	strOutputFolder;		///< The path for creating output XML file and Schemes folder;
										///<   If `NULL` (default), data source's folder will be used.
		PCSTR	strOutputXMLFileName;	///< The name (without path) of output XML file;
										///<   If `NULL` (default), XML file's name will be based on data source name.
		PCSTR    strStylingFile;		///< The optional styling file's full name (*.sld and *.lyrx are supported)
		IMcOverlayManager::ESavingVersionCompatibility eVersion;
										///< The output schemes compatibility to MapCore's previous versions;
										///<   the default is IMcOverlayManager::ESVC_LATEST (no compatibility).
		float	fMaxScaleFactor;		///< The maximal scale factor used in overlay manager when displaying raw vector layer

		IMcFont	*pDefaultFont;			///< The default font used if no font is defined in the internal styling; in case of IMcLogFont only it is
										///<   also used to take the additional parameters that are not present in the internal styling
		float	fTextMaxScale;			///< The maximal scale for displaying texts

		/// Constructor
		explicit SInternalStylingParams(
			PCSTR _strOutputFolder = NULL,
			PCSTR _strOutputXMLFileName = NULL,
			IMcOverlayManager::ESavingVersionCompatibility _eVersion = IMcOverlayManager::ESVC_LATEST,
			float _fMaxScaleFactor = 1,
			IMcFont	*_pDefaultFont = NULL,
			float _fTextMaxScale = FLT_MAX,
			PCSTR _strStylingFile = NULL) :
			strOutputFolder(_strOutputFolder),
			strOutputXMLFileName(_strOutputXMLFileName),
			strStylingFile(_strStylingFile),
			eVersion(_eVersion),
			fMaxScaleFactor(_fMaxScaleFactor),
			pDefaultFont(_pDefaultFont),
			fTextMaxScale(_fTextMaxScale)
		{
		}
	};

	/// Parameters for creating raw vector map layer or loading objects from raw vector data
	struct SParams
	{
		PCSTR strDataSource;								///< Raw(*.shp, *.mdb, etc)/XML file name / PostGIS Layer;
															///<   on IMcRawVectorMapLayer::Create(), in case of a multi-layer raw data source,
															///<   unless the proper suffix is provided, only the first sub-layer will be opened.
		IMcGridCoordinateSystem *pSourceCoordinateSystem;	///< Source's coordinate system.
		float fMinScale;									///< The minimal scale for displaying the layer. Not relevant for XML format;
															///<   minimal/maximal scales will be considered only if fMinScale < fMaxScale.
		float fMaxScale;									///< The maximal scale for displaying the layer. Not relevant for XML format;
															///<   minimal/maximal scales will be considered only if fMinScale < fMaxScale.
		PCSTR strPointTextureFile;							///< Texture file for point layer;
															///<   if not NULL, will also be used as default texture when using internal styling data.
		PCSTR strLocaleStr;									///< Optional locale string. If NULL or "", current locale is used.
		double dSimplificationTolerance;					///< Tolerance (in map units) of simplification of lines and polygons;
															///<   the default is 0 (no simplification).
		const SMcBox *pClipRect;							///< Optional clipping rectangle (x and y only are relevant).
															///<   for Create() - is in source's coordinate system. for Convert() - is in target coordinate system.
															///< If NULL or if all values are 0 (or DBL_MAX), no clipping will be done.
		EAutoStylingType eAutoStylingType;					///< Automatic styling type (see #EAutoStylingType); relevant only when strDataSource is NOT XML.
		PCSTR strCustomStylingFolder;						///< Custom styling folder containing styling XMLs and MapCore's object schemes;
															///<   relevant only when eAutoStylingType is #EAST_CUSTOM and strDataSource is NOT XML
		SInternalStylingParams StylingParams;				///< Styling params to be used when creating MapCore's schemes based on internal styling data;
															///<   relevant only when eAutoStylingType is #EAST_INTERNAL, and strDataSource is NOT XML.
		UINT uMaxNumVerticesPerTile;						///< Maximal number of vertices of vector objects per tile (if there are more, each vector 
															///<   object that can fit into one of the 4 child tiles will be moved into it); 
															///<   higher numbers improve performance and memory consumption in zoom-in, smaller numbers 
															///<   improve performance in zoom-out; the default is 100000; to disable use `UINT_MAX`
		UINT uMaxNumVisiblePointObjectsPerTile;				///< Maximal number of objects of vector points per tile that should be visible (objects 
															///<   beyond this number will be hidden depending on map scale); the default is 5000; 
															///<   to disable use `UINT_MAX`
		UINT uMinPixelSizeForObjectVisibility;				///< Minimal size in pixels of vector lines and polygons that should be visible (smaller 
															///<   objects will be hidden depending on map scale); the default is 8; to disable use 0
		float fOptimizationMinScale;						///< Minimal map scale for performing the optimization of hiding small pixel-size objects 
															///<   according to \b uMinPixelSizeForObjectVisibility and point-objects according to 
															///<   \b uMaxNumVisiblePointObjectsPerTile (at smaller scales the objects will not be 
															///<   hidden at all); the default is 0 (perform at any scale); 
															///<   to disable (do not perform at all) use `FLT_MAX`
		/// Constructor
		explicit SParams(
			PCSTR _strDataSource EMSCRIPTEN_ONLY(= NULL),
			IMcGridCoordinateSystem *_pSourceCoordinateSystem EMSCRIPTEN_ONLY(= NULL),
			float _fMinScale = 0, float _fMaxScale = 0,
			PCSTR _strPointTextureFile = NULL,
			PCSTR _strLocaleStr = NULL,
			double _dSimplificationTolerance = 0,
			const SMcBox *_pClipRect = NULL,
			const SInternalStylingParams &_StylingParams = SInternalStylingParams(),
			EAutoStylingType _eAutoStylingType = EAST_INTERNAL,
			PCSTR _strCustomStylingFolder = NULL) :
			strDataSource(_strDataSource),
			pSourceCoordinateSystem(_pSourceCoordinateSystem),
			fMinScale(_fMinScale), fMaxScale(_fMaxScale),
			strPointTextureFile(_strPointTextureFile),
			strLocaleStr(_strLocaleStr),
			dSimplificationTolerance(_dSimplificationTolerance),
			pClipRect(_pClipRect),
			StylingParams(_StylingParams),
			eAutoStylingType(_eAutoStylingType),
			strCustomStylingFolder(_strCustomStylingFolder),
			uMaxNumVerticesPerTile(100000),
			uMaxNumVisiblePointObjectsPerTile(5000),
			uMinPixelSizeForObjectVisibility(8),
			fOptimizationMinScale(0)
		{
		}
	};

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates raw vector layer
	///
	/// \param[out]	ppLayer						Raw vector layer created
	/// \param[in]	Params						Parameters for creating raw vector map layer
	/// \param[in]	pTargetCoordinateSystem		Optional layer's target coordinate system; default is NULL
	///											(Means same as layer's source coordinate system)
	/// \param[in]  pTilingScheme				Tiling scheme to use; default is NULL (means MapCore scheme)
	/// \param[in]  pReadCallback				Optional callback receiving read layer events; default is NULL
	/// \param[in]	pLocalCacheLayerParams		Parameters of optional cache on local disk or NULL if should not be used
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(
		IMcRawVectorMapLayer **ppLayer, const IMcRawVectorMapLayer::SParams &Params,
		IMcGridCoordinateSystem *pTargetCoordinateSystem = NULL,
		const IMcMapLayer::STilingScheme *pTilingScheme = NULL,
		IReadCallback *pReadCallback = NULL, const SLocalCacheLayerParams *pLocalCacheLayerParams = NULL);

	//==============================================================================
	// Method Name: GetCreateParams()
	//------------------------------------------------------------------------------
	/// Retrieves paramters set in Create()
	///
	/// \param[out]	pParams						Parameters for creating raw vector map layer
	/// \param[out]	ppTargetCoordinateSystem	Optional layer's target coordinate system
	///											(NULL means same as layer's source coordinate system)
	/// \param[out] ppTilingScheme				Tiling scheme to use (NULL means MapCore scheme) 
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCreateParams(
		IMcRawVectorMapLayer::SParams *pParams,
		IMcGridCoordinateSystem **ppTargetCoordinateSystem,
		const IMcMapLayer::STilingScheme **ppTilingScheme) const = 0;

	//@}

	/// \name Raw Vector Layer Parameters
	//@{

	//==============================================================================
	// Method Name: GetDataSourceSubLayersProperties()
	//------------------------------------------------------------------------------
	/// Retrieves properties of all sub-layers from data source
	///
	/// \param[in]	strDataSource					data source name (without layer suffix)
	/// \param[out]	pDataSourceProperties			properties of all sub-layers from data source
	/// \param[in]	bMultiGeometriesSuffixByName	whether `pDataSourceProperties->aLayersProperties[i].strMultiGeometriesSuffix` should be 
	///												based on layer's name or on its index
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode GetDataSourceSubLayersProperties(PCSTR strDataSource, SDataSourceSubLayersProperties *pDataSourceProperties,
		bool bMultiGeometriesSuffixByName = false);

	//==============================================================================
	// Method Name: GetStylingParams()
	//------------------------------------------------------------------------------
	/// Retrieves parameters used for creating MapCore's schemes based on internal styling data, defined in layer creation.
	///
	/// \param[out]	ppStylingParams		Parameters used for creating MapCore's schemes based on internal styling data
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetStylingParams(const SInternalStylingParams **ppStylingParams) const = 0;

	//================================================================
	// Method Name: IsRasterizedVectorLayer() 
	//----------------------------------------------------------------
	/// Retrieves whether or not the vector layer is of 'rasterized' type
	///
	/// \param[out]	pbIsRasterizedVectorLayer	Is vector layer of 'rasterized' type
	///
	/// \return
	///     - Status result
	//================================================================
	virtual IMcErrors::ECode IsRasterizedVectorLayer(bool *pbIsRasterizedVectorLayer) const = 0;

	//@}
};
