#pragma once
//==================================================================================
/// \file IMcProceduralGeometryItem.h
/// ProceduralGeometry item interface
//==================================================================================

#include "McExports.h"
#include "McCommonTypes.h"
#include "CMcDataArray.h"
#include "OverlayManager/IMcSymbolicItem.h"

//==================================================================================
// Interface Name: IMcProceduralGeometryItem
//----------------------------------------------------------------------------------
///	Interface for procedural geometry items.
//==================================================================================
class IMcProceduralGeometryItem : public virtual IMcSymbolicItem
{
protected:

	virtual ~IMcProceduralGeometryItem() {};

public:

	//==============================================================================
	// Enum Name: ERenderingMode
	//------------------------------------------------------------------------------
	/// Rendering Mode
	//==============================================================================
	enum ERenderingMode
	{
		/// points are rendered, array of connection indices should contain their indices
		ERM_POINTS		= 1,

		/// 2-point line segments are rendered, array of connection indices should contain 
		/// pairs of indices (each 2 indices define a line segment)
		ERM_LINES		= 2,

		/// filled triangles are rendered, array of connection indices should contain 
		/// triplets of indices (each 3 indices define a triangle)
		ERM_TRIANGLES	= 4
	};

	/// \name Geometry coordinate system
	//@{

	//==============================================================================
	// Method Name: GetProceduralGeometryCoordinateSystem(...)
	//------------------------------------------------------------------------------
	/// Retrieves the procedural geometry's coordinate system.
	///
	/// \param[out] peProceduralGeometryCoordinateSystem	The procedural geometry's coordinate system
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetProceduralGeometryCoordinateSystem(
		EMcPointCoordSystem *peProceduralGeometryCoordinateSystem) const = 0;
	
	//@}
};

//==================================================================================
// Interface Name: IMcManualGeometryItem
//----------------------------------------------------------------------------------
///	Interface for manual geometry items.
/// 
//==================================================================================
class IMcManualGeometryItem : public virtual IMcProceduralGeometryItem
{
protected:

	virtual ~IMcManualGeometryItem() {};

public:

	enum
	{
		//==============================================================================
		/// Node unique ID for this interface
		//==============================================================================
		NODE_TYPE = 70
	};

public:

