#include "graphicsview.h"
#include "openglscene.h"
#include <QVBoxLayout>
#include <QMenu>
#include <QMenuBar>

#include "terraininfo.h"


GraphicsView::GraphicsView() : m_fRotationAngle(0),
                m_fScaleFactor(1),m_gc(NULL)

{
    viewport()->setAttribute(Qt::WA_AcceptTouchEvents);
    setDragMode(ScrollHandDrag);

    setWindowTitle(tr("3D Model Viewer"));


     QVBoxLayout *boxLayout = new QVBoxLayout; // Main layout of widget

/*     QMenuBar* menuBar = new QMenuBar();
     QMenu *fileMenu = menuBar->addMenu(tr("Map"));
     QAction *pAction = new QAction(tr("Terrain"), this);
     connect(pAction, SIGNAL(triggered()), this, SLOT(CreateTerrain()));
     fileMenu->addAction(pAction);

     pAction = new QAction(tr("Test Draw"), this);
     connect(pAction, SIGNAL(triggered()), this, SLOT(TestDraw()));
     fileMenu->addAction(pAction);
     boxLayout->setMenuBar(menuBar);
     setLayout(boxLayout);*/

     QMenuBar* menuBar = new QMenuBar();
     QMenu *xmenu = menuBar->addMenu(tr("Map"));
     QAction *pAction = NULL; //= new QAction(tr("Terrain"), this);


     QMenu* submenuA = xmenu->addMenu( tr("Navigation") );
     QMenu* submenuB = xmenu->addMenu( tr("Switch View") );
     QMenu* submenuC = xmenu->addMenu( tr("Change Position") );
     QMenu* submenuD = xmenu->addMenu( tr("Extra Display") );
     QMenu* submenuE = xmenu->addMenu( tr("Map") );
 //    QMenu* submenuF = xmenu->addMenu( tr("Objects") );


 //    pAction = new QAction(tr("Update"), this);
 //    connect(pAction, SIGNAL(triggered()), this, SLOT(UpdateObjects()));
 //    submenuF->addAction(pAction);

     pAction = new QAction(tr("Terrain"), this);
     connect(pAction, SIGNAL(triggered()), this, SLOT(CreateTerrain()));
     submenuE->addAction(pAction);
     pAction = new QAction(tr("Terrain From File"), this);
     connect(pAction, SIGNAL(triggered()), this, SLOT(CreateTerrainForTesting()));
     submenuE->addAction(pAction);

     pAction = new QAction(tr("Attached Orientation to Device"), this);
     pAction->setCheckable(true);
     pAction->setChecked(false);
     connect(pAction, SIGNAL(toggled(bool)), this, SLOT(AttachedOrientation(bool)));
     submenuA->addAction(pAction);
     pAction = new QAction(tr("Rotate Around Center"), this);
     pAction->setCheckable(true);
     pAction->setChecked(false);
     connect(pAction, SIGNAL(toggled(bool)), this, SLOT(RotateAroundCenter(bool)));
     submenuA->addAction(pAction);

     pAction = new QAction(tr("2D"), this);
     connect(pAction, SIGNAL(triggered()), this, SLOT(Open2D()));
     submenuB->addAction(pAction);
     pAction = new QAction(tr("3D"), this);
     connect(pAction, SIGNAL(triggered()), this, SLOT(Open3D()));
     submenuB->addAction(pAction);
     pAction = new QAction(tr("AR"), this);
     connect(pAction, SIGNAL(triggered()), this, SLOT(OpenAR()));
     submenuB->addAction(pAction);

     pAction = new QAction(tr("Netanya"), this);
     connect(pAction, SIGNAL(triggered()), this, SLOT(RelocateNetanya()));
     submenuC->addAction(pAction);
     pAction = new QAction(tr("Elyakim"), this);
     connect(pAction, SIGNAL(triggered()), this, SLOT(RelocateElyakim()));
     submenuC->addAction(pAction);
 //    pAction = new QAction(tr("Acco"), this);
 //    connect(pAction, SIGNAL(triggered()), this, SLOT(RelocateAcco()));
 //    submenuC->addAction(pAction);
 //    pAction = new QAction(tr("Arbel"), this);
 //    connect(pAction, SIGNAL(triggered()), this, SLOT(RelocateArbel()));
 //    submenuC->addAction(pAction);

     pAction = new QAction(tr("Heights Map"), this);
     pAction->setCheckable(true);
     pAction->setChecked(false);
     connect(pAction, SIGNAL(toggled(bool)), this, SLOT(SetHeightsMap(bool)));
     submenuD->addAction(pAction);
/*     pAction = new QAction(tr("Polygon"), this);
     pAction->setCheckable(true);
     pAction->setChecked(false);
     connect(pAction, SIGNAL(toggled(bool)), this, SLOT(DisplayPolygon(bool)));
     submenuD->addAction(pAction);*/
     pAction = new QAction(tr("10,000 Objects"), this);
     pAction->setCheckable(true);
     pAction->setChecked(false);
     connect(pAction, SIGNAL(toggled(bool)), this, SLOT(DisplayObjects(bool)));
     submenuD->addAction(pAction);
     pAction = new QAction(tr("Sight Presentation"), this);
     pAction->setCheckable(true);
     pAction->setChecked(false);
     connect(pAction, SIGNAL(toggled(bool)), this, SLOT(DisplaySightPresentation(bool)));
     submenuD->addAction(pAction);

     boxLayout->setMenuBar(menuBar);
     setLayout(boxLayout);

}

