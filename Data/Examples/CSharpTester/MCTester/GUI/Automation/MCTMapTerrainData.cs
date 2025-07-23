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
    class MCTMapTerrainData
    {
        [DataMember]
        public sbyte DrawPriority { get; set; }

        [DataMember]
        public uint NumCacheTiles { get; set; }

        [DataMember]
        public uint NumCacheTilesForStaticObjects { get; set; }
    }
}
