using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCTester.Controls;
using MapCore;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class CtrlVectorItemID : UserControl
    {
        IDNMcVectorMapLayer mcVectorMapLayer;

        public CtrlVectorItemID()
        {
            InitializeComponent();
            rbGlobalID.Checked = true;
            ChangeUI();
        }

        public void SetVectorMapLayer(IDNMcVectorMapLayer vectorMapLayer)
        {
            mcVectorMapLayer = vectorMapLayer;
        }

        private void rbUint64_CheckedChanged(object sender, EventArgs e)
        {
            ChangeUI();
        }

        private void ChangeUI()
        {
            ntxDataSourceAsId.Enabled = rbDataSourceAsID.Checked;
            ntxDataSourceAsName.Enabled = rbDataSourceAsName.Checked;
        }

        public UInt64 GetVectorItemID()
        {
            if (rbGlobalID.Checked)
            {
                return ntxVectorItemId.GetUInt64();
            }
            else
            {
                return GetVectorID64FromUInt32();
            }
        }

        private UInt64 GetVectorID64FromUInt32()
        {
            UInt64 value = 0;
            uint vectorItemId;
            if (!uint.TryParse(ntxVectorItemId.Text, out vectorItemId))
            {
                ntxVectorItemId.Focus();
                throw new InvalidCastException("Vector Item Id should be as uint number");
            }

            if (rbDataSourceAsID.Checked)
            {
                value = mcVectorMapLayer.VectorItemIDFromOriginalID(ntxVectorItemId.GetUInt32(), "", ntxDataSourceAsId.GetUInt32());
            }
            else
            {
                value = mcVectorMapLayer.VectorItemIDFromOriginalID(ntxVectorItemId.GetUInt32(), ntxDataSourceAsName.Text);
            }
            return value;
        }

        public string GetVectorItemIDAsText()
        {
            return ntxVectorItemId.Text;
        }



        public void ConversionVectorItemIDFromOriginalID()
        {
            ntxVectorItemId.SetUInt64(GetVectorID64FromUInt32());
        }

     

        public bool CheckNegativeValue()
        {
            if (rbGlobalID.Checked && ntxVectorItemId.GetInt64() < 0)
            {
                UInt64 value = ntxVectorItemId.GetUInt64();
                ShowMsg("Vector Item ID", ntxVectorItemId);
                return true;
            }
            else
            {
                if (ntxDataSourceAsId.GetInt32() < 0)
                {
                    ShowMsg("Data Source ID", ntxDataSourceAsId);
                    return true;
                }
                if (ntxVectorItemId.GetInt32() < 0)
                {
                    ShowMsg("Org Vector Item ID", ntxVectorItemId);
                    return true;
                }
            }
            return false;
        }

        public bool CheckEmpty()
        {
            if ((rbGlobalID.Checked && ntxVectorItemId.Text == "") ||
                ((ntxDataSourceAsId.Text == "")))
            {
                return true;
            }
            return false;
        }

        private void ShowMsg(string fieldname, NumericTextBox numericTextBox)
        {
            MessageBox.Show(fieldname + " cannot be negative", "Invalid value");
            numericTextBox.Focus();
        }

        private void rbDataSourceAsID_CheckedChanged(object sender, EventArgs e)
        {
            ChangeUI();
        }

        private void rbDataSourceAsName_CheckedChanged(object sender, EventArgs e)
        {
            ChangeUI();
        }
    }
}
