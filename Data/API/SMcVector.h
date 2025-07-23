//===========================================================================
/// \file SMcVector.h
/// SMcVector3D/SMcFVector3D, SMcVector2D/SMcFVector2D, SMcBox/SMcFBox structures
//===========================================================================

#pragma once

#include <float.h>
#include <math.h>
#include <stdlib.h>

#ifndef M_PI
	#define M_PI 3.14159265358979323846
#endif

#ifndef M_PI_2
	#define M_PI_2 1.57079632679489661923
#endif


#define MC_VECTOR_DOUBLE_RECIPROCAL_EPS	10000000
#define MC_VECTOR_FLOAT_RECIPROCAL_EPS	10000

#define MC_VECTOR_DOUBLE_EPS	(1.0  / MC_VECTOR_DOUBLE_RECIPROCAL_EPS)	// 0.0000001
#define MC_VECTOR_FLOAT_EPS		(1.0f / MC_VECTOR_FLOAT_RECIPROCAL_EPS)		// 0.0001

template<typename Type, int nReciprocalEps>
struct SMcVector2D_T;

//===========================================================================
// SMcVector3D_T struct
//---------------------------------------------------------------------------
///	3D vector with generic components.
/// 
//===========================================================================
template<typename Type, int nReciprocalEps>
struct SMcVector3D_T
{
	/// Coordinate X
	Type x;

	/// Coordinate Y
	Type y;

	/// Coordinate Z
	Type z;

	/// \name Construction & Assignment
	//@{
	/// \n
	inline SMcVector3D_T() {}
	/// \n
	inline SMcVector3D_T(const SMcVector3D_T &v) { x = Type(v.x); y = Type(v.y); z = Type(v.z); }
	/// \n
	//inline explicit SMcVector3D_T(const Type d[3]) { x = d[0]; y = d[1]; z = d[2]; }
	/// \n
	explicit SMcVector3D_T(const SMcVector2D_T<Type, nReciprocalEps> &v2);
	/// \n
	template<typename OtherType, int nOtherReciprocalEps>
	inline explicit SMcVector3D_T(const SMcVector3D_T<OtherType, nOtherReciprocalEps> &v)
		{ x = Type(v.x); y = Type(v.y); z = Type(v.z); }
	/// \n
	template<typename OtherType, int nOtherReciprocalEps>
	inline explicit SMcVector3D_T(const SMcVector2D_T<OtherType, nOtherReciprocalEps> &v)
		{ x = Type(v.x); y = Type(v.y); z = 0; }
	/// \n
	inline SMcVector3D_T(Type _x, Type _y, Type _z) { x = _x; y = _y; z=_z; }
	/// \n
	inline SMcVector3D_T& operator = (const SMcVector3D_T &v) { x = v.x; y = v.y; z = v.z; return *this; }
	//@}

//	/// \name Casting Operators
//	//@{
//	/// \n
//	inline operator Type* () { return (Type*)&x; }
//	/// \n
//	inline operator const Type* () const { return (const Type*)&x; }
//	//@}

	/// \name Compare Operators
	//@{
	/// \n
	inline bool operator == (const SMcVector3D_T &v) const { return (x == v.x && y == v.y && z == v.z); }
	/// \n
	inline bool operator != (const SMcVector3D_T &v) const { return (x != v.x || y != v.y || z != v.z); }
	//@}

	/// \name Assignment Operators
	//@{
	/// \n
	inline SMcVector3D_T& operator += (const SMcVector3D_T &v) { x += v.x; y += v.y; z += v.z; return *this; }
	/// \n
	inline SMcVector3D_T& operator -= (const SMcVector3D_T &v) { x -= v.x; y -= v.y; z -= v.z; return *this; }
	/// \n
	inline SMcVector3D_T& operator *= (Type d) { x *= d; y *= d; z *= d; return *this; }
	/// \n
	inline SMcVector3D_T& operator /= (Type d) { Type dInv = Type(1)/d; x *= dInv; y *= dInv; z *=dInv; return *this; }
	//@}

	/// \name Unary Operators
	//@{
	/// \n
	inline SMcVector3D_T operator + () const { return *this; }
	/// \n
	inline SMcVector3D_T operator - () const { return SMcVector3D_T(-x, -y, -z); }
	//@}

	/// \name Binary Operators
	//@{
	/// \n
	inline SMcVector3D_T operator + (const SMcVector3D_T &v) const { return SMcVector3D_T(x + v.x, y + v.y, z + v.z); }
	/// \n
	inline SMcVector3D_T operator - (const SMcVector3D_T &v) const { return SMcVector3D_T(x - v.x, y - v.y, z - v.z); }
	/// \n
	inline SMcVector3D_T operator + (Type d) const { return SMcVector3D_T(x + d, y + d, z + d); }
	/// \n
	inline SMcVector3D_T operator - (Type d) const { return SMcVector3D_T(x - d, y - d, z - d); }
	/// \n
	inline SMcVector3D_T operator * (Type d) const { return SMcVector3D_T(x*d, y*d, z*d); }
	/// \n
	inline SMcVector3D_T operator / (Type d) const { Type dInv = Type(1)/d; return SMcVector3D_T(x*dInv, y*dInv, z*dInv); }
	/// \n
	template<typename OtherType, int nOtherReciprocalEps>
 	friend SMcVector3D_T<OtherType, nOtherReciprocalEps> operator * (OtherType, const SMcVector3D_T<OtherType, nOtherReciprocalEps> &v);
	//@}

