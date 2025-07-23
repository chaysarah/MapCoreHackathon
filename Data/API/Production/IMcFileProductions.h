#pragma once
//===========================================================================
/// \file IMcFileProductions.h
/// The File Productions interface
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "SMcVector.h"
#include "Calculations/IMcGridCoordinateSystem.h"
class	IMcLoropImageCalc;
class	IMcDtmMapLayer;

/// The file productions static interface
class IMcFileProductions
{
public:
	struct SGeoReferencingParams
	{
		SMcVector3D FirstCornerInDiagonal; //in true coordinate units, without the scale factor
		SMcVector3D SecondCornerInDiagonal;
		double dAzimDeg; // azimuth of rectangles left edge (degrees) 
		double dGSD;     // in the true coordinate units
	} ;

	/// resampling method
	enum EResamplingMethod
	{
		ERM_NEAREST_NEIGHBORHOOD = 0,	///< nearest neighborhood method
		ERM_BILINEAR = 1				///< bilinear method
	} ;

	static FILEPRODUCTIONS_API IMcErrors::ECode   GenerateOrthophotoFromLoropImage(PCSTR strSourceLoropTifFileFullPath,	//in
		IMcLoropImageCalc				*pSourceLoropImage,				//in
		EResamplingMethod			eSourceResamplingMethod,	//in
		const SGeoReferencingParams		&DestOrthopothoParams,			//in
		int								nDestOrthopothoTileSize,		//in must be in size divided to 16:  16, 32, 64 ... 
		BYTE							uDestOrthopothoBackgroundGrayLevel, //in
		IMcDtmMapLayer					*pDestOrthophotoDtmMapLayer,//in
		double							dDestOrthophotoDefaultHeight,//in
		IMcGridCoordinateSystem			*pDestOrthophotoCoordSys,//in
		PCSTR							strDestOrthophotoTifFileFullPath,//in
		PCSTR							strDestOrthophotoTfwFileFullPath,//in
		int								nCompressionFactor = -1 //in
		);


	static FILEPRODUCTIONS_API IMcErrors::ECode   GenerateMosaicFromOrthophotos(
		PCSTR							astrSourceOrthophotoTifFileFullPathArray[],	//in	 
		PCSTR							astrSourceOrthophotoTfwFileFullPathArray[],	//in
		IMcGridCoordinateSystem			*pSourceOrthophotoCoordSys,		//in
		UINT							uNumberOfSourceOrthophoto,	//in
		EResamplingMethod			eSourceResamplingMethod,	//in
		const SGeoReferencingParams		&DestMosaicParams,		//in
		int								nDestMosaicTileSize,	//in
		BYTE							uDestMosaicBackgroundGrayLevel,//in
		IMcGridCoordinateSystem			*pDestMosaicCoordSys,			//in
		PCSTR							strDestMosaicTifFileFullPath,	//in
		PCSTR							strDestMosaicTfwFileFullPath,//in
		int								nCompressionFactor = -1 //in	
		);

