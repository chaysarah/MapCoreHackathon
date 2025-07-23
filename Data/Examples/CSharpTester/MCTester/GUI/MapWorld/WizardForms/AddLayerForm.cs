using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;
using MCTester.Managers.MapWorld;
using MapCore.Common;
using MCTester.MapWorld.Assist_Forms;
using MCTester.General_Forms;
using MCTester.MapWorld.WebMapLayers;
using MCTester.ButtonsImplementation;
using MCTester.GUI.Map;
using static MCTester.Managers.MapWorld.Manager_MCLayers;

namespace MCTester.MapWorld.WizardForms
{
    public partial class AddLayerForm : Form, IUserControlItem, IWebLayerRequest, IUserSelectServerLayers
    {
        private string m_LayerFileName;
        private IDNMcMapLayer m_NewLayer;
        private List<IDNMcMapLayer> m_NewLayers;
        private EUserAction m_UserAction = EUserAction.CreateNewLayer;

        private bool mWMTSOpenAllLayersAsOne;
        private bool mWMSOpenAllLayersAsOne;
        private DNSWMSParams m_SWMSParams = new DNSWMSParams();
        private DNSWMTSParams m_SWMTSParams = new DNSWMTSParams();
        private DNSWCSParams m_SWCSParams = new DNSWCSParams();
        private MCTRaw3DModelParams m_Raw3DModelParams = new MCTRaw3DModelParams();
        private bool mIsWCSRaster = true;
		private List<DNSMcKeyStringValue> m_RequestParams = null;

        private Dictionary<string, MCTServerSelectLayersData> mDicWMTSFromCSWLayersData = new Dictionary<string, MCTServerSelectLayersData>();
        private bool m_isAddLayerToDic = true;

        public EUserAction UserAction
        {
            get { return m_UserAction; }
            set { m_UserAction = value; }
        }

        private List<IDNMcMapLayer> lstExistLayers = new List<IDNMcMapLayer>();
        private List<IDNMcMapLayer> m_lstRawVectorLayers = new List<IDNMcMapLayer>();

        public enum EUserAction
        {
            CreateNewLayer,
            UseExistLayer,
            LoadFromFile,
            SelectFromServer
        }
        
        private FrmWebMapLayers mFrmWebMapLayers;
        private Dictionary<string, List<Manager_MCLayers.MCTWebServerUserSelection>> mDicServerSelectLayers = new Dictionary<string, List<Manager_MCLayers.MCTWebServerUserSelection>>();

        private bool mIsInSelectGroup = false;

        private Dictionary<string, DNEWebMapServiceType> mDicServerSelectLayersType = new Dictionary<string, DNEWebMapServiceType>();
        private bool m_isFilterDTMType = false;