	/// \name Dot product
	//@{
	/// \n
	inline Type operator * (const SMcVector3D_T &v) const { return (x*v.x + y*v.y + z*v.z); }
	//@}

	/// \name Divide vector by vector (each component)
	//@{
	/// \n
	inline SMcVector3D_T operator / (const SMcVector3D_T &v) const { return SMcVector3D_T(x/v.x, y/v.y, z/v.z); }
	//@}

	/// \name Cross-product
	//@{
	/// \n
	inline SMcVector3D_T operator ^ (const SMcVector3D_T &v) const { return SMcVector3D_T(y*v.z - v.y*z, z*v.x - v.z*x, x*v.y - v.x*y); }
	/// \n
	inline SMcVector3D_T& operator ^= (const SMcVector3D_T &v) { return *this = SMcVector3D_T(y*v.z - v.y*z, z*v.x - v.z*x, x*v.y - v.x*y); }
	//@}

	/// \name Average
	//@{
	/// \n
	inline SMcVector3D_T operator | (const SMcVector3D_T &v) const { return SMcVector3D_T((x+v.x)/2., (y+v.y)/2., (z+v.z)/2.); }
	/// \n
	inline SMcVector3D_T& operator |= (const SMcVector3D_T &v) { x = (x+v.x)/2.; y = (y+v.y)/2.; z = (z+v.z)/2.; return *this; }
	//@}

	/// \name Square length and length
	//@{
	/// \n
	inline Type SquareLength() const { return x*x + y*y + z*z; }
	/// \n
	inline Type Length() const { return sqrt(x*x + y*y + z*z); }
	//@}

	/// \name Multiply and add
	//@{
	/// \n
	void MulAdd(Type dMul, const SMcVector3D_T &vAdd);
	/// \n
	SMcVector3D_T GetMulAdded(Type dMul, const SMcVector3D_T &vAdd) const;
	//@}

	/// \name Normalize
	//@{
	/// \n
	void Normalize();
	/// \n
	SMcVector3D_T GetNormalized() const;
	//@}

	/// \name Linear interpolation
	//@{
	/// \n
	SMcVector3D_T GetLinearInterpolationWith(const SMcVector3D_T &vSecond, Type dInterpolationParam) const;
	//@}

	/// \name Yaw, pitch, roll angles
	//@{
	/// \n
	void GetRadianYawPitchFromForwardVector(Type *pdYaw, Type *pdPitch) const;
	/// \n
	void GetDegreeYawPitchFromForwardVector(Type *pdYaw, Type *pdPitch) const;
	/// \n
	void GetRadianPitchRollFromUpVector(Type *pdPitch, Type *pdRoll) const;
	/// \n
	void GetDegreePitchRollFromUpVector(Type *pdPitch, Type *pdRoll) const;
	/// \n
	void RotateByRadianYawAngle(Type dYaw);
	/// \n
	void RotateByDegreeYawAngle(Type dYaw);
	/// \n
	void RotateByRadianYawPitchRoll(Type dYaw, Type dPitch, Type dRoll);
	/// \n
	void RotateByDegreeYawPitchRoll(Type dYaw, Type dPitch, Type dRoll);
	//@}
};

//////////////////////////////////////////////////////////////////////////
/// 3D vector with double components
typedef SMcVector3D_T<double, MC_VECTOR_DOUBLE_RECIPROCAL_EPS> SMcVector3D;

const SMcVector3D	

	/// Zero vector
	v3Zero(0.0, 0.0, 0.0),					

	/// All-DBL_MIN vector
	v3MinDouble(DBL_MIN, DBL_MIN, DBL_MIN),

	/// All-DBL_MAX vector
	v3MaxDouble(DBL_MAX, DBL_MAX, DBL_MAX);

//////////////////////////////////////////////////////////////////////////
/// 3D vector with float components
typedef SMcVector3D_T<float, MC_VECTOR_FLOAT_RECIPROCAL_EPS> SMcFVector3D;

const SMcFVector3D

	/// Zero vector
	vf3Zero(0.0f, 0.0f, 0.0f),					

	/// All-FLT_MIN vector
	vf3MinFloat(FLT_MIN, FLT_MIN, FLT_MIN),

	/// All-FLT_MAX vector
	vf3MaxFloat(FLT_MAX, FLT_MAX, FLT_MAX);

//===========================================================================
// SMcVector2D_T struct
//---------------------------------------------------------------------------
///	2D vector with generic components.
/// 
//===========================================================================
template<typename Type, int nReciprocalEps>
struct SMcVector2D_T
{
	/// Coordinate X
	Type x;

	/// Coordinate Y
	Type y;

	/// \name Construction & Assignment
	//@{
	/// \n
	inline SMcVector2D_T() {}
	/// \n
	inline SMcVector2D_T(const SMcVector2D_T &v) { x = v.x; y = v.y; }
	/// \n
	//inline explicit SMcVector2D_T(const Type d[2]) { x = d[0]; y = d[1]; }
	/// \n
	inline explicit SMcVector2D_T(const SMcVector3D_T<Type, nReciprocalEps> &v) { x = v.x; y = v.y; }
	/// \n
	template<typename OtherType, int nOtherReciprocalEps>
	inline explicit SMcVector2D_T(const SMcVector2D_T<OtherType, nOtherReciprocalEps> &v)
		{ x = Type(v.x); y = Type(v.y); }
	/// \n
	template<typename OtherType, int nOtherReciprocalEps>
	inline explicit SMcVector2D_T(const SMcVector3D_T<OtherType, nOtherReciprocalEps> &v)
		{ x = Type(v.x); y = Type(v.y); }
	/// \n
	inline SMcVector2D_T(Type _x, Type _y) { x = _x; y = _y; }
	/// \n
	inline SMcVector2D_T& operator = (const SMcVector2D_T &v) { x = v.x; y = v.y; return *this; }
	//@}

//	/// \name Casting Operators
//	//@{
//	/// \n
//	inline operator Type* () { return (Type*)&x; }
//	/// \n
//	inline operator const Type* () const { return (const Type*)&x; }
//	//@}

