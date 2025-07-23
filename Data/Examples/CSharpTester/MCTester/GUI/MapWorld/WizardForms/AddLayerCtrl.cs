using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCTester.MapWorld.MagicForm;
using MapCore;
using MCTester.Controls;
using UnmanagedWrapper;
using MapCore.Common;
using MCTester.Managers.MapWorld;
using MCTester.MapWorld.WebMapLayers;
using MCTester.General_Forms;
using MCTester.ButtonsImplementation;
using MCTester.Managers;

namespace MCTester.MapWorld.WizardForms
{
    public partial class AddLayerCtrl : UserControl, IWebLayerRequest
    {
        private int m_SelectedRow = -1;
        private bool m_isFinishLoad = false;

        // private string m_LayerFileName;
        // private IDNMcMapLayer m_NewLayer;
        private List<IDNMcMapLayer> m_NewLayers;
        private EUserAction m_UserAction = EUserAction.CreateNewLayer;

        private bool mWMTSOpenAllLayersAsOne;
        private bool mWMSOpenAllLayersAsOne;
        private DNSWMSParams m_SWMSParams = new DNSWMSParams();
        private DNSWMTSParams m_SWMTSParams = new DNSWMTSParams();
        private DNSWCSParams m_SWCSParams = new DNSWCSParams();
        private bool mIsWCSRaster = true;
        private List<DNSMcKeyStringValue> m_RequestParams = null;


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
        private Dictionary<string, DNEWebMapServiceType> mDicServerSelectLayersType = new Dictionary<string, DNEWebMapServiceType>();

        public AddLayerCtrl()
        {
            InitializeComponent();

            txtMapCoreServerPath.Text = Manager_MCLayers.UrlMapCoreServer;
            txtWMTSServerPath.Text = Manager_MCLayers.UrlWMTSServer;
            txtWCSServerPath.Text = Manager_MCLayers.UrlWCSServer;
            txtWMSServerPath.Text = Manager_MCLayers.UrlWMSServer;

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

            m_isFinishLoad = true;

            HideRadioButtons();  // this form can create layers from all panels
            ucLayerParams1.Enabled = false;

        }

        public void HideRadioButtons()
        {
            m_radLoadFromFile.Visible = m_radSelectFromServer.Visible = m_radCreateNew.Visible = m_radUseExisting.Visible = false;
        }

        public void NoticeLayerRemoved(IDNMcMapLayer layerToRemove)
        {
            if (m_NewLayers != null && m_NewLayers.Count > 0 && m_NewLayers.Contains(layerToRemove))
                m_NewLayers.Remove(layerToRemove);
        }

        private void btnSaveSelected_Click(object sender, EventArgs e)
        {
            SaveSelectedRow();
            // SetLayerParamsEnabled(false);
            if(dgvLayers[1,dgvLayers.RowCount-1].Value != null)
                btnAddNewRow.Enabled = true;
        }

        private void btnAddNewRow_Click(object sender, EventArgs e)
        {
           // SaveSelectedRow();
            dgvLayers.Rows.Insert(dgvLayers.RowCount, 1);
            dgvLayers.CurrentCell = dgvLayers[0, dgvLayers.RowCount - 1];
            dgvLayers.Rows[dgvLayers.RowCount - 1].Selected = true;
            ucLayerParams1.SetDefaultValues();
            SetLayerParamsEnabled(true);
        }

        private void SaveSelectedRow()
        {
            if (m_SelectedRow >= 0 && dgvLayers.RowCount > m_SelectedRow)
                dgvLayers.Rows[m_SelectedRow].Tag = SaveCurrRow();
        }

        private void SetLayerParamsEnabled(bool isEnabled)
        {
            btnSaveSelected.Enabled = isEnabled;
            ucLayerParams1.Enabled = isEnabled;
            btnAddNewRow.Enabled = !isEnabled;
        }

        private void dgvLayers_SelectionChanged(object sender, EventArgs e)
        {
            if (m_isFinishLoad)
            {
               // SaveSelectedRow();
                if (dgvLayers.SelectedRows.Count > 0)
                {
                    m_SelectedRow = dgvLayers.SelectedRows[0].Index;
                    if (dgvLayers.Rows[m_SelectedRow].Tag != null)
                        LoadCurrRow(dgvLayers.Rows[m_SelectedRow].Tag);
                    else
                        ucLayerParams1.SetDefaultValues();
                    //SetLayerParamsEnabled(true);
                }
            }
        }

