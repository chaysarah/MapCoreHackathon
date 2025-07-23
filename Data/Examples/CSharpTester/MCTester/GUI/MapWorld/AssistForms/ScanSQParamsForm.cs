using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld
{
    public partial class ScanSQParamsForm : Form
    {
        DNSQueryParams m_CurrSQParams;
        private DNSQueryParams dNSQueryParams;
        
        public ScanSQParamsForm()
        {
            InitializeComponent();
            IDNMcMapViewport currViewport = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport;
            ctrlQueryParamsScan.LoadItem(currViewport.OverlayManager);
           
        }

        public ScanSQParamsForm(DNSQueryParams dNSQueryParams):this()
        {
            this.dNSQueryParams = dNSQueryParams;
            ctrlQueryParamsScan.SetQueryParams(dNSQueryParams);
        }

        private void btnScanSQParamsOK_Click(object sender, EventArgs e)
        {
            m_CurrSQParams = ctrlQueryParamsScan.GetQueryParams();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public DNSQueryParams CurrSQParams
        {
            get { return m_CurrSQParams; }
        }
    }


}