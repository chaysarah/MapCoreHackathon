 
/****************************************************************************
*                                                                           *
* McBasicTypes.h -- Basic Windows Type Definitions                                *
*                                                                           *
*                                                                            *
****************************************************************************/
//#ifndef _WIN32
#ifndef _MC_BASIC_TYPES_
#define _MC_BASIC_TYPES_
#pragma once

#include <limits.h>

#ifndef _WIN32
	#include "stdlib.h"
	#include "wchar.h"

	typedef int                 INT;

	typedef unsigned int        UINT;
	typedef unsigned int        *PUINT;

	#ifndef NULL
	#ifdef __cplusplus
	#define NULL    0
	#else
	#define NULL    ((void *)0)
	#endif
	#endif

	#ifndef VOID
	#define VOID void
	#endif

	#ifndef CONST
	#define CONST const
	#endif

	typedef char CHAR;
 	typedef const char* LPCSTR;
	typedef CONST CHAR *LPCSTR, *PCSTR;
	typedef LPCSTR PCTSTR, LPCTSTR, PCUTSTR, LPCUTSTR;
	typedef CHAR *NPSTR, *LPSTR, *PSTR;
	typedef wchar_t WCHAR;    // wc,   16-bit UNICODE character
	typedef WCHAR *NWPSTR, *LPWSTR, *PWSTR;
	typedef CONST WCHAR *LPCWSTR, *PCWSTR;
#endif

#ifdef __EMSCRIPTEN__
	#include <emscripten/val.h>
	#include "IMcEmscriptenInterfaceRef.h"
	#include "IMcEmscriptenCallbackRef.h"

	#define EMSCRIPTEN_VAL_MEMBER_INITIALIZER(Member)												\
		: Member(emscripten::val::null())
	#define EMSCRIPTEN_ADD_VAL_MEMBER_TO_INITIALIZERS(Member)										\
		, Member(emscripten::val::null())
	#define NULL_HANDLE								emscripten::val::null()
 	#define IS_HANDLE_NULL(Handle)					Handle.isNull()
 	#define ARE_HANDLES_EQUAL(Handle1, Handle2)		Handle1.strictlyEquals(Handle2)
	#define EMSCRIPTEN_ONLY(Code)					Code
	#define JAVA_SCRIPT_API
 	typedef emscripten::val HTML_VIDEO_SOURCE;
#else
	#define EMSCRIPTEN_VAL_MEMBER_INITIALIZER(Member)
	#define EMSCRIPTEN_ADD_VAL_MEMBER_TO_INITIALIZERS(Member)
	#define NULL_HANDLE								NULL
	#define IS_HANDLE_NULL(Handle)					(Handle == NULL)
	#define ARE_HANDLES_EQUAL(Handle1, Handle2)		(Handle1 == Handle2)
	#define EMSCRIPTEN_ONLY(Code)
 	typedef const char* HTML_VIDEO_SOURCE;
#endif

//#define JAVA_SCRIPT_API_IN_WRAPPER should always by undefined (used for documentation only)

#ifdef _WIN32
#include <Windows.h>
template <typename TOtherType, typename TType>
inline TOtherType explicit_cast(TType This) { return static_cast<TOtherType>(This); }

#define ENUM_DECL_START(EnumName, TypeName) enum EnumName : TypeName
#define ENUM_DECL_END(EnumName, TypeName)
#else
#include "stdlib.h"
#include "wchar.h"
inline wchar_t *wcsdup(const wchar_t* ptr) 
{
	wchar_t* dup;      
	dup = (wchar_t*)malloc((wcslen(ptr) + 1) * sizeof(wchar_t));     
	if (dup == NULL) {         
		//		   __set_errno(ENOMEM);         
		return NULL;     
	}     
	wcscpy(dup, ptr);     
	return dup; 
}
template <typename TEnum, typename TType>
struct STypedEnumWrapper
{
	inline STypedEnumWrapper() : Value(TEnum()) {}
	inline STypedEnumWrapper(TEnum e) : Value(static_cast<TType>(e))  {}
	inline STypedEnumWrapper(TType t) : Value(t)  {}

