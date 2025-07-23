/****************************************************************************
**
** Copyright (C) 2008 Nokia Corporation and/or its subsidiary(-ies).
** Contact: Qt Software Information (qt-info@nokia.com)
**
** This file is part of the documentation of Qt. It was originally
** published as part of Qt Quarterly.
**
** Commercial Usage
** Licensees holding valid Qt Commercial licenses may use this file in
** accordance with the Qt Commercial License Agreement provided with the
** Software or, alternatively, in accordance with the terms contained in
** a written agreement between you and Nokia.
**
**
** GNU General Public License Usage
** Alternatively, this file may be used under the terms of the GNU
** General Public License versions 2.0 or 3.0 as published by the Free
** Software Foundation and appearing in the file LICENSE.GPL included in
** the packaging of this file.  Please review the following information
** to ensure GNU General Public Licensing requirements will be met:
** http://www.fsf.org/licensing/licenses/info/GPLv2.html and
** http://www.gnu.org/copyleft/gpl.html.  In addition, as a special
** exception, Nokia gives you certain additional rights. These rights
** are described in the Nokia Qt GPL Exception version 1.3, included in
** the file GPL_EXCEPTION.txt in this package.
**
** Qt for Windows(R) Licensees
** As a special exception, Nokia, as the sole copyright holder for Qt
** Designer, grants users of the Qt/Eclipse Integration plug-in the
** right for the Qt/Eclipse Integration to link to functionality
** provided by Qt Designer and its related libraries.
**
** If you are unsure which license is appropriate for your use, please
** contact the sales department at qt-sales@nokia.com.
**
****************************************************************************/


#include "openglscene.h"
//#include "model.h"
//////
#ifndef _WIN32
#include <dirent.h>
#endif
#include <stdio.h>

#include <QBoxLayout>

//////
#include <QtGui>
#include <QtOpenGL>

#include "McBasicTypes.h"
#include  "IMcErrors.h"
#include "Map/IMcDtmMapLayer.h"
#include "Map/IMcRasterMapLayer.h"
#include "Map/IMcStaticObjectsMapLayer.h"
#include "Map/IMcMapTerrain.h"
#include "Map/IMcMapGrid.h"
#include "OverlayManager/IMcLightBasedItem.h"
#include "OverlayManager/IMcOverlayManager.h"
#include "OverlayManager/IMcOverlay.h"
#include "OverlayManager/IMcObject.h"
#include "OverlayManager/IMcObjectScheme.h"
#include "OverlayManager/IMcPolygonItem.h"
#include "OverlayManager/IMcPictureItem.h"
#include "OverlayManager/IMcTexture.h"
#include "OverlayManager/IMcMeshItem.h"
#include "OverlayManager/IMcMesh.h"
#include "OverlayManager/IMcFont.h"
#include "OverlayManager/IMcTextItem.h"
#include <QTime>
#include <jni.h>
#include <QAndroidJniObject>
#include <qpa/QPlatformNativeInterface.h>

///

#include "calctypes.h"
//#include "convert.h"

///

#ifndef GL_MULTISAMPLE
#define GL_MULTISAMPLE  0x809D
#endif

#define GOOD_SCHEME_ID                          10111
#define BAD_SCHEME_ID                           10222
#define FIRST_SCHEME_ID                         10333
#define SYMBOL_SCHEME_ID                        10555
#define SECOND_SCHEME_ID                        10444

#define ICON_BASE								31000
#define TEXT_BASE								41000

#define BASE_LINE_WIDTH_ID_OFFSET				1
#define BASE_LINE_STYLE_ID_OFFSET				2
#define BASE_LINE_COLOR_ID_OFFSET				3
#define BASE_LINE_TEXTURE_ID_OFFSET				4
#define TEXT_TEXT_ID_OFFSET                     10
#define TEXT_BACKGROUND_COLOR_ID_OFFSET         20
#define ADDITIONAL_LINE_OFFSET					100
#define PICTURE_TEXTURE_ID_OFFSET				50

#define PROPID_LINE_WIDTH		LINE_NODE + BASE_LINE_WIDTH_ID_OFFSET				// 21001
#define PROPID_LINE_STYLE		LINE_NODE + BASE_LINE_STYLE_ID_OFFSET				// 21002
#define PROPID_LINE_COLOR		LINE_NODE + BASE_LINE_COLOR_ID_OFFSET				// 21003
#define PROPID_LINE_TEXTURE		LINE_NODE + BASE_LINE_TEXTURE_ID_OFFSET				// 21004

#define PROPID_ICON				ICON_BASE + PICTURE_TEXTURE_ID_OFFSET				// 31050
#define PROPID_TEXT				TEXT_BASE + TEXT_TEXT_ID_OFFSET						// 41010
#define PROPID_TEXT_BACK_COLOR	TEXT_BASE + TEXT_BACKGROUND_COLOR_ID_OFFSET			// 41020

#define PRIMARY_SCHEMA_GEO_ITEM_ID              10000

#define LINE_NODE                               21000
#define BASE_LINE_WIDTH_ID_OFFSET				1
#define BASE_LINE_STYLE_ID_OFFSET				2
#define BASE_LINE_COLOR_ID_OFFSET				3
#define BASE_LINE_TEXTURE_ID_OFFSET				4
#define ADDITIONAL_LINE_OFFSET					100

#define PROPID_LINE_WIDTH		LINE_NODE + BASE_LINE_WIDTH_ID_OFFSET				// 21001
#define PROPID_LINE_STYLE		LINE_NODE + BASE_LINE_STYLE_ID_OFFSET				// 21002
#define PROPID_LINE_COLOR		LINE_NODE + BASE_LINE_COLOR_ID_OFFSET				// 21003
#define PROPID_LINE_TEXTURE		LINE_NODE + BASE_LINE_TEXTURE_ID_OFFSET				// 21004

#define PROPID_ADDIT_LINE_WIDTH	LINE_NODE + ADDITIONAL_LINE_OFFSET + BASE_LINE_WIDTH_ID_OFFSET				// 21101
#define PROPID_ADDIT_LINE_COLOR	LINE_NODE + ADDITIONAL_LINE_OFFSET + BASE_LINE_COLOR_ID_OFFSET				// 21103
#define PROPID_ADDIT_LINE_STYLE LINE_NODE + ADDITIONAL_LINE_OFFSET + BASE_LINE_STYLE_ID_OFFSET				// 21102
#define PROP_ID_ADDIT_LINE_TEXTURE	LINE_NODE + ADDITIONAL_LINE_OFFSET + BASE_LINE_TEXTURE_ID_OFFSET		// 21104

#define A_ICON_A                                31000
#define A_ICON_B                                71000
#define A_ICON_C                                91000
#define T_TEXT_UNIQUE_DESIGNATION_A		41300
#define XY_TEXT_A                               41100

#define UTIL_LOCATION_ACTIVE 0
#define UTIL_TELEMETRY_ACTIVE 1
#define UTIL_CAMERA_ACTIVE 1
#define SMOOTH_ROTATE_FRAMES 30
#define GEO_MAPS 1


float OpenGLScene::m_fYaw = 0;
float OpenGLScene::m_fPitch = 0;
float OpenGLScene::m_fRoll = 0;


QDialog *OpenGLScene::createDialog(const QString &windowTitle) const
{
    QDialog *dialog = new QDialog(0, Qt::CustomizeWindowHint | Qt::WindowTitleHint);

    dialog->setWindowOpacity(0.8);
    dialog->setWindowTitle(windowTitle);
    dialog->setLayout(new QVBoxLayout);

    return dialog;
}

OpenGLScene::OpenGLScene()
    : m_wireframeEnabled(false)
    , m_normalsEnabled(false)
    , m_modelColor(153, 255, 0)
    , m_backgroundColor(0, 170, 255)
    , m_model(0)
    , m_bAttachedOrientation(false)
    , m_bRotateAroundCenter(false)
    , m_distance(1.4f)
    , m_angularMomentum(0, 40, 0)
    , m_p2DViewport(NULL)
    , m_p3DViewport(NULL)
    , m_pViewport(NULL)
    , m_nResizeCounter(0)
    , m_oldDist(1)
    , m_eDisplayType(EDisplayType::EDT_2D)
    , m_pNativeDtmLayer(NULL)
    , m_pNativeRasterLayer(NULL)
    , m_pRawDtmLayer(NULL)
    , m_pRawRasterLayer(NULL)
    , m_pRawVectorLayer(NULL)
    , m_pBuildingSoLayer (NULL)
    , m_pIvorySoLayer (NULL)
 //   , m_pAccoSoLayer (NULL)
    , m_pElyakimSoLayer (NULL)
    , m_pTerrain(NULL)
    , m_pArbelTerrain(NULL)
    , m_bTestInRender(false)
    , m_pFPSText(NULL)
    , m_pFPS(NULL)
    , m_pSightPresentation(NULL)
    ,m_pPolygon(NULL)
{
    m_time.start();



#ifdef _WIN32
    m_TerrainInfo.m_NativeDtmDirectory = "C:/Maps/Israel/Dtm";
    m_TerrainInfo.m_NativeRasterDirectory = "C:/Maps/Israel/Raster";
#else

#ifdef GEO_MAPS
    m_TerrainInfo.m_NativeDtmDirectory = "/sdcard/MapCore/NetanyaGeoWgs84/Dtm";
    m_TerrainInfo.m_NativeRasterDirectory = "/sdcard/MapCore/NetanyaGeoWgs84/RasterEtc";
#else
    m_TerrainInfo.m_NativeRasterDirectory = "/sdcard/MapCore/NetanyaUtmEd50/RasterEtc";
    m_TerrainInfo.m_NativeDtmDirectory = "/sdcard/MapCore/NetanyaUtmEd50/Dtm";
//      m_TerrainInfo.m_NativeRasterDirectory = "/sdcard/Arbel/Raster";
//      m_TerrainInfo.m_NativeDtmDirectory = "/sdcard/Arbel/DTM";

#endif

#endif
    m_javaClass = NULL;
    m_jniReady = false;

 }

#ifdef __cplusplus
extern "C" {
#endif

JNIEXPORT void JNICALL Java_com_elbit_mapcore_utils_IMcNativeTelemetryUpdatesListener_OnTelemetryUpdate (JNIEnv *env, jobject obj, jfloat Yaw,jfloat Pitch,jfloat Roll )
{
//    qDebug() << "Yaw:" << Yaw << " Pitch:" << Pitch << " Roll:" << Roll << "\n" ;
        OpenGLScene::m_fYaw =  Yaw;

    OpenGLScene::m_fPitch = Pitch;
    OpenGLScene::m_fRoll = Roll;
}

JNIEXPORT void JNICALL Java_com_elbit_mapcore_utils_IMcNativeLocationUpdatesListener_OnLocationUpdate (JNIEnv *env, jobject obj, jdouble Long,jdouble Lat )
{
//    qDebug() << "Long:" << Long << " Lat" << Lat  << "\n" ;
}

JNIEXPORT void JNICALL Java_org_qtproject_example_cube_IMcNativeCubeActivityListener_OnPause ()
{
    if (UTIL_CAMERA_ACTIVE)
    {
        QAndroidJniObject::callStaticMethod<void>("com/elbit/mapcore/utils/IMcGLDeviceCamera",
                                           "onPause",
                                           "()V");
    }
    if (UTIL_TELEMETRY_ACTIVE)
    {
        QAndroidJniObject::callStaticMethod<void>("com/elbit/mapcore/utils/IMcTelemetryUpdates",
                                           "onPause",
                                           "()V");
    }
    if (UTIL_LOCATION_ACTIVE)
    {
        QAndroidJniObject::callStaticMethod<void>("com/elbit/mapcore/utils/IMcLocationUpdates",
                                           "onPause",
                                           "()V");
    }

//    qDebug() << "JNICALL OnPause"  << "\n" ;
}

JNIEXPORT void JNICALL Java_org_qtproject_example_cube_IMcNativeCubeActivityListener_OnResume ()
{
    if (UTIL_CAMERA_ACTIVE)
    {
       QAndroidJniObject::callStaticMethod<void>("com/elbit/mapcore/utils/IMcGLDeviceCamera",
                                         "onResume",
                                         "()V");
    }
    if (UTIL_TELEMETRY_ACTIVE)
    {
       QAndroidJniObject::callStaticMethod<void>("com/elbit/mapcore/utils/IMcTelemetryUpdates",
                                         "onResume",
                                         "()V");
    }
    if (UTIL_LOCATION_ACTIVE)
    {
       QAndroidJniObject::callStaticMethod<void>("com/elbit/mapcore/utils/IMcLocationUpdates",
                                          "onResume",
                                         "()V");
    }
    qDebug() << "JNICALL OnResume"  << "\n" ;
}

#ifdef __cplusplus
}
#endif



void OpenGLScene::initializeGL()
{



}
/*
JNIEXPORT jint JNI_OnLoad(JavaVM* vm, void* )
{
    JNIEnv* env;
    if (vm->GetEnv(reinterpret_cast<void**>(&env), JNI_VERSION_1_6) != JNI_OK) {
        qDebug() << "ERROR: JNI version";
        return JNI_ERR;
    }
    jclass javaClass = env->FindClass("com/elbit/mapcore/utils/IMcLocationUpdates");
    if (!javaClass) {
        qDebug() << "ERROR: class IMcLocationUpdates not found";
        return JNI_ERR;
    } else {
        qDebug() << "JNI Success: class IMcLocationUpdates found";
    }
    return JNI_VERSION_1_6;
}
*/

void OpenGLScene::drawBackground(QPainter *painter, const QRectF &)
{

    if (UTIL_CAMERA_ACTIVE && m_eDisplayType == EDT_AR)
    {
        QAndroidJniObject::callStaticMethod<void>("com/elbit/mapcore/utils/IMcGLDeviceCamera",
                                           "Render",
                                           "()V");
    }

   if (m_pViewport)
   {
        m_pViewport->Render();
        m_FPS.update();
        RenderUpdates();
    }
    float fDeltaYaw = m_time.elapsed() / 40;
   if(fDeltaYaw >= 1.f)
   {
       if (m_bRotateAroundCenter)
       {
           m_pViewport->RotateCameraAroundWorldPoint(m_CenterPivotPoint, fDeltaYaw ,0,0,false);
       }
       m_time.restart();
   }


 //   m_frameCount++;
  //  QTimer::singleShot(100, this, SLOT(update()));
QTimer::singleShot(1, this, SLOT(update()));
}

