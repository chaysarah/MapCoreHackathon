#pragma once
//===========================================================================
/// \file IMcMapProduction.h
/// Interface for MapCore-format map layers production
//===========================================================================

#include "IMcDestroyable.h"
#include "McCommonTypes.h"
#include "SMcColor.h"
#include "SMcSizePointRect.h"
#include "IMcErrors.h"
#include "CMcDataArray.h"
#include "OverlayManager/IMcOverlayManager.h"
#include "OverlayManager/IMcObject.h"
#include "OverlayManager/IMcEllipseItem.h"
#include "Map/IMcVectorMapLayer.h"
#include "Map/IMcHeatMapViewport.h"
#include "Map/IMcStaticObjectsMapLayer.h"
#include "Calculations/IMcGridCoordinateSystem.h"
#include "Calculations/IMcSpatialQueries.h"

/// The no-value constant for floating point (float and double) variables 
/// (used in IMcMapProduction::SSourceFileParams)
/// \hideinitializer
#define MC_SOURCE_FILE_PARAM_NO_VALUE			(FLT_MAX)

/// The source highest resolution constant used in IMcMapProduction::SConvertParams::fTargetHighestResolution and 
/// IMcMapProduction::SHeatMapConvertParams::fTargetLowestResolution 
/// \hideinitializer
#define MC_RESOLUTION_SOURCE					(FLT_MAX)

const PCSTR MC_XML_DEFAULT_VALUE = "McDefaultValue";

//===========================================================================
// Interface Name: IMcMapProduction
//---------------------------------------------------------------------------
/// Interface for MapCore-format map layers production
//===========================================================================
class IMcMapProduction : public IMcDestroyable
{
protected:

	virtual ~IMcMapProduction() {};

public:

	struct SSourceFileParams;
	
	/// Comparison operator
	enum EComparisonOperator
	{
		ECO_EQUAL,				///< ==
		ECO_NOT_EQUAL,			///< !=
		ECO_LOWER_THAN,			///< <
		ECO_LOWER_EQUAL_THAN,	///< <=
		ECO_GREATER_THAN,		///< >
		ECO_GREATER_EQUAL_THAN,	///< >=
		ECO_BETWEEN,			///< < x <
		ECO_SUB_STRING,			///< *s*
		ECO_SQL,				///< evaluate SQL query
		ECO_ALWAYS,				///< always true
		ECO_ELSE,				///< true if none of the previous conditions was satisfied
		ECO_EXISTING,			///< true if the field exists in the source data
		ECO_NUM					///< number of the enum's members (not used as a valid character set)
	};

	enum EBoolOperator
	{
		EBO_AND,
		EBO_OR,
		EBO_NOT,
		EBO_NUM
	};
	
	/// Callback for receiving user-provided source data
	class ISourceDataCallback
#ifdef __EMSCRIPTEN__
		: public IMcEmscriptenCallbackRef
#endif
	{
	public:
		virtual ~ISourceDataCallback() {}

		//==============================================================================
		// Method Name: ProvideSourceDtmData()
		//------------------------------------------------------------------------------
		/// Callback function used to receive user-provided source raster data
		///
		/// \param[in]	uSourceIndex	source raster index
		/// \param[out]	aRasterBuffer	buffer for user-provided source raster data
		///								(already allocated and sufficient for the required source)
		//==============================================================================
		virtual void ProvideSourceRasterData(UINT uSourceIndex, BYTE aRasterBuffer[]) = 0;

		//==============================================================================
		// Method Name: ProvideSourceDtmData()
		//------------------------------------------------------------------------------
		/// Callback function used to receive user-provided source DTM data
		///
		/// \param[in]	uSourceIndex	source DTM index
		/// \param[out]	afDtmBuffer		buffer for user-provided source DTM data
		///								(already allocated and sufficient for the required source)
		//==============================================================================
		virtual void ProvideSourceDtmData(UINT uSourceIndex, float afDtmBuffer[]) = 0;

		virtual UINT GetNumSourceStaticObjects() = 0;

		virtual void GetSourceStaticObjectDataSize(UINT uObjectIndex, 
			UINT *puNumPoints, UINT *puNumConnectionIndices, UINT *puNumResources, UINT *puNumPathsToSerachForResources, 
			bool *pbPointsColorsUsed, bool *pbPointsNormalsUsed, bool *pbResourcesAreTextureFiles) = 0;

		virtual void ProvideSourceStaticObjectData(UINT uObjectIndex, 
			SMcVector3D aPointsCoordinates[],
			SMcFVector2D aPointsTextureCoordinates[],
			SMcBColor aPointsColors[],
			SMcVector3D aPointsNormals[],
			UINT auConnectionIndices[],
			char astrResourcesNames[][MAX_PATH],
			UINT auResourcesStartIndices[],
			char astrPathsToSerachForResources[][MAX_PATH]) = 0;

		//==============================================================================
		// Method Name: Release()
		//------------------------------------------------------------------------------
		/// A callback that should release callback class instance.
		///
		///	Can be implemented by the user to optionally delete callback class instance when 
		/// IMcMapProduction instance is been removed.
		//==============================================================================
		virtual void Release() {}
	};

	/// interface for rater or DTM matrix provided tile by tile
	class ITiledSourceMatrix : public IMcDestroyable
	{
	protected:
		virtual ~ITiledSourceMatrix() {};

	public:
		//==============================================================================
		// Method Name: GetTilesDistribution()
		//------------------------------------------------------------------------------
		/// Retrieves tiles distribution parameters
		///
		/// \param[out]	puNumTilesX			number of tiles along X axis
		/// \param[out]	puNumTilesY			number of tiles along Y axis
		/// \param[out]	puTilePointsX		number of points in each tile along X axis
		/// \param[out]	puTilePointsY		number of points in each tile along Y axis
		/// \param[out]	puLastTilePointsX	number of points in the last tile along X axis
		/// \param[out]	puLastTilePointsY	number of points in the last tile along Y axis
		/// \param[out]	paSources			optional array of source parameters to be passed to 
		///									IMcMapProduction::ConvertDtmLayer();
		///									can be NULL (the default) if unnecessary
		///
		/// \note each pair of consecutive tiles overlap by one line of points
		/// 
		/// \return
		///     - status result
		//==============================================================================
		virtual IMcErrors::ECode GetTilesDistribution(UINT *puNumTilesX, UINT *puNumTilesY, 
			UINT *puTilePointsX, UINT *puTilePointsY, UINT *puLastTilePointsX, UINT *puLastTilePointsY,
			CMcDataArray<IMcMapProduction::SSourceFileParams> *paSources = NULL) const = 0;

		//==============================================================================
		// Method Name: ProvideRasterTile()
		//------------------------------------------------------------------------------
		/// Provides raster data of one tile
		///
		/// \param[in]	uTileIndexX		tile index along X axis
		/// \param[in]	uTileIndexY		tile index along Y axis
		/// \param[out]	aRasterBuffer	buffer for raster data (already allocated and sufficient for the tile data)
		//==============================================================================
		virtual IMcErrors::ECode ProvideRasterTile(UINT uTileIndexX, UINT uTileIndexY, BYTE aRasterBuffer[]) = 0;

		//==============================================================================
		// Method Name: ProvideDtmTile()
		//------------------------------------------------------------------------------
		/// Provides DTM data of one tile
		///
		/// \param[in]	uTileIndexX		tile index along X axis
		/// \param[in]	uTileIndexY		tile index along Y axis
		/// \param[out]	afDtmBuffer		buffer for DTM data (already allocated and sufficient for the tile data)
		//==============================================================================
		virtual IMcErrors::ECode ProvideDtmTile(UINT uTileIndexX, UINT uTileIndexY, float afDtmBuffer[]) = 0;
	};

	/// Flags describing which parameters in IMcMapProduction::SSourceFileParams are invalid
	enum ESourceParamsValidityFlags
	{
		ESPVF_NONE				= 0x0000, ///< nothing invalid
		ESPVF_INVALID_X_LIMITS	= 0x0001, ///< \p fMinX, \p fMaxX are invalid or do not match \p fResolution
		ESPVF_INVALID_Y_LIMITS	= 0x0002, ///< \p fMinY, \p fMaxY are invalid or do not match \p fResolution
		ESPVF_INVALID_RESOLUTION= 0x0004, ///< \p fResolution is invalid
	} ;

	/// Raster compression type
	enum ERasterCompression
	{
		/// DXT1 compression: gives the best performance, most video cards support it
		ERC_DXT1,

		/// JPEG compression: recommended only if video card does not support DXT1 compression 
		/// or when the raster layer is to be used as JPEG tiles server
		ERC_JPEG,
		
		/// PNG compression: recommended only if video card does not support DXT1 compression
		/// or when the raster layer is to be used as PNG tiles server
		ERC_PNG,

		/// PVR compression: gives the best performance for mobile systems
		ERC_PVR,

		/// ETC1 compression: gives the best performance for mobile systems
		ERC_ETC1,

		/// no compression: not recommended
		ERC_NONE
	} ;

