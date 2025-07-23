#pragma once
//===========================================================================
/// \file IMcImageCalc.h
/// The base interface for all images in the photogrammetric calculation package
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcBase.h"
#include "McCommonTypes.h"
#include "SMcVector.h"
#include "Calculations/IMcGridCoordinateSystem.h"
#include "Calculations/IMcSpatialQueries.h"

class IMcAffineImageCalc;
class IMcFrameIC;
class IMcFrameImageCalc;
//class IMcGalaxyAidsImageCalc;
class IMcLoropImageCalc;
//class IMcFrameMosaicImageCalc;
class IMcUserDefinedImageCalc;
class IMcDtmMapLayer;
class IMcMapTerrain;

//===========================================================================
// Interface Name: IMcImageCalc
//---------------------------------------------------------------------------
///	The base interface for all images in the photogrammetric calculation package
/// 
//===========================================================================
class IMcImageCalc : public virtual IMcBase
{
public:
//////////////////////////////////////////////////////////////////////////
	class IImageCalcChangeCallBack
#ifdef __EMSCRIPTEN__
		: public IMcEmscriptenCallbackRef
#endif
	{
	public:
		virtual ~IImageCalcChangeCallBack() {}

		/// \n
		virtual void OnImageCalcChanged(IMcImageCalc *pImageCalc)  = 0;

		//==============================================================================
		// Method Name: Release()
		//------------------------------------------------------------------------------
		/// A callback that should release callback class instance.
		///
		///	Can be implemented by the user to optionally delete callback class instance when 
		/// IMcImageCalc instance is been removed or IMcImageCalc::UnregisterForImageCalcChanges() is called.
		//==============================================================================
		virtual void Release()  {}
	};

	/// structure defining coefficients for the rational function polynomial equations (RPC)
	struct SRationalPolynomialCoefficients 
	{
		double dErrBias;					///< Error - Bias. The RMS bias error in meters per horizontal axis of all points in the image (-1.0 if unknown)
		double dErrRand;					///< Error - Random. RMS random error in meters per horizontal axis of each point in the image (-1.0 if unknown)
		double adOffsets[5];				///< Offsets [Line Sample Long Lat Height]
		double adScales[5];					///< Scales  [Line Sample Long Lat Height]
		double adLineNumCoefficients[20];	///< Line Numerator Coefficients for the polynomial in the Numerator of the Rn equation.
		double adLineDenCoefficients[20];	///< Line Numerator Coefficients for the polynomial in the Denominator of the Rn equation.
		double adSampleNumCoefficients[20];	///< Line Numerator Coefficients for the polynomial in the Numerator of the Cn equation.
		double adSampleDenCoefficients[20];	///< Line Numerator Coefficients for the polynomial in the Denominator of the Cn equation.
	};
//////////////////////////////////////////////////////////////////////////////

protected:
	virtual ~IMcImageCalc() {};

public :
	typedef enum {
		EIT_NONE		= 0,
		EIT_GALAXYAIDS	= 1,
		EIT_LOROP		= 2,
		EIT_FRAME		= 3,
		EIT_AFFINE		= 4,
		EIT_ORTHO		= 5,
		EIT_FRAME_MOSAIC= 6,
		EIT_USER_DEFINED= 7,
		EIT_NUM
	}EImageType;

	virtual bool IsEqual(IMcImageCalc *pOther)const = 0;

	virtual EImageType GetImageType()const = 0;

	virtual IMcErrors::ECode GetSpatialQueries(IMcSpatialQueries **ppSpatialQueries) const = 0;

	//==============================================================================
	// Method Name: WorldCoordToImagePixel()
	//------------------------------------------------------------------------------
	/// Converts world point coordinates to image pixel coordinates
	///
	/// \param[in]	WorldCoord				world point coordinates
	/// \param[out]	pImagePixel				image pixel coordinates
	/// \param[out]	peIntersectionStatus	whether the image point was found
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode WorldCoordToImagePixel(
		const SMcVector3D &WorldCoord, SMcVector2D *pImagePixel,
		IMcErrors::ECode *peIntersectionStatus = NULL) const=0;

	//==============================================================================
	// Method Name: WorldCoordToImagePixelWithCache()
	//------------------------------------------------------------------------------
	/// Converts world point coordinates to image pixel coordinates using a cache mechanism
	///
	/// \param[in]	WorldCoord				world point coordinates
	/// \param[out]	pImagePixel				image pixel coordinates
	/// \param[out]	peIntersectionStatus	whether the image point was found
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode  WorldCoordToImagePixelWithCache(
		const SMcVector3D &WorldCoord, SMcVector2D *pImagePixel,
		IMcErrors::ECode *peIntersectionStatus = NULL) const = 0;

