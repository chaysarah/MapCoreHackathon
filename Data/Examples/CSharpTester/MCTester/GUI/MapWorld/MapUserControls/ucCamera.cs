using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using MCTester.Managers;
using MCTester.GUI.Map;


namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucCamera : UserControl,IUserControlItem
    {
        private IDNMcMapCamera m_CurrentObject;
        private IDNMcMapViewport m_CurrentViewport;
        private bool CtrlLoadedFirsTime;  

        public ucCamera()
        {
            InitializeComponent();
            CtrlLoadedFirsTime = true;

            cmbScreenVisibleAreaOperation.Items.AddRange(Enum.GetNames(typeof(DNESetVisibleArea3DOperation)));
            cmbScreenVisibleAreaOperation.Text = DNESetVisibleArea3DOperation._ESVAO_ROTATE_AND_MOVE.ToString();


        }
        
        private void SetIC(IDNMcMapViewport CurrViewport)
        {
            ctrWorldPt.PointCoordSystem = DNEMcPointCoordSystem._EPCS_IMAGE;
            ctrlCameraPositionPt.PointCoordSystem = DNEMcPointCoordSystem._EPCS_IMAGE;
            boxWorldVisibleArea.SetPointData(CurrViewport);
            ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt.PointCoordSystem = DNEMcPointCoordSystem._EPCS_IMAGE;
            ctrlLookAtPt.PointCoordSystem = DNEMcPointCoordSystem._EPCS_IMAGE;
            label5.Text = "World (Image)";
            lblToIC.Visible = true;
        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcMapCamera)aItem;

            IDNMcMapViewport tempVp = null;
            if (aItem is IDNMcMapViewport)
            {
                m_CurrentViewport = (IDNMcMapViewport)aItem;
                tempVp = m_CurrentViewport;
            }
            else
            {
                foreach(MCTMapForm mapForm in MCTMapFormManager.AllMapForms)
                {
                    if (tempVp != null) break;
                    IDNMcMapCamera[] cameras = mapForm.Viewport.GetCameras();
                    foreach(IDNMcMapCamera camera in cameras)
                    {
                        if(camera == m_CurrentObject)
                        {
                            tempVp = mapForm.Viewport;
                            break;
                        }
                    }
                }
            }
            if (tempVp != null)
            {
                if(tempVp.GetImageCalc() != null)
                    SetIC(tempVp);
                else
                    boxWorldVisibleArea.SetPointData(tempVp);
            }


            pnlCameraScale3D.Enabled = m_CurrentObject.MapType == DNEMapType._EMT_3D;

            //Only for 2D map type
            if (m_CurrentObject.MapType==DNEMapType._EMT_2D)
            {
                try
                {
                    ntxCameraScale.SetFloat(m_CurrentObject.GetCameraScale());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCameraScale()", McEx);
                }
                if (!chxOrientationRelative.Checked)
                {
                    try
                    {
                        float yaw;
                        m_CurrentObject.GetCameraOrientation(out yaw);

                        ctrlCameraOrientation.Yaw = yaw;
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetCameraOrientation", McEx);
                    }
                }
            }

            //Only for 3D map type
            if (m_CurrentObject.MapType==DNEMapType._EMT_3D)
            {
                try
                {
                    ntxFieldOfView.SetFloat(m_CurrentObject.GetCameraFieldOfView());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("CameraFieldOfView", McEx);
                }

                try
                {
                    bool parseResultX, parseResultY, parseResultZ;
                    double resultX,resultY, resultZ;

                    parseResultX = double.TryParse(Manager_StatusBar.WorldCoordX.Text, out resultX );
                    parseResultY = double.TryParse(Manager_StatusBar.WorldCoordY.Text, out resultY);
                    parseResultZ = double.TryParse(Manager_StatusBar.WorldCoordZ.Text, out resultZ);

                    if (parseResultX == true && parseResultY == true && parseResultZ == true)
                    {
                        ctrl3DCameraScaleWorldPoint.X = resultX;
                        ctrl3DCameraScaleWorldPoint.Y = resultY;
                        ctrl3DCameraScaleWorldPoint.Z = resultZ;


                        DNSMcVector3D scaleWorldPoint = ctrl3DCameraScaleWorldPoint.GetVector3D();
                        ntxCameraScale.SetFloat(m_CurrentObject.GetCameraScale(scaleWorldPoint));
                    }
                                        
                   
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCameraScale", McEx);
                }

                try
                {
                    bool enableRelative = false;
                    double minHeight, maxHeight;
                    m_CurrentObject.GetCameraRelativeHeightLimits(out minHeight, out maxHeight, out enableRelative);
                    ntxMinRelativeHeightLimits.SetDouble(minHeight);
                    ntxMaxRelativeHeightLimits.SetDouble(maxHeight);
                    chxCameraRelativeHeightLimits.Checked = enableRelative;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCameraRelativeHeightLimits", McEx);
                }

                try
                {
                    float minDistances, maxDistances;
                    bool twoSessionsRender;
                    m_CurrentObject.GetCameraClipDistances(out minDistances, out maxDistances,out twoSessionsRender);
                    ntxMinCameraClipDistances.SetFloat(minDistances);
                    ntxMaxCameraClipDistances.SetFloat(maxDistances);
                    chxRenderInTwoSessions.Checked = twoSessionsRender;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCameraClipDistances", McEx);
                }
                if (!chxOrientationRelative.Checked)
                {
                    try
                    {
                        float yaw, pitch, roll;
                        m_CurrentObject.GetCameraOrientation(out yaw, out pitch, out roll);

                        ctrlCameraOrientation.Yaw = yaw;
                        ctrlCameraOrientation.Pitch = pitch;
                        ctrlCameraOrientation.Roll = roll;

                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetCameraOrientation", McEx);
                    }
                }

                try
                {
                    DNSMcVector3D FwrVector = m_CurrentObject.GetCameraForwardVector();
                    ctrl3DForwardVector.SetVector3D( new DNSMcVector3D(FwrVector.x, FwrVector.y, FwrVector.z));
                    //ctrl3DForwardVector.SetVector3D( new DNSMcVector3D(Math.Round(FwrVector.x,10), FwrVector.y, FwrVector.z);

                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCameraForwardVector", McEx);
                }
            }

            //For any map type
            
            try
            {
                txtMapType.Text = m_CurrentObject.MapType.ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("MapType", McEx);
            }
            if (!chxPositionRelative.Checked)
            {
                try
                {
                    ctrl3DCameraPosition.SetVector3D( m_CurrentObject.GetCameraPosition());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCameraPosition", McEx);
                }
            }
            try
            {
                ctrl3DCameraUpVector.SetVector3D( m_CurrentObject.GetCameraUpVector());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetCameraUpVector", McEx);
            }

            btnGetWorldVisibleArea_Click(null, null);

            /*if (m_CurrentObject.MapType == DNEMapType._EMT_2D)  //Relevant for 2D and 3D - Currently implemented in MapCore only for 2D
            {
                try
                {
                    DNSMcBox worldVisibleArea = m_CurrentObject.GetCameraWorldVisibleArea(ntxWorldVisibleAreaMargin.GetInt32(),ntxRectangleYaw.GetFloat());
                    boxWorldVisibleArea.SetBoxValue(worldVisibleArea);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCameraWorldVisibleArea", McEx);
                }
            }*/

            try
            {
                ctrl2DFVectorCenterOffset.SetVector2D(m_CurrentObject.GetCameraCenterOffset());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetCameraCenterOffset", McEx);
            }            
        }
                        
        #endregion

        private void btnConWorldToScreen_Click(object sender, EventArgs e)
        {
            try
            {
                ctrl3DCameraConScreenPoint.SetVector3D( m_CurrentObject.WorldToScreen(ctrl3DCameraConWorldPoint.GetVector3D()));
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("WorldToScreen", McEx);
            }
        }        

        private void btnConScreenToWorldOnTerrain_Click(object sender, EventArgs e)
        {
            try
            {
                DNSMcVector3D worldPoint;
                bool intersection = false;
                m_CurrentObject.ScreenToWorldOnTerrain(ctrl3DCameraConScreenPoint.GetVector3D(), out worldPoint, out intersection);
                ctrl3DCameraConWorldPoint.SetVector3D( worldPoint);
                chxIsIntersectionFound.Checked = intersection;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ScreenToWorldOnTerrain", McEx);
            }
        }

        private void btnConScreenToWorldOnPlane_Click(object sender, EventArgs e)
        {
            try
            {
                DNSMcVector3D worldPoint;
                bool intersection = false;

                m_CurrentObject.ScreenToWorldOnPlane(ctrl3DCameraConScreenPoint.GetVector3D(),
                                                        out worldPoint,
                                                        out intersection, 
                                                        ntxPlaneLocation.GetDouble(),
                                                        ctrl3DCameraConPlaneNormal.GetVector3D());

                ctrl3DCameraConWorldPoint.SetVector3D( worldPoint);
                chxIsIntersectionFound.Checked = intersection;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ScreenToWorldOnPlane", McEx);
            }
        }

        private void btnCameraPositionOK_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetCameraPosition(ctrl3DCameraPosition.GetVector3D(), chxPositionRelative.Checked);

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetCameraPosition", McEx);
            }
            
            LoadItem(m_CurrentObject);
        }

        private void btnCameraUpVectorOK_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetCameraUpVector(ctrl3DCameraUpVector.GetVector3D(), chxUpVectorRelativeToOrientation.Checked);

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetCameraUpVector", McEx);
            }

            LoadItem(m_CurrentObject);
        }

        private void btnRotateCameraAroundWorldPoint_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.RotateCameraAroundWorldPoint(ctrl3DRotateCameraAroundWorldPointPivotPoint.GetVector3D(),
                                                                ntxRotateCameraAroundWorldPointDeltaYaw.GetFloat(),
                                                                ntxRotateCameraAroundWorldPointDeltaPitch.GetFloat(),
                                                                ntxRotateCameraAroundWorldPointDeltaRoll.GetFloat(),
                                                                chxRotateCameraAroundWorldPointRelativeToOrientation.Checked);

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetCameraUpVector", McEx);
            }

            LoadItem(m_CurrentObject);
        }

        private void btnCameraOrientationOK_Click(object sender, EventArgs e)
        {
            if (m_CurrentObject.MapType == DNEMapType._EMT_2D)
            {
                try
                {
                    m_CurrentObject.SetCameraOrientation(ctrlCameraOrientation.Yaw, chxOrientationRelative.Checked);

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetCameraOrientation", McEx);
                }
            }
            if (m_CurrentObject.MapType == DNEMapType._EMT_3D)
            {
                try
                {
                    m_CurrentObject.SetCameraOrientation(ctrlCameraOrientation.Yaw,ctrlCameraOrientation.Pitch,ctrlCameraOrientation.Roll, chxOrientationRelative.Checked);

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetCameraOrientation", McEx);
                }
            }
                        
            LoadItem(m_CurrentObject);
        }

        private void btnMoveRelativeToOrientationOK_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.MoveCameraRelativeToOrientation(ctrl3DMoveCameraRelativeToOrientation.GetVector3D(), chxXYDirectionOnly.Checked);

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("MoveCameraRelativeToOrientation", McEx);
            }
            
            LoadItem(m_CurrentObject);
        }

        private void btnCameraScaleOK_Click(object sender, EventArgs e)
        {
            if (m_CurrentObject.MapType == DNEMapType._EMT_2D)
            {
                try
                {
                    m_CurrentObject.SetCameraScale(ntxCameraScale.GetFloat());

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetCameraScale2D", McEx);
                }
            }
            if (m_CurrentObject.MapType == DNEMapType._EMT_3D)
            {
                try
                {
                    m_CurrentObject.SetCameraScale(ntxCameraScale.GetFloat(), ctrl3DCameraScaleWorldPoint.GetVector3D());

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetCameraScale3D", McEx);
                }
            }

            LoadItem(m_CurrentObject);
        }
        
        private void btnScreenVisibleAreaOk_Click(object sender, EventArgs e)
        {
            try
            {
                Point TL = new Point((int)ctrl2DTLScreenVisibleArea.X, (int)ctrl2DTLScreenVisibleArea.Y);
                Point BR = new Point((int)ctrl2DBRScreenVisibleArea.X, (int)ctrl2DBRScreenVisibleArea.Y);

                DNSMcRect rectVisibleArea = new DNSMcRect(TL, BR);
                DNESetVisibleArea3DOperation visibleArea3DOperation = (DNESetVisibleArea3DOperation)Enum.Parse(typeof(DNESetVisibleArea3DOperation), cmbScreenVisibleAreaOperation.Text);
                m_CurrentObject.SetCameraScreenVisibleArea(rectVisibleArea, visibleArea3DOperation);

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("CameraScreenVisibleArea", McEx);
            }

            LoadItem(m_CurrentObject);
        }

        private void btnScrollCameraOK_Click(object sender, EventArgs e)
        {
            if (m_CurrentObject.MapType == DNEMapType._EMT_2D)
            {
                try
                {
                    m_CurrentObject.ScrollCamera(ntxScrollCameraDeltaX.GetInt32(), ntxScrollCameraDeltaY.GetInt32());

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ScrollCamera", McEx);
                }

                LoadItem(m_CurrentObject);
            }
            else
                MessageBox.Show("Only for 2D map");
        }

        private void btnLookAtPointOK_Click(object sender, EventArgs e)
        {
            if (m_CurrentObject.MapType == DNEMapType._EMT_3D)
            {
                try
                {
                    m_CurrentObject.SetCameraLookAtPoint(ctrl3DLookAt.GetVector3D());

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("CameraLookAtPoint", McEx);
                }

                LoadItem(m_CurrentObject);
            }
            else
                MessageBox.Show("Only for 3D map");
        }

        private void btnForwardVectorOK_Click(object sender, EventArgs e)
        {
            if (m_CurrentObject.MapType == DNEMapType._EMT_3D)
            {
                try
                {
                    m_CurrentObject.SetCameraForwardVector(ctrl3DForwardVector.GetVector3D(), chxFVectorRelative.Checked);

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetCameraForwardVector", McEx);
                }

                LoadItem(m_CurrentObject);
            }
            else
                MessageBox.Show("Only for 3D map");
        }

        private void btnFieldOfViewOK_Click(object sender, EventArgs e)
        {
            if (m_CurrentObject.MapType == DNEMapType._EMT_3D)
            {
                try
                {
                    m_CurrentObject.SetCameraFieldOfView(ntxFieldOfView.GetFloat());

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("CameraFieldOfView", McEx);
                }

                LoadItem(m_CurrentObject);
            }
            else
                MessageBox.Show("Only for 3D map");
        }

        private void btnRotateCameraRelativeToOrientationOK_Click(object sender, EventArgs e)
        {
            if (m_CurrentObject.MapType == DNEMapType._EMT_3D)
            {
                try
                {
                    m_CurrentObject.RotateCameraRelativeToOrientation(ctrlRotateCamera.Yaw, ctrlRotateCamera.Pitch, ctrlRotateCamera.Roll);

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("RotateCameraRelativeToOrientation", McEx);
                }

                LoadItem(m_CurrentObject);
            }
            else
                MessageBox.Show("Only for 3D map");
        }
        
        private void btnHeightLimitsOK_Click(object sender, EventArgs e)
        {
            if (m_CurrentObject.MapType == DNEMapType._EMT_3D)
            {
                try
                {
                    m_CurrentObject.SetCameraRelativeHeightLimits(ntxMinRelativeHeightLimits.GetDouble(), ntxMaxRelativeHeightLimits.GetDouble(), chxCameraRelativeHeightLimits.Checked);

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetCameraRelativeHeightLimits", McEx);
                }

                LoadItem(m_CurrentObject);
            }
            else
                MessageBox.Show("Only for 3D map");
        }

        private void BtnCameraClipDistancesOK_Click(object sender, EventArgs e)
        {
            if (m_CurrentObject.MapType == DNEMapType._EMT_3D)
            {
                try
                {
                    m_CurrentObject.SetCameraClipDistances(ntxMinCameraClipDistances.GetFloat(), ntxMaxCameraClipDistances.GetFloat(), chxRenderInTwoSessions.Checked);

                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetCameraClipDistances", McEx);
                }

                LoadItem(m_CurrentObject);
            }
            else
                MessageBox.Show("Only for 3D map");
        }

        private void btnCameraWorldVisibleAreaOK_Click(object sender, EventArgs e)
        {
            try
            {
                float RectangleYaw = ntxRectangleYaw.GetFloat();
                DNSMcBox visibleArea = boxWorldVisibleArea.GetBoxValue();
                m_CurrentObject.SetCameraWorldVisibleArea(visibleArea, ntxWorldVisibleAreaMargin.GetInt32(),RectangleYaw);

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetCameraWorldVisibleArea", McEx);
            }

            LoadItem(m_CurrentObject);
        }

        private void btnCameraFootPrintOK_Click(object sender, EventArgs e)
        {
            if (chxFootprintIsDefined.Checked == true)
            {
                MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.bFootprintIsDefined = true;
            }
            else
            {
                MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.bFootprintIsDefined = false;
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
            
        }

        private void btnCameraCenterOffset_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetCameraCenterOffset(ctrl2DFVectorCenterOffset.GetVector2D());

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetCameraCenterOffset", McEx);
            }            
        }

        private void CreateCameraAttachmentTree()
        {
            string[] names = new string[2];
            string obj_key, node_key, node_text;
            IDNMcOverlayManager OM = m_CurrentViewport.OverlayManager;
            IDNMcOverlay [] overlayArr = OM.GetOverlays();
            foreach (IDNMcOverlay overlay in overlayArr)
            {
                IDNMcObject[] objects = overlay.GetObjects();
                for (int i = 0; i < objects.Length; i++ )
                {
                    Manager_MCNames.GetNameByObjectArray(objects[i], names);

                    treeViewCameraAttachmentSrc.Nodes.Add(names[0], names[1]);
                    treeViewCameraAttachmentlookAt.Nodes.Add(names[0], names[1]);

                    treeViewCameraAttachmentSrc.Nodes[i].Tag = objects[i];
                    treeViewCameraAttachmentlookAt.Nodes[i].Tag = objects[i];

                    IDNMcObjectScheme scheme = objects[i].GetScheme();
                    IDNMcObjectSchemeNode[] nodes = scheme.GetNodes(DNENodeKindFlags._ENKF_ANY_ITEM);
                    for (int Idx = 0; Idx < nodes.Length; Idx++ )
                    {
                        obj_key = names[0];
                        Manager_MCNames.GetNameByObjectArray(nodes[Idx], names);
                        node_key = obj_key + names[0];
                        node_text = names[1];
                        
                        treeViewCameraAttachmentSrc.Nodes[i].Nodes.Add(node_key,node_text);
                        treeViewCameraAttachmentlookAt.Nodes[i].Nodes.Add(node_key,node_text);

                        (treeViewCameraAttachmentSrc.Nodes.Find(node_key, true))[0].Tag = nodes[Idx];
                        (treeViewCameraAttachmentlookAt.Nodes.Find(node_key, true))[0].Tag = nodes[Idx];
	                }
                }
            }
        }

        private void treeViewCameraAttachmentSrc_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lsvCameraAttachmentSrc.Visible = false;

            if (treeViewCameraAttachmentSrc.SelectedNode.Tag is IDNMcMeshItem)
            {
                IDNMcMesh mesh;
                uint propId;
                ((IDNMcMeshItem)treeViewCameraAttachmentSrc.SelectedNode.Tag).GetMesh(out mesh,out propId, false);
                if (mesh != null)
                {
                    if (mesh.MeshType == DNEMeshType._EMT_NATIVE_LOD_MESH_FILE || mesh.MeshType == DNEMeshType._EMT_NATIVE_MESH_FILE)
                    {
                        lsvCameraAttachmentSrc.Visible = true;

                        uint[] AttachPointsIDs = ((IDNMcNativeMesh)mesh).GetMappedNamesIDs(DNEMappedNameType._EMNT_ATTACH_POINT);

                        lsvCameraAttachmentSrc.Items.Clear();
                        ListViewItem[] lsvItems = new ListViewItem[AttachPointsIDs.Length];
                        string name;
                        int counter = 0;
                        foreach (uint ui in AttachPointsIDs)
                        {
                            ((IDNMcNativeMesh)mesh).GetMappedNameByID(DNEMappedNameType._EMNT_ATTACH_POINT, ui, out name);
                            string[] newRow = new string[] { ui.ToString(), name.ToString() };
                            lsvItems[counter] = new ListViewItem(newRow);

                            counter++;
                        }

                        //Insert the attach point ID's and Name to the ListView.
                        lsvCameraAttachmentSrc.Items.AddRange(lsvItems);
                    }
                }
            }
        }

        private void chxLookAt_CheckedChanged(object sender, EventArgs e)
        {
            if (chxLookAt.Checked == true)
            {
                gbLookAt.Visible = false;
                splitContainerCameraAttachment.Panel2.Enabled = true;
            }
            else
            {

                gbLookAt.Visible = true;
                treeViewCameraAttachmentlookAt.SelectedNode = null;
                splitContainerCameraAttachment.Panel2.Enabled = false;
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
        }

        private void treeViewCameraAttachmentlookAt_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lsvCameraAttachmentLookAt.Visible = false;

            if (treeViewCameraAttachmentlookAt.SelectedNode.Tag is IDNMcMeshItem)
            {
                IDNMcMesh mesh;
                uint propId;
                ((IDNMcMeshItem)treeViewCameraAttachmentlookAt.SelectedNode.Tag).GetMesh(out mesh, out propId, false);
                if (mesh.MeshType == DNEMeshType._EMT_NATIVE_LOD_MESH_FILE || mesh.MeshType == DNEMeshType._EMT_NATIVE_MESH_FILE)
                {
                    lsvCameraAttachmentLookAt.Visible = true;

                    uint[] AttachPointsIDs = ((IDNMcNativeMesh)mesh).GetMappedNamesIDs(DNEMappedNameType._EMNT_ATTACH_POINT);

                    lsvCameraAttachmentLookAt.Items.Clear();
                    ListViewItem[] lsvItems = new ListViewItem[AttachPointsIDs.Length];
                    string name;
                    int counter = 0;
                    foreach (uint ui in AttachPointsIDs)
                    {
                        ((IDNMcNativeMesh)mesh).GetMappedNameByID(DNEMappedNameType._EMNT_ATTACH_POINT, ui, out name);
                        string[] newRow = new string[] { ui.ToString(), name.ToString() };
                        lsvItems[counter] = new ListViewItem(newRow);

                        counter++;
                    }

                    //Insert the attach point ID's and Name to the ListView.
                    lsvCameraAttachmentLookAt.Items.AddRange(lsvItems);
                }
            }
        }

        private void btnCameraAttachmentOK_Click(object sender, EventArgs e)
        {
            try
            {
                DNSCameraAttachmentParams CameraAttachmentParams = null;
                //CameraAttachmentParams.

                if (treeViewCameraAttachmentSrc.SelectedNode != null)
                {
                    //Create DNSCameraAttachmentParams.
                    object selectetNodeSrc = treeViewCameraAttachmentSrc.SelectedNode.Tag;
                    object selectedNodeParentSrc = null;

                    if (selectetNodeSrc is IDNMcObject)
                    {
                        CameraAttachmentParams = new DNSCameraAttachmentParams((IDNMcObject)selectetNodeSrc, null, ntxCameraAttachmentSrcAPIndex.GetUInt32());
                    }
                    else if (selectetNodeSrc is IDNMcMeshItem)
                    {
                        selectedNodeParentSrc = treeViewCameraAttachmentSrc.SelectedNode.Parent.Tag;
                        CameraAttachmentParams = new DNSCameraAttachmentParams((IDNMcObject)selectedNodeParentSrc, (IDNMcObjectSchemeItem)selectetNodeSrc, ntxCameraAttachmentSrcAPIndex.GetUInt32());
                    }
                    else
                    {
                        selectedNodeParentSrc = treeViewCameraAttachmentSrc.SelectedNode.Parent.Tag;
                        CameraAttachmentParams = new DNSCameraAttachmentParams((IDNMcObject)selectedNodeParentSrc, (IDNMcObjectSchemeItem)selectetNodeSrc);
                    }

                    try
                    {
                        CameraAttachmentParams.Offset = ctrl3DVectorAttachmentCameraOffset.GetVector3D();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("Offset", McEx);
                    }

                    try
                    {
                        CameraAttachmentParams.bAttachOrientation = chxAttachOrientation.Checked;
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("bAttachOrientation", McEx);
                    }

                    try
                    {
                        CameraAttachmentParams.fAdditionalYaw = ntxAdditionalYaw.GetFloat();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("fAdditionalYaw", McEx);
                    }

                    try
                    {
                        CameraAttachmentParams.fAdditionalPitch = ntxAdditionalPitch.GetFloat();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("fAdditionalPitch", McEx);
                    }

                    try
                    {
                        CameraAttachmentParams.fAdditionalRoll = ntxAdditionalRoll.GetFloat();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("fAdditionalRoll", McEx);
                    }
                }
                
                //Create DNSCameraAttachmentTarget
                object selectetNodeTarget = null;
                object selectedNodeParentTarget = null;
                DNSCameraAttachmentTarget CameraAttachmentTarget = null;

                if (treeViewCameraAttachmentlookAt.SelectedNode != null)
                {
                    selectetNodeTarget = treeViewCameraAttachmentlookAt.SelectedNode.Tag;

                    if (selectetNodeTarget is IDNMcObject)
                    {
                        CameraAttachmentTarget = new DNSCameraAttachmentTarget((IDNMcObject)selectetNodeTarget);
                    }
                    else if (selectetNodeTarget is IDNMcMeshItem)
                    {
                        selectedNodeParentTarget = treeViewCameraAttachmentlookAt.SelectedNode.Parent.Tag;
                        CameraAttachmentTarget = new DNSCameraAttachmentTarget((IDNMcObject)selectedNodeParentTarget, (IDNMcObjectSchemeItem)selectetNodeTarget, ntxCameraAttachmentLookAtAPIndex.GetUInt32());
                    }
                    else
                    {
                        selectedNodeParentTarget = treeViewCameraAttachmentlookAt.SelectedNode.Parent.Tag;
                        CameraAttachmentTarget = new DNSCameraAttachmentTarget((IDNMcObject)selectedNodeParentTarget, (IDNMcObjectSchemeItem)selectetNodeTarget);
                    }
                }

                m_CurrentObject.SetCameraAttachment(CameraAttachmentParams, CameraAttachmentTarget);
                
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetCameraAttachment", McEx);
            }              
        }

        private void btnCameraAttachmentEnabled_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetCameraAttachmentEnabled(chxCameraAttachmentEnabled.Checked);

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentViewport);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetCameraAttachmentEnabled", McEx);
            }
        }

        private void btnGetCameraScale3D_Click(object sender, EventArgs e)
        {
            try
            {
                DNSMcVector3D scaleWorldPoint = ctrl3DCameraScaleWorldPoint.GetVector3D();
                ntxCameraScale.SetFloat(m_CurrentObject.GetCameraScale(scaleWorldPoint));
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetCameraScale", McEx);
            }
        }

        private void btnGetWorldVisibleArea_Click(object sender, EventArgs e)
        {
            if (m_CurrentObject.MapType == DNEMapType._EMT_2D)  //Relevant for 2D and 3D - Currently implemented in MapCore only for 2D
            {
                try
                {
                    DNSMcBox worldVisibleArea = m_CurrentObject.GetCameraWorldVisibleArea(ntxWorldVisibleAreaMargin.GetInt32(),ntxRectangleYaw.GetFloat());
                    boxWorldVisibleArea.SetBoxValue(worldVisibleArea);
                    boxWorldVisibleArea.CheckBBObject();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCameraWorldVisibleArea", McEx);
                }
            }
        }

        private void m_CameraTabCtrl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Name == "tpCameraAttachment")
            {
                if (m_CurrentViewport != null && CtrlLoadedFirsTime == true)
                {
                    CtrlLoadedFirsTime = false;
                    CreateCameraAttachmentTree();

                    try
                    {
                        DNSCameraAttachmentParams cameraAttachmentParams;
                        DNSCameraAttachmentTarget cameraAttachmentTarget;

                        m_CurrentObject.GetCameraAttachment(out cameraAttachmentParams, out cameraAttachmentTarget);

                        if (cameraAttachmentTarget == null)
                            chxLookAt.Checked = false;
                        else
                        {
                            chxLookAt.Checked = true;
                            ntxCameraAttachmentLookAtAPIndex.SetUInt32(cameraAttachmentTarget.uAttachPoint);

                            TreeNode[] selectedNodeLookAt = new TreeNode[1];
                            if (cameraAttachmentTarget.pItem != null)
                            {
                                selectedNodeLookAt = treeViewCameraAttachmentlookAt.Nodes.Find(Manager_MCNames.GetIdByObject(cameraAttachmentTarget.pObject) + Manager_MCNames.GetIdByObject(cameraAttachmentTarget.pItem), true);
                                if (selectedNodeLookAt.Length != 0)
                                    treeViewCameraAttachmentlookAt.SelectedNode = selectedNodeLookAt[0];
                            }
                            else
                            {
                                if (cameraAttachmentTarget.pObject != null)
                                {
                                    selectedNodeLookAt = treeViewCameraAttachmentlookAt.Nodes.Find(Manager_MCNames.GetIdByObject(cameraAttachmentTarget.pObject), true);
                                    if (selectedNodeLookAt.Length != 0)
                                        treeViewCameraAttachmentlookAt.SelectedNode = selectedNodeLookAt[0];
                                }
                            }
                        }

                        TreeNode[] selectedNodeSrc = new TreeNode[1];
                        if (cameraAttachmentParams != null)
                        {
                            if (cameraAttachmentParams.pItem != null)
                            {
                               selectedNodeSrc = treeViewCameraAttachmentSrc.Nodes.Find(Manager_MCNames.GetIdByObject(cameraAttachmentParams.pObject) + Manager_MCNames.GetIdByObject(cameraAttachmentParams.pItem), true);

                                //SelectedNodeSrc equal to Null in case that the selected node in the tree is map camera
                                if (selectedNodeSrc.Length != 0)
                                {
                                    treeViewCameraAttachmentSrc.SelectedNode = selectedNodeSrc[0];

                                    ntxCameraAttachmentSrcAPIndex.SetUInt32(cameraAttachmentParams.uAttachPoint);
                                    ctrl3DVectorAttachmentCameraOffset.SetVector3D( cameraAttachmentParams.Offset);
                                    chxAttachOrientation.Checked = cameraAttachmentParams.bAttachOrientation;
                                    ntxAdditionalYaw.SetFloat(cameraAttachmentParams.fAdditionalYaw);
                                    ntxAdditionalPitch.SetFloat(cameraAttachmentParams.fAdditionalPitch);
                                    ntxAdditionalRoll.SetFloat(cameraAttachmentParams.fAdditionalRoll);
                                }
                            }
                            else
                            {
                                if (cameraAttachmentParams.pObject != null)
                                {
                                    selectedNodeSrc = treeViewCameraAttachmentSrc.Nodes.Find(Manager_MCNames.GetNameByObject(cameraAttachmentParams.pObject), true);

                                    //SelectedNodeSrc equal to Null in case that the selected node in the tree is map camera
                                    if (selectedNodeSrc.Length != 0)
                                    {
                                        treeViewCameraAttachmentSrc.SelectedNode = selectedNodeSrc[0];

                                        ntxCameraAttachmentSrcAPIndex.SetUInt32(cameraAttachmentParams.uAttachPoint);
                                        ctrl3DVectorAttachmentCameraOffset.SetVector3D( cameraAttachmentParams.Offset);
                                        chxAttachOrientation.Checked = cameraAttachmentParams.bAttachOrientation;
                                        ntxAdditionalYaw.SetFloat(cameraAttachmentParams.fAdditionalYaw);
                                        ntxAdditionalPitch.SetFloat(cameraAttachmentParams.fAdditionalPitch);
                                        ntxAdditionalRoll.SetFloat(cameraAttachmentParams.fAdditionalRoll);
                                    }

                                }
                            }
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetCameraAttachment", McEx);
                    }

                    try
                    {
                        chxCameraAttachmentEnabled.Checked = m_CurrentObject.GetCameraAttachmentEnabled();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetCameraAttachmentEnabled", McEx);
                    }
                }
            }
        }

        private void treeViewCameraAttachmentSrc_MouseDown(object sender, MouseEventArgs e)
        {
            if (treeViewCameraAttachmentSrc.GetNodeAt(e.X, e.Y) == null)
                treeViewCameraAttachmentSrc.SelectedNode = null;
        }

        private void chxPositionRelative_CheckedChanged(object sender, EventArgs e)
        {
            if (chxPositionRelative.Checked)
            {
                ctrl3DCameraPosition.SetVector3D( new DNSMcVector3D(0, 0, 0));
            }
            else
            {
                LoadItem(m_CurrentObject);
            }
        }

        private void chxOrientationRelative_CheckedChanged(object sender, EventArgs e)
        {
            if (chxOrientationRelative.Checked)
            {
                ctrlCameraOrientation.Yaw = 0;
                ctrlCameraOrientation.Pitch = 0;
                ctrlCameraOrientation.Roll = 0;
            }
            else
            {
                LoadItem(m_CurrentObject);
            }
        }

        private void tpAnyMapType_Enter(object sender, EventArgs e)
        {
            CheckBoxWorldVisibleArea();
        }

        private void tpAnyMapType_Leave(object sender, EventArgs e)
        {
            boxWorldVisibleArea.RemoveBBObject();
        }

        public void CheckBoxWorldVisibleArea()
        {
            boxWorldVisibleArea.CheckBBObject();
        }

        internal void CheckBoxesObjects()
        {
            if (m_CameraTabCtrl.SelectedTab == tpAnyMapType)
                CheckBoxWorldVisibleArea();
        }

        public void RemoveBBObject()
        {
            boxWorldVisibleArea.RemoveBBObject();
        }
    }   
}
