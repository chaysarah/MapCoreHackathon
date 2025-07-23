using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;

namespace MCTester.Controls
{
    public partial class CtrlAnimation : UserControl
    {
        public CtrlAnimation()
        {
            InitializeComponent();
        }

        public string StringValue
        {
            get { return this.txtText.Text; }
            set { this.txtText.Text = value; }
        }

        public bool BoolValue
        {
            get { return this.chxBool.Checked; }
            set { this.chxBool.Checked = value; }
        }

        public DNSMcAnimation AnimationValue
        {
            get
            {
                return new DNSMcAnimation(txtText.Text, chxBool.Checked);
            }
            set
            {
                chxBool.Checked = value.bLoop;
                txtText.Text = value.strAnimationName;
            }
        }
    }
}
