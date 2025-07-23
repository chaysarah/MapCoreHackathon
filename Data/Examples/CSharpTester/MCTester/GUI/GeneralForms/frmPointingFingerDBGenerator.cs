using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;
using MCTester.ButtonsImplementation;
using MCTester.Managers.MapWorld;
using System.IO;

namespace MCTester.General_Forms
{
    public partial class frmPointingFingerDBGenerator : Form
    {
        public frmPointingFingerDBGenerator()
        {
            InitializeComponent();
            ctrlLoadTestModel.BtnBrowse.Click += new EventHandler(BtnModelBrowse_Click); 
            ctrlGenerateOutputToDir.BtnBrowse.Click +=new EventHandler(BtnOutputDirBrowse_Click);
        }

        void BtnModelBrowse_Click(object sender, EventArgs e)
        {
            // load model
            IDNMcObject[] modelObj = null;
            IDNMcOverlay m_ActiveOverlay = Manager_MCOverlayManager.ActiveOverlay;

            try
            {
                UserDataFactory UDF = new UserDataFactory();

                modelObj = m_ActiveOverlay.LoadObjects(ctrlLoadTestModel.FileName, UDF);

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("LoadObjects", McEx);
            }

            if (modelObj == null)
            {
                MessageBox.Show("Invalid Object", "The object you chosen is invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // insert model location to be the default look at point
            ctrl3DVectorLookAtPt.SetVector3D( modelObj[0].GetLocationPoints(0)[0]);

            // move the camera to object location
            Manager_MCObject.MoveToLocation(modelObj[0], 0);

            MCTMapFormManager.MapForm.Viewport.PerformPendingUpdates();
            MCTMapFormManager.MapForm.Viewport.Render();

        }

        void BtnOutputDirBrowse_Click(object sender, EventArgs e)
        {
            //if (!Directory.Exists(ctrlGenerateOutputToDir.FileName))
            //{
            //    Directory.CreateDirectory(ctrlGenerateOutputToDir.FileName);
            //}
            //else
            //{
            //    if (Directory.GetFiles(ctrlGenerateOutputToDir.FileName).Length > 0)
            //    {
            //        Directory.Delete(ctrlGenerateOutputToDir.FileName);
            //        Directory.CreateDirectory(ctrlGenerateOutputToDir.FileName);
            //    }
            //}
        }

        private void btnRunPFGenerator_Click(object sender, EventArgs e)
        {
            if (ctrlGenerateOutputToDir.FileName != "" && ctrlLoadTestModel.FileName != "" && ctrl3DVectorLookAtPt.GetVector3D() != null)
            {
                this.Hide();

                btnPointingFingerDBGenerator PointingFingerDBGenerator = new btnPointingFingerDBGenerator(this);
                PointingFingerDBGenerator.ExecuteAction();

                this.Show();
            }
            else
                MessageBox.Show("Empty Fields", "Some fields are empty or invalids", MessageBoxButtons.OK, MessageBoxIcon.Error);            
        }
    }
}
