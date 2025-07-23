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
using MCTester.MapWorld.WebMapLayers;
using MCTester.Managers.MapWorld;
using static MCTester.Managers.MapWorld.Manager_MCLayers;
using MCTester.MapWorld;
using MCTester.MapWorld.Assist_Forms;

namespace MCTester.Controls
{
    public partial class ucLayerParams : UserControl, ISetTilingScheme
    {

        private DNSTilingScheme mcTilingScheme = null;

        private DNSRawVector3DExtrusionParams mExtrusionParams;
        private DNSRawVector3DExtrusionGraphicalParams mExtrusionGraphicalParams;

        private List<IDNMcMapLayer> lstExistLayers = new List<IDNMcMapLayer>();
        private List<IDNMcMapLayer> m_lstRawVectorLayers = new List<IDNMcMapLayer>();

        private Dictionary<string, List<MCTWebServerUserSelection>> mDicServerSelectLayers2 = new Dictionary<string, List<MCTWebServerUserSelection>>();

        private Dictionary<string, DNEWebMapServiceType> mDicServerSelectLayersType = new Dictionary<string, DNEWebMapServiceType>();

        private bool m_IsDoActionsIfFileSelectedOrChanged = false;
        private bool m_RawVectorIsMultiSelect = true;
        private bool m_RawVectorIsSelectStyling = true;
        private bool m_isTextChanged = false;
        private bool m_isReadOnly = false;
        public event EventHandler SelectedIndexChanged;


        public ucLayerParams()
        {
            InitializeComponent();
            //ShowNonNativeParams();
            Manager_MCLayers.LoadCmbLayers(cmbMapLayerType);

            noudNoOfLayers.Value = 1;

           // browseLayerCtrl.FileNameSelected += CtrlBrowseLayer_FileNameSelectedOrChanged;
            browseLayerCtrl.FileNameChanged += CtrlBrowseLayer_FileNameSelectedOrChanged;
            ctrlNonNativeParams1.SetTilingSchemeForm(this);

            DNSMcBox box = new DNSRawParams().MaxWorldLimit;
            ctrl3DMaxWorldBoundingBox.SetVector3D(box.MaxVertex);
            ctrl3DMinWorldBoundingBox.SetVector3D(box.MinVertex);

        }

        private bool m_IsFilterDTMMapLayers = false;

        public void SetFilterDTMLayer()
        {
            Manager_MCLayers.LoadCmbLayers(cmbMapLayerType, true);
            m_IsFilterDTMMapLayers = true;
        }
            

        public void SetIsDoActionsIfFileSelectedOrChanged(bool isDoActionsIfFileSelectedOrChanged)
        {
            m_IsDoActionsIfFileSelectedOrChanged = isDoActionsIfFileSelectedOrChanged;
        }
       
        public void IsReadOnly(bool isReadOnly)
        {
            m_isReadOnly = isReadOnly;
            ctrlNonNativeParams1.IsReadOnly(isReadOnly);
            ctrlWebServiceLayerParams1.IsReadOnly(isReadOnly);
            rawVectorParams1.IsReadOnly(isReadOnly);
            ctrlRawVector3DExtrusionParams1.IsReadOnly(isReadOnly);
            ctrlRaw3DModelParams1.IsReadOnly(isReadOnly);

        }

        public void HideNoOfLayers()
        {
            noudNoOfLayers.Visible = false;
            lblNoOfLayers.Visible = false;
        }

        public void SetTilingScheme(DNSTilingScheme tilingScheme)
        {
            mcTilingScheme = tilingScheme;
        }

        public DNSTilingScheme GetTilingScheme()
        {
            return mcTilingScheme;
        }

       

