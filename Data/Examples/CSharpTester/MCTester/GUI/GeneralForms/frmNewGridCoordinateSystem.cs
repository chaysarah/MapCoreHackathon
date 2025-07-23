using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MapCore.Common;
using MCTester.Controls;
using MCTester.Managers.MapWorld;
using UnmanagedWrapper;

namespace MCTester.General_Forms
{
    public partial class frmNewGridCoordinateSystem : Form
    {
        private IDNMcGridCoordinateSystem m_ExistCoordSys;
        private IDNMcGridCoordinateSystem m_GridCoordinateSystem = null;
        
        public frmNewGridCoordinateSystem()
        {
            InitializeComponent();
            btnOK.Text = "OK";
            
            panel1.Visible = false;

            int height = ctrlNewGridCoordinateSystem1.GetOKBtnLocation() + 20; 
            Size = new Size(795, height);
            gbGridCoordinateSystem.Visible = false;
            this.Text = "New Grid Coordinate System";
        }

        public frmNewGridCoordinateSystem(IDNMcGridCoordinateSystem coordSys, bool isEnable = false) :this()
        {
            this.Text = "Grid Coordinate System";
            m_ExistCoordSys = coordSys;
            btnOK.Visible = isEnable;
            panel1.Visible = true;
            btnCloneAsNonGeneric.Visible = (m_ExistCoordSys.GetGridCoorSysType() == DNEGridCoordSystemType._EGCS_GENERIC_GRID);

            if (isEnable)
            {
                btnClose.Location = new Point(btnOK.Location.X + 100, btnOK.Location.Y);
            }
            List<object> lst = Manager_MCGridCoordinateSystem.AllParams.Keys.ToList();
            lst.Remove(coordSys);
            lstExistingGridCoordSys.Items.AddRange(lst.ToArray());
            if (lst.Count > 0)
                lstExistingGridCoordSys.SetSelected(0, true);


            if (coordSys != null)
            { 
                try
                { 
                    ctrlNewGridCoordinateSystem1.ShowCurrGridCoordSysParams(coordSys, isEnable);
                    gbGridCoordinateSystem.Enabled = true;
                    chxIsGeographical.Checked = coordSys.IsGeographical();
                    chxIsUtm.Checked = coordSys.IsUtm();
                    chxIsMultyZoneGrid.Checked = coordSys.IsMultyZoneGrid();
                    boxLegalValuesForGeographicCoordinates.SetBoxValue(coordSys.GetLegalValuesForGeographicCoordinates());
                    boxLegalValuesForGridCoordinates.SetBoxValue(coordSys.GetLegalValuesForGridCoordinates());
                    Size = new Size(795, 700);
                    gbGridCoordinateSystem.Visible = true;
                    try
                    {
                        string pstrOgcCrsCode = "";
                        chxIsExist.Checked = m_ExistCoordSys.GetOgcCrsCode(out pstrOgcCrsCode);
                        txtOgcCrsCode.Text = pstrOgcCrsCode;
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetOgcCrsCode", McEx);
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("get GridCoordSystem info", McEx);
                }
            }
        }

        private void ShowError(Control ctrlToSelect)
        {
            MessageBox.Show("Missing Datum", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            ctrlToSelect.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((btnOK.Text == "OK"))
            {
                m_GridCoordinateSystem = ctrlNewGridCoordinateSystem1.CreateCoordinateSystem();

                if (m_GridCoordinateSystem != null)
                {
                    Managers.MapWorld.Manager_MCGridCoordinateSystem.AddNewGridCoordinateSystem(NewGridCoordinateSystem,false);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public IDNMcGridCoordinateSystem NewGridCoordinateSystem
        {
            get
            {
                return m_GridCoordinateSystem;
            }
            set
            {
                m_GridCoordinateSystem = value;
            }
        }
                
        private void lstExistingGridCoordSys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstExistingGridCoordSys.SelectedItem != null && m_ExistCoordSys != null)
            {
                chxIsEqual.Checked = m_ExistCoordSys.IsEqual((IDNMcGridCoordinateSystem)lstExistingGridCoordSys.SelectedItem);
            }
        }

        private void lstExistingGridCoordSys_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstExistingGridCoordSys.SelectedItem != null)
            {
                frmNewGridCoordinateSystem NewGridCoordinateSystemForm = new frmNewGridCoordinateSystem((IDNMcGridCoordinateSystem)lstExistingGridCoordSys.SelectedItem);
                NewGridCoordinateSystemForm.ShowDialog();
            }
        }

        public DialogResult closeDialogResult = DialogResult.OK;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = closeDialogResult;
            this.Close();
        }

