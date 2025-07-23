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
    public partial class CtrlPropertyAttenuation : CtrlPropertyBase
    {
        public CtrlPropertyAttenuation()
        {
            InitializeComponent();
        }

        public DNSMcAttenuation RegAttenuation
        {
            get
            {
                return new DNSMcAttenuation(ntxRegConst.GetFloat(),ntxRegLinear.GetFloat(),ntxRegSquare.GetFloat(),ntxRegRange.GetFloat());
            }
            set
            {
                ntxRegConst.SetFloat(value.fConst);
                ntxRegLinear.SetFloat(value.fLinear);
                ntxRegSquare.SetFloat(value.fSquare);
                ntxRegRange.SetFloat(value.fRange);
            }
        }

        public DNSMcAttenuation SelAttenuation
        {
            get
            {
                return new DNSMcAttenuation(ntxSelConst.GetFloat(), ntxSelLinear.GetFloat(), ntxSelSquare.GetFloat(), ntxSelRange.GetFloat());
            }
            set
            {
                ntxSelConst.SetFloat(value.fConst);
                ntxSelLinear.SetFloat(value.fLinear);
                ntxSelSquare.SetFloat(value.fSquare);
                ntxSelRange.SetFloat(value.fRange);
            }
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }
    }
}
