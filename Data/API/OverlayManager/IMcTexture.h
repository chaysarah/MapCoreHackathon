#pragma once
//==================================================================================
/// \file IMcTexture.h
/// The interface for texture resource
//==================================================================================

#include "IMcErrors.h"
#include "IMcBase.h"
#include "CMcDataArray.h"
#include "McCommonTypes.h"
struct SMcBColor;
class IMcImageFileTexture;
class IMcMemoryBufferTexture;
class IMcResourceTexture;
class IMcIconHandleTexture;
class IMcBitmapHandleTexture;
class IMcMovieTexture;
class IMcMovieFileTexture;
class IMcVideoTexture;
class IMcDirectShowTexture;
class IMcDirectShowGraphTexture;
class IMcDirectShowSourceFileTexture;
class IMcDirectShowUSBCameraTexture;
class IMcFFMpegVideoTexture;
class IMcHtmlVideoTexture;
class IMcSharedMemoryVideoTexture;
class IMcMemoryBufferVideoTexture;
class IMcTextureArray;

//==================================================================================
// Interface Name: IMcTexture
//----------------------------------------------------------------------------------
/// The texture interface
///
/// The base interface for all texture resources
//==================================================================================
class IMcTexture : public virtual IMcBase
{
protected:

	virtual ~IMcTexture() {};

public:

	/// Pixel format (bits order is from the most significant bit to the least significant one).
	enum EPixelFormat
	{
		/// Unknown pixel format.
		EPF_UNKNOWN = 0,
		/// 8-bit pixel format, all bits luminance.
		EPF_L8 = 1,
		/// 16-bit pixel format, all bits luminance.
		EPF_L16 = 2,
		/// 8-bit pixel format, all bits alpha.
		EPF_A8 = 3,
		/// 2 byte pixel format, 1 byte luminance, 1 byte alpha
		EPF_BYTE_LA = 4,
		/// 16-bit pixel format, 5 bits red, 6 bits green, 5 bits blue.
		EPF_R5G6B5 = 5,
		/// 16-bit pixel format, 5 bits red, 6 bits green, 5 bits blue.
		EPF_B5G6R5 = 6,
		/// 16-bit pixel format, 4 bits for alpha, red, green and blue.
		EPF_A4R4G4B4 = 7,
		/// 16-bit pixel format, 5 bits for blue, green, red and 1 for alpha.
		EPF_A1R5G5B5 = 8,
		/// 24-bit pixel format, 8 bits for red, green and blue.
		EPF_R8G8B8 = 9,
		/// 24-bit pixel format, 8 bits for blue, green and red.
		EPF_B8G8R8 = 10,
		/// 32-bit pixel format, 8 bits for alpha, red, green and blue.
		EPF_A8R8G8B8 = 11,
		/// 32-bit pixel format, 8 bits for blue, green, red and alpha.
		EPF_A8B8G8R8 = 12,
		/// 32-bit pixel format, 8 bits for blue, green, red and alpha.
		EPF_B8G8R8A8 = 13,
		/// 32-bit pixel format, 2 bits for alpha, 10 bits for red, green and blue.
		EPF_A2R10G10B10 = 14,
		/// 32-bit pixel format, 10 bits for blue, green and red, 2 bits for alpha.
		EPF_A2B10G10R10 = 15,
        /// DDS (DirectDraw Surface) DXT1 format
		EPF_DXT1 = 16,
        /// DDS (DirectDraw Surface) DXT2 format
		EPF_DXT2 = 17,
        /// DDS (DirectDraw Surface) DXT3 format
		EPF_DXT3 = 18,
        /// DDS (DirectDraw Surface) DXT4 format
		EPF_DXT4 = 19,
        /// DDS (DirectDraw Surface) DXT5 format
		EPF_DXT5 = 20,
		/// 48-bit pixel format, 16 bits (float) for red, 16 bits (float) for green, 16 bits (float) for blue
		EPF_FLOAT16_RGB = 21,
		/// 64-bit pixel format, 16 bits (float) for red, 16 bits (float) for green, 16 bits (float) for blue, 16 bits (float) for alpha
		EPF_FLOAT16_RGBA = 22,
		/// 96-bit pixel format, 32 bits (float) for red, 32 bits (float) for green, 32 bits (float) for blue
		EPF_FLOAT32_RGB = 23,
		/// 128-bit pixel format, 32 bits (float) for red, 32 bits (float) for green, 32 bits (float) for blue, 32 bits (float) for alpha
		EPF_FLOAT32_RGBA = 24,
        /// 32-bit pixel format, 8 bits for red, 8 bits for green, 8 bits for blue
        /// like Ogre::PF_A8R8G8B8, but alpha will get discarded
        EPF_X8R8G8B8 = 25,
        /// 32-bit pixel format, 8 bits for blue, 8 bits for green, 8 bits for red
        /// like Ogre::PF_A8B8G8R8, but alpha will get discarded
        EPF_X8B8G8R8 = 26,
        /// 32-bit pixel format, 8 bits for red, green, blue and alpha.
        EPF_R8G8B8A8 = 27,
		/// Depth texture format
		EPF_DEPTH = 28,
		/// 64-bit pixel format, 16 bits for red, green, blue and alpha
		EPF_SHORT_RGBA = 29,
        /// 8-bit pixel format, 2 bits blue, 3 bits green, 3 bits red.
        EPF_R3G3B2 = 30,
        /// 16-bit pixel format, 16 bits (float) for red
        EPF_FLOAT16_R = 31,
        /// 32-bit pixel format, 32 bits (float) for red
        EPF_FLOAT32_R = 32,
		/// 32-bit pixel format, 16-bit green, 16-bit red
    	EPF_SHORT_GR = 33,
        /// 32-bit, 2-channel s10e5 floating point pixel format, 16-bit green, 16-bit red
        EPF_FLOAT16_GR = 34,
        /// 64-bit, 2-channel floating point pixel format, 32-bit green, 32-bit red
        EPF_FLOAT32_GR = 35,
        /// 48-bit pixel format, 16 bits for red, green and blue
        EPF_SHORT_RGB = 36,
		/// Number of pixel formats currently defined
		EPF_COUNT = 37
	};

	/// Texture usage (for advanced users only); the default is EU_STATIC_WRITE_ONLY.
	enum EUsage
	{
		/// Static texture which the application rarely updates once created;
		/// updating its contents will involve a performance hit.
		EU_STATIC							= 1,

		/// Static texture which the application rarely updates once created and never 
		/// reads; updating its contents will involve a performance hit.
		///
		/// If the texture is not to be used as a fill pattern of line / closed shape or as a projector texture, 
		/// consider using #EU_STATIC_WRITE_ONLY_IN_ATLAS instead for better performance and (in some video 
		/// cards with limitations on texture sizes supported) to avoid resampling
		EU_STATIC_WRITE_ONLY				= 5,

		/// Static texture which the application rarely updates once created and never 
		/// reads; updating its contents will involve a performance hit; for better performance the 
		/// texture will be automatically inserted into atlas of objects textures together with other 
		/// textures of similar sizes.
		/// 
		/// The following limitations will be applied:
		///   - The texture format will be #EPF_A8R8G8B8 (32 bit) or #EPF_A1R5G5B5 (16 bit) depending on 
		///		IMcMapDevice::SInitParams::bObjectsTexturesAtlas16bit flag
		///   - Mipmaps (levels of detail) will not be generated for this texture.
		///   - The texture cannot be used as a fill pattern of line / closed shape or as a projector texture
		///   - Neither the texture contents nor underlying DirectX texture can be retrieved.
		EU_STATIC_WRITE_ONLY_IN_ATLAS		= -1,

		/// Dynamic texture which the application will update fairly often; 
		/// will be allocated in AGP memory rather than video memory.
		EU_DYNAMIC							= 2,

		/// Dynamic texture which the application will update fairly often but will never 
		/// read; will be allocated in AGP memory rather than video memory.
		EU_DYNAMIC_WRITE_ONLY				= 6,

		/// Dynamic texture which the application will never read but will update regularly 
		/// (not just updating, but generating the contents from scratch) and therefore does not mind 
		/// if the contents of the texture are lost somehow and need to be recreated; 
		/// will be allocated in AGP memory rather than video memory.
		EU_DYNAMIC_WRITE_ONLY_DISCARDABLE	= 14,

		/// This texture will be a render target, i.e. used as a target for render to texture.
		EU_RENDERTARGET						= 512
	};

	/// Color substitution (one color to one color)
	struct SColorSubstitution
	{
		SMcBColor ColorToSubstitute;	///< The color value to substitute
		SMcBColor SubstituteColor;		///< The color value to place instead

		/// Constructor
		SColorSubstitution(
			const SMcBColor& _ColorToSubstitute = SMcBColor(0, 0, 0, 0),
			const SMcBColor& _SubstituteColor = SMcBColor(0, 0, 0, 0)) :
			ColorToSubstitute(_ColorToSubstitute), SubstituteColor(_SubstituteColor) {}

