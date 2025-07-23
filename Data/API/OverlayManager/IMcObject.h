#pragma once
//==================================================================================
/// \file IMcObject.h
/// Interface for graphical object
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcBase.h"
#include "McCommonTypes.h"
#include "OverlayManager/IMcProperty.h"
#include "OverlayManager/IMcSymbolicItem.h"
#include "CMcDataArray.h"
#include "OverlayManager/IMcConditionalSelector.h"

class IMcOverlay;
class IMcOverlayManager;
class IMcCollection;
class IMcImageCalc;
class IMcObjectLocation;
class IMcObjectSchemeNode;
class IMcObjectSchemeItem;
class IMcObjectScheme;
class IMcMapViewport;
class IMcTraversabilityMapLayer;

//==================================================================================
// Interface Name: IMcObject
//----------------------------------------------------------------------------------
/// Interface for graphical object
//==================================================================================
class IMcObject : public virtual IMcBase
{
protected:

	virtual ~IMcObject() {};

	virtual IMcErrors::ECode SetProperty(const IMcProperty::SPropertyNameID &NameOrID, IMcProperty::EPropertyType eType, const void *pValue) = 0;
	virtual IMcErrors::ECode GetProperty(const IMcProperty::SPropertyNameID &NameOrID, IMcProperty::EPropertyType eType, void *pValue) const = 0;

public:

	//==============================================================================
	// Enum Name: EPositionInterpolationMode
	//------------------------------------------------------------------------------
	/// Path animation position interpolation mode used in PlayPathAnimation() to calculate 
	/// object positions between animation nodes.
	//==============================================================================
	enum EPositionInterpolationMode 
	{
		EPIM_LINEAR,	///< Values are interpolated along straight lines
		EPIM_SPLINE		///< Values are interpolated along a spline
	};

	//==============================================================================
	// Enum Name: ERotationInterpolationMode
	//------------------------------------------------------------------------------
	/// Path animation rotation interpolation mode used in PlayPathAnimation() to calculate 
	/// object rotations between animation nodes.
	//==============================================================================
	enum ERotationInterpolationMode
	{
		ERIM_LINEAR,	///< Values are interpolated linearly
						///< (faster but does not necessarily give a completely accurate result)
		ERIM_SPHERICAL	///< Values are interpolated spherically
						///< (more accurate but has a higher cost)
	};

	/// Parameters for attachment of object's location to a scheme node of another object
	struct SObjectToObjectAttachmentParams
	{
		/// object to attach to
		IMcObject							*pTargetObject;

		/// the object's scheme node to attach to
		IMcObjectSchemeNode					*pTargetNode;

		/// attach point parameters; the default is all the points \p pTargetNode or \p pTargetObject is based on
		IMcSymbolicItem::SAttachPointParams AttachPointParams;

		/// offset in object's location coordinate system
		SMcFVector3D						Offset;

		SObjectToObjectAttachmentParams() : pTargetObject(NULL), pTargetNode(NULL), Offset(vf3Zero) {}

		SObjectToObjectAttachmentParams(IMcObject *_pTargetObject, IMcObjectSchemeNode *_pTargetNode, SMcFVector3D _Offset = vf3Zero)
			: pTargetObject(_pTargetObject), pTargetNode(_pTargetNode), Offset(_Offset) {}
	};

	/// Symbology standards supported by MapCore 
	enum ESymbologyStandard : BYTE
	{
		ESS_NONE,	///< No symbology standard
		ESS_APP6D,	///< APP-6 (D) - Allied Procedural Publication Standard.
					///<  - The valid names for non-geometric amplifiers are "Additional Information", "Altitude/Depth", "Altitude/Depth 1", "Automatic Identification System (AIS)", 
					///<	"Capacity of Installation", "Combat Effectiveness", "Common Identifier", "Country Indicator", "Country Indicator 1", "Date Time Group (DTG)", 
					///<	"Date Time Group (DTG) (Period)", "Distance", "Dummy Indicator", "Echelon", "Engagement Bar", "Equipment Teardown Time", "Evaluation Rating", 
					///<	"Headquarters Staff Indicator", "Higher Formation", "Hostile", "IFF/SIF", "Installation Composition", "Location", "Mobility Indicator", 
					///<	"Operational Condition", "Platform Type", "Primary Purpose", "Quantity", "Reinforced or Reduced", "Signature Equipment", "Special Designator", 
					///<	"Speed", "Staff Comment", "Symbol Icon", "Target Number", "Type", "Type of Equipment", "Unique Designation", "Unique Designation 1", "Unlisted Point Information".
					///<  - The valid names for geometric amplifiers are "Azimuth", "Distance", "Distance 1".
		ESS_2525C	///< MIL-STD-2525 (C) - 2525 Military Standard.
	};

	/// Path animation node used in PlayPathAnimation()
	struct SPathAnimationNode
	{
		SMcVector3D	Position;             ///< position relative to the object's original location
		float		fTime;                ///< absolute time signature in seconds
		SMcRotation	ManualRotation;       ///< manual rotation definition
										  ///< (not relevant in automatic rotation mode)
	};

	/// Pair of string key and variant-property value
	struct SKeyVariantValue
	{
		PCSTR strKey;							///< unique string key
		IMcProperty::SVariantProperty Value;	///< variant-property value

		SKeyVariantValue() : strKey(NULL) {}
	};

	/// Definition of array of numbered keys
	struct SMultiKeyName
	{
		PCSTR strKeyBaseName;					///< base key's name
		UINT uNumAdditionalNames;				///< number of additional keys with names built as a concatenation of \p strKeyBaseName and 
												///< a number 1 through \p uNumAdditionalNames (can be zero if there is a single key only)
		SMultiKeyName() : strKeyBaseName(NULL) {}

	};

