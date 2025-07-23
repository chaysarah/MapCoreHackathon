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

namespace MCTester.ObjectWorld.OverlayManagerWorld
{
	public partial class ucCollection : UserControl,IUserControlItem
	{
		private IDNMcCollection m_CurrentObject;
        private Dictionary<object, uint> m_Viewports;

        private List<string> m_lstObjectText = new List<string>();
        private List<IDNMcObject> m_lstObjectValue = new List<IDNMcObject>();
        private List<string> m_lstCollectionObjectText = new List<string>();
        private List<IDNMcObject> m_lstCollectionObjectValue = new List<IDNMcObject>();
        private List<string> m_lstOverlayText = new List<string>();
        private List<IDNMcOverlay> m_lstOverlayValue = new List<IDNMcOverlay>();
        private List<string> m_lstCollectionOverlayText = new List<string>();
        private List<IDNMcOverlay> m_lstCollectionOverlayValue = new List<IDNMcOverlay>();

        private List<string> m_lstCollectionVisibilityText = new List<string>();
        private List<IDNMcMapViewport> m_lstCollectionVisibilityValue = new List<IDNMcMapViewport>();

        private IDNMcMapViewport[] m_ViewportsList;
        private IDNMcOverlayManager m_CurrOM;

        private List<string> m_lstViewportText = new List<string>();
        private List<IDNMcMapViewport> m_lstViewportValue = new List<IDNMcMapViewport>();
       
		public ucCollection()
		{
            InitializeComponent();
			
            lstObjects.DisplayMember = "ObjectListText";
            lstObjects.ValueMember = "ObjectListValue";
            lstCollectionObjects.DisplayMember = "CollectionObjectListText";
            lstCollectionObjects.ValueMember = "CollectionObjectListValue";
            lstOverlay.DisplayMember = "OverlayListText";
            lstOverlay.ValueMember = "OverlayListValue";
            lstCollectionOverlay.DisplayMember = "CollectionOverlayListText";
            lstCollectionOverlay.ValueMember = "CollectionOverlayListValue";
            lstObjectsViewports.DisplayMember = "ViewportTextList";
            lstObjectsViewports.ValueMember = "ViewportValueList";

            cmbObjectsVisibility.Items.AddRange(Enum.GetNames(typeof(DNEActionOptions)));
            cmbOverlaysVisibility.Items.AddRange(Enum.GetNames(typeof(DNEActionOptions)));

		}

		#region IUserControlItem Members

        public void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcCollection)aItem;
            string name = string.Empty;

            GetViewportList();
            FillViewportListBoxes();
            FillViewportsCheckedListBox();

