#pragma once
//===========================================================================
/// \file IMcEditMode.h
/// Interface for interactive operations in a map viewport performed by the user.
//===========================================================================

#include "McCommonTypes.h"
#include "CMcDataArray.h"
#include "IMcErrors.h"
#include "IMcDestroyable.h"
#include "Calculations/IMcGridCoordinateSystem.h"
#include "Map/IMcMapCamera.h"

class IMcMapViewport;
class IMcObjectSchemeItem;
class IMcLineItem;
class IMcRectangleItem;
class IMcPictureItem;
class IMcTextItem;
class IMcOverlay;
class IMcObject;
struct SMcBColor;
class CMcTime;

#ifndef EDITMODE_API
#ifdef _WIN32
	#ifdef EDITMODE_EXPORTS
	#define EDITMODE_API __declspec(dllexport)
	#else
	#define EDITMODE_API __declspec(dllimport)
	#endif
#else
	#ifdef EDITMODE_EXPORTS
	#define EDITMODE_API 
	#else
	#define EDITMODE_API 
	#endif
#endif
#endif
//===========================================================================
// Interface Name: IMcEditMode
//---------------------------------------------------------------------------
/// Interface for interactive operations in a map viewport performed by the user.
///
/// Allows to:
/// - initialize and edit objects;
/// - drag and rotate the map;
/// - change the scale (zoom level) of the map;
/// - measure distances on the map;
/// - calculate height and volume of objects in an image viewport.
//===========================================================================
class IMcEditMode : public IMcDestroyable
{
protected:

	virtual ~IMcEditMode() {};

public:

	class ICallback;

	/// The cursor types
	///
	/// The cursor type is chosen according to the currently preformed operation,
	/// triggered by OnMouseEvent().
	/// The application is supposed to set the appropriate cursor, according to it.
	enum ECursorType
	{
		ECT_DEFAULT_CURSOR,	///< The appropriate type of cursor, for example: IDC_ARROW
		ECT_DRAG_CURSOR,	///< The appropriate type of cursor, for example: IDC_HAND
		ECT_MOVE_CURSOR,	///< The appropriate type of cursor, for example: IDC_SIZEALL
		ECT_EDIT_CURSOR		///< The appropriate type of cursor, for example: IDC_CROSS
	};

	/// Mouse events
	enum EMouseEvent
	{
		EME_BUTTON_PRESSED,				///< The relevant mouse button or first finger is pressed
		EME_BUTTON_RELEASED,			///< The relevant mouse button or first finger is released
		EME_BUTTON_DOUBLE_CLICK,		///< The relevant mouse button is double clicked
		EME_MOUSE_MOVED_BUTTON_DOWN,	///< The mouse is moved when the relevant mouse button is down (touch screen: any finger is pressed)
		EME_MOUSE_MOVED_BUTTON_UP,		///< The mouse is moved when the relevant mouse button is up  (touch screen: any finger is released)
		EME_MOUSE_WHEEL	,				///< The mouse wheel is rotated
		EME_SECOND_TOUCH_PRESSED,       ///< For the touch screen: second finger is pressed
		EME_SECOND_TOUCH_RELEASED		///< For the touch screen: second finger is released
	};

	/// Mouse-move usage (relevant in creation of lines, polygons, arrows etc.)
	enum EMouseMoveUsage
	{
		EMMU_REGULAR,					///< The regular usage
		EMMU_IGNORED,					///< The mouse-move is ignored; items are updated by mouse clicks only (good for touch screen)
		EMMU_ADDS_POINT,				///< Each mouse-move adds a point as mouse click does
		EMMU_TYPES			  			///< The number of the enum's members (not to be used as a valid type)
	};

	/// Key events
	enum EKeyEvent
	{
		EKE_MOVE_LEFT,		///< Move down an item/map
		EKE_MOVE_RIGHT,		///< Move right an item/map
		EKE_MOVE_UP,		///< Move up an item/map
		EKE_MOVE_DOWN,		///< Move down an item/map
		EKE_RAISE,			///< Raise 3D an item / 3D map
		EKE_LOWER,			///< Lower 3D an item / 3D map
		EKE_ROTATE_LEFT,	///< Rotate the map left (a yaw angle)
		EKE_ROTATE_RIGHT,	///< Rotate the map right (a yaw angle)
		EKE_ROTATE_UP,		///< Rotate the map up (a pitch angle of 3D map)
		EKE_ROTATE_DOWN,	///< Rotate the map down (a pitch angle of 3D map)
		EKE_DELETE_VERTEX,	///< Delete the current item vertex
		EKE_NEXT_ICON,		///< Go to the next edit icon
		EKE_PREV_ICON,		///< Go to the previous edit icon
		EKE_CONFIRM,		///< Confirm the current operation
		EKE_ABORT,			///< Abort the current operation
	};

	/// Types of steps used in navigation or editing by keys
	enum EKeyStepType
	{
		EKST_MAP_MOVE_PIXELS,               ///< The step in pixels used when moving map
		EKST_OBJECT_MOVE_PIXELS,            ///< The step in pixels used when moving objects
		EKST_ROTATION_DEGREES,              ///< The step in degrees used when rotating map or objects
		EKST_3D_EDIT_MOVE_WORLD_UNITS,		///< The step in world map units used when moving 3D objects
		EKST_3D_EDIT_RESIZE_FACTOR          ///< The factor used when resizing 3D objects
	};

	/// Flags for editing permissions 
	enum EPermission
	{
		EEMP_NONE						= 0x0000,	///< Allows nothing
		EEMP_MOVE_VERTEX				= 0x0001,	///< Allows moving a vertex
		EEMP_BREAK_EDGE					= 0x0002,	///< Allows breaking a line/polygon segment
		EEMP_RESIZE						= 0x0004,	///< Allows resizing a rectangle/ellipse etc.
		EEMP_ROTATE						= 0x0008,	///< Allows rotating an item
		EEMP_DRAG						= 0x0010,	///< Allows dragging an item
		EEMP_FINISH_TEXT_STRING_BY_KEY	= 0x0020	///< Allows finishing editing text string by ENTER key instead of adding new line
	};
	
	/// Utility picture types used for editing operations
	enum EUtilityPictureType
	{
		EUPT_VERTEX_ACTIVE,			///< The icon for selected point of multi points item
		EUPT_VERTEX_REGULAR,		///< The icon for non selected point of multi points item
		EUPT_MID_EDGE_ACTIVE,   	///< The icon for selected middle edge point of multi points item
		EUPT_MID_EDGE_REGULAR,		///< The icon for non selected middle edge point of multi points item
		EUPT_MOVE_ITEM_ACTIVE,		///< The icon for selected middle point of item
		EUPT_MOVE_ITEM_REGULAR,		///< The icon for non selected middle point of item
		EUPT_MOVE_PART_ACTIVE,		///< The icon for selected movable part of item
		EUPT_MOVE_PART_REGULAR,		///< The icon for non selected movable part of item
		EUPT_ITEM_ROTATE_ACTIVE,	///< The icon for selected rotate point of item
		EUPT_ITEM_ROTATE_REGULAR,	///< The icon for non selected rotate point of item
		EUPT_TYPES			  		///< The number of the enum's members (not to be used as a valid type)
	};

