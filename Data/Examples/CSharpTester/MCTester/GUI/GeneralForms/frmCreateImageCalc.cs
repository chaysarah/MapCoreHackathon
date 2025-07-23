using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;
using MapCore;
using MCTester.MapWorld.WizardForms;
using MCTester.Managers;
using MCTester.Managers.MapWorld;
using MapCore.Common;

namespace MCTester.General_Forms
{
    public partial class frmCreateImageCalc : Form
    {
        private IDNMcImageCalc m_ImageCalc;
        private IDNMcDtmMapLayer m_DtmLayer;
        private AddLayerForm addDtmLayer;
        private IDNMcImageCalc m_SelectedImageCalc = null;
        private List<string> m_lImageCalcText = new List<string>();
        private List<IDNMcImageCalc> m_lImageCalcTag = new List<IDNMcImageCalc>();
        private TerrainWizzardForm mTerrainWizFrm;
        private List<TerrainWizzardForm> m_lstTerrainWizzardForm;
        private List<IDNMcMapTerrain> m_SelectedTerrains = new List<IDNMcMapTerrain>();

        public frmCreateImageCalc()
        {
            InitializeComponent();
            cmbImageType.Items.AddRange(Enum.GetNames(typeof(DNEImageType)));
            cmbImageType.Text = DNEImageType._EIT_NONE.ToString();

            gbCameraParams.Enabled = true;
            gbImageWorkingArea.Enabled = false;
            m_DtmLayer = null;
            m_lstTerrainWizzardForm = new List<TerrainWizzardForm>();
            rbDtm.Checked = true;
                ResetDtmTerrain();

            /*      ntxPixelesNumX.SetInt(10000);
                   ntxPixelesNumY.SetInt(7472);
                   ntxCameraOpeningAngleX.SetDouble(0.45500000000000002);
                   ctrlCameraLocation.SetVector3D( new DNSMcVector3D(35.636005401611328, 32.773555755615234, 3151.4);
                   ntxCameraPitch.SetDouble(-64.345159071317994);
                   ntxCameraRoll.SetDouble(0.60151064939100252);
                   ntxCameraYaw.SetDouble(38.741130701060001);*/

        }

        public frmCreateImageCalc(IDNMcImageCalc imageCalc) : this()
        {
            m_ImageCalc = imageCalc;

            gbCameraParams.Enabled = false;
            gbImageWorkingArea.Enabled = true;
            groupBox1.Visible = false;
            btnOK.Text = "Close";
            FillFormData();
        }

