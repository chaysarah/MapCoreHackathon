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
using MCTester.Managers.MapWorld;
using MCTester.General_Forms;
using static MCTester.Managers.MapWorld.Manager_MCLayers;
using MCTester.Managers;
using MCTester.MapWorld.MapUserControls;
using UnmanagedWrapper;
using MapCore.Common;
using MCTester.Controls;

namespace MCTester.MapWorld.WebMapLayers
{
    public partial class CtrlWebServiceLayerParams : UserControl
    {
        private DNEWebMapServiceType m_WMSTypeSelectedLayer;
        private string[] m_StyleNames;
        private string m_StyleUserSelection;
        private DNSTileMatrixSet[] m_TileMatrixSet;
        private IDNMcGridCoordinateSystem m_GridCoordinateSystem;
        private List<DNSMcKeyStringValue> m_RequestParams;
        private CtrlNonNativeParams mCtrlNonNativeParams1;
        private bool m_IsMultiSelection;
        private int m_CurrNumSelected = 0;
        public bool m_IsOpenAllLayersAsOneChanged = false;
        private bool m_isReadOnly = false;
        string m_ctrlBoundingBoxGroupBoxText = "Bounding Box";

        public CtrlWebServiceLayerParams()
        {
            InitializeComponent();

            SetParams(new DNSWMSParams());
            SetParams(new DNSWMTSParams());
            SetParams(new DNSWCSParams());
            SetParams(new MCTRaw3DModelParams());

            string[] serviceTypes = Enum.GetNames(typeof(DNEWebMapServiceType));
            List<string> lstServiceTypes = serviceTypes.ToList();
            lstServiceTypes.Remove(DNEWebMapServiceType._EWMS_MAPCORE.ToString());

            cbWMSTypes.Items.AddRange(lstServiceTypes.ToArray());
            cbWMSTypes.Text = Manager_MCLayers.mcLastWMSLayerType.ToString();
            m_WMSTypeSelectedLayer = Manager_MCLayers.mcLastWMSLayerType;

            cbCapabilitiesBoundingBoxAxesOrder.Items.AddRange(Enum.GetNames(typeof(DNECoordinateAxesOrder)));
            cbUseServerTilingScheme.Checked = true;

            ctrlBoundingBox.HideZ();
        }

        public void SetUsedServerTilingScheme(object pbUsedServerTilingScheme)
        {
            chxUsedServerTilingScheme.Visible = true;
            if (pbUsedServerTilingScheme == null)
                chxUsedServerTilingScheme.CheckState = CheckState.Indeterminate;
            else
            {
                chxUsedServerTilingScheme.CheckState = CheckState.Checked;
                chxUsedServerTilingScheme.Checked = (bool)pbUsedServerTilingScheme;
            }
        }

        public void SetType(DNEWebMapServiceType type)
        {
            cbWMSTypes.Text = type.ToString();
        }

        private void SetWebMapServiceParams(DNSWebMapServiceParams layerParams)
        {
            tbServerURL.Text = layerParams.strServerURL;
            tbOptionalUserAndPassword.Text = layerParams.strOptionalUserAndPassword;
            tbLayers.Text = layerParams.strLayersList;
            ctrlBoundingBox.SetBoxValue(layerParams.BoundingBox);

            if (cmbImageFormat.Items.Count > 0 && cmbImageFormat.Items.Contains(layerParams.strImageFormat))
                cmbImageFormat.SelectedItem = layerParams.strImageFormat;
            else
                txImageFormat.Text  = layerParams.strImageFormat;

            tbStylesList.Text = layerParams.strStylesList;

            if (layerParams.aRequestParams != null)
                m_RequestParams = layerParams.aRequestParams.ToList();
            else
                m_RequestParams = null;

            chxSkipSSLCertificateVerification.Checked = layerParams.bSkipSSLCertificateVerification;
            ntbTimeoutInSec.SetUInt32(layerParams.uTimeoutInSec);
            chxTransparent.Checked = layerParams.bTransparent;
            txImageFormat.Text = layerParams.strImageFormat;
            tbZeroBlockHttpCodes.Text = layerParams.strZeroBlockHttpCodes;
            bZeroBlockOnServerException.Checked = layerParams.bZeroBlockOnServerException;
        }

        internal void IsReadOnly(bool isReadOnly)
        {
            m_isReadOnly = isReadOnly;
            
        }

