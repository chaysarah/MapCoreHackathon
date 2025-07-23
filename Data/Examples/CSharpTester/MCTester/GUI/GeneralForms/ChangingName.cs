using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCTester.Managers;
using MapCore;

namespace MCTester.General_Forms
{
    public partial class ChangingName : Form
    {
        private string m_newName;
        private string m_fullName;
        private uint m_currId;
        private object m_currNode;
       

        public uint CurrId
        {
            get { return m_currId; }
            set { m_currId = value; }
        }

        public object CurrNode
        {
            get { return m_currNode; }
            set { m_currNode = value; }
        }

        public string NewName
        {
            get { return m_newName; }
            set { m_newName = value; }
        }

        public string FullName
        {
            get { return m_fullName; }
            set { m_fullName = value; }
        }

        public ChangingName()
        {
            InitializeComponent();
        }
       

        public ChangingName(object currNode)
            : this()
        {
            m_currNode = currNode;
            m_currId = uint.Parse(currNode.GetHashCode().ToString());
            tbName.Text = Manager_MCNames.GetMcNameById(m_currId);
        }

        public ChangingName(object currNode, string name)
            : this(currNode)
        {
            if(name != string.Empty)
                tbName.Text = name;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_newName = tbName.Text;
            Manager_MCNames.SetName(m_currNode, m_newName);

            m_fullName = Manager_MCNames.GetNameByObject(m_currNode);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
