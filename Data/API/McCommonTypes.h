#pragma once
//==================================================================================
/// \file McCommonTypes.h
/// Types used in different header files
//==================================================================================

#include "McExports.h"
#include "McBasicTypes.h"
#include "CMcDataArray.h"
#include "SMcSizePointRect.h"
#include "SMcVector.h"
#include "SMcColor.h"
#include "string.h"
#include "IMcErrors.h"

//================================================================
/// Point Coordinate System Type
//
// Must use this order (high transformable higher in the enum)
//================================================================
enum EMcPointCoordSystem 
{
	EPCS_IMAGE,							///< Image coordinate system in pixels (2D)
	EPCS_WORLD,							///< World coordinate system in map units(3D)
	EPCS_SCREEN							///< Screen (window) coordinate system in pixels (2D)
};

/// Field data types in vector map layers.
enum EFieldType 
{
	IntegerType =  0,					///< 32bit integer
	RealType = 2,						///< Double precision floating point
	StringType = 4,						///< String of ASCII chars
	RawBinaryType = 8,					///< Raw binary data
	Integer64Type = 12,					///< 64bit integer

	UnSupportedType = 99				///< Unsupported type of field data
};

/// Basic Geometry types of raw vector map layers.
enum EGeometry 
{
	LineGeometry =  0,					///< The raw vector entities are lines
	PolygonGeometry = 1,				///< The raw vector entities are polygons
	PointGeometry = 2,					///< The raw vector entities are points

	UnSupportedGeometry = 99			///< The raw vector entities are of unsupported geometry
};

/// Extended geometry types of raw vector map layers.
enum EExtendedGeometry : UINT // based on OGR Geometry Type (OGRwkbGeometryType)
{
	EEG_Unknown					= 0,	///< The raw vector entities are of unknown geometry
								
    EEG_Point					= 1,	///< The raw vector entities are points
    EEG_LineString				= 2,	///< The raw vector entities are lines
    EEG_Polygon					= 3,	///< The raw vector entities are polygons
    							
	EEG_MultiPoint				= 4,	///< The raw vector entities are collection of points
    EEG_MultiLineString			= 5,	///< The raw vector entities are collection of lines
    EEG_MultiPolygon			= 6,	///< The raw vector entities are collection of polygons
								
	EEG_GeometryCollection		= 7,	///< The raw vector entities are collections of one or more geometric types
								
    EEG_None					= 100,	///< Non-standard, for pure attribute raw vector entities
								
    EEG_LinearRing				= 101,	///< Non-standard
								
    EEG_Point25D				= 0x80000001, 	///< The raw vector entities are points 2.5D extension
    EEG_LineString25D			= 0x80000002,	///< The raw vector entities are lines 2.5D extension	
    EEG_Polygon25D				= 0x80000003,	///< The raw vector entities are polygons 2.5D extension
								
    EEG_MultiPoint25D			= 0x80000004,	///< The raw vector entities are collection of points 2.5D extension	
    EEG_MultiLineString25D		= 0x80000005,	///< The raw vector entities are collection of lines 2.5D extension	
    EEG_MultiPolygon25D			= 0x80000006,	///< The raw vector entities are collection of polygons 2.5D extension	

    EEG_GeometryCollection25D	= 0x80000007	///< The raw vector entities are collections of one or more 2.5D extension geometric types
};

/// Describes the alignment on the X axis
enum EAxisXAlignment
{
    /// Align to left
    EXA_LEFT,
    /// Align to center
    EXA_CENTER,
    /// Align to right
    EXA_RIGHT
};

/// Describes the alignment on the Y axis
enum EAxisYAlignment
{
    /// Align to top
    EYA_TOP,
    /// Align to center
    EYA_CENTER,
    /// Align to bottom
    EYA_BOTTOM
};

/// The type (native C++ only) that can contain bool value or no-value.
/// The following is used in other languages:
/// - C#: bool?
/// - Java: Boolean
/// - JavaScript: boolean or null
struct SMcNullablelBool
{
	/// Default constructor (no-value)
	SMcNullablelBool() : uValue(2) {}

	/// Constructor with bool value
	SMcNullablelBool(bool bValue) : uValue(bValue ? 1 : 0) {}

	/// Copy constructor
	SMcNullablelBool(const SMcNullablelBool &Other) : uValue(Other.uValue) {}

	/// Assignment operator
	SMcNullablelBool& operator=(const SMcNullablelBool &Other) { uValue = Other.uValue; return *this; }

	/// Equality operator
	bool operator==(const SMcNullablelBool &Other) const { return (uValue == Other.uValue); }

	/// Inequality operator
	bool operator!=(const SMcNullablelBool &Other) const { return (uValue != Other.uValue); }

	/// operator bool
	operator bool() const { return (uValue == 1); }

	/// Whether a bool value exists
	bool HasValue() const { return (uValue <= 1); }
private:
	BYTE uValue;
};

/// The type for a histogram for each color channel
typedef		 __int64	MC_HISTOGRAM[256];

//==============================================================================
/// Empty ID
//==============================================================================
#define MC_EMPTY_ID						UINT_MAX

//==============================================================================
/// Special ID for multi-contour polygon's sub-items (see SMcSubItemData)
//==============================================================================
#define MC_EXTRA_CONTOUR_SUB_ITEM_ID	(UINT_MAX - 1)

//==============================================================================
/// Special ID for multi-contour polygon's sub-items in a Vector MapLayer
//==============================================================================
#define MC_EXTRA_CONTOUR_VECTOR_ITEM_ID	(UINT64_MAX - 1)

//==============================================================================
/// Ellipse/Arc sampling accuracy
//==============================================================================
#define MC_MAX_NUM_POINTS_PER_COMPLETE_ELLIPSE	64

//==============================================================================
/// No DTM value
//==============================================================================
#define	MC_NO_DTM_VALUE -1000

