using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.Managers.MapWorld;
using UnmanagedWrapper;

namespace MCTester.GUI.Forms
{
    public partial class QueryParamsForm : Form
    {
        private DNSQueryParams m_QueryParams;
        
        public QueryParamsForm()
        {
            InitializeComponent();
        }

        private void btnStandaloneSQ_Click(object sender, EventArgs e)
        {
            this.Close();

            m_QueryParams = ctrlSpatialQueriesParams.GetQueryParams();
            IDNMcMapViewport mcMapViewport = null;
            if (MCTMapFormManager.MapForm != null)
                mcMapViewport = MCTMapFormManager.MapForm.Viewport;
            
         
        }
    }
}