void GraphicsView::DisplayPolygon(bool bDisplay)
{
    ((OpenGLScene *) scene())->DisplayPolygon(bDisplay);
}

void GraphicsView::AttachedOrientation(bool bAttached)
{
    ((OpenGLScene *) scene())->AttachedOrientation(bAttached);
}

void GraphicsView::RotateAroundCenter(bool bEnable)
{
    ((OpenGLScene *) scene())->RotateAroundCenter(bEnable);
}
void  GraphicsView::DisplaySightPresentation(bool bDisplay)
{
    ((OpenGLScene *) scene())->DisplaySightPresentation(bDisplay);
}

void GraphicsView::DisplayObjects (bool bDisplay)
{
    ((OpenGLScene *) scene())->DisplayObjects(bDisplay);
}

void GraphicsView::SetHeightsMap (bool bEnable)
{
    ((OpenGLScene *) scene())->DtmVisualization(bEnable);
}

long GraphicsView::GetWinId()
{
    return winId();

}
bool GraphicsView::viewportEvent(QEvent *event)
{
    QPointF Point1(-1,-1);
    QPointF Point2(-1,-1);
    int nTouchCount = 0;
    IMcEditMode::EMouseEvent eMouseEvent;
   // qDebug() << "event->type() " << (int) event->type()<<"\n";

    switch(event->type())
    {

       case QEvent::TouchBegin:
       case QEvent::TouchUpdate:
       case QEvent::TouchEnd:
       {
    //      qDebug() <<  event->type() <<"\n";

          QTouchEvent *touchEvent = static_cast<QTouchEvent *>(event);
          QList<QTouchEvent::TouchPoint> touchPoints = touchEvent->touchPoints();

          nTouchCount = touchPoints.count();
          if (nTouchCount == 0)
          {
              break;
          }
          const QTouchEvent::TouchPoint &touchPoint1 = touchPoints.first();
          Point1 = touchPoint1.pos();
          if (touchEvent->touchPointStates() & Qt::TouchPointMoved)
          {
              eMouseEvent = IMcEditMode::EME_MOUSE_MOVED_BUTTON_DOWN;
              qDebug() << "EME_MOUSE_MOVED_BUTTON_DOWN" <<"\n";
          }
          if (nTouchCount==1)
          {
                if (touchEvent->touchPointStates() & Qt::TouchPointPressed)
                {
                    eMouseEvent = IMcEditMode::EME_BUTTON_PRESSED;
   //                 qDebug() << "EME_BUTTON_PRESSED" <<"\n";

                }

                if (touchEvent->touchPointStates() & Qt::TouchPointReleased)
                {
                    eMouseEvent = IMcEditMode::EME_BUTTON_RELEASED;
     //               qDebug() << "EME_BUTTON_RELEASED" <<"\n";
                }

          }
          if (nTouchCount==2)
          {
              const QTouchEvent::TouchPoint &touchPoint2 = touchPoints.last();
              Point2 = touchPoint2.pos();
              if (touchEvent->touchPointStates() & Qt::TouchPointPressed)
              {
                  eMouseEvent = IMcEditMode::EME_SECOND_TOUCH_PRESSED;
                  qDebug() << "EME_SECOND_TOUCH_PRESSED" <<"\n";
              }
              if (touchEvent->touchPointStates() & Qt::TouchPointReleased)
              {
                  eMouseEvent = IMcEditMode::EME_SECOND_TOUCH_RELEASED;
                  qDebug() << "EME_SECOND_TOUCH_RELEASED" <<"\n";
              }
          }
          ((OpenGLScene *) scene())->UpdateEditModeOnMouseEvent(Point1,Point2,eMouseEvent);
          return true;
       }
       default:
         break;
     }

    return QGraphicsView::viewportEvent(event);

}
void GraphicsView::resizeEvent(QResizeEvent *event) {
    if (scene())
    {
        QSize Size = event->size();
        uint w = Size.width();
        uint h = Size.height();

        scene()->setSceneRect(QRect(QPoint(0, 0), event->size()));
        ((OpenGLScene *) scene())->WindowResize(w , h);

   }
    QGraphicsView::resizeEvent(event);
}

