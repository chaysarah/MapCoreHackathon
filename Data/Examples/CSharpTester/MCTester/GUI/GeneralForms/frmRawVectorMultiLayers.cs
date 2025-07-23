using MapCore;
using MapCore.Common;
using MCTester.Managers.MapWorld;
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
using UnmanagedWrapper;

namespace MCTester.General_Forms
{
    public partial class frmRawVectorMultiLayers : Form
    {
        private DNSDataSourceSubLayersProperties m_VectorDataSourceProperties;
        string m_StrDataSource;
        private int m_colSelectRow = 0;
        private int m_colPath = 6;
        private int m_colSelectPath = 7;
        private bool m_isEnableStyling;
        private bool m_isMultiSelect;

        public frmRawVectorMultiLayers(string strDataSource, bool isEnableStyling = true, bool isMultiSelect = true)
        {
            InitializeComponent();

            m_StrDataSource = strDataSource;
            m_isEnableStyling = isEnableStyling;
            m_isMultiSelect = isMultiSelect;

            dgvLayers.Columns[m_colPath].Visible = isEnableStyling;
            dgvLayers.Columns[m_colSelectPath].Visible = isEnableStyling;
            this.Width = isEnableStyling ? this.Width : 1200;
            dgvLayers.Width = isEnableStyling ? dgvLayers.Width : 900;
            btnOK.Location = new Point(isEnableStyling ? btnOK.Location.X : 760, btnOK.Location.Y);
            chxCreateSingleVectorLayerFromAllSublayers.Location = new Point(isEnableStyling ? chxCreateSingleVectorLayerFromAllSublayers.Location.X : 510, chxCreateSingleVectorLayerFromAllSublayers.Location.Y);
            chxSelectAll.Visible = isMultiSelect;

            LoadDGV();
        }

        private void LoadDGV()
        {
            dgvLayers.Rows.Clear();

            if (m_StrDataSource != "")
            {
                try
                {
                    m_VectorDataSourceProperties = DNMcRawVectorMapLayer.GetDataSourceSubLayersProperties(m_StrDataSource, chxSuffixByName.Checked);
                    if (m_VectorDataSourceProperties.aLayersProperties != null && m_VectorDataSourceProperties.aLayersProperties.Length > 0)
                    {
                        int nLength = m_VectorDataSourceProperties.aLayersProperties.Length;
                        int i = 0;
                        foreach (DNSDataSourceSubLayerProperties prop in m_VectorDataSourceProperties.aLayersProperties)
                        {
                            dgvLayers.Rows.Add(false,
                                prop.uLayerIndex.ToString(),
                                prop.strLayerName,
                                prop.eExtendedGeometry.ToString(),
                                prop.strMultiGeometriesSuffix,
                                prop.uNumVectorItems.ToString(),
                                ""
                            );
                            dgvLayers.Rows[i].Tag = prop.strMultiGeometriesSuffix;
                            i++;
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                     Utilities.ShowErrorMessage("DNMcRawVectorMapLayer.GetDataSourceSubLayersProperties", McEx);
                }
            }
        }

        private void btnSelectLayers_Click(object sender, EventArgs e)
        {
            SelectLayersFromUser();
        }

        private void SelectLayersFromUser()
        {
            string selectedLayers = m_StrDataSource;

            if (m_isMultiSelect)
                selectedLayers += Manager_MCLayers.RawVectorSplitStr;

            //  Dictionary<string,string> dicSuffixStylingFiles = new Dictionary<string, string>();
            bool isRowSelected = false;
            for (int i = 0; i < dgvLayers.RowCount; i++)
            {
                DataGridViewCheckBoxCell cell = dgvLayers[m_colSelectRow, i] as DataGridViewCheckBoxCell;
                if(((bool)cell.Value))
                {
                    isRowSelected = true;
                    DataGridViewRow row = dgvLayers.Rows[i];
                    string subLayerName = row.Tag.ToString();
                    string key = m_StrDataSource + subLayerName;
                    string StylingFile = "";
                    string folder = Path.GetDirectoryName(m_StrDataSource);

                    if (row.Cells[m_colPath].Value != null && row.Cells[m_colPath].Value.ToString() != "")
                    {
                        StylingFile = row.Cells[m_colPath].Value.ToString();
                    }
                   
                    selectedLayers += subLayerName + (StylingFile != "" ? Manager_MCLayers.RawVectorStylingSplitStr + StylingFile : "") + Manager_MCLayers.RawVectorLayersSplitChar;
                }
            }
            DialogResult dialogResult = DialogResult.OK; 
            if(!isRowSelected)
            {
                dialogResult = MessageBox.Show("No Row Selected, Are You Sure To Continue?", "Take Selected Rows", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            }
            if (dialogResult == DialogResult.OK)
            {
                SelectLayers = selectedLayers.TrimEnd(Manager_MCLayers.RawVectorSplitChar);
                if (!m_isMultiSelect)
                    SelectLayers = selectedLayers.TrimEnd(Manager_MCLayers.RawVectorLayersSplitChar);
                DialogResult = DialogResult.OK;
                this.Close();
            }
            
        }

        public String SelectLayers
        { set; get; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (chxCreateSingleVectorLayerFromAllSublayers.Checked)
            {
                DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
            {
               SelectLayersFromUser();
            }
        }

        private void dgvLayers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == m_colSelectPath && m_isEnableStyling)  // button select file column
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Styling files (*.sld, *.lyrx)|*.sld;*.lyrx|All files (*.*)|*.*";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        dgvLayers[m_colPath, e.RowIndex].Value = openFileDialog.FileName;
                    }
                }
                else if (e.ColumnIndex == m_colSelectRow)
                {
                    dgvLayers.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void dgvLayers_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgvLayers.Rows[e.RowIndex].Cells[m_colSelectPath].Value = "...";
        }

        private void dgvLayers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //SelectLayersFromUser();
        }

