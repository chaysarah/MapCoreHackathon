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
    public partial class ucSoundItem : ucPhysicalItem, IUserControlItem
    {
        private IDNMcSoundItem m_CurrentObject;

        public ucSoundItem()
        {
            InitializeComponent();
        }

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);

            base.SaveItem();

            try
            {
                ctrlObjStatePropertySoundName.Save(m_CurrentObject.SetSoundName);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSoundName", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundLoop.Save(m_CurrentObject.SetLoop);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetLoop", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundStartingTimePoint.Save(m_CurrentObject.SetStartingTimePoint);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetStartingTimePoint", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundState.Save(m_CurrentObject.SetState);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetState", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundVolume.Save(m_CurrentObject.SetVolume);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetVolume", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundMinVolume.Save(m_CurrentObject.SetMinVolume);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetMinVolume", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundMaxVolume.Save(m_CurrentObject.SetMaxVolume);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetMaxVolume", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundRollOffFactor.Save(m_CurrentObject.SetRollOffFactor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetRollOffFactor", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundMaxDistance.Save(m_CurrentObject.SetMaxDistance);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetMaxDistance", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundHalfVolumeDistance.Save(m_CurrentObject.SetHalfVolumeDistance);
                
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetHalfVolumeDistance", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundHalfOuterAngle.Save(m_CurrentObject.SetHalfOuterAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetHalfOuterAngle", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundHalfInnerAngle.Save(m_CurrentObject.SetHalfInnerAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetHalfInnerAngle", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundOuterAngleVolume.Save(m_CurrentObject.SetOuterAngleVolume);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetOuterAngleVolume", McEx);
            }

            try
            {
                ctrlPropertySoundVelocity.Save(m_CurrentObject.SetVelocity);

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetVelocity", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundPitch.Save(m_CurrentObject.SetPitch);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetPitch", McEx);
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        #region IUserControlItem Members
        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcSoundItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);

            base.LoadItem(aItem);

            try
            {
                ctrlObjStatePropertySoundName.Load(m_CurrentObject.GetSoundName);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetSoundName", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundLoop.Load(m_CurrentObject.GetLoop);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetLoop", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundStartingTimePoint.Load(m_CurrentObject.GetStartingTimePoint);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetStartingTimePoint", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundState.Load(m_CurrentObject.GetState);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetState", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundVolume.Load(m_CurrentObject.GetVolume);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetVolume", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundMinVolume.Load(m_CurrentObject.GetMinVolume);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetMinVolume", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundMaxVolume.Load(m_CurrentObject.GetMaxVolume);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetMaxVolume", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundRollOffFactor.Load(m_CurrentObject.GetRollOffFactor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRollOffFactor", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundMaxDistance.Load(m_CurrentObject.GetMaxDistance);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetMaxDistance", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundHalfVolumeDistance.Load(m_CurrentObject.GetHalfVolumeDistance);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetHalfVolumeDistance", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundHalfOuterAngle.Load(m_CurrentObject.GetHalfOuterAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetHalfOuterAngle", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundHalfInnerAngle.Load(m_CurrentObject.GetHalfInnerAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetHalfInnerAngle", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundOuterAngleVolume.Load(m_CurrentObject.GetOuterAngleVolume);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetOuterAngleVolume", McEx);
            }

            try
            {
                ctrlPropertySoundVelocity.Load(m_CurrentObject.GetVelocity);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetVelocity", McEx);
            }

            try
            {
                ctrlObjStatePropertySoundPitch.Load(m_CurrentObject.GetPitch);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetPitch", McEx);
            }
        }

        #endregion
    }
}
