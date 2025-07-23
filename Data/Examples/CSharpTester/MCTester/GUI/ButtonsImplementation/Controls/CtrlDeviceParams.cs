using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCTester.MapWorld;
using MapCore;
using MCTester.Managers;
using MapCore.Common;

namespace MCTester.Controls
{
    public partial class CtrlDeviceParams : UserControl
    {
        private IDNMcMapDevice m_device;

        public CtrlDeviceParams()
        {
            InitializeComponent();
            cmbLoggingLevel.Items.AddRange(Enum.GetNames(typeof(DNELoggingLevel)));
            cmbTerrainObjectsQuality.Items.AddRange(Enum.GetNames(typeof(DNETerrainObjectsQuality)));
            cmbRenderingSystem.Items.AddRange(Enum.GetNames(typeof(DNERenderingSystem)));
            cmbViewportAntiAliasingLevel.Items.AddRange(Enum.GetNames(typeof(DNEAntiAliasingLevel)));
            cmbTerrainObjectsAntiAliasingLevel.Items.AddRange(Enum.GetNames(typeof(DNEAntiAliasingLevel)));

            ntbNumBackgroundThreads.SetUInt32(Manager_MCGeneralDefinitions.m_NumBackgroundThreads);
            chxbMultiScreenDevice.Checked = Manager_MCGeneralDefinitions.m_MultiScreenDevice;
            chxMultiThreadedDevice.Checked = Manager_MCGeneralDefinitions.m_MultiThreadedDevice;

            if (MCTMapDevice.CurrDevice != null)
            {
                m_device = MCTMapDevice.m_Device;
                ntbNumBackgroundThreads.Enabled = false;
                gbCreateDevice.Enabled = false;
            }
        }

        private void DisableControl()
        {
            if (MCTMapDevice.CurrDevice != null)
            {
                ntbNumBackgroundThreads.Enabled = false;
                gbCreateDevice.Enabled = false;
            }
        }

        public DNSInitParams GetDeviceParams()
        {
            DNSInitParams initParams = new DNSInitParams();
            initParams.uNumBackgroundThreads = ntbNumBackgroundThreads.GetUInt32();
            initParams.bMultiScreenDevice = chxbMultiScreenDevice.Checked;
            initParams.bMultiThreadedDevice = chxMultiThreadedDevice.Checked;
            initParams.eLoggingLevel = (DNELoggingLevel)Enum.Parse(typeof(DNELoggingLevel), cmbLoggingLevel.Text);
            initParams.bOpenConfigWindow = chkOpenConfigWindow.Checked;
            initParams.eTerrainObjectsQuality = (DNETerrainObjectsQuality)Enum.Parse(typeof(DNETerrainObjectsQuality), cmbTerrainObjectsQuality.Text);
            initParams.eRenderingSystem = (DNERenderingSystem)Enum.Parse(typeof(DNERenderingSystem), cmbRenderingSystem.Text);
            initParams.bShaderBasedRenderingSystem = bShaderBasedRenderingSystem.Checked;
            initParams.strLogFileDirectory = ctrlBrowseLogFileDirectory.FileName;
            initParams.strConfigFilesDirectory = ctrlBrowseConfigFilesDirectory.FileName;
            initParams.bIgnoreRasterLayerMipmaps = chxIgnoreRasterLayerMipmaps.Checked;
            initParams.fObjectsBatchGrowthRatio = ntxObjectsBatchGrowthRatio.GetFloat();
            initParams.uObjectsTexturesAtlasSize = ntxObjectsTexturesAtlasSize.GetUInt32();
            initParams.bObjectsTexturesAtlas16bit = chxObjectsTexturesAtlas16bit.Checked;
            initParams.bDisableDepthBuffer = chxDisableDepthBuffer.Checked;
            initParams.bEnableObjectsBatchEnlarging = chxEnableObjectsBatchEnlarging.Checked;
            initParams.uObjectsBatchInitialNumIndices = ntxObjectsBatchInitialNumIndices.GetUInt32();
            initParams.uObjectsBatchInitialNumVertices = ntxObjectsBatchInitialNumVertices.GetUInt32();
            initParams.bEnableGLQuadBufferStereo = chxEnableGLQuadBufferStereo.Checked;
            initParams.bAlignScreenSizeObjects = chxAlignScreenSizeObjects.Checked;
            initParams.bPreferUseTerrainTileRenderTargets = chxPreferUseTerrainTileRenderTargets.Checked;
            initParams.uNumTerrainTileRenderTargets = ntbNumTerrainTileRenderTargets.GetUInt32();
            initParams.eViewportAntiAliasingLevel = (DNEAntiAliasingLevel)Enum.Parse(typeof(DNEAntiAliasingLevel), cmbViewportAntiAliasingLevel.Text);
            initParams.eTerrainObjectsAntiAliasingLevel = (DNEAntiAliasingLevel)Enum.Parse(typeof(DNEAntiAliasingLevel), cmbTerrainObjectsAntiAliasingLevel.Text);
            initParams.strMapLayersLocalCacheFolder = ctrlBrowseLocalCacheFolder.FileName;
            initParams.uMapLayersLocalCacheSizeInMB = ntxLocalCacheMaxSize.GetUInt32();
            initParams.uDtmVisualizationPrecision = ntbShadingDtmPrecision.GetUInt32();
            initParams.uMainMonitorIndex = ntbMainMonitorIndex.GetUInt32();
            initParams.uWebRequestRetryCount = ntbWebRequestRetryCount.GetUInt32();
            initParams.uAsyncQueryTilesMaxActiveWebRequests = ntbAsyncQueryTilesMaxActiveWebRequests.GetUInt32();

            return initParams;
        }