	inline operator TEnum() const { return static_cast<TEnum>(Value); }
	//	operator TType() const { return Value; }

	TType Value;
}; 

template <typename TOtherType, typename TType>
inline TOtherType explicit_cast(TType This) { return static_cast<TOtherType>(This.Value); }

#define ENUM_DECL_START(EnumName, TypeName) enum EnumName##_Real
#define ENUM_DECL_END(EnumName, TypeName) typedef STypedEnumWrapper<EnumName##_Real, TypeName> EnumName;
#endif

#ifndef _WINDEF_
#ifndef NO_STRICT
#ifndef STRICT 
#define STRICT 1
#endif
#endif 

#include "wchar.h"
#include <stdlib.h>

#ifndef _WIN32

#include <stdint.h>
#include <sys/stat.h>

#ifndef __max
#define __max(a,b)            (((a) > (b)) ? (a) : (b))
#endif

#ifndef __min
#define __min(a,b)            (((a) < (b)) ? (a) : (b))
#endif

#define ANSI_CHARSET            0
#define DEFAULT_CHARSET         1
#define SYMBOL_CHARSET          2
#define SHIFTJIS_CHARSET        128
#define HANGEUL_CHARSET         129
#define HANGUL_CHARSET          129
#define GB2312_CHARSET          134
#define CHINESEBIG5_CHARSET     136
#define OEM_CHARSET             255
#define JOHAB_CHARSET           130
#define HEBREW_CHARSET          177
#define ARABIC_CHARSET          178
#define GREEK_CHARSET           161
#define TURKISH_CHARSET         162
#define VIETNAMESE_CHARSET      163
#define THAI_CHARSET            222
#define EASTEUROPE_CHARSET      238
#define RUSSIAN_CHARSET         204

#define MAC_CHARSET             77
#define BALTIC_CHARSET          186

/* Font Families */
#define FF_DONTCARE         (0<<4)  /* Don't care or don't know. */
#define FF_ROMAN            (1<<4)  /* Variable stroke width, serifed. */
                                    /* Times Roman, Century Schoolbook, etc. */
#define FF_SWISS            (2<<4)  /* Variable stroke width, sans-serifed. */
                                    /* Helvetica, Swiss, etc. */
#define FF_MODERN           (3<<4)  /* Constant stroke width, serifed or sans-serifed. */
                                    /* Pica, Elite, Courier, etc. */
#define FF_SCRIPT           (4<<4)  /* Cursive, etc. */
#define FF_DECORATIVE       (5<<4)  /* Old English, etc. */

/* Font Weights */
#define FW_DONTCARE         0
#define FW_THIN             100
#define FW_EXTRALIGHT       200
#define FW_LIGHT            300
#define FW_NORMAL           400
#define FW_MEDIUM           500
#define FW_SEMIBOLD         600
#define FW_BOLD             700
#define FW_EXTRABOLD        800
#define FW_HEAVY            900

#define OUT_DEFAULT_PRECIS          0
#define OUT_STRING_PRECIS           1
#define OUT_CHARACTER_PRECIS        2
#define OUT_STROKE_PRECIS           3
#define OUT_TT_PRECIS               4
#define OUT_DEVICE_PRECIS           5
#define OUT_RASTER_PRECIS           6
#define OUT_TT_ONLY_PRECIS          7
#define OUT_OUTLINE_PRECIS          8
#define OUT_SCREEN_OUTLINE_PRECIS   9
#define OUT_PS_ONLY_PRECIS          10

#define CLIP_DEFAULT_PRECIS     0
#define CLIP_CHARACTER_PRECIS   1
#define CLIP_STROKE_PRECIS      2
#define CLIP_MASK               0xf
#define CLIP_LH_ANGLES          (1<<4)
#define CLIP_TT_ALWAYS          (2<<4)
#define CLIP_DFA_DISABLE        (4<<4)
#define CLIP_EMBEDDED           (8<<4)

