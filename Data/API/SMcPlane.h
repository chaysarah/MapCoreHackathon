//===========================================================================
/// \file SMcPlane.h
/// SMcPlane structure
//===========================================================================

#pragma once

#include "SMcVector.h"

//===========================================================================
// SMcPlane struct
//---------------------------------------------------------------------------
///	3D plane definition.
/// 
//===========================================================================
struct SMcPlane
{
	SMcVector3D	Normal;		///< The plane's normal
	double		dLocation;	///< The signed distance from the axes origin to the plane along the normal

	/// \name Construction & Assignment
	//@{
	SMcPlane() {}
	SMcPlane(SMcVector3D Normal, double dLocation) 
	{
		this->Normal = Normal;
		this->dLocation = dLocation;
	}
	SMcPlane(const SMcPlane &Other) 
	{
		*this = Other;
	}
	SMcPlane(const SMcVector3D &Normal, const SMcVector3D &Point)
	{
		this->Normal = Normal;
		this->dLocation = Normal * Point;
	}
	SMcPlane(SMcVector3D Point1, SMcVector3D Point2, SMcVector3D Point3) 
	{
		SMcVector3D Edge1 = Point2 - Point1;
		SMcVector3D Edge2 = Point3 - Point1;
		Normal = (Edge1 ^ Edge2).GetNormalized();
		dLocation = Normal * Point1;
	}

	SMcPlane& operator=(const SMcPlane &Other)
	{
		Normal = Other.Normal;
		dLocation = Other.dLocation;
		return *this;
	}
	//@}
};
