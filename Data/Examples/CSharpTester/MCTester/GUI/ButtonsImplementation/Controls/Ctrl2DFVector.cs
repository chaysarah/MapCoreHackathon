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
    public partial class Ctrl2DFVector : UserControl
    {
        public Ctrl2DFVector()
        {
            InitializeComponent();
        }

        public Ctrl2DFVector(DNSMcFVector2D Fvector2D)
        {
            InitializeComponent();

            X = Fvector2D.x;
            Y = Fvector2D.y;
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

        public DNSMcFVector2D GetVector2D()
        {
            return new DNSMcFVector2D(X, Y);
        }

        public void SetVector2D(DNSMcFVector2D value)
        {
            X = value.x;
            Y = value.y;
        }

        internal void SetEmptyValue()
        {
            txtXValue.Text = "";
            txtYValue.Text = "";
        }
    }
}