void OpenGLScene::CreateMapDevice()
{

    IMcMapDevice::SInitParams InitParams;
    InitParams.eLoggingLevel = IMcMapDevice::ELL_MEDIUM;
#ifndef _WIN32
    InitParams.strConfigFilesDirectory ="/sdcard/MapCore/";
    InitParams.strLogFileDirectory ="/sdcard/MapCore/";
#else
    InitParams.strConfigFilesDirectory ="C:/Prj/MapCore7/Dev/MapCore/Android/Test/cube/CfgFiles";
#endif
    InitParams.uNumBackgroundThreads =1;
    IMcErrors::ECode res = IMcMapDevice::Create(&m_pMapDevice, InitParams);
    m_pMapDevice->AddRef();
    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create Device");
            printf ("%d",res);
            return ;
    }
    printf("Create Device Success...\n");

}
void OpenGLScene::CreateGridCoordinates()
{
#ifdef GEO_MAPS
    IMcGridCoordSystemGeographic *pGeoGrid;
    IMcGridCoordSystemGeographic::Create(&pGeoGrid,IMcGridCoordSystemGeographic::EDT_WGS84);
    m_pGridCoordinateSystem = pGeoGrid;
#else
    IMcGridUTM *pUTMGrid;
    IMcGridUTM::Create(&pUTMGrid, 36, IMcGridCoordinateSystem::EDT_ED50_ISRAEL);
    m_pGridCoordinateSystem = pUTMGrid;
#endif
    m_pGridCoordinateSystem->AddRef();
}
void OpenGLScene::CreateNativeRasterLayer()
{
    if ( m_TerrainInfo.m_NativeRasterDirectory.isEmpty())
    {
        return;
    }

    IMcErrors::ECode res = IMcNativeRasterMapLayer::Create(&m_pNativeRasterLayer, m_TerrainInfo.m_NativeRasterDirectory.toStdString().c_str());

    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create Raster layer");
            printf ("%d",res);
            return ;
    }
    m_pNativeRasterLayer->AddRef();
    printf("Create Raster Layer Success...\n");
}
void OpenGLScene::CreateRawVectorLayer()
{
    if ( m_TerrainInfo.m_RawVectorDirectory.isEmpty())
    {
        return;
    }

    IMcErrors::ECode res = IMcRawVectorMapLayer::Create(&m_pRawVectorLayer, m_TerrainInfo.m_RawVectorDirectory.toLatin1(), m_pGridCoordinateSystem , m_pGridCoordinateSystem , 0 ,1000000 , "/sdcard/MapCore/ArabArmy_A.png"  );

    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create Raster layer");
            printf ("%d",res);
            return ;
    }
    m_pRawVectorLayer->AddRef();
    printf("Create Vector Layer Success...\n");
}

void OpenGLScene::CreateRawRasterLayer()
{
    if ( m_TerrainInfo.m_RawRasterDirectory.isEmpty())
    {
        return;
    }
    IMcMapLayer::SRawParams Params;
    IMcMapLayer::SComponentParams aComponent;
    aComponent.eType = IMcMapLayer::ECT_DIRECTORY;
    strcpy (aComponent.strName, m_TerrainInfo.m_RawRasterDirectory.toStdString().c_str());
    aComponent.WorldLimit = SMcBox(DBL_MAX,DBL_MAX,DBL_MAX,DBL_MAX,DBL_MAX,DBL_MAX);

    Params.aComponents = &aComponent;
    Params.uNumComponents = 1;
    Params.pCoordinateSystem = m_pGridCoordinateSystem;
    Params.uMaxNumOpenFiles = 500;
    IMcErrors::ECode res = IMcRawRasterMapLayer::Create(&m_pRawRasterLayer, Params   );

    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create Raster layer");
            printf ("%d",res);
            return ;
    }
    m_pRawRasterLayer->AddRef();
    printf("Create Raster Layer Success...\n");
}

void OpenGLScene::CreateNativeDtmLayer()
{
    if ( m_TerrainInfo.m_NativeDtmDirectory.isEmpty())
    {
        return;
    }

    IMcErrors::ECode res = IMcNativeDtmMapLayer::Create(&m_pNativeDtmLayer, m_TerrainInfo.m_NativeDtmDirectory.toStdString().c_str() );

    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create Dtm layer");
            printf ("%d",res);
            return ;
    }
    m_pNativeDtmLayer->AddRef();
    printf("Create Dtm Layer Success...\n");

}

void OpenGLScene::CreateRawDtmLayer()
{
    if ( m_TerrainInfo.m_RawDtmDirectory.isEmpty())
    {
        return;
    }
    IMcMapLayer::SRawParams Params;
    IMcMapLayer::SComponentParams aComponent;
    aComponent.eType = IMcMapLayer::ECT_DIRECTORY;
    strcpy (aComponent.strName, m_TerrainInfo.m_RawDtmDirectory.toStdString().c_str());
    aComponent.WorldLimit = SMcBox(DBL_MAX,DBL_MAX,DBL_MAX,DBL_MAX,DBL_MAX,DBL_MAX);

    Params.aComponents = &aComponent;
    Params.uNumComponents = 1;
    Params.pCoordinateSystem = m_pGridCoordinateSystem;

    IMcErrors::ECode res = IMcRawDtmMapLayer::Create(&m_pRawDtmLayer,Params);

    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create Dtm layer");
            printf ("%d",res);
            return ;
    }
    m_pRawDtmLayer->AddRef();
    printf("Create Dtm Layer Success...\n");

}

void OpenGLScene::CreateBuildingSolLayer()
{
    CHAR strBuildingSolPath[256];

#ifdef GEO_MAPS
    strcpy(strBuildingSolPath,"/sdcard/MapCore/NetanyaGeoWgs84/BuildingsSol");
#else
    strcpy(strBuildingSolPath,"/sdcard/MapCore/NetanyaUtmEd50/BuildingsSol");
#endif


    IMcErrors::ECode res = IMcNativeStaticObjectsMapLayer::Create(&m_pBuildingSoLayer, strBuildingSolPath);

    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create Sol layer");
            printf ("%d",res);
            return ;
    }
    m_pBuildingSoLayer->AddRef();
    printf("Create Sol Layer Success...\n");

}

void OpenGLScene::CreateIvorySolLayer()
{
    CHAR strIvorySolPath[256];

#ifdef GEO_MAPS
//    strcpy(strIvorySolPath,"/sdcard/MapCore/NetanyaGeoWgs84/BuildingsSol");
#else
    strcpy(strIvorySolPath,"/sdcard/MapCore/NetanyaUtmEd50/IvorySmall");
#endif


    IMcErrors::ECode res = IMcNativeStaticObjectsMapLayer::Create(&m_pIvorySoLayer, strIvorySolPath);

    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create Sol layer");
            printf ("%d",res);
            return ;
    }
    m_pIvorySoLayer->AddRef();
    printf("Create Sol Layer Success...\n");

}
/*
void OpenGLScene::CreateAccoSolLayer()
{
    CHAR strAccoSolPath[256];

#ifdef GEO_MAPS
//    strcpy(strAccoSolPath,"/sdcard/MapCore/NetanyaGeoWgs84/BuildingsSol");
#else
    strcpy(strAccoSolPath,"/sdcard/MapCore/NetanyaUtmEd50/AccoSol");
#endif


    IMcErrors::ECode res = IMcNativeStaticObjectsMapLayer::Create(&m_pAccoSoLayer, strAccoSolPath);

    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create Sol layer");
            printf ("%d",res);
            return ;
    }
    m_pAccoSoLayer->AddRef();
    printf("Create Sol Layer Success...\n");

}
*/
void OpenGLScene::CreateElyakimSolLayer()
{
    CHAR strElyakimSolPath[256];

#ifdef GEO_MAPS
//    strcpy(strElyakimSolPath,"/sdcard/MapCore/NetanyaGeoWgs84/BuildingsSol");
#else
    strcpy(strElyakimSolPath,"/sdcard/MapCore/NetanyaUtmEd50/ElyakimSol3B");
#endif


    IMcErrors::ECode res = IMcNativeStaticObjectsMapLayer::Create(&m_pElyakimSoLayer, strElyakimSolPath);

    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create Sol layer");
            printf ("%d",res);
            return ;
    }
    m_pElyakimSoLayer->AddRef();
    printf("Create Sol Layer Success...\n");

}

void OpenGLScene::CreateMapTerrain()
{
    IMcMapLayer *MapLayers[6];
    int nCurIndex = 0;
    if (m_pNativeDtmLayer)
    {
        MapLayers[nCurIndex] =m_pNativeDtmLayer;
        nCurIndex++;
    }
    else if (m_pRawDtmLayer)
    {
        MapLayers[nCurIndex] =m_pRawDtmLayer;
        nCurIndex++;
    }
    if (m_pNativeRasterLayer)
    {
        MapLayers[nCurIndex] =m_pNativeRasterLayer;
        nCurIndex++;
    }
    if (m_pRawRasterLayer)
    {
        MapLayers[nCurIndex] =m_pRawRasterLayer;
        nCurIndex++;
    }
    if (m_pRawVectorLayer)
    {
        MapLayers[nCurIndex] =m_pRawVectorLayer;
        nCurIndex++;
    }
    if (m_pBuildingSoLayer)
    {
        MapLayers[nCurIndex] = m_pBuildingSoLayer;
        nCurIndex++;
    }

    if (m_pIvorySoLayer)
    {
        MapLayers[nCurIndex] = m_pIvorySoLayer;
        nCurIndex++;
    }

 /*   if (m_pAccoSoLayer)
    {
        MapLayers[nCurIndex] = m_pAccoSoLayer;
        nCurIndex++;
    }*/

    if (m_pElyakimSoLayer)
    {
        MapLayers[nCurIndex] = m_pElyakimSoLayer;
        nCurIndex++;
    }


    IMcErrors::ECode res = IMcMapTerrain::Create(&m_pTerrain, m_pGridCoordinateSystem,MapLayers, nCurIndex );
    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create Terrain\n");
            printf ("%d",res);
            return ;
    }
    m_pTerrain->AddRef();
    printf("Create MapTerrain Success...\n");

}

void OpenGLScene::CreateOverlayManager()
{
     IMcErrors::ECode res=IMcOverlayManager::Create(&m_pOverlayManager,m_pGridCoordinateSystem);
    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create IMcOverlayManager");
            printf ("%d",res);
            return ;
    }
    m_pOverlayManager->AddRef();

    m_pOverlayManager->SetItemSizeFactors(IMcOverlayManager::ESizePropertyType::ESPT_PICTURE_SIZE | IMcOverlayManager::ESizePropertyType::ESPT_OFFSET | IMcOverlayManager::ESizePropertyType::ESPT_LINE_WIDTH, 3.0) ;

}
void OpenGLScene::CreateOverlays()
{
    IMcErrors::ECode res=IMcOverlay::Create(&m_pOverlay,m_pOverlayManager);
    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create IMcOverlay");
            printf ("%d",res);
            return ;
    }
    m_pOverlay->AddRef();

    res=IMcOverlay::Create(&m_pMulObjsOverlay,m_pOverlayManager);
    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create IMcOverlay");
            printf ("%d",res);
            return ;
    }
    m_pMulObjsOverlay->AddRef();

}

void OpenGLScene::CreateEditMode()
{
    IMcErrors::ECode res = IMcErrors::SUCCESS;
    res=IMcEditMode::Create(m_p2DViewport,&m_p2DEditMode);
    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create IMcEditMode");
            printf ("%d",res);
            return ;
    }
    res=IMcEditMode::Create(m_p3DViewport,&m_p3DEditMode);
    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create IMcEditMode");
            printf ("%d",res);
            return ;
    }
    res=IMcEditMode::Create(m_pARViewport,&m_pAREditMode);
    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create IMcEditMode");
            printf ("%d",res);
            return ;
    }

    if (m_eDisplayType == EDisplayType::EDT_2D)
    {
        m_pEditMode = m_p2DEditMode;
    }
    else if (m_eDisplayType == EDisplayType::EDT_3D)
    {
        m_pEditMode = m_p3DEditMode;
    }
    else if (m_eDisplayType == EDisplayType::EDT_AR)
    {
        m_pEditMode = m_pAREditMode;
    }
    m_pEditMode->StartNavigateMap(false);
}

void OpenGLScene::SetCameraScale (float fScale,const QPointF &CenterPoint)
{
    qDebug() << "OpenGLScene::SetCameraScale" <<"\n";

    if (m_pViewport == NULL)
    {
        return ;
    }
    if (m_eDisplayType == EDisplayType::EDT_2D)
    {
         m_pViewport->SetCameraScale(fScale);
    }
    else
    {

        SMcVector3D CameraPosition;
        SMcVector3D NewCameraPosition;
        SMcVector3D CameraTargetPosition;
        if (ScreenToWorld(CenterPoint , &CameraTargetPosition) == false)
        {
            return ;
        }

        m_pViewport->GetCameraPosition(&CameraPosition);
        NewCameraPosition = CameraTargetPosition.GetLinearInterpolationWith(CameraPosition,fScale);
        m_pViewport->SetCameraPosition(NewCameraPosition);



    }
}

void OpenGLScene::GetCameraScale (float *fScale, const SMcVector3D &CenterPoint)
{
    if (m_pViewport == NULL)
    {
        return ;
    }

    m_pViewport->GetCameraScale(fScale/*,CenterPoint*/);
}
void OpenGLScene::WorldToScreen(const SMcVector3D &WorldPoint,  SMcVector3D *pScreenPoint)
{
    if (m_pViewport == NULL)
    {
        return ;
    }

    m_pViewport->WorldToScreen(WorldPoint,pScreenPoint);
}

