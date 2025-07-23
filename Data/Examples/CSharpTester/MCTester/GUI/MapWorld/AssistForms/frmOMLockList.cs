using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;
using MCTester.Managers.ObjectWorld;

namespace MCTester.MapWorld.Assist_Forms
{
    public partial class frmOMLockList : Form
    {
        private IDNMcOverlayManager m_OverlayManager;
        private string m_SenderName;
        private IDNMcObjectScheme[] m_ObjectSchemes;
        private IDNMcConditionalSelector[] m_ConditionalSelectors;

        public frmOMLockList(IDNMcOverlayManager overlayManager, string sender)
        {
            InitializeComponent();
            m_OverlayManager = overlayManager;
            m_SenderName = sender;
        }

        private void frmOMLockList_Load(object sender, EventArgs e)
        {
            if (m_SenderName == "LockSchemeList")
            {
                try
                {
                    m_ObjectSchemes = Manager_MCObjectScheme.GetSchemesWithoutTempSchemes(m_OverlayManager.GetObjectSchemes());
                    dgvLockList.RowCount = m_ObjectSchemes.Length;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetObjectSchemes", McEx);
                }

                for (int row = 0; row < m_ObjectSchemes.Length; row++)
                {
                    dgvLockList[0, row].Value = Manager_MCNames.GetNameByObject(m_ObjectSchemes[row], "Scheme");
                    try
                    {
                        dgvLockList[1,row].Value = m_OverlayManager.IsObjectSchemeLocked(m_ObjectSchemes[row]);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("IsObjectSchemeLocked", McEx);
                    }
                }
            }
            else
            {
                try
                {
                    m_ConditionalSelectors = m_OverlayManager.GetConditionalSelectors();
                    dgvLockList.RowCount = m_ConditionalSelectors.Length;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetObjectSchemes", McEx);
                }

                for (int row = 0; row < m_ConditionalSelectors.Length; row++)
                {
                    dgvLockList[0, row].Value = Manager_MCNames.GetNameByObject(m_ConditionalSelectors[row], m_ConditionalSelectors[row].ConditionalSelectorType.ToString());
                    try
                    {
                        dgvLockList[1, row].Value = m_OverlayManager.IsConditionalSelectorLocked(m_ConditionalSelectors[row]);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("IsConditionalSelectorLocked", McEx);
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (m_SenderName == "LockSchemeList")
            {
                for(int row = 0; row < m_ObjectSchemes.Length; row++)
                {
                    try
                    {
                        m_OverlayManager.SetObjectSchemeLock(m_ObjectSchemes[row], (bool)dgvLockList[1, row].Value);
                        if ((bool)dgvLockList[1, row].Value == false)
                            m_ObjectSchemes[row].Dispose();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("SetObjectSchemeLock", McEx);
                    }
                }
            }
            else
            {
                for (int row = 0; row < m_ConditionalSelectors.Length; row++)
                {
                    try
                    {
                        m_OverlayManager.SetConditionalSelectorLock(m_ConditionalSelectors[row], (bool)dgvLockList[1, row].Value);
                        if ((bool)dgvLockList[1, row].Value == false)
                            m_ConditionalSelectors[row].Dispose();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("SetConditionalSelectorLock", McEx);
                    }
                }
            }

            this.Close();
        }
    }
}
