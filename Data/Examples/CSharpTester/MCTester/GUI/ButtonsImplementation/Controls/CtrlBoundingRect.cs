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

namespace MCTester.Controls
{
    public partial class CtrlBoundingRect : UserControl
    {
        private IDNMcObjectSchemeNode m_SchemeNode;
        private IDNMcObject m_RectObject;
        private bool m_IsScreenBoundingBox = true;

        public CtrlBoundingRect()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(CtrlBoundingRect_Disposed);
            this.VisibleChanged += new EventHandler(CtrlBoundingRect_VisibleChanged);
            if (m_RectObject != null)
            {
                m_RectObject.Remove();
                m_RectObject.Dispose();
            }
            
        }

        void CtrlBoundingRect_VisibleChanged(object sender, EventArgs e)
        {
            if (m_RectObject != null)
            {
                m_RectObject.Remove();
                m_RectObject.Dispose();
            }
        }

        void CtrlBoundingRect_Disposed(object sender, EventArgs e)
        {
            if (m_RectObject != null)
            {
                m_RectObject.Remove();
                m_RectObject.Dispose();
            }
        }

        private void btnGetBoundingBox_Click(object sender, EventArgs e)
        {
            frmViewportObjectLists ScreenBoundingRectForm = new frmViewportObjectLists(BoundingRectSchemeNode);
            if (ScreenBoundingRectForm.ShowDialog() == DialogResult.OK)
            {
                DNSMcBox box;
                IDNMcObjectSchemeItem ObjSchemeItem = null;
                DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];
                if (IsScreenBoundingBox == true)
                {
                    try
                    {
                        BoundingRectSchemeNode.GetScreenBoundingRect(ScreenBoundingRectForm.SelectedViewport,
                                                                        ScreenBoundingRectForm.SelectedObject,
                                                                        out box);

                        locationPoints = new DNSMcVector3D[2];
                        locationPoints[0] = new DNSMcVector3D(box.MinVertex.x, box.MinVertex.y, box.MinVertex.z);
                        locationPoints[1] = new DNSMcVector3D(box.MaxVertex.x, box.MaxVertex.y, box.MaxVertex.z);

                        ObjSchemeItem = DNMcRectangleItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                    DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                    DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT,
                                                                    DNERectangleDefinition._ERD_RECTANGLE_DIAGONAL_POINTS, 
                                                                    0f,
                                                                    0f,
                                                                    DNELineStyle._ELS_DASH,
                                                                    new DNSMcBColor(255, 0, 0, 255),
                                                                    2,
                                                                    null,
                                                                    new DNSMcFVector2D(1, 1),
                                                                    1,
                                                                    DNEFillStyle._EFS_NONE);
                        
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetScreenBoundingRect", McEx);
                        return;
                    }
                }
                else
                {
                    try
                    {
                        BoundingRectSchemeNode.GetWorldBoundingBox(ScreenBoundingRectForm.SelectedViewport,
                                                                        ScreenBoundingRectForm.SelectedObject,
                                                                        out box);

                        locationPoints = new DNSMcVector3D[16];
                        DNSMcVector3D[] boxPoints = new DNSMcVector3D[8];

                        boxPoints[0].x = boxPoints[3].x = boxPoints[4].x = boxPoints[7].x = box.MinVertex.x;
                        boxPoints[1].x = boxPoints[2].x = boxPoints[5].x = boxPoints[6].x = box.MaxVertex.x;

                        boxPoints[0].y = boxPoints[1].y = boxPoints[4].y = boxPoints[5].y = box.MinVertex.y;
                        boxPoints[2].y = boxPoints[3].y = boxPoints[6].y = boxPoints[7].y = box.MaxVertex.y;

                        boxPoints[0].z = boxPoints[1].z = boxPoints[2].z = boxPoints[3].z = box.MinVertex.z;
                        boxPoints[4].z = boxPoints[5].z = boxPoints[6].z = boxPoints[7].z = box.MaxVertex.z;

                        locationPoints[0] = boxPoints[0];
                        locationPoints[1] = boxPoints[1];
                        locationPoints[2] = boxPoints[2];
                        locationPoints[3] = boxPoints[3];
                        locationPoints[4] = boxPoints[0];
                        locationPoints[5] = boxPoints[4];
                        locationPoints[6] = boxPoints[5];
                        locationPoints[7] = boxPoints[1];
                        locationPoints[8] = boxPoints[2];
                        locationPoints[9] = boxPoints[6];
                        locationPoints[10] = boxPoints[7];
                        locationPoints[11] = boxPoints[4];
                        locationPoints[12] = boxPoints[5];
                        locationPoints[13] = boxPoints[6];
                        locationPoints[14] = boxPoints[7];
                        locationPoints[15] = boxPoints[3];

                        ObjSchemeItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                DNELineStyle._ELS_DASH,
                                                                new DNSMcBColor(255,0,0,255),
                                                                2);
                    
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetWorldBoundingBox", McEx);
                        return;
                    }
                }

                ctrl3DVectorMinVertex.SetVector3D( box.MinVertex);
                ctrl3DVectorMaxVertex.SetVector3D( box.MaxVertex);

                if (m_RectObject != null)
                {
                    m_RectObject.Remove();
                    m_RectObject.Dispose();
                }

                DNEMcPointCoordSystem rectCoordSys;
                if (IsScreenBoundingBox == true)
                    rectCoordSys = DNEMcPointCoordSystem._EPCS_SCREEN;
                else
                    rectCoordSys = DNEMcPointCoordSystem._EPCS_WORLD;

                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                m_RectObject = DNMcObject.Create(activeOverlay,
                                                    ObjSchemeItem,
                                                    rectCoordSys,
                                                    locationPoints,
                                                    false);
            }
        }

        public IDNMcObjectSchemeNode BoundingRectSchemeNode
        {
            get { return m_SchemeNode; }
            set { m_SchemeNode = value; }
        }

        public string Title
        {
            get { return gbBoundingRect.Text; }
            set { gbBoundingRect.Text = value; }
        }

        public bool IsScreenBoundingBox
        {
            get { return m_IsScreenBoundingBox; }
            set { m_IsScreenBoundingBox = value; }
        }
    }
}
