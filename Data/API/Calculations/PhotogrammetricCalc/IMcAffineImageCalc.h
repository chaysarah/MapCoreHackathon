#ifndef _WIN32_WCE
#pragma once
//===========================================================================
/// \file IMcAffineImageCalc.h
/// The interface for Affine images in the photogrammetric calculation package
//===========================================================================

// #include "PhotogrammetricDefs.h"
#include "Calculations/PhotogrammetricCalc/IMcImageCalc.h"

//===========================================================================
// Interface Name: IMcAffineImageCalc
//---------------------------------------------------------------------------
///	The interface for Affine images in the photogrammetric calculation package
/// 
//===========================================================================
class IMcAffineImageCalc : 
	virtual public IMcImageCalc
{
protected:
	virtual ~IMcAffineImageCalc() {};
public :


	virtual EImageType GetImageType()const{return EIT_AFFINE;}

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API Create(IMcAffineImageCalc **ppImageCalc,
		const char * strImageDataFileName, 
		IMcDtmMapLayer *iDtmMapLayer,
		IMcGridCoordinateSystem *pGridCoordinateSystem);

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API Create(IMcAffineImageCalc **ppImageCalc,
		const char * strImageDataFileName, 
		IMcMapTerrain *const apMapTerrains[], UINT uNumMapTerrains,
		IMcGridCoordinateSystem *pGridCoordinateSystem);
};
#endif //_WIN32_WCE
