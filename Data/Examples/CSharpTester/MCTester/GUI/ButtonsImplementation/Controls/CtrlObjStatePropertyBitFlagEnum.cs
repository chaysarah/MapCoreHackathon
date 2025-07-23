using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCTester.Controls
{
    public partial class CtrlObjStatePropertyBitFlagEnum : CtrlObjStatePropertyDataHandlerInt
    {
        private Type m_EnumType = null;
        //private string m_enumType;
        //bool m_blockEvent = false;

        public CtrlObjStatePropertyBitFlagEnum()
        {
            InitializeComponent();
        }

        public void SetEnumList(Type enumType)
        {
            lstRegEnum.Items.Clear();
            lstSelEnum.Items.Clear();
            
            m_EnumType = enumType;

            lstRegEnum.Items.AddRange(Enum.GetNames(enumType));
            lstSelEnum.Items.AddRange(Enum.GetNames(enumType));
        }

        private int GetEnumVal(CheckedListBox lstEnum)
        {
            int currValue = 0;
            for (int i = 0; i < lstEnum.Items.Count; i++)
            {
                if (lstEnum.GetItemChecked(i))
                {
                    currValue += (int)Enum.Parse(m_EnumType, lstEnum.Items[i].ToString());
                }
            }

            return currValue;
        }

        private void SetEnumVal(CheckedListBox lstEnum, int value)
        {
            int checkedValue = (int)value;
            //m_blockEvent = true;
            for (int i = 0; i < lstEnum.Items.Count; i++)
            {
                int currEnum = (int)Enum.Parse(m_EnumType, lstEnum.Items[i].ToString());
                if (currEnum == 0 && checkedValue == 0)
                {
                    lstEnum.SetItemChecked(i, true);
                }
                else
                {
                    lstEnum.SetItemChecked(i, (currEnum & checkedValue) != 0);
                }
            }
            //m_blockEvent = false;

        }

        public int RegEnumVal
        {
            get
            {
                return GetEnumVal(lstRegEnum);
            }
            set
            {
                SetEnumVal(lstRegEnum, value);
            }
        }


        public int SelEnumVal
        {
            get
            {
                return GetEnumVal(lstSelEnum);
            }
            set
            {
                SetEnumVal(lstSelEnum, value);
            }
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            //if (bInitCtrl)
            //    SelEnumVal = m_InitValue;
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelEnumVal);

            if (IsExistObjectState(objectState))
            {
                SelEnumVal = GetSelValByObjectState(objectState);
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegEnumVal);
        }

        public new void Load(delegateLoad func)
        {
            SelEnumVal = m_InitValue;
            RegEnumVal = base.Load(func);
        }

    }

}
