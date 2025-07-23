using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTester.Managers.MapWorld;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;
using System.Drawing;
using System.Windows.Forms;
using MCTester.General_Forms;

namespace MCTester.ButtonsImplementation
{
    public class btnDynamicZoom
    {
        public btnDynamicZoom()
        {
        }

        public void ExecuteAction()
        {
            if (EditModePropertiesBase.DynamicZoomWaitForClick == true)
            {
                try
                {
                    MCTMapFormManager.MapForm.EditMode.StartDynamicZoom(EditModePropertiesBase.DynamicZoomMinScale,
                                                                            true,
                                                                            new DNSMcPoint(),
                                                                            EditModePropertiesBase.DynamicZoomRectangle,
                                                                            EditModePropertiesBase.DynamicZoomOperation);

                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Dynamic zoom failed", McEx);
                }
            }
            else
            {
                MCTMapFormManager.MapForm.MapObjectCtrl.MouseDownEvent += new MouseDownEventArgs(MapObjectCtrl_MouseDownEvent);
            }  
        }

        void MapObjectCtrl_MouseDownEvent(object sender, Point mouseLocation, MouseButtons mouseClickedButton, int mouseWheelDelta)
        {
            try
            {
                DNSMcPoint mousePos = new DNSMcPoint(mouseLocation);
                MCTMapFormManager.MapForm.MapObjectCtrl.MouseDownEvent -= new MouseDownEventArgs(MapObjectCtrl_MouseDownEvent);

                MCTMapFormManager.MapForm.EditMode.StartDynamicZoom(EditModePropertiesBase.DynamicZoomMinScale,
                                                                        EditModePropertiesBase.DynamicZoomWaitForClick,
                                                                        mousePos,
                                                                        EditModePropertiesBase.DynamicZoomRectangle,
                                                                        EditModePropertiesBase.DynamicZoomOperation);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("StartDynamicZoom", McEx);
            }
        }
    }
}
