#include <windows.h>

#include "IMcErrors.h"
#include "Map/McMapManagement.h"
#include "OverlayManager/McOverlayManagement.h"
#include "Calculations/IMcSpatialQueries.h"
#include "Calculations/IMcGridConverter.h"
#include "Calculations/IMcGeographicCalculations.h"
#include "Calculations/IMcGeometricCalculations.h"
#include "Calculations/PhotogrammetricCalc/IMcImageCalc.h"
#include "Calculations/PhotogrammetricCalc/IMcAffineImageCalc.h"
#include "Calculations/PhotogrammetricCalc/IMcFrameImageCalc.h"
#include "Calculations/PhotogrammetricCalc/IMcLoropImageCalc.h"
#include "Calculations/PhotogrammetricCalc/IMcUserDefinedImageCalc.h"
#include "Production/IMcMapProduction.h"
#include "Production/IMcFileProductions.h"
#include "Production/IMcImagesCorrelator.h"
#include "SMcPlane.h"
#include "Map/IMcMapViewportRenderServer.h"
#include "Map/IMcMapLayersRenderServer.h"
#include "Osm/McOsmCommon.h"
#include "Osm/IMcOsmConnection.h"
#include "Osm/IMcOsmGeocoder.h"
#include "Osm/IMcOsmRvrsGeocoder.h"
#include "Osm/IMcOsmRouter.h"
#include "IMcImage.h"

#define MILITARY_UNIT_COLOR_ID 256
#define SCHEMATIC_VIEW_MODE 0x1
#define EMERGENCY_VIEW_MODE 0x2