        private MCTSLayerData SetParams(MCTSLayerData layerData)
        {
            layerData.LayerPath = ucLayerParams1.GetLayerFileName();
            layerData.NoOfLayers = ucLayerParams1.GetNumOfLayers();
            layerData.LayerSubFolderLocalCache = ucLayerParams1.GetLocalCacheLayerParams();
            layerData.LayerType = ucLayerParams1.GetMapLayerTypeAsEnum();
            return layerData;
        }

        private MCTSLayerData SaveCurrRow()
        {
            if (m_isFinishLoad)
            {
                MCTSLayerData layerData300 = new MCTSLayerData();

                DNELayerType eLayerType = ucLayerParams1.GetMapLayerTypeAsEnum();
                MCTSLayerData layerDataDefault = new MCTSLayerData();
                layerDataDefault = SetParams(layerDataDefault);

                try
                {
                    dgvLayers.Rows[m_SelectedRow].Cells[1].Value = ucLayerParams1.GetMapLayerType() + " , " + ucLayerParams1.GetLayerFileName();
                }
                catch (Exception) { }

                switch (eLayerType)
                {
                    case DNELayerType._ELT_NATIVE_SERVER_3D_MODEL:
                    case DNELayerType._ELT_NATIVE_SERVER_DTM:
                    case DNELayerType._ELT_NATIVE_SERVER_MATERIAL:
                    case DNELayerType._ELT_NATIVE_SERVER_RASTER:
                    case DNELayerType._ELT_NATIVE_SERVER_TRAVERSABILITY:
                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR:
                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR_3D_EXTRUSION:
                    case DNELayerType._ELT_NATIVE_VECTOR:
                        {
                            layerData300 = layerDataDefault;
                            return layerDataDefault;
                        }
                    case DNELayerType._ELT_NATIVE_HEAT_MAP:
                    case DNELayerType._ELT_NATIVE_RASTER:
                        MCTSLayerDataRaster layerData3 = new MCTSLayerDataRaster();
                        layerData3 = (MCTSLayerDataRaster)SetParams(layerData3);
                        layerData3.NumLevelsToIgnore = ucLayerParams1.GetNumLevelsToIgnore();
                        layerData3.ThereAreMissingFiles = ucLayerParams1.GetThereAreMissingFiles();
                        layerData3.FirstLowerQualityLevel = ucLayerParams1.GetFirstLowerQualityLevel();
                        layerData3.EnhanceBorderOverlap = ucLayerParams1.GetEnhanceBorderOverlap2();
                        return layerData3;

                    case DNELayerType._ELT_NATIVE_3D_MODEL:
                    case DNELayerType._ELT_NATIVE_DTM:
                        MCTSLayerDataDTM layerData = new MCTSLayerDataDTM();
                        layerData = (MCTSLayerDataDTM)SetParams(layerData);
                        layerData.NumLevelsToIgnore = ucLayerParams1.GetNumLevelsToIgnore();
                        return layerData;

                    case DNELayerType._ELT_NATIVE_VECTOR_3D_EXTRUSION:
                        MCTSLayerDataVector3DExtrusion layerData2 = new MCTSLayerDataVector3DExtrusion();
                        layerData2 = (MCTSLayerDataVector3DExtrusion)SetParams(layerData2);
                        layerData2.NumLevelsToIgnore = ucLayerParams1.GetNumLevelsToIgnore();
                        layerData2.ExtrusionHeightMaxAddition = ucLayerParams1.GetExtrusionHeightMaxAddition();
                        return layerData2;

                    case DNELayerType._ELT_NATIVE_MATERIAL:
                    case DNELayerType._ELT_NATIVE_TRAVERSABILITY:
                        MCTSLayerDataTraversability layerData4 = new MCTSLayerDataTraversability();
                        layerData4 = (MCTSLayerDataTraversability)SetParams(layerData4);
                        layerData4.ThereAreMissingFiles = ucLayerParams1.GetThereAreMissingFiles();
                        return layerData4;

                    case DNELayerType._ELT_RAW_DTM:
                        MCTSLayerDataRawDTM layerData5 = new MCTSLayerDataRawDTM();
                        layerData5 = (MCTSLayerDataRawDTM)SetParams(layerData5);
                        layerData5.Params = ucLayerParams1.GetSRawParams();
                        return layerData5;

                    case DNELayerType._ELT_RAW_RASTER:
                        MCTSLayerDataRawRaster layerData6 = new MCTSLayerDataRawRaster();
                        layerData6 = (MCTSLayerDataRawRaster)SetParams(layerData6);
                        layerData6.Params = ucLayerParams1.GetSRawParams();
                        layerData6.ImageCoordSys = ucLayerParams1.GetImageCoordSys();
                        return layerData6;

                    case DNELayerType._ELT_RAW_VECTOR:
                        MCTSLayerDataRawVector layerData7 = new MCTSLayerDataRawVector();
                        layerData7 = (MCTSLayerDataRawVector)SetParams(layerData7);
                        layerData7.Params = ucLayerParams1.GetRawVectorParams();
                        layerData7.TilingScheme = ucLayerParams1.GetTilingScheme();
                        layerData7.GridCoordSys = ucLayerParams1.GetTargetGridCoordinateSystem();

                        return layerData7;
                    case DNELayerType._ELT_RAW_VECTOR_3D_EXTRUSION:
                        if (ucLayerParams1.GetIsUseIndexing())
                        {
                            if (!ucLayerParams1.CheckRawVector3DExtrusionValidity())
                            {
                                Enabled = true;
                                return null;
                            }
                            else
                            {
                                MCTSLayerDataRawVector3DExtrusionGraphical layerData8 = new MCTSLayerDataRawVector3DExtrusionGraphical();
                                layerData8 = (MCTSLayerDataRawVector3DExtrusionGraphical)SetParams(layerData8);
                                layerData8.Params = ucLayerParams1.GetRawVector3DExtrusionGraphicalParams();
                                layerData8.ExtrusionHeightMaxAddition = ucLayerParams1.GetExtrusionHeightMaxAddition();
                                layerData8.StrIndexingDataDirectory = ucLayerParams1.GetExtrusionIndexingDataDirectory();
                                return layerData8;
                            }
                        }
                        else
                        {
                            MCTSLayerDataRawVector3DExtrusion layerData9 = new MCTSLayerDataRawVector3DExtrusion();
                            layerData9 = (MCTSLayerDataRawVector3DExtrusion)SetParams(layerData9);
                            layerData9.Params = ucLayerParams1.GetRawVector3DExtrusionParams();
                            layerData9.ExtrusionHeightMaxAddition = ucLayerParams1.GetExtrusionHeightMaxAddition();
                            return layerData9;
                        }

                    case DNELayerType._ELT_RAW_3D_MODEL:
                        MCTSLayerDataRaw3DModel layerData10 = new MCTSLayerDataRaw3DModel();
                        layerData10 = (MCTSLayerDataRaw3DModel)SetParams(layerData10);
                       // layerData10.OrthometricHeights = ucLayerParams1.GetOrthometricHeights();
                        layerData10.NumLevelsToIgnore = ucLayerParams1.GetNumLevelsToIgnore();
                       // layerData10.StrIndexingDataDirectory = ucLayerParams1.Get3DModelIndexingDataDirectory();
                        return layerData10;

                    case DNELayerType._ELT_WEB_SERVICE_DTM:
                    case DNELayerType._ELT_WEB_SERVICE_RASTER:
                        MCTSLayerDataWebService layerData11 = new MCTSLayerDataWebService();
                        layerData11 = (MCTSLayerDataWebService)SetParams(layerData11);
                        layerData11.WebMapServiceType = ucLayerParams1.GetWMSTypeSelectedLayer();
                        layerData11.Params = ucLayerParams1.GetWebMapServiceParams(layerData11.Params, layerData11.WebMapServiceType);
                        return layerData11;

                }

                return layerDataDefault;
            }
            return null;
        }

