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
    public partial class ucClosedShapeItem : ucLineBasedItem, IUserControlItem
    {
        private IDNMcClosedShapeItem m_CurrentObject;
        //private uint m_PropId;
        //private float m_fProp;
        //private DNSMcBColor m_BColor;
        //private IDNMcTexture m_Texture;
        protected DNSMcFVector2D m_FVector2DParam;

        public ucClosedShapeItem():base()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcClosedShapeItem)aItem;

            base.LoadItem(aItem);

            try
            {
                ctrlObjStatePropertyFillStyle.Load(m_CurrentObject.GetFillStyle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetFillStyle", McEx);
            }

            try
            {
                ctrlObjStatePropertyFillColor.Load(m_CurrentObject.GetFillColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetFillColor", McEx);
            }

            try
            {
                ctrlObjStatePropertyFillTexture.Load(m_CurrentObject.GetFillTexture);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetFillTexture", McEx);
            }

            try
            {
                ctrlObjStatePropertyFillTextureScale.Load(m_CurrentObject.GetFillTextureScale);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetFillTextureScale", McEx);
            }

           
          
        }

        #endregion

        protected override void SaveItem()
        {
            base.SaveItem();

          

            try
            {
                ctrlObjStatePropertyFillStyle.Save(m_CurrentObject.SetFillStyle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetFillStyle", McEx);
            }

            try
            {
                ctrlObjStatePropertyFillColor.Save(m_CurrentObject.SetFillColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetFillColor", McEx);
            }

            try
            {
                ctrlObjStatePropertyFillTexture.Save(m_CurrentObject.SetFillTexture);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSidesFillTexture", McEx);
            }

            try
            {
                ctrlObjStatePropertyFillTextureScale.Save(m_CurrentObject.SetFillTextureScale);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetFillTextureScale", McEx);
            }

         
           

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        private void ucClosedShapeItem_Load(object sender, EventArgs e)
        {

        }

       

    }
}
