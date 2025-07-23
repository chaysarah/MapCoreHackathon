using MapCore;
using MapCore.Common;
using MCTester.MapWorld;
using MCTester.MapWorld.MapUserControls;
using MCTester.ObjectWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.Managers.MapWorld
{
    public class MCTMapLayerReadCallbackFactory : IDNMapLayerReadCallbackFactory
    {
      
        public MCTMapLayerReadCallbackFactory()
        { }

        public IDNMapLayerReadCallback CreateReadCallback(byte[] aBuffer)
        {
            return MCTMapLayerReadCallback.getInstance();
        }
    }
}
