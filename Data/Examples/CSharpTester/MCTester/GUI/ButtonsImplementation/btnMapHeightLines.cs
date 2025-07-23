using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTester.Managers.MapWorld;
using UnmanagedWrapper;
using MapCore;

namespace MCTester.ButtonsImplementation
{
    public class btnMapHeightLines
    {
        public btnMapHeightLines()
        {
        }

        public void ExecuteAction()
        {
            IDNMcMapViewport activeVP = MCTMapFormManager.MapForm.Viewport;
            try
            {
                /* bool visible;
                 activeVP.GetHeightLinesVisibility(out visible);
                 activeVP.SetHeightLinesVisibility(!visible);
                 // turn on all viewports render needed flags
                 Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(activeVP);
 */

                IDNMcMapHeightLines mapHeightLines = activeVP.GetHeightLines();
                int count = 4;
                if (mapHeightLines == null)
                {
                    DNSHeightLinesScaleStep[] heightLinesScaleStep;
                    heightLinesScaleStep = new DNSHeightLinesScaleStep[count];
                    DNSMcBColor[] scaleColorsFirst = new DNSMcBColor[3];

                    scaleColorsFirst[0] = new DNSMcBColor(255, 0, 0, 255);
                    scaleColorsFirst[1] = new DNSMcBColor(0, 255, 0, 255);
                    scaleColorsFirst[2] = new DNSMcBColor(0, 0, 255, 255);

                    DNSMcBColor[] scaleColorsSecond = new DNSMcBColor[3];

                    scaleColorsSecond[0] = new DNSMcBColor(255, 255, 0, 255);
                    scaleColorsSecond[1] = new DNSMcBColor(0, 255, 255, 255);
                    scaleColorsSecond[2] = new DNSMcBColor(255, 0, 255, 255);

                    heightLinesScaleStep[0].fMaxScale = 10;
                    heightLinesScaleStep[0].fLineHeightGap = 10;
                    heightLinesScaleStep[0].aColors = scaleColorsFirst;

                    heightLinesScaleStep[1].fMaxScale = 25;
                    heightLinesScaleStep[1].fLineHeightGap = 25;
                    heightLinesScaleStep[1].aColors = scaleColorsSecond;

                    heightLinesScaleStep[2].fMaxScale = 50;
                    heightLinesScaleStep[2].fLineHeightGap = 50;
                    heightLinesScaleStep[2].aColors = scaleColorsFirst;

                    heightLinesScaleStep[3].fMaxScale = 100000;
                    heightLinesScaleStep[3].fLineHeightGap = 100;
                    heightLinesScaleStep[3].aColors = scaleColorsSecond;
                    

                   

                    try
                    {
                        mapHeightLines = DNMcMapHeightLines.Create(heightLinesScaleStep, 1);
                        Manager_MCMapHeightLines.CreateMapHeightLines(mapHeightLines);

                        activeVP.SetHeightLines(mapHeightLines);
                        activeVP.SetHeightLinesVisibility(true);

                        // turn on all viewports render needed flags
                        Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(activeVP);

                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("SetHeightLines", McEx);
                    }
                }
                else
                {
                    activeVP.SetHeightLines(null);
                    Manager_MCMapHeightLines.RemoveMapHeightLines(mapHeightLines);
                    MainForm.RebuildMapWorldTree();
                    mapHeightLines.Dispose();
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Get/SetHeightLinesVisibility", McEx);
            }
        
        }
    }
}
