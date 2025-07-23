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
    public partial class ucRawVector3DExtrusionLayer : ucVector3DExtrusionLayer, IUserControlItem
    {
        IDNMcRawVector3DExtrusionMapLayer m_CurrentObject;

        public ucRawVector3DExtrusionLayer()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcRawVector3DExtrusionMapLayer)aItem;
            base.LoadItem(aItem);
        }

        protected override void SaveItem()
        {
            base.SaveItem();
        }
        #endregion
    }
}
