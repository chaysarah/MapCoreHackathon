using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using MCTester.General_Forms;
using UnmanagedWrapper;
using MCTester.GUI.Map;
using System.Reflection;
using MapCore;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers.MapWorld;
using MCTester.Managers;

namespace MCTester.ButtonsImplementation
{
    public class btnPerformanceTester
    {
        private MCTPerformanceTest PerformanceTestParams;
        private IDNMcMapViewport mapViewport;
        private IDNMcMapCamera mapCamera;
        private Random m_Rand;
        private long startTime, stopTime;
        private long freq;
        private List<double> m_lDuration;
        private List<double> m_lFrameRate;
        private List<float> m_lMemPrivateBytes;
        private List<float> m_lMemVirtualBytes;
        private List<float> m_lMemWorkingSet;
        private const int testIterations = 3;
        private OpenFileDialog OFD;
        private FolderBrowserDialog FBD;
        private StreamWriter STW;
        private Process myProcess;
        private PerformanceCounter mCounterPrivateBytes;
        private PerformanceCounter mCounterVirtualBytes;
        private PerformanceCounter mCounterWorkingSet;

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        public btnPerformanceTester()
        {
            mapViewport = null;
            mapCamera = null;
            m_Rand = new Random();
            startTime = 0;
            stopTime = 0;
            OFD = new OpenFileDialog();
            FBD = new FolderBrowserDialog();
            m_lDuration = new List<double>();
            m_lFrameRate = new List<double>();
            m_lMemPrivateBytes = new List<float>();
            m_lMemVirtualBytes = new List<float>();
            m_lMemWorkingSet = new List<float>();


            myProcess = Process.GetCurrentProcess();

            if (QueryPerformanceFrequency(out freq) == false)
            {
                // high-performance counter not supported
                throw new Win32Exception();
            }

            string processName = Process.GetCurrentProcess().ProcessName;

            mCounterPrivateBytes = new PerformanceCounter();
            mCounterPrivateBytes.CategoryName = "Process";
            mCounterPrivateBytes.CounterName = "Private Bytes";
            mCounterPrivateBytes.InstanceName = processName;

            mCounterWorkingSet = new PerformanceCounter();
            mCounterWorkingSet.CategoryName = "Process";
            mCounterWorkingSet.CounterName = "Working Set";
            mCounterWorkingSet.InstanceName = processName;

            mCounterVirtualBytes = new PerformanceCounter();
            mCounterVirtualBytes.CategoryName = "Process";
            mCounterVirtualBytes.CounterName = "Virtual Bytes";
            mCounterVirtualBytes.InstanceName = processName;
        }

