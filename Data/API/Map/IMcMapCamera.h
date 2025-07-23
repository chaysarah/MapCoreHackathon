#pragma once

//===========================================================================
/// \file IMcMapCamera.h
/// Interface for map camera
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcBase.h"
#include "SMcVector.h"
#include "Calculations/IMcSpatialQueries.h"

//===========================================================================
// Interface Name: IMcMapCamera
//---------------------------------------------------------------------------
/// \interface IMcMapCamera
/// Interface for operating viewport camera (mainly used for map navigation).
///
/// The IMcMapCamera is a virtual camera which is used as an observer. It is located somewhere above the terrain
/// and can be rotated, tilted, zoomed and much more. The camera's frame content is the actual footage displayed by the
/// viewport to user. 
/// The IMcMapCamera interface enables the following operations:
/// - Change the observation position
///   + To absolute position
///   + To a vector originated in previous position
/// - Change the observed position
///   + By tilting the camera using yaw, pitch and roll.
///   + By set an observation point on the ground.
/// - Rotate the camera around a position
/// - Change the camera's field of view to zoom-in/out
/// - Control the camera footprint and visibility issues.
/// - Get information about all camera's properties.
/// - And much more
//===========================================================================
class IMcMapCamera : virtual public IMcBase
{
protected:
    virtual ~IMcMapCamera() {}

public:

	/// Map type
	enum EMapType
	{
		EMT_2D,	///< 2D map (either regular 2D map or diagonal image 2D map based on camera model)
		EMT_3D	///< 3D map
	};

	/// Camera matrix type
	enum ECameraMatrixType
	{
		ECMT_VIEW,			///< view matrix in graphics coordinate system (X - right, Y - up, Z - backward)
		ECMT_PROJECTION,	///< projection matrix
		ECMT_VIEWPORT		///< viewport matrix
	};

	/// Performed operation for changing 3D camera's visible area to the specified rectangle
	/// (used in SetCameraScreenVisibleArea() for 3D camera)
	enum ESetVisibleArea3DOperation
	{
		ESVAO_ROTATE_AND_MOVE,		///< rotate and move towards the rectangle
		ESVAO_ROTATE_AND_SET_FOV	///< rotate towards the rectangle and change field of view (zoom)
	};

	/// \struct SCameraFootprintPoints
	/// Camera footprint on terrain
	///
	/// Contains intersections of 4 camera corner rays and center ray with the terrain 
	/// along with flags indicating if each appropriate point is found and valid
	/// (otherwise it is undefined).
	struct SCameraFootprintPoints
	{
		SMcVector3D	UpperLeft;			///< upper-left corner footprint
		SMcVector3D	UpperRight;			///< upper-right corner footprint
		SMcVector3D	LowerRight;			///< lower-right corner footprint
		SMcVector3D	LowerLeft;			///< lower-left corner footprint
		SMcVector3D	Center;				///< camera center footprint
		bool		bUpperLeftFound;	///< whether upper-left corner footprint is found
		bool		bUpperRightFound;	///< whether upper-right corner footprint is found
		bool		bLowerRightFound;	///< whether lower-right corner footprint is found
		bool		bLowerLeftFound;	///< whether lower-left corner footprint is found
		bool		bCenterFound;		///< whether camera center footprint is found
	};

	/// \struct SCameraAttachmentTarget
	/// Parameters of camera attachment target
	///
	/// \note
	///		Default constructor initializes with zeros; only non-zero values should be set.
	struct SCameraAttachmentTarget
	{
		/// the object to attach camera to 
		/// (camera is attached to its scheme item specified or to its default item)
		IMcObject						*pObject;

		/// the object's scheme item to attach camera to (NULL means the default item, which is the first item of the first location)
		IMcObjectSchemeItem				*pItem;

