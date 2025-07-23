#pragma once

//===========================================================================
/// \file IMcSpatialQueries.h
/// Interface for terain/objects spatial queries
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcBase.h"
#include "IMcDestroyable.h"
#include "McCommonTypes.h"
#include "CMcDataArray.h"
#include "SMcScanGeometry.h"
#include "Map/IMcDtmMapLayer.h"
#include "OverlayManager/IMcTexture.h"

class IMcMapTerrain;
class IMcMapLayer;
class IMcVectorMapLayer;
class IMcStaticObjectsMapLayer;
class IMcOverlay;
class IMcImageCalc;
class IMcOverlayManager;
class IMcMapDevice;
class IMcMapViewport;
class IMcSectionMapViewport;
class IMcGridCoordinateSystem;
class IMcHeatMapViewport;

//===========================================================================
// Interface Name: IMcSpatialQueries
//---------------------------------------------------------------------------
/// Interface for terrain/objects spatial queries
//===========================================================================
class IMcSpatialQueries : virtual public IMcBase
{
protected:
    virtual ~IMcSpatialQueries() {}

public:
	
	enum
	{
		//==============================================================================
		/// Unique ID for IMcSpatialQueries-derived interface
		//==============================================================================
		INTERFACE_TYPE = 1
	};

	/// Spatial queries creation parameters
	struct SCreateData
	{
		/// previously created rendering device interface
		IMcMapDevice					*pDevice;

		/// the coordinate system of the queries/viewport
		IMcGridCoordinateSystem			*pCoordinateSystem;

		/// previously created an interface of a world of objects
		IMcOverlayManager				*pOverlayManager;

		/// user-defined viewport ID to be used in viewport visibility selectors 
		///	in order to define objects visibility for this instance of queries/viewport
		UINT							uViewportID;

		SCreateData() : pDevice(NULL), pCoordinateSystem(NULL), pOverlayManager(NULL), uViewportID(MC_EMPTY_ID) {}
	};

	/// Target type bits (for using in bit field)
	enum EIntersectionTargetType
	{
		EITT_NONE						= 0x0000,	///< no target
		EITT_DTM_LAYER					= 0x0001,	///< DTM layer of each terrain
		EITT_STATIC_OBJECTS_LAYER		= 0x0002,	///< static objects map layer of each terrain
		EITT_VISIBLE_VECTOR_LAYER		= 0x0004,	///< visible vector map layers of each terrain
		EITT_NON_VISIBLE_VECTOR_LAYER	= 0x0008,	///< not visible vector map layers of each terrain
		EITT_OVERLAY_MANAGER_OBJECT		= 0x0010,	///< items of overlay manager objects
		EITT_ANY_TARGET					= 0xFFFF	///< any of the below
	};
	
	/// Query precision (which levels of detail of terrain or objects to use)
	///
	/// \note Default value meaning differs for standalone spatial queries and for viewport
	enum EQueryPrecision 
	{
		/// default precision: 
		/// - for standalone spatial queries implementation: equivalent to EQueryPrecision::EQP_HIGHEST
		/// - for viewport implementing spatial queries: only currently shown levels of detail 
		///   are used; query result is compatible with what is displayed in the viewport 
		EQP_DEFAULT = 0,

		/// default-plus-lowest precision: 
		/// - for standalone spatial queries implementation: equivalent to EQueryPrecision::EQP_LOWEST
		/// - for viewport implementing spatial queries: currently shown levels of detail 
		///   are used first, if no intersection with shown terrain is detected, 
		///   complete with lowest level of detail loading it if necessary;
		///   query result is compatible with what is displayed in the viewport, completed 
		///   with lowest level of detail where the terrain is not displayed at all
		EQP_DEFAULT_PLUS_LOWEST,
		
		/// highest precision: highest level of detail is used
		EQP_HIGHEST,

		/// high precision: rather high level of detail is used
		EQP_HIGH,

		/// medium precision: medium level of detail is used
		EQP_MEDIUM,

		/// low precision: rather low level of detail is used
		EQP_LOW,

		/// lowest precision: lowest level of detail is used
		EQP_LOWEST
	};

	/// No DTM result for line or area of sight queries
	enum ENoDTMResult
	{
		/// when no DTM in query area - fail the query.
		ENDR_FAIL = 0,

		/// when no DTM in query area - return all unknown areas as visible.
		ENDR_VISIBLE,

		/// when no DTM in query area - return all unknown areas as invisible.
		ENDR_INVISIBLE
	};

	/// Result of intersection with object item
	struct SObjectItemFound
	{
		/// Item part found
		enum EItemPart
		{
			EAP_VERTEX,					///< vertex
			EAP_LINE_SEGMENT,			///< line segment
			EAP_ARC_SEGMENT,			///< arc segment
			EAP_ARROW_HEAD,				///< arrow head
			EAP_MESH_PART,				///< mesh part
			EAP_INSIDE					///< inside (closed shape inside or item as a whole)
		};

		IMcObject			*pObject;	///< object found
		IMcObjectSchemeItem *pItem;		///< object scheme item found
		UINT				uSubItemID;	///< sub-item ID for symbolic items only
		EItemPart			ePartFound;	///< item part found
		UINT				uPartIndex;	///< index of item's part found (vertex, segment or mesh part) 
										///< if relevant (according to \a ePartFound, see #EItemPart)
		SObjectItemFound()
		{
			memset(this, 0, sizeof(*this));
			// uTargetID should be zero in case it will be filled only with 32 or 64 bits;
			uSubItemID = MC_EMPTY_ID;
		}

		SObjectItemFound(const SObjectItemFound &Other) 
		{ memcpy(this, &Other, sizeof(*this)); }

		SObjectItemFound& operator=(const SObjectItemFound &Other) 
		{ memcpy(this, &Other, sizeof(*this)); return *this; }
	};

	/// static-object's contour (points and relative height)
	struct SStaticObjectContour
	{
		CMcDataArray<SMcVector3D>	aPoints;			///< contour's points
		double						dRelativeHeight;	///< contour's relative height
	};

	/// Result of intersection with single target.
	///
	///	Members other than #eTargetType and are relevant only for some types of targets,
	///	otherwise they are undefined.
	struct STargetFound
	{
		/// type of the target found; 
		EIntersectionTargetType				eTargetType;

		/// intersection point (relevant for intersection and scan-at-point queries)
		SMcVector3D							IntersectionPoint;

		/// intersection point coordinate system: world, screen or image
		EMcPointCoordSystem					eIntersectionCoordSystem;

		/// terrain (relevant only if #eTargetType is equal to either 
		/// #EITT_DTM_LAYER, #EITT_STATIC_OBJECTS_LAYER, 
		IMcMapTerrain						*pTerrain;

		/// map layer (relevant only if #eTargetType is equal to either 
		/// #EITT_DTM_LAYER, #EITT_STATIC_OBJECTS_LAYER, 
		/// #EITT_VISIBLE_VECTOR_LAYER or #EITT_NON_VISIBLE_VECTOR_LAYER)
		IMcMapLayer							*pTerrainLayer;

		/// target index (relevant only if #eTargetType is equal to either 
		/// #EITT_STATIC_OBJECTS_LAYER, #EITT_VISIBLE_VECTOR_LAYER or #EITT_NON_VISIBLE_VECTOR_LAYER):
		/// - static objects: for number of bits used call IMcStaticObjectsMapLayer::GetObjectIDBitCount()
		/// - vector layers: \p uTargetID.u64Bit only is used
		SMcVariantID						uTargetID;

		/// object item data (relevant only if #eTargetType is equal to 
		/// #EITT_OVERLAY_MANAGER_OBJECT or #EITT_VISIBLE_VECTOR_LAYER)
		SObjectItemFound					ObjectItemData;

		/// optional contours of static object found (relevant only if #eTargetType is equal to #EITT_STATIC_OBJECTS_LAYER)
		CMcDataArray<SStaticObjectContour>	aStaticObjectContours;
		
		STargetFound()
		{
			memset((void*)this, 0, sizeof(*this));
			// uTargetID should be zero in case it will be filled only with 32 or 64 bits;
			ObjectItemData.uSubItemID = MC_EMPTY_ID;
		}

		STargetFound(const STargetFound &Other) 
		{
			memcpy((void*)this, (void*)&Other, sizeof(*this));
			IMcDataArray<SStaticObjectContour> *paStaticObjectContoursArray = aStaticObjectContours.GetDataArray();
			if (paStaticObjectContoursArray != NULL)
			{
				paStaticObjectContoursArray->AddedToContainer();
			}
		}

		STargetFound& operator=(const STargetFound &Other) 
		{
			IMcDataArray<SStaticObjectContour> *paOldStaticObjectContoursArray = aStaticObjectContours.GetDataArray();
			memcpy((void*)this, (void*)&Other, sizeof(*this)); return *this;
			IMcDataArray<SStaticObjectContour> *paNewStaticObjectContoursArray = aStaticObjectContours.GetDataArray();

			if (paNewStaticObjectContoursArray != paOldStaticObjectContoursArray)
			{
				if (paOldStaticObjectContoursArray != NULL)
				{
					paOldStaticObjectContoursArray->RemovedFromContainer();
				}
				if (paNewStaticObjectContoursArray != NULL)
				{
					paNewStaticObjectContoursArray->AddedToContainer();
				}
			}
		}

	};

	/// Result of line of sight 
	struct SLineOfSightPoint 
	{
		SMcVector3D Point;		///< the point
		bool		bVisible;	///< the visibility (from the scouter) of the segment starting with this point
	};

	/// Visibility color type
	enum EPointVisibility
	{
		EPV_SEEN,				///< color of point seen by the scouter
		EPV_UNSEEN,				///< color of point unseen by the scouter
		EPV_UNKNOWN,			///< color of point of unknown visibility (if there are areas 
								///< without DTM between the scouter and the point)
		EPV_OUT_OF_QUERY_AREA,	///< color of point beyond the target polygon
		EPV_SEEN_STATIC_OBJECT,	///< color of point static object seen by the scouter (irrelevant in IMcSpatialQueries, 
								///<  used only in IMcSightPresentationItemParams::SetSightColor() and IMcSightPresentationItemParams::GetSightColor())
		EPV_ASYNC_CALCULATING,	///< line color of sight-presentation closed shapes when the precise sight-presentation is being calculated 
								///<  asynchronously, the calculation has not been completed yet (irrelevant in IMcSpatialQueries, used only in 
								///<  IMcSightPresentationItemParams::SetSightColor() and IMcSightPresentationItemParams::GetSightColor())
		EPV_NUM					///< number of the enum's members (not to be used as a valid type)
	};

