using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld
{
    public class MCTMapDevices
    {
        private List<MCTMapDevice> m_Devices;

        public MCTMapDevices()
        {
            m_Devices = new List<MCTMapDevice>();
        }

       /* public MCTMapDevice GetDevice(int DeviceId)
        {
            MCTMapDevice ret = null;
            foreach (MCTMapDevice de in Device)
            {
                if (de.ID == DeviceId)
                {
                    ret = de;
                    break;
                }
            }

            return ret;
        }*/
        
        public List<MCTMapDevice> Device
        {
            get { return m_Devices; }
            set { m_Devices = value; }
        }
    }
}
