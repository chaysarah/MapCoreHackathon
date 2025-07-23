#pragma once

//===========================================================================
/// \file IMcPrintMap.h
/// Interfaces for print map
//===========================================================================

#include "IMcErrors.h" 
#include "McCommonTypes.h"
#include "CMcDataArray.h"

class IMcObjectScheme;

//===========================================================================
// Interface Name: IMcPrintMap
//---------------------------------------------------------------------------
/// Base interface for printing map viewport contents
//===========================================================================
class IMcPrintMap
{
protected:
	virtual ~IMcPrintMap() {}

public:

	/// Page settings structure
	struct SPageSettings
	{
		SPageSettings() :
			fOverlappingSize(0), fLeftMargin(0), fRightMargin(0), fTopMargin(0), fBottomMargin(0),
			pScreenAnnotationScheme(NULL), pWorldAnnotationScheme(NULL), uPageNumberTextPropertyID(MC_EMPTY_ID) {}

		/// Size of overlapping area between neighbor pages (in paper meters for each page)
		float fOverlappingSize;

		/// Left margin in addition to printer's margin (in paper meters)
		float fLeftMargin;

		/// Right margin in addition to printer's margin (in paper meters)
		float fRightMargin; 

		/// Top margin in addition to printer's margin (in paper meters)
		float fTopMargin;

		/// Bottom margin in addition to printer's margin (in paper meters)
		float fBottomMargin;

		/// For PrintScreen() only: 
		/// object scheme defining optional annotation object that will be printed in the 
		/// margins or over the map; can be NULL if no annotation object should be printed. 
		///
		/// Should be based on 3 locations in #EPCS_SCREEN coordinate system. MapCore will print 
		/// an object based on this scheme with 4 points per location. The points are the corners 
		/// of the following screen rectangle (in the order: top-left, top-right, bottom-right, bottom-left): 
		/// - first location: the page's printed area with overlapping area plus above-defined margins;
		/// - second location: the page's printed area with overlapping area;
		/// - third location: the page's printed area without overlapping area 
		IMcObjectScheme *pScreenAnnotationScheme;

		/// For PrintRect2D() only: 
		/// object scheme defining optional annotation object that will be printed in the 
		/// margins or over the map; can be NULL if no annotation object should be printed. 
		///
		/// Should be based on 3 locations in #EPCS_WORLD coordinate system. MapCore will print 
		/// an object based on this scheme with 4 points per location. The points are the corners 
		/// of the following world rectangle (in the order: top-left, top-right, bottom-right, bottom-left): 
		/// - first location: the page's printed area with overlapping area plus above-defined margins;
		/// - second location: the page's printed area with overlapping area;
		/// - third location: the page's printed area without overlapping area 
		IMcObjectScheme *pWorldAnnotationScheme;

		/// Optional page number property ID for private property of \p pAnnotationScheme.
		/// Can be #MC_EMPTY_ID if page numbers should not be printed.
		UINT uPageNumberTextPropertyID;
	};

	/// Type of dialog box to be presented on print
	enum EPrintDlgType
	{
		EPDT_NONE,					///< no dialog box presented, default printer settings used
		EPDT_PRINT_SETUP,			///< print setup dialog Box
		EPDT_PRINT					///< print dialog box
	};

	/// Output of CalcPrintScreenPages() function
	/// 
	/// \note printed areas of pages are screen rectangles (x and y only are used)
	struct SPrintScreenPagesCalc
	{
		float						fPrintScale;		///< scale of the map when printed on the paper
		SMcSize						PagePixelSize;		///< page size in printer pixels
		CMcDataArray<SMcBox>		aPagesScreenRects1;	///< printed areas of pages without overlapping
		CMcDataArray<SMcBox>		aPagesScreenRects2;	///< printed areas of pages with overlapping
		CMcDataArray<SMcBox>		aPagesScreenRects3;	///< printed areas of pages with overlapping plus margins
	};

