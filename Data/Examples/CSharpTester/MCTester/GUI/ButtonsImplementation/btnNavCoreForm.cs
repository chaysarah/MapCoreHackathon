using MapCore;
using MapCore.Common;
using MCTester.Controls;
using MCTester.GUI.Forms;
using MCTester.Managers;
using MCTester.Managers.MapWorld;
using MCTester.Managers.ObjectWorld;
using MCTester.MapWorld;
using NavCore.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.ButtonsImplementation
{
    public partial class btnNavCoreForm : Form
    {
        private NavCoreBaseWrapper m_CurrNavCore;
        private IDNMcOverlay m_CurrActiveOverlay;
        private IDNMcEditMode m_CurrEditMode;
        private IDNMcMapViewport m_CurrViewport;
        private IDNMcMapTerrain m_CurrMapTerrain;
        private IDNMcMapTerrain m_CurrStandaloneMapTerrain;
        private IDNMcObjectSchemeItem m_UserPathItem;
        private IDNMcObject m_NavBoundingBoxRect;

        //private int m_ExitStatus = 0;

        private IDNMcObject m_UserPathObj = null;
        private IDNMcObject m_NavCorePathObj = null;
        private DNSMcVector3D[] m_LocationPointsNavCoreViewport = null;
        private IDNMcLineItem m_NavCorePathItem = null;
        private IDNMcTraversabilityMapLayer m_TraversabilityMapLayer = null;
        private List<IDNMcObject> m_ListTraversabilityObjects = new List<IDNMcObject>();

        private ECode m_ECodeGetBoundingBox;
        private uint m_EpsgCode = 0;
        private double m_MaxRoiAreaSupported = 0;
        private IDNMcObject m_BoundingBoxRect;
        private IDNMcObject m_BoundingBoxPaintRect;
        private DNSMcVector3D[] m_UserObjPoints = null;
        private ELoadStatus m_ELoadStatus;
        private ECode m_SetSoilCostECode;
        private ECode m_SetRoadsCostECode;
        private ECode m_SetLimitedPassabilityAreasCostECode;
        private ECode m_SetVeryLimitedPassabilityAreasCostECode;
        private DNSTraversabilityPoint[] m_TraversabilityPoints;
        private EFindPathStatus m_FindPathStatus;
        private ECode m_FindPathECode;
        private char[] m_Seperator = { ',' };
        private bool m_IsSameAfterImport = true;
        private IDNMcSpatialQueries m_StandaloneSQ = null;
        private IDNMcGridConverter m_GridConverter;

        private enum ErrorCode
        {
            compare, invalid, none
        }

        public btnNavCoreForm()
        {
            InitializeComponent();

            LoadForm();
        }

        private void LoadForm()
        {
            btnExportToCSV.Enabled = false;

            ntxResultLineWidth.SetFloat(Manager_NavCore.LineWidth);
            ntxGDResultLineWidth.SetFloat(Manager_NavCore.LineWidth);
            ntxGDTransparency.SetByte(Manager_NavCore.TraversabilityTransparency);

            if (MCTMapFormManager.MapForm != null)
            {
                m_CurrViewport = MCTMapFormManager.MapForm.Viewport;
                try
                {
                    if (m_CurrViewport != null)
                    {
                        m_CurrActiveOverlay = Manager_MCOverlayManager.GetActiveOverlayOfOverlayManager(m_CurrViewport.OverlayManager);
                        IDNMcMapTerrain[] mapTerrains = m_CurrViewport.GetTerrains();
                        if (mapTerrains.Length > 0)
                            m_CurrMapTerrain = mapTerrains[0];

                        ctrlGridCoordinateSystem1.GridCoordinateSystem = m_CurrViewport.CoordinateSystem;
                        ctrlGridCoordinateSystem1.Enabled = false;
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetOverlayManager", McEx);
                }


                m_CurrEditMode = MCTMapFormManager.MapForm.EditMode;


            }
            else
            {
                btnLoad.Enabled = true;
            }

            ctrlNavBoundingBox.EnableButtons(false);

            if (Manager_NavCore.NavCoreFolderPath != "")
            {
                ctrlBrowseNavMeshFolder.FileName = Manager_NavCore.NavCoreFolderPath;
                btnCreate_Click(null, null);
            }
            if (Manager_NavCore.TraversabilityFolderPath != "")
            {
                ctrlBrowseTraversabilityMapLayer.FileName = Manager_NavCore.TraversabilityFolderPath;
                chxIsCreateStandaloneSpatialQueries.Checked = Manager_NavCore.IsCreateStandaloneSpatialQueries;

                btnTraversabilityMapLayer_Click(null, null);
                ntxTransparency.SetByte(Manager_NavCore.TraversabilityTransparency);
            }
            if (Manager_NavCore.BoundingBox != new DNSMcBox())
            {
                ctrlUserBoundingBox.SetBoxValue(Manager_NavCore.BoundingBox);
                btnBuild_Click(null, null);
            }
            ntxSoilCost.SetFloat(Manager_NavCore.SoilCost);
            ntxRoadsCost.SetFloat(Manager_NavCore.RoadsCost);
            ntxLimitedPassabilityAreasCost.SetFloat(Manager_NavCore.LimitedPassabilityAreasCost);
            ntxVeryLimitedPassabilityAreasCost.SetFloat(Manager_NavCore.VeryLimitedPassabilityAreasCost);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (m_CurrViewport == null && ctrlGridCoordinateSystem1.GridCoordinateSystem == null && sender != null)  // not came from import
            {
                MessageBox.Show("Missing Grid Coordinate System");
                return;
            }
            if (ctrlBrowseNavMeshFolder.FileName != "")
            {
                LoadNavCoreParams();
            }
            else
            {
                MessageBox.Show("Please choose nav mesh folder", "Missing nav mesh folder");
            }
        }



        private void LoadNavCoreParams()
        {
            Manager_NavCore.NavCoreFolderPath = "";
            try
            {
                SParams param = new SParams();
                m_CurrNavCore = new NavCoreBaseWrapper();
                if (m_CurrNavCore.Init(ref param) == ECode.SUCCESS)
                {
                    if (m_CurrNavCore.NavCoreCreate(ref param, ctrlBrowseNavMeshFolder.FileName) == ECode.SUCCESS)
                    {
                        Manager_NavCore.NavCoreFolderPath = ctrlBrowseNavMeshFolder.FileName;

                        m_CurrNavCore.GetCoordSysEpsgCode(ref m_EpsgCode);
                        txtCoordSysEpsg.Text = m_EpsgCode.ToString();
                        IDNMcGridCoordinateSystem m_navGridCoordinateSystem = null;
                        if (m_EpsgCode == 0)
                        {
                            m_navGridCoordinateSystem = DNMcGridUTM.Create(36, DNEDatumType._EDT_ED50_ISRAEL, new DNSDatumParams());
                        }
                        else
                        {
                            m_navGridCoordinateSystem = DNMcGridGeneric.Create(string.Format("EPSG:{0}", m_EpsgCode.ToString()));
                        }
                        if (ctrlGridCoordinateSystem1.GridCoordinateSystem == null)
                             ctrlGridCoordinateSystem1.GridCoordinateSystem = m_navGridCoordinateSystem;
                        m_GridConverter = DNMcGridConverter.Create(m_navGridCoordinateSystem, ctrlGridCoordinateSystem1.GridCoordinateSystem, false);

                        m_CurrNavCore.GetMaxRoiAreaSupported(ref m_MaxRoiAreaSupported);
                        ntbMaxRoiAreaSupported.SetDouble(m_MaxRoiAreaSupported);

                        STnBox pBoundingBox = new STnBox();
                        m_ECodeGetBoundingBox = m_CurrNavCore.GetBoundingBox(ref pBoundingBox);
                        if (m_ECodeGetBoundingBox == ECode.SUCCESS)
                        {
                            DNSMcBox mcBox = new DNSMcBox();
                            mcBox.MinVertex = new DNSMcVector3D(pBoundingBox.MinVertex.x, pBoundingBox.MinVertex.y, pBoundingBox.MinVertex.z);
                            mcBox.MaxVertex = new DNSMcVector3D(pBoundingBox.MaxVertex.x, pBoundingBox.MaxVertex.y, pBoundingBox.MaxVertex.z);
                            ctrlNavBoundingBox.SetBoxValue(mcBox);

                            DNSMcVector3D[] locationPoints = { ConvertPointAtoB(mcBox.MinVertex), ConvertPointAtoB(mcBox.MaxVertex) };
                            m_NavBoundingBoxRect = DrawRect(new DNSMcBColor(255, 255, 0, 255), locationPoints);
                        }
                        groupBox1.Enabled = false;
                        gbLoadNavMesh.Enabled = true;


                    }
                    else
                        MessageBox.Show("Failed to create the NavCore layer: " + m_CurrNavCore.LastError);
                }
                else
                    MessageBox.Show("Failed to init the NavCore " + m_CurrNavCore.LastError);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Create DNGridCoordinateSystem/DNMcGridConverter", McEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDrawRect_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            CreateRect();
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();

            Manager_NavCore.BoundingBox = new DNSMcBox();
            STnBox tnBox = new STnBox();
            DNSMcVector3D min = new DNSMcVector3D(ctrlUserBoundingBox.GetBoxValue().MinVertex.x, ctrlUserBoundingBox.GetBoxValue().MinVertex.y, ctrlUserBoundingBox.GetBoxValue().MinVertex.z);
            DNSMcVector3D max = new DNSMcVector3D(ctrlUserBoundingBox.GetBoxValue().MaxVertex.x, ctrlUserBoundingBox.GetBoxValue().MaxVertex.y, ctrlUserBoundingBox.GetBoxValue().MaxVertex.z);
            DNSMcVector3D convertedMin = ConvertPointBtoA(min);
            DNSMcVector3D convertedMax = ConvertPointBtoA(max);
            tnBox.MinVertex = new STnVector3D(convertedMin.x, convertedMin.y, convertedMin.z);
            tnBox.MaxVertex = new STnVector3D(convertedMax.x, convertedMax.y, convertedMax.z);
            m_ELoadStatus = ELoadStatus.ELS_NOT_LOADED;
            if (m_CurrNavCore.LoadNavMesh(tnBox, ref m_ELoadStatus) == ECode.SUCCESS)
            {
                if (m_ELoadStatus == ELoadStatus.ELS_LOADED_TRAV_DATA_FOUND_IN_ROI)
                {
                    RemoveObject(m_BoundingBoxRect);
                    RemoveObject(m_BoundingBoxPaintRect);

                    m_BoundingBoxRect = null;
                    m_BoundingBoxPaintRect = null;

                    DNSMcVector3D[] locationPoints = { ctrlUserBoundingBox.GetBoxValue().MinVertex, ctrlUserBoundingBox.GetBoxValue().MaxVertex };

                    m_BoundingBoxRect = DrawRect(DNSMcBColor.bcBlackOpaque, locationPoints);
                    gbFindPathParams.Enabled = true;
                    gbFindPathCalculation.Enabled = true;

                    Manager_NavCore.BoundingBox = ctrlUserBoundingBox.GetBoxValue();
                }
                else
                {
                    MessageBox.Show("Failed to load the nav mesh: " + m_CurrNavCore.LastError + "status: " + m_ELoadStatus.ToString());
                }
            }
            else
            {
                MessageBox.Show("Failed to load the nav mesh: " + m_CurrNavCore.LastError + "status: " + m_ELoadStatus.ToString());
            }
        }

        private void CreateRect()
        {
            if (m_CurrActiveOverlay != null)
            {
                RemoveObject(m_BoundingBoxRect);
                RemoveObject(m_BoundingBoxPaintRect);

                m_BoundingBoxRect = null;
                m_BoundingBoxPaintRect = null;

                this.Hide();
                MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(InitItemResultsBoundingBox);
               // m_ExitStatus = 0;

                try
                {
                    IDNMcObjectSchemeItem ObjSchemeItem;
                    IDNMcObject obj;
                    PaintRect(out ObjSchemeItem, out obj);
                    m_BoundingBoxPaintRect = obj;

                    m_CurrEditMode.StartInitObject(obj, ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("StartInitObject", McEx);
                    this.Show();
                }
            }
        }

        public void InitItemResultsBoundingBox(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            DNSMcVector3D[] points = pObject.GetLocationPoints(0);
            //m_ReturnItem = pItem;
            if (points != null && points.Length == 2)
            {
                ctrlUserBoundingBox.SetBoxValue(new DNSMcBox(points[0], points[1]));
            }
            MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(null);
            MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent -= new InitItemResultsEventArgs(InitItemResultsBoundingBox);
            this.Show();
        }

        private void PaintRect(out IDNMcObjectSchemeItem ObjSchemeItem, out IDNMcObject obj)
        {

            IDNMcOverlay activeOverlay = m_CurrActiveOverlay;
            DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];

            DNEItemSubTypeFlags subTypeFlags = 0;
            subTypeFlags |= DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN;
            subTypeFlags |= DNEItemSubTypeFlags._EISTF_SCREEN;

            ObjSchemeItem = DNMcRectangleItem.Create(subTypeFlags,
                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                        DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT,
                                                        DNERectangleDefinition._ERD_RECTANGLE_DIAGONAL_POINTS,
                                                        0f,
                                                        0f,
                                                        DNELineStyle._ELS_SOLID,
                                                        DNSMcBColor.bcBlackOpaque,
                                                        2f,
                                                        null,
                                                        new DNSMcFVector2D(0, -1),
                                                        1f,
                                                        DNEFillStyle._EFS_SOLID,
                                                        new DNSMcBColor(0, 255, 100, 100));

            obj = DNMcObject.Create(activeOverlay,
                                    ObjSchemeItem,
                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                    locationPoints,
                                    false);

            //In order to prevent retrieval of the rectangle in the scan 
            ObjSchemeItem.Detectibility = false;
            ((IDNMcRectangleItem)ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
            ((IDNMcRectangleItem)ObjSchemeItem).SetDrawPriority(SByte.MaxValue, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false); 

            obj.SetDrawPriority(SByte.MaxValue);

        }

        private IDNMcObject DrawRect(DNSMcBColor color, DNSMcVector3D[] locationPoints)
        {
            if (m_CurrActiveOverlay != null)
            {
                IDNMcObject obj;

                DNEItemSubTypeFlags subTypeFlags = 0;
                subTypeFlags |= DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN;
                subTypeFlags |= DNEItemSubTypeFlags._EISTF_SCREEN;

                IDNMcObjectSchemeItem ObjSchemeItem = DNMcRectangleItem.Create(subTypeFlags,
                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                        DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT,
                                                        DNERectangleDefinition._ERD_RECTANGLE_DIAGONAL_POINTS,
                                                        0f,
                                                        0f,
                                                        DNELineStyle._ELS_SOLID,
                                                        color,
                                                        2f,
                                                        null,
                                                        new DNSMcFVector2D(0, -1),
                                                        1f,
                                                        DNEFillStyle._EFS_NONE,
                                                        new DNSMcBColor(0, 255, 100, 100));

                obj = DNMcObject.Create(m_CurrActiveOverlay,
                                        ObjSchemeItem,
                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                        locationPoints,
                                        false);

                return obj;
            }
            return null;
        }

        private void RemoveObject(IDNMcObject obj)
        {
            try
            {
                if (obj != null)
                {
                    obj.Dispose();
                    obj.Remove();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Remove Object", McEx);
            }
        }

        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();

            if (m_CurrActiveOverlay != null)
            {
                RemoveObject(m_UserPathObj);
                ResetPathObject();
                RemoveTraversabilityObjects();

                this.Hide();
                MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(InitItemResults);
                //m_ExitStatus = 0;

                try
                {
                    IDNMcLineItem ObjSchemeItem;
                    IDNMcObject obj;
                    CreateLine(out obj, out ObjSchemeItem);
                    m_UserPathItem = ObjSchemeItem;

                    m_CurrEditMode.StartInitObject(obj, ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("StartInitObject", McEx);
                    this.Show();
                }
            }
        }

        private void CreateLine(out IDNMcObject obj, out IDNMcLineItem ObjSchemeItem, DNSMcVector3D[] points = null)
        {

            DNSMcVector3D[] locationPoints;
            if (points != null)
                locationPoints = points;
            else
                locationPoints = new DNSMcVector3D[0];

            DNEItemSubTypeFlags subTypeFlags = 0;
            subTypeFlags |= DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN;
            subTypeFlags |= DNEItemSubTypeFlags._EISTF_SCREEN;
            DNEMcPointCoordSystem ePictureCoordinateSystem = DNEMcPointCoordSystem._EPCS_SCREEN;

            ObjSchemeItem = DNMcLineItem.Create(subTypeFlags,
                                                DNELineStyle._ELS_SOLID,
                                                DNSMcBColor.bcBlackOpaque,
                                                6f);

            IDNMcTexture texture = DNMcBitmapHandleTexture.Create(Icons.TesterVertex.GetHbitmap(), false, false, new DNSMcBColor(0, 0, 0, 255));
            IDNMcPictureItem picItem = DNMcPictureItem.Create(subTypeFlags, ePictureCoordinateSystem, texture);
            // picItem.SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

            obj = DNMcObject.Create(m_CurrActiveOverlay,
                                    ObjSchemeItem,
                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                    locationPoints,
                                    false);

            picItem.Connect(obj.GetScheme().GetObjectLocation(0));

            m_UserPathObj = obj;
            m_UserPathItem = ObjSchemeItem;
        }

        private void PaintRect(DNEItemSubTypeFlags rectItemSubTypeBitField, DNEMcPointCoordSystem rectRectangleCoordinateSystem, DNEMcPointCoordSystem objLocationCoordSystem)
        {
            try
            {
                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];


                IDNMcObjectSchemeItem ObjSchemeItem = DNMcRectangleItem.Create(rectItemSubTypeBitField,
                                                                                rectRectangleCoordinateSystem,
                                                                                DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT,
                                                                                DNERectangleDefinition._ERD_RECTANGLE_DIAGONAL_POINTS,
                                                                                0f,
                                                                                0f,
                                                                                DNELineStyle._ELS_SOLID,
                                                                                DNSMcBColor.bcBlackOpaque,
                                                                                2f,
                                                                                null,
                                                                                new DNSMcFVector2D(0, -1),
                                                                                1f,
                                                                                DNEFillStyle._EFS_SOLID,
                                                                                new DNSMcBColor(0, 255, 100, 100));

                IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                    ObjSchemeItem,
                                                    objLocationCoordSystem,
                                                    locationPoints,
                                                    false);

                //In order to prevent retrieval of the rectangle in the scan 
                ObjSchemeItem.Detectibility = false;
                ((IDNMcRectangleItem)ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                ((IDNMcRectangleItem)ObjSchemeItem).SetDrawPriority(SByte.MaxValue, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false); 

                uint[] activeViewportID = new uint[1];
                activeViewportID[0] = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport.ViewportID;
                IDNMcConditionalSelector viewportSelector = DNMcViewportConditionalSelector.Create(MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport.OverlayManager,
                                                                                                        DNEViewportType._EVT_ALL_VIEWPORTS,
                                                                                                        DNEViewportCoordinateSystem._EVCS_ALL_COORDINATE_SYSTEMS,
                                                                                                        activeViewportID,
                                                                                                        true);
                obj.SetConditionalSelector(DNEActionType._EAT_VISIBILITY,
                                            true,
                                            viewportSelector);
                obj.SetDrawPriority(SByte.MaxValue);

                MCTMapFormManager.MapForm.EditMode.StartInitObject(obj, ObjSchemeItem);

                /*m_EditMode = m_EditMode.GetEventsCallback();
                m_EditMode.SetEventsCallback(this);*/
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating rectangle item", McEx);
            }
        }

        public void InitItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            m_UserPathObj = pObject;
            m_UserPathItem = pItem;
            m_UserObjPoints = m_UserPathObj.GetLocationPoints(0);
            ctrlPointsGrid1.SetPoints(m_UserObjPoints);
            MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(null);
            MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent -= new InitItemResultsEventArgs(InitItemResults);
            this.Show();
        }

        private void ShowPoints()
        {
            IDNMcTexture texture = DNMcBitmapHandleTexture.Create(Icons.TesterVertex.GetHbitmap(), false, false, new DNSMcBColor(0, 0, 0, 255));
            IDNMcObjectSchemeItem picItem = DNMcPictureItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN, texture);
            ((IDNMcSymbolicItem)picItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

            // m_UserPathItem.
            // IDNMcObject m_ShowCoordObj = DNMcObject.Create(m_CurrActiveOverlay, picItem, , false);
            // m_ShowCoordObj.SetDetectibility(false);
        }

        private void btnFindPath_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();

            this.Cursor = Cursors.WaitCursor;
            SNavCorePointsArray navCorePoints = new SNavCorePointsArray();
            bool result = ctrlPointsGrid1.GetPoints(out m_UserObjPoints);

            ResetPathObject();
            RemoveTraversabilityObjects();

            if (result && m_UserObjPoints != null && m_UserObjPoints.Length > 0)
            {
                btnExportToCSV.Enabled = true;

                STnVector3D[] pointsTn = new STnVector3D[m_UserObjPoints.Length];
                for (int i = 0; i < m_UserObjPoints.Length; i++)
                {
                    DNSMcVector3D convertedPoint = ConvertPointBtoA(m_UserObjPoints[i]);
                    pointsTn[i] = new STnVector3D(convertedPoint.x, convertedPoint.y, convertedPoint.z);
                }

                m_FindPathECode = m_CurrNavCore.FindPath(pointsTn, ref navCorePoints, ref m_FindPathStatus);
                if (m_FindPathStatus != EFindPathStatus.EFPS_PATH_NOT_FOUND)
                {

                    Console.WriteLine(String.Format("Waypoints were built using {0} points", navCorePoints.m_uNumPoints.ToString()));
                    m_LocationPointsNavCoreViewport = Points2Vector(navCorePoints);
                    //Points2Csv(LocationPointsNavCoreViewport);
                    SaveParams();
                    if (m_LocationPointsNavCoreViewport != null)
                    {
                        if (m_NavCorePathObj != null)
                        {
                            m_NavCorePathObj.SetLocationPoints(m_LocationPointsNavCoreViewport, 0);
                            if(m_UserPathItem == null)
                            {
                                IDNMcLineItem ObjSchemeItem;
                                IDNMcObject obj;
                                CreateLine(out obj, out ObjSchemeItem);
                                m_UserPathItem = ObjSchemeItem;

                            }
                            m_UserPathItem.SetVisibilityOption(DNEActionOptions._EAO_FORCE_FALSE, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        }
                        btnChangeLineWidth.Enabled = true;

                        if (m_TraversabilityMapLayer != null && chxIsCallTraversabilityCalculationFunction.Checked)
                        {
                            DNSQueryParams queryParams = new DNSQueryParams();
                            queryParams.eTerrainPrecision = DNEQueryPrecision._EQP_DEFAULT;
                            IDNMcMapViewport mapViewport = m_CurrViewport;
                            if (m_StandaloneSQ == null)
                                m_TraversabilityPoints = mapViewport.GetTraversabilityAlongLine(m_TraversabilityMapLayer, m_LocationPointsNavCoreViewport, queryParams);
                            else
                                m_TraversabilityPoints = m_StandaloneSQ.GetTraversabilityAlongLine(m_TraversabilityMapLayer, m_LocationPointsNavCoreViewport);

                            if (m_TraversabilityPoints.Length > 0)
                            {
                                if (m_CurrActiveOverlay != null)
                                {
                                    IDNMcObjectSchemeItem visibleLineItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                                                                    DNELineStyle._ELS_SOLID,
                                                                                                    new DNSMcBColor(0, 255, 0, 255),
                                                                                                    10);

                                    IDNMcObjectSchemeItem nonVisibleLineItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                                                                    DNELineStyle._ELS_SOLID,
                                                                                                    new DNSMcBColor(255, 0, 0, 255),
                                                                                                    10);

                                    IDNMcObjectSchemeItem unknownLineItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                                                                   DNELineStyle._ELS_SOLID,
                                                                                                   DNSMcBColor.bcWhiteOpaque,
                                                                                                   10);

                                    IDNMcObjectScheme visibleScheme = DNMcObjectScheme.Create(m_CurrActiveOverlay.GetOverlayManager(),
                                                                                                    visibleLineItem,
                                                                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                                    false);

                                    IDNMcObjectScheme nonVisibleScheme = DNMcObjectScheme.Create(m_CurrActiveOverlay.GetOverlayManager(),
                                                                                                    nonVisibleLineItem,
                                                                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                                    false);

                                    IDNMcObjectScheme unknownScheme = DNMcObjectScheme.Create(m_CurrActiveOverlay.GetOverlayManager(),
                                                                                                    unknownLineItem,
                                                                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                                    false);
                                    Manager_MCObjectScheme.AddTempObjectScheme(visibleScheme);
                                    Manager_MCObjectScheme.AddTempObjectScheme(nonVisibleScheme);
                                    Manager_MCObjectScheme.AddTempObjectScheme(unknownScheme);

                                    ((IDNMcLineItem)visibleLineItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                                    ((IDNMcLineItem)visibleLineItem).SetOutlineWidth(2f, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                                    ((IDNMcLineItem)visibleLineItem).SetOutlineColor(DNSMcBColor.bcBlackOpaque, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                                    ((IDNMcLineItem)nonVisibleLineItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                                    ((IDNMcLineItem)nonVisibleLineItem).SetOutlineWidth(2f, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                                    ((IDNMcLineItem)nonVisibleLineItem).SetOutlineColor(DNSMcBColor.bcBlackOpaque, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                                    ((IDNMcLineItem)unknownLineItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                                    ((IDNMcLineItem)unknownLineItem).SetOutlineWidth(2f, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                                    ((IDNMcLineItem)unknownLineItem).SetOutlineColor(DNSMcBColor.bcBlackOpaque, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                                    DNSMcVector3D[] locationPt = new DNSMcVector3D[2];

                                    for (int i = 1; i < m_TraversabilityPoints.Length; i++)
                                    {
                                        locationPt[0] = m_TraversabilityPoints[i - 1].Point;
                                        locationPt[1] = m_TraversabilityPoints[i].Point;
                                        IDNMcObject traversabilityObj = null;
                                        if (m_TraversabilityPoints[i - 1].eTraversability == DNEPointTraversability._EPT_TRAVERSABLE)
                                        {
                                            traversabilityObj = DNMcObject.Create(m_CurrActiveOverlay,
                                                                                visibleScheme,
                                                                                locationPt);
                                        }
                                        else if(m_TraversabilityPoints[i - 1].eTraversability == DNEPointTraversability._EPT_UNTRAVERSABLE)
                                        {
                                            traversabilityObj = DNMcObject.Create(m_CurrActiveOverlay,
                                                                                nonVisibleScheme,
                                                                                locationPt);
                                        }
                                        else if (m_TraversabilityPoints[i - 1].eTraversability == DNEPointTraversability._EPT_UNKNOWN)
                                        {
                                            traversabilityObj = DNMcObject.Create(m_CurrActiveOverlay,
                                                                                unknownScheme,
                                                                                locationPt);
                                        }
                                        m_ListTraversabilityObjects.Add(traversabilityObj);
                                    }
                                }
                            }
                        }
                    }
                    if (sender != null)
                        MessageBox.Show(string.Format("Find path return status {0} {1}, return {2} points.", m_FindPathStatus.ToString(), Environment.NewLine, navCorePoints.m_uNumPoints.ToString()));
                }
                else
                {
                    Console.WriteLine("Failed to build waypoints");
                    MessageBox.Show(string.Format("Find path return status {0}", m_FindPathStatus.ToString()));
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void SaveParams()
        {
            Manager_NavCore.SoilCost = ntxSoilCost.GetFloat();
            Manager_NavCore.RoadsCost = ntxRoadsCost.GetFloat();
            Manager_NavCore.LimitedPassabilityAreasCost = ntxLimitedPassabilityAreasCost.GetFloat();
            Manager_NavCore.VeryLimitedPassabilityAreasCost = ntxVeryLimitedPassabilityAreasCost.GetFloat();
        }

        DNSMcVector3D[] Points2Vector(SNavCorePointsArray points)
        {
            DNSMcVector3D[] retval = null;

            if (points.m_uNumPoints > 0)
            {
                try
                {
                    if (!UnmanagedArray2Struct(points.m_aPoints, points.m_uNumPoints, out retval))
                    {
                        throw new Exception("Failed to get the path points");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    retval = null;
                }
            }

            return retval;
        }

        private bool UnmanagedArray2Struct(IntPtr unmanagedArray, uint length, out DNSMcVector3D[] mangagedArray)
        {
            try
            {
                var size = Marshal.SizeOf(typeof(DNSMcVector3D));
                DNSMcVector3D[] array = new DNSMcVector3D[length];
                mangagedArray = new DNSMcVector3D[length];

                for (int i = 0; i < length; i++)
                {
                    IntPtr ins = new IntPtr(unmanagedArray.ToInt64() + i * size);
                    array[i] = (DNSMcVector3D)Marshal.PtrToStructure(ins, typeof(DNSMcVector3D));
                    mangagedArray[i] = ConvertPointAtoB(array[i]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                mangagedArray = null;
                return false;
            }
            return true;
        }

        public void ResetPathObject()
        {
            if (m_NavCorePathObj != null)
            {
                m_NavCorePathObj.Remove();
                m_NavCorePathObj = null;
                m_NavCorePathItem = null;
            }

            // create a line item and object
            m_NavCorePathItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN);
            m_NavCorePathObj = DNMcObject.Create(m_CurrActiveOverlay, m_NavCorePathItem, DNEMcPointCoordSystem._EPCS_WORLD, new DNSMcVector3D[] { }, true);
            m_NavCorePathObj.SetVisibilityOption(DNEActionOptions._EAO_FORCE_FALSE);

            // set line properties
            m_NavCorePathItem.SetLineColor(new DNSMcBColor(0, 0, 255, 255), 1, false);
            btnChangeLineWidth_Click(null, null);
            //m_NavCorePathItem.SetLineWidth(10, 2, false);
            //m_NavCorePathItem.SetLineStyle(DNELineStyle._ELS_DASH, 3, false);
            m_NavCorePathItem.SetOutlineWidth(2f, 4, false);

            // set visibility
            m_NavCorePathObj.SetVisibilityOption(DNEActionOptions._EAO_FORCE_TRUE);
        }

        public void RemoveTraversabilityObjects()
        {
            for (int i = 0; i < m_ListTraversabilityObjects.Count; i++)
            {
                IDNMcObject mcObject = m_ListTraversabilityObjects[i];
                mcObject.Remove();
                mcObject = null;
            }
            m_ListTraversabilityObjects.Clear();
        }

        private void btnTraversabilityMapLayer_Click(object sender, EventArgs e)
        {
            try
            {
                if (chxIsServerMapLayer.Checked)
                {
                    m_TraversabilityMapLayer = (IDNMcTraversabilityMapLayer)Manager_MCLayers.CreateNativeServerTraversabilityLayer(ctrlBrowseTraversabilityMapLayer.FileName, false);
                }
                else
                {
                    m_TraversabilityMapLayer = (IDNMcTraversabilityMapLayer)Manager_MCLayers.CreateNativeTraversabilityLayer(ctrlBrowseTraversabilityMapLayer.FileName, false);
                }

                if (m_TraversabilityMapLayer != null)
                {
                    Manager_NavCore.TraversabilityFolderPath = ctrlBrowseTraversabilityMapLayer.FileName;
                    Manager_NavCore.IsCreateStandaloneSpatialQueries = chxIsCreateStandaloneSpatialQueries.Checked;


                    if (!chxIsCreateStandaloneSpatialQueries.Checked)
                    {
                        if (m_StandaloneSQ != null)
                            m_StandaloneSQ.Dispose();
                        m_StandaloneSQ = null;
                        if(m_CurrMapTerrain == null && m_CurrViewport != null)
                        {
                            m_CurrMapTerrain = DNMcMapTerrain.Create(m_TraversabilityMapLayer.CoordinateSystem, new IDNMcMapLayer[1] { m_TraversabilityMapLayer });
                            m_CurrViewport.AddTerrain(m_CurrMapTerrain);
                        }
                        if (m_CurrMapTerrain != null)
                        {
                            m_CurrMapTerrain.AddLayer(m_TraversabilityMapLayer);
                        }
                        else
                        {
                            MessageBox.Show("Missing viewport or terrain", "Create Map Layer");
                            return;
                        }
                    }
                    else
                    {
                        CreateSpatialQueries();
                    }
                    SetLayerParams();

                    btnRemoveMapLayer.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Missing traversability map layer", "Create Map Layer");
                    return;
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Create Traversability Layer", McEx);
            }
        }

        private void SetLayerParams()
        {
            try
            {
                DNSLayerParams layerParams = new DNSLayerParams();
                layerParams.nDrawPriority = 10;
                layerParams.byTransparency = ntxTransparency.GetByte();

                if (m_TraversabilityMapLayer != null)
                {
                    if (!chxIsCreateStandaloneSpatialQueries.Checked)
                    {
                        m_CurrMapTerrain.SetLayerParams(m_TraversabilityMapLayer, layerParams);
                    }
                    else
                    {
                        CreateSpatialQueries();
                        m_CurrStandaloneMapTerrain.SetLayerParams(m_TraversabilityMapLayer, layerParams);
                    }

                    Manager_NavCore.TraversabilityTransparency = ntxTransparency.GetByte();
                }
                else
                {
                    MessageBox.Show("Missing terrain or viewport", "Create Map Layer");
                    return;
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetLayerParams", McEx);
            }

        }

        private void btnRemoveMapLayer_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_TraversabilityMapLayer != null)
                {
                    if (m_CurrMapTerrain != null)
                    {
                        m_CurrMapTerrain.RemoveLayer(m_TraversabilityMapLayer);
                    }
                    else  // standalone
                    {
                        m_StandaloneSQ.GetTerrains()[0].RemoveLayer(m_TraversabilityMapLayer);
                        m_StandaloneSQ.Dispose();
                        m_StandaloneSQ = null;
                    }
                    m_TraversabilityMapLayer.Dispose();
                    m_TraversabilityMapLayer = null;
                    btnRemoveMapLayer.Enabled = false;

                    Manager_NavCore.TraversabilityFolderPath = "";
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Remove Traversability Layer", McEx);
            }
        }

        private void CheckIsChangeCurrentViewport()
        {
            if (chxIsCreateStandaloneSpatialQueries.Checked & (m_CurrViewport != null &&
                    (MCTMapFormManager.MapForm == null || (MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport == null))))
            {
                m_CurrViewport = null;
                m_CurrActiveOverlay = null;
                m_CurrEditMode = null;
            }

            if (MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport != null && m_CurrViewport == null)
            {
                SetCurrentViewportParams();
            }
        }

        private void SetCurrentViewportParams()
        {
            m_CurrViewport = MCTMapFormManager.MapForm.Viewport;
            m_CurrActiveOverlay = Manager_MCOverlayManager.GetActiveOverlayOfOverlayManager(m_CurrViewport.OverlayManager);
            m_CurrEditMode = MCTMapFormManager.MapForm.EditMode;
        }

        private void HandleError(ErrorCode errorCode, string msgCode)
        {
            MessageBox.Show(string.Format("Import operation fails. {0} '{1}' value.", errorCode == ErrorCode.invalid ? "Invalid" : "Compare data fails in", msgCode));
            m_IsSameAfterImport = !(errorCode == ErrorCode.compare);
        }

        private DNSMcBox GetBoundingBoxFromFile(StreamReader streamReader)
        {
            string[] readerData;

            DNSMcBox boundingBox = new DNSMcBox();
            double dresult1 = 0;
            double dresult2 = 0;
            double dresult3 = 0;
            for (int i = 0; i < 2; i++)
            {
                readerData = streamReader.ReadLine().Split(m_Seperator);
                if (readerData != null && readerData.Length > 3)
                {
                    bool IsParseSucc = double.TryParse(readerData[1], out dresult1) && double.TryParse(readerData[2], out dresult2) && double.TryParse(readerData[3], out dresult3);
                    if (IsParseSucc == true)
                    {
                        switch (i)
                        {
                            case 0:
                                boundingBox.MinVertex = new DNSMcVector3D(dresult1, dresult2, dresult3);
                                break;
                            case 1:
                                boundingBox.MaxVertex = new DNSMcVector3D(dresult1, dresult2, dresult3);
                                break;
                        }
                    }
                }
            }
            return boundingBox;
        }

        private DNSMcVector3D[] GetPointsFromFile(StreamReader streamReader, out DNSTraversabilityPoint[] traversabilityPoints, bool isTrav = false)
        {
            double dresult = 0;
            traversabilityPoints = null;
            string[] readerData = streamReader.ReadLine().Split(m_Seperator);
            if (readerData != null && readerData.Length > 1)
            {
                int numPoints;
                bool IsParseSucc = int.TryParse(readerData[1], out numPoints);
                if (IsParseSucc)
                {
                    DNSMcVector3D[] points = new DNSMcVector3D[numPoints];
                    traversabilityPoints = new DNSTraversabilityPoint[numPoints];
                    for (int i = 0; i < numPoints; i++)
                    {
                        DNSMcVector3D point = new DNSMcVector3D();
                        readerData = streamReader.ReadLine().Split(m_Seperator);
                        int numCol = isTrav ? 4 : 3;
                        for (int j = 0; j < numCol; j++)
                        {
                            if (readerData != null && readerData.Length > 2)
                            {
                                IsParseSucc = double.TryParse(readerData[j], out dresult);
                                if (IsParseSucc == true || (j == 3 && isTrav))
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            point.x = dresult;
                                            break;
                                        case 1:
                                            point.y = dresult;
                                            break;
                                        case 2:
                                            point.z = dresult;
                                            break;
                                        case 3:
                                            if (isTrav)
                                            {
                                                DNSTraversabilityPoint traversabilityPoint = new DNSTraversabilityPoint();
                                                traversabilityPoint.Point = point;

                                                DNEPointTraversability ePointTraversability;
                                                try
                                                {
                                                    if (Enum.TryParse(readerData[j], out ePointTraversability))
                                                        traversabilityPoint.eTraversability = ePointTraversability;
                                                    else
                                                        HandleError(ErrorCode.invalid, "Traversability Point eTraversability value");
                                                }
                                                catch (ArgumentException) {
                                                    HandleError(ErrorCode.invalid, "Traversability Point eTraversability value");
                                                }
                                                
                                                traversabilityPoints[i] = traversabilityPoint;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        points[i] = point;
                    }
                    return points;
                }
            }
            return null;
        }

        private ErrorCode CheckText(string[] readerData, string dataToCompare, string fieldName)
        {
            if (readerData != null && readerData.Length > 1)
            {
                if (readerData[1] != dataToCompare)
                {
                    HandleError(ErrorCode.compare, fieldName);
                    m_IsSameAfterImport = false;
                    return ErrorCode.compare;
                }
            }
            else
            {
                HandleError(ErrorCode.invalid, fieldName);
                return ErrorCode.invalid;
            }
            return ErrorCode.none;
        }

        private bool CheckSetParams(string fieldName, string perviousECode, string newECode)
        {
            if (perviousECode != newECode)
            {
                MessageBox.Show(string.Format("Compare data fails, {0} return different from data file." +
                    " {0} is {1}, data file is {2}", fieldName, newECode, perviousECode));
                m_IsSameAfterImport = false;

                return false;
            }
            return true;
        }

        // 
        private bool IsVector3DSame(DNSMcVector3D mcVector3D1, DNSMcVector3D mcVector3D2)
        {
            return (Math.Round(mcVector3D1.x, 4) == Math.Round(mcVector3D2.x, 4) &&
                Math.Round(mcVector3D1.y, 4) == Math.Round(mcVector3D2.y, 4) &&
                Math.Round(mcVector3D1.z, 4) == Math.Round(mcVector3D2.z, 4));
        }

        private bool IsVectorArraysSame(DNSMcVector3D[] mcVector3Ds1, DNSMcVector3D[] mcVector3Ds2)
        {
            bool isSame = false;
            if (mcVector3Ds1 == null || mcVector3Ds2 == null)
                return false;

            if (mcVector3Ds1.Length == mcVector3Ds2.Length)
            {
                isSame = true;
                int len = mcVector3Ds1.Length;
                for (int i = 0; i < len; i++)
                {
                    if (!IsVector3DSame(mcVector3Ds1[i], mcVector3Ds2[i]))
                    {
                        isSame = false;
                        break;
                    }
                }
            }
            return isSame;
        }

        private bool IsTraversabilityArraysSame(DNSTraversabilityPoint[] mcVector3Ds1, DNSTraversabilityPoint[] mcVector3Ds2)
        {
            bool isSame = false;
            if (mcVector3Ds1 == null || mcVector3Ds2 == null)
                return false;
            if (mcVector3Ds1.Length == mcVector3Ds2.Length)
            {
                isSame = true;
                int len = mcVector3Ds1.Length;
                for (int i = 0; i < len; i++)
                {
                    if ((!IsVector3DSame(mcVector3Ds1[i].Point, mcVector3Ds2[i].Point)) ||
                        (mcVector3Ds1[i].eTraversability != mcVector3Ds2[i].eTraversability))
                    {
                        isSame = false;
                        break;
                    }
                }
            }
            return isSame;
        }

        private void btnImportFromCSV_Click(object sender, EventArgs e)
        {
            try
            {
                m_IsSameAfterImport = true;
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    StreamReader streamReader = new StreamReader(ofd.FileName);
                    string[] readerData;
                    string line = "";
                    List<DNSMcVector3D> user_points = new List<DNSMcVector3D>();
                    readerData = streamReader.ReadLine().Split(m_Seperator);          // Nav Mesh Folder Path
                    if (readerData != null && readerData.Length > 1)
                    {
                        ctrlBrowseNavMeshFolder.FileName = readerData[1];
                        btnCreate_Click(null, null);
                        readerData = streamReader.ReadLine().Split(m_Seperator);      // Get Coord Sys Epsg Code

                        if (CheckText(readerData, txtCoordSysEpsg.Text, "Coord Sys Epsg Code") == ErrorCode.invalid)
                            return;

                        DNSMcBox mcBox = GetBoundingBoxFromFile(streamReader);

                        if (mcBox != ctrlNavBoundingBox.GetBoxValue())
                        {
                            HandleError(ErrorCode.compare, "Nav Mesh Bounding Box");
                            m_IsSameAfterImport = false;
                        }

                        readerData = streamReader.ReadLine().Split(m_Seperator);      // Get Max Roi Area Supported
                        if (CheckText(readerData, ntbMaxRoiAreaSupported.Text, "Max Roi Area Supported") == ErrorCode.invalid)
                            return;

                        readerData = streamReader.ReadLine().Split(m_Seperator);      // Is Server Traversability Map Layer
                        if (readerData != null && readerData.Length > 1)
                        {
                            if (readerData[1].ToLower() == Boolean.TrueString.ToLower())
                            {
                                chxIsServerMapLayer.Checked = true;
                            }
                        }
                        else
                        {
                            HandleError(ErrorCode.invalid, "Is Server Traversability Map Layer");
                            return;
                        }
                        readerData = streamReader.ReadLine().Split(m_Seperator);      // Is Create Standalone Spatial Queries
                        if (readerData != null && readerData.Length > 1)
                        {
                            if (readerData[1].ToLower() == Boolean.TrueString.ToLower())
                            {
                                chxIsCreateStandaloneSpatialQueries.Checked = true;
                            }
                        }
                        else
                        {
                            HandleError(ErrorCode.invalid, "Is Create Standalone Spatial Queries");
                            return;
                        }
                        readerData = streamReader.ReadLine().Split(m_Seperator);      // Is Used Traversability Map Layer
                        line = streamReader.ReadLine();
                        bool isUsedTraversabilityMapLayer = false;
                        if (readerData != null && readerData.Length > 1)
                        {
                            if (readerData[1].ToLower() == Boolean.TrueString.ToLower())
                            {
                                isUsedTraversabilityMapLayer = true;
                                readerData = line.Split(m_Seperator);
                                if (readerData != null && readerData.Length > 1)
                                {
                                    ctrlBrowseTraversabilityMapLayer.FileName = readerData[1];
                                    btnTraversabilityMapLayer_Click(null, null);
                                }
                                else
                                {
                                    HandleError(ErrorCode.invalid, "Traversability Map Layer Folder Path");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            HandleError(ErrorCode.invalid, "Is Used Traversability Map Layer");
                            return;
                        }
                        
                        readerData = streamReader.ReadLine().Split(m_Seperator);   // Traversability Transparency
                        if (readerData != null && readerData.Length > 1)
                        {
                            byte bResult;
                            if (byte.TryParse(readerData[1], out bResult))
                                ntxTransparency.SetByte(bResult);
                            else
                            {
                                //HandleError(ErrorCode.invalid, "Traversability Transparency");
                                ntxTransparency.SetByte(Manager_NavCore.TraversabilityTransparency);
                            }
                        }
                        else
                        {
                            HandleError(ErrorCode.invalid, "Traversability Transparency");
                            //return;
                        }

                        DNSMcBox userBox = GetBoundingBoxFromFile(streamReader);
                        ctrlUserBoundingBox.SetBoxValue(new DNSMcBox(ConvertPointAtoB(userBox.MinVertex), ConvertPointAtoB(userBox.MaxVertex)));
                        btnBuild_Click(null, null);

                        readerData = streamReader.ReadLine().Split(m_Seperator);                  // Load Status
                        if (CheckText(readerData, m_ELoadStatus.ToString(), "Load Status") == ErrorCode.invalid)
                            return;

                        // Set Params
                        string setSoilCostECode = "";
                        string setRoadsCostECode = "";
                        string setLimitedPassabilityAreasCostECode = "";
                        string setVeryLimitedPassabilityAreasCostECode = "";

                        for (int i = 0; i < 4; i++)
                        {
                            // get value
                            readerData = streamReader.ReadLine().Split(m_Seperator);
                            if (readerData != null && readerData.Length > 3)
                            {
                                int iresult;
                                bool IsParseSucc = int.TryParse(readerData[1], out iresult);
                                switch (i)
                                {
                                    case 0:
                                        ntxSoilCost.SetInt(iresult);
                                        setSoilCostECode = readerData[3];
                                        break;
                                    case 1:
                                        ntxRoadsCost.SetInt(iresult);
                                        setRoadsCostECode = readerData[3];
                                        break;
                                    case 2:
                                        ntxLimitedPassabilityAreasCost.SetInt(iresult);
                                        setLimitedPassabilityAreasCostECode = readerData[3];
                                        break;
                                    case 3:
                                        ntxVeryLimitedPassabilityAreasCost.SetInt(iresult);
                                        setVeryLimitedPassabilityAreasCostECode = readerData[3];
                                        break;
                                }
                            }
                            else
                            {
                                HandleError(ErrorCode.invalid, "Get/Set Params");
                                return;
                            }
                        }
                        btnSetParams_Click(null, null);

                        CheckSetParams("SetSoilCostECode", setSoilCostECode, m_SetSoilCostECode.ToString());
                        CheckSetParams("SetRoadsCostECode", setRoadsCostECode, m_SetRoadsCostECode.ToString());
                        CheckSetParams("SetLimitedPassabilityAreasCostECode", setLimitedPassabilityAreasCostECode, m_SetLimitedPassabilityAreasCostECode.ToString());
                        CheckSetParams("SetVeryLimitedPassabilityAreasCostECode", setVeryLimitedPassabilityAreasCostECode, m_SetVeryLimitedPassabilityAreasCostECode.ToString());

                        readerData = streamReader.ReadLine().Split(m_Seperator);   // Line Width
                        if (readerData != null && readerData.Length > 1)
                        {
                            float fResult;
                            if(float.TryParse(readerData[1], out fResult))
                                ntxResultLineWidth.SetFloat(fResult);
                        }
                        else
                        {
                            HandleError(ErrorCode.invalid, "Line Width");
                            //return;
                        }
                        
                        readerData = streamReader.ReadLine().Split(m_Seperator);      // Is Call Traversability Map Layer
                        if (readerData != null && readerData.Length > 1)
                        {
                            if (readerData[1].ToLower() == Boolean.TrueString.ToLower())
                            {
                                chxIsCallTraversabilityCalculationFunction.Checked = true;
                            }
                        }
                        else
                        {
                            HandleError(ErrorCode.invalid, "Is Call Traversability Map Layer");
                            //return;
                        }

                        DNSTraversabilityPoint[] traversabilityPoints = null;
                        DNSMcVector3D[] userPoints = ConvertArrayPointsAtoB(GetPointsFromFile(streamReader, out traversabilityPoints));
                        if (userPoints != null)
                        {
                            if (m_CurrActiveOverlay != null)
                            {
                                RemoveObject(m_UserPathObj);
                                ResetPathObject();

                                IDNMcObject obj = null;
                                IDNMcLineItem ObjSchemeItem = null;
                                CreateLine(out obj, out ObjSchemeItem, userPoints);
                                m_UserPathObj = obj;
                            }
                            m_UserObjPoints = userPoints;
                            ctrlPointsGrid1.SetPoints(m_UserObjPoints);
                            btnFindPath_Click(null, null);

                            DNSMcVector3D[] navCorePoints = ConvertArrayPointsAtoB(GetPointsFromFile(streamReader, out traversabilityPoints));
                            if (navCorePoints != null && !IsVectorArraysSame(navCorePoints, m_LocationPointsNavCoreViewport))
                                m_IsSameAfterImport = false;
                            //MessageBox.Show("Compare data fails, Nav Core points return different from data file.");

                            readerData = streamReader.ReadLine().Split(m_Seperator);         // Find Path ECode
                            if (CheckText(readerData, m_FindPathECode.ToString(), "Find Path ECode") == ErrorCode.invalid)
                                return;

                            readerData = streamReader.ReadLine().Split(m_Seperator);         // Find Path Status
                            if (CheckText(readerData, m_FindPathStatus.ToString(), "Find Path Status") == ErrorCode.invalid)
                                return;

                            if (isUsedTraversabilityMapLayer)
                            {
                                GetPointsFromFile(streamReader, out traversabilityPoints, true);
                                for (int i = 0; i < traversabilityPoints.Length; i++)
                                {
                                    traversabilityPoints[i].Point = ConvertPointAtoB(traversabilityPoints[i].Point);
                                }
                                if (!IsTraversabilityArraysSame(traversabilityPoints, m_TraversabilityPoints))
                                    m_IsSameAfterImport = false;
                                // MessageBox.Show("Compare data fails, traversability points return different from data file.");
                            }

                            if (m_IsSameAfterImport)
                                MessageBox.Show("Import data from file succeed, No differences were found.");
                            else
                                MessageBox.Show("Import data from file succeed, differences were found.");

                            string filename = Path.GetFileNameWithoutExtension(ofd.FileName);
                            string filePath = Path.GetDirectoryName(ofd.FileName);
                            string newFilePath = Path.Combine(filePath, filename) + "_Output.csv";

                            ExportToCSV(newFilePath);
                        }
                        else
                        {
                            HandleError(ErrorCode.invalid, "Get Line Points");
                            return;
                        }
                    }
                    streamReader.Close();
                }
            }
            catch (ArgumentException ex)
            { MessageBox.Show(String.Format("Error open file , reason - {0}", ex.Message)); }
            catch (FileNotFoundException ex)
            { MessageBox.Show(String.Format("Error open file , reason - {0}", ex.Message)); }
            catch (DirectoryNotFoundException ex)
            { MessageBox.Show(String.Format("Error open file , reason - {0}", ex.Message)); }
            catch (IOException ex)
            { MessageBox.Show(String.Format("Error open file , reason - {0}", ex.Message)); }


        }

        private static void CloneDirectory(string sourceDir, string destDir)
        {
            string dirName = new DirectoryInfo(sourceDir).Name;
            string newDirName = Path.Combine(destDir, dirName);
            if (!Directory.Exists(newDirName))
            {
                Directory.CreateDirectory(newDirName);
            }

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                File.Copy(file, Path.Combine(newDirName, Path.GetFileName(file)), true);
            }
            foreach (var directory in Directory.GetDirectories(sourceDir))
            {
                string dirNameInner = Path.GetFileName(directory);
                string fullPath = Path.Combine(newDirName, dirNameInner);
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                CloneDirectory(directory, newDirName);
            }
        }

        private void btnExportToCSV_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV File (*.csv) | *.csv";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ExportToCSV(sfd.FileName);
            }
        }

        private void ExportToCSV(string filePath)
        {
            try
            {
                StreamWriter stw = new StreamWriter(filePath);
                stw.WriteLine(String.Format("Nav Mesh Folder Path,{0}", ctrlBrowseNavMeshFolder.FileName));
                stw.WriteLine(String.Format("Get Coord Sys Epsg Code,{0}", txtCoordSysEpsg.Text));
                stw.WriteLine(String.Format("Get Nav Bounding Box Min Point,{0},{1},{2}", ctrlNavBoundingBox.GetBoxValue().MinVertex.x, ctrlNavBoundingBox.GetBoxValue().MinVertex.y, ctrlNavBoundingBox.GetBoxValue().MinVertex.z));
                stw.WriteLine(String.Format("Get Nav Bounding Box Max Point,{0},{1},{2}", ctrlNavBoundingBox.GetBoxValue().MaxVertex.x, ctrlNavBoundingBox.GetBoxValue().MaxVertex.y, ctrlNavBoundingBox.GetBoxValue().MaxVertex.z));
                stw.WriteLine(String.Format("Get Max Roi Area Supported,{0}", ntbMaxRoiAreaSupported.GetDouble()));
                bool isUsedTraversabilityMapLayer = m_TraversabilityMapLayer == null ? false : true;
                stw.WriteLine(String.Format("Is Server Traversability Map Layer,{0}", chxIsServerMapLayer.Checked.ToString()));
                stw.WriteLine(String.Format("Is Create Standalone Spatial Queries,{0}", m_StandaloneSQ != null ? Boolean.TrueString : Boolean.FalseString));
                stw.WriteLine(String.Format("Is Used Traversability Map Layer,{0}", isUsedTraversabilityMapLayer.ToString()));
                stw.WriteLine(String.Format("Traversability Map Layer Folder Path,{0}", isUsedTraversabilityMapLayer ? ctrlBrowseTraversabilityMapLayer.FileName : ""));
                stw.WriteLine(String.Format("Traversability Transparency,{0}", ntxTransparency.GetByte()));
                DNSMcBox convertedUserBoundingBox = new DNSMcBox();
                convertedUserBoundingBox.MinVertex = ConvertPointBtoA(ctrlUserBoundingBox.GetBoxValue().MinVertex);
                convertedUserBoundingBox.MaxVertex = ConvertPointBtoA(ctrlUserBoundingBox.GetBoxValue().MaxVertex);
                stw.WriteLine(String.Format("User Bounding Box Min Point,{0},{1},{2}", convertedUserBoundingBox.MinVertex.x.ToString(), convertedUserBoundingBox.MinVertex.y.ToString(), convertedUserBoundingBox.MinVertex.z.ToString()));
                stw.WriteLine(String.Format("User Bounding Box Max Point,{0},{1},{2}", convertedUserBoundingBox.MaxVertex.x.ToString(), convertedUserBoundingBox.MaxVertex.y.ToString(), convertedUserBoundingBox.MaxVertex.z.ToString()));
                stw.WriteLine(String.Format("Load Status,{0}", m_ELoadStatus.ToString()));
                stw.WriteLine(String.Format("Soil Cost,{0},Set Soil Cost ECode,{1}", ntxSoilCost.GetInt32(), m_SetSoilCostECode.ToString()));
                stw.WriteLine(String.Format("Roads Cost,{0},Set Roads Cost ECode,{1}", ntxRoadsCost.GetInt32(), m_SetRoadsCostECode.ToString()));
                stw.WriteLine(String.Format("Limited Passability Areas Cost,{0},Set Limited Passability Areas Cost ECode,{1}", ntxLimitedPassabilityAreasCost.GetInt32(), m_SetLimitedPassabilityAreasCostECode.ToString()));
                stw.WriteLine(String.Format("Very Limited Passability Areas Cost,{0},Set Very Limited Passability Areas Cost ECode,{1}", ntxVeryLimitedPassabilityAreasCost.GetInt32(), m_SetVeryLimitedPassabilityAreasCostECode.ToString()));
                stw.WriteLine(string.Format("Line Width,{0}", ntxResultLineWidth.GetFloat()));
                stw.WriteLine(String.Format("Is Call Traversability Map Layer,{0}", chxIsCallTraversabilityCalculationFunction.Checked.ToString()));
                stw.WriteLine(string.Format("Line Points,{0}", m_UserObjPoints == null ? 0 : m_UserObjPoints.Length));
                AddPointsToFile(stw, m_UserObjPoints);
                stw.WriteLine(string.Format("Nav Base Results Points,{0}", m_LocationPointsNavCoreViewport == null ? 0 : m_LocationPointsNavCoreViewport.Length));
                AddPointsToFile(stw, m_LocationPointsNavCoreViewport);

                stw.WriteLine(String.Format("Find Path ECode,{0}", m_FindPathECode));
                stw.WriteLine(String.Format("Find Path Status,{0}", m_FindPathStatus));

                if (m_TraversabilityMapLayer != null)
                {
                    stw.WriteLine(string.Format("traversability points,{0}", m_TraversabilityPoints == null ? 0 : m_TraversabilityPoints.Length));
                    if (m_TraversabilityPoints != null)
                    {
                        foreach (DNSTraversabilityPoint traversabilityPoint in m_TraversabilityPoints)
                        {
                            DNSMcVector3D convertedPoint = ConvertPointBtoA(traversabilityPoint.Point);
                            stw.WriteLine(string.Format("{0},{1},{2},{3}", convertedPoint.x.ToString(), convertedPoint.y.ToString(), convertedPoint.z.ToString(), traversabilityPoint.eTraversability.ToString()));
                        }
                    }
                }

                stw.Close();

                string path = filePath.Replace(Path.GetFileName(filePath), "");

                if (chxIsCopyNavMeshFolder.Checked)
                {
                    CloneDirectory(ctrlBrowseNavMeshFolder.FileName, path);
                }
                if (chxIsCopyTraversabilityMapLayerFolder.Checked && m_TraversabilityMapLayer != null)
                {
                    CloneDirectory(ctrlBrowseTraversabilityMapLayer.FileName, path);
                }
            }
            catch (ArgumentException ex)
            { MessageBox.Show(String.Format("Error open file , reason - {0}", ex.Message)); }
            catch (FileNotFoundException ex)
            { MessageBox.Show(String.Format("Error open file , reason - {0}", ex.Message)); }
            catch (DirectoryNotFoundException ex)
            { MessageBox.Show(String.Format("Error open file , reason - {0}", ex.Message)); }
            catch (IOException ex)
            { MessageBox.Show(String.Format("Error open file , reason - {0}", ex.Message)); }

        }

        private void AddPointsToFile(StreamWriter streamWriter, DNSMcVector3D[] points)
        {
            if (points != null)
            {
                foreach (DNSMcVector3D point in points)
                {
                    DNSMcVector3D convertedPoint = ConvertPointBtoA(point);
                    streamWriter.WriteLine(string.Format("{0},{1},{2}", convertedPoint.x.ToString(), convertedPoint.y.ToString(), convertedPoint.z.ToString()));
                }
            }
        }

        private void btnSetParams_Click(object sender, EventArgs e)
        {
            m_SetSoilCostECode = m_CurrNavCore.SetSoilCost(ntxSoilCost.GetFloat());
            m_SetRoadsCostECode = m_CurrNavCore.SetRoadsCost(ntxRoadsCost.GetFloat());
            m_SetLimitedPassabilityAreasCostECode = m_CurrNavCore.SetLimitedPassabilityAreasCost(ntxLimitedPassabilityAreasCost.GetFloat());
            m_SetVeryLimitedPassabilityAreasCostECode = m_CurrNavCore.SetVeryLimitedPassabilityAreasCost(ntxVeryLimitedPassabilityAreasCost.GetFloat());
            if (m_SetSoilCostECode == ECode.SUCCESS && m_SetRoadsCostECode == ECode.SUCCESS &&
                m_SetLimitedPassabilityAreasCostECode == ECode.SUCCESS && m_SetVeryLimitedPassabilityAreasCostECode == ECode.SUCCESS)
            {
                btnDrawLine.Enabled = true;
                gbFindPath.Enabled = true;

                SaveParams();
            }
            else
            {
                MessageBox.Show("Set Params Fails,results: + " + Environment.NewLine
                   + "SetSoilCostECode = " + m_SetSoilCostECode.ToString() + Environment.NewLine
                   + "SetRoadsCostECode = " + m_SetRoadsCostECode.ToString() + Environment.NewLine
                   + "SetLimitedPassabilityAreasCostECode = " + m_SetLimitedPassabilityAreasCostECode.ToString() + Environment.NewLine
                   + "SetVeryLimitedPassabilityAreasCostECode = " + m_SetVeryLimitedPassabilityAreasCostECode.ToString());
            }
        }

   
        private DNSMcVector3D ConvertPointAtoB(DNSMcVector3D point)
        {
            DNSMcVector3D convertedPoint = new DNSMcVector3D();
            int zone;
            if (m_GridConverter != null)
                m_GridConverter.ConvertAtoB(point, out convertedPoint, out zone);
            return convertedPoint;
        }

        private DNSMcVector3D ConvertPointBtoA(DNSMcVector3D point)
        {
            DNSMcVector3D convertedPoint = new DNSMcVector3D();
            int zone;
            if (m_GridConverter != null)
                m_GridConverter.ConvertBtoA(point, out convertedPoint, out zone);
            return convertedPoint;
        }

        private DNSMcVector3D[] ConvertArrayPointsAtoB(DNSMcVector3D[] points)
        {
            if (points != null)
            {
                DNSMcVector3D[] convertedPoint = new DNSMcVector3D[points.Length];

                for (int i = 0; i < points.Length; i++)
                {
                    convertedPoint[i] = ConvertPointAtoB(points[i]);
                }
                return convertedPoint;
            }
            return null;
        }



        private void btnLinePointsUpdate_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();

            DNSMcVector3D[] locationPoints;
            bool result = ctrlPointsGrid1.GetPoints(out locationPoints);
            if (result && m_CurrActiveOverlay != null)
            {
                RemoveObject(m_UserPathObj);
                m_UserPathObj = null;

                try
                {
                    IDNMcLineItem ObjSchemeItem;
                    IDNMcObject obj;
                    CreateLine(out obj, out ObjSchemeItem, locationPoints);
                    m_UserPathObj = obj;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("CreateLine", McEx);
                    this.Show();
                }
            }
        }

        private DNSMcVector3D[] GetPointsFromGrid(DataGridView gridPoints, bool isChangeCoordPoints = false)
        {
            DNSMcVector3D[] aTargetPoints = new DNSMcVector3D[gridPoints.RowCount - 1];
            double result = 0;
            int currRowNum = 0;

            while (!gridPoints.Rows[currRowNum].IsNewRow)
            {
                aTargetPoints[currRowNum].x = (double.TryParse(gridPoints[0, currRowNum].Value.ToString(), out result) == true) ? result : 0;
                aTargetPoints[currRowNum].y = (double.TryParse(gridPoints[1, currRowNum].Value.ToString(), out result) == true) ? result : 0;
                aTargetPoints[currRowNum].z = (double.TryParse(gridPoints[2, currRowNum].Value.ToString(), out result) == true) ? result : 0;

                if (isChangeCoordPoints)
                {
                    if (m_CurrViewport != null)
                        aTargetPoints[currRowNum] = m_CurrViewport.OverlayManagerToViewportWorld(aTargetPoints[currRowNum]);
                    else
                        aTargetPoints[currRowNum] = aTargetPoints[currRowNum];
                }
                currRowNum++;
            }

            // set the last point to be equal to the first one in order to close the polygon
            //if (!gridPoints.Rows[0].IsNewRow)
            //    aTargetPoints[gridPoints.RowCount - 1] = aTargetPoints[0];

            return aTargetPoints;
        }

        /*private void CreateLine(DNSMcVector3D[] locationPoints)
        {
            if (m_CurrActiveOverlay != null)
            {
                RemoveObject(m_UserPathObj);
                m_UserPathObj = null;

                try
                {
                    IDNMcLineItem ObjSchemeItem;
                    IDNMcObject obj;
                    CreateLine(out obj, out ObjSchemeItem, locationPoints);
                    m_UserPathObj = obj;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("CreateLine", McEx);
                    this.Show();
                }
            }
        }*/

        private void SetPointsToGrid(DNSMcVector3D[] polygonPoints, DataGridView dataGridView)
        {
            try
            {
                dataGridView.Rows.Clear();
                if (polygonPoints != null)
                {
                    dataGridView.RowCount = polygonPoints.Length + 1;
                    for (int i = 0; i < polygonPoints.Length; i++)
                    {
                        dataGridView[0, i].Value = polygonPoints[i].x;
                        dataGridView[1, i].Value = polygonPoints[i].y;
                        dataGridView[2, i].Value = polygonPoints[i].z;
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetLocationPoints", McEx);
            }
        }

        private void chxIsCreateSpatialQueries_CheckedChanged(object sender, EventArgs e)
        {
            //btnCreateSpatialQueries.Enabled = chxIsCreateSpatialQueries.Checked;
        }

        private void btnCreateSpatialQueries_Click(object sender, EventArgs e)
        {
            if (m_TraversabilityMapLayer != null)
            {
                try
                {
                    IDNMcMapLayer[] mapLayers = new IDNMcMapLayer[1] { m_TraversabilityMapLayer };
                    IDNMcMapTerrain mapTerrain = DNMcMapTerrain.Create(ctrlGridCoordinateSystem1.GridCoordinateSystem, mapLayers);
                    IDNMcMapTerrain[] mapTerrains = new IDNMcMapTerrain[1] { mapTerrain };
                    DNSCreateDataMV createDataSQ = new DNSCreateDataMV(DNEMapType._EMT_2D);
                    createDataSQ.eDtmUsageAndPrecision = DNEQueryPrecision._EQP_HIGHEST;
                    m_StandaloneSQ = DNMcSpatialQueries.Create(createDataSQ, mapTerrains.ToArray());

                    MessageBox.Show("Create Standalone Success");
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNMcSpatialQueries.Create or DNMcMapTerrain.Create", McEx);
                }
            }
        }

        private void CreateSpatialQueries()
        {
            if (m_TraversabilityMapLayer != null)
            {
                try
                {
                    IDNMcMapLayer[] mapLayers = new IDNMcMapLayer[1] { m_TraversabilityMapLayer };
                    m_CurrStandaloneMapTerrain = DNMcMapTerrain.Create(m_TraversabilityMapLayer.CoordinateSystem, mapLayers);
                    IDNMcMapTerrain[] mapTerrains = new IDNMcMapTerrain[1] { m_CurrStandaloneMapTerrain };
                    DNSCreateDataMV createDataSQ = new DNSCreateDataMV(DNEMapType._EMT_2D);
                    createDataSQ.CoordinateSystem = m_TraversabilityMapLayer.CoordinateSystem;
                    createDataSQ.eDtmUsageAndPrecision = DNEQueryPrecision._EQP_HIGHEST;
                    createDataSQ.pDevice = MCTMapDevice.m_Device;
                    m_StandaloneSQ = DNMcSpatialQueries.Create(createDataSQ, mapTerrains.ToArray());
                    //SetLayerParams(m_CurrStandaloneMapTerrain, m_TraversabilityMapLayer);
                    //MessageBox.Show("Create Standalone Success");
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNMcSpatialQueries.Create or DNMcMapTerrain.Create", McEx);
                }
            }
        }

        private void ChangeTransparency()
        {
            byte transparency = ntxTransparency.GetByte();

        }

        private void ntxTransparency_TextChanged(object sender, EventArgs e)
        {
            SetLayerParams();
        }

        private void tbSoilCost_Scroll(object sender, EventArgs e)
        {
            if (!m_isText)
                ntxSoilCost.SetInt(tbSoilCost.Value);
            Manager_NavCore.SoilCost = tbSoilCost.Value;
            CheckLiveUpdate();
        }

        private void tbRoadsCost_Scroll(object sender, EventArgs e)
        {
            if (!m_isText)
                ntxRoadsCost.SetInt(tbRoadsCost.Value);
            Manager_NavCore.RoadsCost = tbRoadsCost.Value;
            CheckLiveUpdate();
        }

        private void tbLimitedPassabilityAreasCost_Scroll(object sender, EventArgs e)
        {
            if (!m_isText)
                ntxLimitedPassabilityAreasCost.SetInt(tbLimitedPassabilityAreasCost.Value);
            Manager_NavCore.LimitedPassabilityAreasCost = tbLimitedPassabilityAreasCost.Value;
            CheckLiveUpdate();
        }

        private void tbVeryLimitedPassabilityAreasCost_Scroll(object sender, EventArgs e)
        {
            if (!m_isText)
                ntxVeryLimitedPassabilityAreasCost.SetInt(tbVeryLimitedPassabilityAreasCost.Value);
            Manager_NavCore.VeryLimitedPassabilityAreasCost = tbVeryLimitedPassabilityAreasCost.Value;
            CheckLiveUpdate();
        }

        bool m_isText = false;
        private void ntxSoilCost_TextChanged(object sender, EventArgs e)
        {
            m_isText = true;
            tbSoilCost.Value = ntxSoilCost.GetByte();
            m_isText = false;
        }

        private void ntxRoadsCost_TextChanged(object sender, EventArgs e)
        {
            m_isText = true;
            tbRoadsCost.Value = ntxRoadsCost.GetByte();
            m_isText = false;
        }

        private void ntxLimitedPassabilityAreasCost_TextChanged(object sender, EventArgs e)
        {
            m_isText = true;
            tbLimitedPassabilityAreasCost.Value = ntxLimitedPassabilityAreasCost.GetByte();
            m_isText = false;
        }

        private void ntxVeryLimitedPassabilityAreasCost_TextChanged(object sender, EventArgs e)
        {
            m_isText = true;
            tbVeryLimitedPassabilityAreasCost.Value = ntxVeryLimitedPassabilityAreasCost.GetByte();
            m_isText = false;
        }

        private void CheckLiveUpdate()
        {
            if (chxUseLiveUpdate.Checked)
            {
                btnSetParams_Click(null, null);
                btnFindPath_Click(null, null);
            }
        }

        private void btnChangeLineWidth_Click(object sender, EventArgs e)
        {
            if (m_NavCorePathItem != null)
                m_NavCorePathItem.SetLineWidth(ntxResultLineWidth.GetFloat(), 2, false);
        }

        private void btnNavCoreForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_CurrActiveOverlay != null)
            {
                RemoveObject(m_UserPathObj); 
                RemoveObject(m_NavCorePathObj);
                RemoveObject(m_BoundingBoxRect);
                RemoveObject(m_NavBoundingBoxRect);
                RemoveTraversabilityObjects();
            }
        }

        private void btnSaveGeneralData_Click(object sender, EventArgs e)
        {
            Manager_NavCore.NavCoreFolderPath = ctrlGDBrowseNavMeshFolder.FileName;
            Manager_NavCore.TraversabilityFolderPath = ctrlGDBrowseTraversabilityFolder.FileName;
            Manager_NavCore.TraversabilityTransparency = ntxGDTransparency.GetByte();
            Manager_NavCore.SoilCost = ntxGDSoilCost.GetFloat();
            Manager_NavCore.RoadsCost = ntxGDRoadsCost.GetFloat();
            Manager_NavCore.LimitedPassabilityAreasCost = ntxGDLimitedPassabilityAreasCost.GetFloat();
            Manager_NavCore.VeryLimitedPassabilityAreasCost = ntxGDVeryLimitedPassabilityAreasCost.GetFloat();
            Manager_NavCore.LineWidth = ntxGDResultLineWidth.GetFloat();

            LoadForm();
            //Manager_NavCore
        }
    }
}
