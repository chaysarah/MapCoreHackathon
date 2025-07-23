using MapCore;
using MapCore.Common;
using MCTester.Controls;
using MCTester.Managers;
using MCTester.Managers.MapWorld;
using MCTester.MapWorld;
using MCTester.MapWorld.Assist_Forms;
using MCTester.MapWorld.MagicForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCTester.ButtonsImplementation
{
    public partial class frmMagicForm : Form
    {
        private int m_colLayerType = 0;
        private int m_colLayerPath = 1;
        private int m_colSelectPath = 2;
        private int m_colLayerParams = 3;

        //private DNSRawVectorParams mcRawVectorParams;
        private DNSRawVector3DExtrusionParams mExtrusionParams = new DNSRawVector3DExtrusionParams();
        private DNSRawVector3DExtrusionGraphicalParams mExtrusionGraphicalParams = new DNSRawVector3DExtrusionGraphicalParams();

        //private IDNMcGridCoordinateSystem mRawVectorTargetGridCoordinateSystem;
        private bool m_isFinishLoad = false;

        private IDNMcMapDevice m_device;
        private MCTMapDevice MapDevice;

        /*   class layersdata {
               public DNELayerType layerType;
               public String layerPath;
               public object layerParams;
           }*/
        public frmMagicForm()
        {
            InitializeComponent();

            DataGridViewComboBoxColumn colCombo = (dataGridView1.Columns[m_colLayerType] as DataGridViewComboBoxColumn);
            colCombo.DataSource = Manager_MCLayers.GetLayerNames();

            DNSInitParams initParams = new DNSInitParams();
            if (MCTMapDevice.CurrDevice != null)
            {
                m_device = MCTMapDevice.m_Device;
               // ctrlDeviceParams1.Enabled =  btnCreateDevice.Enabled = false;
                initParams = MCTMapDevice.CurrDevice.GetDeviceParams();
            }
            ctrlDeviceParams1.SetDeviceParams(initParams);
            m_isFinishLoad = true;
        }

        private bool GetIsFolder(DNELayerType layerType)
        {
            switch(layerType)
            {
                case DNELayerType._ELT_RAW_VECTOR:
                case DNELayerType._ELT_RAW_VECTOR_3D_EXTRUSION:
                    return false;
                default: return true;
            }
        }
        private string Filter { get; set; }

        private int GetLayerTypeAsInt(int rowIndex)
        {
            int index = -1;
            DNELayerType layerType = DNELayerType._ELT_NATIVE_DTM;
            if (rowIndex >= 0 && rowIndex < dataGridView1.RowCount)
            {
                object obj1 = dataGridView1[m_colLayerType, rowIndex].Value;
                if (obj1 != null)
                {
                    string text = obj1.ToString();
                    layerType = (DNELayerType)Enum.Parse(typeof(DNELayerType), Manager_MCLayers.LayerTypePrefix + text);
                    index = (int)layerType;
                }
            }
            return index;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int layerType = GetLayerTypeAsInt(e.RowIndex);
                if (layerType >= 0)
                {
                    if (e.ColumnIndex == m_colSelectPath)  // select folder btn column
                    {
                        DNELayerType eLayerType = (DNELayerType)layerType;
                        if (GetIsFolder(eLayerType))
                        {
                            FolderSelectDialog FSD = new FolderSelectDialog();
                            FSD.Title = "Folder to select";
                            FSD.InitialDirectory = @"c:\";
                            if (FSD.ShowDialog(IntPtr.Zero))
                            {
                                InsertNewPath(FSD.FileName, e.RowIndex);
                            }
                        }
                        else
                        {
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            openFileDialog.Filter = Filter;
                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                InsertNewPath(openFileDialog.FileName, e.RowIndex);
                            }
                        }

                    }
                    else if (e.ColumnIndex == m_colLayerParams)
                    {
                        object objParams = dataGridView1[e.ColumnIndex, e.RowIndex].Tag;

                        switch (layerType)
                        {
                            case (int)DNELayerType._ELT_RAW_VECTOR:
                                DNSRawVectorParams mcRawVectorParams = null;
                                if (objParams != null && objParams is DNSRawVectorParams)
                                {
                                    mcRawVectorParams = (DNSRawVectorParams)objParams;
                                }
                                else
                                    mcRawVectorParams = CtrlRawVectorParams.GetSRawVectorParamsWithDefaultValue();

                                object objGridCoordSystem = dataGridView1[m_colSelectPath, e.RowIndex].Tag;
                                IDNMcGridCoordinateSystem mRawVectorTargetGridCoordinateSystem = null;
                                if (objGridCoordSystem != null && objGridCoordSystem is IDNMcGridCoordinateSystem)
                                    mRawVectorTargetGridCoordinateSystem = (IDNMcGridCoordinateSystem)objGridCoordSystem;

                                frmRawVectorParams frmRawVectorParams1 = new frmRawVectorParams();

                                if (ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem != null)
                                {
                                    mcRawVectorParams.pSourceCoordinateSystem = ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem;
                                    frmRawVectorParams1.TargetGridCoordinateSystem = ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem;
                                    ctrlGridCoordinateSystemRawLayers.ClearSelectedList();
                                }
                                else
                                {
                                    frmRawVectorParams1.TargetGridCoordinateSystem = mRawVectorTargetGridCoordinateSystem;
                                }

                                frmRawVectorParams1.RawVectorParams = mcRawVectorParams;

                                if (frmRawVectorParams1.ShowDialog() == DialogResult.OK)
                                {
                                    dataGridView1[e.ColumnIndex, e.RowIndex].Tag = frmRawVectorParams1.RawVectorParams;
                                    dataGridView1[m_colSelectPath, e.RowIndex].Tag = frmRawVectorParams1.TargetGridCoordinateSystem;
                                }

                                break;
                            case (int)DNELayerType._ELT_RAW_VECTOR_3D_EXTRUSION:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        /*
         private void btnRawVectorParams_Click(object sender, EventArgs e)
        {
            frmRawVectorParams frmRawVectorParams = new frmRawVectorParams();
            if (ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem != null)
            {
                mcRawVectorParams.pSourceCoordinateSystem = ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem;
                frmRawVectorParams.TargetGridCoordinateSystem = ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem;
                ctrlGridCoordinateSystemRawLayers.ClearSelectedList();
            }
            else
            {
                frmRawVectorParams.TargetGridCoordinateSystem = mRawVectorTargetGridCoordinateSystem;
            }

            frmRawVectorParams.RawVectorParams = mcRawVectorParams;

            if (frmRawVectorParams.ShowDialog() == DialogResult.OK)
            {
                mcRawVectorParams = frmRawVectorParams.RawVectorParams;
                mRawVectorTargetGridCoordinateSystem = frmRawVectorParams.TargetGridCoordinateSystem;
            }
        }

*/
        private void InsertNewPath(string path, int rowIndex)
        {
            if (rowIndex == dataGridView1.RowCount - 1 || dataGridView1.Rows[dataGridView1.RowCount - 1].IsNewRow == false)
            {
                dataGridView1.Rows.Add();
            }
            dataGridView1[m_colLayerPath, rowIndex].Value = path;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[m_colSelectPath].Value = "...";
            this.dataGridView1.Rows[e.RowIndex].Cells[m_colLayerParams].Value = "Params...";
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //this.dataGridView2.Rows[e.RowIndex].Cells[m_colAdd].Value = "+";
            //this.dataGridView2.Rows[e.RowIndex].Cells[m_colRemove].Value = "-";
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            // dataGridView1.SelectedRows SaveCurrRow();
            if (m_SelectedRow >= 0 && dgvLayers.RowCount > m_SelectedRow)
                dgvLayers.Rows[m_SelectedRow].Tag = SaveCurrRow();
            dgvLayers.Rows.Insert(dgvLayers.RowCount/*dataGridView2.Rows.GetLastRow(DataGridViewElementStates.None)*/, 1);
            dgvLayers.CurrentCell = dgvLayers[0, dgvLayers.RowCount - 1];
            dgvLayers.Rows[dgvLayers.RowCount - 1].Selected = true;
           // m_SelectedRow = dataGridView2.RowCount;

            ucLayerParams1.SetDefaultValues();
        }

        private MCTSLayerData SetParams(MCTSLayerData layerData)
        {
            //layerData = ;
            layerData.LayerPath = ucLayerParams1.GetLayerFileName();
            layerData.NoOfLayers = ucLayerParams1.GetNumOfLayers();
            layerData.LayerSubFolderLocalCache = ucLayerParams1.GetLocalCacheLayerParams();
            return layerData;
        }

        private MCTSLayerData SaveCurrRow()
        {
            if (m_isFinishLoad)
            {
                DNELayerType eLayerType = ucLayerParams1.GetMapLayerTypeAsEnum();
                MCTSLayerData layerDataDefault = new MCTSLayerData();
                layerDataDefault = SetParams(layerDataDefault);
                layerDataDefault.LayerType = eLayerType;

                try
                {
                    dgvLayers.Rows[m_SelectedRow].Cells[1].Value = ucLayerParams1.GetMapLayerType() + " , " + ucLayerParams1.GetLayerFileName();
                }
                catch(Exception ) { }

                switch (eLayerType)
                {
                    case DNELayerType._ELT_NATIVE_SERVER_3D_MODEL:              
                    case DNELayerType._ELT_NATIVE_SERVER_DTM:                   
                    case DNELayerType._ELT_NATIVE_SERVER_MATERIAL:              
                    case DNELayerType._ELT_NATIVE_SERVER_RASTER:                
                    case DNELayerType._ELT_NATIVE_SERVER_TRAVERSABILITY:        
                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR:                
                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR_3D_EXTRUSION:   
                    case DNELayerType._ELT_NATIVE_VECTOR:                                                
                        return layerDataDefault;

                    case DNELayerType._ELT_NATIVE_HEAT_MAP: 
                    case DNELayerType._ELT_NATIVE_RASTER:        
                        MCTSLayerDataRaster layerData3 = new MCTSLayerDataRaster();
                        layerData3 = (MCTSLayerDataRaster)SetParams(layerData3);
                        layerData3.LayerType = eLayerType;
                        layerData3.NumLevelsToIgnore = ucLayerParams1.GetNumLevelsToIgnore();
                        layerData3.ThereAreMissingFiles = ucLayerParams1.GetThereAreMissingFiles();
                        layerData3.FirstLowerQualityLevel = ucLayerParams1.GetFirstLowerQualityLevel();
                        layerData3.EnhanceBorderOverlap = ucLayerParams1.GetEnhanceBorderOverlap2();
                        return layerData3;
                    
                    case DNELayerType._ELT_NATIVE_3D_MODEL: 
                    case DNELayerType._ELT_NATIVE_DTM:              
                        MCTSLayerDataDTM layerData = new MCTSLayerDataDTM();
                        layerData = (MCTSLayerDataDTM)SetParams(layerData);
                        layerData.LayerType = eLayerType;
                        layerData.NumLevelsToIgnore = ucLayerParams1.GetNumLevelsToIgnore();
                        return layerData;
                    case DNELayerType._ELT_NATIVE_VECTOR_3D_EXTRUSION: 
                        MCTSLayerDataVector3DExtrusion layerData2 = new MCTSLayerDataVector3DExtrusion();
                        layerData2 = (MCTSLayerDataVector3DExtrusion)SetParams(layerData2);
                        layerData2.LayerType = eLayerType;
                        layerData2.NumLevelsToIgnore = ucLayerParams1.GetNumLevelsToIgnore();
                        layerData2.ExtrusionHeightMaxAddition = ucLayerParams1.GetExtrusionHeightMaxAddition();
                        return layerData2;

                    case DNELayerType._ELT_NATIVE_MATERIAL:         
                    case DNELayerType._ELT_NATIVE_TRAVERSABILITY:      
                        MCTSLayerDataTraversability layerData4 = new MCTSLayerDataTraversability();
                        layerData4 = (MCTSLayerDataTraversability)SetParams(layerData4);
                        layerData4.LayerType = eLayerType;
                        layerData4.ThereAreMissingFiles = ucLayerParams1.GetThereAreMissingFiles();
                        return layerData4;
                    
                    case DNELayerType._ELT_RAW_DTM:
                        MCTSLayerDataRawDTM layerData5 = new MCTSLayerDataRawDTM();
                        layerData5 = (MCTSLayerDataRawDTM)SetParams(layerData5);
                        layerData5.Params = ucLayerParams1.GetSRawParams();
                        layerData5.LayerType = eLayerType;
                        return layerData5;
                    case DNELayerType._ELT_RAW_RASTER: 
                        MCTSLayerDataRawRaster layerData6 = new MCTSLayerDataRawRaster();
                        layerData6 = (MCTSLayerDataRawRaster)SetParams(layerData6);
                        layerData6.LayerType = eLayerType;
                        layerData6.Params = ucLayerParams1.GetSRawParams();
                        layerData6.ImageCoordSys = ucLayerParams1.GetImageCoordSys();
                        return layerData6;
                    case DNELayerType._ELT_RAW_VECTOR: 
                        MCTSLayerDataRawVector layerData7 = new MCTSLayerDataRawVector();
                        layerData7 = (MCTSLayerDataRawVector)SetParams(layerData7);
                        layerData7.LayerType = eLayerType;
                        layerData7.Params = ucLayerParams1.GetRawVectorParams();
                        return layerData7;
                    case DNELayerType._ELT_RAW_VECTOR_3D_EXTRUSION: 
                        if (ucLayerParams1.GetIsUseIndexing())
                        {
                            if (!ucLayerParams1.CheckRawVector3DExtrusionValidity())
                            {
                                Enabled = true;
                                return null;
                            }
                            else
                            {
                                MCTSLayerDataRawVector3DExtrusionGraphical layerData8 = new MCTSLayerDataRawVector3DExtrusionGraphical();
                                layerData8 = (MCTSLayerDataRawVector3DExtrusionGraphical)SetParams(layerData8);
                                layerData8.LayerType = eLayerType;
                                layerData8.Params = ucLayerParams1.GetRawVector3DExtrusionGraphicalParams();
                                layerData8.ExtrusionHeightMaxAddition = ucLayerParams1.GetExtrusionHeightMaxAddition();
                                layerData8.StrIndexingDataDirectory = ucLayerParams1.GetExtrusionIndexingDataDirectory();
                                return layerData8;
                            }
                        }
                        else
                        {
                            MCTSLayerDataRawVector3DExtrusion layerData9 = new MCTSLayerDataRawVector3DExtrusion();
                            layerData9 = (MCTSLayerDataRawVector3DExtrusion)SetParams(layerData9);
                            layerData9.LayerType = eLayerType;
                            layerData9.Params = ucLayerParams1.GetRawVector3DExtrusionParams();
                            layerData9.ExtrusionHeightMaxAddition = ucLayerParams1.GetExtrusionHeightMaxAddition();
                            return layerData9;
                        }
                    
                    case DNELayerType._ELT_RAW_3D_MODEL: 
                        MCTSLayerDataRaw3DModel layerData10 = new MCTSLayerDataRaw3DModel();
                        layerData10 = (MCTSLayerDataRaw3DModel)SetParams(layerData10);
                        layerData10.LayerType = eLayerType;
                       // layerData10.OrthometricHeights = ucLayerParams1.GetOrthometricHeights();
                        layerData10.NumLevelsToIgnore = ucLayerParams1.GetNumLevelsToIgnore();
                       // layerData10.StrIndexingDataDirectory = ucLayerParams1.Get3DModelIndexingDataDirectory();
                        return layerData10;
                   
                    case DNELayerType._ELT_WEB_SERVICE_DTM:     
                    case DNELayerType._ELT_WEB_SERVICE_RASTER:   
                        
                        return layerDataDefault;
                       
                }

                return layerDataDefault;
            }
            return null;
        }

        private void LoadCurrRow(object tag)
        {
            if (tag is MCTSLayerData)
            {
                MCTSLayerData layerData = (MCTSLayerData)tag;
                ucLayerParams1.SetMapLayerType(layerData.LayerType);
                ucLayerParams1.SetLayerFileName(layerData.LayerPath);
                ucLayerParams1.SetNumOfLayers(layerData.NoOfLayers);
                ucLayerParams1.SetLocalCacheLayerParams(layerData.LayerSubFolderLocalCache);

                switch(layerData.LayerType)
                {
                    case DNELayerType._ELT_NATIVE_SERVER_DTM:
                    case DNELayerType._ELT_NATIVE_SERVER_RASTER:
                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR:
                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR_3D_EXTRUSION:
                    case DNELayerType._ELT_NATIVE_SERVER_3D_MODEL:
                    case DNELayerType._ELT_NATIVE_SERVER_TRAVERSABILITY:
                    case DNELayerType._ELT_NATIVE_SERVER_MATERIAL:
                    case DNELayerType._ELT_NATIVE_VECTOR:
                        break;
                    case DNELayerType._ELT_NATIVE_HEAT_MAP:
                    case DNELayerType._ELT_NATIVE_RASTER:
                        MCTSLayerDataRaster layerData3 = (MCTSLayerDataRaster)layerData;
                        ucLayerParams1.SetThereAreMissingFiles(layerData3.ThereAreMissingFiles);
                        ucLayerParams1.SetFirstLowerQualityLevel(layerData3.FirstLowerQualityLevel);
                        ucLayerParams1.SetEnhanceBorderOverlap2(layerData3.EnhanceBorderOverlap);
                        break;

                    case DNELayerType._ELT_NATIVE_3D_MODEL:
                    case DNELayerType._ELT_NATIVE_DTM:
                        MCTSLayerDataDTM layerData4 = (MCTSLayerDataDTM)layerData;
                        ucLayerParams1.SetNumLevelsToIgnore(layerData4.NumLevelsToIgnore);

                        break;
                    /*
                      MCTSLayerDataRawDTM layerData5 = new MCTSLayerDataRawDTM();
                    layerData5 = (MCTSLayerDataRawDTM)SetParams(layerData5);
                    layerData5.Params = ucLayerParams1.GetSRawParams();
                    layerData5.LayerType = eLayerType;
                    return layerData5;
                case DNELayerType._ELT_RAW_RASTER: 
                    MCTSLayerDataRawRaster layerData6 = new MCTSLayerDataRawRaster();
                    layerData6 = (MCTSLayerDataRawRaster)SetParams(layerData6);
                    layerData6.LayerType = eLayerType;
                    layerData6.Params = ucLayerParams1.GetSRawParams();
                    layerData6.ImageCoordSys = ucLayerParams1.GetImageCoordSys();
                    return layerData6;
                    */
                    case DNELayerType._ELT_RAW_RASTER:
                    case DNELayerType._ELT_RAW_DTM:
                        MCTSLayerDataRawDTM layerData5 = (MCTSLayerDataRawDTM)layerData;
                        ucLayerParams1.SetRawParams(layerData5.Params);

                        if(layerData is MCTSLayerDataRawRaster)
                        {
                            MCTSLayerDataRawRaster layerData6 = (MCTSLayerDataRawRaster)layerData;
                            ucLayerParams1.SetImageCoordSys(layerData6.ImageCoordSys );
                        }
                        break;
                    
                }
            }
        }

        private void btnRemoveLayers_Click(object sender, EventArgs e)
        {

        }

        private int m_SelectedRow = 0;

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (m_isFinishLoad)
            {
                if (m_SelectedRow >= 0 && dgvLayers.RowCount > m_SelectedRow)
                    dgvLayers.Rows[m_SelectedRow].Tag = SaveCurrRow();
                if (dgvLayers.SelectedRows.Count > 0)
                {
                    m_SelectedRow = dgvLayers.SelectedRows[0].Index;
                    if (dgvLayers.Rows[m_SelectedRow].Tag != null)
                        LoadCurrRow(dgvLayers.Rows[m_SelectedRow].Tag);
                }
            }
        }

        public void CreateDevice()
        {
            try
            {
                if (m_device == null)
                {
                    DNSInitParams initParams = ctrlDeviceParams1.GetDeviceParams();
                    if (initParams.uNumBackgroundThreads == 0)
                        MessageBox.Show("Num Background Threads should be greater then 0");
                    else
                    {
                        MapDevice = new MCTMapDevice();

                        Manager_MCGeneralDefinitions.m_NumBackgroundThreads = initParams.uNumBackgroundThreads;
                        Manager_MCGeneralDefinitions.m_MultiScreenDevice = initParams.bMultiScreenDevice;
                        Manager_MCGeneralDefinitions.m_MultiThreadedDevice = initParams.bMultiThreadedDevice;

                        MapDevice.SetDeviceParams(initParams);

                        m_device = MapDevice.CreateDevice();

                        if (m_device != null)
                        {
                            ctrlDeviceParams1.Enabled = false;
                            //btnCreateDevice.Enabled = false;
                        }
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcMapDevice.Create", McEx);
            }
        }
        private void btnCreateMap_Click(object sender, EventArgs e)
        {

            
        }

       

        
    }
}
