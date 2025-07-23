using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCTester.Controls;
using MapCore;

namespace MCTester.Controls
{
    public partial class CtrlPropertyVector2DArray : CtrlPropertyBase
    {
        public CtrlPropertyVector2DArray()
        {
            InitializeComponent();
        }

        public DNSArrayProperty<DNSMcVector2D> RegVector2DArrayPropertyValue
        {
            get
            {
                return RegPropertyVector2DArray.Vector2DArrayPropertyValue;
            }
            set
            {
                RegPropertyVector2DArray.Vector2DArrayPropertyValue = value;
            }
        }

        public DNSArrayProperty<DNSMcFVector2D> RegFVector2DArrayPropertyValue
        {
            get
            {
                return RegPropertyVector2DArray.FVector2DArrayPropertyValue;
            }
            set
            {
                RegPropertyVector2DArray.FVector2DArrayPropertyValue = value;
            }
        }


        public DNSArrayProperty<DNSMcVector2D> SelVector2DArrayPropertyValue
        {
            get
            {
                return SelPropertyVector2DArray.Vector2DArrayPropertyValue;
            }
            set
            {
                SelPropertyVector2DArray.Vector2DArrayPropertyValue = value;
            }
        }

        public DNSArrayProperty<DNSMcFVector2D> SelFVector2DArrayPropertyValue
        {
            get
            {
                return SelPropertyVector2DArray.FVector2DArrayPropertyValue;
            }
            set
            {
                SelPropertyVector2DArray.FVector2DArrayPropertyValue = value;
            }
        }

        private void btnSelReset_Click(object sender, EventArgs e)
        {
            SelPropertyVector2DArray.ResetGrid();
        }

        private void btnRegReset_Click(object sender, EventArgs e)
        {
            RegPropertyVector2DArray.ResetGrid();
        }
    }
}
