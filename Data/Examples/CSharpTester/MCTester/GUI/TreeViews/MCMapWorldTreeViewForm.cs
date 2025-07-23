using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MCTester;
using MCTester.GUI;
using MapCore;
using UnmanagedWrapper;
using MCTester.MapWorld.MapUserControls;
using MCTester.ObjectWorld.ObjectsUserControls;
using MCTester.ObjectWorld.Assit_Forms;
using MCTester.Managers.MapWorld;
using System.IO;
using MCTester.GUI.Map;
using MCTester.Managers.ObjectWorld;
using MCTester.General_Forms;
using MCTester.Managers;
using MCTester.Controls;
using MapCore.Common;
using MCTester.MapWorld;
using MCTester.Automation;
using MCTester.MapWorld.WizardForms;
using MCTester.MapWorld.Assist_Forms;
using System.Diagnostics;

namespace MCTester.GUI.Trees
{
    public partial class MCMapWorldTreeViewForm : TreeViewDisplayForm
    {
        public MCMapWorldTreeViewForm() : base()
        {
            InitializeComponent();
            this.cmsViewportOptions.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripWithRename_Opening);
            this.cmsMWRoot.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripWithoutNames_Opening);

            BuildTree();
        }

        public void BuildTree()
        {
            TreeFormBaseClass m_tree_Viewport = new TreeFormBaseClass();
            m_tree_Viewport.SourceDataArray = "MCTester.Managers.MapWorld.Manager_MCViewports";
            m_tree_Viewport.ClassHandles = typeof(IDNMcMapViewport);
            //m_tree_Viewport.ClassHandles = typeof(IDNMcSectionMapViewport);
            m_tree_Viewport.HandlerPanelType.Add(typeof(IDNMcMapViewport), "MCTester.MapWorld.MapUserControls.ucViewport");
            m_tree_Viewport.HandlerPanelType.Add(typeof(IDNMcSectionMapViewport), "MCTester.MapWorld.MapUserControls.ucViewport");

            TreeFormBaseClass m_tree_Terrain = new TreeFormBaseClass();
            m_tree_Terrain.SourceDataArray = "MCTester.Managers.MapWorld.Manager_MCTerrain";
            m_tree_Terrain.ClassHandles = typeof(IDNMcMapTerrain);
            m_tree_Terrain.HandlerPanelType.Add(typeof(IDNMcMapTerrain), "MCTester.MapWorld.MapUserControls.ucTerrain");

            TreeFormBaseClass m_tree_Camera = new TreeFormBaseClass();
            m_tree_Camera.SourceDataArray = "MCTester.Managers.MapWorld.Manager_MCCameras";
            m_tree_Camera.ClassHandles = typeof(IDNMcMapCamera);
            m_tree_Camera.HandlerPanelType.Add(typeof(IDNMcMapCamera), "MCTester.MapWorld.MapUserControls.ucCamera");

            TreeFormBaseClass m_tree_Grid = new TreeFormBaseClass();
            m_tree_Grid.SourceDataArray = "MCTester.Managers.MapWorld.Manager_MCGrid";
            m_tree_Grid.ClassHandles = typeof(IDNMcMapGrid);
            m_tree_Grid.HandlerPanelType.Add(typeof(IDNMcMapGrid), "MCTester.MapWorld.MapUserControls.ucGrid");

            TreeFormBaseClass m_tree_HeightLines = new TreeFormBaseClass();
            m_tree_HeightLines.SourceDataArray = "MCTester.Managers.MapWorld.Manager_MCMapHeightLines";
            m_tree_HeightLines.ClassHandles = typeof(IDNMcMapHeightLines);
            m_tree_HeightLines.HandlerPanelType.Add(typeof(IDNMcMapHeightLines), "MCTester.MapWorld.MapUserControls.ucMapHeightLines");

            TreeFormBaseClass m_tree_DTM_Layers = new TreeFormBaseClass();
            m_tree_DTM_Layers.SourceDataArray = "MCTester.Managers.MapWorld.Manager_MCLayers";
            m_tree_DTM_Layers.ClassHandles = typeof(IDNMcMapLayer);
            m_tree_DTM_Layers.HandlerPanelType.Add(typeof(IDNMcNativeDtmMapLayer), "MCTester.MapWorld.MapUserControls.ucDTMNativeLayer");
            m_tree_DTM_Layers.HandlerPanelType.Add(typeof(IDNMcRawDtmMapLayer), "MCTester.MapWorld.MapUserControls.ucDTMRawLayer");
            m_tree_DTM_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServerDtmMapLayer), "MCTester.MapWorld.MapUserControls.ucNativeServerDTMLayer");
            m_tree_DTM_Layers.HandlerPanelType.Add(typeof(IDNMcWebServiceDtmMapLayer), "MCTester.MapWorld.MapUserControls.ucDTMLayer");


            TreeFormBaseClass m_tree_Layers = new TreeFormBaseClass();
            m_tree_Layers.SourceDataArray = "MCTester.Managers.MapWorld.Manager_MCLayers";
            m_tree_Layers.ClassHandles = typeof(IDNMcMapLayer);

            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcMapLayer), "MCTester.MapWorld.MapUserControls.ucLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcDtmMapLayer), "MCTester.MapWorld.MapUserControls.ucDTMLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeDtmMapLayer), "MCTester.MapWorld.MapUserControls.ucDTMNativeLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcRawDtmMapLayer), "MCTester.MapWorld.MapUserControls.ucDTMRawLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcRasterMapLayer), "MCTester.MapWorld.MapUserControls.ucRasterLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeRasterMapLayer), "MCTester.MapWorld.MapUserControls.ucRasterNativeLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcRawRasterMapLayer), "MCTester.MapWorld.MapUserControls.ucRasterRawLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcVectorMapLayer), "MCTester.MapWorld.MapUserControls.ucVectorLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeVectorMapLayer), "MCTester.MapWorld.MapUserControls.ucVectorNativeLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcRawVectorMapLayer), "MCTester.MapWorld.MapUserControls.ucVectorRawLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeHeatMapLayer), "MCTester.MapWorld.MapUserControls.ucHeatLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeMaterialMapLayer), "MCTester.MapWorld.MapUserControls.ucMaterialMapLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeTraversabilityMapLayer), "MCTester.MapWorld.MapUserControls.ucTraversabilityMapLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServerTraversabilityMapLayer), "MCTester.MapWorld.MapUserControls.ucTraversabilityMapLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcRawTraversabilityMapLayer), "MCTester.MapWorld.MapUserControls.ucTraversabilityRawMapLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServerMaterialMapLayer), "MCTester.MapWorld.MapUserControls.ucMaterialMapLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcRawMaterialMapLayer), "MCTester.MapWorld.MapUserControls.ucMaterialRawMapLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServerDtmMapLayer), "MCTester.MapWorld.MapUserControls.ucNativeServerDTMLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServerRasterMapLayer), "MCTester.MapWorld.MapUserControls.ucNativeServerRasterLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNative3DModelMapLayer), "MCTester.MapWorld.MapUserControls.ucNative3DModelLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServer3DModelMapLayer), "MCTester.MapWorld.MapUserControls.ucNativeServer3DModelLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcRaw3DModelMapLayer), "MCTester.MapWorld.MapUserControls.ucRaw3DModelLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeVector3DExtrusionMapLayer), "MCTester.MapWorld.MapUserControls.ucNativeVector3DExtrusionLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServerVector3DExtrusionMapLayer), "MCTester.MapWorld.MapUserControls.ucNativeServerVector3DExtrusionLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcRawVector3DExtrusionMapLayer), "MCTester.MapWorld.MapUserControls.ucRawVector3DExtrusionLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServerVectorMapLayer), "MCTester.MapWorld.MapUserControls.ucNativeServerVectorLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcWebServiceRasterMapLayer), "MCTester.MapWorld.MapUserControls.ucRasterLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcWebServiceDtmMapLayer), "MCTester.MapWorld.MapUserControls.ucDTMLayer");

            m_tree_Viewport.Children.Add(typeof(IDNMcMapTerrain), m_tree_Terrain);
            m_tree_Viewport.Children.Add(typeof(IDNMcMapCamera), m_tree_Camera);
            m_tree_Viewport.Children.Add(typeof(IDNMcMapGrid), m_tree_Grid);
            m_tree_Viewport.Children.Add(typeof(IDNMcMapHeightLines), m_tree_HeightLines);
            m_tree_Viewport.Children.Add(typeof(IDNMcMapLayer), m_tree_DTM_Layers);
            m_tree_Viewport.Children.Add(typeof(IDNMcNativeDtmMapLayer), m_tree_DTM_Layers);
            m_tree_Viewport.Children.Add(typeof(IDNMcRawDtmMapLayer), m_tree_DTM_Layers);
            m_tree_Viewport.Children.Add(typeof(IDNMcNativeServerDtmMapLayer), m_tree_DTM_Layers);
            m_tree_Viewport.Children.Add(typeof(IDNMcWebServiceDtmMapLayer), m_tree_DTM_Layers);

            m_tree_Terrain.Children.Add(typeof(IDNMcMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcDtmMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeDtmMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcRawDtmMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcRasterMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeRasterMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcRawRasterMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcVectorMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeVectorMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcRawVectorMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeHeatMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeMaterialMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcRawMaterialMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeTraversabilityMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeServerTraversabilityMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcRawTraversabilityMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeServerMaterialMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeServerDtmMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeServerRasterMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcRaw3DModelMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNative3DModelMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeServer3DModelMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcRawVector3DExtrusionMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeVector3DExtrusionMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeServerVector3DExtrusionMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeServerVectorMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcWebServiceRasterMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcWebServiceDtmMapLayer), m_tree_Layers);

            base.CreateTree();

            //Create Viewport - Terrain tree
            this.TreeDefinitionClass = m_tree_Viewport;
            HandleFirstTreeElement(TreeDefinitionClass, m_TreeView.Nodes[0]);

            //Create Grid tree
            this.TreeDefinitionClass = m_tree_Grid;
            HandleFirstTreeElement(TreeDefinitionClass, m_TreeView.Nodes[0]);

            //Create Map Height Lines tree
            this.TreeDefinitionClass = m_tree_HeightLines;
            HandleFirstTreeElement(TreeDefinitionClass, m_TreeView.Nodes[0]);

            this.TreeDefinitionClass = m_tree_Terrain;
            HandleFirstTreeElement(TreeDefinitionClass, m_TreeView.Nodes[0]);

            this.TreeDefinitionClass = m_tree_Layers;
            HandleFirstTreeElement(TreeDefinitionClass, m_TreeView.Nodes[0]);

           // m_TreeView.Nodes[0].Expand();

            CurrentViewportOfSelectedLayer = null;

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        protected override void m_TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            base.m_TreeView_NodeMouseClick(sender, e);

            if (e.Node.Tag != null)
            {
                string interfaceName = GeneralFuncs.GetDirectInterfaceName(m_NodeClickedType.GetType());
                if (e.Button == MouseButtons.Right)
                {
                    switch (interfaceName)
                    {
                        case "IDNMcMapViewport":
                        case "IDNMcSectionMapViewport":
                            cmsViewportOptions.Show(m_TreeView, e.Location);
                            break;
                        case "IDNMcMapLayer":
                        case "IDNMcDtmMapLayer":
                        case "IDNMcNativeDtmMapLayer":
                        case "IDNMcRawDtmMapLayer":
                        case "IDNMcRasterMapLayer":
                        case "IDNMcNativeRasterMapLayer":
                        case "IDNMcRawRasterMapLayer":
                        case "IDNMcVectorMapLayer":
                        case "IDNMcNativeVectorMapLayer":
                        case "IDNMcRawVectorMapLayer":
                        case "IDNMcNativeHeatMapLayer":
                        case "IDNMcRawMaterialMapLayer":
                        case "IDNMcNativeMaterialMapLayer":
                        case "IDNMcRawTraversabilityMapLayer":
                        case "IDNMcNativeTraversabilityMapLayer":
                        case "IDNMcNativeServerDtmMapLayer":
                        case "IDNMcNativeServerRasterMapLayer":
                        case "IDNMcNativeServerVectorMapLayer":
                        case "IDNMcWebServiceRasterMapLayer":
                        case "IDNMcWebServiceDtmMapLayer":
                        case "IDNMcRaw3DModelMapLayer":
                        case "IDNMcNative3DModelMapLayer":
                        case "IDNMcNativeServer3DModelMapLayer":
                        case "IDNMcNativeVector3DExtrusionMapLayer":
                        case "IDNMcRawVector3DExtrusionMapLayer":
                        case "IDNMcNativeServerVector3DExtrusionMapLayer":
                        case "IDNMcNativeServerMaterialMapLayer":
                            cmsLayerOptions.Show(m_TreeView, e.Location);
                            break;
                        case "IDNMcMapTerrain":
                            cmsTerrainOptions.Show(m_TreeView, e.Location);
                            break;
                        case "IDNMcMapCamera":
                            cmbCameraOptions.Show(m_TreeView, e.Location);
                            break;
                        default:
                            break;
                    }
                }
                else if (e.Button == MouseButtons.Left)
                {
                    switch (interfaceName)
                    {
                        case "IDNMcVectorMapLayer":
                        case "IDNMcNativeVectorMapLayer":
                        case "IDNMcRawVectorMapLayer":
                        case "IDNMcNativeServerVectorMapLayer":
                            if (GrandfatherOfSelectedNode != null && GrandfatherOfSelectedNode is IDNMcMapViewport)
                                CurrentViewportOfSelectedLayer = (IDNMcMapViewport)GrandfatherOfSelectedNode;
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right && e.Node.Text == "Root")
            {
                cmsMWRoot.Show(m_TreeView, e.Location);
            }
        }

        private void miNewGrid_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcMapGrid newGrid = DNMcMapGrid.Create(new DNSGridRegion[0], new DNSScaleStep[0], Manager_MCGeneralDefinitions.UseBasicItemPropertiesOnly);
                MCTester.Managers.MapWorld.Manager_MCGrid.AddNewMapGrid(newGrid);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcMapGrid.Create", McEx);
            }

            BuildTree();
        }

        private void miNewMapHeightLines_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcMapHeightLines newHeightLines = DNMcMapHeightLines.Create(new DNSHeightLinesScaleStep[0], 1);
                MCTester.Managers.MapWorld.Manager_MCMapHeightLines.CreateMapHeightLines(newHeightLines);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcMapHeightLines.Create", McEx);
            }

            BuildTree();
        }

        private void miSetGrid_Click(object sender, EventArgs e)
        {
          
            frmGridList GridListForm = new frmGridList();
            if (GridListForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ((IDNMcMapViewport)SelectedNode).Grid = GridListForm.SelectedGrid;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Grid", McEx);
                }
            }

            // turn on relevant viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(((IDNMcMapViewport)SelectedNode).OverlayManager);

            BuildTree();
        }

        private void miRemoveGrid_Click(object sender, EventArgs e)
        {
                try
                {
                    ((IDNMcMapViewport)SelectedNode).Grid = null;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Grid", McEx);
                }

            // turn on relevant viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(((IDNMcMapViewport)SelectedNode).OverlayManager);

            BuildTree();
        }

        private void miSetHeightLines_Click(object sender, EventArgs e)
        {
            frmMapHeightLines MapHeightLinesForm = new frmMapHeightLines();
            if (MapHeightLinesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ((IDNMcMapViewport)SelectedNode).SetHeightLines(MapHeightLinesForm.SelectedMapHeightLines);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetHeightLines", McEx);
                }
            }

            // turn on relevant viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(((IDNMcMapViewport)SelectedNode).OverlayManager);

            BuildTree();
        }

        private void miRemoveHeightLines_Click(object sender, EventArgs e)
        {
            try
            {
                ((IDNMcMapViewport)SelectedNode).SetHeightLines(null);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetHeightLines", McEx);
            }

            // turn on relevant viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(((IDNMcMapViewport)SelectedNode).OverlayManager);

            BuildTree();
        }

        private void miSaveLayerToAFile_Click(object sender, EventArgs e)
        {
            frmSaveLoad SaveLoadLayerForm = new frmSaveLoad(sender, null, (IDNMcMapLayer)SelectedNode, false);
            SaveLoadLayerForm.ShowDialog();

            BuildTree();
        }

        private void miSaveLayerToABuffer_Click(object sender, EventArgs e)
        {
            frmSaveLoad SaveLoadLayerForm = new frmSaveLoad(sender, null, (IDNMcMapLayer)SelectedNode, false);
            SaveLoadLayerForm.ShowDialog();

            BuildTree();
        }

        private void m_TreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "Root")
            {
                BuildTree();
            }
        }

        private void ShowExpError(Exception ex)
        {
            MessageBox.Show("Error open/write to file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (stw != null)
                stw.Close();
        }

        private StreamWriter stw = null;

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
                if (isSRID && pastrCreateParams != null && pastrCreateParams.Length > 0)
                    currGridCoorSys.EpsgCode = pastrCreateParams[0];
            }
            return currGridCoorSys;
        }

        private void miSaveViewportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manager_MCAutomation.SaveViewportSession((IDNMcMapViewport)SelectedNode);

        }

        private void CloseFile()
        {
            if (stw != null)
                stw.Close();
        }

      
        public static bool LoadSessionFromFolder(bool bWithoutSize)
        {
            FolderSelectDialog FSD = new FolderSelectDialog();
            FSD.Title = "Folder to select";
            //FSD.InitialDirectory = @"c:\";
            if (FSD.ShowDialog(IntPtr.Zero))
            {
                string filename = @"\FullViewportDataParams.json";

                string jsonPath = FSD.FileName + filename;

                if (File.Exists(jsonPath))
                {
                    MainForm.m_AutoIsShowMsg = true;
                    Manager_MCAutomation.GetInstance().LoadFromJson(jsonPath, FSD.FileName, bWithoutSize);
                 

                    return true;
                }
                else
                {
                    string filePath = FSD.FileName + @"\ViewportDataParams.txt";
                    if (File.Exists(filePath))
                    {
                        StreamReader stream = new StreamReader(filePath);

                        char[] trimSign = new char[] { ':' };
                        IDNMcMapViewport Viewport = null;
                        IDNMcMapCamera Camera = null;
                        MCTMapForm NewMapForm = new MCTMapForm(false);

                        MCTMapFormManager.AddMapForm(NewMapForm);

                        uint uViewportID = uint.Parse(stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);
                        // skip empty line
                        stream.ReadLine();

                        DNEMapType eMapType = (DNEMapType)Enum.Parse(typeof(DNEMapType), stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);

                        DNSCreateDataMV createData = new DNSCreateDataMV(eMapType);

                        createData.uViewportID = uViewportID;
                        createData.hWnd = NewMapForm.MapPointer;
                        if (createData.hWnd == IntPtr.Zero)
                        {
                            createData.uWidth = (uint)NewMapForm.Width;
                            createData.uHeight = (uint)NewMapForm.Height;
                        }
                        createData.pGrid = null;
                        createData.bShowGeoInMetricProportion = false;
                        createData.pImageCalc = null;

                        try
                        {
                            if (MCTMapDevice.m_Device == null)
                            {

                                MCTMapDevice MapDevice = new MCTMapDevice();
                                MapDevice.BackgroundThreads = 1;
                                MapDevice.LoggingLevel = DNELoggingLevel._ELL_HIGH;
                                createData.pDevice = MapDevice.CreateDevice(false);
                            }
                            else
                                createData.pDevice = MCTMapDevice.m_Device;
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcMapDevice.Create", McEx);
                        }

                        // skip 2 empty line
                        stream.ReadLine();
                        stream.ReadLine();

                        //create the viewport coordinate system
                        DNEGridCoordSystemType gridCoordSys = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);

                        switch (gridCoordSys)
                        {
                            case DNEGridCoordSystemType._EGCS_GEOGRAPHIC:
                                DNEDatumType geographicDatum = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);
                                stream.ReadLine(); // skip line containing Zone
                                stream.ReadLine(); // skip line containing epsg code
                                try
                                {
                                    createData.CoordinateSystem = DNMcGridCoordSystemGeographic.Create(geographicDatum);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("DNMcGridCoordSystemGeographic.Create", McEx);
                                }

                                break;
                            case DNEGridCoordSystemType._EGCS_UTM:
                                DNEDatumType UtmDatumType = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);
                                try
                                {
                                    createData.CoordinateSystem = DNMcGridUTM.Create(int.Parse(stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]), UtmDatumType);
                                    stream.ReadLine(); // skip line containing epsg code
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("DNMcGridUTM.Create", McEx);
                                }
                                break;
                            default:
                                MessageBox.Show("Current viewport coordinate system not supported in load viewport", "Viewport coordinate system not supported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                stream.Close();
                                return false;
                        }

                        // skip empty line
                        stream.ReadLine();

                        // Read number of terrains 
                        int numTerrains = int.Parse(stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);
                        IDNMcMapTerrain[] terrainArr = null;

                        // Collect terrain array
                        if (numTerrains > 0)
                        {
                            terrainArr = new IDNMcMapTerrain[numTerrains];
                            UserDataFactory UDF = new UserDataFactory();

                            for (int i = 0; i < numTerrains; i++)
                            {
                                string pathFolder = stream.ReadLine();
                                if (!Path.IsPathRooted(pathFolder))
                                    pathFolder = FSD.FileName + "\\" + pathFolder;
                                terrainArr[i] = DNMcMapTerrain.Load(pathFolder, @"c:\\maps", UDF,
                                                              new MCTMapLayerReadCallbackFactory());

                                Manager_MCLayers.CheckingAfterLoadTerrain(terrainArr[i], false);
                            }
                        }

                        // create the overlay manager coordinate system
                        // skip 2 empty line
                        stream.ReadLine();
                        stream.ReadLine();

                        gridCoordSys = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);
                        IDNMcGridCoordinateSystem overlayManagerCoordSys = null;
                        switch (gridCoordSys)
                        {
                            case DNEGridCoordSystemType._EGCS_GEOGRAPHIC:
                                DNEDatumType geographicDatum = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);
                                stream.ReadLine(); // skip line containing Zone
                                stream.ReadLine(); // skip line containing espgcode
                                try
                                {
                                    overlayManagerCoordSys = DNMcGridCoordSystemGeographic.Create(geographicDatum);

                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("DNMcGridCoordSystemGeographic.Creat", McEx);
                                }

                                break;
                            case DNEGridCoordSystemType._EGCS_UTM:
                                DNEDatumType UtmDatumType = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);
                                try
                                {
                                    overlayManagerCoordSys = DNMcGridUTM.Create(int.Parse(stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]), UtmDatumType);
                                    stream.ReadLine(); // skip line containing espgcode
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("DNMcGridUTM.Create", McEx);
                                }
                                break;
                            default:
                                MessageBox.Show("Current overlay manager coordinate system not supported in load viewport", "Overlay manager coordinate system not supported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                stream.Close();
                                return false;
                        }

                        try
                        {
                            IDNMcOverlayManager overlayManager = Manager_MCOverlayManager.CreateOverlayManager(overlayManagerCoordSys);
                            createData.pOverlayManager = overlayManager;
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcOverlayManager.Create", McEx);
                            MessageBox.Show("Current overlay manager coordinate system not supported in load viewport", "Overlay manager coordinate system not supported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            stream.Close();
                            return false;
                        }

                        try
                        {
                            DNMcMapViewport.Create(ref Viewport, ref Camera, createData, terrainArr);
                            Manager_MCViewports.AddViewport(Viewport);

                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("Create", McEx);
                            stream.Close();
                            return false;
                        }

                        //MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.RenderMap();
                        NewMapForm.Viewport = Viewport;
                        NewMapForm.CreateEditMode(Viewport);

                        // skip empty line
                        stream.ReadLine();

                        // Read number of overlays 
                        int numOverlays = int.Parse(stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);

                        // Collect overlay array
                        try
                        {
                            if (numOverlays > 0)
                            {
                                UserDataFactory UDF = new UserDataFactory();

                                for (int i = 0; i < numOverlays; i++)
                                {
                                    string pathFolder = stream.ReadLine();
                                    if (!Path.IsPathRooted(pathFolder))
                                        pathFolder = FSD.FileName + "\\" + pathFolder;

                                    IDNMcOverlay overlay = DNMcOverlay.Create(Viewport.OverlayManager);
                                    if (i == 0)
                                        Manager_MCOverlayManager.UpdateOverlayManager(Viewport.OverlayManager, overlay);
                                    overlay.LoadObjects(pathFolder, UDF);
                                }
                            }
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcOverlay.Create or LoadObjects", McEx);
                        }

                        NewMapForm.Show();

                        // skip 2 empty line
                        stream.ReadLine();
                        stream.ReadLine();

                        try
                        {
                            double x, y, z;
                            x = double.Parse(stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);
                            y = double.Parse(stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);
                            z = double.Parse(stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);

                            Camera.SetCameraPosition(new DNSMcVector3D(x, y, z), false);
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("SetCameraPosition", McEx);
                        }

                        try
                        {
                            float yaw, pitch, roll;
                            yaw = float.Parse(stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);
                            pitch = float.Parse(stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);
                            roll = float.Parse(stream.ReadLine().Split(trimSign, StringSplitOptions.RemoveEmptyEntries)[1]);

                            Camera.SetCameraOrientation(yaw, pitch, roll, false);
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("SetCameraOrientation", McEx);
                        }
                        stream.Close();

                        return true;
                    }
                    else
                        MessageBox.Show("Wrong path : " + filePath, "Load viewport from file");
                }


            }
            return false;
        }

        public void miLoadSessionFromFolder_Click(object sender, EventArgs e)
        {
            if (LoadSessionFromFolder(false))
                BuildTree();
        }

        private void MCViewportTerrainTreeViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.BtnMapWorldCheckedState = false;

            Manager_MCOverlay.RemoveAllOverlaysFromVectorItemsOverlay();

            FindControls(this);
        }

        private void FindControls(Control control)
        {
            if (control is ucViewport)
            {
                ((ucViewport)control).DeleteTempObjects();
                return;
            }
            else if (control is ucTerrain)
            {
                ((ucTerrain)control).DeleteTempObjects();
                return;
            }
            else if (control is ucLayer)
            {
                ((ucLayer)control).DeleteTempObjects();
                return;
            }
            foreach (Control cntl in control.Controls)
            {
                FindControls(cntl);
            }
        }

        private void miRenameLayer_Click(object sender, EventArgs e)
        {
            Rename();
        }

        private void miDeleteLayer_Click(object sender, EventArgs e)
        {
            DeleteName();
        }

        private void miRenameVP_Click(object sender, EventArgs e)
        {
            Rename();
        }

        private void miDeleteVP_Click(object sender, EventArgs e)
        {
            DeleteName();
        }

        private void imRenameTerrain_Click(object sender, EventArgs e)
        {
            Rename();
        }

        private void imDeleteNameTerrain_Click(object sender, EventArgs e)
        {
            DeleteName();
        }

        private void newTerrainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDNMcMapViewport currViewport = (IDNMcMapViewport)SelectedNode;
            TerrainWizzardForm terWiz = new TerrainWizzardForm();
            if (terWiz.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    currViewport.AddTerrain(terWiz.Terrain);
                    Manager_MCTerrain.RemoveTerrainFromDic(terWiz.Terrain);
                    // turn on relevant viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(currViewport.OverlayManager);

                    BuildTree();
                }
                catch (MapCoreException McEx)
                {
                    if (terWiz.Terrain != null)
                    {
                        Manager_MCTerrain.RemoveTerrain(terWiz.Terrain);
                    }
                    Utilities.ShowErrorMessage("AddTerrain", McEx);
                }
            }
           /* else
                MessageBox.Show("add terrain failed");*/


        }

        public object ParentOfSelectedNode
        {
            get
            {
                if (m_TreeView.SelectedNode.Parent.Tag != null)
                    return ((TreeFormBaseClass)m_TreeView.SelectedNode.Parent.Tag).TemporaryObject;
                else
                    return null;
            }
        }

        public static IDNMcMapViewport CurrentViewportOfSelectedLayer;

        public object GrandfatherOfSelectedNode
        {
            get
            {
                if (m_TreeView.SelectedNode != null && m_TreeView.SelectedNode.Parent != null && m_TreeView.SelectedNode.Parent.Parent != null && m_TreeView.SelectedNode.Parent.Parent.Tag != null)
                    return ((TreeFormBaseClass)m_TreeView.SelectedNode.Parent.Parent.Tag).TemporaryObject;
                else
                    return null;
            }
        }

        private void imRemoveTerrain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove Terrain?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IDNMcMapTerrain terrain = (IDNMcMapTerrain)SelectedNode;
                try
                {
                    IDNMcMapViewport currViewport = (IDNMcMapViewport)ParentOfSelectedNode;
                    if (currViewport != null)
                    {
                        currViewport.RemoveTerrain(terrain);
                       // turn on relevant viewports render needed flags
                       MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(currViewport.OverlayManager);
                    }
                    
                    Manager_MCTerrain.RemoveTerrain(terrain);
                    BuildTree();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("RemoveTerrain", McEx);
                }


            }
        }

        private void miCamera_Click(object sender, EventArgs e)
        {
            IDNMcMapViewport mapViewport = (IDNMcMapViewport)SelectedNode;

            try
            {
                IDNMcMapCamera newCamera = mapViewport.CreateCamera();

                IDNMcMapTerrain[] terrainInViewport = mapViewport.GetTerrains();
                if (terrainInViewport != null && terrainInViewport.Length > 0)
                {
                    DNSMcVector3D minVertex = terrainInViewport[0].BoundingBox.MinVertex;
                    DNSMcVector3D maxVertex = terrainInViewport[0].BoundingBox.MaxVertex;

                    DNSMcVector3D newCameraPosition = new DNSMcVector3D();
                    newCameraPosition.x = (maxVertex.x + minVertex.x) / 2;
                    newCameraPosition.y = (maxVertex.y + minVertex.y) / 2;
                    newCameraPosition.z = (maxVertex.z + minVertex.z) / 2;

                    newCamera.SetCameraPosition(newCameraPosition, false);
                }
                // turn on relevant viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(mapViewport.OverlayManager);

                BuildTree();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CreateCamera", McEx);
            }
        }

        private void miDestroyCamera_Click(object sender, EventArgs e)
        {
            IDNMcMapViewport currViewport = (IDNMcMapViewport)ParentOfSelectedNode;
            IDNMcMapCamera currCamera = (IDNMcMapCamera)SelectedNode;
            if (MessageBox.Show("Destroy Camera?", "Destroy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    currViewport.DestroyCamera(currCamera);

                    // turn on relevant viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(currViewport.OverlayManager);

                    BuildTree();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DestroyCamera", McEx);
                }
            }
        }

        private void miSetActiveCamera_Click(object sender, EventArgs e)
        {
            IDNMcMapViewport currViewport = (IDNMcMapViewport)ParentOfSelectedNode;

            try
            {
                currViewport.ActiveCamera = (IDNMcMapCamera)SelectedNode;

                // turn on relevant viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(currViewport.OverlayManager);

                //BuildTree();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ActiveCamera", McEx);
            }
        }

        private void miMoveToCenterLayer_Click(object sender, EventArgs e)
        {

            IDNMcMapLayer currLayer = (IDNMcMapLayer)SelectedNode;
            try
            {
                if (currLayer.BoundingBox != null)
                {
                    DNSMcVector3D centerPoint = currLayer.BoundingBox.CenterPoint();
                    IDNMcMapViewport mapViewport = MCTMapFormManager.MapForm.Viewport;
                    if (GrandfatherOfSelectedNode != null && GrandfatherOfSelectedNode is IDNMcMapViewport)
                        mapViewport = (IDNMcMapViewport)GrandfatherOfSelectedNode;
                    MCTAsyncQueryCallback.MoveToCenterPoint(centerPoint, mapViewport);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("BoundingBox", McEx);
            }
        }

        private void addAllExistingFormsToSchemesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MCTMapFormManager.MapForm != null)
                MCTMapFormManager.MapForm.AddAllMapFormsToScheme();
        }

        private void addMapFormToSchemesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MCTMapFormManager.MapForm != null)
                MCTMapFormManager.MapForm.AddMapToScheme();
        }

        private void miLoadSessionFromFolderWithoutViewportSize_Click(object sender, EventArgs e)
        {
            if (LoadSessionFromFolder(true))
                BuildTree();
        }

        private void miLoadTerrainFromAFile_Click(object sender, EventArgs e)
        {
            frmSaveLoad SaveLoadTerrainForm = new frmSaveLoad(sender, null, null, true);
            SaveLoadTerrainForm.ShowDialog();

            BuildTree();
        }

        private void miLoadTerrainFromABuffer_Click(object sender, EventArgs e)
        {
            frmSaveLoad SaveLoadTerrainForm = new frmSaveLoad(sender, null, null, true);
            SaveLoadTerrainForm.ShowDialog();

            BuildTree();
        }

        private void miCreateNewTerrain_Click(object sender, EventArgs e)
        {
            TerrainWizzardForm TerrainWiz = new TerrainWizzardForm();
            Manager_MCLayers.AddTerrainWizzardFormToList(TerrainWiz);
            if (TerrainWiz.ShowDialog(this) == DialogResult.OK)
                BuildTree();

            Manager_MCLayers.RemoveTerrainWizzardFormFromList(TerrainWiz);
        }

        private void miLoadLayerFromAFile_Click(object sender, EventArgs e)
        {
            frmSaveLoad SaveLoadLayerForm = new frmSaveLoad(sender, null, null, false);
            SaveLoadLayerForm.ShowDialog();

            BuildTree();
        }

        private void miLoadLayerFromABuffer_Click(object sender, EventArgs e)
        {
            frmSaveLoad SaveLoadLayerForm = new frmSaveLoad(sender, null, null, false);
            SaveLoadLayerForm.ShowDialog();

            BuildTree();
        }

        private void miCreateNewLayer_Click(object sender, EventArgs e)
        {
            AddLayerForm layerWiz = new AddLayerForm();

            if (layerWiz.ShowDialog(this) == DialogResult.OK)
                BuildTree();

            MCTMapLayerReadCallback.RemoveAllReplacedLayer();
        }

        private void miBackgroungThreadIndex_Click(object sender, EventArgs e)
        {
            frmBackgroundThreadIndex frm = new frmBackgroundThreadIndex();
            frm.Show();
        }

        private void checkAllNativeServerLayersValidityAsyncToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void checkValidityBeforeRenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkValidityBeforeRenderToolStripMenuItem.Checked = !checkValidityBeforeRenderToolStripMenuItem.Checked;
            MainForm.CheckAllNativeServerLayersValidityAsyncCheckedState = checkValidityBeforeRenderToolStripMenuItem.Checked;
        }

        private void buildIndexingDataFor3DModelToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmBuildIndexingDataForRawStaticObjects frmBuildIndexingDataFor3DModel = new frmBuildIndexingDataForRawStaticObjects();
            frmBuildIndexingDataFor3DModel.Show();
        }

        private void deleteIndexingDataForRaw3DModelToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmDeleteIndexingData frmDeleteIndexingDataFor3DModel = new frmDeleteIndexingData();
            frmDeleteIndexingDataFor3DModel.Show();
        }

        private void buildIndexingDataForVector3DExtrusionToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmBuildIndexingDataForRawStaticObjects frmBuildIndexingData = new frmBuildIndexingDataForRawStaticObjects(false);
            frmBuildIndexingData.Show();
        }

        private void deleteIndexingDataForRawVector3DExtrusionToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmDeleteIndexingData frmDeleteIndexingData = new frmDeleteIndexingData(false);
            frmDeleteIndexingData.Show();
        }

        private void miAddLayer_Click(object sender, EventArgs e)
        {
            IDNMcMapTerrain currTerrain = (IDNMcMapTerrain)SelectedNode;
            Manager_MCLayers.ResetServerLayersPriorityDic();
            AddLayerForm layerWiz = new AddLayerForm();
            Manager_MCLayers.AddLayerFormToList(layerWiz);
            try
            {
                DialogResult res = layerWiz.ShowDialog();

                if (res == DialogResult.OK)
                {
                    MCTMapLayerReadCallback.IsSaveReplacedOrRemovedLayer = true;
                    if (layerWiz.UserAction == AddLayerForm.EUserAction.CreateNewLayer || layerWiz.UserAction == AddLayerForm.EUserAction.SelectFromServer)
                    {
                        List<IDNMcMapLayer> allNewLayers = MCTMapLayerReadCallback.CheckLayersIsRemovedOrReplaced(layerWiz.NewLayers);

                        foreach (IDNMcMapLayer newLayer in allNewLayers)
                        {
                            Manager_MCLayers.RemoveStandaloneLayer(newLayer);
                            currTerrain.AddLayer(newLayer);
                        }
                    }
                    else
                    {
                        IDNMcMapLayer newLayer = MCTMapLayerReadCallback.CheckLayerIsRemovedOrReplaced(layerWiz.NewLayer);
                        Manager_MCLayers.RemoveStandaloneLayer(newLayer);
                        currTerrain.AddLayer(newLayer);
                    }
                    Manager_MCLayers.DrawPriorityServerLayer(currTerrain);

                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("AddLayer", McEx);
            }
            Manager_MCLayers.RemoveLayerFormFromList(layerWiz);

            MCTMapLayerReadCallback.RemoveAllReplacedLayer();
            BuildTree();
        }

        private void miRemoveLayer_Click(object sender, EventArgs e)
        {
            IDNMcMapTerrain currTerrain = (IDNMcMapTerrain)SelectedNode;
            frmLayersList LayerListForm = new frmLayersList(currTerrain);
            if (LayerListForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (LayerListForm.SelectedLayers.Length > 0)
                    {
                        for (int i = 0; i < LayerListForm.SelectedLayers.Length; i++)
                        {
                            IDNMcMapLayer layerToRemove = LayerListForm.SelectedLayers[i];
                            currTerrain.RemoveLayer(layerToRemove);
                            Manager_MCLayers.NoticeLayerRemoved(layerToRemove);
                            layerToRemove.Dispose();
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("RemoveLayer", McEx);
                }
            }

            BuildTree();
        }

        private void miAddAndRemoveVectorLayers_Click(object sender, EventArgs e)
        {
            AddAndRemoveVectorLayersForm addAndRemoveVectorLayersForm = new MCTester.MapWorld.WizardForms.AddAndRemoveVectorLayersForm((IDNMcMapTerrain)SelectedNode);
            addAndRemoveVectorLayersForm.Show();
        }

        private void removeLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDNMcMapTerrain currTerrain = null;
            if (m_TreeView.SelectedNode.Parent != null && m_TreeView.SelectedNode.Parent.Tag != null)
            {
                object objParent = ((TreeFormBaseClass)m_TreeView.SelectedNode.Parent.Tag).TemporaryObject;
                if (objParent != null && objParent is IDNMcMapTerrain) 
                    currTerrain = (IDNMcMapTerrain)objParent;
            }
            IDNMcMapLayer layerToRemove = (IDNMcMapLayer)SelectedNode;

            try
            {
                if (currTerrain != null)
                    currTerrain.RemoveLayer(layerToRemove);
                Manager_MCLayers.NoticeLayerRemoved(layerToRemove);
                layerToRemove.Dispose();
                Manager_MCLayers.RemoveStandaloneLayer(layerToRemove);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("RemoveLayer", McEx);
            }
            BuildTree();
        }

        private void miSaveTerrainToAFile_Click(object sender, EventArgs e)
        {
            frmSaveLoad SaveLoadTerrainForm = new frmSaveLoad(sender, (IDNMcMapTerrain)SelectedNode, null, true);
            SaveLoadTerrainForm.ShowDialog();

            BuildTree();
        }

        private void miSaveTerrainToABuffer_Click(object sender, EventArgs e)
        {
            frmSaveLoad SaveLoadTerrainForm = new frmSaveLoad(sender, (IDNMcMapTerrain)SelectedNode, null, true);
            SaveLoadTerrainForm.ShowDialog();

            BuildTree();
        }

        private void MCViewportTerrainTreeViewForm_Load(object sender, EventArgs e)
        {
            ExpendTree(2);
        }

        private void smartRealityJumpToBuildingToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport != null)
            {
                ScanItemsFoundFormHistoryBuildings JumpToBuilding = new ScanItemsFoundFormHistoryBuildings(ScanItemsFoundFormHistoryBuildings.Source.JumpToBuilding);
                JumpToBuilding.ShowDialog();
                if (JumpToBuilding.DialogResult == DialogResult.OK)
                    MainForm.MinimizeMapWorldTreeViewForm();
            }
            else
            {
                MessageBox.Show("This operation required a viewport", "Previous demand requires", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void smartRealityShowSurfaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport != null)
            {
                ScanItemsFoundFormHistoryBuildings showSurface = new ScanItemsFoundFormHistoryBuildings(ScanItemsFoundFormHistoryBuildings.Source.ShowSurfaces);
                showSurface.ShowDialog();
                if (showSurface.DialogResult == DialogResult.OK)
                    MainForm.MinimizeMapWorldTreeViewForm();
            }
            else
            {
                MessageBox.Show("This operation required a viewport", "Previous demand requires", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}