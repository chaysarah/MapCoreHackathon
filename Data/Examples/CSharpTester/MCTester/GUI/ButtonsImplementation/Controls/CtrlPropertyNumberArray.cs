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
    public partial class CtrlPropertyNumberArray : CtrlPropertyBase
    {
        public CtrlPropertyNumberArray()
        {
            InitializeComponent();
        }


        public DNSArrayProperty<uint> RegNumberArrayPropertyValue
        {
            get
            {
                return RegPropertyNumberArray.UIntArrayPropertyValue;
            }
            set
            {
                RegPropertyNumberArray.UIntArrayPropertyValue = value;
            }
        }

        public DNSArrayProperty<uint> SelNumberArrayPropertyValue
        {
            get
            {
                return RegPropertyNumberArray.UIntArrayPropertyValue;
            }
            set
            {
                RegPropertyNumberArray.UIntArrayPropertyValue = value;
            }
        }

        private void btnRegReset_Click(object sender, EventArgs e)
        {
        }

        private void btnSelReset_Click(object sender, EventArgs e)
        {
        }
    }
}