	/// \name Create, Clone, Attach, Remove
	//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates an object based on a scheme.
	///
	/// The object will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Remove() method is called,
	/// when its overlay is destroyed, or when its overlay manger is destroyed.
	///
	/// \param[out] ppCreatedObject		The created object
	/// \param[in]  pOverlay			The object's overlay
	/// \param[in]	pObjectScheme		The object scheme
	/// \param[in]  aLocationPoints		The first location points
	/// \param[in]  uNumLocationPoints	The number of location points
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcObject **ppCreatedObject,
		IMcOverlay *pOverlay,
		IMcObjectScheme *pObjectScheme,
		const SMcVector3D aLocationPoints[] = NULL,
		UINT uNumLocationPoints = 0);

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates an object along with its own scheme containing one location.
	///
	/// The object will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Remove() method is called,
	/// when its overlay is destroyed, or when its overlay manger is destroyed.
	///
	/// \param[out] ppCreatedObject			The created object
	/// \param[out] ppLocation				The first location created (pass `NULL` if unnecessary)
	/// \param[in]  pOverlay				The object's overlay
	/// \param[in]  eLocationCoordSystem	The location points' coordinate system
	/// \param[in]  aLocationPoints			The location's points
	/// \param[in]  uNumLocationPoints		The number of location's points
	/// \param[in]	bLocationRelativeToDTM	Whether location points' heights are relative to DTM
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcObject **ppCreatedObject,
		IMcObjectLocation **ppLocation,
		IMcOverlay *pOverlay,
		EMcPointCoordSystem eLocationCoordSystem,
		const SMcVector3D aLocationPoints[] = NULL,
		UINT uNumLocationPoints = 0,
		bool bLocationRelativeToDTM = false);

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates an object along with its own scheme containing one location and one item.
	///
	/// The object will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Remove() method is called,
	/// when its overlay is destroyed, or when its overlay manger is destroyed.
	///
	/// \param[out] ppCreatedObject			The created object
	/// \param[in]  pOverlay				The object's overlay
	/// \param[in]	pItem					The item to connect to location
	/// \param[in]  eLocationCoordSystem	The location points' coordinate system
	/// \param[in]  aLocationPoints			The location's points
	/// \param[in]  uNumLocationPoints		The number of location's points
	/// \param[in]	bLocationRelativeToDTM	Whether location points' height is relative to DTM
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcObject **ppCreatedObject,
		IMcOverlay *pOverlay,
		IMcObjectSchemeItem *pItem,
		EMcPointCoordSystem eLocationCoordSystem,
		const SMcVector3D aLocationPoints[] = NULL,
		UINT uNumLocationPoints = 0,
		bool bLocationRelativeToDTM = false);

	//==============================================================================
	// Method Name: Clone()
	//------------------------------------------------------------------------------
	/// Clones the object.
	///
	/// \param[out] ppClonedObject			The created clone
	/// \param[in]  pOverlay				The object's overlay
	/// \param[in]  bCloneObjectScheme		Whether the object scheme should be cloned or the new object 
	///										should use on the original scheme
	/// \param[in]  bClonePointsAndSubItems	Whether location points and sub-items private properties should be cloned 
	///										or the new object should be created with empty locations and with 
	///										scheme's default sub-items; the default is true
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Clone(IMcObject **ppClonedObject, IMcOverlay *pOverlay,
		bool bCloneObjectScheme, bool bClonePointsAndSubItems = true) = 0;

	//==============================================================================
	// Method Name: SetObjectToObjectAttachment()
	//------------------------------------------------------------------------------
	/// Sets or cancels attachment of a specified location to a scheme node of another object.
	/// 
	/// Each location is attached independently. The attached location's points are ignored and the actual points are taken and automatically updated 
	/// according to the target node defined by \p pAttachmentParams. Items connected to the attached location inherit the target node's rotation.
	///
	/// \param[in]	uAttachedLocationIndex	The index of the location to attach to another object.
	/// \param[in]	pAttachmentParams		The attachment parameters or `NULL` to cancel the attachment.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectToObjectAttachment(UINT uAttachedLocationIndex, 
		const SObjectToObjectAttachmentParams *pAttachmentParams) = 0;

	//==============================================================================
	// Method Name: GetObjectToObjectAttachment()
	//------------------------------------------------------------------------------
	/// Retrieves parameters of attachment of a specified location to a scheme node of another object set by SetObjectToObjectAttachment().
	///
	/// Each location is attached independently. The attached location's points are ignored and the actual points are taken and automatically updated 
	/// according to the target node defined by \p pAttachmentParams. Items connected to the attached location inherit the target node's rotation.
	///
	/// \param[in]	uAttachedLocationIndex	The index of the location attached to another object.
	/// \param[out]	ppAttachmentParams		The attachment parameters (`NULL` means no attachment 
	///										is defined for the specified location).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectToObjectAttachment(UINT uAttachedLocationIndex, 
		const SObjectToObjectAttachmentParams **ppAttachmentParams) const = 0;

	//==============================================================================
	// Method Name: IsAttachedToAnotherObject()
	//------------------------------------------------------------------------------
	/// Retrieves whether any of the object's locations is attached to any other object.
	///
	/// \param[out] pbIsAttached	Whether any of the object's locations is attached to any other object.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode IsAttachedToAnotherObject(
		bool *pbIsAttached) const = 0;

	//==============================================================================
	// Method Name: Remove()
	//------------------------------------------------------------------------------
	/// Removes the object from its overlay and destroys it.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Remove() = 0;

	//@}

	/// \name Symbology Standard
	//@{

	//==============================================================================
	// Method Name: CreatePointlessFromSymbology()
	//------------------------------------------------------------------------------
	/// Creates an object according to the specified symbology standard's symbol ID string and non-geometric amplifiers 
	/// (without anchor points and geometric amplifiers).
	/// 
	/// The anchor points and geometric amplifiers should be set later or initialized by EditMode.
	/// 
	/// IMcOverlayManager::InitializeSymbologyStandardSupport() should be called once for the appropriate standard before creating the object.
	///
	/// The object will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Remove() method is called,
	/// when its overlay is destroyed, or when its overlay manger is destroyed.
	///
	/// \param[out]	ppCreatedObject			The created object
	/// \param[in]	pOverlay				The object's overlay
	/// \param[in]	eSymbologyStandard		The symbology standard.
	/// \param[in]	strSymbolID				The symbol ID string that identifies the object in the standard and defines its graphical attributes
	/// \param[in]  aAmplifiers				The standard's non-geometric amplifiers (see #ESymbologyStandard for their valid names)
	/// \param[in]  uNumAmplifiers			The number of the non-geometric amplifiers in the above array
	/// \param[in]	bFlipped				Whether or not the object's display is flipped (ignored if not relevant)
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API CreatePointlessFromSymbology(
		IMcObject **ppCreatedObject,
		IMcOverlay *pOverlay,
		ESymbologyStandard eSymbologyStandard,
		PCSTR strSymbolID,
		const SKeyVariantValue aAmplifiers[],
		UINT uNumAmplifiers,
		bool bFlipped = false);

	//==============================================================================
	// Method Name: CreateFromSymbology()
	//------------------------------------------------------------------------------
	/// Creates an object according to the specified symbology standard's symbol ID string, anchor points and amplifiers.
	///
	/// IMcOverlayManager::InitializeSymbologyStandardSupport() should be called once for the appropriate standard before creating the object.
	///
	/// The object will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Remove() method is called,
	/// when its overlay is destroyed, or when its overlay manger is destroyed.
	///
	/// \param[out] ppCreatedObject			The created object
	/// \param[in]  pOverlay				The object's overlay
	/// \param[in]	eSymbologyStandard		The symbology standard.
	/// \param[in]  strSymbolID				The symbol ID string that identifies the object in the standard and defines its graphical attributes
	/// \param[in]  aAnchorPoints			The standard's anchor points
	/// \param[in]  uNumAnchorPoints		The number of the anchor points in the above array
	/// \param[in]  aGeometricAmplifiers	The standard's geometric amplifiers (see #ESymbologyStandard for their valid names)
	/// \param[in]  uNumGeometricAmplifiers The number of the geometric amplifiers in the above array
	/// \param[in]  aAmplifiers				The standard's non-geometric amplifiers (see #ESymbologyStandard for their valid names)
	/// \param[in]  uNumAmplifiers			The number of the non-geometric amplifiers in the above array
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API CreateFromSymbology(
		IMcObject **ppCreatedObject,
		IMcOverlay *pOverlay,
		ESymbologyStandard eSymbologyStandard,
		PCSTR strSymbolID,
		const SMcVector3D aAnchorPoints[],
		UINT uNumAnchorPoints,
		const SMcKeyFloatValue aGeometricAmplifiers[],
		UINT uNumGeometricAmplifiers,
		const SKeyVariantValue aAmplifiers[],
		UINT uNumAmplifiers);

	//==============================================================================
	// Method Name: GetSymbologyStandard()
	//------------------------------------------------------------------------------
	/// Checks whether the object was created according to a symbology standard and retrieves the standard.
	///
	/// \param[out]	peSymbologyStandard		The object's symbology standard (if the object was created by CreatePointlessFromSymbology() or CreateFromSymbology()) or
	///										#ESS_NONE (otherwise).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSymbologyStandard(
		ESymbologyStandard *peSymbologyStandard) const = 0;

	//==============================================================================
	// Method Name: SetSymbologyAnchorPointsAndGeometricAmplifiers()
	//------------------------------------------------------------------------------
	/// Sets the symbology standard's anchor points and geometric amplifiers.
	///
	/// Applicable only to objects created by CreatePointlessFromSymbology() or CreateFromSymbology().
	///
	/// \param[in]  aAnchorPoints			The standard's anchor points
	/// \param[in]  uNumAnchorPoints		The number of the anchor points in the above array
	/// \param[in]  aGeometricAmplifiers	The standard's geometric amplifiers (see #ESymbologyStandard for their valid names)
	/// \param[in]  uNumGeometricAmplifiers The number of the geometric amplifiers in the above array
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSymbologyAnchorPointsAndGeometricAmplifiers(
		const SMcVector3D aAnchorPoints[],
		UINT uNumAnchorPoints,
		const SMcKeyFloatValue aGeometricAmplifiers[],
		UINT uNumGeometricAmplifiers) = 0;

	//==============================================================================
	// Method Name: GetSymbologyAnchorPointsAndGeometricAmplifiers()
	//------------------------------------------------------------------------------
	/// Retrieves the symbology standard's anchor points and geometric amplifiers.
	///
	/// Applicable only to objects created by CreatePointlessFromSymbology() or CreateFromSymbology().
	///
	/// \param[out]  paAnchorPoints			The standard's anchor points
	/// \param[out]  paGeometricAmplifiers	The standard's geometric amplifiers
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSymbologyAnchorPointsAndGeometricAmplifiers(
		CMcDataArray<SMcVector3D> *paAnchorPoints,
		CMcDataArray<SMcKeyFloatValue> *paGeometricAmplifiers) const = 0;

	//==============================================================================
	// Method Name: SetSymbologyGraphicalProperties()
	//------------------------------------------------------------------------------
	/// Sets the symbology standard's graphical attributes (coded into the symbol ID) and non-geometric amplifiers.
	///
	/// Applicable only to objects created by CreatePointlessFromSymbology() or CreateFromSymbology().
	///
	/// \param[in]  strSymbolID				The symbol ID string that identifies the object in the standard and defines its graphical attributes
	///										(NULL or empty string if need not be set)
	/// \param[in]  aAmplifiers				The standard's non-geometric amplifiers (see #ESymbologyStandard for their valid names)
	/// \param[in]  uNumAmplifiers			The number of the non-geometric amplifiers in the above array
	///
	/// \note
	/// The parts of the symbol ID string identifying the object in the standard cannot be modified and should be equal to those passed during the object creation.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSymbologyGraphicalProperties(
		PCSTR strSymbolID, const SKeyVariantValue aAmplifiers[], UINT uNumAmplifiers) = 0;

	//==============================================================================
	// Method Name: GetSymbologyGraphicalProperties()
	//------------------------------------------------------------------------------
	/// Retrieves the symbology standard's symbol ID string and non-geometric amplifiers.
	///
	/// Applicable only to objects created by CreatePointlessFromSymbology() or CreateFromSymbology().
	///
	/// \param[out]	pstrSymbolID			The symbol ID string that identifies the object in the standard and defines its graphical attributes
	/// \param[out]	paAmplifiers			The standard's non-geometric amplifiers (pass `NULL` if unnecessary)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSymbologyGraphicalProperties(
		PCSTR *pstrSymbolID, CMcDataArray<SKeyVariantValue> *paAmplifiers = NULL) const = 0;

	//==============================================================================
	// Method Name: UpdateSymbologyTextualAmplifiersFromGeometricData()
	//------------------------------------------------------------------------------
	/// Updates those of the symbology standard's textual amplifiers that depend on the object's geometric data.
	///
	/// Applicable only to objects created by CreatePointlessFromSymbology() or CreateFromSymbology().
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode UpdateSymbologyTextualAmplifiersFromGeometricData() = 0;

	//@}

	/// \name Locations
	//@{

	//==============================================================================
	// Method Name: GetNumLocations()
	//------------------------------------------------------------------------------
	/// \copydoc IMcObjectScheme::GetNumObjectLocations()
	//==============================================================================
	virtual IMcErrors::ECode GetNumLocations(UINT *puNumLocations) const = 0;

	//==============================================================================
	// Method Name: GetLocationIndexByID()
	//------------------------------------------------------------------------------
	/// \copydoc IMcObjectScheme::GetObjectLocationIndexByID()
	//==============================================================================
	virtual IMcErrors::ECode GetLocationIndexByID(UINT uNodeID, UINT *puIndex) const = 0;

	//==============================================================================
	// Method Name: SetNumLocationPoints()
	//------------------------------------------------------------------------------
	/// Changes the number of location points in a given location.
	/// 
	/// In case the number is less than the current number, the array will be trimmed.
	///
	/// \param[in] uNumLocationPoints	The number of location points
	/// \param[in] uLocationIndex		The location's index
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetNumLocationPoints(
		UINT uNumLocationPoints,
		UINT uLocationIndex = 0) = 0;

	//==============================================================================
	// Method Name: SetLocationPoints()
	//------------------------------------------------------------------------------
	/// Changes the number of location points and their coordinates in a given location.
	///
	/// \param[in] aLocationPoints		The array of location points
	/// \param[in] uNumLocationPoints	The number of location points in the above array
	/// \param[in] uLocationIndex		The location's index
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetLocationPoints(
		const SMcVector3D aLocationPoints[],
		UINT uNumLocationPoints,
		UINT uLocationIndex = 0) = 0;

	//==============================================================================
	// Method Name: UpdateLocationPoints()
	//------------------------------------------------------------------------------
	/// Updates some location points of a given location without changing their number.
	///
	/// \param[in] aLocationPoints		The array of location points
	/// \param[in] uNumLocationPoints	The number of location points in the above array
	/// \param[in] uStartIndex			The index of the first point to update
	/// \param[in] uLocationIndex		The location's index
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode UpdateLocationPoints(
		const SMcVector3D aLocationPoints[],
		UINT uNumLocationPoints,
		UINT uStartIndex = 0,
		UINT uLocationIndex = 0) = 0;

	//==============================================================================
	// Method Name: GetLocationPoints()
	//------------------------------------------------------------------------------
	/// Retrieves the location points of a given location.
	///
	/// \param[out] paLocationPoints	The location points
	/// \param[in] uLocationIndex		The location's index
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLocationPoints(
		CMcDataArray<SMcVector3D> *paLocationPoints,
		UINT uLocationIndex = 0) const = 0;

	//==============================================================================
	// Method Name: AddLocationPoint()
	//------------------------------------------------------------------------------
	/// Adds a new location point to a given location.
	///
	/// To append a location point to the end of the array, use `UINT_MAX` index.
	///
	/// \param[in]  uInsertIndex	The index of the point to add, pass `UINT_MAX` to append to the end of the array
	/// \param[out] puInsertedIndex	The actual index of the added point
	/// \param[in]  LocationPoint	The location point to add
	/// \param[in]  uLocationIndex	The location's index
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode AddLocationPoint(
		UINT uInsertIndex,
		UINT *puInsertedIndex,
		const SMcVector3D &LocationPoint,
		UINT uLocationIndex = 0) = 0;

	//==============================================================================
	// Method Name: RemoveLocationPoint()
	//------------------------------------------------------------------------------
	/// Removes a location point from a given location.
	///
	/// To remove the last point in the array, use `UINT_MAX` index.
	///
	/// \param[in] uRemoveIndex		The index of the point to remove, pass `UINT_MAX` to remove the last point in the array.
	/// \param[in] uLocationIndex	The location's index
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RemoveLocationPoint(
		UINT uRemoveIndex,
		UINT uLocationIndex = 0) = 0;

	//==============================================================================
	// Method Name: UpdateLocationPoint()
	//------------------------------------------------------------------------------
	/// Updates a location point in a given location.
	///
	/// \param[in] uUpdateIndex		The index of the point to update
	/// \param[in] LocationPoint	The location point to update
	/// \param[in] uLocationIndex	The location's index
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode UpdateLocationPoint(
		UINT uUpdateIndex,
		const SMcVector3D &LocationPoint,
		UINT uLocationIndex = 0) = 0;

	//==============================================================================
	// Method Name: MoveAllLocationsPoints()
	//------------------------------------------------------------------------------
	/// Moves all locations points of all locations by a given offset
	///
	/// \param[in]	Offset		The offset
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode MoveAllLocationsPoints(
		const SMcVector3D &Offset) = 0;

	//==============================================================================
	// Method Name: SetEachObjectLocationPoint()
	//------------------------------------------------------------------------------
	/// For each specified object: sets one location point per object (in a given location)
	///
	/// \param[in]	apObjects			The  array of objects
	/// \param[in]	aLocationPoints		The array of location points (one point per object)
	/// \param[in]	uNumObjects			The number of elements in the above arrays
	/// \param[in]	uLocationIndex		The location's index
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API SetEachObjectLocationPoint(IMcObject* const apObjects[], 
		SMcVector3D aLocationPoints[], UINT uNumObjects, UINT uLocationIndex = 0);

	//@}

	/// \name Path Animation
	//@{

	//==============================================================================
	// Method Name: PlayPathAnimation()
	//------------------------------------------------------------------------------
	/// Plays path animation by moving the object's locations along specified path and rotating
	/// the object according either to the path or to the specified rotation definitions.
	///
	/// \param[in]	aPathAnimationNodes			The array of path animation nodes
	/// \param[in]	uNumPathAnimationNodes		The size of the above array
	/// \param[in]	ePositionInterpolationMode	The position interpolation mode (see #EPositionInterpolationMode)
	/// \param[in]	eRotationInterpolationMode	The rotation interpolation mode (see #ERotationInterpolationMode)
	/// \param[in]	fStartingTimePoint			The starting time point
	/// \param[in]	fRotationAdditionalYaw		Optional yaw angle to add for each node
	/// \param[in]	bAutomaticRotation			Whether rotation is to be calculated automatically
	///											according to the path (yaw only)
	///											or to be taken from animation nodes
	/// \param[in]	bLoop						Whether to play the path animation in loop
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode PlayPathAnimation(
		const SPathAnimationNode aPathAnimationNodes[],
		UINT uNumPathAnimationNodes,
		EPositionInterpolationMode ePositionInterpolationMode,
		ERotationInterpolationMode eRotationInterpolationMode,
		float fStartingTimePoint,
		float fRotationAdditionalYaw,
		bool bAutomaticRotation,
		bool bLoop) = 0;

	//==============================================================================
	// Method Name: StopPathAnimation()
	//------------------------------------------------------------------------------
	/// Stops playing path animation started by PlayPathAnimation().
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode StopPathAnimation() = 0;

	//@}

	/// \name Rotate by Item, Screen Arrangement Offset
	//@{

	//==============================================================================
	// Method Name: RotateByItem()
	//------------------------------------------------------------------------------
	/// Rotates the object, by rotating the item defined in IMcObjectScheme::SetObjectRotationItem()
	/// If no item is defined, the default item is used (the first item connected to the first location).
	///
	/// \param[in]	Rotation	The rotation definition
	///
	/// \note
	///		If there are several objects based on the same scheme, the item's rotation properties
	///		should be defined as private (otherwise the function would rotate all these objects).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RotateByItem(const SMcRotation &Rotation) = 0;

	//==============================================================================
	// Method Name: SetScreenArrangementOffset()
	//------------------------------------------------------------------------------
	/// Sets or cancels the screen arrangement offset of the item defined by 
	/// IMcObjectScheme::SetObjectScreenArrangementItem() for a given viewport.
	///
	/// \see IMcOverlayManager::SetScreenArrangement()
	///
	/// \param[in] pMapViewport		The viewport to set the screen arrangement offset in.
	/// \param[in] Offset			The screen arrangement offset to set or zero to cancel.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetScreenArrangementOffset(
		IMcMapViewport *pMapViewport, const SMcFVector2D &Offset) = 0;

	//==============================================================================
	// Method Name: GetScreenArrangementOffset()
	//------------------------------------------------------------------------------
	/// Retrieves the screen arrangement offset set by SetScreenArrangementOffset() for a given viewport.
	///
	/// \see IMcOverlayManager::SetScreenArrangement()
	///
	/// \param[in]	pMapViewport	The viewport to retrieve the screen arrangement offset from.
	/// \param[out] pOffset			The screen arrangement offset or zero if not set.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetScreenArrangementOffset(
		IMcMapViewport *pMapViewport, SMcFVector2D *pOffset) const = 0;

	//@}

	/// \name Image-calculation Interface
	//@{

	//==============================================================================
	// Method Name: SetImageCalc()
	//------------------------------------------------------------------------------
	/// Sets the image-calculation interface for all locations with #EPCS_IMAGE coordinate system.
	///
	/// \param[in]	pLocationImageCalc		The image-calculation interface
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetImageCalc(
		IMcImageCalc *pLocationImageCalc) = 0;

	//==============================================================================
	// Method Name: GetImageCalc()
	//------------------------------------------------------------------------------
	/// Retrieves the image calculation interface set by SetImageCalc().
	///
	/// \param[in]	ppLocationImageCalc		The image calculation interface
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetImageCalc(
		IMcImageCalc **ppLocationImageCalc) const = 0;

	//@}

	/// \name Overlay, Overlay Manager, Collections
	//@{

	//==============================================================================
	// Method Name: SetOverlay()
	//------------------------------------------------------------------------------
	/// Moves the object to another overlay.
	///
	/// \param[in] pOverlay      The object's new overlay
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetOverlay(IMcOverlay *pOverlay) = 0;

	//==============================================================================
	// Method Name: GetOverlay()
	//------------------------------------------------------------------------------
	/// Retrieves the object's overlay.
	///
	/// \param[out] ppOverlay      The object's overlay
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetOverlay(
		IMcOverlay **ppOverlay) const = 0;

	//==============================================================================
	// Method Name: GetOverlayManager()
	//------------------------------------------------------------------------------
	/// Retrieves the object's overlay manager.
	/// 
	/// \param[out] ppOverlayManager	The object's overlay manager
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetOverlayManager(
		IMcOverlayManager **ppOverlayManager) const = 0;

	//==============================================================================
	// Method Name: GetCollections()
	//------------------------------------------------------------------------------
	/// Retrieves all the collections that the object is a member of.
	///
	/// \param[out] papCollections	The array of collections
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetCollections(
		CMcDataArray<IMcCollection*> *papCollections) const = 0;

	//@}

	/// \name Object Scheme
	//@{

	//==============================================================================
	// Method Name: SetScheme()
	//------------------------------------------------------------------------------
	/// Changes the object's scheme.
	///
	/// The object will be adjusted according to the new scheme.
	///
	/// \param[in] pObjectScheme			The object's new scheme
	/// \param[in] bKeepRelevantProperties	Whether to keep values of the object's private properties that have the same IDs and types 
	///										in the new scheme or to use the new  scheme's default values.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetScheme(
		IMcObjectScheme *pObjectScheme, bool bKeepRelevantProperties = true) = 0;

	//==============================================================================
	// Method Name: GetScheme()
	//------------------------------------------------------------------------------
	/// Retrieves the object's scheme set in Create() or SetScheme().
	///
	/// \param [out] ppObjectScheme		The object's scheme
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetScheme(IMcObjectScheme **ppObjectScheme) const = 0;

	//==============================================================================
	// Method Name: GetNodeByID()
	//------------------------------------------------------------------------------
	/// \copydoc IMcObjectScheme::GetNodeByID()
	//==============================================================================
	virtual IMcErrors::ECode GetNodeByID(UINT uNodeID, IMcObjectSchemeNode **ppNode) const = 0;

	//==============================================================================
	// Method Name: GetNodeByName()
	//------------------------------------------------------------------------------
	/// \copydoc IMcObjectScheme::GetNodeByName()
	//==============================================================================
	virtual IMcErrors::ECode GetNodeByName(PCSTR strNodeName, IMcObjectSchemeNode **ppNode) const = 0;

	//@}

	/// \name Sight/Traversability Presentation
	//@{

	//==============================================================================
	// Method Name: SetSuppressQueryPresentationMapTilesWebRequests()
	//------------------------------------------------------------------------------
	/// Sets the mode of suppressing requests of tiles of web-based DTM/static-objects layers for calculation of query-presentation items.
	///
	/// \param[in]	bSuppress	Whether or not the web requests should be suppressed; the default is false
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSuppressQueryPresentationMapTilesWebRequests(bool bSuppress) = 0;

	//==============================================================================
	// Method Name: GetSuppressQueryPresentationMapTilesWebRequests()
	//------------------------------------------------------------------------------
	/// Retrieves the mode of suppressing requests of tiles of web-based DTM/static-objects layers for calculation of query-presentation items
	///
	/// \param[out]	pbSuppress	Whether or not the web requests are suppressed; the default is false
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSuppressQueryPresentationMapTilesWebRequests(bool *pbSuppress) const = 0;

	//==============================================================================
	// Method Name: SetTraversabilityPresentationMapLayer()
	//------------------------------------------------------------------------------
	/// Sets the traversability map layer to be used in line/arrow items with traversability presentation
	///
	/// \param[in]	pMapLayer	The traversability map layer to be used in the object's line/arrow items with traversability presentation;
	///							can be NULL (the default) if there is a single traversability map layer of the viewport and this layer should be used.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTraversabilityPresentationMapLayer(IMcTraversabilityMapLayer *pMapLayer) = 0;

	//==============================================================================
	// Method Name: GetTraversabilityPresentationMapLayer()
	//------------------------------------------------------------------------------
	/// Retrieves the traversability map layer set by SetTraversabilityPresentationMapLayer().
	///
	/// \param[out]	ppMapLayer	The traversability map layer used in the object's line/arrow items with traversability presentation.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTraversabilityPresentationMapLayer(IMcTraversabilityMapLayer **ppMapLayer) const = 0;

	//@}

	/// \name Object ID, Name, Description and User Data
	//@{

	//==============================================================================
	// Method Name: SetID()
	//------------------------------------------------------------------------------
	/// Sets the object's user-defined unique ID that allows retrieving the object by IMcOverlay::GetObjectByID().
	///
	/// \param[in]	uID		The object's ID to be set (specify ID not equal to #MC_EMPTY_ID to set ID,
	///						equal to #MC_EMPTY_ID to remove ID).
	/// \note
	/// The ID should be unique in the object's overlay, otherwise the function returns IMcErrors::ID_ALREADY_EXISTS
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetID(UINT uID) = 0;

	//==============================================================================
	// Method Name: GetID()
	//------------------------------------------------------------------------------
	/// Retrieves the object's user-defined unique ID set by SetID().
	///        
	/// \param [out] puID	The object ID (or #MC_EMPTY_ID if the ID is not set).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetID(UINT *puID) const = 0;


	//==============================================================================
	// Method Name: SetNameAndDescription()
	//------------------------------------------------------------------------------
	/// Sets the object's user-defined name and description.
	///
	/// \param[in]	strName				The object's name to be set (or `NULL` to remove the current name); do not have to be unique.
	/// \param[in]	strDescription		The object's description to be set (or `NULL` to remove the current description).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetNameAndDescription(PCSTR strName, PCSTR strDescription) = 0;

	//==============================================================================
	// Method Name: GetNameAndDescription()
	//------------------------------------------------------------------------------
	/// Retrieves the object's name and description set by SetNameAndDescription() or filled by IMcOverlay::LoadObjectsFromRawVectorData().
	///        
	/// \param [out] pstrName			The object's name (pass `NULL` if unnecessary).
	/// \param [out] pstrDescription	The object's description (pass `NULL` if unnecessary).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetNameAndDescription(PCSTR *pstrName, PCSTR *pstrDescription) const = 0;


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
	/// Sets the object's draw priority defining the drawing order of the object's symbolic items relative to items of other objects.
	///
	/// Either the draw priority in one specific viewport or the draw priority default in all viewports is set. 
	/// Setting the draw priority default in all viewports overrides a draw priority in each 
	/// viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] nPriority		The object's draw priority; the range is -32768 to 32767;
	///								objects with higher priority are rendered on top of objects with lower priority; the default is 0.
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
	/// Retrieves the object's draw priority set by SetDrawPriority().
	///
	/// Either the draw priority in one specific viewport or the draw priority default in all viewports is retrieved.
	///
	/// \param[out] pnPriority		The object's draw priority
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
	/// Sets the object's visibility option in one specific viewport or the default visibility option in all viewports.
	///
	/// Setting the visibility option default in all viewports overrides a visibility option in each 
	/// viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// The objects's visibility in a specific viewport depends on its visibility option for this viewport along with 
	/// its visibility conditional selector's result (only if the visibility option is IMcConditionalSelector::EAO_USE_SELECTOR - the default), 
	/// the visibility of collections the object belongs to and on similar parameters of the object's overlay.
	///
	/// \param[in]	eVisibility		The object's visibility option; the default is IMcConditionalSelector::EAO_USE_SELECTOR.
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
	/// Sets the object's visibility option in the specified viewports.
	///
	/// Just calls SetVisibilityOption() for each viewport specified.
	///
	/// \param[in]	eVisibility			The object's visibility option; the default is IMcConditionalSelector::EAO_USE_SELECTOR.
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
	/// Retrieves the object's visibility option in one specific viewport (set by SetVisibilityOption() with specific viewport(s)) or 
	/// the default visibility in all viewports (set by SetVisibilityOption() without viewport).
	///
	/// The objects's visibility in a specific viewport depends on its visibility option for this viewport along with 
	/// its visibility conditional selector's result (only if the visibility option is IMcConditionalSelector::EAO_USE_SELECTOR - the default), 
	/// the visibility of collections the object belongs to and on similar parameters of the object's overlay.
	///
	/// \param[out]	peVisibility	The object's visibility option
	/// \param[in]	pMapViewport	The viewport to retrieve a visibility option for or `NULL` for the default visibility in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetVisibilityOption(IMcConditionalSelector::EActionOptions *peVisibility, 
												 IMcMapViewport *pMapViewport = NULL) const = 0;

	//==============================================================================
	// Method Name: GetEffectiveVisibilityInViewport()
	//------------------------------------------------------------------------------
	/// Retrieves the object's current effective visibility in the specified viewport.
	///
	/// The result is irrespective to the viewport's current visible area. It depends on its visibility option for this viewport along with 
	/// its visibility conditional selector's result (only if the visibility option is IMcConditionalSelector::EAO_USE_SELECTOR - the default), 
	/// the visibility of collections the object belongs to and on similar parameters of the object's overlay..
	///
	/// \param[in]	pMapViewport	The viewport to check a visibility in.
	/// \param[out]	pbVisible		Whether the object is currently visible in the viewport
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
	/// Sets the object's conditional selector controlling the object's visibility.
	///
	/// \param[in] eActionType		The selector's action type (only IMcConditionalSelector::EAT_VISIBILITY is applicable here).
	/// \param[in] bActionOnResult	Defines which selector result makes the object visible (in other words: whether the selector serves as either *if* or *else* condition).
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
	/// Retrieves the object's conditional selector controlling the object's visibility set by SetConditionalSelector()
	///
	/// \param[in]  eActionType			The selector's action type (only IMcConditionalSelector::EAT_VISIBILITY is applicable here).
	/// \param[out] pbActionOnResult	Defines which selector result makes the object visible (in other words: whether the selector serves as either *if* or *else* condition).
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
	// Method Name: SetIgnoreViewportVisibilityMaxScale()
	//------------------------------------------------------------------------------
	/// Sets whether viewport's objects visibility maximal scale (set by 
	/// IMcMapViewport::SetObjectsVisibilityMaxScale()) should be ignored for the object.
	///
	/// \param[in] bIgnoreViewportVisibilityMaxScale	Whether visibility max scale should be ignored.
	///								
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetIgnoreViewportVisibilityMaxScale(
		bool bIgnoreViewportVisibilityMaxScale) = 0;

	//==============================================================================
	// Method Name: GetIgnoreViewportVisibilityMaxScale()
	//------------------------------------------------------------------------------
	/// Retrieves whether viewport's objects visibility maximal scale (set by 
	/// IMcMapViewport::SetObjectsVisibilityMaxScale()) is ignored for the object, set by SetIgnoreViewportVisibilityMaxScale().
	///
	/// \param[out] pbIgnoreViewportVisibilityMaxScale	Whether visibility max scale is ignored
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetIgnoreViewportVisibilityMaxScale(
		bool *pbIgnoreViewportVisibilityMaxScale) const = 0;

	//@}

	/// \name Detectibility
	//@{

	//==============================================================================
	// Method Name: SetDetectibility()
	//------------------------------------------------------------------------------
	/// Sets whether the object will be retrieved by IMcSpatialQueries::ScanInGeometry().
	///
	/// Either the detectibility in one specific viewport or the detectibility default in all viewports is set. 
	/// Setting the detectibility default in all viewports overrides a detectibility in each 
	/// viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] bDetectibility	The object's detectibility; the default is true.
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
	/// Retrieves the object's detectibility set by SetDetectibility().
	///
	/// Either the detectibility in one specific viewport or the detectibility default in all viewports is retrieved.
	///
	/// \param[out] pbDetectibility		The object's detectibility; the default is true.
	/// \param[in]	pMapViewport		The viewport to retrieve a detectibility for or `NULL` for the detectibility default in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetDetectibility(
		bool *pbDetectibility, IMcMapViewport *pMapViewport = NULL) const = 0;

	//@}

    /// \name State
    //@{

	//==============================================================================
	// Method Name: SetState()
	//------------------------------------------------------------------------------
	/// Sets the object's state as a single sub-state.
	///
	/// Either the state in one specific viewport or the state default in all viewports is set. Setting the state default in all viewports 
	/// overrides a state in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] uState			The object's new state a single sub-state.
	/// \param[in] pMapViewport		The viewport to set the state for or `NULL` for the state default in all viewports.
	///
	/// \note
	/// - The object's state determines the versions of the properties to be used. 
	/// - The actual versions of properties used by the object in a specific viewport at specific time are determined according to 
	///   the object's current effective state in the specified viewport at that time, see GetEffectiveState() for details.
	/// - If the object's effective state consists of several sub-states, the property version is that of the first sub-state defined 
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
	/// Sets the object's state as an array of sub-states.
	///
	/// Either the state in one specific viewport or the state default in all viewports is set. Setting the state default in all viewports 
	/// overrides a state in each viewport previously set. On the other hand, it can be changed later for any specific viewport.
	///
	/// \param[in] auStates			The object's new state as an array of sub-states.
	/// \param[in] uNumStates		The number of sub-states in the above array.
	/// \param[in] pMapViewport		The viewport to set the state for or `NULL` for the state default in all viewports.
	///
	/// \note
	/// - The object's state determines the versions of the properties to be used. 
	/// - The actual versions of properties used by the object in a specific viewport at specific time are determined according to 
	///   the object's current effective state in the specified viewport at that time, see GetEffectiveState() for details.
	/// - If the object's effective state consists of several sub-states, the property version is that of the first sub-state defined 
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
	/// Retrieves the object's state set by SetState().
	///
	/// Either the state in one specific viewport or the state default in all viewports is retrieved.
	///
	/// \param[out] pauStates		The array of the object's sub-states.
	/// \param[in]	pMapViewport	The viewport to retrieve the state for or `NULL` for the state default in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetState(CMcDataArray<BYTE> *pauStates, IMcMapViewport *pMapViewport = NULL) const = 0;

	//==============================================================================
	// Method Name: GetEffectiveState()
	//------------------------------------------------------------------------------
	/// Retrieves the object's current effective state in the specified viewport.
	///
	/// The effective state consists of (in the following order):
	/// - sub-states of the object's overlay defined by IMcOverlay::SetState(), 
	/// - sub-states of the object itself defined by SetState(),
	/// - sub-states currently calculated by relevant object-state modifiers of the object's scheme in a specified viewport 
	///   (only if non-`NULL` \p pMapViewport is specified).
	///
	/// \param[out] pauStates		The array of the object's sub-states of the effective state.
	/// \param[in]	pMapViewport	The viewport to retrieve the state for or `NULL` for the state default in all viewports.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetEffectiveState(CMcDataArray<BYTE> *pauStates, IMcMapViewport *pMapViewport = NULL) const = 0;

    //@}

	/// \name Properties
	//@{

    //==============================================================================
    // Method Name: ResetProperty()
    //------------------------------------------------------------------------------
    /// Resets a property by linking it back to its default defined in the object's scheme
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode ResetProperty(const IMcProperty::SPropertyNameID &NameOrID) = 0;

    //==============================================================================
    // Method Name: ResetAllProperties()
    //------------------------------------------------------------------------------
    /// Resets all the properties by linking them back to their defaults defined in the object's scheme.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode ResetAllProperties() = 0;

    //==============================================================================
    // Method Name: IsPropertyDefault()
    //------------------------------------------------------------------------------
    /// Checks whether a property is linked to its default defined in the object's scheme or its value is overridden by the object.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pbDefault	Whether the property is linked to its default defined in the object's scheme or 
    ///							its value is overridden by the object.
	///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode IsPropertyDefault(const IMcProperty::SPropertyNameID &NameOrID, bool *pbDefault) = 0;

	//==============================================================================
    // Method Name: SetBoolProperty()
    //------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetBoolProperty(const IMcProperty::SPropertyNameID &NameOrID, bool Value) = 0;

    //==============================================================================
    // Method Name: GetBoolProperty()
    //------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetBoolProperty(const IMcProperty::SPropertyNameID &NameOrID, bool *pValue) const = 0;

    //==============================================================================
    // Method Name: SetByteProperty()
    //------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetByteProperty(const IMcProperty::SPropertyNameID &NameOrID, BYTE Value) = 0;

    //==============================================================================
    // Method Name: GetByteProperty()
    //------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetByteProperty(const IMcProperty::SPropertyNameID &NameOrID, BYTE *pValue) const = 0;

	//==============================================================================
	// Method Name: SetSByteProperty()
	//------------------------------------------------------------------------------
	/// Overrides/updates the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[in]	Value		The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSByteProperty(const IMcProperty::SPropertyNameID &NameOrID, signed char Value) = 0;

	//==============================================================================
	// Method Name: GetSByteProperty()
	//------------------------------------------------------------------------------
	/// Retrieves the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[out] pValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSByteProperty(const IMcProperty::SPropertyNameID &NameOrID, signed char *pValue) const = 0;

	//==============================================================================
	// Method Name: SetEnumProperty()
	//------------------------------------------------------------------------------
	/// Overrides/updates the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetEnumProperty(const IMcProperty::SPropertyNameID &NameOrID, UINT Value) = 0;

	//==============================================================================
	// Method Name: GetEnumProperty()
	//------------------------------------------------------------------------------
	/// Retrieves the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetEnumProperty(const IMcProperty::SPropertyNameID &NameOrID, UINT *pValue) const = 0;

    //==============================================================================
    // Method Name: SetIntProperty()
    //------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetIntProperty(const IMcProperty::SPropertyNameID &NameOrID, int Value) = 0;

    //==============================================================================
    // Method Name: GetIntProperty()
    //------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetIntProperty(const IMcProperty::SPropertyNameID &NameOrID, int *pValue) const = 0;

    //==============================================================================
    // Method Name: SetUIntProperty()
    //------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetUIntProperty(const IMcProperty::SPropertyNameID &NameOrID, UINT Value) = 0;

    //==============================================================================
    // Method Name: GetUIntProperty()
    //------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetUIntProperty(const IMcProperty::SPropertyNameID &NameOrID, UINT *pValue) const = 0;

	//==============================================================================
    // Method Name: SetFloatProperty()
	//------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetFloatProperty(const IMcProperty::SPropertyNameID &NameOrID, float Value) = 0;

    //==============================================================================
    // Method Name: GetFloatProperty()
    //------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
    virtual IMcErrors::ECode GetFloatProperty(const IMcProperty::SPropertyNameID &NameOrID, float *pValue) const = 0;

    //==============================================================================
    // Method Name: SetDoubleProperty()
    //------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetDoubleProperty(const IMcProperty::SPropertyNameID &NameOrID, double Value) = 0;

    //==============================================================================
    // Method Name: GetDoubleProperty()
    //------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetDoubleProperty(const IMcProperty::SPropertyNameID &NameOrID, double *pValue) const = 0;

    //==============================================================================
    // Method Name: SetVector2DProperty()
    //------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetVector2DProperty(const IMcProperty::SPropertyNameID &NameOrID, const SMcVector2D &Value) = 0;

    //==============================================================================
    // Method Name: GetVector2DProperty()
    //------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetVector2DProperty(const IMcProperty::SPropertyNameID &NameOrID, SMcVector2D *pValue) const = 0;

	//==============================================================================
    // Method Name: SetVector2DProperty()
    //------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetVector2DProperty(const IMcProperty::SPropertyNameID &NameOrID, const SMcFVector2D &Value) = 0;

    //==============================================================================
    // Method Name: GetVector2DProperty()
	//------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
    virtual IMcErrors::ECode GetVector2DProperty(const IMcProperty::SPropertyNameID &NameOrID, SMcFVector2D *pValue) const = 0;

    //==============================================================================
    // Method Name: SetVector3DProperty()
    //------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetVector3DProperty(const IMcProperty::SPropertyNameID &NameOrID, const SMcVector3D &Value) = 0;

    //==============================================================================
    // Method Name: GetVector3DProperty()
    //------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetVector3DProperty(const IMcProperty::SPropertyNameID &NameOrID, SMcVector3D *pValue) const = 0;

	//==============================================================================
    // Method Name: SetVector3DProperty()
	//------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetVector3DProperty(const IMcProperty::SPropertyNameID &NameOrID, const SMcFVector3D &Value) = 0;

    //==============================================================================
    // Method Name: GetVector3DProperty()
    //------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
    virtual IMcErrors::ECode GetVector3DProperty(const IMcProperty::SPropertyNameID &NameOrID, SMcFVector3D *pValue) const = 0;

    //==============================================================================
    // Method Name: SetColorProperty()
    //------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetColorProperty(const IMcProperty::SPropertyNameID &NameOrID, const SMcBColor &Value) = 0;

    //==============================================================================
    // Method Name: GetColorProperty()
    //------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetColorProperty(const IMcProperty::SPropertyNameID &NameOrID, SMcBColor *pValue) const = 0;

	//==============================================================================
    // Method Name: SetColorProperty()
    //------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetColorProperty(const IMcProperty::SPropertyNameID &NameOrID, const SMcFColor &Value) = 0;

    //==============================================================================
    // Method Name: GetColorProperty()
    //------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetColorProperty(const IMcProperty::SPropertyNameID &NameOrID, SMcFColor *pValue) const = 0;

    //==============================================================================
    // Method Name: SetAttenuationProperty()
    //------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetAttenuationProperty(const IMcProperty::SPropertyNameID &NameOrID, const SMcAttenuation &Value) = 0;

    //==============================================================================
    // Method Name: GetAttenuationProperty()
    //------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetAttenuationProperty(const IMcProperty::SPropertyNameID &NameOrID, SMcAttenuation *pValue) const = 0;

    //==============================================================================
    // Method Name: SetStringProperty()
    //------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	Value		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetStringProperty(const IMcProperty::SPropertyNameID &NameOrID, const SMcVariantString &Value) = 0;

    //==============================================================================
    // Method Name: GetStringProperty()
    //------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out] pValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetStringProperty(const IMcProperty::SPropertyNameID &NameOrID, SMcVariantString *pValue) const = 0;

	//==============================================================================
	// Method Name: SetFontProperty()
	//------------------------------------------------------------------------------
	/// Overrides/updates the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	pValue		The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetFontProperty(const IMcProperty::SPropertyNameID &NameOrID, IMcFont *pValue) = 0;

	//==============================================================================
	// Method Name: GetFontProperty()
	//------------------------------------------------------------------------------
	/// Retrieves the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	ppValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetFontProperty(const IMcProperty::SPropertyNameID &NameOrID, IMcFont **ppValue) const = 0;

    //==============================================================================
    // Method Name: SetTextureProperty()
    //------------------------------------------------------------------------------
    /// Overrides/updates the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	pValue		The new value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode SetTextureProperty(const IMcProperty::SPropertyNameID &NameOrID, IMcTexture *pValue) = 0;

    //==============================================================================
    // Method Name: GetTextureProperty()
    //------------------------------------------------------------------------------
    /// Retrieves the value of the object's property.
    ///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	ppValue		The value.
    ///
    /// \return
    ///     - status result
    //==============================================================================
    virtual IMcErrors::ECode GetTextureProperty(const IMcProperty::SPropertyNameID &NameOrID, IMcTexture **ppValue) const = 0;

	//==============================================================================
	// Method Name: SetMeshProperty()
	//------------------------------------------------------------------------------
	/// Overrides/updates the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[in]	pValue		The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetMeshProperty(const IMcProperty::SPropertyNameID &NameOrID, IMcMesh *pValue) = 0;

	//==============================================================================
	// Method Name: GetMeshProperty()
	//------------------------------------------------------------------------------
	/// Retrieves the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
    /// \param[out]	ppValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetMeshProperty(const IMcProperty::SPropertyNameID &NameOrID, IMcMesh **ppValue) const = 0;

	//==============================================================================
	// Method Name: SetConditionalSelectorProperty()
	//------------------------------------------------------------------------------
	/// Overrides/updates the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[in]	pValue		The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetConditionalSelectorProperty(const IMcProperty::SPropertyNameID &NameOrID, 
															IMcConditionalSelector *pValue) = 0;

	//==============================================================================
	// Method Name: GetConditionalSelectorProperty()
	//------------------------------------------------------------------------------
	/// Retrieves the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[out]	ppValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetConditionalSelectorProperty(const IMcProperty::SPropertyNameID &NameOrID, 
															IMcConditionalSelector **ppValue) const = 0;

	//==============================================================================
	// Method Name: SetRotationProperty()
	//------------------------------------------------------------------------------
	/// Overrides/updates the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[in]	Value		The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetRotationProperty(const IMcProperty::SPropertyNameID &NameOrID, const SMcRotation &Value) = 0;

	//==============================================================================
	// Method Name: GetRotationProperty()
	//------------------------------------------------------------------------------
	/// Retrieves the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[out] pValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRotationProperty(const IMcProperty::SPropertyNameID &NameOrID, SMcRotation *pValue) const = 0;

	//==============================================================================
	// Method Name: SetAnimationProperty()
	//------------------------------------------------------------------------------
	/// Overrides/updates the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[in]	Value		The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetAnimationProperty(const IMcProperty::SPropertyNameID &NameOrID, const SMcAnimation &Value) = 0;

	//==============================================================================
	// Method Name: GetAnimationProperty()
	//------------------------------------------------------------------------------
	/// Retrieves the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[out] pValue		The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAnimationProperty(const IMcProperty::SPropertyNameID &NameOrID, SMcAnimation *pValue) const = 0;

	//==============================================================================
	// Method Name: SetArrayProperty()
	//------------------------------------------------------------------------------
	/// Overrides/updates the value of the object's property.
	///
	/// \param[in]	NameOrID		The property's unique name or ID (name if it is not empty, ID otherwise); 
	///								name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[in]	ePropertyType	The property type (IMcProperty::EPT_XXX_ARRAY).
	/// \param[in]	Value			The new value.
	///
	/// \return
	///     - status result
	//==============================================================================
	template <typename T>
	inline IMcErrors::ECode SetArrayProperty(const IMcProperty::SPropertyNameID &NameOrID, IMcProperty::EPropertyType ePropertyType, 
		const IMcProperty::SArrayProperty<T> &Value)
	{
		return SetProperty(NameOrID, ePropertyType, &Value);
	}

	//==============================================================================
	// Method Name: GetArrayProperty()
	//------------------------------------------------------------------------------
	/// Retrieves the value of the object's property.
	///
	/// \param[in]	NameOrID		The property's unique name or ID (name if it is not empty, ID otherwise); 
	///								name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[in]	ePropertyType	The property type (IMcProperty::EPT_XXX_ARRAY).
	/// \param[out]	pValue			The value.
	///
	/// \return
	///     - status result
	//==============================================================================
	template <typename T>
		inline IMcErrors::ECode GetArrayProperty(const IMcProperty::SPropertyNameID &NameOrID, IMcProperty::EPropertyType ePropertyType, 
			IMcProperty::SArrayProperty<T> *pValue) const
	{
		return GetProperty(NameOrID, ePropertyType, pValue);
	}

	//==============================================================================
	// Method Name: SetProperty()
	//------------------------------------------------------------------------------
	/// Overrides/updates the value of the object's property.
	///
	/// \param[in] Property   The property's name or ID (name if it is not empty, ID otherwise) along with type and value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetProperty(const IMcProperty::SVariantProperty &Property) = 0;

	//==============================================================================
	// Method Name: GetProperty()
	//------------------------------------------------------------------------------
	/// Retrieves the value of the object's property.
	///
	/// \param[in]	NameOrID	The property's unique name or ID (name if it is not empty, ID otherwise); 
	///							name or ID can also be passed to the function directly instead of IMcProperty::SPropertyNameID.
	/// \param[out]	pProperty	The property name, ID, type and value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetProperty(const IMcProperty::SPropertyNameID &NameOrID, IMcProperty::SVariantProperty *pProperty) const = 0;

	//==============================================================================
	// Method Name: SetProperties()
	//------------------------------------------------------------------------------
	/// Updates values of several properties.
	///
	/// \param[in] aProperties[]	The array of properties, each one is identified by name or ID (name if it is not empty, ID otherwise) and 
	///								contains type and value.
	/// \param[in] uNumProperties	The number of properties in the above array.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetProperties(const IMcProperty::SVariantProperty aProperties[], 
										   UINT uNumProperties) = 0;

	//==============================================================================
	// Method Name: GetProperties()
	//------------------------------------------------------------------------------
	/// Retrieves all the properties with their values.
	///
	/// \param[out] paProperties	The array of properties, each one contains name, ID, type and value.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetProperties(CMcDataArray<IMcProperty::SVariantProperty> *paProperties) const = 0;

    //==============================================================================
    // Method Name: GetPropertyType()
    //------------------------------------------------------------------------------
    /// \copydoc IMcObjectScheme::GetPropertyType()
    //==============================================================================
	virtual IMcErrors::ECode GetPropertyType(const IMcProperty::SPropertyNameID &NameOrID, IMcProperty::EPropertyType *peType, 
		bool bNoFailOnNonExistent = false) const = 0;

    //==============================================================================
    // Method Name: GetEnumPropertyActualType()
    //------------------------------------------------------------------------------
    /// \copydoc IMcObjectScheme::GetEnumPropertyActualType()
    //==============================================================================
	virtual IMcErrors::ECode GetEnumPropertyActualType(const IMcProperty::SPropertyNameID &NameOrID, PCSTR *pstrTypeName) const = 0;

	//==============================================================================
	// Method Name: UpdatePropertiesAndLocationPoints()
	//------------------------------------------------------------------------------
	/// Updates several properties and location points of a given location.
	///
	/// \param[in] aProperties[]		The array of properties, each one is defined by name or ID (name if it is not empty, ID otherwise) and 
	///									contains type and value.
	/// \param[in] uNumProperties		The number of properties in the above array.
	/// \param[in] aLocationPoints		The location points.
	/// \param[in] uNumLocationPoints	The number of location points in the above array.
	/// \param[in] uStartIndex			The index of the first point to update.
	/// \param[in] uLocationIndex		The index of location.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode UpdatePropertiesAndLocationPoints(
		const IMcProperty::SVariantProperty aProperties[], UINT uNumProperties, 
		const SMcVector3D aLocationPoints[], UINT uNumLocationPoints, UINT uStartIndex = 0, 
		UINT uLocationIndex = 0) = 0;

	//==============================================================================
	// Method Name: GetPropertiesAndLocationPoints()
	//------------------------------------------------------------------------------
	/// Retrieves all the properties and location points of a given location.
	///
	/// \param[out] paProperties		The array of properties, each one contains name, ID, type and value.
	/// \param[out] paLocationPoints	The location points.
	/// \param[in] uLocationIndex		The index of location.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetPropertiesAndLocationPoints(
		CMcDataArray<IMcProperty::SVariantProperty> *paProperties, 
		CMcDataArray<SMcVector3D> *paLocationPoints, UINT uLocationIndex = 0) const = 0;

	//==============================================================================
	// Method Name: SetEachObjectProperty()
	//------------------------------------------------------------------------------
	/// For each specified object: sets property per object
	///
	/// \param[in]	apObjects			The array of objects.
	/// \param[in]	aProperties			The array of properties (one property per object).
	/// \param[in]	uNumObjects			The number of elements in the above arrays.
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API SetEachObjectProperty(IMcObject* const apObjects[], 
		const IMcProperty::SVariantProperty aProperties[], UINT uNumObjects);
	//@}

	template<typename TNode, typename TProperty, typename TPropertySetArg>
	IMcErrors::ECode SetEffectivePropertyValue(IMcMapViewport *pMapViewport, IMcObjectSchemeNode *pNode, TProperty PropertyValue,
		IMcErrors::ECode (TNode::*pGetFunction)(TProperty *pPropertyValue, UINT *puPropertyID, BYTE uObjectState) const, 
		IMcErrors::ECode (TNode::*pSetFunction)(TPropertySetArg PropertyValue, UINT uPropertyID, BYTE uObjectState));

	template<typename TNode, typename TProperty, typename TPropertySetArg>
	IMcErrors::ECode SetEffectivePropertyValue(IMcObjectSchemeNode *pNode, TProperty PropertyValue,
		IMcErrors::ECode(TNode::*pGetFunction)(TProperty *pPropertyValue, UINT *puPropertyID) const,
		IMcErrors::ECode(TNode::*pSetFunction)(TPropertySetArg PropertyValue, UINT uPropertyID));

	template<typename TNode, typename TProperty>
	IMcErrors::ECode GetEffectivePropertyValue(IMcMapViewport *pMapViewport, IMcObjectSchemeNode *pNode, TProperty *pPropertyValue,
		IMcErrors::ECode (TNode::*pGetFunction)(TProperty *pPropertyValue, UINT *puPropertyID, BYTE uObjectState) const,
		UINT *puPropertyID = NULL);

	template<typename TNode, typename TProperty>
	IMcErrors::ECode GetEffectivePropertyValue(IMcObjectSchemeNode *pNode, TProperty *pPropertyValue,
		IMcErrors::ECode(TNode::*pGetFunction)(TProperty *pPropertyValue, UINT *puPropertyID) const,
		UINT *puPropertyID = NULL);

	template<typename TNode, typename TProperty>
	IMcErrors::ECode GetAllStatesPropertyValues(IMcObjectSchemeNode *pNode, 
		TProperty apPropertyValues[256], UINT *puNumPropertyValues, 
		IMcErrors::ECode (TNode::*pGetFunction)(TProperty *pPropertyValue, UINT *puPropertyID, BYTE uObjectState) const,
		UINT auPropertyIDs[256] = NULL);
};

