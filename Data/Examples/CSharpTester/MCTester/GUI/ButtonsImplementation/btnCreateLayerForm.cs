using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;
using MCTester.Managers.MapWorld;
using MCTester.GUI;
using MCTester.GUI.Map;
using MCTester.MapWorld.LoadingMapScheme;
using MCTester.MapWorld;
using MCTester.MapWorld.Assist_Forms;
using MapCore.Common;
using MCTester.Controls;
using MCTester.General_Forms;
using System.IO;
using MCTester.MapWorld.WebMapLayers;
using static MCTester.Managers.MapWorld.Manager_MCLayers;
using MCTester.MapWorld.WizardForms;


namespace MCTester.ButtonsImplementation
{
    public partial class btnCreateLayerForm : Form, IWebLayerRequest, IUserSelectServerLayers
    {
        private List<IDNMcMapLayer> mLayers;
       // private List<IDNMcMapLayer> lstLayersAfterInit;
        private IDNMcMapLayer[] arrLayers;
        private IDNMcGridCoordinateSystem mGridCoordSys;
        private IDNMcMapDevice currSchemaDevice = null;
        private DNSLocalCacheLayerParams m_localCacheSubFolderNativeRaster1;
        private DNSLocalCacheLayerParams m_localCacheSubFolderNativeRaster2;
        private DNSLocalCacheLayerParams m_localCacheSubFolderNativeRaster3;
        private DNSLocalCacheLayerParams m_localCacheSubFolderNativeVector1;
        private DNSLocalCacheLayerParams m_localCacheSubFolderNativeVector2;
        private DNSLocalCacheLayerParams m_localCacheSubFolderNativeVector3;
        private DNSLocalCacheLayerParams m_localCacheSubFolderNativeVector4;
        private DNSLocalCacheLayerParams m_localCacheSubFolderNativeDTM;
        private DNSLocalCacheLayerParams m_localCacheSubFolderRawRaster;
        private DNSLocalCacheLayerParams m_localCacheSubFolderRawDTM;
        private DNSLocalCacheLayerParams m_localCacheSubFolderRawVector;
        private DNSLocalCacheLayerParams m_localCacheSubFolderNativeMaterial;
        private DNSLocalCacheLayerParams m_localCacheSubFolderNativeTraversability;
        private DNSLocalCacheLayerParams m_localCacheSubFolderRawMaterial;
        private DNSLocalCacheLayerParams m_localCacheSubFolderRawTraversability;
        private DNSLocalCacheLayerParams m_localCacheSubFolderNativeHeat;
        private DNSTilingScheme mcTilingScheme = null;
        private DNSRawVectorParams mcRawVectorParams;
        private IDNMcGridCoordinateSystem mRawVectorTargetGridCoordinateSystem;
        private IDNMcMapViewport mNewViewport;
        private IDNMcMapViewport mNewViewportSecond;
        private IDNMcMapCamera mNewCamera;
        private IDNMcMapCamera mNewCameraSecond;
        private IDNMcMapTerrain terrain = null;
        private MCTOverlayManager newMCTOverlayManager;
        private IDNMcOverlayManager newDNMcOverlayManager = null;
        private MCTMapViewport mCurrViewport;
        private MCTMapViewport mCurrViewportSecond;
        private List<IDNMcMapTerrain> lViewportTerrains;
        private DNEMapType mMapType;
        private Timer mLoadDelayLayersTimer = new Timer();
        private Timer mFinishLoadAllDelayLayersTimer = new Timer();
        private DNSRawVector3DExtrusionParams mExtrusionParams = new DNSRawVector3DExtrusionParams();
        private DNSRawVector3DExtrusionGraphicalParams mExtrusionGraphicalParams = new DNSRawVector3DExtrusionGraphicalParams();
        private string mExtrusionIndexingData = "";
        private bool mRawVector3DExtrusionIsUseIndexingData = false;
        private FrmWebMapLayers mFrmWebMapLayers;
        private Dictionary<string, List<Manager_MCLayers.MCTWebServerUserSelection>> mDicServerSelectLayers = new Dictionary<string, List<Manager_MCLayers.MCTWebServerUserSelection>>();
        private Dictionary<string, DNEWebMapServiceType> mDicServerSelectLayersType = new Dictionary<string, DNEWebMapServiceType>();
        private bool mWMTSOpenAllLayersAsOne;
        private bool mWMSOpenAllLayersAsOne;
        private DNSWMTSParams m_SWMTSParams = new DNSWMTSParams();
        private DNSWMSParams m_SWMSParams = new DNSWMSParams();
        private DNSWCSParams m_SWCSParams = new DNSWCSParams();
        private MCTRaw3DModelParams m_Raw3DModelParams = new MCTRaw3DModelParams();
        private bool mIsWCSRaster = true;
        private Dictionary<IDNMcMapLayer, KeyValuePair<float, float>> dicVectorLayersScale = new Dictionary<IDNMcMapLayer, KeyValuePair<float, float>>();
        private static List<DNSMcKeyStringValue> m_RequestParams = null;
        private bool m_isTextChanged = false;

        private Dictionary<string, MCTServerSelectLayersData> mDicWMTSFromCSWLayersData = new Dictionary<string, MCTServerSelectLayersData>();
        private List<IDNMcMapLayer> m_QuerySecondaryDtmLayers = null;

        private bool mIsOpenMapWithoutWaitAllLayersInitChecked = false;

        public btnCreateLayerForm(IDNMcMapDevice myDevice)
        {
            InitializeComponent();
            m_SWMTSParams.bUseServerTilingScheme = true;

            ctrlBrowseLayerRawVector.FileNameChanged += CtrlBrowseLayerRawVector_FileNameSelectedOrChanged;
            ctrlBrowserRawVector3DExtrusion.FileNameChanged += CtrlBrowseLayerRawVector_FileNameSelectedOrChanged;

            new ToolTip().SetToolTip(btnLocalCacheNativeRaster1, "Local Cache");
            new ToolTip().SetToolTip(btnLocalCacheNativeRaster2, "Local Cache");
            new ToolTip().SetToolTip(btnLocalCacheNativeRaster3, "Local Cache");
            new ToolTip().SetToolTip(btnLocalCacheNativeVector1, "Local Cache");
            new ToolTip().SetToolTip(btnLocalCacheNativeVector2, "Local Cache");
            new ToolTip().SetToolTip(btnLocalCacheNativeVector3, "Local Cache");
            new ToolTip().SetToolTip(btnLocalCacheNativeDTM, "Local Cache");
            new ToolTip().SetToolTip(btnLocalCacheRawRaster, "Local Cache");
            new ToolTip().SetToolTip(btnLocalCacheRawDTM, "Local Cache");

            ctrlSelectTransparentColor.EnabledButtons(true);
            ctrlSelectTransparentColor.AlphaColor = Color.FromArgb(0, 0, 0, 0);

            ctrlNativeVectorLayers.Filter = "TEXT File|*.txt";

            mLoadDelayLayersTimer.Interval = 1000;
            mLoadDelayLayersTimer.Tick += new EventHandler(LoadDelayLayersTimer_Tick);

            mFinishLoadAllDelayLayersTimer.Interval = 1000;
            mFinishLoadAllDelayLayersTimer.Tick += new EventHandler(FinishLoadAllDelayLayersTimer_Tick);

            mcRawVectorParams = CtrlRawVectorParams.GetSRawVectorParamsWithDefaultValue();

            mExtrusionParams = new DNSRawVector3DExtrusionParams();
            mExtrusionParams.strHeightColumn = Manager_MCLayers.RawVector3DExtrusion_HeightColumnText;

            txtMapCoreServerPath.Text = Manager_MCLayers.UrlMapCoreServer;
            txtWMTSServerPath.Text = Manager_MCLayers.UrlWMTSServer;
            txtWCSServerPath.Text = Manager_MCLayers.UrlWCSServer;
            txtWMSServerPath.Text = Manager_MCLayers.UrlWMSServer;
            txtCSWServerPath.Text = Manager_MCLayers.UrlCSWServer;

            ctrlBrowse3DModelIndexingDataDirectory.Enabled = false;

            chxOrthometricHeights.Checked = new DNS3DModelConvertParams().bOrthometricHeights;
            ntxTargetHighestResolution.SetFloat(new DNS3DModelConvertParams().fTargetHighestResolution);

        }

