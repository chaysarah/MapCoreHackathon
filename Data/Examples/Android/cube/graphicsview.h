#ifndef GRAPHICSVIEW_H
#define GRAPHICSVIEW_H

#include <QtGui>
#include <QGLWidget>
#include <qgraphicsview.h>
#include <qapplication.h>
#include <QGraphicsScene>
#include <QLabel>
#include <QTime>
#include <QMenu>
#include <qaction.h>
#include "McBasicTypes.h"
#include  "IMcErrors.h"
#include "Map/IMcDtmMapLayer.h"
#include "Map/IMcRasterMapLayer.h"
#include "Map/IMcMapTerrain.h"
#include "Map/IMcMapViewport.h"
#include "Map/IMcMapGrid.h"
#include "Calculations/IMcGridCoordinateSystem.h"
#include "Calculations/IMcSpatialQueries.h"
#include "Calculations/IMcGeometricCalculations.h"
#include "Calculations/IMcGeographicCalculations.h"
#include "IMcEditMode.h"
#include "layersdialog.h"
#include <QPushButton>
#include "common.h"
//class QGestureEvent;
//class QPinchGesture;



class GraphicsView : public QGraphicsView
{
    Q_OBJECT
public:
     GraphicsView();
     long GetWinId();
     bool viewportEvent(QEvent *event);

protected:

private slots:
    void CreateTerrain();
    void CreateTerrainForTesting();
    void UpdateObjects();
    void Open2D();
    void Open3D();
    void OpenAR();
    void TestDraw();
    void RelocateNetanya();
    void RelocateElyakim();
    void RelocateArbel();
    void SetHeightsMap (bool bEnable);
    void DisplayObjects (bool bDisplay);
    void DisplaySightPresentation(bool bDisplay);
    void AttachedOrientation(bool bAttached);
    void RotateAroundCenter(bool bEnable);
    void DisplayPolygon(bool bDisplay);

protected:

    void resizeEvent(QResizeEvent *event);
    void GeoCalcCrash();



    float m_fRotationAngle;
    float m_fScaleFactor;
    float m_fLastTotalScaleFactor;

    SMcVector3D m_WorldStartCenterPoint;
    CLayersDialog m_LayersDlg;

    QPointF LastCenterPoint;

     IMcGeographicCalculations* m_gc;
    QPushButton *m_pButton;

};

#endif // GRAPHICSVIEW_H

