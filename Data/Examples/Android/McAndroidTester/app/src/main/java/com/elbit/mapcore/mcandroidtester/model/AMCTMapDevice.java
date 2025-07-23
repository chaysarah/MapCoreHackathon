package com.elbit.mapcore.mcandroidtester.model;

import com.elbit.mapcore.mcandroidtester.managers.Manager_MCGeneralDefinitions;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;

import com.elbit.mapcore.Classes.Map.McMapDevice;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapDevice;

/**
 * Created by tc99382 on 06/07/2016.
 */

public class AMCTMapDevice {

    private IMcMapDevice mDevice = null;
    public static boolean IsExistLocalCache;
    private static AMCTMapDevice mAmctMapDevice;
    private boolean mIsUseDefualtValues = false;  // not need in android

    private int BackgroundThreads;// = 1;
    private IMcMapDevice.ELoggingLevel mLoggingLevel;// = IMcMapDevice.ELoggingLevel.ELL_LOW;
    private boolean OpenConfigWindow;// = false;
    private String ConfigFilesDirectory;// = "/sdcard/MapCore";
    private String LogFileDirectory;// = "/sdcard/MapCore";
    private IMcMapDevice.EAntiAliasingLevel mViewportAntiAliasingLevel;// = IMcMapDevice.EAntiAliasingLevel.EAAL_NONE;
    private IMcMapDevice.EAntiAliasingLevel mTerrainObjectsAntiAliasingLevel;// = IMcMapDevice.EAntiAliasingLevel.EAAL_NONE;
    private IMcMapDevice.ETerrainObjectsQuality mTerrainObjectsQuality;// = IMcMapDevice.ETerrainObjectsQuality.ETOQ_MEDIUM;
    private int DtmVisualizationPrecision;// = 1;
    private float ObjectsBatchGrowthRatio;// = 4;
    private int ObjectsTexturesAtlasSize;// = 1024;
    private boolean ObjectsTexturesAtlas16bit;// = false;
    private boolean DisableDepthBuffer;// = false;
    private IMcMapDevice.ERenderingSystem mRenderingSystem;// = IMcMapDevice.ERenderingSystem.ERS_AUTO_SELECT;
    private boolean ShaderBasedRenderingSystem;// = false;
    private boolean IgnoreRasterLayerMipmaps;// = false;
    private boolean EnableGLQuadBufferStereo;// = false;
    private int NumTerrainTileRenderTargets;// = 5;
    private boolean PreferUseTerrainTileRenderTargets;// = false;
    private boolean MultiThreadedDevice;// = false;
    private boolean MultiScreenDevice;// = false;
    private long MainMonitorIndex;// = -1;
    private int ObjectsBatchInitialNumVertices;// = 32;
    private int ObjectsBatchInitialNumIndices;// = 64;
    private boolean EnableObjectsBatchEnlarging;// = true;
    private boolean AlignScreenSizeObjects;// = true;
    private String MapLayersLocalCacheFolder;// = "/sdcard/MapCore";
    private int MapLayersLocalCacheSizeInMB;// = 0;
    private IMcMapDevice.EStaticObjectsVisibilityQueryPrecision StaticObjectsVisibilityQueryPrecision;
    private boolean mFullScreen;
    private String PrefixForPathsInResourceFile;

    private String LoggingLevelText;
    private String RenderingSystemText;
    private String TerrainObjectsAntiAliasingLevelText;
    private String TerrainObjectsQualityText;
    private String ViewportAntiAliasingLevelText;

    public IMcMapDevice getDevice() {
        return mDevice;
    }

    public AMCTMapDevice() {
        IMcMapDevice.SInitParams initParams = new IMcMapDevice.SInitParams();
        LogFileDirectory = initParams.strLogFileDirectory;
        ConfigFilesDirectory = initParams.strConfigFilesDirectory;
        MapLayersLocalCacheFolder = initParams.strMapLayersLocalCacheFolder;
        BackgroundThreads = initParams.uNumBackgroundThreads;
        mLoggingLevel = initParams.eLoggingLevel;
        OpenConfigWindow = initParams.bOpenConfigWindow;
        mTerrainObjectsQuality = initParams.eTerrainObjectsQuality;
        mRenderingSystem = initParams.eRenderingSystem;
        ShaderBasedRenderingSystem = initParams.bShaderBasedRenderingSystem;
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
        mViewportAntiAliasingLevel = initParams.eViewportAntiAliasingLevel;
        mTerrainObjectsAntiAliasingLevel = initParams.eTerrainObjectsAntiAliasingLevel;
        MapLayersLocalCacheSizeInMB = initParams.uMapLayersLocalCacheSizeInMB;
        DtmVisualizationPrecision = initParams.uDtmVisualizationPrecision;
        MainMonitorIndex = initParams.uMainMonitorIndex;
        StaticObjectsVisibilityQueryPrecision = initParams.eStaticObjectsVisibilityQueryPrecision;
        mFullScreen = initParams.bFullScreen;
        PrefixForPathsInResourceFile = initParams.strPrefixForPathsInResourceFile;

        MapLayersLocalCacheFolder = "/sdcard/MapCore";
        ConfigFilesDirectory = "/sdcard/MapCore";
        LogFileDirectory = "/sdcard/MapCore";
    }

