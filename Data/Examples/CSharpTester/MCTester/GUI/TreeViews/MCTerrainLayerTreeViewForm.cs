using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.GUI;
using MCTester;
using MCTester.MapWorld.MapUserControls;
using MCTester.Managers.MapWorld;
using MCTester.General_Forms;
using MCTester.Managers;
using MCTester.MapWorld.Assist_Forms;
using MCTester.Managers.ObjectWorld;
using MCTester.MapWorld.WizardForms;
using MapCore.Common;

namespace MCTester.GUI.Trees
{
    public partial class MCTerrainLayerTreeViewForm : TreeViewDisplayForm
    {
        public MCTerrainLayerTreeViewForm() : base()
        {
            InitializeComponent();
            BuildTree();
        }

        public void BuildTree()
        {
            TreeFormBaseClass m_tree_Terrain = new TreeFormBaseClass();
            m_tree_Terrain.SourceDataArray = "MCTester.Managers.MapWorld.Manager_MCTerrain";
            m_tree_Terrain.ClassHandles = typeof(IDNMcMapTerrain);
            m_tree_Terrain.HandlerPanelType.Add(typeof(IDNMcMapTerrain), "MCTester.MapWorld.MapUserControls.ucTerrain");

            TreeFormBaseClass m_tree_Layers = new TreeFormBaseClass();
            m_tree_Layers.SourceDataArray = "MCTester.Managers.MapWorld.Manager_MCLayers";
            m_tree_Layers.ClassHandles = typeof(IDNMcMapLayer);

            //for background thread index m_tree_Layers.HandlerPanelType.Add(typeof(object), "MCTester.MapWorld.MapUserControls.ucRoot");
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
          //  m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeMaterialMapLayer), "MCTester.MapWorld.MapUserControls.ucCodeLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeTraversabilityMapLayer), "MCTester.MapWorld.MapUserControls.ucTraversabilityMapLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServerTraversabilityMapLayer), "MCTester.MapWorld.MapUserControls.ucTraversabilityMapLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeMaterialMapLayer), "MCTester.MapWorld.MapUserControls.ucMaterialMapLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServerMaterialMapLayer), "MCTester.MapWorld.MapUserControls.ucMaterialMapLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcWebServiceRasterMapLayer), "MCTester.MapWorld.MapUserControls.ucRasterLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcWebServiceDtmMapLayer), "MCTester.MapWorld.MapUserControls.ucDTMLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcStaticObjectsMapLayer), "MCTester.MapWorld.MapUserControls.ucStaticObjectsLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNative3DModelMapLayer), "MCTester.MapWorld.MapUserControls.ucNative3DModelLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServer3DModelMapLayer), "MCTester.MapWorld.MapUserControls.ucNativeServer3DModelLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcRaw3DModelMapLayer), "MCTester.MapWorld.MapUserControls.ucRaw3DModelLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeVector3DExtrusionMapLayer), "MCTester.MapWorld.MapUserControls.ucNativeVector3DExtrusionLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServerVector3DExtrusionMapLayer), "MCTester.MapWorld.MapUserControls.ucNativeServerVector3DExtrusionLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcRawVector3DExtrusionMapLayer), "MCTester.MapWorld.MapUserControls.ucRawVector3DExtrusionLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServerDtmMapLayer), "MCTester.MapWorld.MapUserControls.ucNativeServerDTMLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServerRasterMapLayer), "MCTester.MapWorld.MapUserControls.ucNativeServerRasterLayer");
            m_tree_Layers.HandlerPanelType.Add(typeof(IDNMcNativeServerVectorMapLayer), "MCTester.MapWorld.MapUserControls.ucNativeServerVectorLayer");

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
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeTraversabilityMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeServerTraversabilityMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeServerMaterialMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcRaw3DModelMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNative3DModelMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeServer3DModelMapLayer), m_tree_Layers);  
            m_tree_Terrain.Children.Add(typeof(IDNMcRawVector3DExtrusionMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeVector3DExtrusionMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeServerVector3DExtrusionMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcWebServiceRasterMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcWebServiceDtmMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcStaticObjectsMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeServerDtmMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeServerRasterMapLayer), m_tree_Layers);
            m_tree_Terrain.Children.Add(typeof(IDNMcNativeServerVectorMapLayer), m_tree_Layers);

            base.CreateTree();

            this.TreeDefinitionClass = m_tree_Terrain;
            HandleFirstTreeElement(TreeDefinitionClass, m_TreeView.Nodes[0]);

            this.TreeDefinitionClass = m_tree_Layers;
            HandleFirstTreeElement(TreeDefinitionClass, m_TreeView.Nodes[0]);

            // turn on all viewports render needed flags
            Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        protected override void m_TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            base.m_TreeView_NodeMouseClick(sender, e);