	//==============================================================================
	// Method Name: ImagePixelToCoordWorldOnHorzPlane()
	//------------------------------------------------------------------------------
	/// Converts image pixel coordinates to world point coordinates on horizontal plane
	///
	/// \param[in]	ImagePixel				image pixel coordinates
	/// \param[in]  dPlaneHeight			the plane height
	/// \param[out]	pWorldCoord				world point coordinates
	/// \param[out]	peIntersectionStatus	whether the world point was found
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ImagePixelToCoordWorldOnHorzPlane(
		const SMcVector2D &ImagePixel, double dPlaneHeight, SMcVector3D *pWorldCoord,
		IMcErrors::ECode *peIntersectionStatus = NULL) const = 0;

	//==============================================================================
	// Method Name: ImagePixelToCoordWorld()
	//------------------------------------------------------------------------------
	/// Converts image pixel coordinates to world point coordinates
	///
	/// \param[in]	ImagePixel				image pixel coordinates
	/// \param[out]	pWorldCoord				world point coordinates
	/// \param[out]	pbDTMAvailable			whether a terrain exists to perform intersections with (`true`) or default height is used (`false`)
	/// \param[out]	peIntersectionStatus	whether the world point was found
	/// \param[in]	pAsyncQueryCallback		optional callback for asynchronous query; if not NULL: the query will be performed asynchronously; 
	///										the function will return without results, when the results are ready a callback function 
	///										will be called IMcSpatialQueries::IAsyncQueryCallback::OnRayIntersectionResults(); 
	///										the same callback instance can be used again in another query only after the first query is completed; 
	///										the arguments of the callback function will be:
	///										- `bIntersectionFound = (*peIntersectionStatus == IMcErrors::SUCCESS)`
	///										- `*pIntersection = *pWorldCoord`
	///										- `pNormal`: unused
	///										- `*pdDistance = (*pbDTMAvailable ? 1 : 0)`
	/// \note
	/// The query can be performed asynchronously only with map layers based on MapCore's Map Layer Server; with local map layers it can be performed 
	/// via asynchronous callback, but in that case the calculation will be performed synchronously, the callback with the results will be called 
	/// before the function is ended and the function will return without results.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ImagePixelToCoordWorld(
		const SMcVector2D &ImagePixel, SMcVector3D *pWorldCoord,
		bool *pbDTMAvailable = NULL,
		IMcErrors::ECode *peIntersectionStatus = NULL,
		IMcSpatialQueries::IAsyncQueryCallback *pAsyncQueryCallback = NULL) const=0;

	//==============================================================================
	// Method Name: ImagePixelToCoordWorldWithCache()
	//------------------------------------------------------------------------------
	/// Converts image pixel coordinates to world point coordinates using a cache mechanism
	///
	/// \param[in]	ImagePixel				image pixel coordinates
	/// \param[out]	pWorldCoord				world point coordinates
	/// \param[out]	pbDTMAvailable			whether a terrain exists to perform intersections with (`true`) or default height is used (`false`)
	/// \param[out]	peIntersectionStatus	whether the world point was found
	/// \param[in]	pAsyncQueryCallback		optional callback for asynchronous query; if not NULL: the query will be performed asynchronously; 
	///										the function will return without results, when the results are ready a callback function 
	///										will be called IMcSpatialQueries::IAsyncQueryCallback::OnRayIntersectionResults(); 
	///										the same callback instance can be used again in another query only after the first query is completed; 
	///										the arguments of the callback function will be:
	///										- `bIntersectionFound = (*peIntersectionStatus == IMcErrors::SUCCESS)`
	///										- `*pIntersection = *pWorldCoord`
	///										- `pNormal`: unused
	///										- `*pdDistance = (*pbDTMAvailable ? 1 : 0)`
	/// \note
	/// The query can be performed asynchronously only with map layers based on MapCore's Map Layer Server; with local map layers it can be performed 
	/// via asynchronous callback, but in that case the calculation will be performed synchronously, the callback with the results will be called 
	/// before the function is ended and the function will return without results.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ImagePixelToCoordWorldWithCache(
		const SMcVector2D &ImagePixel, SMcVector3D *pWorldCoord,
		bool *pbDTMAvailable = NULL,
		IMcErrors::ECode *peIntersectionStatus = NULL,
		IMcSpatialQueries::IAsyncQueryCallback *pAsyncQueryCallback = NULL) const = 0;

	//=================================================================================================
	// 
	// Function name: int,   TwoImagesPixelsToWorldCoords (...)
	// 
	// Author     : Marina Gupshpun
	// Date       : 02/07/2007
	//-------------------------------------------------------------------------------------------------
	/// calculate world coords from pair of correlated image coords			
	/// \param [in ]    numberOfcorrelatedPixels  is number of correlated pixels
	/// \param [in ]    thisImagePixels is array of image coords of pixels of the current image
	/// \param [in ]    otherImagePixels is array of image coords of pixels of the other image
	/// \param [in ]    otherImageCalc  is image calculation of the other image
	/// \param [out]    worldCoords is array of world coords of calculated points, this array will be allocated by user 
	/// \param [out]    successfulWorldCoords  is array of indicators of calculated points if them can be used, this array will be allocated by user

	/// \return
	///     - status result
	/// \note
	/// Used for calculate world coords from pair of correlated image coords
	//=================================================================================================	
	virtual IMcErrors::ECode TwoImagesPixelsToWorldCoords(int                numberOfcorrelatedPixels,
								             const SMcVector2D	thisImagePixels[],
											 const SMcVector2D	otherImagePixels[],
											 IMcImageCalc *     otherImageCalc,
											 SMcVector3D worldCoords[],
											 bool successfulWorldCoords[])=0;


	virtual IMcErrors::ECode ImageToWorldRay(const SMcVector2D &ImagePixel, SMcVector3D *pRayOrigin, SMcVector3D *pRayDirection) const = 0;

	//==============================================================================
	// Method Name: IsWorldCoordVisible()
	//------------------------------------------------------------------------------
	/// Checks whether a target is visible from a scouter and from what height the scouter or target will enable visibility.
	///
	/// The method works with a DTM with or without static objects.
	///
	/// \param[in]	WorldCoord							The point's world coordinates to check a visibility for.
	/// \param[out]	pbIsVisible							Whether or not the point is visible (according to DTM data).
	/// \param[in]	pAsyncQueryCallback					optional callback for asynchronous query; if not NULL: the query will be performed asynchronously; 
	///													the function will return without results, when the results are ready a callback function 
	///													will be called: IMcSpatialQueries::IAsyncQueryCallback::OnPointVisibilityResults()); 
	///													the same callback instance can be used again in another query only after the first query is completed;
	///													the arguments of the callback function will be:
	///													- `bIsTargetVisible = *pbIsVisible`
	///													- `pdMinimalTargetHeightForVisibility`: unused
	///													- `pdMinimalScouterHeightForVisibility`: unused
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode IsWorldCoordVisible(const SMcVector3D &WorldCoord, bool *pbIsVisible, 
		IMcSpatialQueries::IAsyncQueryCallback *pAsyncQueryCallback = NULL) = 0;

	virtual IMcErrors::ECode CalcRPC (int nPixelDensity, IMcImageCalc::SRationalPolynomialCoefficients *pRPC) const = 0;


	virtual IMcErrors::ECode CalculateHeightAboveGround( SMcVector2D stGroundPixel,
											SMcVector2D stUpperPixel, 
											double *dHeight )  = 0;

	
	virtual IMcErrors::ECode CalculateBoxVolume( SMcVector2D stBasePixel,
									SMcVector2D stTopOfBasePixel, 
									SMcVector2D stTopPixel1, 
									SMcVector2D stTopPixel2, 
									double *dVolume )  = 0;


	//==============================================================================
	// Method Name: GetHeight()
	//------------------------------------------------------------------------------
	/// Retrieves the height of terrains (DTM with or without static objects) at the requested world point.
	///
	/// \param[in]	x						world point's x coordinate
	/// \param[in]	y						world point's y coordinate
	/// \param[out]	h						height of the highest target
	/// \param[in]	pAsyncQueryCallback		optional callback for asynchronous query; if not NULL: the query will be performed asynchronously; 
	///										the function will return without results, when the results are ready a callback function 
	///										will be called IMcSpatialQueries::IAsyncQueryCallback::OnTerrainHeightResults(); 
	///										the same callback instance can be used again in another query only after the first query is completed; 
	///										the arguments of the callback function will be:
	///										- `bHeightFound = (return_value == IMcErrors::SUCCESS)`
	///										- `dHeight = *h`
	///										- `pNormal`: unused
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetHeight(double x, double y, double *h,
		IMcSpatialQueries::IAsyncQueryCallback *pAsyncQueryCallback = NULL)const=0;

