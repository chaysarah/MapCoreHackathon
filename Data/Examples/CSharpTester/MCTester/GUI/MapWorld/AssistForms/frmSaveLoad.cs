using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.GUI.Map;
using MCTester.Managers.MapWorld;
using MCTester.Controls;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class frmSaveLoad : Form
    {
        private bool m_IsSaveMode; //true represent save mode. false represent load mode.
        private string m_SenderName;
        private byte[] m_TerrainBuffer;
        private IDNMcMapTerrain m_CurrTerrain;
        private IDNMcMapTerrain m_NewTerrain;
        private byte[] m_LayerBuffer;
        private IDNMcMapLayer m_CurrLayer;
        private IDNMcMapLayer m_NewLayer;
        private bool m_TerrainType;
                
        public frmSaveLoad(object sender, IDNMcMapTerrain terrain, IDNMcMapLayer layer, bool IsTerrain)
        {
            m_SenderName = sender.ToString();
            m_CurrTerrain = terrain;
            m_CurrLayer = layer;
            m_TerrainType = IsTerrain;

            InitializeComponent();
            this.Text = m_SenderName;

            switch (m_SenderName)
            {
                case "Load from a File":
                    m_IsSaveMode = false;

                    txtFileName.Enabled = true;
                    btnOpenSaveFileName.Enabled = true;
                    txtBaseDirectory.Enabled = true;
                    btnOpenSaveBaseDir.Enabled = true;
                    chkSaveUserData.Enabled = false;
                    break;
                case "Load from a Buffer":
                    m_IsSaveMode = false;

                    txtFileName.Enabled = false;
                    btnOpenSaveFileName.Enabled = false;
                    txtBaseDirectory.Enabled = true;
                    btnOpenSaveBaseDir.Enabled = true;
                    chkSaveUserData.Enabled = false;
                    break;
                case "Save to a File":
                    m_IsSaveMode = true;

                    txtFileName.Enabled = true;
                    btnOpenSaveFileName.Enabled = true;
                    txtBaseDirectory.Enabled = true;
                    btnOpenSaveBaseDir.Enabled = true;
                    chkSaveUserData.Enabled = true;
                    break;
                case "Save to a Buffer":
                    m_IsSaveMode = true;

                    txtFileName.Enabled = false;
                    btnOpenSaveFileName.Enabled = false;
                    txtBaseDirectory.Enabled = true;
                    btnOpenSaveBaseDir.Enabled = true;
                    chkSaveUserData.Enabled = true;
                    break;
            }
        }

        private void btnOpenSaveFileName_Click(object sender, EventArgs e)
        {
            txtFileName.Text = FileDialog();
        }

        private void btnOpenSaveBaseDir_Click(object sender, EventArgs e)
        {
            BaseDirectory = FileDialog(true);
        }

        private string FileDialog(bool isDir = false)
        {
            string fileName = null;
            if (m_SenderName == "Load from a Buffer" || m_SenderName == "Save to a Buffer" || isDir)
            {
                FolderSelectDialog FSD = new FolderSelectDialog();
                FSD.Title = "Folder to select";
                FSD.InitialDirectory = @"c:\";
                if (FSD.ShowDialog(IntPtr.Zero))
                {
                    fileName = FSD.FileName;
                }
            }
            else
            {
                if (m_IsSaveMode == true)
                {
                    SaveFileDialog SFD = new SaveFileDialog();
                    SFD.RestoreDirectory = true;
                    if (SFD.ShowDialog() == DialogResult.OK)
                    {
                        fileName = SFD.FileName;
                    }
                }
                else
                {
                    OpenFileDialog OFD = new OpenFileDialog();
                    OFD.RestoreDirectory = true;
                    if (OFD.ShowDialog() == DialogResult.OK)
                    {
                        fileName = OFD.FileName;
                    }
                }
            }            

            return fileName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (m_SenderName)
            {
                case "Load from a File":
                    try
                    {
                        UserDataFactory UDF = new UserDataFactory();

                        if (m_TerrainType == true)  //Load terrain
                        {
                            m_NewTerrain = DNMcMapTerrain.Load(txtFileName.Text,
                                                                BaseDirectory,
                                                                UDF,
                                                               new MCTMapLayerReadCallbackFactory());
                            Manager_MCLayers.CheckingAfterLoadTerrain(m_NewTerrain);
                         
                        }
                        else //Load layer
                        {
                            m_NewLayer = DNMcMapLayer.Load(txtFileName.Text,
                                                            BaseDirectory,
                                                            UDF,
                                                            new MCTMapLayerReadCallbackFactory());
                             MCTester.Managers.MapWorld.Manager_MCLayers.CheckingAfterCreateLayer(m_NewLayer, true);
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("Load", McEx);
                    }
                    break;
                case "Load from a Buffer":
                    try
                    {
                        UserDataFactory UDF = new UserDataFactory();

                        if (m_TerrainType == true) //Load terrain
                        {
                            m_NewTerrain = DNMcMapTerrain.Load(MCTMapForm.m_dicBuffer[3],
                                                                BaseDirectory,
                                                                UDF,
                                                                new MCTMapLayerReadCallbackFactory());

                            Manager_MCLayers.CheckingAfterLoadTerrain(m_NewTerrain, true);
                           
                        }
                        else //Load layer
                        {
                            m_NewLayer = DNMcMapLayer.Load(MCTMapForm.m_dicBuffer[4],
                                                            BaseDirectory,
                                                            UDF,
                                                            new MCTMapLayerReadCallbackFactory());


                            MCTester.Managers.MapWorld.Manager_MCLayers.CheckingAfterCreateLayer(m_NewLayer, true);
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("Load", McEx);
                    }
                    break;
                case "Save to a File":
                    try
                    {
                        if (m_TerrainType == true) //Save terrain
                        {
                            m_CurrTerrain.Save(txtFileName.Text,
                                                BaseDirectory,
                                                chkSaveUserData.Checked);
                        }
                        else //Save layer
                        {
                            m_CurrLayer.Save(txtFileName.Text,
                                                BaseDirectory,
                                                chkSaveUserData.Checked);
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("Save", McEx);
                    }
                    break;
                case "Save to a Buffer":
                    try
                    {
                        if (m_TerrainType == true) //Save terrain
                        {
                            TerrainBuffer = m_CurrTerrain.Save(BaseDirectory,
                                                                 chkSaveUserData.Checked);

                            MCTMapForm.m_dicBuffer[3] = TerrainBuffer;
                        }
                        else //Save layer
                        {
                            LayerBuffer = m_CurrLayer.Save(BaseDirectory,
                                                             chkSaveUserData.Checked);

                            MCTMapForm.m_dicBuffer[4] = LayerBuffer;
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("Save", McEx);
                    }
                    break;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public byte [] TerrainBuffer
        {
            get { return m_TerrainBuffer; }
            set { m_TerrainBuffer = value; }
        }

        public byte[] LayerBuffer
        {
            get { return m_LayerBuffer; }
            set { m_LayerBuffer = value; }
        }

        public string BaseDirectory
        {
            get 
            {
                if (txtBaseDirectory.Text == "")
                    return null;
                else
                    return txtBaseDirectory.Text; 
            }
            set { txtBaseDirectory.Text = value; }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {

        }
    }
}