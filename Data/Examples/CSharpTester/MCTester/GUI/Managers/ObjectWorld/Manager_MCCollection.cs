using System;
using System.Collections.Generic;
using System.Text;
using UnmanagedWrapper;
using MapCore;

namespace MCTester.Managers.ObjectWorld
{
    public static class Manager_MCCollection
    {
        static Dictionary<object, uint> dCollection;

        static Manager_MCCollection()
        {
            dCollection = new Dictionary<object, uint>();
        }

        #region IManagersGetter Members

        public static Dictionary<object, uint> AllParams
        {
            get { return dCollection; }
        }

        public static Dictionary<object, uint> GetChildren(object Parent)
        {
			IDNMcCollection collection = (IDNMcCollection)Parent;
			Dictionary<object, uint > Ret = new Dictionary<object, uint >();

			if (collection == null)
				return Ret;

            /*
            int i = 0;
            if (collection.GetOverlays().Length != 0)
            {
                IDNMcOverlay[] OverlaysInCollection = collection.GetOverlays();

                foreach (IDNMcOverlay currOverlay in OverlaysInCollection)
                {
                    Ret.Add(i++, currOverlay);
                }

            }

            if (collection.GetObjects().Length != 0)
            {
                IDNMcObject[] objectsInCollection = collection.GetObjects();

                foreach (IDNMcObject currObjects in objectsInCollection)
                {
                    Ret.Add(i++, currObjects);
                }
            }
            */
			
			return Ret;
        }

        #endregion

    }
}
