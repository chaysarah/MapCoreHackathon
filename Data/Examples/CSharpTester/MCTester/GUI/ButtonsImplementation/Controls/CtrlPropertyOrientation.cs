using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Controls
{
    public partial class CtrlPropertyOrientation : CtrlPropertyBase
    {
        public CtrlPropertyOrientation()
        {
            InitializeComponent();
        }

        public DNSMcRotation GetRegRotationVal()
        {
            return new DNSMcRotation(ctrl3DRegOrientation.Yaw,
                                        ctrl3DRegOrientation.Pitch,
                                        ctrl3DRegOrientation.Roll,
                                        chkRegRelativeToCurrOrientation.Checked);
        }

        public void SetRegRotationVal(DNSMcRotation value)
        {
            ctrl3DRegOrientation.Yaw = value.fYaw;
            ctrl3DRegOrientation.Pitch = value.fPitch;
            ctrl3DRegOrientation.Roll = value.fRoll;
            chkRegRelativeToCurrOrientation.Checked = value.bRelativeToCurrOrientation;
        }

        public DNSMcRotation GetSelRotationVal()
        {
            return new DNSMcRotation(ctrl3DSelOrientation.Yaw,
                                        ctrl3DSelOrientation.Pitch,
                                        ctrl3DSelOrientation.Roll,
                                        chkSelRelativeToCurrOrientation.Checked);
        }

        public void SetSelRotationVal(DNSMcRotation value)
        {

            ctrl3DSelOrientation.Yaw = value.fYaw;
            ctrl3DSelOrientation.Pitch = value.fPitch;
            ctrl3DSelOrientation.Roll = value.fRoll;
            chkSelRelativeToCurrOrientation.Checked = value.bRelativeToCurrOrientation;
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }

        private IDNMcPhysicalItem m_phisicalItem;
        public IDNMcPhysicalItem PhisicalItem
        {
            get { return m_phisicalItem; }
            set
            {
                m_phisicalItem = value;
                if (m_phisicalItem!=null)
                {
                    btnApply.Visible = true;
                }
            }
        }
        
        protected virtual void btnApply_Click(object sender, EventArgs e)
        {
            if (PhisicalItem == null)
                return;

            try
            {
                PhisicalItem.SetRotation(GetRegRotationVal(), RegPropertyID);

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
                //MessageBox.Show("Updated successfully","success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetRotation", McEx);
            }
        }
    }
}