	/// 3D Edit item types used for editing operations
	enum EUtility3DEditItemType
	{
		EUEIT_MOVE_ITEM_CENTER_ACTIVE,	///< The icon for selected move item center
		EUEIT_MOVE_ITEM_CENTER_REGULAR,	///< The icon for non selected move item center
		EUEIT_MOVE_ITEM_X_ACTIVE,		///< The icon for selected move item in X-axis
		EUEIT_MOVE_ITEM_X_REGULAR,		///< The icon for non selected move item in X-axis
		EUEIT_MOVE_ITEM_Y_ACTIVE,		///< The icon for selected move item in Y-axis
		EUEIT_MOVE_ITEM_Y_REGULAR,		///< The icon for non selected move item in Y-axis
		EUEIT_MOVE_ITEM_Z_ACTIVE,		///< The icon for selected move item in Z-axis
		EUEIT_MOVE_ITEM_Z_REGULAR,		///< The icon for non selected move item in Z-axis
		EUEIT_RESIZE_ITEM_X_ACTIVE,		///< The icon for selected resize item in X-axis
		EUEIT_RESIZE_ITEM_X_REGULAR,	///< The icon for non selected resize item in X-axis
		EUEIT_RESIZE_ITEM_Y_ACTIVE,		///< The icon for selected resize item in Y-axis
		EUEIT_RESIZE_ITEM_Y_REGULAR,	///< The icon for non selected resize item in Y-axis
		EUEIT_RESIZE_ITEM_Z_ACTIVE,		///< The icon for selected resize item in Z-axis
		EUEIT_RESIZE_ITEM_Z_REGULAR,	///< The icon for non selected resize item in Z-axis
		EUEIT_ROTATE_ITEM_YAW_ACTIVE,	///< The icon for selected rotate item in yaw
		EUEIT_ROTATE_ITEM_YAW_REGULAR,	///< The icon for non selected rotate item in yaw
		EUEIT_ROTATE_ITEM_PITCH_ACTIVE,	///< The icon for selected rotate item in pitch
		EUEIT_ROTATE_ITEM_PITCH_REGULAR,///< The icon for non selected rotate item in pitch
		EUEIT_ROTATE_ITEM_ROLL_ACTIVE,	///< The icon for selected rotate item in roll
		EUEIT_ROTATE_ITEM_ROLL_REGULAR,	///< The icon for non selected rotate item in roll
		EUEIT_TYPES			  			///< The number of the enum's members (not to be used as a valid type)
	};

	/// The 3D Edit parameters, defining the edit items behavior while 3D editing
	struct S3DEditParams
	{
		bool bLocalAxes;						///< whether local (item's) axes or global (world's) ones should be used. default is true.
		bool bKeepScaleRatio;					///< whether scale ratio along different axes should be kept. default is false.
		float fUtilityItemsOptionalScreenSize;	///< utility items' optional size in screen units (pixels) or 0 (the default) if they should automatically 
												///< be resized according to edited item's world size.
	};

	/// The parameters of texts displayed by StartDistanceDirectionMeasure() for distances, heights and angles
	struct SMeasureTextParams 
	{
		/// The factor for converting values to desired units to be displayed: 
		/// units per meter for distances or units per 360 degrees for angles;
		/// zero (default) means 1.0 for distances (i.e. meters), 360 for angles (i.e. degrees)
		double				dUnitsFactor; 
		
		/// The text describing units name; empty text (default) means "m" for distances, "m height" for heights, "deg" for angles
		SMcVariantString	UnitsName;

		/// The number of digits to be disiplayed after a decimal point; default is 2
		UINT				uNumDigitsAfterDecimalPoint;

		SMeasureTextParams() 
			: dUnitsFactor(0.0), UnitsName(""), uNumDigitsAfterDecimalPoint(2) {}
	};

	/// The parameters defining the measurements of distance, height difference and direction
	struct SDistanceDirectionMeasureParams
	{
		/// Distance text parameters (see SMeasureTextParams for details) or `NULL`.
		const SMeasureTextParams* pDistanceTextParams; 
		/// Angle text parameters (see SMeasureTextParams for details) or `NULL`.
		const SMeasureTextParams* pAngleTextParams;
		/// Height text parameters (see SMeasureTextParams for details) or `NULL'.
		const SMeasureTextParams* pHeightTextParams;

		/// The item containing the properties of the text that should be drawn;
		///	if `NULL` item was passed, default item (or the item set by SetUtilityItems()) will be used.
		IMcTextItem* pText;

		/// The item containing the properties of the line that should be drawn (relevant only for StartDistanceDirectionMeasure());
		///	if `NULL` item was passed, default item (or the item set by SetUtilityItems()) will be used; 
		IMcLineItem* pLine;

		/// The optional coordinate system to return the direction (azimuth) in (used only if \p bUseMagneticAzimuth is `false`);
		///	`NULL' (the default) means the geographic azimuth, a value other than `NULL' means the coordinate system grid azimuth.
		const IMcGridCoordinateSystem *pDirectionCoordSys;

		/// Whether or not to use the magnetic azimuth; the default is `false`.
		bool bUseMagneticAzimuth;

		/// The optional date for the magnetic azimuth calculation (used only if \p bUseMagneticAzimuth is `false`); 
		///	`NULL` (the default) means the current date should be used.
		const CMcTime *pDate;

		SDistanceDirectionMeasureParams()
			: pDistanceTextParams(NULL), pAngleTextParams(NULL), pHeightTextParams(NULL),
			  pText(NULL), pLine(NULL), pDirectionCoordSys(NULL), bUseMagneticAzimuth(false), pDate(NULL) {}
	};

	/// The icons that should be hidden for one permission category
	struct SPermissionHiddenIcons
	{
		EPermission			ePermission;		///< The permission category.
		const UINT			*auIconIndices;		///< The indices of icons to hide (see SetHiddenIconsPerPermission() for details).
		UINT				uNumIconIndices;	///< The number of elements in the above array.

		SPermissionHiddenIcons() : ePermission(EEMP_NONE), auIconIndices(NULL), uNumIconIndices(0) {}
	};

	/// The position of an utility icon, identified by permission category, index and active/unactive status
	struct SIconPosition
	{
		SMcVector2D		ScreenPosition;			///< The screen position.
		EPermission		ePermission;			///< The permission category.
		UINT			uIndex;					///< The index inside the permission category (see SetHiddenIconsPerPermission() for the meaning of the indices).
		bool			bIsActive;				///< Whether it is an active icon or not.
	};

	/// The parameters (for object initialization/editing operations started by StartInitObject()/StartEditObject()) that can be changed by passing 
	/// this structure to ChangeObjectOperationsParams() (their current effective values can be retrieved by GetObjectOperationsParams()).
	/// 
	/// It is possible to pre-define these parameters per certain object type by passing this structure to IMcObjectScheme::SetEditModeParams(). 
	/// Then, before starting EditMode's operation, retrieve the paramters by IMcObjectScheme::GetEditModeParams() and give them to EditMode by 
	/// ChangeObjectOperationsParams().
	///
	/// Each parameter can have 'no-change' value mentioned below. This value means ChangeObjectOperationsParams() should not change this parameter 
	/// keeping EditMode's current value. The constructor of SObjectOperationsParams assigns 'no-change' values so that the user can touch only 
	/// the parameters that should be changed. Note: the 'no-change' value is different from the EditMode's appropriate default 
	/// in order to distinguish between forcing the EditMode's default and keeping the current value.
	struct SObjectOperationsParams
	{
		/// the bit field (based on #EPermission) defining which actions are allowed; the 'no-change' value is #EEMP_NONE.
		UINT							uPermissions;

		/// The array of permission categories with hidden icons; only permissions listed here will be changed by ChangeObjectOperationsParams(); the 'no-change' value is `NULL`.
		const SPermissionHiddenIcons	*aPermissionsWithHiddenIcons;		
																	
		/// The number of element in the above array; the 'no-change' value is 0.
		UINT							uNumPermissionsWithHiddenIcons;

		/// The array (with a length of #EUPT_TYPES or `NULL`) of picture items (per #EUtilityPictureType as an index) to be used as icons while editing objects; the 'no-change' value (**for the whole array**) is `NULL`.
		IMcPictureItem					*const *apUtilityPictures;
		
		/// The line item to be used while editing objects and manipulating the map; the 'no-change' state is indicated in the below flag (**bUtilityLineOverriden**).
		IMcLineItem						*pUtilityLine;

		/// Whether the above utility line (**pUtilityLine**) should be changed by ChangeObjectOperationsParams(); the 'no-change' value is `false`.
		bool							bUtilityLineOverriden;
		
		/// The offset of rotate icon from top-middle point of item's bounding box; the 'no-change' value is `FLT_MAX`.
		float							fRotatePictureOffset;