        public void SetParams(DNSWCSParams layerParams)
        {
            SetType(DNEWebMapServiceType._EWMS_WCS);
            SetWebMapServiceParams((DNSWebMapServiceParams)layerParams);
            tbWCSVersion.Text = layerParams.strWCSVersion;
            chxDontUseServerInterpolation.Checked = layerParams.bDontUseServerInterpolation;
        }

        public void SetParams(MCTRaw3DModelParams layerParams)
        {
            if (layerParams != null)
            {
                chxOrthometricHeights.Checked = layerParams.OrthometricHeights;
                ctrlBoundingBox.SetBoxValue(layerParams.pClipRect);
                if (mCtrlNonNativeParams1 != null)
                {
                    mCtrlNonNativeParams1.SetGridCoordinateSystem(layerParams.pTargetCoordinateSystem);
                }
                ntxTargetHighestResolution.SetFloat(layerParams.fTargetHighestResolution);
                if (layerParams.aRequestParams != null)
                    m_RequestParams = layerParams.aRequestParams.ToList();
                else
                    m_RequestParams = null;

            }

        }

        public void SetParams(DNSWMSParams layerParams)
        {
            SetType(DNEWebMapServiceType._EWMS_WMS);

            SetWebMapServiceParams((DNSWebMapServiceParams)layerParams);
            tbWMSVersion.Text = layerParams.strWMSVersion;
            ntbBlockWidth.SetUInt32(layerParams.uBlockWidth);
            ntbBlockHeight.SetUInt32(layerParams.uBlockHeight);
            tbMinScale.SetFloat(layerParams.fMinScale);

            if (cmbWMSCoordinateSystem.Items.Count > 0)
            {
                if(layerParams.strServerCoordinateSystem == "" || layerParams.strServerCoordinateSystem == null)
                    cmbWMSCoordinateSystem.SelectedIndex = -1;
                else
                    cmbWMSCoordinateSystem.SelectedItem = layerParams.strServerCoordinateSystem;
            }
            else
                tbWMSCoordinateSystem.Text = layerParams.strServerCoordinateSystem;
        }

        public void SetParams(DNSWMTSParams layerParams)
        {
            SetType(DNEWebMapServiceType._EWMS_WMTS);

            SetWebMapServiceParams((DNSWebMapServiceParams)layerParams);
            bExtendBeyondDateLine.Checked = layerParams.bExtendBeyondDateLine;
            tbInfoFormat.Text = layerParams.strInfoFormat;

            if (cmbTileMatrixSet.Items.Count > 0)
                cmbTileMatrixSet.SelectedItem = layerParams.strServerCoordinateSystem;
            else
                tbTileMatrixSet.Text = layerParams.strServerCoordinateSystem;

            cbCapabilitiesBoundingBoxAxesOrder.Text = layerParams.eCapabilitiesBoundingBoxAxesOrder.ToString();
            cbUseServerTilingScheme.Checked = layerParams.bUseServerTilingScheme;
        }

        internal void setCtrlNonNativeParams(CtrlNonNativeParams ctrlNonNativeParams1)
        {
            mCtrlNonNativeParams1 = ctrlNonNativeParams1;
        }

        private void SetGridCoordinateSystem(IDNMcGridCoordinateSystem gridCoordinateSystem)
        {
            mCtrlNonNativeParams1.SetGridCoordinateSystem(gridCoordinateSystem);
        } 

        private void cbWMSTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_ctrlBoundingBoxGroupBoxText = "Bounding Box";
            panel1.Visible = true;
            ctrlBoundingBox.Location = new System.Drawing.Point(4, 166);