bool OpenGLScene::ScreenToWorld(const QPointF &ScreenPoint, SMcVector3D *pWorldPoint)
{
    bool bIntersect = false;
    if (m_pViewport == NULL)
    {
        return bIntersect;
    }

    SMcVector3D ScrnPoint (ScreenPoint.x() ,ScreenPoint.y(),0 );
    if (m_pRawDtmLayer || m_pNativeDtmLayer)
    {
        m_pViewport->ScreenToWorldOnTerrain(ScrnPoint,pWorldPoint,&bIntersect);
    }
    else
    {
        m_pViewport->ScreenToWorldOnPlane(ScrnPoint,pWorldPoint,&bIntersect);
    }
    return bIntersect;
}

void OpenGLScene::SetCameraOrientation(float fYaw, const QPointF &CenterPoint)
{
    if (m_pViewport == NULL)
    {
        return ;
    }

    SMcVector3D WorldPoint;
    if (m_eDisplayType == EDisplayType::EDT_2D)
    {
        qDebug() << "OpenGLScene::SetCameraOrientation EDisplayType::EDT_2D" <<"\n";

        m_pViewport->SetCameraOrientation(fYaw,FLT_MAX ,FLT_MAX ,true);
    }
    else
    {
        if (ScreenToWorld(CenterPoint,&WorldPoint) == false)
        {
            return;
        }
        qDebug() << "OpenGLScene::SetCameraOrientation EDisplayType::EDT_3D" <<"\n";

        m_pViewport->RotateCameraAroundWorldPoint(WorldPoint,fYaw);
    }
}

void OpenGLScene::SetCameraPitch(float fPitch)
{
    if (m_pViewport == NULL)
    {
        return ;
    }

    float fCamYaw , fCamPitch , fCamRoll;
    m_pViewport->GetCameraOrientation(&fCamYaw,&fCamPitch,&fCamRoll);
    fCamPitch += fPitch;
    if (fCamPitch > 90 || fCamPitch < -90)
    {
        return;
    }
    m_pViewport->SetCameraOrientation(fCamYaw,fCamPitch,fCamRoll);
}

void OpenGLScene::GetCameraOrientation(float *fYaw)
{
    if (m_pViewport == NULL)
    {
        return ;
    }

    m_pViewport->GetCameraOrientation(fYaw);
 }

bool OpenGLScene::IsDisplay2D()
{
   return (m_eDisplayType == EDT_2D);
}

void OpenGLScene::ScrollCamera(int nDeltaX, int nDeltaY)
{
    if (m_pViewport == NULL)
    {
        return ;
    }

    m_pViewport->ScrollCamera(nDeltaX,nDeltaY);
}

void OpenGLScene::SetCameraPosition(SMcVector3D Pos,bool bRelative)
{
    if (m_pViewport == NULL)
    {
        return ;
    }

    m_pViewport->SetCameraPosition(Pos,bRelative);
}
void OpenGLScene::StartEditMode()
{
    bool bStatus = false;
    m_pEditMode->IsEditingActive(&bStatus);
    if (bStatus)
    {
        return;
    }
    m_pEditMode->StartNavigateMap(false,true,false);
}

void OpenGLScene::SetWinId(long lWinId)
{
    m_lWinId = lWinId;
}

IMcMapGrid *OpenGLScene::CreateGrid()
{
    IMcMapGrid* pGrid;
    IMcMapGrid::SGridRegion gridRegion[1];

    gridRegion[0].pCoordinateSystem = m_pGridCoordinateSystem;


    IMcLineItem* pLineItem = NULL;
    IMcLineItem::Create(&pLineItem,IMcObjectSchemeItem::EISTF_SCREEN, IMcLineBasedItem::ELS_DASH_DOT, SMcBColor(255,0,0,255), 10);
    gridRegion[0].pGridLine = pLineItem;

    gridRegion[0].GeoLimit.MinVertex = SMcVector3D(0,0,0);
    gridRegion[0].GeoLimit.MaxVertex = SMcVector3D(0,0,0);
    IMcTextItem* pTextItem = NULL;
    IMcTextItem::Create(&pTextItem,IMcObjectSchemeItem::EISTF_SCREEN, NULL, SMcFVector2D(1, 8));
    pTextItem->SetTextColor(SMcBColor(0,0,255,255));
    gridRegion[0].pGridText = pTextItem;

    double basicStep = 2000.0;
    double currentStep = basicStep;

    IMcMapGrid::SScaleStep scaleStep[7];
    scaleStep[0].fMaxScale = 80000;
    scaleStep[0].eAngleValuesFormat = IMcMapGrid::EAF_DECIMAL_DEG;
    scaleStep[0].NextLineGap = SMcVector2D(currentStep,currentStep);
    scaleStep[0].uNumOfLinesBetweenDifferentTextX = 2;
    scaleStep[0].uNumOfLinesBetweenDifferentTextY = 2;
    scaleStep[0].uNumOfLinesBetweenSameTextX = 1;
    scaleStep[0].uNumOfLinesBetweenSameTextY = 1;
    scaleStep[0].uNumMetricDigitsToTruncate = 3;

    currentStep *= 2;



    scaleStep[1].fMaxScale = 160000;
    scaleStep[1].eAngleValuesFormat = IMcMapGrid::EAF_DEG_MIN_SEC;
    scaleStep[1].NextLineGap = SMcVector2D(currentStep,currentStep);
    scaleStep[1].uNumOfLinesBetweenDifferentTextX = 2;
    scaleStep[1].uNumOfLinesBetweenDifferentTextY = 2;
    scaleStep[1].uNumOfLinesBetweenSameTextX = 1;
    scaleStep[1].uNumOfLinesBetweenSameTextY = 1;
    scaleStep[1].uNumMetricDigitsToTruncate = 3;

    currentStep *= 2;

    scaleStep[2].fMaxScale = 320000;
    scaleStep[2].eAngleValuesFormat = IMcMapGrid::EAF_DEG_MIN_SEC;
    scaleStep[2].NextLineGap = SMcVector2D(currentStep,currentStep);
    scaleStep[2].uNumOfLinesBetweenDifferentTextX = 2;
    scaleStep[2].uNumOfLinesBetweenDifferentTextY = 2;
    scaleStep[2].uNumOfLinesBetweenSameTextX = 1;
    scaleStep[2].uNumOfLinesBetweenSameTextY = 1;
    scaleStep[2].uNumMetricDigitsToTruncate = 3;

    currentStep *= 2;

    scaleStep[3].fMaxScale = 640000;
    scaleStep[3].eAngleValuesFormat = IMcMapGrid::EAF_DEG_MIN_SEC;
    scaleStep[3].NextLineGap = SMcVector2D(currentStep,currentStep);
    scaleStep[3].uNumOfLinesBetweenDifferentTextX = 2;
    scaleStep[3].uNumOfLinesBetweenDifferentTextY = 2;
    scaleStep[3].uNumOfLinesBetweenSameTextX = 1;
    scaleStep[3].uNumOfLinesBetweenSameTextY = 1;
    scaleStep[3].uNumMetricDigitsToTruncate = 3;

    currentStep *= 2;

    scaleStep[4].fMaxScale = 1280000;
    scaleStep[4].eAngleValuesFormat = IMcMapGrid::EAF_DEG_MIN_SEC;
    scaleStep[4].NextLineGap = SMcVector2D(currentStep,currentStep);
    scaleStep[4].uNumOfLinesBetweenDifferentTextX = 2;
    scaleStep[4].uNumOfLinesBetweenDifferentTextY = 2;
    scaleStep[4].uNumOfLinesBetweenSameTextX = 1;
    scaleStep[4].uNumOfLinesBetweenSameTextY = 1;
    scaleStep[4].uNumMetricDigitsToTruncate = 3;

    currentStep *= 2;

    scaleStep[5].fMaxScale = 2560000;
    scaleStep[5].eAngleValuesFormat = IMcMapGrid::EAF_DEG_MIN_SEC;
    scaleStep[5].NextLineGap = SMcVector2D(currentStep,currentStep);
    scaleStep[5].uNumOfLinesBetweenDifferentTextX = 2;
    scaleStep[5].uNumOfLinesBetweenDifferentTextY = 2;
    scaleStep[5].uNumOfLinesBetweenSameTextX = 1;
    scaleStep[5].uNumOfLinesBetweenSameTextY = 1;
    scaleStep[5].uNumMetricDigitsToTruncate = 3;

    currentStep *= 2;

    scaleStep[6].fMaxScale = FLT_MAX;
    scaleStep[6].eAngleValuesFormat = IMcMapGrid::EAF_DEG_MIN_SEC;
    scaleStep[6].NextLineGap = SMcVector2D(currentStep,currentStep);
    scaleStep[6].uNumOfLinesBetweenDifferentTextX = 2;
    scaleStep[6].uNumOfLinesBetweenDifferentTextY = 2;
    scaleStep[6].uNumOfLinesBetweenSameTextX = 1;
    scaleStep[6].uNumOfLinesBetweenSameTextY = 1;
    scaleStep[6].uNumMetricDigitsToTruncate = 3;



    IMcMapGrid::Create(&pGrid,gridRegion,1,scaleStep,7);
    return pGrid;

}

void OpenGLScene::CreateViewport()
{
    IMcMapTerrain *apTerrains[2];
    IMcErrors::ECode res;
    IMcMapCamera::EMapType eMapType = IMcMapCamera::EMT_2D;

    IMcMapViewport::SCreateData createData(eMapType);

    createData.bFullScreen =false;
    createData.pGrid = NULL;
   // createData.pGrid = CreateGrid();
    createData.pImageCalc = NULL;
    createData.uWidth = width();
    createData.uHeight = height();
    createData.bShowGeoInMetricProportion = false;
    createData.fTerrainObjectsBestResolution = 0.125;
    createData.pCoordinateSystem = m_pGridCoordinateSystem;
    createData.pDevice = m_pMapDevice;
#ifdef _WIN32
    createData.hWnd =  (HWND)m_lWinId;
#else
    createData.hWnd = MC_HWND_CURRENT_GL_CONTEXT;
#endif
    createData.bExternalGLControl = true;
    createData.pOverlayManager = m_pOverlayManager;
    m_pOverlayManager->AddRef();
    IMcMapCamera *pCamera=NULL;
    int nNumTerrains = 0;
    if (m_pTerrain)
    {
        apTerrains[nNumTerrains] = m_pTerrain;
        nNumTerrains++;
    }
    if (m_pArbelTerrain)
    {
        apTerrains[nNumTerrains] = m_pArbelTerrain;
        nNumTerrains++;
    }
    res = IMcMapViewport::Create(&m_p2DViewport, &pCamera, createData,apTerrains, nNumTerrains);
    createData.eMapType = IMcMapCamera::EMT_3D;
 //   createData.pOverlayManager = NULL;
   res = IMcMapViewport::Create(&m_p3DViewport, &pCamera, createData,apTerrains, nNumTerrains);
   res = IMcMapViewport::Create(&m_pARViewport, &pCamera, createData,NULL, 0);

    m_p2DViewport->AddRef();
    m_p3DViewport->AddRef();
    m_pARViewport->AddRef();


///    m_p2DViewport->SetTerrainNumCacheTiles(m_pTerrain,false,15);

    if (m_eDisplayType == EDisplayType::EDT_2D)
    {
        m_pViewport = m_p2DViewport;
    }
    else if (m_eDisplayType == EDisplayType::EDT_3D)
    {
        m_pViewport = m_p3DViewport;
    }
    else if (m_eDisplayType == EDisplayType::EDT_AR)
    {
        m_pViewport = m_pARViewport;
    }

    m_pViewport->SetBackgroundColor(SMcBColor(255,0,0,255));
    CenterViewport();

    m_pViewport->SetCameraScale(3);

    /// Better Performance
    m_p3DViewport->SetObjectsDelay(IMcMapViewport::EODT_VIEWPORT_CHANGE_OBJECT_UPDATE,true,50);
    m_p3DViewport->SetObjectsDelay(IMcMapViewport::EODT_VIEWPORT_CHANGE_OBJECT_HEIGHT,true,50);
    m_p3DViewport->SetObjectsDelay(IMcMapViewport::EODT_VIEWPORT_CHANGE_OBJECT_SIZE,true,1);

}

void OpenGLScene::CenterViewport()
{   
    SMcBox BoundingBox;
    if (m_pNativeRasterLayer)
    {
        m_pNativeRasterLayer->GetBoundingBox(&BoundingBox);
    }
    else if (m_pRawRasterLayer)
    {
        m_pRawRasterLayer->GetBoundingBox(&BoundingBox);
    }
    else if (m_pNativeDtmLayer)
    {
        m_pNativeDtmLayer->GetBoundingBox(&BoundingBox);
    }
    else if (m_pRawDtmLayer)
    {
        m_pRawDtmLayer->GetBoundingBox(&BoundingBox);
    }

    SMcVector3D CameraPosition (BoundingBox.CenterPoint());
    CameraPosition.z = 100;
    qDebug() << "CameraPosition" << CameraPosition.x << " " << CameraPosition.y << " " << CameraPosition.z ;
    m_pViewport->SetCameraPosition(CameraPosition);
}

void OpenGLScene::DtmVisualization(bool bEnable)
{
    IMcMapViewport::SDtmVisualizationParams DtmVisualization;

    DtmVisualization.SetDefaultHeightColors(0, 300);

    DtmVisualization.bDtmVisualizationAboveRaster = true;

    DtmVisualization.uHeightColorsTransparency = 120;
    DtmVisualization.uShadingTransparency = 255;
    m_pViewport->SetDtmVisualization(bEnable, &DtmVisualization);


}

void OpenGLScene::DisplayObjects (bool Display)
{
    IMcConditionalSelector::EActionOptions eAction = IMcConditionalSelector::EAO_FORCE_FALSE;
    if (Display)
    {
        eAction = IMcConditionalSelector::EAO_FORCE_TRUE;
    }
    m_pMulObjsOverlay->SetVisibilityOption(eAction);
}

