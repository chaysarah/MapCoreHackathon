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
using MCTester.Managers;

namespace MCTester.General_Forms
{
    public partial class frmGeometricCalcCirclesList : Form
    {
        private List<IDNMcEllipseItem> m_lCircles;
        private IDNMcEllipseItem [] m_InputCircles = null;
        private List<DNSMcVector3D> m_lCircleLocationPoint;
        private DNSMcVector3D [] m_CircleLocationPoint = null;
        
        public frmGeometricCalcCirclesList()
        {
            InitializeComponent();
            m_lCircles = new List<IDNMcEllipseItem>();
            m_lCircleLocationPoint = new List<DNSMcVector3D>(); 
        }

        private void frmGeometricCalcCirclesList_Load(object sender, EventArgs e)
        {
            IDNMcOverlay[] overlays = MCTMapFormManager.MapForm.Viewport.OverlayManager.GetOverlays();
            foreach (IDNMcOverlay overlay in overlays)
            {
                IDNMcObject[] objects = overlay.GetObjects();
                foreach (IDNMcObject obj in objects)
                {
                    IDNMcObjectScheme scheme = obj.GetScheme();
                    IDNMcObjectSchemeNode[] nodes = scheme.GetNodes(DNENodeKindFlags._ENKF_SYMBOLIC_ITEM);
                    
                    foreach (IDNMcObjectSchemeNode node in nodes)
                    {
                        if (node.GetNodeType() == DNEObjectSchemeNodeType._ELLIPSE_ITEM)
                        {
                            if (((IDNMcEllipseItem)node).GetEllipseDefinition() == DNEEllipseDefinition._EED_CIRCLE_CENTER_POINT_ANGLES || ((IDNMcEllipseItem)node).GetEllipseDefinition() == DNEEllipseDefinition._EED_CIRCLE_CENTER_RADIUS_ANGLES)
                            {
                                clstCircles.Items.Add(Manager_MCNames.GetNameByObject(node,"Circle"));
                                m_lCircles.Add((IDNMcEllipseItem)node);
                                m_lCircleLocationPoint.Add(obj.GetLocationPoints(0)[0]);  
                            }
                        }
                    }
                }
            }
        }

        public IDNMcEllipseItem [] InputCircles
        {
            get { return m_InputCircles; }
            set { m_InputCircles = value; }
        }

        public List<DNSMcVector3D> lCircleLocationPoint
        {
            get { return m_lCircleLocationPoint; }
            set { m_lCircleLocationPoint = value; }
        }

        public DNSMcVector3D [] CircleLocationPoint
        {
            get { return m_CircleLocationPoint; }
            set { m_CircleLocationPoint = value; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_InputCircles = new IDNMcEllipseItem[clstCircles.CheckedItems.Count];
            m_CircleLocationPoint = new DNSMcVector3D[clstCircles.CheckedItems.Count];
            int idx = 0;

            for (int i = 0; i < clstCircles.Items.Count; i++ )
            {
                if (clstCircles.GetItemChecked(i))
                {
                    InputCircles[idx] = m_lCircles[i];
                    m_CircleLocationPoint[idx] = lCircleLocationPoint[i];
                    m_lCircles[i].SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    m_lCircles[i].SetFillColor(new DNSMcBColor(0, 255, 0, 125), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                    idx++;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