        private void LoadCurrRow(object tag)
        {
            if (tag is MCTSLayerData)
            {
                MCTSLayerData layerData = (MCTSLayerData)tag;
                ucLayerParams1.SetMapLayerType(layerData.LayerType);
                ucLayerParams1.SetLayerFileName(layerData.LayerPath);
                ucLayerParams1.SetNumOfLayers(layerData.NoOfLayers);
                ucLayerParams1.SetLocalCacheLayerParams(layerData.LayerSubFolderLocalCache);

                switch (layerData.LayerType)
                {
                    case DNELayerType._ELT_NATIVE_SERVER_DTM:
                    case DNELayerType._ELT_NATIVE_SERVER_RASTER:
                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR:
                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR_3D_EXTRUSION:
                    case DNELayerType._ELT_NATIVE_SERVER_3D_MODEL:
                    case DNELayerType._ELT_NATIVE_SERVER_TRAVERSABILITY:
                    case DNELayerType._ELT_NATIVE_SERVER_MATERIAL:
                    case DNELayerType._ELT_NATIVE_VECTOR:
                        break;
                    case DNELayerType._ELT_NATIVE_HEAT_MAP:
                    case DNELayerType._ELT_NATIVE_RASTER:
                        MCTSLayerDataRaster layerData3 = (MCTSLayerDataRaster)layerData;
                        ucLayerParams1.SetThereAreMissingFiles(layerData3.ThereAreMissingFiles);
                        ucLayerParams1.SetFirstLowerQualityLevel(layerData3.FirstLowerQualityLevel);
                        ucLayerParams1.SetEnhanceBorderOverlap2(layerData3.EnhanceBorderOverlap);
                        break;

                    case DNELayerType._ELT_NATIVE_3D_MODEL:
                    case DNELayerType._ELT_NATIVE_DTM:
                        MCTSLayerDataDTM layerData4 = (MCTSLayerDataDTM)layerData;
                        ucLayerParams1.SetNumLevelsToIgnore(layerData4.NumLevelsToIgnore);

                        break;                  
                    case DNELayerType._ELT_RAW_RASTER:
                    case DNELayerType._ELT_RAW_DTM:
                        MCTSLayerDataRawDTM layerData5 = (MCTSLayerDataRawDTM)layerData;
                        ucLayerParams1.SetRawParams(layerData5.Params);

                        if (layerData is MCTSLayerDataRawRaster)
                        {
                            MCTSLayerDataRawRaster layerData6 = (MCTSLayerDataRawRaster)layerData;
                            ucLayerParams1.SetImageCoordSys(layerData6.ImageCoordSys);
                        }
                        break;
                    case DNELayerType._ELT_RAW_VECTOR:
                        MCTSLayerDataRawVector layerData7 = (MCTSLayerDataRawVector)layerData ;
                        ucLayerParams1.SetRawVectorParams(layerData7.Params);
                        ucLayerParams1.SetTilingScheme(layerData7.TilingScheme);
                        ucLayerParams1.SetTargetGridCoordinateSystem(layerData7.GridCoordSys);

                        break;
                    case DNELayerType._ELT_RAW_VECTOR_3D_EXTRUSION:

                        if(layerData is MCTSLayerDataRawVector3DExtrusionGraphical)
                        {

                        }
                        else if( layerData is MCTSLayerDataRawVector3DExtrusion)
                        {

                        }
                        /*  if (ucLayerParams1.GetIsUseIndexing())
                          {
                              if (!ucLayerParams1.CheckRawVector3DExtrusionValidity())
                              {
                                  Enabled = true;
                                  return null;
                              }
                              else
                              {
                                  MCTSLayerDataRawVector3DExtrusionGraphical layerData8 = new MCTSLayerDataRawVector3DExtrusionGraphical();
                                  layerData8 = (MCTSLayerDataRawVector3DExtrusionGraphical)SetParams(layerData8);
                                  layerData8.LayerType = eLayerType;
                                  layerData8.Params = ucLayerParams1.GetRawVector3DExtrusionGraphicalParams();
                                  layerData8.ExtrusionHeightMaxAddition = ucLayerParams1.GetExtrusionHeightMaxAddition();
                                  layerData8.StrIndexingDataDirectory = ucLayerParams1.GetExtrusionIndexingDataDirectory();
                                  return layerData8;
                              }
                          }
                          else
                          {
                              MCTSLayerDataRawVector3DExtrusion layerData9 = new MCTSLayerDataRawVector3DExtrusion();
                              layerData9 = (MCTSLayerDataRawVector3DExtrusion)SetParams(layerData9);
                              layerData9.LayerType = eLayerType;
                              layerData9.Params = ucLayerParams1.GetRawVector3DExtrusionParams();
                              layerData9.ExtrusionHeightMaxAddition = ucLayerParams1.GetExtrusionHeightMaxAddition();
                              return layerData9;
                          }*/
                        break;
                    case DNELayerType._ELT_RAW_3D_MODEL:
                        /* MCTSLayerDataRaw3DModel layerData10 = new MCTSLayerDataRaw3DModel();
                         layerData10 = (MCTSLayerDataRaw3DModel)SetParams(layerData10);
                         layerData10.LayerType = eLayerType;
                         layerData10.OrthometricHeights = ucLayerParams1.GetOrthometricHeights();
                         layerData10.NumLevelsToIgnore = ucLayerParams1.GetNumLevelsToIgnore();
                         layerData10.StrIndexingDataDirectory = ucLayerParams1.Get3DModelIndexingDataDirectory();
                         return layerData10;*/
                        break;
                    
                    case DNELayerType._ELT_WEB_SERVICE_DTM:
                    case DNELayerType._ELT_WEB_SERVICE_RASTER:
                       /* MCTSLayerDataWebService layerData5 = (MCTSLayerDataRawDTM)layerData;
                        ucLayerParams1.SetRawParams((layerData5.Params), CtrlTilingSchemeParams.GetETilingSchemeType(layerData5.Params.pTilingScheme));

                        if (layerData is MCTSLayerDataRawRaster)
                        {
                            MCTSLayerDataRawRaster layerData6 = (MCTSLayerDataRawRaster)layerData;
                            ucLayerParams1.SetImageCoordSys(layerData6.ImageCoordSys);
                        }*/
                        break;

                       
                }
            }
        }

