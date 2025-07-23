#pragma once

//===========================================================================
/// \file IMcMapViewport.h
/// Interfaces for map viewport
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcPrintMap.h"
#include "IMcGlobalMap.h"
#include "IMcImageProcessing.h"
#include "IMcMapDevice.h"
#include "IMcMapCamera.h"
#include "McCommonTypes.h"
#include "Calculations/IMcSpatialQueries.h"
#include "OverlayManager/IMcTexture.h"
#include "OverlayManager/IMcObjectSchemeItem.h"
#include "OverlayManager/IMcProperty.h"
// #ifndef _WIN32
// #include "LinuxDef.h"
// #endif

struct SMcPlane;
class IMcMapTerrain;
class IMcMapGrid;
class IMcMapHeightLines;
class IMcImageCalc;
class IMcOverlayManager;
class IMcMapLayer;
class IMcGridCoordinateSystem;

//==============================================================================
/// To be passed instead of window handle for using current GL render context.
/// 
/// Tells MapCore that the user is responsible for creating GL render context 
/// (the current GL render context will be used by MapCore).
/// 
/// Applicable only to GL rendering systems.
//==============================================================================
#define MC_HWND_CURRENT_GL_CONTEXT		((HWND)-1)

//==============================================================================
/// To be passed instead of window handle for non-window viewport.
//==============================================================================
#define MC_HWND_NONE					((HWND)NULL)

