using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;
//using System.Linq;
using MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms;
using MCTester.Managers.ObjectWorld;
using UnmanagedWrapper;
using MCTester.Controls;
using MCTester.ObjectWorld.Assit_Forms.PropertyTypeForms;
using System.IO;

namespace MCTester.Controls
{
    public partial class CtrlAmplifiers : UserControl
    {
        //exist 2 hide columns ( 1. names - combo box of keys, 2. id's) now they not used.
        //private int m_dgvCellSelectedIndex = 0;
        //string[] m_AmplifiersKeys;
        IDNMcObject m_mcObject;
        bool m_IsInCellValueChanged = false;

        int m_ColSelect = 0;
        int m_ColKey = 1;
        int m_ColName = 2;
        int m_ColID = 3;
        int m_ColType = 4;
        int m_ColVal = 5;

        int m_CountSelectedRows = 0;

        DNSKeyVariantValue[] m_Amplifiers = null;

        public CtrlAmplifiers()
        {
            InitializeComponent();
            dgvAmplifiers.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgvAmplifiers_EditingControlShowing);

            ((DataGridViewComboBoxColumn)dgvAmplifiers.Columns[m_ColType]).Items.Clear();
            ((DataGridViewComboBoxColumn)dgvAmplifiers.Columns[m_ColType]).Items.AddRange(Enum.GetNames(typeof(DNEPropertyType)));
            /* todo remove int_array and conditional selector from type list*/
            SetNewRow(0);

        }