        public AddLayerForm(bool isFilterDTMType = false)
        {
            InitializeComponent();
            Manager_MCLayers.AddLayerFormToList(this);
            m_isFilterDTMType = isFilterDTMType;
            m_isAddLayerToDic = !isFilterDTMType;

            if (m_isFilterDTMType)
                ucLayerParams1.SetFilterDTMLayer();

            m_SWMTSParams.bUseServerTilingScheme = true;

            SelectCurrentGroup(EUserAction.UseExistLayer);

            txtMapCoreServerPath.Text = Manager_MCLayers.UrlMapCoreServer;
            txtWMTSServerPath.Text = Manager_MCLayers.UrlWMTSServer;
            txtWCSServerPath.Text = Manager_MCLayers.UrlWCSServer;
            txtWMSServerPath.Text = Manager_MCLayers.UrlWMSServer;
            txtCSWServerPath.Text = Manager_MCLayers.UrlCSWServer;

            switch (Manager_MCLayers.mcLastWMSLayerType)
            {
                case DNEWebMapServiceType._EWMS_WMS:
                    ucLayerParams1.SetWMSParams(Manager_MCLayers.mcCurrSWMSParams);
                    break;
                case DNEWebMapServiceType._EWMS_WMTS:
                    ucLayerParams1.SetWMTSParams(Manager_MCLayers.mcCurrSWMTSParams);
                    break;
                case DNEWebMapServiceType._EWMS_WCS:
                    ucLayerParams1.SetWCSParams(Manager_MCLayers.mcCurrSWCSParams);
                    break;
            }

            ucLayerParams1.SetIsDoActionsIfFileSelectedOrChanged(true);

          /*  txtCSWServerPath.Text = "https://catalog.mapcolonies.net/api/raster/v1/csw";

            DNSMcKeyStringValue token = new DNSMcKeyStringValue();
            token.strKey = "token";
            token.strValue = "eyJhbGciOiJSUzI1NiIsImtpZCI6Im1hcC1jb2xvbmllcy1pbnQifQ.eyJkIjpbInJhc3RlciIsInZlY3RvciIsIjNkIiwiZGVtIl0sImlhdCI6MTcxMzI2NzAxMCwic3ViIjoibWFwY29yZSIsImlzcyI6Im1hcGNvbG9uaWVzLXRva2VuLWNsaSJ9.gA01QzFd1a3hTlSBTq8hkHUnCuCdNR6v8XDcfIdVj-btWvQ5zzSHmXfKVKGvumoL2rdR7SSMvfi5IdWwkplimySXger4B5XSF78_a8RWZDwX97Lnbg_bqY8UxBD46Rgq5BkhGr8P0H8b04FMHDkdKwb5fpX5unD8iVNeEpRs5LYHJgAw8R_IYinNBJTXEbrnv_dKqX5mlSAlehWeIzROgdnPLgjD8EyXDDz3hvvEOSxRk9CYruG06MnqHw1rjl9Jmjut0BADMBNgtoPYzdcud72IVdW5CkQEjFJITDHCv-4MD08pIH5HZ34TCdjtbMLeoIi0p1J_DegaExt3sc0qcQ";

            DNSMcKeyStringValue ver = new DNSMcKeyStringValue();
            ver.strKey = "ver";
            ver.strValue = "1";

            DNSMcKeyStringValue cswBody = new DNSMcKeyStringValue();
            cswBody.strKey = MCTMapForm.CSWBodyKey;
            cswBody.strValue = "<csw:GetRecords\r\nmaxRecords=\"10\"\r\noutputFormat=\"application/xml\"\r\noutputSchema=\"http://schema.mapcolonies.com/raster\"\r\nservice=\"CSW\"\r\nversion=\"2.0.2\"\r\nstartPosition=\"1\"\r\nxmlns:mc=\"http://schema.mapcolonies.com/raster\"\r\nxmlns:csw=\"http://www.opengis.net/cat/csw/2.0.2\"\r\nxmlns:ogc=\"http://www.opengis.net/ogc\">\r\n\t<csw:Query typeNames=\"mc:MCRasterRecord\">\r\n\t\t<csw:ElementSetName>full</csw:ElementSetName>\r\n\t\t<csw:Constraint version=\"1.1.0\">\r\n\t\t\t<ogc:Filter xmlns:ogc=\"http://www.opengis.net/ogc\">\r\n\t\t\t\t<ogc:PropertyIsEqualTo>\r\n\t\t\t\t\t<ogc:PropertyName>mc:productType</ogc:PropertyName> <ogc:Literal>OrthophotoBest</ogc:Literal>\r\n\t\t\t\t</ogc:PropertyIsEqualTo>\r\n\t\t\t</ogc:Filter>\r\n\t\t</csw:Constraint>\r\n\t</csw:Query>\r\n</csw:GetRecords>";

            m_RequestParams = new List<DNSMcKeyStringValue> { token, ver, cswBody };*/
        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {

        }

        #endregion

        public void NoticeLayerRemoved(IDNMcMapLayer layerToRemove)
        {
            if (m_NewLayers != null && m_NewLayers.Count > 0 && m_NewLayers.Contains(layerToRemove))
                m_NewLayers.Remove(layerToRemove);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Manager_MCLayers.ResetServerLayersPriorityDic();

            DNSLocalCacheLayerParams? localCacheLayerParams = ucLayerParams1.GetLocalCacheLayerParams();
            m_NewLayers = new List<IDNMcMapLayer>();
           
            if (m_radCreateNew.Checked)
            {
                MCTMapLayerReadCallback.IsSaveReplacedOrRemovedLayer = true;
                m_LayerFileName = ucLayerParams1.GetLayerFileName();
                IDNMcMapLayer layer = null;
                List<string> subdirs = new List<string>();
                string strLayerType = ucLayerParams1.GetMapLayerType();
                if(strLayerType.StartsWith("NATIVE") && !strLayerType.StartsWith("NATIVE_SERVER"))
                    subdirs = Manager_MCLayers.GetRecursiveFolders(m_LayerFileName);
                for (int i = 0; i < ucLayerParams1.GetNumOfLayers(); i++)
                {
                    m_lstRawVectorLayers.Clear();
                    switch (strLayerType)
                    {
                        case "NATIVE_DTM":
                            NewLayer = Manager_MCLayers.CreateNativeDTMLayer(LayerFileName,ucLayerParams1.GetNumLevelsToIgnore(), localCacheLayerParams, m_isAddLayerToDic);
                            break;
                        case "NATIVE_SERVER_DTM":
                            NewLayer = Manager_MCLayers.CreateNativeServerDTMLayer(LayerFileName, m_isAddLayerToDic);
                            break;
                        case "NATIVE_MATERIAL":
                            foreach (string dir in subdirs)
                            {
                                layer = Manager_MCLayers.CreateNativeMaterialLayer(dir,ucLayerParams1.GetThereAreMissingFiles(), localCacheLayerParams);
                                if (layer != null)
                                    m_NewLayers.Add(layer);
                            }
                            break;
                        case "NATIVE_TRAVERSABILITY":
                            foreach (string dir in subdirs)
                            {
                                layer = Manager_MCLayers.CreateNativeTraversabilityLayer(dir, ucLayerParams1.GetThereAreMissingFiles(), localCacheLayerParams);
                                if (layer != null)
                                    m_NewLayers.Add(layer);
                            }
                            break;
                        case "NATIVE_HEAT_MAP":
                            foreach (string dir in subdirs)
                            {
                                layer = Manager_MCLayers.CreateNativeHeatMapLayer(dir,
                                ucLayerParams1.GetFirstLowerQualityLevel(),
                                ucLayerParams1.GetThereAreMissingFiles(),
                                ucLayerParams1.GetNumLevelsToIgnore(),
                                ucLayerParams1.GetEnhanceBorderOverlap2(),
                                localCacheLayerParams);
                                if(layer != null)
                                    m_NewLayers.Add(layer);
                            }
                            break;
                        case "NATIVE_RASTER":
                            foreach (string dir in subdirs)
                            {
                                layer = Manager_MCLayers.CreateNativeRasterLayer(dir,
                                ucLayerParams1.GetFirstLowerQualityLevel(),
                                ucLayerParams1.GetThereAreMissingFiles(),
                                ucLayerParams1.GetNumLevelsToIgnore(),
                                ucLayerParams1.GetEnhanceBorderOverlap2(),
                                localCacheLayerParams);
                                if (layer != null)
                                    m_NewLayers.Add(layer);
                            }
                            break;
                        case "NATIVE_SERVER_RASTER":
                            NewLayer = Manager_MCLayers.CreateNativeServerRasterLayer(LayerFileName);
                            break;
                        case "NATIVE_VECTOR":
                            foreach (string dir in subdirs)
                            {
                                layer = Manager_MCLayers.CreateNativeVectorLayer(dir, localCacheLayerParams);
                                if (layer != null)
                                    m_NewLayers.Add(layer);
                            }
                            break;
                        case "NATIVE_VECTOR_3D_EXTRUSION":
                            foreach (string dir in subdirs)
                            {
                                layer = Manager_MCLayers.CreateNativeVector3DExtrusionMapLayer(dir,
                                ucLayerParams1.GetNumLevelsToIgnore(),
                                ucLayerParams1.GetExtrusionHeightMaxAddition());
                                if (layer != null)
                                    m_NewLayers.Add(layer);
                            }
                            break;
                        case "NATIVE_3D_MODEL":
                            foreach (string dir in subdirs)
                            {
                                layer = Manager_MCLayers.CreateNative3DModelMapLayer(dir, ucLayerParams1.GetNumLevelsToIgnore());
                                if (layer != null)
                                    m_NewLayers.Add(layer);
                            }
                            break;
                        case "NATIVE_SERVER_VECTOR":
                            NewLayer = Manager_MCLayers.CreateNativeServerVectorLayer(LayerFileName);
                            break;
                        case "RAW_DTM":
                        case "RAW_RASTER":
                        case "RAW_TRAVERSABILITY":
                        case "RAW_MATERIAL":
                            /*if (!ucLayerParams1.IsUserSelectGridCoordinateSystem())
                            {
                                MessageBox.Show("No Grid Coordinate System was specified.\nYou have to choose one!\n");
                                return;
                            }*/
                            DNSRawParams dnParams = ucLayerParams1.GetSRawParams();
                            if (dnParams == null)
                            {
                                MessageBox.Show("Invalid Pyramid Resolution Values");
                                return;
                            }
                            
                            if (ucLayerParams1.GetMapLayerType() == "RAW_DTM")
                                NewLayer = Manager_MCLayers.CreateRawDTMLayer(
                                                                dnParams,
                                                                localCacheLayerParams, m_isAddLayerToDic);
                            else if (ucLayerParams1.GetMapLayerType() == "RAW_RASTER")
                                NewLayer = Manager_MCLayers.CreateRawRasterLayer(
                                                                dnParams,
                                                                ucLayerParams1.GetImageCoordSys(),
                                                                localCacheLayerParams);
                            else if (ucLayerParams1.GetMapLayerType() == "RAW_TRAVERSABILITY")
                                NewLayer = Manager_MCLayers.CreateRawTraversabilityLayer(
                                                                dnParams,
                                                                localCacheLayerParams);
                            else if (ucLayerParams1.GetMapLayerType() == "RAW_MATERIAL")
                                NewLayer = Manager_MCLayers.CreateRawMaterialLayer(
                                                                dnParams,
                                                                localCacheLayerParams);
                            break;
                        case "RAW_VECTOR":
                            DNSRawVectorParams rawVectorParams = ucLayerParams1.GetRawVectorParams();
                            if (rawVectorParams == null)
                                return;

                            string StylingName = rawVectorParams.StylingParams.strStylingFile;

                            List<string> layerNames = Manager_MCLayers.CheckMultiRawVectorLayer(LayerFileName);
                            bool existStyling = LayerFileName.ToLower().Contains(".sld") || LayerFileName.ToLower().Contains(".lyrx");
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

                                    IDNMcMapLayer vectorMapLayer = Manager_MCLayers.CreateRawVectorLayer(
                                                       rawVectorParams,
                                                       ucLayerParams1.GetTargetGridCoordinateSystem(),
                                                        ucLayerParams1.GetTilingScheme(),
                                                        localCacheLayerParams);
                                    if (vectorMapLayer != null)
                                        m_lstRawVectorLayers.Add(vectorMapLayer);
                                }
                            }
                            m_NewLayers.AddRange(m_lstRawVectorLayers);
                            break;
                        case "NATIVE_SERVER_VECTOR_3D_EXTRUSION":
                            NewLayer = Manager_MCLayers.CreateNativeServerVector3DExtrusionLayer(LayerFileName);
                            break;
                        case "NATIVE_SERVER_3D_MODEL":
                            NewLayer = Manager_MCLayers.CreateNativeServer3DModelLayer(LayerFileName);
                            break;
                        case "NATIVE_SERVER_TRAVERSABILITY":
                            NewLayer = Manager_MCLayers.CreateNativeServerTraversabilityLayer(LayerFileName);
                            break;
                        case "NATIVE_SERVER_MATERIAL":
                            NewLayer = Manager_MCLayers.CreateNativeServerMaterialLayer(LayerFileName);
                            break;
                        case "RAW_VECTOR_3D_EXTRUSION":

                            List<string> layerNames2 = Manager_MCLayers.CheckMultiRawVectorLayer(LayerFileName);
                            foreach (string dataLayer in layerNames2)
                            {
                                IDNMcMapLayer vectorMapLayer = CreateRawVector3DExtrusion(dataLayer);
                                if (vectorMapLayer != null)
                                    m_lstRawVectorLayers.Add(vectorMapLayer);
                            }
                            m_NewLayers.AddRange(m_lstRawVectorLayers);

                            break;
                        case "RAW_3D_MODEL":
                            if (!ucLayerParams1.CheckRaw3DModelValidity())
                            {
                                Enabled = true;
                                return;
                            }
                            MCTRaw3DModelParams raw3DModelParams = ucLayerParams1.GetRaw3DModelParams();
                            if (raw3DModelParams.IsUseIndexing)
                            {
                                NewLayer = Manager_MCLayers.CreateRaw3DModelLayer(LayerFileName,
                                    raw3DModelParams.OrthometricHeights,
                                    ucLayerParams1.GetNumLevelsToIgnore(),
                                    raw3DModelParams.NonDefaultIndexingDataDirectory);

                            }
                            else
                            {
                                NewLayer = Manager_MCLayers.CreateRaw3DModelLayer(LayerFileName,
                                        raw3DModelParams.pTargetCoordinateSystem,
                                        raw3DModelParams.OrthometricHeights,
                                        raw3DModelParams.pClipRect,
                                        raw3DModelParams.fTargetHighestResolution,
                                        raw3DModelParams.aRequestParams,
                                        raw3DModelParams.PositionOffset);
                            }
                            break;
                        case "WEB_SERVICE_RASTER":
                        case "WEB_SERVICE_DTM":
                            
                            DNEWebMapServiceType wMSTypeSelectedLayer = ucLayerParams1.GetWMSTypeSelectedLayer();
                            switch (wMSTypeSelectedLayer)
                            {
                                case DNEWebMapServiceType._EWMS_WMS:
                                    {
                                        //DNSWMSParams layerParams = Manager_MCLayers.mcCurrSWMSParams;
                                        Manager_MCLayers.mcLastWMSLayerType = DNEWebMapServiceType._EWMS_WMS;
                                        Manager_MCLayers.mcCurrSWMSParams = (DNSWMSParams)ucLayerParams1.GetWebMapServiceParams(Manager_MCLayers.mcCurrSWMSParams, Manager_MCLayers.mcLastWMSLayerType);

                                        if (ucLayerParams1.GetMapLayerType() == "WEB_SERVICE_RASTER")
                                            NewLayer = Manager_MCLayers.CreateWebServiceRasterLayer(Manager_MCLayers.mcCurrSWMSParams, localCacheLayerParams);
                                        else
                                            NewLayer = Manager_MCLayers.CreateWebServiceDtmLayer(Manager_MCLayers.mcCurrSWMSParams, localCacheLayerParams, m_isAddLayerToDic);
                                    }
                                    break;

                                case DNEWebMapServiceType._EWMS_WMTS:
                                    {
                                        Manager_MCLayers.mcLastWMSLayerType = DNEWebMapServiceType._EWMS_WMTS;
                                        Manager_MCLayers.mcCurrSWMTSParams = (DNSWMTSParams)ucLayerParams1.GetWebMapServiceParams(Manager_MCLayers.mcCurrSWMTSParams, Manager_MCLayers.mcLastWMSLayerType);

                                        if (ucLayerParams1.GetMapLayerType() == "WEB_SERVICE_RASTER")
                                            NewLayer = Manager_MCLayers.CreateWebServiceRasterLayer(Manager_MCLayers.mcCurrSWMTSParams, localCacheLayerParams);
                                        else
                                            NewLayer = Manager_MCLayers.CreateWebServiceDtmLayer(Manager_MCLayers.mcCurrSWMTSParams, localCacheLayerParams, m_isAddLayerToDic);
                                    }
                                    break;
                                case DNEWebMapServiceType._EWMS_WCS:
                                    {
                                        Manager_MCLayers.mcLastWMSLayerType = DNEWebMapServiceType._EWMS_WCS;
                                        Manager_MCLayers.mcCurrSWCSParams = (DNSWCSParams)ucLayerParams1.GetWebMapServiceParams(Manager_MCLayers.mcCurrSWCSParams, Manager_MCLayers.mcLastWMSLayerType);

                                        if (ucLayerParams1.GetMapLayerType() == "WEB_SERVICE_RASTER")
                                            NewLayer = Manager_MCLayers.CreateWebServiceRasterLayer(Manager_MCLayers.mcCurrSWCSParams, localCacheLayerParams);
                                        else
                                            NewLayer = Manager_MCLayers.CreateWebServiceDtmLayer(Manager_MCLayers.mcCurrSWCSParams, localCacheLayerParams, m_isAddLayerToDic);
                                    }
                                    break;
                            }
                            break;
                    }
                    if (NewLayer == null && m_NewLayers.Count == 1)
                    {
                        NewLayer = m_NewLayers[0];
                    }
                    if (NewLayer != null && m_lstRawVectorLayers.Count == 0 && !m_NewLayers.Contains(NewLayer))
                    {
                        m_NewLayers.Add(NewLayer);
                    }
                    if (NewLayer == null && m_NewLayers.Count == 0 && m_lstRawVectorLayers.Count == 0)
                    {
                        //MessageBox.Show("Failed to create new layer", "create new layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (m_NewLayers.Count > 0)
                {
                    CloseForm();
                }
            }
            else if (m_radSelectFromServer.Checked)
            {
                m_UserAction = EUserAction.SelectFromServer;

                bool isEnabled = false;
                m_NewLayers.AddRange(Manager_MCLayers.CheckAndCreateWebLayersData(mDicServerSelectLayers, mDicServerSelectLayersType, mWMTSOpenAllLayersAsOne, mWMSOpenAllLayersAsOne, m_SWMTSParams, m_SWMSParams, m_SWCSParams, m_Raw3DModelParams, mIsWCSRaster, m_isFilterDTMType, m_isAddLayerToDic, out isEnabled , mDicWMTSFromCSWLayersData));
                if (isEnabled)
                    this.Enabled = isEnabled;
                else
                    CloseForm();

            }
            else if (m_radUseExisting.Checked)
            {
                if (lstLayers.SelectedItem != null)
                {
                    m_UserAction = EUserAction.UseExistLayer;
                    NewLayer = lstExistLayers[lstLayers.SelectedIndex];

                    m_NewLayers.Add(NewLayer);
                    CloseForm();

                }
                else
                    MessageBox.Show("You have to choose a layer", "Use existing layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (m_radLoadFromFile.Checked)
            {
                try
                {
                    UserDataFactory UDF = new UserDataFactory();
                    m_UserAction = EUserAction.LoadFromFile;
                    NewLayer = DNMcMapLayer.Load(txtFileName.Text,
                                                    BaseDirectory,
                                                    UDF, 
                                                    new MCTMapLayerReadCallbackFactory());

                    m_NewLayers.Add(NewLayer);
                    Manager_MCLayers.CheckingAfterCreateLayer(NewLayer, true);

                    CloseForm();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNMcMapLayer.Load", McEx);
                }
            }
        }

        private IDNMcMapLayer CreateRawVector3DExtrusion(string filename)
        {
            if (ucLayerParams1.GetIsUseIndexing())
            {
                if (!ucLayerParams1.CheckRawVector3DExtrusionValidity())
                {
                    Enabled = true;
                    return null;
                }
                return  Manager_MCLayers.CreateRawVector3DExtrusionLayer(filename,
                                                                        ucLayerParams1.GetRawVector3DExtrusionGraphicalParams(),
                                                                        ucLayerParams1.GetExtrusionHeightMaxAddition(),
                                                                        ucLayerParams1.GetExtrusionIndexingDataDirectory());
            }
            else
            {
                DNSRawVector3DExtrusionParams rawVector3DExtrusionParams = ucLayerParams1.GetRawVector3DExtrusionParams();
                rawVector3DExtrusionParams.strDataSource = filename;
                return Manager_MCLayers.CreateRawVector3DExtrusionLayer(rawVector3DExtrusionParams,
                                                                        ucLayerParams1.GetExtrusionHeightMaxAddition());
            }
        }
        private void CloseForm()
        {
            MCTMapLayerReadCallback.IsSaveReplacedOrRemovedLayer = false;
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void EnabledControls(bool isEnabled)
        {
            foreach (Control cntrl in this.Controls)
                cntrl.Enabled = isEnabled;
        }

        public string LayerFileName
        {
            get { return m_LayerFileName; }
        }

        public IDNMcMapLayer NewLayer
        {
            get { return m_NewLayer; }
            set { m_NewLayer = value; }
        }

        public List<IDNMcMapLayer> NewLayers
        {
            get { return m_NewLayers; }
            set { m_NewLayers = value; }
        }

        public string LayerType
        {
            get
            {
                return ucLayerParams1.GetMapLayerType();
            }
        }

       

        public string BaseDirectory
        {
            get
            {
                if (txtBaseDirectory.Text == "")
                    return null;
                else
                    return txtBaseDirectory.Text;
            }
        }

        private void AddLayerForm_Load(object sender, EventArgs e)
        {
            lstLayers.Items.Clear();

            Dictionary<object, uint> LayerDic = Manager_MCLayers.AllParams;
            List<IDNMcMapTerrain> terrainList = Manager_MCTerrain.AllTerrains();
            foreach (IDNMcMapTerrain terr in terrainList)
            {
                IDNMcMapLayer[] layersInTerr = terr.GetLayers();
                foreach (IDNMcMapLayer layer in layersInTerr)
                {
                    if (!lstExistLayers.Contains(layer) && (!m_isFilterDTMType || layer.LayerType.ToString().ToLower().Contains("dtm")))
                    {
                        lstLayers.Items.Add(Manager_MCNames.GetNameByObject(layer));
                        lstExistLayers.Add(layer);
                    }
                }
            }

            foreach (IDNMcMapLayer layer in LayerDic.Keys)
            {
                if (!m_isFilterDTMType || layer.LayerType.ToString().ToLower().Contains("dtm"))
                {
                    lstLayers.Items.Add(Manager_MCNames.GetNameByObject(layer));
                    lstExistLayers.Add(layer);
                }
            }

            foreach (IDNMcMapViewport vp in Manager_MCViewports.AllParams.Keys)
            {
                IDNMcDtmMapLayer[] querySecondaryDtmLayers = vp.GetQuerySecondaryDtmLayers();
                foreach (IDNMcMapLayer layer in querySecondaryDtmLayers)
                {
                    if (!lstExistLayers.Contains(layer))
                    {
                        lstLayers.Items.Add(Manager_MCNames.GetNameByObject(layer));
                        lstExistLayers.Add(layer);
                    }
                }
            }
        }

        private void btnbtnLoadFileName_Click(object sender, EventArgs e)
        {
            txtFileName.Text = OpenFileDlg();
        }

        private void btnLoadBaseDir_Click(object sender, EventArgs e)
        {
            txtBaseDirectory.Text = OpenFileDlg();
        }

        private string OpenFileDlg()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.RestoreDirectory = true;
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                return OFD.FileName;
            }
            else
                return "";
        }

        private void m_radCreateNew_CheckedChanged(object sender, EventArgs e)
        {
            if(m_radCreateNew.Checked)
                SelectCurrentGroup(EUserAction.CreateNewLayer);
        }

        private void m_radUseExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (m_radUseExisting.Checked)
                SelectCurrentGroup(EUserAction.UseExistLayer);
        }

        private void m_radSelectFromServer_CheckedChanged(object sender, EventArgs e)
        {
            if (m_radSelectFromServer.Checked)
                SelectCurrentGroup(EUserAction.SelectFromServer);
        }

        private void m_radLoadFromFile_CheckedChanged(object sender, EventArgs e)
        {
            if (m_radLoadFromFile.Checked)
                SelectCurrentGroup(EUserAction.LoadFromFile);
        }

        private void SelectCurrentGroup(EUserAction eUserAction)
        {
            if (!mIsInSelectGroup)
            {
                mIsInSelectGroup = true;
                gbCreateNewLayer.Enabled = m_radCreateNew.Checked;
                gbLoadFromFile.Enabled = m_radLoadFromFile.Checked;
                gbUseExisting.Enabled = m_radUseExisting.Checked;
                gbSelectFromServer.Enabled = m_radSelectFromServer.Checked;

                if (eUserAction != EUserAction.CreateNewLayer)
                    m_radCreateNew.Checked = false;
                if (eUserAction != EUserAction.LoadFromFile)
                    m_radLoadFromFile.Checked = false;
                if (eUserAction != EUserAction.UseExistLayer)
                    m_radUseExisting.Checked = false;
                if (eUserAction != EUserAction.SelectFromServer)
                    m_radSelectFromServer.Checked = false;

                mIsInSelectGroup = false;
            }
        }

    

        private DNSWebMapServiceParams GetWebMapServiceParams(DNEWebMapServiceType eWebMapServiceType)
        {
            SetRequestParams();
            DNSWebMapServiceParams webMapServiceParams = m_SWMTSParams;
            if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WCS)
                webMapServiceParams = m_SWCSParams;
            if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMS)
                webMapServiceParams = m_SWMSParams;
            return webMapServiceParams;
        }
        public void GetWebServerLayers(DNEMcErrorCode eStatus, string strServerURL, DNEWebMapServiceType eWebMapServiceType, DNSServerLayerInfo[] aLayers, string[] astrServiceMetadataURLs, string urlServer, string strServiceProviderName)
        {
            List<Manager_MCLayers.MCTWebServerUserSelection> mServerSelectLayers = null;
            if (mDicServerSelectLayers.ContainsKey(urlServer))
                mServerSelectLayers = mDicServerSelectLayers[urlServer];

            if (eStatus == DNEMcErrorCode.SUCCESS)
            {
                bool isOpenAsOneLayer = false;
                if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS)
                    isOpenAsOneLayer = mWMTSOpenAllLayersAsOne;
                 else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMS)
                     isOpenAsOneLayer = mWMSOpenAllLayersAsOne;