	/// Traversability type
	enum EPointTraversability
	{
		EPT_TRAVERSABLE,		///< point is traversable
		EPT_UNTRAVERSABLE,		///< point is not traversable
		EPT_UNKNOWN,			///< point traversability is unknown
		EPT_ASYNC_CALCULATING,	///< line color of traversability-presentation line/arrow when the traversability is being calculated asynchronously, 
								///<  the calculation has not been completed yet (irrelevant in IMcSpatialQueries, used only in 
								///<  IMcLineItem::SetTraversabilityColor(), IMcLineItem::GetTraversabilityColor(), 
								///<  IMcArrowItem::SetTraversabilityColor(), IMcArrowItem::GetTraversabilityColor())
		EPT_NUM					///< number of the enum's members (not to be used as a valid type)
	};

	/// Determines how to sum results for each point  when using multiple scouters in Area of Sight methods.
	enum EScoutersSumType
	{
		ESST_OR,		///< For each point return 1 if point is visible by at least one scouter, . Otherwise return 0.
		ESST_ADD,		///< For each point return the number of scouters it is seen by.
		ESST_ALL		///< For each point return all the scouters IDs that it is seen by. Limited to 32 scouters The first scouter is indexed to 1.
	};

	/// The Areas of sight - described as a rectangular matrix
	struct SAreaOfSightMatrix
	{
		UINT		uWidth;							///< The matrix width
		UINT		uHeight;						///< The matrix height
		float		fAngle;							///< The matrix angle relative to earth XY axes
		float		fTargetResolutionInMeters;		///< The matrix resolution in meters (the value given in GetXXXAreaOfSight(), 
													///<   or maximum allowed by graphics device
		float		fTargetResolutionInMapUnitsX;	///< The matrix resolution in map units along X axis 
		float		fTargetResolutionInMapUnitsY;	///< The matrix resolution in map units along Y axis 
		SMcVector3D LeftTopPoint;					///< The matrix left-top corner
		SMcVector3D RightTopPoint;					///< The matrix right-top corner
		SMcVector3D LeftBottomPoint;				///< The matrix left-bottom corner
		SMcVector3D RightBottomPoint;				///< The matrix right-bottom corner	
		
		CMcDataArray<SMcBColor,true>
			aPointsVisibilityColors;				///< The result for each matrix point
	};

	/// Interface for the result of area of sight
	class IAreaOfSight : public IMcDestroyable
	{
	protected:
			virtual ~IAreaOfSight() {}
	public:
	
		/// \name Matrix
		//@{

		//==============================================================================
		// Method Name: GetAreaOfSightMatrix()
		//------------------------------------------------------------------------------
		/// Retrieves the calculated matrix containing the area of sight
		///
		/// \param[out]	pAreaOfSight			the calculated matrix containing the area of sight.
		///								
		/// \param[in]	bFillPointsVisibility	whether to fill \a pAreaOfSight->aPointsVisibilityColors
		///
		/// \return
		///     - status result
		//==============================================================================
		virtual IMcErrors::ECode GetAreaOfSightMatrix(SAreaOfSightMatrix *pAreaOfSight, bool bFillPointsVisibility) const = 0;

		//@}

		/// \name Point Visibility
		//@{

		//==============================================================================
		// Method Name: GetPointVisibilityColor()
		//------------------------------------------------------------------------------
		/// Quickly calculates (O(1)) a given point's visibility color
		///
		/// \param[in]	Point					the given point to test
		/// \param[out]	pPointVisibilityColor	the point's visibility color
		///
		/// \return
		///     - status result
		//==============================================================================
		virtual IMcErrors::ECode GetPointVisibilityColor(const SMcVector3D &Point,
			SMcBColor *pPointVisibilityColor) const = 0;

		//==============================================================================
		// Method Name: GetPointVisibilityColorsSurrounding()
		//------------------------------------------------------------------------------
		/// Quickly calculates a matrix of visibility colors surrounding a given point.
		///
		/// \param[in]	Point					the given point that is center of the matrix
		/// \param[in]	NumVisibilityColorsX	the visibility colors matrix width
		/// \param[in]	NumVisibilityColorsY	the visibility colors matrix length
		/// \param[out]	PointVisibilityColors	the point's visibility color matrix. 
		///										allocated by the user to size NumVisibilityColorsX * NumVisibilityColorsY.
		///
		/// \return
		///     - status result
		//==============================================================================
		virtual IMcErrors::ECode GetPointVisibilityColorsSurrounding(const SMcVector3D &Point,
			UINT NumVisibilityColorsX, UINT NumVisibilityColorsY,
			UINT32 PointVisibilityColors[]) const = 0;

		//==============================================================================
		// Method Name: GetVisibilityColors()
		//------------------------------------------------------------------------------
		/// Retrieves visibility colors array defined by the user in GetXXXAreaOfSight()
		///
		/// \param[out]   aVisibilityColors[]	The array (of length #EPV_NUM) of points' visibility 
		///                                     colors per #EPointVisibility as an index 
		///                                     (\a aVisibilityColors[#EPV_SEEN] is a color for seen points etc.)
		/// \return
		///     - status result
		//==============================================================================
		virtual IMcErrors::ECode GetVisibilityColors(SMcBColor aVisibilityColors[EPV_NUM]) const = 0;

		//@}

		/// \name Save & Load
		//@{

		//================================================================
		// Method Name: Save(...)
		//----------------------------------------------------------------
		/// Saves the interface data to a file
		///
		/// \param[in]	strFileName			The name of a file to save to
		///
		/// \return
		///     - Status result
		//================================================================
		virtual IMcErrors::ECode Save(PCSTR strFileName) = 0;

		//================================================================
		// Method Name: Load(...)
		//----------------------------------------------------------------
		/// Loads the interface previously saved by Save() from a file. 
		///
		/// \param[out]	ppAreaOfSight		The loaded IAreaOfSight interface
		/// \param[in]	strFileName			The name of the file to load from
		///
		/// \return
		///     - Status result
		//================================================================
		static SCENEMANAGER_API IMcErrors::ECode Load(IAreaOfSight **ppAreaOfSight, PCSTR strFileName);

		//@}
	};

	/// Area of sight polygons
	struct SPolygonsOfSight
	{
		CMcDataArray<CMcDataArray<SMcVector3D> >	aaContoursPoints;		///< array of contours (each one is array of points)
	};

	/// IDs of static objects along with their static objects map layer
	struct SStaticObjectsIDs
	{
		IMcStaticObjectsMapLayer							*pMapLayer;				///< static objects map layer
		CMcDataArray<SMcVariantID>							auIDs;					///< array of IDs of static objects 
																					///<  (for number of bits used call IMcStaticObjectsMapLayer::GetObjectIDBitCount())
		CMcDataArray<CMcDataArray<SStaticObjectContour> >	aaStaticObjectsContours;///< optional array of contours for each static object in #auIDs

		SStaticObjectsIDs() : pMapLayer(NULL) {}
	};

	/// Result of traversability along line
	struct STraversabilityPoint
	{
		SMcVector3D Point;						///< the point
		EPointTraversability eTraversability;   ///< the traversability of the segment starting with this point
												///<  (#EPT_ASYNC_CALCULATING and #EPT_NUM are not used)
	};

	class ICoverageQuality : public IMcDestroyable
	{
	protected:
		virtual ~ICoverageQuality() {}
	public:

		enum ETargetType
		{
			ETT_STANDING,
			ETT_WALKING,
			ETT_VEHICLE
		};

		struct SQualityParams 
		{
			float fStandingRadius;
			float fWalkingRadius;
			float fVehicleRadius;
			UINT  uCellFactor;
		};

		static SCENEMANAGER_API IMcErrors::ECode Create(ICoverageQuality **ppCoverageQuality,
			const IMcSpatialQueries::SAreaOfSightMatrix &sAreaOfSightMatrix,
			const SMcBColor aVisibilityColors[IMcSpatialQueries::EPV_NUM],
			const SQualityParams &QualityParams);

		virtual IMcErrors::ECode GetQuality(const SMcVector3D &Location, ETargetType eType, 
			float fMovementAngle, short *pnQuality) const = 0;
	};

	/// Additional Slopes Data
	struct SSlopesData
	{
		float fMaxSlope;	///< Max Slope in line
		float fMinSlope;	///< Min Slope in line
		float fHeightDelta;	///<Delta Height between first to last point
	};

	/// Asynchronous query callback receiving query results
	class IAsyncQueryCallback
#ifdef __EMSCRIPTEN__
		: public IMcEmscriptenCallbackRef
#endif