	/// \name Compare Operators
	//@{
	/// \n
	inline bool operator == (const SMcVector2D_T &v2) const { return (x == v2.x && y == v2.y); }
	/// \n
	inline bool operator != (const SMcVector2D_T &v2) const { return (x != v2.x || y != v2.y); }
	/// \n
	inline bool operator == (const SMcVector3D_T<Type, nReciprocalEps> &v) const { return (x == v.x && y == v.y); }
	/// \n
	inline bool operator != (const SMcVector3D_T<Type, nReciprocalEps> &v) const { return (x != v.x || y != v.y); }
	//@}

	/// \name Assignment Operators
	//@{
	/// \n
	inline SMcVector2D_T& operator += (const SMcVector2D_T &v2) { x += v2.x; y += v2.y; return *this; }
	/// \n
	inline SMcVector2D_T& operator -= (const SMcVector2D_T &v2) { x -= v2.x; y -= v2.y; return *this; }
	/// \n
	inline SMcVector2D_T& operator *= (Type d) { x *= d; y *= d; return *this; }
	/// \n
	inline SMcVector2D_T& operator /= (Type d) { Type dInv = Type(1)/d; x *= dInv; y *= dInv; return *this; }
	//@}

	/// \name Unary Operators
	//@{
	/// \n
	inline SMcVector2D_T operator + () const { return *this; }
	/// \n
	inline SMcVector2D_T operator - () const { return SMcVector2D_T(-x, -y); }
	//@}

	/// \name Binary Operators
	//@{
	/// \n
	inline SMcVector2D_T operator + (const SMcVector2D_T &v2) const { return SMcVector2D_T(x + v2.x, y + v2.y); }
	/// \n
	inline SMcVector2D_T operator - (const SMcVector2D_T &v2) const { return SMcVector2D_T(x - v2.x, y - v2.y); }
	/// \n
	inline SMcVector2D_T operator * (Type d) const { return SMcVector2D_T(x*d, y*d); }
	/// \n
	inline SMcVector2D_T operator / (Type d) const { Type dInv = Type(1)/d; return SMcVector2D_T(x*dInv, y*dInv); }
	/// \n
	template<typename OtherType, int nOtherReciprocalEps>
 	friend SMcVector2D_T<OtherType, nOtherReciprocalEps> operator * (OtherType, const SMcVector2D_T<OtherType, nOtherReciprocalEps> &v2);
	//@}

	/// \name Dot product
	//@{
	/// \n
	inline Type operator * (const SMcVector2D_T &v2) const { return (x*v2.x + y*v2.y); }
	//@}

	/// \name Divide vector by vector (each component)
	//@{
	/// \n
	inline SMcVector2D_T operator / (const SMcVector2D_T &v2) const { return SMcVector2D_T(x/v2.x, y/v2.y); }
	//@}

	/// \name Cross-product's z
	//@{
	/// \n
	inline Type operator ^ (const SMcVector2D_T &v2) const { return x*v2.y - v2.x*y; }
	//@}

	/// \name Average
	//@{
	/// \n
	inline SMcVector2D_T operator | (const SMcVector2D_T &v2) const { return SMcVector2D_T((x+v2.x)/2., (y+v2.y)/2.); }
	/// \n
	inline SMcVector2D_T& operator |= (const SMcVector2D_T &v2 ) { x = (x+v2.x)/2.; y = (y+v2.y)/2.; return *this; }
	//@}

	/// \name Square length and length
	//@{
	/// \n
	inline Type SquareLength() const { return x*x + y*y; }
	/// \n
	inline Type Length() const { return sqrt(x*x + y*y); }
	//@}

	/// \name Yaw Angle
	//@{
	/// \n
	inline Type GetYawAngleRadians() const 
	{
		return (fabs(x) < (Type(1)/nReciprocalEps) && fabs(y) < (Type(1)/nReciprocalEps) ? 
				0.0 : atan2(x, y));
	}
	/// \n
	inline Type GetYawAngleDegrees() const { return GetYawAngleRadians() * 180.0 / M_PI; }
	/// \n
	void RotateByRadianYawAngle(Type dYaw);
	/// \n
	void RotateByDegreeYawAngle(Type dYaw);
	//@}

	/// \name Multiply and add
	//@{
	/// \n
	void MulAdd(Type dMul, const SMcVector2D_T &v2Add);
	/// \n
	SMcVector2D_T GetMulAdded(Type dMul, const SMcVector2D_T &v2Add) const;
	//@}

	/// \name Normalize
	//@{
	/// \n
	void Normalize();
	/// \n
	SMcVector2D_T GetNormalized() const;
	//@}

	/// \name Linear interpolation
	//@{
	/// \n
	SMcVector2D_T GetLinearInterpolationWith(const SMcVector2D_T &vSecond, Type dInterpolationParam) const;
	//@}
};

//////////////////////////////////////////////////////////////////////////
/// 2D vector with double components
typedef SMcVector2D_T<double, MC_VECTOR_DOUBLE_RECIPROCAL_EPS> SMcVector2D;

