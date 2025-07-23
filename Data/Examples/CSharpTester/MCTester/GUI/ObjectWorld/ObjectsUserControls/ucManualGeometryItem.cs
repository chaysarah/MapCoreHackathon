using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;
using MapCore;
using MCTester.Managers;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ucManualGeometryItem : ucProceduralGeometryItem, IUserControlItem
    {
        private IDNMcManualGeometryItem m_CurrentObject;
        //private uint m_PropID;

        public ucManualGeometryItem()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcManualGeometryItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject); 
            base.LoadItem(aItem);

            //DNERenderingMode RenderingMode;
            ctrlObjStatePropertyManualGeometryRenderingMode.OnERenderingModeChanged += CtrlObjStatePropertyManualGeometryRenderingMode_OnERenderingModeChanged;
            try
            {
                ctrlObjStatePropertyManualGeometryRenderingMode.Load(m_CurrentObject.GetRenderingMode);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRenderingMode", McEx);
            }
                

            try
            {
                ctrlObjStatePropertyTexture.Load(m_CurrentObject.GetTexture);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTexture", McEx);
            }

            GetPointsColors();

            GetPointsCoordinates();

            GetPointsTextureCoordinates();

            GetConnectionIndices();
            //FillDGVData();
            //LoadConnectionIndices();
        }

        private void CtrlObjStatePropertyManualGeometryRenderingMode_OnERenderingModeChanged(DNERenderingMode RenderingMode)
        {
            ctrlObjStatePropertyManualGeometryConnectionIndices.SetRenderingMode(RenderingMode);
        }

        private void GetPointsColors()
        {
            try
            {
                ctrlPropertyManualGeometryPointsColors1.Load(m_CurrentObject.GetPointsColors);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetPointsColors", McEx);
            }
        }

        private void GetPointsCoordinates()
        {
            try
            {
                ctrlObjStatePropertyManualGeometryVector3DArray.Load(m_CurrentObject.GetPointsCoordinates);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetPointsCoordinates", McEx);
            }
        }

        private void GetPointsTextureCoordinates()
        {
            try
            {
                ctrlObjStatePropertyManualGeometryFVector2DArray.Load(m_CurrentObject.GetPointsTextureCoordinates);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetPointsCoordinates", McEx);
            }
        }

        private void GetConnectionIndices()
        {
            try
            {
                ctrlObjStatePropertyManualGeometryConnectionIndices.Load( m_CurrentObject.GetConnectionIndices);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetPointsCoordinates", McEx);
            }
        }
        #endregion

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.SaveItem();

            try
            {
                ctrlObjStatePropertyManualGeometryRenderingMode.Save(m_CurrentObject.SetRenderingMode);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetRenderingMode", McEx);
            }

            try
            {
                ctrlObjStatePropertyTexture.Save(m_CurrentObject.SetTexture);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTexture", McEx);
            }


            try
            {
                ctrlPropertyManualGeometryPointsColors1.Save(m_CurrentObject.SetPointsColors);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetPointsColors", McEx);
            }

            try
            {
                ctrlObjStatePropertyManualGeometryVector3DArray.Save(m_CurrentObject.SetPointsCoordinates);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetPointsCoordinates", McEx);
            }


            try
            {
                ctrlObjStatePropertyManualGeometryFVector2DArray.Save(m_CurrentObject.SetPointsTextureCoordinates);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetPointsTextureCoordinates", McEx);
            }

            try
            {
                ctrlObjStatePropertyManualGeometryConnectionIndices.Save(m_CurrentObject.SetConnectionIndices);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetConnectionIndices", McEx);
            }



            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        #region Private Method

        private void RefreshUI()
        {
            GetPointsColors();
            GetPointsCoordinates();
            GetPointsTextureCoordinates();
        }

        #endregion


        #region Events

        private void btnSetPointsDataAsShared_Click(object sender, EventArgs e)
        {
            DNSMcVector3D[] aPointsCoordinates = ctrlObjStatePropertyManualGeometryVector3DArray.RegVector3DArrValue.aElements;
            DNSMcFVector2D[] aPointsTextureCoordinates = ctrlObjStatePropertyManualGeometryFVector2DArray.RegFVector2DArrValue.aElements;
            DNSMcBColor[] aPointsColors = ctrlPropertyManualGeometryPointsColors1.RegBColorsPropertyValue.aElements;
            try
            {
                m_CurrentObject.SetPointsData(aPointsCoordinates, aPointsTextureCoordinates, aPointsColors);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetPointsData", McEx);
            }

            RefreshUI();
        }

        private void btnGetPointsValues_Click(object sender, EventArgs e)
        {
            DNSMcVector3D[] aPointsCoordinates;
            DNSMcFVector2D[] aPointsTextureCoordinates;
            DNSMcBColor[] aPointsColors;

            m_CurrentObject.GetPointsData(out aPointsCoordinates, out aPointsTextureCoordinates, out aPointsColors);

            ctrlObjStatePropertyManualGeometryVector3DArray.RegVector3DArrValue = new DNSArrayProperty<DNSMcVector3D>(aPointsCoordinates);
            ctrlObjStatePropertyManualGeometryFVector2DArray.RegFVector2DArrValue = new DNSArrayProperty<DNSMcFVector2D>(aPointsTextureCoordinates);
            ctrlPropertyManualGeometryPointsColors1.RegBColorsPropertyValue = new DNSArrayProperty<DNSMcBColor>(aPointsColors);

        }
        
        #endregion

        private void ucManualGeometryItem_Load(object sender, EventArgs e)
        {

        }
    }
}
