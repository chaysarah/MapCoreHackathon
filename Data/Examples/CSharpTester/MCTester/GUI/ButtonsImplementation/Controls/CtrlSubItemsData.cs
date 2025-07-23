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
    public partial class CtrlSubItemsData : UserControl
    {
        public CtrlSubItemsData()
        {
            InitializeComponent();
        }

        public DNSArrayProperty<DNSMcSubItemData> SubItemsData
        {
            get
            {
                DNSArrayProperty<DNSMcSubItemData> RetValue = new DNSArrayProperty<DNSMcSubItemData>();
                uint[] ids = InvertInputToUintArr(txtSubItemID.Text);
                short[] startIndex = InvertInputToShortArr(txtPointsStartIndex.Text);

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
                txtSubItemID.Text = string.Empty;
                txtPointsStartIndex.Text = string.Empty;
                
                if (value.aElements != null)
                {
                    foreach (DNSMcSubItemData subItem in value.aElements)
                    {
                        uint id = subItem.uSubItemID;
                        string sid = id.ToString();
                        if(id == DNMcConstants._MC_EMPTY_ID)
                            sid = "MAX";
                        else if(id == DNMcConstants._MC_EXTRA_CONTOUR_SUB_ITEM_ID)
                            sid = "MAX-1";
                        txtSubItemID.Text += sid + " ";
                        txtPointsStartIndex.Text += subItem.nPointsStartIndex.ToString() + " ";
                    }
                }
            }
        }

        private uint[] InvertInputToUintArr(string input)
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
                    if (idsName[i].Trim().ToUpper().StartsWith("MAX")) 
                    {
                        if (idsName[i].Trim().ToUpper() == "MAX")
                            ids.Add(DNMcConstants._MC_EMPTY_ID);
                        else if (idsName[i].Trim().ToUpper() == "MAX-1")
                            ids.Add(DNMcConstants._MC_EXTRA_CONTOUR_SUB_ITEM_ID);
                    }
                    else
                    {
                        IsParse = uint.TryParse(idsName[i], out result);
                        if (IsParse == true)
                            ids.Add(result);
                    }
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

    }
}
