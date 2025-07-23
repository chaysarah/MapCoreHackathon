using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    public partial class frmBasePropertyType : Form
    {
        public frmBasePropertyType()
        {
            InitializeComponent();
        }

        public frmBasePropertyType(uint id)
        {
            ntxPropertyID = new MCTester.Controls.NumericTextBox();
            ID = id;
            InitializeComponent();
            this.BackColor = Color.FromArgb(255, 250, 250, 250);

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        internal void HideIDControls()
        {
            label1.Visible = ntxPropertyID.Visible = false;
        }

        public uint ID
        {
            get { return ntxPropertyID.GetUInt32(); }
            set { ntxPropertyID.SetUInt32(value); }
        }
    }
}