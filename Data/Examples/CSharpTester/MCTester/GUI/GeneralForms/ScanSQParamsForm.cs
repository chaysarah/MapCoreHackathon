using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.GUI.Forms
{
    public partial class ScanSQParamsForm : Form
    {
        DNSQueryParams m_SQParams;

        public ScanSQParamsForm()
        {
            InitializeComponent();
            m_SQParams = new DNSQueryParams();
        }

        private void btnScanSQParamsOK_Click(object sender, EventArgs e)
        {
            m_SQParams = ctrlQueryParamsScan.GetQueryParams();
            this.Close();
        }

        public DNSQueryParams ScanSQParams
        {
            get { return m_SQParams; }
        }
    }
}