        public void SetRawVectorFlags(bool IsSelectStyling, bool IsMultiSelect)
        {
            m_RawVectorIsMultiSelect = IsMultiSelect;
            m_RawVectorIsSelectStyling = IsSelectStyling;
        }
        private void CtrlBrowseLayer_FileNameSelectedOrChanged(object sender, EventArgs e)
        {
            if (browseLayerCtrl.FileName != "")
            {
                if (m_IsDoActionsIfFileSelectedOrChanged)
                {
                    if (cmbMapLayerType.Text == "RAW_VECTOR" || cmbMapLayerType.Text == "RAW_VECTOR_3D_EXTRUSION")
                    {
                        if (!m_isTextChanged)
                        {
                            m_isTextChanged = true;
                          
                            browseLayerCtrl.FileName = Manager_MCLayers.CheckRawVector(browseLayerCtrl.FileName, m_RawVectorIsSelectStyling && cmbMapLayerType.Text == "RAW_VECTOR", m_RawVectorIsMultiSelect);
                            rawVectorParams1.SetDisableStyling(browseLayerCtrl.FileName);
                            m_isTextChanged = false;
                        }
                    }
                    //else 
                }
                if (cmbMapLayerType.Text == "RAW_RASTER" || cmbMapLayerType.Text == "RAW_DTM")
                {
                    ctrlRawRasterComponentParams1.SetBaseDirectory(browseLayerCtrl.FileName);
                }
                if (cmbMapLayerType.Text == "RAW_VECTOR_3D_EXTRUSION")
                    ctrlRawVector3DExtrusionParams1.SetLayerFileName(browseLayerCtrl.FileName);
            }
        }

        private void cmbMapLayerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            browseLayerCtrl.Visible = true;
            lblNoOfIgnore.Visible = true;
            ntxNumLevelsToIgnore.Visible = true;
            chxEnhanceBorderOverlap2.Visible = false;
            chxImageCoordSys.Visible = true;
            ctrlNonNativeParams1.Visible = false;

            lblFirstLowerQualityLevel.Visible = false;
            ntxFirstLowerQualityLevel.Visible = false;
            cbxThereAreMissingFiles.Visible = false;
            ctrlLocalCacheParams.Visible = true;
            tcParams.Visible = false;
            ctrlLocalCacheParams.Enabled = MCTMapDevice.IsInitilizeDeviceLocalCache();

            btnTilingScheme1.Visible = false;