		/// defines to which point of node camera should be attached: 
		/// - when attaching to a symbolic item: defines index of its point 
		///   or #MC_EMPTY_ID to use the first one;
		/// - when attaching to a mesh item: defines its attach point ID 
		///   or #MC_EMPTY_ID to use mesh itself;
		/// - when attaching to a physical item other than mesh item: unused.
		UINT							uAttachPoint; 

		/// Default constructor: initializes with zeros plus empty attach point
		SCameraAttachmentTarget() { memset(this, 0, sizeof(*this)); uAttachPoint = MC_EMPTY_ID; }

		/// Constructor for attaching to object's scheme item
		SCameraAttachmentTarget(IMcObject *_pObject, IMcObjectSchemeItem *_pItem = NULL, 
								UINT _uAttachPoint = MC_EMPTY_ID)
		  :	pObject(_pObject), pItem(_pItem), uAttachPoint(_uAttachPoint) {}
	};

	/// \struct SCameraAttachmentParams
	/// Parameters of attachment of camera position and (optionally) orientation 
	///
	/// \note
	///		Default constructor initializes with zeros; only non-zero values should be set.
	struct SCameraAttachmentParams : public SCameraAttachmentTarget
	{
		/// offset from the target
		SMcVector3D						Offset;

		/// whether camera orientation is attached to the same target
		/// (used only if look-at attachment is not defined in SetCameraAttachment()):
		///	- when attaching to an item: its rotation is used;
		bool							bAttachOrientation;

		/// yaw angle of additional rotation to be added (used only if camera orientation is enabled 
		/// i.e. bAttachOrientation == true and look-at attachment is not defined in SetCameraAttachment())
		float							fAdditionalYaw;

		/// pitch angle of additional rotation to be added (used only if camera orientation is enabled 
		/// i.e. bAttachOrientation == true and look-at attachment is not defined in SetCameraAttachment())
		float							fAdditionalPitch;

		/// roll angle of additional rotation to be added (used only if camera orientation is enabled 
		/// i.e. bAttachOrientation == true and look-at attachment is not defined in SetCameraAttachment())
		float							fAdditionalRoll;

		/// Default constructor: initializes with zeros plus empty attach point
		SCameraAttachmentParams() { memset(this, 0, sizeof(*this)); uAttachPoint = MC_EMPTY_ID; }

