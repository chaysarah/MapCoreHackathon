using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MCTester.Controls
{
    public partial class ctrlUserData : UserControl
    {
        private byte[] m_UserDataByte;

        public ctrlUserData()
        {
            InitializeComponent();
        }

        public byte[] UserDataByte
        {
            get
            {
                UTF8Encoding UTF8 = new UTF8Encoding();
                return m_UserDataByte = UTF8.GetBytes(txtUserData.Text);
            }
            set 
            {
                m_UserDataByte = value;
                UTF8Encoding UTF8 = new UTF8Encoding();
                txtUserData.Text = UTF8.GetString(m_UserDataByte);
            }
        }

        public string UserDateText
        {
            get { return m_UserDataByte.ToString(); }
            set
            {
                ASCIIEncoding ASCII = new ASCIIEncoding();
                m_UserDataByte = ASCII.GetBytes(value);
            }
        }
    }
}
