using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using MCTester.MapWorld;
using MCTester.MCTPackages;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;

namespace MCTester.Automation
{
    [DataContract]
    class MCTAutomationParams : MCTPackageBase
    {
        [DataMember (Order = 0)]
        public string UserComment { get; set; }

        [DataMember (Order = 1)]
        public MCTMapDevice MapDevice { get; set; }

        [DataMember (Order = 2)]
        public MCTMapViewportData MapViewport { get; set; }

        [DataMember (Order = 3)]
        public MCTMapsBaseDirectory MapsBaseDirectory { get; set; }

        [DataMember (Order = 4)]
        public MCTOverlayManager OverlayManager { get; set; }

        public MCTAutomationParams()
        {
            MapViewport = new MCTMapViewportData();
            OverlayManager = new MCTOverlayManager();
            MapsBaseDirectory = new MCTMapsBaseDirectory();
        }

        public bool Load(Stream stream)
        {
            MCTAutomationParams automationParams = null;
            bool resultLoad = Load(stream, out automationParams, typeof(MCTAutomationParams));
            if (resultLoad)
            {
                Load(automationParams);
            }
            return resultLoad;
        }

        public bool Load(string fileName)
        {
            MCTAutomationParams automationParams = null;
            if (Load(fileName, out automationParams, typeof(MCTAutomationParams)))
            {
                Load(automationParams);
                return true;
            }
            return false;
        }

        public void Load(MCTAutomationParams automationParams)
        {
            MapDevice = automationParams.MapDevice;
            MapViewport = automationParams.MapViewport;
            OverlayManager = automationParams.OverlayManager;
            MapsBaseDirectory = automationParams.MapsBaseDirectory;
        }
    }
}
