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
    public partial class ucEllipseItem : ucClosedShapeItem, IUserControlItem
    {
        private IDNMcEllipseItem m_CurrentObject;
        
        public ucEllipseItem():base()
        {
            InitializeComponent();

            cmbEllipseType.Items.AddRange(Enum.GetNames(typeof(DNEItemGeometryType)));
            cmbEllipseDefinition.Items.AddRange(Enum.GetNames(typeof(DNEEllipseDefinition)));
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcEllipseItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.LoadItem(aItem);
            
            try
            {
                txtEllipseCoordinateSystem.Text = m_CurrentObject.EllipseCoordinateSystem.ToString();
                
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("EllipseCoordinateSystem", McEx);
            }

            try
            {
                cmbEllipseType.Text = m_CurrentObject.GetEllipseType().ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetEllipseType", McEx);
            }

            try
            {
                ctrlObjStatePropertyStartAngle.Load(m_CurrentObject.GetStartAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetStartAngle", McEx);
            }

            try
            {
                ctrlObjStatePropertyEndAngle.Load(m_CurrentObject.GetEndAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetEndAngle", McEx);
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
            
            try
            {
                ctrlObjStatePropertyInnerRadiusFactor.Load(m_CurrentObject.GetInnerRadiusFactor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetInnerRadiusFactor", McEx);
            }

            try
            {
                cmbEllipseDefinition.Text = m_CurrentObject.GetEllipseDefinition().ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetEllipseDefinition", McEx);
            }

            try
            {
                chxFillTexturePolarMapping.Checked = m_CurrentObject.GetFillTexturePolarMapping();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetFillTexturePolarMapping", McEx);
            }
        }

        #endregion

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);

            base.SaveItem();
            try
            {
                m_CurrentObject.SetEllipseType((DNEItemGeometryType)Enum.Parse(typeof(DNEItemGeometryType), cmbEllipseType.Text));
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("EllipseType", McEx);
            }

            try
            {
                ctrlObjStatePropertyStartAngle.Save(m_CurrentObject.SetStartAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetStartAngle", McEx);
            }

            try
            {

                ctrlObjStatePropertyEndAngle.Save(m_CurrentObject.SetEndAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetEndAngle", McEx);
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

            try
            {
                ctrlObjStatePropertyInnerRadiusFactor.Save(m_CurrentObject.SetInnerRadiusFactor);               
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetInnerRadiusFactor", McEx);
            }

            try
            {
                m_CurrentObject.SetEllipseDefinition((DNEEllipseDefinition)Enum.Parse(typeof(DNEEllipseDefinition), cmbEllipseDefinition.Text));
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetEllipseDefinition", McEx);
            }

            try
            {
                m_CurrentObject.SetFillTexturePolarMapping(chxFillTexturePolarMapping.Checked);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetFillTexturePolarMapping", McEx);
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }
    }
}
