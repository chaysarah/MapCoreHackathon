using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MCTester.General_Forms
{
    public partial class frmOpenCommunication : Form
    {
        private string mServerIP;
        private int mServerAddress;

        public frmOpenCommunication()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ServerIP = txtServerIP.Text;
            
            int address;
            bool parseSuccess = int.TryParse(txtServerAddress.Text, out address);
            if (parseSuccess == true)
            {
                mServerAddress = address;
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }                
            else
                MessageBox.Show("Server Address", "Incorrect Server Address Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public string ServerIP
        {
            get { return mServerIP; }
            set { mServerIP = value; }
        }

        public int ServerAddress
        {
            get { return mServerAddress; }
            set 
            {

                mServerAddress = value; 
            }
        }
    }
}
