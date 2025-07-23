#pragma once

//===========================================================================
/// \file SMcColor.h
/// SMcBColor, SMcFColor, SMcAttenuation structures
//===========================================================================

#include <float.h>
#include <stdlib.h>

inline BYTE FloatToByteLimits(float f)
{
	return BYTE(__min(__max(f > 0 ? f + 0.5f : f - 0.5f, 0.f),255.f));
}

struct SMcFColor;

//===========================================================================
// SMcBColor struct
//---------------------------------------------------------------------------
///	RGBA color with byte components (all functions are inline).
/// 
//===========================================================================
struct SMcBColor
{
	/// Union: Two access ways
	union
	{
		/// One 4-byte number in the following order: Red, Green, Blue, Alpha
		DWORD dwRGBA;
		/// 4 separate bytes
		struct
		{
			BYTE	
					r,	///< Red component
					g,	///< Green component 
					b,	///< Blue component
					a;	///< Alpha component
		};
	};

	/// \name Construction & Assignment
	//@{
	SMcBColor() {}
	SMcBColor(const SMcBColor &Color);
	explicit SMcBColor(DWORD _dwRGBA);
	SMcBColor(BYTE bR, BYTE bG, BYTE bB, BYTE bA);
	//SMcBColor(const BYTE bRGBA[4]);
	SMcBColor(const SMcFColor &Color);

	SMcBColor& operator = (const SMcBColor &Color);
	SMcBColor& operator = (DWORD _dwRGBA);
	SMcBColor& operator = (const SMcFColor &Color);
	void SetBGRA(DWORD dwBGRA);
	//@}

	/// \name Get functions
	//@{
	DWORD GetBGRA() const;
	BYTE* GetComponentArray();
	const BYTE* GetComponentArray() const;
	COLORREF GetCOLORREF() const;
	//@}

	/// \name Assignment Operators
	//@{
	/// \n
	SMcBColor& operator += (const SMcBColor &Color);
	/// \n
	SMcBColor& operator -= (const SMcBColor &Color);
	/// Calculate the average for each color channel
	SMcBColor& operator |= (const SMcBColor &Color); // average
	/// \n
	SMcBColor& operator *= (const SMcBColor &Color);
	/// \n
	SMcBColor& operator /= (const SMcBColor &Color);
	/// \n
	SMcBColor& operator *= (float f);
	/// \n
	SMcBColor& operator /= (float f);
	//@}

	/// \name Binary Operators
	//@{
	/// \n
	SMcBColor operator + (const SMcBColor &Color) const;
	/// \n
	SMcBColor operator - (const SMcBColor &Color) const;
	/// Calculate the average for each color channel
	SMcBColor operator | (const SMcBColor &Color) const; // average
	/// \n
	SMcBColor operator * (const SMcBColor &Color) const;
	/// \n
	SMcBColor operator / (const SMcBColor &Color) const;
	/// \n
	SMcBColor operator * (float f) const;
	/// \n
	SMcBColor operator / (float f) const;

	/// \n
	friend SMcBColor operator*(float f, const SMcBColor &Color);
	//@}

	/// \name Compare Operators
	//@{
	bool operator == (const SMcBColor &cf) const;
	bool operator != (const SMcBColor &cf) const;
	//@}
};

const SMcBColor

	/// Black transparent color
	bcBlackTransparent	(0,   0,   0,   0  ), 

	/// Black opaque color
	bcBlackOpaque		(0  , 0  , 0  , 255),

	/// White transparent color
	bcWhiteTransparent	(255, 255, 255, 0  ),	

	/// White opaque color
	bcWhiteOpaque		(255, 255, 255, 255);

//===========================================================================
// SMcFColor struct
//---------------------------------------------------------------------------
///	RGBA color with float components (all functions are inline).
/// 
//===========================================================================
struct SMcFColor
{
    float	r,	///< Red component
			g,	///< Green component
			b,	///< Blue component
			a;	///< Alpha component

	/// \name Construction & Assignment
	//@{
	SMcFColor() {}
	SMcFColor(float fR, float fG, float fB, float fA);
	//SMcFColor(const float f[4]);
	SMcFColor(const SMcBColor &Color);
	SMcFColor(const SMcFColor &Color);