            switch (cmbMapLayerType.Text)
            {
                case "NATIVE_SERVER_3D_MODEL":
                case "NATIVE_SERVER_VECTOR_3D_EXTRUSION":
                case "NATIVE_SERVER_VECTOR":
                case "NATIVE_SERVER_RASTER":
                case "NATIVE_SERVER_TRAVERSABILITY":
                case "NATIVE_SERVER_MATERIAL":
                case "NATIVE_SERVER_DTM":
                    browseLayerCtrl.IsFolderDialog = true;
                    ctrlLocalCacheParams.Visible = false;
                    lblNoOfIgnore.Visible = false;
                    ntxNumLevelsToIgnore.Visible = false;
                    break;
               case "NATIVE_DTM":
                    browseLayerCtrl.IsFolderDialog = true;
                    break;
                case "NATIVE_RASTER":
                    browseLayerCtrl.IsFolderDialog = true;
                    lblFirstLowerQualityLevel.Visible = true;
                    ntxFirstLowerQualityLevel.Visible = true;
                    cbxThereAreMissingFiles.Visible = true;
                    chxEnhanceBorderOverlap2.Visible = true;
                    break;
                case "NATIVE_VECTOR":
                    browseLayerCtrl.IsFolderDialog = true;
                    lblNoOfIgnore.Visible = false;
                    ntxNumLevelsToIgnore.Visible = false;
                    break;
                
                case "NATIVE_VECTOR_3D_EXTRUSION":
                    tcParams.Visible = true;
                    browseLayerCtrl.IsFolderDialog = true;
                    tcParams.SelectedTab = tpVector3DExtrusionParams;
                    ctrlRawVector3DExtrusionParams1.Visible = false;
                    break;
                case "RAW_VECTOR_3D_EXTRUSION":
                    tcParams.Visible = true;
                    browseLayerCtrl.IsFolderDialog = false;
                    tcParams.SelectedTab = tpVector3DExtrusionParams;
                    ctrlRawVector3DExtrusionParams1.Visible = true;
                    btnTilingScheme1.Visible = true;
                    CtrlBrowseLayer_FileNameSelectedOrChanged(null, null);
                    break;
                case "RAW_3D_MODEL":
                    tcParams.Visible = true;
                    browseLayerCtrl.IsFolderDialog = true;
                    browseLayerCtrl.Visible = true;
                    tcParams.SelectedTab = tp3DModelParams;
                    break;
                case "RAW_DTM":
                case "RAW_RASTER":
                case "RAW_TRAVERSABILITY":
                case "RAW_MATERIAL":
                    tcParams.Visible = true;
                    tcParams.SelectedTab = tpRawLayerParams;
                    tcLayerParams.SelectedTab = tpRawParams;
                    cmbMapLayerType.Select();
                    browseLayerCtrl.IsFolderDialog = true;

                    ctrlNonNativeParams1.TransparentColorEnabledButtons(true);
                    ctrlNonNativeParams1.TransparentColorPrecisionEnabled(true);

                    ctrlNonNativeParams1.Visible = true;

                    ntbMaxNumOpenFiles.Visible = true;
                    SetNonNativeParams();
                    chxImageCoordSys.Visible = cmbMapLayerType.Text == "RAW_RASTER";

                    break;
                case "RAW_VECTOR":

                    tcParams.Visible = true;
                    tcParams.SelectedTab = tpVectorLayerParams;
                    cmbMapLayerType.Select();
                    browseLayerCtrl.IsFolderDialog = false;
                    btnTilingScheme1.Visible = true;
                    SetNonNativeParams();
                    CtrlBrowseLayer_FileNameSelectedOrChanged(null, null);
                    
                    break;
                   
                case "NATIVE_3D_MODEL":
                    browseLayerCtrl.IsFolderDialog = true;
                    ctrlLocalCacheParams.Visible = false;
                    lblNoOfIgnore.Visible = true;
                    ntxNumLevelsToIgnore.Visible = true;
                    break;
                case "NATIVE_HEAT_MAP":
                    browseLayerCtrl.IsFolderDialog = true;
                    lblFirstLowerQualityLevel.Visible = true;
                    ntxFirstLowerQualityLevel.Visible = true;
                    cbxThereAreMissingFiles.Visible = true;
                    chxEnhanceBorderOverlap2.Visible = true;
                    ctrlLocalCacheParams.Visible = false;
                    break;
                case "NATIVE_TRAVERSABILITY":
                case "NATIVE_MATERIAL":
                    browseLayerCtrl.IsFolderDialog = true;
                    lblFirstLowerQualityLevel.Visible = true;
                    ntxFirstLowerQualityLevel.Visible = true;
                    cbxThereAreMissingFiles.Visible = true;
                    break;
                case "WEB_SERVICE_RASTER":
                case "WEB_SERVICE_DTM":
                    /*    tbServerURL.Text = "http://10.0.217.7:8080/geoserver/gwc/service/wmts?request=GetCapabilities";
                        tbLayers.Text = "israel:Netanya-full8";
                        txImageFormat.Text = "jpeg";
                        ctrlBoundingBox.BoxValue = new DNSMcBox(3485400, 3227300, 0, 3485900,3226700 ,0);
                        tbServerCoordinateSystem.Text = "EPSG:4326";
                        */
                    tcParams.Visible = true;
                    tcParams.SelectedTab = tpRawLayerParams;
                    tcLayerParams.SelectedTab = tpWMSLayerParams;
                    cmbMapLayerType.Select();

                    ctrlNonNativeParams1.TransparentColorEnabledButtons(true);
                    ctrlNonNativeParams1.TransparentColorPrecisionEnabled(true);
                    ctrlNonNativeParams1.Visible = true;
                    //btnTilingScheme1.Visible = true;

                    browseLayerCtrl.Visible = false;
                    browseLayerCtrl.IsFolderDialog = false;
                    lblNoOfIgnore.Visible = false;
                    ntxNumLevelsToIgnore.Visible = false;

                    //ShowNonNativeParams();

                    break;
            }

