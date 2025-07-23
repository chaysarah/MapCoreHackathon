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
    public partial class SelectColor : UserControl
    {
        private Color m_SelectedColor;
        public SelectColor()
        {
            InitializeComponent();

            m_SelectedColor = Color.Black;
            picbColor.BackColor = m_SelectedColor;
            picbColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            
            nudAlpha.Value = 255;

            EnabledButtons(false);
        }

        public DNSMcBColor BColor
        {
            get
            {
                return new DNSMcBColor(m_SelectedColor.R, m_SelectedColor.G, m_SelectedColor.B, (byte)nudAlpha.Value);
            }

            set
            {
                byte alpha = (byte)value.a;
                if (value.a > 255)
                    alpha = 255;
                else
                    if (value.a < 0)
                        alpha = 0;

                m_SelectedColor = Color.FromArgb(255, (int)value.r, (int)value.g, (int)value.b);
                nudAlpha.Value = alpha;
                picbColor.BackColor = m_SelectedColor;
            }
        }

        public DNSMcFColor FColor
        {
            get
            {
                return new DNSMcFColor(m_SelectedColor.R / 255f, m_SelectedColor.G / 255f, m_SelectedColor.B / 255f, (int)nudAlpha.Value / 255f);
            }

            set
            {
                //byte alpha = (byte)value.a;

                m_SelectedColor = Color.FromArgb(255, (int)(value.r * 255), (int)(value.g * 255), (int)(value.b * 255));
                nudAlpha.Value = (decimal)(byte)(value.a * 255);
                picbColor.BackColor = m_SelectedColor;
            }
        }

        public Color AlphaColor
        {
            set
            {
                m_SelectedColor = value;
                nudAlpha.Value = value.A;
                picbColor.BackColor = Color.FromArgb(255, m_SelectedColor.R, m_SelectedColor.G, m_SelectedColor.B);
            }
        }

        private void nudAlpha_ValueChanged(object sender, EventArgs e)
        {
            //ChangeColor(m_SelectedColor);
        }

        private void ChangeColor(Color color)
        {
            m_SelectedColor = Color.FromArgb(255, color);
            picbColor.BackColor = m_SelectedColor;
        }

        public void EnabledButtons(bool isEnabled)
        {
            picbColor.Enabled = isEnabled;
            nudAlpha.Enabled = isEnabled;
        }

        private void picbColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = m_SelectedColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                ChangeColor(colorDialog.Color);
            }
            picbColor.Focus();
        }
    }
}