	//SMcFColor& operator = (const float f[4]);
	SMcFColor& operator = (const SMcBColor &Color);
	SMcFColor& operator = (const SMcFColor &Color);
	//@}

//	/// \name Casting to float array
//	//@{
//	/// \n
//	operator float* ();
//	/// \n
//	operator const float* () const;
//	//@}

	/// \name Assignment Operators
	//@{
	/// \n
	SMcFColor& operator += (const SMcFColor &Color);
	/// \n
	SMcFColor& operator -= (const SMcFColor &Color);
	/// Calculate the average for each color channel
	SMcFColor& operator |= (const SMcFColor &Color); // average
	/// \n
	SMcFColor& operator *= (const SMcFColor &Color);
	/// \n
	SMcFColor& operator /= (const SMcFColor &Color);
	/// \n
	SMcFColor& operator *= (float f);
	/// \n
	SMcFColor& operator /= (float f);
	//@}

	/// \name Unary Operators
	//@{
	/// \n
	SMcFColor operator + () const;
	/// \n
	SMcFColor operator - () const;
	//@}

	/// \name Binary Operators
	//@{
	/// \n
	SMcFColor operator + (const SMcFColor &Color) const;
	/// \n
	SMcFColor operator - (const SMcFColor &Color) const;
	/// Calculate the average for each color channel
	SMcFColor operator | (const SMcFColor &Color) const; // average
	/// \n
	SMcFColor operator * (const SMcFColor &Color) const;
	/// \n
	SMcFColor operator / (const SMcFColor &Color) const;
	/// \n
	SMcFColor operator * (float f) const;
	/// \n
	SMcFColor operator / (float f) const;

	/// \n
	friend SMcFColor operator*(float f, const SMcFColor &cf);
	//@}

	/// \name Compare Operators
	//@{
	bool operator == (const SMcFColor &Color) const;
	bool operator != (const SMcFColor &Color) const;
	//@}
};

const SMcFColor 

	/// Black transparent color
	fcBlackTransparent	(0.f, 0.f, 0.f, 0.f), 

	/// Black opaque color
	fcBlackOpaque		(0.f, 0.f, 0.f, 1.f),

	/// White transparent color
	fcWhiteTransparent	(1.f, 1.f, 1.f, 0.f),	

	/// White opaque color
	fcWhiteOpaque		(1.f, 1.f, 1.f, 1.f);


//////////////////////////////////////////////////////////////////////////
/// Light source attenuation.
///
/// Controls how a light source's intensity decreases with the distance from it.
///
/// Attenuation = 1 / ( fConst + fLinear * d + fSquare * (d * d)),
/// where d is the distance from a light source.
///
/// \note all three attenuation coefficients should not be set to 0 at the same time.

struct SMcAttenuation
{
	float	fConst,	///< Constant coefficient (0 through FLT_MAX)
			fLinear,///< Linear coefficient (0 through FLT_MAX)
			fSquare;///< Square coefficient (0 through FLT_MAX)
	float	fRange;	///< The range

	/// \name Construction & Assignment
	//@{
	SMcAttenuation() {}
	SMcAttenuation(const SMcAttenuation &a);
	SMcAttenuation(float _fConst, float _fLinear, float _fSquare, float fRange);

	SMcAttenuation& operator = (const SMcAttenuation &a);
	//@}

	/// \name Compare Operators
	//@{
	bool operator == (const SMcAttenuation &a) const;
	bool operator != (const SMcAttenuation &a) const;
	//@}
};

const SMcAttenuation

	/// Light intensity doesn't change with the distance from a light source
	aNoAttenuation(1, 0, 0, FLT_MAX),

	/// Light intensity changes as 1 / d, where d is the distance from a light source
	aLinearAttenuation(0, 1, 0, FLT_MAX),

	/// Light intensity changes as 1 / (d * d), where d is the distance from a light source
	aSquareAttenuation(0, 0, 1, FLT_MAX);

//////////////////////////////////////////////////////////////////////////
// struct SMcBColor: inline implementation

