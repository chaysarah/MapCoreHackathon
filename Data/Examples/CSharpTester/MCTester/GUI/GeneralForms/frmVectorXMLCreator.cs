using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.MapWorld;
using UnmanagedWrapper;
using MapCore.Common;
using MCTester.MapWorld.Assist_Forms;
using MCTester.Managers.MapWorld;
using MCTester.Managers;
using MCTester.Controls;

namespace MCTester.General_Forms
{
    public partial class frmVectorXMLCreator : Form
    {
        //private Dictionary<string, DNEFieldType> dFieldsData;
        private IDNMcMapProduction m_MapProduction;
        private List<DNEComparisonOperator> m_ComparisonOperator = new List<DNEComparisonOperator>();
        private DNSTilingScheme mcTilingScheme = null;

        private XmlVectorLayerGraphicalSettings m_settings;
        private IDNMcRectangleItem m_ClippingSchemeItem;
        private IDNMcObject m_CliipingRectObj;
        private bool m_isTextChanged = false;

        private int m_colSelect = 0;
        private bool m_isInCellSelect = false;
        private bool m_isInSelectAll = false;
        private List<IDNMcRawVectorMapLayer> m_selectedLayers = new List<IDNMcRawVectorMapLayer>();

        public frmVectorXMLCreator()
        {
            InitializeComponent();
            m_settings = new XmlVectorLayerGraphicalSettings();
            ctrlVectorLayerSettings.SelectedObject = m_settings;

            m_MapProduction = DNMcMapProduction.Create();

            cbSavingVersionCompatibility.Items.AddRange(Enum.GetNames(typeof(DNESavingVersionCompatibility)));
            cbSavingVersionCompatibility.Text = DNESavingVersionCompatibility._ESVC_LATEST.ToString();

            ctrlBrowseFileConvertVector.FileNameChanged += CtrlBrowseLayerRawVector_FileNameSelectedOrChanged;
            try
            {
                foreach (IDNMcMapViewport vp in Manager_MCViewports.AllParams.Keys)
                {
                    IDNMcMapTerrain[] terrains = vp.GetTerrains();
                    foreach (IDNMcMapTerrain terrain in terrains)
                    {
                        IDNMcMapLayer[] layers = terrain.GetLayers();
                        foreach (IDNMcMapLayer layer in layers)
                        {
                            if (layer.LayerType == DNELayerType._ELT_RAW_VECTOR)
                            {
                                IDNMcRawVectorMapLayer rawVectorMapLayer = (IDNMcRawVectorMapLayer)layer;
                                DNSRawVectorParams pParams;
                                IDNMcGridCoordinateSystem ppTargetCoordinateSystem;
                                DNSTilingScheme ppTilingScheme;

                                rawVectorMapLayer.GetCreateParams(out pParams, out ppTargetCoordinateSystem, out ppTilingScheme);
                                dgvRawVectorLayers.Rows.Add(false, Manager_MCNames.GetNameByObject(layer, layer.LayerType.ToString()).Replace("_ELT_", "") +" " + pParams.strDataSource);
                                dgvRawVectorLayers.Rows[dgvRawVectorLayers.RowCount - 1].Tag = rawVectorMapLayer;

                            }
                        }
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTerrains/GetLayers", McEx);
            }

        }

        private void CtrlBrowseLayerRawVector_FileNameSelectedOrChanged(object sender, EventArgs e)
        {
            if (!m_isTextChanged )
            {
                m_isTextChanged = true;
                ctrlBrowseFileConvertVector.FileName = Manager_MCLayers.CheckRawVector(ctrlBrowseFileConvertVector.FileName, true);
                ctrlRawVectorParams1.SetDisableStyling(ctrlBrowseFileConvertVector.FileName);
                SetMultipleLayers(ctrlBrowseFileConvertVector.FileName.Contains(Manager_MCLayers.RawVectorSplitStr));

                SetLayerData();
                m_isTextChanged = false;
            }
        }

        private void SetLayerData(IDNMcRawVectorMapLayer rawVectorMapLayer = null)
        {
            lstFieldIds.Items.Clear();
            IDNMcRawVectorMapLayer rawVectorMapLayer2 = rawVectorMapLayer;
            try
            {
                if (rawVectorMapLayer == null)
                {
                    DNSRawVectorParams pParams = new DNSRawVectorParams(ctrlBrowseFileConvertVector.FileName.Replace(Manager_MCLayers.RawVectorSplitStr, ""), DNMcGridCoordSystemGeographic.Create(DNEDatumType._EDT_WGS84));
                    rawVectorMapLayer2 = DNMcRawVectorMapLayer.Create(pParams);
                }
                if (rawVectorMapLayer2 != null)
                {
                    uint numFields = rawVectorMapLayer2.GetNumFields();
                    uint i = 0;
                    for (; i < numFields; i++)
                    {
                        string fieldName;
                        DNEFieldType fieldType;
                        rawVectorMapLayer2.GetFieldData(i, out fieldName, out fieldType);
                        lstFieldIds.Items.Add(i.ToString() + " - " + fieldName);
                    }
                    DNMcNullableOut<string[]> namesOut = new DNMcNullableOut<string[]>();
                    DNMcNullableOut<string[]> valuesOut = new DNMcNullableOut<string[]>();

                    rawVectorMapLayer2.GetLayerAttributes(namesOut, valuesOut);
                    if (namesOut.Value != null && namesOut.Value.Length > 0 && valuesOut.Value != null && valuesOut.Value.Length > 0)
                    {
                        gbAttrs.Enabled = true;
                        lstAttributes.Items.Clear();
                        for (i = 0; i < namesOut.Value.Length; i++)
                        {
                            lstAttributes.Items.Add(namesOut.Value[i] + " - " + valuesOut.Value[i]);
                        }
                    }
                    else
                        gbAttrs.Enabled = false;

                }

            }
            catch (MapCoreException )
            {
            }
        }

        private void SetMultipleLayers(bool isMultiple)
        {
            if (isMultiple)
            {
                strConvertedLayerName.Text = Manager_MCLayers.RawVectorMultiple;
                strConvertedLayerName.Enabled = false;
            }
            else
            {
                if (strConvertedLayerName.Text == Manager_MCLayers.RawVectorMultiple)
                    strConvertedLayerName.Text = "";
                strConvertedLayerName.Enabled = true;
            }
        }

        #region Private Events 
        #endregion


        private object IntToObject(int intValue)
        {
            return (object)intValue;
        }

        private object DoubleToObject(double doubleValue)
        {
            return (object)doubleValue;
        }

        private object StringToObject(string stringValue)
        {
            return (object)stringValue;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            DNSXmlVectorLayerGraphicalSettings xmlVectorLayerGraphicalSettings = (DNSXmlVectorLayerGraphicalSettings)m_settings;

            try
            {
                m_MapProduction.SaveVectorLayerGraphicalSettings(xmlVectorLayerGraphicalSettings, ctrlBrowseOutputXMLFile.FileName);

                MessageBox.Show("Vector Layer's graphic settings XML was created successfully", "XML created successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SaveVectorLayerGraphicalSettings", McEx);
            }
        }

        /*
        private DNEComparisonOperator ConvertSymbolToOperator(string oeratorSymbol)
        {
            switch(oeratorSymbol)
            {
                case "<>":
                    return DNEComparisonOperator._ECO_BETWEEN;
                case "~":
                    return DNEComparisonOperator._ECO_ALWAYS;
                case "=":
                    return DNEComparisonOperator._ECO_EQUAL;
                case ">=":
                    return DNEComparisonOperator._ECO_GREATER_EQUAL_THAN;
                case ">":
                    return DNEComparisonOperator._ECO_GREATER_THAN;
                case "<=":
                    return DNEComparisonOperator._ECO_LOWER_EQUAL_THAN;
                case "<":
                    return DNEComparisonOperator._ECO_LOWER_THAN;
                case "!=":
                    return DNEComparisonOperator._ECO_NOT_EQUAL;
                case "NUM":
                    return DNEComparisonOperator._ECO_NUM;
            }

            return DNEComparisonOperator._ECO_NUM;
        }

        private string ConvertOperatorToSymbol(DNEComparisonOperator EOperator)
        {
            switch (EOperator)
            {
                case DNEComparisonOperator._ECO_BETWEEN:
                    return "<>";
                case DNEComparisonOperator._ECO_ALWAYS:
                    return "~";
                case  DNEComparisonOperator._ECO_EQUAL:
                    return "=";
                case DNEComparisonOperator._ECO_GREATER_EQUAL_THAN:
                    return ">=" ;
                case DNEComparisonOperator._ECO_GREATER_THAN:
                    return ">" ;
                case DNEComparisonOperator._ECO_LOWER_EQUAL_THAN:
                    return "<=" ;
                case DNEComparisonOperator._ECO_LOWER_THAN:
                    return "<" ;
                case DNEComparisonOperator._ECO_NOT_EQUAL:
                    return "!=" ;
                case DNEComparisonOperator._ECO_NUM:
                    return "NUM" ;
            }

            return string.Empty;
        }
        */ 

        private void btnLoadXMLFile_Click(object sender, EventArgs e)
        {
            try
            {
                DNSXmlVectorLayerGraphicalSettings xmlVectorLayerGraphicalSettings = null ;
                try
                {
                    xmlVectorLayerGraphicalSettings = m_MapProduction.LoadVectorLayerGraphicalSettings(ctrlBrowseInputXML.FileName);
                    m_settings = new XmlVectorLayerGraphicalSettings(xmlVectorLayerGraphicalSettings);
                    ctrlVectorLayerSettings.SelectedObject = m_settings;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNSXmlVectorLayerGraphicalSettings", McEx);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cbIsUseFilter_CheckedChanged(object sender, EventArgs e)
        {
            tbFieldIdsFilter.Enabled = cbIsUseFilter.Checked;
            label9.Enabled = cbIsUseFilter.Checked;
            
            if (!cbIsUseFilter.Checked)
            {
                tbFieldIdsFilter.Text = "";
            }
        }

        private bool CheckDestinationDir()
        {
            if (ctrlBrowseFolderConvertVector.FileName == String.Empty)
            {
                MessageBox.Show("Missing Destination Directory", "MCTester Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private DialogResult AlertDestinationDir()
        {
            string msg =
           "The following destination directory contents will\n" +
           "be erased before starting the conversion:\n\n" +
           ctrlBrowseFolderConvertVector.FileName + "\n\n" +
           "Are you sure you want to continue?";
            return MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            //     return MessageBox.Show("Destination Directory Content Will Deleted, Continue?", "Destination Directory Content", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        private void btnConvertVectorLayer_Click(object sender, EventArgs e)
        {
            if (!CheckDestinationDir())
                return;
            if (AlertDestinationDir() == DialogResult.Cancel)
                return;

            try
            {
                if (m_selectedLayers.Count > 0)
                {
                    foreach (IDNMcRawVectorMapLayer rawVectorMapLayer in m_selectedLayers)
                    {
                        DNSRawVectorParams pParams;
                        IDNMcGridCoordinateSystem ppTargetCoordinateSystem;
                        DNSTilingScheme ppTilingScheme;

                        rawVectorMapLayer.GetCreateParams(out pParams, out ppTargetCoordinateSystem, out ppTilingScheme);
                        DNSVectorConvertParams vectorConvertParams1 = GetConvertParams(pParams);
                        vectorConvertParams1.pTargetCoordinateSystem = ppTargetCoordinateSystem;
                        vectorConvertParams1.pTilingScheme = ppTilingScheme;
                        vectorConvertParams1.strDataSource = pParams.strDataSource;
                        vectorConvertParams1.strConvertedLayerName = "";

                        m_MapProduction.ConvertVectorLayer(vectorConvertParams1);
                    }
                }
                else
                {
                    DNSRawVectorParams rawVectorParams = ctrlRawVectorParams1.RawVectorParams;
                    DNSVectorConvertParams vectorConvertParams = GetConvertParams(rawVectorParams);

                    if (ctrlBrowseFileConvertVector.FileName.Contains(Manager_MCLayers.RawVectorSplitStr))
                    {
                        string fileName = ctrlBrowseFileConvertVector.FileName;
                        string[] layers = fileName.Split(Manager_MCLayers.RawVectorSplitChar);
                        if (layers.Length > 1)
                        {
                            string strDataSource = layers[0];
                            string[] dataLayers = layers[1].TrimEnd(Manager_MCLayers.RawVectorLayersSplitChar).Split(Manager_MCLayers.RawVectorLayersSplitChar);
                            string strDestDir = vectorConvertParams.strDestDir;

                            foreach (string layerName in dataLayers)
                            {
                                if (layerName != "")
                                {
                                    string prefix = layerName;
                                    string StylingName = "";
                                    if (layerName.Contains(Manager_MCLayers.RawVectorStylingSplitStr))
                                    {
                                        string[] nameAndStyling = layerName.Split(Manager_MCLayers.RawVectorStylingSplitChar);
                                        if (nameAndStyling.Length == 2)
                                        {
                                            prefix = nameAndStyling[0];
                                            StylingName = nameAndStyling[1];
                                        }
                                    }
                                    if (vectorConvertParams.StylingParams == null)
                                        vectorConvertParams.StylingParams = new DNSInternalStylingParams();
                                    vectorConvertParams.StylingParams.strStylingFile = StylingName;

                                    vectorConvertParams.strDataSource = strDataSource + prefix;
                                    vectorConvertParams.strConvertedLayerName = "";
                                    m_MapProduction.ConvertVectorLayer(vectorConvertParams);
                                }
                            }
                        }
                    }
                    else
                    {
                        m_MapProduction.ConvertVectorLayer(vectorConvertParams);
                    }
                }
                MessageBox.Show("Convert of Vector Layer succeed", "Convert Vector Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ConvertVectorLayer", McEx);
            }
        }

        private DNSVectorConvertParams GetConvertParams(DNSRawVectorParams rawVectorParams)
        {
            uint[] filterFieldIds = null, filterLayerAttributes = null;
            if (tbFieldIdsFilter.Text != "")
                filterFieldIds = ConvertLabelStringToArray(tbFieldIdsFilter.Text);
            if (cbSaveLayerAttributes.Checked && tbLayerAttributesIdsFilter.Text != "")
                filterLayerAttributes = ConvertLabelStringToArray(tbLayerAttributesIdsFilter.Text);
            DNESavingVersionCompatibility version = (DNESavingVersionCompatibility)Enum.Parse(typeof(DNESavingVersionCompatibility), cbSavingVersionCompatibility.Text);

            DNSVectorConvertParams vectorConvertParams =
               new DNSVectorConvertParams(ctrlBrowseFolderConvertVector.FileName,
                                          strConvertedLayerName.Text,
                                          ctrlBrowseFileConvertVector.FileName,
                                          rawVectorParams.strPointTextureFile,
                                          rawVectorParams.strLocaleStr,
                                          strMetaDataFormat.Text,
                                          rawVectorParams.pSourceCoordinateSystem,
                                          ctrlRawVectorParams1.TargetGridCoordinateSystem,
                                          filterFieldIds,
                                          cgbIsCreateMetaData.Checked,
                                          rawVectorParams.dSimplificationTolerance,
                                          tbMinSizeFactor.GetFloat(),
                                          tbMaxSizeFactor.GetFloat(),
                                          chxbIsLiteVectorLayer.Checked,
                                          filterLayerAttributes,
                                          cbSaveLayerAttributes.Checked,
                                          rawVectorParams.fMinScale,
                                          rawVectorParams.fMaxScale);
            vectorConvertParams.eVersion = version;
            vectorConvertParams.pTilingScheme = mcTilingScheme;
            vectorConvertParams.uNumTilesInFileEdge = ntxNumTilesInFileEdge.GetUInt32();
            vectorConvertParams.eAutoStylingType = rawVectorParams.eAutoStylingType;
            vectorConvertParams.strCustomStylingFolder = rawVectorParams.strCustomStylingFolder;
            vectorConvertParams.StylingParams = rawVectorParams.StylingParams;
            vectorConvertParams.uMaxNumVerticesPerTile = rawVectorParams.uMaxNumVerticesPerTile;
            vectorConvertParams.uMaxNumVisiblePointObjectsPerTile = rawVectorParams.uMaxNumVisiblePointObjectsPerTile;
            vectorConvertParams.uMinPixelSizeForObjectVisibility = rawVectorParams.uMinPixelSizeForObjectVisibility;
            vectorConvertParams.fOptimizationMinScale = rawVectorParams.fOptimizationMinScale;

            if (IsHasClipRectValue())
            {
                vectorConvertParams.pClipRect = ctrlSMcBoxClipRect.GetBoxValue();
            }
            else
                vectorConvertParams.pClipRect = null;

            return vectorConvertParams;
        }

        private uint[] ConvertLabelStringToArray(string text)
        {
            string[] stringIds = text.Split(',');
            uint[] uintIds = new uint[stringIds.Length];
            for (int i = 0; i < stringIds.Length; i++)
                UInt32.TryParse(stringIds[i], out uintIds[i]);

            return uintIds;
        }

        private void btnAddVectorLayerTilesData_Click(object sender, EventArgs e)
        {
            try
            {
               m_MapProduction.GenerateVectorLayerTilesData(ctrlBrowseFolderConvertVector.FileName);
               MessageBox.Show("Add Vector Layer Tiles Data succeed", "Convert Vector Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GenerateVectorLayerTilesData", McEx);
            }
        }

        private bool IsHasClipRectValue()
        {
            return (ctrlSMcBoxClipRect.GetBoxValue().MaxVertex.x != 0 &&
                ctrlSMcBoxClipRect.GetBoxValue().MaxVertex.y != 0 &&
                ctrlSMcBoxClipRect.GetBoxValue().MinVertex.x != 0 &&
                ctrlSMcBoxClipRect.GetBoxValue().MinVertex.y != 0);
        }

        private void chxShowClipping_CheckedChanged(object sender, EventArgs e)
        {
            if (chxShowClipping.Checked == true)
            {
                if (IsHasClipRectValue())
                {
                    try
                    {
                        m_ClippingSchemeItem = DNMcRectangleItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                            DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                            DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT,
                                                                            DNERectangleDefinition._ERD_RECTANGLE_DIAGONAL_POINTS,
                                                                            0f,
                                                                            0f,
                                                                            DNELineStyle._ELS_DASH_DOT,
                                                                            new DNSMcBColor(0, 255, 0, 255),
                                                                            3f,
                                                                            null,
                                                                            new DNSMcFVector2D(0, -1),
                                                                            1f,
                                                                            DNEFillStyle._EFS_NONE);


                        DNSMcVector3D[] locationPoints = new DNSMcVector3D[2];
                        locationPoints[0].x = ctrlSMcBoxClipRect.GetBoxValue().MinVertex.x;
                        locationPoints[0].y = ctrlSMcBoxClipRect.GetBoxValue().MaxVertex.y;
                        locationPoints[1].x = ctrlSMcBoxClipRect.GetBoxValue().MaxVertex.x;
                        locationPoints[1].y = ctrlSMcBoxClipRect.GetBoxValue().MinVertex.y;

                        IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;

                        m_CliipingRectObj = DNMcObject.Create(activeOverlay,
                                                                m_ClippingSchemeItem,
                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                locationPoints,
                                                                false);

                        chxShowClipping.Text = "Unshow";
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                    }
                }
            }
            else
            {
                try
                {
                    if (m_CliipingRectObj != null)
                    {
                        m_CliipingRectObj.Remove();
                        m_CliipingRectObj.Dispose();
                        m_CliipingRectObj = null;

                        chxShowClipping.Text = "Show";
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Remove", McEx);
                }
            }
        }

        private void cbIsUseLayerAttributesFilter_CheckedChanged(object sender, EventArgs e)
        {
            tbLayerAttributesIdsFilter.Enabled = cbSaveLayerAttributes.Checked;
            label9.Enabled = cbSaveLayerAttributes.Checked;

            if (!cbSaveLayerAttributes.Checked)
            {
                tbLayerAttributesIdsFilter.Text = "";
            }
        }

        private void btnTilingScheme_Click(object sender, EventArgs e)
        {
            frmSTilingSchemeParams frmSTilingSchemeParams = new frmSTilingSchemeParams();
            if (mcTilingScheme != null)
                frmSTilingSchemeParams.STilingScheme = mcTilingScheme;

            if (frmSTilingSchemeParams.ShowDialog() == DialogResult.OK)
            {
                mcTilingScheme = frmSTilingSchemeParams.STilingScheme;
            }
        }

        private void dgvRawVectorLayers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == m_colSelect)
                    {
                        dgvRawVectorLayers.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                }

                /* DNSRawVectorParams pParams;
                 IDNMcGridCoordinateSystem ppTargetCoordinateSystem;
                 DNSTilingScheme ppTilingScheme;

                 IDNMcRawVectorMapLayer rawVectorMapLayer = (IDNMcRawVectorMapLayer)dgvRawVectorLayers.Rows[e.RowIndex].Tag; 

                 rawVectorMapLayer.GetCreateParams(out pParams, out ppTargetCoordinateSystem, out ppTilingScheme);

                 ctrlRawVectorParams1.RawVectorParams = pParams;
                 ctrlRawVectorParams1.TargetGridCoordinateSystem = ppTargetCoordinateSystem;
                 mcTilingScheme = ppTilingScheme;
                 mcETilingSchemeType = CtrlTilingSchemeParams.GetETilingSchemeType(ppTilingScheme);

                 ctrlBrowseFileConvertVector.FileName = pParams.strDataSource;*/
        }
       
        private void chxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_isInCellSelect)
            {
                m_isInSelectAll = true;
                for (int i = 0; i < dgvRawVectorLayers.RowCount; i++)
                {
                    DataGridViewCheckBoxCell cell = dgvRawVectorLayers[m_colSelect, i] as DataGridViewCheckBoxCell;
                    cell.Value = chxSelectAll.Checked;
                }
                
                m_isInSelectAll = false;
            }
        }

        private void dgvRawVectorLayers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!m_isInCellSelect && e.ColumnIndex == m_colSelect)
            {
                m_isInCellSelect = true;
                DataGridViewCheckBoxCell cell = dgvRawVectorLayers[e.ColumnIndex, e.RowIndex] as DataGridViewCheckBoxCell;
                bool isSelect = (bool)cell.Value;
                    IDNMcRawVectorMapLayer rawVectorMapLayer = (IDNMcRawVectorMapLayer)dgvRawVectorLayers.Rows[e.RowIndex].Tag;
                if(isSelect)
                {
                    if(!m_selectedLayers.Contains(rawVectorMapLayer))
                        m_selectedLayers.Add(rawVectorMapLayer);
                }
                else
                {
                    if(m_selectedLayers.Contains(rawVectorMapLayer))
                        m_selectedLayers.Remove(rawVectorMapLayer);
                }
                if (!m_isInSelectAll)
                {
                    if (!isSelect)
                        chxSelectAll.Checked = false;
                    else
                    {
                        bool all = true;
                        for (int i = 0; i < dgvRawVectorLayers.RowCount; i++)
                        {
                            DataGridViewCheckBoxCell cell2 = dgvRawVectorLayers[m_colSelect, i] as DataGridViewCheckBoxCell;
                            if (!((bool)cell2.Value))
                            {
                                all = false;
                                break;
                            }
                        }
                        chxSelectAll.Checked = all;
                    }
                }
                
                m_isInCellSelect = false;

                SetMultipleLayers(m_selectedLayers.Count > 1);
                // ctrlRawVectorParams1.SetDisableStyling(isMultiple ? ".Styling" : "");
                ctrlRawVectorParams1.Enabled = m_selectedLayers.Count  == 0;
                ctrlBrowseFileConvertVector.Enabled = m_selectedLayers.Count == 0;
                btnTilingScheme.Enabled = m_selectedLayers.Count == 0;

                if (m_selectedLayers.Count == 1)
                {
                    SetLayerData(m_selectedLayers[0]);
                }
            }
        }

       
    }
}
