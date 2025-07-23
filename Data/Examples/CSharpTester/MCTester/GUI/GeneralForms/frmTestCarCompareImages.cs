using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MCTester.General_Forms
{
    public partial class frmTestCarCompareImages : Form
    {
        private Image[] images;

        public frmTestCarCompareImages(Image[] imagesToShow)
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            images = imagesToShow;            

        }

        private void frmTestCarCompareImages_Load(object sender, EventArgs e)
        {            
            foreach (Image img in images)
            {
                Form newMDIChild = new Form();
                // Set the Parent Form of the Child window.
                newMDIChild.MdiParent = this;

                // Display the new form.
                newMDIChild.Show();
                newMDIChild.BackgroundImage = img;
            }

            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void frmTestCarCompareImages_SizeChanged(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }        
    }
}