	/// DXT compression method
	enum EDXTCompressMethod
	{
		EDCM_DIRECTX_TEX			= 0,	///< with Microsoft DirectX Texture Library (DirectXTex)
		EDCM_SQUISH_BEST_SLOWEST	= 1,	///< with CPU-based squish library (best quality with slowest method)
		EDCM_SQUISH_GOOD_SLOW		= 2,	///< with CPU-based squish library (good quality with slow method)
		EDCM_SQUISH_BAD_FAST		= 3,	///< with CPU-based squish library (bad quality with fast method)
	};

	/// Raster compression quality for ERasterCompression::ERC_JPEG, ERasterCompression::ERC_PVR, ERasterCompression::ERC_ETC1 compressions only
	enum ERasterCompressionQuality
	{
		ERCQ_SUPERB		= 0,	///< superb quality
		ERCQ_GOOD		= 1,	///< good quality
		ERCQ_NORMAL		= 2,	///< normal quality
		ERCQ_AVERAGE	= 3,	///< average quality	(for ERasterCompression::ERC_PVR compression the same as ERasterCompression::ERCQ_BAD)
		ERCQ_BAD		= 4		///< bad quality	(for ERasterCompression::ERC_PVR compression the same as ERasterCompression::ERCQ_AVERAGE)
	};

	/// What to do when source raster overlap or when rasters are added to existing layer
	enum ERasterOverlapMode
	{
		EROM_OVERRIDE_ALWAYS,            ///< the new raster always overrides the existing one
		EROM_OVERRIDE_IF_NEW_NOT_TRANSP, ///< each non-transparent pixel of the new raster overrides an existing pixel
		EROM_OVERRIDE_IF_EXISTING_TRANSP ///< each existing transparent pixel is overridden by a pixel of the new raster
	} ;

	/// Output of static objects production
	enum EStaticObjectsOutput
	{
		ESOO_NATIVE_MAP_LAYER,	///< MapCore's native map layer
		ESOO_TILE_MESHES,		///< meshes (a mesh per tile)
		ESOO_SINGLE_MESH		///< one single mesh
	};

	/// Formats to export static-objects layer
	enum EStaticObjectsExportFormat
	{
		ESOEF_VRML,			///< VRML file
		ESOEF_TILE_MESHES	///< meshes (a mesh per tile)
	};

	/// Sorting of source raster or DTM files used in BuildSourceRasterOrDtmFilesList()
	enum ESourceSorting
	{
		ESS_NONE,								///< no sorting
		ESS_RESOLUTION_FROM_LOW_TO_HIGH,		///< according to resolution: from low to high 
		ESS_RESOLUTION_FROM_HIGH_TO_LOW,		///< according to resolution: from high to low 
		ESS_ALPHABET_ASCENDING,					///< according to alphabet: ascending order 
		ESS_ALPHABET_DESCENDING					///< according to alphabet: descending order
	};

	/// MapCore version compatibility
	enum EVersionCompatibility
	{
		EVC_LATEST_VERSION			= 0,		///< compatible to all versions starting with 7.9.6
		EVC_LATEST_AND_PREV_VERSION	= 1,		///< prev version is currently: EVC_7_9_5_VERSION
		EVC_7_5_X_VERSION			= 2,		///< for raster/DTM: compatible to version 7.5.X and older
												///<   (newer MapCore versions can work with it, but with performance penalty)
		EVC_7_8_1_STATIC_OBJECTS	= 3,		///< for static-objects: compatible to version 7.9.5 and older starting with 7.8.1
		EVC_7_8_4_STATIC_OBJECTS	= 4,		///< for static-objects: compatible to version 7.9.5 and older starting with 7.8.4
		EVC_7_9_5_VERSION			= 5,		///< compatible to version 7.9.5 and older starting with:
												///<  - 7.8.4 for static-objects,
												///<  - 7.6.0 for raster/DTM.
		EVC_7_11_2_STATIC_OBJECTS	= 6,		///< for static-objects: compatible to version 7.11.2 and older starting with 7.9.6
		EVC_7_11_3_STATIC_OBJECTS   = 7,		///< for static-objects: compatible to version 7.11.3
	};

	struct SFrameData
	{
		char strFrameDepthMapFullFileName[MAX_PATH];
		char strFrameFullFileName[MAX_PATH];

		SMcFVector2D CameraCenterOffset;
		SMcVector3D CameraPos;
		SMcRotation CameraRotation;
		float fCameraFieldOfViewHorizAngle;

		UINT uWidth;
		UINT uHeight;

		SFrameData()  { memset(this, 0, sizeof(*this));  }
	};

	 /// Source raster/DTM parameters (see detailed description below)
	 struct SSourceFileParams 
	 {
		double fMinX;					///< minimal x limit (west)
		double fMinY;					///< minimal y limit (south)
		double fMaxX;					///< maximal x limit (east)
		double fMaxY;					///< maximal y limit (north)
		double fXRes;					///< source x resolution
		double fYRes;					///< source y resolution
		double dTargetMinRes;			///< target min resolution
		double dTargetMaxRes;			///< target max resolution
		char  strPathName[MAX_PATH];	///< source file name including full path (for native raster layer: Raster.bin); 
										///<  for DTM only: can be empty if source data callback 
										///<  is used to provide source data
		SMcVector3D MeshPosition;		///< mesh position (for mesh only)
		float fMeshYaw;					///< mesh yaw angle (for mesh only)
		float fMeshPitch;				///< mesh pitch angle (for mesh only)
		float fMeshRoll;				///< mesh roll angle (for mesh only)
		SSourceFileParams()  { memset(this, 0, sizeof(*this));  }
	 };

	/// Conversion parameters
	struct SConvertParams
	{
		const SSourceFileParams *aSources;				///< Array of source files with their parameters
		UINT uNumSources;								///< Number of files in \b aSources
		const SMcBox *aClipRects;						///< optional clipping rectangles for sources
		UINT uNumClipRects;								///< Number of clipping rectangle in \b aClipRects
		IMcGridCoordinateSystem *pSourceCoordSys;		///< source files' coordinate system
		IMcGridCoordinateSystem *pDestCoordSys;			///< destination coordinate system if 
														///<  reprojection is required or NULL if not
		const IMcMapLayer::STilingScheme *pTilingScheme;///< destination tiling scheme; default is NULL (means MapCore scheme)
		PCSTR strDestDir;								///< convert destination directory
		float fTargetHighestResolution;					///< The target highest resolution (or #MC_RESOLUTION_SOURCE meaning source highest resolution); 
														///<  the value will be rounded to the nearest valid resolution of the tiling scheme (\p pTilingScheme).
														///<  the default in case of 3DModel will be 0.05 
														///<  and otherwise #MC_RESOLUTION_SOURCE.
		float fReprojectionPrecision;					///< reprojection precision (relevant \b pDestCoordSys is 
														///<  not NULL and not equal to \b pSourceCoordSys)
		EVersionCompatibility eVersionCompatibility;	///< version compatibility
		UINT uNumTilesInFileEdge;						///< number of tiles in each output file edge (\b uNumTilesInFileEdge x \b uNumTilesInFileEdge tiles in file);
														///<  0 means 8 x 8 tiles (the default)
		bool bOneResolutionOnly;						///< whether to build one resolution only instead of the whole 
														///<  pyramid of resolutions (suitable only if the map is to be 
														///<  be shown in a scale equal to the target highest resolution) 
		bool  bUseRecoveryInfo;							///< whether to use recovery info and continue the conversion from the point it stopped
														///<  (for raster layer only)
		bool bAllowRasterUpSampling;					///< whether to use fTargetHighestResolution even when it is higher than 
														///<  sources' highest resolution (for raster layer only)

		SConvertParams(PCSTR strDestDirectory = NULL, const SSourceFileParams aFiles[] = NULL, 
					   UINT uNumFiles = 0, IMcGridCoordinateSystem *_pSourceCoordSys = NULL,
					   bool _bUseRecoveryInfo = false)
		{
			SetDefaults(strDestDirectory, aFiles, uNumFiles, pSourceCoordSys, _bUseRecoveryInfo);
		}

		void SetDefaults(PCSTR strDestDirectory, const SSourceFileParams aFiles[], 
			             UINT uNumFiles, IMcGridCoordinateSystem *_pSourceCoordSys,
			             bool _bUseRecoveryInfo = false)
		{
			memset(this, 0, sizeof(*this));
			strDestDir					= strDestDirectory;
			aSources					= aFiles;
			uNumSources					= uNumFiles;
			pSourceCoordSys				= _pSourceCoordSys;
			fTargetHighestResolution	= MC_RESOLUTION_SOURCE;
			//pDestCoordSys				= NULL;
			//pTilingScheme				= MULL;
			fReprojectionPrecision		= 508;
			//bOneResolutionOnly		= false;
			eVersionCompatibility		= EVC_LATEST_VERSION;
			//uNumTilesInFileEdge		= 0;
			bUseRecoveryInfo			= _bUseRecoveryInfo;
			//bAllowRasterUpSampling	= false;
		}
	};

