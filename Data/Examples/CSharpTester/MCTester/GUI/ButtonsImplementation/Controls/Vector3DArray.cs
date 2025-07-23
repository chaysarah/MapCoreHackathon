using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;

namespace MCTester.Controls
{
    public partial class Vector3DArray : UserControl
    {
        public Vector3DArray()
        {
            InitializeComponent();
        }

        public Vector3DArray(DNSArrayProperty<DNSMcVector3D> value)
            : this()
        {
            Vector3DArrayPropertyValue = value;
        }

        public Vector3DArray(DNSArrayProperty<DNSMcFVector3D> value)
            : this()
        {
            FVector3DArrayPropertyValue = value;
        }

        public DNSArrayProperty<DNSMcVector3D> Vector3DArrayPropertyValue
        {
            get
            {
                DNSArrayProperty<DNSMcVector3D> retValue = new DNSArrayProperty<DNSMcVector3D>();
                retValue.aElements = new DNSMcVector3D[dgvVector3D.RowCount - 1];
                
                FillEmptyCells();
                
                for (int i = 0; i < dgvVector3D.RowCount; i++)
                {
                    if (dgvVector3D.Rows[i].IsNewRow == false)
                    {
                        retValue.aElements[i] = new DNSMcVector3D(GetDouble(dgvVector3D[1, i].Value.ToString()), GetDouble(dgvVector3D[2, i].Value.ToString()), GetDouble(dgvVector3D[3, i].Value.ToString()));
                    }
                }
                return retValue;
            }
            set
            {
                if (value.aElements != null)
                {
                    dgvVector3D.Rows.Clear();
                    foreach (DNSMcVector3D vector3D in value.aElements)
                    {
                        dgvVector3D.Rows.Add("", vector3D.x, vector3D.y, vector3D.z);
                    }
                    dgvVector3D.ClearSelection();
                }
            }
        }

        public DNSArrayProperty<DNSMcFVector3D> FVector3DArrayPropertyValue
        {
            get
            {
                DNSArrayProperty<DNSMcFVector3D> retValue = new DNSArrayProperty<DNSMcFVector3D>();
                retValue.aElements = new DNSMcFVector3D[dgvVector3D.RowCount - 1];
                
                FillEmptyCells();
                
                for (int i = 0; i < dgvVector3D.RowCount; i++)
                {
                    if (dgvVector3D.Rows[i].IsNewRow == false)
                    {
                        retValue.aElements[i] = new DNSMcFVector3D(GetFloat(dgvVector3D[1, i].Value.ToString()), GetFloat(dgvVector3D[2, i].Value.ToString()), GetFloat(dgvVector3D[3, i].Value.ToString()));
                    }
                }
                return retValue;
            }
            set
            {
                if (value.aElements != null)
                {
                    dgvVector3D.Rows.Clear();
                    foreach (DNSMcFVector3D vector3D in value.aElements)
                    {
                        dgvVector3D.Rows.Add("", vector3D.x, vector3D.y, vector3D.z);
                    }
                    dgvVector3D.ClearSelection();
                }
            }
        }

        private void FillEmptyCells()
        {
            for (int i = 0; i < dgvVector3D.RowCount; i++)
            {
                if (dgvVector3D.Rows[i].IsNewRow == false)
                {
                    for (int j = 0; j < dgvVector3D.ColumnCount; j++)
                    {
                        if (dgvVector3D[j, i].Value == null)
                            dgvVector3D[j, i].Value = "0";
                    }
                }
            }
        }

        public float GetFloat(string value)
        {
            try
            {
                float fParam;
                if (String.Compare(value, "MAX", true) == 0)
                    fParam = float.MaxValue;
                else
                    fParam = float.Parse(value);

                return fParam;
            }
            catch
            {
                return 0f;
            }
        }

        public double GetDouble(string value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0;
            }
        }

        private void dgvVector3D_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgvVector3D.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex).ToString();
        }

        public void ResetGrid()
        {
            dgvVector3D.Rows.Clear();
        }
    }
}
