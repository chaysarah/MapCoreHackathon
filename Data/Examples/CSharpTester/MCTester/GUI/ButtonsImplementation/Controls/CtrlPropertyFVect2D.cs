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
    public partial class CtrlPropertyFVect2D : CtrlPropertyBase
    {
        public CtrlPropertyFVect2D()
        {
            InitializeComponent();
        }

        public DNSMcFVector2D RegFVector2DVal
        {
            get { return this.ctrl2DRegFVector.GetVector2D(); }
            set { this.ctrl2DRegFVector.SetVector2D(value); }
        }

        public string RegFVectLable
        {
            get { return lblRegFVec2D.Text; }
            set { lblRegFVec2D.Text = value; }
        }

        public DNSMcFVector2D SelFVector2DVal
        {
            get { return this.ctrl2DSelFVector.GetVector2D(); }
            set { this.ctrl2DSelFVector.SetVector2D(value); }
        }

        public string SelFVectLable
        {
            get { return lblSelFVect2D.Text; }
            set { lblSelFVect2D.Text = value; }
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }
    }
}
