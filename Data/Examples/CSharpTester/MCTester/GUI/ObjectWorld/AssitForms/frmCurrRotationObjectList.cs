using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class frmCurrRotationObjectList : Form
    {
        private IDNMcObject m_Object;

        public frmCurrRotationObjectList(IDNMcPhysicalItem currPhysical)
        {
            InitializeComponent();
            IDNMcObjectScheme schemes = currPhysical.GetScheme();
            
            lstObject.Items.AddRange(schemes.GetObjects());
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstObject.SelectedItem != null)
            {
                SelectedObject = (IDNMcObject)lstObject.SelectedItem;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("You have to chose an object first!");
        }

        public IDNMcObject SelectedObject
        {
            get { return m_Object; }
            set { m_Object = value; }
        }

    }
}