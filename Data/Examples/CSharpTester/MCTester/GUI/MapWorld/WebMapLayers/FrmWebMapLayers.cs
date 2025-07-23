using MapCore;
using MCTester.ButtonsImplementation;
using MCTester.Controls;
using MCTester.Managers;
using MCTester.Managers.MapWorld;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnmanagedWrapper;
using static MCTester.Managers.MapWorld.Manager_MCLayers;

namespace MCTester.MapWorld.WebMapLayers
{
    public partial class FrmWebMapLayers : Form, IWebLayerRequest
    {
        private int mColIndexCheckBoxGroup = 0;
        private int mColIndexTextGroup = 1;
        private int mColIndexCheckBoxLayer = 2;
        private int mColIndexLayerType = 5;
        private int mColIndexMetadata = 9;

        private bool mIsInCellValueChanged = false;
        private bool mIsLoadForm = false;
        private List<MCTWebServerUserSelection> mUserSelectLayers;
        private List<MCTWebServerUserSelection> mSelectLayers;
        private List<DNSServerLayerInfo> mLayers;

        private bool mOpenAllLayersAsOne = false;
        private DNEWebMapServiceType mWebMapServiceType;
        private string mUrlServer;
        private string mServiceProviderName;
        private DNSServerLayerInfo[] aInitLayers;
        private DNEMcErrorCode mStatus;
        private bool mIsRaster = true;
        private DNSWebMapServiceParams mWebMapServiceParams;
        private MCTRaw3DModelParams mRaw3DModelParams;
        private bool mCheckBoxGroupValue = false;

        private IUserSelectServerLayers mParentForm;

        public bool OpenAllLayersAsOne
        {
            get { return mOpenAllLayersAsOne; }
            set { mOpenAllLayersAsOne = value; }
        }

        public bool IsRaster
        {
            get { return mIsRaster; }
            set { mIsRaster = value; }
        }

        public FrmWebMapLayers()
        {
            mIsLoadForm = true;
            InitializeComponent();
            mIsLoadForm = false;

        }

        public FrmWebMapLayers(List<MCTWebServerUserSelection> userSelectLayers, DNEWebMapServiceType eWebMapServiceType, IUserSelectServerLayers parentForm = null) : this()
        {
            mUserSelectLayers = userSelectLayers;
            if (mUserSelectLayers == null)
                mUserSelectLayers = new List<MCTWebServerUserSelection>();
            mWebMapServiceType = eWebMapServiceType;
            ctrlWebServiceLayerParams1.SetType(eWebMapServiceType);

            mParentForm = parentForm;

            ctrlWebServiceLayerParams1.setCtrlNonNativeParams(ctrlNonNativeParams1);

            if (mWebMapServiceType == DNEWebMapServiceType._EWMS_MAPCORE)
            {
                this.Size = new Size(935, 410);
                ctrlNonNativeParams1.Visible = false;
                ctrlWebServiceLayerParams1.Visible = false;
                chxOpenAllLayersAsOne.Visible = false;
                gbSelectLayerType.Visible = false;
            }
            else
            {
                this.Size = new Size(935, 900);
                ctrlNonNativeParams1.Visible = true;
                ctrlWebServiceLayerParams1.Visible = true;
                chxOpenAllLayersAsOne.Visible = (mWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS || mWebMapServiceType == DNEWebMapServiceType._EWMS_WMS);
                gbSelectLayerType.Visible = mWebMapServiceType == DNEWebMapServiceType._EWMS_WCS;
                rbRaster.Checked = IsRaster;
                rbDTM.Checked = !IsRaster;
                if (mWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS)
                    dgvWebLayers.Columns[6].HeaderText = "Tile Matrix Set";
            }

        }

        private void FrmWebMapLayers_Load(object sender, EventArgs e)
        {
            
        }