        private void FillFormData()
        {
            if (ImageCalc != null)
            {
                try
                {
                    cmbImageType.Text = ImageCalc.GetImageType().ToString();
                    if (ImageCalc.GetImageType() == DNEImageType._EIT_FRAME)
                    {
                        ctrlCameraParams1.SetCameraParams((IDNMcFrameImageCalc)ImageCalc);
                    }
                    else if (ImageCalc.GetImageType() == DNEImageType._EIT_LOROP)
                    {
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetPixelWorkingAreaValid", McEx);
                }
                try
                {
                    ctrlGCSImageCalc.GridCoordinateSystem = ImageCalc.GetGridCoordSys();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetPixelWorkingAreaValid", McEx);
                }

                LoadImageCalcList();

                try
                {
                    chxPixelWorkingAreaValid.Checked = ImageCalc.GetPixelWorkingAreaValid();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetPixelWorkingAreaValid", McEx);
                }

                try
                {
                    DNSMcVector2D areaLowerLeft, areaUpperRight;
                    ImageCalc.GetPixelWorkingArea(out areaLowerLeft, out areaUpperRight);

                    if (areaLowerLeft != null && areaUpperRight != null)
                    {
                        ctrl2DVectorPixelWorkingAreaLowerLeft.SetVector2D(areaLowerLeft);
                        ctrl2DVectorPixelWorkingAreaUpperRight.SetVector2D(areaUpperRight);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetPixelWorkingArea", McEx);
                }
            }
        }

        private void LoadImageCalcList()
        {
            try
            {
                lstExistingImageCalc.Items.Clear();
                m_lImageCalcText.Clear();
                m_lImageCalcTag.Clear();
                DNEImageType imageType = GetImageType();
                foreach (IDNMcImageCalc imageCalc in Manager_MCImageCalc.AllParams.Keys)
                {
                    if (imageType == DNEImageType._EIT_NONE || (imageType != DNEImageType._EIT_NONE && imageType == imageCalc.GetImageType()))
                    {
                        m_lImageCalcText.Add(Manager_MCNames.GetNameByObject(imageCalc, imageCalc.GetImageType().ToString()));
                        m_lImageCalcTag.Add(imageCalc);
                    }
                }

                lstExistingImageCalc.Items.AddRange(m_lImageCalcText.ToArray());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetImageCalc", McEx);
            }
        }

        private DNEImageType GetImageType()
        {
            return (DNEImageType)Enum.Parse(typeof(DNEImageType), cmbImageType.Text);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "OK")
            {
                DNEImageType type = GetImageType();
                if (type != DNEImageType._EIT_NONE)
                {
                    // add to list only the selected terrains
                    List<IDNMcMapTerrain> selectedTerrains = new List<IDNMcMapTerrain>();
                    for (int i = 0; i < m_lstTerrainWizzardForm.Count; i++)
                    {
                        if (lstTerrains.GetSelected(i) == true)
                            selectedTerrains.Add(m_lstTerrainWizzardForm[i].Terrain);
                    }

                    switch (type)
                    {
                        case DNEImageType._EIT_AFFINE:
                            try
                            {
                                if (rbDtm.Checked)
                                    ImageCalc = DNMcAffineImageCalc.Create(ctrlBrowseImageDataFileName.FileName, m_DtmLayer, ctrlGCSImageCalc.GridCoordinateSystem);
                                else
                                    ImageCalc = DNMcAffineImageCalc.Create(ctrlBrowseImageDataFileName.FileName, selectedTerrains.ToArray(), ctrlGCSImageCalc.GridCoordinateSystem);

                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("DNMcAffineImageCalc.Create", McEx);
                                return;
                            }
                            break;
                        case DNEImageType._EIT_FRAME:
                            DNSCameraParams frameCameraParams = ctrlCameraParams1.GetCameraParams();

                            try
                            {
                                if (rbDtm.Checked)
                                    ImageCalc = DNMcFrameImageCalc.Create(ref frameCameraParams, m_DtmLayer, ctrlGCSImageCalc.GridCoordinateSystem);
                                else
                                    ImageCalc = DNMcFrameImageCalc.Create(ref frameCameraParams, selectedTerrains.ToArray(), ctrlGCSImageCalc.GridCoordinateSystem);

                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("DNMcFrameImageCalc.Create", McEx);
                                return;
                            }
                            break;
                        case DNEImageType._EIT_GALAXYAIDS:
                            break;
                        case DNEImageType._EIT_LOROP:
                            try
                            {
                                ImageCalc = DNMcLoropImageCalc.Create(ctrlBrowseImageDataFileName.FileName, chxLoropIsFileName.Checked, m_DtmLayer, ctrlGCSImageCalc.GridCoordinateSystem);
                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("DNMcLoropImageCalc.Create", McEx);
                                return;
                            }
                            break;
                        case DNEImageType._EIT_NUM:
                            break;
                        case DNEImageType._EIT_ORTHO:
                            break;

                    }

                    this.DialogResult = DialogResult.OK;
                    Managers.MapWorld.Manager_MCImageCalc.AddToDictionary(ImageCalc);
                    this.Close();
                }
                else { MessageBox.Show("Invalid image calc type", "Create new image calc"); }

            }
        }

        private void chxLoropIsFileName_CheckedChanged(object sender, EventArgs e)
        {
            if (chxLoropIsFileName.Checked == true)
                ctrlBrowseImageDataFileName.IsFolderDialog = false;
            else
                ctrlBrowseImageDataFileName.IsFolderDialog = true;
        }

        private void cmbImageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DNEImageType imageType = (DNEImageType)Enum.Parse(typeof(DNEImageType), cmbImageType.Text);
            switch (imageType)
            {
                case DNEImageType._EIT_AFFINE:
                    chxLoropIsFileName.Visible = false;
                    ctrlBrowseImageDataFileName.Visible = true;
                    ctrlCameraParams1.Visible = false;
                    break;
                case DNEImageType._EIT_FRAME:
                    chxLoropIsFileName.Visible = false;
                    ctrlBrowseImageDataFileName.Visible = false;
                    ctrlCameraParams1.Visible = true;

                    break;
                case DNEImageType._EIT_LOROP:
                    chxLoropIsFileName.Visible = true;
                    ctrlBrowseImageDataFileName.Visible = true;
                    ctrlCameraParams1.Visible = false;
                    break;
            }

            LoadImageCalcList();
            ctrlCameraParams1.SetCameraParams(null);
        }
        public IDNMcImageCalc ImageCalc
        {
            get { return m_ImageCalc; }
            set { m_ImageCalc = value; }
        }

