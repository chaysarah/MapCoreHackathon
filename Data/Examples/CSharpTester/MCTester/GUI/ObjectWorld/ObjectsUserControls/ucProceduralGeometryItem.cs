using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;
using MapCore;
using MCTester.Managers;
using MapCore.Common;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ucProceduralGeometryItem : ucSymbolicItem, IUserControlItem
    {
        private IDNMcProceduralGeometryItem m_CurrentObject;

        public ucProceduralGeometryItem()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcProceduralGeometryItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);

            try
            {
                tbCoordinateSystem.Text = m_CurrentObject.ProceduralGeometryCoordinateSystem.ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ProceduralGeometryCoordinateSystem", McEx);
            }
            base.LoadItem(aItem);
        }

        #endregion

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);

            base.SaveItem();

        }
    }
}