		/// The mouse move usage during creation of lines, polygons, arrows etc; the 'no-change' value is #EMMU_TYPES.
		EMouseMoveUsage					eMouseMoveUsageForMultiPointItem;

		/// The mouse click tolerance of icons and lines, while editing an object; the 'no-change' value is `UINT_MAX`.
		UINT							uPointAndLineClickTolerance;

		/// The maximal number of points of a line, polygon, arrow item; 0 means no limit; the 'no-change' value is `UINT_MAX`.
		UINT							uMaxNumberOfPoints;

		/// Whether or not to finish editing when reaching the maximal number of points; the 'no-change' value is no-value
		SMcNullablelBool				bForceFinishOnMaxPoints;

		/// The maximal radius of an arc, ellipse item for image coordinate system type; 0 means no limit; the 'no-change' value is `DBL_MAX`
		double							dMaxRadiusForImageCoordSys;

		/// The maximal radius of an arc, ellipse item for world coordinate system type; 0 means no limit; the 'no-change' value is `DBL_MAX`
		double							dMaxRadiusForWorldCoordSys;

		/// The maximal radius of an arc, ellipse item for screen coordinate system type; 0 means no limit; the 'no-change' value is `DBL_MAX`
		double							dMaxRadiusForScreenCoordSys;

		/// Whether resizing rectangle should be relative to its center (like ellipse); the 'no-change' value is no-value
		SMcNullablelBool				bRectangleResizeRelativeToCenter;

		/// The array (with a length of #EUEIT_TYPES or `NULL`) of utility items (per #EUtility3DEditItemType as an index) to be used in 3D editing; the 'no-change' value (**for the whole array**) is `NULL`.
		IMcObjectSchemeItem				*const *ap3DEditUtilityItems;

		/// Whether local (item's) axes or global (world's) ones should be used in 3D editing; the 'no-change' value is no-value.
		SMcNullablelBool				b3DEditLocalAxes;

		/// Whether scale ratio along different axes should be kept in 3D editing; the 'no-change' value is no-value.
		SMcNullablelBool				b3DEditKeepScaleRatio;

		/// The 3D editing utility items' optional size in screen units (pixels) if they should automatically be resized according to edited item's world size; 
		/// the 'no-change' value is `FLT_MAX`
		float							f3DEditUtilityItemsOptionalScreenSize;

		SObjectOperationsParams()
		  :	uPermissions(EEMP_NONE), aPermissionsWithHiddenIcons(NULL), uNumPermissionsWithHiddenIcons(0), 
			apUtilityPictures(NULL), pUtilityLine(NULL), bUtilityLineOverriden(false), fRotatePictureOffset(FLT_MAX), 
			eMouseMoveUsageForMultiPointItem(EMMU_TYPES), uPointAndLineClickTolerance(UINT_MAX), uMaxNumberOfPoints(UINT_MAX), 
			bForceFinishOnMaxPoints(), dMaxRadiusForImageCoordSys(DBL_MAX), dMaxRadiusForWorldCoordSys(DBL_MAX), dMaxRadiusForScreenCoordSys(DBL_MAX), 
			bRectangleResizeRelativeToCenter(), ap3DEditUtilityItems(NULL), b3DEditLocalAxes(), b3DEditKeepScaleRatio(), 
			f3DEditUtilityItemsOptionalScreenSize(FLT_MAX) {}

		bool IsDefault() const
		{
			return (this->uPermissions == EEMP_NONE && this->aPermissionsWithHiddenIcons == NULL
				&& this->uNumPermissionsWithHiddenIcons == 0 && this->apUtilityPictures == NULL
				&& this->bUtilityLineOverriden == false
				&& this->fRotatePictureOffset == FLT_MAX && this->eMouseMoveUsageForMultiPointItem == EMMU_TYPES
				&& this->uPointAndLineClickTolerance == UINT_MAX && this->uMaxNumberOfPoints == UINT_MAX
				&& this->bForceFinishOnMaxPoints == SMcNullablelBool() && this->dMaxRadiusForImageCoordSys == DBL_MAX
				&& this->dMaxRadiusForWorldCoordSys == DBL_MAX && this->dMaxRadiusForScreenCoordSys == DBL_MAX
				&& this->bRectangleResizeRelativeToCenter == SMcNullablelBool() && this->ap3DEditUtilityItems == NULL
				&& this->b3DEditLocalAxes ==  SMcNullablelBool() && this->b3DEditKeepScaleRatio == SMcNullablelBool()
				&& this->f3DEditUtilityItemsOptionalScreenSize == FLT_MAX);
		}
	};

	/// \name Create
	//@{

	//=================================================================================================
	// Function name: Create()
	//
	//-------------------------------------------------------------------------------------------------
	/// Creates EditMode instance for a specified map viewport.			
	///
	/// \param[in]	pViewport			A map viewport
	/// \param[out]	ppEditMode			The created EditMode's interface
	/// \return
	///     - status result
	//=================================================================================================
	static EDITMODE_API IMcErrors::ECode Create(IMcMapViewport *pViewport, IMcEditMode **ppEditMode);

	//@}

	/// \name Object Operations
	//@{

	//=================================================================================================
	// Function name: StartInitObject()
	//
	//-------------------------------------------------------------------------------------------------
	/// Starts an object's initialization process.
	///
	/// The initializing is done by responding to user's mouse and key events
	///
	/// \param[in]	pObject											The object to initialize
	/// \param[in]	pItem											The item to initialize the object; the default is NULL 
	///																(in this case the item defined by IMcObjectScheme::SetEditModeDefaultItem() will be used)
	/// \param[in] bEnableDistanceDirectionMeasureForMultiPointItem	If true, enable measurements of distance, height difference and direction 
	///																of the last segment of line, polygon, arrow, etc. (parameters set by 
	///																SetDistanceDirectionMeasureParams() are used).
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode StartInitObject(IMcObject *pObject, IMcObjectSchemeItem *pItem = NULL, bool bEnableDistanceDirectionMeasureForMultiPointItem = false) = 0;

	//=================================================================================================
	// Function name: StartEditObject()
	//
	//-------------------------------------------------------------------------------------------------
	/// Starts an object's editing process.
	///
	/// The editing is done by responding to user's mouse and key events
	///
	/// \param[in]	pObject										The object to edit
	/// \param[in]	pItem										The item to edit the object by; the default is NULL 
	///															(in this case the item defined by IMcObjectScheme::SetEditModeDefaultItem() will be used)
	/// \param[in]	bEnableAddingNewPointsForMultiPointItem		If true, enable adding new points for lines, polygons, arrows etc.
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode StartEditObject(IMcObject *pObject, IMcObjectSchemeItem *pItem = NULL, bool bEnableAddingNewPointsForMultiPointItem = false) = 0;

	//=================================================================================================
	// Method Name: AddOverlayManagerWorldPoint()
	//-------------------------------------------------------------------------------------------------
	/// Adds a world point to an object during an initialization process started by StartInitObject().
	///
	/// \param[in]	WorldPoint			The point to be added, in overlay manager's coordinate system
	///
	/// \note
	///		Relevant only when an initialization process started by StartInitObject() is active.
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode AddOverlayManagerWorldPoint(const SMcVector3D &WorldPoint) = 0;

	//@}

	/// \name Map Operations
	//@{

