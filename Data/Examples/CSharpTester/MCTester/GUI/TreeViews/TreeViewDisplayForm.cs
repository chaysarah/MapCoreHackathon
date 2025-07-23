using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MCTester;
using MCTester.GUI;
using System.Reflection;
using MCTester.Managers;
using MapCore;
using UnmanagedWrapper;
using MCTester.ObjectWorld.ObjectsUserControls;
using MCTester.MapWorld.MapUserControls;
using MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers.MapWorld;
using MCTester.General_Forms;
using System.Linq;
using MapCore.Common;

namespace MCTester.GUI.Trees
{
    public partial class TreeViewDisplayForm : Form
    {
        //protected System.Windows.Forms.ToolStripMenuItem tsmi_RenameMCNames;
        protected System.Windows.Forms.ToolStripMenuItem tsmi_Rename;
        protected System.Windows.Forms.ToolStripMenuItem tsmi_DeleteName;
        protected System.Windows.Forms.ToolStripMenuItem tsmi_ExpandAll;
        protected System.Windows.Forms.ToolStripMenuItem tsmi_Collapse;
           
        protected System.Windows.Forms.ToolStripSeparator ts_separator1;
        protected System.Windows.Forms.ToolStripSeparator ts_separator2;

        private TreeFormBaseClass m_treeDefinition;
        private SaveFileDialog SFD;
        private OpenFileDialog OFD;
        protected object m_NodeClickedType;
                      
        public TreeViewDisplayForm()
        {
            InitializeComponent();
            MCTester.Controls.ParentChildManagerControl.OnRefreshItem += new MCTester.Controls.OnRefreshItemEventArgs(ParentChildManagerControl_OnRefreshItem);
            SFD = new SaveFileDialog();
            OFD = new OpenFileDialog();

            tsmi_Rename = new ToolStripMenuItem();
            this.tsmi_Rename.Name = "tsmi_Rename";
            this.tsmi_Rename.Text = "Rename";
            this.tsmi_Rename.Click += new System.EventHandler(this.tsmi_Rename_Click);

            tsmi_DeleteName = new ToolStripMenuItem();
            this.tsmi_DeleteName.Name = "tsmi_DeleteName";
            this.tsmi_DeleteName.Text = "Delete Name";
            this.tsmi_DeleteName.Click += new System.EventHandler(this.tsmi_DeleteName_Click);

            tsmi_ExpandAll = new ToolStripMenuItem();
            this.tsmi_ExpandAll.Name = "tsmi_ExpandAll";
            this.tsmi_ExpandAll.Text = "Expand";
            this.tsmi_ExpandAll.Click += new System.EventHandler(this.tsmi_ExpandAll_Click);

            tsmi_Collapse = new ToolStripMenuItem();
            this.tsmi_Collapse.Name = "tsmi_Collapse";
            this.tsmi_Collapse.Text = "Collapse";
            this.tsmi_Collapse.Click += new System.EventHandler(this.tsmi_Collapse_Click);

            ts_separator1 = new ToolStripSeparator();
            ts_separator2 = new ToolStripSeparator();

            
         
        }

        protected void SetToolStripMenuItems(ContextMenuStrip contextMenuStrip, bool isAddRename, bool isAddExpand, bool isAddDeleteName = true)
        {
            if(isAddRename)
            {
                contextMenuStrip.Items.Add(ts_separator1);
                contextMenuStrip.Items.Add(tsmi_Rename);
            }
            if(isAddDeleteName)
                contextMenuStrip.Items.Add(tsmi_DeleteName);

            if (isAddExpand)
            {
                contextMenuStrip.Items.Add(ts_separator2);
                contextMenuStrip.Items.Add(tsmi_ExpandAll);
                contextMenuStrip.Items.Add(tsmi_Collapse);
            }
        }

        protected void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            SetToolStripMenuItems(sender as ContextMenuStrip, false,true);
        }

        protected void ContextMenuStripWithoutNames_Opening(object sender, CancelEventArgs e)
        {
            SetToolStripMenuItems(sender as ContextMenuStrip, false, true, false);
        }

