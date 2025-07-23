using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Managers.ObjectWorld
{
    public static class Manager_MCObjectSchemeNode
    {
        static Dictionary<object, uint> dObjSchemeNode;

        static Manager_MCObjectSchemeNode()
        {
            dObjSchemeNode = new Dictionary<object, uint>();
        }
        

        #region IManagersGetter Members

        public static Dictionary<object, uint> AllParams
        {
            get { return dObjSchemeNode; } 
        }

        public static Dictionary<object, uint> GetChildren(object Parent)
        {
            IDNMcObjectSchemeNode objectSchemeNode = (IDNMcObjectSchemeNode)Parent;
            Dictionary<object, uint > Ret = new Dictionary<object, uint >();
            uint i = 0;
            IDNMcObjectSchemeNode[] NodeChildren = objectSchemeNode.GetChildren();
            foreach (IDNMcObjectSchemeNode currChild in NodeChildren)
            {
                Ret.Add(currChild,i++);
            }
            return Ret;
        }

 

        #endregion
    }
}