        private void CtrlBrowseLayerRawVector_FileNameSelectedOrChanged(object sender, EventArgs e)
        {
            if (sender is CtrlBrowseControl)
            {
                CtrlBrowseControl ctrlBrowseControl = sender as CtrlBrowseControl;
                if (!m_isTextChanged && (ctrlBrowseControl.Name == ctrlBrowserRawVector3DExtrusion.Name || ctrlBrowseControl.Name == ctrlBrowseLayerRawVector.Name))
                {
                    m_isTextChanged = true;
                    ctrlBrowseControl.FileName = Manager_MCLayers.CheckRawVector(ctrlBrowseControl.FileName, ctrlBrowseControl.Name == ctrlBrowseLayerRawVector.Name);
                    m_isTextChanged = false;
                }
            }
        }

    
        private void btnCreateMap_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                MCTMapLayerReadCallback.SetIsShowFailedError(false);

                mLayers = new List<IDNMcMapLayer>();
                arrLayers = new IDNMcMapLayer[] { };
                lViewportTerrains = new List<IDNMcMapTerrain>();
                mGridCoordSys = null;
                Manager_MCLayers.ResetServerLayersPriorityDic();

                // get user input    
                if (ctrlSmallViewportParams1.GetIs2DChecked() || ctrlSmallViewportParams1.GetIs2D3DChecked())
                    mMapType = DNEMapType._EMT_2D;
                else
                    mMapType = DNEMapType._EMT_3D;

             //   mIsWpfWindow = chxIsWpfWindow.Checked;
                uint uDefaultFirstLowerQuality = DNMcConstants._MC_EMPTY_ID;
                bool bDefaultThereAreMissingFiles = false;
                uint uNumLevelsToIgnore = ntbResolutionsToIgnore.GetUInt32();

             
                // create new device if not exist

                if (currSchemaDevice == null)
                {
                    CheckDevice();
                }
                MCTMapLayerReadCallback.IsSaveReplacedOrRemovedLayer = true;

                if (ctrlBrowserRaster1.FileName != "")
                {
                    List<string> subdirs = Manager_MCLayers.GetRecursiveFolders(ctrlBrowserRaster1.FileName);
                    foreach (string dir in subdirs)
                    {
                        if (!CreateNativeRasterMapLayer(dir, m_localCacheSubFolderNativeRaster1, uNumLevelsToIgnore))
                        {
                           HandleError(); // this.Enabled = true;
                            return;
                        }
                    }
                }