	/// Output of CalcPrintRectPages2D() function
	/// 
	/// \note printed areas of pages are world rectangles (each one defined by 4 points: 
	///		  top-left, top-right, bottom-right, bottom-left)
	struct SPrintRectPagesCalc2D
	{
		UINT						uNumPagesX;			///< number of pages along X axis
		UINT						uNumPagesY;			///< number of pages along Y axis
		SMcSize						PagePixelSize;		///< page size in printer pixels
		CMcDataArray<SMcVector3D[4]>aPagesWorldRects1;	///< printed areas of pages without overlapping	
		CMcDataArray<SMcVector3D[4]>aPagesWorldRects2;	///< printed areas of pages with overlapping  
		CMcDataArray<SMcVector3D[4]>aPagesWorldRects3;	///< printed areas of pages with overlapping plus margins  
	};

	/// Print callback interface
	class IPrintCallback
#ifdef __EMSCRIPTEN__
		: public IMcEmscriptenCallbackRef
#endif
	{
	public:
		virtual ~IPrintCallback() {}

		//=================================================================================================
		// Function name: OnPrintTileReceived(...)
		//-------------------------------------------------------------------------------------------------
		/// Called when PrintScreen() / PrintRect2D() (with \p bPrintToMemory flag set) has generated each printed tile.
		///
		/// \param[in] hTileBitmap		The GDI handle of the bitmap containing the tile
		/// \param[in] uPageNumber		The page number to which the tile belongs
		/// \param[in] nTileStartX		The tile's X location in pixels 
		/// \param[in] nTileStartY		The tile's Y location in pixels 
		//=================================================================================================
		virtual void OnPrintTileReceived(HBITMAP hTileBitmap, UINT uPageNumber, int nTileStartX,
			UINT nTileStartY) {}

		//=================================================================================================
		// Function name: OnPrintFinished(...)
		//-------------------------------------------------------------------------------------------------
		/// Called when asynchronous operation of PrintScreen() / PrintRect2D() / PrintScreenToRawRasterData() / PrintRect2DToRawRasterData() or 
		/// synchronous operation of PrintScreen() / PrintRect2D() (with \p bPrintToMemory flag set) has been completed or canceled.
		/// 
		/// Not called when PrintScreen() / PrintRect2D() / PrintScreenToRawRasterData() / PrintRect2DToRawRasterData() fails with an error.
		///
		/// \param[in]	eStatus							The operation's status: IMcErrors::SUCCESS, IMcErrors::ASYNC_OPERATION_CANCELED (if the operation 
		///												was canceled be calling CancelAsyncPrint() or IMcMapViewport::Release()) or other error code.
		/// \param[in]	strFileNameOrRasterDataFormat	The name of the file of raw raster data saved or raw raster data format as a file extension 
		///												(e.g. "tif" and "pdf"); relevant for PrintScreenToRawRasterData() / PrintRect2DToRawRasterData() only.
		/// \param[in]	auRasterFileMemoryBuffer		The memory buffer of the file with raw raster data result; 
		///												relevant for PrintScreenToRawRasterData() / PrintRect2DToRawRasterData() only with 
		///												\p pauFileMemoryBuffer parameter other than `NULL`.
		/// \param[in]	auWorldFileMemoryBuffer			The memory buffer of the file with the raster's world data (*.wld); 
		///												relevant for PrintScreenToRawRasterData() / PrintRect2DToRawRasterData() only with 
		///												both \p pauFileMemoryBuffer and \p pauWorldFileMemoryBuffer parameters other than `NULL`.
		//=================================================================================================
		virtual void OnPrintFinished(IMcErrors::ECode eStatus, PCSTR strFileNameOrRasterDataFormat, 
			const CMcDataArray<BYTE> &auRasterFileMemoryBuffer, const CMcDataArray<BYTE> &auWorldFileMemoryBuffer) {}
	};

	/// Printer settings structure
	struct SPrinterSettings
	{
		/// Handle to a global memory object that contains a DEVMODEW structure (with UNICODE names)
		HGLOBAL hPrinterDeviceMode;

		/// Handle to a global memory object that contains a DEVNAMES structure (with UNICODE names)
		HGLOBAL hPrinterDeviceNames;
	};

#ifdef _WIN32

	/// \name Print Settings
	//@{
	
	//==============================================================================
	// Method Name: SetPrintedPageSettings()
	//------------------------------------------------------------------------------
	/// Sets printed page settings.
	///
	/// \param[in]	Settings		Printed page settings.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetPrintedPageSettings(const SPageSettings &Settings) = 0;

	//==============================================================================
	// Method Name: GetPrintedPageSettings()
	//------------------------------------------------------------------------------
	/// Retrieves current printed page settings.
	///
	/// \param[out]	pSettings		Printed page settings.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetPrintedPageSettings(SPageSettings *pSettings) = 0;

