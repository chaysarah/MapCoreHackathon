using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTester.Managers.MapWorld;
using MapCore;
using MCTester.Managers.ObjectWorld;
using System.Drawing;
using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.ButtonsImplementation
{
    public class btnDistanceDirectionMeasure
    {
        public btnDistanceDirectionMeasure()
        {
        }

        public void ExecuteAction()
        {
            if (EditModePropertiesBase.P2PWaitForClick == true)
            {
                MCTMapFormManager.MapForm.EditMode.StartDistanceDirectionMeasure(EditModePropertiesBase.ShowResult,
                                                                                    true,
                                                                                    new DNSMcPoint());
            }
            else
            {
                MCTMapFormManager.MapForm.MapObjectCtrl.MouseDownEvent += new General_Forms.MouseDownEventArgs(MapObjectCtrl_MouseDownEvent);
            }
        }

        void MapObjectCtrl_MouseDownEvent(object sender, Point mouseLocation, MouseButtons mouseClickedButton, int mouseWheelDelta)
        {
            try
            {

                DNSMcPoint mousePos = new DNSMcPoint(mouseLocation.X, mouseLocation.Y);
                MCTMapFormManager.MapForm.MapObjectCtrl.MouseDownEvent -= new General_Forms.MouseDownEventArgs(MapObjectCtrl_MouseDownEvent);

                MCTMapFormManager.MapForm.EditMode.StartDistanceDirectionMeasure(EditModePropertiesBase.ShowResult,
                                                                                    false,
                                                                                    mousePos);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("StartDistanceDirectionMeasure", McEx);
            }
        }
    }
}
