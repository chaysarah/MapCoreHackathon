using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MCTester.Managers;
using MapCore;

namespace MCTester.ObjectWorld.OverlayManagerWorld.WizardForms
{
    public partial class OverlayWizardForm : Form, IUserControlItem
    {
        public OverlayWizardForm()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {
        }

        #endregion

        private void OverlayWizardForm_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            try
            {
                short nDrawPriority = short.Parse(txtDrawPriority.Text);
                this.DialogResult = DialogResult.OK;
                
            }
            catch
            {
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

        public short DrawPriority
        {
            get { return short.Parse(txtDrawPriority.Text);}
        }
        
    }
}