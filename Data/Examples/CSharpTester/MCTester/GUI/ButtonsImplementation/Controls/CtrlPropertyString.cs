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
    public partial class CtrlPropertyString : CtrlPropertyBase
    {
        #region Private Members
        private int m_RegStringNum = -1;
        private int m_RegStringNumView = 0;

        private int m_SelStringNum = -1;
        private int m_SelStringNumView = 0;

        private List<string> m_lRegString;
        private List<string> m_lSelString;
        
        #endregion

        public CtrlPropertyString()
        {
            InitializeComponent();
            m_lRegString = new List<string>();
            //m_lRegString.Add("");
            m_lSelString = new List<string>();
            //m_lSelString.Add("");
        }

        #region Public Properties
        public string RegValueLable
        {
            get { return this.lblRegString.Text; }
            set { this.lblRegString.Text = value; }
        }

        public DNMcVariantString RegTextString
        {
            get
            {
                if (RegStringNum >= 0)
                    m_lRegString[RegStringNum] = txtRegString.Text;

                DNMcVariantString varString = new DNMcVariantString(m_lRegString.ToArray(), chxRegIsUnicode.Checked);
                return varString;
            }
            set
            {
                this.chxRegIsUnicode.Checked = value.bIsUnicode;

                if (value.astrStrings != null)
                {
                    if (value.astrStrings.Length > 0)
                    {
                        this.txtRegString.Text = value.astrStrings[0];
                        m_lRegString.AddRange(value.astrStrings);
                        RegStringNum++;
                    }
                    else
                    {
                        btnRegRemove.Enabled = false;
                        btnRegNextString.Enabled = false;
                        btnRegPrvString.Enabled = false;
                        txtRegString.Enabled = false;
                    }
                }
            }
        }

        public string RegPropertyString
        {
            get { return txtRegString.Text; }
            set { txtRegString.Text = value; }
        }

        public string SelValueLable
        {
            get { return this.lblSelString.Text; }
            set { this.lblSelString.Text = value; }
        }

        public DNMcVariantString SelTextString
        {
            get
            {
                if (SelStringNum >= 0)
                    m_lSelString[SelStringNum] = txtSelString.Text;

                DNMcVariantString varString = new DNMcVariantString(m_lSelString.ToArray(), chxSelIsUnicode.Checked);
                return varString;
            }
            set
            {
                this.chxSelIsUnicode.Checked = value.bIsUnicode;

                if (value.astrStrings != null)
                {
                    if (value.astrStrings.Length > 0)
                    {
                        this.txtSelString.Text = value.astrStrings[0];
                        m_lSelString.AddRange(value.astrStrings);
                        SelStringNum++;
                    }
                    else
                    {
                        btnSelRemove.Enabled = false;
                        btnSelNextString.Enabled = false;
                        btnSelPrvString.Enabled = false;
                        txtSelString.Enabled = false;
                    }
                }
            }
        }

        public string SelPropertyString
        {
            get { return txtSelString.Text; }
            set { txtSelString.Text = value; }
        }

        public bool IsVariantString
        {
            get
            {
                return chxRegIsUnicode.Enabled;
            }
            set
            {
                chxRegIsUnicode.Enabled = value;
                chxSelIsUnicode.Enabled = value;

                chxRegIsUnicode.Visible = value;
                chxSelIsUnicode.Visible = value;
            }
        }

        public int RegStringNum
        {
            get { return m_RegStringNum; }
            set
            {
                m_RegStringNum = value;
                m_RegStringNumView = m_RegStringNum + 1;
                lblRegStringNum.Text = m_RegStringNumView.ToString() + ":";

                if (m_RegStringNum < 0)
                    txtRegString.Text = "";
                else
                    txtRegString.Text = m_lRegString[m_RegStringNum];
            }
        }

        public int SelStringNum
        {
            get { return m_SelStringNum; }
            set
            {
                m_SelStringNum = value;
                m_SelStringNumView = m_SelStringNum + 1;
                lblSelStringNum.Text = m_SelStringNumView.ToString() + ":";

                if (m_SelStringNum < 0)
                    txtSelString.Text = "";
                else
                    txtSelString.Text = m_lSelString[m_SelStringNum];
            }
        }
        
        #endregion
        
        #region Privete Events

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);

            if (chxSelectionProperty.Checked == true)
            {
                if (m_lSelString.Count > 0)
                    txtSelString.Enabled = true;
                else
                    txtSelString.Enabled = false;                
            }
            else
            {
                if (m_lSelString != null)
                    m_lSelString.Clear();                    

                if (txtSelString != null)
                    txtSelString.Text = "";

                m_SelStringNum = -1;
                m_SelStringNumView = 0;
            }
        }

        private void btnRegAdd_Click(object sender, EventArgs e)
        {
            if (m_lRegString.Count == 0)
            {
                btnRegRemove.Enabled = true;
                btnRegNextString.Enabled = true;
                btnRegPrvString.Enabled = true;
                txtRegString.Enabled = true;
            }

            if (RegStringNum >= 0)
                m_lRegString[RegStringNum] = txtRegString.Text;            

            m_lRegString.Add("");
            RegStringNum = m_lRegString.Count - 1;
        }

        private void btnRegRemove_Click(object sender, EventArgs e)
        {
            m_lRegString.RemoveAt(RegStringNum);

            if (m_lRegString.Count == 0)
            {
                btnRegRemove.Enabled = false;
                btnRegNextString.Enabled = false;
                btnRegPrvString.Enabled = false;
                txtRegString.Enabled = false;

                RegStringNum--;
            }
            else
            {
                if (RegStringNum > m_lRegString.Count - 1)
                    RegStringNum --;
                else if (RegStringNum == m_lRegString.Count - 1)
                    RegStringNum -=0;
                    else
                        RegStringNum +=0;
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

        private void btnRegPrvString_Click(object sender, EventArgs e)
        {
            if (RegStringNum > 0)
            {
                m_lRegString[RegStringNum] = txtRegString.Text;
                RegStringNum--;
            }
        }

        

        private void btnSelAdd_Click(object sender, EventArgs e)
        {
            if (m_lSelString.Count == 0)
            {
                btnSelRemove.Enabled = true;
                btnSelNextString.Enabled = true;
                btnSelPrvString.Enabled = true;
                txtSelString.Enabled = true;
            }

            if (SelStringNum >= 0)
                m_lSelString[SelStringNum] = txtSelString.Text;

            m_lSelString.Add("");
            SelStringNum = m_lSelString.Count - 1;
        }

        private void btnSelRemove_Click(object sender, EventArgs e)
        {
            m_lSelString.RemoveAt(SelStringNum);

            if (m_lSelString.Count == 0)
            {
                btnSelRemove.Enabled = false;
                btnSelNextString.Enabled = false;
                btnSelPrvString.Enabled = false;
                txtSelString.Enabled = false;

                SelStringNum--;
            }
            else
            {
                if (SelStringNum > m_lSelString.Count - 1)
                    SelStringNum--;
                else if (SelStringNum == m_lSelString.Count - 1)
                    SelStringNum -= 0;
                else
                    SelStringNum += 0;
            }
        }

        private void btnSelNextString_Click(object sender, EventArgs e)
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

        #endregion

        private void chxRegIsUnicode_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lblRegStringNum_Click(object sender, EventArgs e)
        {

        }

        private void txtRegString_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
