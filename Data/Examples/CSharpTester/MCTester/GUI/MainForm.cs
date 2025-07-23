using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCTester.Managers.MapWorld;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers;
using MCTester.Managers.VectorialWorld;
using MCTester.MapWorld.MapUserControls;
using MCTester.GUI.Map;
using MCTester.GUI.Forms;
using MCTester.ButtonsImplementation;
using UnmanagedWrapper;
using MapCore;
using MCTester.GUI.Trees;
using MCTester.General_Forms;
using MCTester.ObjectWorld.ObjectsUserControls;
using System.Diagnostics;
using MCTester.MapWorld;
using MCTester.Controls;
using MCTester.Automation;
using MapCore.Common;
using System.IO;

namespace MCTester
{
    public partial class MainForm : Form
    {
        #region Data Members
        public static MapLoaderDefinitionClass MapLoaderDefinitionManager;
        public static EnvironmentBasis EnvironmentBasisManager;
        public static StatusStrip MainFormStatusBar;
        public static StatusStrip MainFormStatusBarStatistics;

        public static bool mBtnObjectWorldCheckedState = false;
        private static MCObjectWorldTreeViewForm m_ObjectWorldTreeViewForm;

        public static bool mBtnViewportTerrainsCheckedState = false;
        private static MCMapWorldTreeViewForm m_MapWorldTreeViewForm;

        public static MCMapWorldTreeViewForm GetMapWorldTreeViewForm()
            { return m_MapWorldTreeViewForm; }

        public static void MinimizeMapWorldTreeViewForm()
        {
            if (m_MapWorldTreeViewForm != null)
                m_MapWorldTreeViewForm.WindowState = FormWindowState.Minimized;
        }

        public static void NormalMapWorldTreeViewForm()
        {
            if (m_MapWorldTreeViewForm != null)
                m_MapWorldTreeViewForm.WindowState = FormWindowState.Normal;
        }

        public static bool mBtnTerrainLayersCheckedState = false;
        private static MCTerrainLayerTreeViewForm m_TerrainLayerTreeViewForm;

        public static bool CheckAllNativeServerLayersValidityAsyncCheckedState = false;

        public static bool FontDialog_SetAsDefault_UserSelected = false;
        public static bool TextureDialog_SetAsDefault_UserSelected = false;

        public static IDNMcObject CameraCornersAndCenterObject;

        private btnZoomIn m_BtnZoomIn;
        private btnZoomOut m_BtnZoomOut;
        private btnGrid m_BtnGrid;
        private btnMapHeightLines m_BtnMapHeightLines;
        private btnDtmVisualization m_BtnDtmVisualization;
        private string[] m_ArgsParams;
        private Timer mAppTimer;
        public static bool mIsInAppTimer = false;
        bool mIsShowMCVersion = false;

        public static IDNMcMapViewport m_AutoViewport = null;
        public static bool m_isNeedCheckAutoViewport = false;
        private static string m_PrintViewportPath = "";
        private bool m_IsNeedToClose = false;
        public static bool m_AutoIsShowMsg = false;
        public static StreamWriter m_AutoStreamWriter;
        public static Stopwatch m_AutoTimeRender;
        public static Stopwatch m_AutoTimeAll;

        public enum FontTextureSourceForm
        {
            CreateOnMap,
            CreateDialog,
            Recreate,
            Update
        }


        public static List<btnPrintForm> BtnPrintForms = new List<btnPrintForm>();

        #endregion

        #region C-Tor
        public MainForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            mosaicsCreatorToolStripMenuItem.Visible = false;
            tsbRenderType.Text = "Pending - Updates Base";
            EnvironmentBasisManager = new EnvironmentBasis();
            MainFormStatusBar = this.MainStatusBar;
            MainFormStatusBarStatistics = this.MainStatusBarStatistics;

            m_BtnZoomIn = new btnZoomIn();
            m_BtnZoomOut = new btnZoomOut();
            m_BtnGrid = new btnGrid();
            m_BtnMapHeightLines = new btnMapHeightLines();
            m_BtnDtmVisualization = new btnDtmVisualization();
            mAppTimer = new Timer();
            mAppTimer.Interval = 10;
            mAppTimer.Tick += new EventHandler(mAppTimer_Tick);
           
            mAppTimer.Start();

            //  m_AutoTimeAll = new Stopwatch();
        }