	//=================================================================================================
	// 
	// Function name: int,   GenerateDTMFromPointsCloud (...)
	// 
	// Author     : Marina Gupshpun
	// Date       : 02/07/2007
	//-------------------------------------------------------------------------------------------------
	/// generate DTM from cloud's points			
	/// \param [in]    aCloudPoints  is array of cloud's points 
	/// \param [in]    aUsedCloudPoints is array of indicators if coresponding points from cloud will be used
	/// \param [in]    uNumCloudPoints is number of cloud's points
	/// \param [in]    strNewDTMPath   path to output DTM folder
	/// \param [in]    sExistingDTMPath   path to existing DTM folder
	/// \param [in]    nDTMResolution  is resolution(cell size) of DTM grid
	/// \param [in]    DtmMinPoint is minimal point of of DTM region
	/// \param [in]    DtmMaxPoint is maximal point of of DTM region
	/// \param [in]    dMaxSearchRadiusForHeightCalculation  is max search radius for calculate one DTM grid point
	/// \param [in]    nMinCloudPointsForHeightCalculation  is minimal number of points for calculate one DTM grid point
	/// \return
	///   - error code if the function fails, value 0 if the function successes.
	/// \remarks
	/// Used for generate DTM from cloud's points
	//=================================================================================================	
	static FILEPRODUCTIONS_API IMcErrors::ECode   GenerateDTMFromPointsCloud(
		const SMcVector3D	aCloudPoints[],
		const bool			aUsedCloudPoints[],
		UINT				uNumCloudPoints,
		PCSTR				strNewDTMPath,
		PCSTR				sExistingDTMPath,
		int					nDTMResolution,
		const SMcVector2D	&DtmMinPoint,
		const SMcVector2D	&DtmMaxPoint,
		double				dMaxSearchRadiusForHeightCalculation,
		int					nMinCloudPointsForHeightCalculation);

// 	static FILEPRODUCTIONS_API IMcErrors::ECode   GenerateTiledTiffImageFromTiff(PCSTR				sSourceTifFileFullPath,	//in
// 		PCSTR	sSourceTfwFileFullPath,
// 		MAP_WORLD_t *		stSourceWorldParams,		//in, if NULL than stDestWorldParams must be alsoNULL
// 		// in this case, the source and dest are considered as the same
// 		EmCresamplingMethodType	eSourceResamplingMethod,	//in
// 		const stGeoReferencingParams	*stDestOrthopothoParams, //in, if NULL than dest image will have
// 		// the same geo reference (TFW) as source
// 		int					nDestOrthopothoTileSize,		//in must be in size divided to 16:  16, 32, 64 ... 
// 		BYTE 					nDestOrthopothoBackground[3], //in, always RGB, even if image is gray level
// 		MAP_WORLD_t *			stDestWorldParams,			//in, see stSourceWorldParams remark
// 		PCSTR					sDestOrthophotoTifFileFullPath,//in
// 		PCSTR					sDestOrthophotoTfwFileFullPath,//in
// 		int nCompressionFactor = -1 //in
// 		);
// 
	// 
	// static FILEPRODUCTIONS_API IMcErrors::ECode   GenerateTiledTiffImageFromCMP(PCSTR				sSourceParFileFullPath,	//in
	// 													   bool					bTileDest,		//in 
	// 													   PCSTR					sDestOrthophotoTifFolderPath,//in
	// 													   PCSTR					sDestOrthophotoTfwFolderPath,
	// 													   EMapUnits				EmapUnits, //in
	// 													   bool					bIsGeoProjection);
	// 

	static FILEPRODUCTIONS_API IMcErrors::ECode   GetFileParameters(
		PCSTR	strSourceFileFullPath,	//in
		UINT	*puImageWidth,
		UINT	*puImageHeight,
		bool	*pbIsTiled,
		UINT	*puTileWidth,
		UINT	*puTileHeight,
		UINT	*puStripHeight);


	static FILEPRODUCTIONS_API IMcErrors::ECode   GeoReferencingParamsToTFW(
		const SGeoReferencingParams	*pOrthopothoParams, //in
		UINT						uTileSize,	//in, when not tiled use 1 
		double						adTFWParams[6]);

	static FILEPRODUCTIONS_API IMcErrors::ECode   GeoTFWToReferencingParams(
		const  double	adTFWParams[6], //in
		UINT			uImageWidth,
		UINT			uImageHeight,
		SGeoReferencingParams	*pOrthopothoParams);



// 	//=================================================================================================
// 	// 
// 	// Function name: int,   GetTFWParams (...)
// 	// 
// 	// Author     : Omer Shelef
// 	// Date       : 10/02/2008
// 	//-------------------------------------------------------------------------------------------------
// 	/// get the TFW six parameters from tfw file or from GeoTif			
// 	/// \param [in ]    sSourceTifFileFullPath  The path to the GeoTif file. If the file is not GeoTif then 
// 	///					the parameter should be NULL and only sSourceTfwFileFullPath will be used.
// 	/// \param [in ]    sSourceTfwFileFullPath  The path to the GeoTif file. If the file is GeoTif then 
// 	///					the parameter should be NULL and only sSourceTifFileFullPath will be used.
// 	/// \param [out]    arrdTFWParams  Array containing the six TFW parameters.
// 
// 	/// \return
// 	///   - error code if the function fails, value 0 if the function successes.
// 	/// \remarks
// 	//=================================================================================================	
// 	static FILEPRODUCTIONS_API IMcErrors::ECode   GetTFWParams(PCSTR	sSourceTifFileFullPath,	//in
// 		PCSTR	sSourceTfwFileFullPath,
// 		double	arrdTFWParams[6]);
// 

};