            if (SelectedIndexChanged != null)
                SelectedIndexChanged(sender, e);
        }

        internal void SetRaw3DModelParams(MCTRaw3DModelParams raw3DModelParams)
        {
            ctrlRaw3DModelParams1.SetRaw3DModelParams(raw3DModelParams);
        }

        internal void SetDefaultValues()
        {
            cmbMapLayerType.Text = DNELayerType._ELT_NATIVE_RASTER.ToString().Replace(LayerTypePrefix, "");
            SetLayerFileName("");
            ntxNumLevelsToIgnore.SetUInt32(0);
            ntxFirstLowerQualityLevel.Text = "MAX";
            cbxThereAreMissingFiles.Checked = false;
            chxEnhanceBorderOverlap2.Checked = false;

            ctrlLocalCacheParams.SubFolderPath = "";
            ntxFirstPyramidResolution.Text = "";
            ntbMaxNumOpenFiles.SetInt(5000);
            chxImageCoordSys.Checked = false;
            cbIgnoreRasterPalette.Checked = false;
            txtPyramidResolution.Text = "";
            ntxHighestResolution.Text = "";

            DNSMcBox box = new DNSRawParams().MaxWorldLimit;
            ctrl3DMaxWorldBoundingBox.SetVector3D(box.MaxVertex);
            ctrl3DMinWorldBoundingBox.SetVector3D(box.MinVertex);

           /* ctrl3DMinWorldBoundingBox.SetEmptyValue();
            ctrl3DMaxWorldBoundingBox.SetEmptyValue();*/

            ctrlNonNativeParams1.SetNonNativeParams(new DNSNonNativeParams());
            ctrlRawRasterComponentParams1.SetDefaultValues();

            ctrlRawVector3DExtrusionParams1.SetExtrusionParams(new DNSRawVector3DExtrusionParams());
            ctrlWebServiceLayerParams1.SetParams(new DNSWCSParams());
            ctrlWebServiceLayerParams1.SetParams(new DNSWMSParams());
            ctrlWebServiceLayerParams1.SetParams(new DNSWMTSParams());
            rawVectorParams1.RawVectorParams = new DNSRawVectorParams("", null);

            ntbExtrusionHeightMaxAddition.Text = "";
            ctrlRaw3DModelParams1.SetRaw3DModelParams(new MCTRaw3DModelParams());
        }

        private void SetNonNativeParams()
        {
            ctrlNonNativeParams1.SetNonNativeParams(new DNSNonNativeParams());
            mcTilingScheme = Manager_MCLayers.mcCurrSNonNativeParams.pTilingScheme;
        }

        private void ucLayerParams_Load(object sender, EventArgs e)
        {
           
        }

        public DNSLocalCacheLayerParams? GetLocalCacheLayerParams()
        {
            DNSLocalCacheLayerParams? localCacheLayerParams = null;
            if (ctrlLocalCacheParams.SubFolderPath != string.Empty)
            {
                localCacheLayerParams = ctrlLocalCacheParams.GetLocalCacheLayerParams();
            }
            return localCacheLayerParams;
        }

        public void SetLocalCacheLayerParams(DNSLocalCacheLayerParams? localCacheLayerParams)
        {
            if (localCacheLayerParams != null)
            {
                ctrlLocalCacheParams.SetLocalCacheLayerParams(localCacheLayerParams.Value);
            }
            else
            {
                ctrlLocalCacheParams.ClearData();
            }
        }

        public string GetLayerFileName()
        {
            return browseLayerCtrl.FileName; 
        }

        public void SetLayerFileName(string value)
        {
            browseLayerCtrl.FileName = value;
        }

        public string GetMapLayerType()
        {
            return cmbMapLayerType.Text;
        }