	//=================================================================================================
	// Function name: StartNavigateMap()
	//
	//-------------------------------------------------------------------------------------------------
	/// Starts a map's navigating process, enabling the user to drag and rotate the map / change the map camera.
	///
	/// Operations performed by mouse drag without Ctrl key pressed or by pressing pressing 4 predefined keyboard keys 
	/// #EKE_MOVE_LEFT, #EKE_MOVE_RIGHT, #EKE_MOVE_UP, #EKE_MOVE_DOWN (passed by OnKeyEvent()) using a predefined offset 
	/// (that can be changed to SetKeyStep()):
	/// - For 2D viewport: dragging the map (changing the camera's location across the x,y axises).
	/// - For 3D viewport: changing the yaw and pitch angles of the camera.
	///
	/// Operations performed by mouse drag with Ctrl key pressed:
	/// - For 2D viewport: rotating the map (changing the yaw angle of the camera).
	/// - For 3D viewport: changing the camera's location across the x,y axises. 
	///
	/// Operations performed by rotating mouse wheel or by pressing pressing 2 predefined keyboard keys 
	/// #EKE_MOVE_UP, #EKE_MOVE_DOWN (passed to OnKeyEvent()) using a predefined offset (that can be changed by SetKeyStep()):
	/// - For 3D: Enables changing the camera's height.
	///
	/// \param[in]	bDrawLine			Defines whether a line will be drawn on the map while rotating.
	///									Effective only in 2D.
	/// \param[in]	bOneOperationOnly	Defines whether after one operation in this navigation mode
	///									EditMode will exit the navigation mode.
	/// \param[in]  bWaitForMouseClick	Defines whether to wait for the first mouse click or to 
	///									start as though the mouse was already clicked
	/// \param[in]	MousePos			Mouse click position in window coordinates 
	///									(used when \p bWaitForMouseClick is false)
	/// \param[in]	pLine				The item containing the properties of the line that should 
	///									be drawn (in case the \p bDrawLine parameter was true).
	///									If NULL item was passed, default item (or the item set by SetUtilityItems()) will be used.
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode StartNavigateMap(bool bDrawLine,
		bool bOneOperationOnly = false,
		bool bWaitForMouseClick = true,
		SMcPoint MousePos = SMcPoint(0, 0),
		IMcLineItem *pLine = NULL) = 0;

	//=================================================================================================
	// Function name: StartDynamicZoom()
	//
	//-------------------------------------------------------------------------------------------------
	/// Starts a map's zooming process, enabling the user to change camera's visible area.
	///
	/// Operations performed by drawing a screen rectangle (by pressing mouse button, moving the mouse 
	/// and releasing the button) defining a new camera's visible area or (for 2D viewport) by rotating mouse wheel:
	/// - for 2D viewport: scrolling and changing scale (zoom)
	/// - for 3D viewport: according to \p e3DOperation (see details below)
	///
	/// \param[in]	fMinScale			The minimal scale to be used.
	/// \param[in]  bWaitForMouseClick	Defines whether to wait for the first mouse click or to 
	///									start as though the mouse was clicked
	/// \param[in]	MousePos			Mouse click position in window coordinates 
	///									(used when \p bWaitForMouseClick is false)
	/// \param[in]  pRectangle			The item containing the properties of the rectangle that should be drawn.
	///									If NULL item was passed, default item (or the item set by SetUtilityItems()) will be used.
	/// \param[in]	e3DOperation		Performed operation for 3D camera:
	///									- IMcMapCamera::ESVAO_ROTATE_AND_MOVE (default) rotates and 
	///									  moves towards the rectangle
	///									- IMcMapCamera::ESVAO_ROTATE_AND_SET_FOV rotates towards the 
	///									  rectangle and changes field of view (zoom);
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode StartDynamicZoom(float fMinScale, bool bWaitForMouseClick = true,
		SMcPoint MousePos = SMcPoint(0, 0), IMcRectangleItem* pRectangle = NULL,
		IMcMapCamera::ESetVisibleArea3DOperation e3DOperation = IMcMapCamera::ESVAO_ROTATE_AND_MOVE) = 0;

	//=================================================================================================
	// Function name: StartDistanceDirectionMeasure()
	//
	//-------------------------------------------------------------------------------------------------
	/// Performs measurements of distance, height difference and direction.
	///
	/// Performed by pressing a mouse button and moving the mouse. Parameters set by SetDistanceDirectionMeasureParams() are used.
	///
	/// \param[in]	bShowResults		Should the result be shown on the screen
	/// \param[in]	bWaitForMouseClick	Defines whether to wait for the first mouse click or to 
	///									start as though the mouse was clicked
	/// \param[in]	MousePos			Mouse click position in window coordinates
	///									(used when \p bWaitForMouseClick is `false`)
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode StartDistanceDirectionMeasure(
		bool bShowResults = true,
		bool bWaitForMouseClick = true,
		SMcPoint MousePos = SMcPoint(0, 0)) = 0;

	//=================================================================================================
	// Function name: StartCalculateHeightInImage()
	//
	//-------------------------------------------------------------------------------------------------
	/// For #IMcImageCalc-based maps only: calculates a height of an object in the image (building, tree etc.).
	///
	/// The first point should be taken from the base of the object.
	///
	/// \param[in]	pLine				The item containing the properties of the line that should be drawn.
	///									If NULL item was passed, default item (or the item set by SetUtilityItems()) will be used.
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode StartCalculateHeightInImage(IMcLineItem *pLine) = 0;

	//=================================================================================================
	// Function name: StartCalculateVolumeInImage()
	//
	//-------------------------------------------------------------------------------------------------
	/// For #IMcImageCalc-based maps only: calculates a volume of box in the image (building etc.).
	///
	/// The first point should be be taken from the base of the box and the other three from the upper surface.
	///
	/// \param[in]	pLine				The item containing the properties of the line that should be drawn.
	///									If NULL item was passed, default item (or the item set by SetUtilityItems()) will be used.
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode StartCalculateVolumeInImage(IMcLineItem*	pLine) = 0;

	//@}

	/// \name Change and Retrieve the Current Operation's Status
	//@{

	//=================================================================================================
	// Function name: ExitCurrentAction()
	//
	//-------------------------------------------------------------------------------------------------
	/// Finishes the current EditMode's operation.
	///
	/// \param[in]	bDiscard			Determines whether to discard the changes in the graphical object.  
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode ExitCurrentAction(bool bDiscard) = 0;

	//=================================================================================================
	// Function name: GetLastExitStatus()
	//
	//-------------------------------------------------------------------------------------------------
	/// Retrieves the exit status of the last operation.
	///
	/// \param[out] pnLastExitStatus	The last exit status: (1 if completed, 0 if discarded).
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetLastExitStatus(int *pnLastExitStatus) const = 0;

	//=================================================================================================
	// Function name: IsEditingActive()
	//
	//-------------------------------------------------------------------------------------------------
	/// Checkes if there is an active EditMode operation.	
	///
	/// \param[out]	bStatus				Whether or not there is an active EditMode operation.
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode IsEditingActive(bool *bStatus) const = 0;

	//@}

	/// \name Events' Callback
	//@{

	//=================================================================================================
	// Function name: SetEventsCallback()
	//
	//-------------------------------------------------------------------------------------------------
	/// Sets an events callback, allowing the application to get notifications on various EditMode's events.
	///
	/// The relevant events:
	/// - ICallback::NewVertex							(a new object's point was added)
	/// - ICallback::PointDeleted						(an existing object's point was deleted)
	/// - ICallback::PointNewPos						(an existing object's point was moved)
	/// - ICallback::ActiveIconChanged					(an existing object's active icon was changed)
	/// - ICallback::InitItemResults					(initializing-object operation results)
	/// - ICallback::EditItemResults					(editing-object operation results)
	/// - ICallback::DragMapResults						(drag map operation results)
	/// - ICallback::RotateMapResults					(rotate map operation results)
	/// - ICallback::DynamicZoomResults					(dynamic zoom operation ebded)
	/// - ICallback::DistanceDirectionMeasureResults	(distance direction measure operation results)
	/// - ICallback::CalculateHeightResults				(calculate height operation results)
	/// - ICallback::CalculateVolumeResults				(calculate volume operation results)
	/// - ICallback::ExitAction							(any EditMode operation ended)
	///
	/// \param[in]	pEventsCallback		The callback (instance of user-defined class derived from ICallback).
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetEventsCallback(ICallback *pEventsCallback) = 0;

	//=================================================================================================
	// Function name: GetEventsCallback()
	//
	//-------------------------------------------------------------------------------------------------
	/// Retrieve the events callback set by SetEventsCallback().
	///
	/// \param[out] ppaEventsCallback	The callback (instance of user-defined class derived from ICallback).
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetEventsCallback(ICallback **ppaEventsCallback) const = 0;

