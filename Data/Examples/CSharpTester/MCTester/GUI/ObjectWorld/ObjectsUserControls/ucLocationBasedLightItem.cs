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
    public partial class ucLocationBasedLightItem : ucLightBaseItem , IUserControlItem
    {
        //private uint m_PropId;
        //private DNSMcAttenuation m_AttenuationParam;
        private IDNMcLocationBasedLightItem m_CurrentObject;

        public ucLocationBasedLightItem()
        {
            InitializeComponent();
        }
                
        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            
            base.SaveItem();

            try
            {
                ctrlObjStatePropertyAttenuation1.Save(m_CurrentObject.SetAttenuation);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetAttenuation", McEx);
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcLocationBasedLightItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.LoadItem(aItem);

            try
            {
                ctrlObjStatePropertyAttenuation1.Load(m_CurrentObject.GetAttenuation);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetAttenuation", McEx);
            }
        }

        #endregion

        
    }
}
