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
    public partial class ucParticleEffectItem : ucPhysicalItem, IUserControlItem
    {
        private IDNMcParticleEffectItem m_CurrentObject;
        //private uint m_PropId;
        //private string m_sParam;
        //private DNEParticleEffectState m_eStateParam;
        //private float m_fParam;
        //private DNSMcFVector3D m_fVectParam;

        public ucParticleEffectItem()
        {
            InitializeComponent();
        }

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.SaveItem();

            try
            {
                ctrlObjStatePropertyEffectFileName.Save(m_CurrentObject.SetEffectName);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetEffectName", McEx);
            }

            try
            {
                ctrlObjStatePropertyStartingTimePoint.Save(m_CurrentObject.SetStartingTimePoint);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetStartingTimePoint", McEx);
            }

            try
            {
                ctrlObjStatePropertyStartingDelay.Save(m_CurrentObject.SetStartingDelay);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetStartingDelay", McEx);
            }

            //Advanced Properties
            try
            {
                ctrlObjStatePropertySamplingStep.Save(m_CurrentObject.SetSamplingStep);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSamplingStep", McEx);
            }

            try
            {
                ctrlObjStatePropertyTimeFactor.Save(m_CurrentObject.SetTimeFactor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTimeFactor", McEx);
            }

            try
            {
                ctrlObjStatePropertyParticleVelocity.Save(m_CurrentObject.SetParticleVelocity);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetParticleVelocity", McEx);
            }

            try
            {
                ctrlPropertyParticleDirection.Save(m_CurrentObject.SetParticleDirection);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetParticleDirection", McEx);
            }

            try
            {
                ctrlObjStatePropertyParticleAngle.Save(m_CurrentObject.SetParticleAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetParticleAngle", McEx);
            }

            try
            {
                ctrlObjStatePropertyParticleEmissionRate.Save(m_CurrentObject.SetParticleEmissionRate);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetParticleEmissionRate", McEx);
            }

            try
            {
                ctrlObjStatePropertyTimeToLive.Save(m_CurrentObject.SetTimeToLive);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTimeToLive", McEx);
            }

			// State should be set last !!!
			try
			{
                ctrlObjStatePropertyParticleEffectState.Save(m_CurrentObject.SetState);
			}
			catch (MapCoreException McEx)
			{
				MapCore.Common.Utilities.ShowErrorMessage("SetState", McEx);
			}

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }



        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcParticleEffectItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.LoadItem(aItem);

            try
            {
                ctrlObjStatePropertyEffectFileName.Load(m_CurrentObject.GetEffectName);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetEffectName", McEx);
            }

            try
            {
                ctrlObjStatePropertyParticleEffectState.Load(m_CurrentObject.GetState);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetState", McEx);
            }

            try
            {
                ctrlObjStatePropertyStartingTimePoint.Load(m_CurrentObject.GetStartingTimePoint);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetStartingTimePoint", McEx);
            }

            try
            {
                ctrlObjStatePropertyStartingDelay.Load(m_CurrentObject.GetStartingDelay);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetStartingDelay", McEx);
            }

            try
            {
                ctrlObjStatePropertySamplingStep.Load(m_CurrentObject.GetSamplingStep);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetSamplingStep", McEx);
            }

            try
            {
                ctrlObjStatePropertyTimeFactor.Load(m_CurrentObject.GetTimeFactor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTimeFactor", McEx);
            }

            try
            {
                ctrlObjStatePropertyParticleVelocity.Load(m_CurrentObject.GetParticleVelocity);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetParticleVelocity", McEx);
            }

            try
            {
                ctrlPropertyParticleDirection.Load(m_CurrentObject.GetParticleDirection);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetParticleDirection", McEx);
            }

            try
            {
                ctrlObjStatePropertyParticleAngle.Load( m_CurrentObject.GetParticleAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetParticleAngle", McEx);
            }

            try
            {
                ctrlObjStatePropertyParticleEmissionRate.Load(m_CurrentObject.GetParticleEmissionRate);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetParticleEmissionRate", McEx);
            }

            try
            {
                ctrlObjStatePropertyTimeToLive.Load(m_CurrentObject.GetTimeToLive);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTimeToLive", McEx);
            }
        }

        #endregion
    }
}
