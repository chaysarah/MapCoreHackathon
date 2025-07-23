using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCTester.ButtonsImplementation
{
    public partial class frmStaticObjectsQuestion : Form
    {
        public frmStaticObjectsQuestion()
        {
            InitializeComponent();
                   
        }
        // Layer.Title
        public frmStaticObjectsQuestion(string layerTitle):this()
        {
            lblUserMsg.Text = "This server cannot distinguish between different types of static-objects layers." + Environment.NewLine + "Select the right type for layer '" + layerTitle +"'";

        }
        private void btnNativeServer3DModel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnNativeServerVector3DExtrusion_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }
    }
}