        // flagTemp - added to simulate return from 'oninit' callback fail after terrain created
        //public static bool flagTemp = true;
        void mAppTimer_Tick(object sender, EventArgs e)
        {
            if (mIsInAppTimer == false)
            {
                mIsInAppTimer = true;
                DNMcGarbageCollector.Collect();
                if (MCTMapDevice.m_Device != null /*&& flagTemp*/)
                {
                    try
                    {
                        DNMcMapDevice.PerformPendingCalculations();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("PerformPendingCalculations", McEx);
                    }
                }
                if (!mIsShowMCVersion)
                {
                    try
                    {
                        string strVersion = DNMcMapDevice.GetVersion();
                        if (strVersion != "")
                        {
                            mIsShowMCVersion = true;
                            string prefix =
#if DEBUG
                "D_"; // Debug mode
#else
            "R_"; // Release mode
#endif
                            string userTitle = "";
                            if (m_ArgsParams != null && m_ArgsParams.Contains("-title"))
                            {
                                int index1 = m_ArgsParams.ToList().IndexOf("-title");
                                if (index1 + 1 < m_ArgsParams.Length)
                                {
                                    userTitle = m_ArgsParams[index1 + 1];
                                }
                            }
                            this.Text = prefix + "MCTester " + strVersion + " " + userTitle;
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetVersion", McEx);
                    }
                }
                if (m_IsNeedToClose)
                {
                    CloseStreamWriter();
                    this.Close();
                }
                else if (m_isNeedCheckAutoViewport && m_AutoViewport != null && !m_AutoViewport.HasPendingUpdates())
                {
                    if (m_AutoTimeRender != null && m_AutoStreamWriter != null)
                    {
                        StopAutoViewport();
                        if (m_PrintViewportPath == "")
                            CloseStreamWriter();
                    }


                    if (m_PrintViewportPath != "")
                    {
                        uint widthDimension, heightDimension;
                        DNEPixelFormat viewportPixelFormat = DNEPixelFormat._EPF_A8R8G8B8;
                        try
                        {
                            m_AutoViewport.GetViewportSize(out widthDimension, out heightDimension);
                        }
                        catch (MapCoreException McEx)
                        {
                            Manager_MCAutomation.HandleMapCoreExecption("GetViewportSize", McEx);
                            if (!m_AutoIsShowMsg)
                            {
                                m_IsNeedToClose = true;
                            }
                            return;
                        }

                        try
                        {
                            m_AutoViewport.GetRenderToBufferNativePixelFormat(out viewportPixelFormat);
                        }
                        catch (MapCoreException McEx)
                        {
                            Manager_MCAutomation.HandleMapCoreExecption("GetRenderToBufferNativePixelFormat", McEx);
                            if (!m_AutoIsShowMsg)
                            {
                                m_IsNeedToClose = true;
                            }
                            return;
                        }
                        Manager_MCViewports.RenderScreenRectToBuffer(m_AutoViewport,
                            viewportPixelFormat,
                            0,
                            widthDimension,
                            heightDimension,
                            new Point(0, 0),
                            new Point((int)widthDimension,
                            (int)heightDimension),
                            "",
                            m_PrintViewportPath,
                            true,
                            m_AutoIsShowMsg,
                            m_AutoStreamWriter);

                        if (m_IsNeedToClose || !m_AutoIsShowMsg)
                            this.Close();
                        else
                            CloseStreamWriter();


                    }
                    m_isNeedCheckAutoViewport = false;
                    m_AutoViewport = null;
                }
                mIsInAppTimer = false;
            }
        }

        public MainForm(string[] argParams) : this()
        {
            m_ArgsParams = argParams;

        }
        #endregion

        #region ToolStripMenuItem
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewMapForm newMapFrm = new NewMapForm();
            newMapFrm.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapLoaderDefinitionForm DefsFrm = new MapLoaderDefinitionForm();
            DefsFrm.ShowDialog();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mosaicsCreatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsmiMosaicsCreatorForm mosaicsCreatorForm = new tsmiMosaicsCreatorForm();
            mosaicsCreatorForm.ShowDialog();
        }