                if (ctrlBrowserRaster2.FileName != "")
                {
                    List<string> subdirs = Manager_MCLayers.GetRecursiveFolders(ctrlBrowserRaster2.FileName);
                    foreach (string dir in subdirs)
                    {
                        if (!CreateNativeRasterMapLayer(dir, m_localCacheSubFolderNativeRaster2, uNumLevelsToIgnore))
                        {
                           HandleError(); // this.Enabled = true;
                            return;
                        }
                    }
                }
                if (ctrlBrowserRaster3.FileName != "")
                {
                    List<string> subdirs = Manager_MCLayers.GetRecursiveFolders(ctrlBrowserRaster3.FileName);
                    foreach (string dir in subdirs)
                    {
                        if (!CreateNativeRasterMapLayer(dir, m_localCacheSubFolderNativeRaster3, uNumLevelsToIgnore))
                        {
                           HandleError(); // this.Enabled = true;
                            return;
                        }
                    }
                }
                if (ctrlNativeVector1.FileName != "")
                {
                    List<string> subdirs = Manager_MCLayers.GetRecursiveFolders(ctrlNativeVector1.FileName);
                    foreach (string dir in subdirs)
                    {
                        if (!CreateNativeVectorLayer(dir, m_localCacheSubFolderNativeVector1))
                        {
                           HandleError(); // this.Enabled = true;
                            return;
                        }
                    }
                }
                if (ctrlNativeVector2.FileName != "")
                {
                    List<string> subdirs = Manager_MCLayers.GetRecursiveFolders(ctrlNativeVector2.FileName);
                    foreach (string dir in subdirs)
                    {
                        if (!CreateNativeVectorLayer(dir, m_localCacheSubFolderNativeVector2))
                        {
                           HandleError(); // this.Enabled = true;
                            return;
                        }
                    }
                }
                if (ctrlNativeVector3.FileName != "")
                {
                    List<string> subdirs = Manager_MCLayers.GetRecursiveFolders(ctrlNativeVector3.FileName);
                    foreach (string dir in subdirs)
                    {
                        if (!CreateNativeVectorLayer(dir, m_localCacheSubFolderNativeVector3))
                        {
                           HandleError(); // this.Enabled = true;
                            return;
                        }
                    }
                }
                if (ctrlNativeVectorLayers.FileName != "")
                {
                    if (!CreateNativeVectorLayers(ctrlNativeVectorLayers.FileName, m_localCacheSubFolderNativeVector4))
                    {
                       HandleError(); // this.Enabled = true;
                        return;
                    }
                }
                if (ctrlBrowserDTM.FileName != "")
                {
                    IDNMcMapLayer layer = Manager_MCLayers.CreateNativeDTMLayer(ctrlBrowserDTM.FileName, uNumLevelsToIgnore, m_localCacheSubFolderNativeDTM);
                    if (layer == null)
                    {
                       HandleError(); // this.Enabled = true;
                        return;
                    }
                    else
                        mLayers.Add(layer);
                }
                if (ctrlBrowserNativeVector3DExtrusion.FileName != "")
                {
                    List<string> subdirs = Manager_MCLayers.GetRecursiveFolders(ctrlBrowserNativeVector3DExtrusion.FileName);
                    foreach (string dir in subdirs)
                    {
                        IDNMcMapLayer layer = Manager_MCLayers.CreateNativeVector3DExtrusionMapLayer(dir, uNumLevelsToIgnore, ntbExtrusionHeightMaxAddition.GetFloat());
                        if (layer == null)
                        {
                           HandleError(); // this.Enabled = true;
                            return;
                        }
                        else
                            mLayers.Add(layer);
                    }
                }
                if (ctrlBrowserNative3DModel.FileName != "")
                {
                    List<string> subdirs = Manager_MCLayers.GetRecursiveFolders(ctrlBrowserNative3DModel.FileName);
                    foreach (string dir in subdirs)
                    {
                        IDNMcMapLayer layer = Manager_MCLayers.CreateNative3DModelMapLayer(dir, uNumLevelsToIgnore);
                        if (layer == null)
                        {
                           HandleError(); // this.Enabled = true;
                            return;
                        }
                        else
                            mLayers.Add(layer);
                    }
                }
                if (ctrlBrowserHeatMap.FileName != "")
                {
                    List<string> subdirs = Manager_MCLayers.GetRecursiveFolders(ctrlBrowserHeatMap.FileName);
                    foreach (string dir in subdirs)
                    {
                        IDNMcMapLayer layer = Manager_MCLayers.CreateNativeHeatMapLayer(dir,
                                                            uDefaultFirstLowerQuality,
                                                            bDefaultThereAreMissingFiles,
                                                            uNumLevelsToIgnore,
                                                            chxEnhanceBorderOverlap.Checked,
                                                            m_localCacheSubFolderNativeHeat);
                        if (layer == null)
                        {
                           HandleError(); // this.Enabled = true;
                            return;
                        }
                        else
                            mLayers.Add(layer);
                    }
                }
                if (ctrlBrowseNativeMaterialMapLayer.FileName != "")
                {
                    List<string> subdirs = Manager_MCLayers.GetRecursiveFolders(ctrlBrowseNativeMaterialMapLayer.FileName);
                    foreach (string dir in subdirs)
                    {
                        IDNMcMapLayer layer = Manager_MCLayers.CreateNativeMaterialLayer(dir,
                                                            bDefaultThereAreMissingFiles,
                                                            m_localCacheSubFolderNativeMaterial);
                        if (layer == null)
                        {
                           HandleError(); // this.Enabled = true;
                            return;
                        }
                        else
                            mLayers.Add(layer);
                    }
                }
                if (ctrlBrowseNativeTraversabilityMapLayer.FileName != "")
                {
                    List<string> subdirs = Manager_MCLayers.GetRecursiveFolders(ctrlBrowseNativeTraversabilityMapLayer.FileName);
                    foreach (string dir in subdirs)
                    {
                        IDNMcMapLayer layer = Manager_MCLayers.CreateNativeTraversabilityLayer(dir,
                                                            bDefaultThereAreMissingFiles,
                                                            m_localCacheSubFolderNativeTraversability);
                        if (layer == null)
                        {
                           HandleError(); // this.Enabled = true;
                            return;
                        }
                        else
                            mLayers.Add(layer);
                    }
                }
                if (ctrlBrowseLayerRawDTM.FileName != "" || ctrlBrowseRawRaster.FileName != "" || ctrlBrowserRawMaterial.FileName != "" || ctrlBrowserRawTraversability.FileName != "") 
                {
                    List<DNSComponentParams> CompParamsList = new List<DNSComponentParams>();
                    DNSComponentParams m_ComponentParams = new DNSComponentParams();

                    m_ComponentParams.eType = DNEComponentType._ECT_DIRECTORY;
                    m_ComponentParams.strName = "";
                    m_ComponentParams.WorldLimit.MinVertex = DNSMcVector3D.v3Zero;
                    m_ComponentParams.WorldLimit.MaxVertex = DNSMcVector3D.v3Zero;

                    CompParamsList.Add(m_ComponentParams);

                    DNSRawParams dnParams = new DNSRawParams();
                    dnParams.pCoordinateSystem = ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem;
                    dnParams.fMaxScale = ntbMaxScale.GetFloat();
                    dnParams.strDirectory = null;
                    dnParams.aComponents = CompParamsList.ToArray();
                    dnParams.uMaxNumOpenFiles = ntbMaxNumOpenFiles.GetUInt32();
                    dnParams.fFirstPyramidResolution = 0.0F;
                    dnParams.auPyramidResolutions = null;
                    dnParams.bEnhanceBorderOverlap = chxEnhanceBorderOverlap.Checked;
                    dnParams.bResolveOverlapConflicts = chxResolveOverlapConflicts.Checked;
                    dnParams.TransparentColor = ctrlSelectTransparentColor.BColor;
                    dnParams.byTransparentColorPrecision = ntbTransparentColorPrecision.GetByte();
                    dnParams.bFillEmptyTilesByLowerResolutionTiles = chxFillEmptyTilesByLowerResolutionTiles.Checked;
                    dnParams.bIgnoreRasterPalette = cbIgnoreRasterPalette.Checked;
                    dnParams.bPostProcessSourceData = chxPostProcessSourceData.Checked;
                    dnParams.pTilingScheme = mcTilingScheme;

                    if (ctrlBrowseLayerRawDTM.FileName != "")
                    {
                        dnParams.strDirectory = ctrlBrowseLayerRawDTM.FileName;

                        try
                        {
                            IDNMcMapLayer layer = Manager_MCLayers.CreateRawDTMLayer(dnParams, m_localCacheSubFolderRawDTM);
                            if (layer == null)
                            {
                               HandleError(); // this.Enabled = true;
                                return;
                            }
                            else
                                mLayers.Add(layer);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("RawDtm Creation", McEx);
                            return;
                        }
                    }
                    if (ctrlBrowseRawRaster.FileName != "")
                    {
                        dnParams.strDirectory = ctrlBrowseRawRaster.FileName;

                        IDNMcMapLayer layer = Manager_MCLayers.CreateRawRasterLayer(dnParams, chbImageCoordSystem.Checked, m_localCacheSubFolderRawRaster);
                        if (layer == null)
                        {
                           HandleError(); // this.Enabled = true;
                            return;
                        }
                        else
                            mLayers.Add(layer);
                    }
                    if (ctrlBrowserRawMaterial.FileName != "")
                    {
                        dnParams.strDirectory = ctrlBrowserRawMaterial.FileName;
                        IDNMcMapLayer layer = Manager_MCLayers.CreateRawMaterialLayer(dnParams,  m_localCacheSubFolderRawMaterial);
                        if (layer == null)
                        {
                           HandleError(); // this.Enabled = true;
                            return;
                        }
                        else
                            mLayers.Add(layer);
                    }
                    if (ctrlBrowserRawTraversability.FileName != "")
                    {
                        dnParams.strDirectory = ctrlBrowserRawTraversability.FileName;
                        IDNMcMapLayer layer = Manager_MCLayers.CreateRawTraversabilityLayer(dnParams, m_localCacheSubFolderRawTraversability);
                        if (layer == null)
                        {
                           HandleError(); // this.Enabled = true;
                            return;
                        }
                        else
                            mLayers.Add(layer);
                    }
                }

                if (!CreateRaw3DModel(ctrlBrowserRaw3DModel1.FileName, chxRaw3DModelWithIndexing1.Checked))
                    return;
                if (!CreateRaw3DModel(ctrlBrowserRaw3DModel2.FileName, chxRaw3DModelWithIndexing2.Checked))
                    return;