            if (cbWMSTypes.Text == DNEWebMapServiceType._EWMS_WMS.ToString())
            {
                m_WMSTypeSelectedLayer = DNEWebMapServiceType._EWMS_WMS;
                tcWebServiceLayerParams.SelectedTab = tpWMSParams;
            }
            else if (cbWMSTypes.Text == DNEWebMapServiceType._EWMS_WMTS.ToString())
            {
                m_WMSTypeSelectedLayer = DNEWebMapServiceType._EWMS_WMTS;
                tcWebServiceLayerParams.SelectedTab = tpWMTSParams;
            }
            else if (cbWMSTypes.Text == DNEWebMapServiceType._EWMS_CSW.ToString())
            {
                m_WMSTypeSelectedLayer = DNEWebMapServiceType._EWMS_CSW;
                tcWebServiceLayerParams.SelectedTab = tpCSWParams;
                m_ctrlBoundingBoxGroupBoxText = "Clip Rect";
                panel1.Visible = false;
                ctrlBoundingBox.Location = panel1.Location;
            }
            else if(cbWMSTypes.Text == DNEWebMapServiceType._EWMS_WCS.ToString())
            {
                m_WMSTypeSelectedLayer = DNEWebMapServiceType._EWMS_WCS;
                tcWebServiceLayerParams.SelectedTab = tpWCSParams;
            }
           
        }

        public DNEWebMapServiceType GetWMSTypeSelectedLayer()
        {
            return m_WMSTypeSelectedLayer;
        }

