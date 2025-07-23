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

#ifndef OPENGLSCENE_H
#define OPENGLSCENE_H

#include "point3d.h"

#include <QGraphicsScene>
#include <QLabel>
#include <QTime>
#include <QMenu>
#include <QAction>
#include "McBasicTypes.h"
#include  "IMcErrors.h"
#include "Map/IMcDtmMapLayer.h"
#include "Map/IMcRasterMapLayer.h"
#include "Map/IMcVectorMapLayer.h"
#include "Map/IMcMapTerrain.h"
#include "Map/IMcMapViewport.h"
#include "Map/IMcMapGrid.h"
#include "Calculations/IMcGridCoordinateSystem.h"
#include "Calculations/IMcSpatialQueries.h"
#include "Calculations/IMcGeometricCalculations.h"
#include "Calculations/IMcGeographicCalculations.h"
#include "IMcEditMode.h"
#include "OverlayManager/IMcFont.h"
#include "terraininfo.h"
#include <fps.h>
#include <QAndroidJniObject>
#include "common.h"

#ifndef QT_NO_CONCURRENT
#include <QFutureWatcher>
#endif

class Model;
extern char FONT_FACE[MAX_PATH];

typedef QMap<QString, IMcLogFont*> FONTLIST_TYPE;

enum EDisplayType
{
    EDT_2D,
    EDT_3D,
    EDT_AR
};

class OpenGLScene : public QGraphicsScene
{
    Q_OBJECT

public:
    OpenGLScene();
    void initializeGL();
    void drawBackground(QPainter *painter, const QRectF &rect);
    void WindowResize(uint w , uint h);
    void SetCameraScale (float fScale,const QPointF &CenterPoint);
    void GetCameraScale (float *fScale,const SMcVector3D &CenterPoint);
    bool ScreenToWorld(const QPointF &ScreenPoint, SMcVector3D *pWorldPoint);
    void WorldToScreen(const SMcVector3D &WorldPoint,  SMcVector3D *pScreenPoint);
    void SetCameraPitch(float fPitch);
    void SetCameraOrientation(float fYaw, const QPointF &CenterPoint);
    void GetCameraOrientation(float *fYaw);
    void SetCameraPosition(SMcVector3D Pos,bool bRelative);
    void ScrollCamera(int nDeltaX, int nDeltaY);
 //   void SetGestureStatus (bool bGestureIsActive);
    void SetTerrainInfo(const sTerrainInfo &TerrainInfo);
    void ExitEditModeCurrentAction();
    void StartEditMode();
    void UpdateEditModeOnMouseEvent( QPointF &Point1 , QPointF &Point2, IMcEditMode::EMouseEvent eEventType);
    void CreateViewport();
    void ChangeViewport(EDisplayType eDisplayType);
    bool IsDisplay2D();
    void SetWinId(long lWinId);
    void BuildMapTerrain();
    void TestDraw();
    void CreateTelemetryText();
    void CreateWorldPicObject(const SMcVector3D &Loc);
    void Relocate(EMapPos ePos);
    void DtmVisualization(bool bEnable);
    void DisplayObjects (bool Display);
 //   void CreateSchemaObject(QString schemePath, int schemeID, SMcVector3D* locationPoints, int pointsCount);
 //   void CreateSchemaObject(QString schemePath, int schemeID, SMcVector3D* locationPoints, int pointsCount, QString* pIconStr, QString *pStrText);
    void CreateSchemaObject(QString schemePath, int schemeID, SMcVector3D* locationPoints, int pointsCount, QString* pIconStr = NULL, QString *pStrText = NULL);
    void DisplaySightPresentation(bool bDisplay);
    void AttachedOrientation(bool bAttached);
    void RotateAroundCenter(bool bEnable);
    void DisplayPolygon(bool bDisplay);
    void CreateArbelTerrain();
    void UpdateObjectsPos();

    IMcMemoryBufferTexture* GetIconTexture(QString iconName, bool isFaded, QColor transparentColor);

    IMcMapGrid *CreateGrid();
//    static char m_StrTelemetryText[256];
    static float m_fYaw;
    static float m_fPitch;
    static float m_fRoll;


//    static float m_fOrgYaw;
//    static float m_fOrgPitch;
//    static float m_fOrgRoll;

//    virtual bool	event(QEvent * event);
public slots:
    void enableWireframe(bool enabled);
    void enableNormals(bool enabled);
    void setModelColor();
    void setBackgroundColor();
    void loadModel();
    void loadModel(const QString &filePath);
    void modelLoaded();

protected:
/*    void mousePressEvent(QGraphicsSceneMouseEvent *event);
    void mouseReleaseEvent(QGraphicsSceneMouseEvent *event);
    void mouseMoveEvent(QGraphicsSceneMouseEvent *event);
    void wheelEvent(QGraphicsSceneWheelEvent * wheelEvent);
*/

//    void paintEvent(QPaintEvent *e);


private:
    QDialog *createDialog(const QString &windowTitle) const;

//    void OpenMap();
    void ReleaseExistingTerrain();
    void CreateMapDevice();
     void CreateGridCoordinates();
     void CreateNativeRasterLayer();
     void CreateRawRasterLayer();
     void CreateRawVectorLayer();
     void CreateMapTerrain();
     void CreateNativeDtmLayer();
     void CreateRawDtmLayer();
     void CreateBuildingSolLayer();
     void CreateIvorySolLayer();
     void CreateElyakimSolLayer();
     void CreateOverlayManager();
     void CreateOverlays();
 //    void CreateObject();
     void CreateEditMode();
     void CreateMapGrid();
     void CreatePicture(const SMcVector3D &ScreenPos);
     void CreateText(const SMcVector3D &ScreenPos);
     void CreateFPSText();
     void CreateLine(const SMcVector3D &ScreenPos);
     void CreatePolygon(const SMcVector3D &Pos,UINT eType);
     void CenterViewport();
     void RenderUpdates();
     void BadTransparency();
     void AddBlueForce();
     void AddObjects();
      double fRand(double fMin, double fMax);
     void Set3DPosition();
     void GetCenterPosition(SMcVector3D &Center);
     SMcVector3D m_CenterPivotPoint;
    IMcErrors::ECode LoadObjectSchemes( QString strAssetPath,CMcDataArray<IMcObjectScheme*> *papLoadedSchemes);
     QImage* UTLGetCombinedIcon(QList<QString>& subIcons);
    QImage* UTLGetIcon(QString icon, QColor transparentColor = QColor(255, 255, 255), bool preventScaling = false);
    QImage* UTLGetNativeIcon(QString iconName);
    QImage* UTLInitAlphaByTransparentColor(QImage* icon, QColor transparentColor);
    QHash<QString, QImage*>  m_IconsCache;
    QHash<QString, QImage*>  m_NativeIconsCache;

