using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MapCore.Common;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucNativeServerVectorLayer : ucVectorLayer, IUserControlItem
    {
        private IDNMcNativeServerVectorMapLayer m_CurrentObject;

        public ucNativeServerVectorLayer()
        {
            InitializeComponent();
            SetLocalCacheEnabled(false);
        }

        protected override void SaveItem()
        {
            base.SaveItem();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcNativeServerVectorMapLayer)aItem;
            base.LoadItem(aItem);
        }

        #endregion

        private void ucVectorNativeLayer_Load(object sender, EventArgs e)
        {
            
        }
    }
}