const SMcVector2D	

	/// Zero vector
	v2Zero(0,0),					

	/// All-DBL_MIN vector
	v2MinDouble(DBL_MIN, DBL_MIN),

	/// All-DBL_MAX vector
	v2MaxDouble(DBL_MAX, DBL_MAX);

//////////////////////////////////////////////////////////////////////////
/// 2D vector with float components
typedef SMcVector2D_T<float, MC_VECTOR_FLOAT_RECIPROCAL_EPS> SMcFVector2D;

const SMcFVector2D

	/// Zero vector
	vf2Zero(0.0f, 0.0f),					

	/// all-FLT_MIN vector
	vf2MinFloat(FLT_MIN, FLT_MIN),

	/// all-FLT_MAX vector
	vf2MaxFloat(FLT_MAX, FLT_MAX);

//===========================================================================
// SMcBox_T struct
//---------------------------------------------------------------------------
///	3D box parallel to axes with generic components.
/// 
//===========================================================================
template<typename Type, int nReciprocalEps>
struct SMcBox_T
{
	/// Minimum (x, y, z) vertex
	SMcVector3D_T<Type, nReciprocalEps>	MinVertex;

	/// Maximum (x, y, z) vertex
	SMcVector3D_T<Type, nReciprocalEps>	MaxVertex;

	/// \name Construction & Assignment
	//@{
	/// uninitialized box
	inline SMcBox_T() {}
	/// from two vertices
	SMcBox_T(const SMcVector3D_T<Type, nReciprocalEps> &_MinVertex, const SMcVector3D_T<Type, nReciprocalEps> &_MaxVertex);
	/// from MinX, MinY, MinZ, MaxX, MaxY, MaxZ
	SMcBox_T(Type dMinX, Type dMinY, Type dMinZ, Type dMaxX, Type dMaxY, Type dMaxZ);
	/// copy constructor
	inline SMcBox_T(const SMcBox_T &Box) { *this = Box; }
	/// constructor for box with other components type
	template<typename OtherType, int nOtherReciprocalEps>
	inline explicit SMcBox_T(const SMcBox_T<OtherType, nOtherReciprocalEps> &Box) 
		: MinVertex(Box.MinVertex), MaxVertex(Box.MaxVertex) {}
	void operator=(const SMcBox_T &Box);
	//@}

	/// \name Attributes
	//@{
	/// Returns the size
	inline SMcVector3D_T<Type, nReciprocalEps> Size() const { return (MaxVertex - MinVertex); }
	/// Retrieves the size in X
	inline Type SizeX() const  { return (MaxVertex.x - MinVertex.x); }
	/// Retrieves the size in Y
	inline Type SizeY() const { return (MaxVertex.y - MinVertex.y); }
	/// Retrieves the size in Z
	inline Type SizeZ() const { return (MaxVertex.z - MinVertex.z); }
	/// The geometric center point of the box
	inline SMcVector3D_T<Type, nReciprocalEps> CenterPoint() const  { return (MinVertex + MaxVertex)/2.0; }
	//@}

	/// \name Overlapping
	//@{
	/// Returns true if the vertex is within box
	bool VertexInBox(const SMcVector3D_T<Type, nReciprocalEps> &Vertex) const;
	/// Returns true if the vertex is within box on XY plane
	bool VertexInBoxXY(const SMcVector3D_T<Type, nReciprocalEps> &Vertex) const;
	/// Returns true if the box is within the box
	bool BoxInBox(const SMcBox_T &Box) const;
	/// Returns true if the requested box is inside our box
	bool Contains(const SMcBox_T &Box) const; 
	//@}

	/// \name Inflate
	//@{
	/// Inflate the box by offsetting its vertices by the specified values in the appropriate directions
	void Inflate(Type x, Type y, Type z);
	/// Inflate the box by offsetting its vertices by the specified vector in the appropriate directions
	void Inflate(const SMcVector3D_T<Type, nReciprocalEps> &OffsetVector);
	/// Inflate box's sizes by offsetting individual sides in the appropriate directions
	void Inflate(const SMcBox_T &Box);
	/// Inflate box's sizes by offsetting individual sides in the appropriate directions
	void Inflate(const SMcVector3D_T<Type, nReciprocalEps> &MinOffset, const SMcVector3D_T<Type, nReciprocalEps> &MaxOffset);
	//@}

	/// \name Deflate
	//@{
	/// Deflate the box by offsetting its vertices by the specified values in the appropriate directions
	void Deflate(Type x, Type y, Type z);
	/// Deflate the box by offsetting its vertices by the specified vector in the appropriate directions
	void Deflate(const SMcVector3D_T<Type, nReciprocalEps> &OffsetVector);
	/// Deflate box's sizes by offsetting individual sides in the appropriate directions
	void Deflate(const SMcBox_T &Box);
	/// Deflate box's sizes by offsetting individual sides in the appropriate directions
	void Deflate(const SMcVector3D_T<Type, nReciprocalEps> &MinOffset, const SMcVector3D_T<Type, nReciprocalEps> &MaxOffset);
	//@}

	/// \name Translate by moving the vertices
	//@{
	/// \n
	void Offset(Type x, Type y, Type z);
	/// \n
	inline void Offset(const SMcVector3D_T<Type, nReciprocalEps> &OffsetVector)
		{ Offset(OffsetVector.x, OffsetVector.y, OffsetVector. z); }
	//@}

