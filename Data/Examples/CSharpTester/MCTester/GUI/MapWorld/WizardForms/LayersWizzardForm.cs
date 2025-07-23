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
    public partial class LayersWizzardForm : Form
    {
        public LayersWizzardForm(List<IDNMcMapLayer> layers = null, bool isFilterDTMType = false)
        {
            InitializeComponent();
            ctrlLayers1.SetEnabled(true);
            ctrlLayers1.SetIsFilterDTMType(isFilterDTMType);
            ctrlLayers1.SetLayers(layers);
            Manager_MCLayers.AddCtrlLayersToList(ctrlLayers1);

        }

        public List<IDNMcMapLayer> GetLayers()
        {
            return ctrlLayers1.GetLayers();
        }

        private void LayersWizzardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Manager_MCLayers.RemoveCtrlLayersFromList(ctrlLayers1);

        }
    }
}
