using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld
{
    [Serializable]
    public class MCTMapLayers
    {
        private List<MCTMapLayer> m_layers;

        public MCTMapLayers()
        {
            m_layers = new List<MCTMapLayer>();
        }

        public MCTMapLayer GetLayer(int LayerID)
        {
            MCTMapLayer ret = null;
            foreach (MCTMapLayer l in Layer)
            {
                if (l.ID == LayerID)
                {
                    ret = l;
                    break;
                }
            }

            return ret;
        }

        public int GetNextLayerID()
        {
            int max = 0;
            foreach (MCTMapLayer l in Layer)
            {
                if (l.ID > max )
                {
                    max = l.ID;
                }
            }

            return max + 1;
        }


        #region Public Properties
        public List<MCTMapLayer> Layer
        {
            get { return m_layers; }
            set { m_layers = value; }
        }

        #endregion
    }
}
