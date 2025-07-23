using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.GUI.Map;
using MCTester.Managers.MapWorld;
using MCTester.Managers;
using MapCore.Common;
using System.IO;
using MCTester.Managers.ObjectWorld;
using MCTester.MapWorld;
using static MCTester.MainForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ToolTip = System.Windows.Forms.ToolTip;
using MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms;
using ComboBox = System.Windows.Forms.ComboBox;

namespace MCTester.General_Forms
{
    public partial class frmFontDialog : Form
    {
        private IDNMcFont m_Font;
        private Font m_NewFont = null;
        private DNMcVariantLogFont m_VarLogFont;
        private ToolTip mListToolTip;
        private int lstExistingFontCurrIndex;
        private IDNMcLogFont m_currLogFont;
        private IDNMcFileFont m_currFileFont;
        //private DNEFontType m_currFontType;
        private bool m_isNewAction = false;
        private FontTextureSourceForm m_FontSourceForm;
        private DNMcVariantLogFont m_currVariantLogFont;
        private DNSMcFileSource m_currFileSource;
        private int m_currFileHeight;
        private int m_lstExistingFontSelectedIndex = -1;

        private bool m_IsUseExisting;
        private bool m_IsStaticFont;
        private DNSCharactersRange[] m_CharRange;
        private uint m_MaxNumCharsInDynamicAtlas;
        private uint m_CharacterSpacing;
        private uint m_NumAntialiasingAlphaLevels;
        private uint m_OutlineWidth;
        private bool m_IsSelectListTab = false;
        private bool m_IsLoad = false;

        private int m_colSpecialCharCodeLetter = 0;
        private int m_colSpecialCharCodeDecimalValue = 1;
        private int m_colSpecialCharTexture = 2;
        private int m_colSpecialCharSizeParamMeaning = 3;
        private int m_colSpecialCharWidth = 4;
        private int m_colSpecialCharHeight = 5;
        private int m_colSpecialCharOffset = 6;
        private int m_colSpecialCharLeftSpacing = 7;
        private int m_colSpecialCharRightSpacing = 8;

        private DNSSpecialChar[] m_SpecialChars = null;

        enum CellValue { Empty, Valid, Invalid}

        public frmFontDialog(MainForm.FontTextureSourceForm FontSourceForm)
        {
            InitializeComponent();
            m_isNewAction = true;
            lstExistingFontCurrIndex = -1;
            m_NewFont = new Font("Arial", 10F);
            UpdateFontDetails();
            mListToolTip = new ToolTip();
            mListToolTip.ShowAlways = true;
            mListToolTip.UseAnimation = true;
            mListToolTip.UseFading = true;
            ctrlBrowseFileFont.Filter = "Font files (*.ttf)|*.ttf;*.ttc;*.fon;| All Files|*.*";
            ctrlBrowseFileFont.FileName = @"c:\windows\fonts\";
            btnUseSelected.Enabled = false;
            m_FontSourceForm = FontSourceForm;
            chxSetAsDefault.Visible = FontSourceForm == FontTextureSourceForm.CreateOnMap;
            chxSetAsDefault.Checked = FontDialog_SetAsDefault_UserSelected;

            SetGuiFontDefaultParams();

            DataGridViewComboBoxColumn colCombo = (dgvSpecialChars.Columns[m_colSpecialCharSizeParamMeaning] as DataGridViewComboBoxColumn);
            colCombo.Items.Clear();
            colCombo.Items.AddRange(Enum.GetNames(typeof(DNESpecialCharSizeMeaning)));
        }

        public frmFontDialog(FontTextureSourceForm FontSourceForm, IDNMcFont currFont) : this(FontSourceForm)
        {
            CurrFont = currFont;

            m_FontSourceForm = FontSourceForm;

            m_isNewAction = (m_FontSourceForm == FontTextureSourceForm.CreateOnMap ||
                             m_FontSourceForm == FontTextureSourceForm.CreateDialog ||
                             m_FontSourceForm == FontTextureSourceForm.Recreate);

           if (CurrFont != null)
           {
                btnCreate.Text = "Re-create";
                if (m_FontSourceForm == FontTextureSourceForm.Update)
                {
                    btnCreate.Text = "Update";
                    foreach (TabPage currentTab in tabFonts.TabPages)
                    {
                        currentTab.Enabled = false;
                    }
                    gbFontParams.Enabled = false;
                }
                    
                TabPage currTab = null;
                int tabIndex = -1;
                switch (CurrFont.FontType)
                {
                    case DNEFontType._EFT_LOG_FONT:
                        m_currLogFont = (IDNMcLogFont)CurrFont;
                        try
                        {
                            m_currVariantLogFont = m_currLogFont.LogFont;
                            m_NewFont = Font.FromLogFont(m_currVariantLogFont.LogFont);
                            lblSelectNewFont.Text = "(Selected)";
                            UpdateLogFontDetails(CurrFont);
                           
                            UpdateFontDetails();
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("font params", McEx);
                        }

                        tabIndex = 0;
                        break;
                    case DNEFontType._EFT_FILE_FONT:
                        m_currFileFont = (IDNMcFileFont)CurrFont;
                        GetFontFileAndHeight();
                        ctrlBrowseFileFont.FileName = m_currFileSource.strFileName;
                        ntbFontHeight.SetInt(m_currFileHeight);
                        cbIsMemoryBuffer.Checked = m_currFileSource.bIsMemoryBuffer;
                        tabIndex = 1;
                        break;
                }
                if (tabIndex >= 0)
                {
                    currTab = tabFonts.TabPages[tabIndex];
                    currTab.Enabled = true;
                    tabFonts.SelectedTab = currTab;
                }
            }
            UpdateGuiFontParams(CurrFont);
        }

