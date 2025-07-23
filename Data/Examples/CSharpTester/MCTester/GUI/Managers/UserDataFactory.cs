using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester
{
    public class UserDataFactory: IDNMcUserDataFactory
    {
        #region IDNMcUserDataFactory Members

        public IDNMcUserData CreateUserData(byte[] aBuffer)
        {
            TesterUserData ret = new TesterUserData(aBuffer);
            return ret;
        }

        #endregion
    }
}
