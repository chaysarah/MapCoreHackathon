#pragma once

//===========================================================================
/// \file SMcScanGeometry.h
/// Structures for geometries used in scan operations
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "SMcVector.h"
#include "OverlayManager/IMcObjectSchemeNode.h"

//===========================================================================
// Struct Name: SMcScanGeometry
//---------------------------------------------------------------------------
/// Base struct for geometries used in scan operations
//===========================================================================
struct SMcScanGeometry
{
	/// geometry coordinate system: world, screen or image
	EMcPointCoordSystem	eCoordinateSystem;

	/// Geometry type unique ID
	UINT									uGeometryType;

protected:
	SMcScanGeometry(EMcPointCoordSystem _eCoordinateSystem, UINT _uGeometryType)
	{
		eCoordinateSystem = _eCoordinateSystem;
		uGeometryType = _uGeometryType;
	}
};	

//===========================================================================
// Struct Name: SMcScanPointGeometry
//---------------------------------------------------------------------------
/// Struct for point geometry used in scan operations
//===========================================================================
struct SMcScanPointGeometry : public SMcScanGeometry
{
	SMcVector3D	Point;					///< point to scan at
	float		fPointAndLineTolerance;	///< tolerance for points and lines scanned

    enum
    {
        //================================================================
        /// Geometry unique ID for this struct
        //================================================================
        GEOMETRY_TYPE = 1
    };

/// \name Constructor
//@{
	//==============================================================================
	// Method Name: SMcScanPointGeometry()
	//------------------------------------------------------------------------------
	/// Constructs scan point geometry struct
	///
	/// \param[in]	_eCoordinateSystem			geometry coordinate system: world, screen or image
	/// \param[in]	_Point						point to scan at
	/// \param[in]	_fPointAndLineTolerance		tolerance for points and lines scanned
	//==============================================================================
	SMcScanPointGeometry(EMcPointCoordSystem _eCoordinateSystem,
				 SMcVector3D _Point, float _fPointAndLineTolerance)
		: SMcScanGeometry(_eCoordinateSystem, GEOMETRY_TYPE)
	{
		Point = _Point; fPointAndLineTolerance = _fPointAndLineTolerance;
	}
//@}
};

//===========================================================================
// Struct Name: SMcScanBoxGeometry
//---------------------------------------------------------------------------
/// Struct for box/rectangle geometry used in scan operations
//===========================================================================
struct SMcScanBoxGeometry : public SMcScanGeometry
{
	SMcBox	Box;				///< box to scan in (for screen and image coordinate systems
								///< XY rectangle is used (\a z is ignored)

    enum
    {
        //================================================================
        /// Geometry unique ID for this struct
        //================================================================
        GEOMETRY_TYPE = 2
    };

/// \name Constructor
//@{
	//==============================================================================
	// Method Name: SMcScanBoxGeometry()
	//------------------------------------------------------------------------------
	/// Constructs box/rectangle point geometry struct
	///
	/// \param[in]	_eCoordinateSystem		geometry coordinate system: world, screen or image
	/// \param[in]	_Box					box to scan in (for screen and image coordinate systems
	///										XY rectangle is used (\a z is ignored)
	//==============================================================================
	SMcScanBoxGeometry(EMcPointCoordSystem _eCoordinateSystem, const SMcBox &_Box)
		: SMcScanGeometry(_eCoordinateSystem, GEOMETRY_TYPE)
	{
		Box = _Box;
	}
//@}
};

//===========================================================================
// Struct Name: SMcScanPolygonGeometry
//---------------------------------------------------------------------------
/// Struct for polygon geometry used in scan operations
//===========================================================================
struct SMcScanPolygonGeometry : public SMcScanGeometry
{
	SMcVector3D	*pPolygonVertices;		///< vertices of polygon to scan in (\a z is ignored)
	UINT		uNumVertices;			///< number of polygon vertices

    enum
    {
        //================================================================
        /// Geometry unique ID for this struct
        //================================================================
        GEOMETRY_TYPE = 3
    };

/// \name Constructor
//@{
	//==============================================================================
	// Method Name: SMcScanPolygonGeometry()
	//------------------------------------------------------------------------------
	/// Constructs box/rectangle point geometry struct
	///
	/// \param[in]	_eCoordinateSystem	geometry coordinate system: world, screen or image
	/// \param[in]	_aPolygonVertices[]	vertices of polygon to scan in (\a z is ignored)
	/// \param[in]	_uNumVertices		number of polygon vertices
	//==============================================================================
	SMcScanPolygonGeometry(EMcPointCoordSystem _eCoordinateSystem, 
		SMcVector3D _aPolygonVertices[], UINT _uNumVertices)
		: SMcScanGeometry(_eCoordinateSystem, GEOMETRY_TYPE)
	{
		pPolygonVertices = _aPolygonVertices;
		uNumVertices = _uNumVertices;
	}
//@}
};
