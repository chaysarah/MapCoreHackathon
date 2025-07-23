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
    public partial class frmGdalOptions : Form
    {
        List<string> m_options = new List<string>();

        public frmGdalOptions()
        {
            InitializeComponent();
        }

        public frmGdalOptions(List<string> options):this()
        {
            for (int i = 0; i < options.Count; i++)
            {
                string optn = options[i];
                string[] optns = optn.Split('=');
                if(optns.Length == 2)
                {
                    dataGridView1.Rows.Add(optns[0], optns[1]);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string key = dataGridView1[0, i] == null || dataGridView1[0, i].Value == null ? "" : dataGridView1[0, i].Value.ToString();
                string value = dataGridView1[1, i] == null || dataGridView1[0, i].Value == null ? "" : dataGridView1[1, i].Value.ToString();

                if (key != "" && value != "")
                {
                    m_options.Add(key + "=" + value);
                }
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        public List<string> GetOptions()
        {
            return m_options;
        }



    }
}
