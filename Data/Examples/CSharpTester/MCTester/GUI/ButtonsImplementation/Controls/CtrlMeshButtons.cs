using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCTester.General_Forms;
using MapCore;
using static MCTester.MainForm;
using MCTester.ObjectWorld.ObjectsUserControls;

namespace MCTester.Controls
{
    public partial class CtrlMeshButtons : UserControl
    {
        private IDNMcMesh mMcMesh;
        public delegate void delegateUpdateMeshData();
        private delegateUpdateMeshData m_delegateMeshData;

        public void SetMesh(IDNMcMesh mcMesh)
        {
            mMcMesh = mcMesh;
            SetMeshButtonEnabled();
        }


        public CtrlMeshButtons()
        {
            InitializeComponent();
            SetMeshButtonEnabled();
        }
        public IDNMcMesh GetMesh()
        {
            return mMcMesh;
        }

        public void SetUpdateDelegate(delegateUpdateMeshData _delegateUpdateMeshData)
        {
            m_delegateMeshData = _delegateUpdateMeshData;
        }

        private void btnCreateMesh_Click(object sender, EventArgs e)
        {
            ChangeMesh(FontTextureSourceForm.CreateDialog, null);
        }

        private void btnUpdateMesh_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("The update will be performed in-place influencing other properties using the same mesh and canceling use-existing option.Re - creating should be used to update this property only and preserve use - existing option.Are you sure you want to continue updating in-place ? ", "In-place Update", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                ChangeMesh(FontTextureSourceForm.Update, mMcMesh);
        }

        private void btnDeleteMesh_Click(object sender, EventArgs e)
        {
            mMcMesh = null;
            SetMeshButtonEnabled();
            if (m_delegateMeshData != null)
                m_delegateMeshData();
        }

        private void btnRecreateMesh_Click(object sender, EventArgs e)
        {
            ChangeMesh(FontTextureSourceForm.Recreate, mMcMesh);
        }

        private void ChangeMesh(FontTextureSourceForm MeshTextureSource, IDNMcMesh mcMesh = null)
        {
            frmCreateMesh MeshDlg = new frmCreateMesh(MeshTextureSource, mcMesh);

            // Show the dialog
            if (MeshDlg.ShowDialog() == DialogResult.OK)
            {
                // Set the new Mesh
                mMcMesh = MeshDlg.CurrentMesh;
                if (m_delegateMeshData != null)
                    m_delegateMeshData();
            }
            SetMeshButtonEnabled();
        }

        public void SetMeshButtonEnabled()
        {
            btnDeleteMesh.Enabled = btnUpdateMesh.Enabled = btnRecreateMesh.Enabled = mMcMesh != null;
        }
    }
}