        public void SetWebServerLayers(DNEMcErrorCode eStatus, string strServerURL, /*DNEWebMapServiceType eWebMapServiceType,*/
            DNSWebMapServiceParams webMapServiceParams, DNSServerLayerInfo[] aLayers, string[] astrServiceMetadataURLs, 
            string urlServer, string strServiceProviderName, bool isOpenAsOne, bool IsRaster, bool firstSet = true, 
            MCTRaw3DModelParams raw3DModelParams = null, Dictionary<string, MCTServerSelectLayersData> WMTSFromCSWLayersData = null)
        {
            mIsLoadForm = true;
            dgvWebLayers.Rows.Clear();
            
            mWebMapServiceParams = webMapServiceParams;
            mRaw3DModelParams = raw3DModelParams;
            mUrlServer = urlServer;
            mServiceProviderName = strServiceProviderName;
            mDicWMTSFromCSWLayersData = WMTSFromCSWLayersData;

            if (firstSet)
            {
                aInitLayers = aLayers;
                mStatus = eStatus;
            }
            if(strServiceProviderName != "")
                this.Text = strServiceProviderName;
            if (eStatus == DNEMcErrorCode.SUCCESS)
            {
                if (aLayers != null && aLayers.Length > 0)
                {
                    mLayers = aLayers.ToList();
                    int index = 0;
                    bool allGroupsSelected = false;

                    // count num layers exists
                    int numCounts = 0;
                    foreach (DNSServerLayerInfo serverLayerInfo in mLayers)
                    {
                        if (serverLayerInfo.astrGroups != null)
                            numCounts += serverLayerInfo.astrGroups.Length;
                    }

                    if (mUserSelectLayers != null && numCounts == mUserSelectLayers.Count && numCounts > 0)
                        allGroupsSelected = true;


                    var arrGroupNames = mLayers.Select(x => x.astrGroups);
                    List<string> groupNames = new List<string>();
                    foreach (string[] tempGroupNames in arrGroupNames)
                    {
                        foreach (string groupName in tempGroupNames)
                            if (!groupNames.Contains(groupName))
                                groupNames.Add(groupName);
                    }

                    foreach (string groupName in groupNames)
                    {
                        dgvWebLayers.Rows.Add(0, groupName);
                        dgvWebLayers.Rows[index].DataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                        dgvWebLayers.Rows[index].Cells[mColIndexCheckBoxLayer] = new DataGridViewTextBoxCell();
                        dgvWebLayers.Rows[index].Cells[mColIndexCheckBoxLayer].Value = "";
                        dgvWebLayers.Rows[index].Cells[mColIndexCheckBoxLayer].ReadOnly = true;
                        index++;

                        List<DNSServerLayerInfo> layerInfosOfGroup = mLayers.FindAll(x => x.astrGroups.Contains(groupName));

                        foreach (var layer in layerInfosOfGroup)
                        {
                            AddLayerToGrid(index, layer, groupName);
                            index++;
                        }
                    }
                    List<DNSServerLayerInfo> layerInfosWithoutGroup = mLayers.FindAll(x => x.astrGroups.Length == 0);
                    foreach (DNSServerLayerInfo layerInfo in layerInfosWithoutGroup)
                    {
                        AddLayerToGrid(index, layerInfo, "");
                        index++;
                    }

                    // if return layers without groups and all layers selected
                    if ( (!allGroupsSelected) && mUserSelectLayers != null && layerInfosWithoutGroup.Count > 0 && dgvWebLayers.Rows.Count == layerInfosWithoutGroup.Count && layerInfosWithoutGroup.Count == mUserSelectLayers.Count)
                        allGroupsSelected = true;
                
                    chxSelectAll.Checked = allGroupsSelected;
                    SaveSelectedLayers();

                    // if WMTS_SERVER_URL type change ui
                    if (IsWmtsServerUrl())
                    {
                        dgvWebLayers.Columns[mColIndexTextGroup].HeaderText = "Selected Layers from WMTS Server";
                    }
                }
                if (mWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS || mWebMapServiceType == DNEWebMapServiceType._EWMS_WCS || mWebMapServiceType == DNEWebMapServiceType._EWMS_WMS)
                {
                    ctrlNonNativeParams1.SetNonNativeParamsFromWeb(mWebMapServiceParams);
                    ctrlWebServiceLayerParams1.SetFromWeb(urlServer, mWebMapServiceType, mWebMapServiceParams);
                }
                else if (mWebMapServiceType == DNEWebMapServiceType._EWMS_CSW)
                {
                    ctrlWebServiceLayerParams1.SetParams(mRaw3DModelParams);
                }

                mIsLoadForm = false;

                chxOpenAllLayersAsOne.Checked = isOpenAsOne;
                if (mWebMapServiceType == DNEWebMapServiceType._EWMS_CSW)
                {
                    ctrlNonNativeParams1.SetUI(true);
                }
            }
            else
            {

            }

            if (mWebMapServiceType == DNEWebMapServiceType._EWMS_CSW && IsWmtsServerUrl())
            {
                ctrlWebServiceLayerParams1.Visible = false;
                 ctrlNonNativeParams1.SetTilingSchemeVisiblity(false);
            }
        }

