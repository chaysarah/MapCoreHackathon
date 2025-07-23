using MapCore;
using MapCore.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCTester.ObjectWorld.Assit_Forms
{
    public partial class frmLoadObjectsFromRawVectorData : Form
    {
        IDNMcOverlay mcOverlay;
        IDNMcObject[] mLoadedObjects;
        public frmLoadObjectsFromRawVectorData()
        {
            InitializeComponent();
        }
        public frmLoadObjectsFromRawVectorData(IDNMcOverlay _mcOverlay): this()
        {
            mcOverlay = _mcOverlay;
        }

        public IDNMcObject[] LoadedObjects
        {
            get { return mLoadedObjects; }
        }

        private void btnLoadObjectsFromRawVectorData_Click(object sender, EventArgs e)
        {
            try
            {
                /*if(ctrlLayerGridCoordinateSystem.GridCoordinateSystem == null)
                {
                    MessageBox.Show("No Grid Coordinate System was specified.\nYou have to choose one!\n");
                    return;
                }*/
                DNSRawVectorParams vectorMapLayerParams = rawVectorParams1.RawVectorParams;
                vectorMapLayerParams.strDataSource = browseLayerCtrl.FileName;
                vectorMapLayerParams.pSourceCoordinateSystem = ctrlLayerGridCoordinateSystem.GridCoordinateSystem;
                mLoadedObjects = mcOverlay.LoadObjectsFromRawVectorData(vectorMapLayerParams, chxClearObjectSchemesCache.Checked);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch(MapCoreException mcEx)
            {
                Utilities.ShowErrorMessage("LoadObjectsFromRawVectorData", mcEx);
            }
        }
    }
}
