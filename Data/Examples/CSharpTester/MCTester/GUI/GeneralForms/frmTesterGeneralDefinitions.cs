using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using MCTester.Managers;

namespace MCTester.General_Forms
{
    public partial class frmTesterGeneralDefinitions : Form
    {
        private bool isGridValuesChanged;
        private int m_colFontButton = 0;
        private int m_colFontName = 1;
        private int m_colFontBold = 2;
        private int m_colFontItalic = 3;
        private int m_colFileButton = 4;
        private int m_colFilePath = 5;

        public frmTesterGeneralDefinitions()
        {
            InitializeComponent();

            isGridValuesChanged = false;

            rdbMsgPendingUpdates.Checked = Manager_MCGeneralDefinitions.HasPendingChanges;
            ntxRenderInterval.SetInt(Manager_MCGeneralDefinitions.RenderInterval);
            chxUseBasicItemPropertiesOnly.Checked = Manager_MCGeneralDefinitions.UseBasicItemPropertiesOnly;

            DNEPendingUpdateType pendingUpdateType = Manager_MCGeneralDefinitions.UpdateTypeBitField;
            if ((pendingUpdateType & DNEPendingUpdateType._EPUT_ANY_UPDATE) == DNEPendingUpdateType._EPUT_ANY_UPDATE)
                chxANY_UPDATE.Checked = true;
            if ((pendingUpdateType & DNEPendingUpdateType._EPUT_GLOBAL_MAP) == DNEPendingUpdateType._EPUT_GLOBAL_MAP)
                chxGLOBAL_MAP.Checked = true;
            if ((pendingUpdateType & DNEPendingUpdateType._EPUT_OBJECTS) == DNEPendingUpdateType._EPUT_OBJECTS)
                chxOBJECT_DELAY.Checked = true;
            if ((pendingUpdateType & DNEPendingUpdateType._EPUT_TERRAIN) == DNEPendingUpdateType._EPUT_TERRAIN)
                chxTERRAIN_LOAD.Checked = true;
            if ((pendingUpdateType & DNEPendingUpdateType._EPUT_IMAGEPROCESS) == DNEPendingUpdateType._EPUT_IMAGEPROCESS)
                chxIMAGEPROCESS.Checked = true;
            if ((pendingUpdateType & DNEPendingUpdateType._EPUT_GRID) == DNEPendingUpdateType._EPUT_GRID)
                chxEPUT_GRID.Checked = true;

            chxGetRenderStatistics.Checked = Manager_MCGeneralDefinitions.GetRenderStatistics;


            try
            {
                MapCore.DNSLogFontToTtfFile[] arrDNSLogFontToTtfFile = DNMcLogFont.GetLogFontToTtfFileMap();
                if (arrDNSLogFontToTtfFile != null && arrDNSLogFontToTtfFile.Length > 0)
                {
                    int rowIndex = 0;
                    foreach (DNSLogFontToTtfFile fontToTtf in arrDNSLogFontToTtfFile)
                    {
                        DNSMcLogFont logFont = fontToTtf.LogFont.LogFont;
                        InsertNewValue(logFont.lfFaceName, logFont.lfWeight == 700, logFont.lfItalic == 1, rowIndex);
                        InsertNewValue(fontToTtf.strTtfFileFullPathName, m_colFilePath, rowIndex);
                        rowIndex++;
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcLogFont.GetLogFontToTtfFileMap", McEx);
            }

            rdbMsgPendingUpdates_CheckedChanged(null, null);
           
            try
            {
                DNSMcKeyStringValue[] mcKeyStringValues = DNMcGridGeneric.GetSupportedSRIDs();
                foreach (DNSMcKeyStringValue i in mcKeyStringValues)
                {
                    dataGridView1.Rows.Add(i.strKey, i.strValue);
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcGridGeneric.GetSupportedSRIDs()", McEx);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Manager_MCGeneralDefinitions.RenderInterval = ntxRenderInterval.GetInt32();

            if (rdbMsgNone.Checked == true)
            {
                Manager_MCGeneralDefinitions.HasPendingChanges = false;
            }
            if (rdbMsgPendingUpdates.Checked == true)
            {
                DNEPendingUpdateType uUpdateTypeBitField = 0;

                if (chxANY_UPDATE.Checked == true)
                    uUpdateTypeBitField |= DNEPendingUpdateType._EPUT_ANY_UPDATE;
                if (chxGLOBAL_MAP.Checked == true)
                    uUpdateTypeBitField |= DNEPendingUpdateType._EPUT_GLOBAL_MAP;
                if (chxOBJECT_DELAY.Checked == true)
                    uUpdateTypeBitField |= DNEPendingUpdateType._EPUT_OBJECTS;
                if (chxTERRAIN_LOAD.Checked == true)
                    uUpdateTypeBitField |= DNEPendingUpdateType._EPUT_TERRAIN;
                if (chxIMAGEPROCESS.Checked == true)
                    uUpdateTypeBitField |= DNEPendingUpdateType._EPUT_IMAGEPROCESS;
                if (chxEPUT_GRID.Checked == true)
                    uUpdateTypeBitField |= DNEPendingUpdateType._EPUT_GRID;

                Manager_MCGeneralDefinitions.HasPendingChanges = true;
                Manager_MCGeneralDefinitions.UpdateTypeBitField = uUpdateTypeBitField;

            }

            // Activate Render Statistics
            Manager_MCGeneralDefinitions.GetRenderStatistics = chxGetRenderStatistics.Checked;

            Manager_MCGeneralDefinitions.CharacterSpacing = ntxCharacterSpacing.GetUInt32();
            Manager_MCGeneralDefinitions.NumAntialiasingAlphaLevels = ntxNumAntialiasingAlphaLevels.GetUInt32();

            if (isGridValuesChanged)   // if the grid data changed
            {
                MapCore.DNSLogFontToTtfFile[] arrDNSLogFontToTtfFile = new DNSLogFontToTtfFile[0];

                int rowsCount = dgvLogFontToTtfFileMap.Rows.Count;

                if (dgvLogFontToTtfFileMap.Rows[rowsCount - 1].IsNewRow) // remove new empty row
                    rowsCount--;
                arrDNSLogFontToTtfFile = new DNSLogFontToTtfFile[rowsCount];
                foreach (DataGridViewRow row in dgvLogFontToTtfFileMap.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        DNSLogFontToTtfFile fontToTTF = new DNSLogFontToTtfFile();
                        object objPath = row.Cells[m_colFilePath].Value;
                        if (objPath != null)
                            fontToTTF.strTtfFileFullPathName = objPath.ToString();
                        if (row.Cells[m_colFontName].Value != null)
                        {
                            fontToTTF.LogFont = new DNMcVariantLogFont();
                            fontToTTF.LogFont.LogFont = new DNSMcLogFont();
                            fontToTTF.LogFont.LogFont.lfFaceName = row.Cells[m_colFontName].Value.ToString();
                            fontToTTF.LogFont.LogFont.lfWeight = ((bool)row.Cells[m_colFontBold].Value) ? 700 : 400;
                            fontToTTF.LogFont.LogFont.lfItalic = ((bool)row.Cells[m_colFontItalic].Value) ? (byte)1 : (byte)0;

                            arrDNSLogFontToTtfFile[row.Index] = fontToTTF;
                        }
                        else
                        {
                            MessageBox.Show(string.Concat("Missing font value to row ", row.Index), "Missing Font Value");
                            return;
                        }
                    }
                }

                try
                {
                    DNMcLogFont.SetLogFontToTtfFileMap(arrDNSLogFontToTtfFile);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcLogFont.GetLogFontToTtfFileMap", McEx);
                }
            }

            Manager_MCGeneralDefinitions.UseBasicItemPropertiesOnly = chxUseBasicItemPropertiesOnly.Checked;

            this.Close();
        }

        private void frmTesterGeneralDefinitions_Load(object sender, EventArgs e)
        {
            ntxCharacterSpacing.SetUInt32(Manager_MCGeneralDefinitions.CharacterSpacing);
            ntxNumAntialiasingAlphaLevels.SetUInt32(Manager_MCGeneralDefinitions.NumAntialiasingAlphaLevels);
        }

        private void dgvLogFontToTtfFileMap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Font newFont;
            string fontDesc = string.Empty;

            if (e.ColumnIndex == m_colFontButton)  // open 'font' dialog
            {
                if (dgvLogFontToTtfFileMap[0, e.RowIndex] != null && dgvLogFontToTtfFileMap[0, e.RowIndex].Tag != null)
                    openFontDialog.Font = (Font)dgvLogFontToTtfFileMap[0, e.RowIndex].Tag;
                
                if (openFontDialog.ShowDialog() == DialogResult.OK)
                {
                    newFont = openFontDialog.Font;
                    InsertNewValue(newFont.Name, newFont.Bold, newFont.Italic, e.RowIndex);
                    dgvLogFontToTtfFileMap[0, e.RowIndex].Tag = newFont;
                }
            }
            else if (e.ColumnIndex == m_colFileButton) // open 'open file' dialog
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "TTF Files|*.ttf|All Files|*.*"; 
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    InsertNewValue(openFileDialog.FileName, m_colFilePath, e.RowIndex);
                }
            }
        }