void GraphicsView::UpdateObjects()
{
      ((OpenGLScene *) scene())->UpdateObjectsPos();
}

void GraphicsView::CreateTerrainForTesting()
{
    sTerrainInfo TerrainInfo;
//    TerrainInfo.m_RawVectorDirectory = QString ("/sdcard/MapCore/Maps/ESRI/EurHiway");
//    TerrainInfo.m_RawVectorDirectory = QString ("/sdcard/MapCore/Maps/ESRI/Europe");
 //  TerrainInfo.m_RawVectorDirectory = QString ("/sdcard/MapCore/Maps/ESRI/Hiways");

 //   TerrainInfo.m_RawVectorDirectory = QString ("/sdcard/MapCore/Maps/ESRI/EurCityL");
 //    TerrainInfo.m_RawVectorDirectory = QString ("/sdcard/MapCore/Maps/ESRI/Eurcities");

 //   TerrainInfo.m_RawRasterDirectory = QString ("/sdcard/Elbit_Map_Material/cadrg/2545_deu_std_2013_cadrg_250k/2545_deu_std_2013_cadrg_250k");
 //     TerrainInfo.m_RawRasterDirectory = QString ("/sdcard/Elbit_Map_Material/cadrg/2545_deu_std_2013_cadrg_500k/2545_deu_std_2013_cadrg_500k");
//      TerrainInfo.m_RawRasterDirectory = QString ("/sdcard/Elbit_Map_Material/cadrg/2545_deu_std_2013_cadrg_25k/2545_deu_std_2013_cadrg_25k");
//          TerrainInfo.m_RawRasterDirectory = QString ("/sdcard/MapCore/Maps/cadrg");
//       TerrainInfo.m_RawDtmDirectory = QString ("/sdcard/MapCore/Elbit_Map_Material/DTED/dted/e000");
//    TerrainInfo.m_RawDtmDirectory = QString ("/sdcard/MapCore/Elbit_Map_Material/DtedFile2");
//    TerrainInfo.m_RawDtmDirectory = QString ("/sdcard/DTED/dted");
//  TerrainInfo.m_RawRasterDirectory = QString ("/sdcard/MapCore/Maps/cib");
    TerrainInfo.m_RawRasterDirectory = QString ("/sdcard/Maps");

//    TerrainInfo.m_RawDtmDirectory = QString ("/sdcard/DtedFile2");

    ((OpenGLScene *) scene())->SetTerrainInfo(TerrainInfo);
    ((OpenGLScene *) scene())->BuildMapTerrain();

}

