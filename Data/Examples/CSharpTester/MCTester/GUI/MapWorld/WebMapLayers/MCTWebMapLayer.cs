using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MCTester.MapWorld.WebMapLayers
{
    [Serializable]
    public class MCTWebMapLayer
    {
        private string m_Identifier;
        private MCTBoundingBox m_BoundingBox;
        private string m_Title;
        private string m_Format;
        private sbyte m_DrawPriority;
        private string m_Group;

        [XmlElement(Namespace = "http://www.opengis.net/ows/1.1")] 
        public string Identifier
        {
            get { return m_Identifier; }
            set { m_Identifier = value; }
        }

        [XmlElement(Namespace = "http://www.opengis.net/ows/1.1")]
        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }

        [XmlElement]
        public string Format
        {
            get { return m_Format; }
            set { m_Format = value; }
        }

        [XmlElement]
        public sbyte DrawPriority
        {
            get { return m_DrawPriority; }
            set { m_DrawPriority = value; }
        }

        [XmlElement]
        public string Group
        {
            get { return m_Group; }
            set { m_Group = value; }
        }

        [XmlElement(Namespace = "http://www.opengis.net/ows/1.1")]
        public MCTBoundingBox BoundingBox
        {
            get { return m_BoundingBox; }
            set { m_BoundingBox = value; }
        }
    }
}
