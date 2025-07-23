#pragma once

//===========================================================================
/// \file IMcMapLayersRenderServer.h
/// Interface for map layers render server
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcMapRenderServer.h"
#include "CMcDataArray.h"
#include "SMcVector.h"
#include "SMcColor.h"

class IMcMapViewport;
class IMcMapLayer;

//===========================================================================
// Interface Name: IMcMapLayersRenderServer
//---------------------------------------------------------------------------
/// Interface for map layers render server
//===========================================================================
class IMcMapLayersRenderServer : public virtual IMcMapRenderServer
{
protected:
    virtual ~IMcMapLayersRenderServer() {}

public:
	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates map layer server interface
	///
	/// \param[out]	ppServer		map layer server interface created
	/// \param[in]	pViewport		map viewport to use for rendering client frames
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode Create(IMcMapLayersRenderServer **ppServer, IMcMapViewport *pViewport);

	//@}

	/// \name Layers Render Server
	//@{

	//==============================================================================
	// Method Name: ProcessWMSGetMapRequest()
	//------------------------------------------------------------------------------
	/// Processes a request received from client.
	/// 
	/// Should be called from MapCore's main thread. After it has ended, the result should be encoded 
	/// by EncodeClientRequestResult() (from any thread) and sent back to client.
	///
	/// \param[in]	bNewClient			whether the client has changed
	/// \param[in]	apLayers[]			array of layers to render
	/// \param[in]	uNumLayers			number of layers.
	/// \param[in]	BoundingBox			bounding box of the requested area (\a z is ignored)
	/// \param[in]	uImageWidth			width of the image rendered
	/// \param[in]	uImageHeight		height of the image rendered
	/// \param[in]	bPng				whether PNG or JPG image is required
	/// \param[in]	BackgroundColor		optional background color (the default is black transparent)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ProcessWMSGetMapRequest(bool bNewClient, IMcMapLayer *const apLayers[], UINT uNumLayers, 
		const SMcBox &BoundingBox, UINT uImageWidth, UINT uImageHeight, bool bPng, 
		const SMcBColor &BackgroundColor = bcBlackTransparent) = 0;
	//@}
};
