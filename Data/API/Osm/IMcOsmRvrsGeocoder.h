//===========================================================================
/// \file IMcOsmRvrsGeocoder.h
/// Reverse GeoCoding functionality module.
///===========================================================================

#pragma once

#include "SMcVector.h"
#include "IMcOsmConnection.h"
#include "IMcDestroyable.h"

//////////////////////////////////////////////////////////////////////////
/// The following class provides the reverse GeoCoding functionality.
/// Each of its methods returns a success failure flag.
/// In case of failure, a global LastError char string contains the error occurs.
/// the caller may use an external reference to the LastError character string.

class IMcOsmRvrsGeocoder : public IMcDestroyable
{
public:
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates a new interface instance
	///
	/// \param[out]	ppMcOsmRvrsGeocoder		The interface being created
	/// \param[in]	pConn					Connectivity object holds an opened database connection
	///
	/// \return
	///     - status result
	//==============================================================================
	static MC_OSM_PLUGIN_API IMcErrors::ECode Create(IMcOsmRvrsGeocoder **ppMcOsmRvrsGeocoder, IMcOsmConnection *pConn);

	//==============================================================================
	// Method Name: GetTolerances()
	//------------------------------------------------------------------------------
	/// Returns the current tolerances used by ScanPosition(). 
	///
	/// Tolerance values are the distance (in meters) that enables ScanPosition
	/// to offset from the exact point specified as its parameter.
	/// For example: If a user clicks a house and the mouse position is few meters away from
	/// the exact place, it is steel considered to be the house.
	///
	/// \param[out]	pnPlaceTolerance	Tolerance between the point scanned to all places except for street
	/// \param[out]	pnStreetTolerance	Tolerance between the point scanned to a street
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTolerances(int *pnPlaceTolerance, int *pnStreetTolerance) = 0;

	//==============================================================================
	// Method Name: SetTolerances()
	//------------------------------------------------------------------------------
	/// Set the default Tolerance values used by the ScanPosition().
	///
	/// See GetTollerances for an explanation about tolerances.
	///
	/// \param[out]	nPlaceTolerance		Tolerance between the point scanned to all places except for street
	/// \param[out]	nStreetTolerance	Tolerance between the point scanned to a street
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTolerances(int nPlaceTolerance = 20, int nStreetTolerance = 40) = 0;

	//==============================================================================
	// Method Name: ScanPoint()
	//------------------------------------------------------------------------------
	/// Scans starting from the specified position, returns a priorities list of places.
	///
	/// By default it will return all the houses from the nearest up to the tolerance
	/// then all other places from the nearest to the most distance.
	///
	/// \param[in]	position				The position to scan from
	/// \param[out]	paPlaces				A list of places returned. this list is ordered by priority and distance
	/// \param[out] panDistances			A list of distances returned. each entry within this list is corresponding to paPlaces entry.
	/// \param[in]  strLanguage				Two letter code specifing the language to use (NULL=default)
	/// \param[in]	astrPrioritizedLayers	An optional array of layers to prioritize acoordingly. - Not implemented yet - must provide NULL.
	/// \param[in]  uNumPriorituzedLayers	The number of prioritized layers
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ScanPoint(	SMcVector3D position, 
											CMcDataArray<SMcOsmPlaceInfo> *paPlaces,
											CMcDataArray<double> *panDistances,
											PCSTR strLanguage = NULL, 
											const PCSTR astrPrioritizedLayers[] = NULL,
											UINT uNumPriorituzedLayers = 0) = 0;
};