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
using MCTester.Managers;
using System.Linq;

namespace MCTester.Controls
{
    public partial class CtrlGridCoordinateSystem : UserControl
    {
        private IDNMcGridCoordinateSystem m_GridCoordinateSystem;
        private Object[] GridCoordSysArr;

        public CtrlGridCoordinateSystem()
        {
            InitializeComponent();
            LoadCoordSysList();
        }
        
        public void SetReadOnly()
        {
            btnRefreshList.Enabled = BtnNewGridCoordSys.Enabled = lstExistingGridCoordSys.Enabled = false;
        }

        public IDNMcGridCoordinateSystem GridCoordinateSystem
        {
            get
            {
                return GetSelectedGridCoordinateSystem();
            }
            set
            {
                m_GridCoordinateSystem = value;
                if (value != null)
                {
                    if (!Manager_MCGridCoordinateSystem.IsExist(value))
                    {
                        Manager_MCGridCoordinateSystem.AddNewGridCoordinateSystem(value);
                        LoadCoordSysList();
                    }

                    SetSelectedGridCoordinateSystem(value);
                }
                else
                    lstExistingGridCoordSys.ClearSelected();
            }
        }

        public bool EnableNewCoordSysCreation
        {
            get { return BtnNewGridCoordSys.Enabled; }
            set 
            {
                BtnNewGridCoordSys.Enabled = value;
                btnRefreshList.Enabled = value;
            }
        }

        public bool IsEditable
        {
            get;set;
        }

        public void ClearSelectedList()
        {
            lstExistingGridCoordSys.ClearSelected();
        }

        public void LoadCoordSysList()
        {
            lstExistingGridCoordSys.Items.Clear();
            List<string> listNames = new List<string>();
            int arrSize = 0;
            if (Manager_MCGridCoordinateSystem.AllParams != null)
            {
                if (Manager_MCGridCoordinateSystem.AllParams.Count > 0)
                {
                    arrSize = Manager_MCGridCoordinateSystem.AllParams.Count;
                    GridCoordSysArr = new object[arrSize];
                    Manager_MCGridCoordinateSystem.AllParams.Keys.CopyTo(GridCoordSysArr, 0);
                    for (int i = 0; i < GridCoordSysArr.Length; i++)
                    {
                        string counterText = "";
                        uint counter = Manager_MCGridCoordinateSystem.GetCounter(GridCoordSysArr[i] as IDNMcGridCoordinateSystem);
                        if (counter > 1)
                            counterText = " (" + counter +")";
                        listNames.Add(GridCoordSysArr[i].ToString() + counterText);
                    }
                    lstExistingGridCoordSys.Items.AddRange(listNames.ToArray());
                    lstExistingGridCoordSys.SetSelected(0, true);
                   
                    
                }
            }
        }

        private void btnRefreshList_Click(object sender, EventArgs e)
        {
            lstExistingGridCoordSys.Items.Clear();
            LoadCoordSysList();
        }

        private void BtnNewGridCoordSys_Click(object sender, EventArgs e)
        {
            frmNewGridCoordinateSystem NewGridCoordinateSystemForm = new frmNewGridCoordinateSystem();
            if (NewGridCoordinateSystemForm.ShowDialog() == DialogResult.OK)
            {
                m_GridCoordinateSystem = NewGridCoordinateSystemForm.NewGridCoordinateSystem;
                LoadCoordSysList();

                SetSelectedGridCoordinateSystem(m_GridCoordinateSystem);
            }
            else
            {
                MessageBox.Show("New Grid Coordinate System creation was aborted.\n" +
                                "You have to create a new one or to choose one from the list",
                                "Grid Coordinate System Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private IDNMcGridCoordinateSystem GetSelectedGridCoordinateSystem()
        {
            if (lstExistingGridCoordSys.SelectedIndex >= 0 && lstExistingGridCoordSys.SelectedIndex < Manager_MCGridCoordinateSystem.AllParams.Count)
            {
                return (IDNMcGridCoordinateSystem)Manager_MCGridCoordinateSystem.AllParams.Keys.ToList()[lstExistingGridCoordSys.SelectedIndex];
            }
            return null;
        }

        private void SetSelectedGridCoordinateSystem(IDNMcGridCoordinateSystem mcGridCoordinateSystem)
        {
            if (mcGridCoordinateSystem != null)
            {
                int index = Manager_MCGridCoordinateSystem.FindLastIndex(mcGridCoordinateSystem);
                if (index >= 0 && index <= lstExistingGridCoordSys.Items.Count)
                    lstExistingGridCoordSys.SelectedIndex = index;
                else
                    lstExistingGridCoordSys.ClearSelected();
            }
            else
                lstExistingGridCoordSys.ClearSelected();

        }


        private void lstExistingGridCoordSys_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IDNMcGridCoordinateSystem selectedGridCoordinateSystem = GetSelectedGridCoordinateSystem();

            if (selectedGridCoordinateSystem != null)
            {
                frmNewGridCoordinateSystem NewGridCoordinateSystemForm = new frmNewGridCoordinateSystem(selectedGridCoordinateSystem);
                NewGridCoordinateSystemForm.ShowDialog();
            }
        }

        private void lstExistingGridCoordSys_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > lstExistingGridCoordSys.ItemHeight * lstExistingGridCoordSys.Items.Count)
                lstExistingGridCoordSys.ClearSelected();
        }

        public string GroupBoxText
        {
        	get { return m_gbGridCoordinateSystem.Text;}
            set { m_gbGridCoordinateSystem.Text = value; }
        }

        public void SetSelected(int index)
        {
            if (lstExistingGridCoordSys.Items != null && lstExistingGridCoordSys.Items.Count > index)
                lstExistingGridCoordSys.SelectedIndex = index;
        }

       
    }
}