	/// \name Create
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates a manual geometry item
	///
	/// The item will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Release() method is called,
	/// when its (or its parents) Disconnect() method is called,
	/// or when its object scheme is destroyed.
	///
	/// \param[out] ppItem						The newly created item
	/// \param[in] uItemSubTypeBitField			The item sub type (see IMcObjectSchemeItem::EItemSubTypeFlags)
	/// \param[in] eProceduralGeometryCoordinateSystem	
	///											The item coordinate system (see #EMcPointCoordSystem)
	/// \param[in] eRenderingMode				The rendering mode (see #ERenderingMode).
	/// \param[in] pTexture						The optional texture (can be NULL).
	/// \param[in] auConnectionIndices			The connection indices containing indices of points
	///											from \a aPointsCoordinates (single indices, pairs or 
	///											triplets depending on \a eRenderingMode, see #ERenderingMode).
	/// \param[in] uNumConnectionIndices		The number of connection indices
	/// \param[in] aPointsCoordinates			The points' coordinates array
	/// \param[in] aPointsTextureCoordinates	The points' texture coordinates array
	///											(can be NULL if texture is not used)
	/// \param[in] aPointsColors				The points' colors array
	///											(can be NULL if texture is used)
	/// \param[in] uNumPoints					The number of points
	///
	/// \note
	///		Either texture or colors should be defined (if both are defined, texture is used 
	///		and colors are ignored.
	///
	/// \return
	///     - Status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcManualGeometryItem **ppItem,
		UINT uItemSubTypeBitField,
		EMcPointCoordSystem eProceduralGeometryCoordinateSystem,
		ERenderingMode eRenderingMode,
		IMcTexture *pTexture = NULL,
		const UINT auConnectionIndices[] = NULL,
		UINT uNumConnectionIndices = 0,
		const SMcVector3D aPointsCoordinates[] = NULL,
		const SMcFVector2D aPointsTextureCoordinates[] = NULL,
		const SMcBColor aPointsColors[] = NULL,
		UINT uNumPoints = 0);

	//@}

	/// \name Clone
	//@{

	//==============================================================================
	// Method Name: Clone(...)
	//------------------------------------------------------------------------------
	/// Clone the item.
	///
	/// \param[out] ppClonedItem	The newly cloned item
	/// \param[in]  pObject			Optional object, to take the values of private properties from
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Clone(
		IMcManualGeometryItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}

	/// \name Rendering Mode
	//@{

	//==============================================================================
	// Method Name: SetRenderingMode(...)
	//------------------------------------------------------------------------------
	/// Defines the rendering mode as a shared or private property.
	///
	/// \param[in] eRenderingMode		The rendering mode (see #ERenderingMode).
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetRenderingMode(
		ERenderingMode eRenderingMode,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetRenderingMode(...)
	//------------------------------------------------------------------------------
	/// Retrieves the rendering mode property defined by SetXXX().
	///
	/// \param[out] peRenderingMode		The rendering mode.
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetRenderingMode(
		ERenderingMode *peRenderingMode,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Texture
	//@{

	//==============================================================================
	// Method Name: SetTexture(...)
	//------------------------------------------------------------------------------
	/// Defines the texture resource as a shared or private property.
	///
	/// \param[in] pTexture				The optional texture (can be NULL).
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetTexture(
		IMcTexture *pTexture,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetTexture(...)
	//------------------------------------------------------------------------------
	/// Retrieves the texture resource property defined by SetXXX().
	///
	/// \param[out] ppTexture			The texture.
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetTexture(
		IMcTexture **ppTexture,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//@}

	/// \name Connection Indices and Points
	//@{

	//==============================================================================
	// Method Name: SetConnectionIndices(...)
	//------------------------------------------------------------------------------
	/// Sets the connection indices as a shared or private property.
	///
	/// \param[in] auConnectionIndices	The connection indices containing indices of points
	///									from points array (single indices, pairs or triplets depending 
	///									on \a eRenderingMode, see #ERenderingMode).
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetConnectionIndices(
		const IMcProperty::SArrayProperty<UINT> auConnectionIndices,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetConnectionIndices(...)
	//------------------------------------------------------------------------------
	/// Retrieves the connection indices property defined by SetXXX().
	///
	/// \param[out] pauConnectionIndices	The connection indices set by SetConnectionIndices()
	///										The parameter's meaning depends on \a puPropertyID
	///										and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetConnectionIndices(
		IMcProperty::SArrayProperty<UINT> *pauConnectionIndices, 
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetPointsCoordinates(...)
	//------------------------------------------------------------------------------
	/// Sets the points' world coordinates.
	///
	/// \param[in] aPointsCoordinates	The points' world coordinates array
	///									The parameter's meaning depends on \a uPropertyID
	///									and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID			The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetPointsCoordinates(
		const IMcProperty::SArrayProperty<SMcVector3D> aPointsCoordinates,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetPointsCoordinates(...)
	//------------------------------------------------------------------------------
	/// Retrieves the points' world coordinates defined by SetXXX().
	///
	/// \param[out] paPointsCoordinates	The points' world coordinates array set by 
	///									SetPointsCoordinates() or SetPointsData()
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetPointsCoordinates(
		IMcProperty::SArrayProperty<SMcVector3D> *paPointsCoordinates, 
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetPointsTextureCoordinates(...)
	//------------------------------------------------------------------------------
	/// Sets the points' texture coordinates.
	///
	/// \param[in] aPointsTextureCoordinates	The points' texture coordinates array
	///											(can be empty if texture is not used)
	///											The parameter's meaning depends on \a uPropertyID
	///											and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID					The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetPointsTextureCoordinates(
		const IMcProperty::SArrayProperty<SMcFVector2D> aPointsTextureCoordinates,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetPointsTextureCoordinates(...)
	//------------------------------------------------------------------------------
	/// Retrieves the points' texture coordinates defined by SetXXX().
	///
	/// \param[out] paPointsTextureCoordinates	The points' texture coordinates array set by 
	///											SetPointsTextureCoordinates() or SetPointsData()
	///											The parameter's meaning depends on \a puPropertyID
	///											and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID				The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetPointsTextureCoordinates(
		IMcProperty::SArrayProperty<SMcFVector2D> *paPointsTextureCoordinates,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetPointsColors(...)
	//------------------------------------------------------------------------------
	/// Sets the points' colors.
	///
	/// \param[in] aPointsColors				The points colors array
	///											(can be empty if texture is used)
	///											The parameter's meaning depends on \a uPropertyID
	///											and \a uObjectStateToServe (see note below).
	/// \param[in] uPropertyID					The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	///	\note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of removing object-state properties (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is ignored.
	/// - The property of zero-state (\a uObjectStateToServe == 0) cannot be removed. 
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetPointsColors(
		const IMcProperty::SArrayProperty<SMcBColor> aPointsColors,
		UINT uPropertyID = IMcProperty::EPPI_SHARED_PROPERTY_ID,
		BYTE uObjectStateToServe = 0) = 0;

	//==============================================================================
	// Method Name: GetPointsColors(...)
	//------------------------------------------------------------------------------
	/// Retrieves the points' colors defined by SetXXX().
	///
	/// \param[out] paPointsColors		The points' colors array set by 
	///									SetPointsColors() or SetPointsData()
	///									The parameter's meaning depends on \a puPropertyID
	///									and \a uObjectStateToServe (see SetXXX()).
	/// \param[out] puPropertyID		The private property ID or one of the following special values:
	///									IMcProperty::EPPI_SHARED_PROPERTY_ID, IMcProperty::EPPI_NO_STATE_PROPERTY_ID, IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID.
	/// \param[in] uObjectStateToServe	The property version to be dealt with (index of object-state to be served by this property version).
	///
	/// \note
	/// - In case of shared property (\a uPropertyID == IMcProperty::EPPI_SHARED_PROPERTY_ID), 
	///   the first parameter is the shared property value itself.
	/// - In case of private property (\a uPropertyID < IMcProperty::EPPI_FIRST_RESERVED_ID), 
	///   the first parameter is the default value for each object's private property.
	/// - In case of undefined object-state property (\a uPropertyID == IMcProperty::EPPI_NO_STATE_PROPERTY_ID 
	///   or IMcProperty::EPPI_NO_MORE_STATE_PROPERTIES_ID) the first parameter is of the property of zero-state.
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetPointsColors(
		IMcProperty::SArrayProperty<SMcBColor> *paPointsColors,
		UINT *puPropertyID = NULL,
		BYTE uObjectStateToServe = 0) const = 0;

	//==============================================================================
	// Method Name: SetPointsData(...)
	//------------------------------------------------------------------------------
	/// Sets the arrays of points' world coordinates, texture coordinates and colors as shared properties
	///
	/// \param[in] aPointsCoordinates			The points' coordinates array
	/// \param[in] aPointsTextureCoordinates	The points' texture coordinates array
	///											(can be NULL if texture is not used)
	/// \param[in] aPointsColors				The points' colors array
	///											(can be NULL if texture is used)
	/// \param[in] uNumPoints					The number of points
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode SetPointsData(
		const SMcVector3D aPointsCoordinates[],
		const SMcFVector2D aPointsTextureCoordinates[],
		const SMcBColor aPointsColors[],
		UINT uNumPoints) = 0;
	
	//==============================================================================
	// Method Name: GetPointsData(...)
	//------------------------------------------------------------------------------
	/// Retrieves the shared properties' values or private properties' default values of 
	///	points' world coordinates, texture coordinates and colors defined by SetXXX().
	///
	/// \param[out] paPointsCoordinates			The points' coordinates array
	/// \param[out] paPointsTextureCoordinates	The points' texture coordinates array
	/// \param[out] paPointsColors				The points' colors array
	///
	/// \return
	///     - Status result
	//==============================================================================
	virtual IMcErrors::ECode GetPointsData(
		CMcDataArray<SMcVector3D> *paPointsCoordinates,
		CMcDataArray<SMcFVector2D> *paPointsTextureCoordinates,
		CMcDataArray<SMcBColor> *paPointsColors) const = 0;

	//@}
};

