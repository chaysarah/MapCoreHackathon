using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucDTMNativeLayer : ucDTMLayer, IUserControlItem
    {
        private IDNMcDtmMapLayer m_CurrentObject;

        public ucDTMNativeLayer()
        {
            InitializeComponent();
        }

        protected override void SaveItem()
        {
            base.SaveItem();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcNativeDtmMapLayer)aItem;
            base.LoadItem(aItem);
        }

        #endregion
    }
}