                if (ctrlBrowserRawVector3DExtrusion.FileName != "")
                {
                    List<string> layerNames = Manager_MCLayers.CheckMultiRawVectorLayer(ctrlBrowserRawVector3DExtrusion.FileName);
                    foreach(string dataLayer in layerNames)
                        CreateRawVector3DExtrusion(dataLayer);
                }
                if (ctrlBrowseLayerRawVector.FileName != "")
                {
                    DNSRawVectorParams rawVectorParams = mcRawVectorParams;
                    if (mcRawVectorParams.pSourceCoordinateSystem == null)
                        rawVectorParams.pSourceCoordinateSystem = ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem;

                    if (rawVectorParams.StylingParams == null)
                        rawVectorParams.StylingParams = new DNSInternalStylingParams();
                    
                    string StylingName = rawVectorParams.StylingParams.strStylingFile;

                    List<string> layerNames =  Manager_MCLayers.CheckMultiRawVectorLayer(ctrlBrowseLayerRawVector.FileName);
                    bool existStyling = ctrlBrowseLayerRawVector.FileName.ToLower().Contains(".sld") || ctrlBrowseLayerRawVector.FileName.ToLower().Contains(".lyrx");
                    foreach (string dataLayer in layerNames)
                    {
                        if (dataLayer != "")
                        {

                            string prefix = dataLayer;
                            if (existStyling)
                                rawVectorParams.StylingParams.strStylingFile = "";
                            if (dataLayer.Contains(Manager_MCLayers.RawVectorStylingSplitStr))
                            {
                                string[] nameAndStyling = dataLayer.Split(Manager_MCLayers.RawVectorStylingSplitChar);
                                if (nameAndStyling.Length == 2)
                                {
                                    
                                    prefix = nameAndStyling[0];
                                    rawVectorParams.StylingParams.strStylingFile = nameAndStyling[1];
                                }
                               
                            }

                            rawVectorParams.strDataSource = prefix;
                            

                            IDNMcMapLayer layer = Manager_MCLayers.CreateRawVectorLayer(
                                               rawVectorParams,
                                               mRawVectorTargetGridCoordinateSystem,
                                               mcTilingScheme,
                                               m_localCacheSubFolderRawVector);
                            if (layer == null)
                            {
                               HandleError(); // this.Enabled = true;
                                return;
                            }
                            else
                                mLayers.Add(layer);
                        }
                      
                    }
                }
                if (ctrlBrowseNativeServerRaster.FileName != "")
                {
                    IDNMcMapLayer layer = Manager_MCLayers.CreateNativeServerRasterLayer(ctrlBrowseNativeServerRaster.FileName);
                    if (layer == null)
                    {
                        Enabled = true;
                        return;
                    }
                    else
                        mLayers.Add(layer);
                }
                if (ctrlBrowseNativeServerDTM.FileName != "")
                {
                    IDNMcMapLayer layer = Manager_MCLayers.CreateNativeServerDTMLayer(ctrlBrowseNativeServerDTM.FileName);
                    if (layer == null)
                    {
                        Enabled = true;
                        return;
                    }
                    else
                        mLayers.Add(layer);
                }
                if (ctrlBrowseNativeServerVector.FileName != "")
                {
                    IDNMcMapLayer layer = Manager_MCLayers.CreateNativeServerVectorLayer(ctrlBrowseNativeServerVector.FileName);
                    if (layer == null)
                    {
                        Enabled = true;
                        return;
                    }
                    else
                        mLayers.Add(layer);
                }
                if (ctrlBrowseNativeServerVector3DExtrusion.FileName != "")
                {
                    IDNMcMapLayer layer = Manager_MCLayers.CreateNativeServerVector3DExtrusionLayer(ctrlBrowseNativeServerVector3DExtrusion.FileName);
                    if (layer == null)
                    {
                       HandleError(); // this.Enabled = true;
                        return;
                    }
                    else
                        mLayers.Add(layer);
                }
                if (ctrlBrowseNativeServer3DModel.FileName != "")
                {
                    IDNMcMapLayer layer = Manager_MCLayers.CreateNativeServer3DModelLayer(ctrlBrowseNativeServer3DModel.FileName);
                    if (layer == null)
                    {
                       HandleError(); // this.Enabled = true;
                        return;
                    }
                    else
                        mLayers.Add(layer);
                }
                if (ctrlBrowseNativeServerTraversabilityMapLayer.FileName != "")
                {
                    IDNMcMapLayer layer = Manager_MCLayers.CreateNativeServerTraversabilityLayer(ctrlBrowseNativeServerTraversabilityMapLayer.FileName);
                    if (layer == null)
                    {
                       HandleError(); // this.Enabled = true;
                        return;
                    }
                    else
                        mLayers.Add(layer);
                }
                if (ctrlBrowseNativeServerMaterialMapLayer.FileName != "")
                {
                    IDNMcMapLayer layer = Manager_MCLayers.CreateNativeServerMaterialLayer(ctrlBrowseNativeServerMaterialMapLayer.FileName);
                    if (layer == null)
                    {
                       HandleError(); // this.Enabled = true;
                        return;
                    }
                    else
                        mLayers.Add(layer);
                }
                bool isEnabled = false;
                
                    
                mLayers.AddRange(Manager_MCLayers.CheckAndCreateWebLayersData(mDicServerSelectLayers, mDicServerSelectLayersType, mWMTSOpenAllLayersAsOne, mWMSOpenAllLayersAsOne, m_SWMTSParams, m_SWMSParams, m_SWCSParams, m_Raw3DModelParams, mIsWCSRaster,/*isFilterDTMLayer*/ false, true , out isEnabled,  mDicWMTSFromCSWLayersData));
                
                this.Enabled = isEnabled;

                if (!MCTMapLayerReadCallback.IsCheckNativeServerLayer)
                {
                    // if at least on layer failed init
                    if (MCTMapLayerReadCallback.GetIsShowFailedError())
                    {
                       HandleError(); // this.Enabled = true;
                        return;
                    }
                    if (ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem != null)
                        mGridCoordSys = ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem;
                    else if (ctrlSmallViewportParams1.GetImageCalc() != null)
                        mGridCoordSys = ctrlSmallViewportParams1.GetImageCalc().GetGridCoordSys();
                    
                    if (mLayers.Count > 0)
                    {
                        // check if need to replace layers or remove layers.
                        mLayers = MCTMapLayerReadCallback.CheckLayersIsRemovedOrReplaced(mLayers);

                        if (mGridCoordSys == null && Manager_MCLayers.IsAllLayersInitialized(mLayers))
                            mGridCoordSys = mLayers[0].CoordinateSystem;

                        // create new instance of IDNMcMapTerrain 
                        try
                        {
                            terrain = DNMcMapTerrain.Create(mGridCoordSys, mLayers.ToArray(), null, null, chxDisplayItemsAttachedTo3DModelWithoutDtm.Checked);

                            try
                            {
                                foreach (IDNMcMapLayer layer in dicVectorLayersScale.Keys)
                                {
                                    KeyValuePair<float, float> minMaxScale = dicVectorLayersScale[layer];
                                    DNSLayerParams layerParams = terrain.GetLayerParams(layer);
                                    layerParams.fMinScale = minMaxScale.Key;
                                    layerParams.fMaxScale = minMaxScale.Value;
                                    terrain.SetLayerParams(layer, layerParams);
                                }
                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("DNMcMapTerrain Set/GetLayerParams", McEx);
                            }

                            Manager_MCLayers.RemoveStandaloneLayers(mLayers.ToArray());
                            lViewportTerrains.Add(terrain);
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcMapTerrain Creation", McEx);
                            Enabled = true;
                            return;

                        }
                    }
                 
                    MCTMapLayerReadCallback.RemoveAllReplacedLayer();
                    mLoadDelayLayersTimer.Start();


                }
                else
                {
                   HandleError(); // this.Enabled = true;
                    return;
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Open Map From Layers", McEx);
                HandleError(); //Enabled = true;
            }
        }

