using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;

namespace MCTester.ObjectWorld.Assit_Forms
{
    public partial class frmMapHeightLines : Form
    {
        private IDNMcMapHeightLines m_SelectedMapHeightLines;

        public frmMapHeightLines()
        {
            InitializeComponent();
        }

        private void frmMapHeightLines_Load(object sender, EventArgs e)
        {
            Dictionary<object, uint> dHLines = Manager_MCMapHeightLines.AllParams;
            IDNMcMapHeightLines[] maps = new IDNMcMapHeightLines[dHLines.Count];
            dHLines.Keys.CopyTo(maps, 0);

            lstMapsHeightLines.Items.AddRange(maps);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstMapsHeightLines.SelectedItem != null)
            {
                SelectedMapHeightLines = (IDNMcMapHeightLines)lstMapsHeightLines.SelectedItem;

                this.Close();
                this.DialogResult = DialogResult.OK;
            }
        }

        public IDNMcMapHeightLines SelectedMapHeightLines
        {
            get { return m_SelectedMapHeightLines; }
            set { m_SelectedMapHeightLines = value; }
        }
    }
}