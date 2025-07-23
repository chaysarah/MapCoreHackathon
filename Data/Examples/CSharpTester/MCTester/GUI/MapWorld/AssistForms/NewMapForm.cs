using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MCTester.ObjectWorld.OverlayManagerWorld.WizardForms;
using MCTester.MapWorld.WizardForms;
using MapCore;
using UnmanagedWrapper;
using MCTester.GUI;
using MCTester.GUI.Map;
using MCTester.Managers;
using MCTester.Managers.MapWorld;
using MapCore.Common;
using MCTester.Automation;
using System.Linq;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class NewMapForm : Form, IDNEditModeCallback, ICreateSectionMap
    {
        private List<IDNMcMapTerrain> m_lstMcTerrain;
        private DNSCreateDataMV m_CreateData;
        private IDNMcMapViewport m_Viewport;
        private DNSMcVector3D[] m_sectionPoints;
        private List<IDNMcMapTerrain> m_lstTerrains;
        private IDNMcSectionMapViewport m_SectionMapViewport;
        private IDNMcMapCamera m_Camera;
        private IDNMcEditMode m_EditMode;
        private IDNEditModeCallback m_Callback;
        private Timer mLoadDelayLayersTimer = new Timer();
        private Timer mUpdateVPTimer = new Timer();
        private Timer mAllLayerInit = new Timer();
        private TerrainWizzardForm mTerrainWizFrm;
        private MCTMapForm mNewMapForm = null;
        private IDNMcObject m_SectionMapObject = null;
        private List<IDNMcMapLayer> m_QuerySecondaryDtmLayers;

        public NewMapForm()
        {
            InitializeComponent();
            m_lstMcTerrain = new List<IDNMcMapTerrain>();
            m_CreateData = null;
            m_Callback = null;
            mLoadDelayLayersTimer.Interval = 100;
            mLoadDelayLayersTimer.Tick += new EventHandler(LoadLayers_Tick);

            mUpdateVPTimer.Interval = 100;
            mUpdateVPTimer.Tick += new EventHandler(UpdateVP_Tick);

            mAllLayerInit.Interval = 100;
            mAllLayerInit.Tick += new EventHandler(AllLayerInit_Tick);

            ctrlSectionMapPoints.HideZ();
            
        }

        private void UpdateVP_Tick(object sender, EventArgs e)
        {
            if (chxIsOpenMapWithoutWaitAllLayersInit.Checked)
            {
                OpenMap();
            }
            else if (Manager_MCLayers.IsAllLayersInitialized(GetLayersOfViewport()) && !mLoadDelayLayersTimer.Enabled)
            {
                CenterMap();
                OpenMap();
            }
        }

        private void CenterMap()
        {
            try
            {
                m_Viewport.ActiveCamera.SetCameraPosition(ctrlCameraPosition.GetVector3D(), false);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetCameraPosition", McEx);
            }
        }

        private void OpenMap()
        {
            mNewMapForm.Show();
            MCTMapFormManager.AddMapForm(mNewMapForm);

            // turn on all viewports render needed flags
            Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();

            mUpdateVPTimer.Stop();

            CloseForm();

            if (chxIsOpenMapWithoutWaitAllLayersInit.Checked)
            {
                mAllLayerInit.Start();
            }
        }

        private List<IDNMcMapLayer> GetLayersOfViewport()
        {
            List<IDNMcMapLayer> layers = new List<IDNMcMapLayer>();
            foreach (IDNMcMapTerrain mcMapTerrain in m_Viewport.GetTerrains())
            {
                if (mcMapTerrain.GetLayers() != null)
                    layers.AddRange(mcMapTerrain.GetLayers());
            }
            return layers;
        }

        private void AllLayerInit_Tick(object sender, EventArgs e)
        {
            if (Manager_MCLayers.IsAllLayersInitialized(GetLayersOfViewport()))
            {
                mAllLayerInit.Stop();
                ShowMsgAllLayersInit();
                CenterMap();
            }
        }
        
        private void LoadLayers_Tick(object sender, EventArgs e)
        {
            if (Manager_MCLayers.IsAllLayersInitialized(GetSelectedLayers()))
            {
                mLoadDelayLayersTimer.Stop();
                
                if (!m_IsFormClosed)
                {
                    try
                    {
                        DNSMcBox boundingBox = CalcBoundingBox();

                        /*DNSMcBox boundingBox = mTerrainWizFrm.Terrain.BoundingBox;
                        ctrl3DMinBoundingBox.SetVector3D(boundingBox.MinVertex);
                        ctrl3DMaxBoundingBox.SetVector3D(boundingBox.MaxVertex);*/

                        if (ctrlCheckGroupBoxSectionMap.Checked == false)
                        {
                            ctrlCameraPosition.X = (boundingBox.MaxVertex.x + boundingBox.MinVertex.x) / 2;
                            ctrlCameraPosition.Y = (boundingBox.MaxVertex.y + boundingBox.MinVertex.y) / 2;
                            ctrlCameraPosition.Z = (boundingBox.MaxVertex.z + boundingBox.MinVertex.z) / 2;
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        mLoadDelayLayersTimer.Stop();
                        if (McEx.ErrorCode == DNEMcErrorCode.NOT_INITIALIZED)
                        {
                            if (!MCTMapLayerReadCallback.GetIsShowError())
                            {
                                MCTMapLayerReadCallback.SetIsShowError(true);
                                mLoadDelayLayersTimer.Stop();
                                MessageBox.Show("Viewport includes delayed load layers - can not set camera position to center!", "Set Camera Position To BoundingBox.CenterPoint");
                            }
                        }
                        else
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("Terrain.BoundingBox property", McEx);
                        }
                        this.Enabled = true;
                        return;
                    }
                }
                ShowMsgAllLayersInit();
            }
        }

        private List<IDNMcMapLayer> GetSelectedLayers()
        {
            List<IDNMcMapLayer> mcMapLayers = new List<IDNMcMapLayer>();
            List<IDNMcMapTerrain> mcMapTerrains = GetSelectedMapTerrain();

            try
            {
                foreach (IDNMcMapTerrain mcMapTerrain in mcMapTerrains)
                {
                    mcMapLayers.AddRange(mcMapTerrain.GetLayers());
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("CreateSection", McEx);
            }
            return mcMapLayers;
        }

        private bool m_IsShowMsgAllLayersInit = false;

        private void ShowMsgAllLayersInit()
        {
            if(!m_IsShowMsgAllLayersInit && chxIsOpenMapWithoutWaitAllLayersInit.Checked)
            {
                MessageBox.Show("All layers are initialized!", "Open viewport without waiting until all layers are initialized");
                m_IsShowMsgAllLayersInit = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                m_IsShowMsgAllLayersInit = false ;
                uint viewportID = createDataMVCtrl.ViewportID;
                if (Manager_MCViewports.AllParams.ContainsKey(viewportID))                                        
                {
                    MessageBox.Show("Viewport ID already exist!\n Insert other ID number.");
                }
                else
                {
                    m_CreateData = createDataMVCtrl.CreateData;
                    mNewMapForm = createDataMVCtrl.MapForm;

                    // add to list only the selected terrains
                    m_lstTerrains = GetSelectedMapTerrain();
                    /*m_lstTerrains = new List<IDNMcMapTerrain>();
                    for (int i = 0; i < m_lstMcTerrain.Count; i++)
                    {
                        if (lstTerrains.GetSelected(i) == true)
                            m_lstTerrains.Add(m_lstMcTerrain[i]);
                    }*/
                   

                    if (ctrlCheckGroupBoxSectionMap.Checked == false)
                    {
                        try
                        {
                            DNMcMapViewport.Create(ref m_Viewport,
                                                        ref m_Camera,
                                                        m_CreateData,
                                                        m_lstTerrains.ToArray(),
                                                        Manager_MCLayers.GetLayersAsDTM(m_QuerySecondaryDtmLayers));

                           // if(m_QuerySecondaryDtmLayers != null)
                            //    Manager_MCLayers.RemoveStandaloneLayers(m_QuerySecondaryDtmLayers.ToArray());

                            try
                            {
                                mNewMapForm.CreateEditMode(m_Viewport);
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("CreateEditMode", McEx);
                            }
                            this.Enabled = false;
                            mNewMapForm.Viewport = m_Viewport;

                            Manager_MCViewports.AddViewport(m_Viewport);

                            m_Viewport.PerformPendingUpdates(DNEPendingUpdateType._EPUT_ANY_UPDATE);
                            ReleaseTerrains();
                            
                            mUpdateVPTimer.Start();
                        }
                        catch (MapCoreException McEx)
                        {
                            if (McEx.ErrorCode == DNEMcErrorCode.INVALID_PARAMETERS && m_CreateData.pDevice == null)
                                MessageBox.Show("Missing  Device, You have to create one", "Create Map Viewport");
                            else
                                MapCore.Common.Utilities.ShowErrorMessage("Create Map Viewport", McEx);
                            this.Enabled = true;
                        }

                    }
                    else
                    {
                        try
                        {
                            if (ctrlSectionMapPoints.GetPoints(out m_sectionPoints))
                            {
                                if (cbIsCalculateSectionHeightPoints.Checked)
                                {
                                    DNSQueryParams queryParams = new DNSQueryParams();
                                    queryParams.eTerrainPrecision = DNEQueryPrecision._EQP_HIGHEST;
                                    queryParams.pAsyncQueryCallback = new MCTAsyncQueryCallbackSectionMap(this);
                                    DNSSlopesData slopesData = new DNSSlopesData();
                                    float[] slopes = new float[0];
                                    MCTMapFormManager.MapForm.Viewport.GetTerrainHeightsAlongLine(m_sectionPoints, out slopes, out slopesData, queryParams);
                                }
                                else
                                {
                                    CreateSectionMap(false, null);
                                }
                            }
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("CreateSection", McEx);
                        }
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                if (mNewMapForm != null)
                    mNewMapForm.Close();

                MapCore.Common.Utilities.ShowErrorMessage("Create Viewport", McEx);
            }
        }

        private void ReleaseTerrains()
        {
            // remove terrain from dic
            for (int i = 0; i < m_lstTerrains.Count; i++)
            {
                Manager_MCTerrain.RemoveTerrainFromDic(m_lstTerrains[i]);
            }
            m_lstTerrains.Clear();
        }

        public void CreateSectionMap(bool isCameFromGetTerrainHeightsAlongLine, DNSMcVector3D[] heightPoints)
        {
            try
            {
                if (isCameFromGetTerrainHeightsAlongLine)
                {
                    DNMcSectionMapViewport.CreateSection(ref m_SectionMapViewport,
                                                         ref m_Camera,
                                                         m_CreateData,
                                                         m_lstTerrains.ToArray(),
                                                         m_sectionPoints,
                                                         heightPoints);
                }
                else
                {
                    DNMcSectionMapViewport.CreateSection(ref m_SectionMapViewport,
                                                         ref m_Camera,
                                                         m_CreateData,
                                                         m_lstTerrains.ToArray(),
                                                         m_sectionPoints);
                }
                try
                {
                    mNewMapForm.CreateEditMode(m_SectionMapViewport);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("CreateEditMode", McEx);
                }

                mNewMapForm.Viewport = m_SectionMapViewport;

                Manager_MCViewports.AddViewport(m_SectionMapViewport);

                m_SectionMapViewport.PerformPendingUpdates(DNEPendingUpdateType._EPUT_ANY_UPDATE);

                m_SectionMapViewport.ActiveCamera.SetCameraPosition(ctrlCameraPosition.GetVector3D(), false);

                mNewMapForm.Show();
                MCTMapFormManager.AddMapForm(mNewMapForm);

                // turn on all viewports render needed flags
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
                ReleaseTerrains();
                CloseForm();
            }
            catch (MapCoreException McEx)
            {
                if (mNewMapForm != null)
                    mNewMapForm.Close();

                MapCore.Common.Utilities.ShowErrorMessage("CreateSection", McEx);
            }
        }

        private bool m_IsFormClosed = false;

        private void CloseForm()
        {
            DeleteSectionMapLine();
            this.Close();
            m_IsFormClosed = true;
        }

        private DNSMcBox CalcBoundingBox()
        {
            DNSMcBox boundingBox = new DNSMcBox();
            try
            {
                if (Manager_MCLayers.IsAllLayersInitialized(GetSelectedLayers()))
                {
                    List<IDNMcMapTerrain> mcMapTerrains = GetSelectedMapTerrain();
                    if (mcMapTerrains != null && mcMapTerrains.Count > 0)
                    {
                        boundingBox = mcMapTerrains[0].BoundingBox;

                        for (int i = 1; i < mcMapTerrains.Count; i++)
                        {
                            boundingBox.Union(boundingBox, mcMapTerrains[i].BoundingBox);
                        }
                    }
                    ctrl3DMinBoundingBox.SetVector3D(boundingBox.MinVertex);
                    ctrl3DMaxBoundingBox.SetVector3D(boundingBox.MaxVertex);
                    
                    if (ctrlCheckGroupBoxSectionMap.Checked == false)
                    {
                        ctrlCameraPosition.SetVector3D(boundingBox.CenterPoint());
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("CalcBoundingBox", McEx);
            }
            return boundingBox;
        }

        private List<IDNMcMapTerrain> GetSelectedMapTerrain()
        {
            List<IDNMcMapTerrain> lstSelectedTerrains = new List<IDNMcMapTerrain>();

            for (int i = 0; i < lstTerrains.Items.Count; i++)
            {
                if (lstTerrains.GetSelected(i) == true)
                    lstSelectedTerrains.Add(m_lstMcTerrain[i]);
            }

            return lstSelectedTerrains;
        }

        private void btnAddTerrain_Click(object sender, EventArgs e)
        {
            mTerrainWizFrm = new TerrainWizzardForm();
            Manager_MCLayers.AddTerrainWizzardFormToList(mTerrainWizFrm);
            try
            {
                if (mTerrainWizFrm.ShowDialog() == DialogResult.OK)
                {
                    if (mTerrainWizFrm.Terrain != null)
                    {
                        m_lstMcTerrain.Add(mTerrainWizFrm.Terrain);
                        lstTerrains.Items.Add(Manager_MCNames.GetNameByObject(mTerrainWizFrm.Terrain, "Terrain"));
                        lstTerrains.SetSelected(lstTerrains.Items.Count - 1, true);

                        if (mTerrainWizFrm.Terrain.GetLayers().Length != 0)
                        {
                            mLoadDelayLayersTimer.Start();
                        }
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Add terrain", McEx);
            }
            Manager_MCLayers.RemoveTerrainWizzardFormFromList(mTerrainWizFrm);
        }

        private void btnRemoveTerrain_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTerrains.SelectedItems.Count > 0)
                {
                    int SelectedIdx = lstTerrains.SelectedIndex;
                    lstTerrains.Items.RemoveAt(SelectedIdx);
                    m_lstMcTerrain.RemoveAt(SelectedIdx);

                    if (m_lstMcTerrain.Count == 0)
                        mLoadDelayLayersTimer.Stop();

                    CalcBoundingBox();
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Remove Terrain", McEx);
            }

        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            lstTerrains.ClearSelected();
            CalcBoundingBox() ;
        }

        private void ctrlCheckGroupBoxSectionMap_CheckedChanged(object sender, EventArgs e)
        {
            if (ctrlCheckGroupBoxSectionMap.Checked == true)
                ctrlCameraPosition.X = ctrlCameraPosition.Y = ctrlCameraPosition.Z = 0;
        }

        private void btnSectionMapLine_Click(object sender, EventArgs e)
        {
            this.Hide();

            try
            {
                if (MCTMapFormManager.MapForm != null)
                {
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
                MapCore.Common.Utilities.ShowErrorMessage("SectionMapLine", McEx);
            }
        }

        #region IDNEditModeCallback Members

        public void ExitAction(int nExitCode)
        {
            m_EditMode.SetEventsCallback(m_Callback);
        }

        public void NewVertex(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex, double dAngle)
        {
        }

        public void PointDeleted(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex)
        {
        }

        public void PointNewPos(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex, double dAngle, bool bDownOnHeadPoint)
        {
        }

        public void ActiveIconChanged(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNEPermission eIconPermission, uint uIconIndex)
        {

        }

        public void InitItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            m_SectionMapObject = pObject;
            ctrlSectionMapPoints.SetPoints(pObject.GetLocationPoints(0));
            this.ShowDialog();
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
        public void EditItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
        }

        public void DragMapResults(IDNMcMapViewport pViewport, DNSMcVector3D NewCenter)
        {
        }

        public void RotateMapResults(IDNMcMapViewport pViewport, float fNewYaw, float fNewPitch)
        {
        }

        public void DistanceDirectionMeasureResults(IDNMcMapViewport pViewport, DNSMcVector3D WorldVertex1, DNSMcVector3D WorldVertex2, double dDistance, double dAngle)
        {
        }

        public void DynamicZoomResults(IDNMcMapViewport pViewport, float fNewScale, DNSMcVector3D NewCenter)
        {
        }

        public void CalculateHeightResults(IDNMcMapViewport pViewport, double dHeight, DNSMcVector3D[] coords, int status)
        {
        }

        public void CalculateVolumeResults(IDNMcMapViewport pViewport, double dVolume, DNSMcVector3D[] coords, int status)
        {
        }

        #endregion

        private void NewMapForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mLoadDelayLayersTimer.Enabled)
                mLoadDelayLayersTimer.Stop();
            if (mUpdateVPTimer.Enabled)
                mUpdateVPTimer.Stop();
            
            MCTMapLayerReadCallback.ResetReadCallbackCounter(!chxIsOpenMapWithoutWaitAllLayersInit.Checked);
            DeleteSectionMapLine();
            m_lstMcTerrain.Clear();
            if (m_QuerySecondaryDtmLayers != null)
                Manager_MCLayers.RemoveStandaloneLayers(m_QuerySecondaryDtmLayers.ToArray());
        }

        private void btnAddDTMLayers_Click(object sender, EventArgs e)
        {
            LayersWizzardForm layersWizzardForm = new LayersWizzardForm(m_QuerySecondaryDtmLayers, true);
            if (layersWizzardForm.ShowDialog() == DialogResult.OK)
            {
                m_QuerySecondaryDtmLayers = layersWizzardForm.GetLayers();
            }
        }

        private void lstTerrains_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcBoundingBox();
        }
    }

}