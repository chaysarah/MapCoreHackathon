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
    public partial class Ctrl2DVector : UserControl
    {
        public Ctrl2DVector()
        {
            InitializeComponent();
        }

        public Ctrl2DVector(DNSMcVector2D vector2D)
        {
            InitializeComponent();

            X = vector2D.x;
            Y = vector2D.y;
        }

        public double GetDouble(string text)
        {
            try
            {
                double dParam;
                if (String.Compare(text, "MAX", true) == 0)
                    dParam = double.MaxValue;
                else
                    dParam = double.Parse(text);

                return dParam;
            }
            catch
            {
                return 0d;
            }
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

        public DNSMcVector2D GetVector2D()
        {
            return new DNSMcVector2D(X, Y);
        }

        public void SetVector2D(DNSMcVector2D value)
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
