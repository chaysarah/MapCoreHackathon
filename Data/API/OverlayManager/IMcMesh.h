#pragma once
//==================================================================================
/// \file IMcMesh.h
/// The interface for mesh resource
//==================================================================================

#include "IMcErrors.h"
#include "IMcBase.h"
#include "OverlayManager/IMcTexture.h"
#include "CMcDataArray.h"

struct SMcBColor;
class IMcXFileMesh;
class IMcNativeMesh;
class IMcNativeMeshFile;
class IMcNativeLODMeshFile;

//==================================================================================
// Interface Name: IMcMesh
//----------------------------------------------------------------------------------
/// The mesh interface
///
/// The base interface for all mesh resources
//==================================================================================
class IMcMesh : public virtual IMcBase
{
protected:

	virtual ~IMcMesh() {};

public:
	
    /// \name Mesh Type And Casting
    //@{

    //==============================================================================
    // Method Name: GetMeshType()
    //------------------------------------------------------------------------------
    /// Returns the mesh type unique id.
    ///
 	/// \remark
	///		Use the cast methods in order to get the correct type.
	//==============================================================================
    virtual UINT GetMeshType() const = 0;

    //==============================================================================
    // Method Name: CastToXFileMesh(...)
    //------------------------------------------------------------------------------
    /// Casts the #IMcMesh* To #IMcXFileMesh*
    /// 
    /// \return
    ///     - #IMcXFileMesh*
    //==============================================================================
	virtual IMcXFileMesh* CastToXFileMesh() = 0;
	
	//==============================================================================
	// Method Name: CastToNativeMesh(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcMesh* To #IMcNativeMesh*
	/// 
	/// \return
	///     - #IMcNativeMesh*
	//==============================================================================
	virtual IMcNativeMesh* CastToNativeMesh() = 0;

	//==============================================================================
	// Method Name: CastToNativeMeshFile(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcMesh* To #IMcNativeMeshFile*
	/// 
	/// \return
	///     - #IMcNativeMeshFile*
	//==============================================================================
	virtual IMcNativeMeshFile* CastToNativeMeshFile() = 0;

	//==============================================================================
	// Method Name: CastToNativeLODMeshFile(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcMesh* To #IMcNativeLODMeshFile*
	/// 
	/// \return
	///     - #IMcNativeLODMeshFile*
	//==============================================================================
	virtual IMcNativeLODMeshFile* CastToNativeLODMeshFile() = 0;

    //@}

	/// \name Creation Parameters
	//@{

	//==============================================================================
	// Method Name: IsCreatedWithUseExisting(...)
	//------------------------------------------------------------------------------
	/// Retrieves whether the mesh was created with use-existing flag.
	///
	/// \param[out] pbCreatedWithUseExisting	True if \a bUseExisting parameter of 
	///											Create() function was true and the mesh's 
	///											parameters were not changed after creation.
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode IsCreatedWithUseExisting(bool *pbCreatedWithUseExisting) const = 0;

	//@}
};

//==================================================================================
// Interface Name: IMcXFileMesh
//----------------------------------------------------------------------------------
/// The interface for mesh in DirectX format loaded from X-file (*.x)
///
/// \note
///		Unimplemented.
//==================================================================================
class IMcXFileMesh : public virtual IMcMesh
{
protected:

	virtual ~IMcXFileMesh() {};

public: 

    enum
    {
        //==============================================================================
        /// Mesh unique ID for this interface
        //==============================================================================
        MESH_TYPE = 1
    };

    /// \name X-File and Transparent Color
    //@{

	//==============================================================================
	// Method Name: SetXFile(...)
	//------------------------------------------------------------------------------
	/// Sets the X-File name.
	///
	/// \param[in]	strXFile			The X-File name
	/// \param[in]	pTransparentColor	The color value for mesh textures to replace with transparent 
	///									or NULL to disable color key
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetXFile(PCSTR strXFile, 
									  const SMcBColor *pTransparentColor = NULL) = 0;

	//==============================================================================
	// Method Name: GetXFile(...)
	//------------------------------------------------------------------------------
	/// Retrieves the X-File name.
	///
	/// \param[out] pstrXFile	The X-File file name
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetXFile(PCSTR *pstrXFile) const = 0;

	//==============================================================================
	// Method Name: GetTransparentColor(...)
	//------------------------------------------------------------------------------
	/// Retrieves transparent color.
	///
	/// \param[out]	pTransparentColor				color value to replace with transparent 
	///												(valid only if color key is enabled)
	///	\param[out]	pbIsTransparentColorEnabled		whether transparent color is enabled
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetTransparentColor(
		SMcBColor *pTransparentColor, bool *pbIsTransparentColorEnabled) const = 0;

    //@}