// construction and assignment
inline SMcBColor::SMcBColor(const SMcBColor &Color) { dwRGBA=Color.dwRGBA; }
inline SMcBColor::SMcBColor(DWORD _dwRGBA) { dwRGBA=_dwRGBA; }
inline SMcBColor::SMcBColor(BYTE bR, BYTE bG, BYTE bB, BYTE bA) { b=bB; g=bG; r=bR; a=bA; }
//inline SMcBColor::SMcBColor(const BYTE bRGBA[4]) { dwRGBA = *PDWORD(bRGBA); }
inline SMcBColor::SMcBColor(const SMcFColor &Color)
{
	r = FloatToByteLimits(255.f*Color.r);
	g = FloatToByteLimits(255.f*Color.g);
	b = FloatToByteLimits(255.f*Color.b);
	a = FloatToByteLimits(255.f*Color.a);
}

inline SMcBColor& SMcBColor::operator = (const SMcBColor &Color) { dwRGBA=Color.dwRGBA; return *this; }
inline SMcBColor& SMcBColor::operator = (DWORD _dwRGBA) { dwRGBA = _dwRGBA; return *this; }
inline SMcBColor& SMcBColor::operator = (const SMcFColor &Color)
{
	r = FloatToByteLimits(255.f*Color.r);
	g = FloatToByteLimits(255.f*Color.g);
	b = FloatToByteLimits(255.f*Color.b);
	a = FloatToByteLimits(255.f*Color.a);
	return *this;
}

inline void SMcBColor::SetBGRA(DWORD dwBGRA)
{
	dwRGBA = dwBGRA;
	BYTE uB = b;
	b = r;
	r = uB;
}

// Get functions
inline DWORD SMcBColor::GetBGRA() const
{
	return (dwRGBA & 0xFF00FF00) | b | (r <<16);
}
inline BYTE* SMcBColor::GetComponentArray() { return &r; }
inline const BYTE* SMcBColor::GetComponentArray() const { return &r; }
inline COLORREF SMcBColor::GetCOLORREF() const { return (dwRGBA&0x00FFFFFF); }

// assignment operators
inline SMcBColor& SMcBColor::operator += (const SMcBColor &Color)
{
	r += Color.r;
	g += Color.g;
	b += Color.b;
	a += Color.a;
	return *this;
}

inline SMcBColor& SMcBColor::operator -= (const SMcBColor &Color)
{
	r -= Color.r;
	g -= Color.g;
	b -= Color.b;
	a -= Color.a;
	return *this;
}

inline SMcBColor& SMcBColor::operator |= (const SMcBColor &Color) // average
{
	r = (UINT(r) + Color.r)/2;
	g = (UINT(g) + Color.g)/2;
	b = (UINT(b) + Color.b)/2;
	a = (UINT(a) + Color.a)/2;
	return *this;
}

inline SMcBColor& SMcBColor::operator *= (const SMcBColor &Color)
{
	r *= Color.r;
	g *= Color.g;
	b *= Color.b;
	a *= Color.a;
	return *this;
}

inline SMcBColor& SMcBColor::operator /= (const SMcBColor &Color)
{
	r /= Color.r;
	g /= Color.g;
	b /= Color.b;
	a /= Color.a;
	return *this;
}

inline SMcBColor& SMcBColor::operator *= (float f)
{
	r = FloatToByteLimits(r*f);
	g = FloatToByteLimits(g*f);
	b = FloatToByteLimits(b*f);
	a = FloatToByteLimits(a*f);
	return *this;
}

inline SMcBColor& SMcBColor::operator /= (float f)
{
	float fInv = 1.0f/f;
	r = FloatToByteLimits(r*fInv);
	g = FloatToByteLimits(g*fInv);
	b = FloatToByteLimits(b*fInv);
	a = FloatToByteLimits(a*fInv);
	return *this;
}

// binary operators
inline SMcBColor SMcBColor::operator + (const SMcBColor &Color) const
{
	return SMcBColor(r + Color.r, g + Color.g, b + Color.b, a + Color.a);
}

inline SMcBColor SMcBColor::operator - (const SMcBColor &Color) const
{
	return SMcBColor(r - Color.r, g - Color.g, b - Color.b, a - Color.a);
}

