using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCTester.General_Forms
{
    public partial class frmAddMapToSchemeDetails : Form
    {
        public string SchemeAreaTxt;
        public string SchemeMapTypeTxt;
        public string OverlayManagerName;
        public string SchemeCommentsTxt;

        public frmAddMapToSchemeDetails()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SchemeAreaTxt = txtSchemeArea.Text;
            SchemeCommentsTxt = txtSchemeComments.Text;
            SchemeMapTypeTxt = txtSchemeMapType.Text;
            OverlayManagerName = txtOverlayManagerName.Text;

            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