/// Variant ID that can be used as 32 (through \a u32Bit), 64 (through \a u64Bit), 128 bit (through \a a128bit[16])
struct SMcVariantID
{
	/// union: 3 access ways
	union
	{
		UINT32	u32Bit;			///< 32 bits as UINT32
		UINT64	u64Bit;			///< 64 bits as UINT64
		BYTE	a128bit[16];	///< 128 bits as BYTE[16]
	};

	/// default constructor
	SMcVariantID()
	{
		u64Bit = 0;
		(&u64Bit)[1] = 0;
	}

	/// 32-bit constructor
	SMcVariantID(UINT32 _u32Bit)
	{
		u64Bit = 0;
		(&u64Bit)[1] = 0;
		u32Bit = _u32Bit;
	}

	/// 64-bit constructor
	SMcVariantID(UINT64 _u64Bit)
	{
		u64Bit = _u64Bit;
		(&u64Bit)[1] = 0;
	}

	/// 128-bit constructor
	SMcVariantID(BYTE _a128bit[16])
	{
		memcpy(a128bit, _a128bit, sizeof(a128bit));
	}

	/// copy constructor
	SMcVariantID(const SMcVariantID &Other)
	{
		*this = Other;
	}

	/// makes empty
	void SetEmpty()
	{
		u64Bit = UINT64(-1);
		(&u64Bit)[1] = UINT64(-1);
	}
	
	/// checks whether is empty
	bool IsEmpty() const
	{
		return (u64Bit == UINT64(-1) && (&u64Bit)[1] == UINT64(-1));
	}

	/// sets 128-bit ID from a UUID string
	COMMONUTILS_API IMcErrors::ECode Set128bitAsUUIDString(PCSTR strUUID);

	/// retrieves 128-bit ID as a UUID string
	COMMONUTILS_API void Get128bitAsUUIDString(CMcString *pstrUUID) const;

	/// operator =
	const SMcVariantID& operator=(const SMcVariantID &Other)
	{
		u64Bit = Other.u64Bit;
		(&u64Bit)[1] = (&Other.u64Bit)[1];
		return *this;
	}

	/// operator ==
	bool operator==(const SMcVariantID &Other) const
	{
		return (u64Bit == Other.u64Bit && (&u64Bit)[1] == (&Other.u64Bit)[1]);
	}

	/// operator !=
	bool operator!=(const SMcVariantID &Other) const
	{
		return (u64Bit != Other.u64Bit || (&u64Bit)[1] != (&Other.u64Bit)[1]);
	}
};

/// Variant string that can be used as ANSI (through \a astrAnsiStrings or \a strSingleAnsiString), UNICODE (through \a astrUnicodeStrings or \a strSingleUnicodeString)
struct SMcVariantString
{
	/// \name Construction & Assignment
	//@{

	SMcVariantString() {}
	SMcVariantString(PCSTR str) : astrAnsiStrings(NULL), strSingleAnsiString(str), uNumStrings(1), bIsUnicode(false) {}
	SMcVariantString(PCWSTR str) : astrUnicodeStrings(NULL), strSingleUnicodeString(str), uNumStrings(1), bIsUnicode(true) {}
	SMcVariantString(const PCSTR astr[], UINT _uNumStrings) 
		: astrAnsiStrings(astr), strSingleAnsiString(NULL), uNumStrings(_uNumStrings), bIsUnicode(false) {}
	SMcVariantString(const PCWSTR astr[], UINT _uNumStrings) 
		: astrUnicodeStrings(astr), strSingleUnicodeString(NULL), uNumStrings(_uNumStrings), bIsUnicode(true) {}
	SMcVariantString(void *pStr, bool _bUnicode) 
		: astrAnsiStrings(NULL), strSingleAnsiString((PCSTR)pStr), uNumStrings(1), bIsUnicode(_bUnicode) {}
	SMcVariantString(void *aStrArray, UINT _uNumStrings, bool _bIsUnicode) 
		: astrAnsiStrings((PCSTR*)aStrArray), strSingleAnsiString(NULL), uNumStrings(_uNumStrings), bIsUnicode(_bIsUnicode) {}
	SMcVariantString(const SMcVariantString &str) { *this = str; }
	SMcVariantString& operator=(const SMcVariantString &str)
	{
		astrAnsiStrings = str.astrAnsiStrings;
		strSingleAnsiString = str.strSingleAnsiString;
		bIsUnicode = str.bIsUnicode;
		uNumStrings = str.uNumStrings;
		return *this;
	}
	//@}

	/// \name Getters
	//@{

	/// retrieves ANSI strings (array or pointer to single string), this memory cannot be released!
	const PCSTR* GetAnsiStrings() const { return (astrAnsiStrings != NULL ? astrAnsiStrings : &strSingleAnsiString); }

	/// retrieves ANSI strings array if array is used (NULL if single string is used)
	const PCSTR* GetAnsiStringsArray() const { return astrAnsiStrings; }

	/// retrieves Unicode strings (array or pointer to single string), this memory cannot be released!
	const PCWSTR* GetUnicodeStrings() const { return (astrUnicodeStrings != NULL ? astrUnicodeStrings : &strSingleUnicodeString); }

	/// retrieves Unicode strings array if array is used (NULL if single string is used)
	const PCWSTR* GetUnicodeStringsArray() const { return astrUnicodeStrings; }

	/// retrieves number of strings
	UINT GetNumStrings() const { return uNumStrings; }

	/// check whether UNIODE or ANSI string(s) are used
	bool IsUnicode() const { return bIsUnicode; }
	//@}

private:
	union 
	{
		const PCSTR		*astrAnsiStrings;
		const PCWSTR	*astrUnicodeStrings;
	};
	union 
	{
		PCSTR			strSingleAnsiString;
		PCWSTR			strSingleUnicodeString;
	};
	UINT				uNumStrings;
	bool				bIsUnicode;
};

