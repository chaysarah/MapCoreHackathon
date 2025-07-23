using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.General_Forms;

namespace MCTester.Controls
{
    public partial class CtrlPropertyFont : CtrlPropertyButton
    {
        private IDNMcFont m_RegFont;
        private IDNMcFont m_SelFont;
        
        public CtrlPropertyFont()
        {
            InitializeComponent();
        }

        protected override void btnRegFunction_Click(object sender, EventArgs e)
        {
            frmFontDialog fontDlg = new frmFontDialog( MainForm.FontTextureSourceForm.CreateDialog,RegPropertyFont);
            // Show the dialog
            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                // Set the new font
                RegPropertyFont = fontDlg.CurrFont;
            }
        }

        public IDNMcFont RegPropertyFont
        {
            get { return m_RegFont; }
            set { m_RegFont = value; }
        }

        protected override void btnSelFunction_Click(object sender, EventArgs e)
        {
            frmFontDialog fontDlg = new frmFontDialog( MainForm.FontTextureSourceForm.CreateDialog,SelPropertyFont);

            // Show the dialog
            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                // Set the new font
                SelPropertyFont = fontDlg.CurrFont;
            }
        }

        public IDNMcFont SelPropertyFont
        {
            get { return m_SelFont; }
            set { m_SelFont = value; }
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }
    }
}