IMcErrors::ECode SampleGraph()
{
	IMcErrors::ECode result = IMcErrors::SUCCESS;

    // Create the Overlay Manager
    //---------------------------------------------------
    IMcOverlayManager *pOverlayManager = NULL;
    IMcGridCoordSystemGeographic *pCoordSysDef;
	IMcGridCoordSystemGeographic::Create(&pCoordSysDef, IMcGridCoordinateSystem::EDT_WGS84);
    result = IMcOverlayManager::Create(&pOverlayManager, pCoordSysDef);
    MC_CHECK_ERROR(result);

    //Create a polygon item that will be drawn over the terrain texture
    //---------------------------------------------------
    IMcPolygonItem *pPolygon;
	result = IMcPolygonItem::Create(
		&pPolygon, IMcObjectSchemeItem::EISTF_SCREEN);
    MC_CHECK_ERROR(result);

    //Create the picture item
    //---------------------------------------------------
	IMcResourceTexture *pTexture;
	result = IMcResourceTexture::Create(&pTexture, 100, IMcResourceTexture::ERT_ICON, NULL, false/*FillPattern*/);
	MC_CHECK_ERROR(result);

	IMcPictureItem *pFlag;
	result = IMcPictureItem::Create(&pFlag, IMcObjectSchemeItem::EISTF_SCREEN, EPCS_SCREEN, pTexture);
	MC_CHECK_ERROR(result);

    //Create the Object Scheme
    //---------------------------------------------------
    IMcObjectScheme *pObjectScheme;
	IMcObjectLocation *pLocation;
	result = IMcObjectScheme::Create(&pObjectScheme, &pLocation,	pOverlayManager, EPCS_WORLD, true);
    MC_CHECK_ERROR(result);

    // Polygon connection
    //---------------------------------------------------
    result = pPolygon->Connect(pLocation);
    MC_CHECK_ERROR(result);

    result = pPolygon->SetFillColor(bcWhiteOpaque, MILITARY_UNIT_COLOR_ID);
    MC_CHECK_ERROR(result);

	// Top-most
    //---------------------------------------------------
	IMcEmptySymbolicItem *pTopMost = NULL;
	IMcEmptySymbolicItem::Create(&pTopMost);
	pTopMost->Connect(pPolygon);
	pTopMost->SetAttachPointType(0, IMcSymbolicItem::EAPT_SCREEN_TOP_MOST);

	// Flag 1
    //---------------------------------------------------
    IMcPictureItem *pFlag1;
	result = pFlag->Clone(&pFlag1);
    MC_CHECK_ERROR(result);

	result = pFlag1->Connect(pTopMost);
    MC_CHECK_ERROR(result);

	pFlag1->SetRectAlignment(IMcSymbolicItem::EBRP_BOTTOM_MIDDLE);
	// Screen offset
	pFlag1->SetCoordinateSystemConversion(EPCS_SCREEN, true);
	pFlag1->SetOffset(SMcFVector3D(0.0, -10.0, 0.0));

	// move if blocked
	pFlag1->SetMoveIfBlockedMaxChange(FLT_MAX);
	pFlag1->SetMoveIfBlockedHeightAboveObstacle(0);

	// connector line
    IMcLineItem *pConnectorLine;
    result = IMcLineItem::Create(&pConnectorLine, IMcObjectSchemeItem::EISTF_WORLD);
    MC_CHECK_ERROR(result);

	IMcObjectSchemeNode *apNodes[2] = { pTopMost, pFlag1 }; 
	pConnectorLine->Connect(apNodes, 2);
	pConnectorLine->SetAttachPointType(1, IMcSymbolicItem::EAPT_BOUNDING_BOX_POINT);
	pConnectorLine->SetBoundingBoxAttachPointType(1, IMcSymbolicItem::EBBPF_BOTTOM_MIDDLE);

    MC_CHECK_ERROR(result);

    result = pConnectorLine->SetLineColor(bcWhiteOpaque, MILITARY_UNIT_COLOR_ID);
    MC_CHECK_ERROR(result);

    // Flag 2
    //---------------------------------------------------

	IMcPictureItem *pFlag2;
	result = pFlag->Clone(&pFlag2);
    MC_CHECK_ERROR(result);

	pFlag2->Connect(pFlag1);
    MC_CHECK_ERROR(result);

    // Screen offset
	pFlag2->SetOffset(SMcFVector3D(-10.0, 0.0, 0.0));
    MC_CHECK_ERROR(result);

    // Flag 3
    //---------------------------------------------------

	IMcPictureItem *pFlag3;
	result = pFlag->Clone(&pFlag3);
    MC_CHECK_ERROR(result);

	pFlag3->Connect(pFlag1);
    MC_CHECK_ERROR(result);

    // Screen offset
	pFlag3->SetOffset(SMcFVector3D(10.0, 0.0, 0.0));
    MC_CHECK_ERROR(result);

    // Flag 4
    //---------------------------------------------------

	IMcPictureItem *pFlag4;
	result = pFlag->Clone(&pFlag4);
    MC_CHECK_ERROR(result);

	pFlag4->Connect(pFlag1);
    MC_CHECK_ERROR(result);

    // Screen offset
	pFlag4->SetOffset(SMcFVector3D(10.0, 0.0, 0.0));
    MC_CHECK_ERROR(result);

    // Visibility Selectors
    //---------------------------------------------------

	UINT windowList[3] = { 1, 2, 3 };
    float minScale = 0;
    float maxScale = 5;

    UINT cancelMode = SCHEMATIC_VIEW_MODE | EMERGENCY_VIEW_MODE;
    UINT cancelModeVisibility = EMERGENCY_VIEW_MODE;

    IMcViewportConditionalSelector *pWindowConditionalSelector;
    result = IMcViewportConditionalSelector::Create(
		pOverlayManager,
		&pWindowConditionalSelector,
		IMcViewportConditionalSelector::EVT_3D_VIEWPORT,
		IMcViewportConditionalSelector::EVCS_ALL_COORDINATE_SYSTEMS,
		windowList,
		3,
		true);
    MC_CHECK_ERROR(result);

    IMcScaleConditionalSelector *pScaleConditionalSelector;
    result = IMcScaleConditionalSelector::Create(
		pOverlayManager,
		&pScaleConditionalSelector,
		minScale, maxScale, 
        cancelMode, cancelModeVisibility);
    MC_CHECK_ERROR(result);

    IMcBooleanConditionalSelector *pAndSelector;
    IMcConditionalSelector *SelectorList[] = {
        pWindowConditionalSelector, 
        pScaleConditionalSelector
    };
    result = IMcBooleanConditionalSelector::Create(
		pOverlayManager,
		&pAndSelector,
		SelectorList, 2, 
        IMcBooleanConditionalSelector::EB_AND);
    MC_CHECK_ERROR(result);

	result = pFlag1->SetConditionalSelector(IMcConditionalSelector::EAT_VISIBILITY, true, pAndSelector);
    MC_CHECK_ERROR(result);

    //Create the overlay;
    //---------------------------------------------------
    IMcOverlay *pOverlay;
	result = IMcOverlay::Create(&pOverlay, pOverlayManager);
    MC_CHECK_ERROR(result);

    //Create the object;
    //---------------------------------------------------
    IMcObject *pObject;
    SMcVector3D aPoints[] = 
	{
		SMcVector3D(1.0,1.0,0.0),
        SMcVector3D(2.0,2.0,0.0),
        SMcVector3D(3.0,3.0,0.0),
        SMcVector3D(4.0,4.0,0.0)
    };
	result = IMcObject::Create(&pObject, pOverlay, pObjectScheme, aPoints, 3);
	MC_CHECK_ERROR(result);

    // Set color property
    //---------------------------------------------------
    result = pObject->SetColorProperty(MILITARY_UNIT_COLOR_ID, SMcBColor(255, 255, 0, 255));
    MC_CHECK_ERROR(result);

    // Update color property
    //---------------------------------------------------
    result = pObject->SetColorProperty(MILITARY_UNIT_COLOR_ID, SMcBColor(0, 255, 0, 255));
    MC_CHECK_ERROR(result);

    return IMcErrors::SUCCESS;
}