/// Variant font attributes definition used to define a font via Win32 API's structures (`LOGFONTA`/`LOGFONTW` for a font with ANSI/UNICODE name respectively).
///
/// In wrappers \a LogFontAnsi and \a LogFontUnicode are not used, the following is used instead:
/// - .NET wrapper: \a logFont of .NET's type `Font`
/// - JS wrapper: `LOGFONTA`'s members used directly in SMcVariantLogFont
/// - Java wrapper: \a LogFont of type `LOGFONT` (equivalent to Win32's `LOGFONTA`) defined in Java wrapper
struct SMcVariantLogFont
{
	union 
	{
		LOGFONTA LogFontAnsi;		///< Win32-API-like `LOGFONTA` defining a font with ANSI name
		LOGFONTW LogFontUnicode;	///< Win32-API-like `LOGFONTW` defining a font with UNICODE name
	};
	bool bIsUnicode;				///< `false` if \a LogFontAnsi is used; `true` if \a LogFontUnicode is used
	bool bIsEmbedded;				///< `false` if only the font name and parameters are saved; `true` if the contents of the appropriate font file is 
									///<	embedded as well, so that exactly the same the font is used on any platform even if it is not installed there

	/// \name Construction & Assignment
	//@{

	SMcVariantLogFont() { memset(this, 0, sizeof(*this)); }
	SMcVariantLogFont(const LOGFONTA &LogFont, bool _bIsEmbedded = false) : LogFontAnsi(LogFont), bIsUnicode(false), bIsEmbedded(_bIsEmbedded) {}
	SMcVariantLogFont(const LOGFONTW &LogFont, bool _bIsEmbedded = false) : LogFontUnicode(LogFont), bIsUnicode(true), bIsEmbedded(_bIsEmbedded) {}
	SMcVariantLogFont(const SMcVariantLogFont &LogFont) { *this = LogFont; }
	SMcVariantLogFont& operator=(const SMcVariantLogFont &LogFont)
	{
		LogFontUnicode = LogFont.LogFontUnicode;
		bIsUnicode = LogFont.bIsUnicode;
		bIsEmbedded = LogFont.bIsEmbedded;
		return *this;
	}

	//@}

	/// \name Comparing Operators & Functions
	//@{

	bool operator==(const SMcVariantLogFont &Other) const
	{
		return (IsSameLogFont(*this, Other) == true);
	}
	bool operator!=(const SMcVariantLogFont &Other) const
	{
		return (IsSameLogFont(*this, Other) == false);
	}

	bool IsSameLogFont(const SMcVariantLogFont &LogFont1, const SMcVariantLogFont &LogFont2) const
	{
		if (LogFont1.bIsUnicode != LogFont2.bIsUnicode || LogFont1.bIsEmbedded != LogFont2.bIsEmbedded)
			return false;
		if (LogFont1.bIsUnicode)
		{
			return (
				memcmp(&LogFont1.LogFontUnicode, &LogFont2.LogFontUnicode, sizeof(LogFont1.LogFontUnicode) - sizeof(LogFont1.LogFontUnicode.lfFaceName)) == 0
				&&
				wcsncmp(LogFont1.LogFontUnicode.lfFaceName, LogFont2.LogFontUnicode.lfFaceName, LF_FACESIZE) == 0);
		}
		else
		{
			return (
				memcmp(&LogFont1.LogFontAnsi, &LogFont2.LogFontAnsi, sizeof(LogFont1.LogFontAnsi) - sizeof(LogFont1.LogFontAnsi.lfFaceName)) == 0
				&&
				strncmp(LogFont1.LogFontAnsi.lfFaceName, LogFont2.LogFontAnsi.lfFaceName, LF_FACESIZE) == 0);
		}
	}

	//@}
};

/// File source (either file-system file name or in-memory file buffer)
struct SMcFileSource
{
	union
	{
		PCSTR strFileName;							///< file-system file name
		struct
		{
			const BYTE *aFileMemoryBuffer;			///< in-memory file buffer
			UINT uMemoryBufferSize;					///< size of in-memory file buffer
			PCSTR strFormatExtension;				///< optional file extension defining file format (e.g. "jpg"); relevant for image files only;
													///< can be `NULL` (the default) if it can be determined by file contents
		};
	};

	bool bIsMemoryBuffer;							///< file source type: false for file-system file name, true for in-memory file buffer


	SMcFileSource() : bIsMemoryBuffer(false) { strFileName = NULL; }
	SMcFileSource(const SMcFileSource &FileSource) { *this = FileSource; }
	SMcFileSource& operator=(const SMcFileSource &FileSource)
	{
		aFileMemoryBuffer = FileSource.aFileMemoryBuffer;
		uMemoryBufferSize = FileSource.uMemoryBufferSize;
		strFormatExtension = FileSource.strFormatExtension;
		bIsMemoryBuffer = FileSource.bIsMemoryBuffer;
		return *this;
	}

	//==============================================================================
	// Method Name: SMcFileSource()
	//------------------------------------------------------------------------------
	/// Constructs SMcFileSource from file-system file name
	///
	/// \param[in]	_strFileName		file-system file name
	//==============================================================================
	SMcFileSource(PCSTR _strFileName) : strFileName(_strFileName), bIsMemoryBuffer(false) {}

	//==============================================================================
	// Method Name: SMcFileSource()
	//------------------------------------------------------------------------------
	/// Constructs SMcFileSource from in-memory file buffer
	///
	/// \param[in]	_aFileMemoryBuffer	in-memory file buffer
	/// \param[in]	_uMemoryBufferSize	size of in-memory file buffer
	/// \param[in]	_strFormatExtension	optional file extension defining file format (e.g. "jpg"); can be `NULL` (the default) if it can be determined by file contents
	//==============================================================================
	SMcFileSource(const BYTE _aFileMemoryBuffer[], UINT _uMemoryBufferSize, PCSTR _strFormatExtension = NULL)
		: aFileMemoryBuffer(_aFileMemoryBuffer), uMemoryBufferSize(_uMemoryBufferSize), strFormatExtension(_strFormatExtension), bIsMemoryBuffer(true) { }