void OpenGLScene::CreateMapGrid()
{
    IMcMapGrid::SGridRegion gridRegion[1];
    IMcMapGrid* ppGrid = NULL;

    gridRegion[0].pCoordinateSystem = m_pGridCoordinateSystem;
    m_pGridCoordinateSystem->AddRef();

    IMcLineItem* pLineItem = NULL;
    IMcLineItem::Create(&pLineItem,IMcObjectSchemeItem::EISTF_SCREEN, IMcLineBasedItem::ELS_DASH_DOT, SMcBColor(255,0,0,255), 30);
    gridRegion[0].pGridLine = pLineItem;

    gridRegion[0].GeoLimit.MinVertex = SMcVector3D(0,0,0);
    gridRegion[0].GeoLimit.MaxVertex = SMcVector3D(0,0,0);

    IMcLogFont *pLogFont = NULL;
    LOGFONTA LogFont;
    LogFont.lfHeight = 40;

    IMcFont::SCharactersRange aCharactersRanges[1];
    aCharactersRanges[0].nFrom = 0;
    aCharactersRanges[0].nTo = 127;
    IMcLogFont::Create(&pLogFont, LogFont, true, aCharactersRanges, 1);

    IMcTextItem* pTextItem = NULL;
    IMcTextItem::Create(&pTextItem,IMcObjectSchemeItem::EISTF_SCREEN, pLogFont, SMcFVector2D(1, 40));
    pTextItem->SetTextColor(SMcBColor(255,0,0,255));
    gridRegion[0].pGridText = pTextItem;

    double basicStep = 2000.0;
    double currentStep = basicStep;

    IMcMapGrid::SScaleStep scaleStep[7];
    scaleStep[0].fMaxScale = 80000;
    scaleStep[0].eAngleValuesFormat = IMcMapGrid::EAF_DECIMAL_DEG;
    scaleStep[0].NextLineGap = SMcVector2D(currentStep,currentStep);
    scaleStep[0].uNumOfLinesBetweenDifferentTextX = 2;
    scaleStep[0].uNumOfLinesBetweenDifferentTextY = 2;
    scaleStep[0].uNumOfLinesBetweenSameTextX = 1;
    scaleStep[0].uNumOfLinesBetweenSameTextY = 1;
    scaleStep[0].uNumMetricDigitsToTruncate = 3;

    currentStep *= 2;



    scaleStep[1].fMaxScale = 160000;
    scaleStep[1].eAngleValuesFormat = IMcMapGrid::EAF_DEG_MIN_SEC;
    scaleStep[1].NextLineGap = SMcVector2D(currentStep,currentStep);
    scaleStep[1].uNumOfLinesBetweenDifferentTextX = 2;
    scaleStep[1].uNumOfLinesBetweenDifferentTextY = 2;
    scaleStep[1].uNumOfLinesBetweenSameTextX = 1;
    scaleStep[1].uNumOfLinesBetweenSameTextY = 1;
    scaleStep[1].uNumMetricDigitsToTruncate = 3;

    currentStep *= 2;

    scaleStep[2].fMaxScale = 320000;
    scaleStep[2].eAngleValuesFormat = IMcMapGrid::EAF_DEG_MIN_SEC;
    scaleStep[2].NextLineGap = SMcVector2D(currentStep,currentStep);
    scaleStep[2].uNumOfLinesBetweenDifferentTextX = 2;
    scaleStep[2].uNumOfLinesBetweenDifferentTextY = 2;
    scaleStep[2].uNumOfLinesBetweenSameTextX = 1;
    scaleStep[2].uNumOfLinesBetweenSameTextY = 1;
    scaleStep[2].uNumMetricDigitsToTruncate = 3;

    currentStep *= 2;

    scaleStep[3].fMaxScale = 640000;
    scaleStep[3].eAngleValuesFormat = IMcMapGrid::EAF_DEG_MIN_SEC;
    scaleStep[3].NextLineGap = SMcVector2D(currentStep,currentStep);
    scaleStep[3].uNumOfLinesBetweenDifferentTextX = 2;
    scaleStep[3].uNumOfLinesBetweenDifferentTextY = 2;
    scaleStep[3].uNumOfLinesBetweenSameTextX = 1;
    scaleStep[3].uNumOfLinesBetweenSameTextY = 1;
    scaleStep[3].uNumMetricDigitsToTruncate = 3;

    currentStep *= 2;

    scaleStep[4].fMaxScale = 1280000;
    scaleStep[4].eAngleValuesFormat = IMcMapGrid::EAF_DEG_MIN_SEC;
    scaleStep[4].NextLineGap = SMcVector2D(currentStep,currentStep);
    scaleStep[4].uNumOfLinesBetweenDifferentTextX = 2;
    scaleStep[4].uNumOfLinesBetweenDifferentTextY = 2;
    scaleStep[4].uNumOfLinesBetweenSameTextX = 1;
    scaleStep[4].uNumOfLinesBetweenSameTextY = 1;
    scaleStep[4].uNumMetricDigitsToTruncate = 3;

    currentStep *= 2;

    scaleStep[5].fMaxScale = 2560000;
    scaleStep[5].eAngleValuesFormat = IMcMapGrid::EAF_DEG_MIN_SEC;
    scaleStep[5].NextLineGap = SMcVector2D(currentStep,currentStep);
    scaleStep[5].uNumOfLinesBetweenDifferentTextX = 2;
    scaleStep[5].uNumOfLinesBetweenDifferentTextY = 2;
    scaleStep[5].uNumOfLinesBetweenSameTextX = 1;
    scaleStep[5].uNumOfLinesBetweenSameTextY = 1;
    scaleStep[5].uNumMetricDigitsToTruncate = 3;

    currentStep *= 2;

    scaleStep[6].fMaxScale = FLT_MAX;
    scaleStep[6].eAngleValuesFormat = IMcMapGrid::EAF_DEG_MIN_SEC;
    scaleStep[6].NextLineGap = SMcVector2D(currentStep,currentStep);
    scaleStep[6].uNumOfLinesBetweenDifferentTextX = 2;
    scaleStep[6].uNumOfLinesBetweenDifferentTextY = 2;
    scaleStep[6].uNumOfLinesBetweenSameTextX = 1;
    scaleStep[6].uNumOfLinesBetweenSameTextY = 1;
    scaleStep[6].uNumMetricDigitsToTruncate = 3;


    IMcMapGrid *pGrid;
    IMcMapGrid::Create(&pGrid,gridRegion,1,scaleStep,7);
    m_pViewport->SetGrid(pGrid);
}
/*
void OpenGLScene::CreateObject()
{
    QImage *pImage1 = new QImage ("assets:/CompassSmall.png");
    QImage *pImage2 = new QImage ("assets:/CompassSmall.png");//("assets:/Circle_North.png");
    QImage *pImage3 = new QImage ("assets:/Dot_North.png");
    QImage *pImage4 = new QImage ("assets:/Grid_North.png");
    QImage *pImage5 = new QImage ("assets:/True_North.png");


    IMcMemoryBufferTexture *pTexture1;
    IMcMemoryBufferTexture *pTexture2;
#ifdef USE_EXTRA_FILES
    IMcImageFileTexture *pTexture3;
    IMcImageFileTexture *pTexture4;
#endif
    IMcMemoryBufferTexture *pTexture5;
    SMcBColor TranspColor(65, 62, 60, 255);
    SMcBColor ColorToSubst(179, 178, 177, 255);
    SMcBColor SubstColor(0, 0, 255, 255);
    IMcErrors::ECode res =IMcMemoryBufferTexture::Create(  &pTexture1, pImage1->width() , pImage1->height() ,IMcTexture::EPF_A8R8G8B8 ,
                                   IMcTexture::EU_STATIC_WRITE_ONLY_IN_ATLAS,true,(BYTE *)pImage1->bits());
    res =IMcMemoryBufferTexture::Create(  &pTexture2, pImage2->width() , pImage2->height() ,IMcTexture::EPF_A8R8G8B8 ,
                                   IMcTexture::EU_STATIC_WRITE_ONLY,true,(BYTE *)pImage2->bits());
#ifdef USE_EXTRA_FILES
    res =IMcImageFileTexture::Create(  &pTexture3, "/sdcard/MapCore/CubeRes/CompassSmall.png", false, true, NULL, &ColorToSubst, &SubstColor);
    res =IMcImageFileTexture::Create(  &pTexture4, "/sdcard/MapCore/CubeRes/CompassSmall.png", true, false, &TranspColor);
#endif
    res =IMcMemoryBufferTexture::Create(  &pTexture5, pImage5->width() , pImage5->height() ,IMcTexture::EPF_A8R8G8B8 ,
                                   IMcTexture::EU_STATIC_WRITE_ONLY,true,(BYTE *)pImage5->bits());
    if (res != IMcErrors::SUCCESS)
    {
        printf("Failed to create IMcMemoryBufferTexture");
        printf ("%d",res);
        return ;
    }

    IMcObject *pObj = NULL;
    IMcPictureItem *pPic=NULL;
    SMcVector3D Center;
    m_pViewport->GetCameraPosition(&Center);

    Center.z = 0;
    SMcVector3D sCameraPos1 (Center);
    SMcVector3D sCameraPos2 (Center.x+1000,Center.y,0);

 //   QImage *pImage2 = new QImage ("assets:/CompassSmall.png");//("assets:/Circle_North.png");


    res =IMcMemoryBufferTexture::Create(  &m_pTexture, pImage2->width() , pImage2->height() ,IMcTexture::EPF_A8R8G8B8 ,
                                   IMcTexture::EU_STATIC_WRITE_ONLY,true,(BYTE *)pImage2->bits());

 //   CreatePolygon(SMcVector3D  (1438/2,2458/2,0));
///        CreatePolygon(SMcVector3D  (502,500,0));
///        CreateText(SMcVector3D(700,400,0));

    
//        CreateFPSText();
//        CreateTelemetryText();
        CreateWorldPicObject();
      
 //       CreatePolygon(SMcVector3D  (500,500,0));

 //   CreatePicture(SMcVector3D  (1438/2,2458/2,0));
 //   CreatePicture(SMcVector3D  (500,500,0));
//    CreateLine(SMcVector3D(300,300,0));
//    CreateText(SMcVector3D(400,400,0));

     ////

return;
#ifdef USE_EXTRA_FILES
    SMcVector3D sCameraPos3 (Center.x,Center.y+1000,0);
    IMcPictureItem::Create(&pPic,IMcObjectSchemeItem::EISTF_SCREEN,pTexture3);
    res = IMcObject::Create(&pObj,m_pOverlay,pPic,EPCS_WORLD,&sCameraPos3,1,false);
    SMcVector3D sCameraPos4 (Center.x+1000,Center.y+1000,0);
    IMcPictureItem::Create(&pPic,IMcObjectSchemeItem::EISTF_SCREEN,pTexture4);
    res = IMcObject::Create(&pObj,m_pOverlay,pPic,EPCS_WORLD,&sCameraPos4,1,false);
#endif
    SMcVector3D sCameraPos5 (Center.x-1000,Center.y,0);
    IMcPictureItem::Create(&pPic,IMcObjectSchemeItem::EISTF_SCREEN,pTexture5);
    res = IMcObject::Create(&pObj,m_pOverlay,pPic,EPCS_WORLD,&sCameraPos5,1,false);

    IMcTextItem *pText = NULL;


    IMcLogFont *pLogFont = NULL;
    LOGFONTA LogFont;
    LogFont.lfHeight = 40;



    IMcFont::SCharactersRange aCharactersRanges[1];
    aCharactersRanges[0].nFrom = 0;
    aCharactersRanges[0].nTo = 127;
    res = IMcLogFont::Create(&pLogFont, LogFont, true, aCharactersRanges, 1);

    // Create a text symbol

    IMcTextItem::Create(&pText,IMcObjectSchemeItem::EISTF_SCREEN,pLogFont);
    QString strText(tr("Test 123"));
   // strText.constData()
//    res = IMcObject::Create(&pObj,m_pOverlay,pText,EPCS_SCREEN,&sFPSTextPos,1,false);

     QString str = "Hello world";
     QChar *data = str.data();
  //   while (!data->isNull()) {
  //      qDebug() << data->unicode();
  //      ++data;
  //   }
     pText->SetText((char *)str.unicode());

    res = IMcObject::Create(&pObj,m_pOverlay,pText,EPCS_WORLD,&sCameraPos1,1,false);


}
*/

void OpenGLScene::CreateText(const SMcVector3D &ScreenPos)
{
    IMcObject *pObj = NULL;

    IMcTextItem *pText1 = NULL;


    IMcLogFont *pLogFont1 = NULL;
    LOGFONTA LogFont1;
    LogFont1.lfHeight = 60;
//    strcpy (LogFont1.lfFaceName,"/sdcard/MapCore/azeri.ttf");
//    strcpy (LogFont1.lfFaceName,"/sdcard/MapCore/DAVIDNEW.TTF");
//    strcpy (LogFont1.lfFaceName,"/sdcard/MapCore/times.ttf");
    strcpy (LogFont1.lfFaceName,"/system/fonts/Roboto-Regular.ttf");

    IMcFont::SCharactersRange aCharactersRanges[1];
    aCharactersRanges[0].nFrom = 73;// 0;
    aCharactersRanges[0].nTo = 500; // 127;
    IMcErrors::ECode res = IMcLogFont::Create(&pLogFont1, LogFont1, true, aCharactersRanges, 1);

    // Create a text symbol

    IMcTextItem::Create(&pText1,IMcObjectSchemeItem::EISTF_SCREEN,pLogFont1);

  //  QString str = "The quick brown fox jumps";


  //  pText1->SetText((char *)str.unicode());

    pText1->SetTextColor(SMcBColor(255,0,0,255));

//    pText1->SetText("צחי בדיקה");

        pText1->SetText(L"\u00c7\u00e7\u018f\u01dd\u011e\u011f\u0131\u0130\u00d6\u00f6\u015e\u015f\u00dc\u00fc");

//    pText1->SetText(L"Xəritədə göstər");
//    pText1->SetText(L"The quick brown fox jumps over the lazy dog");
//    pText1->SetText(L"צחי בדיקה");



    res = IMcObject::Create(&pObj,m_pOverlay,pText1,EPCS_SCREEN,&ScreenPos,1,false);


}