void GraphicsView::CreateTerrain()
{
    QSize s = size();
    m_LayersDlg.SetWindowSize(s);
    m_LayersDlg.ClearInfo();
    int nRes = m_LayersDlg.exec();
    if (nRes == 1)
    {
        sTerrainInfo TerrainInfo = m_LayersDlg.GetInfo();
        ((OpenGLScene *) scene())->SetTerrainInfo(TerrainInfo);
        ((OpenGLScene *) scene())->BuildMapTerrain();
    }
}

void GraphicsView::Open2D()
{
     ((OpenGLScene *) scene())->ChangeViewport(EDisplayType::EDT_2D);
}

void GraphicsView::Open3D()
{
    ((OpenGLScene *) scene())->ChangeViewport(EDisplayType::EDT_3D);
}

void GraphicsView::OpenAR()
{
    ((OpenGLScene *) scene())->ChangeViewport(EDisplayType::EDT_AR);
}

void GraphicsView::RelocateNetanya()
{
    EMapPos ePos = EMapPos::EMP_NETANYA;
    ((OpenGLScene *) scene())->Relocate(ePos);
}

void GraphicsView::RelocateElyakim()
{
    EMapPos ePos = EMapPos::EMP_ELYAKIM;
    ((OpenGLScene *) scene())->Relocate(ePos);
}
/*
void GraphicsView::RelocateAcco()
{
    EMapPos ePos = EMapPos::EMP_ACCO;
    ((OpenGLScene *) scene())->Relocate(ePos);
}
*/
void GraphicsView::RelocateArbel()
{
    EMapPos ePos = EMapPos::EMP_ARBEL;
    ((OpenGLScene *) scene())->Relocate(ePos);
}



void GraphicsView::TestDraw()
{
//    GeoCalcCrash();
    ((OpenGLScene *) scene())->TestDraw();
}

void GraphicsView::GeoCalcCrash()
{
    if(m_gc==NULL)
    {
        IMcGridCoordSystemGeographic* geoCoordSys;
        IMcGridCoordSystemGeographic::Create(
            &geoCoordSys,
            IMcGridCoordinateSystem::EDatumType::EDT_WGS84);

        geoCoordSys->AddRef();

        IMcGeographicCalculations::Create(&m_gc, geoCoordSys);
    }
    SMcVector3D point(3486905.75000,3228904.75000,0.00000),nearp;
    double d;
    SMcVector3D poly[2]={
 //       {3486060.00000,3228451.25000,0.00000},
 //       {3483771.25000,3228343.00000,0.00000},
 //       {3483753.50000,3229331.75000,0.00000},
 //       {3486034.25000,3229472.50000,0.00000},
 //       {3486565.50000,3229590.25000,0.00000},
 //       {3485488.75000,3230406.75000,0.00000},
 //       {3484868.00000,3228301.00000,0.00000},
 //       {3487167.25000,3228605.50000,0.00000},
 //       {3488478.25000,3229982.25000,0.00000},
 //       {3488096.75000,3229309.25000,0.00000},
 //       {3488851.00000,3228747.50000,0.00000},
 //       {3488933.75000,3227493.50000,0.00000},
 //       {3486619.25000,3226366.50000,0.00000},
 //       {3483550.25000,3227869.00000,0.00000},
        {3487683.00000,3228501.25000,0.00000},
        {3488357.00000,3227512.25000,0.00000}};
 //       {3487642.00000,3227287.50000,0.00000},
 //       {3486054.75000,3228191.00000,0.00000},
 //       {3486444.75000,3227108.00000,0.00000},
 //       {3485679.50000,3227211.75000,0.00000},
 //       {3484741.00000,3228207.25000,0.00000}};
    m_gc->ShortestDistPoint2DLine(point,poly,2,&nearp,&d);
}
