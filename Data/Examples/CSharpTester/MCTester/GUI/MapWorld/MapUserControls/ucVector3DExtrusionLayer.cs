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
using MapCore.Common;
using MCTester.Managers.MapWorld;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucVector3DExtrusionLayer : ucStaticObjectsLayer, IUserControlItem
    {
        IDNMcVector3DExtrusionMapLayer m_CurrentObject;

        public ucVector3DExtrusionLayer()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcVector3DExtrusionMapLayer)aItem;
            base.LoadItem(aItem);

            try
            {
                chxIsExtrusionHeightChangeSupported.Checked = m_CurrentObject.IsExtrusionHeightChangeSupported();
                chxIsExtrusionHeightChangeSupported.Text = "Is Extrusion Height Change Supported";
            }
            catch (MapCoreException McEx)
            {
                if (McEx.ErrorCode == DNEMcErrorCode.NOT_INITIALIZED)
                {
                    chxIsExtrusionHeightChangeSupported.Text = "Is Extrusion Height Change Supported (not initialized)";
                }
                else
                    Utilities.ShowErrorMessage("IsExtrusionHeightChangeSupported", McEx);
            }

        }

        protected override void SaveItem()
        {
            base.SaveItem();
        }
        #endregion
    }
}