        private void AddLayerToGrid(int index, DNSServerLayerInfo layer, string groupName)
        {
            string strCoordinateSystem = GetGCSStr(layer);

            DNSMcBox boundingBox = GetBB(layer);
            string strBoundingBox = GetBBStr(boundingBox);

            string strMetadata = "", strMetadataToolTip ="";
            if (layer.aMetadataValues != null && layer.aMetadataValues.Length > 0)
            {
                foreach (DNSMcKeyStringValue keyStringValue in layer.aMetadataValues)
                {
                    string str = "(" + keyStringValue.strKey + "," + keyStringValue.strValue + "),";
                    strMetadata += str;
                    strMetadataToolTip += str + "\n";
                }
                strMetadata.Substring(0, strMetadata.Length - 1);
            }
            string wmtsLayers = "" ;
            if(groupName == "" && layer.strLayerType == Manager_MCLayers.WMTSFromCSWType && mDicWMTSFromCSWLayersData.ContainsKey(layer.strLayerId))
            {
                MCTServerSelectLayersData serverSelectLayersData = mDicWMTSFromCSWLayersData[layer.strLayerId];
                if (serverSelectLayersData != null )
                {
                    wmtsLayers = WMTSLayersAsText(serverSelectLayersData.SelectedLayers);
                }
            }

            dgvWebLayers.Rows.Add(false, groupName == "" ? wmtsLayers : "", 0, layer.strLayerId, layer.strTitle,
                layer.strLayerType.Replace("MapCoreServer", ""),
                strCoordinateSystem,
                strBoundingBox,
                layer.nDrawPriority,
                strMetadata);

            dgvWebLayers.Rows[index].Tag = layer.strLayerId;
            dgvWebLayers[mColIndexTextGroup, index].Tag = groupName;
            dgvWebLayers[mColIndexMetadata, index].ToolTipText = strMetadataToolTip;

            if (mUserSelectLayers != null && mUserSelectLayers.Any(x => (x.ServerLayerInfo.strLayerId == layer.strLayerId) && x.GroupName == groupName))
                dgvWebLayers.Rows[index].Cells[mColIndexCheckBoxLayer].Value = true;
            else
                dgvWebLayers.Rows[index].Cells[mColIndexCheckBoxLayer].Value = false;

            dgvWebLayers.Rows[index].Cells[mColIndexCheckBoxGroup] = new DataGridViewTextBoxCell();
            dgvWebLayers.Rows[index].Cells[mColIndexCheckBoxGroup].Value = "";
            dgvWebLayers.Rows[index].Cells[mColIndexCheckBoxGroup].ReadOnly = true;
        }

        private new void Refresh()
        {
            SetWebServerLayers(mStatus, "", mWebMapServiceParams, aInitLayers.ToArray(), null, mUrlServer, mServiceProviderName, chxOpenAllLayersAsOne.Checked, mIsRaster, false, mRaw3DModelParams);
            txtSearch.Text = "";
        }

        public List<MCTWebServerUserSelection> SelectLayers
        {
            get { return mSelectLayers; }
            set { mSelectLayers = value; }
        }

        private Dictionary<string, MCTServerSelectLayersData> mDicWMTSFromCSWLayersData = new Dictionary<string, MCTServerSelectLayersData>();

        public Dictionary<string, MCTServerSelectLayersData> WMTSFromCSWLayersData { get => mDicWMTSFromCSWLayersData; set => mDicWMTSFromCSWLayersData = value; }

        bool mIsInSave = false;

