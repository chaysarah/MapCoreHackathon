using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld.LoadingMapScheme
{
    [Serializable]
    public class MCTOverlayManagers
    {
        private List<MCTOverlayManager> m_OverlayManagers;

        public MCTOverlayManagers()
        {
            m_OverlayManagers = new List<MCTOverlayManager>();
        }

        public MCTOverlayManager GetOverlayManager(int OverlayManagerID)
        {
            MCTOverlayManager ret = null;
            foreach (MCTOverlayManager OM in OverlayManager)
            {
                if (OM.ID == OverlayManagerID)
                {
                    ret = OM;
                    break;
                }
            }

            return ret;
        }

        public int GetNextId()
        {
            int max = 0;
            foreach (MCTOverlayManager OM in OverlayManager)
            {
                if (OM.ID > max)
                {
                    max = OM.ID;
                }
            }

            return max + 1;
        }

        public List<MCTOverlayManager> OverlayManager
        {
            get { return m_OverlayManagers; }
            set { m_OverlayManagers = value; }
        }
    }
}
