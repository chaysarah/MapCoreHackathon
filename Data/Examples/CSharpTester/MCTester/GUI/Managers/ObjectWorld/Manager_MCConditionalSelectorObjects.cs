using MapCore;
using MapCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTester.Managers.ObjectWorld
{
    class Manager_MCConditionalSelectorObjects
    {
        static Dictionary<int, IDNMcObject> dLocationCondSelectorObjects;

        static Manager_MCConditionalSelectorObjects()
        {
            dLocationCondSelectorObjects = new Dictionary<int, IDNMcObject>();
        }

        static Dictionary<int, IDNMcObject> LocationCondSelectorObjects
        {
            get
            {
                if(dLocationCondSelectorObjects == null)
                    dLocationCondSelectorObjects = new Dictionary<int, IDNMcObject>();
                return dLocationCondSelectorObjects;
            }
        }

        public static void AddItem(IDNMcLocationConditionalSelector locationCondSelector, IDNMcObject mcObject) 
        {
            
            int id = GetHashCode(locationCondSelector);
           
            if (IsExistItem(id))
            {
                RemoveObject(id);
                dLocationCondSelectorObjects[id] = mcObject;
            }
            else
                dLocationCondSelectorObjects.Add(id, mcObject);
        }

        internal static void AddNewItem(IDNMcLocationConditionalSelector locationCondSelector, IDNMcObject mcObject)
        {
            int id = GetHashCode(locationCondSelector);
            locationCondSelector.GetOverlayManager().SetConditionalSelectorLock(locationCondSelector, true);
            if (!IsExistItem(id))
                dLocationCondSelectorObjects.Add(id, mcObject);
        }

        public static IDNMcObject GetObjectOfSelector(IDNMcLocationConditionalSelector locationCondSelector)
        {
            int id = GetHashCode(locationCondSelector);
            if (IsExistItem(id))
                return dLocationCondSelectorObjects[id];
            return null;
        }

        public static void RemoveItem(IDNMcLocationConditionalSelector locationCondSelector)
        {
            int id = GetHashCode(locationCondSelector);
            if (IsExistItem(id))
            {
                RemoveObject(id);
                dLocationCondSelectorObjects.Remove(id);
            }
        }

        public static void RemoveObjectFromDic(IDNMcLocationConditionalSelector locationCondSelector, IDNMcObject objectToRemove)
        {
            int locationCondSelectorId = GetHashCode(locationCondSelector);
            if (IsExistItem(locationCondSelectorId) && dLocationCondSelectorObjects[locationCondSelectorId] == objectToRemove)
                dLocationCondSelectorObjects[locationCondSelectorId] = null;
        }

        public static void RemoveObject(IDNMcLocationConditionalSelector locationCondSelector)
        {
            int id = GetHashCode(locationCondSelector);
            if (IsExistItem(id))
            {
                RemoveObject(id);
            }
        }

        private static void RemoveObject(int id)
        {
            IDNMcObject mcObject = dLocationCondSelectorObjects[id];
            if (mcObject != null)
            {
                try
                {
                    IDNMcObjectScheme scheme = mcObject.GetScheme();

                    try
                    {
                        foreach (IDNMcObjectSchemeNode node in scheme.GetNodes(DNENodeKindFlags._ENKF_ANY_NODE))
                        {
                            node.Dispose();
                        }

                        IDNMcOverlayManager OM = mcObject.GetOverlayManager();
                        IDNMcConditionalSelector[] selectorArr = OM.GetConditionalSelectors();
                        foreach (IDNMcConditionalSelector selector in selectorArr)
                        {
                            if (OM.IsConditionalSelectorLocked(selector) == false)
                                selector.Dispose();
                            if (selector is IDNMcLocationConditionalSelector)
                                RemoveObjectFromDic((IDNMcLocationConditionalSelector)selector, mcObject);
                        }

                        scheme.Dispose();
                        mcObject.Dispose();
                        mcObject.Remove();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("Remove Object", McEx);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("IDNMcObject.GetScheme", McEx);
                }
            }
        }

        public static bool IsExistItem(int id)
        {
            return dLocationCondSelectorObjects.Keys.Contains(id);
        }

        public static int GetHashCode(IDNMcLocationConditionalSelector locationCondSelector)
        {
            return locationCondSelector.GetHashCode();
        }

       
    }
}
