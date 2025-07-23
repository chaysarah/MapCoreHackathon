using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MCTester.Automation
{
    [DataContract]
    class MCTOverlayManager
    {
        [DataMember]
        public MCTGridCoordinateSystem GridCoordinateSystem { get; set; }

        [DataMember]
        public List<string> Overlays { get; set; }

        public MCTOverlayManager()
        {
            GridCoordinateSystem = new MCTGridCoordinateSystem();
            Overlays = new List<string>();
        }

    }
}
