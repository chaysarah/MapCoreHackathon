using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using System.Linq;


namespace MCTester.Managers.MapWorld
{
    public static class Manager_MCGrid
    {
        public static Dictionary<object, uint > dGrid;

        static Manager_MCGrid()
        {
            dGrid = new Dictionary<object, uint>();
        }

        public static void AddNewMapGrid(IDNMcMapGrid mapGrid)
        {
            if (!dGrid.ContainsKey(mapGrid))
                dGrid.Add(mapGrid,(uint)dGrid.Count);            
        }

        public static void AddNewMapGrid(IDNMcMapGrid mapGrid, uint chosenGrid)
        {
            IDNMcMapGrid gridKey = null;
            if (!dGrid.ContainsValue(chosenGrid))
                dGrid.Add(mapGrid, chosenGrid);
            else
            {
                try
                {
                    gridKey = (IDNMcMapGrid)dGrid.FirstOrDefault(x => x.Value == chosenGrid).Key;
                    dGrid.Remove(gridKey);
                    dGrid.Add(mapGrid, chosenGrid);
                }
                catch(Exception)
                {}
            }
        }

        public static Dictionary<object, uint> AllParams
        {
            get { return dGrid; }
        }

        public static Dictionary<object, uint > GetChildren(object Parent)
        {
            Dictionary<object, uint > Ret = new Dictionary<object, uint >();

            return Ret;
        }
    }

     
}