	//@}

	/// \name Mouse and Key Events
	//@{

	//=================================================================================================
	// Function name: OnMouseEvent()
	//
	//-------------------------------------------------------------------------------------------------
	/// Should be called by the application to pass the operating system's mouse events to EditMode.
	///
	/// \param[in]	eEvent					The event to respond to, based on #EMouseEvent
	/// \param[in]	MousePosition			The mouse position at the time of the event
	/// \param[in]	bControlKeyDown			Whether or not the Ctrl key is down at the time of the event
	/// \param[in]	nWheelDelta				The wheel delta at the time of the event
	/// \param[out]	pbRenderNeeded			Whether or not EditMode handled the event and changed something, 
	///										so that the application should call Render() to refresh the viewport
	/// \param[out]	peCursorType			The recommended cursor type to which the application should set afterwards
	/// \param[in]	pSecondTouchPosition	For touch screen: the second finger position at the time of the event
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode OnMouseEvent(
		EMouseEvent eEvent, const SMcPoint &MousePosition,
		bool bControlKeyDown, short nWheelDelta,
		bool *pbRenderNeeded, ECursorType *peCursorType, SMcPoint *pSecondTouchPosition = NULL) = 0;

	//=================================================================================================
	// Function name: OnKeyEvent()
	//
	//-------------------------------------------------------------------------------------------------
	/// Should be called by the application to pass the operating system's key events to EditMode.
	///
	/// \param[in]	eEvent				The event to respond to, based on #EKeyEvent
	/// \param[out]	pbRenderNeeded		Whether or not EditMode handled the event and changed something, 
	///									so that the application should call Render() to refresh the viewport
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode OnKeyEvent(
		EKeyEvent eEvent, bool *pbRenderNeeded) = 0;

	//==============================================================================
	// Method Name: SetKeyStep()
	//------------------------------------------------------------------------------
	/// Sets a step value of the specified type, used in object's editing and map's navigation by keys
	///
	/// \param[in]	eStepType			Step type (see #EKeyStepType)
	/// \param[in]	fStep				Step value in units depending on the type
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetKeyStep(EKeyStepType eStepType, float fStep) = 0;

	//==============================================================================
	// Method Name: GetKeyStep()
	//------------------------------------------------------------------------------
	/// Retrieves a step value of the specified type, used in object's editing and map's navigation by keys
	///
	/// \param[in]	eStepType			Step type (see #EKeyStepType)
	/// \param[out]	pfStep				Step value in units depending on the type
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetKeyStep(EKeyStepType eStepType, float *pfStep) const = 0;

	//@}

	/// \name Auto Scroll
	//@{

	//=================================================================================================
	// Function name: AutoScroll()
	//
	//-------------------------------------------------------------------------------------------------
	/// Activates/deactivates auto scroll mode. If set, the map will scroll when the cursor will enter 
	/// the viewport's margins defined by \p nMarginsSize.	
	///
	/// \param[in]	bAutoScroll			Whether to activate or deactivate the auto scroll mode.
	/// \param[in]	nMarginsSize		The margins size in pixels.
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode AutoScroll(bool bAutoScroll, int nMarginsSize) = 0;

	//=================================================================================================
	// Function name: GetAutoScrollMode()
	//
	//-------------------------------------------------------------------------------------------------
	/// Retrieves the auto scroll mode.
	///
	/// \param[out] pbAutoScrollMode	Whether or not the auto scroll mode is activated.
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetAutoScrollMode(bool *pbAutoScrollMode) const = 0;

	//=================================================================================================
	// Function name: GetMarginSize()
	//
	//-------------------------------------------------------------------------------------------------
	/// Retrieves the auto scroll margin's size in pixels.
	///
	/// \param[out] pnMarginSize		The margin's size in pixels.
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetMarginSize(int *pnMarginSize) const = 0;

	//@}

	/// \name Utility Items
	//@{

	//=================================================================================================
	// Function name: SetUtilityItems()
	//
	//-------------------------------------------------------------------------------------------------
	/// Sets items to be used while editing objects and manipulating the map.
	///
	/// If SetUtilityItems() was not called, or if NULL item was passed, default item will be used.
	/// The items passed should not belong to any scheme.
	///
	/// \param[in]	pRectangle			Rectangle item to be used (or NULL) 
	/// \param[in]	pLine				Line item to be used (or NULL)
	/// \param[in]	pText				Text item to be used (or NULL)
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetUtilityItems(
		IMcRectangleItem*	pRectangle,
		IMcLineItem*		pLine, 
		IMcTextItem*		pText) = 0;

	//=================================================================================================
	// Function name: GetUtilityItems()
	//
	//-------------------------------------------------------------------------------------------------
	/// Retrieves items to be used in EditMode's utilities (such as dynamic zoom, distance direction measure, etc.).
	///
	/// If the received item is NULL, it means that default item is used.
	///
	/// \param[out]	pRectangle			Rectangle item to be used 
	/// \param[out]	pLine				Line item to be used 
	/// \param[out]	pText				Text item to be used 
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetUtilityItems(
		IMcRectangleItem**	pRectangle,
		IMcLineItem**		pLine,
		IMcTextItem**		pText) const = 0;

	//=================================================================================================
	// Function name: SetUtilityPicture()
	//
	//-------------------------------------------------------------------------------------------------
	/// Sets the picture item to be used as an icon while editing. 
	///
	/// If SetUtilityPicture() was not called, or if NULL item was passed, default item will be used.
	/// The item passed should not belong to any scheme.
	///
	/// \param[in]	pIcon				Picture item to be used (or NULL)
	/// \param[in]	eType				Editing icon type (see #EUtilityPictureType)
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetUtilityPicture(
		IMcPictureItem* pIcon,
		EUtilityPictureType eType) = 0;

	//=================================================================================================
	// Function name: GetUtilityPicture()
	//
	//-------------------------------------------------------------------------------------------------
	/// Retrieves the picture item to be used as an icon while editing. 
	///
	/// If the received picture item is NULL, it means that default item is used.
	///
	/// \param[in]	eType				Editing icon type (see #EUtilityPictureType)
	/// \param[out]	ppIcon				Picture item to be used
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetUtilityPicture(
		EUtilityPictureType eType,
		IMcPictureItem** ppIcon) const = 0;

	//==============================================================================
	// Method Name: SetRotatePictureOffset()
	//------------------------------------------------------------------------------
	/// Sets offset of rotate icon from top-middle point of item's bounding box
	///
	/// \param[in]	fOffset				The offset; 0 means using MapCore's default offset calculation.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetRotatePictureOffset(float fOffset) = 0;

	//==============================================================================
	// Method Name: GetRotatePictureOffset()
	//------------------------------------------------------------------------------
	/// Retrieves offset of rotate icon from top-middle point of item's bounding box
	///
	/// \param[in]	pfOffset			The offset; 0 means using MapCore's default offset calculation.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRotatePictureOffset(float *pfOffset) const = 0;

	//=================================================================================================
	// Function name: SetUtility3DEditItem()
	//-------------------------------------------------------------------------------------------------
	/// Sets the item to be used in 3D editing. 
	///
	/// If SetUtility3DEditItem() was not called, or if NULL item was passed, default item will be used.
	/// The item passed should not belong to any scheme.
	///
	/// \param[in]	pEditItem			3D Edit item to be used (or NULL)
	/// \param[in]	eType				3D Edit item type (see #EUtility3DEditItemType)
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetUtility3DEditItem(
		IMcObjectSchemeItem* pEditItem, 
		EUtility3DEditItemType eType) = 0;

	//=================================================================================================
	// Function name: GetUtility3DEditItem()
	//
	//-------------------------------------------------------------------------------------------------
	/// Retrieves the item to be used in 3D editing. 
	///
	/// If the received 3D edit item is NULL, it means that default item is used.
	///
	/// \param[in]	eType				3D Edit item type (see #EUtility3DEditItemType)
	/// \param[out]	ppEditItem			3D Edit item to be used
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetUtility3DEditItem(
		EUtility3DEditItemType eType,
		IMcObjectSchemeItem** ppEditItem) const = 0;

