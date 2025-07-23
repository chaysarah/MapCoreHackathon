#pragma once
//==================================================================================
/// \file IMcProperty.h
/// Interface for object scheme property definitions
//==================================================================================

#include "SMcVector.h"
#include "McCommonTypes.h"

class IMcTexture;
class IMcFont;
class IMcMesh;
class IMcConditionalSelector;

struct SMcBColor;
struct SMcFColor;
struct SMcAttenuation;
struct SMcVariantString;
struct SMcRotation;
struct SMcAnimation;

//==================================================================================
// Interface Name: IMcProperty
//----------------------------------------------------------------------------------
/// Interface for object scheme property definitions. 
///
//==================================================================================
class IMcProperty
{
protected:
	virtual ~IMcProperty() {}

public:

	//==============================================================================
	// Enum Name: EPredefinedPropertyIDs
	//------------------------------------------------------------------------------
	/// Predefined property ID constants
	//==============================================================================
	enum EPredefinedPropertyIDs : UINT
	{
		/// All user property IDs should be smaller than this.
		EPPI_FIRST_RESERVED_ID = UINT(-100),	

		/// - To be used as parameter in SetXXX() of object scheme node 
		///   to define a property as a shared property.
		/// - If a property of object scheme node is shared, 
		///   GetXXX() will return EPPI_SHARED_PROPERTY_ID as a property ID.
		/// \note 
		/// - A shared property is defined in object scheme node
		///   along with its value, shared by all objects of the scheme.
		/// - A private property is defined in object scheme node
		///   along with its ID, while each object of the scheme
		///   has its own property value, accessed via this ID.
		EPPI_SHARED_PROPERTY_ID = UINT(-1),

		/// - To be used as parameter in SetXXX() of object scheme node 
		///	  to remove the specified object-state property.
		/// - If the object-state property of object scheme node is not defined but there are some properties defined
		///	  for the following states, the appropriate GetXXX() will return EPPI_NO_STATE_PROPERTY_ID as a property ID.
		/// \note
		///   The appropriate object-state property of object scheme node is automatically selected when the state 
		///   of the object is changed. If the property is undefined, the property of zero-state is used instead.
		EPPI_NO_STATE_PROPERTY_ID = UINT(-2),

		/// - To be used as parameter in SetXXX() of object scheme node 
		///   to remove all the object-state properties starting with the specified one.
		/// - If all the object-state properties starting with the specified one are not defined, 
		///   the appropriate GetXXX() will return EPPI_NO_MORE_STATE_PROPERTIES_ID as a property ID.
		/// \note
		///   The appropriate object-state property of object scheme node is automatically selected when the state 
		///   of the object is changed. If the property is undefined, the property of zero-state is used instead.
		EPPI_NO_MORE_STATE_PROPERTIES_ID = UINT(-3)

	};

