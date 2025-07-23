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
    public partial class CtrlPropertyBool : CtrlPropertyBase
    {
        public CtrlPropertyBool()
        {
            RegPropertyID = (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID;
            InitializeComponent();
        }

        public bool RegBoolVal
        {
            get { return this.chxRegBool .Checked; }
            set { this.chxRegBool.Checked = value; }
        }

        public string RegBoolLable
        {
            get { return this.chxRegBool.Text; }
            set { this.chxRegBool.Text = value; }
        }

        public bool SelBoolVal
        {
            get { return this.chxSelBool.Checked; }
            set { this.chxSelBool.Checked = value; }
        }

        public string SelBoolLable
        {
            get { return this.chxSelBool.Text; }
            set { this.chxSelBool.Text = value; }
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }
    }
}