        public void SetReadOnly()
        {
            GeneralFuncs.SetControlsReadonly(this);
            btnDeleteFont.Enabled = btnUseSelected.Enabled = btnCreate.Enabled = false;
        }

        private void UpdateFontDetails()
        {
            if (m_NewFont != null)
            {
                NewFontDialog.Font = m_NewFont;
                lblLogName.Text = m_NewFont.Name;
                lblSizeInPoints.Text = Math.Round(m_NewFont.SizeInPoints).ToString();
                lblStyle.Text = m_NewFont.Style.ToString();
            }
        }

        private void UpdateLogFontDetails(IDNMcFont font)
        {
            if (font != null && font.FontType == DNEFontType._EFT_LOG_FONT)
            {
                chxIsUnicode.Checked = ((IDNMcLogFont)font).LogFont.bIsUnicode;
                chxIsEmbedded.Checked = ((IDNMcLogFont)font).LogFont.bIsEmbedded;
            }
        }

        public IDNMcFont CurrFont
        {
            get { return m_Font; }
            set { m_Font = value; }
        }

        bool? existingUsed = false;

        private CellValue CheckFloatCell(int i, int c, string s, out float fResult)
        {
            object cell1 = dgvSpecialChars[c, i].Value;
            CellValue cellValue = cell1 == null ? CellValue.Empty : CellValue.Valid;
            fResult = 0f;
            bool bResult = true;
            if(cell1 != null) 
                bResult =  float.TryParse(cell1.ToString(), out fResult);

            if (cell1 != null && !bResult)
            {
                cellValue = CellValue.Invalid;
                MessageBox.Show("Invalid value in column '"+ s + "' in row " + i.ToString() + " , fix it and try again", "Get Special Char");
            }
            return cellValue;
        }

