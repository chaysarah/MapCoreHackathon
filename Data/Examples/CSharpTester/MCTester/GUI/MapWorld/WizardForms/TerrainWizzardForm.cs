using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;
using MapCore;
using MCTester.Managers;
using MCTester.Managers.MapWorld;

namespace MCTester.MapWorld.WizardForms
{
    public partial class TerrainWizzardForm : Form, IUserControlItem
    {
        private IDNMcMapTerrain aTerrain;
       // private List<AddLayerForm> m_layersList;
        private List<IDNMcMapTerrain> m_terrainsList = new List<IDNMcMapTerrain>();
        private List<string> m_lstTerrainText = new List<string>();
        private List<IDNMcMapTerrain> m_lstTerrainValue = new List<IDNMcMapTerrain>();

        public TerrainWizzardForm(SelectionMode selectionMode = SelectionMode.One)
        {
            InitializeComponent();
            lstTerrains.SelectionMode = selectionMode;
            if(selectionMode == SelectionMode.MultiSimple)
            {
                m_radUseExisting.Text = m_radUseExisting.Text + " (Multi Selection)";
            }

            Manager_MCLayers.AddCtrlLayersToList(ctrlLayers1);
        }

        #region IUserControlItem Members
        public void LoadItem(object aItem)
        {

        }
        #endregion

        public void NoticeLayerRemoved(IDNMcMapLayer layerToRemove)
        {
            ctrlLayers1.NoticeLayerRemoved(layerToRemove);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_radCreateNew.Checked)
                {
                    MCTMapLayerReadCallback.IsSaveReplacedOrRemovedLayer = true;

                    List<IDNMcMapLayer> lstLayers = ctrlLayers1.GetLayers();

                    List<IDNMcMapLayer> lstLayersAfterInit = MCTMapLayerReadCallback.CheckLayersIsRemovedOrReplaced(lstLayers);
                    MCTMapLayerReadCallback.RemoveAllReplacedLayer();

                    aTerrain = Manager_MCTerrain.CreateTerrain(ctrlTerrainGridCoordinateSystem.GridCoordinateSystem, lstLayersAfterInit.ToArray(), chxDisplayItemsAttachedTo3DModelWithoutDtm.Checked);
                    //MainForm.flagTemp = true;
                    if (aTerrain != null)
                    {
                        //In case it is not a new layer remove it from the standalone layer dictionary
                        Manager_MCLayers.RemoveStandaloneLayers(lstLayersAfterInit.ToArray());

                        Manager_MCLayers.DrawPriorityServerLayer(aTerrain);
                    }
                }

                else if (m_radUseExisting.Checked)
                {
                    if (lstTerrains.SelectionMode == SelectionMode.One)
                    {
                        if (lstTerrains.SelectedIndex == -1)
                        {
                            MessageBox.Show("No existing Terrain was specified.\nYou have to choose one!\n");
                            return;
                        }
                        aTerrain = m_lstTerrainValue[lstTerrains.SelectedIndex];
                        if (aTerrain == null)
                        {
                            MessageBox.Show("No existing Terrain was specified.\nYou have to choose one!\n");
                            return;
                        }
                    }
                    else
                    {
                        foreach (int i in lstTerrains.SelectedIndices)
                        {
                            m_terrainsList.Add(m_lstTerrainValue[i]);
                        }
                    }
                }

                else if (m_radLoadFromFile.Checked)
                {
                    try
                    {
                        UserDataFactory UDF = new UserDataFactory();

                        IDNMcMapTerrain newTerrain = DNMcMapTerrain.Load(txtFileName.Text,
                                                                            BaseDirectory,
                                                                            UDF,
                                                                            new MCTMapLayerReadCallbackFactory());

                        aTerrain = newTerrain;
                        Manager_MCLayers.CheckingAfterLoadTerrain(newTerrain, true);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcMapTerrain.Load", McEx);
                    }

                }
                if (aTerrain != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (MapCoreException McEx)
            {
                this.DialogResult = DialogResult.Cancel;
                MapCore.Common.Utilities.ShowErrorMessage("CreateTerrain", McEx);
            }
           
        }



        public IDNMcMapTerrain Terrain
        {
            get { return aTerrain; }
        }

        public List<IDNMcMapTerrain> Terrains
        {
            get { return m_terrainsList; }
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
        }

        private void TerrainWizzardForm_Load(object sender, EventArgs e)
        {
            Manager_MCLayers.ResetServerLayersPriorityDic();

           // MainForm.mIsPerformPendingCalculationsSuspend = true;
            lstTerrains.Items.Clear();
            List<IDNMcMapTerrain> terrainList = Manager_MCTerrain.AllTerrains();
            foreach (IDNMcMapTerrain terr in terrainList)
            {
                m_lstTerrainText.Add(Manager_MCNames.GetNameByObject(terr, "Terrain"));
                m_lstTerrainValue.Add(terr);
            }

            lstTerrains.Items.AddRange(m_lstTerrainText.ToArray());
        }

        private void btnOpenSaveFileName_Click(object sender, EventArgs e)
        {
            txtFileName.Text = OpenFileDlg();
        }

        private void btnOpenSaveBaseDir_Click(object sender, EventArgs e)
        {
            txtBaseDirectory.Text = OpenFileDlg();
        }

        private string OpenFileDlg()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.RestoreDirectory = true;
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                return OFD.FileName;
            }
            else
                return "";
        }

        private void m_radCreateNew_CheckedChanged(object sender, EventArgs e)
        {
            gbCreateNew.Enabled = true;
            ctrlLayers1.SetEnabled(true);
           
            lstTerrains.Enabled = false;
            gbLoadFromFile.Enabled = false;
            lstTerrains.ClearSelected();
        }

        private void m_radUseExisting_CheckedChanged(object sender, EventArgs e)
        {
            gbCreateNew.Enabled = false;
            lstTerrains.Enabled = true;
            gbLoadFromFile.Enabled = false;
            
            ctrlLayers1.ClearSelected();

            ctrlTerrainGridCoordinateSystem.ClearSelectedList();
        }

        private void m_radLoadFromFile_CheckedChanged(object sender, EventArgs e)
        {
            gbCreateNew.Enabled = false;
            lstTerrains.Enabled = false;
            gbLoadFromFile.Enabled = true;
            lstTerrains.ClearSelected();

            ctrlLayers1.ClearSelected();
           
            ctrlTerrainGridCoordinateSystem.ClearSelectedList();
        }

        private void TerrainWizzardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MCTMapLayerReadCallback.RemoveAllReplacedLayer();
            Manager_MCLayers.RemoveCtrlLayersFromList(ctrlLayers1);
        }
    }
}