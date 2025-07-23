using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Managers.ObjectWorld
{
    public static class Manager_MCConditionalSelector
    {
        static Dictionary<object, uint > dConSelector;

        static Manager_MCConditionalSelector()
        {
            dConSelector = new Dictionary<object, uint >();
        }

        #region IManagersGetter Members

        public static Dictionary<object, uint > AllParams
        {
            get { return dConSelector; }
        }

        public static Dictionary<object, uint> GetChildren(object Parent)
        {
            Dictionary<object, uint > Ret = new Dictionary<object, uint >();
            
            return Ret;
        }

        #endregion
                
    }
}