//===========================================================================
// Interface Name: IMcMapViewport
//---------------------------------------------------------------------------
/// Interface for map viewport
//===========================================================================
class IMcMapViewport 
  :	public virtual IMcMapCamera, public virtual IMcSpatialQueries, 
	public IMcGlobalMap, public IMcPrintMap, public IMcImageProcessing
{
protected:
    virtual ~IMcMapViewport() {}

public:
	enum
	{
		//==============================================================================
		/// Unique ID for IMcSpatialQueries-derived interface
		//==============================================================================
		INTERFACE_TYPE = 2
	};

	/// Types of delayed object updates
	enum EObjectDelayType
	{
		/// for vector-layer collision prevention: check objects previously hidden to recognize those 
		/// that can be restored; enabled with 100 checks per render by default.
		EODT_VIEWPORT_CHECK_HIDDEN_OBJECT_COLLISION,

		/// position update of object's items as a result of the viewport's 
		/// active camera update; disabled by default.
		EODT_VIEWPORT_CHANGE_OBJECT_UPDATE,

		/// update of overlay/object/object-node's conditional selector result as a result of 
		/// the viewport's active camera update; disabled by default.
		EODT_VIEWPORT_CHANGE_OBJECT_CONDITION,
		
		/// size update of object's screen-size items as a result of the viewport's 
		/// active camera update; enabled with 50 batch updates per render by default.
		EODT_VIEWPORT_CHANGE_OBJECT_SIZE,

		/// height update of relative-to-DTM objects (in 3D viewport only); enabled with 500
		/// objects per render by default
		EODT_VIEWPORT_CHANGE_OBJECT_HEIGHT,

		/// memory defragmentation of object batches (n batches per 100 renderings: 
		/// if n >= 100 then n/100 batches will be defragmented per one rendering, 
		/// if n < 100 then one batch will be defragmented each 100/n renderings);
		/// enabled with 50 by default (one batch each 100/50 = 2 renderings)
		EODT_VIEWPORT_DEFRAG_OBJECT_BATCHES
	};

	/// Types of pending updates bits (for using in bit field)
	enum EPendingUpdateType
	{
		EPUT_TERRAIN		= 0x0001,	///< terrain changes and loading
		EPUT_OBJECTS		= 0x0002,	///< object changes
		EPUT_GLOBAL_MAP		= 0x0004,	///< global map operation
		EPUT_GRID			= 0x0008,	///< grid changes
		EPUT_IMAGEPROCESS	= 0x0010,	///< Image process changes
		EPUT_ANY_UPDATE		= 0xFFFF	///< any pending updates
	};

	/// Render stages to apply image processing at
	enum EImageProcessingStage
	{
		EIPS_RASTER_LAYERS,			///< after rendering raster map layers
		EIPS_WITHOUT_OBJECTS,		///< before rendering objects
		EIPS_ALL,					///< after all rendering (post-processing)
	};

	/// Modes of stereo rendering
	enum EStereoRenderMode
	{
		ESRM_NONE,					///< no stereo (mono mode)
		ESRM_GL_QUAD_BUFFER,		///< GL's quad-buffer stereo
		ESRM_MANUAL					///< manual stereo (left-eye and right-eye frames are rendered one after 
									///< another to be switched by external hardware operated by the user)
	};

	/// Modes of adding noise to terrain
	enum ETerrainNoiseMode
	{
		ETNM_NONE,				///< no noise is added
		ETNM_NOISE_TEXTURE		///< noise texture is added
	};

	/// Modes of shadow to be used in the scene
	enum EShadowMode
	{
        /** No shadows. */
        ESM_NONE = 0x00,

		/** Stencil shadow technique which renders all shadow volumes as
            a modulation after all the non-transparent areas have been 
            rendered. This technique is considerably less fillrate intensive 
            than the additive stencil shadow approach when there are multiple
            lights, but is not an accurate model.\n\n
        */
        ESM_STENCIL_MODULATIVE = 0x12,

		/** Stencil shadow technique which renders each light as a separate
            additive pass to the scene. This technique can be very fillrate
            intensive because it requires at least 2 passes of the entire
            scene, more if there are multiple lights. However, it is a more
            accurate model than the modulative stencil approach and this is
            especially apparent when using colored lights or bump mapping.\n\n
        */
        ESM_STENCIL_ADDITIVE = 0x11,

		/** Texture-based shadow technique which involves a monochrome render-to-texture
            of the shadow caster and a projection of that texture onto the 
            shadow receivers as a modulative pass.\n
        */
        ESM_TEXTURE_MODULATIVE = 0x22,
		
        /** Texture-based shadow technique which involves a render-to-texture
            of the shadow caster and a projection of that texture onto the 
            shadow receivers, built up per light as additive passes. 
			This technique can be very fillrate intensive because it requires numLights + 2 
			passes of the entire scene. However, it is a more accurate model than the 
			modulative approach and this is especially apparent when using colored lights 
			or bump mapping.\n\n
        */
        ESM_TEXTURE_ADDITIVE = 0x21,

		/** Texture-based shadow technique which involves a render-to-texture
		of the shadow caster and a projection of that texture on to the shadow
		receivers, with the usage of those shadow textures completely controlled
		by the materials of the receivers.
		This technique is easily the most flexible of all techniques because 
		the material author is in complete control over how the shadows are
		combined with regular rendering. It can perform shadows as accurately
		as ESM_TEXTURE_ADDITIVE but more efficiently because it requires
		less passes. However it also requires more expertise to use, and 
		in almost all cases, shader capable hardware to really use to the full.
		@note The 'additive' part of this mode means that the color of
		the rendered shadow texture is by default plain black. It does
		not mean it does the adding on your receivers automatically though, how you
		use that result is up to you.\n\n
		*/
		ESM_TEXTURE_ADDITIVE_INTEGRATED = 0x25,

		/** Texture-based shadow technique which involves a render-to-texture
			of the shadow caster and a projection of that texture on to the shadow
			receivers, with the usage of those shadow textures completely controlled
			by the materials of the receivers.
			This technique is easily the most flexible of all techniques because 
			the material author is in complete control over how the shadows are
			combined with regular rendering. It can perform shadows as accurately
			as ESM_TEXTURE_ADDITIVE but more efficiently because it requires
			less passes. However it also requires more expertise to use, and 
			in almost all cases, shader capable hardware to really use to the full.
			@note The 'modulative' part of this mode means that the color of
			the rendered shadow texture is by default the 'shadow color'. It does
			not mean it modulates on your receivers automatically though, how you
			use that result is up to you.
		*/
		ESM_TEXTURE_MODULATIVE_INTEGRATED = 0x26
	};


	/// Determines on which targets object scheme items attached to terrain should be displayed
	enum EDisplayingItemsAttachedToTerrain
	{
		EDIATT_ON_TERRAIN_ONLY,					///< on terrain only (the default)
		EDIATT_ON_TERRAIN_AND_SPECIFIED_ITEMS,	///< on terrain and specified items
		EDIATT_ON_TERRAIN_AND_ALL_MESH_ITEMS	///< on terrain and all mesh items
	};

	//==============================================================================
	// Enum Name: EGpuProgramType
	//------------------------------------------------------------------------------
	/// GPU program type
	//==============================================================================
	enum EGpuProgramType
	{
		EGPT_VERTEX_PROGRAM,	///< vertex program
		EGPT_FRAGMENT_PROGRAM	///< fragment program (a.k.a. pixel program)
	};

	/// Camera update callback interface to be inherited by the user
	class ICameraUpdateCallback
#ifdef __EMSCRIPTEN__
		: public IMcEmscriptenCallbackRef
#endif
	{
	public:
		virtual ~ICameraUpdateCallback() {}

		//==============================================================================
		// Method Name: OnActiveCameraUpdated()
		//------------------------------------------------------------------------------
		/// Callback function to be called on active camera update.
		///
		/// Called when either the active camera itself is updated or the viewport's 
		/// active camera is changed
		///
		/// \param[in]	pViewport	the viewport called the callback
		//==============================================================================
		virtual void OnActiveCameraUpdated(IMcMapViewport *pViewport) = 0;

		//==============================================================================
		// Method Name: Release()
		//------------------------------------------------------------------------------
		/// A callback that should release callback class instance.
		///
		///	Can be implemented by the user to optionally delete callback class instance when 
		/// IMcMapViewport instance is been removed.
		//==============================================================================
		virtual void Release() {}
	};

	/// Stereo rendering callback interface to be inherited by the user
	class IStereoRenderCallback
#ifdef __EMSCRIPTEN__
		: public IMcEmscriptenCallbackRef
#endif
	{
	public:
		virtual ~IStereoRenderCallback() {}

		//==============================================================================
		// Method Name: OnEyeFrameStarted()
		//------------------------------------------------------------------------------
		/// Called before each-eye-frame rendering started
		///
		/// \param[in]	pViewport	the viewport called the callback
		/// \param[in]	bLeftEye	true for left eye, false for right one
		//==============================================================================
		virtual void OnEyeFrameStarted(IMcMapViewport *pViewport, bool bLeftEye) = 0;

		//==============================================================================
		// Method Name: OnEyeFrameFinished()
		//------------------------------------------------------------------------------
		/// Called after each-eye-frame rendering finished
		///
		/// \param[in]	pViewport	the viewport called the callback
		/// \param[in]	bLeftEye	true for left eye, false for right one
		//==============================================================================
		virtual void OnEyeFrameFinished(IMcMapViewport *pViewport, bool bLeftEye) = 0;

		//==============================================================================
		// Method Name: Release()
		//------------------------------------------------------------------------------
		/// A callback that should release callback class instance.
		///
		///	Can be implemented by the user to optionally delete callback class instance when 
		/// IMcMapViewport instance is been removed.
		//==============================================================================
		virtual void Release() {}
	};

	/// Viewport creation parameters
	struct SCreateData : public IMcSpatialQueries::SCreateData
	{		
		/// either of the following:
		/// - real handle of window to create map viewport in (in Web: HTMLCanvasElement)
		/// - #MC_HWND_CURRENT_GL_CONTEXT for using current GL context created by the user 
		///   (see #MC_HWND_CURRENT_GL_CONTEXT for details)
		/// - #MC_HWND_NONE for non-window viewport
		HWND			hWnd;

		/// GL render context (HGLRC) created by the user; 
		/// the default is NULL (the render context should be created by MapCore); 
		/// applicable only to GL rendering systems
		void			*pExternalGLRenderContext;

		/// existing viewport to share window with; 
		/// use #TopLeftCornerInWindow and #BottomRightCornerInWindow to control the position in the window
		IMcMapViewport	*pShareWindowViewport;

		/// whether the user is responsible for dealing with pixel format, vsync and swapping buffers; 
		/// the default is false; 
		/// applicable only to GL rendering systems
		bool			bExternalGLControl;

		/// whether the user application (e.g. based on QT) might change GL states of this viewport's GL context; 
		/// the default is false; 
		/// applicable only to GL rendering systems
		bool			bExternalGLStateChanges;

		/// whether the viewport should be created in full-screen mode
		bool			bFullScreen;
		
		/// whether the viewport will use GL's quad-buffer stereo mode
		bool			bEnableGLQuadBufferStereo;

		/// non-window viewport pixel format; ignored if \a hWnd is a real window handle (in Web: HTMLCanvasElement)
		IMcTexture::EPixelFormat ePixelFormat;

		/// grid definition
		IMcMapGrid		*pGrid;

		/// For diagonal-image 2D maps:
		/// - in mono mode: camera model interface
		/// - in stereo mode: camera model interface for master-eye image (see SStereoParams for details)
		IMcImageCalc	*pImageCalc;

		/// For diagonal-image 2D maps in stereo mode only : camera model interface for slave-eye image
		///  (see SStereoParams for details)
		IMcImageCalc	*pStereoSlaveImageCalc;

		/// Stereo rendering callback
		IStereoRenderCallback	*pStereoRenderCallback;

		/// map type
		EMapType		eMapType;

		/// viewport's window width in pixels; ignored if \a hWnd is a real window handle (in Web: HTMLCanvasElement)
		UINT uWidth;

		/// viewport's window height in pixels; ignored if \a hWnd is a real window handle (in Web: HTMLCanvasElement)
		UINT uHeight;

		/// position of viewport's top-left corner relative to its window; values of x and y are between 0 and 1; the default is (0.0, 0.0)
		SMcFVector2D TopLeftCornerInWindow;

		/// position of viewport's bottom-right corner relative to its window; values of x and y are between 0 and 1; the default is (1.0, 1.0)
		SMcFVector2D BottomRightCornerInWindow;

		/// whether geographic coordinate system should be displayed in metric proportions
		/// (default is true for 3D map and false for 2D map)
		bool bShowGeoInMetricProportion;

		/// viewport DTM usage and precision (relevant in 2D only, default is #EQP_DEFAULT): 
		/// - #EQP_DEFAULT means DTM is loaded according to viewport scale and can be used 
		///   both in queries and visualization
		/// - other values mean DTM is loaded in a specified precision and can be used in 
		///   queries only, but not in visualization - recommended in low memory computers
		EQueryPrecision eDtmUsageAndPrecision;

		/// best resolution (in world units per pixel) of objects attached to terrain; larger value
		/// means building additional levels of detail of terrain; will be rounded to the nearest 
		/// negative power of 2; the default is 0.125
		float fTerrainObjectsBestResolution;

		/// whether render data of objects attached to unused terrain tiles should be stored in cache 
		/// or removed immediately; the default is true; false should be used in low memory computers only
		bool bTerrainObjectsCache;

		/// determines on which targets object scheme items attached to terrain should be displayed; 
		/// the default is #EDIATT_ON_TERRAIN_ONLY
		EDisplayingItemsAttachedToTerrain eDisplayingItemsAttachedToTerrain;

		/// The factor affecting the selection of the resolution of terrain layers to be displayed in 
		///	each area according to the area's local scale; 1 (the default) means the resolution intended for the scale, 
		///	values greater than 1 mean lower (coarser) resolutions, values smaller than 1 mean higher (finer) resolutions 
		///	(the minimal allowed value is 0.1)
		float fTerrainResolutionFactor;

		//==============================================================================
		// Method Name: SCreateData()
		//------------------------------------------------------------------------------
		/// Constructor
		///
		/// \param[in]	_eMapType	Map type (affects the default of SCreateData::bShowGeoInMetricProportion)
		//==============================================================================
		SCreateData(EMapType _eMapType EMSCRIPTEN_ONLY(= IMcMapCamera::EMT_2D))
			: pExternalGLRenderContext(NULL), pShareWindowViewport(NULL), bExternalGLControl(false), bExternalGLStateChanges(false), 
			  bFullScreen(false), bEnableGLQuadBufferStereo(false), ePixelFormat(IMcTexture::EPF_UNKNOWN),
			  pGrid(NULL), pImageCalc(NULL), pStereoSlaveImageCalc(NULL), 
			  pStereoRenderCallback(NULL), uWidth(0), uHeight(0), TopLeftCornerInWindow(0.0f, 0.0f), BottomRightCornerInWindow(1.0f, 1.0f), 
			  eMapType(_eMapType), bShowGeoInMetricProportion(_eMapType == EMT_3D), eDtmUsageAndPrecision(EQP_DEFAULT), 
			  fTerrainObjectsBestResolution(0.125),
			  bTerrainObjectsCache(true),
			  eDisplayingItemsAttachedToTerrain(EDIATT_ON_TERRAIN_ONLY),
			  fTerrainResolutionFactor(1.0)

			EMSCRIPTEN_ADD_VAL_MEMBER_TO_INITIALIZERS(hWnd)
		{}
	};

	/// Rendering statistics
	struct SRenderStatistics
	{
		float fLastFPS;				///< frame rate of the last frame (in frames per second)
		float fAverageFPS;			///< average frame rate (in frames per second)
		float fBestFPS;				///< best frame rate (in frames per second)
		float fWorstFPS;			///< worst frame rate (in frames per second)
		UINT uNumLastFrameTriangles;///< number of triangles rendered in the last frame
		UINT uNumLastFrameBatches;	///< number of batches rendered in the last frame
	};

	/// frame buffer interface
	class IFrameBuffer : public IMcDestroyable
	{
	protected:
		virtual ~IFrameBuffer() {}

	public:
		//==============================================================================
		// Method Name: GetData()
		//------------------------------------------------------------------------------
		/// Creates and retrieves memory buffer containing the frame pixels.
		///
		/// \param[out]	ppData		memory buffer created (valid until the interface is destroyed)
		///
		/// \return
		///     - status result
		//==============================================================================
		virtual IMcErrors::ECode GetData(const void **ppData) = 0;
	};
	
	struct SPFrameData
	{
		UINT					uFrameMinSizeToInvertColors;
		IFrameBuffer			*pFrameBuffer;
		SMcRect					BoundingRect;
		SMcBColor				NoChangeColor;
		short					nOffsetX;
		short					nOffsetY;
		bool					bIFrame;

		SPFrameData() : pFrameBuffer (NULL), uFrameMinSizeToInvertColors(0), bIFrame (false) {}
	};

	/// Rolling-shutter camera position and orientation
	struct SRollingShutterLocation
	{
		SMcVector3D	Position;	///< position
		float		fYaw;		///< yaw angle
		float		fPitch;		///< pitch angle
		float		fRoll;		///< roll angle
		UINT		uRow;		///< starting pixel row
	};

	/// Rolling-shutter camera frame data
	struct SRollingShutterData
	{
		/// array of rolling-shutter locations; starting pixel row (\a uRow) should be 
		/// zero for the first location, frame height minus one for the last location
		SRollingShutterLocation		*aRollingShutterLocations;

		/// length of array of rolling-shutter locations
		UINT						uNumRollingShutterLocations;

		/// number of pixel rows in each strip rendered together
		UINT						uNumPixelsPerStrip;

		SRollingShutterData() : aRollingShutterLocations(NULL), uNumRollingShutterLocations(0) {}
	};

	/// Parameters for stereo mode
	struct SStereoParams
	{
		/// offset between camera centers for left-eye and right-eye images (see SetCameraCenterOffset())
		float fCamerasCentersOffset;

		/// for 3D stereo only: offset between camera positions for left-eye and right-eye images (see SetCameraPosition())
		float fCamerasPositionOffset;

		/// for two-diagonal-image 2D stereo only: whether the master image is for the left or right eye 
		/// (the first terrain is shown on the master image, the second one on the slave image; 
		/// objects positions in both images are according to the master camera and master image calc)
		bool bLeftEyeMaster;
	};

	/// Color with height
	struct SHeigtColor
	{
		/// color value
		SMcBColor	Color;			
		
		/// color height in steps from the origin (the actual height is 
		/// SDtmVisualizationParams.fHeightColorsHeightOrigin + \b nHeightInSteps * SDtmVisualizationParams.fHeightColorsHeightStep);
		/// can be negative
		int			nHeightInSteps;
	};
	
	/// Parameters of DTM visualization
	struct SDtmVisualizationParams
	{
		/// array of colors with heights (for heights out of the range the first/last color will be used) 
		const SHeigtColor *aHeightColors;
		
		/// number of colors in the above array
		UINT uNumHeightColors;

		/// height origin
		float fHeightColorsHeightOrigin;

		/// height step
		float fHeightColorsHeightStep;

		/// height factor for shading
		float fShadingHeightFactor;

		/// light source yaw angle in degrees
		float fShadingLightSourceYaw;

		/// light source pitch angle in degrees
		float fShadingLightSourcePitch;

		/// transparency of height colors or 0 if height colors should not be displayed
		BYTE uHeightColorsTransparency;

		/// transparency of shading or 0 if shading should not be displayed
		BYTE uShadingTransparency;

		/// whether height colors should be interpolated between the heights specified or 
		/// each color is used between the height if the previous color and its height
		bool bHeightColorsInterpolation;

		/// whether DTM visualization should be displayed above or below raster layers
		bool bDtmVisualizationAboveRaster;

		//==============================================================================
		// Method Name: SDtmVisualizationParams()
		//------------------------------------------------------------------------------
		/// Default constructor
		//==============================================================================
		SDtmVisualizationParams()
		{
			memset(this, 0, sizeof(*this));
			fShadingHeightFactor = 1;
			fShadingLightSourceYaw = 180;
			fShadingLightSourcePitch = -45;
			bHeightColorsInterpolation = true;
		}

		//==============================================================================
		// Method Name: SetDefaultHeightColors()
		//------------------------------------------------------------------------------
		/// Sets default height colors for backward compatibility
		///
		/// \param[in]	fMinHeight		minimum height to be colored by the first color 
		///								(the default is -500)
		/// \param[in]	fMaxHeight		maximum height to be colored by the last color 
		///								(the default is 9500)
		//==============================================================================
		void SetDefaultHeightColors(float fMinHeight = -500, float fMaxHeight = 9500)
		{
			static SHeigtColor aDefaultHeightColors[8] = 
			{
				{ SMcBColor(  3,   0,  25, 255), 0 }, 
				{ SMcBColor(  8,   1,  68, 255), 1 }, 
				{ SMcBColor( 14,  17, 161, 255), 2 }, 
				{ SMcBColor(  9, 120, 197, 255), 3 }, 
				{ SMcBColor( 19, 234, 154, 255), 4 }, 
				{ SMcBColor( 79, 239,  47, 255), 5 }, 
				{ SMcBColor(217 ,175,   0, 255), 6 }, 
				{ SMcBColor(248,  67,  10, 255), 7 }
			};

			aHeightColors = aDefaultHeightColors;
			uNumHeightColors = sizeof(aDefaultHeightColors) / sizeof(aDefaultHeightColors[0]);
			fHeightColorsHeightOrigin = fMinHeight;
			fHeightColorsHeightStep = (fMaxHeight - fMinHeight) / (uNumHeightColors - 1);
			uHeightColorsTransparency = 255;
			bHeightColorsInterpolation = true;
		}
	};

/// \name Create
//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates 2D or 3D viewport.
	///
	/// \param[out]	ppViewport						viewport created.
	/// \param[out]	ppCamera						default viewport camera created.
	/// \param[in]	CreateData						parameters used for the creation of the viewport.
	/// \param[in]	apTerrains						list of terrains to attach to the viewport.
	/// \param[in]	uNumTerrains					number of terrains to attach.
	/// \param[in]	apQuerySecondaryDtmLayers[]		optional array of secondary DTM layers to use for spatial queries lying 
	///												entirely inside a bounding box of one of the secondary DTM layers
	/// \param[in]	uNumQuerySecondaryDtmLayers		number of secondary DTM layers in the above array
	///
	/// \note
	///		ppCamera can be NULL.
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode Create(IMcMapViewport **ppViewport, 
		IMcMapCamera **ppCamera, const IMcMapViewport::SCreateData &CreateData, 
		IMcMapTerrain *const apTerrains[], UINT uNumTerrains,
		IMcDtmMapLayer *const apQuerySecondaryDtmLayers[] = NULL, UINT uNumQuerySecondaryDtmLayers = 0);

	//==============================================================================
	// Method Name: GetCreateParams()
	//------------------------------------------------------------------------------
	/// Retrieves paramters set in Create()
	///
	/// \param[out]	pParams					parameters used for creating the viewport
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCreateParams(IMcMapViewport::SCreateData *pParams) const = 0;
//@}

/// \name Cameras
//@{
	//==============================================================================
	// Method Name: CreateCamera()
	//------------------------------------------------------------------------------
	/// Creates additional camera
	///
	/// \param[out]	ppCamera	camera created
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode CreateCamera(IMcMapCamera **ppCamera) = 0;

	//==============================================================================
	// Method Name: DestroyCamera()
	//------------------------------------------------------------------------------
	/// Destroys camera
	///
	/// \param[in]	pCamera		camera to be destroyed
	///
	/// \note
	///		Active camera cannot be destroyed.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode DestroyCamera(IMcMapCamera *pCamera) = 0;

	//==============================================================================
	// Method Name: SetActiveCamera()
	//------------------------------------------------------------------------------
	/// Sets the viewport's active camera
	///
	/// \param[in]	pCamera		new active camera
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetActiveCamera(IMcMapCamera *pCamera) = 0;

	//==============================================================================
	// Method Name: GetActiveCamera()
	//------------------------------------------------------------------------------
	/// Retrieves the viewport's active camera
	///
	/// \param[out]	ppCamera		current active camera
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetActiveCamera(IMcMapCamera **ppCamera) const = 0;

	//==============================================================================
	// Method Name: GetCameras()
	//------------------------------------------------------------------------------
	/// Retrieves the viewport's cameras
	///
	/// \param[out]	papCameras	array of cameras
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCameras(CMcDataArray<IMcMapCamera*> *papCameras) const = 0;
//@}

/// \name Retrieve Viewport Parameters
//@{
	//==============================================================================
	// Method Name: GetWindowHandle()
	//------------------------------------------------------------------------------
	/// Retrieve the window handle.
	///
	/// \param[out]	phWindowHandle	handle to the window (in Web: HTMLCanvasElement).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetWindowHandle(HWND *phWindowHandle) const = 0;

	//==============================================================================
	// Method Name: SetWindowHandle()
	//------------------------------------------------------------------------------
	/// Sets the window handle.
	///
	/// Relevant only in Android. To replace window handle call once with NULL when disconnecting 
	/// the old window and with a new handle when connecting the new one.
	///
	/// \param[in]	hWindowHandle	handle to the window.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetWindowHandle(HWND hWindowHandle) = 0;

	//==============================================================================
	// Method Name: GetImageCalc()
	//------------------------------------------------------------------------------
	/// Retrieve the image calc attached to the viewport.
	///
	/// \param[out]	ppImageCalc		viewport's image calc.
	/// \param[in]	bStereoSlave	whether secondary image calc (used for slave-eye image in stereo mode) 
	///								should be retrieved; 
	///								the default is false - regular image calc
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetImageCalc(IMcImageCalc **ppImageCalc, bool bStereoSlave = false) const = 0;
//@}

/// \name Viewport Size, Position, Pixel Physical Size, Full Screen Mode
//@{
	//==============================================================================
	// Method Name: ViewportResized()
	//------------------------------------------------------------------------------
	/// Notifies MapCore that the viewport's window has been resized.
	///
	/// Applicable only to a window viewport.
	///
	/// \return
	///     - status result
	//==============================================================================
#ifndef __ANDROID__  // Not for Android
		virtual IMcErrors::ECode ViewportResized() = 0;
#else  // Only for Android
		virtual IMcErrors::ECode ViewportResized(UINT uWidth, UINT uHeight) = 0;
#endif
	//==============================================================================
	// Method Name: GetViewportSize()
	//------------------------------------------------------------------------------
	/// Retrieves the viewport size.
	///
	/// \param[out]	puWidth		viewport width in pixels.
	/// \param[out]	puHeight	viewport height in pixels.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetViewportSize(UINT *puWidth, UINT *puHeight) const = 0;

	//==============================================================================
	// Method Name: SetRelativePositionInWindow()
	//------------------------------------------------------------------------------
	/// Updates viewport position in its window
	///
	/// \param[in]	TopLeftCorner		position of viewport's top-left corner relative to its window; 
	///									values of x and y are between 0 and 1; the default is (0.0, 0.0).
	/// \param[in]	BottomRightCorner	position of viewport's bottom-right corner relative to its window; 
	///									values of x and y are between 0 and 1; the default is (1.0, 1.0).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetRelativePositionInWindow(const SMcFVector2D &TopLeftCorner, const SMcFVector2D &BottomRightCorner) = 0;

	//==============================================================================
	// Method Name: GetRelativePositionInWindow()
	//------------------------------------------------------------------------------
	/// Updates viewport's relative position in its window
	///
	/// \param[out]	pTopLeftCorner		position of viewport's top-left corner relative to its window; 
	///									values of x and y are between 0 and 1.
	/// \param[out]	pBottomRightCorner	position of viewport's bottom-right corner relative to its window; 
	///									values of x and y are between 0 and 1.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRelativePositionInWindow(SMcFVector2D *pTopLeftCorner, SMcFVector2D *pBottomRightCorner) const = 0;

	//==============================================================================
	// Method Name: GetRelativePositionInWindow()
	//------------------------------------------------------------------------------
	/// Retrieves viewport's absolute position in its window
	///
	/// \param[out]	pTopLeftCorner		absolute position of viewport's top-left corner in pixels of its window
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAbsolutePositionInWindow(SMcPoint *pTopLeftCorner) const = 0;

	//==============================================================================
	// Method Name: WindowPixelToViewportPixel()
	//------------------------------------------------------------------------------
	/// Converts pixel coordinates according to window's top-left corner to coordinates according to viewport's top-left corner.
	/// 
	/// Check can be performed whether the pixel is inside viewport's area and Z-order and which viewport (sharing the same window) contains the pixel.
	///
	/// \param[in]	WindowPixel			pixel coordinates according to window's top-left corner
	/// \param[out]	pViewportPixel		pixel coordinates according to viewport's top-left corner; if pPixelViewport != NULL the coordinates are according to *pPixelViewport
	/// \param[out]	pbInViewport		whether the pixel is inside viewport's rctangle and Z-order; can be NULL if unnecessary; the default is NULL
	/// \param[out]	pbInViewportRect	whether the pixel is inside viewport's rectangle regardless of Z-order; can be NULL if unnecessary; the default is NULL
	/// \param[out]	ppPixelViewport		if not NULL and pixel is outside viewport's rectangle or Z-order: the viewport (sharing the same window) that contains the pixel
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode WindowPixelToViewportPixel(const SMcPoint &WindowPixel, SMcPoint *pViewportPixel, bool *pbInViewport, 
		bool *pbInViewportRect = NULL, IMcMapViewport **ppPixelViewport = NULL) = 0;

	//==============================================================================
	// Method Name: SetTopMostZOrderInWindow()
	//------------------------------------------------------------------------------
	/// Changes viewport Z-order making it top-most in its window
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTopMostZOrderInWindow() = 0;

	//==============================================================================
	// Method Name: GetPixelPhysicalHeight()
	//------------------------------------------------------------------------------
	/// Retrieves screen pixel's physical height in meters
	///
	/// \param[out]	pfPixelPhysicalHeightInMeters	physical height of a display pixel in meters 
	/// \param[in]	fDisplayHeightInMeters			physical height of the whole display in meters
	///												(0 means to take it from a display driver)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetPixelPhysicalHeight(float *pfPixelPhysicalHeightInMeters, 
													float fDisplayHeightInMeters = 0) const = 0;

	//==============================================================================
	// Method Name: SetFullScreenMode()
	//------------------------------------------------------------------------------
	/// Switches the viewport's full-screen mode on and off.
	///
	/// \param[in]	bFullScreen	true for full screen, false for regular window
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetFullScreenMode(bool bFullScreen) = 0;

	//==============================================================================
	// Method Name: GetFullScreenMode()
	//------------------------------------------------------------------------------
	/// Retrieves the viewport's full screen mode.
	///
	/// \param[out]	pbFullScreen	current full screen mode.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetFullScreenMode(bool *pbFullScreen) const = 0;
