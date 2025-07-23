using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Controls
{
    public partial class CtrlPropertyVect2D : CtrlPropertyBase
    {
        public CtrlPropertyVect2D()
        {
            InitializeComponent();
        }

        public DNSMcVector2D Vect2Dval
        {
            get { return ctrl2DRegVector.GetVector2D(); }
            set { ctrl2DRegVector.SetVector2D(value); }
        }

        public string VectLable
        {
            get { return lblRegVect2D.Text; }
            set { lblRegVect2D.Text = value; }
        }

        public DNSMcVector2D SelVect2Dval
        {
            get { return ctrl2DSelVector.GetVector2D(); }
            set { ctrl2DSelVector.SetVector2D(value); }
        }

        public string SelVectLable
        {
            get { return lblSelVect2D.Text; }
            set { lblSelVect2D.Text = value; }
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }
    }
}
