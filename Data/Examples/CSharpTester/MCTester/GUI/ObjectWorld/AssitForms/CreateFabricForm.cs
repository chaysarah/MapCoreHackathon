using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;
using MapCore;
using MCTester.MapWorld;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class CreateFabricForm : Form
    {
        #region Data Members
        protected OpenFileDialog ofd;
        private int m_colColorToSub = 1;
        private int m_colColorToSubAlpha = 2;
        private int m_colSubColor = 3;
        private int m_colSubColorAlpha = 4;

        #endregion

        public CreateFabricForm()
        {
            InitializeComponent();

            DataGridViewComboBoxColumn colCombo = (dgvColors.Columns[m_colColorToSubAlpha] as DataGridViewComboBoxColumn);
            colCombo.DataSource = Choice.GetChoices(255);
            colCombo.DisplayMember = "Name";  // the Name property in Choice class
            colCombo.ValueMember = "Value";

            DataGridViewComboBoxColumn colCombo2 = (dgvColors.Columns[m_colSubColorAlpha] as DataGridViewComboBoxColumn);
            colCombo2.DataSource = Choice.GetChoices(255);
            colCombo2.DisplayMember = "Name";  // the Name property in Choice class
            colCombo2.ValueMember = "Value";
        }

        #region Protected Methods
        protected void Color_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_colorSelection.ShowDialog() == DialogResult.OK)
            {
                LinkLabel link = sender as LinkLabel;
                switch (link.Text)
                {
                    case "Transparent Color":
                        m_pbxTransparentColor.BackColor = m_colorSelection.Color;
                        m_pbxTransparentColor.Enabled = true;
                        break;
                }
            }
        }

        protected void SetColors(/*DNSMcBColor? pTransparentColor,*/ DNSColorSubstitution[] pColorSubstitution)
        {
            //if(pTransparentColor.HasValue)

            int index = 0;
            if (pColorSubstitution != null)
            {
                foreach (DNSColorSubstitution colorSubstitution in pColorSubstitution)
                {
                    int alpha1 = -1, alpha2 = -1;
                    Color color1 = Color.Transparent;
                    Color color2 = Color.Transparent;

                    dgvColors.Rows.Add();

                    alpha1 = (int)colorSubstitution.ColorToSubstitute.a;
                    color1 = Color.FromArgb(colorSubstitution.ColorToSubstitute.r, colorSubstitution.ColorToSubstitute.g, colorSubstitution.ColorToSubstitute.b);

                    alpha2 = (int)colorSubstitution.SubstituteColor.a;
                    color2 = Color.FromArgb(colorSubstitution.SubstituteColor.r, colorSubstitution.SubstituteColor.g, colorSubstitution.SubstituteColor.b);

                    (dgvColors[m_colColorToSub, index] as DataGridViewButtonCell).FlatStyle = FlatStyle.Popup;
                    dgvColors[m_colColorToSub, index].Style.BackColor = color1;
                    if (alpha1 != -1)
                        dgvColors[m_colColorToSubAlpha, index].Value = alpha1;

                    (dgvColors[m_colSubColor, index] as DataGridViewButtonCell).FlatStyle = FlatStyle.Popup;
                    dgvColors[m_colSubColor, index].Style.BackColor = color2;
                    if (alpha2 != -1)
                        dgvColors[m_colSubColorAlpha, index].Value = alpha2;
                    index++;
                }
                dgvColors.ClearSelection();
            }
        }

        protected void GetColors(out DNSMcBColor? pTransparentColor, out DNSColorSubstitution[] pColorSubstitution, out bool pColorSubstitutionResult)
        {
            pTransparentColor = null;
            pColorSubstitution = null;
            pColorSubstitutionResult = true;
            //init the texture colors
            if (m_pbxTransparentColor.Enabled)
                pTransparentColor = new DNSMcBColor(m_pbxTransparentColor.BackColor.R, m_pbxTransparentColor.BackColor.G, m_pbxTransparentColor.BackColor.B, 0);

            pColorSubstitution = new DNSColorSubstitution[dgvColors.RowCount - 1];

            for (int i = 0; i < dgvColors.RowCount; i++)
            {
                if (dgvColors.Rows[i].IsNewRow == false)
                {
                    Color toSubColor = dgvColors[m_colColorToSub, i].Style.BackColor;
                    if(toSubColor == new Color())
                    {
                        MessageBox.Show("Missing 'Color to Sub' in row " + i + " ,fix it and try again", "Get Color Substitution");
                        pColorSubstitutionResult = false;
                        return;
                    }
                    int toSubAlpha = 0;
                    if (dgvColors[m_colColorToSubAlpha, i].Value == null)
                    {
                        MessageBox.Show("Missing 'Color to Sub Alpha' in row " + i + " ,fix it and try again", "Get Color Substitution");
                        pColorSubstitutionResult = false;
                        return;
                    }
                    else
                        toSubAlpha = (int)dgvColors[m_colColorToSubAlpha, i].Value;
                    Color subColor = dgvColors[m_colSubColor, i].Style.BackColor;
                    if (subColor == new Color())
                    {
                        MessageBox.Show("Missing 'Sub Color' in row " + i + " ,fix it and try again", "Get Color Substitution");
                        pColorSubstitutionResult = false;
                        return;
                    }
                    int subColorAlpha = 0;
                    if (dgvColors[m_colSubColorAlpha, i].Value == null)
                    {
                        MessageBox.Show("Missing 'Sub Color Alpha' in row " + i + " ,fix it and try again", "Get Color Substitution");
                        pColorSubstitutionResult = false;
                        return;
                    }
                    else
                        subColorAlpha = (int)dgvColors[m_colSubColorAlpha, i].Value;

                    pColorSubstitution[i] = new DNSColorSubstitution();
                    pColorSubstitution[i].ColorToSubstitute = new DNSMcBColor((byte)toSubColor.R, (byte)toSubColor.G, (byte)toSubColor.B, (byte)toSubAlpha);
                    pColorSubstitution[i].SubstituteColor = new DNSMcBColor((byte)subColor.R, (byte)subColor.G, (byte)subColor.B, (byte)subColorAlpha);

                }
            }

        }

        protected void VisibleColorControls(bool visible)
        {
            m_lilTransparentColor.Visible = visible;
            m_pbxTransparentColor.Visible = visible;

            dgvColors.Visible = visible;


        }
        #endregion

        

        private void dgvColors_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == m_colColorToSub || e.ColumnIndex == m_colSubColor)  // color column
            {
                ColorDialog colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    if (e.RowIndex == dgvColors.RowCount - 1 || dgvColors.Rows[dgvColors.RowCount - 1].IsNewRow == false)
                    {
                        dgvColors.Rows.Add();
                    }
                    DataGridViewButtonCell buttCell = dgvColors[e.ColumnIndex, e.RowIndex] as DataGridViewButtonCell;
                    buttCell.FlatStyle = FlatStyle.Popup;
                    buttCell.Style.BackColor = colorDialog.Color;
                    if (dgvColors[e.ColumnIndex + 1, e.RowIndex].Value == null)
                    {
                        ((DataGridViewComboBoxCell)dgvColors[e.ColumnIndex + 1, e.RowIndex]).Value = 255;
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvColors.Rows.Clear();
        }

        private void dgvColors_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgvColors.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex).ToString();
        }
    }
}