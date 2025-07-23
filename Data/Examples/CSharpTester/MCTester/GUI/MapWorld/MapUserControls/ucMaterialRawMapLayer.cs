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
    public partial class ucMaterialRawMapLayer : ucMaterialMapLayer, IUserControlItem
    {
        private IDNMcRawMaterialMapLayer m_CurrentObject;

        public ucMaterialRawMapLayer()
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
            m_CurrentObject = (IDNMcRawMaterialMapLayer)aItem;
            base.LoadItem(aItem);

            ctrlRawComponents1.SetMCObject(m_CurrentObject, false);
            ctrlRawResolutions1.SetResolutions(m_CurrentObject);
           
            base.GetColorTable();
        }
        #endregion
    }
}
