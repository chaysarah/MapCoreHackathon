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
using MCTester.General_Forms;
using MCTester.MapWorld.Assist_Forms;
using MCTester.Managers.MapWorld;
using UnmanagedWrapper;
using MCTester.Managers;

namespace MCTester.Controls
{
    public partial class CtrlRawVector3DExtrusionParams : UserControl
    {
        private DNSExtrusionTexture m_RootExtrusionTexture;
        private DNSExtrusionTexture m_SideExtrusionTexture;
        private List<string> m_CmbColNames = new List<string>();
        // private List<string> m_ColNames = new List<string>();
        private bool m_isReadOnly = false;

        public CtrlRawVector3DExtrusionParams()
        {
            InitializeComponent();
            checkGroupBoxClipRect.Checked = false;

            cgbUseBuiltIndexingData_CheckedChanged(null, null);

            cbNonDefaultIndexDirectory.Checked = false;
            cbNonDefaultIndexDirectory_CheckedChanged(null, null);
            
        }

        internal void IsReadOnly(bool isReadOnly)
        {
            m_isReadOnly = isReadOnly;
            ctrlExtrusionTextureArray1.IsReadOnly(isReadOnly);
        }


        public void SetLayerFileName(string fileName)
        {
            if (fileName != "")
            {
                try
                {
                    m_CmbColNames.Clear();

                    cmbHeightColumn.Items.Clear();
                    cmbObjectIDColumn.Items.Clear();
                    cmbRoofTextureIndexColumn.Items.Clear();
                    cmbSideTextureIndexColumn.Items.Clear();

                    IDNMcGridCoordSystemGeographic tempGridCoordSystemGeographic = null;
                    DNSRawVectorParams tempRawVectorParams = null;
                    IDNMcRawVectorMapLayer tempVectorMapLayer = null;
                    List<IDNMcRawVectorMapLayer> rawVectorLayers = new List<IDNMcRawVectorMapLayer>();

                    if (fileName.Contains(Manager_MCLayers.RawVectorSplitStr))
                    {
                        string[] layers = fileName.Split(Manager_MCLayers.RawVectorSplitChar);
                        if (layers.Length > 1)
                        {
                            string strDataSource = layers[0];
                            string[] dataLayers = layers[1].TrimEnd(Manager_MCLayers.RawVectorLayersSplitChar).Split(Manager_MCLayers.RawVectorLayersSplitChar);

                            foreach (string layerName in dataLayers)
                            {
                                if (layerName != "")
                                {
                                    tempRawVectorParams = new DNSRawVectorParams(strDataSource + layerName, tempGridCoordSystemGeographic);
                                    tempVectorMapLayer = DNMcRawVectorMapLayer.Create(tempRawVectorParams, tempGridCoordSystemGeographic);
                                    if (tempVectorMapLayer != null && tempVectorMapLayer.GetNumFields() > 0)
                                        rawVectorLayers.Add(tempVectorMapLayer);
                                }
                            }
                        }
                    }
                    else
                    {
                        tempRawVectorParams = new DNSRawVectorParams(fileName, tempGridCoordSystemGeographic);
                        tempVectorMapLayer = DNMcRawVectorMapLayer.Create(tempRawVectorParams, tempGridCoordSystemGeographic);
                        if (tempVectorMapLayer != null && tempVectorMapLayer.GetNumFields() > 0)
                            rawVectorLayers.Add(tempVectorMapLayer);
                    }
                    m_CmbColNames.Add("");
                    string pstrName;
                    DNEFieldType peFieldType;
                    int height_index = -1;
                    foreach (IDNMcRawVectorMapLayer layer in rawVectorLayers)
                    {
                        uint numFields = layer.GetNumFields();
                        if (numFields > 0)
                        {
                            for (int i = 0; i < numFields; i++)
                            {
                                layer.GetFieldData((uint)i, out pstrName, out peFieldType);
                                if (!m_CmbColNames.Contains(pstrName))
                                {
                                    m_CmbColNames.Add(pstrName);
                                    if (pstrName.Trim().ToLower() == Manager_MCLayers.RawVector3DExtrusion_HeightColumnText.ToLower())
                                        height_index = m_CmbColNames.Count - 1; // i + 1;
                                }
                                // m_ColNames.Add(pstrName);
                            }
                        }
                    }
                        string[] colNames = m_CmbColNames.ToArray();
                        cmbHeightColumn.Items.AddRange(colNames);
                        cmbHeightColumn.SelectedIndex = height_index;

                        cmbObjectIDColumn.Items.AddRange(colNames);
                        cmbObjectIDColumn.SelectedIndex = 0;

                        cmbRoofTextureIndexColumn.Items.AddRange(colNames);
                        cmbRoofTextureIndexColumn.SelectedIndex = 0;

                        cmbSideTextureIndexColumn.Items.AddRange(colNames);
                        cmbSideTextureIndexColumn.SelectedIndex = 0;



                }
                catch (MapCoreException )
                {
                }
            }
        }

