//===========================================================================
/// \file SMcSizePointRect.h
/// SMcSize, SMcPoint, SMcRect structures
//===========================================================================
#pragma once

#ifdef _WIN32_WCE
//#include <cmnintrin.h>
#include <winuser.h>
#endif // _WIN32_WCE

//#ifndef _WIN32
//#include "CLinuxComp.h"
//#endif

#ifdef _MSC_VER
	// Disable warning messages C4800 : "forcing value to bool 'true' or 'false' (performance warning)"
	#pragma warning( disable : 4800 )
#endif

struct SMcSize;
struct SMcPoint;
struct SMcRect;

//===========================================================================
// SMcSize struct
//---------------------------------------------------------------------------
///	An extent derived from Windows SIZE structure (all functions are inline).
/// 
//===========================================================================
struct SMcSize : public SIZE
{
	/// \name Construction & Assignment
	//@{
	/// Construct an uninitialized size
	SMcSize();
	/// Create from two integers
	SMcSize(int initCX, int initCY);
	/// Create from another size
	SMcSize(const SMcSize &initSize);
	/// Create from another size
	SMcSize(SIZE initSize);
	/// Create from a point
	SMcSize(POINT initPt);

	/// Set from another size
	SMcSize& operator=(const SMcSize &size);
	/// Set from another size
	SMcSize& operator=(SIZE size);

	/// Set from two integers
	void SetSize(int CX, int CY);
	//@}

	/// \name Assignment Operators
	//@{
	void operator+=(SIZE size);
	void operator-=(SIZE size);
	//@}

	/// \name Compare Operators
	//@{
	bool operator==(SIZE size) const;
	bool operator!=(SIZE size) const;
	//@}

	/// \name Operators returning SMcSize values
	//@{
	SMcSize operator+(SIZE size) const;
	SMcSize operator-(SIZE size) const;
	SMcSize operator-() const;
	//@}

	/// \name Operators returning SMcPoint values
	//@{
	SMcPoint operator+(POINT point) const;
	SMcPoint operator-(POINT point) const;
	//@}

	/// \name Operators returning SMcRect values
	//@{
	SMcRect operator+(const RECT* lpRect) const;
	SMcRect operator-(const RECT* lpRect) const;
	//@}
};

//===========================================================================
// SMcPoint struct
//---------------------------------------------------------------------------
///	A 2-D point, derived from Windows POINT structure (all functions are inline).
/// 
//===========================================================================
struct SMcPoint : public POINT
{
public:
	/// \name Construction & Assignment
	//@{
	/// Create an uninitialized point
	SMcPoint();
	/// Create from two integers
	SMcPoint(int initX, int initY);
	/// Create from another point
	SMcPoint(const SMcPoint &initPt);
	/// Create from another point
	SMcPoint(POINT initPt);
	/// Create from a size
	SMcPoint(SIZE initSize);

	/// Set from another point
	SMcPoint& operator=(const SMcPoint &point);
	/// Set from another point
	SMcPoint& operator=(POINT point);

	/// Set from two integers
	void SetPoint(int X, int Y);
	//@}

	/// \name Offset
	//@{
	void Offset(int xOffset, int yOffset);
	void Offset(POINT point);
	void Offset(SIZE size);
	//@}

	/// \name Assignment Operators
	//@{
	void operator+=(SIZE size);
	void operator-=(SIZE size);
	void operator+=(POINT point);
	void operator-=(POINT point);
	//@}

	/// \name Compare Operators
	//@{
	bool operator==(POINT point) const;
	bool operator!=(POINT point) const;
	//@}

	/// \name Operators returning SMcPoint values
	//@{
	SMcPoint operator+(SIZE size) const;
	SMcPoint operator-(SIZE size) const;
	SMcPoint operator-() const;

	SMcPoint operator+(POINT point) const;
	//SMcPoint operator-(POINT point) const; // can't be defined cause there is already such operator overloaded, returning SMcSize...
	//@}

	/// \name Operators returning SMcSize values
	//@{
	//SMcSize operator+(POINT point) const; // can't be defined cause there is already such operator overloaded, returning SMcPoint...
	SMcSize operator-(POINT point) const;
	//@}

	/// \name Operators returning SMcRect values
	//@{
	SMcRect operator+(const RECT* lpRect) const;
	SMcRect operator-(const RECT* lpRect) const;
	//@}
};

