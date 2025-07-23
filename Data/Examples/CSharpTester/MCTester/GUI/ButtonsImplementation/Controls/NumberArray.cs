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
    public partial class NumberArray : UserControl
    {
        public NumberArray()
        {
            InitializeComponent();
            dgvNumbers.Visible = false;
            SeperateNumbers = 1;
        }
        public enum NumbersType { Int, Uint }
        public NumbersType mNumbersType { set; get; }

        private int mNumColumns;

        private string mHeaderTextCol1;
        private string mHeaderTextCol2;
        private string mHeaderTextCol3;
        private int mSeperateNumbers = 1;
        private bool mIsGridFocus;

        public void SetHeadersColumns(int numColumns, string headerTextCol1, string headerTextCol2 = "", string headerTextCol3 = "")
        {
            dgvNumbers.Visible = true;
            mNumColumns = numColumns;
            mHeaderTextCol1 = headerTextCol1;
            mHeaderTextCol2 = headerTextCol2;
            mHeaderTextCol3 = headerTextCol3;

            if (mNumColumns > 3)
                mNumColumns = 3;
            if (mNumColumns == 0)
                mNumColumns = 1;

            if (mNumColumns < 3)
                Column3.Visible = false;
            if (mNumColumns < 2)
                Column2.Visible = false;

            Column1.HeaderText = headerTextCol1;
            Column2.HeaderText = headerTextCol2;
            Column3.HeaderText = headerTextCol3;

        }

        public bool IsSeperateWithTwiceSpace
        {
            get; set;
        }

        public int SeperateNumbers {
            get {
                return mSeperateNumbers;
            }
            set {
                mSeperateNumbers = value;
            }
        }

        public uint GetUInt32(string value)
        {
            try
            {
                uint uParam;
                if (String.Compare(value, "MAX", true) == 0)
                    uParam = DNMcConstants._MC_EMPTY_ID;
                else
                    uParam = uint.Parse(value);

                return uParam;
            }
            catch
            {
                return (uint)0;
            }
        }

        public int GetInt32(string value)
        {
            try
            {
                int uParam;
                if (String.Compare(value, "MAX", true) == 0)
                    uParam = int.MaxValue;
                else
                    uParam = int.Parse(value);

                return uParam;
            }
            catch
            {
                return (int)0;
            }
        }

        private DNSArrayProperty<int> InvertInputToIntArr(string input)
        {
            List<int> ids = new List<int>();
            DNSArrayProperty<int> retValue = new DNSArrayProperty<int>();
            string[] idsName;
            if (input != "")
            {
                string[] delimeters = null;
                delimeters = new string[2];
                delimeters[0] = " ";
                delimeters[1] = "  ";

                idsName = input.Trim().Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                retValue.aElements = new int[idsName.Length];

                for (int i = 0; i < idsName.Length; i++)
                {
                    retValue.aElements[i] = GetInt32(idsName[i]);
                }

                return retValue;
            }
            else
                return retValue;
        }

        private string InvertIntArrToString(DNSArrayProperty<int> input)
        {
            string strValue = string.Empty;

            if (input.aElements != null)
            {
                bool step = true;
                foreach (int number in input.aElements)
                {
                    step = !step;
                    strValue += number.ToString() + " ";
                    if (step && IsSeperateWithTwiceSpace)
                        strValue += " ";
                }
            }
            return strValue;
        }

        private DNSArrayProperty<uint> InvertInputToUintArr(string input)
        {
            List<uint> ids = new List<uint>();
            DNSArrayProperty<uint> retValue = new DNSArrayProperty<uint>();
            string[] idsName;
            if (input != "")
            {
                string[] delimeters = null;
                if (IsSeperateWithTwiceSpace)
                    delimeters = new string[2];
                else
                    delimeters = new string[1];
                delimeters[0] = " ";
                if (IsSeperateWithTwiceSpace)
                    delimeters[1] = " ";

                idsName = input.Trim().Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                retValue.aElements = new uint[idsName.Length];

                for (int i = 0; i < idsName.Length; i++)
                {
                    retValue.aElements[i] = GetUInt32(idsName[i]);
                }

                return retValue;
            }
            else
                return retValue;
        }

        private string InvertUintArrToString(DNSArrayProperty<uint> input)
        {
            string strValue = string.Empty;

            if (input.aElements != null)
            {
                for (int i = 0; i < input.aElements.Length;)
                {

                    for (int j = 0; j < SeperateNumbers && i < input.aElements.Length; j++)
                    {
                        strValue += input.aElements[i].ToString() + " ";
                        i++;
                    }
                    if (SeperateNumbers> 1 && IsSeperateWithTwiceSpace)
                        strValue += " ";
                }
               /* bool step = true;
                foreach (uint number in input.aElements)
                {
                    step = !step;
                    strValue += number.ToString() + " ";
                    if (step && IsSeperateWithTwiceSpace)
                        strValue += " ";
                }*/
            }
            return strValue;
        }

        public DNSArrayProperty<uint> UIntArrayPropertyValue
        {
            get
            {
                return InvertInputToUintArr(txtNumbersArray.Text);
            }
            set
            {
                txtNumbersArray.Text = InvertUintArrToString(value);
            }
        }

        public DNSArrayProperty<int> IntArrayPropertyValue
        {
            get
            {
                return InvertInputToIntArr(txtNumbersArray.Text);
            }
            set
            {
                txtNumbersArray.Text = InvertIntArrToString(value);
            }
        }

        public DNSArrayProperty<int> IntArrayPropertyValueNew { get; set; }

        public DNSArrayProperty<uint> UIntArrayPropertyValueNew { get; set; }

        private void dgvNumbers_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            UpdateValue();
        }

        private void dgvNumbers_Enter(object sender, EventArgs e)
        {
            mIsGridFocus = true;
        }

        private void txtNumbersArray_Enter(object sender, EventArgs e)
        {
            mIsGridFocus = false;
        }

        private void FillEmptyCells()
        {
            for (int i = 0; i < dgvNumbers.RowCount; i++)
            {
                if (dgvNumbers.Rows[i].IsNewRow == false)
                {
                    for (int j = 0; j < dgvNumbers.ColumnCount; j++)
                    {
                        if (dgvNumbers.Columns[j].Visible && dgvNumbers[j, i].Value == null)
                            dgvNumbers[j, i].Value = "0";
                    }
                }
            }
        }

        private void UpdateValue()
        {
            if (mNumbersType == NumbersType.Int)
            {
                DNSArrayProperty<int> retValue = new DNSArrayProperty<int>();
                if (mIsGridFocus)
                {
                    retValue.aElements = new int[(dgvNumbers.RowCount - 1) * mNumColumns];
                    FillEmptyCells();
                    int elementIndex = 0;
                    for (int rowIndex = 0; rowIndex < dgvNumbers.RowCount - 1; rowIndex++)
                    {
                        retValue.aElements[elementIndex] = GetInt32(dgvNumbers[1, rowIndex].Value.ToString());
                        elementIndex++;
                        if (mNumColumns > 1)
                        {
                            retValue.aElements[elementIndex] = GetInt32(dgvNumbers[2, rowIndex].Value.ToString());
                            elementIndex++;
                        }
                        if (mNumColumns > 2)
                        {
                            retValue.aElements[elementIndex] = GetInt32(dgvNumbers[3, rowIndex].Value.ToString());
                            elementIndex++;
                        }
                    }

                    txtNumbersArray.Text = InvertIntArrToString(retValue);
                }
                else
                {
                    retValue = InvertInputToIntArr(txtNumbersArray.Text);

                    dgvNumbers.Rows.Clear();

                    int elementIndex = 0;
                    if (retValue.aElements != null)
                    {
                        for (int i = 0; (i < retValue.aElements.Length) && (elementIndex < retValue.aElements.Length); i++)
                        {
                            dgvNumbers.Rows.Add();
                            for (int colIndex = 0; (elementIndex < retValue.aElements.Length) && (colIndex < mNumColumns); colIndex++)
                            {
                                dgvNumbers[colIndex + 1, dgvNumbers.NewRowIndex - 1].Value = retValue.aElements[elementIndex];
                                elementIndex++;
                            }
                        }
                    }
                }
            }
            else
            {
                DNSArrayProperty<uint> retValue = new DNSArrayProperty<uint>();
                if (mIsGridFocus)
                {
                    retValue.aElements = new uint[(dgvNumbers.RowCount - 1) * mNumColumns];
                    FillEmptyCells();
                    int elementIndex = 0;
                    for (int rowIndex = 0; rowIndex < dgvNumbers.RowCount - 1; rowIndex++)
                    {
                        retValue.aElements[elementIndex] = GetUInt32(dgvNumbers[1, rowIndex].Value.ToString());
                        elementIndex++;
                        if (mNumColumns > 1)
                        {
                            retValue.aElements[elementIndex] = GetUInt32(dgvNumbers[2, rowIndex].Value.ToString());
                            elementIndex++;
                        }
                        if (mNumColumns > 2)
                        {
                            retValue.aElements[elementIndex] = GetUInt32(dgvNumbers[3, rowIndex].Value.ToString());
                            elementIndex++;
                        }
                    }

                    txtNumbersArray.Text = InvertUintArrToString(retValue);
                }
                else
                {
                    retValue = InvertInputToUintArr(txtNumbersArray.Text);

                    dgvNumbers.Rows.Clear();

                    int elementIndex = 0;
                    if (retValue.aElements != null)
                    {
                        for (int i = 0; (i < retValue.aElements.Length) && (elementIndex < retValue.aElements.Length); i++)
                        {
                            dgvNumbers.Rows.Add();
                            for (int colIndex = 0; (elementIndex < retValue.aElements.Length) && (colIndex < mNumColumns); colIndex++)
                            {
                                dgvNumbers[colIndex + 1, dgvNumbers.NewRowIndex - 1].Value = retValue.aElements[elementIndex];
                                elementIndex++;
                            }
                        }
                    }
                }
            }
        }

        private void dgvNumbers_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dgvNumbers.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex).ToString();
        }

        private void dgvNumbers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (mIsGridFocus)
                UpdateValue();
        }

        private void txtNumbersArray_TextChanged(object sender, EventArgs e)
        {
            if (!mIsGridFocus)
                UpdateValue();
        }
    }
}
