using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using MCTester.Managers;
using MapCore.Common;

namespace MCTester.ButtonsImplementation
{
    public class btnGrid
    {

        public enum GridTypes
        {
            UTMGrid = 101,
            GeoGrid = 102,
            MGRSGrid = 103,
            NZMGGrid = 104,
            GEOREFGrid = 105
        }

        public btnGrid()
        {
        }

        public void ExecuteAction(string mapGridName)
        {
            try
            {


                IDNMcMapViewport activeVP = MCTMapFormManager.MapForm.Viewport;

                if (activeVP.MapType == DNEMapType._EMT_2D)
                {
                    if (Program.AppMainForm.DefaultActiveMapGrid == "")
                    {
                        activeVP.Grid = null;

                        // turn on all viewports render needed flags
                        Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(activeVP);


                    }
                    else
                    {
                        uint chosenGrid = 0;
                        switch (mapGridName)
                        {
                            case "UTM Grid":
                                chosenGrid = (int)GridTypes.UTMGrid;
                                break;
                            case "Geo Grid":
                                chosenGrid = (int)GridTypes.GeoGrid;
                                break;
                            case "MGRS Grid":
                                chosenGrid = (int)GridTypes.MGRSGrid;
                                break;
                            case "NZMG Grid":
                                chosenGrid = (int)GridTypes.NZMGGrid;
                                break;
                            case "GEOREF Grid":
                                chosenGrid = (int)GridTypes.GEOREFGrid;
                                break;
                        }

                        uint gridValue;
                        foreach (IDNMcMapGrid gridKey in Manager_MCGrid.dGrid.Keys)
                        {
                            Manager_MCGrid.dGrid.TryGetValue(gridKey, out gridValue);

                            if (gridValue == chosenGrid)
                            {
                                try
                                {
                                    // if user change value of UseBasicItemPropertiesOnly and grid is exist, the existing grid removed and new grid is need to create.
                                    bool isUseBasicItemPropertiesOnly = gridKey.IsUsingBasicItemPropertiesOnly();
                                    if (isUseBasicItemPropertiesOnly != Manager_MCGeneralDefinitions.UseBasicItemPropertiesOnly)
                                    {
                                        Manager_MCGrid.dGrid.Remove(gridKey);
                                        break;
                                    }

                                    activeVP.Grid = gridKey;
                                    activeVP.GridVisibility = true;

                                    // turn on all viewports render needed flags
                                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(activeVP);

                                    return;

                                }
                                catch (MapCoreException McEx)
                                {
                                    MapCore.Common.Utilities.ShowErrorMessage("Grid/GridVisibility", McEx);
                                }
                            }
                        }

                        DNSGridRegion[] region = null;
                        DNSScaleStep[] scale = null;

                        switch (mapGridName)
                        {
                            case "UTM Grid":
                                GetUTMMapGrid(out region, out scale);
                                break;
                            case "Geo Grid":
                                GetGeoMapGrid(out region, out scale);
                                break;
                            case "MGRS Grid":
                                GetMGRSMapGrid(out region, out scale);
                                break;
                            case "NZMG Grid":
                                GetNZMGMapGrid(out region, out scale);
                                break;
                            case "GEOREF Grid":
                                GetGeoRefMapGrid(out region, out scale);
                                break;
                        }

                        IDNMcMapGrid newGrid = DNMcMapGrid.Create(region, scale, Manager_MCGeneralDefinitions.UseBasicItemPropertiesOnly);
                        Manager_MCGrid.dGrid.Add(newGrid, chosenGrid);

                        activeVP.Grid = newGrid;
                        activeVP.GridVisibility = true;

                        // turn on all viewports render needed flags
                        Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(activeVP);

                        return;
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcMapGrid.Create", McEx);
            }
        }

        public void GetUTMMapGrid(out DNSGridRegion[] region, out DNSScaleStep[] scale)
        {
            region = new DNSGridRegion[120];
            scale = new DNSScaleStep[6];

            IDNMcMapViewport activeVP = MCTMapFormManager.MapForm.Viewport;
            IDNMcGridCoordinateSystem currCoordsys = activeVP.CoordinateSystem;
            DNSDatumParams dp = currCoordsys.GetDatumParams();
            DNEDatumType datum = currCoordsys.GetDatum();

            for (int i = 0; i < 120; i++)
            {
                int zone = i < 60 ? i-60 : i - 59;
                double minY = i < 60 ? -8400000 : 0;
                double maxY = i < 60 ? 0 : 8400000;

                region[i].pCoordinateSystem = DNMcGridUTM.Create(zone, datum, dp);
                region[i].GeoLimit.MinVertex.x = -18000000 + 600000 * (Math.Abs(zone)-1);
                region[i].GeoLimit.MinVertex.y = minY;
                region[i].GeoLimit.MinVertex.z = double.MinValue;

                region[i].GeoLimit.MaxVertex.x = -17400001 + 600000 * (Math.Abs(zone)-1);
                region[i].GeoLimit.MaxVertex.y = maxY;
                region[i].GeoLimit.MaxVertex.z = double.MaxValue;

                region[i].uFirstScaleStepIndex = 0;
                region[i].uLastScaleStepIndex = 5;
                region[i].pGridLine = null;
                region[i].pGridText = null;
            }

            scale[0] = new DNSScaleStep(12.5f, new DNSMcVector2D(1000, 1000), 2, 2, 5, 5, 3, DNEAngleFormat._EAF_DECIMAL_DEG);
            scale[1] = new DNSScaleStep(25f, new DNSMcVector2D(1000, 1000), 2, 2, 10, 10, 3, DNEAngleFormat._EAF_DECIMAL_DEG);
            scale[2] = new DNSScaleStep(125f, new DNSMcVector2D(20000, 20000), 2, 2, 2, 2, 3, DNEAngleFormat._EAF_DECIMAL_DEG);
            scale[3] = new DNSScaleStep(625f, new DNSMcVector2D(50000, 50000), 2, 2, 2, 2, 3, DNEAngleFormat._EAF_DECIMAL_DEG);
            scale[4] = new DNSScaleStep(1250f, new DNSMcVector2D(100000, 100000), 2, 2, 2, 2, 3, DNEAngleFormat._EAF_DECIMAL_DEG);
            scale[5] = new DNSScaleStep(2500f, new DNSMcVector2D(200000, 200000), 2, 2, 2, 2, 3, DNEAngleFormat._EAF_DECIMAL_DEG);
        }

        public void GetGeoMapGrid(out DNSGridRegion[] region, out DNSScaleStep[] scale)
        {
            region = new DNSGridRegion[1];
            scale = new DNSScaleStep[6];

            IDNMcGridCoordinateSystem gridCoordSys = DNMcGridCoordSystemGeographic.Create(DNEDatumType._EDT_WGS84);
            region[0].pCoordinateSystem = gridCoordSys;
            region[0].GeoLimit.MinVertex.x = 0;
            region[0].GeoLimit.MinVertex.y = 0;
            region[0].GeoLimit.MaxVertex.x = 0;
            region[0].GeoLimit.MaxVertex.y = 0;
            region[0].uFirstScaleStepIndex = 0;
            region[0].uLastScaleStepIndex = 5;
            region[0].pGridLine = null;
            region[0].pGridText = null;

            scale[0] = new DNSScaleStep(14.64f, new DNSMcVector2D(833, 833), 2, 2, 5, 5, 3, DNEAngleFormat._EAF_DEG_MIN);
            scale[1] = new DNSScaleStep(29.296f, new DNSMcVector2D(833, 833), 2, 2, 10, 10, 3, DNEAngleFormat._EAF_DEG_MIN);
            scale[2] = new DNSScaleStep(146.48f, new DNSMcVector2D(20000, 20000), 2, 2, 2, 2, 3, DNEAngleFormat._EAF_DEG_MIN);
            scale[3] = new DNSScaleStep(723.42f, new DNSMcVector2D(50000, 50000), 2, 2, 2, 2, 3, DNEAngleFormat._EAF_DEG_MIN);
            scale[4] = new DNSScaleStep(1464f, new DNSMcVector2D(100000, 100000), 2, 2, 2, 2, 3, DNEAngleFormat._EAF_DEG_MIN);
            scale[5] = new DNSScaleStep(2929f, new DNSMcVector2D(200000, 200000), 2, 2, 2, 2, 3, DNEAngleFormat._EAF_DEG_MIN);
        }

        public void GetGeoRefMapGrid(out DNSGridRegion[] region, out DNSScaleStep[] scale)
        {
            region = new DNSGridRegion[1];
            scale = new DNSScaleStep[6];

            IDNMcGridCoordinateSystem gridCoordSys = DNMcGridGEOREF.Create();
            region[0].pCoordinateSystem = gridCoordSys;
            region[0].GeoLimit.MinVertex.x = 0;
            region[0].GeoLimit.MinVertex.y = 0;
            region[0].GeoLimit.MaxVertex.x = 0;
            region[0].GeoLimit.MaxVertex.y = 0;
            region[0].uFirstScaleStepIndex = 0;
            region[0].uLastScaleStepIndex = 5;
            region[0].pGridLine = null;
            region[0].pGridText = null;

            scale[0] = new DNSScaleStep(14.64f, new DNSMcVector2D(833, 833), 2, 2, 5, 5, 3, DNEAngleFormat._EAF_DEG_MIN);
            scale[1] = new DNSScaleStep(29.296f, new DNSMcVector2D(833, 833), 2, 2, 10, 10, 3, DNEAngleFormat._EAF_DEG_MIN);
            scale[2] = new DNSScaleStep(146.48f, new DNSMcVector2D(20000, 20000), 2, 2, 2, 2, 3, DNEAngleFormat._EAF_DEG_MIN);
            scale[3] = new DNSScaleStep(723.42f, new DNSMcVector2D(50000, 50000), 2, 2, 2, 2, 3, DNEAngleFormat._EAF_DEG_MIN);
            scale[4] = new DNSScaleStep(1464f, new DNSMcVector2D(100000, 100000), 2, 2, 2, 2, 3, DNEAngleFormat._EAF_DEG_MIN);
            scale[5] = new DNSScaleStep(2929f, new DNSMcVector2D(200000, 200000), 2, 2, 2, 2, 3, DNEAngleFormat._EAF_DEG_MIN);
        }

        public void GetMGRSMapGrid(out DNSGridRegion[] region, out DNSScaleStep[] scale)
        {
            region = new DNSGridRegion[2];
            scale = new DNSScaleStep[2];

            IDNMcGridCoordinateSystem gridCoordSys = DNMcGridMGRS.Create();
            
            region[0].pCoordinateSystem = gridCoordSys;
            region[0].GeoLimit.MinVertex.x = 0;
            region[0].GeoLimit.MinVertex.y = 0;
            region[0].GeoLimit.MaxVertex.x = 0;
            region[0].GeoLimit.MaxVertex.y = 0;
            region[0].uFirstScaleStepIndex = 0;
            region[0].uLastScaleStepIndex = 0;
            region[0].pGridLine = null;
            region[0].pGridText = null;

            region[1].pCoordinateSystem = gridCoordSys;
            region[1].GeoLimit.MinVertex.x = 0;
            region[1].GeoLimit.MinVertex.y = 0;
            region[1].GeoLimit.MaxVertex.x = 0;
            region[1].GeoLimit.MaxVertex.y = 0;
            region[1].uFirstScaleStepIndex = 1;
            region[1].uLastScaleStepIndex = 1;
            region[1].pGridLine = null;
            region[1].pGridText = null;

            scale[0] = new DNSScaleStep(1000f, new DNSMcVector2D(100000, 100000), 2, 2, 2, 2, 2, DNEAngleFormat._EAF_DECIMAL_DEG);
            scale[1] = new DNSScaleStep(1000f, new DNSMcVector2D(20000, 20000), 2, 2, 2, 2, 2, DNEAngleFormat._EAF_DECIMAL_DEG);            
        }

        public void GetNZMGMapGrid(out DNSGridRegion[] region, out DNSScaleStep[] scale)
        {
            region = new DNSGridRegion[1];
            scale = new DNSScaleStep[1];

            IDNMcGridCoordinateSystem gridCoordSys = DNMcGridNZMG.Create();

            region[0].pCoordinateSystem = gridCoordSys;
            region[0].GeoLimit.MinVertex.x = 0;
            region[0].GeoLimit.MinVertex.y = 0;
            region[0].GeoLimit.MaxVertex.x = 0;
            region[0].GeoLimit.MaxVertex.y = 0;
            region[0].uFirstScaleStepIndex = 0;
            region[0].uLastScaleStepIndex = 0;
            region[0].pGridLine = null;
            region[0].pGridText = null;

            scale[0] = new DNSScaleStep(100000f, new DNSMcVector2D(10000, 10000), 2, 2, 2, 2, 2, DNEAngleFormat._EAF_DECIMAL_DEG);            
        }
    }
}
