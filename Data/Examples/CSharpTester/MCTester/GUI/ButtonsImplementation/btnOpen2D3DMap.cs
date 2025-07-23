using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTester.Managers.ObjectWorld;
using MCTester.General_Forms;
using MapCore;
using UnmanagedWrapper;
using System.Windows.Forms;
using MCTester.Managers.MapWorld;
using MCTester.MapWorld.MapUserControls;
using System.Windows.Media.Media3D;
using MCTester.GUI.Map;

namespace MCTester.ButtonsImplementation
{
    public class btnOpen2D3DMap
    {
        public btnOpen2D3DMap()
        {
        }

        public void ExecuteAction()
        {
            try
            {
                if (MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport != null)
                {
                    IDNMcMapViewport mcCurrentMapViewport = MCTMapFormManager.MapForm.Viewport;
                    if (mcCurrentMapViewport.MapType == DNEMapType._EMT_2D)
                    {
                        if (mcCurrentMapViewport.GetImageCalc() != null)
                        {
                            MessageBox.Show("Image Calc Viewport Can't Opened As 3D Map.", "Open 3D Map");
                            return;
                        }
                        if (mcCurrentMapViewport is IDNMcSectionMapViewport)
                        {
                            MessageBox.Show("Section Map Viewport Can't Opened As 3D Map.", "Open 3D Map");
                            return;
                        }

                    }
                    MCTMapForm mNewMapForm = new MCTMapForm(false);
                    DNSCreateDataMV newCreateParams = mcCurrentMapViewport.GetCreateParams();
                    newCreateParams.pStereoRenderCallback = mNewMapForm;
                    newCreateParams.hWnd = mNewMapForm.MapPointer;

                    if (newCreateParams.eMapType == DNEMapType._EMT_2D)
                        newCreateParams.eMapType = DNEMapType._EMT_3D;
                    else if (newCreateParams.eMapType == DNEMapType._EMT_3D)
                        newCreateParams.eMapType = DNEMapType._EMT_2D;

                    IDNMcMapViewport mcNewMapViewport1 = null;
                    IDNMcMapCamera mcMapCamera1 = null;
                    DNMcMapViewport.Create(ref mcNewMapViewport1,
                                            ref mcMapCamera1,
                                            newCreateParams,
                                            mcCurrentMapViewport.GetTerrains(),
                                            mcCurrentMapViewport.GetQuerySecondaryDtmLayers());

                    try
                    {
                        mNewMapForm.CreateEditMode(mcNewMapViewport1);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("CreateEditMode", McEx);
                    }

                    mNewMapForm.Viewport = mcNewMapViewport1;

                    Manager_MCViewports.AddViewport(mcNewMapViewport1);

                    mcNewMapViewport1.PerformPendingUpdates(DNEPendingUpdateType._EPUT_ANY_UPDATE);
                    MCTAsyncQueryCallback.MoveToCenterPoint(mcCurrentMapViewport.ActiveCamera.GetCameraPosition(), mcNewMapViewport1);
                    mNewMapForm.Show();
                    MCTMapFormManager.AddMapForm(mNewMapForm);

                    // turn on all viewports render needed flags
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Create 3D Map", McEx);
            }

        }
    }
}
