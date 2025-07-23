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
    public partial class frmMapLayers : Form
    {
        public frmMapLayers()
        {
            InitializeComponent();
        }

       public void SetLayer(IDNMcMapLayer layer)
        {
            ucLayer1.LoadItem(layer);
            ucLayer1.SetSecondaryDTMLayer();

        }
    }
}
