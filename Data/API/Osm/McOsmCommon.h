//===========================================================================
/// \file McOsmCommon.h
/// McOsmCommon.h : include file for common objects used by the MCOSM API,
/// or project specific include files that are used frequently, but
/// are changed infrequently
//===========================================================================

#pragma once

#include "McExports.h"
#include <string>
#include "CMcDataArray.h"

//////////////////////////////////////////////////////////////////////////

struct SMcOsmStateInfo
{
	/// object identifier
	__int64 nOsmId;

	/// object name
	wchar_t strName[64];
};

/// City information (returned)
struct SMcOsmCityInfo 
{
	/// object Identifier
	__int64 nOsmId;

	/// object name
	wchar_t strName[128];

	/// object type (city, town or village).
	wchar_t strType[64];

	// state (empty if not exists)
	wchar_t strState[64];
};

//////////////////////////////////////////////////////////////////////////
/// Street information (returned)
struct SMcOsmStreetInfo
{
	/// object Identifier
	__int64 nOsmId;

	// City name
	wchar_t strCityName[128];

	/// Street name
	wchar_t strStreetName[128];

	/// State (empty if not exists)
	wchar_t strStateName[64];
};


//////////////////////////////////////////////////////////////////////////
/// A place information (returned)
struct SMcOsmPlaceInfo
{
	/// object Identifier
	__int64 nOsmId;

	/// object name
	wchar_t strName[128];

	/// object type
	wchar_t strType[64];

	/// OSM type
	char	charOsmType;

	/// Street name - empty if not specified
	wchar_t strStreetName[128];

	/// House number - empty if not specified
	wchar_t	strHouseNum[12];

	/// City
	wchar_t strCity[64];

	/// State
	wchar_t strState[64];

	/// Country
	wchar_t strCountry[64];

	/// Geometry - for HTML interface only
	CMcDataArray<WCHAR>		strGeometry;
};


//////////////////////////////////////////////////////////////////////////////
/// Full Address Result
struct SMcOsmAddressSearchResult
{
	/// placeId
	__int64 nOsmId;
	// The country the place is a part of
	CMcDataArray<WCHAR> strCountry;

	// The states the place is a part of
	CMcDataArray<WCHAR> strState;

	/// The city the place is a part of
	CMcDataArray<WCHAR> strCity;

	/// The suburb
	CMcDataArray<WCHAR> strSuburb;

	/// The neighborhood
	CMcDataArray<WCHAR> strNeighbourhood;

	/// The street of the place
	CMcDataArray<WCHAR>	strStreet;

	/// The house number
	CMcDataArray<WCHAR> strHouseNumber;

	/// The place name
	CMcDataArray<WCHAR> strPlaceName;

	/// The place type
	CMcDataArray<WCHAR> strType;

	/// the place postcode
	CMcDataArray<WCHAR> strPostcode;

	/// Geometry - for HTML interface only
	CMcDataArray<WCHAR> strGeometry;

	/// Search rank
	int					nRank;

	/// Grade - how relevant is the result in respect to the query (0 to 100)
	int					nGrade;

	/// OSM Type
	char				charOsmType;
};

////////////////////////////////////////////////////////////////////////////
/// A single routing direction information
struct SMcOsmRoutingDirection {
	/// Direction sequence number (1 based.)
	int		nSequenceNo;

	/// The edge node number
	int		nNodeId;

	/// The object name
	CMcDataArray<WCHAR>	strName;

	/// The direction relative to the navigating person/vehicle
	double	nHeading;

	/// The step cost
	double  nCost;

	/// Coded geometry string. Use the Geocoder class to gather coordinates from it
	CMcDataArray<CHAR>	strGeom;
};
