using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTester.Managers.MapWorld;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ButtonsImplementation
{
    public class btnZoomOut
    {
        public btnZoomOut()
        {
        }

        public void ExecuteAction()
        {
            if (MCTMapFormManager.MapForm.Viewport.MapType == DNEMapType._EMT_2D)
            {
                try
                {
                    float currScale = MCTMapFormManager.MapForm.Viewport.GetCameraScale();
                    float newScale = currScale * 2;
                    MCTMapFormManager.MapForm.Viewport.SetCameraScale(newScale);


                    // turn on all viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(MCTMapFormManager.MapForm.Viewport);

                    //Update status bae scale
                    Manager_StatusBar.UpdateScale();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Map scale change process failed", McEx);
                }
            }

            if (MCTMapFormManager.MapForm.Viewport.MapType == DNEMapType._EMT_3D)
            {
                try
                {
                    float currFOV = MCTMapFormManager.MapForm.Viewport.GetCameraFieldOfView();
                    MCTMapFormManager.MapForm.Viewport.SetCameraFieldOfView(currFOV + 5);


                    // turn on all viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(MCTMapFormManager.MapForm.Viewport);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Map Field Of View change process failed", McEx);
                }
            }
        }

    }
}
