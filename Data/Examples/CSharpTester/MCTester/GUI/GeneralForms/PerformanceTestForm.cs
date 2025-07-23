using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using System.Diagnostics;
using MCTester.GUI.Map;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using MCTester.Managers.MapWorld;
using MCTester.Managers;

namespace MCTester.General_Forms
{
    public partial class PerformanceTestForm : Form
    {
        private IDNMcMapViewport activeViewport;
        private Stopwatch sw;
        private double m_LocationArea_TL_X;
        private double m_LocationArea_TL_Y;
        private double m_LocationArea_BR_X;
        private double m_LocationArea_BR_Y;
        private int m_Iteration;
        private float m_TestCheckedValue;
        private long m_AverageFPS;
        OpenFileDialog OFD;
        private Random randX = new Random();
        private bool m_IsObjLoadingAreaExist;
        private IDNMcObject RectAreaObj;
                
        public PerformanceTestForm()
        {
            InitializeComponent();
            sw = new Stopwatch();
            OFD = new OpenFileDialog();
            m_IsObjLoadingAreaExist = false;
        }

        private void btnZoomTest_Click(object sender, EventArgs e)
        {
            if (IsViewportExist())
            {
                if (activeViewport.MapType == DNEMapType._EMT_2D)
                {
                    m_Iteration = ntxIterations.GetInt32();
                    m_TestCheckedValue = ntxCheckedValue.GetFloat();

                    if (IsBackAndForthTestType())
                    {
                        try
                        {
                            sw.Start();
                            for (int i = 0; i < m_Iteration / 2; i++)
                            {                                
                                activeViewport.SetCameraScale(activeViewport.GetCameraScale() * m_TestCheckedValue);
                                activeViewport.Render();
                            }

                            for (int i = 0; i < m_Iteration / 2; i++)
                            {
                                activeViewport.SetCameraScale(activeViewport.GetCameraScale() / m_TestCheckedValue);
                                activeViewport.Render();

                            }
                            sw.Stop();

                            DNMcMapDevice.PerformPendingCalculations();
                            PrintResultToTextBox(((Button)sender).Text, "SetCameraScale", m_Iteration);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("SetCameraScale", McEx);
                        }
                    }                    
                }
                else
                    MessageBox.Show("Work only on 2D Map", "Wrong Map Type", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }           
        }

        private void btnRotateTest_Click(object sender, EventArgs e)
        {
            if (IsViewportExist())
            {
                m_Iteration = ntxIterations.GetInt32();
                m_TestCheckedValue = ntxCheckedValue.GetFloat();

                try
                {
                    sw.Start();

                    for (int i = 0; i < m_Iteration; i++)
                    {
                        activeViewport.ActiveCamera.SetCameraOrientation(m_TestCheckedValue, true);
                        activeViewport.Render();

                    }
                    sw.Stop();

                    DNMcMapDevice.PerformPendingCalculations();
                    PrintResultToTextBox(((Button)sender).Text, "SetCameraOrientation", m_Iteration);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetCameraOrientation", McEx);
                }
            } 
        }

        private void btnMoveTest_Click(object sender, EventArgs e)
        {
            if (IsViewportExist())
            {
                m_Iteration = ntxIterations.GetInt32();
                m_TestCheckedValue = ntxCheckedValue.GetFloat();

                DNSMcVector3D vectorToRight = new DNSMcVector3D(0, -m_TestCheckedValue, 0);
                DNSMcVector3D vectorToLeft = new DNSMcVector3D(0, m_TestCheckedValue, 0);

                if (IsBackAndForthTestType())
                {
                    if (activeViewport.MapType == DNEMapType._EMT_2D)
                    {
                        try
                        {
                            sw.Start();
                            for (int i = 0; i < m_Iteration / 2; i++)
                            {
                                activeViewport.ActiveCamera.ScrollCamera(0, (int)m_TestCheckedValue);
                                activeViewport.Render();

                            }
                            for (int i = 0; i < m_Iteration / 2; i++)
                            {
                                activeViewport.ActiveCamera.ScrollCamera(0, (int)-m_TestCheckedValue);
                                activeViewport.Render();
                                
                            }
                            sw.Stop();

                            DNMcMapDevice.PerformPendingCalculations();
                            PrintResultToTextBox(((Button)sender).Text, "ScrollCamera", m_Iteration);
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
                            sw.Start();
                            for (int i = 0; i < m_Iteration / 2; i++)
                            {
                                activeViewport.ActiveCamera.SetCameraPosition(vectorToRight, true);
                                activeViewport.Render();

                            }
                            for (int i = 0; i < m_Iteration / 2; i++)
                            {
                                activeViewport.ActiveCamera.SetCameraPosition(vectorToLeft, true);
                                activeViewport.Render();
                            }
                            sw.Stop();

                            DNMcMapDevice.PerformPendingCalculations();
                            PrintResultToTextBox(((Button)sender).Text, "SetCameraPosition", m_Iteration);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("SetCameraPosition", McEx);
                        }
                    }
                }
            }  
        }

        private void btnLayersVisibilityTest_Click(object sender, EventArgs e)
        {
            if (IsViewportExist())
            {
                m_Iteration = ntxIterations.GetInt32();

                if (IsBackAndForthTestType())
                {
                    try
                    {
                        IDNMcMapTerrain[] terrains = activeViewport.GetTerrains();
                        IDNMcMapTerrain terrain = terrains[0];
                        IDNMcMapLayer[] layers = terrain.GetLayers();


                        sw.Start();
                        for (int i = 0; i < m_Iteration / 2; i++)
                        {
                            foreach (IDNMcMapLayer layer in layers)
                            {
                                layer.SetVisibility(false, activeViewport);
                            }
                            activeViewport.Render();

                            foreach (IDNMcMapLayer layer in layers)
                            {
                                layer.SetVisibility(true, activeViewport);
                            }
                            activeViewport.Render();
                        }
                        sw.Stop();

                        DNMcMapDevice.PerformPendingCalculations();
                        PrintResultToTextBox(((Button)sender).Text, "SetLayerVisibility", m_Iteration);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("SetTerrainVisibility", McEx);
                    }
                }
            }               
        }                

        private void btnLoadObjectsTest_Click(object sender, EventArgs e)
        {
            if (IsViewportExist())
            {
                UserDataFactory UDF = new UserDataFactory();
                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;

                if (ctrlBrowseAreaFile.FileName != "")
                {
                    try
                    {
                        //Load rectangle item that representing the loading area
                        IDNMcObject[] rectItem = activeOverlay.LoadObjects(ctrlBrowseAreaFile.FileName, UDF);

                        SetObjectLoadingAreaPt(rectItem[0]);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("LoadObjects", McEx);
                    }
                }

                //Try to load objects only if loading area be defined
                if (m_IsObjLoadingAreaExist == true)
                {                    
                    int currRowNum = 0;
                    m_Iteration = ntxIterations.GetInt32();

                    while (!dgvLoadObject.Rows[currRowNum].IsNewRow)
                    {
                        try
                        {
                            IDNMcObject[] loadedObj = activeOverlay.LoadObjects(dgvLoadObject[0, currRowNum].Value.ToString(), UDF);
                            IDNMcObjectScheme scheme = loadedObj[0].GetScheme();
                            DNSVariantProperty[] Properties = loadedObj[0].GetProperties();
                            DNSMcVector3D[] objOriginLocationPoints = loadedObj[0].GetLocationPoints(0);
                            int ObjNumPoints = objOriginLocationPoints.Length;
                            DNSMcVector3D[] objLocationPointsAfterShifting = new DNSMcVector3D[ObjNumPoints];
                            
                            //Remove the first object
                            loadedObj[0].Remove();

                            int amountToLoad = int.Parse(dgvLoadObject[1, currRowNum].Value.ToString());
                            
                            //Measured the time that takes to raffled points, create an object and move all its points to the new location.
                            sw.Start();
                            for (int i = 0; i < amountToLoad; i++)
                            {
                                DNSMcVector3D distanceDelta = GetRaffledPointDelta(objOriginLocationPoints[0]);
                                for (int locationIdx = 0; locationIdx<ObjNumPoints; locationIdx++)
                                {
                                    objLocationPointsAfterShifting[locationIdx].x = objOriginLocationPoints[locationIdx].x + distanceDelta.x;
                                    objLocationPointsAfterShifting[locationIdx].y = objOriginLocationPoints[locationIdx].y + distanceDelta.y;
                                    objLocationPointsAfterShifting[locationIdx].z = objOriginLocationPoints[locationIdx].z + distanceDelta.z;
                                }

                                IDNMcObject newObj = DNMcObject.Create(activeOverlay,
                                                                            scheme,
                                                                            objLocationPointsAfterShifting);
                                newObj.SetProperties(Properties);
                            }
                            sw.Stop();

                            currRowNum++;
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("LoadObjects", McEx);
                        }
                    }

                    sw.Start();
                    for (int i = 0; i < m_Iteration; i++)
                    {
                        activeViewport.Render();
                    }
                    sw.Stop();

                    DNMcMapDevice.PerformPendingCalculations();
                    PrintResultToTextBox(((Button)sender).Text, "DNMcObject.Create", m_Iteration);
                }
                else
                {
                    MessageBox.Show("You have to insert objects loading area\n\rYou can choose to load a rectangle item or to draw one", "Object loading area did not defined", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
        }

        private DNSMcVector3D GetRaffledPointDelta(DNSMcVector3D originCoord)
        {
            DNSMcVector3D locationDelta = new DNSMcVector3D();
            
            double NewXPoint = randX.Next((int)m_LocationArea_TL_X, (int)m_LocationArea_BR_X);
            double NewYPoint = randX.Next((int)m_LocationArea_BR_Y, (int)m_LocationArea_TL_Y);

            locationDelta.x = NewXPoint - originCoord.x;
            locationDelta.y = NewYPoint - originCoord.y;
            
            return locationDelta;
        }


        private void btnOpenMapTest_Click(object sender, EventArgs e)
        {
            if (ctrlBrowseOpenMapFileName.FileName != "")
            {
                try
                {
                    DNSCreateDataMV m_CreateData;
                    IDNMcMapViewport Viewport = null;
                    IDNMcMapCamera Camera = null;
                    UserDataFactory UDF = new UserDataFactory();
                    IDNMcMapTerrain[] terrains = new IDNMcMapTerrain[1];
                    m_Iteration = ntxIterations.GetInt32();

                    MCTMapForm NewmapForm = new MCTMapForm(false);
                    MCTester.Managers.MapWorld.MCTMapFormManager.AddMapForm(NewmapForm);

                    terrains[0] = DNMcMapTerrain.Load(ctrlBrowseOpenMapFileName.FileName, ctrlBrowseOpenMapBaseDirectory.FileName, UDF);
                    Manager_MCLayers.CheckingAfterLoadTerrain(terrains[0]);
                    m_CreateData = new DNSCreateDataMV((rdb2DMap.Checked == true) ? DNEMapType._EMT_2D : DNEMapType._EMT_3D);
                    m_CreateData.bFullScreen = false;

                    //Init coordinate system accordingly the terrain coordinate system
                    m_CreateData.CoordinateSystem = terrains[0].CoordinateSystem;

                    m_CreateData.uViewportID = 0;
                    m_CreateData.hWnd = NewmapForm.MapPointer;
                    if (m_CreateData.hWnd == IntPtr.Zero)
                    {
                        m_CreateData.uWidth = (uint)NewmapForm.Width;
                        m_CreateData.uHeight = (uint)NewmapForm.Height;
                    }
                    m_CreateData.pGrid = null;
                    m_CreateData.pImageCalc = null;
                    DNSInitParams initParams = new DNSInitParams();
                    initParams.uNumBackgroundThreads = 0;
                    m_CreateData.pDevice = DNMcMapDevice.Create(initParams);
                                        
                    m_CreateData.pOverlayManager = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.CreateOverlayManager(ctrlOMGridCoordinateSystem.GridCoordinateSystem);
                    IDNMcOverlay newOverlay = DNMcOverlay.Create(m_CreateData.pOverlayManager);
                    MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.dOverlayManager_Overlay[m_CreateData.pOverlayManager] = newOverlay;
                   // MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlayManager = m_CreateData.pOverlayManager;
                    
                    ShowMap(NewmapForm);
                    
                    sw.Start();

                    DNMcMapViewport.Create(ref Viewport, ref Camera, m_CreateData, terrains);
                    NewmapForm.Viewport = Viewport;
                    NewmapForm.CreateEditMode(Viewport);
                    NewmapForm.Viewport.PerformPendingUpdates(DNEPendingUpdateType._EPUT_ANY_UPDATE);
                    for (int i = 0; i < m_Iteration; i++)
                    {
                        NewmapForm.Viewport.Render();
                    }
                    sw.Stop();

                    Manager_MCViewports.AddViewport(Viewport);

                    DNMcMapDevice.PerformPendingCalculations();
                    PrintResultToTextBox(((Button)sender).Text, "Create", m_Iteration);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Create", McEx);
                }
            }
            else
                MessageBox.Show("Must insert terrain path file name", "Empty file name", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowMap(MCTMapForm form)
        {
            form.Show();
        }

        private void PrintResultToTextBox(string testName, string funcName, int numIteration)
        {           
            //Calculate average FPS
            m_AverageFPS = (Stopwatch.Frequency / (sw.ElapsedTicks / numIteration));
            Stopwatch.StartNew();
                                            
            string[] resultText = new string[] 
                                    {
                                        "Test name:\t\t" + testName,
                                        "Function name:\t\t" + funcName,
                                        "Number of iterations:\t" + numIteration,
                                        "Total time (ms):\t\t" + sw.ElapsedMilliseconds,
                                        "Average time (ms):\t\t" + ((float)((float)sw.ElapsedMilliseconds / (float)numIteration)).ToString(),
                                        "Average FPS:\t\t" + m_AverageFPS, 
                                        "-------------------------------------------------------------------------"
                                    };

            lstPerformanceResult.Items.AddRange(resultText);
            lstPerformanceResult.TopIndex = lstPerformanceResult.Items.Count - 1;
            
            //Reset all timers
            m_AverageFPS = 0;
            sw.Reset();
            
        }        

        private bool IsViewportExist()
        {
            if (MCTester.Managers.MapWorld.MCTMapFormManager.MapForm != null)
            {
                activeViewport = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport;
                return true;
            }
            else
                return false;                

        }

        private void btnClearTextBox_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save content to a file\n before erasion?", "Clear List",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.Filter = "Word Document (*.doc)|*.*";
                SFD.FileName = "PerformanceTestResult" + DateTime.Today.Date.Day.ToString() + DateTime.Today.Date.Month.ToString() + DateTime.Today.Date.Year.ToString() + ".doc";
                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamWriter sWriter = new System.IO.StreamWriter(SFD.FileName);

                    foreach (object item in lstPerformanceResult.Items)
                        sWriter.WriteLine(item.ToString());

                    sWriter.Close();
                }
            }
             
            lstPerformanceResult.Items.Clear();
        }

        private void btnDrawObjLocationArea_Click(object sender, EventArgs e)
        {
            this.Hide();
            MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new MCTester.Managers.ObjectWorld.InitItemResultsEventArgs(InitItemResults);

            try
            {
                
                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];


                IDNMcObjectSchemeItem ObjSchemeItem = DNMcRectangleItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                    DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT,
                                                                                    DNERectangleDefinition._ERD_RECTANGLE_DIAGONAL_POINTS, 
                                                                                    0f,
                                                                                    0f,
                                                                                    DNELineStyle._ELS_SOLID,
                                                                                    DNSMcBColor.bcBlackOpaque,
                                                                                    3,
                                                                                    null,
																					new DNSMcFVector2D(0, -1),
                                                                                    1f,
                                                                                    DNEFillStyle._EFS_NONE,
                                                                                    DNSMcBColor.bcWhiteTransparent,
                                                                                    null,
                                                                                    new DNSMcFVector2D(1,1));

                

                RectAreaObj = DNMcObject.Create(activeOverlay,
                                                    ObjSchemeItem,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    locationPoints,
                                                    true);


                IDNMcEditMode m_EditMode = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditMode;
                m_EditMode.StartInitObject(RectAreaObj, ObjSchemeItem);

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
            }
        }

        public void InitItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            if (pItem.GetNodeType() == DNEObjectSchemeNodeType._RECTANGLE_ITEM)
            {
                SetObjectLoadingAreaPt(pObject);

                MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(null);
                MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent -= new MCTester.Managers.ObjectWorld.InitItemResultsEventArgs(InitItemResults);

                this.Show();                
            }
        }

