using MapCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCTester.MapWorld.Assist_Forms
{
    public partial class frmRawVectorParams : Form
    {
        public frmRawVectorParams()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public DNSRawVectorParams RawVectorParams
        {
            get
            {
                return rawVectorParams1.RawVectorParams;
            }
            set
            {
                rawVectorParams1.RawVectorParams = value;
            }
        }

        public IDNMcGridCoordinateSystem TargetGridCoordinateSystem
        {
            get { return rawVectorParams1.TargetGridCoordinateSystem; }
            set { rawVectorParams1.TargetGridCoordinateSystem = value; }
        }

        internal void SetDisableStyling(string rawVectorFileName)
        {
            rawVectorParams1.SetDisableStyling(rawVectorFileName);
        }
    }
}