        public DNELayerType GetMapLayerTypeAsEnum()
        {
            // (DNELayerType)Enum.Parse(typeof(DNELayerType), Manager_MCLayers.LayerTypePrefix + ucLayerParams1.GetMapLayerType());
            return (DNELayerType)Enum.Parse(typeof(DNELayerType), LayerTypePrefix +  cmbMapLayerType.Text);
        }

        public void SetMapLayerType(DNELayerType value)
        {
            cmbMapLayerType.Text = value.ToString().Replace(LayerTypePrefix, ""); ;
        }

        public void SetMapLayerType(string value)
        {
            cmbMapLayerType.Text = value;
        }

        public uint GetNumOfLayers()
        {
            return (uint)noudNoOfLayers.Value;
        }

        public void SetNumOfLayers(uint value)
        {
            noudNoOfLayers.Value = value;
        }

        public uint GetNumLevelsToIgnore()
        {
            return ntxNumLevelsToIgnore.GetUInt32();
        }

        public void SetNumLevelsToIgnore(uint value)
        {
            ntxNumLevelsToIgnore.SetUInt32(value);
        }

        public bool GetThereAreMissingFiles()
        {
            return cbxThereAreMissingFiles.Checked;
        }

        internal MCTRaw3DModelParams GetRaw3DModelParams()
        {
            return ctrlRaw3DModelParams1.GetRaw3DModelParams();
        }

        public void SetThereAreMissingFiles(bool value)
        {
            cbxThereAreMissingFiles.Checked = value;
        }

        public uint GetFirstLowerQualityLevel()
        {
            return ntxFirstLowerQualityLevel.GetUInt32();
        }

        public void SetFirstLowerQualityLevel(uint value)
        {
            ntxFirstLowerQualityLevel.SetUInt32(value);
        }

        public bool GetEnhanceBorderOverlap2()
        {
            return chxEnhanceBorderOverlap2.Checked;
        }

        public void SetEnhanceBorderOverlap2(bool value)
        {
            chxEnhanceBorderOverlap2.Checked = value;
        }

      

        // DNSRawParams
        public DNSRawParams GetSRawParams()
        {
            uint[] pyramidArr = GetPyramidResolutionArray();
            if (pyramidArr == null && txtPyramidResolution.Text != "")
            {
                MessageBox.Show("Invalid Pyramid Resolution Values");
                return null;
            }

            DNSRawParams dnParams = new DNSRawParams();
            dnParams = (DNSRawParams)GetNonNativeParams(dnParams);
            dnParams.strDirectory = GetLayerFileName();
            dnParams.aComponents = ctrlRawRasterComponentParams1.GetComponentParamsList().ToArray();
            dnParams.uMaxNumOpenFiles = ntbMaxNumOpenFiles.GetUInt32();
            dnParams.fFirstPyramidResolution = ntxFirstPyramidResolution.GetFloat();
            dnParams.auPyramidResolutions = pyramidArr;
            dnParams.bIgnoreRasterPalette = cbIgnoreRasterPalette.Checked;
            dnParams.bPostProcessSourceData = chxPostProcessSourceData.Checked;
            dnParams.fHighestResolution = ntxHighestResolution.GetFloat();
            dnParams.MaxWorldLimit = new DNSMcBox(ctrl3DMinWorldBoundingBox.GetVector3D(), ctrl3DMaxWorldBoundingBox.GetVector3D());

            return dnParams;
        }

        private void SetNonNativeParams(DNSNonNativeParams nonNativeParams)
        {
            ctrlNonNativeParams1.SetNonNativeParams(nonNativeParams);
            SetTilingScheme(nonNativeParams.pTilingScheme);
            //Manager_MCLayers.mcCurrSNonNativeParams = nonNativeParams;
        }