	//==============================================================================
	// Enum Name: EPropertyType
	//------------------------------------------------------------------------------
	/// Property type
	//==============================================================================
	enum EPropertyType 
	{
		EPT_BOOL				= 0,	///< bool
		EPT_BYTE				= 1,	///< BYTE
		EPT_INT					= 2,	///< int
		EPT_UINT				= 3,	///< UINT
		EPT_FLOAT				= 4,	///< float
		EPT_DOUBLE				= 5,	///< double
		EPT_FVECTOR2D			= 6,	///< SMcFVector2D
		EPT_VECTOR2D			= 7,	///< SMcVector2D
		EPT_FVECTOR3D			= 8,	///< SMcFVector3D
		EPT_VECTOR3D			= 9,	///< SMcVector3D
		EPT_BCOLOR				= 10,	///< SMcBColor
		EPT_FCOLOR				= 11,	///< SMcFColor
		EPT_ATTENUATION			= 12,	///< SMcAttenuation
		EPT_STRING				= 13,	///< SMcVariantString
		EPT_TEXTURE				= 14,	///< IMcTexture*
		EPT_FONT				= 15,	///< IMcFont*
		EPT_MESH				= 16,	///< IMcMesh*
		EPT_CONDITIONALSELECTOR	= 17,	///< IMcConditionalSelector*
		EPT_ROTATION			= 18,	///< SMcRotation
		EPT_ANIMATION			= 19,	///< SMcAnimation
		EPT_SUBITEM_ARRAY		= 20,	///< IMcProperty::SArrayProperty<SMcSubItemData>
		EPT_INT_ARRAY			= 21,	///< IMcProperty::SArrayProperty<int>
		EPT_UINT_ARRAY			= 22,	///< IMcProperty::SArrayProperty<UINT>
		EPT_FVECTOR2D_ARRAY		= 23,	///< IMcProperty::SArrayProperty<SMcFVector2D>
		EPT_VECTOR2D_ARRAY		= 24,	///< IMcProperty::SArrayProperty<SMcVector2D>
		EPT_FVECTOR3D_ARRAY		= 25,	///< IMcProperty::SArrayProperty<SMcFVector3D>
		EPT_VECTOR3D_ARRAY		= 26,	///< IMcProperty::SArrayProperty<SMcVector3D>
		EPT_BCOLOR_ARRAY		= 27,	///< IMcProperty::SArrayProperty<SMcBColor>
		EPT_MATRIX4D			= 28,	///< SMcMatrix4D
		EPT_SBYTE 				= 29,	///< signed byte
		EPT_ENUM				= 30,	///< BYTE/UINT enum
		EPT_NUM							///< number of the enum's members (not used as a valid property type)
	};

	/// Array property struct
	template<typename T>
	struct SArrayProperty
	{
		const T *aElements;		///< array of element
		UINT uNumElements;		///< number of elements in the array

		/// \n
		inline SArrayProperty()	: aElements(NULL), uNumElements(0) {}
		/// \n
		inline SArrayProperty(const T _aElements[], UINT _uNumElements)
			: aElements(_aElements), uNumElements(_uNumElements) {}
		/// \n
		inline SArrayProperty(const SArrayProperty &Other) { memcpy(this, &Other, sizeof(*this)); }
		/// \n
		inline SArrayProperty& operator=(const SArrayProperty &Other)
		{
			memcpy(this, &Other, sizeof(*this));
			return *this;
		}
		/// \n
		inline bool operator==(const SArrayProperty &Other) const
		{
			return (aElements == Other.aElements && uNumElements == Other.uNumElements);
		}
		/// \n
		inline bool operator!=(const SArrayProperty &Other) const
		{
			return (aElements != Other.aElements || uNumElements != Other.uNumElements);
		}
	};

	/// Property name, ID and type
	struct SPropertyNameIDType
	{
		
		PCSTR strName;			///< Property name
		UINT uID;				///< Property ID
		EPropertyType eType;	///< Property type

		SPropertyNameIDType() : strName(NULL), uID(MC_EMPTY_ID), eType(IMcProperty::EPT_NUM) {}
	};

	/// Property name and/or ID
	struct SPropertyNameID
	{
		PCSTR strName;			///< Property name
		UINT uID;				///< Property ID

		SPropertyNameID() : strName(NULL), uID(MC_EMPTY_ID) {}
		SPropertyNameID(PCSTR _strName) : strName(_strName), uID(MC_EMPTY_ID) {}
		SPropertyNameID(UINT _uID) : strName(NULL), uID(_uID) {}
	};

	/// Property struct containing the property name and/or ID as well as type and value
	struct SVariantProperty
	{
		/// Property name
		PCSTR strName;

		/// Property ID
		UINT uID;
		
		/// Property type
		EPropertyType eType;

