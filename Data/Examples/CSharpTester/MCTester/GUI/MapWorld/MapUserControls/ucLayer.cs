using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using MapCore.Common;
using MCTester.Managers;
using MCTester.GUI.Trees;
using MCTester.General_Forms;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucLayer : UserControl,IUserControlItem
    {
        private IDNMcMapLayer m_CurrentObject;
        
        private List<string> m_clstViewportText = new List<string>();
        private List<IDNMcMapViewport> m_clstViewportValue = new List<IDNMcMapViewport>();

        private List<string> m_lstTerrainText = new List<string>();
        private List<IDNMcMapTerrain> m_lstTerrainValue = new List<IDNMcMapTerrain>();

        private bool m_isLoadItem;
        public ucLayer()
        {
            InitializeComponent();
            
            clstViewports.DisplayMember = "ViewportsTextList";
            clstViewports.ValueMember = "ViewportsValueList";

            lstTerrains.DisplayMember = "TerrainsTextList";
            lstTerrains.ValueMember = "TerrainsValueList";

            m_clstViewportText = new List<string>();
            m_lstTerrainText = new List<string>();
        }

        public void SetSecondaryDTMLayer()
        {
            btnRemoveLayerAsync.Visible = false;
            btnReplaceNativeServerLayerAsync.Visible = false;
            gbLayerVisibility.Visible = false;
        }

        #region IUserControlItem Members
        public void LoadItem(object aItem)
        {
            m_isLoadItem = true;
            m_CurrentObject = (IDNMcMapLayer)aItem;
            LoadViewportsToListBox();

            if (MainForm.GetMapWorldTreeViewForm() != null)
            {
                object GrandfatherInTree = MainForm.GetMapWorldTreeViewForm().GrandfatherOfSelectedNode;
                if (GrandfatherInTree != null && GrandfatherInTree is IDNMcMapViewport)
                    ctrlBoundingBox.SetPointData((IDNMcMapViewport)GrandfatherInTree, true);
                else
                {
                    object fatherInTree = MainForm.GetMapWorldTreeViewForm().ParentOfSelectedNode;

                    if (fatherInTree != null && fatherInTree is IDNMcMapViewport)
                        ctrlBoundingBox.SetPointData((IDNMcMapViewport)fatherInTree, true);
                    else
                        ctrlBoundingBox.SetPointData(null, true);
                }
            }
            else
                ctrlBoundingBox.SetPointData(null, true);

            try
            {
                ctrlGridCoordinateSystemDetails1.LoadData(m_CurrentObject.CoordinateSystem);
                ctrlGridCoordinateSystemDetails1.SetText(true);
            }
            catch (MapCoreException McEx)
            {
                if (McEx.ErrorCode == DNEMcErrorCode.NOT_INITIALIZED)
                    ctrlGridCoordinateSystemDetails1.SetText(false);
                else
                    Utilities.ShowErrorMessage("GridCoordinateSystem", McEx);
            }

            try
            {
                ctrlBoundingBox.SetBoxValue(m_CurrentObject.BoundingBox);
                ctrlBoundingBox.GroupBoxText = "World Bounding Box";
            }
            catch (MapCoreException McEx)
            {
                if (McEx.ErrorCode == DNEMcErrorCode.NOT_INITIALIZED)
                {
                    ctrlBoundingBox.Text = "World Bounding Box (not initialized)";
                    //MessageBox.Show("This is delayed load layer - can not get bounding box", "Get Bounding Box");
                }
                else
                    Utilities.ShowErrorMessage("BoundingBox", McEx);
            }

            try
            {
                ntxLayerID.SetUInt32(m_CurrentObject.GetID());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ID", McEx);
            }

            try
            {
                ntxLayerType.Text = m_CurrentObject.LayerType.ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("LayerType", McEx);
            }

            try
            {
                IDNMcUserData UD = m_CurrentObject.GetUserData();
                if (UD != null)
                {
                    TesterUserData TUD = (TesterUserData)UD;
                    ctrlUserData.UserDataByte = TUD.UserDataBuffer;
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetUserData", McEx);
            }

            if (MCTMapDevice.IsInitilizeDeviceLocalCache())
            {
                try
                {
                    DNSLocalCacheLayerParams localCacheLayerParams = m_CurrentObject.GetLocalCacheLayerParams();
                    ctrlLocalCacheParams.SetLocalCacheLayerParams(localCacheLayerParams);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetLocalCacheLayerParams", McEx);
                }
            }

            try
            {
                uint index = m_CurrentObject.GetBackgroundThreadIndex();
                if(index != DNMcConstants._MC_EMPTY_ID)
                    ntbGetBackgroundThreadIndex.SetUInt32(index);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetBackgroundThreadIndex", McEx);
            }

            try
            {
                cbIsInitialized.Checked = m_CurrentObject.IsInitialized();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IsInitialized", McEx);
            }

            try
            {
                ntxNumTilesInNativeServerRequest.SetUInt32(m_CurrentObject.GetNumTilesInNativeServerRequest());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetNumTilesInNativeServerRequest", McEx);
            }

            m_isLoadItem = false; 
        }

        public void SetLocalCacheEnabled(bool isEnabled)
        {
            ctrlLocalCacheParams.Enabled = isEnabled;
        }

        protected virtual void SaveItem()
        {
            try
            {
                m_CurrentObject.SetID(ntxLayerID.GetUInt32());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ntxLayerID", McEx);
            }

            try
            {
                TesterUserData TUD = null;
                if (ctrlUserData.UserDataByte != null && ctrlUserData.UserDataByte.Length != 0)
                    TUD = new TesterUserData(ctrlUserData.UserDataByte);

                m_CurrentObject.SetUserData(TUD);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetUserData", McEx);
            }

        }
        #endregion

        private void LoadViewportsToListBox()
        {
            Dictionary<object, uint> dViewports = Manager_MCViewports.AllParams;

            clstViewports.Items.Clear();
            ViewportsTextList.Clear();
            m_clstViewportValue.Clear();
            foreach (IDNMcMapViewport vp in dViewports.Keys)
            {
                ViewportsTextList.Add(Manager_MCNames.GetNameByObject(vp,"Viewport"));
                m_clstViewportValue.Add(vp);
            }

            clstViewports.Items.AddRange(ViewportsTextList.ToArray());

            try
            {
                for (int i = 0; i < GetViewportsValueList().Count; i++ )
                {
                    clstViewports.SetItemChecked(i, m_CurrentObject.GetVisibility(GetViewportsValueList()[i]));
                }

                // get default visibility
                chxDefaultVisibility.Checked = m_CurrentObject.GetVisibility();

                LoadTerrainsToList();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVisibility", McEx);
            }
        }

        #region Events
        private void btnLayerOK_Click(object sender, EventArgs e)
        {
            SaveItem();
        }

        #endregion

        #region Public Properties

        public List<string> ViewportsTextList
        {
            get { return m_clstViewportText; }
            set { m_clstViewportText = value; }
        }

        public List<IDNMcMapViewport> GetViewportsValueList()
        {
            return m_clstViewportValue;
        }

        public void SetViewportsValueList(List<IDNMcMapViewport> value)
        {
            m_clstViewportValue = value; 
        }

        public List<string> GetTerrainsTextList()
        {
            return m_lstTerrainText; 
        }

        public void SetTerrainsTextList(List<string> value)
        {
            m_lstTerrainText = value; 
        }

        public List<IDNMcMapTerrain> GetTerrainsValueList()
        {
            return m_lstTerrainValue;
        }

        public void SetViewportsValueList(List<IDNMcMapTerrain> value)
        {
            m_lstTerrainValue = value;
        }

        #endregion  

        private void clstViewports_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTerrainsToList();
        }

        private void LoadTerrainsToList()
        {
            lstTerrains.Items.Clear();
            m_lstTerrainText.Clear();
            m_lstTerrainValue.Clear();

            chxLayerEffectiveVisibility.CheckState = CheckState.Indeterminate;

            if (clstViewports.SelectedIndex != -1)
            {
                
                IDNMcMapTerrain[] terrains = m_clstViewportValue[clstViewports.SelectedIndex].GetTerrains();

                foreach (IDNMcMapTerrain terr in terrains)
                {
                    m_lstTerrainText.Add(Manager_MCNames.GetNameByObject(terr, "Terrain"));
                    m_lstTerrainValue.Add(terr);
                }

                lstTerrains.Items.AddRange(m_lstTerrainText.ToArray());
            }
        }

        private void lstTerrains_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstTerrains.SelectedIndex != -1 && clstViewports.SelectedIndex != -1)
                {
                    chxLayerEffectiveVisibility.Checked = m_CurrentObject.GetEffectiveVisibility(m_clstViewportValue[clstViewports.SelectedIndex], m_lstTerrainValue[lstTerrains.SelectedIndex]);
                    chxLayerEffectiveVisibility.CheckState = chxLayerEffectiveVisibility.Checked ? CheckState.Checked : CheckState.Unchecked;
                }                
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetEffectiveVisibility", McEx);
            }
        }

        private void clstViewports_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                m_CurrentObject.SetVisibility(!clstViewports.GetItemChecked(e.Index), m_clstViewportValue[e.Index]);
              //  
                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetVisibility", McEx);
            }
        }

        private void chxDefaultVisibility_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_isLoadItem)
                {
                    m_CurrentObject.SetVisibility(chxDefaultVisibility.Checked);
                    LoadViewportsToListBox();
                    // turn on all viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetVisibility", McEx);
            }
        }

        private void btnReplaceNativeServerLayerAsync_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.ReplaceNativeServerLayerAsync(MCTMapLayerReadCallback.getInstance());
                BuildTree();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ReplaceNativeServerLayerAsync", McEx);
            }
        }

        private void BuildTree()
        {
            Form ContainerForm = GetParentForm(this);

            // close the form only in case that this is creation of new selector

            if ((ContainerForm is MCMapWorldTreeViewForm))
                ((MCMapWorldTreeViewForm)ContainerForm).BuildTree();
        }

        private Form GetParentForm(Control ctr)
        {
            if (ctr.Parent is Form)
                return ctr.Parent as Form;
            else
                return GetParentForm(ctr.Parent);
        }

        private void btnRemoveLayerAsync_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.RemoveLayerAsync();
                BuildTree();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("RemoveLayerAsync", McEx);
            }
        }

        private void btnGetCreateLayerParams_Click(object sender, EventArgs e)
        {
            frmLayerParams frmLayerParams1 = new frmLayerParams(m_CurrentObject);
            frmLayerParams1.Show();
        }

        private void btnSetNumTilesInNativeServerRequest_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetNumTilesInNativeServerRequest(ntxNumTilesInNativeServerRequest.GetUInt32());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetNumTilesInNativeServerRequest", McEx);
            }
        }

        private void tpGeneral_Enter(object sender, EventArgs e)
        {
            ctrlBoundingBox.CheckBBObject();

        }

        private void tpGeneral_Leave(object sender, EventArgs e)
        {
            ctrlBoundingBox.RemoveBBObject();   

        }

        internal void DeleteTempObjects()
        {
            tpGeneral_Leave(null, null);
        }
    }
}
