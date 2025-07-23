using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.MapWorld;
using MCTester.Managers.ObjectWorld;
using System.Diagnostics;
using MCTester.General_Forms;
using MCTester.Managers.MapWorld;
using MapCore.Common;
using MCTester.Managers;
using MCTester.MapWorld.LoadingMapScheme;
using MCTester.Controls;
using MCTester.GUI.Forms;

namespace MCTester.GUI.Map
{
    public delegate void ClickOnMapEventArgs(Point PointOnMap, DNSMcVector3D PointIn3D, DNSMcVector3D PointInImage, bool IsHasIntersection);

    public partial class MCTMapForm : Form, IDNCameraUpdateCallback, IDNStereoRenderCallback , IDNAsyncQueryCallback
    {
        #region Private Member
        private IMapObject m_MapObject;
        private WinFormMapObject m_WinFormMap;
        private WpfMapObject m_WpfMap;
        private System.Windows.Interop.D3DImage mD3DImage;
        public static event ClickOnMapEventArgs OnMapClicked;

        public static string CSWBodyKey = DNMcConstants._MC_CSW_QUERY_BODY_KEY;
        public static string CSWBodyValue;

        private IDNMcMapViewport m_viewport;
        private IDNMcMapEnvironment m_environment;
        private Stopwatch FPS_Timer = new Stopwatch();
        private int FPS_RenderCounter = 0;
        private static Timer mRenderTimer;
        //private static Timer mStartRenderTimer;
        DateTime currTime;
        private long m_LastTime;
        private float m_RotationFactor = 0.01f;
        private float m_MovementSensitivity = 100f;
        private bool bRegisteredToVieportChangedEvent;
        private Point m_sPrevMousePoint = Point.Empty;
        private IDNMcEditMode m_EditMode;
        private EditmodeCallbackManager m_editModeManagerCallback;
        private StatusStrip m_statusBar;
        private Cursor[] m_GlobalMapCursorArr;
        private Cursor[] m_EditModeCursorArr;
        private bool m_RenderNeeded;
        public static RenderType eRender;
        public bool mIsInRender = false;
        public DNEStereoRenderMode m_eStereoMode = DNEStereoRenderMode._ESRM_NONE;
        public static Dictionary<uint, byte[]> m_dicBuffer;
        public static DNEStorageFormat m_eOMStorageFormat = DNEStorageFormat._ESF_MAPCORE_BINARY;
        public static DNEStorageFormat m_eOStorageFormat = DNEStorageFormat._ESF_MAPCORE_BINARY;
        float m_CamScale = 0;
        DNEMapType mapType;
        Point mLastMouse = new Point();
        private Point m_RotatePoint = new Point();
        private bool m_bUpdatingNeeded;
        private bool m_FootprintIsDefined = false;
        public static RenderNeededForm FrmRenderNeeded = null;
        private bool m_IsWpfMapWindow;
        private bool m_TesterMouseDownCatch = false;
       // private bool m_IsShiftPressed = false;
        IDNMcObject m_objRotatePoint;
        IDNMcObjectSchemeItem m_ObjSchemeRotatePointItem;
        private DNSMcVector3D m_RotationCenter = DNSMcVector3D.v3Zero;
        public static IDNMcObject mcFootprintObject;
        const double dZoom3DMinDistance = 10;
        const double dZoom3DMaxDistance = 2000000;

        const float dMinPitch = -90;
        const float dMaxPitch = 90;

        private bool m_holeAdded = false;

        public enum RenderType
        {
            FlagBaseRender = 0,
            RenderAll = 1,
            Render = 2,
            Manual = 3,
            PendingUpdatesBase = 4
        }

        #endregion

        public static List<DNSMcKeyStringValue> GetRequestParamsWithCSWBody(List<DNSMcKeyStringValue> RequestParams)
        {
            List<DNSMcKeyStringValue> tmpRequestParams = null;
            if (RequestParams == null)
                tmpRequestParams = new List<DNSMcKeyStringValue>();
            else
                tmpRequestParams = new List<DNSMcKeyStringValue>(RequestParams);

            if (!String.IsNullOrEmpty(CSWBodyValue) && !String.IsNullOrWhiteSpace(CSWBodyValue))
            {
                if (tmpRequestParams == null)
                {
                    tmpRequestParams = new List<DNSMcKeyStringValue>();
                }
                DNSMcKeyStringValue cswBody = new DNSMcKeyStringValue();
                cswBody.strKey = CSWBodyKey;
                cswBody.strValue = CSWBodyValue;
                tmpRequestParams.Add(cswBody);
            }
            return tmpRequestParams;
        }

        public MCTMapForm(bool IsWpfMapWindows)
        {
            InitializeComponent();
            this.MdiParent = Program.AppMainForm;
            m_IsWpfMapWindow = IsWpfMapWindows;
            
            bRegisteredToVieportChangedEvent = false;
            
            //this.FormClosing +=new FormClosingEventHandler(MCTMapForm_FormClosing);
            m_environment = null;
            m_statusBar = new StatusStrip();
                        
            mRenderTimer = new Timer();
            mRenderTimer.Interval = 60;  
            //mRenderTimer.Interval = 15; // sent render command every 0.25 second
            //mRenderTimer.Interval = 1;
            mRenderTimer.Tick += new EventHandler(mRenderTimer_Tick);
            // mRenderTimer.Start();  move to load function


            currTime = new DateTime();
            m_LastTime = 0;
                        
            MCTester.Footprints Footprints = new Footprints();

            m_GlobalMapCursorArr = new Cursor[3];
            m_GlobalMapCursorArr[(uint)DNECursorType._ECT_DEFAULT_CURSOR] = Cursors.Default;
            m_GlobalMapCursorArr[(uint)DNECursorType._ECT_DRAG_CURSOR] = Cursors.Hand;
            m_GlobalMapCursorArr[(uint)DNECursorType._ECT_RESIZE_CURSOR] = Cursors.SizeAll;

            m_EditModeCursorArr = new Cursor[4];
            m_EditModeCursorArr[(uint)DNECursorTypeEditMode._ECT_DEFAULT_CURSOR] = Cursors.Default;
            m_EditModeCursorArr[(uint)DNECursorTypeEditMode._ECT_DRAG_CURSOR] = Cursors.Hand;
            m_EditModeCursorArr[(uint)DNECursorTypeEditMode._ECT_MOVE_CURSOR] = Cursors.SizeAll;
            m_EditModeCursorArr[(uint)DNECursorTypeEditMode._ECT_EDIT_CURSOR] = Cursors.Cross;
                        
            m_dicBuffer = new Dictionary<uint, byte[]>(4);
            m_dicBuffer[1] = null;
            m_dicBuffer[2] = null;
            m_dicBuffer[3] = null;
            m_dicBuffer[4] = null;
            /*
             * key 1: Represent ObjectScheme buffer
             * key 2: Represent Object buffer
             * key 3: Represent Terrain buffer
             * key 4: Represent Layer buffer
            */

            if (m_IsWpfMapWindow == false)
            {
                WinFormMap = new WinFormMapObject(this);
                this.Text = "WinForm Window";
                m_MapObject = WinFormMap;
                WinFormMap.Dock = DockStyle.Fill;
                this.Controls.Add(WinFormMap);
            }
        }

        public MCTMapForm(int width, int height):this(false)
        {
            if(WinFormMap != null)
            {
                this.ClientSize = new Size(width, height);
            }
        }

