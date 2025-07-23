using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;

namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    public partial class frmNumericPropertyType : frmBasePropertyType
    {
        string[] mEnumNames = null;
        Array mArrayEnumValues;
        private bool mIsLoadForm = false;
        private bool mIsCanNumber = false;
        private string mEnumName;
        private Type mEnumType;

        public frmNumericPropertyType()
            : base()
        {
            InitializeComponent();
        }

        public frmNumericPropertyType(uint id) : base(id)
        {
            base.ID = id;
            InitializeComponent();
        }

        public frmNumericPropertyType(uint id, object mcdnObject) : base(id)
        {
            base.ID = id;
            InitializeComponent();

            MCTPrivatePropertiesData.GetEnumValuesByPropertyId(mcdnObject, id, out mEnumNames, out mArrayEnumValues, out mIsCanNumber, out mEnumName, out mEnumType);
            mIsLoadForm = true;
            if (mEnumNames != null && mEnumNames.Length > 0)
            {
                if (!mEnumName.ToLower().Contains("flags")) // came from byte
                {
                    cmbEnumOptions.Visible = true;
                    cmbEnumOptions.Items.AddRange(mEnumNames);
                    if (!mIsCanNumber)
                        ntxPropertyValue.Visible = false;
                }
                else   // flags (DNEBoundingBoxPointFlags or DNETargetTypesFlags  )
                {
                    lstEnumFlags.Visible = true;
                    ntxPropertyValue.Visible = cmbEnumOptions.Visible = false;
                    lstEnumFlags.Items.AddRange(mEnumNames);

                    
                }
            }
            mIsLoadForm = false;
        }

        public string PropertyUintValueAsText;
        

        public uint PropertyUintValue
        {
            get
            {
                PropertyUintValueAsText = "";
                if (mEnumName.ToLower().Contains("flags"))
                {
                    uint currValue = 0;
                    for (int i = 0; i < lstEnumFlags.Items.Count; i++)
                    {
                        if (lstEnumFlags.GetItemChecked(i))
                        {
                            currValue +=(uint)((int)Enum.Parse(mEnumType, lstEnumFlags.Items[i].ToString()));
                            PropertyUintValueAsText = lstEnumFlags.Items[i].ToString() + ",";
                        }
                        if (PropertyUintValueAsText != "")
                            PropertyUintValueAsText = PropertyUintValueAsText.TrimEnd(',');
                    }

                    return currValue;
                }
                else
                    return ntxPropertyValue.GetUInt32();
            }
            set
            {
                if (mEnumName.ToLower().Contains("flags"))
                {
                    if (mEnumType != null)
                    {
                        int checkedValue = (int)value;
                        for (int i = 0; i < mEnumNames.Length; i++)
                        {
                            int currEnum = (int)Enum.Parse(mEnumType, mEnumNames[i]);
                            if (currEnum == 0 && checkedValue == 0)
                            {
                                lstEnumFlags.SetItemChecked(i, true);
                            }
                            else
                            {
                                lstEnumFlags.SetItemChecked(i, (currEnum & checkedValue) != 0);
                            }
                        }
                    }
                }
                else
                {
                    ntxPropertyValue.SetUInt32(value);
                }
            }
        }

        public int PropertyIntValue
        {
            get
            {
                return ntxPropertyValue.GetInt32();
            }
            set
            {
                ntxPropertyValue.SetInt(value);
            }
        }

        public double PropertyDoubleValue
        {
            get
            {
                return ntxPropertyValue.GetDouble();
            }
            set
            {
                ntxPropertyValue.SetDouble(value);
            }
        }

        public float PropertyFloatValue
        {
            get
            {
                return ntxPropertyValue.GetFloat();
            }
            set
            {
                ntxPropertyValue.SetFloat(value);
            }
        }

        public byte PropertyByteValue
        {
            get
            {
                return ntxPropertyValue.GetByte();
            }
            set
            {
                ntxPropertyValue.SetByte(value);
            }
        }

        public sbyte PropertySByteValue
        {
            get
            {
                return ntxPropertyValue.GetSByte();
            }
            set
            {
                ntxPropertyValue.SetSByte(value);
            }
        }

        private void SelectValueInCombo()
        {
            uint value = ntxPropertyValue.GetUInt32();
            if (ntxPropertyValue.Text == "" && cmbEnumOptions.SelectedIndex == -1)
                return;
            else if (mArrayEnumValues == null)
                return;
            else
            {
                if (cmbEnumOptions.SelectedIndex >= 0 && cmbEnumOptions.Text != "")
                {
                    try
                    {
                        if (value == (int)mArrayEnumValues.GetValue(cmbEnumOptions.SelectedIndex))
                            return;
                    }
                    catch (InvalidCastException)
                    {
                        if (value == (uint)mArrayEnumValues.GetValue(cmbEnumOptions.SelectedIndex))
                            return;
                    }
                }
                int index = MCTPrivatePropertiesData.GetEnumIndexInArray(mArrayEnumValues, value);
                if (index != -1)
                    cmbEnumOptions.SelectedIndex = index;
                else
                {
                    cmbEnumOptions.Text = "";
                    if (!mIsCanNumber)
                    {
                        MessageBox.Show("Invalid value - should be from enum options", "Invalid Value", 
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        ntxPropertyValue.Focus();
                    }

                }

            }
        }

       

        private void SetValueInTextBox()
        {
            if (cmbEnumOptions.SelectedIndex > -1)
            {
                try
                {
                    int value = (int)mArrayEnumValues.GetValue(cmbEnumOptions.SelectedIndex);
                    ntxPropertyValue.SetInt(value);
                }
                catch (InvalidCastException)
                {
                    uint value = (uint)mArrayEnumValues.GetValue(cmbEnumOptions.SelectedIndex);
                    ntxPropertyValue.SetUInt32(value);
                }
            }
            else
            {
                ntxPropertyValue.Text = "";
            }
        }


        private void cmbEnumOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetValueInTextBox();
        }

        private void ntxPropertyValue_TextChanged(object sender, EventArgs e)
        {
            if (!mIsLoadForm)
                SelectValueInCombo();
        }

    }
}