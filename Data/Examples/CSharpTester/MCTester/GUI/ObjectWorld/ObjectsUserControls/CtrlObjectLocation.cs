using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class CtrlObjectLocation : MCTester.ObjectWorld.ObjectsUserControls.ucObjectSchemeNode, IUserControlItem
    {
        private IDNMcObjectLocation m_CurrentObject;


        public CtrlObjectLocation():base()
        {
            InitializeComponent();
            ctrlObjStatePropertyRelativeToDTM.HideObjStateTab();
            ctrlObjStatePropertyMaxNumOfPoints.HideObjStateTab();
        }

        protected override void SaveItem()
        {
            base.SaveItem();

            try
            {
                DNEMcPointCoordSystem ptCoordSys;

                m_CurrentObject.GetCoordSystem(out ptCoordSys);

                txtLocationCoordSys.Text = ptCoordSys.ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetCoordSystem", McEx);
            }

            try
            {
                ctrlObjStatePropertyRelativeToDTM.Save(m_CurrentObject.SetRelativeToDTM);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetRelativeToDTM", McEx);
            }

            try
            {
                ctrlObjStatePropertyMaxNumOfPoints.Save(m_CurrentObject.SetMaxNumPoints);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetMaxNumPoints", McEx);
            }
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcObjectLocation)aItem;
            base.LoadItem(aItem);

            try
            {
                ntxLocationIndex.SetUInt32(m_CurrentObject.GetIndex());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetIndex", McEx);
            }

            try
            {
                ctrlObjStatePropertyRelativeToDTM.Load(m_CurrentObject.GetRelativeToDTM);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRelativeToDTM", McEx);
            }

            try
            {
                ctrlObjStatePropertyMaxNumOfPoints.Load(m_CurrentObject.GetMaxNumPoints);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetMaxNumPoints", McEx);
            }

            try
            {
                DNEMcPointCoordSystem coordSys;

                m_CurrentObject.GetCoordSystem(out coordSys);
                txtLocationCoordSys.Text = coordSys.ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetCoordSystem", McEx);
            }
        }

        #endregion

        
    }
}