template<typename TNode, typename TProperty, typename TPropertySetArg>
IMcErrors::ECode IMcObject::SetEffectivePropertyValue(IMcMapViewport *pMapViewport, IMcObjectSchemeNode *pNode, TProperty PropertyValue,
	IMcErrors::ECode (TNode::*pGetFunction)(TProperty *pPropertyValue, UINT *puPropertyID, BYTE uObjectState) const, 
	IMcErrors::ECode (TNode::*pSetFunction)(TPropertySetArg PropertyValue, UINT uPropertyID, BYTE uObjectState))
{
	CMcDataArray<BYTE> aObjectStates;
	IMcErrors::ECode eRet = GetEffectiveState(&aObjectStates, pMapViewport);
	if (eRet != IMcErrors::SUCCESS)
	{
		return eRet;
	}
	UINT uNumObjectStates = aObjectStates.GetLength();

	BYTE uObjectState = 0; 
	UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID;
	TProperty DummyPropertyValue;
	for (UINT uIdx = 0; uIdx < uNumObjectStates; ++uIdx)
	{
		eRet = (dynamic_cast<TNode*>(pNode)->*pGetFunction)(&DummyPropertyValue, &uPropertyID, aObjectStates[uIdx]);
		if (eRet != IMcErrors::SUCCESS)
		{
			return eRet;
		}

		if (uPropertyID != IMcProperty::EPPI_NO_STATE_PROPERTY_ID && uPropertyID != IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID)
		{
			uObjectState = aObjectStates[uIdx];
			break;
		}
	}
	if (uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID || uPropertyID == IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID)
	{
		uObjectState = 0;
		eRet = (dynamic_cast<TNode*>(pNode)->*pGetFunction)(&DummyPropertyValue, &uPropertyID, 0);
		if (eRet != IMcErrors::SUCCESS)
		{
			return eRet;
		}
	}

	if (uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID)
	{
		eRet = (dynamic_cast<TNode*>(pNode)->*pSetFunction)(PropertyValue, IMcProperty::EPPI_SHARED_PROPERTY_ID, uObjectState);
		if (eRet != IMcErrors::SUCCESS)
		{
			return eRet;
		}
	}
	else
	{
		IMcProperty::SVariantProperty VariantProperty;
		eRet = GetProperty(IMcProperty::SPropertyNameID(uPropertyID), &VariantProperty);
		if (eRet != IMcErrors::SUCCESS)
		{
			return eRet;
		}

		VariantProperty.Value.SetAs(PropertyValue);
		eRet = SetProperty(VariantProperty);
		if (eRet != IMcErrors::SUCCESS)
		{
			return eRet;
		}
	}

	return IMcErrors::SUCCESS;
}

