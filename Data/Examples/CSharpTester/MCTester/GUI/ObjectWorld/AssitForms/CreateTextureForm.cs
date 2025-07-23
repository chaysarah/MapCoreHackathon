/*
Documentation Date	05/03/07
Documented By	Lihi Alter
Form Name	Create texture
Parent	Edit 
Description	In this form the user can define a texture for picture/line/fill. There are 4 options to create the texture: from image file, from resource, hicon and hbitmap. 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;
using MapCore;
using System.IO;
using System.Drawing.Imaging;
using MCTester.GUI.Forms;
using MCTester.Controls;
using MapCore.Common;
using MCTester.Managers;
using MCTester.Managers.ObjectWorld;
using System.Runtime.InteropServices;
using static MCTester.MainForm;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class CreateTextureForm : CreateFabricForm
    {
        #region Data members
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        private bool m_isCreateFromTextureArray;
        private FontTextureSourceForm m_TextureSourceForm;
        private bool m_isNewAction = false;

        private string m_textureName;
        private IDNMcTexture m_currentTexture;
        private IDNMcImageFileTexture m_imageTexture;
        private IDNMcIconHandleTexture m_iconTexture;
        private IDNMcResourceTexture m_resourceTexture;
        private IDNMcBitmapHandleTexture m_bitmapTexture;
        private IDNMcDirectShowGraphTexture m_directShowGraphTexture;
        private IDNMcDirectShowSourceFileTexture m_directShowSourceFileTexture;
        private IDNMcDirectShowUSBCameraTexture m_directShowUSBCameraTexture;
        private IDNMcFFMpegVideoTexture m_FFMpegVideoTexture;
        private IDNMcMemoryBufferTexture m_MemoryBufferTexture;
        private IDNMcSharedMemoryVideoTexture m_sharedMemVideoTexture;
        private IDNMcHtmlVideoTexture m_htmlVideoTexture;
        private IDNMcTextureArray m_TextureArray;

        private List<IDNMcTexture> m_lstExistingTexturesValues = new List<IDNMcTexture>();
        private List<IDNMcTexture> m_lstArrayTexturesValues = new List<IDNMcTexture>();

        private DNSMcFileSource m_imageTextureSource;

        #endregion

        #region C-tor

        public CreateTextureForm(FontTextureSourceForm TextureSourceForm = FontTextureSourceForm.CreateDialog, bool isCreateFromTextureArray = false) : base() 
        {

          /*  Screen[] screenList = Screen.AllScreens;

            foreach (Screen screen in screenList)
            {
                DEVMODE dm = new DEVMODE();
                dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                EnumDisplaySettings(screen.DeviceName, -1, ref dm);

                var scalingFactor = Math.Round(Decimal.Divide(dm.dmPelsWidth, screen.Bounds.Width), 2);
            }*/

            InitializeComponent();
            m_isNewAction = true;
            m_TextureSourceForm = TextureSourceForm;

            cmbMemBufferTextureUsage.Items.AddRange(Enum.GetNames(typeof(DNETextureUsage)));
            cmbMemBufferTextureUsage.Text = DNETextureUsage._EU_STATIC.ToString();

            cmbVideoUpdateMethod.Items.AddRange(Enum.GetNames(typeof(DNEUpdateMethod)));
            cmbVideoUpdateMethod.Text = DNEUpdateMethod._EUM_RENDER.ToString();
            cmbVideoPixelFormat.Items.AddRange(Enum.GetNames(typeof(DNEPixelFormat)));
            cmbVideoPixelFormat.Text = DNEPixelFormat._EPF_X8R8G8B8.ToString();

            cmbMemBufferColorsTextureUsage.Items.AddRange(Enum.GetNames(typeof(DNETextureUsage)));
            cmbMemBufferColorsTextureUsage.Text = DNETextureUsage._EU_STATIC_WRITE_ONLY.ToString();
            cmbMemBufferPixelFormat.Items.AddRange(Enum.GetNames(typeof(DNEPixelFormat)));
            cmbMemBufferPixelFormat.Text = DNEPixelFormat._EPF_A8R8G8B8.ToString();

            m_cbxResourceType.Items.AddRange(Enum.GetNames(typeof(DNEResourceType)));
            m_cbxResourceType.Text = DNEResourceType._ERT_ICON.ToString();

            lstExistingTextures.Items.Clear();
            m_lstExistingTexturesValues.Clear();
            Dictionary<IDNMcTexture, uint> textures = Managers.ObjectWorld.Manager_MCTextures.dTextures;
            string desc;
            foreach (IDNMcTexture key in textures.Keys)
            {
                desc = GetFullTextureDesc(key);
                m_lstExistingTexturesValues.Add(key);
                lstExistingTextures.Items.Add(desc);

                if (m_currentTexture != null && m_currentTexture == key)
                    continue;
                if (key.TextureType == DNETextureType._ETT_TEXTURE_ARRAY)
                    continue;

                m_lstArrayTexturesValues.Add(key);

            }
            btnDeleteExistingTexture.Enabled = btnSaveAllTexturesToFolder.Enabled = (textures.Count > 0);

            ChangeButtonText();
            btnSaveTextureToFile.Enabled = false;

            m_pbxTransparentColor.BackColor = Color.Empty;
            chxSetAsDefault.Visible = m_TextureSourceForm == FontTextureSourceForm.CreateOnMap;
            chxSetAsDefault.Checked = TextureDialog_SetAsDefault_UserSelected;
            m_isCreateFromTextureArray = isCreateFromTextureArray;
            SetUI();
            m_tbTextureCreation.SelectedTab = m_tbTextureCreation.TabPages[1];
            rbMemoryBufferColorArray_CheckedChanged(null, null);

            m_tbTextureCreation.Selecting += new TabControlCancelEventHandler(m_tbTextureCreation_Selecting);

        }

        public CreateTextureForm(FontTextureSourceForm TextureSourceForm, IDNMcTexture currentTexture, bool isCreateFromTextureArray = false) : this(TextureSourceForm, isCreateFromTextureArray)
        {
            m_currentTexture = currentTexture;

            m_isNewAction = (m_TextureSourceForm == FontTextureSourceForm.CreateOnMap ||
                            m_TextureSourceForm == FontTextureSourceForm.CreateDialog ||
                            m_TextureSourceForm == FontTextureSourceForm.Recreate);

            m_textureName = currentTexture != null ? currentTexture.ToString() : "";
            m_imageTexture = currentTexture as IDNMcImageFileTexture;
            m_iconTexture = currentTexture as IDNMcIconHandleTexture;
            m_resourceTexture = currentTexture as IDNMcResourceTexture;
            m_bitmapTexture = currentTexture as IDNMcBitmapHandleTexture;
            m_directShowGraphTexture = currentTexture as IDNMcDirectShowGraphTexture;
            m_directShowSourceFileTexture = currentTexture as IDNMcDirectShowSourceFileTexture;
            m_directShowUSBCameraTexture = currentTexture as IDNMcDirectShowUSBCameraTexture;
            m_FFMpegVideoTexture = currentTexture as IDNMcFFMpegVideoTexture;
            m_MemoryBufferTexture = currentTexture as IDNMcMemoryBufferTexture;
            m_sharedMemVideoTexture = currentTexture as IDNMcSharedMemoryVideoTexture;


            ChangeButtonText();
            btnSaveTextureToFileMP.Enabled = (m_currentTexture != null);
            try
            {
                if (m_currentTexture != null)
                {
                    m_chkUseExisting.Checked = m_currentTexture.IsCreatedWithUseExisting();
                    m_chkUseExisting.Text = "Is Created With Use Existing";
                    m_chkUseExisting.Enabled = false;

                    DNSMcBColor pTransparentColor;
                    bool pbIsTransparentColorEnabled;
                    m_currentTexture.GetTransparentColor(out pTransparentColor, out pbIsTransparentColorEnabled);

                    if (pbIsTransparentColorEnabled)
                    {
                        m_pbxTransparentColor.BackColor = ToColor(pTransparentColor);
                    }
                    else
                        m_pbxTransparentColor.BackColor = Color.Empty;

                    uint width, height;
                    m_currentTexture.GetSize(out width, out height);
                    ntxWidth.SetUInt32(width);
                    ntxHeight.SetUInt32(height);

                    m_currentTexture.GetSourceSize(out width, out height);
                    ntxSrcWidth.SetUInt32(width);
                    ntxSrcHeight.SetUInt32(height);

                    m_pbxTransparentColor.Enabled = pbIsTransparentColorEnabled;

                    DNSMcBColor pColorToSub = DNSMcBColor.bcBlackTransparent;
                    DNSMcBColor pSubColor = DNSMcBColor.bcBlackTransparent;
                    DNSColorSubstitution[] pSubColorSubstitution = m_currentTexture.GetColorSubstitutions();

                    SetColors(pSubColorSubstitution);

                    chxFillPattern.Checked = m_currentTexture.IsFillPattern();
                    chxIgnoreTransparentMargin.Checked = m_currentTexture.IsTransparentMarginIgnored();

                    if (m_TextureSourceForm == FontTextureSourceForm.Update)
                    {
                        foreach (TabPage currentTab in m_tbTextureCreation.TabPages)
                        {
                            currentTab.Enabled = false;
                        }
                    }
                    TabPage currTab = null;
                    int tabIndex = -1;
                    switch (m_currentTexture.TextureType)
                    {
                        case DNETextureType._ETT_BITMAP_HANDLE_TEXTURE:
                            tabIndex = 0;
                            break;
                        case DNETextureType._ETT_IMAGE_FILE_TEXTURE:
                            tabIndex = 1;
                            m_imageTextureSource = ((IDNMcImageFileTexture)m_currentTexture).GetImageFile();
                            cbIsMemoryBuffer.Checked = m_imageTextureSource.bIsMemoryBuffer;

                            btnSaveTextureToFile.Enabled = m_imageTextureSource.bIsMemoryBuffer;

                            if (m_imageTextureSource.bIsMemoryBuffer == false)
                                m_txtImageFile.Text = m_imageTextureSource.strFileName;

                            break;
                        case DNETextureType._ETT_RESOURCE_TEXTURE:
                            tabIndex = 2;
                            int pnResourceID;
                            DNEResourceType peResourceType;
                            string resourceFileName;
                            ((IDNMcResourceTexture)m_currentTexture).GetResource(out pnResourceID, out peResourceType, out resourceFileName);
                            m_ntxtResourceId.SetInt(pnResourceID);
                            m_cbxResourceType.Text = peResourceType.ToString();
                            m_txtResourceFile.Text = resourceFileName;
                            break;
                        case DNETextureType._ETT_ICON_HANDLE_TEXTURE:
                            tabIndex = 3;
                            break;
                        case DNETextureType._ETT_MEMORY_BUFFER_TEXTURE:
                            tabIndex = 4;
                            txtSrcPixelFormat.Text = m_MemoryBufferTexture.GetSourcePixelFormat().ToString();
                            uint srcWidth, srcHeight;
                            m_MemoryBufferTexture.GetSourceSize(out srcWidth, out srcHeight);
                            txtMemBufferSrcWidth.Text = srcWidth.ToString();
                            txtMemBufferSrcHeight.Text = srcHeight.ToString();

                            txtMemBufferPixelFormat.Text = m_MemoryBufferTexture.GetPixelFormat().ToString();
                            //uint width, height;
                            m_MemoryBufferTexture.GetSize(out width, out height);
                            ntxMemBufferWidth.SetUInt32(width);
                            ntxMemBufferHeight.SetUInt32(height);

                            DNSMcBColor[] paColors;
                            float[] pafColorPositions;
                            bool pbColorInterpolation;
                            bool pbColorColumns;

                            /* ntxMemBufferColorHeight.Enabled = false;
                             ntxMemBufferColorWidth.Enabled = false;
                             chxColorsAutoMipmap.Enabled = false;
                             cmbMemBufferColorsTextureUsage.Enabled = false;
                             cmbMemBufferPixelFormat.Enabled = false;*/
                            cmbMemBufferPixelFormat.Text = m_MemoryBufferTexture.GetPixelFormat().ToString();
                            ntxMemBufferColorHeight.SetUInt32(srcHeight);
                            ntxMemBufferColorWidth.SetUInt32(srcWidth);

                            m_MemoryBufferTexture.GetColorData(out paColors, out pafColorPositions, out pbColorInterpolation, out pbColorColumns);
                            if (paColors != null)
                            {
                                rbMemoryBufferColorArray.Checked = true;

                                ctrlMemBufferColors.SetColors(paColors);
                                dgvColorPositions.Rows.Clear();
                                if (pafColorPositions != null)
                                {
                                    int index = 0;
                                    foreach (float position in pafColorPositions)
                                    {
                                        dgvColorPositions.Rows.Add("", position);
                                        index++;
                                    }
                                    dgvColorPositions.ClearSelection();
                                }
                                rbColorColumns.Checked = pbColorColumns;
                                chxColorInterpolation.Checked = pbColorInterpolation;

                            }

                            break;
                        case DNETextureType._ETT_DIRECT_SHOW_GRAPH_TEXTURE:
                        case DNETextureType._ETT_DIRECT_SHOW_SOURCE_FILE_TEXTURE:
                        case DNETextureType._ETT_DIRECT_SHOW_USB_CAMERA_TEXTURE:
                        case DNETextureType._ETT_FFMPEG_VIDEO_TEXTURE:
                        case DNETextureType._ETT_SHARED_MEMORY_TEXTURE:
                        case DNETextureType._ETT_HTML_VIDEO_TEXTURE:
                            m_btnCreateTexture.Enabled = false;
                            tabIndex = 5;
                            FillTextureVideo((IDNMcVideoTexture)m_currentTexture);
                            break;
                        case DNETextureType._ETT_TEXTURE_ARRAY:

                            tabIndex = 7;
                            m_TextureArray = (IDNMcTextureArray)m_currentTexture;
                            List<IDNMcTexture> mcTextures = new List<IDNMcTexture>();
                            mcTextures.AddRange(m_TextureArray.GetTextures());
                            for (int i = 0; i < mcTextures.Count; i++)
                            {
                                AddTextureToGrid(mcTextures[i]);
                            }
                            break;
                    }
                    if (tabIndex != -1)
                    {
                        currTab = m_tbTextureCreation.TabPages[tabIndex];
                        currTab.Enabled = true;
                        m_tbTextureCreation.SelectedTab = currTab;
                    }

                    txtTextureUniqueName.Text = GetTextureName(m_currentTexture);

                    SetUIByTextureTypeAndFormType();
                }

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTextureData", McEx);
            }
        }

        private void SetUI()
        {
            cmbMemBufferTextureUsage.Enabled = true;
            chxAutoMipmap.Enabled = true;
            rbStoppedState.Checked = true;
            rbTypeFFMpegVideo.Checked = true;
        }

        private void AddTextureToGrid(IDNMcTexture mcTexture)
        {
            string desc = GetFullTextureDesc(mcTexture);

            dgvTextures.Rows.Add(desc);
            dgvTextures.Rows[dgvTextures.Rows.Count - 1].Tag = mcTexture;
            SelectRow(dgvTextures.Rows.Count - 1);
        }

        private void FillTextureVideo(IDNMcVideoTexture currTexture)
        {
            DNETextureType eTextureType = currTexture.TextureType;

            if (eTextureType == DNETextureType._ETT_DIRECT_SHOW_GRAPH_TEXTURE)
                rbTypeDirectShowGraph.Checked = true;
            else if (eTextureType == DNETextureType._ETT_DIRECT_SHOW_USB_CAMERA_TEXTURE)
                rbTypeDirectShowUSBCamera.Checked = true;
            else if (eTextureType == DNETextureType._ETT_FFMPEG_VIDEO_TEXTURE)
                rbTypeFFMpegVideo.Checked = true;
            else if (eTextureType == DNETextureType._ETT_SHARED_MEMORY_TEXTURE)
                rbTypeSharedMemoryVideo.Checked = true;
            else if (eTextureType == DNETextureType._ETT_HTML_VIDEO_TEXTURE)
            {
                rbTypeHtmlVideo.Checked = true;
                try
                {
                    IDNMcHtmlVideoTexture mcHtmlVideoTexture = (IDNMcHtmlVideoTexture)currTexture;
                    tbHtmlVideoSourceName.Text = mcHtmlVideoTexture.GetVideoSource();
                    tbHtmlVideoSourceName.Enabled = false;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetSourceName", McEx);
                }
            }
            try
            {
                DNETextureState state = currTexture.GetState();
                if (state == DNETextureState._ES_RUNNING)
                    rbRunningState.Checked = true;
                else if (state == DNETextureState._ES_STOPPED)
                    rbStoppedState.Checked = true;
                if (state == DNETextureState._ES_PAUSED)
                    rbPausedState.Checked = true;

                tbFileDesc.Text = GetFullTextureDesc(currTexture);

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetState", McEx);
            }

            try
            {
                tbVideoFrameRateForRenderBasedUpdate.SetFloat((m_currentTexture as IDNMcVideoTexture).GetFrameRateForRenderBasedUpdate());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetFrameRateForRenderBasedUpdate", McEx);
            }

            pnlVideoRadioGroup.Enabled = false;
        }

        private string GetFullTextureDesc(IDNMcTexture currTexture)
        {
            return Manager_MCNames.GetNameByObject(currTexture);
        }


        private string GetTextureName(IDNMcTexture currTexture)
        {
            string retValue = "";
            try
            {
                retValue = currTexture.GetName();
            }
            catch (MapCoreException McEx)
            {
                if (McEx.ErrorCode != DNEMcErrorCode.NOT_INITIALIZED)
                    Utilities.ShowErrorMessage("GetName", McEx);
            }
            return retValue;
        }

        #endregion

        #region public static method 

        public static Color ToColor(DNSMcBColor mcColor)
        {
            return Color.FromArgb(255, mcColor.r, mcColor.g, mcColor.b);
        }

        public static Color ToColor(DNSMcFColor mcColor)
        {
            return Color.FromArgb(255, (int)mcColor.r, (int)mcColor.g, (int)mcColor.b);
        }
        #endregion

        #region Public Properties
        public IDNMcTexture CurrentTexture
        {
            get
            {
                return m_currentTexture;
            }
            set
            {
                m_currentTexture = value;
            }
        }
        #endregion

        #region Private Methods

        private void SetAsDefault()
        {
            if (chxSetAsDefault.Visible && chxSetAsDefault.Checked)
                ObjectPropertiesBase.PictureTexture = m_currentTexture;
        }

        private void m_btnCreateTexture_Click(object sender, EventArgs e)
        {
            if(chxSetAsDefault.Visible)
                MainForm.TextureDialog_SetAsDefault_UserSelected = chxSetAsDefault.Checked;

            DNETextureType textureType = DNETextureType._ETT_NONE;
            // Init according to the texture type
            string textFileName = null;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            switch (m_tbTextureCreation.SelectedTab.Name)
            {
                case "m_hBitmapTab":
                    textureType = DNETextureType._ETT_BITMAP_HANDLE_TEXTURE;
                    textFileName = (m_txtBitmapFile.Text == "") ? null : m_txtBitmapFile.Text;
                    break;
                case "m_imageFileTab":
                    textureType = DNETextureType._ETT_IMAGE_FILE_TEXTURE;
                    textFileName = (m_txtImageFile.Text == "") ? null : m_txtImageFile.Text;
                    break;
                case "m_resourceTab":
                    textureType = DNETextureType._ETT_RESOURCE_TEXTURE;
                    textFileName = (m_txtResourceFile.Text == "") ? null : m_txtResourceFile.Text;
                    break;
                case "m_hIconTab":
                    textureType = DNETextureType._ETT_ICON_HANDLE_TEXTURE;
                    textFileName = (m_txtIconFile.Text == "") ? null : m_txtIconFile.Text;
                    break;
                case "m_VideoFiles":
                    textureType = DNETextureType._ETT_DIRECT_SHOW_GRAPH_TEXTURE;
                    break;
                case "m_MemoryBuffer":
                    textureType = DNETextureType._ETT_MEMORY_BUFFER_TEXTURE;
                    textFileName = (m_txtMemoryBufferFile.Text == "") ? null : m_txtMemoryBufferFile.Text;
                    break;
                case "m_FromListTab":
                    if (lstExistingTextures.SelectedItem == null)
                    {
                        MessageBox.Show("You must choose texture file first", "MCTester", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;
                case "m_TextureArrayTab":
                    textureType = DNETextureType._ETT_TEXTURE_ARRAY;
                    break;
            }
            
            DNSMcBColor? pTransparentColor;

            //get the texture colors
            DNSColorSubstitution[] colorSubstitutions = null;
            bool pColorSubstitutionResult;
            GetColors(out pTransparentColor, out colorSubstitutions, out pColorSubstitutionResult);
            if (!pColorSubstitutionResult)
            {
                DialogResult = DialogResult.None;
                return;
            }

            if (m_isNewAction)
            {
                // case we take an existing texture from list
                if (m_tbTextureCreation.SelectedTab.Name == "m_FromListTab")
                {
                    CurrentTexture = m_lstExistingTexturesValues[lstExistingTextures.SelectedIndex];
                    SetAsDefault();
                    this.Close();
                    return;
                }

                // Create Texture
                bool bUseExisting = m_chkUseExisting.Checked;
                bool? pbExistingUsed = false;

                switch (textureType)
                {
                    case DNETextureType._ETT_IMAGE_FILE_TEXTURE:
                        try
                        {
                            if (cbIsMemoryBuffer.Checked == false)
                            {
                                m_imageTexture = DNMcImageFileTexture.Create(new DNSMcFileSource(textFileName),
                                                                                chxFillPattern.Checked,
                                                                                chxIgnoreTransparentMargin.Checked,
                                                                                pTransparentColor,
                                                                                colorSubstitutions,
                                                                                bUseExisting,
                                                                                ref pbExistingUsed);
                            }
                            else
                            {
                                string fileExt = null;
                                byte[] fileByteArray = m_imageTextureSource.aFileMemoryBuffer;
                                if (textFileName != null)
                                {
                                    if(chxUseFileExtension.Checked)
                                    try
                                    {
                                        fileExt = Path.GetExtension(textFileName).Substring(1);
                                    }
                                    catch (ArgumentException)
                                    {
                                        MessageBox.Show("Invalid Path Extension");
                                        return ;
                                    }
                                    try
                                    {
                                        fileByteArray = File.ReadAllBytes(textFileName);
                                    }
                                    catch (UnauthorizedAccessException)
                                    {
                                        MessageBox.Show("File path incorrect, please fixed it.", "Invalid Texture File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        m_txtImageFile.Focus();

                                    }
                                    catch (IOException)
                                    {
                                        MessageBox.Show("File path incorrect, please fixed it.", "Invalid Texture File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        m_txtImageFile.Focus();
                                    }
                                }
                                m_imageTexture = DNMcImageFileTexture.Create(new DNSMcFileSource(fileByteArray, fileExt),
                                                                                    chxFillPattern.Checked,
                                                                                    chxIgnoreTransparentMargin.Checked,
                                                                                    pTransparentColor,
                                                                                    colorSubstitutions,
                                                                                    bUseExisting,
                                                                                    ref pbExistingUsed);
                            }
                            m_currentTexture = m_imageTexture;

                            if (pbExistingUsed == true)
                                MessageBox.Show("This texture is existing used based", "Existing Used Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                            {
                                Managers.ObjectWorld.Manager_MCTextures.AddToDictionary(m_imageTexture);
                            }
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcImageFileTexture.Create", McEx);
                            DialogResult = DialogResult.None;
                            return;
                        }
                        break;
                    case DNETextureType._ETT_RESOURCE_TEXTURE:
                        try
                        {
                            int nResourceID = m_ntxtResourceId.GetInt32();
                            DNEResourceType eResourceType = (DNEResourceType)Enum.Parse(typeof(DNEResourceType), m_cbxResourceType.Text);

                            m_resourceTexture = DNMcResourceTexture.Create(nResourceID,
                                                                            eResourceType,
                                                                            textFileName,
                                                                            chxFillPattern.Checked,
                                                                            chxIgnoreTransparentMargin.Checked,
                                                                            pTransparentColor,
                                                                            colorSubstitutions,
                                                                            bUseExisting,
                                                                            ref pbExistingUsed);

                            m_currentTexture = m_resourceTexture;

                            if (pbExistingUsed == true)
                                MessageBox.Show("This texture is existing used based", "Existing Used Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                            {
                                Managers.ObjectWorld.Manager_MCTextures.AddToDictionary(m_resourceTexture);
                            }
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcResourceTexture.Create", McEx);
                            DialogResult = DialogResult.None;
                        }

                        break;
                    case DNETextureType._ETT_BITMAP_HANDLE_TEXTURE:
                        try
                        {
                            // Note that even thou the Bitmap is created with the correct pixel format,
                            // the HBitmap (received by the GetHbitmap() call) is always 32 bpp !!!
                            // - Bitmap format can be received by calling:
                            //   ((System.Drawing.Image)(currentBitmap)).PixelFormat
                            // - HBitmap format can be received by calling:
                            //   BITMAP bm; HBITMAP hbm;
                            //   ::GetObject(hbm, sizeof(BITMAP), (LPSTR)&bm);

                            IntPtr bitmapHandle = IntPtr.Zero;
                            if (textFileName != null)
                            {
                                int pTexture;
                                bool isPointer = int.TryParse(textFileName, out pTexture);
                                if (isPointer == true)
                                {
                                    bitmapHandle = new IntPtr(pTexture);
                                }
                                else
                                {
                                    try
                                    {
                                        Bitmap currentBitmap = new Bitmap(textFileName);
                                        bitmapHandle = currentBitmap.GetHbitmap();
                                    }
                                    catch (System.Exception ex)
                                    {
                                        MessageBox.Show(ex.Message + "\nTexture type is not supported by .Net", "Illegal Texture", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        DialogResult = DialogResult.None;
                                        return;
                                    }
                                }
                            }

                            m_bitmapTexture = DNMcBitmapHandleTexture.Create(bitmapHandle,
                                                                                chxFillPattern.Checked,
                                                                                chxIgnoreTransparentMargin.Checked,
                                                                                pTransparentColor,
                                                                                colorSubstitutions,
                                                                                m_chkTakeOwnershipBitmap.Checked,
                                                                                bUseExisting,
                                                                                ref pbExistingUsed);

                            m_currentTexture = m_bitmapTexture;

                            // delete hBitmap in case that MapCore take ownership
                            if (m_chkTakeOwnershipBitmap.Checked == false)
                                DeleteObject(bitmapHandle);

                            if (pbExistingUsed == true)
                                MessageBox.Show("This texture is existing used based", "Existing Used Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                            {
                                Managers.ObjectWorld.Manager_MCTextures.AddToDictionary(m_bitmapTexture);
                            }
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcBitmapHandleTexture.Create", McEx);
                            DialogResult = DialogResult.None;
                        }

                        break;
                    case DNETextureType._ETT_ICON_HANDLE_TEXTURE:
                        try
                        {
                            IntPtr iconHandle = IntPtr.Zero;
                            if (textFileName != null)
                            {
                                int pTexture;
                                bool isPointer = int.TryParse(textFileName, out pTexture);
                                if (isPointer == true)
                                {
                                    iconHandle = new IntPtr(pTexture);
                                }
                                else
                                {
                                    try
                                    {
                                        Icon currentIcon = new Icon(textFileName);
                                        iconHandle = currentIcon.ToBitmap().GetHicon();
                                    }
                                    catch (UnauthorizedAccessException)
                                    {
                                        MessageBox.Show("File path incorrect, please fixed it.", "Invalid Texture File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        m_txtIconFile.Focus();
                                    }
                                    catch (IOException)
                                    {
                                        MessageBox.Show("File path incorrect, please fixed it.", "Invalid Texture File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        m_txtIconFile.Focus();
                                    }
                                    catch (Exception)
                                    {
                                        MessageBox.Show("Unsupported file, please fixed it.", "Invalid Texture File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        m_txtIconFile.Focus();
                                    }
                                }
                            }
                            if (iconHandle != IntPtr.Zero)
                            {
                                m_iconTexture = DNMcIconHandleTexture.Create(iconHandle,
                                                                                    chxFillPattern.Checked,
                                                                                    chxIgnoreTransparentMargin.Checked,
                                                                                    pTransparentColor,
                                                                                    colorSubstitutions,
                                                                                    m_chkTakeOwnershipIcon.Checked,
                                                                                    bUseExisting,
                                                                                    ref pbExistingUsed);

                                m_currentTexture = m_iconTexture;

                                // delete hIcon in case that MapCore take ownership
                                if (m_chkTakeOwnershipIcon.Checked == false)
                                    DestroyIcon(iconHandle);

                                if (pbExistingUsed == true)
                                    MessageBox.Show("This texture is existing used based", "Existing Used Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                {
                                    Managers.ObjectWorld.Manager_MCTextures.AddToDictionary(m_iconTexture);
                                }
                            }
                            else
                            {
                                DialogResult = DialogResult.None;
                                return;
                            }
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcIconHandleTexture.Create", McEx);
                            DialogResult = DialogResult.None;
                        }

                        break;
                    case DNETextureType._ETT_DIRECT_SHOW_GRAPH_TEXTURE:
                        {
                            DNEPixelFormat pixelFormat = (DNEPixelFormat)Enum.Parse(typeof(DNEPixelFormat), cmbVideoPixelFormat.Text);
                            if (rbTypeDirectShowGraph.Checked)
                            {
                                textFileName = (ctrlBrowseTextureVideo.FileName == "") ? null : ctrlBrowseTextureVideo.FileName;
                                textureType = DNETextureType._ETT_DIRECT_SHOW_GRAPH_TEXTURE;

                                try
                                {
                                    DNEUpdateMethod updateMethod = (DNEUpdateMethod)Enum.Parse(typeof(DNEUpdateMethod), cmbVideoUpdateMethod.Text);

                                    m_directShowGraphTexture = DNMcDirectShowGraphTexture.Create(textFileName,
                                                                                                    updateMethod,
                                                                                                    chxVideoReadable.Checked,
                                                                                                    pixelFormat);
                                    m_currentTexture = m_directShowGraphTexture;

                                    Managers.ObjectWorld.Manager_MCTextures.AddToDictionary(m_directShowGraphTexture);

                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("DNMcDirectShowGraphTexture.Create", McEx);
                                    DialogResult = DialogResult.None;
                                }
                            }
                            else if (rbTypeDirectShowUSBCamera.Checked)
                            {
                                textureType = DNETextureType._ETT_DIRECT_SHOW_USB_CAMERA_TEXTURE;
                                try
                                {
                                    DNEUpdateMethod updateMethod = (DNEUpdateMethod)Enum.Parse(typeof(DNEUpdateMethod), cmbVideoUpdateMethod.Text);

                                    m_directShowUSBCameraTexture = DNMcDirectShowUSBCameraTexture.Create(ntxDirectShowUSBCameraDeviceIndex.GetUInt32(),
                                                                                                            updateMethod,
                                                                                                            chxVideoReadable.Checked,
                                                                                                            pixelFormat);
                                    m_currentTexture = m_directShowUSBCameraTexture;

                                    Managers.ObjectWorld.Manager_MCTextures.AddToDictionary(m_directShowUSBCameraTexture);

                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("DNMcDirectShowUSBCameraTexture.Create", McEx);
                                    DialogResult = DialogResult.None;
                                }
                            }
                            else if (rbTypeFFMpegVideo.Checked)
                            {
                                textureType = DNETextureType._ETT_FFMPEG_VIDEO_TEXTURE;
                                try
                                {
                                    textFileName = (ctrlBrowseTextureVideo.FileName == "") ? null : ctrlBrowseTextureVideo.FileName;

                                    m_FFMpegVideoTexture = DNMcFFMpegVideoTexture.Create(textFileName,
                                                                                    chxVideoPlayInLoop.Checked,
                                                                                    chxVideoReadable.Checked,
                                                                                    tbVideoSourceFormat.Text);
                                    m_currentTexture = m_FFMpegVideoTexture;

                                    Managers.ObjectWorld.Manager_MCTextures.AddToDictionary(m_FFMpegVideoTexture);

                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("DNMcFFMpegVideoTexture.Create", McEx);
                                    DialogResult = DialogResult.None;
                                }
                            }
                            else if (rbTypeSharedMemoryVideo.Checked)
                            {
                                textureType = DNETextureType._ETT_SHARED_MEMORY_TEXTURE;
                                pixelFormat = (DNEPixelFormat)Enum.Parse(typeof(DNEPixelFormat), cmbVideoPixelFormat.Text);
                                try
                                {
                                    m_sharedMemVideoTexture = DNMcSharedMemoryVideoTexture.Create(tbSharedMemoryName.Text, pixelFormat);
                                    m_currentTexture = m_sharedMemVideoTexture;
                                    Managers.ObjectWorld.Manager_MCTextures.AddToDictionary(m_sharedMemVideoTexture);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("DNMcSharedMemVideoTexture.Create", McEx);
                                    DialogResult = DialogResult.None;
                                }
                            }
                            else if (rbTypeHtmlVideo.Checked)
                            {
                                textureType = DNETextureType._ETT_HTML_VIDEO_TEXTURE;
                                pixelFormat = (DNEPixelFormat)Enum.Parse(typeof(DNEPixelFormat), cmbVideoPixelFormat.Text);
                                try
                                {
                                    m_htmlVideoTexture = DNMcHtmlVideoTexture.Create(tbHtmlVideoSourceName.Text, chxVideoPlayInLoop.Checked, chxVideoReadable.Checked);
                                    m_currentTexture = m_htmlVideoTexture;
                                    Managers.ObjectWorld.Manager_MCTextures.AddToDictionary(m_htmlVideoTexture);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("DNMcHtmlVideoTexture.Create", McEx);
                                    DialogResult = DialogResult.None;
                                }
                            }

                            if (rbRunningState.Checked)
                                SetVideoState(m_currentTexture as IDNMcVideoTexture, DNETextureState._ES_RUNNING);
                            else if (rbStoppedState.Checked)
                                SetVideoState(m_currentTexture as IDNMcVideoTexture, DNETextureState._ES_STOPPED);
                            else if (rbPausedState.Checked)
                                SetVideoState(m_currentTexture as IDNMcVideoTexture, DNETextureState._ES_PAUSED);

                            if (m_currentTexture != null)
                            {
                                try
                                {
                                    (m_currentTexture as IDNMcVideoTexture).SetFrameRateForRenderBasedUpdate(tbVideoFrameRateForRenderBasedUpdate.GetFloat());
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetFrameRateForRenderBasedUpdate", McEx);
                                    DialogResult = DialogResult.None;
                                }
                            }
                            break;
                        }
                    case DNETextureType._ETT_MEMORY_BUFFER_TEXTURE:
                        if (rbMemoryBufferBMFFile.Checked)
                            try
                            {
                                IDNMcImage mcImage = DNMcImage.Create(new DNSMcFileSource(m_txtMemoryBufferFile.Text));

                                IntPtr intPtr = mcImage.GetPixelBuffer();
                                int userRowPitch = ntxMemBufferRowPitch.GetInt32();
                                if (userRowPitch > 0 && userRowPitch < ntxMemBufferWidth.GetInt32())
                                {
                                    MessageBox.Show("Invalid row pitch, should be greater then width", "Memory buffer texture", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    DialogResult = DialogResult.None;
                                    return;
                                }
                                else
                                {
                                    try
                                    {
                                        DNEPixelFormat bufferPixelFormat = (DNEPixelFormat)Enum.Parse(typeof(DNEPixelFormat), txtMemBufferPixelFormat.Text);
                                        DNETextureUsage textureUsage = (DNETextureUsage)Enum.Parse(typeof(DNETextureUsage), cmbMemBufferTextureUsage.Text);

                                        int height = ntxMemBufferHeight.GetInt32();
                                        int width = ntxMemBufferWidth.GetInt32();

                                        if (userRowPitch > width)
                                        {
                                            byte[] newBuffer = ConvertIntPtrToByteArray(intPtr, height, width, bufferPixelFormat, userRowPitch);

                                            m_MemoryBufferTexture = DNMcMemoryBufferTexture.Create(ntxMemBufferWidth.GetUInt32(),
                                                                                               ntxMemBufferHeight.GetUInt32(),
                                                                                               bufferPixelFormat,
                                                                                               textureUsage,
                                                                                               chxAutoMipmap.Checked,
                                                                                               newBuffer,
                                                                                               (uint)userRowPitch,
                                                                                               txtTextureUniqueName.Text);

                                        }
                                        else
                                        {

                                            m_MemoryBufferTexture = DNMcMemoryBufferTexture.Create(ntxMemBufferWidth.GetUInt32(),
                                                                                                ntxMemBufferHeight.GetUInt32(),
                                                                                                bufferPixelFormat,
                                                                                                textureUsage,
                                                                                                chxAutoMipmap.Checked,
                                                                                                intPtr,
                                                                                                (uint)userRowPitch,
                                                                                                txtTextureUniqueName.Text);

                                        }
                                        m_currentTexture = m_MemoryBufferTexture;

                                        Managers.ObjectWorld.Manager_MCTextures.AddToDictionary(m_MemoryBufferTexture);

                                        if (chkDirectXTexture.Checked == true)
                                        {
                                            try
                                            {
                                                IntPtr ptr = m_MemoryBufferTexture.GetDirectXTexture();
                                                MessageBox.Show("DirectX Texture created successfully\nIntPtr: " + ptr.ToString(), "DirectX Texture", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            catch (MapCoreException McEx)
                                            {
                                                Utilities.ShowErrorMessage("GetDirectXTexture", McEx);
                                                DialogResult = DialogResult.None;
                                            }
                                        }
                                    }
                                    catch (MapCoreException McEx)
                                    {
                                        Utilities.ShowErrorMessage("DNMcMemoryBufferTexture.Create", McEx);
                                        DialogResult = DialogResult.None;
                                    }
                                }

                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("DNMcMemoryBufferTexture.Create", McEx);
                                DialogResult = DialogResult.None;
                            }
                        else
                        {
                            try
                            {
                                if (ntxMemBufferColorWidth.Text == "" || ntxMemBufferColorHeight.Text == "")
                                {
                                    MessageBox.Show("Width or Height cannot be empty, fix it and try again.", "Memory Buffer Texture");
                                    DialogResult = DialogResult.None;
                                    return;
                                }

                                DNEPixelFormat bufferPixelFormat = (DNEPixelFormat)Enum.Parse(typeof(DNEPixelFormat), cmbMemBufferPixelFormat.Text);
                                DNETextureUsage textureUsage = (DNETextureUsage)Enum.Parse(typeof(DNETextureUsage), cmbMemBufferColorsTextureUsage.Text);

                              
                                m_MemoryBufferTexture = DNMcMemoryBufferTexture.Create(ntxMemBufferColorWidth.GetUInt32(),
                                                                                                  ntxMemBufferColorHeight.GetUInt32(),
                                                                                                  ctrlMemBufferColors.GetColors(),
                                                                                                  GetColorPositions(),
                                                                                                  chxColorInterpolation.Checked,
                                                                                                  rbColorColumns.Checked,
                                                                                                  bufferPixelFormat,
                                                                                                  textureUsage,
                                                                                                  chxColorsAutoMipmap.Checked);
                                m_currentTexture = m_MemoryBufferTexture;

                                Managers.ObjectWorld.Manager_MCTextures.AddToDictionary(m_MemoryBufferTexture);

                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("DNMcMemoryBufferTexture.Create", McEx);
                                DialogResult = DialogResult.None;
                            }
                        }
                        break;
                    case DNETextureType._ETT_TEXTURE_ARRAY:
                        try
                        {
                            List<IDNMcTexture> mcTextures = new List<IDNMcTexture>();
                            for (int i = 0; i < dgvTextures.Rows.Count; i++)
                            {
                                mcTextures.Add((IDNMcTexture)dgvTextures.Rows[i].Tag);
                            }

                            m_TextureArray = DNMcTextureArray.Create(mcTextures.ToArray());
                            m_currentTexture = m_TextureArray;

                            Managers.ObjectWorld.Manager_MCTextures.AddToDictionary(m_TextureArray);

                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcTextureArray.Create", McEx);
                            DialogResult = DialogResult.None;
                        }
                        break;
                }
                
            }
            else
            {
                // Update Texture
                switch (m_currentTexture.TextureType)
                {
                    case DNETextureType._ETT_IMAGE_FILE_TEXTURE:
                        try
                        {
                            DNSMcFileSource mcNewFileSource = m_imageTextureSource;  // if want to update only colors.

                            if (cbIsMemoryBuffer.Checked == false)
                            {
                                m_imageTexture.SetImageFile(new DNSMcFileSource(m_txtImageFile.Text), pTransparentColor, colorSubstitutions);
                            }
                            else   // cbIsMemoryBuffer.Checked = true
                            {
                                byte[] fileByteArray = null;
                                if (m_txtImageFile.Text != "")
                                {
                                    try
                                    {
                                        fileByteArray = File.ReadAllBytes(textFileName);
                                    }
                                    catch (UnauthorizedAccessException)
                                    {
                                        MessageBox.Show("File path incorrect, please fixed it.", "Invalid Texture File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        m_txtImageFile.Focus();

                                    }
                                    catch (IOException)
                                    {
                                        MessageBox.Show("File path incorrect, please fixed it.", "Invalid Texture File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        m_txtImageFile.Focus();
                                    }
                                    string fileExt = null;
                                    if (chxUseFileExtension.Checked)
                                        try
                                        {
                                            fileExt = Path.GetExtension(textFileName).Substring(1);
                                        }
                                        catch (ArgumentException)
                                        {
                                            MessageBox.Show("Invalid Path Extension");
                                            return;
                                        }
                                    mcNewFileSource = new DNSMcFileSource(fileByteArray, fileExt);
                                }

                                m_imageTexture.SetImageFile(mcNewFileSource, pTransparentColor, colorSubstitutions);
                            }
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("SetImageFile", McEx);
                            DialogResult = DialogResult.None;
                            return;
                        }
                        break;
                    case DNETextureType._ETT_RESOURCE_TEXTURE:
                        try
                        {
                            int nResourceID = m_ntxtResourceId.GetInt32();
                            DNEResourceType eResourceType = (DNEResourceType)Enum.Parse(typeof(DNEResourceType), m_cbxResourceType.Text);
                            m_resourceTexture.SetResource(nResourceID, eResourceType, m_txtResourceFile.Text,
                                pTransparentColor, colorSubstitutions);

                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("SetResource", McEx);
                            DialogResult = DialogResult.None;
                        }

                        break;
                    case DNETextureType._ETT_BITMAP_HANDLE_TEXTURE:
                        try
                        {
                            IntPtr bitmapHandle = IntPtr.Zero;
                            if (m_txtBitmapFile.Text != "")
                            {
                                int pTexture;
                                bool isPointer = int.TryParse(m_txtBitmapFile.Text, out pTexture);
                                if (isPointer == true)
                                {
                                    bitmapHandle = new IntPtr(pTexture);
                                }
                                else
                                {
                                    try
                                    {
                                        Bitmap currentBitmap = new Bitmap(m_txtBitmapFile.Text);
                                        bitmapHandle = currentBitmap.GetHbitmap();
                                    }
                                    catch (System.Exception ex)
                                    {
                                        MessageBox.Show(ex.Message + "\nTexture type is not supported by .Net", "Illegal Texture", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        DialogResult = DialogResult.None;
                                        return;
                                    }
                                }
                            }

                            m_bitmapTexture.SetBitmap(bitmapHandle,
                                                        pTransparentColor,
                                                        colorSubstitutions,
                                                        m_chkTakeOwnershipBitmap.Checked);

                            // delete hBitmap in case that MapCore take ownership
                            if (m_chkTakeOwnershipBitmap.Checked == false)
                                DeleteObject(bitmapHandle);
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("SetBitmap", McEx);
                            DialogResult = DialogResult.None;
                        }

                        break;
                    case DNETextureType._ETT_ICON_HANDLE_TEXTURE:
                        try
                        {
                            IntPtr iconHandle = IntPtr.Zero;
                            if (m_txtIconFile.Text != "")
                            {
                                int pTexture;
                                bool isPointer = int.TryParse(m_txtIconFile.Text, out pTexture);
                                if (isPointer == true)
                                {
                                    iconHandle = new IntPtr(pTexture);
                                }
                                else
                                {
                                    Icon currentIcon = new Icon(m_txtIconFile.Text);
                                    iconHandle = currentIcon.ToBitmap().GetHicon();
                                }
                            }

                            m_iconTexture.SetIcon(iconHandle,
                                                    pTransparentColor,
                                                    colorSubstitutions,
                                                    m_chkTakeOwnershipIcon.Checked);

                            // delete hIcon in case that MapCore take ownership
                            if (m_chkTakeOwnershipIcon.Checked == false)
                                DestroyIcon(iconHandle);

                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("SetIcon", McEx);
                            DialogResult = DialogResult.None;
                        }
                        break;
                    case DNETextureType._ETT_DIRECT_SHOW_GRAPH_TEXTURE:
                    case DNETextureType._ETT_DIRECT_SHOW_SOURCE_FILE_TEXTURE:
                    case DNETextureType._ETT_DIRECT_SHOW_USB_CAMERA_TEXTURE:
                        MessageBox.Show("Update is not supported in this texture type", "Not Supported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case DNETextureType._ETT_MEMORY_BUFFER_TEXTURE:

                        try
                        {
                            if (rbMemoryBufferBMFFile.Checked == true)
                            {
                                IDNMcImage mcImage = DNMcImage.Create(new DNSMcFileSource(m_txtMemoryBufferFile.Text));
                                IntPtr intPtr = mcImage.GetPixelBuffer();

                                int userRowPitch = ntxMemBufferRowPitch.GetInt32();
                                if (userRowPitch > 0 && userRowPitch < ntxMemBufferWidth.GetInt32())
                                {
                                    MessageBox.Show("Invalid row pitch, should be greater then width", "Memory buffer texture", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    DialogResult = DialogResult.None;
                                    return;
                                }
                                else
                                {
                                    DNEPixelFormat bufferPixelFormat = (DNEPixelFormat)Enum.Parse(typeof(DNEPixelFormat), txtMemBufferPixelFormat.Text);
                                    uint PixelFormatByteCount = DNMcTexture.GetPixelFormatByteCount(bufferPixelFormat);

                                    int height = ntxMemBufferHeight.GetInt32();
                                    int width = ntxMemBufferWidth.GetInt32();

                                    if (userRowPitch > width)
                                    {
                                        byte[] newBuffer = ConvertIntPtrToByteArray(intPtr, height, width, bufferPixelFormat, userRowPitch);

                                        m_MemoryBufferTexture.UpdateFromMemoryBuffer(ntxMemBufferWidth.GetUInt32(),
                                                                                        ntxMemBufferHeight.GetUInt32(),
                                                                                        bufferPixelFormat,
                                                                                        (uint)userRowPitch,
                                                                                        newBuffer);
                                    }
                                    else
                                    {
                                        m_MemoryBufferTexture.UpdateFromMemoryBuffer(ntxMemBufferWidth.GetUInt32(),
                                                                                        ntxMemBufferHeight.GetUInt32(),
                                                                                        bufferPixelFormat,
                                                                                        (uint)userRowPitch,
                                                                                        intPtr);
                                    }
                                }
                            }
                            else
                            {
                                m_MemoryBufferTexture.UpdateFromColorData(ctrlMemBufferColors.GetColors(),
                                                                            GetColorPositions(),
                                                                            chxColorInterpolation.Checked,
                                                                            rbColorColumns.Checked);
                            }
                            m_currentTexture = m_MemoryBufferTexture;

                            Managers.ObjectWorld.Manager_MCTextures.AddToDictionary(m_MemoryBufferTexture);

                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcMemoryBufferTexture.Create", McEx);
                            DialogResult = DialogResult.None;
                        }

                        break;
                }
            }
           
            SetAsDefault();

            if(DialogResult == DialogResult.OK)
                this.Close();
        }

        private float[] GetColorPositions()
        {
            float[] afColorPositions = null;
            if (dgvColorPositions.RowCount - 1 > 0)
            {
                afColorPositions = new float[dgvColorPositions.RowCount - 1];
                for (int i = 0; i < dgvColorPositions.RowCount; i++)
                {
                    if (dgvColorPositions.Rows[i].IsNewRow == false)
                    {
                        if (dgvColorPositions[1, i].Value != null && dgvColorPositions[1, i].Value.ToString() != "")
                            afColorPositions[i] = Convert.ToSingle(dgvColorPositions[1, i].Value);
                        else
                        {
                            MessageBox.Show("Color position cannot be empty in row " + i + ", fix it and try again.", "Memory Buffer Texture");
                            DialogResult = DialogResult.None;
                            break;
                        }
                    }
                }
            }
            return afColorPositions;
        }

        private byte[] ConvertIntPtrToByteArray(IntPtr intPtr, int height, int width, DNEPixelFormat bufferPixelFormat, int userRowPitch)
        {

            uint PixelFormatSize = DNMcTexture.GetPixelFormatByteCount(bufferPixelFormat);
            int size = width * height * (int)PixelFormatSize;
            byte[] buffer = new byte[size];
            Marshal.Copy(intPtr, buffer, 0, size);
            long sourceIndex = 0, destIndex = 0; ;
            byte[] newBuffer = new byte[userRowPitch * height * PixelFormatSize];
            for (int i = 0; i < height; i++)
            {
                Array.Copy(buffer, sourceIndex, newBuffer, destIndex, width * PixelFormatSize);
                sourceIndex += width * PixelFormatSize;
                destIndex += userRowPitch * PixelFormatSize;
            }
            return newBuffer;
        }

        private void m_btnFile_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string buttonName = clickedButton.Name;
            TextBox targetTextBox = new TextBox();
            ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            if (buttonName.Contains("Image") == true)
            {
                ofd.Filter = "Image files|*.bmp;*.jpg;*.jpeg;*.tif;*.tiff;*.gif;*.png;*.ping;*.raw;*.mnf;*.svg| All Files (*.*)|*.*"; //JPEG files (*.jpg)||| TIFF files (*.tif)| | PNG files (*.png)| | Others files | ";
                targetTextBox = m_txtImageFile;
            }
            if (buttonName.Contains("Bitmap") == true)
            {
                ofd.Filter = "Bitmap files |*.bmp;*.jpg;*.jpeg;*.tif;*.tiff;*.gif;*.png;*.ping;*.raw;*.mnf| All Files (*.*)|*.*";
                targetTextBox = m_txtBitmapFile;
            }
            if (buttonName.Contains("Icon") == true)
            {
                ofd.Filter = "Icon files (*.ico)|*.ico| All Files (*.*)|*.*";
                targetTextBox = m_txtIconFile;
            }
            if (buttonName.Contains("Memory") == true)
            {
                ofd.Filter = "Bitmap files (*.bmp)|*.bmp| All Files (*.*)|*.*";
                targetTextBox = m_txtMemoryBufferFile;
            }

            //Display the openFile dialog.
            DialogResult result = ofd.ShowDialog();
            //OK button was pressed.
            if (result == DialogResult.OK)
            {
                targetTextBox.Text = ofd.FileName;

                if (buttonName.Contains("Memory") == true)
                {
                    try
                    {
                        IDNMcImage mcImage = DNMcImage.Create(new DNSMcFileSource(m_txtMemoryBufferFile.Text));

                        string convertedResult = mcImage.GetPixelFormat().ToString();

                        if (m_btnCreateTexture.Text == "Update")
                        {
                            uint SrcWidth, srcHeight;
                            DNEPixelFormat srcFormat = m_MemoryBufferTexture.GetSourcePixelFormat();
                            m_MemoryBufferTexture.GetSourceSize(out SrcWidth, out srcHeight);
                            txtSrcPixelFormat.Text = srcFormat.ToString();
                            txtMemBufferSrcWidth.Text = SrcWidth.ToString();
                            txtMemBufferSrcHeight.Text = srcHeight.ToString();
                        }

                        txtMemBufferPixelFormat.Text = convertedResult;
                        uint width, height;
                        mcImage.GetSize(out width, out height);
                        ntxMemBufferWidth.SetInt((int)width);
                        ntxMemBufferHeight.SetInt((int)height);

                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("IDNMcImage", McEx);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error create image object: " + ex.Message + Environment.NewLine + "file path: " + m_txtMemoryBufferFile.Text,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        private void m_btnResourceFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog resourceFileDialog = new OpenFileDialog();

            // resourceFileDialog.DefaultExt = "dll";
            resourceFileDialog.Filter = "Resource files|*.dll;*.res;*.exe;| All Files|*.*";

            //Display the openFile dialog.
            resourceFileDialog.RestoreDirectory = true;
            DialogResult result = resourceFileDialog.ShowDialog();
            //OK button was pressed.
            if (result == DialogResult.OK)
                m_txtResourceFile.Text = resourceFileDialog.FileName;
        }

        void m_tbTextureCreation_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!m_isNewAction || (m_isCreateFromTextureArray && e.TabPage == m_TextureArrayTab))
            {
                e.Cancel = true;
                return;
            }
            SetUIByTextureTypeAndFormType();
        }


        private void SetUIByTextureTypeAndFormType()
        {
            txtTextureUniqueName.Enabled = false;
            txtTextureUniqueName.Visible = true;
            label11.Visible = true;
            chkDirectXTexture.Visible = false;
          //  pnlSrcSize.Visible = true;

            TabPage selectedTab = m_tbTextureCreation.SelectedTab;
            
            bool isFooterHide = (((selectedTab == m_MemoryBuffer || selectedTab == m_TextureArrayTab || selectedTab == m_FromListTab || selectedTab == m_VideoFiles) && m_isNewAction) ||
                     (selectedTab == m_FromListTab && m_TextureSourceForm == FontTextureSourceForm.Update));

            ChangeControlsVisibility(!isFooterHide);

            if(selectedTab == m_MemoryBuffer)
            {
                txtTextureUniqueName.Visible = true;
                label11.Visible = true;
                chkDirectXTexture.Visible = true;
                chkDirectXTexture.Enabled = txtTextureUniqueName.Enabled = m_isNewAction;
                pnlSrcSize.Visible = false ;

                if (m_TextureSourceForm == FontTextureSourceForm.Update)
                {
                    chxColorsAutoMipmap.Enabled = false;
                    cmbMemBufferColorsTextureUsage.Enabled = false;
                    cmbMemBufferPixelFormat.Enabled = false;
                    ntxMemBufferColorHeight.Enabled = false;
                    ntxMemBufferColorWidth.Enabled = false;

                }
            }

            if (selectedTab == m_FromListTab)
            {
                m_btnCreateTexture.Text = "Take Selected";
            }
            else
            {
                ChangeButtonText();
            }

            if(m_TextureSourceForm == FontTextureSourceForm.Update)
            {
                btnCreateNewTexture.Visible = false;
                btnRemoveFromList.Visible = false;
                btnDuplicateTexture.Visible = false;
                btnUp.Visible = false;
                btnDown.Visible = false;
                cmbMemBufferTextureUsage.Enabled = false;
                chxAutoMipmap.Enabled = false;
                chxFillPattern.Enabled = false;
                chxIgnoreTransparentMargin.Enabled = false;


            }
        }

        private void ChangeControlsVisibility(bool isVisible)
        {
            m_chkUseExisting.Visible = isVisible;
            chxFillPattern.Visible = isVisible;
            chxIgnoreTransparentMargin.Visible = isVisible;
            txtTextureUniqueName.Visible = isVisible;
            label11.Visible = isVisible;
           
            pnlSrcSize.Visible = isVisible;
            pnlSize.Visible = isVisible;

            VisibleColorControls(isVisible);
        }

        private void ChangeButtonText()
        {
            if (m_currentTexture == null)
            {
                m_btnCreateTexture.Text = "Create";
            }
            else
            {
                btnClose.Text = "Close";

                if (m_currentTexture.TextureType != DNETextureType._ETT_TEXTURE_ARRAY)
                {
                    m_btnCreateTexture.Text = m_TextureSourceForm == FontTextureSourceForm.Recreate? "Recreate" : "Update";
                    
                }
                else
                {
                    m_btnCreateTexture.Visible = false;
               }
            }
        }

        #endregion

        private void btnDeleteExistingTexture_Click(object sender, EventArgs e)
        {
            if (lstExistingTextures.SelectedItem != null)
            {
                IDNMcTexture selectedTexture = m_lstExistingTexturesValues[lstExistingTextures.SelectedIndex];

                Managers.ObjectWorld.Manager_MCTextures.RemoveFromDictionary(selectedTexture);
                selectedTexture.Dispose();

                m_lstExistingTexturesValues.RemoveAt(lstExistingTextures.SelectedIndex);
                lstExistingTextures.Items.RemoveAt(lstExistingTextures.SelectedIndex);

            }
        }

        private void btnGetDeviceIndex_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_currentTexture != null)
                    ntxDirectShowUSBCameraDeviceIndex.SetUInt32(((IDNMcDirectShowUSBCameraTexture)m_currentTexture).GetDeviceIndex());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetDeviceIndex", McEx);
            }
        }

        private void btnGetUSBDevices_Click(object sender, EventArgs e)
        {
            try
            {
                ntxDirectShowUSBCameraDeviceIndex.Text = "";
                string[] USBDevicesNames;
                USBDevicesNames = DNMcDirectShowUSBCameraTexture.GetUSBDevices();

                foreach (string name in USBDevicesNames)
                {
                    ntxDirectShowUSBCameraDeviceIndex.Text += (name + ",");
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetUSBDevices", McEx);
            }
        }

        private void btnSetDeviceIndex_Click(object sender, EventArgs e)
        {
            if (m_currentTexture != null && m_currentTexture is IDNMcDirectShowUSBCameraTexture)
            {
                try
                {
                    ((IDNMcDirectShowUSBCameraTexture)m_currentTexture).SetDeviceIndex(ntxDirectShowUSBCameraDeviceIndex.GetUInt32());
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetDeviceIndex", McEx);
                }
            }
        }

        private void rbRunningState_CheckedChanged(object sender, EventArgs e)
        {
            SetVideoState(CurrentTexture as IDNMcVideoTexture, DNETextureState._ES_RUNNING);
        }



        private void rbStoppedState_CheckedChanged(object sender, EventArgs e)
        {
            SetVideoState(CurrentTexture as IDNMcVideoTexture, DNETextureState._ES_STOPPED);
        }

        private void rbPausedState_CheckedChanged(object sender, EventArgs e)
        {
            SetVideoState(CurrentTexture as IDNMcVideoTexture, DNETextureState._ES_PAUSED);
        }

        private void SetVideoState(IDNMcVideoTexture videoTexture, DNETextureState state)
        {
            if (videoTexture != null)
            {
                try
                {
                    videoTexture.SetState(state);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("IDNMcVideoTexture.SetState", McEx);
                }
            }
        }

        private void rbTypeGraph_CheckedChanged(object sender, EventArgs e)
        {
            ChangeUIVideoTexture();
        }

        private void rbTypeUSBCamera_CheckedChanged(object sender, EventArgs e)
        {
            ChangeUIVideoTexture();
        }

        private void ChangeUIVideoTexture()
        {
            chxVideoWithSound.Visible = false;
            pnlHtmlSourceName.Visible = false;
            pnlFrameRateForRenderBasedUpdate.Visible = false;

            if (rbTypeDirectShowGraph.Checked)
            {
                label30.Visible = ctrlBrowseTextureVideo.Visible = true;
                pnlUpdateMethod.Visible = true;
                pnlUSBDevice.Visible = false;
                chxVideoPlayInLoop.Visible = false;
                pnlPixelFormat.Visible = true;
                pnlVideoSourceFormat.Visible = false;
                pnlSharedMemoryName.Visible = false;
                pnlFrameRateForRenderBasedUpdate.Visible = true;
            }
            else if (rbTypeDirectShowUSBCamera.Checked)
            {
                label30.Visible = ctrlBrowseTextureVideo.Visible = false;
                pnlUpdateMethod.Visible = true;
                pnlUSBDevice.Visible = true;
                chxVideoPlayInLoop.Visible = false;
                pnlPixelFormat.Visible = true;
                pnlVideoSourceFormat.Visible = false;
                pnlSharedMemoryName.Visible = false;
                pnlFrameRateForRenderBasedUpdate.Visible = true;
            }
            else if (rbTypeFFMpegVideo.Checked)
            {
                label30.Visible = ctrlBrowseTextureVideo.Visible = true;
                pnlUpdateMethod.Visible = false;
                pnlUSBDevice.Visible = false;
                chxVideoPlayInLoop.Visible = true;
                pnlPixelFormat.Visible = false;
                pnlVideoSourceFormat.Visible = true;
                pnlSharedMemoryName.Visible = false;
            }
            else if (rbTypeSharedMemoryVideo.Checked)
            {
                label30.Visible = ctrlBrowseTextureVideo.Visible = false;
                pnlUpdateMethod.Visible = false;
                pnlUSBDevice.Visible = false;
                chxVideoPlayInLoop.Visible = true;
                pnlPixelFormat.Visible = true;
                pnlVideoSourceFormat.Visible = false;
                pnlSharedMemoryName.Visible = true;
            }
            else if(rbTypeHtmlVideo.Checked)
            {
                pnlHtmlSourceName.Visible = true;
                label30.Visible = ctrlBrowseTextureVideo.Visible = false;
                chxVideoWithSound.Visible = true;
                pnlUpdateMethod.Visible = false;
                pnlUSBDevice.Visible = false;
                chxVideoPlayInLoop.Visible = true;
                pnlPixelFormat.Visible = false;
                pnlVideoSourceFormat.Visible = false;
                pnlSharedMemoryName.Visible = false;
                pnlFrameRateForRenderBasedUpdate.Visible = true;
            }
        }

        private void rbTypeFFMpegVideo_CheckedChanged(object sender, EventArgs e)
        {
            ChangeUIVideoTexture();
        }

        private void rbTypeSharedMemoryVideo_CheckedChanged(object sender, EventArgs e)
        {
            ChangeUIVideoTexture();
        }

        private void rbTypeHtmlVideo_CheckedChanged(object sender, EventArgs e)
        {
            ChangeUIVideoTexture();
        }

        private void lstExistingTextures_DoubleClick(object sender, EventArgs e)
        {
            int index = lstExistingTextures.SelectedIndex;
            if (index >= 0)
            {
                IDNMcTexture texture = m_lstExistingTexturesValues[lstExistingTextures.SelectedIndex];
                CreateTextureForm createdTextureForm = new CreateTextureForm(FontTextureSourceForm.Update, texture);

                createdTextureForm.ShowDialog();
            }
        }

        private void btnCreateNewTexture_Click(object sender, EventArgs e)
        {
            CreateTextureForm createdTextureForm = new CreateTextureForm(FontTextureSourceForm.CreateDialog, true);
            if (createdTextureForm.ShowDialog() == DialogResult.OK)
            {
                if (createdTextureForm.CurrentTexture != null)
                {
                    string desc = GetFullTextureDesc(createdTextureForm.CurrentTexture);
                    m_lstExistingTexturesValues.Add(createdTextureForm.CurrentTexture);
                    lstExistingTextures.Items.Add(desc);

                    AddTextureToGrid(createdTextureForm.CurrentTexture);
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int totalRows = dgvTextures.Rows.Count;
            // get index of the row for the selected cell
            int rowIndex = GetSelectedRow();
            MoveTextureOnGrid(rowIndex, 0, rowIndex - 1);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int totalRows = dgvTextures.Rows.Count;
            // get index of the row for the selected cell
            int rowIndex = GetSelectedRow();
            MoveTextureOnGrid(rowIndex, totalRows - 1, rowIndex + 1);
        }

        private void MoveTextureOnGrid(int rowIndex, int indexRowToCheck, int indexRowToMove)
        {
            DataGridView dgv = dgvTextures;
            try
            {
                // get index of the row for the selected cell
                if (rowIndex >= 0)
                {
                    if (rowIndex == indexRowToCheck)
                        return;
                    // get index of the column for the selected cell
                    int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                    DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                    dgv.Rows.Remove(selectedRow);
                    dgv.Rows.Insert(indexRowToMove, selectedRow);
                    dgv.ClearSelection();
                    SelectRow(indexRowToMove);
                }
            }
            catch { }
        }

        private void SelectRow(int indexRow)
        {
            dgvTextures.CurrentCell = dgvTextures.Rows[indexRow].Cells[0];
            dgvTextures.Rows[indexRow].Selected = true;
        }

        private void btnDuplicateTexture_Click(object sender, EventArgs e)
        {
            int rowIndex = GetSelectedRow();
            if (rowIndex >= 0)
            {
                AddTextureToGrid((IDNMcTexture)dgvTextures.Rows[rowIndex].Tag);
            }
        }

        private void btnRemoveFromList_Click(object sender, EventArgs e)
        {
            int rowIndex = GetSelectedRow();
            if (rowIndex >= 0)
            {
                dgvTextures.Rows.RemoveAt(rowIndex);
            }
        }

        private int GetSelectedRow()
        {
            if (dgvTextures.SelectedCells.Count > 0)
                return dgvTextures.SelectedCells[0].OwningRow.Index;
            else
                return -1;
        }

        private void dgvTextures_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex <= dgvTextures.Rows.Count)
            {
                IDNMcTexture mcTexture = (IDNMcTexture)dgvTextures.Rows[e.RowIndex].Tag;
                CreateTextureForm createTextureForm = new CreateTextureForm(FontTextureSourceForm.Update, mcTexture);
                createTextureForm.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSaveTextureToFile_Click(object sender, EventArgs e)
        {
            Manager_MCObjectSchemeItem.SaveImageFileTextureToFile(m_imageTexture);
        }

        private void btnSaveTextureToFileMP_Click(object sender, EventArgs e)
        {
            Manager_MCObjectSchemeItem.SaveMemoryBufferTextureToFile(m_MemoryBufferTexture);
        }

        private void btnSaveAllTexturesToFolder_Click(object sender, EventArgs e)
        {
            foreach(IDNMcTexture texture in m_lstExistingTexturesValues) {
                Manager_MCObjectSchemeItem.CheckSaveTextureToFolder(texture);
            }
            Manager_MCObjectSchemeItem.SaveTexturesToFolder();
        }

        private void btnSaveSelectedTextureToFile_Click(object sender, EventArgs e)
        {
            if (lstExistingTextures.SelectedItem != null)
            {
                IDNMcTexture selectedTexture = m_lstExistingTexturesValues[lstExistingTextures.SelectedIndex];
                Manager_MCObjectSchemeItem.CheckSaveTextureToFolder(selectedTexture);
                Manager_MCObjectSchemeItem.SaveTexturesToFolder();
            }
            else
            {
                MessageBox.Show("Please select texture from list before.", "Save Selected Texture To File");
            }
        }

        private void lstExistingTextures_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rbMemoryBufferBMFFile_CheckedChanged(object sender, EventArgs e)
        {
            pnlMemoryBufferFile.Visible = rbMemoryBufferBMFFile.Checked;
            txtTextureUniqueName.Enabled = rbMemoryBufferBMFFile.Checked && m_isNewAction;
        }

        private void rbMemoryBufferColorArray_CheckedChanged(object sender, EventArgs e)
        {
            pnlMemoryBufferColors.Visible = rbMemoryBufferColorArray.Checked;
            if(m_tbTextureCreation.SelectedTab == m_MemoryBuffer)
                pnlSize.Visible = rbMemoryBufferColorArray.Checked; 
        }

        private void dgvColorPositions_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgvColorPositions.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex).ToString();
        }

        private void btnClearColors_Click(object sender, EventArgs e)
        {
            ctrlMemBufferColors.ResetGrid();
        }

        private void btnClearColorPositions_Click(object sender, EventArgs e)
        {
            dgvColorPositions.Rows.Clear();
        }

        private void m_tbTextureCreation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbIsMemoryBuffer_CheckedChanged(object sender, EventArgs e)
        {
            chxUseFileExtension.Enabled = cbIsMemoryBuffer.Checked;  
        }
    }
}
