using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.MapWorld;
using MCTester.General_Forms;
using MCTester.Managers.MapWorld;

namespace MCTester.Controls
{
    public partial class CtrlRawVectorParams : UserControl
    {
        IDNMcFont mStylingFont = null;
        private bool m_isReadOnly = false;

        public static DNSRawVectorParams GetSRawVectorParamsWithDefaultValue()
        {
            DNSInternalStylingParams stylingParams = new DNSInternalStylingParams();
            stylingParams.eVersion = DNESavingVersionCompatibility._ESVC_LATEST;

            DNSRawVectorParams rawVectorParams = new DNSRawVectorParams("", null);
            rawVectorParams.eAutoStylingType = DNEAutoStylingType._EAST_INTERNAL;
            rawVectorParams.fMaxScale = float.MaxValue;
           // rawVectorParams.strPointTextureFile = MCTMapLayer.DefualtPointTexturePath;
            rawVectorParams.StylingParams = stylingParams;
            return rawVectorParams;
        }

        public CtrlRawVectorParams()
        {
            InitializeComponent();
            cbSavingVersionCompatibility.Items.AddRange(Enum.GetNames(typeof(DNESavingVersionCompatibility)));
            cbSavingVersionCompatibility.Text = DNESavingVersionCompatibility._ESVC_LATEST.ToString();
            MaxScale = float.MaxValue;
            TextMaxScale = float.MaxValue;

            cmbAutoStylingType.Items.AddRange(Enum.GetNames(typeof(DNEAutoStylingType)));
            cmbAutoStylingType.Text = DNEAutoStylingType._EAST_INTERNAL.ToString();

           // ctrlBrowsePointTextureFile.FileName = MCTMapLayer.DefualtPointTexturePath;

            DNSRawVectorParams rawVectorParams = new DNSRawVectorParams("", null);
            MaxNumVerticesPerTile = rawVectorParams.uMaxNumVerticesPerTile;
            MinPixelSizeForObjectVisibility = rawVectorParams.uMinPixelSizeForObjectVisibility;
            OptimizationMinScale = rawVectorParams.fOptimizationMinScale;
            MaxNumVisiblePointObjectsPerTile = rawVectorParams.uMaxNumVisiblePointObjectsPerTile;
            ctrlBrowseStrOutputXMLFileName.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            ctrlBrowseStylingFileName.Filter = "Styling files (*.sld, *.lyrx)|*.sld;*.lyrx|All files (*.*)|*.*";
            /*
                        chxStylingParams.Checked = true;
                        chxStylingParams_CheckedChanged(null, null);
            */
        }

        internal void IsReadOnly(bool isReadOnly)
        {
            m_isReadOnly = isReadOnly;
        }

        public void HideGridCoordinateSystemParams()
        {
            ctrlSourceGridCoordinateSystem.Visible = false;
            ctrlTargetGridCoordinateSystem.Visible = false;
        }

        public CtrlRawVectorParams(DNSRawVectorParams mcRawVectorParams)
        {
            RawVectorParams = mcRawVectorParams;
        }

        private string RawVectorParamsText { set { gbRawVectorParams.Text = value; } }

        private float MinScale
        {
            get { return ntxRawVectorMinScale.GetFloat(); }
            set { ntxRawVectorMinScale.SetFloat(value); }
        }

        private float MaxScale
        {
            get { return ntxRawVectorMaxScale.GetFloat(); }
            set { ntxRawVectorMaxScale.SetFloat(value); }
        }

        private string strPointTextureFile
        {
            get { return ctrlBrowsePointTextureFile.FileName; }
            set { ctrlBrowsePointTextureFile.FileName = value; }
        }

        private string strLocaleStr
        {
            get { return tbLocaleStr.Text; }
            set { tbLocaleStr.Text = value; }
        }

        private double SimplificationTolerance
        {
            get { return ntxSimplificationTolerance.GetDouble(); }
            set { ntxSimplificationTolerance.SetDouble(value); }
        }

        internal void SetDisableStyling(string rawVectorFileName)
        {
            bool isDisable = rawVectorFileName.ToLower().Contains(".sld") || rawVectorFileName.ToLower().Contains(".lyrx"); 
            ctrlBrowseStylingFileName.Enabled = !isDisable;
            ctrlBrowseStylingFileName.FileName = isDisable? Manager_MCLayers.RawVectorMultiple :
                ctrlBrowseStylingFileName.FileName == Manager_MCLayers.RawVectorMultiple? "" : ctrlBrowseStylingFileName.FileName;
        }