		bool operator== (const SColorSubstitution& a) const
		{
			return (ColorToSubstitute == a.ColorToSubstitute && SubstituteColor == a.SubstituteColor);
		}

		bool operator!= (const SColorSubstitution& a) const
		{
			return (ColorToSubstitute != a.ColorToSubstitute || SubstituteColor != a.SubstituteColor);
		}
	};

    /// \name Texture Type And Casting
    //@{

    //==============================================================================
    // Method Name: GetTextureType() 
    //------------------------------------------------------------------------------
    /// Returns the texture type unique id.
    ///
 	/// \remark
	///		Use the cast methods in order to get the correct type.
   //==============================================================================
    virtual UINT GetTextureType() const = 0;

    //==============================================================================
    // Method Name: CastToImageFileTexture(...)
    //------------------------------------------------------------------------------
    /// Casts the #IMcTexture* To #IMcImageFileTexture*
    /// 
    /// \return
    ///     - #IMcImageFileTexture*
    //==============================================================================
	virtual IMcImageFileTexture* CastToImageFileTexture() = 0;

	//==============================================================================
	// Method Name: CastToMemoryBufferTexture(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcTexture* To #IMcMemoryBufferTexture*
	/// 
	/// \return
	///     - #IMcMemoryBufferTexture*
	//==============================================================================
	virtual IMcMemoryBufferTexture* CastToMemoryBufferTexture() = 0;

    //==============================================================================
    // Method Name: CastToResourceTexture(...)
    //------------------------------------------------------------------------------
    /// Casts the #IMcTexture* To #IMcResourceTexture*
    /// 
    /// \return
    ///     - #IMcResourceTexture*
    //==============================================================================
	virtual IMcResourceTexture* CastToResourceTexture() = 0;
	
    //==============================================================================
    // Method Name: CastToIconHandleTexture(...)
    //------------------------------------------------------------------------------
    /// Casts the #IMcTexture* To #IMcIconHandleTexture*
    /// 
    /// \return
    ///     - #IMcIconHandleTexture*
    //==============================================================================
	virtual IMcIconHandleTexture* CastToIconHandleTexture() = 0;
	
    //==============================================================================
    // Method Name: CastToBitmapHandleTexture(...)
    //------------------------------------------------------------------------------
    /// Casts the #IMcTexture* To #IMcBitmapHandleTexture*
    /// 
    /// \return
    ///     - #IMcBitmapHandleTexture*
    //==============================================================================
	virtual IMcBitmapHandleTexture* CastToBitmapHandleTexture() = 0;
	
	//==============================================================================
	// Method Name: CastToVideoTexture(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcTexture* To #IMcVideoTexture*
	/// 
	/// \return
	///     - #IMcVideoTexture*
	//==============================================================================
	virtual IMcVideoTexture* CastToVideoTexture() = 0;
	
	//==============================================================================
	// Method Name: CastToDirectShowTexture(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcTexture* To #IMcDirectShowTexture*
	/// 
	/// \return
	///     - #IMcDirectShowTexture*
	//==============================================================================
	virtual IMcDirectShowTexture* CastToDirectShowTexture() = 0;
	
	//==============================================================================
	// Method Name: CastToDirectShowGraphTexture(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcTexture* To #IMcDirectShowGraphTexture*
	/// 
	/// \return
	///     - #IMcDirectShowGraphTexture*
	//==============================================================================
	virtual IMcDirectShowGraphTexture* CastToDirectShowGraphTexture() = 0;
	
	//==============================================================================
	// Method Name: CastToDirectShowSourceFileTexture(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcTexture* To #IMcDirectShowSourceFileTexture*
	/// 
	/// \return
	///     - #IMcDirectShowSourceFileTexture*
	//==============================================================================
	virtual IMcDirectShowSourceFileTexture* CastToDirectShowSourceFileTexture() = 0;

	//==============================================================================
	// Method Name: CastToDirectShowUSBCameraTexture(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcTexture* To #IMcDirectShowUSBCameraTexture*
	/// 
	/// \return
	///     - #IMcDirectShowUSBCameraTexture*
	//==============================================================================
	virtual IMcDirectShowUSBCameraTexture* CastToDirectShowUSBCameraTexture() = 0;

	//==============================================================================
	// Method Name: CastToFFMpegVideoTexture()
	//------------------------------------------------------------------------------
	/// Casts the #IMcTexture* to #IMcFFMpegVideoTexture*
	///
	/// \return
	///     - #IMcFFMpegVideoTexture*
	//==============================================================================
	virtual IMcFFMpegVideoTexture* CastToFFMpegVideoTexture() = 0;

	//==============================================================================
	// Method Name: CastToHtmlVideoTexture()
	//------------------------------------------------------------------------------
	/// Casts the #IMcTexture* to #IMcHtmlVideoTexture*
	///
	/// \return
	///     - #IMcHtmlVideoTexture*
	//==============================================================================
	virtual IMcHtmlVideoTexture* CastToHtmlVideoTexture() = 0;

	//==============================================================================
	// Method Name: CastToSharedMemoryVideoTexture()
	//------------------------------------------------------------------------------
	/// Casts the #IMcTexture* to #IMcSharedMemoryVideoTexture*
	///
	/// \return
	///     - #IMcSharedMemoryVideoTexture*
	//==============================================================================
	virtual IMcSharedMemoryVideoTexture* CastToSharedMemoryVideoTexture() = 0;

	//==============================================================================
	// Method Name: CastToMemoryBufferVideoTexture(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcTexture* To #IMcMemoryBufferVideoTexture*
	/// 
	/// \return
	///     - #IMcMemoryBufferVideoTexture*
	//==============================================================================
	virtual IMcMemoryBufferVideoTexture* CastToMemoryBufferVideoTexture() = 0;

	//==============================================================================
	// Method Name: CastToTextureArray(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcTexture* To #IMcTextureArray*
	/// 
	/// \return
	///     - #IMcTextureArray*
	//==============================================================================
	virtual IMcTextureArray* CastToTextureArray() = 0;

	//@}

    /// \name Format, Size, Transparent Color, Color Substitutions
    //@{

	//==============================================================================
	// Method Name: GetPixelFormatByteCount()
	//------------------------------------------------------------------------------
	/// Retrieves pixel byte count (number of bytes used by each pixel) for specified pixel format
	///
	/// \param[in]	ePixelFormat		Pixel format.
	/// \param[out]	puPixelByteCount	Pixel byte count (0 is returned for compressed formats)
	///
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode GetPixelFormatByteCount(EPixelFormat ePixelFormat, 
																	   UINT *puPixelByteCount);

	//==============================================================================
	// Method Name: GetSize(...)
	//------------------------------------------------------------------------------
	/// Retrieves texture size.
	///
	/// For memory buffer texture and Direct Show textures only: this size is the effective size and can be 
	/// different from the source size returned by GetSourceSize().
	///
	/// \param[out] puWidth		Texture width
	/// \param[out] puHeight	Texture height
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetSize(UINT *puWidth, UINT *puHeight) const = 0;

	//==============================================================================
	// Method Name: GetSourceSize(...)
	//------------------------------------------------------------------------------
	/// Retrieves the texture's source size.
	/// 
	/// For memory buffer texture and Direct Show textures only: the source size can be different from 
	/// the effective size returned by GetSize().
	///
	/// \param[out] puWidth		Texture width
	/// \param[out] puHeight	Texture height
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetSourceSize(UINT *puWidth, UINT *puHeight) const = 0;

	//==============================================================================
	// Method Name: GetTransparentColor(...)
	//------------------------------------------------------------------------------
	/// Retrieves transparent color.
	///
	/// \param[out]	pTransparentColor				Color value to replace with transparent 
	///												(valid only if color key is enabled)
	///	\param[out]	pbIsTransparentColorEnabled		Whether transparent color is enabled
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetTransparentColor(SMcBColor *pTransparentColor, bool *pbIsTransparentColorEnabled) const = 0;
	
	//==============================================================================
	// Method Name: GetColorSubstitutions(...)
	//------------------------------------------------------------------------------
	/// Retrieves color substitutions previously set.
	///
	/// \param[out]	paColorSubstitutions		Array of color substitution pairs
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetColorSubstitutions(
		CMcDataArray<SColorSubstitution> *paColorSubstitutions) const = 0;

	//@}

	/// \name Creation Parameters
	//@{

	//==============================================================================
	// Method Name: IsFillPattern(...)
	//------------------------------------------------------------------------------
	/// Retrieves whether texture uses include a fill pattern of line / closed shape or a projector texture.
	///
	/// \param[out] pbFillPattern		Whether texture uses include a fill pattern 
	///									of line / closed shape or a projector texture
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode IsFillPattern(bool *pbFillPattern) const  = 0;