	//==============================================================================
	// Method Name: SetPrinterSettings()
	//------------------------------------------------------------------------------
	/// Sets printer settings received from a print dialog displayed by the user.
	///
	/// \param[in]	Settings		Printer settings with handles pointing to UNICODE structures.
	/// \param[in]	bTakeOwnership
	///								- True if the viewport should take ownership of the printer 
	///								  settings handles and destroy them when necessary;
	///								  in this case the user should not destroy the handles.
	///								- False if the viewport should store a copy of the printer 
	///								  settings handles; in this case the user is still 
	///								  responsible to destroy the handles.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetPrinterSettings(const SPrinterSettings &Settings, 
												bool bTakeOwnership) = 0;

	//==============================================================================
	// Method Name: GetPrinterSettings()
	//------------------------------------------------------------------------------
	/// Retrieves current printer settings set by the last called print function.
	///
	/// \param[out]	pSettings			Printer settings with handles pointing to UNICODE structures.
	/// \param[in]	bViewportOwnership
	///									- True if the viewport should return its own printer 
	///									  settings handles that will be destroyed when necessary;
	///									  in this case the user should not destroy the handles and 
	///									  can use them until the next call to one of print functions.
	///									- False if the viewport should return a copy of the printer 
	///									  settings handles; in this case the user is responsible 
	///									  to destroy the handles and can use them theretofore.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetPrinterSettings(SPrinterSettings *pSettings, 
												bool bViewportOwnership) = 0;
	//@}

#endif // _WIN32

	/// \name Print Screen
	//@{

#ifdef _WIN32

	//==============================================================================
	// Method Name: CalcPrintScreenPages()
	//------------------------------------------------------------------------------
	/// Calculates the data of pages to be printed by PrintScreen().
	///
	/// \param[in]	ePrintDlgBox		which print dialog box to open.
	/// \param[in]	uNumPagesX			number of pages along X axis.
	/// \param[in]	uNumPagesY			number of pages along Y axis.
	/// \param[out]	pPrintPages			printed pages data calculated.
	///
	/// \note
	///		Set ePrintDlgBox to EPDT_NONE in order to use the default printer settings.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode CalcPrintScreenPages(EPrintDlgType ePrintDlgBox, 
		UINT uNumPagesX, UINT uNumPagesY, SPrintScreenPagesCalc *pPrintPages) = 0;

	//==============================================================================
	// Method Name: PrintScreen()
	//------------------------------------------------------------------------------
	/// Prints the map's rectangle displayed in the window on a specified number of pages
	///
	/// \param[in]	ePrintDlgBox	specify which print dialog box to open.
	/// \param[in]	uNumPagesX		number of pages along X axis.
	/// \param[in]	uNumPagesY		number of pages along Y axis.
	/// \param[in]  bPrintToMemory	whether or not to print to memory by sending IPrintCallback::OnPrintTileReceived() callbacks
	/// \param[in]	bPrintAsync		whether the printing should be asynchronous.
	/// \param[in]	pCallback		optional callback to get tile's bitmap (IPrintCallback::OnPrintTileReceived()) and/or handle an asynchronously printing; 
	///								the result will be returned in IPrintCallback::OnPrintFinished()
	/// \param[in]	nPageToPrint	number of page (1, 2, ...) to print (0 for all pages)
	///
	/// \note
	///		Set ePrintDlgBox to EPDT_NONE in order to use the default print dialog.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode PrintScreen(EPrintDlgType ePrintDlgBox, 
		UINT uNumPagesX, UINT uNumPagesY, bool bPrintToMemory = false, 
		bool bPrintAsync = false, IPrintCallback *pCallback = NULL, UINT nPageToPrint = 0) = 0;
