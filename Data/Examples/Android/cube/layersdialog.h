#ifndef LAYERSDIALOG_H
#define LAYERSDIALOG_H
#include "QDialog"

#include <QLabel>
#include <QPushButton>
#include <terraininfo.h>

class CLayersDialog : public QDialog
{
    Q_OBJECT
public:
    explicit CLayersDialog( QDialog *parent = 0);
    void SetWindowSize (const QSize & Size);
    sTerrainInfo GetInfo();
    void ClearInfo();

signals:

public slots:

private slots:
     void setDtmDirectoryName();
     void setRasterDirectory1Name();
     void setRasterDirectory2Name();
     void setRasterDirectory3Name();

private:

     QString DisplayFileDialog();

    QLabel *m_pNativeDtmDirectoryNameLabel;
    QLabel *m_pRawDtmDirectoryNameLabel;
    QLabel *m_pNativeRasterDirectoryNameLabel;
    QLabel *m_pRawRasterDirectoryNameLabel;

    QSize m_Size;

};

#endif // LAYERSDIALOG_H