template<typename TNode, typename TProperty, typename TPropertySetArg>
IMcErrors::ECode IMcObject::SetEffectivePropertyValue(IMcObjectSchemeNode *pNode, TProperty PropertyValue,
	IMcErrors::ECode(TNode::*pGetFunction)(TProperty *pPropertyValue, UINT *puPropertyID) const,
	IMcErrors::ECode(TNode::*pSetFunction)(TPropertySetArg PropertyValue, UINT uPropertyID))
{
	UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID;
	TProperty DummyPropertyValue;
	IMcErrors::ECode eRet = (dynamic_cast<TNode*>(pNode)->*pGetFunction)(&DummyPropertyValue, &uPropertyID);
	if (eRet != IMcErrors::SUCCESS)
	{
		return eRet;
	}

	if (uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID)
	{
		eRet = (dynamic_cast<TNode*>(pNode)->*pSetFunction)(PropertyValue, IMcProperty::EPPI_SHARED_PROPERTY_ID);
		if (eRet != IMcErrors::SUCCESS)
		{
			return eRet;
		}
	}
	else
	{
		IMcProperty::SVariantProperty VariantProperty;
		eRet = GetProperty(IMcProperty::SPropertyNameID(uPropertyID), &VariantProperty);
		if (eRet != IMcErrors::SUCCESS)
		{
			return eRet;
		}

		VariantProperty.Value.SetAs(PropertyValue);
		eRet = SetProperty(VariantProperty);
		if (eRet != IMcErrors::SUCCESS)
		{
			return eRet;
		}
	}
	return IMcErrors::SUCCESS;
}

