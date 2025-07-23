using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.GUI.Map;

namespace MCTester.General_Forms
{
    public partial class RenderNeededForm : Form
    {
        private bool m_RenderState;
        private int m_Counter;

        public RenderNeededForm()
        {
            InitializeComponent();
            MCTMapForm.FrmRenderNeeded = this;
            m_RenderState = false;
            m_Counter = 0;
        }

        public void AddRenderToList(bool renderResult)
        {
            int eventIndex = lsvRenderNeeded.Items.Count + 1;
            ListViewItem newItem = new ListViewItem();

            if (m_RenderState != renderResult)
            {
                string [] textLine = new string [2];
                newItem.Text = eventIndex.ToString();
                textLine[0] = renderResult.ToString();
                textLine[1] = "1";

                newItem.SubItems.AddRange(textLine);
                lsvRenderNeeded.Items.Add(newItem);
                newItem.EnsureVisible();

                m_Counter = 0;
            }
            else
            {
                if (lsvRenderNeeded.Items.Count == 0)
                {
                    string[] textLine = new string[2];
                    newItem.Text = eventIndex.ToString();
                    textLine[0] = renderResult.ToString();
                    textLine[1] = "1";

                    newItem.SubItems.AddRange(textLine);
                    lsvRenderNeeded.Items.Add(newItem);
                    newItem.EnsureVisible();
                }
                else
                    lsvRenderNeeded.Items[lsvRenderNeeded.Items.Count - 1].SubItems[2].Text = (++m_Counter).ToString();

            }
            m_RenderState = renderResult;

        }

        private void RenderNeededForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MCTMapForm.FrmRenderNeeded = null;
        }

        private void btnClearEventList_Click(object sender, EventArgs e)
        {
            lsvRenderNeeded.Items.Clear();
        }
    }
}