using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.GUI.Map;
using MCTester.ObjectWorld.OverlayManagerWorld.WizardForms;
using MCTester.GUI;
using MCTester.MapWorld;
using MapCore.Common;
using MCTester.Managers;


namespace MCTester.Controls
{
    public partial class CreateDataSQControl : UserControl
    {
        private IDNMcMapDevice m_device;
        private IDNMcOverlayManager m_overlayManager;
        private MCTMapDevice MapDevice;
        private int currentResourceTypeIndex;
        public CreateDataSQControl()
        {
            InitializeComponent();

            cmbResourceLocationType.Items.AddRange(Enum.GetNames(typeof(DNEResourceLocationType)));
            cmbResourceLocationType.Text = DNEResourceLocationType._ERLT_FOLDER.ToString();

            localPathArray1.IsFolder = true;
            currentResourceTypeIndex = 0;
            localPathArray1.Filter = "Zip files (*.zip) | *.zip;";

            if (MCTMapDevice.CurrDevice != null)
            {
                m_device = MCTMapDevice.m_Device;
                btnCreateDevice.Enabled = false;

                ctrlDeviceParams1.SetDeviceParams(MCTMapDevice.CurrDevice.GetDeviceParams());
                ctrlDeviceParams1.Enabled = false;
                LoadLocalCahceParams(false);
                tabControl1.SelectedTab = tpCreateDataSQ;

            }
            else
            {
                EnableLoadResourceTab(false);
                DNSInitParams initParams = new DNSInitParams();

                ctrlDeviceParams1.SetDeviceParams(new DNSInitParams());
            }
        }

        private void EnableLoadResourceTab(bool isEnabled)
        {
            gbLoadResource.Enabled = isEnabled;
            gbUnloadResource.Enabled = isEnabled;
        }

        public DNSCreateDataSQ CreateData
        {
            get
            {
                DNSCreateDataSQ ret = new DNSCreateDataSQ();
                ret.pDevice = m_device;
                ret.pOverlayManager = m_overlayManager;
                ret.CoordinateSystem = ctrlCreateDataSQGridCoordinateSystem.GridCoordinateSystem;
                ret.uViewportID = ViewportID;

                return ret;
            }
        }

        private void btnCreateDevice_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_device == null)
                {
                    DNSInitParams initParams = ctrlDeviceParams1.GetDeviceParams();

                    MapDevice = new MCTMapDevice();

                    Manager_MCGeneralDefinitions.m_NumBackgroundThreads = initParams.uNumBackgroundThreads;
                    Manager_MCGeneralDefinitions.m_MultiScreenDevice = initParams.bMultiScreenDevice;
                    Manager_MCGeneralDefinitions.m_MultiThreadedDevice = initParams.bMultiThreadedDevice;

                    MapDevice.SetDeviceParams(initParams);

                    m_device = MapDevice.CreateDevice();

                    if (m_device != null)
                    {
                        ctrlDeviceParams1.Enabled = false;
                        EnableLoadResourceTab(true);
                        btnCreateDevice.Enabled = false;
                        Manager_MCGeneralDefinitions.mFirstMapLoaderDefinition = false;
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcMapDevice.Create", McEx);
            }
        }

        private void btnSelectOverlayManager_Click(object sender, EventArgs e)
        {
            OverlayManagerWizardForm overlaymWizFrm = new OverlayManagerWizardForm(m_overlayManager);
            //m_overlayManager = null;
            overlaymWizFrm.ShowDialog();
            if (overlaymWizFrm.OverlayManager != null)
            {
                lblOverlayManagerSelectionStatus.Text = "Selected!";
                m_overlayManager = overlaymWizFrm.OverlayManager;
                btnRemoveOM.Enabled = true;
            }
            else
            {
                lblOverlayManagerSelectionStatus.Text = "Not Selected";                
            }
        }       
        
        public uint ViewportID
        {
            get { return this.ntbViewportID.GetUInt32(); }
        }

        private void btnUnloadResource_Click(object sender, EventArgs e)
        {
            MCTMapDevice.CurrDevice.UnloadResourceGroup(tbUnloadGroupName.Text);

        }

        private void btnLoadResource_Click(object sender, EventArgs e)
        {
            MCTMapDevice.CurrDevice.LoadResourceGroup(txtGroupName.Text, localPathArray1.PathArray, (DNEResourceLocationType)Enum.Parse(typeof(DNEResourceLocationType), cmbResourceLocationType.Text));
        }

        private void cbResourceLocationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool currIsFolder = localPathArray1.IsFolder;

