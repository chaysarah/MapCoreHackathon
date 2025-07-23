using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class frmNewOverlayManager : Form
    {
        public frmNewOverlayManager()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.CreateOverlayManager(ctrlOMGridCoordinateSystem.GridCoordinateSystem);
                this.DialogResult = DialogResult.OK;                
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcOverlayManager.Create", McEx);
            }

            this.Close();

        }


    }
}