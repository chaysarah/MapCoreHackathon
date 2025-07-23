#pragma once

//==================================================================================
/// \file CMcDataArray.h
/// Interface for data array and its wrapper class
//==================================================================================

#include "McBasicTypes.h"

//==================================================================================
// Interface Name: IMcDataArray
//----------------------------------------------------------------------------------
/// Interface for data array returned by API functions
//==================================================================================
template<typename Type, bool bEditingAllowed = false>
class IMcDataArray
{
protected:
	virtual ~IMcDataArray() {}
public:
	IMcDataArray() : m_nContainerRefCount(0) {}

	//==============================================================================
    // Method Name: GetData()
    //------------------------------------------------------------------------------
	/// Retrieves the array data as const pointer
	///
    /// \return		The array data pointer
    //==============================================================================
	virtual const Type* GetData() const = 0;

	//==============================================================================
    // Method Name: GetLength()
    //------------------------------------------------------------------------------
	/// Retrieves the array length
	///
    /// \return		The array length
    //==============================================================================
	virtual UINT GetLength() const = 0;

	//==============================================================================
    // Method Name: GetNonConstDataIfAllowed()
    //------------------------------------------------------------------------------
	/// Retrieves the array data as non-const pointer if editing is allowed
	///
    /// \return		The array data pointer if \a bEditingAllowed == true; NULL otherwise
    //==============================================================================
	virtual Type* GetNonConstDataIfAllowed() = 0;

	//==============================================================================
    // Method Name: InsertElementsIfAllowed()
    //------------------------------------------------------------------------------
	/// Inserts elements into the array.
	/// 
	/// Valid only if editing is allowed (\a bEditingAllowed == true).
	///
    /// \param[in]	uIndex			The index of the first element to insert
    /// \param[in]	uNumElements	The number of elements to insert
    //==============================================================================
	virtual void InsertElementsIfAllowed(UINT uIndex, UINT uNumElements) = 0;

	//==============================================================================
    // Method Name: RemoveElementsIfAllowed()
    //------------------------------------------------------------------------------
	/// Removes elements from the array.
	/// 
	/// Valid only if editing is allowed (\a bEditingAllowed == true).
	///
    /// \param[in]	uIndex			The index of the first element to remove
    /// \param[in]	uNumElements	The number of elements to remove
    //==============================================================================
	virtual void RemoveElementsIfAllowed(UINT uIndex, UINT uNumElements) = 0;

	//==============================================================================
    // Method Name: Release()
    //------------------------------------------------------------------------------
	/// Releases array object
	/// 
	/// Called when removed from its last container
    //==============================================================================
	virtual void Release() = 0;

	//==============================================================================
	// Method Name: AddedToContainer()
	//------------------------------------------------------------------------------
	/// Increase the container's reference count
	//==============================================================================
	virtual void AddedToContainer() { ++m_nContainerRefCount; }

	//==============================================================================
	// Method Name: RemovedFromContainer()
	//------------------------------------------------------------------------------
	/// Decrease the container's reference count
	//==============================================================================
    virtual void RemovedFromContainer()
    {
        m_nContainerRefCount --;
        if (m_nContainerRefCount <= 0)
        {
            Release();
        }
    }

private:
	int m_nContainerRefCount;
};

//==================================================================================
// Class Name: CMcDataArray
//----------------------------------------------------------------------------------
/// Class for data array returned by API functions (data array interface wrapper)
///
/// Destroys data array interface in the destructor
//==================================================================================
template<typename Type, bool bEditingAllowed = false>
class CMcDataArray
{
public:
	/// \name Construction & Assignment
	//@{

	//==============================================================================
	// Method Name: CMcDataArray()
	//------------------------------------------------------------------------------
	/// Constructs data array class
	//==============================================================================
	inline CMcDataArray() : m_pDataArray(NULL) {}
	//==============================================================================
	// Method Name: CMcDataArray()
	//------------------------------------------------------------------------------
	/// Constructs data array class
	//==============================================================================
	inline CMcDataArray(const CMcDataArray &Src) : m_pDataArray(Src.m_pDataArray)
	{
		if (m_pDataArray != NULL)
		{
			m_pDataArray->AddedToContainer();
		}
	}

	//==============================================================================
	// Method Name: operator=
	//------------------------------------------------------------------------------
	/// Assignment operator
	///
	/// \return		The pointer to this class
	//==============================================================================
	inline CMcDataArray& operator=(const CMcDataArray &Src)
	{
		SetDataArray(Src.m_pDataArray);
		return *this;
	}

	//@}

	/// \name Destruction
	//@{

	//==============================================================================
	// Method Name: ~CMcDataArray()
	//------------------------------------------------------------------------------
	/// Destructs data array class destroying data array interface
	//==============================================================================
	virtual ~CMcDataArray() 
	{
		if (m_pDataArray != NULL)
		{
			m_pDataArray->RemovedFromContainer();
		}
	}

	//@}

	/// \name Get Data
	//@{

	//==============================================================================
	// Method Name: GetDataArray()
	//------------------------------------------------------------------------------
	/// Retrieves data array interface
	///
	/// \return
	///     The data array interface
	//==============================================================================
	inline IMcDataArray<Type, bEditingAllowed>* GetDataArray() const
	{
		return m_pDataArray;
	}

