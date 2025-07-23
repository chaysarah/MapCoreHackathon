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
    public partial class CtrlPropertyEnum :CtrlPropertyBase
    {
        private Type m_RegEnumType = null;
        private Type m_SelEnumType = null;
        private string m_EnumType = null;

        public CtrlPropertyEnum()
        {
            InitializeComponent();
        }

        public void SetEnumList(Type enumType)
        {
            cmbRegEnum.Items.Clear();
            cmbSelEnum.Items.Clear();

            m_RegEnumType = enumType;
            m_SelEnumType = enumType;

            cmbRegEnum.Items.AddRange(Enum.GetNames(enumType));
            cmbSelEnum.Items.AddRange(Enum.GetNames(enumType));


            if (cmbRegEnum.Items.Count > 0)
                cmbRegEnum.Text = cmbRegEnum.Items[0].ToString();

            if (cmbSelEnum.Items.Count > 0)
                cmbSelEnum.Text = cmbSelEnum.Items[0].ToString();
        }

        public object RegEnumVal
        {
            get
            {
                if (cmbRegEnum.SelectedItem != null)
                    return Enum.Parse(m_RegEnumType, cmbRegEnum.SelectedItem.ToString());
                else
                    return 0;
            }
            set
            {
                if (m_RegEnumType != null && cmbRegEnum.Items.Count > 0)
                    cmbRegEnum.Text = Enum.GetName(m_RegEnumType, value);
            }
        }

        public string RegEnumLable
        {
            get { return this.lblRegEnum.Text; }
            set { this.lblRegEnum.Text = value; }
        }

        public object SelEnumVal
        {
            get
            {
                if (cmbSelEnum.SelectedItem != null)
                    return Enum.Parse(m_SelEnumType, cmbSelEnum.SelectedItem.ToString());
                else
                    return 0;
            }
            set
            {
                if (m_SelEnumType != null && cmbSelEnum.Items.Count > 0)
                    cmbSelEnum.Text = Enum.GetName(m_SelEnumType, value);
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

        public string SelEnumLable
        {
            get { return this.lblSelEnum.Text; }
            set { this.lblSelEnum.Text = value; }
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }
    }
}
