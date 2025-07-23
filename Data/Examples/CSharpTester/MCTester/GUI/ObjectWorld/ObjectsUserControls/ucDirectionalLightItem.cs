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
    public partial class ucDirectionalLightItem : ucLightBaseItem, IUserControlItem
    {
        //private uint m_PropId;
        //private DNSMcFVector3D m_FVector3DParam;
        private IDNMcDirectionalLightItem m_CurrentObject;

        public ucDirectionalLightItem()
        {
            InitializeComponent();
        }

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);

            base.SaveItem();

            try
            {
                ctrlPropertyDirection1.Save(m_CurrentObject.SetDirection);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetDirection", McEx);
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        
        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcDirectionalLightItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.LoadItem(aItem);

            try
            {
                ctrlPropertyDirection1.Load(m_CurrentObject.GetDirection);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetDirection", McEx);
            }
        }

        #endregion
        }
}
