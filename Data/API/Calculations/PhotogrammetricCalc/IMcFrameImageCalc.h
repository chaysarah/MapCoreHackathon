#pragma once
//===========================================================================
/// \file IMcFrameImageCalc.h
/// The interface for Frame images in the photogrammetric calculation package
//===========================================================================

//#include "PhotogrammetricDefs.h"
#include "Calculations/PhotogrammetricCalc/IMcImageCalc.h"
#include "CMcDataArray.h"

//===========================================================================
// Interface Name: IMcFrameIC
//---------------------------------------------------------------------------
///	The base interface for Frame images in the photogrammetric calculation package
/// 
//===========================================================================
class IMcFrameIC : 
	virtual public IMcImageCalc
{
public:
	struct SCameraParams
	{
		int				nPixelesNumX;
		int				nPixelesNumY;
		double 			dCameraOpeningAngleX;
		double 			dPixelRatio;			/// Xsize / Ysize
		double 			dCameraRoll;
		double 			dCameraPitch;
		double 			dCameraYaw;
		SMcVector3D		CameraPosition;
		double			dOffsetCenterPixelX;
		double			dOffsetCenterPixelY;
	};

	/// Types of Ray-intersection status
	typedef enum  {
		/// Indicates a valid intersection point
		ERS_Intersection = 0, 
		/// Indicates a valid horizon intersection point but not the corner itself.
		/// the returned point is the last visible point in the frame along the frame's edge.
		ERS_HorizonPoint, 
		/// Indicates no intersection point was found.
		ERS_NoIntersection
	}ERayStatus;

protected:
	virtual ~IMcFrameIC() {};
public :

	virtual IMcErrors::ECode  SetCameraParams(
		const SCameraParams& stParams)  = 0;

	virtual IMcErrors::ECode  GetCameraParams( SCameraParams *pParams)const  = 0;

	virtual IMcErrors::ECode  SetQueryMaxDistance(
		const double dMaxDistance)  = 0;

	virtual IMcErrors::ECode  GetQueryMaxDistance( double *pdMaxDistance)const  = 0;
	//=================================================================================================
	// 
	// Function name: GetCameraCornersAndCenter(...)
	// 
	// Author     : Omer Shelef
	//-------------------------------------------------------------------------------------------------
	/// Calculates the ground footprint trace of a given frame camera's center point and four corners.
	///
	/// \param[in]	bCalcHorizon		In case no intersection was found, determines if the function should try to 
	///										search for the relevant horizon point along frame's edge.
	/// \param[out]	arrCenterAndCorners An array of locations of the ground footprint trace. The order is:
	///										1. Center point (Mid pixel)
	///										2. Top left.
	///										3. Top right.
	///										4. Bottom right.
	///										5. Bottom left.
	/// \param[out]	arrRayStatus		An array of the returned results status corresponding to the order of \a arrCenterAndCorners. 
	///										- If \p bCalcHorizon is false only #ERS_Intersection and #ERS_NoIntersection are relevant. 
	///										- If \p bCalcHorizon is true #ERS_HorizonPoint is also relevant. 
	/// \param[in]	pAsyncQueryCallback		optional callback for asynchronous query; if not NULL: the query will be performed asynchronously, 
	///										the function will return without results, when the results are ready a callback function 
	///										IMcSpatialQueries::IAsyncQueryCallback::OnRayIntersectionTargetsResults() will be called; 
	///										the same callback instance can be used again in another query only after the first query is completed; 
	///										the arguments of the callback function will be (for `i` from 0 to 4):
	///										- aIntersections[i].IntersectionPoint = arrCenterAndCorners[i]
	///										- aIntersections[i].eTargetType` according to arrRayStatus[i]: 
	///											- IMcSpatialQueries::EITT_NONE in case of IMcFrameIC::ERS_NoIntersection,
	///											- IMcSpatialQueries::EITT_DTM_LAYER in case of IMcFrameIC::ERS_Intersection, 
	///											- IMcSpatialQueries::EITT_STATIC_OBJECTS_LAYER in case of IMcFrameIC::ERS_HorizonPoint,
	///										- other members of IMcSpatialQueries::STargetFound: unused.
	/// \return
	///     - status result
	///
	/// \note
	/// The frame-camera parameters for calculation are the last entered by SetCameraParams()
	//=================================================================================================

	virtual IMcErrors::ECode GetCameraCornersAndCenter(bool bCalcHorizon,
		SMcVector3D arrCenterAndCorners[5], ERayStatus arrRayStatus[5],
		IMcSpatialQueries::IAsyncQueryCallback *pAsyncQueryCallback = NULL) = 0;
};

