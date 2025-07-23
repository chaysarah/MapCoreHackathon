using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;
using MCTester.Managers.MapWorld;

namespace MCTester.MapWorld.WizardForms
{
    public partial class CtrlLayers : UserControl
    {
        private List<IDNMcMapLayer> m_mapLayersList;
        private bool m_isFilterDTMType = false;

        public CtrlLayers()
        {
            InitializeComponent();
            m_mapLayersList = new List<IDNMcMapLayer>();
        }

        public void SetIsFilterDTMType(bool isFilterDTMType)
        {
            m_isFilterDTMType = isFilterDTMType;
        }

        public List<IDNMcMapLayer> GetLayers()
        {
            return m_mapLayersList;
        }

        public void NoticeLayerRemoved(IDNMcMapLayer layerToRemove)
        {
            if (layerToRemove != null && m_mapLayersList.Contains(layerToRemove))
            {
                m_lstLayerItems.Items.Clear();
                m_mapLayersList.Remove(layerToRemove);
                LoadLayers();
            }
        }

        private void LoadLayers()
        {
            if (m_mapLayersList != null)
            {
                m_lstLayerItems.Items.Clear();
                foreach (IDNMcMapLayer layer in m_mapLayersList)
                    m_lstLayerItems.Items.Add(layer.LayerType + " - " + layer.GetDirectory());
            }
        }

        internal void ClearSelected()
        {
            m_lstLayerItems.ClearSelected();
        }

        internal void SetEnabled(bool isEnabled)
        {
            m_lstLayerItems.Enabled = isEnabled;
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            AddLayerForm addLayer = new AddLayerForm(m_isFilterDTMType);

            if (addLayer.ShowDialog() == DialogResult.OK && addLayer.NewLayers != null && addLayer.NewLayers.Count > 0)
            {
                for (int i = 0; i < addLayer.NewLayers.Count; i++)
                {
                    if (m_isFilterDTMType && !(addLayer.NewLayers[i] is IDNMcDtmMapLayer))
                    {
                        MessageBox.Show("Error: Non DTM layer can not be chosen as Query Secondary DTM Layer", "Add Query Secondary DTM Layer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if(m_mapLayersList == null)
                    m_mapLayersList = new List<IDNMcMapLayer>();    
                m_mapLayersList.AddRange(addLayer.NewLayers);
                LoadLayers();
            }
        }

        private void btnRemoveLayer_Click(object sender, EventArgs e)
        {
            if (m_lstLayerItems.SelectedItems.Count > 0)
            {
                int SelectedIdx = m_lstLayerItems.SelectedIndex;

                foreach (IDNMcMapLayer layer in m_mapLayersList)
                {
                    Manager_MCLayers.RemoveLayerFromServerLayersPriorityDic(layer);
                }
                m_lstLayerItems.Items.RemoveAt(SelectedIdx);
                m_mapLayersList.RemoveAt(SelectedIdx);
            }
        }

        internal void SetLayers(List<IDNMcMapLayer> layers)
        {
            m_mapLayersList = layers;
            LoadLayers();   
        }
    }
}
