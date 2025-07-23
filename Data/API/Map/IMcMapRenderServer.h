#pragma once

//===========================================================================
/// \file IMcMapRenderServer.h
/// Interfaces for map render server
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcDestroyable.h"
#include "CMcDataArray.h"
#include "OverlayManager/IMcLineBasedItem.h"

class IMcMapViewport;
class IMcObject;
class IMcObjectSchemeItem;
class IMcTexture;
class IMcFont;
class IMcOverlay;

//===========================================================================
// Interface Name: IMcMapRenderServer
//---------------------------------------------------------------------------
/// Interface for map render server
//===========================================================================
class IMcMapRenderServer : public IMcDestroyable
{
protected:
    virtual ~IMcMapRenderServer() {}

public:

	/// \name Render Server
	//@{

	//==============================================================================
	// Method Name: GetMapViewport()
	//------------------------------------------------------------------------------
	/// Retrieves map viewport used for rendering client frames
	///
	/// \param[out]	ppViewport		viewport used for rendering client frames
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetMapViewport(IMcMapViewport **ppViewport) const = 0;

	//==============================================================================
	// Method Name: EncodeClientRequestResult()
	//------------------------------------------------------------------------------
	/// Encodes the result of the last processed client request.
	/// 
	/// Can be called from any thread after a client request function has ended in MapCore's main thread.
	///
	/// \param[out]	paEncodedResult		memory buffer with encoded result to be sent to client
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode EncodeClientRequestResult(CMcDataArray<BYTE> *paEncodedResult) = 0;

};
