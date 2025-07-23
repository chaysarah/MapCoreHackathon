using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using System.Windows.Forms;

namespace MCTester.Managers.VectorialWorld
{
    public static class Manager_MCVectorial
    {
        static List<MapProductionParams> lMapProduction = new List<MapProductionParams>();

        static Manager_MCVectorial()
        {
            
        }

        public static List<MapProductionParams> lMapProductionParams
        {
            get { return lMapProduction; }
            set { lMapProduction = value; }
        }
    }

    public class MapProductionParams
    {
        private IDNMcMapProduction m_MapProduction;
        private IDNMcMapViewport m_Viewport;
        private List<IDNMcObject> m_lObjectsToUpdate;
        private IDNMcNativeVectorMapLayer m_NativeVectorLayer;
        private string m_UpdateLayerDir;
        
        public MapProductionParams()
        {   
            try
            {
                m_MapProduction = DNMcMapProduction.Create();

                m_Viewport = null;
                m_lObjectsToUpdate = new List<IDNMcObject>();

                MessageBox.Show("Map production created successfully", "Map Production Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcMapProduction.Create", McEx);
            }

            
        }

        public IDNMcMapProduction MapProduction
        {
            get { return m_MapProduction; }
            set { m_MapProduction = value; }
        }

        public IDNMcMapViewport Viewport
        {
            get { return m_Viewport; }
            set { m_Viewport = value; }
        }

        public IDNMcNativeVectorMapLayer NativeVectorLayer
        {
            get { return m_NativeVectorLayer; }
            set { m_NativeVectorLayer = value; }
        }

        public string UpdateLayerDir
        {
            get { return m_UpdateLayerDir; }
            set { m_UpdateLayerDir = value; }
        }

        public List<IDNMcObject> lObjectsToUpdate
        {
            get { return m_lObjectsToUpdate; }
            set { m_lObjectsToUpdate = value; }
        }
    }
}
