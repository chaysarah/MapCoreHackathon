using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucRasterLayer : ucLayer, IUserControlItem
    {
        private IDNMcRasterMapLayer m_CurrentObject;

        public ucRasterLayer()
        {
            InitializeComponent();
        }

        protected override void SaveItem()
        {
            base.SaveItem();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcRasterMapLayer)aItem;
            base.LoadItem(aItem);
        }

        #endregion

        private void btnCalcHistogram_Click(object sender, EventArgs e)
        {
            txtCalcHistogramR.Text = "";
            txtCalcHistogramG.Text = "";
            txtCalcHistogramB.Text = "";

            Int64[][] histogram = null;
            try
            {
                m_CurrentObject.CalcHistogram(out histogram);

                if (histogram != null)
                {
                    for (int i = 0; i < 256; i++)
                    {
                        txtCalcHistogramR.Text += histogram[0][i] + ",";
                        txtCalcHistogramG.Text += histogram[1][i] + ",";
                        txtCalcHistogramB.Text += histogram[2][i] + ",";
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("CalcHistogram", McEx);
            }
        }
    }
}