	//==============================================================================
	// Method Name: IsTransparentMarginIgnored(...)
	//------------------------------------------------------------------------------
	/// Retrieves whether transparent margins of the texture should be ignored.
	///
	/// \param[out] pbIgnoreTransparentMargin	Whether transparent margins of the texture 
	///											should be ignored
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode IsTransparentMarginIgnored(bool *pbIgnoreTransparentMargin) const = 0;

	//==============================================================================
	// Method Name: IsCreatedWithUseExisting(...)
	//------------------------------------------------------------------------------
	/// Retrieves whether the texture was created with use-existing flag.
	///
	/// \param[out] pbCreatedWithUseExisting	True if \a bUseExisting parameter of 
	///											Create() function was true and the texture's 
	///											parameters were not changed after creation.
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode IsCreatedWithUseExisting(bool *pbCreatedWithUseExisting) const = 0;

	//==============================================================================
	// Method Name: GetName()
	//------------------------------------------------------------------------------
	/// Retrieves the texture's unique name.
	///
	/// \param[in]	pstrUniqueName		The texture's unique name
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetName(PCSTR *pstrUniqueName) const = 0;

	//@}

};

//==================================================================================
// Interface Name: IMcImageFileTexture
//----------------------------------------------------------------------------------
/// The interface for texture loaded from image file (file-system file or in-memory file buffer)
//==================================================================================
class IMcImageFileTexture : public virtual IMcTexture
{
protected:

	virtual ~IMcImageFileTexture() {};

public:

    enum
    {
        //==============================================================================
        /// Texture unique ID for this interface
        //==============================================================================
        TEXTURE_TYPE = 1
    };

	/// \name Image File
    //@{

	//==============================================================================
	// Method Name: SetImageFile(...)
	//------------------------------------------------------------------------------
	/// Sets the image file name.
	///
	/// \param[in]	ImageSource				The image file source (file-system file name or in-memory file buffer)
	/// \param[in]	pTransparentColor		The color value to replace with transparent or NULL
	///										to disable color key
	/// \param[in]	aColorSubstitutions[]	The array of color substitution pairs
	/// \param[in]	uNumColorSubstitutions	The number of color substitution pairs in the above array
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetImageFile(
		const SMcFileSource &ImageSource,
		const SMcBColor *pTransparentColor = NULL,
		const SColorSubstitution aColorSubstitutions[] = NULL,
		UINT uNumColorSubstitutions = 0) = 0;
 
	//==============================================================================
	// Method Name: GetImageFile(...)
	//------------------------------------------------------------------------------
	/// Retrieves the image file source.
	///
	/// \param[out]	pImageSource		The image file source (file-system file name or in-memory file buffer)
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetImageFile(SMcFileSource *pImageSource) const = 0;

    //@}

    /// \name Create
    //@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a texture from image file.
	///
	/// \param[out]	ppTexture					The pointer to the created texture
	/// \param[in]	ImageSource					The image file source (file-system file name or in-memory file buffer)
	/// \param[in]  bFillPattern				Whether texture uses include a fill pattern 
	///											of line / closed shape or a projector texture
	/// \param[in]  bIgnoreTransparentMargin	Whether transparent margins of the texture 
	///											should be ignored
	/// \param[in]	pTransparentColor			The color value to replace with transparent or 
	///											NULL to disable color key
	/// \param[in]	aColorSubstitutions[]		The array of color substitution pairs
	/// \param[in]	uNumColorSubstitutions		The number of color substitution pairs in the above array
	///	\param[in]	bUseExisting				If true and some texture based on this file already 
	///											exists, it will be returned instead of creating a new one
	///	\param[out]	pbExistingUsed				Whether an existing texture based on this file 
	///											was used or a new one was created
	///
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcImageFileTexture **ppTexture,
		const SMcFileSource &ImageSource,
		bool bFillPattern,
		bool bIgnoreTransparentMargin = false,
		const SMcBColor *pTransparentColor = NULL,
		const SColorSubstitution aColorSubstitutions[] = NULL,
		UINT uNumColorSubstitutions = 0,
		bool bUseExisting = true,
		bool *pbExistingUsed = NULL);

    //@}
};

//==================================================================================
// Interface Name: IMcMemoryBufferTexture
//----------------------------------------------------------------------------------
/// The interface for texture loaded from memory buffer
//==================================================================================
class IMcMemoryBufferTexture : public virtual IMcTexture
{
protected:

	virtual ~IMcMemoryBufferTexture() {};

public:

	enum
	{
		//==============================================================================
		/// Texture unique ID for this interface
		//==============================================================================
		TEXTURE_TYPE = 6
	};

	/// \name Memory Buffer
	//@{
	//==============================================================================
	// Method Name: UpdateFromMemoryBuffer(...)
	//------------------------------------------------------------------------------
	/// Update the texture according to the specified memory buffer.
	/// 
	/// \param[in]	uBufferWidth		The buffer width
	/// \param[in]	uBufferHeight		The buffer height
	/// \param[in]	eBufferPixelFormat	The buffer pixel format, see #EPixelFormat for details
	/// \param[in]	uBufferRowPitch		The buffer row pitch i.e. the offset in pixels between
	///									the leftmost pixel of one row and the leftmost pixel of the 
	///									next one (can be != \a uBufferWidth); \a 0 or \a uBufferWidth 
	///									can be passed for consecutive rows without gaps;
	/// \param[in]	pBuffer				The texture's buffer
	///
	/// \note 
	///		For compressed formats \a uBufferRowPitch must be always equal to 0 or \a uBufferWidth
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode UpdateFromMemoryBuffer(
		UINT uBufferWidth, 
		UINT uBufferHeight,
		EPixelFormat eBufferPixelFormat,
		UINT uBufferRowPitch,
		BYTE *pBuffer) = 0;

	//==============================================================================
	// Method Name: UpdateFromColorData(...)
	//------------------------------------------------------------------------------
	/// Update the texture according to the specified color data.
	/// 
	/// \param[in]	aColors[]					The array of colors
	/// \param[in]	uNumColors					The number of colors in the above array
	/// \param[in]	afColorPositions[]			The optional array of color positions relative to the texture's dimension (normalized 0 through 1); 
	///											first color's position > 0 or last color position < 1 mean continuing the appropriate color till the texture's edge (regardless of \a bColorInterpolation flag); 
	///											`NULL` / empty array (the default) means equidistrubuted colors
	/// \param[in]	uNumColorPositions			The number of color positions in the above array
	/// \param[in]	bColorInterpolation			Whether to interpolate colors between the positions (gradient fill) or continue each color till next color's postion (solid color strips);
	///											the default is `false' (solid color strips)
	/// \param[in]	bColorColumns				whether to fill with color columns or color rows; the default is `false' (rows)
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode UpdateFromColorData(
		const SMcBColor aColors[], UINT uNumColors,
		const float afColorPositions[] = NULL, UINT uNumColorPositions = 0,
		bool bColorInterpolation = false,
		bool bColorColumns = false) = 0;

	//==============================================================================
	// Method Name: GetToMemoryBuffer(...)
	//------------------------------------------------------------------------------
	/// Copies texture into the specified memory buffer.
	/// 
	/// \param[in]	uBufferWidth		The buffer width
	/// \param[in]	uBufferHeight		The buffer height
	/// \param[in]	eBufferPixelFormat	The buffer pixel format, see #EPixelFormat for details
	/// \param[in]	uBufferRowPitch		The buffer row pitch i.e. the offset in pixels between
	///									the leftmost pixel of one row and the leftmost pixel of the 
	///									next one (can be != \a uBufferWidth); \a 0 or \a uBufferWidth 
	///									can be passed for consecutive rows without gaps;
	/// \param[out]	pauBuffer			The texture's buffer
	///
	/// \note 
	///		For compressed formats \a uBufferRowPitch must be always equal to 0 or \a uBufferWidth
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetToMemoryBuffer(
		UINT uBufferWidth, 
		UINT uBufferHeight,
		EPixelFormat eBufferPixelFormat,
		UINT uBufferRowPitch,
		CMcDataArray<BYTE> *pauBuffer) const = 0;
	//@}

	/// \name DirectX Textures
	//@{
	//==============================================================================
	// Method Name: GetDirectXTexture()
	//------------------------------------------------------------------------------
	/// Retrieves Direct 3D texture interface (for advanced users only).
	///
	/// \param[out]	ppDirect3DTexture9	Direct 3D texture interface 
	///									(the result should be cast to IDirect3DTexture9*)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetDirectXTexture(void **ppDirect3DTexture9) = 0;
	//@}

	/// \name Pixel Format
	//@{
	//==============================================================================
	// Method Name: GetPixelFormat(...)
	//------------------------------------------------------------------------------
	/// Retrieves the texture's pixel format as it is stored in a hardware.
	///
	/// \param[out] pePixelFormat		Texture Pixel Format
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetPixelFormat(EPixelFormat *pePixelFormat) const = 0;

