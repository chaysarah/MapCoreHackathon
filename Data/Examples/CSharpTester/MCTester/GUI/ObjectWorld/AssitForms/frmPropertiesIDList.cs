using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using System.Reflection;
using MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms;
using MCTester.ObjectWorld.Assit_Forms.PropertyTypeForms;
using MCTester.Managers;
using MCTester.ObjectWorld.Assit_Forms;
using MapCore.Common;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers.MapWorld;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class frmPropertiesIDList : Form
    {
        private IDNMcObjectScheme m_CurrScheme;
        private object m_SenderObj;
        private DNSPropertyNameIDType[] m_ArrPropertyID;
        private DNSPropertyNameID[] m_ArrPropertyNames;
        private bool bIsCloseWithoutSave;
        private List<DNSVariantProperty> listPropertiesValues;
        private bool m_LoadData = false;

        public bool IsCloseWithoutSave
        {
            get { return bIsCloseWithoutSave; }
            set { bIsCloseWithoutSave = value; }
        }

        public frmPropertiesIDList(object obj)
        {
            InitializeComponent();
            listPropertiesValues = new List<DNSVariantProperty>();
            m_SenderObj = obj;
            IdentifySenderType();
        }

        private void IdentifySenderType()
        {
            string text = "Private Properties Of ";
            string textOf = "";
            if (m_SenderObj is IDNMcObjectScheme)
            {
                //Case scheme type property table
                m_CurrScheme = (IDNMcObjectScheme)m_SenderObj;
                LoadSchemePropertyTable();
                textOf = "ObjectScheme";
            }
            else if (m_SenderObj is IDNMcObject)
            {
                //Case object type property table
                m_CurrScheme = ((IDNMcObject)m_SenderObj).GetScheme();
                LoadObjectPropertyTable();
                textOf = "Object";
            }
            else
            {
                //Case item type property table
                m_CurrScheme = ((IDNMcObjectSchemeNode)m_SenderObj).GetScheme();
                LoadSchemePropertyTable();

                if (m_SenderObj is IDNMcObjectLocation)
                    textOf = "Location";
                else
                    textOf = "Item";
            
            }
            this.Text = text + textOf;
            int rowIndex = 0;
            
            // check map core functions (The above functions haven't UI).
            foreach (DNSPropertyNameIDType sPropertyId in m_ArrPropertyID)
            {
                try
                {
                    uint propertyId = sPropertyId.uID;
                    DNEPropertyType schemePropertyTypeById = m_CurrScheme.GetPropertyType(sPropertyId.uID);
                   
                    object gridPropertyValue = dgvPropertyList[3, rowIndex].Tag;

                    if (m_SenderObj is IDNMcObject)
                    {
                        DNEPropertyType objectPropertyTypeById = ((IDNMcObject)m_SenderObj).GetPropertyType(sPropertyId.uID);
                        if (objectPropertyTypeById != sPropertyId.eType)
                            MessageBox.Show("Property type by id mismatch from object and from 'GetPropertyType' of id " + sPropertyId.uID + ":" + Environment.NewLine +  " from object type is = " + objectPropertyTypeById.ToString() + " , from scheme = " + schemePropertyTypeById, "IDNMcObject.GetPropertyType");

                        if (sPropertyId.strName != null && sPropertyId.strName != "")
                        {
                            DNEPropertyType objectPropertyTypeByName = ((IDNMcObject)m_SenderObj).GetPropertyType(sPropertyId.strName);
                            if (objectPropertyTypeByName != objectPropertyTypeById)
                                MessageBox.Show("Property type by id and by name not equal. property id " + sPropertyId.uID + ":" + Environment.NewLine + " from object type by id is = " + objectPropertyTypeById.ToString() + " , object type (GetPropertyType) by name is= " + objectPropertyTypeByName, "IDNMcObject.GetPropertyType");

                            bool isDefaultById = ((IDNMcObject)m_SenderObj).IsPropertyDefault(sPropertyId.uID);
                            bool isDefaultByName = ((IDNMcObject)m_SenderObj).IsPropertyDefault(sPropertyId.strName);
                            if(isDefaultById != isDefaultByName)
                            {
                                MessageBox.Show("'IsPropertyDefault' by id and by name not equal. property id " + sPropertyId.uID + ":" + Environment.NewLine + " is default by id = " + (isDefaultById ? "True" : "False") + " , is default by name = " + (isDefaultByName ? "True" : "False"), "IDNMcObject.GetPropertyType");
                            }
                        }

                        DNSVariantProperty objectVariantPropertyById = ((IDNMcObject)m_SenderObj).GetProperty(sPropertyId.uID);
                        if (sPropertyId.strName != null && sPropertyId.strName != "")
                        {
                            DNSVariantProperty objectVariantPropertyByName = ((IDNMcObject)m_SenderObj).GetProperty(sPropertyId.strName);
                            if (objectVariantPropertyById.uID != objectVariantPropertyByName.uID ||
                                objectVariantPropertyById.eType != objectVariantPropertyByName.eType ||
                                ( MCTPrivatePropertiesData.IsExistDifferentValues(objectVariantPropertyByName.eType, gridPropertyValue, objectVariantPropertyByName.Value, m_CurrScheme, objectVariantPropertyByName.uID)))
                                MessageBox.Show("IDNMcObject.GetProperty (DNSVariantProperty)  by id and by name not equal, Property Id " + sPropertyId.uID + "Property Name " + sPropertyId.strName, "GetProperty");
                        }

                        if (objectVariantPropertyById.eType != sPropertyId.eType)
                            MessageBox.Show("Property type mismatch of id " + sPropertyId.uID + ":" + Environment.NewLine + " from object type from id is = " + sPropertyId.eType.ToString() + " , object type is = " + sPropertyId.eType, "IDNMcObject.GetProperty");
                        if (MCTPrivatePropertiesData.IsExistDifferentValues(schemePropertyTypeById, gridPropertyValue, objectVariantPropertyById.Value, m_CurrScheme, sPropertyId.uID))
                            MessageBox.Show("Property value mismatch of id " + sPropertyId.uID, "IDNMcObject.GetProperty");
                       
                    }
                    else
                    {

                        if (schemePropertyTypeById != sPropertyId.eType)
                            MessageBox.Show("Property type mismatch of id " + sPropertyId.uID + ":" + Environment.NewLine + " from scheme type from id is = " + sPropertyId.eType.ToString() + " , scheme (GetPropertyType) type by id is = " + sPropertyId.eType, "IDNMcObjectScheme.GetPropertyType");
                        if (sPropertyId.strName != null && sPropertyId.strName != "")
                        {
                            DNEPropertyType schemePropertyTypeByName = m_CurrScheme.GetPropertyType(sPropertyId.strName);
                            if (schemePropertyTypeByName != schemePropertyTypeById)
                                MessageBox.Show("Property type by id and by name not equal.  property id " + sPropertyId.uID + ":" + Environment.NewLine + " from object type by id is = " + schemePropertyTypeById.ToString() + " , object type (GetPropertyType) by name is= " + schemePropertyTypeByName, "IDNMcObjectScheme.GetPropertyType");
                        }
                        DNSVariantProperty schemeVariantPropertyById = m_CurrScheme.GetPropertyDefault(sPropertyId.uID);

                        if (sPropertyId.strName != null && sPropertyId.strName != "")
                        {
                            DNSVariantProperty schemeVariantPropertyByName = m_CurrScheme.GetPropertyDefault(sPropertyId.strName);

                            if (schemeVariantPropertyById.uID != schemeVariantPropertyByName.uID ||
                                schemeVariantPropertyById.eType != schemeVariantPropertyByName.eType ||
                                ( MCTPrivatePropertiesData.IsExistDifferentValues(schemePropertyTypeById, gridPropertyValue, schemeVariantPropertyByName.Value, m_CurrScheme, sPropertyId.uID)))
                                MessageBox.Show("IDNMcObjectScheme.GetPropertyDefault by id and by name not equal, Property Id " + sPropertyId.strName + "", "Property Name " + sPropertyId.strName);
                        }

                        if (schemeVariantPropertyById.eType != sPropertyId.eType)
                            MessageBox.Show("Property type mismatch of id " + sPropertyId.uID + ":" + Environment.NewLine + " from object type from id is = " + sPropertyId.eType.ToString() + " , scheme type (GetPropertyDefault) is = " + schemeVariantPropertyById.eType, "IDNMcObjectScheme.GetPropertyDefault");

                        if (MCTPrivatePropertiesData.IsExistDifferentValues(schemePropertyTypeById, gridPropertyValue, schemeVariantPropertyById.Value, m_CurrScheme, sPropertyId.uID))
                            MessageBox.Show("Property value mismatch of id " + sPropertyId.uID, "IDNMcObjectScheme.GetPropertyDefault");

                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Get Property Functions", McEx);
                }
                rowIndex++;
            }

        }

        private void AddPropertyToList(uint propID, DNEPropertyType propType, object propVal)
        {
            DNSVariantProperty variantProperty = new DNSVariantProperty();
            variantProperty.uID = propID;
            variantProperty.eType = propType;
            variantProperty.Value = propVal;

            listPropertiesValues.Add(variantProperty);

        }

        private void LoadSchemePropertyTable()
        {
            btnSetEachChangedNames.Enabled = true;

            dgvPropertyList.Columns[4].Visible = false;   // is default column
            dgvPropertyList.Columns[8].Visible = false;   // to reset column
            //btnUpdatePropertiesAndLocationPoints.Enabled = false;
            pnlUpdateProperties.Enabled = false;
            pnlObjects.Enabled = false;

            m_ArrPropertyID = m_CurrScheme.GetProperties();

            if (m_ArrPropertyID.Length == 0)
                return;
            else
            {
                dgvPropertyList.RowCount = m_ArrPropertyID.Length;
                dgvPropertyList.Columns[4].ReadOnly = true;

                uint propID;
                DNEPropertyType propType;
                object propVal;

                for (int row = 0; row < m_ArrPropertyID.Length; row++)
                {
                    propID = m_ArrPropertyID[row].uID;
                    propType = m_ArrPropertyID[row].eType;
                    propVal = GetDefaultPropertiesMethods(propID, propType);

                    AddPropertyToList(propID, propType, propVal);

                    dgvPropertyList[0, row].ToolTipText = "Click to show properties using this id";
                    dgvPropertyList[0, row].Value = propID;
                    dgvPropertyList[1, row].Value = propType;

                    try
                    {
                        dgvPropertyList[2, row].Value = m_CurrScheme.GetPropertyNameByID(propID).ToString();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetPropertyNameByID", McEx);
                    }

                    dgvPropertyList[3, row].Tag = propVal;
                    SetValueCellView(row, propType, propVal);
                    dgvPropertyList[4, row].Value = true;
                    dgvPropertyList[7, row].Value = false;
                }
            }
            try
            {
                DNSVariantProperty[] variantProperties = m_CurrScheme.GetPropertyDefaults();
                if (variantProperties.Length != m_ArrPropertyID.Length)
                    MessageBox.Show("IDNMcObjectScheme.GetPropertyDefaults", "Invalid values");
                else
                {
                    List<DNSVariantProperty> listVariantProperties = new List<DNSVariantProperty>();
                    listVariantProperties.AddRange(variantProperties);

                    for (int i = 0; i < listPropertiesValues.Count; i++)
                    {
                        DNSVariantProperty variantProperty1 = listVariantProperties[i];
                        DNSVariantProperty variantProperty2 = listPropertiesValues[i];
                        if(variantProperty1.uID != variantProperty2.uID || 
                           variantProperty1.eType != variantProperty2.eType ||
                           MCTPrivatePropertiesData.IsExistDifferentValues(variantProperty1.eType ,variantProperty1.Value ,variantProperty2.Value, m_CurrScheme, variantProperty1.uID))
                        {
                            MessageBox.Show("IDNMcObjectScheme.GetPropertyDefaults", "Invalid values");
                            break;
                        }
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetPropertyNameByID", McEx);
            }

        }

        //Display the table that open from an Object form
        private void LoadObjectPropertyTable(List<DNSVariantProperty> lstVariantProperties = null)
        {
            m_LoadData = true;
            btnSetEachChangedNames.Enabled = false;

            btnSetEachChangedPropertyAsVarient.Enabled = true;
            btnSetAllChangedPropertiesAsVarient.Enabled = true;
            // btnUpdatePropertiesAndLocationPoints.Enabled = true;
            pnlUpdateProperties.Enabled = true;
            pnlObjects.Enabled = true;
            gbPropertiesNames.Enabled = false;
            dgvPropertyList.Columns[4].Visible = true;
            //btnSetAllChangedPropertiesAsName.Enabled = false;
            this.Size = new Size(1000, this.Height);
            m_ArrPropertyID = m_CurrScheme.GetProperties();
            m_ArrPropertyNames = m_CurrScheme.GetPropertyNames();

            if (m_ArrPropertyID.Length == 0)
                return;
            else
            {
                dgvPropertyList.RowCount = m_ArrPropertyID.Length;
                dgvPropertyList.ReadOnly = false;

                uint propID;
                DNEPropertyType propType;
                object propVal;

                for (int row = 0; row < m_ArrPropertyID.Length; row++)
                {
                    dgvPropertyList[2, row].ReadOnly = true;

                    if (lstVariantProperties == null)
                    {
                        propID = m_ArrPropertyID[row].uID;
                        propType = m_ArrPropertyID[row].eType;
                        propVal = GetPropertiesMethods(propID, propType);
                        
                    }
                    else
                    {
                        DNSVariantProperty variantProperty = lstVariantProperties.Find(x => x.uID == m_ArrPropertyID[row].uID);
                        propID = variantProperty.uID;
                        propType = variantProperty.eType;
                        propVal = variantProperty.Value;
                    }
                    

                    dgvPropertyList[0, row].ToolTipText = "Get Nodes";
                    dgvPropertyList[0, row].Value = propID;
                    dgvPropertyList[1, row].Value = propType;

                    try
                    {
                        dgvPropertyList[2, row].Value = m_CurrScheme.GetPropertyNameByID(propID).ToString();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetPropertyNameByID", McEx);
                    }

                    dgvPropertyList[3, row].Tag = propVal;
                    SetValueCellView(row, propType, propVal);
                    dgvPropertyList[5, row].Value = true;
                    dgvPropertyList[7, row].Value = false;
                    dgvPropertyList[4, row].Value = false;
                    try
                    {
                        if (((IDNMcObject)m_SenderObj).IsPropertyDefault(propID) == true)
                            dgvPropertyList[4, row].Value = true;
                        //else
                        //    dgvPropertyList[4, row].Value = false;
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("IsPropertyDefault", McEx);
                    }
                }
            }

            m_LoadData = false; 
        }

        private object GetDefaultPropertiesMethods(uint propID, DNEPropertyType propType)
        {
            object propVal = null;
            if (propType == DNEPropertyType._EPT_SUBITEM_ARRAY)
            {
                propVal = m_CurrScheme.GetArrayPropertyDefault<DNSMcSubItemData>(propID, DNEPropertyType._EPT_SUBITEM_ARRAY);
            }
            else if (propType == DNEPropertyType._EPT_UINT_ARRAY)
            {
                propVal = m_CurrScheme.GetArrayPropertyDefault<uint>(propID, DNEPropertyType._EPT_UINT_ARRAY);
            }
            else if (propType == DNEPropertyType._EPT_INT_ARRAY)
            {
                propVal = m_CurrScheme.GetArrayPropertyDefault<int>(propID, DNEPropertyType._EPT_INT_ARRAY);
            }
            else if (propType == DNEPropertyType._EPT_FVECTOR2D_ARRAY)
            {
                propVal = m_CurrScheme.GetArrayPropertyDefault<DNSMcFVector2D>(propID, DNEPropertyType._EPT_FVECTOR2D_ARRAY);
            }
            else if (propType == DNEPropertyType._EPT_VECTOR2D_ARRAY)
            {
                propVal = m_CurrScheme.GetArrayPropertyDefault<DNSMcVector2D>(propID, DNEPropertyType._EPT_VECTOR2D_ARRAY);
            }
            else if (propType == DNEPropertyType._EPT_FVECTOR3D_ARRAY)
            {
                propVal = m_CurrScheme.GetArrayPropertyDefault<DNSMcFVector3D>(propID, DNEPropertyType._EPT_FVECTOR3D_ARRAY);
            }
            else if (propType == DNEPropertyType._EPT_VECTOR3D_ARRAY)
            {
                propVal = m_CurrScheme.GetArrayPropertyDefault<DNSMcVector3D>(propID, DNEPropertyType._EPT_VECTOR3D_ARRAY);
            }
            else if (propType == DNEPropertyType._EPT_BCOLOR_ARRAY)
            {
                propVal = m_CurrScheme.GetArrayPropertyDefault<DNSMcBColor>(propID, DNEPropertyType._EPT_BCOLOR_ARRAY);
            }
            else
            {
                MethodInfo[] MiArray = m_CurrScheme.GetType().GetMethods();
                string propName;
                foreach (MethodInfo mi in MiArray)
                {
                    propName = propType.ToString();
                    propName = propName.ToLower();
                    propName = propName.Replace("_ept_", ""); //intentionally small letters

                    if (mi.Name.ToLower().Contains("get" + propName + "propertydefault"))
                    {
                        //this is the function we want to invoke
                        object[] paramsArr = new object[1];
                        paramsArr[0] = new DNSPropertyNameID(propID);
                        propVal = mi.Invoke(m_CurrScheme, paramsArr);
                        break;
                    }
                }
            }

            return propVal;
        }

        private object GetPropertiesMethods(uint propID, DNEPropertyType propType)
        {
            IDNMcObject currObject = (IDNMcObject)m_SenderObj;
            object propVal = null;

            if (propType == DNEPropertyType._EPT_SUBITEM_ARRAY)
            {
                propVal = currObject.GetArrayProperty<DNSMcSubItemData>(propID, DNEPropertyType._EPT_SUBITEM_ARRAY);
            }
            else if (propType == DNEPropertyType._EPT_UINT_ARRAY)
            {
                propVal = currObject.GetArrayProperty<uint>(propID, DNEPropertyType._EPT_UINT_ARRAY);
            }
            else if (propType == DNEPropertyType._EPT_INT_ARRAY)
            {
                propVal = currObject.GetArrayProperty<int>(propID, DNEPropertyType._EPT_INT_ARRAY);
            }
            else if (propType == DNEPropertyType._EPT_FVECTOR2D_ARRAY)
            {
                propVal = currObject.GetArrayProperty<DNSMcFVector2D>(propID, DNEPropertyType._EPT_FVECTOR2D_ARRAY);
            }
            else if (propType == DNEPropertyType._EPT_VECTOR2D_ARRAY)
            {
                propVal = currObject.GetArrayProperty<DNSMcVector2D>(propID, DNEPropertyType._EPT_VECTOR2D_ARRAY);
            }
            else if (propType == DNEPropertyType._EPT_FVECTOR3D_ARRAY)
            {
                propVal = currObject.GetArrayProperty<DNSMcFVector3D>(propID, DNEPropertyType._EPT_FVECTOR3D_ARRAY);
            }
            else if (propType == DNEPropertyType._EPT_VECTOR3D_ARRAY)
            {
                propVal = currObject.GetArrayProperty<DNSMcVector3D>(propID, DNEPropertyType._EPT_VECTOR3D_ARRAY);
            }
            else if (propType == DNEPropertyType._EPT_BCOLOR_ARRAY)
            {
                propVal = currObject.GetArrayProperty<DNSMcBColor>(propID, DNEPropertyType._EPT_BCOLOR_ARRAY);
            }
            else
            {
                MethodInfo[] MiArray = currObject.GetType().GetMethods();
                string propName;
                foreach (MethodInfo mi in MiArray)
                {
                    propName = propType.ToString();
                    propName = propName.ToLower();
                    propName = propName.Replace("_ept_", ""); //intentionally small letters

                    if (mi.Name.ToLower().Contains("get" + propName + "property"))
                    {
                        //this is the function we want to invoke
                        object[] paramsArr = new object[1];
                        paramsArr[0] = new DNSPropertyNameID(propID);
                        propVal = mi.Invoke(currObject, paramsArr);
                        break;
                    }
                }
            }
            return propVal;
        }

        private bool SetValueCellView(int currRow, DNEPropertyType cellType, object cellVal)
        {
            bool isChanged = false;
            if (cellVal != null)
            {
                uint id = UInt32.Parse(dgvPropertyList[0, currRow].Value.ToString());
                string text = MCTPrivatePropertiesData.GetPropertyValueAsText(m_SenderObj, id, cellType, cellVal);
                if (cellType == DNEPropertyType._EPT_BCOLOR)
                {
                    DNSMcBColor btnColor = (DNSMcBColor)cellVal;
                    dgvPropertyList[3, currRow].Style.BackColor = Color.FromArgb(255, (int)btnColor.r, (int)btnColor.g, (int)btnColor.b);
                }

                
                if (dgvPropertyList[6, currRow].Value != null)
                {
                    string oldValue = dgvPropertyList[6, currRow].Value.ToString();
                    if (oldValue != text)
                        isChanged = true;
                }

                dgvPropertyList[3, currRow].ToolTipText = text;
                dgvPropertyList[6, currRow].Value = text;
                
            }
            else
            {
                dgvPropertyList[6, currRow].Value = "null";
            }
            dgvPropertyList[3, currRow].Value = "Value";
            return isChanged;
        }

        private void frmPropertiesIDList_Load(object sender, EventArgs e)
        {
            if (IsCloseWithoutSave)
            {
                btnClose.Visible = false;
                btnSetAllChangedPropertiesAsVarient.Visible = false;
                btnSetEachChangedNames.Visible = false;
                btnSetEachChangedPropertyAsType.Visible = false;
                btnSetEachChangedPropertyAsVarient.Visible = false;
                //btnUpdatePropertiesAndLocationPoints.Visible = false;
                pnlUpdateProperties.Visible = false;
            }
            else
                btnCloseWithoutSave.Visible = false;
        }

        private void UpdateAfterUserChanged(int rowIndex, DNEPropertyType propertyType, object newValue)
        {
            object propValue = dgvPropertyList[3, rowIndex].Tag;

            dgvPropertyList[4, rowIndex].Value = false;
            bool isChanged = SetValueCellView(rowIndex, propertyType, newValue);
            dgvPropertyList[3, rowIndex].Tag = newValue;
            if (isChanged)
                dgvPropertyList[7, rowIndex].Value = true;
        }

        private void SetIsChanged(int row, bool value)
        {
            dgvPropertyList[7, row].Value = value;
        }

        private void OpenPropertyTypeForm(int rowIndex, uint id, DNEPropertyType propType)
        {
            object propValue = dgvPropertyList[3, rowIndex].Tag;
            switch (propType)
            {
                case DNEPropertyType._EPT_ANIMATION:
                    frmAnimationPropertyType AnimationPropertyTypeForm = new frmAnimationPropertyType(id, (DNSMcAnimation)propValue);
                    if (AnimationPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_ANIMATION, AnimationPropertyTypeForm.AnimationPropertyValue);
                    }
                    break;
                case DNEPropertyType._EPT_ATTENUATION:
                    frmAttenuationPropertyType AttenuationPropertyTypeForm = new frmAttenuationPropertyType(id, (DNSMcAttenuation)propValue);
                    if (AttenuationPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_ATTENUATION, AttenuationPropertyTypeForm.AttenuationPropertyValue);
                    }
                    break;
                case DNEPropertyType._EPT_BCOLOR:
                    frmColorPropertyType BColorPropertyTypeForm = new frmColorPropertyType(id);
                    BColorPropertyTypeForm.BColor = (DNSMcBColor)propValue;
                    if (BColorPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_BCOLOR, BColorPropertyTypeForm.BColor);
                    }
                    break;
                case DNEPropertyType._EPT_BCOLOR_ARRAY:
                    //DNSArrayProperty<DNSMcBColor> a = new DNSArrayProperty<DNSMcBColor>(new DNSMcBColor[1] { (DNSMcBColor)propValue });
                    frmColorArrayPropertyType colorArrayPropertyTypeForm = new frmColorArrayPropertyType(id, (DNSArrayProperty<DNSMcBColor>)propValue);
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
                    frmBoolPropertyType BoolPropertyTypeForm = new frmBoolPropertyType(id, (bool)propValue);
                    if (BoolPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_BOOL, BoolPropertyTypeForm.IsChecked);
                    }
                    break;
                case DNEPropertyType._EPT_BYTE:
                    frmNumericPropertyType BytePropertyTypeForm = new frmNumericPropertyType(id, m_SenderObj);
                    BytePropertyTypeForm.PropertyByteValue = (byte)propValue;
                    if (BytePropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_BYTE, BytePropertyTypeForm.PropertyByteValue);
                    }
                    
                    break;
                case DNEPropertyType._EPT_ENUM:
                    frmNumericPropertyType EnumPropertyTypeForm = new frmNumericPropertyType(id, m_SenderObj);
                    string enumName = MCTPrivatePropertiesData.GetEnumNameFromActualName(m_CurrScheme, id);
                    if (enumName != null && enumName != "")
                    {
                        EnumPropertyTypeForm.PropertyUintValue = (uint)propValue;
                        if (EnumPropertyTypeForm.ShowDialog() == DialogResult.OK)
                        {
                            UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_ENUM, EnumPropertyTypeForm.PropertyUintValue);
                        }
                    }
                    break;
                case DNEPropertyType._EPT_SBYTE:
                    frmNumericPropertyType SBytePropertyTypeForm = new frmNumericPropertyType(id, m_SenderObj);
                    SBytePropertyTypeForm.PropertySByteValue = (sbyte)propValue;
                    if (SBytePropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_SBYTE, SBytePropertyTypeForm.PropertySByteValue);
                    }
                    break;
                case DNEPropertyType._EPT_CONDITIONALSELECTOR:
                    frmConditionalSelectorPropertyType ConditionalSelectorPropertyTypeForm = new frmConditionalSelectorPropertyType(id, m_CurrScheme, (IDNMcConditionalSelector)propValue);
                    if (ConditionalSelectorPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_CONDITIONALSELECTOR, ConditionalSelectorPropertyTypeForm.PropertySelectorValue);
                    }
                    break;
                case DNEPropertyType._EPT_DOUBLE:
                    frmNumericPropertyType DoublePropertyTypeForm = new frmNumericPropertyType(id);
                    DoublePropertyTypeForm.PropertyDoubleValue = (double)propValue;
                    if (DoublePropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_DOUBLE, DoublePropertyTypeForm.PropertyDoubleValue);
                    }
                    break;
                case DNEPropertyType._EPT_FCOLOR:
                    frmColorPropertyType FColorPropertyTypeForm = new frmColorPropertyType(id);
                    FColorPropertyTypeForm.FColor = (DNSMcFColor)propValue;
                    if (FColorPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_FCOLOR, FColorPropertyTypeForm.FColor);
                    }
                    break;
                case DNEPropertyType._EPT_FLOAT:
                    frmNumericPropertyType FloatPropertyTypeForm = new frmNumericPropertyType(id);
                    FloatPropertyTypeForm.PropertyFloatValue = (float)propValue;
                    if (FloatPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_FLOAT, FloatPropertyTypeForm.PropertyFloatValue);
                    }
                    break;
                case DNEPropertyType._EPT_FONT:
                  
                        frmFontPropertyType FontPropertyTypeForm = new frmFontPropertyType(id, (IDNMcFont)propValue);
                        if (FontPropertyTypeForm.ShowDialog() == DialogResult.OK)
                        {
                            UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_FONT, FontPropertyTypeForm.McFont);
                        }
                   


                    break;
                case DNEPropertyType._EPT_FVECTOR2D:
                    frmFVector2DPropertyType FVector2DPropertyTypeForm = new frmFVector2DPropertyType(id, (DNSMcFVector2D)propValue);
                    if (FVector2DPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_FVECTOR2D, FVector2DPropertyTypeForm.FVector2D);
                    }
                    break;
                case DNEPropertyType._EPT_FVECTOR2D_ARRAY:
                    //DNSArrayProperty<DNSMcFVector2D> a = new DNSArrayProperty<DNSMcFVector2D>(new DNSMcFVector2D[1] { (DNSMcFVector2D)propValue });
                    frmVector2DArrayPropertyType frmFVector2DArrayPropertyTypeForm = new frmVector2DArrayPropertyType(id, (DNSArrayProperty<DNSMcFVector2D>)propValue);
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
                    frmFVector3DPropertyType FVector3DPropertyTypeForm = new frmFVector3DPropertyType(id, (DNSMcFVector3D)propValue);
                    if (FVector3DPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_FVECTOR3D, FVector3DPropertyTypeForm.FVector3D);
                    }
                    break;
                case DNEPropertyType._EPT_FVECTOR3D_ARRAY:
                    //DNSArrayProperty<DNSMcFVector3D> ba = new DNSArrayProperty<DNSMcFVector3D>(new DNSMcFVector3D[1] { (DNSMcFVector3D)propValue });
                    frmVector3DArrayPropertyType frmFVector3DArrayPropertyTypeForm = new frmVector3DArrayPropertyType(id, (DNSArrayProperty<DNSMcFVector3D>)propValue);
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
                    IntPropertyTypeForm.PropertyIntValue = (int)propValue;
                    if (IntPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_INT, IntPropertyTypeForm.PropertyIntValue);
                    }
                    break;
                case DNEPropertyType._EPT_MESH:
                    frmMeshPropertyType MeshPropertyTypeForm = new frmMeshPropertyType(id, (IDNMcMesh)propValue);
                    if (MeshPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_MESH, MeshPropertyTypeForm.SelectedMesh);
                    }
                    break;
                case DNEPropertyType._EPT_ROTATION:
                    frmRotationPropertyType RotationPropertyTypeForm = new frmRotationPropertyType(id, (DNSMcRotation)propValue);
                    if (RotationPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_ROTATION, RotationPropertyTypeForm.Rotation);
                    }
                    break;
                case DNEPropertyType._EPT_STRING:
                    frmStringPropertyType StringPropertyTypeForm = new frmStringPropertyType(id, (DNMcVariantString)propValue);
                    if (StringPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_STRING, StringPropertyTypeForm.StringPropertyValue);
                    }
                    break;
                case DNEPropertyType._EPT_SUBITEM_ARRAY:
                    frmSubItemsDataPropertyType SubItemsDataPropertyTypeForm = new frmSubItemsDataPropertyType(id, (DNSArrayProperty<DNSMcSubItemData>)propValue);
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
                case DNEPropertyType._EPT_TEXTURE:
                    frmTexturePropertyType TexturePropertyTypeForm = new frmTexturePropertyType(id, (IDNMcTexture)propValue);
                    if (TexturePropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_TEXTURE, TexturePropertyTypeForm.SelectedTexture);
                    }
                    break;
                case DNEPropertyType._EPT_UINT:
                    frmNumericPropertyType UIntPropertyTypeForm = new frmNumericPropertyType(id, m_SenderObj);
                    UIntPropertyTypeForm.PropertyUintValue = (uint)propValue;
                    if (UIntPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_UINT, UIntPropertyTypeForm.PropertyUintValue);
                    }
                    break;
                case DNEPropertyType._EPT_UINT_ARRAY:
                    frmNumericArrayPropertyType numberArrayPropertyTypeForm = new frmNumericArrayPropertyType(id, (DNSArrayProperty<uint>)propValue);
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
                case DNEPropertyType._EPT_INT_ARRAY:

                    List<SPropertyDesc> sPropertyDescs = frmPrivatePropertiesDescription.GetPrivatePropertiesDescription(m_CurrScheme, id);
                    bool isPointsDuplications = sPropertyDescs.Select(x => x.propertyDesc).Contains("PointsDuplication");

                    frmNumericArrayPropertyType intArrayPropertyTypeForm = new frmNumericArrayPropertyType(id);
                    if (isPointsDuplications)
                        intArrayPropertyTypeForm.SetHeadersColumns(2, "Index", "Count");
                    intArrayPropertyTypeForm.IntArrayPropertyValue = ((DNSArrayProperty<int>)propValue);

                    if (intArrayPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        if (intArrayPropertyTypeForm.IntArrayPropertyValue.aElements != null)
                        {
                            UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_INT_ARRAY, intArrayPropertyTypeForm.IntArrayPropertyValue);
                        }
                        else
                            MessageBox.Show("Missing Numbers.\nIn order to make changes set parameters again", "Problem in Numbers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case DNEPropertyType._EPT_VECTOR2D:
                    frmVector2DPropertyType Vector2DPropertyTypeForm = new frmVector2DPropertyType(id, (DNSMcVector2D)propValue);
                    if (Vector2DPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_VECTOR2D, Vector2DPropertyTypeForm.GetVector2D());
                    }
                    break;
                case DNEPropertyType._EPT_VECTOR2D_ARRAY:
                    //DNSArrayProperty<DNSMcVector2D> av = new DNSArrayProperty<DNSMcVector2D>(new DNSMcVector2D[1] { (DNSMcVector2D)propValue });
                    frmVector2DArrayPropertyType frmVector2DArrayPropertyTypeForm = new frmVector2DArrayPropertyType(id, (DNSArrayProperty<DNSMcVector2D>)propValue);
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
                    frmVector3DPropertyType Vector3DPropertyTypeForm = new frmVector3DPropertyType(id, (DNSMcVector3D)propValue);
                    if (Vector3DPropertyTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateAfterUserChanged(rowIndex, DNEPropertyType._EPT_VECTOR3D, Vector3DPropertyTypeForm.GetVector3D());
                    }
                    break;
                case DNEPropertyType._EPT_VECTOR3D_ARRAY:
                    //DNSArrayProperty<DNSMcVector3D> ba = new DNSArrayProperty<DNSMcVector3D>(new DNSMcVector3D[1] { (DNSMcVector3D)propValue });
                    frmVector3DArrayPropertyType frmVector3DArrayPropertyTypeForm = new frmVector3DArrayPropertyType(id, (DNSArrayProperty<DNSMcVector3D>)propValue);
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
            }
        }

        private object GetSpecificPropertyValueByType(uint propID, DNEPropertyType propType)
        {
            object propVal = null;

            if (m_SenderObj is IDNMcObject)
            {
                IDNMcObject currObject = (IDNMcObject)m_SenderObj;

                switch (propType)
                {
                    case DNEPropertyType._EPT_ANIMATION:
                        try
                        {
                            propVal = currObject.GetAnimationProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetAnimationProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_ATTENUATION:
                        try
                        {
                            propVal = currObject.GetAttenuationProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetAttenuationProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_BCOLOR:
                        try
                        {
                            propVal = currObject.GetBColorProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetBColorProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_BCOLOR_ARRAY:
                        try
                        {
                            propVal = currObject.GetArrayProperty<DNSMcBColor>(propID, DNEPropertyType._EPT_BCOLOR_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayProperty<DNSMcBColor>", McEx); }
                        break;
                    case DNEPropertyType._EPT_BOOL:
                        try
                        {
                            propVal = currObject.GetBoolProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetBoolProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_SBYTE:
                        try
                        {
                            propVal = currObject.GetSByteProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetSByteProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_BYTE:
                        try
                        {
                            propVal = currObject.GetByteProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetByteProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_ENUM:
                        try
                        {
                            propVal = currObject.GetEnumProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetEnumProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_CONDITIONALSELECTOR:
                        try
                        {
                            propVal = currObject.GetConditionalSelectorProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetConditionalSelectorProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_DOUBLE:
                        try
                        {
                            propVal = currObject.GetDoubleProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetDoubleProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_FCOLOR:
                        try
                        {
                            propVal = currObject.GetFColorProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetBColorProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_FLOAT:
                        try
                        {
                            propVal = currObject.GetFloatProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetFloatProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_FONT:
                        try
                        {
                            propVal = currObject.GetFontProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetFontProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_FVECTOR2D:
                        try
                        {
                            propVal = currObject.GetFVector2DProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetFVector2DProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_FVECTOR2D_ARRAY:
                        try
                        {
                            propVal = currObject.GetArrayProperty<DNSMcFVector2D>(propID, DNEPropertyType._EPT_FVECTOR2D_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayProperty<DNSMcFVector2D>", McEx); }
                        break;
                    case DNEPropertyType._EPT_FVECTOR3D:
                        try
                        {
                            propVal = currObject.GetFVector3DProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetFVector3DProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_FVECTOR3D_ARRAY:
                        try
                        {
                            propVal = currObject.GetArrayProperty<DNSMcFVector3D>(propID, DNEPropertyType._EPT_FVECTOR3D_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayProperty<DNSMcFVector3D>", McEx); }
                        break;
                    case DNEPropertyType._EPT_INT:
                        try
                        {
                            propVal = currObject.GetIntProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetIntProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_MESH:
                        try
                        {
                            propVal = currObject.GetMeshProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetMeshProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_ROTATION:
                        try
                        {
                            propVal = currObject.GetRotationProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetRotationProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_STRING:
                        try
                        {
                            propVal = currObject.GetStringProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetStringProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_SUBITEM_ARRAY:
                        try
                        {
                            propVal = currObject.GetArrayProperty<DNSMcSubItemData>(propID, DNEPropertyType._EPT_SUBITEM_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayProperty<DNSMcSubItemData>", McEx); }
                        break;
                    case DNEPropertyType._EPT_TEXTURE:
                        try
                        {
                            propVal = currObject.GetTextureProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetTextureProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_UINT:
                        try
                        {
                            propVal = currObject.GetUIntProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetUIntProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_UINT_ARRAY:
                        try
                        {
                            propVal = currObject.GetArrayProperty<uint>(propID, DNEPropertyType._EPT_UINT_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayProperty<UInt32>", McEx); }
                        break;
                    case DNEPropertyType._EPT_INT_ARRAY:
                        try
                        {
                            propVal = currObject.GetArrayProperty<int>(propID, DNEPropertyType._EPT_INT_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayProperty<Int32>", McEx); }
                        break;
                    case DNEPropertyType._EPT_VECTOR2D:
                        try
                        {
                            propVal = currObject.GetVector2DProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetVector2DProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_VECTOR2D_ARRAY:
                        try
                        {
                            propVal = currObject.GetArrayProperty<DNSMcVector2D>(propID, DNEPropertyType._EPT_VECTOR2D_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayProperty<DNSMcVector2D>", McEx); }
                        break;
                    case DNEPropertyType._EPT_VECTOR3D:
                        try
                        {
                            propVal = currObject.GetVector3DProperty(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetVector3DProperty", McEx); }
                        break;
                    case DNEPropertyType._EPT_VECTOR3D_ARRAY:
                        try
                        {
                            propVal = currObject.GetArrayProperty<DNSMcVector3D>(propID, DNEPropertyType._EPT_VECTOR3D_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayProperty<DNSMcVector3D>", McEx); }
                        break;
                }
            }
            else
            {
                switch (propType)
                {
                    case DNEPropertyType._EPT_ANIMATION:
                        try
                        {
                            propVal = m_CurrScheme.GetAnimationPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetAnimationPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_ATTENUATION:
                        try
                        {
                            propVal = m_CurrScheme.GetAttenuationPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetAttenuationPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_BCOLOR:
                        try
                        {
                            propVal = m_CurrScheme.GetBColorPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetBColorPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_BCOLOR_ARRAY:
                        try
                        {
                            propVal = m_CurrScheme.GetArrayPropertyDefault<DNSMcBColor>(propID, DNEPropertyType._EPT_BCOLOR_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayPropertyDefault<DNSMcBColor>", McEx); }
                        break;
                    case DNEPropertyType._EPT_BOOL:
                        try
                        {
                            propVal = m_CurrScheme.GetBoolPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetBoolPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_SBYTE:
                        try
                        {
                            propVal = m_CurrScheme.GetSBytePropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetSBytePropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_BYTE:
                        try
                        {
                            propVal = m_CurrScheme.GetBytePropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetBytePropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_ENUM:
                        try
                        {
                            propVal = m_CurrScheme.GetEnumPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetEnumPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_CONDITIONALSELECTOR:
                        try
                        {
                            propVal = m_CurrScheme.GetConditionalSelectorPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetConditionalSelectorPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_DOUBLE:
                        try
                        {
                            propVal = m_CurrScheme.GetDoublePropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetDoublePropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_FCOLOR:
                        try
                        {
                            propVal = m_CurrScheme.GetFColorPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetFColorPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_FLOAT:
                        try
                        {
                            propVal = m_CurrScheme.GetFloatPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetFloatPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_FONT:
                        try
                        {
                            propVal = m_CurrScheme.GetFontPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetFontPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_FVECTOR2D:
                        try
                        {
                            propVal = m_CurrScheme.GetFVector2DPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetFVector2DPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_FVECTOR2D_ARRAY:
                        try
                        {
                            propVal = m_CurrScheme.GetArrayPropertyDefault<DNSMcFVector2D>(propID, DNEPropertyType._EPT_FVECTOR2D_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayPropertyDefault<DNSMcFVector2D>", McEx); }
                        break;
                    case DNEPropertyType._EPT_FVECTOR3D:
                        try
                        {
                            propVal = m_CurrScheme.GetFVector3DPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetFVector3DPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_FVECTOR3D_ARRAY:
                        try
                        {
                            propVal = m_CurrScheme.GetArrayPropertyDefault<DNSMcFVector3D>(propID, DNEPropertyType._EPT_FVECTOR3D);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayPropertyDefault<DNSMcFVector3D>", McEx); }
                        break;
                    case DNEPropertyType._EPT_INT:
                        try
                        {
                            propVal = m_CurrScheme.GetIntPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetIntPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_MESH:
                        try
                        {
                            propVal = m_CurrScheme.GetMeshPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetMeshPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_ROTATION:
                        try
                        {
                            propVal = m_CurrScheme.GetRotationPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetRotationPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_STRING:
                        try
                        {
                            propVal = m_CurrScheme.GetStringPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetStringPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_SUBITEM_ARRAY:
                        try
                        {
                            propVal = m_CurrScheme.GetArrayPropertyDefault<DNSMcSubItemData>(propID, DNEPropertyType._EPT_SUBITEM_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayPropertyDefault<DNSMcSubItemData>", McEx); }
                        break;
                    case DNEPropertyType._EPT_TEXTURE:
                        try
                        {
                            propVal = m_CurrScheme.GetTexturePropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetTexturePropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_UINT:
                        try
                        {
                            propVal = m_CurrScheme.GetUIntPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetUIntPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_UINT_ARRAY:
                        try
                        {
                            propVal = m_CurrScheme.GetArrayPropertyDefault<uint>(propID, DNEPropertyType._EPT_UINT_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayPropertyDefault<UInt32>", McEx); }
                        break;
                    case DNEPropertyType._EPT_INT_ARRAY:
                        try
                        {
                            propVal = m_CurrScheme.GetArrayPropertyDefault<int>(propID, DNEPropertyType._EPT_INT_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayPropertyDefault<Int32>", McEx); }
                        break;
                    case DNEPropertyType._EPT_VECTOR2D:
                        try
                        {
                            propVal = m_CurrScheme.GetVector2DPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetVector2DPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_VECTOR2D_ARRAY:
                        try
                        {
                            propVal = m_CurrScheme.GetArrayPropertyDefault<DNSMcVector2D>(propID, DNEPropertyType._EPT_VECTOR2D_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayPropertyDefault<DNSMcVector2D>", McEx); }
                        break;
                    case DNEPropertyType._EPT_VECTOR3D:
                        try
                        {
                            propVal = m_CurrScheme.GetVector3DPropertyDefault(propID);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetVector3DPropertyDefault", McEx); }
                        break;
                    case DNEPropertyType._EPT_VECTOR3D_ARRAY:
                        try
                        {
                            propVal = m_CurrScheme.GetArrayPropertyDefault<DNSMcVector3D>(propID, DNEPropertyType._EPT_VECTOR3D_ARRAY);
                        }
                        catch (MapCoreException McEx) { Utilities.ShowErrorMessage("GetArrayPropertyDefault<DNSMcVector3D>", McEx); }
                        break;
                }
            }
            return propVal;
        }

        private bool IsCellChecked(int col, int row)
        {
            return (dgvPropertyList[col, row].Value != null) && ((bool)dgvPropertyList[col, row].Value == true);
        }

        private void GetPropertiesValue(int row)
        {
            uint propID = uint.Parse(dgvPropertyList[0, row].Value.ToString());
            DNEPropertyType propType = (DNEPropertyType)Enum.Parse(typeof(DNEPropertyType), dgvPropertyList[1, row].Value.ToString());
            object propVal = GetSpecificPropertyValueByType(propID, propType);

            dgvPropertyList[3, row].Tag = propVal;
            SetValueCellView(row, propType, propVal);
            dgvPropertyList[7, row].Value = false;
            dgvPropertyList[8, row].Value = false;
            try
            {
                if (((IDNMcObject)m_SenderObj).IsPropertyDefault(propID) == true)
                    dgvPropertyList[4, row].Value = true;
                else
                    dgvPropertyList[4, row].Value = false;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IsPropertyDefault", McEx);
            }
        }

        private List<DNSVariantProperty> GetChangedProperties()
        {
            List<DNSVariantProperty> changedPropertiesList = new List<DNSVariantProperty>();
            DNSVariantProperty variantProp;

            for (int row = 0; row < dgvPropertyList.RowCount; row++)
            {
                if (IsCellChecked(7, row))
                {
                    variantProp = new DNSVariantProperty();
                    variantProp.eType = (DNEPropertyType)Enum.Parse(typeof(DNEPropertyType), dgvPropertyList[1, row].Value.ToString());

                    object strNameValue = dgvPropertyList[2, row].Value;
                    if ((!chxByNameIfExists.Checked) || (strNameValue == null || (strNameValue != null && strNameValue.ToString() == "")))
                        variantProp.uID = uint.Parse(dgvPropertyList[0, row].Value.ToString());
                    else if (chxByNameIfExists.Checked && strNameValue != null && strNameValue.ToString() != "")
                    {
                        variantProp.strName = dgvPropertyList[2, row].Value.ToString();
                        variantProp.uID = 0;
                    }
                    variantProp.uID = uint.Parse(dgvPropertyList[0, row].Value.ToString());
                    variantProp.Value = dgvPropertyList[3, row].Tag;
                    changedPropertiesList.Add(variantProp);
                }
            }
            return changedPropertiesList;
        }

        private void dgvPropertyList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !m_LoadData)
            {
                if (e.ColumnIndex == 2)
                {
                    dgvPropertyList[7, e.RowIndex].Value = true;
                }
                else if (e.ColumnIndex == 4 && !IsCellChecked(4, e.RowIndex) && !IsCellChecked(7, e.RowIndex))
                {
                    dgvPropertyList[7, e.RowIndex].Value = true;
                }
                else if (e.ColumnIndex == 7 && IsCellChecked(7, e.RowIndex) && IsCellChecked(8, e.RowIndex))
                {
                    dgvPropertyList[8, e.RowIndex].Value = false;
                }
                else if (e.ColumnIndex == 8 && IsCellChecked(8, e.RowIndex) && IsCellChecked(7, e.RowIndex))
                {
                    dgvPropertyList[7, e.RowIndex].Value = false;
                }
            }
        }

        private void dgvPropertyList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPropertyList.IsCurrentCellDirty)
            {
                dgvPropertyList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

        }

        private void dgvPropertyList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 3 && dgvPropertyList[3, e.RowIndex].ReadOnly == false)
                {
                    int rowIndex = e.RowIndex;
                    uint id = (uint)(dgvPropertyList[0, rowIndex].Value);
                    DNEPropertyType buttonType = (DNEPropertyType)Enum.Parse(typeof(DNEPropertyType), dgvPropertyList[1, rowIndex].Value.ToString());

                    OpenPropertyTypeForm(rowIndex, id, buttonType);
                }
                if (e.ColumnIndex == 0 && dgvPropertyList[0, e.RowIndex].ReadOnly == false)
                {
                    int rowIndex = e.RowIndex;
                    uint id = (uint)(dgvPropertyList[0, rowIndex].Value);
                    frmPrivatePropertiesDescription privatePropertiesDescription = null;
                    try
                    {
                        privatePropertiesDescription = new frmPrivatePropertiesDescription(m_CurrScheme, id, "Properties using this id");
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetNodesByPropertyID", McEx);
                        return;
                    }

                    if (Manager_MCPropertyDescription.FrmPrivatePropertiesDescription != null)
                        Manager_MCPropertyDescription.FrmPrivatePropertiesDescription.Dispose();
                    Manager_MCPropertyDescription.FrmPrivatePropertiesDescription = privatePropertiesDescription;
                    privatePropertiesDescription.Show();
                }
            }
        }

        private void btnResetEachProperty_Click(object sender, EventArgs e)
        {
            IDNMcObject currObject = (IDNMcObject)m_SenderObj;
            uint id;
            bool isAnyRowChecked = false;
            for (int row = 0; row < dgvPropertyList.RowCount; row++)
            {
                id = uint.Parse(dgvPropertyList[0, row].Value.ToString());
                object propStrValue = dgvPropertyList[2, row].Value;

                if (IsCellChecked(8, row))
                {
                    isAnyRowChecked = true;
                    try
                    {
                        if(IsSetByName(propStrValue))
                            currObject.ResetProperty(propStrValue.ToString());
                        else
                            currObject.ResetProperty(id);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("ResetProperty", McEx);
                    }

                    GetPropertiesValue(row);
                    //dgvPropertyList[8, row].Value = false;
                }
            }
            if (isAnyRowChecked)
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrScheme.GetOverlayManager());
            else
                ShowErrorToUser("Reset Each Property", "No Row Selected");
        }

        private bool IsSetByName(object propStrValue)
        {
            return chxByNameIfExists.Checked && propStrValue != null && propStrValue.ToString() != "";
        }

        private void btnSetEachChangedPropertyAsType_Click(object sender, EventArgs e)
        {
            bool isAnyRowChecked = false;
            uint propID;
            object propVal, propStrValue;

            for (int row = 0; row < dgvPropertyList.RowCount; row++)
            {
                if (IsCellChecked(7, row))
                {
                    isAnyRowChecked = true;
                    DNEPropertyType propType = (DNEPropertyType)Enum.Parse(typeof(DNEPropertyType), dgvPropertyList[1, row].Value.ToString());
                    propID = uint.Parse(dgvPropertyList[0, row].Value.ToString());
                    propVal = dgvPropertyList[3, row].Tag;
                    propStrValue = dgvPropertyList[2, row].Value;

                    if (m_SenderObj is IDNMcObject)
                    {
                        IDNMcObject currObject = (IDNMcObject)m_SenderObj;

                        switch (propType)
                        {
                            case DNEPropertyType._EPT_ANIMATION:
                                try
                                {
                                    if(IsSetByName(propStrValue))
                                        currObject.SetAnimationProperty(propStrValue.ToString(), (DNSMcAnimation)propVal);
                                    else
                                        currObject.SetAnimationProperty(propID, (DNSMcAnimation)propVal);

                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetAnimationProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_ATTENUATION:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetAttenuationProperty(propStrValue.ToString(), (DNSMcAttenuation)propVal);
                                    else
                                        currObject.SetAttenuationProperty(propID, (DNSMcAttenuation)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetAttenuationProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_BCOLOR:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetBColorProperty(propStrValue.ToString(), (DNSMcBColor)propVal);
                                    else
                                        currObject.SetBColorProperty(propID, (DNSMcBColor)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetColorProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_BCOLOR_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetArrayProperty(propStrValue.ToString(), DNEPropertyType._EPT_BCOLOR_ARRAY, (DNSArrayProperty<DNSMcBColor>)propVal);
                                    else
                                        currObject.SetArrayProperty(propID, DNEPropertyType._EPT_BCOLOR_ARRAY, (DNSArrayProperty<DNSMcBColor>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetColorProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_BOOL:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetBoolProperty(propStrValue.ToString(), (bool)propVal);
                                    else
                                        currObject.SetBoolProperty(propID, (bool)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetBoolProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_SBYTE:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetSByteProperty(propStrValue.ToString(), (sbyte)propVal);
                                    else
                                        currObject.SetSByteProperty(propID, (sbyte)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetSByteProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_BYTE:
                                try
                                {
                                    
                                        if(IsSetByName(propStrValue))
                                            currObject.SetByteProperty(propStrValue.ToString(), (byte)propVal);
                                        else
                                            currObject.SetByteProperty(propID, (byte)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetByteProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_ENUM:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetEnumProperty(propStrValue.ToString(), (uint)propVal);
                                    else
                                        currObject.SetEnumProperty(propID, (uint)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetEnumProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_CONDITIONALSELECTOR:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetConditionalSelectorProperty(propStrValue.ToString(), (IDNMcConditionalSelector)propVal);
                                    else
                                        currObject.SetConditionalSelectorProperty(propID, (IDNMcConditionalSelector)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetConditionalSelectorProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_DOUBLE:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetDoubleProperty(propStrValue.ToString(), (double)propVal);
                                    else
                                        currObject.SetDoubleProperty(propID, (double)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetDoubleProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_FCOLOR:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetFColorProperty(propStrValue.ToString(), (DNSMcFColor)propVal);
                                     else
                                        currObject.SetFColorProperty(propID, (DNSMcFColor)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetColorProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_FLOAT:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetFloatProperty(propStrValue.ToString(), (float)propVal);
                                    else
                                        currObject.SetFloatProperty(propID, (float)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetFloatProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_FONT:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetFontProperty(propStrValue.ToString(), (IDNMcFont)propVal);
                                    else
                                        currObject.SetFontProperty(propID, (IDNMcFont)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetFontProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_FVECTOR2D:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetFVector2DProperty(propStrValue.ToString(), (DNSMcFVector2D)propVal);
                                    else
                                        currObject.SetFVector2DProperty(propID, (DNSMcFVector2D)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetFVector2DProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_FVECTOR2D_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetArrayProperty(propStrValue.ToString(), DNEPropertyType._EPT_FVECTOR2D_ARRAY, (DNSArrayProperty<DNSMcFVector2D>)propVal);
                                    else
                                        currObject.SetArrayProperty(propID, DNEPropertyType._EPT_FVECTOR2D_ARRAY, (DNSArrayProperty<DNSMcFVector2D>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetFVector2DProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_FVECTOR3D:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetFVector3DProperty(propStrValue.ToString(), (DNSMcFVector3D)propVal);
                                    else
                                        currObject.SetFVector3DProperty(propID, (DNSMcFVector3D)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetFVector3DProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_FVECTOR3D_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetArrayProperty(propStrValue.ToString(), DNEPropertyType._EPT_FVECTOR3D_ARRAY, (DNSArrayProperty<DNSMcFVector3D>)propVal);
                                    else
                                        currObject.SetArrayProperty(propID, DNEPropertyType._EPT_FVECTOR3D_ARRAY, (DNSArrayProperty<DNSMcFVector3D>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetFVector3DProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_INT:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetIntProperty(propStrValue.ToString(), (int)propVal);
                                    else
                                        currObject.SetIntProperty(propID, (int)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetIntProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_MESH:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetMeshProperty(propStrValue.ToString(), (IDNMcMesh)propVal);
                                    else
                                        currObject.SetMeshProperty(propID, (IDNMcMesh)propVal);
                                    SetIsChanged(row, false);

                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetMeshProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_ROTATION:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetRotationProperty(propStrValue.ToString(), (DNSMcRotation)propVal);
                                    else
                                        currObject.SetRotationProperty(propID, (DNSMcRotation)propVal);
                                    SetIsChanged(row, false);

                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetRotationProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_STRING:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetStringProperty(propStrValue.ToString(), (DNMcVariantString)propVal);
                                    else
                                        currObject.SetStringProperty(propID, (DNMcVariantString)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetStringProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_SUBITEM_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetArrayProperty(propStrValue.ToString(), DNEPropertyType._EPT_SUBITEM_ARRAY, (DNSArrayProperty<DNSMcSubItemData>)propVal);
                                     else
                                        currObject.SetArrayProperty(propID, DNEPropertyType._EPT_SUBITEM_ARRAY, (DNSArrayProperty<DNSMcSubItemData>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetSubItemsDataProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_TEXTURE:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetTextureProperty(propStrValue.ToString(), (IDNMcTexture)propVal);
                                     else
                                        currObject.SetTextureProperty(propID, (IDNMcTexture)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetTextureProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_UINT:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetUIntProperty(propStrValue.ToString(), (uint)propVal);
                                    else
                                        currObject.SetUIntProperty(propID, (uint)propVal);
                                    SetIsChanged(row, false);

                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetUIntProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_UINT_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetArrayProperty(propStrValue.ToString(), DNEPropertyType._EPT_UINT_ARRAY, (DNSArrayProperty<uint>)propVal);
                                    else
                                        currObject.SetArrayProperty(propID, DNEPropertyType._EPT_UINT_ARRAY, (DNSArrayProperty<uint>)propVal);
                                    SetIsChanged(row, false);

                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetUIntProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_INT_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetArrayProperty(propStrValue.ToString(), DNEPropertyType._EPT_INT_ARRAY, (DNSArrayProperty<int>)propVal);
                                    else
                                        currObject.SetArrayProperty(propID, DNEPropertyType._EPT_INT_ARRAY, (DNSArrayProperty<int>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetIntProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_VECTOR2D:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetVector2DProperty(propStrValue.ToString(), (DNSMcVector2D)propVal);
                                    else
                                        currObject.SetVector2DProperty(propID, (DNSMcVector2D)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetVector2DProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_VECTOR2D_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetArrayProperty(propStrValue.ToString(), DNEPropertyType._EPT_VECTOR2D_ARRAY, (DNSArrayProperty<DNSMcVector2D>)propVal);
                                    else
                                        currObject.SetArrayProperty(propID, DNEPropertyType._EPT_VECTOR2D_ARRAY, (DNSArrayProperty<DNSMcVector2D>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetVector2DProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_VECTOR3D:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetVector3DProperty(propStrValue.ToString(), (DNSMcVector3D)propVal);
                                    else
                                        currObject.SetVector3DProperty(propID, (DNSMcVector3D)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetVector3DProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_VECTOR3D_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        currObject.SetArrayProperty(propStrValue.ToString(), DNEPropertyType._EPT_VECTOR3D_ARRAY, (DNSArrayProperty<DNSMcVector3D>)propVal);
                                    else
                                        currObject.SetArrayProperty(propID, DNEPropertyType._EPT_VECTOR3D_ARRAY, (DNSArrayProperty<DNSMcVector3D>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetVector3DProperty", McEx);
                                }
                                break;
                        }
                    }
                    else
                    {
                        if (propID != 0)
                            SetPropertyName(row, propID);
                        //SetPropertyValueAtSchema(m_PropType, m_PropID, m_PropVal, propStrValue);
                        switch (propType)
                        {
                            case DNEPropertyType._EPT_ANIMATION:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetAnimationPropertyDefault(propStrValue.ToString(), (DNSMcAnimation)propVal);
                                    else
                                        m_CurrScheme.SetAnimationPropertyDefault(propID, (DNSMcAnimation)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetAnimationPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_ATTENUATION:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetAttenuationPropertyDefault(propStrValue.ToString(), (DNSMcAttenuation)propVal);
                                    else
                                        m_CurrScheme.SetAttenuationPropertyDefault(propID, (DNSMcAttenuation)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetAttenuationPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_BCOLOR:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetBColorPropertyDefault(propStrValue.ToString(), (DNSMcBColor)propVal);
                                    else
                                        m_CurrScheme.SetBColorPropertyDefault(propID, (DNSMcBColor)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetBColorPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_BOOL:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetBoolPropertyDefault(propStrValue.ToString(), (bool)propVal);
                                    else
                                        m_CurrScheme.SetBoolPropertyDefault(propID, (bool)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetBoolPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_BYTE:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetBytePropertyDefault(propStrValue.ToString(), (byte)propVal);
                                    else
                                        m_CurrScheme.SetBytePropertyDefault(propID, (byte)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetBytePropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_ENUM:
                                try
                                {
                                    string propName = IsSetByName(propStrValue) ? propStrValue.ToString() : "";
                                    string enumName = MCTPrivatePropertiesData.GetEnumNameFromActualName(m_CurrScheme, propID, propName);
                                    if (enumName != null && enumName != "")
                                    {
                                        if (IsSetByName(propStrValue))
                                            m_CurrScheme.SetEnumPropertyDefault(propStrValue.ToString(), (uint)propVal);
                                        else
                                            m_CurrScheme.SetEnumPropertyDefault(propID, (uint)propVal);

                                        SetIsChanged(row, false);
                                    }
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetEnumPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_SBYTE:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetSBytePropertyDefault(propStrValue.ToString(), (sbyte)propVal);
                                    else
                                        m_CurrScheme.SetSBytePropertyDefault(propID, (sbyte)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetSBytePropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_CONDITIONALSELECTOR:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetConditionalSelectorPropertyDefault(propStrValue.ToString(), (IDNMcConditionalSelector)propVal);
                                    else
                                        m_CurrScheme.SetConditionalSelectorPropertyDefault(propID, (IDNMcConditionalSelector)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("SetConditionalSelectorProperty", McEx);
                                }
                                break;
                            case DNEPropertyType._EPT_DOUBLE:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetDoublePropertyDefault(propStrValue.ToString(), (double)propVal);
                                    else
                                        m_CurrScheme.SetDoublePropertyDefault(propID, (double)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetDoublePropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_FCOLOR:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetFColorPropertyDefault(propStrValue.ToString(), (DNSMcFColor)propVal);
                                    else
                                        m_CurrScheme.SetFColorPropertyDefault(propID, (DNSMcFColor)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetFColorPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_FLOAT:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetFloatPropertyDefault(propStrValue.ToString(), (float)propVal);
                                    else
                                        m_CurrScheme.SetFloatPropertyDefault(propID, (float)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetFloatPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_FONT:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetFontPropertyDefault(propStrValue.ToString(), (IDNMcFont)propVal);
                                    else
                                        m_CurrScheme.SetFontPropertyDefault(propID, (IDNMcFont)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetFontPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_FVECTOR2D:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetFVector2DPropertyDefault(propStrValue.ToString(), (DNSMcFVector2D)propVal);
                                    else
                                        m_CurrScheme.SetFVector2DPropertyDefault(propID, (DNSMcFVector2D)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetFVector2DPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_FVECTOR2D_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetArrayPropertyDefault(propStrValue.ToString(), DNEPropertyType._EPT_FVECTOR2D_ARRAY, (DNSArrayProperty<DNSMcFVector2D>)propVal);
                                    else
                                        m_CurrScheme.SetArrayPropertyDefault(propID, DNEPropertyType._EPT_FVECTOR2D_ARRAY, (DNSArrayProperty<DNSMcFVector2D>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetArrayPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_FVECTOR3D:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetFVector3DPropertyDefault(propStrValue.ToString(), (DNSMcFVector3D)propVal);
                                    else
                                        m_CurrScheme.SetFVector3DPropertyDefault(propID, (DNSMcFVector3D)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetFVector3DPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_FVECTOR3D_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetArrayPropertyDefault(propStrValue.ToString(), DNEPropertyType._EPT_FVECTOR3D_ARRAY, (DNSArrayProperty<DNSMcFVector3D>)propVal);
                                    else
                                        m_CurrScheme.SetArrayPropertyDefault(propID, DNEPropertyType._EPT_FVECTOR3D_ARRAY, (DNSArrayProperty<DNSMcFVector3D>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetArrayPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_INT:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetIntPropertyDefault(propStrValue.ToString(), (int)propVal);
                                    else
                                        m_CurrScheme.SetIntPropertyDefault(propID, (int)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetIntPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_MESH:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetMeshPropertyDefault(propStrValue.ToString(), (IDNMcMesh)propVal);
                                    else
                                        m_CurrScheme.SetMeshPropertyDefault(propID, (IDNMcMesh)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetMeshPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_ROTATION:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetRotationPropertyDefault(propStrValue.ToString(), (DNSMcRotation)propVal);
                                    else
                                        m_CurrScheme.SetRotationPropertyDefault(propID, (DNSMcRotation)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetRotationPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_STRING:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetStringPropertyDefault(propStrValue.ToString(), (DNMcVariantString)propVal);
                                    else
                                        m_CurrScheme.SetStringPropertyDefault(propID, (DNMcVariantString)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetStringPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_SUBITEM_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetArrayPropertyDefault(propStrValue.ToString(), DNEPropertyType._EPT_SUBITEM_ARRAY, (DNSArrayProperty<DNSMcSubItemData>)propVal);
                                    else
                                        m_CurrScheme.SetArrayPropertyDefault(propID, DNEPropertyType._EPT_SUBITEM_ARRAY, (DNSArrayProperty<DNSMcSubItemData>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetArrayPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_UINT_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetArrayPropertyDefault(propStrValue.ToString(), DNEPropertyType._EPT_UINT_ARRAY, (DNSArrayProperty<uint>)propVal);
                                    else
                                        m_CurrScheme.SetArrayPropertyDefault(propID, DNEPropertyType._EPT_UINT_ARRAY, (DNSArrayProperty<uint>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetArrayPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_INT_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetArrayPropertyDefault(propStrValue.ToString(), DNEPropertyType._EPT_INT_ARRAY, (DNSArrayProperty<int>)propVal);
                                    else
                                        m_CurrScheme.SetArrayPropertyDefault(propID, DNEPropertyType._EPT_INT_ARRAY, (DNSArrayProperty<int>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetArrayPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_BCOLOR_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetArrayPropertyDefault(propStrValue.ToString(), DNEPropertyType._EPT_BCOLOR_ARRAY, (DNSArrayProperty<DNSMcBColor>)propVal);
                                    else
                                        m_CurrScheme.SetArrayPropertyDefault(propID, DNEPropertyType._EPT_BCOLOR_ARRAY, (DNSArrayProperty<DNSMcBColor>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetArrayPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_TEXTURE:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetTexturePropertyDefault(propStrValue.ToString(), (IDNMcTexture)propVal);
                                    else
                                        m_CurrScheme.SetTexturePropertyDefault(propID, (IDNMcTexture)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetTexturePropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_UINT:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetUIntPropertyDefault(propStrValue.ToString(), (uint)propVal);
                                    else
                                        m_CurrScheme.SetUIntPropertyDefault(propID, (uint)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetUIntPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_VECTOR2D:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetVector2DPropertyDefault(propStrValue.ToString(), (DNSMcVector2D)propVal);
                                    else
                                        m_CurrScheme.SetVector2DPropertyDefault(propID, (DNSMcVector2D)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetVector2DPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_VECTOR2D_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetArrayPropertyDefault(propStrValue.ToString(), DNEPropertyType._EPT_VECTOR2D_ARRAY, (DNSArrayProperty<DNSMcVector2D>)propVal);
                                    else
                                        m_CurrScheme.SetArrayPropertyDefault(propID, DNEPropertyType._EPT_VECTOR2D_ARRAY, (DNSArrayProperty<DNSMcVector2D>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetArrayPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_VECTOR3D:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetVector3DPropertyDefault(propStrValue.ToString(), (DNSMcVector3D)propVal);
                                    else
                                        m_CurrScheme.SetVector3DPropertyDefault(propID, (DNSMcVector3D)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetVector3DPropertyDefault", McEx); }
                                break;
                            case DNEPropertyType._EPT_VECTOR3D_ARRAY:
                                try
                                {
                                    if (IsSetByName(propStrValue))
                                        m_CurrScheme.SetArrayPropertyDefault(propStrValue.ToString(), DNEPropertyType._EPT_VECTOR3D_ARRAY, (DNSArrayProperty<DNSMcVector3D>)propVal);
                                    else
                                        m_CurrScheme.SetArrayPropertyDefault(propID, DNEPropertyType._EPT_VECTOR3D_ARRAY, (DNSArrayProperty<DNSMcVector3D>)propVal);
                                    SetIsChanged(row, false);
                                }
                                catch (MapCoreException McEx) { Utilities.ShowErrorMessage("SetArrayPropertyDefault", McEx); }
                                break;
                        }
                    }
                }
            }
            if (isAnyRowChecked)
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrScheme.GetOverlayManager());
            else
                ShowErrorToUser("Set Each Changed Property As Type", "No Row Changed");

        }

        private void btnSetAllChangedPropertiesAsVarient_Click(object sender, EventArgs e)
        {
            List<DNSVariantProperty> changedPropertiesList = GetChangedProperties();

            if (changedPropertiesList.Count == 0)
            {
                ShowErrorToUser("Set All Changed Properties As Variant", "No Row Changed");
                return;
            }
            else
            {
                try
                {
                    if (m_SenderObj is IDNMcObject)
                    {
                        IDNMcObject currObject = (IDNMcObject)m_SenderObj;
                        currObject.SetProperties(changedPropertiesList.ToArray());
                    }
                    else
                    {
                        btnSetEachChangedNames_Click(null, null);
                        m_CurrScheme.SetPropertyDefaults(changedPropertiesList.ToArray());
                    }
                    SetIsChangedAllGrid(false);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetProperties", McEx);
                }

                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrScheme.GetOverlayManager());
            }
        }

        private void btnResetAllProperties_Click(object sender, EventArgs e)
        {
            IDNMcObject currObject = (IDNMcObject)m_SenderObj;
            try
            {
                currObject.ResetAllProperties();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ResetAllProperties", McEx);
            }
            Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrScheme.GetOverlayManager());


            for (int row = 0; row < dgvPropertyList.RowCount; row++)
            {
                GetPropertiesValue(row);
            }
        }

        private void SetPropertyName(int rowIndex, uint propertyId)
        {
            try
            {
                object propertyValue = dgvPropertyList[2, rowIndex].Value;
                if (propertyValue != null)
                {
                    string propertyName = propertyValue.ToString();
                    m_CurrScheme.SetPropertyName(propertyName, propertyId);
                    // uint propertyId = uint.Parse(dgvPropertyList[0, rowIndex].Value.ToString());
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetPropertyName", McEx);
            }
        }

        private void btnSetEachChangedNames_Click(object sender, EventArgs e)
        {
            List<DNSPropertyNameID> lstPropertiesNames = new List<DNSPropertyNameID>();
            string propertyName = "";
            uint propertyId = 0;
            bool isAnyChecked = false;
            DNSPropertyNameID sPropertyName;
            for (int rowIndex = 0; rowIndex < dgvPropertyList.RowCount; rowIndex++)
            {
                if (IsCellChecked(7, rowIndex))
                {
                    isAnyChecked = true;
                }
                try
                {
                    if (dgvPropertyList[2, rowIndex].Value != null)
                    {
                        propertyName = dgvPropertyList[2, rowIndex].Value.ToString();
                        propertyId = uint.Parse(dgvPropertyList[0, rowIndex].Value.ToString());
                        sPropertyName = new DNSPropertyNameID();
                        sPropertyName.strName = propertyName;
                        sPropertyName.uID = propertyId;

                        lstPropertiesNames.Add(sPropertyName);
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetPropertyName", McEx);
                }

            }

            if (isAnyChecked)
            {
                try
                {
                    m_CurrScheme.SetPropertyNames(lstPropertiesNames.ToArray());
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetPropertyNames", McEx);
                }
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrScheme.GetOverlayManager());
            }
            else
                ShowErrorToUser("Set Each Changed Names", "No Row Changed");
        }

        private void ShowErrorToUser(string title, string msg)
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSetEachChangedPropertyAsVarient_Click(object sender, EventArgs e)
        {

            DNSVariantProperty variantProp;
            bool isAnyChecked = false;

            for (int row = 0; row < dgvPropertyList.RowCount; row++)
            {
                object strNameValue = dgvPropertyList[2, row].Value;
                uint id = 0;
                if (IsCellChecked(7, row) /*&& (! || (strNameValue != null && strNameValue.ToString() != ""))*/)
                {
                    isAnyChecked = true;
                    variantProp = new DNSVariantProperty();
                    variantProp.eType = (DNEPropertyType)Enum.Parse(typeof(DNEPropertyType), dgvPropertyList[1, row].Value.ToString());
                    id = uint.Parse(dgvPropertyList[0, row].Value.ToString());
                    if ((!chxByNameIfExists.Checked) || (strNameValue == null || (strNameValue != null && strNameValue.ToString() == "")))
                        variantProp.uID = uint.Parse(dgvPropertyList[0, row].Value.ToString());
                    else if (chxByNameIfExists.Checked && strNameValue != null && strNameValue.ToString() != "")
                    {
                        variantProp.strName = dgvPropertyList[2, row].Value.ToString();
                        variantProp.uID = 0;
                    }
                    variantProp.Value = dgvPropertyList[3, row].Tag;

                    try
                    {
                        if (m_SenderObj is IDNMcObject)
                        {
                            IDNMcObject currObject = (IDNMcObject)m_SenderObj;
                            currObject.SetProperty(variantProp);
                        }
                        else
                        {
                            if(id != 0)
                                SetPropertyName(row, id);
                            m_CurrScheme.SetPropertyDefault(variantProp);
                        }
                        SetIsChanged(row, false);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("SetProperty", McEx);
                    }
                }
            }
            if (isAnyChecked)
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrScheme.GetOverlayManager());
            else
                ShowErrorToUser("Set Each Changed Property As Variant", "No Row Changed");

        }

        private void btnUpdatePropertiesAndLocationPoints_Click(object sender, EventArgs e)
        {
            IDNMcObject currObject = (IDNMcObject)m_SenderObj;

            List<DNSVariantProperty> changedPropertiesList = GetChangedProperties();
           
            if (changedPropertiesList.Count == 0)
            {
                ShowErrorToUser("Update Properties And Location Points" + (chxByNameIfExists.Checked ? " By Name" : ""), "No Row Changed" + (chxByNameIfExists.Checked ? " With Names" : ""));
                return;
            }
            frmUpdatePropertiesAndLocationPoints UpdatePropertiesAndLocationPointsForm = new frmUpdatePropertiesAndLocationPoints(currObject, changedPropertiesList);
            UpdatePropertiesAndLocationPointsForm.SetFrmPropertiesIDList(this);
            DialogResult result = UpdatePropertiesAndLocationPointsForm.ShowDialog();
            
        }

        public void SetIsChangedAllGrid(bool value)
        {
            for (int row = 0; row < dgvPropertyList.RowCount; row++)
            {
                SetIsChanged(row, value);
            }
        }

        private void btnGetPropertiesAndLocationPoints_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Get properties and location points will cause losing data\nWould you like to continue?", "Get properties and location points", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {

                try
                {
                    IDNMcObject currObject = (IDNMcObject)m_SenderObj;
                    DNSVariantProperty[] paProperties = new DNSVariantProperty[0];
                    DNSMcVector3D[] paLocationPoints = new DNSMcVector3D[0];
                    currObject.GetPropertiesAndLocationPoints(out paProperties, out paLocationPoints, ntxLocationIndex.GetUInt32());

                    List<DNSVariantProperty> listProperties = null;
                    if (paProperties != null)
                    {
                        listProperties = paProperties.ToList();
                        dgvPropertyList.Rows.Clear();
                        LoadObjectPropertyTable(listProperties);
                    }
                    
                    if (paLocationPoints != null)
                    {
                        frmUpdatePropertiesAndLocationPoints UpdatePropertiesAndLocationPointsForm = new frmUpdatePropertiesAndLocationPoints(currObject, listProperties, paLocationPoints);
                        UpdatePropertiesAndLocationPointsForm.ShowDialog();
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetPropertiesAndLocationPoints", McEx);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        public List<DNSVariantProperty> ChangedProperties
        {
            get { return GetChangedProperties(); }
        }

        private void btnCloseWithoutSave_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void btnGetPropertyIdByName_Click(object sender, EventArgs e)
        {
            try
            {
                string propertyName = txtPropertyName.Text;
                if (propertyName != "")
                {
                    uint propertyId = m_CurrScheme.GetPropertyIDByName(propertyName);
                    
                    if(propertyId != DNMcConstants._MC_EMPTY_ID)
                        llPropertyId.Text = propertyId.ToString();
                    else
                        MessageBox.Show("No ID was set for the name", "Incorrect Property name", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    MessageBox.Show("Please insert property name", "Property name is incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    llPropertyId.Text = "";
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetPropertyIDByName", McEx);
            }
        }

        private void llPropertyId_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            uint propertyId;
            if (UInt32.TryParse(llPropertyId.Text, out propertyId))
            {
                List<DNSPropertyNameIDType> lstPropertyIDs = new List<DNSPropertyNameIDType>();
                lstPropertyIDs.AddRange(m_ArrPropertyID);
                int index = lstPropertyIDs.FindIndex(x => x.uID == propertyId);
                if (index >= 0 && index < m_ArrPropertyID.Length)
                {
                    dgvPropertyList.ClearSelection();
                    dgvPropertyList.Rows[index].Selected = true;
                    dgvPropertyList.FirstDisplayedScrollingRowIndex = index;
                }
            }
        }

        private void btnGetProperties_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcObject currObject = (IDNMcObject)m_SenderObj;
                DNSVariantProperty[] paProperties = currObject.GetProperties();

                if (paProperties != null)
                {
                    dgvPropertyList.Rows.Clear();
                    LoadObjectPropertyTable(paProperties.ToList());
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetProperties", McEx);
            }
        }
    }
}
