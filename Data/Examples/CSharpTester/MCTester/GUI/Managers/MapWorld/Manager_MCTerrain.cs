using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using MCTester.MapWorld;
using System.Windows.Forms;

namespace MCTester.Managers.MapWorld
{
    public static class Manager_MCTerrain
    {
        static uint currentTerrainID = 0;
        static Dictionary<IDNMcMapTerrain, uint> m_dTerrain;

        static Manager_MCTerrain()
        {
            m_dTerrain = new Dictionary<IDNMcMapTerrain, uint>();

        }

        public static IDNMcMapTerrain CreateTerrain(IDNMcGridCoordinateSystem gridCoordSys, IDNMcMapLayer[] layerList, bool bDisplayItemsAttachedTo3DModelWithoutDtm = false)
        {
            IDNMcMapTerrain ret = null;

            try
            {
                ret = DNMcMapTerrain.Create(gridCoordSys, layerList,null, null, bDisplayItemsAttachedTo3DModelWithoutDtm);

                if (ret != null)
                    dTerrain.Add(ret, currentTerrainID++);
                else
                    MessageBox.Show("Terrain creation failed!!!","Terrain creation",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcMapTerrain.Create", McEx);
            }
            
            return ret;
        }

        public static void AddTerrain(IDNMcMapTerrain terrain)
        {
            dTerrain.Add(terrain,currentTerrainID++);
        }

        public static void RemoveTerrainFromDic(IDNMcMapTerrain terrain)
        {
            if(dTerrain.ContainsKey(terrain))
                dTerrain.Remove(terrain);
            terrain = null;
        }

        public static void RemoveTerrain(IDNMcMapTerrain terrain)
        {
            if (dTerrain.ContainsKey(terrain))
                dTerrain.Remove(terrain);

            try
            {
                terrain.Dispose();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Dispose terrain", McEx);
            }
            terrain = null;
        }
        

        public static  Dictionary<object, uint > AllParams
        {
            get
            {
                Dictionary<object, uint> Ret = new Dictionary<object, uint>();

                foreach (IDNMcMapTerrain keyTerr in dTerrain.Keys)
                {
                    Ret.Add(keyTerr, dTerrain[keyTerr]);
                }

                return Ret;
            }            
        }

        public static List<IDNMcMapTerrain> AllTerrains()
        {
            List<IDNMcMapTerrain> lst = new List<IDNMcMapTerrain>();
            foreach (IDNMcMapViewport vp in Manager_MCViewports.AllParams.Keys)
            {
                IDNMcMapTerrain[] Terrs = vp.GetTerrains();
                foreach (IDNMcMapTerrain terrain in Terrs)
                {
                    if (!lst.Contains(terrain))
                        lst.Add(terrain);
                }
            }

            foreach (IDNMcMapTerrain terr in AllParams.Keys)
            {
                if (!lst.Contains(terr))
                    lst.Add(terr);
            }
            return lst;
        }

        public static  Dictionary<object, uint > GetChildren(object Parent)
        {
            Dictionary<object, uint > Ret = new Dictionary<object, uint >();
            if (Parent is IDNMcMapTerrain)
            {
                IDNMcMapTerrain Terrain = (IDNMcMapTerrain)Parent;
                IDNMcMapLayer[] LayersInTerrain = Terrain.GetLayers();

                uint i = 0;
                foreach (IDNMcMapLayer currLayer in LayersInTerrain)
                {
                    if(!Ret.ContainsKey(currLayer))
                        Ret.Add(currLayer, i++);
                }
            }
            return Ret;
        }

        public static Dictionary<IDNMcMapTerrain, uint> dTerrain
        {
            get { return m_dTerrain; }
            set { m_dTerrain = value; }
        }
    }
}