		/// Constructor for attaching to object's scheme item
		SCameraAttachmentParams(IMcObject *_pObject, IMcObjectSchemeItem *_pItem = NULL, UINT _uAttachPoint = MC_EMPTY_ID)
		  :	SCameraAttachmentTarget(_pObject, _pItem, _uAttachPoint), Offset(v3Zero), 
			bAttachOrientation(false), fAdditionalYaw(0), fAdditionalPitch(0), fAdditionalRoll(0) {}
	};

/// \name Camera And Viewport
//@{
	//==============================================================================
	// Method Name: GetMapType()
	//------------------------------------------------------------------------------
	/// Retrieve the map type of the camera's viewport.
	///
	/// \param[out]	peMapType	the camera's viewport map type.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetMapType(EMapType *peMapType) const = 0;
//@}

/// \name Camera operations (for any map type)
//@{
	//==============================================================================
	// Method Name: SetCameraPosition()
	//------------------------------------------------------------------------------
	/// Sets the camera position.
	///
	/// \param[in]	Position		new camera position to set.
	/// \param[in]	bRelative		whether the camera position is relative to the 
	///								current one (delta), or absolute.
	///
	/// \note
	///		If SetCameraRelativeHeightLimits() is enabled, the camera
	///		position is adjusted according to the parameters set in
	///		that function.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCameraPosition(const SMcVector3D &Position, 
											   bool bRelative = false) = 0;

	//==============================================================================
	// Method Name: GetCameraPosition()
	//------------------------------------------------------------------------------
	/// Retrieves the camera position.
	///
	/// \param[out]	pPosition	current camera position.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameraPosition(SMcVector3D *pPosition) const = 0;

	//==============================================================================
	// Method Name: MoveCameraRelativeToOrientation()
	//------------------------------------------------------------------------------
	/// Moves the camera by a vector offset relative to the current camera orientation.
	///
	/// The camera's position is translated by the specified vector offset rotated by 
	/// the camers's orientation.
	///
	/// \param[in]	DeltaPosition		offset to add to the camera's position.
	/// \param[in]	bIgnorePitch		whether the camera's pitch angle is ignored
	///									(relevant for 3D only)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode MoveCameraRelativeToOrientation(const SMcVector3D &DeltaPosition, 
															 bool bIgnorePitch = false) = 0;

	//==============================================================================
	// Method Name: SetCameraOrientation()
	//------------------------------------------------------------------------------
	/// Sets the camera orientation angles (yaw, pitch and roll) in degrees.
	///
	/// For 2D cameras yaw only is used as camera rotation angle.
	///
	/// \param[in]	fYaw		new yaw value to set or FLT_MAX to leave the current value.
	/// \param[in]	fPitch		new pitch value to set or FLT_MAX to leave the current value.
	/// \param[in]	fRoll		new roll value to set or FLT_MAX to leave the current value.
	/// \param[in]	bRelative	specifies whether or not the yaw, pitch
	///							and roll values are relative to the
	///							current ones, or absolute.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCameraOrientation(float fYaw = FLT_MAX, float fPitch = FLT_MAX, 
												  float fRoll = FLT_MAX, bool bRelative = false) = 0;

	//==============================================================================
	// Method Name: GetCameraOrientation()
	//------------------------------------------------------------------------------
	/// Retrieves the camera orientation angles (yaw, pitch and roll) in degrees.
	///
	/// For 2D cameras yaw only is used as camera rotation angle.
	///
	/// \param[out]	pfYaw		current yaw (can be NULL if unnecessary).
	/// \param[out]	pfPitch		current pitch (can be NULL if unnecessary), 0.0f returned for 2D.
	/// \param[out]	pfRoll		current roll (can be NULL if unnecessary), 0.0f returned for 2D.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameraOrientation(float *pfYaw = NULL, float *pfPitch = NULL, 
												  float *pfRoll = NULL) const = 0;

	//==============================================================================
	// Method Name: SetCameraUpVector()
	//------------------------------------------------------------------------------
	/// Sets the camera up vector.
	///
	/// \param[in]	UpVector				new up vector to set.
	/// \param[in]	bRelativeToOrientation	whether or not the up vector is relative 
	///										to the camera orientation, or absolute.
	/// 
	/// \note
	///		For 3D: If the UpVector is not orthogonal to the current forward vector
	///		it is made orthogonal prior to setting it. The forward 
	///		vector is not modified by this function.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCameraUpVector(const SMcVector3D &UpVector, 
											   bool bRelativeToOrientation = false) = 0;

	//==============================================================================
	// Method Name: GetCameraUpVector()
	//------------------------------------------------------------------------------
	/// Retrieves the camera up vector.
	///
	/// \param[out]	pUpVector	current up vector.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameraUpVector(SMcVector3D *pUpVector) const = 0;

	//==============================================================================
	// Method Name: RotateCameraAroundWorldPoint()
	//------------------------------------------------------------------------------
	/// Rotates and moves the camera around given world pivot point by delta angles 
	/// in degrees so that the pivot point preserves its screen position.
	///
	/// For 2D cameras: yaw only is used as camera rotation angle; relative and absolute 
	/// rotations are the same.
	///
	/// \param[in]	PivotPoint				pivot point to rotate the camera around.
	/// \param[in]	fDeltaYaw				delta yaw.
	/// \param[in]	fDeltaPitch				delta pitch.
	/// \param[in]	fDeltaRoll				delta roll.
	/// \param[in]	bRelativeToOrientation	true if delta angles are relative to the camera's 
	///										current orientation (in the camera's local 
	///										coordinate system), false if they should be added 
	///										to the camera's yaw, pitch and roll angles.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RotateCameraAroundWorldPoint(const SMcVector3D &PivotPoint, 
		float fDeltaYaw, float fDeltaPitch = 0, float fDeltaRoll = 0, 
		bool bRelativeToOrientation = false) = 0;

	//==============================================================================
	// Method Name: SetCameraScale()
	//------------------------------------------------------------------------------
	/// Changes the camera zoom according to the desired scale (for perspective-projection 3D: at specified world point)
	///
	/// \param[in]	fWorldUnitsPerPixel		camera's desired scale in world units (e.g. meters) per pixel
	///										(the ratio of a distance on the ground to the corresponding 
	///										distance in display pixels)
	/// \param[in]	Point					world point to set the scale at (relevant for perspective-projection 3D only)
	/// 
	/// \note
	///		- To convert the map scale (the ratio of a distance on the ground to the corresponding 
	///		  distance on the display) to the camera scale, multiply the map scale by the physical height of 
	///		  a display pixel in world units (e.g. received from IMcMapViewport::GetPixelPhysicalHeight()).
	///		- Point is ignored in 2D maps and in parallel-projection 3D maps.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCameraScale(float fWorldUnitsPerPixel, const SMcVector3D &Point = v3MaxDouble) = 0;

	//==============================================================================
	// Method Name: GetCameraScale()
	//------------------------------------------------------------------------------
	/// Retrieves the current scale (for perspective-projection 3D: at specified world point)
	///
	/// \param[out]	pfWorldUnitsPerPixel	camera's current scale in world units (e.g. meters) per pixel
	///										(the ratio of a distance on the ground to the corresponding 
	///										distance in display pixels)
	/// \param[in]	Point					world point to get the scale at (relevant for perspective-projection 3D only)
	///
	/// \note
	///		- To convert the camera scale to the map scale (the ratio of a distance on the ground to 
	///		  the corresponding distance on the display), divide the camera scale by the physical height of 
	///		  a display pixel in world units (e.g. received from IMcMapViewport::GetPixelPhysicalHeight()).
	///		- Point is ignored in 2D maps and in parallel-projection 3D maps.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameraScale(float *pfWorldUnitsPerPixel, const SMcVector3D &Point = v3MaxDouble) const = 0;

	//==============================================================================
	// Method Name: SetCameraCenterOffset()
	//------------------------------------------------------------------------------
	/// Sets the the offset of the viewport's center relative to the camera frame's center.
	///
	/// Allows to define a camera with a viewport's center that is offset from camera frame's center. 
	///
	/// \param[in]	Offset		The offset along \a x and \a y axes in viewport's half-size units:
	///							- the default - (0, 0) means the camera's position, 
	///							- (-1, -1) means viewport's half-width left and half-height up 
	///							  from the camera's position,
	///							- ( 1,  1) means viewport's half-width right and half-height down 
	///							  from the camera's position,
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCameraCenterOffset(const SMcFVector2D &Offset) = 0;

	//==============================================================================
	// Method Name: GetCameraCenterOffset()
	//------------------------------------------------------------------------------
	/// Retrieves the offset of the camera center relative to the camera frame's center.
	///
	/// \param[out]	pOffset		The offset along \a x and \a y axes in viewport's half-size units:
	///							- the default - (0, 0) means the camera's position, 
	///							- (-1, -1) means viewport's half-width left and half-height up 
	///							  from the camera's position,
	///							- ( 1,  1) means viewport's half-width right and half-height down 
	///							  from the camera's position,
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameraCenterOffset(SMcFVector2D *pOffset) const = 0;

	//==============================================================================
	// Method Name: SetCameraWorldVisibleArea()
	//------------------------------------------------------------------------------
	/// Changes camera's visible area to the specified world box (optionally rotated in 2D maps).
	/// 
	/// The performed operation is:
	/// - for 2D camera: scrolling, rotating and changing scale (zoom)
	/// - for parallel-projection 3D camera: scrolling and changing scale (zoom)
	///
	/// Applicable only to 2D maps and to parallel-projection 3D maps.
	///
	/// \param[in]	VisibleArea		world rectangle (SMcBox::MinVertex and SMcBox::MaxVertex are its 
	///								diagonal points already rotated according to \a fYawAngle)
	/// \param[in]	nScreenMargin	screen margins: world visible area will be set according to the 
	///								viewport's screen rectangle reduced by moving its sides toward
	///								its center in \a nScreenMargin pixels (the default is zero).
	/// \param[in]	fRectangleYaw	world rectangle yaw angle (the default is zero); non-zero is applicable only to 2D maps.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCameraWorldVisibleArea(const SMcBox &VisibleArea, int nScreenMargin = 0, 
		float fRectangleYaw = 0) = 0;

	//==============================================================================
	// Method Name: GetCameraWorldVisibleArea()
	//------------------------------------------------------------------------------
	/// Retrieves the camera's world visible area as a world rectangle (optionally rotated).
	///
	/// Applicable only to 2D maps.
	///
	/// \param[out]	pVisibleArea	world rectangle (SMcBox::MinVertex and SMcBox::MaxVertex are its 
	///								diagonal points already rotated according to \a fYawAngle)
	/// \param[in]	nScreenMargin	screen margins: world visible area will be calculated for the 
	///								viewport's screen rectangle reduced by moving its sides toward
	///								its center in \a nScreenMargin pixels (the default is zero).
	/// \param[in]	fRectangleYaw	world rectangle yaw angle (the default is zero).
	/// 
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameraWorldVisibleArea(SMcBox *pVisibleArea, int nScreenMargin = 0, 
		float fRectangleYaw = 0) const = 0;

	//==============================================================================
	// Method Name: SetCameraScreenVisibleArea()
	//------------------------------------------------------------------------------
	/// Changes camera's visible area to the specified screen rectangle.
	/// 
	/// The performed operation is:
	/// - for 2D camera: scrolling and changing scale (zoom)
	/// - for 3D camera: according to \a e3DOperation (see details below)
	///
	/// \param[in]	VisibleArea		screen visible area to set.
	/// \param[in]	e3DOperation	performed operation for 3D camera:
	///								- #ESVAO_ROTATE_AND_MOVE (default) rotates and moves 
	///								  towards the rectangle
	///								- #ESVAO_ROTATE_AND_SET_FOV rotates towards the rectangle 
	///								  and changes field of view (zoom);
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCameraScreenVisibleArea(const SMcRect &VisibleArea, 
		ESetVisibleArea3DOperation e3DOperation = ESVAO_ROTATE_AND_MOVE) = 0;