        private void InsertNewValue(string fontName, bool isBold, bool isItalic, int rowIndex)
        {
            if (rowIndex == dgvLogFontToTtfFileMap.RowCount - 1 || dgvLogFontToTtfFileMap.Rows[dgvLogFontToTtfFileMap.RowCount - 1].IsNewRow == false)
            {
                dgvLogFontToTtfFileMap.Rows.Add();
            }

            dgvLogFontToTtfFileMap[m_colFontName, rowIndex].Value = fontName;
            dgvLogFontToTtfFileMap[m_colFontName, rowIndex].ToolTipText = fontName;
            dgvLogFontToTtfFileMap[m_colFontBold, rowIndex].Value = isBold;
            dgvLogFontToTtfFileMap[m_colFontItalic, rowIndex].Value = isItalic;

        }


        private void InsertNewValue(string value,int colIndex, int rowIndex)
        {
            if (rowIndex == dgvLogFontToTtfFileMap.RowCount - 1 || dgvLogFontToTtfFileMap.Rows[dgvLogFontToTtfFileMap.RowCount - 1].IsNewRow == false)
            {
                dgvLogFontToTtfFileMap.Rows.Add();
            }
            
            DataGridViewCell cell = dgvLogFontToTtfFileMap[colIndex, rowIndex];

              if (value.Length > 40)
                  cell.Value = value.Remove(40) + "...";
              else
                  cell.Value = value;
            cell.ToolTipText = value;
        }

        private string GetFontDesc(Font font)
        {
            string retValue = string.Empty;
            
            retValue = font.FontFamily.Name;
            if(font.Bold)
                retValue = string.Concat(retValue, ", Bold");
            if (font.Italic)
                retValue = string.Concat(retValue, ", Italic");

            return retValue;
        }

        private void dgvLogFontToTtfFileMap_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            isGridValuesChanged = true;
        }

        private void rdbMsgPendingUpdates_CheckedChanged(object sender, EventArgs e)
        {
            gbPendingUpdateTypes.Enabled = rdbMsgPendingUpdates.Checked;
        }
    }
}
