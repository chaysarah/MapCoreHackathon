using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MCTester.ObjectWorld.Assit_Forms
{
    public partial class frmSaveSchemeOptions : Form
    {
        private bool m_SaveNames;
        private bool m_SaveToCSV;

        public frmSaveSchemeOptions()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveNames = chxSavePropertiesNames.Checked;
            SaveToCSV = chxSaveToCSV.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #region Public Properties
        public bool SaveNames
        {
            get { return m_SaveNames; }
            set { m_SaveNames = value; }
        }

        public bool SaveToCSV
        {
            get { return m_SaveToCSV; }
            set { m_SaveToCSV = value; }
        }
        
        #endregion
    }
}
