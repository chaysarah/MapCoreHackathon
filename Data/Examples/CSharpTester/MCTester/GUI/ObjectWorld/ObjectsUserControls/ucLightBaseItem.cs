using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ucLightBaseItem : ucPhysicalItem , IUserControlItem
    {
        //private uint m_PropId;
        //private DNSMcFColor m_FColor;
        private IDNMcLightBasedItem m_CurrentObject;

        public ucLightBaseItem()
        {
            InitializeComponent();
        }
                
        protected override void SaveItem()
        {
            base.SaveItem();

            try
            {
                ctrlObjStatePropertyDiffuseColor.Save(m_CurrentObject.SetDiffuseColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetDiffuseColor", McEx);
            }

            try
            {
                ctrlObjStatePropertySpecularColor.Save(m_CurrentObject.SetSpecularColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSpecularColor", McEx);
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcLightBasedItem)aItem;
            base.LoadItem(aItem);

            try
            {
                ctrlObjStatePropertyDiffuseColor.Load(m_CurrentObject.GetDiffuseColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetDiffuseColor", McEx);
            }

            try
            {
                ctrlObjStatePropertySpecularColor.Load(m_CurrentObject.GetSpecularColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetSpecularColor", McEx);
            } 
        }

        #endregion
    }
}