	{
	public:

		virtual ~IAsyncQueryCallback() {}

		//==============================================================================
		// Method Name: OnTerrainHeightResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetTerrainHeight() has been completed successfully; returns the results
		///
		/// \param[in]	bHeightFound	whether height found
		/// \param[in]	dHeight			height of highest target
		/// \param[in]	pNormal			optional normal of highest target at world point (if was requested)
		//==============================================================================
		virtual void OnTerrainHeightResults(bool bHeightFound, double dHeight, const SMcVector3D *pNormal) {}

		//==============================================================================
		// Method Name: OnTerrainHeightsAlongLineResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetTerrainHeightsAlongLine() has been completed successfully; returns the results
		///
		/// \param[in]	aPointsWithHeights	optional array of calculated points with heights (if was requested)
		/// \param[in]	afSlopes			optional array of calculated slopes: a slope per point, 0 is a horizon (if was requested)
		/// \param[in]	pSlopesData			optional slopes data (if was requested)
		//==============================================================================
		virtual void OnTerrainHeightsAlongLineResults(const CMcDataArray<SMcVector3D> &aPointsWithHeights, const CMcDataArray<float> &afSlopes, 
			const IMcSpatialQueries::SSlopesData *pSlopesData) {}

		//==============================================================================
		// Method Name: OnExtremeHeightPointsInPolygonResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetExtremeHeightPointsInPolygon() has been completed successfully; returns the results
		///
		/// \param[in]	bPointsFound		whether points found
		/// \param[in]	pHighestPoint		optional highest point found (if was requested)
		/// \param[in]	pLowestPoint		optional lowest point found (if was requested)
		//==============================================================================
		virtual void OnExtremeHeightPointsInPolygonResults(
			bool bPointsFound, const SMcVector3D *pHighestPoint, const SMcVector3D *pLowestPoint) {}

		//==============================================================================
		// Method Name: OnTerrainHeightMatrixResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetTerrainHeightMatrix() has been completed successfully; returns the results
		///
		/// \param[in]	adHeightMatrix		calculated matrix - array of heights of length \a uNumHorizontalPoints * \a uNumVerticalPoints, 
		///									row by row bottom to top - in ascending order of \a y
		//==============================================================================
		virtual void OnTerrainHeightMatrixResults(const CMcDataArray<double> &adHeightMatrix) {}

		//==============================================================================
		// Method Name: OnTerrainAnglesResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetTerrainAngles() has been completed successfully; returns the results
		///
		/// \param[in]	dPitch			vehicle's pitch [deg]
		/// \param[in]	dRoll			vehicle's roll [deg]
		//==============================================================================
		virtual void  OnTerrainAnglesResults(
			double dPitch, double dRoll) {}

		//==============================================================================
		// Method Name: OnRayIntersectionResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetRayIntersection() has been completed successfully; returns the results
		///	
		/// \param[in]	bIntersectionFound		whether at least one intersection found
		/// \param[in]	pIntersection			optional nearest intersection (if was requested)
		/// \param[in]	pNormal					optional normal (if was requested)
		/// \param[in]	pdDistance				optional distance between the ray origin and the nearest 
		///										intersection point or NULL if should not be computed.
		//==============================================================================
		virtual void OnRayIntersectionResults(
			bool bIntersectionFound,
			const SMcVector3D *pIntersection,
			const SMcVector3D *pNormal,
			const double *pdDistance) {}

		//==============================================================================
		// Method Name: OnRayIntersectionTargetsResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetRayIntersectionTargets() has been completed successfully; returns the results
		///	
		/// \param[in]	aIntersections			intersections with requested targets 
		//==============================================================================
		virtual void OnRayIntersectionTargetsResults(const CMcDataArray<STargetFound> &aIntersections) {}

		//==============================================================================
		// Method Name: OnLineOfSightResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetLineOfSight() has been completed successfully; returns the results
		///
		/// \param[in]	aPoints						A line running from the Scouter to the target,
		///											each point consists of its coordinate and whether it is seen or not.
		///	\param[in]	dCrestClearanceAngle		The angle above horizon from the scouter to
		///											the last visible terrain location.
		///	\param[in]	dCrestClearanceDistance	The horizontal distance from the scouter to
		///											the last visible terrain location.
		//==============================================================================
		virtual void OnLineOfSightResults(
			const CMcDataArray<IMcSpatialQueries::SLineOfSightPoint> &aPoints,
			double dCrestClearanceAngle,
			double dCrestClearanceDistance) {}

		//==============================================================================
		// Method Name: OnPointVisibilityResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetPointVisibility() has been completed successfully; returns the results
		///
		/// \param[in]	bIsTargetVisible						Whether or not the target is seen from the scouter.
		///	\param[in]	pdMinimalTargetHeightForVisibility		optional height in which the target will be
		///														visible from the current scouter (if was requested).
		///	\param[in]	pdMinimalScouterHeightForVisibility		optional height in which the scouter will 
		///														see the current target (if was requested).
		//==============================================================================
		virtual void OnPointVisibilityResults(
			bool bIsTargetVisible,
			const double *pdMinimalTargetHeightForVisibility,
			const double *pdMinimalScouterHeightForVisibility) {}

		//==============================================================================
		// Method Name: OnAreaOfSightResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetPolygonAreaOfSight(), IMcSpatialQueries::GetRectangleAreaOfSight(), IMcSpatialQueries::GetEllipseAreaOfSight()
		/// has been completed successfully; returns the results
		///
		/// \param[in]	pAreaOfSight				optional area-of-sight interface for the results (if was requested).
		///											the user should call IAreaOfSight::Destroy() to delete it. 
		///											relevant only for GPU calculation.
		/// \param[in]	aLinesOfSight				optional array of lines running from the Scouter to each aTargetPoints (if was requested).
		///											each point consists of its coordinate and whether it is seen or not.
		///											relevant only for CPU calculation.
		/// \param[in]	pSeenPolygons				optional multi-contour polygons that are seen (if was requested)
		/// \param[in]	pUnseenPolygons				optional multi-contour polygons that are unseen (if was requested)
		/// \param[in]	aSeenStaticObjects			optional array of IDs of seen static objects per layer (if was requested)
		//==============================================================================

		virtual void OnAreaOfSightResults(
			IAreaOfSight *pAreaOfSight,
			const CMcDataArray<CMcDataArray<IMcSpatialQueries::SLineOfSightPoint> > &aLinesOfSight,
			const SPolygonsOfSight *pSeenPolygons,
			const SPolygonsOfSight *pUnseenPolygons,
			const CMcDataArray<SStaticObjectsIDs> &aSeenStaticObjects) {}

		//==============================================================================
		// Method Name: OnBestScoutersLocationsResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetBestScoutersLocationsInEllipse() has been completed successfully; returns the results
		///
		/// \param[in]	aScouters				The X,Y,Z coordinates of the scouters found.
		//==============================================================================
		virtual void OnBestScoutersLocationsResults(const CMcDataArray<SMcVector3D> &aScouters) {}

		//==============================================================================
		// Method Name: OnLocationFromTwoDistancesAndAzimuthResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::LocationFromTwoDistancesAndAzimuth() has been completed successfully; returns the results
		///
		/// \param[in]	Target						The calculated target. If not found will return #v3MaxDouble
		//==============================================================================
		virtual void OnLocationFromTwoDistancesAndAzimuthResults(
			const SMcVector3D &Target) {}

		//==============================================================================
		// Method Name: OnDtmLayerTileGeometryByKeyResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetDtmLayerTileGeometryByKey() has been completed successfully; returns the results
		///
		/// \param[in]	TileGeometry				The tile's geometry data
		//==============================================================================
		virtual void OnDtmLayerTileGeometryByKeyResults(const IMcDtmMapLayer::STileGeometry &TileGeometry) {}

		//==============================================================================
		// Method Name: OnRasterLayerTileBitmapByKeyResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetRasterLayerTileBitmapByKey() has been completed successfully; returns the results
		///
		/// \param[in]	eBitmapPixelFormat			The tile's bitmap pixel format
		/// \param[in]	bBitmapFromTopToBottom		Whether or not tile's bitmap is from top to bottom
		/// \param[in]	BitmapSize					The tile's bitmap dimensions in pixels
		/// \param[in]	BitmapMargins				The tile's bitmap margins in pixels
		/// \param[in]	aBitmapBits					The tile's bitmap bits
		//==============================================================================
		virtual void OnRasterLayerTileBitmapByKeyResults(
			IMcTexture::EPixelFormat eBitmapPixelFormat, bool bBitmapFromTopToBottom,
			const SMcSize &BitmapSize, const SMcSize &BitmapMargins, const CMcDataArray<BYTE> &aBitmapBits) {}

		//==============================================================================
		// Method Name: OnRasterLayerColorByPointResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetRasterLayerColorByPoint() has been completed successfully; returns the results
		///
		/// \param[in]	Color						The color found
		//==============================================================================
		virtual void OnRasterLayerColorByPointResults(const SMcBColor &Color) {}

		//==============================================================================
		// Method Name: OnTraversabilityAlongLineResults()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query of IMcSpatialQueries::GetTraversabilityAlongLine() has been completed successfully; returns the results
		///
		/// \param[in]	aTraversabilitySegments		The array of segments found, each one has a flag whether it is traversable or not
		//==============================================================================
		virtual void OnTraversabilityAlongLineResults(const CMcDataArray<IMcSpatialQueries::STraversabilityPoint> &aTraversabilitySegments) {}

		//==============================================================================
		// Method Name: OnError()
		//------------------------------------------------------------------------------
		/// Called when asynchronous query has failed
		///
		/// \param[in]	eErrorCode		error code
		//==============================================================================
		virtual void OnError(IMcErrors::ECode eErrorCode) = 0;
	};

	/// Query parameters
	///
	/// Default value of each member is 0 / false / NULL / \a EITT_ANY_TARGET)
	struct SQueryParams
	{
		/// targets bit mask, see #EIntersectionTargetType
		UINT		uTargetsBitMask;

		/// maximal number of targets to find in intersection/scan functions; 
		/// if exceeded, the appropriate function will fail with IMcErrors::TOO_MANY_TARGETS; 
		/// the default is 0 (means no limit)
		UINT		uMaxNumTargetsToFind;

		/// the distance to expand mesh bounding boxes (relevant only if #bUseMeshBoundingBoxOnly == true)
		float		fBoundingBoxExpansionDist;
												
		/// objects overlay to intersect or NULL for all overlays
		IMcOverlay	*pOverlayFilter;

		/// bit field of node kinds (categories) of object items to intersect or 0 for all kinds 
		///  (see IMcObjectSchemeNode::ENodeKindFlags)
		UINT		uItemKindsBitField;

		/// bit field of flags of object item types to intersect or 0 for all types
		///  (use NodeTypeToItemTypeBit() to convert XXX::NODE_TYPE to item type bit)
		UINT		uItemTypeFlagsBitField;
		
		/// terrain precision, see #EQueryPrecision for dSetails
		EQueryPrecision	eTerrainPrecision;
		
		/// whether to check intersection with mesh bounding box only or precise intersection 
		///  with mesh itself
		bool		bUseMeshBoundingBoxOnly;

		/// whether to calculate sight queries using flat or spheric earth model
		bool		bUseFlatEarth;

		/// whether to add static object's contours to each static object found be ScanInGeometry(), GetRayIntersectionTargets() and GetXXXAreaOfSight()
		bool		bAddStaticObjectContours;

		/// maximal error between a representation of a line and it's corresponding
		///  great circle (geodetic line); zero (the default) means straight geometric line
		float		fGreatCirclePrecision;

