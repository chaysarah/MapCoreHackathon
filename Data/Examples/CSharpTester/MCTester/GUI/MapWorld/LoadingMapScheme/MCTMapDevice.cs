using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using System.Runtime.Serialization;
using System.IO;
using MCTester.Managers;

namespace MCTester.MapWorld
{
    [DataContract]
    public class MCTMapDevice
    {
        public static IDNMcMapDevice m_Device = null;
        private static MCTMapDevice m_CurrDevice;

        private uint m_BackgroundThreads;
        private bool m_OpenConfigWindow;
        private DNELoggingLevel m_LoggingLevel;
        private DNETerrainObjectsQuality m_TerrainObjectsQuality;
        private DNERenderingSystem m_RenderingSystem;
        private bool m_bShaderBasedRenderingSystem;
        private string m_LogFileDirectory;
        private string m_ConfigFilesDirectory;
        private bool m_bIgnoreRasterLayerMipmaps;
        private float m_ObjectsBatchGrowthRatio;
        private uint m_ObjectsTexturesAtlasSize;
        private bool m_ObjectsTexturesAtlas16bit;
        private bool m_DisableDepthBuffer;
        private bool m_EnableObjectsBatchEnlarging;
        private bool m_EnableGLQuadBufferStereo;
        private bool m_MultiScreenDevice;
        private uint m_ObjectsBatchInitialNumVertices;
        private uint m_ObjectsBatchInitialNumIndices;
        private bool m_AlignScreenSizeObjects;
        private bool m_MultiThreadedDevice;
        private bool m_PreferUseTerrainTileRenderTarget;
        private uint m_NumTerrainTileRenderTargets;
        private DNEAntiAliasingLevel m_ViewportAntiAliasingLevel;
        private DNEAntiAliasingLevel m_TerrainObjectsAntiAliasingLevel;
        private string m_MapLayersLocalCacheFolder = null;
        private uint m_MapLayersLocalCacheSizeInMB;
        private uint m_DtmVisualizationPrecision;
        private uint m_MainMonitorIndex;
        private uint m_WebRequestRetryCount;
        private uint m_AsyncQueryTilesMaxActiveWebRequests;

        public static bool IsInitilizeDeviceLocalCache()
        {
            bool result = false;
            if (CurrDevice != null && CurrDevice.MapLayersLocalCacheFolder != String.Empty && CurrDevice.MapLayersLocalCacheSizeInMB > 0)
                result = true;
            return result;
        }

        public MCTMapDevice()
        {
            DNSInitParams initParams = new DNSInitParams();
            m_BackgroundThreads = initParams.uNumBackgroundThreads;
            m_OpenConfigWindow = initParams.bOpenConfigWindow;
            m_LoggingLevel = initParams.eLoggingLevel;
            m_TerrainObjectsQuality = initParams.eTerrainObjectsQuality;
            m_RenderingSystem = initParams.eRenderingSystem;
            m_bShaderBasedRenderingSystem = initParams.bShaderBasedRenderingSystem;
            m_bIgnoreRasterLayerMipmaps = initParams.bIgnoreRasterLayerMipmaps;
            m_ObjectsBatchGrowthRatio = initParams.fObjectsBatchGrowthRatio;
            m_ObjectsTexturesAtlasSize = initParams.uObjectsTexturesAtlasSize;
            m_ObjectsTexturesAtlas16bit = initParams.bObjectsTexturesAtlas16bit;
            m_DisableDepthBuffer = initParams.bDisableDepthBuffer;
            m_EnableObjectsBatchEnlarging = initParams.bEnableObjectsBatchEnlarging;
            m_MultiScreenDevice = initParams.bMultiScreenDevice;
            m_ObjectsBatchInitialNumVertices = initParams.uObjectsBatchInitialNumVertices;
            m_ObjectsBatchInitialNumIndices = initParams.uObjectsBatchInitialNumIndices;
            m_AlignScreenSizeObjects = initParams.bAlignScreenSizeObjects;
            m_MultiThreadedDevice = initParams.bMultiThreadedDevice;
            m_PreferUseTerrainTileRenderTarget = initParams.bPreferUseTerrainTileRenderTargets;
            m_NumTerrainTileRenderTargets = initParams.uNumTerrainTileRenderTargets;
            m_ViewportAntiAliasingLevel = initParams.eViewportAntiAliasingLevel;
            m_TerrainObjectsAntiAliasingLevel = initParams.eTerrainObjectsAntiAliasingLevel;
            m_MapLayersLocalCacheSizeInMB = initParams.uMapLayersLocalCacheSizeInMB;
            m_DtmVisualizationPrecision = initParams.uDtmVisualizationPrecision;
            m_MainMonitorIndex = initParams.uMainMonitorIndex;
            m_WebRequestRetryCount = initParams.uWebRequestRetryCount;
            m_AsyncQueryTilesMaxActiveWebRequests = initParams.uAsyncQueryTilesMaxActiveWebRequests;
        }

