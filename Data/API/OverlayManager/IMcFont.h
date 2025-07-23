#pragma once
//==================================================================================
/// \file IMcFont.h
/// The interface for font resource
//==================================================================================

#include "IMcErrors.h"
#include "IMcBase.h"
#include "CMcDataArray.h"
#include "McCommonTypes.h"

class IMcLogFont;
class IMcFileFont;
class IMcImage;

//==================================================================================
// Interface Name: IMcFont
//----------------------------------------------------------------------------------
/// The base interface for all font resources
///
//==================================================================================
class IMcFont : public virtual IMcBase
{
protected:

	virtual ~IMcFont() {};

public:

	/// Meaning of SSpecialChar::fCharHeightParam and non-zero SSpecialChar::fCharWidthParam.
	///
	/// \note
	/// Zero SSpecialChar::fCharWidthParam always means character aspect ratio is equal to that of image, so that:
	/// - character width = character height * (image width / image height)
	enum ESpecialCharSizeMeaning
	{
		/// **fCharHeightParam** is a factor of font height, **fCharWidthParam** divided by **fCharHeightParam** is a factor of 
		/// image aspect ratio, so that:
		/// - character height = font height * **fCharHeightParam**
		/// - character width = character height * (**fCharWidthParam** / **fCharHeightParam**) * (image width / image height)
		ESCSM_FACTOR_OF_FONT_HEIGHT,

		/// **fCharHeightParam** and **fCharWidthParam** are factors of image height and width respectively, so that:
		/// - character height = image height * **fCharHeightParam**
		/// - character width  = image width  * **fCharWidthParam**
		ESCSM_FACTORS_OF_IMAGE_SIZE,

		/// **fCharHeightParam** and **fCharWidthParam** are character's absolute height and width respectively (in pixels)
		ESCSM_ABSOLUTE_SIZE
	};
	/// characters' range
	struct SCharactersRange
	{
		USHORT nFrom;	///< range start as 16-bit Unicode character code
		USHORT nTo;		///< range end as 16-bit Unicode character code

		bool operator==(const SCharactersRange &Other) const
		{
			return (nFrom == Other.nFrom && nTo == Other.nTo);
		}
		bool operator!=(const SCharactersRange &Other) const
		{
			return (nFrom != Other.nFrom || nTo != Other.nTo);
		}
	};

	/// Special character as user-defined image
	struct SSpecialChar
	{
		IMcImage	*pImage;			///< character's image
		float		fCharWidthParam;	///< parameter used to calculate character's width  (the meaning depends on **eSizeParamMeaning**)
		float		fCharHeightParam;	///< parameter used to calculate character's height (the meaning depends on **eSizeParamMeaning**)
		int			nVerticalOffset;	///< character's vertical offset in pixels from text's baseline
		int			nLeftSpacing;		///< left spacing from the previous character
		int			nRightSpacing;		///< right spacing from the next character
		USHORT		uCharCode;			///< character's code (7-bit ASCII or 16-bit Unicode) to represent this character in text
										///< (should be equal neither to zero nor to any regular character code used in texts)
										///< Replacing numbers is not allowed (48-57 in ascii)
		ESpecialCharSizeMeaning
					eSizeParamMeaning;	///< The meaning of **fCharWidthParam** and **fCharHeightParam**; 
										///< note that special character shouldn't be much larger than font's regular characters, 
										///< otherwise font atlas creation may fail
		/// constructor
		SSpecialChar() { memset((void*)this, 0, sizeof(*this)); }

		bool operator==(const SSpecialChar &Other) const
		{
			return (pImage == Other.pImage && fCharWidthParam == Other.fCharWidthParam && fCharHeightParam == Other.fCharHeightParam &&
				nVerticalOffset == Other.nVerticalOffset && nLeftSpacing == Other.nLeftSpacing && nRightSpacing == Other.nRightSpacing &&
				uCharCode == Other.uCharCode);
		}
		bool operator!=(const SSpecialChar &Other) const
		{
			return (pImage != Other.pImage || fCharWidthParam != Other.fCharWidthParam || fCharHeightParam != Other.fCharHeightParam ||
				nVerticalOffset != Other.nVerticalOffset || nLeftSpacing != Other.nLeftSpacing || nRightSpacing != Other.nRightSpacing ||
				uCharCode != Other.uCharCode);
		}
	};

	/// \name Font Type And Casting
	//@{

	//==============================================================================
	// Method Name: GetFontType()
	//------------------------------------------------------------------------------
	/// Returns the font type unique id.
	///
	/// \remark
	///		Use the cast methods in order to get the correct type.
	//==============================================================================
	virtual UINT GetFontType() const = 0;