	//==============================================================================
    // Method Name: GetData()
    //------------------------------------------------------------------------------
	/// Retrieves the array data as const pointer
	///
    /// \return		The array data pointer
    //==============================================================================
	inline const Type* GetData() const
	{
		return (m_pDataArray != NULL ? m_pDataArray->GetData() : (const Type*)NULL);
	}

	//==============================================================================
    // Method Name: operator[]
    //------------------------------------------------------------------------------
    /// Retrieves an array element via an index
	///
    /// \return		The reference to the requested element
    //==============================================================================
	inline const Type& operator[](UINT uIndex) const
	{
		return (m_pDataArray->GetData())[uIndex];
	}

	//==============================================================================
    // Method Name: GetLength()
    //------------------------------------------------------------------------------
	/// Retrieves the array length
	///
    /// \return		The array length
    //==============================================================================
	inline UINT GetLength() const
	{
		return (m_pDataArray != NULL ? m_pDataArray->GetLength() : 0);
	}

	//==============================================================================
    // Method Name: GetNonConstDataIfAllowed()
    //------------------------------------------------------------------------------
	/// Retrieves the array data as non-const pointer if editing is allowed
	///
    /// \return		The array data pointer if \a bEditingAllowed == true; NULL otherwise
    //==============================================================================
	inline Type* GetNonConstDataIfAllowed()
	{
		return (m_pDataArray != NULL ? m_pDataArray->GetNonConstDataIfAllowed() : (Type*)NULL);
	}

	//@}

	/// \name Set Data
	//@{

	//==============================================================================
    // Method Name: InsertElementsIfAllowed()
    //------------------------------------------------------------------------------
	/// Inserts elements into the array.
	/// 
	/// Valid only if editing is allowed (\a bEditingAllowed == true).
	///
    /// \param[in]	uIndex			The index of the first element to insert
    /// \param[in]	uNumElements	The number of elements to insert
    //==============================================================================
	inline void InsertElementsIfAllowed(UINT uIndex, UINT uNumElements)
	{
		if (m_pDataArray != NULL)
		{
			m_pDataArray->InsertElementsIfAllowed(uIndex, uNumElements);
		}
	}

	//==============================================================================
    // Method Name: RemoveElementsIfAllowed()
    //------------------------------------------------------------------------------
	/// Removes elements from the array.
	/// 
	/// Valid only if editing is allowed (\a bEditingAllowed == true).
	///
    /// \param[in]	uIndex			The index of the first element to remove
    /// \param[in]	uNumElements	The number of elements to remove
    //==============================================================================
	inline void RemoveElementsIfAllowed(UINT uIndex, UINT uNumElements)
	{
		if (m_pDataArray != NULL)
		{
			m_pDataArray->RemoveElementsIfAllowed(uIndex, uNumElements);
		}
	}

	//==============================================================================
	// Method Name: SetDataArray()
	//------------------------------------------------------------------------------
	/// Sets data array interface that will be destroyed in destructor
	///
	/// \param[in]	pDataArray		The data array interface
	//==============================================================================
	inline void SetDataArray(IMcDataArray<Type, bEditingAllowed> *pDataArray)
	{
		if (pDataArray != m_pDataArray)
		{
			if (m_pDataArray != NULL)
			{
				m_pDataArray->RemovedFromContainer();
			}
			m_pDataArray = pDataArray;
			if (m_pDataArray != NULL)
			{
				m_pDataArray->AddedToContainer();
			}
		}
	}

	//@}

protected:
	IMcDataArray<Type, bEditingAllowed> *m_pDataArray;
};

//==================================================================================
// Class Name: CMcString
//----------------------------------------------------------------------------------
/// Class for string (as a char array) returned by API functions (data array interface wrapper)
///
/// Destroys data array interface in the destructor
//==================================================================================
class CMcString : public CMcDataArray<char>
{
public:

	/// \name Length
	//@{
	//==============================================================================
    // Method Name: GetLength()
    //------------------------------------------------------------------------------
	/// Retrieves the string length (excluding the terminal NULL) 
	///
    /// \return		The string length
    //==============================================================================
	inline UINT GetLength() const
	{
		return (m_pDataArray != NULL ? m_pDataArray->GetLength() - 1 : 0);
	}
	//@}
};

//==================================================================================
// Class Name: CMcWString
//----------------------------------------------------------------------------------
/// Class for string (as a wchar_t array) returned by API functions (data array interface wrapper)
///
/// Destroys data array interface in the destructor
//==================================================================================
class CMcWString : public CMcDataArray<wchar_t>
{
public:

	/// \name Length
	//@{
	//==============================================================================
    // Method Name: GetLength()
    //------------------------------------------------------------------------------
	/// Retrieves the string length (excluding the terminal NULL) 
	///
    /// \return		The string length
    //==============================================================================
	inline UINT GetLength() const
	{
		return (m_pDataArray != NULL ? m_pDataArray->GetLength() - 1 : 0);
	}
	//@}
};