//@}

/// \name Stereo Mode
//@{
	//==============================================================================
	// Method Name: SetStereoMode()
	//------------------------------------------------------------------------------
	/// Sets or the stereo mode in the viewport.
	/// 
	/// Applicable for:
	/// - 3D viewport;
	/// - diagonal-image 2D viewport with SCreateData::pStereoSlaveImageCalc != NULL
	///
	/// \param[in]	eStereoRenderMode	stereo rendering mode to set (or #ESRM_NONE to switch stereo off)
	/// \param[in]	pParams				stereo mode parameters (can be NULL only if \a eStereoRenderMode == #ESRM_NONE)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetStereoMode(EStereoRenderMode eStereoRenderMode, 
		const SStereoParams *pParams) = 0;
	
	//==============================================================================
	// Method Name: GetStereoMode()
	//------------------------------------------------------------------------------
	/// Retrieves the stereo mode and (if stereo is switched on) its parameters.
	/// 
	/// \param[out]	peStereoRenderMode	stereo rendering mode set (or #ESRM_NONE if stereo is switched off)
	/// \param[out]	pParams				stereo mode parameters if stereo is switched on (can be NULL if unnecessary);
	///									the default is NULL
	///							
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetStereoMode(EStereoRenderMode *peStereoRenderMode, 
		SStereoParams *pParams = NULL) const = 0;
