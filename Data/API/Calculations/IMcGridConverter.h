#pragma once
//===========================================================================
/// \file IMcGridConverter.h
/// The interface for converting any coordinate system to another 
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcBase.h"
#include "SMcVector.h"
#include "IMcGridCoordinateSystem.h"

//===========================================================================
// Interface Name: IMcGridConverter
//---------------------------------------------------------------------------
/// The interface for converting any coordinate system to another. 
///
//===========================================================================
class IMcGridConverter : virtual public IMcBase
{
protected:
	virtual ~IMcGridConverter() {};
public:
	/// \name Create 
	//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates an interface for a Converter between two Grid Coordinate systems  
	///
	/// \param[out]	ppGridConvertor				Grid Coordinate system Converter interface created
	/// \param[in]	pGridCoordinateSystem_A		The first coordinate system
	/// \param[in]	pGridCoordinateSystem_B		The second coordinate system
	/// \param[in]	bConvertHeight				Whether or not to convert height
	//
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode GRIDCOORDINATESYSTEMS_API Create(IMcGridConverter **ppGridConvertor,
		const IMcGridCoordinateSystem *pGridCoordinateSystem_A,
		const IMcGridCoordinateSystem *pGridCoordinateSystem_B,
		bool bConvertHeight = true);

	//==============================================================================
	// Method Name: ConvertAtoB()
	//------------------------------------------------------------------------------
	/// Converts a coordinate from gridA to gridB  
	///
	/// \param[in]	LocationA					The coordinate in grid A
	/// \param[out]	pLocationB					The coordinate converted to grid B
	/// \param[out]	pnZoneB						if not NULL, retrieves the zone for grid B
	//
	/// \return
	///     - status result
	/// \note
	/// if grid B is a UTM grid initialized to zone 0 (any zone), than pnZoneB is essential 
	/// to define in which zone the result is
	//==============================================================================
	virtual IMcErrors::ECode ConvertAtoB(const SMcVector3D &LocationA, 
		SMcVector3D *pLocationB, int *pnZoneB=0 ) const = 0;

	//==============================================================================
	// Method Name: ConvertBtoA()
	//------------------------------------------------------------------------------
	/// Converts a coordinate from gridB to gridA  
	///
	/// \param[in]	LocationB					The coordinate in grid B
	/// \param[out]	pLocationA					The coordinate converted to grid A
	/// \param[out]	pnZoneA						if not NULL, retrieves the zone for grid A
	//
	/// \return
	///     - status result
	/// \note
	/// if grid A is a UTM grid initialized to zone 0 (any zone), than pnZoneB is essential 
	/// to define in which zone the result is
	//==============================================================================
	virtual IMcErrors::ECode ConvertBtoA(const SMcVector3D &LocationB, 
		SMcVector3D *pLocationA, int *pnZoneA=0) const = 0;

	//==============================================================================
	// Method Name: IsSameCoordinateSystem()
	//------------------------------------------------------------------------------
	/// Retrieves if the two grids are identical  
	///
	/// \param[out]	pbIsSame					true if both grids are identical
	//
	/// \return
	///     - status result
	/// \note
	/// two grids are identical if they are two instanced of the same interface with the same parameters
	//==============================================================================
	virtual IMcErrors::ECode	IsSameCoordinateSystem(bool *pbIsSame)const  = 0;

	//==============================================================================
	// Method Name: GetGridCoordinateSystem_A()
	//------------------------------------------------------------------------------
	/// Retrieves  Grid Coordinate System A  
	///
	/// \param[out]	ppGridCoordinateSystem_A		the returned grid
	//
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetGridCoordinateSystem_A(
		const IMcGridCoordinateSystem **ppGridCoordinateSystem_A) const  = 0;

	//==============================================================================
	// Method Name: GetGridCoordinateSystem_B()
	//------------------------------------------------------------------------------
	/// Retrieves  Grid Coordinate System B  
	///
	/// \param[out]	ppGridCoordinateSystem_B		the returned grid
	//
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetGridCoordinateSystem_B(
		const IMcGridCoordinateSystem **ppGridCoordinateSystem_B) const  = 0;

	//==============================================================================
	// Method Name: SetConvertingHeight()
	//------------------------------------------------------------------------------
	/// Sets whether or not to convert height 
	///
	/// \param[in]	bConvertHeight		Whether or not to convert height
	//
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetConvertingHeight(bool bConvertHeight) = 0;

	//==============================================================================
	// Method Name: GetConvertingHeight()
	//------------------------------------------------------------------------------
	/// Retrieves whether or not to convert height 
	///
	/// \param[out]	pbConvertHeight		Whether or not to convert height
	//
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetConvertingHeight(bool *pbConvertHeight) const = 0;

	//=================================================================================================
	// 
	// Function name: SetCheckGridLimits(...)
	// 
	// Author     : Omer Shelef
	// Date       : 19/12/2010
	//-------------------------------------------------------------------------------------------------
	/// Sets whether to disallow calculations beyond grid limits (disallow extended zones).
	///
	/// \param [in]		bCheckGridLimits	whether to disallow calculations beyond grid limits;
	///										the default is false (allow calculations beyond grid limits)
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode SetCheckGridLimits(bool bCheckGridLimits) = 0;

	//=================================================================================================
	// 
	// Function name: GetCheckGridLimits(...)
	// 
	// Author     : Omer Shelef
	// Date       : 19/12/2010
	//-------------------------------------------------------------------------------------------------
	/// Retrieves whether calculations beyond grid limits are disallowed (extended zones are disallowed) 
	///
	/// \param [out]	pbCheckGridLimits	whether calculations beyond grid limits are disallowed
	///
	/// \return
	///     - status result
	//=================================================================================================
	virtual IMcErrors::ECode GetCheckGridLimits(bool *pbCheckGridLimits) const = 0;

};



