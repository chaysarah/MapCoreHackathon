using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.GUI;
using System.Runtime.InteropServices;
using System.IO;
using MCTester.MapWorld.Assist_Forms;
using System.Drawing.Imaging;
using MCTester.GUI.Map;
using MCTester.Managers.MapWorld;
using MCTester.General_Forms;
using MapCore.Common;
using MCTester.GUI.Trees;
using MCTester.GUI.Forms;
using System.Linq;

using MCTester.Managers;
using MCTester.Managers.ObjectWorld;


namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucViewport : UserControl,IUserControlItem, IDNEditModeCallback, ICreateSectionMap
    {
        private IDNMcMapViewport m_CurrentObject;
        private IDNMcSectionMapViewport m_SectionMapCurrentObject;
        private Form m_containerForm;
        private DNSQueryParams m_queryParams;
        private DNSQueryParams m_ScanQueryParams;
        private float m_ImageProcAll;
        private float m_ImageProcRasterLayers;
        private float m_ImageProcWithoutObjects;
        private IDNMcPolygonItem m_SectionPolygonItem;
        private IDNMcMapTerrain m_LayerEffectiveVisibilityTerrain;

        private List<string> m_lstOverlayText = new List<string>();
        private List<IDNMcOverlay> m_lstOverlayValue = new List<IDNMcOverlay>();

        private List<string> m_lstObjectsText = new List<string>();
        private List<IDNMcObject> m_lstObjectsValue = new List<IDNMcObject>();

        private IDNMcRasterMapLayer m_CurrLayer = null;
        private List<string> m_lLayerText = new List<string>();
        private List<IDNMcRasterMapLayer> m_lLayerTag = new List<IDNMcRasterMapLayer>();

        private IDNMcMapTerrain[] m_terrainsArr;
        private SaveFileDialog SFD;
        private IDNMcImageCalc mImageCalc = null;
        private IDNMcEditMode m_EditMode;
        private IDNEditModeCallback m_Callback;

        private IDNMcImageCalc m_SelectedImageCalc = null;
        private List<string> m_lImageCalcText = new List<string>();
        private List<IDNMcImageCalc> m_lImageCalcTag = new List<IDNMcImageCalc>();
        private Control m_ParentForm;
        private IDNMcObject m_SectionMapObject;
        private DNSMcVector3D[] m_sectionPoint = null;

        public ucViewport()
        {
            InitializeComponent();
            cmbObjectDelayType.Items.AddRange(Enum.GetNames(typeof(DNEObjectDelayType)));
            cmbShadowMode.Items.AddRange(Enum.GetNames(typeof(DNEShadowMode)));
            cmbTerrainNoiseMode.Items.AddRange(Enum.GetNames(typeof(DNETerrainNoiseMode)));
            cmbBufferPixelFormat.Items.AddRange(Enum.GetNames(typeof(DNEPixelFormat)));

            cmbOverlayActionOption.Items.AddRange(Enum.GetNames(typeof(DNEActionOptions)));
            cmbOverlayActionOption.Text = DNEActionOptions._EAO_FORCE_TRUE.ToString();

            cmbObjectsActionOption.Items.AddRange(Enum.GetNames(typeof(DNEActionOptions)));
            cmbObjectsActionOption.Text = DNEActionOptions._EAO_FORCE_TRUE.ToString();

            cmbStereoMode.Items.AddRange(Enum.GetNames(typeof(DNEStereoRenderMode)));
            cmbStereoMode.Text = DNEStereoRenderMode._ESRM_NONE.ToString();

            lstTerrain.SelectedValueChanged += new EventHandler(lstTerrain_SelectedValueChanged);
            cmbImageProcessingStage.Items.AddRange(Enum.GetNames(typeof(DNEImageProcessingStage)));
            m_queryParams = new DNSQueryParams();
            m_ScanQueryParams = new DNSQueryParams();
            m_ViewportTabCtrl.TabPages.Remove(tpImageCalc);

            // image processing
            cmbFilterProccessingOperation.Items.AddRange(Enum.GetNames(typeof(DNEFilterProccessingOperation)));

            cmbFunctionOnColorTableColorChannel.Items.AddRange(Enum.GetNames(typeof(DNEColorChannel)));
            cmbFunctionOnColorTableColorChannel.Text = DNEColorChannel._ECC_MULTI_CHANNEL.ToString();

            chlOverlayVisibilityOption.DisplayMember = "OverlaysTextList";
            chlOverlayVisibilityOption.ValueMember = "OverlaysValueList";

            chlOverlayVisibilityOption.DisplayMember = "ObjectsTextList";
            chlOverlayVisibilityOption.ValueMember = "ObjectsValueList";

            lstLayers.DisplayMember = "lLayerText";
            lstLayers.ValueMember = "lLayerTag";

            lstImageCalc.DisplayMember = "lImageCalcText";
            lstImageCalc.ValueMember = "lImageCalcTag";

            MCTester.Controls.ParentChildManagerControl.OnRefreshItem += new MCTester.Controls.OnRefreshItemEventArgs(ParentChildManagerControl_OnRefreshItem);

            DataGridViewComboBoxColumn colCombo = (dgvDtmVisualColors.Columns[1] as DataGridViewComboBoxColumn);
            colCombo.DataSource = Choice.GetChoices(255);
            colCombo.DisplayMember = "Name";  // the Name property in Choice class
            colCombo.ValueMember = "Value";

            SFD = new SaveFileDialog();

            ctrlSectionHeightPoints.ChangeReadOnly();
            ctrlSectionRoutePoints.HideZ();

            ctrlImagePt.SetIsMustImageCalc(true);

        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {   
            gbSectionMapParams.Enabled = false;
            m_CurrentObject = (IDNMcMapViewport)aItem;

            if (m_CurrentObject.GetImageCalc() != null)
            {
                m_ViewportTabCtrl.TabPages.Add(tpImageCalc);

            }
            boxWorldRect.SetPointData(m_CurrentObject);
            boxViewportWorldBoundingBox.SetPointData(m_CurrentObject, true);


            cmbImageProcessingStage.TextChanged += new EventHandler(cmbImageProcessingStage_TextChanged);
            ntxBrightness.TextChanged += new EventHandler(ntxBrightness_TextChanged);
            cmbObjectDelayType.Text = DNEObjectDelayType._EODT_VIEWPORT_CHANGE_OBJECT_UPDATE.ToString();
            cmbImageProcessingStage.Text = DNEImageProcessingStage._EIPS_ALL.ToString();
                        
            try
            {
                m_terrainsArr = m_CurrentObject.GetTerrains();
                List<string> lstTerrainsNames = new List<string>();
                foreach (IDNMcMapTerrain terrain in m_terrainsArr)
                    lstTerrainsNames.Add(Manager_MCNames.GetNameByObject(terrain));

                lstTerrain.Items.AddRange(lstTerrainsNames.ToArray());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTerrains", McEx);
            }

            try
            {
                txtMapType.Text = m_CurrentObject.MapType.ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("MapType", McEx);
            }

            try
            {
                ctrlGridCoordinateSystemDetails.LoadData(m_CurrentObject.CoordinateSystem);

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCoordSysDef", McEx);
            }
                         
            try
            {
                ntxViewportID.SetUInt32(m_CurrentObject.ViewportID);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ViewportID", McEx);
            }

            try
            {
                ntxOneBitAlphaMode.SetByte(m_CurrentObject.GetOneBitAlphaMode());	
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetOneBitAlphaMode", McEx);
            }

            try
            {
                chxTransparencyOrderingMode.Checked = m_CurrentObject.GetTransparencyOrderingMode();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTransparencyOrderingMode", McEx);
            }

            try
            {
                ntxSpatialPartitionNumCacheNodes.SetUInt32(m_CurrentObject.GetSpatialPartitionNumCacheNodes());	
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSpatialPartitionNumCacheNodes", McEx);
            }
            
			try
            {
                boxViewportWorldBoundingBox.SetBoxValue(m_CurrentObject.GetTerrainsBoundingBox());
            }
            catch (MapCoreException McEx)
            {
                if (McEx.ErrorCode == DNEMcErrorCode.NOT_INITIALIZED)
                {
                    boxViewportWorldBoundingBox.GroupBoxText = "World Bounding Box(not initialized)";
                }
                else
                    Utilities.ShowErrorMessage("GetTerrainsBoundingBox", McEx);
            }
            
            try
            {
                chkGridVisibility.Checked = m_CurrentObject.GridVisibility;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GridVisibility", McEx);
            }

            try
            {
                chxGridAboveVectorLayers.Checked = m_CurrentObject.GridAboveVectorLayers;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GridAboveVectorLayers", McEx);
            }

            try
            {
                bool visible;

                m_CurrentObject.GetHeightLinesVisibility(out visible);
                chxHeightLinesVisibility.Checked = visible;
            }
            catch (MapCoreException McEx)
            {
            	Utilities.ShowErrorMessage("GetHeightLinesVisibility", McEx);
            }
          
            try
            {
                ntxWindowHandle.Text = m_CurrentObject.WindowHandle.ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("WindowHandle", McEx);
            }
                        
            try
            {
                uint widthDimension, heightDimension;
                m_CurrentObject.GetViewportSize(out widthDimension, out heightDimension);
                ntxWidthDimension.SetUInt32(widthDimension);
                ntxHeightDimension.SetUInt32(heightDimension);

                ctrl2DRenderScreenRectToBufferBR.SetVector2D(new DNSMcVector2D(widthDimension,heightDimension));
                ntxRenderScreenrectToBufferWidth.SetUInt32(widthDimension);
                ntxRenderScreenrectToBufferHeight.SetUInt32(heightDimension);
            
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetViewportSize", McEx);
            }
            
            try
            {
                bool overloadEnabled;
                uint minNumItem;
                m_CurrentObject.GetOverloadMode(out overloadEnabled, out minNumItem);

                chxOverloadModeEnabled.Checked = overloadEnabled;
                ntxMinNumItemForOverload.SetUInt32(minNumItem);
            
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetOverloadMode", McEx);
            }

            try
            {
                chxFreezeObjectsVisualization.Checked = m_CurrentObject.GetFreezeObjectsVisualization();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetFreezeObjectsVisualization", McEx);
            }

            try
            {
                DNEStereoRenderMode eStereoMode;
                DNSStereoParams pParams;
                m_CurrentObject.GetStereoMode(out eStereoMode, out pParams);
                ntxStereoCameraPositionOffset.SetFloat(pParams.fCameraPositionOffset);
                ntxStereoCameraCenterOffset.SetFloat(pParams.fCameraCenterOffset);
                chxLeftEyeMaster.Checked = pParams.bLeftEyeMaster;
                cmbStereoMode.Text = eStereoMode.ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetStereoMode", McEx);
            }

            try
            {
                DNSMcBColor backgroundColor = m_CurrentObject.GetBackgroundColor();
                picbBackgroundColor.BackColor = Color.FromArgb(255, backgroundColor.r, backgroundColor.g, backgroundColor.b);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetBackgroundColor", McEx);
            }

            try
            {
                float brightnessAll, brightnessRaster, brightnessWithoutObj;
                m_CurrentObject.GetBrightness(DNEImageProcessingStage._EIPS_ALL, out brightnessAll);
                m_CurrentObject.GetBrightness(DNEImageProcessingStage._EIPS_RASTER_LAYERS, out brightnessRaster);
                m_CurrentObject.GetBrightness(DNEImageProcessingStage._EIPS_WITHOUT_OBJECTS, out brightnessWithoutObj);

                ImageProcAll = brightnessAll;
                ImageProcRasterLayers = brightnessRaster;
                ImageProcWithoutObjects = brightnessWithoutObj;

                ntxBrightness.SetFloat(ImageProcAll);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetBrightness", McEx);
            }

            try
            {
                ntxVector3DExtrusionVisibilityMaxScale.SetFloat(m_CurrentObject.GetVector3DExtrusionVisibilityMaxScale());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVector3DExtrusionVisibilityMaxScale", McEx);
            }

            try
            {
                ntx3DModelVisibilityMaxScale.SetFloat(m_CurrentObject.Get3DModelVisibilityMaxScale());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Get3DModelVisibilityMaxScale", McEx);
            }

            try
            {
                ntxObjectVisibiltyMaxScale.SetFloat(m_CurrentObject.GetObjectsVisibilityMaxScale());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjectsVisibilityMaxScale", McEx);
            }

            try
            {
                ntxThresholdInPixels.SetUInt32(m_CurrentObject.GetObjectsMovementThreshold());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjectsMovementThreshold", McEx);
            }

            if (m_CurrentObject.MapType == DNEMapType._EMT_3D)
            {
                try
                {
                    ntxScreenSizeTerrainObjectsFactor.SetFloat(m_CurrentObject.GetScreenSizeTerrainObjectsFactor());
                }
                catch (MapCoreException McEx)
                {
                	Utilities.ShowErrorMessage("GetScreenSizeTerrainObjectsFactor", McEx);
                }
            }            

            try
            {
                bool isEnable;
                DNSDtmVisualizationParams DtmVisualizationParams;
                m_CurrentObject.GetDtmVisualization(out isEnable, out DtmVisualizationParams);
                chxDtmVisualizationWithoutRasterEnable.Checked = isEnable;
                LoadDtmVisualization(DtmVisualizationParams);
                
                

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetDtmVisualizationWithoutRaster", McEx);
            }

            try
            {
                chxDtmTransparencyWithoutRaster.Checked = m_CurrentObject.GetDtmTransparencyWithoutRaster();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetDtmTransparencyWithoutRaster", McEx);
            }

            try
            {
                chxRenderToBufferMode.Checked = m_CurrentObject.GetRenderToBufferMode();	
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetRenderToBufferMode", McEx);
            }

            try
            {
                cmbShadowMode.Text = m_CurrentObject.GetShadowMode().ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetShadowMode", McEx);
            }

            try
            {
                cmbTerrainNoiseMode.Text = (m_CurrentObject.GetTerrainNoiseMode()).ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTerrainNoiseMode", McEx);
            }

            try
            {
                dgvClipPlane.Rows.Clear();
                DNSMcPlane[] planes = m_CurrentObject.GetClipPlanes();
	            
                for (int i = 0; i < planes.Length; i++)
                {
                    dgvClipPlane.Rows.Add();
                    dgvClipPlane[0, i].Value = planes[i].Normal.x;
                    dgvClipPlane[1, i].Value = planes[i].Normal.y;
                    dgvClipPlane[2, i].Value = planes[i].Normal.z;
                    dgvClipPlane[3, i].Value = planes[i].dLocation;

                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetClipPlanes", McEx);
            }

            // fill overlay and objects visibility option checkListBox 
            chlOverlayVisibilityOption.Items.Clear();
            m_lstOverlayText.Clear();
            m_lstOverlayValue.Clear();

            chlObjectsVisibilityOption.Items.Clear();
            m_lstObjectsText.Clear();
            m_lstObjectsValue.Clear();

            if (m_CurrentObject.OverlayManager != null)
            {
                IDNMcOverlay[] overlays = m_CurrentObject.OverlayManager.GetOverlays();
                List<IDNMcObject> objs = new List<IDNMcObject>();

                foreach (IDNMcOverlay overlay in overlays)
                {
                    m_lstOverlayText.Add(Manager_MCNames.GetNameByObject(overlay, "Overlay"));
                    m_lstOverlayValue.Add(overlay);

                    objs.AddRange(overlay.GetObjects());
                }

                foreach (IDNMcObject obj in objs)
                {
                    m_lstObjectsText.Add(Manager_MCNames.GetNameByObject(obj,"Object"));
                    m_lstObjectsValue.Add(obj);
                }

                chlOverlayVisibilityOption.Items.AddRange(m_lstOverlayText.ToArray());
                chlObjectsVisibilityOption.Items.AddRange(m_lstObjectsText.ToArray());

            }
            //Case viewport is Section Map Viewport
            if (aItem is IDNMcSectionMapViewport)
            {
                gbSectionMapParams.Enabled = true;
                m_SectionMapCurrentObject = (IDNMcSectionMapViewport)aItem;
            
                try
                {
                    DNSMcVector3D[] routePoints;
                    DNMcNullableOut<DNSMcVector3D[]> heightPoints = new DNMcNullableOut<DNSMcVector3D[]>();

                    m_SectionMapCurrentObject.GetSectionRoutePoints(out routePoints, heightPoints);

                    ctrlSectionRoutePoints.SetPoints(routePoints);
                    UpdateDGVSectionHeightPoints(heightPoints.Value);
                   
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetSectionRoutePoints", McEx);
                }

                try
                {
                    ntxAxesRatio.SetFloat(m_SectionMapCurrentObject.AxesRation);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("AxesRation", McEx);
                }

                try
                {
                    m_SectionPolygonItem = m_SectionMapCurrentObject.SectionPolygonItem;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SectionPolygonItem", McEx);
                }
            }

            try
            {          
                DNEPixelFormat viewportPixelFormat;
                m_CurrentObject.GetRenderToBufferNativePixelFormat(out viewportPixelFormat);

                cmbBufferPixelFormat.Text = viewportPixelFormat.ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetRenderToBufferNativePixelFormat", McEx);
            }

            try
            {
                lstLayers.Items.Clear();
                m_lLayerText.Clear();
                m_lLayerTag.Clear();

                IDNMcMapTerrain[] terrains = m_CurrentObject.GetTerrains();
                foreach (IDNMcMapTerrain terrain in terrains)
                {
                    IDNMcMapLayer[] layers = terrain.GetLayers();
                    foreach (IDNMcMapLayer layer in layers)
                    {
                        if (layer is IDNMcRasterMapLayer)
                        {
                            lLayerText.Add(Manager_MCNames.GetNameByObject(layer,layer.LayerType.ToString()));
                            lLayerTag.Add((IDNMcRasterMapLayer)layer);
                        }
                    }
                }

                lstLayers.Items.AddRange(lLayerText.ToArray());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTerrains/GetLayers", McEx);
            }

            LoadImageCalcList();
            if(ctrlViewportAsCamera != null)
                 ((IUserControlItem)ctrlViewportAsCamera).LoadItem(m_CurrentObject);
 
            try
            {
                mImageCalc = m_CurrentObject.GetImageCalc();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetImageCalc", McEx);
            }
            try
            {
                if (mImageCalc != null && mImageCalc.GetImageType() == DNEImageType._EIT_FRAME)
                {
                   
                        DNSCameraParams cameraParams = ((IDNMcFrameImageCalc)mImageCalc).GetCameraParams();
                        ctrlCameraLocation.SetVector3D( cameraParams.CameraPosition);
                        ntxCameraOpeningAngleX.SetDouble(cameraParams.dCameraOpeningAngleX);
                        ntxCameraPitch.SetDouble(cameraParams.dCameraPitch);
                        ntxCameraRoll.SetDouble(cameraParams.dCameraRoll);
                        ntxCameraYaw.SetDouble(cameraParams.dCameraYaw);
                        ntxPixelRatio.SetDouble(cameraParams.dPixelRatio);
                        ntxPixelesNumX.SetInt(cameraParams.nPixelesNumX);
                        ntxPixelesNumY.SetInt(cameraParams.nPixelesNumY);
                    
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCameraParams", McEx);
            }
            try
            {
                if (mImageCalc != null)
                {
                    txtImageType.Text = mImageCalc.GetImageType().ToString();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ImageType", McEx);
            }
        }

        private void LoadImageCalcList()
        {
            try
            {
                lstImageCalc.Items.Clear();
                m_lImageCalcText.Clear();
                m_lImageCalcTag.Clear();

                foreach (IDNMcImageCalc imageCalc in Manager_MCImageCalc.AllParams.Keys)
                {
                    m_lImageCalcText.Add(Manager_MCNames.GetNameByObject(imageCalc, imageCalc.GetImageType().ToString()));
                    m_lImageCalcTag.Add(imageCalc);
                }

                lstImageCalc.Items.AddRange(m_lImageCalcText.ToArray());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTerrains/GetImageCalc", McEx);
            }
        }

        private void btnRefreshImageCalcList_Click(object sender, EventArgs e)
        {
            lstImageCalc.Items.Clear();
            LoadImageCalcList();
            SelectedImageCalc = null;
            ctrlImagePt.SetImageCalc(null);
        }

        private void UpdateDGVSectionHeightPoints(DNSMcVector3D[] heightPoints)
        {
            if (heightPoints != null)
            {
                ctrlSectionHeightPoints.SetPoints(heightPoints);
            }
        }

        void ntxBrightness_TextChanged(object sender, EventArgs e)
        {
            DNEImageProcessingStage imageProc = (DNEImageProcessingStage)Enum.Parse(typeof(DNEImageProcessingStage), cmbImageProcessingStage.Text);
            switch (imageProc)
            {
                case DNEImageProcessingStage._EIPS_ALL:
                    ImageProcAll = ntxBrightness.GetFloat();
                    break;
                case DNEImageProcessingStage._EIPS_RASTER_LAYERS:
                    ImageProcRasterLayers = ntxBrightness.GetFloat();
                    break;
                case DNEImageProcessingStage._EIPS_WITHOUT_OBJECTS:
                    ImageProcWithoutObjects = ntxBrightness.GetFloat();
                    break;
            }
        }
        
        void cmbImageProcessingStage_TextChanged(object sender, EventArgs e)
        {
            DNEImageProcessingStage imageProc = (DNEImageProcessingStage)Enum.Parse(typeof(DNEImageProcessingStage), cmbImageProcessingStage.Text);
            switch (imageProc)
            {
                case DNEImageProcessingStage._EIPS_ALL:
                    ntxBrightness.SetFloat(ImageProcAll);
                    break;
                case DNEImageProcessingStage._EIPS_RASTER_LAYERS:
                    ntxBrightness.SetFloat(ImageProcRasterLayers);
                    break;
                case DNEImageProcessingStage._EIPS_WITHOUT_OBJECTS:
                    ntxBrightness.SetFloat(ImageProcWithoutObjects);
                    break;
            }
        }           
        
        public void SaveItem()
        {
            try
            {
                m_CurrentObject.SetOneBitAlphaMode(ntxOneBitAlphaMode.GetByte());	
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetOneBitAlphaMode", McEx);
            }

            try
            {
                m_CurrentObject.SetTransparencyOrderingMode(chxTransparencyOrderingMode.Checked);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetTransparencyOrderingMode", McEx);
            }

            try
            {
                m_CurrentObject.SetSpatialPartitionNumCacheNodes(ntxSpatialPartitionNumCacheNodes.GetUInt32());	
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSpatialPartitionNumCacheNodes", McEx);
            }

            try
            {
                DNEObjectDelayType delayType = (DNEObjectDelayType)Enum.Parse(typeof(DNEObjectDelayType),cmbObjectDelayType.Text);
                m_CurrentObject.SetObjectsDelay(delayType, chxDelayEnabled.Checked, ntxNumToUpdatePerRender.GetUInt32()); 
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetObjectsDelay", McEx);
            }

            try
            {
                m_CurrentObject.SetOverloadMode(chxOverloadModeEnabled.Checked, ntxMinNumItemForOverload.GetUInt32());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetOverloadMode", McEx);
            }

            try
            {
                m_CurrentObject.SetFreezeObjectsVisualization(chxFreezeObjectsVisualization.Checked);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetFreezeObjectsVisualization", McEx);
            }

            try
            {
            
                m_CurrentObject.SetVector3DExtrusionVisibilityMaxScale(ntxVector3DExtrusionVisibilityMaxScale.GetFloat());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetVector3DExtrusionVisibilityMaxScale", McEx);
            }

            try
            {
                m_CurrentObject.Set3DModelVisibilityMaxScale(ntx3DModelVisibilityMaxScale.GetFloat());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Set3DModelVisibilityMaxScale", McEx);
            }

            try
            {
                m_CurrentObject.SetObjectsVisibilityMaxScale(ntxObjectVisibiltyMaxScale.GetFloat());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetObjectsVisibilityMaxScale", McEx);
            }

            try
            {
                m_CurrentObject.SetObjectsMovementThreshold(ntxThresholdInPixels.GetUInt32());				
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetObjectsMovementThreshold", McEx);
            }

            if (m_CurrentObject.MapType == DNEMapType._EMT_3D)
            {
                try
                {
                    m_CurrentObject.SetScreenSizeTerrainObjectsFactor(ntxScreenSizeTerrainObjectsFactor.GetFloat());	
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetScreenSizeTerrainObjectsFactor", McEx);
                }
            }            

            try
            {
                m_CurrentObject.GridVisibility = chkGridVisibility.Checked;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GridVisibility", McEx);
            }

            try
            {
                m_CurrentObject.GridAboveVectorLayers = chxGridAboveVectorLayers.Checked;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GridAboveVectorLayers", McEx);
            }

            try
            {
                m_CurrentObject.SetHeightLinesVisibility(chxHeightLinesVisibility.Checked);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetHeightLinesVisibility", McEx);
            }

            try
            {
                DNSMcBColor backgroundColor;
                backgroundColor.a = 255;
                backgroundColor.r = picbBackgroundColor.BackColor.R;
                backgroundColor.g = picbBackgroundColor.BackColor.G;
                backgroundColor.b = picbBackgroundColor.BackColor.B;
                m_CurrentObject.SetBackgroundColor(backgroundColor);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetBackgroundColor", McEx);
            }

            try
            {
                m_CurrentObject.SetBrightness(DNEImageProcessingStage._EIPS_ALL, ImageProcAll);
                m_CurrentObject.SetBrightness(DNEImageProcessingStage._EIPS_RASTER_LAYERS, ImageProcRasterLayers);
                m_CurrentObject.SetBrightness(DNEImageProcessingStage._EIPS_WITHOUT_OBJECTS, ImageProcWithoutObjects);
            }
            catch (MapCoreException McEx)
            {
            	Utilities.ShowErrorMessage("SetBrightness", McEx);
            }

            try
            {

                DNETerrainNoiseMode Noisetype = (DNETerrainNoiseMode)Enum.Parse(typeof(DNETerrainNoiseMode), cmbTerrainNoiseMode.Text);
                m_CurrentObject.SetTerrainNoiseMode(Noisetype);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetTerrainNoiseMode", McEx);
            }

            try
            {
                DNEShadowMode ShadowMode = (DNEShadowMode)Enum.Parse(typeof(DNEShadowMode), cmbShadowMode.Text);
                m_CurrentObject.SetShadowMode(ShadowMode);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetShadowMode", McEx);
            }

           

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.OverlayManager);
        }

        #endregion

        #region EventArgs
        private void btnOMToViewportWorld_Click(object sender, EventArgs e)
        {
            try
            {
                ctrl3DViewportPoint.SetVector3D( m_CurrentObject.OverlayManagerToViewportWorld(ctrl3DOMPoint.GetVector3D()));
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("OverlayManagerToViewportWorld", McEx);
            }
        }

        private void btnViewportToOMWorld_Click(object sender, EventArgs e)
        {
            try
            {
                ctrl3DOMPoint.SetVector3D( m_CurrentObject.ViewportToOverlayManagerWorld(ctrl3DViewportPoint.GetVector3D()));
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ViewportToOverlayManagerWorld", McEx);
            }
        }

        private void btnViewportToImageCalcWorld_Click(object sender, EventArgs e)
        {
            try
            {
                ctrl3DImagePoint.SetVector3D( m_CurrentObject.ViewportToImageCalcWorld(ctrl3DWorldPoint.GetVector3D(), SelectedImageCalc));
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ViewportToImageCalcWorld", McEx);
            }
        }

        private void btnImageCalcWorldToViewport_Click(object sender, EventArgs e)
        {
            try
            {
                ctrl3DWorldPoint.SetVector3D( m_CurrentObject.ImageCalcWorldToViewport(ctrl3DImagePoint.GetVector3D(),SelectedImageCalc));
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ImageCalcWorldToViewport", McEx);
            }
        }

        public IDNMcImageCalc SelectedImageCalc
        {
            get
            {
                if (lstImageCalc.SelectedIndex != -1)
                    m_SelectedImageCalc = m_lImageCalcTag[lstImageCalc.SelectedIndex];

                return m_SelectedImageCalc;
            }
            set
            {
                m_SelectedImageCalc = value;
            }
        }

        private void btnViewportOK_Click(object sender, EventArgs e)
        {
            SaveItem();
        }

        #endregion

        void ParentChildManagerControl_OnRefreshItem(object ItemToRefresh)
        {
            if(ctrlViewportAsCamera != null)
                ((IUserControlItem)ctrlViewportAsCamera).LoadItem(m_CurrentObject);            
        }

     
        
        private void btnViewportSQ_Click(object sender, EventArgs e)
        {
            MCMapWorldTreeViewForm mcTreeView = null;
            if (Parent != null && Parent.Parent != null && Parent.Parent.Parent != null && Parent.Parent.Parent is MCMapWorldTreeViewForm)
                 mcTreeView = Parent.Parent.Parent as MCMapWorldTreeViewForm;

            m_containerForm = GetParentForm(this);
            //hide the container form
            m_containerForm.WindowState = FormWindowState.Minimized;

            SpatialQueriesForm SpatialQueriesForm = new SpatialQueriesForm(m_CurrentObject, mcTreeView);
            Manager_MCLayers.ListSpatialQueriesForms.Add(SpatialQueriesForm);
            SpatialQueriesForm.Show();
        }

        

        private Form GetParentForm(Control ctr)
        {
            if (ctr.Parent is Form)
                return ctr.Parent as Form;
            else
                return GetParentForm(ctr.Parent);
        }

        public DNSQueryParams QueryParams
        {
            get { return m_queryParams; }
            set { m_queryParams = value; }
        }

        public List<string> OverlaysTextList
        {
            get { return m_lstOverlayText; }
            set { m_lstOverlayText = value; }
        }

        public List<IDNMcOverlay> OverlaysValueList
        {
            get { return m_lstOverlayValue; }
            set { m_lstOverlayValue = value; }
        }

        public List<string> ObjectsTextList
        {
            get { return m_lstObjectsText; }
            set { m_lstObjectsText = value; }
        }

        public List<IDNMcObject> ObjectsValueList
        {
            get { return m_lstObjectsValue; }
            set { m_lstObjectsValue = value; }
        }

        public List<string> lLayerText
        {
            get { return m_lLayerText; }
            set { m_lLayerText = value; }
        }

        public List<IDNMcRasterMapLayer> lLayerTag
        {
            get { return m_lLayerTag; }
            set { m_lLayerTag = value; }
        }

        public IDNMcRasterMapLayer CurrLayer
        {
            get
            {
                if (lstLayers.SelectedIndex != -1)
                    m_CurrLayer = lLayerTag[lstLayers.SelectedIndex];

                return m_CurrLayer;
            }
            set
            {
                m_CurrLayer = value;
            }
        }

        private void btnTerrainDrawPriorityOK_Click(object sender, EventArgs e)
        {
            if (lstTerrain.SelectedIndex >= 0)
            {
                IDNMcMapTerrain SelectedTerrain = (m_terrainsArr[lstTerrain.SelectedIndex]);

                try
                {
                    if (SelectedTerrain != null)
                    {
                        m_CurrentObject.SetTerrainDrawPriority(SelectedTerrain,
                                                                ntxTerrainDrawPriority.GetSByte());

                        // turn on all viewports render needed flags
                        MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
                    }

                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetTerrainDrawPriority", McEx);
                }
            }
        }
        private void btnMaxNumTileRequestsPerRenderOK_Click(object sender, EventArgs e)
        {
            if (lstTerrain.SelectedIndex >= 0)
            {
                IDNMcMapTerrain SelectedTerrain = (m_terrainsArr[lstTerrain.SelectedIndex]);

                try
                {
                    if (SelectedTerrain != null)
                    {
                        m_CurrentObject.SetTerrainMaxNumTileRequestsPerRender(SelectedTerrain,
                                                                ntxMaxNumTileRequestsPerRender.GetUInt32());

                        // turn on all viewports render needed flags
                        MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
                    }

                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetTerrainMaxNumTileRequestsPerRender", McEx);
                }
            }
        }

        private void btnMaxNumPendingTileRequestsOK_Click(object sender, EventArgs e)
        {
            if (lstTerrain.SelectedIndex >= 0)
            {
                IDNMcMapTerrain SelectedTerrain = (m_terrainsArr[lstTerrain.SelectedIndex]);

                try
                {
                    if (SelectedTerrain != null)
                    {
                        m_CurrentObject.SetTerrainMaxNumPendingTileRequests(SelectedTerrain,
                                                                ntxMaxNumPendingTileRequests.GetUInt32());

                        // turn on all viewports render needed flags
                        MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
                    }

                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetTerrainMaxNumPendingTileRequests", McEx);
                }
            }
        }

        private void btnTerrainNumCacheTilesOK_Click(object sender, EventArgs e)
        {
            if (lstTerrain.SelectedIndex >= 0)
            {
                IDNMcMapTerrain SelectedTerrain = (m_terrainsArr[lstTerrain.SelectedIndex]);

                try
                {
                    if (SelectedTerrain != null)
                    {
                        m_CurrentObject.SetTerrainNumCacheTiles(SelectedTerrain, false, ntxNumCacheTiles.GetUInt32());
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetTerrainNumCacheTiles", McEx);
                }
            }
        }

        private void btnTerrainNumCacheTilesStaticObjectsOK_Click(object sender, EventArgs e)
        {
            if (lstTerrain.SelectedIndex >= 0)
            {
                IDNMcMapTerrain SelectedTerrain = (m_terrainsArr[lstTerrain.SelectedIndex]);

                try
                {
                    if (SelectedTerrain != null)
                    {
                        m_CurrentObject.SetTerrainNumCacheTiles(SelectedTerrain, true, ntxNumCacheTilesStaticObjects.GetUInt32());
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetTerrainNumCacheTiles", McEx);
                }
            }
        }

        void lstTerrain_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lstTerrain.SelectedIndex >= 0)
            {
                IDNMcMapTerrain SelectedTerrain = m_terrainsArr[lstTerrain.SelectedIndex];

                try
                {
                    if (SelectedTerrain != null)
                        ntxTerrainDrawPriority.SetSByte(m_CurrentObject.GetTerrainDrawPriority(SelectedTerrain));
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetTerrainDrawPriority", McEx);
                }

                try
                {
                    if (SelectedTerrain != null)
                    {
                        ntxNumCacheTiles.SetUInt32(m_CurrentObject.GetTerrainNumCacheTiles(SelectedTerrain, false));
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetTerrainNumCacheTiles", McEx);
                }

                try
                {
                    if (SelectedTerrain != null)
                    {
                        ntxNumCacheTilesStaticObjects.SetUInt32(m_CurrentObject.GetTerrainNumCacheTiles(SelectedTerrain, true));
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetTerrainNumCacheTiles", McEx);
                }

                try
                {
                    if (SelectedTerrain != null)
                    {
                        ntxMaxNumPendingTileRequests.SetUInt32(m_CurrentObject.GetTerrainMaxNumPendingTileRequests(SelectedTerrain));
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetTerrainMaxNumPendingTileRequests", McEx);
                }

                try
                {
                    if (SelectedTerrain != null)
                    {
                        ntxMaxNumTileRequestsPerRender.SetUInt32(m_CurrentObject.GetTerrainMaxNumTileRequestsPerRender(SelectedTerrain));
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetTerrainMaxNumTileRequestsPerRender", McEx);
                }

            }
        }
        
        private void cmbObjectDelayType_DropDown(object sender, EventArgs e)
        {
            try
            {
                DNEObjectDelayType delayType = (DNEObjectDelayType)Enum.Parse(typeof(DNEObjectDelayType), cmbObjectDelayType.Text);
                m_CurrentObject.SetObjectsDelay(delayType, chxDelayEnabled.Checked, ntxNumToUpdatePerRender.GetUInt32());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetObjectsDelay", McEx);
            }
        }

        private void cmbObjectDelayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool renderingEnabled;
                uint numToUpdate;
                DNEObjectDelayType delayType = (DNEObjectDelayType)Enum.Parse(typeof(DNEObjectDelayType), cmbObjectDelayType.Text);
                m_CurrentObject.GetObjectsDelay(delayType, out renderingEnabled, out numToUpdate);

                chxDelayEnabled.Checked = renderingEnabled;
                ntxNumToUpdatePerRender.SetUInt32(numToUpdate);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjectsDelay", McEx);
            }
        }

        //private void cmbTerrainNoiseMode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DNETerrainNoiseMode Noisetype = (DNETerrainNoiseMode)Enum.Parse(typeof(DNETerrainNoiseMode), cmbObjectDelayType.Text);
        //    }
        //    catch (MapCoreException McEx)
        //    {
        //        Utilities.ShowErrorMessage("SetTerrainNoiseMode", McEx);
        //    }
        //}

        //private void btnGetTerrainNoiseMode_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        cmbTerrainNoiseMode.Text = (m_CurrentObject.GetTerrainNoiseMode()).ToString();
        //    }
        //    catch (MapCoreException McEx)
        //    {
        //        Utilities.ShowErrorMessage("GetTerrainNoiseMode", McEx);
        //    }
        //}

        //private void cmbShadowMode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DNEShadowMode ShadowMode = (DNEShadowMode)Enum.Parse(typeof(DNEShadowMode), cmbShadowMode.Text);
        //    }
        //    catch (MapCoreException McEx)
        //    {
        //        Utilities.ShowErrorMessage("SetShadowMode", McEx);
        //    }
        //}

        private void btnBackgroundColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                picbBackgroundColor.BackColor = Color.FromArgb(255, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
            }
        }

        public float ImageProcAll
        {
            get { return m_ImageProcAll; }
            set { m_ImageProcAll = value; }
        }

        public float ImageProcRasterLayers
        {
            get { return m_ImageProcRasterLayers; }
            set { m_ImageProcRasterLayers = value; }
        }

        public float ImageProcWithoutObjects
        {
            get { return m_ImageProcWithoutObjects; }
            set { m_ImageProcWithoutObjects = value; }
        }        

        private void btnRegisterLocalMap_Click(object sender, EventArgs e)
        {
            frmGlobalMapViewportList GlobalMapViewportList = new frmGlobalMapViewportList(m_CurrentObject, "Register");

            if (GlobalMapViewportList.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    m_CurrentObject.RegisterLocalMap(GlobalMapViewportList.SelectedViewport);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("RegisterLocalMap", McEx);
                }
            }
        }

        private void btnUnRegisterLocalMap_Click(object sender, EventArgs e)
        {
            frmGlobalMapViewportList GlobalMapViewportList = new frmGlobalMapViewportList(m_CurrentObject, "UnRegister");

            if (GlobalMapViewportList.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    m_CurrentObject.UnRegisterLocalMap(GlobalMapViewportList.SelectedViewport);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("UnRegisterLocalMap", McEx);
                }
            }
        }
        
        private void btnSetActiveLoacalMap_Click(object sender, EventArgs e)
        {
            frmGlobalMapViewportList GlobalMapViewportList = new frmGlobalMapViewportList(m_CurrentObject, "SetActiveLoacalMap");

            if (GlobalMapViewportList.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    m_CurrentObject.SetActiveLocalMap(GlobalMapViewportList.SelectedViewport);

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetActiveMap", McEx);
                }
            }
        }

        private void chxGlobalMapAutoCenterMode_CheckedChanged(object sender, EventArgs e)
        {
            if (chxGlobalMapAutoCenterMode.Checked == true)
                m_CurrentObject.GlobalMapAutoCenterMode = true;
            else
                m_CurrentObject.GlobalMapAutoCenterMode = false;

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
        }

        private void btnFootprintItem_Click(object sender, EventArgs e)
        {
            frmLocalMapFootprintItem LocalMapFootprintItem = new frmLocalMapFootprintItem(m_CurrentObject);
            if (LocalMapFootprintItem.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    m_CurrentObject.SetLocalMapFootprintItem(LocalMapFootprintItem.InactiveLine,
                                                                LocalMapFootprintItem.ActiveLine,
                                                                LocalMapFootprintItem.GetSelectedLocalMapViewport());

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetLocalMapFootprintItem", McEx);
                }
            }            
        }

        private void btnSectionMapPolygonItem_Click(object sender, EventArgs e)
        {
            frmSectionMapPolygonItem SectionMapPolygonItemForm = new frmSectionMapPolygonItem(m_SectionMapCurrentObject);
            if (SectionMapPolygonItemForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    m_SectionMapCurrentObject.SectionPolygonItem = SectionMapPolygonItemForm.SelectedPolygon;

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SectionPolygonItem", McEx);
                }
            }
        }

        private void btnSectionToWorld_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_SectionMapCurrentObject != null)
                {
                    DNSMcVector3D worldPt;

                    m_SectionMapCurrentObject.SectionToWorld(ctrl3DVectorSection.GetVector3D(),
                                                                out worldPt);

                    ctrl3DVectorWorld.SetVector3D( worldPt);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SectionToWorld", McEx);
            }
        }

        private void btnWorldToSection_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_SectionMapCurrentObject != null)
                {
                    double XCoord;
                   
                    m_SectionMapCurrentObject.WorldToSection(ctrl3DVectorWorld.GetVector3D(),
                                                                out XCoord);

                    ctrl3DVectorSection.X = XCoord;
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("WorldToSection", McEx);
            }
        }

        private void btnSectionHeightAtPoint_Click(object sender, EventArgs e)
        {
            if (m_SectionMapCurrentObject != null)
            {
                try
                {
                    double height;
                    double slope;

                    m_SectionMapCurrentObject.GetSectionHeightAtPoint(ntxXCoordinate.GetDouble(),
                                                                        out height,
                                                                        out slope);

                    ntxHeight.SetDouble(height);
                    ntxSlope.SetDouble(slope);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetSectionHeightAtPoint", McEx);
                }
            }       
        }

        private void btnHeightLimitsOK_Click(object sender, EventArgs e)
        {
            try
            {
                double minY;
                double maxY;

                m_SectionMapCurrentObject.GetHeightLimits(ntxXLeftLimit.GetDouble(),
                                                            ntxXRightLimit.GetDouble(),
                                                            out minY,
                                                            out maxY);

                ntxYLowerLimit.SetDouble(minY);
                ntxYUpperLimit.SetDouble(maxY);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetHeightLimits", McEx);
            }
        }

        private void btnAxesRatioOK_Click(object sender, EventArgs e)
        {
            try
            {
                m_SectionMapCurrentObject.AxesRation = ntxAxesRatio.GetFloat();

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_SectionMapCurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("AxesRation", McEx);
            }
        }

        private void btnEffectiveVisibilityLayer_Click(object sender, EventArgs e)
        {
            frmTerrainList TerrainListForm = new frmTerrainList(m_CurrentObject);
            if (TerrainListForm.ShowDialog() == DialogResult.OK)
            {
                m_LayerEffectiveVisibilityTerrain = TerrainListForm.SelectedTerrain;
            }
        }

        private void btnGetVisibleLayers_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTerrain.SelectedItem != null)
                {
                    lstVisibleLayers.Items.Clear();
                    IDNMcMapLayer[] layers = m_CurrentObject.GetVisibleLayers(m_terrainsArr[lstTerrain.SelectedIndex]);
                    foreach(IDNMcMapLayer layer in layers)
                        lstVisibleLayers.Items.Add(Manager_MCNames.GetNameByObject(layer));
                }
                else
                    MessageBox.Show("Choose terrain from the list first!", "Terrain didn't chosen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVisibleLayers", McEx);
            }
        }

        private void btnGetVisibleOverlays_Click(object sender, EventArgs e)
        {
            try
            {
                lstVisibleOverlays.Items.Clear();
                IDNMcOverlay[] overlays = m_CurrentObject.GetVisibleOverlays();
                foreach(IDNMcOverlay overlay in overlays)
                    lstVisibleOverlays.Items.Add(Manager_MCNames.GetNameByObject(overlay));
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVisibleOverlays", McEx);
            }
        }
        private DNSDtmVisualizationParams SetDtmVisualizationParamsForm()
        {
            DNSDtmVisualizationParams DtmVisualizationParams = new DNSDtmVisualizationParams();

            DtmVisualizationParams.fHeightColorsHeightOrigin = ntbHeightColorsHeightOrigin.GetFloat();
            DtmVisualizationParams.fHeightColorsHeightStep = ntbHeightColorsHeightStep.GetFloat();
            DtmVisualizationParams.fShadingHeightFactor = ntbShadingHeightFactor.GetFloat();
            DtmVisualizationParams.fShadingLightSourceYaw = ntbShadingLightSourceYaw.GetFloat();
            DtmVisualizationParams.fShadingLightSourcePitch = ntbShadingLightSourcePitch.GetFloat();
            DtmVisualizationParams.uHeightColorsTransparency = ntbHeightColorsTransparency.GetByte();
            DtmVisualizationParams.uShadingTransparency = ntbShadingTransparency.GetByte();
            DtmVisualizationParams.bHeightColorsInterpolation = cbHeightColorsInterpolation.Checked;
            DtmVisualizationParams.bDtmVisualizationAboveRaster = cbDtmVisualizationAboveRaster.Checked;

            //loop on table color
            List<DNSHeigtColor> listColors = new List<DNSHeigtColor>();
            try
            {
                for (int i = 0; i < dgvDtmVisualColors.RowCount; i++)
                {
                    if (dgvDtmVisualColors.Rows[i].IsNewRow == false)
                    {
                        Color color = dgvDtmVisualColors[0, i].Style.BackColor;
                        int alpha = (int)dgvDtmVisualColors[1, i].Value;
                        int height = Convert.ToInt32(dgvDtmVisualColors[2, i].Value);

                        DNSMcBColor mcBColor = new DNSMcBColor((byte)color.R, (byte)color.G, (byte)color.B, (byte)alpha);
                        DNSHeigtColor row = new DNSHeigtColor(mcBColor, height);
                        listColors.Add(row);
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

            DtmVisualizationParams.aHeightColors = listColors.ToArray();

            return DtmVisualizationParams;
        }

        private void btnDtmVisualizationWithoutRasterOK_Click(object sender, EventArgs e)
        {
            SetDtmVisualization(chxDtmVisualizationWithoutRasterEnable.Checked, SetDtmVisualizationParamsForm());
        }

        private void SetDtmVisualization(bool isEnable, DNSDtmVisualizationParams dtmVisualizationParams)
        {
            try
            {

                m_CurrentObject.SetDtmVisualization(isEnable, dtmVisualizationParams);

                // turn on all viewports render needed flags
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetDtmVisualizationWithoutRaster", McEx);
            }
        }

        private void LoadDefaultDtmVisualizationValues()
        {
            chxDtmVisualizationWithoutRasterEnable.Checked = false;
            dgvDtmVisualColors.Rows.Clear();

            ntbHeightColorsHeightOrigin.Text = "";
            ntbHeightColorsHeightStep.Text = "";
            ntbShadingHeightFactor.SetFloat(1);
            ntbShadingLightSourceYaw.SetFloat(135);
            ntbShadingLightSourcePitch.SetFloat(-45);
            ntbHeightColorsTransparency.Text = "";
            ntbShadingTransparency.Text = "";
            cbHeightColorsInterpolation.Checked = true;
            cbDtmVisualizationAboveRaster.Checked = false;
        }

        private void LoadDtmVisualization(DNSDtmVisualizationParams DtmVisualizationParams)
        {
            float minHeight = -500, maxHeight = 9500;

            dgvDtmVisualColors.Rows.Clear();
            if (DtmVisualizationParams.aHeightColors != null && DtmVisualizationParams.aHeightColors.Length != 0)
            {
                minHeight = DtmVisualizationParams.fHeightColorsHeightOrigin +
                    DtmVisualizationParams.aHeightColors[0].nHeightInSteps *
                    DtmVisualizationParams.fHeightColorsHeightStep;
                maxHeight = DtmVisualizationParams.fHeightColorsHeightOrigin +
                    DtmVisualizationParams.aHeightColors[DtmVisualizationParams.aHeightColors.Length - 1].nHeightInSteps *
                    DtmVisualizationParams.fHeightColorsHeightStep;
                DNSHeigtColor[] arrColors = DtmVisualizationParams.aHeightColors;

                for (int i = 0; i < arrColors.Length; i++)
                {
                    DNSMcBColor mcColor = arrColors[i].Color;
                    Color color = Color.FromArgb(mcColor.r, mcColor.g, mcColor.b);

                    dgvDtmVisualColors.Rows.Add(null, (int)mcColor.a, arrColors[i].nHeightInSteps);
                    (dgvDtmVisualColors[0, i] as DataGridViewButtonCell).FlatStyle = FlatStyle.Popup;
                    dgvDtmVisualColors[0, i].Style.BackColor = color;
                }
                dgvDtmVisualColors.ClearSelection();

            }

           
            ntxDtmVisualizationWithoutRasterMinHeight.SetFloat(minHeight);
            ntxDtmVisualizationWithoutRasterMaxHeight.SetFloat(maxHeight);

            ntbHeightColorsHeightOrigin.SetFloat(DtmVisualizationParams.fHeightColorsHeightOrigin);
            ntbHeightColorsHeightStep.SetFloat(DtmVisualizationParams.fHeightColorsHeightStep);
            ntbShadingHeightFactor.SetFloat(DtmVisualizationParams.fShadingHeightFactor);
            ntbShadingLightSourceYaw.SetFloat(DtmVisualizationParams.fShadingLightSourceYaw);
            ntbShadingLightSourcePitch.SetFloat(DtmVisualizationParams.fShadingLightSourcePitch);
            ntbHeightColorsTransparency.SetByte(DtmVisualizationParams.uHeightColorsTransparency);
            ntbShadingTransparency.SetByte(DtmVisualizationParams.uShadingTransparency);
            cbHeightColorsInterpolation.Checked = DtmVisualizationParams.bHeightColorsInterpolation;
            cbDtmVisualizationAboveRaster.Checked = DtmVisualizationParams.bDtmVisualizationAboveRaster;

        }
        private void btnDtmTransparencyWithoutRaster_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetDtmTransparencyWithoutRaster(chxDtmTransparencyWithoutRaster.Checked);

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetDtmTransparencyWithoutRaster", McEx);
            }
        }

        private void btnImageCalcApply_Click(object sender, EventArgs e)
        {
            if (mImageCalc.GetImageType() == DNEImageType._EIT_FRAME)
            {
                DNSCameraParams cameraParams = new DNSCameraParams();
                cameraParams.CameraPosition = ctrlCameraLocation.GetVector3D();
                cameraParams.dCameraOpeningAngleX = ntxCameraOpeningAngleX.GetDouble();
                cameraParams.dCameraPitch = ntxCameraPitch.GetDouble();
                cameraParams.dCameraRoll = ntxCameraRoll.GetDouble();
                cameraParams.dCameraYaw = ntxCameraYaw.GetDouble();
                cameraParams.dPixelRatio = ntxPixelRatio.GetDouble();
                cameraParams.nPixelesNumX = ntxPixelesNumX.GetInt32();
                cameraParams.nPixelesNumY = ntxPixelesNumY.GetInt32();

                try
                {

                    ((IDNMcFrameImageCalc)mImageCalc).SetCameraParams(cameraParams);

                    // turn on all viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);

                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetCameraParams", McEx);
                }
            }
        }

        private void btnImageCalcDefaultHeightSet_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.GetImageCalc().SetDefaultHeight(ntxImageCalcDefaultHeight.GetDouble());

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DefaultHeight", McEx);
            }
        }

        private void btnImageCalcDefaultHeightGet_Click(object sender, EventArgs e)
        {
            try
            {
                ntxImageCalcDefaultHeight.SetDouble(m_CurrentObject.GetImageCalc().GetDefaultHeight());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DefaultHeight", McEx);
            }
        }

        private void btnRenderScreenRectToBufferApply_Click(object sender, EventArgs e)
        {
            uint pixelFormatByteCount = 0;
            DNEPixelFormat pixelFormat = DNEPixelFormat._EPF_A1R5G5B5;
            
            try
            {        
                pixelFormat = (DNEPixelFormat)Enum.Parse(typeof(DNEPixelFormat), cmbBufferPixelFormat.Text);
                pixelFormatByteCount = DNMcTexture.GetPixelFormatByteCount(pixelFormat);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetPixelFormatByteCount", McEx);
            }

            Point TL = new Point((int)ctrl2DRenderScreenRectToBufferTL.GetVector2D().x, (int)ctrl2DRenderScreenRectToBufferTL.GetVector2D().y);
            Point BR = new Point((int)ctrl2DRenderScreenRectToBufferBR.GetVector2D().x, (int)ctrl2DRenderScreenRectToBufferBR.GetVector2D().y);
            Manager_MCViewports.RenderScreenRectToBuffer(m_CurrentObject,
                            pixelFormat,
                            ntxBufferRawPitch.GetUInt32(),
                            ntxRenderScreenrectToBufferWidth.GetUInt32(),
                            ntxRenderScreenrectToBufferHeight.GetUInt32(),
                            TL,
                            BR);

        }

        private void btnRenderToBufferModeApply_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetRenderToBufferMode(chxRenderToBufferMode.Checked);

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetRenderToBufferMode", McEx);
            }
        }

        // image processing
        private void btnFilterProccessingOperationSet_Click(object sender, EventArgs e)
        {
            try
            {            
                DNEFilterProccessingOperation filter = (DNEFilterProccessingOperation)Enum.Parse(typeof(DNEFilterProccessingOperation), cmbFilterProccessingOperation.Text);
                m_CurrentObject.SetFilterImageProcessing(CurrLayer, filter);

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetFilterImageProcessing", McEx);
            }
        }

        private void FilterProccessingOperationGet_Click(object sender, EventArgs e)
        {
            try
            {        
                DNEFilterProccessingOperation filter = m_CurrentObject.GetFilterImageProcessing(CurrLayer);
                cmbFilterProccessingOperation.Text = filter.ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetFilterImageProcessing", McEx);
            }
        }

        private void btnEnableImageProccessingSet_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetEnableColorTableImageProcessing(CurrLayer, chxColorTableProccessing.Checked);

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetEnableColorTableImageProcessing", McEx);
            }
        }

        private void btnbtnEnableImageProccessingGet_Click(object sender, EventArgs e)
        {
            try
            {   
                bool colorTableProcessing = m_CurrentObject.GetEnableColorTableImageProcessing(CurrLayer);
                chxColorTableProccessing.Checked = colorTableProcessing;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetEnableColorTableImageProcessing", McEx);
            }
        }

        private void btnCustomFilterSet_Click(object sender, EventArgs e)
        {
            try
            {
                float[] arrFilter = new float[ntxFilterXSize.GetUInt32() * ntxFilterYSize.GetUInt32()];
                int i = 0;

                for (int row = 0; row < dgvFilter.Rows.Count; row++ )
                {
                    for (int col = 0; col < dgvFilter.Columns.Count; col++)
                    {
                        arrFilter[i] = float.Parse(dgvFilter[col, row].Value.ToString());
                        i++;
                    }
                }

                m_CurrentObject.SetCustomFilter(CurrLayer, ntxFilterXSize.GetUInt32(), ntxFilterYSize.GetUInt32(), arrFilter, ntxBias.GetFloat(), ntxDivider.GetFloat());

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
            	Utilities.ShowErrorMessage("SetCustomFilter", McEx);
            }
        }

        private void btnCustomFilterGet_Click(object sender, EventArgs e)
        {
            try
            {
                uint puFilterXsize;
                uint puFilterYsize;
                float[] ppaFilter;
                float pfBias;
                float pfDivider;

                m_CurrentObject.GetCustomFilter(CurrLayer, out puFilterXsize, out puFilterYsize, out ppaFilter, out pfBias, out pfDivider);

                ntxFilterXSize.SetUInt32(puFilterXsize);
                ntxFilterYSize.SetUInt32(puFilterYsize);
                ntxBias.SetFloat(pfBias);
                ntxDivider.SetFloat(pfDivider);

                int i = 0;

                for (int row = 0; row < dgvFilter.Rows.Count; row++ )
                {
                    for (int col = 0; col < dgvFilter.Columns.Count; col++)
                    {
                        dgvFilter[col, row].Value = ppaFilter[i].ToString();
                        i++;
                    }
                }
            }
            catch (MapCoreException McEx)
            {
            	Utilities.ShowErrorMessage("GetCustomFilter", McEx);
            }
        }

        private void ntxFilterXSize_TextChanged(object sender, EventArgs e)
        {
            dgvFilter.ColumnCount = ntxFilterXSize.GetInt32();
        }

        private void ntxFilterYSize_TextChanged(object sender, EventArgs e)
        {
            dgvFilter.RowCount = ntxFilterYSize.GetInt32();
        }

        public DNEColorChannel Channel
        {
            get
            {
                if (cmbFunctionOnColorTableColorChannel.Text != "")
                    return (DNEColorChannel)Enum.Parse(typeof(DNEColorChannel), cmbFunctionOnColorTableColorChannel.Text);
                else
                {
                    MessageBox.Show("Missing Color Channel");
                    return DNEColorChannel._ECC_MULTI_CHANNEL;
                }
            }
        }

        private byte [] ConvertColorValuesToByteArr(string values)
        {
            byte[] Ret = new byte[256];

            char [] separator = new char [1];
            separator[0] = ',';

            string[] splitedValues = values.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            byte byteVal;
            bool isParse = false;
            for (int i = 0; i < splitedValues.Length; i++)
            {
                isParse = byte.TryParse(splitedValues[i], out byteVal);
                Ret[i] = (isParse == true) ? byteVal : (byte)0;
            }

            return Ret;
        }

        private Int64[] ConvertHistogramToIntArr(string values)
        {
            Int64[] Ret = new Int64[256];

            char[] separator = new char[1];
            separator[0] = ',';

            string[] splitedValues = values.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Int64 longVal;
            bool isParse = false;
            for (int i = 0; i < splitedValues.Length; i++)
            {
                isParse = Int64.TryParse(splitedValues[i], out longVal);
                Ret[i] = (isParse == true) ? longVal : (byte)0;
            }

            return Ret;
        }

        private string ConvertByteArrToColorValuesString(byte [] values)
        {
            string Ret = "";
            foreach (byte val in values)
                Ret += val + ",";

            // remove last comma
            Ret.Remove(Ret.Length - 1, 1);

            return Ret;
        }

        private string ConvertIntArrToHistogramString(Int64[] values)
        {
            string Ret = "";

            if (values != null)
            {
                foreach (Int64 val in values)
                    Ret += val + ",";
            }

            // remove last comma
            Ret.Remove(Ret.Length - 1, 1);

            return Ret;
        }

        private void btnUserColorValuesSet_Click(object sender, EventArgs e)
        {
            try
            {            
                byte [] colorValues = new byte [256];
                colorValues = ConvertColorValuesToByteArr(txtUserColorValues.Text);
                m_CurrentObject.SetUserColorValues(CurrLayer, Channel, colorValues, chxUserColorValuesUse.Checked);

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetUserColorValues", McEx);
            }
        }

        private void btnUserColorValuesGet_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] colorValues;
                bool bUse = m_CurrentObject.GetUserColorValues(CurrLayer, Channel, out colorValues);

                txtUserColorValues.Text = ConvertByteArrToColorValuesString(colorValues);
                chxUserColorValuesUse.Checked = bUse;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetUserColorValues", McEx);
            }
        }

        private void btnColorValuesToDefaultSet_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetColorValuesToDefault(CurrLayer, Channel);

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetColorValuesToDefault", McEx);
            }
        }

        private void btnCurrentColorValuesGet_Click(object sender, EventArgs e)
        {
            try
            {    
                byte [] colorValues = m_CurrentObject.GetCurrentColorValues(CurrLayer, Channel);

                txtCurrentColorValuesColorValues.Text = ConvertByteArrToColorValuesString(colorValues);
            }   
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCurrentColorValues", McEx);
            }
        }

        private void btnColorTableBrightnessSet_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetColorTableBrightness(CurrLayer, Channel, ntxColorTableBrightness.GetDouble());

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetColorTableBrightness", McEx);
            }
        }


        private void btnColorTableBrightnessGet_Click(object sender, EventArgs e)
        {
            try
            {            
                double brightness = m_CurrentObject.GetColorTableBrightness(CurrLayer, Channel);

                ntxColorTableBrightness.SetDouble(brightness);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetColorTableBrightness", McEx);
            }
        }

        private void btnColorTableContrastSet_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetContrast(CurrLayer, Channel, ntxContrast.GetDouble());

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetContrast", McEx);
            }
        }


        private void btnColorTableContrastGet_Click(object sender, EventArgs e)
        {
            try
            {
                double contrast = m_CurrentObject.GetContrast(CurrLayer, Channel);

                ntxContrast.SetDouble(contrast);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetContrast", McEx);
            }
        }

        private void btnNegativeSet_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetNegative(CurrLayer, Channel, chxNegativeUse.Checked);

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetNegative", McEx);
            }
        }

        private void btnNegativeGet_Click(object sender, EventArgs e)
        {
            try
            {
                bool bUse = m_CurrentObject.GetNegative(CurrLayer, Channel);

                chxNegativeUse.Checked = bUse;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetNegative", McEx);
            }
        }

        private void btnGammaSet_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetGamma(CurrLayer, Channel, ntxGamma.GetDouble());

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetGamma", McEx);
            }
        }

        private void btnGammaGet_Click(object sender, EventArgs e)
        {
            try
            {
                double gamma = m_CurrentObject.GetGamma(CurrLayer, Channel);

                ntxGamma.SetDouble(gamma);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetGamma", McEx);
            }
        }

        private void btnWhiteBalanceBrightnessSet_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetWhiteBalanceBrightness(CurrLayer, ntxWhiteBalanceBrightnessR.GetByte(), ntxWhiteBalanceBrightnessG.GetByte(), ntxWhiteBalanceBrightnessB.GetByte());

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetWhiteBalanceBrightness", McEx);
            }
        }

        private void btnWhiteBalanceBrightnessGet_Click(object sender, EventArgs e)
        {
            try
            {           
                byte r,g,b;
                m_CurrentObject.GetWhiteBalanceBrightness(CurrLayer, out r, out g, out b);

                ntxWhiteBalanceBrightnessR.SetByte(r);
                ntxWhiteBalanceBrightnessG.SetByte(g);
                ntxWhiteBalanceBrightnessB.SetByte(b);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetWhiteBalanceBrightness", McEx);
            }
        }

        private void btnIsHistogramSet_Click(object sender, EventArgs e)
        {
            try
            {
                bool isSet = m_CurrentObject.IsOriginalHistogramSet(CurrLayer, Channel);

                chxIsHistogramSet.Checked = isSet;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IsOriginalHistogramSet", McEx);
            }
        }

        private void btnCurrentHistogramGet_Click(object sender, EventArgs e)
        {
            try
            {
                Int64[] currHistogram = m_CurrentObject.GetCurrentHistogram(CurrLayer, Channel);

                txtCurrentHistogram.Text = ConvertIntArrToHistogramString(currHistogram);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCurrentHistogram", McEx);
            }
        }

        private void btnHistogramEqualizationSet_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetHistogramEqualization(CurrLayer, Channel, chxHistogramEqualizationUse.Checked);

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetHistogramEqualization", McEx);
            }
        }

        private void btnHistogramEqualizationGet_Click(object sender, EventArgs e)
        {
            try
            {
                bool bUse = m_CurrentObject.GetHistogramEqualization(CurrLayer, Channel);

                chxHistogramEqualizationUse.Checked = bUse;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetHistogramEqualization", McEx);
            }
        }

        private void btnHistogramFitSet_Click(object sender, EventArgs e)
        {
            try
            {
                Int64[] referenceHistogram = ConvertHistogramToIntArr(txtHistogramFitReferenceHistogram.Text);
                m_CurrentObject.SetHistogramFit(CurrLayer, Channel, chxHistogramFitUse.Checked, referenceHistogram);

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetHistogramFit", McEx);
            }
        }

        private void btnHistogramFitGet_Click(object sender, EventArgs e)
        {
            try
            {
                Int64[] referenceHistogram;
                bool bUse = m_CurrentObject.GetHistogramFit(CurrLayer, Channel, out referenceHistogram);

                chxHistogramFitUse.Checked = bUse;
                txtHistogramFitReferenceHistogram.Text = ConvertIntArrToHistogramString(referenceHistogram);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetHistogramFit", McEx);
            }
        }

        private void btnOriginalHistogramSet_Click(object sender, EventArgs e)
        {
            try
            {
                Int64[] histogram = ConvertHistogramToIntArr(txtOriginalHistogram.Text);
                m_CurrentObject.SetOriginalHistogram(CurrLayer, Channel, histogram);

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetOriginalHistogram", McEx);
            }
        }

        private void btnGetVisibleAreaOriginalHistogram_Click(object sender, EventArgs e)
        {
            try
            {
                bool bUse = m_CurrentObject.GetVisibleAreaOriginalHistogram(CurrLayer, Channel);

                chxVisibleAreaOriginalHistogram.Checked = bUse;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetHistogramEqualization", McEx);
            }
        }

        private void btnSetVisibleAreaOriginalHistogram_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetVisibleAreaOriginalHistogram(CurrLayer, Channel, chxVisibleAreaOriginalHistogram.Checked);

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetHistogramEqualization", McEx);
            }
        }

        private void btnOriginalHistogramGet_Click(object sender, EventArgs e)
        {
            try
            {
                Int64[] histogram = m_CurrentObject.GetOriginalHistogram(CurrLayer, Channel);

                txtOriginalHistogram.Text = ConvertIntArrToHistogramString(histogram);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetOriginalHistogram", McEx);
            }
        }

        private void btnHistogramNormalizationSet_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetHistogramNormalization(CurrLayer, Channel, chxHistogramNormalizationUse.Checked, ntxHistogramNormalizationMean.GetDouble(), ntxHistogramNormalizationStdev.GetDouble());

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetHistogramNormalization", McEx);
            }
        }

        private void btnHistogramNormalizationGet_Click(object sender, EventArgs e)
        {
            try
            {
                bool bUse;
                double mean, stdev;
                m_CurrentObject.GetHistogramNormalization(CurrLayer, Channel, out bUse, out mean, out stdev);

                chxHistogramNormalizationUse.Checked = bUse;
                ntxHistogramNormalizationMean.SetDouble(mean);
                ntxHistogramNormalizationStdev.SetDouble(stdev);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetHistogramNormalization", McEx);
            }
        }

        private void btnSelectWhitePoint_Click(object sender, EventArgs e)
        {
            m_containerForm = GetParentForm(this);
            //hide the container form
            m_containerForm.Hide();

            //register to click on active map
            MCTester.GUI.Map.MCTMapForm.OnMapClicked += new MCTester.GUI.Map.ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
        }

        void MCTMapForm_OnMapClicked(Point PointOnMap, DNSMcVector3D PointIn3D, DNSMcVector3D PointInImage, bool IsHasIntersection)
        {
            MCTester.GUI.Map.MCTMapForm.OnMapClicked -= new MCTester.GUI.Map.ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
            
            uint pixelFormatByteCount = DNMcTexture.GetPixelFormatByteCount(DNEPixelFormat._EPF_A8R8G8B8);

            uint puWidth, puHeight;
            MCTMapFormManager.MapForm.Viewport.GetViewportSize(out puWidth, out puHeight);

            uint stride = puWidth * pixelFormatByteCount;
            uint bufferSize = stride * puHeight;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.AllocHGlobal((int)bufferSize);

            DNSMcRect rect = new DNSMcRect(new Point(0, 0), new Point((int)puWidth, (int)puHeight));
            
            try
            {
                MCTMapFormManager.MapForm.Viewport.RenderScreenRectToBuffer(rect, puWidth, puHeight, DNEPixelFormat._EPF_A8R8G8B8, 0, ptr);

                Bitmap bmp = new Bitmap((int)puWidth, (int)puHeight, (int)stride, PixelFormat.Format32bppArgb, ptr);

                Color pixelColor = bmp.GetPixel(PointOnMap.X, PointOnMap.Y);
                ntxWhiteBalanceBrightnessR.SetByte(pixelColor.R);
                ntxWhiteBalanceBrightnessG.SetByte(pixelColor.G);
                ntxWhiteBalanceBrightnessB.SetByte(pixelColor.B);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("RenderScreenRectToBuffer", McEx);
            }

            m_containerForm.Show();
        }

        private void cmbImageProcessingStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            DNEImageProcessingStage stage = (DNEImageProcessingStage)Enum.Parse(typeof(DNEImageProcessingStage), cmbImageProcessingStage.Text);

            switch (stage)
            {
                case DNEImageProcessingStage._EIPS_ALL:
                    ntxBrightness.SetFloat(ImageProcAll);
                    break;
                case DNEImageProcessingStage._EIPS_RASTER_LAYERS:
                    ntxBrightness.SetFloat(ImageProcRasterLayers);
                    break;
                case DNEImageProcessingStage._EIPS_WITHOUT_OBJECTS:
                    ntxBrightness.SetFloat(ImageProcWithoutObjects);
                    break;
            }
        }

        private void btnAddPostProcess_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.AddPostProcess(txtPostProcess.Text);	
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("AddPostProcess", McEx);
            }
        }

        private void btnRemovePostProcess_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.RemovePostProcess(txtPostProcess.Text);	
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("RemovePostProcess", McEx);
            }
        }

        private void btnMaterialSchemeDefinitionApply_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetMaterialSchemeDefinition(txtMaterialSchemeName.Text, txtMaterialNameToCopyFrom.Text);	
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetMaterialSchemeDefinition", McEx);
            }
        }

        private void btnStereoModeApply_Click(object sender, EventArgs e)
        {
            SetStereoMode();
        }

        private void btnClipPlane_Click(object sender, EventArgs e)
        {
            try
            {
                List<DNSMcPlane> lPlanes = new List<DNSMcPlane>();
                DNSMcPlane plane;  
                DNSMcVector3D normal = new DNSMcVector3D();
                double location;
                int rowIndex = 0;

                while (!dgvClipPlane.Rows[rowIndex].IsNewRow)
                {
                    normal.x = double.Parse(dgvClipPlane[0, rowIndex].Value.ToString());
                    normal.y = double.Parse(dgvClipPlane[1, rowIndex].Value.ToString());
                    normal.z = double.Parse(dgvClipPlane[2, rowIndex].Value.ToString());
                    location = double.Parse(dgvClipPlane[3, rowIndex].Value.ToString());

                    plane = new DNSMcPlane(normal, location);
                    lPlanes.Add(plane);

                    rowIndex++;
                }

                m_CurrentObject.SetClipPlanes(lPlanes.ToArray());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetClipPlanes", McEx);
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
        }

        private void SetStereoMode()
        {
            try
            {
                DNSStereoParams oParams = new DNSStereoParams();
                oParams.fCameraPositionOffset = ntxStereoCameraPositionOffset.GetFloat();
                oParams.fCameraCenterOffset = ntxStereoCameraCenterOffset.GetFloat();
                oParams.bLeftEyeMaster = chxLeftEyeMaster.Checked;
                               
				DNEStereoRenderMode eStereoMode = (DNEStereoRenderMode)Enum.Parse(typeof(DNEStereoRenderMode), cmbStereoMode.Text);

                m_CurrentObject.SetStereoMode(eStereoMode, oParams);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetStereoMode", McEx);
            }

        }

        private void dgvClipPlane_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && dgvClipPlane[4, e.RowIndex].ReadOnly == false)
            {
                m_containerForm = GetParentForm(this);
                //hide the container form
                m_containerForm.Hide();

                MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.MapObjectCtrl.MouseClickEvent += new MouseClickEventArgs(MapObjectCtrl_MouseClickEvent);       
            }
        }

        private void dgvClipPlane_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        void MapObjectCtrl_MouseClickEvent(object sender, Point mouseLocation)
        {
            MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.MapObjectCtrl.MouseClickEvent -= new MouseClickEventArgs(MapObjectCtrl_MouseClickEvent);

            IDNMcMapViewport Viewport = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport;

            bool isIntersect;
            DNSMcVector3D worldPoint = new DNSMcVector3D();
            DNSMcVector3D screenPoint = new DNSMcVector3D();
            screenPoint.x = mouseLocation.X;
            screenPoint.y = mouseLocation.Y;

            DNSQueryParams queryParams = new DNSQueryParams();
            
            Viewport.ScreenToWorldOnTerrain(screenPoint, out worldPoint, out isIntersect, queryParams);

            if (isIntersect == false)
                Viewport.ScreenToWorldOnPlane(screenPoint, out worldPoint, out isIntersect);
            if (isIntersect == false)
            {
                worldPoint.x = 0;
                worldPoint.y = 0;
                worldPoint.z = 0;
            }           

            int currRow = dgvClipPlane.CurrentRow.Index;
            DNSMcVector3D normal = new DNSMcVector3D();
            double result = 0;

            if (dgvClipPlane[0, currRow].Value != null && dgvClipPlane[1, currRow].Value != null && dgvClipPlane[2, currRow].Value != null)
            {
                normal.x = (double.TryParse(dgvClipPlane[0, currRow].Value.ToString(), out result) == true) ? result : 0;
                normal.y = (double.TryParse(dgvClipPlane[1, currRow].Value.ToString(), out result) == true) ? result : 0;
                normal.z = (double.TryParse(dgvClipPlane[2, currRow].Value.ToString(), out result) == true) ? result : 0;

                DNSMcPlane plane = new DNSMcPlane(ref normal, ref worldPoint);
                dgvClipPlane[3, currRow].Value = plane.dLocation;
            }
            else
                MessageBox.Show("Can't calculate location without any Normal\nPlease insert Normal and try again", "Can not calculate location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
            //show the form
            m_containerForm.Show();
        }

        private void btnOverlayVisibilityOption_Click(object sender, EventArgs e)
        {
            List<IDNMcOverlay> overlaysToChange = new List<IDNMcOverlay>();

            for (int i = 0; i < m_lstOverlayValue.Count; i++)
            {
                if (chlOverlayVisibilityOption.GetItemChecked(i))
                {
                    overlaysToChange.Add(m_lstOverlayValue[i]);
                }
            }

            DNEActionOptions ao = (DNEActionOptions)Enum.Parse(typeof(DNEActionOptions), cmbOverlayActionOption.Text);
            
            try
            {
                m_CurrentObject.SetOverlaysVisibilityOption(ao, overlaysToChange.ToArray());

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetOverlaysVisibilityOption", McEx);
            }            
        }

        private void btnObjectsVisibilityOption_Click(object sender, EventArgs e)
        {
            List<IDNMcObject> objectsToChange = new List<IDNMcObject>();

            for (int i = 0; i < m_lstObjectsValue.Count; i++)
            {
                if (chlObjectsVisibilityOption.GetItemChecked(i))
                {
                    objectsToChange.Add(m_lstObjectsValue[i]);
                }
            }

            DNEActionOptions ao = (DNEActionOptions)Enum.Parse(typeof(DNEActionOptions), cmbObjectsActionOption.Text);

            try
            {
                m_CurrentObject.SetObjectsVisibilityOption(ao, objectsToChange.ToArray());

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetObjectsVisibilityOption", McEx);
            }  
        }

        private void lstLayers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > lstLayers.ItemHeight * lstLayers.Items.Count)
            {
                CurrLayer = null;
                lstLayers.ClearSelected();
            }
        }

        private void dgvDtmVisualColors_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)  // color column
            {
                ColorDialog colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    DataGridViewButtonCell buttCell = dgvDtmVisualColors[0, e.RowIndex] as DataGridViewButtonCell;
                    buttCell.FlatStyle = FlatStyle.Popup;
                    buttCell.Style.BackColor = colorDialog.Color;
                    if (dgvDtmVisualColors[1, e.RowIndex].Value == null)
                    {
                        ((DataGridViewComboBoxCell)dgvDtmVisualColors[1, e.RowIndex]).Value = 255;
                    }
                }
            }
        }

        private void btnDtmVisualizationWithoutRasterDefaultOK_Click(object sender, EventArgs e)
        {
            DNSDtmVisualizationParams DtmVisualizationParams = SetDtmVisualizationParamsForm();
            DtmVisualizationParams.SetDefaultHeightColors(ntxDtmVisualizationWithoutRasterMinHeight.GetFloat(),
                                                    ntxDtmVisualizationWithoutRasterMaxHeight.GetFloat());

            LoadDtmVisualization(DtmVisualizationParams);
        }

        private void btnResetForm_Click(object sender, EventArgs e)
        {
            DNSDtmVisualizationParams DtmVisualizationParams = new DNSDtmVisualizationParams();
            LoadDtmVisualization(DtmVisualizationParams);
        }

        private void btnGetObjectsVisibleInWorldRect_Click(object sender, EventArgs e)
        {
            if (CheckMissingData(true))
            {
                string fileName = SaveFileDlg();
                if (fileName != String.Empty)
                {
                    DNSMcBox mcBoxWorldRect = boxWorldRect.GetBoxValue();
                    float fCameraScale = ntbCameraScale.GetFloat();
                    bool isExistBBObject = boxWorldRect.GetIsShowBBObject();
                    if(isExistBBObject)
                        boxWorldRect.HideBBHide();
                    IDNMcObject[] objectsToSave = m_CurrentObject.GetObjectsVisibleInWorldRectAndScale2D(mcBoxWorldRect, fCameraScale);
                    if(isExistBBObject)
                        boxWorldRect.ShowBBHide();

                    Manager_MCOverlayManager.ActiveOverlay.SaveObjects(objectsToSave, fileName, Manager_MCObject.GetStorageFormatByFileName(fileName));
                }
            }
        }

        private string SaveFileDlg()
        {
            SFD.Filter = "MapCore Object Files (*.mcobj, *.mcobj.json , *.m, *.json) |*.mcobj; *.mcobj.json; *.m; *.json;|" +
            "MapCore Object Binary Files (*.mcobj,*.m)|*.mcobj;*.m;|" +
            "MapCore Object Json Files (*.mcobj.json, *.json)|*.mcobj.json;*.json;|" +
            "All Files|*.*";

            SFD.RestoreDirectory = true;
            if (SFD.ShowDialog() == DialogResult.OK)
            {
                return SFD.FileName;
            }
            else
                return "";
        }

        bool CheckMissingData(bool checkPoints = false)
        {
            if (ntbCameraScale.Text == "" || ntbCameraScale.Text == "0")
            {
                MessageBox.Show("Missing Camera Scale", "Empty Values Msg");
                return false;
            }
            if (checkPoints && 
                ((boxWorldRect.GetBoxValue() == null) || 
                (boxWorldRect.GetBoxValue() != null && boxWorldRect.GetBoxValue().CenterPoint() == null) ||
                (boxWorldRect.GetBoxValue().CenterPoint() != null &&
                boxWorldRect.GetBoxValue().CenterPoint().x == 0 &&
                boxWorldRect.GetBoxValue().CenterPoint().y == 0 &&
                boxWorldRect.GetBoxValue().CenterPoint().z == 0)))
            {
                MessageBox.Show("Missing Points Values", "Empty Values Msg");
                return false;
            }
            return true;

        }

        private void tpColorTable_Click(object sender, EventArgs e)
        {

        }

        private void btnGetTerrainByID_Click(object sender, EventArgs e)
        {
            bool isShowError = false;
            string strTerrainId = ntxGetTerrainById.Text;
            if (strTerrainId != "")
            {
                uint TerrainId;
                if (UInt32.TryParse(strTerrainId, out TerrainId))
                {
                    try
                    {
                        IDNMcMapTerrain mcTerrain = m_CurrentObject.GetTerrainByID(TerrainId);
                        if (mcTerrain != null)
                        {
                            lnkTerrainName.Text = Manager_MCNames.GetNameByObject(mcTerrain);
                            lnkTerrainName.Tag = mcTerrain;
                        }
                        else
                            MessageBox.Show("IDNMcMapViewport.GetTerrainByID", "Not exist Terrain with id " + TerrainId, MessageBoxButtons.OK);

                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetTerrainByID", McEx);
                        return;
                    }
                }
                else
                    isShowError = true;
            }
            else
                isShowError = true;

            if (isShowError)
            {
                MessageBox.Show("Terrain Id should be positive number", "Invalid Terrain Id", MessageBoxButtons.OK);
                ntxGetTerrainById.Focus();
            }
        }

        private void lnkTerrainName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lnkTerrainName.Tag != null)
            {
                Control control = Parent.Parent.Parent;
                TreeViewDisplayForm mcTreeView = control as TreeViewDisplayForm;
                if (mcTreeView != null)
                {
                    
                    mcTreeView.SelectNodeInTreeNode((uint)lnkTerrainName.Tag.GetHashCode());
                }
            }
        }

        private void btnConImageToWorld_Click(object sender, EventArgs e)
        {
            List<object> listInputs = new List<object>();
            listInputs.Add(ctrl2DCameraConImagePoint.GetVector2D());
            listInputs.Add(chxRequestSeparateIntersectionStatus.Checked);
            listInputs.Add(chxImageUseCache.Checked);

            MCTImageCalcCallback imageCalcCallback = null;

            if (chxImageUseCache.Checked)
            {

                if (chxImagePixelAsync.Checked)
                    imageCalcCallback = new MCTImageCalcCallback(this, listInputs, AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorldWithCache);
                try
                {
                    DNMcNullableOut<bool> pbDTMAvailable = new DNMcNullableOut<bool>();
                    DNMcNullableOut<DNEMcErrorCode> peIntersectionStatus = new DNMcNullableOut<DNEMcErrorCode>();
                    DNSMcVector3D worldPoint = mImageCalc.ImagePixelToCoordWorldWithCache(ctrl2DCameraConImagePoint.GetVector2D(), pbDTMAvailable,
                        chxRequestSeparateIntersectionStatus.Checked ? peIntersectionStatus : null,
                        imageCalcCallback);

                    if (!chxImagePixelAsync.Checked)
                    {
                        if ((!chxRequestSeparateIntersectionStatus.Checked) || (chxRequestSeparateIntersectionStatus.Checked && peIntersectionStatus.Value == DNEMcErrorCode.SUCCESS))
                        {
                            List<object> listResults = new List<object>();
                            listResults.Add(true);                              // peIntersectionStatus == DNEMcErrorCode.SUCCESS
                            listResults.Add(worldPoint);
                            double? dist = ((bool)pbDTMAvailable.Value) ? 1 : 0;
                            listResults.Add(dist);

                            AddResultToList(AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorldWithCache, listResults, false, listInputs);
                        }
                        else if (chxRequestSeparateIntersectionStatus.Checked && peIntersectionStatus.Value != DNEMcErrorCode.SUCCESS)
                        {
                            AddErrorToList(AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorldWithCache, peIntersectionStatus.Value, chxImagePixelAsync.Checked, listInputs, false);
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    AddErrorToList(AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorldWithCache, McEx.ErrorCode, chxImagePixelAsync.Checked, listInputs);
                    Utilities.ShowErrorMessage("ImagePixelToCoordWorldWithCache", McEx);
                }
            }
            else
            {
                if (chxImagePixelAsync.Checked)
                    imageCalcCallback = new MCTImageCalcCallback(this, listInputs, AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorld);

                try
                {
                    DNMcNullableOut<bool> pbDTMAvailable = new DNMcNullableOut<bool>();
                    DNMcNullableOut<DNEMcErrorCode> peIntersectionStatus = new DNMcNullableOut<DNEMcErrorCode>();
                    DNSMcVector3D worldPoint = mImageCalc.ImagePixelToCoordWorld(ctrl2DCameraConImagePoint.GetVector2D(), pbDTMAvailable, chxRequestSeparateIntersectionStatus.Checked ? peIntersectionStatus : null, imageCalcCallback);

                    if (!chxImagePixelAsync.Checked)
                    {
                        if ((!chxRequestSeparateIntersectionStatus.Checked) || (chxRequestSeparateIntersectionStatus.Checked && peIntersectionStatus.Value == DNEMcErrorCode.SUCCESS))
                        {
                            List<object> listResults = new List<object>();
                            listResults.Add(true);                              // peIntersectionStatus == DNEMcErrorCode.SUCCESS
                            listResults.Add(worldPoint);
                            double? dist = ((bool)pbDTMAvailable) ? 1 : 0;
                            listResults.Add(dist);

                            AddResultToList(AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorld, listResults, false, listInputs);
                        }
                        else if (chxRequestSeparateIntersectionStatus.Checked && peIntersectionStatus.Value != DNEMcErrorCode.SUCCESS)
                        {
                            AddErrorToList(AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorld, peIntersectionStatus.Value, chxImagePixelAsync.Checked, listInputs, false);
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    AddErrorToList(AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorld, McEx.ErrorCode, chxImagePixelAsync.Checked, listInputs);
                    Utilities.ShowErrorMessage("WorldCoordToImagePixel", McEx);
                }
            }
        }

        private void btnCoordWorldToImagePixel_Click(object sender, EventArgs e)
        {
            List<object> listInputs = new List<object>();
            listInputs.Add(ctrl3DCameraConWorldPoint.GetVector3D());
            listInputs.Add(chxRequestSeparateIntersectionStatus.Checked);
            listInputs.Add(chxImageUseCache.Checked);
            txtIntersectionStatus.Text = "";

            if (chxImageUseCache.Checked)
            {
                try
                {
                    DNMcNullableOut<DNEMcErrorCode> peIntersectionStatus = new DNMcNullableOut<DNEMcErrorCode>();
                    ctrl2DCameraConImagePoint.SetVector2D(mImageCalc.WorldCoordToImagePixelWithCache(ctrl3DCameraConWorldPoint.GetVector3D(), chxRequestSeparateIntersectionStatus.Checked ? peIntersectionStatus : null));
                    if ((!chxRequestSeparateIntersectionStatus.Checked) || (chxRequestSeparateIntersectionStatus.Checked && peIntersectionStatus.Value == DNEMcErrorCode.SUCCESS))
                    {
                        List<object> listResults = new List<object>();
                        listResults.Add(true);                              // peIntersectionStatus == DNEMcErrorCode.SUCCESS
                        listResults.Add(ctrl2DCameraConImagePoint.GetVector2D());

                        AddResultToList(AsyncImageCalcQueryFunctionName.WorldCoordToImagePixelWithCache, listResults, false, listInputs);
                    }
                    else if (chxRequestSeparateIntersectionStatus.Checked && peIntersectionStatus.Value != DNEMcErrorCode.SUCCESS)
                    {
                        AddErrorToList(AsyncImageCalcQueryFunctionName.WorldCoordToImagePixelWithCache, peIntersectionStatus.Value, false, listInputs, false);
                    }
                    // txtIntersectionStatus.Text = IDNMcErrors.ErrorCodeToString(peIntersectionStatus);

                    // MessageBox.Show(IDNMcErrors.ErrorCodeToString(peIntersectionStatus), "WorldCoordToImagePixel", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (MapCoreException McEx)
                {
                    AddErrorToList(AsyncImageCalcQueryFunctionName.WorldCoordToImagePixelWithCache, McEx.ErrorCode, false, listInputs);
                    Utilities.ShowErrorMessage("WorldCoordToImagePixel", McEx);
                }
            }
            else
            {
                try
                {
                    DNMcNullableOut<DNEMcErrorCode> peIntersectionStatus = new DNMcNullableOut<DNEMcErrorCode>();
                    ctrl2DCameraConImagePoint.SetVector2D( mImageCalc.WorldCoordToImagePixel(ctrl3DCameraConWorldPoint.GetVector3D(), chxRequestSeparateIntersectionStatus.Checked ? peIntersectionStatus : null));
                    if ((!chxRequestSeparateIntersectionStatus.Checked) || (chxRequestSeparateIntersectionStatus.Checked && peIntersectionStatus.Value == DNEMcErrorCode.SUCCESS))
                    {
                        List<object> listResults = new List<object>();
                        listResults.Add(true);                              // peIntersectionStatus == DNEMcErrorCode.SUCCESS
                        listResults.Add(ctrl2DCameraConImagePoint.GetVector2D());

                        AddResultToList(AsyncImageCalcQueryFunctionName.WorldCoordToImagePixel, listResults, false, listInputs);
                    }
                    else if (chxRequestSeparateIntersectionStatus.Checked && peIntersectionStatus.Value != DNEMcErrorCode.SUCCESS)
                    {
                        AddErrorToList(AsyncImageCalcQueryFunctionName.WorldCoordToImagePixel, peIntersectionStatus.Value, false, listInputs, false);
                    }
                    // txtIntersectionStatus.Text = IDNMcErrors.ErrorCodeToString(peIntersectionStatus);

                    // MessageBox.Show(IDNMcErrors.ErrorCodeToString(peIntersectionStatus), "WorldCoordToImagePixel", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (MapCoreException McEx)
                {
                    AddErrorToList(AsyncImageCalcQueryFunctionName.WorldCoordToImagePixel, McEx.ErrorCode, false, listInputs);
                    Utilities.ShowErrorMessage("WorldCoordToImagePixel", McEx);
                }
            }
        }

        private void btnImagePixelToCoordWorldOnHorzPlane_Click(object sender, EventArgs e)
        {
            List<object> listInputs = new List<object>();
            listInputs.Add(ctrl2DCameraConImagePoint.GetVector2D());
            listInputs.Add(chxRequestSeparateIntersectionStatus.Checked);
            listInputs.Add(ntbPlaneHeight.GetDouble());

            txtIntersectionStatus.Text = "";
            try
            {
                DNMcNullableOut<DNEMcErrorCode> peIntersectionStatus = new DNMcNullableOut<DNEMcErrorCode>();
                ctrl3DCameraConWorldPoint.SetVector3D( mImageCalc.ImagePixelToCoordWorldOnHorzPlane(ctrl2DCameraConImagePoint.GetVector2D(), ntbPlaneHeight.GetDouble(), chxRequestSeparateIntersectionStatus.Checked ? peIntersectionStatus : null));

                if ((!chxRequestSeparateIntersectionStatus.Checked) || (chxRequestSeparateIntersectionStatus.Checked && peIntersectionStatus.Value == DNEMcErrorCode.SUCCESS))
                {
                    List<object> listResults = new List<object>();
                    listResults.Add(true);                              // peIntersectionStatus == DNEMcErrorCode.SUCCESS
                    listResults.Add(ctrl3DCameraConWorldPoint.GetVector3D());

                    AddResultToList(AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorldOnHorzPlane, listResults, false, listInputs);
                }
                else if (chxRequestSeparateIntersectionStatus.Checked && peIntersectionStatus.Value != DNEMcErrorCode.SUCCESS)
                {
                    AddErrorToList(AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorldOnHorzPlane, peIntersectionStatus.Value, false, listInputs, false);
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorldOnHorzPlane, McEx.ErrorCode, false, listInputs);
                Utilities.ShowErrorMessage("ImagePixelToCoordWorldOnHorzPlane", McEx);
            }
        }

        /*private void btnImagePixelToCoordWorldWithCache_Click(object sender, EventArgs e)
        {
            List<object> listInputs = new List<object>();
            listInputs.Add(ctrl2DCameraConImagePoint.GetVector2D());
            listInputs.Add(chxRequestSeparateIntersectionStatus.Checked);

            MCTImageCalcCallback imageCalcCallback = null;
            if (chxImagePixelAsync.Checked)
                imageCalcCallback = new MCTImageCalcCallback(this, listInputs, AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorldWithCache);
            try
            {
                DNMcNullableOut<bool> pbDTMAvailable = new DNMcNullableOut<bool>();
                DNMcNullableOut<DNEMcErrorCode> peIntersectionStatus = new DNMcNullableOut<DNEMcErrorCode>();
                DNSMcVector3D worldPoint = mImageCalc.ImagePixelToCoordWorldWithCache(ctrl2DCameraConImagePoint.GetVector2D(). pbDTMAvailable,
                    chxRequestSeparateIntersectionStatus.Checked ? peIntersectionStatus : null, 
                    imageCalcCallback);

                if (!chxImagePixelAsync.Checked)
                {
                    if ((!chxRequestSeparateIntersectionStatus.Checked) || (chxRequestSeparateIntersectionStatus.Checked && peIntersectionStatus == DNEMcErrorCode.SUCCESS))
                    {
                        List<object> listResults = new List<object>();
                        listResults.Add(true);                              // peIntersectionStatus == DNEMcErrorCode.SUCCESS
                        listResults.Add(worldPoint);
                        listResults.Add(null);                              // 1 - Normal
                        double? dist = pbDTMAvailable ? 1 : 0;
                        listResults.Add(dist);

                        AddResultToList(AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorldWithCache, listResults, false, listInputs);
                    }
                    else if(chxRequestSeparateIntersectionStatus.Checked && peIntersectionStatus != DNEMcErrorCode.SUCCESS)
                    {
                        AddErrorToList(AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorldWithCache, peIntersectionStatus, chxImagePixelAsync.Checked, listInputs);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorldWithCache, McEx.ErrorCode, chxImagePixelAsync.Checked, listInputs);
                Utilities.ShowErrorMessage("ImagePixelToCoordWorldWithCache", McEx);
            }
           
        }
        */

        private void btnGetHeight_Click(object sender, EventArgs e)
        {
            List<object> listInputs = new List<object>();
            listInputs.Add(ctrl2DGetHeight.GetVector2D());
            MCTImageCalcCallback imageCalcCallback = null;
            if (chxGetHeightAsync.Checked)
                imageCalcCallback = new MCTImageCalcCallback(this, listInputs, AsyncImageCalcQueryFunctionName.GetHeight);
            try
            {
                double height = mImageCalc.GetHeight(ctrl2DGetHeight.GetVector2D().x, ctrl2DGetHeight.GetVector2D().y, imageCalcCallback);
                if (!chxGetHeightAsync.Checked)
                {
                    List<object> listResults = new List<object>();
                    listResults.Add(height);

                    AddResultToList(AsyncImageCalcQueryFunctionName.GetHeight, listResults, false, listInputs);
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncImageCalcQueryFunctionName.GetHeight, McEx.ErrorCode, chxGetHeightAsync.Checked, listInputs);
                Utilities.ShowErrorMessage("GetHeight", McEx);
            }
        }

        private void btnIsWorldCoordVisible_Click(object sender, EventArgs e)
        {
            List<object> listInputs = new List<object>();
            listInputs.Add(ctrl3DIsWorldCoordVisible.GetVector3D());
            MCTImageCalcCallback imageCalcCallback = null;
            if (chxIsWorldAsync.Checked)
                imageCalcCallback = new MCTImageCalcCallback(this, listInputs, AsyncImageCalcQueryFunctionName.IsWorldCoordVisible);

            try
            {
                bool isWorld = mImageCalc.IsWorldCoordVisible(ctrl3DIsWorldCoordVisible.GetVector3D(), imageCalcCallback);

                if (!chxIsWorldAsync.Checked)
                {
                    List<object> listResults = new List<object>();
                    listResults.Add(isWorld);

                    AddResultToList(AsyncImageCalcQueryFunctionName.IsWorldCoordVisible, listResults, false, listInputs);
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncImageCalcQueryFunctionName.IsWorldCoordVisible, McEx.ErrorCode, chxIsWorldAsync.Checked, listInputs);
                Utilities.ShowErrorMessage("IsWorldCoordVisible", McEx);
            }
           
        }

        public void AddResultToList(AsyncImageCalcQueryFunctionName functionName, List<Object> listResults, bool isAsync, List<Object> listInputs)
        {
            int index = dgvAsyncQueryResults.Rows.Add();
            dgvAsyncQueryResults.Rows[index].Cells[0].Value = index + 1;
            dgvAsyncQueryResults.Rows[index].Cells[1].Value = functionName.ToString();
            dgvAsyncQueryResults.Rows[index].Cells[2].Value = isAsync;
            dgvAsyncQueryResults.Rows[index].Tag = listResults;
            dgvAsyncQueryResults.Rows[index].Cells[0].Tag = listInputs;
            
            ClickShowResults(index);
        }

        public void AddErrorToList(AsyncImageCalcQueryFunctionName functionName, DNEMcErrorCode eErrorCode, bool isAsync, List<Object> listInputs, bool isEx = true)
        {
            int index = dgvAsyncQueryResults.Rows.Add();
            dgvAsyncQueryResults.Rows[index].Cells[0].Value = index + 1;
            dgvAsyncQueryResults.Rows[index].Cells[1].Value = functionName.ToString();
            dgvAsyncQueryResults.Rows[index].Cells[2].Value = isAsync;
            dgvAsyncQueryResults.Rows[index].Cells[2].Tag = eErrorCode;
            dgvAsyncQueryResults.Rows[index].Cells[3].Value = (isEx? "Exception: " : "Intersection Error: ") + IDNMcErrors.ErrorCodeToString(eErrorCode);
            dgvAsyncQueryResults.Rows[index].Cells[3].Tag = isEx;
            dgvAsyncQueryResults.Rows[index].Cells[0].Tag = listInputs;
            ClickShowResults(index);
        }

        private void dgvAsyncQueryResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!dgvAsyncQueryResults.Rows[e.RowIndex].IsNewRow)
                {
                    ClickShowResults(e.RowIndex);
                }
            }
        }

        private void ClickShowResults(int rowIndex)
        {

            for (int i = 0; i < dgvAsyncQueryResults.Rows.Count; i++)
            {
                dgvAsyncQueryResults.Rows[i].Selected = false;
            }
            dgvAsyncQueryResults.Rows[rowIndex].Selected = true;
            string functionName = dgvAsyncQueryResults.Rows[rowIndex].Cells[1].Value.ToString();
            AsyncImageCalcQueryFunctionName asyncImageCalcQueryFunctionName = (AsyncImageCalcQueryFunctionName)Enum.Parse(typeof(AsyncImageCalcQueryFunctionName), functionName);
            List<Object> listResults = (List<Object>)dgvAsyncQueryResults.Rows[rowIndex].Tag;
            List<Object> listInputs = (List<Object>)dgvAsyncQueryResults.Rows[rowIndex].Cells[0].Tag;
            bool isAsync = (bool)dgvAsyncQueryResults.Rows[rowIndex].Cells[2].Value;
            bool isError = (dgvAsyncQueryResults.Rows[rowIndex].Cells[3].Value != null);
            bool isEx = (dgvAsyncQueryResults.Rows[rowIndex].Cells[3].Tag != null && (bool)dgvAsyncQueryResults.Rows[rowIndex].Cells[3].Tag);
            DNEMcErrorCode mcErrorCode = DNEMcErrorCode.SUCCESS;
            if(dgvAsyncQueryResults.Rows[rowIndex].Cells[2].Tag != null )
                mcErrorCode = (DNEMcErrorCode)dgvAsyncQueryResults.Rows[rowIndex].Cells[2].Tag;

            switch (asyncImageCalcQueryFunctionName)
            {
                case AsyncImageCalcQueryFunctionName.GetHeight:
                    GetTerrainHeightShowInputs(isAsync, (DNSMcVector2D)listInputs[0]);
                    if (!isError)
                        GetTerrainHeightResult(isAsync, (double)listResults[0]);
                    break;
                case AsyncImageCalcQueryFunctionName.IsWorldCoordVisible:
                    GetPointVisibilityShowInputs(isAsync, listInputs);
                    if (!isError)
                        GetPointVisibilityResults((bool)listResults[0]);
                    break;
                case AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorld:
                case AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorldWithCache:
                    GetRayIntersectionImageToWorldShowInputs(isAsync, listInputs);
                    if (isError && !isEx)
                        GetRayIntersectionImageToWorldShowError(isAsync, (bool)listInputs[1], mcErrorCode);
                    else if (!isError)
                        GetRayIntersectionImageToWorldResults(isAsync, (bool)listResults[0], (DNSMcVector3D?)listResults[1], (double?)listResults[2]);
                    break;
                case AsyncImageCalcQueryFunctionName.WorldCoordToImagePixel:
                case AsyncImageCalcQueryFunctionName.WorldCoordToImagePixelWithCache:
                    GetRayIntersectionWorldToImageShowInputs(isAsync, listInputs);
                    if (isError && !isEx)
                        GetRayIntersectionWorldToImageShowError(isAsync, (bool)listInputs[1], mcErrorCode);
                    else if(!isError)
                        GetRayIntersectionWorldToImageResults(isAsync, (bool)listResults[0], (DNSMcVector2D)listResults[1]);
                    break;
                case AsyncImageCalcQueryFunctionName.ImagePixelToCoordWorldOnHorzPlane:
                    GetRayIntersectionImageToWorldOnHorzPlaneShowInputs(isAsync, listInputs);
                    if (isError && !isEx)
                        GetRayIntersectionImageToWorldShowError(isAsync, (bool)listInputs[1], mcErrorCode);
                    else if (!isError)
                        GetRayIntersectionImageToWorldResults(isAsync, (bool)listResults[0], (DNSMcVector3D?)listResults[1], null);
                    break;
                default:break;
            }
        }

        private void GetTerrainHeightShowInputs(bool isAsync, DNSMcVector3D TerrainHeightPt)
        {
            ctrl2DGetHeight.SetVector2D(new DNSMcVector2D(TerrainHeightPt.x, TerrainHeightPt.y));
            chxGetHeightAsync.Checked = isAsync;
        }

        private void GetTerrainHeightResult(bool isAsync, double pHeight)
        {
            ntbGetHeight.SetDouble(pHeight);
        }

        private void GetPointVisibilityShowInputs(bool isAsync, List<Object> listInputs)
        {
            chxIsWorldAsync.Checked = isAsync;
            ctrl3DIsWorldCoordVisible.SetVector3D( (DNSMcVector3D)listInputs[0]);
        }

        private void GetPointVisibilityResults(bool isTargetVisible)
        {
            chxIsVisible.Checked = isTargetVisible;
        }

        private void GetRayIntersectionImageToWorldResults(bool isAsync, bool bIntersectionFound, DNSMcVector3D? intersectionPt, double? dist)
        {
            if (intersectionPt != null)
                ctrl3DCameraConWorldPoint.SetVector3D( intersectionPt.Value);
            else
                ctrl3DCameraConWorldPoint.SetVector3D( new DNSMcVector3D());

            if (dist != null)
                chxDTMAvailable.Checked = dist.Value == 1 ? true : false;
            
        }

        private void GetRayIntersectionImageToWorldShowInputs(bool isAsync, List<Object> listInputs)
        {
            chxImagePixelAsync.Checked = isAsync;
            ctrl2DCameraConImagePoint.SetVector2D((DNSMcVector2D)listInputs[0]);
            chxRequestSeparateIntersectionStatus.Checked = (bool)listInputs[1];
            chxImageUseCache.Checked = (bool)listInputs[2];

            ntbPlaneHeight.Text = "";
            txtIntersectionStatus.Text = "";
        }

        private void GetRayIntersectionImageToWorldOnHorzPlaneShowInputs(bool isAsync, List<Object> listInputs)
        {
            txtIntersectionStatus.Text = "";

           // chxImagePixelAsync.Checked = isAsync;
            ctrl2DCameraConImagePoint.SetVector2D((DNSMcVector2D)listInputs[0]);
            chxRequestSeparateIntersectionStatus.Checked = (bool)listInputs[1];
            ntbPlaneHeight.SetDouble((double)listInputs[2]);
        }

        private void GetRayIntersectionImageToWorldShowError(bool isAsync, bool requestSeparateIntersectionStatus, DNEMcErrorCode mcErrorCode)
        {
            if (requestSeparateIntersectionStatus)
                txtIntersectionStatus.Text = IDNMcErrors.ErrorCodeToString(mcErrorCode);
            
        }

        private void GetRayIntersectionWorldToImageResults(bool isAsync, bool bIntersectionFound, DNSMcVector3D? intersectionPt)
        {
            if (intersectionPt != null)
                ctrl2DCameraConImagePoint.SetVector2D(new DNSMcVector2D( intersectionPt.Value.x, intersectionPt.Value.y));
            else
                ctrl2DCameraConImagePoint.SetVector2D(new DNSMcVector2D());

        }

        private void GetRayIntersectionWorldToImageShowInputs(bool isAsync, List<Object> listInputs)
        {

            txtIntersectionStatus.Text = "";

            //chxImagePixelAsync.Checked = isAsync;
            ctrl3DCameraConWorldPoint.SetVector3D( (DNSMcVector3D)listInputs[0]);
            chxRequestSeparateIntersectionStatus.Checked = (bool)listInputs[1];
            chxImageUseCache.Checked = (bool)listInputs[2];

            ntbPlaneHeight.Text = "";
        }

        private void GetRayIntersectionWorldToImageShowError(bool isAsync, bool requestSeparateIntersectionStatus, DNEMcErrorCode mcErrorCode)
        {
            if (requestSeparateIntersectionStatus)
                txtIntersectionStatus.Text = IDNMcErrors.ErrorCodeToString(mcErrorCode);
            else
                txtIntersectionStatus.Text = "";
        }

        private void btnSetSectionRoutePoints_Click(object sender, EventArgs e)
        {
            if (m_SectionMapCurrentObject != null)
            {
                try
                {
                    m_sectionPoint = null;
                    if (ctrlSectionRoutePoints.GetPoints(out m_sectionPoint))
                    {
                        if (cbIsCalculateSectionHeightPoints.Checked)
                        {
                            DNSQueryParams queryParams = new DNSQueryParams();
                            queryParams.eTerrainPrecision = DNEQueryPrecision._EQP_HIGHEST;
                            queryParams.pAsyncQueryCallback = new MCTAsyncQueryCallbackSectionMap(this);
                            float[] slopes = new float[0];
                            DNSSlopesData pSlopesData;
                            m_SectionMapCurrentObject.GetTerrainHeightsAlongLine(m_sectionPoint, out slopes, out pSlopesData, queryParams);
                        }
                        else
                            m_SectionMapCurrentObject.SetSectionRoutePoints(m_sectionPoint);
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetSectionRoutePoints", McEx);
                }
            }
        }

        private void HideForm()
        {
            m_ParentForm = this.Parent.Parent.Parent;
            m_ParentForm.Hide();
        }

        private void DeleteSectionMapLine()
        {
            if (m_SectionMapObject != null)
            {
                m_SectionMapObject.Remove();
                m_SectionMapObject.Dispose();
                m_SectionMapObject = null;
            }
        }

        private void btnDrawSectionMapLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (MCTMapFormManager.MapForm != null)
                {
                    //this.Hide();
                    HideForm();
                    DeleteSectionMapLine();

                    IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                    DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];

                    IDNMcObjectSchemeItem ObjSchemeItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                                                DNELineStyle._ELS_SOLID,
                                                                                DNSMcBColor.bcBlackOpaque,
                                                                                3f);

                    IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                        ObjSchemeItem,
                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                        locationPoints,
                                                        false);

                    m_EditMode = MCTMapFormManager.MapForm.EditMode;
                    m_EditMode.StartInitObject(obj, ObjSchemeItem);
                    m_Callback = m_EditMode.GetEventsCallback();
                    m_EditMode.SetEventsCallback(this);
                }
                else
                {
                    MessageBox.Show("You can't capture points from map because there is no open map.\nYou can only insert points manually .", "Capture Points Is Illegal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (MapCoreException McEx)
            {
                m_ParentForm.Show();
                MapCore.Common.Utilities.ShowErrorMessage("SectionMapLine", McEx);
            }
        }

        public void ExitAction(int nExitCode)
        {
            m_EditMode.SetEventsCallback(m_Callback);
        }

        public void NewVertex(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex, double dAngle)
        {
            //throw new NotImplementedException();
        }

        public void PointDeleted(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex)
        {
            //throw new NotImplementedException();
        }

        public void PointNewPos(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex, double dAngle, bool bDownOnHeadPoint)
        {
            //throw new NotImplementedException();
        }

        public void ActiveIconChanged(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNEPermission eIconPermission, uint uIconIndex)
        {
        }

        public void InitItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            m_SectionMapObject = pObject;

            ctrlSectionRoutePoints.SetPoints(pObject.GetLocationPoints(0));

            m_ParentForm.Show();
        }

        public void EditItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            //throw new NotImplementedException();
        }

        public void DragMapResults(IDNMcMapViewport pViewport, DNSMcVector3D NewCenter)
        {
            //throw new NotImplementedException();
        }

        public void RotateMapResults(IDNMcMapViewport pViewport, float fNewYaw, float fNewPitch)
        {
            //throw new NotImplementedException();
        }

        public void DistanceDirectionMeasureResults(IDNMcMapViewport pViewport, DNSMcVector3D WorldVertex1, DNSMcVector3D WorldVertex2, double dDistance, double dAngle)
        {
            //throw new NotImplementedException();
        }

        public void DynamicZoomResults(IDNMcMapViewport pViewport, float fNewScale, DNSMcVector3D NewCenter)
        {
            //throw new NotImplementedException();
        }

        public void CalculateHeightResults(IDNMcMapViewport pViewport, double dHeight, DNSMcVector3D[] aCoords, int nStatus)
        {
            //throw new NotImplementedException();
        }

        public void CalculateVolumeResults(IDNMcMapViewport pViewport, double dVolume, DNSMcVector3D[] aCoords, int nStatus)
        {
            //throw new NotImplementedException();
        }

        private void chxGetHeightAsync_CheckedChanged(object sender, EventArgs e)
        {
            ctrlSamplePoint1.IsAsync = chxGetHeightAsync.Checked;
        }

        private void chxImagePixelAsync_CheckedChanged(object sender, EventArgs e)
        {
            ctrImagePt.IsAsync = chxImagePixelAsync.Checked;
        }

        private void chxIsWorldAsync_CheckedChanged(object sender, EventArgs e)
        {
            ctrlSamplePoint2.IsAsync = chxIsWorldAsync.Checked;
        }

        private void btnCancelAsyncQuery_Click(object sender, EventArgs e)
        {

        }

        private void btnSethistogramFromLayers_Click(object sender, EventArgs e)
        {
            Int64[][] histogram = new long[3][];

            if (CurrLayer != null)
            {
                histogram = CalcHistogram(CurrLayer);
            }
            else
            {
                histogram[0] = new long[256];
                histogram[1] = new long[256];
                histogram[2] = new long[256];

                foreach (IDNMcRasterMapLayer rasterMapLayer in lLayerTag)
                {
                    Int64[][] layerHistogram = CalcHistogram(rasterMapLayer);

                    if (layerHistogram != null)
                    {
                        for (int i = 0; i < 256; i++)
                        {
                            histogram[0][i] += layerHistogram[0][i];
                            histogram[1][i] += layerHistogram[1][i];
                            histogram[2][i] += layerHistogram[2][i];
                        }
                    }
                }
            }
            if (histogram != null)
            {
                DNEColorChannel colorChannel;
                if (cmbFunctionOnColorTableColorChannel.Text != "" && cmbFunctionOnColorTableColorChannel.Text != DNEColorChannel._ECC_MULTI_CHANNEL.ToString())
                {
                    colorChannel = (DNEColorChannel)Enum.Parse(typeof(DNEColorChannel), cmbFunctionOnColorTableColorChannel.Text);

                    try
                    {
                        m_CurrentObject.SetOriginalHistogram(CurrLayer, colorChannel, histogram[(int)colorChannel]);

                        // turn on all viewports render needed flags
                        MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("SetOriginalHistogram", McEx);
                    }
                }
                else
                {
                    try
                    {
                        m_CurrentObject.SetOriginalHistogram(CurrLayer, DNEColorChannel._ECC_RED, histogram[(int)DNEColorChannel._ECC_RED]);
                        m_CurrentObject.SetOriginalHistogram(CurrLayer, DNEColorChannel._ECC_GREEN, histogram[(int)DNEColorChannel._ECC_GREEN]);
                        m_CurrentObject.SetOriginalHistogram(CurrLayer, DNEColorChannel._ECC_BLUE, histogram[(int)DNEColorChannel._ECC_BLUE]);

                        // turn on all viewports render needed flags
                        MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("SetOriginalHistogram", McEx);
                    }
                }
            }
        }

        private Int64[][] CalcHistogram(IDNMcRasterMapLayer rasterMapLayer)
        {
            Int64[][] histogram = null;
            try
            {
                rasterMapLayer.CalcHistogram(out histogram);              
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("CalcHistogram", McEx);
            }
            return histogram;
        }

        private void lstLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadImageProcessing();
        }

        private void cmbFunctionOnColorTableColorChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadImageProcessing();
        }

        private void LoadImageProcessing()
        {
            /*if(tcImageProcessing.SelectedTab == tpFilters)
            {

            }
            else
            {
                btnUserColorValuesGet_Click(null, null);
                btnCurrentColorValuesGet_Click(null, null);
                btnColorTableBrightnessGet_Click(null, null);
                btnColorTableContrastGet_Click(null, null);
                btnNegativeGet_Click(null, null);
                btnGammaGet_Click(null, null);
            }*/
        }

        private void lstImageCalc_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > lstImageCalc.ItemHeight * lstImageCalc.Items.Count)
            {
                SelectedImageCalc = null;
                lstImageCalc.ClearSelected();
            }
        }

        private void tpSectionMap_Leave(object sender, EventArgs e)
        {
            DeleteSectionMapLine();
        }

        private void ucViewport_Leave(object sender, EventArgs e)
        {
            DeleteTempObjects();
        }
         
        public void CreateSectionMap(bool isCameFromGetTerrainHeightsAlongLine, DNSMcVector3D[] heightPoints)
        {
            m_SectionMapCurrentObject.SetSectionRoutePoints(m_sectionPoint, heightPoints);
            UpdateDGVSectionHeightPoints(heightPoints);
        }

        private void lstImageCalc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ctrlImagePt.SetImageCalc(SelectedImageCalc);
        }

        private void tpObjectWorld_Leave(object sender, EventArgs e)
        {
            boxWorldRect.RemoveBBObject();
        }

        private void tpObjectWorld_Enter(object sender, EventArgs e)
        {
            boxWorldRect.CheckBBObject();
        }

        private void tpGeneral_Enter(object sender, EventArgs e)
        {
            boxViewportWorldBoundingBox.CheckBBObject();
        }

        private void tpGeneral_Leave(object sender, EventArgs e)
        {
            boxViewportWorldBoundingBox.RemoveBBObject();
        }

        private void tpViewportsAsCamera_Enter(object sender, EventArgs e)
        {
            ctrlViewportAsCamera.CheckBoxesObjects(); 
          
        }

       /* protected override void DestroyHandle()
        {
            base.DestroyHandle();
            
            DeleteTempObjects();
        }*/

        public void DeleteTempObjects()
        {
            DeleteSectionMapLine();
            boxViewportWorldBoundingBox.RemoveBBObject();   // tab general
            ctrlViewportAsCamera.RemoveBBObject();          // tab 'viewport as camera' - tab 'any map type'
            boxWorldRect.RemoveBBObject() ;                 // tab 'object world'
        }

        private void tpViewportsAsCamera_Leave(object sender, EventArgs e)
        {
            DeleteTempObjects();
        }

        private void btnGetDebugOptionKey_Click(object sender, EventArgs e)
        {
            try
            {
                if (ntxDebugOptionKey.Text != "")
                {
                    ntxDebugOptionValue.SetInt(m_CurrentObject.GetDebugOption(ntxDebugOptionKey.GetUInt32()));
                }
                else
                    MessageBox.Show("Key cannot be empty, fix it and try again", "Get Debug Option", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetDebugOption", McEx);
            }
        }

        private void btnSetDebugOptionKey_Click(object sender, EventArgs e)
        {
            try
            {
                if (ntxDebugOptionKey.Text == "")
                { 
                    MessageBox.Show("Key cannot be empty, fix it and try again", "Set Debug Option", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (ntxDebugOptionValue.Text == "")
                {
                    MessageBox.Show("Value cannot be empty, fix it and try again", "Set Debug Option", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               
                m_CurrentObject.SetDebugOption(ntxDebugOptionKey.GetUInt32(), ntxDebugOptionValue.GetInt32());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetDebugOption", McEx);
            }
        }

        private void btnIncrement_Click(object sender, EventArgs e)
        {
            try
            {
                if (ntxDebugOptionKey.Text != "")
                {
                    m_CurrentObject.IncrementDebugOption(ntxDebugOptionKey.GetUInt32());
                    btnGetDebugOptionKey_Click(sender, e);
                }
                else
                    MessageBox.Show("Key cannot be empty, fix it and try again", "Increment Debug Option", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IncrementDebugOption", McEx);
            }
        }

        private void btnGetLocalMapFootprintScreenPositions_Click(object sender, EventArgs e)
        {
            MainForm.MinimizeMapWorldTreeViewForm();
            frmGlobalMapViewportPointsList GlobalMapViewportList = new frmGlobalMapViewportPointsList(m_CurrentObject, "SetActiveLoacalMap");
            GlobalMapViewportList.ShowDialog();
            MainForm.NormalMapWorldTreeViewForm();
                


        }
    }
}