		/// Property value (meaning depends on property type, use appropriate access functions)
		struct SValue
		{
			const bool& Boolean() const { return *(const bool*)Buffer; }
			const BYTE& Byte() const { return *(const BYTE*)Buffer; }
			const int& Int() const { return *(const int*)Buffer; }
			const UINT& UInt() const { return *(const UINT*)Buffer; }
			const float& Float() const { return *(const float*)Buffer; }
			const double& Double() const { return *(const double*)Buffer; }
			const SMcFVector2D& FVector2D() const { return *(const SMcFVector2D*)Buffer; }
			const SMcVector2D& Vector2D() const { return *(const SMcVector2D*)Buffer; }
			const SMcFVector3D& FVector3D() const { return *(const SMcFVector3D*)Buffer; }
			const SMcVector3D& Vector3D() const { return *(const SMcVector3D*)Buffer; }
			const SMcBColor& BColor() const { return *(const SMcBColor*)Buffer; }
			const SMcFColor& FColor() const { return *(const SMcFColor*)Buffer; }
			const SMcAttenuation& Attenuation() const { return *(const SMcAttenuation*)Buffer; }
			const SMcVariantString& String() const { return *(const SMcVariantString*)Buffer; }
			IMcTexture *const& Texture() const { return *(IMcTexture *const*)Buffer; }
			IMcFont *const& Font() const { return *(IMcFont *const*)Buffer; }
			IMcMesh *const& Mesh() const { return *(IMcMesh *const*)Buffer; }
			IMcConditionalSelector *const& Selector() const { return *(IMcConditionalSelector *const*)Buffer; }
			const SMcRotation& Rotation() const { return *(const SMcRotation*)Buffer; }
			const SMcAnimation& Animation() const { return *(const SMcAnimation*)Buffer; }
			template<typename T>
			inline const SArrayProperty<T>& Array() const { return *(const SArrayProperty<T>*)Buffer; }
			const SMcMatrix4D& Matrix4D() const { return *(const SMcMatrix4D*)Buffer; }
			const signed char& SByte() const { return *(const signed char*)Buffer; }
			const UINT& Enum() const { return *(const UINT*)Buffer; }
			const void* UnknownPtr() const { return Buffer; }

			template<typename T>
			inline void GetAs(SArrayProperty<T> *pValue) const { *pValue = Array<T>(); }
			template<typename T>
			inline void GetAs(T *pValue) const { *pValue = (T)Enum(); } // for enums: passed as UINT; for other types: instantiated below

			bool& Boolean() { return *(bool*)Buffer; }
			BYTE& Byte() { return *(BYTE*)Buffer; }
			int& Int() { return *(int*)Buffer; }
			UINT& UInt() { return *(UINT*)Buffer; }
			float& Float() { return *(float*)Buffer; }
			double& Double() { return *(double*)Buffer; }
			SMcFVector2D& FVector2D() { return *(SMcFVector2D*)Buffer; }
			SMcVector2D& Vector2D() { return *(SMcVector2D*)Buffer; }
			SMcFVector3D& FVector3D() { return *(SMcFVector3D*)Buffer; }
			SMcVector3D& Vector3D() { return *(SMcVector3D*)Buffer; }
			SMcBColor& BColor() { return *(SMcBColor*)Buffer; }
			SMcFColor& FColor() { return *(SMcFColor*)Buffer; }
			SMcAttenuation& Attenuation() { return *(SMcAttenuation*)Buffer; }
			SMcVariantString& String() { return *(SMcVariantString*)Buffer; }
			IMcTexture*& Texture() { return *(IMcTexture**)Buffer; }
			IMcFont*& Font() { return *(IMcFont**)Buffer; }
			IMcMesh*& Mesh() { return *(IMcMesh**)Buffer; }
			IMcConditionalSelector*& Selector() { return *(IMcConditionalSelector**)Buffer; }
			SMcRotation& Rotation() { return *(SMcRotation*)Buffer; }
			SMcAnimation& Animation() { return *(SMcAnimation*)Buffer; }
			template<typename T>
			inline SArrayProperty<T>& Array() { return *(SArrayProperty<T>*)Buffer; }
			SMcMatrix4D& Matrix4D() { return *(SMcMatrix4D*)Buffer; }
			signed char& SByte() { return *(signed char*)Buffer; }
			UINT& Enum() { return *(UINT*)Buffer; }
			void* UnknownPtr() { return Buffer; }

			template<typename T>
			inline void SetAs(const SArrayProperty<T> &Value) { Array<T>() = Value; }
			template<typename T>
			inline void SetAs(const T &Value) { Enum() = (UINT)Value; } // for enums: passed as UINT; for other types: instantiated below

