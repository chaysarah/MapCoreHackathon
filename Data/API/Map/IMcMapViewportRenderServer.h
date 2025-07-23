#pragma once

//===========================================================================
/// \file IMcMapViewportRenderServer.h
/// Interfaces for map render server
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcMapRenderServer.h"

class IMcMapViewport;

//===========================================================================
// Interface Name: IMcMapViewportRenderServer
//---------------------------------------------------------------------------
/// Interface for map viewport render server
//===========================================================================
class IMcMapViewportRenderServer : public virtual IMcMapRenderServer
{
protected:
    virtual ~IMcMapViewportRenderServer() {}

public:
	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates map viewport render server interface
	///
	/// \param[out]	ppServer	map viewport render server interface created
	/// \param[in]	pViewport	map viewport to use for rendering client frames
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode Create(IMcMapViewportRenderServer **ppServer, IMcMapViewport *pViewport);

	//@}

	/// \name Viewport Render Server
	//@{

	//==============================================================================
	// Method Name: ProcessClientRequest()
	//------------------------------------------------------------------------------
	/// Processes a request received from client.
	/// 
	/// Should be called from MapCore's main thread. After it has ended, the result should be encoded 
	/// by EncodeClientRequestResult() (from any thread) and sent back to client.
	///
	/// \param[in]	strCommand		command to process
	/// \param[in]	adParams		command parameters
	/// \param[in]	uNumParams		number of command parameters
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ProcessClientRequest(PCSTR strCommand, double adParams[], UINT uNumParams) = 0;

	//==============================================================================
	// Method Name: ProcessClientCommand()
	//------------------------------------------------------------------------------
	/// Processes render command received from client.
	/// 
	/// Should be called in MapCore's main thread. It does not produce any answer to client.
	///
	/// \param[in]	strCommand		command to process
	/// \param[in]	adParams		command parameters
	/// \param[in]	uNumParams		number of command parameters
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ProcessClientCommand(PCSTR strCommand, double adParams[], UINT uNumParams) = 0;
	//@}
};