void OpenGLScene::RenderUpdates()
{
    unsigned int nFPS = m_FPS.getFPS();
    char strFPS [256];
    sprintf (strFPS , "FPS:%d",nFPS );
    if (m_pFPSText)
    {
        m_pFPSText->SetText(strFPS);
    }


    if ( m_pTelemetryText)
    {
        char StrTelemetryText[256];
        sprintf(StrTelemetryText,"Yaw:%5.0f    Pitch:%5.0f    Roll:%5.0f", m_fYaw,m_fPitch,m_fRoll);

        m_pTelemetryText->SetText(StrTelemetryText);
    }
    if (m_bAttachedOrientation && m_pViewport &&   (m_eDisplayType != EDisplayType::EDT_2D)  )
    {

        m_pViewport->SetCameraOrientation( m_fYaw ,m_fPitch,m_fRoll );
    }
}

void OpenGLScene::CreateWorldPicObject(const SMcVector3D &Loc)
{
    CMcDataArray<IMcObjectScheme*> pLoadedSchemes;

    LoadObjectSchemes("assets:/Schemes/ScreenPictureLegScheme.m",&pLoadedSchemes);

//    m_pOverlayManager->LoadObjectSchemes("assets:/Schemes/OnePicScmWorld.m",NULL,&pLoadedSchemes);

 //    SMcVector3D *pLocs = new SMcVector3D[1];
/*       SMcVector3D Pos = new SMcVector3D(34.8640356 * 100000 , 32.2872633 * 100000, 40);
     Locs[0] = Pos;
     IMcObject PicObject = mc.Interfaces.Overlay.IMcObject.Static.Create(m_pOverlay, LoadedSchemes[0], Locs);

     Locs[0] = new SMcVector3D(34.8640456 * 100000 , 32.2872733 * 100000, 40);
     mc.Interfaces.Overlay.IMcObject.Static.Create(m_pOverlay, LoadedSchemes[0], Locs);

     Locs[0] = new SMcVector3D(34.8640356 * 100000 , 32.2872733 * 100000, 40);
     mc.Interfaces.Overlay.IMcObject.Static.Create(m_pOverlay, LoadedSchemes[0], Locs);

     Locs[0] = new SMcVector3D(34.8640356 * 100000 , 32.2872633 * 100000, 40);
     mc.Interfaces.Overlay.IMcObject.Static.Create(m_pOverlay, LoadedSchemes[0], Locs);
*/
 //    SMcVector3D Loc (34.864092 * 100000 , 32.287991 * 100000, 40);
     IMcObject *pObject = NULL;
     IMcObject::Create(&pObject,m_pOverlay,pLoadedSchemes[0],&Loc,1);
 //    res = IMcObject::Create(&pObj,m_pOverlay,m_pFPSText,EPCS_SCREEN,&ScreenPos,1,false);

     //    PicObject.SetTextureProperty(1, m_PreviewTexture);


     //      Render();
 }

void OpenGLScene::CreateFPSText()
{
    IMcObject *pObj = NULL;
    SMcVector3D ScreenPos (100,100,0);


    IMcLogFont *pLogFont1 = NULL;
    LOGFONTA LogFont1;
    LogFont1.lfHeight = 50;


    IMcFont::SCharactersRange aCharactersRanges[1];
    aCharactersRanges[0].nFrom = 0;
    aCharactersRanges[0].nTo = 127;
    IMcErrors::ECode res = IMcLogFont::Create(&pLogFont1, LogFont1, true, aCharactersRanges, 1);

    // Create a text symbol

    IMcTextItem::Create(&m_pFPSText,IMcObjectSchemeItem::EISTF_SCREEN,pLogFont1);
    m_pFPSText->SetText("Tsahi 123");
    m_pFPSText->SetTextColor(SMcBColor(255,0,0,255));
    m_pFPSText->SetBackgroundColor(SMcBColor(0,0,0,255));

    res = IMcObject::Create(&pObj,m_pOverlay,m_pFPSText,EPCS_SCREEN,&ScreenPos,1,false);
    if (res != IMcErrors::SUCCESS)
    {
             return ;
    }


}

void OpenGLScene::CreateTelemetryText()
{
    IMcObject *pObj = NULL;
    SMcVector3D ScreenPos (700,100,0);


    IMcLogFont *pLogFont1 = NULL;
    LOGFONTA LogFont1;
    LogFont1.lfHeight = 60;


    IMcFont::SCharactersRange aCharactersRanges[1];
    aCharactersRanges[0].nFrom = 0;
    aCharactersRanges[0].nTo = 127;
    IMcErrors::ECode res = IMcLogFont::Create(&pLogFont1, LogFont1, true, aCharactersRanges, 1);

    // Create a text symbol

    IMcTextItem::Create(&m_pTelemetryText,IMcObjectSchemeItem::EISTF_SCREEN,pLogFont1);
    m_pTelemetryText->SetText("");
    m_pTelemetryText->SetTextColor(SMcBColor(255,0,0,255));
    m_pTelemetryText->SetBackgroundColor(SMcBColor(0,0,0,255));

    res = IMcObject::Create(&pObj,m_pOverlay,m_pTelemetryText,EPCS_SCREEN,&ScreenPos,1,false);
    if (res != IMcErrors::SUCCESS)
    {
             return ;
    }

  //  pObj->Remove();
  //  res = IMcObject::Create(&pObj,m_pOverlay,m_pTelemetryText,EPCS_SCREEN,&ScreenPos,1,false);
 //   if (res != IMcErrors::SUCCESS)
  //  {
 //            return ;
 //   }

}
void OpenGLScene::CreateLine(const SMcVector3D &ScreenPos)
{

    IMcLineItem *pLine = NULL;
    IMcErrors::ECode res = IMcLineItem::Create(&pLine,IMcObjectSchemeItem::EISTF_SCREEN,IMcLineBasedItem::ELS_SOLID,SMcBColor(255,0,255,255),10.0);

    if (res != IMcErrors::SUCCESS)
    {
             return ;
    }

  SMcVector3D sVector[3];
  sVector[0] = ScreenPos;
  sVector[1].x = sVector[0].x + 200;
  sVector[1].y = sVector[0].y + 100;
  sVector[1].z = 0;
  sVector[2].x = sVector[1].x ;
  sVector[2].y = sVector[1].y + 300;
  sVector[2].z = 0;

  res = IMcObject::Create(&pObj,m_pOverlay,pLine,EPCS_SCREEN,sVector,3,false);
  if (res != IMcErrors::SUCCESS)
  {
           return ;
  }

}

void OpenGLScene::CreatePolygon(const SMcVector3D &Pos,UINT eType)
{

    IMcErrors::ECode res = IMcPolygonItem::Create(&pPoly,eType,IMcLineBasedItem::ELS_SOLID,SMcBColor(255,0,0,128),10.0,0,SMcFVector2D(0.0, -1.0),1.0,IMcClosedShapeItem::EFS_SOLID,SMcBColor(0,0,255,128));

   if (res != IMcErrors::SUCCESS)
   {
            return ;
   }

  SMcVector3D sVector[3];
  sVector[0] = Pos;
  sVector[0].z = 50;
  sVector[1].x = sVector[0].x + 200;
  sVector[1].y = sVector[0].y + 100;
  sVector[1].z = 50;
  sVector[2].x = sVector[1].x ;
  sVector[2].y = sVector[1].y + 300;
  sVector[2].z = 50;

  res = IMcObject::Create(&m_pPolygon,m_pOverlay,pPoly,EPCS_WORLD,sVector,3,true);
  if (res != IMcErrors::SUCCESS)
  {
           return ;
  }


}

void OpenGLScene::CreatePicture(const SMcVector3D &ScreenPos)
{
//    IMcMemoryBufferTexture *pTexture2;
    IMcPictureItem *pPic=NULL;
    IMcPictureItem *pPicEm=NULL;

//    IMcErrors::ECode res =IMcMemoryBufferTexture::Create(  &pTexture2, pImage2->width() , pImage2->height() ,IMcTexture::EPF_A8R8G8B8 ,
//                                   IMcTexture::EU_STATIC_WRITE_ONLY,true,(BYTE *)pImage2->bits());


    IMcPictureItem::Create(&pPic,IMcObjectSchemeItem::EISTF_SCREEN,m_pTexture);

    pPic->SetCoordinateSystemConversion(EMcPointCoordSystem::EPCS_SCREEN);
//   res = IMcObject::Create(&pObj,m_pOverlay,pPic,EPCS_WORLD,&sCameraPos2,1,false);
     IMcErrors::ECode res = IMcObject::Create(&pObj,m_pOverlay,pPic,EPCS_WORLD,&ScreenPos,1,false);
     ////

 //    m_pEditMode->ExitCurrentAction(false);
 //   res  = m_pEditMode->StartEditObject(pObj, pPic);
 //    if (res != IMcErrors::SUCCESS)
 //    {
 //             return ;
 //    }

}

static Model *loadModel(const QString &filePath)
{
}

void OpenGLScene::loadModel()
{
}

void OpenGLScene::loadModel(const QString &filePath)
{
}

void OpenGLScene::modelLoaded()
{

}

void OpenGLScene::setModel(Model *model)
{

}

void OpenGLScene::enableWireframe(bool enabled)
{
}

void OpenGLScene::enableNormals(bool enabled)
{
}

void OpenGLScene::setModelColor()
{
}
void OpenGLScene::ExitEditModeCurrentAction()
{
    m_pEditMode->ExitCurrentAction(true);
}

void OpenGLScene::setBackgroundColor()
{
}
void OpenGLScene::UpdateEditModeOnMouseEvent( QPointF &Point1 ,QPointF &Point2 ,IMcEditMode::EMouseEvent eEventType)
{
    SMcPoint MousePosition;
    MousePosition.x = Point1.x();
    MousePosition.y = Point1.y();

    SMcPoint SecondPosition;
    SecondPosition.x = Point2.x();
    SecondPosition.y = Point2.y();

    SMcPoint *pSecondPosition = NULL;
    if (SecondPosition.x != -1)
    {
        pSecondPosition = &SecondPosition;
    }
    bool bRenderIsNeeded = false;
    bool bCtrlDown = false;

    IMcEditMode::ECursorType eCursor = IMcEditMode::ECT_DEFAULT_CURSOR;
    m_pEditMode->OnMouseEvent(eEventType,MousePosition,bCtrlDown,0,&bRenderIsNeeded,&eCursor,pSecondPosition);
}


void OpenGLScene::ChangeViewport(EDisplayType eDisplayType)
{
    m_pEditMode->ExitCurrentAction(true);

    m_eDisplayType = eDisplayType;
    SMcVector3D CameraPosition;
    m_pViewport->GetCameraPosition(&CameraPosition);
    double dHeight;
    bool pbHeightFound;
    IMcErrors::ECode res = m_pViewport->GetTerrainHeight(CameraPosition,&pbHeightFound, &dHeight);
    CameraPosition.z = dHeight+100;
    if (eDisplayType == EDisplayType::EDT_2D)
    {
        m_pViewport= m_p2DViewport;
        m_pEditMode = m_p2DEditMode;

    }
    else if (eDisplayType == EDisplayType::EDT_3D)
    {
        m_pViewport= m_p3DViewport;
        m_pEditMode = m_p3DEditMode;
     }
    else  if (eDisplayType == EDisplayType::EDT_AR)
    {
        m_pViewport= m_pARViewport;
        m_pEditMode = m_pAREditMode;
    }
    m_pViewport->SetCameraPosition(CameraPosition);
    m_pEditMode->StartNavigateMap(false);
 }

void OpenGLScene::Set3DPosition()
{
//    SMcVector3D CameraPosition (693795 , 3612763 , 0);

    SMcVector3D CameraPosition (675512 , 3573849 , 0);
//    SMcVector3D CameraPosition;
//    m_p2DViewport->GetCameraPosition(&CameraPosition);
    CameraPosition.z = 100;
    m_pViewport->SetCameraPosition(CameraPosition);
    CameraPosition.z = 0;
    m_pViewport->SetCameraLookAtPoint(CameraPosition);
}

void OpenGLScene::ReleaseExistingTerrain()
{
    if (m_pNativeRasterLayer)
    {
        m_pNativeRasterLayer->Release();
        m_pNativeRasterLayer = NULL;
    }
    if (m_pNativeDtmLayer)
    {
        m_pNativeDtmLayer->Release();
        m_pNativeDtmLayer = NULL;
    }
    if (m_pRawDtmLayer)
    {
        m_pRawDtmLayer->Release();
        m_pRawDtmLayer = NULL;
    }
    if (m_pRawRasterLayer)
    {
        m_pRawRasterLayer->Release();
        m_pRawRasterLayer = NULL;
    }
    if (m_pTerrain)
    {
        m_pTerrain->Release();
        m_pTerrain = NULL;
    }
}

void OpenGLScene::BuildMapTerrain()
{
    qDebug() << "BuildMapTerrain" << "\n" ;

    IMcErrors::ECode res = IMcErrors::SUCCESS;

    if (m_p2DViewport)
    {
        res = m_p2DViewport->RemoveTerrain(m_pTerrain);
    }
    if (m_p3DViewport)
    {
        res = m_p3DViewport->RemoveTerrain(m_pTerrain);
    }

    ReleaseExistingTerrain();
    CreateGridCoordinates();
    CreateRawDtmLayer();
    CreateNativeDtmLayer();
    CreateRawRasterLayer();
    CreateNativeRasterLayer();
    CreateRawVectorLayer();
//    CreateBuildingSolLayer();
//    CreateAccoSolLayer();
//    CreateIvorySolLayer();
//    CreateElyakimSolLayer();
    CreateMapTerrain();
    if (m_p2DViewport)
    {
        res = m_p2DViewport->AddTerrain(m_pTerrain);
    }
    if (m_p3DViewport)
    {
        res = m_p3DViewport->AddTerrain(m_pTerrain);
    }
    if (m_pViewport)
    {
        CenterViewport();
    }
}

