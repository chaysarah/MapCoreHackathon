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
    public partial class ucTraversabilityRawMapLayer : ucTraversabilityMapLayer, IUserControlItem
    {
        private IDNMcRawTraversabilityMapLayer m_CurrentObject;

        public ucTraversabilityRawMapLayer()
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
            m_CurrentObject = (IDNMcRawTraversabilityMapLayer)aItem;
            base.LoadItem(aItem);

            ctrlRawComponents1.SetMCObject(m_CurrentObject, false);
            ctrlRawResolutions1.SetResolutions(m_CurrentObject);

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
