using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCTester.General_Forms;
using MapCore;
using static MCTester.MainForm;

namespace MCTester.Controls
{
    public partial class CtrlFontButtons : UserControl
    {
        private IDNMcFont mMcFont;
        public delegate void delegateUpdateFontData();
        private delegateUpdateFontData m_delegateFontData;

        public void SetFont(IDNMcFont mcFont)
        {
            mMcFont = mcFont;
            SetFontButtonEnabled();
        }


        public CtrlFontButtons()
        {
            InitializeComponent();
            SetFontButtonEnabled();
        }
        public IDNMcFont GetFont()
        {
            return mMcFont;
        }

        public void SetUpdateDelegate(delegateUpdateFontData _delegateUpdateFontData)
        {
            m_delegateFontData = _delegateUpdateFontData;
        }

        private void btnCreateFont_Click(object sender, EventArgs e)
        {
            ChangeFont(FontTextureSourceForm.CreateDialog, null);
        }

        private void btnUpdateFont_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("The update will be performed in-place influencing other properties using the same font and canceling use-existing option.Re - creating should be used to update this property only and preserve use - existing option.Are you sure you want to continue updating in-place ? ", "In -place Update", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                ChangeFont(FontTextureSourceForm.Update, mMcFont);
        }

        private void btnDeleteFont_Click(object sender, EventArgs e)
        {
            mMcFont = null;
            SetFontButtonEnabled();
            if (m_delegateFontData != null)
                m_delegateFontData();
        }

        private void btnRecreateFont_Click(object sender, EventArgs e)
        {
            ChangeFont(FontTextureSourceForm.Recreate, mMcFont);
        }

        private void ChangeFont(FontTextureSourceForm fontTextureSource, IDNMcFont mcFont = null)
        {
            frmFontDialog fontDlg = new frmFontDialog(fontTextureSource, mcFont);

            // Show the dialog
            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                // Set the new font
                mMcFont = fontDlg.CurrFont;
                if (m_delegateFontData != null)
                    m_delegateFontData();
            }
            SetFontButtonEnabled();
        }

        public void SetFontButtonEnabled()
        {
            btnDeleteFont.Enabled = btnUpdateFont.Enabled = btnRecreateFont.Enabled = mMcFont != null;
        }
    }
}
