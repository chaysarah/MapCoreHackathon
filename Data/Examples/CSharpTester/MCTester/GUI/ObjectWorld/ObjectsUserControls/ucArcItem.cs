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
    public partial class ucArcItem : ucLineBasedItem, IUserControlItem
    {
        private IDNMcArcItem m_CurrentObject;
        //private uint m_PropId;
        //private float m_fParam;
        
        public ucArcItem():base()
        {
            InitializeComponent();
            cmbEllipseDefinition.Items.AddRange(Enum.GetNames(typeof(DNEEllipseDefinition)));
            cmbArcType.Items.AddRange(Enum.GetNames(typeof(DNEItemGeometryType)));
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcArcItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.LoadItem(aItem);

            try
            {
                txtArcCoordinateSystem.Text = m_CurrentObject.EllipseCoordinateSystem.ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("EllipseCoordinateSystem", McEx);
            }

            try
            {
                cmbArcType.Text = m_CurrentObject.GetEllipseType().ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetEllipseType", McEx);
            }

            try
            {
                ctrlObjStatePropertyArcStartAngle.Load(m_CurrentObject.GetStartAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetStartAngle", McEx);
            }

            try
            {
                ctrlObjStatePropertyArcEndAngle.Load(m_CurrentObject.GetEndAngle);
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
                cmbEllipseDefinition.Text = m_CurrentObject.GetEllipseDefinition().ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetEllipseDefinition", McEx);
            }
        }

        #endregion

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.SaveItem();

            try
            {
                m_CurrentObject.SetEllipseType((DNEItemGeometryType)Enum.Parse(typeof(DNEItemGeometryType), cmbArcType.Text));
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetEllipseType", McEx);
            }

            try
            {
                ctrlObjStatePropertyArcStartAngle.Save(m_CurrentObject.SetStartAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetStartAngle", McEx);
            }

            try
            {
                ctrlObjStatePropertyArcEndAngle.Save(m_CurrentObject.SetEndAngle);   
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
                m_CurrentObject.SetEllipseDefinition((DNEEllipseDefinition)Enum.Parse(typeof(DNEEllipseDefinition), cmbEllipseDefinition.Text));
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetEllipseDefinition", McEx);
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }
    }
}