        public IDNMcMapDevice CreateDevice(bool isUseDefualtValues = true)
        {
            try
            {
                if (m_Device == null)
                {
                    if (isUseDefualtValues)
                    {
                        BackgroundThreads = Manager_MCGeneralDefinitions.m_NumBackgroundThreads;
                        MultiScreenDevice = Manager_MCGeneralDefinitions.m_MultiScreenDevice;
                        MultiThreadedDevice = Manager_MCGeneralDefinitions.m_MultiThreadedDevice;
                    }
                    
                    m_Device = DNMcMapDevice.Create(GetDeviceParams());
                    
                    m_CurrDevice = this;
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("CreateDevice", McEx);
            }
            return m_Device;
        }


        public void LoadResourceGroup(string groupName, string[] pathArray, DNEResourceLocationType resourceType)
        {
            try
            {
                DNMcMapDevice.LoadResourceGroup(groupName, pathArray, resourceType);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcMapDevice.LoadResourceGroup", McEx);
            }
        }

        public void UnloadResourceGroup(string groupName)
        {
            try
            {
                DNMcMapDevice.UnloadResourceGroup(groupName);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcMapDevice.UnloadResourceGroup", McEx);
            }
        }


        public IDNMcMapDevice CreateNewDevice()
        {
            m_Device = null;
            return CreateDevice(false);
        }

        [DataMember]
        public bool OpenConfigWindow
        {
            get { return m_OpenConfigWindow; }
            set { m_OpenConfigWindow = value; }
        }

        public DNELoggingLevel LoggingLevel
        {
            get { return m_LoggingLevel; }
            set { m_LoggingLevel = value; }
        }

        [DataMember]
        public string LoggingLevelText
        {
            get { return m_LoggingLevel.ToString(); }
            set { m_LoggingLevel = (DNELoggingLevel)Enum.Parse(typeof(DNELoggingLevel), value); }
        }

        public DNETerrainObjectsQuality TerrainObjectsQuality
        {
            get { return m_TerrainObjectsQuality; }
            set { m_TerrainObjectsQuality = value; }
        }

        [DataMember]
        public string TerrainObjectsQualityText
        {
            get { return m_TerrainObjectsQuality.ToString(); }
            set { m_TerrainObjectsQuality = (DNETerrainObjectsQuality)Enum.Parse(typeof(DNETerrainObjectsQuality), value); }
        }

        public DNERenderingSystem RenderingSystem
        {
            get { return m_RenderingSystem; }
            set { m_RenderingSystem = value; }
        }

        [DataMember]
        public string RenderingSystemText
        {
            get { return m_RenderingSystem.ToString(); }
            set { m_RenderingSystem = (DNERenderingSystem)Enum.Parse(typeof(DNERenderingSystem), value); }
        }

        [DataMember]
        public bool ShaderBasedRenderingSystem
        {
            get { return m_bShaderBasedRenderingSystem; }
            set { m_bShaderBasedRenderingSystem = value; }
        }

        [DataMember]
        public string LogFileDirectory
        {
            get { return m_LogFileDirectory; }
            set { m_LogFileDirectory = value; }
        }

        [DataMember]
        public string ConfigFilesDirectory
        {
            get { return m_ConfigFilesDirectory; }
            set { m_ConfigFilesDirectory = value; }
        }

        [DataMember]
        public bool IgnoreRasterLayerMipmaps
        {
            get { return m_bIgnoreRasterLayerMipmaps; }
            set { m_bIgnoreRasterLayerMipmaps = value; }
        }

        [DataMember]
        public uint BackgroundThreads
        {
            get { return m_BackgroundThreads; }
            set { m_BackgroundThreads = value; }
        }

        [DataMember]
        public float ObjectsBatchGrowthRatio
        {
            get { return m_ObjectsBatchGrowthRatio; }
            set { m_ObjectsBatchGrowthRatio = value; }
        }

        [DataMember]
        public uint ObjectsTexturesAtlasSize
        {
            get { return m_ObjectsTexturesAtlasSize; }
            set { m_ObjectsTexturesAtlasSize = value; }
        }

        [DataMember]
        public bool ObjectsTexturesAtlas16bit
        {
            get { return m_ObjectsTexturesAtlas16bit; }
            set { m_ObjectsTexturesAtlas16bit = value; }
        }

        [DataMember]
        public bool DisableDepthBuffer
        {
            get { return m_DisableDepthBuffer; }
            set { m_DisableDepthBuffer = value; }
        }

        [DataMember]
        public bool EnableObjectsBatchEnlarging
        {
            get { return m_EnableObjectsBatchEnlarging; }
            set { m_EnableObjectsBatchEnlarging = value; }
        }

        [DataMember]
        public bool EnableGLQuadBufferStereo
        {
            get { return m_EnableGLQuadBufferStereo; }
            set { m_EnableGLQuadBufferStereo = value; }
        }

        [DataMember]
        public bool MultiScreenDevice
        {
            get { return m_MultiScreenDevice; }
            set { m_MultiScreenDevice = value; }
        }

        [DataMember]
        public uint ObjectsBatchInitialNumVertices
        {
            get { return m_ObjectsBatchInitialNumVertices; }
            set { m_ObjectsBatchInitialNumVertices = value; }
        }

        [DataMember]
        public uint ObjectsBatchInitialNumIndices
        {
            get { return m_ObjectsBatchInitialNumIndices; }
            set { m_ObjectsBatchInitialNumIndices = value; }
        }

        [DataMember]
        public bool AlignScreenSizeObjects
        {
            get { return m_AlignScreenSizeObjects; }
            set { m_AlignScreenSizeObjects = value; }
        }

        [DataMember]
        public bool MultiThreadedDevice
        {
            get { return m_MultiThreadedDevice; }
            set { m_MultiThreadedDevice = value; }
        }

        [DataMember]
        public bool PreferUseTerrainTileRenderTargets
        {
            get { return m_PreferUseTerrainTileRenderTarget; }
            set { m_PreferUseTerrainTileRenderTarget = value; }
        }

        [DataMember]
        public uint NumTerrainTileRenderTargets
        {
            get { return m_NumTerrainTileRenderTargets; }
            set { m_NumTerrainTileRenderTargets = value; }
        }

        [DataMember]
        public uint DtmVisualizationPrecision
        {
            get { return m_DtmVisualizationPrecision; }
            set { m_DtmVisualizationPrecision = value; }
        }

        [DataMember]
        public uint MainMonitorIndex
        {
            get { return m_MainMonitorIndex; }
            set { m_MainMonitorIndex = value; }
        }

        [DataMember]
        public uint WebRequestRetryCount
        {
            get { return m_WebRequestRetryCount; }
            set { m_WebRequestRetryCount = value; }
        }

        [DataMember]
        public uint AsyncQueryTilesMaxActiveWebRequests
        {
            get { return m_AsyncQueryTilesMaxActiveWebRequests; }
            set { m_AsyncQueryTilesMaxActiveWebRequests = value; }
        }

        public DNEAntiAliasingLevel ViewportAntiAliasingLevel
        {
            get { return m_ViewportAntiAliasingLevel; }
            set { m_ViewportAntiAliasingLevel = value; }
        }

        [DataMember]
        public string ViewportAntiAliasingLevelText
        {
            get { return m_ViewportAntiAliasingLevel.ToString(); }
            set { m_ViewportAntiAliasingLevel = (DNEAntiAliasingLevel)Enum.Parse(typeof(DNEAntiAliasingLevel), value); }
        }

        public DNEAntiAliasingLevel TerrainObjectsAntiAliasingLevel
        {
            get { return m_TerrainObjectsAntiAliasingLevel; }
            set { m_TerrainObjectsAntiAliasingLevel = value; }
        }

        [DataMember]
        public string TerrainObjectsAntiAliasingLevelText
        {
            get { return m_TerrainObjectsAntiAliasingLevel.ToString(); }
            set { m_TerrainObjectsAntiAliasingLevel = (DNEAntiAliasingLevel)Enum.Parse(typeof(DNEAntiAliasingLevel), value); }
        }

        [DataMember]
        public string MapLayersLocalCacheFolder
        {
            get { return m_MapLayersLocalCacheFolder; }
            set { m_MapLayersLocalCacheFolder = value; }
        }

        [DataMember]
        public uint MapLayersLocalCacheSizeInMB
        {
            get { return m_MapLayersLocalCacheSizeInMB; }
            set { m_MapLayersLocalCacheSizeInMB = value; }
        }

        public static MCTMapDevice CurrDevice
        {
            get { return m_CurrDevice; }
        }

        public DNSInitParams GetDeviceParams()
        {
            DNSInitParams initParams = new DNSInitParams();
            initParams.uNumBackgroundThreads = BackgroundThreads;
            initParams.eLoggingLevel = LoggingLevel;
            initParams.bOpenConfigWindow = OpenConfigWindow;
            initParams.eTerrainObjectsQuality = TerrainObjectsQuality;
            initParams.eRenderingSystem = RenderingSystem;
            initParams.bShaderBasedRenderingSystem = ShaderBasedRenderingSystem;
            initParams.strLogFileDirectory = LogFileDirectory;
            initParams.strConfigFilesDirectory = ConfigFilesDirectory;
            initParams.bIgnoreRasterLayerMipmaps = IgnoreRasterLayerMipmaps;
            initParams.fObjectsBatchGrowthRatio = ObjectsBatchGrowthRatio;
            initParams.uObjectsTexturesAtlasSize = ObjectsTexturesAtlasSize;
            initParams.bObjectsTexturesAtlas16bit = ObjectsTexturesAtlas16bit;
            initParams.bDisableDepthBuffer = DisableDepthBuffer;
            initParams.bEnableObjectsBatchEnlarging = EnableObjectsBatchEnlarging;
            initParams.uObjectsBatchInitialNumIndices = ObjectsBatchInitialNumIndices;
            initParams.uObjectsBatchInitialNumVertices = ObjectsBatchInitialNumVertices;
            initParams.bEnableGLQuadBufferStereo = EnableGLQuadBufferStereo;
            initParams.bMultiScreenDevice = MultiScreenDevice;
            initParams.bAlignScreenSizeObjects = AlignScreenSizeObjects;
            initParams.bMultiThreadedDevice = MultiThreadedDevice;
            initParams.bPreferUseTerrainTileRenderTargets = PreferUseTerrainTileRenderTargets;
            initParams.uNumTerrainTileRenderTargets = NumTerrainTileRenderTargets;
            initParams.eViewportAntiAliasingLevel = ViewportAntiAliasingLevel;
            initParams.eTerrainObjectsAntiAliasingLevel = TerrainObjectsAntiAliasingLevel;
            initParams.strMapLayersLocalCacheFolder = MapLayersLocalCacheFolder;
            initParams.uMapLayersLocalCacheSizeInMB = MapLayersLocalCacheSizeInMB;
            initParams.uDtmVisualizationPrecision = DtmVisualizationPrecision;
            initParams.uMainMonitorIndex = MainMonitorIndex;
            initParams.uWebRequestRetryCount = WebRequestRetryCount;
            initParams.uAsyncQueryTilesMaxActiveWebRequests = AsyncQueryTilesMaxActiveWebRequests;

            return initParams;
        }

        public void SetDeviceParams(DNSInitParams initParams)
        {
            m_BackgroundThreads = initParams.uNumBackgroundThreads;
            LoggingLevel = initParams.eLoggingLevel;
            OpenConfigWindow = initParams.bOpenConfigWindow;
            TerrainObjectsQuality = initParams.eTerrainObjectsQuality;
            RenderingSystem = initParams.eRenderingSystem;
            ShaderBasedRenderingSystem = initParams.bShaderBasedRenderingSystem;
            LogFileDirectory = initParams.strLogFileDirectory;
            ConfigFilesDirectory = initParams.strConfigFilesDirectory;
            IgnoreRasterLayerMipmaps = initParams.bIgnoreRasterLayerMipmaps;
            ObjectsBatchGrowthRatio = initParams.fObjectsBatchGrowthRatio;
            ObjectsTexturesAtlasSize = initParams.uObjectsTexturesAtlasSize;
            ObjectsTexturesAtlas16bit = initParams.bObjectsTexturesAtlas16bit;
            DisableDepthBuffer = initParams.bDisableDepthBuffer;
            EnableObjectsBatchEnlarging = initParams.bEnableObjectsBatchEnlarging;
            ObjectsBatchInitialNumIndices = initParams.uObjectsBatchInitialNumIndices;
            ObjectsBatchInitialNumVertices = initParams.uObjectsBatchInitialNumVertices;
            EnableGLQuadBufferStereo = initParams.bEnableGLQuadBufferStereo;
            MultiScreenDevice = initParams.bMultiScreenDevice;
            AlignScreenSizeObjects = initParams.bAlignScreenSizeObjects;
            MultiThreadedDevice = initParams.bMultiThreadedDevice;
            PreferUseTerrainTileRenderTargets = initParams.bPreferUseTerrainTileRenderTargets;
            NumTerrainTileRenderTargets = initParams.uNumTerrainTileRenderTargets;
            ViewportAntiAliasingLevel = initParams.eViewportAntiAliasingLevel;
            TerrainObjectsAntiAliasingLevel = initParams.eTerrainObjectsAntiAliasingLevel;
            MapLayersLocalCacheFolder = initParams.strMapLayersLocalCacheFolder;
            MapLayersLocalCacheSizeInMB = initParams.uMapLayersLocalCacheSizeInMB;
            DtmVisualizationPrecision = initParams.uDtmVisualizationPrecision;
            MainMonitorIndex = initParams.uMainMonitorIndex;
            WebRequestRetryCount = initParams.uWebRequestRetryCount;
            AsyncQueryTilesMaxActiveWebRequests = initParams.uAsyncQueryTilesMaxActiveWebRequests;

        }
    }
}
