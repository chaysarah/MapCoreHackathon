#pragma once
//===========================================================================
/// \file IMcGridCoordinateSystem.h
/// The Grid Coordinate System interfaces
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcBase.h"
#include "SMcVector.h"
#include "McCommonTypes.h"
#include "CMcDataArray.h"

#ifndef MAPCORE_GEOGRAPHIC_UNITS
#define MAPCORE_GEOGRAPHIC_UNITS 100000
#endif // MAPCORE_GEOGRAPHIC_UNITS

class IMcGridCoordSystemGeographic	;		
class IMcGridCoordSystemGeocentric	;		
class IMcGridCoordSystemTraverseMercator;		
class IMcGridTMUserDefined			;		
class IMcGridUTM					;			
class IMcGridNewIsrael				;		
class IMcGridS42					;			
class IMcGridKKJ		 			;			
class IMcGridRT90		 			;			
class IMcGridCoordSystemRSO			;		
class IMcGridRSOSingapore			;			
class IMcGridCoordSystemLambertConicConformic	;
class IMcGridIndiaLCC				;
class IMcGridMGRS;
class IMcGridBNG;
class IMcGridGARS;
class IMcGridGEOREF;
class IMcGridIrish;
class IMcGridNZMG;
class IMcGridGeneric;

//===========================================================================
// Interface Name: IMcGridCoordinateSystem
//---------------------------------------------------------------------------
/// The base interface for all coordinate systems. 
///
//===========================================================================
class IMcGridCoordinateSystem : virtual public IMcBase
{
protected:
	virtual ~IMcGridCoordinateSystem() {};
public:


	virtual UINT GetGridCoorSysType() const = 0;


	virtual bool IsEqual(const IMcGridCoordinateSystem *pOtherCoordinateSystem)  const = 0;

	/// Datum type definition
	enum EDatumType
	{
		EDT_USER_DEFINED	  = 0, ///< Initialized by SDatumParams\n
		EDT_WGS84			  = 1, ///<\n
		EDT_ED50_ISRAEL		  = 2, ///<\n
		EDT_PULKOVO42_POLAND  = 3, ///<\n
		EDT_HERMANSKOGEL	  = 4, ///<\n
		EDT_NAD27			  = 5, ///<\n	
		EDT_KKJ				  = 6, ///<\n
		EDT_INDIAN_EVEREST56  = 7, ///<\n
		EDT_RT90_BESSEL1841	  = 8, ///<\n
		EDT_NAD83			  = 9, ///<\n
		EDT_PULKOVO_GEORGIA	  = 10,///<\n 
		EDT_IND				  = 11,///<\n 
		EDT_NZGD1949		  = 12, //<\n
		EDT_NZGD2000		  = 13, //<\n
		EDT_SAD69			  = 14, //<\n
		EDT_PULKOVO_KAMIN	  = 15, //<\n
		EDT_KERTAU			  = 16, //<\n
		EDT_ED50_MEAN		  = 17, //<\n
		EDT_PULKOVO_KZ		  = 18, //<\n
		EDT_PULKOVO_RU		  = 19, //<\n
		EDT_OSGB			  = 20, //<\n
		EDT_IRISH1965		  = 21, //<\n
		EDT_NUM = 22 //<\n 
	};
	/// Geographic coordinate system definition
	struct SDatumParams
	{
		double dA; ///< Equatorial Radius in meters
		double dF; ///< Flattening
		double dDX; ///< Delta X shift to WGS84 datum in meters
		double dDY; ///< Delta Y shift to WGS84 datum in meters
		double dDZ; ///< Delta Z shift to WGS84 datum in meters
		double dRx; ///< Rotation angle to WGS84 datum around X axis in arc seconds
		double dRy; ///< Rotation angle to WGS84 datum around Y axis in arc seconds
		double dRz; ///< Rotation angle to WGS84 datum around Z axis in arc seconds
		double dS;	///< Delta scale factor to WGS84 datum 

		// constructor parameters are for WGS84 datum
		SDatumParams() 
		{
			dA  = 6378137.0;
			dF  = 1/298.257223563;
			dDX  = 0;
			dDY  = 0;
			dDZ  = 0;
			dRx = 0;
			dRy = 0;
			dRz = 0;
			dS  = 0;
		};

