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
    public partial class ucGlobalMap : UserControl,IUserControlItem
    {
        private IDNMcMapGlobal m_CurrentObject;
        
        public ucGlobalMap()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcMapGlobal)aItem;
        }
        #endregion
    }
}
