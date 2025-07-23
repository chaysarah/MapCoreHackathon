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
    public partial class ucImageProcessing : UserControl, IUserControlItem
    {
        //private IDNMcMapImageProcessing m_CurrentObject;

        public ucImageProcessing()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {
            //m_CurrentObject = (IDNMcMapImageProcessing)aItem;
        }
        #endregion
    }
}
