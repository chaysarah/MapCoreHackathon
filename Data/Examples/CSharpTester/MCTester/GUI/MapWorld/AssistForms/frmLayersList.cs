using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class frmLayersList : Form
    {
        private IDNMcMapTerrain m_Terrain;
        private IDNMcMapLayer[] m_SelectedLayers;

        public frmLayersList(IDNMcMapTerrain terrain)
        {
            InitializeComponent();
            m_Terrain = terrain;
            LoadForm();
        }

        private void LoadForm()
        {
            IDNMcMapLayer[] layers = m_Terrain.GetLayers();

            foreach (IDNMcMapLayer layer in layers)
            {
                lstLayers.Items.Add(layer);
            }
        }

        public IDNMcMapLayer[] SelectedLayers
        {
            get { return m_SelectedLayers; }
            set { m_SelectedLayers = value; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedLayers = new IDNMcMapLayer[lstLayers.SelectedItems.Count];
            if (lstLayers.SelectedItems.Count > 0)
                lstLayers.SelectedItems.CopyTo(SelectedLayers, 0);
            //SelectedLayers = (IDNMcMapLayer[])lstLayers.SelectedItems.CopyTo(SelectedLayers,0);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}