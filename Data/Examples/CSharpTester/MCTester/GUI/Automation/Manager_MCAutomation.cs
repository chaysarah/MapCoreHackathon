using MapCore;
using MapCore.Common;
using MCTester.General_Forms;
using MCTester.GUI.Map;
using MCTester.Managers;
using MCTester.Managers.MapWorld;
using MCTester.Managers.ObjectWorld;
using MCTester.MapWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnmanagedWrapper;
using static MCTester.ButtonsImplementation.btnGrid;

namespace MCTester.Automation
{
    public class Manager_MCAutomation : ICreateSectionMap
    {
        private DNSCreateDataMV m_CreateData;
        private IDNMcMapTerrain[] m_arrTerrain = null;
        private MCTAutomationParams m_AutomationParams;
        private MCTMapForm m_NewMapForm = null;
        private string m_FolderPath = "";
        private IDNMcMapCamera m_Camera = null;
        Stopwatch m_AutoTimeUntilRender = new Stopwatch();

        public static void HandleMapCoreExecption(string exTitle, MapCoreException McEx)
        {
            if(MainForm.m_AutoIsShowMsg)
                Utilities.ShowErrorMessage(exTitle, McEx);
            if (MainForm.m_AutoStreamWriter != null)
                MainForm.m_AutoStreamWriter.WriteLine(McEx.Message);
        }