//@}

/// \name Terrains And Layers
//@{
	//==============================================================================
	// Method Name: AddTerrain()
	//------------------------------------------------------------------------------
	/// Adds a terrain to the viewport.
	///
	/// \param[in]	pTerrain	terrain to add.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode AddTerrain(IMcMapTerrain *pTerrain) = 0;

	//==============================================================================
	// Method Name: RemoveTerrain()
	//------------------------------------------------------------------------------
	/// Removes a terrain from the viewport.
	///
	/// \param[in]	pTerrain	terrain to remove.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RemoveTerrain(IMcMapTerrain *pTerrain) = 0;

	//==============================================================================
	// Method Name: SetTerrainMaxNumTileRequestsPerRender()
	//------------------------------------------------------------------------------
	/// Sets terrain's maximum number of each layer's HTTP tile requests per render for this viewport.
	///
	/// \param[in]	pTerrain		The terrain to set the maximum for.
	/// \param[in]	uNumTiles		The maximum number of each layer's HTTP tile requests per render (the default 8).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTerrainMaxNumTileRequestsPerRender(IMcMapTerrain *pTerrain, UINT uNumTiles) = 0;

	//==============================================================================
	// Method Name: GetTerrainMaxNumTileRequestsPerRender()
	//------------------------------------------------------------------------------
	/// Retrieves terrain's maximum number of each layer's HTTP tile requests per render for this viewport.
	///
	/// \param[in]	pTerrain		The terrain to retrieve the maximum for.
	/// \param[out]	puNumTiles		The maximum number of each layer's HTTP tile requests per render (the default 8).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTerrainMaxNumTileRequestsPerRender(IMcMapTerrain *pTerrain, UINT *puNumTiles) const = 0;

	//==============================================================================
	// Method Name: SetTerrainMaxNumPendingTileRequests()
	//------------------------------------------------------------------------------
	/// Sets terrain's maximum number of each layer's pending HTTP tile requests per render for this viewport.
	///
	/// \param[in]	pTerrain		The terrain to set the maximum for.
	/// \param[in]	uNumTiles		The maximum number of each layer's pending HTTP tile requests per render (the default 8).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTerrainMaxNumPendingTileRequests(IMcMapTerrain *pTerrain, UINT uNumTiles) = 0;

	//==============================================================================
	// Method Name: GetTerrainMaxNumPendingTileRequests()
	//------------------------------------------------------------------------------
	/// Retrieves terrain's maximum number of each layer's pending HTTP tile requests per render for this viewport.
	///
	/// \param[in]	pTerrain		The terrain to retrieve the maximum for.
	/// \param[out]	puNumTiles		The maximum number of each layer's pending HTTP tile requests per render (the default 8).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTerrainMaxNumPendingTileRequests(IMcMapTerrain *pTerrain, UINT *puNumTiles) const = 0;

	//==============================================================================
	// Method Name: SetTerrainNumCacheTiles()
	//------------------------------------------------------------------------------
	/// Sets terrain's rendering cache size (in tiles of the specified layer type) for this viewport.
	///
	/// Different cache sizes are used for static-objects layers and for other layers.
	///
	/// \param[in]	pTerrain		The terrain to set the cache for.
	/// \param[in]	bStaticObjects	whether cache to be set is for static-objects layers or for other layers.
	/// \param[in]	uNumTiles		The number of tiles in the cache (the default is 150).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTerrainNumCacheTiles(IMcMapTerrain *pTerrain, bool bStaticObjects, UINT uNumTiles) = 0;

	//==============================================================================
	// Method Name: GetTerrainNumCacheTiles()
	//------------------------------------------------------------------------------
	/// Retrieves terrain's rendering cache size (in tiles of the specified layer type) for this viewport.
	///
	/// Different cache sizes are used for static-objects layers and for other layers.
	///
	/// \param[in]	pTerrain		The terrain to retrieve the cache for.
	/// \param[in]	bStaticObjects	whether cache to be retrieved is for static-objects layers or for other layers.
	/// \param[out]	puNumTiles		The number of tiles in the cache (the default is 150).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTerrainNumCacheTiles(IMcMapTerrain *pTerrain, bool bStaticObjects, UINT *puNumTiles) const = 0;

	//==============================================================================
	// Method Name: GetTerrainByID()
	//------------------------------------------------------------------------------
	/// Retrieve a terrain by ID.
	///
	/// \param[in]	uID			The ID of the terrain to retrieve.
	/// \param[out]	ppTerrain	The retrieved terrain.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTerrainByID(UINT uID, IMcMapTerrain **ppTerrain) const = 0;

	//==============================================================================
	// Method Name: SetTerrainDrawPriority()
	//------------------------------------------------------------------------------
	/// Sets the draw priority of a terrain.
	///
	/// \param[in]	pTerrain		terrain to set the draw priority.
	/// \param[in]	nDrawPriority	draw priority to set; the range is –8 to 7;
	///								the default is 0
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTerrainDrawPriority(IMcMapTerrain *pTerrain, 
		signed char nDrawPriority) = 0;

	//==============================================================================
	// Method Name: GetTerrainDrawPriority()
	//------------------------------------------------------------------------------
	/// Retrieves the draw priority of a terrain.
	///
	/// \param[in]	pTerrain		terrain to retrieve the draw priority from.
	/// \param[out]	pnDrawPriority	current draw priority.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTerrainDrawPriority(IMcMapTerrain *pTerrain, 
		signed char *pnDrawPriority) const = 0;

	//==============================================================================
	// Method Name: GetVisibleLayers()
	//------------------------------------------------------------------------------
	/// Retrieves the terrain's all layers currently visible in the viewport.
	///
	/// The result is irrespective to the viewport's current visible area. It is based on 
	/// per-viewport and per-terrain visibility of layers and their scale ranges.
	///
	/// \param[in]	pTerrain		the terrain to which layers belong.
	/// \param[out]	paLayers		array of layers currently visible in the viewport
	///								(irrespectively to the viewport's current visible area).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVisibleLayers(IMcMapTerrain *pTerrain, 
		CMcDataArray<IMcMapLayer*> *paLayers) const = 0;
//@}

/// \name Grid
//@{
	//==============================================================================
	// Method Name: SetGrid()
	//------------------------------------------------------------------------------
	/// Sets the viewport's grid definition
	///
	/// \param[in]	pGrid	grid definition interface; can be NULL
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetGrid(IMcMapGrid *pGrid) = 0;

	//==============================================================================
	// Method Name: GetGrid()
	//------------------------------------------------------------------------------
	/// Retrieves the viewport's grid definition set by SetGrid()
	///
	/// \param[out]	ppGrid	grid definition interface
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetGrid(IMcMapGrid **ppGrid) const = 0;

	//==============================================================================
	// Method Name: SetGridVisibility()
	//------------------------------------------------------------------------------
	/// Sets the visibility of a grid.
	///
	/// \param[in]	bVisible	visibility to set.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetGridVisibility(bool bVisible) = 0;

	//==============================================================================
	// Method Name: GetGridVisibility()
	//------------------------------------------------------------------------------
	/// Retrieves the visibility of a grid.
	///
	/// \param[out]	pbVisible	current visibility.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetGridVisibility(bool *pbVisible) const = 0;

	//==============================================================================
	// Method Name: SetGridAboveVectorLayers()
	//------------------------------------------------------------------------------
	/// Sets the draw priority of a grid (below or above vector layers).
	///
	/// \param[in]	bAboveVector		whether a grid should be drawn above vector layers; the default is false (below vector layers).
	/// 
	/// \note
	/// Lines and polygons of lite native vector layers are always drawn below a grid.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetGridAboveVectorLayers(bool bAboveVector) = 0;

	//==============================================================================
	// Method Name: GetGridAboveVectorLayers()
	//------------------------------------------------------------------------------
	/// Retrieves the draw priority of a grid (below or above vector layers).
	///
	/// \param[out]	pbAboveVector		whether a grid is drawn above vector layers; the default is false (below vector layers).
	///
	/// \note
	/// Lines and polygons of lite native vector layers are always drawn below a grid.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetGridAboveVectorLayers(bool *pbAboveVector) const = 0;
//@}


	/// \name Height Lines