     IMcMemoryBufferTexture* GetTextureFromIcon(QImage* icon);
   IMcMemoryBufferTexture* GetCombinedIcon(QList<QString>& subIcons,bool isFaded = false);
   void MakeTransparent(QImage *img);
 	QHash<QString, IMcMemoryBufferTexture *>        m_texturesCache;
    FONTLIST_TYPE& GetMapFontsList(){return m_fonts;}
    IMcLogFont* CreateFont(LOGFONTA& lf,IMcFont::SCharactersRange aCharactersRanges[]=NULL, int vLen=0);
    QMap<QString, IMcLogFont*> m_fonts;

    void AlterSchemeFonts(IMcObjectScheme *scheme);
    IMcErrors::ECode AddStringCmd(IMcObject* pObj,QString val,int propid);
    void setModel(Model *model);
    QImage* GetIcon(QString iconPath, QColor transparentColor);
 //   void BadTransparency();

    bool m_wireframeEnabled;
    bool m_normalsEnabled;
    bool m_bGestureIsActive;
    QColor m_modelColor;
    QColor m_backgroundColor;
    float m_oldDist ;
    long  m_lWinId;
    Model *m_model;
//    QTime m_LastTime;
    QTime m_time;
//    int m_lastTime;
    int m_mouseEventTime;
    float m_distance;
    Point3d m_rotation;
    Point3d m_angularMomentum;
    Point3d m_accumulatedMomentum;
    FPS m_FPS;
    QLabel *m_labels[4];
    QWidget *m_modelButton;

    IMcTextItem *m_pTelemetryText;
    QGraphicsRectItem *m_lightItem;


    bool           m_bFirstTime;
    EDisplayType   m_eDisplayType;
    IMcMapViewport *m_p2DViewport;
    IMcMapViewport *m_p3DViewport;
    IMcMapViewport *m_pARViewport;
    IMcMapViewport *m_pViewport;
    IMcEditMode *m_p2DEditMode;
    IMcEditMode *m_p3DEditMode;
    IMcEditMode *m_pAREditMode;
    IMcEditMode *m_pEditMode;
    IMcMemoryBufferTexture *m_pTexture;
    FPS *m_pFPS;
    IMcObject *m_pObject;
    IMcOverlayManager *m_pOverlayManager;
    IMcOverlay *m_pOverlay;
    IMcOverlay *m_pMulObjsOverlay;
    IMcObject **aObjects;
    bool m_bEditModeActive;
    int m_nCount;
    SMcPoint sPos;
    IMcPolygonItem *pPoly;
    IMcObject *pObj;
//    bool m_bTmp;
    int m_nResizeCounter;
    IMcMapDevice *m_pMapDevice;
    IMcGridCoordinateSystem* m_pGridCoordinateSystem;
    IMcNativeRasterMapLayer* m_pNativeRasterLayer;
    IMcNativeDtmMapLayer *m_pNativeDtmLayer;
    IMcRawDtmMapLayer *m_pRawDtmLayer;
    IMcNativeStaticObjectsMapLayer *m_pBuildingSoLayer;
    IMcNativeStaticObjectsMapLayer *m_pIvorySoLayer;
    IMcNativeStaticObjectsMapLayer *m_pElyakimSoLayer;
    IMcRawRasterMapLayer* m_pRawRasterLayer;
    IMcRawVectorMapLayer* m_pRawVectorLayer;
    IMcMapTerrain* m_pTerrain;
    IMcMapTerrain* m_pArbelTerrain;
    uint m_nWidth;
    uint m_nHeight;
    sTerrainInfo m_TerrainInfo;
    IMcTextItem *m_pFPSText;
    bool m_bTestInRender;
    jclass m_javaClass;
    bool m_jniReady;
    IMcObject* m_pSightPresentation;
    bool m_bAttachedOrientation;
    bool m_bRotateAroundCenter;
     IMcObject* m_pPolygon ;

//    static double m_dFrameIndex;
#ifndef QT_NO_CONCURRENT
    QFutureWatcher<Model *> m_modelLoader;
#endif
};

#endif
