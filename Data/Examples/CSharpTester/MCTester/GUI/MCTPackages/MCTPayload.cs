using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;

namespace MCTester.MCTPackages
{
    [DataContract]
    public class MCTPayload : MCTPackageBase
    {
        [DataMember]
        public string authorization { get; set; }

        [DataMember]
        public string sessionId { get; set; }

        [DataMember]
        public string group { get; set; }

        [DataMember]
        public string[] R { get; set; }

        [DataMember]
        public string exp { get; set; }

        public MCTPayload() { }

        public bool Load(Stream stream)
        {
            MCTPayload payload = null;
            bool resultLoad = Load(stream, out payload, typeof(MCTPayload));
            if (resultLoad)
            {
                Load(payload);
            }
            return resultLoad;
        }

        public void Load(MCTPayload payload)
        {
            authorization = payload.authorization;
            sessionId = payload.sessionId;
            group = payload.group;
            R = payload.R;
            exp = payload.exp;
        }
    }
}