		/// determines what line or area of sight queries will return when the answer is ambiguous due
		/// to no DTM in query area.
		ENoDTMResult eNoDTMResult;

		/// optional callback for asynchronous query; if not NULL: the query will be performed asynchronously (query function will return without 
		/// results, when the results are ready an appropriate callback function with the results will be called); the same callback instance can be 
		/// used again in another query only after the first query is completed
		IAsyncQueryCallback *pAsyncQueryCallback;
		
		/// constructor setting default values 
		SQueryParams() 
		{ memset(this, 0, sizeof(*this)); uTargetsBitMask=EITT_ANY_TARGET; }

		SQueryParams(const SQueryParams &Other) 
		{ memcpy(this, &Other, sizeof(*this)); }

		SQueryParams& operator=(const SQueryParams &Other) 
		{ memcpy(this, &Other, sizeof(*this)); return *this; }

		//==============================================================================
		// Method Name: NodeTypeToItemTypeBit()
		//------------------------------------------------------------------------------
		/// Converts item's node type to item type bit to be used in uItemTypeFlagsBitField
		///
		/// \param[in]	uNodeType	Node type
		///
		/// \return					Item type bit
		//==============================================================================
		static UINT NodeTypeToItemTypeBit(UINT uNodeType)
		{
			return (uNodeType != 0 ? (1 << (uNodeType - 50)) : 0);
		}
	};
	
	/// \name Create Spatial Queries Standalone Interface
//@{
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates spatial queries interface
	///
	/// \param[out]	ppQueries						spatial queries interface created
	/// \param[in]	CreateData						parameters used for the creation of the queries
	/// \param[in]	apTerrains						terrains to to be used for terrain queries
	/// \param[in]	uNumTerrains					number of terrains
	/// \param[in]	apQuerySecondaryDtmLayers[]		optional array of secondary DTM layers to use for spatial queries lying 
	///												entirely inside a bounding box of one of the secondary DTM layers
	/// \param[in]	uNumQuerySecondaryDtmLayers		number of secondary DTM layers in the above array
	//
	/// \note
	///		terrains or overlay manager can be emitted (NULL) if the appropriate queries
	///		will not be performed
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode Create(IMcSpatialQueries **ppQueries, 
							const SCreateData &CreateData, 
							IMcMapTerrain *const apTerrains[] = NULL, UINT uNumTerrains = 0,
							IMcDtmMapLayer* const apQuerySecondaryDtmLayers[] = NULL, UINT uNumQuerySecondaryDtmLayers = 0);

	//==============================================================================
	// Method Name: GetQuerySecondaryDtmLayers()
	//------------------------------------------------------------------------------
	/// Retrieves the secondary DTM layers for spatial queries passed to IMcSpatialQueries::Create() or IMcMapViewport::Create()
	///
	/// \param[out]	papQuerySecondaryDtmLayers		array of secondary DTM layers for spatial queries
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual SCENEMANAGER_API IMcErrors::ECode GetQuerySecondaryDtmLayers(CMcDataArray<IMcDtmMapLayer*>* papQuerySecondaryDtmLayers) const = 0;
//@}

/// \name Interface Type And Casting
//@{
    //==============================================================================
    // Method Name: GetInterfaceType() 
    //------------------------------------------------------------------------------
    /// Returns the unique ID of IMcSpatialQueries-derived interface type.
    ///
 	/// \remark
	///		Use the cast methods in order to get the correct type.
   //==============================================================================
    virtual UINT GetInterfaceType() const = 0;

    //==============================================================================
    // Method Name: CastToMapViewport(...)
    //------------------------------------------------------------------------------
    /// Casts the #IMcSpatialQueries* To #IMcMapViewport*
    /// 
    /// \return
    ///     - #IMcMapViewport*
    //==============================================================================
	virtual IMcMapViewport* CastToMapViewport() = 0;

	//==============================================================================
    // Method Name: CastToSectionMapViewport(...)
    //------------------------------------------------------------------------------
    /// Casts the #IMcSpatialQueries* To #IMcSectionMapViewport*
    /// 
    /// \return
    ///     - #IMcSectionMapViewport*
    //==============================================================================
	virtual IMcSectionMapViewport* CastToSectionMapViewport() = 0;

	//==============================================================================
	// Method Name: CastToHeatMapViewport(...)
	//------------------------------------------------------------------------------
	/// Casts the #IMcSpatialQueries* To #IMcHeatMapViewport*
	/// 
	/// \return
	///     - #IMcHeatMapViewport*
	//==============================================================================
	virtual IMcHeatMapViewport* CastToHeatMapViewport() = 0;

//@}
/// \name Retrieve Spatial Queries Parameters
//@{
	//==============================================================================
	// Method Name: GetOverlayManager()
	//------------------------------------------------------------------------------
	/// Retrieve the rendering device interface used to create spatial queries or viewport.
	///
	/// \param[out]	ppDevice	the rendering device.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetDevice(IMcMapDevice **ppDevice) const = 0;

	//==============================================================================
	// Method Name: GetOverlayManager()
	//------------------------------------------------------------------------------
	/// Retrieve the overlay manager attached to spatial queries or viewport.
	///
	/// \param[out]	ppOverlayManager	the overlay manager.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetOverlayManager(IMcOverlayManager **ppOverlayManager) const = 0;

	//==============================================================================
	// Method Name: GetCoordinateSystem()
	//------------------------------------------------------------------------------
	/// Retrieve the coordinate system used by spatial queries or viewport.
	///
	/// \param[out]	ppCoordinateSystem	the coordinate system.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCoordinateSystem(IMcGridCoordinateSystem **ppCoordinateSystem) const = 0; 

	//==============================================================================
	// Method Name: GetViewportID()
	//------------------------------------------------------------------------------
	/// Retrieve user-defined viewport ID used in viewport visibility selectors.
	///
	/// \param[out]	puViewportID	the user-defined viewport ID.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetViewportID(UINT *puViewportID) const = 0;

	//==============================================================================
	// Method Name: CanPerformQuery()
	//------------------------------------------------------------------------------
	/// Checks if spatial queries (standalone or via a viewport with a precision other than currently shown) of the defined type can be performed 
	/// synchronously or asynchronously.
	///
	/// Spatial queries cannot be performed synchronously when they use server-based layers; spatial queries that are not supported by servers 
	/// other than MapCore's Map Layer Server cannot be performed asynchronously when they use other-server-based layers.
	///
	/// \param[in]	bQuerySupportedByNonNativeServer	whether the queries in question are supported by servers other than MapCore's Map Layer Server.
	/// \param[out]	pbCanPerformSyncQuery				whether spatial queries with with the specified targets bit mask or with the specified map layer can be 
	///													performed synchronously.
	/// \param[out]	pbCanPerformAsyncQuery				whether spatial queries with with the specified targets bit mask or with the specified map layer can be 
	///													performed asynchronously.
	/// \param[in]	uTargetsBitMask						the targets bit mask, see #EIntersectionTargetType; the default is #EITT_ANY_TARGET.
	/// \param[in]	pLayer								the optional map layer for queries that require specifying a layer; the default is `NULL`.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode CanPerformQuery(bool bQuerySupportedByNonNativeServer, bool *pbCanPerformSyncQuery, bool *pbCanPerformAsyncQuery, 
		UINT uTargetsBitMask = EITT_ANY_TARGET, IMcMapLayer *pLayer = NULL) const = 0;

//@}
/// \name Terrains
//@{

	//==============================================================================
	// Method Name: SetTerrainQueriesNumCacheTiles()
	//------------------------------------------------------------------------------
	/// Sets terrain queries cache size (in tiles of the specified layer type) for this spatial queries interface.
	///
	/// Different cache sizes are used for static-objects layers and for the DTM layer.
	///
	/// \param[in]	pTerrain		the terrain to set the cache for.
	/// \param[in]  eLayerKind		the layer kind of the cache; see #IMcMapLayer::ELayerKind for details 
	///								(only DTM, raster and static-objects are currently supported)
	/// \param[in]	uNumTiles		the number of tiles in the cache (the default is 300).
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTerrainQueriesNumCacheTiles(IMcMapTerrain *pTerrain, IMcMapLayer::ELayerKind eLayerKind, UINT uNumTiles) = 0;

	//==============================================================================
	// Method Name: GetTerrainQueriesNumCacheTiles()
	//------------------------------------------------------------------------------
	/// Retrieves terrain queries cache size (in tiles of the specified layer type) for this spatial queries interface.
	///
	/// Different cache sizes are used for static-objects layers and for DTM layer.
	///
	/// \param[in]	pTerrain		the terrain to retrieve the cache for.
	/// \param[in]  eLayerKind		the layer kind of the cache; see #IMcMapLayer::ELayerKind for details 
	///								(only DTM, raster and static-objects are currently supported)
	/// \param[out]	puNumTiles		the number of tiles in the cache (the default is 300)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTerrainQueriesNumCacheTiles(IMcMapTerrain *pTerrain, IMcMapLayer::ELayerKind eLayerKind, UINT *puNumTiles) const = 0;

	//==============================================================================
	// Method Name: GetTerrains()
	//------------------------------------------------------------------------------
	/// Retrieves a list of terrains in the queries/viewport.
	///
	/// \param[out]	papTerrains			array of current terrains.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTerrains(CMcDataArray<IMcMapTerrain*> *papTerrains) const = 0;

	//==============================================================================
	// Method Name: GetTerrainsBoundingBox()
	//------------------------------------------------------------------------------
	/// Retrieve the terrains' bounding box.
	///
	/// \param[out]	pBoundingBox	the terrains' bounding box.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTerrainsBoundingBox(SMcBox *pBoundingBox) const = 0;