	bool operator==(const SMcFileSource &Other) const
	{
		return (IsSameFileSource(*this, Other) == true);
	}
	bool operator!=(const SMcFileSource &Other) const
	{
		return (IsSameFileSource(*this, Other) == false);
	}

	bool IsSameFileSource(const SMcFileSource &FileSource1, const SMcFileSource &FileSource2) const
	{
		if (FileSource1.bIsMemoryBuffer != FileSource2.bIsMemoryBuffer)
			return false;
		if (FileSource1.bIsMemoryBuffer == false)
		{
			return (strcmp(FileSource1.strFileName, FileSource2.strFileName) == 0);
		}
		else // if (FileSource1.bIsMemoryBuffer == true)
		{
			return (
				(FileSource1.uMemoryBufferSize == FileSource2.uMemoryBufferSize)
				&&
				(memcmp(FileSource1.aFileMemoryBuffer, FileSource2.aFileMemoryBuffer, FileSource1.uMemoryBufferSize) == 0)
				&& (
					(FileSource1.strFormatExtension == NULL && FileSource2.strFormatExtension == NULL) ||
					(FileSource1.strFormatExtension != NULL && FileSource2.strFormatExtension != NULL && strcmp(FileSource1.strFormatExtension, FileSource2.strFormatExtension) == 0)
					));
		}
	}
};

/// File in memory (file name along with its contents)
struct SMcFileInMemory
{
	CMcString strFileName;				///< file name
	CMcDataArray<BYTE> auMemoryBuffer;	///< file contents as memory buffer
};


/// Pair of string key and string value
struct SMcKeyStringValue
{
	PCSTR strKey;						///< unique string key
	PCSTR strValue;						///< string value

	SMcKeyStringValue() : strKey(NULL), strValue(NULL) {}
};

/// Pair of string key and float value
struct SMcKeyFloatValue
{
	PCSTR strKey;						///< unique string key
	float fValue;						///< float value

	SMcKeyFloatValue() : strKey(NULL) {}
};

/// Quaternion for rotation (orientation) definition
struct SMcQuaternion
{
	double w;	///< w component
	double x;	///< x component
	double y;	///< y component
	double z;	///< z component

	/// Constructs zero quaternion (not valid)
	SMcQuaternion() : w(0), x(0), y(0), z(0) {}

	/// Constructs from w, x, y, z components
	SMcQuaternion(double _w, double _x, double _y, double _z) : w(_w), x(_x), y(_y), z(_z) {}

	/// Constructs from other quaternion
	SMcQuaternion(const SMcQuaternion &Other) : w(Other.w), x(Other.x), y(Other.y), z(Other.z) {}

	/// Constructs from yaw, pitch, roll angles in MapCore's coordinate system 
	/// (the default: X - right, Y - forward, Z - up; yaw rotates clockwise about Z, pitch counter-clockwise about X, roll counter-clockwise about Y) or 
	/// in graphics coordinate system (X - right, Y - up, Z - backward; yaw rotates clockwise about Y, pitch counter-clockwise about X, roll clockwise about Z)
	SMcQuaternion(double dYaw, double dPitch, double dRoll, bool bGraphicsCoordinateSystem = false)
	{
		FromYawPitchRoll(dYaw, dPitch, dRoll, bGraphicsCoordinateSystem);
	}

    /// Construct from an angle/axis
    SMcQuaternion(double dAngle, const SMcVector3D &Axis) { FromAngleAxis(dAngle, Axis); }
    
	/// Construct from 3 orthonormal local axes
    SMcQuaternion(const SMcVector3D &AxisX, const SMcVector3D &AxisY, const SMcVector3D &AxisZ) { FromAxes(AxisX, AxisY, AxisZ); }
	
	/// Assignment operator from other quaternion
	SMcQuaternion& operator=(const SMcQuaternion &Other) {  w = Other.w; x = Other.x; y = Other.y; z = Other.z; return *this; }

	/// Comparison operator
	bool operator==(const SMcQuaternion &Other) const { return w == Other.w && x == Other.x && y == Other.y && z == Other.z; }

	/// Comparison operator
	bool operator!=(const SMcQuaternion &Other) const { return w != Other.w || x != Other.x || y != Other.y || z != Other.z; }

	/// Setups the quaternion using yaw, pitch, roll angles in MapCore's coordinate system 
	/// (the default: X - right, Y - forward, Z - up; yaw rotates clockwise about Z, pitch counter-clockwise about X, roll counter-clockwise about Y) or 
	/// in graphics coordinate system (X - right, Y - up, Z - backward; yaw rotates clockwise about Y, pitch counter-clockwise about X, roll clockwise about Z)
	COMMONUTILS_API void FromYawPitchRoll(double dYaw, double dPitch, double dRoll, bool bGraphicsCoordinateSystem = false);

	/// Calculates yaw, pitch, roll angles defining the same rotation (orientation) in MapCore's coordinate system 
	/// (the default: X - right, Y - forward, Z - up; yaw rotates clockwise about Z, pitch counter-clockwise about X, roll counter-clockwise about Y) or 
	/// in graphics coordinate system (X - right, Y - up, Z - backward; yaw rotates clockwise about Y, pitch counter-clockwise about X, roll clockwise about Z)
	COMMONUTILS_API void ToYawPitchRoll(double *pdYaw, double *pdPitch, double *pdRoll, bool bGraphicsCoordinateSystem = false) const;

	/// Calculates yaw, pitch, roll angles defining the same rotation (orientation) in MapCore's coordinate system 
	/// (the default: X - right, Y - forward, Z - up; yaw rotates clockwise about Z, pitch counter-clockwise about X, roll counter-clockwise about Y) or 
	/// in graphics coordinate system (X - right, Y - up, Z - backward; yaw rotates clockwise about Y, pitch counter-clockwise about X, roll clockwise about Z)
	COMMONUTILS_API void ToYawPitchRoll(float *pfYaw, float *pfPitch, float *pfRoll, bool bGraphicsCoordinateSystem = false) const;

