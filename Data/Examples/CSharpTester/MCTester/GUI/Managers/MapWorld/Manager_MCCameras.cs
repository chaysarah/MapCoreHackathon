using System;
using System.Collections.Generic;
using System.Text;

namespace MCTester.Managers.MapWorld
{
    public static class Manager_MCCameras
    {
        public static Dictionary<object, uint > dCameras;

        static Manager_MCCameras()
        {
            dCameras = new Dictionary<object, uint>();
        }
        
        public static Dictionary<object, uint > AllParams
        {
            get { return dCameras; }
        }

        public static Dictionary<object, uint > GetChildren(object Parent)
        {
            Dictionary<object, uint > Ret = new Dictionary<object, uint >();

            return Ret;
        }
    }
}