        public void SetRawParams(DNSRawParams dnParams)
        {
            SetNonNativeParams((DNSNonNativeParams)dnParams);
            SetLayerFileName(dnParams.strDirectory);

            ctrlRawRasterComponentParams1.SetComponentParamsList(dnParams.aComponents.ToList());
            ntbMaxNumOpenFiles.SetUInt32(dnParams.uMaxNumOpenFiles);
            ntxFirstPyramidResolution.SetFloat(dnParams.fFirstPyramidResolution);
            cbIgnoreRasterPalette.Checked = dnParams.bIgnoreRasterPalette;
            chxPostProcessSourceData.Checked = dnParams.bPostProcessSourceData;
            ntxHighestResolution.SetFloat(dnParams.fHighestResolution);
            if (dnParams.MaxWorldLimit != null)
            {
                ctrl3DMinWorldBoundingBox.SetVector3D( dnParams.MaxWorldLimit.MinVertex);
                ctrl3DMaxWorldBoundingBox.SetVector3D( dnParams.MaxWorldLimit.MaxVertex);
            }
            if (dnParams.auPyramidResolutions != null)
            {
                txtPyramidResolution.Text = "";
                foreach (uint resolution in dnParams.auPyramidResolutions)
                    txtPyramidResolution.Text += (resolution + " ");
            }
            ctrlRawRasterComponentParams1.HideAddToListBtn();
        }

        private uint[] GetPyramidResolutionArray()
        {
            uint[] pyramidArr = null;

            if (txtPyramidResolution.Text != "")
            {
                string[] IDs = txtPyramidResolution.Text.Trim().Split(' ');

                if (IDs.Length > 0)
                {
                    pyramidArr = new uint[IDs.Length];
                    int i = 0;

                    foreach (string strID in IDs)
                    {
                        uint result = 0;
                        bool tryParse = true;
                        tryParse = UInt32.TryParse(strID, out result);
                        if (tryParse)
                            pyramidArr[i] = result;
                        else
                        {

                            txtPyramidResolution.Select();
                            pyramidArr = null;
                            break;
                        }
                        ++i;
                    }
                }
            }

            return pyramidArr;
        }

        private DNSNonNativeParams GetNonNativeParams(DNSNonNativeParams nonNativeParams)
        {
            ctrlNonNativeParams1.GetNonNativeParams(nonNativeParams);
            //nonNativeParams.pTilingScheme = mcTilingScheme;
            Manager_MCLayers.mcCurrSNonNativeParams = nonNativeParams;
            return nonNativeParams;
        }

        public bool IsUserSelectGridCoordinateSystem()
        {
            return ctrlNonNativeParams1.GetGridCoordinateSystem() != null;
        }

        public bool GetImageCoordSys()
        {
            return chxImageCoordSys.Checked;
        }

        public void SetImageCoordSys(bool value)
        {
            chxImageCoordSys.Checked = value;
        }

        public DNSRawVectorParams GetRawVectorParams(bool isCheckSourceCoordinateSystem = true)
        {
            DNSRawVectorParams rawVectorParams = rawVectorParams1.RawVectorParams;
            rawVectorParams.strDataSource = GetLayerFileName();

            return rawVectorParams;
        }

        public void SetRawVectorParams(DNSRawVectorParams rawVectorParams)
        {
            SetLayerFileName(rawVectorParams.strDataSource);
            rawVectorParams1.RawVectorParams = rawVectorParams;
        }

        public void HideGridCoordinateSystemParams()
        {
            rawVectorParams1.HideGridCoordinateSystemParams();
        }

        public DNSRawVectorParams GetRawVectorParamsFromLayer()
        {
            DNSRawVectorParams rawVectorParams = rawVectorParams1.RawVectorParams;
            rawVectorParams.strDataSource = GetLayerFileName();

            return rawVectorParams;
        }


        public IDNMcGridCoordinateSystem GetTargetGridCoordinateSystem()
        {
            return rawVectorParams1.TargetGridCoordinateSystem;
        }

        public void SetTargetGridCoordinateSystem(IDNMcGridCoordinateSystem value)
        {
            rawVectorParams1.TargetGridCoordinateSystem = value;
        }

        public float GetExtrusionHeightMaxAddition()
        {
            return ntbExtrusionHeightMaxAddition.GetFloat();
        }

