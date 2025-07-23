using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld
{
    [Serializable]
    public class MCTMapTerrains
    {
        private List<MCTMapTerrain> m_Terrains;
        
        public List<MCTMapTerrain> Terrain
        {
            get { return m_Terrains; }
            set { m_Terrains = value; }
        }

        public MCTMapTerrains()
        {
            m_Terrains = new List<MCTMapTerrain>();
        }

        public MCTMapTerrain GetTerrain(int TerrainID)
        {
            MCTMapTerrain ret = null;
            foreach(MCTMapTerrain t in Terrain)
            {
                if (t.ID == TerrainID)
                {
                    ret = t;
                    break;
                }
            }

            return ret;
        }

        public int GetTerrainLayerID()
        {
            int max = 0;
            foreach (MCTMapTerrain t in Terrain)
            {
                if (t.ID > max)
                {
                    max = t.ID;
                }
            }

            return max + 1;
        }

    }
}