//@}
/// \name Terrain Height And Intersection
//@{
	//==============================================================================
	// Method Name: GetTerrainHeight()
	//------------------------------------------------------------------------------
	/// Retrieves the height of terrains (DTM with or without static objects) and the normal 
	/// for the intersected triangle at the requested world point.
	///
	/// \param[in]	Point			world point to retrieve the height at (\a x and \a y only are used)
	/// \param[out]	pbHeightFound	whether height found
	/// \param[out]	pdHeight		height of highest target (particularly, \a &Point.z can be passed) 
	/// \param[out]	pNormal			normal of highest target at world point; optional, will not be calculated if NULL
	/// \param[in]	pParams			query parameters or NULL to use the default
	///								(in \a pParams->uTargetsBitMask only #EITT_DTM_LAYER and 
	///								#EITT_STATIC_OBJECTS_LAYER targets specified are taken into account)
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTerrainHeight(const SMcVector3D &Point, bool *pbHeightFound, double *pdHeight, 
		SMcVector3D *pNormal = NULL, const SQueryParams *pParams = NULL) = 0;


	//==============================================================================
	// Method Name: GetTerrainHeightsAlongLine()
	//------------------------------------------------------------------------------
	/// Retrieves the heights of terrains (DTM with or without static objects) along each one 
	/// of horizontal polyline segments.
	///
	/// \param[in]	LineVertices[]			line vertices (\a x and \a y only are used)
	/// \param[in]	uNumVertices			number of line vertices
	/// \param[out]	paPointsWithHeights		calculated points with heights (optional)
	/// \param[out]	pafSlopes				calculated calculated slopes: a slope per point, 0 is a horizon (optional, will not be calculated if NULL)
	/// \param[out] pSlopesData				slopes data; optional, will not be calculated if NULL
	/// \param[in]	pParams					query parameters or NULL to use the default
	///										(only #EITT_DTM_LAYER and #EITT_STATIC_OBJECTS_LAYER targets
	///										specified in \a pParams->uTargetsBitMask are taken into account).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTerrainHeightsAlongLine(
		const SMcVector3D LineVertices[], UINT uNumVertices,
		CMcDataArray<SMcVector3D> *paPointsWithHeights,
		CMcDataArray<float> *pafSlopes = NULL,
		IMcSpatialQueries::SSlopesData *pSlopesData = NULL,
		const SQueryParams *pParams = NULL) = 0;

	//==============================================================================
	// Method Name: GetExtremeHeightPointsInPolygon()
	//------------------------------------------------------------------------------
	/// Retrieves highest and lowest points in polygon
	///
	/// \param[in]	aPolygonVertices	polygon vertices (\a x and \a y only are used).
	/// \param[in]	uNumVertices		number of polygon vertices.
	/// \param[out]	pbPointsFound		whether points found.
	/// \param[out]	pHighestPoint		the highest point found (pass `NULL` if unnecessary).
	/// \param[out]	pLowestPoint		the lowest point found (pass `NULL` if unnecessary).
	/// \param[in]	pParams				query parameters or NULL to use the default
	///									(only #EITT_DTM_LAYER and #EITT_STATIC_OBJECTS_LAYER targets
	///									specified in \a pParams->uTargetsBitMask are taken into account).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetExtremeHeightPointsInPolygon(const SMcVector3D aPolygonVertices[],
		UINT uNumVertices, bool *pbPointsFound, SMcVector3D *pHighestPoint = NULL, SMcVector3D *pLowestPoint = NULL,
		const SQueryParams *pParams = NULL) = 0;

	//==============================================================================
	// Method Name: GetTerrainHeightMatrix()
	//------------------------------------------------------------------------------
	/// Calculates terrain height matrix according to the specified world rectangle.
	///
	/// \param[in]	LowerLeftPoint				rectangle's lower-left point (\a x and \a y only are used)
	/// \param[in]	dHorizontalResolution		matrix resolution along \a x axis
	/// \param[in]	dVerticalResolution			matrix resolution along \a y axis
	/// \param[in]	uNumHorizontalPoints		matrix width in points
	/// \param[in]	uNumVerticalPoints			matrix height in points
	/// \param[out]	padHeightMatrix				calculated matrix - array of heights of length \a uNumHorizontalPoints * \a uNumVerticalPoints, 
	///											row by row bottom to top - in ascending order of \a y
	/// \param[in]	pParams						query parameters or NULL to use the default
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTerrainHeightMatrix(const SMcVector3D &LowerLeftPoint, 
		double dHorizontalResolution, double dVerticalResolution,
		UINT uNumHorizontalPoints, UINT uNumVerticalPoints, 
		CMcDataArray<double> *padHeightMatrix, 
		const IMcSpatialQueries::SQueryParams *pParams = NULL) = 0;

	//==============================================================================
	// Method Name: GetTerrainAngles()
	//------------------------------------------------------------------------------
	/// Given a vehicle location point and its azimuth, retrieves the vehicle's Pitch and Roll
	///
	/// \param[in]	Point			vehicle world location point(\a x and \a y only are used).
	/// \param[in]	dAzimuth		vehicle's azimuth [deg].
	/// \param[out]	pdPitch			vehicle's pitch [deg].
	/// \param[out]	pdRoll			vehicle's roll [deg].
	/// \param[in]	pParams			query parameters or NULL to use the default
	///								(in \a pParams->uTargetsBitMask only #EITT_DTM_LAYER and 
	///								#EITT_STATIC_OBJECTS_LAYER targets specified are taken into account).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode  GetTerrainAngles(const SMcVector3D &Point,
		double dAzimuth,
		double *pdPitch, 
		double *pdRoll, 
		const IMcSpatialQueries::SQueryParams *pParams = NULL) = 0;


	//==============================================================================
	// Method Name: GetRayIntersection()
	//------------------------------------------------------------------------------
	/// Computes the nearest intersection point between a ray and the specified targets.
	///	
	/// \param[in]	RayOrigin				origin of the ray.
	/// \param[in]	RayDirection			direction of the ray.
	/// \param[in]	dMaxDistance			maximal distance to the intersection.
	/// \param[out]	pbIntersectionFound		whether at least one intersection found.
	/// \param[out]	pIntersection			nearest intersection or NULL if should not be computed.
	/// \param[out]	pNormal					normal or NULL if should not be computed.
	/// \param[out]	pdDistance				distance between the ray origin and the nearest 
	///										intersection point or NULL if should not be computed.
	/// \param[in]	pParams					query parameters or NULL to use the default.
	///
	/// \note
	/// The query can be performed asynchronously only with map layers based on MapCore's Map Layer Server; with local map layers it can be performed 
	/// via asynchronous callback, but in that case the calculation will be performed synchronously, the callback with the results will be called 
	/// before the function is ended and the function will return without results.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRayIntersection( const SMcVector3D &RayOrigin, 
												const SMcVector3D &RayDirection, 
												double dMaxDistance, 
												bool *pbIntersectionFound, 
												SMcVector3D *pIntersection = NULL, 
												SMcVector3D *pNormal = NULL, 
												double *pdDistance = NULL, 
												const SQueryParams *pParams = NULL) = 0;

	//==============================================================================
	// Method Name: GetRayIntersectionTargets()
	//------------------------------------------------------------------------------
	/// Computes the intersections between a ray and the specified targets, returning
	/// the intersections details.
	///	
	/// \param[in]	RayOrigin				origin of the ray.
	/// \param[in]	RayDirection			direction of the ray.
	/// \param[in]	dMaxDistance			maximal distance to the intersection.
	/// \param[out]	paIntersections			intersections with requested targets 
	///										(sorted from nearest to farthest).
	/// \param[in]	pParams					query parameters or NULL to use the default.
	///
	/// \note
	/// The query can be performed asynchronously only with map layers based on MapCore's Map Layer Server; with local map layers it can be performed 
	/// via asynchronous callback, but in that case the calculation will be performed synchronously, the callback with the results will be called 
	/// before the function is ended and the function will return without results.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRayIntersectionTargets(const SMcVector3D &RayOrigin, 
		const SMcVector3D &RayDirection, double dMaxDistance, 
		CMcDataArray<STargetFound> *paIntersections,
		const SQueryParams *pParams = NULL) = 0;

	//==============================================================================
	// Method Name: GetLineOfSight()
	//------------------------------------------------------------------------------
	/// Retrieves locations on a line that are seen from a scouter.
	/// The method works with a DTM with or without static objects.
	///
	/// \param[in]	Scouter						The X,Y,Z coordinates of the scouter
	/// \param[in]	bIsScouterHeightAbsolute	Determines whether the Scouter's Z coordinate
	///											is absolute (above sea level) or relative to ground
	/// \param[in]	Target						The target to calculate line to.
	/// \param[in]	bTargetHeightAbsolute		Determines whether the Target's Z coordinate
	///											is absolute (above sea level) or relative to ground
	/// \param[out]	paPoints					A line running from the Scouter to the target,
	///											each point consists of its coordinate and whether it is seen or not.
	///	\param[out]	pdCrestClearanceAngle		The angle above horizon from the scouter to
	///											the last visible terrain location.
	///	\param[out]	pdCrestClearanceDistance	The horizontal distance from the scouter to
	///											the last visible terrain location.
	/// \param[in]	dMaxPitchAngle				The scouter maximal pitch ability.
	/// \param[in]	dMinPitchAngle				The scouter minimal pitch ability.
	/// \param[in]	pParams						query parameters or NULL to use the default
	///											(only #EITT_DTM_LAYER and #EITT_STATIC_OBJECTS_LAYER targets
	///											specified in \a pParams->uTargetsBitMask are taken into account).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLineOfSight(const SMcVector3D &Scouter, 
		bool bIsScouterHeightAbsolute,
		const SMcVector3D &Target, 
		bool bTargetHeightAbsolute, 
		CMcDataArray<IMcSpatialQueries::SLineOfSightPoint> *paPoints, 
		double *pdCrestClearanceAngle,
		double *pdCrestClearanceDistance,
		double dMaxPitchAngle = 90, 
		double dMinPitchAngle = -90, 
		const SQueryParams *pParams = NULL) = 0;

	//==============================================================================
	// Method Name: GetPointVisibility()
	//------------------------------------------------------------------------------
	/// Checks whether a target is visible from a scouter and from what height the scouter or target will enable visibility.
	///
	/// The method works with a DTM with or without static objects.
	///
	/// \param[in]	Scouter								The X,Y,Z coordinates of the scouter
	/// \param[in]	bIsScouterHeightAbsolute			Determines whether the Scouter's Z coordinate
	///													is absolute (above sea level) or relative to ground
	/// \param[in]	Target								The target to calculate line to.
	/// \param[in]	bTargetHeightAbsolute				Determines whether the Target's Z coordinate
	///													is absolute (above sea level) or relative to ground
	/// \param[out]	pbIsTargetVisible					Whether or not the target is seen from the scouter.
	///	\param[out]	pMinimalTargetHeightForVisibility	The height in which the target will be visible from the current scouter 
	///													(pass `NULL` if unneeded for simpler calculations).
	///	\param[out]	pMinimalScouterHeightForVisibility	The height in which the scouter will see the current target 
	///													(pass `NULL` if unneeded for simpler calculations).
	/// \param[in]	dMaxPitchAngle						The scouter maximal pitch ability.
	/// \param[in]	dMinPitchAngle						The scouter minimal pitch ability.
	/// \param[in]	pParams								query parameters or `NULL` to use the default
	///													(only #EITT_DTM_LAYER and #EITT_STATIC_OBJECTS_LAYER targets
	///													specified in \a pParams->uTargetsBitMask are taken into account).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetPointVisibility(const SMcVector3D &Scouter, 
		bool bIsScouterHeightAbsolute,
		const SMcVector3D &Target, 
		bool bTargetHeightAbsolute,
		bool *pbIsTargetVisible, 
		double *pMinimalTargetHeightForVisibility = NULL,
		double *pMinimalScouterHeightForVisibility = NULL,
		double dMaxPitchAngle = 90,
		double dMinPitchAngle = -90,
		const SQueryParams *pParams = NULL) = 0;

	//==============================================================================
	// Method Name: GetPolygonAreaOfSight()
	//------------------------------------------------------------------------------
	/// Retrieves the areas in a region of interest that are seen from a scouter.
	/// The method works with a DTM with or without static objects.
	/// 
	/// If the calculation is GPU-based (\a bGPUBased == true) it will be redirected by MapCore to its main thread 
	/// (if called from any other thread). In this case IMcMapDevice::PerformPendingCalculations() 
	/// should be constantly called in MapCore's main thread, otherwise the calculation will hang.
	///
	/// \param[in]	Scouter						The X,Y,Z coordinates of the scouter
	/// \param[in]	bIsScouterHeightAbsolute	Determines whether the Scouter's Z coordinate
	///											is absolute (above sea level) or relative to ground
	/// \param[in]	aTargetPolygonPoints		The target (region of interest) to calculate in.
	/// \param[in]	uNumTargetPolygonPoints		The number of the target's polygon points.
	/// \param[in]	dTargetHeight				The target's height.
	/// \param[in]	bTargetsHeightAbsolute		Determines whether the Targets Z coordinate
	///											is absolute (above sea level) or relative to ground
	/// \param[in]	fTargetResolutionInMeters	The target's sampling resolution to check visibility.
	/// \param[in]	dRotationAngle				Rotation angle and of area-of-sight matrix (in degrees)
	/// \param[in]	uNumRaysPer360Degrees		The number of rays to check visibility, or 0 (=number of target points).
	///											more points will resolve to better accuracy but
	///											more calculation time as well.
	///											relevant only if bGPUBased is false (CPU calculation).
	/// \param[in]	aVisibilityColors[]			The array (of length #EPV_NUM) of points' visibility 
	///											colors per #EPointVisibility as an index 
	///											(\a aVisibilityColors[#EPV_SEEN] is a color for seen points etc.)
	/// \param[out]	ppAreaOfSight				The Area-of-sight interface for the results (pass `NULL` if unneeded for simpler calculations); 
	///											the user should call IAreaOfSight::Destroy() to delete it; 
	///											relevant only if bGPUBased is true (GPU calculation).
	/// \param[out]	paLinesOfSight				An array of lines running from the Scouter to each aTargetPoints 
	///											(pass `NULL` if unneeded for simpler calculations).
	///											each point consists of its coordinate and whether it is seen or not.
	///											relevant only if bGPUBased is false (CPU calculation).
	/// \param[out]	pSeenPolygons				multi-contour polygons that are seen (pass `NULL` if unneeded for simpler calculations)
	/// \param[out]	pUnseenPolygons				multi-contour polygons that are unseen (pass `NULL` if unneeded for simpler calculations)
	/// \param[out]	paSeenStaticObjects			The array of IDs of seen static objects per layer (pass `NULL` if unneeded for simpler calculations)
	/// \param[in]	dMaxPitchAngle				The scouter maximal pitch ability.
	/// \param[in]	dMinPitchAngle				The scouter minimal pitch ability.
	/// \param[in]	pParams						query parameters or NULL to use the default
	///											(only #EITT_DTM_LAYER and #EITT_STATIC_OBJECTS_LAYER targets
	///											specified in \a pParams->uTargetsBitMask are taken into account).
	/// \param[in]	bGPUBased					whether the calculations should be performed in GPU
	///
	/// \remarks
	///		each output parameter that is input as NULL, will not be calculated - for better performance
	/// \return
	///     - status result
	//==============================================================================

	virtual IMcErrors::ECode GetPolygonAreaOfSight(
		const SMcVector3D &Scouter, 
		bool bIsScouterHeightAbsolute,
		const SMcVector3D aTargetPolygonPoints[], 
		UINT uNumTargetPolygonPoints,
		double dTargetHeight,
		bool bTargetsHeightAbsolute,
		float fTargetResolutionInMeters,
		double dRotationAngle, 
		unsigned int		uNumRaysPer360Degrees,
		const SMcBColor		aVisibilityColors[EPV_NUM],
		IAreaOfSight		**ppAreaOfSight = NULL,
		CMcDataArray<CMcDataArray<IMcSpatialQueries::SLineOfSightPoint> > *paLinesOfSight = NULL, 
		SPolygonsOfSight *pSeenPolygons = NULL, 
		SPolygonsOfSight *pUnseenPolygons = NULL, 
		CMcDataArray<SStaticObjectsIDs> *paSeenStaticObjects = NULL, 
		double				dMaxPitchAngle = 90,
		double				dMinPitchAngle = -90,
		const SQueryParams	*pParams = NULL,
		bool				bGPUBased = true) = 0;

	//==============================================================================
	// Method Name: GetRectangleAreaOfSight()
	//------------------------------------------------------------------------------
	/// Retrieves the areas in a rectangle surrounding the scouter that are seen from it .
	/// The method works with a DTM with or without static objects.
	/// 
	/// If the calculation is GPU-based (\a bGPUBased == true) it will be redirected by MapCore to its main thread 
	/// (if called from any other thread). In this case IMcMapDevice::PerformPendingCalculations() 
	/// should be constantly called in MapCore's main thread, otherwise the calculation will hang.
	///
	/// \param[in]	Scouter						The X,Y,Z coordinates of the scouter, also the rectangle's center
	/// \param[in]	bIsScouterHeightAbsolute	Determines whether the Scouter's Z coordinate
	///											is absolute (above sea level) or relative to ground
	/// \param[in]	dRectangletHeight			Rectangle height
	/// \param[in]	dRectangleWidth				Rectangle width
	/// \param[in]	dTargetHeight				The target's height.
	/// \param[in]	bTargetsHeightAbsolute		Determines whether the Targets Z coordinate
	///											is absolute (above sea level) or relative to ground
	/// \param[in]	fTargetResolutionInMeters	The target's sampling resolution to check visibility.
	/// \param[in]	dRotationAngle				Geographic angle of rectangle (azimuth of its Y axis) and of area-of-sight matrix (in degrees)
	/// \param[in]	uNumRaysPer360Degrees		The number of rays to check visibility.
	///											more points will resolve to better accuracy but
	///											more calculation time as well.
	///											relevant only if bGPUBased is false (CPU calculation).
	/// \param[in]	aVisibilityColors[]			The array (of length #EPV_NUM) of points' visibility 
	///											colors per #EPointVisibility as an index 
	///											(\a aVisibilityColors[#EPV_SEEN] is a color for seen points etc.)
	/// \param[out]	ppAreaOfSight				The Area-of-sight interface for the results (pass `NULL` if unneeded for simpler calculations); 
	///											the user should call IAreaOfSight::Destroy() to delete it; 
	///											relevant only if bGPUBased is true (GPU calculation).
	/// \param[out]	paLinesOfSight				An array of lines running from the Scouter to each aTargetPoints 
	///											(pass `NULL` if unneeded for simpler calculations).
	///											each point consists of its coordinate and whether it is seen or not.
	///											relevant only if bGPUBased is false (CPU calculation).
	/// \param[out]	pSeenPolygons				multi-contour polygons that are seen (pass `NULL` if unneeded for simpler calculations)
	/// \param[out]	pUnseenPolygons				multi-contour polygons that are unseen (pass `NULL` if unneeded for simpler calculations)
	/// \param[out]	paSeenStaticObjects			The array of IDs of seen static objects per layer (pass `NULL` if unneeded for simpler calculations)
	/// \param[in]	dMaxPitchAngle				The scouter maximal pitch ability.
	/// \param[in]	dMinPitchAngle				The scouter minimal pitch ability.
	/// \param[in]	pParams						query parameters or NULL to use the default
	///											(only #EITT_DTM_LAYER and #EITT_STATIC_OBJECTS_LAYER targets
	///											specified in \a pParams->uTargetsBitMask are taken into account).
	/// \param[in]	bGPUBased					whether the calculations should be performed in GPU
	///
	/// \remarks
	///		each output parameter that is input as NULL, will not be calculated - for better performance
	/// \return
	///     - status result
	//==============================================================================

	virtual IMcErrors::ECode GetRectangleAreaOfSight(
		const SMcVector3D	&Scouter, 
		bool				bIsScouterHeightAbsolute,
		double 				dRectangletHeight,
		double 				dRectangleWidth, 
		double 				dRotationAngle,
		double				dTargetHeight,
		bool				bTargetsHeightAbsolute,
		float				fTargetResolutionInMeters,
		unsigned int		uNumRaysPer360Degrees,
		const SMcBColor		aVisibilityColors[EPV_NUM],
		IAreaOfSight		**ppAreaOfSight = NULL,
		CMcDataArray<CMcDataArray<IMcSpatialQueries::SLineOfSightPoint> > *paLinesOfSight = NULL, 
		SPolygonsOfSight *pSeenPolygons = NULL, 
		SPolygonsOfSight *pUnseenPolygons = NULL, 
		CMcDataArray<SStaticObjectsIDs> 
							*paSeenStaticObjects = NULL, 
		double				dMaxPitchAngle = 90,
		double				dMinPitchAngle = -90,
		const SQueryParams	*pParams = NULL,
		bool				bGPUBased = true) = 0;

	//==============================================================================
	// Method Name: GetEllipseAreaOfSight()
	//------------------------------------------------------------------------------
	/// Retrieves the areas in an elliptic region surrounding the scouter that are seen from it .
	/// The method works with a DTM with or without static objects.
	/// 
	/// If the calculation is GPU-based (\a bGPUBased == true) it will be redirected by MapCore to its main thread 
	/// (if called from any other thread). In this case IMcMapDevice::PerformPendingCalculations() 
	/// should be constantly called in MapCore's main thread, otherwise the calculation will hang.
	///
	/// \param[in]	Scouter						The X,Y,Z coordinates of the scouter, also the ellipse's center
	/// \param[in]	bIsScouterHeightAbsolute	Determines whether the Scouter's Z coordinate
	///											is absolute (above sea level) or relative to ground
	/// \param[in]	dTargetHeight				The target's height.
	/// \param[in]	bTargetsHeightAbsolute		Determines whether the Targets Z coordinate
	///											is absolute (above sea level) or relative to ground
	/// \param[in]	fTargetResolutionInMeters	The target's sampling resolution to check visibility.
	///	\param[in]	fRadiusX					the radius of the ellipse in x axis direction.				
	///	\param[in]	fRadiusY					the radius of the ellipse in Y axis direction.				
	///	\param[in]	fStartAngle					start azimuth of the elliptic region.
	///	\param[in]	fEndAngle					end azimuth of the elliptic region.
	///	\param[in]	fRotationAngle				Geographic angle of ellipse (azimuth of its Y axis) and of area-of-sight matrix (in degrees)
	///	\param[in]	uNumRaysPer360Degrees		The number of rays to sample the elliptic region,
	///											and also the number of rays to check visibility.
	///											more points will resolve to better accuracy but
	///											more calculation time as well.
	///											relevant only if bGPUBased is false (CPU calculation).
	/// \param[in]	aVisibilityColors[]			The array (of length #EPV_NUM) of points' visibility 
	///											colors per #EPointVisibility as an index 
	///											(\a aVisibilityColors[#EPV_SEEN] is a color for seen points etc.)
	/// \param[out]	ppAreaOfSight				The Area-of-sight interface for the results (pass `NULL` if unneeded for simpler calculations); 
	///											the user should call IAreaOfSight::Destroy() to delete it; 
	///											relevant only if bGPUBased is true (GPU calculation).
	/// \param[out]	paLinesOfSight				An array of lines running from the Scouter to each aTargetPoints 
	///											(pass `NULL` if unneeded for simpler calculations).
	///											each point consists of its coordinate and whether it is seen or not.
	///											relevant only if bGPUBased is false (CPU calculation).
	/// \param[out]	pSeenPolygons				multi-contour polygons that are seen (pass `NULL` if unneeded for simpler calculations)
	/// \param[out]	pUnseenPolygons				multi-contour polygons that are unseen (pass `NULL` if unneeded for simpler calculations)
	/// \param[out]	paSeenStaticObjects			The array of IDs of seen static objects per layer (pass `NULL` if unneeded for simpler calculations)
	/// \param[in]	dMaxPitchAngle				The scouter maximal pitch ability.
	/// \param[in]	dMinPitchAngle				The scouter minimal pitch ability.
	/// \param[in]	pParams						query parameters or NULL to use the default
	///											(only #EITT_DTM_LAYER and #EITT_STATIC_OBJECTS_LAYER targets
	///											specified in \a pParams->uTargetsBitMask are taken into account).
	/// \param[in]	bGPUBased					whether the calculations should be performed in GPU
	///
	/// \remarks
	///		each output parameter that is input as NULL, will not be calculated - for better performance
	/// \return
	///     - status result
	//==============================================================================

	virtual IMcErrors::ECode GetEllipseAreaOfSight(
		const SMcVector3D &Scouter,
		bool bIsScouterHeightAbsolute,
		double dTargetHeight,
		bool bTargetsHeightAbsolute,
		float fTargetResolutionInMeters,
		float fRadiusX,
		float fRadiusY,
		float fStartAngle,
		float fEndAngle,
		float fRotationAngle,
		unsigned int uNumRaysPer360Degrees,
		const SMcBColor		aVisibilityColors[EPV_NUM],
		IAreaOfSight		**ppAreaOfSight = NULL,
		CMcDataArray<CMcDataArray<IMcSpatialQueries::SLineOfSightPoint> > *paLinesOfSight = NULL,
		SPolygonsOfSight *pSeenPolygons = NULL,
		SPolygonsOfSight *pUnseenPolygons = NULL,
		CMcDataArray<SStaticObjectsIDs> *paSeenStaticObjects = NULL,
		double				dMaxPitchAngle = 90,
		double				dMinPitchAngle = -90,
		const SQueryParams	*pParams = NULL,
		bool				bGPUBased = true) = 0;


	//==============================================================================
	// Method Name: GetEllipseAreaOfSightForMultipleScouters()
	//------------------------------------------------------------------------------
	/// Retrieves the areas in an elliptic region surrounding the scouters that are seen from different scouting locations .
	/// The method works with a DTM with or without static objects.
	/// 
	/// If the calculation is GPU-based (\a bGPUBased == true) it will be redirected by MapCore to its main thread 
	/// (if called from any other thread). In this case IMcMapDevice::PerformPendingCalculations() 
	/// should be constantly called in MapCore's main thread, otherwise the calculation will hang.
	///
	/// \param[in]	Scouters[]					The X,Y,Z coordinates of the scouters.
	/// \param[in]	uNumOfScouters				The number of scouters.
	/// \param[in]	bIsScoutersHeightsAbsolute	Determines whether the Scouters Z coordinate
	///											is absolute (above sea level) or relative to ground
	/// \param[in]	dTargetHeight				The target's height.
	/// \param[in]	bTargetsHeightAbsolute		Determines whether the Targets Z coordinate
	///											is absolute (above sea level) or relative to ground
	/// \param[in]	fTargetResolutionInMeters	The target's sampling resolution to check visibility.
	///	\param[in]	TargetEllipseCenter			The center of the ellipse.				
	///	\param[in]	fRadiusX					the radius of the ellipse in x axis direction.				
	///	\param[in]	fRadiusY					the radius of the ellipse in Y axis direction.				
	///	\param[in]	uNumRaysPer360Degrees		The number of rays to sample the elliptic region,
	///											and also the number of rays to check visibility.
	///											more points will resolve to better accuracy but
	///											more calculation time as well.
	///											relevant only if bGPUBased is false (CPU calculation).
	/// \param[in]	eScoutersSumType			Determines how to sum results for each point.  
	/// \param[out]	ppAreaOfSight				The Area-of-sight interface for the results; 
	///											the user should call IAreaOfSight::Destroy() to delete it;
	/// \param[in]	dMaxPitchAngle				The scouter maximal pitch ability.
	/// \param[in]	dMinPitchAngle				The scouter minimal pitch ability.
	/// \param[in]	pParams						query parameters or NULL to use the default
	///											(only #EITT_DTM_LAYER and #EITT_STATIC_OBJECTS_LAYER targets
	///											specified in \a pParams->uTargetsBitMask are taken into account).
	/// \param[in]	bGPUBased					whether the calculations should be performed in GPU
	///
	/// \return
	///     - status result
	//==============================================================================

	virtual IMcErrors::ECode GetEllipseAreaOfSightForMultipleScouters(
		const SMcVector3D	Scouters[],
		unsigned int uNumOfScouters,
		bool bIsScoutersHeightsAbsolute,
		double dTargetHeight,
		bool bTargetsHeightAbsolute,
		float fTargetResolutionInMeters,
		const SMcVector3D	&TargetEllipseCenter,
		float fRadiusX,
		float fRadiusY,
		unsigned int uNumRaysPer360Degrees,
		EScoutersSumType eScoutersSumType,
		IAreaOfSight		**ppAreaOfSight,
		double				dMaxPitchAngle = 90,
		double				dMinPitchAngle = -90,
		const SQueryParams	*pParams = NULL,
		bool				bGPUBased = true) = 0;

	//==============================================================================
	// Method Name: GetBestScoutersLocationsInEllipse()
	//------------------------------------------------------------------------------
	/// Retrieves the best locations to place scouters to view maximal areas of an elliptic region surrounding the scouters.
	/// The method works with a DTM with or without static objects.
	///
	///	\param[in]	TargetEllipseCenter			The center of the target ellipse.				
	/// \param[in]	dTargetHeight				The target's height.
	/// \param[in]	bTargetsHeightAbsolute		Determines whether the Targets Z coordinate
	///											is absolute (above sea level) or relative to ground
	///	\param[in]	fRadiusX					the radius of the target ellipse in x axis direction.				
	///	\param[in]	fRadiusY					the radius of the target ellipse in Y axis direction.				
	///	\param[in]	ScoutersCenter				The center of the potential ellipse area for the scouters.				
	///	\param[in]	fScoutersRadiusX			the radius of the potential ellipse area for the scouters in x axis direction.				
	///	\param[in]	fScoutersRadiusY			the radius of the potential ellipse area for the scouters in y axis direction.				
	/// \param[in]	dScoutersHeight				The scouters height.
	/// \param[in]	bIsScoutersHeightsAbsolute	Determines whether the Scouters Z coordinate
	///											is absolute (above sea level) or relative to ground
	/// \param[in]	uMaxNumOfScouters			The maximal number of scouters to retrieve.
	/// \param[out]	paScouters[]				The X,Y,Z coordinates of the scouters found.
	/// \param[in]	pParams						query parameters or NULL to use the default
	///											(only #EITT_DTM_LAYER and #EITT_STATIC_OBJECTS_LAYER targets
	///											specified in \a pParams->uTargetsBitMask are taken into account).
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetBestScoutersLocationsInEllipse(
		const SMcVector3D	&TargetEllipseCenter,
		double dTargetHeight,
		bool bTargetsHeightAbsolute,
		float fRadiusX,
		float fRadiusY,
		const SMcVector3D	&ScoutersCenter,
		float fScoutersRadiusX,
		float fScoutersRadiusY,
		double dScoutersHeight,
		bool bIsScoutersHeightsAbsolute,
		unsigned int uMaxNumOfScouters,
		CMcDataArray<SMcVector3D> *paScouters,
		const SQueryParams	*pParams = NULL) = 0;
	