		SDatumParams(double _dA,
					 double _dF, 
					 double _dDX, 
					 double _dDY, 
					 double _dDZ, 
					 double _dRx, 
					 double _dRy, 
					 double _dRz, 
					 double _dS )	:
			dA  (_dA), 
			dF  (_dF), 
			dDX (_dDX), 
			dDY (_dDY), 
			dDZ (_dDZ), 
			dRx (_dRx), 
			dRy (_dRy), 
			dRz (_dRz), 
			dS  (_dS) {	};
	};
	virtual EDatumType GetDatum() const = 0;
//	virtual IMcErrors::ECode SetDatum(const EDatumType eDatum) = 0;
	virtual SDatumParams GetDatumParams() const = 0;

	virtual bool IsGeographicLocationLegal(const SMcVector3D &Location) const = 0;

	virtual bool IsLocationLegal(const SMcVector3D &Location) const = 0;

	virtual SMcBox GetLegalValuesForGeographicCoordinates() const = 0;

	virtual SMcBox GetLegalValuesForGridCoordinates() const = 0;

	virtual IMcErrors::ECode SetLegalValuesForGeographicCoordinates(const SMcBox& LegalValues) = 0;

	virtual IMcErrors::ECode SetLegalValuesForGridCoordinates(const SMcBox& LegalValues) = 0;

	virtual bool IsMultyZoneGrid() const = 0;

	virtual int	GetZone() const = 0;

	//==============================================================================
	// Method Name: GetOgcCrsCode()
	//------------------------------------------------------------------------------
	/// Retrieves the coordinate system's OGC CRS code (if can be found)
	///
	/// \param[out]	pstrOgcCrsCode	The fully qualified CRS code. If cannot be found, "urn:ogc:def:nil:OGC:unknown" will be returned.
	///
	/// \return
	///     - `true` if can be found, `false` otherwise.
	//==============================================================================
	virtual bool GetOgcCrsCode(CMcString *pstrOgcCrsCode) const = 0;

	virtual IMcErrors::ECode GetDefaultZoneFromGeographicLocation(const SMcVector3D &GeographicLocation,
		int *pnZone) const = 0;

	virtual	IMcGridCoordSystemGeographic				* CastToGridCoordSystemGeographic			() = 0; 
	virtual	IMcGridCoordSystemGeocentric				* CastToGridCoordSystemGeocentric			() = 0; 
	virtual	IMcGridCoordSystemTraverseMercator			* CastToGridCoordSystemTraverseMercator		() = 0; 
	virtual	IMcGridTMUserDefined						* CastToGridTMUserDefined					() = 0; 
	virtual	IMcGridUTM									* CastToGridUTM								() = 0; 
	virtual	IMcGridMGRS									* CastToGridMGRS() = 0;
	virtual	IMcGridBNG									* CastToGridBNG() = 0;
	virtual	IMcGridGARS									* CastToGridGARS() = 0;
	virtual	IMcGridGEOREF								* CastToGridGEOREF() = 0;
	virtual	IMcGridIrish								* CastToGridIrish() = 0;
	virtual	IMcGridNewIsrael							* CastToGridNewIsrael() = 0;
	virtual	IMcGridS42									* CastToGridS42								() = 0; 
	virtual	IMcGridKKJ		 							* CastToGridKKJ		 						() = 0; 
	virtual	IMcGridRT90		 							* CastToGridRT90		 					() = 0; 
	virtual	IMcGridCoordSystemRSO						* CastToGridCoordSystemRSO					() = 0; 
	virtual	IMcGridRSOSingapore							* CastToGridRSOSingapore					() = 0; 
	virtual	IMcGridCoordSystemLambertConicConformic		* CastToGridCoordSystemLambertConicConformic() = 0; 
	virtual	IMcGridIndiaLCC								* CastToGridIndiaLCC						() = 0; 
	virtual	IMcGridNZMG		 							* CastToGridNZMG		 					() = 0; 
	virtual IMcGridGeneric                              * CastToGridGeneric						    () = 0;

	//==============================================================================
	// Method Name: IsGeographic()
	//------------------------------------------------------------------------------
	/// Checks if the coordinate system is geographic
	///
	/// \return
	///     - true if it is, otherwise false
	//==============================================================================
	virtual bool IsGeographic() const = 0;