        private bool CreateRaw3DModel(string raw3DModelPath, bool raw3DModelWithIndexing)
        {
            if (raw3DModelPath != "")
            {
                IDNMcMapLayer layer = null;
                if (raw3DModelWithIndexing)
                {
                    if (cb3DModelNonDefaultIndexDirectory.Checked && ctrlBrowse3DModelIndexingDataDirectory.FileName == "")
                    {
                        MessageBox.Show("Missing Non Default Index Directory", "Invalid Data");
                        HandleError(); 
                        ctrlBrowse3DModelIndexingDataDirectory.Focus();
                        return false;
                    }
                    layer = Manager_MCLayers.CreateRaw3DModelLayer(raw3DModelPath,
                        chxOrthometricHeights.Checked,
                        ntbResolutionsToIgnore.GetUInt32(),
                        cb3DModelNonDefaultIndexDirectory.Checked ? ctrlBrowse3DModelIndexingDataDirectory.FileName : null);
                }
                else
                {
                    layer = Manager_MCLayers.CreateRaw3DModelLayer(raw3DModelPath,
                        ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem,
                        chxOrthometricHeights.Checked,
                        null,
                        ntxTargetHighestResolution.GetFloat(),
                        m_RequestParams == null ? null : m_RequestParams.ToArray(),
                        ctrlPositionOffset.GetVector3D());
                }

                if (layer == null)
                {
                    HandleError();
                    return false;
                }
                else
                    mLayers.Add(layer);
            }
            return true;
        }

        private void HandleError()
        {
            if (mLayers != null)
            {
                foreach (IDNMcMapLayer layer in mLayers)
                {
                    layer.RemoveLayerAsync();
                    layer.Dispose();
                }
                mLayers.Clear();
            }
            this.Enabled = true;
        }

        private void CreateRawVector3DExtrusion(string filename)
        {
            IDNMcMapLayer layer = null;

            if (mRawVector3DExtrusionIsUseIndexingData)
            {
              
                layer = Manager_MCLayers.CreateRawVector3DExtrusionLayer(filename ,
                                                                         mExtrusionGraphicalParams,
                                                                         ntbExtrusionHeightMaxAddition.GetFloat(),
                                                                         mExtrusionIndexingData);
            }
            else
            {
                if (ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem != null)
                {
                    if (mExtrusionParams.pSourceCoordinateSystem == null)
                        mExtrusionParams.pSourceCoordinateSystem = ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem;
                    if (mExtrusionParams.pTargetCoordinateSystem == null)
                        mExtrusionParams.pTargetCoordinateSystem = ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem;
                }

                mExtrusionParams.strDataSource = filename;
                mExtrusionParams.pTilingScheme = mcTilingScheme;
                layer = Manager_MCLayers.CreateRawVector3DExtrusionLayer(mExtrusionParams,
                                                                         ntbExtrusionHeightMaxAddition.GetFloat());
            }
            if (layer == null)
            {
                //this.Enabled = true;
                HandleError();
                return;
            }
            else
                mLayers.Add(layer);
        }


        public List<IDNMcMapLayer> GetLayers()
        {
            return mLayers;
        }


        private void LoadDelayLayersTimer_Tick(object sender, EventArgs e)
        {
            if (!MCTMapLayerReadCallback.IsCheckNativeServerLayer)
            {
                // if at least on layer failed init
                if (MCTMapLayerReadCallback.GetIsShowFailedError())
                {
                    mLoadDelayLayersTimer.Stop();
                    HandleError();
                    //this.Enabled = true;
                    return;
                }
               
                if( mLayers.Count == 0 || ctrlSmallViewportParams1.GetIsOpenMapWithoutWaitAllLayersInit() || (mLayers.Count > 0 && Manager_MCLayers.IsAllLayersInitialized(mLayers) /*MCTMapLayerReadCallback.IsLayersInitialized()*/) )
                {
                    mLoadDelayLayersTimer.Stop();
                    CreateViewport();
                }
            }
        }

        private void CreateViewport()
        {
            if (mGridCoordSys == null && mLayers.Count > 0 && Manager_MCLayers.IsAllLayersInitialized(mLayers))
            {
                mGridCoordSys = mLayers[0].CoordinateSystem;
            }
            // create new instance of IDNMcOverlayManager
            try
            {
                newMCTOverlayManager = new MCTOverlayManager();
                newDNMcOverlayManager = newMCTOverlayManager.CreateOverlayManager(mGridCoordSys);
                newDNMcOverlayManager.SetScaleFactor(ctrlSmallViewportParams1.GetScaleFactor());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IDNMcOverlayManager Creation", McEx);
                HandleError();
                return;
            }

            // create new instance of MCTMapViewport
            mCurrViewport = new MCTMapViewport();
            mCurrViewport.ShowGeoInMetricProportion = ctrlSmallViewportParams1.GetShowGeoInMetricProportion();
            mCurrViewport.MapType = mMapType;
            mCurrViewport.ID = 1;

            Managers.Manager_MCGeneralDefinitions.m_TerrainResolutionFactor = ctrlSmallViewportParams1.GetTerrainResolutionFactor();
            try
            {
                IDNMcDtmMapLayer[] apQuerySecondaryDtmLayers = Manager_MCLayers.GetLayersAsDTM(m_QuerySecondaryDtmLayers);

                // call func CreateViewportMono to create new viewport and camera to the new map
                mCurrViewport.CreateViewportMono(out mNewViewport,
                                                    out mNewCamera,
                                                    lViewportTerrains.ToArray(),
                                                    ctrlSmallViewportParams1.GetImageCalc(),
                                                    currSchemaDevice,
                                                    newDNMcOverlayManager,
                                                    mGridCoordSys,
                                                    ctrlSmallViewportParams1.GetIsWpfWindow(),
                                                    apQuerySecondaryDtmLayers);

                // if created new viewport
                if (mNewViewport != null)
                {
                    // add the new viewport and terrain to their MC Managers
                    Manager_MCViewports.AddViewport(mNewViewport);

                    if (ctrlSmallViewportParams1.GetIs2D3DChecked())
                    {
                        mCurrViewportSecond = new MCTMapViewport();
                        mCurrViewportSecond.ShowGeoInMetricProportion = ctrlSmallViewportParams1.GetShowGeoInMetricProportion();
                        mCurrViewportSecond.MapType = DNEMapType._EMT_3D;
                        mCurrViewportSecond.ID = 2;

                        mCurrViewportSecond.CreateViewportMono(out mNewViewportSecond,
                                                    out mNewCameraSecond,
                                                    lViewportTerrains.ToArray(),
                                                    null,
                                                    currSchemaDevice,
                                                    newDNMcOverlayManager,
                                                    mGridCoordSys,
                                                    ctrlSmallViewportParams1.GetIsWpfWindow(),
                                                    apQuerySecondaryDtmLayers);

                        // if created new viewport
                        if (mNewViewportSecond != null)
                        {
                            // add the new viewport and terrain to their MC Managers
                            Manager_MCViewports.AddViewport(mNewViewportSecond);
                        }
                    }

                    // turn on viewport render needed flag
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(mNewViewport);
                    if (mNewViewportSecond != null)
                        Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(mNewViewportSecond);
                }

              //  Manager_MCLayers.RemoveStandaloneLayers(apQuerySecondaryDtmLayers);

                mIsOpenMapWithoutWaitAllLayersInitChecked = ctrlSmallViewportParams1.GetIsOpenMapWithoutWaitAllLayersInit();
                this.Close();

                mFinishLoadAllDelayLayersTimer.Start();

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcMapViewport Creation", McEx);
                //this.Enabled = true;
                HandleError();

                return;
            }
        }