void OpenGLScene::GetCenterPosition(SMcVector3D &Center)
{
    if ( m_eDisplayType == EDisplayType::EDT_2D)
    {
        m_pViewport->GetCameraPosition(&Center);
    }
    else
    {
        UINT uWidth, uHeight;
        m_pViewport->GetViewportSize(&uWidth,&uHeight);
        const SMcVector3D ScreenPoint(uWidth/2,uHeight/2,0.0);
        bool bIntersect = false;
        m_pViewport->ScreenToWorldOnTerrain(ScreenPoint,&Center,&bIntersect);

    }
}


void OpenGLScene::DisplayPolygon(bool bDisplay)
{
    if (bDisplay)
    {
        SMcVector3D Center;
        GetCenterPosition(Center);
        CreatePolygon(Center,IMcObjectSchemeItem::EISTF_WORLD /*| IMcObjectSchemeItem::EISTF_ATTACHED_TO_TERRAIN*/);
    }
    else
    {
        m_pPolygon->Remove();
    }
}

void OpenGLScene::UpdateObjectsPos()
{
    SMcVector3D aLocationPoints[1];
    SMcVector3D Center;
    m_pViewport->GetCameraPosition(&Center);
    int nRange = 10000;
    srand (time(NULL));
    for (int i=0 ; i<100 ; i++)
    {
        qint64 lBeginTime = QDateTime::currentMSecsSinceEpoch();
        for (int idx=0 ; idx<2000 ; idx++)
        {
            aLocationPoints[0].x = fRand(Center.x - nRange/2 , Center.x + nRange/2);
            aLocationPoints[0].y = fRand(Center.y - nRange/2 , Center.y + nRange/2) ;
            aLocationPoints[0].z = 0;

            aObjects[idx]->UpdateLocationPoints(aLocationPoints,1);

        }
        qDebug() << "Time elapsed" << QDateTime::currentMSecsSinceEpoch() - lBeginTime << "\n" ;
    }
}

void OpenGLScene::AddObjects()
{

      QString schemePath = "assets:/Schemes/ScreenPictureLegScheme.m";



    CMcDataArray<IMcObjectScheme*> Schemes;
    IMcObject* pObj = NULL;
    SMcVector3D Center;
    m_pViewport->GetCameraPosition(&Center);
    int nRange = 10000;
    srand (time(NULL));

    SMcVector3D aLocationPoints[1];
    IMcErrors::ECode nErr = LoadObjectSchemes(schemePath,&Schemes);
//    IMcErrors::ECode nErr = m_pOverlayManager->LoadObjectSchemes(schemePath.toLatin1().constData(), NULL, &Schemes);
    if (nErr != IMcErrors::SUCCESS)
    {
        qDebug("CreateSchemaObjs - Schema not created... nErr = %d ", nErr);
        return;
    }

    for (int i=0 ; i<1000 ; i++) {
             aLocationPoints[0].x = fRand(Center.x - nRange/2 , Center.x + nRange/2);
            aLocationPoints[0].y = fRand(Center.y - nRange/2 , Center.y + nRange/2) ;
            aLocationPoints[0].z = 0;

            IMcObject::Create(&pObj, m_pMulObjsOverlay, Schemes[0]);
            pObj->SetLocationPoints(aLocationPoints,1);
            pObj->SetState((byte)(i%7));

    }

/*
    for (int i=0 ; i<1000 ; i++) {
             aLocationPoints[0].x = fRand(Center.x - nRange/2 , Center.x + nRange/2);
            aLocationPoints[0].y = fRand(Center.y - nRange/2 , Center.y + nRange/2) ;
            aLocationPoints[0].z = 0;

            IMcObject::Create(&pObj, m_pMulObjsOverlay, Schemes[0]);
            pObj->SetTextureProperty(1,pTexture1);
            pObj->SetLocationPoints(aLocationPoints,1);
      //      pObj->SetState((byte)(i%7));

    }
*/
}


void OpenGLScene::DisplaySightPresentation(bool bDisplay)
{
    if (!bDisplay)
    {
        m_pEditMode->ExitCurrentAction(true);
        m_pSightPresentation->Remove();
        m_pSightPresentation = NULL;
        return;
    }

    CMcDataArray<IMcObjectScheme*> Schemes;
    LoadObjectSchemes( "assets:/Schemes/SightPresentation.m",&Schemes);
    SMcVector3D Center;
    GetCenterPosition(Center);
    IMcObject::Create(&m_pSightPresentation, m_pOverlay, Schemes[0]);
    m_pSightPresentation->SetLocationPoints(&Center,1);
    CMcDataArray<IMcObjectSchemeNode*> apNodes;
    Schemes[0]->GetNodes(&apNodes,IMcObjectSchemeNode::ENKF_SYMBOLIC_ITEM);
    IMcObjectSchemeItem *pSchemeItem = apNodes[0]->CastToObjectSchemeItem();
    IMcErrors::ECode res  = m_pEditMode->StartEditObject(m_pSightPresentation, pSchemeItem);
    if (res != IMcErrors::SUCCESS)
    {
              return ;
    }

}
IMcErrors::ECode OpenGLScene::LoadObjectSchemes(QString strAssetPath,CMcDataArray<IMcObjectScheme*> *papLoadedSchemes)
{
    QFile File(strAssetPath);
    if (!File.open(QIODevice::ReadOnly))
    {
        return IMcErrors::CANT_READ_FILE;
    }
    QByteArray qbyBuffer = File.readAll();
    int nBufferSize = qbyBuffer.size();
    BYTE *byBuffer = (BYTE *)qbyBuffer.data();
    IMcErrors::ECode nErr = m_pOverlayManager->LoadObjectSchemes(byBuffer,nBufferSize, NULL, papLoadedSchemes);
    return nErr;
}

double OpenGLScene::fRand(double fMin, double fMax)
{
    double f = (double)rand() / RAND_MAX;
    return fMin + f * (fMax - fMin);
}
void OpenGLScene::AddBlueForce()
{
#ifdef WIN32
        QString schemePath = "./Schemes/Symbol.m";
#else
        //QString schemePath = "assets:/Symbol.m";
        QString schemePath = "assets:/Schemes/Symbol.m";
#endif

    SMcVector3D Center;
    m_pViewport->GetCameraPosition(&Center);
    SMcVector3D aLocationPoints[2];
    aLocationPoints[0].x =	Center.x;
    aLocationPoints[0].y =	Center.y;
    aLocationPoints[0].z =	Center.z;

    CreateSchemaObject(schemePath, SYMBOL_SCHEME_ID, aLocationPoints, 1);
}

void OpenGLScene::WindowResize(uint w , uint h)
{
    if ( m_nResizeCounter == 0)
    {
        m_nResizeCounter++;
        return ;
    }

    m_nWidth = w;
    m_nHeight = h;

    if (m_pViewport)
    {
#ifdef _WIN32
          m_pViewport->ViewportResized();
#else
        m_pViewport->ViewportResized(w , h);
#endif
    }
    else
    {
        CreateMapDevice();
        BuildMapTerrain();
        CreateArbelTerrain();
        CreateOverlayManager();
        CreateOverlays();
        m_eDisplayType = EDisplayType::EDT_2D;
        CreateViewport();

        if (m_pViewport)
        {
            CreateEditMode();
//CreateObject();
          //  CreateFPSText();
            CreateTelemetryText();
            AddObjects();
            m_pMulObjsOverlay->SetVisibilityOption(IMcConditionalSelector::EAO_FORCE_FALSE);
        }
        QPlatformNativeInterface *interface = QApplication::platformNativeInterface();
        jobject activity = (jobject)interface->nativeResourceForIntegration("QtActivity");
        if (UTIL_CAMERA_ACTIVE)
        {
            QAndroidJniObject::callStaticMethod<void>("com/elbit/mapcore/utils/IMcGLDeviceCamera",
                                               "Create",
                                               "(Landroid/content/Context;)V",
                                               activity);
        }
        if (UTIL_TELEMETRY_ACTIVE)
        {
            QAndroidJniObject::callStaticMethod<void>("com/elbit/mapcore/utils/IMcTelemetryUpdates",
                                               "Create",
                                               "(Landroid/content/Context;)V",
                                               activity);
        }
        if (UTIL_LOCATION_ACTIVE)
        {
            QAndroidJniObject::callStaticMethod<void>("com/elbit/mapcore/utils/IMcLocationUpdates",
                                               "Create",
                                               "(Landroid/content/Context;)V",
                                               activity);
        }

        Java_org_qtproject_example_cube_IMcNativeCubeActivityListener_OnResume ();

    }

}

void OpenGLScene::TestDraw()
{
    ////////////////

 /*       SMcVector3D Center1;

        if (m_pViewport)
        {
            m_pViewport->GetCameraPosition(&Center1);
        }*/

    ////////////////

///    BadTransparency();
///      AddBlueForce();

///    AddObjects();
///       AddSightPresentation();
return;
        IMcErrors::ECode res = IMcErrors::SUCCESS;
        SMcVector3D Center;
        IMcPictureItem *pPicEm=NULL;

        if (m_pViewport)
        {
            m_pViewport->GetCameraPosition(&Center);
        }
//         CreatePicture(Center);
        IMcLineItem *pLine1 = NULL;
        res = IMcLineItem::Create(&pLine1,IMcObjectSchemeItem::EISTF_SCREEN,IMcLineBasedItem::ELS_SOLID,SMcBColor(255,0,255,255),10.0);

       if (res != IMcErrors::SUCCESS)
       {
                return ;
       }
/*
      SMcVector3D sVector1[3];
      sVector1[0] = Center;
      sVector1[1].x = sVector1[0].x + 2000;
      sVector1[1].y = sVector1[0].y + 1000;
      sVector1[1].z = 0;
      sVector1[2].x = sVector1[1].x ;
      sVector1[2].y = sVector1[1].y + 3000;
      sVector1[2].z = 0;

      res = IMcObject::Create(&pObj,m_pOverlay,pLine1,EPCS_WORLD,sVector1,3,false);
      if (res != IMcErrors::SUCCESS)
      {
               return ;
      }

      m_pEditMode->ExitCurrentAction(false);
      res = m_pEditMode->SetUtilityPicture(pPicEm,IMcEditMode::EUPT_MID_EDGE_REGULAR);
     res  = m_pEditMode->StartEditObject(pObj, pLine1);
      if (res != IMcErrors::SUCCESS)
      {
               return ;
      }
*/


/*        IMcLineItem *pLine1 = NULL;
        res = IMcLineItem::Create(&pLine1,IMcObjectSchemeItem::EISTF_SCREEN,IMcLineBasedItem::ELS_SOLID,SMcBColor(255,0,255,255),10.0);

       if (res != IMcErrors::SUCCESS)
       {
                return ;
       }

      SMcVector3D sVector1[3];
      sVector1[0] = Center;
      sVector1[1].x = sVector1[0].x + 2000;
      sVector1[1].y = sVector1[0].y + 1000;
      sVector1[1].z = 0;
      sVector1[2].x = sVector1[1].x ;
      sVector1[2].y = sVector1[1].y + 3000;
      sVector1[2].z = 0;

      res = IMcObject::Create(&pObj,m_pOverlay,pLine1,EPCS_WORLD,sVector1,3,false);
      if (res != IMcErrors::SUCCESS)
      {
               return ;
      }
*/

         QString schemePath = "assets:/Schemes/PositionPerpendicularToArrow.m";



        SMcVector3D aLocationPoints[2];
        aLocationPoints[0].x =	Center.x;
        aLocationPoints[0].y =	Center.y;
        aLocationPoints[0].z =	Center.z;

        aLocationPoints[1].x =	Center.x-1000;
        aLocationPoints[1].y =	Center.y-1000;
        aLocationPoints[1].z =	Center.z;
        CreateSchemaObject(schemePath, GOOD_SCHEME_ID, aLocationPoints, 2);

/*        CMcDataArray<IMcObject*> LoadedObjects;
        m_pOverlay->LoadObjects("/sdcard/MapCore/ambush_texture_8_normal.bin",NULL,&LoadedObjects);
        LoadedObjects[0]->SetFloatProperty(21101,19.36);
        SMcVector3D Pos (4744721.0, 3955374,0);*/
 //       m_pOverlay->LoadObjects("/sdcard/MapCore/ambush_texture_8_thick.bin");
 //       SMcVector3D Pos (2611225.0, 4443097,0);

  //      m_pOverlay->LoadObjects("/sdcard/MapCore/ambush_texture_8_thin.bin");
  //      SMcVector3D Pos (2610928.0, 4444138,0);

   //     m_pViewport->SetCameraPosition(Pos);
  //      m_pViewport->SetCameraScale(5);

}




