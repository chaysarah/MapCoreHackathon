using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld.LoadingMapScheme
{
    [Serializable]
    public class MCTGridCoordinateSystems
    {
        private List<MCTGridCoordinateSystem> m_CoordinateSystem;

        public MCTGridCoordinateSystems()
        {
            CoordinateSystem = new List<MCTGridCoordinateSystem>();
        }

        public List<MCTGridCoordinateSystem> CoordinateSystem
        {
            get { return m_CoordinateSystem; }
            set { m_CoordinateSystem = value; }
        }

        public MCTGridCoordinateSystem GetCoordSys(int CoordSysID)
        {
            MCTGridCoordinateSystem ret = null;
            foreach (MCTGridCoordinateSystem coordSys in CoordinateSystem)
            {
                if (coordSys.ID == CoordSysID)
                {
                    ret = coordSys;
                    break;
                }
            }

            return ret;
        }

        public int GetNextID()
        {
            int  max = 0;
            foreach (MCTGridCoordinateSystem coordSys in CoordinateSystem)
            {
                if (coordSys.ID > max)
                {
                    max = coordSys.ID;
                }
            }

            return max + 1;
        }

        public int GetCoordSysID(IDNMcGridCoordinateSystem gridCoordinateSystem)
        {
            foreach (MCTGridCoordinateSystem coordSys in CoordinateSystem)
            {
                if (coordSys.GridCoordinateSystemType == gridCoordinateSystem.GetGridCoorSysType())
                {
                    if (gridCoordinateSystem.GetGridCoorSysType() == DNEGridCoordSystemType._EGCS_GENERIC_GRID)
                    {
                        IDNMcGridGeneric mcGridGeneric = gridCoordinateSystem as IDNMcGridGeneric;
                        string[] pastrCreateParams;
                        bool isSRID;
                        mcGridGeneric.GetCreateParams(out pastrCreateParams, out isSRID);
                        if (isSRID == coordSys.IsSRID)
                        {
                            if (pastrCreateParams != null && coordSys.AStrCreateParams != null && pastrCreateParams.Length == coordSys.AStrCreateParams.Length)
                            {
                                int i = 0;
                                bool isEqual = true;
                                foreach (string pstrCreateParams in pastrCreateParams)
                                {
                                    if (pstrCreateParams != coordSys.AStrCreateParams[i++])
                                        isEqual = false;
                                }
                                if (isEqual)
                                    return coordSys.ID;
                            }
                            //coordSys.AStrCreateParams
                        }
                    }
                    else if (coordSys.DatumType == gridCoordinateSystem.GetDatum() && coordSys.Zone == gridCoordinateSystem.GetZone())
                    {
                        return coordSys.ID;
                    }
                }
            }
            return -1;
        }
    }
}