//===========================================================================
// SMcRect struct
//---------------------------------------------------------------------------
///	A 2-D rectangle, derived from Windows RECT structure (all functions are inline).
/// 
//===========================================================================
struct SMcRect : public RECT
{
	/// \name Construction
	//@{
	/// Create uninitialized rectangle
	SMcRect();
	/// Create from left, top, right, and bottom
	SMcRect(int l, int t, int r, int b);
	/// Create from another rectangle
	SMcRect(const SMcRect& srcRect);
	/// Create from another rectangle
	SMcRect(const RECT& srcRect);
	/// Create from a pointer to another rect
	SMcRect(LPCRECT lpSrcRect);
	/// Create from a point and size
	SMcRect(POINT point, SIZE size);
	/// Create from two points
	SMcRect(POINT topLeft, POINT bottomRight);
	//@}

	/// \name Assignment
	//@{
	/// Set from another rectangle
	SMcRect& operator=(const SMcRect& srcRect);
	/// Set from another rectangle
	SMcRect& operator=(const RECT& srcRect);

	/// Set rectangle from left, top, right, and bottom
	void SetRect(int x1, int y1, int x2, int y2);
	/// Set rectangle from TopLeft and BottomRight
	void SetRect(POINT topLeft, POINT bottomRight);
	/// Empty the rectangle
	void SetRectEmpty();
	/// Copy from another rectangle
	void CopyRect(LPCRECT lpSrcRect);
	//@}

	/// \name Attributes (in addition to RECT members)
	//@{
	/// Returns the width
	int Width() const;
	/// Returns the height
	int Height() const;
	/// Returns the size
	SMcSize Size() const;
	/// Returns reference to the top-left point
	SMcPoint& TopLeft();
	/// Returns reference to the bottom-right point
	SMcPoint& BottomRight();
	/// Returns const reference to the top-left point
	const SMcPoint& TopLeft() const;
	/// Returns const reference to the bottom-right point
	const SMcPoint& BottomRight() const;
	/// Returns the geometric center point of the rectangle
	SMcPoint CenterPoint() const;
	/// Swap the left and right
	void SwapLeftRight();
	//@}

	/// \name Convert between SMcRect and LPRECT/LPCRECT (no need for &)
	//@{
	operator LPRECT();
	operator LPCRECT() const;
	//@}

	/// \name Check if rectangle is empty or intersecting
	//@{
	/// Returns TRUE if rectangle has no area
	bool IsRectEmpty() const;
	/// Returns TRUE if rectangle is at (0,0) and has no area
	bool ISMcRectNull() const;
	/// Returns TRUE if point is within rectangle
	bool PtInRect(POINT point) const;
	/// Returns TRUE if the rect is within the rectangle
	bool RectInRect(RECT rect) const;
	/// Returns TRUE if the requested rect is inside our rect
	bool Contains(RECT rect) const; 
	/// Returns TRUE if exactly the same as another rectangle
	bool EqualRect(LPCRECT lpRect) const;
	//@}

	/// \name Inflate
	//@{
	/// Inflate rectangle's width and height by
	/// x units to the left and right ends of the rectangle
	/// and y units to the top and bottom.
	void InflateRect(int x, int y);
	/// Inflate rectangle's width and height by
	/// size.cx units to the left and right ends of the rectangle
	/// and size.cy units to the top and bottom.
	void InflateRect(SIZE size);
	/// Inflate rectangle's width and height by moving individual sides.
	/// Left side is moved to the left, right side is moved to the right,
	/// top is moved up and bottom is moved down.
	void InflateRect(LPCRECT lpRect);
	/// Inflate rectangle's width and height by moving individual sides.
	/// Left side is moved to the left, right side is moved to the right,
	/// top is moved up and bottom is moved down.
	void InflateRect(int l, int t, int r, int b);
	//@}

	/// \name Deflate
	//@{
	/// Deflate the rectangle's width and height without
	/// moving its top or left
	void DeflateRect(int x, int y);
	/// \n
	void DeflateRect(SIZE size);
	/// \n
	void DeflateRect(LPCRECT lpRect);
	/// \n
	void DeflateRect(int l, int t, int r, int b);
	//@}