        private DNSMcBox? pClipRect
        {
            get {
                if (cgbWorldLimit.Checked)
                    return new DNSMcBox(ctrl2DMinVectorLayerParams2.GetVector2D(), ctrl2DMaxVectorLayerParams2.GetVector2D());
                else
                    return null;
            }
            set
            {
                cgbWorldLimit.Checked = value.HasValue;
                if (value.HasValue)
                {
                    ctrl2DMinVectorLayerParams2.SetVector2D(new DNSMcVector2D(value.Value.MinVertex.x, value.Value.MinVertex.y));
                    ctrl2DMaxVectorLayerParams2.SetVector2D(new DNSMcVector2D(value.Value.MaxVertex.x, value.Value.MaxVertex.y));
                }
            }
        }

        private string strOutputFolder
        {
            get { return ctrlBrowseStrOutputFolder.FileName; }
            set { ctrlBrowseStrOutputFolder.FileName = value; }
        }

        private string strOutputXMLFileName
        {
            get { return ctrlBrowseStrOutputXMLFileName.FileName; }
            set { ctrlBrowseStrOutputXMLFileName.FileName = value; }
        }

        private DNESavingVersionCompatibility eVersion
        {
            get
            {
                DNESavingVersionCompatibility SavingVersionCompatibility = DNESavingVersionCompatibility._ESVC_LATEST;
                if (cbSavingVersionCompatibility.Text != "")
                    SavingVersionCompatibility = (DNESavingVersionCompatibility)Enum.Parse(typeof(DNESavingVersionCompatibility), cbSavingVersionCompatibility.Text);
                return SavingVersionCompatibility;
            }
            set
            {
                cbSavingVersionCompatibility.SelectedItem = value.ToString();
            }
        }

        private DNEAutoStylingType eAutoStylingType
        {
            get
            {
                DNEAutoStylingType autoStylingType = DNEAutoStylingType._EAST_INTERNAL;
                if (cmbAutoStylingType.Text != "")
                    autoStylingType = (DNEAutoStylingType)Enum.Parse(typeof(DNEAutoStylingType), cmbAutoStylingType.Text);
                return autoStylingType;
            }
            set
            {
                cmbAutoStylingType.SelectedItem = value.ToString();
            }
        }

        private string strCustomStylingFolder
        {
            get { return ctrlBrowseCustomStylingFolder.FileName; }
            set { ctrlBrowseCustomStylingFolder.FileName = value; }
        }

        private string strStylingFile
        {
            get { return ctrlBrowseStylingFileName.FileName; }
            set { ctrlBrowseStylingFileName.FileName = value; }
        }

        private float MaxScaleFactor
        {
            get { return ntxRawVectorMaxScaleFactor.GetFloat(); }
            set { ntxRawVectorMaxScaleFactor.SetFloat(value); }
        }

        private float TextMaxScale
        {
            get { return ntxRawVectorTextMaxScale.GetFloat(); }
            set { ntxRawVectorTextMaxScale.SetFloat(value); }
        }

        private uint MaxNumVerticesPerTile
        {
            get { return ntxMaxNumVerticesPerTile.GetUInt32(); }
            set { ntxMaxNumVerticesPerTile.SetUInt32(value); }
        }

        private uint MinPixelSizeForObjectVisibility
        {
            get { return ntxMinPixelSizeForObjectVisibility.GetUInt32(); }
            set { ntxMinPixelSizeForObjectVisibility.SetUInt32(value); }
        }

        private float OptimizationMinScale
        {
            get { return ntxOptimizationMinScale.GetFloat(); }
            set { ntxOptimizationMinScale.SetFloat(value); }
        }

        private uint MaxNumVisiblePointObjectsPerTile
        {
            get { return ntxMaxNumVisiblePointObjectsPerTile.GetUInt32(); }
            set { ntxMaxNumVisiblePointObjectsPerTile.SetUInt32(value); }
        }

        private IDNMcGridCoordinateSystem SourceGridCoordinateSystem
        {
            get { return ctrlSourceGridCoordinateSystem.GridCoordinateSystem; }
            set { ctrlSourceGridCoordinateSystem.GridCoordinateSystem = value; }
        }

        public IDNMcGridCoordinateSystem TargetGridCoordinateSystem
        {
            get { return ctrlTargetGridCoordinateSystem.GridCoordinateSystem; }
            set { ctrlTargetGridCoordinateSystem.GridCoordinateSystem = value; }
        }

