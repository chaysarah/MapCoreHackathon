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
using MCTester.Managers;

namespace MCTester.MapWorld.Assist_Forms
{
    public partial class frmFindShortesPathOverlayList : Form
    {
        private IDNMcOverlay m_SelectedOverlay;
        private List<string> m_lstOverlayText = new List<string>();
        private List<IDNMcOverlay> m_lstOverlayValue = new List<IDNMcOverlay>();
        
        public frmFindShortesPathOverlayList()
        {
            InitializeComponent();
            lstOverlays.DisplayMember = "OverlayText";
            lstOverlays.ValueMember = "OverlayValue";
        }

        private void frmFindShortesPathOverlayList_Load(object sender, EventArgs e)
        {
            foreach (IDNMcOverlayManager om in Manager_MCOverlayManager.AllParams.Keys)
            {
                try
                {
                    IDNMcOverlay[] overlays = om.GetOverlays();
                    foreach (IDNMcOverlay overlay in overlays)
                    {
                        OverlayText.Add(Manager_MCNames.GetNameByObject(overlay,"Overlay"));
                        OverlayValue.Add(overlay);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetOverlays", McEx);
                }
            }

            lstOverlays.Items.AddRange(m_lstOverlayText.ToArray());
        }

        public List<string> OverlayText
        {
            get { return m_lstOverlayText; }
            set { m_lstOverlayText = value; }
        }

        public List<IDNMcOverlay> OverlayValue
        {
            get { return m_lstOverlayValue; }
            set { m_lstOverlayValue = value; }
        }

        public IDNMcOverlay SelectedOverlay
        {
            get { return m_SelectedOverlay; }
            set { m_SelectedOverlay = value; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstOverlays.SelectedIndex > 0)
            {
                SelectedOverlay = OverlayValue[lstOverlays.SelectedIndex];

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("You have to choose overlay!", "Overlay Not Chosen", MessageBoxButtons.OK, MessageBoxIcon.Error);            
        }
    }
}