        public static void HandleTesterExecption(string msgTitle, string msg)
        {
            if (MainForm.m_AutoIsShowMsg)
                MessageBox.Show(msg, msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (MainForm.m_AutoStreamWriter != null)
                MainForm.m_AutoStreamWriter.WriteLine(msgTitle + " - " + msg);
        }

        static Manager_MCAutomation mInstance = null;

        public static Manager_MCAutomation GetInstance()
        {
            if (mInstance == null)
                mInstance = new Manager_MCAutomation();
            return mInstance;
        }

        public void LoadFromJson(string filename, string folderPath, bool bWithoutSize = false)
        {
            MainForm.m_AutoTimeRender = new Stopwatch();
            MainForm.m_AutoStreamWriter = null;
            MainForm.m_AutoViewport = null;
            MainForm.m_isNeedCheckAutoViewport = false;

            
            m_AutoTimeUntilRender.Start();
            MainForm.m_AutoTimeAll = new Stopwatch();
            MainForm.m_AutoTimeAll.Start();

            m_FolderPath = folderPath;
           /* IDNMcMapViewport m_AutoViewport = null;
            bool m_isNeedCheckAutoViewport;
            StreamWriter m_AutoStreamWriter;
            Stopwatch m_AutoTimeRender;
            IDNMcMapViewport AutoViewport = null;*/

            try
            {
                if (File.Exists(filename))
                {
                    string logFileName = "win_autorender_log.txt";
                    string fullLogFileName = Path.Combine(folderPath, logFileName);
                    try
                    {
                        MainForm.m_AutoStreamWriter = new StreamWriter(fullLogFileName);
                    }
                    catch (Exception ex) {
                        HandleTesterExecption("Automation", "Error create log file, \n" + ex.Message);
                        MainForm.CloseStreamWriter();
                        return;
                    }

                    m_AutomationParams = new MCTAutomationParams();
                    if (m_AutomationParams.Load(filename))
                    {
                        //IDNMcMapCamera Camera = null;
                        int width = (int)m_AutomationParams.MapViewport.ViewportSize.ViewportWidth;
                        int height = (int)m_AutomationParams.MapViewport.ViewportSize.ViewportHeight;

                        
                        if (bWithoutSize)
                            m_NewMapForm = new MCTMapForm(false);
                        else
                            m_NewMapForm = new MCTMapForm(width, height);

                        MCTMapFormManager.AddMapForm(m_NewMapForm);

                        uint uViewportID = m_AutomationParams.MapViewport.ViewportID;

                        DNEMapType eMapType = (DNEMapType)Enum.Parse(typeof(DNEMapType), m_AutomationParams.MapViewport.ViewportMapType);

                        m_CreateData = new DNSCreateDataMV(eMapType);
                        m_CreateData.uViewportID = uViewportID;
                        m_CreateData.hWnd = m_NewMapForm.MapPointer;
                        m_CreateData.uWidth = (uint)width;
                        m_CreateData.uHeight = (uint)height;
                        m_CreateData.pGrid = null;
                        m_CreateData.bShowGeoInMetricProportion = m_AutomationParams.MapViewport.ViewportShowGeoInMetricProportion;
                        m_CreateData.pImageCalc = null;

                        try
                        {
                            MCTMapDevice MapDevice = new MCTMapDevice();
                            MapDevice = m_AutomationParams.MapDevice;
                            m_CreateData.pDevice = MapDevice.CreateDevice(false);
                        }
                        catch (MapCoreException McEx)
                        {
                            HandleMapCoreExecption("DNMcMapDevice.Create", McEx);
                            MainForm.CloseStreamWriter();
                            return;
                        }

                        if (m_AutomationParams.MapViewport.GridCoordinateSystem.GridCoordinateSystemType != null)
                        {
                            m_CreateData.CoordinateSystem = CreateGridCoordinateSystem(m_AutomationParams.MapViewport.GridCoordinateSystem);
                            if (m_CreateData.CoordinateSystem == null)
                            {
                                HandleTesterExecption("Load Map From File", "Invalid MapViewport.GridCoordinateSystem");
                                MainForm.CloseStreamWriter();
                                return;
                            }
                        }

                        if (m_AutomationParams.MapViewport.Terrains != null)
                        {
                            // Read number of terrains 
                            int numTerrains = m_AutomationParams.MapViewport.Terrains.Count;
                            try
                            {
                                // Collect terrain array
                                if (numTerrains > 0)
                                {
                                    m_arrTerrain = new IDNMcMapTerrain[numTerrains];
                                    UserDataFactory UDF = new UserDataFactory();

                                    for (int i = 0; i < numTerrains; i++)
                                    {
                                        string folderName = m_AutomationParams.MapViewport.Terrains[i];
                                        if (!Path.IsPathRooted(folderName))
                                            folderName = folderPath + "\\" + folderName;

                                        string baseDir = "";
                                        if (m_AutomationParams.MapsBaseDirectory != null)
                                            baseDir = m_AutomationParams.MapsBaseDirectory.Windows;

                                        if (baseDir == null || baseDir == "")     // to support old tests that not exists WindowsMapsBaseDirectory data (exist amir tests that mapped to c:\maps)
                                            baseDir = frmSaveSessionToFolderDef.WinMaps;
                                        m_arrTerrain[i] = DNMcMapTerrain.Load(folderName, baseDir, UDF, new MCTMapLayerReadCallbackFactory());
                                        Manager_MCLayers.CheckingAfterLoadTerrain(m_arrTerrain[i]);
                                    }
                                }
                            }
                            catch (MapCoreException McEx)
                            {
                                HandleMapCoreExecption("DNMcMapTerrain.Load", McEx);
                                MainForm.CloseStreamWriter();
                                return;
                            }
                            List<IDNMcMapLayer> arrQuerySecondaryDtmMapLayers = null;
                            if (m_AutomationParams.MapViewport.QuerySecondaryDTMLayers != null)
                            {
                                // Read number of terrains 
                                int numQuerySecondaryDTMLayers = m_AutomationParams.MapViewport.QuerySecondaryDTMLayers.Count;
                                try
                                {
                                    // Collect terrain array
                                    if (numQuerySecondaryDTMLayers > 0)
                                    {
                                        arrQuerySecondaryDtmMapLayers = new List<IDNMcMapLayer>();
                                        UserDataFactory UDF = new UserDataFactory();

                                        for (int i = 0; i < numQuerySecondaryDTMLayers; i++)
                                        {
                                            string fileName = m_AutomationParams.MapViewport.QuerySecondaryDTMLayers[i];
                                            if (!Path.IsPathRooted(fileName))
                                                fileName = folderPath + "\\" + fileName;

                                            string baseDir = "";
                                            if (m_AutomationParams.MapsBaseDirectory != null)
                                                baseDir = m_AutomationParams.MapsBaseDirectory.Windows;

                                            if (baseDir == null || baseDir == "")     // to support old tests that not exists WindowsMapsBaseDirectory data (exist amir tests that mapped to c:\maps)
                                                baseDir = frmSaveSessionToFolderDef.WinMaps;
                                            arrQuerySecondaryDtmMapLayers.Add(DNMcMapLayer.Load(fileName, baseDir, UDF, new MCTMapLayerReadCallbackFactory()));
                                        }
                                    }
                                }
                                catch (MapCoreException McEx)
                                {
                                    HandleMapCoreExecption("DNMcMapTerrain.Load", McEx);
                                    MainForm.CloseStreamWriter();
                                    return;
                                }
                            }
                                if (m_AutomationParams.OverlayManager != null && m_AutomationParams.OverlayManager.GridCoordinateSystem != null && m_AutomationParams.OverlayManager.GridCoordinateSystem.GridCoordinateSystemType != null)
                            {
                                IDNMcGridCoordinateSystem overlayManagerCoordSys = CreateGridCoordinateSystem(m_AutomationParams.OverlayManager.GridCoordinateSystem);
                                if (overlayManagerCoordSys == null)
                                {
                                    HandleTesterExecption("Load Map From File", "Invalid OverlayManager.GridCoordinateSystem");
                                    MainForm.CloseStreamWriter();
                                    return;
                                }
                                try
                                {
                                    IDNMcOverlayManager overlayManager = Manager_MCOverlayManager.CreateOverlayManager(overlayManagerCoordSys);
                                    m_CreateData.pOverlayManager = overlayManager;
                                }
                                catch (MapCoreException McEx)
                                {
                                    HandleMapCoreExecption("DNMcOverlayManager.Create", McEx);
                                    MainForm.CloseStreamWriter();
                                    return;
                                }
                            }
                            if (!m_AutomationParams.MapViewport.IsSectionMapViewport)
                            {
                                try
                                {
                                    DNMcMapViewport.Create(ref MainForm.m_AutoViewport, ref m_Camera, m_CreateData, m_arrTerrain, Manager_MCLayers.GetLayersAsDTM(arrQuerySecondaryDtmMapLayers));
                                }
                                catch (MapCoreException McEx)
                                {
                                    HandleMapCoreExecption("DNMcMapViewport.Create", McEx);
                                    MainForm.CloseStreamWriter();
                                    return;
                                }
                                SetAutomationParamsAfterViewportCreate();
                            }
                            else   // create section map
                            {
                                try
                                {
                                    IDNMcSectionMapViewport m_SectionMapViewport = null;
                                    DNMcSectionMapViewport.CreateSection(ref m_SectionMapViewport,
                                                         ref m_Camera,
                                                         m_CreateData,
                                                         m_arrTerrain,
                                                         m_AutomationParams.MapViewport.SectionMapViewportData.SectionRoutePoints,
                                                         m_AutomationParams.MapViewport.SectionMapViewportData.SectionHeightPoints.Value);

                                    MainForm.m_AutoViewport = m_SectionMapViewport;

                                    SetAutomationParamsAfterViewportCreate();
                                }
                                catch (MapCoreException McEx)
                                {
                                    HandleMapCoreExecption("DNMcSectionMapViewport.CreateSection", McEx);
                                    MainForm.CloseStreamWriter();
                                    return;
                                }
                            }

                        }
                    }
                }
            }
            //catch (Exception ex)
            catch (MapCoreException McEx)
            {
                HandleTesterExecption("Automation error", McEx.Message);
                MainForm.CloseStreamWriter();
            }
        }

        private void SetAutomationParamsAfterViewportCreate()
        {
            IDNMcMapViewport AutoViewport = MainForm.m_AutoViewport;
            if (MainForm.m_AutoViewport != null)
            {
                
                Manager_MCViewports.AddViewport(MainForm.m_AutoViewport);
                Manager_MCGeneralDefinitions.m_ShowGeoInMetricProportion = m_AutomationParams.MapViewport.ViewportShowGeoInMetricProportion;

                try
                {
                    if (m_AutomationParams.MapViewport.ViewportBackgroundColor != null && m_AutomationParams.MapViewport.ViewportBackgroundColor != DNSMcBColor.bcBlackTransparent)
                        MainForm.m_AutoViewport.SetBackgroundColor(m_AutomationParams.MapViewport.ViewportBackgroundColor);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetBackgroundColor", McEx);
                }

                m_NewMapForm.Viewport = MainForm.m_AutoViewport;
                m_NewMapForm.CreateEditMode(MainForm.m_AutoViewport);

                if (m_AutomationParams.OverlayManager != null && m_AutomationParams.OverlayManager.Overlays != null)
                {
                    // Read number of overlays 
                    int numOverlays = m_AutomationParams.OverlayManager.Overlays.Count;

                    // Collect overlay array
                    try
                    {
                        if (numOverlays > 0)
                        {
                            UserDataFactory UDF = new UserDataFactory();

                            for (int i = 0; i < numOverlays; i++)
                            {
                                string folderName = m_AutomationParams.OverlayManager.Overlays[i];
                                if (!Path.IsPathRooted(folderName))
                                    folderName = m_FolderPath + "\\" + folderName;

                                IDNMcOverlay overlay = DNMcOverlay.Create(AutoViewport.OverlayManager);
                                if (i == 0)
                                    Manager_MCOverlayManager.UpdateOverlayManager(AutoViewport.OverlayManager, overlay);
                                overlay.LoadObjects(folderName, UDF);
                            }
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        HandleMapCoreExecption("DNMcOverlay.Create or LoadObjects", McEx);
                    }
                }
                m_NewMapForm.WindowState = FormWindowState.Normal;
                m_NewMapForm.Show();

                if (m_AutomationParams.MapViewport.MapTerrainsData != null && m_AutomationParams.MapViewport.MapTerrainsData.Count > 0)
                {
                    try
                    {
                        for (int i = 0; i < m_arrTerrain.Length; i++)
                        {
                            if (m_AutomationParams.MapViewport.MapTerrainsData.ContainsKey(i))
                            {
                                MCTMapTerrainData mapTerrainData = m_AutomationParams.MapViewport.MapTerrainsData[i];
                                AutoViewport.SetTerrainDrawPriority(m_arrTerrain[i], mapTerrainData.DrawPriority);
                                AutoViewport.SetTerrainNumCacheTiles(m_arrTerrain[i], false, mapTerrainData.NumCacheTiles);
                                AutoViewport.SetTerrainNumCacheTiles(m_arrTerrain[i], true, mapTerrainData.NumCacheTilesForStaticObjects);
                            }
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        HandleMapCoreExecption("SetTerrainDrawPriority/SetTerrainNumCacheTiles", McEx);
                    }
                }

                try
                {
                    double x, y, z;
                    x = m_AutomationParams.MapViewport.CameraData.CameraPosition.X;
                    y = m_AutomationParams.MapViewport.CameraData.CameraPosition.Y;
                    z = m_AutomationParams.MapViewport.CameraData.CameraPosition.Z;

                    m_Camera.SetCameraPosition(new DNSMcVector3D(x, y, z), false);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetCameraPosition", McEx);
                }

                if (AutoViewport.MapType == DNEMapType._EMT_2D)
                {
                    try
                    {
                        m_Camera.SetCameraScale(m_AutomationParams.MapViewport.CameraData.CameraScale);
                    }
                    catch (MapCoreException McEx)
                    {
                        HandleMapCoreExecption("SetCameraScale", McEx);
                    }
                }
                else
                {
                    try
                    {
                        AutoViewport.SetCameraClipDistances(m_AutomationParams.MapViewport.CameraData.CameraClipDistances.Min,
                                                            m_AutomationParams.MapViewport.CameraData.CameraClipDistances.Max,
                                                            m_AutomationParams.MapViewport.CameraData.CameraClipDistances.RenderInTwoSessions);
                    }
                    catch (MapCoreException McEx)
                    {
                        HandleMapCoreExecption("SetCameraClipDistances", McEx);
                    }

                    try
                    {
                        m_Camera.SetCameraFieldOfView(m_AutomationParams.MapViewport.CameraData.CameraFieldOfView);
                    }
                    catch (MapCoreException McEx)
                    {
                        HandleMapCoreExecption("SetCameraFieldOfView", McEx);

                    }
                }
                try
                {
                    float yaw, pitch, roll;
                    yaw = m_AutomationParams.MapViewport.CameraData.CameraOrientation.Yaw;
                    pitch = m_AutomationParams.MapViewport.CameraData.CameraOrientation.Pitch;
                    roll = m_AutomationParams.MapViewport.CameraData.CameraOrientation.Roll;

                    m_Camera.SetCameraOrientation(yaw, pitch, roll, false);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetCameraOrientation", McEx);
                }

                try
                {
                    AutoViewport.SetTransparencyOrderingMode(m_AutomationParams.MapViewport.TransparencyOrderingMode);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetTransparencyOrderingMode", McEx);
                }

                try
                {
                    AutoViewport.SetOneBitAlphaMode(m_AutomationParams.MapViewport.OneBitAlphaMode);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetOneBitAlphaMode", McEx);
                }

                try
                {
                    AutoViewport.SetShadowMode(m_AutomationParams.MapViewport.ShadowMode);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetShadowMode", McEx);
                }

                try
                {
                    if (m_AutomationParams.MapViewport.PostProcess != null)
                    {
                        foreach (string postProcessName in m_AutomationParams.MapViewport.PostProcess)
                            AutoViewport.AddPostProcess(postProcessName);
                    }
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("AddPostProcess", McEx);
                }

                try
                {
                    if (m_AutomationParams.MapViewport.DebugOptions != null && m_AutomationParams.MapViewport.DebugOptions.Count > 0)
                    {
                        foreach (KeyValuePair<uint, int> debugOption in m_AutomationParams.MapViewport.DebugOptions)
                            AutoViewport.SetDebugOption(debugOption.Key, debugOption.Value);
                    }
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetDebugOption", McEx);
                }

                try
                {
                    if (m_AutomationParams.MapViewport.MaterialSchemeName != null && m_AutomationParams.MapViewport.MaterialSchemeName != "")
                    {
                        AutoViewport.SetMaterialScheme(m_AutomationParams.MapViewport.MaterialSchemeName);
                    }
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetMaterialScheme", McEx);
                }

                try
                {
                    if (m_AutomationParams.MapViewport.MaterialSchemeDefinition != null && m_AutomationParams.MapViewport.MaterialSchemeDefinition.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> materialSchemeDefinition in m_AutomationParams.MapViewport.MaterialSchemeDefinition)
                            AutoViewport.SetMaterialSchemeDefinition(materialSchemeDefinition.Key, materialSchemeDefinition.Value);
                    }
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetMaterialSchemeDefinition", McEx);
                }

                try
                {
                    AutoViewport.SetDtmVisualization(m_AutomationParams.MapViewport.DtmVisualization.IsEnabled, m_AutomationParams.MapViewport.DtmVisualization.Params);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetDtmVisualization", McEx);

                }

                try
                {
                    if (m_AutomationParams.MapViewport.HeightLines.ScaleSteps.Count > 0)
                    {
                        IDNMcMapHeightLines mapHeightLines = DNMcMapHeightLines.Create(m_AutomationParams.MapViewport.HeightLines.ScaleSteps.ToArray(), m_AutomationParams.MapViewport.HeightLines.LineWidth);
                        mapHeightLines.SetColorInterpolationMode(m_AutomationParams.MapViewport.HeightLines.ColorInterpolationMode.IsEnabled,
                            m_AutomationParams.MapViewport.HeightLines.ColorInterpolationMode.MinHeight,
                            m_AutomationParams.MapViewport.HeightLines.ColorInterpolationMode.MaxHeight);
                        Manager_MCMapHeightLines.CreateMapHeightLines(mapHeightLines);

                        AutoViewport.SetHeightLines(mapHeightLines);
                        AutoViewport.SetHeightLinesVisibility(true);
                    }
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetHeightLines", McEx);

                }
                if (m_AutomationParams.MapViewport.MapGrid != null)
                {
                    try
                    {
                        AutoViewport.GridAboveVectorLayers = m_AutomationParams.MapViewport.MapGrid.GridAboveVectorLayers;
                    }
                    catch (MapCoreException McEx)
                    {
                        HandleMapCoreExecption("GridAboveVectorLayers", McEx);
                    }

                    try
                    {
                        AutoViewport.GridVisibility = m_AutomationParams.MapViewport.MapGrid.GridVisibility;
                    }
                    catch (MapCoreException McEx)
                    {
                        HandleMapCoreExecption("GridVisibility", McEx);

                    }

                    try
                    {
                        Dictionary<string, IDNMcLineItem> dicLineItemsNames = new Dictionary<string, IDNMcLineItem>();
                        Dictionary<string, IDNMcTextItem> dicTextItemsNames = new Dictionary<string, IDNMcTextItem>();

                        DNSGridRegion[] mcGridRegions = null;
                        if (m_AutomationParams.MapViewport.MapGrid.GridRegions != null && m_AutomationParams.MapViewport.MapGrid.GridRegions.Count > 0)
                        {
                            mcGridRegions = new DNSGridRegion[m_AutomationParams.MapViewport.MapGrid.GridRegions.Count];
                            int i = 0;
                            foreach (MCTGridRegion mctGridRegion in m_AutomationParams.MapViewport.MapGrid.GridRegions)
                            {
                                DNSGridRegion mcGridRegion = new DNSGridRegion();
                                mcGridRegion.pCoordinateSystem = CreateGridCoordinateSystem(mctGridRegion.pCoordinateSystem);
                                mcGridRegion.GeoLimit = mctGridRegion.GeoLimit;
                                mcGridRegion.uFirstScaleStepIndex = mctGridRegion.uFirstScaleStepIndex;
                                mcGridRegion.uLastScaleStepIndex = mctGridRegion.uLastScaleStepIndex;

                                if (mctGridRegion.pGridLine != null && mctGridRegion.pGridLine != "")
                                {
                                    if (dicLineItemsNames.ContainsKey(mctGridRegion.pGridLine))
                                    {
                                        mcGridRegion.pGridLine = dicLineItemsNames[mctGridRegion.pGridLine];
                                    }
                                    else
                                    {
                                        string filePath = m_FolderPath + "\\" + mctGridRegion.pGridLine;
                                        IDNMcObjectScheme[] objectSchemes = AutoViewport.OverlayManager.LoadObjectSchemes(filePath);
                                        if (objectSchemes != null && objectSchemes.Length > 0)
                                        {
                                            IDNMcObjectSchemeNode[] items = objectSchemes[0].GetNodes(DNENodeKindFlags._ENKF_SYMBOLIC_ITEM);
                                            if (items != null && items.Length > 0 && items[0] is IDNMcLineItem)
                                            {
                                                mcGridRegion.pGridLine = (IDNMcLineItem)items[0];
                                                dicLineItemsNames.Add(mctGridRegion.pGridLine, mcGridRegion.pGridLine);
                                            }

                                            objectSchemes[0].Dispose();
                                        }
                                    }
                                }
                                if (mctGridRegion.pGridText != null && mctGridRegion.pGridText != "")
                                {
                                    if (dicTextItemsNames.ContainsKey(mctGridRegion.pGridText))
                                    {
                                        mcGridRegion.pGridText = dicTextItemsNames[mctGridRegion.pGridText];
                                    }
                                    else
                                    {
                                        string filePath = m_FolderPath + "\\" + mctGridRegion.pGridText;
                                        IDNMcObjectScheme[] objectSchemes = AutoViewport.OverlayManager.LoadObjectSchemes(filePath);
                                        if (objectSchemes != null && objectSchemes.Length > 0)
                                        {
                                            IDNMcObjectSchemeNode[] items = objectSchemes[0].GetNodes(DNENodeKindFlags._ENKF_SYMBOLIC_ITEM);
                                            if (items != null && items.Length > 0 && items[0] is IDNMcTextItem)
                                            {
                                                mcGridRegion.pGridText = (IDNMcTextItem)items[0];
                                                dicTextItemsNames.Add(mctGridRegion.pGridText, mcGridRegion.pGridText);
                                            }

                                            objectSchemes[0].Dispose();
                                        }
                                    }
                                }

                                mcGridRegions[i] = mcGridRegion;
                                i++;
                            }
                        }
                        DNSScaleStep[] mcScaleSteps = null;
                        if (m_AutomationParams.MapViewport.MapGrid.ScaleSteps != null && m_AutomationParams.MapViewport.MapGrid.ScaleSteps.Count > 0)
                        {
                            mcScaleSteps = m_AutomationParams.MapViewport.MapGrid.ScaleSteps.ToArray();
                        }
                        if (mcGridRegions != null && mcScaleSteps != null)
                        {
                            AutoViewport.Grid = DNMcMapGrid.Create(mcGridRegions, mcScaleSteps, m_AutomationParams.MapViewport.MapGrid.IsUseBasicItemPropertiesOnly);
                            if (mcGridRegions.Length > 0)
                            {
                                DNSGridRegion gridRegion = mcGridRegions[0];
                                if (gridRegion.pCoordinateSystem != null)
                                {
                                    uint chosenGrid = 0;
                                    switch (gridRegion.pCoordinateSystem.GetGridCoorSysType())
                                    {
                                        case DNEGridCoordSystemType._EGCS_UTM:
                                            chosenGrid = (int)GridTypes.UTMGrid; break;
                                        case DNEGridCoordSystemType._EGCS_GEOGRAPHIC:
                                            chosenGrid = (int)GridTypes.GeoGrid; break;
                                        case DNEGridCoordSystemType._EGCS_MGRS:
                                            chosenGrid = (int)GridTypes.MGRSGrid; break;
                                        case DNEGridCoordSystemType._EGCS_NZMG:
                                            chosenGrid = (int)GridTypes.NZMGGrid; break;
                                        case DNEGridCoordSystemType._EGCS_GEOREF:
                                            chosenGrid = (int)GridTypes.GEOREFGrid; break;
                                    }
                                    Manager_MCGrid.AddNewMapGrid(AutoViewport.Grid, chosenGrid);
                                }
                            }

                        }

                    }
                    catch (MapCoreException McEx)
                    {
                        HandleMapCoreExecption("MapGrid.Create", McEx);

                    }
                }

                if (m_AutomationParams.MapViewport.ViewportImageProccessingData != null)
                    SetImageProcessingData(AutoViewport, null, m_AutomationParams.MapViewport.ViewportImageProccessingData);

                if (m_AutomationParams.MapViewport.ImageProccessingData != null)
                {
                    Dictionary<KeyValuePair<int, int>, MCTImageProcessingData> imageProcessing = m_AutomationParams.MapViewport.ImageProccessingData;
                    IDNMcMapTerrain[] mapTerrains = AutoViewport.GetTerrains();
                    for (int i = 0; i < mapTerrains.Length; i++)
                    {
                        IDNMcMapLayer[] mapLayers = mapTerrains[i].GetLayers();
                        for (int j = 0; j < mapLayers.Length; j++)
                        {
                            if (mapLayers[j] is IDNMcRasterMapLayer)
                            {
                                KeyValuePair<int, int> keyValuePair = new KeyValuePair<int, int>(i, j);
                                if (imageProcessing.ContainsKey(keyValuePair))
                                    SetImageProcessingData(AutoViewport, mapLayers[j] as IDNMcRasterMapLayer, imageProcessing[keyValuePair]);
                            }
                        }
                    }
                }
                try
                {
                    m_AutoTimeUntilRender.Stop();
                    if(MainForm.m_AutoStreamWriter != null)
                        MainForm.m_AutoStreamWriter.WriteLine("auto render time: from start until render - " + m_AutoTimeUntilRender.ElapsedMilliseconds);
                }
                catch (Exception) { }
                MainForm.m_AutoTimeRender.Start();
                try
                {
                    m_arrTerrain = null;
                    AutoViewport.Render();
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("AutoViewport Render", McEx);
                }
               MainForm.m_isNeedCheckAutoViewport = true;
            }
        }

        public static void SaveViewportSession(IDNMcMapViewport mapViewport)
        {
            StreamWriter stw = null;

            frmSaveSessionToFolderDef frmSaveSessionToFolderDef = new frmSaveSessionToFolderDef();
            DNERenderingSystem RenderingSystem = MCTMapDevice.CurrDevice.RenderingSystem;

            if (frmSaveSessionToFolderDef.ShowDialog() == DialogResult.OK)
            {
                string destDir = frmSaveSessionToFolderDef.DestFolder;

                IDNMcOverlayManager mapOM = mapViewport.OverlayManager;

                MCTAutomationParams automationParams = new MCTAutomationParams();
                automationParams.MapDevice = MCTMapDevice.CurrDevice;

                uint widthDimension = 0, heightDimension = 0;

                automationParams.MapsBaseDirectory.Windows = frmSaveSessionToFolderDef.WindowsMapsBaseDir;
                automationParams.MapsBaseDirectory.Android = frmSaveSessionToFolderDef.AndroidMapsBaseDir;
                automationParams.MapsBaseDirectory.Web = frmSaveSessionToFolderDef.WebMapsBaseDir;
                automationParams.UserComment = frmSaveSessionToFolderDef.UserComment;

                try
                {
                    stw = new StreamWriter(destDir + @"\ViewportDataParams.txt");
                    stw.WriteLine("Viewport ID: " + mapViewport.ViewportID.ToString());
                    automationParams.MapViewport.ViewportID = mapViewport.ViewportID;

                    mapViewport.GetViewportSize(out widthDimension, out heightDimension);
                    automationParams.MapViewport.ViewportSize.ViewportWidth = widthDimension;
                    automationParams.MapViewport.ViewportSize.ViewportHeight = heightDimension;
                }
                catch (Exception McEx)
                {
                    if (McEx is MapCoreException)
                        Utilities.ShowErrorMessage("ViewportID - Save viewport was failed", McEx);
                    else
                        MessageBox.Show("Error open/write to file: " + McEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile(stw);
                    return;
                }

                try
                {
                    stw.WriteLine();
                    stw.WriteLine("Viewport Map Type: " + mapViewport.MapType.ToString());
                    automationParams.MapViewport.ViewportMapType = mapViewport.MapType.ToString();
                    automationParams.MapViewport.ViewportShowGeoInMetricProportion = Managers.Manager_MCGeneralDefinitions.m_ShowGeoInMetricProportion;
                }
                catch (Exception McEx)
                {
                    if (McEx is MapCoreException)
                        Utilities.ShowErrorMessage("MapType - Save viewport was failed", McEx);
                    else
                        MessageBox.Show("Error open/write to file: " + McEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile(stw);
                    return;
                }

                if (mapViewport.CoordinateSystem != null)
                {
                    try
                    {
                        MCTGridCoordinateSystem mctGridCoordinateSystem = CreateMCTGridCoordinateSystem(mapViewport.CoordinateSystem);
                        stw.WriteLine();
                        stw.WriteLine("Viewport Coordinate System:");

                        stw.WriteLine("Grid Coordinate System: " + mctGridCoordinateSystem.GridCoordinateSystemType);
                        stw.WriteLine("Datum: " + mctGridCoordinateSystem.Datum);
                        stw.WriteLine("Zone: " + mctGridCoordinateSystem.Zone);
                        stw.WriteLine("EpsgCode: " + mctGridCoordinateSystem.EpsgCode);

                        automationParams.MapViewport.GridCoordinateSystem = mctGridCoordinateSystem;

                    }
                    catch (Exception McEx)
                    {
                        if (McEx is MapCoreException)
                            Utilities.ShowErrorMessage("Viewport - GetGridCoorSysType - Save viewport was failed", McEx);
                        else
                            MessageBox.Show("Error open/write to file: " + McEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CloseFile(stw);
                        return;
                    }
                }
                try
                {
                    stw.WriteLine();
                    IDNMcMapTerrain[] terrainArr = mapViewport.GetTerrains();
                    stw.WriteLine("Terrains Paths: " + terrainArr.Length.ToString());
                    automationParams.MapViewport.Terrains = new List<string>();
                    automationParams.MapViewport.MapTerrainsData = new Dictionary<int, MCTMapTerrainData>();
                    for (int i = 0; i < terrainArr.Length; i++)
                    {
                        terrainArr[i].Save(destDir + @"\Terrain" + i.ToString(), frmSaveSessionToFolderDef.WindowsMapsBaseDir, true);
                        automationParams.MapViewport.Terrains.Add("Terrain" + i.ToString());
                        stw.WriteLine(@"Terrain" + i.ToString());

                        sbyte DrawPriority = mapViewport.GetTerrainDrawPriority(terrainArr[i]);
                        uint tiles = mapViewport.GetTerrainNumCacheTiles(terrainArr[i], false);
                        uint tiles2 = mapViewport.GetTerrainNumCacheTiles(terrainArr[i], true);

                        MCTMapTerrainData mapTerrainData = new MCTMapTerrainData();
                        mapTerrainData.DrawPriority = DrawPriority;
                        mapTerrainData.NumCacheTiles = tiles;
                        mapTerrainData.NumCacheTilesForStaticObjects = tiles2;

                        automationParams.MapViewport.MapTerrainsData.Add(i, mapTerrainData);
                    }
                }
                catch (Exception McEx)
                {
                    if (McEx is MapCoreException)
                        Utilities.ShowErrorMessage("Save Terrain - Save viewport was failed", McEx);
                    else
                        MessageBox.Show("Error open/write to file: " + McEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile(stw);
                    return;
                }
                try
                {
                    stw.WriteLine();
                    IDNMcMapLayer[] querySecondaryDtmLayersArr = mapViewport.GetQuerySecondaryDtmLayers();
                    if (querySecondaryDtmLayersArr != null && querySecondaryDtmLayersArr.Length > 0)
                    {
                        stw.WriteLine("Query Secondary Dtm Layers Paths: " + querySecondaryDtmLayersArr.Length.ToString());
                        automationParams.MapViewport.QuerySecondaryDTMLayers = new List<string>();
                        for (int i = 0; i < querySecondaryDtmLayersArr.Length; i++)
                        {
                            querySecondaryDtmLayersArr[i].Save(destDir + @"\QuerySecondaryDtmLayer" + i.ToString(), frmSaveSessionToFolderDef.WindowsMapsBaseDir, true);
                            automationParams.MapViewport.QuerySecondaryDTMLayers.Add("QuerySecondaryDtmLayer" + i.ToString());
                            stw.WriteLine(@"QuerySecondaryDtmLayer" + i.ToString());
                        }
                    }
                }
                catch (Exception McEx)
                {
                    if (McEx is MapCoreException)
                        Utilities.ShowErrorMessage("Save QuerySecondaryDTMLayers - Save viewport was failed", McEx);
                    else
                        MessageBox.Show("Error open/write to file: " + McEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile(stw);
                    return;
                }
                if (mapOM != null)
                {
                    try
                    {
                        MCTGridCoordinateSystem mctGridCoordinateSystem = CreateMCTGridCoordinateSystem(mapOM.GetCoordinateSystemDefinition());

                        stw.WriteLine();
                        stw.WriteLine("Overlay Manager Coordinate System:");
                        stw.WriteLine("Grid Coordinate System: " + mctGridCoordinateSystem.GridCoordinateSystemType);
                        stw.WriteLine("Datum: " + mctGridCoordinateSystem.Datum);
                        stw.WriteLine("Zone: " + mctGridCoordinateSystem.Zone);
                        stw.WriteLine("EpsgCode: " + mctGridCoordinateSystem.EpsgCode);

                        automationParams.OverlayManager.GridCoordinateSystem = mctGridCoordinateSystem;
                    }
                    catch (Exception McEx)
                    {
                        if (McEx is MapCoreException)
                            Utilities.ShowErrorMessage("Overlay Manager - GetGridCoorSysType - Save viewport was failed", McEx);
                        else
                            MessageBox.Show("Error open/write to file: " + McEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CloseFile(stw);
                        return;
                    }


                    try
                    {
                        stw.WriteLine();
                        IDNMcOverlay[] overlaysArr = mapOM.GetOverlays();
                        stw.WriteLine("Overlays Paths: " + overlaysArr.Length.ToString());
                        automationParams.OverlayManager.Overlays = new List<string>();
                        for (int i = 0; i < overlaysArr.Length; i++)
                        {
                            string filename = "Overlay" + i.ToString() + ".mcobj.json";
                            string path = destDir + "\\" + filename;
                            overlaysArr[i].SaveAllObjects(path, DNEStorageFormat._ESF_JSON);
                            stw.WriteLine(filename);
                            automationParams.OverlayManager.Overlays.Add(filename);
                        }
                    }
                    catch (Exception McEx)
                    {
                        if (McEx is MapCoreException)
                            Utilities.ShowErrorMessage("Save Overlay - Save viewport was failed", McEx);
                        else
                            MessageBox.Show("Error open/write to file: " + McEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CloseFile(stw);
                        return;
                    }
                }
                else
                {
                    automationParams.OverlayManager = null;
                }
                try
                {
                    if (mapViewport is DNMcSectionMapViewport)
                    {
                        automationParams.MapViewport.IsSectionMapViewport = true;

                        DNSMcVector3D[] routePoints;
                        DNMcNullableOut<DNSMcVector3D[]> heightPoints = new DNMcNullableOut<DNSMcVector3D[]>();

                        ((IDNMcSectionMapViewport)mapViewport).GetSectionRoutePoints(out routePoints, heightPoints);
                        
                        automationParams.MapViewport.SectionMapViewportData = new MCTSectionMapViewport();
                        automationParams.MapViewport.SectionMapViewportData.SectionRoutePoints = routePoints;
                        automationParams.MapViewport.SectionMapViewportData.SectionHeightPoints = heightPoints;

                    }
                    else
                        automationParams.MapViewport.IsSectionMapViewport = false;
                }
                catch (Exception McEx)
                {
                    if (McEx is MapCoreException)
                        Utilities.ShowErrorMessage("Save section viewport - Save viewport was failed", McEx);
                    else
                        MessageBox.Show("Error open/write to file: " + McEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile(stw);
                    return;
                }

                try
                {
                    automationParams.MapViewport.ViewportBackgroundColor = mapViewport.GetBackgroundColor();
                }
                catch (Exception McEx)
                {
                    if (McEx is MapCoreException)
                        Utilities.ShowErrorMessage("Get Background Color - Save viewport was failed", McEx);
                    else
                        MessageBox.Show("Error open/write to file: " + McEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile(stw);
                    return;
                }
                try
                {
                    stw.WriteLine();
                    stw.WriteLine("Camera Data:");
                    stw.WriteLine("Position X: " + mapViewport.ActiveCamera.GetCameraPosition().x.ToString());
                    stw.WriteLine("Position Y: " + mapViewport.ActiveCamera.GetCameraPosition().y.ToString());
                    stw.WriteLine("Position Z: " + mapViewport.ActiveCamera.GetCameraPosition().z.ToString());

                    automationParams.MapViewport.CameraData.CameraPosition.X = mapViewport.ActiveCamera.GetCameraPosition().x;
                    automationParams.MapViewport.CameraData.CameraPosition.Y = mapViewport.ActiveCamera.GetCameraPosition().y;
                    automationParams.MapViewport.CameraData.CameraPosition.Z = mapViewport.ActiveCamera.GetCameraPosition().z;
                }
                catch (Exception McEx)
                {
                    if (McEx is MapCoreException)
                        Utilities.ShowErrorMessage("GetCameraPosition - Save viewport was failed", McEx);
                    else
                        MessageBox.Show("Error open/write to file: " + McEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile(stw);
                    return;
                }

                try
                {
                    float yaw, pitch, roll;
                    mapViewport.ActiveCamera.GetCameraOrientation(out yaw, out pitch, out roll);
                    stw.WriteLine("Yaw: " + yaw.ToString());
                    stw.WriteLine("Pitch: " + pitch.ToString());
                    stw.WriteLine("Roll: " + roll.ToString());

                    automationParams.MapViewport.CameraData.CameraOrientation.Yaw = yaw;
                    automationParams.MapViewport.CameraData.CameraOrientation.Pitch = pitch;
                    automationParams.MapViewport.CameraData.CameraOrientation.Roll = roll;

                }
                catch (Exception McEx)
                {
                    if (McEx is MapCoreException)
                        Utilities.ShowErrorMessage("GetCameraOrientation - Save viewport was failed", McEx);
                    else
                        MessageBox.Show("Error open/write to file: " + McEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile(stw);
                    return;
                }

                try
                {
                    automationParams.MapViewport.TransparencyOrderingMode = mapViewport.GetTransparencyOrderingMode();
                    automationParams.MapViewport.ShadowMode = mapViewport.GetShadowMode();
                    automationParams.MapViewport.OneBitAlphaMode = mapViewport.GetOneBitAlphaMode();
                }
                catch (Exception McEx)
                {
                    if (McEx is MapCoreException)
                        Utilities.ShowErrorMessage("GetTransparencyOrderingMode - Save viewport was failed", McEx);
                    else
                        MessageBox.Show("Error open/write to file: " + McEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile(stw);
                    return;
                }

                if (mapViewport.MapType == DNEMapType._EMT_2D)
                {
                    try
                    {
                        automationParams.MapViewport.CameraData.CameraScale = mapViewport.ActiveCamera.GetCameraScale();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetCameraScale", McEx);

                        MessageBox.Show("Save viewport was failed", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CloseFile(stw);
                        return;
                    }



                }
                else
                {
                    try
                    {
                        float minDistances, maxDistances;
                        bool twoSessionsRender;
                        mapViewport.GetCameraClipDistances(out minDistances, out maxDistances, out twoSessionsRender);

                        automationParams.MapViewport.CameraData.CameraClipDistances.Min = minDistances;
                        automationParams.MapViewport.CameraData.CameraClipDistances.Max = maxDistances;
                        automationParams.MapViewport.CameraData.CameraClipDistances.RenderInTwoSessions = twoSessionsRender;
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetCameraClipDistances", McEx);

                        MessageBox.Show("Save viewport was failed", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CloseFile(stw);
                        return;
                    }

                    try
                    {
                        automationParams.MapViewport.CameraData.CameraFieldOfView = mapViewport.ActiveCamera.GetCameraFieldOfView();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetCameraFieldOfView", McEx);

                        MessageBox.Show("Save viewport was failed", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CloseFile(stw);
                        return;
                    }
                }
                try
                {
                    bool isEnable;
                    DNSDtmVisualizationParams DtmVisualizationParams;
                    mapViewport.GetDtmVisualization(out isEnable, out DtmVisualizationParams);

                    automationParams.MapViewport.DtmVisualization.Params = DtmVisualizationParams;
                    automationParams.MapViewport.DtmVisualization.IsEnabled = isEnable;

                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetDtmVisualization", McEx);
                    MessageBox.Show("Save viewport was failed", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile(stw);
                    return;
                }

                try
                {
                    IDNMcMapHeightLines mapHeightLines = mapViewport.GetHeightLines();
                    float lineWidth = 0;
                    bool isEnabled = false;
                    float min, max;
                    if (mapHeightLines != null)
                    {
                        DNSHeightLinesScaleStep[] linesScaleSteps = mapHeightLines.GetScaleSteps();
                        if (linesScaleSteps != null)
                        {
                            automationParams.MapViewport.HeightLines.ScaleSteps.AddRange(linesScaleSteps);

                            mapHeightLines.GetLineWidth(out lineWidth);
                            automationParams.MapViewport.HeightLines.LineWidth = lineWidth;

                            mapHeightLines.GetColorInterpolationMode(out isEnabled, out min, out max);
                            automationParams.MapViewport.HeightLines.ColorInterpolationMode.IsEnabled = isEnabled;
                            automationParams.MapViewport.HeightLines.ColorInterpolationMode.MinHeight = min;
                            automationParams.MapViewport.HeightLines.ColorInterpolationMode.MaxHeight = max;
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetHeightLines()", McEx);
                    MessageBox.Show("Save viewport was failed", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile(stw);
                    return;
                }

                try
                {
                    IDNMcMapGrid mapGrid = mapViewport.Grid;
                    MCTMapGrid mctMapGrid = new MCTMapGrid();

                    mctMapGrid.GridAboveVectorLayers = mapViewport.GridAboveVectorLayers;
                    mctMapGrid.GridVisibility = mapViewport.GridVisibility;

                    if (mapGrid != null)
                    {
                        mctMapGrid.IsUseBasicItemPropertiesOnly = mapGrid.IsUsingBasicItemPropertiesOnly();

                        DNSScaleStep[] scaleSteps = mapGrid.GetScaleSteps();
                        if (scaleSteps != null)
                            mctMapGrid.ScaleSteps = new List<DNSScaleStep>(scaleSteps);

                        Dictionary<IDNMcLineItem, string> lstLineItemSchemeName = new Dictionary<IDNMcLineItem, string>();
                        Dictionary<IDNMcTextItem, string> lstTextItemSchemeName = new Dictionary<IDNMcTextItem, string>();

                        DNSGridRegion[] gridRegions = mapGrid.GetGridRegions();
                        if (gridRegions != null)
                        {
                            List<MCTGridRegion> lstGridRegions = new List<MCTGridRegion>();

                            int index_line = 0;
                            int index_text = 0;
                            foreach (DNSGridRegion gridRegion in gridRegions)
                            {
                                MCTGridRegion mctGridRegion = new MCTGridRegion();

                                mctGridRegion.pCoordinateSystem = CreateMCTGridCoordinateSystem(gridRegion.pCoordinateSystem);
                                mctGridRegion.GeoLimit = gridRegion.GeoLimit;
                                mctGridRegion.uFirstScaleStepIndex = gridRegion.uFirstScaleStepIndex;
                                mctGridRegion.uLastScaleStepIndex = gridRegion.uLastScaleStepIndex;

                                if (gridRegion.pGridLine != null)
                                {
                                    if (lstLineItemSchemeName.ContainsKey(gridRegion.pGridLine))
                                    {
                                        mctGridRegion.pGridLine = lstLineItemSchemeName[gridRegion.pGridLine];
                                    }
                                    else
                                    {
                                        IDNMcObjectScheme mcObjectScheme = DNMcObjectScheme.Create(mapViewport.OverlayManager, gridRegion.pGridLine, gridRegion.pGridLine.GetGeometryCoordinateSystem());
                                        IDNMcObjectScheme[] mcObjectSchemes = new IDNMcObjectScheme[1] { mcObjectScheme };
                                        string fileName = "GridLineItem" + index_line.ToString() + ".json";
                                        string fullPath = destDir + "\\" + fileName;
                                        mapViewport.OverlayManager.SaveObjectSchemes(mcObjectSchemes, fullPath, DNEStorageFormat._ESF_JSON);
                                        lstLineItemSchemeName.Add(gridRegion.pGridLine, fileName);
                                        mcObjectScheme.Dispose();
                                        mctGridRegion.pGridLine = fileName;
                                        index_line++;
                                    }
                                }
                                if (gridRegion.pGridText != null)
                                {
                                    if (lstTextItemSchemeName.ContainsKey(gridRegion.pGridText))
                                        mctGridRegion.pGridText = lstTextItemSchemeName[gridRegion.pGridText];
                                    else
                                    {
                                        IDNMcObjectScheme mcObjectScheme = DNMcObjectScheme.Create(mapViewport.OverlayManager, gridRegion.pGridText, gridRegion.pGridText.GetGeometryCoordinateSystem());
                                        IDNMcObjectScheme[] mcObjectSchemes = new IDNMcObjectScheme[1] { mcObjectScheme };
                                        string fileName = "GridTextItem" + index_text.ToString() + ".json";
                                        string fullPath = destDir + "\\" + fileName;
                                        mapViewport.OverlayManager.SaveObjectSchemes(mcObjectSchemes, fullPath, DNEStorageFormat._ESF_JSON);
                                        lstTextItemSchemeName.Add(gridRegion.pGridText, fileName);
                                        mcObjectScheme.Dispose();
                                        mctGridRegion.pGridText = fileName;
                                        index_text++;
                                    }
                                }

                                lstGridRegions.Add(mctGridRegion);
                            }
                            mctMapGrid.GridRegions = lstGridRegions;
                        }
                    }
                    automationParams.MapViewport.MapGrid = mctMapGrid;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Grid", McEx);

                    MessageBox.Show("Save viewport was failed", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile(stw);
                    return;
                }


                try
                {
                    automationParams.MapViewport.ViewportImageProccessingData = GetImageProcessingData(mapViewport, null);

                    Dictionary<KeyValuePair<int, int>, MCTImageProcessingData> ImageProccessingData = new Dictionary<KeyValuePair<int, int>, MCTImageProcessingData>();
                    IDNMcMapTerrain[] mapTerrains = mapViewport.GetTerrains();
                    for (int i = 0; i < mapTerrains.Length; i++)
                    {
                        IDNMcMapLayer[] mapLayers = mapTerrains[i].GetLayers();
                        for (int j = 0; j < mapLayers.Length; j++)
                        {
                            if (mapLayers[j] is IDNMcRasterMapLayer)
                            {
                                KeyValuePair<int, int> keyValuePair = new KeyValuePair<int, int>(i, j);
                                ImageProccessingData.Add(keyValuePair, GetImageProcessingData(mapViewport, mapLayers[j] as IDNMcRasterMapLayer));
                            }
                        }
                    }

                    automationParams.MapViewport.ImageProccessingData = ImageProccessingData;

                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetImageProcessingData()", McEx);

                    MessageBox.Show("Save viewport was failed", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile(stw);
                    return;
                }

                try
                {
                    // automationParams.MapViewport.PostProcess.Add("BnW");
                    automationParams.MapViewport.MaterialSchemeName = "";

                    // automationParams.MapViewport.DebugOptions.Add(new KeyValuePair<uint, int>(24, 1));
                    automationParams.Save(destDir + @"\FullViewportDataParams.json");

                    automationParams.MapDevice.RenderingSystem = DNERenderingSystem._ERS_DIRECT_3D_9;
                    automationParams.Save(destDir + @"\FullViewportDataParams_DX9.json");

                    automationParams.MapDevice.RenderingSystem = DNERenderingSystem._ERS_DIRECT_3D_9EX;
                    automationParams.Save(destDir + @"\FullViewportDataParams_DX9X.json");

                    automationParams.MapDevice.RenderingSystem = DNERenderingSystem._ERS_OPEN_GL;
                    automationParams.Save(destDir + @"\FullViewportDataParams_GL.json");

                    StreamReader streamReader = new StreamReader(destDir + @"\FullViewportDataParams.json");
                    string file1 = streamReader.ReadToEnd();
                    int index = file1.IndexOf("RenderingSystemText");
                    int index1 = file1.IndexOf(':', index);
                    int index2 = file1.IndexOf('"', index1);
                    int index3 = file1.IndexOf('"', index2 + 1);
                    string renderSys = file1.Substring(index2 + 1, index3 - index2 - 1);

                    file1 = file1.Replace(renderSys, "_ERS_DIRECT_3D_11");

                    StreamWriter writer1 = new StreamWriter(destDir + @"\FullViewportDataParams_DX11.json");
                    writer1.Write(file1);
                    writer1.Close();
                    file1 = file1.Replace("_ERS_DIRECT_3D_11", "_ERS_OPEN_GL_3PLUS");
                    StreamWriter writer2 = new StreamWriter(destDir + @"\FullViewportDataParams_GL3Plus.json");
                    writer2.Write(file1);
                    writer2.Close();

                    file1 = file1.Replace("_ERS_OPEN_GL_3PLUS", "_ERS_OPEN_GLES");
                    StreamWriter writer3 = new StreamWriter(destDir + @"\FullViewportDataParams_GLES.json");
                    writer3.Write(file1);
                    writer3.Close();
                    streamReader.Close();

                    // Hack to support running GLES from main branch, IMcMapDevice does not have _ERS_OPEN_GLES
                    file1 = file1.Replace("_ERS_OPEN_GLES", "_ERS_AUTO_SELECT");
                    StreamWriter writer4 = new StreamWriter(destDir + @"\FullViewportDataParams_GLES_Master.json");
                    writer4.Write(file1);
                    writer4.Close();
                    streamReader.Close();


                    DNEPixelFormat viewportPixelFormat = DNEPixelFormat._EPF_A8R8G8B8;
                    try
                    {
                        mapViewport.GetRenderToBufferNativePixelFormat(out viewportPixelFormat);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetRenderToBufferNativePixelFormat", McEx);
                        CloseFile(stw);
                        return;
                    }
                    if (frmSaveSessionToFolderDef.ImgFileName != null && frmSaveSessionToFolderDef.ImgFileName != "")
                    {
                        Manager_MCViewports.RenderScreenRectToBuffer(mapViewport,
                            viewportPixelFormat,
                            0,
                            widthDimension,
                            heightDimension,
                            new Point(0, 0),
                            new Point((int)widthDimension, (int)heightDimension),
                            frmSaveSessionToFolderDef.DestFolder,
                            frmSaveSessionToFolderDef.DestFolder + "\\" + frmSaveSessionToFolderDef.ImgFileName,
                            true);
                    }
                    try
                    {
                        stw.Close();
                    }
                    catch (EncoderFallbackException ex)
                    {
                        MessageBox.Show("Error close file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception McEx)
                {
                    if (McEx is MapCoreException)
                        Utilities.ShowErrorMessage("Save To File - Save viewport was failed", McEx);
                    else
                        MessageBox.Show("Error open/write to file: " + McEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile(stw);
                }
            }

            MCTMapDevice.CurrDevice.RenderingSystem = RenderingSystem;
        }

        public static MCTGridCoordinateSystem CreateMCTGridCoordinateSystem(IDNMcGridCoordinateSystem gridCoordinateSystem)
        {
            MCTGridCoordinateSystem currGridCoorSys = new MCTGridCoordinateSystem();
            currGridCoorSys.GridCoordinateSystemType = gridCoordinateSystem.GetGridCoorSysType().ToString();

            if (gridCoordinateSystem.GetGridCoorSysType() != DNEGridCoordSystemType._EGCS_GENERIC_GRID)
            {
                currGridCoorSys.Datum = gridCoordinateSystem.GetDatum().ToString();
                currGridCoorSys.Zone = gridCoordinateSystem.GetZone();
            }
            else if (gridCoordinateSystem is IDNMcGridGeneric)
            {
                IDNMcGridGeneric mcGridGeneric = (IDNMcGridGeneric)gridCoordinateSystem;
                string[] pastrCreateParams;
                bool isSRID;
                mcGridGeneric.GetCreateParams(out pastrCreateParams, out isSRID);
                if (pastrCreateParams != null && pastrCreateParams.Length > 0)
                {
                    if (isSRID)
                        currGridCoorSys.EpsgCode = pastrCreateParams[0];
                    else
                    {
                        if (pastrCreateParams.Length == 1)
                            currGridCoorSys.FullInitialization = pastrCreateParams[0];
                        else
                            currGridCoorSys.GridParameters = pastrCreateParams;
                    }
                }
            }
            return currGridCoorSys;
        }

        private static MCTImageProcessingData GetImageProcessingData(IDNMcMapViewport mapViewport, IDNMcRasterMapLayer layer)
        {
            MCTImageProcessingData imageProcessingData = new MCTImageProcessingData();

            try
            {
                DNEFilterProccessingOperation filter = mapViewport.GetFilterImageProcessing(layer);
                imageProcessingData.Filter = filter;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetFilterImageProcessing", McEx);
            }

            try
            {
                uint puFilterXsize;
                uint puFilterYsize;
                float[] ppaFilter;
                float pfBias;
                float pfDivider;

                mapViewport.GetCustomFilter(layer, out puFilterXsize, out puFilterYsize, out ppaFilter, out pfBias, out pfDivider);

                imageProcessingData.CustomFilter = new MCTImageProcessingCustomFilter();
                imageProcessingData.CustomFilter.Bias = pfBias;
                imageProcessingData.CustomFilter.Divider = pfDivider;
                imageProcessingData.CustomFilter.FilterXsize = puFilterXsize;
                imageProcessingData.CustomFilter.FilterYsize = puFilterYsize;
                imageProcessingData.CustomFilter.Filters = ppaFilter;

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCustomFilter", McEx);
            }

            try
            {
                byte r, g, b;
                mapViewport.GetWhiteBalanceBrightness(layer, out r, out g, out b);

                imageProcessingData.WhiteBalanceBrightnessR = r;
                imageProcessingData.WhiteBalanceBrightnessG = g;
                imageProcessingData.WhiteBalanceBrightnessB = b;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetWhiteBalanceBrightness", McEx);
            }

            try
            {
                bool colorTableProcessing = mapViewport.GetEnableColorTableImageProcessing(layer);
                imageProcessingData.IsEnableColorTableImageProcessing = colorTableProcessing;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetEnableColorTableImageProcessing", McEx);
            }

            int ChannelCount = 4;
            DNEColorChannel[] colorChannels = new DNEColorChannel[ChannelCount];
            colorChannels[0] = DNEColorChannel._ECC_RED;
            colorChannels[1] = DNEColorChannel._ECC_GREEN;
            colorChannels[2] = DNEColorChannel._ECC_BLUE;
            colorChannels[3] = DNEColorChannel._ECC_MULTI_CHANNEL;

            for (int i = 0; i < ChannelCount; i++)
            {
                DNEColorChannel Channel = colorChannels[i];
                imageProcessingData.ChannelDatas[i] = new MCTImageProcessingChannelData();
                imageProcessingData.ChannelDatas[i].Channel = Channel;
                try
                {
                    byte[] colorValues;
                    bool bUse = mapViewport.GetUserColorValues(layer, Channel, out colorValues);

                    imageProcessingData.ChannelDatas[i].UserColorValues = colorValues;
                    imageProcessingData.ChannelDatas[i].UserColorValuesUse = bUse;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetUserColorValues", McEx);
                }

                try
                {
                    double brightness = mapViewport.GetColorTableBrightness(layer, Channel);

                    imageProcessingData.ChannelDatas[i].Brightness = brightness;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetColorTableBrightness", McEx);
                }

                try
                {
                    double contrast = mapViewport.GetContrast(layer, Channel);

                    imageProcessingData.ChannelDatas[i].Contrast = contrast;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetContrast", McEx);
                }

                try
                {
                    bool bUse = mapViewport.GetNegative(layer, Channel);

                    imageProcessingData.ChannelDatas[i].Negative = bUse;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetNegative", McEx);
                }


                try
                {
                    double gamma = mapViewport.GetGamma(layer, Channel);

                    imageProcessingData.ChannelDatas[i].Gamma = gamma;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetGamma", McEx);
                }
                try
                {
                    bool isSet = mapViewport.IsOriginalHistogramSet(layer, Channel);
                    imageProcessingData.ChannelDatas[i].IsOriginalHistogramSet = isSet;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("IsOriginalHistogramSet", McEx);
                }

                try
                {
                    bool bUse = mapViewport.GetHistogramEqualization(layer, Channel);

                    imageProcessingData.ChannelDatas[i].HistogramEqualization = bUse;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetHistogramEqualization", McEx);
                }

                try
                {
                    Int64[] referenceHistogram;
                    bool bUse = mapViewport.GetHistogramFit(layer, Channel, out referenceHistogram);

                    imageProcessingData.ChannelDatas[i].ReferenceHistogram = referenceHistogram;
                    imageProcessingData.ChannelDatas[i].ReferenceHistogramUse = bUse;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetHistogramFit", McEx);
                }

                if (imageProcessingData.ChannelDatas[i].IsOriginalHistogramSet)
                {
                    try
                    {
                        Int64[] histogram = mapViewport.GetOriginalHistogram(layer, Channel);

                        imageProcessingData.ChannelDatas[i].OriginalHistogram = histogram;
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetOriginalHistogram", McEx);
                    }
                }

                try
                {
                    bool bUse;
                    double mean, stdev;
                    mapViewport.GetHistogramNormalization(layer, Channel, out bUse, out mean, out stdev);

                    imageProcessingData.ChannelDatas[i].HistogramNormalizationUse = bUse;
                    imageProcessingData.ChannelDatas[i].HistogramNormalizationMean = mean;
                    imageProcessingData.ChannelDatas[i].HistogramNormalizationStdev = stdev;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetHistogramNormalization", McEx);
                }

                try
                {
                    bool bUse = mapViewport.GetVisibleAreaOriginalHistogram(layer, Channel);

                    imageProcessingData.ChannelDatas[i].VisibleAreaOriginalHistogram = bUse;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetVisibleAreaOriginalHistogram", McEx);
                }
            }
            return imageProcessingData;
        }


        private static void CloseFile(StreamWriter stw)
        {
            if (stw != null)
                stw.Close();
        }


        private static bool GetDatum(string datum, out DNEDatumType datumType)
        {
            if (datum != null && datum != "")
            {
                datumType = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), datum);
                return true;
            }
            else
            {
                datumType = DNEDatumType._EDT_NUM;
                return false;
            }
        }

        private static IDNMcGridCoordinateSystem CreateGridCoordinateSystem(MCTGridCoordinateSystem mctGridCoordinateSystem)
        {
            DNEGridCoordSystemType gridCoordSys = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), mctGridCoordinateSystem.GridCoordinateSystemType);
            DNEDatumType datum;
            bool isDatumValid = GetDatum(mctGridCoordinateSystem.Datum, out datum);

            IDNMcGridCoordinateSystem mcGridCoordinateSystem = null;
            switch (gridCoordSys)
            {
                case DNEGridCoordSystemType._EGCS_GEOGRAPHIC:
                    try
                    {
                        if(isDatumValid)
                            mcGridCoordinateSystem = DNMcGridCoordSystemGeographic.Create(datum);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridCoordSystemGeographic.Create", McEx);
                    }

                    break;
                case DNEGridCoordSystemType._EGCS_UTM:
                    try
                    {
                        if (isDatumValid)
                            mcGridCoordinateSystem = DNMcGridUTM.Create(mctGridCoordinateSystem.Zone, datum);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridUTM.Create", McEx);
                    }
                    break;
                case DNEGridCoordSystemType._EGCS_GENERIC_GRID:
                    try
                    {
                        if (mctGridCoordinateSystem.EpsgCode != null && mctGridCoordinateSystem.EpsgCode != "")
                            mcGridCoordinateSystem = DNMcGridGeneric.Create(mctGridCoordinateSystem.EpsgCode);
                        else if(mctGridCoordinateSystem.FullInitialization != null && mctGridCoordinateSystem.FullInitialization != "")
                            mcGridCoordinateSystem = DNMcGridGeneric.Create(mctGridCoordinateSystem.FullInitialization, false);
                        else if(mctGridCoordinateSystem.GridParameters != null && mctGridCoordinateSystem.GridParameters.Length > 0)
                            mcGridCoordinateSystem = DNMcGridGeneric.Create(mctGridCoordinateSystem.GridParameters);
                      
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridGeneric.Create", McEx);
                    }

                    break;
                case DNEGridCoordSystemType._EGCS_NEW_ISRAEL:
                    try
                    {
                        mcGridCoordinateSystem = DNMcGridNewIsrael.Create();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridGeneric.Create", McEx);
                    }

                    break;

                default:
                    MessageBox.Show("Current viewport coordinate system not supported in load viewport", "Viewport coordinate system not supported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
            }
            Manager_MCGridCoordinateSystem.AddNewGridCoordinateSystem(mcGridCoordinateSystem);
            return mcGridCoordinateSystem;
        }


        private static void SetImageProcessingData(IDNMcMapViewport mapViewport, IDNMcRasterMapLayer layer, MCTImageProcessingData imageProcessingData)
        {
            try
            {
                mapViewport.SetFilterImageProcessing(layer, imageProcessingData.Filter);
            }
            catch (MapCoreException McEx)
            {
                HandleMapCoreExecption("SetFilterImageProcessing", McEx);
            }

            try
            {
                if (imageProcessingData.CustomFilter != null)
                {
                    mapViewport.SetCustomFilter(layer, imageProcessingData.CustomFilter.FilterXsize,
                        imageProcessingData.CustomFilter.FilterYsize,
                        imageProcessingData.CustomFilter.Filters,
                        imageProcessingData.CustomFilter.Bias,
                        imageProcessingData.CustomFilter.Divider);
                }

            }
            catch (MapCoreException McEx)
            {
                HandleMapCoreExecption("SetCustomFilter", McEx);
            }
            try
            {
                mapViewport.SetWhiteBalanceBrightness(layer, imageProcessingData.WhiteBalanceBrightnessR, imageProcessingData.WhiteBalanceBrightnessG, imageProcessingData.WhiteBalanceBrightnessB);
            }
            catch (MapCoreException McEx)
            {
                HandleMapCoreExecption("SetWhiteBalanceBrightness", McEx);
            }

            try
            {
                mapViewport.SetEnableColorTableImageProcessing(layer, imageProcessingData.IsEnableColorTableImageProcessing);
            }
            catch (MapCoreException McEx)
            {
                HandleMapCoreExecption("SetEnableColorTableImageProcessing", McEx);
            }

            for (int i = 0; i < 3; i++)
            {
                MCTImageProcessingChannelData processingChannelData = imageProcessingData.ChannelDatas[i];
                DNEColorChannel Channel = imageProcessingData.ChannelDatas[i].Channel;
                try
                {
                    mapViewport.SetUserColorValues(layer, Channel, imageProcessingData.ChannelDatas[i].UserColorValues, imageProcessingData.ChannelDatas[i].UserColorValuesUse);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetUserColorValues", McEx);
                }

                try
                {
                    mapViewport.SetColorTableBrightness(layer, Channel, imageProcessingData.ChannelDatas[i].Brightness);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("GetColorTableBrightness", McEx);
                }

                try
                {
                    mapViewport.SetContrast(layer, Channel, imageProcessingData.ChannelDatas[i].Contrast);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetContrast", McEx);
                }

                try
                {
                    mapViewport.SetNegative(layer, Channel, imageProcessingData.ChannelDatas[i].Negative);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetNegative", McEx);
                }

                try
                {
                    mapViewport.SetGamma(layer, Channel, imageProcessingData.ChannelDatas[i].Gamma);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetGamma", McEx);
                }

                if (imageProcessingData.ChannelDatas[i].IsOriginalHistogramSet)
                {
                    try
                    {
                        mapViewport.SetOriginalHistogram(layer, Channel, imageProcessingData.ChannelDatas[i].OriginalHistogram);
                    }
                    catch (MapCoreException McEx)
                    {
                        HandleMapCoreExecption("SetOriginalHistogram", McEx);
                    }
                }
                try
                {
                    mapViewport.SetHistogramNormalization(layer, Channel,
                        imageProcessingData.ChannelDatas[i].HistogramNormalizationUse,
                        imageProcessingData.ChannelDatas[i].HistogramNormalizationMean,
                        imageProcessingData.ChannelDatas[i].HistogramNormalizationStdev);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetHistogramNormalization", McEx);
                }

                try
                {
                    mapViewport.SetVisibleAreaOriginalHistogram(layer, Channel,
                        imageProcessingData.ChannelDatas[i].VisibleAreaOriginalHistogram);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetVisibleAreaOriginalHistogram", McEx);
                }

                try
                {
                    mapViewport.SetHistogramEqualization(layer, Channel, imageProcessingData.ChannelDatas[i].HistogramEqualization);
                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetHistogramEqualization", McEx);
                }

                try
                {
                    mapViewport.SetHistogramFit(layer, Channel, imageProcessingData.ChannelDatas[i].ReferenceHistogramUse, imageProcessingData.ChannelDatas[i].ReferenceHistogram);

                }
                catch (MapCoreException McEx)
                {
                    HandleMapCoreExecption("SetHistogramFit", McEx);
                }

            }
        }

        public void CreateSectionMap(bool isCameFromGetTerrainHeightsAlongLine, DNSMcVector3D[] resultPoints)
        {
            try
            {
                IDNMcSectionMapViewport m_SectionMapViewport = null;

                if (isCameFromGetTerrainHeightsAlongLine)
                {
                    DNMcSectionMapViewport.CreateSection(ref m_SectionMapViewport,
                                                         ref m_Camera,
                                                         m_CreateData,
                                                         m_arrTerrain,
                                                         m_AutomationParams.MapViewport.SectionMapViewportData.SectionRoutePoints,
                                                         resultPoints);
                }
                else
                {
                    DNMcSectionMapViewport.CreateSection(ref m_SectionMapViewport,
                                                         ref m_Camera,
                                                         m_CreateData,
                                                         m_arrTerrain,
                                                         m_AutomationParams.MapViewport.SectionMapViewportData.SectionRoutePoints);
                }
                MainForm.m_AutoViewport = m_SectionMapViewport;

                SetAutomationParamsAfterViewportCreate();

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("CreateSection", McEx);
            }
        }

    }
}
