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
    public partial class CtrlPropertyNumericUpDown : CtrlPropertyBase
    {
        public CtrlPropertyNumericUpDown()
        {
            InitializeComponent();
        }

        public byte NudVal
        {
            get { return (Byte)this.nudVal.Value; }
            set { this.nudVal.Value = value; }
        }

        public string NudLable
        {
            get { return lblNumericUpDown.Text; }
            set { lblNumericUpDown.Text = value.ToString(); }
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }
    }
}
