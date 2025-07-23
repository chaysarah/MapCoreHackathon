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
    public partial class ucNativeServerRasterLayer : ucRasterLayer,IUserControlItem
    {
        private IDNMcNativeServerRasterMapLayer m_CurrentObject;

        public ucNativeServerRasterLayer()
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
            m_CurrentObject = (IDNMcNativeServerRasterMapLayer)aItem;
            base.LoadItem(aItem);

        }

        #endregion
    }
}
