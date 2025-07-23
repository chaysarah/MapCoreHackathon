using MapCore;
using MCTester.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.MapWorld.Assist_Forms
{
    public partial class frmSTilingSchemeParams : Form
    {
        public frmSTilingSchemeParams()
        {
            InitializeComponent();
           
        }

        public DNSTilingScheme STilingScheme
        {
            get
            {
                return ctrlTilingSchemeParams1.GetTilingScheme();
            }
            set
            {
                ctrlTilingSchemeParams1.SetTilingScheme(value);
               
            }
        }

        public void SetETilingSchemeAndType(DNSTilingScheme STilingScheme)
        {
            ctrlTilingSchemeParams1.SetETilingSchemeTypeByValue(STilingScheme);
            ctrlTilingSchemeParams1.SetTilingScheme(STilingScheme);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        internal void SetControlsReadonly(bool isReadOnly)
        {
            if(isReadOnly)
                GeneralFuncs.SetControlsReadonly(this);
        }

    }
}
