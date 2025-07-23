using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    public partial class frmColorPropertyType : frmBasePropertyType
    {
        private Color m_SelectedColor;

        public frmColorPropertyType()
            : base()
        {
            InitializeComponent();
        }

        public frmColorPropertyType(uint id)
            : base(id)
        {
            base.ID = id;
            InitializeComponent();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            colorDialogPropertyValue.Color = m_SelectedColor;
            if (colorDialogPropertyValue.ShowDialog() == DialogResult.OK)
            {
                m_SelectedColor = colorDialogPropertyValue.Color;
                btnColor.BackColor = m_SelectedColor;
            }
        }

        public DNSMcBColor BColor
        {
            get
            {
                return new DNSMcBColor(m_SelectedColor.R, m_SelectedColor.G, m_SelectedColor.B, (byte)AlphaColor);
            }
            set
            {
                m_SelectedColor = Color.FromArgb(value.a, value.r, value.g, value.b);
                btnColor.BackColor = m_SelectedColor;
                AlphaColor = value.a;
            }
        }

        public DNSMcFColor FColor
        {
            get
            {
                return new DNSMcFColor((float)m_SelectedColor.R, (float)m_SelectedColor.G, (float)m_SelectedColor.B, (float)AlphaColor);
            }
            set
            {
                m_SelectedColor = Color.FromArgb((int)value.a,(int)value.r, (int)value.g, (int)value.b);
                btnColor.BackColor = m_SelectedColor;
                AlphaColor = (int)value.a;
            }
        }
        
        public decimal AlphaColor
        {
            get { return nudAlphaColor.Value; }
            set
            {
                if (value <= 255 && value >= 0)
                    nudAlphaColor.Value = value;
                else
                    nudAlphaColor.Value = 0;
            }
        }


    }
}