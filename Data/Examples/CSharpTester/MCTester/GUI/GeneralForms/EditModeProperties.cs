using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers.MapWorld;
using MCTester.MapWorld.MapUserControls;
using MCTester.General_Forms;
using System.Collections;
using System.Reflection;
using System.Linq;
using MapCore.Common;

namespace MCTester.GUI.Forms
{
    public partial class EditModeProperties : Form
    {
        private IDNMcEditMode m_EditMode;

        private IDNMcLineItem m_CurrDistanceDirectionMeasureLine;
        private IDNMcTextItem m_CurrDistanceDirectionMeasureText;
        private IDNMcRectangleItem m_CurrCurrDynamicZoomRect;

        private List<string> m_EditItemText = new List<string>();
        private List<IDNMcObjectSchemeItem> m_EditItemValue = new List<IDNMcObjectSchemeItem>();

        private List<string> m_PictureItemText = new List<string>();
        private List<IDNMcPictureItem> m_PictureItemValue = new List<IDNMcPictureItem>();

        private List<string> m_RectangleItemText = new List<string>();
        private List<IDNMcRectangleItem> m_RectangleItemValue = new List<IDNMcRectangleItem>();
        private List<string> m_LineItemText = new List<string>();
        private List<IDNMcLineItem> m_LineItemValue = new List<IDNMcLineItem>();
        private List<string> m_TextItemText = new List<string>();
        private List<IDNMcTextItem> m_TextItemValue = new List<IDNMcTextItem>();

        private List<string> m_IntersectionTargetText = new List<string>();
        private List<DNEIntersectionTargetType> m_IntersectionTargetValues = new List<DNEIntersectionTargetType>();

        public EditModeProperties()
        {
            InitializeComponent();

            rbUseMagnetic.BringToFront();

            m_EditMode = MCTMapFormManager.MapForm.EditMode;
            cmbDynamicZoomOperation.Items.AddRange(Enum.GetNames(typeof(DNESetVisibleArea3DOperation)));

            List<string> listEMouseMoveUsageNames = Enum.GetNames(typeof(DNEMouseMoveUsage)).ToList();
            listEMouseMoveUsageNames.Remove(DNEMouseMoveUsage._EMMU_TYPES.ToString());
            cmbMouseMoveUsageForMultiPointItem.Items.AddRange(listEMouseMoveUsageNames.ToArray());

            string[] stepTypes = Enum.GetNames(typeof(DNEKeyStepType));
            foreach (string step in stepTypes)
            {
                dgvKeyStep.Rows.Add();
                dgvKeyStep[0, dgvKeyStep.Rows.GetLastRow(DataGridViewElementStates.Visible)].Value = step;
            }

            m_IntersectionTargetText = new List<string>(Enum.GetNames(typeof(DNEIntersectionTargetType)));
            m_IntersectionTargetValues = new List<DNEIntersectionTargetType>(
                (DNEIntersectionTargetType[])Enum.GetValues(typeof(DNEIntersectionTargetType)));

            m_IntersectionTargetText.RemoveAt(0);
            m_IntersectionTargetValues.RemoveAt(0);

            lstIntersectionTargets.Items.AddRange(m_IntersectionTargetText.ToArray());
            chxObjectSchemeParams.Checked = m_EditMode.AutoChangeObjectOperationsParams;

            SetDistanceMeasureTextParams(new DNSMeasureTextParams());
            SetAngleMeasureTextParams(new DNSMeasureTextParams());
            SetHeightMeasureTextParams(new DNSMeasureTextParams());

            LoadItem();
        }

