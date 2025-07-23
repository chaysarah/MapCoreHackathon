using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.GUI.Map;
using MCTester.Managers.MapWorld;
using MCTester.Managers;
using MapCore.Common;
using System.IO;
using MCTester.Managers.ObjectWorld;
using MCTester.MapWorld;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls;

namespace MCTester.General_Forms
{
    public partial class frmImageDialog : Form
    {
        private IDNMcImage m_mcImage = null;
        private bool m_isLoaded = false;

        List<IDNMcImage> m_lstExistingImages = new List<IDNMcImage>();

        public void SetExistingImages(List<IDNMcImage> lstExistingImages)
        {
            m_lstExistingImages = lstExistingImages;

            lstImages.Items.Clear();

            List<IDNMcImage> images = Managers.Manager_MCImages.GetImages();
            foreach (IDNMcImage image in m_lstExistingImages)
            {
                if (!images.Contains(image))
                {
                    images.Add(image);
                }
            }

            int index = 0, selectedIndex = -1;
            foreach (IDNMcImage key in images)
            {
                if (key == m_mcImage)
                    selectedIndex = index;
                lstImages.Items.Add(key);

                index++;
            }
            if (images.Count > 0 && selectedIndex >= 0)
            {
                lstImages.SetSelected(selectedIndex, true);
                lstImages_SelectedIndexChanged(null, null);
            }
        }

        public frmImageDialog(IDNMcImage mcImage = null)
        {
            InitializeComponent();
            m_isLoaded = true;
            ctrlStrFileName.FileNameChanged += ctrlStrFileName_FileNameSelectedOrChanged;
            ctrlStrFileName.Filter =
                            "Image Files (*.bmp;*.jpg;*.jpeg;*.tif;*.tiff;*.gif;*.png;*.svg;*.ico;)|" +
                            "*.bmp;*.jpg;*.jpeg;*.tif;*.tiff;*.gif;*.png;*.ico;*.svg|" +
                            "Bitmap files (*.bmp)|*.bmp|" +
                            "JPEG files (*.jpg, *.jpeg)|*.jpg;*.jpeg|" +
                            "TIFF files (*.tif, *.tiff)|*.tif;*.tiff|" +
                            "GIF files (*.gif)|*.gif|" +
                            "PNG files (*.png)|*.png|" +
                            "Icon files (*.ico)|*.ico|" +
                            "SVG files (*.svg)|*.svg|" +
                            "All Files|*.*";

            rbCreateAsPixelBuffer_CheckedChanged(null, null);

            if (mcImage != null)
            {
                m_mcImage = mcImage;
                DNSMcFileSource mcFileSource = mcImage.GetFileSource();

                GetImageData(mcImage);
                if (mcFileSource.strFileName != "" && mcFileSource.strFileName != null)
                {
                    ctrlStrFileName.FileName = mcFileSource.strFileName;
                }
                else if (mcFileSource.strFormatExtension != "" && mcFileSource.strFormatExtension != null)
                {
                    ctrlStrFileName.FileName = "." + mcFileSource.strFormatExtension;
                }
            }

           

            cmbResizeFilter.Items.AddRange(Enum.GetNames(typeof(DNEResizeFilter)));
            cmbResizeFilter.SelectedIndex = 0;

            m_isLoaded = false;
        }

        public IDNMcImage GetImage() { return m_mcImage; }

        private void ctrlStrFileName_FileNameSelectedOrChanged(object sender, EventArgs e)
        {
            if (ctrlStrFileName.FileName != "" && !m_isLoaded)
            {
                try
                {
                    GetImageData(DNMcImage.Create(new DNSMcFileSource(ctrlStrFileName.FileName)));
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("IDNMcImage.Create", McEx);
                }
            }
        }

        private void GetImageData(IDNMcImage mcImage)
        {
            try
            {
                uint width, height;
                mcImage.GetSize(out width, out height);
                ntxWidth.SetUInt32(width);
                ntxHeight.SetUInt32(height);

                ntxPixelFormat.Text = mcImage.GetPixelFormat().ToString();

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IDNMcImage.GetSize/GetPixelFormat", McEx);
            }
        }

        private string GetExtension()
        {
            try
            {
                return Path.GetExtension(ctrlStrFileName.FileName).Substring(1);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Invalid Path Extension");
                return "";
            }
        }

        public string GetFilePath() { return ctrlStrFileName.FileName != "" ? ctrlStrFileName.FileName : ""; }

        public string GetFileName() { return ctrlStrFileName.FileName != "" ? Path.GetFileName(ctrlStrFileName.FileName) : ""; }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (tcImages.SelectedIndex == 0)
            {
                try
                {
                    if (ctrlStrFileName.FileName != "")
                    {
                        if (rbCreateAsFile.Checked)
                        {
                            m_mcImage = DNMcImage.Create(new DNSMcFileSource(ctrlStrFileName.FileName));
                        }
                        else if (rbCreateAsMemoryBuffer.Checked)
                        {
                            try
                            {
                                byte[] fileByte = File.ReadAllBytes(ctrlStrFileName.FileName);
                                m_mcImage = DNMcImage.Create(new DNSMcFileSource(fileByte, (chxUseFileExtension.Checked ? GetExtension() : null)));
                            }
                            catch (FileNotFoundException ex)
                            {
                                MessageBox.Show("Invalid File Name", "Create Image");
                                return;
                            }
                        }
                        else if (rbCreateAsPixelBuffer.Checked)
                        {
                            IDNMcImage mcTempImage = DNMcImage.Create(new DNSMcFileSource(ctrlStrFileName.FileName));
                            m_mcImage = DNMcImage.Create(mcTempImage.GetPixelBuffer(), ntxWidth.GetUInt32(), ntxHeight.GetUInt32(), mcTempImage.GetPixelFormat(), mcTempImage.GetNumMipmaps());
                        }
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                        MessageBox.Show("Missing File Name", "Create Image");
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNMcImage.Create", McEx);
                }
            }
           
        }

        private void rbCreateAsPixelBuffer_CheckedChanged(object sender, EventArgs e)
        {
            chxUseFileExtension.Enabled = rbCreateAsMemoryBuffer.Checked;
        }

        private void lstImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                IDNMcImage mcImage = (IDNMcImage)lstImages.SelectedItem;
                uint width, height;
                mcImage.GetSize(out width, out height);
                ntxExistsWidth.SetUInt32(width);
                ntxExistsHeight.SetUInt32(height);

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IDNMcImage.GetSize", McEx);
            }
        }

        private void btnSelectFromExisting_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItem != null)
            {
                m_mcImage = lstImages.SelectedItem as IDNMcImage;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Missing image selection", "Select Image");
        }

        private void btnCreateFromExisting_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItem != null)
            {
                try
                {
                    IDNMcImage selectedImage = lstImages.SelectedItem as IDNMcImage;
                    DNEResizeFilter resizeFilter = (DNEResizeFilter)Enum.Parse(typeof(DNEResizeFilter), cmbResizeFilter.Text);

                    m_mcImage = DNMcImage.Create(selectedImage, ntxExistsWidth.GetUInt32(), ntxExistsHeight.GetUInt32(), resizeFilter, chxFlipAroundX.Checked, chxFlipAroundY.Checked);
                    DialogResult = DialogResult.OK; 
                    Close();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNMcImage.Create", McEx);
                }
            }
            else
                MessageBox.Show("Missing image selection", "Create Image");
        }
    }
}