    public static AMCTMapDevice getInstance() {
        if (mAmctMapDevice == null) {
            mAmctMapDevice = new AMCTMapDevice();

        }
        return mAmctMapDevice;
    }

    public int getNumBackgroundThreads() {
        return BackgroundThreads;
    }

    public void setNumBackgroundThreads(int backgroundThreads) {
        this.BackgroundThreads = backgroundThreads;
    }

    public long getMainMonitorIndex() {
        return MainMonitorIndex;
    }

    public void setMainMonitorIndex(int mainMonitorIndex) {
        this.MainMonitorIndex = mainMonitorIndex;
    }

    public int getDtmVisualizationPrecision() {
        return DtmVisualizationPrecision;
    }

    public void setDtmVisualizationPrecision(int dtmVisualizationPrecision) {
        this.DtmVisualizationPrecision = dtmVisualizationPrecision;
    }

    public int getMapLayersLocalCacheSizeInMB() {
        return MapLayersLocalCacheSizeInMB;
    }

    public void setMapLayersLocalCacheSizeInMB(int mapLayersLocalCacheSizeInMB) {
        this.MapLayersLocalCacheSizeInMB = mapLayersLocalCacheSizeInMB;
    }

    public String getMapLayersLocalCacheFolder() {
        return MapLayersLocalCacheFolder;
    }

    public void setMapLayersLocalCacheFolder(String mapLayersLocalCacheFolder) {
        this.MapLayersLocalCacheFolder = mapLayersLocalCacheFolder;
    }

    public IMcMapDevice.EAntiAliasingLevel getTerrainObjectsAntiAliasingLevel() {
        return mTerrainObjectsAntiAliasingLevel;
    }

    public void setTerrainObjectsAntiAliasingLevel(IMcMapDevice.EAntiAliasingLevel terrainObjectsAntiAliasingLevel) {
        this.mTerrainObjectsAntiAliasingLevel = terrainObjectsAntiAliasingLevel;
    }

    public IMcMapDevice.EAntiAliasingLevel getViewportAntiAliasingLevel() {
        return mViewportAntiAliasingLevel;
    }

    public void setViewportAntiAliasingLevel(IMcMapDevice.EAntiAliasingLevel viewportAntiAliasingLevel) {
        this.mViewportAntiAliasingLevel = viewportAntiAliasingLevel;
    }

    public int getNumTerrainTileRenderTargets() {
        return NumTerrainTileRenderTargets;
    }

    public void setNumTerrainTileRenderTargets(int numTerrainTileRenderTargets) {
        this.NumTerrainTileRenderTargets = numTerrainTileRenderTargets;
    }

    public boolean getIsPreferUseTerrainTileRenderTargets() {
        return PreferUseTerrainTileRenderTargets;
    }

    public void setPreferUseTerrainTileRenderTargets(boolean preferUseTerrainTileRenderTargets) {
        this.PreferUseTerrainTileRenderTargets = preferUseTerrainTileRenderTargets;
    }

    public boolean getIsMultiThreadedDevice() {
        return MultiThreadedDevice;
    }

    public void setMultiThreadedDevice(boolean multiThreadedDevice) {
        this.MultiThreadedDevice = multiThreadedDevice;
    }

    public boolean getIsAlignScreenSizeObjects() {
        return AlignScreenSizeObjects;
    }

    public void setAlignScreenSizeObjects(boolean alignScreenSizeObjects) {
        this.AlignScreenSizeObjects = alignScreenSizeObjects;
    }

    public boolean getIsMultiScreenDevice() {
        return MultiScreenDevice;
    }

    public void setMultiScreenDevice(boolean multiScreenDevice) {
        this.MultiScreenDevice = multiScreenDevice;
    }

    public boolean getIsEnableGLQuadBufferStereo() {
        return EnableGLQuadBufferStereo;
    }

    public void setEnableGLQuadBufferStereo(boolean enableGLQuadBufferStereo) {
        this.EnableGLQuadBufferStereo = enableGLQuadBufferStereo;
    }