        private void btnSetLegalValuesForGeographicCoordinates_Click(object sender, EventArgs e)
        {
            try
            {
                m_ExistCoordSys.SetLegalValuesForGeographicCoordinates(boxLegalValuesForGeographicCoordinates.GetBoxValue());
            }
            catch(MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetLegalValuesForGeographicCoordinates", McEx);
            }
        }

        private void btnSetLegalValuesForGridCoordinates_Click(object sender, EventArgs e)
        {
            try
            {
                m_ExistCoordSys.SetLegalValuesForGridCoordinates(boxLegalValuesForGridCoordinates.GetBoxValue());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetLegalValuesForGridCoordinates", McEx);
            }
        }

        private void btnIsGeographicLocationLegal_Click(object sender, EventArgs e)
        {
            try
            {
                chxIsGeographicLocationLegal.Checked = m_ExistCoordSys.IsGeographicLocationLegal(ctrlLocation.GetVector3D());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IsGeographicLocationLegal", McEx);
            }
        }

        private void btnIsLocationLegal_Click(object sender, EventArgs e)
        {
            try
            {
                chxIsLocationLegal.Checked = m_ExistCoordSys.IsLocationLegal(ctrlLocation.GetVector3D());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IsLocationLegal", McEx);
            }
        }

        private void btnGetDefaultZoneFromGeographicLocation_Click(object sender, EventArgs e)
        {
            try
            {
                int zone = 0;
                m_ExistCoordSys.GetDefaultZoneFromGeographicLocation(ctrlLocation.GetVector3D(), out zone);
                ntbZone.SetInt(zone);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetDefaultZoneFromGeographicLocation", McEx);
            }
        }

        private void btnCloneAsGeneric_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcGridGeneric mcGridGeneric = m_ExistCoordSys.CloneAsGeneric();
                if (mcGridGeneric != null)
                {
                    if (chxAddToGridCoordinate.Checked)
                        Manager_MCGridCoordinateSystem.AddNewGridCoordinateSystem(mcGridGeneric, false);
                    frmNewGridCoordinateSystem NewGridCoordinateSystemForm = new frmNewGridCoordinateSystem(mcGridGeneric);
                    NewGridCoordinateSystemForm.Show();
                }
                else
                    MessageBox.Show("This grid coordinate system could not be cloned as generic", "Clone As Generic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CloneAsGeneric", McEx);
            }
        }

        private void btnCloneAsNonGeneric_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_ExistCoordSys.GetGridCoorSysType() == DNEGridCoordSystemType._EGCS_GENERIC_GRID)
                {
                    IDNMcGridCoordinateSystem mcGridCoordinateSystem = ((IDNMcGridGeneric)m_ExistCoordSys).CloneAsNonGeneric();
                    if (mcGridCoordinateSystem != null)
                    {
                        if (chxAddToGridCoordinate.Checked)
                            Manager_MCGridCoordinateSystem.AddNewGridCoordinateSystem(mcGridCoordinateSystem, false);
                        frmNewGridCoordinateSystem NewGridCoordinateSystemForm = new frmNewGridCoordinateSystem(mcGridCoordinateSystem);
                        NewGridCoordinateSystemForm.Show();
                    }
                    else
                        MessageBox.Show("This grid coordinate system could not be cloned as non generic", "Clone As Non Generic", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CloneAsNonGeneric", McEx);
            }
        }

    }
}