        private void btnOpenSelectedLayers_Click(object sender, EventArgs e)
        {
            mIsInSave = true;

            SaveSelectedLayers();
            if(mUserSelectLayers.Count>0)
            {
                foreach(MCTWebServerUserSelection serverUserSelection in mUserSelectLayers)
                {
                    if(!SelectLayers.Any(x=> x.GroupName == serverUserSelection.GroupName && x.ServerLayerInfo.strLayerId == serverUserSelection.ServerLayerInfo.strLayerId))
                    {
                        SelectLayers.Add(serverUserSelection);
                    }
                }
            }
            mWebMapServiceParams = new DNSWMTSParams();
            if (mWebMapServiceType == DNEWebMapServiceType._EWMS_WCS)
                mWebMapServiceParams = new DNSWCSParams();
            if (mWebMapServiceType == DNEWebMapServiceType._EWMS_WMS)
                mWebMapServiceParams = new DNSWMSParams();

            ctrlNonNativeParams1.GetNonNativeParams(mWebMapServiceParams);

            if (mWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS)
            {
                mWebMapServiceParams = ctrlWebServiceLayerParams1.GetWMTSParams((DNSWMTSParams)mWebMapServiceParams);
            }
            else if (mWebMapServiceType == DNEWebMapServiceType._EWMS_WCS)
            {
                mWebMapServiceParams = ctrlWebServiceLayerParams1.GetWCSParams((DNSWCSParams)mWebMapServiceParams);
            }
            else if (mWebMapServiceType == DNEWebMapServiceType._EWMS_WMS)
            {
                mWebMapServiceParams = ctrlWebServiceLayerParams1.GetWMSParams((DNSWMSParams)mWebMapServiceParams);
            }
            else if (mWebMapServiceType == DNEWebMapServiceType._EWMS_CSW)
            {
                mRaw3DModelParams = ctrlWebServiceLayerParams1.GetCSWParams();
            }
            mOpenAllLayersAsOne = chxOpenAllLayersAsOne.Checked;
            DialogResult = DialogResult.OK;

            if (IsWmtsServerUrl() && mParentForm != null)
                mParentForm.AfterUserSelectServerLayers(mUrlServer, mWebMapServiceType);
            
            this.Close();

            mIsInSave = false;  
        }


        private void SaveSelectedLayers()
        {
            List<string> selectedGroups = new List<string>();  // list of selected group name's
            List<string> selectedLayers = new List<string>();  // list of selected id's
            List<MCTWebServerUserSelection> selectedLayers2 = new List<MCTWebServerUserSelection>();  // list of selected id's
            // get selected groups
            for (int i = 0; i < dgvWebLayers.RowCount; i++)
            {
                DataGridViewCell gridViewGroupCell = dgvWebLayers[mColIndexCheckBoxGroup, i];
                DataGridViewCell gridViewLayer = dgvWebLayers[mColIndexCheckBoxLayer, i];
                if (gridViewGroupCell is DataGridViewCheckBoxCell)  // line of group
                {
                    if (gridViewGroupCell.Value is bool && (bool)gridViewGroupCell.Value)
                    {
                        selectedGroups.Add(dgvWebLayers[mColIndexTextGroup, i].Value.ToString());
                    }
                    else                                            // get selected layers
                    {
                        for (int j = i + 1; j < dgvWebLayers.RowCount; j++)
                        {
                            DataGridViewCell gridViewLayerCell = dgvWebLayers[mColIndexCheckBoxLayer, j];
                            if (gridViewLayerCell is DataGridViewCheckBoxCell)
                            {
                                if (gridViewLayerCell.Value is bool && (bool)gridViewLayerCell.Value)
                                {
                                    string layerId = dgvWebLayers.Rows[j].Tag.ToString();
                                    string groupName = dgvWebLayers[mColIndexTextGroup, i].Value.ToString();
                                    selectedLayers.Add(layerId);

                                    MCTWebServerUserSelection webServerUserSelection = new MCTWebServerUserSelection();
                                    webServerUserSelection.GroupName = groupName;
                                    webServerUserSelection.ServerLayerInfo = mLayers.Find(x =>( x.strLayerId == layerId));
                                    selectedLayers2.Add(webServerUserSelection);
                                }
                              
                            }
                            else
                                break;
                        }
                    }
                }
                else if(gridViewLayer is DataGridViewCheckBoxCell)   // layer without group
                {
                    if (gridViewLayer.Value is bool && (bool)gridViewLayer.Value)
                    {
                        string layerId = dgvWebLayers.Rows[i].Tag.ToString();
                        string groupName = (string)dgvWebLayers[mColIndexTextGroup, i].Tag;  //dgvWebLayers[mColIndexTextGroup, i].Value.ToString();
                        if (IsWmtsServerUrl(i) || groupName == "")
                        {
                            selectedLayers.Add(layerId);

                            MCTWebServerUserSelection webServerUserSelection = new MCTWebServerUserSelection();
                            webServerUserSelection.GroupName = groupName;
                            webServerUserSelection.ServerLayerInfo = mLayers.Find(x => (x.strLayerId == layerId));
                            selectedLayers2.Add(webServerUserSelection);
                        }
                    }
                }
            }

            mSelectLayers = new List<MCTWebServerUserSelection>();

            foreach (string groupName in selectedGroups)
            {
                List<DNSServerLayerInfo> layers = mLayers.FindAll(x => x.astrGroups.Contains(groupName));
                foreach (DNSServerLayerInfo layer in layers)
                {
                    MCTWebServerUserSelection webServerUserSelection = new MCTWebServerUserSelection();
                    webServerUserSelection.GroupName = groupName;
                    webServerUserSelection.ServerLayerInfo = layer;

                    mSelectLayers.Add(webServerUserSelection);
                }
            }

            mSelectLayers.AddRange(selectedLayers2);
            mIsRaster = rbRaster.Checked;

            UpdateForm();
        }