    /// Setups the quaternion using the supplied vector, and rotate around that vector by the angle
    COMMONUTILS_API void FromAngleAxis(double dAngle, const SMcVector3D &Axis);

    /// Calculates the vector and the angle defining the quaternion
    COMMONUTILS_API void ToAngleAxis(double *pdAngle, SMcVector3D *pAxis) const;

    /// Calculates the vector and the angle defining the quaternion
    COMMONUTILS_API void ToAngleAxis(float *pfAngle, SMcVector3D *pAxis) const;

    /// Setups the quaternion using 3 orthonormal local axes
    COMMONUTILS_API void FromAxes(const SMcVector3D &AxisX, const SMcVector3D &AxisY, const SMcVector3D &AxisZ);

     /// Calculates the 3 orthonormal axes defining the quaternion
	COMMONUTILS_API void ToAxes(SMcVector3D *pAxisX, SMcVector3D *pAxisY, SMcVector3D *pAxisZ) const;

	/// Calculates the quaternion defining the inversed transform
	COMMONUTILS_API SMcQuaternion GetInverse() const;

	/// Addition operator with other quaternion
	SMcQuaternion operator+(const SMcQuaternion &Other) const { return SMcQuaternion(w + Other.w, x + Other.x, y + Other.y, z + Other.z); }

	/// Addition operator with other quaternion 
	SMcQuaternion& operator+=(const SMcQuaternion &Other) { w += Other.w; x += Other.x, y += Other.y; z += Other.z; return *this; }

	/// Subtraction operator with other quaternion
	SMcQuaternion operator-(const SMcQuaternion &Other) const { return SMcQuaternion(w - Other.w, x - Other.x, y - Other.y, z - Other.z); }

	/// Subtraction operator with other quaternion 
	SMcQuaternion& operator-=(const SMcQuaternion &Other) { w -= Other.w; x -= Other.x, y -= Other.y; z -= Other.z; return *this; }

	/// Multiplication operator with scalar
	SMcQuaternion operator*(double d) const { return SMcQuaternion(w * d, x * d, y * d, z * d); }

	/// Multiplication operator with scalar 
	SMcQuaternion& operator*=(double d) { w *= d; x *= d; y *= d; z *= d; return  *this; }

	/// Multiplication operator with other quaternion
	COMMONUTILS_API SMcQuaternion operator*(const SMcQuaternion &Other) const;

	/// Multiplication operator with other quaternion 
	COMMONUTILS_API SMcQuaternion& operator*=(const SMcQuaternion &Other);

	/// Multiplication operator with 3D vector
	COMMONUTILS_API SMcVector3D operator*(const SMcVector3D &Vector) const;

	/// Multiplication operator with float-components 3D vector
	COMMONUTILS_API SMcFVector3D operator*(const SMcFVector3D &Vector) const;
};

const SMcQuaternion	

	/// Zero quaternion (not valid)
	qZero(0.0, 0.0, 0.0, 0.0),

	/// Identity quaternion (no rotation)
	qIdentity(1.0, 0.0, 0.0, 0.0);

/// 4x4 matrix for scale, rotation, translation and projection transforms
struct SMcMatrix4D
{
	double m[4][4];	///< matrix entries, indexed by [row][col]

    /// Constructs an uninitialized matrix (for efficiency)
	SMcMatrix4D() {}

    /// Constructs from 4x4-double array
	SMcMatrix4D(const double aadMatrix[4][4]) { *this = aadMatrix; }

    /// Constructs from 16-double array ordered row by row
	SMcMatrix4D(const double adMatrix[16]) { *this = adMatrix; }

    /// Constructs from other matrix
	SMcMatrix4D(const SMcMatrix4D &Other) { *this = Other; }

    /// Constructs from quaternion
	SMcMatrix4D(const SMcQuaternion &Rotation) { *this = Rotation; }

	/// Assignment operator from 4x4-double array
    SMcMatrix4D& operator=(const double aadMatrix[4][4]) { memcpy(m, aadMatrix, sizeof(m)); return *this; }

	/// Assignment operator from 16-double array ordered row by row
	SMcMatrix4D& operator=(const double adMatrix[16]) { memcpy(m, adMatrix, sizeof(m));  return *this; }

	/// Assignment operator from other matrix
	SMcMatrix4D& operator=(const SMcMatrix4D &Other) { memcpy(m, &Other.m, sizeof(m)); return *this; }

	/// Assignment operator from quaternion
	COMMONUTILS_API SMcMatrix4D& operator=(const SMcQuaternion &Rotation);

	/// Comparison operator
    bool operator==(const SMcMatrix4D &Other) const { return memcmp(m, Other.m, sizeof(m)) == 0; }

	/// Comparison operator
    bool operator!=(const SMcMatrix4D &Other) const { return memcmp(m, Other.m, sizeof(m)) != 0; }

	/// Indexing operator returning one row
	double* operator[](UINT uRow) { return m[uRow]; }

	/// Indexing operator returning one row
	const double* operator[](UINT uRow) const { return m[uRow]; }

	/// Multiplication operator with scalar
	COMMONUTILS_API SMcMatrix4D operator*(double d) const;

	/// Multiplication operator with scalar
	COMMONUTILS_API SMcMatrix4D& operator*=(double d);

	/// Multiplication operator with other matrix
	COMMONUTILS_API SMcMatrix4D operator*(const SMcMatrix4D &Other) const;

	/// Multiplication operator with other matrix
	COMMONUTILS_API SMcMatrix4D& operator*=(const SMcMatrix4D &Other);

	/// Multiplication operator with 3D vector
	COMMONUTILS_API SMcVector3D operator*(const SMcVector3D &Vector) const;

	/// Multiplication operator with float-components 3D vector
	COMMONUTILS_API SMcFVector3D operator*(const SMcFVector3D &Vector) const;

