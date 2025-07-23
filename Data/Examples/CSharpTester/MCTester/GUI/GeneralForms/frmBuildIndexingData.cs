using MapCore;
using MapCore.Common;
using MCTester.Managers.MapWorld;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.General_Forms
{
    public partial class frmBuildIndexingDataForRawStaticObjects : Form
    {
        bool m_bIs3DModel;

        public frmBuildIndexingDataForRawStaticObjects(bool bIs3DModel = true)
        {
            InitializeComponent();
            m_bIs3DModel = bIs3DModel;
            if (m_bIs3DModel)
                Text = "Build Indexing Data For Raw 3D Model Map Layer";
            else
                Text = "Build Indexing Data For Raw Vector 3D Extrusion Map Layer";

            ctrlBuildIndexingDataParams1.SetUI(m_bIs3DModel);
        }

        private void btnBuildIndexingData_Click(object sender, EventArgs e)
        {
            if (!ctrlBuildIndexingDataParams1.CheckValidity())
            {
               // MessageBox.Show("Missing Raw Data Directory", "MCTester Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
/*
            if (ctrlBuildIndexingDataParams1.StrIndexingDataDirectory == String.Empty)
            {
                MessageBox.Show("Missing Destination Directory", "MCTester Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/

            string msg =
               (ctrlBuildIndexingDataParams1.IsUsedDefaultIndexingDataDir() ? "The default index directory" : "The following destination directory" ) +
            " contents will be erased before starting the conversion\n" +
             (ctrlBuildIndexingDataParams1.IsUsedDefaultIndexingDataDir() ? "\n"  :  (ctrlBuildIndexingDataParams1.GetStrIndexingDataDirectory() + "\n") )+
            "Are you sure you want to continue?";
            DialogResult dialogResult = MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    MCTBuildIndexingParams buildIndexingParams = ctrlBuildIndexingDataParams1.GetBuildIndexingParams();

                    if (m_bIs3DModel)
                    {
                        DNMcRaw3DModelMapLayer.BuildIndexingData(
                        buildIndexingParams.RawDataSourceDirectory,
                        buildIndexingParams.pTargetCoordinateSystem,
                        buildIndexingParams.pClipRect,
                        buildIndexingParams.pTilingScheme,
                        buildIndexingParams.fTargetHighestResolution,
                        buildIndexingParams.IsUseExisting,
                        buildIndexingParams.NonDefaultIndexingDataDirectory);
                    }
                    else
                    {
                        DNMcRawVector3DExtrusionMapLayer.BuildIndexingData(
                        buildIndexingParams.RawDataSourceDirectory,
                        buildIndexingParams.pSourceCoordinateSystem,
                        buildIndexingParams.pTargetCoordinateSystem,
                        buildIndexingParams.pClipRect,
                        buildIndexingParams.pTilingScheme,
                        buildIndexingParams.IsUseExisting,
                        buildIndexingParams.NonDefaultIndexingDataDirectory);
                    }
                    MessageBox.Show("Build indexing data ended successfully", "Build Indexing Data", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("BuildIndexingData", McEx);
                }
            }
        }
    }
}
