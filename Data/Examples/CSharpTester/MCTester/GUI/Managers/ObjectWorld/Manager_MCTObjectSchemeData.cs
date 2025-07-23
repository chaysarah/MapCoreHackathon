using MapCore;
using MCTester.ObjectWorld.ObjectsUserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using static MCTester.Managers.Manager_MCTObjectSchemeData;

namespace MCTester.Managers
{
    public static class Manager_MCTObjectSchemeData
    {
        private static Dictionary<object, ObjectSchemeData> dicObjectSchemeData;

        public class ObjectSchemeData {
            public string strFileSavedPath;
            public uint uVersion;
            public DNEStorageFormat eStorageFormat;
        }

        public static Dictionary<object, ObjectSchemeData> DicObjectSchemeData
        {
            get
            {
                if (dicObjectSchemeData == null)
                    dicObjectSchemeData = new Dictionary<object, ObjectSchemeData>();
                return dicObjectSchemeData;
            }
        }

        static Manager_MCTObjectSchemeData(){}
       
        public static string GetMsgText(string version, bool isScheme = true)
        {
            return "The" + (isScheme? " schemes " : " objects " )+"are of different versions. Save using the most recent detected version ("+ version.Replace("_ESVC_", "") + "), or cancel?";
        }

        public static bool IsExistObject(object obj)
        {
            if (DicObjectSchemeData.ContainsKey(obj) && DicObjectSchemeData[obj] != null)
                return true;
            else
                return false;
        }

        public static void AddObjectSchemeData(object obj, uint uVersion, DNEStorageFormat eStorageFormat, string filePath = "")
        {
            if (!IsExistObject(obj))
                DicObjectSchemeData.Add(obj, new ObjectSchemeData());

            if (filePath != "")
                DicObjectSchemeData[obj].strFileSavedPath = filePath;
            if (uVersion == 0)
                dicObjectSchemeData[obj].uVersion = (uint)GetLatestSavingVersionCompatibility();
            else
                DicObjectSchemeData[obj].uVersion = uVersion;
            DicObjectSchemeData[obj].eStorageFormat = eStorageFormat;

        }


        private static uint GetIdByObject(object obj)
        {
            return uint.Parse(obj.GetHashCode().ToString());
        }

        public static string GetFilePathByObject(object obj)
        {
            if (IsExistObject(obj))
                return DicObjectSchemeData[obj].strFileSavedPath;
            else
                return "";
        }

        public static void GetSchemeData(object obj, out string version, out DNEStorageFormat storageFormat)
        {
            version = string.Empty;
            storageFormat = DNEStorageFormat._ESF_MAPCORE_BINARY;
            if (IsExistObject(obj))
            {
                version = GetVersionByValue(DicObjectSchemeData[obj].uVersion);
                storageFormat = DicObjectSchemeData[obj].eStorageFormat;
            }
        }

        public static uint GetVersion(object obj)
        {
            if (IsExistObject(obj))
            {
                return DicObjectSchemeData[obj].uVersion;
            }
            return (uint)DNESavingVersionCompatibility._ESVC_LATEST;
        }

        public static void RemoveObjectFromDic(object node)
        {
            DicObjectSchemeData.Remove(node);
        }

        public static void RemoveObjectFilePath(object node)
        {
            if (IsExistObject(node))
                DicObjectSchemeData[node].strFileSavedPath = "";
        }
        public static DNESavingVersionCompatibility GetSavingVersionCompatibility(object[] objects, out bool isAllSchemeHasSameVersion)
        {
            uint retVersion = (uint)DNESavingVersionCompatibility._ESVC_LATEST;
            isAllSchemeHasSameVersion = true;

            if (SaveParamsData.IsSaveWithOriginalVersion)
            {
                if (objects != null)
                {
                    DNESavingVersionCompatibility latestSavingVersionCompatibility = Manager_MCTObjectSchemeData.GetLatestSavingVersionCompatibility();
                    for (int i = 0; i < objects.Length; i++)
                    {
                        uint tempVersion = GetVersion(objects[i]);
                        if(tempVersion == 0)   // 0 meaning LATEST
                            tempVersion = (uint)latestSavingVersionCompatibility;
                        if (i == 0)
                            retVersion = tempVersion;
                        else if (tempVersion != retVersion)
                        {
                            isAllSchemeHasSameVersion = false;
                            break;
                        }

                    }
                 
                    if(!isAllSchemeHasSameVersion)
                    {
                        // find the latest version 
                        uint maxVersion = (uint)DNESavingVersionCompatibility._ESVC_LATEST;
                        for (int i = 0; i < objects.Length; i++)
                        {
                            uint version = GetVersion(objects[i]);
                            if (version > maxVersion)
                                maxVersion = version;
                        }
                        retVersion = maxVersion;
                    }
                }

                return GetSavingVersionCompatibility(retVersion); ;
            }
            else
                return SaveParamsData.SavingVersionCompatibility;
        }
        public static string GetVersionByValue(uint uVersion)
        {
            Array values = Enum.GetValues(typeof(DNESavingVersionCompatibility));
            string[] names = Enum.GetNames(typeof(DNESavingVersionCompatibility));
            string versionText = uVersion.ToString();
            for (int i = 0; i < values.Length; i++)
            {
                DNESavingVersionCompatibility savingVersionCompatibility = (DNESavingVersionCompatibility)values.GetValue(i);
                if (((uint)savingVersionCompatibility) == uVersion)
                {
                    versionText = names[i] + " (" + versionText + ")";
                    break;
                }
            }
            return versionText;
        }

        public static DNESavingVersionCompatibility GetSavingVersionCompatibility(uint uVersion)
        {
            if (SaveParamsData.IsSaveWithOriginalVersion && uVersion == (uint)GetLatestSavingVersionCompatibility())
            {
                // if user save object that not loaded, i need to send to save function '_ESVC_LATEST' 
                return DNESavingVersionCompatibility._ESVC_LATEST;
            }
            else
            {
                Array values = Enum.GetValues(typeof(DNESavingVersionCompatibility));
                for (int i = 0; i < values.Length; i++)
                {
                    DNESavingVersionCompatibility savingVersionCompatibility = (DNESavingVersionCompatibility)values.GetValue(i);
                    if (((uint)savingVersionCompatibility) == uVersion)
                    {
                        return savingVersionCompatibility;
                    }
                }
                return DNESavingVersionCompatibility._ESVC_LATEST;
            }
        }


        public static DNESavingVersionCompatibility GetLatestSavingVersionCompatibility()
        {
            Array values = Enum.GetValues(typeof(DNESavingVersionCompatibility));
            return (DNESavingVersionCompatibility)values.GetValue(values.Length - 1);
        }

      /*  public static DNESavingVersionCompatibility GetSavingVersionCompatibility(string version)
        {
            try
            {
                if (version.Contains("("))
                    version = version.Substring(0, version.IndexOf('('));
                return (DNESavingVersionCompatibility)Enum.Parse(typeof(DNESavingVersionCompatibility), version);
            }
            catch (ArgumentException argumentException)
            {
                return SaveParamsData.SavingVersionCompatibility;
            }

        }*/

    }
}
