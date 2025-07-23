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
    public partial class ucVectorRawLayer : ucVectorLayer, IUserControlItem
    {
        private IDNMcRawVectorMapLayer m_CurrentObject;

        public ucVectorRawLayer()
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
            m_CurrentObject = (IDNMcRawVectorMapLayer)aItem;
            base.LoadItem(aItem);

            try
            {
                cbIsRasterizedVectorLayer.Checked = m_CurrentObject.IsRasterizedVectorLayer();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("IsRasterizedVectorLayer", McEx);
            }  
        }

        #endregion
    }
}