	/// \name Translate the rectangle by moving its top and left
	//@{
	void OffsetRect(int x, int y);
	/// \n
	void OffsetRect(SIZE size);
	/// \n
	void OffsetRect(POINT point);
	/// \n
	void NormalizeRect();
	//@}

	/// \name Absolute position of rectangle
	//@{
	/// \n
	void MoveToY(int y);
	/// \n
	void MoveToX(int x);
	/// \n
	void MoveToXY(int x, int y);
	/// \n
	void MoveToXY(POINT point);
	//@}

	/// \name Intersect, Union, Subtract rectangle
	//@{
	/// Set this rectangle to intersection of two others
	bool IntersectRect(LPCRECT lpRect1, LPCRECT lpRect2);
	/// Set this rectangle to bounding union of two others
	bool UnionRect(LPCRECT lpRect1, LPCRECT lpRect2);
	/// Set this rectangle to minimum of two others
	bool SubtractRect(LPCRECT lpRectSrc1, LPCRECT lpRectSrc2);
	//@}

	/// \name Assignment Operators
	//@{
	void operator+=(POINT point);
	void operator+=(SIZE size);
	void operator+=(LPCRECT lpRect);
	void operator-=(POINT point);
	void operator-=(SIZE size);
	void operator-=(LPCRECT lpRect);

	void operator&=(const RECT& rect);
	void operator|=(const RECT& rect);
	//@}

	/// \name Compare Operators
	//@{
	bool operator==(const RECT& rect) const;
	bool operator!=(const RECT& rect) const;
	//@}

	/// \name Operators returning SMcRect values
	//@{
	SMcRect operator+(POINT point) const;
	SMcRect operator+(SIZE size) const;
	SMcRect operator+(LPCRECT lpRect) const;
	SMcRect operator-(POINT point) const;
	SMcRect operator-(SIZE size) const;
	SMcRect operator-(LPCRECT lpRect) const;

	SMcRect operator&(const RECT& rect2) const;
	SMcRect operator|(const RECT& rect2) const;

	SMcRect MulDiv(int nMultiplier, int nDivisor) const;
	//@}
};

const SMcSize
	
	/// zero size
	szZero(0, 0), 
	
	/// one pixel size
	szOne(1, 1);

const SMcPoint 
	
	/// zero point
	pntZero(0, 0), 
	
	/// one pixel point
	pntOne(1, 1);
const SMcRect
	
	/// empty (all-zeros) rectangle
	rcZero(0, 0, 0, 0);

/////////////////////////////////////////////////////////////////////////////
// struct SMcSize inline implementation

inline SMcSize::SMcSize()
{ /* random filled */ }
inline SMcSize::SMcSize(int initCX, int initCY)
{ cx = initCX; cy = initCY; }
inline SMcSize::SMcSize(const SMcSize &initSize)
{ cx = initSize.cx; cy = initSize.cy; }
inline SMcSize::SMcSize(SIZE initSize)
{ cx = initSize.cx; cy = initSize.cy; }
inline SMcSize::SMcSize(POINT initPt)
{ cx = initPt.x; cy = initPt.y; }
inline SMcSize& SMcSize::operator=(const SMcSize &size)
{ cx = size.cx; cy = size.cy; return *this; }
inline SMcSize& SMcSize::operator=(SIZE size)
{ cx = size.cx; cy = size.cy; return *this; }
inline bool SMcSize::operator==(SIZE size) const
{ return (cx == size.cx && cy == size.cy); }
inline bool SMcSize::operator!=(SIZE size) const
{ return (cx != size.cx || cy != size.cy); }
inline void SMcSize::operator+=(SIZE size)
{ cx += size.cx; cy += size.cy; }
inline void SMcSize::operator-=(SIZE size)
{ cx -= size.cx; cy -= size.cy; }
inline void SMcSize::SetSize(int CX, int CY)
{ cx = CX; cy = CY; }
inline SMcSize SMcSize::operator+(SIZE size) const
{ return SMcSize(cx + size.cx, cy + size.cy); }
inline SMcSize SMcSize::operator-(SIZE size) const
{ return SMcSize(cx - size.cx, cy - size.cy); }
inline SMcSize SMcSize::operator-() const
{ return SMcSize(-cx, -cy); }
inline SMcPoint SMcSize::operator+(POINT point) const
{ return SMcPoint(cx + point.x, cy + point.y); }
inline SMcPoint SMcSize::operator-(POINT point) const
{ return SMcPoint(cx - point.x, cy - point.y); }
inline SMcRect SMcSize::operator+(const RECT* lpRect) const
{ return SMcRect(lpRect) + *this; }
inline SMcRect SMcSize::operator-(const RECT* lpRect) const
{ return SMcRect(lpRect) - *this; }