template<typename TNode, typename TProperty>
IMcErrors::ECode IMcObject::GetEffectivePropertyValue(IMcMapViewport *pMapViewport, IMcObjectSchemeNode *pNode, TProperty *pPropertyValue,
	IMcErrors::ECode (TNode::*pGetFunction)(TProperty *pPropertyValue, UINT *puPropertyID, BYTE uObjectState) const,
	UINT *puPropertyID)
{
	CMcDataArray<BYTE> aObjectStates;
	IMcErrors::ECode eRet = GetEffectiveState(&aObjectStates, pMapViewport);
	if (eRet != IMcErrors::SUCCESS)
	{
		return eRet;
	}
	UINT uNumObjectStates = aObjectStates.GetLength();

	//BYTE uObjectState = 0;
	UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID;
	for (UINT uIdx = 0; uIdx < uNumObjectStates; ++uIdx)
	{
		eRet = (dynamic_cast<TNode*>(pNode)->*pGetFunction)(pPropertyValue, &uPropertyID, aObjectStates[uIdx]);
		if (eRet != IMcErrors::SUCCESS)
		{
			return eRet;
		}

		if (uPropertyID != IMcProperty::EPPI_NO_STATE_PROPERTY_ID && uPropertyID != IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID)
		{
			//uObjectState = aObjectStates[uIdx];

			if (uPropertyID != IMcProperty::EPPI_SHARED_PROPERTY_ID)
			{
				IMcProperty::SVariantProperty VariantProperty;
				eRet = GetProperty(IMcProperty::SPropertyNameID(uPropertyID), &VariantProperty);
				if (eRet != IMcErrors::SUCCESS)
				{
					return eRet;
				}

				VariantProperty.Value.GetAs(pPropertyValue);
			}
			if (puPropertyID != NULL)
			{
				*puPropertyID = uPropertyID;
			}
			return eRet;
		}
	}

	// every state returned EPPI_NO_STATE_PROPERTY_ID or EPPI_NO_MORE_STATE_PROPERTIES_ID, use zero-state
	{
		//uObjectState = 0;

		eRet = (dynamic_cast<TNode*>(pNode)->*pGetFunction)(pPropertyValue, &uPropertyID, 0);
		if (eRet != IMcErrors::SUCCESS)
		{
			return eRet;
		}
	}

	if (uPropertyID != IMcProperty::EPPI_SHARED_PROPERTY_ID)
	{
		IMcProperty::SVariantProperty VariantProperty;
		eRet = GetProperty(IMcProperty::SPropertyNameID(uPropertyID), &VariantProperty);
		if (eRet != IMcErrors::SUCCESS)
		{
			return eRet;
		}

		VariantProperty.Value.GetAs(pPropertyValue);
	}
	if (puPropertyID != NULL)
	{
		*puPropertyID = uPropertyID;
	}
	return IMcErrors::SUCCESS;
}