            try
            {
                IDNMcObject[] objectsInCollection = m_CurrentObject.GetObjects();
                if (objectsInCollection != null)
                {
                    foreach (IDNMcObject obj in objectsInCollection)
                    {
                        name = Manager_MCNames.GetNameByObject(obj);
                        m_lstCollectionObjectText.Add(name);
                        m_lstCollectionObjectValue.Add(obj);
                    }

                    lstCollectionObjects.Items.AddRange(m_lstCollectionObjectText.ToArray());
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetObjects", McEx);
            }

            try
            {
                IDNMcOverlay[] overlayInCollection = m_CurrentObject.GetOverlays();
                if (overlayInCollection != null)
                {
                    foreach (IDNMcOverlay overlay in overlayInCollection)
                    {
                        name = Manager_MCNames.GetNameByObject(overlay);
                        m_lstCollectionOverlayText.Add(name);
                        m_lstCollectionOverlayValue.Add(overlay);
                    }

                    lstCollectionOverlay.Items.AddRange(m_lstCollectionOverlayText.ToArray());
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetOverlays", McEx);
            }

            try
            {
                IDNMcOverlayManager OM = m_CurrentObject.GetOverlayManager();

                //Fill object and overlay list boxes
                IDNMcOverlay [] overlays = OM.GetOverlays();
                foreach(IDNMcOverlay overlay in overlays)
                {
                    if (!m_lstCollectionOverlayValue.Contains(overlay))
                    {
                        name = Manager_MCNames.GetNameByObject(overlay);
                        m_lstOverlayText.Add(name);
                        m_lstOverlayValue.Add(overlay);
                        
                    }
                    
                    IDNMcObject [] objects = overlay.GetObjects();
                    foreach (IDNMcObject obj in objects)
                    {
                        if (!m_lstCollectionObjectValue.Contains(obj))
                        {
                            name = Manager_MCNames.GetNameByObject(obj);
                            m_lstObjectText.Add(name);
                            m_lstObjectValue.Add(obj);
                        }
                    }
                }

                lstOverlay.Items.AddRange(m_lstOverlayText.ToArray());
                lstObjects.Items.AddRange(m_lstObjectText.ToArray());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Fill object & overlay list boxes", McEx);
            }
            
            try
            {
                chkCollectionVisibility.Checked = m_CurrentObject.GetCollectionVisibility();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("CollectionVisibilty", McEx);
            }
		}
		#endregion

        #region Private Method

        private void FillViewportListBoxes()
        {
            lstObjectsViewports.Items.Clear();
            m_lstViewportValue.Clear();
            m_lstViewportValue.Clear();

            if (m_ViewportsList == null || m_ViewportsList.Length == 0)
            {
                GetViewportList();
            }
            
            for (int i=0; i<m_ViewportsList.Length; i++)
            {
                m_lstViewportValue.Add((IDNMcMapViewport)m_ViewportsList[i]);
                m_lstViewportText.Add(Manager_MCNames.GetNameByObject(m_ViewportsList[i]));
                lstObjectsViewports.Items.Add(m_lstViewportText[i]);
                lstOverlaysViewports.Items.Add(m_lstViewportText[i]);
            }
        }
            
        private void FillViewportsCheckedListBox()
        {
            clstCollectionVisibility.Items.Clear();
            m_lstCollectionVisibilityText = new List<string>() ;
            m_lstCollectionVisibilityValue = new List<IDNMcMapViewport>();

            foreach (IDNMcMapViewport VP in m_ViewportsList)
            {
                if (VP.OverlayManager == m_CurrOM)
                {
                    try
                    {
                        bool IsVisible = m_CurrentObject.GetCollectionVisibility(VP);
                        string name = Manager_MCNames.GetNameByObject(VP);
                        m_lstCollectionVisibilityText.Add(name);
                        m_lstCollectionVisibilityValue.Add(VP);
                        clstCollectionVisibility.Items.Add(name, IsVisible);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetCollectionVisibility", McEx);
                    }                    
                }
            }
        }

        private void GetViewportList()
        {
            m_CurrOM = m_CurrentObject.GetOverlayManager();
            m_Viewports = Manager_MCViewports.AllParams;
            m_ViewportsList = new IDNMcMapViewport[m_Viewports.Count];
            m_Viewports.Keys.CopyTo(m_ViewportsList, 0);
        }


        #endregion

        private void btnAddObject_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstObjects.SelectedIndices.Count > 0)
                {
                    IDNMcObject[] selectedObj = new IDNMcObject[lstObjects.SelectedIndices.Count];
                    int i = 0;
                    foreach (int idx in lstObjects.SelectedIndices)
                    {
                        m_lstCollectionObjectText.Add(m_lstObjectText[idx]);
                        m_lstCollectionObjectValue.Add(m_lstObjectValue[idx]);

                        lstCollectionObjects.Items.Add(m_lstObjectText[idx]);
                        selectedObj[i] = m_lstObjectValue[idx];
                        i++;
                    }

                    ListBox.SelectedIndexCollection indices = lstObjects.SelectedIndices;
                    int[] ArrSelectedIndices = new int[indices.Count];
                    indices.CopyTo(ArrSelectedIndices, 0);

                    for (int idx = ArrSelectedIndices.Length - 1; idx >= 0; idx--)
                    {
                        m_lstObjectText.RemoveAt(ArrSelectedIndices[idx]);
                        m_lstObjectValue.RemoveAt(ArrSelectedIndices[idx]);

                        lstObjects.Items.RemoveAt(ArrSelectedIndices[idx]);
                    }

                    m_CurrentObject.AddObjects(selectedObj);

                    // turn on all viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }                                            
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("AddObjects", McEx);
            }
        }

        private void btnRemoveObj_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstCollectionObjects.SelectedIndices.Count > 0)
                {
                    int i = 0;
                    foreach (int idx in lstCollectionObjects.SelectedIndices)
                    {
                        m_lstObjectText.Add(m_lstCollectionObjectText[idx]);
                        m_lstObjectValue.Add(m_lstCollectionObjectValue[idx]);

                        lstObjects.Items.Add(m_lstCollectionObjectText[idx]);
                        m_CurrentObject.RemoveObjectFromCollection(m_lstCollectionObjectValue[idx]);
                       i++;
                    }

                    ListBox.SelectedIndexCollection indices = lstCollectionObjects.SelectedIndices;
                    int[] ArrSelectedIndices = new int[indices.Count];
                    indices.CopyTo(ArrSelectedIndices, 0);

                    for (int idx = ArrSelectedIndices.Length - 1; idx >= 0; idx--)
                    {
                        m_lstCollectionObjectText.RemoveAt(ArrSelectedIndices[idx]);
                        m_lstCollectionObjectValue.RemoveAt(ArrSelectedIndices[idx]);

                        lstCollectionObjects.Items.RemoveAt(ArrSelectedIndices[idx]);
                    }


                    // turn on all viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("RemoveObjectFromCollection", McEx);
            }
        }
        
        private void btnMoveAllObjOK_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.MoveObjects(ctrl3DVectorMoveAllObj.GetVector3D());

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("MoveObjects", McEx);
            }            
        }

        private void btnCollectionClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        
        private void btnAddOverlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstOverlay.SelectedIndices.Count > 0)
                {
                    IDNMcOverlay[] selectedOverlay = new IDNMcOverlay[lstOverlay.SelectedIndices.Count];
                    int i = 0;
                    foreach (int idx in lstOverlay.SelectedIndices)
                    {
                        m_lstCollectionOverlayText.Add(m_lstOverlayText[idx]);
                        m_lstCollectionOverlayValue.Add(m_lstOverlayValue[idx]);

                        lstCollectionOverlay.Items.Add(m_lstOverlayText[idx]);
                        selectedOverlay[i] = m_lstOverlayValue[idx];
                        i++;
                    }

                    ListBox.SelectedIndexCollection indices = lstOverlay.SelectedIndices;
                    int[] ArrSelectedIndices = new int[indices.Count];
                    indices.CopyTo(ArrSelectedIndices, 0);

                    for (int idx = ArrSelectedIndices.Length - 1; idx >= 0; idx--)
                    {
                        m_lstOverlayText.RemoveAt(ArrSelectedIndices[idx]);
                        m_lstOverlayValue.RemoveAt(ArrSelectedIndices[idx]);

                        lstOverlay.Items.RemoveAt(ArrSelectedIndices[idx]);
                    }

                    m_CurrentObject.AddOverlays(selectedOverlay);

                    // turn on all viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }
                
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("AddOverlays", McEx);
            }
        }


        private void btnRemoveOverlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstCollectionOverlay.SelectedIndex != -1)
                {
                    int selectedIndex = lstCollectionOverlay.SelectedIndex;
                    m_lstOverlayText.Add(m_lstCollectionOverlayText[selectedIndex]);
                    m_lstOverlayValue.Add(m_lstCollectionOverlayValue[selectedIndex]);
                    lstOverlay.Items.Add(m_lstCollectionOverlayText[selectedIndex]);

                    m_CurrentObject.RemoveOverlayFromCollection(m_lstCollectionOverlayValue[selectedIndex]);

                    // turn on all viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());

                    m_lstCollectionOverlayText.RemoveAt(selectedIndex);
                    m_lstCollectionOverlayValue.RemoveAt(selectedIndex);
                    lstCollectionOverlay.Items.RemoveAt(selectedIndex);
                }
                
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("RemoveOverlayFromCollection", McEx);
            }
        }

        private void btnClearCollection_Click(object sender, EventArgs e)
        {
            try
            {
                for (int idx = 0; idx < lstCollectionObjects.Items.Count; idx++)
                {
                    m_lstObjectText.Add(m_lstCollectionObjectText[idx]);
                    m_lstObjectValue.Add(m_lstCollectionObjectValue[idx]);

                    lstObjects.Items.Add(m_lstCollectionObjectText[idx]);
                }

                m_lstCollectionObjectText.Clear();
                m_lstCollectionObjectValue.Clear();
                lstCollectionObjects.Items.Clear();

                for (int idx = 0; idx < lstCollectionOverlay.Items.Count; idx++)
                {
                    m_lstOverlayText.Add(m_lstCollectionOverlayText[idx]);
                    m_lstOverlayValue.Add(m_lstCollectionOverlayValue[idx]);

                    lstOverlay.Items.Add(m_lstCollectionOverlayText[idx]);
                }
                
                m_lstCollectionOverlayText.Clear();
                m_lstCollectionOverlayValue.Clear();
                lstCollectionOverlay.Items.Clear();

                m_CurrentObject.Clear();

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Clear", McEx);
            }
        }

        private void btnRemoveOverlaysFromOM_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.RemoveOverlaysFromOverlayManager();

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("RemoveOverlaysFromOverlayManager", McEx);
            }
        }

        private void btnRemoveObjectsFromTheirOverlays_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.RemoveObjectsFromTheirOverlays();

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("RemoveObjectsFromTheirOverlays", McEx);
            }
        }

        private void clstCollectionVisibilityInViewport_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetCollectionVisibility(clstCollectionVisibility.GetItemChecked(
                                                        clstCollectionVisibility.SelectedIndex),
                                                        (m_lstCollectionVisibilityValue[clstCollectionVisibility.SelectedIndex]));
            }
            catch (MapCoreException McEx)
            {
                clstCollectionVisibility.SetItemChecked(clstCollectionVisibility.SelectedIndex, false);
                MapCore.Common.Utilities.ShowErrorMessage("SetCollectionVisibility", McEx);
            }
        }

        private void chkSelectAllObjects_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetObjectsState(chkSelectAllObjects.Checked ? (byte)1 : (byte)0); // TODO: support passing a viewport here, and for IDNMcObjectScheme::SetObjectsState() as well!

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetObjectsState", McEx);
            }
        }        


        #region Public Properties

        public List<string> ObjectListText
        {
            get { return m_lstObjectText; }
            set { m_lstObjectText = value; }
        }

        public List<IDNMcObject> ObjectListValue
        {
            get { return m_lstObjectValue; }
            set { m_lstObjectValue = value; }
        }

        public List<string> CollectionObjectListText
        {
            get { return m_lstCollectionObjectText; }
            set { m_lstCollectionObjectText = value; }
        }

        public List<IDNMcObject> CollectionObjectListValue
        {
            get { return m_lstCollectionObjectValue; }
            set { m_lstCollectionObjectValue = value; }
        }

        public List<string> OverlayListText
        {
            get { return m_lstOverlayText; }
            set { m_lstOverlayText = value; }
        }

        public List<IDNMcOverlay> OverlayListValue
        {
            get { return m_lstOverlayValue; }
            set { m_lstOverlayValue = value; }
        }

        public List<string> CollectionOverlayListText
        {
            get { return m_lstCollectionOverlayText; }
            set { m_lstCollectionOverlayText = value; }
        }

        public List<IDNMcOverlay> CollectionOverlayListValue
        {
            get { return m_lstCollectionOverlayValue; }
            set { m_lstCollectionOverlayValue = value; }
        }

        public List<string> ViewportTextList
        {
            get { return m_lstOverlayText; }
            set { m_lstOverlayText = value; }
        }

        public List<IDNMcOverlay> ViewportValueList
        {
            get { return m_lstOverlayValue; }
            set { m_lstOverlayValue = value; }
        }

        #endregion

        private void btnCollectionVisibilityApply_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetCollectionVisibility(chkCollectionVisibility.Checked);

                FillViewportsCheckedListBox();

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("CollectionVisibilty", McEx);
            }
        }


        private void btnObjectsVisbilityApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbObjectsVisibility.Text != "")
                {
                    DNEActionOptions actionOpt = (DNEActionOptions)Enum.Parse(typeof(DNEActionOptions), cmbObjectsVisibility.Text);
                    if (lstObjectsViewports.SelectedIndex >= 0)
                    {
                        m_CurrentObject.SetObjectsVisibilityOption(actionOpt, m_lstViewportValue[lstObjectsViewports.SelectedIndex]);
                    }
                    else
                    {
                        m_CurrentObject.SetObjectsVisibilityOption(actionOpt);
                    }

                    // turn on all viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }
                
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetObjectsVisibilityOption", McEx);
            }  
        }

        private void btnOverlaysVisbilityApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbOverlaysVisibility.Text != "")
                {
                    DNEActionOptions actionOpt = (DNEActionOptions)Enum.Parse(typeof(DNEActionOptions), cmbOverlaysVisibility.Text);
                    if (lstOverlaysViewports.SelectedIndex >= 0)
                    {
                        m_CurrentObject.SetObjectsVisibilityOption(actionOpt, m_lstViewportValue[lstOverlaysViewports.SelectedIndex]);
                    }
                    else
                    {
                        m_CurrentObject.SetOverlaysVisibilityOption(actionOpt);
                    }

                    // turn on all viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }                
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetOverlaysVisibilityOption", McEx);
            }
        }

        private void btnClearObjectsViewportSelection_Click(object sender, EventArgs e)
        {
            for (int i=0; i<lstObjectsViewports.Items.Count; i++)
            {
                lstObjectsViewports.SetSelected(i, false);
            }

            cmbObjectsVisibility.Text = "";
        }

        private void btnClearOverlaysViewportSelection_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstOverlaysViewports.Items.Count; i++)
            {
                lstOverlaysViewports.SetSelected(i, false);
            }

            cmbOverlaysVisibility.Text = "";
        }

        private void lstObjectsViewports_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbObjectsVisibility.Text = "";
        }

        private void lstOverlaysViewports_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbObjectsVisibility.Text = "";
        }

        private void btnStateApply_Click(object sender, EventArgs e)
        {
            string txtObjectStates = tbState.Text;
            byte[] abyStates = null;
            if (txtObjectStates != null && txtObjectStates.Trim() != string.Empty)
            {
                string txRepObjectStates = txtObjectStates;
                while (((txRepObjectStates = txtObjectStates.Replace("  ", " "))) != txtObjectStates)
                {
                    txtObjectStates = txRepObjectStates;
                }
                string[] antxStates = txtObjectStates.Split(" ".ToCharArray());
                abyStates = new byte[antxStates.Length];
                for (int i = 0; i < antxStates.Length; i++)
                {
                    abyStates[i] = byte.Parse(antxStates[i]);
                }
            }

            try
            {
                if (lstObjectsViewports.SelectedIndex >= 0)
                {

                    if (abyStates != null && abyStates.Length == 1)
                        m_CurrentObject.SetObjectsState(abyStates[0], m_lstViewportValue[lstObjectsViewports.SelectedIndex]);
                    else
                        m_CurrentObject.SetObjectsState(abyStates, m_lstViewportValue[lstObjectsViewports.SelectedIndex]);
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_lstViewportValue[lstObjectsViewports.SelectedIndex]);
                }
                else
                {
                    if (abyStates != null && abyStates.Length == 1)
                        m_CurrentObject.SetObjectsState(abyStates[0]);
                    else
                        m_CurrentObject.SetObjectsState(abyStates);
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetState", McEx);
            }
        }
    }
}
