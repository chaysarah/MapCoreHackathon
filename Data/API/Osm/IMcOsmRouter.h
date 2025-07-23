//===========================================================================
/// \file IMcOsmRouter.h
/// Routing functionality module.
///===========================================================================

#pragma once

#include "SMcVector.h"
#include "IMcOsmConnection.h"
#include "IMcDestroyable.h"

//////////////////////////////////////////////////////////////////////////
/// The following class provides the Routing functionality.
/// Basically the class exposes a single method RouteFromAtoB which
/// provides routing directions from Point A to Point B.

class IMcOsmRouter : public IMcDestroyable
{
public:
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates a new interface instance
	///
	/// \param[out]	iMcOsmRouter		The interface being created
	/// \param[in]	pConn				Connectivity object holds an opened database connection
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode MC_OSM_PLUGIN_API Create(IMcOsmRouter **iMcOsmRouter, IMcOsmConnection *pConn);			// An opened database connection object

	//==============================================================================
	// Method Name: RouteFromAtoB()
	//------------------------------------------------------------------------------
	/// Provides routing instructions from point A to point B.
	///
	/// \param[in]	PointA					Routing start point
	/// \param[in]	PointB					Routing end point
	/// \param[out]	paRoutingDirections		A returned array of routing directions
	/// \param[in]	bConsiderOneWay			Should consider one-way traffic
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RouteFromAtoB(
			SMcVector3D		PointA,
			SMcVector3D		PointB,
			CMcDataArray<SMcOsmRoutingDirection>	*paRoutingDirections,
			bool			bConsiderOneWay = true) = 0;
};