        protected void ContextMenuStripWithRename_Opening(object sender, CancelEventArgs e)
        {
            SetToolStripMenuItems(sender as ContextMenuStrip, true, true);
        }

        protected void ContextMenuStripWithoutExpand_Opening(object sender, CancelEventArgs e)
        {
            SetToolStripMenuItems(sender as ContextMenuStrip, false, false);
        }

        private void tsmi_Rename_Click(object sender, EventArgs e)
        {
            Rename();
        }


        private void tsmi_DeleteName_Click(object sender, EventArgs e)
        {
            DeleteName();
        }

        private void tsmi_ExpandAll_Click(object sender, EventArgs e)
        {
            ExpandAllNode();
        }

        private void tsmi_Collapse_Click(object sender, EventArgs e)
        {
            Collapse();
        }

        protected void ExpandAllNode()
        {
            if (m_TreeView != null && m_TreeView.SelectedNode != null)
                m_TreeView.SelectedNode.ExpandAll();
        }

        protected void Collapse()
        {
            if (m_TreeView != null && m_TreeView.SelectedNode != null)
                m_TreeView.SelectedNode.Collapse();
        }

        public TreeNode GetSelectedNode()
        {
            return m_TreeView.SelectedNode;
        }

        void ParentChildManagerControl_OnRefreshItem(object ItemToRefresh)
        {
            TreeNode SelectedTN = m_TreeView.SelectedNode;
            CreateTree();
            ReSelectTreeNode(SelectedTN, m_TreeView.Nodes[0] /*root Node*/);
            m_TreeView.ExpandAll();
        }

        public void ReSelectTreeNode(TreeNode TnToFind, TreeNode CurrentTreeNode)
        {
            if ((TreeFormBaseClass)TnToFind.Tag != null)
            {
                object ObjectToCompare = ((TreeFormBaseClass)TnToFind.Tag).TemporaryObject;

                if (CurrentTreeNode.Tag != null && ObjectToCompare != null &&
                    (CurrentTreeNode.Tag is TreeFormBaseClass) &&
                    (CurrentTreeNode.Tag as TreeFormBaseClass).TemporaryObject != null)
                {
                    object NewObj = (CurrentTreeNode.Tag as TreeFormBaseClass).TemporaryObject;
                    if (NewObj.Equals(ObjectToCompare))
                    {
                        m_TreeView.SelectedNode = CurrentTreeNode;
                        return;
                    }
                }
            }
            foreach (TreeNode TreeN in CurrentTreeNode.Nodes)
            {
                ReSelectTreeNode(TnToFind, TreeN);
            }
        }

        public virtual TreeFormBaseClass TreeDefinitionClass
        {
            get { return this.m_treeDefinition; }
            set { this.m_treeDefinition = value; }
        }

        public void CreateTree()
        {
            if (!m_TreeView.IsDisposed)
                m_TreeView.Nodes.Clear();
            
            m_TreeView.Nodes.Add("Root");

            //Clear the panel after rebuilding tree
            m_splitter.Panel2.Controls.Clear();
            
        }

        private string[] getTokens(string classString)
        {
            char[] delimeters = new char[1];
            delimeters[0] = '.';
            string[] ret = classString.Split(delimeters);
            return ret;
        }

        private void GetPropertyInfo(object StartObject,Type ObjectType, string FullClassString, out PropertyInfo PI, out object Obj)
        {
            string[] SubProperties = getTokens(FullClassString);
            PropertyInfo ret = null;
            Type currType = ObjectType;
            object FoundObject = StartObject;

            foreach (string currentProp in SubProperties)
            {
                ret = currType.GetProperty(currentProp);
                if (ret !=null)
                {
                    currType = ret.PropertyType;
                    FoundObject = ret.GetValue(FoundObject,null);
                }
            }

            PI = ret;
            Obj = FoundObject;
        }