//@{
	//==============================================================================
	// Method Name: SetHeightLines()
	//------------------------------------------------------------------------------
	/// Sets the viewport's height-lines definition
	///
	/// \param[in]	pHeightLines	height-lines definition interface
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetHeightLines(IMcMapHeightLines *pHeightLines) = 0;

	//==============================================================================
	// Method Name: GetHeightLines()
	//------------------------------------------------------------------------------
	/// Retrieves the viewport's height-lines definition set by SetHeightLines()
	///
	/// \param[out]	ppHeightLines	height-lines definition interface
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetHeightLines(IMcMapHeightLines **ppHeightLines) const = 0;

	//==============================================================================
	// Method Name: SetHeightLinesVisibility()
	//------------------------------------------------------------------------------
	/// Sets the visibility of height lines.
	///
	/// \param[in]	bVisible	visibility to set
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetHeightLinesVisibility(bool bVisible) = 0;

	//==============================================================================
	// Method Name: GetHeightLinesVisibility()
	//------------------------------------------------------------------------------
	/// Retrieves the visibility of height lines.
	///
	/// \param[out]	pbVisible	current visibility.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetHeightLinesVisibility(bool *pbVisible) const = 0;
//@}

/// \name DTM Visualization
//@{

	//==============================================================================
	// Method Name: SetDtmVisualization()
	//------------------------------------------------------------------------------
	/// Enables or disables the mode of DTM visualization in areas covered by DTM.
	///
	/// Disabled by default.
	/// 
	/// \param[in]	bEnabled	whether to enable or disable the mode
	/// \param[in]	pParams		DTM visualization parameters; can NULL if \a bEnabled == \a false
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetDtmVisualization(bool bEnabled, 
		const SDtmVisualizationParams *pParams = NULL) = 0;

	//==============================================================================
	// Method Name: GetDtmVisualizationWithoutRaster()
	//------------------------------------------------------------------------------
	/// Retrieves the mode of DTM visualization in areas covered by DTM.
	///
	/// \param[out]	pbEnabled	whether or not the mode is enabled
	/// \param[out]	pParams		DTM visualization parameters (NULL if not needed)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetDtmVisualization(bool *pbEnabled, 
		SDtmVisualizationParams *pParams = NULL) const = 0;

	//==============================================================================
	// Method Name: SetDtmTransparencyWithoutRaster()
	//------------------------------------------------------------------------------
	/// Enables or disables the mode of transparent DTM in areas covered by DTM but not covered 
	/// by any raster layer.
	/// 
	/// Disabled by default.
	/// 
	/// \param[in]	bEnabled	whether to enable or disable the mode
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetDtmTransparencyWithoutRaster(bool bEnabled) = 0;

	//==============================================================================
	// Method Name: GetDtmTransparencyWithoutRaster()
	//------------------------------------------------------------------------------
	/// Retrieves the mode of transparent DTM in areas covered by DTM but not covered 
	/// by any raster layer.
	///
	/// \param[out]	pbEnabled	whether the mode is enabled or disabled
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetDtmTransparencyWithoutRaster(bool *pbEnabled) const = 0;

//@}

/// \name Background Color and Image Processing
//@{
	//==============================================================================
	// Method Name: SetBackgroundColor()
	//------------------------------------------------------------------------------
	/// Sets the viewport's background color.
	///
	/// \param[in]	BackgroundColor		The viewport's background color.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetBackgroundColor(const SMcBColor &BackgroundColor) = 0;

	//==============================================================================
	// Method Name: GetBackgroundColor()
	//------------------------------------------------------------------------------
	/// Retrieves the viewport's background color.
	///
	/// \param[out]	pBackgroundColor		The viewport's background color.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetBackgroundColor(SMcBColor *pBackgroundColor) = 0;

	//==============================================================================
	// Method Name: SetBrightness()
	//------------------------------------------------------------------------------
	/// Sets brightness for each rendering stage (see #EImageProcessingStage).
	///
	/// \param[in]	eStage			Rendering stage to apply brightness at.
	/// \param[in]	fBrightness		Brightness value (from -1 to 1); default is 0.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetBrightness(EImageProcessingStage eStage, float fBrightness) = 0;

	//==============================================================================
	// Method Name: GetBrightness()
	//------------------------------------------------------------------------------
	/// Retrieves brightness for each rendering stage (see #EImageProcessingStage).
	///
	/// \param[in]	eStage			Rendering stage to apply brightness at.
	/// \param[out]	pfBrightness	Brightness value (from -1 to 1); default is 0.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetBrightness(EImageProcessingStage eStage, float *pfBrightness) const = 0;
//@}

	
/// \name Camera Update Callback
//@{
	//==============================================================================
	// Method Name: AddCameraUpdateCallback()
	//------------------------------------------------------------------------------
	/// Adds a callback to be called on active camera update.
	///
	/// Called when either the active camera itself is updated or the viewport's 
	/// active camera is changed
	///
	/// \param[in]	pCallback callback to add.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode AddCameraUpdateCallback(ICameraUpdateCallback *pCallback) = 0;

	//==============================================================================
	// Method Name: RemoveCameraUpdateCallback()
	//------------------------------------------------------------------------------
	/// Removes a callback added by AddCameraUpdateCallback().
	///
	/// \param[in]	pCallback	callback to remove.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RemoveCameraUpdateCallback(ICameraUpdateCallback *pCallback) = 0;
//@}

/// \name Coordinate Conversions
//@{
	//==============================================================================
	// Method Name: OverlayManagerToViewportWorld()
	//------------------------------------------------------------------------------
	/// Converts a world point from overlay manager coordinate system to that of the viewport.
	///
	/// \param[in]	OverlayManagerPoint		overlay manager point to convert.
	/// \param[out]	pViewportPoint			world point converted.
	/// \param[in]	pOverlayManager			overlay manager to use (NULL means using
	///										the viewport's own overlay manager)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode OverlayManagerToViewportWorld(const SMcVector3D &OverlayManagerPoint, 
	  SMcVector3D *pViewportPoint, IMcOverlayManager *pOverlayManager = NULL) const = 0;

	//==============================================================================
	// Method Name: ViewportToOverlayManagerWorld()
	//------------------------------------------------------------------------------
	/// Converts a world point from the viewport coordinate system to that of overlay manager.
	///
	/// \param[in]	ViewportPoint			world point to convert.
	/// \param[out]	pOverlayManagerPoint	overlay manager point converted.
	/// \param[in]	pOverlayManager			overlay manager to use (NULL means using
	///										the viewport's own overlay manager)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ViewportToOverlayManagerWorld(const SMcVector3D &ViewportPoint, 
		SMcVector3D *pOverlayManagerPoint, IMcOverlayManager *pOverlayManager = NULL) const = 0;

	//==============================================================================
	// Method Name: ViewportToOtherViewportWorld()
	//------------------------------------------------------------------------------
	/// Converts a world point from this viewport coordinate system to that of other viewport.
	///
	/// \param[in]	ThisViewportPoint		world point to convert.
	/// \param[out]	pOtherViewportPoint		other viewport point converted.
	/// \param[in]	pOtherViewport			other viewport to convert to.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ViewportToOtherViewportWorld(const SMcVector3D &ThisViewportPoint, 
		SMcVector3D *pOtherViewportPoint, IMcMapViewport *pOtherViewport) const = 0;

	//==============================================================================
	// Method Name: ViewportToImageCalcWorld()
	//------------------------------------------------------------------------------
	/// Converts a world point from this viewport coordinate system to that of the specified image-calc.
	///
	/// \param[in]	ViewportWorldPoint		viewport world point to convert.
	/// \param[out]	pImageWorldPoint		image-calc world point converted.
	/// \param[in]	pImageCalc				image-calc to use
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ViewportToImageCalcWorld(const SMcVector3D &ViewportWorldPoint, 
		SMcVector3D *pImageWorldPoint, const IMcImageCalc *pImageCalc) const = 0;

	//==============================================================================
	// Method Name: ImageCalcWorldToViewport()
	//------------------------------------------------------------------------------
	/// Converts a world point from the specified image-calc coordinate system to that of this viewport.
	///
	/// \param[in]	ImageCalcWorldPoint		image-calc world point to convert.
	/// \param[out]	pViewportWorldPoint		viewport world point converted.
	/// \param[in]	pImageCalc				image-calc to use
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ImageCalcWorldToViewport(const SMcVector3D &ImageCalcWorldPoint, 
		SMcVector3D *pViewportWorldPoint, const IMcImageCalc *pImageCalc) const = 0;
//@}

