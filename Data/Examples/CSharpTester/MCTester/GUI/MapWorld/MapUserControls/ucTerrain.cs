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
using MCTester.Managers;
using MapCore.Common;
using MCTester.GUI.Trees;
using MCTester.Controls;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucTerrain : UserControl, IUserControlItem
    {
        private IDNMcMapTerrain m_CurrentObject;
        private IDNMcMapLayer[] m_layers;
        private List<string> m_clstViewportText = new List<string>();
        private List<IDNMcMapViewport> m_clstViewportValue = new List<IDNMcMapViewport>();
        private List<IDNMcMapLayer> removedLayers = null;

        public ucTerrain()
        {
            InitializeComponent();
            lstLayers.SelectedIndexChanged += new EventHandler(lstLayers_SelectedIndexChanged);
            clstViewports.DisplayMember = "ViewportsTextList";
            clstViewports.ValueMember = "ViewportsValueList";

            object parentInTree = MainForm.GetMapWorldTreeViewForm().ParentOfSelectedNode;
            if (parentInTree != null && parentInTree is IDNMcMapViewport)
                ctrlBoundingBox.SetPointData((IDNMcMapViewport)parentInTree, true);
            else
                ctrlBoundingBox.SetPointData(null, true);
        }

        #region IUserControlItem Members

        bool m_IsInLoad;

        public void LoadItem(object aItem)
        {
            m_IsInLoad = true;
            m_CurrentObject = (IDNMcMapTerrain)aItem;

            try
            {
                ntxTerrainID.SetUInt32(m_CurrentObject.GetID());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ViewportID", McEx);
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

            Dictionary<object, uint> dViewports = Manager_MCViewports.AllParams;

            foreach (IDNMcMapViewport vp in dViewports.Keys)
            {
                ViewportsTextList.Add(Manager_MCNames.GetNameByObject(vp, "Viewport"));
                ViewportsValueList.Add(vp);
            }

            clstViewports.Items.AddRange(ViewportsTextList.ToArray());

            try
            {
                for (int i = 0; i < ViewportsValueList.Count; i++)
                {
                    clstViewports.SetItemChecked(i, m_CurrentObject.GetVisibility(ViewportsValueList[i]));
                }

                // get default visibility
                chxDefaultVisibility.Checked = m_CurrentObject.GetVisibility();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVisibility", McEx);
            }

            try
            {
                ctrlGridCoordinateSystemDetails1.LoadData(m_CurrentObject.CoordinateSystem);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CoordinateSystem", McEx);
            }

            try
            {
                if (m_CurrentObject.GetLayers().Length != 0)
                {
                    ctrlBoundingBox.SetBoxValue(m_CurrentObject.BoundingBox);
                    ctrlBoundingBox.GroupBoxText = "World Bounding Box";
                }
            }
            catch (MapCoreException McEx)
            {
                if (McEx.ErrorCode == DNEMcErrorCode.NOT_INITIALIZED)
                    ctrlBoundingBox.GroupBoxText = "World Bounding Box (not initialized)";

                // MessageBox.Show("Terrain includes delayed load layers - can not get BoundingBox", "GetBoundingBox");
                else
                    Utilities.ShowErrorMessage("BoundingBox", McEx);
            }

            try
            {
                chxDisplayItemsAttachedTo3DModelWithoutDtm.Checked = m_CurrentObject.GetDisplayItemsAttachedTo3DModelWithoutDtm();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetDisplayItemsAttachedTo3DModelWithoutDtm", McEx);
            }

            LoadLayers();

            m_IsInLoad = false;
        }

        public void SaveItem()
        {
            try
            {
                m_CurrentObject.SetID(ntxTerrainID.GetUInt32());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetID", McEx);
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

        void ctrlLayerCreator_OnItemChanged(object SelectedItem)
        {
            DNSLayerParams CurrLayerParams = m_CurrentObject.GetLayerParams((IDNMcMapLayer)SelectedItem);
            ctrlSLayerParams.LayerParams = CurrLayerParams;
        }


        #endregion

        private void btnLayerOK_Click(object sender, EventArgs e)
        {
            IDNMcMapLayer CurrLayer = null;
            if (lstLayers.SelectedIndex != -1)
            {
                CurrLayer = m_layers[lstLayers.SelectedIndex];
            }
            if (CurrLayer != null)
            {
                try
                {
                    m_CurrentObject.SetLayerParams(CurrLayer, ctrlSLayerParams.LayerParams);

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetLayerParams", McEx);
                }

            }
            else
                MessageBox.Show("You have to choose layer first!");

        }

        void lstLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstLayers.SelectedIndex >= 0 && m_layers[lstLayers.SelectedIndex] != null)
                    ctrlSLayerParams.LayerParams = m_CurrentObject.GetLayerParams(m_layers[lstLayers.SelectedIndex]);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetLayerParams", McEx);
            }
        }

        private void clstViewports_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                m_CurrentObject.SetVisibility(!clstViewports.GetItemChecked(e.Index), ViewportsValueList[e.Index]);

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
                if (!m_IsInLoad)
                    m_CurrentObject.SetVisibility(chxDefaultVisibility.Checked);

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetVisibility", McEx);
            }
        }

        #region Public Properties

        public List<string> ViewportsTextList
        {
            get { return m_clstViewportText; }
            set { m_clstViewportText = value; }
        }

        public List<IDNMcMapViewport> ViewportsValueList
        {
            get { return m_clstViewportValue; }
            set { m_clstViewportValue = value; }
        }

        #endregion        

        private void LoadLayers()
        {
            try
            {
                m_layers = m_CurrentObject.GetLayers();
                List<string> lstLayersNames = new List<string>();
                foreach (IDNMcMapLayer layer in m_layers)
                    lstLayersNames.Add(Manager_MCNames.GetNameByObject(layer));


                lstLayers.Items.Clear();
                lstLayersRemoveAdd.Items.Clear();

                lstLayers.Items.AddRange(lstLayersNames.ToArray());
                lstLayersRemoveAdd.Items.AddRange(lstLayersNames.ToArray());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetLayers", McEx);
            }
        }



        private void btnRemoveAdd_Click(object sender, EventArgs e)
        {
            if (removedLayers == null)
            {
                // get selected layers
                foreach (int index in lstLayersRemoveAdd.SelectedIndices)
                {
                    IDNMcMapLayer layer = m_layers[index];
                    try
                    {
                        m_CurrentObject.RemoveLayer(layer);
                        if (removedLayers == null)
                            removedLayers = new List<IDNMcMapLayer>();
                        removedLayers.Add(layer);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("RemoveLayer", McEx);
                    }
                }
                LoadLayers();
            }
            else
            {
                if (removedLayers != null && removedLayers.Count > 0)
                {
                    foreach (IDNMcMapLayer layer in removedLayers)
                    {
                        try
                        {
                            m_CurrentObject.AddLayer(layer);
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("AddLayer", McEx);
                        }

                    }
                    LoadLayers();

                    // selected the added layers
                    foreach (IDNMcMapLayer layer in removedLayers)
                    {
                        int index = lstLayersRemoveAdd.Items.IndexOf(Manager_MCNames.GetNameByObject(layer));
                        if (index >= 0)
                        {
                            lstLayersRemoveAdd.SetSelected(index, true);
                        }
                    }
                }

                removedLayers = null;
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();

            // change btn text
            if (removedLayers == null)
                btnRemoveAdd.Text = "Remove Selected";
            else
                btnRemoveAdd.Text = "Add Removed";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveItem();
        }

        private void btnGetLayerByID_Click(object sender, EventArgs e)
        {
            bool isShowError = false;
            string strLayerId = ntxGetLayerById.Text;
            if (strLayerId != "")
            {
                uint LayerId;
                if (UInt32.TryParse(strLayerId, out LayerId))
                {
                    try
                    {
                        IDNMcMapLayer mcLayer = m_CurrentObject.GetLayerByID(LayerId);
                        if (mcLayer != null)
                        {
                            lnkLayerName.Text = Manager_MCNames.GetNameByObject(mcLayer);
                            lnkLayerName.Tag = mcLayer;
                        }
                        else
                            MessageBox.Show("IDNMcMapTerrain.GetLayerByID", "Not exist Layer with id " + LayerId, MessageBoxButtons.OK);

                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetLayerByID", McEx);
                        return;
                    }
                }
                else
                    isShowError = true;
            }
            else
                isShowError = true;

            if (isShowError)
            {
                MessageBox.Show("Layer Id should be positive number", "Invalid Layer Id", MessageBoxButtons.OK);
                ntxGetLayerById.Focus();
            }
        }

        private void lnkLayerName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lnkLayerName.Tag != null)
            {
                Control control = Parent.Parent.Parent;
                TreeViewDisplayForm mcTreeView = control as TreeViewDisplayForm;
                if (mcTreeView != null)
                {
                    mcTreeView.SelectNodeInTreeNode((uint)lnkLayerName.Tag.GetHashCode());
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstLayersRemoveAdd.Items.Count; i++)
                lstLayersRemoveAdd.SetSelected(i, true);
        }

        private void btnSetNumTilesInNativeServerRequest_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcMapLayer[] layers = m_CurrentObject.GetLayers();
                foreach (IDNMcMapLayer layer in layers)
                {
                    layer.SetNumTilesInNativeServerRequest(ntxNumTilesInNativeServerRequest.GetUInt32());
                }
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