        private CellValue CheckIntCell(int i, int c, string s, out int iResult)
        {
            object cell1 = dgvSpecialChars[c, i].Value;
            CellValue cellValue = cell1 == null ? CellValue.Empty : CellValue.Valid;
            iResult = 0;
            bool bResult = true;
            if(cell1 != null )
                bResult = int.TryParse(cell1.ToString(), out iResult);

            if (cell1 != null && !bResult)
            {
                cellValue = CellValue.Invalid;
                MessageBox.Show("Invalid value in column '" + s + "' in row " + i.ToString() + " , fix it and try again", "Get Special Char");
            }
            return cellValue;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if(chxSetAsDefault.Visible)
                MainForm.FontDialog_SetAsDefault_UserSelected = chxSetAsDefault.Checked;

            DNSCharactersRange[] charRange = GetCharRange();

            if ((m_FontSourceForm == MainForm.FontTextureSourceForm.CreateOnMap || m_FontSourceForm == MainForm.FontTextureSourceForm.CreateDialog) && MCTMapDevice.CurrDevice == null)
            {
                MessageBox.Show("This operation required a device", "Device Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // get Special char
            DNSSpecialChar[] specialChars = GetSpecialChars(true);
            if (specialChars == null && dgvSpecialChars.Rows.Count > 1)  // there is invalid data in dgvSpecialChars
                return;

            switch (tabFonts.SelectedIndex)
            {
                case 0:
                    if (m_isNewAction)   // is new log font
                    {
                        if (m_NewFont == null)
                        {
                            MessageBox.Show("You need to create new font previously", "No font created", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            DNSMcLogFont logFont = new DNSMcLogFont();
                            m_NewFont.ToLogFont(logFont);
                            m_VarLogFont = new DNMcVariantLogFont(logFont, chxIsUnicode.Checked, chxIsEmbedded.Checked);

                            try
                            {
                                CurrFont = DNMcLogFont.Create(m_VarLogFont,
                                                                chxStaticFont.Checked,
                                                                charRange,
                                                                ntxMaxNumCharsInDynamicAtlas.GetUInt32(),
                                                                chxUseExisting.Checked,
                                                                ref existingUsed,
                                                                ntxOutlineWidth.GetUInt32(),
                                                                specialChars,
                                                                chxUseSpecialCharsColors.Checked);

                                
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("Create New Log Font", McEx);
                                return;
                            }
                        }
                    }
                    else    // is update exist font
                    {
                        if (CurrFont is IDNMcLogFont)
                        {
                            try
                            {
                                DNSMcLogFont logFont = new DNSMcLogFont();
                                NewFontDialog.Font.ToLogFont(logFont);
                                DNMcVariantLogFont m_VarLogFont = new DNMcVariantLogFont(logFont, chxIsUnicode.Checked, chxIsEmbedded.Checked);
                                Dictionary<IDNMcFont, uint> dfont = Managers.ObjectWorld.Manager_MCFont.dFont;
                                foreach (IDNMcFont font in dfont.Keys)
                                {
                                    if (font == ((IDNMcFont)lstExistingFont.SelectedItem))
                                    {
                                        if (font is IDNMcLogFont)
                                        {
                                            ((IDNMcLogFont)font).LogFont = m_VarLogFont;
                                        }
                                    }
                                }
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("Set LogFont", McEx);
                                return;
                            }
                        }
                    }
                    break;
                case 1:
                    if (m_isNewAction)
                    {
                        string fileName = ctrlBrowseFileFont.FileName;
                        if (GetFileSourceFromUI())
                        {
                            int height = ntbFontHeight.GetInt32();

                            try
                            {
                                CurrFont = DNMcFileFont.Create(m_currFileSource,
                                                                height,
                                                                chxStaticFont.Checked,
                                                                charRange,
                                                                ntxMaxNumCharsInDynamicAtlas.GetUInt32(),
                                                                chxUseExisting.Checked,
                                                                ref existingUsed,
                                                                ntxOutlineWidth.GetUInt32(),
                                                                specialChars,
                                                                chxUseSpecialCharsColors.Checked);
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("Create New FileFont", McEx);
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (CurrFont is IDNMcFileFont)
                        {
                            if (GetFileSourceFromUI())
                            {
                                int height = ntbFontHeight.GetInt32();
                                try
                                {
                                    m_currFileFont.SetFontFileAndHeight(m_currFileSource, height);
                                }
                                catch (MapCoreException McEx)
                                {
                                    MapCore.Common.Utilities.ShowErrorMessage("GetFontFileAndHeight", McEx);
                                    return;
                                }
                            }
                            else
                                return;
                        }
                    }
                    break;
                case 2:
                    if (CurrFont != null)
                    {
                        try
                        {
                            bool? existingUsed = false;

                            IDNMcFont font = (IDNMcFont)lstExistingFont.SelectedItem;
                            if (font is IDNMcLogFont)
                            {
                                CurrFont = DNMcLogFont.Create(((IDNMcLogFont)font).LogFont,
                                                                font.GetIsStaticFont(),
                                                                font.GetCharactersRanges(),
                                                                font.GetMaxNumCharsInDynamicAtlas(),
                                                                font.IsCreatedWithUseExisting(),
                                                                ref existingUsed,
                                                                font.GetTextOutlineWidth(),
                                                                specialChars,
                                                                chxUseSpecialCharsColors.Checked);
                            }
                            else if (font is IDNMcFileFont)
                            {
                                CurrFont = DNMcFileFont.Create(m_currFileSource,
                                                               m_currFileHeight,
                                                               font.GetIsStaticFont(),
                                                               font.GetCharactersRanges(),
                                                               font.GetMaxNumCharsInDynamicAtlas(),
                                                               font.IsCreatedWithUseExisting(),
                                                               ref existingUsed,
                                                               font.GetTextOutlineWidth(),
                                                               specialChars,
                                                                chxUseSpecialCharsColors.Checked);
                            }
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("Create Identical", McEx);
                            return;
                        }
                    }
                    else
                        MessageBox.Show("Existing text is not selected in order to create identical", "Existing text is not selected", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;
            }
            if (chxSetAsDefault.Visible && chxSetAsDefault.Checked)
                ObjectPropertiesBase.TextFont = CurrFont;

            CloseFormAfterCreate();
        }



        private bool GetFileSourceFromUI()
        {
            string fileName = ctrlBrowseFileFont.FileName;

            if (m_FontSourceForm == FontTextureSourceForm.CreateDialog || m_FontSourceForm == FontTextureSourceForm.CreateOnMap)
                m_currFileSource = new DNSMcFileSource();

            m_currFileSource.bIsMemoryBuffer = cbIsMemoryBuffer.Checked;
            if (!cbIsMemoryBuffer.Checked)
                m_currFileSource.strFileName = fileName;
            else
            {
                try
                {
                    if (fileName != "")
                    {
                        byte[] fileByte = File.ReadAllBytes(fileName);
                        m_currFileSource.aFileMemoryBuffer = fileByte;
                        m_currFileSource.bIsMemoryBuffer = true;
                    }
                    else
                    {
                        MessageBox.Show("File Name is Missing", "No File Choose", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("File Name is Wrong", "No File Choose", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }


            return true;

        }

        private void GetFontFileAndHeight()
        {
            try
            {
                m_currFileFont.GetFontFileAndHeight(out m_currFileSource, out m_currFileHeight);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetFontFileAndHeight", McEx);
            }
        }

        private void CloseFormAfterCreate()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();

            if (existingUsed == true)
                MessageBox.Show("This text is existing used based", "Existing Used Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                Managers.ObjectWorld.Manager_MCFont.AddToDictionary(CurrFont);
        }

        private DNSCharactersRange[] GetCharRange()
        {
            int currRowNum = 0;
            DNSCharactersRange[] charRange = new DNSCharactersRange[dgvLetterCharactersRanges.Rows.Count - 1];
            while (!dgvLetterCharactersRanges.Rows[currRowNum].IsNewRow)
            {
                charRange[currRowNum].nFrom = (dgvLetterCharactersRanges[0, currRowNum].Value != null) ? char.Parse(dgvLetterCharactersRanges[0, currRowNum].Value.ToString()) : (char)0;
                charRange[currRowNum].nTo = (dgvLetterCharactersRanges[1, currRowNum].Value != null) ? char.Parse(dgvLetterCharactersRanges[1, currRowNum].Value.ToString()) : (char)0;

                currRowNum++;
            }
            return charRange;
        }

        private void SetCharRange(DNSCharactersRange[] charRange)
        {
            if (charRange != null && charRange.Length > 0)
            {
                dgvLetterCharactersRanges.Rows.Add(charRange.Length);

                for (int i = 0; i < charRange.Length; i++)
                {
                    dgvLetterCharactersRanges[0, i].Value = charRange[i].nFrom;
                    dgvLetterCharactersRanges[1, i].Value = charRange[i].nTo;
                }

                dgvNumericCharactersRanges.Rows.Add(charRange.Length);

                for (int i = 0; i < charRange.Length; i++)
                {
                    dgvNumericCharactersRanges[0, i].Value = ((int)charRange[i].nFrom).ToString();
                    dgvNumericCharactersRanges[1, i].Value = ((int)charRange[i].nTo).ToString();
                }
            }
        }

        private void btnUseSelected_Click(object sender, EventArgs e)
        {
            try
            {
                CurrFont = (IDNMcFont)lstExistingFont.SelectedItem;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("", McEx);
            }
        }

        private void frmFontDialog_Load(object sender, EventArgs e)
        {
            m_IsLoad = true;
            lstExistingFont.Items.Clear();

            Dictionary<IDNMcFont, uint> font = Managers.ObjectWorld.Manager_MCFont.dFont;

            foreach (IDNMcFont key in font.Keys)
                lstExistingFont.Items.Add(key);

            if (CurrFont != null)
            {
                for (int i = 0; i < lstExistingFont.Items.Count; i++)
                {
                    if ((IDNMcFont)lstExistingFont.Items[i] == CurrFont)
                    {
                        lstExistingFont.SetSelected(i, true);
                    }
                }

                //UpdateGuiFontParams(CurrFont);
            }
            m_IsLoad = false;
        }

        private void btnDeleteFont_Click(object sender, EventArgs e)
        {
            if (lstExistingFont.SelectedItem != null)
            {
                if (ObjectPropertiesBase.TextFont == lstExistingFont.SelectedItem)
                    ObjectPropertiesBase.TextFont = null;
                Managers.ObjectWorld.Manager_MCFont.RemoveFromDictionary((IDNMcFont)lstExistingFont.SelectedItem);
                ((IDNMcFont)lstExistingFont.SelectedItem).Dispose();
                lstExistingFont.Items.RemoveAt(lstExistingFont.SelectedIndex);
            }
        }

        private void btnNewFont_Click(object sender, EventArgs e)
        {
            if (m_NewFont != null)
                NewFontDialog.Font = m_NewFont;
            if (NewFontDialog.ShowDialog() == DialogResult.OK)
            {
                m_NewFont = NewFontDialog.Font;
                lblSelectNewFont.Text = "(Selected)";
                UpdateFontDetails();
            }
        }

        private void SetGuiFontDefaultParams()
        {
            dgvLetterCharactersRanges.Rows.Clear();
            dgvNumericCharactersRanges.Rows.Clear();
            dgvSpecialChars.Rows.Clear();
            ntxCharacterSpacing.SetUInt32(Manager_MCGeneralDefinitions.CharacterSpacing);
            ntxNumAntialiasingAlphaLevels.SetUInt32(Manager_MCGeneralDefinitions.NumAntialiasingAlphaLevels);
            ntxOutlineWidth.SetUInt32(1);

        }

        private void SetGuiFontUserParams()
        {
            if (m_IsSelectListTab)
            {
                chxUseExisting.Checked = m_IsUseExisting;
                chxStaticFont.Checked = m_IsStaticFont;
                dgvLetterCharactersRanges.Rows.Clear();
                dgvNumericCharactersRanges.Rows.Clear();
                SetCharRange(m_CharRange);
                ntxCharacterSpacing.SetUInt32(m_CharacterSpacing);
                ntxNumAntialiasingAlphaLevels.SetUInt32(m_NumAntialiasingAlphaLevels);
                ntxOutlineWidth.SetUInt32(m_OutlineWidth);
                ntxMaxNumCharsInDynamicAtlas.SetUInt32(m_MaxNumCharsInDynamicAtlas);
                SetSpecialChars(m_SpecialChars);
            }
        }


        private void SaveGuiFontParams()
        {
            m_IsSelectListTab = true;  // user select list tab

            m_IsUseExisting = chxUseExisting.Checked;
            m_IsStaticFont = chxStaticFont.Checked;
            m_CharRange = GetCharRange();
            m_MaxNumCharsInDynamicAtlas = ntxMaxNumCharsInDynamicAtlas.GetUInt32();
            m_CharacterSpacing = ntxCharacterSpacing.GetUInt32();
            m_NumAntialiasingAlphaLevels = ntxNumAntialiasingAlphaLevels.GetUInt32();
            m_OutlineWidth = ntxOutlineWidth.GetUInt32();
            m_SpecialChars = GetSpecialChars(false);
        }

        private DNSSpecialChar[] GetSpecialChars( bool isShowMsg)
        {
            DNSSpecialChar[] specialChars = new DNSSpecialChar[dgvSpecialChars.Rows.Count - 1];
            for (int i = 0; i < dgvSpecialChars.Rows.Count; i++)
            {
                if (!dgvSpecialChars.Rows[i].IsNewRow)
                {
                    DNSSpecialChar specialChar = new DNSSpecialChar();

                    object cell1 = dgvSpecialChars[m_colSpecialCharCodeLetter, i].Value;
                    char cResult = '\0';
                    bool bResult = cell1 != null ? char.TryParse(cell1.ToString(), out cResult) : false;
                    if ((cell1 == null || !bResult) && isShowMsg)
                    {
                        MessageBox.Show("Invalid value in column 'Char Code' in row " + i.ToString() + " , fix it and try again", "Get Special Char");
                        return null;
                    }
                    specialChar.uCharCode = cResult;

                    cell1 = dgvSpecialChars[m_colSpecialCharSizeParamMeaning, i].Value;
                    if (cell1 != null && cell1.ToString() != "")
                        specialChar.eSizeParamMeaning = (DNESpecialCharSizeMeaning)Enum.Parse(typeof(DNESpecialCharSizeMeaning), cell1.ToString());

                    if (dgvSpecialChars.Rows[i].Tag == null && isShowMsg)
                    {
                        MessageBox.Show("Missing image in row " + i.ToString() + " , fix it and try again", "Get Special Char");
                        return null;
                    }
                    specialChar.pImage = (IDNMcImage)dgvSpecialChars.Rows[i].Tag;

                    float fResult = 0;
                    CellValue cellValue = CheckFloatCell(i, m_colSpecialCharWidth, "Char Width", out fResult);
                    if (cellValue == CellValue.Valid)
                        specialChar.fCharWidthParam = fResult;
                    else if (cellValue == CellValue.Invalid)
                        return null;
                    cellValue = CheckFloatCell(i, m_colSpecialCharHeight, "Char Height", out fResult);
                    if (cellValue == CellValue.Valid)
                        specialChar.fCharHeightParam = fResult;
                    else if (cellValue == CellValue.Invalid)
                        return null;

                    int iResult = 0;
                    cellValue = CheckIntCell(i, m_colSpecialCharOffset, "Vertical Offset", out iResult);
                    if (cellValue == CellValue.Valid)
                        specialChar.nVerticalOffset = iResult;
                    else if (cellValue == CellValue.Invalid)
                        return null;
                    cellValue = CheckIntCell(i, m_colSpecialCharLeftSpacing, "Left Spacing", out iResult);
                    if (cellValue == CellValue.Valid)
                        specialChar.nLeftSpacing = iResult;
                    else if (cellValue == CellValue.Invalid)
                        return null;
                    cellValue = CheckIntCell(i, m_colSpecialCharRightSpacing, "Right Spacing", out iResult);
                    if (cellValue == CellValue.Valid)
                        specialChar.nRightSpacing = iResult;
                    else if (cellValue == CellValue.Invalid)
                        return null;

                   

                    specialChars[i] = specialChar;
                }
            }
            return specialChars;
        }

        private void UpdateGuiFontParams(IDNMcFont font )
        {
            SetGuiFontDefaultParams();

            if (font != null)
            {
                DNSCharactersRange[] charRange = null;
                try
                { 
                   
                    charRange = font.GetCharactersRanges();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetCharactersRanges", McEx);
                }

                SetCharRange(charRange);

                try
                {
                    chxUseExisting.Checked = font.IsCreatedWithUseExisting();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("IsCreatedWithUseExisting", McEx);
                }

                try
                {
                    chxStaticFont.Checked = font.GetIsStaticFont();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetIsStaticFont", McEx);
                }

                try
                {
                    ntxMaxNumCharsInDynamicAtlas.SetUInt32(font.GetMaxNumCharsInDynamicAtlas());
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetMaxNumCharsInDynamicAtlas", McEx);
                }

                try
                {
                    ntxCharacterSpacing.SetUInt32(font.GetEffectiveCharacterSpacing());
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetEffectiveCharacterSpacing", McEx);
                }

                try
                {
                    ntxNumAntialiasingAlphaLevels.SetUInt32(font.GetEffectiveNumAntialiasingAlphaLevels());
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetEffectiveNumAntialiasingAlphaLevels", McEx);
                }

                try
                {
                    ntxOutlineWidth.SetUInt32(font.GetTextOutlineWidth());
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetTextOutlineWidth", McEx);
                }

                try
                {
                    bool bUseSpecialCharsColors;
                    DNSSpecialChar[] specialChars = null;
                    font.GetSpecialChars(out specialChars, out bUseSpecialCharsColors);
                    chxUseSpecialCharsColors.Checked = bUseSpecialCharsColors;
                    SetSpecialChars(specialChars);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetSpecialChars", McEx);
                }
            }

        }

        private void SetSpecialChars(DNSSpecialChar[] specialChars)
        {
            dgvSpecialChars.Rows.Clear();
            if (specialChars != null && specialChars.Length > 0)
            {
                dgvSpecialChars.Rows.Add(specialChars.Length);
                for (int i = 0; i < specialChars.Length; i++)
                {
                    if (specialChars[i].pImage != null)
                    {
                        dgvSpecialChars.Rows[i].Tag = specialChars[i].pImage;
                        string imageName = "Selected";
                        DNSMcFileSource mcFileSource = specialChars[i].pImage.GetFileSource();
                        if (mcFileSource.strFileName != null && mcFileSource.strFileName != "")
                            imageName = mcFileSource.strFileName;
                        dgvSpecialChars[m_colSpecialCharTexture, i].Value = imageName;
                    }
                    else
                        dgvSpecialChars[m_colSpecialCharTexture, i].Value = "Null";

                    dgvSpecialChars[m_colSpecialCharWidth, i].Value = specialChars[i].fCharWidthParam;
                    dgvSpecialChars[m_colSpecialCharHeight, i].Value = specialChars[i].fCharHeightParam;
                    dgvSpecialChars[m_colSpecialCharOffset, i].Value = specialChars[i].nVerticalOffset;
                    dgvSpecialChars[m_colSpecialCharLeftSpacing, i].Value = specialChars[i].nLeftSpacing;
                    dgvSpecialChars[m_colSpecialCharRightSpacing, i].Value = specialChars[i].nRightSpacing;
                    dgvSpecialChars[m_colSpecialCharCodeLetter, i].Value = specialChars[i].uCharCode;
                    dgvSpecialChars[m_colSpecialCharCodeDecimalValue, i].Value = ((int)specialChars[i].uCharCode).ToString();  
                    dgvSpecialChars[m_colSpecialCharSizeParamMeaning, i].Value = specialChars[i].eSizeParamMeaning.ToString();
                }
            }
        }

        private void lstExistingFont_MouseMove(object sender, MouseEventArgs e)
        {
            int itemIndex = -1;

            itemIndex = lstExistingFont.IndexFromPoint(new Point(e.X, e.Y));

            if (itemIndex >= 0)
            {
                if (lstExistingFontCurrIndex != itemIndex || mListToolTip.Active == false)
                {
                    IDNMcFont font = ((IDNMcFont)lstExistingFont.Items[itemIndex]);
                    string caption = "";
                    if (font is IDNMcLogFont)
                    {
                        IDNMcLogFont logFont = (IDNMcLogFont)font;
                        Font systemFont = Font.FromLogFont(logFont.LogFont.LogFont);
                        if (systemFont != null)
                        {
                            double points = Math.Round(systemFont.SizeInPoints);

                            caption = "Name: " + systemFont.Name +
                                            "\nSize In Points: " + points.ToString() +
                                            "\nStyle: " + systemFont.Style.ToString() +
                                            "\n Is Created With Use Existing: " + logFont.IsCreatedWithUseExisting().ToString();
                        }
                    }
                    else if (font is IDNMcFileFont)
                    {
                        IDNMcFileFont fileFont = (IDNMcFileFont)font;
                        DNSMcFileSource fileSource; int height;
                        fileFont.GetFontFileAndHeight(out fileSource, out height);
                        caption = "File Name: " + fileSource.strFileName +
                            "\nHeight: " + height.ToString() +
                            "\nIs Memory Buffer: " + fileSource.bIsMemoryBuffer.ToString() +
                            "\nIs Created With Use Existing: " + fileFont.IsCreatedWithUseExisting().ToString();
                    }
                    mListToolTip.SetToolTip(lstExistingFont, caption);
                }
            }
            else
            {
                mListToolTip.Hide(lstExistingFont);
            }

            lstExistingFontCurrIndex = itemIndex;
        }

        private void dgvLetterCharactersRanges_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLetterCharactersRanges[e.ColumnIndex, e.RowIndex].Value != null)
            {
                char result;
                bool parseSuccess = char.TryParse(dgvLetterCharactersRanges[e.ColumnIndex, e.RowIndex].Value.ToString(), out result);

                if (parseSuccess == true)
                    dgvNumericCharactersRanges[e.ColumnIndex, e.RowIndex].Value = Convert.ToInt16(result).ToString();
                else
                    dgvNumericCharactersRanges[e.ColumnIndex, e.RowIndex].Value = 0;
            }
        }

        private void dgvNumericCharactersRanges_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNumericCharactersRanges[e.ColumnIndex, e.RowIndex].Value != null)
            {
                short result;

                bool parseSuccess = Int16.TryParse(dgvNumericCharactersRanges[e.ColumnIndex, e.RowIndex].Value.ToString(), out result);

                if (parseSuccess == true)
                    dgvLetterCharactersRanges[e.ColumnIndex, e.RowIndex].Value = Convert.ToChar(result).ToString();
                else
                    dgvLetterCharactersRanges[e.ColumnIndex, e.RowIndex].Value = 0;
            }
        }

        private void dgvLetterCharactersRanges_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            dgvNumericCharactersRanges.Rows.Insert(dgvNumericCharactersRanges.Rows.Count - 1, 1);
        }

        private void dgvNumericCharactersRanges_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            dgvLetterCharactersRanges.Rows.Insert(dgvLetterCharactersRanges.Rows.Count - 1, 1);
        }

        private void lstExistingFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_IsLoad)
            {
                UpdateGuiFontParams((IDNMcFont)lstExistingFont.SelectedItem);
                m_lstExistingFontSelectedIndex = lstExistingFont.SelectedIndex;
            }
        }

        private void dgvLetterCharactersRanges_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            dgvNumericCharactersRanges.Rows.RemoveAt(dgvLetterCharactersRanges.CurrentRow.Index);
        }

        private void dgvNumericCharactersRanges_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            dgvLetterCharactersRanges.Rows.RemoveAt(dgvNumericCharactersRanges.CurrentRow.Index);
        }


