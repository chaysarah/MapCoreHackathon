using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucTraversabilityMapLayer : ucCodeLayer, IUserControlItem
    {
        private IDNMcTraversabilityMapLayer m_CurrentObject;

        public ucTraversabilityMapLayer()
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
            m_CurrentObject = (IDNMcTraversabilityMapLayer)aItem;
            base.LoadItem(aItem);

            try
            {
                txtNumTraversabilityDirections.Text = m_CurrentObject.GetNumTraversabilityDirections().ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetNumTraversabilityDirections", McEx);
            }

            base.GetColorTable();
        }
        #endregion

       
    }
}
