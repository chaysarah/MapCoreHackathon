#pragma once
//==================================================================================
/// \file IMcVideoTextureStreamer.h
/// The interfaces for video texture streamer
//==================================================================================

#include "IMcErrors.h"
#include "IMcBase.h"
#include "McCommonTypes.h"

class IMcSharedMemoryVideoTextureStreamer;

//==================================================================================
// Interface Name: IMcVideoTextureStreamer
//----------------------------------------------------------------------------------
/// The video texture streamer interface
///
/// The base interface for all video texture streamer interfaces
//==================================================================================
class IMcVideoTextureStreamer : public virtual IMcBase
{
protected:

	virtual ~IMcVideoTextureStreamer() {};

public:

    /// \name Video Texture Streamer Type And Casting
    //@{

    //==============================================================================
    // Method Name: GetVideoTextureStreamerType() 
    //------------------------------------------------------------------------------
    /// Returns the video texture streamer type unique id.
    ///
 	/// \remark
	///		Use the cast methods in order to get the correct type.
   //==============================================================================
    virtual UINT GetVideoTextureStreamerType() const = 0;

    //==============================================================================
    // Method Name: CastToSharedMemoryVideoTextureStreamer(...)
    //------------------------------------------------------------------------------
    /// Casts the #IMcVideoTextureStreamer* To #IMcSharedMemoryVideoTextureStreamer*
    /// 
    /// \return
    ///     - #IMcSharedMemoryVideoTextureStreamer*
    //==============================================================================
	virtual IMcSharedMemoryVideoTextureStreamer* CastToSharedMemoryVideoTextureStreamer() = 0;

	//@}

	/// \name Video texture streaming
    //@{

	//==============================================================================
	// Method Name: GetCurrFramePointer()
	//------------------------------------------------------------------------------
	/// Retrieves a pointer to the current frame to be used to copy the frame bits
	///
	/// \param[out]	ppFrameBits		a pointer to the frame bits
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCurrFramePointer(BYTE **ppFrameBits) = 0;

	//==============================================================================
	// Method Name: Send()
	//------------------------------------------------------------------------------
	/// Sends the current frame and/or optional camera parameters and/or optional array of telemetries 
	///
	/// \param[in]	bSendFrame			whether or not the current frame (copied to a pointer returned by 
	///									GetCurrFramePointer()) should be sent
	/// \param[in]	uTimestampInMS		frame timestamp in ms since the start of the process
	/// \param[in]	uFrameIndex			frame index
	/// \param[in]  fTelemetryQuality   0 - original sensor telemetry, 1 - fixed by registration algorithm telemetry
	/// \param[in]	pCameraParams		optional camera parameters
	/// \param[in]	aTelemetries		optional array of telemetries of length specified in Create()
	///									(of the camera itself and/or of other parts).
	/// \param[in]	aUserDataBytes[]	optional user data (array of bytes)
	/// \param[in]	uNumUserDataBytes	optional number of bytes in user data
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Send(bool bSendFrame, UINT64 uTimestampInMS, UINT uFrameIndex, float fTelemetryQuality, const SMcCameraProjectionParams *pCameraParams = NULL,
		 const SMcPositionAndOrientation *aTelemetries = NULL, BYTE aUserDataBytes[] = NULL, UINT uNumUserDataBytes = 0) = 0;
	//@}
};

//==================================================================================
// Interface Name: IMcSharedMemoryVideoTextureStreamer
//----------------------------------------------------------------------------------
/// The shared-memory video texture streamer interface
//==================================================================================
class IMcSharedMemoryVideoTextureStreamer : public virtual IMcVideoTextureStreamer
{
protected:

	virtual ~IMcSharedMemoryVideoTextureStreamer() {};

public:

	enum
	{
		//==============================================================================
		/// Video texture streamer unique ID for this interface
		//==============================================================================
		VIDEO_TEXTURE_STREAMER_TYPE = 1
	};

	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates shared-memory video texture streamer
	///
	/// \param[out]	ppTextureStreamer		pointer to the created interface
	/// \param[in]	strSharedMemoryName		name of shared memory to use
	/// \param[in]	uFrameWidth				frame width
	/// \param[in]	uFrameHeight			frame height
	/// \param[in]	ePixelFormat			pixel format
	/// \param[in]	uNumTelemetries			number of telemetries
	/// \param[in]	uMaxNumUserDataBytes	maximal number of user data bytes used in Send()
	///
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(IMcSharedMemoryVideoTextureStreamer **ppTextureStreamer, 
		PCSTR strSharedMemoryName, UINT uFrameWidth, UINT uFrameHeight, IMcTexture::EPixelFormat ePixelFormat, 
		UINT uNumTelemetries, UINT uMaxNumUserDataBytes = 0);
	//@}
};