/// \name Rendering
//@{
	//==============================================================================
	// Method Name: Render()
	//------------------------------------------------------------------------------
	/// Renders the viewport.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Render(void *pBuffer = NULL) = 0;

	virtual IMcErrors::ECode RenderPFrameToBuffer(SPFrameData *pData) = 0;

	//==============================================================================
	// Method Name: RenderFrameToThreadSafeBuffer()
	//------------------------------------------------------------------------------
	/// Renders the viewport's frame to GPU texture that can be copied to memory in any thread.
	///
	/// \param[out]	ppFrameBuffer		frame buffer interface
	/// \param[in]	bReverseRgbOrder	which frame buffer format to use (the default is false):
	///									- false for IMcTexture::EPF_A8R8G8B8
	///									- true for IMcTexture::EPF_A8B8G8R8
	/// \note
	///  - The frame buffer interface should be destroyed after copying.
	///  - There is no need to call SetRenderToBufferMode() to switch on render-to-buffer mode.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RenderFrameToThreadSafeBuffer(IFrameBuffer **ppFrameBuffer, bool bReverseRgbOrder = false) = 0;

	//==============================================================================
	// Method Name: SetRenderToBufferMode()
	//------------------------------------------------------------------------------
	/// Switches on and off render-to-buffer mode that improves the performance of frequent
	/// calls to RenderScreenRectToBuffer().
	/// 
	/// When the mode is switched off, rendering to buffer is still possible but less optimal 
	/// when used frequently. In the other hand, when rendering to buffer is no longer needed, the mode should 
	/// be switched off to improve the performance of regular rendering.
	///
	/// \param[in]	bOn		whether to switch the mode on or off
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetRenderToBufferMode(bool bOn) = 0;

	//==============================================================================
	// Method Name: SetRenderToBufferMode()
	//------------------------------------------------------------------------------
	/// Retrieves render-to-buffer mode.
	/// 
	/// \param[out]	pbOn	whether the mode is switched on or off
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRenderToBufferMode(bool *pbOn) const = 0;

	//==============================================================================
	// Method Name: RenderScreenRectToBuffer()
	//------------------------------------------------------------------------------
	/// Renders the requested screen rectangle into a memory buffer.
	///
	/// \param[in]	Rect				viewport rectangle to render.
	/// \param[in]	uBufferWidth		the buffer width in pixels
	/// \param[in]	uBufferHeight		the buffer height in pixels
	/// \param[in]	eBufferPixelFormat	the buffer pixel format required
	/// \param[in]	uBufferRowPitch		the buffer row pitch i.e. the offset in pixels between
	///									the leftmost pixel of one row and the leftmost pixel of the 
	///									next one (can be != \a uBufferWidth); \a 0 or \a uBufferWidth 
	///									can be passed for consecutive rows without gaps;
	/// \param[out]	aBuffer				memory buffer to render into (should be allocated by the user, 
	///									its size in bytes should match the required width and height 
	///									in pixels, row pitch and pixel format bit count).
	/// \note
	///		Rendering the viewport's whole rectangle into the same size buffer with 
	///		viewport's native pixel format and consecutive rows can improve the performance
	///		(the required parameters can be retrieved by calling GetViewportSize(), 
	///		GetRenderToBufferNativePixelFormat() and IMcTexture::GetPixelFormatByteCount())
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RenderScreenRectToBuffer(const SMcRect &Rect, 
		UINT uBufferWidth, UINT uBufferHeight, IMcTexture::EPixelFormat eBufferPixelFormat, 
		UINT uBufferRowPitch, BYTE aBuffer[]) = 0;

	//==============================================================================
	// Method Name: GetRenderToBufferNativePixelFormat()
	//------------------------------------------------------------------------------
	/// Retrieves render-to-buffer native pixel format and byte count.
	///
	/// Requesting this pixel format in RenderScreenRectToBuffer() can improve the performance.
	///
	/// \param[out]	pePixelFormat		pixel format, see IMcTexture::EPixelFormat;
	///									can be NULL if unnecessary.
	/// \param[out]	puPixelByteCount	pixel byte count (number of byts used by each pixel);
	///									can be NULL if unnecessary.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRenderToBufferNativePixelFormat(
		IMcTexture::EPixelFormat *pePixelFormat, UINT *puPixelByteCount = NULL) = 0;

	//==============================================================================
	// Method Name: RenderRollingShutter()
	//------------------------------------------------------------------------------
	/// Renders rolling-shutter camera frame
	///
	/// \param[in]	RollingShutterData	rolling-shutter camera data
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RenderRollingShutter(const SRollingShutterData &RollingShutterData) = 0;

	//==============================================================================
	// Method Name: RenderRollingShutterRectToBuffer()
	//------------------------------------------------------------------------------
	/// Renders rolling-shutter camera frame into a memory buffer
	///
	/// \param[in]	RollingShutterData	rolling-shutter camera data
	/// \param[in]	Rect				viewport rectangle to render.
	/// \param[in]	uBufferWidth		the buffer width in pixels
	/// \param[in]	uBufferHeight		the buffer height in pixels
	/// \param[in]	eBufferPixelFormat	the buffer pixel format required
	/// \param[in]	uBufferRowPitch		the buffer row pitch i.e. the offset in pixels between
	///									the leftmost pixel of one row and the leftmost pixel of the 
	///									next one (can be != \a uBufferWidth); \a 0 or \a uBufferWidth 
	///									can be passed for consecutive rows without gaps;
	/// \param[out]	aBuffer				memory buffer to render into (should be allocated by the user, 
	///									its size in bytes should match the required width and height 
	///									in pixels, row pitch and pixel format bit count).
	/// \note
	///		Rendering the viewport's whole rectangle into the same size buffer with 
	///		viewport's native pixel format and consecutive rows can improve the performance
	///		(the required parameters can be retrieved by calling GetViewportSize(), 
	///		GetRenderToBufferNativePixelFormat() and IMcTexture::GetPixelFormatByteCount())
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RenderRollingShutterRectToBuffer(
		const SRollingShutterData &RollingShutterData, const SMcRect &Rect, 
		UINT uBufferWidth, UINT uBufferHeight, IMcTexture::EPixelFormat eBufferPixelFormat, 
		UINT uBufferRowPitch, BYTE aBuffer[]) = 0;

	//==============================================================================
	// Method Name: Render()
	//------------------------------------------------------------------------------
	/// Renders a list of viewports.
	///
	/// \param[in]	apViewports		viewports to render.
	/// \param[in]	uNumViewports	number of viewports to render.
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode Render(IMcMapViewport *apViewports[], UINT uNumViewports);

	//==============================================================================
	// Method Name: RenderAll()
	//------------------------------------------------------------------------------
	/// Renders all viewports.
	///
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode RenderAll();

	//==============================================================================
	// Method Name: HasPendingUpdates()
	//------------------------------------------------------------------------------
	/// Queries whether there are any pending updates requiring rendering the viewport.
	///
	/// \param[out]	pbHasPendingUpdates		true if there are pending updates of specified types
	/// \param[in]	uUpdateTypeBitField		types of pending updates to check
	///										(a bit field based on #EPendingUpdateType)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode HasPendingUpdates(bool* pbHasPendingUpdates, 
		UINT uUpdateTypeBitField = EPUT_ANY_UPDATE) const = 0;

	//==============================================================================
	// Method Name: PerformPendingUpdates()
	//------------------------------------------------------------------------------
	/// Performs pending updates of specified types without rendering.
	///
	/// \param[in]	uUpdateTypeBitField		types of pending updates to perform
	///										(a bit field based on #EPendingUpdateType)
	/// \param[in]	uTerrainLoadTimeout		timeout (in ms) for performing terrain-load 
	///										pending updates; the default is 30000
	///
	/// \note	Can be time-consuming, use it carefully.
	///			Recommended usage is application/scene start.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode PerformPendingUpdates(UINT uUpdateTypeBitField = EPUT_ANY_UPDATE,
												   UINT uTerrainLoadTimeout = 30000) = 0;	

	//==============================================================================
	// Method Name: GetRenderStatistics()
	//------------------------------------------------------------------------------
	/// Retrieves last frames rendering statistics.
	///
	/// \param[out]	pStatistics		rendering statistics (parameters except for per-frame ones 
	///								are calculated based on all the frames since the last call to 
	///								ResetRenderStatistics())
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRenderStatistics(SRenderStatistics *pStatistics) const = 0;

	//==============================================================================
	// Method Name: ResetRenderStatistics()
	//------------------------------------------------------------------------------
	/// Resets rendering statistics parameters except for per-frame ones.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ResetRenderStatistics() = 0;

	//==============================================================================
	// Method Name: GetRenderSurface()
	//------------------------------------------------------------------------------
	/// Retrieves render surface that is rendered by the viewport (surface pointer for DirectX, texture ID for GL); 
	/// can be used in non-window viewport only.
	///
	/// The render surface returned will remain valid until a call to 
	/// ResizeRenderSurface() or Destroy().
	///
	/// \param[out]	ppRenderSurface		the render surface (*ppRenderSurface should be cast to 
	///									IDirect3DSurface9* for DirectX, UINT for GL)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRenderSurface(void **ppRenderSurface) const = 0;

	//==============================================================================
	// Method Name: ResizeRenderSurface()
	//------------------------------------------------------------------------------
	/// Resizes non-window viewport returning the new (resized) render surface 
	///  (surface pointer for DirectX, texture ID for GL)
	///
	/// The render surface returned is empty. Call Render() or RenderAll() to fill it.
	///
	/// \param[in]	uNewWidth			the new width in pixels
	/// \param[in]	uNewHeight			the new height in pixels
	/// \param[out]	ppRenderSurface		the render surface (*ppRenderSurface should be cast to 
	///									IDirect3DSurface9* for DirectX, UINT for GL)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ResizeRenderSurface(UINT uNewWidth, UINT uNewHeight, 
												 void **ppRenderSurface) = 0;

	//==============================================================================
	// Method Name: SetTerrainNoiseMode()
	//------------------------------------------------------------------------------
	/// Sets or disables the mode of adding noise to terrain (used to improve short-distance quality).
	///
	/// \param[in]	eNoiseMode		the noise mode (see #ETerrainNoiseMode for details)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTerrainNoiseMode(ETerrainNoiseMode eNoiseMode) = 0;

	//==============================================================================
	// Method Name: GetTerrainNoiseMode()
	//------------------------------------------------------------------------------
	/// Retrieves the mode of adding noise to terrain defined by SetTerrainNoiseMode().
	///
	/// \param[out]	peNoiseMode		the noise mode (see #ETerrainNoiseMode for details)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTerrainNoiseMode(ETerrainNoiseMode *peNoiseMode) const = 0;

	//==============================================================================
	// Method Name: SetShadowMode()
	//------------------------------------------------------------------------------
	/// Sets or disables the shadow mode to be used in the scene.
	///
	/// \param[in]	eShadowMode		the shadow mode (see #EShadowMode for details)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetShadowMode(EShadowMode eShadowMode) = 0;

	//==============================================================================
	// Method Name: GetShadowMode()
	//------------------------------------------------------------------------------
	/// Retrieves the shadow mode defined by SetShadowMode().
	///
	/// \param[out]	peShadowMode		the shadow mode (see #EShadowMode for details)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetShadowMode(EShadowMode *peShadowMode) const = 0;

	/* for internal use */
	virtual IMcErrors::ECode AddTerrainHole(int nTileLOD, UINT uTileX, UINT uTileY) = 0;
	virtual IMcErrors::ECode RemoveTerrainHole(int nTileLOD, UINT uTileX, UINT uTileY) = 0;

	//==============================================================================
	// Method Name: SetMaterialSchemeDefinition()
	//------------------------------------------------------------------------------
	/// For all materials without the specified scheme, defines it by copying the definition from 
	/// another material that has a scheme with the same name.
	///
	/// \param[in]	strMaterialSchemeName		the name of a scheme to define in materials without it
	/// \param[in]	strMaterialNameToCopyFrom	the name of a material to copy the scheme definition from
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetMaterialSchemeDefinition(
		PCSTR strMaterialSchemeName, PCSTR strMaterialNameToCopyFrom) = 0;

	//==============================================================================
	// Method Name: SetMaterialScheme()
	//------------------------------------------------------------------------------
	/// Set the material scheme for the viewport
	///
	/// \param[in]	strMaterialSchemeName		the name of the scheme
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetMaterialScheme(PCSTR strMaterialSchemeName) = 0;

	//==============================================================================
	// Method Name: SetSpecialMaterialParams()
	//------------------------------------------------------------------------------
	/// Sets parameters for the specified GPU program of a pass in a technique in a special material.
	///
	/// \param[in]	strSpecialMaterial	The special material name.
	/// \param[in]	Technique			The material's technique (identified by name or ID)
	/// \param[in]	Pass				The technique's pass (identified by name or ID)
	/// \param[in]	eGpuProgramType		The GPU program type.
	/// \param[in]	aParameters			The parameters to set (the valid types are IMcProperty::EPT_INT, IMcProperty::EPT_UINT, IMcProperty::EPT_FLOAT, IMcProperty::EPT_DOUBLE, 
	///									IMcProperty::EPT_FVECTOR2D, IMcProperty::EPT_VECTOR2D, IMcProperty::EPT_FVECTOR3D, IMcProperty::EPT_VECTOR3D, 
	///									IMcProperty::EPT_BCOLOR, IMcProperty::EPT_FCOLOR, IMcProperty::EPT_INT_ARRAY, IMcProperty::EPT_UINT_ARRAY, IMcProperty::EPT_MATRIX4D).
	/// \param[in]	uNumParameters		The number of parameters in the above array.
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode SCENEMANAGER_API SetSpecialMaterialParams(PCSTR strSpecialMaterial, IMcProperty::SPropertyNameID Technique, IMcProperty::SPropertyNameID Pass,
		EGpuProgramType eGpuProgramType, const IMcProperty::SVariantProperty aParameters[], UINT uNumParameters);

	//==============================================================================
	// Method Name: SetSpecialMaterialTextureName()
	//------------------------------------------------------------------------------
	/// Sets the texture name for the specified special texture unit of a pass in a technique in a special material.
	///
	/// \param[in]	strSpecialMaterial	The special material name.
	/// \param[in]	Technique			The material's technique (identified by name or ID)
	/// \param[in]	Pass				The technique's pass (identified by name or ID)
	/// \param[in]	TextureUnit			The pass's texture unit (identified by name or ID).
	/// \param[in]	strTextureName		The texture name to set.
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode SCENEMANAGER_API SetSpecialMaterialTextureName(PCSTR strSpecialMaterial, IMcProperty::SPropertyNameID Technique, IMcProperty::SPropertyNameID Pass,
		IMcProperty::SPropertyNameID TextureUnit, PCSTR strTextureName);

	//==============================================================================
	// Method Name: SetClipPlanes()
	//------------------------------------------------------------------------------
	/// Sets world-coordinate planes to clip the rendered scene
	///
	/// \param[in]	aClipPlanes		the clipping planes array
	/// \param[in]	uNumClipPlanes	the number of planes in the array
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetClipPlanes(const SMcPlane aClipPlanes[], UINT uNumClipPlanes) = 0;

	//==============================================================================
	// Method Name: SetClipPlanes()
	//------------------------------------------------------------------------------
	/// Retrieves world-coordinate planes to clip the rendered scene
	///
	/// \param[out]	paClipPlanes		the clipping planes array
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetClipPlanes(CMcDataArray<SMcPlane> *paClipPlanes) const = 0;
//@}
		
