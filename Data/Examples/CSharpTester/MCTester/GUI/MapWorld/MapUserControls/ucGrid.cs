using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.General_Forms;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucGrid : UserControl, IUserControlItem
    {
        private IDNMcMapGrid m_CurrObject;
        
        private DNSGridRegion[] m_ArrGridRegion;
        private DNSScaleStep[] m_ArrScaleStep;
        
        public ucGrid()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {
            m_CurrObject = (IDNMcMapGrid)aItem;
            
            try
            {
                m_ArrScaleStep = m_CurrObject.GetScaleSteps();
                FillScaleStepDGV();

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetScaleSteps", McEx);
            }

            try
            {
                m_ArrGridRegion = m_CurrObject.GetGridRegions();
                FillGridRegionDGV();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetGridRegions", McEx);
            }

            try
            {
                chxIsUsingBasicItemPropertiesOnly.Checked = m_CurrObject.IsUsingBasicItemPropertiesOnly();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("IsUsingBasicItemPropertiesOnly", McEx);
            }

        }

        #endregion



        private List<DNSScaleStep> GetScaleStepDGVParams()
        {
            List<DNSScaleStep> scaleStepList = new List<DNSScaleStep>();
            DNSScaleStep newStep = new DNSScaleStep();
            int rowIndex = 0;
            while (!dgvScaleStep.Rows[rowIndex].IsNewRow)
            {
                bool tryParseSuccess;
                float fRes;
                double dRes;
                uint uRes;

                if (dgvScaleStep[Grid_Col1, rowIndex].Value != null)
                {
                    tryParseSuccess = float.TryParse(dgvScaleStep[Grid_Col1, rowIndex].Value.ToString(), out fRes);
                    newStep.fMaxScale = (tryParseSuccess == true) ? fRes : 0;
                }
                else
                    newStep.fMaxScale = 0;

                if (dgvScaleStep[Grid_Col2, rowIndex].Value != null)
                {
                    tryParseSuccess = double.TryParse(dgvScaleStep[Grid_Col2, rowIndex].Value.ToString(), out dRes);
                    newStep.NextLineGap.x = (tryParseSuccess == true) ? dRes : 0;
                }
                else
                    newStep.NextLineGap.x = 0;

                if (dgvScaleStep[Grid_Col3, rowIndex].Value != null)
                {
                    tryParseSuccess = double.TryParse(dgvScaleStep[Grid_Col3, rowIndex].Value.ToString(), out dRes);
                    newStep.NextLineGap.y = (tryParseSuccess == true) ? dRes : 0;
                }
                else
                    newStep.NextLineGap.y = 0;

                if (dgvScaleStep[Grid_Col4, rowIndex].Value != null)
                {
                    tryParseSuccess = uint.TryParse(dgvScaleStep[Grid_Col4, rowIndex].Value.ToString(), out uRes);
                    newStep.uNumOfLinesBetweenDifferentTextX = (tryParseSuccess == true) ? uRes : 0;
                }
                else
                    newStep.uNumOfLinesBetweenDifferentTextX = 0;

                if (dgvScaleStep[Grid_Col5, rowIndex].Value != null)
                {
                    tryParseSuccess = uint.TryParse(dgvScaleStep[Grid_Col5, rowIndex].Value.ToString(), out uRes);
                    newStep.uNumOfLinesBetweenDifferentTextY = (tryParseSuccess == true) ? uRes : 0;
                }
                else
                    newStep.uNumOfLinesBetweenDifferentTextY = 0;

                if (dgvScaleStep[Grid_Col6, rowIndex].Value != null)
                {
                    tryParseSuccess = uint.TryParse(dgvScaleStep[Grid_Col6, rowIndex].Value.ToString(), out uRes);
                    newStep.uNumOfLinesBetweenSameTextX = (tryParseSuccess == true) ? uRes : 0;
                }
                else
                    newStep.uNumOfLinesBetweenSameTextX = 0;

                if (dgvScaleStep[Grid_Col7, rowIndex].Value != null)
                {
                    tryParseSuccess = uint.TryParse(dgvScaleStep[Grid_Col7, rowIndex].Value.ToString(), out uRes);
                    newStep.uNumOfLinesBetweenSameTextY = (tryParseSuccess == true) ? uRes : 0;
                }
                else
                    newStep.uNumOfLinesBetweenSameTextY = 0;

                if (dgvScaleStep[Grid_Col8, rowIndex].Value != null)
                {
                    tryParseSuccess = uint.TryParse(dgvScaleStep[Grid_Col8, rowIndex].Value.ToString(), out uRes);
                    newStep.uNumMetricDigitsToTruncate = (tryParseSuccess == true) ? uRes : 0;
                }
                else
                    newStep.uNumMetricDigitsToTruncate = 0;

                if (dgvScaleStep[Grid_Col9, rowIndex].Value != null)
                {
                    newStep.eAngleValuesFormat = (dgvScaleStep[Grid_Col9, rowIndex].Value.ToString() != "") ? (DNEAngleFormat)Enum.Parse(typeof(DNEAngleFormat), dgvScaleStep[Grid_Col9, rowIndex].Value.ToString()) : DNEAngleFormat._EAF_DECIMAL_DEG;
                }
                else
                    newStep.eAngleValuesFormat = DNEAngleFormat._EAF_DECIMAL_DEG;

                scaleStepList.Add(newStep);
                rowIndex++;
            }

            return scaleStepList;
        }
        
        private List<DNSGridRegion> GetGridRegionDGVParams()
        {
            List<DNSGridRegion> gridRegionList = new List<DNSGridRegion>();
            List<IDNMcLineItem> lineItemList = new List<IDNMcLineItem>();
            List<IDNMcTextItem> textItemList = new List<IDNMcTextItem>();
            DNSGridRegion newRegion = new DNSGridRegion();
            int rowIndex = 0;
            while (!dgvGridRegion.Rows[rowIndex].IsNewRow)
            {
                bool tryParseSuccess;
                double dRes;
                uint uRes;

                if (dgvGridRegion[Grid_Col1, rowIndex].Value != null)
                {
                    newRegion.pGridLine = (dgvGridRegion[Grid_Col1, rowIndex].Value.ToString() == "Line") ? (IDNMcLineItem)dgvGridRegion[Grid_Col1, rowIndex].Tag : null;
                }
                else
                    newRegion.pGridLine = null;

                if (dgvGridRegion[Grid_Col2, rowIndex].Value != null)
                {
                    newRegion.pGridText = (dgvGridRegion[Grid_Col2, rowIndex].Value.ToString() == "Text") ? (IDNMcTextItem)dgvGridRegion[Grid_Col2, rowIndex].Tag : null;
                }
                else
                    newRegion.pGridText = null;

                if (dgvGridRegion[Grid_Col3, rowIndex].Value != null)
                {
                    newRegion.pCoordinateSystem = (IDNMcGridCoordinateSystem)dgvGridRegion[Grid_Col3, rowIndex].Tag;
                }
                else
                    newRegion.pCoordinateSystem = null;

                
                if (dgvGridRegion[Grid_Col4, rowIndex].Value != null)
                {
                    tryParseSuccess = double.TryParse(dgvGridRegion[Grid_Col4, rowIndex].Value.ToString(), out dRes);
                    newRegion.GeoLimit.MaxVertex.x  = (tryParseSuccess == true) ? dRes : 0;
                }
                else
                    newRegion.GeoLimit.MaxVertex.x  = 0;


                if (dgvGridRegion[Grid_Col5, rowIndex].Value != null)
                {
                    tryParseSuccess = double.TryParse(dgvGridRegion[Grid_Col5, rowIndex].Value.ToString(), out dRes);
                    newRegion.GeoLimit.MaxVertex.y = (tryParseSuccess == true) ? dRes : 0;
                }
                else
                    newRegion.GeoLimit.MaxVertex.y = 0;

                
                if (dgvGridRegion[Grid_Col6, rowIndex].Value != null)
                {
                    tryParseSuccess = double.TryParse(dgvGridRegion[Grid_Col6, rowIndex].Value.ToString(), out dRes);
                    newRegion.GeoLimit.MinVertex.x = (tryParseSuccess == true) ? dRes : 0;
                }
                else
                    newRegion.GeoLimit.MinVertex.x = 0;

                if (dgvGridRegion[Grid_Col7, rowIndex].Value != null)
                {
                    tryParseSuccess = double.TryParse(dgvGridRegion[Grid_Col7, rowIndex].Value.ToString(), out dRes);
                    newRegion.GeoLimit.MinVertex.y = (tryParseSuccess == true) ? dRes : 0;
                }
                else
                    newRegion.GeoLimit.MinVertex.y = 0;

                if (dgvGridRegion[Grid_Col8, rowIndex].Value != null)
                {
                    tryParseSuccess = uint.TryParse(dgvGridRegion[Grid_Col8, rowIndex].Value.ToString(), out uRes);
                    newRegion.uFirstScaleStepIndex = (tryParseSuccess == true) ? uRes : 0;
                }
                else
                    newRegion.uFirstScaleStepIndex = 0;

                if (dgvGridRegion[Grid_Col9, rowIndex].Value != null)
                {
                    tryParseSuccess = uint.TryParse(dgvGridRegion[Grid_Col9, rowIndex].Value.ToString(), out uRes);
                    newRegion.uLastScaleStepIndex = (tryParseSuccess == true) ? uRes : 0;
                }
                else
                    newRegion.uLastScaleStepIndex = 0;


                gridRegionList.Add(newRegion);
                rowIndex++;
            }

            return gridRegionList;
        }

        private void FillScaleStepDGV()
        {
            for (int i = 0; i < m_ArrScaleStep.Length; i++)
            {
                dgvScaleStep.Rows.Add();

                dgvScaleStep[Grid_Col0, i].Value = i.ToString();
                dgvScaleStep[Grid_Col1, i].Value = m_ArrScaleStep[i].fMaxScale;
                dgvScaleStep[Grid_Col2, i].Value = m_ArrScaleStep[i].NextLineGap.x;
                dgvScaleStep[Grid_Col3, i].Value = m_ArrScaleStep[i].NextLineGap.y;
                dgvScaleStep[Grid_Col4, i].Value = m_ArrScaleStep[i].uNumOfLinesBetweenDifferentTextX;
                dgvScaleStep[Grid_Col5, i].Value = m_ArrScaleStep[i].uNumOfLinesBetweenDifferentTextY;
                dgvScaleStep[Grid_Col6, i].Value = m_ArrScaleStep[i].uNumOfLinesBetweenSameTextX;
                dgvScaleStep[Grid_Col7, i].Value = m_ArrScaleStep[i].uNumOfLinesBetweenSameTextY;
                dgvScaleStep[Grid_Col8, i].Value = m_ArrScaleStep[i].uNumMetricDigitsToTruncate;
                dgvScaleStep[Grid_Col9, i].Value = m_ArrScaleStep[i].eAngleValuesFormat.ToString();

            }
        }

        private int Grid_Col0 = 0;
        private int Grid_Col1 = 1;
        private int Grid_Col2 = 2;
        private int Grid_Col3 = 3;
        private int Grid_Col4 = 4;
        private int Grid_Col5 = 5;
        private int Grid_Col6 = 6;
        private int Grid_Col7 = 7;
        private int Grid_Col8 = 8;
        private int Grid_Col9 = 9;
        
        private void FillGridRegionDGV()
        {
            for (int i = 0; i < m_ArrGridRegion.Length; i++)
            {
                dgvGridRegion.Rows.Add();
                dgvGridRegion[Grid_Col0, i].Value = i.ToString();

                dgvGridRegion[Grid_Col1, i].Tag = m_ArrGridRegion[i].pGridLine;
                if (dgvGridRegion[Grid_Col1, i].Tag == null)
                    dgvGridRegion[Grid_Col1, i].Value = "Null";
                else
                    dgvGridRegion[Grid_Col1, i].Value = "Line";

                dgvGridRegion[Grid_Col2, i].Tag = m_ArrGridRegion[i].pGridText;
                if (dgvGridRegion[Grid_Col2, i].Tag == null)
                    dgvGridRegion[Grid_Col2, i].Value = "Null";
                else
                    dgvGridRegion[Grid_Col2, i].Value = "Text";

                dgvGridRegion[Grid_Col3, i].Tag = m_ArrGridRegion[i].pCoordinateSystem;
                if (dgvGridRegion[Grid_Col3, i].Tag == null)
                    dgvGridRegion[Grid_Col3, i].Value = "Null";
                else
                {
                    string zone = "";
                    
                    if (m_ArrGridRegion[i].pCoordinateSystem.IsMultyZoneGrid())
                        zone = "(" + m_ArrGridRegion[i].pCoordinateSystem.GetZone().ToString() + ")";
                    dgvGridRegion[Grid_Col3, i].Value = m_ArrGridRegion[i].pCoordinateSystem.GetGridCoorSysType().ToString() + zone;
                }

                dgvGridRegion[Grid_Col4, i].Value = m_ArrGridRegion[i].GeoLimit.MaxVertex.x;
                dgvGridRegion[Grid_Col5, i].Value = m_ArrGridRegion[i].GeoLimit.MaxVertex.y;
                dgvGridRegion[Grid_Col6, i].Value = m_ArrGridRegion[i].GeoLimit.MinVertex.x;
                dgvGridRegion[Grid_Col7, i].Value = m_ArrGridRegion[i].GeoLimit.MinVertex.y;
                dgvGridRegion[Grid_Col8, i].Value = m_ArrGridRegion[i].uFirstScaleStepIndex;
                dgvGridRegion[Grid_Col9, i].Value = m_ArrGridRegion[i].uLastScaleStepIndex;
                
                                
            }
        }

          private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnApply_Click(sender, e);
            this.Hide();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            List<DNSGridRegion> m_lGridRegion;
            List<DNSScaleStep> m_lScaleStep;
            //Save the last parameters changes before approval
            m_lScaleStep = GetScaleStepDGVParams();
            m_lGridRegion = GetGridRegionDGVParams();
            
            try
            {
                m_CurrObject.SetGridRegions(m_lGridRegion.ToArray());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetGridRegions", McEx);
            }

            try
            {
                m_CurrObject.SetScaleSteps(m_lScaleStep.ToArray());
            }
            catch (MapCoreException McEx)
            {
            	MapCore.Common.Utilities.ShowErrorMessage("SetScaleSteps", McEx);
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        
        private void SetItem(int i, int col, IDNMcSymbolicItem item, string text)
        {
            dgvGridRegion[col, i].Tag = item;
            if (dgvGridRegion[col, i].Tag == null)
                dgvGridRegion[col, i].Value = "Null";
            else
                dgvGridRegion[col, i].Value = text;
        }

        private void dgvGridRegion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Grid_Col1 && dgvGridRegion[Grid_Col1, e.RowIndex].ReadOnly == false)
            {
                IDNMcLineItem CurrLine = null;
                if (dgvGridRegion[Grid_Col1, e.RowIndex].Tag != null)
                    CurrLine = (IDNMcLineItem)dgvGridRegion[Grid_Col1, e.RowIndex].Tag;

                frmGridRegionLine GridRegionLineForm = new frmGridRegionLine(CurrLine);
                if (GridRegionLineForm.ShowDialog() == DialogResult.OK)
                {
                    if (GridRegionLineForm.SetAll)
                    {
                        for (int i = 0; i < dgvGridRegion.RowCount; i++)
                        {
                            if (!dgvGridRegion.Rows[i].IsNewRow)
                            {
                                SetItem(i, Grid_Col1, GridRegionLineForm.CurrLine, "Line");
                            }
                        }
                    }
                    else
                    {
                        SetItem(e.RowIndex, Grid_Col1, GridRegionLineForm.CurrLine, "Line");
                    }
                }
            }
            if (e.ColumnIndex == Grid_Col2 && dgvGridRegion[Grid_Col2, e.RowIndex].ReadOnly == false)
            {
                IDNMcTextItem CurrText = null;
                if (dgvGridRegion[Grid_Col2, e.RowIndex].Tag != null)
                    CurrText = (IDNMcTextItem)dgvGridRegion[Grid_Col2, e.RowIndex].Tag;

                frmGridRegionText GridRegionTextForm = new frmGridRegionText(CurrText);
                if (GridRegionTextForm.ShowDialog() == DialogResult.OK)
                {
                    if (GridRegionTextForm.SetAll)
                    {
                        for (int i = 0; i < dgvGridRegion.RowCount; i++)
                        {
                            if (!dgvGridRegion.Rows[i].IsNewRow)
                            {
                                SetItem(i, Grid_Col2, GridRegionTextForm.CurrText, "Text");
                            }
                        }
                    }
                    else
                    {
                        SetItem(e.RowIndex, Grid_Col2, GridRegionTextForm.CurrText, "Text");
                    }
                    
                }
            }

            if (e.ColumnIndex == Grid_Col3 && dgvGridRegion[Grid_Col3, e.RowIndex].ReadOnly == false)
            {
                IDNMcGridCoordinateSystem mcGridCoordinateSystem = null;
                frmNewGridCoordinateSystem NewGridCoordinateSystemForm = null;

                if (dgvGridRegion[Grid_Col3, e.RowIndex].Tag != null)
                {
                    mcGridCoordinateSystem = (IDNMcGridCoordinateSystem)dgvGridRegion[Grid_Col3, e.RowIndex].Tag;
                    NewGridCoordinateSystemForm = new frmNewGridCoordinateSystem(mcGridCoordinateSystem, true);
                    NewGridCoordinateSystemForm.closeDialogResult = DialogResult.Cancel;
                }
                else
                {
                    NewGridCoordinateSystemForm = new frmNewGridCoordinateSystem();
                    NewGridCoordinateSystemForm.closeDialogResult = DialogResult.Cancel;

                }
                if (NewGridCoordinateSystemForm.ShowDialog() == DialogResult.OK)
                {

                    if (NewGridCoordinateSystemForm.NewGridCoordinateSystem == null)
                        dgvGridRegion[Grid_Col3, e.RowIndex].Value = null;
                    else
                    {
                        string zone = "";
                        if (NewGridCoordinateSystemForm.NewGridCoordinateSystem.IsMultyZoneGrid())
                            zone = "(" + NewGridCoordinateSystemForm.NewGridCoordinateSystem.GetZone().ToString() + ")";
                        dgvGridRegion[Grid_Col3, e.RowIndex].Value = NewGridCoordinateSystemForm.NewGridCoordinateSystem.GetGridCoorSysType() + zone;
                    }
                    dgvGridRegion[Grid_Col3, e.RowIndex].Tag = NewGridCoordinateSystemForm.NewGridCoordinateSystem;

                }

               
            }
        }

        private void dgvGridRegion_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void dgvScaleStep_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
