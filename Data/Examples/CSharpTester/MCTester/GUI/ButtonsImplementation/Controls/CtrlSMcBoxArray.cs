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

namespace MCTester.Controls
{
    public partial class CtrlSMcBoxArray : UserControl
    {
        private bool m_isClickX = true;
        private int m_currentRow = -1;
        private Color m_color;

        public CtrlSMcBoxArray()
        {
            InitializeComponent();
            m_color = dataGridView1.ForeColor;
        }

        public void ChangeForeColor(bool isEnabled)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = isEnabled? m_color: Color.Gray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.DefaultCellStyle.ForeColor = isEnabled ? m_color : Color.Gray;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 3 || e.ColumnIndex == 7) && e.RowIndex < dataGridView1.RowCount)
            {
                m_isClickX = (e.ColumnIndex == 3);
                m_currentRow = e.RowIndex;
                ctrlSamplePoint.CtrlSamplePoint_MouseClick(null, null);
            }
        }

        private void ctrl3DPoint_EnabledChanged(object sender, EventArgs e)
        {
            if (m_currentRow >= 0)
            {
                if (m_currentRow == dataGridView1.RowCount - 1 || dataGridView1.Rows[dataGridView1.RowCount - 1].IsNewRow == false)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.CurrentCell = dataGridView1[0, m_currentRow];
                }
                if (m_isClickX)
                {
                    dataGridView1.Rows[m_currentRow].Cells[0].Value = ctrl3DPoint.GetVector3D().x;
                    dataGridView1.Rows[m_currentRow].Cells[1].Value = ctrl3DPoint.GetVector3D().y;
                    dataGridView1.Rows[m_currentRow].Cells[2].Value = ctrl3DPoint.GetVector3D().z;
                }
                else
                {
                    dataGridView1.Rows[m_currentRow].Cells[4].Value = ctrl3DPoint.GetVector3D().x;
                    dataGridView1.Rows[m_currentRow].Cells[5].Value = ctrl3DPoint.GetVector3D().y;
                    dataGridView1.Rows[m_currentRow].Cells[6].Value = ctrl3DPoint.GetVector3D().z;
                }
            }
        }

        public DNSMcBox[] GetBoxArray()
        {
            List<DNSMcBox> clipRects = new List<DNSMcBox>();
            for (int i = 0; i < dataGridView1.RowCount -1; i++)
            {
                clipRects.Add(new DNSMcBox(
                    ParseStringToDouble(0, i),
                    ParseStringToDouble(1, i),
                    ParseStringToDouble(2, i),
                    ParseStringToDouble(4, i),
                    ParseStringToDouble(5, i),
                    ParseStringToDouble(6, i)));
            }
            return clipRects.ToArray();
        }

        private double ParseStringToDouble(int col,int row)
        {
            double dblValue = 0;
            DataGridViewCell cell = dataGridView1[col, row];
            if (cell != null && cell.Value != null)
            {
                string strValue = cell.Value.ToString();
                if (Double.TryParse(strValue, out dblValue))
                    return dblValue;
            }
            return 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
    }
}