        private void btnLoadFileName_Click(object sender, EventArgs e)
        {
            txtFileName.Text = OpenFileDlg();
        }

        private void btnLoadBaseDir_Click(object sender, EventArgs e)
        {
            txtBaseDirectory.Text = OpenFileDlg(true);
        }

        private string OpenFileDlg(bool isFolder = false)
        {
            if (isFolder)
            {
                FolderSelectDialog FSD = new FolderSelectDialog();
                FSD.Title = "Folder to select";
                FSD.InitialDirectory = @"c:\";
                if (FSD.ShowDialog(IntPtr.Zero))
                {
                    return FSD.FileName;
                }
                else
                    return "";
            }
            else
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
        }

        private void btnMCOpenServerLayers_Click(object sender, EventArgs e)
        {
            OpenServerLayers_Click(txtMapCoreServerPath, DNEWebMapServiceType._EWMS_MAPCORE);
        }

        private void btnWMTSOpenServerLayers_Click(object sender, EventArgs e)
        {
            OpenServerLayers_Click(txtWMTSServerPath, DNEWebMapServiceType._EWMS_WMTS);
        }

        private void btnWCSOpenServerLayers_Click(object sender, EventArgs e)
        {
            OpenServerLayers_Click(txtWCSServerPath, DNEWebMapServiceType._EWMS_WCS);
        }
        private void btnWMSOpenServerLayers_Click(object sender, EventArgs e)
        {
            OpenServerLayers_Click(txtWMSServerPath, DNEWebMapServiceType._EWMS_WMS);
        }

