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
    public partial class ucPictureItem : ucSymbolicItem, IUserControlItem
    {
        private IDNMcPictureItem m_CurrentObject;
        // private uint m_PropId;
        //private float m_fParam;
        

        public ucPictureItem()
            : base()
        {
            InitializeComponent();
            
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcPictureItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.LoadItem(aItem);

            try
            {
                ctrlObjStatePropertyIsSizeFactor.Load(m_CurrentObject.GetIsSizeFactor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetIsSizeFactor", McEx);
            }

            try
            {
                txtPictureCoordinateSystem.Text = m_CurrentObject.PictureCoordinateSystem.ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("PictureCoordinateSystem", McEx);
            }

            try
            {
                chxPictureIsUseTextureGeoReferencing.Checked = m_CurrentObject.IsUsingTextureGeoReferencing;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("IsUsingTextureGeoReferencing", McEx);
            }

            try
            {
                ctrlObjStatePropertyWidth.Load(m_CurrentObject.GetWidth);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetWidth", McEx);
            }

            try
            {
                ctrlObjStatePropertyHeight.Load(m_CurrentObject.GetHeight);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetHeight", McEx);
            }

            try
            {
                ctrlObjStatePropertyTexture1.Load(m_CurrentObject.GetTexture);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTexture", McEx);
            }

           
            try
            {
                ctrlObjStatePropertyTextureColor.Load(m_CurrentObject.GetTextureColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTextureColor", McEx);
            }

            try
            {
                ctrlObjStatePropertyNeverUpsideDown.Load(m_CurrentObject.GetNeverUpsideDown);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetNeverUpsideDown", McEx);
            }

            try
            {
                ctrlObjStatePropertyEBoundingRectanglePoint1.Load(m_CurrentObject.GetRectAlignment);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRectAlignment", McEx);
            }
        }

        #endregion

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.SaveItem();

            try
            {
                ctrlObjStatePropertyWidth.Save(m_CurrentObject.SetWidth);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetWidth", McEx);
            }

            try
            {
                ctrlObjStatePropertyHeight.Save(m_CurrentObject.SetHeight);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetHeight", McEx);
            }

            try
            {
                ctrlObjStatePropertyTexture1.Save(m_CurrentObject.SetTexture);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTexture", McEx);
            }

            try
            {
                ctrlObjStatePropertyTextureColor.Save(m_CurrentObject.SetTextureColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTextureColor", McEx);
            }

            try
            {
                ctrlObjStatePropertyEBoundingRectanglePoint1.Save(m_CurrentObject.SetRectAlignment);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetRectAlignment", McEx);
            }

            try
            {
                ctrlObjStatePropertyNeverUpsideDown.Save(m_CurrentObject.SetNeverUpsideDown);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetNeverUpsideDown", McEx);
            }

            try
            {
                ctrlObjStatePropertyIsSizeFactor.Save(m_CurrentObject.SetIsSizeFactor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetNeverUpsideDown", McEx);
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

    }
}
