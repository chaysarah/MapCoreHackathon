using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using MapCore;

namespace MCTester.Automation
{
    [DataContract]
    public class MCTSectionMapViewport
    {
        [DataMember]
        public DNSMcVector3D[] SectionRoutePoints;

        [DataMember]
        public DNMcNullableOut<DNSMcVector3D[]> SectionHeightPoints;

    }
}
