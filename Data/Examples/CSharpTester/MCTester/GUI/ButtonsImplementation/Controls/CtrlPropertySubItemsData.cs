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
    public partial class CtrlPropertySubItemsData : CtrlPropertyBase
    {
        public CtrlPropertySubItemsData()
        {
            InitializeComponent();
        }

        public DNSArrayProperty<DNSMcSubItemData> RegSubItemsData
        {
            get
            {
                DNSArrayProperty<DNSMcSubItemData> RetValue = new DNSArrayProperty<DNSMcSubItemData>();
                uint[] ids = InvertInputToUintArr(txtRegSubItemID.Text);
                short[] startIndex = InvertInputToShortArr(txtRegPointsStartIndex.Text);

                if (ids.Length == startIndex.Length)
                {
                    RetValue.aElements = new DNSMcSubItemData[ids.Length];
                    for (int i = 0; i < ids.Length; i++)
                    {
                        RetValue.aElements[i].uSubItemID = ids[i];
                        RetValue.aElements[i].nPointsStartIndex = startIndex[i];
                    }
                }
                else
                    MessageBox.Show("Id's number different from points start index number\nChange input data and click Apply button again", "Problem in Sub Items Data", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return RetValue;
            }
            set
            {
                if (value.aElements != null)
                {
                    foreach (DNSMcSubItemData subItem in value.aElements)
                    {
                        txtRegSubItemID.Text += subItem.uSubItemID.ToString() + " ";
                        txtRegPointsStartIndex.Text += subItem.nPointsStartIndex.ToString() + " ";
                    }
                }
            }
        }

        public DNSArrayProperty<DNSMcSubItemData> SelSubItemsData
        {
            get
            {
                DNSArrayProperty<DNSMcSubItemData> RetValue = new DNSArrayProperty<DNSMcSubItemData>();
                uint[] ids = InvertInputToUintArr(txtSelSubItemID.Text);
                short[] startIndex = InvertInputToShortArr(txtSelPointsStartIndex.Text);

                if (ids.Length == startIndex.Length)
                {
                    RetValue.aElements = new DNSMcSubItemData[ids.Length];
                    for (int i = 0; i < ids.Length; i++)
                    {
                        RetValue.aElements[i].uSubItemID = ids[i];
                        RetValue.aElements[i].nPointsStartIndex = startIndex[i];
                    }
                }
                else
                    MessageBox.Show("Id's number different from points start index number\nChange input data and click Apply button again", "Problem in Sub Items Data", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return RetValue;
            }
            set
            {
                if (value.aElements != null)
                {
                    foreach (DNSMcSubItemData subItem in value.aElements)
                    {
                        txtSelSubItemID.Text += subItem.uSubItemID.ToString() + " ";
                        txtSelPointsStartIndex.Text += subItem.nPointsStartIndex.ToString() + " ";
                    }
                }
            }
        }

        private uint [] InvertInputToUintArr(string input)
        {
            List<uint> ids = new List<uint>();
            string[] idsName;
            if (input != "")
            {
                string[] delimeter = null;
                idsName = input.Trim().Split(delimeter, StringSplitOptions.RemoveEmptyEntries);

                bool IsParse = false;
                uint result;
                for (int i = 0; i < idsName.Length; i++)
                {
                    IsParse = uint.TryParse(idsName[i], out result);
                    if (IsParse == true)
                        ids.Add(result);
                }

                return ids.ToArray();
            }
            else
                return ids.ToArray();
        }

        private short[] InvertInputToShortArr(string input)
        {
            List<short> PointsStartIndex = new List<short>();
            string[] PointsStartIndexName;
            if (input != "")
            {
                string[] delimeter = null;
                PointsStartIndexName = input.Trim().Split(delimeter, StringSplitOptions.RemoveEmptyEntries);

                bool IsParse = false;
                short result;
                for (int i = 0; i < PointsStartIndexName.Length; i++)
                {
                    IsParse = short.TryParse(PointsStartIndexName[i], out result);
                    if (IsParse == true)
                        PointsStartIndex.Add(result);
                }

                return PointsStartIndex.ToArray();
            }
            else
                return PointsStartIndex.ToArray();
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }
    }
}
