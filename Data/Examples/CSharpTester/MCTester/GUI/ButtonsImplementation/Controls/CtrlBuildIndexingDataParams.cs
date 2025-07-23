using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;
using MCTester.Managers.MapWorld;

namespace MCTester.Controls
{
    public partial class CtrlBuildIndexingDataParams : UserControl
    {
        private bool m_isReadOnly = false;

        public CtrlBuildIndexingDataParams()
        {
            InitializeComponent();
            //ctrlSMcBoxClipRect.DisabledZ();
            cbNonDefaultIndexDirectory_CheckedChanged(null, null);
        }

        public void IsReadOnly(bool isReadOnly)
        {
            m_isReadOnly = isReadOnly;
            ctrlBrowseIndexingDataDirectory.Enabled = true;
        }

        public void SetUI(bool m_bIs3DModel)
        {
            chxUseExisting.Checked = true;
            if(m_bIs3DModel)
            {
                ctrlLayerTargetGridCoordinateSystem.Location = new Point(5, 30);
                ctrlLayerSourceGridCoordinateSystem.Visible = false;
                ctrlBrowseRawDataDirectory.LabelCaption = "Raw Data:                             ";
                ntxTargetHighestResolution.SetFloat(new DNS3DModelConvertParams().fTargetHighestResolution);
                checkTilingScheme.Visible = false;  
            }
            else
            {
                lblFirstLowerQualityLevel.Visible = ntxTargetHighestResolution.Visible = false;
                ctrlBrowseRawDataDirectory.IsFolderDialog = false;
                ctrlBrowseRawDataDirectory.LabelCaption = "Data Source:                        ";
            }
        }

        public string GetStrRawDataDirectory() { return ctrlBrowseRawDataDirectory.FileName; } 

        public string GetStrIndexingDataDirectory() { return ctrlBrowseIndexingDataDirectory.FileName; } 

        public IDNMcGridCoordinateSystem GetSourceCoordinateSystem (){ return ctrlLayerSourceGridCoordinateSystem.GridCoordinateSystem; }

        public IDNMcGridCoordinateSystem GetTargetCoordinateSystem() { return ctrlLayerTargetGridCoordinateSystem.GridCoordinateSystem; }

        public DNSTilingScheme GetTilingScheme()
        {
            if (checkTilingScheme.Checked)
                return ctrlTilingSchemeParams1.GetTilingScheme();
            else
                return null;
        }

        public float GetTargetHighestResolution() { return ntxTargetHighestResolution.GetFloat();  }

       
        public bool GetIsUseClipRect() { return checkGroupBoxClipRect.Checked; }

        public bool GetIsUseExisting() { return chxUseExisting.Checked; }

        public DNSMcBox? GetClipRect()
        {
            if (checkGroupBoxClipRect.Checked)
                return ctrlSMcBoxClipRect.GetBoxValue();
            else
                return null;
        }

        private void checkTilingScheme_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTilingScheme.Checked)
                ctrlTilingSchemeParams1.SetStandardTilingScheme();
        }

        public bool IsUsedDefaultIndexingDataDir()
        {
            return !cbNonDefaultIndexDirectory.Checked;
        }

        private void cbNonDefaultIndexDirectory_CheckedChanged(object sender, EventArgs e)
        {
            ctrlBrowseIndexingDataDirectory.Enabled = cbNonDefaultIndexDirectory.Checked;
        }

        public bool CheckValidity()
        {
            bool validity = !cbNonDefaultIndexDirectory.Checked || (cbNonDefaultIndexDirectory.Checked && ctrlBrowseIndexingDataDirectory.FileName != "");
            if (!validity)
            {
                MessageBox.Show("Missing Non Default Index Directory", "Invalid Data");
                ctrlBrowseIndexingDataDirectory.Focus();
            }
            return validity;
        }

        private void ctrlBrowseRawDataDirectory_FileNameChanged(object sender, EventArgs e)
        {
            ctrlBrowseRawDataDirectory.FileName = Manager_MCLayers.CheckRawVector(ctrlBrowseRawDataDirectory.FileName, false, false); 
        }

        internal void SetUIIndexing(bool isIndexing)
        {
            SetUI(true);
            ctrlBrowseRawDataDirectory.Visible = label1.Visible = chxUseExisting.Visible = false;
            cbNonDefaultIndexDirectory.Enabled = isIndexing;
            pnlNonIndexingParams.Enabled = !isIndexing;

        }
        internal MCTBuildIndexingParams GetBuildIndexingParams()
        {
            MCTBuildIndexingParams buildIndexingParams = new MCTBuildIndexingParams();
            GetRaw3DModelParams(buildIndexingParams);
            buildIndexingParams.RawDataSourceDirectory = GetStrRawDataDirectory();
            buildIndexingParams.pSourceCoordinateSystem = ctrlLayerSourceGridCoordinateSystem.GridCoordinateSystem; 
            buildIndexingParams.IsUseExisting = chxUseExisting.Checked;
            buildIndexingParams.pTilingScheme = GetTilingScheme();
            return buildIndexingParams;
        }

        internal MCTRaw3DModelParams GetRaw3DModelParams()
        {
            MCTRaw3DModelParams raw3DModelParams = new MCTRaw3DModelParams();
            GetRaw3DModelParams(raw3DModelParams);

            return raw3DModelParams;
        }

        internal MCTRaw3DModelParams GetRaw3DModelParams(MCTRaw3DModelParams raw3DModelParams)
        {
            raw3DModelParams.pTargetCoordinateSystem = ctrlLayerTargetGridCoordinateSystem.GridCoordinateSystem;
            raw3DModelParams.pClipRect = GetClipRect();
            raw3DModelParams.fTargetHighestResolution = ntxTargetHighestResolution.GetFloat();
            raw3DModelParams.NonDefaultIndexingDataDirectory = IsUsedDefaultIndexingDataDir() ? null : GetStrIndexingDataDirectory();
            return raw3DModelParams;
        }

        internal void SetRaw3DModelParams(MCTRaw3DModelParams raw3DModelParams)
        {
            ctrlLayerTargetGridCoordinateSystem.GridCoordinateSystem = raw3DModelParams.pTargetCoordinateSystem;
            checkGroupBoxClipRect.Checked = raw3DModelParams.pClipRect.HasValue;
            ctrlSMcBoxClipRect.SetBoxValue(raw3DModelParams.pClipRect);
            ntxTargetHighestResolution.SetFloat(raw3DModelParams.fTargetHighestResolution);

            cbNonDefaultIndexDirectory.Checked = raw3DModelParams.IsUseNonDefaultIndexDirectory;
            ctrlBrowseIndexingDataDirectory.FileName = raw3DModelParams.NonDefaultIndexingDataDirectory;
        }
    }
}
