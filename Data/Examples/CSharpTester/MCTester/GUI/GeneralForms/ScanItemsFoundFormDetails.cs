using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;
using MapCore.Common;
using MCTester.GUI.Forms;
using MCTester.GUI.Trees;

namespace MCTester.General_Forms
{
    public partial class ScanItemsFoundFormDetails : Form
    {
        DNSTargetFound m_ItemFound;

        public ScanItemsFoundFormDetails()
        {
            InitializeComponent();
            ctrlUnifiedVectorItemsPoints.ChangeReadOnly();
        }

        public ScanItemsFoundFormDetails(DNSTargetFound itemFound, ScanTargetFound scanTargetFound, DNSVectorItemFound[] VectorItems, DNSMcVector3D[] unifiedVectorItemsPoints, IDNMcMapViewport mapViewport) : this()
        {
            m_ItemFound = itemFound;
            if (scanTargetFound != null)
            {
                tbTargetId.Text = scanTargetFound.TargetID;
            }

            if (VectorItems != null)
            {
                for (int i = 0; i < VectorItems.Length; i++)
                {
                    dgvVectorItems.Rows.Add();
                    dgvVectorItems[0, i].Value = i.ToString();
                    dgvVectorItems[1, i].Value = (VectorItems[i].uVectorItemID == DNMcConstants.MC_EXTRA_CONTOUR_VECTOR_ITEM_ID) ? "HOLE" : VectorItems[i].uVectorItemID.ToString();
                    dgvVectorItems[2, i].Value = VectorItems[i].uVectorItemFirstPointIndex;
                    dgvVectorItems[3, i].Value = VectorItems[i].uVectorItemLastPointIndex;
                }
            }
            else
                dgvVectorItems.Enabled = false;

            if (unifiedVectorItemsPoints != null && unifiedVectorItemsPoints.Length > 0)
            {
                ctrlUnifiedVectorItemsPoints.SetPoints(unifiedVectorItemsPoints);
                ntxNumLocationPoints.SetInt(unifiedVectorItemsPoints.Length);
            }
        }

    }
}
