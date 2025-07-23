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
using System.IO;

namespace MCTester.Controls
{
    public partial class CtrlGeometricAmplifiers : UserControl
    {
        private int m_dgvCellselectedIndex = 0;
        string[] m_GeometricAmplifiersKeys;
        bool isUserClickAdd = false;

        int m_ColSelect = 0;
        int m_ColKey = 1;
        //int m_ColCount = 2;
        int m_ColVal = 3;
        //int m_ColAdd = 4;

        bool m_IsInCreateObject = false;

        public CtrlGeometricAmplifiers()
        {
            InitializeComponent();

            dgvGeometricAmplifiers.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgvGeometricAmplifiers_EditingControlShowing);
            dgvGeometricAmplifiers.Rows.Clear();
            SetNewRow(0);
        }

        public void SetIsInCreateObject(bool isInCreateObject)
        {
            m_IsInCreateObject = isInCreateObject;
        }

        void dgvGeometricAmplifiers_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvGeometricAmplifiers.CurrentCell.ColumnIndex == m_ColVal)
            {
                // Check box column
                TextBox textBox = e.Control as TextBox;
                textBox.TextChanged += new EventHandler(TextBox_TextChanged);
            }
            /*if (dgvGeometricAmplifiers.CurrentCell.ColumnIndex == m_ColKey)
            {
                // Check box column
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
            }*/
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
           /* if (m_IsInCreateObject)
            {
                TextBox textBox = sender as TextBox;
                bool isSelected = false;
                if (textBox.Text != "")
                    isSelected = true;
                dgvGeometricAmplifiers[m_ColSelect, dgvGeometricAmplifiers.CurrentCell.RowIndex].Value = isSelected;
            }*/
        }

        void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((ComboBox)sender).SelectedIndexChanged -= new EventHandler(comboBox_SelectedIndexChanged);

            m_dgvCellselectedIndex = ((ComboBox)sender).SelectedIndex;
            dgvGeometricAmplifiers[m_ColKey, dgvGeometricAmplifiers.CurrentCell.RowIndex].Tag = m_GeometricAmplifiersKeys[m_dgvCellselectedIndex];

        }

        public void SetGeometricAmplifiers(DNSMcKeyFloatValue[] aGeometricAmplifiers)
        {
            dgvGeometricAmplifiers.Rows.Clear();

            List<string> list = aGeometricAmplifiers.Select(x => x.strKey).ToList<string>();
            SetGeometricAmplifiersKeys(list.ToArray());
            if (aGeometricAmplifiers.Length > 0)
            {
                dgvGeometricAmplifiers.Rows.Add(aGeometricAmplifiers.Length);
                for (int i = 0; i < aGeometricAmplifiers.Length; i++)
                {
                    dgvGeometricAmplifiers[m_ColKey, i].Value = aGeometricAmplifiers[i].strKey;
                    dgvGeometricAmplifiers[m_ColVal, i].Value = aGeometricAmplifiers[i].fValue;
                    dgvGeometricAmplifiers[m_ColSelect, i].Value = true;
                }
            }
            SetNewRow(aGeometricAmplifiers.Length);
        }

        internal void ResetGeometricAmplifiers()
        {
            dgvGeometricAmplifiers.Rows.Clear();
        }

        public void SetGeometricAmplifiersKeys(string[] aGeometricAmplifiersKeys)
        {
            m_GeometricAmplifiersKeys = aGeometricAmplifiersKeys;
            /*((DataGridViewComboBoxColumn)dgvGeometricAmplifiers.Columns[m_ColKey]).Items.Clear();
            ((DataGridViewComboBoxColumn)dgvGeometricAmplifiers.Columns[m_ColKey]).Items.AddRange(m_GeometricAmplifiersKeys);
*/
        }

        public DNSMcKeyFloatValue[] GetGeometricAmplifiers()
        {
            List<DNSMcKeyFloatValue> lstGeometricAmplifiers = new List<DNSMcKeyFloatValue>();
            for (int i = 0; i < dgvGeometricAmplifiers.Rows.Count; i++)
            {
                if (!dgvGeometricAmplifiers.Rows[i].IsNewRow)
                {
                    if (getSelectValue(i))
                    {
                        DNSMcKeyFloatValue keyFloatValue = new DNSMcKeyFloatValue();
                        object objKey = dgvGeometricAmplifiers[m_ColKey, i].Value;
                        if (objKey != null && objKey.ToString() != "")
                        {
                            keyFloatValue.strKey = objKey.ToString();
                            object objVal = dgvGeometricAmplifiers[m_ColVal, i].Value;
                            float fValue = 0;
                            if (objVal != null)
                            {
                                string strValue = objVal.ToString();
                                
                                if(float.TryParse(strValue, out fValue))
                                    keyFloatValue.fValue = fValue;
                            }
                            lstGeometricAmplifiers.Add(keyFloatValue);
                        }
                       
                    }
                }
            }

            return lstGeometricAmplifiers.ToArray();
        }

        private bool getSelectValue(int rowIndex)
        {
            return (bool)dgvGeometricAmplifiers[m_ColSelect, rowIndex].Value;
        }

        private void chxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chxSelectAll.Checked;
            for (int i = 0; i < dgvGeometricAmplifiers.Rows.Count; i++)
            {
                dgvGeometricAmplifiers[m_ColSelect, i].Value = isChecked;
            }
        }

        private void dgvGeometricAmplifiers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*if (e.RowIndex >= 0 &&
                e.ColumnIndex == m_ColAdd &&
                getSelectValue(e.RowIndex) / *&&
                getPrimary(e.RowIndex)* /)
            {
                AddRow(e.RowIndex);
            }
            else*/ if (e.RowIndex >= 0 && e.ColumnIndex == m_ColSelect)
            {
                dgvGeometricAmplifiers[m_ColSelect, e.RowIndex].Value = !getSelectValue(e.RowIndex);
            }
            //dgvGeometricAmplifiers.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        
        private void dgvGeometricAmplifiers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.ColumnIndex == m_ColSelect)
            {
                dgvGeometricAmplifiers.Rows[e.RowIndex].ReadOnly = !getSelectValue(e.RowIndex);
            }
        }

        private void dgvGeometricAmplifiers_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rec = dgvGeometricAmplifiers.GetCellDisplayRectangle(0, 0, false);
            chxSelectAll.Location = new Point(
            dgvGeometricAmplifiers.Location.X + rec.Left + (rec.Right - rec.Left) / 2 - chxSelectAll.AccessibilityObject.Bounds.Width / 2,
            dgvGeometricAmplifiers.Location.Y + (rec.Bottom - rec.Top) / 2 - chxSelectAll.AccessibilityObject.Bounds.Height / 2);

        }

        private void SetNewRow(int rowIndex)
        {
            dgvGeometricAmplifiers[m_ColSelect, rowIndex].Value = false;
           // dgvGeometricAmplifiers[m_ColSelect, rowIndex].Tag = false;
            dgvGeometricAmplifiers.Rows[rowIndex].ReadOnly = true;
        }

        private void dgvGeometricAmplifiers_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!isUserClickAdd)
                SetNewRow(e.RowIndex);
        }

        internal void SetGeometricAmplifiersNames(DNSMultiKeyName[] aGeometricAmplifiers)
        {
            dgvGeometricAmplifiers.Rows.Clear();

           /* List<string> list = aGeometricAmplifiers.Select(x => x.strKey).ToList<string>();
            SetGeometricAmplifiersKeys(list.ToArray());*/
            if (aGeometricAmplifiers.Length > 0)
            {
                dgvGeometricAmplifiers.Rows.Add(aGeometricAmplifiers.Length);
                for (int i = 0; i < aGeometricAmplifiers.Length; i++)
                {
                    dgvGeometricAmplifiers[m_ColKey, i].Value = aGeometricAmplifiers[i].strKeyBaseName;
                    dgvGeometricAmplifiers[m_ColSelect, i].Value = false;

                    dgvGeometricAmplifiers.Rows[i].ReadOnly = false;
                }
            }
            SetNewRow(aGeometricAmplifiers.Length);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            //ObjectPropertiesBase.ImportDataFromFile(dgvGeometricAmplifiers, 3);

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(ofd.FileName);
                char[] delimeters = new char[1];
                delimeters[0] = ',';
                string[] readerData = reader.ReadLine().Split(delimeters);
                reader.Close();

                float result = 0;
                //int currColumn = 0;
                int currRow = 0;
                dgvGeometricAmplifiers.RowCount = (int)readerData.Length / 2 + 1;

                for (int i = 0; i < readerData.Length; i+= 2)
                {
                    dgvGeometricAmplifiers[m_ColSelect, currRow].Value = true;
                    dgvGeometricAmplifiers[m_ColKey, currRow].Value = readerData[i];
                   
                    string value = readerData[i + 1];

                    bool IsParseSucc = float.TryParse(value, out result);
                    if (IsParseSucc != true)
                    {
                        MessageBox.Show("Import data from file failed.\nThe data '" + readerData[i].ToString() + "' located in cell: " + i.ToString() + " is invalid",
                                            "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        dgvGeometricAmplifiers.Rows.Clear();
                        dgvGeometricAmplifiers.RowCount = 1;
                        return;
                    }
                    else
                    {
                        dgvGeometricAmplifiers[m_ColVal, currRow].Value = result;
                        currRow++;
                    }
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //ObjectPropertiesBase.ExportDataToFile(dgvGeometricAmplifiers, 3);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV File (*.csv) | *.csv";
            sfd.RestoreDirectory = true;
           // int numColumns = 3;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter stw = new StreamWriter(sfd.FileName);
                string amplifiers = "";

                DNSMcKeyFloatValue[] geometricAmplifiers = GetGeometricAmplifiers();

                for(int i=0;i< geometricAmplifiers.Length;i++)
                {
                    amplifiers += geometricAmplifiers[i].strKey + "," + geometricAmplifiers[i].fValue + ",";
                }

                string exportData = amplifiers.Remove(amplifiers.Length - 1);
                stw.Write(exportData);

                stw.Close();
            }
        }
    }
}
