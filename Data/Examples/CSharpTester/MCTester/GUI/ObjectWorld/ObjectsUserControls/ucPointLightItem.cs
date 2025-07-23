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

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ucPointLightItem : ucLocationBasedLightItem, IUserControlItem
    {
        private IDNMcPointLightItem m_CurrentObject;

        public ucPointLightItem()
        {
            InitializeComponent();
        }
        
        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcPointLightItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.LoadItem(aItem);
        }

        #endregion
    }
}