	/// \name Boolean Operations
	//@{
	/// set this box to intersection of two others
	bool Intersect(const SMcBox_T &Box1, const SMcBox_T &Box2);

	/// set this box to bounding union of two others
	bool Union(const SMcBox_T &Box1, const SMcBox_T &Box2);
	//@}

	/// \name Additional Operations
	//@{
	/// \n
	void Normalize();
	/// \n
	inline void operator+=(const SMcVector3D_T<Type, nReciprocalEps> &OffsetVector) { Offset(OffsetVector); }
	/// \n
	inline void operator+=(const SMcBox_T &BoxToInflate) { Inflate(BoxToInflate); }
	/// \n
	inline void operator-=(const SMcVector3D_T<Type, nReciprocalEps> &OffsetVector) { Offset(-OffsetVector); }
	/// \n
	inline void operator-=(const SMcBox_T &BoxToDeflate) { Deflate(BoxToDeflate); }
	/// \n
	inline void operator&=(const SMcBox_T &BoxToIntersect) { Intersect(*this, BoxToIntersect); }
	/// \n
	inline void operator|=(const SMcBox_T &BoxToUnion) { Union(*this, BoxToUnion); }
	//@}

	/// \name Compare Operations
	//@{
	bool operator==(const SMcBox_T &Box) const;
	bool operator!=(const SMcBox_T &Box) const;
	//@}
};

//////////////////////////////////////////////////////////////////////////
/// 3D box parallel to axes with double components
typedef SMcBox_T<double, MC_VECTOR_DOUBLE_RECIPROCAL_EPS> SMcBox;

//////////////////////////////////////////////////////////////////////////
/// 3D box parallel to axes with float components
typedef SMcBox_T<float, MC_VECTOR_FLOAT_RECIPROCAL_EPS> SMcFBox;

//////////////////////////////////////////////////////////////////////////
// struct SMcVector3D_T: the rest of inline implementation

// construction and assignment
template<typename Type, int nReciprocalEps>
inline SMcVector3D_T<Type, nReciprocalEps>::SMcVector3D_T(const SMcVector2D_T<Type, nReciprocalEps> &v2)
{
	x = v2.x;
	y = v2.y;
	z = 0.f;
}

// binary operators
template<typename Type, int nReciprocalEps>
inline SMcVector3D_T<Type, nReciprocalEps> operator * (Type d, const SMcVector3D_T<Type, nReciprocalEps> &v)
{
	return SMcVector3D_T<Type, nReciprocalEps>(v.x*d, v.y*d, v.z*d);
}

// multiply and add
template<typename Type, int nReciprocalEps>
inline void SMcVector3D_T<Type, nReciprocalEps>::MulAdd(Type dMul, const SMcVector3D_T<Type, nReciprocalEps> &vAdd)
{
	x = x*dMul + vAdd.x;
	y = y*dMul + vAdd.y;
	z = z*dMul + vAdd.z;
}
template<typename Type, int nReciprocalEps>
inline SMcVector3D_T<Type, nReciprocalEps> SMcVector3D_T<Type, nReciprocalEps>::GetMulAdded(Type dMul, const SMcVector3D_T<Type, nReciprocalEps> &vAdd) const
{
	return SMcVector3D_T<Type, nReciprocalEps>(x*dMul + vAdd.x, y*dMul + vAdd.y, z*dMul + vAdd.z);
}

// normalize
template<typename Type, int nReciprocalEps>
inline void SMcVector3D_T<Type, nReciprocalEps>::Normalize()
{
	Type dLength2 = x*x + y*y + z*z;
	if (dLength2<=(Type(1)/nReciprocalEps)) {
		return;
	}
	Type dInvLen = Type(Type(1)/sqrt(dLength2));
	x *= dInvLen;
	y *= dInvLen;
	z *= dInvLen;
}
template<typename Type, int nReciprocalEps>
inline SMcVector3D_T<Type, nReciprocalEps> SMcVector3D_T<Type, nReciprocalEps>::GetNormalized() const
{
	Type dLength2 = x*x + y*y + z*z;
	if (dLength2<(Type(1)/nReciprocalEps)) {
		return SMcVector3D_T<Type, nReciprocalEps>(0,0,0);
	}
	Type dInvLen = Type(Type(1)/sqrt(dLength2));
	return SMcVector3D_T<Type, nReciprocalEps>(x*dInvLen, y*dInvLen, z*dInvLen);
}

// linear interpolation
template<typename Type, int nReciprocalEps>
inline SMcVector3D_T<Type, nReciprocalEps> SMcVector3D_T<Type, nReciprocalEps>::GetLinearInterpolationWith(const SMcVector3D_T<Type, nReciprocalEps> &vSecond, Type dInterpolationParam) const
{
	return SMcVector3D_T<Type, nReciprocalEps>((vSecond.x - x)*dInterpolationParam + x, (vSecond.y - y)*dInterpolationParam + y,
					 (vSecond.z - z)*dInterpolationParam + z);
}

template<typename Type, int nReciprocalEps>
inline void SMcVector3D_T<Type, nReciprocalEps>::GetRadianYawPitchFromForwardVector(Type *pdYaw, Type *pdPitch) const
{
	Type dXYLength = sqrt(x * x + y * y);
	if (dXYLength < (Type(1)/nReciprocalEps)) { // vertical vector
		*pdPitch = (fabs(z) < (Type(1)/nReciprocalEps) ? 0 : (z > 0 ? M_PI_2 : -M_PI_2));
		*pdYaw	 = 0; //can be anything
	}
	else
	{
		*pdPitch = atan2(z, dXYLength);
		*pdYaw	 = atan2(x, y);
	}
}

