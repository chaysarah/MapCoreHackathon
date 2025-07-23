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

namespace MCTester.MapWorld.MapUserControls
{
    public partial class frmGlobalMapViewportList : Form
    {
        private IDNMcMapViewport m_GlobalMapVP;
        private IDNMcMapViewport m_SelectedViewport;
        private string m_DesirableAction;

        List<string> m_mapViewportNames = new List<string>();
        List<IDNMcMapViewport> m_mapViewports = new List<IDNMcMapViewport>();

        public frmGlobalMapViewportList(IDNMcMapViewport currViewport, string desirableAction)
        {
            m_GlobalMapVP = currViewport;
            m_DesirableAction = desirableAction;
            this.DialogResult = DialogResult.Ignore;

            InitializeComponent();
            
        }

        private void frmGlobalMapViewportList_Load(object sender, EventArgs e)
        {
            switch (m_DesirableAction)
            {
                case "Register":
                    this.Text = "Register Local Map";
                    break;
                case "UnRegister":
                    this.Text = "UnRegister Local Map";
                    break;
                case "SetActiveLoacalMap":
                    this.Text = "Set Active Local Map";
                    break;
            }
            switch (m_DesirableAction)
            {
                case "Register":

                    Dictionary<object, uint> allViewport = Manager_MCViewports.AllParams;

                    foreach (IDNMcMapViewport keyVP in allViewport.Keys)
                    {
                        if (m_GlobalMapVP != keyVP)
                        {
                            m_mapViewportNames.Add(Manager_MCNames.GetNameByObject(keyVP));
                            m_mapViewports.Add(keyVP);
                        }
                    }
                    lstViewports.Items.AddRange(m_mapViewportNames.ToArray());
                    break;
                case "UnRegister":
                case "SetActiveLoacalMap":

                    if (m_DesirableAction == "SetActiveLoacalMap")
                        btnClearSelection.Visible = true;

                    try
                    {
                        IDNMcMapViewport[] registeresMaps = m_GlobalMapVP.GetRegisteredLocalMaps();
                        foreach (IDNMcMapViewport mcMapViewport in registeresMaps)
                        {
                            m_mapViewportNames.Add(Manager_MCNames.GetNameByObject(mcMapViewport));
                        }
                        lstViewports.Items.AddRange(m_mapViewportNames.ToArray());
                        m_mapViewports.AddRange(registeresMaps);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetRegisteredLocalMaps", McEx);
                    }

                    try
                    {
                        if (m_GlobalMapVP.GetActiveLocalMap() != null)
                        {
                            IDNMcMapViewport activeLoacalMap = m_GlobalMapVP.GetActiveLocalMap();
                            for (int i = 0; i < m_mapViewports.Count; i++)
                            {
                                if (m_mapViewports[i] == activeLoacalMap)
                                    lstViewports.SetSelected(i, true);
                            }
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetActiveLocalMap", McEx);
                    }
                    break;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstViewports.SelectedIndex >= 0)
                SelectedViewport = m_mapViewports[lstViewports.SelectedIndex];

            if (m_DesirableAction == "SetActiveLoacalMap")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                if (SelectedViewport != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    MessageBox.Show("You have to choose viewport!!!");
            }            
        }

        public IDNMcMapViewport SelectedViewport
        {
            get { return m_SelectedViewport; }
            set { m_SelectedViewport = value; }
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            lstViewports.ClearSelected();
        }

    }
}