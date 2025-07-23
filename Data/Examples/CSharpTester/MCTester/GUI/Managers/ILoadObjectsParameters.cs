using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTester.Managers
{
    public interface ILoadObjectsParameters
    {
        void SetLoadIsShowVersion(bool isLoadIsShowVersion);
        void SetLoadIsShowStorageFormat(bool isLoadIsShowStorageFormat);
    }
}