        void dgvAmplifiers_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvAmplifiers.CurrentCell.ColumnIndex == m_ColType)
            {
                // Check box column
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged += new EventHandler(comboBoxName_SelectedIndexChanged);
            }
            if (dgvAmplifiers.CurrentCell.ColumnIndex == m_ColSelect)
            {
                // Check box column
                CheckBox checkBox = e.Control as CheckBox;
                checkBox.CheckedChanged += new EventHandler(CheckBoxSelectRow_CheckedChanged);
            }
        }

        private void CheckBoxSelectRow_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
                m_CountSelectedRows++;
            else
                m_CountSelectedRows--;

            chxSelectAll.Checked = m_CountSelectedRows == dgvAmplifiers.RowCount;
        }

        private void dgvAmplifiers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 &&
                e.ColumnIndex == m_ColVal &&
                getSelectValue(e.RowIndex))
            {
                OpenPropertyTypeForm(e.RowIndex);
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == m_ColSelect)
            {
                dgvAmplifiers[m_ColSelect, e.RowIndex].Value = !getSelectValue(e.RowIndex);
            }
        }

        private void dgvAmplifiers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!m_IsInCellValueChanged)
            {
                m_IsInCellValueChanged = true;
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == m_ColSelect)
                    {
                        dgvAmplifiers.Rows[e.RowIndex].ReadOnly = !getSelectValue(e.RowIndex);
                    }
                    if (e.ColumnIndex == m_ColName)
                    {
                        if (dgvAmplifiers[m_ColName, e.RowIndex].Value != dgvAmplifiers[m_ColKey, e.RowIndex].Value)
                        {
                            dgvAmplifiers[m_ColKey, e.RowIndex].Value = "";
                        }
                        else
                        {
                            dgvAmplifiers[m_ColKey, e.RowIndex].Value = dgvAmplifiers[m_ColName, e.RowIndex].Value;
                        }
                    }
                    if (e.ColumnIndex == m_ColType)
                    {
                        /*bool isSameType = CheckIfTypeSameTypeVal(e.RowIndex);
                        if(!isSameType)
                        {
                            SetDefualtValueByType(e.RowIndex);
                            SetValueCellView(e.RowIndex, GetPropertyType(e.RowIndex), dgvAmplifiers[m_ColVal, e.RowIndex].Tag);
                        }*/

                    }
                    if (dgvAmplifiers[m_ColName, e.RowIndex].Value != dgvAmplifiers[m_ColKey, e.RowIndex].Value)
                    {
                        dgvAmplifiers[m_ColKey, e.RowIndex].Value = "";
                    }
                    else
                    {
                        dgvAmplifiers[m_ColKey, e.RowIndex].Value = dgvAmplifiers[m_ColName, e.RowIndex].Value;
                    }

                }
                m_IsInCellValueChanged = false;
            }
        }

        private bool CheckIfTypeSameTypeVal(int rowIndex, DNEPropertyType type)
        {
            object val = dgvAmplifiers[m_ColVal, rowIndex].Tag;
            if (val != null)
            {
                if ((type == DNEPropertyType._EPT_ANIMATION && val is DNSMcAnimation)
                    || (type == DNEPropertyType._EPT_ATTENUATION && val is DNSMcAttenuation)
                    || (type == DNEPropertyType._EPT_BCOLOR && val is DNSMcBColor)
                    || (type == DNEPropertyType._EPT_BCOLOR_ARRAY && val is DNSArrayProperty<DNSMcBColor>)
                    || (type == DNEPropertyType._EPT_BOOL && val is bool)
                    || (type == DNEPropertyType._EPT_BYTE && val is byte)
                    || (type == DNEPropertyType._EPT_ENUM && val is uint)
                    || (type == DNEPropertyType._EPT_SBYTE && val is sbyte)
                    || (type == DNEPropertyType._EPT_DOUBLE && val is double)
                    || (type == DNEPropertyType._EPT_FCOLOR && val is DNSMcFColor)
                    || (type == DNEPropertyType._EPT_FLOAT && val is float)
                    || (type == DNEPropertyType._EPT_FONT && val is DNMcFont)
                    || (type == DNEPropertyType._EPT_FVECTOR2D && val is DNSMcFVector2D)
                    || (type == DNEPropertyType._EPT_FVECTOR2D_ARRAY && val is DNSArrayProperty<DNSMcFVector2D>)
                    || (type == DNEPropertyType._EPT_FVECTOR3D && val is DNSMcFVector3D)
                    || (type == DNEPropertyType._EPT_FVECTOR3D_ARRAY && val is DNSArrayProperty<DNSMcFVector3D>)
                    || (type == DNEPropertyType._EPT_INT && val is int)
                    || (type == DNEPropertyType._EPT_MESH && val is IDNMcMesh)
                    || (type == DNEPropertyType._EPT_ROTATION && val is DNSMcRotation)
                    || (type == DNEPropertyType._EPT_STRING && val is DNMcVariantString)
                    || (type == DNEPropertyType._EPT_SUBITEM_ARRAY && val is DNSArrayProperty<DNSMcSubItemData>)
                    || (type == DNEPropertyType._EPT_TEXTURE && val is IDNMcTexture)
                    || (type == DNEPropertyType._EPT_UINT && val is uint)
                    || (type == DNEPropertyType._EPT_UINT_ARRAY && val is DNSArrayProperty<uint>)
                    || (type == DNEPropertyType._EPT_VECTOR2D && val is DNSMcVector2D)
                    || (type == DNEPropertyType._EPT_VECTOR2D_ARRAY && val is DNSArrayProperty<DNSMcVector2D>)
                    || (type == DNEPropertyType._EPT_VECTOR3D && val is DNSMcVector3D)
                    || (type == DNEPropertyType._EPT_VECTOR3D_ARRAY && val is DNSArrayProperty<DNSMcVector3D>))
                    return true;
                else
                    return false;
            }
            return false;
        }
    

        private void SetDefualtValueByType(int rowIndex,  DNEPropertyType type)
        {
            object val = null;
            switch(type)
            {
                case DNEPropertyType._EPT_BOOL:
                    val = false;
                    break;
                case DNEPropertyType._EPT_BYTE:
                    val = new byte();
                    break;
                case DNEPropertyType._EPT_SBYTE:
                    val = new sbyte();
                    break;
                case DNEPropertyType._EPT_INT:
                    val = new int();
                    break;
                case DNEPropertyType._EPT_ENUM:
                case DNEPropertyType._EPT_UINT:
                    val = new uint();
                    break;
                 case DNEPropertyType._EPT_DOUBLE:
                    val = new double();
                    break;
                case DNEPropertyType._EPT_FLOAT:
                    val = new float();
                    break;
                case DNEPropertyType._EPT_ANIMATION:
                    val = new DNSMcAnimation();
                    break;
                case DNEPropertyType._EPT_ATTENUATION:
                    val = new DNSMcAttenuation();
                    break;
                case DNEPropertyType._EPT_BCOLOR:
                    val = new DNSMcBColor();
                    break;
                case DNEPropertyType._EPT_BCOLOR_ARRAY:
                    val = new DNSArrayProperty<DNSMcBColor>();
                    break;
                case DNEPropertyType._EPT_FCOLOR:
                    val = new DNSMcFColor();
                    break;
                case DNEPropertyType._EPT_FONT:
                    val = null;
                    break;
                case DNEPropertyType._EPT_FVECTOR2D:
                    val = new DNSMcFVector2D();
                    break;
                case DNEPropertyType._EPT_FVECTOR2D_ARRAY:
                    val = new DNSArrayProperty<DNSMcFVector2D>();
                    break;
                case DNEPropertyType._EPT_FVECTOR3D:
                    val = new DNSMcFVector3D();
                    break;
                case DNEPropertyType._EPT_FVECTOR3D_ARRAY:
                    val = new DNSArrayProperty<DNSMcFVector3D>();
                    break;
                case DNEPropertyType._EPT_MESH:
                    val = null;
                    break;
                case DNEPropertyType._EPT_ROTATION:
                    val = new DNSMcRotation();
                    break;
                case DNEPropertyType._EPT_STRING:
                    val = new DNMcVariantString();
                    break;
                case DNEPropertyType._EPT_SUBITEM_ARRAY:
                    val = new DNSArrayProperty<DNSMcSubItemData>();
                    break;
                case DNEPropertyType._EPT_TEXTURE:
                    val = null;
                    break;
                case DNEPropertyType._EPT_UINT_ARRAY:
                    val = new DNSArrayProperty<uint>();
                    break;
                case DNEPropertyType._EPT_VECTOR2D:
                    val = new DNSMcVector2D();
                    break;
                case DNEPropertyType._EPT_VECTOR2D_ARRAY:
                    val = new DNSArrayProperty<DNSMcVector2D>();
                    break;
                case DNEPropertyType._EPT_VECTOR3D:
                    val = new DNSMcVector3D();
                    break;
                case DNEPropertyType._EPT_VECTOR3D_ARRAY:
                    val = new DNSArrayProperty<DNSMcVector3D>();
                    break;

            }

            dgvAmplifiers[m_ColVal, rowIndex].Tag = val;
        }

        private bool getSelectValue(int rowIndex)
        {
            return (bool)dgvAmplifiers[m_ColSelect, rowIndex].Value;
        }

        private void UpdateAfterUserChanged(int rowIndex, DNEPropertyType propertyType, object newValue)
        {
            bool isChanged = SetValueCellView(rowIndex, propertyType, newValue);
            dgvAmplifiers[m_ColVal, rowIndex].Tag = newValue;
        }

        private bool SetValueCellView(int currRow, DNEPropertyType cellType, object cellVal)
        {
            bool isChanged = false;
            if (cellVal != null)
            {
                uint id = 0;
                string text = MCTPrivatePropertiesData.GetPropertyValueAsText(null, id, cellType, cellVal);
                if (cellType == DNEPropertyType._EPT_BCOLOR)
                {
                    DNSMcBColor btnColor = (DNSMcBColor)cellVal;
                    dgvAmplifiers[m_ColVal, currRow].Style.BackColor = Color.FromArgb(255, (int)btnColor.r, (int)btnColor.g, (int)btnColor.b);
                }
                dgvAmplifiers[m_ColVal, currRow].ToolTipText = text;
                dgvAmplifiers[m_ColVal, currRow].Value = text;
            }
            else
            {
                dgvAmplifiers[m_ColVal, currRow].Value = "null";
            }
            return isChanged;
        }

        private DNEPropertyType GetPropertyType(int rowIndex)
        {
            return (DNEPropertyType)Enum.Parse(typeof(DNEPropertyType), dgvAmplifiers[m_ColType, rowIndex].Value.ToString());
        }

        private void OpenPropertyTypeForm(int rowIndex)
        {
            uint id = 0;
            /*if (dgvAmplifiers[m_ColID, rowIndex].Value is uint)
                id = (uint)(dgvAmplifiers[m_ColID, rowIndex].Value);
            else if (dgvAmplifiers[m_ColID, rowIndex].Value is string)
            {
                if (!uint.TryParse(dgvAmplifiers[m_ColID, rowIndex].Value.ToString(), out id))
                {
                    MessageBox.Show("Invalid ID");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Invalid ID");
                return;
            }*/

                            if (dgvAmplifiers[m_ColType, rowIndex].Value == null)
            {
                MessageBox.Show("Invalid Type");
                return;
            }

            
            DNEPropertyType type = GetPropertyType(rowIndex);

            object propValue = dgvAmplifiers[m_ColVal, rowIndex].Tag;
            switch (type)
            {
                case DNEPropertyType._EPT_ANIMATION:
                    frmAnimationPropertyType AnimationPropertyTypeForm = new frmAnimationPropertyType(id);
                    if (propValue is DNSMcAnimation)
                        AnimationPropertyTypeForm.AnimationPropertyValue = (DNSMcAnimation)propValue;
                    if (AnimationPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_ANIMATION, AnimationPropertyTypeForm.AnimationPropertyValue);
                    }
                    break;
                case DNEPropertyType._EPT_ATTENUATION:
                    frmAttenuationPropertyType AttenuationPropertyTypeForm = new frmAttenuationPropertyType(id);
                    if (propValue is DNSMcAttenuation)
                        AttenuationPropertyTypeForm.AttenuationPropertyValue = (DNSMcAttenuation)propValue;
                    if (AttenuationPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_ATTENUATION, AttenuationPropertyTypeForm.AttenuationPropertyValue);
                    }
                    break;
                case DNEPropertyType._EPT_BCOLOR:
                    frmColorPropertyType BColorPropertyTypeForm = new frmColorPropertyType(id);
                    if(propValue is DNSMcBColor)
                        BColorPropertyTypeForm.BColor = (DNSMcBColor)propValue;
                    if (BColorPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_BCOLOR, BColorPropertyTypeForm.BColor);
                    }
                    break;
                case DNEPropertyType._EPT_BCOLOR_ARRAY:
                    frmColorArrayPropertyType colorArrayPropertyTypeForm = new frmColorArrayPropertyType(id);
                    if (propValue is DNSArrayProperty<DNSMcBColor>)
                        colorArrayPropertyTypeForm.BColorsPropertyValue = (DNSArrayProperty<DNSMcBColor>)propValue;
                    if (colorArrayPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        if (colorArrayPropertyTypeForm.BColorsPropertyValue.aElements != null)
                        {
                            UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_BCOLOR_ARRAY, colorArrayPropertyTypeForm.BColorsPropertyValue);
                        }
                        else
                            MessageBox.Show("Missing Colors.\nIn order to make changes set parameters again", "Problem in Colors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case DNEPropertyType._EPT_BOOL:
                    frmBoolPropertyType BoolPropertyTypeForm = new frmBoolPropertyType(id); 
                    if (propValue is bool)
                        BoolPropertyTypeForm.IsChecked = (bool)propValue;

                    if (BoolPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_BOOL, BoolPropertyTypeForm.IsChecked);
                    }
                    break;
                case DNEPropertyType._EPT_BYTE:
                    frmNumericPropertyType BytePropertyTypeForm = new frmNumericPropertyType(id, m_mcObject);
                    if (propValue is byte)
                        BytePropertyTypeForm.PropertyByteValue = (byte)propValue;
                    if (BytePropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_BYTE, BytePropertyTypeForm.PropertyByteValue);
                    }
                    break;
                case DNEPropertyType._EPT_SBYTE:
                    frmNumericPropertyType SBytePropertyTypeForm = new frmNumericPropertyType(id, m_mcObject);
                    if (propValue is sbyte)
                        SBytePropertyTypeForm.PropertySByteValue = (sbyte)propValue;
                    if (SBytePropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_SBYTE, SBytePropertyTypeForm.PropertySByteValue);
                    }
                    break;
                /* case DNEPropertyType._EPT_CONDITIONALSELECTOR:
                     frmConditionalSelectorPropertyType ConditionalSelectorPropertyTypeForm = new frmConditionalSelectorPropertyType(id, m_mcObject, (IDNMcConditionalSelector)propValue);
                     if (ConditionalSelectorPropertyTypeForm.ShowDialog() == DialogResult.OK)
                     {
                         UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_CONDITIONALSELECTOR, ConditionalSelectorPropertyTypeForm.PropertySelectorValue);
                     }
                     break;*/
                case DNEPropertyType._EPT_DOUBLE:
                    frmNumericPropertyType DoublePropertyTypeForm = new frmNumericPropertyType(id);
                    if (propValue is double)
                        DoublePropertyTypeForm.PropertyDoubleValue = (double)propValue;
                    if (DoublePropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_DOUBLE, DoublePropertyTypeForm.PropertyDoubleValue);
                    }
                    break;
                case DNEPropertyType._EPT_FCOLOR:
                    frmColorPropertyType FColorPropertyTypeForm = new frmColorPropertyType(id);
                    if (propValue is DNSMcFColor)
                        FColorPropertyTypeForm.FColor = (DNSMcFColor)propValue;
                    if (FColorPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_FCOLOR, FColorPropertyTypeForm.FColor);
                    }
                    break;
              
                case DNEPropertyType._EPT_FONT:
                        frmFontPropertyType FontPropertyTypeForm = new frmFontPropertyType(id);
                    if (propValue is IDNMcFont)
                        FontPropertyTypeForm.McFont = (IDNMcFont)propValue;

                        if (FontPropertyTypeForm.ShowDialog() == DialogResult.OK)
                        {
                            UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_FONT, FontPropertyTypeForm.McFont);
                        }
                    break;
                case DNEPropertyType._EPT_FVECTOR2D:
                    frmFVector2DPropertyType FVector2DPropertyTypeForm = new frmFVector2DPropertyType(id);
                    if(propValue is DNSMcFVector2D)
                        FVector2DPropertyTypeForm.FVector2D = (DNSMcFVector2D)propValue;

                    if (FVector2DPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_FVECTOR2D, FVector2DPropertyTypeForm.FVector2D);
                    }
                    break;
                case DNEPropertyType._EPT_FVECTOR2D_ARRAY:
                    frmVector2DArrayPropertyType frmFVector2DArrayPropertyTypeForm = new frmVector2DArrayPropertyType(id);
                    if (propValue is DNSArrayProperty<DNSMcFVector2D>)
                        frmFVector2DArrayPropertyTypeForm.FVector2DArrayPropertyValue = (DNSArrayProperty<DNSMcFVector2D>)propValue;
                    if (frmFVector2DArrayPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        if (frmFVector2DArrayPropertyTypeForm.FVector2DArrayPropertyValue.aElements != null)
                        {
                            UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_FVECTOR2D_ARRAY, frmFVector2DArrayPropertyTypeForm.FVector2DArrayPropertyValue);
                        }
                        else
                            MessageBox.Show("Missing 2D Vectors.\nIn order to make changes set parameters again", "Problem in Numbers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case DNEPropertyType._EPT_FVECTOR3D:

                    frmFVector3DPropertyType FVector3DPropertyTypeForm = new frmFVector3DPropertyType(id);
                    if (propValue is DNSMcFVector3D)
                        FVector3DPropertyTypeForm.FVector3D = (DNSMcFVector3D)propValue;
                   
                    if (FVector3DPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_FVECTOR3D, FVector3DPropertyTypeForm.FVector3D);
                    }
                    break;
                case DNEPropertyType._EPT_FVECTOR3D_ARRAY:
                    frmVector3DArrayPropertyType frmFVector3DArrayPropertyTypeForm = new frmVector3DArrayPropertyType(id);
                    if (propValue is DNSArrayProperty<DNSMcFVector3D>)
                        frmFVector3DArrayPropertyTypeForm.FVector3DArrayPropertyValue = (DNSArrayProperty<DNSMcFVector3D>)propValue;
                    if (frmFVector3DArrayPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        if (frmFVector3DArrayPropertyTypeForm.FVector3DArrayPropertyValue.aElements != null)
                        {
                            UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_FVECTOR3D_ARRAY, frmFVector3DArrayPropertyTypeForm.FVector3DArrayPropertyValue);
                        }
                        else
                            MessageBox.Show("Missing 3D Vectors.\nIn order to make changes set parameters again", "Problem in Numbers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case DNEPropertyType._EPT_INT:
                    frmNumericPropertyType IntPropertyTypeForm = new frmNumericPropertyType(id);
                    if(propValue is int)
                        IntPropertyTypeForm.PropertyIntValue = (int)propValue;
                    if (IntPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_INT, IntPropertyTypeForm.PropertyIntValue);
                    }
                    break;
                case DNEPropertyType._EPT_MESH:
                    frmMeshPropertyType MeshPropertyTypeForm = new frmMeshPropertyType(id);
                    if (propValue is IDNMcMesh)
                        MeshPropertyTypeForm.SelectedMesh = (IDNMcMesh)propValue;
                    if (MeshPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_MESH, MeshPropertyTypeForm.SelectedMesh);
                    }
                    break;
                case DNEPropertyType._EPT_ROTATION:
                    frmRotationPropertyType RotationPropertyTypeForm = new frmRotationPropertyType(id);
                    if (propValue is DNSMcRotation)
                        RotationPropertyTypeForm.Rotation = (DNSMcRotation)propValue;
                    if (RotationPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_ROTATION, RotationPropertyTypeForm.Rotation);
                    }
                    break;
                case DNEPropertyType._EPT_STRING:
                    frmStringPropertyType StringPropertyTypeForm = new frmStringPropertyType(id);
                    if (propValue is DNMcVariantString)
                        StringPropertyTypeForm.StringPropertyValue = (DNMcVariantString)propValue;

                    if (StringPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_STRING, StringPropertyTypeForm.StringPropertyValue);
                    }
                    break;
                case DNEPropertyType._EPT_SUBITEM_ARRAY:
                    frmSubItemsDataPropertyType SubItemsDataPropertyTypeForm = new frmSubItemsDataPropertyType(id);
                    if (propValue is DNSArrayProperty<DNSMcSubItemData>)
                        SubItemsDataPropertyTypeForm.SubItemsDataPropertyValue = (DNSArrayProperty<DNSMcSubItemData>)propValue;

                    if (SubItemsDataPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        if (SubItemsDataPropertyTypeForm.SubItemsDataPropertyValue.aElements != null)
                        {
                            UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_SUBITEM_ARRAY, SubItemsDataPropertyTypeForm.SubItemsDataPropertyValue);
                        }
                        else
                            MessageBox.Show("Id's number different from points start index number.\nIn order to make changes set parameters again", "Problem in Sub Items Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                
              /*  case DNEPropertyType._EPT_INT_ARRAY:
                    try
                    {
                        m_CurrScheme.SetArrayProperty(propID, DNEPropertyType._EPT_INT_ARRAY, (DNSArrayProperty<int>)propVal);
                    }
                    catch (MapCoreException McEx) {
                        Utilities.ShowErrorMessage("SetArrayPropertyDefault", McEx); }
                    break;
               */
                case DNEPropertyType._EPT_TEXTURE:
                    frmTexturePropertyType TexturePropertyTypeForm = new frmTexturePropertyType(id);

                    if (propValue is IDNMcTexture)
                        TexturePropertyTypeForm.SelectedTexture = (IDNMcTexture)propValue;
                   
                    if (TexturePropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_TEXTURE, TexturePropertyTypeForm.SelectedTexture);
                    }
                    break;
                case DNEPropertyType._EPT_UINT:

                    frmNumericPropertyType UIntPropertyTypeForm = new frmNumericPropertyType(id, m_mcObject);
                    if(propValue is uint)
                        UIntPropertyTypeForm.PropertyUintValue = (uint)propValue;
                    if (UIntPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_UINT, UIntPropertyTypeForm.PropertyUintValue);
                    }
                    break;
                case DNEPropertyType._EPT_ENUM:

                    frmNumericPropertyType EnumPropertyTypeForm = new frmNumericPropertyType(id, m_mcObject);
                    if(propValue is uint)
                        EnumPropertyTypeForm.PropertyUintValue = (uint)propValue;
                    if (EnumPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_ENUM, EnumPropertyTypeForm.PropertyUintValue);
                    }
                    break;
                case DNEPropertyType._EPT_UINT_ARRAY:
                    frmNumericArrayPropertyType numberArrayPropertyTypeForm = new frmNumericArrayPropertyType(id);
                    if (propValue is DNSArrayProperty<uint>)
                        numberArrayPropertyTypeForm.UIntArrayPropertyValue = (DNSArrayProperty<uint>)propValue;

                    if (numberArrayPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        if (numberArrayPropertyTypeForm.UIntArrayPropertyValue.aElements != null)
                        {
                            UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_UINT_ARRAY, numberArrayPropertyTypeForm.UIntArrayPropertyValue);
                        }
                        else
                            MessageBox.Show("Missing Numbers.\nIn order to make changes set parameters again", "Problem in Numbers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case DNEPropertyType._EPT_VECTOR2D:
                    frmVector2DPropertyType Vector2DPropertyTypeForm = new frmVector2DPropertyType(id);
                    if(propValue is DNSMcVector2D)
                        Vector2DPropertyTypeForm.SetVector2D((DNSMcVector2D)propValue);
                  
                    if (Vector2DPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_VECTOR2D, Vector2DPropertyTypeForm.GetVector2D());
                    }
                    break;
                case DNEPropertyType._EPT_VECTOR2D_ARRAY:
                    frmVector2DArrayPropertyType frmVector2DArrayPropertyTypeForm = new frmVector2DArrayPropertyType(id);
                    if (propValue is DNSArrayProperty<DNSMcVector2D>)
                        frmVector2DArrayPropertyTypeForm.Vector2DArrayPropertyValue = (DNSArrayProperty<DNSMcVector2D>)propValue;
                    if (frmVector2DArrayPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        if (frmVector2DArrayPropertyTypeForm.Vector2DArrayPropertyValue.aElements != null)
                        {
                            UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_VECTOR2D_ARRAY, frmVector2DArrayPropertyTypeForm.Vector2DArrayPropertyValue);
                        }
                        else
                            MessageBox.Show("Missing 2D Vectors.\nIn order to make changes set parameters again", "Problem in Numbers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case DNEPropertyType._EPT_VECTOR3D:

                    frmVector3DPropertyType Vector3DPropertyTypeForm = new frmVector3DPropertyType(id);
                    if (propValue is DNSMcVector3D)
                        Vector3DPropertyTypeForm.SetVector3D( (DNSMcVector3D)propValue); 
                    if (Vector3DPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_VECTOR3D, Vector3DPropertyTypeForm.GetVector3D());
                    }
                    break;
                case DNEPropertyType._EPT_VECTOR3D_ARRAY:
                    frmVector3DArrayPropertyType frmVector3DArrayPropertyTypeForm = new frmVector3DArrayPropertyType(id);
                    if(propValue is DNSArrayProperty<DNSMcVector3D>)
                        frmVector3DArrayPropertyTypeForm.Vector3DArrayPropertyValue = (DNSArrayProperty<DNSMcVector3D>)propValue;

                    if (frmVector3DArrayPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        if (frmVector3DArrayPropertyTypeForm.Vector3DArrayPropertyValue.aElements != null)
                        {
                            UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_VECTOR3D_ARRAY, frmVector3DArrayPropertyTypeForm.Vector3DArrayPropertyValue);
                        }
                        else
                            MessageBox.Show("Missing 3D Vectors.\nIn order to make changes set parameters again", "Problem in Numbers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case DNEPropertyType._EPT_FLOAT:
                   
                    frmNumericPropertyType FloatPropertyTypeForm = new frmNumericPropertyType(id);
                     if(propValue is float)
                        FloatPropertyTypeForm.PropertyFloatValue = (float)propValue;
                    if (FloatPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_FLOAT, FloatPropertyTypeForm.PropertyFloatValue);
                    }
                    break;
            }
        }

        void comboBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  ((ComboBox)sender).SelectedIndexChanged -= new EventHandler(comboBoxName_SelectedIndexChanged);

            /* m_dgvCellSelectedIndex = ((ComboBox)sender).SelectedIndex;
             dgvAmplifiers[m_ColKey, dgvAmplifiers.CurrentCell.RowIndex].Tag = m_AmplifiersKeys[m_dgvCellSelectedIndex];
             dgvAmplifiers[m_ColName, dgvAmplifiers.CurrentCell.RowIndex].Value = m_AmplifiersKeys[m_dgvCellSelectedIndex];*/


            int rowIndex = dgvAmplifiers.CurrentCell.RowIndex;
            string strType = ((ComboBox)sender).SelectedItem.ToString();
            DNEPropertyType type = (DNEPropertyType)Enum.Parse(typeof(DNEPropertyType), strType);

            bool isSameType = CheckIfTypeSameTypeVal(rowIndex, type);
            if (!isSameType)
            {
                SetDefualtValueByType(rowIndex, type);
                SetValueCellView(dgvAmplifiers.CurrentCell.RowIndex, type, dgvAmplifiers[m_ColVal, rowIndex].Tag);
            }
        }

        public void ResetAmplifiers()
        {
            m_mcObject = null;
            m_Amplifiers = null;

            dgvAmplifiers.Rows.Clear();
        }

        public void SetAmplifiers(IDNMcObject mcObject, DNSKeyVariantValue[] Amplifiers)
        {
            m_mcObject = mcObject;
            m_Amplifiers = Amplifiers;

            dgvAmplifiers.Rows.Clear();
            
            if (Amplifiers.Length > 0)
            {
                dgvAmplifiers.Rows.Add(Amplifiers.Length);
                for (int i = 0; i < Amplifiers.Length; i++)
                {
                    dgvAmplifiers[m_ColSelect, i].Value = true;
                    dgvAmplifiers[m_ColKey, i].Value = Amplifiers[i].strKey;
                    dgvAmplifiers[m_ColName, i].Value = Amplifiers[i].strKey;
                    dgvAmplifiers[m_ColID, i].Value = Amplifiers[i].Value.uID;
                    dgvAmplifiers[m_ColType, i].Value = Amplifiers[i].Value.eType.ToString();
                    dgvAmplifiers[m_ColVal, i].Tag = Amplifiers[i].Value.Value;
                    SetValueCellView(i, Amplifiers[i].Value.eType, Amplifiers[i].Value.Value);

                    dgvAmplifiers.Rows[i].ReadOnly = false;
                }
                SetNewRow(Amplifiers.Length);
            }
        }

        private void SetNewRow(int rowIndex)
        {
            dgvAmplifiers[m_ColSelect, rowIndex].Value = false;
            dgvAmplifiers.Rows[rowIndex].ReadOnly = true;
        }

        private void dgvAmplifiers_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rec = dgvAmplifiers.GetCellDisplayRectangle(0, 0, false);
            if (rec.X != 0 && rec.Y != 0)
            {
                chxSelectAll.Location = new Point(
                dgvAmplifiers.Location.X + rec.Left + (rec.Right - rec.Left) / 2 - chxSelectAll.AccessibilityObject.Bounds.Width / 2,
                dgvAmplifiers.Location.Y + (rec.Bottom - rec.Top) / 2 - chxSelectAll.AccessibilityObject.Bounds.Height / 2);
            }
        }

        private void chxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chxSelectAll.Checked;
            for (int i = 0; i < dgvAmplifiers.Rows.Count; i++)
            {
                dgvAmplifiers[m_ColSelect, i].Value = isChecked;
            }
        }

        public DNSKeyVariantValue[] GetAmplifiers()
        {
            List<DNSKeyVariantValue> lstAmplifiers = new List<DNSKeyVariantValue>();
            for (int i = 0; i < dgvAmplifiers.Rows.Count; i++)
            {
                if (getSelectValue(i))
                {
                    DNSKeyVariantValue keyVariantValue = new DNSKeyVariantValue();
                    if (dgvAmplifiers[m_ColName, i].Value != null && dgvAmplifiers[m_ColType, i].Value != null)
                    {
                        keyVariantValue.strKey = dgvAmplifiers[m_ColName, i].Value.ToString();

                        DNSVariantProperty variantProperty = new DNSVariantProperty();
                        uint id;
                        object objId = dgvAmplifiers[m_ColID, i].Value;
                        if (objId != null && objId.ToString() != "")
                        {
                            uint.TryParse(objId.ToString(), out id);
                            variantProperty.uID = id;
                        }

                        variantProperty.eType = GetPropertyType(i);
                        variantProperty.Value = dgvAmplifiers[m_ColVal, i].Tag;

                        keyVariantValue.Value = variantProperty;

                        lstAmplifiers.Add(keyVariantValue);
                    }
                }
            }

            return lstAmplifiers.ToArray();
        }

        private void dgvAmplifiers_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
       
        private void dgvAmplifiers_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            SetNewRow(e.RowIndex);
            
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(ofd.FileName);
                char[] delimeters = new char[1];
                delimeters[0] = ',';
                string[] readerData = reader.ReadLine().Split(delimeters);
                reader.Close();

                double result = 0;
                int currColumn = 0;
                int currRow = 0;
                int numColumns = 3;

                dgvAmplifiers.RowCount = (int)readerData.Length / numColumns + 1;

                for (int i = 0; i < readerData.Length; i++)
                {
                    bool IsParseSucc = double.TryParse(readerData[i], out result);
                    if (IsParseSucc != true)
                    {
                        MessageBox.Show("Import amplifiers from file failed.\nThe data '" + readerData[i].ToString() + "' located in cell: " + i.ToString() + " is invalid",
                                            "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        dgvAmplifiers.Rows.Clear();
                        dgvAmplifiers.RowCount = 1;
                        return;
                    }
                    else
                    {
                        dgvAmplifiers[currColumn, currRow].Value = result;
                        currColumn++;

                        if (currColumn >= numColumns)
                        {
                            currColumn = 0;
                            currRow++;
                        }
                    }
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV File (*.csv) | *.csv";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter stw = new StreamWriter(sfd.FileName);
                string amplifiers = "";

                for (int i = 0; i < dgvAmplifiers.Rows.Count - 1; i++)
                {
                    amplifiers += dgvAmplifiers[m_ColName, i].Value.ToString() + "," +
                                        GetPropertyType(i).ToString() + "," +
                                        dgvAmplifiers[m_ColVal, i].Value.ToString() + "," ;
                }

                string exportData = amplifiers.Remove(amplifiers.Length - 1);
                stw.Write(exportData);

                stw.Close();
            }
        }
    }
}
