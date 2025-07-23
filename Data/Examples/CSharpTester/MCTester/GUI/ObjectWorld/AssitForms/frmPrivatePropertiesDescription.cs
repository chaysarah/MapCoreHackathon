using MapCore;
using MCTester.GUI.Trees;
using MCTester.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.Assit_Forms
{
    public partial class frmPrivatePropertiesDescription : Form
    {
        private IDNMcObjectScheme m_mcObjectScheme;
        private uint m_propertyId;
        private int m_colJumpToItem = 3;

        public frmPrivatePropertiesDescription(IDNMcObjectScheme mcObjectScheme, uint propertyId, string formMsg)
        {
            InitializeComponent();
            Text = formMsg;
            m_mcObjectScheme = mcObjectScheme;
            m_propertyId = propertyId;

            List<SPropertyDesc> sPropertyDescs = GetPrivatePropertiesDescription(mcObjectScheme, propertyId);
            int index = 0;
            foreach(SPropertyDesc desc in sPropertyDescs)
            {
                dgvPropertyDesc.Rows.Add(desc.propertyId, desc.propertyName, desc.propertyDesc);
                dgvPropertyDesc.Rows[index].Tag = desc.propertyNode;
                index++;
            }

        }

        public static List<SPropertyDesc> GetPrivatePropertiesDescription(IDNMcObjectScheme mcObjectScheme, uint propertyId)
        {
            List<SPropertyDesc> sPropertyDescs = new List<SPropertyDesc>();
            IDNMcObjectSchemeNode[] nodes = null;
            try
            {
                nodes = mcObjectScheme.GetNodesByPropertyID(propertyId);
            }
            catch (MapCoreException McEx)
            {
                //MapCore.Common.Utilities.ShowErrorMessage("GetNodesByPropertyID", McEx);
                throw McEx;
            }
            if (nodes != null)
            {
                foreach (IDNMcObjectSchemeNode node in nodes)
                {
                    string nodeType = "";
                    if (node is IDNMcObjectLocation)
                        nodeType = "MCTester.ObjectWorld.ObjectsUserControls.CtrlObjectLocation";
                    else
                        nodeType = GeneralFuncs.GetDirectInterfaceName(node.GetType()).Replace("IDNMc", "MCTester.ObjectWorld.ObjectsUserControls.uc");

                    Type PanelType = Type.GetType(nodeType);
                    object aPanel = Activator.CreateInstance(PanelType);
                    UserControl uc = (UserControl)aPanel;

                    MCTester.IUserControlItem userCtrlItem = (MCTester.IUserControlItem)uc;
                    userCtrlItem.LoadItem(node);

                    List<KeyValuePair<uint, string>> descs = Managers.Manager_MCPropertyDescription.GetProprtyDescByNodeAndId(node, propertyId);
                    string name = Managers.Manager_MCNames.GetNameByObject(node);
                    if (descs != null)
                    {
                        foreach (KeyValuePair<uint, string> desc in descs)
                        {
                            SPropertyDesc sPropertyDesc = new SPropertyDesc(propertyId, name, desc.Value, node);
                            sPropertyDescs.Add(sPropertyDesc);
                        }
                    }
                    else
                    {
                        SPropertyDesc sPropertyDesc = new SPropertyDesc(propertyId, name, "", node);
                        sPropertyDescs.Add(sPropertyDesc);
                    }
                }
            }

            return sPropertyDescs;
        }

        private void dgvPropertyDesc_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgvPropertyDesc.Rows[e.RowIndex].Cells[m_colJumpToItem].Value = "Jump To Node";
        }

        private void dgvPropertyDesc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == m_colJumpToItem && dgvPropertyDesc.Rows[e.RowIndex].Tag != null)
            {
                MCObjectWorldTreeViewForm overlayMangerTreeViewForm = MainForm.ShowObjectWorldForm();
                overlayMangerTreeViewForm.RefreshTree();

                uint hashcode = (uint)dgvPropertyDesc.Rows[e.RowIndex].Tag.GetHashCode();
                overlayMangerTreeViewForm.SelectNodeInTreeNode(hashcode, true);

            }
        }
    }

    public class SPropertyDesc
    {
        public uint propertyId;
        public string propertyName;
        public string propertyDesc;
        public IDNMcObjectSchemeNode propertyNode;

        public SPropertyDesc(uint _propertyId, string _propertyName)
        {
            propertyId = _propertyId;
            propertyName = _propertyName;
        }

        public SPropertyDesc(uint _propertyId, string _propertyName, string _propertyDesc, IDNMcObjectSchemeNode _propertyNode)
        {
            propertyId = _propertyId;
            propertyName = _propertyName;
            propertyDesc = _propertyDesc;
            propertyNode = _propertyNode;
        }
    }

}

    