inline SMcBColor SMcBColor::operator | (const SMcBColor &Color) const // average
{
	return SMcBColor((UINT(r) + Color.r)/2, (UINT(g) + Color.g)/2, (UINT(b) + Color.b)/2, 
					 (UINT(a) + Color.a)/2);
}

inline SMcBColor SMcBColor::operator * (const SMcBColor &Color) const
{
	return SMcBColor(r*Color.r, g*Color.g, b*Color.b, a*Color.a);
}

inline SMcBColor SMcBColor::operator / (const SMcBColor &Color) const
{
	return SMcBColor(r/Color.r, g/Color.g, b/Color.b, a/Color.a);
}

inline SMcBColor SMcBColor::operator * (float f) const
{
	return SMcBColor(FloatToByteLimits(r*f), FloatToByteLimits(g*f), FloatToByteLimits(b*f), 
					 FloatToByteLimits(a*f));
}

inline SMcBColor SMcBColor::operator / (float f) const
{
	float fInv = 1.0f/f;
	return SMcBColor(FloatToByteLimits(r*fInv), FloatToByteLimits(g*fInv), 
					 FloatToByteLimits(b*fInv), FloatToByteLimits(a*fInv));
}

inline SMcBColor operator * (float f, const SMcBColor &Color)
{
	return SMcBColor(FloatToByteLimits(f*Color.r), FloatToByteLimits(f*Color.g), 
					 FloatToByteLimits(f*Color.b), FloatToByteLimits(f*Color.a));
}


inline bool SMcBColor::operator == (const SMcBColor &Color) const
{
	return (r == Color.r && g == Color.g && b == Color.b && a == Color.a);
}

inline bool SMcBColor::operator != (const SMcBColor &Color) const
{
	return (r != Color.r || g != Color.g || b != Color.b || a != Color.a);
}


//////////////////////////////////////////////////////////////////////////
// struct SMcFColor: inline implementation

// construction and assignment
inline SMcFColor::SMcFColor(float fR, float fG, float fB, float fA) { r = fR; g = fG; b = fB; a = fA; }
//inline SMcFColor::SMcFColor(const float f[4]) { r = f[0]; g = f[1]; b = f[2]; a = f[3]; }
inline SMcFColor::SMcFColor(const SMcFColor &Color) { r = Color.r; g = Color.g; b = Color.b; a = Color.a; }
inline SMcFColor::SMcFColor(const SMcBColor &Color)
{
	static const float f = 1.0f/255.0f;
	r = f*Color.r;
	g = f*Color.g;
	b = f*Color.b;
	a = f*Color.a;
}

//inline SMcFColor& SMcFColor::operator = (const float f[4])
//{
//	r = f[0];
//	g = f[1];
//	b = f[2];
//	a = f[3];
//	return *this;
//}

inline SMcFColor& SMcFColor::operator = (const SMcFColor &Color)
{
	r = Color.r;
	g = Color.g;
	b = Color.b;
	a = Color.a;
	return *this;
}

inline SMcFColor& SMcFColor::operator = (const SMcBColor &Color)
{
	static const float f = 1.0f/255.0f;
	r = f*Color.r;
	g = f*Color.g;
	b = f*Color.b;
	a = f*Color.a;
	return *this;
}


// casting to float array
//inline SMcFColor::operator float*() { return (float *) &r; }
//inline SMcFColor::operator const float*() const { return (const float *) &r; }

// assignment operators
inline SMcFColor& SMcFColor::operator += (const SMcFColor &Color)
{
	r += Color.r;
	g += Color.g;
	b += Color.b;
	a += Color.a;
	return *this;
}

inline SMcFColor& SMcFColor::operator -= (const SMcFColor &Color)
{
	r -= Color.r;
	g -= Color.g;
	b -= Color.b;
	a -= Color.a;
	return *this;
}

inline SMcFColor& SMcFColor::operator |= (const SMcFColor &Color) // average
{
	r = (r + Color.r)/2.f;
	g = (g + Color.g)/2.f;
	b = (b + Color.b)/2.f;
	a = (a + Color.a)/2.f;
	return *this;
}