	//==============================================================================
	// Method Name: IsUtm()
	//------------------------------------------------------------------------------
	/// IsUtm checks if the coordinate system is UTM
	///
	///
	/// \return
	///     - true if it is, otherwise false
	//==============================================================================
	virtual bool IsUtm() const = 0;

	//==============================================================================
	// Method Name: CloneAsGeneric()
	//------------------------------------------------------------------------------
	/// Creates generic coordinate system (IMcGridGeneric) equal to the coordinate system
	///
	/// Applicable only to IMcGridCoordinateSystem's descendant other than IMcGridGeneric
	///
	/// \param[out]	ppGeneric	generic coordinate system created or `NULL` if it is not found
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode CloneAsGeneric(IMcGridGeneric **ppGeneric) const = 0;

// 	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridCoordinateSystem **ppGrid,
// 		const SMcCoordinateSystemDefinition & sGridParams);
// 
// 	virtual SMcCoordinateSystemDefinition GetGridParams() const = 0;
// 
};

//===========================================================================
// Interface Name: IMcGridCoordSystemGeographic
//---------------------------------------------------------------------------
/// The interface for geographic(lat,long, height) coordinate systems. 
///
//===========================================================================
class IMcGridCoordSystemGeographic : virtual public IMcGridCoordinateSystem
{
protected:
	virtual ~IMcGridCoordSystemGeographic() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 0
	};
public:
	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridCoordSystemGeographic **ppGrid,
		EDatumType eDatum, const SDatumParams *pDatumParams = 0);

protected:
private:
};


//===========================================================================
// Interface Name: IMcGridCoordSystemGeocentric
//---------------------------------------------------------------------------
/// The interface for geocentric(Cartesian right handed X,Y,Z) coordinate systems. 
///
//===========================================================================
class IMcGridCoordSystemGeocentric : virtual public IMcGridCoordinateSystem
{
protected:
	virtual ~IMcGridCoordSystemGeocentric() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 1
	};
public:
	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridCoordSystemGeocentric **ppGrid,
		EDatumType eDatum, const SDatumParams *pDatumParams = 0);

protected:
private:
};


//===========================================================================
// Interface Name: IMcGridCoordSystemTraverseMercator
//---------------------------------------------------------------------------
/// The base interface for all Traverse Mercator coordinate systems. 
///
//===========================================================================
class IMcGridCoordSystemTraverseMercator : virtual public IMcGridCoordinateSystem
{
protected:
	virtual ~IMcGridCoordSystemTraverseMercator() {};

public:
	struct  STMGridParams
	{
		double dFalseNorthing; 
		double dFalseEasting; 
		double dCentralMeridian;
		double dLatitudeOfGridOrigin;
		double dScaleFactor;
		double dZoneWidth;

		STMGridParams() {}
		STMGridParams(
			double _dFalseNorthing,
			double _dFalseEasting,
			double _dCentralMeridian,
			double _dLatitudeOfGridOrigin,
			double _dScaleFactor,
			double _dZoneWidth) :
		dFalseNorthing			(_dFalseNorthing		),
		dFalseEasting 			(_dFalseEasting 		),
		dCentralMeridian		(_dCentralMeridian		),
		dLatitudeOfGridOrigin	(_dLatitudeOfGridOrigin	),
		dScaleFactor			(_dScaleFactor			),
		dZoneWidth				(_dZoneWidth			)
		{	}
	};

  	virtual STMGridParams GetTMParams() const = 0;


protected:
private:
};

//===========================================================================
// Interface Name: IMcGridTMUserDefined
//---------------------------------------------------------------------------
/// The interface for a general Traverse Mercator coordinate systems. 
/// The user can input any valid parameters defining the specific Traverse Mercator coordinate systems. 
///
//===========================================================================
class IMcGridTMUserDefined : virtual public IMcGridCoordSystemTraverseMercator
{
protected:
	virtual ~IMcGridTMUserDefined() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 2
	};
public:
	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridTMUserDefined **ppGrid,
		 const STMGridParams &GridParams, int nZone, EDatumType eDatum,const SDatumParams *pDatumParams = 0);

protected:
private:
};

//===========================================================================
// Interface Name: IMcGridUTM
//---------------------------------------------------------------------------
/// The interface for Universal Traverse Mercator (UTM) coordinate systems. 
/// This projection is valid for any datum specified.
/// Valid zone numbers are 1 - 60 for northern hemisphere, 
/// and -1 - -60 for southern hemisphere.
/// When converting to this grid, defining nZone=0 means using the most appropriate zone.
//===========================================================================
class IMcGridUTM : virtual public IMcGridCoordSystemTraverseMercator
{
protected:
	virtual ~IMcGridUTM() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 3
	};