        public void CheckRender()
        {
            try
            {
                if (eRender == RenderType.PendingUpdatesBase)
                {
                    if (m_IsWpfMapWindow == false)
                    {
                        MCTesterRender(Viewport);
                    }
                    else
                    {
                        RenderWPFMap(this);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                if (McEx.ErrorCode == DNEMcErrorCode.RENDERING_DEVICE_LOST)
                    Manager_MCViewports.dViewportRenderNeededFlag[Viewport] = true;
                else
                    Utilities.ShowErrorMessage("Render on paint window", McEx);
            }
        }

        void mRenderTimer_Tick(object sender, EventArgs e)
        {
            if (MainForm.mIsInAppTimer == false)
            {
                if (MainForm.CheckAllNativeServerLayersValidityAsyncCheckedState)
                {
                    try
                    {
                        DNMcMapLayer.CheckAllNativeServerLayersValidityAsync();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("CheckAllNativeServerLayersValidityAsync", McEx);
                    }
                }
                MainForm.mIsInAppTimer = true;
                if (Viewport != null)
                {
                    if (Manager_MCViewports.IsExistViewportInManager(Viewport))
                    {
                        if (!bRegisteredToVieportChangedEvent)
                        {
                            Viewport.AddCameraUpdateCallback(this);
                            bRegisteredToVieportChangedEvent = true;
                        }
                        RenderMap();
                    }
                }
                SetWindowName();
                MainForm.mIsInAppTimer = false;
            }
        }

        public float FrameStarted()
        {
            long now = currTime.Ticks;
            float lag = ((float)(now - m_LastTime))/1000.0f;
            m_LastTime = now;

            return lag;
        }
        
        public bool ControlKeyDown
        {
            get 
            {
                return (Control.ModifierKeys & Keys.Control) == Keys.Control;
            }            
        }

        public bool ShiftKeyDown
        {
            get
            {
                return (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
            }
        }

        public void RenderMap()
        {
            if (Viewport != null)
            {
                if (MCTester.Managers.Manager_MCGeneralDefinitions.HasPendingChanges == true)
                {
                    try
                    {
                        m_bUpdatingNeeded = Viewport.HasPendingUpdates(MCTester.Managers.Manager_MCGeneralDefinitions.UpdateTypeBitField);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("HasPendingUpdates", McEx);
                    }

                    Manager_StatusBar.UpdateMsg(m_bUpdatingNeeded.ToString());
                }



                if (mIsInRender == false)
                {
                    mIsInRender = true;

                    if (eRender == RenderType.RenderAll)
                    {
                        FPS_RenderCounter++;

                        if (FPS_RenderCounter >= 5 && FPS_Timer.ElapsedTicks > 0)
                        {
                            long FPS = (Stopwatch.Frequency / (long)(FPS_Timer.ElapsedTicks / 4));
                            Manager_StatusBar.UpdateAverageFPS(FPS);

                            FPS_RenderCounter = 1;
                            Stopwatch.StartNew();
                            FPS_Timer = new Stopwatch();
                        }

                        // Implement DNMcMapViewport.RenderAll() using Render() cause render work differently in WPF and WinForm windows
                        foreach (MCTMapForm mapForm in MCTMapFormManager.AllMapForms)
                        {
                            if (mapForm.Viewport != null)
                            {
                                try
                                {
                                    mapForm.Viewport.GetStereoMode(out m_eStereoMode);
                                    if (mapForm.m_IsWpfMapWindow == false)
                                    {
                                        FPS_Timer.Start();
                                        MCTesterRender(mapForm.Viewport);
                                        FPS_Timer.Stop();
                                    }
                                    else
                                    {
                                        RenderWPFMap(mapForm);
                                    }
                                }
                                catch (MapCoreException McEx)
                                {
                                    if (McEx.ErrorCode == DNEMcErrorCode.RENDERING_DEVICE_LOST)
                                        Manager_MCViewports.dViewportRenderNeededFlag[mapForm.Viewport] = true;
                                    else
                                        MapCore.Common.Utilities.ShowErrorMessage("RenderAll implemented by looping on Render() method", McEx);
                                }
                            }
                        }
                    }
                    else if (eRender == RenderType.Render)
                    {
                        mIsInRender = true;
                        FPS_RenderCounter++;

                        if (FPS_RenderCounter >= 5 && FPS_Timer.ElapsedTicks > 0)
                        {
                            long FPS = (Stopwatch.Frequency / (long)(FPS_Timer.ElapsedTicks / 4));
                            Manager_StatusBar.UpdateAverageFPS(FPS);

                            FPS_RenderCounter = 1;
                            Stopwatch.StartNew();
                            FPS_Timer = new Stopwatch();
                        }

                        try
                        {
                            Viewport.GetStereoMode(out m_eStereoMode);
                            if (m_IsWpfMapWindow == false)
                            {
                                FPS_Timer.Start();
                                MCTesterRender(Viewport);
                                FPS_Timer.Stop();
                            }
                            else
                            {
                                RenderWPFMap(this);
                            }
                        }
                        catch (MapCoreException McEx)
                        {
                            if (McEx.ErrorCode == DNEMcErrorCode.RENDERING_DEVICE_LOST)
                                Manager_MCViewports.dViewportRenderNeededFlag[Viewport] = true;
                            else
                                MapCore.Common.Utilities.ShowErrorMessage("Render", McEx);
                        }
                    }
                    else if (eRender == RenderType.FlagBaseRender)
                    {
                        mIsInRender = true;

                        foreach (MCTMapForm mapForm in MCTMapFormManager.AllMapForms)
                        {
                            bool isRenderFlag = Manager_MCViewports.dViewportRenderNeededFlag.Count > 0 && Manager_MCViewports.dViewportRenderNeededFlag.ContainsKey(mapForm.Viewport) && Manager_MCViewports.dViewportRenderNeededFlag[mapForm.Viewport] == true;
                            try
                            {
                                m_bUpdatingNeeded = mapForm.Viewport.HasPendingUpdates(DNEPendingUpdateType._EPUT_ANY_UPDATE);
                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("HasPendingUpdates", McEx);
                            }
                            if (isRenderFlag || m_bUpdatingNeeded)
                            {
                                // change specific viewport flag to False
                                Manager_MCViewports.dViewportRenderNeededFlag[mapForm.Viewport] = false;

                                FPS_RenderCounter++;

                                if (FPS_RenderCounter >= 5 && FPS_Timer.ElapsedTicks > 0)
                                {
                                    long FPS = (Stopwatch.Frequency / (long)(FPS_Timer.ElapsedTicks / 4));
                                    Manager_StatusBar.UpdateAverageFPS(FPS);

                                    FPS_RenderCounter = 1;
                                    Stopwatch.StartNew();
                                    FPS_Timer = new Stopwatch();
                                }

                                try
                                {
                                    mapForm.Viewport.GetStereoMode(out m_eStereoMode);
                                    if (mapForm.m_IsWpfMapWindow == false)
                                    {
                                        FPS_Timer.Start();
                                        MCTesterRender(mapForm.Viewport);
                                        FPS_Timer.Stop();
                                    }
                                    else
                                    {
                                        RenderWPFMap(mapForm);
                                    }
                                }
                                catch (MapCoreException McEx)
                                {
                                    if (McEx.ErrorCode == DNEMcErrorCode.RENDERING_DEVICE_LOST)
                                        Manager_MCViewports.dViewportRenderNeededFlag[mapForm.Viewport] = true;
                                    else
                                        MapCore.Common.Utilities.ShowErrorMessage("FlagBaseRender", McEx);
                                }
                            }
                        }
                    }
                    else if (eRender == RenderType.PendingUpdatesBase)
                    {
                        try
                        {
                            m_bUpdatingNeeded = false;
                            try
                            {
                                m_bUpdatingNeeded = Viewport.HasPendingUpdates(DNEPendingUpdateType._EPUT_ANY_UPDATE);
                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("HasPendingUpdates", McEx);
                            }
                            if (m_bUpdatingNeeded == true)
                            {
                                Viewport.GetStereoMode(out m_eStereoMode);

                                FPS_RenderCounter++;

                                if (FPS_RenderCounter >= 5 && FPS_Timer.ElapsedTicks > 0)
                                {
                                    long FPS = (Stopwatch.Frequency / (long)(FPS_Timer.ElapsedTicks / 4));
                                    Manager_StatusBar.UpdateAverageFPS(FPS);

                                    FPS_RenderCounter = 1;
                                    Stopwatch.StartNew();
                                    FPS_Timer = new Stopwatch();
                                }

                                if (m_IsWpfMapWindow == false)
                                {
                                    FPS_Timer.Start();
                                    MCTesterRender(Viewport);
                                    FPS_Timer.Stop();
                                }
                                else
                                {
                                    RenderWPFMap(this);
                                }
                            }
                        }
                        catch (MapCoreException McEx)
                        {
                            if (McEx.ErrorCode == DNEMcErrorCode.RENDERING_DEVICE_LOST)
                                Manager_MCViewports.dViewportRenderNeededFlag[Viewport] = true;
                            else
                                Utilities.ShowErrorMessage("FlagBaseRender", McEx);
                        }
                    }
                    mIsInRender = false;
                }

                if (MCTester.Managers.Manager_MCGeneralDefinitions.GetRenderStatistics == true)
                {
                    if (Viewport != null)
                    {
                        try
                        {
                            DNSRenderStatistics statistics = Viewport.GetRenderStatistics();
                            Manager_StatusBar.UpdateStatisticsStatusBar(statistics);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("GetRenderStatistics", McEx);
                        }
                    }
                }
            }
        }

        private void MCTesterRender(IDNMcMapViewport viewport)
        {
            if(viewport != null)
			    viewport.Render();
        }

        private void RenderWPFMap(MCTMapForm currMapForm)
        {
            if (currMapForm.WpfMap != null)
            {
                mD3DImage = currMapForm.WpfMap.D3DImage;

                // lock the D3DImage
                mD3DImage.Lock();

                FPS_Timer.Start();

                // Force Render the map.

                MCTesterRender(currMapForm.Viewport);

                FPS_Timer.Stop();

                // invalidate the updated region of the D3DImage (in this case, the whole image)
                mD3DImage.AddDirtyRect(new System.Windows.Int32Rect(0, 0, (int)mD3DImage.PixelWidth, (int)mD3DImage.PixelHeight));

                // unlock the D3DImage
                mD3DImage.Unlock();
            }            
        }

        #region Public Methods
        public void MoveCamera(int deltaX, int deltaY, int deltaZ)
        {
            DNSMcVector3D DeltaPosition = new DNSMcVector3D(deltaX, deltaY, deltaZ);

            try
            {
                if (Viewport == null)
                {
                    return;
                }
                if (Viewport.MapType == DNEMapType._EMT_2D)
                {
                    Viewport.MoveCameraRelativeToOrientation(DeltaPosition);
                }
                if (Viewport.MapType == DNEMapType._EMT_3D)
                {
                    Viewport.MoveCameraRelativeToOrientation(DeltaPosition, false);
                }

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_viewport.OverlayManager);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("MoveCameraRelativeToOrientation", McEx);
            }
        }

        #endregion

        #region IDNCameraUpdateCallback Members

        public void OnActiveCameraUpdated(IDNMcMapViewport pViewport)
        {
            //System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + "- camera updated!");
        }

        #endregion

        #region IDNStereoRenderCallback Members

        public void OnEyeFrameStarted(IDNMcMapViewport pViewport, bool bLeftEye)
        {
        }

        public void OnEyeFrameFinished(IDNMcMapViewport pViewport, bool bLeftEye)
        {

        }

        #endregion

        #region Events

        public void ViewportOnMouseEventCall(DNEMouseEvent mouseEvent, DNSMcPoint point)
        {
            try
            {
                DNECursorType CursorType;
                m_viewport.OnMouseEvent(mouseEvent,
                                        point,
                                        out m_RenderNeeded,
                                        out CursorType);

                if (m_RenderNeeded == true)
				{
					this.Cursor = m_GlobalMapCursorArr[(uint)CursorType];

                    // turn on all viewports render needed flags
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_viewport);
                }

                if (FrmRenderNeeded != null)
                    FrmRenderNeeded.AddRenderToList(m_RenderNeeded);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("OnMouseEvent", McEx);
            }
        }

        public void EditModeOnMouseEventCall(DNEMouseEventEditMode mouseEventEditMode, DNSMcPoint point, short wheelDelta)
        {
            try
            {
                DNECursorTypeEditMode CursorType;
                m_EditMode.OnMouseEvent(mouseEventEditMode,
                                        point,
                                        ControlKeyDown,
                                        wheelDelta,
                                        out m_RenderNeeded,
                                        out CursorType);


                // only if edit mode operation was happened and it is oneOperationOnly
                if (m_RenderNeeded == true)
                {
                    if (EditModePropertiesBase.OneOperationOnly == true || EditMode.IsEditingActive == false)
                        Program.AppMainForm.EditModeNavigationButtonState = false;                        
                    
                    this.Cursor = m_EditModeCursorArr[(uint)CursorType];

                    // turn on all viewports render needed flags
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_viewport.OverlayManager);
                }
                
                if (FrmRenderNeeded != null)
                    FrmRenderNeeded.AddRenderToList(m_RenderNeeded);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("OnMouseEvent", McEx);
            }       
        }

        public void EditModeOnKeyEventCall(DNEKeyEvent keyEvent)
        {
            try
            {
                m_EditMode.OnKeyEvent(keyEvent, out m_RenderNeeded);

                // only if edit mode operation was happened and it is oneOperationOnly
                if (m_RenderNeeded == true)
                {
                    if (EditModePropertiesBase.OneOperationOnly == true || EditMode.IsEditingActive == false)
                        Program.AppMainForm.EditModeNavigationButtonState = false;
                    
                    // turn on all viewports render needed flags
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_viewport.OverlayManager);
                }

                if (FrmRenderNeeded != null)
                    FrmRenderNeeded.AddRenderToList(m_RenderNeeded);
            }
            catch (MapCoreException McEx)
            {
            	Utilities.ShowErrorMessage("OnKeyEvent", McEx);
            }
        }