    public int getObjectsBatchInitialNumVertices() {
        return ObjectsBatchInitialNumVertices;
    }

    public void setObjectsBatchInitialNumVertices(int objectsBatchInitialNumVertices) {
        this.ObjectsBatchInitialNumVertices = objectsBatchInitialNumVertices;
    }

    public int getObjectsBatchInitialNumIndices() {
        return ObjectsBatchInitialNumIndices;
    }

    public void setObjectsBatchInitialNumIndices(int objectsBatchInitialNumIndices) {
        this.ObjectsBatchInitialNumIndices = objectsBatchInitialNumIndices;
    }

    public boolean getIsEnableObjectsBatchEnlarging() {
        return EnableObjectsBatchEnlarging;
    }

    public void setEnableObjectsBatchEnlarging(boolean enableObjectsBatchEnlarging) {
        this.EnableObjectsBatchEnlarging = enableObjectsBatchEnlarging;
    }

    public boolean getIsDisableDepthBuffer() {
        return DisableDepthBuffer;
    }

    public void setDisableDepthBuffer(boolean disableDepthBuffer) {
        this.DisableDepthBuffer = disableDepthBuffer;
    }

    public boolean getIsObjectsTexturesAtlas16bit() {
        return ObjectsTexturesAtlas16bit;
    }

    public void setObjectsTexturesAtlas16bit(boolean objectsTexturesAtlas16bit) {
        this.ObjectsTexturesAtlas16bit = objectsTexturesAtlas16bit;
    }

    public int getObjectsTexturesAtlasSize() {
        return ObjectsTexturesAtlasSize;
    }

    public void setObjectsTexturesAtlasSize(int objectsTexturesAtlasSize) {
        this.ObjectsTexturesAtlasSize = objectsTexturesAtlasSize;
    }

    public float getObjectsBatchGrowthRatio() {
        return ObjectsBatchGrowthRatio;
    }

    public void setObjectsBatchGrowthRatio(float objectsBatchGrowthRatio) {
        this.ObjectsBatchGrowthRatio = objectsBatchGrowthRatio;
    }

    public boolean getIsIgnoreRasterLayerMipmaps() {
        return IgnoreRasterLayerMipmaps;
    }

    public void setIgnoreRasterLayerMipmaps(boolean ignoreRasterLayerMipmaps) {
        this.IgnoreRasterLayerMipmaps = ignoreRasterLayerMipmaps;
    }

    public String getConfigFilesDirectory() {
        return ConfigFilesDirectory;
    }

    public void setConfigFilesDirectory(String configFilesDirectory) {
        this.ConfigFilesDirectory = configFilesDirectory;
    }

    public String getLogFileDirectory() {
        return LogFileDirectory;
    }

    public void setLogFileDirectory(String logFileDirectory) {
        this.LogFileDirectory = logFileDirectory;
    }

    public boolean getIsShaderBasedRenderingSystem() {
        return ShaderBasedRenderingSystem;
    }

    public void setShaderBasedRenderingSystem(boolean shaderBasedRenderingSystem) {
        this.ShaderBasedRenderingSystem = shaderBasedRenderingSystem;
    }

    public IMcMapDevice.ERenderingSystem getRenderingSystem() {
        return mRenderingSystem;
    }

    public void setRenderingSystem(IMcMapDevice.ERenderingSystem renderingSystem) {
        this.mRenderingSystem = renderingSystem;
    }

    public IMcMapDevice.ETerrainObjectsQuality getTerrainObjectsQuality() {
        return mTerrainObjectsQuality;
    }

    public void setTerrainObjectsQuality(IMcMapDevice.ETerrainObjectsQuality terrainObjectsQuality) {
        this.mTerrainObjectsQuality = terrainObjectsQuality;
    }

    public boolean getIsOpenConfigWindow() {
        return OpenConfigWindow;
    }

    public void setOpenConfigWindow(boolean openConfigWindow) {
        this.OpenConfigWindow = openConfigWindow;
    }

    public IMcMapDevice.ELoggingLevel getLoggingLevel() {
        return mLoggingLevel;
    }

    public void setLoggingLevel(IMcMapDevice.ELoggingLevel loggingLevel) {
        this.mLoggingLevel = loggingLevel;
    }

    public IMcMapDevice.EStaticObjectsVisibilityQueryPrecision getStaticObjectsVisibilityQueryPrecision() {
        return StaticObjectsVisibilityQueryPrecision;
    }

    public void setStaticObjectsVisibilityQueryPrecision(IMcMapDevice.EStaticObjectsVisibilityQueryPrecision staticObjectsVisibilityQueryPrecision) {
        this.StaticObjectsVisibilityQueryPrecision = staticObjectsVisibilityQueryPrecision;
    }

