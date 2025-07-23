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
    public partial class ucMaterialMapLayer : ucCodeLayer, IUserControlItem
    {
        private IDNMcMaterialMapLayer m_CurrentObject;

        public ucMaterialMapLayer()
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
            m_CurrentObject = (IDNMcMaterialMapLayer)aItem;
            base.LoadItem(aItem);

           /* try
            {
                txtNumMaterialDirections.Text = m_CurrentObject.GetNumMaterialDirections().ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetNumMaterialDirections", McEx);
            }
*/
            base.GetColorTable();
        }
        #endregion
    }
}
