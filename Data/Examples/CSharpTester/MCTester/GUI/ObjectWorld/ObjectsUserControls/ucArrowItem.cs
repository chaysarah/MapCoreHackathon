using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ucArrowItem : ucClosedShapeItem, IUserControlItem
    {
        private IDNMcArrowItem m_CurrentObject;
        
        public ucArrowItem():base()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcArrowItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);

            base.LoadItem(aItem);
         
            try
            {
                txtArrowCoordinateSystem.Text = m_CurrentObject.GetArrowCoordinateSystem().ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetArrowCoordinateSystem", McEx);
            }

            try
            {
                ctrlObjStatePropertyHeadSize.Load(m_CurrentObject.GetHeadSize);
            }
            catch (MapCoreException McEx)
            {
				MapCore.Common.Utilities.ShowErrorMessage("GetHeadSize", McEx);
            }

            try
            {
                ctrlObjStatePropertyHeadAngle.Load( m_CurrentObject.GetHeadAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetHeadAngle", McEx);
            }

            try
            {
                ctrlObjStatePropertyGapSize.Load(m_CurrentObject.GetGapSize);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetGapSize", McEx);
            }

            try
            {
                ctrlObjStatePropertyShowSlopePresentation.Load(m_CurrentObject.GetShowSlopePresentation);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetShowSlopePresentation", McEx);
            }

            try
            {
                DNSSlopePresentationColor[] slopePresentationColors = m_CurrentObject.GetSlopePresentationColors();

                if (slopePresentationColors != null)
                {
                    DNSMcBColor currColor = new DNSMcBColor();

                    for (int i = 0; i < slopePresentationColors.Length; i++)
                    {
                        dgvColors.Rows.Add();
                        currColor = slopePresentationColors[i].Color;

                        dgvColors[0, i].Value = "R: " + currColor.r.ToString() +
                                                ",  G: " + currColor.g.ToString() +
                                                ",  B: " + currColor.b.ToString() +
                                                ",  A: " + currColor.a.ToString();

                        dgvColors[1, i].Style.BackColor = Color.FromArgb((int)currColor.a, (int)currColor.r, (int)currColor.g, (int)currColor.b);
                        dgvColors[2, i].Value = slopePresentationColors[i].fMaxSlope;
                    }
                }
            }   
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetSlopePresentationColors", McEx);
            }

            try
            {
                ctrlObjStatePropertySlopeQueryPrecision.Load(m_CurrentObject.GetSlopeQueryPrecision);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetSlopeQueryPrecision", McEx);
            }

            try
            {
                ctrlShowTraversabilityPresentation.Load(m_CurrentObject.GetShowTraversabilityPresentation);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetShowTraversabilityPresentation", McEx);
            }

            try
            {
                ctrlObjStatePropertyDictionaryTraversabilityColor1.Load(m_CurrentObject.GetTraversabilityColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTraversabilityColor", McEx);
            }
        }

        #endregion

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);

            base.SaveItem();

            try
            {
                ctrlObjStatePropertyHeadSize.Save(m_CurrentObject.SetHeadSize);
            }
            catch (MapCoreException McEx)
            {
				MapCore.Common.Utilities.ShowErrorMessage("SetHeadSize", McEx);
            }

            try
            {
                ctrlObjStatePropertyHeadAngle.Save(m_CurrentObject.SetHeadAngle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetHeadAngle", McEx);
            }

            try
            {
                ctrlObjStatePropertyGapSize.Save(m_CurrentObject.SetGapSize);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetGapSize", McEx);
            }

            try
            {
                ctrlObjStatePropertyShowSlopePresentation.Save(m_CurrentObject.SetShowSlopePresentation);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetShowSlopePresentation", McEx);
            }

            try
            {
                List<DNSSlopePresentationColor> lSlopPresentationColor = new List<DNSSlopePresentationColor>();
                DNSSlopePresentationColor currSlop = new DNSSlopePresentationColor();

                if (dgvColors.Rows.Count > 1)
                {
                    for (int i = 0; i < dgvColors.Rows.Count - 1; i++)
                    {
                        DNSMcBColor color = new DNSMcBColor(dgvColors[1, i].Style.BackColor.R, dgvColors[1, i].Style.BackColor.G, dgvColors[1, i].Style.BackColor.B, dgvColors[1, i].Style.BackColor.A);
                        currSlop.Color = color;
                        currSlop.fMaxSlope = float.Parse(dgvColors[2,i].Value.ToString());

                        lSlopPresentationColor.Add(currSlop);
                    }
                }

                m_CurrentObject.SetSlopePresentationColors(lSlopPresentationColor.ToArray());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSlopePresentationColors", McEx);
            }

            try
            {
                ctrlObjStatePropertySlopeQueryPrecision.Save(m_CurrentObject.SetSlopeQueryPrecision);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSlopeQueryPrecision", McEx);
            }

            try
            {
                ctrlShowTraversabilityPresentation.Save(m_CurrentObject.SetShowTraversabilityPresentation);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetShowTraversabilityPresentation", McEx);
            }

            try
            {
                ctrlObjStatePropertyDictionaryTraversabilityColor1.Save(m_CurrentObject.SetTraversabilityColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTraversabilityColor", McEx);
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        private void dgvColors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                ColorDialog dlgColor = new ColorDialog();
                if (dlgColor.ShowDialog() == DialogResult.OK)
                {
                    //Add row to table
                    if (dgvColors.Rows[e.RowIndex].IsNewRow == true)
                    {
                        dgvColors.Rows.Insert(dgvColors.Rows.GetLastRow(DataGridViewElementStates.None), 1);
                    }


                    dgvColors[0, e.RowIndex].Value = "R: " + dlgColor.Color.R.ToString() +
                                                        ",  G: " + dlgColor.Color.G.ToString() +
                                                        ",  B: " + dlgColor.Color.B.ToString() +
                                                        ",  A: " + dlgColor.Color.A.ToString();

                    dgvColors[1, e.RowIndex].Style.BackColor = dlgColor.Color;
                }
            }
        }   
    }
}