    public boolean getIsFullScreen() {
        return mFullScreen;
    }

    public void setFullScreen(boolean fullScreen) {
        this.mFullScreen = fullScreen;
    }
    
    public String getPrefixForPathsInResourceFile() {
        return PrefixForPathsInResourceFile;
    }

    public void setPrefixForPathsInResourceFile(String prefixForPathsInResourceFile) {
        this.PrefixForPathsInResourceFile = prefixForPathsInResourceFile;
    }
    
    public IMcMapDevice CreateDevice() {
        try {
            if (mDevice == null) {
                if (mIsUseDefualtValues) {
                    BackgroundThreads = Manager_MCGeneralDefinitions.m_NumBackgroundThreads;
                    MultiScreenDevice = Manager_MCGeneralDefinitions.m_MultiScreenDevice;
                    MultiThreadedDevice = Manager_MCGeneralDefinitions.m_MultiThreadedDevice;
                }
                //MapLayersLocalCacheFolder = "/sdcard/MapCore";
                //ConfigFilesDirectory = "/sdcard/MapCore";
                //LogFileDirectory = "/sdcard/MapCore";

                IMcMapDevice.SInitParams initParams = new IMcMapDevice.SInitParams();
                initParams.strLogFileDirectory = LogFileDirectory;
                initParams.strConfigFilesDirectory = ConfigFilesDirectory;
                initParams.strMapLayersLocalCacheFolder = MapLayersLocalCacheFolder;
                initParams.uNumBackgroundThreads = BackgroundThreads;
                if(LoggingLevelText != null && !LoggingLevelText.isEmpty())
                    mLoggingLevel = IMcMapDevice.ELoggingLevel.valueOf(LoggingLevelText.substring(1));
                initParams.eLoggingLevel = mLoggingLevel;
                initParams.bOpenConfigWindow = OpenConfigWindow;
                if(TerrainObjectsQualityText != null && !TerrainObjectsQualityText.isEmpty())
                    mTerrainObjectsQuality = IMcMapDevice.ETerrainObjectsQuality.valueOf(TerrainObjectsQualityText.substring(1));
                initParams.eTerrainObjectsQuality = mTerrainObjectsQuality;
                if(RenderingSystemText != null && !RenderingSystemText.isEmpty())
                    mRenderingSystem = IMcMapDevice.ERenderingSystem.valueOf(RenderingSystemText.substring(1));
                initParams.eRenderingSystem = mRenderingSystem;
                initParams.bShaderBasedRenderingSystem = ShaderBasedRenderingSystem;
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
                if(ViewportAntiAliasingLevelText != null && !ViewportAntiAliasingLevelText.isEmpty())
                    mViewportAntiAliasingLevel = IMcMapDevice.EAntiAliasingLevel.valueOf(ViewportAntiAliasingLevelText.substring(1));
                initParams.eViewportAntiAliasingLevel = mViewportAntiAliasingLevel;
                if(TerrainObjectsAntiAliasingLevelText != null && !TerrainObjectsAntiAliasingLevelText.isEmpty())
                    mTerrainObjectsAntiAliasingLevel = IMcMapDevice.EAntiAliasingLevel.valueOf(TerrainObjectsAntiAliasingLevelText.substring(1));
                initParams.eTerrainObjectsAntiAliasingLevel = mTerrainObjectsAntiAliasingLevel;
                initParams.uMapLayersLocalCacheSizeInMB = MapLayersLocalCacheSizeInMB;
                initParams.uDtmVisualizationPrecision = DtmVisualizationPrecision;
                initParams.uMainMonitorIndex = (int) MainMonitorIndex;
                mDevice = IMcMapDevice.Static.Create(initParams);
                mAmctMapDevice = this;
            }
        } catch (MapCoreException McEx) {
            McEx.printStackTrace();
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), McEx, "CreateDevice");
        } catch (Exception e) {
            e.printStackTrace();
        }
        return mDevice;
    }

    public void LoadResourceGroup(String groupName, String[] pathArray, IMcMapDevice.EResourceLocationType resourceType) {
        try {
            IMcMapDevice.Static.LoadResourceGroup(groupName, pathArray, resourceType);
        } catch (MapCoreException McEx) {
            McEx.printStackTrace();
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), McEx, "McMapDevice.LoadResourceGroup");

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void UnloadResourceGroup(String groupName) {
        try {
            McMapDevice.Static.UnloadResourceGroup(groupName);
        } catch (MapCoreException McEx) {
            McEx.printStackTrace();
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), McEx, "McMapDevice.UnloadResourceGroup");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