        public DNSRawVectorParams RawVectorParams
        {
            get
            {
                DNSInternalStylingParams stylingParams = new DNSInternalStylingParams(); 
                stylingParams.strOutputFolder = strOutputFolder;
                stylingParams.strOutputXMLFileName = strOutputXMLFileName;
                stylingParams.eVersion = eVersion;
                stylingParams.fMaxScaleFactor = MaxScaleFactor;
                stylingParams.fTextMaxScale = TextMaxScale;
                stylingParams.pDefaultFont = mStylingFont;
                stylingParams.strStylingFile = strStylingFile == Manager_MCLayers.RawVectorMultiple ? "" : strStylingFile;

                DNSRawVectorParams rawVectorParams = new DNSRawVectorParams(
                        null,
                        SourceGridCoordinateSystem,
                        MinScale,
                        MaxScale,
                        strPointTextureFile,
                        strLocaleStr,
                        SimplificationTolerance,
                        pClipRect,
                        stylingParams,
                        eAutoStylingType,
                        strCustomStylingFolder);

                rawVectorParams.uMaxNumVerticesPerTile = MaxNumVerticesPerTile;
                rawVectorParams.uMinPixelSizeForObjectVisibility = MinPixelSizeForObjectVisibility;
                rawVectorParams.uMaxNumVisiblePointObjectsPerTile = MaxNumVisiblePointObjectsPerTile;
                rawVectorParams.fOptimizationMinScale = OptimizationMinScale;
                return rawVectorParams;
            }
            set
            {
                MinScale = value.fMinScale;
                MaxScale = value.fMaxScale;
                strPointTextureFile = value.strPointTextureFile;
                strLocaleStr = value.strLocaleStr;
                SimplificationTolerance = value.dSimplificationTolerance;
                pClipRect = value.pClipRect;
                eAutoStylingType = value.eAutoStylingType;
                strCustomStylingFolder = value.strCustomStylingFolder;

                if (value.StylingParams != null)
                {
                    strOutputFolder = value.StylingParams.strOutputFolder;
                    strOutputXMLFileName = value.StylingParams.strOutputXMLFileName;
                    strStylingFile = value.StylingParams.strStylingFile;
                    eVersion = value.StylingParams.eVersion;
                    MaxScaleFactor = value.StylingParams.fMaxScaleFactor;
                    TextMaxScale = value.StylingParams.fTextMaxScale;
                    mStylingFont = value.StylingParams.pDefaultFont;
                    btnDeleteFont.Enabled = mStylingFont != null;
                }

                MaxNumVerticesPerTile  = value.uMaxNumVerticesPerTile;
                MinPixelSizeForObjectVisibility = value.uMinPixelSizeForObjectVisibility;
                MaxNumVisiblePointObjectsPerTile = value.uMaxNumVisiblePointObjectsPerTile;
                OptimizationMinScale  = value.fOptimizationMinScale;
                SourceGridCoordinateSystem = value.pSourceCoordinateSystem;
            }
        }

        /*  private void chxStylingParams_CheckedChanged(object sender, EventArgs e)
          {
              // Make sure the CheckBox isn't in the GroupBox.
              // This will only happen the first time.
              if (chxStylingParams.Parent == grpStylingParams)
              {
                  // Reparent the CheckBox so it's not in the GroupBox.
                  grpStylingParams.Parent.Controls.Add(chxStylingParams);

                  // Adjust the CheckBox's location.
                  chxStylingParams.Location = new Point(
                      chxStylingParams.Left + grpStylingParams.Left,
                      chxStylingParams.Top + grpStylingParams.Top);

                  // Move the CheckBox to the top of the stacking order.
                  chxStylingParams.BringToFront();
              }

              // Enable or disable the GroupBox.
              grpStylingParams.Enabled = chxStylingParams.Checked;
          }*/

        private void btnSelectFont_Click(object sender, EventArgs e)
        {
            frmFontDialog fontDlg = new frmFontDialog(MainForm.FontTextureSourceForm.CreateDialog, mStylingFont);
            if(m_isReadOnly)
                fontDlg.SetReadOnly();

            // Show the dialog
            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                // Set the new font
                if (fontDlg.CurrFont != null)
                {
                    mStylingFont = fontDlg.CurrFont;
                    btnDeleteFont.Enabled = true;
                    btnSelectFont.Text = "Update Text";
                }
            }
        }


        private void cmbAutoStylingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            grpStylingParams.Enabled = (cmbAutoStylingType.Text == DNEAutoStylingType._EAST_INTERNAL.ToString());
            ctrlBrowseCustomStylingFolder.Enabled = (cmbAutoStylingType.Text == DNEAutoStylingType._EAST_CUSTOM.ToString());
        }

        private void btnDeleteFont_Click(object sender, EventArgs e)
        {
            mStylingFont = null;
            btnDeleteFont.Enabled = false;
            btnSelectFont.Text = "Selected Text";
        }
    }
}