template<typename Type, int nReciprocalEps>
inline void SMcVector3D_T<Type, nReciprocalEps>::GetDegreeYawPitchFromForwardVector(Type *pdYaw, Type *pdPitch) const
{
	GetRadianYawPitchFromForwardVector(pdYaw, pdPitch);
	*pdYaw *= 180. / M_PI;
	*pdPitch *= 180. / M_PI;
}

template<typename Type, int nReciprocalEps>
inline void SMcVector3D_T<Type, nReciprocalEps>::GetRadianPitchRollFromUpVector(Type *pdPitch, Type *pdRoll) const
{
	Type dYZLength = sqrt(y * y + z * z);
	if (dYZLength < (Type(1)/nReciprocalEps)) { // vertical vector
		*pdRoll = (fabs(x) < (Type(1)/nReciprocalEps) ? 0 : (x > 0 ? M_PI_2 : -M_PI_2));
		*pdPitch = 0; //can be anything
	}
	else
	{
		*pdRoll = atan2(x, dYZLength);
		*pdPitch = atan2(y, z);
	}
}

template<typename Type, int nReciprocalEps>
inline void SMcVector3D_T<Type, nReciprocalEps>::GetDegreePitchRollFromUpVector(Type *pdPitch, Type *pdRoll) const
{
	GetRadianPitchRollFromUpVector(pdPitch, pdRoll);
	*pdPitch *= 180. / M_PI;
	*pdRoll *= 180. / M_PI;
}

template<typename Type, int nReciprocalEps>
inline void SMcVector3D_T<Type, nReciprocalEps>::RotateByRadianYawAngle(Type dYaw)
{
	Type	dCos = cos(dYaw),
			dSin = sin(dYaw);
	Type dX = x;
	x = x*dCos + y*dSin;
	y = y*dCos - dX*dSin;
}

template<typename Type, int nReciprocalEps>
inline void SMcVector3D_T<Type, nReciprocalEps>::RotateByDegreeYawAngle(Type dYaw)
{
	RotateByRadianYawAngle(dYaw * M_PI / 180.);
}

template<typename Type, int nReciprocalEps>
void SMcVector3D_T<Type, nReciprocalEps>::RotateByRadianYawPitchRoll(Type dYaw, Type dPitch, Type dRoll)
{
	Type dCos, dSin;
	dCos = cos(dRoll);
	dSin = sin(dRoll);
	Type dZ = z;
	z = z*dCos - x*dSin;
	x = x*dCos + dZ*dSin;

	dCos = cos(dPitch);
	dSin = sin(dPitch);
	Type dY = y;
	y = y*dCos - z*dSin;
	z = z*dCos + dY*dSin;

	dCos = cos(dYaw);
	dSin = sin(dYaw);
	Type dX = x;
	x = x*dCos + y*dSin;
	y = y*dCos - dX*dSin;
}

template<typename Type, int nReciprocalEps>
void SMcVector3D_T<Type, nReciprocalEps>::RotateByDegreeYawPitchRoll(Type dYaw, Type dPitch, Type dRoll)
{
	RotateByRadianYawPitchRoll(dYaw * M_PI / 180., dPitch * M_PI / 180., dRoll * M_PI / 180.);
}

//////////////////////////////////////////////////////////////////////////
// struct SMcVector2D_T: the rest of inline implementation

// binary operators
template<typename Type, int nReciprocalEps>
inline SMcVector2D_T<Type, nReciprocalEps> operator * (Type d, const SMcVector2D_T<Type, nReciprocalEps> &v)
{
	return SMcVector2D_T<Type, nReciprocalEps>(v.x*d, v.y*d);
}

// Yaw Angle
template<typename Type, int nReciprocalEps>
inline void SMcVector2D_T<Type, nReciprocalEps>::RotateByRadianYawAngle(Type dYaw)
{
	Type	dCos = cos(dYaw),
			dSin = sin(dYaw);
	Type dX = x;
	x = x*dCos + y*dSin;
	y = y*dCos - dX*dSin;
}
template<typename Type, int nReciprocalEps>
inline void SMcVector2D_T<Type, nReciprocalEps>::RotateByDegreeYawAngle(Type dYaw)
{
	RotateByRadianYawAngle(dYaw * M_PI / 180.);
}

// multiply and add
template<typename Type, int nReciprocalEps>
inline void SMcVector2D_T<Type, nReciprocalEps>::MulAdd(Type dMul, const SMcVector2D_T &v2Add)
{
	x = x*dMul + v2Add.x;
	y = y*dMul + v2Add.y;
}
template<typename Type, int nReciprocalEps>
inline SMcVector2D_T<Type, nReciprocalEps> SMcVector2D_T<Type, nReciprocalEps>::GetMulAdded(Type dMul, const SMcVector2D_T &v2Add) const
{
	return SMcVector2D_T<Type, nReciprocalEps>(x*dMul + v2Add.x, y*dMul + v2Add.y);
}

// normalize
template<typename Type, int nReciprocalEps>
inline void SMcVector2D_T<Type, nReciprocalEps>::Normalize()
{
	Type dInvLen = 1/sqrt(x*x + y*y);
	x *= dInvLen;
	y *= dInvLen;
}
template<typename Type, int nReciprocalEps>
inline SMcVector2D_T<Type, nReciprocalEps> SMcVector2D_T<Type, nReciprocalEps>::GetNormalized() const
{
	Type dInvLen = 1/sqrt(x*x + y*y);
	return SMcVector2D_T<Type, nReciprocalEps>(x*dInvLen, y*dInvLen);
}

