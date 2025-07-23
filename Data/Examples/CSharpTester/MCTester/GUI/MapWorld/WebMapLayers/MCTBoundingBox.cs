using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MCTester.MapWorld.WebMapLayers
{
    [Serializable]
    public class MCTBoundingBox
    {
        private string m_LowerCorner;
        private string m_UpperCorner;
        private string m_crs;

        [XmlElement(Namespace = "http://www.opengis.net/ows/1.1")]
        public string LowerCorner
        {
            get { return m_LowerCorner; }
            set { m_LowerCorner = value; }
        }

        [XmlElement(Namespace = "http://www.opengis.net/ows/1.1")]
        public string UpperCorner
        {
            get { return m_UpperCorner; }
            set { m_UpperCorner = value; }
        }

        [XmlElement(Namespace = "http://www.opengis.net/ows/1.1")]
        public string crs
        {
            get { return m_crs; }
            set { m_crs = value; }
        }

    }
}