/////////////////////////////////////////////////////////////////////////////
// struct SMcPoint inline implementation

inline SMcPoint::SMcPoint()
{ /* random filled */ }
inline SMcPoint::SMcPoint(int initX, int initY)
{ x = initX; y = initY; }
inline SMcPoint::SMcPoint(const SMcPoint &initPt)
{ x = initPt.x; y = initPt.y; }
inline SMcPoint::SMcPoint(POINT initPt)
{ x = initPt.x; y = initPt.y; }
inline SMcPoint::SMcPoint(SIZE initSize)
{ x = initSize.cx; y = initSize.cy; }
inline void SMcPoint::Offset(int xOffset, int yOffset)
{ x += xOffset; y += yOffset; }
inline void SMcPoint::Offset(POINT point)
{ x += point.x; y += point.y; }
inline void SMcPoint::Offset(SIZE size)
{ x += size.cx; y += size.cy; }
inline void SMcPoint::SetPoint(int X, int Y)
{ x = X; y = Y; }
inline SMcPoint& SMcPoint::operator=(const SMcPoint &point)
{ x = point.x; y = point.y; return *this; }
inline SMcPoint& SMcPoint::operator=(POINT point)
{ x = point.x; y = point.y; return *this; }
inline bool SMcPoint::operator==(POINT point) const
{ return (x == point.x && y == point.y); }
inline bool SMcPoint::operator!=(POINT point) const
{ return (x != point.x || y != point.y); }
inline void SMcPoint::operator+=(SIZE size)
{ x += size.cx; y += size.cy; }
inline void SMcPoint::operator-=(SIZE size)
{ x -= size.cx; y -= size.cy; }
inline void SMcPoint::operator+=(POINT point)
{ x += point.x; y += point.y; }
inline void SMcPoint::operator-=(POINT point)
{ x -= point.x; y -= point.y; }
inline SMcPoint SMcPoint::operator+(SIZE size) const
{ return SMcPoint(x + size.cx, y + size.cy); }
inline SMcPoint SMcPoint::operator-(SIZE size) const
{ return SMcPoint(x - size.cx, y - size.cy); }
inline SMcPoint SMcPoint::operator-() const
{ return SMcPoint(-x, -y); }
inline SMcPoint SMcPoint::operator+(POINT point) const
{ return SMcPoint(x + point.x, y + point.y); }
//inline SMcPoint SMcPoint::operator-(POINT point) const
//{ return SMcPoint(x - point.x, y - point.y); }
//inline SMcSize SMcPoint::operator+(POINT point) const
//{ return SMcSize(x + point.x, y + point.y); }
inline SMcSize SMcPoint::operator-(POINT point) const
{ return SMcSize(x - point.x, y - point.y); }
inline SMcRect SMcPoint::operator+(const RECT* lpRect) const
{ return SMcRect(lpRect) + *this; }
inline SMcRect SMcPoint::operator-(const RECT* lpRect) const
{ return SMcRect(lpRect) - *this; }