	/// Raster conversion params
	struct SRasterConvertParams : SConvertParams
	{
		ERasterCompression	eCompression;				///< compression type; the default is ERasterCompression::ERC_DXT1;
														///< don't use other values unless there are good 
														///< reasons (see #ERasterCompression for details)
		EDXTCompressMethod eDXTCompressMethod;			///< DXT compression method (for ERasterCompression::ERC_DXT1 compression only)
														///< the default is #EDCM_DIRECTX_TEX
		ERasterCompressionQuality eCompressionQuality;	///< compression quality for ERasterCompression::ERC_JPEG, ERasterCompression::ERC_PVR, ERasterCompression::ERC_ETC1 compressions; 
														///< the default is ERasterCompression::ERCQ_NORMAL
		UINT				uNumPngColors;				///< number of colors to use (for ERasterCompression::ERC_PNG compression only); 
														///< can be 0 (no limit) or from 2 to 255; the default is 15
		ERasterOverlapMode  eOverlapMode;				///< raster overlap mode
		SMcBColor			TransparentColor;			///< Color of transparent pixel 
		BYTE                byTransparentColorPrecision;///< Transparent color precision for each channel (R, G, B)
		bool                bGrayscale;                 ///< If the target should be grayscale
		bool				bImageCoordinateSystem;		///< If it is an image without coordinate system
		UINT				uNumChannelsForMultiLayers; ///< number of channels in case convert should create a layer per channel.
														///< default is 0 (=a single layer will be created).

		SRasterConvertParams(PCSTR strDestDirectory = NULL, const SSourceFileParams aFiles[] = NULL,
			                 UINT uNumFiles = 0, IMcGridCoordinateSystem *_pSourceCoordSys = NULL,
							 bool _bUseRecoveryInfo = true)
		{ 
			SetDefaults(strDestDirectory, aFiles, uNumFiles, _pSourceCoordSys, _bUseRecoveryInfo);
		}
		
		void SetDefaults(PCSTR strDestDirectory, const SSourceFileParams aFiles[], 
			UINT uNumFiles, IMcGridCoordinateSystem *_pSourceCoordSys,
			bool _bUseRecoveryInfo = true)
		 {
			 SConvertParams::SetDefaults(strDestDirectory, aFiles, uNumFiles, _pSourceCoordSys, _bUseRecoveryInfo);
			 bGrayscale = false;
			 eCompression = ERC_DXT1;
			 eCompressionQuality = ERCQ_NORMAL;
			 eDXTCompressMethod = EDCM_DIRECTX_TEX;
			 uNumPngColors = 15;
			 eOverlapMode = EROM_OVERRIDE_ALWAYS;
			 TransparentColor = bcBlackTransparent;
			 byTransparentColorPrecision = 0;
			 bImageCoordinateSystem = false;
			 uNumChannelsForMultiLayers = 0;
		 }
	};

	/// parameters for building raster layer from frames projected onto DTM
	struct SRasterFromFramesParams : SRasterConvertParams
	{
		PCSTR strSrcDtmDirectory;
		bool bIsRasterOrthoView;
		const IMcMapProduction::SFrameData *aFramesArray;
		UINT uNumFrames;

		SRasterFromFramesParams(
			PCSTR _strDestDirectory = NULL, bool _bIsRasterOrthoView = true,
			IMcMapProduction::SFrameData _aFramesArray[] = NULL, UINT _uNumFrames = 0,
			PCSTR _strSrcDtmDirectory = NULL, IMcGridCoordinateSystem *_pSourceCoordSys = NULL)
		{ 
			SRasterConvertParams::SetDefaults(_strDestDirectory, NULL, 0, _pSourceCoordSys);

			strSrcDtmDirectory = _strSrcDtmDirectory;
			bIsRasterOrthoView = _bIsRasterOrthoView;
			aFramesArray = _aFramesArray;
			uNumFrames = _uNumFrames;
		}
	};

	/// DTM conversion params
	struct SDtmConvertParams: SConvertParams
	{
		UINT	uNumSmoothingLevels;	///< Number of times to halve source DTM grid step for smoothing,
										///<  new grid step will be smaller by 2 in power of uNumSmoothingLevels;
										///<  if source DTM resolution is high enough, 0 should be specified; 
		bool	bFillNoHeightAreas;		///< whether no-height areas (holes inside source files and parts of 
										///<  converted tiles partially covered by source files) should be filled by 
										///<  synthesized values by smooth continuation beyond valid-height areas

		bool	bIsDtmOrthoView;		///< Whether or not to create the Dtm layer with non-ortho-view data

		SDtmConvertParams(PCSTR strDestDirectory = NULL, const SSourceFileParams aFiles[] = NULL,
						  UINT uNumFiles = 0, IMcGridCoordinateSystem *_pSourceCoordSys = NULL)
		{
			 SetDefaults(strDestDirectory, aFiles, uNumFiles, _pSourceCoordSys);
		}
		
		void SetDefaults(PCSTR strDestDirectory, const SSourceFileParams aFiles[],
						UINT uNumFiles, IMcGridCoordinateSystem *_pSourceCoordSys)
		{
			 SConvertParams::SetDefaults(strDestDirectory, aFiles, uNumFiles, _pSourceCoordSys);
			 uNumSmoothingLevels = 0;
			 bFillNoHeightAreas = false;
			 bIsDtmOrthoView = true;
		 }
	};

	/// Heat map conversion params
	struct SHeatMapConvertParams : SConvertParams
	{
		const IMcHeatMapViewport::SHeatMapPoint *aPoints;	///< Array of heat map world points with their parameters
		UINT uNumPoints;					///< Number of points in \b aPoints
		UINT eItemType;						///< Type of Item
		UINT uItemInfluenceRadius;			///< Radius of heat map item
		bool bIsRadiusInPixels;				///< Whether heat map item radius in pixels or in map units
											///< Note: in case radius is in pixels, independent items may overlap when zooming out
		bool bIsGradient;					///< Whether gradient fading should be used
		bool bGPUBased;						///< Whether convert is done using GPU
		float fTargetLowestResolution;		///< The target's lowest resolution(or #MC_RESOLUTION_SOURCE meaning to calculate it automatically); 
											///<  the value will be rounded to the nearest valid resolution of the tiling scheme (\p pTilingScheme).
											///<  the default is #MC_RESOLUTION_SOURCE.
		bool bCalcAveragePerPoint;			///< Default is false, meaning calculation of sum per point
		double dMinValThreshold;			///< if pre-normalized pixel value is below this value, set it to dMinVal
		double dMaxValThreshold;			///< if pre-normalized pixel value is above this value, set it to dMaxVal
		double dMinVal;						///< value to be used with dMinValThreshold. use 0 to hide point.
		double dMaxVal;						///< value to be used with dMaxValThreshold. use 0 to hide point.

		SHeatMapConvertParams(PCSTR strDestDirectory = NULL, const IMcHeatMapViewport::SHeatMapPoint aHeatMapPoints[] = NULL,
			                  UINT uNumHeatMapPoints = 0, IMcGridCoordinateSystem *_pSourceCoordSys = NULL)
		{ 
			SetDefaults(strDestDirectory, aHeatMapPoints, uNumHeatMapPoints, _pSourceCoordSys);
		}
		
		void SetDefaults(PCSTR strDestDirectory = NULL, const IMcHeatMapViewport::SHeatMapPoint aHeatMapPoints[] = NULL,
			             UINT uNumHeatMapPoints = 0, IMcGridCoordinateSystem *_pSourceCoordSys = NULL)
		 {
			 SConvertParams::SetDefaults(strDestDirectory, NULL, 0, _pSourceCoordSys);
			 aPoints = aHeatMapPoints;
			 uNumPoints = uNumHeatMapPoints;
			 eItemType = IMcEllipseItem::NODE_TYPE;
			 uItemInfluenceRadius = 10;
			 bIsRadiusInPixels = false;
			 bIsGradient = false;
			 bGPUBased = false;
			 fTargetLowestResolution = MC_RESOLUTION_SOURCE;
			 bCalcAveragePerPoint = false;
			 dMinValThreshold = 0;
			 dMaxValThreshold = DBL_MAX;
			 dMinVal = dMinValThreshold;
			 dMaxVal = dMaxValThreshold;
		 }
	};

	/// Recoding pair of UINTs
	struct SRecodePair
	{
		UINT uFrom;					///< from
		UINT uTo;					///< to

		/// \n
		SRecodePair(UINT _uFrom = 0, UINT _uTo = 0) : uFrom(_uFrom), uTo(_uTo) {}
	};

	/// CodeMap conversion params
	struct SCodeMapConvertParams : SConvertParams
	{
		ERasterOverlapMode  eOverlapMode;				///< raster overlap mode
		SMcBColor			TransparentColor;			///< Color of transparent pixel 
		BYTE                byTransparentColorPrecision;///< Transparent color precision for each channel (R, G, B)
		const SRecodePair	*aRecodeData;				///< Optional array for material ids recode.
		UINT				uNumRecodeData;				///< Number of entries in \b aRecodeData

