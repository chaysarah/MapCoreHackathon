using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms;
using System.Collections.ObjectModel;
using MCTester.Managers;
using MapCore.Common;

namespace MCTester.Controls
{
    public partial class CtrlConditionalSelectorProperty : UserControl,IUserControlItem
    {
        private IDNMcObjectSchemeNode m_CurrObject;
        private IDNMcConditionalSelector[] mSelectorsArr;
                
        public CtrlConditionalSelectorProperty()
        {
            InitializeComponent();
            tcConditionalSelector.TabPages.Remove(tpSelConditionalSelector);

            cmbRegActionType.Items.AddRange(Enum.GetNames(typeof(DNEActionType)));
            cmbSelActionType.Items.AddRange(Enum.GetNames(typeof(DNEActionType)));
        }

        private void btnSetSelector_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcConditionalSelector selectedSelector;
                if (clstRegConditionalSelector.CheckedItems.Count == 0)
                    selectedSelector = null;
                else
                    selectedSelector = mSelectorsArr[clstRegConditionalSelector.CheckedIndices[0]];

                DNEActionType actiontype = (DNEActionType)Enum.Parse(typeof(DNEActionType), cmbRegActionType.Text);

                m_CurrObject.SetConditionalSelector(actiontype,
                                                        chkActionOnResult.Checked,
                                                        selectedSelector,
                                                        ntxRegPropertyID.GetUInt32(),
                                                        false);

