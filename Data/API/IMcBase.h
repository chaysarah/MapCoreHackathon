#pragma once
//===========================================================================
/// \file IMcBase.h
/// Base interface for Reference Count support
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "McBasicTypes.h"

//===========================================================================
// Interface Name: IMcBase
//---------------------------------------------------------------------------
/// Base interface for Reference Count support
///
//===========================================================================
class IMcBase
#ifdef __EMSCRIPTEN__
	: public IMcEmscriptenInterfaceRef
#endif
{
protected:
    virtual ~IMcBase()
    {
    }

public:

	/// \name Reference Count
	//@{

	//================================================================
    // Method Name: AddRef()
    //----------------------------------------------------------------
    /// Increases the reference count
	///
    //================================================================
    virtual void AddRef() const =0;

    //================================================================
    // Method Name: Release()
    //----------------------------------------------------------------
    /// Decreases the reference count.
    ///
    /// When reference count drops to zero the instance will be deleted.
    //================================================================
    virtual void Release() const =0;

	//================================================================
    // Method Name: GetRefCount()
    //----------------------------------------------------------------
    /// Returns the current reference count.
	///
	/// Use this value is for diagnostic and testing purposes only.
    //================================================================
    virtual int GetRefCount() const =0;

	//@}
};