        private void btnRequestParams_Click(object sender, EventArgs e)
        {
            frmRequestParams frmKeyValueArray1 = new frmRequestParams(m_RequestParams, "Server Request Params");
            if (frmKeyValueArray1.ShowDialog() == DialogResult.OK)
            {
                m_RequestParams = frmKeyValueArray1.GetMcKeyStringValues();
                m_SWCSParams.aRequestParams = m_RequestParams == null ? null : m_RequestParams.ToArray();
                m_SWMTSParams.aRequestParams = m_RequestParams == null ? null : m_RequestParams.ToArray();
                m_SWMSParams.aRequestParams = m_RequestParams == null ? null : m_RequestParams.ToArray();
            }
        }

        private void OpenServerLayers_Click(TextBox txtServerPath, DNEWebMapServiceType webMapServiceType)
        {
            if (txtServerPath.Text == "")
            {
                MessageBox.Show("Server path field is empty, please fill it.", "Missing server path");
                txtServerPath.Select();
                return;
            }

            if (Parent.Parent.Parent is frmMagicForm)
            {
                ((Parent.Parent.Parent) as frmMagicForm).CreateDevice();
            }

            string sURL = txtServerPath.Text.Trim();
            try
            {
                if (webMapServiceType == DNEWebMapServiceType._EWMS_MAPCORE)
                    Manager_MCLayers.UrlMapCoreServer = sURL;
                else if (webMapServiceType == DNEWebMapServiceType._EWMS_WCS)
                    Manager_MCLayers.UrlWCSServer = sURL;
                else if (webMapServiceType == DNEWebMapServiceType._EWMS_WMS)
                    Manager_MCLayers.UrlWMSServer = sURL;
                else if (webMapServiceType == DNEWebMapServiceType._EWMS_WMTS)
                    Manager_MCLayers.UrlWMTSServer = sURL;

                MCTServerLayersAsyncOperationCallback mctAsyncOperationCallback = new MCTServerLayersAsyncOperationCallback(this, sURL);

                DNMcMapDevice.GetWebServerLayers(sURL, webMapServiceType, m_RequestParams == null ? null : m_RequestParams.ToArray(), mctAsyncOperationCallback);

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcMapDevice.GetWebServerLayers", McEx);
            }
        }

