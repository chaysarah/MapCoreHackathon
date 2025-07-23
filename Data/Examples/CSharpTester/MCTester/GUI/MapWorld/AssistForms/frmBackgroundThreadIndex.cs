using MapCore;
using MCTester.Managers;
using MCTester.Managers.MapWorld;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCTester.MapWorld.Assist_Forms
{
    public partial class frmBackgroundThreadIndex : Form
    {
        public frmBackgroundThreadIndex()
        {
            InitializeComponent();
            FillGrid();
        }

        private void FillGrid()
        {
            List<IDNMcMapLayer> layers = Manager_MCLayers.AllLayers();
            Dictionary<string, uint> dicLayersBackgroundThreadIndex = new Dictionary<string, uint>();

            foreach(IDNMcMapLayer layer in layers)
            {
                uint index = layer.GetBackgroundThreadIndex();
                if(index == DNMcConstants._MC_EMPTY_ID)
                    dgvLayerThreadIndex.Rows.Add(Manager_MCNames.GetNameByObject(layer),"");
                else
                    dgvLayerThreadIndex.Rows.Add(Manager_MCNames.GetNameByObject(layer), index);
            }
        }
    }
}
