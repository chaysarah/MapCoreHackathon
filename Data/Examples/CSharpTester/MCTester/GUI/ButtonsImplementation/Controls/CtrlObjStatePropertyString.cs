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
    public partial class CtrlObjStatePropertyString : CtrlObjStatePropertyDataHandlerTextString
    {
        DNSByteBool currState = false;
  
        public CtrlObjStatePropertyString()
        {
            InitializeComponent();   
            
            m_InitValue = new DNMcVariantString();
        }

        private DNMcVariantString GetStringVal(int stringNum, List<string> lString, string text, bool isUnicode)
        {
            if (stringNum >= 0 && stringNum < lString.Count)
                lString[stringNum] = text;
            DNMcVariantString varString = new DNMcVariantString(lString.ToArray(), isUnicode);
            return varString;
        }

      /*  private void SetStringVal(DNMcVariantString value, CheckBox IsUnicode, TextBox textBox, List<string> lString, List<Control> lButton, bool isReg)
        {
            IsUnicode.Checked = value.bIsUnicode;

            if (value.astrStrings != null)
            {
                if (value.astrStrings.Length > 0)
                {
                    EnableControls(lButton, true);
                    textBox.Text = value.astrStrings[0];
                    lString.Clear();
                    lString.AddRange(value.astrStrings);
                    if (isReg)
                        RegStringNum = 0;
                    else
                        SelStringNum = 0;
                }
                else
                {
                    textBox.Text = "";
                    SelStringNum = -1;
                    EnableControls(lButton, false);
                }
            }
            else
                EnableControls(lButton, false);
        }
*/

            
        public DNMcVariantString RegStringVal
        {
            get
            {
                return ctrlRegString.StringVal;
            }
            set
            {
                ctrlRegString.StringVal = value;
            }
        }

        public DNMcVariantString SelStringVal
        {
            get
            {
                return ctrlSelString.StringVal;
            }
            set
            {
                ctrlSelString.StringVal = value;
            }
        }


      /*  private void SetStringNum(int stringNum,Label label,TextBox text,List<string> lString)
        {
            int m_RegStringNumView = stringNum + 1;
            label.Text = m_RegStringNumView.ToString() + ":";

            if (stringNum < 0)
                text.Text = "";
            else
                text.Text = lString[stringNum];

        }
       
       public int StringNum
        {
            get { return m_RegStringNum; }
            set
            {
                m_RegStringNum = value;

                SetStringNum(m_RegStringNum, lblRegStringNum, txtRegString, m_lRegString);
            }
        }      

        public int SelStringNum
        {
            get { return m_SelStringNum; }
            set
            {
                m_SelStringNum = value;
                SetStringNum(m_SelStringNum, lblSelStringNum, txtSelString, m_lSelString);
            }
        }
         */

        //public DNMcVariantString SelStringVal
        //{
        //    get { return ntbSelProperty.GetByte(); }
        //    set { ntbSelProperty.SetByte(value); }
        //}


       /* public bool IsVariantString
        {
            get
            {
                return ctrlRegString.IsVariantString;
            }
            set
            {
                ctrlRegString.IsVariantString = value;
            }
        }*/

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            ctrlSelString.InitControl(m_InitValue, bInitCtrl);
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, ctrlSelString.StringVal);

            if (IsExistObjectState(objectState))
            {
                ctrlSelString.StringVal = GetSelValByObjectState(objectState);
            }
            //if (SelStringVal.astrStrings == null || SelStringVal.astrStrings.Length == 0)
            //    EnableControls(m_lSelControl, false);
            
        }

        public void Save(delegateSave func)
        {
            base.Save(func, ctrlRegString.StringVal);
        }

        public new void Load(delegateLoad func)
        {
            SelStringVal = m_InitValue;
            RegStringVal = base.Load(func);
        }

        private void EnableControls(List<Control> lControl, bool isEnabled)
        {
            foreach (Control ctrl in lControl)
                ctrl.Enabled = isEnabled;
          ctrlSelString.lblStringNum.Text = "0";
        }

        private void btnStringAdd_Click(List<string> lString, List<Control> lControl, int stringNum, TextBox textBox)
        {
            if (lString.Count == 0)
            {
                EnableControls(lControl, true);
            }

            if (stringNum >= 0)
                lString[stringNum] = textBox.Text;

            lString.Add("");
        }

        /*private void btnRegStringAdd_Click(object sender, EventArgs e)
        {
            btnStringAdd_Click(m_lRegString, m_lRegControl, RegStringNum, txtRegString);
            RegStringNum = m_lRegString.Count - 1;
        }*/

       /* private void btnSelStringAdd_Click(object sender, EventArgs e)
        {
            btnStringAdd_Click(m_lSelString, m_lSelControl, ctrlSelString.StringNum, ctrlSelString.txtSelString);
            ctrlSelString.StringNum = m_lSelString.Count - 1;
        }
*/

 /*       private void btnRegStringRemove_Click(object sender, EventArgs e)
        {
            m_lRegString.RemoveAt(RegStringNum);

            if (m_lRegString.Count == 0)
            {
                EnableControls(m_lRegControl, false);

                RegStringNum--;
            }
            else if (RegStringNum > m_lRegString.Count - 1)
                RegStringNum--;
            else
                RegStringNum -= 0;

            ValidateChildren();
        }

        private void btnRegPrvString_Click(object sender, EventArgs e)
        {
            if (RegStringNum > 0)
            {
                m_lRegString[RegStringNum] = txtRegString.Text;
                RegStringNum--;
            }
        }

        private void btnRegNextString_Click(object sender, EventArgs e)
        {
            if (RegStringNum < m_lRegString.Count - 1)
            {
                m_lRegString[RegStringNum] = txtRegString.Text;
                RegStringNum++;
            }
        }     
*/

        /*private void btnSelNextString_Click(object sender, EventArgs e)
        {
            if (SelStringNum < m_lSelString.Count - 1)
            {
                m_lSelString[SelStringNum] = txtSelString.Text;
                SelStringNum++;
            }
        }

        private void btnSelPrvString_Click(object sender, EventArgs e)
        {
            if (SelStringNum > 0)
            {
                m_lSelString[SelStringNum] = txtSelString.Text;
                SelStringNum--;
            }
        }      

        private void btnSelStringRemove_Click(object sender, EventArgs e)
        {
            m_lSelString.RemoveAt(SelStringNum);

            if (m_lSelString.Count == 0)
            {
                EnableControls(m_lSelControl, false);

                SelStringNum--;
            }
            else if(SelStringNum > m_lSelString.Count - 1)
                    SelStringNum--;
            else
                SelStringNum -= 0;

            ValidateChildren();
        }
*/

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegStringVal, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelStringVal, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_STRING; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegStringVal = (DNMcVariantString)value;
            if (SelPropertyID == propertyId)
                SelStringVal = (DNMcVariantString)value;
            SetStateValueByProperty(propertyId, (DNMcVariantString)value);
        }

        private void ctrlRegString_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegStringVal, this, new DNSByteBool(0));
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

        private void ctrlSelString_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelStringVal, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerTextString : CtrlObjStatePropertyDataHandler<DNMcVariantString> { }
}