	/// Addition operator with other matrix
	COMMONUTILS_API SMcMatrix4D operator+(const SMcMatrix4D &Other) const;

	/// Addition operator with other matrix
	COMMONUTILS_API SMcMatrix4D& operator+=(const SMcMatrix4D &Other);

	/// Subtraction operator with other matrix
	COMMONUTILS_API SMcMatrix4D operator-(const SMcMatrix4D &Other) const;

	/// Subtraction operator with other matrix
	COMMONUTILS_API SMcMatrix4D& operator-=(const SMcMatrix4D &Other);

	/// Sets the scale part of the matrix
    void SetScale(const SMcVector3D &Vector) { m[0][0] = Vector.x; m[1][1] = Vector.y; m[2][2] = Vector.z; }

	/// Extracts the scale part of the matrix
    SMcVector3D GetScale() const { return SMcVector3D(m[0][0], m[1][1], m[2][2]); }

	/// Sets the rotation part of the matrix
    COMMONUTILS_API void SetRotation(const SMcQuaternion &Rotation);

	/// Extracts the rotation part of the matrix
    COMMONUTILS_API SMcQuaternion GetRotation() const;

	/// Sets the translation part of the matrix
	void SetTranslation(const SMcVector3D &Vector) { m[0][3] = Vector.x; m[1][3] = Vector.y; m[2][3] = Vector.z; }

	/// Extracts the translation part of the matrix
    SMcVector3D GetTranslation() const { return SMcVector3D(m[0][3], m[1][3], m[2][3]); }

	/// Calculates the determinant of the matrix
    COMMONUTILS_API double CalcDeterminant() const;

	/// Calculates the transposed matrix
    COMMONUTILS_API SMcMatrix4D GetTranspose() const;

	/// Calculates the inversed matrix
    COMMONUTILS_API SMcMatrix4D GetInverse() const;

    /// Builds a matrix from a transform defined by scale, rotation and translation (in that order, i.e. translation is independent of orientation axes, 
    /// scale does not affect size of translation, rotation and scaling are always centered on the origin)
	COMMONUTILS_API void FromTransform(const SMcVector3D &Scale, const SMcQuaternion &Rotation, const SMcVector3D &Translation);

    /// Builds a matrix from a transform inverse to that defined in FromTransform(), i.e. -translation, -rotation, 1/scale (in that order)
    COMMONUTILS_API void FromInverseTransform(const SMcVector3D &Scale, const SMcQuaternion &Rotation, const SMcVector3D &Translation);
};

const double adMcMatrix4DArrayZero[16]		= { 0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 0, 0 };
const double adMcMatrix4DArrayIdentity[16]	= { 1, 0, 0, 0,  0, 1, 0, 0,  0, 0, 1, 0,  0, 0, 0, 1 };

const SMcMatrix4D

	/// Zero matrix (not valid as a transform)
	m4Zero(adMcMatrix4DArrayZero),

	/// Identity matrix (no transform)
	m4Identity(adMcMatrix4DArrayIdentity);

/// Rotation (orientation) definition
struct SMcRotation
{
	float	fYaw;						///< yaw angle value
	float	fPitch;						///< pitch angle value
	float	fRoll;						///< roll angle value
	bool	bRelativeToCurrOrientation;	///< true if relative to current orientation, 
										///<   false if absolute (default)
    /// Constructs zero rotation
	SMcRotation() : fYaw(0), fPitch(0), fRoll(0), bRelativeToCurrOrientation(false)	{}

	/// Constructs from yaw, pitch, roll and relative flag
	SMcRotation(float _fYaw, float _fPitch, float _fRoll, bool _bRelativeToCurrOrientation = false)
	  : fYaw(_fYaw), fPitch(_fPitch), fRoll(_fRoll), bRelativeToCurrOrientation(_bRelativeToCurrOrientation) {}

	/// Constructs from other rotation
	SMcRotation(const SMcRotation &Other) { memcpy(this, &Other, sizeof(*this)); }

	/// Constructs from quaternion and relative flag in MapCore's coordinate system 
	/// (the default: X - right, Y - forward, Z - up; yaw rotates clockwise about Z, pitch counter-clockwise about X, roll counter-clockwise about Y) or 
	/// in graphics coordinate system (X - right, Y - up, Z - backward; yaw rotates clockwise about Y, pitch counter-clockwise about X, roll clockwise about Z)
	SMcRotation(const SMcQuaternion &Quaternion, bool _bRelativeToCurrOrientation = false, bool bGraphicsCoordinateSystem = false)
		: bRelativeToCurrOrientation(_bRelativeToCurrOrientation)
	{
		Quaternion.ToYawPitchRoll(&fYaw, &fPitch, &fRoll, bGraphicsCoordinateSystem);
	}

	/// Assignment operator from other rotation
	SMcRotation& operator=(const SMcRotation &Other)
	{
		memcpy(this, &Other, sizeof(*this));
		return *this;
	}
	/// Equality operator (relative rotations are always considered unequal)
	bool operator==(const SMcRotation &Other) const
	{
		return (fYaw == Other.fYaw && fPitch == Other.fPitch && fRoll == Other.fRoll && 
				!bRelativeToCurrOrientation && 
				bRelativeToCurrOrientation == Other.bRelativeToCurrOrientation);
	}
	/// Inequality operator (relative rotations are always considered unequal)
	bool operator!=(const SMcRotation &Other) const
	{
		return (fYaw != Other.fYaw || fPitch != Other.fPitch || fRoll != Other.fRoll || 
				bRelativeToCurrOrientation || 
				bRelativeToCurrOrientation != Other.bRelativeToCurrOrientation);
	}