void OpenGLScene::CreateSchemaObject(QString schemePath, int schemeID, SMcVector3D* locationPoints, int pointsCount, QString* pIconStr, QString *pStrText)
{
    CMcDataArray<IMcObjectScheme*> schemes;
    IMcObjectScheme* m_Scheme = NULL;
    IMcErrors::ECode nErr;

    IMcObject* pObj = NULL;

    qDebug("CreateSchemaObjs - Try to to get schema  %d  from cache...  ", schemeID);
    m_pOverlayManager->GetObjectSchemeByID(schemeID,&m_Scheme);

    qDebug("CreateSchemaObjs - Schema %d not found in cache...  ", schemeID);
    qDebug("\nCurrent dir: %s", QFileInfo(".").absolutePath().toLatin1().constData());
    qDebug("\nCurrent dir: %s", getenv("PWD"));
    if(m_Scheme == NULL)
    {
        nErr = LoadObjectSchemes(schemePath,&schemes);
        qDebug("CreateSchemaObjs - Load schema  from file %s ...  ", schemePath.toLatin1().constData());
 //       nErr = m_pOverlayManager->LoadObjectSchemes(schemePath.toLatin1().constData(), NULL, &schemes);
        if (nErr == IMcErrors::SUCCESS)
        {
            m_Scheme = schemes[0];
            m_Scheme->AddRef();
            m_Scheme->SetID(schemeID);
            AlterSchemeFonts(m_Scheme);
        }
        else
        {
            qDebug("CreateSchemaObjs - Schema not created... nErr = %d ", nErr);
            return;
        }
    }

    SMcBColor lineColor = SMcBColor(0, 0, 255, 255);
    float lineWidth = 6.0;
    byte lineStyle  = 0;

    qDebug("CreateSchemaObjs - Update schema defaults ...  ");
    m_Scheme->SetBytePropertyDefault(PROPID_LINE_STYLE, lineStyle);
    m_Scheme->SetFloatPropertyDefault(PROPID_LINE_WIDTH, lineWidth);
    m_Scheme->SetColorPropertyDefault(PROPID_LINE_COLOR, lineColor);

//    m_Scheme->SetBytePropertyDefault(PROPID_ADDIT_LINE_STYLE, lineStyle);
//    m_Scheme->SetFloatPropertyDefault(PROPID_ADDIT_LINE_WIDTH, lineWidth);
//    m_Scheme->SetColorPropertyDefault(PROPID_ADDIT_LINE_COLOR, lineColor);

    if(pIconStr)
    {
        QString iconPath = *pIconStr;

        IMcMemoryBufferTexture* iconMemoryTexture = GetIconTexture(iconPath, false, QColor(255, 255, 255));

        if(iconMemoryTexture)
        {
            m_Scheme->SetTexturePropertyDefault(PROPID_ICON, iconMemoryTexture);
        }
    }

    if(pStrText !=NULL)
    {
        QString texts = *pStrText;
        m_Scheme->SetStringPropertyDefault(PROPID_TEXT, texts.toLatin1().constData());

        SMcBColor tmpTextBackColor = SMcBColor(255, 255, 255, 255);
        m_Scheme->SetColorPropertyDefault(PROPID_TEXT_BACK_COLOR, tmpTextBackColor);

    }

    qDebug("CreateSchemaObjs - Create map object pObj = %d m_pOverlay = %d m_Scheme = %d ...  ",(int)pObj, (int)m_pOverlay, (int)m_Scheme);
    nErr = IMcObject::Create(&pObj, m_pOverlay, m_Scheme);

    qDebug("CreateSchemaObjs - Set map object properties ...  ");
    //nErr = pObj->SetFloatProperty(PROPID_ROTATION, 5.0);
    nErr = pObj->SetColorProperty(PROPID_LINE_COLOR, lineColor);

if(schemeID == SYMBOL_SCHEME_ID)
{

    //Get Icons from entity and merge them
    QList<QString> iconsNames;
    //EErrorCode errEnt = pEntity->GetGraphics(iconsNames);
    iconsNames << "/sdcard/TorchD/Bin/Icons/Frame/FRIEND.EQT.present.black";
    iconsNames << "/sdcard/TorchD/Bin/Icons/Fill/WAR.GRDTRK.EQT.FRIEND";
    iconsNames << "/sdcard/TorchD/Bin/Icons/Track/WAR.GRDTRK.EQT.GRDVEH.ARMD";
    QString imgType;
    imgType =  ".bmp"; //pEntity->GetImagesSuffix(imgType);


    //Select Icon size:

    QList<QString> iconFullNamesA,iconFullNamesB,iconFullNamesC;
    foreach(QString str , iconsNames)
    {
        iconFullNamesA.push_back(str + "_A" + imgType);
        iconFullNamesB.push_back(str + "_B" + imgType);
        iconFullNamesC.push_back(str + "_C" + imgType);
    }


    //ENT_2525Entity* ent2525 = NULL;
    static bool faded = true;
    faded = !faded;

  //Create combined bitmap
    IMcMemoryBufferTexture* iconMemoryTexture = GetCombinedIcon(iconFullNamesA,faded);
    //Set main texture property
    nErr = pObj->SetTextureProperty(A_ICON_A + PICTURE_TEXTURE_ID_OFFSET,iconMemoryTexture);
    IMcMemoryBufferTexture* iconMemoryTextureB = GetCombinedIcon(iconFullNamesB,faded);
    if (iconMemoryTextureB)
        nErr =pObj->SetTextureProperty(A_ICON_B + PICTURE_TEXTURE_ID_OFFSET,iconMemoryTextureB);
    else
         nErr = pObj->SetTextureProperty(A_ICON_B + PICTURE_TEXTURE_ID_OFFSET,iconMemoryTexture);

    UINT w, h,w1,h1;
    nErr = iconMemoryTexture->GetSize(&w,&h);
    nErr = iconMemoryTextureB->GetSize(&w1,&h1);
    QString id_texture = QString("A(%1x%2) + B(%3x%4)").arg(QString::number(w),QString::number(h),QString::number(w1),QString::number(h1));
    nErr = AddStringCmd(pObj,id_texture,T_TEXT_UNIQUE_DESIGNATION_A+TEXT_TEXT_ID_OFFSET);

    nErr = iconMemoryTexture->GetSourceSize(&w,&h);
    nErr = iconMemoryTextureB->GetSourceSize(&w1,&h1);
    QString id_source = QString("A(%1x%2) + B(%3x%4)").arg(QString::number(w),QString::number(h),QString::number(w1),QString::number(h1));
    nErr = AddStringCmd(pObj,id_source,XY_TEXT_A+TEXT_TEXT_ID_OFFSET);

}


    qDebug("CreateSchemaObjs - Set map object location ...  ");
    pObj->SetLocationPoints(locationPoints,pointsCount);

}


QImage* OpenGLScene::GetIcon(QString iconPath, QColor transparentColor)
{
    QImage* icon = NULL;

    //Return empty bitmap if list is not valid (empty)
    if(iconPath == "") return NULL;

    QString iconName = iconPath;

    QImage tempImg(iconName);
    if(tempImg.isNull())
    {
        return NULL;
    }
    if(!(tempImg.hasAlphaChannel()))
    {
        int tmpWidth = tempImg.width();
        if(tmpWidth == 0) return NULL;

        if(tmpWidth % 2)
        {
            QPixmap dashboard(tempImg.width() + 1,tempImg.height() + 1);
            dashboard.fill();

            QPainter p;
            p.begin(&dashboard);
            p.drawImage(0,0,tempImg);
            p.end();
            icon = new QImage(dashboard.toImage());
        }
        else
            icon = new QImage(tempImg);

        QImage mask = icon->createMaskFromColor(transparentColor.rgb(),Qt::MaskOutColor);
        icon->setAlphaChannel(mask);

    }
    else
    {
        icon = new QImage(tempImg);
    }

    QImage* scaledIcon = new QImage(icon->scaled(256, 256, Qt::KeepAspectRatioByExpanding, Qt::SmoothTransformation));
    return scaledIcon;
}


IMcMemoryBufferTexture* OpenGLScene::GetIconTexture(QString iconName, bool isFaded, QColor transparentColor)
{
    QString textureKey = QString::number(isFaded) + "|" + iconName;

    QImage *pOrigImage = GetIcon(iconName, transparentColor);
    QImage *pCopyImage = NULL;
    if (pOrigImage)
    {
        pCopyImage = new QImage(*pOrigImage);
    }
    return GetTextureFromIcon(pCopyImage);
}

IMcMemoryBufferTexture* OpenGLScene::GetTextureFromIcon(QImage* icon)
{

    IMcMemoryBufferTexture* iconTexture = NULL;

    if(icon != NULL)
    {
        IMcErrors::ECode result = IMcMemoryBufferTexture::Create(   &iconTexture,
                                                                    icon->width(),
                                                                    icon->height(),
                                                                    IMcTexture::EPF_A8R8G8B8,
                                                                    IMcTexture::EU_STATIC_WRITE_ONLY,
                                                                    //IMcTexture::EU_STATIC,
                                                                    //IMcTexture::EU_STATIC_WRITE_ONLY_IN_ATLAS,
// IMcTexture::EU_DYNAMIC,
                                                                    //IMcTexture::EU_DYNAMIC_WRITE_ONLY,
                                                                    //IMcTexture::EU_DYNAMIC_WRITE_ONLY_DISCARDABLE,

                                                                   // IMcTexture::EU_STATIC_WRITE_ONLY,
                                                                    true,
                                                                    (BYTE*)icon->bits());

        if (result == IMcErrors::SUCCESS)
            iconTexture->AddRef();

    }
    return iconTexture;

}


QImage* OpenGLScene::UTLGetCombinedIcon(QList<QString>& subIcons)
{
    QImage* combinedIcon = NULL;

    //Return empty bitmap if list is not valid (empty)
    if(subIcons.size() == 0) return combinedIcon;

    //Return native (uncombined) bitmap if list size is 1
    if(subIcons.size() == 1) return UTLGetIcon(subIcons[0]);

    //Create key for hash
    QString key,name;
    foreach(name,subIcons)
    {
        key = key + name;
    }


    if(m_IconsCache.contains(key))
    {
        combinedIcon =  m_IconsCache.value(key);
    }
    else
    {
        QList<QImage*> subPixmaps;

        int maxWidth = 0;
        int maxHeight = 0;
        bool hasAlfa = false;
        for(int i = 0; i<subIcons.size(); i++)
        {
            QImage* pixmap = UTLGetNativeIcon(subIcons[i]);
            if(!hasAlfa) hasAlfa = pixmap->hasAlphaChannel();
            subPixmaps.append(pixmap);
            int width = pixmap->width();
            int height = pixmap->height();
            if(width > maxWidth) maxWidth = width;
            if(height > maxHeight) maxHeight = height;
        }

        //TEMP - Workaround MC bug on raptor
        if(maxWidth % 2)
            maxWidth++;
        if(maxHeight % 2)
            maxHeight++;

        QPixmap dashboard(maxWidth ,maxHeight);
        dashboard.fill();

        QPainter p;
        p.begin(&dashboard);

        QPainter::CompositionMode mode = QPainter::RasterOp_SourceAndDestination;

        p.setCompositionMode(mode);
        for(int i = 0; i<subPixmaps.size(); i++)
        {
            p.drawImage((maxWidth - subPixmaps[i]->width())/2 ,
                        (maxHeight -subPixmaps[i]->height())/2,
                                                *(subPixmaps[i]));
        }

        p.end();



        combinedIcon = new  QImage(dashboard.toImage());
        if(!hasAlfa) combinedIcon = UTLInitAlphaByTransparentColor(combinedIcon,QColor(255,255,255));

        m_IconsCache.insert(key,combinedIcon);
    }
    return combinedIcon;
}

QImage* OpenGLScene::UTLGetIcon(QString iconPath, QColor transparentColor, bool preventScaling)
{
    qreal  GENERAL_ICONS_SCALING_FACTOR = 3.0F;//UTL_Settings::Instance()->GetDpiResizeHeightFactor();

    QImage* icon = NULL;
    QImage* scaledIcon = NULL;

    //Return empty bitmap if list is not valid (empty)
    if(iconPath == "") return NULL;

    QString iconName =iconPath;//GetActualPath(iconPath);

    if(m_IconsCache.contains(iconName))
    {
        scaledIcon =  m_IconsCache.value(iconName);
    }
    else
    {
        //TEMP - woraround MC bug
        QImage tempImg(iconName);
        if(tempImg.isNull())
        {
            qDebug("PATH PROBLEM %s",iconName.toLatin1().constData());
            return NULL;
        }
        if(!(tempImg.hasAlphaChannel()))
        {
            int tmpWidth = tempImg.width();
            if(tmpWidth == 0) return NULL;

            if(tmpWidth % 2)
            {
                QPixmap dashboard(tempImg.width() + 1,tempImg.height() + 1);
                dashboard.fill();

                QPainter p;
                p.begin(&dashboard);
                p.drawImage(0,0,tempImg);
                p.end();
                icon = new QImage(dashboard.toImage());
            }
            else
                icon = new QImage(tempImg);

            icon = UTLInitAlphaByTransparentColor(icon, transparentColor);
        }
        else
        {
            icon = new QImage(tempImg);
        }

        if(icon != NULL)
        {
            int w = icon->width()*GENERAL_ICONS_SCALING_FACTOR;
            int h = icon->height()*GENERAL_ICONS_SCALING_FACTOR;

            if((w==icon->width() && h == icon->height()) || preventScaling)
            {
                scaledIcon = icon;
            }
            else
            {
                scaledIcon = new QImage(icon->scaled(w,h,Qt::KeepAspectRatioByExpanding, Qt::SmoothTransformation));
                delete icon;
            }
        }

        m_IconsCache.insert(iconName,scaledIcon);
    }
    return scaledIcon;
}

QImage* OpenGLScene::UTLGetNativeIcon(QString iconName)
{
    qreal  GENERAL_ICONS_SCALING_FACTOR = 3.0F;//UTL_Settings::Instance()->GetDpiResizeHeightFactor();
    QImage* icon = NULL;
    QImage* scaledIcon = NULL;

    //Return empty bitmap if list is not valid (empty)
    if(iconName == "") return NULL;

    iconName = iconName;//GetActualPath(iconName);

    if(m_NativeIconsCache.contains(iconName))
    {
        scaledIcon =  m_NativeIconsCache.value(iconName);
    }
    else
    {
        icon = new QImage(iconName);

        if(icon != NULL)
        {
            int w = icon->width()*GENERAL_ICONS_SCALING_FACTOR;
            int h = icon->height()*GENERAL_ICONS_SCALING_FACTOR;
            if(w==icon->width() && h == icon->height())
            {
                scaledIcon = icon;
            }
            else
            {
                scaledIcon = new QImage(icon->scaled(w,h,Qt::KeepAspectRatioByExpanding, Qt::SmoothTransformation));
                delete icon;
            }
        }
        m_NativeIconsCache.insert(iconName,scaledIcon);
    }

    return scaledIcon;
}