// linear interpolation
template<typename Type, int nReciprocalEps>
inline SMcVector2D_T<Type, nReciprocalEps> SMcVector2D_T<Type, nReciprocalEps>::GetLinearInterpolationWith(const SMcVector2D_T<Type, nReciprocalEps> &vSecond, Type dInterpolationParam) const
{
	return SMcVector2D_T<Type, nReciprocalEps>((vSecond.x - x)*dInterpolationParam + x, (vSecond.y - y)*dInterpolationParam + y);
}

//////////////////////////////////////////////////////////////////////////
// struct SMcBox_T: the rest of inline implementation

// from two vertices
template<typename Type, int nReciprocalEps>
inline SMcBox_T<Type, nReciprocalEps>::SMcBox_T(const SMcVector3D_T<Type, nReciprocalEps> &_MinVertex, const SMcVector3D_T<Type, nReciprocalEps> &_MaxVertex)
	: MinVertex(_MinVertex), MaxVertex(_MaxVertex) {}
// from MinX, MinY, MinZ, MaxX, MaxY, MaxZ
template<typename Type, int nReciprocalEps>
inline SMcBox_T<Type, nReciprocalEps>::SMcBox_T(Type dMinX, Type dMinY, Type dMinZ, Type dMaxX, Type dMaxY, Type dMaxZ)
	: MinVertex(dMinX, dMinY, dMinZ), MaxVertex(dMaxX, dMaxY, dMaxZ) {}
// returns true if the vertex is within box
template<typename Type, int nReciprocalEps>
inline bool SMcBox_T<Type, nReciprocalEps>::VertexInBox(const SMcVector3D_T<Type, nReciprocalEps> &Vertex) const
{
	return (Vertex.x >= MinVertex.x && Vertex.x <= MaxVertex.x &&
			Vertex.y >= MinVertex.y && Vertex.y <= MaxVertex.y &&
			Vertex.z >= MinVertex.z && Vertex.z <= MaxVertex.z);
}
template<typename Type, int nReciprocalEps>
inline bool SMcBox_T<Type, nReciprocalEps>::VertexInBoxXY(const SMcVector3D_T<Type, nReciprocalEps> &Vertex) const
{
	return (Vertex.x >= MinVertex.x && Vertex.x <= MaxVertex.x &&
		Vertex.y >= MinVertex.y && Vertex.y <= MaxVertex.y);
}
// returns true if the box is within the box
template<typename Type, int nReciprocalEps>
inline bool SMcBox_T<Type, nReciprocalEps>::BoxInBox(const SMcBox_T<Type, nReciprocalEps> &Box) const
{
	return (Box.MaxVertex.x >= MinVertex.x && Box.MinVertex.x <= MaxVertex.x &&
			Box.MaxVertex.y >= MinVertex.y && Box.MinVertex.y <= MaxVertex.y &&
			Box.MaxVertex.z >= MinVertex.z && Box.MinVertex.z <= MaxVertex.z);
}
// returns true if the requested box is inside our box
template<typename Type, int nReciprocalEps>
inline bool SMcBox_T<Type, nReciprocalEps>::Contains(const SMcBox_T<Type, nReciprocalEps> &Box) const 
{
	return (Box.MinVertex.x >= MinVertex.x && Box.MaxVertex.x <= MaxVertex.x &&
			Box.MinVertex.y >= MinVertex.y && Box.MaxVertex.y <= MaxVertex.y &&
			Box.MinVertex.z >= MinVertex.z && Box.MaxVertex.z <= MaxVertex.z);
}
// Inflate the box by offsetting its vertices by the specified values in the appropriate directions
template<typename Type, int nReciprocalEps>
inline void SMcBox_T<Type, nReciprocalEps>::Inflate(Type x, Type y, Type z)
{
	MinVertex.x -= x; MinVertex.y -= y; MinVertex.z -= z;
	MaxVertex.x += x; MaxVertex.y += y; MaxVertex.z += z;
}
// Inflate the box by offsetting its vertices by the specified vector in the appropriate directions
template<typename Type, int nReciprocalEps>
inline void SMcBox_T<Type, nReciprocalEps>::Inflate(const SMcVector3D_T<Type, nReciprocalEps> &OffsetVector) 
	{ Inflate(OffsetVector.x, OffsetVector.y, OffsetVector.z); }
// Inflate box's sizes by offsetting individual sides in the appropriate directions
template<typename Type, int nReciprocalEps>
inline void SMcBox_T<Type, nReciprocalEps>::Inflate(const SMcBox_T<Type, nReciprocalEps> &Box)
{
	Inflate(Box.MinVertex, Box.MaxVertex);
}
// Inflate box's sizes by offsetting individual sides in the appropriate directions
template<typename Type, int nReciprocalEps>
inline void SMcBox_T<Type, nReciprocalEps>::Inflate(const SMcVector3D_T<Type, nReciprocalEps> &MinOffset, const SMcVector3D_T<Type, nReciprocalEps> &MaxOffset)
{
	MinVertex.x -= MinOffset.x; MinVertex.y -= MinOffset.y; MinVertex.z -= MinOffset.z;
	MaxVertex.x += MaxOffset.x; MaxVertex.y += MaxOffset.y; MaxVertex.z += MaxOffset.z;
}
// Deflate the box by offsetting its vertices by the specified values in the appropriate directions
template<typename Type, int nReciprocalEps>
inline void SMcBox_T<Type, nReciprocalEps>::Deflate(Type x, Type y, Type z)
{
	MinVertex.x += x; MinVertex.y += y; MinVertex.z += z;
	MaxVertex.x -= x; MaxVertex.y -= y; MaxVertex.z -= z;
}
// Deflate the box by offsetting its vertices by the specified vector in the appropriate directions
template<typename Type, int nReciprocalEps>
inline void SMcBox_T<Type, nReciprocalEps>::Deflate(const SMcVector3D_T<Type, nReciprocalEps> &OffsetVector) 
	{ Deflate(OffsetVector.x, OffsetVector.y, OffsetVector.z); }