//@}

	//==============================================================================
	// Method Name: ScrollCamera()
	//------------------------------------------------------------------------------
	/// Scrolls the camera by the desired amounts of pixels.
	///
	/// Applicable only to 2D maps and to parallel-projection 3D maps.
	///
	/// \param[in]	nDeltaX		desired horizontal translation.
	/// \param[in]	nDeltaY		desired vertical translation.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ScrollCamera(int nDeltaX, int nDeltaY) = 0;

/// \name Camera operations (for 3D map type only)
//@{
	//==============================================================================
	// Method Name: SetCameraLookAtPoint()
	//------------------------------------------------------------------------------
	/// Sets the camera lookat point.
	///
	/// \param[in]	LookAtPoint		new lookat point to set.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCameraLookAtPoint(const SMcVector3D &LookAtPoint) = 0;

	//==============================================================================
	// Method Name: SetCameraForwardVector()
	//------------------------------------------------------------------------------
	/// Sets the camera forward vector
	///
	/// \param[in]	ForwardVector			new forward vector to set.
	/// \param[in]	bRelativeToOrientation	whether or not the forward vector is relative 
	///										to the camera orientation, or absolute.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCameraForwardVector(const SMcVector3D &ForwardVector, 
													bool bRelativeToOrientation = false) = 0;

	//==============================================================================
	// Method Name: GetCameraForwardVector()
	//------------------------------------------------------------------------------
	/// Retrieves the camera forward vector.
	///
	/// \param[out]	pForwardVector	current forward vector.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameraForwardVector(SMcVector3D *pForwardVector) const = 0;

	//==============================================================================
	// Method Name: SetCameraFieldOfView()
	//------------------------------------------------------------------------------
	/// Sets the camera field of view in degrees.
	///
	/// \param[in]	fFieldOfViewHorizAngle	new horizontal field of view to set (the default is 45); 
	///										non-zero means perspective-projection 3D camera, zero means parallel-projection 3D camera.
	/// 
	/// \note
	///		The vertical field of view is adjusted automatically, in
	///		accordance to the current aspect ratio.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCameraFieldOfView(float fFieldOfViewHorizAngle) = 0;

	//==============================================================================
	// Method Name: GetCameraFieldOfView()
	//------------------------------------------------------------------------------
	/// Retrieves the camera field of view in degrees.
	///
	/// \param[out]	pfFieldOfViewHorizAngle	current horizontal field of view (the default is 45); 
	///										non-zero means perspective-projection 3D camera, zero means parallel-projection 3D camera.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameraFieldOfView(float *pfFieldOfViewHorizAngle) const = 0;
	
	//==============================================================================
	// Method Name: RotateCameraRelativeToOrientation()
	//------------------------------------------------------------------------------
	/// Rotates the camera by delta angles in degrees, relative to the current camera orientation.
	///
	/// Delta pitch and roll are in the camera's local coordinate system, yaw - according to \p bYawAbsolute.
	///
	/// \param[in]	fDeltaYaw		delta yaw.
	/// \param[in]	fDeltaPitch		local delta pitch.
	/// \param[in]	fDeltaRoll		local delta roll.
	/// \param[in]	bYawAbsolute	whether yaw is in the global coordinate system or in the camera's local one; 
	///								the default is `true` (in the global coordinate system).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RotateCameraRelativeToOrientation(float fDeltaYaw, float fDeltaPitch, float fDeltaRoll, bool bYawAbsolute = true) = 0;

	//==============================================================================
	// Method Name: SetCameraRelativeHeightLimits()
	//------------------------------------------------------------------------------
	/// Sets the relative height limits of the camera.
	///
	/// \param[in]	dMinHeight	minimum height.
	/// \param[in]	dMaxHeight	maximum height.
	/// \param[in]	bEnabled	specifies whether the height limits
	///							are in effect or not.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCameraRelativeHeightLimits(double dMinHeight, double dMaxHeight, 
														   bool bEnabled) = 0;

	//==============================================================================
	// Method Name: GetCameraRelativeHeightLimits()
	//------------------------------------------------------------------------------
	/// Retrieves the relative height limits of the camera.
	///
	/// \param[out]	pdMinHeight		current minimum height.
	/// \param[out]	pdMaxHeight		current maximum height.
	/// \param[out]	pbEnabled		current activation status.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameraRelativeHeightLimits(double *pdMinHeight, double *pdMaxHeight, bool *pbEnabled) const = 0;

	//==============================================================================
	// Method Name: SetCameraClipDistances()
	//------------------------------------------------------------------------------
	/// Sets the camera clip distances.
	///
	/// \param[in]	fMin					minimum clip distance (near clip plane).
	/// \param[in]	fMax					maximum clip distance (far clip plane).
	/// \param[in]	bRenderInTwoSessions	whether to calculate an intermediate clip distanse and 
	///										to render in two sessions:
	///										- first, between intermediate to maximum
	///										- second, between minimum to intermediate
	///
	/// \note
	///		Rendering in two sessions combines an improved visual quality in the near range 
	///		with an ability to render very far terrain areas and objects. Can affect the performance.
	///		Should be used when the maximum/minimum ratio is big (more than 5,000 - 10,000).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCameraClipDistances(float fMin, float fMax, 
													bool bRenderInTwoSessions) = 0;

	//==============================================================================
	// Method Name: GetCameraClipDistances()
	//------------------------------------------------------------------------------
	/// Retrieves the camera clip distances.
	///
	/// \param[out]	pfMin					current minimum clip distance.
	/// \param[out]	pfMax					current maximum clip distance.
	/// \param[out]	pbRenderInTwoSessions	whether rendering in two sessions is enabled
	///										(can be NULL if unnecessary)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameraClipDistances(float *pfMin, float *pfMax,
													bool *pbRenderInTwoSessions = NULL) const = 0;