public:


	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridUTM **ppGrid,
		 int nZone, EDatumType eDatum, const SDatumParams *pDatumParams = 0);
protected:
private:
};


//===========================================================================
// Interface Name: IMcGridMGRS
//---------------------------------------------------------------------------
/// The interface for Military Grid Reference System (MGRS) coordinate systems. 
/// This projection uses WGS84 datum.
/// Valid zone numbers are 1 - 60 for northern hemisphere, 
/// and -1 - -60 for southern hemisphere.
/// When converting to this grid, defining nZone=0 means using the most appropriate zone.
//===========================================================================
class IMcGridMGRS : virtual public IMcGridCoordSystemTraverseMercator
{
protected:
	virtual ~IMcGridMGRS() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 10
	};

	///< MGRS 100000k"m square 
	struct SSquare
	{
		char			cSquareFst;		///< Square Identification
		char			cSquareSnd;		///< Square Identification
		char			cBand;          ///< The band in a zone for MGRS
		short		    nZone;          ///< for MGRS projection zone. Legal values are -60 - 60.

	};
	/// MGRS location type
	struct SFullMGRS 
	{
		SMcVector3D		Coord;		   ///< X,Y,Z of the location	
		SSquare      Square;        ///< 100000k"m square
	};
public:


	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridMGRS **ppGrid);

	virtual IMcErrors::ECode CoordToFullMGRS(const SMcVector3D &Coord, IMcGridMGRS::SFullMGRS *pFullMGRS)const = 0;
	virtual IMcErrors::ECode FullMGRSToCoord(const IMcGridMGRS::SFullMGRS &FullMGRS, SMcVector3D *pCoord)const = 0;
protected:
private:
};



//===========================================================================
// Interface Name: IMcGridBNG
//---------------------------------------------------------------------------
/// The interface for Britain National Grid  (BNG) coordinate systems. 
/// This projection uses Airy 1830 datum.
//===========================================================================
class IMcGridBNG : virtual public IMcGridCoordSystemTraverseMercator
{
protected:
	virtual ~IMcGridBNG() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 12
	};

	/// BNG 100000k"m square 
	struct SSquare
	{
		char			cSquareFst;		///< Square Identification
		char			cSquareSnd;		///< Square Identification

	};
	/// BNG location type
	struct SFullBNG
	{
		SMcVector3D		Coord;		   ///< X,Y,Z of the location	
		SSquare			Square;        ///< 100000k"m square
	};
public:


	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridBNG **ppGrid);

	virtual IMcErrors::ECode CoordToFullBNG(const SMcVector3D &Coord, IMcGridBNG::SFullBNG *pFullBNG)const = 0;
	virtual IMcErrors::ECode FullBNGToCoord(const IMcGridBNG::SFullBNG &FullBNG, SMcVector3D *pCoord)const = 0;
protected:
private:
};


//===========================================================================
// Interface Name: IMcGridGARS
//---------------------------------------------------------------------------
/// The interface for Global Area Reference System  (GARS) coordinate systems.
///
/// This projection uses WGS84 datum. This projection is used for describing areas of 5*5 minute of Latitude / Longitude earth coverages.
/// \note
/// More information can be found here: https://earth-info.nga.mil/index.php?dir=coordsys&action=coordsys
/// or here: https://en.wikipedia.org/wiki/Global_Area_Reference_System
//===========================================================================
class IMcGridGARS : virtual public IMcGridCoordinateSystem
{
protected:
	virtual ~IMcGridGARS() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 13
	};

public:


	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridGARS **ppGrid);

	virtual IMcErrors::ECode CoordToFullGARS(const SMcVector3D &Coord, char strGARS5minute[8]) const = 0;
	virtual IMcErrors::ECode FullGARSToCoord(const char strGARS5minute[8], SMcVector3D *pCoord) const = 0;
protected:
private:
};