// Deflate box's sizes by offsetting individual sides in the appropriate directions
template<typename Type, int nReciprocalEps>
inline void SMcBox_T<Type, nReciprocalEps>::Deflate(const SMcBox_T<Type, nReciprocalEps> &Box)
{
	Deflate(Box.MinVertex, Box.MaxVertex);
}
// Deflate box's sizes by offsetting individual sides in the appropriate directions
template<typename Type, int nReciprocalEps>
inline void SMcBox_T<Type, nReciprocalEps>::Deflate(const SMcVector3D_T<Type, nReciprocalEps> &MinOffset, const SMcVector3D_T<Type, nReciprocalEps> &MaxOffset)
{
	MinVertex.x += MinOffset.x; MinVertex.y += MinOffset.y; MinVertex.z += MinOffset.z;
	MaxVertex.x -= MaxOffset.x; MaxVertex.y -= MaxOffset.y; MaxVertex.z -= MaxOffset.z;
}
// Translate by moving the vertices
template<typename Type, int nReciprocalEps>
inline void SMcBox_T<Type, nReciprocalEps>::Offset(Type x, Type y, Type z)
{
	MinVertex.x += x; MinVertex.y += y; MinVertex.z += z;
	MaxVertex.x += x; MaxVertex.y += y; MaxVertex.z += z;
}
// set this box to intersection of two others
template<typename Type, int nReciprocalEps>
inline bool SMcBox_T<Type, nReciprocalEps>::Intersect(const SMcBox_T<Type, nReciprocalEps> &Box1, const SMcBox_T<Type, nReciprocalEps> &Box2)
{
	MinVertex.x = __max(Box1.MinVertex.x, Box2.MinVertex.x);
	MinVertex.y = __max(Box1.MinVertex.y, Box2.MinVertex.y);
	MinVertex.z = __max(Box1.MinVertex.z, Box2.MinVertex.z);
	MaxVertex.x = __min(Box1.MaxVertex.x, Box2.MaxVertex.x);
	MaxVertex.y = __min(Box1.MaxVertex.y, Box2.MaxVertex.y);
	MaxVertex.z = __min(Box1.MaxVertex.z, Box2.MaxVertex.z);
	return (MinVertex.x <= MaxVertex.x &&
			MinVertex.y <= MaxVertex.y &&
			MinVertex.z <= MaxVertex.z);
}

// set this box to bounding union of two others
template<typename Type, int nReciprocalEps>
inline bool SMcBox_T<Type, nReciprocalEps>::Union(const SMcBox_T<Type, nReciprocalEps> &Box1, const SMcBox_T<Type, nReciprocalEps> &Box2)
{
	MinVertex.x = __min(Box1.MinVertex.x, Box2.MinVertex.x);
	MinVertex.y = __min(Box1.MinVertex.y, Box2.MinVertex.y);
	MinVertex.z = __min(Box1.MinVertex.z, Box2.MinVertex.z);
	MaxVertex.x = __max(Box1.MaxVertex.x, Box2.MaxVertex.x);
	MaxVertex.y = __max(Box1.MaxVertex.y, Box2.MaxVertex.y);
	MaxVertex.z = __max(Box1.MaxVertex.z, Box2.MaxVertex.z);
	return true;
}

template<typename Type, int nReciprocalEps>
inline void SMcBox_T<Type, nReciprocalEps>::Normalize()
{
	if (MinVertex.x > MaxVertex.x)
	{
		Type d = MinVertex.x;
		MinVertex.x = MaxVertex.x;
		MaxVertex.x = d;
	}
	if (MinVertex.y > MaxVertex.y)
	{
		Type d = MinVertex.y;
		MinVertex.y = MaxVertex.y;
		MaxVertex.y = d;
	}
	if (MinVertex.z > MaxVertex.z)
	{
		Type d = MinVertex.z;
		MinVertex.z = MaxVertex.z;
		MaxVertex.z = d;
	}
}

template<typename Type, int nReciprocalEps>
inline void SMcBox_T<Type, nReciprocalEps>::operator=(const SMcBox_T<Type, nReciprocalEps> &Box) 
{
	MinVertex = Box.MinVertex; MaxVertex = Box.MaxVertex;
}
template<typename Type, int nReciprocalEps>
inline bool SMcBox_T<Type, nReciprocalEps>::operator==(const SMcBox_T<Type, nReciprocalEps> &Box) const
{
	return (MinVertex == Box.MinVertex && MaxVertex == Box.MaxVertex);
}
template<typename Type, int nReciprocalEps>
inline bool SMcBox_T<Type, nReciprocalEps>::operator!=(const SMcBox_T<Type, nReciprocalEps> &Box) const
{
	return (MinVertex != Box.MinVertex || MaxVertex != Box.MaxVertex);
}