        private void tcWebServiceLayerParams_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 0 &&
             (m_WMSTypeSelectedLayer == DNEWebMapServiceType._EWMS_WMS))
                return;
            if (e.TabPageIndex == 1 &&
               (m_WMSTypeSelectedLayer == DNEWebMapServiceType._EWMS_WMTS))
                return;
            if (e.TabPageIndex == 2 &&
               (m_WMSTypeSelectedLayer == DNEWebMapServiceType._EWMS_WCS))
                return;
            if (e.TabPageIndex == 3 &&
              (m_WMSTypeSelectedLayer == DNEWebMapServiceType._EWMS_CSW))
                return;
            e.Cancel = true;
        }

    
        public DNSWMSParams GetWMSParams(DNSWMSParams mcSWMSParams)
        {
            // not new - for non native params DNSWMSParams mcSWMSParams = new DNSWMSParams();
            GetWebMapServiceParams(mcSWMSParams);
            mcSWMSParams.strWMSVersion = tbWMSVersion.Text;
            mcSWMSParams.uBlockWidth = ntbBlockWidth.GetUInt32();
            mcSWMSParams.uBlockHeight = ntbBlockHeight.GetUInt32();
            mcSWMSParams.fMinScale = tbMinScale.GetFloat();

            if (cmbWMSCoordinateSystem.Visible && cmbWMSCoordinateSystem.Items.Count > 0)
                mcSWMSParams.strServerCoordinateSystem = cmbWMSCoordinateSystem.Text;
            else
                mcSWMSParams.strServerCoordinateSystem = tbWMSCoordinateSystem.Text;


            return mcSWMSParams;
        }

        public DNSWMTSParams GetWMTSParams(DNSWMTSParams mcSWMTSParams)
        {
            // not new - for non native params DNSWMTSParams mcSWMTSParams = new DNSWMTSParams();
            GetWebMapServiceParams(mcSWMTSParams);
            mcSWMTSParams.bExtendBeyondDateLine = bExtendBeyondDateLine.Checked;
            mcSWMTSParams.eCapabilitiesBoundingBoxAxesOrder = (DNECoordinateAxesOrder)Enum.Parse(typeof(DNECoordinateAxesOrder), cbCapabilitiesBoundingBoxAxesOrder.Text); ;
            mcSWMTSParams.strInfoFormat = tbInfoFormat.Text;
            mcSWMTSParams.bUseServerTilingScheme = cbUseServerTilingScheme.Checked;

            if (cmbTileMatrixSet.Items.Count > 0)
                mcSWMTSParams.strServerCoordinateSystem = cmbTileMatrixSet.Text;
            else
                mcSWMTSParams.strServerCoordinateSystem = tbTileMatrixSet.Text;

            if (m_GridCoordinateSystem != null)
                mcSWMTSParams.pCoordinateSystem = m_GridCoordinateSystem;

            return mcSWMTSParams;
        }

        public DNSWCSParams GetWCSParams(DNSWCSParams mcSWCSParams)
        {
            // not new - for non native params DNSWCSParams mcSWCSParams = new DNSWCSParams();
            GetWebMapServiceParams(mcSWCSParams);
            mcSWCSParams.strWCSVersion = tbWCSVersion.Text;
            mcSWCSParams.bDontUseServerInterpolation = chxDontUseServerInterpolation.Checked;

            return mcSWCSParams;
        }

        private void GetWebMapServiceParams(DNSWebMapServiceParams webMapServiceParams)
        {
            webMapServiceParams.strServerURL = tbServerURL.Text;
            webMapServiceParams.strOptionalUserAndPassword = tbOptionalUserAndPassword.Text;
            webMapServiceParams.bSkipSSLCertificateVerification = chxSkipSSLCertificateVerification.Checked;
            webMapServiceParams.uTimeoutInSec = ntbTimeoutInSec.GetUInt32();
            webMapServiceParams.strLayersList = tbLayers.Text;
            webMapServiceParams.BoundingBox = ctrlBoundingBox.GetBoxValue();

            if (cmbImageFormat.Visible && cmbImageFormat.Items.Count > 0)
                webMapServiceParams.strImageFormat = cmbImageFormat.Text;
            else
                webMapServiceParams.strImageFormat = txImageFormat.Text;

            webMapServiceParams.bTransparent = chxTransparent.Checked;
            webMapServiceParams.bZeroBlockOnServerException = bZeroBlockOnServerException.Checked;
            webMapServiceParams.strStylesList = tbStylesList.Text;
            webMapServiceParams.strZeroBlockHttpCodes = tbZeroBlockHttpCodes.Text;
            webMapServiceParams.aRequestParams = GetRequestParams();
        }

        public MCTRaw3DModelParams GetCSWParams()
        {
            MCTRaw3DModelParams raw3DModelParams = new MCTRaw3DModelParams();
            raw3DModelParams.fTargetHighestResolution = ntxTargetHighestResolution.GetFloat();
            raw3DModelParams.OrthometricHeights = chxOrthometricHeights.Checked;
            raw3DModelParams.pClipRect = ctrlBoundingBox.GetBoxValue();
            if (mCtrlNonNativeParams1 != null)
            {
                raw3DModelParams.pTargetCoordinateSystem = mCtrlNonNativeParams1.GetGridCoordinateSystem();
            }
            raw3DModelParams.fTargetHighestResolution = ntxTargetHighestResolution.GetFloat();
            raw3DModelParams.aRequestParams = GetRequestParams();

            return raw3DModelParams;
        }

        public DNSMcKeyStringValue[] GetRequestParams()
        {
            return (m_RequestParams == null ? null : m_RequestParams.ToArray());
        }


        public void SetFromWeb(string urlServer, DNEWebMapServiceType eWebMapServiceType, DNSWebMapServiceParams webMapServiceParams)
        {
            switch(eWebMapServiceType)
            {
                case DNEWebMapServiceType._EWMS_WMTS:
                    SetParams((DNSWMTSParams)webMapServiceParams);
                    break;
                case DNEWebMapServiceType._EWMS_WCS:
                    SetParams((DNSWCSParams)webMapServiceParams);
                    break;
                case DNEWebMapServiceType._EWMS_WMS:
                    SetParams((DNSWMSParams)webMapServiceParams);
                    break;

            }
            tbServerURL.Text = urlServer;
            //cbWMSTypes.Text = eWebMapServiceType.ToString();
            cbWMSTypes.Enabled = false;
        }

        internal void SetSelectdLayers(string layers, string strCoordSystem, string strLayerType)
        {
            tbLayers.Text = layers;
            tbTileMatrixSet.Text = strCoordSystem;
            txImageFormat.Text = strLayerType;
        }

        internal void SetSelectdLayers(string layers, bool multiselection)
        {
            tbLayers.Text = layers;
            tbLayers.ReadOnly = multiselection;
        }      

        internal void SetStyles(string[] styles, string strStyle)
        {
            if (strStyle != MultiSelection)
            {
                string strStyles = "";
                m_StyleNames = styles;
                btnSelectStyle.Enabled = (styles != null && styles.Length > 0);
                if (styles != null && styles.Length > 0)
                {
                    foreach (string str in styles)
                        strStyles += str + ",";

                    if (strStyles != "")
                        strStyles = strStyles.Substring(0, strStyles.Length - 1);

                    tbStylesList.Text = strStyles;
                }
                else
                {
                    tbStylesList.Text = strStyle;
                }
            }
            else
            {
                tbStylesList.Enabled = btnSelectStyle.Enabled = false;
                tbStylesList.Text = strStyle;
            }
        }

        internal void SetBB(DNSMcBox boundingBox)
        {
            ctrlBoundingBox.SetBoxValue(boundingBox);
        }

        internal void SetGCS(string strWMSCoordinateSystem)
        {
            tbWMSCoordinateSystem.Text = strWMSCoordinateSystem;

            tbWMSCoordinateSystem.Visible = true;
            cmbWMSCoordinateSystem.Visible = false;

            cmbWMSCoordinateSystem.Location = tbWMSCoordinateSystem.Location;
            cmbWMSCoordinateSystem.Items.Clear();

            if (m_TileMatrixSet != null && m_TileMatrixSet.Length > 0)
            {
                cmbWMSCoordinateSystem.Visible = true;
                tbWMSCoordinateSystem.Visible = false;
                cmbWMSCoordinateSystem.Items.AddRange(m_TileMatrixSet.Select(x => x.strName).ToArray());
            }
            else
            {
                SetBB(new DNSMcBox());
            }
            cmbWMSCoordinateSystem.Text = strWMSCoordinateSystem;
        }

        internal void SetTransparent(bool bTransparent)
        {
            chxTransparent.Checked = bTransparent;
        }

        internal string GetSelectdLayers()
        {
            return tbLayers.Text;
        }

        public void SetImageFormat(string[] infos, string str)
        {
            cmbImageFormat.Items.Clear();
            if (infos != null && infos.Length > 0 && str != MultiSelection)
            {
                cmbImageFormat.Visible = true;
                cmbImageFormat.Items.AddRange(infos);
                cmbImageFormat.SelectedItem = infos[0];
            }
            else
            {
                cmbImageFormat.Visible = false;
                txImageFormat.Text = str;
                txImageFormat.Enabled = (str != MultiSelection);

            }
        }

        public void SetTileMatrixSets(string strTileMatrixSet)
        {
            cmbTileMatrixSet.Items.Clear();
            tbTileMatrixSet.ReadOnly = false;

            if (m_TileMatrixSet != null && m_TileMatrixSet.Length > 0 )
            {
                if (strTileMatrixSet != Manager_MCLayers.MultiSelection)
                {
                    string[] astrTileMatrixSets = m_TileMatrixSet.Select(x => x.strName).ToArray();
                    cmbTileMatrixSet.Visible = true;
                    cmbTileMatrixSet.Items.AddRange(astrTileMatrixSets);
                    cmbTileMatrixSet.SelectedItem = astrTileMatrixSets[0];
                }
                else  
                {
                    cmbTileMatrixSet.Visible = false;
                    tbTileMatrixSet.Text = strTileMatrixSet;
                    tbTileMatrixSet.ReadOnly = true;
                }
            }
            else
            {
                cmbTileMatrixSet.Visible = false;
                tbTileMatrixSet.Text = strTileMatrixSet;
                tbTileMatrixSet.ReadOnly = (strTileMatrixSet == Manager_MCLayers.MultiSelection);
                SetBB(new DNSMcBox());
            }
        }

        private void btnSelectStyle_Click(object sender, EventArgs e)
        {
            SelectWebServerStyle selectWebServerStyle = new SelectWebServerStyle(m_StyleNames, m_StyleUserSelection);
            if (m_isReadOnly)
                GeneralFuncs.SetControlsReadonly(selectWebServerStyle);

            if (selectWebServerStyle.ShowDialog() == DialogResult.OK)
            {
               tbStylesList.Text = m_StyleUserSelection = selectWebServerStyle.strSelectStyles;
            }
        }

        private void cmbTileMatrixSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_TileMatrixSet != null && m_TileMatrixSet.Length > 0 && cmbTileMatrixSet.SelectedIndex >= 0 && cmbTileMatrixSet.SelectedIndex < m_TileMatrixSet.Length)
            {
                m_GridCoordinateSystem = m_TileMatrixSet[cmbTileMatrixSet.SelectedIndex].pCoordinateSystem;
                //SetGridCoordinateSystem(m_GridCoordinateSystem);
                ChangeGCSAndBBFromTileMatrixSet(cmbTileMatrixSet.SelectedIndex);
            }
            else
            {
                SetBB(new DNSMcBox());
                SetGridCoordinateSystem(null);
            }
        }

        private void cmbImageFormat_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void btnRequestParams_Click(object sender, EventArgs e)
        {
            frmRequestParams frmKeyValueArray1 = new frmRequestParams(m_RequestParams, "Server Request Params");
            if(m_isReadOnly)
                GeneralFuncs.SetControlsReadonly(frmKeyValueArray1);
            frmKeyValueArray1.VisibleCSWParams(false);
            if (frmKeyValueArray1.ShowDialog() == DialogResult.OK)
            {
                m_RequestParams = frmKeyValueArray1.GetMcKeyStringValues();
            }
        }

        public void SetRequestParams(List<DNSMcKeyStringValue> RequestParams)
        {
            m_RequestParams = RequestParams;
        }

        private void cmbWMSCoordinateSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBBConverted.Visible = false;

            if (cmbWMSCoordinateSystem.Text == "")
            {
                SetBB(new DNSMcBox());
                SetGridCoordinateSystem(null);
            }
            else if (cmbWMSCoordinateSystem.SelectedItem == null || cmbWMSCoordinateSystem.SelectedItem.ToString() != cmbWMSCoordinateSystem.Text) // user write value
            {
                if (m_TileMatrixSet != null && m_TileMatrixSet.Length > 0 && m_TileMatrixSet.Select(x => x.strName).Contains(cmbWMSCoordinateSystem.Text))
                {
                    cmbWMSCoordinateSystem.SelectedItem = cmbWMSCoordinateSystem.Text;
                }
                else
                {
                    SetBB(new DNSMcBox());
                    SetGridCoordinateSystem(null);
                    lblBBConverted.Visible = true;
                }
            }
            else if (m_TileMatrixSet != null && cmbWMSCoordinateSystem.SelectedIndex >= 0 && cmbWMSCoordinateSystem.SelectedIndex < m_TileMatrixSet.Length)
            {
                ChangeGCSAndBBFromTileMatrixSet(cmbWMSCoordinateSystem.SelectedIndex);
            }
            else
            {
                SetBB(new DNSMcBox());
                SetGridCoordinateSystem(null);
                lblBBConverted.Visible = true;
            }
        }

        private void ChangeGCSAndBBFromTileMatrixSet(int selectedIndex)
        {
            lblBBConverted.Visible = false;
            if (m_TileMatrixSet != null && m_TileMatrixSet.Length > selectedIndex)
            {
                DNSTileMatrixSet tileMatrixSet = m_TileMatrixSet[selectedIndex];
                IDNMcGridCoordinateSystem gridCoordinateSystem = m_TileMatrixSet[0].bHasBoundingBox ?  m_TileMatrixSet[0].pCoordinateSystem : m_ServerLayerInfo.pCoordinateSystem;
                DNSMcBox mcCurrBox = tileMatrixSet.bHasBoundingBox ? m_TileMatrixSet[0].BoundingBox : m_ServerLayerInfo.BoundingBox;

                m_GridCoordinateSystem = tileMatrixSet.pCoordinateSystem;

                DNSMcBox mcBox = tileMatrixSet.BoundingBox;

                if (!tileMatrixSet.bHasBoundingBox)
                {
                    if (m_GridCoordinateSystem == null && gridCoordinateSystem != null)
                    {
                        try
                        {
                            m_GridCoordinateSystem = DNMcGridGeneric.Create(tileMatrixSet.strName);
                        }
                        catch (MapCoreException) { }
                    }
                        try
                        {
                            if (m_GridCoordinateSystem != null && gridCoordinateSystem != m_GridCoordinateSystem)
                            {
                                IDNMcGridConverter mcGridConverter = DNMcGridConverter.Create(gridCoordinateSystem, m_GridCoordinateSystem);
                                DNSMcVector3D Point1 = mcCurrBox.MinVertex;
                                DNSMcVector3D Point2 = mcCurrBox.MaxVertex;
                                DNSMcVector3D Point3 = new DNSMcVector3D(Point1.x, Point2.y, 0);
                                DNSMcVector3D Point4 = new DNSMcVector3D(Point2.x, Point1.y, 0);

                                DNSMcVector3D convertedPoint1 = new DNSMcVector3D();
                                DNSMcVector3D convertedPoint2 = new DNSMcVector3D();
                                DNSMcVector3D convertedPoint3 = new DNSMcVector3D();
                                DNSMcVector3D convertedPoint4 = new DNSMcVector3D();
                                int zone = 0;

                                mcGridConverter.ConvertAtoB(Point1, out convertedPoint1, out zone);
                                mcGridConverter.ConvertAtoB(Point2, out convertedPoint2, out zone);
                                mcGridConverter.ConvertAtoB(Point3, out convertedPoint3, out zone);
                                mcGridConverter.ConvertAtoB(Point4, out convertedPoint4, out zone);

                                DNSMcVector3D[] arr = new DNSMcVector3D[4] { convertedPoint1, convertedPoint2, convertedPoint3, convertedPoint4 };
                                double minX = convertedPoint1.x, minY = convertedPoint1.y, maxX = convertedPoint2.x, maxY = convertedPoint2.y;
                                for (int i = 0; i < 4; i++)
                                {
                                    if (arr[i].x < minX)
                                        minX = arr[i].x;
                                    if (arr[i].y < minY)
                                        minY = arr[i].y;

                                    if (arr[i].x > maxX)
                                        maxX = arr[i].x;
                                    if (arr[i].y > maxY)
                                        maxY = arr[i].y;
                                }
                                mcBox = new DNSMcBox(minX, minY, 0, maxX, maxY, 0);
                                if (mcBox.MinVertex == DNSMcVector3D.v3Zero || mcBox.MaxVertex == DNSMcVector3D.v3Zero)
                                    lblBBConverted.Visible = true;
                            }
                            else
                                lblBBConverted.Visible = true;
                        }
                        catch (MapCoreException)
                        {
                            //Utilities.ShowErrorMessage("DNMcGridConverter.Create / ConvertAtoB", McEx);
                            lblBBConverted.Visible = true;
                        }
                    }
                
                SetBB(mcBox);
                SetGridCoordinateSystem(m_GridCoordinateSystem);
            }
        }


        internal void SetMultiSelection(bool isMultiSelection)
        {
            m_IsMultiSelection = isMultiSelection;
        }

        DNSServerLayerInfo m_ServerLayerInfo;

        public void UpdateForm(List<MCTWebServerUserSelection> selectLayers, bool isOpenAllLayersAsOne, bool isInLoad, bool m_checkBoxGroupValue)
        {
            string layers = "";
            string strTileMatrixSets = "";
            string strImageFormat = "";
            string strStyle = "";
            string strGCS = "";
            bool bTransparent = false;
            string[] astrImageFormats = null;
            string[] astrStyles = null;
            m_ServerLayerInfo = new DNSServerLayerInfo();
            DNSMcBox boundingBox = new DNSMcBox();
            bool isMultiSelection = false;
            IDNMcGridCoordinateSystem gcs = null;
            isMultiSelection = !isOpenAllLayersAsOne && selectLayers.Count > 1;
            m_TileMatrixSet = null;
            if (selectLayers.Count == 1 || (selectLayers.Count > 0 && isInLoad) || m_checkBoxGroupValue || (m_IsOpenAllLayersAsOneChanged && selectLayers.Count > 0))
            {
                m_ServerLayerInfo = selectLayers[0].ServerLayerInfo;

                m_TileMatrixSet = m_ServerLayerInfo.aTileMatrixSets;

                astrImageFormats = m_ServerLayerInfo.astrImageFormats;

                gcs = GetGCS(m_ServerLayerInfo);
                strGCS = GetGCSStr(m_ServerLayerInfo);

                boundingBox = GetBB(m_ServerLayerInfo);

                bTransparent = m_ServerLayerInfo.bTransparent;

                astrStyles = m_ServerLayerInfo.astrStyles;
                SetStyles(astrStyles, strStyle);
            }
            if (selectLayers.Count > 0)
            {
                foreach (MCTWebServerUserSelection layerInfo in selectLayers)
                {
                    layers += layerInfo.ServerLayerInfo.strLayerId + ",";
                }
                if (layers != "")
                    layers = layers.Substring(0, layers.Length - 1);
            }

            SetSelectdLayers(layers, isMultiSelection);

            SetMultiSelection(isMultiSelection);

            // search tile matrix set if exist one that exist in all selected layers.
            if (m_WMSTypeSelectedLayer == DNEWebMapServiceType._EWMS_WMTS && selectLayers.Count > 1 && !isOpenAllLayersAsOne)
            {
                strTileMatrixSets = CheckWMTSTileMatrix(selectLayers);
                astrImageFormats = selectLayers[0].ServerLayerInfo.astrImageFormats;
                strImageFormat = CheckWMTSImageFormat(selectLayers);
                strStyle = CheckWMTSStyle(selectLayers);
            }
            if (selectLayers.Count == 0 || (selectLayers.Count == 1) || (selectLayers.Count > 0 && isInLoad) || m_checkBoxGroupValue || m_IsOpenAllLayersAsOneChanged)
            {
                
                SetImageFormat(astrImageFormats, strImageFormat);
                if (m_WMSTypeSelectedLayer == DNEWebMapServiceType._EWMS_WMTS)
                {
                    if (strTileMatrixSets != Manager_MCLayers.MultiSelection)
                        SetTileMatrixSets(strTileMatrixSets);
                   
                }
                else if (m_WMSTypeSelectedLayer == DNEWebMapServiceType._EWMS_WMS)
                    SetGCS(strGCS);
                else if (m_WMSTypeSelectedLayer == DNEWebMapServiceType._EWMS_WCS)
                    SetBB(boundingBox);

                SetGridCoordinateSystem(gcs);
                SetStyles(astrStyles, strStyle);
                SetTransparent(bTransparent);
            }
            ctrlBoundingBox.GroupBoxText = m_ctrlBoundingBoxGroupBoxText;
            ctrlBoundingBox.Enabled = true;

            // user select more the one wmts layer with different tile matrix set , so set each layer her tile matrix set and bounding box
            if (m_WMSTypeSelectedLayer == DNEWebMapServiceType._EWMS_WMTS && !isOpenAllLayersAsOne )
            {

                {
                    SetTileMatrixSets(strTileMatrixSets);
                    ctrlBoundingBox.Enabled = (selectLayers.Count < 2);
                    if (strTileMatrixSets == MultiSelection)
                        ctrlBoundingBox.GroupBoxText = m_ctrlBoundingBoxGroupBoxText + " - " + MultiSelection;
                }
               // if(strImageFormat == MultiSelection)
                {
                    SetImageFormat(astrImageFormats, strImageFormat);
                }
               // if(strStyle == MultiSelection)
                {
                    SetStyles(astrStyles, strStyle);
                }
            }

            m_CurrNumSelected = selectLayers.Count;
        }

        private void cmbWMSCoordinateSystem_TextUpdate(object sender, EventArgs e)
        {
            cmbWMSCoordinateSystem_SelectedIndexChanged(null, null);
        }

        // if user select more the one wmts layer with different tile matrix set , so set each layer her tile matrix set and bounding box
        public static string CheckWMTSTileMatrix(List<MCTWebServerUserSelection> selectLayers)
        {
            DNSTileMatrixSet[] aTileMatrixSets = selectLayers[0].ServerLayerInfo.aTileMatrixSets;
            for (int j = 0; j < aTileMatrixSets.Length; j++)
            {
                int counter = selectLayers.Count(y => y.ServerLayerInfo.aTileMatrixSets.Any(x => x.strName == aTileMatrixSets[j].strName));
                
                if (counter == selectLayers.Count)
                {
                    return aTileMatrixSets[j].strName;
                }
            }
            return MultiSelection;
        }

        public static string CheckWMTSImageFormat(List<MCTWebServerUserSelection> selectLayers)
        {
            string[] astrImageFormats = selectLayers[0].ServerLayerInfo.astrImageFormats;
            for (int j = 0; j < astrImageFormats.Length; j++)
            {
                int counter = selectLayers.Count(y => y.ServerLayerInfo.astrImageFormats.Any(x => x == astrImageFormats[j]));

                if (counter == selectLayers.Count)
                {
                    return astrImageFormats[j];
                }
            }
            return MultiSelection;
        }

        public static string CheckWMTSStyle(List<MCTWebServerUserSelection> selectLayers)
        {
            string[] astrStyles = selectLayers[0].ServerLayerInfo.astrStyles;
            for (int j = 0; j < astrStyles.Length; j++)
            {
                int counter = selectLayers.Count(y => y.ServerLayerInfo.astrStyles.Any(x => x == astrStyles[j]));

                /* for (int i = 0; i < selectLayers.Count; i++)
                {
                    if (selectLayers[i].ServerLayerInfo.astrStyles.Any(x => x == astrStyles[j]))
                        counter++;
                }*/
                if (counter == selectLayers.Count)
                {
                    return astrStyles[j];
                }
            }
            return MultiSelection;
        }

    }
}