//===========================================================================
// Interface Name: IMcGridGEOREF
//---------------------------------------------------------------------------
/// The interface for World Geographic Reference System  (GEOREF) coordinate systems.
///
/// This projection uses WGS84 datum. This projection is used for describing areas of 0.01 minute of Latitude / Longitude earth coverages.
/// \note
/// More information can be found here: https://en.wikipedia.org/wiki/World_Geographic_Reference_System
//===========================================================================
class IMcGridGEOREF : virtual public IMcGridCoordinateSystem
{
protected:
	virtual ~IMcGridGEOREF() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 15
	};

public:


	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridGEOREF **ppGrid);

	virtual IMcErrors::ECode CoordToFullGEOREF(const SMcVector3D &Coord, char strGEOREF_ThousandthMinute[15]) const = 0;
	virtual IMcErrors::ECode FullGEOREFToCoord(const char strGEOREF_ThousandthMinute[15], SMcVector3D *pCoord) const = 0;
protected:
private:
};



//===========================================================================
// Interface Name: IMcGridIrish
//---------------------------------------------------------------------------
/// The interface for Irish Grid (TM65) coordinate systems.
///
/// This projection uses Modified Airy 1830 ellipsoid.
/// \note
/// More information can be found here: 
/// - https://www.osi.ie/wp-content/uploads/2015/04/The-Irish-Grid-A-Description-of-the-Coordinate-Reference-System-Used-in-Ireland.pdf
/// - https://www.osi.ie/wp-content/uploads/2015/05/transformations_booklet.pdf
//===========================================================================
class IMcGridIrish : virtual public IMcGridCoordSystemTraverseMercator
{
protected:
	virtual ~IMcGridIrish() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 14
	};

	/// Irish location type
	struct SFullIrish
	{
		SMcVector3D		Coord;		///< X,Y,Z of the location	
		char			cLetter;	///< 100000k"m square
	};
public:


	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridIrish **ppGrid);

	virtual IMcErrors::ECode CoordToFullIrish(const SMcVector3D &Coord, IMcGridIrish::SFullIrish *pFullIrish) const = 0;
	virtual IMcErrors::ECode FullIrishToCoord(const IMcGridIrish::SFullIrish &FullIrish, SMcVector3D *pCoord) const = 0;
protected:
private:
};



//===========================================================================
// Interface Name: IMcGridNewIsrael
//---------------------------------------------------------------------------
/// The interface for Israel's new grid coordinate systems(ITM). 
//===========================================================================
class IMcGridNewIsrael : virtual public IMcGridCoordSystemTraverseMercator
{
protected:
	virtual ~IMcGridNewIsrael() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 4
	};
public:
	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridNewIsrael **ppGrid);
protected:
private:
};

//===========================================================================
// Interface Name: IMcGridS42
//---------------------------------------------------------------------------
/// The interface for SYSTEM-42 grid coordinate systems(S-42). 
/// This projection is valid for various Pulkovo datums, all using the Krassovsky ellipsoid. 
/// When converting to this grid, defining nZone=0 means using the most appropriate zone.
//===========================================================================
class IMcGridS42 : virtual public IMcGridCoordSystemTraverseMercator
{
protected:
	virtual ~IMcGridS42() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 5
	};
public:
	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridS42 **ppGrid,
		 int nZone,EDatumType eDatum, const SDatumParams *pDatumParams = 0);
protected:
private:
};


//===========================================================================
// Interface Name: IMcGridRT90
//---------------------------------------------------------------------------
/// The interface for the Swedish grid coordinate systems(RT90). 
//===========================================================================
class IMcGridRT90 : virtual public IMcGridCoordSystemTraverseMercator
{
protected:
	virtual ~IMcGridRT90() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 7
	};
public:
	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridRT90 **ppGrid);
protected:
private:
};


//===========================================================================
// Interface Name: IMcGridNZMG
//---------------------------------------------------------------------------
/// The interface for the Old New Zealand Military Grid(NZMG). 
//===========================================================================
class IMcGridNZMG : virtual public IMcGridCoordinateSystem
{
protected:
	virtual ~IMcGridNZMG() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 11
	};
public:
	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridNZMG **ppGrid);
protected:
private:
};


/////////////////////// FORM HERE DOWN THE FUNCTIONS ARE NOT IMPLEMENTED /////////////////////////////////////