                // turn on all viewports render needed flags
                if (selectedSelector != null)
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(selectedSelector.GetOverlayManager());
                else
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrObject.GetScheme().GetOverlayManager());                    

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetConditionalSelector", McEx);
            }

            try
            {
                IDNMcConditionalSelector selectedSelector;
                if (clstSelConditionalSelector.CheckedItems.Count == 0)
                    selectedSelector = null;
                else
                    selectedSelector = mSelectorsArr[clstSelConditionalSelector.CheckedIndices[0]];

                DNEActionType actiontype = (DNEActionType)Enum.Parse(typeof(DNEActionType), cmbSelActionType.Text);

                if (chxSelectionExists.Checked == true)
                {
                    m_CurrObject.SetConditionalSelector(actiontype,
                                                        chkActionOnResult.Checked,
                                                        selectedSelector,
                                                        ntxSelPropertyID.GetUInt32(),
                                                        true);

                    // turn on all viewports render needed flags
                    if (selectedSelector != null)
                        MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(selectedSelector.GetOverlayManager());
                    else                        
                        MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrObject.GetScheme().GetOverlayManager());                    
                    
                }
                else
                {
                    ntxSelPropertyID.SetUInt32((uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID);

                    m_CurrObject.SetConditionalSelector(actiontype,
                                                        chkActionOnResult.Checked,
                                                        selectedSelector,
                                                        ntxSelPropertyID.GetUInt32(),
                                                        true);

                    // turn on all viewports render needed flags
                    if (selectedSelector != null)
                        MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(selectedSelector.GetOverlayManager());
                    else
                        MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrObject.GetScheme().GetOverlayManager());
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetConditionalSelector", McEx);
            }
        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {
            m_CurrObject = (IDNMcObjectSchemeNode)aItem;

            FillConditionalSelectorLists();
            
            cmbRegActionType.Text = DNEActionType._EAT_VISIBILITY.ToString();
            cmbSelActionType.Text = DNEActionType._EAT_VISIBILITY.ToString();

            SetRegSelectedSelector();
            SetSelSelectedSelector();
        }

        #endregion

        private void FillConditionalSelectorLists()
        {
            if (m_CurrObject.GetScheme() != null)
                SelectorsArr = m_CurrObject.GetScheme().GetOverlayManager().GetConditionalSelectors();
            else
                SelectorsArr = new IDNMcConditionalSelector[0];            
        }

        protected virtual void cmbRegActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetRegSelectedSelector();
            
        }

        protected virtual void cmbSelActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool actionOnResult = false;
            IDNMcConditionalSelector condSelector = null;
            uint propID;
            DNEActionType actionType = DNEActionType._EAT_VISIBILITY;

            try
            {
                actionType = (DNEActionType)Enum.Parse(typeof(DNEActionType), cmbSelActionType.Text);

                m_CurrObject.GetConditionalSelector(actionType,
                                                        out actionOnResult,
                                                        out condSelector,
                                                        out propID,
                                                        true);

                for (int i = 0; i < clstSelConditionalSelector.Items.Count; i++)
                {
                    if (condSelector == SelectorsArr[i])
                        clstSelConditionalSelector.SetItemChecked(i, true);
                    else
                        clstSelConditionalSelector.SetItemChecked(i, false);
                }

                if (propID != (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
                {
                    if (ntxSelPropertyID.GetUInt32() == (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID)
                    {
                        rdbSelShared.Checked = true;
                        ntxSelPropertyID.Enabled = false;
                    }
                    else
                    {
                        rdbSelPrivate.Checked = true;
                        ntxSelPropertyID.Enabled = true;
                    }
                }

                ntxSelPropertyID.SetUInt32(propID);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetConditionalSelector", McEx);
            }
        }

        private void SetSelSelectedSelector()
        {
            bool actionOnResult = false;
            IDNMcConditionalSelector condSelector = null;
            uint propID;
            DNEActionType actionType;
            bool selectionExist = false;
            
            // check for any action type if there is a selection mode
            try
            {
                actionType = DNEActionType._EAT_ACTIVITY;
                m_CurrObject.GetConditionalSelector(actionType,
                                                        out actionOnResult,
                                                        out condSelector,
                                                        out propID,
                                                        true);

                if (propID != (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
                    selectionExist = true;

                actionType = DNEActionType._EAT_TRANSFORM;
                m_CurrObject.GetConditionalSelector(actionType,
                                                        out actionOnResult,
                                                        out condSelector,
                                                        out propID,
                                                        true);

                if (propID != (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
                    selectionExist = true;

                actionType = DNEActionType._EAT_VISIBILITY;
                m_CurrObject.GetConditionalSelector(actionType,
                                                        out actionOnResult,
                                                        out condSelector,
                                                        out propID,
                                                        true);

                if (propID != (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
                    selectionExist = true;


                chxSelectionExists.Checked = selectionExist;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetConditionalSelector", McEx);
            }
            
            try
            {
                actionType = (DNEActionType)Enum.Parse(typeof(DNEActionType), cmbSelActionType.Text);

                m_CurrObject.GetConditionalSelector(actionType,
                                                        out actionOnResult,
                                                        out condSelector,
                                                        out propID,
                                                        true);

                for (int i = 0; i < clstSelConditionalSelector.Items.Count; i++)
                {
                    if (condSelector == SelectorsArr[i])
                        clstSelConditionalSelector.SetItemChecked(i, true);
                    else
                        clstSelConditionalSelector.SetItemChecked(i, false);
                }
                
                if (propID != (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
                {
                    chxSelectionExists.Checked = true;

                    if (ntxSelPropertyID.GetUInt32() == (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID)
                    {
                        rdbSelShared.Checked = true;
                        ntxSelPropertyID.Enabled = false;
                    }
                    else
                    {
                        rdbSelPrivate.Checked = true;
                        ntxSelPropertyID.Enabled = true;
                    }
                }

                ntxSelPropertyID.SetUInt32(propID);                
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetConditionalSelector", McEx);
            }
        }

        private void SetRegSelectedSelector()
        {
            bool actionOnResult = false;
            IDNMcConditionalSelector condSelector = null;
            uint propID;
            DNEActionType actionType = DNEActionType._EAT_VISIBILITY;

            try
            {
                actionType = (DNEActionType)Enum.Parse(typeof(DNEActionType), cmbRegActionType.Text);

                m_CurrObject.GetConditionalSelector(actionType,
                                                        out actionOnResult,
                                                        out condSelector,
                                                        out propID,
                                                        false);

                for (int i = 0; i < clstRegConditionalSelector.Items.Count; i++)
                {
                    if (condSelector == SelectorsArr[i])
                        clstRegConditionalSelector.SetItemChecked(i, true);
                    else
                        clstRegConditionalSelector.SetItemChecked(i, false);
                }
                


                ntxRegPropertyID.SetUInt32(propID);
                chkActionOnResult.Checked = actionOnResult;

                if (ntxRegPropertyID.GetUInt32() == (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID)
                {
                    rdbRegShared.Checked = true;
                    ntxRegPropertyID.Enabled = false;
                }
                else
                {
                    rdbRegPrivate.Checked = true;
                    ntxRegPropertyID.Enabled = true;
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetConditionalSelector", McEx);
            }
        }
        
        public IDNMcConditionalSelector [] SelectorsArr
        {
            get 
            {
                return mSelectorsArr; 
            }
            set 
            {
                mSelectorsArr = value;

                for (int i = 0; i < value.Length; i++)
                {
                    string name = Manager_MCNames.GetNameByObject(value[i], value[i].ConditionalSelectorType.ToString());
                    
                    clstRegConditionalSelector.Items.Add(name);
                    clstSelConditionalSelector.Items.Add(name);
                }
            }
        }

        private void chxSelectionExists_CheckedChanged(object sender, EventArgs e)
        {
            if (chxSelectionExists.Checked == false)
                tcConditionalSelector.TabPages.Remove(tpSelConditionalSelector);
            else
            {
                if (tcConditionalSelector.TabPages.Contains(tpSelConditionalSelector) == false)
                {
                    tcConditionalSelector.TabPages.Add(tpSelConditionalSelector);
                }

                if (rdbSelShared.Checked == true)
                {
                    ntxSelPropertyID.SetUInt32((uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID);
                }

            }
        }

        private void rdbRegPrivate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRegPrivate.Checked == true)
                ntxRegPropertyID.Enabled = true;
        }

        private void rdbSelPrivate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSelPrivate.Checked == true)
                ntxSelPropertyID.Enabled = true;
        }

        private void rdbRegShared_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRegShared.Checked == true)
            {
                ntxRegPropertyID.Enabled = false;
                ntxRegPropertyID.SetUInt32((uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID);
            }
        }

        private void rdbSelShared_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSelShared.Checked == true)
            {
                ntxSelPropertyID.Enabled = false;
                ntxSelPropertyID.SetUInt32((uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID);
            }
        }

        private void clstRegConditionalSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < clstRegConditionalSelector.Items.Count; i++)
                clstRegConditionalSelector.SetItemChecked(i, false);

            int selectedIndex = clstRegConditionalSelector.SelectedIndex;
            
            if (selectedIndex >= 0)
                clstRegConditionalSelector.SetItemChecked(selectedIndex, true);
        }

        private void clstSelConditionalSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < clstSelConditionalSelector.Items.Count; i++)
                clstSelConditionalSelector.SetItemChecked(i, false);

            int selectedIndex = clstSelConditionalSelector.SelectedIndex;

            if (selectedIndex >= 0)
                clstSelConditionalSelector.SetItemChecked(selectedIndex, true);
        }    
    }
}