        private DNSWebMapServiceParams GetWebMapServiceParams(DNEWebMapServiceType eWebMapServiceType)
        {
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

                mFrmWebMapLayers = new FrmWebMapLayers(mServerSelectLayers, eWebMapServiceType);
                mFrmWebMapLayers.SetWebServerLayers(eStatus, strServerURL, GetWebMapServiceParams(eWebMapServiceType), aLayers, astrServiceMetadataURLs, urlServer, strServiceProviderName, isOpenAsOneLayer, mIsWCSRaster);

                mFrmWebMapLayers.ShowDialog();
                if (mFrmWebMapLayers.DialogResult == DialogResult.OK)
                {
                    mServerSelectLayers = mFrmWebMapLayers.SelectLayers;

                    if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS)
                    {
                        m_SWMTSParams = (DNSWMTSParams)mFrmWebMapLayers.GetWebMapServiceParams();
                    }
                    else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WCS)
                    {
                        m_SWCSParams = (DNSWCSParams)mFrmWebMapLayers.GetWebMapServiceParams();
                        mIsWCSRaster = mFrmWebMapLayers.IsRaster;
                    }
                    else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMS)
                    {
                        m_SWMSParams = (DNSWMSParams)mFrmWebMapLayers.GetWebMapServiceParams();
                    }

                    if (mDicServerSelectLayers.ContainsKey(urlServer))
                        mDicServerSelectLayers.Remove(urlServer);
                    mDicServerSelectLayers.Add(urlServer, mServerSelectLayers);

                    if (mDicServerSelectLayersType.ContainsKey(urlServer))
                        mDicServerSelectLayersType.Remove(urlServer);
                    mDicServerSelectLayersType.Add(urlServer, eWebMapServiceType);

