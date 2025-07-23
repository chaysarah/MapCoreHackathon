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
    class MCTImageProcessingCustomFilter
    {
        [DataMember]
        public float Bias { get; set; }

        [DataMember]
        public float Divider { get; set; }

        [DataMember]
        public uint FilterXsize { get; set; }

        [DataMember]
        public uint FilterYsize { get; set; }

        [DataMember]
        public float[] Filters { get; set; }

        public MCTImageProcessingCustomFilter()
        {
            
        }
    }
}
