#pragma once
//===========================================================================
/// \file IMcDestroyable.h
/// The base interface for interfaces supporting destroying
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "McBasicTypes.h"


//===========================================================================
/// The base interface for interfaces supporting destroying
//===========================================================================

class IMcDestroyable
#ifdef __EMSCRIPTEN__
	: public IMcEmscriptenInterfaceRef
#endif
{
protected:
    virtual ~IMcDestroyable() {}

public:
	/// \name Destroy
	//@{

	//==============================================================================
	// Method Name: Destroy()
	//------------------------------------------------------------------------------
	/// Destroys the object implementing this interface
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Destroy() = 0;

	//@}
};
