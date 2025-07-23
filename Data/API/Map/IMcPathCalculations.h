#pragma once

//===========================================================================
/// \file IMcPathCalculations.h
/// Interfaces for path calculations
//===========================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "SMcVector.h"
#include "IMcDestroyable.h"

class IMcMapProduction;
class IMcMapViewport;
class IMcOverlay;
class IMcVectorMapLayer;
class IMcObject;
class IMcObjectScheme;
class IMcObjectSchemeItem;
//class SMcBox;

class IMcPathCalculations : public IMcDestroyable
{
protected:
	virtual ~IMcPathCalculations() {};

public:

	struct SProcessPathParams
	{
		SProcessPathParams()
		{
			//memset(this, 0, sizeof(*this));

			fMaxDistInMetersBetweenOrgPointsInPathSegment = 20;
			fMaxDistInMetersBetweenSamePathSegments = 10;
			fMaxDistInMetersBetweenCheckPointsInPathSegment = 5;

			uMinNumSequencedPointsInCommonSegment = 2;
		}

		float fMaxDistInMetersBetweenOrgPointsInPathSegment; // if dist is longer, points are considered to be in diff segments
		float fMaxDistInMetersBetweenSamePathSegments; // if dist is longer, paths are considered to be diff
		float fMaxDistInMetersBetweenCheckPointsInPathSegment; // max dist between check points in a path segment. since check points are added to org points, actual dist between points may be smaller!
		UINT uMinNumSequencedPointsInCommonSegment; // min length in meters is <= ((uMinNumSequencedPointsInCommonSegment-1)*ffMaxDistInMetersBetweenCheckPointsInPathSegment)
	};

	static OVERLAYMANAGER_API IMcErrors::ECode Create(IMcPathCalculations **ppPathCalculations, const SProcessPathParams &oParams);

	virtual IMcErrors::ECode ProcessPaths(
		IMcMapViewport *pMapViewport,
		IMcOverlay* const aInputOverlays[], UINT uNumInputOverlays, IMcVectorMapLayer* const aInputVectorLayers[], UINT uNumInputVectorLayers,
		const SMcBox *pBoundingBox, IMcOverlay *pOutputOverlay, IMcMapProduction *pMapProduction, PCSTR strOutputHeatMapDirectory, bool bSetSpeed) = 0;
};
