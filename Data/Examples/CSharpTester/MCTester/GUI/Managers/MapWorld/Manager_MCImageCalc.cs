using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Managers.MapWorld
{
    public static class Manager_MCImageCalc
    {
        static Dictionary<IDNMcImageCalc, uint > dImageCalc;

        static Manager_MCImageCalc()
        {
            dImageCalc = new Dictionary<IDNMcImageCalc, uint>();
        }

        public static void AddToDictionary(IDNMcImageCalc imageCalc)
        {
            if (imageCalc != null)
                dImageCalc.Add(imageCalc, (uint)dImageCalc.Count);       
        }
                
        public static  Dictionary<object, uint> AllParams
        {            
            get 
            {
                Dictionary<object, uint> Ret = new Dictionary<object, uint>();

                foreach (IDNMcImageCalc keyImgc in dImageCalc.Keys)
                {
                    Ret.Add(keyImgc, dImageCalc[keyImgc]);
                }

                return Ret;                
            }
        }

        public static Dictionary<object, uint> GetChildren(object Parent)
        {
            Dictionary<object, uint> Ret = new Dictionary<object, uint>();

            return Ret;
        }

        public static void RemoveImageCalc(IDNMcImageCalc mcImageCalc)
        {
            if(dImageCalc.ContainsKey(mcImageCalc))
                dImageCalc.Remove(mcImageCalc);
        }
    }
}