        public void SetExtrusionHeightMaxAddition(float value)
        {
            ntbExtrusionHeightMaxAddition.SetFloat(value);
        }

        public string GetExtrusionIndexingDataDirectory()
        {
            return ctrlRawVector3DExtrusionParams1.GetIndexingDataDirectory();
        }

        public void SetExtrusionIndexingDataDirectoryFromLayer(string indexingDataDirectory, bool pbNonDefaultIndexingDataDirectory)
        {
            ctrlRawVector3DExtrusionParams1.SetIndexingData(indexingDataDirectory != "", indexingDataDirectory, pbNonDefaultIndexingDataDirectory);
        }

        public void SetReadOnlyBrowseIndexingDataDirectory()
        {
            ctrlRawVector3DExtrusionParams1.SetReadOnlyBrowseIndexingDataDirectory();
        }

        public bool GetIsUseIndexing()
        {
            return ctrlRawVector3DExtrusionParams1.GetIsUseIndexing();
        }

        public DNSRawVector3DExtrusionParams GetRawVector3DExtrusionParams()
        {
            mExtrusionParams = ctrlRawVector3DExtrusionParams1.GetExtrusionParams();
            mExtrusionParams.strDataSource = GetLayerFileName();
            mExtrusionParams.pTilingScheme = mcTilingScheme;
            return mExtrusionParams;
        }

        public void SetRawVector3DExtrusionParams(DNSRawVector3DExtrusionParams value, float fExtrusionHeightMaxAddition)
        {
            ctrlRawVector3DExtrusionParams1.SetExtrusionParams(value);
            mcTilingScheme = value.pTilingScheme;
            SetExtrusionHeightMaxAddition(fExtrusionHeightMaxAddition);

          //  SetLayerFileName()
        }

        public DNSRawVector3DExtrusionGraphicalParams GetRawVector3DExtrusionGraphicalParams()
        {
            mExtrusionGraphicalParams = ctrlRawVector3DExtrusionParams1.GetExtrusionGraphicalParams();
            return mExtrusionGraphicalParams;
        }

        public void SetRawVector3DExtrusionGraphicalParams(DNSRawVector3DExtrusionGraphicalParams value, float fExtrusionHeightMaxAddition)
        {
            ctrlRawVector3DExtrusionParams1.SetExtrusionGraphicalParams(value);
            SetExtrusionHeightMaxAddition(fExtrusionHeightMaxAddition);
        }

        public bool CheckRawVector3DExtrusionValidity()
        {
            return ctrlRawVector3DExtrusionParams1.CheckValidity();
        }

        public bool CheckRaw3DModelValidity()
        {
            return ctrlRaw3DModelParams1.CheckRaw3DModelValidity();
        }

        public DNEWebMapServiceType GetWMSTypeSelectedLayer()
        {
            return ctrlWebServiceLayerParams1.GetWMSTypeSelectedLayer(); 
        }

        public void SetWMSTypeSelectedLayer(DNEWebMapServiceType type)
        {
            ctrlWebServiceLayerParams1.SetType(type);
        }

        public void SetWCSParams(DNSWCSParams layerParams)
        {
            SetNonNativeParams(layerParams);
            ctrlWebServiceLayerParams1.SetParams(layerParams);
        }

        public void SetWMSParams(DNSWMSParams layerParams)
        {
            SetNonNativeParams(layerParams);
            ctrlWebServiceLayerParams1.SetParams(layerParams);
        }

        public void SetWMTSParams(DNSWMTSParams layerParams)
        {
            SetNonNativeParams(layerParams);
            ctrlWebServiceLayerParams1.SetParams(layerParams);
            
        }

        public void SetUsedServerTilingScheme(object pbUsedServerTilingScheme)
        {
            ctrlWebServiceLayerParams1.SetUsedServerTilingScheme(pbUsedServerTilingScheme);
        }

