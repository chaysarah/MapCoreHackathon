using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;
using MapCore;

namespace MCTester.ButtonsImplementation
{
    public partial class frmFontUpdateBtnImplementation : Form
    {
        private int lstExistingFontCurrIndex;
        private ToolTip mListToolTip;
        private DNMcVariantLogFont m_VarLogFont;
        
        public frmFontUpdateBtnImplementation()
        {
            InitializeComponent();
            lstExistingFontCurrIndex = -1;
            mListToolTip = new ToolTip();
            mListToolTip.ShowAlways = true;
            mListToolTip.UseAnimation = true;
            mListToolTip.UseFading = true;
        }

        private void lstExistingFont_MouseMove(object sender, MouseEventArgs e)
        {
            int itemIndex = -1;

            itemIndex = lstExistingFont.IndexFromPoint(new Point(e.X, e.Y));

            if (itemIndex >= 0)
            {
                if (lstExistingFontCurrIndex != itemIndex || mListToolTip.Active == false)
                {
                    Font font = Font.FromLogFont(((IDNMcLogFont)lstExistingFont.Items[itemIndex]).LogFont.LogFont);
                    if (font != null)
                    {
                        double points = Math.Round(font.SizeInPoints);

                        string caption = "Name: " + font.Name +
                                        "\nSize In Points: " + points.ToString() +
                                        "\nStyle: " + font.Style.ToString() +
                                        "\n Is Created With Use Existing: " + ((IDNMcLogFont)lstExistingFont.Items[itemIndex]).IsCreatedWithUseExisting().ToString();

                        mListToolTip.SetToolTip(lstExistingFont, caption);
                    }
                }
            }
            else
            {
                mListToolTip.Hide(lstExistingFont);
            }

            lstExistingFontCurrIndex = itemIndex;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lstExistingFont.SelectedIndex != -1)
            {
                if (NewFontDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DNSMcLogFont logFont = new DNSMcLogFont();
                        NewFontDialog.Font.ToLogFont(logFont);
                        m_VarLogFont = new DNMcVariantLogFont(logFont, chxIsUnicode.Checked, chxIsEmbedded.Checked);

                        if (m_VarLogFont.LogFont != null)
                        {
                            //((IDNMcLogFont)lstExistingFont.SelectedItem).LogFont = m_VarLogFont;

                            Dictionary<IDNMcFont, uint> dfont = Managers.ObjectWorld.Manager_MCFont.dFont;
                            foreach (IDNMcFont font in dfont.Keys)
                            {
                                if (font == ((IDNMcFont)lstExistingFont.SelectedItem))
                                    ((IDNMcLogFont)font).LogFont = m_VarLogFont;
                            }

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("Update LogFont", McEx);
                    }
                }
            }
            else
                MessageBox.Show("You need to select an existing font in order to perform an update", "Unselected font", MessageBoxButtons.OK, MessageBoxIcon.Error);                
        }

        private void btnDeleteFont_Click(object sender, EventArgs e)
        {
            if (lstExistingFont.SelectedItem != null)
            {
                Managers.ObjectWorld.Manager_MCFont.RemoveFromDictionary((IDNMcFont)lstExistingFont.SelectedItem);
                ((IDNMcFont)lstExistingFont.SelectedItem).Dispose();
                lstExistingFont.Items.RemoveAt(lstExistingFont.SelectedIndex);
            }
        }

        private void frmFontUpdateBtnImplementation_Load(object sender, EventArgs e)
        {
            lstExistingFont.Items.Clear();

            Dictionary<IDNMcFont, uint> font = Managers.ObjectWorld.Manager_MCFont.dFont;
            foreach (IDNMcFont key in font.Keys)
                lstExistingFont.Items.Add(key);

            if (lstExistingFont.Items.Count > 0)
                lstExistingFont.SetSelected(0, true);             
        }

        private void lstExistingFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstExistingFont.SelectedItem != null)
            {
                chxIsUnicode.Checked = ((IDNMcLogFont)lstExistingFont.SelectedItem).LogFont.bIsUnicode;
                chxIsEmbedded.Checked = ((IDNMcLogFont)lstExistingFont.SelectedItem).LogFont.bIsEmbedded;
            }
        }
    }
}
