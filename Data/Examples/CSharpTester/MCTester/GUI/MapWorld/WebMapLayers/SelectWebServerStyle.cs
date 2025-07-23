using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCTester.MapWorld.WebMapLayers
{
    public partial class SelectWebServerStyle : Form
    {
        public SelectWebServerStyle()
        {
            InitializeComponent();
        }
        public List<string> SelectStyles = new List<string>();
        public string strSelectStyles = "";

        public SelectWebServerStyle(string[] names, string userSelection) : this()
        {
            string def = "Default";
           
            List<string> style_names = new List<string>();
            style_names.Add(def);

            if (names != null && names.Length > 0)
            {

                style_names.AddRange(names);
            }

            ctrlCheckedListBox1.LoadList(style_names.ToArray(), style_names.Count, 0, -1);

            if (names != null && names.Length > 0)
            {
                if (userSelection != null && userSelection != "")
                {
                    string[] styles = userSelection.Split(',');
                    int i = 0;
                    foreach (string name in style_names)
                    {
                        if (styles.Contains(name))
                            ctrlCheckedListBox1.SetItemChecked(i, true);
                        i++;
                    }
                }
                else
                {
                    ctrlCheckedListBox1.SetItemChecked(0, true);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectStyles.Clear();
            if (ctrlCheckedListBox1.CheckedIndices.Count == 1 && ctrlCheckedListBox1.CheckedIndices[0] == 0)
                strSelectStyles = "";
            else
            {
                foreach (string str in ctrlCheckedListBox1.CheckedItems)
                    strSelectStyles += str + ",";

                if (strSelectStyles != "")
                    strSelectStyles = strSelectStyles.Substring(0, strSelectStyles.Length - 1);
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
