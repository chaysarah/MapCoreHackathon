using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld
{
    [Serializable]
    public class MCTMapTerrain
    {
        private int m_TerrainId;
        private List<int> m_LayersIDs;
        private string m_FileName;
        private string m_TerrainPath;
        private bool m_IsNative;
        private int m_CoordSysID;
                
        public MCTMapTerrain()
        {
            m_LayersIDs = new List<int>();
        }

        public IDNMcMapTerrain CreateTerrain(IDNMcMapLayer[] layers, IDNMcGridCoordinateSystem coordSys)
        {
            IDNMcMapTerrain ret = null;
            try
            {
                ret = DNMcMapTerrain.Create(coordSys, layers);
                //MCTester.Managers.MapWorld.Manager_MCTerrain.AddTerrain(ret);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcMapTerrain.Create", McEx);
            }
            return ret;
        }
        
        #region Public Properties
        public int ID
        {
            get { return m_TerrainId; }
            set { m_TerrainId = value; }
        }
        public string Name
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }
        public string Path
        {
            get { return m_TerrainPath; }
            set { m_TerrainPath = value; }
        }
        public bool IsNative
        {
            get { return m_IsNative; }
            set { m_IsNative = value; }
        }

        public int CoordSysID
        {
            get { return m_CoordSysID; }
            set { m_CoordSysID = value; }
        }

        public List<int> LayersIDs
        {
            get { return m_LayersIDs; }
            set { m_LayersIDs = value; }
        }
        #endregion
    }
}