	//==============================================================================
	// Method Name: GetSourcePixelFormat(...)
	//------------------------------------------------------------------------------
	/// Retrieves the texture's source pixel format.
	///
	/// Note: the effective pixel format can be obtained by calling IMcMemoryBufferTexture::GetPixelFormat()
	///
	/// \param[out] pePixelFormat		Texture Pixel Format
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetSourcePixelFormat(EPixelFormat *pePixelFormat) const = 0;
	//@}

	/// \name Colors Data
	//@{
	//==============================================================================
	// Method Name: GetColorData(...)
	//------------------------------------------------------------------------------
	/// Retrieves the texture's color data used to create or update it.
	///
	/// \param[out]	paColors					The array of colors
	/// \param[out]	pafColorPositions			The optional array of color positions relative to the texture's dimension (normalized 0 through 1); 
	///											first color's position > 0 or last color position < 1 mean continuing the appropriate color till the texture's edge (regardless of \a bColorInterpolation flag); 
	///											empty array means equidistrubuted colors; pass 'NULL' if unneeded
	/// \param[out]	pbColorInterpolation		Whether to interpolate colors between the positions (gradient fill) or continue each color till next color's postion (solid color strips);
	///											pass 'NULL' if unneeded
	/// \param[out]	pbColorColumns				whether to fill with color columns or color rows; pass 'NULL' if unneeded
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetColorData(
		CMcDataArray<SMcBColor> *paColors,
		CMcDataArray<float> *pafColorPositions,
		bool *pbColorInterpolation,
		bool *pbColorColumns) const = 0;
	//@}

	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a texture from memory buffer.
	/// 
	/// The texture can be used as a fill pattern of line or closed shape unless \a eUsage is #EU_STATIC_WRITE_ONLY_IN_ATLAS.
	///
	/// \param[out]	ppTexture					The pointer to the created texture
	/// \param[in]	uWidth						The texture's width
	/// \param[in]	uHeight						The texture's height
	/// \param[in]	ePixelFormat				The texture's pixel format, see #EPixelFormat for details (for textures with \a eUsage == #EU_STATIC_WRITE_ONLY_IN_ATLAS 
	///											the format applies only to \a pBuffer if specified, because the texture format will be the same as that of its atlas); 
	///											the default is #EPF_A8R8G8B8
	/// \param[in]	eUsage						The texture's usage, see #EUsage for details; the default is #EU_STATIC_WRITE_ONLY
	/// \param[in]	bAutoMipmap					Whether mipmaps (levels of detail) will be automatically generated for this texture; the default is `true`
	/// \param[in]	pBuffer						The texture's buffer of a size according to the rest of 
	///											the parameters; can be `NULL` to create uninitialized texture
	/// \param[in]	uBufferRowPitch				The texture's buffer row pitch i.e. the offset in pixels between the leftmost pixel of one row and the leftmost pixel of the
	///											next one; can be different from the width for uncompressed texture formats only; 0 (the default) means row pitch equal to the width
	/// \param[in]	strUniqueName				The texture's unique name used to refer to in material scripts; 
	///											`NULL` (the default) or empty string means to use a unique name generated automatically)
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcMemoryBufferTexture **ppTexture,
		UINT uWidth, 
		UINT uHeight,
		EPixelFormat ePixelFormat = IMcTexture::EPF_A8R8G8B8,
		EUsage eUsage = IMcTexture::EU_STATIC_WRITE_ONLY,
		bool bAutoMipmap = true,
		BYTE *pBuffer = NULL,
		UINT uBufferRowPitch = 0,
		PCSTR strUniqueName = NULL);


	//@}

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a texture filled by color rows or color columns.
	/// 
	/// The texture can be used as a fill pattern of line or closed shape unless \a eUsage is #EU_STATIC_WRITE_ONLY_IN_ATLAS.
	///
	/// \param[out]	ppTexture					The pointer to the created texture
	/// \param[in]	uWidth						The texture's width
	/// \param[in]	uHeight						The texture's height
	/// \param[in]	aColors[]					The array of colors
	/// \param[in]	uNumColors					The number of colors in the above array
	/// \param[in]	afColorPositions[]			The optional array of color positions relative to the texture's dimension (normalized 0 through 1); 
	///											first color's position > 0 or last color position < 1 mean continuing the appropriate color till the texture's edge (regardless of \a bColorInterpolation flag); 
	///											`NULL` / empty array (the default) means equidistrubuted colors
	/// \param[in]	uNumColorPositions			The number of color positions in the above array
	/// \param[in]	bColorInterpolation			Whether to interpolate colors between the positions (gradient fill) or continue each color till next color's postion (solid color strips);
	///											the default is `false' (solid color strips)
	/// \param[in]	bColorColumns				whether to fill with color columns or color rows; the default is `false' (rows)
	/// \param[in]	ePixelFormat				The texture's pixel format, see #EPixelFormat for details (irrelevant in case of \a eUsage == #EU_STATIC_WRITE_ONLY_IN_ATLAS); the default is #EPF_A8R8G8B8
	/// \param[in]	eUsage						The texture's usage, see #EUsage for details; the default is #EU_STATIC_WRITE_ONLY
	/// \param[in]	bAutoMipmap					Whether mipmaps (levels of detail) will be automatically generated for this texture; the default is `true`
	///
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcMemoryBufferTexture** ppTexture,
		UINT uWidth,
		UINT uHeight,
		const SMcBColor aColors[], UINT uNumColors,
		const float afColorPositions[] = NULL, UINT uNumColorPositions = 0,
		bool bColorInterpolation = false,
		bool bColorColumns = false,
		EPixelFormat ePixelFormat = IMcTexture::EPF_A8R8G8B8,
		EUsage eUsage = IMcTexture::EU_STATIC_WRITE_ONLY,
		bool bAutoMipmap = true);
	//@}
};

//==================================================================================
// Interface Name: IMcResourceTexture
//----------------------------------------------------------------------------------
/// The interface for texture loaded from icon/bitmap resource
//==================================================================================
class IMcResourceTexture : public virtual IMcTexture
{
protected:

	virtual ~IMcResourceTexture() {};

public:

    enum
    {
        //==============================================================================
        /// Texture unique ID for this interface
        //==============================================================================
        TEXTURE_TYPE = 2
    };

	//==============================================================================
	// Enum Name: EResourceType
	//------------------------------------------------------------------------------
	/// Resource Type
	//==============================================================================
	enum EResourceType
	{
		ERT_ICON,	///< Icon type
		ERT_BITMAP	///< Bitmap type
	};

    /// \name Resource
    //@{

	//==============================================================================
	// Method Name: SetResource(...)
	//------------------------------------------------------------------------------
	/// Sets the icon/bitmap resource's parameters.
	///
	/// \param[in]	nResourceID				The resource ID
	/// \param[in]	eResourceType			The resource type (icon or bitmap)
	/// \param[in]	strResourceFile			The file name of the resource file (dll or exe)
	///										(use NULL to use the calling application's exe)
	/// \param[in]	pTransparentColor		The color value to replace with transparent or NULL
	///										to disable color key
	/// \param[in]	aColorSubstitutions[]	The array of color substitution pairs
	/// \param[in]	uNumColorSubstitutions	The number of color substitution pairs in the above array
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetResource(
		int nResourceID, EResourceType eResourceType,
		PCSTR strResourceFile = NULL,
		const SMcBColor *pTransparentColor = NULL,
		const SColorSubstitution aColorSubstitutions[] = NULL,
		UINT uNumColorSubstitutions = 0) = 0;

	//==============================================================================
	// Method Name: GetResource(...)
	//------------------------------------------------------------------------------
	/// Retrieves the icon/bitmap resource's parameters.
	///
	/// \param[out]	pnResourceID		The resource ID
	/// \param[out]	peResourceType		The resource type (icon or bitmap)
	/// \param[out]	pstrResourceFile	The file name of the resource file (dll or exe)
	///									(NULL means the calling application's exe)
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetResource(
		int *pnResourceID,
		EResourceType *peResourceType, 
		PCSTR *pstrResourceFile) const = 0;

    //@}

    /// \name Create
    //@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a texture from icon/bitmap resource.
	///
	/// \param[out]	ppTexture					The pointer to the created texture
	/// \param[in]	nResourceID					The resource ID
	/// \param[in]	eResourceType				The resource type (icon or bitmap)
	/// \param[in]	strResourceFile				The file name of the resource file (dll or exe)
	///											(use NULL to use the calling application's exe)
	/// \param[in]  bFillPattern				Whether texture uses include a fill pattern 
	///											of line / closed shape or a projector texture
	/// \param[in]  bIgnoreTransparentMargin	Whether transparent margins of the texture 
	///											should be ignored
	/// \param[in]	pTransparentColor			The color value to replace with transparent or 
	///											NULL to disable color key
	/// \param[in]	aColorSubstitutions[]		The array of color substitution pairs
	/// \param[in]	uNumColorSubstitutions		The number of color substitution pairs in the above array
	///	\param[in]	bUseExisting				If true and some texture based on this file already 
	///											exists, it will be returned instead of creating a new one
	///	\param[out]	pbExistingUsed				Whether an existing texture based on this file 
	///											was used or a new one was created
	///
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcResourceTexture **ppTexture,
		int nResourceID,
		EResourceType eResourceType,
		PCSTR strResourceFile,
		bool bFillPattern,
		bool bIgnoreTransparentMargin = false,
		const SMcBColor *pTransparentColor = NULL,
		const SColorSubstitution aColorSubstitutions[] = NULL,
		UINT uNumColorSubstitutions = 0,
		bool bUseExisting = true, bool *pbExistingUsed = NULL);

	//@}
};

