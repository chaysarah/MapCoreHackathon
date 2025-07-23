#include "openglscene.h"

#include <QtGui>
#include <QGLWidget>
#include <qgraphicsview.h>
#include <qapplication.h>
#include <QAction>
#include <QImage>
#include <QtWidgets>
#include "GraphicsView.h"



int main(int argc, char **argv)
{
    QApplication app(argc, argv);
    GraphicsView view;
    view.setViewport(new QGLWidget(QGLFormat(QGL::SampleBuffers)));


    view.setViewportUpdateMode(QGraphicsView::FullViewportUpdate);
    QGraphicsScene *pScene = new OpenGLScene();



   ((OpenGLScene *)pScene)->SetWinId(view.GetWinId());
    view.setScene(pScene);


    view.showMaximized();

   return app.exec();
}
