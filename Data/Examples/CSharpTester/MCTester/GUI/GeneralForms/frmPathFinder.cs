using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.General_Forms
{
    public partial class frmPathFinder : Form
    {
        private IDNMcPathFinder m_PathFinder;

        public frmPathFinder()
        {
            InitializeComponent();
        }

        private void btnPathFinderCreate_Click(object sender, EventArgs e)
        {
            string [] obstaclesTables = new string[dgvObstaclesTables.RowCount-1];
            string [] costFields = new string[dgvCostFields.RowCount-1];

            int currRowNum = 0;
            while (!dgvObstaclesTables.Rows[currRowNum].IsNewRow)
            {
                obstaclesTables[currRowNum] = dgvObstaclesTables[0, currRowNum].Value.ToString();

                currRowNum++;
            }

            currRowNum = 0;
            while (!dgvCostFields.Rows[currRowNum].IsNewRow)
            {
                costFields[currRowNum] = dgvCostFields[0, currRowNum].Value.ToString();

                currRowNum++;
            }           

            try
            {

                m_PathFinder = DNMcPathFinder.Create(txtVectorData.Text, 
                                                        txtTableName.Text, 
                                                        obstaclesTables, 
                                                        ntxPointTolerance.GetUInt32(), 
                                                        costFields);	
                
                if (m_PathFinder != null)
                {
                    gbShortestPath.Enabled = true;
                    btnUpdateTables.Enabled = true;
                    btrnFindShortestPath.Enabled = true;
                }
            }
            catch (MapCoreException McEx)
            {
            	MapCore.Common.Utilities.ShowErrorMessage("Create", McEx);
            }            
        }

        private void btnUpdateTables_Click(object sender, EventArgs e)
        {
            if (m_PathFinder != null)
            {
                try
                {
                    m_PathFinder.UpdateTables();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("UpdateTables", McEx);
                }
            }
        }

        private void btrnFindShortestPath_Click(object sender, EventArgs e)
        {
            if (m_PathFinder != null)
            {
                dgvLocationPoints.Rows.Clear();
                dgvLocationPoints.RowCount = 0;                
                txtEdgeIds.Clear();

                try
                {
                    DNSMcVector3D [] paLocationPoints;
                    uint [] aEdgeIds;

                    m_PathFinder.FindShortestPath(ctrl3DFindShortestPathSourcePt.GetVector3D(),
                                                    ctrl3DFindShortestPathTargetPt.GetVector3D(),
                                                    txtCostField.Text,
                                                    txtReverseCostField.Text,
                                                    chxConsiderObstacles.Checked,
                                                    out paLocationPoints,
                                                    out aEdgeIds);


                    if (paLocationPoints != null && paLocationPoints.Length > 0)
                    {
                        dgvLocationPoints.RowCount = paLocationPoints.Length;

                        for (int i = 0; i < paLocationPoints.Length; i++ )
                        {
                            dgvLocationPoints[0, i].Value = paLocationPoints[i].x.ToString();
                            dgvLocationPoints[1, i].Value = paLocationPoints[i].y.ToString();
                            dgvLocationPoints[2, i].Value = paLocationPoints[i].z.ToString();
                        }
                    }

                    if (aEdgeIds != null && aEdgeIds.Length > 0)
                    {
                        for (int i = 0; i < aEdgeIds.Length; i++ )
                        {
                            txtEdgeIds.Text += aEdgeIds[i].ToString() + ",";
                        }
                    }
                    //Draw Shortest Path
                    try
                    {
                        IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                        IDNMcObjectSchemeItem ObjSchemeItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                    DNELineStyle._ELS_DASH,
                                                                                    new DNSMcBColor(255,255,255, 255),
                                                                                    5f);
                        IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                            ObjSchemeItem,
                                                            DNEMcPointCoordSystem._EPCS_WORLD,
                                                            paLocationPoints,
                                                            false);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("FindShortestPath", McEx);
                }

            }            
        }
    }
}
