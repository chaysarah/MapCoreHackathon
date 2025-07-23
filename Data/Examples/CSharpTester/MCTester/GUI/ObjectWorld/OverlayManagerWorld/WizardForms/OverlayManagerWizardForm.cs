using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MCTester.Managers;
using MapCore;
using MCTester.Managers.ObjectWorld;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using MCTester.GUI.Map;

namespace MCTester.ObjectWorld.OverlayManagerWorld.WizardForms
{
    public partial class OverlayManagerWizardForm : Form, IUserControlItem
    {
        private IDNMcOverlayManager aOverlayManager;

        public OverlayManagerWizardForm(IDNMcOverlayManager overlayManager)
        {
            InitializeComponent();
            aOverlayManager = overlayManager;
            m_radUseExisting.CheckedChanged += new EventHandler(m_radUseExisting_CheckedChanged);
            m_radUseExisting.Checked = true;
        }

        void m_radUseExisting_CheckedChanged(object sender, EventArgs e)
        {
            gbCreateNew.Enabled = m_radCreateNew.Checked;
            lstOverlayManagers.Enabled = m_radUseExisting.Checked;

            if (m_radUseExisting.Checked == false)
            {
                lstOverlayManagers.SelectedIndexChanged -= new EventHandler(lstOverlayManagers_SelectedIndexChanged);
                lstOverlays.Items.Clear();
                lstOverlayManagers.ClearSelected();
            }
            else
            {
                if (lstOverlayManagers.Items.Count != 0)
                {
                    lstOverlayManagers.ClearSelected();
                    lstOverlays.Items.Clear();
                    lstOverlayManagers.SelectedIndexChanged += new EventHandler(lstOverlayManagers_SelectedIndexChanged);
                }
            }
        }

        void lstOverlayManagers_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstOverlays.Items.Clear();
            //lstOverlayManagers.SelectedItem = lstOverlayManagers.Items[0];
            aOverlayManager = (IDNMcOverlayManager)lstOverlayManagers.SelectedItem;
            
            if (aOverlayManager != null)
            {
                IDNMcOverlay[] existingOverlay = aOverlayManager.GetOverlays();
                for (int i = 0; i < existingOverlay.Length; i++)
                {
                    lstOverlays.Items.Add(existingOverlay[i]);
                    if (Manager_MCOverlayManager.ActiveOverlay == existingOverlay[i])
                        lstOverlays.SelectedItem = (object)lstOverlays.Items[i];
                }
            }
            
        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {   
        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_radUseExisting.Checked)
                {
                    if (lstOverlayManagers.SelectedItem == null)
                        MessageBox.Show("You have to select desirable overlay manager", "Overlay Manager", MessageBoxButtons.OK);
                    else
                    {
                        aOverlayManager = (IDNMcOverlayManager)lstOverlayManagers.SelectedItem;

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                if (m_radCreateNew.Checked)
                {
                    if (ctrlOMGridCoordinateSystem.GridCoordinateSystem == null)
                    {
                        MessageBox.Show("You have to select desirable grid coordinate system", "Overlay Manager", MessageBoxButtons.OK);
                        return;
                    }

                    IDNMcOverlay newOverlay = null;

                    aOverlayManager = Manager_MCOverlayManager.CreateOverlayManager(ctrlOMGridCoordinateSystem.GridCoordinateSystem);
                    // Manager_MCOverlayManager.ActiveOverlayManager = aOverlayManager;
                    aOverlayManager.SetScaleFactor(ntxScaleFactor.GetFloat());

                    for (int i = 0; i < lstOverlays.Items.Count; i++)
                    {
                        newOverlay = DNMcOverlay.Create(aOverlayManager);
                        if (lstOverlays.SelectedIndex == i)
                            Manager_MCOverlayManager.UpdateOverlayManager(aOverlayManager, newOverlay);
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (MapCoreException McEx)
            {
                this.DialogResult = DialogResult.Cancel;
                MapCore.Common.Utilities.ShowErrorMessage("DNMcOverlay.Create or DNMcOverlayManager.Create", McEx);
            }
        }

        private void OverlayManagerWizardForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                lstOverlayManagers.Items.Clear();

                foreach (object OM in Manager_MCOverlayManager.AllParams.Keys)
                    lstOverlayManagers.Items.Add(OM);
            }
        }

        public IDNMcOverlayManager OverlayManager
        {
            get { return this.aOverlayManager; }
        }
        
        private void btnAddOverlay_Click(object sender, EventArgs e)
        {
            lstOverlays.Items.Add("Overlay");
        }

        private void btnRemoveOverlay_Click(object sender, EventArgs e)
        {
            if (lstOverlays.SelectedItems.Count > 0)
            {
                int SelectedIdx = lstOverlays.SelectedIndex;
                lstOverlays.Items.RemoveAt(SelectedIdx);
            }
        }       
    }
}