        private void FinishLoadAllDelayLayersTimer_Tick(object sender, EventArgs e)
        {
            if (Manager_MCViewports.IsExistViewportInManager(mNewViewport))
            {
                if (MCTMapLayerReadCallback.GetIsShowFailedError())
                {
                    mFinishLoadAllDelayLayersTimer.Stop();
                    return;
                }

                if (Manager_MCLayers.IsAllLayersInitialized(mLayers))
                {
                    mFinishLoadAllDelayLayersTimer.Stop();

                    if(mIsOpenMapWithoutWaitAllLayersInitChecked) 
                        MessageBox.Show("All layers are initialized!", "Open viewport without waiting until all layers are initialized");

                    // set camera position 
                    if (terrain != null)
                    {
                        try
                        {
                            Manager_MCLayers.DrawPriorityServerLayer(terrain);
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("Get/SetLayerParams function", McEx);
                        }
                        try
                        {
                            mNewCamera.SetCameraPosition(terrain.BoundingBox.CenterPoint(), false);
                        }
                        catch (MapCoreException McEx)
                        {
                            if (McEx.ErrorCode == DNEMcErrorCode.NOT_INITIALIZED)
                            {
                                if (!MCTMapLayerReadCallback.GetIsShowError())
                                {
                                    MCTMapLayerReadCallback.SetIsShowError(true);
                                    MessageBox.Show("Viewport includes delayed load layers - can not set camera position to center!", "Set Camera Position To BoundingBox.CenterPoint");
                                }
                            }
                            else
                            {
                                Utilities.ShowErrorMessage("IDNMcMapCamera.SetCameraPosition function", McEx);
                            }
                           
                            return;
                        }
                    }
                    btnCenterMap m_CenterMap = new btnCenterMap();
                    if (mMapType == DNEMapType._EMT_3D)
                    {
                        m_CenterMap.ExecuteAction();
                    }
                    if (ctrlSmallViewportParams1.GetIs2D3DChecked() && mNewViewportSecond != null)
                    {
                        m_CenterMap.ExecuteAction(mNewViewportSecond);
                    }

                    // turn on viewport render needed flag
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(mNewViewport);
                    if (mNewViewportSecond != null)
                        Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(mNewViewportSecond);
                }
            }
            else
            {
                mFinishLoadAllDelayLayersTimer.Stop();
            }
        }
        private bool IsInitilizeDeviceLocalCache()
        {
            return ctrlSmallDeviceParams1.IsInitilizeDeviceLocalCache();
         
        }

        private bool CreateNativeRasterMapLayer(string folderPath, DNSLocalCacheLayerParams localCacheLayerParams, uint uNumLevelsToIgnore)
        {
            uint uDefaultFirstLowerQuality = DNMcConstants._MC_EMPTY_ID;
            bool bDefaultThereAreMissingFiles = false;

            bool isUseLocalCache = localCacheLayerParams.strLocalCacheSubFolder != string.Empty;
            IDNMcMapLayer layer = Manager_MCLayers.CreateNativeRasterLayer(folderPath,
                                            uDefaultFirstLowerQuality,
                                            bDefaultThereAreMissingFiles,
                                            uNumLevelsToIgnore,
                                            chxEnhanceBorderOverlap.Checked,
                                            localCacheLayerParams);
            if (layer == null)
                return false;
            else
            {
                mLayers.Add(layer);
                return true;
            }
        }

        private bool CreateNativeVectorLayer(string folderPath, DNSLocalCacheLayerParams localCacheLayerParams)
        {
            bool isUseLocalCache = localCacheLayerParams.strLocalCacheSubFolder != string.Empty;
            IDNMcMapLayer layer = Manager_MCLayers.CreateNativeVectorLayer(folderPath, localCacheLayerParams);

            if (layer == null)
                return false;
            else
            {
                mLayers.Add(layer);
                return true;
            }
        }

