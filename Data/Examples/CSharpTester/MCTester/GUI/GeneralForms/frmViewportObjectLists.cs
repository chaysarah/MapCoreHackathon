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
using MCTester.Managers;

namespace MCTester.General_Forms
{
    public partial class frmViewportObjectLists : Form
    {
        private IDNMcObjectSchemeNode m_CurrObject;
        private List<string> m_lstViewportText = new List<string>();
        private List<IDNMcMapViewport> m_lstViewportValue = new List<IDNMcMapViewport>();
        private List<string> m_lstObjectText = new List<string>();
        private List<IDNMcObject> m_lstObjectValue = new List<IDNMcObject>();
        private IDNMcMapViewport m_SelectedViewport;
        private IDNMcObject m_SelectedObject;

        public frmViewportObjectLists(IDNMcObjectSchemeNode schemeNode)
        {
            InitializeComponent();
            m_CurrObject = schemeNode;

            lstViewports.DisplayMember = "ViewportsTextList";
            lstViewports.ValueMember = "ViewportsValueList";
            lstObjects.DisplayMember = "ObjectsTextList";
            lstObjects.ValueMember = "ObjectsValueList";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstViewports.SelectedItem != null)
                SelectedViewport = m_lstViewportValue[lstViewports.SelectedIndex];
            else
                SelectedViewport = null;

            if (lstObjects.SelectedItem != null)
                SelectedObject = m_lstObjectValue[lstObjects.SelectedIndex];
            else
                SelectedObject = null;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmViewportObjectLists_Load(object sender, EventArgs e)
        {
            if (m_CurrObject != null)
            {
                try
                {
                    IDNMcOverlayManager OM = (m_CurrObject.GetScheme()).GetOverlayManager();
                    IDNMcObject[] objects = (m_CurrObject.GetScheme()).GetObjects();

                    //Add objects to list
                    foreach (IDNMcObject obj in objects)
                    {
                        m_lstObjectText.Add(Manager_MCNames.GetNameByObject(obj, "Object"));
                        m_lstObjectValue.Add(obj);
                    }

                    uint[] VpIds = OM.GetViewportsIDs();

                    Dictionary<object, uint> viewports = Manager_MCViewports.AllParams;

                    //Add relevant viewport to list
                    foreach (uint ID in VpIds)
                    {
                        foreach (IDNMcMapViewport VP in viewports.Keys)
                        {
                            if (VP.ViewportID == ID)
                            {
                                m_lstViewportText.Add(Manager_MCNames.GetNameByObject(VP,"Viewport"));
                                m_lstViewportValue.Add(VP);
                            }
                        }
                    }

                    lstViewports.Items.AddRange(m_lstViewportText.ToArray());
                    lstObjects.Items.AddRange(m_lstObjectText.ToArray());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("frmScreenBoundingRect_Load", McEx);
                }
            }
            else
            {
                Dictionary<object, uint> viewports = Manager_MCViewports.AllParams;
                
                //Add all viewports and their objects to lists
                List<IDNMcOverlayManager> lOverlayManagers = new List<IDNMcOverlayManager>();
                foreach (IDNMcMapViewport VP in viewports.Keys)
                {
                    if (!lOverlayManagers.Contains(VP.OverlayManager))
                        lOverlayManagers.Add(VP.OverlayManager);

                    m_lstViewportText.Add(Manager_MCNames.GetNameByObject(VP,"Viewport"));
                    m_lstViewportValue.Add(VP);
                }

                foreach (IDNMcOverlayManager om in lOverlayManagers)
                {
                    IDNMcOverlay [] overlays = om.GetOverlays();
                    foreach (IDNMcOverlay overlay in overlays)
                    {
                        IDNMcObject [] objs = overlay.GetObjects();
                        foreach (IDNMcObject obj in objs)
                        {
                            m_lstObjectText.Add(Manager_MCNames.GetNameByObject(obj, " Object"));
                            m_lstObjectValue.Add(obj);
                        }                        
                    }
                }

                lstViewports.Items.AddRange(m_lstViewportText.ToArray());
                lstObjects.Items.AddRange(m_lstObjectText.ToArray());
            }
        }

        public List<string> ViewportsTextList
        {
            get { return m_lstViewportText; }
            set { m_lstViewportText = value; }
        }

        public List<IDNMcMapViewport> ViewportsValueList
        {
            get { return m_lstViewportValue;}
            set { m_lstViewportValue = value; }
        }

        public List<string> ObjectsTextList
        {
            get { return m_lstObjectText;}
            set { m_lstObjectText = value; }
        }

        public List<IDNMcObject> ObjectsValueList
        {
            get { return m_lstObjectValue;}
            set { m_lstObjectValue = value; }
        }

        public IDNMcMapViewport SelectedViewport
        {
            get { return m_SelectedViewport; }
            set { m_SelectedViewport = value; }
        }

        public IDNMcObject SelectedObject
        {
            get { return m_SelectedObject; }
            set { m_SelectedObject = value; }
        }

        private void btnClearViewportSelection_Click(object sender, EventArgs e)
        {
            lstViewports.ClearSelected();
            lstObjects.ClearSelected();
        }
    }
}