        public void ExecuteAction()
        {
            OFD.RestoreDirectory = true;
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                PerformanceTestParams = null;
                try
                {
                    StreamReader SR = new StreamReader(OFD.FileName);
                    XmlSerializer Xser = new XmlSerializer(typeof(MCTPerformanceTest));

                    PerformanceTestParams = (MCTPerformanceTest)Xser.Deserialize(SR);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("LoadXmlFile", McEx);
                }

                // stop tester render
                MCTMapForm.eRender = MCTMapForm.RenderType.Manual;


                object[] paramsArr = null;
                foreach (string methodName in MCTPerformanceTest.TestManager)
                {
                    MethodInfo MI = this.GetType().GetMethod(methodName);
                    MI.Invoke(this, paramsArr);
                }

                MCTMapForm.eRender = MCTMapForm.RenderType.FlagBaseRender;
                MessageBox.Show("Auto performance test scenario completed successfully", "scenario completed successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void OpenMap()
        {
            if (PerformanceTestParams.OpenMapTest.MapTerrainPath != "")
            {
                DNSCreateDataMV createData = new DNSCreateDataMV(PerformanceTestParams.OpenMapTest.eMapType);
                UserDataFactory UDF = new UserDataFactory();
                IDNMcMapTerrain[] terrains = new IDNMcMapTerrain[1];
                MCTMapForm mapForm = new MCTMapForm(false);
                MCTester.Managers.MapWorld.MCTMapFormManager.AddMapForm(mapForm);

                try
                {
                    terrains[0] = DNMcMapTerrain.Load(PerformanceTestParams.OpenMapTest.MapTerrainPath, "", UDF);
                    Manager_MCLayers.CheckingAfterLoadTerrain(terrains[0]);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcMapTerrain.Load", McEx);
                }

                createData.CoordinateSystem = terrains[0].CoordinateSystem;
                createData.uViewportID = 0;
                createData.hWnd = mapForm.MapPointer;
                if (createData.hWnd == IntPtr.Zero)
                {
                    createData.uWidth = (uint)mapForm.Width;
                    createData.uHeight = (uint)mapForm.Height;
                }

                createData.pGrid = null;
                createData.pImageCalc = null;
                DNSInitParams initParams = new DNSInitParams();
                initParams.eLoggingLevel = PerformanceTestParams.OpenMapTest.eLoggingLevel;
                initParams.uNumBackgroundThreads = 0;
                createData.pDevice = DNMcMapDevice.Create(initParams);
                createData.pOverlayManager = Manager_MCOverlayManager.CreateOverlayManager(PerformanceTestParams.OpenMapTest.MapGridCoordinateSystem.GridCoordSystem);
                IDNMcOverlay activeOverlay = DNMcOverlay.Create(createData.pOverlayManager);
                Manager_MCOverlayManager.dOverlayManager_Overlay[createData.pOverlayManager] = activeOverlay;
                //Manager_MCOverlayManager.ActiveOverlayManager = createData.pOverlayManager;


                try
                {
                    CollectDataBeforeAction();
                    QueryPerformanceCounter(out startTime);

                    DNMcMapViewport.Create(ref mapViewport, ref mapCamera, createData, terrains);

                    QueryPerformanceCounter(out stopTime);
                    CollectDataAfterAction();

                    OpenStreamConection("Create");
                    PrintResultToCSV("Create");
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Create", McEx);
                }

                mapForm.Viewport = mapViewport;

                try
                {
                    foreach (IDNMcMapTerrain terr in terrains)
                    {
                        mapForm.Viewport.SetTerrainNumCacheTiles(terr, true, PerformanceTestParams.OpenMapTest.TerrainNumCacheTiles);
                        mapForm.Viewport.SetTerrainNumCacheTiles(terr, false, PerformanceTestParams.OpenMapTest.TerrainNumCacheTiles);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetTerrainNumCacheTiles", McEx);
                }

                mapForm.CreateEditMode(mapViewport);

                try
                {
                    mapCamera.SetCameraPosition(PerformanceTestParams.OpenMapTest.MapCameraOpeningPosition.Position, false);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetCameraPosition", McEx);
                }

                try
                {
                    if (PerformanceTestParams.OpenMapTest.MapCameraOpeningOrientation.Pitch != 0 && PerformanceTestParams.OpenMapTest.MapCameraOpeningOrientation.Roll != 0)
                        mapCamera.SetCameraOrientation(PerformanceTestParams.OpenMapTest.MapCameraOpeningOrientation.Yaw, PerformanceTestParams.OpenMapTest.MapCameraOpeningOrientation.Pitch, PerformanceTestParams.OpenMapTest.MapCameraOpeningOrientation.Roll, false);
                    else
                        mapCamera.SetCameraOrientation(PerformanceTestParams.OpenMapTest.MapCameraOpeningOrientation.Yaw, false);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetCameraOrientation", McEx);
                }

                Manager_MCViewports.AddViewport(mapViewport);

                mapForm.Show();


                try
                {
                    mapViewport.SetObjectsVisibilityMaxScale(PerformanceTestParams.OpenMapTest.ObjectsVisibilityMaxScale);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetObjectsVisibilityMaxScale", McEx);
                }

                System.Windows.Forms.Application.DoEvents();

                try
                {
                    CollectDataBeforeAction();
                    QueryPerformanceCounter(out startTime);

                    mapForm.Viewport.PerformPendingUpdates(DNEPendingUpdateType._EPUT_ANY_UPDATE);
                    mapViewport.Render();
                    DNMcMapDevice.PerformPendingCalculations();

                    QueryPerformanceCounter(out stopTime);
                    CollectDataAfterAction();

                    PrintResultToCSV("PerformPendingUpdatesAndRender");
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("PerformPendingUpdates + Render", McEx);
                }

                if (mapForm.Viewport.HasPendingUpdates(DNEPendingUpdateType._EPUT_TERRAIN))
                {
                    MessageBox.Show("PerformPendingUpdates: still has pending updates");
                }

                CloseStreamConection();

                System.Windows.Forms.Application.DoEvents();
            }
        }

        public void OperateDelays()
        {
            if (mapViewport == null)
            {
                if (MCTMapFormManager.MapForm != null)
                    mapViewport = MCTMapFormManager.MapForm.Viewport;
                else
                    return;
            }

            try
            {
                if (PerformanceTestParams.LoadObjectsTest.ObjectDelays.ObjectConditionNumToUpdate != 0)
                    mapViewport.SetObjectsDelay(DNEObjectDelayType._EODT_VIEWPORT_CHANGE_OBJECT_CONDITION, true, PerformanceTestParams.LoadObjectsTest.ObjectDelays.ObjectConditionNumToUpdate);
                if (PerformanceTestParams.LoadObjectsTest.ObjectDelays.ObjectUpdateNumToUpdate != 0)
                    mapViewport.SetObjectsDelay(DNEObjectDelayType._EODT_VIEWPORT_CHANGE_OBJECT_UPDATE, true, PerformanceTestParams.LoadObjectsTest.ObjectDelays.ObjectUpdateNumToUpdate);
                if (PerformanceTestParams.LoadObjectsTest.ObjectDelays.HiddenObjectCollisionNumToCheck != 0)
                    mapViewport.SetObjectsDelay(DNEObjectDelayType._EODT_VIEWPORT_CHECK_HIDDEN_OBJECT_COLLISION, true, PerformanceTestParams.LoadObjectsTest.ObjectDelays.HiddenObjectCollisionNumToCheck);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetObjectsDelay", McEx);
            }
        }

        public void LoadObjects()
        {
            if (mapViewport == null)
            {
                if (MCTMapFormManager.MapForm != null)
                    mapViewport = MCTMapFormManager.MapForm.Viewport;
                else
                    return;
            }

            if (PerformanceTestParams.LoadObjectsTest.LoadedObjectsParams.Count != 0)
            {
                UserDataFactory UDF = new UserDataFactory();
                IDNMcOverlay activeOverlay = Manager_MCOverlayManager.ActiveOverlay;
                List<IDNMcObject> loadedObjects = new List<IDNMcObject>();
                Dictionary<IDNMcObject, int> objectParams = new Dictionary<IDNMcObject, int>();
                bool isRelativeToDTM;
                bool pbHeightFound = false;
                double height;
                DNMcNullableOut<DNSMcVector3D> normal = null;

                foreach (ObjectsParams obj in PerformanceTestParams.LoadObjectsTest.LoadedObjectsParams)
                {
                    IDNMcObject[] objArr = activeOverlay.LoadObjects(obj.ObjectPath, UDF);
                    objectParams.Add(objArr[0], obj.AmountObjects);
                }

                try
                {
                    mapViewport.Render();
                    DNMcMapDevice.PerformPendingCalculations();

                    System.Windows.Forms.Application.DoEvents();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Render", McEx);
                }

                foreach (IDNMcObject obj in objectParams.Keys)
                {
                    IDNMcObjectScheme scheme = obj.GetScheme();
                    DNSMcVector3D[] objOriginLocationPoints = obj.GetLocationPoints(0);
                    DNSVariantProperty[] Properties = obj.GetProperties();
                    int ObjNumPoints = objOriginLocationPoints.Length;
                    DNSMcVector3D[] objLocationPointsAfterShifting = new DNSMcVector3D[ObjNumPoints];

                    int iterations = objectParams[obj];

                    for (int numObj = 0; numObj < iterations; numObj++)
                    {
                        DNSMcVector3D distanceDelta = new DNSMcVector3D(); ;
                        double NewXPoint = m_Rand.Next((int)PerformanceTestParams.LoadObjectsTest.BoxLoadingArea.ObjectLoadingArea.MinVertex.x, (int)PerformanceTestParams.LoadObjectsTest.BoxLoadingArea.ObjectLoadingArea.MaxVertex.x);
                        double NewYPoint = m_Rand.Next((int)PerformanceTestParams.LoadObjectsTest.BoxLoadingArea.ObjectLoadingArea.MinVertex.y, (int)PerformanceTestParams.LoadObjectsTest.BoxLoadingArea.ObjectLoadingArea.MaxVertex.y);
                        distanceDelta.x = NewXPoint - objOriginLocationPoints[0].x;
                        distanceDelta.y = NewYPoint - objOriginLocationPoints[0].y;

                        for (int locationIdx = 0; locationIdx < ObjNumPoints; locationIdx++)
                        {
                            objLocationPointsAfterShifting[locationIdx].x = objOriginLocationPoints[locationIdx].x + distanceDelta.x;
                            objLocationPointsAfterShifting[locationIdx].y = objOriginLocationPoints[locationIdx].y + distanceDelta.y;

                            try
                            {
                                uint PropId; // TODO: properly support RelativeToDTM as PrivateProperty!
                                scheme.GetObjectLocation(0).GetRelativeToDTM(out isRelativeToDTM, out PropId);
                                
                                if (isRelativeToDTM == true)
                                    objLocationPointsAfterShifting[locationIdx].z = 0;
                                else
                                {
                                    mapViewport.GetTerrainHeight(objLocationPointsAfterShifting[locationIdx], out pbHeightFound, out height, normal); 
                                    objLocationPointsAfterShifting[locationIdx].z = height;
                                }
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("GetRelativeToDTM", McEx);
                            }
                        }

                        try
                        {
                            CollectDataBeforeAction();
                            QueryPerformanceCounter(out startTime);
                            IDNMcObject newObj = DNMcObject.Create(activeOverlay,
                                                                    scheme,
                                                                    objLocationPointsAfterShifting);
                            newObj.SetProperties(Properties);

                            QueryPerformanceCounter(out stopTime);
                            CollectDataAfterAction();
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                        }
                    }
                }

                // remove all loaded objects
                try
                {
                    foreach (IDNMcObject obj in objectParams.Keys)
                    {
                        obj.Remove();
                        obj.Dispose();
                    }

                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Remove", McEx);
                }

                System.Windows.Forms.Application.DoEvents();

                OpenStreamConection("Create Object");
                PrintResultToCSV("Create Object");

                try
                {
                    CollectDataBeforeAction();
                    QueryPerformanceCounter(out startTime);

                    mapViewport.Render();

                    QueryPerformanceCounter(out stopTime);

                    DNMcMapDevice.PerformPendingCalculations();
                    CollectDataAfterAction();

                    PrintResultToCSV("Render");
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Render", McEx);
                }

                System.Windows.Forms.Application.DoEvents();

                CloseStreamConection();
            }
        }

        public void MapZoom()
        {
            if (mapViewport == null)
            {
                if (MCTMapFormManager.MapForm != null)
                    mapViewport = MCTMapFormManager.MapForm.Viewport;
                else
                    return;
            }

            if (PerformanceTestParams.MapZoomTest.NumIterations > 0)
            {
                for (int testIterationsNum = 0; testIterationsNum < testIterations; testIterationsNum++)
                {
                    for (int iteration = 0; iteration < PerformanceTestParams.MapZoomTest.NumIterations; iteration++)
                    {
                        // collect data only at the last iteration
                        if (testIterationsNum == testIterations - 1)
                            CollectDataBeforeAction();

                        if (mapViewport.MapType == DNEMapType._EMT_2D)
                        {
                            try
                            {
                                float currScale = mapViewport.GetCameraScale();
                                if (iteration < PerformanceTestParams.MapZoomTest.NumIterations / 2)
                                {
                                    QueryPerformanceCounter(out startTime);

                                    mapViewport.SetCameraScale(currScale * PerformanceTestParams.MapZoomTest.Factor);
                                    mapViewport.Render();

                                    QueryPerformanceCounter(out stopTime);

                                    DNMcMapDevice.PerformPendingCalculations();
                                }
                                else
                                {
                                    QueryPerformanceCounter(out startTime);

                                    mapViewport.SetCameraScale(currScale / PerformanceTestParams.MapZoomTest.Factor);
                                    mapViewport.Render();

                                    QueryPerformanceCounter(out stopTime);

                                    DNMcMapDevice.PerformPendingCalculations();
                                }

                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("SetCameraScale", McEx);
                            }
                        }
                        else
                        {
                            float currFOV = mapViewport.GetCameraFieldOfView();
                            if (iteration < PerformanceTestParams.MapZoomTest.NumIterations / 2)
                            {
                                try
                                {
                                    QueryPerformanceCounter(out startTime);

                                    mapViewport.SetCameraFieldOfView(currFOV * PerformanceTestParams.MapZoomTest.Factor);
                                    mapViewport.Render();

                                    QueryPerformanceCounter(out stopTime);

                                    DNMcMapDevice.PerformPendingCalculations();
                                }
                                catch (MapCoreException McEx)
                                {
                                    MapCore.Common.Utilities.ShowErrorMessage("SetCameraFieldOfView", McEx);
                                }
                            }
                            else
                            {
                                try
                                {
                                    QueryPerformanceCounter(out startTime);

                                    mapViewport.SetCameraFieldOfView(currFOV / PerformanceTestParams.MapZoomTest.Factor);
                                    mapViewport.Render();

                                    QueryPerformanceCounter(out stopTime);

                                    DNMcMapDevice.PerformPendingCalculations();
                                }
                                catch (MapCoreException McEx)
                                {
                                    MapCore.Common.Utilities.ShowErrorMessage("SetCameraFieldOfView", McEx);
                                }
                            }
                        }

                        // collect data only at the last iteration
                        if (testIterationsNum == testIterations - 1)
                            CollectDataAfterAction();
                    }
                }

                if (mapViewport.MapType == DNEMapType._EMT_2D)
                {
                    OpenStreamConection("SetCameraScale");
                    PrintResultToCSV("SetCameraScale");
                }
                else
                {
                    OpenStreamConection("SetCameraFieldOfView");
                    PrintResultToCSV("SetCameraFieldOfView");
                }

                CloseStreamConection();
            }
        }

        public void MapMove()
        {
            if (mapViewport == null)
            {
                if (MCTMapFormManager.MapForm != null)
                    mapViewport = MCTMapFormManager.MapForm.Viewport;
                else
                    return;
            }

            DNSMcVector3D vectorToRight = new DNSMcVector3D(0, -PerformanceTestParams.MapMoveTest.Delta, 0);
            DNSMcVector3D vectorToLeft = new DNSMcVector3D(0, PerformanceTestParams.MapMoveTest.Delta, 0);

            if (PerformanceTestParams.MapMoveTest.NumIterations > 0)
            {
                for (int testIterationsNum = 0; testIterationsNum < testIterations; testIterationsNum++)
                {
                    for (int iteration = 0; iteration < PerformanceTestParams.MapMoveTest.NumIterations; iteration++)
                    {
                        // collect data only at the last iteration
                        if (testIterationsNum == testIterations - 1)
                            CollectDataBeforeAction();

                        if (mapViewport.MapType == DNEMapType._EMT_2D)
                        {
                            try
                            {
                                if (iteration < PerformanceTestParams.MapMoveTest.NumIterations / 2)
                                {
                                    QueryPerformanceCounter(out startTime);

                                    mapViewport.ScrollCamera(PerformanceTestParams.MapMoveTest.Delta, 0);
                                    mapViewport.Render();

                                    QueryPerformanceCounter(out stopTime);

                                    DNMcMapDevice.PerformPendingCalculations();
                                }
                                else
                                {
                                    QueryPerformanceCounter(out startTime);

                                    mapViewport.ScrollCamera(-PerformanceTestParams.MapMoveTest.Delta, 0);
                                    mapViewport.Render();

                                    QueryPerformanceCounter(out stopTime);

                                    DNMcMapDevice.PerformPendingCalculations();
                                }
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("ScrollCamera", McEx);
                            }
                        }
                        else
                        {
                            try
                            {
                                if (iteration < PerformanceTestParams.MapMoveTest.NumIterations / 2)
                                {
                                    QueryPerformanceCounter(out startTime);

                                    mapViewport.SetCameraPosition(vectorToRight, true);
                                    mapViewport.Render();

                                    QueryPerformanceCounter(out stopTime);

                                    DNMcMapDevice.PerformPendingCalculations();
                                }
                                else
                                {
                                    QueryPerformanceCounter(out startTime);

                                    mapViewport.SetCameraPosition(vectorToLeft, true);
                                    mapViewport.Render();

                                    QueryPerformanceCounter(out stopTime);

                                    DNMcMapDevice.PerformPendingCalculations();
                                }

                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("SetCameraPosition", McEx);
                            }
                        }

                        // collect data only at the last iteration
                        if (testIterationsNum == testIterations - 1)
                            CollectDataAfterAction();
                    }
                }

                if (mapViewport.MapType == DNEMapType._EMT_2D)
                {
                    OpenStreamConection("ScrollCamera");
                    PrintResultToCSV("ScrollCamera");
                }
                else
                {
                    OpenStreamConection("SetCameraPosition");
                    PrintResultToCSV("SetCameraPosition");
                }

                CloseStreamConection();
            }
        }

        public void MapRotate()
        {
            if (mapViewport == null)
            {
                if (MCTMapFormManager.MapForm != null)
                    mapViewport = MCTMapFormManager.MapForm.Viewport;
                else
                    return;
            }

            if (PerformanceTestParams.MapRotateTest.NumIterations > 0)
            {
                for (int iteration = 0; iteration < PerformanceTestParams.MapRotateTest.NumIterations * testIterations; iteration++)
                {
                    // collect data only at the last iteration
                    if (iteration >= PerformanceTestParams.MapRotateTest.NumIterations * (testIterations - 1))
                        CollectDataBeforeAction();

                    try
                    {
                        QueryPerformanceCounter(out startTime);

                        mapViewport.SetCameraOrientation(PerformanceTestParams.MapRotateTest.Delta, true);
                        mapViewport.Render();

                        QueryPerformanceCounter(out stopTime);

                        DNMcMapDevice.PerformPendingCalculations();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("SetCameraOrientation", McEx);
                    }

                    // collect data only at the last iteration
                    if (iteration >= PerformanceTestParams.MapRotateTest.NumIterations * (testIterations - 1))
                        CollectDataAfterAction();
                }

                OpenStreamConection("ScrollCamera");
                PrintResultToCSV("ScrollCamera");
                CloseStreamConection();
            }
        }

        public void CollectDataBeforeAction()
        {
            m_lMemPrivateBytes.Add(mCounterPrivateBytes.NextValue());
            m_lMemVirtualBytes.Add(mCounterVirtualBytes.NextValue());
            m_lMemWorkingSet.Add(mCounterWorkingSet.NextValue());
        }

        public void CollectDataAfterAction()
        {
            m_lDuration.Add(((double)(stopTime - startTime) / (double)freq) * 1000);
            m_lFrameRate.Add(Math.Round((double)freq / (double)(stopTime - startTime), 0));

            m_lMemPrivateBytes.Add(mCounterPrivateBytes.NextValue());
            m_lMemVirtualBytes.Add(mCounterVirtualBytes.NextValue());
            m_lMemWorkingSet.Add(mCounterWorkingSet.NextValue());
        }

        public void OpenStreamConection(string funcName)
        {
            //Open stream writer
            string path = Path.GetDirectoryName(OFD.FileName) + "\\" + funcName.Replace(" ", "") + "_" + DateTime.Today.Day.ToString() + "_" + DateTime.Today.Month.ToString() + "_" + DateTime.Today.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".csv";
            STW = new StreamWriter(path);
        }

        public void CloseStreamConection()
        {
            //Close stream writer
            STW.Close();
        }

        public void PrintResultToCSV(string funcName)
        {
            string outputLine = "";
            double totalDur = 0;
            double totalFPS = 0;
            float PrivateBytesDelta = 0;
            float VirtualBytesDelta = 0;
            float WorkingSetDelta = 0;

            double minDur = 0;
            double maxDur = 0;
            double minFPS = 0;
            double maxFPS = 0;
            double minMemPrivateBytes = 0;
            double maxMemPrivateBytes = 0;
            double minMemVirtualBytes = 0;
            double maxMemVirtualBytes = 0;
            double minMemWorkingSet = 0;
            double maxMemWorkingSet = 0;

            int numOfActionExecuted = m_lDuration.Count;

            if (numOfActionExecuted > 0)
            {
                minDur = (m_lDuration[0] > 0) ? m_lDuration[0] : 0;
                maxDur = minDur;

                minFPS = (m_lFrameRate[0] > 0) ? m_lFrameRate[0] : 0;
                maxFPS = minFPS;

                minMemPrivateBytes = m_lMemPrivateBytes[0];
                maxMemPrivateBytes = m_lMemPrivateBytes[0];

                minMemVirtualBytes = m_lMemVirtualBytes[0];
                maxMemVirtualBytes = m_lMemVirtualBytes[0];

                minMemWorkingSet = m_lMemWorkingSet[0];
                maxMemWorkingSet = m_lMemWorkingSet[0];
            }

            // print header line
            outputLine = "" + "," +
                            "Time (MS)" + "," +
                            "Frame Rate" + "," +
                            "Memory - Private Bytes (Bytes) " + "," +
                            "Memory - Virtual Bytes (Bytes) " + "," +
                            "Memory - Working Set (Bytes) ";

            STW.WriteLine(outputLine);

            int memSampleCounter = 0;
            for (int sample = 0; sample < numOfActionExecuted; sample++)
            {
                memSampleCounter = sample * 2;

                PrivateBytesDelta = m_lMemPrivateBytes[memSampleCounter + 1] - m_lMemPrivateBytes[memSampleCounter];
                VirtualBytesDelta = m_lMemVirtualBytes[memSampleCounter + 1] - m_lMemVirtualBytes[memSampleCounter];
                WorkingSetDelta = m_lMemWorkingSet[memSampleCounter + 1] - m_lMemWorkingSet[memSampleCounter];

                outputLine = funcName + "," +
                                m_lDuration[sample].ToString() + "," +
                                m_lFrameRate[sample].ToString() + "," +
                                PrivateBytesDelta.ToString() + "," +
                                VirtualBytesDelta.ToString() + "," +
                                WorkingSetDelta.ToString();

                if (m_lDuration[sample] < minDur)
                    minDur = m_lDuration[sample];
                if (m_lDuration[sample] > maxDur)
                    maxDur = m_lDuration[sample];
                totalDur += m_lDuration[sample];

                if (m_lFrameRate[sample] < minFPS)
                    minFPS = m_lFrameRate[sample];
                if (m_lFrameRate[sample] > maxFPS)
                    maxFPS = m_lFrameRate[sample];
                totalFPS += m_lFrameRate[sample];

                if (PrivateBytesDelta < minMemPrivateBytes)
                    minMemPrivateBytes = PrivateBytesDelta;
                if (PrivateBytesDelta > maxMemPrivateBytes)
                    maxMemPrivateBytes = PrivateBytesDelta;

                if (VirtualBytesDelta < minMemVirtualBytes)
                    minMemVirtualBytes = VirtualBytesDelta;
                if (VirtualBytesDelta > maxMemVirtualBytes)
                    maxMemVirtualBytes = VirtualBytesDelta;

                if (WorkingSetDelta < minMemWorkingSet)
                    minMemWorkingSet = WorkingSetDelta;
                if (WorkingSetDelta > maxMemWorkingSet)
                    maxMemWorkingSet = WorkingSetDelta;

                STW.WriteLine(outputLine);
            }

            //Space Line
            outputLine = "";
            STW.WriteLine(outputLine);

            //Print summary
            outputLine = "Total Time" + "," +
                            totalDur.ToString();
            STW.WriteLine(outputLine);

            outputLine = "Minimum" + "," +
                            minDur.ToString() + "," +
                            minFPS.ToString();
            STW.WriteLine(outputLine);

            outputLine = "Maximum" + "," +
                            maxDur.ToString() + "," +
                            maxFPS.ToString();
            STW.WriteLine(outputLine);

            if (numOfActionExecuted > 0)
            {
                string dur = (totalDur > 0) ? (totalDur / numOfActionExecuted).ToString() : "0";
                string fps = (totalFPS > 0) ? (totalFPS / numOfActionExecuted).ToString() : "0";

                outputLine = "Average" + "," +
                             dur + "," +
                             fps + "," +
                            ((m_lMemPrivateBytes[numOfActionExecuted * 2 - 1] - m_lMemPrivateBytes[0]) / numOfActionExecuted).ToString() + "," +
                            ((m_lMemVirtualBytes[numOfActionExecuted * 2 - 1] - m_lMemVirtualBytes[0]) / numOfActionExecuted).ToString() + "," +
                            ((m_lMemWorkingSet[numOfActionExecuted * 2 - 1] - m_lMemWorkingSet[0]) / numOfActionExecuted).ToString();
            }

            STW.WriteLine(outputLine);

            //Space Line
            outputLine = "";
            STW.WriteLine(outputLine);

            m_lDuration.Clear();
            m_lFrameRate.Clear();
            m_lMemPrivateBytes.Clear();
            m_lMemVirtualBytes.Clear();
            m_lMemWorkingSet.Clear();
        }
    }
}