		SCodeMapConvertParams(PCSTR strDestDirectory = NULL, const SSourceFileParams aFiles[] = NULL,
							   UINT uNumFiles = 0, IMcGridCoordinateSystem *_pSourceCoordSys = NULL)
		{ 
			SetDefaults(strDestDirectory, aFiles, uNumFiles, _pSourceCoordSys);
		}

		void SetDefaults(PCSTR strDestDirectory = NULL, const SSourceFileParams aFiles[] = NULL,
						 UINT uNumFiles = 0, IMcGridCoordinateSystem *_pSourceCoordSys = NULL,
						 bool _bUseRecoveryInfo = false)
		{
			SConvertParams::SetDefaults(strDestDirectory, aFiles, uNumFiles, _pSourceCoordSys);

			eOverlapMode = EROM_OVERRIDE_ALWAYS;
			TransparentColor = bcBlackTransparent;
			byTransparentColorPrecision = 0;
			aRecodeData = NULL;
			uNumRecodeData = 0;
		}
	};

	/// Vector conversion params
	struct SVectorConvertParams : IMcRawVectorMapLayer::SParams
	{
		char strDestDir[MAX_PATH];									///< conversion destination directory
		char strConvertedLayerName[MAX_PATH];						///< Optional converted layer name; if "", name is based on the DataSource.
		char strMetaDataFormat[MAX_PATH];							///< Optional GdalDriverName string; if "", default format is used
																	///< (old - "ESRI Shapefile", new - "SQLite").
		UINT *aFieldIdsFilter;										///< IDs of metadata fields to be copied to the converted layer
		UINT uNumFieldIds;											///< number of IDs of metadata fields
		bool bCreateMetaData;										///< Whether or not to create metadata at all
		UINT *aLayerAttributeIdsFilter;								///< IDs of layer attributes to be copied to the converted layer
		UINT uNumLayerAttributeIds;									///< number of IDs of layer attributes
		bool bSaveLayerAttributes;									///< Whether or not to save layer attributes at all
		
		IMcGridCoordinateSystem *pTargetCoordinateSystem;			///< Optional layer's target coordinate system; default is NULL
																	///< (means same as layer's source coordinate system)
		const IMcMapLayer::STilingScheme *pTilingScheme;			///< Optional destination tiling scheme; default is NULL (means MapCore scheme)
		
		float fMinSizeFactor;										///< Relevant only when \b bIsLiteVectorLayer is true;
																	///< will effect the minimum size factor that can be set by IMcOverlayManager::SetItemSizeFactors
																	///< in the application later loading the converted layer.
		float fMaxSizeFactor;										///< Relevant only when \b bIsLiteVectorLayer is true;
																	///< will effect the maximum size factor that can be set by IMcOverlayManager::SetItemSizeFactors
																	///< in the application later loading the converted layer.
		bool bIsLiteVectorLayer;									///< Whether or not to convert to Lite Native Vector
		IMcOverlayManager::ESavingVersionCompatibility eVersion;	///< Compatibility to MapCore's previous versions; the 
																	///< default is IMcOverlayManager::ESVC_LATEST (no compatibility)
		UINT uNumTilesInFileEdge;									///< number of tiles in each output file edge (\b uNumTilesInFileEdge x \b uNumTilesInFileEdge tiles in file);
																	///< 0 means 8 x 8 tiles (the default)
		
		SVectorConvertParams(PCSTR strDestDirectory = NULL, PCSTR _strConvertedLayerName = NULL, PCSTR _strDataSource = NULL,
			PCSTR _strPointTextureFile = NULL, PCSTR _strLocaleStr = NULL, PCSTR _strMetaDataFormat = NULL,
			IMcGridCoordinateSystem *_pSourceCoordinateSystem = NULL, IMcGridCoordinateSystem *_pTargetCoordinateSystem = NULL,
			UINT *aFIdsFilter = NULL, UINT uNumFIds = 0, bool _bCreateMetaData = true,
			double _dSimplificationTolerance = 0, float _fMinSizeFactor = 1, float _fMaxSizeFactor = 1, bool _bIsLiteVectorLayer = false,
			UINT *_aLayerAttributeIdsFilter = NULL, UINT _uNumLayerAttributeIds = 0, bool _bSaveLayerAttributes = true,
			float _fMinScale = 0, float _fMaxScale = 0) :
			IMcRawVectorMapLayer::SParams(
				_strDataSource,
				_pSourceCoordinateSystem,
				_fMinScale, _fMaxScale,
				_strPointTextureFile,
				_strLocaleStr,
				_dSimplificationTolerance)
		{
			UINT uMaxStrNum = MAX_PATH;

			if (strDestDirectory)
			{
				strncpy(strDestDir, strDestDirectory, uMaxStrNum);
				strDestDir[uMaxStrNum - 1] = '\0';
			}
			else
				strDestDir[0] = '\0';
			if (_strConvertedLayerName)
			{
				strncpy(strConvertedLayerName, _strConvertedLayerName, uMaxStrNum);
				strConvertedLayerName[uMaxStrNum - 1] = '\0';
			}
			else
				strConvertedLayerName[0] = '\0';
			if (_strMetaDataFormat)
			{
				strncpy(strMetaDataFormat, _strMetaDataFormat, uMaxStrNum);
				strMetaDataFormat[uMaxStrNum - 1] = '\0';
			}
			else
				strMetaDataFormat[0] = '\0';
			aFieldIdsFilter = aFIdsFilter;
			uNumFieldIds = uNumFIds;
			bCreateMetaData = _bCreateMetaData;
			aLayerAttributeIdsFilter = _aLayerAttributeIdsFilter;
			uNumLayerAttributeIds = _uNumLayerAttributeIds;
			bSaveLayerAttributes = _bSaveLayerAttributes;
			pTargetCoordinateSystem = _pTargetCoordinateSystem;
			pTilingScheme = NULL;
			fMinSizeFactor = _fMinSizeFactor;
			fMaxSizeFactor = _fMaxSizeFactor;
			bIsLiteVectorLayer = _bIsLiteVectorLayer;
			eVersion = IMcOverlayManager::ESVC_LATEST;
			uNumTilesInFileEdge = 0;
		}
	};

	/// Parameters for vector 3D extrusion layer
	struct SVector3DExtrusionConvertParams : IMcRawVector3DExtrusionMapLayer::SParams
	{
		PCSTR strDestDir;								///< The destination directory.
		EVersionCompatibility eVersionCompatibility;	///< version compatibility
		UINT uNumTilesInFileEdge;						///< number of tiles in each output file edge (\b uNumTilesInFileEdge x \b uNumTilesInFileEdge tiles in file);
														///<  0 means 8 x 8 tiles (the default)
		IMcSpatialQueries *pSpatialQueries;				///< The interface for DTM queries.

		SVector3DExtrusionConvertParams(PCSTR _strDestDir = NULL, PCSTR _strDataSource = NULL, IMcGridCoordinateSystem *_pSourceCoordSys = NULL)
			: IMcRawVector3DExtrusionMapLayer::SParams(_strDataSource, _pSourceCoordSys), strDestDir(_strDestDir),
			eVersionCompatibility(IMcMapProduction::EVC_LATEST_VERSION), uNumTilesInFileEdge(0), pSpatialQueries(NULL) {}
	};

	/// 3D model conversion params
	struct S3DModelConvertParams : SConvertParams
	{
		char (*astrResourceDirs)[MAX_PATH];						///< Optional directories with additional resources
		UINT					uNumResourceDirs;				///< Number of directories in \b astrResourceDirs
		PCSTR					strMeshName;					///< Name to be given to mesh 
																///<   (relevant if \b eOutputType == #ESOO_SINGLE_MESH)
		int						nXAxisMapping;					///< mapping for input's X axis
																///<   (+/-1 means to +/-x, +/-2 means to +/-y, +/-3 means to +/-z); 
																///<   the default is 1 (x)
		int						nYAxisMapping;					///< mapping for input's Y axis (for meaning see \b nXAxisMapping); 
																///<   the default is 2 (y)
		int						nZAxisMapping;					///< mapping for input's Y axis (for meaning see \b nXAxisMapping);
																///<   the default is 3 (z)
		bool					bOrthometricHeights;			///< whether the heights are orthometric (above the geoid / sea level) or ellipsoid 
																///<   heights (above input's coordinate system's elipsoid); the default is `false`

		S3DModelConvertParams(PCSTR strDestDirectory = NULL, const SSourceFileParams aFiles[] = NULL, 
					   UINT uNumFiles = 0, IMcGridCoordinateSystem *_pSourceCoordSys = NULL)
		{
			SetDefaults(strDestDirectory, aFiles, uNumFiles, _pSourceCoordSys);
		}

		void SetDefaults(PCSTR strDestDirectory, const SSourceFileParams aFiles[], 
			             UINT uNumFiles, IMcGridCoordinateSystem *_pSourceCoordSys)
		{
			SConvertParams::SetDefaults(strDestDirectory, aFiles, uNumFiles, _pSourceCoordSys);
			fTargetHighestResolution = 0.05f;
			astrResourceDirs = NULL;
			uNumResourceDirs = 0;
			strMeshName = NULL;
			nXAxisMapping = 1;
			nYAxisMapping = 2;
			nZAxisMapping = 3;
			bOrthometricHeights = false;
		}
	};

