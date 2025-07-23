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
    public partial class frmLocalCacheParam : Form
    {
        private string m_localCacheFolder;
        public frmLocalCacheParam()
        {
            InitializeComponent();
            
        }

        public frmLocalCacheParam(DNSLocalCacheLayerParams localCacheLayerParams):this()
        {
            LocalCacheLayerParams = localCacheLayerParams;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_localCacheFolder = ctrlLocalCacheParams.SubFolderPath;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        public String LocalCacheFolder
        {
            get { return m_localCacheFolder; }
            set { m_localCacheFolder = value; }
        }

        public DNSLocalCacheLayerParams LocalCacheLayerParams
        {
            get { return ctrlLocalCacheParams.GetLocalCacheLayerParams(); }
            set { ctrlLocalCacheParams.SetLocalCacheLayerParams(value); }
        }
    }
}
