using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using MCTester.MapWorld.WizardForms;
using MCTester.Managers;
using MCTester.Managers.ObjectWorld;
using MapCore.Common;
using System.Linq;

namespace MCTester.GUI.Forms
{
    public partial class PreStandaloneSQForm : Form
    {
        //private DNSQueryParams m_queryParams;
        private IDNMcMapViewport m_currentViewport;
        private List<IDNMcMapTerrain> terrainList;
        private List<string> m_lstTerrainText = new List<string>();
        private List<IDNMcMapTerrain> m_lstTerrainValue = new List<IDNMcMapTerrain>();

        public PreStandaloneSQForm(IDNMcMapViewport currentViewport)
        {
            InitializeComponent();
            LoadItem();

            m_currentViewport = currentViewport;
           
        }

        public void LoadItem()
        {
            lstTerrains.Items.Clear();

            terrainList = Manager_MCTerrain.AllTerrains();
            int terrainLength = terrainList.Count;

            foreach (IDNMcMapTerrain terr in terrainList)
            {
                m_lstTerrainText.Add(Manager_MCNames.GetNameByObject(terr, "Terrain"));
                m_lstTerrainValue.Add(terr);
            }

            lstTerrains.Items.AddRange(m_lstTerrainText.ToArray());

            ctrlLayers1.SetEnabled(true);
            ctrlLayers1.SetIsFilterDTMType(true);
        }



        private void btnCreateStandaloneSQ_Click(object sender, EventArgs e)
        {
            List<IDNMcMapTerrain> selectedTerrains = new List<IDNMcMapTerrain>();

            foreach (int index in lstTerrains.SelectedIndices)
            {
                selectedTerrains.Add(m_lstTerrainValue[index]);
            }

            try
            {
                IDNMcDtmMapLayer[] apQuerySecondaryDtmLayers = Manager_MCLayers.GetLayersAsDTM(ctrlLayers1.GetLayers());
                IDNMcSpatialQueries m_currSQ = DNMcSpatialQueries.Create(ucCreateData.CreateData, selectedTerrains.ToArray(), apQuerySecondaryDtmLayers);
                this.Close();
                SpatialQueriesForm SpatialQueriesForm = new SpatialQueriesForm(m_currSQ, ucCreateData.CreateData, selectedTerrains.ToArray(), m_currentViewport);
               // Manager_MCLayers.RemoveStandaloneLayers(apQuerySecondaryDtmLayers);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcSpatialQueries.Create", McEx);
            }
        }

        private void contextMenuStripTerrainList_Click(object sender, EventArgs e)
        {
            CreateNewTerrain();
        }

        private void btnCreateNewTerrain_Click(object sender, EventArgs e)
        {
            CreateNewTerrain();
        }

        private void CreateNewTerrain()
        {
            TerrainWizzardForm terrainWiz = new TerrainWizzardForm();

            Manager_MCLayers.AddTerrainWizzardFormToList(terrainWiz);

            if (terrainWiz.ShowDialog() == DialogResult.OK)
            {
                if (terrainWiz.Terrain != null)
                {
                    string name = Manager_MCNames.GetNameByObject(terrainWiz.Terrain, "Terrain");
                    m_lstTerrainText.Add(name);

                    lstTerrains.Items.Add(name);
                    lstTerrains.SelectedItem = name;
                    m_lstTerrainValue.Add(terrainWiz.Terrain);

                    Manager_MCTerrain.RemoveTerrainFromDic(terrainWiz.Terrain);
                }
            }

            Manager_MCLayers.RemoveTerrainWizzardFormFromList(terrainWiz);

        }

        private void PreStandaloneSQForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Manager_MCLayers.RemoveStandaloneLayers(ctrlLayers1.GetLayers().ToArray());
        }
    }
}