	/// Parameters for building static-objects textures from frames projected onto static-objects geometry
	struct SStaticObjectsTexturesFromFramesParams
	{
		PCSTR					strObjectsDir;					///< static-objects directory
		EStaticObjectsOutput	eObjectsFormat;					///< static-objects format
		PCSTR					strMeshName;					///< Name given to mesh 
																///<  (relevant if \b eObjectsFormat == #ESOO_SINGLE_MESH)
		const IMcMapProduction::SFrameData *aFramesForTextures;	///< Frames to be projected for building textures
		UINT uNumFramesForTextures;								///< Number of frames in \b aFramesForTextures

		SStaticObjectsTexturesFromFramesParams(PCSTR _strObjectsDir = NULL, 
			const IMcMapProduction::SFrameData *_aFramesForTextures = NULL, UINT _uNumFramesForTextures = 0)
		{

			SetDefaults(_strObjectsDir, _aFramesForTextures, _uNumFramesForTextures);
		}

		void SetDefaults(PCSTR _strObjectsDir, 
			const IMcMapProduction::SFrameData *_aFramesForTextures = NULL, UINT _uNumFramesForTextures = 0)
		{
			strObjectsDir =_strObjectsDir;
			aFramesForTextures = _aFramesForTextures; 
			uNumFramesForTextures = _uNumFramesForTextures;
			eObjectsFormat = ESOO_NATIVE_MAP_LAYER;
			strMeshName = NULL;
		}
	};

	/// Map layer properties
	struct SMapLayerProperties
	{
		IMcMapLayer::ELayerKind	eLayerKind;		///< layer kind
		bool		bIsRasterInWorldCoordSys;	///< whether or not the raster is in world coordinate system.
												///< if raster is in world coordinates system, it is geo-referenced.
												///< else, raster is in image coordinate system (pixel coordinates): 
												///< top left pixel is (0, 0), right bottom pixel is (width-1, -(height-1).
		double		dMinX;						///< minimal x limit (west)
		double		dMinY;						///< minimal y limit (south)
		double		dMaxX;						///< maximal x limit (east)
		double		dMaxY;						///< maximal y limit (north)
		float		fHighestResolution;			///< layer's highest resolution
		UINT		uNumResolutions;			///< number of layer's resolutions
		EVersionCompatibility
					eVersionCompatibility;		///< version compatibility
	};

	struct SXmlScaleRangeNode	
	{
		float fMin;
		float fMax;
		SXmlScaleRangeNode() : fMin(0),fMax(0) {}
	};
		
	struct SXmlBaseProperyNode
	{
	public:
		char strPropertyName[MAX_PATH];
	private:
		IMcProperty::EPropertyType ePropertyType;

	public:
		IMcProperty::EPropertyType GetPropertyType() const { return ePropertyType; }

		template <typename T>
		void GetProperyFieldName(PCSTR *pstrFieldName) const
		{
			if (pstrFieldName)
			{
				const IMcMapProduction::SXmlProperyNode<T> *pProperty = static_cast<const IMcMapProduction::SXmlProperyNode<T> *>(this);
				if (pProperty->aPropertySwitchCases && pProperty->uNumPropertySwitchCases > 0)
				{
					*pstrFieldName = pProperty->strSwitchFieldName;
				}
				else
				{
					*pstrFieldName = pProperty->strFieldName;
				}
			}
		}

	protected:
		SXmlBaseProperyNode(IMcProperty::EPropertyType eTypeValue)
		{
			strPropertyName[0] = '\0';
			ePropertyType = eTypeValue;
		}
	};

	template <typename T>
	struct SXmlProperySwitchCase
	{
		char strFieldValue[MAX_PATH];
		T PropertyValue;
	};

	template <typename T>
	struct SXmlGenericBaseProperyNode: public SXmlBaseProperyNode
	{
		char strSwitchFieldName[MAX_PATH];
  		SXmlProperySwitchCase<T> *aPropertySwitchCases;
		UINT uNumPropertySwitchCases;

	protected:
		SXmlGenericBaseProperyNode(IMcProperty::EPropertyType eTypeValue) : SXmlBaseProperyNode(eTypeValue)
		{
			strSwitchFieldName[0] = '\0';
			aPropertySwitchCases = NULL;
			uNumPropertySwitchCases = 0;
		}
	};

	template <typename T>
	struct SXmlProperyNode : public SXmlGenericBaseProperyNode<T> 
	{
		T Value;
		char strFieldName[MAX_PATH];
		// bEnumAsUint needed because EPT_UINT and EPT_ENUM with same T=UINT type
		inline SXmlProperyNode(bool bEnumAsUint = false) : SXmlGenericBaseProperyNode<T>(IMcProperty::EPT_NUM) {} 
	};

	template <typename T>
	struct SXmlProperyNodeComponent 
	{
		T Value;
		char strFieldName[MAX_PATH];
	};

	struct SXmlTextureDef
	{
		char		strImageFileName[MAX_PATH];
		SMcBColor	TransparentColor;
		bool		bTransparentColorEnabled;
		bool		bIgnoreTransparentMargin;

		SXmlTextureDef() : bTransparentColorEnabled(false), bIgnoreTransparentMargin(false)
		{
			strImageFileName[0] = '\0';
		}
	};

	struct SXmlFontDef
	{ 
		int		nHeight;
		char	strFaceName[LF_FACESIZE];
		UINT	uCharSet;

		SXmlFontDef() : nHeight(0), uCharSet(ANSI_CHARSET)
		{
			strFaceName[0] = '\0';
		}
	};

	struct SXmlScaleConditionalSelectorDef
	{
		float fMinScale;
		float fMaxScale;
		UINT uCancelScaleMode;
		UINT uCancelScaleModeResult;

		SXmlScaleConditionalSelectorDef() : fMinScale(0), fMaxScale(0), uCancelScaleMode(0), uCancelScaleModeResult(0)
		{
		}
	};

	struct SXmlFVector2DProperyNode : public SXmlGenericBaseProperyNode<SMcFVector2D>
	{
		SXmlProperyNodeComponent<float> X;
		SXmlProperyNodeComponent<float> Y;

		inline SXmlFVector2DProperyNode() : SXmlGenericBaseProperyNode(IMcProperty::EPT_FVECTOR2D)
		{
			X.Value = 0;
			X.strFieldName[0] = '\0';
			Y.Value = 0;
			Y.strFieldName[0] = '\0';
		}
	};

	struct SXmlVector2DProperyNode : public SXmlGenericBaseProperyNode<SMcVector2D>
	{
		SXmlProperyNodeComponent<double> X;
		SXmlProperyNodeComponent<double> Y;

		inline SXmlVector2DProperyNode() : SXmlGenericBaseProperyNode(IMcProperty::EPT_VECTOR2D)
		{
			X.Value = 0;
			X.strFieldName[0] = '\0';
			Y.Value = 0;
			Y.strFieldName[0] = '\0';
		}
	};

	struct SXmlFVector3DProperyNode : public SXmlGenericBaseProperyNode<SMcFVector3D>
	{
		SXmlProperyNodeComponent<float> X;
		SXmlProperyNodeComponent<float> Y;
		SXmlProperyNodeComponent<float> Z;

		inline SXmlFVector3DProperyNode() : SXmlGenericBaseProperyNode(IMcProperty::EPT_FVECTOR3D)
		{
			X.Value = 0;
			X.strFieldName[0] = '\0';
			Y.Value = 0;
			Y.strFieldName[0] = '\0';
			Z.Value = 0;
			Z.strFieldName[0] = '\0';
		}
	};

	struct SXmlVector3DProperyNode : public SXmlGenericBaseProperyNode<SMcVector3D>
	{
		SXmlProperyNodeComponent<double> X;
		SXmlProperyNodeComponent<double> Y;
		SXmlProperyNodeComponent<double> Z;

		inline SXmlVector3DProperyNode() : SXmlGenericBaseProperyNode(IMcProperty::EPT_VECTOR3D)
		{
			X.Value = 0;
			X.strFieldName[0] = '\0';
			Y.Value = 0;
			Y.strFieldName[0] = '\0';
			Z.Value = 0;
			Z.strFieldName[0] = '\0';
		}
	};

	struct SXmlTextureProperyNode : public SXmlGenericBaseProperyNode<SXmlTextureDef>
	{
		SXmlProperyNodeComponent <char[MAX_PATH]> ImageFileName;
		SXmlProperyNodeComponent <SMcBColor> TransparentColor;
		SXmlProperyNodeComponent <bool> IsTransparentColorEnabled;
		SXmlProperyNodeComponent <bool> IgnoreTransparentMargin;

		SXmlTextureProperyNode() : SXmlGenericBaseProperyNode(IMcProperty::EPT_TEXTURE)
		{
			ImageFileName.Value[0] = '\0';
			ImageFileName.strFieldName[0] = '\0';
			TransparentColor.Value = 0;
			TransparentColor.strFieldName[0] = '\0';
			IsTransparentColorEnabled.Value = false;
			IsTransparentColorEnabled.strFieldName[0] = '\0';
			IgnoreTransparentMargin.Value = false;
			IgnoreTransparentMargin.strFieldName[0] = '\0';
		}		
	};

