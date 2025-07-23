using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MCTester.MapWorld.WebMapLayers
{
    [Serializable]
    public class MCTContents
    {
        private List<MCTWebMapLayer> m_Layer;

        [XmlElement]
        public List<MCTWebMapLayer> Layer
        {
            get { return m_Layer; }
            set { m_Layer = value; }
        }

        public MCTContents()
        {
            m_Layer = new List<MCTWebMapLayer>();
        }
    }
}
