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
    public partial class frmOverlaysList : Form
    {
        private IDNMcOverlay m_SelectedOverlay;

        public frmOverlaysList()
        {
            InitializeComponent();
            LoadForm();
        }

        private void LoadForm()
        {
            Dictionary<object, uint> allOverlaysManagers = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.AllParams;

            foreach (IDNMcOverlayManager currOM in allOverlaysManagers.Keys)
            {
                IDNMcOverlay [] overlaysInOM = currOM.GetOverlays();
                lstOverlay.Items.AddRange(overlaysInOM);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstOverlay.SelectedItem != null)
            {
                SelectedOverlay = (IDNMcOverlay)lstOverlay.SelectedItem;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public IDNMcOverlay SelectedOverlay
        {
            get { return m_SelectedOverlay; }
            set { m_SelectedOverlay = value; }
        }
    }
}