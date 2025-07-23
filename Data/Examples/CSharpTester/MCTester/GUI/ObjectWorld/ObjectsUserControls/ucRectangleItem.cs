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
    public partial class ucRectangleItem : ucClosedShapeItem, IUserControlItem
    {
        private IDNMcRectangleItem m_CurrentObject;

        public ucRectangleItem()
            : base()
        {
            InitializeComponent();

            cmbEllipseType.Items.AddRange(Enum.GetNames(typeof(DNEItemGeometryType)));
            cmbRectangleDefinition.Items.AddRange(Enum.GetNames(typeof(DNERectangleDefinition)));
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcRectangleItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.LoadItem(aItem);
            
            try
            {
                txtRectangleCoordinateSystem.Text = m_CurrentObject.RectangleCoordinateSystem.ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("RectangleCoordinateSystem", McEx);
            }

            try
            {
                cmbEllipseType.Text = m_CurrentObject.GetRectangleType().ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRectangleType", McEx);
            }

            try
            {
                cmbRectangleDefinition.Text = m_CurrentObject.GetRectangleDefinition().ToString();                
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRectangleDefinition", McEx);
            }

            try
            {
                ctrlObjStatePropertyRadiusX.Load(m_CurrentObject.GetRadiusX);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRadiusX", McEx);
            }

            try
            {
                ctrlObjStatePropertyRadiusY.Load(m_CurrentObject.GetRadiusY);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRadiusY", McEx);
            }
        }

        #endregion

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);

            base.SaveItem();

            try
            {
                DNERectangleDefinition eRectDef = (DNERectangleDefinition)Enum.Parse(typeof(DNERectangleDefinition), cmbRectangleDefinition.Text);
                m_CurrentObject.SetRectangleDefinition(eRectDef);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetRectangleDefinition", McEx);
            }

            try
            {
                m_CurrentObject.SetRectangleType((DNEItemGeometryType)Enum.Parse(typeof(DNEItemGeometryType), cmbEllipseType.Text));
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetRectangleType", McEx);
            }

            try
            {
                ctrlObjStatePropertyRadiusX.Save(m_CurrentObject.SetRadiusX);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetRadiusX", McEx);
            }

            try
            {
                ctrlObjStatePropertyRadiusY.Save(m_CurrentObject.SetRadiusY);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetRadiusY", McEx);
            }
        }

    }
}
