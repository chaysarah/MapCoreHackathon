using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;
using MCTester.Managers.ObjectWorld;

namespace MCTester.Controls
{
    public partial class CtrlSymbologySymbolID : UserControl
    {

        char[] m_App6D_DummySIDC = { 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X' };
        char[] m_2525C_DummySIDC = { 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X' };
        string m_2525_prefix = "METOC";
        public event EventHandler SymbolIDUpdated;
        public event EventHandler SymbologyStandardUpdated;

        public CtrlSymbologySymbolID()
        {
            InitializeComponent();
            cmbSymbologyStandard.Items.AddRange(Enum.GetNames(typeof(DNESymbologyStandard)));
            cmbSymbologyStandard.Text = DNESymbologyStandard._ESS_APP6D.ToString();
        }

        private void FunctionThatRaisesEvent()
        {
            //Null check makes sure the main page is attached to the event
            if (SymbolIDUpdated != null)
                SymbolIDUpdated(this, new EventArgs());
            if (SymbologyStandardUpdated != null)
                SymbologyStandardUpdated(this, new EventArgs());
        }

        private void txtSymbolID_TextChanged(object sender, EventArgs e)
        {
            CalculatetFullSymbolID();
        }

        private void CalculatetFullSymbolID()
        {
            DNESymbologyStandard symbologyStandard = GetSymbologyStandard();
            if (symbologyStandard == DNESymbologyStandard._ESS_APP6D)
            {
                if (txtSymbolID.Text.Length == 8)
                {
                    if (txtFullSymbolID.Text.Length == 30)
                    {
                        m_App6D_DummySIDC = txtFullSymbolID.Text.ToString().ToCharArray();
                    }
                    char[] userIdArr = txtSymbolID.Text.ToCharArray();
                    if (userIdArr.Length == 8)
                    {
                        m_App6D_DummySIDC[4] = userIdArr[0];
                        m_App6D_DummySIDC[5] = userIdArr[1];
                        m_App6D_DummySIDC[10] = userIdArr[2];
                        m_App6D_DummySIDC[11] = userIdArr[3];
                        m_App6D_DummySIDC[12] = userIdArr[4];
                        m_App6D_DummySIDC[13] = userIdArr[5];
                        m_App6D_DummySIDC[14] = userIdArr[6];
                        m_App6D_DummySIDC[15] = userIdArr[7];
                    }
                    txtFullSymbolID.Text = new string(m_App6D_DummySIDC);

                }
                else if (txtSymbolID.Text.Length == 30)
                {
                    txtFullSymbolID.Text = txtSymbolID.Text;
                }
            }
            else if (symbologyStandard == DNESymbologyStandard._ESS_2525C)
            {
                if (txtSymbolID.Text.StartsWith(m_2525_prefix))
                {
                    txtFullSymbolID.Text = txtSymbolID.Text;
                }
                else if (txtSymbolID.Text.Length == 8)
                {
                    if (txtFullSymbolID.Text.Length == 23)
                    {
                        m_2525C_DummySIDC = txtFullSymbolID.Text.ToString().ToCharArray();
                    }
                    char[] userIdArr = txtSymbolID.Text.ToCharArray();
                    if (userIdArr.Length == 8)
                    {
                        m_2525C_DummySIDC[0] = userIdArr[0];
                        m_2525C_DummySIDC[2] = userIdArr[1];
                        m_2525C_DummySIDC[4] = userIdArr[2];
                        m_2525C_DummySIDC[5] = userIdArr[3];
                        m_2525C_DummySIDC[6] = userIdArr[4];
                        m_2525C_DummySIDC[7] = userIdArr[5];
                        m_2525C_DummySIDC[8] = userIdArr[6];
                        m_2525C_DummySIDC[9] = userIdArr[7];
                    }
                    txtFullSymbolID.Text = new string(m_2525C_DummySIDC);
                }
                else if (txtSymbolID.Text.Length == 30)
                {
                    txtFullSymbolID.Text = txtSymbolID.Text;
                }
            }
            FunctionThatRaisesEvent();
        }

        public string GetSymbolID()
        {
            return txtFullSymbolID.Text;
        }

        public void SetFullSymbolID(string sFullSymbolID)
        {
            txtFullSymbolID.Text = sFullSymbolID;
            if (GetSymbologyStandard() == DNESymbologyStandard._ESS_APP6D)
            {
                //char[] arr = sFullSymbolID.ToCharArray();
                char[] arrSymbolID = new char[8];
                arrSymbolID[0] = sFullSymbolID[4];
                arrSymbolID[1] = sFullSymbolID[5];
                arrSymbolID[2] = sFullSymbolID[10];
                arrSymbolID[3] = sFullSymbolID[11];
                arrSymbolID[4] = sFullSymbolID[12];
                arrSymbolID[5] = sFullSymbolID[13];
                arrSymbolID[6] = sFullSymbolID[14];
                arrSymbolID[7] = sFullSymbolID[15];

                txtSymbolID.Text = new string(arrSymbolID);

                txtColorID.Text = sFullSymbolID[3] != 'X' ? sFullSymbolID[3].ToString() : "";
            }
            else if(GetSymbologyStandard() == DNESymbologyStandard._ESS_2525C)
            {
                if (sFullSymbolID.StartsWith(m_2525_prefix))
                {
                    txtSymbolID.Text = sFullSymbolID;
                }
                else
                {
                    char[] arrSymbolID = new char[8];
                    arrSymbolID[0] = sFullSymbolID[4];
                    arrSymbolID[1] = sFullSymbolID[2];
                    arrSymbolID[2] = sFullSymbolID[4];
                    arrSymbolID[3] = sFullSymbolID[5];
                    arrSymbolID[4] = sFullSymbolID[6];
                    arrSymbolID[5] = sFullSymbolID[7];
                    arrSymbolID[6] = sFullSymbolID[8];
                    arrSymbolID[7] = sFullSymbolID[9];

                    txtSymbolID.Text = new string(arrSymbolID);
                }
            }
        }

        public DNESymbologyStandard GetSymbologyStandard()
        {
            if (cmbSymbologyStandard.Text != "")
                return (DNESymbologyStandard)Enum.Parse(typeof(DNESymbologyStandard), cmbSymbologyStandard.Text);
            else
                return DNESymbologyStandard._ESS_NONE;
        }

        public void SetSymbologyStandard(DNESymbologyStandard symbologyStandard)
        {
            cmbSymbologyStandard.Text = symbologyStandard.ToString();
        }

        public void SetControlsEnabled(bool isEnabled)
        {
            cmbSymbologyStandard.Enabled = isEnabled;
            txtSymbolID.Enabled = isEnabled;
        }

        private void txtColorID_TextChanged(object sender, EventArgs e)
        {
            if (txtColorID.Text.Length == 1)
                m_App6D_DummySIDC[3] = txtColorID.Text[0];
            else if (txtColorID.Text.Length == 0)
                m_App6D_DummySIDC[3] = 'X';

            txtFullSymbolID.Text = new string(m_App6D_DummySIDC);
            FunctionThatRaisesEvent();
        }

        private void cmbSymbologyStandard_SelectedIndexChanged(object sender, EventArgs e)
        {
            DNESymbologyStandard symbologyStandard = GetSymbologyStandard();
            label1.Enabled = txtColorID.Enabled = (symbologyStandard == DNESymbologyStandard._ESS_APP6D);
            CalculatetFullSymbolID();
        }
    }

    
}