//==================================================================================
// Interface Name: IMcIconHandleTexture
//----------------------------------------------------------------------------------
/// The interface for texture created from icon handle
//==================================================================================
class IMcIconHandleTexture : public virtual IMcTexture
{
protected:

	virtual ~IMcIconHandleTexture() {};

public:

    enum
    {
        //==============================================================================
        /// Texture unique ID for this interface
        //==============================================================================
        TEXTURE_TYPE = 3
    };

    /// \name Icon
    //@{

	//==============================================================================
	// Method Name: SetIcon(...)
	//------------------------------------------------------------------------------
	/// Sets the icon.
	///
	/// \param[in]	hIcon					The icon handle (in Web: scalable HTMLImageElement e.g. with SVG source)
	/// \param[in]	pTransparentColor		The color value to replace with transparent or NULL	to disable color key
	/// \param[in]	aColorSubstitutions[]	The array of color substitution pairs
	/// \param[in]	uNumColorSubstitutions	The number of color substitution pairs in the above array
	/// \param[in]	bTakeOwnership			Whether the texture should take ownership of the 
	///										handle and destroy it when necessary (in this case 
	///										the caller should not destroy the handle) instead of 
	///										copying the icon
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetIcon(
		HICON hIcon,
		const SMcBColor *pTransparentColor = NULL,
		const SColorSubstitution aColorSubstitutions[] = NULL,
		UINT uNumColorSubstitutions = 0,
		bool bTakeOwnership = false) = 0;

	//==============================================================================
	// Method Name: GetIcon(...)
	//------------------------------------------------------------------------------
	/// Retrieves the icon.
	///
	/// \param[out]	phIcon	The icon handle (in Web: scalable HTMLImageElement e.g. with SVG source); 
	///						NULL will be returned if the icon handle is not owned by the texture
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetIcon(HICON *phIcon) const = 0;

    //@}

    /// \name Create
    //@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a texture from icon handle.
	///
	/// \param[out]	ppTexture					The pointer to the created texture
	/// \param[in]	hIcon						The icon handle (in Web: scalable HTMLImageElement e.g. with SVG source)
	/// \param[in]  bFillPattern				Whether texture uses include a fill pattern 
	///											of line / closed shape or a projector texture
	/// \param[in]  bIgnoreTransparentMargin	Whether transparent margins of the texture 
	///											should be ignored
	/// \param[in]	pTransparentColor			The color value to replace with transparent or 
	///											NULL to disable color key
	/// \param[in]	aColorSubstitutions[]		The array of color substitution pairs
	/// \param[in]	uNumColorSubstitutions		The number of color substitution pairs in the above array
	/// \param[in]	bTakeOwnership				Whether the texture should take ownership of the 
	///											handle and destroy it when necessary (in this case 
	///											the caller should not destroy the handle) 
	///											instead of copying the icon
	///	\param[in]	bUseExisting				If true and some texture based on this file already 
	///											exists, it will be returned instead of creating a new one
	///	\param[out]	pbExistingUsed				Whether an existing texture based on this file 
	///											was used or a new one was created
	///
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcIconHandleTexture** ppTexture,
		HICON hIcon,
		bool bFillPattern,
		bool bIgnoreTransparentMargin = false,
		const SMcBColor *pTransparentColor = NULL,
		const SColorSubstitution aColorSubstitutions[] = NULL,
		UINT uNumColorSubstitutions = 0,
		bool bTakeOwnership = false,
		bool bUseExisting = true, bool *pbExistingUsed = NULL);

	//@}
};

//==================================================================================
// Interface Name: IMcBitmapHandleTexture
//----------------------------------------------------------------------------------
/// The interface for texture created from bitmap handle
//==================================================================================
class IMcBitmapHandleTexture : public virtual IMcTexture
{
protected:

	virtual ~IMcBitmapHandleTexture() {};

public:

    enum
    {
        //==============================================================================
        /// Texture unique ID for this interface
        //==============================================================================
        TEXTURE_TYPE = 4
    };

    /// \name Icon
    //@{

	//==============================================================================
	// Method Name: SetBitmap(...)
	//------------------------------------------------------------------------------
	/// Sets the bitmap.
	///
	/// \param[in]	hBitmap					The bitmap handle (in Web: HTMLCanvasElement or constant-size HTMLImageElement)
	/// \param[in]	pTransparentColor		The color value to replace with transparent or NULL to disable color key
	/// \param[in]	aColorSubstitutions[]	The array of color substitution pairs
	/// \param[in]	uNumColorSubstitutions	The number of color substitution pairs in the above array
	/// \param[in]	bTakeOwnership			Whether the texture should take ownership of the 
	///										handle and destroy it when necessary (in this case 
	///										the caller should not destroy the handle) instead of 
	///										copying the icon
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetBitmap(
		HBITMAP hBitmap,
		const SMcBColor *pTransparentColor = NULL,
		const SColorSubstitution aColorSubstitutions[] = NULL,
		UINT uNumColorSubstitutions= 0,
		bool bTakeOwnership = false) = 0;

	//==============================================================================
	// Method Name: GetBitmap(...)
	//------------------------------------------------------------------------------
	/// Retrieves the bitmap.
	///
	/// \param[out]	phBitmap	The bitmap handle (in Web: HTMLCanvasElement or constant-size HTMLImageElement); 
	///							NULL will be returned if the icon handle is not owned by the texture
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetBitmap(HBITMAP *phBitmap) const = 0;

    //@}

    /// \name Create
    //@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a texture from bitmap handle.
	///
	/// \param[out]	ppTexture					The pointer to the created texture
	/// \param[in]	hBitmap						The bitmap handle (in Web: HTMLCanvasElement or constant-size HTMLImageElement)
	/// \param[in]  bFillPattern				Whether texture uses include a fill pattern 
	///											of line / closed shape or a projector texture
	/// \param[in]  bIgnoreTransparentMargin	Whether transparent margins of the texture 
	///											should be ignored
	/// \param[in]	pTransparentColor			The color value to replace with transparent or 
	///											NULL to disable color key
	/// \param[in]	aColorSubstitutions[]		The array of color substitution pairs
	/// \param[in]	uNumColorSubstitutions		The number of color substitution pairs in the above array
	/// \param[in]	bTakeOwnership				Whether the texture should take ownership of the 
	///											handle and destroy it when necessary (in this case 
	///											the caller should not destroy the handle) 
	///											instead of copying the bitmap
	///	\param[in]	bUseExisting				If true and some texture based on this file already 
	///											exists, it will be returned instead of creating a new one
	///	\param[out]	pbExistingUsed				Whether an existing texture based on this file 
	///											was used or a new one was created
	///
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcBitmapHandleTexture **ppTexture,
		HBITMAP hBitmap,
		bool bFillPattern,
		bool bIgnoreTransparentMargin = false,
		const SMcBColor *pTransparentColor = NULL,
		const SColorSubstitution aColorSubstitutions[] = NULL,
		UINT uNumColorSubstitutions = 0,
		bool bTakeOwnership = false,
		bool bUseExisting = true,
		bool *pbExistingUsed = NULL);

	//@}
};

//==================================================================================
// Interface Name: IMcVideoTexture
//----------------------------------------------------------------------------------
/// The base interface for textures playing video
//==================================================================================
class IMcVideoTexture : public virtual IMcTexture
{
protected:

	virtual ~IMcVideoTexture() {};
public:

	/// States of playing video
	enum EState
	{
		ES_STOPPED,		///< Calling ES_RUNNING from this state will re-play the video
		ES_RUNNING,		///< Start or continue playing the video
		ES_PAUSED		///< Calling ES_RUNNING from this state will continue the video 
						///<   from the point were it was paused
	};
	
	/// Method of updating the texture
	enum EUpdateMethod
	{
		EUM_RENDER,	///< during each render operation the current frame is copied from the video source if it is needed according to the desired 
					///< playback frame rate set by SetFrameRateForRenderBasedUpdate()
		EUM_FRAME,	///< when each frame is ready it is copied from the video source and 
					///< IFrameCallback::OnNewFrame() callback is called
		EUM_MANUAL	///< the user is responsible to call UpdateFrame() before render operation to 
					///< copy the current frame from the video source
	};