	/// Converts to quaternion in MapCore's coordinate system 
	/// (the default: X - right, Y - forward, Z - up; yaw rotates clockwise about Z, pitch counter-clockwise about X, roll counter-clockwise about Y) or 
	/// in graphics coordinate system (X - right, Y - up, Z - backward; yaw rotates clockwise about Y, pitch counter-clockwise about X, roll clockwise about Z)
	void ToQuaternion(SMcQuaternion *pQuaternion, bool bGraphicsCoordinateSystem = false) const
	{
		pQuaternion->FromYawPitchRoll(fYaw, fPitch, fRoll, bGraphicsCoordinateSystem);
	}
};

/// Position and orientation parameters
struct SMcPositionAndOrientation
{
	SMcVector3D Position;		///< position
	SMcRotation Orientation;	///< orientation
};

/// Parameters of projection from world to camera's frame
struct SMcCameraProjectionParams
{
	/// frame width in pixels
	UINT 			uFrameWidth;

	/// frame height in pixels
	UINT 			uFrameHeight;

	/// pixel ratio (pixel's width / pixel's height)
	float 			fPixelAspectRatio;

	/// field-of-view horizontal angle in degrees
	float 			fFieldOfViewHorizAngle;

	/// offset of frame center relative to camera's optical center along \b x and \b y axes in half-frame units:
	///  - the default - (0, 0) means frame's center
	///  - (-1, -1) means frame's half-width left and half-height up from camera's optical center
	///  - ( 1,  1) means frame's half-width right and half-height down from camera's optical center
	SMcFVector2D	CenterOffset;
};

/// Mesh animation definition
struct SMcAnimation
{
	PCSTR strAnimationName;	///< The animation name as defined in mesh file or NULL to stop current animation
	bool bLoop;				///< Whether the animation should be repeated until stopped by the user

	/// \name Construction & Assignment
	//@{
	SMcAnimation() : strAnimationName(NULL), bLoop(false) {}
	SMcAnimation(PCSTR _strAnimationName, bool _bLoop)
		: strAnimationName(_strAnimationName), bLoop(_bLoop) {}
	SMcAnimation(const SMcAnimation &Other) { memcpy(this, &Other, sizeof(*this)); }
	SMcAnimation& operator=(const SMcAnimation &Other)
	{
		memcpy(this, &Other, sizeof(*this));
		return *this;
	}
	//@}

	/// \name Compare Operators
	//@{
	bool operator==(const SMcAnimation &Other) const
	{
		return (strAnimationName == Other.strAnimationName && bLoop == Other.bLoop);
	}
	bool operator!=(const SMcAnimation &Other) const
	{
		return (strAnimationName != Other.strAnimationName || bLoop != Other.bLoop);
	}
	//@}
};

/// Single sub-item data used in IMcProperty::SArrayProperty to define how a symbolic item is divided into sub-items (by dividing 
/// the item's points into sequential groups, each one is rendered like independent item).
/// 
/// Applicable to straight-segments' line/polygon item, text item and picture item only:
/// - Straight-segments' line item: divided into separate polylines.
/// - Straight-segments' polygon item: divided into separate one-contour or multi-contour polygons (e.g. polygons with  holes); each multi-contour 
///   polygon consists of a main sub-item with an ID other than #MC_EXTRA_CONTOUR_SUB_ITEM_ID (defining its first contour and its ID) followed 
///   by one or several sub-items with IDs equal to #MC_EXTRA_CONTOUR_SUB_ITEM_ID (defining its extra contours, e.g. holes).
/// - Text item: divided into groups of texts, each group has the same ID (\p uSubItemID) and use the same sub-text from SMcVariantString's array.
/// - Picture item: divided into groups of pictures, each group has the same ID (\p uSubItemID).
/// 
/// Each sub-item is based on the item's points with indices `nPointsStartIndex` through `nPointsStartIndex - 1` of the next sub-item 
/// (for the last sub-item: through the item's last point).
/// 
/// Used in:
/// - IMcSymbolicItem::SetSubItemsData(), IMcSymbolicItem::GetSubItemsData()), 
/// - IMcObjectScheme::SetArrayPropertyDefault(), IMcObjectScheme::GetArrayPropertyDefault(), IMcObjectScheme's functions using IMcProperty::SVariantProperty, 
/// - IMcObject::SetArrayProperty(), IMcObject::GetArrayProperty(), IMcObject's functions using IMcProperty::SVariantProperty.
struct SMcSubItemData
{
	/// The sub-item's optional ID (use #MC_EMPTY_ID if unnecessary, use #MC_EXTRA_CONTOUR_SUB_ITEM_ID for multi-contour polygons) that can be used for 
	/// controlling the visibility of sub-items for the whole overlay (IMcOverlay::SetSubItemsVisibility()) and for identificating sub-items returned 
	/// by IMcSpatialQueries::ScanInGeometry(). 
	UINT uSubItemID;

	/// An index of the sub-item's first point (negative number means indexing from the end:
	///  -1 for the last point, -2 for the point before the last one, etc.)
	int nPointsStartIndex;

	/// \n
	SMcSubItemData(UINT _uSubItemID = 0, int _nPointsStartIndex = 0)
		: uSubItemID(_uSubItemID), nPointsStartIndex(_nPointsStartIndex) {}
	/// \n
	SMcSubItemData(const SMcSubItemData &Other) { *this = Other; }
	/// \n
	SMcSubItemData& operator=(const SMcSubItemData &Other)
	{
		uSubItemID = Other.uSubItemID;
		nPointsStartIndex = Other.nPointsStartIndex;

		return *this;
	}
};

//==================================================================================
// Interface Name: IMcUserData
//----------------------------------------------------------------------------------
/// The base interface for user data. 
///
/// Used as a base interface for user-defined user data classes.
/// Must be inherited in order to define user data class.
//==================================================================================
class IMcUserData 
#ifdef __EMSCRIPTEN__
	: public IMcEmscriptenCallbackRef
