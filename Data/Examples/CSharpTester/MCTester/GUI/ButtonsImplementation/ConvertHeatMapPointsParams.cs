using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCTester.ButtonsImplementation
{
    public partial class ConvertHeatMapPointsParams : Form
    {

        public struct HeatMapPointsParams
        {
            public byte intensity;
            public int noPoints;
            public int noLocations;
        }

        HeatMapPointsParams[] m_PointsParams;
        public ConvertHeatMapPointsParams()
        {
            InitializeComponent();
        }

        public ConvertHeatMapPointsParams(HeatMapPointsParams[] _PointsParams):this()
        {
            PointsParams = _PointsParams;
        }

        public HeatMapPointsParams[] PointsParams
        {
            get
            {
                return m_PointsParams;
            }
            set
            {
                if (value != null)
                {
                    dgvPointsParams.Rows.Clear();
                    foreach (HeatMapPointsParams pointsParams in value)
                    {
                        dgvPointsParams.Rows.Add(pointsParams.intensity.ToString(), pointsParams.noPoints.ToString(), pointsParams.noLocations.ToString());
                    }
                }
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            m_PointsParams = new HeatMapPointsParams[dgvPointsParams.Rows.Count-1];
            HeatMapPointsParams pointParams;
            foreach (DataGridViewRow row in dgvPointsParams.Rows)
            {
                if (row.IsNewRow == false)
                {
                    pointParams = new HeatMapPointsParams();
                    pointParams.intensity = Convert.ToByte(row.Cells[0].Value);
                    pointParams.noPoints = Convert.ToInt32(row.Cells[1].Value);
                    pointParams.noLocations = Convert.ToInt32(row.Cells[2].Value);

                    m_PointsParams[row.Index] = pointParams;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dgvPointsParams_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgvPointsParams_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >=0) // paint rect on map
            {
                try
                {
                    object cellValue = dgvPointsParams[0, e.RowIndex].Value;
                    byte intensity;
                    if(cellValue != null)
                        intensity = Convert.ToByte(cellValue);
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Enter number between 0-255!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgvPointsParams[0, e.RowIndex].Value = 0;
                }
            }
        }
    }
}
