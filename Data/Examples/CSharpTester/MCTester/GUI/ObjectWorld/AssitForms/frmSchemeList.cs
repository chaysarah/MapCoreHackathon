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

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class frmSchemeList : Form
    {
        private IDNMcObject obj;
        private IDNMcObjectScheme m_SelectedScheme;
        private bool m_KeepRelevantProperties;
        IDNMcObjectScheme[] schemes;

        public frmSchemeList(IDNMcObject selectedObj)
        {
            InitializeComponent();
            obj = selectedObj;
            LoadForm();
        }

        private void LoadForm()
        {
            if (obj != null)
            {
                IDNMcOverlayManager OM = obj.GetOverlayManager();
                schemes = Manager_MCObjectScheme.GetSchemesWithoutTempSchemes(OM.GetObjectSchemes());

                foreach (IDNMcObjectScheme scheme in schemes)
                {
                    lstScheme.Items.Add(Manager_MCNames.GetNameByObject(scheme, "ObjectScheme"));
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_SelectedScheme = (IDNMcObjectScheme)schemes[lstScheme.SelectedIndex];
            KeepRelevantProperties = chxKeepRelevantProperties.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public IDNMcObjectScheme SelectedScheme
        {
            get { return m_SelectedScheme; }
            set { m_SelectedScheme = value; }
        }

        public bool KeepRelevantProperties
        {
            get { return m_KeepRelevantProperties; }
            set { m_KeepRelevantProperties = value; }
        }
    }
}