            if (e.Button == MouseButtons.Right)
            {
                string interfaceName = GeneralFuncs.GetDirectInterfaceName(m_NodeClickedType.GetType());

                switch (interfaceName)
                {
                    case "IDNMcMapTerrain":
                        cmsTerrainOptions.Show(m_TreeView, e.Location);
                        break;
                    case "IDNMcMapLayer":
                    case "IDNMcNativeDtmMapLayer":
                    case "IDNMcNativeRasterMapLayer":
                    case "IDNMcNativeVectorMapLayer":
                    case "IDNMcNativeHeatMapLayer":
                    case "IDNMcNativeMaterialMapLayer":
                    case "IDNMcNativeTraversabilityMapLayer":
                    case "IDNMcRaw3DModelMapLayer":
                    case "IDNMcNative3DModelMapLayer":  
                    case "IDNMcNativeServer3DModelMapLayer":
                    case "IDNMcNativeVector3DExtrusionMapLayer":
                    case "IDNMcRawVector3DExtrusionMapLayer":
                    case "IDNMcNativeServerVector3DExtrusionMapLayer":
                    case "IDNMcRawDtmMapLayer":
                    case "IDNMcRawRasterMapLayer":
                    case "IDNMcRawVectorMapLayer":
                    case "IDNMcDtmMapLayer":
                    case "IDNMcRasterMapLayer":
                    case "IDNMcWebServiceRasterMapLayer":
                    case "IDNMcWebServiceDtmMapLayer":
                    case "IDNMcStaticObjectsMapLayer":
                    case "IDNMcNativeServerDtmMapLayer":
                    case "IDNMcNativeServerRasterMapLayer":
                    case "IDNMcNativeServerVectorMapLayer":
                    case "IDNMcNativeServerStaticObjectsMapLayer":
                    case "IDNMcNativeServerMaterialMapLayer":
                        cmsLayerOptions.Show(m_TreeView, e.Location);
                        break;
                    default:
                        break;
                }
            }
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

        private void miCreateNewTerrain_Click(object sender, EventArgs e)
        {
            TerrainWizzardForm TerrainWiz = new TerrainWizzardForm();
            Manager_MCLayers.AddTerrainWizzardFormToList(TerrainWiz);
            if (TerrainWiz.ShowDialog(this) == DialogResult.OK)
                BuildTree();

            Manager_MCLayers.RemoveTerrainWizzardFormFromList(TerrainWiz);
        }

        private void m_TreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "Root")
            {
                BuildTree();
            }
        }

        private void MCTerrainLayerTreeViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.BtnTerrainLayersCheckedState = false;

            Manager_MCOverlay.RemoveAllOverlaysFromVectorItemsOverlay();
        }

        private void miAddAndRemoveVectorLayers_Click(object sender, EventArgs e)
        {
            AddAndRemoveVectorLayersForm addAndRemoveVectorLayersForm = new MCTester.MapWorld.WizardForms.AddAndRemoveVectorLayersForm((IDNMcMapTerrain)SelectedNode);
            addAndRemoveVectorLayersForm.Show();
        }

        private void miRename_Click(object sender, EventArgs e)
        {
            Rename();
        }

        private void miDeleteName_Click(object sender, EventArgs e)
        {
            DeleteName();
        }

        private void miRenameLayer_Click(object sender, EventArgs e)
        {
            Rename();
        }

        private void miDeleteLayer_Click(object sender, EventArgs e)
        {
            DeleteName();
        }

        private void removeTerrainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manager_MCTerrain.RemoveTerrain((IDNMcMapTerrain)SelectedNode);
            BuildTree();
        }

        private void removeLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDNMcMapTerrain currTerrain = null;
            if (m_TreeView.SelectedNode.Parent != null && m_TreeView.SelectedNode.Parent.Tag != null)
                currTerrain = (IDNMcMapTerrain)((TreeFormBaseClass)m_TreeView.SelectedNode.Parent.Tag).TemporaryObject;
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

        private void miBackgroungThreadIndex_Click(object sender, EventArgs e)
        {
            frmBackgroundThreadIndex frm = new frmBackgroundThreadIndex();
            frm.Show();
        }

        private void MCTerrainLayerTreeViewForm_Load(object sender, EventArgs e)
        {

        }

        private void cmsLayerOptions_Opening(object sender, CancelEventArgs e)
        {

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

        private void miMoveToCenterLayer_Click(object sender, EventArgs e)
        {
            IDNMcMapLayer currLayer = (IDNMcMapLayer)SelectedNode;
            try
            {
                if (currLayer.BoundingBox != null)
                {
                    DNSMcVector3D centerPoint = currLayer.BoundingBox.CenterPoint();
                    MCTAsyncQueryCallback.MoveToCenterPoint(centerPoint, MCTMapFormManager.MapForm.Viewport);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("BoundingBox", McEx);
            }
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
    }
}
