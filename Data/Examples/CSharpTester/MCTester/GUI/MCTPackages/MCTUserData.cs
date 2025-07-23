using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MCTester.MCTPackages
{
    [DataContract]
    public class MCTUserData
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string Groups { get; set; }

        [DataMember]
        public string Roles { get; set; }

        public MCTUserData() { }
    }
}
