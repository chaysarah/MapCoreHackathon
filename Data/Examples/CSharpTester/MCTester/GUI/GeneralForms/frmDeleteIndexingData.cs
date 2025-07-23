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
    public partial class frmDeleteIndexingData : Form
    {
        bool m_bIs3DModel;
        public frmDeleteIndexingData(bool bIs3DModel = true)
        {
            InitializeComponent();

            m_bIs3DModel = bIs3DModel;
            if (m_bIs3DModel)
            {
                Text = "Delete Indexing Data For Raw 3D Model Map Layer";
                ctrlBrowseControlRawData.IsFolderDialog = true;
            }
            else
            {
                Text = "Delete Indexing Data For Raw Vector 3D Extrusion Map Layer";
                ctrlBrowseControlRawData.IsFolderDialog = false;
            }

            cbNonDefaultIndexDirectory_CheckedChanged(null, null);
        }

        private void btnDeleteIndexingData_Click(object sender, EventArgs e)
        {
            bool validity = !cbNonDefaultIndexDirectory.Checked || (cbNonDefaultIndexDirectory.Checked && ctrlBrowseIndexingDataDirectory.FileName != "");
            if (!validity)
            {
                MessageBox.Show("Missing Non Default Index Directory", "Invalid Data");
                ctrlBrowseIndexingDataDirectory.Focus();
                return;
            }

            string msg =
           "The following destination directory contents will\n" +
           "be erased before starting the conversion:"  +
           ( ctrlBrowseControlRawData.FileName != "" ? "\n\n" + ((m_bIs3DModel)? "Raw Data: ": "Source Data: ") + ctrlBrowseControlRawData.FileName: "" ) +
           ( ctrlBrowseIndexingDataDirectory.FileName != "" ? "\n\nIndexing Data: " + ctrlBrowseIndexingDataDirectory.FileName: "" ) + 
           "\n\nAre you sure you want to continue?";
            DialogResult dialogResult = MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    if (m_bIs3DModel)
                        DNMcRaw3DModelMapLayer.DeleteIndexingData(ctrlBrowseControlRawData.FileName,
                            cbNonDefaultIndexDirectory.Checked ? ctrlBrowseIndexingDataDirectory.FileName : null);
                    else
                        DNMcRawVector3DExtrusionMapLayer.DeleteIndexingData(ctrlBrowseControlRawData.FileName,
                            cbNonDefaultIndexDirectory.Checked ? ctrlBrowseIndexingDataDirectory.FileName : null);
                    MessageBox.Show("Delete indexing data ended successfully", "Delete Indexing Data", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DeleteIndexingData", McEx);
                }
            }
        }

        private void cbNonDefaultIndexDirectory_CheckedChanged(object sender, EventArgs e)
        {
            ctrlBrowseIndexingDataDirectory.Enabled = cbNonDefaultIndexDirectory.Checked;
        }

        private void ctrlBrowseControlRawData_FileNameChanged(object sender, EventArgs e)
        {
            ctrlBrowseControlRawData.FileName = Manager_MCLayers.CheckRawVector(ctrlBrowseControlRawData.FileName, false, false);

        }
    }
}
