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

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class frmNewObjectScheme : Form
    {
        private string m_NewScheme;
        protected IDNMcOverlayManager m_OverlayManager;

        List<string> m_lstSchemeText = new List<string>();
        List<IDNMcObjectSchemeItem> m_lstSchemeValue = new List<IDNMcObjectSchemeItem>();

        public frmNewObjectScheme(object newSchemeKind, IDNMcOverlayManager overlayManager)
        {
            InitializeComponent();
            cmbLocationCoordSys.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            cmbLocationCoordSys.Text = DNEMcPointCoordSystem._EPCS_WORLD.ToString();

            DNETerrainObjectsConsiderationFlags[] flags =
                (DNETerrainObjectsConsiderationFlags[])Enum.GetValues(typeof(DNETerrainObjectsConsiderationFlags));
            for (int i = 0; i < flags.Length; i++ )
            {
                if (flags[i] != DNETerrainObjectsConsiderationFlags._ETOCF_NONE)
                {
                    lstTerrainObjectsConsiderationFlags.Items.Add(flags[i].ToString());
                }
            }

            lstTerrainObjectsConsiderationFlags.SetItemCheckState(0, CheckState.Checked);

            m_NewScheme = newSchemeKind.ToString();
            m_OverlayManager = overlayManager;

            if (m_NewScheme == "Scheme with one location")
            {
                lstItemsList.Enabled = false;
            }
            else
            {
                lstItemsList.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DNEMcPointCoordSystem locationCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem),cmbLocationCoordSys.Text);
          DNETerrainObjectsConsiderationFlags flags = DNETerrainObjectsConsiderationFlags._ETOCF_NONE;
                    for (int i=0; i<lstTerrainObjectsConsiderationFlags.CheckedItems.Count; i++)
                    {
                        string name = lstTerrainObjectsConsiderationFlags.CheckedItems[i].ToString();
                        DNETerrainObjectsConsiderationFlags val = (DNETerrainObjectsConsiderationFlags)Enum.Parse(
                            typeof(DNETerrainObjectsConsiderationFlags), name);
                        flags |= val;
                    }

            if (m_NewScheme == "Scheme with one location")
            {
                try
                {
                    IDNMcObjectLocation ObjLocation = null;

                    IDNMcObjectScheme newScheme =  DNMcObjectScheme.Create(ref ObjLocation,
                                                                            m_OverlayManager,
                                                                            locationCoordSys,
                                                                            chxLocationRelativeToDTM.Checked,
                                                                            flags);

                    m_OverlayManager.SetObjectSchemeLock(newScheme, true);

                    this.Close();
                    this.DialogResult = DialogResult.OK;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcObjectScheme.Create", McEx);
                }
            }
            else
            {
                try
                {
                    if (lstItemsList.SelectedItem != null)
                    {
                        IDNMcObjectScheme newScheme = DNMcObjectScheme.Create(m_OverlayManager,
                                                                                (IDNMcObjectSchemeItem)m_lstSchemeValue[lstItemsList.SelectedIndex],
                                                                                locationCoordSys,
                                                                                chxLocationRelativeToDTM.Checked,
                                                                                flags);

                        m_OverlayManager.SetObjectSchemeLock(newScheme, true);

                        MCTester.Managers.ObjectWorld.Manager_MCObjectSchemeItem.RemoveItem((IDNMcObjectSchemeItem)m_lstSchemeValue[lstItemsList.SelectedIndex]);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                        MessageBox.Show("You have to choose an item", "Invalid chosen", MessageBoxButtons.OK, MessageBoxIcon.Information);   
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcObjectScheme.Create", McEx);
                }
            }            
        }

        private void frmNewObjectScheme_Load(object sender, EventArgs e)
        {
            Dictionary<object, uint> itemList = Manager_MCObjectSchemeItem.AllParams;
            //foreach (object item in itemList.Keys)
            //{
            //    lstItemsList.Items.Add(item);
            //}

            foreach (IDNMcObjectSchemeItem item in itemList.Keys)
            {
                string name = Manager_MCNames.GetNameByObject(item);

                m_lstSchemeText.Add(name);
                m_lstSchemeValue.Add(item);
            }

            lstItemsList.Items.AddRange(m_lstSchemeText.ToArray());
        }
    }
}