        private void MCTMapForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Viewport != null)
            {
                if (bRegisteredToVieportChangedEvent)
                {
                    try
                    {
                        Viewport.RemoveCameraUpdateCallback(this);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("RemoveCameraUpdateCallback", McEx);
                    }
                    bRegisteredToVieportChangedEvent = false;
                }

                List<btnPrintForm> printFormsToClose = new List<btnPrintForm>(); ;
                if (MainForm.BtnPrintForms.Count > 0)
                {
                    foreach (btnPrintForm printForm in MainForm.BtnPrintForms)
                    {
                        if (printForm.GetActiveViewport() == Viewport)
                        {
                            printFormsToClose.Add(printForm);
                        }
                    }
                    foreach (btnPrintForm printForm in printFormsToClose)
                    {
                        MainForm.BtnPrintForms.Remove(printForm);
                        printForm.Close();
                    }
                }
                List<SpatialQueriesForm> spatialQueriesFormToClose = new List<SpatialQueriesForm>(); 
                if (Manager_MCLayers.ListSpatialQueriesForms.Count > 0)
                {
                    foreach (SpatialQueriesForm form in Manager_MCLayers.ListSpatialQueriesForms)
                    {
                        if (form.GetActiveViewport() == Viewport)
                        {
                            spatialQueriesFormToClose.Add(form);
                        }
                    }
                    foreach (SpatialQueriesForm form in spatialQueriesFormToClose)
                    {
                        Manager_MCLayers.ListSpatialQueriesForms.Remove(form);
                        form.Close();
                    }
                }

                

                MCTMapFormManager.RemoveMapForm(this);

                Manager_MCViewports.RemoveOnlyViewport(Viewport);
                RemoveViewport();

                MainForm.RebuildMapWorldTree();

            }
        }

        private void MCTMapForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        Dictionary<IDNMcOverlayManager, int> lstOverlayManager = new Dictionary<IDNMcOverlayManager, int>();
        List<int> ViewportIDs = new List<int>();

        private void AddAllMapFormsToScheme_Click(object sender, EventArgs e) { }

        public void AddAllMapFormsToScheme()
        {
            lstOverlayManager.Clear();
            ViewportIDs.Clear();
            frmAddMapToSchemeDetails frmAddMapToSchemeDetails = new frmAddMapToSchemeDetails();

            if (frmAddMapToSchemeDetails.ShowDialog() == DialogResult.OK)
            {
                MCTMapForm[] mapForms = MCTMapFormManager.AllMapForms;
                foreach(MCTMapForm mapForm in mapForms)
                {
                    this.Cursor = Cursors.WaitCursor;
                    int overlayManagerId = AddMapFormToScheme(mapForm, frmAddMapToSchemeDetails.OverlayManagerName, 0);
                    int schemeId = MainForm.MapLoaderDefinitionManager.Schemas.GetNextID();

                    MCTMapSchema currSchema = new MCTMapSchema();

                    currSchema.ID = schemeId;
                    currSchema.Area = frmAddMapToSchemeDetails.SchemeAreaTxt;
                    currSchema.MapType = frmAddMapToSchemeDetails.SchemeMapTypeTxt;
                    currSchema.Comments = frmAddMapToSchemeDetails.OverlayManagerName;

                    currSchema.ViewportsIDs = ViewportIDs;

                    currSchema.OverlayManagerID = overlayManagerId;
                    MainForm.MapLoaderDefinitionManager.Schemas.Schema.Add(currSchema);
                  
                }
                MapLoaderDefinitionForm.SaveFormDataToFile();
                this.Cursor = Cursors.Default;
                MessageBox.Show("Success add all open maps to schemes", "Add all open maps to schemes");
            }
        }

        private int AddMapFormToScheme(MCTMapForm mapForm, string OverlayManagerName, int viewport_index)
        {
            IDNMcMapViewport viewport = mapForm.Viewport;
            int gridCoordinateSystemId = GetCoordSysID(viewport.CoordinateSystem);
            int overlayManagerId = 0 ;
            DNSCreateDataMV createDataMV = mapForm.Viewport.GetCreateParams();

            if (viewport.OverlayManager != null)
            {
                if (lstOverlayManager.ContainsKey(viewport.OverlayManager))
                {
                    overlayManagerId = lstOverlayManager[viewport.OverlayManager];
                }
                else
                {
                    overlayManagerId = MainForm.MapLoaderDefinitionManager.OverlayManagers.GetNextId();

                    MCTOverlayManager currOverlayManager = new MCTOverlayManager();
                    currOverlayManager.ID = overlayManagerId;
                    currOverlayManager.CoordSysID = GetCoordSysID(viewport.OverlayManager.GetCoordinateSystemDefinition(), viewport.CoordinateSystem, gridCoordinateSystemId);
                    currOverlayManager.Name = OverlayManagerName;
                    MainForm.MapLoaderDefinitionManager.OverlayManagers.OverlayManager.Add(currOverlayManager);
                    lstOverlayManager.Add(viewport.OverlayManager, overlayManagerId);
                }
            }

            List<int> TerrainsIDs = new List<int>();
            IDNMcMapTerrain[] terrains = viewport.GetTerrains();

            int terrain_index = 0;
            int layer_index = 0;

            foreach (IDNMcMapTerrain terrain in terrains)
            {
                List<int> LayersIDs = new List<int>();
                IDNMcMapLayer[] layers = terrain.GetLayers();
                
                foreach (IDNMcMapLayer layer in layers)
                {
                    MCTMapLayer currLayer = CreateMCTMapLayerFromLayer(layer, layer_index, gridCoordinateSystemId, viewport.CoordinateSystem);
                    if(currLayer != null)
                        LayersIDs.Add(currLayer.ID);
                    
                    layer_index++;
                }

                int terrainId = MainForm.MapLoaderDefinitionManager.Terrains.GetTerrainLayerID();
                MCTMapTerrain currTerrain = new MCTMapTerrain();

                currTerrain.LayersIDs = LayersIDs;

                currTerrain.ID = terrainId;
                currTerrain.Name = "MCTester terrain" + terrain_index;
                currTerrain.CoordSysID = gridCoordinateSystemId;
                TerrainsIDs.Add(terrainId);
                MainForm.MapLoaderDefinitionManager.Terrains.Terrain.Add(currTerrain);

                terrain_index++;
            }

            int viewportId = MainForm.MapLoaderDefinitionManager.Viewports.GetNextId();
            MCTMapViewport currMCTMapViewport = new MCTMapViewport();

            currMCTMapViewport.ID = viewportId;
            currMCTMapViewport.Name = "MCTester viewport" + viewport_index;
            currMCTMapViewport.MapType = viewport.MapType;
            currMCTMapViewport.ShowGeoInMetricProportion = createDataMV.bShowGeoInMetricProportion;
            currMCTMapViewport.TerrainObjectBestResolution = createDataMV.fTerrainObjectsBestResolution;
            currMCTMapViewport.DtmUsageAndPrecision = createDataMV.eDtmUsageAndPrecision.ToString();

            currMCTMapViewport.TerrainsIDs = TerrainsIDs;
            currMCTMapViewport.CameraPosition = viewport.GetCameraPosition();
            currMCTMapViewport.CoordSysID = gridCoordinateSystemId;

         /*   IDNMcImageCalc mcImageCalc = viewport.GetImageCalc();
            if(mcImageCalc != null && mcImageCalc.ImageType == DNEImageType._EIT_FRAME)
            {
                int ID = MainForm.MapLoaderDefinitionManager.ImageCalcs.GetNextImageCalcID();
                MCTImageCalc mctImageCalc = new MCTImageCalc();
                mctImageCalc.ID = ID;
                mctImageCalc.ImageType = mcImageCalc.ImageType.ToString();
                mctImageCalc.CoordSysID = MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.GetCoordSysID(mcImageCalc.GridCoordSys);

                IDNMcFrameImageCalc frameImageCalc = (IDNMcFrameImageCalc)mcImageCalc;
                DNSCameraParams cameraParams = frameImageCalc.GetCameraParams();
                //  mctImageCalc.DTMMapLayerID = CreateMCTMapLayerFromLayer(cameraParams.)
                mctImageCalc.Location = cameraParams.CameraPosition;
                mctImageCalc.OffsetCenterPixelX = cameraParams.dOffsetCenterPixelX;
                mctImageCalc.OffsetCenterPixelY = cameraParams.dOffsetCenterPixelY;
                mctImageCalc.OpeningAngleX = cameraParams.dCameraOpeningAngleX;
                mctImageCalc.Pitch = cameraParams.dCameraPitch;
                mctImageCalc.PixelRatio = cameraParams.dPixelRatio;
                mctImageCalc.PixelsNumX = cameraParams.nPixelesNumX;
                mctImageCalc.PixelsNumY = cameraParams.nPixelesNumY;
                mctImageCalc.Roll = cameraParams.dCameraRoll;
                mctImageCalc.Yaw = cameraParams.dCameraYaw;

                MainForm.MapLoaderDefinitionManager.ImageCalcs.ImageCalc.Add(mctImageCalc);
                currMCTMapViewport.ImageCalcID = ID;
            }
            */
            ViewportIDs.Add(viewportId);
            MainForm.MapLoaderDefinitionManager.Viewports.Viewport.Add(currMCTMapViewport);
            //return ViewportIDs;
            return overlayManagerId;
        }

        private MCTMapLayer CreateMCTMapLayerFromLayer(IDNMcMapLayer layer, int layer_index, int vpGridCoordinateSystemId, IDNMcGridCoordinateSystem vpGridCoordinateSystem)
        {
            MCTMapLayer currLayer = new MCTMapLayer();
            int ID = MainForm.MapLoaderDefinitionManager.Layers.GetNextLayerID();
            currLayer.ID = ID;
            currLayer.Name = "MCTester layer " + layer_index;
            currLayer.IsSaveFromMap = true;
            currLayer.LayerType = layer.LayerType;
            currLayer.CoordSysID = vpGridCoordinateSystemId;
            currLayer.MapLayerReadCallback = (MCTMapLayerReadCallback)layer.GetCallback();
            if (MCTMapDevice.IsInitilizeDeviceLocalCache())
                try
                {
                    currLayer.LocalCacheLayerParams = layer.GetLocalCacheLayerParams();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetLocalCacheLayerParams", McEx);
                }

            try
            {
                currLayer.Path = layer.GetDirectory();
                uint puFirstLowerQualityLevel;
                bool pbThereAreMissingFiles;
                uint puNumLevelsToIgnore;
                float pfTargetHighestResolution;
                bool pbEnhanceBorderOverlap;
                float pfExtrusionHeightMaxAddition;
                bool pbOrthometricHeights;
                string pstrRawDataDirectory;
                string pstrIndexingDataDirectory;
                bool pbNonDefaultIndexingDataDirectory;
                IDNMcGridCoordinateSystem ppTargetCoordinateSystem;
                DNSTilingScheme ppTilingScheme;
                DNSMcKeyStringValue[] paRequestParams;
                DNSRawParams rawParams;
                DNSMcVector3D PositionOffset;

                bool bImageCoordinateSystem;
                switch (layer.LayerType)
                {
                    case DNELayerType._ELT_NATIVE_DTM:
                        (layer as IDNMcNativeDtmMapLayer).GetCreateParams(out puNumLevelsToIgnore);
                        currLayer.NumLevelsToIgnore = puNumLevelsToIgnore;
                        break;
                    case DNELayerType._ELT_NATIVE_RASTER:
                        (layer as IDNMcNativeRasterMapLayer).GetCreateParams(out puFirstLowerQualityLevel,
                            out pbThereAreMissingFiles,
                            out puNumLevelsToIgnore,
                            out pbEnhanceBorderOverlap);
                        currLayer.FirstLowerQualityLevel = puFirstLowerQualityLevel;
                        currLayer.ThereAreMissingFiles = pbThereAreMissingFiles;
                        currLayer.NumLevelsToIgnore = puNumLevelsToIgnore;
                        currLayer.EnhanceBorderOverlap = pbEnhanceBorderOverlap;
                        break;
                    case DNELayerType._ELT_NATIVE_3D_MODEL:
                        (layer as IDNMcNative3DModelMapLayer).GetCreateParams(out puNumLevelsToIgnore);
                        currLayer.NumLevelsToIgnore = puNumLevelsToIgnore;
                        break;
                    case DNELayerType._ELT_NATIVE_VECTOR_3D_EXTRUSION:
                        (layer as IDNMcNativeVector3DExtrusionMapLayer).GetCreateParams(out puNumLevelsToIgnore, out pfExtrusionHeightMaxAddition);
                        currLayer.NumLevelsToIgnore = puNumLevelsToIgnore;
                        currLayer.ExtrusionHeightMaxAddition = pfExtrusionHeightMaxAddition;
                        break;
                    case DNELayerType._ELT_NATIVE_HEAT_MAP:
                        (layer as IDNMcNativeHeatMapLayer).GetCreateParams(out puFirstLowerQualityLevel,
                            out pbThereAreMissingFiles,
                            out puNumLevelsToIgnore,
                            out pbEnhanceBorderOverlap);
                        currLayer.FirstLowerQualityLevel = puFirstLowerQualityLevel;
                        currLayer.ThereAreMissingFiles = pbThereAreMissingFiles;
                        currLayer.NumLevelsToIgnore = puNumLevelsToIgnore;
                        currLayer.EnhanceBorderOverlap = pbEnhanceBorderOverlap;
                        break;
                    case DNELayerType._ELT_NATIVE_MATERIAL:
                        (layer as IDNMcNativeMaterialMapLayer).GetCreateParams(out pbThereAreMissingFiles);
                        currLayer.ThereAreMissingFiles = pbThereAreMissingFiles;
                        break;
                    case DNELayerType._ELT_NATIVE_TRAVERSABILITY:
                        (layer as IDNMcNativeTraversabilityMapLayer).GetCreateParams(out pbThereAreMissingFiles);
                        currLayer.ThereAreMissingFiles = pbThereAreMissingFiles;
                        break;
                    case DNELayerType._ELT_NATIVE_VECTOR:
                    case DNELayerType._ELT_NATIVE_SERVER_DTM:
                    case DNELayerType._ELT_NATIVE_SERVER_RASTER:
                    case DNELayerType._ELT_NATIVE_SERVER_TRAVERSABILITY:
                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR:
                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR_3D_EXTRUSION:
                    case DNELayerType._ELT_NATIVE_SERVER_3D_MODEL:
                        break;
                    case DNELayerType._ELT_RAW_RASTER:
                        (layer as IDNMcRawRasterMapLayer).GetCreateParams(out rawParams, out bImageCoordinateSystem);
                        currLayer.RasterImageCoordinateSystem = bImageCoordinateSystem;
                        SetRawParams(currLayer, rawParams, vpGridCoordinateSystem, vpGridCoordinateSystemId);
                        break;
                    case DNELayerType._ELT_RAW_DTM:
                        (layer as IDNMcRawDtmMapLayer).GetCreateParams(out rawParams);
                        SetRawParams(currLayer, rawParams, vpGridCoordinateSystem, vpGridCoordinateSystemId);
                        break;
                    case DNELayerType._ELT_RAW_VECTOR:
                        DNSRawVectorParams pRawVectorParams;

                        (layer as IDNMcRawVectorMapLayer).GetCreateParams(out pRawVectorParams, out ppTargetCoordinateSystem, out ppTilingScheme);
                        if (ppTargetCoordinateSystem != null)
                            currLayer.TargetCoordinateSystemID = GetCoordSysID(ppTargetCoordinateSystem, vpGridCoordinateSystem, vpGridCoordinateSystemId);
                        currLayer.TilingScheme = ppTilingScheme;
                        currLayer.RVStrPointTextureFile = pRawVectorParams.strPointTextureFile;
                        currLayer.RVStrLocaleStr = pRawVectorParams.strLocaleStr;
                        currLayer.RVAutoStylingType = pRawVectorParams.eAutoStylingType;
                        currLayer.RVClipRect = pRawVectorParams.pClipRect;
                        currLayer.RVMaxNumVerticesPerTile = pRawVectorParams.uMaxNumVerticesPerTile;
                        currLayer.RVMaxNumVisiblePointObjectsPerTile = pRawVectorParams.uMaxNumVisiblePointObjectsPerTile;
                        currLayer.RVMaxScale = pRawVectorParams.fMaxScale;
                        currLayer.RVMinPixelSizeForObjectVisibility = pRawVectorParams.uMinPixelSizeForObjectVisibility;
                        currLayer.RVMinScale = pRawVectorParams.fMinScale;
                        currLayer.RVOptimizationMinScale = pRawVectorParams.fOptimizationMinScale;
                        currLayer.RVSimplificationTolerance = pRawVectorParams.dSimplificationTolerance;
                        currLayer.SourceCoordinateSystemID = GetCoordSysID(pRawVectorParams.pSourceCoordinateSystem, vpGridCoordinateSystem, vpGridCoordinateSystemId);
                        currLayer.RVStrCustomStylingFolder = pRawVectorParams.strCustomStylingFolder;
                        currLayer.RVStrDataSource = pRawVectorParams.strDataSource;
                        break;
                    case DNELayerType._ELT_RAW_3D_MODEL:
                        DNSMcBox? ppClipRect;
                        (layer as IDNMcRaw3DModelMapLayer).GetCreateParams(
                            out pstrRawDataDirectory, out pbOrthometricHeights, out puNumLevelsToIgnore,
                            out ppTargetCoordinateSystem, out ppClipRect,
                            out pfTargetHighestResolution,out pstrIndexingDataDirectory,
                            out pbNonDefaultIndexingDataDirectory, out paRequestParams, out PositionOffset);
                        currLayer.Path = pstrRawDataDirectory;
                        currLayer.R3DOrthometricHeights = pbOrthometricHeights;
                        currLayer.NumLevelsToIgnore = puNumLevelsToIgnore;
                        currLayer.RSOIndexingDataDir = pstrIndexingDataDirectory;
                        currLayer.RSONonDefaultIndexingDataDir = pbNonDefaultIndexingDataDirectory;
                        currLayer.CoordSysID = GetCoordSysID(ppTargetCoordinateSystem, vpGridCoordinateSystem, vpGridCoordinateSystemId);
                        currLayer.R3DClipRect = ppClipRect;
                        currLayer.TargetHighestResolution = pfTargetHighestResolution;
                        currLayer.WSRequestParams = paRequestParams;
                        currLayer.IsUseIndexing = pstrIndexingDataDirectory != null && pstrIndexingDataDirectory != "";
                        currLayer.R3DPositionOffset = PositionOffset;
                        break;
                    case DNELayerType._ELT_RAW_VECTOR_3D_EXTRUSION:
                        DNSRawVector3DExtrusionParams pRawVector3DExtrusionParams;
                        (layer as IDNMcRawVector3DExtrusionMapLayer).GetCreateParams(
                            out pRawVector3DExtrusionParams, out pfExtrusionHeightMaxAddition, out pstrIndexingDataDirectory, out pbNonDefaultIndexingDataDirectory);
                        currLayer.RV3DaSpecificTextures = pRawVector3DExtrusionParams.aSpecificTextures;
                        currLayer.RV3DClipRect = pRawVector3DExtrusionParams.pClipRect;
                        currLayer.RV3DRoofDefaultTexture = pRawVector3DExtrusionParams.RoofDefaultTexture;
                        currLayer.RV3DSideDefaultTexture = pRawVector3DExtrusionParams.SideDefaultTexture;
                        currLayer.RV3DStrDataSource = pRawVector3DExtrusionParams.strDataSource;
                        currLayer.RV3DStrHeightColumn = pRawVector3DExtrusionParams.strHeightColumn;
                        currLayer.RV3DStrObjectIDColumn = pRawVector3DExtrusionParams.strObjectIDColumn;
                        currLayer.RV3DStrRoofTextureIndexColumn = pRawVector3DExtrusionParams.strRoofTextureIndexColumn;
                        currLayer.RV3DStrSideTextureIndexColumn = pRawVector3DExtrusionParams.strSideTextureIndexColumn;
                        currLayer.TilingScheme = pRawVector3DExtrusionParams.pTilingScheme;
                        currLayer.SourceCoordinateSystemID = GetCoordSysID(pRawVector3DExtrusionParams.pSourceCoordinateSystem, vpGridCoordinateSystem, vpGridCoordinateSystemId);
                        currLayer.TargetCoordinateSystemID = GetCoordSysID(pRawVector3DExtrusionParams.pTargetCoordinateSystem, vpGridCoordinateSystem, vpGridCoordinateSystemId);

                        currLayer.ExtrusionHeightMaxAddition = pfExtrusionHeightMaxAddition;
                        currLayer.RSOIndexingDataDir = pstrIndexingDataDirectory;
                        currLayer.RSONonDefaultIndexingDataDir = pbNonDefaultIndexingDataDirectory;
                        break;
                    case DNELayerType._ELT_WEB_SERVICE_DTM:
                        IDNMcWebServiceDtmMapLayer webServiceDtmMapLayer = (layer as IDNMcWebServiceDtmMapLayer);
                        DNEWebMapServiceType webMapServiceDTMType = webServiceDtmMapLayer.GetWebMapServiceType();
                        currLayer.WSWebMapServiceType = webMapServiceDTMType;

                        switch (webMapServiceDTMType)
                        {
                            case DNEWebMapServiceType._EWMS_WMS:
                                DNSWMSParams mcWMSParams = webServiceDtmMapLayer.GetWMSParams();
                                int coordSysID1 = GetCoordSysID(mcWMSParams.pCoordinateSystem, vpGridCoordinateSystem, vpGridCoordinateSystemId);
                                currLayer.SetWMSParams(mcWMSParams, coordSysID1);
                                break;
                            case DNEWebMapServiceType._EWMS_WMTS:
                                DNSWMTSParams mcWMTSParams = webServiceDtmMapLayer.GetWMTSParams();
                                int coordSysID2 = GetCoordSysID(mcWMTSParams.pCoordinateSystem, vpGridCoordinateSystem, vpGridCoordinateSystemId);
                                currLayer.SetWMTSParams(mcWMTSParams, coordSysID2);
                                break;
                            case DNEWebMapServiceType._EWMS_WCS:
                                DNSWCSParams mcWCSParams = webServiceDtmMapLayer.GetWCSParams();
                                int coordSysID3 = GetCoordSysID(mcWCSParams.pCoordinateSystem, vpGridCoordinateSystem, vpGridCoordinateSystemId);
                                currLayer.SetWCSParams(mcWCSParams, coordSysID3);
                                break;
                        }
                        break;
                    case DNELayerType._ELT_WEB_SERVICE_RASTER:
                        IDNMcWebServiceRasterMapLayer webServiceRasterMapLayer = (layer as IDNMcWebServiceRasterMapLayer);
                        DNEWebMapServiceType webMapServiceRasterType = webServiceRasterMapLayer.GetWebMapServiceType();
                        currLayer.WSWebMapServiceType = webMapServiceRasterType;

                        switch (webMapServiceRasterType)
                        {
                            case DNEWebMapServiceType._EWMS_WMS:
                                DNSWMSParams mcWMSParams = webServiceRasterMapLayer.GetWMSParams();
                                int coordSysID1 = GetCoordSysID(mcWMSParams.pCoordinateSystem, vpGridCoordinateSystem, vpGridCoordinateSystemId);
                                currLayer.SetWMSParams(mcWMSParams, coordSysID1);
                                break;
                            case DNEWebMapServiceType._EWMS_WMTS:
                                DNSWMTSParams mcWMTSParams = webServiceRasterMapLayer.GetWMTSParams();
                                int coordSysID2 = GetCoordSysID(mcWMTSParams.pCoordinateSystem, vpGridCoordinateSystem, vpGridCoordinateSystemId);
                                currLayer.SetWMTSParams(mcWMTSParams, coordSysID2);
                                break;
                            case DNEWebMapServiceType._EWMS_WCS:
                                DNSWCSParams mcWCSParams = webServiceRasterMapLayer.GetWCSParams();
                                int coordSysID3 = GetCoordSysID(mcWCSParams.pCoordinateSystem, vpGridCoordinateSystem, vpGridCoordinateSystemId);
                                currLayer.SetWCSParams(mcWCSParams, coordSysID3);
                                break;
                        }
                        break;
                }
                MainForm.MapLoaderDefinitionManager.Layers.Layer.Add(currLayer);
                return currLayer;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetDirectory()/GetCreateParams()", McEx);
            }
            return null;
        }

        private void AddMapToScheme_Click(object sender, EventArgs e) { }

        public void AddMapToScheme()
        {
            lstOverlayManager.Clear();
            ViewportIDs.Clear();

            frmAddMapToSchemeDetails frmAddMapToSchemeDetails = new frmAddMapToSchemeDetails();

            if (frmAddMapToSchemeDetails.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                int overlayManagerId = AddMapFormToScheme(MCTMapFormManager.MapForm, frmAddMapToSchemeDetails.OverlayManagerName, 0);
                int schemeId = MainForm.MapLoaderDefinitionManager.Schemas.GetNextID();

                MCTMapSchema currSchema = new MCTMapSchema();

                currSchema.ID = schemeId;
                currSchema.Area = frmAddMapToSchemeDetails.SchemeAreaTxt;
                currSchema.MapType = frmAddMapToSchemeDetails.SchemeMapTypeTxt;
                currSchema.Comments = frmAddMapToSchemeDetails.OverlayManagerName;

                currSchema.ViewportsIDs = ViewportIDs;

                currSchema.OverlayManagerID = overlayManagerId;
                MainForm.MapLoaderDefinitionManager.Schemas.Schema.Add(currSchema);
                MainForm.MapLoaderDefinitionManager.MapDevices.Device[0] = MCTMapDevice.CurrDevice;

                MapLoaderDefinitionForm.SaveFormDataToFile();

                lstOverlayManager.Clear();
                this.Cursor = Cursors.Default;
                MessageBox.Show("Success add current map to schemes", "Add Map To Scheme");
            }
        }

        private void SetRawParams(MCTMapLayer currLayer, DNSRawParams pParams, IDNMcGridCoordinateSystem gridCoordinateSystem, int coordId)
        {
            currLayer.SetRawParams(pParams, GetCoordSysID(pParams.pCoordinateSystem, gridCoordinateSystem, coordId));
        }

        private int GetCoordSysID(IDNMcGridCoordinateSystem gridCoordinateSystem, IDNMcGridCoordinateSystem vpGridCoordinateSystem, int coordId)
        {
            int gridCoordID = 0;

            if (gridCoordinateSystem == null || gridCoordinateSystem == vpGridCoordinateSystem || gridCoordinateSystem.IsEqual(vpGridCoordinateSystem))
                gridCoordID = coordId;
            else
            {
                gridCoordID = GetCoordSysID(gridCoordinateSystem);
            }
            return gridCoordID;
        }

        private int GetCoordSysID(IDNMcGridCoordinateSystem gridCoordinateSystem)
        {
            int gridCoordID = 0;
            gridCoordID = MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.GetCoordSysID(gridCoordinateSystem);
            if (gridCoordID == -1)
            {
                MCTGridCoordinateSystem mctGridCoordinateSystem = MCTGridCoordinateSystem.CreateMCTGridCoordinateSystem(gridCoordinateSystem);
                gridCoordID = mctGridCoordinateSystem.ID;
            }
            return gridCoordID;
        }


        private void RemoveViewport()
        {
            if (m_viewport != null)
            {
                if (Environment != null)
                {
                    try
                    {
                        m_environment.Dispose();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("Dispose Environment", McEx);
                    }
                }
                if (EditMode != null)
                {
                    try
                    {
                        EditMode.Dispose();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("Dispose EditMode", McEx);
                    }
                    m_EditMode = null;
                }
                try
                {
                    MainForm.StopAutoViewport(m_viewport);
                    IDNMcMapViewport tempVp = m_viewport;
                    this.m_viewport = null;

                    tempVp.Dispose();

                    MainForm.CollectGC();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Dispose viewport", McEx);
                }
            }

        }

        #endregion

        #region Map Events Implementation
        public void KeyDownOnMap(object sender, string pressedKey, bool IsShiftPressed, bool IsControlPressed)
        {
           // m_IsShiftPressed = IsShiftPressed;

            if (this.EditMode != null && EditMode.IsEditingActive == true)
            {
                switch (pressedKey)
                {
                    case "Escape":
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_ABORT);
                        break;
                    case "Return": // the same as Enter key
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_CONFIRM);
                        break;
                    case "Delete":
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_DELETE_VERTEX);
                        break;
                    case "Next":
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_LOWER);
                        break;
                    case "Add":
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_NEXT_ICON);
                        break;
                    case "Subtract":
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_PREV_ICON);
                        break;
                    case "PageUp":
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_RAISE);
                        break;
                    case "NumPad4":
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_ROTATE_LEFT);
                        break;
                    case "NumPad6":
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_ROTATE_RIGHT);
                        break;
                    case "NumPad8":
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_ROTATE_UP);
                        break;
                    case "NumPad2":
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_ROTATE_DOWN);
                        break;
                }                
            }
            else
            {
                switch (pressedKey)
                {
                    case "Left":
                        MoveCamera(-MoveFactor, 0, 0);
                        break;
                    case "Right":
                        MoveCamera(MoveFactor, 0, 0);
                        break;
                    case "Up":
                        MoveCamera(0, MoveFactor, 0);
                        break;
                    case "Down":
                        MoveCamera(0, -MoveFactor, 0);
                        break;
                }

                // keys that work only on 3D map
                if (this.Viewport.MapType == DNEMapType._EMT_3D)
                {
                    try
                    {
                        switch (pressedKey)
                        {
                            case "NumPad4":
                                this.Viewport.RotateCameraRelativeToOrientation(-RotationFactor, 0, 0);
                                break;
                            case "NumPad6":
                                this.Viewport.RotateCameraRelativeToOrientation(RotationFactor, 0, 0);
                                break;
                            case "NumPad2":
                                this.Viewport.RotateCameraRelativeToOrientation(0, -RotationFactor, 0);
                                break;
                            case "NumPad8":
                                this.Viewport.RotateCameraRelativeToOrientation(0, RotationFactor, 0);
                                break;
                            case "NumPad3":
                                this.Viewport.RotateCameraRelativeToOrientation(0, 0, RotationFactor);
                                break;
                            case "NumPad1":
                                this.Viewport.RotateCameraRelativeToOrientation(0, 0, -RotationFactor);
                                break;
                            case "NumPad7":
                                this.Viewport.RotateCameraRelativeToOrientation(-RotationFactor, 0, 0);
                                MoveCamera(0, MoveFactor, 0);
                                break;
                            case "NumPad9":
                                this.Viewport.RotateCameraRelativeToOrientation(RotationFactor, 0, 0);
                                MoveCamera(0, MoveFactor, 0);
                                break;
                            case "PageUp":
                                MoveCamera(0, 0, MoveFactor);
                                break;
                            case "Next":
                                MoveCamera(0, 0, -MoveFactor);
                                break;
                            case "NumPad0":
                                try
                                {
                                    this.Viewport.SetCameraOrientation(0, -90, 0, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    MapCore.Common.Utilities.ShowErrorMessage("SetCameraOrientation", McEx);
                                }

                                break;
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("RotateCameraRelativeToOrientation", McEx);
                    }
                }

                // Show/Hide map tiles 
                if (IsShiftPressed == true)
                {
                    uint debugOptionsKey = DNMcConstants._MC_EMPTY_ID;
                    int debugOptionsVal = 0;
                    if (pressedKey == Keys.D.ToString())
                    {
                        debugOptionsKey = 0/*ELO_BOX_DRAW_MODE*/;
                        debugOptionsVal = Viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 3;
                    }

                    if (pressedKey == Keys.F1.ToString())
                    {
                        debugOptionsKey = 9/*ELO_RENDER_ONLY_SELECTED_PASS*/;
                        debugOptionsVal = Viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 3;
                    }

                    if (pressedKey == Keys.B.ToString())
                    {
                        debugOptionsKey = 2/*ELO_OVERLAY_OBJECTS_BOX_DRAW_MODE*/;
                        debugOptionsVal = Viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 2;
                    }

                    if (pressedKey == Keys.W.ToString())
                    {
                        debugOptionsKey = 21; //ELO_WIREFRAME_MODE
                        debugOptionsVal = Viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 3;
                    }

                    if (pressedKey == Keys.F.ToString())
                    {
                        debugOptionsKey = 3; //ELO_FREEZE_TREE
                        debugOptionsVal = Viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 2;
                    }

                    if (pressedKey == Keys.S.ToString()) 
                    {
                        debugOptionsKey = 24; //ELO_VIEWPORT_STATS
                        debugOptionsVal = Viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 2;
                    }

                    if (pressedKey == Keys.V.ToString())
                    {
                        debugOptionsKey = 25; //ELO_BOX_DRAW_MODE_TYPE
                        debugOptionsVal = Viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 3;
                    }

                    if (pressedKey == Keys.T.ToString())
                    {
                        /* - Relaod shaders, still doesn't work
                        Ogre::Viewport* vp = dynamic_cast<CMcMapViewport*>(mViewport)->GetInternalMapViewport()->__GetOgreViewport();
                        Ogre::CompositorChain* cc = Ogre::CompositorManager::getSingleton().getCompositorChain(vp);

                        std::vector < std::string> sv;
                        auto compositor_instances_list = cc->getCompositorInstances();
                        for (auto & element : compositor_instances_list)
                        {
                            sv.push_back(element->getCompositor()->getName());
                        }

                        //mViewport->RemovePostProcess(NULL);
                        CMcDataArray<IMcMapTerrain*> dataArr;
                        mViewport->GetTerrains(&dataArr);
                        for (UINT i = 0; i < dataArr.GetLength(); ++i)
                        {
                            mViewport->RemoveTerrain(dataArr[i]);
                        }
                        ResourceGroupHelper::reloadResources();
                        for (UINT i = 0; i < dataArr.GetLength(); ++i)
                        {
                            mViewport->AddTerrain(dataArr[i]);
                        }
                        for (UINT i = 0; i < sv.size(); i++)
                        {
                            mViewport->AddPostProcess(sv[i].c_str());
                        }
                        // free to use
                        */
                    }
                    if ((pressedKey == Keys.S.ToString()) && (IsControlPressed == true))
                    {
                        debugOptionsKey = 4; //ELO_SAVE_INTERSECTING_TILE
                        debugOptionsVal = Viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 2;
                    }

                    if (debugOptionsKey != DNMcConstants._MC_EMPTY_ID)
                    {
                        Viewport.SetDebugOption(debugOptionsKey, debugOptionsVal);
                    }
                }

                //Change movement sensitivity
                if (IsControlPressed == true && (pressedKey == Keys.D1.ToString() ||
                                            pressedKey == Keys.D2.ToString() ||
                                            pressedKey == Keys.D3.ToString() ||
                                            pressedKey == Keys.D4.ToString() ||
                                            pressedKey == Keys.D5.ToString()))
                {
                    switch (pressedKey)
                    {
                        case "D1":
                            MovementSensitivity = 50;
                            break;
                        case "D2":
                            MovementSensitivity = 75;
                            break;
                        case "D3":
                            MovementSensitivity = 100;
                            break;
                        case "D4":
                            MovementSensitivity = 250;
                            break;
                        case "D5":
                            MovementSensitivity = 750;
                            break;
                    }
                }


                // Activate terrain hole
                if (IsControlPressed == true && pressedKey == Keys.H.ToString())
                {
                    if (Viewport != null)
                    {
                        if (m_holeAdded == false)
                        {
                            try
                            {
                                Viewport.AddTerrainHole(1, 33409, 36131);
                                m_holeAdded = true;
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("AddTerrainHole", McEx);
                            }
                        }
                        else
                        {
                            try
                            {
                                Viewport.RemoveTerrainHole(1, 33409, 36131);
                                m_holeAdded = false;
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("RemoveTerrainHole", McEx);
                            }
                        }

                    }
                }

                // render map manually 
                if (eRender == RenderType.Manual)
                {
                    if (IsShiftPressed == true && pressedKey == Keys.R.ToString())
                    {
                        try
                        {
                            m_viewport.Render();
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("Render()", McEx);
                        }
                    }
                }

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_viewport.OverlayManager);
            }
        }

        public void KeyUpOnMap(object sender, string pressedKey)
        {
           /* if (pressedKey == "ShiftKey")
                m_IsShiftPressed = false;*/
            if (this.EditMode != null && EditMode.IsEditingActive == true)
            {
                //The arrow keys are special keys that catch only on key up. 
                switch (pressedKey)
                {
                    case "Up":
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_MOVE_UP);
                        break;
                    case "Down":
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_MOVE_DOWN);
                        break;
                    case "Left":
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_MOVE_LEFT);
                        break;
                    case "Right":
                        EditModeOnKeyEventCall(DNEKeyEvent._EKE_MOVE_RIGHT);
                        break;
                }                
            }
        }

        // function converting screen point to world point (on 3D: with z if possible, on 2D: without z) 
        private DNSMcVector3D ViewportScreenToWorld(DNSMcVector3D ScreenPoint)
        {
            bool bIntersection = false;
            DNSMcVector3D WorldCenter = new DNSMcVector3D();
            if (mapType == DNEMapType._EMT_3D)
            {
                 Viewport.ScreenToWorldOnTerrain(ScreenPoint, out WorldCenter, out bIntersection);
            }
            if (!bIntersection) // EMT_2D || !bIntersection
            {
                Viewport.ScreenToWorldOnPlane(ScreenPoint, out WorldCenter, out bIntersection);
            }
            return (bIntersection ? WorldCenter : DNSMcVector3D.v3Zero);
        }


        public void MouseMoveOnMap(object sender, Point mousePoint, MouseButtons currMouseButton, int mouseDelta)
        {
            if (m_viewport == null)
            {
                return;
            }
            try
            {
                // if viewport is global map
                if (m_viewport.GetRegisteredLocalMaps().Length > 0)
                {
                    switch (currMouseButton)
                    {
                        case MouseButtons.Left:
                            ViewportOnMouseEventCall(DNEMouseEvent._EME_MOUSE_MOVED_BUTTON_DOWN,
                                                        new DNSMcPoint(mousePoint));
                            break;
                        case MouseButtons.None:
                            ViewportOnMouseEventCall(DNEMouseEvent._EME_MOUSE_MOVED_BUTTON_UP,
                                                        new DNSMcPoint(mousePoint));
                            break;
                    }

                    // global map footprint was change therefore we don't want to catch edit mode events
                    if (m_RenderNeeded == true)
                    {
                        m_TesterMouseDownCatch = false;
                        return;
                    }
                }


                DNSMcPoint EventPixel = new DNSMcPoint(mousePoint.X - mLastMouse.X, mousePoint.Y - mLastMouse.Y);
                if (EditMode != null && EditMode.IsEditingActive == true)
                {
                    switch (currMouseButton)
                    {
                        case MouseButtons.Left:
                            EditModeOnMouseEventCall(DNEMouseEventEditMode._EME_EM_MOUSE_MOVED_BUTTON_DOWN,
                                                        new DNSMcPoint(mousePoint),
                                                        (short)mouseDelta);
                            break;
                        case MouseButtons.None:
                            EditModeOnMouseEventCall(DNEMouseEventEditMode._EME_EM_MOUSE_MOVED_BUTTON_UP,
                                                        new DNSMcPoint(mousePoint),
                                                        (short)mouseDelta);
                            break;
                    }

                    m_TesterMouseDownCatch = false;
                }
                else
                {
                    if (m_sPrevMousePoint != Point.Empty)
                    {
                        uint uWidth, uHeight;
                        Viewport.GetViewportSize(out uWidth, out uHeight);
                        DNSMcVector2D ViewportCenter = new DNSMcVector2D(uWidth / 2, uHeight / 2);
                        DNSMcVector2D MousePrev = new DNSMcVector2D(m_sPrevMousePoint.X, m_sPrevMousePoint.Y);
                        DNSMcVector2D MouseCurr = new DNSMcVector2D(mousePoint.X, mousePoint.Y);
                        DNSMcVector2D MousePrevFromCenter = MousePrev - ViewportCenter;
                        DNSMcVector2D MouseCurrFromCenter = MouseCurr - ViewportCenter;
                        DNSMcVector2D MouseDellta = MouseCurr - MousePrev;
                        mapType = m_viewport.MapType;

                        if (m_TesterMouseDownCatch == true)
                        {
                            if (currMouseButton == MouseButtons.Left)
                            {
                                if (mapType == DNEMapType._EMT_3D)
                                {
                                    DNSMcVector3D MouseCurrWorld = ViewportScreenToWorld(new DNSMcVector3D(MouseCurr));
                                    if (ControlKeyDown)
                                    {
                                        DNSMcVector3D MousePrevWorld = ViewportScreenToWorld(new DNSMcVector3D(MousePrev));
                                        if (MousePrevWorld != null && MouseCurrWorld != null)
                                        {
                                            Viewport.SetCameraPosition((MousePrevWorld - MouseCurrWorld), true);
                                        }
                                    }
                                    else
                                    {
                                        float fPitch, fRoll, fYaw;
                                        Viewport.GetCameraOrientation(out fYaw, out fPitch, out fRoll);
                                        float fFOV = Viewport.GetCameraFieldOfView();
                                        double dCameraDist = ViewportCenter.x / Math.Tan(fFOV / 2 * Math.PI / 180);
                                        double dDeltaYaw = (new DNSMcVector2D(MousePrevFromCenter.x, dCameraDist)).GetYawAngleDegrees() -
                                                        (new DNSMcVector2D(MouseCurrFromCenter.x, dCameraDist)).GetYawAngleDegrees();
                                        double dDeltaPitch = (new DNSMcVector2D(MouseCurrFromCenter.y, dCameraDist)).GetYawAngleDegrees() -
                                                  (new DNSMcVector2D(MousePrevFromCenter.y, dCameraDist)).GetYawAngleDegrees();
                                        Viewport.RotateCameraRelativeToOrientation((float)dDeltaYaw, (float)Math.Min(Math.Max(dDeltaPitch, dMinPitch - fPitch), dMaxPitch - fPitch), 0, false);

                                        // roll could be altered by RotateCameraRelativeToOrientation(), return it to the previous value
                                        float pfYaw, pfPitch, pfRoll;
                                        Viewport.GetCameraOrientation(out pfYaw, out pfPitch, out pfRoll);
                                        float fRollDiff = fRoll - pfRoll;
                                        if (fRollDiff != 0)
                                        {
                                            if (MouseCurrWorld != null)
                                            {
                                                Viewport.RotateCameraAroundWorldPoint(MouseCurrWorld, 0, 0, fRollDiff, false);
                                            }
                                            else
                                            {
                                                Viewport.SetCameraOrientation(0, 0, fRollDiff, true);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (ControlKeyDown)
                                    {
                                        double fDeltaYaw = (MouseCurrFromCenter.GetYawAngleDegrees()) - (MousePrevFromCenter.GetYawAngleDegrees());
                                        Viewport.SetCameraOrientation((float)fDeltaYaw, 0, 0, true);
                                    }
                                    else
                                    {
                                        Viewport.ScrollCamera((int)-MouseDellta.x, (int)-MouseDellta.y);
                                    }
                                }


                            }
                            else if (currMouseButton == MouseButtons.Middle && m_RotationCenter != DNSMcVector3D.v3Zero)
                            {
                                DrawPointOnMap(m_RotationCenter);

                                Viewport.RotateCameraAroundWorldPoint(m_RotationCenter, (float)(MouseDellta.x * 360 / uWidth), 0, 0, false);
                                if (mapType == DNEMapType._EMT_3D)
                                {
                                    float fPitch, fRoll, fYaw;
                                    Viewport.GetCameraOrientation(out fYaw, out fPitch, out fRoll);
                                    Viewport.RotateCameraAroundWorldPoint(m_RotationCenter, 0, (float)(Math.Min(Math.Max(-MouseDellta.y * 180 / uWidth, dMinPitch - fPitch), dMaxPitch - fPitch)), 0, true);
                                }
                            }
                        }
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("mouse move on map", McEx);
            }

            m_sPrevMousePoint = mousePoint;
            
        }

        private void DrawPointOnMap(DNSMcVector3D worldPoint)
        {
            if (m_objRotatePoint == null)
            {
                IDNMcTexture m_DefaultTexture = null;
                try
                {
                    m_DefaultTexture = DNMcBitmapHandleTexture.Create(MCTester.Icons.NotationPoint.GetHbitmap(), false, false, new DNSMcBColor(0, 0, 0, 255));
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcBitmapHandleTexture.Create", McEx);
                }

                try
                {
                    m_ObjSchemeRotatePointItem = DNMcPictureItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN, m_DefaultTexture);
                    ((IDNMcPictureItem)m_ObjSchemeRotatePointItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcPictureItem.Create", McEx);
                }

                try
                {
                    if (Viewport.OverlayManager != null)
                    {
                        IDNMcOverlay[] overlays = Viewport.OverlayManager.GetOverlays();
                        if(overlays != null && overlays.Length > 0)
                        {
                            m_objRotatePoint = DNMcObject.Create(overlays[0],
                                            m_ObjSchemeRotatePointItem,
                                            DNEMcPointCoordSystem._EPCS_WORLD,
                                            new DNSMcVector3D[1] { worldPoint },
                                            false);
                        }
                        else
                            MessageBox.Show("Missing Overlay");
                    }
                    else
                        MessageBox.Show("Missing Overlay Manager");
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                }
            }
        }

        public new void MouseWheel(object sender, Point mousePoint, int mouseDelta)
        {
            if (EditMode != null && m_EditMode.IsEditingActive == true)
            {
                EditModeOnMouseEventCall(DNEMouseEventEditMode._EME_EM_MOUSE_WHEEL,
                                            new DNSMcPoint(mousePoint),
                                            (short)mouseDelta);
            }
            else
            {
                try
                {
                    float fFactor = (ShiftKeyDown ? 5 : 1);
                    float fScalefactor = (float)Math.Pow(1.25, fFactor);

                    //change zoom using the mouse wheel.
                    if (Viewport.MapType == DNEMapType._EMT_2D)
                    {
                        try
                        {

                            float fScale = Viewport.GetCameraScale();

                            if (mouseDelta > 0)
                            {
                                m_CamScale = (fScale / fScalefactor);
                            }
                            else
                            {
                                m_CamScale = (fScale * fScalefactor);
                            }

                            if (m_CamScale > float.MinValue && m_CamScale < float.MaxValue)
                            {
                                Viewport.SetCameraScale(m_CamScale);
                                Manager_StatusBar.UpdateScale();
                            }
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("SetCameraScale", McEx);
                        }

                    }

                    //change elevation using the mouse wheel.
                    else if (Viewport.MapType == DNEMapType._EMT_3D)
                    {
                        if (ControlKeyDown)
                        {

                            Viewport.MoveCameraRelativeToOrientation(new DNSMcVector3D(0, 0, -Math.Sign(mouseDelta) * 12.5 * fFactor), true);
                        }
                        else
                        {
                            DNSMcVector3D CameraPosition = Viewport.GetCameraPosition();
                            DNMcNullableOut<double> dist = new DNMcNullableOut<double>();
                            DNMcNullableOut<double> pdDistance = new DNMcNullableOut<double>();
                            double dDistance = double.MinValue;
                            DNSMcVector3D IntersectionPoint;
                            bool pbIntersectionFound;

                            Viewport.GetRayIntersection(CameraPosition, Viewport.GetCameraForwardVector(), double.MaxValue, out pbIntersectionFound, null, null, dist);
                            if (!pbIntersectionFound)
                            {
                                IDNMcGeographicCalculations GeoCalc = DNMcGeographicCalculations.Create(Viewport.CoordinateSystem);
                                uint puWidth, puHeight;
                                Viewport.GetViewportSize(out puWidth, out puHeight);
                                DNSMcVector3D ScreenCenter = new DNSMcVector3D(puWidth / 2, puHeight / 2, 0);
                                if (Viewport.GetTerrainsBoundingBox() != null)
                                {
                                    DNSMcBox terrainsBoundingBox = Viewport.GetTerrainsBoundingBox();
                                    double dDistanceX = double.MinValue, dDistanceY = double.MinValue;
                                    DNSMcVector3D Normal = new DNSMcVector3D(1, 0, 0);

                                    // intersect bounding box's 4 planes
                                    Viewport.ScreenToWorldOnPlane(ScreenCenter, out IntersectionPoint, out pbIntersectionFound, terrainsBoundingBox.MinVertex.x, Normal);
                                    if (pbIntersectionFound && (CameraPosition - IntersectionPoint).Length() < dZoom3DMaxDistance)
                                    {
                                        GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint, null, pdDistance, true);
                                        dDistanceX = pdDistance.Value;
                                    }

                                    Viewport.ScreenToWorldOnPlane(ScreenCenter, out IntersectionPoint, out pbIntersectionFound, terrainsBoundingBox.MaxVertex.x, Normal);
                                    if (pbIntersectionFound && (CameraPosition - IntersectionPoint).Length() < dZoom3DMaxDistance)
                                    {
                                        GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint, null, pdDistance, true);
                                        if (pdDistance.Value > dDistanceX)
                                        {
                                            dDistanceX = pdDistance.Value;
                                        }
                                    }

                                    Normal = new DNSMcVector3D(0, 1, 0);
                                    Viewport.ScreenToWorldOnPlane(ScreenCenter, out IntersectionPoint, out pbIntersectionFound, terrainsBoundingBox.MinVertex.y, Normal);
                                    if (pbIntersectionFound && (CameraPosition - IntersectionPoint).Length() < dZoom3DMaxDistance)
                                    {
                                        GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint, null, pdDistance, true);
                                        dDistanceY = pdDistance.Value;
                                    }

                                    Viewport.ScreenToWorldOnPlane(ScreenCenter, out IntersectionPoint, out pbIntersectionFound, terrainsBoundingBox.MaxVertex.y, Normal);
                                    if (pbIntersectionFound && (CameraPosition - IntersectionPoint).Length() < dZoom3DMaxDistance)
                                    {
                                        GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint, null, pdDistance, true);
                                        if (pdDistance.Value > dDistanceY)
                                        {
                                            dDistanceY = pdDistance.Value;
                                        }
                                    }

                                    if (dDistanceX != double.MinValue && dDistanceY != double.MinValue)
                                        dDistance = Math.Min(dDistanceX, dDistanceY);
                                    else if (dDistanceX != double.MinValue)
                                        dDistance = dDistanceX;
                                    else if (dDistanceY != double.MinValue)
                                        dDistance = dDistanceY;
                                }
                                if (dDistance == double.MinValue)
                                {
                                    // intersect zero-height plane
                                    Viewport.ScreenToWorldOnPlane(ScreenCenter, out IntersectionPoint, out pbIntersectionFound);
                                    if (pbIntersectionFound && (CameraPosition - IntersectionPoint).Length() < dZoom3DMaxDistance)
                                    {
                                        GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint, null, pdDistance, true);
                                        if (pdDistance.Value > dDistance)
                                            dDistance = pdDistance.Value;
                                    }
                                }
                            }
                            else
                                dDistance = dist.Value;

                            if (dDistance != double.MinValue)
                            {
                                double dNewDistance;
                                if (mouseDelta > 0)
                                {
                                    dNewDistance = Math.Max(dDistance / fScalefactor, dZoom3DMinDistance);
                                }
                                else
                                {
                                    dNewDistance = Math.Min(dDistance * fScalefactor, dZoom3DMaxDistance);
                                }
                                Viewport.MoveCameraRelativeToOrientation(new DNSMcVector3D(0, dDistance - dNewDistance, 0), false);
                            }
                        }
                    }

                    // turn on all viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_viewport.OverlayManager);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("MouseWheel", McEx);
                }
            }
        }

        public void MouseDownOnMap(object sender, Point mousePoint, MouseButtons currMouseButton, int mouseDelta)
        {
            if (m_viewport.GetRegisteredLocalMaps().Length > 0)
            {
                if (currMouseButton == MouseButtons.Left)
                {
                    ViewportOnMouseEventCall(DNEMouseEvent._EME_BUTTON_PRESSED,
                                             new DNSMcPoint(mousePoint));
                }                

                if (m_RenderNeeded == true)
                {
                    m_TesterMouseDownCatch = false;
                    return;
                }
            }

            if (EditMode != null && EditMode.IsEditingActive == true)
            {
                m_TesterMouseDownCatch = false;

                if (currMouseButton == MouseButtons.Left)
                {
                    EditModeOnMouseEventCall(DNEMouseEventEditMode._EME_EM_BUTTON_PRESSED,
                                            new DNSMcPoint(mousePoint),
                                            (short)mouseDelta);
                }
                if (currMouseButton == MouseButtons.Middle)
                {
                    frmAddOverlayManagerWorldPt addOverlayManagerWorldPt = new frmAddOverlayManagerWorldPt(mousePoint);
                    addOverlayManagerWorldPt.ShowDialog();
                }
            }
            else
            {
                m_TesterMouseDownCatch = true;
                m_RotatePoint = mousePoint;
                m_RotationCenter = ViewportScreenToWorld(new DNSMcVector3D(mousePoint.X, mousePoint.Y, 0));
                //m_sPrevMousePoint = mousePoint;
            }
        }

        public void MouseDoubleClickOnMap(object sender, Point mousePoint, int mouseDelta)
        {
            if (EditMode != null && EditMode.IsEditingActive == true)
            {
                EditModeOnMouseEventCall(DNEMouseEventEditMode._EME_EM_BUTTON_DOUBLE_CLICK,
                                                new DNSMcPoint(mousePoint),
                                                (short)mouseDelta);
            }
        }

        public void MouseUpOnMap(object sender, Point mousePoint, int mouseDelta)
        {
            this.Cursor = Cursors.Default;

            if (m_viewport.GetRegisteredLocalMaps().Length > 0)
            {
                ViewportOnMouseEventCall(DNEMouseEvent._EME_BUTTON_RELEASED,
                                             new DNSMcPoint(mousePoint));

                //if (m_RenderNeeded == true)
                //    return;
            }

            if (EditMode != null && EditMode.IsEditingActive == true)
            {
                EditModeOnMouseEventCall(DNEMouseEventEditMode._EME_EM_BUTTON_RELEASED,
                                            new DNSMcPoint(mousePoint),
                                            (short)mouseDelta);
            }
            // for rotate point object
            if (m_objRotatePoint != null)
            {
                m_objRotatePoint.Dispose();
                m_objRotatePoint.Remove();
                m_objRotatePoint = null;
            }
            m_RotationCenter = DNSMcVector3D.v3Zero;
            m_TesterMouseDownCatch = false;
        }

        public void PreviewKeyDownOnMap(object sender, string pressedKey)
        {
            
        }

        public void MCTMapForm_Activated(object sender, EventArgs e)
        {
            //if (Viewport != null)
            //    MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlayManager = Viewport.OverlayManager;

            MCTMapFormManager.ActivateMapForm(this);
        }

        DNSMcVector3D m_worldPoint = new DNSMcVector3D();
        DNSMcVector3D m_imagePoint = new DNSMcVector3D();
        DNSMcVector3D m_screenPoint = new DNSMcVector3D();
        Point m_mousePoint;

        public void OnMapClick(object sender, Point mousePoint, MouseButtons mouseClickedButton)
        {
            m_mousePoint = mousePoint;

            bool isUseCallback = false;
            try
            {
                if (OnMapClicked != null)
                {
                    bool isIntersect;
                    m_worldPoint = new DNSMcVector3D();
                    m_imagePoint = new DNSMcVector3D();
                    m_screenPoint = new DNSMcVector3D( mousePoint.X,  mousePoint.Y, 0);

                    // if MapCore display ELO_BOX_DRAW_MODE with children=2, display tile key information EGO_DEBUG_TILE on mouse click 
                    int mod = Viewport.GetDebugOption(0) == 2 ? 1 : 0;
                    Viewport.SetDebugOption(106, mod); // Magic option -> EGO_DEBUG_TILE, do not remove 
                    Viewport.ScreenToWorldOnTerrain(m_screenPoint, out m_worldPoint, out isIntersect);
                    Viewport.SetDebugOption(106, mod); // Magic option -> EGO_DEBUG_TILE, do not remove

                    if (isIntersect == false)
                        Viewport.ScreenToWorldOnPlane(m_screenPoint, out m_worldPoint, out isIntersect);
                    if (isIntersect == false)
                    {
                        m_worldPoint.x = 0;
                        m_worldPoint.y = 0;
                        m_worldPoint.z = 0;
                    }

                    if (Viewport.GetImageCalc() != null)
                    {
                        try
                        {
                            m_imagePoint = m_worldPoint;
                            DNMcNullableOut<bool> isDTM = new DNMcNullableOut<bool>();
                            //DNMcNullableOut<DNEMcErrorCode> errorCode = new DNMcNullableOut<DNEMcErrorCode>();
                            isUseCallback = true;
                            m_worldPoint = Viewport.GetImageCalc().ImagePixelToCoordWorld(new DNSMcVector2D(m_imagePoint), isDTM, null, this);
                        }
                        catch (MapCoreException McEx)
                        {
                           Utilities.ShowErrorMessage("ImagePixelToCoordWorld", McEx);
                        }
                    }
                    else
                    {
                        m_imagePoint = DNSMcVector3D.v3Zero;
                    }

                    if(!isUseCallback)
                    {
                        OnMapClickedResults(Viewport, m_mousePoint, m_worldPoint, m_imagePoint, isIntersect);
                    }
                    // set world point according the overlay manager coordinate system
                     /** move to callback
                    if (Viewport.OverlayManager != null)
                        worldPoint = Viewport.ViewportToOverlayManagerWorld(worldPoint, Viewport.OverlayManager);

                    OnMapClicked(mousePoint, worldPoint, imagePoint);*/
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ScreenToWorldOnTerrain/ScreenToWorldOnPlane", McEx);
            }
        }

        private void OnMapClickedResults(IDNMcMapViewport viewport, Point mousePoint, DNSMcVector3D worldPoint, DNSMcVector3D imagePoint, bool IsHasIntersection)
        {
            try
            {
                if (Viewport.OverlayManager != null)
                    worldPoint = Viewport.ViewportToOverlayManagerWorld(worldPoint, Viewport.OverlayManager);

                OnMapClicked(mousePoint, worldPoint, imagePoint, IsHasIntersection);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ViewportToOverlayManagerWorld", McEx);
            }
        }

        public void MapResize(object sender, EventArgs e)
        {

        }

        #endregion

        #region Public Properties
        public IMapObject MapObjectCtrl
        {
            get { return m_MapObject; }
            set { m_MapObject = value; }
        }

        public WinFormMapObject WinFormMap
        {
            get { return m_WinFormMap; }
            set { m_WinFormMap = value; }
        }

        public WpfMapObject WpfMap
        {
            get { return m_WpfMap; }
            set { m_WpfMap = value; }
        }

        public IntPtr MapPointer
        {
            get
            {
                if (WinFormMap != null)
                    return WinFormMap.Handle;
                else
                    return IntPtr.Zero;
            }
        }

        public static Timer RenderTimer
        {
            get { return mRenderTimer; }
            set { mRenderTimer = value; }
        }

        public IDNMcMapViewport Viewport
        {
            get { return this.m_viewport; }
            set {
                this.m_viewport = value;
                
            }
        }

        private void SetWindowName()
        {
            this.Text = Manager_MCViewports.GetFullNameOfViewport(m_viewport);
        }



        public EditmodeCallbackManager EditModeManagerCallback
        {
            get { return this.m_editModeManagerCallback; }
        }

        public IDNMcEditMode EditMode
        {
            get
            {
                return m_EditMode; 
            }
            set
            {
                m_EditMode = value;
            }
        }

        public void CreateEditMode(IDNMcMapViewport viewport)
        {
            try
            {
                this.EditMode = DNMcEditMode.Create(viewport);
                m_editModeManagerCallback = new EditmodeCallbackManager(m_EditMode);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcEditMode.Create", McEx);
            }
        }

        public IDNMcMapEnvironment Environment
        {
            get { return m_environment; }
            set { m_environment = value; }
        }

        public StatusStrip StatusBar
        {
            get { return m_statusBar; }
            set { m_statusBar = value; }
        }

        public bool bFootprintIsDefined
        {
            get { return m_FootprintIsDefined; }
            set { m_FootprintIsDefined = value; }
        }

        public float MovementSensitivity
        {
            get { return m_MovementSensitivity; }
            set { m_MovementSensitivity = value; }
        }

        public int MoveFactor
        {
            get { return (int)MovementSensitivity;}
        }

        public float RotationFactor
        {
            get { return m_RotationFactor * MovementSensitivity; }
        }

        public bool IsWpfMapWindow
        {
            get { return m_IsWpfMapWindow; }
            set { m_IsWpfMapWindow = value; }
        }

        private void MCTMapForm_Load(object sender, EventArgs e)
        {
            if (m_IsWpfMapWindow == true)
            {                
                System.Windows.Forms.Integration.ElementHost elhost = new System.Windows.Forms.Integration.ElementHost();

                elhost.Dock = DockStyle.Fill;
                WpfMap = new WpfMapObject(this);
                elhost.Child = WpfMap;
                this.Controls.Add(elhost);
                WpfMap.LoadMap();
                m_MapObject = WpfMap;
                this.Text = "WPF Window";
            }

            mRenderTimer.Start();
        }

        public void OnTerrainHeightResults(bool bHeightFound, double dHeight, DNSMcVector3D? pNormal)
        {
            throw new NotImplementedException();
        }

        public void OnTerrainHeightsAlongLineResults(DNSMcVector3D[] aPointsWithHeights, float[] afSlopes, DNSSlopesData? pSlopesData)
        {
            throw new NotImplementedException();
        }

        public void OnExtremeHeightPointsInPolygonResults(bool bPointsFound, DNSMcVector3D? pHighestPoint, DNSMcVector3D? pLowestPoint)
        {
            throw new NotImplementedException();
        }

        public void OnTerrainHeightMatrixResults(double[] adHeightMatrix)
        {
            throw new NotImplementedException();
        }

        public void OnTerrainAnglesResults(double dPitch, double dRoll)
        {
            throw new NotImplementedException();
        }

        public void OnRayIntersectionResults(bool bIntersectionFound, DNSMcVector3D? pIntersection, DNSMcVector3D? pNormal, double? pdDistance)
        {
            DNSMcVector3D worldPoint = new DNSMcVector3D();
            if (bIntersectionFound &&
             pIntersection != null &&
             pIntersection.HasValue &&
             Viewport != null)
            {
                worldPoint = pIntersection.Value;
            }

            OnMapClickedResults(Viewport, m_mousePoint, worldPoint, m_imagePoint, bIntersectionFound);
        }

        public void OnRayIntersectionTargetsResults(DNSTargetFound[] aIntersections)
        {
            throw new NotImplementedException();
        }

        public void OnLineOfSightResults(DNSLineOfSightPoint[] aPoints, double dCrestClearanceAngle, double dCrestClearanceDistance)
        {
            throw new NotImplementedException();
        }

        public void OnPointVisibilityResults(bool bIsTargetVisible, double? pdMinimalTargetHeightForVisibility, double? pdMinimalScouterHeightForVisibility)
        {
            throw new NotImplementedException();
        }

        public void OnAreaOfSightResults(IDNAreaOfSight pAreaOfSight, DNSLineOfSightPoint[][] aLinesOfSight, DNSPolygonsOfSight? pSeenPolygons, DNSPolygonsOfSight? pUnseenPolygons, DNSStaticObjectsIDs[] aSeenStaticObjects)
        {
            throw new NotImplementedException();
        }

        public void OnBestScoutersLocationsResults(DNSMcVector3D[] aScouters)
        {
            throw new NotImplementedException();
        }

        public void OnLocationFromTwoDistancesAndAzimuthResults(DNSMcVector3D Target)
        {
            throw new NotImplementedException();
        }

        public void OnDtmLayerTileGeometryByKeyResults(DNSTileGeometry TileGeometry)
        {
            throw new NotImplementedException();
        }

        public void OnRasterLayerTileBitmapByKeyResults(DNEPixelFormat eBitmapPixelFormat, bool bBitmapFromTopToBottom, DNSMcSize BitmapSize, DNSMcSize BitmapMargins, byte[] aBitmapBits)
        {
            throw new NotImplementedException();
        }

        public void OnRasterLayerColorByPointResults(DNSMcBColor Color)
        {
            throw new NotImplementedException();
        }

        public void OnTraversabilityAlongLineResults(DNSTraversabilityPoint[] aTraversabilitySegments)
        {
            throw new NotImplementedException();
        }

        public void OnError(DNEMcErrorCode eErrorCode)
        {
            MessageBox.Show("Convert points from image" + ", Error: " + IDNMcErrors.ErrorCodeToString(eErrorCode),
                    "Error From Async Query Callback", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

    }
}
