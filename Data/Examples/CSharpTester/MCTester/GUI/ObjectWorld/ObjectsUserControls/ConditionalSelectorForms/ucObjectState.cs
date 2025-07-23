using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms
{
    public partial class ucObjectState : UserControl
    {
        int m_nLastSelectedIndex = -1;
        ComboBox m_combo;

        public ucObjectState()
        {
            InitializeComponent();
            string[] values = new string[256];
            for (int i = 0; i < values.Length; i++ )
            {
                values[i] = "";
            }
            listBox1.Items.AddRange(values);

            m_combo = new ComboBox();
            m_combo.Visible = false;
            m_combo.Parent = listBox1;
            for (int i=0; i<256; i++)
            {
                m_combo.Items.Add(i.ToString());
            }
        }

        public string States
        {
            get
            {
                string result = "";
                for (int i=0; i<listBox1.Items.Count; i++)
                {
                    if (listBox1.Items[i] != null)
                    {
                        result += (listBox1.Items[i].ToString() + ", ");
                    }
                }
                if (result.EndsWith(", ")) { result = result.Substring(0, result.Length - 2); }
                return result;
            }
            set
            {
                string[] inData = value.Split(",".ToCharArray());
                for (int i=0; i<listBox1.Items.Count; i++)
                {
                    if (i < inData.Length)
                    {
                        listBox1.Items[i] = inData[i];
                    }
                    else
                    {
                        listBox1.Items[i] = "";
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_nLastSelectedIndex != -1)
            {
                listBox1.Items[m_nLastSelectedIndex] = m_combo.Text;
            }

            if (listBox1.SelectedIndex != -1)
            {
                m_combo.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
                Rectangle rect = listBox1.GetItemRectangle(listBox1.SelectedIndex);
                m_combo.Location = rect.Location;
                m_combo.Size = rect.Size;
                m_combo.Visible = true;
            }
            else
            {
                m_combo.Visible = false;
            }
            m_nLastSelectedIndex = listBox1.SelectedIndex;
        }
    }
}