#define DEFAULT_QUALITY           0
#define DRAFT_QUALITY             1
#define PROOF_QUALITY             2
#define NONANTIALIASED_QUALITY    3
#define ANTIALIASED_QUALITY       4
#define CLEARTYPE_QUALITY         5
#define CLEARTYPE_NATURAL_QUALITY 6

#define DEFAULT_PITCH           0
#define FIXED_PITCH             1
#define VARIABLE_PITCH          2
#define MONO_FONT               8

#ifdef __cplusplus
extern "C" {
#endif



/*
 * BASETYPES is defined in ntdef.h if these types are already defined
 */

#ifndef BASETYPES
#define BASETYPES
#define S_OK                                   ((HRESULT)0L)
typedef unsigned int ULONG;
typedef ULONG *PULONG;
typedef unsigned short USHORT;
typedef USHORT *PUSHORT;
typedef unsigned char UCHAR;
typedef UCHAR *PUCHAR;
typedef char *PSZ;
typedef unsigned char byte;
typedef int HRESULT;


#define FAR                 far
#define NEAR                near

#undef far
#undef near
#undef pascal

#define far
#define near
#if (!defined(_MAC)) && ((_MSC_VER >= 800) || defined(_STDCALL_SUPPORTED))
#define pascal __stdcall
#else
#define pascal
#endif

#define BI_RGB        0L
#define BI_RLE8       1L
#define BI_RLE4       2L
#define BI_BITFIELDS  3L
#define BI_JPEG       4L
#define BI_PNG        5L

#ifndef FREE_IMAGE_TYPES
#include <inttypes.h>

typedef uint32_t  			DWORD;
typedef int32_t				LONG;
typedef unsigned char       BYTE;
typedef unsigned short      WORD;
typedef int                 BOOL;

typedef LONG *PLONG;

typedef struct tagBITMAPINFOHEADER{
	DWORD      biSize;
	LONG       biWidth;
	LONG       biHeight;
	WORD       biPlanes;
	WORD       biBitCount;
	DWORD      biCompression;
	DWORD      biSizeImage;
	LONG       biXPelsPerMeter;
	LONG       biYPelsPerMeter;
	DWORD      biClrUsed;
	DWORD      biClrImportant;
} BITMAPINFOHEADER, FAR *LPBITMAPINFOHEADER, *PBITMAPINFOHEADER;



typedef struct tagRGBQUAD {
#if (!(defined(BYTE_ORDER) && BYTE_ORDER==BIG_ENDIAN) || (defined(__BYTE_ORDER) && __BYTE_ORDER==__BIG_ENDIAN) || 	defined(__BIG_ENDIAN__))
	  BYTE rgbBlue;
	  BYTE rgbGreen;
	  BYTE rgbRed;
#else
	  BYTE rgbRed;
	  BYTE rgbGreen;
	  BYTE rgbBlue;
#endif // FREEIMAGE_COLORORDER
	BYTE    rgbReserved;
} RGBQUAD;



typedef struct tagBITMAPINFO {
	BITMAPINFOHEADER    bmiHeader;
	RGBQUAD             bmiColors[1];
} BITMAPINFO, FAR *LPBITMAPINFO, *PBITMAPINFO;

#endif //FREE_IMAGE_TYPES

typedef void *HANDLE;
typedef short SHORT;

#ifdef _LP64
	typedef uint64_t	UINT_PTR, *PUINT_PTR;
	typedef int64_t		INT_PTR, *PINT_PTR;
#else
	typedef uint32_t	UINT_PTR, *PUINT_PTR;
 	typedef int32_t		INT_PTR, *PINT_PTR;
#endif

#ifndef _WIN32
typedef long long unsigned int UINT64, *PUINT64;
typedef long long int LONGLONG,__int64;
typedef struct _LARGE_INTEGER {
     LONGLONG QuadPart;
} LARGE_INTEGER;

#define _atoi64 atoll

#ifndef UINT64_MAX
	#define UINT64_MAX	0xffffffffffffffffUL
#endif

#ifndef INT64_MAX
	#define INT64_MAX	0x7fffffffffffffffL
#endif

#endif

typedef signed char			__int8;
typedef signed short		__int16;
typedef unsigned char       UINT8, *PUINT8;
typedef unsigned short      UINT16, *PUINT16;
typedef char  TCHAR;
// 	typedef unsigned char BYTE;
// 	typedef unsigned int DWORD;
// 	typedef unsigned int DWORD_PTR;
// 	typedef unsigned int UINT;
// 	typedef unsigned int UINT_PTR;

typedef unsigned int        UINT32, *PUINT32;
// //	typedef unsigned short wchar_t;
// 	typedef const wchar_t* LPCWSTR;
// 	typedef int BOOL;
// 	#define FALSE 0
// 	#define TRUE 1
#endif  /* !BASETYPES */

#define MAX_PATH          260
#define _MAX_FNAME  256 /* max. length of file name component */

#ifndef FALSE
#define FALSE               0
#endif

#ifndef TRUE
#define TRUE                1
#endif

#ifndef IN
#define IN
#endif

#ifndef OUT
#define OUT
#endif

#ifndef OPTIONAL
#define OPTIONAL
#endif

#define DECLARE_HANDLE(name) struct name##__{int unused;}; typedef struct name##__ *name

#if defined(DOSWIN32) || defined(_MAC)
#define cdecl _cdecl
#ifndef CDECL
#define CDECL _cdecl
#endif
#else
#define cdecl
#ifndef CDECL
#define CDECL
#endif
#endif

#ifdef _MAC
#define CALLBACK    PASCAL
#define WINAPI      CDECL
#define WINAPIV     CDECL
#define APIENTRY    WINAPI
#define APIPRIVATE  CDECL
#ifdef _68K_
#define PASCAL      __pascal
#else
#define PASCAL
#endif
#elif (_MSC_VER >= 800) || defined(_STDCALL_SUPPORTED)
#define CALLBACK    __stdcall
#define WINAPI      __stdcall
#define WINAPIV     __cdecl
#define APIENTRY    WINAPI
#define APIPRIVATE  __stdcall
#define PASCAL      __stdcall
#else
#define CALLBACK
#define WINAPI
#define WINAPIV
#define APIENTRY    WINAPI
#define APIPRIVATE
#define PASCAL      pascal
#endif

typedef float               FLOAT;


#define CP_ACP                    0           // default to ANSI code page
#define CP_OEMCP                  1           // default to OEM  code page
#define CP_MACCP                  2           // default to MAC  code page
#define CP_THREAD_ACP             3           // current thread's ANSI code page
#define CP_SYMBOL                 42          // SYMBOL translations

#define CP_UTF7                   65000       // UTF-7 translation
#define CP_UTF8                   65001       // UTF-8 translation


#undef FAR
#undef  NEAR
#define FAR                 far
#define NEAR                near

typedef unsigned char       BYTE;
typedef unsigned short      WORD;
typedef float               FLOAT;
typedef FLOAT               *PFLOAT;
typedef BOOL near           *PBOOL;
typedef BOOL far            *LPBOOL;
typedef BYTE near           *PBYTE;
typedef BYTE far            *LPBYTE;
typedef int near            *PINT;
typedef int far             *LPINT;
typedef WORD near           *PWORD;
typedef WORD far            *LPWORD;
typedef LONG far            *LPLONG;
typedef DWORD near          *PDWORD;
typedef DWORD far           *LPDWORD;
typedef void far            *LPVOID;
typedef CONST void far      *LPCVOID;
typedef INT_PTR LONG_PTR, *PLONG_PTR;
typedef UINT_PTR ULONG_PTR, *PULONG_PTR;
typedef ULONG_PTR DWORD_PTR, *PDWORD_PTR;

/* Bitmap Header Definition */
typedef struct tagBITMAP
{
	LONG        bmType;
	LONG        bmWidth;
	LONG        bmHeight;
	LONG        bmWidthBytes;
	WORD        bmPlanes;
	WORD        bmBitsPixel;
	LPVOID      bmBits;
} BITMAP, *PBITMAP, NEAR *NPBITMAP, FAR *LPBITMAP;


typedef struct tagBITMAPCOREHEADER {
	DWORD   bcSize;                 /* used to get to color table */
	WORD    bcWidth;
	WORD    bcHeight;
	WORD    bcPlanes;
	WORD    bcBitCount;
} BITMAPCOREHEADER, FAR *LPBITMAPCOREHEADER, *PBITMAPCOREHEADER;


typedef struct tagBITMAPFILEHEADER {
	WORD    bfType;
	WORD   bfSize;
	WORD   bfSizeHigh;
//	DWORD   bfSize;
	WORD    bfReserved1;
	WORD    bfReserved2;
//	DWORD   bfOffBits;
	WORD   bfOffBits;
    WORD   bfOffBitsHigh;
} BITMAPFILEHEADER, FAR *LPBITMAPFILEHEADER, *PBITMAPFILEHEADER;

typedef struct tagDIBSECTION {
	BITMAP       dsBm;
	BITMAPINFOHEADER    dsBmih;
	DWORD               dsBitfields[3];
	HANDLE              dshSection;
	DWORD               dsOffset;
} DIBSECTION, FAR *LPDIBSECTION, *PDIBSECTION;

#ifndef __EMSCRIPTEN__
	DECLARE_HANDLE(HWND);
	DECLARE_HANDLE(HBITMAP);
	DECLARE_HANDLE(HICON);
#else 
	typedef emscripten::val HWND;
 	typedef emscripten::val HBITMAP;
 	typedef emscripten::val HICON;
#endif

typedef HANDLE              HGLOBAL;
DECLARE_HANDLE(HDC);
DECLARE_HANDLE(HFONT);
DECLARE_HANDLE(HINSTANCE);
typedef HINSTANCE HMODULE;      /* HMODULEs can be used in place of HINSTANCEs */

 typedef DWORD   COLORREF;
 typedef DWORD   *LPCOLORREF;
 
 typedef struct tagRECT
 {
     LONG    left;
     LONG    top;
     LONG    right;
     LONG    bottom;
 } RECT, *PRECT, NEAR *NPRECT, FAR *LPRECT;
 
 typedef const RECT FAR* LPCRECT;
 
 typedef struct _RECTL       /* rcl */
 {
     LONG    left;
     LONG    top;
     LONG    right;
     LONG    bottom;
 } RECTL, *PRECTL, *LPRECTL;
 
 typedef const RECTL FAR* LPCRECTL;
 
 typedef struct tagPOINT
 {
     LONG  x;
     LONG  y;
 } POINT, *PPOINT, NEAR *NPPOINT, FAR *LPPOINT;
 
 typedef struct _POINTL      /* ptl  */
 {
     LONG  x;
     LONG  y;
 } POINTL, *PPOINTL;

 typedef struct tagSIZE
 {
     LONG        cx;
     LONG        cy;
 } SIZE, *PSIZE, *LPSIZE;
 
 typedef SIZE               SIZEL;
 typedef SIZE               *PSIZEL, *LPSIZEL;
 
 typedef struct tagPOINTS
 {
 #ifndef _MAC
     SHORT   x;
     SHORT   y;
 #else
     SHORT   y;
     SHORT   x;
 #endif
 } POINTS, *PPOINTS, *LPPOINTS;
 
#define _MAX_PATH   260 /* max. length of full pathname */
#define LF_FACESIZE         32

 typedef struct tagLOGFONTA
 {
	 LONG      lfHeight;
	 LONG      lfWidth;
	 LONG      lfEscapement;
	 LONG      lfOrientation;
	 LONG      lfWeight;
	 BYTE      lfItalic;
	 BYTE      lfUnderline;
	 BYTE      lfStrikeOut;
	 BYTE      lfCharSet;
	 BYTE      lfOutPrecision;
	 BYTE      lfClipPrecision;
	 BYTE      lfQuality;
	 BYTE      lfPitchAndFamily;
	 CHAR      lfFaceName[LF_FACESIZE];
 } LOGFONTA, *PLOGFONTA, NEAR *NPLOGFONTA, FAR *LPLOGFONTA;

 typedef struct tagLOGFONTW
 {
	 LONG      lfHeight;
	 LONG      lfWidth;
	 LONG      lfEscapement;
	 LONG      lfOrientation;
	 LONG      lfWeight;
	 BYTE      lfItalic;
	 BYTE      lfUnderline;
	 BYTE      lfStrikeOut;
	 BYTE      lfCharSet;
	 BYTE      lfOutPrecision;
	 BYTE      lfClipPrecision;
	 BYTE      lfQuality;
	 BYTE      lfPitchAndFamily;
	 WCHAR     lfFaceName[LF_FACESIZE];
 } LOGFONTW, *PLOGFONTW, NEAR *NPLOGFONTW, FAR *LPLOGFONTW;

#define FILE_SHARE_READ                 0x00000001  
#define FILE_SHARE_WRITE                0x00000002  
#define FILE_SHARE_DELETE               0x00000004  
#define FILE_ATTRIBUTE_READONLY             0x00000001  
#define FILE_ATTRIBUTE_HIDDEN               0x00000002  
#define FILE_ATTRIBUTE_SYSTEM               0x00000004  
#define FILE_ATTRIBUTE_DIRECTORY            0x00000010  
#define FILE_ATTRIBUTE_ARCHIVE              0x00000020  
#define FILE_ATTRIBUTE_DEVICE               0x00000040  
#define FILE_ATTRIBUTE_NORMAL               0x00000080  
#define FILE_ATTRIBUTE_TEMPORARY            0x00000100  
#define FILE_ATTRIBUTE_SPARSE_FILE          0x00000200  
#define FILE_ATTRIBUTE_REPARSE_POINT        0x00000400  
#define FILE_ATTRIBUTE_COMPRESSED           0x00000800  
#define FILE_ATTRIBUTE_OFFLINE              0x00001000  
#define FILE_ATTRIBUTE_NOT_CONTENT_INDEXED  0x00002000  
#define FILE_ATTRIBUTE_ENCRYPTED            0x00004000  
#define FILE_ATTRIBUTE_VIRTUAL              0x00010000  
#define FILE_NOTIFY_CHANGE_FILE_NAME    0x00000001   
#define FILE_NOTIFY_CHANGE_DIR_NAME     0x00000002   
#define FILE_NOTIFY_CHANGE_ATTRIBUTES   0x00000004   
#define FILE_NOTIFY_CHANGE_SIZE         0x00000008   
#define FILE_NOTIFY_CHANGE_LAST_WRITE   0x00000010   
#define FILE_NOTIFY_CHANGE_LAST_ACCESS  0x00000020   
#define FILE_NOTIFY_CHANGE_CREATION     0x00000040   
#define FILE_NOTIFY_CHANGE_SECURITY     0x00000100   
#define FILE_ACTION_ADDED                   0x00000001   
#define FILE_ACTION_REMOVED                 0x00000002   
#define FILE_ACTION_MODIFIED                0x00000003   
#define FILE_ACTION_RENAMED_OLD_NAME        0x00000004   
#define FILE_ACTION_RENAMED_NEW_NAME        0x00000005   
#define MAILSLOT_NO_MESSAGE             ((DWORD)-1) 
#define MAILSLOT_WAIT_FOREVER           ((DWORD)-1) 
#define FILE_CASE_SENSITIVE_SEARCH      0x00000001  
#define FILE_CASE_PRESERVED_NAMES       0x00000002  
#define FILE_UNICODE_ON_DISK            0x00000004  
#define FILE_PERSISTENT_ACLS            0x00000008  
#define FILE_FILE_COMPRESSION           0x00000010  
#define FILE_VOLUME_QUOTAS              0x00000020  
#define FILE_SUPPORTS_SPARSE_FILES      0x00000040  
#define FILE_SUPPORTS_REPARSE_POINTS    0x00000080  
#define FILE_SUPPORTS_REMOTE_STORAGE    0x00000100  
#define FILE_VOLUME_IS_COMPRESSED       0x00008000  
#define FILE_SUPPORTS_OBJECT_IDS        0x00010000  
#define FILE_SUPPORTS_ENCRYPTION        0x00020000  
#define FILE_NAMED_STREAMS              0x00040000  
#define FILE_READ_ONLY_VOLUME           0x00080000  
#define FILE_SEQUENTIAL_WRITE_ONCE      0x00100000  
#define FILE_SUPPORTS_TRANSACTIONS      0x00200000  

#define MOVEFILE_REPLACE_EXISTING       0x00000001
#define MOVEFILE_COPY_ALLOWED           0x00000002
#define MOVEFILE_DELAY_UNTIL_REBOOT     0x00000004
#define MOVEFILE_WRITE_THROUGH          0x00000008

 inline BOOL CopyRect( LPRECT lprcDst, CONST RECT *lprcSrc) ;
 inline BOOL IsRectEmpty(CONST RECT *lprc);
 inline BOOL PtInRect(CONST RECT *lprc,POINT pt) ;
 inline BOOL SetRect(LPRECT lprc,int xLeft,int yTop,int xRight,int yBottom);
 inline BOOL SetRectEmpty(LPRECT lprc);
 inline BOOL EqualRect(CONST RECT *lprc1,CONST RECT *lprc2) ;
 inline BOOL InflateRect(LPRECT lprc,int dx,int dy);
 inline BOOL OffsetRect(LPRECT lprc,int dx,int dy);
 inline BOOL UnionRect(LPRECT lprcDst,CONST RECT *lprcSrc1,CONST RECT *lprcSrc2);
 inline BOOL IntersectRect(LPRECT lprcDst,CONST RECT *lprcSrc1,CONST RECT *lprcSrc2);
 inline BOOL SubtractRect(LPRECT lprcDst,CONST RECT *lprcSrc1,CONST RECT *lprcSrc2) ;

 inline BOOL CopyRect(LPRECT lprcDst,
	 CONST RECT *lprcSrc)
 {
	 if(lprcDst == NULL || lprcSrc == NULL)
		 return(FALSE);

	 *lprcDst = *lprcSrc;
	 return(TRUE);
 }

 inline BOOL EqualRect(CONST RECT *lprc1,
	 CONST RECT *lprc2)
 {
	 if (lprc1 == NULL || lprc2 == NULL)
		 return FALSE;

	 return (lprc1->left == lprc2->left) && (lprc1->top == lprc2->top) &&
		 (lprc1->right == lprc2->right) && (lprc1->bottom == lprc2->bottom);
 }

 inline BOOL InflateRect(LPRECT rect,
	 int dx,
	 int dy)
 {
	 rect->left -= dx;
	 rect->top -= dy;
	 rect->right += dx;
	 rect->bottom += dy;
	 return(TRUE);
 }

 inline BOOL IntersectRect(LPRECT lprcDst,
	 CONST RECT *lprcSrc1,
	 CONST RECT *lprcSrc2)
 {
	 if (IsRectEmpty(lprcSrc1) || IsRectEmpty(lprcSrc2) ||
		 lprcSrc1->left >= lprcSrc2->right ||
		 lprcSrc2->left >= lprcSrc1->right ||
		 lprcSrc1->top >= lprcSrc2->bottom ||
		 lprcSrc2->top >= lprcSrc1->bottom)
	 {
		 SetRectEmpty(lprcDst);
		 return(FALSE);
	 }
	 lprcDst->left = __max(lprcSrc1->left, lprcSrc2->left);
	 lprcDst->right = __min(lprcSrc1->right, lprcSrc2->right);
	 lprcDst->top = __max(lprcSrc1->top, lprcSrc2->top);
	 lprcDst->bottom = __min(lprcSrc1->bottom, lprcSrc2->bottom);
	 return(TRUE);
 }

 inline BOOL IsRectEmpty(CONST RECT *lprc)
 {
	 return((lprc->left >= lprc->right) || (lprc->top >= lprc->bottom));
 }

 inline BOOL OffsetRect(LPRECT rect,
	 int dx,
	 int dy)
 {
	 if(rect == NULL)
		 return(FALSE);

	 rect->left += dx;
	 rect->top += dy;
	 rect->right += dx;
	 rect->bottom += dy;
	 return(TRUE);
 }

 inline BOOL PtInRect(CONST RECT *lprc,
	 POINT pt)
 {
	 return((pt.x >= lprc->left) && (pt.x < lprc->right) &&
		 (pt.y >= lprc->top) && (pt.y < lprc->bottom));
 }

 inline BOOL SetRect(LPRECT lprc,
	 int xLeft,
	 int yTop,
	 int xRight,
	 int yBottom)
 {
	 lprc->left = xLeft;
	 lprc->top = yTop;
	 lprc->right = xRight;
	 lprc->bottom = yBottom;
	 return(TRUE);
 }

 inline BOOL SetRectEmpty(LPRECT lprc)
 {
	 lprc->left = lprc->right = lprc->top = lprc->bottom = 0;
	 return(TRUE);
 }


 inline BOOL SubtractRect(LPRECT lprcDst,
	 CONST RECT *lprcSrc1,
	 CONST RECT *lprcSrc2)
 {
	 RECT tempRect;

	 if(lprcDst == NULL || lprcSrc1 == NULL || lprcSrc2 == NULL)
		 return(FALSE);

	 CopyRect(lprcDst, lprcSrc1);

	 if(!IntersectRect(&tempRect, lprcSrc1, lprcSrc2))
		 return(TRUE);

	 if (EqualRect(&tempRect, lprcDst))
	 {
		 SetRectEmpty(lprcDst);
		 return FALSE;
	 }
	 if(lprcDst->top == tempRect.top && lprcDst->bottom == tempRect.bottom)
	 {
		 if(lprcDst->left == tempRect.left)
			 lprcDst->left = tempRect.right;
		 else if(lprcDst->right == tempRect.right)
			 lprcDst->right = tempRect.left;
	 }
	 else if(lprcDst->left == tempRect.left && lprcDst->right == tempRect.right)
	 {
		 if(lprcDst->top == tempRect.top)
			 lprcDst->top = tempRect.bottom;
		 else if(lprcDst->right == tempRect.right)
			 lprcDst->right = tempRect.left;
	 }

	 return(TRUE);
 }

 inline BOOL UnionRect(LPRECT lprcDst,
	 CONST RECT *lprcSrc1,
	 CONST RECT *lprcSrc2)
 {
	 if (IsRectEmpty(lprcSrc1))
	 {
		 if (IsRectEmpty(lprcSrc2))
		 {
			 SetRectEmpty(lprcDst);
			 return(FALSE);
		 }
		 else
		 {
			 *lprcDst = *lprcSrc2;
		 }
	 }
	 else
	 {
		 if (IsRectEmpty(lprcSrc2))
		 {
			 *lprcDst = *lprcSrc1;
		 }
		 else
		 {
			 lprcDst->left = __min(lprcSrc1->left, lprcSrc2->left);
			 lprcDst->top = __min(lprcSrc1->top, lprcSrc2->top);
			 lprcDst->right = __max(lprcSrc1->right, lprcSrc2->right);
			 lprcDst->bottom = __max(lprcSrc1->bottom, lprcSrc2->bottom);
		 }
	 }

	 return(TRUE);
 }

inline int _mkdir(const char *strDir)
{
	return mkdir(strDir,0777);
}

#ifdef __cplusplus
}
#endif
#endif /* _WINDEF_ */
#endif /* _MC_BASIC_TYPES_ */
//#endif //_WIN32

#endif