/////////////////////////////////////////////////////////////////////////////
// struct SMcRect inline implementation
inline SMcRect::SMcRect()
{ /* random filled */ }
inline SMcRect::SMcRect(int l, int t, int r, int b)
{ left = l; top = t; right = r; bottom = b; }
inline SMcRect::SMcRect(const SMcRect& srcRect)
{ ::CopyRect(this, &srcRect); }
inline SMcRect::SMcRect(const RECT& srcRect)
{ ::CopyRect(this, &srcRect); }
inline SMcRect::SMcRect(LPCRECT lpSrcRect)
{ ::CopyRect(this, lpSrcRect); }
inline SMcRect::SMcRect(POINT point, SIZE size)
{ right = (left = point.x) + size.cx; bottom = (top = point.y) + size.cy; }
inline SMcRect::SMcRect(POINT topLeft, POINT bottomRight)
{ left = topLeft.x; top = topLeft.y; right = bottomRight.x; bottom = bottomRight.y; }
inline int SMcRect::Width() const
{ return right - left; }
inline int SMcRect::Height() const
{ return bottom - top; }
inline SMcSize SMcRect::Size() const
{ return SMcSize(right - left, bottom - top); }
inline SMcPoint& SMcRect::TopLeft()
{ return *((SMcPoint*)this); }
inline SMcPoint& SMcRect::BottomRight()
{ return *((SMcPoint*)this+1); }
inline const SMcPoint& SMcRect::TopLeft() const
{ return *((SMcPoint*)this); }
inline const SMcPoint& SMcRect::BottomRight() const
{ return *((SMcPoint*)this+1); }
inline SMcPoint SMcRect::CenterPoint() const
{ return SMcPoint((left+right)/2, (top+bottom)/2); }
inline void SMcRect::SwapLeftRight()
{ LONG temp = left; left = right; right = temp; }
inline SMcRect::operator LPRECT()
{ return this; }
inline SMcRect::operator LPCRECT() const
{ return this; }
inline bool SMcRect::IsRectEmpty() const
{ return ::IsRectEmpty(this); }
inline bool SMcRect::ISMcRectNull() const
{ return (left == 0 && right == 0 && top == 0 && bottom == 0); }
inline bool SMcRect::PtInRect(POINT point) const
{ return ::PtInRect(this, point); }
inline bool SMcRect::RectInRect(RECT rect) const
{
	return !((rect.right <= this->left) || 
             (rect.left >= this->right) ||
			 (rect.bottom <= this->top) || 
             (rect.top >= this->bottom));

}
inline bool SMcRect::Contains(RECT rect) const 
{ 
	return (rect.left >= this->left && 
			rect.right <= this->right &&
			rect.top >= this->top && 
			rect.bottom <= this->bottom); 
}
inline void SMcRect::SetRect(int x1, int y1, int x2, int y2)
{ ::SetRect(this, x1, y1, x2, y2); }
inline void SMcRect::SetRect(POINT topLeft, POINT bottomRight)
{ ::SetRect(this, topLeft.x, topLeft.y, bottomRight.x, bottomRight.y); }
inline void SMcRect::SetRectEmpty()
{ ::SetRectEmpty(this); }
inline void SMcRect::CopyRect(LPCRECT lpSrcRect)
{ ::CopyRect(this, lpSrcRect); }
inline bool SMcRect::EqualRect(LPCRECT lpRect) const
{ return ::EqualRect(this, lpRect); }
inline void SMcRect::InflateRect(int x, int y)
{ ::InflateRect(this, x, y); }
inline void SMcRect::InflateRect(SIZE size)
{ ::InflateRect(this, size.cx, size.cy); }
inline void SMcRect::DeflateRect(int x, int y)
{ ::InflateRect(this, -x, -y); }
inline void SMcRect::DeflateRect(SIZE size)
{ ::InflateRect(this, -size.cx, -size.cy); }
inline void SMcRect::OffsetRect(int x, int y)
{ ::OffsetRect(this, x, y); }
inline void SMcRect::OffsetRect(POINT point)
{ ::OffsetRect(this, point.x, point.y); }
inline void SMcRect::OffsetRect(SIZE size)
{ ::OffsetRect(this, size.cx, size.cy); }
inline void SMcRect::MoveToY(int y)
{ bottom = Height() + y; top = y; }
inline void SMcRect::MoveToX(int x)
{ right = Width() + x; left = x; }
inline void SMcRect::MoveToXY(int x, int y)
{ MoveToX(x); MoveToY(y); }
inline void SMcRect::MoveToXY(POINT pt)
{ MoveToX(pt.x); MoveToY(pt.y); }
inline bool SMcRect::IntersectRect(LPCRECT lpRect1, LPCRECT lpRect2)
{ return ::IntersectRect(this, lpRect1, lpRect2);}
inline bool SMcRect::UnionRect(LPCRECT lpRect1, LPCRECT lpRect2)
{ return ::UnionRect(this, lpRect1, lpRect2); }
inline SMcRect& SMcRect::operator=(const SMcRect& srcRect)
{
	::CopyRect(this, &srcRect);
	return *this;
}
inline SMcRect& SMcRect::operator=(const RECT& srcRect)
{
	::CopyRect(this, &srcRect);
	return *this;
}
inline bool SMcRect::operator==(const RECT& rect) const
{ return ::EqualRect(this, &rect); }
inline bool SMcRect::operator!=(const RECT& rect) const
{ return !::EqualRect(this, &rect); }
inline void SMcRect::operator+=(POINT point)
{ ::OffsetRect(this, point.x, point.y); }
inline void SMcRect::operator+=(SIZE size)
{ ::OffsetRect(this, size.cx, size.cy); }
inline void SMcRect::operator+=(LPCRECT lpRect)
{ InflateRect(lpRect); }
inline void SMcRect::operator-=(POINT point)
{ ::OffsetRect(this, -point.x, -point.y); }
inline void SMcRect::operator-=(SIZE size)
{ ::OffsetRect(this, -size.cx, -size.cy); }
inline void SMcRect::operator-=(LPCRECT lpRect)
{ DeflateRect(lpRect); }
inline void SMcRect::operator&=(const RECT& rect)
{ ::IntersectRect(this, this, &rect); }
inline void SMcRect::operator|=(const RECT& rect)
{ ::UnionRect(this, this, &rect); }
inline SMcRect SMcRect::operator+(POINT pt) const
{ SMcRect rect(*this); ::OffsetRect(&rect, pt.x, pt.y); return rect; }
inline SMcRect SMcRect::operator-(POINT pt) const
{ SMcRect rect(*this); ::OffsetRect(&rect, -pt.x, -pt.y); return rect; }
inline SMcRect SMcRect::operator+(SIZE size) const
{ SMcRect rect(*this); ::OffsetRect(&rect, size.cx, size.cy); return rect; }
inline SMcRect SMcRect::operator-(SIZE size) const
{ SMcRect rect(*this); ::OffsetRect(&rect, -size.cx, -size.cy); return rect; }
inline SMcRect SMcRect::operator+(LPCRECT lpRect) const
{ SMcRect rect(this); rect.InflateRect(lpRect); return rect; }
inline SMcRect SMcRect::operator-(LPCRECT lpRect) const
{ SMcRect rect(this); rect.DeflateRect(lpRect); return rect; }
inline SMcRect SMcRect::operator&(const RECT& rect2) const
{ SMcRect rect; ::IntersectRect(&rect, this, &rect2); return rect; }
inline SMcRect SMcRect::operator|(const RECT& rect2) const
{ SMcRect rect; ::UnionRect(&rect, this, &rect2); return rect; }
inline bool SMcRect::SubtractRect(LPCRECT lpRectSrc1, LPCRECT lpRectSrc2)
{ return ::SubtractRect(this, lpRectSrc1, lpRectSrc2); }

