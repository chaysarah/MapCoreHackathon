using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.Managers.ObjectWorld;

namespace MCTester.Controls
{
   public partial class CtrlObjStatePropertyIntArray : CtrlObjStatePropertyDataHandlerIntArray
    {
        public CtrlObjStatePropertyIntArray()
        {
            InitializeComponent();
        }

        public bool IsSeperateWithTwiceSpace
        {
            get{ return RegNumberArray.IsSeperateWithTwiceSpace; }
            set
            {
                RegNumberArray.IsSeperateWithTwiceSpace = value;
                SelNumberArray.IsSeperateWithTwiceSpace = value;
            }
        }

        public int SeperateNumbers { get; set; }

        public int mNumColumns { get; set; }

        public string mHeaderTextCol1 { get; set; }
        public string mHeaderTextCol2 { get; set; }
        public string mHeaderTextCol3 { get; set; }

        public DNSArrayProperty<int> RegNumberArrayPropertyValue
        {
            get
            {
                return RegNumberArray.IntArrayPropertyValue;
            }
            set
            {
                RegNumberArray.IntArrayPropertyValue = value;
            }
        }

        public DNSArrayProperty<int> SelNumberArrayPropertyValue
        {
            get
            {
                return SelNumberArray.IntArrayPropertyValue;
            }
            set
            {
                SelNumberArray.IntArrayPropertyValue = value;
            }
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            if (bInitCtrl)
                SelNumberArrayPropertyValue = new DNSArrayProperty<int>();
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelNumberArrayPropertyValue);

            if (IsExistObjectState(objectState))
            {
                SelNumberArrayPropertyValue = GetSelValByObjectState(objectState);
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegNumberArrayPropertyValue);
        }

        public new void Load(delegateLoad func)
        {
            RegNumberArray.SetHeadersColumns(mNumColumns, mHeaderTextCol1, mHeaderTextCol2, mHeaderTextCol3);
            SelNumberArray.SetHeadersColumns(mNumColumns, mHeaderTextCol1, mHeaderTextCol2, mHeaderTextCol3);

            SelNumberArrayPropertyValue = m_InitValue;
            RegNumberArrayPropertyValue = base.Load(func);
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegNumberArray.IntArrayPropertyValue, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelNumberArray.IntArrayPropertyValue, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_INT_ARRAY; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegNumberArrayPropertyValue = (DNSArrayProperty<int>)value;
            if (SelPropertyID == propertyId)
                SelNumberArrayPropertyValue = (DNSArrayProperty<int>)value;
            SetStateValueByProperty(propertyId, (DNSArrayProperty<int>)value);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void RegNumberArray_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegNumberArrayPropertyValue, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                if (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None)
                {
                    AfterValidationSucceed();
                }
                else // user click cancel
                {
                    e.Cancel = true;
                }
            }
        }

        private void SelNumberArray_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelNumberArrayPropertyValue, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                if (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None)
                {
                    AfterValidationSucceed();
                }
                else // user click cancel
                {
                    e.Cancel = true;
                }
            }
        }

        
    }

    public class CtrlObjStatePropertyDataHandlerIntArray : CtrlObjStatePropertyDataHandler<DNSArrayProperty<int>> { }
}