//===========================================================================
// Interface Name: IMcGridKKJ
//---------------------------------------------------------------------------
/// The interface for the Finnish national grid coordinate systems(KKJ). 
/// Valid zone numbers are 1 - 5. 
/// When converting to this grid, defining nZone=0 means using the most appropriate zone.
//===========================================================================
class IMcGridKKJ : virtual public IMcGridCoordSystemTraverseMercator
{
protected:
	virtual ~IMcGridKKJ() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 6
	};
public:
	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridKKJ **ppGrid,  int nZone);
protected:
private:
};



//===========================================================================
// Interface Name: IMcGridCoordSystemRSO
//---------------------------------------------------------------------------
/// The base interface for all rectified skew Orthomorphic (RSO) coordinate systems. 
///
//===========================================================================
class IMcGridCoordSystemRSO : virtual public IMcGridCoordinateSystem
{
protected:
	virtual ~IMcGridCoordSystemRSO() {};

public:
protected:
private:
};

//===========================================================================
// Interface Name: IMcGridRSOSingapore
//---------------------------------------------------------------------------
/// The interface for the Singapore grid coordinate systems. 
//===========================================================================
class IMcGridRSOSingapore : virtual public IMcGridCoordSystemRSO
{
protected:
	virtual ~IMcGridRSOSingapore() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 8
	};
public:
	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridRSOSingapore **ppGrid);
protected:
private:
};


//===========================================================================
// Interface Name: IMcGridCoordSystemLambertConicConformic
//---------------------------------------------------------------------------
/// The base interface for all Lambert Conic Conformic (LCC) coordinate systems. 
//===========================================================================
class IMcGridCoordSystemLambertConicConformic : virtual public IMcGridCoordinateSystem
{
protected:
	virtual ~IMcGridCoordSystemLambertConicConformic() {};

public:
protected:
private:
};


//===========================================================================
// Interface Name: IMcGridIndiaLCC
//---------------------------------------------------------------------------
/// The interface for India's grid coordinate system. 
/// Valid zone numbers are 1 - 10, using the enum EIndiaLCCZone. 
/// When converting to this grid, defining nZone=0 means using the most appropriate zone.
//===========================================================================
class IMcGridIndiaLCC : virtual public IMcGridCoordSystemLambertConicConformic
{
protected:
	virtual ~IMcGridIndiaLCC() {};
public:

	enum
	{
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 9
	};
public:

	enum EIndiaLCCZone
	{
		EILCCZ_NONE		= 0,
		EILCCZ_0   		= 1, 
		EILCCZ_1A  		= 2,
		EILCCZ_1B  		= 3,
		EILCCZ_2A  		= 4,
		EILCCZ_2B  		= 5,
		EILCCZ_3A  		= 6,
		EILCCZ_3B  		= 7,
		EILCCZ_4A  		= 8,
		EILCCZ_4B  		= 9,
		EILCCZ_TYPE_NUM = 10 
	};


	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridIndiaLCC **ppGrid,
		EIndiaLCCZone eZone, EDatumType eDatum, const SDatumParams *pDatumParams = 0);

	virtual IMcErrors::ECode GetDefaultZonesFromGeographicLocation(const SMcVector3D &GeographicLocation,
		EIndiaLCCZone *peZone1, EIndiaLCCZone *peZone2) const = 0;

protected:
private:
};

//==================================================================================
// Interface Name: IMcGridGeneric
//----------------------------------------------------------------------------------
/// Interface for generic coordinate system 
//==================================================================================
class IMcGridGeneric : virtual public IMcGridCoordinateSystem
{
protected:
	virtual ~IMcGridGeneric() {};
public:
	enum {
		//==============================================================================
		/// Grid Coordinate system unique ID for this interface
		//==============================================================================
		GRID_COOR_SYS_TYPE = 21
	};

	//==============================================================================
	// Method Name: IsEllipsoidOf()
	//------------------------------------------------------------------------------
	/// Checks whether the coordinate system is of the specified ellipsoid 
	///
	/// \param[in]	strEllipsoidName	Ellipsoid to check
	///
	/// \return
	///     - status result - match found or not. 
	//==============================================================================
	virtual bool IsEllipsoidOf(PCSTR strEllipsoidName) const = 0;

