using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCTester.General_Forms
{
    public partial class frmRasterorDtmLayerFiles : Form
    {
        public frmRasterorDtmLayerFiles(string path, string[] pastrFilesname)
        {
            InitializeComponent();
            lblFullPath.Text = path;
            lbxFileNames.Items.AddRange(pastrFilesname);
        }
    }
}
