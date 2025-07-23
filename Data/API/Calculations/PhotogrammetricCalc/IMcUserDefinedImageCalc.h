#ifndef _WIN32_WCE
#pragma once
//===========================================================================
/// \file IMcUserDefinedImageCalc.h
/// The interface for User Defined images in the photogrammetric calculation package
//===========================================================================

// #include "PhotogrammetricDefs.h"
#include "Calculations/PhotogrammetricCalc/IMcImageCalc.h"

//===========================================================================
// Interface Name: IMcUserDefinedImageCalc
//---------------------------------------------------------------------------
///	The interface for User Defined images in the photogrammetric calculation package
/// 
//===========================================================================
class IMcUserDefinedImageCalc : 
	virtual public IMcImageCalc
{
protected:
	virtual ~IMcUserDefinedImageCalc() {}
public :

	/// IMcUserDefinedImageCalc callback interface for Image to Ray conversions
	///
	/// Override its virtual member functions in derived class to implement 
	/// the desired code for Image to Ray conversions
	class ICallback 
#ifdef __EMSCRIPTEN__
		: public IMcEmscriptenCallbackRef
#endif
	{
	public:
		virtual ~ICallback() {}

		/// \n
		virtual bool ImageToRay(const SMcVector2D &ImagePixel, 
			SMcVector3D *pRayOrigin, SMcVector3D *pRayDirection) const { return false; }

		/// \n
		virtual bool ImageToCoord(const SMcVector2D &ImagePixel, 
			SMcVector3D *pCoord, bool *pbDTMAvailable, bool *pbImplemented) const { *pbImplemented = false; return false; }

		/// \n
		virtual bool WorldCoordToImagePixel(const SMcVector3D &WorldCoord, SMcVector2D *pImagePixel) const = 0;

		/// \n
		virtual double GetScale(const SMcVector3D &WorldCoord) const = 0;

		//==============================================================================
		// Method Name: Release()
		//------------------------------------------------------------------------------
		/// A callback that should release callback class instance.
		///
		///	Can be implemented by the user to optionally delete callback class instance when 
		/// IMcUserDefinedImageCalc instance is been removed.
		//==============================================================================
		virtual void Release()  {}
	};

	virtual EImageType GetImageType() const {return EIT_USER_DEFINED; }

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API Create(IMcUserDefinedImageCalc **ppImageCalc,
		IMcUserDefinedImageCalc::ICallback *pCallBack, IMcDtmMapLayer *pDtmMapLayer,
		IMcGridCoordinateSystem *pGridCoordinateSystem);

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API Create(IMcUserDefinedImageCalc **ppImageCalc,
		IMcUserDefinedImageCalc::ICallback *pCallBack, IMcMapTerrain *const apMapTerrains[], UINT uNumMapTerrains,
		IMcGridCoordinateSystem *pGridCoordinateSystem);

	virtual IMcErrors::ECode  SetQueryMaxDistance(
		const double dMaxDistance) =0;

	virtual IMcErrors::ECode  GetQueryMaxDistance( double *pdMaxDistance)const  =0;

	virtual IMcErrors::ECode  CameraModelChanged()  = 0;

	virtual IMcErrors::ECode SetFrameDimensions(int nPixelsNumX, int nPixelsNumY) = 0;
};
#endif //_WIN32_WCE
