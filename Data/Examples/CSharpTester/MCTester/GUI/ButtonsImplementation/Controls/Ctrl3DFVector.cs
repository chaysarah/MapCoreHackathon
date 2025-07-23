using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using MapCore;

namespace MCTester.Controls
{
    public partial class Ctrl3DFVector : UserControl
    {
        public Ctrl3DFVector()
        {
            InitializeComponent();
        }

        public Ctrl3DFVector(DNSMcFVector3D vector3D)
        {
            InitializeComponent();

            X = vector3D.x;
            Y = vector3D.y;
            Z = vector3D.z;
        }

        public float X
		{
			get { return txtXValue.GetFloat(); }
			set { txtXValue.SetFloat(value); }
		}

        public float Y
		{
            get { return txtYValue.GetFloat(); }
            set { txtYValue.SetFloat(value); }
		}

        public float Z
        {
            get { return txtZValue.GetFloat(); }
            set { txtZValue.SetFloat(value); }
        }

        public DNSMcFVector3D GetVector3D()
        {
            return new DNSMcFVector3D(X, Y, Z);
        }

        public void SetVector3D(DNSMcFVector3D value)
        {
            X = value.x;
            Y = value.y;
            Z = value.z;
        }

        internal void SetEmptyValue()
        {
            txtXValue.Text = "";
            txtYValue.Text = "";
            txtZValue.Text = "";
        }
    }
}