    /// \name Create
    //@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a mesh from X-File.
	///
	/// \param[out]	ppMesh				The pointer to the created mesh
	/// \param[in]	strXFile			The X-File name
	/// \param[in]	pTransparentColor	The color value for mesh textures to replace with transparent 
	///									or NULL to disable color key
	///	\param[in]	bUseExisting		If true and some mesh based on this file already 
	///									exists, it will be returned instead of creating a new one
	///	\param[out]	pbExistingUsed		Whether an existing mesh based on this file was used
	///									or a new one was created
	/// \note
	///		Unimplemented.
	///
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcXFileMesh **ppMesh, PCSTR strXFile, 
		const SMcBColor *pTransparentColor = NULL,
		bool bUseExisting = true, bool *pbExistingUsed = NULL);

    //@}
};

//==================================================================================
// Interface Name: IMcNativeMesh
//----------------------------------------------------------------------------------
/// The base interface for mesh in MapCore format
//==================================================================================
class IMcNativeMesh : public virtual IMcMesh
{
protected:

	virtual ~IMcNativeMesh() {};

public:

	//==============================================================================
	// Enum Name: EMappedNameType
	//------------------------------------------------------------------------------
	/// Mapped names types
	//==============================================================================
	enum EMappedNameType
	{
		EMNT_ATTACH_POINT,
		EMNT_TEXTURE_UNIT_STATE
	};

	/// Mapped name data structure
	struct SMappedNameData
	{
		UINT	uID;		///< The ID to be referred to in object scheme
		PCSTR	strName;	///< The name defined in mesh file

		SMappedNameData() : strName(NULL) {}
	};

	/// \name Mapped Name
    //@{

	//==============================================================================
	// Method Name: SetMappedNameID()
	//------------------------------------------------------------------------------
	/// Sets or removes mapped name ID to be referred to in object scheme
	///
	/// \param[in]  eType			The mapped name type
	/// \param[in]	uID				The ID to set or remove (should be != #MC_EMPTY_ID) 
	/// \param[in]	strName			The name defined in mesh file to set ID
	///								or NULL to remove ID
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetMappedNameID(EMappedNameType eType, UINT uID, PCSTR strName) = 0;

	//==============================================================================
	// Method Name: GetMappedNameByID()
	//------------------------------------------------------------------------------
	/// Retrieves mapped name by ID previously set
	///
	/// \param[in]  eType			The mapped name type
	/// \param[in]	uID				The ID of the mapped name to retrieve
	/// \param[out]	pstrName		The mapped name
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetMappedNameByID(EMappedNameType eType, UINT uID, PCSTR *pstrName) const = 0;

	//==============================================================================
	// Method Name: SetMappedNamesIDs()
	//------------------------------------------------------------------------------
	/// Sets all mapped names IDs replacing the existing ones.
	///
	/// \param[in]  eType				The mapped name type
	/// \param[in]	aMappedNamesData	The mapped names data array
	/// \param[in]	uNumMappedNames		The number of mapped names in the array
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetMappedNamesIDs(
		EMappedNameType eType, SMappedNameData aMappedNamesData[], UINT uNumMappedNames) = 0;

	//==============================================================================
	// Method Name: GetMappedNamesIDs()
	//------------------------------------------------------------------------------
	/// Retrieves all mapped names IDs previously set
	///
	/// \param[in]  eType	The mapped name type
	/// \param[out]	paIDs	The array of IDs to fill
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetMappedNamesIDs(EMappedNameType eType, CMcDataArray<UINT> *paIDs) const = 0;

	//@}

	/// \name Texture Unit States, Attach Points and Animations
	//@{

	//==============================================================================
	// Method Name: GetTextureUnitStatesNames(...)
	//------------------------------------------------------------------------------
	/// Retrieves names of all texture unit states of the mesh.
	///
	/// \param[out] pastrNames			The array of names
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetTextureUnitStatesNames(CMcDataArray<PCSTR> *pastrNames) /*const*/ = 0;

	//==============================================================================
	// Method Name: GetAttachPointsNames(...)
	//------------------------------------------------------------------------------
	/// Retrieves names of all attach points of the mesh.
	///
	/// \param[out] pastrNames			The array of names
	///	
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetAttachPointsNames(CMcDataArray<PCSTR> *pastrNames) /*const*/ = 0;

	//==============================================================================
	// Method Name: GetNumAttachPoints(...)
	//------------------------------------------------------------------------------
	/// Retrieves the number of attach points in mesh.
	///
	/// \param[out] puNumAttachPoints			The attach points number
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetNumAttachPoints(UINT *puNumAttachPoints) /*const*/ = 0;

	//==============================================================================
	// Method Name: GetAttachPointIndexByName(...)
	//------------------------------------------------------------------------------
	/// Retrieves the index of an attach point in mesh, by its name.
	///
	/// \param[in] strName			The attach point name
	/// \param[out] puIndex			The attach point index
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetAttachPointIndexByName(PCSTR strName, UINT *puIndex) /*const*/ = 0;

