using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MCTester.MapWorld.WebMapLayers
{
    [Serializable]
    [XmlRoot("Capabilities", Namespace = "http://www.opengis.net/wmts/1.0")]  
    public class Capabilities
    {
        private MCTContents m_Contents;
        private MCTServiceMetadataURL m_ServiceMetadataURL;

        [XmlElement]
        public MCTContents Contents
        {
            get { return m_Contents; }
            set { m_Contents = value; }
        }
        [XmlElement]
        public MCTServiceMetadataURL ServiceMetadataURL
        {
            get { return m_ServiceMetadataURL; }
            set { m_ServiceMetadataURL = value; }
        }

        public Capabilities()
        {
            m_Contents = new MCTContents();
            m_ServiceMetadataURL = new MCTServiceMetadataURL();
        }
            
    }
}
