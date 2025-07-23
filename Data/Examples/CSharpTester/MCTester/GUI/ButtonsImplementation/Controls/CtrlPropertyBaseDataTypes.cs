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
    public partial class CtrlPropertyBaseDataTypes : CtrlPropertyBase 
    {
        public CtrlPropertyBaseDataTypes()
        {
            InitializeComponent();
        }

        public float RegFloatVal
        {
            get { return ntxRegValue.GetFloat(); }
            set { ntxRegValue.SetFloat(value); }
        }

        public uint RegUintVal
        {
            get { return ntxRegValue.GetUInt32(); }
            set { ntxRegValue.SetUInt32(value); }
        }

        public double RegDoubleVal
        {
            get { return ntxRegValue.GetDouble(); }
            set { ntxRegValue.SetDouble(value); }
        }

        public int RegIntVal
        {
            get { return ntxRegValue.GetInt32(); }
            set { ntxRegValue.SetInt(value); }
        }

        public byte RegByteVal
        {
            get { return ntxRegValue.GetByte(); }
            set { ntxRegValue.SetByte(value); }
        }

        public short RegShortVal
        {
            get { return ntxRegValue.GetShort(); }
            set { ntxRegValue.SetShort(value); }
        }

        public string RegValueLable
        {
            get { return this.lblRegValue.Text; }
            set { this.lblRegValue.Text = value; }
        }

        public float SelFloatVal
        {
            get { return ntxSelValue.GetFloat(); }
            set { ntxSelValue.SetFloat(value); }
        }

        public uint SelUintVal
        {
            get { return ntxSelValue.GetUInt32(); }
            set { ntxSelValue.SetUInt32(value); }
        }

        public double SelDoubleVal
        {
            get { return ntxSelValue.GetDouble(); }
            set { ntxSelValue.SetDouble(value); }
        }

        public int SelIntVal
        {
            get { return ntxSelValue.GetInt32(); }
            set { ntxSelValue.SetInt(value); }
        }

        public byte SelByteVal
        {
            get { return ntxSelValue.GetByte(); }
            set { ntxSelValue.SetByte(value); }
        }

        public short SelShortVal
        {
            get { return ntxSelValue.GetShort(); }
            set { ntxSelValue.SetShort(value); }
        }

        public string SelValueLable
        {
            get { return this.lblSelValue.Text; }
            set { this.lblSelValue.Text = value; }
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }
 
    }
}
