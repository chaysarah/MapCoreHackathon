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
    public partial class CtrlNamesArray : UserControl
    {
        public CtrlNamesArray()
        {
            InitializeComponent();
        }

        public enum EMCTTypeName
        {
            Property, State
        }

        public EMCTTypeName TypeName { get; set; }

        public bool IsNewRow(int indexRow)
        {
            return dgvPropertiesList.Rows[indexRow].IsNewRow;
        }

        public int SelectedRowIndex
        {
            get { return dgvPropertiesList.SelectedRows[0].Index; }
        }

        public Dictionary<byte, string> StateNames
        {
            get
            {
                byte result;
                Dictionary<byte, string> dicNames = new Dictionary<byte, string>();

                for (int i = 0; i < dgvPropertiesList.Rows.Count - 1; i++)
                {
                    object obj = dgvPropertiesList[0, i].Value;
                    if (obj != null)
                    {
                        byte.TryParse(obj.ToString(), out result);
                        if (dicNames.ContainsKey(result))
                        {
                            break;
                        }
                        string strName = (dgvPropertiesList[1, i].Value != null) ? dgvPropertiesList[1, i].Value.ToString() : "";
                        dicNames.Add(result, strName);
                    }
                }
                return dicNames;
            }
            set
            {
                if (value != null)
                {
                    int i = 0;
                    foreach(KeyValuePair<byte, string> data in value)
                    {
                        dgvPropertiesList.Rows.Add();
                        dgvPropertiesList[0, i].Value = data.Key;
                        dgvPropertiesList[1, i].Value = data.Value;
                        i++;
                    }
                }
            }
        }

       /* public DNSPropertyName[] PropertyNames
        {
            get
            {
                DNSPropertyName[] propertyNames = null;
                uint result;
                uint uID;
                bool parseSucceed;

                if (dgvPropertiesList.Rows[0].IsNewRow == false)
                {
                    propertyNames = new DNSPropertyName[dgvPropertiesList.Rows.Count - 1];

                    for (int i = 0; i < dgvPropertiesList.Rows.Count - 1; i++)
                    {
                        propertyNames[i].strName = (dgvPropertiesList[0, i].Value != null) ? dgvPropertiesList[0, i].Value.ToString() : "";

                        if (dgvPropertiesList[1, i].Value != null)
                        {
                            parseSucceed = uint.TryParse(dgvPropertiesList[1, i].Value.ToString(), out result);
                            uID = (parseSucceed == true) ? result : DNMcConstants._MC_EMPTY_ID;
                        }
                        else
                            uID = DNMcConstants._MC_EMPTY_ID;

                        propertyNames[i].uID = uID;
                    }
                }
                return propertyNames;
            }
            set
            {
                if (value != null)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        dgvPropertiesList.Rows.Add();
                        dgvPropertiesList[0, i].Value = value[i].strName;
                        dgvPropertiesList[1, i].Value = value[i].uID.ToString();
                    }
                }
            }
        }*/

        private void dgvPropertiesList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)  // ID
                {
                    byte result;
                    object strValue = dgvPropertiesList[0, e.RowIndex].Value;
                    bool parseSucceed = false;
                    if (strValue != null)
                    {
                        if (strValue.ToString() != "")
                        {
                            parseSucceed = byte.TryParse(strValue.ToString(), out result);

                            if (TypeName == EMCTTypeName.State && !parseSucceed)
                            {

                                MessageBox.Show("ID value should be between 1-255!", "Error", MessageBoxButtons.OK);
                                dgvPropertiesList[0, e.RowIndex].Value = "";
                            }

                            if (dgvPropertiesList[0, e.RowIndex].Value.ToString() != "" && parseSucceed)
                            {
                                object strRowValue;
                                uint rowResult;
                                for (int i = 0; i < dgvPropertiesList.Rows.Count - 1; i++)
                                {
                                    if (i != e.RowIndex)
                                    {
                                        strRowValue = dgvPropertiesList[0, i].Value;
                                        if ((strRowValue != null) && (strRowValue.ToString() != ""))
                                        {
                                            rowResult = uint.Parse(strRowValue.ToString());
                                            if (result == rowResult)
                                            {
                                                MessageBox.Show(string.Concat("ID value already exist: ", result, ", line number: ", i + 1), "Error", MessageBoxButtons.OK);
                                                dgvPropertiesList[0, e.RowIndex].Value = "";
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

