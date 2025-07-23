using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers.VectorialWorld;
using MCTester.Managers;

namespace MCTester.General_Forms
{
    public partial class frmVectorial : Form
    {
        private IDNMcMapProduction m_MapProduction;
        private int m_MapProductionIndex;
         
        private enum m_MapProductionAction
        {
            Add,
            Remove,
            Previous,
            Next,
            Load
        }

        public frmVectorial()
        {
            InitializeComponent();

            if (Manager_MCVectorial.lMapProductionParams.Count > 0)
            {
                m_MapProductionIndex = 0;
                m_MapProduction = Manager_MCVectorial.lMapProductionParams[0].MapProduction;
            }
            else
                m_MapProductionIndex = -1;


        }

        private void frmVectorial_Load(object sender, EventArgs e)
        {
            updateMapProductionGroupBox(m_MapProductionAction.Load);
        }
      
        private void LoadFormParams()
        {            
            ctrlBrowseVectorLayer.FileName = "";
            ntxVectorLayerMaxScale.SetFloat(0);
            ntxVectorLayerMinScale.SetFloat(0);
        }

        private void btnPrvMapProduction_Click(object sender, EventArgs e)
        {
            updateMapProductionGroupBox(m_MapProductionAction.Previous);            
        }

        private void btnNxtMapProduction_Click(object sender, EventArgs e)
        {
            updateMapProductionGroupBox(m_MapProductionAction.Next);
        }
  
        private void btnGetVectorLayerMinMaxScale_Click(object sender, EventArgs e)
        {
            if (m_MapProduction != null)
            {
                try
                {
                    float minScale, maxScale;
                    m_MapProduction.GetVectorLayerMinMaxScale(ctrlBrowseVectorLayer.FileName, out minScale, out maxScale);
                    ntxVectorLayerMaxScale.SetFloat(maxScale);
                    ntxVectorLayerMinScale.SetFloat(minScale);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetVectorLayerMinMaxScale", McEx);
                }
            }
            else
                MessageBox.Show("You have to create Map Production first!", "No Map Production", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMapProductionCreate_Click(object sender, EventArgs e)
        {
            Manager_MCVectorial.lMapProductionParams.Add(new MapProductionParams());

            updateMapProductionGroupBox(m_MapProductionAction.Add);            
        }

        private void btnMapProductionRemove_Click(object sender, EventArgs e)
        {
            if (Manager_MCVectorial.lMapProductionParams.Count > 0)
            {
				MapProductionParams mapProductionParamsTMP = Manager_MCVectorial.lMapProductionParams[m_MapProductionIndex];
                Manager_MCVectorial.lMapProductionParams.Remove(Manager_MCVectorial.lMapProductionParams[m_MapProductionIndex]);
				mapProductionParamsTMP.MapProduction.Dispose();

                updateMapProductionGroupBox(m_MapProductionAction.Remove);
            }
        }

        private void updateMapProductionGroupBox(m_MapProductionAction index)
        {
            switch (index)
            {
                case m_MapProductionAction.Add:
                    m_MapProductionIndex += 1;
                    break;
                case m_MapProductionAction.Load:
                    break;
                case m_MapProductionAction.Next:
                    if (m_MapProductionIndex < Manager_MCVectorial.lMapProductionParams.Count - 1)
                        m_MapProductionIndex += 1;                    
                    break;
                case m_MapProductionAction.Previous:
                    if (m_MapProductionIndex > 0)
                    {
                        m_MapProductionIndex -= 1;
                    }
                    break;
                case m_MapProductionAction.Remove:
                    if (m_MapProductionIndex == Manager_MCVectorial.lMapProductionParams.Count || Manager_MCVectorial.lMapProductionParams.Count == 0)
                        m_MapProductionIndex -= 1;
                    break;
            }

            gbMapProductionParams.Text = "Map Production Params   (" + (m_MapProductionIndex + 1).ToString() + "/" + Manager_MCVectorial.lMapProductionParams.Count.ToString() + ")";

            if (m_MapProductionIndex >= 0)
                m_MapProduction = Manager_MCVectorial.lMapProductionParams[m_MapProductionIndex].MapProduction;                
            else
                m_MapProduction = null;

            LoadFormParams();
            
            
        }
    }
}