#include "layersdialog.h"
#include <QVBoxLayout>
#include <QFileDialog>
#include <QListWidget>

CLayersDialog::CLayersDialog(QDialog *parent) : QDialog(parent)
{
    QVBoxLayout *mainLayout = new QVBoxLayout(this);

    setModal(true);


    int frameStyle = QFrame::Sunken | QFrame::Panel;

    m_pNativeDtmDirectoryNameLabel = new QLabel;
    m_pNativeDtmDirectoryNameLabel->setFrameStyle(frameStyle);
    QPushButton *openDtmDirectoryButton =
            new QPushButton(tr("Native DTM"));

    connect(openDtmDirectoryButton, SIGNAL(clicked()),
            this, SLOT(setDtmDirectoryName()));


    m_pRawDtmDirectoryNameLabel = new QLabel;
    m_pRawDtmDirectoryNameLabel->setFrameStyle(frameStyle);
    QPushButton *openRasterDirectory1Button =
            new QPushButton(tr("Raw DTM"));

    connect(openRasterDirectory1Button, SIGNAL(clicked()),
            this, SLOT(setRasterDirectory1Name()));

    m_pNativeRasterDirectoryNameLabel = new QLabel;
    m_pNativeRasterDirectoryNameLabel->setFrameStyle(frameStyle);
    QPushButton *openRasterDirectory2Button =
            new QPushButton(tr("Native Raster"));

    connect(openRasterDirectory2Button, SIGNAL(clicked()),
            this, SLOT(setRasterDirectory2Name()));


    m_pRawRasterDirectoryNameLabel = new QLabel;
    m_pRawRasterDirectoryNameLabel->setFrameStyle(frameStyle);
    QPushButton *openRasterDirectory3Button =
            new QPushButton(tr("Raw Raster"));

    connect(openRasterDirectory3Button, SIGNAL(clicked()),
            this, SLOT(setRasterDirectory3Name()));


    QPushButton *OkButton =
            new QPushButton(tr("Ok"));
    connect(OkButton, SIGNAL(clicked()),
            this, SLOT(accept()));

    QPushButton *CancelButton =
            new QPushButton(tr("Cancel"));
    connect(CancelButton, SIGNAL(clicked()),
            this, SLOT(reject()));



    QWidget *page = new QWidget;
    QGridLayout *layout = new QGridLayout(page);
    layout->setColumnStretch(1, 1);
    layout->addWidget(openDtmDirectoryButton, 0, 0);
    layout->addWidget(m_pNativeDtmDirectoryNameLabel, 0, 1);
    layout->addWidget(openRasterDirectory1Button, 1, 0);
    layout->addWidget(m_pRawDtmDirectoryNameLabel, 1, 1);
    layout->addWidget(openRasterDirectory2Button, 2, 0);
    layout->addWidget(m_pNativeRasterDirectoryNameLabel, 2, 1);
    layout->addWidget(openRasterDirectory3Button, 3, 0);
    layout->addWidget(m_pRawRasterDirectoryNameLabel, 3, 1);
    layout->addWidget(OkButton, 4, 0);
    layout->addWidget(CancelButton, 4, 1);


    mainLayout->addWidget(page);
//    setWindowTitle(tr("Standard Dialogs"));

}

void CLayersDialog::ClearInfo()
{
    m_pNativeDtmDirectoryNameLabel->clear();
    m_pRawDtmDirectoryNameLabel->clear();
    m_pNativeRasterDirectoryNameLabel->clear();
    m_pNativeDtmDirectoryNameLabel->clear();
}

QString CLayersDialog::DisplayFileDialog( )
{
    QFileDialog dlg;
    dlg.setWindowTitle("Open Directory");
    dlg.setAcceptMode(QFileDialog::AcceptOpen);
    dlg.setOption(QFileDialog::ShowDirsOnly);
    dlg.setFileMode(QFileDialog::DirectoryOnly);
    dlg.setReadOnly(true);
    dlg.setViewMode(QFileDialog::List);
    dlg.setDirectory("/sdcard/Elbit_Map_Material/");
    dlg.setFixedSize(m_Size.width(),m_Size.height());
    int res = dlg.exec();
    if (res)
    {
        QDir directory = dlg.selectedFiles()[0];
        return directory.absolutePath();
    }
    else
    {
        return QString("");
    }
}

void CLayersDialog::setRasterDirectory1Name()
{
    QString Path = DisplayFileDialog();
    if (Path != QString(""))
    {
         m_pRawDtmDirectoryNameLabel->setText(Path);
    }
}

void  CLayersDialog::SetWindowSize (const QSize & Size)
{
    m_Size = Size;
    setFixedSize(m_Size.width(),m_Size.height());
}

void CLayersDialog::setRasterDirectory2Name()
{
    QString Path = DisplayFileDialog();
    if (Path != QString(""))
    {
         m_pNativeRasterDirectoryNameLabel->setText(Path);
    }
}

void CLayersDialog::setRasterDirectory3Name()
{
    QString Path = DisplayFileDialog();
    if (Path != QString(""))
    {
         m_pRawRasterDirectoryNameLabel->setText(Path);
    }
}

void CLayersDialog::setDtmDirectoryName()
{
    QString Path = DisplayFileDialog();
    if (Path != QString(""))
    {
         m_pNativeDtmDirectoryNameLabel->setText(Path);
    }
}

sTerrainInfo CLayersDialog::GetInfo()
{
    sTerrainInfo Info;
    Info.m_NativeDtmDirectory = m_pNativeDtmDirectoryNameLabel->text();
    Info.m_RawDtmDirectory = m_pRawDtmDirectoryNameLabel->text();
    Info.m_NativeRasterDirectory = m_pNativeRasterDirectoryNameLabel->text();
    Info.m_RawRasterDirectory = m_pRawRasterDirectoryNameLabel->text();
    return Info;
}
