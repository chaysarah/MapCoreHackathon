using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;
using MCTester.ObjectWorld.Assit_Forms;
using MCTester.Managers.MapWorld;
using MapCore.Common;
using MCTester.Managers.ObjectWorld;
using MCTester.GUI.Trees;
using MCTester.General_Forms;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ucObjectScheme : UserControl, IUserControlItem
    {
        private IDNMcObjectScheme m_CurrentObject;
        private List<DNSObjectStateModifier> m_objectStateModifiers;
        private List<string> m_lViewportText;
        private List<IDNMcMapViewport> m_lViewportValue;

        private List<string> m_lstSchemeText;
        private List<IDNMcObjectSchemeNode> m_lstSchemeValue;

        public ucObjectScheme()
        {
            InitializeComponent();
            m_objectStateModifiers = new List<DNSObjectStateModifier>();
            DNETerrainObjectsConsiderationFlags[] values = 
                (DNETerrainObjectsConsiderationFlags[])Enum.GetValues(typeof(DNETerrainObjectsConsiderationFlags));

            foreach (DNETerrainObjectsConsiderationFlags val in values)
            {
                if (val != DNETerrainObjectsConsiderationFlags._ETOCF_NONE)
                {
                    lstTerrainObjectsConsiderationFlags.Items.Add(val.ToString());
                }
            }

            m_lViewportText = new List<string>();
            m_lViewportValue = new List<IDNMcMapViewport>();

            lstViewports.DisplayMember = "lViewportText";
            lstViewports.ValueMember = "lViewportValue";

            m_lstSchemeText = new List<string>();
            m_lstSchemeValue = new List<IDNMcObjectSchemeNode>();
        }

        #region Properties
        public List<string> lViewportText
        {
            get { return m_lViewportText; }
            set { m_lViewportText = value; }
        }

        public List<IDNMcMapViewport> lViewportValue
        {
            get { return m_lViewportValue; }
            set { m_lViewportValue = value; }
        }

        #endregion
        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcObjectScheme)aItem;

            LoadViewportsToListBox();
            // lbObjects
            try
            {
                IDNMcObject[] objs = m_CurrentObject.GetObjects();
                foreach (IDNMcObject obj in objs)
                {
                    lstObjects.Items.Add(Manager_MCNames.GetNameByObject(obj));
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjects", McEx);
            }


            try
            {
                ntxSchemeID.SetUInt32(m_CurrentObject.GetID());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetID", McEx);
            }

            try
            {
                ntxNumLocation.SetUInt32(m_CurrentObject.GetNumObjectLocations());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetNumObjectLocations", McEx);
            }

            IDNMcObjectSchemeNode[] schemeNodeArr = m_CurrentObject.GetNodes(DNENodeKindFlags._ENKF_ANY_ITEM);
            string name;
            m_lstSchemeText.Add("None");
            m_lstSchemeValue.Add(null);

            foreach (IDNMcObjectSchemeNode schemeNode in schemeNodeArr)
            {
                name = Manager_MCNames.GetNameByObject(schemeNode);

                m_lstSchemeText.Add(name);
                m_lstSchemeValue.Add(schemeNode);
            }
            lstSchemeItems.Items.AddRange(m_lstSchemeText.ToArray());
            cbObjectRotationItems.Items.AddRange(m_lstSchemeText.ToArray());
            cbScreenArrangementItem.Items.AddRange(m_lstSchemeText.ToArray());
            cbEditModeDefaultItem.Items.AddRange(m_lstSchemeText.ToArray());

            try
            {
                IDNMcObjectSchemeItem CurrObjRotationItem = m_CurrentObject.GetObjectRotationItem();


                if (CurrObjRotationItem != null)
                    cbObjectRotationItems.SelectedText = Manager_MCNames.GetNameByObject(CurrObjRotationItem);
                else
                    cbObjectRotationItems.SelectedIndex = 0;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjectRotationItem", McEx);
            }

            try
            {
                IDNMcObjectSchemeItem CurrObjArrangmentItem = m_CurrentObject.GetObjectScreenArrangementItem();

                if (CurrObjArrangmentItem != null)
                    cbScreenArrangementItem.SelectedText = Manager_MCNames.GetNameByObject(CurrObjArrangmentItem);
                else
                    cbScreenArrangementItem.SelectedIndex = 0;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjectScreenArrangementItem", McEx);
            }

            try
            {
                IDNMcObjectSchemeItem CurrEditModeDefaultItem = m_CurrentObject.GetEditModeDefaultItem();

                if (CurrEditModeDefaultItem != null)
                    cbEditModeDefaultItem.SelectedText = Manager_MCNames.GetNameByObject(CurrEditModeDefaultItem);
                else
                    cbEditModeDefaultItem.SelectedIndex = 0;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetEditModeDefaultItem", McEx);
            }

            try
            {
                Dictionary<byte, string> dicStateNames = new Dictionary<byte, string>();

                for (byte i = 1; i <= 255; i++)
                {
                    name = m_CurrentObject.GetObjectStateName(i);
                    if (name != string.Empty)
                    {
                        dicStateNames.Add(i, name);
                    }
                    if (i == 255)
                        break;
                }

                ctrlStateNames.StateNames = dicStateNames;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjectStateName", McEx);
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

            try
            {
                chxTerrainItemsConsistency.Checked = m_CurrentObject.GetTerrainItemsConsistency();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTerrainItemsConsistency", McEx);
            }

            try
            {
                chxGroupingItemsByDrawPriorityWithinObjects.Checked = m_CurrentObject.GetGroupingItemsByDrawPriorityWithinObjects();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetGroupingItemsByDrawPriorityWithinObjects", McEx);
            }

            try
            {
                DNETerrainObjectsConsiderationFlags flags = m_CurrentObject.GetTerrainObjectsConsideration();

                for (int i = 0; i < lstTerrainObjectsConsiderationFlags.Items.Count; i++)
                {
                    lstTerrainObjectsConsiderationFlags.SetItemChecked(i, false);

                    name = lstTerrainObjectsConsiderationFlags.Items[i].ToString();
                    DNETerrainObjectsConsiderationFlags flag =
                        (DNETerrainObjectsConsiderationFlags)Enum.Parse(typeof(DNETerrainObjectsConsiderationFlags), name);
                    if ((flags & flag) != 0)
                    {
                        lstTerrainObjectsConsiderationFlags.SetItemChecked(i, true);
                    }
                    i++;
                }

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTerrainItemsConsistency", McEx);
            }

            m_objectStateModifiers = new List<DNSObjectStateModifier>(m_CurrentObject.GetObjectStateModifiers());
            for (int i = 0; i < m_objectStateModifiers.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = Manager_MCNames.GetNameByObject(
                    m_objectStateModifiers[i].pConditionalSelector,
                    m_objectStateModifiers[i].pConditionalSelector.ConditionalSelectorType.ToString());
                lvi.ToolTipText = m_objectStateModifiers[i].pConditionalSelector.ConditionalSelectorType.ToString();
                lvi.Tag = m_objectStateModifiers[i].pConditionalSelector;
                lvi.SubItems.Add(m_objectStateModifiers[i].bActionOnResult ? "Yes" : "No");
                lvi.SubItems.Add(m_objectStateModifiers[i].uObjectState.ToString());

                lstObjectStateModifier.Items.Add(lvi);
            }
            try
            {
                chxIgnoreUpdatingNonExistentProperty.Checked = DNMcObjectScheme.GetIgnoreUpdatingNonExistentProperty();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetIgnoreUpdatingNonExistentProperty", McEx);
            }
        }

        #endregion

        #region Public Method

        private void SaveItem()
        {
            

            try
            {
                m_CurrentObject.SetID(ntxSchemeID.GetUInt32());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetID", McEx);
            }

                try
                {
                    Dictionary<byte, string> names = ctrlStateNames.StateNames;
                    
                    string name;
                    for (byte i = 1; i <= 255;i++ )
                    {
                        name = "";
                        if (names.ContainsKey(i))
                            name = names[i];
                        m_CurrentObject.SetObjectStateName(name, i);
                        if (i == 255)
                            break;
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetObjectStateName", McEx);
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

            try
            {
                m_CurrentObject.SetTerrainItemsConsistency(chxTerrainItemsConsistency.Checked);

                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetTerrainItemsConsistency", McEx);
            }
            try
            {
                m_CurrentObject.SetGroupingItemsByDrawPriorityWithinObjects(chxGroupingItemsByDrawPriorityWithinObjects.Checked);
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetGroupingItemsByDrawPriorityWithinObjects", McEx);
            }
            Manager_MCObjectScheme.CurrentScheme = null;
            Manager_MCObjectScheme.CurrentScheme = m_CurrentObject;
        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnApply_Click(sender, e);
            GeneralFuncs.CloseParentForm(this);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveItem();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GeneralFuncs.CloseParentForm(this);
        }

        private void btnGetLocationIndexByID_Click(object sender, EventArgs e)
        {
            try
            {
                ntxLocationIndexByID.SetUInt32(m_CurrentObject.GetObjectLocationIndexByID(ntxNodeID.GetUInt32()));
            }   
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjectLocationIndexByID", McEx);
            }
        }

        private void btnPropertiesID_Click(object sender, EventArgs e)
        {
            frmPropertiesIDList PropertyIDListForm = new frmPropertiesIDList(m_CurrentObject);
            PropertyIDListForm.Show();
        }

        private void btnObjectRotationItemApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbObjectRotationItems.SelectedIndex >= 0)
                {
                    m_CurrentObject.SetObjectRotationItem((IDNMcObjectSchemeItem)m_lstSchemeValue[cbObjectRotationItems.SelectedIndex]);
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }
                /*if (lstSchemeItems.SelectedItem != null)
                {
                    //SelectedScheme = m_lstSchemeValue[lstSchemes.SelectedIndex];
                    m_CurrentObject.SetObjectRotationItem((IDNMcObjectSchemeItem)m_lstSchemeValue[cbObjectRotationItems.SelectedIndex]);
                }

                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());*/
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetObjectRotationItem", McEx);
            }
        }

        private void btnObjectRotationItemClear_Click(object sender, EventArgs e)
        {
            lstSchemeItems.ClearSelected();
        }

        private void btnPropertyTypeOK_Click(object sender, EventArgs e)
        {
            try
            {
                uint result;
                bool IsSucceed = uint.TryParse(ntxPropertyID.Text, out result);
                if (IsSucceed == true)
                {
                    txtPropertyType.Text = m_CurrentObject.GetPropertyType(result,chxNoFailOnNotExist.Checked).ToString();                    
                }
                else
                {
                    MessageBox.Show("Please insert property ID", "Property Id is incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPropertyType.Text = "";
                }
            }
            catch (MapCoreException McEx)
            {
                txtPropertyType.Text = "";
                Utilities.ShowErrorMessage("GetPropertyType", McEx);
            }
        }
        
        private void btnScreenArrangementItemApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbScreenArrangementItem.SelectedIndex >= 0)
                {
                    m_CurrentObject.SetObjectScreenArrangementItem((IDNMcObjectSchemeItem)m_lstSchemeValue[cbScreenArrangementItem.SelectedIndex]);
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetObjectScreenArrangementItem", McEx);
            }
        }

        private void btnEditModeDefaultItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbEditModeDefaultItem.SelectedIndex >= 0)
                {
                    m_CurrentObject.SetEditModeDefaultItem((IDNMcObjectSchemeItem)m_lstSchemeValue[cbEditModeDefaultItem.SelectedIndex]);
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetEditModeDefaultItem", McEx);
            }
        }

        private void mnuObjStateModifierItems_Opening(object sender, CancelEventArgs e)
        {
            if (lstObjectStateModifier.SelectedItems.Count == 0)
            {
                mnuItemAdd.Visible = true;
                mnuItemInsert.Visible = false;
                mnuItemEdit.Enabled = false;
                mnuItemRemove.Enabled = false;
            }
            else
            {
                mnuItemAdd.Visible = false;
                mnuItemInsert.Visible = true;
                mnuItemEdit.Enabled = true;
                mnuItemRemove.Enabled = true;
            }
        }

        private void btnObjectStateMdfrClear_Click(object sender, EventArgs e)
        {
            m_objectStateModifiers.Clear();
            lstObjectStateModifier.Items.Clear();
        }

        private void btnObjectStateMdfrApply_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetObjectStateModifiers(m_objectStateModifiers.ToArray());

                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetObjectStateModifiers", McEx);
            }
        }

        private void mnuItemAdd_Click(object sender, EventArgs e)
        {
            frmObjectStateModifier objStateMdfrFrm = new frmObjectStateModifier(m_CurrentObject);
            if (objStateMdfrFrm.ShowDialog() == DialogResult.OK)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = objStateMdfrFrm.ObjectState.pConditionalSelector;
                if (objStateMdfrFrm.ObjectState.pConditionalSelector != null)
                {
                    lvi.Text = Manager_MCNames.GetNameByObject(
                        objStateMdfrFrm.ObjectState.pConditionalSelector,
                        objStateMdfrFrm.ObjectState.pConditionalSelector.ConditionalSelectorType.ToString());
                    lvi.ToolTipText = objStateMdfrFrm.ObjectState.pConditionalSelector.ConditionalSelectorType.ToString();
                }
                lvi.SubItems.Add(objStateMdfrFrm.ObjectState.bActionOnResult ? "Yes": "No");
                lvi.SubItems.Add(objStateMdfrFrm.ObjectState.uObjectState.ToString());
                lstObjectStateModifier.Items.Add(lvi);
                m_objectStateModifiers.Add(objStateMdfrFrm.ObjectState);
            }
        }

        private void mnuItemInsert_Click(object sender, EventArgs e)
        {
            if (lstObjectStateModifier.SelectedIndices.Count == 0)
            {
                return;
            }

            int index = lstObjectStateModifier.SelectedIndices[0];

            frmObjectStateModifier objStateMdfrFrm = new frmObjectStateModifier(m_CurrentObject);
            if (objStateMdfrFrm.ShowDialog() == DialogResult.OK)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = objStateMdfrFrm.ObjectState.pConditionalSelector;
                lvi.Text = Manager_MCNames.GetNameByObject(
                    objStateMdfrFrm.ObjectState.pConditionalSelector,
                    objStateMdfrFrm.ObjectState.pConditionalSelector.ConditionalSelectorType.ToString());
                lvi.ToolTipText = objStateMdfrFrm.ObjectState.pConditionalSelector.ConditionalSelectorType.ToString();
                lvi.SubItems.Add(objStateMdfrFrm.ObjectState.bActionOnResult ? "Yes" : "No");
                lvi.SubItems.Add(objStateMdfrFrm.ObjectState.uObjectState.ToString());
                lstObjectStateModifier.Items.Insert(index,lvi);
                m_objectStateModifiers.Insert(index,objStateMdfrFrm.ObjectState);
            }
        }

        private void mnuItemEdit_Click(object sender, EventArgs e)
        {
            if (lstObjectStateModifier.SelectedItems.Count == 0)
            {
                return;
            }

            int index = lstObjectStateModifier.SelectedIndices[0];
            frmObjectStateModifier objStateMdfrFrm = new frmObjectStateModifier(m_CurrentObject);

            objStateMdfrFrm.ObjectState = m_objectStateModifiers[index];
            if (objStateMdfrFrm.ShowDialog() == DialogResult.OK)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = objStateMdfrFrm.ObjectState.pConditionalSelector;
                lvi.Text = Manager_MCNames.GetNameByObject(
                    objStateMdfrFrm.ObjectState.pConditionalSelector,
                    objStateMdfrFrm.ObjectState.pConditionalSelector.ConditionalSelectorType.ToString());
                lvi.ToolTipText = objStateMdfrFrm.ObjectState.pConditionalSelector.ConditionalSelectorType.ToString();
                lvi.SubItems.Add(objStateMdfrFrm.ObjectState.bActionOnResult ? "Yes" : "No");
                lvi.SubItems.Add(objStateMdfrFrm.ObjectState.uObjectState.ToString());

                lstObjectStateModifier.Items[index] = lvi;
                m_objectStateModifiers[index] = objStateMdfrFrm.ObjectState;
            }
        }

        private void mnuItemRemove_Click(object sender, EventArgs e)
        {
            if (lstObjectStateModifier.SelectedItems.Count == 0)
            {
                return;
            }

            int index = lstObjectStateModifier.SelectedIndices[0];
            frmObjectStateModifier objStateMdfrFrm = new frmObjectStateModifier(m_CurrentObject);

            if (MessageBox.Show("Delete Object State Modifier. Are you sure?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lstObjectStateModifier.Items.RemoveAt(index);
                m_objectStateModifiers.RemoveAt(index);
            }
        }

        private void btnTerrainObjConsClear_Click(object sender, EventArgs e)
        {
            for (int i=0; i<lstTerrainObjectsConsiderationFlags.Items.Count; i++)
            {
                lstTerrainObjectsConsiderationFlags.SetItemChecked(i, false);
            }
        }

        private void btnTerrainObjConsApply_Click(object sender, EventArgs e)
        {
            DNETerrainObjectsConsiderationFlags flags = DNETerrainObjectsConsiderationFlags._ETOCF_NONE;
            for (int i = 0; i < lstTerrainObjectsConsiderationFlags.Items.Count; i++)
            {
                if (lstTerrainObjectsConsiderationFlags.GetItemChecked(i))
                {
                    DNETerrainObjectsConsiderationFlags currFlag =
                        (DNETerrainObjectsConsiderationFlags)Enum.Parse(
                            typeof(DNETerrainObjectsConsiderationFlags),
                            lstTerrainObjectsConsiderationFlags.Items[i].ToString());
                    flags |= currFlag;
                }
            }
            m_CurrentObject.SetTerrainObjectsConsideration(flags);
        }

        private void LoadViewportsToListBox()
        {
            try
            {
                IDNMcOverlayManager currOM = m_CurrentObject.GetOverlayManager();
                Dictionary<object, uint> dViewports = Manager_MCViewports.AllParams;
                foreach (IDNMcMapViewport vp in dViewports.Keys)
                {
                    if (vp.OverlayManager == currOM)
                    {
                        string name = Manager_MCNames.GetNameByObject(vp);
                        lViewportText.Add(name);
                        lViewportValue.Add(vp);
                    }
                }

                lstViewports.Items.AddRange(lViewportText.ToArray());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetViewportsIDs", McEx);
            }
        }

        private void SetObjectStates()
        {
            string strObjectStates = antxObjectStates.Text;
            byte[] states = null;
            if (strObjectStates != null && strObjectStates.Trim() != string.Empty)
            {
                while (strObjectStates.Contains("  "))
                {
                    strObjectStates = strObjectStates.Replace("  ", " ");
                }
                string[] strStates = strObjectStates.Split(" ".ToCharArray());
                states = new byte[strStates.Length];
                for (int i = 0; i < states.Length; i++)
                {
                    states[i] = byte.Parse(strStates[i]);
                }
            }
            try
            {
                if (lstViewports.SelectedIndex >= 0)
                {
                    if (states != null && states.Length == 1)
                        m_CurrentObject.SetObjectsState(states[0], lViewportValue[lstViewports.SelectedIndex]);
                    else
                        m_CurrentObject.SetObjectsState(states, lViewportValue[lstViewports.SelectedIndex]);
                }
                else
                {
                    if (states != null && states.Length == 1)
                        m_CurrentObject.SetObjectsState(states[0]);
                    else
                        m_CurrentObject.SetObjectsState(states);

                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetObjectsState", McEx);
            }
        }

        private void btnSetObjState_Click(object sender, EventArgs e)
        {
            SetObjectStates();
        }

        private void btnClearViewportSelection_Click(object sender, EventArgs e)
        {
            lstViewports.SelectedIndex = -1;
        }

        private void antxObjectStates_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(antxObjectStates, "");
        }

        private void antxObjectStates_Validating(object sender, CancelEventArgs e)
        {
            if (antxObjectStates.Text.Length > 0)
            {
                string strObjectStates = antxObjectStates.Text;
                while (strObjectStates.Contains("  "))
                {
                    strObjectStates = strObjectStates.Replace("  ", " ");
                }
                string[] strStates = strObjectStates.Split(" ".ToCharArray());
                foreach (string strVal in strStates)
                {
                    byte val;
                    if (!byte.TryParse(strVal, out val))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(antxObjectStates, "You must provide list of byte values [0..255], separated with space");
                        return;
                    }
                }
            }
        }

        private void btnGetStateIdByName_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStateName.Text != "")
                    ntxStateID.Text = m_CurrentObject.GetObjectStateByName(txtStateName.Text).ToString();
                else
                {
                    MessageBox.Show("Please insert State name", "State name is incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ntxStateID.Text = "";
                }
            }
            catch (MapCoreException McEx)
            {
                ntxStateID.Text = "";
                Utilities.ShowErrorMessage("GetObjectStateByName", McEx);
            }
        }

        private void GetStateNameById_Click(object sender, EventArgs e)
        {
            try
            {
                byte result;
                bool IsSucceed = byte.TryParse(ntxStateID.Text, out result);
                if (IsSucceed == true)
                {
                    txtStateName.Text = m_CurrentObject.GetObjectStateName(result);
                }
                else
                {
                    MessageBox.Show("Please insert State", "State is incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtStateName.Text = "";
                }
            }
            catch (MapCoreException McEx)
            {
                txtStateName.Text = "";
                Utilities.ShowErrorMessage("GetObjectStateName", McEx);
            }
        }
      
        private void btnGetNodeByID_Click(object sender, EventArgs e)
        {
            bool isShowError = false;
            string strNodeId = ntxGetNodeById.Text;
            if(strNodeId != "")
            {
                uint nodeId;
                if (UInt32.TryParse(strNodeId, out nodeId))
                {
                    try
                    {
                        IDNMcObjectSchemeNode mcObjectSchemeNode = m_CurrentObject.GetNodeByID(nodeId);
                        if (mcObjectSchemeNode != null)
                        {
                            llNodeName.Text = Manager_MCNames.GetNameByObject(mcObjectSchemeNode);
                            llNodeName.Tag = mcObjectSchemeNode;
                        }
                        else
                            MessageBox.Show("IDNMcObjectScheme.GetNodeByID", "Not exist node with id " + nodeId, MessageBoxButtons.OK);

                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetNodeByID", McEx);
                        return;
                    }
                }
                else
                    isShowError = true;
            }
            else
                isShowError = true;

            if(isShowError)
                MessageBox.Show("Node Id should be positive number", "Invalid Node Id", MessageBoxButtons.OK);
        }

        private void llNodeName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (llNodeName.Tag != null)
            {
                Control control = Parent.Parent.Parent;
                MCObjectWorldTreeViewForm mcOverlayMangerTreeView = control as MCObjectWorldTreeViewForm;
                if (mcOverlayMangerTreeView != null)
                {
                    mcOverlayMangerTreeView.SelectNodeInTreeNode((uint)llNodeName.Tag.GetHashCode());
                }
            }
        }

        private void btnGetNodeByName_Click(object sender, EventArgs e)
        {
            if (ntxGetNodeByName.Text != "")
            {
                try
                {
                    IDNMcObjectSchemeNode mcObjectSchemeNode = m_CurrentObject.GetNodeByName(ntxGetNodeByName.Text);
                    if (mcObjectSchemeNode != null)
                    {
                        llNodeName.Text = Manager_MCNames.GetNameByObject(mcObjectSchemeNode);
                        llNodeName.Tag = mcObjectSchemeNode;
                    }
                    else
                        MessageBox.Show("Not exist node with name " + ntxGetNodeByName.Text, "IDNMcObjectScheme.GetNodeByName", MessageBoxButtons.OK);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetNodeByName", McEx);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please insert node name", "Invalid Node Name", MessageBoxButtons.OK);
            }

        }

        private void btnIgnoreUpdatingNonExistentProperty_Click(object sender, EventArgs e)
        {
            try
            {
                DNMcObjectScheme.SetIgnoreUpdatingNonExistentProperty(chxIgnoreUpdatingNonExistentProperty.Checked);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetIgnoreUpdatingNonExistentProperty", McEx);
                return;
            }
        }

        private void btnOpenEditModeObjectOperationsParameters_Click(object sender, EventArgs e)
        {
            EditModePropertiesNoChanges editModePropertieNoChanges = new EditModePropertiesNoChanges(m_CurrentObject);
            editModePropertieNoChanges.Show();
        }

    }
}