        private void btnIsInPixelWorkingAreaGet_Click(object sender, EventArgs e)
        {
            if (ImageCalc != null)
            {
                try
                {
                    chxIsInPixelWorkingArea.Checked = ImageCalc.IsInPixelWorkingArea(ctrl2DVectorIsInPixelWorkingAreaPixelCoord.GetVector2D());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("IsInPixelWorkingArea", McEx);
                }
            }
        }

        private void btnPixelWorkingAreaValidSet_Click(object sender, EventArgs e)
        {
            if (ImageCalc != null)
            {
                try
                {
                    ImageCalc.SetPixelWorkingAreaValid(chxPixelWorkingAreaValid.Checked);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetPixelWorkingAreaValid", McEx);
                }
            }
        }

        private void btnPixelWorkingAreaSet_Click(object sender, EventArgs e)
        {
            if (ImageCalc != null)
            {
                try
                {
                    ImageCalc.SetPixelWorkingArea(ctrl2DVectorPixelWorkingAreaLowerLeft.GetVector2D(), ctrl2DVectorPixelWorkingAreaUpperRight.GetVector2D());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetPixelWorkingArea", McEx);
                }
            }
        }

        private void btnAddDtmMapLayer_Click(object sender, EventArgs e)
        {
            if (rbDtm.Checked)
            {
                addDtmLayer = new AddLayerForm();
                Manager_MCLayers.AddLayerFormToList(addDtmLayer);

                if (addDtmLayer.ShowDialog() == DialogResult.OK)
                {
                    if (addDtmLayer.NewLayers != null && addDtmLayer.NewLayers.Count == 1)
                    {
                        if (addDtmLayer.NewLayers[0].LayerType == DNELayerType._ELT_NATIVE_DTM ||
                            addDtmLayer.NewLayers[0].LayerType == DNELayerType._ELT_RAW_DTM ||
                            addDtmLayer.NewLayers[0].LayerType == DNELayerType._ELT_NATIVE_SERVER_DTM)
                        {
                            lblDtmMapLayer.Text = Manager_MCNames.GetNameByObject(addDtmLayer.NewLayers[0], "DTM Layer");
                            m_DtmLayer = (IDNMcDtmMapLayer)addDtmLayer.NewLayers[0];
                        }
                        else
                            MessageBox.Show("Wrong Layer Type", "You have to choose DTM layer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                Manager_MCLayers.RemoveLayerFormFromList(addDtmLayer);
            }
            /* else
             {
                 mTerrainWizFrm = new TerrainWizzardForm(SelectionMode.MultiSimple);
                 Manager_MCLayers.AddTerrainWizzardFormToList(mTerrainWizFrm);
                 try
                 {
                     if (mTerrainWizFrm.ShowDialog() == DialogResult.OK)
                     {
                         if (mTerrainWizFrm.Terrains != null)
                         {
                             lblDtmMapLayer.Text = "Selected Terrains"; / *Manager_MCNames.GetNameByObject(mTerrainWizFrm.Terrain, "Terrain");* /
                             m_MapTerrainList = mTerrainWizFrm.Terrains;
                         }
                     }
                 }
                 catch (MapCoreException McEx)
                 {
                     MapCore.Common.Utilities.ShowErrorMessage("Add terrain", McEx);
                 }
                 Manager_MCLayers.RemoveTerrainWizzardFormFromList(mTerrainWizFrm);
             }
             }*/
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            gbCameraParams.Enabled = true;
            btnOK.Text = "OK";
        }

        private void lstExistingImageCalc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedImageCalc != null)
            {
                ImageCalc = SelectedImageCalc;
                FillFormData();
            }
        }
        private void lstExistingImageCalc_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > lstExistingImageCalc.ItemHeight * lstExistingImageCalc.Items.Count)
            {

            }
        }