	//==============================================================================
	// Method Name: CastToLogFont(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcFont* To #IMcLogFont*
	/// 
	/// \return
	///     - #IMcLogFont*
	//==============================================================================
	virtual IMcLogFont* CastToLogFont() = 0;

	//==============================================================================
	// Method Name: CastToFileFont(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcFont* To #IMcFileFont*
	/// 
	/// \return
	///     - #IMcFileFont*
	//==============================================================================
	virtual IMcFileFont* CastToFileFont() = 0;

	//@}

	/// \name Creation Parameters
	//@{

	//==============================================================================
	// Method Name: GetIsStaticFont(...)
	//------------------------------------------------------------------------------
	/// Retrieves whether or not font is static.
	///
	/// \param[out] pbStaticFont	Whether the font is static or dynamic
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetIsStaticFont(
		bool *pbStaticFont) const = 0;

	//==============================================================================
	// Method Name: GetCharactersRanges(...)
	//------------------------------------------------------------------------------
	/// Retrieves the characters ranges.
	///
	/// \param[out] paCharactersRanges	The characters ranges
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCharactersRanges(
		CMcDataArray<SCharactersRange> *paCharactersRanges) const = 0;

	//==============================================================================
	// Method Name: GetMaxNumCharsInDynamicAtlas(...)
	//------------------------------------------------------------------------------
	/// Retrieves maximum number of characters in dynamic font atlas.
	///
	/// \param[out] puMaxNumCharsInDynamicAtlas		The maximum number of characters in dynamic font atlas 
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetMaxNumCharsInDynamicAtlas(
		UINT *puMaxNumCharsInDynamicAtlas) const = 0;

	//==============================================================================
	// Method Name: GetTextOutlineWidth(...)
	//------------------------------------------------------------------------------
	/// Retrieves text outline width in pixels.
	///
	/// \param[out] puTextOutlineWidth		The text outline width in pixels
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTextOutlineWidth(
		UINT *puTextOutlineWidth) const = 0;

	//==============================================================================
	// Method Name: GetSpecialChars()
	//------------------------------------------------------------------------------
	/// Retrieves special characters (user-defined images)
	///
	/// \param[out]	paSpecialChars				The array of special characters (user-defined images).
	/// \param[out]	pbUseSpecialCharsColors		Whether special characters' pixel colors are used (ignoring text item's text 
	///											color) or their alpha values only are used (with text item's text color).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSpecialChars(CMcDataArray<SSpecialChar> *paSpecialChars, bool* pbUseSpecialCharsColors) const = 0;

	//==============================================================================
	// Method Name: IsCreatedWithUseExisting(...)
	//------------------------------------------------------------------------------
	/// Retrieves whether the font was created with use-existing flag.
	///
	/// \param[out] pbCreatedWithUseExisting	True if \a bUseExisting parameter of 
	///											Create() function was true and the font's 
	///											parameters were not changed after creation.
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode IsCreatedWithUseExisting(bool *pbCreatedWithUseExisting) const = 0;

	//@}

	/// \name Character Spacing and Antialiasing Color Levels
	//@{
	//==============================================================================
	// Method Name: SetCharacterSpacing()
	//------------------------------------------------------------------------------
	/// Sets additional spacing between characters for fonts created after this function 
	/// has been called.
	///
	/// \param[in]	uSpacing	Spacing between characters in font's pixels (should be between 0 and 5); 
	///							the default is 1
	///
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode SetCharacterSpacing(UINT uSpacing);

	//==============================================================================
	// Method Name: GetCharacterSpacing()
	//------------------------------------------------------------------------------
	/// Retrieves additional spacing between characters set by SetCharacterSpacing().
	///
	/// \param[out]	puSpacing	Spacing between characters in font's pixels
	///
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode GetCharacterSpacing(UINT *puSpacing);

	//==============================================================================
	// Method Name: GetEffectiveCharacterSpacing()
	//------------------------------------------------------------------------------
	/// Retrieves effective additional spacing between characters.
	///
	/// \param[out]	puSpacing	Spacing between characters in font's pixels
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetEffectiveCharacterSpacing(UINT *puSpacing) const = 0;

	//==============================================================================
	// Method Name: SetNumAntialiasingAlphaLevels()
	//------------------------------------------------------------------------------
	/// Sets number of transparency levels used for antialiasing atlases for fonts created after 
	/// this function has been called.
	///
	/// \param[in]	uNumAlphaLevels		Number of transparency levels used for antialiasing 
	///									(should be between 2 and 256); the default is 256
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode SetNumAntialiasingAlphaLevels(UINT uNumAlphaLevels);

