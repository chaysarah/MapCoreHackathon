using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;
using MapCore;

namespace MCTester.GUI.Forms
{
    public partial class ExistingMeshListForm : Form
    {
        private DNEMeshType m_MeshType;
        private IDNMcMesh m_SelectedMesh;

        public ExistingMeshListForm(DNEMeshType meshType)
        {
            InitializeComponent();
            m_MeshType = meshType;
        }

        private void ExistingMeshListForm_Load(object sender, EventArgs e)
        {
            lstMeshFiles.Items.Clear();

            Dictionary<IDNMcMesh, uint> mesh = Managers.ObjectWorld.Manager_MCMesh.dMesh;
            uint meshTypeNum;
            foreach (IDNMcMesh key in mesh.Keys)
            {
                mesh.TryGetValue(key, out meshTypeNum);
                if (meshTypeNum == (uint)m_MeshType)
                {
                    lstMeshFiles.Items.Add(key);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstMeshFiles.SelectedItem != null)
            {
                m_SelectedMesh = (IDNMcMesh)lstMeshFiles.SelectedItem;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("You have to choose Mesh", "MCTester", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public IDNMcMesh SelectedMesh
        {
            get
            {
                return (IDNMcMesh)lstMeshFiles.SelectedItem;
            }
            set
            {
                m_SelectedMesh = value;
            }
        }

        private void btnDeleteMesh_Click(object sender, EventArgs e)
        {
            if (lstMeshFiles.SelectedItem != null)
            {
                Managers.ObjectWorld.Manager_MCMesh.RemoveFromDictionary((IDNMcMesh)lstMeshFiles.SelectedItem);
                ((IDNMcMesh)lstMeshFiles.SelectedItem).Dispose();
                lstMeshFiles.Items.RemoveAt(lstMeshFiles.SelectedIndex);
            }
        }
    }
}