	//==============================================================================
	// Method Name: Set3DEditParams()
	//------------------------------------------------------------------------------
	/// Sets parameters defining the edit items behavior while 3D editing.
	///
	/// \param[in]	Params				The parameters to set (see S3DEditParams)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Set3DEditParams(const S3DEditParams &Params) = 0;

	//==============================================================================
	// Method Name: Get3DEditParams()
	//------------------------------------------------------------------------------
	/// Retrieves parameters defining the edit items behavior while 3D editing.
	///
	/// \param[out]	pParams				The parameters retrieved (see S3DEditParams)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Get3DEditParams(S3DEditParams *pParams) const = 0;

	//@}

	/// \name Permissions and Icons
	//@{

	//=================================================================================================
	// Function name: SetPermissions()
	//
	//-------------------------------------------------------------------------------------------------
	/// Sets the bit field defining which editing actions are allowed
	///
	/// \param[in]	uPermissionsBitField	Permission bit field based on #EPermission
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetPermissions(UINT uPermissionsBitField) = 0;

	//=================================================================================================
	// Function name: GetPermissions()
	//
	//-------------------------------------------------------------------------------------------------
	/// Retrieves the bit field defining which editing actions are allowed
	///
	/// \param[out]	puPermissionsBitField	Permission bit field based on #EPermission
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetPermissions(UINT *puPermissionsBitField) const = 0;

	//==============================================================================
	// Method Name: SetHiddenIconsPerPermission()
	//------------------------------------------------------------------------------
	/// Sets which icons should be hidden for the specified permission category of #EPermission.
	///
	/// The relevant permission categories and according values of indices:
	/// - #EEMP_MOVE_VERTEX:the indices are according to the order of vertices starting with zero
	/// - #EEMP_BREAK_EDGE:	the indices are according to the order of edges starting with zero
	/// - #EEMP_RESIZE (for physical): the indices are 0 (scale x), 1 (scale y), 2 (scale z)
	/// - #EEMP_RESIZE (for arrow):	the indices are 0 (tail-left), 1 (head-left), 2 (head-right), 3 (tail-right)
	/// - #EEMP_RESIZE (for the rest): the indices are 0 (top-left), 1 (top-middle), 2 (top-right), 3 (middle-right), 
	///						4 (bottom-right), 5 (bottom-middle), 6 (bottom-left), 7 (middle-left), 
	///						9 (circle arc middle), 10 (ellipse/arc start angle), 11 (ellipse/arc end angle), 
	///						12 (ellipse inner radius)
	/// - #EEMP_ROTATE: the indices are 0 (yaw), 1 (pitch), 2 (roll)
	/// - #EEMP_DRAG: the indices are 0 (center), 1 (x-axis), 2 (y-axis), 3 (z-axis)
	///
	/// \param[in]	ePermission			Permission category
	/// \param[in]	auIconIndices[]		Indices of icons to hide
	/// \param[in]	uNumIconIndices		Number of indices of icons to hide
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetHiddenIconsPerPermission(EPermission ePermission,
		const UINT auIconIndices[], UINT uNumIconIndices) = 0;