		private:
			BYTE Buffer[sizeof(SMcMatrix4D)];
		} Value;
		SVariantProperty() : strName(NULL), uID(MC_EMPTY_ID), eType(IMcProperty::EPT_NUM) {}

	};
};

template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<bool>(bool *pValue) const { *pValue = Boolean(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<BYTE>(BYTE *pValue) const { *pValue = Byte(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<int>(int *pValue) const { *pValue = Int(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<UINT>(UINT *pValue) const { *pValue = UInt(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<float>(float *pValue) const { *pValue = Float(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<double>(double *pValue) const { *pValue = Double(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<SMcFVector2D>(SMcFVector2D *pValue) const { *pValue = FVector2D(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<SMcVector2D>(SMcVector2D *pValue) const { *pValue = Vector2D(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<SMcFVector3D>(SMcFVector3D *pValue) const { *pValue = FVector3D(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<SMcVector3D>(SMcVector3D *pValue) const { *pValue = Vector3D(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<SMcBColor>(SMcBColor *pValue) const { *pValue = BColor(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<SMcFColor>(SMcFColor *pValue) const { *pValue = FColor(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<SMcAttenuation>(SMcAttenuation *pValue) const { *pValue = Attenuation(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<SMcVariantString>(SMcVariantString *pValue) const { *pValue = String(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<PCSTR>(PCSTR *pValue) const { *pValue = String().GetAnsiStrings()[0]; }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<PCWSTR>(PCWSTR *pValue) const { *pValue = String().GetUnicodeStrings()[0]; }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<IMcTexture*>(IMcTexture **pValue) const { *pValue = Texture(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<IMcFont*>(IMcFont **pValue) const { *pValue = Font(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<IMcMesh*>(IMcMesh **pValue) const { *pValue = Mesh(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<IMcConditionalSelector*>(IMcConditionalSelector **pValue) const { *pValue = Selector(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<SMcRotation>(SMcRotation *pValue) const { *pValue = Rotation(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<SMcAnimation>(SMcAnimation *pValue) const { *pValue = Animation(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<SMcMatrix4D>(SMcMatrix4D *pValue) const { *pValue = Matrix4D(); }
template<> inline void IMcProperty::SVariantProperty::SValue::GetAs<signed char>(signed char *pValue) const { *pValue = SByte(); }

template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<bool>(const bool &Value) { Boolean() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<BYTE>(const BYTE &Value) { Byte() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<int>(const int &Value) { Int() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<UINT>(const UINT &Value) { UInt() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<float>(const float &Value) { Float() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<double>(const double &Value) { Double() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<SMcFVector2D>(const SMcFVector2D &Value) { FVector2D() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<SMcVector2D>(const SMcVector2D &Value) { Vector2D() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<SMcFVector3D>(const SMcFVector3D &Value) { FVector3D() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<SMcVector3D>(const SMcVector3D &Value) { Vector3D() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<SMcBColor>(const SMcBColor &Value) { BColor() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<SMcFColor>(const SMcFColor &Value) { FColor() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<SMcAttenuation>(const SMcAttenuation &Value) { Attenuation() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<SMcVariantString>(const SMcVariantString &Value) { String() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<PCSTR>(const PCSTR &Value) { String() = SMcVariantString(Value); }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<PCWSTR>(const PCWSTR &Value) { String() = SMcVariantString(Value); }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<IMcTexture*>(IMcTexture *const& Value) { Texture() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<IMcFont*>(IMcFont *const& Value) { Font() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<IMcMesh*>(IMcMesh *const& Value) { Mesh() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<IMcConditionalSelector*>(IMcConditionalSelector *const& Value) { Selector() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<SMcRotation>(const SMcRotation &Value) { Rotation() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<SMcAnimation>(const SMcAnimation &Value) { Animation() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<SMcMatrix4D>(const SMcMatrix4D &Value) { Matrix4D() = Value; }
template<> inline void IMcProperty::SVariantProperty::SValue::SetAs<signed char>(const signed char &Value) { SByte() = Value; }