inline SMcFColor& SMcFColor::operator *= (const SMcFColor &Color)
{
	r *= Color.r;
	g *= Color.g;
	b *= Color.b;
	a *= Color.a;
	return *this;
}

inline SMcFColor& SMcFColor::operator /= (const SMcFColor &Color)
{
	r /= Color.r;
	g /= Color.g;
	b /= Color.b;
	a /= Color.a;
	return *this;
}

inline SMcFColor& SMcFColor::operator *= (float f)
{
	r *= f;
	g *= f;
	b *= f;
	a *= f;
	return *this;
}

inline SMcFColor& SMcFColor::operator /= (float f)
{
	float fInv = 1.0f/f;
	r *= fInv;
	g *= fInv;
	b *= fInv;
	a *= fInv;
	return *this;
}


// unary operators
inline SMcFColor SMcFColor::operator + () const
{
	return *this;
}

inline SMcFColor SMcFColor::operator - () const
{
	return SMcFColor(-r, -g, -b, -a);
}

// binary operators
inline SMcFColor SMcFColor::operator + (const SMcFColor &Color) const
{
	return SMcFColor(r + Color.r, g + Color.g, b + Color.b, a + Color.a);
}

inline SMcFColor SMcFColor::operator - (const SMcFColor &Color) const
{
	return SMcFColor(r - Color.r, g - Color.g, b - Color.b, a - Color.a);
}

inline SMcFColor SMcFColor::operator | (const SMcFColor &Color) const // average
{
	return SMcFColor((r + Color.r)/2.f, (g + Color.g)/2.f, (b + Color.b)/2.f, (a + Color.a)/2.f);
}

inline SMcFColor SMcFColor::operator * (const SMcFColor &Color) const
{
	return SMcFColor(r*Color.r, g*Color.g, b*Color.b, a*Color.a);
}

inline SMcFColor SMcFColor::operator / (const SMcFColor &Color) const
{
	return SMcFColor(r/Color.r, g/Color.g, b/Color.b, a/Color.a);
}

inline SMcFColor SMcFColor::operator * (float f) const
{
	return SMcFColor(r*f, g*f, b*f, a*f);
}

inline SMcFColor SMcFColor::operator / (float f) const
{
	float fInv = 1.0f/f;
	return SMcFColor(r*fInv, g*fInv, b*fInv, a*fInv);
}

inline SMcFColor operator*(float f, const SMcFColor &Color)
{
	return SMcFColor(f*Color.r, f*Color.g, f*Color.b, f*Color.a);
}


inline bool SMcFColor::operator == (const SMcFColor &Color) const
{
	return (r == Color.r && g == Color.g && b == Color.b && a == Color.a);
}

inline bool SMcFColor::operator != (const SMcFColor &Color) const
{
	return (r != Color.r || g != Color.g || b != Color.b || a != Color.a);
}


//////////////////////////////////////////////////////////////////////////
// struct SMcAttenuation: inline implementation

// construction and assignment

inline SMcAttenuation::SMcAttenuation(const SMcAttenuation &a)
{ 
	fConst=a.fConst; fLinear=a.fLinear; fSquare=a.fSquare;
	fRange=a.fRange;
}

inline SMcAttenuation::SMcAttenuation(float _fConst, float _fLinear, float _fSquare, float _fRange)
{
	fConst =_fConst;
	fLinear=_fLinear;
	fSquare=_fSquare;
	fRange =_fRange;
}

inline SMcAttenuation& SMcAttenuation::operator = (const SMcAttenuation &a)
{
	fConst=a.fConst; fLinear=a.fLinear; fSquare=a.fSquare;
	fRange=a.fRange;

	return *this;
}

inline bool SMcAttenuation::operator == (const SMcAttenuation &a) const
{
	return (fConst == a.fConst && fLinear == a.fLinear && fSquare == a.fSquare && fRange == a.fRange);
}

inline bool SMcAttenuation::operator != (const SMcAttenuation &a) const
{
	return (fConst != a.fConst || fLinear != a.fLinear || fSquare != a.fSquare || fRange != a.fRange);
}
