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
    public class MCTUsersData : MCTPackageBase
    {
        [DataMember]
        public MCTUserData[] UsersData { get; set; }

        public MCTUsersData()
        {
            UsersData = new MCTUserData[] { };
        }

        public void Load(string fileName)
        {
            MCTUsersData usersData = null;
            if (Load(fileName, out usersData, typeof(MCTUsersData)))
            {
                Load(usersData);
            }
        }

        public bool Load(Stream stream)
        {
            MCTUsersData usersData = null;
            bool resultLoad = Load(stream, out usersData, typeof(MCTUsersData));
            if (resultLoad)
            {
                Load(usersData);
            }
            return resultLoad;
        }

        public void Load(MCTUsersData usersData)
        {
            UsersData = usersData.UsersData;
        }
    }
}
