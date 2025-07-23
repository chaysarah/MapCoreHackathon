using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCTester.Controls;
using MapCore;

namespace MCTester.Controls
{
    public partial class CtrlPropertyColorArray : CtrlPropertyBase 
    {
        public CtrlPropertyColorArray()
        {
            InitializeComponent();
        }

        public DNSArrayProperty<DNSMcBColor> RegBColorsPropertyValue
        {
            get
            {
                return RegPropertyColorArray.BColorsPropertyValue;
            }
            set
            {
                RegPropertyColorArray.BColorsPropertyValue = value;
            }
        }

        public DNSArrayProperty<DNSMcBColor> SelBColorsPropertyValue
        {
            get
            {
                return SelPropertyColorArray.BColorsPropertyValue;
            }
            set
            {
                SelPropertyColorArray.BColorsPropertyValue = value;
            }
        }

        private void btnRegReset_Click(object sender, EventArgs e)
        {
            RegPropertyColorArray.ResetGrid();
        }
        
        private void btnSelReset_Click(object sender, EventArgs e)
        {
            SelPropertyColorArray.ResetGrid();
        }

        
    }
}
