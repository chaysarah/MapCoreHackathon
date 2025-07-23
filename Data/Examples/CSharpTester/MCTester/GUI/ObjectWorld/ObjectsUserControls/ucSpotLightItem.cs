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
    public partial class ucSpotLightItem : ucLocationBasedLightItem, IUserControlItem
    {
        //private uint m_PropId;
        //private DNSMcFVector3D m_FVector3DParam;
        //private float m_fParam;
        private IDNMcSpotLightItem m_CurrentObject;

        public ucSpotLightItem()
        {
            InitializeComponent();
        }

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);

            base.SaveItem();

            try
            {
                ctrlPropertySpotDirection.Save(m_CurrentObject.SetDirection);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetDirection", McEx);
            }

            try
            {
                ctrlObjStatePropertyHalfOuterAngle.Save(m_CurrentObject.SetHalfOuterAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetHalfOuterAngle", McEx);
            }

            try
            {
                ctrlObjStatePropertyHalfInnerAngle.Save(m_CurrentObject.SetHalfInnerAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetHalfInnerAngle", McEx);
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        #region IUserControlItem Members


        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcSpotLightItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.LoadItem(aItem);

            try
            {
                ctrlPropertySpotDirection.Load(m_CurrentObject.GetDirection);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetDirection", McEx);
            }

            try
            {
                ctrlObjStatePropertyHalfOuterAngle.Load(m_CurrentObject.GetHalfOuterAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetHalfOuterAngle", McEx);
            }

            try
            {
                ctrlObjStatePropertyHalfInnerAngle.Load(m_CurrentObject.GetHalfInnerAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetHalfInnerAngle", McEx);
            }
        }

        #endregion

        private void Tab_LightBaseItem_Click(object sender, EventArgs e)
        {

        }
    }
}