	//==============================================================================
	// Method Name: GetCreateParams()
	//------------------------------------------------------------------------------
	/// Retrieves the parameters used to in Create()
	///
	/// \param[out]	pastrCreateParams	The array whose meaning depends on Create() previously used and the meaning is:
	///									- in case of a single string: either SRID (spatial reference ID, e.g. "epsg:4326") or
	///									  a full initialization string (e.g. "+proj=utm +zone=32 +ellps=GRS80 +no_defs") depending on \p pbSRID
	///									- in case of an array of multiple strings: the grid parameters, each one contains one parameter string 
	///									  (e.g. "proj=utm", "zone=32", "ellps=GRS80")
	/// \param[out]	pbSRID				Whether or not \p pastrGridParams meaning is SRID (spatial reference ID)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCreateParams(CMcDataArray<PCSTR> *pastrCreateParams, bool *pbSRID) const = 0;

	//==============================================================================
	// Method Name: CloneAsNonGeneric()
	//------------------------------------------------------------------------------
	/// Creates non-generic coordinate system (IMcGridCoordinateSystem's descendant other than IMcGridGeneric) equal to the coordinate system
	///
	/// Applicable only to generic coordinate system created with SRID (spatial reference ID) string
	///
	/// \param[out]	ppNonGeneric	non-generic coordinate system created  or `NULL` if it is not found
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode CloneAsNonGeneric(IMcGridCoordinateSystem **ppNonGeneric) const = 0;

	//==============================================================================
	// Method Name: GetFullInitializationString()
	//------------------------------------------------------------------------------
	/// Retrieves the full initialization string of a specific spatial reference ID to be used in Create()
	///
	/// \param[in]	strSRID							SRID (spatial reference ID, e.g. "epsg:4326")
	/// \param[out]	pstrFullInitializationString	The full initialization string to be used in Create()
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API GetFullInitializationString(PCSTR strSRID, CMcString *pstrFullInitializationString);	

	//==============================================================================
	// Method Name: GetSupportedSRIDs()
	//------------------------------------------------------------------------------
	/// Retrieves the supported SRIDs (spatial reference IDs) along with their names
	///
	/// \param[out]	paSRIDs		The array of key-value pairs: **strKey** is a SRID itself (e.g. "epsg:4326"), **strValue** is its name (e.g "WGS 84")
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API GetSupportedSRIDs(CMcDataArray<SMcKeyStringValue>* paSRIDs);

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates a generic coordinate system based upon an initialization string
	///
	/// \param[out]	ppGrid					The returned grid coordinate system object
	/// \param[in]	strInitializationString	Either SRID (spatial reference ID, e.g. "epsg:4326") or
	///										full initialization string retrieved by GetFullInitializationString() (e.g. "+proj=utm +zone=32 +ellps=GRS80 +no_defs")
	/// \param[in]	bIsSRID					Whether strInitializationString is SRID (spatial reference ID) or full initialization string
	///
	/// \note
	///		PROJ_DATA folder should be accessible: either its full path should be defined in "MCPROJ_LIB" environment variable (except for Web version) 
	///		or it should be present as "PROJ_DATA" in its default location (Windows: inside the folder of McProj.dll/McProjD.dll, 
	///		Android: inside "/sdcard/MapCore/", Linux: inside the current directory, Web in node.js: inside the virtual directory "FS" mapped by calling 
	///		IMcMapDevice.MapNodeJsDirectory(), Web in browser: embedded in MapCore's code - the user should only initiate MapCore by creating IMcMapDevice)
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridGeneric **ppGrid, PCSTR strInitializationString, bool bIsSRID = true);

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates a generic coordinate system based upon data taken from the spatial reference
	///
	/// \param[out]	ppGrid			The returned grid object
	/// \param[in]	astrGridParams	An array of grid parameters, each one contains one parameter string, e.g. "proj=utm", "zone=32", "ellps=GRS80"
	/// \param[in]	uNumGridParams	The number of elements in the above array
	///
	/// \note
	///		PROJ_DATA folder should be accessible: either its full path should be defined in "MCPROJ_LIB" environment variable (except for Web version) 
	///		or it should be present as "PROJ_DATA" in its default location (Windows: inside the folder of McProj.dll/McProjD.dll, 
	///		Android: inside "/sdcard/MapCore/", Linux: inside the current directory, Web in node.js: inside the virtual directory "FS" mapped by calling 
	///		IMcMapDevice.MapNodeJsDirectory(), Web in browser: embedded in MapCore's code - the user should only initiate MapCore by creating IMcMapDevice)
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridGeneric **ppGrid, const PCSTR astrGridParams[], UINT uNumGridParams);
};
