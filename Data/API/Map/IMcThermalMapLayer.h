#pragma once

//===========================================================================
/// \file IMcThermalMapLayer.h
/// Interfaces for thermal map layers
//===========================================================================

#include "McExports.h"
#include "Map/IMcMapLayer.h"

//===========================================================================
// Interface Name: IMcThermalMapLayer
//---------------------------------------------------------------------------
/// The base interface for thermal layers
//===========================================================================
class IMcThermalMapLayer : virtual public IMcMapLayer
{
protected:
    virtual ~IMcThermalMapLayer() {}

public:

	enum
	{
		//================================================================
		/// Map layer unique ID for this interface
		//================================================================
		LAYER_TYPE = 51
	};


	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates MapCore's native thermal layer
	///
	/// \param[out]	ppLayer				native thermal layer created
	/// \param[in]	strDirectory		a directory containing the layer's files
	/// \param[in]  pReadCallback			Callback receiving read layer events
	///
	/// \return
	///     - status result
	//==============================================================================
	static MAPLAYER_API IMcErrors::ECode Create(IMcThermalMapLayer **ppLayer, PCSTR strDirectory, IReadCallback *pReadCallback = NULL);
};
