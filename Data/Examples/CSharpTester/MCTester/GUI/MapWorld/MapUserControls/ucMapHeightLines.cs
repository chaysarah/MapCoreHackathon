using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.MapWorld.Assist_Forms;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucMapHeightLines : UserControl, IUserControlItem
    {
        private IDNMcMapHeightLines m_CurrObject;
        private List<List<DNSMcBColor>> m_lColors = new List<List<DNSMcBColor>>();

        public ucMapHeightLines()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        void IUserControlItem.LoadItem(object aItem)
        {
            m_CurrObject = (IDNMcMapHeightLines)aItem;

            try
            {
                DNSHeightLinesScaleStep [] scaleStep = m_CurrObject.GetScaleSteps();
                for (int i = 0; i < scaleStep.Length; i++)
                {
                    dgvScaleStep.Rows.Add();

                    dgvScaleStep[0, i].Value = scaleStep[i].fMaxScale;
                    dgvScaleStep[1, i].Value = scaleStep[i].fLineHeightGap;

                    if (scaleStep[i].aColors != null)
                    {
                        List<DNSMcBColor> colorsList = new List<DNSMcBColor>(scaleStep[i].aColors);
                        m_lColors.Add(colorsList);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetScaleSteps", McEx);
            }

            try
            {        
                bool IsColorInterpolationMode;
                float minHeight, maxHeight;
                m_CurrObject.GetColorInterpolationMode(out IsColorInterpolationMode, out minHeight, out maxHeight);

                chxIsColorInterpolationEnabled.Checked = IsColorInterpolationMode;
                ntxMinHeight.SetFloat(minHeight);
                ntxMaxHeight.SetFloat(maxHeight);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetColorInterpolationMode", McEx);
            }

            try
            {            
                float lineWidth;
                m_CurrObject.GetLineWidth(out lineWidth);

                ntxLineWidth.SetFloat(lineWidth);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetLineWidth", McEx);
            }
        }

        #endregion

        private void dgvScaleStep_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                frmHeightLinesScaleStepColorList HeightLinesScaleStepColorListForm;

                //Case that this scale step already defined colors
                if (m_lColors.Count > e.RowIndex)
                {
                    HeightLinesScaleStepColorListForm = new frmHeightLinesScaleStepColorList(m_lColors[e.RowIndex]);
                }
                //Case that this is new scale step
                else
                {
                    HeightLinesScaleStepColorListForm = new frmHeightLinesScaleStepColorList(new List<DNSMcBColor>());
                }

                if (HeightLinesScaleStepColorListForm.ShowDialog() == DialogResult.OK)
                {

                    if (dgvScaleStep.Rows[e.RowIndex].IsNewRow)
                    {
                        dgvScaleStep.Rows.Add();
                        dgvScaleStep[e.ColumnIndex, e.RowIndex].Selected = true;
                    }

                    //add empty rows
                    while (m_lColors.Count < e.RowIndex + 1)
                        m_lColors.Add(new List<DNSMcBColor>());
                    
                    m_lColors[e.RowIndex] = HeightLinesScaleStepColorListForm.ColorList;
                  
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {      
                int i = 0;
                DNSHeightLinesScaleStep[] ArrScaleStep = new DNSHeightLinesScaleStep[dgvScaleStep.Rows.Count-1];

                while (dgvScaleStep.Rows[i].IsNewRow == false)
                {
                    if(dgvScaleStep[0, i].Value != null)
                        ArrScaleStep[i].fMaxScale = float.Parse(dgvScaleStep[0, i].Value.ToString());
                    if (dgvScaleStep[1, i].Value != null)
                        ArrScaleStep[i].fLineHeightGap = float.Parse(dgvScaleStep[1, i].Value.ToString());

                    if (i < m_lColors.Count && m_lColors[i].Count > 0)
                    {
                        DNSMcBColor[] colors = new DNSMcBColor[m_lColors[i].Count];
                        m_lColors[i].CopyTo(colors);
                        ArrScaleStep[i].aColors = colors;
                    }
                    i++;
                }

                m_CurrObject.SetScaleSteps(ArrScaleStep);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetScaleSteps", McEx);
            }

            try
            {
                m_CurrObject.SetColorInterpolationMode(chxIsColorInterpolationEnabled.Checked,
                                                        ntxMinHeight.GetFloat(),
                                                        ntxMaxHeight.GetFloat());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetColorInterpolationMode", McEx);
            }

            try
            {
                m_CurrObject.SetLineWidth(ntxLineWidth.GetFloat());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetLineWidth", McEx);
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        private void chxIsColorInterpolationEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chxIsColorInterpolationEnabled.Checked == true)
            {
                ntxMaxHeight.Enabled = true;
                ntxMinHeight.Enabled = true;
            }
            else
            {
                ntxMaxHeight.Enabled = false;
                ntxMinHeight.Enabled = false;
            }
        }

        private void dgvScaleStep_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if(m_lColors.Count > e.Row.Index)
                m_lColors.RemoveAt(e.Row.Index);
        }
    }
}
