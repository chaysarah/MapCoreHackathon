#pragma once

//===========================================================================
/// \file IMcPathFinder.h
/// Interfaces for the vector Item
//===========================================================================

#include "McExports.h"

#include "IMcErrors.h"
#include "SMcVector.h"
#include "IMcDestroyable.h"
#include "CMcDataArray.h"

class IMcPathFinder : public IMcDestroyable
{
protected:
	virtual ~IMcPathFinder() {}
public:
	static MAPLAYER_API IMcErrors::ECode Create(IMcPathFinder **ppPathFinder,PCSTR strVectorData ,PCSTR strTableName,PCSTR strObstaclesTables[], UINT uNumObstacles, UINT uPointTolerance,
		PCSTR strCostFields[],UINT uNumCostFields);

	virtual IMcErrors::ECode UpdateTables() = 0 ;

	virtual IMcErrors::ECode FindShortestPath (const SMcVector3D &SourcePoint, const SMcVector3D &TargetPoint, PCSTR strCostField, PCSTR strReverseCostField, bool bConsiderObstacles, CMcDataArray<SMcVector3D> *paLocationPoints, CMcDataArray <UINT> *aEdgeIds) = 0;
};



