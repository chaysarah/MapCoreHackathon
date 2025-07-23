using MapCore;
using MCTester.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCTester.General_Forms
{
    public partial class frmRequestParams : Form
    {
        List<DNSMcKeyStringValue> m_lstKeyValue;

        public string CSWValue { get { return txCSWBodyValue.Text; } set { txCSWBodyValue.Text = value; } }

        public frmRequestParams()
        {
            InitializeComponent();
        }

        public frmRequestParams(List<DNSMcKeyStringValue> lstKeyValue, string txt) : this()
        {
            Text = txt;
            m_lstKeyValue = lstKeyValue;
            if (lstKeyValue != null)
            {
                for (int i = 0; i < lstKeyValue.Count; i++)
                {
                    dgvKeyValueArray.Rows.Add(lstKeyValue[i].strKey, lstKeyValue[i].strValue);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_lstKeyValue = new List<DNSMcKeyStringValue>();
            for (int i = 0; i < dgvKeyValueArray.RowCount; i++)
            {
                string key = dgvKeyValueArray[0, i] == null || dgvKeyValueArray[0, i].Value == null ? "" : dgvKeyValueArray[0, i].Value.ToString();
                string value = dgvKeyValueArray[1, i] == null || dgvKeyValueArray[1, i].Value == null ? "" : dgvKeyValueArray[1, i].Value.ToString();

                if (key != "" && value != "")
                {
                    DNSMcKeyStringValue mcKeyStringValue = new DNSMcKeyStringValue();
                    mcKeyStringValue.strKey = key;
                    mcKeyStringValue.strValue = value;

                    m_lstKeyValue.Add(mcKeyStringValue);
                }
            }
            DialogResult = DialogResult.OK;
            this.Close();

        }

        public List<DNSMcKeyStringValue> GetMcKeyStringValues()
        {
            return m_lstKeyValue;
        }

        public void VisibleCSWParams(bool isVisible)
        {
            groupBox1.Visible = isVisible;
            this.Size = new Size(389, 320);
        }

        private void btnImportFromFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML files (*.xml)|*.xml| Json files (*.json)|*.json | All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txCSWBodyValue.Text = File.ReadAllText(ofd.FileName);
            }
        }

    }
}
