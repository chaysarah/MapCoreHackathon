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

namespace MCTester.ObjectWorld.OverlayManagerWorld
{
    public partial class frmEffectiveVisibilityInViewport : Form
    {
        private object m_SenderObject;
        private IDNMcMapViewport m_SelectedViewport;

        public frmEffectiveVisibilityInViewport(object senderObj)
        {
            InitializeComponent();
            m_SenderObject = senderObj;
        }

        private void frmEffectiveVisibilityInViewport_Load(object sender, EventArgs e)
        {
            if (m_SenderObject is IDNMcOverlay)
            {
                try
                {
                    IDNMcOverlayManager OM = ((IDNMcOverlay)m_SenderObject).GetOverlayManager();
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
            else
            {
                if (m_SenderObject is IDNMcObject)
                {
                    try
                    {
                        IDNMcOverlayManager OM = ((IDNMcObject)m_SenderObject).GetOverlayManager();
                        uint[] VpIds = OM.GetViewportsIDs();

                        Dictionary<object, uint> viewports = Manager_MCViewports.AllParams;

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
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedViewport = (IDNMcMapViewport)lstViewports.SelectedItem;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public IDNMcMapViewport SelectedViewport
        {
            get { return m_SelectedViewport; }
            set { m_SelectedViewport = value; }
        }
    }
}