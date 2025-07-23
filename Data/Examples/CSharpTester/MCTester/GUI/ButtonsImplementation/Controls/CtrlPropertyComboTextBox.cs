using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Controls
{
    public partial class CtrlPropertyComboTextBox : CtrlPropertyBase 
    {
        private Type m_RegEnumType = null;
        private Type m_SelEnumType = null;
        private string m_EnumType = null;
        private Type m_tEnumType;
        private Array m_arrEnumValue;

        public Array ArrEnumValue
        {
            get
            {
                if (m_arrEnumValue == null && TEnumType != null)
                    m_arrEnumValue = Enum.GetValues(TEnumType);
                return m_arrEnumValue;
            }
            set { m_arrEnumValue = value; }
        }


        public CtrlPropertyComboTextBox()
        {
            InitializeComponent();
        }

        public uint RegUintVal
        {
            get { return ntxRegValue.GetUInt32(); }
            set { ntxRegValue.SetUInt32(value); }
        }

        public string RegValueLable
        {
            get { return this.lblRegValue.Text; }
            set { this.lblRegValue.Text = value; }
        }

        public uint SelUintVal
        {
            get { return ntxSelValue.GetUInt32(); }
            set { ntxSelValue.SetUInt32(value); }
        }

        public string SelValueLable
        {
            get { return this.lblSelValue.Text; }
            set { this.lblSelValue.Text = value; }
        }


        public void SetEnumList(Type enumType)
        {
            cmbRegEnum.Items.Clear();
            cmbSelEnum.Items.Clear();

            m_RegEnumType = enumType;
            m_SelEnumType = enumType;
            
            TEnumType = enumType;
            if (enumType != null)
            {
                ArrEnumValue = Enum.GetValues(enumType);
            }
            
            cmbRegEnum.Items.AddRange(Enum.GetNames(enumType));
            cmbSelEnum.Items.AddRange(Enum.GetNames(enumType));

            if (cmbRegEnum.Items.Count > 0)
                cmbRegEnum.Text = cmbRegEnum.Items[0].ToString();

            if (cmbSelEnum.Items.Count > 0)
                cmbSelEnum.Text = cmbSelEnum.Items[0].ToString();
        }

        public Type TEnumType
        {
            get
            {
                return m_tEnumType;
            }
            set
            {
                m_tEnumType = value;
            }
        }

        public string EnumType
        {
            get
            {
                if (m_EnumType == null)
                    m_EnumType = "";
                return m_EnumType;
            }
            set
            {
                m_EnumType = value;
            }
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }

        private void cmbRegEnum_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetValueInTextBox(ntxRegValue, cmbRegEnum);
        }

        private void ntxRegValue_TextChanged(object sender, EventArgs e)
        {
            SelectValueInCombo(ntxRegValue, cmbRegEnum);
        }

        private void cmbSelEnum_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetValueInTextBox(ntxSelValue, cmbSelEnum);
        }

        private void ntxSelValue_TextChanged(object sender, EventArgs e)
        {
            SelectValueInCombo(ntxSelValue, cmbSelEnum);
        }

        private void SelectValueInCombo(NumericTextBox numTextBox, ComboBox comboBox)
        {
            //int returnValue = -1;
            uint value = numTextBox.GetUInt32();
            if (numTextBox.Text == "" && comboBox.SelectedIndex == -1)
                return;
            else if (comboBox.SelectedIndex > -1)
            {
                if (value == (uint)ArrEnumValue.GetValue(comboBox.SelectedIndex))
                    return;
                else
                {
                    int index = 0;
                    foreach (object arrValue in ArrEnumValue)
                    {
                        if (value == (uint)arrValue)
                        {
                            comboBox.SelectedIndex = index;
                            return;
                        }
                        index++;
                    }
                }
            }
        }

        private void SetValueInTextBox(NumericTextBox numTextBox, ComboBox comboBox)
        {
            if (comboBox.SelectedIndex > -1)
            {
                uint value = (uint)ArrEnumValue.GetValue(comboBox.SelectedIndex);
                numTextBox.SetUInt32(value);
            }
            else
            {
                numTextBox.Text = "";
            }
        }
    }
}
