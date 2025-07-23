using MapCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCTester.Controls
{
    public partial class frmModifier : Form
    {
        public frmModifier(string[] names)
        {
            InitializeComponent();
            cmbNames.Items.AddRange(names);
            cmbTypes.Items.AddRange(Enum.GetNames(typeof(DNEPropertyType)));
        }

        private void cmbNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtName.Text =  cmbNames.SelectedText;
                        
        }

        private void cmbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DNEPropertyType type = (DNEPropertyType)Enum.Parse(typeof(DNEPropertyType), cmbTypes.Text);

            rbFont.Checked = type == DNEPropertyType._EPT_FONT;
            rbTexture.Checked = type == DNEPropertyType._EPT_TEXTURE;
            rbOther.Checked = (type != DNEPropertyType._EPT_TEXTURE && type != DNEPropertyType._EPT_FONT);

        }
    }
}
