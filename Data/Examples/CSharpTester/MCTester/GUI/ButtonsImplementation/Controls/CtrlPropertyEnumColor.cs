using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.ObjectWorld.ObjectsUserControls;

namespace MCTester.Controls
{
    public partial class CtrlPropertyEnumColor : CtrlPropertyEnum
    {
        private Color m_RegSelectedColor;
        private Color m_SelSelectedColor;

        public CtrlPropertyEnumColor()
        {
            InitializeComponent();
            numUDRegAlphaColor.Value = 255;
            numUDSelAlphaColor.Value = 255;
        }

        public Color RegSelectedColor
        {
            get { return m_RegSelectedColor; }
            set
            {
                m_RegSelectedColor = value;
                numUDRegAlphaColor.Value = value.A;
                picbRegColor.BackColor = m_RegSelectedColor;
            }
        }

        public Color SelSelectedColor
        {
            get { return m_SelSelectedColor; }
            set
            {
                m_SelSelectedColor = value;
                numUDSelAlphaColor.Value = value.A;
                picbSelColor.BackColor = m_SelSelectedColor;
            }
        }

        private void picbRegColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            colorDialog.Color = m_RegSelectedColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                numUDRegAlphaColor.Value = 255;
                m_RegSelectedColor = Color.FromArgb((int)numUDRegAlphaColor.Value, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                picbRegColor.BackColor = m_RegSelectedColor;
            }
        }

        private void picbSelColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            colorDialog.Color = m_SelSelectedColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                numUDSelAlphaColor.Value = 255;
                m_SelSelectedColor = Color.FromArgb((int)numUDSelAlphaColor.Value, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                picbSelColor.BackColor = m_SelSelectedColor;
            }
        }

        public DNSMcFColor RegFColor
        {
            get
            {
                return new DNSMcFColor(m_RegSelectedColor.R / 255f, m_RegSelectedColor.G / 255f, m_RegSelectedColor.B / 255f, m_RegSelectedColor.A / 255f);
            }

            set
            {
                byte alpha = (byte)value.a;

                m_RegSelectedColor = Color.FromArgb((int)(value.a * 255), (int)(value.r * 255), (int)(value.g * 255), (int)(value.b * 255));
                numUDRegAlphaColor.Value = (decimal)(alpha * 255);
                picbRegColor.BackColor = m_RegSelectedColor;
            }
        }

        public DNSMcBColor RegBColor
        {
            get
            {
                return new DNSMcBColor(m_RegSelectedColor.R, m_RegSelectedColor.G, m_RegSelectedColor.B, m_RegSelectedColor.A);
            }

            set
            {
                byte alpha = (byte)value.a;
                if (value.a > 255)
                    alpha = 255;
                else
                    if (value.a < 0)
                        alpha = 0;

                m_RegSelectedColor = Color.FromArgb(alpha, (int)value.r, (int)value.g, (int)value.b);
                numUDRegAlphaColor.Value = alpha;
                picbRegColor.BackColor = m_RegSelectedColor;
            }
        }

        public DNSMcFColor SelFColor
        {
            get
            {
                return new DNSMcFColor(m_SelSelectedColor.R / 255f, m_SelSelectedColor.G / 255f, m_SelSelectedColor.B / 255f, m_SelSelectedColor.A / 255);
            }

            set
            {
                if (value.a < 0)
                    value.a = 0;

                m_SelSelectedColor = Color.FromArgb((int)(value.a * 255), (int)(value.r * 255), (int)(value.g * 255), (int)(value.b * 255));
                numUDSelAlphaColor.Value = (decimal)(value.a * 255);
                picbSelColor.BackColor = m_SelSelectedColor;
            }
        }

        public DNSMcBColor SelBColor
        {
            get
            {
                return new DNSMcBColor(m_SelSelectedColor.R, m_SelSelectedColor.G, m_SelSelectedColor.B, m_SelSelectedColor.A);
            }

            set
            {
                if (value.a > 255)
                    value.a = 255;
                else
                    if (value.a < 0)
                        value.a = 0;

                m_SelSelectedColor = Color.FromArgb((int)value.a, (int)value.r, (int)value.g, (int)value.b);
                numUDSelAlphaColor.Value = (decimal)value.a;
                picbSelColor.BackColor = m_SelSelectedColor;
            }
        }

        private void RegNumUDAlphaColor_ValueChanged(object sender, EventArgs e)
        {
            m_RegSelectedColor = Color.FromArgb((int)numUDRegAlphaColor.Value, m_RegSelectedColor.R, m_RegSelectedColor.G, m_RegSelectedColor.B);
            picbRegColor.BackColor = m_RegSelectedColor;
        }

        private void SelNumUDAlphaColor_ValueChanged(object sender, EventArgs e)
        {
            m_SelSelectedColor = Color.FromArgb((int)numUDSelAlphaColor.Value, m_SelSelectedColor.R, m_SelSelectedColor.G, m_SelSelectedColor.B);
            picbSelColor.BackColor = m_SelSelectedColor;
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }

        public void ParentSetCtrlSelRadioButtonState()
        {
            base.SetCtrlSelRadioButtonState();
        }

        public void ParentSetCtrlRegRadioButtonState()
        {
            base.SetCtrlRegRadioButtonState();
        }

        public void ChangeSelectionCB()
        {
            base.chxSelectionProperty.Checked = true;
            base.chxSelectionProperty.Enabled = false;

        }
    }
}
