using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Managers.ObjectWorld
{
    public static class Manager_MCObjectLocation
    {
        static Dictionary<object, uint> dObjectLocation;

        static Manager_MCObjectLocation()
        {
            dObjectLocation = new Dictionary<object, uint >();
        }

        #region IManagersGetter Members

        public static Dictionary<object, uint > AllParams
        {
            get { return dObjectLocation; }
        }

        public static Dictionary<object, uint> GetChildren(object Parent)
        {
            if (Parent is IDNMcObjectLocation)
            {
                IDNMcObjectSchemeNode objectSchemeNode = (IDNMcObjectSchemeNode)Parent;
                Dictionary<object, uint > Ret = new Dictionary<object, uint >();
                uint i = 0;
                IDNMcObjectSchemeNode[] objectItems = objectSchemeNode.GetChildren();
                foreach (IDNMcObjectSchemeNode currChild in objectItems)
                {
                    Ret.Add(currChild, i++);
                }
                return Ret;
            }
            return null;
        }

        #endregion
    }
}
