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
    public partial class frmGetSpecificViewportList : Form
    {
        #region  Private Members

        private IDNMcMapViewport m_SelectedViewport;
        private IDNMcOverlay mOverlay;
        private List<string> m_lstViewportText = new List<string>();
        private List<IDNMcMapViewport> m_lstViewportValue = new List<IDNMcMapViewport>();

        #endregion


        public frmGetSpecificViewportList()
        {
            InitializeComponent();
            m_SelectedViewport = null;

            lstViewports.DisplayMember = "ViewportsTextList";
            lstViewports.ValueMember = "ViewportsValueList";
        }

        public frmGetSpecificViewportList(IDNMcOverlay overlay): this()
        {
            mOverlay = overlay;            
        }

        #region  Private Events
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstViewports.SelectedItem != null)
                SelectedViewport = m_lstViewportValue[lstViewports.SelectedIndex];
            else
                SelectedViewport = null;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            lstViewports.ClearSelected();
        }

        #endregion


        #region Public Properties
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

        public IDNMcMapViewport SelectedViewport
        {
            get { return m_SelectedViewport; }
            set { m_SelectedViewport = value; }
        }

        #endregion

        private void frmGetSpecificViewportList_Load(object sender, EventArgs e)
        {
            if (mOverlay != null)
            {
                try
                {
                    IDNMcOverlayManager OM = mOverlay.GetOverlayManager();

                    if (OM != null)
                    {
                        uint[] VpIds = OM.GetViewportsIDs();

                        Dictionary<object, uint> viewports = Manager_MCViewports.AllParams;

                        //Add relevant viewports to list
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
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetSpecificViewportList_Load", McEx);
                }
            }
        }
    }
}