inline void SMcRect::NormalizeRect()
{
	int nTemp;
	if (left > right)
	{
		nTemp = left;
		left = right;
		right = nTemp;
	}
	if (top > bottom)
	{
		nTemp = top;
		top = bottom;
		bottom = nTemp;
	}
}

inline void SMcRect::InflateRect(LPCRECT lpRect)
{
	left -= lpRect->left;   top -= lpRect->top;
	right += lpRect->right; bottom += lpRect->bottom;
}

inline void SMcRect::InflateRect(int l, int t, int r, int b)
{
	left -= l;  top -= t;
	right += r; bottom += b;
}

inline void SMcRect::DeflateRect(LPCRECT lpRect)
{
	left += lpRect->left;   top += lpRect->top;
	right -= lpRect->right; bottom -= lpRect->bottom;
}

inline void SMcRect::DeflateRect(int l, int t, int r, int b)
{
	left += l;  top += t;
	right -= r; bottom -= b;
}

/*inline SMcRect SMcRect::MulDiv(int nMultiplier, int nDivisor) const
{
	return SMcRect(::MulDiv(left, nMultiplier, nDivisor), ::MulDiv(top, nMultiplier, nDivisor),
					::MulDiv(right, nMultiplier, nDivisor), ::MulDiv(bottom, nMultiplier, nDivisor));
}*/

#ifdef _MSC_VER
	// Enable warning messages C4800 : "forcing value to bool 'true' or 'false' (performance warning)"
	#pragma warning( default : 4800 )
#endif