        public bool GetIsUseIndexing()
        {
            return cgbUseBuiltIndexingData.Checked;
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

        private string GetColName(ComboBox comboBox)
        {
            string colName = "";
            if(comboBox.SelectedIndex >= 0)
            {
                colName = m_CmbColNames[comboBox.SelectedIndex];
            }
            else 
            {
                colName = comboBox.Text;
            }
            return colName;
        }

        private int GetIndex(string text)
        {
            if (text != null)
                return m_CmbColNames.IndexOf(text);
            else
                return -1;
        }

        public DNSRawVector3DExtrusionParams GetExtrusionParams()
        {
            DNSRawVector3DExtrusionParams extrusionParams = new DNSRawVector3DExtrusionParams();
            extrusionParams.RoofDefaultTexture = m_RootExtrusionTexture;
            extrusionParams.SideDefaultTexture = m_SideExtrusionTexture;
            extrusionParams.aSpecificTextures = ctrlExtrusionTextureArray1.GetExtrusionTextures().ToArray();
            extrusionParams.strHeightColumn = GetColName(cmbHeightColumn);
            extrusionParams.strObjectIDColumn = GetColName(cmbObjectIDColumn) ;
            extrusionParams.strRoofTextureIndexColumn = GetColName(cmbRoofTextureIndexColumn);
            extrusionParams.strSideTextureIndexColumn = GetColName(cmbSideTextureIndexColumn);

            extrusionParams.pSourceCoordinateSystem = ctrlSourceGridCoordinateSystem.GridCoordinateSystem;
            extrusionParams.pTargetCoordinateSystem = ctrlTargetGridCoordinateSystem.GridCoordinateSystem;
            if (checkGroupBoxClipRect.Checked)
                extrusionParams.pClipRect = ctrlSMcBox1.GetBoxValue();

            return extrusionParams;
        }

        public void SetExtrusionParams(DNSRawVector3DExtrusionParams extrusionParams)
        {
            ctrlSourceGridCoordinateSystem.LoadCoordSysList();
            ctrlTargetGridCoordinateSystem.LoadCoordSysList();
            ctrlSourceGridCoordinateSystem.GridCoordinateSystem = extrusionParams.pSourceCoordinateSystem;
            ctrlTargetGridCoordinateSystem.GridCoordinateSystem = extrusionParams.pTargetCoordinateSystem;
            //SetExtrusionGraphicalParams(extrusionParams);
            m_RootExtrusionTexture = extrusionParams.RoofDefaultTexture;
            m_SideExtrusionTexture = extrusionParams.SideDefaultTexture;

            ctrlExtrusionTextureArray1.SetExtrusionTextures(extrusionParams.aSpecificTextures);

            SetComboBoxValue(cmbHeightColumn, extrusionParams.strHeightColumn);
            SetComboBoxValue(cmbObjectIDColumn, extrusionParams.strObjectIDColumn);
            SetComboBoxValue(cmbRoofTextureIndexColumn, extrusionParams.strRoofTextureIndexColumn);
            SetComboBoxValue(cmbSideTextureIndexColumn, extrusionParams.strSideTextureIndexColumn);

            if (extrusionParams.pClipRect.HasValue)
            {
                checkGroupBoxClipRect.Checked = true;
                ctrlSMcBox1.SetBoxValue(extrusionParams.pClipRect.Value);
            }
        }

        public DNSRawVector3DExtrusionGraphicalParams GetExtrusionGraphicalParams()
        {
            DNSRawVector3DExtrusionGraphicalParams vector3DExtrusionGraphicalParams = new DNSRawVector3DExtrusionGraphicalParams();
            vector3DExtrusionGraphicalParams.RoofDefaultTexture = m_RootExtrusionTexture;
            vector3DExtrusionGraphicalParams.SideDefaultTexture = m_SideExtrusionTexture;
            vector3DExtrusionGraphicalParams.aSpecificTextures = ctrlExtrusionTextureArray1.GetExtrusionTextures().ToArray();
            vector3DExtrusionGraphicalParams.strHeightColumn = GetColName(cmbHeightColumn);
            vector3DExtrusionGraphicalParams.strObjectIDColumn = GetColName(cmbObjectIDColumn);
            vector3DExtrusionGraphicalParams.strRoofTextureIndexColumn = GetColName(cmbRoofTextureIndexColumn);
            vector3DExtrusionGraphicalParams.strSideTextureIndexColumn = GetColName(cmbSideTextureIndexColumn);
            return vector3DExtrusionGraphicalParams;
        }

        public void SetExtrusionGraphicalParams(DNSRawVector3DExtrusionGraphicalParams vector3DExtrusionGraphicalParams)
        {
            m_RootExtrusionTexture = vector3DExtrusionGraphicalParams.RoofDefaultTexture;
            m_SideExtrusionTexture = vector3DExtrusionGraphicalParams.SideDefaultTexture;

            ctrlExtrusionTextureArray1.SetExtrusionTextures(vector3DExtrusionGraphicalParams.aSpecificTextures);

            SetComboBoxValue(cmbHeightColumn, vector3DExtrusionGraphicalParams.strHeightColumn);
            SetComboBoxValue(cmbObjectIDColumn, vector3DExtrusionGraphicalParams.strObjectIDColumn);
            SetComboBoxValue(cmbRoofTextureIndexColumn, vector3DExtrusionGraphicalParams.strRoofTextureIndexColumn);
            SetComboBoxValue(cmbSideTextureIndexColumn, vector3DExtrusionGraphicalParams.strSideTextureIndexColumn);

        }

        private void SetComboBoxValue(ComboBox combo, string value)
        {
            if (value != combo.Text)
            {
                combo.SelectedIndex = -1;
            }
            combo.Text = value;
        }

        private void btnSideDefaultTexture_Click(object sender, EventArgs e)
        {
            frmSExtrusionTexture extrusionTexture = new frmSExtrusionTexture(m_SideExtrusionTexture);
            if (m_isReadOnly)
                GeneralFuncs.SetControlsReadonly(extrusionTexture);
            extrusionTexture.Text = "Side Default Texture";
            if (extrusionTexture.ShowDialog() == DialogResult.OK)
                m_SideExtrusionTexture = extrusionTexture.GetExtrusionTexture();
        }

        private void btnRoofDefaultTexture_Click(object sender, EventArgs e)
        {
            frmSExtrusionTexture extrusionTexture = new frmSExtrusionTexture(m_RootExtrusionTexture);
            if (m_isReadOnly)
                GeneralFuncs.SetControlsReadonly(extrusionTexture);
            extrusionTexture.Text = "Roof Default Texture";
            if (extrusionTexture.ShowDialog() == DialogResult.OK)
                m_RootExtrusionTexture = extrusionTexture.GetExtrusionTexture();
        }

        internal void HideGridCoordinateSystemParams()
        {
            ctrlSourceGridCoordinateSystem.Visible = false;
            ctrlTargetGridCoordinateSystem.Visible = false;
        }

        public string GetIndexingDataDirectory()
        {
            if (cbNonDefaultIndexDirectory.Checked)
                return ctrlBrowseIndexingDataDirectory.FileName;
            else
                return "";
        }

        public void SetIndexingData(bool isUseIndexingBuildingData, string strIndexingData, bool pbNonDefaultIndexingDataDirectory)
        {
            cgbUseBuiltIndexingData.Checked = isUseIndexingBuildingData;
            cbNonDefaultIndexDirectory.Checked = pbNonDefaultIndexingDataDirectory;
            ctrlBrowseIndexingDataDirectory.FileName = strIndexingData;
        }

        private void cbNonDefaultIndexDirectory_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_IsSetReadOnlyBrowseIndexingDataDirectory)
                ctrlBrowseIndexingDataDirectory.Enabled = cbNonDefaultIndexDirectory.Checked;
        }

        private void cgbUseBuiltIndexingData_CheckedChanged(object sender, EventArgs e)
        {
            gbOtherParams.Enabled = !cgbUseBuiltIndexingData.Checked;
            if(!m_IsSetReadOnlyBrowseIndexingDataDirectory)
                ctrlBrowseIndexingDataDirectory.Enabled = cbNonDefaultIndexDirectory.Checked;
        }
        private bool m_IsSetReadOnlyBrowseIndexingDataDirectory = false;

        public void SetReadOnlyBrowseIndexingDataDirectory()
        {
            if (!cbNonDefaultIndexDirectory.Checked)
            {
                ctrlBrowseIndexingDataDirectory.SetReadOnly();
                m_IsSetReadOnlyBrowseIndexingDataDirectory = true;
                ctrlBrowseIndexingDataDirectory.Enabled = true;
            }
        }
    }
}