/// \name Objects: Rendering
//@{
	//================================================================
	// Method Name: SetVector3DExtrusionVisibilityMaxScale()
	//----------------------------------------------------------------
	/// Sets the maximum camera scale for vector 3D extrusion map layer (it will not be visible beyond it).
	/// 
	/// \param[in]	fVector3DExtrusionVisibilityMaxScale		vector 3D extrusion visibility max scale; the default is 10
	///
	/// \return
	///     - status result
	//================================================================
	virtual IMcErrors::ECode SetVector3DExtrusionVisibilityMaxScale(float fVector3DExtrusionVisibilityMaxScale) = 0;

	//================================================================
	// Method Name: GetVector3DExtrusionVisibilityMaxScale()
	//----------------------------------------------------------------
	/// Retrieve the maximum camera scale for vector 3D extrusion map layer (it will not be visible 
	/// beyond it) set by SetVector3DExtrusionVisibilityMaxScale().
	///
	/// \param[out]	pfVector3DExtrusionVisibilityMaxScale		vector 3D extrusion visibility max scale
	///
	/// \return
	///     - status result
	//================================================================
	virtual IMcErrors::ECode GetVector3DExtrusionVisibilityMaxScale(float *pfVector3DExtrusionVisibilityMaxScale) const = 0;

	//================================================================
	// Method Name: Set3DModelVisibilityMaxScale()
	//----------------------------------------------------------------
	/// Sets the maximum camera scale for 3D model map layer (it will not be visible beyond it).
	/// 
	/// \param[in]	f3DModelVisibilityMaxScale		3D model visibility max scale; the default is `FLT_MAX` - no maximum
	///
	/// \return
	///     - status result
	//================================================================
	virtual IMcErrors::ECode Set3DModelVisibilityMaxScale(float f3DModelVisibilityMaxScale) = 0;

	//================================================================
	// Method Name: Get3DModelVisibilityMaxScale()
	//----------------------------------------------------------------
	/// Retrieve the maximum camera scale for 3D model map layer (it will not be visible 
	/// beyond it) set by Set3DModelVisibilityMaxScale().
	///
	/// \param[out]	pf3DModelVisibilityMaxScale		3D model visibility max scale.
	///
	/// \return
	///     - status result
	//================================================================
	virtual IMcErrors::ECode Get3DModelVisibilityMaxScale(float *pf3DModelVisibilityMaxScale) const = 0;

	//================================================================
	// Method Name: SetObjectsVisibilityMaxScale()
	//----------------------------------------------------------------
	/// Sets the maximum camera scale for objects and vector items (they will not be visible beyond it).
	/// 
	/// Does not operate on objects based on screen location points and objects with viewport 
	/// visibility max scale ignored (set by IMcObject::SetIgnoreViewportVisibilityMaxScale()).
	///
	/// \param[in]	fObjectsVisibilityMaxScale		objects visibility max scale.
	///
	/// \return
	///     - status result
	//================================================================
	virtual IMcErrors::ECode SetObjectsVisibilityMaxScale(float fObjectsVisibilityMaxScale) = 0;

	//================================================================
	// Method Name: GetObjectsVisibilityMaxScale()
	//----------------------------------------------------------------
	/// Retrieve the maximum camera scale for objects and vector items (they will not be visible 
	/// beyond it) set by SetObjectsVisibilityMaxScale().
	///
	/// \param[out]	pfObjectsVisibilityMaxScale		objects visibility max scale.
	///
	/// \return
	///     - status result
	//================================================================
	virtual IMcErrors::ECode GetObjectsVisibilityMaxScale(float *pfObjectsVisibilityMaxScale) const = 0;

	//==============================================================================
	// Method Name: SetObjectsMovementThreshold()
	//------------------------------------------------------------------------------
	/// Sets objects movement threshold (in pixels).
	/// 
	/// Movement of 1-point object equal or shorter than the threshold should not update 
	/// its rendered position unless some other object's property updated.
	///
	/// \param[in]	uThresholdInPixels		threshold in pixels; the default is 0 - no threshold
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectsMovementThreshold(UINT uThresholdInPixels) = 0;

	//==============================================================================
	// Method Name: GetObjectsMovementThreshold()
	//------------------------------------------------------------------------------
	/// Retrievs objects movement threshold (in pixels) set by SetObjectsMovementThreshold().
	/// 
	/// \param[out]	puThresholdInPixels		threshold in pixels; the default is 0 - no threshold
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectsMovementThreshold(UINT *puThresholdInPixels) const = 0;

	//==============================================================================
	// Method Name: SetSpatialPartitionNumCacheNodes()
	//------------------------------------------------------------------------------
	/// Sets spatial partition cache size (in nodes) for this viewport.
	///
	/// \param[in]	uNumNodes	The number of nodes in the cache
	///							(the default is 1000).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSpatialPartitionNumCacheNodes(UINT uNumNodes) = 0;

	//==============================================================================
	// Method Name: GetSpatialPartitionNumCacheNodes()
	//------------------------------------------------------------------------------
	/// Retrieves spatial partition cache size (in nodes) for this viewport.
	///
	/// \param[out]	puNumNodes	The number of nodes in the cache
	///							(the default is 1000).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSpatialPartitionNumCacheNodes(UINT *puNumNodes) const = 0;

	//==============================================================================
	// Method Name: SetScreenSizeTerrainObjectsFactor()
	//------------------------------------------------------------------------------
	/// Sets size factor applied to screen-size objects attached to terrain in 3D
	///
	/// Applicable to 3D viewport only.
	/// 
	/// \param[in]	fFactor			size factor; the default is 1
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetScreenSizeTerrainObjectsFactor(float fFactor) = 0;

	//==============================================================================
	// Method Name: GetScreenSizeTerrainObjectsFactor()
	//------------------------------------------------------------------------------
	/// Retrieves size factor applied to screen-size objects attached to terrain in 3D
	///
	/// Applicable to 3D viewport only.
	/// 
	/// \param[out]	pfFactor		size factor; the default is 1
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetScreenSizeTerrainObjectsFactor(float *pfFactor) const = 0;

	//==============================================================================
	// Method Name: SetObjectsDelay()
	//------------------------------------------------------------------------------
	/// Enables/disables objects delays and sets number of objects to update per render cycle.
	///
	/// \param[in]	eDelayType				type of delay to set, see #EObjectDelayType for details
	///										including default values for the following parameters.
	/// \param[in]	bEnabled				state of the delay for the type.
	/// \param[in]	uNumToUpdatePerRender	number of updates per render cycle.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectsDelay(EObjectDelayType eDelayType, bool bEnabled, 
											 UINT uNumToUpdatePerRender) = 0;
	//==============================================================================
	// Method Name: GetObjectsDelay()
	//------------------------------------------------------------------------------
	/// Retrieves the state of objects delays and number of objects to update per render cycle.
	///
	/// \param[in]	eDelayType				type of delay to retrieve, see #EObjectDelayType for details.
	/// \param[out]	pbEnabled				state of the delay for the type.
	/// \param[out]	puNumToUpdatePerRender	number of updates per render cycle.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectsDelay(EObjectDelayType eDelayType, bool *pbEnabled, 
											 UINT *puNumToUpdatePerRender) const = 0;

	//==============================================================================
	// Method Name: SetFreezeObjectsVisualization()
	//------------------------------------------------------------------------------
	/// Sets whether objects' visualization should be temporary frozen to preserve high performance of rendering.
	///
	/// \param[in]	bFreeze		whether objects' visualization should be frozen.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetFreezeObjectsVisualization(bool bFreeze) = 0;

	//==============================================================================
	// Method Name: GetFreezeObjectsVisualization()
	//------------------------------------------------------------------------------
	/// Retrieves whether objects' visualization is temporary frozen to preserve high performance of rendering.
	///
	/// \param[out]	pbFreeze	whether objects' visualization is frozen.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetFreezeObjectsVisualization(bool *pbFreeze) const = 0;

	//==============================================================================
	// Method Name: SetOverloadMode()
	//------------------------------------------------------------------------------
	/// Sets the state and the minimum number of items for overload.
	///
	/// \param[in]	bEnabled						state of the overload mode.
	/// \param[in]	uMinNumItemsForOverload	minimum number of items
	///												for overload to occur.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetOverloadMode(bool bEnabled, UINT uMinNumItemsForOverload) = 0;

	//==============================================================================
	// Method Name: GetOverloadMode()
	//------------------------------------------------------------------------------
	/// Retrieves the state and the minimum number of items for overload.
	///
	/// \param[out]	pbEnabled						state of the overload mode.
	/// \param[out]	puMinNumItemsForOverload	minimum number of items
	///												for overload to occur.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetOverloadMode(bool *pbEnabled, UINT *puMinNumItemsForOverload) const = 0;

	//==============================================================================
	// Method Name: GetVisibleOverlays()
	//------------------------------------------------------------------------------
	/// Retrieves the overlay manager's all overlays currently visible in the viewport.
	///
	/// The result is irrespective to the viewport's current visible area. It is based on the 
	/// visibility options (in all viewports and in this specific viewport) along with the 
	/// conditional selectors of visibility and the visibility of collections the overlays belong to.
	///
	/// \param[out]	paOverlays		array of overlays currently visible in the viewport
	///								(irrespectively to the viewport's current visible area).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVisibleOverlays(CMcDataArray<IMcOverlay*> *paOverlays) const = 0;

	//==============================================================================
	// Method Name: SetOverlaysVisibilityOption(...)
	//------------------------------------------------------------------------------
	/// Sets the visibility option of the overlay manager's overlays specified.
	///
	/// Just calls IMcOverlay::SetVisibilityOption() for each overlay specified.
	///
	/// \param[in]	eVisibility		The visibility option
	///								(the default is IMcConditionalSelector::EAO_USE_SELECTOR)
	/// \param[in]	apOverlays[]	The array of overlays to set a visibility option for.
	/// \param[in]	uNumOverlays	The number of overlays in the above array.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetOverlaysVisibilityOption(IMcConditionalSelector::EActionOptions eVisibility, 
		IMcOverlay *apOverlays[], UINT uNumOverlays) = 0;

	//==============================================================================
	// Method Name: SetObjectsVisibilityOption(...)
	//------------------------------------------------------------------------------
	/// Sets the visibility option of the overlay manager's objects specified.
	///
	/// Just calls IMcObject::SetVisibilityOption() for each object specified.
	///
	/// \param[in]	eVisibility		The visibility option
	///								(the default is IMcConditionalSelector::EAO_USE_SELECTOR)
	/// \param[in]	apObjects[]		The array of objects to set a visibility option for.
	/// \param[in]	uNumObjects		The number of objects in the above array.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetObjectsVisibilityOption(IMcConditionalSelector::EActionOptions eVisibility, 
		IMcObject *apObjects[], UINT uNumObjects) = 0;

	//==============================================================================
	// Method Name: GetObjectsVisibleInWorldRectAndScale2D()
	//------------------------------------------------------------------------------
	/// For 2D regular viewport only: retrieves the overlay manager's all objects that would be visible and 
	/// inside the specified world rectangle (maybe partially) if the current camera is in the specified scale.
	///
	/// The result is irrespective to the viewport's current visible area and camera scale. 
	///
	/// \param[in]	WorldRect		The world rectangle (z-coordinate is ignored)
	/// \param[in]	fCameraScale	The camera scale to check the visibility for
	/// \param[out]	papObjects		The result (array of objects)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObjectsVisibleInWorldRectAndScale2D(const SMcBox &WorldRect, 
		float fCameraScale, CMcDataArray<IMcObject*> *papObjects) = 0;

	//==============================================================================
	// Method Name: GetLocalGeoCorrectionFactor()
	//------------------------------------------------------------------------------
	/// Retrieves a local GEO correction factor applied to the specified object's calculations.
	///
	/// Returns a correction factor != (1, 1) and a factor-needed flag == true only if the viewport's 
	/// coordinate system is geographic and SCreateData::bShowGeoInMetricProportion == true.
	///
	/// \param[in]	pObject						The object to retrieve the correction factor for.
	/// \param[in]	eGeometryType				The type of calculation to retrieve the correction factor for.
	/// \param[out]	pCorrectionFactor			The local correction factor for the specified object.
	/// \param[out]	pbCorrectionFactorNeeded	Whether the local correction factor should be applied; 
	///											false means it is equal to (1, 1); can be NULL.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLocalGeoCorrectionFactor(const IMcObject *pObject, 
		IMcObjectSchemeItem::EGeometryType eGeometryType, 
		SMcFVector2D *pCorrectionFactor, bool *pbCorrectionFactorNeeded = NULL) = 0;

	//==============================================================================
	// Method Name: GetLocalGeoCorrectionFactor()
	//------------------------------------------------------------------------------
	/// Retrieves a local GEO correction factor applied in the specified world point.
	///
	/// Returns a correction factor != (1, 1) and a factor-needed flag == true only if the viewport's 
	/// coordinate system is geographic and SCreateData::bShowGeoInMetricProportion == true.
	///
	/// \param[in]	WorldPoint					The world point to retrieve the correction factor for.
	/// \param[in]	eGeometryType				The type of calculation to retrieve the correction factor for.
	/// \param[out]	pCorrectionFactor			The local correction factor for the specified object.
	/// \param[out]	pbCorrectionFactorNeeded	Whether the local correction factor should be applied; 
	///											false means it is equal to (1, 1); can be NULL.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLocalGeoCorrectionFactor(const SMcVector3D &WorldPoint, 
		IMcObjectSchemeItem::EGeometryType eGeometryType, 
		SMcFVector2D *pCorrectionFactor, bool *pbCorrectionFactorNeeded = NULL) = 0;

	//==============================================================================
	// Method Name: SetOneBitAlphaMode()
	//------------------------------------------------------------------------------
	/// Enables (and defines) or disables 1-bit alpha mode used in rendering symbolic objects.
	/// 
	/// When the mode is enabled (\a uAlphaRejectValue > 0) semi-transparent pixels are 
	/// considered:
	/// - opaque if their color's alpha is greater than \a uAlphaRejectValue, 
	/// - transparent otherwise.
	///	  
	/// \param[in]	uAlphaRejectValue	alpha reject value (or 0 to disable the mode);
	///									the default is 0 - disabled.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetOneBitAlphaMode(BYTE uAlphaRejectValue) = 0;

	//==============================================================================
	// Method Name: GetOneBitAlphaMode()
	//------------------------------------------------------------------------------
	/// Retrieves 1-bit alpha mode used in rendering symbolic objects set by SetOneBitAlphaMode().
	/// 
	/// \param[out]	puAlphaRejectValue	alpha reject value (or 0 if the mode is disabled);
	///									the default is 0 - disabled.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetOneBitAlphaMode(BYTE *puAlphaRejectValue) const = 0;

	//==============================================================================
	// Method Name: SetTransparencyOrderingMode()
	//------------------------------------------------------------------------------
	/// Sets transparency ordering mode for semi-transparent object scheme items.
	/// 
	/// Applicable only to items of regular draw-priority group based on world coordinates.
	///
	/// \param[in]	bEnabled	whether the rendering order of semi-transparent object scheme items should be considered for correct colors of pixels 
	///							in overlapping areas; the default is false (disable).
	/// \note 
	/// - Better performance is achieved when the mode is disabled (in this case colors of pixels in overlapping areas may be incorrect).
	/// - The mode cannot be enabled in old GPUs without the appropriate support (e.g. without OpenGL ES3 support in Android, without WebGL2 support in Web).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTransparencyOrderingMode(bool bEnabled) = 0;

	//==============================================================================
	// Method Name: GetTransparencyOrderingMode()
	//------------------------------------------------------------------------------
	/// Retrieves transparency ordering mode for semi-transparent object scheme items set by SetTransparencyOrderingMode().
	///
	/// \param[out]	pbEnabled	whether the rendering order of semi-transparent object scheme items is considered for correct colors of pixels 
	///							in overlapping areas; the default is true (enabled).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTransparencyOrderingMode(bool *pbEnabled) const = 0;

	//==============================================================================
	// Method Name: SetSoundListener()
	//------------------------------------------------------------------------------
	/// Enables playing sound items in this viewport and disables in all other viewports.
	/// 
	/// \note
	///		- Sound items can be simultaneously played in no more than one viewport. 
	///		- By default, playing sound items is disabled in all viewports.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSoundListener() = 0;

	//==============================================================================
	// Method Name: IsSoundListener()
	//------------------------------------------------------------------------------
	/// Retrieves whether the viewport is defined as sound listener by SetSoundListener()
	///
	/// \param[out]	pbSoundListener		whether the viewport is defined as sound listener
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode IsSoundListener(bool *pbSoundListener) const = 0;

