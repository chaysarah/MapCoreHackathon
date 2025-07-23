using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;
using MCTester.MapWorld;

namespace MCTester.ButtonsImplementation
{
    public partial class ctrlSmallDeviceParams : UserControl
    {
        private static IDNMcMapDevice currSchemaDevice = null;
        public ctrlSmallDeviceParams()
        {
            InitializeComponent();

            cmbViewportAntiAliasingLevel.Items.AddRange(Enum.GetNames(typeof(DNEAntiAliasingLevel)));
            cmbViewportAntiAliasingLevel.Text = DNEAntiAliasingLevel._EAAL_NONE.ToString();

            cmbTerrainObjectsAntiAliasingLevel.Items.AddRange(Enum.GetNames(typeof(DNEAntiAliasingLevel)));
            cmbTerrainObjectsAntiAliasingLevel.Text = DNEAntiAliasingLevel._EAAL_NONE.ToString();

            if (MCTMapDevice.m_Device != null)
            {
                ntxNumBackgroundThreads.SetUInt32(MCTMapDevice.CurrDevice.BackgroundThreads);
                chxMultiScreenDevice.Checked = MCTMapDevice.CurrDevice.MultiScreenDevice;
                chxMultiThreads.Checked = MCTMapDevice.CurrDevice.MultiThreadedDevice;
                cmbViewportAntiAliasingLevel.SelectedText = MCTMapDevice.CurrDevice.ViewportAntiAliasingLevel.ToString();
                cmbTerrainObjectsAntiAliasingLevel.SelectedText = MCTMapDevice.CurrDevice.TerrainObjectsAntiAliasingLevel.ToString();
                ctrlBrowseLocalCacheFolder.FileName = MCTMapDevice.CurrDevice.MapLayersLocalCacheFolder;
                ntxLocalCacheMaxSize.SetUInt32(MCTMapDevice.CurrDevice.MapLayersLocalCacheSizeInMB);
                currSchemaDevice = MCTMapDevice.m_Device;
                DisableDeviceControls();
            }
            else
            {
                DNSInitParams initParams = new DNSInitParams();
                ntxNumBackgroundThreads.SetUInt32(initParams.uNumBackgroundThreads);
                chxMultiScreenDevice.Checked = initParams.bMultiScreenDevice;
                chxMultiThreads.Checked = initParams.bMultiThreadedDevice;
                cmbViewportAntiAliasingLevel.SelectedText = initParams.eViewportAntiAliasingLevel.ToString();
                cmbTerrainObjectsAntiAliasingLevel.SelectedText = initParams.eTerrainObjectsAntiAliasingLevel.ToString();
                ntxLocalCacheMaxSize.SetUInt32(initParams.uMapLayersLocalCacheSizeInMB);
            }
        }

        private void DisableDeviceControls()
        {
            foreach (Control cntl in groupBox1.Controls)
                cntl.Enabled = false;
        }

        public IDNMcMapDevice CheckDevice()
        {
            if (MCTMapDevice.m_Device == null)
            {
                MCTMapDevice mctMapDevice = new MCTMapDevice();
                mctMapDevice.BackgroundThreads = ntxNumBackgroundThreads.GetUInt32();
                mctMapDevice.MultiScreenDevice = chxMultiScreenDevice.Checked;
                mctMapDevice.MultiThreadedDevice = chxMultiThreads.Checked;
                mctMapDevice.ViewportAntiAliasingLevel = (DNEAntiAliasingLevel)Enum.Parse(typeof(DNEAntiAliasingLevel), cmbViewportAntiAliasingLevel.Text);
                mctMapDevice.TerrainObjectsAntiAliasingLevel = (DNEAntiAliasingLevel)Enum.Parse(typeof(DNEAntiAliasingLevel), cmbTerrainObjectsAntiAliasingLevel.Text);
                mctMapDevice.MapLayersLocalCacheFolder = ctrlBrowseLocalCacheFolder.FileName;
                mctMapDevice.MapLayersLocalCacheSizeInMB = ntxLocalCacheMaxSize.GetUInt32();
                mctMapDevice.LoggingLevel = DNELoggingLevel._ELL_HIGH;

                currSchemaDevice = mctMapDevice.CreateNewDevice();
                DisableDeviceControls();
            }
            return currSchemaDevice;
        }

        public bool IsInitilizeDeviceLocalCache()
        {
            bool result = false;
            if (ctrlBrowseLocalCacheFolder.FileName != String.Empty && ntxLocalCacheMaxSize.GetUInt32() > 0)
                result = true;
            else
                MessageBox.Show("Invalid device local cache params", "Error Local Cache", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return result;
        }

    }
}
