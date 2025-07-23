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
    public partial class ucNative3DModelLayer : uc3DModelLayer, IUserControlItem
    {
        IDNMcNative3DModelMapLayer m_CurrentObject;

        public ucNative3DModelLayer()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcNative3DModelMapLayer)aItem;
            base.LoadItem(aItem);
        }

        protected override void SaveItem()
        {
            base.SaveItem();
        }
        #endregion
    }
}
