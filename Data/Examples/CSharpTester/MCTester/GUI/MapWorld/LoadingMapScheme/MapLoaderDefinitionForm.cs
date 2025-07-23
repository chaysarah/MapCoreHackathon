using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MCTester.MapWorld;
using MCTester.MapWorld.LoadingMapScheme;
using MapCore;
using MCTester.Managers.MapWorld;

namespace MCTester.GUI.Map
{
    public partial class MapLoaderDefinitionForm : Form
    {
        private List<int> m_lChildCheckListBoxIDs;
        private List<int> m_lCoordSysListBoxIDs;
        private List<int> m_lOMListBoxIDs;
        private List<int> m_lImageCalcListBoxIDs;

        private Color m_NewEntityColor;
        private Color m_DefaultEntityColor;
        private bool m_IsDataChanged;



        public MapLoaderDefinitionForm()
        {
            InitializeComponent();
            LoadForm();

            m_IsDataChanged = false;

            m_NewEntityColor = Color.FromKnownColor(KnownColor.GradientActiveCaption);
            m_DefaultEntityColor = Color.FromKnownColor(KnownColor.Control);

            m_lChildCheckListBoxIDs = new List<int>();
            m_lOMListBoxIDs = new List<int>();
            m_lImageCalcListBoxIDs = new List<int>();
            m_lCoordSysListBoxIDs = new List<int>();

            DNSInitParams initParams = new DNSInitParams();
            initParams.eLoggingLevel = DNELoggingLevel._ELL_HIGH;
            initParams.eTerrainObjectsQuality = DNETerrainObjectsQuality._ETOQ_MEDIUM;
            initParams.eRenderingSystem = DNERenderingSystem._ERS_AUTO_SELECT;

            ctrlDeviceParams1.SetDeviceParams(initParams);

            cmbViewport_MapType.Items.AddRange(Enum.GetNames(typeof(DNEMapType)));
            cmbViewport_MapType.Text = DNEMapType._EMT_2D.ToString();

            cmbImageCalc_Type.Items.AddRange(Enum.GetNames(typeof(DNEImageType)));
            cmbImageCalc_Type.Text = DNEImageType._EIT_FRAME.ToString();

            cmbViewport_DtmUsageAndPrecision.Items.AddRange(Enum.GetNames(typeof(DNEQueryPrecision)));
            cmbViewport_DtmUsageAndPrecision.Text = DNEQueryPrecision._EQP_DEFAULT.ToString();

            ucLayerParams1.HideNoOfLayers();
        }

        private void LoadForm()
        {
            if (MCTester.Managers.Manager_MCGeneralDefinitions.mFirstMapLoaderDefinition == false)
            {
                ntxNumBackgroundThreads.Enabled = false;
                chxMultiScreenDevice.Enabled = false;
                chxMultiThreadedDevice.Enabled = false;
            }

            ntxNumBackgroundThreads.SetUInt32(MCTester.Managers.Manager_MCGeneralDefinitions.m_NumBackgroundThreads);
            ntxTerrainResolutionFactor.SetFloat(MCTester.Managers.Manager_MCGeneralDefinitions.m_TerrainResolutionFactor);
            chxMultiScreenDevice.Checked = MCTester.Managers.Manager_MCGeneralDefinitions.m_MultiScreenDevice;
            chxMultiThreadedDevice.Checked = MCTester.Managers.Manager_MCGeneralDefinitions.m_MultiThreadedDevice;

            lstMapSchemas.Items.Clear();

            int i = 0;
            if (MainForm.MapLoaderDefinitionManager != null)
            {
                foreach (MCTMapSchema schema in MainForm.MapLoaderDefinitionManager.Schemas.Schema)
                {
                    MCTOverlayManager schemaOverlayManager = MainForm.MapLoaderDefinitionManager.OverlayManagers.GetOverlayManager(schema.OverlayManagerID);
                    MCTGridCoordinateSystem OMCordSys = null;

                    if (schemaOverlayManager != null)
                        OMCordSys = MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.GetCoordSys(schemaOverlayManager.CoordSysID);

                    string[] schemaName = new string[5];
                    schemaName[0] = schema.ID.ToString(); // i.ToString();
                    schemaName[1] = schema.Area;
                    schemaName[2] = schema.MapType;
                    schemaName[3] = (OMCordSys != null) ? OMCordSys.GridCoordinateSystemType.ToString() : "NULL";
                    schemaName[4] = schema.Comments;
                    ListViewItem lvItems = new ListViewItem(schemaName);

                    lstMapSchemas.Items.Add(lvItems);
                    i++;
                }
            }
        }

        private void btnLoadSchema_Click(object sender, EventArgs e)
        {
            LoadMapScheme();
        }

        private void lstMapSchemas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LoadMapScheme();
        }