            if (cmbResourceLocationType.SelectedItem.ToString() == DNEResourceLocationType._ERLT_FOLDER.ToString() || cmbResourceLocationType.SelectedItem.ToString() == DNEResourceLocationType._ERLT_FOLDER_RECURSIVE.ToString())
                localPathArray1.IsFolder = true;
            else
                localPathArray1.IsFolder = false;
            
            if (localPathArray1.PathArray.Length > 0 && currIsFolder != localPathArray1.IsFolder)
            {
                DialogResult result = MessageBox.Show("This change delete table rows, Do you want to continue? ", "Change Type", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    localPathArray1.ResetTable();
                    currentResourceTypeIndex = cmbResourceLocationType.SelectedIndex;
                }
                else
                {
                    this.cmbResourceLocationType.SelectedIndexChanged -= new System.EventHandler(this.cbResourceLocationType_SelectedIndexChanged);
                    localPathArray1.IsFolder = currIsFolder;
                    cmbResourceLocationType.SelectedIndex = currentResourceTypeIndex;
                    this.cmbResourceLocationType.SelectedIndexChanged += new System.EventHandler(this.cbResourceLocationType_SelectedIndexChanged);
                }
            }

            currentResourceTypeIndex = cmbResourceLocationType.SelectedIndex;
        }

        private void btnRemoveMapLayersLocalCache_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure to remove map layers local cache?", "Remove Local Cache", MessageBoxButtons.OKCancel);
                if(result == DialogResult.OK)
                {
                    DNMcMapDevice.RemoveMapLayersLocalCache();
                    MessageBox.Show("Remove Map Layers Local Cache Succeed");
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcMapDevice.RemoveMapLayersLocalCache", McEx);
            }
        }

        private void btnSetMapLayersLocalCacheSize_Click(object sender, EventArgs e)
        {
            try
            {
                DNMcMapDevice.SetMapLayersLocalCacheSize(ntbLocalCacheMaxSizeParams.GetUInt32());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcMapDevice.SetMapLayersLocalCacheSize", McEx);
            }
        }
        private void btnRemoveMapLayerFromLocalCache_Click(object sender, EventArgs e)
        {
            try
            {
                string strFolderName = txtLocalCacheSubFolder.Text;
                string strMsg = "Are you sure to remove map layer from local cache?";
                if(strFolderName == String.Empty)
                    strMsg = "Are you sure to remove all map layers from local cache?";
                DialogResult result = MessageBox.Show(strMsg, "Remove Local Cache", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    DNMcMapDevice.RemoveMapLayerFromLocalCache(strFolderName);
                    MessageBox.Show("Remove Map Layer From Local Cache Succeed");
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcMapDevice.RemoveMapLayerFromLocalCache", McEx);
            }
        }

        private void btnGetLocalCacheParams_Click(object sender, EventArgs e)
        {
            try
            {
                LoadLocalCahceParams(true);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcMapDevice.RemoveMapLayerFromLocalCache", McEx);
            }
        }

        private void tpLocalCache_Click(object sender, EventArgs e)
        {

        }

        private void LoadLocalCahceParams(bool isLoadLayersParams)
        {
            string strLocalCacheFolder = "";
            uint uLocalCacheMaxSize = 0;
            uint uLocalCacheCurrentSize = 0;
            DNSLocalCacheLayerParams[] arr_layersLocalCacheParams = null;

            dgvLocalCacheLayersParams.Rows.Clear();

            if (isLoadLayersParams == false)
                DNMcMapDevice.GetMapLayersLocalCacheParams(ref strLocalCacheFolder, ref uLocalCacheMaxSize, ref uLocalCacheCurrentSize);
            else
                DNMcMapDevice.GetMapLayersLocalCacheParams(ref strLocalCacheFolder, ref uLocalCacheMaxSize, ref uLocalCacheCurrentSize, ref arr_layersLocalCacheParams);

            tbLocalCacheFolder.Text = strLocalCacheFolder;
            ntbLocalCacheMaxSizeParams.SetUInt32(uLocalCacheMaxSize);
            ntbLocalCacheCurrentSizeParams.SetUInt32(uLocalCacheCurrentSize);

            if (arr_layersLocalCacheParams != null && arr_layersLocalCacheParams.Length > 0)
            {
                foreach (DNSLocalCacheLayerParams layerParams in arr_layersLocalCacheParams)
                {
                    dgvLocalCacheLayersParams.Rows.Add(layerParams.strLocalCacheSubFolder, layerParams.strOriginalFolder);
                }
            }
        }

        private void btnRemoveOM_Click(object sender, EventArgs e)
        {
            m_overlayManager = null;
            lblOverlayManagerSelectionStatus.Text = "Not Selected";
            btnRemoveOM.Enabled = false;
        }

    }
}