	//==============================================================================
	// Method Name: GetHiddenIconsPerPermission()
	//------------------------------------------------------------------------------
	/// Retrieves icons previously hidden by SetHiddenIconsPerPermission() for the specified permission category of #EPermission.
	///
	/// \param[in]	ePermission			Permission category
	/// \param[out]	pauIconIndices		Indices of icons hidden (see SetHiddenIconsPerPermission() for the meaning of the indices)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetHiddenIconsPerPermission(EPermission ePermission,
		CMcDataArray<UINT> *pauIconIndices) const = 0;

	//==============================================================================
	// Method Name: GetIconsScreenPositions()
	//------------------------------------------------------------------------------
	/// Retrieves the screen positions of currently visible utility icons
	///
	/// \param[out]	paIconPositions		screen positions of currently visible utility icons (see #SIconPosition for details)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetIconsScreenPositions(CMcDataArray<SIconPosition>* paIconPositions) const = 0;

	//@}

	/// \name Behavior When Initializing and Editing Objects
	//@{

	//=================================================================================================
	// Method Name: SetMouseMoveUsageForMultiPointItem()
	//-------------------------------------------------------------------------------------------------
	/// Sets mouse move usage during creation of lines, polygons, arrows etc.
	///
	/// \param[in]	eMouseMoveUsage		Mouse move usage (see #EMouseMoveUsage for details)
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetMouseMoveUsageForMultiPointItem(EMouseMoveUsage eMouseMoveUsage) = 0;

	//=================================================================================================
	// Method Name: GetMouseMoveUsageForMultiPointItem()
	//-------------------------------------------------------------------------------------------------
	/// Retrieves mouse move usage during creation of lines, polygons, arrows etc.
	///
	/// \param[out]	peMouseMoveUsage	Mouse move usage (see #EMouseMoveUsage for details)
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetMouseMoveUsageForMultiPointItem(EMouseMoveUsage *peMouseMoveUsage) const = 0;

	//=================================================================================================
	// Method Name: SetPointAndLineClickTolerance()
	//-------------------------------------------------------------------------------------------------
	/// Sets mouse click tolerance of icons and lines, while editing an object.
	///
	/// The tolerance determines how far from the icon/line, a user can click and it will still be considered as selected.
	///
	/// \param[in]	uTolerance			The tolerance in pixels
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetPointAndLineClickTolerance(UINT uTolerance) = 0;

	//=================================================================================================
	// Method Name: GetPointAndLineClickTolerance()
	//-------------------------------------------------------------------------------------------------
	// Retrieves mouse click tolerance of icons and lines, while editing an object
	///
	/// The tolerance determines how far from the icon/line, a user can click and it will still be considered as selected.
	///
	/// \param[out]	puTolerance			The tolerance in pixels
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetPointAndLineClickTolerance(UINT *puTolerance) const = 0;

	//=================================================================================================
	// Function name: SetMaxNumberOfPoints()
	//
	//-------------------------------------------------------------------------------------------------
	/// Sets the maximal number of points of a line, polygon, arrow item.
	///
	/// If the number set by IMcObjectLocation::SetMaxNumPoints() defines a smaller limit, that limit will be taken into consideration.
	///
	/// \param[in]	uMaxNumberOfPoints			Maximal number of points; 0 means no limit
	/// \param[in]	bForceFinishOnMaxPoints		Whether or not to finish editing when reaching the maximal number of points
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetMaxNumberOfPoints(UINT uMaxNumberOfPoints, bool bForceFinishOnMaxPoints) = 0;

	//=================================================================================================
	// Function name: GetMaxNumberOfPoints()
	//
	//-------------------------------------------------------------------------------------------------
	/// Retrieves the maximal number of points of a line, polygon, arrow item.
	///
	/// \param[out]	puMaxNumberOfPoints			Maximal number of points; 0 means no limit
	/// \param[out]	pbForceFinishOnMaxPoints	Whether or not to finish editing when reaching the maximal number of points
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetMaxNumberOfPoints(UINT *puMaxNumberOfPoints, bool *pbForceFinishOnMaxPoints) const = 0;

	//=================================================================================================
	// Function name: SetMaxRadius()
	//
	//-------------------------------------------------------------------------------------------------
	/// Sets the maximal radius of an arc, ellipse item for the specified coordinate system type.
	///
	/// \param[in]	dMaxRadius			Maximal radius value; 0 means no limit
	/// \param[in]	eCoordSystem		Item's coordinate system to set the value for
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetMaxRadius(double dMaxRadius, EMcPointCoordSystem eCoordSystem) = 0;

	//=================================================================================================
	// Function name: GetMaxRadius()
	//
	//-------------------------------------------------------------------------------------------------
	/// Retrieves the maximal radius of an arc,ellipse item for the specified coordinate system type.
	///
	/// \param[out]	pdMaxRadius			Maximal radius value; 0 means no limit
	/// \param[in]	eCoordSystem		Item's coordinate system to retrieve the value for
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetMaxRadius(double* pdMaxRadius, EMcPointCoordSystem eCoordSystem) const = 0;

	//=================================================================================================
	// Method Name: SetRectangleResizeRelativeToCenter()
	//-------------------------------------------------------------------------------------------------
	/// Sets whether resizing rectangle should be relative to its center (like ellipse)
	///
	/// \param[in]	bRelativeToCenter	Whether resizing rectangle should be relative to its center
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetRectangleResizeRelativeToCenter(bool bRelativeToCenter) = 0;

	//=================================================================================================
	// Method Name: GetRectangleResizeRelativeToCenter()
	//-------------------------------------------------------------------------------------------------
	/// Retrieves whether resizing rectangle is relative to its center (like ellipse)
	///
	/// \param[out]	pbRelativeToCenter	Whether resizing rectangle is relative to its center
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetRectangleResizeRelativeToCenter(bool *pbRelativeToCenter) const = 0;

	//@}

	/// \name General Parameters
	//@{

	//=================================================================================================
	// Function name: SetCameraPitchRange()
	//
	//-------------------------------------------------------------------------------------------------
	/// Sets the 3D camera pitch range (the whole range is from -180 to 180)
	///
	/// \param[in]	dMinPitch			Minimum pitch (the default value is -180)
	/// \param[in]	dMaxPitch			Maximum pitch (the default value is 180)
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetCameraPitchRange(double dMinPitch, double dMaxPitch) = 0;

	//=================================================================================================
	// Function name: GetCameraPitchRange()
	//
	//-------------------------------------------------------------------------------------------------
	/// Retrieves the 3D camera pitch range (the whole range is from -180 to 180)
	///
	/// \param[out] pdMinPitch			Minimum pitch (the default value is -180)
	/// \param[out] pdMaxPitch			Maximum pitch (the default value is 180)
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetCameraPitchRange(double* pdMinPitch, double* pdMaxPitch) const = 0;

	//=================================================================================================
	// Function name: SetIntersectionTargets()
	//
	//-------------------------------------------------------------------------------------------------
	/// Sets possible targets for intersection queries; the default is DTM layer and static-objects layer.
	///
	/// \param[in]	uTargetsBitMask		Targets bit mask, see IMcSpatialQueries::EIntersectionTargetType
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetIntersectionTargets(UINT uTargetsBitMask) = 0;

	//=================================================================================================
	// Function name: GetIntersectionTargets()
	//
	//-------------------------------------------------------------------------------------------------
	/// Retrieves the possible targets for intersection queries. default is DTM layer and static-objects map layer.
	///
	/// \param[out]	puTargetsBitMask	Targets bit mask, see IMcSpatialQueries::EIntersectionTargetType
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetIntersectionTargets(UINT *puTargetsBitMask) const = 0;

	//==============================================================================
	// Method Name: SetAutoSuppressQueryPresentationMapTilesWebRequests()
	//------------------------------------------------------------------------------
	/// Sets a mode of automatically suppressing requests of tiles of web-based DTM/static-objects layers for calculation of 
	/// query-presentation items during initialization/editing of object
	///
	/// \param[in]	bSuppress			Whether or not the web requests should be automatically suppressed; the default is false
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetAutoSuppressQueryPresentationMapTilesWebRequests(bool bSuppress) = 0;

	//==============================================================================
	// Method Name: GetAutoSuppressQueryPresentationMapTilesWebRequests()
	//------------------------------------------------------------------------------
	/// Retrieves a mode of automatically suppressing requests of tiles of web-based DTM/static-objects layers for calculation of 
	/// query-presentation items during initialization/editing of object
	///
	/// \param[out]	pbSuppress			Whether or not the web requests are automatically suppressed; the default is false
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAutoSuppressQueryPresentationMapTilesWebRequests(bool *pbSuppress) const = 0;

	//==============================================================================
	// Method Name: ChangeObjectOperationsParams()
	//------------------------------------------------------------------------------
	/// Changes the parameters for object initialization/editing operations.
	///
	/// \param[in]	Params					The new parameters for object initialization/editing operations; only parameters with values other than 
	///										'no-change' will be changed (see SObjectOperationsParams for details).
	/// \param[in]	bForOneOperationOnly	Whether the new parameters should be effective during the next initialization/editing operation only and 
	///										will be restored to their current values when the operation has finished.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ChangeObjectOperationsParams(const SObjectOperationsParams &Params, bool bForOneOperationOnly = true) = 0;

	//==============================================================================
	// Method Name: GetObjectOperationsParams()
	//------------------------------------------------------------------------------
	/// Retrieves the current effective parameters for object initialization/editing operations set by EditMode's SetXXX() functions and possibly 
	/// changed by ChangeObjectOperationsParams().
	///
	/// \param[out]	pParams		The current effective parameters for object initialization/editing operations.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectOperationsParams(SObjectOperationsParams *pParams) const = 0;

	//==============================================================================
	// Method Name: SetAutoChangeObjectOperationsParams()
	//------------------------------------------------------------------------------
	/// Sets a mode of automatically changing object operations parameters during object initialization/editing according to the parameters defined in its scheme
	///
	/// \param[in]   bChange         Whether or not object operations parameters should be automatically changed; the default is true
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetAutoChangeObjectOperationsParams(bool bChange) = 0;

	//==============================================================================
	// Method Name: GetAutoChangeObjectOperationsParams()
	//------------------------------------------------------------------------------
	/// Retrieves a mode of automatically changing object operations parameters during object initialization/editing according to the parameters defined in its scheme
	///
	/// \param[out]  pbChange        Whether or not object operations parameters are automatically changed; the default is true
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAutoChangeObjectOperationsParams(bool *pbChange) = 0;

	//=================================================================================================
	// Function name: SetDistanceDirectionMeasureParams()
	//
	//-------------------------------------------------------------------------------------------------
	/// Sets parameters defining the measurements of distance, height difference and direction.
	///
	/// The EditMode's default settings are default SMeasureTextParams for DistanceTextParams and AngleTextParams
	/// and no HeightTextParams; Default text and line utility items; false UseMagneticAzimuth; NULL DirectionCoordSys (meaning geographic azimuth);
	/// NULL Date (meaning current date)
	///
	/// \param[in]	Params				The parameters to set (see SDistanceDirectionMeasureParams)
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetDistanceDirectionMeasureParams(
		const SDistanceDirectionMeasureParams &Params) = 0;

	//=================================================================================================
	// Method Name: GetDistanceDirectionMeasureParams()
	//-------------------------------------------------------------------------------------------------
	/// Retrieves parameters defining the measurements of distance, height difference and direction.
	///
	/// \param[out]	pParams				The parameters retrieved (see SDistanceDirectionMeasureParams)
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetDistanceDirectionMeasureParams(
		SDistanceDirectionMeasureParams *pParams) const = 0;

	//@}

	//===========================================================================
	// Interface Name: ICallback
	//---------------------------------------------------------------------------
	/// EditMode's callback interface to be implemented by the user to receive EditMode's events
	///
	/// \note The last event of every EditMode operation is always ExitAction(). Starting a new EditMode operation is not 
	/// allowed from inside any callback except for ExitAction().
	//===========================================================================
	class ICallback
#ifdef __EMSCRIPTEN__
		: public IMcEmscriptenCallbackRef
