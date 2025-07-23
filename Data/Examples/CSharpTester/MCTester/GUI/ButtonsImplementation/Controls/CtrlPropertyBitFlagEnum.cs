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
    public partial class CtrlPropertyBitFlagEnum : CtrlPropertyBase
    {
        private Type m_RegEnumType = null;
        private Type m_SelEnumType = null;
        //private string m_enumType;
        //bool m_blockEvent = false;

        public CtrlPropertyBitFlagEnum()
        {
            InitializeComponent();
        }


        public void SetEnumList(Type enumType)
        {
            lstRegEnum.Items.Clear();
            lstSelEnum.Items.Clear();
            m_SelEnumType = enumType ;
            m_RegEnumType = enumType;

            lstRegEnum.Items.AddRange(Enum.GetNames(enumType));
            lstSelEnum.Items.AddRange(Enum.GetNames(enumType));
        }

        public object RegEnumVal
        {
            get
            {
                if (m_RegEnumType == null)
                {
                    return null;
                }

                int currValue = 0;
                for (int i=0; i<lstRegEnum.Items.Count; i++)
                {
                    if (lstRegEnum.GetItemChecked(i))
                    {
                        currValue += (int)Enum.Parse(m_RegEnumType, lstRegEnum.Items[i].ToString());
                    }
                }

                return currValue;
            }
            set
            {
                if (m_RegEnumType != null)
                {
                    int checkedValue = (int)value;
                    //m_blockEvent = true;
                    for (int i = 0; i < lstRegEnum.Items.Count; i++)
                    {
                        int currEnum = (int)Enum.Parse(m_RegEnumType, lstRegEnum.Items[i].ToString());
                        if (currEnum == 0 && checkedValue == 0)
                        {
                            lstRegEnum.SetItemChecked(i, true);
                        }
                        else
                        {
                            lstRegEnum.SetItemChecked(i, (currEnum & checkedValue) != 0);
                        }
                    }
                    //m_blockEvent = false;
                }
            }
        }


        public object SelEnumVal
        {
            get
            {
                if (m_SelEnumType == null)
                {
                    return null;
                }

                int currValue = 0;
                for (int i = 0; i < lstSelEnum.Items.Count; i++)
                {
                    if (lstSelEnum.GetItemChecked(i))
                    {
                        currValue += (int)Enum.Parse(m_SelEnumType, lstSelEnum.Items[i].ToString());
                    }
                }

                return currValue;
            }
            set
            {
                if (m_SelEnumType != null)
                {
                    int checkedValue = (int)value;
                    //m_blockEvent = true;
                    for (int i = 0; i < lstSelEnum.Items.Count; i++)
                    {
                        int currEnum = (int)Enum.Parse(m_SelEnumType, lstSelEnum.Items[i].ToString());
                        if (currEnum == 0 && checkedValue == 0)
                        {
                            lstSelEnum.SetItemChecked(i, true);
                        }
                        else
                        {
                            lstSelEnum.SetItemChecked(i, (currEnum & checkedValue) != 0);
                        }
                    }
                    //m_blockEvent = false;
                }
            }
        }

        public string RegEnumLable
        {
            get { return this.lblRegEnum.Text; }
            set { this.lblRegEnum.Text = value; }
        }

        public string SelEnumLable
        {
            get { return this.lblSelEnum.Text; }
            set { this.lblSelEnum.Text = value; }
        }

       

        private void chxSelectionProperty_CheckedChanged(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }

    }
}