        private void tabFonts_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    SetGuiFontUserParams();
                    ChangeCreateBttnText();
                    btnUseSelected.Enabled = false;
                    gbFontParams.Enabled = (CurrFont == null || m_isNewAction);
                    break;
                case 1:
                    SetGuiFontUserParams();
                    ChangeCreateBttnText();
                    btnUseSelected.Enabled = false;
                    gbFontParams.Enabled = CurrFont == null || m_isNewAction;
                    break;
                case 2:
                    SaveGuiFontParams();
                    if(m_lstExistingFontSelectedIndex >= 0 && lstExistingFont.Items.Count > m_lstExistingFontSelectedIndex)
                        lstExistingFont.SetSelected(m_lstExistingFontSelectedIndex, true);
                    btnCreate.Text = "Create Identical";
                    btnUseSelected.Enabled = true;
                    gbFontParams.Enabled = false;
                   // todo dgvSpecialChars.Columns[m_colSpecialCharTexture].ReadOnly = false;
                    break;
            }
        }

        private void ChangeCreateBttnText()
        {

            if (CurrFont == null)
                btnCreate.Text = "Create";
            else
                btnCreate.Text = "Update";
        }

        private void tabFonts_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!m_isNewAction)
            {
                if (e.TabPageIndex == 2)
                    tabFonts.DeselectTab(2);
            }
        }

        private void lstExistingFont_DoubleClick(object sender, EventArgs e)
        {
            int index = lstExistingFont.SelectedIndex;
            if (index >= 0)
            {
                IDNMcFont font = ((IDNMcFont)lstExistingFont.SelectedItem);
                frmFontDialog fontDlg = new frmFontDialog(FontTextureSourceForm.Update, font);
                // Show the dialog
                fontDlg.ShowDialog();
            }
        }

        private void dgvSpecialChars_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == m_colSpecialCharTexture && e.RowIndex >= 0)
            {
                object objImage = dgvSpecialChars.Rows[e.RowIndex].Tag;
                List<IDNMcImage> images = new List<IDNMcImage>();
                for(int i = 0; i< dgvSpecialChars.Rows.Count; i++)
                {
                    object tmpImage = dgvSpecialChars.Rows[i].Tag;
                    if(tmpImage != null && !images.Contains(tmpImage as  IDNMcImage))
                        images.Add(tmpImage as IDNMcImage);
                }

                frmImageDialog frmImageDialog = new frmImageDialog( objImage != null ? (IDNMcImage)objImage : null);
                frmImageDialog.SetExistingImages(images);
                if(frmImageDialog.ShowDialog() == DialogResult.OK)
                {
                    string text = "Null";
                    if (frmImageDialog.GetImage() != null)
                    {
                        string fileName = frmImageDialog.GetFileName();
                        if(fileName == "")
                        {
                            try
                            {
                                DNSMcFileSource mcFileSource = frmImageDialog.GetImage().GetFileSource();
                                if (String.IsNullOrEmpty(mcFileSource.strFileName))
                                    text = "Selected";
                                else
                                    text = mcFileSource.strFileName;
                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("GetFileSource", McEx);
                            }
                        }
                        else
                            text = fileName;

                    }
                    if (e.RowIndex == dgvSpecialChars.RowCount - 1 || dgvSpecialChars.Rows[dgvSpecialChars.RowCount - 1].IsNewRow == false)
                    {
                        dgvSpecialChars.Rows.Add();
                    }
                    (dgvSpecialChars[e.ColumnIndex, e.RowIndex] as DataGridViewButtonCell).Value = text;

                    dgvSpecialChars.Rows[e.RowIndex].Tag = frmImageDialog.GetImage();
                }
            }
        }

        private void dgvSpecialChars_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (dgvSpecialChars[m_colSpecialCharSizeParamMeaning, e.RowIndex].Value == null)
                dgvSpecialChars[m_colSpecialCharSizeParamMeaning, e.RowIndex].Value = DNESpecialCharSizeMeaning._ESCSM_FACTOR_OF_FONT_HEIGHT.ToString();
        }

        private void dgvSpecialChars_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgvSpecialChars_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSpecialChars[e.ColumnIndex, e.RowIndex].Value != null)
            {
                if (e.ColumnIndex == m_colSpecialCharCodeLetter)
                {
                    char result;
                    bool parseSuccess = char.TryParse(dgvSpecialChars[e.ColumnIndex, e.RowIndex].Value.ToString(), out result);

                    if (parseSuccess == true)
                        dgvSpecialChars[m_colSpecialCharCodeDecimalValue, e.RowIndex].Value = Convert.ToInt16(result).ToString();
                    else
                        dgvSpecialChars[m_colSpecialCharCodeDecimalValue, e.RowIndex].Value = 0;
                }
                else
                {
                    if(e.ColumnIndex == m_colSpecialCharCodeDecimalValue)
                    {
                        short result;

                        bool parseSuccess = Int16.TryParse(dgvSpecialChars[e.ColumnIndex, e.RowIndex].Value.ToString(), out result);

                        if (parseSuccess == true)
                            dgvSpecialChars[m_colSpecialCharCodeLetter, e.RowIndex].Value = Convert.ToChar(result).ToString();
                        else
                            dgvSpecialChars[m_colSpecialCharCodeLetter, e.RowIndex].Value = 0;
                    }
                }
            }
        }
    }
}