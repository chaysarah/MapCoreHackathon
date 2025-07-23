using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ucLineExpansionItem: ucClosedShapeItem, IUserControlItem
    {
        private IDNMcLineExpansionItem m_CurrentObject;

        public ucLineExpansionItem()
            : base()
        {
            InitializeComponent();

            cmbEllipseType.Items.AddRange(Enum.GetNames(typeof(DNEItemGeometryType)));

        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcLineExpansionItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);

            base.LoadItem(aItem);

           

            try
            {
                DNEMcPointCoordSystem eLineExpansionCoordinateSystem = new DNEMcPointCoordSystem();
                m_CurrentObject.GetLineExpansionCoordinateSystem(out eLineExpansionCoordinateSystem);
                txtLineExpansionCoordinateSystem.Text = eLineExpansionCoordinateSystem.ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNEMcPointCoordSystem:GetLineExpansionCoordinateSystem", McEx);
            }

            try
            {
                cmbEllipseType.Text = m_CurrentObject.GetLineExpansionType().ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetLineExpansionType", McEx);
            }

            try
            {
                ctrlObjStatePropertyRadius.Load(m_CurrentObject.GetRadius);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRadius", McEx);
            }
        }

        #endregion

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);

            base.SaveItem();

            try
            {
                m_CurrentObject.SetLineExpansionType((DNEItemGeometryType)Enum.Parse(typeof(DNEItemGeometryType), cmbEllipseType.Text));
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetLineExpansionType", McEx);
            }

            try
            {
                ctrlObjStatePropertyRadius.Save(m_CurrentObject.SetRadius);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetRadius", McEx);
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }
               
    }
}
