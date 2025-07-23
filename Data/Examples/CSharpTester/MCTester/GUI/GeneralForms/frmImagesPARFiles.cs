using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.General_Forms
{
    public partial class frmImagesPARFiles : Form
    {
        private IDNMcMapProduction mMapProduction;
        private DNELayerKind mSourceType;
        private bool mbIsRasterInWorldCoordSys;
        private DNSSourceFileParams[] mSourceFiles = null;

        public frmImagesPARFiles(IDNMcMapProduction mapProduction, DNELayerKind sourceType, bool bIsRasterInWorldCoordSys)
        {
            InitializeComponent();
            mMapProduction = mapProduction;
            mSourceType = sourceType;
            mbIsRasterInWorldCoordSys = bIsRasterInWorldCoordSys;
        }

        private void btnGeneratePARFile_Click(object sender, EventArgs e)
        {
            try
            {
                DNESourceParamsValidityFlags[] invalidParamsBitField = null;
                string[] fileExtensions = new string[clstFileExtensions.CheckedItems.Count];
                int numExtension = 0;

                for (int i = 0; i < clstFileExtensions.Items.Count; i++ )
                {
                    if (clstFileExtensions.GetItemCheckState(i) == CheckState.Checked)
                        fileExtensions[numExtension++] = clstFileExtensions.Items[i].ToString();
                }               

                mMapProduction.BuildSourceRasterOrDtmFilesList(ctrlBrowseControlSourceDir.FileName,
                                                                    mSourceType,
                                                                    mbIsRasterInWorldCoordSys,
                                                                    out mSourceFiles,
                                                                    out invalidParamsBitField,
                                                                    fileExtensions,
                                                                    chxRecrusive.Checked,
                                                                    DNESourceSorting._ESS_RESOLUTION_FROM_LOW_TO_HIGH);



                for (int i = 0; i < mSourceFiles.Length; i++)
                {
                    dgvSourceFiles.Rows.Add();
                    dgvSourceFiles[0, i].Value = mSourceFiles[i].strPathName;
                    dgvSourceFiles[1, i].Value = mSourceFiles[i].fXRes.ToString();
                    dgvSourceFiles[2, i].Value = mSourceFiles[i].fYRes.ToString();
                    dgvSourceFiles[3, i].Value = mSourceFiles[i].fMinX.ToString();
                    dgvSourceFiles[4, i].Value = mSourceFiles[i].fMinY.ToString();
                    dgvSourceFiles[5, i].Value = mSourceFiles[i].fMaxX.ToString();
                    dgvSourceFiles[6, i].Value = mSourceFiles[i].fMaxY.ToString();



                    if ((invalidParamsBitField[i] & DNESourceParamsValidityFlags._ESPVF_INVALID_RESOLUTION) != 0)
                    {
                        dgvSourceFiles[1, i].Value = "?";
                        dgvSourceFiles[2, i].Value = "?";
                    }
                    if ((invalidParamsBitField[i] & DNESourceParamsValidityFlags._ESPVF_INVALID_X_LIMITS) != 0)
                    {
                        dgvSourceFiles[3, i].Value = "?";
                        dgvSourceFiles[5, i].Value = "?";
                    }
                    if ((invalidParamsBitField[i] & DNESourceParamsValidityFlags._ESPVF_INVALID_Y_LIMITS) != 0)
                    {
                        dgvSourceFiles[2, i].Value = "?";
                        dgvSourceFiles[4, i].Value = "?";
                    }
                }

                txtNumberOfImageRasterFiles.Text = SourceFiles.Length.ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("BuildSourceRasterOrDtmFilesList", McEx);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FormClosing -= new FormClosingEventHandler(frmImagesPARFiles_FormClosing);
            CloseForm();
        }

        private void CloseForm()
        {
            if (mSourceFiles != null)
                this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void frmImagesPARFiles_Load(object sender, EventArgs e)
        {
            string [] fileExtensionsTypes = null;

            if (mSourceType == DNELayerKind._ELK_RASTER && mbIsRasterInWorldCoordSys == true)
                fileExtensionsTypes = new string[] { "bmp", "jpg", "jpeg", "gif", "tif", "tiff", "ecw", "sid", "j2k", "jp2", "png", "img", "bin" };
            else
                fileExtensionsTypes = new string[] { "xyz", "grd", "asc", "dtm", "dt0", "dt1", "dt2", "dt3", "dt4", "dt5" };

            clstFileExtensions.Items.AddRange(fileExtensionsTypes);            
        }

        public DNSSourceFileParams [] SourceFiles
        {
        	get { return mSourceFiles;}
        }

        private void frmImagesPARFiles_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.FormClosing -= new FormClosingEventHandler(frmImagesPARFiles_FormClosing);
            CloseForm();
        }
    }
}
