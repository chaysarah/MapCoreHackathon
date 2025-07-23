using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCTester.MapWorld;
using MapCore;

namespace MCTester.Controls
{
    public partial class ColorArray : UserControl
    {
        public ColorArray()
        {
            InitializeComponent(); 
            
            DataGridViewComboBoxColumn colCombo = (dgvColors.Columns[2] as DataGridViewComboBoxColumn);
            colCombo.DataSource = Choice.GetChoices(255);
            colCombo.DisplayMember = "Name";  // the Name property in Choice class
            colCombo.ValueMember = "Value";
        }

        public ColorArray(DNSArrayProperty<DNSMcBColor> value)
            : this()
        {
            BColorsPropertyValue = value;
        }

        public DNSArrayProperty<DNSMcBColor> BColorsPropertyValue
        {
            get
            {
                DNSArrayProperty<DNSMcBColor> retValue = new DNSArrayProperty<DNSMcBColor>();
                retValue.aElements = GetColors();
                return retValue;
            }
            set
            {
                SetColors(value.aElements);
            }
        }

        public void SetColors(DNSMcBColor[] mcBColors)
        {
            dgvColors.Rows.Clear();
            if (mcBColors != null)
            {
                int index = 0;
                foreach (DNSMcBColor mcColor in mcBColors)
                {
                    Color color = Color.FromArgb(mcColor.r, mcColor.g, mcColor.b);
                    dgvColors.Rows.Add("", null, (int)mcColor.a);

                    (dgvColors[1, index] as DataGridViewButtonCell).FlatStyle = FlatStyle.Popup;
                    dgvColors[1, index].Style.BackColor = color;
                    index++;
                }
                dgvColors.ClearSelection();
            }
        }

        public DNSMcBColor[] GetColors()
        {
            DNSMcBColor[] retValue = new DNSMcBColor[dgvColors.RowCount - 1];

            for (int i = 0; i < dgvColors.RowCount; i++)
            {
                if (dgvColors.Rows[i].IsNewRow == false)
                {
                    Color color = dgvColors[1, i].Style.BackColor;
                    int alpha = (int)dgvColors[2, i].Value;

                    retValue[i].a = (byte)alpha;
                    retValue[i].b = (byte)color.B;
                    retValue[i].r = (byte)color.R;
                    retValue[i].g = (byte)color.G;
                }
            }
            return retValue;
        }
        private void dgvColors_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)  // color column
            {
                ColorDialog colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    if (e.RowIndex == dgvColors.RowCount - 1 || dgvColors.Rows[dgvColors.RowCount - 1].IsNewRow == false)
                    {
                        dgvColors.Rows.Add();
                    }
                    DataGridViewButtonCell buttCell = dgvColors[1, e.RowIndex] as DataGridViewButtonCell;
                    buttCell.FlatStyle = FlatStyle.Popup;
                    buttCell.Style.BackColor = colorDialog.Color;
                    if (dgvColors[2, e.RowIndex].Value == null)
                    {
                        ((DataGridViewComboBoxCell)dgvColors[2, e.RowIndex]).Value = 255;
                    }
                }
            }
        }

        private void dgvColors_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgvColors.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex).ToString();
        }

        public void ResetGrid()
        {
            dgvColors.Rows.Clear();
        }
    }
}