	struct SXmlFontProperyNode : public SXmlGenericBaseProperyNode<SXmlFontDef>
	{ 
		SXmlProperyNodeComponent<int> Height;
		SXmlProperyNodeComponent<char[LF_FACESIZE]> FaceName;
		SXmlProperyNodeComponent<UINT> CharSet;

		SXmlFontProperyNode() : SXmlGenericBaseProperyNode(IMcProperty::EPT_FONT) 
		{
			Height.Value = 0;
			Height.strFieldName[0] = '\0';
			FaceName.Value[0] = '\0';
			FaceName.strFieldName[0] = '\0';
			CharSet.Value = UINT(-1);
			CharSet.strFieldName[0] = '\0';
		}
	};

	struct SXmlScaleConditionalSelectorProperyNode : public SXmlGenericBaseProperyNode<SXmlScaleConditionalSelectorDef>
	{
		SXmlProperyNodeComponent<float> Min;
		SXmlProperyNodeComponent<float> Max;
		SXmlProperyNodeComponent<UINT> CancelScaleMode;
		SXmlProperyNodeComponent<UINT> CancelScaleModeResult;

		SXmlScaleConditionalSelectorProperyNode() : SXmlGenericBaseProperyNode(IMcProperty::EPT_CONDITIONALSELECTOR)
		{	
			Min.Value = 0;
			Min.strFieldName[0] = '\0';
			Max.Value = 0;
			Max.strFieldName[0] = '\0';
			CancelScaleMode.Value = 0;
			CancelScaleMode.strFieldName[0] = '\0';
			CancelScaleModeResult.Value = 0;
			CancelScaleModeResult.strFieldName[0] = '\0';
		}
	};

	struct SXmlSubItemsDataProperyNode : public SXmlGenericBaseProperyNode<typename IMcProperty::SArrayProperty<SMcSubItemData> >
	{
		SXmlSubItemsDataProperyNode() : SXmlGenericBaseProperyNode(IMcProperty::EPT_SUBITEM_ARRAY){}
	};

 	struct SXmlObjectNode
 	{
 		char strSchemeFile[MAX_PATH]; // ANSI in API, UTF8 internally
  		SXmlScaleRangeNode ScaleRange;			
  		SXmlBaseProperyNode **apProperties;
		UINT uNumProperties;
		char strKeyFieldName[MAX_PATH];
		bool bAutoUnifyLabels;

		inline SXmlObjectNode()
		{
			strSchemeFile[0] = '\0';
			strKeyFieldName[0] = '\0';
			bAutoUnifyLabels = false;
			uNumProperties = 0;
			apProperties = NULL;
		}
 	};

 	struct SXmlFieldConditionNode
 	{
		char strFieldName[MAX_PATH];
 		EComparisonOperator eOperator;			
 		PCSTR strOperand; // ANSI in API, UTF8 internally
 		PCSTR strAdditionalOperand; // ANSI in API, UTF8 internally

		SXmlFieldConditionNode()
 		{
			strFieldName[0] = '\0';
 			eOperator = ECO_NUM;
 			strOperand = NULL;
 			strAdditionalOperand = NULL;
 		}
 	};

	struct SXmlBoolConditionNode
 	{
		EBoolOperator eBoolOperator;
		SXmlFieldConditionNode *aFieldConditions;
		UINT uNumFieldConditions;
		SXmlBoolConditionNode *aBoolConditions;
		UINT uNumBoolConditions;

		SXmlBoolConditionNode()
		{
			eBoolOperator = EBO_NUM;
			aFieldConditions = NULL;
			uNumFieldConditions = 0;
			aBoolConditions = NULL;
			uNumBoolConditions = 0;
		}
	};

	struct SXmlObjectConditionNode
	{
		SXmlBoolConditionNode BoolCondition;
		SXmlObjectNode ObjectDefinition;
 	};
 
 	struct SXmlVectorLayerGraphicalSettings
 	{
 		PCSTR strDataSource;
		float fEquidistantMinScale;
		float fMaxScaleFactor;
 		SXmlScaleRangeNode ScaleRange;
		SXmlObjectConditionNode *aObjectConditions;
		UINT uNumObjectConditions;
		bool bMultiCondition;
		char strStylingFormat[MAX_PATH];
		char strUnifyByField[MAX_PATH];

		SXmlVectorLayerGraphicalSettings()
		{
			strDataSource = NULL;
			fEquidistantMinScale = 1;
			fMaxScaleFactor = 1;
			aObjectConditions = NULL;
			uNumObjectConditions = 0;
			bMultiCondition = false;
			strStylingFormat[0] = '\0';
			strUnifyByField[0] = '\0';
		}
 	};

	/// \name Creation And Progress/Error Handling
	//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates map production interface object
	///
	/// \param[out]	ppProduction		Production object created
	/// \param[in]	pSourceDataCallback	Callback for receiving user-provided source data; 
	///									the default is NULL (no user-provided source is used)
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPPRODUCTION_API IMcErrors::ECode Create(IMcMapProduction **ppProduction, 
		ISourceDataCallback *pSourceDataCallback = NULL);

	//==============================================================================
	// Method Name: AddProgressCallback()
	//------------------------------------------------------------------------------
	/// Adds a callback used to pass progress messages to the user application
	///
	/// Called when each progress message should be passed by a conversion function
	/// to the user application.
	///
	/// \param[in]	pCallback	callback to add.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode AddProgressCallback(IMcProgressCallback *pCallback) = 0;

	//==============================================================================
	// Method Name: RemoveProgressCallback()
	//------------------------------------------------------------------------------
	/// Removes a callback added by AddProgressMessageCallback().
	///
	/// \param[in]	pCallback	callback to remove.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RemoveProgressCallback(IMcProgressCallback *pCallback) = 0;

	//==============================================================================
	// Method Name: GetLastDetailedErrorString()
	//------------------------------------------------------------------------------
	/// Retrieves an error returned by a last call to one of IMcProduction's functions 
	/// in a form of detailed error string.
	///
	/// The string can contain detailed error information including file names etc.
	///
	/// \param[out]   pstrErrorString    error string buffer 
	///									 (valid until a next call to one of IMcProduction's functions)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLastDetailedErrorString(PCSTR *pstrErrorString) = 0;
	//@}

	/// \name Conversion
	//@{
	
	//=================================================================================================
	//
	// Function name: BuildSourceRasterOrDtmFilesList(...)
	//
	//-------------------------------------------------------------------------------------------------
	/// Builds a list of source raster/DTM files with limits and resolutions
	///
	/// Based on raster/DTM files found in the specified directory and their description files 
	/// (e.g. TFW files for TIFF files)
	///
	/// \param[in]   strSourceDir				source files directory
	/// \param[in]   eSourceType				type of source files to deal with; see #IMcMapLayer::ELayerKind for details 
	///											(raster and DTM only are valid here)
	/// \param[in]	 bIsRasterInWorldCoordSys	whether or not the raster is in world coordinate system.
	///											if raster is in world coordinates system, it is geo-referenced.
	///											else, raster is in image coordinate system (pixel coordinates): 
	///											top left pixel is (0, 0), right bottom pixel is (width-1, -(height-1).
	/// \param[out]  paFiles					array of source files with limits and resolutions
	/// \param[out]  pauInvalidParamsBitFields	array of invalid parameters descriptions of source files in 
	///											\a paFiles[], each element is zero or combination of flags 
	///											from #ESourceParamsValidityFlags; can be NULL 
	///											(in this case only files with valid parameters are added)
	/// \param[in]   strFileExtensions[]		array of file extensions to search for (without leading '.')  
	///											or NULL to use all supported extensions
	/// \param[in]   uNumExtensions				number of file extensions in \a strFileExtensions[]
	/// \param[in]   bRecursive					whether subdirectories should be included
	/// \param[in]   eSorting					sorting method to apply; the default is #ESS_RESOLUTION_FROM_LOW_TO_HIGH
	///
	/// \return
	///   - #IMcErrors::SUCCESS if the function succeeds;
	///   - #IMcErrors::PRODUCTION_INVALID_SRC_FILE_PARAMS if the function succeeds to build file list but
	///     some limits or resolutions of some files are either missing or invalid or do not match each other
	///     (if \a pauInvalidParamsBitFields != NULL, these problems are marked in \a pauInvalidParamsBitFields[],
	///     besides that missing parameters are filled in paFiles[] with #MC_SOURCE_FILE_PARAM_NO_VALUE;
	///     if \a pauInvalidParamsBitFields == NULL, files with invalid limits and resolutions are not added)
	///   - other error codes if the function fails in case of other reasons.
	//=================================================================================================
	virtual IMcErrors::ECode BuildSourceRasterOrDtmFilesList(
		PCSTR strSourceDir, IMcMapLayer::ELayerKind eSourceType, bool bIsRasterInWorldCoordSys,
		CMcDataArray<SSourceFileParams, true> *paFiles, CMcDataArray<UINT, true> *pauInvalidParamsBitFields, 
		PCSTR strFileExtensions[] = NULL, UINT uNumExtensions = 0, bool bRecursive = false, 
		ESourceSorting eSorting = ESS_RESOLUTION_FROM_LOW_TO_HIGH) = 0;

