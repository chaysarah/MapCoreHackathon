using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;
using MCTester.Managers.ObjectWorld;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class frmConnectedList : Form
    {
        private IDNMcObjectScheme[] schemeList = null;
        private IDNMcObjectSchemeNode[] m_ArrSchemeNodes = null;
        private IDNMcSymbolicItem m_SymbolicItem = null;
        private IDNMcPhysicalItem m_PhysicalItem = null;
        private List<IDNMcObjectSchemeNode> m_lParents;

        public frmConnectedList(IDNMcObjectSchemeItem currItem)
        {
            if (currItem.GetNodeKind() == DNENodeKindFlags._ENKF_SYMBOLIC_ITEM)
                m_SymbolicItem = (IDNMcSymbolicItem)currItem;
            if (currItem.GetNodeKind() == DNENodeKindFlags._ENKF_PHYSICAL_ITEM)
                m_PhysicalItem = (IDNMcPhysicalItem)currItem;

            InitializeComponent();
            LoadList();
        }

        void lstSchemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSchemes.SelectedItem != null)
            {
                m_lParents = new List<IDNMcObjectSchemeNode>();
                IDNMcObjectScheme selectedScheme = schemeList[lstSchemes.SelectedIndex];   //(IDNMcObjectScheme)lstSchemes.Items[lstSchemes.SelectedIndex];
                m_ArrSchemeNodes = selectedScheme.GetNodes(DNENodeKindFlags._ENKF_ANY_NODE);

                if (m_SymbolicItem != null)
                    m_lParents.AddRange(m_SymbolicItem.GetParents());
                else
                    m_lParents.AddRange(m_PhysicalItem.GetParents());

                dgvParentNode.Rows.Clear();
                dgvParentNode.RowCount = m_ArrSchemeNodes.Length;

                for (int row = 0; row < dgvParentNode.RowCount; row++ )
                {
                    dgvParentNode[0, row].Value = Manager_MCNames.GetNameByObject(m_ArrSchemeNodes[row], m_ArrSchemeNodes[row].GetNodeType().ToString());
                    if (m_lParents.Contains(m_ArrSchemeNodes[row]))
                    {
                        dgvParentNode[1, row].Value = true;
                        dgvParentNode[2, row].Value = m_lParents.IndexOf(m_ArrSchemeNodes[row]);
                    }

                    if (m_ArrSchemeNodes[row] == m_SymbolicItem || m_ArrSchemeNodes[row] == m_PhysicalItem)
                        dgvParentNode[0, row].Style.Font = new Font(this.Font, FontStyle.Bold);
                }
            }
        }

        private void LoadList()
        {
            IDNMcMapViewport viewport = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport;
            IDNMcOverlayManager OM = viewport.OverlayManager;
            schemeList = Manager_MCObjectScheme.GetSchemesWithoutTempSchemes(OM.GetObjectSchemes()); 

            foreach (IDNMcObjectScheme currScheme in schemeList)
                lstSchemes.Items.Add(Manager_MCNames.GetNameByObject(currScheme, "ObjectScheme"));
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int parentsNum = 0;
            for (int row = 0; row < dgvParentNode.Rows.Count; row++ )
            {
                if (dgvParentNode[1, row].Value != null)
                    if ((bool)dgvParentNode[1, row].Value == true)
                        parentsNum++;
            }

            if (parentsNum > 0)
            {
                IDNMcObjectSchemeNode[] parentNodes = new IDNMcObjectSchemeNode[parentsNum];

                for (int row = 0; row < dgvParentNode.Rows.Count; row++)
                {
                    if (dgvParentNode[1, row].Value != null)
                    {
                        if ((bool)dgvParentNode[1, row].Value == true)
                        {
                            if (dgvParentNode[2, row].Value != null)
                            {
                                int parentIndex = int.Parse(dgvParentNode[2, row].Value.ToString());
                                if (parentIndex > parentNodes.Length - 1)
                                {
                                    MessageBox.Show(string.Concat("Invalid Parent Value in Row " + row.ToString()), "Error", MessageBoxButtons.OK);
                                    dgvParentNode.Rows[row].Selected = true;
                                    return;
                                }
                                parentNodes[parentIndex] = m_ArrSchemeNodes[row];
                            }
                            else
                                parentNodes[0] = m_ArrSchemeNodes[row];
                        }
                    }
                }

                DNEMcErrorCode eErrorStatus;

                if (m_SymbolicItem != null)
                {
                    try
                    {
                        if (chxCheckErrorStatusInsteadOfException.Checked)
                        {
                            m_SymbolicItem.Connect(parentNodes, out eErrorStatus);
                            if (eErrorStatus != DNEMcErrorCode.SUCCESS)
                            {
                                MessageBox.Show(string.Format("Connect failed,{0}", IDNMcErrors.ErrorCodeToString(eErrorStatus)), "IDNMcSymbolicItem Connect Operation", MessageBoxButtons.OK);
                            }
                            else
                            {
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }

                        }
                        else
                        {
                            m_SymbolicItem.Connect(parentNodes);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                             
                    }
                    catch (MapCoreException McEx)
                    {
                    	MapCore.Common.Utilities.ShowErrorMessage("IDNMcSymbolicItem.Connect", McEx);
                    }
                }
                else
                {
                    try
                    {
                        if (chxCheckErrorStatusInsteadOfException.Checked)
                        {
                            m_PhysicalItem.Connect(parentNodes[0], out eErrorStatus);
                            if (eErrorStatus != DNEMcErrorCode.SUCCESS)
                            {
                                MessageBox.Show(string.Format("Connect failed,{0}", IDNMcErrors.ErrorCodeToString(eErrorStatus)), "IDNMcPhysicalItem Connect Operation", MessageBoxButtons.OK);
                            }
                            else
                            {
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                        }
                        else
                        {
                            m_PhysicalItem.Connect(parentNodes[0]);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }

                        if (parentNodes.Length > 1)
                            MessageBox.Show("Mesh item can connect to one parent only!\nTherefore it will be connected to the first node that was chosen");
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("m_PhysicalItem.Connect", McEx);
                    }
                }                            
            } 
        }
    }
}