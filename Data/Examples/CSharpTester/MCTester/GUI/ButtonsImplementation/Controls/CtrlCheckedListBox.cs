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
    public partial class CtrlCheckedListBox : UserControl
    {
        public CtrlCheckedListBox()
        {
            InitializeComponent();
        }

        public int m_index_none = 0;
        public int m_index_any = 0;
        public int m_count = 0;
        bool flag = false;
        int total = 0;

        public void LoadList(string[] values,int count,int index_none, int index_any)
        {
            checkedListBox.Items.AddRange(values);

            m_index_none = index_none;
            m_index_any = index_any;
            m_count = count;
            if (m_index_none >= 0)
                m_count--;
            if (m_index_any >= 0)
            {
                m_count--;
                checkedListBox.SetItemChecked(checkedListBox.Items.Count - 1, true);
            }

        }

        public void SetItemChecked(int index, bool value)
        {
            checkedListBox.SetItemChecked(index, value);
        }

        public CheckedListBox.CheckedItemCollection CheckedItems
        {
            get { return checkedListBox.CheckedItems; }
        }

        public CheckedListBox.CheckedIndexCollection CheckedIndices
        {
            get { return checkedListBox.CheckedIndices; }
        }

        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (flag == false)
            {
                if (e.Index == m_index_none)
                {
                    if (e.NewValue == CheckState.Checked)
                    {
                        flag = true;
                        for (int i = 1; i < checkedListBox.Items.Count ; i++)
                            checkedListBox.SetItemChecked(i, false);
                        total = 0;
                        flag = false;
                    }
                }
                else if (e.Index == m_index_any)
                {
                    if (e.NewValue == CheckState.Checked)
                    {
                        flag = true;
                        for (int i = 1; i < checkedListBox.Items.Count - 1; i++)
                            checkedListBox.SetItemChecked(i, true);
                        if (m_index_none >= 0)
                        {
                            checkedListBox.SetItemChecked(m_index_none, false);
                        }
                        total = m_count;
                        flag = false;
                    }
                }
                else
                {
                    if (e.NewValue == CheckState.Checked)
                    {
                        if (m_index_none >= 0)
                        {
                            checkedListBox.SetItemChecked(m_index_none, false);
                        }
                        total++;
                        if (total == m_count && m_index_any >= 0)
                        {
                            checkedListBox.SetItemChecked(m_index_any, true);
                        }
                    }
                    else
                    {
                        if (m_index_any >= 0)
                        {
                            checkedListBox.SetItemChecked(m_index_any, false);
                        }
                        total--;
                        if (total == 0 && m_index_none >= 0)
                        {
                            checkedListBox.SetItemChecked(m_index_none, true);
                        }
                    }
                }
            }
        }
    }
}
