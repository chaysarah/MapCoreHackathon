using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
//using MCTester.GUI.Trees;

namespace MCTester.MapWorld.WizardForms
{
    public partial class AddAndRemoveVectorLayersForm : Form
    {
        private bool flagStatus = true;
        private IDNMcMapLayer m_NewLayer;
        private List<IDNMcMapLayer> m_NewLayers;
        private IDNMcMapTerrain m_currTerrain;
      
        // private MCTerrainLayerTreeViewForm m_FatherForm;

        public List<IDNMcMapLayer> NewLayers
        {
            get { return m_NewLayers; }
            set { m_NewLayers = value; }
        }

        public AddAndRemoveVectorLayersForm(IDNMcMapTerrain currTerrain)
        {
            InitializeComponent();
            m_currTerrain = currTerrain;
           
            // m_FatherForm = fatherForm;

            if (flagStatus)
                btnAddRemove.Text = "Add Layers";
            else
                btnAddRemove.Text = "Remove Layers";
        }

        private void btnAddRemove_Click(object sender, EventArgs e)
        {
            if (m_NewLayers == null)
            {
                m_NewLayers = new List<IDNMcMapLayer>();

                m_NewLayer = AddLayer(browseVectorLayer1.FileName);
                if (m_NewLayer != null)
                    m_NewLayers.Add(m_NewLayer);

                m_NewLayer = AddLayer(browseVectorLayer2.FileName);
                if (m_NewLayer != null)
                    m_NewLayers.Add(m_NewLayer);
                
                m_NewLayer = AddLayer(browseVectorLayer3.FileName);
                if (m_NewLayer != null)
                    m_NewLayers.Add(m_NewLayer);

                if (m_NewLayers.Count > 0)
                {
                    btnAddRemove.Text = "Remove Layers";
                    // m_FatherForm.BuildTree();
                    // MCTerrainLayerTreeViewForm to check --- this.Parent  ???
                }
            }
            else
            {
                for (int i = 0; i < m_NewLayers.Count; i++)
                {
                    IDNMcMapLayer layerToRemove = m_NewLayers[i];
                    m_currTerrain.RemoveLayer(layerToRemove);
                    MCTester.Managers.MapWorld.Manager_MCLayers.RemoveStandaloneLayer(layerToRemove);
                    layerToRemove.Dispose();
                    layerToRemove = null;
                }
                btnAddRemove.Text = "Add Layers";
                m_NewLayers = null;
            }
        }

        private IDNMcMapLayer AddLayer(string folderName)
        {
            IDNMcMapLayer newLayer;
            if (folderName != String.Empty)
            {
                newLayer = MCTester.Managers.MapWorld.Manager_MCLayers.CreateNativeVectorLayer(folderName, null);
                MCTester.Managers.MapWorld.Manager_MCLayers.RemoveStandaloneLayer(newLayer);
                m_currTerrain.AddLayer(newLayer);
                return newLayer;
            }
            return null;
        }
    }
}