        protected void HandleFirstTreeElement(TreeFormBaseClass cls, TreeNode CurrentNode)
        {
            Type clsType = Type.GetType(cls.SourceDataArray);
            MethodInfo method = clsType.GetMethod("get_AllParams");// BindingFlags.Static); 
            object methodResult = method.Invoke(null, null);

            Dictionary<object, uint> objects = (Dictionary<object, uint>)methodResult;
            foreach (object key in objects.Keys)
			{
                TreeFormBaseClass NewCls = new TreeFormBaseClass(cls);

                StringBuilder modifiedString = Manager_MCNames.GetType(key);
               
                string ChildName = Manager_MCNames.GetNameByObject(key, modifiedString.ToString()) ;
				TreeNode NewTreeNode = new TreeNode(ChildName);

                NewCls.TemporaryObject = key;
                NewTreeNode.Tag = NewCls;

                NewTreeNode.Nodes.Add(new DummyNode(key));
 
				CurrentNode.Nodes.Add(NewTreeNode);
			}
        }

		private void HandleSecondLevelObject(TreeFormBaseClass cls, TreeNode CurrentNode, object parent)
		{
            if (cls.SourceDataArray == "")
                return;

            object[] paramsArr = new object[] { parent };
            Type clsType = Type.GetType(cls.SourceDataArray);
            MethodInfo method = clsType.GetMethod("GetChildren");
            object methodResult = method.Invoke(null, paramsArr);
            Dictionary<object, uint> objects = (Dictionary<object, uint>)methodResult;

            foreach (object key in objects.Keys)
			{
                StringBuilder modifiedString = Manager_MCNames.GetType(key);

                string ChildName = Manager_MCNames.GetNameByObject(key,  modifiedString.ToString());
				TreeNode NewTreeNode = new TreeNode(ChildName);

                TreeFormBaseClass TreeElementChild = cls.GetChildThatHandlesType(key.GetType());
                TreeFormBaseClass NewCls = new TreeFormBaseClass(TreeElementChild);
                if (TreeElementChild != null &&  NewCls != null)
				{
                    NewCls.TemporaryObject = key;
                    NewTreeNode.Tag = NewCls;
                    NewTreeNode.Nodes.Add(new DummyNode(key));
				}
				else
				{
					MessageBox.Show("unrecognized type:" + key.GetType().ToString());
				}
                CurrentNode.Nodes.Add(NewTreeNode);
			}
		}

        private IManagersGetter UpdateGetter(PropertyInfo pi)
        {
            return UpdateGetter(pi);
        }

		private IManagersGetter UpdateGetter(object obj,PropertyInfo pi)
        {
			IManagersGetter Getter = null;
			if (pi != null)
			{
                object PropValue = pi.GetValue(obj, null);
				Getter = (IManagersGetter)PropValue;
			}
			return Getter;
        }

        protected virtual void m_TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            /*if (e.Button == MouseButtons.Right && e.Node.Text == "Root")
            {
                cmsRoot.Show(m_TreeView, e.Location);
            }*/

            //Automatically select the node at right click event occur 
            m_TreeView.SelectedNode = e.Node;
            TreeFormBaseClass NodeBaseInfo = null;

            if (e.Button == MouseButtons.Left)
            {

                foreach (Control ctrl in m_splitter.Panel2.Controls)
                {
                    ctrl.Dispose();
                }
                m_splitter.Panel2.Controls.Clear();

                if (e.Node.Tag == null)
                    return;

                m_TreeView.SelectedNode = e.Node;

                NodeBaseInfo = (TreeFormBaseClass)e.Node.Tag;
                string CreateThisPanel = "";
                Type type = GeneralFuncs.GetDirectInterface(NodeBaseInfo.TemporaryObject.GetType());
                if (type != null && NodeBaseInfo.HandlerPanelType.ContainsKey(type))
                    CreateThisPanel = NodeBaseInfo.HandlerPanelType[type];
                else
                    return;

                Type PanelType = Type.GetType(CreateThisPanel);
                object aPanel = Activator.CreateInstance(PanelType);
                UserControl uc = (UserControl)aPanel;

                MCTester.IUserControlItem userCtrlItem = (MCTester.IUserControlItem)uc;
                userCtrlItem.LoadItem(NodeBaseInfo.TemporaryObject);

                if (!m_splitter.IsHandleCreated)
                {
                    IntPtr handle = m_splitter.Handle; // Forces handle creation
                }

                if (m_splitter.IsHandleCreated)
                    m_splitter.Panel2.Controls.Add(uc);

                uc.Dock = DockStyle.Fill;
            }
            //if (e.Button == MouseButtons.Right)
            //{
            //    TreeFormBaseClass NodeBaseInfo = (TreeFormBaseClass)e.Node.Tag;
            //    if (NodeBaseInfo == null)
            //        m_NodeClickedType = "Item type not existing";
            //    else
            //        m_NodeClickedType = NodeBaseInfo.TemporaryObject;
            //}

