using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.Assit_Forms
{
    public partial class frmSortedParentList : Form
    {
        IDNMcObjectSchemeNode m_SchemeNode;
        IDNMcObjectSchemeNode[] m_Parents;

        public frmSortedParentList(IDNMcObjectSchemeNode schemeNode)
        {
            InitializeComponent();
            m_SchemeNode = schemeNode;
        }

        private void frmSortedParentList_Load(object sender, EventArgs e)
        {
            try
            {
                m_Parents = m_SchemeNode.GetParents();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetParents", McEx);
            }

            dgvSortedParents.RowCount = m_Parents.Length;
            for(int row=0; row<m_Parents.Length; row++)
            {

                dgvSortedParents[0, row].Value = row;
                dgvSortedParents[1, row].Value = "(" + m_Parents[row].GetHashCode().ToString() + ") " + m_Parents[row].GetNodeType().ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
