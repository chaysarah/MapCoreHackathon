using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCTester.General_Forms
{
    public partial class frmSaveSessionToFolderDef : Form
    {
        public string DestFolder;
        public string WindowsMapsBaseDir;
        public string AndroidMapsBaseDir;
        public string WebMapsBaseDir;
        public string UserComment;

        public string ImgFileName;

        string m_FileName = "Original";
        public static string WinMaps = @"C:\Maps\";
        string m_AndroidMaps = @"/sdcard/maps/";
        string m_WebMaps = @"http:Maps/";

        public frmSaveSessionToFolderDef()
        {
            InitializeComponent();

            //ctrlBrowseControlDestFolder.FileNameSelected += ctrlBrowseControlDestFolder_FileNameSelected;
            //ctrlBrowseControlDestFolderSettings.FileNameSelected += ctrlBrowseControlDestFolderSettings_FileNameSelected;
            string[] exts = new string[] { "PNG files (*.png)" ,"Bitmap files(*.bmp)", "JPEG files(*.jpg, *.jpeg)", "TIFF files (*.tif, *.tiff)", "GIFF files (*.gif)",  "Icon files (*.ico)|*.ico" };
            cmbImgFileExt.Items.AddRange(exts);
            cmbImgFileExt.Text = exts[0];

            txtImgFileName.Text = m_FileName + ".png";

            ctrlBrowseControlMapsBaseDir.FileName = WinMaps;
            txtAndroidMaps.Text = m_AndroidMaps;
            txtWebMaps.Text = m_WebMaps;

            checkGroupBoxMapsBaseDirectory.Checked = true;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.Automation_SaveFolder))
                ctrlBrowseControlDestFolderSettings.FileName = ctrlBrowseControlDestFolder.FileName = Properties.Settings.Default.Automation_SaveFolder;
        }

        private void ctrlBrowseControlDestFolder_FileNameSelected(object sender, EventArgs e)
        {
        }

        private void ctrlBrowseControlDestFolderSettings_FileNameSelected(object sender, EventArgs e)
        {
           
           
        }

        private string GetUserExt()
        {
            string ext = "";
            if(txtImgFileName.Text != "")
                ext = Path.GetExtension(txtImgFileName.Text);
            return ext;
        }

        private void SetFileName(bool isExtChange = false)
        {
            string ext = GetUserExt();
            string name = Path.GetFileNameWithoutExtension(txtImgFileName.Text);
            if (ext == "" || isExtChange)
                ext = GetSelectedExt();
            txtImgFileName.Text = name + ext;
            /* if (m_DestDirInfo != null)
             {
                 txtImgFileName.Text = m_DestDirInfo.Name + ext;
             }*/
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ctrlBrowseControlDestFolder.FileName != "")
            {
                DestFolder = ctrlBrowseControlDestFolder.FileName;

                if (!Directory.Exists(DestFolder))
                    Directory.CreateDirectory(DestFolder);
                if(checkGroupBoxMapsBaseDirectory.Checked)
                {
                    WindowsMapsBaseDir = ctrlBrowseControlMapsBaseDir.FileName;
                    AndroidMapsBaseDir = txtAndroidMaps.Text;
                    WebMapsBaseDir = txtWebMaps.Text;
                }
                UserComment = txtUserComment.Text;

                if (checkGroupBoxSaveToFile.Checked)
                    ImgFileName = txtImgFileName.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                if (ctrlBrowseControlDestFolder.FileName == "" )
                    MessageBox.Show("Destination folder empty", "Save Session To Folder");
               /* else if (ctrlBrowseControlDestFolder.FileName.Contains(" "))
                    MessageBox.Show("Destination folder cannot contain white space", "Save Session To Folder");*/
              //  DialogResult = DialogResult.Cancel;
            }
               
        }

        private void cmbImgFileExt_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFileName(true);
        }

        private string GetSelectedExt()
        {
            string ext = "";
            if (cmbImgFileExt.SelectedIndex >= 0)
            {
                switch (cmbImgFileExt.SelectedIndex)
                {
                    case 0:
                        ext = ".png";
                        break;
                    case 1:
                        ext = ".bmp";
                        break;
                    case 2:
                        ext = ".jpeg";
                        break;
                    case 3:
                        ext = ".tiff";
                        break;
                    case 4:
                        ext = ".giff";
                        break;
                    case 5:
                        ext = ".ico";
                        break;
                    default:
                        ext = ".png";
                        break;
                }
            }
            return ext;
        }

        private void btnSettingSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Automation_SaveFolder = ctrlBrowseControlDestFolderSettings.FileName;
            Properties.Settings.Default.Save();
            ctrlBrowseControlDestFolder.FileName = ctrlBrowseControlDestFolderSettings.FileName;
        }
    }
}
