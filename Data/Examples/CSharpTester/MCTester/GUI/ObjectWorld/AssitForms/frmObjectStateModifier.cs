using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.Managers;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.Assit_Forms
{
    public partial class frmObjectStateModifier : Form
    {
        IDNMcObjectScheme m_currentObject;
        IDNMcConditionalSelector[] m_selectors;

        public frmObjectStateModifier(IDNMcObjectScheme currentObject)
        {
            InitializeComponent();

            DNSObjectStateModifier state = new DNSObjectStateModifier();
            state.pConditionalSelector = null;

            ObjectState = state;

            m_currentObject = currentObject;
            m_selectors = m_currentObject.GetOverlayManager().GetConditionalSelectors();
            for (int i=0; i<m_selectors.Length; i++)
            {
                string id = Manager_MCNames.GetNameByObject(
                    m_selectors[i], 
                    m_selectors[i].ConditionalSelectorType.ToString());
                cmbConditionalSelector.Items.Add(id);
            }
        }


        public DNSObjectStateModifier ObjectState { get; set; }


        private void bnOk_Click(object sender, EventArgs e)
        {
            DNSObjectStateModifier state = ObjectState;
            if (cmbConditionalSelector.SelectedIndex >= 0)
            {
                state.pConditionalSelector = m_selectors[cmbConditionalSelector.SelectedIndex];
            }
            state.bActionOnResult = chkActionOnResult.Checked;
            byte uiObjectText;
            if (byte.TryParse(tbObjectState.Text, out uiObjectText))
            {
                state.uObjectState = uiObjectText;
            }
            ObjectState = state;
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {

        }

        private void frmObjectStateModifier_Load(object sender, EventArgs e)
        {
            if (ObjectState.pConditionalSelector != null)
            {
                string nameToFind = Manager_MCNames.GetNameByObject(
                    ObjectState.pConditionalSelector,
                    ObjectState.pConditionalSelector.ConditionalSelectorType.ToString());

                for (int i=0; i<cmbConditionalSelector.Items.Count; i++)
                {
                    if (cmbConditionalSelector.Items[i].ToString() == nameToFind)
                    {
                        cmbConditionalSelector.SelectedIndex = i;
                        break;
                    }
                }

                chkActionOnResult.Checked = ObjectState.bActionOnResult;
                tbObjectState.Text = ObjectState.uObjectState.ToString();
            }
        }
    }
}