        private void LoadMapScheme()
        {
            if (lstMapSchemas.SelectedItems.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;

                Managers.Manager_MCGeneralDefinitions.m_NumBackgroundThreads = ntxNumBackgroundThreads.GetUInt32();
                Managers.Manager_MCGeneralDefinitions.m_TerrainResolutionFactor = ntxTerrainResolutionFactor.GetFloat();
                Managers.Manager_MCGeneralDefinitions.m_MultiScreenDevice = chxMultiScreenDevice.Checked;
                Managers.Manager_MCGeneralDefinitions.m_MultiThreadedDevice = chxMultiThreadedDevice.Checked;
                Managers.Manager_MCGeneralDefinitions.mFirstMapLoaderDefinition = false;

                int selectedSchema = int.Parse(lstMapSchemas.SelectedItems[0].SubItems[0].Text);
                //MCTMapSchema selectedMapSchema2 = MainForm.MapLoaderDefinitionManager.Schemas.Schema[selectedSchema];
                MCTMapSchema selectedMapSchema = MainForm.MapLoaderDefinitionManager.Schemas.GetSchema(selectedSchema);
                MainForm.MapLoaderDefinitionManager.LoadSchema(selectedMapSchema, chxIsWpfWindow.Checked);
                this.Close();

                this.Cursor = Cursors.Default;
            }
            else
                MessageBox.Show("Please choose map scheme or close the form in order to open tester without map", "No Map Scheme Was Chosen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void tcSchemesDefine_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _TextBrush;
            
            // Get the item from the collection.
            TabPage _TabPage = tcSchemesDefine.TabPages[e.Index];
             
            // Get the real bounds for the tab rectangle.
            Rectangle _TabBounds = tcSchemesDefine.GetTabRect(e.Index);
            
            if(e.State == DrawItemState.Selected)
            {
                // Draw a different background color, and don't paint a focus rectangle.
                _TextBrush = new SolidBrush(Color.Red);
                g.FillRectangle(Brushes.LightGray, e.Bounds);
            }
            else
            {
                _TextBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }
             
            // Use our own font.
            Font _TabFont = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Pixel);
             
            // Draw string. Center the text.
            StringFormat _StringFlags = new StringFormat();
            _StringFlags.Alignment = StringAlignment.Center;
            _StringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_TabPage.Text, _TabFont, _TextBrush, 
                         _TabBounds, new StringFormat(_StringFlags));

        }

        public List<int> lChildCheckListBoxIDs
        {
            get { return m_lChildCheckListBoxIDs; }
            set { m_lChildCheckListBoxIDs = value; }
        }

        public List<int> lCoordSysListBoxIDs
        {
            get { return m_lCoordSysListBoxIDs; }
            set { m_lCoordSysListBoxIDs = value; }
        }

        public List<int> lOMListBoxIDs
        {
            get { return m_lOMListBoxIDs; }
            set { m_lOMListBoxIDs = value; }
        }

        public List<int> lImageCalcListBoxIDs
        {
            get { return m_lImageCalcListBoxIDs; }
            set { m_lImageCalcListBoxIDs = value; }
        }

        private void tcSchemesDefine_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPage.Name)
            {
                case "tpDevice":
                    lstImageCalc.Visible = false;
                    lblImageCalc.Visible = false;
                    lstOverlayManager.Visible = false;
                    lblOverlayManager.Visible = false;
                    lstGridCoordinateSystem.Visible = false;
                    lstTargetGridCoordinateSystem.Visible = false;
                    lblGridCoordSys.Visible = false;
                    clstChilds.Visible = false;
                    lblChild.Visible = false;
                    LoadDeviceParams();
                    break;
                case "tpScheme":
                    lstImageCalc.Visible = false;
                    lblImageCalc.Visible = false;
                    lstOverlayManager.Visible = true;
                    lblOverlayManager.Visible = true;
                    lstGridCoordinateSystem.Visible = false;
                    lstTargetGridCoordinateSystem.Visible = false;
                    lblGridCoordSys.Visible = false;
                    clstChilds.Visible = true;
                    lblChild.Visible = true;
                    LoadViewportCheckListBox();
                    LoadOverlayManagerListBox();
                    if (MainForm.MapLoaderDefinitionManager.Schemas.Schema.Count > 0)
                    {
                        //nudScheme_ID.Minimum = 1;
                        //nudScheme_ID.Maximum = MainForm.MapLoaderDefinitionManager.Schemas.Schema.Count;

                        cmbScheme_ID.Items.Clear();
                       
                        for (int i = 0; i < MainForm.MapLoaderDefinitionManager.Schemas.Schema.Count; i++)
                        {
                            MCTMapSchema mctMapSchema = MainForm.MapLoaderDefinitionManager.Schemas.Schema[i];
                            cmbScheme_ID.Items.Add(GetItemDesc(mctMapSchema.ID, string.Format("{0} {1} ({2})",mctMapSchema.Area, mctMapSchema.MapType, mctMapSchema.Comments)));
                        }
                        cmbScheme_ID.SelectedIndex = 0;
                    }
                    tpScheme.BackColor = m_DefaultEntityColor;
                    LoadSchemaParams();
                    break;
                case "tpViewport":
                    lstImageCalc.Visible = true;
                    lblImageCalc.Visible = true;
                    lstOverlayManager.Visible = false;
                    lblOverlayManager.Visible = false;
                    lstGridCoordinateSystem.Visible = true;
                    lblGridCoordSys.Visible = true;
                    clstChilds.Visible = true;
                    lblChild.Visible = true;
                    LoadTerrainCheckListBox();                    
                    LoadGridCoordinateSystemListBox();
                    LoadImageCalcListBox();
                    if (MainForm.MapLoaderDefinitionManager.Viewports.Viewport.Count > 0)
                    {
                        cmbViewport_ID.Items.Clear();
                        
                        for (int i = 0; i < MainForm.MapLoaderDefinitionManager.Viewports.Viewport.Count; i++)
                        {
                            MCTMapViewport mctMapViewport = MainForm.MapLoaderDefinitionManager.Viewports.Viewport[i];
                            cmbViewport_ID.Items.Add(GetItemDesc(mctMapViewport.ID, mctMapViewport.Name));
                        }
                        cmbViewport_ID.SelectedIndex = 0;
                    }
                    tpViewport.BackColor = m_DefaultEntityColor;
                    LoadViewportParams();
                    break;
                case "tpTerrain":
                    lstImageCalc.Visible = false;
                    lblImageCalc.Visible = false;
                    lstOverlayManager.Visible = false;
                    lblOverlayManager.Visible = false;
                    lstGridCoordinateSystem.Visible = true;
                    lblGridCoordSys.Visible = true;
                    clstChilds.Visible = true;
                    lblChild.Visible = true;
                    LoadLayerListBox();
                    LoadGridCoordinateSystemListBox();
                    if (MainForm.MapLoaderDefinitionManager.Terrains.Terrain.Count > 0)
                    {
                        cmbTerrain_ID.Items.Clear();
                      
                        for (int i = 0; i < MainForm.MapLoaderDefinitionManager.Terrains.Terrain.Count; i++)
                        {
                            MCTMapTerrain mctMapTerrain = MainForm.MapLoaderDefinitionManager.Terrains.Terrain[i];
                            cmbTerrain_ID.Items.Add(GetItemDesc( mctMapTerrain.ID, mctMapTerrain.Name));
                        }
                        cmbTerrain_ID.SelectedIndex = 0;
                    }
                    tpTerrain.BackColor = m_DefaultEntityColor;
                    LoadTerrainParams();
                    break;
                case "tpLayer":
                    lstImageCalc.Visible = false;
                    lblImageCalc.Visible = false;
                    lstOverlayManager.Visible = false;
                    lblOverlayManager.Visible = false;
                    lstGridCoordinateSystem.Visible = false;
                    lblGridCoordSys.Visible = false;
                    clstChilds.Visible = false;
                    lblChild.Visible = false;
                    LoadGridCoordinateSystemListBox();
                    if (MainForm.MapLoaderDefinitionManager.Layers.Layer.Count > 0)
                    {
                        cmbLayer_ID.Items.Clear();
                    	for (int i = 0; i < MainForm.MapLoaderDefinitionManager.Layers.Layer.Count; i++)
                        {
                            MCTMapLayer mctMapLayer = MainForm.MapLoaderDefinitionManager.Layers.Layer[i];
                            cmbLayer_ID.Items.Add(GetItemDesc( mctMapLayer.ID, mctMapLayer.Name));
                        }
                        cmbLayer_ID.SelectedIndex = 0;
                    }
                    tpLayer.BackColor = m_DefaultEntityColor;
                    LoadLayerParams();
                    break;
                case "tpOverlayManager":
                    lstImageCalc.Visible = false;
                    lblImageCalc.Visible = false;
                    lstOverlayManager.Visible = false;
                    lblOverlayManager.Visible = false;
                    lstGridCoordinateSystem.Visible = true;
                    lblGridCoordSys.Visible = true;
                    clstChilds.Visible = false;
                    lblChild.Visible = false;
                    LoadGridCoordinateSystemListBox();
                    if (MainForm.MapLoaderDefinitionManager.OverlayManagers.OverlayManager.Count > 0)
                    {
                        cmbOverlayManager_ID.Items.Clear();
                        for (int i = 0; i < MainForm.MapLoaderDefinitionManager.OverlayManagers.OverlayManager.Count; i++)
                        {
                            MCTOverlayManager mctOverlayManager = MainForm.MapLoaderDefinitionManager.OverlayManagers.OverlayManager[i];
                            cmbOverlayManager_ID.Items.Add(GetItemDesc( mctOverlayManager.ID, mctOverlayManager.Name));
                        }
                        cmbOverlayManager_ID.SelectedIndex = 0;
                    }
                    tpOverlayManager.BackColor = m_DefaultEntityColor;
                    LoadOverlayManagerParams();
                    break;
                case "tpCoordinateSystem":
                    lstImageCalc.Visible = false;
                    lblImageCalc.Visible = false;
                    lstOverlayManager.Visible = false;
                    lblOverlayManager.Visible = false;
                    lstGridCoordinateSystem.Visible = false;
                    lblGridCoordSys.Visible = false;
                    clstChilds.Visible = false;
                    lblChild.Visible = false;
                    if (MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.CoordinateSystem.Count > 0)
                    {
                        cmbGridCoordinateSystem_ID.Items.Clear();
                        for (int i = 0; i < MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.CoordinateSystem.Count; i++)
                        {
                            MCTGridCoordinateSystem mctGridCoordinateSystem = MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.CoordinateSystem[i];
                            cmbGridCoordinateSystem_ID.Items.Add(GetItemDesc( mctGridCoordinateSystem.ID, mctGridCoordinateSystem.GridCoordinateSystemType.ToString()));
                        }
                        cmbGridCoordinateSystem_ID.SelectedIndex = 0;
                    }
                    tpCoordinateSystem.BackColor = m_DefaultEntityColor;
                    LoadCoordinateSystemParams();
                    break;
                case "tpImageCalc":
                    lstImageCalc.Visible = false;
                    lblImageCalc.Visible = false;
                    lstOverlayManager.Visible = false;
                    lblOverlayManager.Visible = false;
                    lstGridCoordinateSystem.Visible = true;
                    lblGridCoordSys.Visible = true;
                    LoadDTMMapLayerListBox();
                    LoadGridCoordinateSystemListBox();
                    if (MainForm.MapLoaderDefinitionManager.ImageCalcs.ImageCalc.Count > 0)
                    {
                        cmbImageCalc_ID.Items.Clear();
                        for (int i = 0; i < MainForm.MapLoaderDefinitionManager.ImageCalcs.ImageCalc.Count; i++)
                            cmbImageCalc_ID.Items.Add(GetItemDesc(MainForm.MapLoaderDefinitionManager.ImageCalcs.ImageCalc[i].ID, MainForm.MapLoaderDefinitionManager.ImageCalcs.ImageCalc[i].Name));
                        cmbImageCalc_ID.SelectedIndex = 0;
                    }
                    tpImageCalc.BackColor = m_DefaultEntityColor;
                    LoadImageCalcParams();
                    break;
            }
        }

        private void LoadDeviceParams()
        {
            if (MainForm.MapLoaderDefinitionManager != null && MainForm.MapLoaderDefinitionManager.MapDevices.Device.Count > 0)
            {
                MCTMapDevice m_MapDevice = MainForm.MapLoaderDefinitionManager.MapDevices.Device[0];
                ctrlDeviceParams1.SetDeviceParams(m_MapDevice.GetDeviceParams());
            }
        }

        private void LoadViewportCheckListBox()
        {
            clstChilds.Items.Clear();
            lChildCheckListBoxIDs.Clear();
            clstChilds.Tag = "Viewport";

            List<MCTMapViewport> lViewports = MainForm.MapLoaderDefinitionManager.Viewports.Viewport;
            foreach (MCTMapViewport viewport in lViewports)
            {
                clstChilds.Items.Add("(ID: " + viewport.ID.ToString() + ") " + viewport.Name);
                lChildCheckListBoxIDs.Add(viewport.ID);
            } 
        }

        private void LoadTerrainCheckListBox()
        {
            clstChilds.Items.Clear();
            lChildCheckListBoxIDs.Clear();
            clstChilds.Tag = "Terrain";

            List<MCTMapTerrain> lTerrains = MainForm.MapLoaderDefinitionManager.Terrains.Terrain;
            foreach (MCTMapTerrain terrain in lTerrains)
            {
                clstChilds.Items.Add("(ID: " + terrain.ID.ToString() + ") " + terrain.Name);
                lChildCheckListBoxIDs.Add(terrain.ID);
            }
        }

        private void LoadLayerListBox()
        {
            clstChilds.Items.Clear();
            lChildCheckListBoxIDs.Clear();
            clstChilds.Tag = "Layer";

            List<MCTMapLayer> lLayers = MainForm.MapLoaderDefinitionManager.Layers.Layer;
            foreach (MCTMapLayer layer in lLayers)
            {
                clstChilds.Items.Add("(ID: " + layer.ID.ToString() + ") " + layer.Name);
                lChildCheckListBoxIDs.Add(layer.ID);
            }
        }

        private void LoadDTMMapLayerListBox()
        {
            clstChilds.Visible = true;
            lblChild.Visible = true;

            clstChilds.Items.Clear();
            lChildCheckListBoxIDs.Clear();
            clstChilds.Tag = "DTMMapLayer";
            
            List<MCTMapLayer> lLayers = MainForm.MapLoaderDefinitionManager.Layers.Layer;
            foreach (MCTMapLayer layer in lLayers)
            {
                if (layer.LayerType == DNELayerType._ELT_NATIVE_DTM ||
                   layer.LayerType == DNELayerType._ELT_NATIVE_SERVER_DTM ||
                   layer.LayerType == DNELayerType._ELT_RAW_DTM)
                {
                    clstChilds.Items.Add("(ID: " + layer.ID.ToString() + ") " + layer.Name);
                    lChildCheckListBoxIDs.Add(layer.ID);
                }
            }
        }

        private void LoadOverlayManagerListBox()
        {
            lstOverlayManager.Items.Clear();
            lOMListBoxIDs.Clear();

            List<MCTOverlayManager> lOverlayManager = MainForm.MapLoaderDefinitionManager.OverlayManagers.OverlayManager;
            foreach (MCTOverlayManager OverlayManager in lOverlayManager)
            {
                lstOverlayManager.Items.Add("(ID: " + OverlayManager.ID.ToString() + ") " + OverlayManager.Name);
                lOMListBoxIDs.Add(OverlayManager.ID);
            }
        }

        private void LoadImageCalcListBox()
        {
            lstImageCalc.Items.Clear();
            lImageCalcListBoxIDs.Clear();

            List<MCTImageCalc> lImageCalc = MainForm.MapLoaderDefinitionManager.ImageCalcs.ImageCalc;
            foreach (MCTImageCalc imageCalc in lImageCalc)
            {
                lstImageCalc.Items.Add("(ID: " + imageCalc.ID.ToString() + ") " + imageCalc.Name);
                lImageCalcListBoxIDs.Add(imageCalc.ID);
            }
        }

        private void LoadGridCoordinateSystemListBox()
        {
            lstGridCoordinateSystem.Items.Clear();
            lstTargetGridCoordinateSystem.Items.Clear();
            lCoordSysListBoxIDs.Clear();

            List<MCTGridCoordinateSystem> lGridCoordinateSystem = MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.CoordinateSystem;
            foreach (MCTGridCoordinateSystem GridCoordinateSystem in lGridCoordinateSystem)
            {
                string str = "(ID: " + GridCoordinateSystem.ID.ToString() + ") " + GridCoordinateSystem.GridCoordinateSystemType.ToString();
                if (GridCoordinateSystem.GridCoordinateSystemType != DNEGridCoordSystemType._EGCS_GENERIC_GRID)
                    str += " / " + GridCoordinateSystem.DatumType.ToString();
                else
                {
                    if(GridCoordinateSystem.IsSRID && GridCoordinateSystem.AStrCreateParams !=null && GridCoordinateSystem.AStrCreateParams.Length > 0)
                        str += " / " + GridCoordinateSystem.AStrCreateParams[0];
                }
                lstGridCoordinateSystem.Items.Add(str);
                lstTargetGridCoordinateSystem.Items.Add(str);

                lCoordSysListBoxIDs.Add(GridCoordinateSystem.ID);
            }
        }

        private void cmbScheme_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_IsInSave)
            {
                int schemeId = (int)cmbScheme_ID.SelectedIndex;
                if (schemeId <= MainForm.MapLoaderDefinitionManager.Schemas.Schema.Count)
                {
                    btnScheme_New.Enabled = true;
                    // btnSaveSchema.Enabled = false;
                    cmbScheme_ID.Enabled = true;

                    tpScheme.BackColor = m_DefaultEntityColor;
                }
                else
                {
                    cmbScheme_ID.Enabled = false;
                    btnScheme_New.Enabled = false;
                    //btnSaveSchema.Enabled = true;
                }

                LoadSchemaParams();
            }
        }

        private void btnScheme_New_Click(object sender, EventArgs e)
        {
            int count = MainForm.MapLoaderDefinitionManager.Schemas.Schema.Count;
            int newId = count + 1;
            cmbScheme_ID.Items.Add(newId);
            cmbScheme_ID.SelectedIndex = count;
            tpScheme.BackColor = m_NewEntityColor;
        }

        private void btnRemoveScheme_Click(object sender, EventArgs e)
        {
           /* int schemeId = (int)cmbScheme_ID.SelectedItem;
            cmbScheme_ID.Items.Remove(cmbScheme_ID.SelectedItem);

            if (currSchema != null)
            {
                MainForm.MapLoaderDefinitionManager.Schemas.Schema.Remove(currSchema);
                currSchema = null;
            }
            cmbScheme_ID.sec*/
        }

        private void cmbViewport_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_IsInSave)
            {
                int viewportId = cmbViewport_ID.SelectedIndex;
                if (viewportId <= MainForm.MapLoaderDefinitionManager.Viewports.Viewport.Count)
                {
                    cmbViewport_ID.Enabled = true;
                    //  btnSaveViewport.Enabled = false;
                    btnViewport_New.Enabled = true;
                }
                else
                {
                    cmbViewport_ID.Enabled = false;
                    //  btnSaveViewport.Enabled = true;
                    btnViewport_New.Enabled = false;
                }
                LoadViewportParams();
            }
        }

        private void btnViewport_New_Click(object sender, EventArgs e)
        {
            int count = MainForm.MapLoaderDefinitionManager.Viewports.Viewport.Count;
            int viewportId = count + 1;
            cmbViewport_ID.Items.Add(viewportId);
            cmbViewport_ID.SelectedIndex = count;

            tpViewport.BackColor = m_NewEntityColor;
        }

        private void cmbTerrain_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_IsInSave)
            {
                int terrainId = cmbTerrain_ID.SelectedIndex;
                if (terrainId <= MainForm.MapLoaderDefinitionManager.Terrains.Terrain.Count)
                {
                    cmbTerrain_ID.Enabled = true;
                    // btnSaveTerrain.Enabled = false;
                    btnTerrain_New.Enabled = true;
                }
                else
                {
                    cmbTerrain_ID.Enabled = false;
                    //  btnSaveTerrain.Enabled = true;
                    btnTerrain_New.Enabled = false;
                }
                LoadTerrainParams();
            }
        }

        private void btnTerrain_New_Click(object sender, EventArgs e)
        {
            int count = MainForm.MapLoaderDefinitionManager.Terrains.Terrain.Count;
            int terrainId = count + 1;
            cmbTerrain_ID.Items.Add(terrainId.ToString());
            cmbTerrain_ID.SelectedIndex = count;
            tpTerrain.BackColor = m_NewEntityColor;
        }
        bool m_IsInSave = false;

        private void cmbLayer_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_IsInSave)
            {
                int layerId = cmbLayer_ID.SelectedIndex;
                if (layerId <= MainForm.MapLoaderDefinitionManager.Layers.Layer.Count)
                {
                    cmbLayer_ID.Enabled = true;
                    // btnSaveLayer.Enabled = false;
                    btnLayer_New.Enabled = true;
                }
                else
                {
                    cmbLayer_ID.Enabled = false;
                    //  btnSaveLayer.Enabled = true;
                    btnLayer_New.Enabled = false;
                }

                LoadLayerParams();
            }
        }

        private void btnLayer_New_Click(object sender, EventArgs e)
        {
            int count = MainForm.MapLoaderDefinitionManager.Layers.Layer.Count;
            int layerID = count + 1;
            cmbLayer_ID.Items.Add(layerID.ToString());
            cmbLayer_ID.SelectedIndex = count;
            tpLayer.BackColor = m_NewEntityColor;
        }

        
        private void cmbOverlayManager_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_IsInSave)
            {
                int OverlayManagerId = cmbOverlayManager_ID.SelectedIndex;
                if (OverlayManagerId <= MainForm.MapLoaderDefinitionManager.OverlayManagers.OverlayManager.Count)
                {
                    cmbOverlayManager_ID.Enabled = true;
                    //   btnSaveOverlayManager.Enabled = false;
                    btnOverlayManager_New.Enabled = true;
                }
                else
                {
                    cmbOverlayManager_ID.Enabled = false;
                    //   btnSaveOverlayManager.Enabled = true;
                    btnOverlayManager_New.Enabled = false;
                }
                LoadOverlayManagerParams();
            }
        }

        private void btnOverlayManager_New_Click(object sender, EventArgs e)
        {
            int count = MainForm.MapLoaderDefinitionManager.OverlayManagers.OverlayManager.Count;
            int OverlayManagerId = count + 1;
            cmbOverlayManager_ID.Items.Add(OverlayManagerId.ToString());
            cmbOverlayManager_ID.SelectedIndex = count;
            tpOverlayManager.BackColor = m_NewEntityColor;
        }

        private void cmbGridCoordinateSystem_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_IsInSave)
            {
                int GridCoordinateSystemId = cmbGridCoordinateSystem_ID.SelectedIndex;
                if (GridCoordinateSystemId <= MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.CoordinateSystem.Count)
                {
                    cmbGridCoordinateSystem_ID.Enabled = true;
                    // btnSaveGridCoordinateSystem.Enabled = false;
                    btnGridCoordinateSystem_New.Enabled = true;
                }
                else
                {
                    cmbGridCoordinateSystem_ID.Enabled = false;
                    //btnSaveGridCoordinateSystem.Enabled = true;
                    btnGridCoordinateSystem_New.Enabled = false;
                }
                LoadCoordinateSystemParams();
            }
        }

        private void btnGridCoordinateSystem_New_Click(object sender, EventArgs e)
        {
            int count = MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.CoordinateSystem.Count;
            int gridCoordinateSystemID = count + 1;
            cmbGridCoordinateSystem_ID.Items.Add(gridCoordinateSystemID.ToString());
            cmbGridCoordinateSystem_ID.SelectedIndex = count;
            tpCoordinateSystem.BackColor = m_NewEntityColor;
        }

        private void cmbImageCalc_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_IsInSave)
            {
                int ImageCalcId = (int)cmbImageCalc_ID.SelectedIndex;
                if (ImageCalcId <= MainForm.MapLoaderDefinitionManager.ImageCalcs.ImageCalc.Count)
                {
                    cmbImageCalc_ID.Enabled = true;
                    // btnSaveImageCalc.Enabled = false;
                    btnImageCalc_New.Enabled = true;
                }
                else
                {
                    cmbImageCalc_ID.Enabled = false;
                    // btnSaveImageCalc.Enabled = true;
                    btnImageCalc_New.Enabled = false;
                }
                LoadImageCalcParams();
            }
        }

        private void btnImageCalc_New_Click(object sender, EventArgs e)
        {
            int ImageCalcId = MainForm.MapLoaderDefinitionManager.ImageCalcs.ImageCalc.Count + 1;
            cmbImageCalc_ID.Items.Add(ImageCalcId);
            cmbImageCalc_ID.SelectedItem = ImageCalcId;
            tpImageCalc.BackColor = m_NewEntityColor;

            for (int i = 0; i < clstChilds.Items.Count; i++)
                clstChilds.SetItemChecked(i, false);

            label19.Visible = ctrlImageCalc_DtmMapLayerPath.Visible = false;
        }
        MCTMapSchema currSchema;

        private void LoadSchemaParams()
        {
            if (MainForm.MapLoaderDefinitionManager.Schemas.Schema.Count > 0 &&
                cmbScheme_ID.SelectedIndex >= 0 &&
                cmbScheme_ID.SelectedIndex < MainForm.MapLoaderDefinitionManager.Schemas.Schema.Count)
            {
                int schemeId = MainForm.MapLoaderDefinitionManager.Schemas.Schema[cmbScheme_ID.SelectedIndex].ID;
                currSchema = MainForm.MapLoaderDefinitionManager.Schemas.GetSchema(schemeId);
                for (int i = 0; i < clstChilds.Items.Count; i++)
                    clstChilds.SetItemChecked(i, false);

                List<int> lViewport = currSchema.ViewportsIDs;
                foreach (int viewport in lViewport)
                    clstChilds.SetItemChecked(lChildCheckListBoxIDs.IndexOf(viewport), true);

                txtScheme_Area.Text = currSchema.Area;
                txtScheme_MapType.Text = currSchema.MapType;
                txtScheme_Comment.Text = currSchema.Comments;

                if (currSchema.OverlayManagerID != -1)
                    lstOverlayManager.SetSelected(lOMListBoxIDs.IndexOf(currSchema.OverlayManagerID), true);
            }
            else
            {
                currSchema = null;
                for (int i = 0; i < clstChilds.Items.Count; i++)
                    clstChilds.SetItemChecked(i, false);
                
                txtScheme_Area.Text = "";
                txtScheme_MapType.Text = "";
                txtScheme_Comment.Text = "";
                lstOverlayManager.ClearSelected();
            }         
        }

        private void LoadViewportParams()
        {
            if (MainForm.MapLoaderDefinitionManager.Viewports.Viewport.Count > 0 &&
                cmbViewport_ID.SelectedIndex >= 0 &&
                cmbViewport_ID.SelectedIndex < MainForm.MapLoaderDefinitionManager.Viewports.Viewport.Count)
            {
                int viewportId = MainForm.MapLoaderDefinitionManager.Viewports.Viewport[cmbViewport_ID.SelectedIndex].ID;
                MCTMapViewport viewport = MainForm.MapLoaderDefinitionManager.Viewports.GetViewport(viewportId);

                txtViewport_Name.Text = viewport.Name;
                cmbViewport_MapType.Text = viewport.MapType.ToString();
                ctrlViewport_CameraPosition.SetVector3D( viewport.CameraPosition);
                chxViewport_ShowGeoInMetricProportion.Checked = viewport.ShowGeoInMetricProportion;
                cmbViewport_DtmUsageAndPrecision.Text = viewport.DtmUsageAndPrecision;
                ntxViewport_TerrainObjectBestResolution.SetFloat(viewport.TerrainObjectBestResolution);

                for (int i = 0; i < clstChilds.Items.Count; i++)
                    clstChilds.SetItemChecked(i, false);

                List<int> lTerrainsIDs = viewport.TerrainsIDs;
                foreach (int terrainID in lTerrainsIDs)
                    clstChilds.SetItemChecked(lChildCheckListBoxIDs.IndexOf(terrainID), true);

                lstGridCoordinateSystem.SetSelected(lCoordSysListBoxIDs.IndexOf(viewport.CoordSysID), true);

                if (lImageCalcListBoxIDs.Count > 0 )
                {
                    if (viewport.ImageCalcID != -1)
                        lstImageCalc.SetSelected(lImageCalcListBoxIDs.IndexOf(viewport.ImageCalcID), true);
                    else
                    {
                        for (int i = 0; i < lstImageCalc.Items.Count; i++)
                        {
                            lstImageCalc.SetSelected(i, false);
                        }
                    }
                }
            }
            else
            {
                txtViewport_Name.Text = "Viewport";
                cmbViewport_MapType.Text = DNEMapType._EMT_2D.ToString();
                ctrlViewport_CameraPosition.SetVector3D( new DNSMcVector3D(0, 0, 0));
                chxViewport_ShowGeoInMetricProportion.Checked = true;
                cmbViewport_DtmUsageAndPrecision.Text = DNEQueryPrecision._EQP_DEFAULT.ToString();
                ntxViewport_TerrainObjectBestResolution.SetFloat(0.0125f);
                
                for (int i = 0; i < clstChilds.Items.Count; i++)
                    clstChilds.SetItemChecked(i, false);

                lstImageCalc.ClearSelected();
            }
        }

        private void LoadTerrainParams()
        {
            if (MainForm.MapLoaderDefinitionManager.Terrains.Terrain.Count > 0 &&
                 cmbTerrain_ID.SelectedIndex >= 0 &&
                 cmbTerrain_ID.SelectedIndex < MainForm.MapLoaderDefinitionManager.Terrains.Terrain.Count)
            {
                int terrainId = MainForm.MapLoaderDefinitionManager.Terrains.Terrain[cmbTerrain_ID.SelectedIndex].ID;
                MCTMapTerrain terrain = MainForm.MapLoaderDefinitionManager.Terrains.GetTerrain(terrainId);

                txtTerrain_Name.Text = terrain.Name;

                for (int i = 0; i < clstChilds.Items.Count; i++)
                    clstChilds.SetItemChecked(i, false);

                List<int> lLayersIDs = terrain.LayersIDs;
                foreach (int layerID in lLayersIDs)
                    clstChilds.SetItemChecked(lChildCheckListBoxIDs.IndexOf(layerID), true);

                lstGridCoordinateSystem.SetSelected(lCoordSysListBoxIDs.IndexOf(terrain.CoordSysID), true);
            }
            else
            {
                txtTerrain_Name.Text = "Terrain";

                for (int i = 0; i < clstChilds.Items.Count; i++)
                    clstChilds.SetItemChecked(i, false);

                lstGridCoordinateSystem.ClearSelected();
            }
        }

        private void SetRawParams(MCTMapLayer layer)
        {
            IDNMcGridCoordinateSystem mcGridCoordinateSystem = null;
           /* MCTGridCoordinateSystem mctGridCoordinateSystem = MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.GetCoordSys(layer.CoordSysID);
            if (mctGridCoordinateSystem != null)
                mcGridCoordinateSystem = mctGridCoordinateSystem.GetGridCoordSys();*/

            DNSRawParams rawParams = layer.GetRawParamsFromLayer(mcGridCoordinateSystem);
            ucLayerParams1.SetRawParams(rawParams);
        }

        private void LoadLayerParams()
        {
            if (MainForm.MapLoaderDefinitionManager.Layers.Layer.Count > 0 &&
                 cmbLayer_ID.SelectedIndex >= 0 &&
                 cmbLayer_ID.SelectedIndex < MainForm.MapLoaderDefinitionManager.Layers.Layer.Count)
            {
                int layerId = MainForm.MapLoaderDefinitionManager.Layers.Layer[cmbLayer_ID.SelectedIndex].ID;
                MCTMapLayer layer = MainForm.MapLoaderDefinitionManager.Layers.GetLayer(layerId);

                txtLayer_Name.Text = layer.Name;
                ucLayerParams1.SetMapLayerType(layer.LayerType.ToString().Replace("_ELT_", ""));
                ucLayerParams1.SetLayerFileName(layer.Path);
                ucLayerParams1.SetLocalCacheLayerParams(layer.LocalCacheLayerParams);

                ShowGridCoordinateSystem(true);

                bool isShowTargetGridCoordinateSystem = false;
                switch (layer.LayerType)
                {
                    case DNELayerType._ELT_RAW_VECTOR:
                    case DNELayerType._ELT_RAW_VECTOR_3D_EXTRUSION:
                        isShowTargetGridCoordinateSystem = true;
                        break;
                }
                lstTargetGridCoordinateSystem.Visible = isShowTargetGridCoordinateSystem;
                lblTargetGridCoordinateSystem.Visible = isShowTargetGridCoordinateSystem;
                if (isShowTargetGridCoordinateSystem)
                    lblGridCoordSys.Text = "Source Grid Coordinate System";
                else
                    lblGridCoordSys.Text = "Grid Coordinate System";

                if (layer.CoordSysID > 0)
                    lstGridCoordinateSystem.SetSelected(lCoordSysListBoxIDs.IndexOf(layer.CoordSysID), true);

                switch (layer.LayerType)
                {
                    case DNELayerType._ELT_NATIVE_DTM:
                        ucLayerParams1.SetNumLevelsToIgnore(layer.NumLevelsToIgnore);
                        break;
                    case DNELayerType._ELT_NATIVE_MATERIAL:
                        ucLayerParams1.SetThereAreMissingFiles(layer.ThereAreMissingFiles);
                        break;
                    case DNELayerType._ELT_NATIVE_TRAVERSABILITY:
                        ucLayerParams1.SetThereAreMissingFiles(layer.ThereAreMissingFiles);
                        break;
                    case DNELayerType._ELT_NATIVE_HEAT_MAP:
                        ucLayerParams1.SetFirstLowerQualityLevel(layer.FirstLowerQualityLevel);
                        ucLayerParams1.SetThereAreMissingFiles(layer.ThereAreMissingFiles);
                        ucLayerParams1.SetNumLevelsToIgnore(layer.NumLevelsToIgnore);
                        ucLayerParams1.SetEnhanceBorderOverlap2(layer.EnhanceBorderOverlap);
                        break;
                    case DNELayerType._ELT_NATIVE_RASTER:
                        ucLayerParams1.SetFirstLowerQualityLevel(layer.FirstLowerQualityLevel);
                        ucLayerParams1.SetThereAreMissingFiles(layer.ThereAreMissingFiles);
                        ucLayerParams1.SetNumLevelsToIgnore(layer.NumLevelsToIgnore);
                        ucLayerParams1.SetEnhanceBorderOverlap2(layer.EnhanceBorderOverlap);
                        break;
                    case DNELayerType._ELT_NATIVE_3D_MODEL:
                        ucLayerParams1.SetNumLevelsToIgnore(layer.NumLevelsToIgnore);
                        break;
                    case DNELayerType._ELT_NATIVE_VECTOR_3D_EXTRUSION:
                        ucLayerParams1.SetNumLevelsToIgnore(layer.NumLevelsToIgnore);
                        ucLayerParams1.SetExtrusionHeightMaxAddition(layer.ExtrusionHeightMaxAddition);
                        break;
                    case DNELayerType._ELT_NATIVE_VECTOR:
                    case DNELayerType._ELT_NATIVE_SERVER_RASTER:
                    case DNELayerType._ELT_NATIVE_SERVER_DTM:
                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR:
                    case DNELayerType._ELT_NATIVE_SERVER_3D_MODEL:
                    case DNELayerType._ELT_NATIVE_SERVER_TRAVERSABILITY:
                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR_3D_EXTRUSION:
                        break;
                    case DNELayerType._ELT_RAW_RASTER:
                        SetRawParams(layer);
                        ucLayerParams1.SetImageCoordSys(layer.RasterImageCoordinateSystem);
                        break;
                    case DNELayerType._ELT_RAW_DTM:
                        SetRawParams(layer);
                        break;
                    case DNELayerType._ELT_RAW_VECTOR:
                        IDNMcGridCoordinateSystem mcSourceGridCoordinateSystem = null;

                        if (layer.SourceCoordinateSystemID > 0)
                            lstGridCoordinateSystem.SetSelected(lCoordSysListBoxIDs.IndexOf(layer.SourceCoordinateSystemID), true);
                        if (layer.TargetCoordinateSystemID > 0)
                            lstTargetGridCoordinateSystem.SetSelected(lCoordSysListBoxIDs.IndexOf(layer.TargetCoordinateSystemID), true);

                        DNSRawVectorParams rawVectorParams = layer.GetRawVectorParamsFromLayer(mcSourceGridCoordinateSystem);
                        
                        ucLayerParams1.SetRawVectorParams(rawVectorParams);
                        ucLayerParams1.SetTilingScheme(layer.TilingScheme);
                        ucLayerParams1.HideGridCoordinateSystemParams();

                        break;
                    case DNELayerType._ELT_RAW_VECTOR_3D_EXTRUSION:

                        IDNMcGridCoordinateSystem mcTargetGridCoordinateSystem = null;
                        mcSourceGridCoordinateSystem = null;

                        DNSRawVector3DExtrusionParams mcExtrusionParams = layer.GetRawVector3DExtrusionParams(mcSourceGridCoordinateSystem, mcTargetGridCoordinateSystem);

                        if (layer.SourceCoordinateSystemID > 0)
                            lstGridCoordinateSystem.SetSelected(lCoordSysListBoxIDs.IndexOf(layer.SourceCoordinateSystemID), true);
                        if (layer.TargetCoordinateSystemID > 0)
                            lstTargetGridCoordinateSystem.SetSelected(lCoordSysListBoxIDs.IndexOf(layer.TargetCoordinateSystemID), true);

                        ucLayerParams1.SetRawVector3DExtrusionParams(mcExtrusionParams, layer.ExtrusionHeightMaxAddition);
                        ucLayerParams1.HideGridCoordinateSystemParams();
                        ucLayerParams1.SetExtrusionIndexingDataDirectoryFromLayer(layer.RSOIndexingDataDir, layer.RSONonDefaultIndexingDataDir);
                        break;
                    case DNELayerType._ELT_RAW_3D_MODEL:

                        ucLayerParams1.SetNumLevelsToIgnore(layer.NumLevelsToIgnore);

                        ucLayerParams1.SetRaw3DModelParams(layer.GetRaw3DModelParams());

                        break;
                    case DNELayerType._ELT_WEB_SERVICE_DTM:
                    case DNELayerType._ELT_WEB_SERVICE_RASTER:
                        //currLayer.WSWebMapServiceType = webMapServiceRasterType;
                        if (layer.SourceCoordinateSystemID > 0)
                            lstGridCoordinateSystem.SetSelected(lCoordSysListBoxIDs.IndexOf(layer.CoordSysID), true);
                        switch (layer.WSWebMapServiceType)
                        {
                            case DNEWebMapServiceType._EWMS_WMS:
                                ucLayerParams1.SetWMSParams(layer.GetWMSParams(null));
                                break;
                            case DNEWebMapServiceType._EWMS_WMTS:
                                ucLayerParams1.SetWMTSParams(layer.GetWMTSParams(null));
                                break;
                            case DNEWebMapServiceType._EWMS_WCS:
                                ucLayerParams1.SetWCSParams(layer.GetWCSParams(null));
                                break;
                        }
                        break;
                }
            }
            else
            {
                txtLayer_Name.Text = "Layer";
                lstGridCoordinateSystem.ClearSelected();
            }
        }

        private void LoadOverlayManagerParams()
        {
		    if (MainForm.MapLoaderDefinitionManager.OverlayManagers.OverlayManager.Count > 0 &&
                  cmbOverlayManager_ID.SelectedIndex >= 0 &&
                  cmbOverlayManager_ID.SelectedIndex < MainForm.MapLoaderDefinitionManager.OverlayManagers.OverlayManager.Count)
            {
                int overlayManagerId = MainForm.MapLoaderDefinitionManager.OverlayManagers.OverlayManager[cmbOverlayManager_ID.SelectedIndex].ID;

                MCTOverlayManager overlayManager = MainForm.MapLoaderDefinitionManager.OverlayManagers.GetOverlayManager(overlayManagerId);
                
                lstGridCoordinateSystem.SetSelected(lCoordSysListBoxIDs.IndexOf(overlayManager.CoordSysID), true);
                txtOverlayManager_Name.Text = overlayManager.Name;
            }
            else
            {
                lstGridCoordinateSystem.ClearSelected();
                txtOverlayManager_Name.Text = "Overlay Manager";
            }
        }

        private void LoadCoordinateSystemParams()
        {
            if (MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.CoordinateSystem.Count > 0 &&
                cmbGridCoordinateSystem_ID.SelectedIndex >= 0 &&
                cmbGridCoordinateSystem_ID.SelectedIndex < MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.CoordinateSystem.Count)
		    {
                int gridCoordinateSystem_ID = MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.CoordinateSystem[cmbGridCoordinateSystem_ID.SelectedIndex].ID;

                MCTGridCoordinateSystem gridCoordSys = MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.GetCoordSys(gridCoordinateSystem_ID);

                ctrlNewGridCoordinateSystem1.SetGridCoordSystemType(gridCoordSys.GridCoordinateSystemType);
                ctrlNewGridCoordinateSystem1.SetDatumType(gridCoordSys.DatumType);
                ctrlNewGridCoordinateSystem1.SetZone(gridCoordSys.Zone);
                ctrlNewGridCoordinateSystem1.SetDatumParams(gridCoordSys.DatumParams);
                ctrlNewGridCoordinateSystem1.SetTMGridParams(gridCoordSys.TMGridParams);
                ctrlNewGridCoordinateSystem1.SetGridGenericParams(gridCoordSys.AStrCreateParams, gridCoordSys.IsSRID);
/*
                cmbCoordinateSystem_Type.Text = gridCoordSys.GridCoordinateSystemType.ToString();
                cmbCoordinateSystem_Datum.Text = gridCoordSys.DatumType.ToString();
                ntxCoordinateSystem_Zone.SetInt(gridCoordSys.Zone);
*/
            }
            else
            {
                ctrlNewGridCoordinateSystem1.SetGridCoordSystemType(DNEGridCoordSystemType._EGCS_UTM);
                ctrlNewGridCoordinateSystem1.SetDatumType(DNEDatumType._EDT_ED50_ISRAEL);
                ctrlNewGridCoordinateSystem1.SetZone(36);
            }
        }

        private void LoadImageCalcParams()
        {
            DNSCameraParams cameraParams = new DNSCameraParams();
            cameraParams.dCameraOpeningAngleX = 45;
            cameraParams.dPixelRatio = 1;

            if (MainForm.MapLoaderDefinitionManager.ImageCalcs.ImageCalc.Count > 0 &&
                cmbImageCalc_ID.SelectedIndex >= 0 &&
                cmbImageCalc_ID.SelectedIndex < MainForm.MapLoaderDefinitionManager.ImageCalcs.ImageCalc.Count)
            {

                int imageCalcId = MainForm.MapLoaderDefinitionManager.ImageCalcs.ImageCalc[cmbImageCalc_ID.SelectedIndex].ID;
                MCTImageCalc imageCalc = MainForm.MapLoaderDefinitionManager.ImageCalcs.GetImageCalc(imageCalcId);

                txtImageCalc_Name.Text = imageCalc.Name;
                cmbImageCalc_Type.Text = imageCalc.ImageType;

                cameraParams.CameraPosition = imageCalc.Location;
                cameraParams.dCameraRoll = imageCalc.Roll;
                cameraParams.dCameraPitch = imageCalc.Pitch;
                cameraParams.dCameraYaw = imageCalc.Yaw;
                cameraParams.dCameraOpeningAngleX = imageCalc.OpeningAngleX;
                cameraParams.dPixelRatio = imageCalc.PixelRatio;
                cameraParams.nPixelesNumX = imageCalc.PixelsNumX;
                cameraParams.nPixelesNumY = imageCalc.PixelsNumY;
                cameraParams.dOffsetCenterPixelX = imageCalc.OffsetCenterPixelX;
                cameraParams.dOffsetCenterPixelY = imageCalc.OffsetCenterPixelY;

                ctrlImageCalc_ctrlCameraParams1.SetCameraParams(cameraParams);
                if (imageCalc.DTMMapLayerID > 0 && lChildCheckListBoxIDs.IndexOf(imageCalc.DTMMapLayerID) >= 0)
                    clstChilds.SetItemChecked(lChildCheckListBoxIDs.IndexOf(imageCalc.DTMMapLayerID), true);

                label19.Visible = ctrlImageCalc_DtmMapLayerPath.Visible = imageCalc.DtmMapLayerPath != "";
                ctrlImageCalc_DtmMapLayerPath.FileName = imageCalc.DtmMapLayerPath;
                
                chxImageCalc_IsFileName.Checked = imageCalc.IsFileName;
                ctrlImageCalc_XmlPath.FileName = imageCalc.XmlPath;
                if(imageCalc.CoordSysID >= 0 && lCoordSysListBoxIDs.IndexOf(imageCalc.CoordSysID) >= 0)
                    lstGridCoordinateSystem.SetSelected(lCoordSysListBoxIDs.IndexOf(imageCalc.CoordSysID), true);
            }
            else
            {
                txtImageCalc_Name.Text = "ImageCalc";
                cmbImageCalc_Type.Text = DNEImageType._EIT_FRAME.ToString();
                ctrlImageCalc_ctrlCameraParams1.SetCameraParams(cameraParams);
                ctrlImageCalc_DtmMapLayerPath.FileName = "";
                chxImageCalc_IsFileName.Checked = true;
                ctrlImageCalc_XmlPath.FileName = "";
                lstGridCoordinateSystem.ClearSelected();
            }
        }

        private void btnSaveDevice_Click(object sender, EventArgs e)
        {
            MCTMapDevice m_MapDevice;
            if (MainForm.MapLoaderDefinitionManager.MapDevices.Device.Count >= 1)
                m_MapDevice = MainForm.MapLoaderDefinitionManager.MapDevices.Device[0];
            else
            {
                m_MapDevice = new MCTMapDevice();
                MainForm.MapLoaderDefinitionManager.MapDevices.Device.Add(m_MapDevice);
            }
            m_MapDevice.SetDeviceParams(ctrlDeviceParams1.GetDeviceParams());

            m_IsDataChanged = true;
       }

        private void btnSaveSchema_Click(object sender, EventArgs e)
        {
            btnScheme_New.Enabled = true;
            cmbScheme_ID.Enabled = true;
            tpScheme.BackColor = m_DefaultEntityColor;

            int schemeId = cmbScheme_ID.SelectedIndex + 1;

            MCTMapSchema currSchema = MainForm.MapLoaderDefinitionManager.Schemas.GetSchema(schemeId);
            if (currSchema == null)
            {
                currSchema = new MCTMapSchema();
                MainForm.MapLoaderDefinitionManager.Schemas.Schema.Add(currSchema);
            }

            currSchema.ID = schemeId;
            currSchema.Area = txtScheme_Area.Text;
            currSchema.MapType = txtScheme_MapType.Text;
            currSchema.Comments = txtScheme_Comment.Text;

            currSchema.ViewportsIDs.Clear();
            for (int i = 0; i < clstChilds.CheckedIndices.Count; i++)
                currSchema.ViewportsIDs.Add(lChildCheckListBoxIDs[clstChilds.CheckedIndices[i]]);

            if (lstOverlayManager.SelectedIndex != -1)
                currSchema.OverlayManagerID = lOMListBoxIDs[lstOverlayManager.SelectedIndex];

            cmbScheme_ID.Enabled = true;

            m_IsInSave = true;
            cmbScheme_ID.Items[cmbScheme_ID.SelectedIndex] = GetItemDesc(schemeId, string.Format("{0} {1} ({2})", currSchema.Area, currSchema.MapType, currSchema.Comments));
            m_IsInSave = false;

            m_IsDataChanged = true;

        }

        private void btnSaveViewport_Click(object sender, EventArgs e)
        {
            if (lstGridCoordinateSystem.SelectedIndex != -1)
            {
                int viewportId = cmbViewport_ID.SelectedIndex + 1;
                btnViewport_New.Enabled = true;
                cmbViewport_ID.Enabled = true;
                tpViewport.BackColor = m_DefaultEntityColor;

                MCTMapViewport currViewport = MainForm.MapLoaderDefinitionManager.Viewports.GetViewport(viewportId);
                if (currViewport == null)
                {
                    currViewport = new MCTMapViewport();
                    MainForm.MapLoaderDefinitionManager.Viewports.Viewport.Add(currViewport);
                }

                currViewport.ID = viewportId;
                currViewport.Name = txtViewport_Name.Text;
                currViewport.MapType = (DNEMapType)Enum.Parse(typeof(DNEMapType), cmbViewport_MapType.Text);
                currViewport.ShowGeoInMetricProportion = chxViewport_ShowGeoInMetricProportion.Checked;
                currViewport.DtmUsageAndPrecision = cmbViewport_DtmUsageAndPrecision.Text;
                currViewport.TerrainObjectBestResolution = ntxViewport_TerrainObjectBestResolution.GetFloat();

                currViewport.TerrainsIDs.Clear();
                for (int i = 0; i < clstChilds.CheckedIndices.Count; i++)
                    currViewport.TerrainsIDs.Add(lChildCheckListBoxIDs[clstChilds.CheckedIndices[i]]);

                currViewport.CameraPosition = ctrlViewport_CameraPosition.GetVector3D();
                currViewport.CoordSysID = lCoordSysListBoxIDs[lstGridCoordinateSystem.SelectedIndex];

                if (lstImageCalc.SelectedIndex != -1)
                    currViewport.ImageCalcID = lImageCalcListBoxIDs[lstImageCalc.SelectedIndex];
                else
                    currViewport.ImageCalcID = -1;

                cmbViewport_ID.Enabled = true;

                m_IsInSave = true;
                cmbViewport_ID.Items[cmbViewport_ID.SelectedIndex] = GetItemDesc(viewportId, txtViewport_Name.Text);
                m_IsInSave = false;

                m_IsDataChanged = true;
            }
            else
                MessageBox.Show("Please choose Grid Coordinate System from the list", "Undefined Grid Coordinate System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void btnSaveTerrain_Click(object sender, EventArgs e)
        {
            if (lstGridCoordinateSystem.SelectedIndex != -1)
            {
                btnTerrain_New.Enabled = true;
                cmbTerrain_ID.Enabled = true;
                tpTerrain.BackColor = m_DefaultEntityColor;
                int terrainId = cmbTerrain_ID.SelectedIndex + 1;
                MCTMapTerrain currTerrain = MainForm.MapLoaderDefinitionManager.Terrains.GetTerrain(terrainId);
                if (currTerrain == null)
                {
                    currTerrain = new MCTMapTerrain();
                    MainForm.MapLoaderDefinitionManager.Terrains.Terrain.Add(currTerrain);
                }

                currTerrain.LayersIDs.Clear();
                for (int i = 0; i < clstChilds.CheckedIndices.Count; i++)
                    currTerrain.LayersIDs.Add(lChildCheckListBoxIDs[clstChilds.CheckedIndices[i]]);

                currTerrain.ID = terrainId;
                currTerrain.Name = txtTerrain_Name.Text;
                currTerrain.CoordSysID = lCoordSysListBoxIDs[lstGridCoordinateSystem.SelectedIndex];

                cmbTerrain_ID.Enabled = true;

                m_IsInSave = true;
                cmbTerrain_ID.Items[cmbTerrain_ID.SelectedIndex] = GetItemDesc(terrainId, txtTerrain_Name.Text);
                m_IsInSave = false;

                m_IsDataChanged = true;

            }
            else
                MessageBox.Show("Please choose Grid Coordinate System from the list", "Undefined Grid Coordinate System", MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }

        private string GetItemDesc(int id, string desc)
        {
            return String.Format("{0} - {1}", id, desc);
        }



        private void btnSaveLayer_Click(object sender, EventArgs e)
        {
            if (ucLayerParams1.GetMapLayerType() != Manager_MCLayers.stringDelimiter)
            {
                DNELayerType layerType = (DNELayerType)Enum.Parse(typeof(DNELayerType), Manager_MCLayers.LayerTypePrefix + ucLayerParams1.GetMapLayerType());

                int layerId = cmbLayer_ID.SelectedIndex + 1;
                btnLayer_New.Enabled = true;
                cmbLayer_ID.Enabled = true;
                tpLayer.BackColor = m_DefaultEntityColor;

                MCTMapLayer currLayer = MainForm.MapLoaderDefinitionManager.Layers.GetLayer(layerId);
                if (currLayer == null)
                {
                    currLayer = new MCTMapLayer();
                    MainForm.MapLoaderDefinitionManager.Layers.Layer.Add(currLayer);
                }

                currLayer.ID = layerId;
                currLayer.Name = txtLayer_Name.Text;
                currLayer.LayerType = layerType;
                currLayer.Path = ucLayerParams1.GetLayerFileName();

                currLayer.LocalCacheLayerParams = ucLayerParams1.GetLocalCacheLayerParams();
                
                if(lstGridCoordinateSystem.SelectedIndex >= 0)
                    currLayer.CoordSysID = lCoordSysListBoxIDs[lstGridCoordinateSystem.SelectedIndex];

                int targetGridCoordId = -1, sourceGridCoordId = -1;

                switch (ucLayerParams1.GetMapLayerType())
                {
                    case "NATIVE_3D_MODEL":
                    case "NATIVE_DTM":
                        currLayer.NumLevelsToIgnore = ucLayerParams1.GetNumLevelsToIgnore();
                        break;
                    case "NATIVE_RASTER":
                        currLayer.FirstLowerQualityLevel = ucLayerParams1.GetFirstLowerQualityLevel();
                        currLayer.ThereAreMissingFiles = ucLayerParams1.GetThereAreMissingFiles();
                        currLayer.NumLevelsToIgnore = ucLayerParams1.GetNumLevelsToIgnore();
                        currLayer.EnhanceBorderOverlap = ucLayerParams1.GetEnhanceBorderOverlap2();
                        break;
                    case "NATIVE_VECTOR_3D_EXTRUSION":
                        currLayer.NumLevelsToIgnore = ucLayerParams1.GetNumLevelsToIgnore();
                        currLayer.ExtrusionHeightMaxAddition = ucLayerParams1.GetExtrusionHeightMaxAddition();
                        break;
                    case "NATIVE_HEAT_MAP":
                        currLayer.FirstLowerQualityLevel = ucLayerParams1.GetFirstLowerQualityLevel();
                        currLayer.ThereAreMissingFiles = ucLayerParams1.GetThereAreMissingFiles();
                        currLayer.NumLevelsToIgnore = ucLayerParams1.GetNumLevelsToIgnore();
                        currLayer.EnhanceBorderOverlap = ucLayerParams1.GetEnhanceBorderOverlap2();
                        break;
                    case "NATIVE_TRAVERSABILITY":
                    case "NATIVE_MATERIAL":
                        currLayer.ThereAreMissingFiles = ucLayerParams1.GetThereAreMissingFiles();
                        break;
                    case "NATIVE_VECTOR":
                    case "NATIVE_SERVER_DTM":
                    case "NATIVE_SERVER_RASTER":
                    case "NATIVE_SERVER_VECTOR":
                    case "NATIVE_SERVER_VECTOR_3D_EXTRUSION":
                    case "NATIVE_SERVER_3D_MODEL":
                    case "NATIVE_SERVER_TRAVERSABILITY":
                        break;
                    case "RAW_DTM":
                    case "RAW_RASTER":
                       /* if (!ucLayerParams1.IsUserSelectGridCoordinateSystem())
                        {
                            MessageBox.Show("No Grid Coordinate System was specified.\nYou have to choose one!\n");
                            return;
                        }*/
                        DNSRawParams dnParams = ucLayerParams1.GetSRawParams();
                        if (dnParams == null)
                        {
                            MessageBox.Show("Invalid Pyramid Resolution Values");
                            return;
                        }
                        currLayer.SetRawParams(dnParams, currLayer.CoordSysID /*lCoordSysListBoxIDs[lstGridCoordinateSystem.SelectedIndex]*/);
                        if (ucLayerParams1.GetMapLayerType() == "RAW_RASTER")
                            currLayer.RasterImageCoordinateSystem = ucLayerParams1.GetImageCoordSys();
                        break;
                    case "RAW_VECTOR":
                        DNSRawVectorParams rawVectorParams = ucLayerParams1.GetRawVectorParams();
                        if (lstGridCoordinateSystem.SelectedIndex >= 0)
                            sourceGridCoordId = lCoordSysListBoxIDs[lstGridCoordinateSystem.SelectedIndex];
                        if (lstTargetGridCoordinateSystem.SelectedIndex >= 0)
                            targetGridCoordId = lCoordSysListBoxIDs[lstTargetGridCoordinateSystem.SelectedIndex];
                        currLayer.SetRawVectorParamsToLayer(rawVectorParams, sourceGridCoordId, targetGridCoordId);
                        currLayer.TilingScheme = ucLayerParams1.GetTilingScheme();
                         break;

                    case "RAW_VECTOR_3D_EXTRUSION":
                        DNSRawVector3DExtrusionParams mExtrusionParams = ucLayerParams1.GetRawVector3DExtrusionParams();
                        currLayer.ExtrusionHeightMaxAddition = ucLayerParams1.GetExtrusionHeightMaxAddition();
                       
                        if (lstGridCoordinateSystem.SelectedIndex >= 0)
                            sourceGridCoordId = lCoordSysListBoxIDs[lstGridCoordinateSystem.SelectedIndex];
                        if (lstTargetGridCoordinateSystem.SelectedIndex >= 0)
                            targetGridCoordId = lCoordSysListBoxIDs[lstTargetGridCoordinateSystem.SelectedIndex];

                        currLayer.SetRawVector3DExtrusionParams(mExtrusionParams, sourceGridCoordId, targetGridCoordId);
                        break;
                    case "RAW_3D_MODEL":
                        currLayer.NumLevelsToIgnore = ucLayerParams1.GetNumLevelsToIgnore();
                        currLayer.SetRaw3DModelParams(ucLayerParams1.GetRaw3DModelParams());

                        break;
                    case "WEB_SERVICE_RASTER":
                    case "WEB_SERVICE_DTM":
                        if (!ucLayerParams1.IsUserSelectGridCoordinateSystem())
                        {
                            MessageBox.Show("No Grid Coordinate System was specified.\nYou have to choose one!\n");
                            return;
                        }
                        if (lstGridCoordinateSystem.SelectedIndex >= 0)
                            sourceGridCoordId = lCoordSysListBoxIDs[lstGridCoordinateSystem.SelectedIndex];
                        DNEWebMapServiceType wMSTypeSelectedLayer = ucLayerParams1.GetWMSTypeSelectedLayer();
                        switch (wMSTypeSelectedLayer)
                        {
                            case DNEWebMapServiceType._EWMS_WMS:
                                {
                                    DNSWMSParams mcSWMSParams = new DNSWMSParams();
                                    ucLayerParams1.GetWebMapServiceParams(mcSWMSParams, DNEWebMapServiceType._EWMS_WMS);
                                    currLayer.SetWMSParams(mcSWMSParams, sourceGridCoordId);
                                }
                                break;
                            case DNEWebMapServiceType._EWMS_WMTS:
                                {
                                    DNSWMTSParams mcSWMTSParams = new DNSWMTSParams();
                                    ucLayerParams1.GetWebMapServiceParams(mcSWMTSParams, DNEWebMapServiceType._EWMS_WMTS);
                                    currLayer.SetWMTSParams(mcSWMTSParams, sourceGridCoordId);
                                }
                                break;
                            case DNEWebMapServiceType._EWMS_WCS:
                                {
                                    DNSWCSParams mcSWCSParams = new DNSWCSParams();
                                    ucLayerParams1.GetWebMapServiceParams(mcSWCSParams, DNEWebMapServiceType._EWMS_WCS);
                                    currLayer.SetWCSParams(mcSWCSParams, sourceGridCoordId);
                                }
                                break;
                        }
                        currLayer.TilingScheme = ucLayerParams1.GetTilingScheme();
                        break;
                }

                m_IsInSave = true;
                cmbLayer_ID.Items[cmbLayer_ID.SelectedIndex] = GetItemDesc(layerId, txtLayer_Name.Text);
                m_IsInSave = false;

                m_IsDataChanged = true;

            }
            else
            {
                MessageBox.Show("Invalid layer type");
            }
        }

        private void btnSaveOverlayManager_Click(object sender, EventArgs e)
        {
            if (lstGridCoordinateSystem.SelectedIndex != -1 )
            {
                if (cmbOverlayManager_ID.SelectedIndex >= 0)
                {
                    int overlayManagerId = cmbOverlayManager_ID.SelectedIndex + 1;
                    btnOverlayManager_New.Enabled = true;
                    cmbOverlayManager_ID.Enabled = true;
                    tpOverlayManager.BackColor = m_DefaultEntityColor;

                    MCTOverlayManager currOverlayManager = MainForm.MapLoaderDefinitionManager.OverlayManagers.GetOverlayManager(overlayManagerId);
                    if (currOverlayManager == null)
                    {
                        currOverlayManager = new MCTOverlayManager();
                        MainForm.MapLoaderDefinitionManager.OverlayManagers.OverlayManager.Add(currOverlayManager);
                    }

                    currOverlayManager.ID = overlayManagerId;

                    currOverlayManager.CoordSysID = lCoordSysListBoxIDs[lstGridCoordinateSystem.SelectedIndex];
                    currOverlayManager.Name = txtOverlayManager_Name.Text;

                    m_IsInSave = true;
                    cmbOverlayManager_ID.Items[cmbOverlayManager_ID.SelectedIndex] = GetItemDesc( overlayManagerId, txtOverlayManager_Name.Text);
                    m_IsInSave = false;

                    m_IsDataChanged = true;
                }
            }
            else
                MessageBox.Show("Please choose Grid Coordinate System from the list", "Undefined Grid Coordinate System", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
            
        }

        private void btnSaveGridCoordinateSystem_Click(object sender, EventArgs e)
        {
            if (cmbGridCoordinateSystem_ID.SelectedIndex >= 0)
            {
                btnGridCoordinateSystem_New.Enabled = true;
                cmbGridCoordinateSystem_ID.Enabled = true;
                tpCoordinateSystem.BackColor = m_DefaultEntityColor;
                int gridCoordinateSystemId =  cmbGridCoordinateSystem_ID.SelectedIndex + 1;

                MCTGridCoordinateSystem currGridCoorSys = MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.GetCoordSys(gridCoordinateSystemId);
                if (currGridCoorSys == null)
                {
                    currGridCoorSys = new MCTGridCoordinateSystem();
                    MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.CoordinateSystem.Add(currGridCoorSys);
                }

                currGridCoorSys.ID = gridCoordinateSystemId;
                currGridCoorSys.GridCoordinateSystemType = ctrlNewGridCoordinateSystem1.GetGridCoordSystemType(); 
                currGridCoorSys.DatumType = ctrlNewGridCoordinateSystem1.GetDatumType();                          
                currGridCoorSys.Zone = ctrlNewGridCoordinateSystem1.GetZone();
                currGridCoorSys.DatumParams = ctrlNewGridCoordinateSystem1.GetDatumParams();
                currGridCoorSys.TMGridParams = ctrlNewGridCoordinateSystem1.GetTMGridParams();

                string[] pastrCreateParams;
                bool isSRID;
                ctrlNewGridCoordinateSystem1.GetGridGenericParams(out pastrCreateParams, out isSRID);
                currGridCoorSys.AStrCreateParams = pastrCreateParams;
                currGridCoorSys.IsSRID = isSRID;

                m_IsInSave = true;
                cmbGridCoordinateSystem_ID.Items[cmbGridCoordinateSystem_ID.SelectedIndex] = GetItemDesc(gridCoordinateSystemId, ctrlNewGridCoordinateSystem1.GetGridCoordSystemType().ToString());
                m_IsInSave = false;

                m_IsDataChanged = true;
            }
        }

        private void btnSaveImageCalc_Click(object sender, EventArgs e)
        {
            btnImageCalc_New.Enabled = true;
            cmbImageCalc_ID.Enabled = true;
            tpImageCalc.BackColor = m_DefaultEntityColor;
            if (cmbImageCalc_ID.SelectedIndex >= 0)
            {
                int ImageCalcId = cmbImageCalc_ID.SelectedIndex + 1;

                MCTImageCalc currImageCalc = MainForm.MapLoaderDefinitionManager.ImageCalcs.GetImageCalc(ImageCalcId);
                if (currImageCalc == null)
                {
                    currImageCalc = new MCTImageCalc();
                    MainForm.MapLoaderDefinitionManager.ImageCalcs.ImageCalc.Add(currImageCalc);
                }

                currImageCalc.ID = ImageCalcId;
                currImageCalc.Name = txtImageCalc_Name.Text;
                currImageCalc.ImageType = cmbImageCalc_Type.Text;

                DNSCameraParams cameraParams = ctrlImageCalc_ctrlCameraParams1.GetCameraParams();
                currImageCalc.Location = cameraParams.CameraPosition;
                currImageCalc.OpeningAngleX = cameraParams.dCameraOpeningAngleX; ;
                currImageCalc.Yaw = cameraParams.dCameraYaw; ;
                currImageCalc.Pitch = cameraParams.dCameraPitch;
                currImageCalc.Roll = cameraParams.dCameraRoll;
                currImageCalc.PixelRatio = cameraParams.dPixelRatio;
                currImageCalc.PixelsNumX = cameraParams.nPixelesNumX;
                currImageCalc.PixelsNumY = cameraParams.nPixelesNumY;
                currImageCalc.OffsetCenterPixelX = cameraParams.dOffsetCenterPixelX;
                currImageCalc.OffsetCenterPixelY = cameraParams.dOffsetCenterPixelY;

                currImageCalc.DtmMapLayerPath = ctrlImageCalc_DtmMapLayerPath.FileName;

                if (clstChilds.CheckedIndices.Count > 0)
                    currImageCalc.DTMMapLayerID = lChildCheckListBoxIDs[clstChilds.CheckedIndices[0]];

                currImageCalc.IsFileName = chxImageCalc_IsFileName.Checked;
                currImageCalc.XmlPath = ctrlImageCalc_XmlPath.FileName;

                if (lstGridCoordinateSystem.SelectedIndex >= 0)
                    currImageCalc.CoordSysID = lCoordSysListBoxIDs[lstGridCoordinateSystem.SelectedIndex];

                m_IsInSave = true;
                cmbImageCalc_ID.Items[cmbImageCalc_ID.SelectedIndex] = GetItemDesc(ImageCalcId, txtImageCalc_Name.Text);
                m_IsInSave = false;

                m_IsDataChanged = true;
            }
        }

        private void tpSchemasDefine_Leave(object sender, EventArgs e)
        {
            tpSchemasDefineLeave();
        }

        public static void SaveFormDataToFile()
        {
            MainForm.MapLoaderDefinitionManager.CreateXMLFile(@".");
        }

        private void tpSchemasDefineLeave()
        {
            if (m_IsDataChanged)
            {
                SaveFormDataToFile();
                LoadForm();
            }
        }

        private void ShowGridCoordinateSystem(bool isVisible)
        {
            lstGridCoordinateSystem.Visible = isVisible;
            lblGridCoordSys.Visible = isVisible;
            lstGridCoordinateSystem.ClearSelected();
        }

        private void cmbImageCalc_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            DNEImageType imageType = (DNEImageType)Enum.Parse(typeof(DNEImageType), cmbImageCalc_Type.Text);
            switch (imageType)
            {
            	case DNEImageType._EIT_FRAME:
                    label8.Visible = ctrlImageCalc_XmlPath.Visible = false;
                    chxImageCalc_IsFileName.Visible = false;
                    ctrlImageCalc_ctrlCameraParams1.Visible = true;
                    break;
                case DNEImageType._EIT_LOROP:
                    label8.Visible = ctrlImageCalc_XmlPath.Visible = true;
                    chxImageCalc_IsFileName.Visible = true;
                    ctrlImageCalc_ctrlCameraParams1.Visible = false;
                    break;
            }
        }

        private void chxImageCalc_IsFileName_CheckedChanged(object sender, EventArgs e)
        {
            if (chxImageCalc_IsFileName.Checked == true)
                ctrlImageCalc_XmlPath.IsFolderDialog = false;
            else
                ctrlImageCalc_XmlPath.IsFolderDialog = true;
            
        }

        private void clstChilds_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int childIndex = clstChilds.IndexFromPoint(new Point(e.X, e.Y));
            if (childIndex != -1)
            {
                int selectedId = lChildCheckListBoxIDs[childIndex] - 1;
                if (clstChilds.Tag.ToString() == "Viewport")
                {
                    tcSchemesDefine.SelectedTab = tpViewport;
                    cmbViewport_ID.SelectedIndex = selectedId;
                }
                else
                {
                    if (clstChilds.Tag.ToString() == "Terrain")
                    {
                        tcSchemesDefine.SelectedTab = tpTerrain;
                        cmbTerrain_ID.SelectedIndex = selectedId;
                    }
                    else
                    {
                        tcSchemesDefine.SelectedTab = tpLayer;
                        cmbLayer_ID.SelectedIndex = selectedId;
                    }                    
                }
            }            
        }

        private void lstGridCoordinateSystem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int childIndex = lstGridCoordinateSystem.IndexFromPoint(new Point(e.X, e.Y));
            if (childIndex != -1)
            {
                tcSchemesDefine.SelectedTab = tpCoordinateSystem;
                cmbGridCoordinateSystem_ID.SelectedIndex = lCoordSysListBoxIDs[childIndex] - 1;
            }
        }

        private void lstOverlayManager_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int childIndex = lstOverlayManager.IndexFromPoint(new Point(e.X, e.Y));
            if (childIndex != -1)
            {
                tcSchemesDefine.SelectedTab = tpOverlayManager;
                cmbOverlayManager_ID.SelectedItem = lOMListBoxIDs[childIndex];
            }
        }

        private void lstImageCalc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int childIndex = lstImageCalc.IndexFromPoint(new Point(e.X, e.Y));
            if (childIndex != -1)
            {
                tcSchemesDefine.SelectedTab = tpImageCalc;
                cmbImageCalc_ID.SelectedItem = lImageCalcListBoxIDs[childIndex];
            }
        }

        private void lstImageCalc_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > lstImageCalc.ItemHeight * lstImageCalc.Items.Count)
                lstImageCalc.ClearSelected();
        }

        private void lstOverlayManager_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > lstOverlayManager.ItemHeight * lstOverlayManager.Items.Count)
                lstOverlayManager.ClearSelected();
        }

        private void dgvLayer_ComponentsParams_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void chxIsWpfWindow_CheckedChanged(object sender, EventArgs e)
        {
            chxMultiThreadedDevice.Checked = chxIsWpfWindow.Checked;
            chxMultiThreadedDevice.Enabled = !chxIsWpfWindow.Checked;
        }

        private void MapLoaderDefinitionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MCTMapLayerReadCallback.ResetReadCallbackCounter();

            if (tcMapLoaderDefinition.SelectedTab == tpSchemasDefine)
                tpSchemasDefineLeave();

        }

        private void tcMapLoaderDefinition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMapLoaderDefinition.SelectedTab == tpSchemasDefine)
            {
                //tcSchemesDefine.SelectedTab = tpDevice;
                TabControlEventArgs eventarg = new TabControlEventArgs(tpDevice, 0, TabControlAction.Selected);
                tcSchemesDefine_Selected(this, eventarg);
            }
                //tcSchemesDefine_Selected()
        }

        private void clstChilds_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (clstChilds.Tag != null && clstChilds.Tag.ToString() == "DTMMapLayer")
            {
                for (int ix = 0; ix < clstChilds.Items.Count; ++ix)
                    if (ix != e.Index)
                        clstChilds.SetItemChecked(ix, false);
            }
        }
    }
}