       int m_prevSelectedRow = -1;

        private void dgvWebLayers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvWebLayers.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        public void GetWebServerLayers(DNEMcErrorCode eStatus, string strServerURL, DNEWebMapServiceType eWebMapServiceType, DNSServerLayerInfo[] aLayers, string[] astrServiceMetadataURLs, string UrlServer, string strServiceProviderName)
        {
            bool WMTSOpenAllLayersAsOne = false;
            DNSWMTSParams SWMTSFromCSWParams = new DNSWMTSParams();
 
            if (eStatus == DNEMcErrorCode.SUCCESS)
            {
                MCTServerSelectLayersData serverSelectLayersData1 = new MCTServerSelectLayersData();
                List<MCTWebServerUserSelection> mServerSelectLayers = null;
                if (mDicWMTSFromCSWLayersData.ContainsKey(strServerURL))
                {
                    serverSelectLayersData1 = mDicWMTSFromCSWLayersData[strServerURL];
                    mServerSelectLayers = serverSelectLayersData1.SelectedLayers;
                    WMTSOpenAllLayersAsOne = serverSelectLayersData1.OpenAllLayersAsOne;
                    SWMTSFromCSWParams = (DNSWMTSParams)serverSelectLayersData1.WebMapServiceParams;
                }

                FrmWebMapLayers mFrmWebMapLayers = new FrmWebMapLayers(mServerSelectLayers, eWebMapServiceType);
                SWMTSFromCSWParams.aRequestParams = mRaw3DModelParams.aRequestParams;
                mFrmWebMapLayers.SetWebServerLayers(eStatus, strServerURL, SWMTSFromCSWParams,
                    aLayers, astrServiceMetadataURLs, strServerURL, strServiceProviderName, WMTSOpenAllLayersAsOne, false, true, null);

                mFrmWebMapLayers.ShowDialog();
                this.Show();                                                    
                if (mFrmWebMapLayers.DialogResult == DialogResult.OK && mFrmWebMapLayers.SelectLayers.Count > 0)
                {
                    mServerSelectLayers = mFrmWebMapLayers.SelectLayers;

                    string selectedLayers = WMTSLayersAsText(mServerSelectLayers);
                    
                    dgvWebLayers[mColIndexTextGroup, m_prevSelectedRow].Value = selectedLayers;

                    if (mDicWMTSFromCSWLayersData.ContainsKey(strServerURL))
                        mDicWMTSFromCSWLayersData.Remove(strServerURL);

                    MCTServerSelectLayersData serverSelectLayersData = new MCTServerSelectLayersData();
                    serverSelectLayersData.WebMapServiceParams = (DNSWMTSParams)mFrmWebMapLayers.GetWebMapServiceParams();
                    serverSelectLayersData.SelectedLayers = mFrmWebMapLayers.SelectLayers;
                    serverSelectLayersData.WebMapServiceType = eWebMapServiceType;
                    serverSelectLayersData.OpenAllLayersAsOne = mFrmWebMapLayers.OpenAllLayersAsOne;
                    mDicWMTSFromCSWLayersData.Add(strServerURL, serverSelectLayersData);
                }
            }
            else
            {
                MessageBox.Show("Return status : " + eStatus.ToString() + ".", "Get Web Server Layers");

            }
        }

        private string WMTSLayersAsText(List<MCTWebServerUserSelection> serverSelectLayers)
        {
            string selectedLayers = "";
            if (serverSelectLayers != null && serverSelectLayers.Count > 0)
            {
                selectedLayers = serverSelectLayers.Count.ToString() + " : ";
                foreach (MCTWebServerUserSelection selectLayer in serverSelectLayers)
                {
                    selectedLayers += selectLayer.ServerLayerInfo.strLayerId + " ,";
                }
                selectedLayers = selectedLayers.TrimEnd(',');
            }
            return selectedLayers;  
        }