        private bool IsBackAndForthTestType()
        {
            if (m_Iteration % 2 == 0)
                return true;
            else
            {
                MessageBox.Show("This test must gets even number \nBecause it perform the operation back and forth", "Number iteration test error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }            
        }

        private void dgvLoadObject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                OFD.RestoreDirectory = true;
                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    dgvLoadObject[0,e.RowIndex].Value = OFD.FileName;
                }
            }
        }

        private void SetObjectLoadingAreaPt(IDNMcObject obj)
        {
            DNSMcVector3D[] rectLocationPT = obj.GetLocationPoints(0);
            if (rectLocationPT[0].x <= rectLocationPT[1].x)
            {
                m_LocationArea_TL_X = rectLocationPT[0].x;
                m_LocationArea_BR_X = rectLocationPT[1].x;
            }
            else
            {
                m_LocationArea_TL_X = rectLocationPT[1].x;
                m_LocationArea_BR_X = rectLocationPT[0].x;
            }

            if (rectLocationPT[0].y >= rectLocationPT[1].y)
            {
                m_LocationArea_TL_Y = rectLocationPT[0].y;
                m_LocationArea_BR_Y = rectLocationPT[1].y;
            }
            else
            {
                m_LocationArea_TL_Y = rectLocationPT[1].y;
                m_LocationArea_BR_Y = rectLocationPT[0].y;
            }

            m_IsObjLoadingAreaExist = true;
        }
    }
}