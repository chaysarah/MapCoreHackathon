using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCTester.Managers.ObjectWorld;
using MCTester.ObjectWorld.ObjectsUserControls;
using UnmanagedWrapper;
using MapCore;
using static MCTester.MainForm;

namespace MCTester.Controls
{
    public partial class CtrlTextureButtons : UserControl
    {
        public delegate void delegateUpdateTextureData();
        private delegateUpdateTextureData m_delegateTextureData;

        private CreateTextureForm m_frmCreatedTexture;
        private IDNMcTexture m_Texture = null;


        public CtrlTextureButtons()
        {
            InitializeComponent();
            ChangeButtonsEnabled();
        }

        public void ChangeButtonsEnabled()
        {
           btnRecreate.Enabled = btnEdit.Enabled = btnDelete.Enabled = m_Texture != null;
        }

        public void SetUpdateDelegate(delegateUpdateTextureData _delegateUpdateTextureData)
        {
            m_delegateTextureData = _delegateUpdateTextureData;
        }

        public enum ObjectPropertiesBase_TextureField
        {
            DefaultTexture,
            LineTexture,
            FillTexture,
            SidesFillTexture
        }

        public ObjectPropertiesBase_TextureField objectPropertiesBase_TextureField
        {
            set;get;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            OpenTextureForm(FontTextureSourceForm.CreateDialog, null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("The update will be performed in-place influencing other properties using the same texture and canceling use-existing option.Re - creating should be used to update this property only and preserve use - existing option.Are you sure you want to continue updating in-place ? ", "In -place Update", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                OpenTextureForm(FontTextureSourceForm.Update, m_Texture);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            m_Texture = null;
            ChangeButtonsEnabled();
            if (m_delegateTextureData != null)
                m_delegateTextureData();
        }

        public void SetTexture(IDNMcTexture texture)
        {
            m_Texture = texture;
            ChangeButtonsEnabled();
        }

        public IDNMcTexture GetTexture()
        {
            return m_Texture;
        }

        private void OpenTextureForm(FontTextureSourceForm fontTextureSource, IDNMcTexture mcTexture)
        {
            m_frmCreatedTexture = new CreateTextureForm(fontTextureSource, mcTexture);
            m_frmCreatedTexture.TopMost = true;
            if (m_frmCreatedTexture.ShowDialog() == DialogResult.OK)
            {
                m_Texture = m_frmCreatedTexture.CurrentTexture;
                ChangeButtonsEnabled();
                if (m_delegateTextureData != null)
                    m_delegateTextureData();
            }
        }

        private void btnRecreate_Click(object sender, EventArgs e)
        {
            OpenTextureForm(FontTextureSourceForm.Recreate, m_Texture);
        }
    }
}
