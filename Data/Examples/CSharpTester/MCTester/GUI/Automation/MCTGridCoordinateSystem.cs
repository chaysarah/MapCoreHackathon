using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MCTester.Automation
{
    [DataContract]
    public class MCTGridCoordinateSystem 
    {
        [DataMember]
        public string GridCoordinateSystemType { get; set; }

        [DataMember]
        public string Datum { get; set; }

        [DataMember]
        public int Zone { get; set; }

        [DataMember]
        public string EpsgCode { get; set; }

        [DataMember]
        public string FullInitialization { get; set; }

        [DataMember]
        public string[] GridParameters { get; set; }
    }
}
