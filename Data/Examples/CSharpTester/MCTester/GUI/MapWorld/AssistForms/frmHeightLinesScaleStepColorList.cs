using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld.Assist_Forms
{
    public partial class frmHeightLinesScaleStepColorList : Form
    {
        private List<DNSMcBColor> m_OrgStepColors;
        private List<DNSMcBColor> m_lColor;

        public frmHeightLinesScaleStepColorList(List<DNSMcBColor> scaleStepColors)
        {
            m_OrgStepColors = scaleStepColors;
            InitializeComponent();
        }

        private void frmHeightLinesScaleStepColorList_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < m_OrgStepColors.Count; i++)
            {
                dgvColors.Rows.Add();
                dgvColors[0, i].Value = "R: " + m_OrgStepColors[i].r.ToString() +
                                        ",  G: " + m_OrgStepColors[i].g.ToString() +
                                        ",  B: " + m_OrgStepColors[i].b.ToString() +
                                        ",  A: " + m_OrgStepColors[i].a.ToString();

                dgvColors[1, i].Style.BackColor = Color.FromArgb((int)m_OrgStepColors[i].a, (int)m_OrgStepColors[i].r, (int)m_OrgStepColors[i].g, (int)m_OrgStepColors[i].b);
            }
        }

        private void dgvColors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                ColorDialog dlgColor = new ColorDialog();
                dlgColor.Color = dgvColors[1, e.RowIndex].Style.BackColor;

                if (dlgColor.ShowDialog() == DialogResult.OK)
                {
                    //Add row to table
                    if (dgvColors.Rows[e.RowIndex].IsNewRow == true)
                    {
                        dgvColors.Rows.Insert(dgvColors.Rows.GetLastRow(DataGridViewElementStates.None), 1);
                    }
                    

                    dgvColors[0, e.RowIndex].Value = "R: " + dlgColor.Color.R.ToString() +
                                                        ",  G: " + dlgColor.Color.G.ToString() +
                                                        ",  B: " + dlgColor.Color.B.ToString() +
                                                        ",  A: " + dlgColor.Color.A.ToString();

                    dgvColors[1, e.RowIndex].Style.BackColor = dlgColor.Color;                    
                }                 
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_lColor = new List<DNSMcBColor>();
            if (dgvColors.Rows.Count > 1)
            {                
                for (int i = 0; i < dgvColors.Rows.Count-1; i++)
                {
                    DNSMcBColor color = new DNSMcBColor(dgvColors[1, i].Style.BackColor.R, dgvColors[1, i].Style.BackColor.G, dgvColors[1, i].Style.BackColor.B, dgvColors[1, i].Style.BackColor.A);
                    m_lColor.Add(color);                    
                }
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public List<DNSMcBColor> ColorList
        {
            get { return m_lColor; }
            set { m_lColor = value; }
        }


    }

}