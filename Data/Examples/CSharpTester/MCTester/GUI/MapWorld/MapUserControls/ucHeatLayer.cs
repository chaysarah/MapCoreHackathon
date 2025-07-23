using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucHeatLayer : ucLayer, IUserControlItem
    {
        private IDNMcNativeHeatMapLayer m_CurrentObject;

        public ucHeatLayer()
        {
            InitializeComponent();
            dgvColorTable.Visible = false;
        }

        protected override void SaveItem()
        {
            base.SaveItem();
        }
        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            

            m_CurrentObject = (IDNMcNativeHeatMapLayer)aItem;
            base.LoadItem(aItem);
            

        }

        #endregion

        private void btnSetColorTable_Click(object sender, EventArgs e)
        {
            DNSMcBColor RGBcolor;
            if (cmbColorRange.SelectedIndex >= 0)
            {
                switch (cmbColorRange.SelectedIndex)
                {
                    case 0:
                        RGBcolor = new DNSMcBColor(0, 255, 0, 255);
                        SetColorTable(1, cmbColorRange.SelectedIndex, RGBcolor);
                        break;
                    case 1:
                        RGBcolor = new DNSMcBColor(0, 0, 255, 255);
                        SetColorTable(2, 0, RGBcolor);
                        break;
                    case 2:
                        RGBcolor = new DNSMcBColor(0, 255, 0, 255);
                        SetColorTable(1, cmbColorRange.SelectedIndex, RGBcolor);
                        break;
                    case 3:
                        RGBcolor = new DNSMcBColor(0, 0, 0, 0);
                        SetColorTable(3, cmbColorRange.SelectedIndex, RGBcolor);
                        break;
                }
            }
            else
                MessageBox.Show("Please select range of colors!");
        }
        public void SetColorTable(int index1,int index2,DNSMcBColor my_Color)
        {
            if (index1 != 3)
            {
                int myIndex = 0;
            
                byte [] RGB=new byte[4]{my_Color.r,my_Color.g,my_Color.b,my_Color.a};
                DNSMcBColor[] colorTable = new DNSMcBColor[256];
                colorTable[myIndex++] = my_Color;
            
                for (int i = 1; i < 256; i++)
                {
                    RGB[index1]--;
                    RGB[index2]++;
                    DNSMcBColor RGBcolor = new DNSMcBColor(RGB);
                    colorTable[myIndex++] = RGBcolor;
                }
                m_CurrentObject.SetColorTable(colorTable);
            }
            else
            {
                m_CurrentObject.SetColorTable(null);
            }
        }

        private void btnGetColorTable_Click(object sender, EventArgs e)
        {
            dgvColorTable.Visible = true;
            DNSMcBColor[] colorTable = m_CurrentObject.GetColorTable();
            for (int i = 0; i < colorTable.Length; i++)
            {
                dgvColorTable.Rows.Add();
                dgvColorTable[0, i].Value = "R: " + colorTable[i].r.ToString() +
                                        ",  G: " + colorTable[i].g.ToString() +
                                        ",  B: " + colorTable[i].b.ToString() +
                                        ",  A: " + colorTable[i].a.ToString();

                dgvColorTable[1, i].Style.BackColor = Color.FromArgb((int)colorTable[i].a, (int)colorTable[i].r, (int)colorTable[i].g, (int)colorTable[i].b);
            }
        }

        private void btnGetMinMaxValues_Click(object sender, EventArgs e)
        {
            try
            {
                double min, max;
                m_CurrentObject.GetMinMaxValues(ntbScale.GetFloat(), out min, out max);
                txtMin.Text = min.ToString();
                txtMax.Text = max.ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetMinMaxValues", McEx);
            }
        }
    }
}
