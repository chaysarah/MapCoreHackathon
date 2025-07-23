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
    public partial class ucVectorNativeLayer : ucVectorLayer, IUserControlItem
    {
        private IDNMcNativeVectorMapLayer m_CurrentObject;

        public ucVectorNativeLayer()
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
            m_CurrentObject = (IDNMcNativeVectorMapLayer)aItem;
            base.LoadItem(aItem);
        }

        #endregion

        private void ucVectorNativeLayer_Load(object sender, EventArgs e)
        {
            
        }

       

       
    }
}