            //TMP
            NodeBaseInfo = (TreeFormBaseClass)e.Node.Tag;
            if (NodeBaseInfo == null)
                m_NodeClickedType = "Item type not existing";
            else
                m_NodeClickedType = NodeBaseInfo.TemporaryObject;

        }

        protected void m_TreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode tn = e.Node;
            TreeFormBaseClass cls = null;

            if (tn.Tag != null &&
                tn.Tag is TreeFormBaseClass)
            {
                cls = (TreeFormBaseClass)tn.Tag;
                object aKey = null;

                if (tn.Nodes[0] is DummyNode)
                {
                    aKey = ((DummyNode)tn.Nodes[0]).ObjectKey;
                    tn.Nodes.RemoveAt(0);
                    HandleSecondLevelObject(cls, tn, aKey);
                }

                
            }
            // else this is the root or some error has occurred (maybe the node is not mapped on schema)

        }

        private void m_TreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            //TreeFormBaseClass cls = new TreeFormBaseClass(m_mainForm);

            //cls.TemporaryObject = e.Node.Tag; 

            ////clear all node tree sub items
            //e.Node.Nodes.Clear();
                        
            //e.Node.Nodes.Add(new DummyNode(cls.TemporaryObject));

        }

        public object SelectedNode
        {
            get
            {
                TreeFormBaseClass nodeTag = (TreeFormBaseClass)m_TreeView.SelectedNode.Tag;
                object node = nodeTag.TemporaryObject;
                return node;
            }
        }

        public object GetMcObjFromNode(TreeNode node)
        {
            TreeFormBaseClass nodeTag = (TreeFormBaseClass)node.Tag;
            if (nodeTag != null)
                return nodeTag.TemporaryObject;
            return null;
        }

        private void TreeViewDisplayForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CausesValidation = false;
        }

        public TreeNode GetNode(uint hashCode, TreeNode rootNode, bool isExpand = false)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                bool IsExpanded = node.IsExpanded;
                if (node.Tag != null && ((TreeFormBaseClass)node.Tag).TemporaryObject.GetHashCode() == hashCode)
                    return node;
                if (!IsExpanded && isExpand)
                    node.Expand();
                TreeNode next = GetNode(hashCode, node, isExpand);
                
                if (next != null)
                    return next;

                // collapse node
                if (!IsExpanded && node.IsExpanded)
                    node.Collapse();
            }
            return null;
        }

        public TreeNode GetNode(uint hashCode, bool isExpand = false)
        {

            TreeNode itemNode = null;
            foreach (TreeNode node in m_TreeView.Nodes)
            {
                bool IsExpanded = node.IsExpanded;
                
                if (node.Tag != null && ((TreeFormBaseClass)node.Tag).TemporaryObject.GetHashCode() == hashCode)
                    return node;
                if (!IsExpanded && isExpand)
                    node.Expand();
                itemNode = GetNode(hashCode, node, isExpand);
              
                if (itemNode != null)
                    break;

                // collapse node
                if(!IsExpanded && node.IsExpanded)
                    node.Collapse();
            }
            return itemNode;
        }

        protected void RefreshTree(TreeNode node ,bool isSelectLastNode = false, int indexTabToSelect = -1)
        {
            if (isSelectLastNode)
            {
                ExpendTree();
                if (node != null && node.Tag != null && ((TreeFormBaseClass)node.Tag).TemporaryObject != null)
                {
                    uint hashCode = (uint)((TreeFormBaseClass)node.Tag).TemporaryObject.GetHashCode();
                    SelectNodeInTreeNode(hashCode,indexTabToSelect, true );
                    TreeNode selectedNode = GetNode(hashCode);
                    if (selectedNode != null)
                        selectedNode.Expand();
                }
            }
            else
                ExpendTree();
        }

        public void ExpendTree(int level = 1)
        {
            m_TreeView.Nodes[0].Expand();

            foreach (TreeNode node in m_TreeView.Nodes[0].Nodes)
            {
                node.Expand();
                if (level == 2)
                {
                    foreach (TreeNode childnode in node.Nodes)
                    {
                        childnode.Expand();
                    }
                }
            }
        }

        public void SelectNodeInTreeNode(uint hashCodeKey, bool isExpand)
        {
            SelectNodeInTreeNode(hashCodeKey, -1, isExpand);
        }

        public void SelectNodeInTreeNode(uint hashCodeKey, int indexTabToSelect = -1, bool isExpand = false)
        {
            this.Focus();
            TreeNode node = GetNode(hashCodeKey, isExpand);
            if (node != null)
            {
                node.Expand();
                m_TreeView_NodeMouseClick(null, new TreeNodeMouseClickEventArgs(node, MouseButtons.Left, 1, 0, 0));
                if(indexTabToSelect >= 0 &&  ActiveControl is SplitContainer)
                {
                    Control control = (ActiveControl as SplitContainer).Panel2.Controls[0];
                    if (control != null)
                    {
                        TabControl tabControl = null;
                        foreach (Control ctrl in control.Controls)
                        {
                            if (ctrl is TabControl)
                            {
                                tabControl = (TabControl)ctrl;
                                break;
                            }
                        }

                        if (tabControl != null && tabControl.TabPages != null && tabControl.TabPages.Count > indexTabToSelect)
                        {
                            tabControl.SelectTab(indexTabToSelect);
                        }
                    }
                }
            }
        }

        private void tsmiExpandAll_Click(object sender, EventArgs e)
        {
            if (m_TreeView != null)
                m_TreeView.ExpandAll();
        }

        private void tsmiCollapseAll_Click(object sender, EventArgs e)
        {
            if (m_TreeView != null)
                m_TreeView.CollapseAll();
        }

        protected void Rename()
        {
            ChangingName changingName = new ChangingName(SelectedNode);
            if (changingName.ShowDialog() == DialogResult.OK)
            {
                m_TreeView.SelectedNode.Text = changingName.FullName;
            }
        }

        protected void RenameMCNames(string name)
        {
            ChangingName changingName = new ChangingName(SelectedNode, name);
            if (changingName.ShowDialog() == DialogResult.OK)
            {
                m_TreeView.SelectedNode.Text = changingName.FullName;
            }
        }

        public void SetName(object obj)
        {
            m_TreeView.SelectedNode.Text = Manager_MCNames.GetNameByObject(obj);
        }

        protected void DeleteName()
        {
            if (Manager_MCNames.RemoveName(SelectedNode))
                m_TreeView.SelectedNode.Text = Manager_MCNames.GetDefualtName(SelectedNode);
        }

        private void TreeViewDisplayForm_Load(object sender, EventArgs e)
        {
          
        }
    }

    public class DummyNode:TreeNode
    {
        private object m_obj;

        public object ObjectKey
        {
            get { return m_obj; }
            set { m_obj = value; }
        }

        public DummyNode(object ObjectKey)
        {
            this.ObjectKey = ObjectKey;
        }
    };

    public delegate void OnRefreshRequiredEventArgs();
    public static class TreeViewFormCommands
    {
        public static event OnRefreshRequiredEventArgs OnRefreshRequired;
        public static void RefreshTreeView()
        {
            if (OnRefreshRequired!=null)
            {
                OnRefreshRequired();
            }
        }

    }

}