        public DNSWebMapServiceParams GetWebMapServiceParams(DNSWebMapServiceParams webMapServiceParams, DNEWebMapServiceType WMSType)
        {
            webMapServiceParams = (DNSWebMapServiceParams)GetNonNativeParams(webMapServiceParams);
            switch(WMSType)
            {
                case DNEWebMapServiceType._EWMS_WMS:
                    webMapServiceParams = ctrlWebServiceLayerParams1.GetWMSParams((DNSWMSParams)webMapServiceParams);
                    break;
                case DNEWebMapServiceType._EWMS_WMTS:
                    webMapServiceParams = ctrlWebServiceLayerParams1.GetWMTSParams((DNSWMTSParams)webMapServiceParams);
                    break;
                case DNEWebMapServiceType._EWMS_WCS:
                    webMapServiceParams = ctrlWebServiceLayerParams1.GetWCSParams((DNSWCSParams)webMapServiceParams);
                    break;
            }
            
            return webMapServiceParams;
        }

        private void tcParams_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 1 /*vector params*/ && cmbMapLayerType.SelectedIndex == 11 /*RawVector*/)
                return;
            if ((e.TabPageIndex == 0 /*no raw vector params*/ &&
                ((cmbMapLayerType.SelectedIndex == 9  /*RawDTM*/||
                cmbMapLayerType.SelectedIndex == 10  /*RawRaster*/||
                cmbMapLayerType.SelectedIndex == 14  /*RawMaterial*/ ||
                cmbMapLayerType.SelectedIndex == 15  /*RawTrav*/ ||
                cmbMapLayerType.SelectedIndex == 16  /*WebServiceRaster*/ ||
                cmbMapLayerType.SelectedIndex == 17  /*WebServiceDtm*/)) || 
                (m_IsFilterDTMMapLayers &&
                (cmbMapLayerType.SelectedIndex == 2  /*RawDTM*/ || 
                cmbMapLayerType.SelectedIndex == 3  /*WebServiceDtm*/))))
                return;
            if (e.TabPageIndex == 2 /*Vector3DExtrusion*/ &&
                (cmbMapLayerType.SelectedIndex == 3 /*NativeVector3DExtrusion*/ ||
                cmbMapLayerType.SelectedIndex == 12 /*RawVector3DExtrusion*/  ||
                cmbMapLayerType.SelectedIndex == 22 /*NativeServerVector3DExtrusion*/  ))
                return;
            if (e.TabPageIndex == 3 /*3DModel*/ && cmbMapLayerType.SelectedIndex == 13  /*Raw3DModel*/ )
                return;
            e.Cancel = true;
        }

        private void tcLayerParams_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 0 &&
            ((cmbMapLayerType.SelectedIndex == 9  /*RawDTM*/ ||
             cmbMapLayerType.SelectedIndex == 10  /*RawRaster*/ ||
             cmbMapLayerType.SelectedIndex == 14  /*RawMaterial*/ ||
             cmbMapLayerType.SelectedIndex == 15  /*RawTrav*/) || 
             (m_IsFilterDTMMapLayers && cmbMapLayerType.SelectedIndex == 1  /*RawDTM*/  )))
                return;
            if (e.TabPageIndex == 1 &&
               ((cmbMapLayerType.SelectedIndex == 16  /*WebServiceRaster*/ ||
                cmbMapLayerType.SelectedIndex == 17 /*WebServiceDtm*/)
                ||
                 (m_IsFilterDTMMapLayers && cmbMapLayerType.SelectedIndex == 2  /*WebServiceDtm*/  ))
                )
                return;
            e.Cancel = true;
        }

        private void btnTilingScheme1_Click(object sender, EventArgs e)
        {
            frmSTilingSchemeParams frmSTilingSchemeParams = new frmSTilingSchemeParams();
            if (mcTilingScheme != null)
                frmSTilingSchemeParams.STilingScheme = mcTilingScheme;
            frmSTilingSchemeParams.SetControlsReadonly(m_isReadOnly);

            if (frmSTilingSchemeParams.ShowDialog() == DialogResult.OK)
            {
                mcTilingScheme = frmSTilingSchemeParams.STilingScheme;
            }
        }

    }
}
