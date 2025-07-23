using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucNativeServerVector3DExtrusionLayer : ucVector3DExtrusionLayer, IUserControlItem
    {
        IDNMcNativeServerVector3DExtrusionMapLayer m_CurrentObject;

        public ucNativeServerVector3DExtrusionLayer():base()
        {
            InitializeComponent();
            SetLocalCacheEnabled(false);
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcNativeServerVector3DExtrusionMapLayer)aItem;
            base.LoadItem(aItem);
        }

        protected override void SaveItem()
        {
            base.SaveItem();
        }
        #endregion
    }
}