                    if (eWebMapServiceType == DNEWebMapServiceType._EWMS_MAPCORE)
                    {
                        lblMapCoreServerLayersCount.Text = mServerSelectLayers.Count + " layers";
                    }
                    else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS)
                    {
                        lblWMTSServerLayersCount.Text = mServerSelectLayers.Count + " layers";
                        mWMTSOpenAllLayersAsOne = mFrmWebMapLayers.OpenAllLayersAsOne;
                    }
                    else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WCS)
                    {
                        lblWCSServerLayersCount.Text = mServerSelectLayers.Count + " layers";
                    }
                    else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMS)
                    {
                        lblWMSServerLayersCount.Text = mServerSelectLayers.Count + " layers";
                        mWMSOpenAllLayersAsOne = mFrmWebMapLayers.OpenAllLayersAsOne;
                    }
                }
            }
            else
            {
                MessageBox.Show("Return status : " + eStatus.ToString() + ".", "Get Web Server Layers");

            }
        }

        private void AddLayerCtrl_Load(object sender, EventArgs e)
        {
            lstLayers.Items.Clear();

            Dictionary<object, uint> LayerDic = Manager_MCLayers.AllParams;
            List<IDNMcMapTerrain> terrainList = Manager_MCTerrain.AllTerrains();
            foreach (IDNMcMapTerrain terr in terrainList)
            {
                IDNMcMapLayer[] layersInTerr = terr.GetLayers();
                foreach (IDNMcMapLayer layer in layersInTerr)
                {
                    if (!lstExistLayers.Contains(layer))
                    {
                        lstLayers.Items.Add(Manager_MCNames.GetNameByObject(layer));
                        lstExistLayers.Add(layer);
                    }
                }
            }

            foreach (IDNMcMapLayer layer in LayerDic.Keys)
            {
                lstLayers.Items.Add(Manager_MCNames.GetNameByObject(layer));
                lstExistLayers.Add(layer);
            }

        }

        private void lstLayers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstLayers.SelectedIndex >= 0 && lstLayers.SelectedIndex < lstExistLayers.Count)
            {
                frmLayerParams frmLayerParams1 = new frmLayerParams(lstExistLayers[lstLayers.SelectedIndex]);
                frmLayerParams1.Show();
            }
        }

        public Dictionary<string, List<Manager_MCLayers.MCTWebServerUserSelection>> GetServerSelectLayers()
        {
            return mDicServerSelectLayers;
        }


        public List<IDNMcMapLayer> GetSelectedLayers()
        {
            // lstLayers
            List<IDNMcMapLayer> selectedLayers = new List<IDNMcMapLayer>();
            if(lstLayers.SelectedIndices != null && lstLayers.SelectedIndices.Count > 0)
            {
                foreach (int i in lstLayers.SelectedIndices)
                    selectedLayers.Add(lstExistLayers[i]);
            }
            IDNMcMapLayer NewLayer = null;
            Manager_MCLayers.ResetServerLayersPriorityDic();

            DNSLocalCacheLayerParams? localCacheLayerParams = ucLayerParams1.GetLocalCacheLayerParams();
            m_NewLayers = new List<IDNMcMapLayer>();
            MCTMapLayerReadCallback.IsSaveReplacedOrRemovedLayer = true;

            // selected layers from grid
            if (dgvLayers.Rows.Count > 0)
            {
                string LayerFileName = "";
                object tag = null;
                for (int i = 0; i < dgvLayers.RowCount; i++)
                {
                    tag = dgvLayers.Rows[i].Tag;
                    if (tag != null && tag is MCTSLayerData)
                    {
                        MCTSLayerData layerData = (tag as MCTSLayerData);
                        LayerFileName = layerData.LayerPath;
                        IDNMcMapLayer layer = null;
                        List<string> subdirs = new List<string>();
                        string strLayerType = layerData.LayerType.ToString().Replace(Manager_MCLayers.LayerTypePrefix, ""); ;
                        if (strLayerType.StartsWith("NATIVE") && !strLayerType.StartsWith("NATIVE_SERVER"))
                            subdirs = Manager_MCLayers.GetRecursiveFolders(LayerFileName);
                        for ( i = 0; i < ucLayerParams1.GetNumOfLayers(); i++)
                        {
                            m_lstRawVectorLayers.Clear();
                            switch (strLayerType)
                            {
                                case "NATIVE_DTM":

                                    NewLayer = Manager_MCLayers.CreateNativeDTMLayer(LayerFileName, ucLayerParams1.GetNumLevelsToIgnore(), localCacheLayerParams);
                                    break;
                                case "NATIVE_SERVER_DTM":
                                    NewLayer = Manager_MCLayers.CreateNativeServerDTMLayer(LayerFileName);
                                    break;
                                case "NATIVE_MATERIAL":
                                    foreach (string dir in subdirs)
                                    {
                                        layer = Manager_MCLayers.CreateNativeMaterialLayer(dir, ucLayerParams1.GetThereAreMissingFiles(), localCacheLayerParams);
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
                                        if (layer != null)
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


                                    DNSRawParams dnParams = ucLayerParams1.GetSRawParams();
                                    if (dnParams == null)
                                    {
                                        MessageBox.Show("Invalid Pyramid Resolution Values");
                                        //  return;
                                    }
                                    if (ucLayerParams1.GetMapLayerType() == "RAW_DTM")
                                        NewLayer = Manager_MCLayers.CreateRawDTMLayer(
                                                                        dnParams,
                                                                        localCacheLayerParams);
                                    else if (ucLayerParams1.GetMapLayerType() == "RAW_RASTER")
                                        NewLayer = Manager_MCLayers.CreateRawRasterLayer(
                                                                        dnParams,
                                                                        ucLayerParams1.GetImageCoordSys(),
                                                                        localCacheLayerParams);
                                    break;
                                case "RAW_VECTOR":
                                    DNSRawVectorParams rawVectorParams = ucLayerParams1.GetRawVectorParams();
                                    //if (rawVectorParams == null)
                                    //  return;

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
                                        // IDNMcMapLayer vectorMapLayer = CreateRawVector3DExtrusion(dataLayer);
                                        // if (vectorMapLayer != null)
                                        //      m_lstRawVectorLayers.Add(vectorMapLayer);
                                    }
                                    m_NewLayers.AddRange(m_lstRawVectorLayers);

                                    break;
                                case "RAW_3D_MODEL":
                                    if (!ucLayerParams1.CheckRaw3DModelValidity())
                                    {
                                        Enabled = true;
                                        // return;
                                    }

                                    NewLayer = null;  /*Manager_MCLayers.CreateRaw3DModelLayer(LayerFileName,
                                        ucLayerParams1.GetOrthometricHeights(),
                                        ucLayerParams1.GetNumLevelsToIgnore(),
                                        ucLayerParams1.Get3DModelIndexingDataDirectory());*/
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
                                                    NewLayer = Manager_MCLayers.CreateWebServiceDtmLayer(Manager_MCLayers.mcCurrSWMSParams, localCacheLayerParams);
                                            }
                                            break;

                                        case DNEWebMapServiceType._EWMS_WMTS:
                                            {
                                                Manager_MCLayers.mcLastWMSLayerType = DNEWebMapServiceType._EWMS_WMTS;
                                                Manager_MCLayers.mcCurrSWMTSParams = (DNSWMTSParams)ucLayerParams1.GetWebMapServiceParams(Manager_MCLayers.mcCurrSWMTSParams, Manager_MCLayers.mcLastWMSLayerType);

                                                if (ucLayerParams1.GetMapLayerType() == "WEB_SERVICE_RASTER")
                                                    NewLayer = Manager_MCLayers.CreateWebServiceRasterLayer(Manager_MCLayers.mcCurrSWMTSParams, localCacheLayerParams);
                                                else
                                                    NewLayer = Manager_MCLayers.CreateWebServiceDtmLayer(Manager_MCLayers.mcCurrSWMTSParams, localCacheLayerParams);
                                            }
                                            break;
                                        case DNEWebMapServiceType._EWMS_WCS:
                                            {
                                                Manager_MCLayers.mcLastWMSLayerType = DNEWebMapServiceType._EWMS_WCS;
                                                Manager_MCLayers.mcCurrSWCSParams = (DNSWCSParams)ucLayerParams1.GetWebMapServiceParams(Manager_MCLayers.mcCurrSWCSParams, Manager_MCLayers.mcLastWMSLayerType);

                                                if (ucLayerParams1.GetMapLayerType() == "WEB_SERVICE_RASTER")
                                                    NewLayer = Manager_MCLayers.CreateWebServiceRasterLayer(Manager_MCLayers.mcCurrSWCSParams, localCacheLayerParams);
                                                else
                                                    NewLayer = Manager_MCLayers.CreateWebServiceDtmLayer(Manager_MCLayers.mcCurrSWCSParams, localCacheLayerParams);
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
                                //  return;
                            }
                        }
                    }
                }
                
             
               

                if (m_NewLayers.Count > 0)
                {
                }
            }
                     
            // load from folder
            if (txtFileName.Text != "")
            {
                try
                {
                    UserDataFactory UDF = new UserDataFactory();
                    IDNMcMapLayer NewLayer2 = DNMcMapLayer.Load(txtFileName.Text,
                                                    /*BaseDirectory*/ "",
                                                    UDF,
                                                    new MCTMapLayerReadCallbackFactory());

                    selectedLayers.Add(NewLayer2);
                    Manager_MCLayers.CheckingAfterCreateLayer(NewLayer2, true);

                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNMcMapLayer.Load", McEx);
                }
            }

            // todo - create from servers 


            return selectedLayers;
        }
    }
}