        public void SetDeviceParams(DNSInitParams initParams)
        {
            ntbNumBackgroundThreads.SetUInt32(initParams.uNumBackgroundThreads);
            cmbLoggingLevel.Text = initParams.eLoggingLevel.ToString();
            chkOpenConfigWindow.Checked = initParams.bOpenConfigWindow;
            cmbViewportAntiAliasingLevel.Text = initParams.eViewportAntiAliasingLevel.ToString();
            cmbTerrainObjectsAntiAliasingLevel.Text = initParams.eTerrainObjectsAntiAliasingLevel.ToString();
            cmbTerrainObjectsQuality.Text = initParams.eTerrainObjectsQuality.ToString();
            ntbShadingDtmPrecision.SetUInt32(initParams.uDtmVisualizationPrecision);
            ntxObjectsBatchGrowthRatio.SetFloat(initParams.fObjectsBatchGrowthRatio);
            ntxObjectsTexturesAtlasSize.SetUInt32(initParams.uObjectsTexturesAtlasSize);
            chxObjectsTexturesAtlas16bit.Checked = initParams.bObjectsTexturesAtlas16bit;
            chxDisableDepthBuffer.Checked = initParams.bDisableDepthBuffer;
            cmbRenderingSystem.Text = initParams.eRenderingSystem.ToString();
            bShaderBasedRenderingSystem.Checked = initParams.bShaderBasedRenderingSystem;
            chxIgnoreRasterLayerMipmaps.Checked = initParams.bIgnoreRasterLayerMipmaps;
            chxEnableGLQuadBufferStereo.Checked = initParams.bEnableGLQuadBufferStereo;
            ntbNumTerrainTileRenderTargets.SetUInt32(initParams.uNumTerrainTileRenderTargets);
            chxPreferUseTerrainTileRenderTargets.Checked = initParams.bPreferUseTerrainTileRenderTargets;
            chxMultiThreadedDevice.Checked = initParams.bMultiThreadedDevice;
            chxbMultiScreenDevice.Checked = initParams.bMultiScreenDevice;
            ntbMainMonitorIndex.SetUInt32(initParams.uMainMonitorIndex);
            ntxObjectsBatchInitialNumVertices.SetUInt32(initParams.uObjectsBatchInitialNumVertices);
            ntxObjectsBatchInitialNumIndices.SetUInt32(initParams.uObjectsBatchInitialNumIndices);
            chxEnableObjectsBatchEnlarging.Checked = initParams.bEnableObjectsBatchEnlarging;
            chxAlignScreenSizeObjects.Checked = initParams.bAlignScreenSizeObjects;
            ntxLocalCacheMaxSize.SetUInt32(initParams.uMapLayersLocalCacheSizeInMB);
            ctrlBrowseLocalCacheFolder.FileName = initParams.strMapLayersLocalCacheFolder;
            ntbWebRequestRetryCount.SetUInt32(initParams.uWebRequestRetryCount);
            ntbAsyncQueryTilesMaxActiveWebRequests.SetUInt32(initParams.uAsyncQueryTilesMaxActiveWebRequests);
        }
    }
}