                Manager_MCLayers.mcCurrSWebMapServiceParams.aRequestParams = m_RequestParams == null ? null : m_RequestParams.ToArray();
                Manager_MCLayers.mcCurrSWMTSParams.aRequestParams = m_RequestParams == null ? null : m_RequestParams.ToArray();
                Manager_MCLayers.mcCurrSWCSParams.aRequestParams = m_RequestParams == null ? null : m_RequestParams.ToArray();

                mFrmWebMapLayers = new FrmWebMapLayers(mServerSelectLayers, eWebMapServiceType, this);
                mFrmWebMapLayers.SetWebServerLayers(eStatus, strServerURL, /*eWebMapServiceType,*/ GetWebMapServiceParams(eWebMapServiceType), aLayers, astrServiceMetadataURLs, urlServer, strServiceProviderName, isOpenAsOneLayer, mIsWCSRaster, true, m_Raw3DModelParams, mDicWMTSFromCSWLayersData);

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
                    if (mServerSelectLayers.Count > 0)
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
            Manager_MCLayers.OpenServerLayers_Click(txtMapCoreServerPath, DNEWebMapServiceType._EWMS_MAPCORE, this, m_RequestParams);
        }

        private void btnWMTSOpenServerLayers_Click(object sender, EventArgs e)
        {
            Manager_MCLayers.OpenServerLayers_Click(txtWMTSServerPath, DNEWebMapServiceType._EWMS_WMTS, this, m_RequestParams);
        }

        private void btnWCSOpenServerLayers_Click(object sender, EventArgs e)
        {
            Manager_MCLayers.OpenServerLayers_Click(txtWCSServerPath, DNEWebMapServiceType._EWMS_WCS, this, m_RequestParams);
        }

        private void btnWMSOpenServerLayers_Click(object sender, EventArgs e)
        {
            Manager_MCLayers.OpenServerLayers_Click(txtWMSServerPath, DNEWebMapServiceType._EWMS_WMS, this, m_RequestParams);
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

        private void btnCSWOpenServerLayers_Click(object sender, EventArgs e)
        {
            List<DNSMcKeyStringValue> cswRequestParams = MCTMapForm.GetRequestParamsWithCSWBody(m_RequestParams);

            Manager_MCLayers.OpenServerLayers_Click(txtCSWServerPath, DNEWebMapServiceType._EWMS_CSW, this, cswRequestParams);
        }

        private void AddLayerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Manager_MCLayers.RemoveLayerFormFromList(this);
        }
    }
}