//@}

/// \name Camera Position/Rotation Attachment
//@{
	//==============================================================================
	// Method Name: SetCameraAttachmentEnabled()
	//------------------------------------------------------------------------------
	/// Enables or disables camera attachment.
	///
	/// When enabled, the camera position and (optionally) orientation or look-at point 
	///	are updated automatically according to the objects or their parts specified in 
	/// SetCameraAttachment().
	///
	/// \param[in]	bEnable		whether camera attachment should be enabled or disabled.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCameraAttachmentEnabled(bool bEnable) = 0;

	//==============================================================================
	// Method Name: GetCameraAttachmentEnabled()
	//------------------------------------------------------------------------------
	/// Checks if camera attachment is enabled or disabled by SetCameraAttachmentEnabled()
	///
	/// \param[out]	pbEnabled	whether camera attachment is enabled or disabled.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameraAttachmentEnabled(bool *pbEnabled) = 0;

	//==============================================================================
	// Method Name: SetCameraAttachment()
	//------------------------------------------------------------------------------
	/// Defines attachment of camera position and (optionally) orientation or look-at point.
	///
	/// \param[in]	pAttachment			attachment of camera position and (optionally) orientation
	///									(see SCameraAttachmentParams for details); 
	///									can be NULL.
	/// \param[in]	pLookAtAttachment	optional attachment of camera look-at point
	///									(see SCameraAttachmentTarget for details); 
	///									can be NULL.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCameraAttachment(
		const SCameraAttachmentParams *pAttachment, 
		const SCameraAttachmentTarget *pLookAtAttachment) = 0;

	//==============================================================================
	// Method Name: GetCameraAttachment()
	//------------------------------------------------------------------------------
	/// Retrieves attachment of camera position and (optionally) orientation or look-at point.
	///
	/// \param[out]	pAttachment					attachment of camera position and (optionally) 
	///											orientation (see SCameraAttachmentParams for details); 
	///											can be NULL if unnecessary.
	/// \param[out]	pbAttachmentDefined			whether attachment is defined; 
	///											can be NULL if unnecessary.
	/// \param[out]	pLookAtAttachment			optional attachment of camera look-at point
	///											(see SCameraAttachmentTarget for details);
	///											valid only if pbLookAtAttachmentDefined returned is true;
	///											can be NULL if unnecessary.
	/// \param[out]	pbLookAtAttachmentDefined	whether look-at attachment is defined; 
	///											can be NULL if unnecessary.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameraAttachment(
		SCameraAttachmentParams *pAttachment = NULL, bool *pbAttachmentDefined = NULL,
		SCameraAttachmentTarget *pLookAtAttachment = NULL, bool *pbLookAtAttachmentDefined = NULL) = 0;
