using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MCTester.Automation
{
    [DataContract]
    class MCTMapsBaseDirectory
    {
        [DataMember]
        public string Windows { get; set; }

        [DataMember]
        public string Android { get; set; }

        [DataMember]
        public string Web { get; set; }
    }
}
