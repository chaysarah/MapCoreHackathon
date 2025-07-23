using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.Assit_Forms
{
    public partial class frmViewportsListContainSpecificOverlay : Form
    {
        private IDNMcOverlay m_Overlay;
        private int m_MinScale;
        private int m_MaxScale;
        private IDNMcMapViewport m_SelectedViewport;

        public frmViewportsListContainSpecificOverlay(IDNMcOverlay senderOverlay)
        {
            InitializeComponent();
            m_Overlay = senderOverlay;
        }

        private void frmViewportsListContainSpecificOverlay_Load(object sender, EventArgs e)
        {
            try
            {
                IDNMcOverlayManager OM = m_Overlay.GetOverlayManager();
                uint[] VpIds = OM.GetViewportsIDs();

                Dictionary<object, uint> viewports = MCTester.Managers.MapWorld.Manager_MCViewports.AllParams;

                foreach (uint ID in VpIds)
                {
                    foreach (IDNMcMapViewport VP in viewports.Keys)
                    {
                        if (VP.ViewportID == ID)
                            lstViewports.Items.Add(VP);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("frmEffectiveVisibilityInViewport_Load", McEx);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedViewport = (IDNMcMapViewport)lstViewports.SelectedItem;
            MinScale = ntxMinScale.GetInt32();
            MaxScale = ntxMaxScale.GetInt32();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public IDNMcMapViewport SelectedViewport
        {
            get { return m_SelectedViewport; }
            set { m_SelectedViewport = value; }
        }

        public int MinScale
        {
            get { return m_MinScale; }
            set { m_MinScale = value; }
        }

        public int MaxScale
        {
            get { return m_MaxScale; }
            set { m_MaxScale = value; }
        }
    }
}