	virtual IMcErrors::ECode BuildRasterLayerFromFrames(
		const IMcMapProduction::SRasterFromFramesParams& BuildParams) = 0;

	virtual IMcErrors::ECode BuildStaticObjectsTexturesFromFrames(
		const SStaticObjectsTexturesFromFramesParams &BuildParams) = 0;

	//=================================================================================================
	//
	// Function name: CalculateSourceDtmPreferredOriginAndResolution(...)
	//
	//-------------------------------------------------------------------------------------------------
	/// Calculates source DTM preferred origin and resolution that are optimal for conversion to MapCore format
	///
	/// \param[in]	dSrcMinX			original source's minimal x limit (west)
	/// \param[in]	dSrcMinY			original source's minimal y limit (south)
	/// \param[in]	dHighestSrcXRes		original source's x resolution
	/// \param[in]	dHighesSrcYRes		original source's y resolution
	/// \param[out]	pdPreferredMinX		preferred minimal x limit (west)
	/// \param[out]	pdPreferredMinY		preferred minimal y limit (south)
	/// \param[out]	pfPreferredXRes		preferred x resolution
	/// \param[out]	pfPreferredYRes		preferred y resolution
	///
	/// \return
	///     - status result
	//=================================================================================================
	static MAPPRODUCTION_API IMcErrors::ECode CalculateSourceDtmPreferredOriginAndResolution(
		double dSrcMinX, double dSrcMinY, double dHighestSrcXRes, double dHighesSrcYRes, 
		double *pdPreferredMinX, double *pdPreferredMinY, float *pfPreferredXRes, float *pfPreferredYRes);

	//==============================================================================
	// Method Name: CreateSourceMatrixFromGeometry()
	//------------------------------------------------------------------------------
	/// Calculates raster or DTM matrix from static objects layer and/or manual geometry
	///
	/// \param[out]	ppSourceMatrix				source matrix interface				
	/// \param[in]	eSourceType					type of source to deal with; see #IMcMapLayer::ELayerKind for details 
	///											(raster and DTM only are valid here)
	/// \param[in]	dGridResolution				resolution of DTM grid
	/// \param[in]	TileMargins					margins of tiles (in grid points) in both directions of every axis
	/// \param[in]	bToBeUsedForSourceDTM		whether to change the resolution and the limits to values optimal for 
	///											conversion to MapCore format
	/// \param[in]	pDevice						device to use for GPU-based calculations
	/// \param[in]	str3DModelLayerDir			optional 3D-model layer directory
	/// \param[in]	aPoints						manual geometry points
	/// \param[in]	uNumPoints					number of manual geometry points
	/// \param[in]	aConnectionIndices			manual geometry connection indices containing triplets of indices
	///											(each 3 indices define a triangle)
	/// \param[in]	uNumConnectionIndices		number of manual geometry connection indices
	/// \param[in]	dMinX						optional minimal limit of DTM matrix along X axis or 
	///											 DBL_MAX if the limits should be calculated from the input
	/// \param[in]	dMinY						optional minimal limit of DTM matrix along Y axis
	/// \param[in]	dMaxX						optional maximal limit of DTM matrix along X axis
	/// \param[in]	dMaxY						optional maximal limit of DTM matrix along Y axis
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode CreateSourceMatrixFromGeometry(ITiledSourceMatrix **ppSourceMatrix, IMcMapLayer::ELayerKind eSourceType,
		double dGridResolution, const SMcSize &TileMargins, bool bToBeUsedForSourceDTM, IMcMapDevice *pDevice, 
		PCSTR str3DModelLayerDir, const SMcVector3D aPoints[] = NULL, UINT uNumPoints = 0, 
		const UINT aConnectionIndices[] = NULL, UINT uNumConnectionIndices = 0, 
		double dMinX = DBL_MAX, double dMinY = DBL_MAX, double dMaxX = DBL_MAX, double dMaxY = DBL_MAX) const = 0;

	//=================================================================================================
	// 
	// Function name: ConvertRasterLayer(...)
	// 
	/// Converts rasters to MapCore Raster layer format 
	/// 
	/// \param[in]   ConvertParams		raster layer conversion parameters
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode ConvertRasterLayer(const SRasterConvertParams &ConvertParams) = 0;

	//=================================================================================================
	// 
	// Function name: ConvertDtm(...)
	// 
	/// Converts DTM files to MapCore DTM layer format 
	/// 
	/// \param[in]   ConvertParams		DTM layer conversion parameters
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode ConvertDtmLayer(const SDtmConvertParams &ConvertParams) = 0;

	//=================================================================================================
	// 
	// Function name: ConvertHeatMapLayer(...)
	// 
	/// Converts points array to MapCore heat map layer format 
	/// 
	/// \param[in]   ConvertParams		heat map layer conversion parameters
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode ConvertHeatMapLayer(const SHeatMapConvertParams &ConvertParams) = 0;

	//=================================================================================================
	// 
	// Function name: ConvertMaterialLayer(...)
	// 
	/// Converts to MapCore material layer format 
	/// 
	/// \param[in]   ConvertParams		material layer conversion parameters
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode ConvertMaterialLayer(const SCodeMapConvertParams &ConvertParams) = 0;

	//=================================================================================================
	// 
	// Function name: ConvertTraversabilityLayer(...)
	// 
	/// Converts to MapCore traversability layer format 
	/// 
	/// \param[in]   ConvertParams		traversability layer conversion parameters
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode ConvertTraversabilityLayer(const SCodeMapConvertParams &ConvertParams) = 0;

	//=================================================================================================
	// 
	//  Function name: ConvertVectorLayer(...)
	// 
	/// Converts vector to MapCore vector layer file format 
	/// 
	/// Not supports update vector files
	///
	/// \param[in]   ConvertParams		vector layer conversion parameters
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode ConvertVectorLayer(const IMcMapProduction::SVectorConvertParams&  ConvertParams) = 0;

	//==============================================================================
	// Method Name: IsObjectSchemeValidForLiteVectorLayerConversion()
	//------------------------------------------------------------------------------
	/// Checks whether object scheme is valid for conversion of lite vector layer
	///
	/// \param[in]	pScheme		object scheme to check
	/// \param[out]	pbIsValid	whether it is valid for conversion of lite vector layer
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPPRODUCTION_API IMcErrors::ECode IsObjectSchemeValidForLiteVectorLayerConversion(
		IMcObjectScheme *pScheme, bool *pbIsValid);

	virtual IMcErrors::ECode SaveVectorLayerGraphicalSettings(
		const IMcMapProduction::SXmlVectorLayerGraphicalSettings &Settings, PCSTR strXmlFile, 
		PCSTR strXsdFile = NULL, PCSTR strLocaleStr = NULL) const = 0;

	virtual IMcErrors::ECode LoadVectorLayerGraphicalSettings(PCSTR strXmlFile, const IMcMapProduction::SXmlVectorLayerGraphicalSettings **ppSettings, PCSTR strLocaleStr = NULL) = 0;

	//=================================================================================================
	// 
	// Function name: Convert3DModelLayer(...)
	// 
	/// Converts 3D model data to MapCore's 3D model layer format
	/// 
	/// \param[in]   ConvertParams		3D model layer conversion parameters
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode Convert3DModelLayer(const S3DModelConvertParams &ConvertParams) = 0;

	//=================================================================================================
	// 
	// Function name: Convert3DModelLayer(...)
	// 
	/// Converts vector data to MapCore's vector 3D extrusion format 
	/// 
	/// \param[in]   ConvertParams		vector 3D extrusion conversion parameters
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode ConvertVector3DExtrusionLayer(const SVector3DExtrusionConvertParams &ConvertParams) = 0;

	//=================================================================================================
	// 
	// Function name: ExportStaticObjectsLayer(...)
	// 
	/// Exports MapCore's static-objects layer into the specified format 
	/// 
	/// \param[in]	strLayerDir		directory of the layer to be exported
	/// \param[in]	strDestDir		directory for the destination files
	/// \param[in]	eExportFormat	export format
	/// \param[in]	pSourceCoordSys	source coordinate system; 
	///								default is NULL (reprojection is not required)
	/// \param[in]	pDestCoordSys	destination coordinate system; 
	///								default is NULL (reprojection is not required)
	/// \note
	/// \a pSourceCoordSys and \a pDestCoordSys should be both equal to NULL or valid coordinate systems
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode ExportStaticObjectsLayer(PCSTR strLayerDir, PCSTR strDestDir, 
		EStaticObjectsExportFormat eExportFormat, 
		IMcGridCoordinateSystem *pSourceCoordSys = NULL, IMcGridCoordinateSystem *pDestCoordSys = NULL) = 0;