	/// metadata flags to be used as a bitfield
	enum EMetadataFlags
	{
		EMF_NONE				= 0x0000,	///< none
		EMF_CAMERA_PROJECTION	= 0x0001,	///< camera projection
		EMF_TELEMETRIES			= 0x0002,	///< array of telemetries
		EMF_TELEMETRY_QUALITY	= 0x0004,	///< telemetry quality
		EMF_TIMESTAMP			= 0x0008,	///< timestamp
		EMF_FRAME_INDEX			= 0x0010,	///< frame index
		EMF_USER_DATA_BYTES		= 0x0020,	///< user data bytes
		EMF_ALL					= 0xFFFF	///< any available data
	};

	/// Meta data of the video
	struct SMetaData
	{
		UINT									uValidParamsBitField;	///< valid parameters (see IMcVideoTexture::EMetadataFlags)
		SMcCameraProjectionParams				CameraParams;			///< camera projection parameters
		CMcDataArray<SMcPositionAndOrientation> aTelemetries;			///< array of telemetries
		float									fTelemetryQuality;		///< Telemetry quality (between 0 and 1);
																		///<   relevant if telemetries are improved by streaming application algorithm
		UINT64									uTimestampInMS;			///< timestamp in ms
		UINT									uFrameIndex;			///< frame index
		CMcDataArray<BYTE>						aUserDataBytes;			///< user data bytes
	};

	/// Data callback interface to be inherited by the user
	class ICallback
#ifdef __EMSCRIPTEN__
		: public IMcEmscriptenCallbackRef
#endif
	{
	public:
		virtual ~ICallback() {}

		//==============================================================================
		// Method Name: OnNewFrame()
		//------------------------------------------------------------------------------
		/// Called after each new frame has been received from the video source.
		///
		/// Called in the context of either render thread or video source's thread (see AddCallback() for details). 
		/// Time consuming operation or thread-unsafe code should not be performed inside the callback.
		///
		/// \param[in] bWithMetadata	Whether or not a metadata has been received together with the frame.
		//==============================================================================
		virtual void OnNewFrame(bool bWithMetadata = false) = 0;

		//==============================================================================
		// Method Name: OnNewFrame()
		//------------------------------------------------------------------------------
		/// Called after each new metadata (without frame) has been received from the video source.
		///
		/// Called in the context of either render thread or video source's thread (see AddCallback() for details). 
		/// Time consuming operation or thread-unsafe code should not be performed inside the callback.
		//==============================================================================
		virtual void OnNewMetadataWithoutFrame() = 0;

		//==============================================================================
		// Method Name: Release()
		//------------------------------------------------------------------------------
		/// A callback that should release callback class instance.
		///
		///	Can be implemented by the user to optionally delete callback class instance when 
		/// IMcTexture instance is been removed.
		//==============================================================================
		virtual void Release() {}
	};

	/// \name State
	//@{

	//==============================================================================
	// Method Name: SetState(...)
	//------------------------------------------------------------------------------
	/// Updates the state of playing video
	///
	/// \param[in] eState			The texture state. See #EState for details.
	///
	///	\note
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetState(EState eState) = 0;

	//==============================================================================
	// Method Name: GetState(...)
	//------------------------------------------------------------------------------
	/// Retrieves the state of playing video
	///
	/// \param[out] peState			The texture state. See #EState for details.
	///									
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetState(EState *peState) const = 0;

	//@}

	/// \name Metadata and Callback
	//@{

	//==============================================================================
	// Method Name: GetAvailableMetadataParams()
	//------------------------------------------------------------------------------
	/// Retrieves metadata parameters that can be retrieved
	///
	/// \param[out]	puAvailableMetadataBitField		metadata parameters that can be retrieved 
	///												(see #EMetadataFlags)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAvailableMetadataParams(UINT *puAvailableMetadataBitField) = 0;

	//==============================================================================
	// Method Name: GetMetadata()
	//------------------------------------------------------------------------------
	/// Retrieves video metadata
	///
	/// \param[out]	pMetadata				Video metadata; use SMetaData::uValidParamsBitField to check the actual 
	///										parameters retrieved (a subset of \a uReqiredParamsBitField)
	/// \param[in]	uReqiredParamsBitField	Required parameters (see #EMetadataFlags); 
	///										the default is #EMF_ALL - all the data that can be retrieved
	/// \return
	///     - status result
	//==============================================================================
 	virtual IMcErrors::ECode GetMetadata(SMetaData *pMetadata, UINT uReqiredParamsBitField = EMF_ALL) = 0;

	//==============================================================================
	// Method Name: AddCallback()
	//------------------------------------------------------------------------------
	/// Adds a callback.
	/// 
	/// \param[in]	pCallback			A callback interface
	/// \param[in]	bInRenderThread		whether to call the callback during rendering in context of render thread or 
	///									in context of video source thread after new frame has been received; 
	///									the default is false (video source thread)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode AddCallback(ICallback *pCallback, bool bInRenderThread = false) = 0;

	//==============================================================================
	// Method Name: RemoveCallback()
	//------------------------------------------------------------------------------
	/// Removes a callback added by AddCallback().
	/// 
	/// \param[in]	pCallback		A callback interface
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode RemoveCallback(ICallback *pCallback) = 0;

	//@}

	/// \name Frame Update
	//@{

	//==============================================================================
	// Method Name: SetFrameRateForRenderBasedUpdate()
	//------------------------------------------------------------------------------
	/// Sets the desired playback frame rate (relevant only for IMcDirectShowTexture with #EUM_RENDER update method and for IMcHtmlVideoTexture).
	///
	/// \param[in]	fFramesPerSecond	the desired playback frame rate in frames per second (during each render operation the current frame will be 
	///									updated from the video source only if (1 / \p fFramesPerSecond) seconds passed since the last frame update); 
	///									the default is 0 (means updating the frame in each render operation)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetFrameRateForRenderBasedUpdate(float fFramesPerSecond) = 0;

	//==============================================================================
	// Method Name: GetFrameRateForRenderBasedUpdate()
	//------------------------------------------------------------------------------
	/// Retrieves the desired playback frame rate set by SetFrameRateForRenderBasedUpdate().
	///
	/// \param[out]	pfFramesPerSecond	the desired playback frame rate in frames per second (during each render operation the current frame is 
	///									updated from the video source only if (1 / \p fFramesPerSecond) seconds passed since the last frame update); 
	///									the default is 0 (means updating the frame in each render operation)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetFrameRateForRenderBasedUpdate(float *pfFramesPerSecond) const = 0;

	//==============================================================================
	// Method Name: SetManualUpdateMethod()
	//------------------------------------------------------------------------------
	/// Sets update method (either #EUM_MANUAL or a combination of #EUM_FRAME and #EUM_RENDER)
	///
	/// Cannot be used in DirectShow textures.
	///
	/// \param[in]	bManual		true for #EUM_MANUAL, false for a combination of #EUM_FRAME and #EUM_RENDER;
	///							the default is false
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetManualUpdateMethod(bool bManual) = 0;

	//==============================================================================
	// Method Name: GetManualUpdateMethod()
	//------------------------------------------------------------------------------
	/// Retrieves update method (either #EUM_MANUAL or a combination of #EUM_FRAME and #EUM_RENDER)
	///
	/// Cannot be used in DirectShow textures.
	///
	/// \param[out]	pbManual	true for #EUM_MANUAL, false for a combination of #EUM_FRAME and #EUM_RENDER;
	///							the default is false
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetManualUpdateMethod(bool *pbManual) const = 0;

	//==============================================================================
	// Method Name: UpdateFrame()
	//------------------------------------------------------------------------------
	/// Copies the current frame from the video source.
	///
	/// Should be called before render operation (applicable only in manual update method (#EUM_MANUAL)).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode UpdateFrame() = 0;

	//@}

	/// \name Memory Buffer
	//@{ 

	//==============================================================================
	// Method Name: GetToMemoryBuffer()
	//------------------------------------------------------------------------------
	/// Copies texture into the specified memory buffer.
	/// 
	/// Works only in textures created with \a bReadable flag. To be called from render thread only.
	/// 
	/// \param[in]	uBufferWidth		The buffer width
	/// \param[in]	uBufferHeight		The buffer height
	/// \param[in]	eBufferPixelFormat	The buffer pixel format, see #EPixelFormat for details
	/// \param[in]	uBufferRowPitch		The buffer row pitch i.e. the offset in pixels between
	///									the leftmost pixel of one row and the leftmost pixel of the 
	///									next one (can be != \a uBufferWidth); \a 0 or \a uBufferWidth 
	///									can be passed for consecutive rows without gaps.
	/// \param[out]	pauBuffer			The texture's buffer
	///
	/// \note 
	///		For compressed formats \a uBufferRowPitch must be always equal to 0 or \a uBufferWidth
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetToMemoryBuffer(
		UINT uBufferWidth, 
		UINT uBufferHeight,
		EPixelFormat eBufferPixelFormat,
		UINT uBufferRowPitch,
		CMcDataArray<BYTE> *pauBuffer) const = 0;

