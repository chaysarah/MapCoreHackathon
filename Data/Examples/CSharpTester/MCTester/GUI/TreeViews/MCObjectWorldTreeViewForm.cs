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
using MCTester.ObjectWorld.ObjectsUserControls;
using MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms;
using MCTester.Managers.ObjectWorld;
using MCTester.General_Forms;
using MCTester.Managers.MapWorld;
using MCTester.Managers;
using MCTester.ObjectWorld.Assit_Forms;
using MapCore.Common;
using System.IO;
using MCTester.GUI.Map;

namespace MCTester.GUI.Trees
{
    public partial class MCObjectWorldTreeViewForm : TreeViewDisplayForm
    {
        private MCTester.GUI.Forms.ObjectPropertiesForm m_ObjectPropertiesForm;
        private List<IDNMcObjectSchemeNode> m_DisconnectedNodes = new List<IDNMcObjectSchemeNode>();
        private IDNMcObject m_ShowCoordObj;
        private IDNMcOverlayManager m_SelectedNodeOM;
        private IDNMcOverlay m_TmpOverlay;
        private IDNMcObject m_CurrObjectOfShowCoord;
        private IDNMcObjectScheme m_CurrSchemeOfShowCoord;
        private IDNMcObjectSchemeNode m_CurrNode = null;
        private FindItemType m_FindItemType = FindItemType.Object;
        private List<object> lstObj = new List<object>();

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = false;
            base.OnClosing(e);
        }

