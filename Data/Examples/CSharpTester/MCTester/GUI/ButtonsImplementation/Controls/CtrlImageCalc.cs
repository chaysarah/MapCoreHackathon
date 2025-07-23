using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.General_Forms;
using MCTester.Managers.MapWorld;

namespace MCTester.Controls
{
    public partial class CtrlImageCalc : UserControl
    {
        private IDNMcImageCalc m_ImageCalc;
        private IDNMcImageCalc [] m_ImageCalcArr;

        public CtrlImageCalc()
        {
            InitializeComponent();
            LoadImageCalcList();
            
        }

        public IDNMcImageCalc ImageCalc
        {
            get
            {
                return (IDNMcImageCalc)lstExistingImageCalc.SelectedItem;
            }
            set
            {
                m_ImageCalc = value;
                if (value != null)
                {
                    if (lstExistingImageCalc.Items.Contains(value))
                    {
                        int imageCalcIdx = lstExistingImageCalc.Items.IndexOf(value);
                        lstExistingImageCalc.SetSelected(imageCalcIdx, true);
                    }
                    else
                    {
                        LoadImageCalcList();
                    }
                }
            }
        }

        public bool EnableNewCoordSysCreation
        {
            get { return BtnNewImageCalc.Enabled; }
            set 
            {
                BtnNewImageCalc.Enabled = value;
                btnRefreshList.Enabled = value;
            }
        }

        private void LoadImageCalcList()
        {
            lstExistingImageCalc.Items.Clear();

            int arrSize = 0;
            if (Manager_MCImageCalc.AllParams != null)
            {
                arrSize = Manager_MCImageCalc.AllParams.Count;
                m_ImageCalcArr = new IDNMcImageCalc[arrSize];
                Manager_MCImageCalc.AllParams.Keys.CopyTo(m_ImageCalcArr, 0);
                lstExistingImageCalc.Items.AddRange(m_ImageCalcArr);
            }
        }

        private void btnRefreshList_Click(object sender, EventArgs e)
        {
            lstExistingImageCalc.Items.Clear();
            LoadImageCalcList();
        }

        private void BtnNewImageCalc_Click(object sender, EventArgs e)
        {
            frmCreateImageCalc CreateImageCalcFrm = new frmCreateImageCalc();
            if (CreateImageCalcFrm.ShowDialog() == DialogResult.OK)
            {

                ImageCalc = CreateImageCalcFrm.ImageCalc;
                LoadImageCalcList();

                lstExistingImageCalc.SelectedIndex = lstExistingImageCalc.Items.Count - 1;
            }
            else
            {
                MessageBox.Show("New Image Calc creation was aborted.\n" +
                                "You have to create a new one or to choose one from the list",
                                "Grid Coordinate System Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lstExistingGridCoordSys_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstExistingImageCalc.SelectedItem != null)
            {
                frmCreateImageCalc CreateImageCalcFrm = new frmCreateImageCalc((IDNMcImageCalc)lstExistingImageCalc.SelectedItem);
                CreateImageCalcFrm.ShowDialog();
            }
        }

        private void lstExistingImageCalc_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > lstExistingImageCalc.ItemHeight * lstExistingImageCalc.Items.Count)
                lstExistingImageCalc.ClearSelected();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstExistingImageCalc.SelectedIndex != -1)
            {
                Manager_MCImageCalc.RemoveImageCalc((IDNMcImageCalc)lstExistingImageCalc.SelectedItem);
                ((IDNMcImageCalc)lstExistingImageCalc.SelectedItem).Dispose();

                LoadImageCalcList();
            }
        }
    }
}
