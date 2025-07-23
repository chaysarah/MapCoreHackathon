using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using MCTester.MapWorld;

namespace MCTester.MapWorld
{
    [Serializable]
    public class MCTMapSchema
    {
        private int m_ID;
        private string m_SchemaArea;
        private string m_SchemaMapType;
        private string m_SchemaVPCoordSys;
        private string m_SchemaOMCoordSys;
        private string m_SchemaComments;
        private List<int> m_Viewport;
        private int m_OverlayManagerID = -1;
        
        public MCTMapSchema()
        {
            m_Viewport = new List<int>();
        }

        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        public string Area
        {
            get { return m_SchemaArea; }
            set { m_SchemaArea = value; }
        }

        public string MapType
        {
            get { return m_SchemaMapType; }
            set { m_SchemaMapType = value; }
        }

        public string VPCoordSys
        {
            get { return m_SchemaVPCoordSys; }
            set { m_SchemaVPCoordSys = value; }
        }

        public string OMCoordSys
        {
            get { return m_SchemaOMCoordSys; }
            set { m_SchemaOMCoordSys = value; }
        }

        public string Comments
        {
            get { return m_SchemaComments; }
            set { m_SchemaComments = value; }
        }

        public List<int> ViewportsIDs
        {
            get { return m_Viewport; }
            set { m_Viewport = value;}
        }

        public int OverlayManagerID
        {
            get { return m_OverlayManagerID; }
            set { m_OverlayManagerID = value; }
        }
    }
}
