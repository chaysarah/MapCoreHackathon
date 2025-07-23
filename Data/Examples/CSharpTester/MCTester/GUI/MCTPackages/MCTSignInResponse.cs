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
    class MCTSignInResponse : MCTPackageBase
    {
        [DataMember]
        public string isAuthenticated { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public string payload { get; set; }

        public MCTSignInResponse()
        {
        }

        public void Load(string fileName)
        {
            MCTSignInResponse signInResponse = null;
            if (Load(fileName, out signInResponse, typeof(MCTSignInResponse)))
            {
                Load(signInResponse);
            }
        }

        public bool Load(Stream stream)
        {
            MCTSignInResponse signInResponse = null;
            bool resultLoad = Load(stream, out signInResponse, typeof(MCTSignInResponse));
            if (resultLoad)
            {
                Load(signInResponse);
            }
            return resultLoad;
        }

        public void Load(MCTSignInResponse signInResponse)
        {
            isAuthenticated = signInResponse.isAuthenticated;
            message = signInResponse.message;
            payload = signInResponse.payload;
        }

    }
}
