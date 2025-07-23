using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.GUI.Trees;
using MCTester.Managers;

namespace MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms
{
    public partial class ucBaseConditionalSelector : UserControl, IUserControlItem
    {
        private IDNMcConditionalSelector m_CurrentCondSelector;
        protected bool m_IsCanCloseFrom = true;
        public ucBaseConditionalSelector()
        {
            InitializeComponent();
            cmbConditionalSelectorType.Items.AddRange(Enum.GetNames(typeof(DNEConditionalSelectorType)));
        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {
            m_CurrentCondSelector = (IDNMcConditionalSelector)aItem;

            try
            {
                ntxConditionalSelectorID.SetUInt32(m_CurrentCondSelector.GetID());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ID", McEx);
            }

            try
            {
                cmbConditionalSelectorType.Text = m_CurrentCondSelector.ConditionalSelectorType.ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ConditionalSelectorType", McEx);
            }
        }

        #endregion

        protected virtual void Save()
        {
            try
            {
                m_CurrentCondSelector.SetID(ntxConditionalSelectorID.GetUInt32());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ID", McEx);
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Form ContainerForm = GeneralFuncs.GetParentForm(this);

            // close the form only in case that this is creation of new selector
            if (!(ContainerForm is MCObjectWorldTreeViewForm))
            {
                if (this is ucLocationConditionalSelector)
                    ((ucLocationConditionalSelector)this).Cancel();
                
            }

            ContainerForm.Close();
           
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Save();

            if (m_IsCanCloseFrom)
            {
                GeneralFuncs.CloseParentForm(this);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.Save();
        }
    }
}