	//==============================================================================
	// Method Name: GetCurrFrameBuffer()
	//------------------------------------------------------------------------------
	/// Retrieves the current frame buffer.
	/// 
	/// Works only in textures created with \a bReadable flag. Can be called from either render thread or video source's thread.
	/// 
	/// \param[out]	ppaBuffer			The current frame buffer; the buffer returned is temporary and should be used (or copied) before calling to 
	///									any other API functions.
	/// \param[out]	puBufferWidth		The buffer width
	/// \param[out]	puBufferHeight		The buffer height
	/// \param[out]	peBufferPixelFormat	The buffer pixel format, see #EPixelFormat for details
	/// \param[out]	puBufferRowPitch	The buffer row pitch i.e. the offset in pixels between
	///									the leftmost pixel of one row and the leftmost pixel of the 
	///									next one (can be != \a *puBufferWidth); \a equal to *puBufferWidth 
	///									means consecutive rows without gaps
	/// \return
	///     - Status result
	//==============================================================================

	virtual IMcErrors::ECode GetCurrFrameBuffer(
		const BYTE **ppaBuffer,
		UINT *puBufferWidth, 
		UINT *puBufferHeight,
		EPixelFormat *peBufferPixelFormat,
		UINT *puBufferRowPitch) const = 0;

	//@}
};

//==================================================================================
// Interface Name: IMcDirectShowTexture
//----------------------------------------------------------------------------------
/// The base interface for textures playing video with Microsoft DirectShow
//==================================================================================
class IMcDirectShowTexture : public virtual IMcVideoTexture
{
};

//==================================================================================
// Interface Name: IMcDirectShowGraphTexture
//----------------------------------------------------------------------------------
/// The interface for texture playing video from Microsoft DirectShow filter graph
//==================================================================================
class IMcDirectShowGraphTexture : public virtual IMcDirectShowTexture
{
protected:

	virtual ~IMcDirectShowGraphTexture() {};

public:

	enum
	{
		//==============================================================================
		/// Texture unique ID for this interface
		//==============================================================================
		TEXTURE_TYPE = 7
	};

	//@}

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a texture from Microsoft DirectShow filter graph file.
	///
	/// \param[out]	ppTexture			The pointer to the created texture
	/// \param[in]	strGraphFileName	The file containing filter graph
	/// \param[in]	eUpdateMethod		The method of updating the texture (see EUpdateMethod)
	/// \param[in]	bReadable			Whether using GetToMemoryBuffer() and GetCurrFrameBuffer() should be enabled
	///									the default is false (more efficient)
	/// \param[in] ePixelFormat			The pixel format
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(IMcDirectShowGraphTexture **ppTexture,
		PCWSTR strGraphFileName, EUpdateMethod eUpdateMethod, bool bReadable = false, IMcTexture::EPixelFormat ePixelFormat = EPF_X8R8G8B8);

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a texture from Microsoft DirectShow filter graph interface.
	///
	/// \param[out]	ppTexture			The pointer to the created texture
	/// \param[in] pFilterGraph			The filter graph interface (should be cast from IFilterGraph)
	/// \param[in] eUpdateMethod		The method of updating the texture (see EUpdateMethod)
	/// \param[in]	bReadable			Whether using GetToMemoryBuffer() and GetCurrFrameBuffer() should be enabled
	///									the default is false (more efficient)
	/// \param[in] ePixelFormat			The pixel format
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(IMcDirectShowGraphTexture **ppTexture,
		void *pFilterGraph, EUpdateMethod eUpdateMethod, bool bReadable = false, IMcTexture::EPixelFormat ePixelFormat = EPF_X8R8G8B8);
	//@}
};

//==================================================================================
// Interface Name: IMcDirectShowSourceFileTexture
//----------------------------------------------------------------------------------
/// The interface for texture playing video from video file
//==================================================================================
class IMcDirectShowSourceFileTexture : public virtual IMcDirectShowTexture
{
protected:

	virtual ~IMcDirectShowSourceFileTexture() {};

public:

	enum
	{
		//==============================================================================
		/// Texture unique ID for this interface
		//==============================================================================
		TEXTURE_TYPE = 8
	};

	//@}

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Create
	///
	/// Creates a texture from video file.
	///
	/// \param[out]	ppTexture			The pointer to the created texture
	/// \param[in]	strSourceFileName	The file to play
	/// \param[in]	bPlayInLoop			Whether to play in loop
	/// \param[in]	eUpdateMethod		The method of updating the texture (see EUpdateMethod)
	/// \param[in]	bReadable			Whether using GetToMemoryBuffer() and GetCurrFrameBuffer() should be enabled;
	///									the default is false (more efficient)
	/// \param[in] ePixelFormat			The pixel format
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(IMcDirectShowSourceFileTexture **ppTexture,
		PCWSTR strSourceFileName, bool bPlayInLoop, EUpdateMethod eUpdateMethod, 
		bool bReadable = false, IMcTexture::EPixelFormat ePixelFormat = EPF_X8R8G8B8);
	//@}
};

//==================================================================================
// Interface Name: IMcDirectShowUSBCameraTexture
//----------------------------------------------------------------------------------
/// The interface for texture playing video from USB camera
//==================================================================================
class IMcDirectShowUSBCameraTexture : public virtual IMcDirectShowTexture
{
protected:

	virtual ~IMcDirectShowUSBCameraTexture() {};

public:

	enum
	{
		//==============================================================================
		/// Texture unique ID for this interface
		//==============================================================================
		TEXTURE_TYPE = 9
	};

	//@}

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a texture according to USB device index.
	///
	/// \param[out]	ppTexture			The pointer to the created texture
	/// \param[in]	uDeviceIndex		The USB device index
	/// \param[in]	eUpdateMethod		The method of updating the texture (see EUpdateMethod)
	/// \param[in]	bReadable			Whether using GetToMemoryBuffer() and GetCurrFrameBuffer() should be enabled
	///									the default is false (more efficient)
	/// \param[in] ePixelFormat			The pixel format
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(IMcDirectShowUSBCameraTexture **ppTexture,
		UINT uDeviceIndex, EUpdateMethod eUpdateMethod, bool bReadable = false, IMcTexture::EPixelFormat ePixelFormat = EPF_X8R8G8B8);

	//==============================================================================
	// Method Name: SetDeviceIndex(...)
	//------------------------------------------------------------------------------
	/// Updates the USB device index used in Create.
	///
	/// \param[in] uDeviceIndex		The USB device index
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetDeviceIndex(UINT uDeviceIndex) = 0;

	//==============================================================================
	// Method Name: GetDeviceIndex()
	//------------------------------------------------------------------------------
	/// Retrieves USB device index used in Create().
	///
	/// \param[out]	puDeviceIndex	The USB device index
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetDeviceIndex(UINT *puDeviceIndex) const = 0;

	//==============================================================================
	// Method Name: GetUSBDevices()
	//------------------------------------------------------------------------------
	/// Retrieves names of video devices connected to USB ports in the order of their indices;
	///
	/// \param[out]	pUSBDevicesNames	The array of devices names
	///
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode GetUSBDevices(CMcDataArray<CMcString> *pUSBDevicesNames);
};

//==================================================================================
// Interface Name: IMcFFMpegVideoTexture
//----------------------------------------------------------------------------------
/// The interface for texture playing video by FFMpeg AVLIB engine
//==================================================================================
class IMcFFMpegVideoTexture : public virtual IMcVideoTexture
{
protected:
	virtual ~IMcFFMpegVideoTexture() {};

public:
	enum 
	{
		//==============================================================================
		/// Texture unique ID for this interface
		//==============================================================================
		TEXTURE_TYPE = 11
	};

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates a texture for playing video by FFMpeg AVLIB engine.
	///
	/// \param[out]	ppTexture		The pointer to the newly created texture
	/// \param[in]	strSourceName   The data source name. It may be the friendly device name in DirectShow format,
	///								file path/URL in case of a movie or TRP/RTSP URL in case of streamed data.
	/// \param[in]	bPlayInLoop		Whether to play in loop (for file only)
	/// \param[in]	bReadable		Whether using GetToMemoryBuffer() and GetCurrFrameBuffer() should be enabled;
	///								the default is false (more efficient)
	/// \param[in]	strSourceFormat	The optional format of the data source; can be NULL in case of movie file/stream name 
	///								with appropriate extension (.avi, etc.), must be "dshow" in case of camera device 
	///								(such as frame grabber or USB camera)
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(IMcFFMpegVideoTexture **ppTexture, PCSTR strSourceName,
		bool bPlayInLoop = false, bool bReadable = false, PCSTR strSourceFormat = NULL);

