using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MapCore;
using UnmanagedWrapper;

namespace MCMsoPluginTester
{
    public partial class ShowCoords : Form
    {
        public ShowCoords(string geomType, DNSMcVector3D [] coords)
        {
            InitializeComponent();

            geomTypeLabel.Text = geomType;

            int i = 1;
            foreach (DNSMcVector3D coord in coords)
            {
                string strX = coord.x.ToString("#0000000.0");
                string strY = coord.y.ToString("#00000000.0");
                ListViewItem item = new ListViewItem();
                item.Text = i.ToString();
                item.SubItems.Add(strX);
                item.SubItems.Add(strY);
                listView1.Items.Add(item);
                i++;
            }
        }
    }
}
