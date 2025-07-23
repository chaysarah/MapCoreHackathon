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

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class frmConditionalSelectorList : Form
    {
        IDNMcConditionalSelector m_Selector;
        IDNMcConditionalSelector[] m_ArrSelectors;
        private IDNMcObjectScheme m_Scheme;

        public frmConditionalSelectorList(IDNMcObjectScheme scheme, IDNMcConditionalSelector selector)
        {
            InitializeComponent();
            m_Scheme = scheme;

            //Get list of the existing selectors
            m_ArrSelectors = m_Scheme.GetOverlayManager().GetConditionalSelectors();
            int index = 1;

            lstConditionalSelector.Items.Add("Null");
            foreach (IDNMcConditionalSelector selectorItem in m_ArrSelectors)
            {
                lstConditionalSelector.Items.Add(Manager_MCNames.GetNameByObject(selectorItem));
                if(selectorItem == selector)
                    lstConditionalSelector.SelectedIndex = index;
                index++;
            }
            ChosenSelector = selector;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstConditionalSelector.SelectedItem != null)
            {
                if (lstConditionalSelector.SelectedIndex == 0) // Null option
                    ChosenSelector = null;
                else
                    ChosenSelector = (IDNMcConditionalSelector)m_ArrSelectors[lstConditionalSelector.SelectedIndex-1];

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("You have to chose selector first!");
        }

        public IDNMcConditionalSelector ChosenSelector
        {
            get { return m_Selector; }
            set { m_Selector = value; }
        }


    }
}