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
    public partial class ucTextItem : ucSymbolicItem, IUserControlItem
    {
        private IDNMcTextItem m_CurrentObject;
		
        public ucTextItem()
            : base()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcTextItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.LoadItem(aItem);

            try
            {
                ctrlObjStatePropertyENeverUpsideDownMode1.Load(m_CurrentObject.GetNeverUpsideDownMode);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetNeverUpsideDownMode", McEx);
            }

            try
            {
                txtTextCoordinateSystem.Text = m_CurrentObject.TextCoordinateSystem.ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("TextCoordinateSystem", McEx);
            }

            try
            {
                ctrlObjStatePropertyString1.Load(m_CurrentObject.GetText);

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetText", McEx);
            }

           
            try
            {
                ctrlObjStatePropertyFont.Load(m_CurrentObject.GetFont);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetFont", McEx);
            }

            try
            {
                ctrlObjStatePropertyTextAlignment.Load(m_CurrentObject.GetTextAlignment);
              
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTextAlignment", McEx);
            }

            try
            {
                ctrlObjStatePropertyRightToLeftReadingOrder.Load(m_CurrentObject.GetRightToLeftReadingOrder);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRightToLeftReadingOrder", McEx);
            }

            try
            {
                ctrlObjStatePropertyTextColor.Load(m_CurrentObject.GetTextColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTextColor", McEx);
            }            

            try
            {
                ctrlObjStatePropertyScale.Load(m_CurrentObject.GetScale);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetScale", McEx);
            }

            try
            {
                ctrlObjStatePropertyMargin.Load(m_CurrentObject.GetMargin);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetMargin", McEx);
            }

            try
            {
                ctrlObjStatePropertyBackgroundColor.Load(m_CurrentObject.GetBackgroundColor);               
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetBackgroundColor", McEx);
            }

            try
            {
                ctrlObjStatePropertyEBoundingRectanglePoint1.Load(m_CurrentObject.GetRectAlignment);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRectAlignment", McEx);
            }

            try
            {
                ctrlObjStatePropertyOutlineColor.Load(m_CurrentObject.GetOutlineColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetOutlineColor", McEx);
            }

            try
            {
                ctrlObjStatePropertyMarginY.Load(m_CurrentObject.GetMarginY);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetMarginY", McEx);
            }

            try
            {
                ctrlObjStatePropertyEBackgroundShape1.Load(m_CurrentObject.GetBackgroundShape);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetBackgroundShape", McEx);
            }
        }

        #endregion

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);

            base.SaveItem();

            try
            {
                ctrlObjStatePropertyENeverUpsideDownMode1.Save(m_CurrentObject.SetNeverUpsideDownMode);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetNeverUpsideDownMode", McEx);
            }

            try
            {
                ctrlObjStatePropertyString1.Save(m_CurrentObject.SetText);

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetText", McEx);
            }

            try
            {
                ctrlObjStatePropertyFont.Save(m_CurrentObject.SetFont);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetFont", McEx);
            }

            try
            {
                ctrlObjStatePropertyTextAlignment.Save(m_CurrentObject.SetTextAlignment);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTextAlignment", McEx);
            }

            try
            {
                ctrlObjStatePropertyRightToLeftReadingOrder.Save(m_CurrentObject.SetRightToLeftReadingOrder);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetRightToLeftReadingOrder", McEx);
            }

            try
            {
                ctrlObjStatePropertyTextColor.Save(m_CurrentObject.SetTextColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTextColor", McEx);
            }            

            try
            {
                ctrlObjStatePropertyScale.Save(m_CurrentObject.SetScale);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetScale", McEx);
            }

            try
            {
                ctrlObjStatePropertyMargin.Save(m_CurrentObject.SetMargin);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetMargin", McEx);
            }

            try
            {
                ctrlObjStatePropertyBackgroundColor.Save(m_CurrentObject.SetBackgroundColor);         
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetBackgroundColor", McEx);
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
                ctrlObjStatePropertyOutlineColor.Save(m_CurrentObject.SetOutlineColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetOutlineColor", McEx);
            }

            try
            {
                ctrlObjStatePropertyMarginY.Save(m_CurrentObject.SetMarginY);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetMarginY", McEx);
            }

            try
            {
                ctrlObjStatePropertyEBackgroundShape1.Save(m_CurrentObject.SetBackgroundShape);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetBackgroundShape", McEx);
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        private void ucTextItem_Load(object sender, EventArgs e)
        {

        }

    }
}