#endif
{
public:

	virtual ~IMcUserData() {}

	//==============================================================================
	// Method Name: Release()
	//------------------------------------------------------------------------------
	/// A callback that should release user data instance.
	///
	///	To be implemented by the user to delete user data when its container is been removed.
	//==============================================================================
	virtual void Release() = 0;

	//==============================================================================
	// Method Name: Clone()
	//------------------------------------------------------------------------------
	/// A callback that should clone user data.
	///
	///	To be implemented by the user. Called when the user data's container is cloned to set
	/// the clone's user data. Default implementation returns NULL - no user data for the clone.
	///
	/// \return
	///     - User data for the clone.
	//==============================================================================
	virtual IMcUserData* Clone() { return NULL; }

	//================================================================
    // Method Name: GetSaveBufferSize()
    //----------------------------------------------------------------
    /// A callback that should return the memory buffer size needed to save the data.
	///
	///	Default implementation returns 0 - user data is not saved.
	///
    /// \return 
	///		- The needed buffer size (zero means the data should not be saved when its 
	///		  container is saved)
    ///
    //================================================================
	virtual UINT GetSaveBufferSize() { return 0; }

	//==============================================================================
	// Method Name: IsSavedBufferUTF8Bytes()
	//------------------------------------------------------------------------------
	/// A callback that should return true if the saved buffer is an array of valid UTF8-encoded non-zero bytes and 
	/// should be saved into textual files as a UTF8 string.
	///
	/// \return
	///     - whether the saved buffer is an array of valid UTF8-encoded non-zero bytes and 
	///		  should be saved into textual files as a UTF8 string.
	//==============================================================================
	virtual bool IsSavedBufferUTF8Bytes() { return false; }

    //================================================================
    // Method Name: SaveToBuffer()
    //----------------------------------------------------------------
    /// A callback that should save the data to a memory buffer.
	///
	///	Default implementation does nothing - user data is not saved.
	///
	/// \param[out]	aBuffer			The buffer to save to (the buffer size is equal or greater 
	///								of the return value of GetSaveBufferSize())
	//================================================================
	virtual void SaveToBuffer(void *aBuffer) { (void)aBuffer; }
};

//==================================================================================
// Interface Name: IMcUserDataFactory
//----------------------------------------------------------------------------------
/// The base interface for user data factory. 
/// 
/// Used as a base interface for user-defined user data factory.
/// Must be inherited in order to define user data factory.
//==================================================================================
class IMcUserDataFactory
#ifdef __EMSCRIPTEN__
	: public IMcEmscriptenCallbackRef
#endif
{
public:
	virtual ~IMcUserDataFactory() {}

	//================================================================
    // Method Name: CreateUserData()
    //----------------------------------------------------------------
    /// A callback that should create user data class instance by loading it from memory buffer
	///
    /// \param[in]	aBuffer				The buffer to load from
	/// \param[in]	uBufferSize			The buffer size
	///
    /// \return 
	///		- user data class instance created 
    //================================================================
	virtual IMcUserData* CreateUserData(void *aBuffer, UINT uBufferSize) = 0;
};

//===========================================================================
// Interface Name: IMcProgressCallback
//---------------------------------------------------------------------------
/// Interface for (long) procedure progress notifications
//===========================================================================
class IMcProgressCallback
#ifdef __EMSCRIPTEN__
	: public IMcEmscriptenCallbackRef
#endif
{

public:

	/// Type of progress message
	enum EProgressMessageType
	{
		EPMT_NEW,          ///< new progress message (new line)
		EPMT_ADD,          ///< progress message continuing the previous one (add to current line)
	};

	virtual ~IMcProgressCallback() {}

	//==============================================================================
	// Method Name: OnProgressMessage()
	//------------------------------------------------------------------------------
	/// Callback function used to pass progress messages to the user application
	///
	/// \param[in]	strMessage		Message string
	/// \param[in]	eMessageType	Message type (see #EProgressMessageType)
	//==============================================================================
	virtual void OnProgressMessage(PCSTR strMessage, EProgressMessageType eMessageType) = 0;

	//==============================================================================
	// Method Name: OnFileError()
	//------------------------------------------------------------------------------
	/// Callback function used to deal with file read/write errors.
	/// 
	/// The recommended behavior options:
	/// - wait some time and return true (do it several times until try counter reaches 
	///   some maximal value)
	/// - show Retry/Cancel message box and give the user an opportunity to decide what to do
	/// - a combination of two previous options: show Retry/Cancel message box after several 
	///   automatic tries
	/// - return false to cancel the whole conversion
	///
	/// \param[in]	strFilePath		File name with path
	/// \param[in]	bRead			Whether it is read or write error
	/// \param[in]	uTryCounter		a counter of tries of the same read/write operation
	///								(zero based)
	/// \return
	///     - true to retry read/write operation, false to cancel the whole conversion
	//==============================================================================
	virtual bool OnFileError(PCSTR strFilePath, bool bRead, UINT uTryCounter) = 0;

	//==============================================================================
	// Method Name: Release()
	//------------------------------------------------------------------------------
	/// A callback that should release callback class instance.
	///
	///	Can be implemented by the user to optionally delete callback class instance when 
	/// IMcMapProduction instance is been removed.
	//==============================================================================
	virtual void Release() {}
};

/// \name MapCore initialization (JavaScript API only)
//@{
#ifdef JAVA_SCRIPT_API_IN_WRAPPER

//==============================================================================
// Method Name: SetStartCallbackFunction()
//------------------------------------------------------------------------------
/// Sets the user's callback function to be called after all the code and the specified optional components have been loaded and defines those 
/// components (JavaScript API only).
/// 
/// \param[in]	CallbackFunction				The user's callback function to be called after all the code and the optional components have been loaded.
/// \param[in]	uOptionalComponentsBitField		The optional components' bit field based on #EMcOptionalComponentFlags; 
///												the default is EMcOptionalComponentFlags.EOCF_NONE.value; when running under node.js and/or running 
///												MapCore_Calculations versions, the default only should be used.
//==============================================================================
void SetStartCallbackFunction(void *CallbackFunction, UINT uOptionalComponentsBitField = EMcOptionalComponentFlags.EOCF_NONE.value);

#endif
//@}