        private bool m_isInSelectAll = false;
        private bool m_isInCellSelect = false;

        private void chxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_isInCellSelect)
            {
                m_isInSelectAll = true;
                for (int i = 0; i < dgvLayers.RowCount; i++)
                {
                    DataGridViewCheckBoxCell cell = dgvLayers[m_colSelectRow, i] as DataGridViewCheckBoxCell;
                    cell.Value = chxSelectAll.Checked;
                }
                m_isInSelectAll = false;
            }
        }

        private void dgvLayers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(!m_isInCellSelect && !m_isInSelectAll && e.ColumnIndex == m_colSelectRow)
            {
                m_isInCellSelect = true;
                DataGridViewCheckBoxCell cell = dgvLayers[e.ColumnIndex, e.RowIndex] as DataGridViewCheckBoxCell;
                bool isSelect = (bool)cell.Value;

                if (m_isMultiSelect)
                {
                    if (!isSelect)
                        chxSelectAll.Checked = false;
                    else
                    {
                        bool all = true;
                        for (int i = 0; i < dgvLayers.RowCount; i++)
                        {
                            DataGridViewCheckBoxCell cell2 = dgvLayers[m_colSelectRow, i] as DataGridViewCheckBoxCell;
                            if (!((bool)cell2.Value))
                            {
                                all = false;
                                break;
                            }
                        }
                        chxSelectAll.Checked = all;
                    }
                }
                else if (isSelect)
                {
                    for (int j = 0; j < dgvLayers.RowCount; j++)
                    {
                        if (j != e.RowIndex)
                        {
                            DataGridViewCheckBoxCell cell3 = dgvLayers[m_colSelectRow, j] as DataGridViewCheckBoxCell;
                            if((bool)cell3.Value == true)
                                cell3.Value = false;
                        }
                    }

                }
                m_isInCellSelect = false;
            }
        }

        private void chxSuffixByName_CheckedChanged(object sender, EventArgs e)
        {
            LoadDGV();
        }

        private void chxCreateSingleVectorLayerFromAllSublayers_CheckedChanged(object sender, EventArgs e)
        {
            dgvLayers.Columns[m_colSelectRow].ReadOnly = chxCreateSingleVectorLayerFromAllSublayers.Checked;
            
            dgvLayers.Columns[m_colSelectRow].DefaultCellStyle.BackColor = chxCreateSingleVectorLayerFromAllSublayers.Checked ? Color.LightGray : Color.Empty ;
            dgvLayers.Columns[m_colSelectRow].DefaultCellStyle.ForeColor = chxCreateSingleVectorLayerFromAllSublayers.Checked ? Color.DarkGray : Color.Empty ;

            for (int i = 0; i < dgvLayers.RowCount; i++)
            {
                DataGridViewCheckBoxCell cell = dgvLayers[m_colSelectRow, i] as DataGridViewCheckBoxCell;
                cell.FlatStyle = chxCreateSingleVectorLayerFromAllSublayers.Checked ? FlatStyle.Flat : FlatStyle.Standard;
            }
            chxSelectAll.Enabled = !chxCreateSingleVectorLayerFromAllSublayers.Checked;

            ChangeUIStylingColumns(chxCreateSingleVectorLayerFromAllSublayers.Checked);
        }

        private void ChangeUIStylingColumns(bool isReadonly)
        {
            dgvLayers.Columns[m_colPath].ReadOnly = isReadonly;
            dgvLayers.Columns[m_colPath].DefaultCellStyle.BackColor = isReadonly ? Color.LightGray : Color.Empty;
            dgvLayers.Columns[m_colPath].DefaultCellStyle.ForeColor = isReadonly ? Color.DarkGray : Color.Empty;
            dgvLayers.Columns[m_colSelectPath].DefaultCellStyle.BackColor = isReadonly ? Color.LightGray : Color.Empty;
            dgvLayers.Columns[m_colSelectPath].DefaultCellStyle.ForeColor = isReadonly ? Color.DarkGray : Color.Empty;

            for (int i = 0; i < dgvLayers.RowCount; i++)
            {
                DataGridViewButtonCell cell = dgvLayers[m_colSelectPath, i] as DataGridViewButtonCell;
                cell.FlatStyle = isReadonly ? FlatStyle.Flat : FlatStyle.Standard;
            }
        }

        private void dgvLayers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
             if (e.ColumnIndex == 6 )
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
        }
    }
}