        private void dgvWebLayers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           // bool isAllGroupsChecked = true;
            if (!mIsInCellValueChanged)
            {
                mIsInCellValueChanged = true;
                mCheckBoxGroupValue = false;
                if ((e.RowIndex >= 0) && (e.ColumnIndex == mColIndexCheckBoxGroup))
                {
                    DataGridViewCell gridViewCell = dgvWebLayers[e.ColumnIndex, e.RowIndex];
                    if (gridViewCell is DataGridViewCheckBoxCell)
                    {
                        DataGridViewCheckBoxCell checkBoxCell = gridViewCell as DataGridViewCheckBoxCell;
                        mCheckBoxGroupValue = (bool)checkBoxCell.Value;

                        string groupName = dgvWebLayers[mColIndexTextGroup, e.RowIndex].Value.ToString();
                        
                        for (int i = e.RowIndex + 1; i < dgvWebLayers.RowCount; i++)
                        {
                            if (dgvWebLayers.Rows[i].Tag != null)   // the row is layer,  Tag == layerId
                            {
                                DataGridViewCell gridViewLayerCell = dgvWebLayers[mColIndexCheckBoxLayer, i];
                                if (gridViewLayerCell is DataGridViewCheckBoxCell)
                                {
                                    gridViewLayerCell.Value = mCheckBoxGroupValue;
                                    CheckIfLayerUnselected(i, mCheckBoxGroupValue);
                                }
                            }
                            else
                                break;
                        }
                        CheckSelectAll(mCheckBoxGroupValue);
                    }
                }
                if ((e.RowIndex >= 0) && (e.ColumnIndex == mColIndexCheckBoxLayer))
                {
                    DataGridViewCell gridViewCell = dgvWebLayers[e.ColumnIndex, e.RowIndex];
                    if (gridViewCell is DataGridViewCheckBoxCell)
                    {
                        DataGridViewCheckBoxCell checkBoxCell = gridViewCell as DataGridViewCheckBoxCell;
                        bool checkBoxLayerValue = (bool)checkBoxCell.Value;
                        CheckIfLayerUnselected(e.RowIndex, checkBoxLayerValue);

                        string groupName = dgvWebLayers[mColIndexTextGroup, e.RowIndex].Value.ToString();
                        if (checkBoxLayerValue == false)   // unchecked group value
                        {
                            int i = e.RowIndex - 1;
                            for (; i >= 0; i--)
                            {
                                DataGridViewCell gridViewGroupCell = dgvWebLayers[mColIndexCheckBoxGroup, i];
                                if ((gridViewGroupCell is DataGridViewCheckBoxCell))
                                {
                                    gridViewGroupCell.Value = checkBoxLayerValue;
                                    CheckSelectAll(checkBoxLayerValue);
                                    break;
                                }
                            }
                            if(i == -1 ) // layer without group
                            {
                                CheckSelectAll(checkBoxLayerValue);
                            }
                        }
                        else  //if (checkBoxLayerValue == true)   // check if need to unchecked group value
                        {
                            int rowStartGroupIndex = GetStartIndexGroup(e.RowIndex);
                            int rowEndGroupIndex = GetEndIndexGroup(e.RowIndex);

                            bool isAllGroupChecked = true;
                            bool isNeedToCheckGroups = true;
                            if (rowStartGroupIndex == -1 && rowEndGroupIndex == dgvWebLayers.RowCount)  // no groups all the grid - layers without groups ( wcs, wms)
                                isNeedToCheckGroups = false;

                            if (isNeedToCheckGroups)
                            {
                                for (int i = rowStartGroupIndex + 1; i < rowEndGroupIndex; i++)
                                {
                                    DataGridViewCell gridViewLayerCell = dgvWebLayers[mColIndexCheckBoxLayer, i];
                                    if (gridViewLayerCell is DataGridViewCheckBoxCell)
                                    {
                                        if (gridViewLayerCell.Value is bool)
                                        {
                                            if (!(bool)gridViewLayerCell.Value)
                                            {
                                                isAllGroupChecked = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            isAllGroupChecked = false;
                                            break;
                                        }
                                    }
                                }
                                if (isAllGroupChecked)
                                {
                                    DataGridViewCell gridViewGroupCell = dgvWebLayers[mColIndexCheckBoxGroup, rowStartGroupIndex];
                                    if (gridViewGroupCell is DataGridViewCheckBoxCell)
                                    {
                                        gridViewGroupCell.Value = true;
                                        CheckSelectAll(true);
                                    }
                                }
                                else
                                    CheckSelectAll(false);
                            }
                            else
                            {
                                CheckSelectAll(true);
                            }

                            if (!mIsLoadForm)
                            {
                                if (IsWmtsServerUrl(e.RowIndex))  // wmts from csw 
                                {
                                    m_prevSelectedRow = e.RowIndex;
                                    this.Hide();
                                    string urlWmts = (string)dgvWebLayers.Rows[e.RowIndex].Tag;
                                    MCTServerLayersAsyncOperationCallback mctAsyncOperationCallback = new MCTServerLayersAsyncOperationCallback(this, urlWmts);

                                    DNMcMapDevice.GetWebServerLayers(urlWmts, DNEWebMapServiceType._EWMS_WMTS, ctrlWebServiceLayerParams1.GetRequestParams(), mctAsyncOperationCallback);
                                }
                            }
                        }
                    }
                }
                if (!mIsLoadForm && (e.ColumnIndex == mColIndexCheckBoxLayer || e.ColumnIndex == mColIndexCheckBoxGroup))
                {
                    SaveSelectedLayers();
                }
                mIsInCellValueChanged = false;
            }
        }

