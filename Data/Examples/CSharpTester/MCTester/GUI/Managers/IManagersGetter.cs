using System;
using System.Collections.Generic;
using System.Text;

namespace MCTester.Managers
{
    public interface IManagersGetter
    {
        Dictionary<object, uint > AllParams
        {
            get;
        }

        Dictionary<object, uint > GetChildren(object Parent);
    }
}
