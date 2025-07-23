using System;
using System.Collections.Generic;
using System.Text;
using UnmanagedWrapper;
using MapCore;
using System.Drawing;
using MapCore.Common;

namespace MCTester.Managers.ObjectWorld
{
    public static class Manager_MCObjectScheme
    {
        static Dictionary<object, uint> dObjectScheme;
        static List<IDNMcObjectScheme> dTempObjectScheme = new List<IDNMcObjectScheme>();

        static Manager_MCObjectScheme()
        {
            dObjectScheme = new Dictionary<object, uint>();
        }

        #region IManagersGetter Members

        public static Dictionary<object, uint> AllParams
        {
            get
            {
                Dictionary<object, uint> dTemp = new Dictionary<object, uint>();
                foreach(IDNMcObjectScheme objectScheme in dObjectScheme.Keys)
                {
                    if (!dTempObjectScheme.Contains(objectScheme))
                        dTemp.Add(objectScheme, dObjectScheme[objectScheme]);
                }
                return dTemp;
            }
        }

        public static Dictionary<object, uint> GetChildren(object Parent)
        {
            Dictionary<object, uint> Ret = new Dictionary<object, uint>();
            IDNMcObjectScheme objectScheme = (IDNMcObjectScheme)Parent;

            uint i = 0;
            IDNMcObjectSchemeNode[] objectLocation = objectScheme.GetNodes(DNENodeKindFlags._ENKF_OBJECT_LOCATION);
            foreach (IDNMcObjectSchemeNode currLocation in objectLocation)
            {
                Ret.Add(currLocation, i++);
            }

            return Ret;
        }

        public static Dictionary<object, uint> GetObjectsUsingScheme(object Parent)
        {
            Dictionary<object, uint> Ret = new Dictionary<object, uint>();
            IDNMcObjectScheme objectScheme = (IDNMcObjectScheme)Parent;

            uint i = 0;
            IDNMcObject[] objects = objectScheme.GetObjects();
            foreach (IDNMcObject currObject in objects)
            {
                Ret.Add(currObject, i++);
            }

            return Ret;
        }

        #endregion

        private static IDNMcObjectScheme m_CurrentScheme = null;
        public static IDNMcObjectScheme CurrentScheme
        {
            set
            {
                try
                {
                    if (value != null)
                    {
                        if (m_CurrentScheme == null || (m_CurrentScheme.GetHashCode() != value.GetHashCode()))
                        {
                            m_CurrentScheme = value;
                            DicStatesNames = new Dictionary<byte, string>();
                            List<object> numbers = new List<object>();
                            for (byte i = 1; i <= 255; i++)
                            {
                                string name = value.GetObjectStateName(i);
                                if (name != "")
                                    DicStatesNames.Add(i, i + " (" + name + ")");
                                else
                                    DicStatesNames.Add(i, i.ToString());
                                if (i == 255)
                                    break;
                            }
                        }
                    }
                    else
                    {
                        m_CurrentScheme = null;
                        DicStatesNames = null;
                    }
                }
                catch(MapCoreException ex)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetObjectStateName", ex);
                }
            }
            get { return m_CurrentScheme; }
        }

        public static Dictionary<byte, string> DicStatesNames { get; set; }

        public static void AddTempObjectScheme(IDNMcObjectScheme mcObjectScheme)
        {
            dTempObjectScheme.Add(mcObjectScheme);
        }

        public static bool IsTempObjectScheme(IDNMcObjectScheme mcObjectScheme)
        {
            return Manager_ShowPointsObjects.CheckIfSchemeExistInShowPointsSchemes(mcObjectScheme) || dTempObjectScheme.Contains(mcObjectScheme);
        }

        public static IDNMcObjectScheme[] GetSchemesWithoutTempSchemes(IDNMcObjectScheme[] schemes)
        {
            List<IDNMcObjectScheme> lstSchemes = new List<IDNMcObjectScheme>();
            foreach (IDNMcObjectScheme scheme in schemes)
            {
                if (!IsTempObjectScheme(scheme))
                    lstSchemes.Add(scheme);
            }
            return lstSchemes.ToArray();
        }

    }
}