        private void rollingShutterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsmiRollingSutterForm rollingSutter = new tsmiRollingSutterForm();
        }

        private void pointingFingerDBGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            frmPointingFingerDBGenerator PointingFingerDBGenerator = new frmPointingFingerDBGenerator();
            PointingFingerDBGenerator.ShowDialog();
        }

        private void vectorXMLCreatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVectorXMLCreator VectorXMLCreatorForm = new frmVectorXMLCreator();
            VectorXMLCreatorForm.ShowDialog();
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainStatusBar.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        #endregion

        #region Panels button click events
        private void tsbOpenMapManually_Click(object sender, EventArgs e)
        {
            NewMapForm newMapFrm = new NewMapForm();
            newMapFrm.ShowDialog();

            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void tsbOpenMapScheme_Click(object sender, EventArgs e)
        {
            MapLoaderDefinitionForm DefsFrm = new MapLoaderDefinitionForm();
            DefsFrm.ShowDialog();

            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void tsbTileWindows_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void tsbRenderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tsbRenderType.SelectedItem.ToString())
            {
                case "Flag Base Render":
                    MCTMapForm.eRender = MCTMapForm.RenderType.FlagBaseRender;
                    break;
                case "Render All":
                    MCTMapForm.eRender = MCTMapForm.RenderType.RenderAll;
                    break;
                case "Render":
                    MCTMapForm.eRender = MCTMapForm.RenderType.Render;
                    break;
                case "Manual Render":
                    MCTMapForm.eRender = MCTMapForm.RenderType.Manual;
                    break;
                case "Pending - Updates Base":
                    MCTMapForm.eRender = MCTMapForm.RenderType.PendingUpdatesBase;
                    break;
            }
        }

        private void tsbGeneralProperties_Click(object sender, EventArgs e)
        {
            frmTesterGeneralDefinitions m_FrmTesterGeneralDefinitions = new frmTesterGeneralDefinitions();
            m_FrmTesterGeneralDefinitions.ShowDialog();
        }

        private void tsbObjectProperties_Click(object sender, EventArgs e)
        {
            if (MCTMapDevice.CurrDevice == null)
            {
                MessageBox.Show("This operation required a device", "Device Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ObjectPropertiesForm m_ObjectPropertiesForm = new ObjectPropertiesForm();
            m_ObjectPropertiesForm.ShowDialog();
        }

        private void tsbNavigateMap_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            MovementFrm mForm = new MovementFrm();
            mForm.ShowDialog();
        }

        private void tsbZoomIn_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            m_BtnZoomIn.ExecuteAction();
        }

        private void tsbZoomOut_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            m_BtnZoomOut.ExecuteAction();
        }

        private void tsbMapGrid_DropDownOpened(object sender, EventArgs e)
        {
            IDNMcMapViewport activeViewport;
            if (MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport != null)
            {
                activeViewport = MCTMapFormManager.MapForm.Viewport;

                tsbUTMMapGrid.Checked = false;
                tsbGeoMapGrid.Checked = false;
                tsbMGRSMapGrid.Checked = false;
                tsbNZMGMapGrid.Checked = false;
                tsbGeoRefMapGrid.Checked = false;

                uint gridIndex;
                if (activeViewport.Grid != null)
                {
                    if (Manager_MCGrid.dGrid.TryGetValue(activeViewport.Grid, out gridIndex))
                    {
                        switch (gridIndex)
                        {
                            case (int)btnGrid.GridTypes.UTMGrid: // UTM Grid
                                tsbUTMMapGrid.Checked = true;
                                tsbGeoMapGrid.Checked = false;
                                tsbMGRSMapGrid.Checked = false;
                                tsbNZMGMapGrid.Checked = false;
                                tsbGeoRefMapGrid.Checked = false;
                                break;
                            case (int)btnGrid.GridTypes.GeoGrid: // Geo Grid
                                tsbUTMMapGrid.Checked = false;
                                tsbGeoMapGrid.Checked = true;
                                tsbMGRSMapGrid.Checked = false;
                                tsbNZMGMapGrid.Checked = false;
                                tsbGeoRefMapGrid.Checked = false;
                                break;
                            case (int)btnGrid.GridTypes.MGRSGrid: // MGRS Grid
                                tsbUTMMapGrid.Checked = false;
                                tsbGeoMapGrid.Checked = false;
                                tsbMGRSMapGrid.Checked = true;
                                tsbNZMGMapGrid.Checked = false;
                                tsbGeoRefMapGrid.Checked = false;
                                break;
                            case (int)btnGrid.GridTypes.NZMGGrid: // NZMG Grid
                                tsbUTMMapGrid.Checked = false;
                                tsbGeoMapGrid.Checked = false;
                                tsbMGRSMapGrid.Checked = false;
                                tsbNZMGMapGrid.Checked = true;
                                tsbGeoRefMapGrid.Checked = false;
                                break;
                            case (int)btnGrid.GridTypes.GEOREFGrid: // georef Grid
                                tsbUTMMapGrid.Checked = false;
                                tsbGeoMapGrid.Checked = false;
                                tsbMGRSMapGrid.Checked = false;
                                tsbNZMGMapGrid.Checked = false;
                                tsbGeoRefMapGrid.Checked = true;
                                break;

                        }
                    }
                }
            }
        }

        private void tsbUTMMapGrid_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            m_BtnGrid.ExecuteAction(sender.ToString());
        }

        private void tsbGeoMapGrid_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            m_BtnGrid.ExecuteAction(sender.ToString());
        }

        private void tsbMGRSMapGrid_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            m_BtnGrid.ExecuteAction(sender.ToString());
        }

        private void tsbNZMGMapGrid_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            m_BtnGrid.ExecuteAction(sender.ToString());
        }

        private void tsbMapHeightLines_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            m_BtnMapHeightLines.ExecuteAction();
        }

        private void tsbDtmVisualization_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            m_BtnDtmVisualization.ExecuteAction();
        }

        private void tsbFileProductions_Click(object sender, EventArgs e)
        {
            btnFileProductionForm fileProductionForm = new btnFileProductionForm();
            fileProductionForm.ShowDialog();
        }


        private void tsbPrint_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnPrintForm PrintForm = new btnPrintForm();
            BtnPrintForms.Add(PrintForm);
            PrintForm.Show();
        }

        public static void HandleRemoveObject(IDNMcObject mcObject)
        {
            foreach (btnPrintForm PrintForm in BtnPrintForms)
            {
                PrintForm.HandleRemoveObject(mcObject);
            }
        }

        public static void StopAutoViewport(IDNMcMapViewport mcMapViewportRemoved = null)
        {
            if (m_AutoViewport != null && m_AutoStreamWriter != null && m_AutoTimeAll.IsRunning)
            {
                m_AutoTimeRender.Stop();
                m_AutoTimeAll.Stop();

                m_AutoStreamWriter.WriteLine("auto render time: from render until HasPendingUpdates = false - " + m_AutoTimeRender.ElapsedMilliseconds);
                m_AutoStreamWriter.WriteLine("auto render time: from start until HasPendingUpdates = false - " + m_AutoTimeAll.ElapsedMilliseconds);

            }

            if (mcMapViewportRemoved != null)
            {
                CloseStreamWriter();
                if (mcMapViewportRemoved == m_AutoViewport)
                {
                    m_isNeedCheckAutoViewport = false;
                    m_AutoViewport = null;
                }
            }
        }

        private void tsbScan_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnScanForm ScanGeometryForm = new btnScanForm();
            ScanGeometryForm.Show();
        }

        private void tsbEnvironment_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnEnvironmentForm EnvironmentForm = new btnEnvironmentForm();
            EnvironmentForm.Show();
        }

        private void tsbLineItem_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnLineItem m_LineItem = new btnLineItem();
            m_LineItem.ExecuteAction();
        }

        private void tsbArrowItem_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnArrowItem m_ArrowItem = new btnArrowItem();
            m_ArrowItem.ExecuteAction();
        }

        private void tsbLineExpansionItem_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnLineExpansionItem m_LineExpansionItem = new btnLineExpansionItem();
            m_LineExpansionItem.ExecuteAction();
        }

        private void tsbPolygonItem_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnPolygonItem m_PolygonItem = new btnPolygonItem();
            m_PolygonItem.ExecuteAction();
        }

        private void tsbEllipseItem_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnEllipseItem m_EllipseItem = new btnEllipseItem();
            m_EllipseItem.ExecuteAction();
        }

        private void tsbArcItem_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnArcItem m_ArcItem = new btnArcItem();
            m_ArcItem.ExecuteAction();
        }

        private void tsbRectangleItem_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnRectangleItem m_RectangleItem = new btnRectangleItem();
            m_RectangleItem.ExecuteAction();
        }

        private void tsbTextItem_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnTextItem m_TextItem = new btnTextItem();
            m_TextItem.ExecuteAction();
        }

        private void tsbPictureItem_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnPictureItem m_PictureItem = new btnPictureItem();
            m_PictureItem.ExecuteAction();
        }

        private void tsbFont_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            frmFontUpdateBtnImplementation m_FrmFontUpdateBtnImplementation = new frmFontUpdateBtnImplementation();
            m_FrmFontUpdateBtnImplementation.ShowDialog();
        }

        private void tsbNativeMesh_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnMeshItem m_MeshItem = new btnMeshItem(sender.ToString());
            m_MeshItem.ExecuteAction();
        }

        private void tsbNativeLODMesh_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnMeshItem m_MeshItem = new btnMeshItem(sender.ToString());
            m_MeshItem.ExecuteAction();
        }

        private void tsbParticalEffectItem_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnParticalEffectItem m_ParticalEffectItem = new btnParticalEffectItem();
            m_ParticalEffectItem.ExecuteAction();
        }

        private void tsbDirectionalLight_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnLightItem m_LightItem = new btnLightItem(sender.ToString());
            m_LightItem.ExecuteAction();
        }

        private void tsbPointLight_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnLightItem m_LightItem = new btnLightItem(sender.ToString());
            m_LightItem.ExecuteAction();
        }

        private void tsbSpotLight_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnLightItem m_LightItem = new btnLightItem(sender.ToString());
            m_LightItem.ExecuteAction();
        }

        private void tsbProjectorItem_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnProjectorItem m_ProjectorItem = new btnProjectorItem();
            m_ProjectorItem.ExecuteAction();
        }

        private void tsbSoundItem_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnSoundItem m_SoundItem = new btnSoundItem();
            m_SoundItem.ExecuteAction();
        }

        private void tsbTerrainLayers_Click(object sender, EventArgs e)
        {
            if (BtnTerrainLayersCheckedState != true)
            {
                BtnTerrainLayersCheckedState = true;
                m_TerrainLayerTreeViewForm = new MCTerrainLayerTreeViewForm();
                m_TerrainLayerTreeViewForm.Show();
            }
            else
            {
                if (m_TerrainLayerTreeViewForm.WindowState == FormWindowState.Normal)
                    m_TerrainLayerTreeViewForm.Focus();
                else
                    m_TerrainLayerTreeViewForm.WindowState = FormWindowState.Normal;
            }
        }

        private void tsbMapWorld_Click(object sender, EventArgs e)
        {
            if (BtnMapWorldCheckedState != true)
            {
                BtnMapWorldCheckedState = true;
                m_MapWorldTreeViewForm = new MCMapWorldTreeViewForm();
                if (MCTMapFormManager.MapForm != null)
                    m_MapWorldTreeViewForm.SelectNodeInTreeNode((uint)MCTMapFormManager.MapForm.Viewport.GetHashCode(), true);
                m_MapWorldTreeViewForm.Show();
            }
            else
            {
                if (m_MapWorldTreeViewForm.WindowState == FormWindowState.Normal)
                    m_MapWorldTreeViewForm.Focus();
                else
                    m_MapWorldTreeViewForm.WindowState = FormWindowState.Normal;
            }
        }

        public static void RebuildMapWorldTree()
        {
            if (m_MapWorldTreeViewForm != null)
            {
                m_MapWorldTreeViewForm.BuildTree();
                m_MapWorldTreeViewForm.ExpendTree();
            }
        }

        public static void RebuildObjectWorldTree(bool isSelectLastNode = false, int indexTabToSelect = -1)
        {
            if (m_ObjectWorldTreeViewForm != null)
            {
                m_ObjectWorldTreeViewForm.RefreshTree(isSelectLastNode, indexTabToSelect);
            }
        }

        public void tsbObjectWorld_Click(object sender, EventArgs e)
        {
            ShowObjectWorldForm();
        }

        public static MCObjectWorldTreeViewForm ShowObjectWorldForm()
        {
            if (BtnObjectWorldCheckedState != true)
            {
                BtnObjectWorldCheckedState = true;
                m_ObjectWorldTreeViewForm = new MCObjectWorldTreeViewForm();
                m_ObjectWorldTreeViewForm.Show();
            }
            else
            {
                if (m_ObjectWorldTreeViewForm.WindowState == FormWindowState.Normal)
                    m_ObjectWorldTreeViewForm.Focus();
                else
                    m_ObjectWorldTreeViewForm.WindowState = FormWindowState.Normal;
            }
            return m_ObjectWorldTreeViewForm;
        }

        public MCObjectWorldTreeViewForm GetOverlayMangerTreeViewForm()
        {
            return m_ObjectWorldTreeViewForm;
        }

        private void tsbEditModeProperties_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            EditModeProperties EditModePropertiesForm = new EditModeProperties();
            EditModePropertiesForm.ShowDialog();
        }

        private void tsbEditObject_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnEditObject m_EditObject = new btnEditObject();
            m_EditObject.ExecuteAction();
        }

        private void tsbInitObject_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnInitObject m_InitObject = new btnInitObject();
            m_InitObject.ExecuteAction();
        }

        private void tsbDistanceDirectionMeasure_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnDistanceDirectionMeasure m_DistanceDirectionMeasure = new btnDistanceDirectionMeasure();
            m_DistanceDirectionMeasure.ExecuteAction();
        }

        private void tsbDynamicZoom_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnDynamicZoom m_DynamicZoom = new btnDynamicZoom();
            m_DynamicZoom.ExecuteAction();
        }

        private void tsbEditModeNavigation_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnEditModeNavigation m_EditModeNavigation = new btnEditModeNavigation();
            m_EditModeNavigation.ExecuteAction();
        }

        public bool EditModeNavigationButtonState
        {
            get { return tsbEditModeNavigation.Checked; }
            set { tsbEditModeNavigation.Checked = value; }
        }

        public string DefaultActiveMapGrid
        {
            get
            {
                ToolStripItemCollection gridItemCollection = tsbMapGrid.DropDownItems;

                for (int i = 0; i < gridItemCollection.Count; i++)
                {
                    if (((ToolStripMenuItem)gridItemCollection[i]).CheckState == CheckState.Checked)
                        return gridItemCollection[i].ToString();
                }

                return "";
            }
            //set
            //{
            //    ToolStripItemCollection gridItemCollection = tsbMapGrid.DropDownItems;

            //    for (int i = 0; i < gridItemCollection.Count; i++)
            //    {
            //        if (gridItemCollection[i].ToString() == value)
            //            ((ToolStripMenuItem)gridItemCollection[i]).Checked = true;
            //        else
            //            ((ToolStripMenuItem)gridItemCollection[i]).Checked = false;
            //    }
            //}
        }

        private void tsbExitCurrentAction_Click(object sender, EventArgs e)
        {
            btnExitCurrentAction m_ExitCurrentAction = new btnExitCurrentAction();
            m_ExitCurrentAction.ExecuteAction();
        }

        private void tsbEventCallBack_Click(object sender, EventArgs e)
        {
            EventCallBackForm m_EventCallBackForm = new EventCallBackForm();
            m_EventCallBackForm.Show();
        }

        private void tsbCheckRenderNeeded_Click(object sender, EventArgs e)
        {
            RenderNeededForm m_RenderNeededForm = new RenderNeededForm();
            m_RenderNeededForm.Show();
        }

        private void tsbMapProductionTool_Click(object sender, EventArgs e)
        {
            btnMapProductionToolForm m_MapProductionToolForm = new btnMapProductionToolForm();
            m_MapProductionToolForm.ShowDialog();
        }

        private void tsbPerformanceTester_Click(object sender, EventArgs e)
        {
            btnPerformanceTester m_PerformanceTester = new btnPerformanceTester();
            m_PerformanceTester.ExecuteAction();
        }

        private void tsbStressTester_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnStressTester m_StressTester = new btnStressTester();
            m_StressTester.ExecuteAction();
        }

        private void tsbObjectLoader_Click(object sender, EventArgs e)
        {
            /* frmMagicForm temp = new frmMagicForm();
             temp.Show();*/
            AutoLoadedObjForm m_AutoLoadedObjForm = new AutoLoadedObjForm();
            m_AutoLoadedObjForm.Show();
        }

        private void tsbObjectAutoAnimatation_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            if (Manager_MCOverlayManager.ActiveOverlayManager == null)
            {
                MessageBox.Show("There is no active overlay manager");
                return;
            }
            ObjectItemSelectedFrm m_ObjectItemSelectedFrm = new ObjectItemSelectedFrm("Animation");

            //Set form to fit this button
            m_ObjectItemSelectedFrm.btnOKPlay.Text = "Play";
            m_ObjectItemSelectedFrm.btnAnimationStop.Visible = true;
            m_ObjectItemSelectedFrm.chxAnimatedAll.Visible = true;

            m_ObjectItemSelectedFrm.ShowDialog();
        }

        private void tsbAnimationState_Click(object sender, EventArgs e)
        {
            btnAnimationStateForm animationStateForm = new btnAnimationStateForm();
            animationStateForm.ShowDialog();
        }

        private void tsbVectorial_Click(object sender, EventArgs e)
        {
            frmVectorial m_FrmVectorial = new frmVectorial();
            m_FrmVectorial.ShowDialog();
        }

        private void tsbPathFinder_Click(object sender, EventArgs e)
        {
            frmPathFinder pathFinderForm = new frmPathFinder();
            pathFinderForm.ShowDialog();
        }

        private void tsbGeographicCalculations_Click(object sender, EventArgs e)
        {
            GeoCalcForm m_GeoCalcForm = new GeoCalcForm();
            m_GeoCalcForm.ShowDialog();
        }

        private void tsbGeometricCalculations_Click(object sender, EventArgs e)
        {
            frmGeometricCalc m_FrmGeometricCalc = new frmGeometricCalc();
            m_FrmGeometricCalc.ShowDialog();
        }

        private void tsbStandaloneSQ_Click(object sender, EventArgs e)
        {
            IDNMcMapViewport mcMapViewport = null;
            if (MCTMapFormManager.MapForm != null)
                mcMapViewport = MCTMapFormManager.MapForm.Viewport;

            PreStandaloneSQForm StandaloneSQ = new PreStandaloneSQForm(mcMapViewport);
            StandaloneSQ.Show();
        }
        #endregion

        #region Tool Strip Menu Events
        private void toolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolBarToolStripMenuItem.Checked == true)
            {
                generalToolStripMenuItem.Checked = true;
                mapOperationToolStripMenuItem.Checked = true;
                managmentsToolStripMenuItem.Checked = true;
                editModeToolStripMenuItem.Checked = true;
                extendedEditModeToolStripMenuItem.Checked = true;
                symbolicItemsToolStripMenuItem.Checked = true;
                physicalItemsToolStripMenuItem.Checked = true;
                calculationsToolStripMenuItem.Checked = true;
                attendantToolsToolStripMenuItem.Checked = true;
                vectorialToolStripMenuItem.Checked = true;
            }
            else
            {
                generalToolStripMenuItem.Checked = false;
                mapOperationToolStripMenuItem.Checked = false;
                managmentsToolStripMenuItem.Checked = false;
                editModeToolStripMenuItem.Checked = false;
                extendedEditModeToolStripMenuItem.Checked = false;
                symbolicItemsToolStripMenuItem.Checked = false;
                physicalItemsToolStripMenuItem.Checked = false;
                calculationsToolStripMenuItem.Checked = false;
                attendantToolsToolStripMenuItem.Checked = false;
                vectorialToolStripMenuItem.Checked = false;
            }
        }

        private void generalToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (generalToolStripMenuItem.Checked == true)
                tsGeneral.Visible = true;
            else
                tsGeneral.Visible = false;

        }

        private void mapOperationToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (mapOperationToolStripMenuItem.Checked == true)
                tsMapOperations.Visible = true;
            else
                tsMapOperations.Visible = false;
        }

        private void managmentsToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (managmentsToolStripMenuItem.Checked == true)
                tsManagments.Visible = true;
            else
                tsManagments.Visible = false;
        }

        private void editModeToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (editModeToolStripMenuItem.Checked == true)
                tsEditMode.Visible = true;
            else
                tsEditMode.Visible = false;
        }

        private void extendedEditModeToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (extendedEditModeToolStripMenuItem.Checked == true)
                tsExtendedEditMode.Visible = true;
            else
                tsExtendedEditMode.Visible = false;
        }

        private void symbolicItemsToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (symbolicItemsToolStripMenuItem.Checked == true)
                tsDrawingSymbolicItems.Visible = true;
            else
                tsDrawingSymbolicItems.Visible = false;
        }

        private void physicalItemsToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (physicalItemsToolStripMenuItem.Checked == true)
                tsDrawingPhysicalItems.Visible = true;
            else
                tsDrawingPhysicalItems.Visible = false;
        }

        private void calculationsToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (calculationsToolStripMenuItem.Checked == true)
                tsCalculations.Visible = true;
            else
                tsCalculations.Visible = false;
        }

        private void attendantToolsToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (attendantToolsToolStripMenuItem.Checked == true)
                tsAttendantTools.Visible = true;
            else
                tsAttendantTools.Visible = false;
        }

        private void vectorialToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (vectorialToolStripMenuItem.Checked == true)
                tsVectorial.Visible = true;
            else
                tsVectorial.Visible = false;
        }
        #endregion

        #region Events
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            /*
                        // Load map schemes XML file
                        string currentPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
                        currentPath = System.IO.Path.GetDirectoryName(currentPath);
                        string fileName = currentPath + @"\PrivateMapDefinitionsDatabase.xml";

                        bool fileExists = System.IO.File.Exists(fileName);
                        if (fileExists == true)
                            MapLoaderDefinitionManager = MapLoaderDefinitionClass.LoadXmlFile(currentPath + @"\PrivateMapDefinitionsDatabase.xml");

                        if(!fileExists || (fileExists && MapLoaderDefinitionManager == null))
                            MapLoaderDefinitionManager = MapLoaderDefinitionClass.LoadXmlFile(currentPath + @"\MapDefinitionsDatabase.xml");
            */

            LoadMapSchemesXMLFile();
            bool isShowMapList = true;
            if (m_ArgsParams != null)
            {
                if (m_ArgsParams.Length >= 2 && (m_ArgsParams[0] == "Animation" || m_ArgsParams[0] == "AutoRender"))
                {
                    isShowMapList = false;
                    if (m_ArgsParams[0] == "Animation")
                    {
                        bool objectPathExists = System.IO.File.Exists(m_ArgsParams[1]);
                        if (objectPathExists == true)
                        {
                            UserDataFactory UDF = new UserDataFactory();
                            IDNMcObject[] loadedObjects = Manager_MCOverlayManager.ActiveOverlay.LoadObjects(@"" + m_ArgsParams[1], UDF);

                            // animate objects
                            string animationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
                            animationPath = (System.IO.Path.GetDirectoryName(animationPath) + @"\PathAnimation.csv");

                            if (System.IO.File.Exists(animationPath) == true)
                            {
                                List<DNSPathAnimationNode> PathAnimationNode = new List<DNSPathAnimationNode>();

                                bool IsLoop = true;
                                bool AutomaticRotation = true;
                                float RotationAdditionalYaw = 0;
                                float StartingTimePoint = 0;
                                DNEPositionInterpolationMode PositionInterpolationMode = DNEPositionInterpolationMode._EPIM_LINEAR;
                                DNERotationInterpolationMode RotationInterpolationMode = DNERotationInterpolationMode._ERIM_LINEAR;
                                System.IO.StreamReader StreamReader = new System.IO.StreamReader(animationPath);

                                while (!StreamReader.EndOfStream)
                                {
                                    string currLine = StreamReader.ReadLine();
                                    string[] values = currLine.Split(',');

                                    IsLoop = bool.Parse(values[0]);
                                    AutomaticRotation = bool.Parse(values[1]);
                                    RotationAdditionalYaw = float.Parse(values[2]);
                                    StartingTimePoint = float.Parse(values[3]);
                                    PositionInterpolationMode = (DNEPositionInterpolationMode)Enum.Parse(typeof(DNEPositionInterpolationMode), values[4]);
                                    RotationInterpolationMode = (DNERotationInterpolationMode)Enum.Parse(typeof(DNERotationInterpolationMode), values[5]);


                                    DNSPathAnimationNode node = new DNSPathAnimationNode();

                                    node.Position.x = float.Parse(values[6]);
                                    node.Position.y = float.Parse(values[7]);
                                    node.Position.z = float.Parse(values[8]);
                                    node.fTime = float.Parse(values[9]);
                                    node.ManualRotation.fYaw = float.Parse(values[10]);
                                    node.ManualRotation.fPitch = float.Parse(values[11]);
                                    node.ManualRotation.fRoll = float.Parse(values[12]);
                                    node.ManualRotation.bRelativeToCurrOrientation = bool.Parse(values[13]);

                                    PathAnimationNode.Add(node);
                                }

                                int step = 1;
                                if (m_ArgsParams.Length >= 3)
                                {
                                    step = int.Parse(m_ArgsParams[2]);
                                }

                                for (int i = 0; i < loadedObjects.Length; i += step)
                                {
                                    IDNMcObjectScheme scheme = loadedObjects[i].GetScheme();
                                    if (scheme.GetNodes(DNENodeKindFlags._ENKF_ANY_ITEM)[0] != null)
                                    {
                                        IDNMcObjectSchemeItem animatedItem = (IDNMcObjectSchemeItem)loadedObjects[i].GetScheme().GetNodes(DNENodeKindFlags._ENKF_ANY_ITEM)[0];

                                        try
                                        {
                                            scheme.SetObjectRotationItem(animatedItem);
                                        }
                                        catch (MapCoreException McEx)
                                        {
                                            MapCore.Common.Utilities.ShowErrorMessage("SetObjectRotationItem", McEx);
                                        }

                                        try
                                        {
                                            loadedObjects[i].PlayPathAnimation(PathAnimationNode.ToArray(),
                                                                                    PositionInterpolationMode,
                                                                                    RotationInterpolationMode,
                                                                                    StartingTimePoint,
                                                                                    RotationAdditionalYaw,
                                                                                    AutomaticRotation,
                                                                                    IsLoop);

                                        }
                                        catch (MapCoreException McEx)
                                        {
                                            MapCore.Common.Utilities.ShowErrorMessage("PlayPathAnimation", McEx);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (m_ArgsParams[0] == "AutoRender")  // example: AutoRender C:\Maps\AutoRender\MapCore_VTest\2D_DTMVis_1\FullViewportDataParams_DX9.json false 5W.bmp
                    {
                        string jsonParams = "";

                        for (int j = 0; j < m_ArgsParams.Length; j++)
                            jsonParams += m_ArgsParams[j] + " ";

                        if (m_ArgsParams.Length < 4)
                        {
                            AutoRenderInvalidParamsMsg(jsonParams, "invalid params, should be 4 params and exist " + m_ArgsParams.Length);
                            return;
                        }

                        string filename = m_ArgsParams[1];
                        if (File.Exists(filename))
                        {
                            if (!Boolean.TryParse(m_ArgsParams[2], out m_AutoIsShowMsg))
                            {
                                AutoRenderInvalidParamsMsg(jsonParams, "invalid 'isShowMsg' param, try parse '" + m_ArgsParams[2] + "' to bool field");
                                return;
                            }

                            string folderPath = Path.GetDirectoryName(filename);

                            Manager_MCAutomation.GetInstance().LoadFromJson(filename, folderPath);

                            if (m_AutoViewport == null)
                            {
                                if (!m_AutoIsShowMsg)
                                    m_IsNeedToClose = true;
                                else
                                    CloseStreamWriter();
                            }
                            else
                            {
                                m_PrintViewportPath = m_ArgsParams[3];
                                if (!Path.IsPathRooted(m_PrintViewportPath))
                                    m_PrintViewportPath = Path.Combine(folderPath, m_PrintViewportPath);
                            }

                        }
                        else
                        {
                            AutoRenderInvalidParamsMsg(jsonParams, "json file not exist");
                            return;
                        }
                    }
                }
            }


            if (isShowMapList && MapLoaderDefinitionManager != null)
            {
                MapLoaderDefinitionForm DefsFrm = new MapLoaderDefinitionForm();
                DefsFrm.ShowDialog();
            }

        }

        private void AutoRenderInvalidParamsMsg(String jsonParams, String msg)
        {
            MessageBox.Show(msg + Environment.NewLine + " params = " + jsonParams, "Auto Render : Invalid input parameters");
        }

        public static void LoadMapSchemesXMLFile()
        {
            // Load map schemes XML file
            string fileName = @"PrivateMapDefinitionsDatabase.xml";
            bool fileExists = System.IO.File.Exists(fileName);
            if (fileExists == true)
                MapLoaderDefinitionManager = MapLoaderDefinitionClass.LoadXmlFile(@"PrivateMapDefinitionsDatabase.xml");

            if (!fileExists || (fileExists && MapLoaderDefinitionManager == null))
                MapLoaderDefinitionManager = MapLoaderDefinitionClass.LoadXmlFile(@"MapDefinitionsDatabase.xml");

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MCTester.Managers.MapWorld.MCTMapFormManager.CloseTester();
            mAppTimer.Stop();
        }

        private void LoadAndSaveSchemes_Click(object sender, EventArgs e)
        {
            FolderSelectDialog FSDTarget = new FolderSelectDialog();
            FSDTarget.Title = "Source Directory";
            FSDTarget.InitialDirectory = @"c:\";
            if (FSDTarget.ShowDialog(IntPtr.Zero))
            {
                FolderSelectDialog FSDDestination = new FolderSelectDialog();
                FSDDestination.Title = "Destination Directory";
                FSDDestination.InitialDirectory = @"c:\";

                if (FSDDestination.ShowDialog(IntPtr.Zero))
                {
                    string[] filePaths = System.IO.Directory.GetFiles(FSDTarget.FileName);
                    IDNMcGridCoordinateSystem gridCoordSys = DNMcGridCoordSystemGeographic.Create(DNEDatumType._EDT_WGS84);
                    IDNMcOverlayManager om = DNMcOverlayManager.Create(gridCoordSys);
                    UserDataFactory userDataFactory = new UserDataFactory();
                    IDNMcObjectScheme[] objectScheme = new IDNMcObjectScheme[1];

                    try
                    {
                        foreach (string path in filePaths)
                        {
                            objectScheme = om.LoadObjectSchemes(path, userDataFactory);
                            om.SaveObjectSchemes(objectScheme,
                                                    FSDDestination.FileName + "\\" + System.IO.Path.GetFileName(path));
                        }

                        MessageBox.Show("Mission completed successfully", "Mission completed successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("Load and Save Schemes", McEx);
                    }
                }
            }
        }

        #endregion

        #region Public Static Properties
        public static bool BtnObjectWorldCheckedState
        {
            get { return mBtnObjectWorldCheckedState; }
            set { mBtnObjectWorldCheckedState = value; }
        }

        public static bool BtnMapWorldCheckedState
        {
            get { return mBtnViewportTerrainsCheckedState; }
            set { mBtnViewportTerrainsCheckedState = value; }
        }

        public static bool BtnTerrainLayersCheckedState
        {
            get { return mBtnTerrainLayersCheckedState; }
            set { mBtnTerrainLayersCheckedState = value; }
        }

        #endregion

        private bool OperationFeasibility()
        {
            if (MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport != null)
                return true;
            else
            {
                MessageBox.Show("This operation required a viewport", "Previous demand requires", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void tbsOpenLayers_Click(object sender, EventArgs e)
        {
            btnCreateLayerForm m_CreateLayerForm = new btnCreateLayerForm(MCTMapDevice.m_Device);//, newMapForm.MapPointer
            m_CreateLayerForm.ShowDialog();
        }

        private void tsbCenterMap_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnCenterMap m_CenterMap = new btnCenterMap();
            m_CenterMap.ExecuteAction();
        }

        private void TsbDropDownFootprintPoints_DropDownOpening(object sender, EventArgs e)
        {
            tsbCameraCornersAsync.Enabled = tsbCameraCornersSync.Enabled = tsbCameraCornersWithHorizonsAsync.Enabled = tsbCameraCornersWithHorizonsSync.Enabled = (MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport != null && MCTMapFormManager.MapForm.Viewport.GetImageCalc() != null);
        }

        private void tsbFootprintPointsSync_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnFootprintPoints m_FootprintPoints = new btnFootprintPoints(false);
            m_FootprintPoints.ExecuteAction();
        }

        private void tsbFootprintPointsAsync_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnFootprintPoints m_FootprintPoints = new btnFootprintPoints(true);
            m_FootprintPoints.ExecuteAction();
        }

        private void tsbCameraCornersSync_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnCameraCornersAndCenter m_CameraCornersAndCenter = new btnCameraCornersAndCenter();
            m_CameraCornersAndCenter.ExecuteAction(false, false);
        }

        private void tsbCameraCornersAsync_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnCameraCornersAndCenter m_CameraCornersAndCenter = new btnCameraCornersAndCenter();
            m_CameraCornersAndCenter.ExecuteAction(false, true);
        }

        private void tsbCameraCornersWithHorizonsSync_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnCameraCornersAndCenter m_CameraCornersAndCenter = new btnCameraCornersAndCenter();
            m_CameraCornersAndCenter.ExecuteAction(true, false);
        }

        private void tsbCameraCornersWithHorizonsAsync_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnCameraCornersAndCenter m_CameraCornersAndCenter = new btnCameraCornersAndCenter();
            m_CameraCornersAndCenter.ExecuteAction(true, true);
        }

        private void signToSecurityServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSignToSecurityServer signToSecurityServer = new btnSignToSecurityServer();
            signToSecurityServer.ShowDialog();
        }

        private void tsbNavPath_Click(object sender, EventArgs e)
        {
            btnNavCoreForm NavCoreForm = new btnNavCoreForm();
            NavCoreForm.Show();
        }

        private void tsbGeoRefMapGrid_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            m_BtnGrid.ExecuteAction(sender.ToString());
        }

        public static void CloseStreamWriter()
        {
            if (m_AutoStreamWriter != null)
            {
                string name = (m_AutoStreamWriter.BaseStream as FileStream).Name;

                m_AutoStreamWriter.Close();
                if (name != "" && new FileInfo(name).Length == 0)
                    File.Delete(name);
            }
            m_AutoStreamWriter = null;



        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseStreamWriter();
        }

        private void tsbSaveViewportToSession_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            Manager_MCAutomation.SaveViewportSession(MCTMapFormManager.MapForm.Viewport);
        }

        private void tsbLoadViewport_Click(object sender, EventArgs e)
        {
            MCMapWorldTreeViewForm.LoadSessionFromFolder(false);
        }

        private void tsbSightEllipseItem_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnSightEllipseItem sightEllipseItem = new btnSightEllipseItem();
            sightEllipseItem.ExecuteAction();
        }

        public static void CollectGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void tsbCollectGC_Click(object sender, EventArgs e)
        {
            CollectGC();
        }

        private void tsb2DTo3D_Click(object sender, EventArgs e)
        {
            if (OperationFeasibility() == false)
                return;

            btnOpen2D3DMap open3DMap = new btnOpen2D3DMap();
            open3DMap.ExecuteAction();   
        }
    }
}
