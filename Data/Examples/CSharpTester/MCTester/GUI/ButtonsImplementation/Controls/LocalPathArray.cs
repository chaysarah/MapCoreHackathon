using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCTester.Controls
{
    public partial class LocalPathArray : UserControl
    { 
        private bool m_isFolder;
        private string m_filter;
        
        public LocalPathArray()
        {
            InitializeComponent();
            m_isFolder = false;
        }

        public bool IsFolder
        {
            get { return m_isFolder; }
            set { m_isFolder = value; }
        }

        public string Filter
        {
            get { return m_filter; }
            set { m_filter = value; }
        }

        private void dgvPathArray_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)  // color column
            {
                if (IsFolder)
                {
                    FolderSelectDialog FSD = new FolderSelectDialog();
                    FSD.Title = "Folder to select";
                    FSD.InitialDirectory = @"c:\";
                    if (FSD.ShowDialog(IntPtr.Zero))
                    {
                        InsertNewPath(FSD.FileName, e.RowIndex);
                    }
                }
                else
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = Filter;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        InsertNewPath(openFileDialog.FileName, e.RowIndex);
                    }
                }
            }
        }

        private void InsertNewPath(string path, int rowIndex)
        {
            if (rowIndex == dgvPathArray.RowCount - 1 || dgvPathArray.Rows[dgvPathArray.RowCount - 1].IsNewRow == false)
            {
                dgvPathArray.Rows.Add();
            }
            dgvPathArray[1, rowIndex].Value = path;
        }

        public String[] PathArray
        {
            get
            {
                string[] returnValue = new string[dgvPathArray.RowCount - 1];
                for (int i = 0; i < dgvPathArray.RowCount; i++)
                {
                    if (dgvPathArray.Rows[i].IsNewRow == false)
                    {
                        string path = dgvPathArray[1, i].Value.ToString();
                        returnValue[i] = path;
                    }
                }
                return returnValue;
            }
            set
            {
                if (value != null)
                {
                    if (value.Length > 0)
                    {
                        for (int i = 0; i < value.Length; i++)
                        {
                            dgvPathArray.Rows.Add("", value[i]);
                        }
                    }
                }
            }
        }

        public void ResetTable()
        {
            dgvPathArray.Rows.Clear();
        }
    }
}
