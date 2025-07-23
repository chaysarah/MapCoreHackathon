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
    public partial class Vector2DArray : UserControl
    {
        public Vector2DArray()
        {
            InitializeComponent();
        }

        public Vector2DArray(DNSArrayProperty<DNSMcVector2D> value)
            : this()
        {
            Vector2DArrayPropertyValue = value;
        }

        public Vector2DArray(DNSArrayProperty<DNSMcFVector2D> value)
            : this()
        {
            FVector2DArrayPropertyValue = value;
        }

        public DNSArrayProperty<DNSMcVector2D> Vector2DArrayPropertyValue
        {
            get
            {
                DNSArrayProperty<DNSMcVector2D> retValue = new DNSArrayProperty<DNSMcVector2D>();
                retValue.aElements = new DNSMcVector2D[dgvVector2D.RowCount - 1];
                FillEmptyCells();
                
                for (int i = 0; i < dgvVector2D.RowCount; i++)
                {
                    if (dgvVector2D.Rows[i].IsNewRow == false)
                    {
                        retValue.aElements[i] = new DNSMcVector2D(GetDouble(dgvVector2D[1, i].Value.ToString()), GetDouble(dgvVector2D[2, i].Value.ToString()));
                    }
                }
                return retValue;
            }
            set
            {
                dgvVector2D.Rows.Clear();
                if (value.aElements != null)
                {
                    foreach (DNSMcVector2D vector2D in value.aElements)
                    {
                        dgvVector2D.Rows.Add("", vector2D.x, vector2D.y);
                    }
                    dgvVector2D.ClearSelection();
                }
            }
        }

        public DNSArrayProperty<DNSMcFVector2D> FVector2DArrayPropertyValue
        {
            get
            {
                DNSArrayProperty<DNSMcFVector2D> retValue = new DNSArrayProperty<DNSMcFVector2D>();
                retValue.aElements = new DNSMcFVector2D[dgvVector2D.RowCount - 1];
                FillEmptyCells();
                
                for (int i = 0; i < dgvVector2D.RowCount; i++)
                {
                    if (dgvVector2D.Rows[i].IsNewRow == false)
                    {
                        retValue.aElements[i] = new DNSMcFVector2D(GetFloat(dgvVector2D[1, i].Value.ToString()), GetFloat(dgvVector2D[2, i].Value.ToString()));
                    }
                }
                return retValue;
            }
            set
            {
                if (value.aElements != null)
                {
                    dgvVector2D.Rows.Clear();
                    foreach (DNSMcFVector2D vector2D in value.aElements)
                    {
                        dgvVector2D.Rows.Add("", vector2D.x, vector2D.y);
                    }
                    dgvVector2D.ClearSelection();
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

        private void dgvVector2D_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgvVector2D.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex).ToString();
        }

        public void ResetGrid()
        {
            dgvVector2D.Rows.Clear();
        }

        private void FillEmptyCells()
        {
            for (int i = 0; i < dgvVector2D.RowCount; i++)
            {
                if (dgvVector2D.Rows[i].IsNewRow == false)
                {
                    for (int j = 0; j < dgvVector2D.ColumnCount; j++)
                    {
                        if (dgvVector2D[j, i].Value == null)
                            dgvVector2D[j, i].Value = "0";
                    }
                }
            }
        }
    }
}
