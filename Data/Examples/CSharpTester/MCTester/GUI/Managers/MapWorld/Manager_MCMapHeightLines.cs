using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Managers.MapWorld
{
    public static class Manager_MCMapHeightLines
    {
        static Dictionary<object, uint> dHeightLines;

        static Manager_MCMapHeightLines()
        {
            dHeightLines = new Dictionary<object, uint>();
        }

        public static void CreateMapHeightLines(IDNMcMapHeightLines mapHeightLines)
        {
            dHeightLines.Add(mapHeightLines, (uint)dHeightLines.Count);
        }

        public static void RemoveMapHeightLines(IDNMcMapHeightLines mapHeightLines)
        {
            dHeightLines.Remove(mapHeightLines);
        }

        public static bool IsContains(IDNMcMapHeightLines mapHeightLines)
        {
            return dHeightLines.ContainsKey(mapHeightLines);
        }

        public static Dictionary<object, uint> AllParams
        {
            get { return dHeightLines; }
        }

        public static Dictionary<object, uint> GetChildren(object Parent)
        {
            Dictionary<object, uint> Ret = new Dictionary<object, uint>();

            return Ret;
        }
    }
}
