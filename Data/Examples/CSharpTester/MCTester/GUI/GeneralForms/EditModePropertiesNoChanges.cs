using MapCore;
using MCTester.Controls;
using MCTester.GUI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCTester.General_Forms
{
    public partial class EditModePropertiesNoChanges : Form
    {
        private IDNMcEditMode m_EditMode;
        private IDNMcObjectScheme m_ObjectScheme;
        private DNSObjectOperationsParams m_SObjectOperationsParams;

        private EditModeProperties m_EditModeProperties;

        public EditModePropertiesNoChanges()
        {
            InitializeComponent();
            string[] arrayEMouseMoveUsageNames = Enum.GetNames(typeof(DNEMouseMoveUsage));
            List<string> listEMouseMoveUsageNames = arrayEMouseMoveUsageNames.ToList();
            listEMouseMoveUsageNames.Remove(DNEMouseMoveUsage._EMMU_TYPES.ToString());
            cmbMouseMoveUsageForMultiPointItem.Items.AddRange(listEMouseMoveUsageNames.ToArray());
            SetEnableButtons(false);
        }

        public EditModePropertiesNoChanges(IDNMcObjectScheme objectScheme) : this()
        {
            m_ObjectScheme = objectScheme;
            btnGetEditModeParameters_Click(null, null);

            btnChangeForOneOperation.Visible = false;
            btnChangeForAllOperation.Visible = false;
            btnGetObjectOperationsParams.Visible = false;
        }

        public EditModePropertiesNoChanges(IDNMcEditMode editMode, EditModeProperties editModeProperties) : this()
        {
            m_EditMode = editMode;
            m_EditModeProperties = editModeProperties;

            btnGetObjectOperationsParams_Click(null, null);

            btnGetEditModeParameters.Visible = false;
            btnSetEditModeParameters.Visible = false;
        }

        public double MaxRadiusWorld
        {
            get { return ntxMaxRadiusWorld.GetDouble(); }
            set { ntxMaxRadiusWorld.SetDouble(value); }
        }

        public double MaxRadiusScreen
        {
            get { return ntxMaxRadiusScreen.GetDouble(); }
            set { ntxMaxRadiusScreen.SetDouble(value); }
        }

        public double MaxRadiusImage
        {
            get { return ntxMaxRadiusImage.GetDouble(); }
            set { ntxMaxRadiusImage.SetDouble(value); }
        }

        private void LoadItem()
        {
            SetEnableButtons(false);
            if(m_ObjectScheme != null)
            {
                if (m_SObjectOperationsParams.fRotatePictureOffset != float.MaxValue)
                    chxIsFieldEnableRotatePicture.Checked = true;
                if (m_SObjectOperationsParams.f3DEditUtilityItemsOptionalScreenSize != float.MaxValue)
                    chxIsFieldEnableUtilityItems.Checked = true;
                if (m_SObjectOperationsParams.uPointAndLineClickTolerance != DNMcConstants._MC_EMPTY_ID)
                    chxIsFieldEnablePointAndLine.Checked = true;
                if (m_SObjectOperationsParams.uMaxNumberOfPoints != DNMcConstants._MC_EMPTY_ID)
                    chxIsFieldEnableMaxNumItems.Checked = true;
                if (m_SObjectOperationsParams.dMaxRadiusForImageCoordSys != double.MaxValue)
                    chxIsFieldEnableMaxRadiusImage.Checked = true;
                if (m_SObjectOperationsParams.dMaxRadiusForWorldCoordSys != double.MaxValue)
                    chxIsFieldEnableMaxRadiusWorld.Checked = true;
                if (m_SObjectOperationsParams.dMaxRadiusForScreenCoordSys != double.MaxValue)
                    chxIsFieldEnableMaxRadiusScreen.Checked = true;
                if (m_SObjectOperationsParams.bRectangleResizeRelativeToCenter != null)
                    chxIsFieldEnableRectangleResize.Checked = true;
                if (m_SObjectOperationsParams.b3DEditKeepScaleRatio != null)
                    chxIsFieldEnableKeepScale.Checked = true;
                if (m_SObjectOperationsParams.b3DEditLocalAxes != null)
                    chxIsFieldEnableUseLocalAxes.Checked = true;
                if (m_SObjectOperationsParams.bForceFinishOnMaxPoints != null)
                    chxIsFieldEnableForceMaxPoints.Checked = true;
                if (m_SObjectOperationsParams.eMouseMoveUsageForMultiPointItem != DNEMouseMoveUsage._EMMU_TYPES)
                    chxIsFieldEnableMouseMove.Checked = true;
            }

            ctrlEditModePermissions1.SetSObjectOperationsParams(m_SObjectOperationsParams, m_ObjectScheme != null) ;
            ctrlEditModeUtility1.SetSObjectOperationsParams(m_SObjectOperationsParams, m_ObjectScheme != null);

            ntbRotatePictureOffset.SetFloat(m_SObjectOperationsParams.fRotatePictureOffset);
           
            ntxMaxNumOfPoints.SetUInt32(m_SObjectOperationsParams.uMaxNumberOfPoints);
            if (m_SObjectOperationsParams.bForceFinishOnMaxPoints.HasValue)
                chxForceFinishOnMaxPoints.Checked = m_SObjectOperationsParams.bForceFinishOnMaxPoints.Value;

            MaxRadiusWorld = m_SObjectOperationsParams.dMaxRadiusForWorldCoordSys;
            MaxRadiusScreen = m_SObjectOperationsParams.dMaxRadiusForScreenCoordSys;
            MaxRadiusImage = m_SObjectOperationsParams.dMaxRadiusForImageCoordSys;

            cmbMouseMoveUsageForMultiPointItem.Text = m_SObjectOperationsParams.eMouseMoveUsageForMultiPointItem.ToString();
            ntxPointAndLineClickTolerance.SetUInt32(m_SObjectOperationsParams.uPointAndLineClickTolerance);

            if (m_SObjectOperationsParams.bRectangleResizeRelativeToCenter.HasValue)
                chxRectangleResizeRelativeToCenter.Checked = m_SObjectOperationsParams.bRectangleResizeRelativeToCenter.Value;

            if (m_SObjectOperationsParams.b3DEditLocalAxes.HasValue)
                chxLocalAxes.Checked = m_SObjectOperationsParams.b3DEditLocalAxes.Value;
            if (m_SObjectOperationsParams.b3DEditKeepScaleRatio.HasValue)
                chxKeepScaleRatio.Checked = m_SObjectOperationsParams.b3DEditKeepScaleRatio.Value;
            ntbUtilityItemsOptionalScreenSize.SetFloat(m_SObjectOperationsParams.f3DEditUtilityItemsOptionalScreenSize);
        }
        
        private void SetEnableButtons(bool isEnable)
        {
            foreach (TabPage tabPage in tcEditMode.TabPages)
            {
                foreach (Control control in tabPage.Controls)
                {
                    if (control is GroupBox)
                    {
                        foreach (Control innerControl in control.Controls)
                        {
                            if (!innerControl.Name.StartsWith("chxIsFieldEnable"))
                                innerControl.Enabled = isEnable;
                            else
                                (innerControl as CheckBox).Checked = false;
                        }
                    }
                    else if (control is CtrlEditModePermissions)
                        (control as CtrlEditModePermissions).SetEnableButtons(isEnable);
                    else if (control is CtrlEditModeUtility)
                        (control as CtrlEditModeUtility).SetEnableButtons(isEnable);
                    else if (!control.Name.StartsWith("chxIsFieldEnable"))
                        control.Enabled = isEnable;
                    else
                        (control as CheckBox).Checked = false;
                }
            }
            label6.Enabled = true;
        }

        private void btnChangeForOneOperation_Click(object sender, EventArgs e)
        {
            try
            {
                m_EditMode.ChangeObjectOperationsParams(ChangeObjectPropertiesOperation(), true);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ChangeObjectOperationsParams", McEx);
            }
            catch (Exception) { }
        }

        private void btnChangeForAllOperation_Click(object sender, EventArgs e)
        {
            try
            {
                m_EditMode.ChangeObjectOperationsParams(ChangeObjectPropertiesOperation(), false);
                
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ChangeObjectOperationsParams", McEx);
            }
            catch (Exception) { }
        }

        private DNSObjectOperationsParams ChangeObjectPropertiesOperation()
        {
            DNSObjectOperationsParams sObjectOperationsParams = new DNSObjectOperationsParams();
            ctrlEditModeUtility1.SaveItem();
            if (ctrlEditModeUtility1.IsUserCheckedUtilityLine)
            {
                sObjectOperationsParams.pUtilityLine = ctrlEditModeUtility1.UtilityLineItem;
                sObjectOperationsParams.bUtilityLineOverriden = true;
            }
            if (ctrlEditModeUtility1.IsUserCheckedUtilityPicture)
                sObjectOperationsParams.apUtilityPictures = ctrlEditModeUtility1.GetUtilityPictureItems().ToArray();
            if (ctrlEditModeUtility1.IsUserCheckedUtility3DItems)
                sObjectOperationsParams.ap3DEditUtilityItems = ctrlEditModeUtility1.GetUtility3DEditItems().ToArray();

            if (ctrlEditModePermissions1.IsUserCheckedPermissions)
            {
                sObjectOperationsParams.uPermissions = (uint)ctrlEditModePermissions1.PermissionsBitArray;
                if (ctrlEditModePermissions1.PermissionsBitArray == DNEPermission._EEMP_NONE)
                {
                    tcEditMode.SelectedIndex = 1;
                    MessageBox.Show("you should select value from the permission list," + Environment.NewLine + " fix it and click again", "Invalid permission value");
                    throw new Exception();
                }
            }

            List<DNSPermissionHiddenIcons> currentPermissionHiddenIcons = ctrlEditModePermissions1.SaveCurrentPermissionHiddenIcons();
            if (currentPermissionHiddenIcons != null)
                sObjectOperationsParams.aPermissionsWithHiddenIcons = currentPermissionHiddenIcons.ToArray();

            if (chxIsFieldEnableMaxRadiusWorld.Enabled)
                sObjectOperationsParams.dMaxRadiusForWorldCoordSys = MaxRadiusWorld;
            if (chxIsFieldEnableMaxRadiusScreen.Enabled)
                sObjectOperationsParams.dMaxRadiusForScreenCoordSys = MaxRadiusScreen;
            if (chxIsFieldEnableMaxRadiusImage.Enabled)
                sObjectOperationsParams.dMaxRadiusForImageCoordSys = MaxRadiusImage;
            if (chxIsFieldEnableMouseMove.Checked)
                sObjectOperationsParams.eMouseMoveUsageForMultiPointItem = (DNEMouseMoveUsage)Enum.Parse(typeof(DNEMouseMoveUsage), cmbMouseMoveUsageForMultiPointItem.Text);
            if (chxIsFieldEnableUtilityItems.Checked)
                sObjectOperationsParams.f3DEditUtilityItemsOptionalScreenSize = ntbUtilityItemsOptionalScreenSize.GetFloat();
            if (chxIsFieldEnableRotatePicture.Checked)
                sObjectOperationsParams.fRotatePictureOffset = ntbRotatePictureOffset.GetFloat();
            if (chxIsFieldEnableMaxNumItems.Checked)
                sObjectOperationsParams.uMaxNumberOfPoints = ntxMaxNumOfPoints.GetUInt32();
            if (chxIsFieldEnablePointAndLine.Checked)
                sObjectOperationsParams.uPointAndLineClickTolerance = ntxPointAndLineClickTolerance.GetUInt32();
            if (chxIsFieldEnableForceMaxPoints.Checked)
                sObjectOperationsParams.bForceFinishOnMaxPoints = chxForceFinishOnMaxPoints.Checked;
            if (chxIsFieldEnableRectangleResize.Checked)
                sObjectOperationsParams.bRectangleResizeRelativeToCenter = chxRectangleResizeRelativeToCenter.Checked;
            if (chxIsFieldEnableUseLocalAxes.Checked)
                sObjectOperationsParams.b3DEditLocalAxes = chxLocalAxes.Checked;
            if (chxIsFieldEnableKeepScale.Checked)
                sObjectOperationsParams.b3DEditKeepScaleRatio = chxKeepScaleRatio.Checked;

            return sObjectOperationsParams;
        }

        private void btnGetObjectOperationsParams_Click(object sender, EventArgs e)
        {
            try
            {
                m_SObjectOperationsParams = m_EditMode.GetObjectOperationsParams();
                LoadItem();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetObjectOperationsParams", McEx);
            }
        }

        private void SetEnableControl(CheckBox chx, Control control, Label label = null)
        {
            if (label != null)
                label.Enabled = chx.Checked;
            control.Enabled = chx.Checked;
        }

        private void chxIsFieldEnableMaxRadiusWorld_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableControl(chxIsFieldEnableMaxRadiusWorld, ntxMaxRadiusWorld, label20);
        }

        private void chxIsFieldEnableMaxRadiusScreen_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableControl( chxIsFieldEnableMaxRadiusScreen, ntxMaxRadiusScreen,label15 );
        }

        private void chxIsFieldEnableMaxRadiusImage_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableControl(chxIsFieldEnableMaxRadiusImage, ntxMaxRadiusImage, label16);
        }

        private void chxIsFieldEnableRotatePicture_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableControl( chxIsFieldEnableRotatePicture, ntbRotatePictureOffset,label28 );
        }

        private void chxIsFieldEnablePointAndLine_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableControl( chxIsFieldEnablePointAndLine, ntxPointAndLineClickTolerance, label17);
        }

        private void chxIsFieldEnableRectangleResize_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableControl(chxIsFieldEnableRectangleResize, chxRectangleResizeRelativeToCenter, label1);
        }

        private void chxIsFieldEnableMaxNumItems_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableControl(chxIsFieldEnableMaxNumItems, ntxMaxNumOfPoints, label3);
        }

        private void chxIsFieldEnableForceMaxPoints_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableControl(chxIsFieldEnableForceMaxPoints, chxForceFinishOnMaxPoints, label4);
        }

        private void chxIsFieldEnableMouseMove_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableControl(chxIsFieldEnableMouseMove, cmbMouseMoveUsageForMultiPointItem, label2);
        }

        private void chxIsFieldEnableUseLocalAxes_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableControl(chxIsFieldEnableUseLocalAxes, chxLocalAxes, label7);
        }

        private void chxIsFieldEnableKeepScale_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableControl(chxIsFieldEnableKeepScale, chxKeepScaleRatio, label5);
        }

        private void chxIsFieldEnableUtilityItems_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableControl(chxIsFieldEnableUtilityItems, ntbUtilityItemsOptionalScreenSize, label27);
        }

        private void btnGetEditModeParameters_Click(object sender, EventArgs e)
        {
            try
            {
                m_SObjectOperationsParams = m_ObjectScheme.GetEditModeParams();

                LoadItem();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetEditModeParams", McEx);
            }
        }

        private void btnSetEditModeParameters_Click(object sender, EventArgs e)
        {
            try
            {
                m_ObjectScheme.SetEditModeParams(ChangeObjectPropertiesOperation());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetEditModeParams", McEx);
            }
            catch (Exception) { }
        }

        private void SetCheckBoxValueByLabel(CheckBox checkBox)
        {
            checkBox.Checked = !checkBox.Checked;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            SetCheckBoxValueByLabel(chxRectangleResizeRelativeToCenter);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            SetCheckBoxValueByLabel(chxForceFinishOnMaxPoints);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            SetCheckBoxValueByLabel(chxLocalAxes);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            SetCheckBoxValueByLabel(chxKeepScaleRatio);
        }

        private void EditModePropertiesNoChanges_FormClosed(object sender, FormClosedEventArgs e)
        {
           // this.Parent
        }

        private void EditModePropertiesNoChanges_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_EditModeProperties != null)
                m_EditModeProperties.LoadItem();
        }
    }
}