//@}

/// \name Get Tile Data
//@{

	//==============================================================================
	// Method Name: GetDtmLayerTileGeometryByKey()
	//------------------------------------------------------------------------------
	/// Retrieves the DTM layer's tile geometry data by its key (unique ID).
	///
	/// \param[in]	pLayer				DTM layer
	/// \param[in]	TileKey				key (unique ID) of the tile in interest
	/// \param[in]	bBuildIfPossible	what to do if if the required tile does not exist but the coarser one does: 
	///									whether to build the geometry from the appropriate portion of a coarser tile or to fail
	/// \param[out]	pTileGeometry		tile's geometry data
	/// \param[in]	pParams				query parameters or NULL to use the default
	///
	/// \note
	/// The query can be performed asynchronously only with map layers based on MapCore's Map Layer Server.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetDtmLayerTileGeometryByKey(
		IMcDtmMapLayer *pLayer, const IMcMapLayer::SLayerTileKey &TileKey, bool bBuildIfPossible,
		IMcDtmMapLayer::STileGeometry *pTileGeometry,
		const SQueryParams *pParams = NULL) = 0;

	//==============================================================================
	// Method Name: GetRasterLayerTileBitmapByKey()
	//------------------------------------------------------------------------------
	/// Retrieves raster layer's tile bitmap by its key (unique ID).
	///
	/// \param[in]	pLayer					raster layer
	/// \param[in]	TileKey					key (unique ID) of the tile in interest
	/// \param[in]	bDecompress				whether or not to decompress compressed bitmap or keep it compressed
	/// \param[out]	peBitmapPixelFormat		tile's bitmap pixel format
	/// \param[out] pbBitmapFromTopToBottom whether or not tile's bitmap is from top to bottom
	/// \param[out]	pBitmapSize				tile's bitmap dimensions in pixels
	/// \param[out]	pBitmapMargins			tile's bitmap margins in pixels
	/// \param[out]	paBitmapBits			tile's bitmap bits
	/// \param[in]	pParams					query parameters or NULL to use the default
	///
	/// \note
	/// The query can be performed asynchronously only with map layers based on MapCore's Map Layer Server.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRasterLayerTileBitmapByKey(
		IMcRasterMapLayer *pLayer, const IMcMapLayer::SLayerTileKey &TileKey, bool bDecompress,
		IMcTexture::EPixelFormat *peBitmapPixelFormat, bool *pbBitmapFromTopToBottom,
		SMcSize *pBitmapSize, SMcSize *pBitmapMargins, CMcDataArray<BYTE> *paBitmapBits,
		const SQueryParams *pParams = NULL) = 0;

	//==============================================================================
	// Method Name: GetRasterLayerColorByPoint()
	//------------------------------------------------------------------------------
	/// Retrieves the color of the specified level of detail at the specified world point.
	///
	/// \param[in]	pLayer				The raster layer
	/// \param[in]	Point				The point to look at
	/// \param[in]	nLOD				The level of detail required or INT_MIN for the finest possible level at this point
	/// \param[in]  bNearestPixel		how to find the pixel
	/// \param[out]	pColor				The color found
	/// \param[in]	pParams				query parameters or NULL to use the default
	///
	/// \note
	/// If at the specified world point the finest level of detail is coarser than the specified one, 
	/// the tile of that level will be returned.
	///
	/// \note
	/// The query can be performed asynchronously only with map layers based on MapCore's Map Layer Server.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRasterLayerColorByPoint(
		IMcRasterMapLayer *pLayer, const SMcVector3D &Point, int nLOD, bool bNearestPixel,
		SMcBColor *pColor,
		const SQueryParams *pParams = NULL) = 0;
