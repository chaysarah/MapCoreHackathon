using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class frmOverlayManagerList : Form
    {
        private IDNMcOverlayManager m_OverlayManager;

        public frmOverlayManagerList()
        {
            InitializeComponent();
            LoadForm();
        }

        private void LoadForm()
        {
            Dictionary<object, uint> dOverlayManager = Manager_MCOverlayManager.AllParams;
            
            foreach(IDNMcOverlayManager keyOM in dOverlayManager.Keys)
            {
                lstOverlayManager.Items.Add(keyOM);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_OverlayManager = (IDNMcOverlayManager)lstOverlayManager.SelectedItem;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public IDNMcOverlayManager SelectedOverlayManager
        {
            get { return m_OverlayManager; }
            set { m_OverlayManager = value; }
        }

    }
}