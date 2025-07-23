using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.GUI.PC.GUI.Forms
{
    public partial class ExistingTextureListForm : Form
    {
        DNETextureType m_TextuerType;
        IDNMcTexture m_SelectedTexture;

        public ExistingTextureListForm(DNETextureType TextureType)
        {
            InitializeComponent();
            m_TextuerType = TextureType;

        }

        public IDNMcTexture SelectedTexture
        {
            get
            {
                return (IDNMcTexture)lstTextures.SelectedItem;
            }
            set
            {
                m_SelectedTexture = value;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstTextures.SelectedItem != null)
            {
                m_SelectedTexture = (IDNMcTexture)lstTextures.SelectedItem;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("You have to choose texture", "MCTester", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ExistingTextureListForm_Load(object sender, EventArgs e)
        {
            lstTextures.Items.Clear();

            Dictionary<IDNMcTexture, uint> textures = Managers.ObjectWorld.Manager_MCTextures.dTextures;
            uint textureTypeNum;
            foreach (IDNMcTexture key in textures.Keys)
            {
                textures.TryGetValue(key, out textureTypeNum);
                if (textureTypeNum == (uint)m_TextuerType)
                {
                    lstTextures.Items.Add(key);
                }
            }
        }

        private void btnDeleteTexture_Click(object sender, EventArgs e)
        {
            if (lstTextures.SelectedItem != null)
            {
                Managers.ObjectWorld.Manager_MCTextures.RemoveFromDictionary((IDNMcTexture)lstTextures.SelectedItem);
                ((IDNMcTexture)lstTextures.SelectedItem).Dispose();
                lstTextures.Items.RemoveAt(lstTextures.SelectedIndex);
                
            }
        }

    }
}