        public IDNMcImageCalc SelectedImageCalc
        {
            get
            {
                if (lstExistingImageCalc.SelectedIndex != -1)
                    m_SelectedImageCalc = m_lImageCalcTag[lstExistingImageCalc.SelectedIndex];

                return m_SelectedImageCalc;
            }
            set
            {
                m_SelectedImageCalc = value;
            }
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            SelectedImageCalc = null;
            lstExistingImageCalc.ClearSelected();
            ctrlCameraParams1.SetCameraParams(null);
        }

        private void rbDtm_CheckedChanged(object sender, EventArgs e)
        {
            ResetDtmTerrain();
        }

        private void rbTerrain_CheckedChanged(object sender, EventArgs e)
        {
            ResetDtmTerrain();
        }

        private void ResetDtmTerrain()
        {
            btnAddDtmMapLayer.Enabled = lblDtmMapLayer.Enabled = rbDtm.Checked;
            pnlTerrains.Enabled = rbTerrain.Checked;
            /* m_DtmLayer = null;
             m_MapTerrainList = null;

             lblDtmMapLayer.Text = "Null";*/
        }

        private void btnTerrainListAdd_Click(object sender, EventArgs e)
        {
            mTerrainWizFrm = new TerrainWizzardForm();
            Manager_MCLayers.AddTerrainWizzardFormToList(mTerrainWizFrm);
            try
            {
                if (mTerrainWizFrm.ShowDialog() == DialogResult.OK)
                {
                    if (mTerrainWizFrm.Terrain != null && !m_SelectedTerrains.Contains(mTerrainWizFrm.Terrain))
                    {
                        m_lstTerrainWizzardForm.Add(mTerrainWizFrm);

                        m_SelectedTerrains.Add(mTerrainWizFrm.Terrain);
                        lstTerrains.Items.Add(Manager_MCNames.GetNameByObject(mTerrainWizFrm.Terrain, "Terrain"));
                        lstTerrains.SetSelected(lstTerrains.Items.Count - 1, true);
                       
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Add terrain", McEx);
            }
            Manager_MCLayers.RemoveTerrainWizzardFormFromList(mTerrainWizFrm);

        }

        private void btnTerrainListRemove_Click(object sender, EventArgs e)
        {
            if (lstTerrains.SelectedIndex >= 0)
            {
                int SelectedIdx = lstTerrains.SelectedIndex;
                lstTerrains.Items.RemoveAt(SelectedIdx);
                m_lstTerrainWizzardForm.RemoveAt(SelectedIdx);
                m_SelectedTerrains.RemoveAt(SelectedIdx);
            }
        }

        private void btnTerrainListClear_Click(object sender, EventArgs e)
        {
            lstTerrains.ClearSelected();
        }
    }
}