        private bool CreateNativeVectorLayers(string filePath, DNSLocalCacheLayerParams localCacheLayerParams)
        {
            bool result = true;
            try
            {
                bool isUseLocalCache = localCacheLayerParams.strLocalCacheSubFolder != string.Empty;
                string[] lines = File.ReadAllLines(filePath);
                foreach (string nativeVectorData in lines)
                {
                    if (nativeVectorData.Trim() != "")
                    {
                        string[] data = nativeVectorData.Trim().Split('*');
                        if (data.Length > 0)
                        {
                            IDNMcMapLayer layer = Manager_MCLayers.CreateNativeVectorLayer(data[0], localCacheLayerParams);

                            if (layer == null)
                                break;
                            else 
                            {
                                mLayers.Add(layer);
                                if (data.Length > 2)
                                {
                                    float min, max;
                                    bool bMin = float.TryParse(data[1], out min);
                                    bool bMax = float.TryParse(data[2], out max);
                                    if(bMin && bMax)
                                        dicVectorLayersScale.Add(layer, new KeyValuePair<float, float>(min, max));
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error in read from file: " + filePath + Environment.NewLine + ex.Message,
                     "Create Native Vector Layers From List", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            return result;
        }


        private void btnLocalCacheNativeRaster1_Click(object sender, EventArgs e)
        {
            if (IsInitilizeDeviceLocalCache())
            {
                frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderNativeRaster1);
                if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
                {
                    m_localCacheSubFolderNativeRaster1 = frmLocalCacheParams.LocalCacheLayerParams;
                }
            }
        }

        private void btnLocalCacheNativeRaster2_Click(object sender, EventArgs e)
        {
            if (IsInitilizeDeviceLocalCache())
            {
                frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderNativeRaster2);
                if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
                {
                    m_localCacheSubFolderNativeRaster2 = frmLocalCacheParams.LocalCacheLayerParams;
                }
            }
        }

        private void btnLocalCacheNativeRaster3_Click(object sender, EventArgs e)
        {
            if (IsInitilizeDeviceLocalCache())
            {
                frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderNativeRaster3);
                if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
                {
                    m_localCacheSubFolderNativeRaster3 = frmLocalCacheParams.LocalCacheLayerParams;
                }
            }
        }

        private void btnLocalCacheNativeVector1_Click(object sender, EventArgs e)
        {
            if (IsInitilizeDeviceLocalCache())
            {
                frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderNativeVector1);
                if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
                {
                    m_localCacheSubFolderNativeVector1 = frmLocalCacheParams.LocalCacheLayerParams;
                }
            }
        }

        private void btnLocalCacheNativeVector2_Click(object sender, EventArgs e)
        {
            if (IsInitilizeDeviceLocalCache())
            {
                frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderNativeVector2);
                if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
                {
                    m_localCacheSubFolderNativeVector2 = frmLocalCacheParams.LocalCacheLayerParams;
                }
            }
        }

        private void btnLocalCacheNativeVector3_Click(object sender, EventArgs e)
        {
            if (IsInitilizeDeviceLocalCache())
            {
                frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderNativeVector3);
                if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
                {
                    m_localCacheSubFolderNativeVector3 = frmLocalCacheParams.LocalCacheLayerParams;
                }
            }
        }
        private void btnLocalCacheNativeVector4_Click(object sender, EventArgs e)
        {
            if (IsInitilizeDeviceLocalCache())
            {
                frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderNativeVector4);
                if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
                {
                    m_localCacheSubFolderNativeVector4 = frmLocalCacheParams.LocalCacheLayerParams;
                }
            }
        }

        private void btnLocalCacheNativeDTM_Click(object sender, EventArgs e)
        {
            if (IsInitilizeDeviceLocalCache())
            {
                frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderNativeDTM);
                if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
                {
                    m_localCacheSubFolderNativeDTM = frmLocalCacheParams.LocalCacheLayerParams;
                }
            }
        }

        private void btnLocalCacheRawRaster_Click(object sender, EventArgs e)
        {
            if (IsInitilizeDeviceLocalCache())
            {
                frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderRawRaster);
                if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
                {
                    m_localCacheSubFolderRawRaster = frmLocalCacheParams.LocalCacheLayerParams;
                }
            }
        }

        private void btnLocalCacheRawDTM_Click(object sender, EventArgs e)
        {
            if (IsInitilizeDeviceLocalCache())
            {
                frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderRawDTM);
                if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
                {
                    m_localCacheSubFolderRawDTM = frmLocalCacheParams.LocalCacheLayerParams;
                }
            }
        }

        private void btnLocalCacheRawVector_Click(object sender, EventArgs e)
        {
            if (IsInitilizeDeviceLocalCache())
            {
                frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderRawVector);
                if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
                {
                    m_localCacheSubFolderRawVector = frmLocalCacheParams.LocalCacheLayerParams;
                }
            }
        }

        private void btnCreateLayerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mLoadDelayLayersTimer.Enabled)
                mLoadDelayLayersTimer.Stop();
            
            MCTMapLayerReadCallback.ResetReadCallbackCounter(!mIsOpenMapWithoutWaitAllLayersInitChecked);
            if(m_QuerySecondaryDtmLayers != null)
                Manager_MCLayers.RemoveStandaloneLayers(m_QuerySecondaryDtmLayers.ToArray());
            if(mLayers != null)
                Manager_MCLayers.RemoveStandaloneLayers(mLayers.ToArray());
        }

        private void btnTilingScheme_Click(object sender, EventArgs e)
        {
            frmSTilingSchemeParams frmSTilingSchemeParams = new frmSTilingSchemeParams();
            if (mcTilingScheme != null)
                frmSTilingSchemeParams.STilingScheme = mcTilingScheme;
            
            if (frmSTilingSchemeParams.ShowDialog() == DialogResult.OK)
            {
                mcTilingScheme = frmSTilingSchemeParams.STilingScheme;
            }
        }

        private void btnRawVectorParams_Click(object sender, EventArgs e)
        {
            frmRawVectorParams frmRawVectorParams = new frmRawVectorParams();
            if (ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem != null)
            {
                mcRawVectorParams.pSourceCoordinateSystem = ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem;
                frmRawVectorParams.TargetGridCoordinateSystem = ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem;
                ctrlGridCoordinateSystemRawLayers.ClearSelectedList();
            }
            else
            {
                frmRawVectorParams.TargetGridCoordinateSystem = mRawVectorTargetGridCoordinateSystem;
            }
            frmRawVectorParams.RawVectorParams = mcRawVectorParams;
            frmRawVectorParams.SetDisableStyling(ctrlBrowseLayerRawVector.FileName);

            if (frmRawVectorParams.ShowDialog() == DialogResult.OK)
            {
                mcRawVectorParams = frmRawVectorParams.RawVectorParams;
                mRawVectorTargetGridCoordinateSystem = frmRawVectorParams.TargetGridCoordinateSystem;
            }
        }


        private void btnRawVector3DExtrusionParams_Click(object sender, EventArgs e)
        {
            if (ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem != null)
            {
                mExtrusionParams.pSourceCoordinateSystem = ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem;
                mExtrusionParams.pTargetCoordinateSystem = ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem;
                ctrlGridCoordinateSystemRawLayers.ClearSelectedList();
            }

            frmSRawVector3DExtrusionParams frmSRawVector3DExtrusionParams = new frmSRawVector3DExtrusionParams(ctrlBrowserRawVector3DExtrusion.FileName, mExtrusionParams);
            frmSRawVector3DExtrusionParams.SetIndexingData(mRawVector3DExtrusionIsUseIndexingData, mExtrusionIndexingData);

            if (frmSRawVector3DExtrusionParams.ShowDialog() == DialogResult.OK)
            {
                mExtrusionParams = frmSRawVector3DExtrusionParams.ExtrusionParams;
                mExtrusionGraphicalParams = frmSRawVector3DExtrusionParams.ExtrusionGraphicalParams;
                mRawVector3DExtrusionIsUseIndexingData = frmSRawVector3DExtrusionParams.GetIsUseIndexing();
                mExtrusionIndexingData = frmSRawVector3DExtrusionParams.GetIndexingDataDirectory();
            }

        }

        private void btnRawVectorMultiLayer_Click(object sender, EventArgs e)
        {

        }

        private void ctrlBrowseLayerRawVector_Leave(object sender, EventArgs e)
        {
           
        }

        private void ctrlBrowseLayerRawVector_Validated(object sender, EventArgs e)
        {

        }

        private void btnLocalCacheNativeMaterial_Click(object sender, EventArgs e)
        {
            if (IsInitilizeDeviceLocalCache())
            {
                frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderNativeMaterial);
                if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
                {
                    m_localCacheSubFolderNativeMaterial = frmLocalCacheParams.LocalCacheLayerParams;
                }
            }
        }

        private void btnLocalCacheNativeTraversability_Click(object sender, EventArgs e)
        {
            frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderNativeTraversability);
            if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
            {
                m_localCacheSubFolderNativeTraversability = frmLocalCacheParams.LocalCacheLayerParams;
            }
        }

        private void btnLocalCacheNativeHeat_Click(object sender, EventArgs e)
        {
            frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderNativeHeat);
            if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
            {
                m_localCacheSubFolderNativeHeat = frmLocalCacheParams.LocalCacheLayerParams;
            }
        }

        private void CheckDevice()
        {
            currSchemaDevice = ctrlSmallDeviceParams1.CheckDevice();
        }

        private DNSWebMapServiceParams GetWebMapServiceParams(DNEWebMapServiceType eWebMapServiceType)
        {
            SetRequestParams();
            DNSWebMapServiceParams webMapServiceParams = m_SWMTSParams;
            if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WCS)
                webMapServiceParams = m_SWCSParams;
            else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMS)
                webMapServiceParams = m_SWMSParams;
            return webMapServiceParams;
        }

        public void GetWebServerLayers(DNEMcErrorCode eStatus, string strServerURL, DNEWebMapServiceType eWebMapServiceType, DNSServerLayerInfo[] aLayers, string[] astrServiceMetadataURLs, string urlServer, string strServiceProviderName)
        {
            List<Manager_MCLayers.MCTWebServerUserSelection> mServerSelectLayers2 = null;
            if (mDicServerSelectLayers.ContainsKey(urlServer))
                mServerSelectLayers2 = mDicServerSelectLayers[urlServer];

            if (eStatus == DNEMcErrorCode.SUCCESS)
            {
                bool isOpenAsOneLayer = false;
                if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS)
                    isOpenAsOneLayer = mWMTSOpenAllLayersAsOne;
                if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMS)
                    isOpenAsOneLayer = mWMSOpenAllLayersAsOne;

                mFrmWebMapLayers = new FrmWebMapLayers(mServerSelectLayers2, eWebMapServiceType, this);
                mFrmWebMapLayers.SetWebServerLayers(eStatus, strServerURL, GetWebMapServiceParams(eWebMapServiceType),
                    aLayers, astrServiceMetadataURLs, urlServer, strServiceProviderName, isOpenAsOneLayer, mIsWCSRaster, true, m_Raw3DModelParams, mDicWMTSFromCSWLayersData);

                mFrmWebMapLayers.ShowDialog();
                AfterUserSelectServerLayers(urlServer, eWebMapServiceType);
            }
            else
            {
                MessageBox.Show("Return status : " + eStatus.ToString() + ".", "Get Web Server Layers");

            }
        }

        public void AfterUserSelectServerLayers(string urlServer, DNEWebMapServiceType eWebMapServiceType)
        {
            if (mFrmWebMapLayers.DialogResult == DialogResult.OK)
            {
                mDicWMTSFromCSWLayersData = mFrmWebMapLayers.WMTSFromCSWLayersData;

                List<Manager_MCLayers.MCTWebServerUserSelection> mServerSelectLayers = mFrmWebMapLayers.SelectLayers;

                if (eWebMapServiceType == DNEWebMapServiceType._EWMS_MAPCORE)
                {
                    lblMapCoreServerLayersCount.Text = mServerSelectLayers.Count + " layers";
                }
                else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS)
                {
                    m_SWMTSParams = (DNSWMTSParams)mFrmWebMapLayers.GetWebMapServiceParams();
                    lblWMTSServerLayersCount.Text = mServerSelectLayers.Count + " layers";
                    mWMTSOpenAllLayersAsOne = mFrmWebMapLayers.OpenAllLayersAsOne;
                }
                else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WCS)
                {
                    m_SWCSParams = (DNSWCSParams)mFrmWebMapLayers.GetWebMapServiceParams();
                    mIsWCSRaster = mFrmWebMapLayers.IsRaster;
                    lblWCSServerLayersCount.Text = mServerSelectLayers.Count + " layers";
                }
                else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMS)
                {
                    m_SWMSParams = (DNSWMSParams)mFrmWebMapLayers.GetWebMapServiceParams();
                    lblWMSServerLayersCount.Text = mServerSelectLayers.Count + " layers";
                    mWMSOpenAllLayersAsOne = mFrmWebMapLayers.OpenAllLayersAsOne;
                }
                else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_CSW)
                {
                    m_Raw3DModelParams = mFrmWebMapLayers.GetRaw3DModelParams();
                    int numLayers = 0;
                    if(mServerSelectLayers.Count > 0)
                    {
                        if (mServerSelectLayers[0].ServerLayerInfo.strLayerType == Manager_MCLayers.WMTSFromCSWType && mDicWMTSFromCSWLayersData != null)
                        {
                            foreach (string strServer in mDicWMTSFromCSWLayersData.Keys)
                            {
                                if (mDicWMTSFromCSWLayersData[strServer].SelectedLayers != null)
                                    numLayers += mDicWMTSFromCSWLayersData[strServer].SelectedLayers.Count;
                            }
                        }
                        else
                            numLayers = mServerSelectLayers.Count;
                    }
                    lblCSWServerLayersCount.Text = numLayers + " layers";
                }

                if (mDicServerSelectLayers.ContainsKey(urlServer))
                    mDicServerSelectLayers.Remove(urlServer);
                mDicServerSelectLayers.Add(urlServer, mServerSelectLayers);

                if (mDicServerSelectLayersType.ContainsKey(urlServer))
                    mDicServerSelectLayersType.Remove(urlServer);
                mDicServerSelectLayersType.Add(urlServer, eWebMapServiceType);
            }
        }

        private void btnOpenServerLayers_Click(object sender, EventArgs e)
        {
            if (txtMapCoreServerPath.Text != "")
                CheckDevice();

            Manager_MCLayers.OpenServerLayers_Click(txtMapCoreServerPath, DNEWebMapServiceType._EWMS_MAPCORE, this, m_RequestParams);
        }


        private void btnWMTSOpenServerLayers_Click(object sender, EventArgs e)
        {
            if (txtWMTSServerPath.Text != "")
                CheckDevice();

            Manager_MCLayers.OpenServerLayers_Click(txtWMTSServerPath, DNEWebMapServiceType._EWMS_WMTS, this, m_RequestParams);
        }

        private void btnWCSOpenServerLayers_Click(object sender, EventArgs e)
        {
            if (txtWCSServerPath.Text != "")
                CheckDevice();

            Manager_MCLayers.OpenServerLayers_Click(txtWCSServerPath, DNEWebMapServiceType._EWMS_WCS, this, m_RequestParams);
        }

        private void btnWMSOpenServerLayers_Click(object sender, EventArgs e)
        {
            if (txtWMSServerPath.Text != "")
                CheckDevice();

            Manager_MCLayers.OpenServerLayers_Click(txtWMSServerPath, DNEWebMapServiceType._EWMS_WMS, this, m_RequestParams);
        }

        private void btnCSWOpenServerLayers_Click(object sender, EventArgs e)
        {
            if (txtCSWServerPath.Text != "")
                CheckDevice();

            List<DNSMcKeyStringValue> cswRequestParams = MCTMapForm.GetRequestParamsWithCSWBody(m_RequestParams);

            Manager_MCLayers.OpenServerLayers_Click(txtCSWServerPath, DNEWebMapServiceType._EWMS_CSW, this, cswRequestParams);
        }

        private void cb3DModelNonDefaultIndexDirectory_CheckedChanged(object sender, EventArgs e)
        {
            ctrlBrowse3DModelIndexingDataDirectory.Enabled = cb3DModelNonDefaultIndexDirectory.Checked;
            if (!cb3DModelNonDefaultIndexDirectory.Checked)
                ctrlBrowse3DModelIndexingDataDirectory.FileName = "";
        }

        private void btnRequestParams_Click(object sender, EventArgs e)
        {
            frmRequestParams frmKeyValueArray1 = new frmRequestParams(m_RequestParams, "Server Request Params");
            frmKeyValueArray1.CSWValue = MCTMapForm.CSWBodyValue;
            if (frmKeyValueArray1.ShowDialog() == DialogResult.OK)
            {
                m_RequestParams = frmKeyValueArray1.GetMcKeyStringValues();

                MCTMapForm.CSWBodyValue = frmKeyValueArray1.CSWValue;
            }
        }

        private void SetRequestParams()
        {
            m_SWCSParams.aRequestParams = m_RequestParams == null ? null : m_RequestParams.ToArray();
            m_SWMTSParams.aRequestParams = m_RequestParams == null ? null : m_RequestParams.ToArray();
            m_SWMSParams.aRequestParams = m_RequestParams == null ? null : m_RequestParams.ToArray();
            m_Raw3DModelParams.aRequestParams = m_RequestParams == null ? null : m_RequestParams.ToArray();
        }

        private void btnLocalCacheRawMaterial_Click(object sender, EventArgs e)
        {
            if (IsInitilizeDeviceLocalCache())
            {
                frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderRawMaterial);
                if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
                {
                    m_localCacheSubFolderRawMaterial = frmLocalCacheParams.LocalCacheLayerParams;
                }
            }
        }

        private void btnLocalCacheRawTraversability_Click(object sender, EventArgs e)
        {
            if (IsInitilizeDeviceLocalCache())
            {
                frmLocalCacheParam frmLocalCacheParams = new frmLocalCacheParam(m_localCacheSubFolderRawTraversability);
                if (frmLocalCacheParams.ShowDialog() == DialogResult.OK)
                {
                    m_localCacheSubFolderRawTraversability = frmLocalCacheParams.LocalCacheLayerParams;
                }
            }
        }


        private void btnAddDTMLayers_Click(object sender, EventArgs e)
        {
            CheckDevice();
            LayersWizzardForm layersWizzardForm = new LayersWizzardForm(m_QuerySecondaryDtmLayers, true);
            if (layersWizzardForm.ShowDialog() == DialogResult.OK)
            {
                m_QuerySecondaryDtmLayers = layersWizzardForm.GetLayers();
            }
        }
    }
}
