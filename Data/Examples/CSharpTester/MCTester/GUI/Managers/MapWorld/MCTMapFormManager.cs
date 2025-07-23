using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using System.Windows.Forms;
using System.Collections;
using MCTester.GUI;
using System.Drawing;
using MCTester.GUI.Map;
using MCTester.MapWorld;

namespace MCTester.Managers.MapWorld
{
    public static class MCTMapFormManager
    {

        #region Data Member
        private static MCTMapForm m_MapForm;
        private static Dictionary<int, object> m_AllForms = new Dictionary<int, object>();
        private static int m_NextMapForm;

        #endregion

        #region Public Properties
        public static MCTMapForm MapForm
        {
            get { return m_MapForm; }
            set { m_MapForm = value; }
        }
        public static Dictionary<int, object>AllForms
        {
            get { return m_AllForms; }
            set { m_AllForms = value; }
        }

        public static int NextMapForm
        {
            get { return m_NextMapForm; }
            set { m_NextMapForm = value; }
        } 
        #endregion

        #region Public Method
        public static void DisposeAllWrapperObjects()
        {
            DNMcGarbageCollector.DisposeAll();            
        }

        public static void CloseTester()
        {
            if (MCTMapDevice.m_Device != null)
            {
                MCTMapDevice.m_Device.Dispose();
            }
        }

        public static void AddMapForm(MCTMapForm currentMap)
        {
            //Add the map to the dictionary
            m_AllForms.Add(m_NextMapForm, currentMap);
            m_NextMapForm++;

            ActivateMapForm(currentMap);

            if (m_AllForms.Count == 1)
                currentMap.WindowState = FormWindowState.Maximized;
            else
                if (m_AllForms.Count > 1)
                    Program.AppMainForm.LayoutMdi(MdiLayout.TileVertical);
        }

        public static void RemoveMapForm(MCTMapForm mapForm)
        {
            int mapFormIndex = GetKeyOfMap(mapForm);
            //remove the map from the dictionary
            if (m_AllForms.ContainsKey(mapFormIndex))
            {
                MCTMapForm MapFormToRemove = (MCTMapForm)m_AllForms[mapFormIndex];
                m_AllForms.Remove(mapFormIndex);
                if (MapForm == MapFormToRemove)
                    MapForm = null;
            }
        }

        private static int GetKeyOfMap(MCTMapForm map)
        {
            int ret = -1;
            if (m_AllForms.ContainsValue(map))
            {
                foreach (int key in m_AllForms.Keys)
                {
                    if (m_AllForms[key] == map)
                    {
                        ret = key;
                        break;
                    }
                }
            }
            return ret;
        }

        public static void ShowMapForm(int mapFormId)
        {
            //show the map form
            MCTMapForm currentMapForm = (MCTMapForm)m_AllForms[mapFormId];
            currentMapForm.Visible = true;
        }
        public static void HideMapForm(int mapFormId)
        {
            //hide the map form
            MCTMapForm currentMapForm = (MCTMapForm)m_AllForms[mapFormId];
            currentMapForm.Visible = false;
            currentMapForm.BackColor = SystemColors.Control;
        }

        public static void ActivateMapForm(MCTMapForm currentMap)
        {
            //set the active map
            if (currentMap != null)
            {
                if (MapForm != currentMap)
                    MapForm = currentMap;    
            }        
        }
        
        //Reterns all map forms
        public static MCTMapForm[] AllMapForms
        {
            get
            {
                List<MCTMapForm> ret = new List<MCTMapForm>();
                foreach (int key in m_AllForms.Keys)
                {
                    MCTMapForm map = (MCTMapForm)m_AllForms[key];
                    ret.Add(map);
                }
                return ret.ToArray();
            }
        }

        #endregion
    }
}