// 	virtual void GetDTMHandle(long *plDTMHandle)const=0;
// 	virtual void GetDTMResolution(long *plParFileResol)const=0;
// 	virtual void SetDTMResolution(long lParFileResol)=0;
// 	virtual IMcErrors::ECode  SetRectForDtm (SMcVector3D stRequestedStartLoc,
// 								 SMcVector3D stRequestedEndLoc,
// 								 SMcVector3D *pstActualStartLoc,
// 								 SMcVector3D *pstActualEndLoc)=0;
	virtual IMcErrors::ECode GetGeoImageFileName(char sFileName[255])const=0;
	virtual IMcErrors::ECode GetGridCoordSys(IMcGridCoordinateSystem **ppGridCoordSys) const = 0;

	virtual IMcErrors::ECode  GetLines( int * nLines ) const=0 ;
	virtual IMcErrors::ECode  GetSamples( int * nSamples ) const=0; 

	virtual IMcErrors::ECode 	  GetMinGSD(double * dMinGSD )=0;
	virtual IMcErrors::ECode 	  GetMaxGSD(double * dMaxGSD )=0;

	virtual IMcErrors::ECode  GetWorkingAreaValid(bool  *bWorkingAreaValid)const=0;
	virtual IMcErrors::ECode  SetWorkingAreaValid(bool  bWorkingAreaValid)=0;
	virtual IMcErrors::ECode  SetWorkingArea(SMcVector3D arrWorkingArea[4])=0;
	virtual IMcErrors::ECode  GetWorkingArea(SMcVector3D arrWorkingArea[4])const=0;
	virtual IMcErrors::ECode  IsInWorkingArea(const SMcVector3D &WorldCoord, bool *pbIsIn)const=0;


	virtual IMcErrors::ECode  GetPixelWorkingAreaValid(bool  *	bPixelWorkingAreaValid)const=0;
	virtual IMcErrors::ECode  SetPixelWorkingAreaValid(bool		bPixelWorkingAreaValid)=0;
	virtual IMcErrors::ECode  SetPixelWorkingArea(
		const SMcVector2D &AreaLowerLeft, const SMcVector2D &AreaUpperRight)=0;
	virtual IMcErrors::ECode  GetPixelWorkingArea(
		SMcVector2D *pAreaLowerLeft, SMcVector2D *pAreaUpperRight)const=0;
	virtual IMcErrors::ECode  IsInPixelWorkingArea(const SMcVector2D &PixelCoord, bool *pbIsIn)const=0;

	//=================================================================================================
	// 
	// Function name: int, GetDefaultHeight(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Get default height 
	///
	/// \param [out] height	the default height which was set during the lorop import process
	///
	/// \note
	/// this height can be used when there is no DTM to intersect LOS with
	///
	/// \return
	///     - status result
	//=================================================================================================  
	virtual IMcErrors::ECode  GetDefaultHeight( double *height) = 0;

	//=================================================================================================
	// 
	// Function name: int, SetDefaultHeight(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Set default height 
	///
	/// \param [in]	height		the default height set by the user  
	///
	/// \note
	///  this height can be used when there is no DTM to intersect LOS with
	///
	/// \return
	///     - status result
	//=================================================================================================  
	virtual IMcErrors::ECode  SetDefaultHeight( const double height ) = 0;

	virtual void RegisterForImageCalcChanges(IImageCalcChangeCallBack *pImageCalcChange) = 0;

	virtual void UnregisterForImageCalcChanges(IImageCalcChangeCallBack *pImageCalcChange) = 0;

	// cast functions. Add more for coming sensors
	virtual IMcAffineImageCalc *		CastToAffineImageCalc()		 = 0;
	virtual IMcFrameImageCalc *			CastToFrameImageCalc()		 = 0;
	virtual IMcFrameIC *				CastToFrameIC()		 = 0;
// 	virtual IMcGalaxyAidsImageCalc* CastToGalaxyAidsImageCalc()  = 0;
	virtual IMcLoropImageCalc *			CastToLoropImageCalc()		 = 0;
//	virtual IMcFrameMosaicImageCalc *	CastToFrameMosaicImageCalc() = 0;
	virtual IMcUserDefinedImageCalc *	CastToUserDefinedImageCalc() = 0;
};
