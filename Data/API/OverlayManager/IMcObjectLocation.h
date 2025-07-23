#pragma once
//==================================================================================
/// \file IMcObjectLocation.h
/// Interface for object scheme node of type object location
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "McCommonTypes.h"
#include "OverlayManager/IMcObjectSchemeNode.h"

//==================================================================================
// Interface Name: IMcObjectLocation
//----------------------------------------------------------------------------------
/// Interface for object scheme node of type object location.
///
/// The actual points are set via the IMcObject specifying the location index.
//==================================================================================
class IMcObjectLocation : public virtual IMcObjectSchemeNode
{
protected:

	virtual ~IMcObjectLocation() {}

public:

	enum
	{
		//==============================================================================
		/// Node unique ID for this interface
		//==============================================================================
		NODE_TYPE = 50
	};

	/// \name Index and Coordinate System
	//@{

	//==============================================================================
	// Method Name: GetIndex(...)
	//------------------------------------------------------------------------------
	/// Retrieves the location's index in its object scheme set by IMcObjectScheme::Create() (in this case always 0) or IMcObjectScheme::AddObjectLocation().
	///
	/// \param[out]	puIndex		The location's index
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetIndex(UINT *puIndex) const = 0;

	//==============================================================================
	// Method Name: GetCoordSystem(...)
	//------------------------------------------------------------------------------
	/// Retrieves the coordinate system of the location's points set by IMcObjectScheme::Create() or IMcObjectScheme::AddObjectLocation().
	///
	/// \param[out]	peCoordSystem	The coordinate system of the location's points.
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCoordSystem(
		EMcPointCoordSystem *peCoordSystem) const = 0;

	//@}

	/// \name Relative to DTM
	//@{

	//==============================================================================
	// Method Name: SetRelativeToDTM()
	//------------------------------------------------------------------------------
	/// Sets whether or not heights of the location's points are relative to DTM, as a shared or private property.
	///
	/// \param[in]	bRelativeToDTM		Whether or not heights of the location's points are relative to DTM.
	///									The parameter's meaning depends on \a uPropertyID (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID.
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetRelativeToDTM(bool bRelativeToDTM,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID) = 0;

	//==============================================================================
	// Method Name: GetRelativeToDTM(...)
	//------------------------------------------------------------------------------
	/// Retrieves the location's relative-to-DTM property set by SetRelativeToDTM().
	///
	/// \param[out]	pbRelativeToDTM			Whether or not heights of the location's points are relative to DTM.
	///										The parameter's meaning depends on \a puPropertyID (see SetXXX()).
	/// \param[out] puPropertyID			The private property ID or one of the following special values:
	///										IMcProperty::EPPI_SHARED_PROPERTY_ID.
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRelativeToDTM(bool *pbRelativeToDTM, UINT *puPropertyID = NULL) const = 0;

	//@}

	/// \name Max number of points
	//@{

	//=================================================================================================
	// Function name: SetMaxNumPoints()
	//
	//-------------------------------------------------------------------------------------------------
	/// Sets the maximal number of points allowed for this location, as a shared or private property.
	///
	/// \param[in] uMaxNumPoints		The maximal number of points allowed; 0 means no limit
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID.
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	///
	/// \return
	///     - Status result
	//=================================================================================================
	virtual IMcErrors::ECode SetMaxNumPoints(
		UINT uMaxNumPoints, UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID) = 0;

	//=================================================================================================
	// Function name: GetMaxNumPoints()
	//
	//-------------------------------------------------------------------------------------------------
	/// Retrieves the maximal number of points allowed for this location property set by SetMaxNumPoints().
	///
	/// \param[out]	puMaxNumPoints		The maximal number of points allowed; 0 means no limit
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID.
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	///
	/// \return
	///     - Status result
	//=================================================================================================
	virtual IMcErrors::ECode GetMaxNumPoints(
		UINT *puMaxNumPoints, UINT *puPropertyID = NULL) const = 0;

	//@}

};