#endif

	//==============================================================================
	// Method Name: PrintScreenToRawRasterData()
	//------------------------------------------------------------------------------
	/// Prints the map's rectangle displayed in the window as raw raster data file.
	///
	/// \param[in]	fResolutionFactor				resolution factor to use for printing: 
	///												- 1 (the default) means the current screen resolution,
	///												- values greater than 1 mean lower (coarser) resolutions,
	///												- values smaller than 1 mean higher (finer) resolutions.
	/// \param[in]	strFileNameOrRasterDataFormat	name of the file of raw raster data to save to (if \p auFileMemoryBuffer is `NULL`) or 
	///												raw raster data format as a file extension e.g. "tif" and "pdf" (if \p auFileMemoryBuffer is not `NULL`).
	/// \param[out]	pauRasterFileMemoryBuffer		memory buffer of the file to save the raw raster data to or `NULL` (the default) to save to 
	///												file system file specified by \p strFileNameOrRasterDataFormat.
	/// \param[out]	pauWorldFileMemoryBuffer		memory buffer of the file to save the raster's the world data (*.wld) to or `NULL` (the default); 
	///												relevant only if \p pauRasterFileMemoryBuffer is not `NULL`.
	/// \param[in]  pCallback						the optional asynchronous callback if the printing should be asynchronous; 
	///												the result will be returned in IPrintCallback::OnPrintFinished().
	/// \param[in]  astrGdalOptions					the optional options for raster creation supported by GDAL library as an array of `key=value` strings
	///												(e.g. "TILED=YES" is recommended for TIFF and PDF); 
	///												see https://gdal.org/drivers/raster/index.html for the creation options valid for each raster format; 
	/// \param[in]  uNumGdalOptions					the number of the above options.
	///
	/// \note
	/// If the viewport is created in geographic coordinate system with IMcMapViewport::SCreateData::bShowGeoInMetricProportion equal to `true` then the area 
	/// will be printed in metric proportion as the viewport's visualization and the raster's world data (geo-referencing) will not be saved as non-relevant. 
	/// The world data will not be saved in case of 3D and image-calc viewport as well.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode PrintScreenToRawRasterData(float fResolutionFactor, PCSTR strFileNameOrRasterDataFormat, 
		CMcDataArray<BYTE> *pauRasterFileMemoryBuffer = NULL, CMcDataArray<BYTE> *pauWorldFileMemoryBuffer = NULL, IPrintCallback *pCallback = NULL, 
		const PCSTR astrGdalOptions[] = NULL, UINT uNumGdalOptions = 0) = 0;

	//@}

	/// \name Print World Rectangle (2D map only)
	//@{

#ifdef _WIN32

	//==============================================================================
	// Method Name: CalcPrintRectPages2D()
	//------------------------------------------------------------------------------
	/// Calculates the data of pages to be printed by PrintRect2D().
	///
	/// \param[in]	ePrintDlgBox			which print dialog box to open.
	/// \param[in]	PrintWorldRectCenter	center of world rectangle to print.
	/// \param[in]	PrintWorldRectSize		width and height of world rectangle to print.
	/// \param[in]	fPrintWorldRectAngle	rotation angle of world rectangle to print.
	/// \param[in]	fPrintScale				required scale of the map when printed on the paper
	/// \param[out]	pPrintPages				printed pages data calculated.
	///
	/// \note
	///		Set ePrintDlgBox to EPDT_NONE in order to use the default printer settings.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode CalcPrintRectPages2D(EPrintDlgType ePrintDlgBox, 
		const SMcVector2D &PrintWorldRectCenter, const SMcVector2D &PrintWorldRectSize, 
		float fPrintWorldRectAngle, float fPrintScale, SPrintRectPagesCalc2D *pPrintPages) = 0;

	//==============================================================================
	// Method Name: PrintRect2D()
	//------------------------------------------------------------------------------
	/// Prints the map's world rectangle (as a 2D viewport) in a specified scale on several pages.
	///
	/// \param[in]	ePrintDlgBox			specify which print dialog box to open.
	/// \param[in]	PrintWorldRectCenter	center of world rectangle to print.
	/// \param[in]	PrintWorldRectSize		width and height of world rectangle to print.
	/// \param[in]	fPrintWorldRectAngle	rotation angle of world rectangle to print.
	/// \param[in]	fPrintScale				required scale of the map when printed on the paper
	/// \param[in]  bPrintToMemory			Whether the print is to memory.
	/// \param[in]	bPrintAsync				whether the printing should be asynchronous.
	/// \param[in]	pCallback				optional callback to handle tile's bitmap and/or handle an asynchronously printing, 
	///										the result will be returned in IPrintCallback::OnPrintFinished()
	/// \param[in]	nPageToPrint			number of page (1, 2, ...) to print (0 for all pages)
	///
	/// \note
	///		Set ePrintDlgBox to EPDT_NONE in order to use the default printer settings.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode PrintRect2D(EPrintDlgType ePrintDlgBox, 
		const SMcVector2D &PrintWorldRectCenter, const SMcVector2D &PrintWorldRectSize, 
		float fPrintWorldRectAngle, float fPrintScale, bool bPrintToMemory = false, 
		bool bPrintAsync = false, IPrintCallback *pCallback = NULL, UINT nPageToPrint = 0) = 0;
