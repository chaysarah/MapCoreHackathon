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

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucCodeLayer : ucRasterLayer, IUserControlItem
    {
        private IDNMcCodeMapLayer m_CurrentObject;

        public ucCodeLayer()
        {
            InitializeComponent();
            dgvColorTable.Rows.Clear();

        }

        protected override void SaveItem()
        {
            base.SaveItem();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcCodeMapLayer)aItem;
            base.LoadItem(aItem);

            GetColorTable();
        }

        public void GetColorTable()
        {
            try
            {
                DNSCodeMapData[] codeMapDatas = m_CurrentObject.GetColorTable();
                LoadColorTable(codeMapDatas);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetColorTable", McEx);
            }
        }

        private void LoadColorTable(DNSCodeMapData[] codeMapDatas)
        {
            dgvColorTable.Rows.Clear();
            if (codeMapDatas != null && codeMapDatas.Length != 0)
            {
                for (int i = 0; i < codeMapDatas.Length; i++)
                {
                    DNSMcBColor mcColor = codeMapDatas[i].CodeColor;
                    Color color = Color.FromArgb(mcColor.r, mcColor.g, mcColor.b);
                   
                    dgvColorTable.Rows.Add(codeMapDatas[i].uCode, null);
                    (dgvColorTable[1, i] as DataGridViewButtonCell).FlatStyle = FlatStyle.Popup;
                    dgvColorTable[1, i].Style.BackColor = color;
                }
                dgvColorTable.ClearSelection();
            }
        }

        public void SetColorTable(DNSCodeMapData[] codeMapDatas)
        {
            try
            {
                m_CurrentObject.SetColorTable(codeMapDatas);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetColorTable", McEx);
            }
        }

        #endregion

        private void btnSetColorTable_Click(object sender, EventArgs e)
        {
            //loop on table color
            List<DNSCodeMapData> listColors = new List<DNSCodeMapData>();
            try
            {
                for (int i = 0; i < dgvColorTable.RowCount; i++)
                {
                    if (dgvColorTable.Rows[i].IsNewRow == false)
                    {
                        Color color = dgvColorTable[1, i].Style.BackColor;

                        DNSMcBColor mcBColor = new DNSMcBColor(color.R, color.G, color.B, 255);
                        DNSCodeMapData colorCode = new DNSCodeMapData();

                        colorCode.uCode = Convert.ToUInt32(dgvColorTable[0, i].Value);
                        colorCode.CodeColor = mcBColor;

                        listColors.Add(colorCode);
                    }
                }
                SetColorTable(listColors.ToArray());
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void dgvColorTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)  // color column
            {
                ColorDialog colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    DataGridViewButtonCell buttCell = dgvColorTable[1, e.RowIndex] as DataGridViewButtonCell;
                    buttCell.FlatStyle = FlatStyle.Popup;
                    buttCell.Style.BackColor = colorDialog.Color;
                }
            }
        }

        private void btnGetColorTable_Click(object sender, EventArgs e)
        {
            GetColorTable();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvColorTable.Rows.Clear();
        }
    }
}
