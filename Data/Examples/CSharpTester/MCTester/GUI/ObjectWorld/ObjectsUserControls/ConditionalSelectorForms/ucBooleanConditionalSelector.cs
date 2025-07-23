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

namespace MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms
{
    public partial class ucBooleanConditionalSelector : ucBaseConditionalSelector,IUserControlItem
    {
        private IDNMcBooleanConditionalSelector m_CurrentCondSelector;
        private IDNMcConditionalSelector[] m_ExistingSelectors;
        private IDNMcConditionalSelector[] m_BondSelectors;
        private IDNMcOverlayManager m_OverlayManager;
        
        public ucBooleanConditionalSelector()
        {
            InitializeComponent();
            cmbOperation.Items.AddRange(Enum.GetNames(typeof(DNEBooleanOp)));
            cmbOperation.Text = DNEBooleanOp._EB_OR.ToString();            
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentCondSelector = (IDNMcBooleanConditionalSelector)aItem;
            m_OverlayManager = m_CurrentCondSelector.GetOverlayManager();

            base.LoadItem(aItem);
            FillSelectorList();
            
            try
            {
                m_BondSelectors = m_CurrentCondSelector.GetListOfSelectors();
                MarkActiveSelector();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetListOfSelectors", McEx);
            }

            try
            {
                cmbOperation.Text = m_CurrentCondSelector.BooleanOperation.ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("BooleanOperation", McEx);
            }
        }

        #endregion

        public void FillSelectorList()
        {
            clstCondSelectors.Items.Clear();
            m_ExistingSelectors = m_OverlayManager.GetConditionalSelectors();
            // todo
            clstCondSelectors.Items.AddRange(CheckedListBoxDisplayMembers());
        }

        public void MarkActiveSelector()
        {
            if (m_BondSelectors.Length > 0)
            {
                for (int i = 0; i < m_ExistingSelectors.Length; i++)
                {
                    for (int z = 0; z < m_BondSelectors.Length; z++ )
                    {
                        if (m_ExistingSelectors[i] == m_BondSelectors[z])
                        {
                            clstCondSelectors.SetItemChecked(i, true);
                        }                        
                    }
                }
            }
        }

        protected override void Save()
        {
            base.Save();

            try
            {
                IDNMcConditionalSelector[] checkedSelector = new IDNMcConditionalSelector[clstCondSelectors.CheckedItems.Count];
                int idx = 0;
                for (int i = 0; i < m_ExistingSelectors.Length; i++)
                {
                    if (clstCondSelectors.GetItemChecked(i))
                    {
                        checkedSelector[idx] = m_ExistingSelectors[i];
                        idx++;
                    }
                }

                m_CurrentCondSelector.SetListOfSelectors(checkedSelector);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetListOfSelectors", McEx);
            }

            try
            {
                m_CurrentCondSelector.BooleanOperation = (DNEBooleanOp)Enum.Parse(typeof(DNEBooleanOp), cmbOperation.Text);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("BooleanOperation", McEx);
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        public IDNMcBooleanConditionalSelector BooleanCondSelector
        {
            get { return m_CurrentCondSelector; }
            set { m_CurrentCondSelector = value; }
        }

        public string [] CheckedListBoxDisplayMembers()
        {
            string[] Ret = new string[m_ExistingSelectors.Length];
            for (int i = 0; i < m_ExistingSelectors.Length; i++)
            {
                Ret[i] = Manager_MCNames.GetNameByObject(m_ExistingSelectors[i], ((IDNMcConditionalSelector)m_ExistingSelectors[i]).ConditionalSelectorType.ToString());
            }

            return Ret;
        }
    }
}
