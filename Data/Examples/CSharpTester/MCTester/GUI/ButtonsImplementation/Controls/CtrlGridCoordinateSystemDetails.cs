using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.General_Forms;
using MapCore.Common;

namespace MCTester.Controls
{
    public partial class CtrlGridCoordinateSystemDetails : UserControl
    {
        IDNMcGridCoordinateSystem mGridCoordinateSystem;
        public CtrlGridCoordinateSystemDetails()
        {
            InitializeComponent();
        }

        public void LoadData(IDNMcGridCoordinateSystem GridCoordinateSystem)
        {
            try
            {
                mGridCoordinateSystem = GridCoordinateSystem;
                if (mGridCoordinateSystem != null)
                {
                    lblType.Text = mGridCoordinateSystem.GetGridCoorSysType().ToString();
                    lblDatum.Text = mGridCoordinateSystem.GetDatum().ToString();
                }
                else
                    btnMoreDetails.Enabled = false;
            }
            catch (MapCoreException mc)
            {
                Utilities.ShowErrorMessage("IDNMcGridCoordinateSystem.GetGridCoorSysType/GetDatum", mc);
            }
        }

        public void SetText(bool isLayerInit)
        {
            string txt = "Grid Coordinate System";
            groupBox1.Text = txt + (isLayerInit ? "" : " (not initialized)");

            if(!isLayerInit)
                btnMoreDetails.Enabled = false;
        }

        private void btnMoreDetails_Click(object sender, EventArgs e)
        {
            if (mGridCoordinateSystem != null)
            {
                frmNewGridCoordinateSystem NewGridCoordinateSystemForm = new frmNewGridCoordinateSystem(mGridCoordinateSystem);
                NewGridCoordinateSystemForm.ShowDialog();
            }
        }
    }
}
