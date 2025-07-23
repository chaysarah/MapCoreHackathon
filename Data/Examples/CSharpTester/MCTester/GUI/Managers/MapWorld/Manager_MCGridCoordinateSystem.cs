using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using System.Linq;

namespace MCTester.Managers.MapWorld
{
    public static class Manager_MCGridCoordinateSystem
    {
        static Dictionary<object, uint> dGridCoordSys;

        static List<KeyValuePair<IDNMcGridCoordinateSystem, bool>> dAllGridCoordSys;

        static Manager_MCGridCoordinateSystem()
        {
            dGridCoordSys = new Dictionary<object, uint>();
            dAllGridCoordSys = new List<KeyValuePair<IDNMcGridCoordinateSystem, bool>>();
        }
        
        public static void AddNewGridCoordinateSystem(IDNMcGridCoordinateSystem gridCoordSys, bool IsCheckExist = true)
        {
            if (dGridCoordSys != null && gridCoordSys != null)
            {
                if (!dGridCoordSys.ContainsKey(gridCoordSys) && (!IsCheckExist || !IsExist(gridCoordSys)))
                    dGridCoordSys.Add(gridCoordSys, (uint)dGridCoordSys.Count);

                dAllGridCoordSys.Add(new KeyValuePair<IDNMcGridCoordinateSystem, bool>(gridCoordSys, IsCheckExist));
            }
        }

        public static Dictionary<object, uint> AllParams
        {
            get { return dGridCoordSys; }
        }

        public static Dictionary<object, uint> GetChildren(object Parent)
        {
            Dictionary<object, uint> Ret = new Dictionary<object, uint>();

            return Ret;
        }

        public static void Remove(IDNMcGridCoordinateSystem gridCoordSys)
        {
            if (gridCoordSys != null && dGridCoordSys.ContainsKey(gridCoordSys))
                dGridCoordSys.Remove(gridCoordSys);
        }

        public static bool IsExist(IDNMcGridCoordinateSystem gridCoordSys)
        {
            return gridCoordSys != null &&
                (dGridCoordSys.ContainsKey(gridCoordSys) || dGridCoordSys.Keys.ToList().Exists(x =>
                (IsSame(gridCoordSys, (x as IDNMcGridCoordinateSystem)))));
        }

        public static bool IsSame(IDNMcGridCoordinateSystem gridCoordSys, IDNMcGridCoordinateSystem gridCoordSys2)
        {
            return gridCoordSys != null && 
               gridCoordSys2 != null &&
               gridCoordSys2.IsEqual(gridCoordSys) &&
               gridCoordSys2.GetGridCoorSysType() == gridCoordSys.GetGridCoorSysType() &&
               gridCoordSys2.GetLegalValuesForGeographicCoordinates() == gridCoordSys.GetLegalValuesForGeographicCoordinates() &&
               gridCoordSys2.GetLegalValuesForGridCoordinates() == gridCoordSys.GetLegalValuesForGridCoordinates();
        }

        public static int FindLastIndex(IDNMcGridCoordinateSystem gridCoordSys)
        {
            return dGridCoordSys.Keys.ToList().FindLastIndex(x => IsSame(gridCoordSys, (x as IDNMcGridCoordinateSystem)));
        }

        public static uint GetCounter(IDNMcGridCoordinateSystem coordSys)
        {
            int counter = 0;
            int sameUserCrearedIndex = -1;

            // find last index of grid coord' that user created 
            for (int i = dAllGridCoordSys.Count-1; i >= 0; i--) 
            {
                if (!dAllGridCoordSys[i].Value && dAllGridCoordSys[i].Key == coordSys)
                {
                    sameUserCrearedIndex = i;
                    break;
                }
            }
            if (sameUserCrearedIndex >= 0)  // found user created coord'
            {
                counter = 1;
                for (int i = sameUserCrearedIndex + 1; i < dAllGridCoordSys.Count; i++)
                {
                    if (!dAllGridCoordSys[i].Value && IsSame(coordSys, dAllGridCoordSys[i].Key))
                        break;
                    if (dAllGridCoordSys[i].Key == coordSys || (dAllGridCoordSys[i].Value && IsSame(coordSys, dAllGridCoordSys[i].Key)))
                        counter++;
                }
            }
            else                           // not found user created coord', search the first auto' coord'
            {
                int currIndex = -1;
                for (int i = dAllGridCoordSys.Count - 1; i >= 0; i--)
                {
                    if (coordSys == dAllGridCoordSys[i].Key)
                    {
                        currIndex = i;
                        break;
                    }
                }
                for (int i = currIndex; i < dAllGridCoordSys.Count; i++)
                {
                    if (!dAllGridCoordSys[i].Value && IsSame(coordSys, dAllGridCoordSys[i].Key))  // is coord' auto, find until same user created
                        break;
                    if (dAllGridCoordSys[i].Value && IsSame(coordSys, dAllGridCoordSys[i].Key))
                    {
                        counter++;
                    }
                }
            }

            return (uint)counter;
        }

    }
}