        public void LoadItem()
        {
            //editMode functions for any map type
            try
            {
                chxAutoScroll.Checked = m_EditMode.AutoScrollMode;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("AutoScrollMode", McEx);
            }

            try
            {
                ntxAutoScrollMargineSize.SetInt(m_EditMode.MarginSize);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("MarginSize", McEx);
            }

            try
            {
                ntbRotatePictureOffset.SetFloat(m_EditMode.GetRotatePictureOffset());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetRotatePictureOffset", McEx);
            }

            try
            {
                uint MaxPoints;
                bool ForceFinish;
                m_EditMode.GetMaxNumberOfPoints(out MaxPoints, out ForceFinish);
                ntxMaxNumOfPoints.SetUInt32(MaxPoints);
                chxForceFinishOnMaxPoints.Checked = ForceFinish;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetMaxNumberOfPoints", McEx);
            }

            try
            {
                MaxRadiusWorld = m_EditMode.GetMaxRadius(DNEMcPointCoordSystem._EPCS_WORLD);
                MaxRadiusScreen = m_EditMode.GetMaxRadius(DNEMcPointCoordSystem._EPCS_SCREEN);
                MaxRadiusImage = m_EditMode.GetMaxRadius(DNEMcPointCoordSystem._EPCS_IMAGE);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetMaxRadius", McEx);
            }

            try
            {
                ntxLastExitStatus.SetInt(m_EditMode.LastExitStatus);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("LastExitStatus", McEx);
            }

            try
            {
                cmbMouseMoveUsageForMultiPointItem.Text = m_EditMode.MouseMoveUsageForMultiPointItem.ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("MouseMoveUsageForMultiPointItem", McEx);
            }

            try
            {
                ntxPointAndLineClickTolerance.SetUInt32(m_EditMode.PointAndLineClickTolerance);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("PointAndLineClickTolerance", McEx);
            }

            try
            {
                int i = 0;
                foreach (DNEKeyStepType stepType in Enum.GetValues(typeof(DNEKeyStepType)))
                {
                    dgvKeyStep[1, i++].Value = m_EditMode.GetKeyStep(stepType);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetKeyStep", McEx);
            }

            ctrlEditModeUtility1.EditMode = m_EditMode; // cues to load item

            try
            {
                chxRectangleResizeRelativeToCenter.Checked = m_EditMode.RectangleResizeRelativeToCenter;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("RectangleResizeRelativeToCenter", McEx);
            }

            try
            {
                chxAutoSuppressSightPresentationMapTilesWebRequests.Checked = m_EditMode.AutoSuppressQueryPresentationMapTilesWebRequests;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("AutoSuppressQueryPresentationMapTilesWebRequests", McEx);
            }

            //Permissions
            ctrlEditModePermissions1.EditMode = m_EditMode;

            //Local edit mode parameters 
            chxEnableMultiPointItemEditNewPoint.Checked = EditModePropertiesBase.EnableAddingNewPointsForMultiPointItem;
            chxEnableDistanceDirectionMeasure.Checked = EditModePropertiesBase.EnableDistanceDirectionMeasureForMultiPointItem;
            chxDrawLine.Checked = EditModePropertiesBase.DrawLine;
            chxOneOperationOnly.Checked = EditModePropertiesBase.OneOperationOnly;
            chxWaitForMouseClick.Checked = EditModePropertiesBase.WaitForMouseClick;
            chxDiscard.Checked = EditModePropertiesBase.DiscardChanges;

            ntxDynamicZoomMinScale.SetFloat(EditModePropertiesBase.DynamicZoomMinScale);
            chxDynamicZoomWaitForClick.Checked = EditModePropertiesBase.DynamicZoomWaitForClick;

            try
            {
                DNSDistanceDirectionMeasureParams Params = new DNSDistanceDirectionMeasureParams();

                Params = m_EditMode.GetDistanceDirectionMeasureParams();
                if (Params != null)
                {
                    if (Params.pDistanceTextParams != null)
                    {
                        gbDistance.Checked = true;
                        SetDistanceMeasureTextParams(Params.pDistanceTextParams);
                    }
                    else
                        gbDistance.Checked = false;

                    if (Params.pAngleTextParams != null)
                    {
                        gbAngle.Checked = true;
                        SetAngleMeasureTextParams(Params.pAngleTextParams);
                    }
                    else
                        gbAngle.Checked = false;

                    if (Params.pHeightTextParams != null)
                    {
                        gbHeight.Checked = true;
                        SetHeightMeasureTextParams(Params.pHeightTextParams);
                    }
                    else
                        gbHeight.Checked = false;

                    CurrDistanceDirectionMeasureLine = Params.pLine;
                    CurrDistanceDirectionMeasureText = Params.pText;

                    rbUseMagnetic.Checked = Params.bUseMagneticAzimuth;

                    if (Params.pDirectionCoordSys != null)
                    {
                        rbGridCoordinateSystem.Checked = true;
                        ctrlGridCoordinateSystemDistanceDirectionMeasure.GridCoordinateSystem = Params.pDirectionCoordSys;
                    }
                    else if (Params.bUseMagneticAzimuth)
                    {
                        rbUseMagnetic.Checked = true;
                        if (Params.pDate.HasValue == false)
                            rbSelectCurrentTime.Checked = true;
                        else
                        {
                            rbSelectTime.Checked = true;
                            dateTimePickerDate.Value = Params.pDate.Value;
                        }
                    }
                    else
                        rbGeographicAzimuth.Checked = true;


                    
                }
               
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetDistanceDirectionMeasureParams", McEx);
            }

            chxShowResults.Checked = EditModePropertiesBase.ShowResult;
            chxWaitForClick.Checked = EditModePropertiesBase.P2PWaitForClick;


            CurrDynamicZoomRect = EditModePropertiesBase.DynamicZoomRectangle;
            cmbDynamicZoomOperation.Text = EditModePropertiesBase.DynamicZoomOperation.ToString();

            //editMode functions only in 3D map
            try
            {
                double MinPitch, MaxPitch;
                m_EditMode.GetCameraPitchRange(out MinPitch, out MaxPitch);
                ntxCameraMinPitchRange.SetDouble(MinPitch);
                ntxCameraMaxPitchRange.SetDouble(MaxPitch);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCameraPitchRange", McEx);
            }

            try
            {
                DNEIntersectionTargetType intersectionType;
                intersectionType = m_EditMode.GetIntersectionTargets();
                for (int i = 0; i < m_IntersectionTargetText.Count; i++)
                {
                    DNEIntersectionTargetType flag = m_IntersectionTargetValues[i];
                    if ((intersectionType & flag) == flag)
                    {
                        lstIntersectionTargets.SetItemChecked(i, true);
                    }
                    else
                    {
                        lstIntersectionTargets.SetItemChecked(i, false);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetIntersectionTargets", McEx);
            }

            try
            {
                DNS3DEditParams editParams = m_EditMode.Get3DEditParams();
                chxLocalAxes.Checked = editParams.bLocalAxes;
                chxKeepScaleRatio.Checked = editParams.bKeepScaleRatio;
                ntbUtilityItemsOptionalScreenSize.SetFloat(editParams.fUtilityItemsOptionalScreenSize);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Get3DEditParams", McEx);
            }

        }

        private void SaveItem()
        {
            if (rbGridCoordinateSystem.Checked && ctrlGridCoordinateSystemDistanceDirectionMeasure.GridCoordinateSystem == null)
            {
                MessageBox.Show("Missing Grid Coordinate System, You should select one from the list", "Distance Direction Measure - Grid Coordinate System");
                return;
            }
            //editMode functions for any map type
            try
            {
                m_EditMode.AutoScroll(chxAutoScroll.Checked, ntxAutoScrollMargineSize.GetInt32());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("AutoScroll", McEx);
            }

            try
            {
                m_EditMode.SetMaxNumberOfPoints(ntxMaxNumOfPoints.GetUInt32(), chxForceFinishOnMaxPoints.Checked);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetMaxNumberOfPoints", McEx);
            }

           

            try
            {
                m_EditMode.SetMaxRadius(MaxRadiusWorld, DNEMcPointCoordSystem._EPCS_WORLD);
                m_EditMode.SetMaxRadius(MaxRadiusScreen, DNEMcPointCoordSystem._EPCS_SCREEN);
                m_EditMode.SetMaxRadius(MaxRadiusImage, DNEMcPointCoordSystem._EPCS_IMAGE);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetMaxRadius", McEx);
            }

            try
            {
                m_EditMode.SetRotatePictureOffset(ntbRotatePictureOffset.GetFloat());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetRotatePictureOffset", McEx);
            }

            try
            {
                m_EditMode.MouseMoveUsageForMultiPointItem = (DNEMouseMoveUsage)Enum.Parse(typeof(DNEMouseMoveUsage), cmbMouseMoveUsageForMultiPointItem.Text);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("MouseMoveUsageForMultiPointItem", McEx);
            }

            try
            {
                m_EditMode.PointAndLineClickTolerance = ntxPointAndLineClickTolerance.GetUInt32();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("PointAndLineClickTolerance", McEx);
            }

            try
            {
                for (int i = 0; i < dgvKeyStep.Rows.Count; i++)
                {
                    DNEKeyStepType stepType = (DNEKeyStepType)Enum.Parse(typeof(DNEKeyStepType), dgvKeyStep[0, i].Value.ToString());
                    m_EditMode.SetKeyStep(stepType, float.Parse(dgvKeyStep[1, i].Value.ToString()));
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetKeyStep", McEx);
            }

            ctrlEditModeUtility1.SaveItem();

            try
            {
                m_EditMode.RectangleResizeRelativeToCenter = chxRectangleResizeRelativeToCenter.Checked;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("RectangleResizeRelativeToCenter", McEx);
            }

            try
            {
                m_EditMode.AutoSuppressQueryPresentationMapTilesWebRequests = chxAutoSuppressSightPresentationMapTilesWebRequests.Checked;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("AutoSuppressQueryPresentationMapTilesWebRequests", McEx);
            }

            try
            {
                m_EditMode.AutoChangeObjectOperationsParams = chxObjectSchemeParams.Checked;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("AutoSuppressSightPresentationMapTilesWebRequests", McEx);
            }

            //Permissions
            try
            {
                m_EditMode.SetPermissions(ctrlEditModePermissions1.PermissionsBitArray);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetPermissions", McEx);
            }

            //Local edit mode parameters

            EditModePropertiesBase.EnableAddingNewPointsForMultiPointItem = chxEnableMultiPointItemEditNewPoint.Checked;
            EditModePropertiesBase.EnableDistanceDirectionMeasureForMultiPointItem = chxEnableDistanceDirectionMeasure.Checked;
            EditModePropertiesBase.DrawLine = chxDrawLine.Checked;
            EditModePropertiesBase.OneOperationOnly = chxOneOperationOnly.Checked;
            EditModePropertiesBase.WaitForMouseClick = chxWaitForMouseClick.Checked;

            try
            {
                DNSDistanceDirectionMeasureParams Params = new DNSDistanceDirectionMeasureParams();

                if (gbDistance.Checked)
                {
                    DNSMeasureTextParams measureTextParams = new DNSMeasureTextParams();
                    measureTextParams.UnitsName = new DNMcVariantString(txtDistanceUnitsName.Text, chxDistanceIsUnicode.Checked);
                    measureTextParams.dUnitsFactor = ntxDistanceFactor.GetDouble();
                    measureTextParams.uNumDigitsAfterDecimalPoint = ntxDistanceDigits.GetUInt32();
                    Params.pDistanceTextParams = measureTextParams;
                }

                if (gbAngle.Checked)
                {
                    DNSMeasureTextParams AngleTextParams = new DNSMeasureTextParams();
                    AngleTextParams.UnitsName = new DNMcVariantString(txtAngleUnitsName.Text, chxAngleIsUnicode.Checked);
                    AngleTextParams.dUnitsFactor = ntxAngleFactor.GetDouble();
                    AngleTextParams.uNumDigitsAfterDecimalPoint = ntxAngleDigits.GetUInt32();
                    Params.pAngleTextParams = AngleTextParams;
                }

                if (gbHeight.Checked)
                {
                    DNSMeasureTextParams HeightTextParams = new DNSMeasureTextParams();
                    HeightTextParams.UnitsName = new DNMcVariantString(txtHeightUnitsName.Text, chxHeightIsUnicode.Checked);
                    HeightTextParams.dUnitsFactor = ntxHeightFactor.GetDouble();
                    HeightTextParams.uNumDigitsAfterDecimalPoint = ntxHeightDigits.GetUInt32();
                    Params.pHeightTextParams = HeightTextParams;
                }

                Params.pText = CurrDistanceDirectionMeasureText;
                Params.pLine = CurrDistanceDirectionMeasureLine;
                Params.bUseMagneticAzimuth = rbUseMagnetic.Checked;

                if (!rbGeographicAzimuth.Checked)
                {
                    if (rbGridCoordinateSystem.Checked)
                    {
                        Params.pDirectionCoordSys = ctrlGridCoordinateSystemDistanceDirectionMeasure.GridCoordinateSystem;
                    }
                    else if (rbUseMagnetic.Checked)
                    {
                        if (rbSelectTime.Checked)
                            Params.pDate = new DateTime(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month, dateTimePickerDate.Value.Day, 0, 0, 0);
                    }
                }

               

                m_EditMode.SetDistanceDirectionMeasureParams(Params);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetDistanceDirectionMeasureParams", McEx);
            }

            EditModePropertiesBase.ShowResult = chxShowResults.Checked;
            EditModePropertiesBase.P2PWaitForClick = chxWaitForClick.Checked;
            EditModePropertiesBase.DiscardChanges = chxDiscard.Checked;

            EditModePropertiesBase.DynamicZoomMinScale = ntxDynamicZoomMinScale.GetFloat();
            EditModePropertiesBase.DynamicZoomWaitForClick = chxDynamicZoomWaitForClick.Checked;
            EditModePropertiesBase.DynamicZoomRectangle = CurrDynamicZoomRect;
            EditModePropertiesBase.DynamicZoomOperation = (DNESetVisibleArea3DOperation)Enum.Parse(typeof(DNESetVisibleArea3DOperation), cmbDynamicZoomOperation.Text);

            //editMode functions only in 3D map
            try
            {
                m_EditMode.SetCameraPitchRange(ntxCameraMinPitchRange.GetDouble(), ntxCameraMaxPitchRange.GetDouble());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetCameraPitchRange", McEx);
            }

            try
            {
                DNEIntersectionTargetType flags = DNEIntersectionTargetType._EITT_NONE;
                for (int i = 0; i < lstIntersectionTargets.Items.Count; i++)
                {
                    if (lstIntersectionTargets.GetItemChecked(i))
                    {
                        flags |= m_IntersectionTargetValues[i];
                    }
                }
                m_EditMode.SetIntersectionTargets(flags);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetIntersectionTargets", McEx);
            }

            try
            {
                DNS3DEditParams editParams = new DNS3DEditParams();
                editParams.bLocalAxes = chxLocalAxes.Checked;
                editParams.bKeepScaleRatio = chxKeepScaleRatio.Checked;
                editParams.fUtilityItemsOptionalScreenSize = ntbUtilityItemsOptionalScreenSize.GetFloat(); // TODO: support fUtilityItemsOptionalScreenSize!

                m_EditMode.Set3DEditParams(editParams);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Set3DEditParams", McEx);
            }
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

        public IDNMcRectangleItem CurrDynamicZoomRect
        {
            get { return m_CurrCurrDynamicZoomRect; }
            set
            {

                m_CurrCurrDynamicZoomRect = value;

                if (value != null)
                    lblDynamicZoomRect.Text = Manager_MCNames.GetNameByObject(value, "Rectangle");
                else
                    lblDynamicZoomRect.Text = "Default";
            }
        }

        public IDNMcLineItem CurrDistanceDirectionMeasureLine
        {
            get { return m_CurrDistanceDirectionMeasureLine; }
            set
            {
                m_CurrDistanceDirectionMeasureLine = value;

                if (value != null)
                    lblDistanceDirectionMeasureLine.Text = Manager_MCNames.GetNameByObject(value, "Line");
                else
                    lblDistanceDirectionMeasureLine.Text = "Default";
            }
        }

        public IDNMcTextItem CurrDistanceDirectionMeasureText
        {
            get { return m_CurrDistanceDirectionMeasureText; }
            set
            {
                m_CurrDistanceDirectionMeasureText = value;

                if (value != null)
                    lblDistanceDirectionMeasureText.Text = Manager_MCNames.GetNameByObject(value, "Text");
                else
                    lblDistanceDirectionMeasureText.Text = "Default";
            }
        }

        private void btnEditmodePropertiesOK_Click(object sender, EventArgs e)
        {
            SaveItem();
            this.Close();
        }

        private void btnDynamicZoomRectangle_Click(object sender, EventArgs e)
        {
            frmStandaloneItemsList StandaloneItemsListForm = new frmStandaloneItemsList(DNEObjectSchemeNodeType._RECTANGLE_ITEM);
            if (StandaloneItemsListForm.ShowDialog() == DialogResult.OK)
            {
                if (StandaloneItemsListForm.CurrItem != null)
                    CurrDynamicZoomRect = (IDNMcRectangleItem)StandaloneItemsListForm.CurrItem;
                else
                    CurrDynamicZoomRect = null;
            }
        }

        private void btnDistanceDirectionMeasureText_Click(object sender, EventArgs e)
        {
            frmStandaloneItemsList StandaloneItemsListForm = new frmStandaloneItemsList(DNEObjectSchemeNodeType._TEXT_ITEM);
            if (StandaloneItemsListForm.ShowDialog() == DialogResult.OK)
            {
                if (StandaloneItemsListForm.CurrItem != null)
                    CurrDistanceDirectionMeasureText = (IDNMcTextItem)StandaloneItemsListForm.CurrItem;
                else
                    CurrDistanceDirectionMeasureText = null;
            }
        }

        private void btnDistanceDirectionMeasureLine_Click(object sender, EventArgs e)
        {
            frmStandaloneItemsList StandaloneItemsListForm = new frmStandaloneItemsList(DNEObjectSchemeNodeType._LINE_ITEM);
            if (StandaloneItemsListForm.ShowDialog() == DialogResult.OK)
            {
                if (StandaloneItemsListForm.CurrItem != null)
                    CurrDistanceDirectionMeasureLine = (IDNMcLineItem)StandaloneItemsListForm.CurrItem;
                else
                    CurrDistanceDirectionMeasureLine = null;
            }
        }

        private void GbHeight_CheckedChanged(object sender, System.EventArgs e)
        {
            /*if (gbHeight.Checked)
            {
                SetHeightMeasureTextParams(new DNSMeasureTextParams());
            }*/
        }

        private void gbAngle_CheckedChanged(object sender, System.EventArgs e)
        {
           /* if (gbAngle.Checked)
            {
                SetAngleMeasureTextParams(new DNSMeasureTextParams());
            }*/
        }

        private void gbDistance_CheckedChanged(object sender, System.EventArgs e)
        {
            /*if (gbDistance.Checked)
            {
                SetDistanceMeasureTextParams(new DNSMeasureTextParams());
            }*/
        }

        private void SetHeightMeasureTextParams(DNSMeasureTextParams measureTextParams)
        {
            if (measureTextParams.UnitsName.astrStrings != null && measureTextParams.UnitsName.astrStrings.Length > 0)
            {
                txtHeightUnitsName.Text = measureTextParams.UnitsName.ToString();
            }
            chxHeightIsUnicode.Checked = measureTextParams.UnitsName.bIsUnicode;
            ntxHeightDigits.SetUInt32(measureTextParams.uNumDigitsAfterDecimalPoint);
            ntxHeightFactor.SetDouble(measureTextParams.dUnitsFactor);
        }

        private void SetDistanceMeasureTextParams(DNSMeasureTextParams measureTextParams)
        {
            if (measureTextParams.UnitsName.astrStrings != null && measureTextParams.UnitsName.astrStrings.Length > 0)
            { txtDistanceUnitsName.Text = measureTextParams.UnitsName.ToString(); }
            chxDistanceIsUnicode.Checked = measureTextParams.UnitsName.bIsUnicode;
            ntxDistanceDigits.SetUInt32(measureTextParams.uNumDigitsAfterDecimalPoint);
            ntxDistanceFactor.SetDouble(measureTextParams.dUnitsFactor);
        }

        private void SetAngleMeasureTextParams(DNSMeasureTextParams measureTextParams)
        {
            if (measureTextParams.UnitsName.astrStrings != null && measureTextParams.UnitsName.astrStrings.Length > 0)
            { txtAngleUnitsName.Text = measureTextParams.UnitsName.ToString(); }
            chxAngleIsUnicode.Checked = measureTextParams.UnitsName.bIsUnicode;
            ntxAngleDigits.SetUInt32(measureTextParams.uNumDigitsAfterDecimalPoint);
            ntxAngleFactor.SetDouble(measureTextParams.dUnitsFactor);
        }

        private void btnOpenEditModePropertiesNoChanges_Click(object sender, EventArgs e)
        {
            EditModePropertiesNoChanges editModePropertieNoChanges = new EditModePropertiesNoChanges(m_EditMode, this);
            editModePropertieNoChanges.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadItem();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveItem();
        }

        private void rbUseMagnetic_CheckedChanged(object sender, EventArgs e)
        {
            EnableGridAndDateControls();
        }

        private void rbGridCoordinateSystem_CheckedChanged(object sender, EventArgs e)
        {
            EnableGridAndDateControls();
            if(rbGridCoordinateSystem.Checked && ctrlGridCoordinateSystemDistanceDirectionMeasure.GridCoordinateSystem == null)
                ctrlGridCoordinateSystemDistanceDirectionMeasure.SetSelected(0);
        }

        private void rbSelectTime_CheckedChanged(object sender, EventArgs e)
        {
            EnableGridAndDateControls();
        }

        private void EnableGridAndDateControls()
        {
            ctrlGridCoordinateSystemDistanceDirectionMeasure.Enabled = rbGridCoordinateSystem.Checked;
            gbMagnetic.Enabled = rbUseMagnetic.Checked;
            dateTimePickerDate.Enabled = rbSelectTime.Checked;
            if (rbUseMagnetic.Checked && rbSelectTime.Checked == false)
                rbSelectCurrentTime.Checked = true;
        }

        private void EditModeProperties_FormClosed(object sender, FormClosedEventArgs e)
        {
            ctrlEditModePermissions1.RemoveTempPoints();
        }
    }
}