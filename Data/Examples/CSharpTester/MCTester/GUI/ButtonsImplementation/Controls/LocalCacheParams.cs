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
    public partial class LocalCacheParams : UserControl
    {
        public LocalCacheParams()
        {
            InitializeComponent();
        }

        public string SubFolderPath
        {
            get { return tbLocalCacheSubFolder.Text; }
            set { tbLocalCacheSubFolder.Text = value; }
        }

        public DNSLocalCacheLayerParams GetLocalCacheLayerParams()
        {
            DNSLocalCacheLayerParams localCacheLayerParams = new DNSLocalCacheLayerParams();
            localCacheLayerParams.strLocalCacheSubFolder = SubFolderPath;
            return localCacheLayerParams;
        }

        public void SetLocalCacheLayerParams(DNSLocalCacheLayerParams layersParams)
        {
            SubFolderPath = layersParams.strLocalCacheSubFolder;
        }

        internal void ClearData()
        {
            SubFolderPath = "";
        }
    }
}
