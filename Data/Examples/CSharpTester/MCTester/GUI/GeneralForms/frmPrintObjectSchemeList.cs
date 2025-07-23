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
using MCTester.Managers.ObjectWorld;

namespace MCTester.General_Forms
{
    public partial class frmPrintObjectSchemeList : Form
    {
        private List<string> m_lSchemeDisplay;
        private List<IDNMcObjectScheme> m_lSchemeValue;
        private IDNMcObjectScheme m_SelectedScheme;

        public frmPrintObjectSchemeList(IDNMcObjectScheme currScheme)
        {
            InitializeComponent();
            m_lSchemeValue = new List<IDNMcObjectScheme>();
            m_lSchemeDisplay = new List<string>();
            m_SelectedScheme = currScheme;
            lstObjectScheme.DisplayMember = "lSchemeDisplay";
            lstObjectScheme.ValueMember = "lSchemeValue";
        }

        private void frmPrintObjectSchemeList_Load(object sender, EventArgs e)
        {
            IDNMcObjectScheme [] schemes = MCTMapFormManager.MapForm.Viewport.OverlayManager.GetObjectSchemes();
            foreach (IDNMcObjectScheme scheme in schemes)
            {
                if (!Manager_MCObjectScheme.IsTempObjectScheme(scheme))
                {
                    lSchemeDisplay.Add(Manager_MCNames.GetNameByObject(scheme, "Object Scheme"));
                    lSchemeValue.Add(scheme);
                }
            }
            lstObjectScheme.Items.AddRange(lSchemeDisplay.ToArray());

            int selectedIdx = lSchemeValue.IndexOf(m_SelectedScheme);
            if (selectedIdx != -1)
                lstObjectScheme.SetSelected(selectedIdx, true);
        }

        public List<string> lSchemeDisplay
        {
            get { return m_lSchemeDisplay; }
            set { m_lSchemeDisplay = value; }
        }

        public List<IDNMcObjectScheme> lSchemeValue
        {
            get { return m_lSchemeValue; }
            set { m_lSchemeValue = value; }
        }

        public IDNMcObjectScheme SelectedScheme
        {
            get { return m_SelectedScheme; }
            set { m_SelectedScheme = value; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstObjectScheme.SelectedIndex != -1)
                m_SelectedScheme = lSchemeValue[lstObjectScheme.SelectedIndex];
            else
                m_SelectedScheme = null;
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lstObjectScheme_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > lstObjectScheme.ItemHeight * lstObjectScheme.Items.Count)
                lstObjectScheme.ClearSelected();
        }

    }
}