//@}

/// \name Traversability
//@{

	//==============================================================================
	// Method Name: GetTraversabilityAlongLine()
	//------------------------------------------------------------------------------
	/// Retrieves segments on a polyline that are traversable according to the specified traversability map layer.
	///
	/// \param[in]  pLayer						The traversability layer
	/// \param[in]	aLineVertices				The array of vertices of a polyline to check
	/// \param[in]	uNumLineVertices			The number of vertices in the above array
	/// \param[out]	paTraversabilitySegments	The array of segments found, each one has a flag whether it is traversable or not
	/// \param[in]	pParams						The query parameters or NULL to use the default
	///
	/// \note
	///		`pParams.eTerrainPrecision` is ignored, #EQP_HIGHEST is always used
	///
	/// \note
	/// The query can be performed asynchronously only with map layers based on MapCore's Map Layer Server.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTraversabilityAlongLine(
		IMcTraversabilityMapLayer *pLayer,
		const SMcVector3D aLineVertices[], UINT uNumLineVertices,
		CMcDataArray<IMcSpatialQueries::STraversabilityPoint> *paTraversabilitySegments,
		const SQueryParams *pParams = NULL) = 0;

//@}

/// \name Scan
//@{
	//==============================================================================
	// Method Name: ScanInGeometry()
	//------------------------------------------------------------------------------
	/// Scans the specified targets in the requested geometry
	///
	/// \param[in]	Geometry				geometry to scan
	/// \param[in]	bCompletelyInsideOnly	for items found: whether to include only 
	///										those that are completely inside the geometry
	/// \param[out]	paResults				requested targets found in the geometry 
	///										(sorted from nearest to farthest (3D only) and 
	///										after that from highest to lowest draw priority) 
	/// \param[in]	pParams					query parameters or NULL to use the default
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ScanInGeometry(const SMcScanGeometry &Geometry, 
		bool bCompletelyInsideOnly,	CMcDataArray<STargetFound> *paResults, 
		const SQueryParams *pParams = NULL) = 0;