#endif

	//==============================================================================
	// Method Name: PrintRect2DToRawRasterData()
	//------------------------------------------------------------------------------
	/// Prints the map's world rectangle (as a 2D viewport) in a specified scale as raw raster data file.
	///
	/// \param[in]	PrintWorldRectCenter			center of world rectangle to print.
	/// \param[in]	PrintWorldRectSize				width and height of world rectangle to print.
	/// \param[in]	fPrintWorldRectAngle			rotation angle of world rectangle to print.
	/// \param[in]	fCameraScale					required camera scale of the map.
	/// \param[in]	fResolutionFactor				resolution factor to use for printing: 
	///												- 1 (the default) means the current screen resolution,
	///												- values greater than 1 mean lower (coarser) resolutions,
	///												- values smaller than 1 mean higher (finer) resolutions.
	/// \param[in]	strFileNameOrRasterDataFormat	name of the file of raw raster data to save to (if \p auFileMemoryBuffer is `NULL`) or 
	///												raw raster data format as a file extension e.g. "tif" and "pdf" (if \p auFileMemoryBuffer is not `NULL`).
	/// \param[out]	pauRasterFileMemoryBuffer		memory buffer of the file to save the raw raster data to or `NULL` (the default) to save to 
	///												file system file specified by \p strFileNameOrRasterDataFormat.
	/// \param[out]	pauWorldFileMemoryBuffer		memory buffer of the file to save the raster's world data (*.wld) to or `NULL` (the default); 
	///												relevant only if \p pauRasterFileMemoryBuffer is not `NULL`.
	/// \param[in]  pCallback						the optional asynchronous callback if the printing should be asynchronous; 
	///												the result will be returned in IPrintCallback::OnPrintFinished().
	/// \param[in]  astrGdalOptions					the optional options for raster creation supported by GDAL library as an array of `key=value` strings
	///												(e.g. "TILED=YES" is recommended for TIFF and PDF); 
	///												see https://gdal.org/drivers/raster/index.html for the creation options valid for each raster format; 
	/// \param[in]  uNumGdalOptions					the number of the above options.
	/// \param[in]	bPrintGeoInMetricProportion		relevant only when the viewport is created in geographic coordinate system with 
	///												IMcMapViewport::SCreateData::bShowGeoInMetricProportion equal to `true`; if so and 
	///												\p bPrintGeoInMetricProportion is also `true` (the default) then the area will be printed in metric proportion 
	///												as the viewport's visualization and the raster's world data (geo-referencing) will not be saved as non-relevant.
	///												otherwise the area will be printed as is (in geographic coordinate system: in degrees) with valid world data.
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode PrintRect2DToRawRasterData(const SMcVector2D &PrintWorldRectCenter, const SMcVector2D &PrintWorldRectSize,
		float fPrintWorldRectAngle, float fCameraScale, float fResolutionFactor, PCSTR strFileNameOrRasterDataFormat,
		CMcDataArray<BYTE> *pauRasterFileMemoryBuffer = NULL, CMcDataArray<BYTE> *pauWorldFileMemoryBuffer = NULL, IPrintCallback *pCallback = NULL, 
		const PCSTR astrGdalOptions[] = NULL, UINT uNumGdalOptions = 0,
		bool bPrintGeoInMetricProportion = true) = 0;

	//@}

	/// \name Canceling asynchronous print
	//@{

	//==============================================================================
	// Method Name: CancelAsyncPrint()
	//------------------------------------------------------------------------------
	/// Cancels asynchronous print identified by \p pCallback previously specified in 
	/// PrintScreen() / PrintRect2D() / PrintScreenToRawRasterData() / PrintRect2DToRawRasterData().
	///
	/// \param[in]  pCallback	asynchronous callback.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode CancelAsyncPrint(IPrintCallback *pCallback) = 0;

	//@}
};