        private bool IsWmtsServerUrl(int rowIndex = 0)
        {
            if (dgvWebLayers.RowCount > rowIndex)
            {
                object cellVal = dgvWebLayers[mColIndexLayerType, rowIndex].Value;
                return (cellVal != null && cellVal.ToString() == Manager_MCLayers.WMTSFromCSWType) ? true : false;
            }
            return false;
        }

        private void CheckIfLayerUnselected(int rowIndex, bool isSelected)
        {
            if(!isSelected)
            {
                string groupName = (string)dgvWebLayers[mColIndexTextGroup, rowIndex].Tag;
                string layerId = (string)dgvWebLayers.Rows[rowIndex].Tag;
                mUserSelectLayers.RemoveAll(x => x.GroupName == groupName && x.ServerLayerInfo.strLayerId == layerId);
            }
        }

        private void CheckSelectAll(bool checkBoxGroupValue)
        {
            if (!mIsLoadForm)
            {
                bool isAllLayersChecked = true;
                if (checkBoxGroupValue)
                {
                    for (int i = 0; i < dgvWebLayers.RowCount; i++)
                    {

                        DataGridViewCell layerCell = dgvWebLayers[mColIndexCheckBoxLayer, i];
                        if (layerCell is DataGridViewCheckBoxCell)
                        {
                            DataGridViewCheckBoxCell groupCheckBoxCell = layerCell as DataGridViewCheckBoxCell;
                            if (!(bool)groupCheckBoxCell.Value)
                            {
                                isAllLayersChecked = false;
                                break;
                            }
                        }
                    }
                    if (isAllLayersChecked)
                        chxSelectAll.Checked = true;
                }
                else
                    chxSelectAll.Checked = false;
            }
        }

        private int GetStartIndexGroup(int indexRowLayer)
        {
            int rowStartGroupIndex = -1;
            for (int i = indexRowLayer - 1; i >= 0; i--)
            {
                DataGridViewCell gridViewGroupCell = dgvWebLayers[mColIndexCheckBoxGroup, i];
                if (gridViewGroupCell is DataGridViewCheckBoxCell)
                {
                    rowStartGroupIndex = i;
                    break;
                }
            }
            return rowStartGroupIndex;
        }

        private int GetEndIndexGroup(int indexRowLayer)
        {
            int rowEndGroupIndex = indexRowLayer+1;
            int i = indexRowLayer + 1;
            for (; i < dgvWebLayers.RowCount; i++)
            {
                DataGridViewCell gridViewGroupCell = dgvWebLayers[mColIndexCheckBoxGroup, i];
                if (gridViewGroupCell is DataGridViewCheckBoxCell)
                {
                    break;
                }
            }
            return i;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            SelectAll(false);
        }