//@}

/// \name Post Process
//@{
	//==============================================================================
	// Method Name: AddPostProcess()
	//------------------------------------------------------------------------------
	/// Adds a post process.
	///
	/// \param[in]	strPostProcess	name of the post process to add.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode AddPostProcess(PCSTR strPostProcess) = 0;

	//==============================================================================
	// Method Name: RemovePostProcess()
	//------------------------------------------------------------------------------
	/// Removes a post process.
	///
	/// \param[in]	strPostProcess	name of the post process to remove.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RemovePostProcess(PCSTR strPostProcess) = 0;

	//==============================================================================
	// Method Name: GetPostProcessResultingTextureName()
	//------------------------------------------------------------------------------
	/// Retrieves The name of a post process's resulting texture.
	///
	/// \param[in]	strPostProcess				name of the post process
	/// \param[in]	strPostProcessTarget		name of the post process's target
	/// \param[in]	uTargetIndex				target index (relevant only in case of multi-render-target post process)
	/// \param[out]	pstrResultingTextureName	name of the post process's resulting texture
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetPostProcessResultingTextureName(PCSTR strPostProcess, PCSTR strPostProcessTarget, UINT uTargetIndex, 
		PCSTR *pstrResultingTextureName) const = 0;

//@}

/// \name Frame Time
//@{
	//==============================================================================
	// Method Name: SetNextFrameDeltaTime()
	//------------------------------------------------------------------------------
	/// Sets the time that has passed since rendering the previous frame.
	///
	/// The function should be called once before Render() or RenderAll(). If it is not called, 
	/// a system time will be taken by MapCore when rendering a frame is started.
	///
	/// \param[in]	fTimeSinceLastFrame		delta time in milliseconds.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetNextFrameDeltaTime(float fTimeSinceLastFrame) = 0;
//@}
};
