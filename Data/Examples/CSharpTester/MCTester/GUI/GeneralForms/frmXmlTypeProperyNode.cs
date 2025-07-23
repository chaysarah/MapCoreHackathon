using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.General_Forms
{
    public partial class frmXmlTypeProperyNode : Form
    {
        DNEPropertyType m_PropertyType;
        object m_PropertyNode = null;

        private uint MASK0 = 1;
        private uint MASK1 = 2;
        private uint MASK2 = 4;
        private uint MASK3 = 8;
        private uint MASK4 = 16;
        private uint MASK5 = 32;
        private uint MASK6 = 64;
        private uint MASK7 = 128;
        private uint MASK8 = 256;
        private uint MASK9 = 512;

        public frmXmlTypeProperyNode(DNEPropertyType propType,Object propNode):this(propType)
        {
            PropertyNode = propNode;
            switch (propType)
            {
                case DNEPropertyType._EPT_BOOL:

                    chxBaseBool.Checked = ((DNSXmlProperyNode<bool>)PropertyNode).Value;
                    txtValue1.Text=((DNSXmlProperyNode<bool>)PropertyNode).strFieldName;
                    break;
                case DNEPropertyType._EPT_BYTE:
                    
                    ntxBaseType.SetByte(((DNSXmlProperyNode<byte>)PropertyNode).Value) ;
                    txtValue1.Text=((DNSXmlProperyNode<byte>)PropertyNode).strFieldName;
                    break;
                case DNEPropertyType._EPT_SBYTE:

                    ntxBaseType.SetSByte(((DNSXmlProperyNode<sbyte>)PropertyNode).Value);
                    txtValue1.Text = ((DNSXmlProperyNode<sbyte>)PropertyNode).strFieldName;
                    break;
                case DNEPropertyType._EPT_DOUBLE:

                    ntxBaseType.SetDouble(((DNSXmlProperyNode<double>)PropertyNode).Value);
                    txtValue1.Text=((DNSXmlProperyNode<double>)PropertyNode).strFieldName;
                    break;
                case DNEPropertyType._EPT_FLOAT:
                    ntxBaseType.SetFloat(((DNSXmlProperyNode<float>)PropertyNode).Value);
                    txtValue1.Text=((DNSXmlProperyNode<float>)PropertyNode).strFieldName;
                    break;
                case DNEPropertyType._EPT_INT:
                    ntxBaseType.SetInt(((DNSXmlProperyNode<int>)PropertyNode).Value);
                    txtValue1.Text=((DNSXmlProperyNode<int>)PropertyNode).strFieldName;
                    break;
                case DNEPropertyType._EPT_STRING:
                    ntxBaseType.Text=((DNSXmlProperyNode<string>)PropertyNode).Value;
                    txtValue1.Text=((DNSXmlProperyNode<string>)PropertyNode).strFieldName;
                    break;
                case DNEPropertyType._EPT_ENUM:
                case DNEPropertyType._EPT_UINT:
                    ntxBaseType.SetUInt32(((DNSXmlProperyNode<uint>)PropertyNode).Value);
                    txtValue1.Text=((DNSXmlProperyNode<uint>)PropertyNode).strFieldName;
                    break;
                 case DNEPropertyType._EPT_BCOLOR:
                    //picbBaseColor.BackColor = new DNSMcBColor(picbBaseColor.BackColor.R, picbBaseColor.BackColor.G, picbBaseColor.BackColor.B, (byte)numUDBaseAlphaColor.Value);
                    DNSMcBColor bColor = ((DNSXmlProperyNode<DNSMcBColor>)PropertyNode).Value;
                    numUDBaseAlphaColor.Value = bColor.a;
                    picbBaseColor.BackColor = Color.FromArgb(255, bColor.r, bColor.g, bColor.b);

                    txtValue1.Text=((DNSXmlProperyNode<DNSMcBColor>)PropertyNode).strFieldName;
                    break;
                case DNEPropertyType._EPT_FVECTOR2D:
                    ntxVector2DX.SetFloat(((DNSXmlFVector2DProperyNode)PropertyNode).X.Value);
                    txtValue1.Text=((DNSXmlFVector2DProperyNode)PropertyNode).X.strFieldName;
                    ntxVector2DY.SetFloat(((DNSXmlFVector2DProperyNode)PropertyNode).Y.Value);
                    txtValue2.Text=((DNSXmlFVector2DProperyNode)PropertyNode).Y.strFieldName;
                    break;
                case DNEPropertyType._EPT_VECTOR2D:
                    ntxVector2DX.SetDouble(((DNSXmlVector2DProperyNode)PropertyNode).X.Value);
                    txtValue1.Text=((DNSXmlVector2DProperyNode)PropertyNode).X.strFieldName;
                    ntxVector2DY.SetDouble(((DNSXmlVector2DProperyNode)PropertyNode).Y.Value);
                    txtValue2.Text=((DNSXmlVector2DProperyNode)PropertyNode).Y.strFieldName;
                    break;
                case DNEPropertyType._EPT_FVECTOR3D:
                    ntxVector3DX.SetFloat(((DNSXmlFVector3DProperyNode)PropertyNode).X.Value);
                    txtValue1.Text=((DNSXmlFVector3DProperyNode)PropertyNode).X.strFieldName;
                    ntxVector3DY.SetFloat(((DNSXmlFVector3DProperyNode)PropertyNode).Y.Value);
                    txtValue2.Text=((DNSXmlFVector3DProperyNode)PropertyNode).Y.strFieldName;
                    ntxVector3DZ.SetFloat(((DNSXmlFVector3DProperyNode)PropertyNode).Z.Value);
                    txtValue3.Text=((DNSXmlFVector3DProperyNode)PropertyNode).Z.strFieldName;
                    break;
                case DNEPropertyType._EPT_VECTOR3D:
                    ntxVector3DX.SetDouble(((DNSXmlVector3DProperyNode)PropertyNode).X.Value);
                    txtValue2.Text=((DNSXmlVector3DProperyNode)PropertyNode).X.strFieldName;
                    ntxVector3DY.SetDouble(((DNSXmlVector3DProperyNode)PropertyNode).Y.Value);
                    txtValue2.Text=((DNSXmlVector3DProperyNode)PropertyNode).Y.strFieldName;
                    ntxVector3DZ.SetDouble(((DNSXmlVector3DProperyNode)PropertyNode).Z.Value);
                    txtValue3.Text=((DNSXmlVector3DProperyNode)PropertyNode).Z.strFieldName;
                    break;
                case DNEPropertyType._EPT_TEXTURE:
                    DNSXmlProperyNodeComponent<string> imageFileName  = new DNSXmlProperyNodeComponent<string>();
                    DNSXmlProperyNodeComponent<DNSMcBColor> transparentColor = new DNSXmlProperyNodeComponent<DNSMcBColor>();
                    DNSXmlProperyNodeComponent<bool> isTransparentColorEnabled = new DNSXmlProperyNodeComponent<bool>();
                    DNSXmlProperyNodeComponent<bool> ignoreTransparentMargin = new DNSXmlProperyNodeComponent<bool>();

                    imageFileName=((DNSXmlTextureProperyNode)PropertyNode).ImageFileName ;
                    transparentColor=((DNSXmlTextureProperyNode)PropertyNode).TransparentColor ;
                    isTransparentColorEnabled=((DNSXmlTextureProperyNode)PropertyNode).IsTransparentColorEnabled ;
                    ignoreTransparentMargin=((DNSXmlTextureProperyNode)PropertyNode).IgnoreTransparentMargin ;

                    ctrlBrowseTextureFileName.FileName = imageFileName.Value ;
                    txtValue1.Text = imageFileName.strFieldName ;

                    chxIsTransparentColorEnabled.Checked=isTransparentColorEnabled.Value ;
                    txtValue3.Text=isTransparentColorEnabled.strFieldName ;

                    chxIgnoreTransparentMargin.Checked=ignoreTransparentMargin.Value ;
                    txtValue4.Text=ignoreTransparentMargin.strFieldName ;

                  
                    numUDTextureAlphaColor.Value = transparentColor.Value.a;
                    picbTextureColor.BackColor = Color.FromArgb(255, transparentColor.Value.r, transparentColor.Value.g, transparentColor.Value.b);
                    txtValue2.Text=transparentColor.strFieldName ;

                     break;
                case DNEPropertyType._EPT_FONT:
                    DNSXmlProperyNodeComponent<int> height = new DNSXmlProperyNodeComponent<int>();
                    DNSXmlProperyNodeComponent<string> faceName = new DNSXmlProperyNodeComponent<string>();
                    DNSXmlProperyNodeComponent<uint> charSet = new DNSXmlProperyNodeComponent<uint>();
                    
                    height = ((DNSXmlFontProperyNode)PropertyNode).Height;
                    faceName = ((DNSXmlFontProperyNode)PropertyNode).FaceName;
                    charSet  = ((DNSXmlFontProperyNode)PropertyNode).CharSet;
                    
                    ntxFontHeight.SetInt(height.Value);
                    txtValue1.Text=height.strFieldName ;

                    ntxFontFaceName.Text=faceName.Value;
                    faceName.strFieldName = txtValue2.Text;

                    ntxFontCharSet.SetUInt32(charSet.Value);
                    txtValue3.Text = charSet.strFieldName;
                    
                    break;
                case DNEPropertyType._EPT_CONDITIONALSELECTOR:
                    DNSXmlProperyNodeComponent<float> minScale = new DNSXmlProperyNodeComponent<float>();
                    DNSXmlProperyNodeComponent<float> maxScale = new DNSXmlProperyNodeComponent<float>();
                    DNSXmlProperyNodeComponent<uint> cancelScaleMode = new DNSXmlProperyNodeComponent<uint>();
                    DNSXmlProperyNodeComponent<uint> cancelScaleModeResult = new DNSXmlProperyNodeComponent<uint>();
                    minScale= ((DNSXmlScaleConditionalSelectorProperyNode)PropertyNode).Min  ;
                    maxScale  = ((DNSXmlScaleConditionalSelectorProperyNode)PropertyNode).Max ;
                    cancelScaleMode= ((DNSXmlScaleConditionalSelectorProperyNode)PropertyNode).CancelScaleMode  ;
                    cancelScaleModeResult = ((DNSXmlScaleConditionalSelectorProperyNode)PropertyNode).CancelScaleModeResult;
                    ntxScaleConditionalSelectorMin.SetFloat( minScale.Value) ;
                    txtValue1.Text= minScale.strFieldName  ;

                     ntxScaleConditionalSelectorMax.SetFloat( maxScale.Value ) ;
                     txtValue2.Text=maxScale.strFieldName ;

                   // cancelScaleMode.Value = GetCancelScaleModeValue();
                    cancelScaleMode.strFieldName = txtValue3.Text;

                   // cancelScaleModeResult.Value = GetCancelScaleModeResultValue();
                    cancelScaleModeResult.strFieldName = txtValue4.Text;
                    
                  
                    break;
                case DNEPropertyType._EPT_SUBITEM_ARRAY:
                     txtValue1.Text=((DNSXmlSubItemsDataProperyNode)PropertyNode).strPropertyName ;
                    break;
                case DNEPropertyType._EPT_NUM:
                    break;
                default:
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    break;                    
           
            }
        }
        public frmXmlTypeProperyNode(DNEPropertyType propType)
        {
            InitializeComponent();
            m_PropertyType = propType;

            // set form appearance
            switch (m_PropertyType)
            {
                case DNEPropertyType._EPT_BOOL:
                case DNEPropertyType._EPT_BYTE:
                case DNEPropertyType._EPT_ENUM:
                case DNEPropertyType._EPT_SBYTE:
                case DNEPropertyType._EPT_DOUBLE:
                case DNEPropertyType._EPT_FLOAT:
                case DNEPropertyType._EPT_INT:
                case DNEPropertyType._EPT_STRING:
                case DNEPropertyType._EPT_UINT:
                case DNEPropertyType._EPT_BCOLOR:
                    gbBaseTypes.Enabled = true;
                    if (m_PropertyType == DNEPropertyType._EPT_BCOLOR || m_PropertyType == DNEPropertyType._EPT_BOOL)
                    {
                        if (m_PropertyType == DNEPropertyType._EPT_BCOLOR)
                        {
                            lblBaseValue.Enabled = false;
                            ntxBaseType.Enabled = false;
                            chxBaseBool.Enabled = false;
                        }
                        else
                        {
                            lblBaseValue.Enabled = false;
                            ntxBaseType.Enabled = false;
                            lblBaseValue.Enabled = false;
                            labelBaseAlpha.Enabled = false;
                            picbBaseColor.Enabled = false;
                            numUDBaseAlphaColor.Enabled = false;
                        }
                    }
                    else
                    {
                        lblBaseValue.Enabled = false;
                        labelBaseAlpha.Enabled = false;
                        picbBaseColor.Enabled = false;
                        numUDBaseAlphaColor.Enabled = false;
                        chxBaseBool.Enabled = false;
                    }
                    break;
                case DNEPropertyType._EPT_FVECTOR2D:
                case DNEPropertyType._EPT_VECTOR2D:
                    gbVector2D.Enabled = true;
                    txtValue2.Enabled = true;
                    break;
                case DNEPropertyType._EPT_FVECTOR3D:
                case DNEPropertyType._EPT_VECTOR3D:
                    gbVector3D.Enabled = true;
                    txtValue2.Enabled = true;
                    txtValue3.Enabled = true;
                    break;
                case DNEPropertyType._EPT_TEXTURE:
                    gbTexture.Enabled = true;
                    txtValue2.Enabled = true;
                    txtValue3.Enabled = true;
                    txtValue4.Enabled = true;
                    break;
                case DNEPropertyType._EPT_FONT:
                    gbFont.Enabled = true;
                    txtValue2.Enabled = true;
                    txtValue3.Enabled = true;
                    break;
                case DNEPropertyType._EPT_CONDITIONALSELECTOR:
                    gbScaleConditionalSelector.Enabled = true;
                    txtValue2.Enabled = true;
                    txtValue3.Enabled = true;
                    txtValue4.Enabled = true;
                    break;
                case DNEPropertyType._EPT_NUM:
                case DNEPropertyType._EPT_SUBITEM_ARRAY:
                    break;


            }
        }

        private void picbBaseColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = new Color();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                picbBaseColor.BackColor = Color.FromArgb(255, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
            }
        }

        private void picbTextureColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = new Color();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                picbTextureColor.BackColor = Color.FromArgb(255, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
            }
        }

        //private void numUDBaseAlphaColor_ValueChanged(object sender, EventArgs e)
        //{
        //    picbBaseColor.BackColor = Color.FromArgb((int)numUDBaseAlphaColor.Value, picbBaseColor.BackColor);            
        //}

        //private void numUDTextureAlphaColor_ValueChanged(object sender, EventArgs e)
        //{
        //    picbTextureColor.BackColor = Color.FromArgb((int)numUDTextureAlphaColor.Value, picbTextureColor.BackColor);
        //}

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            switch (m_PropertyType)
            {
                case DNEPropertyType._EPT_BOOL:
                    PropertyNode = new DNSXmlProperyNode<bool>();
                    ((DNSXmlProperyNode<bool>)PropertyNode).Value = chxBaseBool.Checked;
                    ((DNSXmlProperyNode<bool>)PropertyNode).strFieldName = txtValue1.Text;
                    break;
                case DNEPropertyType._EPT_BYTE:
                    PropertyNode = new DNSXmlProperyNode<byte>();
                    ((DNSXmlProperyNode<byte>)PropertyNode).Value = ntxBaseType.GetByte();
                    ((DNSXmlProperyNode<byte>)PropertyNode).strFieldName = txtValue1.Text;
                    break;
                case DNEPropertyType._EPT_SBYTE:
                    PropertyNode = new DNSXmlProperyNode<sbyte>();
                    ((DNSXmlProperyNode<sbyte>)PropertyNode).Value = ntxBaseType.GetSByte();
                    ((DNSXmlProperyNode<sbyte>)PropertyNode).strFieldName = txtValue1.Text;
                    break;
                case DNEPropertyType._EPT_DOUBLE:
                    PropertyNode = new DNSXmlProperyNode<double>();
                    ((DNSXmlProperyNode<double>)PropertyNode).Value = ntxBaseType.GetDouble();
                    ((DNSXmlProperyNode<double>)PropertyNode).strFieldName = txtValue1.Text;
                    break;
                case DNEPropertyType._EPT_FLOAT:
                    PropertyNode = new DNSXmlProperyNode<float>();
                    ((DNSXmlProperyNode<float>)PropertyNode).Value = ntxBaseType.GetFloat();
                    ((DNSXmlProperyNode<float>)PropertyNode).strFieldName = txtValue1.Text;
                    break;
                case DNEPropertyType._EPT_INT:
                    PropertyNode = new DNSXmlProperyNode<int>();
                    ((DNSXmlProperyNode<int>)PropertyNode).Value = ntxBaseType.GetInt32();
                    ((DNSXmlProperyNode<int>)PropertyNode).strFieldName = txtValue1.Text;
                    break;
                case DNEPropertyType._EPT_STRING:
                    PropertyNode = new DNSXmlProperyNode<string>();
                    ((DNSXmlProperyNode<string>)PropertyNode).Value = ntxBaseType.Text;
                    ((DNSXmlProperyNode<string>)PropertyNode).strFieldName = txtValue1.Text;
                    break;
                case DNEPropertyType._EPT_ENUM:
                case DNEPropertyType._EPT_UINT:
                    PropertyNode = new DNSXmlProperyNode<uint>();
                    ((DNSXmlProperyNode<uint>)PropertyNode).Value = ntxBaseType.GetUInt32();
                    ((DNSXmlProperyNode<uint>)PropertyNode).strFieldName = txtValue1.Text;
                    break;
                case DNEPropertyType._EPT_BCOLOR:
                    PropertyNode = new DNSXmlProperyNode<DNSMcBColor>();
                    DNSMcBColor bColor = new DNSMcBColor(picbBaseColor.BackColor.R, picbBaseColor.BackColor.G, picbBaseColor.BackColor.B, (byte)numUDBaseAlphaColor.Value);
                    ((DNSXmlProperyNode<DNSMcBColor>)PropertyNode).Value = bColor;
                    ((DNSXmlProperyNode<DNSMcBColor>)PropertyNode).strFieldName = txtValue1.Text;
                    break;
                case DNEPropertyType._EPT_FVECTOR2D:
                    PropertyNode = new DNSXmlFVector2DProperyNode();
                    ((DNSXmlFVector2DProperyNode)PropertyNode).X.Value= ntxVector2DX.GetFloat();
                    ((DNSXmlFVector2DProperyNode)PropertyNode).X.strFieldName = txtValue1.Text;
                    ((DNSXmlFVector2DProperyNode)PropertyNode).Y.Value = ntxVector2DY.GetFloat();
                    ((DNSXmlFVector2DProperyNode)PropertyNode).Y.strFieldName = txtValue2.Text;
                    break;
                case DNEPropertyType._EPT_VECTOR2D:
                    PropertyNode = new DNSXmlVector2DProperyNode();
                    ((DNSXmlVector2DProperyNode)PropertyNode).X.Value = ntxVector2DX.GetDouble();
                    ((DNSXmlVector2DProperyNode)PropertyNode).X.strFieldName = txtValue1.Text;
                    ((DNSXmlVector2DProperyNode)PropertyNode).Y.Value = ntxVector2DY.GetDouble();
                    ((DNSXmlVector2DProperyNode)PropertyNode).Y.strFieldName = txtValue2.Text;
                    break;
                case DNEPropertyType._EPT_FVECTOR3D:
                    PropertyNode = new DNSXmlFVector3DProperyNode();
                    ((DNSXmlFVector3DProperyNode)PropertyNode).X.Value = ntxVector3DX.GetFloat();
                    ((DNSXmlFVector3DProperyNode)PropertyNode).X.strFieldName = txtValue1.Text;
                    ((DNSXmlFVector3DProperyNode)PropertyNode).Y.Value = ntxVector3DY.GetFloat();
                    ((DNSXmlFVector3DProperyNode)PropertyNode).Y.strFieldName = txtValue2.Text;
                    ((DNSXmlFVector3DProperyNode)PropertyNode).Z.Value = ntxVector3DZ.GetFloat();
                    ((DNSXmlFVector3DProperyNode)PropertyNode).Z.strFieldName = txtValue3.Text;
                    break;
                case DNEPropertyType._EPT_VECTOR3D:
                    PropertyNode = new DNSXmlVector3DProperyNode();
                    ((DNSXmlVector3DProperyNode)PropertyNode).X.Value = ntxVector3DX.GetDouble();
                    ((DNSXmlVector3DProperyNode)PropertyNode).X.strFieldName = txtValue2.Text;
                    ((DNSXmlVector3DProperyNode)PropertyNode).Y.Value = ntxVector3DY.GetDouble();
                    ((DNSXmlVector3DProperyNode)PropertyNode).Y.strFieldName = txtValue2.Text;
                    ((DNSXmlVector3DProperyNode)PropertyNode).Z.Value = ntxVector3DZ.GetDouble();
                    ((DNSXmlVector3DProperyNode)PropertyNode).Z.strFieldName = txtValue3.Text;
                    break;
                case DNEPropertyType._EPT_TEXTURE:
                    PropertyNode = new DNSXmlTextureProperyNode();
                    DNSXmlProperyNodeComponent<string> imageFileName  = new DNSXmlProperyNodeComponent<string>();
                    DNSXmlProperyNodeComponent<DNSMcBColor> transparentColor = new DNSXmlProperyNodeComponent<DNSMcBColor>();
                    DNSXmlProperyNodeComponent<bool> isTransparentColorEnabled = new DNSXmlProperyNodeComponent<bool>();
                    DNSXmlProperyNodeComponent<bool> ignoreTransparentMargin = new DNSXmlProperyNodeComponent<bool>();

                    imageFileName.Value = ctrlBrowseTextureFileName.FileName;
                    imageFileName.strFieldName = txtValue1.Text;

                    DNSMcBColor bTextureColor = new DNSMcBColor(picbTextureColor.BackColor.R, picbTextureColor.BackColor.G, picbTextureColor.BackColor.B, (byte)numUDTextureAlphaColor.Value);
                    transparentColor.Value = bTextureColor;
                    transparentColor.strFieldName = txtValue2.Text;

                    isTransparentColorEnabled.Value = chxIsTransparentColorEnabled.Checked;
                    isTransparentColorEnabled.strFieldName = txtValue3.Text;

                    ignoreTransparentMargin.Value = chxIgnoreTransparentMargin.Checked;
                    ignoreTransparentMargin.strFieldName = txtValue4.Text;

                    ((DNSXmlTextureProperyNode)PropertyNode).ImageFileName = imageFileName;
                    ((DNSXmlTextureProperyNode)PropertyNode).TransparentColor = transparentColor;
                    ((DNSXmlTextureProperyNode)PropertyNode).IsTransparentColorEnabled = isTransparentColorEnabled;
                    ((DNSXmlTextureProperyNode)PropertyNode).IgnoreTransparentMargin = ignoreTransparentMargin;
                    break;
                case DNEPropertyType._EPT_FONT:
                    PropertyNode = new DNSXmlFontProperyNode();
                    DNSXmlProperyNodeComponent<int> height = new DNSXmlProperyNodeComponent<int>();
                    DNSXmlProperyNodeComponent<string> faceName = new DNSXmlProperyNodeComponent<string>();
                    DNSXmlProperyNodeComponent<uint> charSet = new DNSXmlProperyNodeComponent<uint>();

                    height.Value = ntxFontHeight.GetInt32();
                    height.strFieldName = txtValue1.Text;

                    faceName.Value = ntxFontFaceName.Text;
                    faceName.strFieldName = txtValue2.Text;

                    charSet.Value = ntxFontCharSet.GetUInt32();
                    charSet.strFieldName = txtValue3.Text;
                    
                    ((DNSXmlFontProperyNode)PropertyNode).Height = height;
                    ((DNSXmlFontProperyNode)PropertyNode).FaceName = faceName;
                    ((DNSXmlFontProperyNode)PropertyNode).CharSet = charSet;
                    break;
                case DNEPropertyType._EPT_CONDITIONALSELECTOR:
                    PropertyNode = new DNSXmlScaleConditionalSelectorProperyNode();
                    DNSXmlProperyNodeComponent<float> minScale = new DNSXmlProperyNodeComponent<float>();
                    DNSXmlProperyNodeComponent<float> maxScale = new DNSXmlProperyNodeComponent<float>();
                    DNSXmlProperyNodeComponent<uint> cancelScaleMode = new DNSXmlProperyNodeComponent<uint>();
                    DNSXmlProperyNodeComponent<uint> cancelScaleModeResult = new DNSXmlProperyNodeComponent<uint>();

                    minScale.Value = ntxScaleConditionalSelectorMin.GetFloat();
                    minScale.strFieldName = txtValue1.Text;

                    maxScale.Value = ntxScaleConditionalSelectorMax.GetFloat();
                    maxScale.strFieldName = txtValue2.Text;

                    cancelScaleMode.Value = GetCancelScaleModeValue();
                    cancelScaleMode.strFieldName = txtValue3.Text;

                    cancelScaleModeResult.Value = GetCancelScaleModeResultValue();
                    cancelScaleModeResult.strFieldName = txtValue4.Text;
                    
                    ((DNSXmlScaleConditionalSelectorProperyNode)PropertyNode).Min = minScale;
                    ((DNSXmlScaleConditionalSelectorProperyNode)PropertyNode).Max = maxScale;
                    ((DNSXmlScaleConditionalSelectorProperyNode)PropertyNode).CancelScaleMode = cancelScaleMode;
                    ((DNSXmlScaleConditionalSelectorProperyNode)PropertyNode).CancelScaleModeResult = cancelScaleModeResult;
                    break;
                case DNEPropertyType._EPT_SUBITEM_ARRAY:
                    PropertyNode = new DNSXmlSubItemsDataProperyNode();
                    ((DNSXmlSubItemsDataProperyNode)PropertyNode).strPropertyName = txtValue1.Text;
                    break;
                case DNEPropertyType._EPT_NUM:
                    break;
                default:
                    this.DialogResult = DialogResult.Cancel;
                    break;                    
            }

            this.Close();
        }

        public object PropertyNode
        {
            get {return m_PropertyNode;}
            set {m_PropertyNode = value;}            
        }

        private uint GetCancelScaleModeValue()
        {
            uint scaleMode = 0;

            scaleMode += (chxCancelScale0.Checked == true) ? MASK0 : 0;
            scaleMode += (chxCancelScale1.Checked == true) ? MASK1 : 0;
            scaleMode += (chxCancelScale2.Checked == true) ? MASK2 : 0;
            scaleMode += (chxCancelScale3.Checked == true) ? MASK3 : 0;
            scaleMode += (chxCancelScale4.Checked == true) ? MASK4 : 0;
            scaleMode += (chxCancelScale5.Checked == true) ? MASK5 : 0;
            scaleMode += (chxCancelScale6.Checked == true) ? MASK6 : 0;
            scaleMode += (chxCancelScale7.Checked == true) ? MASK7 : 0;
            scaleMode += (chxCancelScale8.Checked == true) ? MASK8 : 0;
            scaleMode += (chxCancelScale9.Checked == true) ? MASK9 : 0;

            return scaleMode;
        }

        private uint GetCancelScaleModeResultValue()
        {
            uint scaleModeResult = 0;

            scaleModeResult += (chxCancelScaleResult0.Checked == true) ? MASK0 : 0;
            scaleModeResult += (chxCancelScaleResult1.Checked == true) ? MASK1 : 0;
            scaleModeResult += (chxCancelScaleResult2.Checked == true) ? MASK2 : 0;
            scaleModeResult += (chxCancelScaleResult3.Checked == true) ? MASK3 : 0;
            scaleModeResult += (chxCancelScaleResult4.Checked == true) ? MASK4 : 0;
            scaleModeResult += (chxCancelScaleResult5.Checked == true) ? MASK5 : 0;
            scaleModeResult += (chxCancelScaleResult6.Checked == true) ? MASK6 : 0;
            scaleModeResult += (chxCancelScaleResult7.Checked == true) ? MASK7 : 0;
            scaleModeResult += (chxCancelScaleResult8.Checked == true) ? MASK8 : 0;
            scaleModeResult += (chxCancelScaleResult9.Checked == true) ? MASK9 : 0;


            return scaleModeResult;

        }
    }
}