	//=================================================================================================
	// 
	// Function name: ConvertRasterLayerToGrayscale(...)
	// 
	/// Converts Raster layer files  to grayscale
	/// 
	/// \param[in]   strDestDir  directory of the layer to be converted
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode ConvertRasterLayerToGrayscale(PCSTR strDestDir) = 0;

	//=================================================================================================
	// 
	// Function name: ChangeLayerCompatibility(...)
	// 
	/// Changes version compatibility of converted layer
	/// 
	/// \param[in]	strLayerDir				directory of the layer to be converted
	/// \param[in]	eRequiredCompatibility	required compatibility
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	
	virtual IMcErrors::ECode ChangeLayerCompatibility(PCSTR strLayerDir, 
		EVersionCompatibility eRequiredCompatibility) = 0;

	/// \name Monitoring Modified Files
	//@{

	//=================================================================================================
	// 
	// Function name: StartMonitoringModifiedFiles(...)
	// 
	/// Starts monitoring the files added or updated to the layer during conversion
	/// 
	/// \param[in]   strDestDir   destination directory
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode StartMonitoringModifiedFiles(PCSTR strDestDir) = 0;

	
	//=================================================================================================
	// 
	// Function name: GetModifiedFiles(...)
	// 
	//-------------------------------------------------------------------------------------------------
	/// Ends monitoring files added or updated during conversion and retrieves their list
	///
	/// 
	/// \param[out]   pastrModifiedFiles   list of files added or updated during conversion,
	///                                       allocated by user, filled by the function
	/// \param[out]   puFilesTotalSize     total size in bytes of all files added or updated during conversion
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetModifiedFiles(CMcDataArray<PCSTR> *pastrModifiedFiles,
											  UINT64 *puFilesTotalSize = NULL) = 0;

	//@}

	/// \name Retrieving Data From Converted Layers
	//@{

	//=================================================================================================
	// 
	// Function name: GetRasterOrDtmLayerProperties(...)
	// 
	//-------------------------------------------------------------------------------------------------
	/// Retrieves converted raster or DTM layer properties
	/// 
	/// \param[in]    strLayerDir			converted layer directory
	/// \param[out]   pLayerProperties		layer properties, filled by the function
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetRasterOrDtmLayerProperties(PCSTR strLayerDir, 
														   SMapLayerProperties *pLayerProperties) = 0;

	//=================================================================================================
	// 
	// Function name: GetRasterOrDtmLayerFiles(...)
	// 
	//-------------------------------------------------------------------------------------------------
 	/// Retrieves a list of files of one resolution of converted raster or DTM layer 
 	/// (optionally lying inside a specified rectangle of interest) or header files of the layer
 	///
 	/// \param[in]	strLayerDir				converted layer directory
 	/// \param[in]	uResolutionIndex		UINT(-1) for header files or index of resolution to retrieve; 
 	///										from 0 (the highest one), to SMapLayerProperties::uNumResolutions - 1 
 	///										(the lowest one, retrieved by GetRasterOrDtmLayerProperties()); 
 	/// \param[out]	strFilesPath			full path (with trailing backslash) of the files retrieved
 	/// \param[out]	pastrFilesNames			array of names of files relative to \a strFilesPath
 	/// \param[in]	pRectOfInterest			optional rectangle of interest
 	///
 	/// \return
 	///     - status result
 	//==============================================================================
 	virtual IMcErrors::ECode GetRasterOrDtmLayerFiles(PCSTR strLayerDir, UINT uResolutionIndex, 
		char strFilesPath[MAX_PATH], CMcDataArray<PCSTR> *pastrFilesNames, SMcBox *pRectOfInterest = NULL) = 0;

	//=================================================================================================
	// 
	// Function name: PreviewRasterMapLayerFile(...)
	// 
	//-------------------------------------------------------------------------------------------------
	/// Draws content of one of tiles files (*.tl) from converted layer
	/// 
	/// \param[in]   hDC               handle of Windows device context for drawing
	/// \param[in]   Rect            the borders in which the tile will be drawn
	/// \param[in]   strTilesFile      name of tiles file
	/// \param[in]   bDrawTileBorders  if tile borders are to be drawn
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode PreviewRasterMapLayerFile(HDC hDC, const SMcRect &Rect, PCSTR strTilesFile,
													   bool bDrawTileBorders = false) = 0;

	//=================================================================================================
	// 
	// Function name: SaveRasterMapLayerFileAsBitmap(...)
	// 
	//-------------------------------------------------------------------------------------------------
	/// Saves one tiles file (*.tl) from converted layer as bitmap file
	/// 
	/// \param[in]   strTilesFile         name of tiles file
	/// \param[in]   strBitmapFileToSave  name of BMP file to produce
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SaveRasterMapLayerFileAsBitmap(PCSTR strTilesFile, PCSTR strBitmapFileToSave) = 0;
	
	//=================================================================================================
	// 
	// Function name: GenerateVectorLayerTilesData(...)
	// 
	//-------------------------------------------------------------------------------------------------
	/// Generate tiles data files for an existing vector layer
	/// 
	/// \param[in]   strVectorDir	  the vector layer directory.
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GenerateVectorLayerTilesData(PCSTR strVectorDir) = 0;

	virtual IMcErrors::ECode GetVectorLayerMinMaxScale(PCSTR strVectorDir, 
		float *pfMinScale, float *pfMaxScale) = 0;

	//=================================================================================================
	// 
	// Function name: RemoveBorders(...)
	// 
	//-------------------------------------------------------------------------------------------------
	/// Replaces the borders of an input file to black with or without transparency.
	/// 
	/// \param[in]   pszInFile			  The input file name.
	/// \param[in]   sBorderColors		  The colors defining the border, only RGB bands are used (not A).
	/// \param[in]   nColorsNum			  The number of colors defining the border.
	/// \param[in]   pszOutFile			  The output file name.
	/// \param[in]   nMaxNonBorderColor	  The number of pixels which are different from sBorderColors to allow
	///                                    before determining that the border has ended, this is used to avoid 
	///                                    noisy borders.
	/// \param[in]   nNearColorDist	      The distance from the defined border color pixel value which
	///                                    will still be replaced
	/// \param[in]   bSetAlpha	          If true than the output file will consist of an Alpha band
	///                                    where all border pixels have value 0, and other pixels are set to 255.
	///                                   If false, than Alpha band will not be added.
	/// 
	/// \note
	/// 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode RemoveBorders(PCSTR pszInFile,
		SMcBColor sBorderColors[] , UINT nColorsNum,  PCSTR pszOutFile,
		int nMaxNonBorderColor = 2, int nNearColorDist = 15, bool bSetAlpha = false) = 0;

	//@}


};

template <>
inline IMcMapProduction::SXmlProperyNode<bool>::SXmlProperyNode(bool bEnumAsUint): SXmlGenericBaseProperyNode<bool>(IMcProperty::EPT_BOOL)
{
	Value = 0;
	strFieldName[0] = '\0';
}

template <>
inline IMcMapProduction::SXmlProperyNode<BYTE>::SXmlProperyNode(bool bEnumAsUint): SXmlGenericBaseProperyNode<BYTE>(IMcProperty::EPT_BYTE)
{
	Value = 0;
	strFieldName[0] = '\0';
}

template <>
inline  IMcMapProduction::SXmlProperyNode<int>::SXmlProperyNode(bool bEnumAsUint): SXmlGenericBaseProperyNode<int>(IMcProperty::EPT_INT)
{
	Value = 0;
	strFieldName[0] = '\0';
}

template <>
inline IMcMapProduction::SXmlProperyNode<UINT>::SXmlProperyNode(bool bEnumAsUint): SXmlGenericBaseProperyNode<UINT>(bEnumAsUint ? IMcProperty::EPT_ENUM : IMcProperty::EPT_UINT)
{
	Value = 0;
	strFieldName[0] = '\0';
}

template <>
inline  IMcMapProduction::SXmlProperyNode<float>::SXmlProperyNode(bool bEnumAsUint): SXmlGenericBaseProperyNode<float>(IMcProperty::EPT_FLOAT)
{
	Value = 0;
	strFieldName[0] = '\0';
}

template <>
inline  IMcMapProduction::SXmlProperyNode<double>::SXmlProperyNode(bool bEnumAsUint): SXmlGenericBaseProperyNode<double>(IMcProperty::EPT_DOUBLE)
{
	Value = 0;
	strFieldName[0] = '\0';
}

template <>
inline IMcMapProduction::SXmlProperyNode<SMcBColor>::SXmlProperyNode(bool bEnumAsUint): SXmlGenericBaseProperyNode<SMcBColor>(IMcProperty::EPT_BCOLOR)
{
 	Value = 0;
 	strFieldName[0] = '\0';
}
    
template <>
inline IMcMapProduction::SXmlProperyNode<PCSTR>::SXmlProperyNode(bool bEnumAsUint): SXmlGenericBaseProperyNode<PCSTR>(IMcProperty::EPT_STRING)
{
 	Value = 0;
 	strFieldName[0] = '\0';
}