        public MCObjectWorldTreeViewForm()
            : base()
        {
            InitializeComponent();
            this.cmsSchemeOptions.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            this.cmsObjectLocationOption.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            this.cmsOverlayManagerOptions.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            this.cmsOverlayOptions.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            this.cmsPhysicalItemOption.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            this.cmsSchemeOptions.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            this.cmsOMRoot.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripWithoutNames_Opening);
            this.cmsSymbolicNodeOptions.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);

            this.cmsCollectionOptions.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripWithoutExpand_Opening);
            this.cmsObjectOptions.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripWithoutExpand_Opening);
            this.cmsConditionalSelectorOptions.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripWithoutExpand_Opening);
            this.cmsLocationConditionalSelectorOptions.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripWithoutExpand_Opening);

            TreeViewFormCommands.OnRefreshRequired += new OnRefreshRequiredEventArgs(TreeViewFormCommands_OnRefreshRequired);
            
            BuildTree();

            m_TreeView.AllowDrop = true;
            m_TreeView.ItemDrag += new ItemDragEventHandler(m_TreeView_ItemDrag);
            m_TreeView.DragEnter += new DragEventHandler(m_TreeView_DragEnter);
            m_TreeView.DragOver += new DragEventHandler(m_TreeView_DragOver);
            m_TreeView.DragDrop += new DragEventHandler(m_TreeView_DragDrop);

        }

        private void m_TreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Move the dragged node when the left mouse button is used.
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        // Set the target drop effect to the effect 
        // specified in the ItemDrag event handler.
        private void m_TreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        // Select the node under the mouse pointer to indicate the 
        // expected drop location.
        private void m_TreeView_DragOver(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the mouse position.
            Point targetPoint = m_TreeView.PointToClient(new Point(e.X, e.Y));

            // Select the node at the mouse position.
            m_TreeView.SelectedNode = m_TreeView.GetNodeAt(targetPoint);

        }

        private void m_TreeView_DragDrop(object sender, DragEventArgs e)
        {

            // Retrieve the client coordinates of the drop location.
            Point targetPoint = m_TreeView.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNode targetNode = m_TreeView.GetNodeAt(targetPoint);

            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // Confirm that the node at the drop location is not 
            // the dragged node or a descendant of the dragged node.
            if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
            {
                object mcObjDraggedNode = GetMcObjFromNode(draggedNode);
                object mcObjTargetNode = GetMcObjFromNode(targetNode);

                // connect item
                if (mcObjDraggedNode is IDNMcObjectSchemeItem && (mcObjTargetNode is IDNMcObjectSchemeItem || mcObjTargetNode is IDNMcObjectLocation))
                {
                    string txtMsg = string.Format("Do You Want To Connect \nItem {0} \nTo Item {1}?",
                        Manager_MCNames.GetNameByObject(mcObjDraggedNode),
                        Manager_MCNames.GetNameByObject(mcObjTargetNode));

                    if (MessageBox.Show(txtMsg, "Connect Operation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            if (mcObjDraggedNode is IDNMcSymbolicItem)
                                ((IDNMcSymbolicItem)mcObjDraggedNode).Connect((IDNMcObjectSchemeNode)mcObjTargetNode);
                            if (mcObjDraggedNode is IDNMcPhysicalItem)
                                ((IDNMcPhysicalItem)mcObjDraggedNode).Connect((IDNMcObjectSchemeNode)mcObjTargetNode);

                            Managers.ObjectWorld.Manager_MCObjectSchemeItem.RemoveItem((IDNMcObjectSchemeItem)mcObjDraggedNode);
                            // If it is a move operation, remove the node from its current 
                            // location and add it to the node at the drop location.
                            if (e.Effect == DragDropEffects.Move)
                            {
                                draggedNode.Remove();
                                // if exist dummy node - remove it
                                if (targetNode.Nodes != null && targetNode.Nodes.Count > 0)
                                {
                                    TreeNode temp = targetNode.Nodes[0];
                                    if (temp is DummyNode)
                                        temp.Remove();
                                }
                                // do connect
                                targetNode.Nodes.Add(draggedNode);
                            }

                            // If it is a copy operation, clone the dragged node 
                            // and add it to the node at the drop location.
                            //else if (e.Effect == DragDropEffects.Copy)
                            //{
                            //    targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
                            //}

                            // Expand the node at the location 
                            // to show the dropped node.
                            targetNode.Expand();
                        }
                        catch (MapCoreException McEx)
                        {
                            this.DialogResult = DialogResult.Cancel;
                            MapCore.Common.Utilities.ShowErrorMessage("m_SymbolicItem.Connect", McEx);
                        }
                    }
                }
                else
                    MessageBox.Show("Invalid Operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
            }
        }

        // Determine whether one node is a parent 
        // or ancestor of a second node.
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            // Check the parent node of the second node.
            if (node2 == null) return false;
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node, 
            // call the ContainsNode method recursively using the parent of 
            // the second node.
            return ContainsNode(node1, node2.Parent);
        }



        

       

        void TreeViewFormCommands_OnRefreshRequired()
        {
            RefreshTree();
            TreeNode tn = base.m_TreeView.SelectedNode;
            if (tn != null)
            {
                ReSelectTreeNode(tn, m_TreeView.Nodes[0] /*root Node*/);
            }
        }

        public void BuildTree()
        {
            TreeNode tn = base.m_TreeView.SelectedNode;

            TreeFormBaseClass m_tree_OM = new TreeFormBaseClass();
            m_tree_OM.SourceDataArray = "MCTester.Managers.ObjectWorld.Manager_MCOverlayManager";
            m_tree_OM.ClassHandles = typeof(IDNMcOverlayManager);
            m_tree_OM.HandlerPanelType.Add(typeof(IDNMcOverlayManager), "MCTester.ObjectWorld.OverlayManagerWorld.ucOverlayManager");

            TreeFormBaseClass m_tree_Overlay = new TreeFormBaseClass();
            m_tree_Overlay.SourceDataArray = "MCTester.Managers.ObjectWorld.Manager_MCOverlay";
            m_tree_Overlay.ClassHandles = typeof(IDNMcOverlay);
            m_tree_Overlay.HandlerPanelType.Add(typeof(IDNMcOverlay), "MCTester.ObjectWorld.OverlayManagerWorld.ucOverlay");

            TreeFormBaseClass m_tree_Collection = new TreeFormBaseClass();
            m_tree_Collection.SourceDataArray = "MCTester.Managers.ObjectWorld.Manager_MCCollection";
            m_tree_Collection.ClassHandles = typeof(IDNMcCollection);
            m_tree_Collection.HandlerPanelType.Add(typeof(IDNMcCollection), "MCTester.ObjectWorld.OverlayManagerWorld.ucCollection");

            TreeFormBaseClass m_tree_Object = new TreeFormBaseClass();
            m_tree_Object.SourceDataArray = "MCTester.Managers.ObjectWorld.Manager_MCObject";
            m_tree_Object.ClassHandles = typeof(IDNMcObject);
            m_tree_Object.HandlerPanelType.Add(typeof(IDNMcObject), "MCTester.ObjectWorld.OverlayManagerWorld.ucObject");

            TreeFormBaseClass m_tree_VectorObject = new TreeFormBaseClass();
            m_tree_VectorObject.SourceDataArray = "MCTester.Managers.VectorialWorld.Manager_MCVectorial";
            m_tree_VectorObject.ClassHandles = typeof(IDNMcObject);
            m_tree_VectorObject.HandlerPanelType.Add(typeof(IDNMcObject), "MCTester.ObjectWorld.OverlayManagerWorld.ucObject");

            TreeFormBaseClass m_tree_ObjectScheme = new TreeFormBaseClass();
            m_tree_ObjectScheme.SourceDataArray = "MCTester.Managers.ObjectWorld.Manager_MCObjectScheme";
            m_tree_ObjectScheme.ClassHandles = typeof(IDNMcObjectScheme);
            m_tree_ObjectScheme.HandlerPanelType.Add(typeof(IDNMcObjectScheme), "MCTester.ObjectWorld.ObjectsUserControls.ucObjectScheme");

            TreeFormBaseClass m_tree_ObjectLocation = new TreeFormBaseClass();
            m_tree_ObjectLocation.SourceDataArray = "MCTester.Managers.ObjectWorld.Manager_MCObjectLocation";
            m_tree_ObjectLocation.ClassHandles = typeof(IDNMcObjectLocation);
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcObjectLocation), "MCTester.ObjectWorld.ObjectsUserControls.CtrlObjectLocation");

            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcObjectSchemeNode), "MCTester.ObjectWorld.ObjectsUserControls.ucObjectSchemeNode");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcSymbolicItem), "MCTester.ObjectWorld.ObjectsUserControls.ucSymbolicItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcEmptySymbolicItem), "MCTester.ObjectWorld.ObjectsUserControls.ucEmptySymbolicItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcLineBasedItem), "MCTester.ObjectWorld.ObjectsUserControls.ucLineBasedItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcPictureItem), "MCTester.ObjectWorld.ObjectsUserControls.ucPictureItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcTextItem), "MCTester.ObjectWorld.ObjectsUserControls.ucTextItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcArcItem), "MCTester.ObjectWorld.ObjectsUserControls.ucArcItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcClosedShapeItem), "MCTester.ObjectWorld.ObjectsUserControls.ucClosedShapeItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcLineItem), "MCTester.ObjectWorld.ObjectsUserControls.ucLineItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcLineExpansionItem), "MCTester.ObjectWorld.ObjectsUserControls.ucLineExpansionItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcArrowItem), "MCTester.ObjectWorld.ObjectsUserControls.ucArrowItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcEllipseItem), "MCTester.ObjectWorld.ObjectsUserControls.ucEllipseItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcPolygonItem), "MCTester.ObjectWorld.ObjectsUserControls.ucPolygonItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcRectangleItem), "MCTester.ObjectWorld.ObjectsUserControls.ucRectangleItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcPhysicalItem), "MCTester.ObjectWorld.ObjectsUserControls.ucPhysicalItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcEmptyPhysicalItem), "MCTester.ObjectWorld.ObjectsUserControls.ucPhysicalItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcMeshItem), "MCTester.ObjectWorld.ObjectsUserControls.ucMeshItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcParticleEffectItem), "MCTester.ObjectWorld.ObjectsUserControls.ucParticleEffectItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcDirectionalLightItem), "MCTester.ObjectWorld.ObjectsUserControls.ucDirectionalLightItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcPointLightItem), "MCTester.ObjectWorld.ObjectsUserControls.ucPointLightItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcSpotLightItem), "MCTester.ObjectWorld.ObjectsUserControls.ucSpotLightItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcSoundItem), "MCTester.ObjectWorld.ObjectsUserControls.ucSoundItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcProjectorItem), "MCTester.ObjectWorld.ObjectsUserControls.ucProjectorItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcProceduralGeometryItem), "MCTester.ObjectWorld.ObjectsUserControls.ucProceduralGeometryItem");
            m_tree_ObjectLocation.HandlerPanelType.Add(typeof(IDNMcManualGeometryItem), "MCTester.ObjectWorld.ObjectsUserControls.ucManualGeometryItem");

            TreeFormBaseClass m_tree_ObjectSchemeItem = new TreeFormBaseClass();
            m_tree_ObjectSchemeItem.SourceDataArray = "MCTester.Managers.ObjectWorld.Manager_MCObjectSchemeItem";
            m_tree_ObjectSchemeItem.ClassHandles = typeof(IDNMcObjectSchemeItem);
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcObjectSchemeItem), "MCTester.ObjectWorld.ObjectsUserControls.CtrlObjectSchemeItem");

            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcSymbolicItem), "MCTester.ObjectWorld.ObjectsUserControls.ucSymbolicItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcEmptySymbolicItem), "MCTester.ObjectWorld.ObjectsUserControls.ucEmptySymbolicItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcLineBasedItem), "MCTester.ObjectWorld.ObjectsUserControls.ucLineBasedItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcPictureItem), "MCTester.ObjectWorld.ObjectsUserControls.ucPictureItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcTextItem), "MCTester.ObjectWorld.ObjectsUserControls.ucTextItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcArcItem), "MCTester.ObjectWorld.ObjectsUserControls.ucArcItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcClosedShapeItem), "MCTester.ObjectWorld.ObjectsUserControls.ucClosedShapeItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcLineItem), "MCTester.ObjectWorld.ObjectsUserControls.ucLineItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcLineExpansionItem), "MCTester.ObjectWorld.ObjectsUserControls.ucLineExpansionItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcArrowItem), "MCTester.ObjectWorld.ObjectsUserControls.ucArrowItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcEllipseItem), "MCTester.ObjectWorld.ObjectsUserControls.ucEllipseItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcPolygonItem), "MCTester.ObjectWorld.ObjectsUserControls.ucPolygonItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcRectangleItem), "MCTester.ObjectWorld.ObjectsUserControls.ucRectangleItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcPhysicalItem), "MCTester.ObjectWorld.ObjectsUserControls.ucPhysicalItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcEmptyPhysicalItem), "MCTester.ObjectWorld.ObjectsUserControls.ucPhysicalItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcMeshItem), "MCTester.ObjectWorld.ObjectsUserControls.ucMeshItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcParticleEffectItem), "MCTester.ObjectWorld.ObjectsUserControls.ucParticleEffectItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcDirectionalLightItem), "MCTester.ObjectWorld.ObjectsUserControls.ucDirectionalLightItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcPointLightItem), "MCTester.ObjectWorld.ObjectsUserControls.ucPointLightItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcSpotLightItem), "MCTester.ObjectWorld.ObjectsUserControls.ucSpotLightItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcSoundItem), "MCTester.ObjectWorld.ObjectsUserControls.ucSoundItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcProjectorItem), "MCTester.ObjectWorld.ObjectsUserControls.ucProjectorItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcProceduralGeometryItem), "MCTester.ObjectWorld.ObjectsUserControls.ucProceduralGeometryItem");
            m_tree_ObjectSchemeItem.HandlerPanelType.Add(typeof(IDNMcManualGeometryItem), "MCTester.ObjectWorld.ObjectsUserControls.ucManualGeometryItem");

            TreeFormBaseClass m_tree_ConditionalSelector = new TreeFormBaseClass();
            m_tree_ConditionalSelector.SourceDataArray = "MCTester.Managers.ObjectWorld.Manager_MCConditionalSelector";
            m_tree_ConditionalSelector.ClassHandles = typeof(IDNMcConditionalSelector);

            m_tree_ConditionalSelector.HandlerPanelType.Add(typeof(IDNMcBlockedConditionalSelector), "MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms.ucBlockedConditionalSelector");
            m_tree_ConditionalSelector.HandlerPanelType.Add(typeof(IDNMcBooleanConditionalSelector), "MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms.ucBooleanConditionalSelector");
            m_tree_ConditionalSelector.HandlerPanelType.Add(typeof(IDNMcScaleConditionalSelector), "MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms.ucScaleConditionalSelector");
            m_tree_ConditionalSelector.HandlerPanelType.Add(typeof(IDNMcObjectStateConditionalSelector), "MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms.ucObjectStateConditionalSelector");
            m_tree_ConditionalSelector.HandlerPanelType.Add(typeof(IDNMcViewportConditionalSelector), "MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms.ucViewportConditionalSelector");
            m_tree_ConditionalSelector.HandlerPanelType.Add(typeof(IDNMcLocationConditionalSelector), "MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms.ucLocationConditionalSelector");


            //Tree hierarchy Definition
            m_tree_OM.Children.Add(typeof(IDNMcOverlay), m_tree_Overlay);
            m_tree_OM.Children.Add(typeof(IDNMcCollection), m_tree_Collection);

            m_tree_OM.Children.Add(typeof(IDNMcConditionalSelector), m_tree_ConditionalSelector);
            m_tree_OM.Children.Add(typeof(IDNMcBlockedConditionalSelector), m_tree_ConditionalSelector);
            m_tree_OM.Children.Add(typeof(IDNMcBooleanConditionalSelector), m_tree_ConditionalSelector);
            m_tree_OM.Children.Add(typeof(IDNMcScaleConditionalSelector), m_tree_ConditionalSelector);
            m_tree_OM.Children.Add(typeof(IDNMcObjectStateConditionalSelector), m_tree_ConditionalSelector);
            m_tree_OM.Children.Add(typeof(IDNMcViewportConditionalSelector), m_tree_ConditionalSelector);
            m_tree_OM.Children.Add(typeof(IDNMcLocationConditionalSelector), m_tree_ConditionalSelector);

            m_tree_OM.Children.Add(typeof(IDNMcObjectScheme), m_tree_ObjectScheme);
            m_tree_OM.Children.Add(typeof(IDNMcObject), m_tree_VectorObject);
            m_tree_Overlay.Children.Add(typeof(IDNMcObject), m_tree_Object);
            m_tree_ObjectScheme.Children.Add(typeof(IDNMcObjectLocation), m_tree_ObjectLocation);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcObjectSchemeItem), m_tree_ObjectSchemeItem);

            m_tree_ObjectLocation.Children.Add(typeof(IDNMcEmptySymbolicItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcLineBasedItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcPictureItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcTextItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcArcItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcClosedShapeItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcLineItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcLineExpansionItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcArrowItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcEllipseItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcPolygonItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcRectangleItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcMeshItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcParticleEffectItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcEmptyPhysicalItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcDirectionalLightItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcPointLightItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcSpotLightItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcSoundItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcProjectorItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcProceduralGeometryItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectLocation.Children.Add(typeof(IDNMcManualGeometryItem), m_tree_ObjectSchemeItem);

            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcObjectSchemeItem), m_tree_ObjectSchemeItem);

            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcSymbolicItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcEmptySymbolicItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcLineBasedItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcPictureItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcTextItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcArcItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcClosedShapeItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcLineItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcLineExpansionItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcArrowItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcEllipseItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcPolygonItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcRectangleItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcPhysicalItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcEmptyPhysicalItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcMeshItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcParticleEffectItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcDirectionalLightItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcPointLightItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcSpotLightItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcSoundItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcProjectorItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcProceduralGeometryItem), m_tree_ObjectSchemeItem);
            m_tree_ObjectSchemeItem.Children.Add(typeof(IDNMcManualGeometryItem), m_tree_ObjectSchemeItem);

            base.CreateTree();

            //Create the overlay manager tree
            this.TreeDefinitionClass = m_tree_OM;
            HandleFirstTreeElement(TreeDefinitionClass, m_TreeView.Nodes[0]);

            //Create standalone items tree
            this.TreeDefinitionClass = m_tree_ObjectSchemeItem;
            HandleFirstTreeElement(TreeDefinitionClass, m_TreeView.Nodes[0]);


            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        public void RefreshTree(bool isSelectLastNode = false, int indexTabToSelect = -1)
        {
            TreeNode node = GetSelectedNode();
            BuildTree();
            base.RefreshTree(node, isSelectLastNode, indexTabToSelect);
        }


        private void MCOverlayMangerTreeViewForm_Load(object sender, EventArgs e)
        {
            ExpendTree();
        }

        protected override void m_TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            bool isShowPoints = false;
           
            if (e.Button == MouseButtons.Left)
                MCTPrivatePropertiesData.RemoveAllPrivatePropertyControlsData();
            object Node_TemporaryObject = null;
            if (e.Node.Tag != null)
            {
                TreeFormBaseClass nodeBaseInfo = (TreeFormBaseClass)e.Node.Tag;
                if (nodeBaseInfo != null && nodeBaseInfo.TemporaryObject != null)
                {
                    Node_TemporaryObject = nodeBaseInfo.TemporaryObject;

                    if (nodeBaseInfo.TemporaryObject is IDNMcObjectSchemeNode)
                    {
                        IDNMcObjectSchemeNode node = (IDNMcObjectSchemeNode)nodeBaseInfo.TemporaryObject;

                        try
                        {
                            Manager_MCObjectScheme.CurrentScheme = node.GetScheme();
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("create icons for node calculate location points", McEx);
                        }
                    }
                    else if (!(nodeBaseInfo.TemporaryObject is IDNMcObjectScheme))
                        m_CurrSchemeOfShowCoord = null;
                }
            }

            base.m_TreeView_NodeMouseClick(sender, e);

            if (e.Node.Tag != null)
            {
                string typeName = GeneralFuncs.GetDirectInterfaceName(m_NodeClickedType.GetType());
                if (e.Button == MouseButtons.Right)
                {
                    switch (typeName)
                    {
                        case "IDNMcEllipseItem":
                        case "IDNMcArcItem":
                        case "IDNMcLineItem":
                        case "IDNMcLineExpansionItem":
                        case "IDNMcArrowItem":
                        case "IDNMcRectangleItem":
                        case "IDNMcPolygonItem":
                        case "IDNMcTextItem":
                        case "IDNMcPictureItem":
                        case "IDNMcEmptySymbolicItem":
                        case "IDNMcManualGeometryItem":
                            cmsSymbolicNodeOptions.Show(m_TreeView, e.Location);
                            break;
                        case "IDNMcMeshItem":
                        case "IDNMcEmptyPhysicalItem":
                        case "IDNMcParticleEffectItem":
                        case "IDNMcDirectionalLightItem":
                        case "IDNMcPointLightItem":
                        case "IDNMcSpotLightItem":
                        case "IDNMcSoundItem":
                        case "IDNMcProjectorItem":
                            cmsPhysicalItemOption.Show(m_TreeView, e.Location);
                            break;
                        case "IDNMcObjectScheme":
                            BuildOneObjectListOfScheme();
                            cmsSchemeOptions.Show(m_TreeView, e.Location);
                            //treeViewContextMenuStrip1.Show(m_TreeView, e.Location);
                            break;
                        case "IDNMcObject":
                            BuildObjectLocationListOfObject();
                            cmsObjectOptions.Show(m_TreeView, e.Location);
                            break;
                        case "IDNMcOverlay":
                            cmsOverlayOptions.Show(m_TreeView, e.Location);
                            break;
                        case "IDNMcOverlayManager":
                            cmsOverlayManagerOptions.Show(m_TreeView, e.Location);
                            break;
                        case "IDNMcObjectLocation":
                            cmsObjectLocationOption.Show(m_TreeView, e.Location);
                            break;
                        case "IDNMcBlockedConditionalSelector":
                        case "IDNMcBooleanConditionalSelector":
                        case "IDNMcScaleConditionalSelector":
                        case "IDNMcObjectStateConditionalSelector":
                        case "IDNMcViewportConditionalSelector":
                            cmsConditionalSelectorOptions.Show(m_TreeView, e.Location);
                            break;
                        case "IDNMcLocationConditionalSelector":
                            cmsLocationConditionalSelectorOptions.Show(m_TreeView, e.Location);
                            break;
                        case "IDNMcCollection":
                            cmsCollectionOptions.Show(m_TreeView, e.Location);
                            break;
                        default:
                            break;
                    }
                }
               

                if (e.Button == MouseButtons.Left)
                {
                  
                    m_CurrNode = null;
                   
                    switch (typeName)
                    {
                        case "IDNMcEllipseItem":
                        case "IDNMcArcItem":
                        case "IDNMcLineItem":
                        case "IDNMcLineExpansionItem":
                        case "IDNMcArrowItem":
                        case "IDNMcRectangleItem":
                        case "IDNMcPolygonItem":
                        case "IDNMcTextItem":
                        case "IDNMcPictureItem":
                        case "IDNMcEmptySymbolicItem":
                        case "IDNMcMeshItem":
                        case "IDNMcEmptyPhysicalItem":
                        case "IDNMcParticleEffectItem":
                        case "IDNMcDirectionalLightItem":
                        case "IDNMcPointLightItem":
                        case "IDNMcSpotLightItem":
                        case "IDNMcSoundItem":
                        case "IDNMcProjectorItem":
                        case "IDNMcManualGeometryItem":
                        case "IDNMcObjectLocation":
                            isShowPoints = true;
                            m_CurrNode = (IDNMcObjectSchemeNode)base.m_NodeClickedType;
                            if (m_CurrNode.GetScheme() == null || m_CurrNode.GetScheme() != m_CurrSchemeOfShowCoord)
                                m_CurrObjectOfShowCoord = null;
                            break;
                        case "IDNMcObject":
                            {
                                isShowPoints = true;
                                if (Node_TemporaryObject is IDNMcObject)
                                {
                                    m_CurrObjectOfShowCoord = (IDNMcObject)Node_TemporaryObject;
                                    Manager_MCObjectScheme.CurrentScheme = m_CurrObjectOfShowCoord.GetScheme();
                                }
                            }
                            break;
                        case "IDNMcObjectScheme":
                            isShowPoints = true;
                            Manager_MCObjectScheme.CurrentScheme = (IDNMcObjectScheme)base.m_NodeClickedType;
                            if (Manager_MCObjectScheme.CurrentScheme != m_CurrSchemeOfShowCoord)
                                m_CurrObjectOfShowCoord = null;
                            break;

                    }
                    if (isShowPoints)
                    {
                        try
                        {
                            ShowNodeCalcCoord();
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("create icons for node calculate location points", McEx);
                        }
                    }
                    else
                    {
                        m_CurrObjectOfShowCoord = null;
                        m_CurrSchemeOfShowCoord = null;
                        Manager_MCObjectScheme.CurrentScheme = null;
                        UnshowNodeCalcCoord(true);
                    }
                   
                }
            }
            else if (e.Button == MouseButtons.Right && e.Node.Text == "Root")
            {
                cmsOMRoot.Show(m_TreeView, e.Location);
            }
        }

        private void ShowNodeCalcCoord()
        {
            try
            {
                IDNMcObjectScheme currentScheme = Manager_MCObjectScheme.CurrentScheme;
                if ((currentScheme != null || m_CurrNode != null) &&
                   (tsmiOnBasePoints.Checked || tsmiOnCalculatesAndAddedPoints.Checked || tsmiOnCalculatedPoints.Checked))
                {
                    UnshowNodeCalcCoord();
                    if (currentScheme == null)
                        currentScheme = m_CurrNode.GetScheme();
                    if (currentScheme != null && currentScheme.GetObjects().Length > 0)
                    {
                        if (m_CurrObjectOfShowCoord == null || (m_CurrObjectOfShowCoord.GetScheme() != currentScheme && m_CurrSchemeOfShowCoord == null))
                        {
                            m_CurrSchemeOfShowCoord = currentScheme;
                            IDNMcObject[] objs = currentScheme.GetObjects();
                            if (objs != null && objs.Length > 0)
                            {
                                if (objs.Length == 1)
                                {
                                    m_CurrObjectOfShowCoord = objs[0];
                                }
                                else
                                {
                                    frmObjectListBasedOnSpecificSchemeNode ObjectListBasedOnSpecificSchemeNodeForm = null;
                                    if (currentScheme != null)
                                        ObjectListBasedOnSpecificSchemeNodeForm = new frmObjectListBasedOnSpecificSchemeNode(currentScheme);
                                    else if (m_CurrNode != null)
                                        ObjectListBasedOnSpecificSchemeNodeForm = new frmObjectListBasedOnSpecificSchemeNode(m_CurrNode);

                                    if (ObjectListBasedOnSpecificSchemeNodeForm.ShowDialog() == DialogResult.OK)
                                    {
                                        m_CurrObjectOfShowCoord = ObjectListBasedOnSpecificSchemeNodeForm.SelectedObject;
                                    }
                                }
                            }
                        }

                        if (m_CurrObjectOfShowCoord != null)
                        {
                            MCTShowPointsLocations[] arrShowPointsLocations = null;

                            DNSMcVector3D[] paPoints = new DNSMcVector3D[0];
                            DNEMcPointCoordSystem pointCoordSystem = DNEMcPointCoordSystem._EPCS_WORLD;
                            DNSMcVector3D[] locations = null;

                            if ((tsmiOnCalculatesAndAddedPoints.Checked || tsmiOnCalculatedPoints.Checked) && m_CurrNode != null && m_CurrNode is IDNMcSymbolicItem)
                            {
                                
                                if (tsmiOnCalculatesAndAddedPoints.Checked)
                                {
                                    (m_CurrNode as IDNMcSymbolicItem).GetAllCalculatedPoints(MCTMapFormManager.MapForm.Viewport, m_CurrObjectOfShowCoord, out paPoints, out pointCoordSystem);
                                    MCTShowPointsLocations objShowPointsLocations = new MCTShowPointsLocations();
                                    objShowPointsLocations.mcPointCoordSystem = pointCoordSystem;
                                    objShowPointsLocations.mcPoints = paPoints;
                                    arrShowPointsLocations = new MCTShowPointsLocations[1] { objShowPointsLocations };
                                }
                                else if (tsmiOnCalculatedPoints.Checked)
                                {
                                    uint[] pauOriginalPointsIndices;
                                    (m_CurrNode as IDNMcSymbolicItem).GetAllCalculatedPoints(MCTMapFormManager.MapForm.Viewport, m_CurrObjectOfShowCoord, out paPoints, out pointCoordSystem, out pauOriginalPointsIndices);
                                    if(paPoints != null)
                                    {
                                        if (pauOriginalPointsIndices == null)
                                            arrShowPointsLocations = new MCTShowPointsLocations[1];
                                        else
                                            arrShowPointsLocations = new MCTShowPointsLocations[2];

                                        MCTShowPointsLocations objShowPointsLocations = new MCTShowPointsLocations();
                                        objShowPointsLocations.mcPointCoordSystem = pointCoordSystem;
                                        objShowPointsLocations.mcPoints = paPoints;
                                        arrShowPointsLocations[0] = objShowPointsLocations;

                                        if (pauOriginalPointsIndices != null)
                                        {
                                            locations = new DNSMcVector3D[pauOriginalPointsIndices.Length];
                                            for (int i = 0; i < pauOriginalPointsIndices.Length; i++)
                                            {
                                                locations[i] = paPoints[pauOriginalPointsIndices[i]];
                                            }
                                            MCTShowPointsLocations objShowPointsLocationsIndices = new MCTShowPointsLocations();
                                            objShowPointsLocationsIndices.mcPointCoordSystem = pointCoordSystem;
                                            objShowPointsLocationsIndices.mcPoints = locations;
                                            arrShowPointsLocations[1] = objShowPointsLocationsIndices;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                IDNMcObjectSchemeNode[] nodes = null;

                                if (m_CurrNode != null) // came here from node ( = object location or physical item)
                                {
                                    nodes = new IDNMcObjectSchemeNode[1] { m_CurrNode };
                                }
                                else                    // came here from object or scheme 
                                {
                                    nodes = currentScheme.GetNodes(DNENodeKindFlags._ENKF_OBJECT_LOCATION);
                                }
                                arrShowPointsLocations = new MCTShowPointsLocations[nodes.Length];
                                int i = 0;
                                foreach (IDNMcObjectSchemeNode node in nodes)
                                {
                                    pointCoordSystem = node.GetGeometryCoordinateSystem(m_CurrObjectOfShowCoord);
                                    paPoints = node.GetCoordinates(MCTMapFormManager.MapForm.Viewport, pointCoordSystem, m_CurrObjectOfShowCoord);
                                    MCTShowPointsLocations showCoordinatesObjectsLocation = new MCTShowPointsLocations();
                                    showCoordinatesObjectsLocation.mcPointCoordSystem = pointCoordSystem;
                                    showCoordinatesObjectsLocation.mcPoints = paPoints;
                                    arrShowPointsLocations[i++] = showCoordinatesObjectsLocation;
                                }
                            }


                            if (arrShowPointsLocations != null && arrShowPointsLocations.Length > 0)
                            {
                                IDNMcTexture texture = DNMcBitmapHandleTexture.Create(Icons.TesterVertex.GetHbitmap(), false, false, new DNSMcBColor(0, 0, 0, 255));
                                m_SelectedNodeOM = currentScheme.GetOverlayManager();

                                m_TmpOverlay = DNMcOverlay.Create(m_SelectedNodeOM, true);

                                uint InsertAtIndex = 1;
                                IDNMcObjectScheme mcObjectScheme = null;
                                for (int i = 0; i < arrShowPointsLocations.Length; i++)
                                {
                                    IDNMcObjectLocation mcObjectLocation = null;
                                    uint locationIndex;
                                    IDNMcPictureItem picItem = DNMcPictureItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, pointCoordSystem, texture);
                                    picItem.SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                                    if (mcObjectScheme == null)
                                    {
                                        mcObjectScheme = DNMcObjectScheme.Create(ref mcObjectLocation, m_SelectedNodeOM, arrShowPointsLocations[i].mcPointCoordSystem);
                                        m_ShowCoordObj = DNMcObject.Create(m_TmpOverlay, mcObjectScheme, arrShowPointsLocations[i].mcPoints);
                                        locationIndex = 0;
                                    }
                                    else
                                    {
                                        mcObjectScheme.AddObjectLocation(out mcObjectLocation,
                                                   arrShowPointsLocations[i].mcPointCoordSystem,
                                                   false,
                                                   out locationIndex,
                                                   InsertAtIndex++);
                                        if(m_ShowCoordObj != null)
                                            m_ShowCoordObj.SetLocationPoints(arrShowPointsLocations[i].mcPoints, locationIndex);
                                    }
                                    if(picItem != null && mcObjectLocation != null )
                                        picItem.Connect(mcObjectLocation);

                                }
                               
                                if (m_ShowCoordObj != null)
                                {
                                    m_ShowCoordObj.SetDetectibility(false);
                                    m_ShowCoordObj.SetImageCalc(m_CurrObjectOfShowCoord.GetImageCalc());

                                    Manager_ShowPointsObjects.AddShowPointsSchemes(m_ShowCoordObj.GetScheme());

                                    // turn on all viewports render needed flags
                                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(MCTMapFormManager.MapForm.Viewport);
                                }
                            }
                        }
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Create icons for node calculate location points", McEx);
            }
        }

        private void UnshowNodeCalcCoord(bool isClickOff = false)
        {
            Manager_ShowPointsObjects.ClearShowCoordinatesSchemes();
            if (m_ShowCoordObj != null)
            {
               
                m_ShowCoordObj.Remove();
                m_ShowCoordObj.Dispose();
                m_ShowCoordObj = null;
            }
            if (m_TmpOverlay != null)
            {
                m_TmpOverlay.Remove();
                m_TmpOverlay.Dispose();
                m_TmpOverlay = null;
            }


            if (isClickOff)
            {
                ReleaseShowCoord();
            }
        }

        private void ReleaseShowCoord()
        {
            m_CurrNode = null;
            m_CurrObjectOfShowCoord = null;
            m_CurrSchemeOfShowCoord = null;
        }

        private void RemoveObject(IDNMcObject objToRemove)
        {
            if (objToRemove != null)
            {
                IDNMcObjectScheme scheme = objToRemove.GetScheme();
                
                try
                {
                    foreach (IDNMcObjectSchemeNode node in scheme.GetNodes(DNENodeKindFlags._ENKF_ANY_NODE))
                    {
                        node.Dispose();
                        if (node == m_CurrNode)
                            m_CurrNode = null;
                    }

                    IDNMcOverlayManager OM = objToRemove.GetOverlayManager();
                    IDNMcConditionalSelector[] selectorArr = OM.GetConditionalSelectors();
                    foreach (IDNMcConditionalSelector selector in selectorArr)
                    {
                        if (OM.IsConditionalSelectorLocked(selector) == false)
                            selector.Dispose();
                        if (selector is IDNMcLocationConditionalSelector)
                            Manager_MCConditionalSelectorObjects.RemoveObjectFromDic((IDNMcLocationConditionalSelector)selector, objToRemove);
                    }

                    scheme.Dispose();

                    // if objToRemove is symbology and exist anchor points for him, need to removed them. 
                    Manager_MCTSymbology.HandleRemoveObject(objToRemove);
                    MainForm.HandleRemoveObject(objToRemove);

                    objToRemove.Dispose();
                    objToRemove.Remove();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Remove Object", McEx);
                }
            }
        }

        //Create new items
        private void miNewItem_DropDownOpened(object sender, EventArgs e)
        {
            m_ObjectPropertiesForm = new MCTester.GUI.Forms.ObjectPropertiesForm();

          //  m_ObjectPropertiesForm.cmbLocationCoordSys.Visible = false;
          //  m_ObjectPropertiesForm.lblLocationCoordSys.Visible = false;
          //  m_ObjectPropertiesForm.chxLocationRelativeToDTM.Visible = false;
        }

        private void emptySymbolicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcEmptySymbolicItem ObjSchemeItem = DNMcEmptySymbolicItem.Create();

                //Add the new item to items dictionary
                Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("", McEx);
            }

            RefreshTree();
        }

        private void pictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ObjectPropertiesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DNEMcPointCoordSystem ePictureCoordinateSystem =
                        ((ObjectPropertiesBase.ItemSubTypeFlags & DNEItemSubTypeFlags._EISTF_WORLD) != (DNEItemSubTypeFlags)0 ? DNEMcPointCoordSystem._EPCS_WORLD : DNEMcPointCoordSystem._EPCS_SCREEN)/*:::???!!!::: [CoordSys]*/;
                    IDNMcPictureItem ObjSchemeItem = DNMcPictureItem.Create(ObjectPropertiesBase.ItemSubTypeFlags, ePictureCoordinateSystem, null);

                    //Add the new item to items dictionary
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcPictureItem.Create", McEx);
                }
            }
            RefreshTree();
        }

        private void arcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ObjectPropertiesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IDNMcArcItem ObjSchemeItem = DNMcArcItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                        ObjectPropertiesBase.ArcCoordSys,
                                                                        ObjectPropertiesBase.ArcEllipseType);

                    //Add the new item to items dictionary
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcArcItem.Create", McEx);
                }

            }
            RefreshTree();
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ObjectPropertiesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IDNMcLineItem ObjSchemeItem = DNMcLineItem.Create(ObjectPropertiesBase.ItemSubTypeFlags);

                    //Add the new item to items dictionary
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcLineItem.Create", McEx);
                }

            }

            RefreshTree();
        }

        private void arrowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ObjectPropertiesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IDNMcArrowItem ObjSchemeItem = DNMcArrowItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                                ObjectPropertiesBase.ArrowCoordSys);

                    //Add the new item to items dictionary
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcArrowItem.Create", McEx);
                }
            }
            RefreshTree();
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ObjectPropertiesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IDNMcEllipseItem ObjSchemeItem = DNMcEllipseItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                                    ObjectPropertiesBase.EllipseCoordSys,
                                                                                    ObjectPropertiesBase.EllipseType);

                    //Add the new item to items dictionary
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcEllipseItem.Create", McEx);
                }
            }

            RefreshTree();
        }

        private void polygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ObjectPropertiesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IDNMcPolygonItem ObjSchemeItem = DNMcPolygonItem.Create(ObjectPropertiesBase.ItemSubTypeFlags);
                    //Add the new item to items dictionary
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcPolygonItem.Create", McEx);
                }
            }
            RefreshTree();
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ObjectPropertiesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IDNMcRectangleItem ObjSchemeItem = DNMcRectangleItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                                    ObjectPropertiesBase.RectangleCoordSys,
                                                                                    ObjectPropertiesBase.RectangleType);
                    //Add the new item to items dictionary
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcRectangleItem.Create", McEx);
                }
            }
            RefreshTree();
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ObjectPropertiesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DNEMcPointCoordSystem eTextCoordinateSystem =
                        ((ObjectPropertiesBase.ItemSubTypeFlags & DNEItemSubTypeFlags._EISTF_WORLD) != (DNEItemSubTypeFlags)0 ? DNEMcPointCoordSystem._EPCS_WORLD : DNEMcPointCoordSystem._EPCS_SCREEN)/*:::???!!!::: [CoordSys]*/;
                    IDNMcTextItem ObjSchemeItem = DNMcTextItem.Create(ObjectPropertiesBase.ItemSubTypeFlags, eTextCoordinateSystem,
                                                                        null,
                                                                        ObjectPropertiesBase.TextScale,
                                                                        ObjectPropertiesBase.NeverUpsideDown);

                    //Add the new item to items dictionary
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcTextItem.Create", McEx);
                }
            }
            RefreshTree();
        }

        private void meshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ObjectPropertiesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IDNMcMeshItem ObjSchemeItem = DNMcMeshItem.Create(null);

                    //Add the new item to items dictionary
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcMeshItem.Create", McEx);
                }
            }
            RefreshTree();
        }

        private void particleEffectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ObjectPropertiesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IDNMcParticleEffectItem ObjSchemeItem = DNMcParticleEffectItem.Create(null);

                    //Add the new item to items dictionary
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcParticleEffectItem.Create", McEx);
                }
            }
            RefreshTree();
        }

        private void miConnect_Click(object sender, EventArgs e)
        {
            TreeFormBaseClass selNode = (TreeFormBaseClass)m_TreeView.SelectedNode.Tag;
            IDNMcSymbolicItem node = (IDNMcSymbolicItem)selNode.TemporaryObject;

            frmConnectedList connectedForm = new frmConnectedList(node);
            if (connectedForm.ShowDialog() == DialogResult.OK)
            {
                Managers.ObjectWorld.Manager_MCObjectSchemeItem.RemoveItem(node);
            }
            RefreshTree(true);
        }

        private void miDisconnectAndKeep_Click(object sender, EventArgs e)
        {
            try
            {
                TreeFormBaseClass selNode = (TreeFormBaseClass)m_TreeView.SelectedNode.Tag;
                IDNMcObjectSchemeItem node = (IDNMcObjectSchemeItem)selNode.TemporaryObject;

                m_DisconnectedNodes.Add(node);
                GetAllNodeChildren(node);

                foreach (IDNMcObjectSchemeNode item in m_DisconnectedNodes)
                {
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem((IDNMcObjectSchemeItem)item);
                }

                node.Disconnect();
                m_CurrNode = (m_CurrNode == node) ? null : m_CurrNode;
                RefreshTree(true);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Disconnect", McEx);
                return;
            }
}

        private void GetAllNodeChildren(IDNMcObjectSchemeItem CurrNode)
        {
            IDNMcObjectSchemeNode[] schemeNodeArr = CurrNode.GetChildren();
            m_DisconnectedNodes.AddRange(schemeNodeArr);

            while (schemeNodeArr.Length > 0 && schemeNodeArr != null)
            {
                foreach (IDNMcObjectSchemeItem node in schemeNodeArr)
                {
                    GetAllNodeChildren(node);
                }
                break;
            }
        }

        private void miDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                TreeFormBaseClass selNode = (TreeFormBaseClass)m_TreeView.SelectedNode.Tag;
                IDNMcObjectSchemeItem node = (IDNMcObjectSchemeItem)selNode.TemporaryObject;

                node.Disconnect();

                m_CurrNode = (m_CurrNode == node) ? null : m_CurrNode;

                //Remove the item in case this is a standalone item.
                if (node.GetScheme() == null)
                {
                    Manager_MCObjectSchemeItem.RemoveItem(node);
                }

                node.Dispose();
                RefreshTree();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Disconnect", McEx);
                return;
            }
        }

        private void miCloneSymbolicItem_Click(object sender, EventArgs e)
        {
            CloneItem();
        }


        private void miPhysicalItemConnect_Click(object sender, EventArgs e)
        {
            TreeFormBaseClass selNode = (TreeFormBaseClass)m_TreeView.SelectedNode.Tag;
            IDNMcPhysicalItem node = (IDNMcPhysicalItem)selNode.TemporaryObject;

            frmConnectedList connectedForm = new frmConnectedList(node);
            if (connectedForm.ShowDialog() == DialogResult.OK)
            {
                Manager_MCObjectSchemeItem.RemoveItem(node);
            }
            RefreshTree();
        }

        private void CloneItem()
        {
            TreeFormBaseClass selNode = (TreeFormBaseClass)m_TreeView.SelectedNode.Tag;
            IDNMcObjectSchemeItem node = (IDNMcObjectSchemeItem)selNode.TemporaryObject;
            DialogResult DlgResult = MessageBox.Show("Clone object properties as well?", "Clone item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            IDNMcObjectSchemeItem clonedItem = null;

            if (DlgResult == DialogResult.Yes)
            {
                frmGetObjectListBaseOnObjectSchemeItem GetObjectListBaseOnObjectSchemeItemForm = new frmGetObjectListBaseOnObjectSchemeItem(node);
                if (GetObjectListBaseOnObjectSchemeItemForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        node.Clone(out clonedItem, GetObjectListBaseOnObjectSchemeItemForm.SelectedObject);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("Clone - with object", McEx);
                        return;
                    }
                }
            }
            else
            {
                try
                {
                    node.Clone(out clonedItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Clone - without object", McEx);
                    return;
                }
            }

            //Add the new item to items dictionary
            Manager_MCObjectSchemeItem.AddNewItem(clonedItem);

            RefreshTree();
        }

        private void miClonePhysicalItem_Click(object sender, EventArgs e)
        {
            CloneItem();
        }

        private void basedOnSchemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocationPointsNew LocationPointsForm = new frmLocationPointsNew((IDNMcOverlay)SelectedNode, frmLocationPointsNew.FormType.ExistingScheme);
            LocationPointsForm.ShowDialog();
        }

        private void newSchemWithToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocationPointsNew LocationPointsForm = new frmLocationPointsNew((IDNMcOverlay)SelectedNode, frmLocationPointsNew.FormType.NewScheme);
            LocationPointsForm.ShowDialog();
        }

        private void newSchemContainingOneLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocationPointsNew LocationPointsForm = new frmLocationPointsNew((IDNMcOverlay)SelectedNode, frmLocationPointsNew.FormType.NewSchemeAndOneItem);
            LocationPointsForm.ShowDialog();
        }

        private void miNewOverlayManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewOverlayManager newOM = new frmNewOverlayManager();
            if (newOM.ShowDialog() == DialogResult.OK)
                RefreshTree();
        }

        private void miCloneObject_Click(object sender, EventArgs e)
        {
            IDNMcObject obj = (IDNMcObject)SelectedNode;
            frmCloneObject frmCloneObject = new frmCloneObject(obj);
            if (frmCloneObject.ShowDialog() == DialogResult.OK)
            {
                RefreshTree();
            }
        }

        private void miRemoveObject_Click(object sender, EventArgs e)
        {
            IDNMcObject obj = (IDNMcObject)SelectedNode;
            RemoveObject(obj);

            RefreshTree();
        }

        private void miMoveToOtherOverlay_Click(object sender, EventArgs e)
        {
            frmOverlaysList OverlaysListForm = new frmOverlaysList();
            if (OverlaysListForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IDNMcObject obj = (IDNMcObject)SelectedNode;
                    obj.SetOverlay(OverlaysListForm.SelectedOverlay);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetOverlay", McEx);
                }
            }
            RefreshTree();
        }

        private void miReplaceObjectScheme_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
            {
                frmSchemeList schemeListForm = new frmSchemeList((IDNMcObject)SelectedNode);

                if (schemeListForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        IDNMcObjectScheme changedScheme = ((IDNMcObject)SelectedNode).GetScheme();
                        if (changedScheme.GetObjects().Length < 2)
                            changedScheme.Dispose();

                        ((IDNMcObject)SelectedNode).SetScheme(schemeListForm.SelectedScheme,
                                                               schemeListForm.KeepRelevantProperties);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("SetScheme", McEx);
                    }
                }
                RefreshTree();
            }
            else
                MessageBox.Show("Please select scheme from the list", "Replace scheme");
        }

        private void miMoveToLocation_Click(object sender, EventArgs e)
        {
            IDNMcObject mcObject = (IDNMcObject)SelectedNode;
            try
            {
                uint numLocations = mcObject.GetNumLocations();
                if (numLocations == 1)
                    Manager_MCObject.MoveToLocation((IDNMcObject)SelectedNode, 0);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("MoveToLocation_Click", McEx);
            }
        }

        private void miClone_Click(object sender, EventArgs e)
        {
            try
            {
                ((IDNMcObjectScheme)SelectedNode).Clone();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Clone", McEx);
            }

            RefreshTree();
        }

        private void miScemeWithOneLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewObjectScheme NewObjectSchemeForm = new frmNewObjectScheme(sender, (IDNMcOverlayManager)SelectedNode);
            NewObjectSchemeForm.Show();

            RefreshTree();
        }

        private void miSchemeWithLocationAndItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewObjectScheme NewObjectSchemeForm = new frmNewObjectScheme(sender, (IDNMcOverlayManager)SelectedNode);
            if (NewObjectSchemeForm.ShowDialog() == DialogResult.OK)
            {
                RefreshTree();
            }
        }

        private void miCreateOverlay_Click(object sender, EventArgs e)
        {
            try
            {
                DNMcOverlay.Create((IDNMcOverlayManager)SelectedNode);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcOverlay.Create", McEx);
            }

            RefreshTree();
        }

        private void miRemoveOverlayManager_Click(object sender, EventArgs e)
        {
            try
            {
                int numViewportsUsingOM = 0;
                Dictionary<object, uint> Viewports = MCTester.Managers.MapWorld.Manager_MCViewports.AllParams;
                foreach (IDNMcMapViewport VP in Viewports.Keys)
                {
                    if (VP.OverlayManager == (IDNMcOverlayManager)SelectedNode)
                        numViewportsUsingOM++;
                }

                if (numViewportsUsingOM > 0)
                {
                    MessageBox.Show("Overlay Manager can't be removed\n" +
                                        "In order to remove it, you have to close all viewports that using it first!",
                                        "RemoveOwnedForm Overlay Manager",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                }
                else
                {
                    Manager_MCOverlayManager.dOverlayManager_Overlay.Remove((IDNMcOverlayManager)SelectedNode);
                    ((IDNMcOverlayManager)SelectedNode).Dispose();

                    RefreshTree();
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("(IDNMcOverlayManager)SelectedNode).Dispose()", McEx);
            }
        }

        private void miRemoveOverlay_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcOverlay overlayToRemove = (IDNMcOverlay)SelectedNode;
                IDNMcOverlayManager OM = overlayToRemove.GetOverlayManager();

                //In case that the removed overlay is the active overlay, the active overlay change to Null.
                if (Manager_MCOverlayManager.dOverlayManager_Overlay[OM] == overlayToRemove)
                    Manager_MCOverlayManager.dOverlayManager_Overlay[OM] = null;

                IDNMcObject[] mcObjects = overlayToRemove.GetObjects();
                foreach (IDNMcObject mcObject in mcObjects)
                    Manager_MCTSymbology.HandleRemoveObject(mcObject);

                Manager_MCTObjectSchemeData.RemoveObjectFromDic(overlayToRemove);

                overlayToRemove.Remove();
                overlayToRemove.Dispose();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Remove Overlay", McEx);
            }

            RefreshTree();
        }

        private void miRemoveAllObjects_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcObject[] objectsArr = ((IDNMcOverlay)SelectedNode).GetObjects();

                foreach (IDNMcObject obj in objectsArr)
                {
                    RemoveObject(obj);
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Remove", McEx);
            }

            RefreshTree();
        }

        private void miCreateCollection_Click(object sender, EventArgs e)
        {
            IDNMcOverlayManager currOM = (IDNMcOverlayManager)SelectedNode;
            try
            {
                if (currOM != null)
                    DNMcCollection.Create(currOM);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcCollection.Create", McEx);
            }

            RefreshTree();
        }

        private void miSetActiveOverlay_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcOverlay overlay = (IDNMcOverlay)SelectedNode;
                Manager_MCOverlayManager.SetActiveOverlayToOverlayManager(overlay, overlay.GetOverlayManager());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetOverlayManager", McEx);
            }
        }

        private void miAddObjectLocation_Click(object sender, EventArgs e)
        {
            frmNewObjectLocation NewObjectLocation = new frmNewObjectLocation((IDNMcObjectScheme)SelectedNode);
            if (NewObjectLocation.ShowDialog() == DialogResult.OK)
            {
                RefreshTree();
            }
        }

        private void miRemoveObjectLocation_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcObjectScheme locationScheme = ((IDNMcObjectLocation)SelectedNode).GetScheme();
                locationScheme.RemoveObjectLocation(((IDNMcObjectLocation)SelectedNode).GetIndex());
                m_CurrNode = null;
                ((IDNMcObjectLocation)SelectedNode).Dispose();

                RefreshTree();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("RemoveObjectLocation", McEx);
            }
        }

        private void emptyPhysicalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcEmptyPhysicalItem ObjSchemeItem = DNMcEmptyPhysicalItem.Create();

                //Add the new item to items dictionary
                Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcEmptyPhysicalItem.Create()", McEx);
            }

            RefreshTree();
        }

        private void lineExpansionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ObjectPropertiesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IDNMcLineExpansionItem ObjSchemeItem = DNMcLineExpansionItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                                            ObjectPropertiesBase.LineExpansionCoordinateSystem,
                                                                                            ObjectPropertiesBase.LineExpansionType);

                    //Add the new item to items dictionary
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcLineExpansionItem.Create", McEx);
                }

            }

            RefreshTree();
        }

        private void miDirectionalLight_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcDirectionalLightItem ObjSchemeItem = DNMcDirectionalLightItem.Create();

                //Add the new item to items dictionary
                Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcDirectionalLightItem.Create", McEx);
            }

            RefreshTree();
        }

        private void miPointLight_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcPointLightItem ObjSchemeItem = DNMcPointLightItem.Create();

                //Add the new item to items dictionary
                Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcPointLightItem.Create", McEx);
            }

            RefreshTree();
        }

        private void miSpotLight_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcSpotLightItem ObjSchemeItem = DNMcSpotLightItem.Create();

                //Add the new item to items dictionary
                Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcSpotLightItem.Create", McEx);
            }

            RefreshTree();
        }

        private void soundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ObjectPropertiesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IDNMcSoundItem ObjSchemeItem = DNMcSoundItem.Create(null);

                    //Add the new item to items dictionary
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcSoundItem.Create", McEx);
                }

            }

            RefreshTree();
        }

        private void projectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ObjectPropertiesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IDNMcProjectorItem ObjSchemeItem = DNMcProjectorItem.Create(null,
                                                                                ObjectPropertiesBase.ProjectorHalfFOVHorizAngle, 1,
                                                                                ObjectPropertiesBase.ProjectorItemTargetTypeFlags);

                    //Add the new item to items dictionary
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcProjectorItem.Create", McEx);
                }

            }

            RefreshTree();
        }

        private void menualGeometryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ObjectPropertiesForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IDNMcManualGeometryItem ObjSchemeItem = DNMcManualGeometryItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                    ObjectPropertiesBase.ProceduralGeometryCoordinateSys, DNERenderingMode._ERM_POINTS);
                    //Add the new item to items dictionary
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.AddNewItem(ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("IDNMcManualGeometryItem.Create", McEx);
                }
            }
            RefreshTree();
        }

        private void m_TreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "Root")
            {
                BuildTree();
            }
        }

        private void miRomveScheme_Click(object sender, EventArgs e)
        {
            IDNMcOverlayManager OM = ((IDNMcObjectScheme)SelectedNode).GetOverlayManager();
            OM.SetObjectSchemeLock((IDNMcObjectScheme)SelectedNode, false);
            Manager_MCTObjectSchemeData.RemoveObjectFromDic(OM);

            ((IDNMcObjectScheme)SelectedNode).Dispose();

            RefreshTree();
        }

        private void miRemoveConditionalSelector_Click(object sender, EventArgs e)
        {
            IDNMcOverlayManager OM = ((IDNMcConditionalSelector)SelectedNode).GetOverlayManager();
            OM.SetConditionalSelectorLock((IDNMcConditionalSelector)SelectedNode, false);
            ((IDNMcConditionalSelector)SelectedNode).Dispose();

            RefreshTree();
        }

        private void miBlockedConditionalSelector_Click(object sender, EventArgs e)
        {
            ucBlockedConditionalSelector CtrlBlockedCondSelector = new ucBlockedConditionalSelector();
            Form frmCondSelector = new Form();

            frmCondSelector.Controls.Add(CtrlBlockedCondSelector);
            frmCondSelector.ClientSize = CtrlBlockedCondSelector.Size;
            CtrlBlockedCondSelector.Dock = DockStyle.Fill;

            try
            {
                IDNMcBlockedConditionalSelector m_BlockedSelector = DNMcBlockedConditionalSelector.Create((IDNMcOverlayManager)SelectedNode);
                ((IDNMcOverlayManager)SelectedNode).SetConditionalSelectorLock(m_BlockedSelector, true);

                CtrlBlockedCondSelector.LoadItem(m_BlockedSelector);
                frmCondSelector.ShowDialog();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcBlockedConditionalSelector.Create", McEx);
            }

            RefreshTree();
        }

        private void miBooleanConditionalSelector_Click(object sender, EventArgs e)
        {
            ucBooleanConditionalSelector CtrlBooleanCondSelector = new ucBooleanConditionalSelector();

            Form frmCondSelector = new Form();

            frmCondSelector.Controls.Add(CtrlBooleanCondSelector);
            frmCondSelector.ClientSize = CtrlBooleanCondSelector.Size;
            CtrlBooleanCondSelector.Dock = DockStyle.Fill;

            try
            {
                IDNMcBooleanConditionalSelector m_BooleanSelector = DNMcBooleanConditionalSelector.Create((IDNMcOverlayManager)SelectedNode,
                                                                                                            null,
                                                                                                            DNEBooleanOp._EB_OR);

                ((IDNMcOverlayManager)SelectedNode).SetConditionalSelectorLock(m_BooleanSelector, true);
                CtrlBooleanCondSelector.LoadItem(m_BooleanSelector);
                frmCondSelector.ShowDialog();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcBooleanConditionalSelector.Create", McEx);
            }

            RefreshTree();
        }

        private void miScaleConditionalSelector_Click(object sender, EventArgs e)
        {
            ucScaleConditionalSelector CtrlScaleCondSelector = new ucScaleConditionalSelector();
            Form frmCondSelector = new Form();

            frmCondSelector.Controls.Add(CtrlScaleCondSelector);
            frmCondSelector.ClientSize = CtrlScaleCondSelector.Size;
            CtrlScaleCondSelector.Dock = DockStyle.Fill;

            try
            {
                IDNMcScaleConditionalSelector m_ScaleSelector = DNMcScaleConditionalSelector.Create((IDNMcOverlayManager)SelectedNode,
                                                                                                     0,
                                                                                                     float.MaxValue,
                                                                                                     0,
                                                                                                     0);

                ((IDNMcOverlayManager)SelectedNode).SetConditionalSelectorLock(m_ScaleSelector, true);
                CtrlScaleCondSelector.LoadItem(m_ScaleSelector);
                frmCondSelector.ShowDialog();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcScaleConditionalSelector.Create", McEx);
            }

            RefreshTree();
        }

        private void miObjectStateConditionalSelector_Click(object sender, EventArgs e)
        {
            ucObjectStateConditionalSelector CtrlSelectionCondSelector = new ucObjectStateConditionalSelector();
            Form frmCondSelector = new Form();

            frmCondSelector.Controls.Add(CtrlSelectionCondSelector);
            frmCondSelector.ClientSize = CtrlSelectionCondSelector.Size;
            CtrlSelectionCondSelector.Dock = DockStyle.Fill;

            try
            {
                IDNMcObjectStateConditionalSelector m_ObjectStateConditionalSelector = DNMcObjectStateConditionalSelector.Create((IDNMcOverlayManager)SelectedNode);

                ((IDNMcOverlayManager)SelectedNode).SetConditionalSelectorLock(m_ObjectStateConditionalSelector, true);
                CtrlSelectionCondSelector.LoadItem(m_ObjectStateConditionalSelector);
                frmCondSelector.ShowDialog();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcScaleConditionalSelector.Create", McEx);
            }

            RefreshTree();
        }
        ucLocationConditionalSelector CtrlSelectionCondSelector;
        private void miLocationConditionalSelector_Click(object sender, EventArgs e)
        {
             CtrlSelectionCondSelector = new ucLocationConditionalSelector();
            Form frmCondSelector = new Form();
           // frmCondSelector.Name = "Cond";
            frmCondSelector.Controls.Add(CtrlSelectionCondSelector);
            frmCondSelector.ClientSize = CtrlSelectionCondSelector.Size;
            CtrlSelectionCondSelector.Dock = DockStyle.Fill;
            frmCondSelector.FormClosed += frmCondSelector_FormClosed;
            try
            {
                IDNMcLocationConditionalSelector m_LocationConditionalSelector = DNMcLocationConditionalSelector.Create((IDNMcOverlayManager)SelectedNode);

                ((IDNMcOverlayManager)SelectedNode).SetConditionalSelectorLock(m_LocationConditionalSelector, true);
                CtrlSelectionCondSelector.LoadItem(m_LocationConditionalSelector);
                frmCondSelector.Show();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcScaleConditionalSelector.Create", McEx);
            }

            RefreshTree();
        }

        void frmCondSelector_FormClosed(object sender, FormClosedEventArgs e)
        {
            CtrlSelectionCondSelector.ucLocationConditionalSelector_Leave(null, null);
        }

        private void miViewportConditionalSelector_Click(object sender, EventArgs e)
        {
            ucViewportConditionalSelector CtrlViewportCondSelector = new ucViewportConditionalSelector();

            Form frmCondSelector = new Form();

            frmCondSelector.Controls.Add(CtrlViewportCondSelector);
            frmCondSelector.ClientSize = CtrlViewportCondSelector.Size;
            CtrlViewportCondSelector.Dock = DockStyle.Fill;
            uint[] viewportIDs = new uint[0];

            try
            {
                IDNMcViewportConditionalSelector m_ViewportSelector = DNMcViewportConditionalSelector.Create((IDNMcOverlayManager)SelectedNode,
                                                                                                                DNEViewportType._EVT_ALL_VIEWPORTS,
                                                                                                                DNEViewportCoordinateSystem._EVCS_ALL_COORDINATE_SYSTEMS,
                                                                                                                viewportIDs,
                                                                                                                false);

                ((IDNMcOverlayManager)SelectedNode).SetConditionalSelectorLock(m_ViewportSelector, true);
                CtrlViewportCondSelector.LoadItem(m_ViewportSelector);
                frmCondSelector.ShowDialog();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcScaleConditionalSelector.Create", McEx);
            }

            RefreshTree();
        }

        private void MCOverlayMangerTreeViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnshowNodeCalcCoord();
            MainForm.BtnObjectWorldCheckedState = false;
            // Manager_MCObject.RemoveAllTempAnchorPoints();
        }

        private void tsmiOnBasePoints_Click(object sender, EventArgs e)
        {
            if (!tsmiOnBasePoints.Checked)
            {
                tsmiOnBasePoints.Checked = !tsmiOnBasePoints.Checked;
                tsmiOffShowPoints.Checked = !tsmiOnBasePoints.Checked;
                tsmiOnCalculatedPoints.Checked = !tsmiOnBasePoints.Checked;
                tsmiOnCalculatesAndAddedPoints.Checked = !tsmiOnBasePoints.Checked;
                ShowNodeCalcCoord();
            }
        }

        private void tsmiOnCalculatedPoints_Click(object sender, EventArgs e)
        {
           if (!tsmiOnCalculatedPoints.Checked)
            {
                tsmiOnCalculatedPoints.Checked = !tsmiOnCalculatedPoints.Checked;
                tsmiOnBasePoints.Checked = !tsmiOnCalculatedPoints.Checked;
                tsmiOffShowPoints.Checked = !tsmiOnCalculatedPoints.Checked;
                tsmiOnCalculatesAndAddedPoints.Checked = !tsmiOnCalculatedPoints.Checked;
                ShowNodeCalcCoord();
            }
        }

        private void tsmiOnCalculatesAndAddedPoints_Click(object sender, EventArgs e)
        {
            if (!tsmiOnCalculatesAndAddedPoints.Checked)
            {
                tsmiOnCalculatesAndAddedPoints.Checked = !tsmiOnCalculatesAndAddedPoints.Checked;
                tsmiOffShowPoints.Checked = !tsmiOnCalculatesAndAddedPoints.Checked;
                tsmiOnCalculatedPoints.Checked = !tsmiOnCalculatesAndAddedPoints.Checked;
                tsmiOnBasePoints.Checked = !tsmiOnCalculatesAndAddedPoints.Checked;
                ShowNodeCalcCoord();
            }
        }

        private void tsmiOffShowPoints_Click(object sender, EventArgs e)
        {
            if (!tsmiOffShowPoints.Checked)
            {
                tsmiOffShowPoints.Checked = !tsmiOffShowPoints.Checked;
                tsmiOnCalculatesAndAddedPoints.Checked = !tsmiOffShowPoints.Checked;
                tsmiOnBasePoints.Checked = !tsmiOffShowPoints.Checked;
                tsmiOnCalculatedPoints.Checked = !tsmiOffShowPoints.Checked;

                UnshowNodeCalcCoord(true);
               
            }
        }

        private void miObjectRename_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcObject mcObj = (IDNMcObject)SelectedNode;
                string name, desc;
                mcObj.GetNameAndDescription(out name, out desc);
                RenameMCNames(name);

                SelectNodeInTreeNode((uint)mcObj.GetHashCode());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IDNMcObjectScheme.GetName()", McEx);
            }
        }

        private void miDeleteName_Click(object sender, EventArgs e)
        {
            DeleteName();
        }


        private void miPhysicalItemRename_Click(object sender, EventArgs e)
        {
            try
            {
                RenameMCNames(((IDNMcObjectSchemeNode)SelectedNode).GetName());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("IDNMcObjectScheme.GetName()", McEx);
            }
        }

        private void miPhysicalItemDeleteName_Click(object sender, EventArgs e)
        {
            DeleteName();
        }


        private void imSymbolicItemRename_Click(object sender, EventArgs e)
        {
            try
            {
                RenameMCNames(((IDNMcObjectSchemeNode)SelectedNode).GetName());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("IDNMcObjectSchemeNode.GetName()", McEx);
            }
        }

        private void imSymbolicItemDeleteName_Click(object sender, EventArgs e)
        {
            DeleteName();
        }       

        private void miConditionalSelectorRename_Click(object sender, EventArgs e)
        {
            try
            {
                RenameMCNames(((IDNMcConditionalSelector)SelectedNode).GetName());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("IDNMcConditionalSelector.GetName()", McEx);
            }
        }

        private void miConditionalSelectorDeleteName_Click(object sender, EventArgs e)
        {
            DeleteName();
        }

        private void miRemoveCollection_Click(object sender, EventArgs e)
        {
            IDNMcCollection currCollection = (IDNMcCollection)SelectedNode;
            try
            {
                if (currCollection != null)
                {
                    currCollection.Remove();

                    // turn on relevant viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(currCollection.GetOverlayManager());
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Remove", McEx);
            }

            RefreshTree();
        }

        private void miClearCollection_Click(object sender, EventArgs e)
        {
            IDNMcCollection currCollection = (IDNMcCollection)SelectedNode;
            try
            {
                if (currCollection != null)
                {
                    currCollection.Clear();

                    // turn on relevant viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(currCollection.GetOverlayManager());
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Clear", McEx);
            }

            RefreshTree();
        }

        private void miRenameCollection_Click(object sender, EventArgs e)
        {
            Rename();
        }

        private void miSchemeRename_Click(object sender, EventArgs e)
        {
            try
            {
                RenameMCNames(((IDNMcObjectScheme)SelectedNode).GetName());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("IDNMcObjectScheme.GetName()", McEx);
            }
        }

        private void miSchemeDeleteName_Click(object sender, EventArgs e)
        {
            DeleteName();
        }

        private void miOverlayRename_Click(object sender, EventArgs e)
        {
            Rename();
        }

        private void miOverlayDeleteName_Click(object sender, EventArgs e)
        {
            DeleteName();
        }

        private void miOMRename_Click(object sender, EventArgs e)
        {
            Rename();
        }

        private void miOMDeleteName_Click(object sender, EventArgs e)
        {
            DeleteName();
        }

        private void miObjectLocationRename_Click(object sender, EventArgs e)
        {

            try
            {
                RenameMCNames(((IDNMcObjectSchemeNode)SelectedNode).GetName());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("IDNMcObjectSchemeNode.GetName()", McEx);
            }
        }

        private void miObjectLocationDeleteName_Click(object sender, EventArgs e)
        {
            DeleteName();
        }
       

        private void imDeleteNameCollection_Click(object sender, EventArgs e)
        {
            DeleteName();
        }

        private void miSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveObject();
        }

        private void miSaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveObject(true);
        }

        private DNEStorageFormat GetStorageFormatByFileName(string fileName)
        {
            DNEStorageFormat dNEStorageFormat = DNEStorageFormat._ESF_MAPCORE_BINARY;
            string fileExtension = Path.GetExtension(fileName);
            if (fileExtension == ".json")
                dNEStorageFormat = DNEStorageFormat._ESF_JSON;
            return dNEStorageFormat;
        }

        private void miSaveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dictionary<IDNMcOverlayManager, IDNMcOverlay> dOverlayManager_Overlay = Manager_MCOverlayManager.dOverlayManager_Overlay;
            foreach (IDNMcOverlayManager mcOverlayManager in dOverlayManager_Overlay.Keys)
            {
                
                try
                {
                    IDNMcOverlay overlay = dOverlayManager_Overlay[mcOverlayManager];
           
                    IDNMcObject[] lstObject = overlay.GetObjects();
                    foreach (IDNMcObject obj in lstObject)
                    {
                        SaveObject(obj, overlay);
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetObjects Or SaveObjects - to a file ", McEx);
                }

                try
                {
                    IDNMcObjectScheme[] arrObjectScheme = mcOverlayManager.GetObjectSchemes();
                    foreach (IDNMcObjectScheme scheme in arrObjectScheme)
                    {
                        if (!Manager_MCObjectScheme.IsTempObjectScheme(scheme))
                        {
                            SaveObjectScheme(scheme, mcOverlayManager);
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetObjectSchemes Or SaveObjectSchemes - to a file ", McEx);
                }
            }
        }

        private IDNMcObject[] GetArrayObject(IDNMcObject obj)
        {
            return new IDNMcObject[1] {obj};
        }

        private IDNMcObjectScheme[] GetArrayObjectScheme(IDNMcObjectScheme obj)
        {
            return new IDNMcObjectScheme[1] { obj };
        }

        private void SaveObject(bool isSaveAs = false)
        {
            SaveObject((IDNMcObject)SelectedNode, (IDNMcOverlay)((TreeFormBaseClass)m_TreeView.SelectedNode.Parent.Tag).TemporaryObject, isSaveAs, true);
        }

        private void SaveObject(IDNMcObject objectNode, IDNMcOverlay overlayNode, bool isSaveAs = false, bool isOpenSaveFileDlg = false)
        {
            try
            {
                string filePath = GetSavedFilePath(isSaveAs, objectNode, true, isOpenSaveFileDlg);

                if (filePath != "" && filePath != null)
                {
                    DNEStorageFormat format = GetStorageFormatByFileName(filePath);
                    bool isAllSchemeHasSameVersion;
                    IDNMcObject[] mcObjects = GetArrayObject(objectNode);
                    DNESavingVersionCompatibility SavingVersionCompatibility = Manager_MCTObjectSchemeData.GetSavingVersionCompatibility(mcObjects, out isAllSchemeHasSameVersion);

                    overlayNode.SaveObjects(mcObjects, filePath, format, SavingVersionCompatibility);

                    Manager_MCTObjectSchemeData.AddObjectSchemeData(objectNode, (uint)SavingVersionCompatibility, format, filePath);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SaveObjects - to a file", McEx);
            }
        }

        private string GetSavedFilePath(bool isSaveAs, object obj, bool isForObject, bool isOpenSaveFileDlg = false)
        {
            string filePath = Manager_MCTObjectSchemeData.GetFilePathByObject(obj);
            if(isSaveAs || (filePath == string.Empty && isOpenSaveFileDlg))
                filePath = SaveFileDlg(isForObject);
            
            return filePath;
        }

        private string SaveFileDlg(bool isForObject)
        {
            if(isForObject)
                SFD.Filter = "Map Core Object Files (*.mcobj, *.m, *.mcobj.json , *.json) |*.mcobj; *.m; *.mcobj.json; *.json;| Map Core Binary Files (*.mcobj,*.m)|*.mcobj;*.m;| Map Core Json files (*.mcobj.json, *.json)|*.mcobj.json;*.json;| All Files|*.*";
            else
                SFD.Filter = "Map Core Object Scheme Files (*.mcsch, *.m, *.mcsch.json, *.json) |*.mcsch; *.m; *.mcsch.json; *.json;| Map Core Binary Files (*.mcsch,*.m)|*.mcsch;*.m;| Map Core Json files (*.mcsch.json, *.json)|*.mcsch.json; *.json;| All Files|*.*";

            SFD.RestoreDirectory = true;
            if (SFD.ShowDialog() == DialogResult.OK)
            {
                return SFD.FileName;
            }
            else
                return "";
        }

        private SaveFileDialog SFD = new SaveFileDialog();

        private void miStopSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDNMcObject objectNode = (IDNMcObject)SelectedNode;
            Manager_MCTObjectSchemeData.RemoveObjectFilePath(objectNode);
        }

        private void miSaveTextureToFolder_Click(object sender, EventArgs e)
        {
            IDNMcObjectScheme objectNode = (IDNMcObjectScheme)SelectedNode;
            Manager_MCObjectSchemeItem.LoadStadaloneItems(objectNode, true);
           // Manager_MCObjectSchemeItem.SaveTexturesToFolder();
        }

        private void miSchemeSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveObjectScheme();
        }

        private void SaveObjectScheme(bool isSaveAs = false)
        {
            IDNMcObjectScheme objectNode = (IDNMcObjectScheme)SelectedNode;
            SaveObjectScheme(objectNode, (IDNMcOverlayManager)((TreeFormBaseClass)m_TreeView.SelectedNode.Parent.Tag).TemporaryObject, isSaveAs, true);
        }

        private void SaveObjectScheme(IDNMcObjectScheme objectSchemeNode, IDNMcOverlayManager mcOverlayManager, bool isSaveAs = false, bool isOpenSaveFileDlg = false)
        {
            try
            {
                string filePath = GetSavedFilePath(isSaveAs, objectSchemeNode, false, isOpenSaveFileDlg);

                if (filePath != "" && filePath != null)
                {
                    DNEStorageFormat format = GetStorageFormatByFileName(filePath);
                    bool isAllSchemeHasSameVersion;
                    IDNMcObjectScheme[] mcObjectSchemes = GetArrayObjectScheme(objectSchemeNode);
                    DNESavingVersionCompatibility SavingVersionCompatibility = Manager_MCTObjectSchemeData.GetSavingVersionCompatibility(mcObjectSchemes, out isAllSchemeHasSameVersion);

                    mcOverlayManager.SaveObjectSchemes(GetArrayObjectScheme(objectSchemeNode), filePath, GetStorageFormatByFileName(filePath), SavingVersionCompatibility);

                    Manager_MCTObjectSchemeData.AddObjectSchemeData(objectSchemeNode, (uint)SavingVersionCompatibility, format, filePath);

                    SaveParamsData.SavePropertiesCSV(filePath, objectSchemeNode);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SaveObjects - to a file", McEx);
            }
        }

        private void miSchemeSaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveObjectScheme(true);
        }

        private void miSchemeStopSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDNMcObjectScheme objectNode = (IDNMcObjectScheme)SelectedNode;
            Manager_MCTObjectSchemeData.RemoveObjectFilePath(objectNode);
        }

        private void imJumpToScheme_Click(object sender, EventArgs e)
        {
            IDNMcObject objectNode = (IDNMcObject)SelectedNode;
            try
            {
                uint key = (uint)objectNode.GetScheme().GetHashCode();
                SelectNodeInTreeNode(key);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetScheme", McEx);
            }
        }

        
        void ObjectLocation_Click(object sender, EventArgs e)
        {
            IDNMcObject mcObj = (IDNMcObject)((ToolStripMenuItem)sender).Tag;
            uint locationIndex = UInt32.Parse(((ToolStripMenuItem)sender).Text);

            try
            {
                Manager_MCObject.MoveToLocation((IDNMcObject)SelectedNode, locationIndex);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("MoveToLocation_Click", McEx);
            }
        }

        void JumpToObject_Click(object sender, EventArgs e)
        {
            IDNMcObject mcObj = (IDNMcObject)((ToolStripMenuItem)sender).Tag;

            IDNMcOverlay overlay = mcObj.GetOverlay();
            if (overlay != null)
            {
                uint overlayKey = (uint) overlay.GetHashCode();
                TreeNode overlayNode = GetNode(overlayKey);
                if (overlayNode != null)
                {
                    if (overlayNode.IsExpanded == false)
                        overlayNode.Expand();
                    SelectNodeInTreeNode((uint)mcObj.GetHashCode());
                }
            }
            
        }

        private void BuildOneObjectListOfScheme()
        {
            miJumpToObject.DropDownItems.Clear();
            IDNMcObjectScheme schemeNode = (IDNMcObjectScheme)SelectedNode;
            try
            {
                IDNMcObject[] objects = schemeNode.GetObjects();
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                miJumpToObject.Text = "Jump To Objects";

                if (objects != null && objects.Length > 0)
                {
                    IDNMcObject mcObj = objects[0];

                    toolStripMenuItem = new ToolStripMenuItem();
                    string text = Manager_MCNames.GetNameByObject(mcObj);
                    toolStripMenuItem.Text = text;
                    toolStripMenuItem.Tag = mcObj;
                    toolStripMenuItem.Click += JumpToObject_Click;
                    miJumpToObject.DropDownItems.Add(toolStripMenuItem);

                    if (objects.Length > 100)
                        miJumpToObject.Text = "Jump To Objects (until 100)";
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjects", McEx);
            }
        }


        private void BuildObjectListOfScheme()
        {
            miJumpToObject.DropDownItems.Clear();
            IDNMcObjectScheme schemeNode = (IDNMcObjectScheme)SelectedNode;
            try
            {
                IDNMcObject[] objects = schemeNode.GetObjects();
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                int counter = 0;
                if (objects != null && objects.Length > 0)
                {
                    foreach (IDNMcObject mcObj in objects)
                    {
                        if (counter++ == 100)
                            break;
                        
                        toolStripMenuItem = new ToolStripMenuItem();
                        string text = Manager_MCNames.GetNameByObject(mcObj);
                        toolStripMenuItem.Text = text;
                        toolStripMenuItem.Tag = mcObj;
                        toolStripMenuItem.Click += JumpToObject_Click;
                        bool isExist = false;
                        foreach (ToolStripMenuItem mi in miJumpToObject.DropDownItems)
                            if (mi.Text == text)
                                isExist = true;
                        if (!isExist)
                            miJumpToObject.DropDownItems.Add(toolStripMenuItem);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjects/GetOverlay", McEx);
            }
        }

        private void BuildObjectLocationListOfObject()
        {
            
            IDNMcObject mcObject = (IDNMcObject)SelectedNode;
            try
            {
                uint numLocations = mcObject.GetNumLocations();
                
                miMoveToLocation.DropDownItems.Clear();
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();

                if (numLocations > 1)
                {
                    for (int i = 0; i < numLocations; i++)
                    {
                        toolStripMenuItem = new ToolStripMenuItem();
                        toolStripMenuItem.Text = i.ToString();
                        toolStripMenuItem.Tag = mcObject;
                        toolStripMenuItem.Click += ObjectLocation_Click;
                       
                        miMoveToLocation.DropDownItems.Add(toolStripMenuItem);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetNumLocations", McEx);
            }
        }

        private void miShowPolygonLocationConditionalSelector_Click(object sender, EventArgs e)
        {
            //IDNMcOverlayManager OM = ((IDNMcConditionalSelector)SelectedNode).GetOverlayManager();
            IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
            DNSMcVector3D[] points = null;
            try
            {
                IDNMcLocationConditionalSelector m_CurrentCondSelector = (IDNMcLocationConditionalSelector)SelectedNode;
                points = m_CurrentCondSelector.GetPolygonPoints();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetPolygonPoints", McEx);
            }

            if(points != null)
            {
                DNEItemSubTypeFlags subTypeFlags = 0;
                subTypeFlags |= DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN;
                subTypeFlags |= DNEItemSubTypeFlags._EISTF_SCREEN;

                IDNMcObjectSchemeItem m_ObjSchemeItem = DNMcPolygonItem.Create(subTypeFlags,
                                                            DNELineStyle._ELS_SOLID,
                                                            DNSMcBColor.bcBlackOpaque,
                                                            3f,
                                                            null,
                                                            new DNSMcFVector2D(0, -1),
                                                            1f,
                                                            DNEFillStyle._EFS_NONE,
                                                            DNSMcBColor.bcWhiteOpaque,
                                                            null,
                                                            new DNSMcFVector2D(1, 1));

                IDNMcObject m_obj = DNMcObject.Create(activeOverlay,
                                                    m_ObjSchemeItem,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    points,
                                                    true);

            }
        }

        private void saveNodeInterfaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSaveSchemeComponentInterface frmSaveNodeInterface = new frmSaveSchemeComponentInterface();
            frmSaveNodeInterface.ShowDialog();
        }

        private void createSymologyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCreateSymbology createSymbology = new frmCreateSymbology(frmCreateSymbology.ActionType.CreateEmpty, (IDNMcOverlay)SelectedNode);
            createSymbology.Show();
        }

        private void createFromSymbologyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCreateSymbology createSymbology = new frmCreateSymbology(frmCreateSymbology.ActionType.CreateFrom, (IDNMcOverlay)SelectedNode);
            createSymbology.Show();
        }

        private void findObjectInTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_FindItemType = FindItemType.Object;
            this.Hide();
            MCTMapForm.OnMapClicked += new ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
        }

        void MCTMapForm_OnMapClicked(Point PointOnMap, DNSMcVector3D PointIn3D, DNSMcVector3D PointInImage, bool IsHasIntersection)
        {
            Point selectedPoint = PointOnMap;
            DNSMcVector3D pointItem = new DNSMcVector3D(selectedPoint.X, selectedPoint.Y, 0);
            try
            {
                DNSMcScanPointGeometry scanPointGeometry = new DNSMcScanPointGeometry(DNEMcPointCoordSystem._EPCS_SCREEN, pointItem, 0);

                DNSQueryParams queryParams = new DNSQueryParams();
                queryParams.eTargetsBitMask =  DNEIntersectionTargetType._EITT_OVERLAY_MANAGER_OBJECT | DNEIntersectionTargetType._EITT_VISIBLE_VECTOR_LAYER | DNEIntersectionTargetType._EITT_STATIC_OBJECTS_LAYER;
                queryParams.eTerrainPrecision = DNEQueryPrecision._EQP_DEFAULT;
                queryParams.uItemTypeFlagsBitField = 0;

                IDNMcMapViewport currViewport = MCTMapFormManager.MapForm.Viewport;
                uint hashCode = 0;
                MCTMapForm.OnMapClicked -= new ClickOnMapEventArgs(MCTMapForm_OnMapClicked);

                if (m_FindItemType == FindItemType.Object || m_FindItemType == FindItemType.Item)
                {
                    DNSTargetFound[] TargetFound = currViewport.ScanInGeometry(scanPointGeometry, false, queryParams);

                    if (TargetFound.Length > 0)
                    {

                        IDNMcObject SelectedObject = TargetFound[0].ObjectItemData.pObject;
                        IDNMcObjectSchemeItem SelectedItem = TargetFound[0].ObjectItemData.pItem;
                       
                        if (m_FindItemType == FindItemType.Object && SelectedObject != null)
                            hashCode = (uint)SelectedObject.GetHashCode();
                        else if (m_FindItemType == FindItemType.Item && SelectedItem != null)
                            hashCode = (uint)SelectedItem.GetHashCode();
                       

                    }
                }
                else   // OM
                {
                    if (MCTMapFormManager.MapForm.Viewport.OverlayManager != null)
                        hashCode = (uint)MCTMapFormManager.MapForm.Viewport.OverlayManager.GetHashCode();
                    else
                        MessageBox.Show("This viewport hasn't overlay manager", "Find Overlay Manager in Tree");
                }
                RefreshTree();
                SelectNodeInTreeNode(hashCode, -1, true);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ScanInGeometry", McEx);
            }
            this.Show();
        }

        private void findItemInTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_FindItemType = FindItemType.Item;
            this.Hide();
            MCTMapForm.OnMapClicked += new ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
        }

        private enum FindItemType
        {
           OM, Object, Item
        }


        private void AddNodes(TreeNode node)
        {
            if (node.Tag != null && ((TreeFormBaseClass)node.Tag).TemporaryObject != null)
            {
                object obj = ((TreeFormBaseClass)node.Tag).TemporaryObject;
                if (obj is IDNMcObjectScheme || obj is IDNMcConditionalSelector)
                    lstObj.Add(obj);
            }

            foreach (TreeNode treenode in node.Nodes)
            {
                AddNodes(treenode);
            }
        }

        private void MCOverlayMangerTreeViewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_TreeView.Nodes.Count > 0)
            {
                AddNodes(m_TreeView.Nodes[0]);
                foreach (object obj in lstObj)
                {
                    if (obj is IDNMcObjectScheme)
                    {
                        (obj as IDNMcObjectScheme).Dispose();
                    }
                    else if (obj is IDNMcConditionalSelector)
                    {
                        (obj as IDNMcConditionalSelector).Dispose();
                    }
                }
            }

            ReleaseShowCoord();

            //DisposeTreeObjects();
        }

        private void miJumpToObject_DropDownOpening(object sender, EventArgs e)
        {
            BuildObjectListOfScheme();
        }

        private void findOverlayManagerInTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_FindItemType = FindItemType.OM;
            this.Hide();
            MCTMapForm.OnMapClicked += new ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
        }
    }
}
