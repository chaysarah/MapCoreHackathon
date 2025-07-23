using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms
{
    public partial class ucObjectStateConditionalSelector : ucBaseConditionalSelector, IUserControlItem
    {
        private IDNMcObjectStateConditionalSelector m_CurrentCondSelector;

        public ucObjectStateConditionalSelector()
        {
            InitializeComponent();
        }



        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentCondSelector = (IDNMcObjectStateConditionalSelector)aItem;

            base.LoadItem(aItem);
            ntxObjectState.SetByte(m_CurrentCondSelector.GetObjectState());
        }

        #endregion

        protected override void Save()
        {
            m_CurrentCondSelector.SetObjectState(ntxObjectState.GetByte());
            base.Save();
        }

        public IDNMcObjectStateConditionalSelector SelectionCondSelector
        {
            get { return m_CurrentCondSelector; }
            set { m_CurrentCondSelector = value; }
        }

        private void ntxObjectState_Validating(object sender, CancelEventArgs e)
        {
            byte b;
            if (!byte.TryParse(ntxObjectState.Text,out b))
            {
                errorProvider1.SetError(ntxObjectState, "Object state must have a byte value (0..255)");
                e.Cancel = true;
            }
        }

        private void ntxObjectState_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(ntxObjectState, "");
        }
    }
}
