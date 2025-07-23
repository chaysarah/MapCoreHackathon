using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class frmNewObjectLocation : Form
    {
        private IDNMcObjectScheme m_ObjScheme;

        public frmNewObjectLocation(IDNMcObjectScheme objScheme)
        {
            InitializeComponent();
            cmbLocationCoordSystem.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            cmbLocationCoordSystem.Text = DNEMcPointCoordSystem._EPCS_WORLD.ToString();
            m_ObjScheme = objScheme;
        }

        private void btnAddNewLocation_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcObjectLocation objLocation;
                DNEMcPointCoordSystem coordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbLocationCoordSystem.Text);
                uint locationIndex;

                m_ObjScheme.AddObjectLocation(out objLocation,
                                                    coordSys,
                                                    chxRelativeToDTM.Checked,
                                                    out locationIndex,
                                                    ntxInsertAtIndex.GetUInt32());

                
                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("", McEx);
            }
        }

        private void frmNewObjectLocation_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}