QImage* OpenGLScene::UTLInitAlphaByTransparentColor(QImage* icon, QColor transparentColor)
{


    //Turn the transparentColor pixels to transparent
    //This works in Linux embbeded , but expansive according to documentation
    //TODO: Need to find a more effieient way - maybe by QPainter and Composition
    QImage mask = icon->createMaskFromColor(transparentColor.rgb(),Qt::MaskOutColor);
    icon->setAlphaChannel(mask);
    return icon;




//    unsigned int* bufcopy;
//    int width = icon->width();
//    int height = icon->height();
//    int buflen = icon->byteCount();

//    QImage im = icon->convertToFormat(QImage::Format_ARGB32);
//    int buflen2 = im.byteCount();

//    bufcopy = new unsigned int[width*height];

//    QRgb transparent = transparentColor.rgb();
//    memcpy((unsigned char*)bufcopy, (unsigned char*)im.bits(), buflen2);

//    for(int i = 0; i< width*height; i++)
//    {
//            if((bufcopy[i] & RGB_MASK) == (transparent & RGB_MASK))
//            {
//                    bufcopy[i] = transparent & RGB_MASK;
//            }
//    }
//    QImage* alphaIcon = new QImage((unsigned char*)bufcopy,width,height,QImage::Format_ARGB32);
//    return alphaIcon;

}



IMcMemoryBufferTexture* OpenGLScene::GetCombinedIcon(QList<QString>& subIcons,bool isFaded)
{
    static int factor = 50;
    qreal f = (qreal)factor/100.0F;

    QString factorKey = QString::number(factor);

    QString textureKey = QString::number(isFaded);
    textureKey += "|" + factorKey;

    foreach (QString subIcon, subIcons)
        textureKey += "|" + subIcon;

    if (!m_texturesCache.contains(textureKey))
    {
        QImage *pOrigImage = UTLGetCombinedIcon(subIcons);
        QImage *pCopyImage;
        if (pOrigImage)
        {
            qreal  MAP_ICONS_SCALING_FACTOR = f/*0.78*/*3.026042F/*UTL_Settings::Instance()->GetDpiResizeHeightFactor()*/ /2.369492F/*UTL_Settings::Instance()->GetResolutionResizeHeightFactor()*/;
            int worig = pOrigImage->width();
            int horig = pOrigImage->height();
            int w = worig*MAP_ICONS_SCALING_FACTOR;
            int h = horig*MAP_ICONS_SCALING_FACTOR;
            factor++; if(factor == 110) factor = 30;

            if(worig == w && horig == h)
            {
                pCopyImage = new QImage(*pOrigImage);
            }
            else
            {
                //Fixed in Android MapCore 7.8.0
                //w = CalcMC7ConformantSize(w);
                //h = CalcMC7ConformantSize(h);
                pCopyImage = new QImage(pOrigImage->scaled(w,h,Qt::KeepAspectRatioByExpanding, Qt::SmoothTransformation));
            }

            if (isFaded)
                MakeTransparent(pCopyImage);

            m_texturesCache[textureKey] = GetTextureFromIcon(pCopyImage);
            delete pCopyImage;
        }
        else
            m_texturesCache[textureKey] = 0;
    }

    return m_texturesCache[textureKey];
}

void OpenGLScene::MakeTransparent(QImage *img)
{
    QPainter p2;
    p2.begin(img);
    p2.setCompositionMode(QPainter::CompositionMode_DestinationIn);
    p2.fillRect(img->rect(), QColor(0, 0, 0, 150));
    p2.end();
}

char FONT_FACE[MAX_PATH]= "/system/fonts/Roboto-Medium.ttf";

void OpenGLScene::AlterSchemeFonts(IMcObjectScheme* scheme)
{
    IMcTextItem* ti;
    IMcErrors::ECode err;
    IMcFont* origFont;
    IMcLogFont* origLogFont;
    SMcVariantLogFont varOrigLogFont;
    IMcFont::SCharactersRange aCharactersRanges[7];

    if(scheme==NULL)
        return;

    //Check if alteration is needed (font and charset for specific languages and font height scaling on Android)
    bool needFontFaceUpdate = false;
    char* faceName;
    char tempFaceName[MAX_PATH];
    bool needCharsetUpdate = false;
    BYTE charset;

    bool needHeightUpdate = false;
    float factor;

//    QString lang = UTL_Settings::Instance()->GetLanguage();
//    if(lang == "az_AZ")
//    {
        needFontFaceUpdate = true;
        faceName = FONT_FACE; //"tahoma.ttf";//"micross.ttf";//Microsoft Sans Serif";
        needCharsetUpdate = true;
        charset =  0;//THAI_CHARSET
        aCharactersRanges[0].nFrom = 32;
        aCharactersRanges[0].nTo = 255;
        aCharactersRanges[1].nFrom = 0x011e;
        aCharactersRanges[1].nTo = 0x011f;
        aCharactersRanges[2].nFrom = 0x0130;
        aCharactersRanges[2].nTo = 0x0131;
        aCharactersRanges[3].nFrom = 0x015e;
        aCharactersRanges[3].nTo = 0x015f;
        aCharactersRanges[4].nFrom = 0x018f;
        aCharactersRanges[4].nTo = 0x018f;
        aCharactersRanges[5].nFrom = 0x01dd;
        aCharactersRanges[5].nTo = 0x01dd;
        aCharactersRanges[6].nFrom = 0x0259;
        aCharactersRanges[6].nTo = 0x0259;

        factor=1.0f;
        needHeightUpdate=true;
//    }
//    else if(lang == "th_TH")
//    {
//        needFontFaceUpdate = true;
//        faceName = "thai.ttf"; //"tahoma.ttf";//"micross.ttf";//Microsoft Sans Serif";
//        needCharsetUpdate = true;
//        charset =  222;//THAI_CHARSET
//    }
//    else if(lang == "he_IL" || lang == "iw_IL")
//    {
//        needFontFaceUpdate = true;
//        faceName = "micross.ttf";//Microsoft Sans Serif";
//        needCharsetUpdate = true;
//        charset =  177;//HEBREW_CHARSET;
//    }
//    //.......

//    charset =  0;
//    needFontFaceUpdate = true;
//    faceName = "bluehigh.ttf";//"micross.ttf";//Microsoft Sans Serif";

    //There is no need for alteration
    if(!needFontFaceUpdate && !needCharsetUpdate && !needHeightUpdate)
        return;

    //Search and update all schema fonts
    UINT schemeID;
    err = scheme->GetID(&schemeID);

    CMcDataArray<IMcObjectSchemeNode*> nodeList;
    scheme->GetNodes(&nodeList,IMcObjectSchemeNode::ENKF_SYMBOLIC_ITEM);
    FONTLIST_TYPE& fontList= GetMapFontsList();
    for (int i = 0; i < nodeList.GetLength(); i++)
    {
        ti=nodeList[i]->CastToTextItem();
        if(ti)
        {
            IMcLogFont* newLogFont = NULL;

            err = ti->GetFont(&origFont);
            origLogFont = origFont->CastToLogFont();

            err = origLogFont->GetLogFont(&varOrigLogFont);
            if(!fontList.contains(varOrigLogFont.LogFontAnsi.lfFaceName))
            {
                strcpy(tempFaceName,varOrigLogFont.LogFontAnsi.lfFaceName);
                memset(&(varOrigLogFont.LogFontAnsi),0,sizeof(varOrigLogFont.LogFontAnsi));
                //Apply needed changes
                if(needFontFaceUpdate) strcpy(varOrigLogFont.LogFontAnsi.lfFaceName , faceName);
                if(needCharsetUpdate) varOrigLogFont.LogFontAnsi.lfCharSet = charset;
                if(needHeightUpdate) varOrigLogFont.LogFontAnsi.lfHeight = 2.369492*/*UTL_HDPIRESIZE*/(15*factor);

                newLogFont =  CreateFont(varOrigLogFont.LogFontAnsi,aCharactersRanges,7);
                fontList[tempFaceName] = newLogFont;
//                IMcLogFont::SLogFontToTtfFile fontToTtf[1];
//                fontToTtf[0].LogFont=varOrigLogFont;
//                strcpy(fontToTtf[0].strTtfFileFullPathName,"/mnt/sdcard/TorchD/Bin/MapFonts/azeri.ttf");
//                err=IMcLogFont::SetLogFontToTtfFileMap(fontToTtf,1);
                qDebug("Font %s mapped over %s res=%d!",varOrigLogFont.LogFontAnsi.lfFaceName,tempFaceName,err);
            }
            else
            {
                newLogFont = fontList[varOrigLogFont.LogFontAnsi.lfFaceName];
                qDebug("Font %s found!",varOrigLogFont.LogFontAnsi.lfFaceName);
            }

            if(newLogFont != NULL)
            {
                err= ti->SetFont(newLogFont);
                qDebug("Font %s changed res=%d!",varOrigLogFont.LogFontAnsi.lfFaceName,err);
            }
        }
    }
}


IMcLogFont* OpenGLScene::CreateFont(LOGFONTA& lf, IMcFont::SCharactersRange aCharactersRanges[], int vLen)
{
    IMcLogFont* defFont=NULL;

    IMcLogFont::Create(&defFont,lf,true, aCharactersRanges, vLen);
    if(defFont==NULL)
        qDebug("Map_BaseObjectBuilder -- Failed to create font %s with h=%d, w=%d, set=%d !!!", lf.lfFaceName, lf.lfHeight, lf.lfWeight, lf.lfCharSet);
    else
        qDebug("Map_BaseObjectBuilder -- Font %s with h=%d, w=%d, set=%d successfully created.", lf.lfFaceName, lf.lfHeight, lf.lfWeight, lf.lfCharSet);

    return defFont;
}

IMcErrors::ECode OpenGLScene::AddStringCmd(IMcObject* pObj,QString val,int propid)
{
            return pObj->SetStringProperty(propid,val.toStdWString().data());
}
void OpenGLScene::BadTransparency()
{
#ifdef WIN32
        QString schemePath = "./Schemes/ThreeParallelArrowsToVerticalLine.m";
#else
        QString schemePath1 = "assets:/Schemes/PerpendicularLines.m";
        QString schemePath2 = "assets:/Schemes/PointIconCrossThreeMod.m";


#endif

    SMcVector3D Center;
    m_pViewport->GetCameraPosition(&Center);
    SMcVector3D aLocationPoints[2];
    aLocationPoints[0].x =	Center.x;
    aLocationPoints[0].y =	Center.y;
    aLocationPoints[0].z =	Center.z;

    aLocationPoints[1].x =	Center.x-500;
    aLocationPoints[1].y =	Center.y-500;
    aLocationPoints[1].z =	Center.z;
    QString str = "QWERTYASDFG";
    CreateSchemaObject(schemePath1, FIRST_SCHEME_ID, aLocationPoints, 2, NULL, &str);

    aLocationPoints[0].x =	Center.x - 10;
    aLocationPoints[0].y =	Center.y - 260;
    aLocationPoints[0].z =	Center.z;

    QString iconPath = "/sdcard/TorchD/Bin/Icons/TacticalGraphics/Textures/TACGRP.FSUPP.PNT.TGT.NUCTGT.present_A.bmp";

    CreateSchemaObject(schemePath2, SECOND_SCHEME_ID, aLocationPoints, 1, &iconPath);

}

void OpenGLScene::Relocate(EMapPos ePos)
{
    SMcVector3D CameraPosition;
    if (ePos == EMapPos::EMP_NETANYA)
    {
        CameraPosition =  SMcVector3D (674718 , 3579363 , 74);
    }
    else if (ePos == EMapPos::EMP_ELYAKIM)
    {
        CameraPosition =  SMcVector3D (693877 , 3612623 , 321);
    }
 /*   else if (ePos == EMapPos::EMP_ACCO)
    {
        CameraPosition =  SMcVector3D (693847 , 3644939 , 60);
    }*/
    else if (ePos == EMapPos::EMP_ARBEL)
    {
        CameraPosition =  SMcVector3D (729585 , 3634842 , 147);
    }
    m_pViewport->SetCameraPosition(CameraPosition);
    //    CameraPosition.z = 0;
    //    m_pViewport->SetCameraLookAtPoint(CameraPosition);


}

void OpenGLScene::AttachedOrientation(bool bAttached)
{
    m_bAttachedOrientation = bAttached;
}

void OpenGLScene::RotateAroundCenter(bool bEnable)
{
    m_bRotateAroundCenter = bEnable;
    if (bEnable)
    {
        GetCenterPosition(m_CenterPivotPoint);
    ///    CreateWorldPicObject(m_CenterPivotPoint);
        SMcVector3D sPos;
        m_pViewport->GetCameraPosition(&sPos);
        m_CenterPivotPoint.z = sPos.z;
        qDebug() << "CenterPivotPoint:" << m_CenterPivotPoint.x  << m_CenterPivotPoint.y  <<  m_CenterPivotPoint.z << "\n" ;
    }
}


void OpenGLScene::SetTerrainInfo(const sTerrainInfo &TerrainInfo)
{
    m_TerrainInfo = TerrainInfo;
}
void OpenGLScene::CreateArbelTerrain()
{
    IMcMapLayer *MapLayers[2];

    IMcNativeRasterMapLayer* pNativeRasterLayer = NULL;
    IMcNativeDtmMapLayer *pNativeDtmLayer = NULL;

    IMcErrors::ECode res = IMcNativeDtmMapLayer::Create(&pNativeDtmLayer, "/sdcard/Arbel/DTM" );

    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create Dtm layer");
            printf ("%d",res);
            return ;
    }
    MapLayers[0] = pNativeDtmLayer;

    res = IMcNativeRasterMapLayer::Create(&pNativeRasterLayer, "/sdcard/Arbel/Raster");

    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create Raster layer");
            printf ("%d",res);
            return ;
    }
    MapLayers[1] = pNativeRasterLayer;

    res = IMcMapTerrain::Create(&m_pArbelTerrain, m_pGridCoordinateSystem,MapLayers, 2 );
    if (res != IMcErrors::SUCCESS)
    {
            printf("Failed to create Terrain\n");
            printf ("%d",res);
            return ;
    }
    m_pArbelTerrain->AddRef();
/*
    SMcBox BoundingBox;
    pNativeRasterLayer->GetBoundingBox(&BoundingBox);
    SMcVector3D CameraPosition (BoundingBox.CenterPoint());
    int x;
    x=2;
*/
}
