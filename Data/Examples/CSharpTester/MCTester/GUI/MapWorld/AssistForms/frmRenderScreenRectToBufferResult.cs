using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld.Assist_Forms
{
    public partial class frmRenderScreenRectToBufferResult : Form
    {
        private Bitmap m_Bmp;
        private string m_fileName = "";
        private bool m_isClosePic = false;
        private string m_FolderPath;
        private IDNMcImage m_mcImage;

        public frmRenderScreenRectToBufferResult(Bitmap bmp, int width, int height, DNEPixelFormat viewportPixelFormat, IntPtr ptr, string folderPath, string fileName = "", bool isClosePic = false)
        {
            InitializeComponent();
            this.Width = width + 10;
            this.Height = height + 30;

            m_fileName = fileName;
            m_Bmp = bmp;
            m_isClosePic = isClosePic;
            m_FolderPath = folderPath;
            try
            {
                m_mcImage = DNMcImage.Create(ptr, (uint)width, (uint)height, viewportPixelFormat);
            }
            catch (MapCoreException McEx)
            {
                throw McEx;
            }
        }

        private void frmRenderScreenRectToBufferResult_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = (Image)m_Bmp;
        }

        private void frmRenderScreenRectToBufferResult_Shown(object sender, EventArgs e)
        {
            if (m_fileName == "")
            {
                if (MessageBox.Show("Do You Want To Save To File?", "Save To File", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SaveFileDialog saveFileFialog = new SaveFileDialog();
                    saveFileFialog.Filter = "Bitmap files(*.bmp) |*.bmp| JPEG files (*.jpg, *.jpeg)|*.jpg;*.jpeg| TIFF files (*.tif, *.tiff)|*.tif; *.tiff| GIFF files (*.gif)|*.gif| PNG files (*.png)|*.png| Icon files (*.ico)|*.ico| All Files|*.*";
                    saveFileFialog.RestoreDirectory = true;
                    saveFileFialog.InitialDirectory = m_FolderPath;
                    if (saveFileFialog.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = saveFileFialog.FileName;
                        SaveToFile(fileName);
                    }
                }
            }
            else
            {
                SaveToFile(m_fileName);
            }
            if (m_isClosePic)
                Close();
        }

        private void SaveToFile(string fileName)
        {
            try
            {
                m_mcImage.Save(fileName);
            }
            catch (MapCoreException McEx)
            {
                throw McEx;
            }
        }
    }
}
