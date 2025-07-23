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
    public class MCTAuthenticateResponse : MCTPackageBase
    {
        [DataMember]
        public string isAuthenticated { get; set; }

        [DataMember]
        public MCTPayload payload { get; set; }

        public MCTAuthenticateResponse() { }

        public bool Load(Stream stream)
        {
            MCTAuthenticateResponse authenticateResponse = null;
            bool resultLoad = Load(stream, out authenticateResponse, typeof(MCTAuthenticateResponse));
            if (resultLoad)
            {
                Load(authenticateResponse);
            }
            return resultLoad;
        }

        public void Load(MCTAuthenticateResponse authenticateResponse)
        {
            isAuthenticated = authenticateResponse.isAuthenticated;
            payload = authenticateResponse.payload;
        }
    }
}
