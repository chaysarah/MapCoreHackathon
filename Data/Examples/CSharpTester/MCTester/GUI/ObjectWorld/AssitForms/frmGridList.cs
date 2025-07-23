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
	public partial class frmGridList: Form
	{
        IDNMcMapGrid m_SelectedGrid;
        IDNMcMapGrid[] grids;


        public frmGridList()
		{
			InitializeComponent();
            LoadForm();
		}

        private void LoadForm()
        {
            Dictionary<object, uint> dGrid = MCTester.Managers.MapWorld.Manager_MCGrid.AllParams;
            grids = new IDNMcMapGrid [dGrid.Count];
            string[] gridNames = new string[dGrid.Count];
            dGrid.Keys.CopyTo(grids,0);

            for (int i = 0; i < dGrid.Count; i++)
            {
                gridNames[i] = Managers.Manager_MCNames.GetNameByObject(grids[i]);
            }

            lstGrid.Items.AddRange(gridNames);
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstGrid.SelectedIndex >= 0 && lstGrid.SelectedIndex < grids.Length)
            {
                SelectedGrid = grids[lstGrid.SelectedIndex];

                this.Close();
                this.DialogResult = DialogResult.OK;
            }
        }

        public IDNMcMapGrid SelectedGrid
        {
            get { return m_SelectedGrid; }
            set { m_SelectedGrid = value; }
        }
	}
}