	//==============================================================================
	// Method Name: GetNumAntialiasingAlphaLevels()
	//------------------------------------------------------------------------------
	/// Retrieves number of transparency levels used for antialiasing atlases set by 
	/// SetNumAntialiasingAlphaLevels()
	///
	/// \param[out]	puNumAlphaLevels	Number of transparency levels used for antialiasing 
	///									(between 2 and 256)
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode GetNumAntialiasingAlphaLevels(UINT *puNumAlphaLevels);

	//==============================================================================
	// Method Name: GetEffectiveNumAntialiasingAlphaLevels()
	//------------------------------------------------------------------------------
	/// Retrieves effective number of transparency levels used for antialiasing atlases
	///
	/// \param[out]	puNumAlphaLevels	Number of transparency levels used for antialiasing 
	///									(between 2 and 256)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetEffectiveNumAntialiasingAlphaLevels(UINT *puNumAlphaLevels) const = 0;

	//@}
};

//==================================================================================
// Interface Name: IMcLogFont
//----------------------------------------------------------------------------------
/// The interface for font created from logfont
//==================================================================================
class IMcLogFont : public virtual IMcFont
{
protected:

	virtual ~IMcLogFont() {};

public:

	enum
	{
		//==============================================================================
		/// Font unique ID for this interface
		//==============================================================================
		FONT_TYPE = 1
	};

	/// log font with respective TTF file
	struct SLogFontToTtfFile
	{
		SMcVariantLogFont LogFont;				///< log font parameters (only lfWeight, lfItalic, lfFaceName are used)
		char strTtfFileFullPathName[MAX_PATH];	///< full path to TTF file to use instead of log font

		SLogFontToTtfFile() { strTtfFileFullPathName[0] = '\0'; }
	};

	/// \name LogFont
	//@{

	//==============================================================================
	// Method Name: SetLogFont(...)
	//------------------------------------------------------------------------------
	/// Sets the logfont.
	///
	/// \param[in]	LogFont		The logfont
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetLogFont(const SMcVariantLogFont &LogFont) = 0;

	//==============================================================================
	// Method Name: GetLogFont(...)
	//------------------------------------------------------------------------------
	/// Retrieves the logfont.
	///
	/// \param[out]	pLogFont	The logfont
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetLogFont(SMcVariantLogFont *pLogFont) const = 0;

	//==============================================================================
	// Method Name: SetLogFontToTtfFileMap()
	//------------------------------------------------------------------------------
	/// Sets optional mapping of log fonts to TTF files.
	///
	/// If log font with the appropriate parameters (only lfWeight, lfItalic, lfFaceName are used) is defined, 
	/// the respective TTF file will be used instead.
	///
	/// \param[in]	aLogFonts		Array of log fonts with respective TTF files
	/// \param[in]	uNumLogFonts	number of elements in the above array
	///
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode SetLogFontToTtfFileMap(const SLogFontToTtfFile aLogFonts[], UINT uNumLogFonts);

	//==============================================================================
	// Method Name: GetLogFontToTtfFileMap()
	//------------------------------------------------------------------------------
	/// Receives optional mapping of log fonts to TTF files set by SetLogFontToTtfFileMap().
	///
	/// \param[out]	paLogFonts		Array of log fonts with respective TTF files
	///
	/// \return
	///     - status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode GetLogFontToTtfFileMap(CMcDataArray<SLogFontToTtfFile> *paLogFonts);