	//==============================================================================
	// Method Name: GetAttachPointNameByIndex(...)
	//------------------------------------------------------------------------------
	/// Retrieves the name of an attach point in mesh, by its index.
	///
	/// \param[in] uIndex			The attach point index
	/// \param[out] pstrName		The attach point name
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetAttachPointNameByIndex(UINT uIndex, PCSTR *pstrName) /*const*/ = 0;

	//==============================================================================
	// Method Name: GetAttachPointChildren(...)
	//------------------------------------------------------------------------------
	/// Retrieves the indices of attach points that are children of the given attach point.
	///
	/// \param[in] uParentIndex			The parent's attach point index
	/// \param[out] pauChildrenIndices	The children's attach points indices
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetAttachPointChildren(UINT uParentIndex, CMcDataArray<UINT> *pauChildrenIndices) /*const*/ = 0;

	//==============================================================================
	// Method Name: GetAnimationsNames(...)
	//------------------------------------------------------------------------------
	/// Retrieves names of all animations of the mesh.
	///
	/// \param[out] pastrNames			The array of names
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetAnimationsNames(CMcDataArray<PCSTR> *pastrNames) /*const*/ = 0;

	//@}
};

//==================================================================================
// Interface Name: IMcNativeMeshFile
//----------------------------------------------------------------------------------
/// The interface for mesh in MapCore format loaded from mesh file (*.mesh)
//==================================================================================
class IMcNativeMeshFile : public virtual IMcNativeMesh
{
protected:

	virtual ~IMcNativeMeshFile() {};

public:

	enum
	{
		//==============================================================================
		/// Mesh unique ID for this interface
		//==============================================================================
		MESH_TYPE = 2
	};

	/// \name Mesh File
	//@{

	//==============================================================================
	// Method Name: SetMeshFile(...)
	//------------------------------------------------------------------------------
	/// Sets the mesh file.
	///
	/// \param[in]	strMeshFile		The mesh file name
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetMeshFile(PCSTR strMeshFile) = 0;

	//==============================================================================
	// Method Name: GetMeshFile(...)
	//------------------------------------------------------------------------------
	/// Retrieves the mesh File.
	///
	/// \param[out] pstrMeshFile	The mesh file name
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetMeshFile(PCSTR *pstrMeshFile) const = 0;

	//@}

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a mesh from mesh file.
	///
	/// \param[out]	ppMesh				The pointer to the created mesh
	/// \param[in]	strMeshFile			The mesh file name
	///	\param[in]	bUseExisting		If true and some mesh based on this file already 
	///									exists, it will be returned instead of creating a new one
	///	\param[out]	pbExistingUsed		Whether an existing mesh based on this file was used
	///									or a new one was created
	///
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcNativeMeshFile **ppMesh,
		PCSTR strMeshFile, 
		bool bUseExisting = true,
		bool *pbExistingUsed = NULL);

	//@}
};

//==================================================================================
// Interface Name: IMcNativeLODMeshFile
//----------------------------------------------------------------------------------
/// The interface for levels-of-detail mesh in MapCore format loaded from LOD mesh file (*.lodmesh)
//==================================================================================
class IMcNativeLODMeshFile : public virtual IMcNativeMesh
{
protected:

	virtual ~IMcNativeLODMeshFile() {};

public:

	enum
	{
		//==============================================================================
		/// Mesh unique ID for this interface
		//==============================================================================
		MESH_TYPE = 3
	};

	/// \name LOD Mesh File
	//@{

	//==============================================================================
	// Method Name: SetLODMeshFile(...)
	//------------------------------------------------------------------------------
	/// Sets the LOD mesh file.
	///
	/// \param[in]	strLODMeshFile		The LOD mesh file name
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetLODMeshFile(PCSTR strLODMeshFile) = 0;

	//==============================================================================
	// Method Name: GetLODMeshFile(...)
	//------------------------------------------------------------------------------
	/// Retrieves the LOD mesh file.
	///
	/// \param[out] pstrLODMeshFile		The LOD mesh file name
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetLODMeshFile(PCSTR *pstrLODMeshFile) const = 0;

	//@}

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a mesh from LOD mesh file.
	///
	/// \param[out]	ppMesh				The pointer to the created mesh
	/// \param[in]	strLODMeshFile		The LOD mesh file name
	///	\param[in]	bUseExisting		If true and some mesh based on this file already 
	///									exists, it will be returned instead of creating a new one
	///	\param[out]	pbExistingUsed		Whether an existing mesh based on this file was used
	///									or a new one was created
	///
	/// \return
	///     - Status result
	//==============================================================================
	static OVERLAYMANAGER_API IMcErrors::ECode Create(
		IMcNativeLODMeshFile **ppMesh,
		PCSTR strLODMeshFile, 
		bool bUseExisting = true,
		bool *pbExistingUsed = NULL);

	//@}
};
