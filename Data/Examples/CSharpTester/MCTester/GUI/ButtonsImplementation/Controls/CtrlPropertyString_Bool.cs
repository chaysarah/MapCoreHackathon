using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.ObjectWorld;

namespace MCTester.Controls
{
    public partial class CtrlPropertyString_Bool : CtrlPropertyBase
    {
        public CtrlPropertyString_Bool()
        {
            InitializeComponent();
            HideSelectionTab();
        }

        public string RegLableText
        {
            get { return this.lblRegString.Text; }
            set { this.lblRegString.Text = value; }
        }

        public string SelLableText
        {
            get { return this.lblSelString.Text; }
            set { this.lblSelString.Text = value; }
        }

        public string RegBoolText
        {
            get { return this.chxRegBool.Text; }
            set { this.chxRegBool.Text = value; }
        }

        public string SelBoolText
        {
            get { return this.chxSelBool.Text; }
            set { this.chxSelBool.Text = value; }
        }

        public string RegStringValue
        {
            get { return this.txtRegText.Text; }
            set { this.txtRegText.Text = value; }
        }

        public string SelStringValue
        {
            get { return this.txtSelText.Text; }
            set { this.txtSelText.Text = value; }
        }

        public bool RegBoolValue
        {
            get { return this.chxRegBool.Checked; }
            set { this.chxRegBool.Checked = value; }
        }

        public bool SelBoolValue
        {
            get { return this.chxSelBool.Checked; }
            set { this.chxSelBool.Checked = value; }
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }

        
    }
}
