#ifndef _WIN32_WCE
#pragma once
//===========================================================================
/// \file IMcLoropImageCalc.h
/// The interface for Lorop images in the photogrammetric calculation package
//===========================================================================

//#include "PhotogrammetricDefs.h"
#include "Calculations/PhotogrammetricCalc/IMcImageCalc.h"

#define IDPATH 256
#define MAX_NUM_OF_CORRECTION_COEFS 10
#define MAX_PARAM_NAME	100

//===========================================================================
// Interface Name: IMcLoropImageCalc
//---------------------------------------------------------------------------
///	The interface for Lorop images in the photogrammetric calculation package
/// 
//===========================================================================
class IMcLoropImageCalc : 
	virtual public IMcImageCalc
{
protected:
	virtual ~IMcLoropImageCalc() {};
public :


	typedef unsigned long long MCCOVARIANCEHANDLE;

	enum  McScanDirectionType
	{
		MC_RIGHT_SCAN, 
		MC_LEFT_SCAN,
		MC_UNKNOWN_DIRECTION
	};

	enum McCoordSysType
	{
		MC_ECF_CS, 
		MC_GEO_CS,
		MC_UNKNOWN_CS
	};

	enum McRangeStatus
	{
		MC_LOROP_INSIDE_IMAGE,
		MC_LOROP_OUTSIDE_IMAGE,
		MC_LOROP_TOO_FAR
	};

	enum McDtmStatus 
	{
		// there was not use of any DTM at all
		MC_LOROP_DTM_NOT_USED,
		// DTM worked ok and the height for the requested coordinates
		// was provided
		MC_LOROP_DTM_OK,
		// DTM heights were provided except some points along the sight ray
		MC_LOROP_DTM_PARTLY_USED,
		// DTM could not supply height for the requested coordinates
		// and 
		MC_LOROP_DTM_APPROXIMATED,
		// the iterative algorithm that is used
		// with DTM did not converged within maximal
		// number of iterations allowed
		MC_LOROP_DTM_NOT_CONVERGED,
		// critical error in DTM
		MC_LOROP_DTM_ERROR
	};


	enum McPointType
	{
		MC_NONE_POINT,
		MC_CONTROL_POINT,
		MC_TIE_POINT
	};


	enum McSpectrumType 
	{
		MC_SPECTRUM_VISIBLE, 
		MC_SPECTRUM_IR, 
		MC_SPECTRUM_VISIBLE_IN_SIMULTANEOUS,
		MC_SPECTRUM_IR_IN_SIMULTANEOUS,
		MC_UNKNOWN_SPECTRUM_TYPE
	};

#pragma pack( push, BytesPack, 1)

	struct loropImageTileAnn 
	{

		enum
		{ 
			TOTAL_LEN			= 258,
			UNUSED_LEN			= 145, 
			CHECKSUM_BYTES_COUNT= 2, 
			USED_LEN			= TOTAL_LEN - UNUSED_LEN - CHECKSUM_BYTES_COUNT			
		};

		DWORD	timeOfDay;		// VPU time, units:seconds, range 0-86400, resolution: 0.001
		BYTE targetID[3]; 	// Unique target's ID in the leg, units:N/A, range: 0-9 
		// ----->(replaces DeuLrpFormat::TargetID which was originally in the DEU)
		WORD	frameID;		// Frame ID, units:N/A, range: 0-500
		BYTE	spareA;			//
		WORD	sectorID;		// Sector ID, units:N/A, range 0-1000
		BYTE	tileID;			// Tile ID, units:N/A, range 0-3 for vis. 0 for IR
		BYTE	navPointsValidity; 	// Which navigation points are valid, units:N/A

		BYTE	navPoint1Line;		// Line of navigation point 1, units:N/A, range 0-127 
		long	navPoint1Phi;		// Roll angle at navigation point 1, units:degrees, range: -180 - +180 
		// resolution: 180/2^31
		long	navPoint1Teta;		// Elevation angle at navigation point 1, units:degrees, range: -180 - +180 
		// resolution: 180/2^31
		long	navPoint1Psi;		// Azimuth angle at navigation point 1, units:degrees, range: -180 - +180 
		// resolution: 180/2^31

		BYTE	navPoint2Line;		// Line of navigation point 2, units:N/A, range 0-127 
		long	navPoint2Phi;		// Roll angle at navigation point 2, units:degrees, range: -180 - +180 
		// resolution: 180/2^31
		long	navPoint2Teta;		// Elevation angle at navigation point 2, units:degrees, range: -180 - +180 
		// resolution: 180/2^31
		long	navPoint2Psi;		// Azimuth angle at navigation point 2, units:degrees, range: -180 - +180 
		// resolution: 180/2^31

		BYTE	navPoint3Line;		// Line of navigation point 3, units:N/A , range 0-127 
		long	navPoint3Phi;		// Roll angle at navigation point 3, units:degrees, range: -180 - +180 
		// resolution: 180/2^31
		long	navPoint3Teta;		// Elevation angle at navigation point 3, units:degrees, range: -180 - +180 
		// resolution: 180/2^31
		long	navPoint3Psi;		// Azimuth angle at navigation point 3, units:degrees, range: -180 - +180 
		// resolution: 180/2^31

		BYTE	navPoint4Line;		// Line of navigation point 4, units:N/A , range 0-127 
		long	navPoint4Phi;		// Roll angle at navigation point 4, units:degrees, range: -180 - +180 
		// resolution: 180/2^31
		long	navPoint4Teta;		// Elevation angle at navigation point 4, units:degrees, range: -180 - +180 
		// resolution: 180/2^31
		long	navPoint4Psi;		// Azimuth angle at navigation point 4, units:degrees, range: -180 - +180 
		// resolution: 180/2^31

		BYTE	lrpLine;		// Line of Lrp navigation data, units:N/A, range 0-127
		long	lrpLatitude;		// Lrp latitude, units:degrees, range: -90  - +90, resolution: 90/2^31
		long	lrpLongitude;		// Lrp longitude, units:degrees, range: -180 - +180, resolution: 180/2^31
		SHORT	lrpAltitude;		// Lrp altitude, units:feets, range: -1060 - +80336, resolution: 4
		SHORT	lrpGroundTrack;		// Lrp true ground track, units:degrees, range: -180 - +180, resolution: 180/2^15
		long	lrpHeading;		// Lrp true heading, units:semicircle, range: -180 - +180, resolution: 180/2^31

		long	lrpVelocityX;		// INS velocity in X-axis, units:feet/sec, range: -3000 - 3000 resoluion: 1/2^18
		long	lrpVelocityY;		// INS velocity in Y-axis, units:feet/sec, range: -3000 - 3000 resoluion: 1/2^18
		long	lrpVelocityZ;		// INS velocity in Z-axis, units:feet/sec, range: -3000 - 3000 resoluion: 1/2^18

		SHORT	lrpGroundSpeed;		// Sensor ground speed, units:Knot, range: 0-4095, resolution: 1/2^5 
		// ----->( was originally in class DEU_LRP_DLL DeuLrpUsefulTileAnnotBase )
		WORD	lrpRoll;
		WORD	lrpPitch;
		BYTE	hybrifFom;			// Figure Of Merit. Is sampled at time of Lrp navigation data Line. range: 0-9 
		// ----->( was not originally in the DEU classes !)

		BYTE	navigationMode;

		WORD	lineRate;		// Camera actual line rate, units:lines/sec, range: 500-50000, resolution: 1
		// ----->( was originally in class DEU_LRP_DLL DeuLrpUsefulTileAnnotBase )
		BYTE	scanRate;		// Angular rate of the scanning mode. range: 0-2 0=low, 1=normal, 2=high
		// ----->( was originally in class DEU_LRP_DLL DeuLrpUsefulTileAnnotBase )

		BYTE	lrpModeWord1;		// Lrp mode word #1, units:N/A
		WORD	calculatedFocusSetting;
		WORD	measuredFocusSetting;
		BYTE	reserved[UNUSED_LEN];
		WORD	checkSum;		// Check sum of the application marker data, units:N/A
	} ;


	struct loropImageFooterAnn 
	{
		enum
		{ 
			MC_TOTAL_LEN			= 128,					// Length (in bytes) of the Frame footer
			MC_UNUSED_A_LEN		= 32,					// Length (in bytes) of 1st unused space	
			MC_USED_LEN			= 91,					// Length (in bytes) of used space			
			MC_UNUSED_LEN			= 3
		};

		// the fields below were not a part of the original DEU classes hence they will probably be unused !  
		long	nearEdgeStartLong;	//deg, range: -180 - +180, resolution: 180/2^31 
		//----->( was not originally in the DEU classes !)
		long	nearEdgeStartLat;	//deg, range: -90  - +90, resolution: 90/2^31  
		//----->( was not originally in the DEU classes !)
		long	nearEdgeEndLong;	//deg, range: -180 - +180, resolution: 180/2^31 
		//----->( was not originally in the DEU classes !)
		long	nearEdgeEndLat;	//deg, range: -90  - +90, resolution: 90/2^31  
		//----->( was not originally in the DEU classes !)
		long	farEdgeStartLong;	//deg, range: -180 - +180, resolution: 180/2^31 
		//----->( was not originally in the DEU classes !)
		long	farEdgeStartLat;	//deg, range: -90  - +90, resolution: 90/2^31  
		//----->( was not originally in the DEU classes !)
		long	farEdgeEndLong;	//deg, range: -180 - +180, resolution: 180/2^31 
		//----->( was not originally in the DEU classes !)
		long	farEdgeEndLat;		//deg, range: -90  - +90, resolution: 90/2^31  
		//----->( was not originally in the DEU classes !)				
		// end of fields which were not a part of the original code in the DEU

		////////////////////////////////////////////////////////////////////////////////////////	

		// from here are the fields which were in the original code of the DEU 
		WORD	nearEdgeAngle;	 	// Near edge depression angle, units:degrees, range, 0 - 30, resolution 30/2^16 
		WORD	farEdgeAngle;	 	// Far edge depression angle, units:degrees, range, 0 - 30, resolution 30/2^16

		WORD	scanAngle;	 	// Scan angle from near edge to far edge, units:degrees, range, 0 - 30, 
		// resolution 30/2^16
		BYTE	spareA[8];

		WORD	lrpSerialNumber; 	// Serial number of current mission's LRP, range: 0 - 255
		BYTE	cameraSerialNumber;	// Serial number of current mission's camera
		DWORD	missionNumber;		// Mission ID (IRS mission ID + IRS mission name)	
		//----->(replaces DeuLrpFormat::MissionID which was originally in the DEU)
		BYTE	targetId[3];
		WORD	frameID;		// Frame ID, units:N/A
		BYTE	spareB[5];
		BYTE	userDefinedOverlap;	// User defined overlap units: %, range: 1-100, resolution 1
		BYTE	lrpModeWord1;	 	// Lrp mode word #1, units:N/A
		WORD	lrpModeWord2;	 	// Lrp mode word #2, units:N/A   
		WORD	lrpModeWord3;	 	// Lrp mode word #3, units:N/A
		BYTE	spareC[2];
		DWORD	totalVisLinesCount;	// Total lines amount in the Vis frame, range:0-80000
		WORD	totalIrLinesCount; 	// Total lines amount in the IR frame, range:0-16000
		WORD	numberOfPixelsInVisibleLine;
		WORD	numberOfPixelsInIRLine;	
		DWORD	lrpLatitude;	 	// Lrp latitude, deg, range: -90 - +900, resolution: 90/2^31
		DWORD	lrpLongitude;	 	// Lrp longitude, deg, range: -180 - +180, resolution: 180/2^31
		WORD	lrpAltitude;	 	// Lrp altitude, units:feet, range: -1060 - +80336, resolution: 4
		BYTE	spareD[MC_UNUSED_A_LEN];
		WORD	checkSum;		// Check sum of the application marker data, units:N/A

		BYTE	spareE[MC_UNUSED_LEN];

	} ;


	struct McloropProcessedImageFooterAnn 
	{
		// the fields below were not a part of the original DEU classes hence they will probably be unused !  
		double	nearEdgeStartLong;	//deg, range: -180 - +180, resolution: 180/2^31 
		//----->( was not originally in the DEU classes !)
		double	nearEdgeStartLat;	//deg, range: -90  - +90, resolution: 90/2^31  
		//----->( was not originally in the DEU classes !)
		double	nearEdgeEndLong;	//deg, range: -180 - +180, resolution: 180/2^31 
		//----->( was not originally in the DEU classes !)
		double	nearEdgeEndLat;	//deg, range: -90  - +90, resolution: 90/2^31  
		//----->( was not originally in the DEU classes !)
		double	farEdgeStartLong;	//deg, range: -180 - +180, resolution: 180/2^31 
		//----->( was not originally in the DEU classes !)
		double	farEdgeStartLat;	//deg, range: -90  - +90, resolution: 90/2^31  
		//----->( was not originally in the DEU classes !)
		double	farEdgeEndLong;	//deg, range: -180 - +180, resolution: 180/2^31 
		//----->( was not originally in the DEU classes !)
		double	farEdgeEndLat;		//deg, range: -90  - +90, resolution: 90/2^31  
		//----->( was not originally in the DEU classes !)				
		// end of fields which were not a part of the original code in the DEU

		////////////////////////////////////////////////////////////////////////////////////////	

		// from here are the fields which were in the original code of the DEU 
		double	nearEdgeAngle;	 	// Near edge depression angle, units:degrees, range, 0 - 30, resolution 30/2^16 
		double	farEdgeAngle;	 	// Far edge depression angle, units:degrees, range, 0 - 30, resolution 30/2^16

		double	scanAngle;	 	// Scan angle from near edge to far edge, units:degrees, range, 0 - 30, 
		// resolution 30/2^16
		unsigned short	lrpSerialNumber; 	// Serial number of current mission's LRP, range: 0 - 255
		unsigned char	cameraSerialNumber;	// Serial number of current mission's camera
		unsigned int missionNumber;    // Mission ID (IRS mission ID + IRS mission name)	
		//----->(replaces DeuLrpFormat::MissionID which was originally in the DEU)
		unsigned char	targetId[3];

		//unsigned char date[4];		// Date,in format 'DDMMYYYY', units:N/A				
		//----->(replaces DeuLrpFormat::MissionID which was originally in the DEU)
		unsigned short	frameID;		// Frame ID, units:N/A
		unsigned char	userDefinedOverlap;
		unsigned char	lrpModeWord1;	 	// Lrp mode word #1, units:N/A
		unsigned short	lrpModeWord2;	 	// Lrp mode word #2, units:N/A   
		unsigned short	lrpModeWord3;	 	// Lrp mode word #3, units:N/A
		unsigned int	totalVisLinesCount;	// Total lines amount in the Vis frame, range:0-80000
		unsigned short	totalIrLinesCount; 	// Total lines amount in the IR frame, range:0-16000
		unsigned short	numberOfPixelsInVisibleLine;
		unsigned short	numberOfPixelsInIRLine;
		double	lrpLatitude;	 	// Lrp latitude, deg, range: -90 - +900, resolution: 90/2^31
		double	lrpLongitude;	 	// Lrp longitude, deg, range: -180 - +180, resolution: 180/2^31
		double	lrpAltitude;	 	// Lrp altitude, units:feet, range: -1060 - +80336, resolution: 4
		unsigned short	checkSum;	// Check sum of the application marker data, units:N/A	
	};

	struct SMcImageParameters 
	{
		char 	 imageId[IDPATH]; // must be unique id
		double 	 defaultHeight;	  // can be set to  0.0 by default 	
		int 	 numLines;		  // of the tiff image	
		int 	 numSamples;	  // of the tiff image
		unsigned int numTiles;
		// The annotation data necessary for creating a sensor model for an image
		loropImageTileAnn *loropImageTiles;
		loropImageFooterAnn	loropImageFooterAnn;
	} ;

#pragma pack( pop, BytesPack )



	struct McResultStatus
	{
		McRangeStatus rangeStatus;		
		McDtmStatus   dtmStatus;
	};



	/* Adjustment structs */
	struct SMcAdjustParameterData // for one parameter
	{
		// The name of the param.
		char name[100]; //input. can be also output in the GetDefaultAdjustParamsData() or GetAdjustParamsData()

		// Parameter accuracy
		double accuracy;//input

		// The correction polynom coefficient
		double correctionsCoefficient[MAX_NUM_OF_CORRECTION_COEFS]; // output

		/// num Coefficient
		int numCoef; // output.  correctionLevel + 1

		// degree to which polynom will be adjusted
		// generally correctionLevel < numCoef
		// if user assigns correctionLevel >= numCoefs then
		// degree of polynom is increased 
		int correctionLevel; // input between 0 - 2

		// correct or not
		bool needCorrection; //input


	} ;

	struct McSolvePointForImage
	{
		McPointType		point_type; 	//input. Type of the solvePoint
		char 			point_id[IDPATH];//input
		SMcVector3D 	groundPoint;	//input	
		SMcVector3D 	groundSigmas;	//input in (m)
		SMcVector3D 	groundResiduals;//output in (m)
		bool 			use_point; 		/* input false - solvePoint not used in solution */
		SMcVector2D		imagePoint;		//input
		SMcVector2D		imageSigmas;  	/*input data (in pixels) for each point weight*/
		SMcVector2D		imageResiduals; /*output data (in pixels) for each point*/
	} ;

	struct McSolvePointFor2Images
	{
		McPointType		point_type; 			/* input Type of the solvePoint */
		char 			point_id[IDPATH];		//input
		bool 			use_point; 				/* input false - solvePoint not used in solution */
		SMcVector3D 	groundPoint;			//input: control point 
		SMcVector3D 	groundSigmas;			//input in (m)
		SMcVector3D 	groundResiduals;		//output in (m)
		SMcVector2D		currentImagePoint;		//input	
		SMcVector2D		currentImageSigmas;		/*input data (in pixels) for each point weight*/
		SMcVector2D		currentImageResiduals;	/*output data (in pixels) for each point*/
		SMcVector2D		otherImagePoint;		//input	
		SMcVector2D		otherImageSigmas;		//input	
		SMcVector2D		otherImageResiduals;	/*output data (in pixels) for each point*/
	} ;

	struct McAdjustTerminationCriteria
	{
		int		 numIterations; // input: can be: numIterations = 100
		double   relativeChange;// input:  % from last solution. can be: relativeChange = 0.22
		int		 iterations; 	// output: can be: iterations = -1;
	} ;


	struct McImageSatisticData
	{

		char	name[100];// The model name.
		double	rms;      // output The models RMS
	} ;

	struct McAdjustStatisticData
	{

		double				 groundRms;		// output: ground rms 
		int					 numImages;		// input: num models. 1 for adjust of single image. 2 for adjust 0f 2 images
		McImageSatisticData* imagesData;	// output: vector rms of size numImages models 
		// must be allocated by the user	
	} ;

	struct McCovarianceParameterData
	{
		/// The name of the param.
		char name[MAX_PARAM_NAME]; 

		/// degree to which polynom will be adjusted
		/// generally correctionLevel < numCoef
		/// if user assigens correctionLevel >= numCoefs then
		/// degree of polynom is increased 
		int correctionLevel; 
	};


	virtual EImageType GetImageType()const{return EIT_LOROP;}


	//=================================================================================================
	// 
	// Function name: int, ImportImageData(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 11/19/2006
	//-------------------------------------------------------------------------------------------------
	/// imports LOROP image telemetry data (annotation)
	/// \param [in]			mcParams			the telemetry data (LOROP tiles annotation) array.
	/// \param [in]			LongRadius			geodetic ellipsoid's long axis radius 
	/// \param [in]			inverseFlattening	inverse of the ellipsoid flattening (1/f)
	/// \param [out]		xmlData				block of xml image data created after the import.
	/// \return
	///     - status result
	//=================================================================================================
	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  ImportImageData( const struct IMcLoropImageCalc::SMcImageParameters* mcParams,
		double LongRadius, 
		double inverseFlattening, 
		char **xmlData);

	//=================================================================================================
	// Function name: int, Create(...)
	//-------------------------------------------------------------------------------------------------
	/// creates a pointer for LOROP image object
	/// \param[out]	ppImageCalc				a pointer for the image object
	/// \param[in]	sImageData				can be: xml file name full path or block of xml image data.
	/// \param[in]	isFileName				whether xml file name full path or block of xml image data is used.
	/// \param[in]	iDtmMapLayer			an interface for DTM layer
	/// \param[in]	pGridCoordinateSystem	coordinate system definition
	/// \return
	///     - status result
	//=================================================================================================
	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API Create(IMcLoropImageCalc **ppImageCalc, const char * sImageData,
		bool isFileName, IMcDtmMapLayer *iDtmMapLayer, IMcGridCoordinateSystem *pGridCoordinateSystem);

	//=================================================================================================
	// Function name: int, Create(...)
	//-------------------------------------------------------------------------------------------------
	/// creates a pointer for LOROP image object
	/// \param[out]	ppImageCalc				a pointer for the image object
	/// \param[in]	sImageData				can be: xml file name full path or block of xml image data.
	/// \param[in]	isFileName				whether xml file name full path or block of xml image data is used.
	/// \param[in]	apMapTerrains			array of map terrains (each one with one DTM and optional static-objects layers)
	/// \param[in]	uNumMapTerrains			number of map terrains
	/// \param[in]	pGridCoordinateSystem	coordinate system definition
	/// \return
	///     - status result
	//=================================================================================================
	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API Create(IMcLoropImageCalc **ppImageCalc, const char * sImageData,
		bool isFileName, IMcMapTerrain *const apMapTerrains[], UINT uNumMapTerrains,
		IMcGridCoordinateSystem *pGridCoordinateSystem);

	//=================================================================================================
	// 
	// Function name: int, ReleaseXmlMemory(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 11/19/2006
	//-------------------------------------------------------------------------------------------------
	/// imports LOROP image telemetry data (annotation)
	/// \param [in]		xmlData		release the memory of a block of xml image data.
	/// \return
	///     - status result
	//=================================================================================================
	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  ReleaseXmlMemory(char **xmlData);

	//=================================================================================================
	// 
	// Function name: int, SaveToXmlData(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 11/19/2006
	//-------------------------------------------------------------------------------------------------
	/// creates xml file of a LOROP inage on the disk by a given name
	/// \param [in]		mcParams			the telemetry data (LOROP tiles annotation) array.
	/// \param [in]		LongRadius			geodetic ellipsoid's long axis radius 
	/// \param [in]		inverseFlattening	inverse of the ellipsoid flattening (1/f)
	/// \param [in]		outputFileName		xml file name.
	/// \return
	///     - status result
	//=================================================================================================
	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  SaveToXmlData( const struct IMcLoropImageCalc::SMcImageParameters* mcParams,
		double LongRadius,				
		double inverseFlattening,	
		const char* outputFileName);

	//=================================================================================================
	// 
	// Function name: int, LosToGround(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 11/19/2006
	//-------------------------------------------------------------------------------------------------
	/// Calculating ground point in a lorop image by intersecting a constant ground height from an origin point and LOS of the sensor   
	/// \param [in]		groundHeight		the ground height which is used for intersecting with the LOS (line Of Sight).
	/// \param [in]		coordSysType		coordinate system type (Geo. or UTM)
	/// \param [in]		origin				ground coordinate origin   
	/// \param [in]		orientation			unit vector in LOS direction
	/// \param [in]		LongRadius			geodetic ellipsoid's long axis radius 
	/// \param [in]		reverseFlattening	inverse of the ellipsoid flattening (1/f)
	/// \param [in]		useRefraction		use refraction correction (true/false)
	/// \param [out]	vector3dGp			target point ground position.
	/// \return
	///     - status result
	/// \note
	/// - origin is in image's working coordinate system
	/// - orientation is a unit vector in the direction of LOS
	/// - in LSR (Local Space Rectangular) C.S. that is tangent to the Earth in given origin
	//=================================================================================================
	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  LosToGround( double groundHeight,
		McCoordSysType coordSysType,
		SMcVector3D origin,
		SMcVector3D orientation,
		double LongRadius, 
		double reverseFlattening,
		bool   useRefraction,
		SMcVector3D * vector3dGp ); 

	//=================================================================================================
	// 
	// Function name: int, GetImagePathXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 11/19/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets image path from xml data block of a LOROP image 
	/// \param [in]		xmlData				the xml data block.
	/// \param [out]	imagePath			the image full path 
	/// \return
	///     - status result
	//=================================================================================================
	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetImagePathXml(const char* xmlData, char* imagePath);




	//=================================================================================================
	// 
	// Function name: int, GetRefractionCorrectionXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Get flag for refraction correction from Xml data block
	/// \param [in]		xmlData				the xml data block.
	/// \param [out]	use	get current flag for refraction corection
	/// \return
	///     - status result
	/// \note
	///  can be: true/false - use/dont use refraction corection
	//=================================================================================================  


	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetRefractionCorrectionXml(const char* xmlData, bool * use ) ;

	//=================================================================================================
	// 
	// Function name: int, GetImageIDXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 11/19/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets image ID from xml data block of a LOROP image 
	/// \param [in]		xmlData			the xml data block.
	/// \param [out]	imageID			the image unique ID in the xml data block of a LOROP image
	/// \return
	///     - status result
	//=================================================================================================

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetImageIDXml(const char* xmlData, char* imageID);

	//=================================================================================================
	// 
	// Function name: int, GetSensorTypeXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 11/19/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the sensor type from xml data block of a LOROP image 
	/// \param [in]		xmlData			the xml data block.
	/// \param [out]	sensorType		the current image sensor type( visible or IR )
	/// \return
	///     - status result
	//=================================================================================================

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetSensorTypeXml(const char* xmlData, char* sensorType);
	//=================================================================================================
	// 
	// Function name: int, GetPhotoDateXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 11/19/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the photo date from xml data block of a LOROP image 
	/// \param [in]		xmlData			the xml data block.
	/// \param [out]	photoDate		the image photo date 
	/// \return
	///     - status result
	//=================================================================================================
	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetPhotoDateXml(const char* xmlData, char* photoDate);


	//=================================================================================================
	// 
	// Function name: int, GetDefaultHeightXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Get default height from Xml data block 
	/// \param [in]		xmlData				the xml data block.
	/// \param [out] height		the default height which was set during the lorop import process
	/// \return
	///     - status result
	/// \note
	/// this height can be used when there is no DTM to intersect LOS with
	//=================================================================================================  

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API   GetDefaultHeightXml( const char* xmlData, double *height);

	//=================================================================================================
	// 
	// Function name: int, GetLinesXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 11/19/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the lines of the image from xml data block of a LOROP image 
	/// \param [in]		xmlData			the xml data block.
	/// \param [out]	lines		the sum of lines in the image
	/// \return
	///     - status result
	/// \note
	/// units - pixels
	//=================================================================================================

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetLinesXml(const char* xmlData, int* lines);

	//=================================================================================================
	// 
	// Function name: int, GetSamplesXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the samples of the image from xml data block of a LOROP image 
	/// \param [in]		xmlData		the xml data block.
	/// \param [out]	samples		the sum of saples in the image
	/// \return
	///     - status result
	/// \note
	/// units - pixels
	//=================================================================================================

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetSamplesXml(const char* xmlData, int* samples);


	//=================================================================================================
	// 
	// Function name: int, GetGroundSpeedXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the ground speed of the sensor from xml data block of a LOROP image 
	/// \param [in]		xmlData		the xml data block.
	/// \param [out]	groundSpeed	the ground speed
	/// \return
	///     - status result
	/// \note
	/// speed units - knots
	//=================================================================================================

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetGroundSpeedXml(const char* xmlData, double* groundSpeed);

	//=================================================================================================
	// 
	// Function name: int, GetScanDirectionXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the scan direction of the sensor from xml data block of a LOROP image 
	/// \param [in]		xmlData			the xml data block.
	/// \param [out]	sMcScanType		the scan direction of the current image
	/// \return
	///     - status result
	/// \note
	/// can be right or left
	//=================================================================================================

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetScanDirectionXml(const char* xmlData, McScanDirectionType* sMcScanType);

	//=================================================================================================
	// 
	// Function name: int, GetFocalXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the focal length of the sensor from xml data block of a LOROP image 
	/// \param [in]		xmlData		the xml data block.
	/// \param [out]	focal		the focal length 
	/// \return
	///     - status result
	/// \note
	///  focal length units - millimeters
	//=================================================================================================

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetFocalXml(const char* xmlData, double* focal);

	//=================================================================================================
	// 
	// Function name: int, GetAngularScanRateXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the angular scan rate of the sensor from xml data block of a LOROP image 
	/// \param [in]		xmlData			the xml data block.
	/// \param [out]	angularScanRate	the angular scan rate of the sensor
	/// \return
	///     - status result
	/// \note
	///  angular scan rate units - degrees / sec
	//=================================================================================================

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetAngularScanRateXml(const char* xmlData, double* angularScanRate);

	//=================================================================================================
	// 
	// Function name: int, GetLineRateXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the line rate of the sensor from xml data block of a LOROP image 
	/// \param [in]		xmlData		the xml data block.
	/// \param [out]	lineRate	the line rate of the sensor
	/// \return
	///     - status result
	/// \note
	///  line rate units - lines / sec
	//=================================================================================================

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetLineRateXml(const char* xmlData, double* lineRate);
	//=================================================================================================
	// 
	// Function name: int, GetFovAlongScanXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the FOV of the sensor from xml data block of a LOROP image 
	/// \param [in]		xmlData		the xml data block.
	/// \param [out]	fov			the FOV of the sensor along scan directon
	/// \return
	///     - status result
	/// \note
	/// FOV along scan units - degrees
	//================================================================================================= 

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetFovAlongScanXml(const char* xmlData, double* fov);

	//=================================================================================================
	// 
	// Function name: int, GetScanAngleXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the scan angle of the sensor from xml data block of a LOROP image 
	/// \param [in]		xmlData		the xml data block.
	/// \param [out]	scanAngle	the scan angle of the sensor 
	/// \return
	///     - status result
	/// \note
	/// scan angle units - degrees
	//================================================================================================= 

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetScanAngleXml(const char* xmlData, double* scanAngle);

	//=================================================================================================
	// 
	// Function name: int, GetPixelSizeXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the pixel size from xml data block of a LOROP image 
	/// \param [in]		xmlData		the xml data block.
	/// \param [out]	pixelSize	the pixel size of the current image 
	/// \return
	///     - status result
	/// \note
	///  pixel size units - millimeters
	//=================================================================================================  
	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetPixelSizeXml(const char* xmlData, double* pixelSize);

	//=================================================================================================
	// 
	// Function name: int, GetScanDurationXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the scan duration from xml data block of a LOROP image 
	/// \param [in]		xmlData			the xml data block.
	/// \param [out]	scanDuration	the scan duration of the current image 
	/// \return
	///     - status result
	/// \note
	///  scan duration units - seconds
	//=================================================================================================  

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetScanDurationXml(const char* xmlData, double* scanDuration);

	//=================================================================================================
	// 
	// Function name: int, GetFlightAzimuthXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the flight azimuth from xml data block of a LOROP image 
	/// \param [in]		xmlData			the xml data block.
	/// \param [out]	flightAzimuth	the flight azimuth of the sensor in the current image 
	/// \return
	///     - status result
	/// \note
	/// flight azimuth units - degrees clockwise from north
	//=================================================================================================  

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetFlightAzimuthXml(const char* xmlData, double* flightAzimuth);

	//=================================================================================================
	// 
	// Function name: int, GetGsdFarEdgeXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the Gsd of the far edge from xml data block of a LOROP image 
	/// \param [in]		xmlData		the xml data block.
	/// \param [out]	gsd	the		Gsd of the far edge in the current image 
	/// \return
	///     - status result
	/// \note
	/// gsd units - meteres / pixel (the square root of pixel area on the ground)
	//=================================================================================================  
	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetGsdFarEdgeXml(const char* xmlData, double* gsd);

	//=================================================================================================
	// 
	// Function name: int, GetGsdNearEdgeXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the Gsd of the near edge from xml data block of a LOROP image 
	/// \param [in]		xmlData		the xml data block.
	/// \param [out]	gsd	the		Gsd of the near edge in the current image 
	/// \return
	///     - status result
	/// \note
	/// gsd units - meteres / pixel (the square root of pixel area on the ground)
	//=================================================================================================  

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetGsdNearEdgeXml(const char* xmlData, double* gsd);
	//=================================================================================================
	// 
	// Function name: int, GetFootprintsXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the image footprint from xml data block of a LOROP image 
	/// \param [in]		xmlData		the xml data block.
	/// \param [out]	footPrints	the	image footprint arrary of 4 points of the current image 
	/// \return
	///     - status result
	/// \note
	/// Units in working coordinate system:
	///  - ECF - meters
	///  - GEO - degrees. \n
	/// - Default for working coordinate system is GEO. 
	/// - Order of coordinate output: UL,UR,LR,LL. 
	/// - The Z value returned is the default height of the image.
	//=================================================================================================   

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API GetFootprintsXml(const char* xmlData, SMcVector3D footPrints[4]); 

	//=================================================================================================
	// 
	// Function name: int, GetFooterXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the Footer data from xml data block of a LOROP image 
	/// \param [in]		xmlData	the xml data block.
	/// \param [out]	footer	the	Footer data of the current image 
	/// \return
	///     - status result
	//=================================================================================================  

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetFooterXml(const char* xmlData, IMcLoropImageCalc::McloropProcessedImageFooterAnn* footer);

	//=================================================================================================
	// 
	// Function name: int, getSpectrumXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the camera spectrum type from xml data block of a LOROP image 
	/// \param [in]		xmlData		the xml data block.
	/// \param [out]	spectrum	spectrum can be: Visible or IR
	/// \return
	///     - status result
	//=================================================================================================  

	static IMcErrors::ECode PHOTOGRAMMETRICCALC_API  GetSpectrumXml( const char* xmlData, McSpectrumType* spectrum) ;

	//=================================================================================================
	// 
	// Function name: int, SetGeoReference(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Set the ground coordinate system 
	/// \param [in]		type	ground coordinate system type
	/// \return
	///     - status result
	//=================================================================================================  
	/* Set/get the ground coordinate system */
	virtual IMcErrors::ECode   SetGeoReference( const McCoordSysType type ) = 0;

	//=================================================================================================
	// 
	// Function name: int, GetGeoReference(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Get the working ground	coordinate system 
	/// \param [out]	type	ground coordinate system type
	/// \return
	///     - status result
	//=================================================================================================  

	virtual IMcErrors::ECode   GetGeoReference(  McCoordSysType  *type ) = 0;


	//=================================================================================================
	// 
	// Function name: int, GetRefractionCorrection(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Get flag for refraction correction 
	/// \param [out]	use	get current flag for refraction corection
	/// \return
	///     - status result
	/// \note
	///  can be: true/false - use/dont use refraction corection
	//=================================================================================================  


	virtual IMcErrors::ECode  GetRefractionCorrection( bool * use ) = 0 ;

	//=================================================================================================
	// 
	// Function name: int, SetRefractionCorrection(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Set flag for refraction correction 
	/// \param [in]	use	 set current flag for refraction corection
	/// \return
	///     - status result
	/// \note
	///  can be: true/false - use/dont use refraction corection
	//=================================================================================================  

	virtual IMcErrors::ECode  SetRefractionCorrection(  bool use ) = 0 ;

	//=================================================================================================
	// 
	// Function name: int, GetImagePath(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 11/19/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets image path of a LOROP image 
	/// \param [out]	imagePath		the image full path 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode  GetImagePath( char* imagePath  ) = 0 ;

	//=================================================================================================
	// 
	// Function name: int, SetImagePath(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 11/19/2006
	//-------------------------------------------------------------------------------------------------
	/// Sets image path of a LOROP image 
	/// \param [in]	imagePath	the image full path 
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode  SetImagePath( char* imagePath  )= 0;

	//=================================================================================================
	// 
	// Function name: int, GetImageID(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 11/19/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets image ID of a LOROP image 
	/// \param [out]	imageId			the image unique ID of a LOROP image 
	/// \return
	///     - status result
	//=================================================================================================

	virtual IMcErrors::ECode  GetImageID( char* imageId) = 0;


	//=================================================================================================
	// 
	// Function name: int, GetSensorType(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 11/19/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the sensor type  of a LOROP image 
	/// \param [out]	sensorType		the current image sensor type( visible or IR )
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode  GetSensorType( char* sensorType)= 0;

	//=================================================================================================
	// 
	// Function name: int, GetPhotoDate(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 11/19/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the photo date of a LOROP image 
	/// \param [out]	photoDate		the image photo date 
	/// \return
	///     - status result
	//=================================================================================================

	virtual IMcErrors::ECode  GetPhotoDate( char* photoDate )= 0;

	// 	virtual void GetLines( int * lines )= 0;
	// 	virtual void GetSamples( int * samples )= 0;
	//  these functions are implemented in base class by filling members of base class
	//  in the constructor of CLoropImageCalc


	//=================================================================================================
	// 
	// Function name: int, GetGroundSpeed(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the ground speed of the sensor  
	/// \param [out]	groundSpeed	the ground speed
	/// \return
	///     - status result
	/// \note
	/// speed units - knots
	//=================================================================================================

	virtual IMcErrors::ECode  GetGroundSpeed( double * groundSpeed )= 0;

	//=================================================================================================
	// 
	// Function name: int, GetScanDirection(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the scan direction of the sensor  
	/// \param [out]	scanDir		the scan direction of the sensor in the current image
	/// \return
	///     - status result
	/// \note
	/// can be right or left
	//=================================================================================================

	virtual IMcErrors::ECode  GetScanDirection( McScanDirectionType * scanDir )= 0;

	//=================================================================================================
	// 
	// Function name: int, GetFocal(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the focal length of the sensor 
	/// \param [out]	focal		the focal length 
	/// \return
	///     - status result
	/// \note
	///  focal length units - millimeters
	//=================================================================================================

	virtual IMcErrors::ECode  GetFocal( double * focal)= 0;


	//=================================================================================================
	// 
	// Function name: int, GetAngularScanRate(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the angular scan rate  
	/// \param [out]	angularScanRate	the angular scan rate of the sensor
	/// \return
	///     - status result
	/// \note
	///  angular scan rate units - degrees / sec
	//=================================================================================================

	virtual IMcErrors::ECode  GetAngularScanRate( double * angularScanRate)= 0;


	//=================================================================================================
	// 
	// Function name: int, GetLineRate(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the line rate of the sensor  
	/// \param [out]	lineRate	the line rate of the sensor
	/// \return
	///     - status result
	/// \note
	///  line rate units - lines / sec
	//=================================================================================================

	virtual IMcErrors::ECode  GetLineRate( double * lineRate)= 0;

	//=================================================================================================
	// 
	// Function name: int, GetFovAlongScan(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the FOV of the sensor 
	/// \param [out]	fovAlongScan		the FOV of the sensor along scan directon
	/// \return
	///     - status result
	/// \note
	/// FOV along scan units - degrees
	//================================================================================================= 

	virtual IMcErrors::ECode  GetFovAlongScan( double * fovAlongScan)= 0;

	//=================================================================================================
	// 
	// Function name: int, GetScanAngle(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the scan angle of the sensor 
	/// \param [out]	getScanAngle	the scan angle of the sensor 
	/// \return
	///     - status result
	/// \note
	/// scan angle units - degrees
	//================================================================================================= 

	virtual IMcErrors::ECode 	GetScanAngle( double * getScanAngle )= 0;


	//=================================================================================================
	// 
	// Function name: int, GetPixelSize(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the pixel size of a LOROP image 
	/// \param [out]	pixelSize	the pixel size of the current image 
	/// \return
	///     - status result
	/// \note
	///  pixel size units - millimeters
	//=================================================================================================  
	virtual IMcErrors::ECode  GetPixelSize( double * pixelSize )= 0;

	//=================================================================================================
	// 
	// Function name: int, GetScanDuration(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the scan duration of a LOROP image 
	/// \param [out]	scanDuration	the scan duration of the current image 
	/// \return
	///     - status result
	/// \note
	///  scan duration units - seconds
	//=================================================================================================  

	virtual IMcErrors::ECode  GetScanDuration( double * scanDuration )= 0;

	//=================================================================================================
	// 
	// Function name: int, GetFlightAzimuth(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the flight azimuth of a LOROP image 
	/// \param [out]	flightAzimuth	the flight azimuth of the sensor in the current image 
	/// \return
	///     - status result
	/// \note
	/// flight azimuth units - degrees clockwise from north
	//=================================================================================================  

	virtual IMcErrors::ECode  GetFlightAzimuth( double * flightAzimuth)= 0;

	//=================================================================================================
	// 
	// Function name: int, GetGsdFarEdge(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the Gsd of the far edge in a LOROP image 
	/// \param [out]	gsdFarEdge	the Gsd of the far edge in the current image 
	/// \return
	///     - status result
	/// \note
	/// gsd units - meteres / pixel (the square root of pixel area on the ground)
	//=================================================================================================  
	virtual IMcErrors::ECode  GetGsdFarEdge( double * gsdFarEdge)= 0;


	//=================================================================================================
	// 
	// Function name: int, GetGsdNearEdgeXml(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the Gsd of the near edge of a LOROP image 
	/// \param [out]	gsdNearEdge the	Gsd of the near edge in the current image 
	/// \return
	///     - status result
	/// \note
	/// gsd units - meteres / pixel (the square root of pixel area on the ground)
	//=================================================================================================  

	virtual IMcErrors::ECode  GetGsdNearEdge( double * gsdNearEdge )= 0;

	//=================================================================================================
	// 
	// Function name: int, GetFootprints(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the image footprint of a LOROP image 
	/// \param [out]	sMcVector3D[]	the image footprint arrary of 4 points of the current image 
	/// \return
	///     - status result
	/// \note
	/// Units in working coordinate system
	///  - ECF - meters
	///  - GEO - degrees.\n
	/// - Default for working coordinate system is GEO. 
	/// - Order of coordinate output: UL,UR,LR,LL. 
	/// - The Z value returned is the default height of the image.
	//=================================================================================================   
	virtual IMcErrors::ECode  GetFootprints(SMcVector3D sMcVector3D[4]) = 0;

	//=================================================================================================
	// 
	// Function name: int, GroundToImage(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// calculates the image coordinate from a ground coordinate 
	/// \param [in]		vector3dGp	ground coordinate 
	/// \param [out]	vector2dIp	image coordinate 
	/// \param [out]	gToIstatus	status result
	/// \return
	///     - status result
	//=================================================================================================   

	virtual IMcErrors::ECode  GroundToImage(  SMcVector3D vector3dGp,
		SMcVector2D * vector2dIp,
		McResultStatus * gToIstatus) = 0;

	//=================================================================================================
	// 
	// Function name: int, ImageToDefaultHeight(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// calculates the ground coordinate from an image coordinate intersecting LOS with the default height
	/// \param [in]		vector2dIp	image coordinate 
	/// \param [out]	vector3dGp	ground coordinate  
	/// \param [out]	gToIstatus	status result
	/// \return
	///     - status result
	//=================================================================================================   

	virtual IMcErrors::ECode  ImageToDefaultHeight(   SMcVector2D vector2dIp,
		SMcVector3D * vector3dGp,
		McResultStatus * gToIstatus)const= 0;

	//=================================================================================================
	// 
	// Function name: int, ImageToGround(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// calculates the ground coordinate from an image coordinate intersecting LOS with a constant height
	/// \param[in]	groundHeight	the ground height which is used for intersecting with the LOS (line Of Sight).
	/// \param[in]	vector2dIp		image coordinate 
	/// \param[out]	vector3dGp		ground coordinate  
	/// \param[out]	gToIstatus		status result
	/// \return
	///     - status result
	/// \note
	/// Please notice that the LOS is intersected with a constant height and not with a DTM. 
	/// The function which intersect the DTM is the base class function: ImagePixelToCoordWorld(..)
	//=================================================================================================   

	virtual IMcErrors::ECode  ImageToGround( double groundHeight,
		SMcVector2D vector2dIp,
		SMcVector3D * vector3dGp,
		McResultStatus * gToIstatus)const= 0;


	//=================================================================================================
	// 
	// Function name: int, ImageToDtm(...)
	// 
	// Author     : Omer Shelef
	// Date       : 21/1/2007
	//-------------------------------------------------------------------------------------------------
	/// calculates the ground coordinate from an image coordinate intersecting LOS with the DTM
	/// \param[in]	ImagePixel			image coordinate 
	/// \param[in]	dDtmMaxElevation	DTM maximal height
	/// \param[in]	dDtmSpacing			DTM spacing 
	/// \param[in]	dDtmMaxSlope		DTM max slope 
	/// \param[out]	pWorldCoord			ground coordinate  
	/// \param[out]	iToGstatus			status result
	/// \return
	///     - status result
	//=================================================================================================   

	virtual IMcErrors::ECode  ImageToDtm( const	SMcVector2D	&ImagePixel,
		double dDtmMaxElevation, 			// maximal height of the dtm
		double dDtmSpacing, 				// spacing of the dtm 
		double dDtmMaxSlope,
		SMcVector3D *pWorldCoord,
		McResultStatus * iToGstatus)const = 0;



	//=================================================================================================
	// 
	// Function name: int, LosToDefaultHeight(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Intersect default height from an origin and an unit vector in LOS direction 
	/// \param [in]	origin			ground coordinate origin   
	/// \param [in]	orientation		unit vector in LOS direction  
	/// \param [out] vector3dGp		ground coordinate origin in ECF
	/// \return
	///     - status result
	/// \note
	/// - origin is in image's working coordinate system
	/// - orientation is a unit vector in the direction of LOS
	/// - in LSR (Local Space Rectangular) C.S. that is tangent to the Earth in given origin
	//=================================================================================================   

	virtual IMcErrors::ECode  LosToDefaultHeight( SMcVector3D origin,
		SMcVector3D orientation,
		SMcVector3D * vector3dGp )= 0;


	//=================================================================================================
	// 
	// Function name: int, LosToDtm(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Intersect the DTM from an origin and an unit vector in LOS direction
	/// \param [in]	orientation			unit vector in LOS direction  
	/// \param[in]	origin				ground coordinate origin   
	/// \param[in]	dDtmMaxElevation	DTM maximal height
	/// \param[in]	dDtmSpacing			DTM spacing 
	/// \param[in]	dMaxSlope			DTM max slope 
	/// \param[out] vector3dGp			ground coordinate origin in ECF
	/// \param[out] mcStatus			status result
	/// \return
	///     - status result
	/// \note
	/// - origin is in image's working coordinate system
	/// - in LSR (Local Space Rectangular) C.S. that is tangent to the Earth in given origin
	//=================================================================================================   

	virtual IMcErrors::ECode  LosToDtm(  SMcVector3D origin,
		SMcVector3D orientation,
		double dDtmMaxElevation, 			// maximal height of the dtm
		double dDtmSpacing, 				// spacing of the dtm 
		double dMaxSlope,
		SMcVector3D * vector3dGp,
		McResultStatus * mcStatus)= 0;

	//=================================================================================================
	// 
	// Function name: int, CreateXmlData(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Create an Xml Data block for a given image object 
	/// \param [out]	xmlData	xml data block
	/// \return
	///     - status result
	//=================================================================================================   

	virtual IMcErrors::ECode  CreateXmlData( char** xmlData)= 0;


	//=================================================================================================
	// 
	// Function name: int, SaveXmlData(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Save an Xml File of the current image object
	/// \param [in]	outputFileName	xml output file full path 
	/// \return
	///     - status result
	//=================================================================================================   

	virtual IMcErrors::ECode  SaveXmlData( const char* outputFileName)= 0;



	/* Adjustment Functions */
	//=================================================================================================
	// 
	// Function name: int, GetNumberOfAdjustParams(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// returns the max number of parameters which can be used for the adjustment
	/// \param [out]	numParams	number of adjustment parameters 
	/// \return
	///     - status result
	//=================================================================================================   

	virtual IMcErrors::ECode   GetNumberOfAdjustParams( int *numParams )= 0;


	//=================================================================================================
	// 
	// Function name: int, GetAdjustParamsData(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Get the adjustment parameters data currently set by the user
	/// \param [out]	stParamData	the adjustment parameters data block array 
	/// \return
	///     - status result
	/// \note
	/// user must allocate stParamData with size of number of adjustment parameters  obtained by GetNumberOfAdjustParams(...)
	//=================================================================================================   

	virtual IMcErrors::ECode   GetAdjustParamsData( SMcAdjustParameterData* stParamData )= 0;

	//=================================================================================================
	// 
	// Function name: int, SetAdjustParamsData(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Set a new adjustment parameters data block
	/// \param [in]	stParamData		the adjustment parameters data block array 
	/// \return
	///     - status result
	/// \note
	/// user must allocate stParamData with size of number of adjustment parameters  obtained by GetNumberOfAdjustParams(...)   
	//================================================================================================= 

	virtual IMcErrors::ECode   SetAdjustParamsData( SMcAdjustParameterData* stParamData)= 0;


	//=================================================================================================
	// 
	// Function name: int, GetDefaultAdjustParamsData(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Get the default adjustment parameteres data 
	/// \param [in]		nNumOfParameterDataElements		number of adjusted parameters
	/// \param [out]	stParamData						the adjustment parameters data block array 
	/// \return
	///     - status result
	/// \note
	/// user must allocate stParamData with size of number of adjustment parameters  obtained by GetNumberOfAdjustParams(...)      
	//================================================================================================
	virtual IMcErrors::ECode   GetDefaultAdjustParamsData( const int nNumOfParameterDataElements, SMcAdjustParameterData* stParamData)= 0;

	//=================================================================================================
	// 
	// Function name: int, AdjustImage(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Adjust a single image for a given control point array 
	/// \param [in]	numPoints		number of control points
	/// \param [in]	coordSysType	coordinate system type
	/// \param [in, out]	McGpArray		the control points array before and after the adjustment
	/// \param [in, out]	McTermCriteria	adjustment termination criterion data struct before and after the adjustment
	/// \param [out] McStData		statistical results (RMS values)		
	/// \return
	///     - status result
	/// \note
	/// - Every control point (x,y) are equivalent to two equations in the adjustment process
	/// hence you should use at least 4 control points for adjusting the 7 adjusted parameters
	/// - Input and output  data structs and arrays memory (McGpArray, McTermCriteria, McStData)
	///	must be allocated by the user
	//================================================================================================

	virtual IMcErrors::ECode   AdjustImage(	int    numPoints, 
		McCoordSysType coordSysType,   
		McSolvePointForImage* McGpArray, 
		McAdjustTerminationCriteria* McTermCriteria,
		McAdjustStatisticData* McStData)= 0;





	//=================================================================================================
	// 
	// Function name: int, Adjust2Images(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Adjust two images for a given mutual tie point array 
	/// \param [in]	numPoints		number of tie points
	/// \param [in]	coordSysType	coordinate system type
	/// \param [in, out]	McIpArray		the tie points array before and after the adjustment
	/// \param [in, out]	McTermCriteria	adjustment termination criterion data struct before and after the adjustment
	/// \param [out] McStData		statistical results (RMS values)
	/// \param [in]	cOtherImage		pointer for the other image object
	/// \return
	///     - status result
	/// \note
	/// - Every tie point (line, sample) are equivalent to one equation for each image
	///	in the adjustment process hence you should use at least 14 control points for
	///	adjusting 2*7 = 14 parameters
	/// - Input and output data structs and arrays memory (McIpArray, McTermCriteria, McStData)
	///	must be allocated by the user
	//================================================================================================

	virtual IMcErrors::ECode   Adjust2Images(int    numPoints, 
		McCoordSysType coordSysType,   
		McSolvePointFor2Images* McIpArray, 
		McAdjustTerminationCriteria* McTermCriteria,
		McAdjustStatisticData* McStData,
		IMcLoropImageCalc  * cOtherImage
		)= 0;



	//=================================================================================================
	// 
	// Function name: int, AdjustImageWithCovariance(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 13/02/2007
	//-------------------------------------------------------------------------------------------------
	/// Adjust a single image for a given control point array along with covariance matrix calculation
	/// \param [in]	numPoints		number of control points
	/// \param [in]	coordSysType	coordinate system type
	/// \param [in, out]	McGpArray		the control points array before and after the adjustment
	/// \param [in, out]	McTermCriteria	adjustment termination criterion data struct before and after the adjustment
	/// \param [out] McStData		statistical results (RMS values)	
	/// \param [out] mcCovarianceHandle	handle to covariance data object		
	/// \return
	///     - status result
	/// \note
	/// - Every control point (x,y) are equivalent to two equations in the adjustment process
	/// hence you should use at least 4 control points for adjusting the 7 adjusted parameters
	/// - Input and output  data structs and arrays memory (McGpArray, McTermCriteria, McStData)
	///	must be allocated by the user
	//================================================================================================

	virtual IMcErrors::ECode   AdjustImageWithCovariance( int    numPoints, 
		McCoordSysType coordSysType,   
		McSolvePointForImage* McGpArray, 
		McAdjustTerminationCriteria* McTermCriteria,
		McAdjustStatisticData* McStData,
		MCCOVARIANCEHANDLE * mcCovarianceHandle
		)= 0;

	//=================================================================================================
	// 
	// Function name: int, GetCovarianceMatrixSizeForImage(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 13/02/2007
	//-------------------------------------------------------------------------------------------------
	/// This functon Gets the covariance matrix size for the adjusted image
	/// \param[in]	covarianceHandle	handle to covariance data object		
	/// \param[out]	paramsNumber		the number of images parameters that were adjusted
	/// \param[out]	size				the number of rows and columns in covariance matrix
	/// \return
	///     - status result
	/// \note
	/// - This function provides the size of covariance matrix for parameters of the image and also the number 
	/// of the parameters that were adjusted.
	/// - User need these numbers to allocate the covariance matrix
	/// and the arrays of McCovarianceParameterData in order to pass to the function GetCovarianceMatrixForImage
	/// size equals to the sum of (correction levels + 1) of the sensor's adjusted parameters
	//================================================================================================
	virtual IMcErrors::ECode   GetCovarianceMatrixSizeForImage(  	MCCOVARIANCEHANDLE covarianceHandle, 
		int* paramsNumber,
		int* size
		)= 0;

	//=================================================================================================
	// 
	// Function name: int, GetCovarianceMatrixForImage(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 13/02/2007
	//-------------------------------------------------------------------------------------------------
	/// This function Gets the covariance matrix for the adjusted image
	/// \param [in]	covarianceHandle	handle to covariance data object retrieved from AdjustImageWithCovariance()
	/// \param [in]	paramsNumber		the number of image parameters that were adjusted
	/// \param [out] params				the output array for the image parameters. 
	///									the array must be allocated by the user and will be filled by the function
	/// \param [in]	size				the number of rows and columns in covariance matrix
	/// \param [out] matrix				the output covariance matrix
	/// \return
	///     - status result
	/// \note
	/// This function gets the covariance matrix of the adjusted parameters of the image 
	/// and the parameters. 
	//================================================================================================

	virtual IMcErrors::ECode   GetCovarianceMatrixForImage(	MCCOVARIANCEHANDLE covarianceHandle, 
		int paramsNumber,
		McCovarianceParameterData* params, 
		int size,
		double** matrix 
		)=0;


	//=================================================================================================
	// 
	// Function name: int, Adjust2ImagesWithCovariance(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Adjust two images for a given mutual tie point array along with covariance matrix calculation
	/// \param[in]	numPoints			number of tie points
	/// \param[in]	coordSysType		coordinate system type
	/// \param[in, out]	McIpArray		the tie points array before and after the adjustment
	/// \param[in, out]	McTermCriteria	adjustment termination criterion data struct before and after the adjustment
	/// \param[out] McStData			statistical results (RMS values)
	/// \param[in]	cOtherImage			pointer for the other image object
	/// \param[out] covarianceHandle	handle to covariance data object
	/// \return
	///     - status result
	/// \note
	/// - Every tie point (line, sample) are equivalent to one equation for each image
	///	in the adjustment process hence you should use at least 14 control points for
	///	adjusting 2*7 = 14 parameters
	/// - Input and output data structs and arrays memory (McIpArray, McTermCriteria, McStData)
	///	must be allocated by the user
	//================================================================================================
	virtual IMcErrors::ECode   Adjust2ImagesWithCovariance(int    numPoints, 
		McCoordSysType coordSysType,   
		McSolvePointFor2Images* McIpArray, 
		McAdjustTerminationCriteria* McTermCriteria,
		McAdjustStatisticData* McStData,
		IMcLoropImageCalc  * cOtherImage,
		MCCOVARIANCEHANDLE * covarianceHandle
		)= 0;


	//=================================================================================================
	// 
	// Function name: int, GetCovarianceMatrixSizeFor2Images(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 13/02/2007
	//-------------------------------------------------------------------------------------------------
	/// This functon Gets the covariance matrix size for a pair of images
	/// \param[in]		covarianceHandle	handle to covariance data object		
	/// \param [in]		cOtherImage			pointer for the other image object
	/// \param [out]	paramsNumber1		the number of the current image parameters that were adjusted
	/// \param [out]	size1				the number of rows in covariance matrix
	/// \param [out]	paramsNumber2		the number of the other image parameters that were adjusted
	/// \param [out]	size2				the number of columns in covariance matrix
	/// \return
	///     - status result
	/// \note
	/// - This function provides the sizes of the covariance matrix for a pair of images along with the number 
	/// of the parameters that were adjusted fro each image.
	/// - The user need these numbers to allocate the covariance matrix
	/// and the arrays of McCovarianceParameterData in order to pass to the function GetCovarianceMatrixFor2Images()
	/// size equals to the sum of (correction levels + 1) of the sensor's adjusted parameters
	//================================================================================================


	virtual IMcErrors::ECode  GetCovarianceMatrixSizeFor2Images( 	MCCOVARIANCEHANDLE covarianceHandle, 
		IMcLoropImageCalc  * cOtherImage,
		int* paramsNumber1,
		int* size1,
		int* paramsNumber2,
		int* size2 
		)=0;


	//=================================================================================================
	// 
	// Function name: int, GetCovarianceMatrixFor2Images(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 13/02/2007
	//-------------------------------------------------------------------------------------------------
	/// This function Gets the covariance matrix for the adjusted pair of images
	/// \param [in]	covarianceHandle	handle to covariance data object retrieved from Adjust2ImagesWithCovariance()
	/// \param [in] cOtherImage			pointer to the other image object
	/// \param [in]	paramsNumber1		the number of the current image parameters that were adjusted
	/// \param [out] params1			the output array for the parameters. 
	///									the array must be allocated by the user and will be filled by the function
	/// \param [in]	size1				number of rows in the covariance matrix
	/// \param [in]	paramsNumber2		the number of other image parameters that were adjusted
	/// \param [out] params2			the output array for the parameter. 
	///									the array must be allocated by the user and will be filled by the function
	/// \param [in]	size2				number of columns in the covariance matrix
	/// \param [out] matrix				the output covariance matrix
	/// \return
	///     - status result
	/// \note
	/// This function gets the covariance matrix of the adjusted parameters of the pair of images 
	/// and there parameters. 
	//================================================================================================	


	virtual IMcErrors::ECode  GetCovarianceMatrixFor2Images(	MCCOVARIANCEHANDLE covarianceHandle,
		IMcLoropImageCalc  * cOtherImage,
		int paramsNumber1,
		McCovarianceParameterData* params1, 
		int size1,
		int paramsNumber2,
		McCovarianceParameterData* params2,
		int size2,
		double** matrix 
		)=0;

	//=================================================================================================
	// 
	// Function name: int, GetCovarianceParamsNumber(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 13/02/2007
	//-------------------------------------------------------------------------------------------------
	/// This function gets the number of image adjusted parameters (covariance parameters)
	/// \param [in]		covarianceHandle	handle to covariance data object retrieved from Adjust2ImagesWithCovariance()
	/// \param [out]	paramsNumber		number of parameters i.e number of covarianceParameterData structure to 
	///										allocate before calling getCovarianceParams() function
	/// \return
	///     - status result
	//================================================================================================

	virtual IMcErrors::ECode  GetCovarianceParamsNumber(MCCOVARIANCEHANDLE covarianceHandle, 
		int* paramsNumber
		)=0;

	//=================================================================================================
	// 
	// Function name: int, GetCovarianceParams(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 13/02/2007
	//-------------------------------------------------------------------------------------------------
	/// This function gets the parameters that were adjusted for the image
	/// \param [in]	covarianceHandle	handle to covariance data object retrieved from Adjust2ImagesWithCovariance()
	/// \param [in]	paramsNumber		number of parameters i.e number of covarianceParameterData structure user 
	///									allocated before calling getCovarianceParams() function
	///	\param [out] params				the array of covaoance parameters user allocated which will be filled by the function 	
	/// \return
	///     - status result
	//================================================================================================

	virtual IMcErrors::ECode  GetCovarianceParams(
		MCCOVARIANCEHANDLE covarianceHandle, 
		int paramsNumber,
		McCovarianceParameterData* params
		)=0; 

	//=================================================================================================
	// 
	// Function name: int, GetCovarianceValueForImage(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 13/02/2007
	//-------------------------------------------------------------------------------------------------
	/// This function gets a single adjusted parameters covariance value for the adjusted image
	/// \param [in]	covarianceHandle	handle to covariance data object retrieved from Adjust2ImagesWithCovariance()
	/// \param [in]	paramName1			covariance parameter name in matrix row direction
	///	\param [in] paramLevel1			covariance parameter level
	/// \param [in]	paramName2			covariance parameter name in matrix column direction
	///	\param [in] paramLevel2			covariance parameter level
	///	\param [out] value				covariance parameter value
	/// \return
	///     - status result
	/// \note
	/// Level of parameters:
	/// - 0 - bias
	/// - 1 - linear
	/// - 2 - quadratic , etc.
	//================================================================================================

	virtual IMcErrors::ECode  GetCovarianceValueForImage( MCCOVARIANCEHANDLE covarianceHandle, 
		const char* paramName1,
		int paramLevel1,
		const char* paramName2,
		int paramLevel2,
		double* value 
		)=0;

	//=================================================================================================
	// 
	// Function name: int, GetCovarianceValueFor2Images(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 13/02/2007
	//-------------------------------------------------------------------------------------------------
	/// This function gets a single adjusted parameters covariance value for the adjusted pair of images
	/// \param [in]	covarianceHandle	handle to covariance data object retrieved from Adjust2ImagesWithCovariance()
	/// \param [in]	cOtherImage			a pointer to the object of the other adjusted image 
	/// \param [in]	paramName1			covariance parameter name from current image
	///	\param [in] paramLevel1			covariance parameter level 
	/// \param [in]	paramName2			covariance parameter name from other image
	///	\param [in] paramLevel2			covariance parameter level 
	///	\param [out] value				mutual images covariance parameter value
	/// \return
	///     - status result
	/// \note
	/// Level of parameters:
	/// - 0 - bias
	/// - 1 - linear
	/// - 2 - quadratic , etc.
	//================================================================================================

	virtual IMcErrors::ECode  GetCovarianceValueFor2Images(MCCOVARIANCEHANDLE covarianceHandle, 
		IMcLoropImageCalc  * cOtherImage,
		const char* paramName1,
		int paramLevel1,
		const char* paramName2,
		int paramLevel2,
		double* value 
		)=0;


// 	//=================================================================================================
// 	// 
// 	// Function name: int, GetCovarianceValue(...)
// 	// 
// 	// Author     : Meir Gabbay
// 	// Date       : 13/02/2007
// 	//-------------------------------------------------------------------------------------------------
// 	/// This function gets a single adjusted parameter covariance value 
// 	/// \param [in]	covarianceHandle	handle to covariance data object retrieved from Adjust2ImagesWithCovariance()
// 	/// \param [in]	image1			a pointer to the object of the adjusted image1 
// 	/// \param [in]	paramName1		covariance parameter name from current image
// 	///	\param [in] paramLevel		covariance parameter level 
// 	/// \param [in]	image2			a pointer to the object of the adjusted image2 
// 	/// \param [in]	paramName2		covariance parameter name from other image
// 	///	\param [in] paramLeve2		covariance parameter level 
// 	///	\param [out] value			mutual images covariance parameter value
// 	/// \return
// 	///     - status result
// 	/// \note
// 	/// To get the a covariance value of a single image adjustment covariance matrix 
// 	/// input the same pointer to image object in image1 and image2, 
// 	/// choose the covariance value by choosing the right combination of parameter in row versus parameter in column. 
// 	/// Level of parameters:
// 	/// - 0 - bias
// 	/// - 1 - linear
// 	/// - 2 - quadratic , etc.
// 	//================================================================================================
// 	virtual IMcErrors::ECode  GetCovarianceValue(MCCOVARIANCEHANDLE covarianceHandle, 
// 	 									  IMcLoropImageCalc  * Image1,
// 	 									  const char* paramName1,
// 	 									  int paramLevel1,
// 	 									  IMcLoropImageCalc  * Image2,
// 	 									  const char* paramName2,
// 	 									  int paramLevel2,
// 	 									  double* value 
// 	 									  )=0;

	//=================================================================================================
	// 
	// Function name: int, UnloadCovariance(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 13/02/2007
	//-------------------------------------------------------------------------------------------------
	/// This function unloads the covariance data object from memory
	/// \param [in]	covarianceHandle	handle to covariance data object retrieved from Adjust2ImagesWithCovariance()
	/// \return
	///     - status result
	//================================================================================================
	virtual IMcErrors::ECode  UnloadCovariance(MCCOVARIANCEHANDLE* covarianceHandle) = 0;

	//=================================================================================================
	// 
	// Function name: int, GetFooter(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the Footer data of a LOROP image 
	/// \param [out]	footer	the	Footer data of the current image 
	/// \return
	///     - status result
	//=================================================================================================  
	virtual IMcErrors::ECode  GetFooter( McloropProcessedImageFooterAnn * footer ) = 0;

	//=================================================================================================
	// 
	// Function name: int, GetCameraPose(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the camera pose of a certain line in the lorop image 
	/// \param [in]		line		line number in the image
	/// \param [out]	position	sensor position while aquiring the line in image's working coordinate system
	/// \param [out]	attitudes	attitude.x - roll, attitude.y - elevation  attitude.z - azimuth
	/// \return
	///     - status result
	/// \note
	/// attitudes angls ae in deg.
	//=================================================================================================  
	virtual IMcErrors::ECode  GetCameraPose(	int line, 
		SMcVector3D* position, 
		SMcVector3D* attitudes)= 0;



	//=================================================================================================
	// 
	// Function name: int, GetSpectrum(...)
	// 
	// Author     : Meir Gabbay
	// Date       : 12/11/2006
	//-------------------------------------------------------------------------------------------------
	/// Gets the camera spectrum type 
	/// \param [out]	spectrum	spectrum can be: Visible or IR
	/// \return
	///     - status result
	//=================================================================================================  

	virtual IMcErrors::ECode  GetSpectrum( McSpectrumType* spectrum) = 0;


}; // end defenition of class IMcLoropImageCalc

#endif //_WIN32_WCE