        private void SelectAll(bool isSelectAll)
        {
            bool isExistGroup = false;
            for (int i = 0; i < dgvWebLayers.RowCount; i++)
            {
                DataGridViewCell gridViewGroupCell = dgvWebLayers[mColIndexCheckBoxGroup, i];
                if (gridViewGroupCell is DataGridViewCheckBoxCell)  // line of group
                {
                    isExistGroup = true;
                    gridViewGroupCell.Value = isSelectAll;
                    DataGridViewCellEventArgs cellEventArgs = new DataGridViewCellEventArgs(mColIndexCheckBoxGroup, i);
                    dgvWebLayers_CellValueChanged(dgvWebLayers, cellEventArgs);
                }
            }
            if(isExistGroup == false)  // no groups, only layers
            {
                for (int i = 0; i < dgvWebLayers.RowCount; i++)
                {
                    DataGridViewCell gridViewGroupCell = dgvWebLayers[mColIndexCheckBoxLayer, i];
                    if (gridViewGroupCell is DataGridViewCheckBoxCell)  // line of group
                    {
                        gridViewGroupCell.Value = isSelectAll;
                        DataGridViewCellEventArgs cellEventArgs = new DataGridViewCellEventArgs(mColIndexCheckBoxLayer, i);
                        dgvWebLayers_CellValueChanged(dgvWebLayers, cellEventArgs);
                    }
                }
            }
        }

        private void btnRefreshFromServer_Click(object sender, EventArgs e)
        {
            SaveSelectedLayers();
            Refresh();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SelectAll(true);
        }

        private void chxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if(!mIsInCellValueChanged)
                SelectAll(chxSelectAll.Checked);
        }

        private void pbRefreshIcon_Click(object sender, EventArgs e)
        {
            if (mIsLoadForm == false)
            {
                SaveSelectedLayers();
                Refresh();
            }
        }

        private void dgvWebLayers_Paint(object sender, PaintEventArgs e)
        {
            if (dgvWebLayers.RowCount > 0)
            {
                Rectangle rec = dgvWebLayers.GetCellDisplayRectangle(0, 0, true);
                chxSelectAll.Location = new Point(
                 rec.Left + (rec.Right - rec.Left) / 2 - chxSelectAll.AccessibilityObject.Bounds.Width / 2,
                 (rec.Bottom - rec.Top) / 2 - chxSelectAll.AccessibilityObject.Bounds.Height / 2);

                pbRefreshIcon.Size = new Size(pbRefreshIcon.Size.Width, rec.Height - 3);
            }
        }


        public DNSWebMapServiceParams GetWebMapServiceParams()
        {
            return mWebMapServiceParams;
        }

        public MCTRaw3DModelParams GetRaw3DModelParams()
        {
            return mRaw3DModelParams;
        }

        private void UpdateForm()
        {
            if (!mIsInSave && mWebMapServiceType != DNEWebMapServiceType._EWMS_MAPCORE)
            {
                ctrlWebServiceLayerParams1.UpdateForm(mSelectLayers, chxOpenAllLayersAsOne.Checked, mIsLoadForm, mCheckBoxGroupValue);
            }
        }

        private void chxOpenAllLayersAsOne_CheckedChanged(object sender, EventArgs e)
        {
            ctrlWebServiceLayerParams1.m_IsOpenAllLayersAsOneChanged = true;
            SaveSelectedLayers();
            ctrlWebServiceLayerParams1.m_IsOpenAllLayersAsOneChanged = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (aInitLayers != null && aInitLayers.Length > 0)
            {
                if (txtSearch.Text != "")
                {

                    dgvWebLayers.Rows.Clear();
                    List<DNSServerLayerInfo> searched = new List<DNSServerLayerInfo>();
                    for (int i = 0; i < aInitLayers.Length; i++)
                    {
                        if (aInitLayers[i].strTitle.ToLower().Contains(txtSearch.Text.ToLower()) || aInitLayers[i].strLayerId.ToLower().Contains(txtSearch.Text.ToLower()))
                            searched.Add(aInitLayers[i]);
                    }

                    SetWebServerLayers(mStatus, "", mWebMapServiceParams, searched.ToArray(), null, mUrlServer, mServiceProviderName, chxOpenAllLayersAsOne.Checked, mIsRaster, false, mRaw3DModelParams);
                }
                else
                {
                    Refresh();
                }
            }
        }

        private void pbSearch_Click(object sender, EventArgs e)
        {
            mUserSelectLayers.AddRange(mSelectLayers);
            btnSearch_Click(null, null);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            mUserSelectLayers.AddRange(mSelectLayers);
            btnSearch_Click(null, null);
        }
    }
}