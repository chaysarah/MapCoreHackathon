using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MCTester.GUI.Forms
{
    public partial class OpenParticalEffectNameDialogForm : Form
    {
        private string m_EffectName;

        public OpenParticalEffectNameDialogForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            EffectName = txtEffectName.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string EffectName
        {
            get { return m_EffectName; }
            set { m_EffectName = value; }
        }
    }
}