//@}

/// \name Geographic Calculations
//@{
	//==============================================================================
	// Method Name: LocationFromTwoDistancesAndAzimuth()
	//------------------------------------------------------------------------------
	/// Calculates a target location based on two distances from known locations.
	///
	/// \param[in]	FstOrigin					First known location
	/// \param[in]	FstDistance					Distance from first known location
	/// \param[in]	FstAzimuth					Azimuth from first known location.
	/// \param[in]	SndOrigin					Second known location
	/// \param[in]	SndDistance					Distance from second known location
	/// \param[in]	dTargetHeightAboveGround	Target's height above ground (DTM).
	/// \param[out]	pTarget						The calculated target. If not found will return #v3MaxDouble
	/// \param[in]	pParams						query parameters or NULL to use the default
	///
	/// \note
	///		The input distances are considered as slant ranges. Iterative calculation is done to match results to DTM.
	///		\a FstAzimuth is used to distinguish between the two possible target locations with the same distances from known origins.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode LocationFromTwoDistancesAndAzimuth(
		const SMcVector3D	&FstOrigin,
		double				FstDistance,
		double				FstAzimuth,
		const SMcVector3D	&SndOrigin,
		double				SndDistance,
		double				dTargetHeightAboveGround,
		SMcVector3D			*pTarget, 
		const SQueryParams *pParams = NULL) = 0;
//@}

/// \name Asynchronous Queries
//@{

	//==============================================================================
	// Method Name: CancelAsyncQuery()
	//------------------------------------------------------------------------------
	/// Cancels asynchronous query identified by \a pAsyncQueryCallback previously specified in SQueryParams.pAsyncQueryCallback
	///
	/// \param[in]	pAsyncQueryCallback		asynchronous query callback
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode CancelAsyncQuery(IAsyncQueryCallback *pAsyncQueryCallback) = 0;

//@}

/// \name Area-Of-Sight Matrix
//@{
		//==============================================================================
		// Method Name: CloneAreaOfSightMatrix()
		//------------------------------------------------------------------------------
		/// Clones area-of-sight matrix; copying of \a aPointsVisibilityColors is optional
		///
		/// \param[in]	Source					area-of-sight matrix to clone
		/// \param[out]	pTarget					resulting area-of-sight matrix
		/// \param[in]	bFillPointsVisibility	whether to copy aPointsVisibilityColors to the resulting matrix
		///
		/// \return
		///     - status result
		//==============================================================================
		static SCENEMANAGER_API IMcErrors::ECode CloneAreaOfSightMatrix(const SAreaOfSightMatrix &Source, SAreaOfSightMatrix *pTarget, 
			bool bFillPointsVisibility);

		//==============================================================================
		// Method Name: SumAreaOfSightMatrices()
		//------------------------------------------------------------------------------
		/// Sums two area-of-sight matrix according to \a eScoutersSumType
		///
		/// \param[in,out]	pMatrix				area-of-sight matrix to modify
		/// \param[in]		MatrixToAdd			area-of-sight matrix to add to \a pMatrix
		/// \param[in]		eScoutersSumType	defines how to sum results for each point
		///
		/// \return
		///     - status result
		//==============================================================================
		static SCENEMANAGER_API IMcErrors::ECode SumAreaOfSightMatrices(SAreaOfSightMatrix *pMatrix, const SAreaOfSightMatrix &MatrixToAdd, 
			EScoutersSumType eScoutersSumType);

		//==============================================================================
		// Method Name: AreSameRectAreaOfSightMatrices()
		//------------------------------------------------------------------------------
		/// Checks if two area-of-sight matrices are based on the same rectangle and resolutions
		///
		/// \param[in]	First		first area-of-sight matrix
		/// \param[in]	Second		second area-of-sight matrix
		/// \param[out]	pbSameRect	whether two area-of-sight matrices are based on the same rectangle and resolutions
		///     
		/// \return
		///     - status result
		//==============================================================================
		static SCENEMANAGER_API IMcErrors::ECode AreSameRectAreaOfSightMatrices(const SAreaOfSightMatrix &First, const SAreaOfSightMatrix &Second, 
			bool *pbSameRect);
//@}

	/* for internal use */
	virtual IMcErrors::ECode GetDebugOption(UINT uKey, int *pnValue) = 0;
	virtual IMcErrors::ECode SetDebugOption(UINT uKey, int nValue) = 0;
	virtual IMcErrors::ECode IncrementDebugOption(UINT uKey) = 0;
};
