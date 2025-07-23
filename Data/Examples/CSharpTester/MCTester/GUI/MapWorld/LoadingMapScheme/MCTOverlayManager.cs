using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;

namespace MCTester.MapWorld.LoadingMapScheme
{
    [Serializable]
    public class MCTOverlayManager
    {
        private int m_OMId;
        private int m_CoordSysID;
        private string m_Name;
          
        #region Public Properties
        public int ID
        {
            get { return m_OMId; }
            set { m_OMId = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public int CoordSysID
        {
            get { return m_CoordSysID; }
            set { m_CoordSysID = value; }
        }
        #endregion

        public IDNMcOverlayManager CreateOverlayManager(IDNMcGridCoordinateSystem coordSys)
        {
            IDNMcOverlayManager ret = null;
            try
            {
                ret = DNMcOverlayManager.Create(coordSys);
                
                //Create overlay and set it to be the active overlay
                IDNMcOverlay newOverlay = DNMcOverlay.Create(ret);
                Manager_MCOverlayManager.dOverlayManager_Overlay[ret] = newOverlay;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcOverlayManager.Create", McEx);
            }

            return ret;
        }
    }
}
