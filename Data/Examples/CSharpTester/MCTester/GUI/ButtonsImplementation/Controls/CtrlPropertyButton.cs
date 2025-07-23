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
    public partial class CtrlPropertyButton : CtrlPropertyBase
    {
        public CtrlPropertyButton()
        {
            InitializeComponent();
        }

        public string RegLableButton
        {
            get { return this.lblRegButton.Text; }
            set { this.lblRegButton.Text = value.ToString(); }
        }

        public string RegButtonText
        {
            get { return this.btnRegFunction.Text; }
            set { this.btnRegFunction.Text = value.ToString(); }
        }

        public Button RegButtonObj
        {
            get { return this.btnRegFunction; }
            set { this.btnRegFunction = value; }
        }

        protected virtual void btnRegFunction_Click(object sender, EventArgs e)
        {

        }

        public string SelLableButton
        {
            get { return this.lblSelButton.Text; }
            set { this.lblSelButton.Text = value.ToString(); }
        }

        public string SelButtonText
        {
            get { return this.btnSelFunction.Text; }
            set { this.btnSelFunction.Text = value.ToString(); }
        }

        public Button SelButtonObj
        {
            get { return this.btnSelFunction; }
            set { this.btnSelFunction = value; }
        }

        protected virtual void btnSelFunction_Click(object sender, EventArgs e)
        {

        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }
       
    }
}