template<typename TNode, typename TProperty>
IMcErrors::ECode IMcObject::GetEffectivePropertyValue(IMcObjectSchemeNode *pNode, TProperty *pPropertyValue,
	IMcErrors::ECode(TNode::*pGetFunction)(TProperty *pPropertyValue, UINT *puPropertyID) const,
	UINT *puPropertyID)
{
	UINT uPropertyID;
	IMcErrors::ECode eRet = (dynamic_cast<TNode*>(pNode)->*pGetFunction)(pPropertyValue, &uPropertyID);
	if (eRet != IMcErrors::SUCCESS)
	{
		return eRet;
	}

	if (uPropertyID != IMcProperty::EPPI_SHARED_PROPERTY_ID)
	{
		IMcProperty::SVariantProperty VariantProperty;
		eRet = GetProperty(IMcProperty::SPropertyNameID(uPropertyID), &VariantProperty);
		if (eRet != IMcErrors::SUCCESS)
		{
			return eRet;
		}

		VariantProperty.Value.GetAs(pPropertyValue);
	}
	if (puPropertyID != NULL)
	{
		*puPropertyID = uPropertyID;
	}
	return IMcErrors::SUCCESS;
}

template<typename TNode, typename TProperty>
IMcErrors::ECode IMcObject::GetAllStatesPropertyValues(IMcObjectSchemeNode *pNode, 
	TProperty apPropertyValues[256], UINT *puNumPropertyValues, 
	IMcErrors::ECode (TNode::*pGetFunction)(TProperty *pPropertyValue, UINT *puPropertyID, BYTE uObjectState) const,
	UINT auPropertyIDs[256])
{
	*puNumPropertyValues = 0;
	UINT uNumPropertyValues = 0;
	for (UINT uIdx = 0; uIdx < 256; ++uIdx)
	{
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID;
		IMcErrors::ECode eRet = (dynamic_cast<TNode*>(pNode)->*pGetFunction)(&apPropertyValues[uNumPropertyValues], &uPropertyID, uIdx);
		if (eRet != IMcErrors::SUCCESS)
		{
			return eRet;
		}

		// Note: set all values, not only the shared/private, so there will be no uninitialized values...
		if (auPropertyIDs != NULL)
		{
			auPropertyIDs[uIdx] = uPropertyID;
		}

		if (uPropertyID == IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID)
		{
			break;
		}
		if (uPropertyID != IMcProperty::EPPI_NO_STATE_PROPERTY_ID)
		{
			if (uPropertyID != IMcProperty::EPPI_SHARED_PROPERTY_ID)
			{
				IMcProperty::SVariantProperty VariantProperty;
				eRet = GetProperty(IMcProperty::SPropertyNameID(uPropertyID), &VariantProperty);
				if (eRet != IMcErrors::SUCCESS)
				{
					return eRet;
				}

				VariantProperty.Value.GetAs(&apPropertyValues[uNumPropertyValues]);
			}
			++uNumPropertyValues;
		}
	}
	*puNumPropertyValues = uNumPropertyValues;
	return IMcErrors::SUCCESS;
}
