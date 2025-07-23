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
    public partial class ucBlockedConditionalSelector : ucBaseConditionalSelector, IUserControlItem
    {
        private IDNMcBlockedConditionalSelector m_CurrentCondSelector;

        public ucBlockedConditionalSelector()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentCondSelector = (IDNMcBlockedConditionalSelector)aItem;
            base.LoadItem(aItem);
        }

        #endregion

        protected override void Save()
        {
            base.Save();
        }

        public IDNMcBlockedConditionalSelector BlockedCondSelector
        {
            get { return m_CurrentCondSelector; }
            set { m_CurrentCondSelector = value; }
        }
    }
}
