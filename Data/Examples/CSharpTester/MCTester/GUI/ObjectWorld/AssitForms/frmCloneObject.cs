using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;

namespace MCTester.ObjectWorld.Assit_Forms
{
    public partial class frmCloneObject : Form
    {
        IDNMcObject m_object;

        public frmCloneObject(IDNMcObject obj)
        {
            InitializeComponent();
            m_object = obj;
        }

        private void btnClone_Click(object sender, EventArgs e)
        {
            try
            {
                m_object.Clone(m_object.GetOverlay(), cbCloneObjectScheme.Checked, cbCloneLocationPoints.Checked);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Clone Object", McEx);
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