#endif
	{
	public:
		virtual ~ICallback() {}

		//==============================================================================
		// Method Name: NewVertex()
		//------------------------------------------------------------------------------
		/// Called when a new point added to the object initialized/edited
		///
		/// \param[in]	pObject			The object that was initialized/edited
		/// \param[in]	pItem			The item that was initialized/edited
		/// \param[in]  WorldVertex		The new point in world coordinate system
		/// \param[in]  ScreenVertex	The new point in screen coordinate system
		/// \param[in]  uVertexIndex	The index of the new point
		/// \param[in]  dAngle			No longer in use
		//==============================================================================
		virtual void NewVertex(IMcObject *pObject, IMcObjectSchemeItem *pItem, 
							   const SMcVector3D&	WorldVertex, 
							   const SMcVector3D&	ScreenVertex, 
							   UINT					uVertexIndex, 
							   double				dAngle) {}

		//==============================================================================
		// Method Name: PointDeleted()
		//------------------------------------------------------------------------------
		/// Called when a point deleted from the object initialized/edited
		///
		/// \param[in]	pObject			The object that was initialized/edited
		/// \param[in]	pItem			The item that was initialized/edited
		/// \param[in]  WorldVertex		The deleted point in world coordinates system
		/// \param[in]  ScreenVertex	The deleted point in screen coordinates system
		/// \param[in]  uVertexIndex	The index of the deleted point
		//==============================================================================
		virtual void PointDeleted(IMcObject *pObject, IMcObjectSchemeItem *pItem, 
								  const SMcVector3D&	WorldVertex, 
								  const SMcVector3D&	ScreenVertex, 
								  UINT					uVertexIndex) {}

		//==============================================================================
		// Method Name: PointNewPos()
		//------------------------------------------------------------------------------
		/// Called when a point moved in the object initialized/edited
		///
		/// \param[in]	pObject				The object that was initialized/edited
		/// \param[in]	pItem				The item that was initialized/edited
		/// \param[in]  WorldVertex			The moved point in world coordinates system
		/// \param[in]  ScreenVertex		The moved point in screen coordinates system
		/// \param[in]  uVertexIndex		The index of the moved point
		/// \param[in]  dAngle				No longer in use
		/// \param[in]  bDownOnHeadPoint	No longer in use
		//==============================================================================
		virtual void PointNewPos(IMcObject *pObject, IMcObjectSchemeItem *pItem, 
								 const SMcVector3D&	WorldVertex, 
								 const SMcVector3D&	ScreenVertex, 
								 UINT				uVertexIndex, 
								 double				dAngle, 
								 bool				bDownOnHeadPoint) {}

		//==============================================================================
		// Method Name: ActiveIconChanged()
		//------------------------------------------------------------------------------
		/// Called when an active icon was changed in the object initialized/edited
		///
		/// \param[in]	pObject				The object that was initialized/edited
		/// \param[in]	pItem				The item that was initialized/edited
		/// \param[in]  eIconPermission		The icon's category of #EPermission
		/// \param[in]  uIconIndex			The icon's index (see SetHiddenIconsPerPermission() for details)
		//==============================================================================
		virtual void ActiveIconChanged(IMcObject *pObject, IMcObjectSchemeItem *pItem,
								 EPermission eIconPermission, UINT uIconIndex) {}

		//==============================================================================
		// Method Name: InitItemResults()
		//------------------------------------------------------------------------------
		/// Called when the initializing object operation (started by StartInitObject()) is finished
		///
		/// \param[in]	pObject			The object that was initialized
		/// \param[in]	pItem			The item that was initialized
		/// \param[in]  nExitCode		0 when the initialization was aborted, 1 when it succeeded
		//==============================================================================
		virtual void InitItemResults(IMcObject *pObject, IMcObjectSchemeItem *pItem, int nExitCode)	{}

		//==============================================================================
		// Method Name: EditItemResults()
		//------------------------------------------------------------------------------
		/// Called when the editing object operation (started by StartEditObject()) is finished
		///
		/// \param[in]	pObject			The object that was edited
		/// \param[in]	pItem			The item that was edited
		/// \param[in]  nExitCode		0 when the editing was aborted, 1 when it succeeded
		//==============================================================================
		virtual void EditItemResults(IMcObject *pObject, IMcObjectSchemeItem *pItem, int nExitCode)	{}

		//==============================================================================
		// Method Name: DragMapResults()
		//------------------------------------------------------------------------------
		/// Called when the dragging map operation (started by StartNavigateMap()) is finished
		///
		/// \param[in]	pViewport		Map viewport
		/// \param[in]	NewCenter		Map camera's new position
		//==============================================================================
		virtual void DragMapResults(IMcMapViewport* pViewport, const SMcVector3D& NewCenter) {}

		//==============================================================================
		// Method Name: RotateMapResults()
		//------------------------------------------------------------------------------
		/// Called when the rotating map operation (started by StartNavigateMap()) is finished
		///
		/// \param[in]	pViewport		Map viewport
		/// \param[in]  fNewYaw			Map camera's new yaw
		/// \param[in]	fNewPitch		Map camera's new pitch (relevant for 3D only)
		//==============================================================================
		virtual void RotateMapResults(IMcMapViewport* pViewport, float fNewYaw, float fNewPitch) {}

		//==============================================================================
		// Method Name: DynamicZoomResults()
		//------------------------------------------------------------------------------
		/// Called when the changing zoom operation (started by StartDynamicZoom()) is finished
		///
		/// \param[in]	pViewport		Map viewport
		/// \param[in]	fNewScale		Map camera's new scale
		/// \param[in]	NewCenter		Map camera's new position
		//==============================================================================
		virtual void DynamicZoomResults(IMcMapViewport* pViewport, float fNewScale, const SMcVector3D& NewCenter) {}

		//==============================================================================
		// Method Name: DistanceDirectionMeasureResults()
		//------------------------------------------------------------------------------
		/// Called when the distance direction measure operation (started by StartDistanceDirectionMeasure()) is finished
		///
		/// \param[in]	pViewport		Map viewport
		/// \param[in]  WorldVertex1	The first point in world coordinates
		/// \param[in]  WorldVertex2	The second point in world coordinates
		/// \param[in]  dDistance		The measured distance (according to the unit defined in SMeasureTextParams)
		/// \param[in]  dAngle			The measured angle (according to the unit defined in SMeasureTextParams)
		//==============================================================================
		virtual void DistanceDirectionMeasureResults(IMcMapViewport*	pViewport,
			const SMcVector3D&	WorldVertex1,
			const SMcVector3D&	WorldVertex2,
			double				dDistance,
			double				dAngle)	{}

		//==============================================================================
		// Method Name: CalculateHeightResults()
		//------------------------------------------------------------------------------
		/// Called when the calculate height operation (started by StartCalculateHeightInImage()) is finished
		///
		/// \param[in]	pViewport		Map viewport
		/// \param[in]  dHeight			The calculated height
		/// \param[in]	aCoords			The coordinates of points selected for the measurement in image coordinate system
		/// \param[in]	nStatus			The status, based on #IMcErrors
		//==============================================================================
		virtual void CalculateHeightResults(IMcMapViewport* pViewport, double dHeight, const SMcVector3D aCoords[2], int nStatus) {}

		//==============================================================================
		// Method Name: CalculateVolumeResults()
		//------------------------------------------------------------------------------
		/// Called when the calculate volume operation (started by StartCalculateVolumeInImage()) is finished
		///
		/// \param[in]	pViewport		Map viewport
		/// \param[in]  dVolume			The calculated volume
		/// \param[in]	aCoords			The coordinates of points selected for the measurement in image coordinate system
		/// \param[in]	nStatus			The status, based on #IMcErrors
		//==============================================================================
		virtual void CalculateVolumeResults(IMcMapViewport* pViewport, double dVolume, const SMcVector3D aCoords[4], int nStatus) {}

		//==============================================================================
		// Method Name: ExitAction()
		//------------------------------------------------------------------------------
		/// Called after any EditMode operation is finished
		///
		/// EditMode operation is finished when either:
		/// - ExitCurrentAction() is called,
		/// - #EKE_CONFIRM/#EKE_ABORT key events are received,
		/// - #EME_BUTTON_DOUBLE_CLICK mouse event is received
		///
		/// \param[in]  nExitCode		0 when the operation was aborted, 1 when it succeeded
		//==============================================================================
		virtual void ExitAction(int nExitCode) {}

		//==============================================================================
		// Method Name: Release()
		//------------------------------------------------------------------------------
		/// A callback that should release callback class instance.
		///
		///	Can be implemented by the user to optionally delete callback class instance when IMcMapEditMode instance is been removed.
		//==============================================================================
		virtual void Release() {}
	};
};
