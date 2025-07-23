//===========================================================================
/// \file IMcOsmGeocoder.h
/// GeoCoding functionality module.
///===========================================================================

#pragma once

#include "SMcVector.h"
#include "IMcOsmConnection.h"
#include "IMcDestroyable.h"

//////////////////////////////////////////////////////////////////////////
/// The following class provides the GeoCoding functionality.
/// Each of its methods returns a success failure flag.
/// In case of failure, a global LastError char string contains the error occured.
/// the caller may use an external reference to the LastError character string.
class IMcOsmGeocoder : public IMcDestroyable
{
public:
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates a new interface instance
	///
	/// \param[out]	iMcOsmGeocoder		The interface being created
	/// \param[in]	pConn				Connectivity object holds an opened database connection
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode MC_OSM_PLUGIN_API Create(IMcOsmGeocoder **iMcOsmGeocoder, IMcOsmConnection *pConn);			// An opened database connection object

	//==============================================================================
	// Method Name: EnumStates()
	//------------------------------------------------------------------------------
	/// Returns an array of all states
	///
	/// \param[out]	paStates		Returned state information array
	/// \param[in]	strLanguage		Search language (two letter code) - empty = newtral
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode EnumStates(
		CMcDataArray<SMcOsmStateInfo> *paStates,
		PCSTR strLanguage = NULL) = 0;

	//==============================================================================
	// Method Name: EnumCities()
	//------------------------------------------------------------------------------
	/// Returns an array of all cities
	///
	/// \param[out]	paCities		Returned city information array
	/// \param[in]	strState		State to use as filter, NULL means do not filter
	/// \param[in]	strLanguage		Search language (two letter code) - empty = neutral
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode EnumCities(
		CMcDataArray<SMcOsmCityInfo> *paCities,	
		PCWSTR strState = NULL,
		PCSTR strLanguage = NULL) = 0;					 

	//==============================================================================
	// Method Name: EnumStreets()
	//------------------------------------------------------------------------------
	/// Returns an array all streets within a city
	///
	/// \param[in]	strCityName		City name to enum it's streets
	/// \param[out]	paStreets		Returned street information
	/// \param[in]	strState		State to use as filter, NULL means do not filter
	/// \param[in]	strLanguage		Search language (two letter code) - empty = neutral
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode EnumStreets(
		LPCWSTR strCityName,						
		CMcDataArray<SMcOsmStreetInfo> *paStreets,	 
		PCWSTR strState = NULL,
		PCSTR strLanguage = NULL) = 0;					 

	//==============================================================================
	// Method Name: EnumPlaceTypes()
	//------------------------------------------------------------------------------
	/// Returns an array of place type names. 
	///
	/// \param[out]	pastrTypes An array of Place type name (layers)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode EnumPlaceTypes(
		CMcDataArray<PCSTR> *pastrTypes) = 0;

	//==============================================================================
	// Method Name: GetGeometries()
	//------------------------------------------------------------------------------
	/// Returns array of geometry strings each for it's corresponding osm_id. This method
	/// will not work in the usage of HttpConnectionObject
	///
	/// \param[in]	anOsmIds			Input vector of OSM identifiers (Returned by query)
	/// \param[in]	uNumOsmIds			Number of entries 
	/// \param[out]	pastrCoords			Returned String Array each of which contains geometry data that can be resolved by GetGeometryCood
	/// \param[out]	panReturnedOsmIds	The corresponding OSM ID array of each of the returned pCoords
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetGeometries(
		const __int64 anOsmIds[],						
		UINT uNumOsmIds,							
		CMcDataArray<PCSTR> *pastrCoords,				
		CMcDataArray<__int64> *panReturnedOsmIds) = 0;


	//==============================================================================
	// Method Name: GetGeometries()
	//------------------------------------------------------------------------------
	/// Returns an array of geometry strings each for it's corresponding osm_id. This method will work in
	/// the case of HttpConnectionObject usage only.
	///
	/// \param[in]	astrOsmTypes	a null terminated character array, each of its members 
	///								is an OSM type character - R (relation), N (node) or W (way)
	/// \param[in]	anOsmIds		Input vector of OSM identifiers (Returned by query)
	/// \param[out]	pastrCoords		Returned String Array each of which contains geometry data that can be resolved by GetGeometryCood
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetGeometries(
		PCSTR			astrOsmTypes,
		const __int64	anOsmIds[],
		CMcDataArray<PCSTR> *pastrCoords) = 0;

	//==============================================================================
	// Method Name: GetGeometryCoord()
	//------------------------------------------------------------------------------
	/// Converts a geometry string to an array of coordinates, bConvertToMapCore converts the coordinates to internal MapCore values
	/// The method returns a character array contains the geometry type (MULTIPOLYGON, POLYGON, LINESTRING, POINT)
	///
	/// \param[in]	strGeometry		Input geometry (Returned from GetGeometries)
	/// \param[out]	paCoords		Returned coordinates array
	/// \param[out]	strCoordType	buffer holds the coordinate type buffer. 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetGeometryCoord(
		PCSTR strGeometry,							
		CMcDataArray<SMcVector3D> *paCoords,			
		char					  strCoordType[12]) = 0;		

	//==============================================================================
	// Method Name: QueryForAddress()
	//------------------------------------------------------------------------------
	/// Query for a complete address that matches the city name, street name and house number. At least city or street has to contain value
	///
	/// \param[in]	strCityName		City name, if it is an empty string a street name is required
	/// \param[in]	strStreetName	Street within the city. and empty value will require a city and will return all streets within the city
	/// \param[in]	strHouseNumber	House number, if StreetName is empty it will be ignored. Will look to the closest house number within the street
	/// \param[out]	paPlaceInfo		Place information returned
	/// \param[in]  strState		state filter - NULL if not required
	/// \param[in]	strLanguage		Default language code (two letters lowercase) - an empty means natural.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode QueryForAddress(
		PCWSTR strCityName, 
		PCWSTR strStreetName, 
		PCWSTR strHouseNumber, 
		CMcDataArray<SMcOsmPlaceInfo> *paPlaceInfo, 
		PCWSTR strState = NULL,
		PCSTR strLanguage = NULL) = 0;					

	//==============================================================================
	// Method Name: QueryForPlace()
	//------------------------------------------------------------------------------
	/// Query for complete place information for items with layer types.
	///
	/// \param[in]	strCityName		City that contains the layer. If empty - all cities will be searched
	/// \param[in]	strLayerTypes	An array of PSTR containing the layer types
	/// \param[in]	uNumLayerTypes	The number of layer types to scan
	/// \param[out]	paPlaceInfo		Place information returned
	/// \param[in]  strState		state filter - NULL if not required
	/// \param[in]	strLanguage		Two letter language code (empty - neutral).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode QueryForPlace(
		PWSTR strCityName, 
		const PCSTR strLayerTypes[], 
		UINT uNumLayerTypes, 
		CMcDataArray<SMcOsmPlaceInfo> *paPlaceInfo, 
		PCWSTR strState = NULL,
		PCSTR strLanguage = NULL) = 0;		
		
	//==============================================================================
	// Method Name: SearchForAddress()
	//------------------------------------------------------------------------------
	/// Query for a complete address in free text search
	///
	/// \param[in]	strAddressToSearch	Free text to search at
	/// \param[out]	paResults			Query results array - prioritized
	/// \param[in]	strLanguage			Language used in two letter code (empty - neutral)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SearchForAddress(
		PWSTR strAddressToSearch,
		CMcDataArray<SMcOsmAddressSearchResult> *paResults,
		PCSTR strLanguage = NULL) = 0;
};
