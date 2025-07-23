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
    public partial class CtrlPropertyVect3D : CtrlPropertyBase
    {
        public CtrlPropertyVect3D()
        {
            InitializeComponent();
        }

        public DNSMcVector3D RegVector3DVal
        {
            get { return this.ctrl3DRegVector.GetVector3D(); }
            set { ctrl3DRegVector.SetVector3D(value); }
        }

        public string RegVectLable
        {
            get { return lblRegVect.Text; }
            set { lblRegVect.Text = value; }
        }

        public DNSMcVector3D SelVector3DVal
        {
            get { return this.ctrlSel3DVector.GetVector3D(); }
            set { ctrlSel3DVector.SetVector3D(value); }
        }

        public string SelVectLable
        {
            get { return lblSelVect.Text; }
            set { lblSelVect.Text = value; }
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }
    }
}
