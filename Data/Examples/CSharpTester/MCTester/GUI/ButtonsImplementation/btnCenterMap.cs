using MapCore;
using MapCore.Common;
using MCTester.Managers.MapWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.ButtonsImplementation
{
    public class btnCenterMap
    {
        
        public btnCenterMap() { }

        public void ExecuteAction(IDNMcMapViewport mapViewport = null)
        {
            DNSMcVector3D centerPoint;
            IDNMcMapViewport mapCurrViewport= MCTMapFormManager.MapForm.Viewport;
            if (mapViewport != null)
                mapCurrViewport = mapViewport;
            try
            {
                centerPoint = mapCurrViewport.GetTerrainsBoundingBox().CenterPoint();
                MCTAsyncQueryCallback.MoveToCenterPoint(centerPoint, mapCurrViewport);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTerrainsBoundingBox", McEx);
            }
        }
    }
}
