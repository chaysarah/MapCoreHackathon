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

namespace MCTester.MapWorld.MapUserControls
{
    public partial class frmTerrainList : Form
    {
        private IDNMcMapViewport m_Viewport;
        private IDNMcMapTerrain m_SelectedTerrain;
        private List<string> m_lstTerrainText = new List<string>();
        private List<IDNMcMapTerrain> m_lstTerrainValue = new List<IDNMcMapTerrain>();

        public frmTerrainList(IDNMcMapViewport viewport)
        {
            InitializeComponent();
            m_Viewport = viewport;
            LoadForm();
        }

        private void LoadForm()
        {
            IDNMcMapTerrain[] terrains = m_Viewport.GetTerrains();

            foreach (IDNMcMapTerrain terr in terrains)
            {
                m_lstTerrainText.Add(Manager_MCNames.GetNameByObject(terr, "Terrain"));
                m_lstTerrainValue.Add(terr);
            }

            lstTerrains.Items.AddRange(m_lstTerrainText.ToArray());  
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedTerrain = (IDNMcMapTerrain)m_lstTerrainValue[lstTerrains.SelectedIndex];

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public IDNMcMapTerrain SelectedTerrain
        {
            get { return m_SelectedTerrain; }
            set { m_SelectedTerrain = value; }
        }
    }
}