	//@}	

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a font resource from LOGFONT.
	///
	/// \param[out]	ppFont						The pointer to the created font
	/// \param[in]	LogFont						The LOGFONT
	/// \param[in]	bStaticFont					Whether to create a static or a dynamic font
	/// \param[in]	aCharactersRanges			The array of characters ranges, to be used in font.
	///											If is NULL, create all characters.
	///											else, create only specified characters.
	/// \param[in]  uNumCharactersRanges		The number of characters ranges
	/// \param[in]	uMaxNumCharsInDynamicAtlas	The max number of characters in a single dynamic texture.
	///                                         Must be greater than size of longest string,
	///											or 0, for default texture size.
	///	\param[in]	bUseExisting				If true and some font based on this logfont exists, 
	///											it will be returned instead of creating a new one
	///	\param[out]	pbExistingUsed				Whether an existing font based on this LOGFONT 
	///											was used or a new one was created
	/// \param[in]  uTextOutlineWidth			The text outline width in pixels
	/// \param[in]  aSpecialChars[]				The array of special characters as user-defined images
	/// \param[in]  uNumSpecialChars			The number of characters in the above array
	/// \param[in]  bUseSpecialCharsColors		Whether to use special characters' pixel colors (ignoring text item's text 
	///											color) or use their alpha values only (with text item's text color); 
	///											the default and the recommended value is `false` (alpha only); 
	///											pixel colors can affect performance and memory usage of font atlas
	///	
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcLogFont **ppFont,
		const SMcVariantLogFont &LogFont,
		bool bStaticFont = false,
		const SCharactersRange aCharactersRanges[] = NULL,
		UINT uNumCharactersRanges = 0,
		UINT uMaxNumCharsInDynamicAtlas = 0,
		bool bUseExisting = true,
		bool *pbExistingUsed = NULL,
		UINT uTextOutlineWidth = 1,
		const SSpecialChar aSpecialChars[] = NULL,
		UINT uNumSpecialChars = 0,
		bool bUseSpecialCharsColors = false);

	//@}
};

//==================================================================================
// Interface Name: IMcFileFont
//----------------------------------------------------------------------------------
/// The interface for font created from SMcFileSource
//==================================================================================
class IMcFileFont : public virtual IMcFont
{
protected:

	virtual ~IMcFileFont() {};

public:

	enum
	{
		//==============================================================================
		/// Font unique ID for this interface
		//==============================================================================
		FONT_TYPE = 2
	};

	/// \name FileFont
	//@{

	//==============================================================================
	// Method Name: SetFontFileAndHeight(...)
	//------------------------------------------------------------------------------
	/// Sets the Font File & Height.
	///
	/// \param[in]	FontFile			Either file-system file name or in-memory file buffer
	/// \param[in]  nFontHeight			The font's height.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetFontFileAndHeight(const SMcFileSource &FontFile, int nFontHeight) = 0;

	//==============================================================================
	// Method Name: GetFontFileAndHeight(...)
	//------------------------------------------------------------------------------
	/// Retrieves the Font File & Height.
	///
	/// \param[out]	pFontFile			Either file-system file name or in-memory file buffer
	/// \param[out] pnFontHeight			The font's height.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetFontFileAndHeight(SMcFileSource *pFontFile, int *pnFontHeight) const = 0;

	//@}	

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a font resource from SMcFileSource.
	///
	/// \param[out]	ppFont						The pointer to the created font
	/// \param[in]	FontFile					Either file-system file name or in-memory file buffer
	/// \param[in]  nFontHeight					The font's height.
	/// \param[in]	bStaticFont					Whether to create a static or a dynamic font
	/// \param[in]	aCharactersRanges			The array of characters ranges, to be used in font.
	///											If is NULL, create all characters.
	///											else, create only specified characters.
	/// \param[in]  uNumCharactersRanges		The number of characters ranges
	/// \param[in]	uMaxNumCharsInDynamicAtlas	The max number of characters in a single dynamic texture.
	///                                         Must be greater than size of longest string,
	///											or 0, for default texture size.
	///	\param[in]	bUseExisting				If true and some font based on this FontFile & FontHeight exists, 
	///											it will be returned instead of creating a new one
	///	\param[out]	pbExistingUsed				Whether an existing font based on this FontFile & FontHeight
	///											was used or a new one was created
	/// \param[in]  uTextOutlineWidth			The text outline width in pixels
	/// \param[in]  aSpecialChars[]				The array of special characters as user-defined images
	/// \param[in]  uNumSpecialChars			The number of characters in the above array
	/// \param[in]  bUseSpecialCharsColors		Whether to use special characters' pixel colors (ignoring text item's text 
	///											color) or use their alpha values only (with text item's text color); 
	///											the default and the recommended value is `false` (alpha only); 
	///											pixel colors can affect performance and memory usage of font atlas
	///	
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcFileFont **ppFont,
		const SMcFileSource &FontFile,
		int nFontHeight,
		bool bStaticFont = false,
		const SCharactersRange aCharactersRanges[] = NULL,
		UINT uNumCharactersRanges = 0,
		UINT uMaxNumCharsInDynamicAtlas = 0,
		bool bUseExisting = true,
		bool *pbExistingUsed = NULL,
		UINT uTextOutlineWidth = 1,
		const SSpecialChar aSpecialChars[] = NULL,
		UINT uNumSpecialChars = 0,
		bool bUseSpecialCharsColors = false);

	//@}
};
