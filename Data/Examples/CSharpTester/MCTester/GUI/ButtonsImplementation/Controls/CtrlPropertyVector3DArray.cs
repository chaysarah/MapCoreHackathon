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
    public partial class CtrlPropertyVector3DArray : CtrlPropertyBase
    {
        public CtrlPropertyVector3DArray()
        {
            InitializeComponent();
        }

        public DNSArrayProperty<DNSMcVector3D> RegVector3DArrayPropertyValue
        {
            get
            {
                return RegPropertyVector3DArray.Vector3DArrayPropertyValue;
            }
            set
            {
                RegPropertyVector3DArray.Vector3DArrayPropertyValue = value;
            }
        }

        public DNSArrayProperty<DNSMcFVector3D> RegFVector3DArrayPropertyValue
        {
            get
            {
                return RegPropertyVector3DArray.FVector3DArrayPropertyValue;
            }
            set
            {
                RegPropertyVector3DArray.FVector3DArrayPropertyValue = value;
            }
        }


        public DNSArrayProperty<DNSMcVector3D> SelVector3DArrayPropertyValue
        {
            get
            {
                return SelPropertyVector3DArray.Vector3DArrayPropertyValue;
            }
            set
            {
                SelPropertyVector3DArray.Vector3DArrayPropertyValue = value;
            }
        }

        public DNSArrayProperty<DNSMcFVector3D> SelFVector3DArrayPropertyValue
        {
            get
            {
                return SelPropertyVector3DArray.FVector3DArrayPropertyValue;
            }
            set
            {
                SelPropertyVector3DArray.FVector3DArrayPropertyValue = value;
            }
        }

        private void btnSelReset_Click(object sender, EventArgs e)
        {
            SelPropertyVector3DArray.ResetGrid();
        }

        private void btnRegReset_Click(object sender, EventArgs e)
        {
            RegPropertyVector3DArray.ResetGrid();
        }
    }
}
