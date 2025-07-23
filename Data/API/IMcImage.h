#pragma once
//==================================================================================
/// \file IMcImage.h
/// Interface for manipulating images
//==================================================================================

#include "McExports.h"
#include "McCommonTypes.h"
#include "IMcDestroyable.h"
#include "OverlayManager/IMcTexture.h"

//==================================================================================
// Interface Name: IMcImage
//----------------------------------------------------------------------------------
/// The interface for manipulating images
//==================================================================================
class IMcImage : public virtual IMcBase
{
protected:

	virtual ~IMcImage() {};

public:

    /// Filter to be used for resizing
	enum EResizeFilter
    {
        ERF_NEAREST,	///< nearest pixel
        ERF_BILINEAR	///< bi-linear interpolation
    };

	/// \name Create and Clone
    //@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates image from an image file or a memory buffer
	///
	/// \param[out]	ppImage				The pointer to the created image.
	/// \param[in]	ImageSource			The image file source (file-system file name or in-memory file buffer)
	///
	/// \return
	///     - Status result
	//==============================================================================
	static COMMONUTILS_API IMcErrors::ECode Create(IMcImage **ppImage, const SMcFileSource &ImageSource);

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates image from a pixel buffer
	///
	/// \param[out]	ppImage			The pointer to the created image.
	/// \param[in]	aPixelBuffer	The pixel buffer (should be of size CalcPixelBufferSize(uWidth, uHeight, ePixelFormat, uNumMipmaps)).
	/// \param[in]	uWidth			The image's width.
	/// \param[in]	uHeight			The image's height.
	/// \param[in]	ePixelFormat	The buffer's pixel format, see IMcTexture::EPixelFormat for details.
	/// \param[in]	uNumMipmaps		The number of mipmaps (image versions of dimensions reduced by 2, 4 and so on) in the buffer.
	///
	/// \return
	///     - Status result
	//==============================================================================
	static COMMONUTILS_API IMcErrors::ECode Create(IMcImage **ppImage, 
		const BYTE aPixelBuffer[], UINT uWidth, UINT uHeight, IMcTexture::EPixelFormat ePixelFormat, UINT uNumMipmaps = 0);

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates image from another image, optionally resize and/or flip the source image
	///
	/// \param[out]	ppImage				The pointer to the created image.
	/// \param[in]	pSrcImage			The source image.
	/// \param[in]	uWidth				The new image's width or zero to keep the original width.
	/// \param[in]	uHeight				The new image's height or zero to keep the original height.
	/// \param[in]	eFilter				The optional filter to use for resizing; the default is #ERF_BILINEAR.
	/// \param[in]	bFlipAroundX		Whether or not to flip (mirror) the image around the X-axis.
	/// \param[in]	bFlipAroundY		Whether or not to flip (mirror) the image around the Y-axis.
	///
	/// \return
	///     - Status result
	//==============================================================================
	static COMMONUTILS_API IMcErrors::ECode Create(IMcImage **ppImage, const IMcImage *pSrcImage,
		UINT uWidth = 0, UINT uHeight = 0, EResizeFilter eFilter = ERF_BILINEAR,
		bool bFlipAroundX = false, bool bFlipAroundY = false);

	//==============================================================================
	// Method Name: Clone()
	//------------------------------------------------------------------------------
	/// Clones the image
	///
	/// \return		The created clone.
	//==============================================================================
	virtual IMcImage* Clone() const = 0;

	//@}

	/// \name Getters, Utilities
    //@{

	//==============================================================================
	// Method Name: GetFileSource()
	//------------------------------------------------------------------------------
	/// Retrieves the image file source used for creating the image
	///
	/// \return		The image file source (file-system file name or in-memory file buffer)
	///				used for creating the image.
	///				If image was not created by file source, an empty file source will be returned.
	//==============================================================================
	virtual const SMcFileSource* GetFileSource() const = 0;

	//==============================================================================
	// Method Name: GetPixelBuffer()
	//------------------------------------------------------------------------------
	/// Retrieves the image's pixel buffer
	///
	/// \param[out]	puPixelBufferSize	The optional size of the pixel buffer returned; pass `NULL` (the default) if unnecessary.
	///
	/// \return							The pixel buffer.
	//==============================================================================
	virtual BYTE* GetPixelBuffer(UINT *puPixelBufferSize = NULL) const = 0;

	//==============================================================================
	// Method Name: GetNumMipmaps()
	//------------------------------------------------------------------------------
	/// Retrieves the number of mipmaps (image versions of dimensions reduced by 2, 4 and so on) in the image.
	///
	/// \return		The number of mipmaps.
	//==============================================================================
	virtual UINT GetNumMipmaps() const = 0;

	//==============================================================================
	// Method Name: GetSize()
	//------------------------------------------------------------------------------
	/// Retrieves the image's dimensions.
	///
	/// \param[out]	puWidth		The width.
	/// \param[out]	puHeight	The height.
	///
	//==============================================================================
	virtual void GetSize(UINT *puWidth, UINT *puHeight) const = 0;

	//==============================================================================
	// Method Name: GetPixelFormat()
	//------------------------------------------------------------------------------
	/// Retrieves the image's pixel format
	///
	/// \return		The pixel format, see IMcTexture::EPixelFormat for details.
	//==============================================================================
	virtual IMcTexture::EPixelFormat GetPixelFormat() const = 0;

    //==============================================================================
    // Method Name: CalcPixelBufferSize()
    //------------------------------------------------------------------------------
    /// Calculates the pixel buffer size (in bytes) required for an image of the specified parameters. 
    ///
    /// \param[in]	uWidth			The width.
    /// \param[in]	uHeight			The height.
    /// \param[in]	ePixelFormat	The pixel format, see IMcTexture::EPixelFormat for details.
    /// \param[in]	uNumMipmaps		The number of mipmaps (image versions of dimensions reduced by 2, 4 and so on).
    ///
    /// \return						The pixel buffer size required (in bytes)
    //==============================================================================
    static COMMONUTILS_API UINT CalcPixelBufferSize(UINT uWidth, UINT uHeight, IMcTexture::EPixelFormat ePixelFormat, UINT uNumMipmaps = 0);

	//@}
	/// \name Save
    //@{

	//==============================================================================
	// Method Name: Save()
	//------------------------------------------------------------------------------
	/// Saves the image into an image file of the format defined by the file extension of the file name specified.
	///
	/// \param[in]	strFileName		The full name of the file with an extension defining the format (for example "XXX.jpg").
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Save(PCSTR strFileName) const = 0;

	//==============================================================================
	// Method Name: Save()
	//------------------------------------------------------------------------------
	/// Saves the image into an image file (as a memory buffer) of the format defined by the extension specified.
	///
	/// \param[out]	paImageFileMemoryBuffer		The memory buffer filled by the function.
	/// \param[in]	strFormatExtension			The file extension defining the format (for example "jpg").
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Save(CMcDataArray<BYTE> *paImageFileMemoryBuffer, PCSTR strFormatExtension) const = 0;

	//@}

};