	//==============================================================================
	// Method Name: GetSourceName()
	//------------------------------------------------------------------------------
	/// Returns the data source name used to create the texture
	///
	/// \param[in]	pstrSourceName	The data source name. It may be the friendly device name in DirectShow format,
	///								file path/URL in case of a movie or TRP/RTSP URL in case of streamed data.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSourceName(PCSTR *pstrSourceName) = 0;

	//==============================================================================
	// Method Name: GetSourceFormat()
	//------------------------------------------------------------------------------
	/// Returns the format  used to create the texture
	///
	/// \param[in]	pstrFormatName	The optional format of the data source; can be NULL in case of movie file/stream name 
	///								with appropriate extension (.avi, etc.), must be "dshow" in case of camera device 
	///								(such as frame grabber or USB camera)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSourceFormat(PCSTR *pstrFormatName) = 0;
};

//==================================================================================
// Interface Name: IMcHtmlVideoTexture
//----------------------------------------------------------------------------------
/// The interface for texture playing video via the browser's support for HtmlVideoElement (Web version only).
///
/// \note 
///		- For platforms other than Web: can be created (with URL string video source only) and attached to an object scheme item without playing capability
///		- Saving an object scheme or an object using this texture is supported only in case of URL string video source only
//==================================================================================
class IMcHtmlVideoTexture : public virtual IMcVideoTexture
{
protected:
	virtual ~IMcHtmlVideoTexture() {};

public:
	enum 
	{
		//==============================================================================
		/// Texture unique ID for this interface
		//==============================================================================
		TEXTURE_TYPE = 15
	};

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates a texture for playing video via the browser's support for HtmlVideoElement
	///
	/// \param[out]	ppTexture		The pointer to the newly created texture
	/// \param[in]	VideoSource		The video source supported by HtmlVideoElement through either `src` property (URL string) or (JavaScript API only)
	///								`srcObject` property (MediaStream or if supported by the browser: MediaSource, Blob, or File)
	/// \param[in]	bPlayInLoop		Whether to play in loop (for file only)
	/// \param[in]	bReadable		Whether using GetToMemoryBuffer() and GetCurrFrameBuffer() should be enabled;
	///								the default is false (more efficient)
	/// \param[in]	bWithSound		Whether the sound should be played; the default is false
	///
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(IMcHtmlVideoTexture **ppTexture, HTML_VIDEO_SOURCE VideoSource,
		bool bPlayInLoop = false, bool bReadable = false, bool bWithSound = false);

	//==============================================================================
	// Method Name: GetVideoSource()
	//------------------------------------------------------------------------------
	/// Returns the video source used to create the texture.
	///
	/// \param[out]	pVideoSource	The video source set in Create().
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVideoSource(HTML_VIDEO_SOURCE *pVideoSource) = 0;
};

//==================================================================================
// Interface Name: IMcSharedMemoryVideoTexture
//----------------------------------------------------------------------------------
/// The interface for texture playing video from IMcSharedMemoryVideoTextureStreamer
//==================================================================================
class IMcSharedMemoryVideoTexture : public virtual IMcVideoTexture
{
protected:
	virtual ~IMcSharedMemoryVideoTexture() {};

public:
	enum 
	{
		//==============================================================================
		/// Texture unique ID for this interface
		//==============================================================================
		TEXTURE_TYPE = 12
	};

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates a texture for playing video from IMcSharedMemoryVideoTextureStreamer
	///
	/// \param[out]	ppTexture			The pointer to the newly created texture
	/// \param[in]	strSharedMemoryName	The name of shared memory to use
	/// \param[in]	ePixelFormat		The pixel format of the texture to convert to;
	///									#EPF_COUNT (the default) means preserving the source format
	///
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(IMcSharedMemoryVideoTexture **ppTexture, PCSTR strSharedMemoryName, 
		IMcTexture::EPixelFormat ePixelFormat = EPF_COUNT);

	//==============================================================================
	// Method Name: GetSharedMemoryName()
	//------------------------------------------------------------------------------
	/// Returns the name of shared memory used
	///
	/// \param[in]	pstrSharedMemoryName	The name of shared memory
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSharedMemoryName(PCSTR *pstrSharedMemoryName) = 0;
};

//==================================================================================
// Interface Name: IMcMemoryBufferVideoTexture
//----------------------------------------------------------------------------------
/// The interface for video texture updated from memory buffer in context of additional thread
//==================================================================================
class IMcMemoryBufferVideoTexture : public virtual IMcVideoTexture
{
protected:

	virtual ~IMcMemoryBufferVideoTexture() {};

public:

	enum
	{
		//==============================================================================
		/// Texture unique ID for this interface
		//==============================================================================
		TEXTURE_TYPE = 13
	};

	/// \name Memory Buffer
	//@{
	//==============================================================================
	// Method Name: UpdateFromMemoryBuffer(...)
	//------------------------------------------------------------------------------
	/// Update the texture according to the specified memory buffer.
	/// 
	/// \param[in]	uBufferWidth		The buffer width
	/// \param[in]	uBufferHeight		The buffer height
	/// \param[in]	eBufferPixelFormat	The buffer pixel format, see #EPixelFormat for details
	/// \param[in]	uBufferRowPitch		The buffer row pitch i.e. the offset in pixels between
	///									the leftmost pixel of one row and the leftmost pixel of the 
	///									next one (can be != \a uBufferWidth); \a 0 or \a uBufferWidth 
	///									can be passed for consecutive rows without gaps;
	/// \param[in]	pBuffer				The texture's buffer
	///
	/// \note 
	///		For compressed formats \a uBufferRowPitch must be always equal to 0 or \a uBufferWidth
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode UpdateFromMemoryBuffer(
		UINT uBufferWidth, 
		UINT uBufferHeight,
		EPixelFormat eBufferPixelFormat,
		UINT uBufferRowPitch,
		BYTE *pBuffer) = 0;
	//@}

	/// \name Pixel Format
	//@{
	//==============================================================================
	// Method Name: GetPixelFormat(...)
	//------------------------------------------------------------------------------
	/// Retrieves the texture's pixel format as it is stored in a hardware.
	///
	/// \param[out] pePixelFormat		Texture Pixel Format
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetPixelFormat(EPixelFormat *pePixelFormat) const = 0;

	//==============================================================================
	// Method Name: GetSourcePixelFormat(...)
	//------------------------------------------------------------------------------
	/// Retrieves the texture's source pixel format.
	///
	/// Note: the effective pixel format can be obtained by calling IMcMemoryBufferTexture::GetPixelFormat()
	///
	/// \param[out] pePixelFormat		Texture Pixel Format
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetSourcePixelFormat(EPixelFormat *pePixelFormat) const = 0;
	//@}

	/// \name Create
	//@{
	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a video texture from memory buffer.
	/// 
	/// \param[out]	ppTexture					The pointer to the created texture
	/// \param[in]	uWidth						The texture's width
	/// \param[in]	uHeight						The texture's height
	/// \param[in]	ePixelFormat				The texture's pixel format, see #EPixelFormat for details
	///											(for textures with \a eUsage == #EU_STATIC_WRITE_ONLY_IN_ATLAS 
	///											the format applies only to \a pBuffer if specified, because 
	///											the texture format will be the same as that of its atlas)
	/// \param[in]	eUsage						The texture's usage (one of listed in #EUsage except #EU_STATIC_WRITE_ONLY_IN_ATLAS)
	///
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcMemoryBufferVideoTexture **ppTexture,
		UINT uWidth, 
		UINT uHeight,
		EPixelFormat ePixelFormat = IMcTexture::EPF_A8R8G8B8,
		EUsage eUsage = IMcTexture::EU_DYNAMIC_WRITE_ONLY_DISCARDABLE);
};

//==================================================================================
// Interface Name: IMcTextureArray
//----------------------------------------------------------------------------------
/// The interface for an array of textures (to be used in a picture item shown as multiple instances)
//==================================================================================
class IMcTextureArray : public virtual IMcTexture
{
protected:

	virtual ~IMcTextureArray() {};

public:

    enum
    {
        //==============================================================================
        /// Texture unique ID for this interface
        //==============================================================================
        TEXTURE_TYPE = 14
    };

	/// \name Array of Textures
    //@{

	//==============================================================================
	// Method Name: GetTextures(...)
	//------------------------------------------------------------------------------
	/// Retrieves the array of textures.
	///
	/// \param[out]	papTextures					The array of textures
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetTextures(CMcDataArray<IMcTexture*, true> *papTextures) const = 0;

    //@}

    /// \name Create
    //@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a texture array.
	///
	/// \param[out]	ppTextureArray				The pointer to the created texture array
	/// \param[in]	apTextures[]				The array of textures
	/// \param[in]  uNumTextures				The number of textures in the above array
	///
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcTextureArray **ppTextureArray, 
		IMcTexture* const apTextures[], UINT uNumTextures);

    //@}

};
