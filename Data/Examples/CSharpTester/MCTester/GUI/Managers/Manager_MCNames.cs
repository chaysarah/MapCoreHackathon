using MapCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTester.Managers
{
    public static class Manager_MCNames
    {
        private static Dictionary<uint, string> names;

        public static Dictionary<uint, string> Names
        {
            get { return names; }
            set { names = value; }
        }

        static Manager_MCNames()
        {
            names = new Dictionary<uint, string>();
        }

        public static string GetNameById(uint id)
        {
            if (names.ContainsKey(id))
                return names.FirstOrDefault(x => (x.Key == id)).Value;
            return id.ToString();
        }

        public static string GetMcNameById(uint id)
        {
            if (names.ContainsKey(id))
                return names.FirstOrDefault(x => (x.Key == id)).Value;
            return "";
        }

        public static string GetNameById(uint id,string type)
        {
            return "(" + GetNameById(id) + ") " + type;
        }

        //public static string GetIdFromNameByObject(object obj)
        //{
        //    return GetNameByObject(obj,false);
        //}

        public static string GetDefualtName(object obj)
        {
            StringBuilder type = GetType(obj);
            return GetNameById(uint.Parse(obj.GetHashCode().ToString()),type.ToString());
        }

        public static string GetNameByObject(object obj)
        {
            StringBuilder objType = GetType(obj);
            return GetNameByObject(obj, objType.ToString());
        }

        public static void GetNameByObjectArray(object obj, string[] arrOutput)
        {
            StringBuilder objType = GetType(obj);
            string id = GetIdByObject(obj);
            arrOutput[0] = id;
            arrOutput[1] = id + " " + objType;
        }

        public static void GetNameByObjectArrayByNameAndId(object obj, string[] arrOutput)
        {
            StringBuilder objType = GetType(obj);
            string id = GetIdByObject(obj);
            arrOutput[0] = id;
            arrOutput[1] = objType.ToString();
        }

        public static string GetIdByObject(object obj)
        {
            uint u_id = (uint)obj.GetHashCode();

            string fullName; 
            string mcName = string.Empty;

            if (obj is IDNMcObjectScheme)
            {
                try
                {
                    mcName = ((IDNMcObjectScheme)obj).GetName();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("IDNMcObjectScheme.GetName()", McEx);
                }
            }
            else if (obj is IDNMcObjectSchemeNode)
            {
                try
                {
                    mcName = ((IDNMcObjectSchemeNode)obj).GetName();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("IDNMcObjectSchemeNode.GetName()", McEx);
                }
            }
            else if (obj is IDNMcConditionalSelector)
            {
                try
                {
                    mcName = ((IDNMcConditionalSelector)obj).GetName();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("IDNMcConditionalSelector.GetName()", McEx);
                }
            }
            else if (obj is IDNMcObject)
            {
                try
                {
                    string name, desc;
                    ((IDNMcObject)obj).GetNameAndDescription(out name, out desc);
                    mcName = name;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("IDNMcObject.GetNameAndDescription()", McEx);
                }
            }

            if (mcName != string.Empty)
                fullName = "\"" +mcName+ "\"";
            else if (names.ContainsKey(u_id))
                fullName = "\"" + names.FirstOrDefault(x => (x.Key == u_id)).Value + "\"";
            else
                fullName = u_id.ToString();

            
            return fullName;
        }


        public static string GetNameByObject(object obj, string objType)
        {
            string fullName = "(" + GetIdByObject(obj) + ")";
            string strMoreData = "";
            if (obj is IDNMcObject)
            {
                try
                {
                    DNESymbologyStandard symbologyStandard = ((IDNMcObject)obj).GetSymbologyStandard();
                    if (symbologyStandard != DNESymbologyStandard._ESS_NONE)
                    {
                        strMoreData = " [" + symbologyStandard.ToString().Replace("_ESS_", "") + "]";
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetSymbologyStandard", McEx);
                }
            }
            else if(obj is IDNMcMapViewport)
            {
                IDNMcMapViewport mapViewport = obj as IDNMcMapViewport;
                DNEMapType mapType = mapViewport.MapType; 
                string strImage = (mapViewport.GetImageCalc() != null) ? " Image" : "";
                strMoreData = " [" + mapType.ToString().Replace("_EMT_", "") + strImage + "]";
            }
            else if (obj is IDNMcMapGrid)
            {
                IDNMcMapGrid mapGrid = obj as IDNMcMapGrid;
                DNSGridRegion[] gridRegions = mapGrid.GetGridRegions();
                if (gridRegions != null && gridRegions.Length == 1 && gridRegions[0].pCoordinateSystem != null)
                    strMoreData = " [" + gridRegions[0].pCoordinateSystem.GetGridCoorSysType().ToString().Replace("_EGCS_", "")+ "]";
                
            }
            return fullName + " " + objType + strMoreData;
        }
        
        public static StringBuilder GetType(object obj)
        {
            string TypeName = GeneralFuncs.GetDirectInterfaceName(obj.GetType());
            TypeName = TypeName.Replace("IDNMc", "");
            StringBuilder modifiedString = new StringBuilder();
            for(int i=0;i<TypeName.Length;i++)
            {
                char s = TypeName[i];
                if (char.IsDigit(s) && s == '3' && TypeName.ToLower().Contains("3d"))
                {
                    modifiedString.Append(" 3D");
                    i++;
                }
                else
                {
                    if (char.IsUpper(s))
                        modifiedString.Append(' ');
                    modifiedString.Append(s);
                }
            }
           
            return modifiedString;
        }

        public static void SetName(object node, string newName)
        {
            if (!SetMcName(node, newName))
            {
                uint id = uint.Parse(node.GetHashCode().ToString()); 
                
                if (names.ContainsKey(id))
                    names[id] = newName;
                else
                    names.Add(id, newName);
            }
        }

        private static bool SetMcName(object node, string newName)
        {
            bool isChanged = false;
            if (node is IDNMcObjectScheme)
            {
                try
                {
                    isChanged = true;
                    ((IDNMcObjectScheme)node).SetName(newName);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("IDNMcObjectScheme.SetName()", McEx);
                }
            }
            if (node is IDNMcObjectSchemeNode)
            {
                try
                {
                    isChanged = true;
                    ((IDNMcObjectSchemeNode)node).SetName(newName);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("IDNMcObjectSchemeNode.SetName()", McEx);
                }
            }
            if (node is IDNMcConditionalSelector)
            {
                try
                {
                    isChanged = true;
                    ((IDNMcConditionalSelector)node).SetName(newName);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("IDNMcConditionalSelector.SetName()", McEx);
                }
            }
            if(node is IDNMcObject)
            {
                try
                {
                    isChanged = true;
                    string name, desc;
                    ((IDNMcObject)node).GetNameAndDescription(out name, out desc);

                    ((IDNMcObject)node).SetNameAndDescription(newName, desc);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("IDNMcObject.Get/SetNameAndDescription()", McEx);
                }
                
            }
            return isChanged;
        }

        public static bool RemoveName(object node)
        {
            uint id = uint.Parse(node.GetHashCode().ToString());
            bool isRemoved = false;
            if (SetMcName(node, string.Empty))
                isRemoved = true;
            if (!isRemoved && names.ContainsKey(id))
            {
                names.Remove(id);
                isRemoved = true;
            }
            return isRemoved;
        }

    }
}