//@}

/// \name Coordinate Conversions (To And From Screen), Matrices And Footprint
//@{
	//==============================================================================
	// Method Name: WorldToScreen()
	//------------------------------------------------------------------------------
	/// Converts a world point into a screen point.
	///
	/// \param[in]	WorldPoint		world point to convert.
	/// \param[out]	pScreenPoint	screen point converted.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode WorldToScreen(const SMcVector3D &WorldPoint, 
										   SMcVector3D *pScreenPoint) const = 0;

	//==============================================================================
	// Method Name: ScreenToWorldOnTerrain()
	//------------------------------------------------------------------------------
	/// Converts a screen point into a world point by intersecting the terrain.
	///
	/// \param[in]	ScreenPoint			screen point to convert.
	/// \param[out]	pWorldPoint			world point converted.
	/// \param[out]	pbIntersectionFound	whether an intersection is found.
	/// \param[in]	pParams				query parameters or NULL to use the default
	///
	/// \note
	///		in 2D only: if intersection is not found, x,y coordinates are calculated, z will be #MC_NO_DTM_VALUE
	/// 
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ScreenToWorldOnTerrain(const SMcVector3D &ScreenPoint, 
		SMcVector3D *pWorldPoint, bool *pbIntersectionFound, 
		const IMcSpatialQueries::SQueryParams *pParams = NULL) = 0;

	//==============================================================================
	// Method Name: ScreenToWorldOnPlane()
	//------------------------------------------------------------------------------
	/// Converts a screen point into a world point by intersecting the specified plane.
	///
	/// \param[in]	ScreenPoint			screen point to convert.
	/// \param[out]	pWorldPoint			world point converted.
	/// \param[out]	pbIntersectionFound	whether an intersection is found.
	/// \param[in]	dPlaneLocation		location of the plane (signed distance from (0,0,0) to 
	///									the plane along its normal; the default is 0
	/// \param[in]	PlaneNormal			normal of the plane; 
	///									the default is (0,0,1), i.e. horizontal plane
	///									(for 2D viewport default normal only is valid)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ScreenToWorldOnPlane(const SMcVector3D &ScreenPoint, 
		SMcVector3D *pWorldPoint, bool *pbIntersectionFound, double dPlaneLocation = 0, 
		const SMcVector3D PlaneNormal = SMcVector3D(0, 0, 1)) const = 0;

	//==============================================================================
	// Method Name: GetCameraMatrix()
	//------------------------------------------------------------------------------
	/// Retrieves the camera matrix according to the specified type.
	///
	/// \param[in]	eCameraMatrixType	type of camera matrix to retrieve
	/// \param[out]	pMatrix				camera matrix retrieved
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameraMatrix(ECameraMatrixType eCameraMatrixType, SMcMatrix4D *pMatrix) const = 0;

	//==============================================================================
	// Method Name: GetCameraFootprint()
	//------------------------------------------------------------------------------
	/// Calculates the camera's footprint.
	///
	/// Intersections of 4 camera corner rays and center ray with the terrain are returned
	/// along with flags indicating if the appropriate points are found.
	///
	/// \param[out]	pFootprint				footprint points.
	/// \param[in]	pAsyncQueryCallback		optional callback for asynchronous query; if not NULL: the query will be performed asynchronously, 
	///										the function will return without results, when the results are ready a callback function 
	///										IMcSpatialQueries::IAsyncQueryCallback::OnRayIntersectionTargetsResults() will be called; 
	///										the same callback instance can be used again in another query only after the first query is completed; 
	///										the arguments of the callback function will be:
	///										- aIntersections[0].IntersectionPoint = pFootprint->UpperLeft, 
	///										- aIntersections[1].IntersectionPoint = pFootprint->UpperRight, 
	///										- aIntersections[2].IntersectionPoint = pFootprint->LowerRight, 
	///										- aIntersections[3].IntersectionPoint = pFootprint->LowerLeft, 
	///										- aIntersections[4].IntersectionPoint = pFootprint->Center, 
	///										- aIntersections[0,...,4].eTargetType according to pFootprint->bUpperLeftFound, ..., pFootprint->bCenterFound: 
	///											- IMcSpatialQueries::EITT_NONE in case of `false`,
	///											- other than IMcSpatialQueries::EITT_NONE in case of `true`,
	///										- other members of IMcSpatialQueries::STargetFound: unused.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameraFootprint(SCameraFootprintPoints *pFootprint,
		IMcSpatialQueries::IAsyncQueryCallback *pAsyncQueryCallback = NULL) const = 0;
//@}
};
