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

namespace MCTester.Controls
{
    public partial class CtrlConditionalselectorOverlay : UserControl, IUserControlItem
    {
        private object m_CurrObject;
        private IDNMcConditionalSelector[] mSelectorsArr;

        Dictionary<DNEActionType, MCTConditionalSelectorProperty> m_dicValues = new Dictionary<DNEActionType,MCTConditionalSelectorProperty>();
        private List<DNEActionType> m_lstEnumValues = new List<DNEActionType>();
        private Array arrEnumValues;

        public CtrlConditionalselectorOverlay():base()
        {
            InitializeComponent();
            
            arrEnumValues = Enum.GetValues(typeof(DNEActionType));

            foreach (DNEActionType value in arrEnumValues)
            {
                if (value != DNEActionType._EAT_NUM && value != DNEActionType._EAT_TRANSFORM && value != DNEActionType._EAT_ACTIVITY)
                {
                    cmbActionType.Items.Add(value.ToString());
                    m_lstEnumValues.Add(value);
                }
            }
            if (m_lstEnumValues.Count == 1)
                cmbActionType.Enabled = false;
            mSelectorsArr = new IDNMcConditionalSelector[0];
        }


        public void Save()
        {

            IDNMcOverlayManager objOverlayManager = null;
            
            SaveCurrentValue();

            foreach (DNEActionType actionType in m_lstEnumValues)
            {
                MCTConditionalSelectorProperty value = m_dicValues[actionType];
                if (value != null)
                {
                    if(m_CurrObject is IDNMcObject)
                    {
                        ((IDNMcObject)m_CurrObject).SetConditionalSelector(actionType,
                                                                                                       value.ActionOnResult,
                                                                                                       value.ConditionalSelector);
                        objOverlayManager = ((IDNMcObject)m_CurrObject).GetOverlayManager();
                    }
                    else if (m_CurrObject is IDNMcOverlay)
                    {
                            ((IDNMcOverlay)m_CurrObject).SetConditionalSelector(actionType,
                                                                                value.ActionOnResult,
                                                                                value.ConditionalSelector);

                            objOverlayManager = ((IDNMcOverlay)m_CurrObject).GetOverlayManager(); 
                    }
                }
                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(objOverlayManager);
            }
        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {
            m_CurrObject = aItem;
            FillConditionalSelectorLists();
            Load();
            selectedEnumKey = DNEActionType._EAT_VISIBILITY;
            cmbActionType.Text = DNEActionType._EAT_VISIBILITY.ToString();
            //SetSelectedSelector();
        }

        #endregion

        private void FillConditionalSelectorLists()
        {
            if(m_CurrObject is IDNMcObject)
            {
                SetSelectorsArr(((IDNMcObject)m_CurrObject).GetOverlayManager().GetConditionalSelectors());
            }
            else if (m_CurrObject is IDNMcOverlay)
            {
                SetSelectorsArr(((IDNMcOverlay)m_CurrObject).GetOverlayManager().GetConditionalSelectors());

            }
        }

        private new void Load()
        {
            try
            {
                foreach (DNEActionType actionType in m_lstEnumValues)
                {

                    bool actionOnResult = false;
                    IDNMcConditionalSelector condSelector = null;

                    if (m_CurrObject is IDNMcObject)
                    {
                        ((IDNMcObject)m_CurrObject).GetConditionalSelector(actionType,
                                                                            out actionOnResult,
                                                                            out condSelector);
                    }
                    else if (m_CurrObject is IDNMcOverlay)
                    {
                        ((IDNMcOverlay)m_CurrObject).GetConditionalSelector(actionType,
                                                                                out actionOnResult,
                                                                                out condSelector);
                    }

                    MCTConditionalSelectorProperty value = new MCTConditionalSelectorProperty(condSelector, actionOnResult);
                    m_dicValues.Add(actionType, value);

                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetConditionalSelector", McEx);
            }
        }


        private void LoadValue(DNEActionType actionType)
        {
            if (actionType != DNEActionType._EAT_NUM && actionType != DNEActionType._EAT_ACTIVITY)
            {
                MCTConditionalSelectorProperty value = m_dicValues[actionType];
                if(value!= null)
                {
                    IDNMcConditionalSelector condSelector = value.ConditionalSelector;
                    //if (condSelector != null)
                    //{
                    IDNMcConditionalSelector[] condSelectors = GetSelectorsArr();
                        for (int i = 0; i < clstConditionalSelector.Items.Count; i++)
                        {
                            if (condSelector == condSelectors[i])
                                clstConditionalSelector.SetItemChecked(i, true);
                            else
                                clstConditionalSelector.SetItemChecked(i, false);
                        }
                    //}
                    

                    chkActionOnResult.Checked = value.ActionOnResult;
                }
            }
        }

        private void SetSelectedSelector()
        {
            try
            {
                bool actionOnResult = false;
                IDNMcConditionalSelector condSelector = null;
                DNEActionType actionType = DNEActionType._EAT_VISIBILITY;

                actionType = (DNEActionType)Enum.Parse(typeof(DNEActionType), cmbActionType.Text);

                if (m_CurrObject is IDNMcObject)
                {
                    ((IDNMcObject)m_CurrObject).GetConditionalSelector(actionType,
                                                                        out actionOnResult,
                                                                        out condSelector);
                }
                else if (m_CurrObject is IDNMcOverlay)
                {
                    ((IDNMcOverlay)m_CurrObject).GetConditionalSelector(actionType,
                                                                            out actionOnResult,
                                                                            out condSelector);
                }

                IDNMcConditionalSelector[] condSelectors = GetSelectorsArr();

                for (int i = 0; i < clstConditionalSelector.Items.Count; i++)
                {
                    if (condSelector == condSelectors[i])
                        clstConditionalSelector.SetItemChecked(i, true);
                    else
                        clstConditionalSelector.SetItemChecked(i, false);
                }

                chkActionOnResult.Checked = actionOnResult;  
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetConditionalSelector", McEx);
            }         
        }

      /*  public IDNMcConditionalSelector[] SelectorsArr
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
                    string name = Manager_MCNames.GetNameByObject((object)value[i], value[i].ConditionalSelectorType.ToString());
                    clstConditionalSelector.Items.Add(name);
                }
            }
        }*/

        public IDNMcConditionalSelector[] GetSelectorsArr()
        {
            return mSelectorsArr;
        }

        public void SetSelectorsArr(IDNMcConditionalSelector[] value)
        {
            mSelectorsArr = value;

            for (int i = 0; i < value.Length; i++)
            {
                string name = Manager_MCNames.GetNameByObject((object)value[i], value[i].ConditionalSelectorType.ToString());
                clstConditionalSelector.Items.Add(name);
            }
        }

        DNEActionType selectedEnumKey;

        private void cmbActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SetSelectedSelector();
            string newSelectedText = cmbActionType.Text;
            if (newSelectedText != selectedEnumKey.ToString())
            {
                IDNMcConditionalSelector selectedSelector;
                IDNMcConditionalSelector[] condSelectors = GetSelectorsArr();

                if (clstConditionalSelector.CheckedItems.Count == 0)
                    selectedSelector = null;
                else
                    selectedSelector = condSelectors[clstConditionalSelector.CheckedIndices[0]];

                MCTConditionalSelectorProperty value = new MCTConditionalSelectorProperty(selectedSelector, chkActionOnResult.Checked);
                m_dicValues[selectedEnumKey] = value;

                selectedEnumKey = (DNEActionType)Enum.Parse(typeof(DNEActionType), newSelectedText);

            }
            LoadValue(selectedEnumKey);
        }

        private void SaveCurrentValue()
        {
            string newSelectedText = cmbActionType.Text;

            IDNMcConditionalSelector selectedSelector;
            IDNMcConditionalSelector[] condSelectors = GetSelectorsArr();
            if (clstConditionalSelector.CheckedItems.Count == 0)
                selectedSelector = null;
            else
                selectedSelector = condSelectors[clstConditionalSelector.CheckedIndices[0]];

            MCTConditionalSelectorProperty value = new MCTConditionalSelectorProperty(selectedSelector, chkActionOnResult.Checked);
            m_dicValues[selectedEnumKey] = value;

        }
        
        private void clstConditionalSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < clstConditionalSelector.Items.Count; i++)
                clstConditionalSelector.SetItemChecked(i, false);
            
            int selectedIndex = clstConditionalSelector.SelectedIndex;

            if (selectedIndex >= 0)
                clstConditionalSelector.SetItemChecked(selectedIndex, true);
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetConditionalSelector", McEx);
            }
        }
    }
}
