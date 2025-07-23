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

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class frmSchemeNodeEffectiveVisibilityInViewport : Form
    {
        private IDNMcObjectSchemeNode m_CurrSchemeNode;
        private IDNMcObject m_SelectetObject;
        private IDNMcMapViewport m_SelectedViewport;

        public frmSchemeNodeEffectiveVisibilityInViewport(IDNMcObjectSchemeNode schemeNode)
        {
            InitializeComponent();
            m_CurrSchemeNode = schemeNode;
        }

        private void frmEffectiveVisibilityInViewport_Load(object sender, EventArgs e)
        {
            IDNMcObjectScheme scheme = null;

            try
            {
                scheme = m_CurrSchemeNode.GetScheme();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetScheme", McEx);
            }
            try
            {
                lstObjects.Items.AddRange(scheme.GetObjects());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetObjects", McEx);
            }            
        }

        private void lstObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                IDNMcObject obj = (IDNMcObject)lstObjects.SelectedItem;
                IDNMcOverlayManager OM = obj.GetOverlayManager();
                uint [] VpIds = OM.GetViewportsIDs();

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
                MapCore.Common.Utilities.ShowErrorMessage("lstObjects_SelectedIndexChanged", McEx);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedObject =  (IDNMcObject)lstObjects.SelectedItem;
            SelectedViewport = (IDNMcMapViewport)lstViewports.SelectedItem;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public IDNMcObject SelectedObject
        {
            get { return m_SelectetObject; }
            set { m_SelectetObject = value; }
        }

        public IDNMcMapViewport SelectedViewport
        {
            get { return m_SelectedViewport; }
            set { m_SelectedViewport = value; }
        }


    }
}