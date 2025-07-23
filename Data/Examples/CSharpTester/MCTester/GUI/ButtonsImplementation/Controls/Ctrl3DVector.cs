using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;
using MapCore;

namespace MCTester.Controls
{
    public partial class Ctrl3DVector : UserControl
    {
        private bool m_IsReadOnly = false;
        public Ctrl3DVector()
        {
            InitializeComponent();
        }

        public Ctrl3DVector(DNSMcVector3D vector3D)
        {
            InitializeComponent();

            X = vector3D.x;
            Y = vector3D.y;
            Z = vector3D.z;
        }


        public void SetEmptyValue()
        {
            txtXValue.Text = "";
            txtYValue.Text = "";
            txtZValue.Text = "";
        }

        public double X
        {
            get { return txtXValue.GetDouble(); }
            set { txtXValue.SetDouble(value); }
        }

        public double Y
        {
            get { return txtYValue.GetDouble(); }
            set { txtYValue.SetDouble(value); }
        }

        public double Z
        {
            get { return txtZValue.GetDouble(); }
            set { txtZValue.SetDouble(value); }
        }

        public DNSMcVector3D GetVector3D()
        {
            return new DNSMcVector3D(X, Y, Z);
        }
        public void SetVector3D(DNSMcVector3D value)
        {
            X = value.x;
            Y = value.y;
            Z = value.z;
        }

        public bool IsReadOnly
        {
            get { return m_IsReadOnly; }
            set
            {
                m_IsReadOnly = value;
                txtXValue.ReadOnly = value;
                txtYValue.ReadOnly = value;
                txtZValue.ReadOnly = value;
            }
        }

        public void DisabledZ()
        {
            txtZValue.Text = "0";
            //txtZValue.ReadOnly = true;
            txtZValue.Enabled = false;
        }

        public void HideZ()
        {
            label3.Visible = false;
            txtZValue.Visible = false;
        }
    }
}
