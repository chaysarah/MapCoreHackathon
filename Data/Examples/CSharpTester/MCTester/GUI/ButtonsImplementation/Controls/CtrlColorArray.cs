using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCTester.Controls
{
    public partial class CtrlColorArray : UserControl
    {
        public CtrlColorArray()
        {
            InitializeComponent();
            btnAdd_Click(null, null);
            numUDAlpha.Value = 255;
        }

        List<KeyValuePair<Color,int>> m_userColorsAndAlpha;

        public List<KeyValuePair<Color, int>> UserColorsAndAlpha
        {
            get
            {
                m_userColorsAndAlpha = new List<KeyValuePair<Color, int>>();
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    DataGridViewCellStyle cellStyle = row.Cells[0].Style;
                    if (cellStyle.BackColor.IsEmpty == false)
                    {
                        m_userColorsAndAlpha.Add(new KeyValuePair<Color, int>(cellStyle.BackColor, Convert.ToInt32(row.Cells[1].Value)));
                    }
                }
                return m_userColorsAndAlpha;
            }
        }

        private void PaintRow(int row,Color color, string alpha)
        {
            dataGridView2[0, row].Style = new DataGridViewCellStyle() { BackColor = color};
            dataGridView2[1, row] = new DataGridViewTextBoxCell() { Value = alpha };

        }
       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool addRow = false;
           
            if (dataGridView2.Rows.Count > 0)
            {
                DataGridViewCellStyle cellStyle = dataGridView2[0, 0].Style;
                if (cellStyle.BackColor.IsEmpty == false)
                {
                    addRow = true;
                }
                else
                {
                    dataGridView2.Rows[0].Selected = true;
                    dataGridView2.CurrentCell = dataGridView2.Rows[0].Cells[0];
                    ChangeRowToSelected(0);
                }
            }
            else
            {
                addRow = true;
            }

            if (addRow)
            {
                dataGridView2.Rows.Insert(0, 1);
                if (dataGridView2.Rows.Count > 1)
                    dataGridView2[0, dataGridView2.Rows.Count -1].Selected = false;
                dataGridView2[1, 0].Value = "255";
                dataGridView2.Rows[0].Selected = true;
                dataGridView2.CurrentCell = dataGridView2.Rows[0].Cells[0];
            }
        }

        private void picBxColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                picBxColor.BackColor = Color.FromArgb(255, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
            }
        }

        private void dataGridView2_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                ChangeRowToSelected(e.Row.Index);
            }
        }

        private void ChangeRowToSelected(int rowIndex)
        {
            DataGridViewRow row = dataGridView2.Rows[rowIndex];
            picBxColor.BackColor = row.Cells[0].Style.BackColor;
            numUDAlpha.Value = Convert.ToDecimal(row.Cells[1].Value);
        }
        private void btnSet_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                int selectedRow = dataGridView2.SelectedRows[0].Index;
                if (!UserColorsAndAlpha.Any(x => x.Key == picBxColor.BackColor && x.Value.ToString() == numUDAlpha.Value.ToString()))
                {
                    PaintRow(selectedRow, picBxColor.BackColor, numUDAlpha.Value.ToString());
                    dataGridView2.Rows[selectedRow].Selected = false;
                    dataGridView2[0, selectedRow].Selected = false;
                    btnAdd_Click(null, null);
                }
                else
                    MessageBox.Show("The selected color already exist");
            }
            else
                MessageBox.Show("Select one row to set");
        }
    }
}
