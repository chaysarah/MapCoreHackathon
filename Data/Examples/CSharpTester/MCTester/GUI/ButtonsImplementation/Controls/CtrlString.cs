using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;

namespace MCTester.Controls
{
    public partial class CtrlString : UserControl
    {
        //uint m_currNumString = 0;

        private List<string> m_lString;
        private int m_StringNum = -1;
        private List<Control> m_lControl;
        private bool m_IsExistNewLine = false;

        public CtrlString()
        {
            InitializeComponent();

            m_lString = new List<string>();
            m_lControl = new List<Control>();

            m_lControl.Add(btnRemove);
            m_lControl.Add(btnNextString);
            m_lControl.Add(btnPrvString);
            m_lControl.Add(txtString);
        }

        private void SetStringNum(int stringNum, Label label, TextBox text, List<string> lString)
        {
            int m_StringNumView = stringNum + 1;
            label.Text = m_StringNumView.ToString() + ":";


            if (stringNum < 0)
                text.Text = "";
            else
            {
                m_IsExistNewLine = lString.Any(x => x.Contains("\n") && !x.Contains("\r\n"));
                if (m_IsExistNewLine)
                {
                    for (int i = 0; i < lString.Count; i++)
                    {
                        lString[i] = lString[i].Replace("\n", "\r\n").Replace("\r\r", "\r");
                    }
                }
                text.Text = lString[stringNum];
            }
        }

        public int StringNum
        {
            get { return m_StringNum; }
            set
            {
                m_StringNum = value;

                SetStringNum(m_StringNum, lblStringNum, txtString, m_lString);
            }
        }

        private void btnNextString_Click(object sender, EventArgs e)
        {
            if (StringNum < m_lString.Count - 1)
            {
                m_lString[StringNum] = txtString.Text;
                StringNum++;
            }
        }

        private void btnPrvString_Click(object sender, EventArgs e)
        {
            if (StringNum > 0)
            {
                m_lString[StringNum] = txtString.Text;
                StringNum--;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnStringAdd_Click(m_lString, m_lControl, StringNum, txtString);
            StringNum = m_lString.Count - 1;
        }

        public string TextValue
        {
            get { return txtString.Text; }
            set { txtString.Text = value; }
        }

        public DNMcVariantString StringVal
        {
            get
            {
                return GetStringVal(StringNum, m_lString, TextValue, chxIsUnicode.Checked);
            }
            set
            {
                SetStringVal(value, chxIsUnicode, txtString, m_lString, m_lControl, true);

            }
        }

        private DNMcVariantString GetStringVal(int stringNum, List<string> lString, string text, bool isUnicode)
        {
            if (stringNum >= 0 && stringNum < lString.Count)
                lString[stringNum] = text;
            if (m_IsExistNewLine)
            {
                for (int i = 0; i < lString.Count; i++)
                {
                    lString[i] = lString[i].Replace("\r\n", "\n");
                }
            }
            DNMcVariantString varString = new DNMcVariantString(lString.ToArray(), isUnicode);
            return varString;
        }

        private void SetStringVal(DNMcVariantString value, CheckBox IsUnicode, TextBox textBox, List<string> lString, List<Control> lButton, bool isReg)
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
                        StringNum = 0;
                }
                else
                {
                    textBox.Text = "";
                    StringNum = -1;
                    EnableControls(lButton, false);
                }
            }
            else
                EnableControls(lButton, false);
        }

        public bool IsVariantString
        {
            get
            {
                return chxIsUnicode.Enabled;
            }
            set
            {
                chxIsUnicode.Enabled = value;

                chxIsUnicode.Visible = value;
            }
        }

        public void InitControl(DNMcVariantString initValue, bool bInitCtrl)
        {
            if (bInitCtrl)
            {
                StringVal = initValue;
                m_lString = new List<string>();
                StringNum = -1;
            }
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

        private void EnableControls(List<Control> lControl, bool isEnabled)
        {
            foreach (Control ctrl in lControl)
                ctrl.Enabled = isEnabled;
            lblStringNum.Text = "0";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            m_lString.RemoveAt(StringNum);

            if (m_lString.Count == 0)
            {
                EnableControls(m_lControl, false);

                StringNum--;
            }
            else if (StringNum > m_lString.Count - 1)
                StringNum--;
            else
                StringNum -= 0;

            ValidateChildren();
        }

    }
}