//===========================================================================
// Interface Name: IMcFrameImageCalc
//---------------------------------------------------------------------------
///	The interface for FrameImageCalc images in the photogrammetric calculation package
/// 
//===========================================================================
class IMcFrameImageCalc : 
	virtual public IMcFrameIC
{
protected:
	virtual ~IMcFrameImageCalc() {};
public :

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API Create(IMcFrameImageCalc **ppImageCalc,
		const SCameraParams& Params, 
		IMcDtmMapLayer *iDtmMapLayer,
		IMcGridCoordinateSystem *pGridCoordinateSystem);

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API Create(IMcFrameImageCalc **ppImageCalc,
		const SCameraParams& Params, 
		IMcMapTerrain *const apMapTerrains[], UINT uNumMapTerrains,
		IMcGridCoordinateSystem *pGridCoordinateSystem);

	virtual EImageType GetImageType()const{return EIT_FRAME;}
};

// class IMcFrameMosaicImageCalc : 
// 	virtual public IMcFrameIC
// {
// protected:
// 	virtual ~IMcFrameMosaicImageCalc() {};
// public :
// 
// 	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API Create(IMcFrameMosaicImageCalc **ppImageCalc,
// 		const SCameraParams &Params, IMcDtmMapLayer *iDtmMapLayer, 
// 		IMcGridCoordinateSystem *pGridCoordinateSystem, UINT nDeviceIndex, double dDefaultHeight, UINT nMaxCacheSize);
// 
// 	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API Create(IMcFrameMosaicImageCalc **ppImageCalc,
// 		const SCameraParams &Params, IMcMapTerrain *const apMapTerrains[], UINT uNumMapTerrains, 
// 		IMcGridCoordinateSystem *pGridCoordinateSystem, UINT nDeviceIndex, double dDefaultHeight, UINT nMaxCacheSize);
// 
// 	virtual EImageType GetImageType()const{return EIT_FRAME_MOSAIC;}
// 
// 	virtual IMcErrors::ECode   SetDefaultHeight( const double height )  = 0;
// 
// 	virtual IMcErrors::ECode AddImages(SCameraParams *arrImageFiles, UINT nImagesNum)  = 0;
// 
// 	virtual IMcErrors::ECode ClearImages()  = 0;
// 
// 	virtual IMcErrors::ECode SaveCurrentState(UINT nStateId)  = 0;
// 
// 	virtual IMcErrors::ECode RetrieveState(UINT nStateId)  = 0;
// 
// 	virtual IMcErrors::ECode SetState(const IMcFrameIC::SCameraParams &MosaicParams,
// 		const IMcFrameIC::SCameraParams aImagesParams[], UINT uNumImages)  = 0;
// 
// 	// if auImagesIDs is NULL than fast mode with no cache
// 	virtual IMcErrors::ECode SetImagesData(UINT uNumImages, UINT uSizeX,  UINT uSizeY, 
// 		void *const aImagesBuffers[], 
// 		const UINT auImagesIDs[])  = 0;
// 
// 	virtual IMcErrors::ECode GetImagesInCurrentState(
// 		CMcDataArray<UINT> *parrIds, CMcDataArray<bool> *parrDataExists)  = 0;
// 
// 	virtual IMcErrors::ECode CreateCurrentStateMosaic(bool arrbOverlapFound[]) = 0;
// 
// 	virtual IMcErrors::ECode GetMosaicData(void* buffer) const = 0;
// 
// 	virtual IMcErrors::ECode GetMinMosaicBoundingRect(double dRectYaw,
// 		SMcVector3D Rect[4]) const = 0;
// 
// 	virtual IMcErrors::ECode GetBestImage(const SMcVector3D &WorldLoc, 
// 		const IMcFrameImageCalc **pImageCalc) const = 0;
//};
