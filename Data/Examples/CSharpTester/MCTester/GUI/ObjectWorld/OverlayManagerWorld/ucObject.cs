using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.ObjectWorld.ObjectsUserControls;
using MCTester.General_Forms;
using MCTester.Managers.MapWorld;
using System.IO;
using MCTester.Managers;
using MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms;
using MapCore.Common;
using MCTester.GUI.Trees;
using System.Linq;
using MCTester.Managers.ObjectWorld;

namespace MCTester.ObjectWorld.OverlayManagerWorld
{
	public partial class ucObject : UserControl,IUserControlItem, IOnMapClick
	{
		private IDNMcObject m_CurrentObject;
        private List<string> m_lViewportText;
        private List<IDNMcMapViewport> m_lViewportValue;

        private List<string> m_lstObjectText = new List<string>();
        private List<IDNMcObject> m_lstObjectValue = new List<IDNMcObject>();
        private List<string> m_lstSchemeNodeText = new List<string>();
        private List<IDNMcObjectSchemeNode> m_lstSchemeNodeValue = new List<IDNMcObjectSchemeNode>();
        private IDNMcObject m_SelectedObject;

        private List<string> m_lObjectText = new List<string>();
        private List<IDNMcObject> m_lObjectValue = new List<IDNMcObject>();

        private uint m_PropId;
        private bool m_bParam;
        private uint mSelectedLocationIndex;

        private string m_SymbologySymbolID = "";
        private bool m_isStandardSymbology = false;

        private List<string> m_lstViewportText = new List<string>();
        private List<IDNMcMapViewport> m_lstViewportValue = new List<IDNMcMapViewport>();
        private List<IDNMcTraversabilityMapLayer> m_lstTraversabilityMapLayers = new List<IDNMcTraversabilityMapLayer>();
        private List<string> m_lstTraversabilityMapLayersText = new List<string>();

        private IDNMcOverlay m_TempOverlay = null;

        public ucObject()
		{
            InitializeComponent();
            cmbVisibility.Items.AddRange(Enum.GetNames(typeof(DNEActionOptions)));
            cmbPositionInterpolationMode.Items.AddRange(Enum.GetNames(typeof(DNEPositionInterpolationMode)));
            cmbRotationInterpolationMode.Items.AddRange(Enum.GetNames(typeof(DNERotationInterpolationMode)));

            cmbPositionInterpolationMode.Text = DNEPositionInterpolationMode._EPIM_LINEAR.ToString();
            cmbRotationInterpolationMode.Text = DNERotationInterpolationMode._ERIM_LINEAR.ToString();

            cmbAttachPointType.Items.AddRange(Enum.GetNames(typeof(DNEAttachPointType)));
            
            m_lViewportText = new List<string>();
            m_lViewportValue = new List<IDNMcMapViewport>();

            lstViewports.DisplayMember = "lViewportText";
            lstViewports.ValueMember = "lViewportValue";

            DNEBoundingBoxPointFlags[] BoundingBoxPointFlags = (DNEBoundingBoxPointFlags[])Enum.GetValues(typeof(DNEBoundingBoxPointFlags));
            foreach (DNEBoundingBoxPointFlags flag in BoundingBoxPointFlags)
            {
                if (flag != DNEBoundingBoxPointFlags._EBBPF_NONE)
                    clstBoundingBoxPointTypeBitField.Items.Add(flag);
            }

            ctrlLocationPoints.SetUpdateDelegate(UpdatePoints);
            ctrlLocationPoints.SetSelectionChanged(dgvLocationPointsSelectionChanged);

		}

		#region IUserControlItem Members
        
        public void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcObject)aItem;
            ctrlConditionalselectorOverlay.LoadItem(aItem);
            chxEffectiveVisibilityInViewport.CheckState = CheckState.Indeterminate;
            txEffectiveStateInSelectedViewport.Text = "";
            LoadViewportsToListBox();
            LoadObjectToListBox();

            try
            {
                ntbObjectSchemeId.Text = Manager_MCNames.GetNameByObject(m_CurrentObject.GetScheme());
                
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetScheme", McEx);
            }

            try
            {
                uint numLocations = m_CurrentObject.GetNumLocations();
                ntxNumLocation.SetUInt32(numLocations);
                if (numLocations > 0)
                {
                    for (int i = 0; i < numLocations; i++)
                        cbLocationIndex.Items.Add(i);
                    cbLocationIndex.SelectedIndex = 0;
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetNumLocations", McEx);
            }

            //GetLocationPoints();

            try
            {
                ntxObjectID.SetUInt32(m_CurrentObject.GetID());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetID", McEx);
            }
            try
            {
                string name, desc;
                m_CurrentObject.GetNameAndDescription(out name, out desc);
                tbName.Text = name;
                tbDesc.Text = desc;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetNameAndDescription", McEx);
            }

            try
            {
                byte[] states = m_CurrentObject.GetState();
                string txtStates = "";
                if (states != null)
                {
                    foreach (byte byState in states)
                    {
                        txtStates += (byState.ToString() + " ");
                    }
                }
                if (txtStates.Length >= 1)
                {
                    txtStates = txtStates.Substring(0, txtStates.Length - 1);
                }
                antxObjectStates.Text = txtStates;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetState", McEx);
            }

            try
            {
                IDNMcUserData UD = m_CurrentObject.GetUserData();
                if (UD != null)
                {
                    TesterUserData TUD = (TesterUserData)UD;
                    ctrlUserData.UserDataByte = TUD.UserDataBuffer;
                }

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetUserData", McEx);
            }

            try
            {
                cmbVisibility.Text = m_CurrentObject.GetVisibilityOption().ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVisibilityOption", McEx);
            }

            try
            {
                IDNMcCollection [] collections = m_CurrentObject.GetCollections();
                lstMemberInCollections.Items.AddRange(collections);
            }   
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCollections", McEx);
            }

            try
            {
                IDNMcImageCalc imgc = m_CurrentObject.GetImageCalc();
                if (imgc != null)
                    btnSetImageCalc.Text = "Exist";
                else
                    btnSetImageCalc.Text = "Null";
            }
            catch (MapCoreException McEx)
            {
            	Utilities.ShowErrorMessage("", McEx);
            }

            try
            {
                chkObjectDetectibility.Checked = m_CurrentObject.GetDetectibility();
            }
            catch (MapCoreException McEx)
            {
               Utilities.ShowErrorMessage("GetDetectability", McEx);
            }

            try
            {
                ntxDrawPriority.SetShort(m_CurrentObject.GetDrawPriority());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetDrawPriority", McEx);
            }

            try
            {
                DNSObjectToObjectAttachmentParams ? objectToObjectAttachmentParams = m_CurrentObject.GetObjectToObjectAttachment(ntxAttachedLocationIndex.GetUInt32());
                FillObjectToObjectAttachmentParams(objectToObjectAttachmentParams);   	
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjectToObjectAttachment", McEx);
            }

            try
            {
                chxIgnoreViewportVisibilityMaxScale.Checked = m_CurrentObject.GetIgnoreViewportVisibilityMaxScale();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetIgnoreViewportVisibilityMaxScale", McEx);
            }

            try
            {
                cbIsAttachedToAnotherObject.Checked = m_CurrentObject.IsAttachedToAnotherObject();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IsAttachedToAnotherObject", McEx);
            }


            try
            {
                cbSuppressSightPresentationMapTilesWebRequests.Checked = m_CurrentObject.GetSuppressQueryPresentationMapTilesWebRequests();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSuppressQueryPresentationMapTilesWebRequests", McEx);
            }

            try
            {
                m_isStandardSymbology = m_CurrentObject.GetSymbologyStandard() != DNESymbologyStandard._ESS_NONE;
                if (m_isStandardSymbology)
                {
                    ctrlSymbologySymbolID1.SetSymbologyStandard(m_CurrentObject.GetSymbologyStandard());
                    GetSymbologyAmplifiers();
                    GetSymbologyAnchorPointsAndGeometricAmplifiers();
                    ctrlSymbologySymbolID1.SetControlsEnabled(false);
                    if (Manager_MCTSymbology.IsExistAnchorPoints(m_CurrentObject))
                        btnShowAnchorPoints.Text = "Hide Anchor Points";
                } 
                else
                    tcObject.TabPages.Remove(tpSymbolgyStandard);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSymbologyStandard", McEx);
            }
            IDNMcTraversabilityMapLayer traversabilityMapLayer = null;
            try
            {
                traversabilityMapLayer = m_CurrentObject.GetTraversabilityPresentationMapLayer();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTraversabilityPresentationMapLayer", McEx);
            }

            try
            {
                uint[] VpIds = m_CurrentObject.GetOverlayManager().GetViewportsIDs();

                Dictionary<object, uint> viewports = Manager_MCViewports.AllParams;

                //Add relevant viewport to list
                foreach (uint ID in VpIds)
                {
                    foreach (IDNMcMapViewport VP in viewports.Keys)
                    {
                        if (VP.ViewportID == ID)
                        {
                            m_lstViewportText.Add(Manager_MCNames.GetNameByObject(VP, "Viewport"));
                            m_lstViewportValue.Add(VP);
                        }
                    }
                }
                int i = 0;
                int indexLayer = -1;
                foreach (IDNMcMapViewport VP in m_lstViewportValue)
                {
                    foreach (IDNMcMapTerrain terr in VP.GetTerrains())
                    {
                        foreach (IDNMcMapLayer layer in terr.GetLayers())
                        {
                            string layerType = layer.LayerType.ToString().Replace("_ELT_", "");
                            if (layer is IDNMcTraversabilityMapLayer)
                            {
                                if (layer == traversabilityMapLayer)
                                    indexLayer = i;
                                m_lstTraversabilityMapLayersText.Add(Manager_MCNames.GetNameByObject(layer, layerType));
                                m_lstTraversabilityMapLayers.Add(layer as IDNMcTraversabilityMapLayer);
                                i++;
                            }
                            
                        }
                    }
                }
                lstTraversabilityMapLayers.Items.AddRange(m_lstTraversabilityMapLayersText.ToArray());
                if (indexLayer >= 0 && indexLayer < lstTraversabilityMapLayers.Items.Count)
                {
                    lstTraversabilityMapLayers.SelectedIndex = indexLayer;
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTerrains()/GetLayers()", McEx);
            }

        }
        #endregion

        #region Public Method
        public void SaveObject()
        {
            try
            {
                m_CurrentObject.SetID(ntxObjectID.GetUInt32());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetID", McEx);
            }

            try
            {
                m_CurrentObject.SetNameAndDescription(tbName.Text, tbDesc.Text);

                Control control = Parent.Parent.Parent;
                MCObjectWorldTreeViewForm mcOverlayMangerTreeView = control as MCObjectWorldTreeViewForm;
                if (mcOverlayMangerTreeView != null)
                {
                    mcOverlayMangerTreeView.SetName(m_CurrentObject);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetNameAndDescription", McEx);
            }

            try
            {
                m_CurrentObject.SetSuppressQueryPresentationMapTilesWebRequests(cbSuppressSightPresentationMapTilesWebRequests.Checked);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSuppressQueryPresentationMapTilesWebRequests", McEx);
            }
            try
            {
                TesterUserData TUD = null;
                if (ctrlUserData.UserDataByte != null && ctrlUserData.UserDataByte.Length != 0)
                    TUD = new TesterUserData(ctrlUserData.UserDataByte);

                m_CurrentObject.SetUserData(TUD);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetUserData", McEx);
            }

            try
            {
                m_CurrentObject.SetIgnoreViewportVisibilityMaxScale(chxIgnoreViewportVisibilityMaxScale.Checked);	
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetIgnoreViewportVisibilityMaxScale", McEx);
            }

            try
            {
                ctrlConditionalselectorOverlay.Save();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetConditionalSelector", McEx);
            }

            try
            {
                IDNMcTraversabilityMapLayer traversabilityMapLayer = null;
                if (lstTraversabilityMapLayers.SelectedIndex >= 0)
                    traversabilityMapLayer = m_lstTraversabilityMapLayers[lstTraversabilityMapLayers.SelectedIndex];
                m_CurrentObject.SetTraversabilityPresentationMapLayer(traversabilityMapLayer);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetTraversabilityPresentationMapLayer", McEx);
            }

            if (m_isStandardSymbology)
            {
                btnSetSymbologyAnchorPointsAndGeometricAmplifiers_Click(null, null);
                btnSetSymbologyAmplifiers_Click(null, null);
            }

            // turn on all viewports render needed flags
            Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        #endregion

        #region Private Methods

        private void btnGetLocationIndexByID_Click(object sender, EventArgs e)
        {
            try
            {
                uint locationIndex = m_CurrentObject.GetLocationIndexByID(ntxNodeID.GetUInt32());
                if (locationIndex >= 0 && locationIndex < cbLocationIndex.Items.Count)
                    cbLocationIndex.Text = locationIndex.ToString();
            }
            catch (MapCoreException McEx)
            {
            	Utilities.ShowErrorMessage("GetLocationIndexByID", McEx);
            }
                       
        }
        #endregion

        private IDNMcOverlay GetTempOverlay()
        {
            try
            {
                if (m_TempOverlay == null)
                    m_TempOverlay = DNMcOverlay.Create(m_CurrentObject.GetOverlayManager(), true);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcOverlay.Create", McEx);
            }
            return m_TempOverlay;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.btnApply_Click(sender, e);
            GeneralFuncs.CloseParentForm(this);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveObject();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GeneralFuncs.CloseParentForm(this);
        }

       

        private void btnAddLocationPoint_Click(object sender, EventArgs e)
        {
            try
            {
                int insertIndex = ctrlLocationPoints.GetSelectedRowIndex();
                if (insertIndex >= 0)
                {
                    m_CurrentObject.AddLocationPoint((uint)insertIndex,
                                                   ctrl3DLocationPoint.GetVector3D(),
                                                   mSelectedLocationIndex);

                    UpdateGridPoints();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("AddLocationPoint", McEx);
            }
        }

        private void btnRemoveLocationPoint_Click(object sender, EventArgs e)
        {
            try
            {
                int insertIndex = ctrlLocationPoints.GetSelectedRowIndex();
                if (insertIndex >= 0)
                {
                    m_CurrentObject.RemoveLocationPoint((uint)insertIndex,
                                                      mSelectedLocationIndex);

                    UpdateGridPoints();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("RemoveLocationPoint", McEx);
            }
        }

        private void btnUpdateLocationPoint_Click(object sender, EventArgs e)
        {
            try
            {
                int insertIndex = ctrlLocationPoints.GetSelectedRowIndex();
                if (insertIndex >= 0)
                {
                    m_CurrentObject.UpdateLocationPoint((uint)insertIndex,
                                                    ctrl3DLocationPoint.GetVector3D(),
                                                    mSelectedLocationIndex);

                    UpdateGridPoints() ;
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("UpdateLocationPoint", McEx);
            }
        }

        private void ntxMoveAllPoints_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.MoveAllLocationsPoints(ctrl3DLocationPoint.GetVector3D());

                UpdateGridPoints();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("MoveAllLocationsPoints", McEx);
            }
        }

        private void btnPropertiesID_Click(object sender, EventArgs e)
        {
            frmPropertiesIDList PropertyIDListForm = new frmPropertiesIDList(m_CurrentObject);
            PropertyIDListForm.Show();
        }

        private void UpdateGridPoints()
        {
            try
            {
                DNSMcVector3D[] locationPointsArray = m_CurrentObject.GetLocationPoints(mSelectedLocationIndex);
                ctrlLocationPoints.SetPoints(locationPointsArray);
                if (locationPointsArray != null)
                {
                    ntxNumLocationPoints.SetInt(locationPointsArray.Length);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetLocationPoints", McEx);
            }
        }

        private void GetObjectLocationData()
        {
            IDNMcObjectLocation mcObjectLocation = null;

            try
            {
                mcObjectLocation = m_CurrentObject.GetScheme().GetObjectLocation(mSelectedLocationIndex);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjectLocation", McEx);
            }

            if (mcObjectLocation != null)
            {
                try
                {
                    mcObjectLocation.GetRelativeToDTM(out m_bParam, out m_PropId);

                    if (m_PropId == DNMcConstants._MC_EMPTY_ID) // shared
                        chxIsRelativeToDTM.Checked = m_bParam;
                    else
                        chxIsRelativeToDTM.Checked = m_CurrentObject.GetBoolProperty(m_PropId);

                    ctrlLocationPoints.SetIsRelativeToDTM(chxIsRelativeToDTM.Checked);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetRelativeToDTM", McEx);
                }

                try
                {
                    DNEMcPointCoordSystem mcPointCoordSystem;
                    mcObjectLocation.GetCoordSystem(out mcPointCoordSystem);
                    txtGeometryCoordinateSystem.Text = mcPointCoordSystem.ToString();
                    ctrlLocationPoints.SetPointCoordSystem(mcPointCoordSystem);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetCoordSystem", McEx);
                }
            }
        }

        private void btnSetNumLocationPointsOK_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetNumLocationPoints(ntxNumLocationPoints.GetUInt32(),
                                                     mSelectedLocationIndex);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetNumLocationPoints", McEx);
            }

            UpdateGridPoints();
        }

        private void btnUpdateLocationPoints_Click(object sender, EventArgs e)
        {
            try
            {
                uint startIndex = 0;
                DNSMcVector3D[] selectedPoints;
                if (ctrlLocationPoints.GetSelectedPoints(out selectedPoints, out startIndex))
                {
                    m_CurrentObject.UpdateLocationPoints(selectedPoints,
                                                        startIndex,
                                                        mSelectedLocationIndex);

                    UpdateGridPoints();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetLocationPoints", McEx);
            }
        }

        private void btnSetLocationPoints_Click(object sender, EventArgs e)
        {
            try
            {
                DNSMcVector3D[] NewLocationPoints;

                if (ctrlLocationPoints.GetPoints(out NewLocationPoints))
                {
                    m_CurrentObject.SetLocationPoints(NewLocationPoints, mSelectedLocationIndex);

                    UpdateGridPoints();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetLocationPoints", McEx);
            }
        }

        
        private void lstTargetObject_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > lstTargetObject.ItemHeight * lstTargetObject.Items.Count)
            {
                lstTargetObject.ClearSelected();
                SelectedObject = null;
            }
        }

        private void lstTargetNode_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > lstTargetNode.ItemHeight * lstTargetNode.Items.Count)
                lstTargetNode.ClearSelected();
        }

        private void LoadViewportsToListBox()
        {
            try
            {
                IDNMcOverlayManager currOM = m_CurrentObject.GetOverlayManager();
                Dictionary<object, uint> dViewports = Manager_MCViewports.AllParams;
                foreach (IDNMcMapViewport vp in dViewports.Keys)
                {
                    if (vp.OverlayManager == currOM)
                    {
                        string name = Manager_MCNames.GetNameByObject(vp);
                        lViewportText.Add(name);
                        lViewportValue.Add(vp);
                    }
                }

                lstViewports.Items.AddRange(lViewportText.ToArray());
                lstScreenArrangementOffsetViewports.Items.AddRange(lViewportText.ToArray());

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetViewportsIDs", McEx);
            }
        }

        private void LoadObjectToListBox()
        {
            m_lstObjectText.Clear();
            m_lstObjectValue.Clear();

            IDNMcOverlay [] overlays = m_CurrentObject.GetOverlayManager().GetOverlays();
            
            foreach (IDNMcOverlay overlay in overlays)
            {
                IDNMcObject [] objects = overlay.GetObjects();
                foreach (IDNMcObject obj in objects)
                {
                    string name = Manager_MCNames.GetNameByObject(obj);
                    m_lstObjectText.Add(name);
                    m_lstObjectValue.Add(obj);
                }                 
            }

            lstTargetObject.Items.AddRange(m_lstObjectText.ToArray());
        }

        private void btnPlayAnimation_Click(object sender, EventArgs e)
        {
            try
            {
                int pathNodeNumber = dgvPathAnimation.RowCount;
                
                DNSPathAnimationNode[] PathAnimationNode;
                if (dgvPathAnimation.RowCount == 1)
                    PathAnimationNode = new DNSPathAnimationNode[pathNodeNumber];
                else
                    PathAnimationNode = new DNSPathAnimationNode[pathNodeNumber-1];
                
                
                if (dgvPathAnimation[0,0].Value != null)
                {
                    for (int i=0; i<dgvPathAnimation.RowCount-1; i++)
                    {
                        PathAnimationNode[i].Position.x = (dgvPathAnimation[0, i].Value == null) ? 0 : float.Parse(dgvPathAnimation[0, i].Value.ToString());
                        PathAnimationNode[i].Position.y = (dgvPathAnimation[1, i].Value == null) ? 0 : float.Parse(dgvPathAnimation[1, i].Value.ToString());
                        PathAnimationNode[i].Position.z = (dgvPathAnimation[2, i].Value == null) ? 0 : float.Parse(dgvPathAnimation[2, i].Value.ToString());
                        
                        PathAnimationNode[i].fTime = (dgvPathAnimation[3, i].Value == null) ? 0 : float.Parse(dgvPathAnimation[3, i].Value.ToString());

                        PathAnimationNode[i].ManualRotation.fYaw = (dgvPathAnimation[4,i].Value == null) ? 0 : float.Parse(dgvPathAnimation[4,i].Value.ToString()) ;
                        PathAnimationNode[i].ManualRotation.fPitch = (dgvPathAnimation[5, i].Value == null) ? 0 : float.Parse(dgvPathAnimation[5, i].Value.ToString());
                        PathAnimationNode[i].ManualRotation.fRoll = (dgvPathAnimation[6, i].Value == null) ? 0 : float.Parse(dgvPathAnimation[6, i].Value.ToString());

                        PathAnimationNode[i].ManualRotation.bRelativeToCurrOrientation = (dgvPathAnimation[7, i].Value == null || dgvPathAnimation[7, i].Value.ToString() == "") ? false : bool.Parse(dgvPathAnimation[7, i].Value.ToString());
                    }
                        

                    DNEPositionInterpolationMode PositionInterpolationMode = (DNEPositionInterpolationMode)Enum.Parse(typeof(DNEPositionInterpolationMode), cmbPositionInterpolationMode.Text);
                    DNERotationInterpolationMode RotationInterpolationMode = (DNERotationInterpolationMode)Enum.Parse(typeof(DNERotationInterpolationMode), cmbRotationInterpolationMode.Text);
                    m_CurrentObject.PlayPathAnimation(PathAnimationNode,
                                                        PositionInterpolationMode,
                                                        RotationInterpolationMode,
                                                        ntxAnimationStartingTime.GetFloat(),
                                                        ntxRotationAdditionalYaw.GetFloat(),
                                                        chxAutomaticRotation.Checked,
                                                        chxAnimationLoop.Checked);

                    // turn on all viewports render needed flags
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
                }                
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("PlayPathAnimation", McEx);
            }
        }

        private void btnRotateByItemApply_Click(object sender, EventArgs e)
        {
            try
            {
                DNSMcRotation rotation = new DNSMcRotation(ctrl3DOrientationRotateByItem.Yaw,
                                                                ctrl3DOrientationRotateByItem.Pitch,
                                                                ctrl3DOrientationRotateByItem.Roll,
                                                                chxRelativeToCurrOrientation.Checked);
                m_CurrentObject.RotateByItem(rotation);

                // turn on all viewports render needed flags
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("RotateByItem", McEx);
            }
        }

        private void btnStopAnimation_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.StopPathAnimation();

                // turn on all viewports render needed flags
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("StopPathAnimation", McEx);
            }
        }

        private void btnVisibilityApply_Click(object sender, EventArgs e)
        {
            try
            {
                DNEActionOptions actionOption = (DNEActionOptions)Enum.Parse(typeof(DNEActionOptions), cmbVisibility.Text);
                if (lstViewports.SelectedIndex >= 0)
                {
                    m_CurrentObject.SetVisibilityOption(actionOption,lViewportValue[lstViewports.SelectedIndex]);
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(lViewportValue[lstViewports.SelectedIndex]);
                }
                else
                {
                    m_CurrentObject.SetVisibilityOption(actionOption);
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetVisibilityOption", McEx);
            }
        }

        private void btnGetObjectToObjectAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                DNSObjectToObjectAttachmentParams ? objectToObjectAttachmentParams = null;
                objectToObjectAttachmentParams = m_CurrentObject.GetObjectToObjectAttachment(ntxAttachedLocationIndex.GetUInt32());
                FillObjectToObjectAttachmentParams(objectToObjectAttachmentParams);
                
                              
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjectToObjectAttachment", McEx);
            }
        }

        private void FillObjectToObjectAttachmentParams(DNSObjectToObjectAttachmentParams? attachmentParams)
        {
            if (attachmentParams.HasValue == true)
            {
                int listIdx = 0;
                bool objFound = false;
                while (listIdx < m_lstObjectValue.Count && objFound == false)
                {
                    if (m_lstObjectValue[listIdx] == attachmentParams.Value.pTargetObject)
                    {
                        objFound = true;
                        lstTargetObject.SelectedIndexChanged -=new EventHandler(lstTargetObject_SelectedIndexChanged);
                        lstTargetObject.SetSelected(listIdx, true);
                        lstTargetObject.SelectedIndexChanged += new EventHandler(lstTargetObject_SelectedIndexChanged);
                    }

                    listIdx++;
                }

                if (objFound == true)
                {
                    LoadSelectedObjectNodes();

                    int nodeIdx = 0;
                    bool IsFound = false;
                    while (nodeIdx < m_lstSchemeNodeValue.Count && IsFound == false)
                    {
                        if (m_lstSchemeNodeValue[nodeIdx] == attachmentParams.Value.pTargetNode)
                        {
                            objFound = true;
                            lstTargetNode.SetSelected(nodeIdx, true);
                        }

                        nodeIdx++;
                    }
                }

                cmbAttachPointType.Text = attachmentParams.Value.AttachPointParams.eType.ToString();
                ntxPointIndx.SetInt(attachmentParams.Value.AttachPointParams.nPointIndex);
                ntxNumPoints.SetInt(attachmentParams.Value.AttachPointParams.nNumPoints);
                ntxInterpolationRatio.SetFloat(attachmentParams.Value.AttachPointParams.fPositionValue);
                ctrl3DFVectorOffset.SetVector3D( attachmentParams.Value.Offset);

                for (int i = 0; i < clstBoundingBoxPointTypeBitField.Items.Count; i++)
                    clstBoundingBoxPointTypeBitField.SetItemChecked(i, false);

                DNEBoundingBoxPointFlags BoundingBoxFlag = attachmentParams.Value.AttachPointParams.eBoundingBoxPointTypeBitField;

                DNEBoundingBoxPointFlags[] BoundingBoxPointFlags = (DNEBoundingBoxPointFlags[])Enum.GetValues(typeof(DNEBoundingBoxPointFlags));
                int index = 0;
                foreach (DNEBoundingBoxPointFlags flag in BoundingBoxPointFlags)
                {
                    if (flag != DNEBoundingBoxPointFlags._EBBPF_NONE)
                    {
                        if ((BoundingBoxFlag & flag) == flag)
                            clstBoundingBoxPointTypeBitField.SetItemChecked(index, true);

                        ++index;
                    }
                }
            }
            else
            {
                lstTargetObject.ClearSelected();
                lstTargetNode.ClearSelected();
                cmbAttachPointType.Text = DNEAttachPointType._EAPT_ALL_POINTS.ToString();
                ntxPointIndx.SetInt(0);
                ntxNumPoints.SetInt(1);
                ntxInterpolationRatio.SetFloat(0);
                ctrl3DFVectorOffset.SetVector3D( new DNSMcFVector3D(0, 0, 0));

                for (int i = 0; i < clstBoundingBoxPointTypeBitField.Items.Count; i++)
                    clstBoundingBoxPointTypeBitField.SetItemChecked(i, false);
                    
            }
        }

        private void btnObjectToObjectCancelAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetObjectToObjectAttachment(ntxAttachedLocationIndex.GetUInt32(), null);

                DNSObjectToObjectAttachmentParams? objectToObjectAttachmentParams = m_CurrentObject.GetObjectToObjectAttachment(ntxAttachedLocationIndex.GetUInt32());
                FillObjectToObjectAttachmentParams(objectToObjectAttachmentParams);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetObjectToObjectAttachment", McEx);
            }
        }

        private void btnSetObjectToObjectAttacment_Click(object sender, EventArgs e)
        {
            try
            {
                if (ntxAttachedLocationIndex.GetUInt32() >= 0)
                {
                    DNSObjectToObjectAttachmentParams attachmentParams = new DNSObjectToObjectAttachmentParams();

                    if (lstTargetObject.SelectedIndex != -1)
                        attachmentParams.pTargetObject = m_lstObjectValue[lstTargetObject.SelectedIndex];
                    else
                        attachmentParams.pTargetObject = null;

                    if (lstTargetNode.SelectedIndex != -1)
                        attachmentParams.pTargetNode = m_lstSchemeNodeValue[lstTargetNode.SelectedIndex];
                    else
                        attachmentParams.pTargetNode = null;

                    attachmentParams.AttachPointParams.eType = (DNEAttachPointType)Enum.Parse(typeof(DNEAttachPointType), cmbAttachPointType.Text);
                    attachmentParams.AttachPointParams.nPointIndex = ntxPointIndx.GetInt32();
                    attachmentParams.AttachPointParams.nNumPoints = ntxNumPoints.GetInt32();
                    attachmentParams.AttachPointParams.fPositionValue = ntxInterpolationRatio.GetFloat();
                    attachmentParams.Offset = ctrl3DFVectorOffset.GetVector3D();

                    DNEBoundingBoxPointFlags bitField = DNEBoundingBoxPointFlags._EBBPF_NONE;
                    for (int i = 0; i < clstBoundingBoxPointTypeBitField.CheckedItems.Count; i++)
                    {
                        DNEBoundingBoxPointFlags flag = (DNEBoundingBoxPointFlags)Enum.Parse(typeof(DNEBoundingBoxPointFlags), clstBoundingBoxPointTypeBitField.CheckedItems[i].ToString());

                        bitField |= flag;
                    }

                    attachmentParams.AttachPointParams.eBoundingBoxPointTypeBitField = bitField;

                    m_CurrentObject.SetObjectToObjectAttachment(ntxAttachedLocationIndex.GetUInt32(), attachmentParams);

                    // turn on all viewports render needed flags
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }
                else
                    MessageBox.Show("Attached Location Index is not valid", "Attached Location Index", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetObjectToObjectAttachment", McEx);
            }
        }

        private void btnSetImageCalc_Click(object sender, EventArgs e)
        {
            frmCreateImageCalc CreateImageCalcForm = new frmCreateImageCalc();
            if (CreateImageCalcForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    m_CurrentObject.SetImageCalc(CreateImageCalcForm.ImageCalc);
                    btnSetImageCalc.Text = "Exist";

                    // turn on all viewports render needed flags
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetImageCalc", McEx);
                }
            }
            else
                btnSetImageCalc.Text = "Null";

        }

        private void btnDetectibility_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstViewports.SelectedIndex != -1)
                {
                    m_CurrentObject.SetDetectibility(chkObjectDetectibility.Checked, lViewportValue[lstViewports.SelectedIndex]);
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(lViewportValue[lstViewports.SelectedIndex]);
                }
                else
                {
                    m_CurrentObject.SetDetectibility(chkObjectDetectibility.Checked, null);
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetDetectibility", McEx);
            }
        }

        private void btnDrawPriority_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstViewports.SelectedIndex != -1)
                {
                    m_CurrentObject.SetDrawPriority(ntxDrawPriority.GetShort(), lViewportValue[lstViewports.SelectedIndex]);
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(lViewportValue[lstViewports.SelectedIndex]);
                }
                else
                {                    
                    m_CurrentObject.SetDrawPriority(ntxDrawPriority.GetShort(), null);
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetDrawPriority", McEx);
            }
        }

        private void btnClearViewportSelection_Click(object sender, EventArgs e)
        {
            lstViewports.ClearSelected();
            try
            {
                ntxDrawPriority.SetShort(m_CurrentObject.GetDrawPriority());
                chkObjectDetectibility.Checked = m_CurrentObject.GetDetectibility();
                byte[] abyStates = m_CurrentObject.GetState();
                string txtStates = "";
                if (abyStates != null)
                {
                    foreach (byte byState in abyStates)
                    {
                        txtStates += (byState.ToString() + " ");
                    }
                }
                antxObjectStates.Text = txtStates.Trim();
                chxEffectiveVisibilityInViewport.CheckState = CheckState.Indeterminate;
                txEffectiveStateInSelectedViewport.Text = "";
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetDrawPriority", McEx);
            }
        }

        private void lstViewports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstViewports.SelectedIndex != -1)
            {
                try
                {
                    chkObjectDetectibility.Checked = m_CurrentObject.GetDetectibility(lViewportValue[lstViewports.SelectedIndex]);
                    ntxDrawPriority.SetShort(m_CurrentObject.GetDrawPriority(lViewportValue[lstViewports.SelectedIndex]));
                    antxObjectStates.Text = "";
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetDetectibility", McEx);
                }

                try
                {
                    byte [] states = m_CurrentObject.GetState(lViewportValue[lstViewports.SelectedIndex]);
                    string strState = "";
                    if (states != null)
                    {
                        foreach (byte state in states)
                        {
                            strState += (state.ToString() + " ");
                        }
                    }
                    antxObjectStates.Text = strState.Trim();
                    
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetState", McEx);
                }

                try
                {
                    cmbVisibility.Text = m_CurrentObject.GetVisibilityOption(lViewportValue[lstViewports.SelectedIndex]).ToString();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetVisibilityOption", McEx);
                }

                try
                {
                    ntxDrawPriority.SetShort(m_CurrentObject.GetDrawPriority(lViewportValue[lstViewports.SelectedIndex]));
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetDrawPriority", McEx);
                }

                try
                {
                    if (m_CurrentObject.GetEffectiveVisibilityInViewport(lViewportValue[lstViewports.SelectedIndex]))
                    {
                        chxEffectiveVisibilityInViewport.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        chxEffectiveVisibilityInViewport.CheckState = CheckState.Unchecked;
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetEffectiveVisibilityInViewport", McEx);
                }

                try
                {
                    string buf = "";
                    byte[] states = m_CurrentObject.GetEffectiveState(lViewportValue[lstViewports.SelectedIndex]);
                    foreach (byte state in states)
                    {
                        buf += (state.ToString() + " ");
                    }
                    txEffectiveStateInSelectedViewport.Text = buf.Trim();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetEffectiveState", McEx);
                }
            }
        }

        private void btnGetScreenArrangementOffset_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcMapViewport viewport = null;
                if (lstScreenArrangementOffsetViewports.SelectedIndex != -1)
                    viewport = lViewportValue[lstScreenArrangementOffsetViewports.SelectedIndex];

                ctrl2DFVectorScreenArrangementOffset.SetVector2D(m_CurrentObject.GetScreenArrangementOffset(viewport));
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetScreenArrangementOffset", McEx);
            }
        }

        private void btnSetScreenArrangementOffset_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcMapViewport viewport = null;
                if (lstScreenArrangementOffsetViewports.SelectedIndex != -1)
                    viewport = lViewportValue[lstScreenArrangementOffsetViewports.SelectedIndex];

                m_CurrentObject.SetScreenArrangementOffset(viewport, ctrl2DFVectorScreenArrangementOffset.GetVector2D());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetScreenArrangementOffset", McEx);
            }
        }

        #region Public Properties
        public List<string> lViewportText
        {
            get { return m_lViewportText; }
            set { m_lViewportText = value; }
        }

        public List<IDNMcMapViewport> lViewportValue
        {
            get { return m_lViewportValue; }
            set { m_lViewportValue = value; }
        }

        public List<string> ObjectsTextList
        {
            get { return m_lstObjectText; }
            set { m_lstObjectText = value; }
        }

        public List<IDNMcObject> ObjectsValueList
        {
            get { return m_lstObjectValue; }
            set { m_lstObjectValue = value; }
        }

        public List<string> SchemeNodeTextList
        {
            get { return m_lstSchemeNodeText; }
            set { m_lstSchemeNodeText = value; }
        }

        public List<IDNMcObjectSchemeNode> SchemeNodeValueList
        {
            get { return m_lstSchemeNodeValue; }
            set { m_lstSchemeNodeValue = value; }
        }

        public IDNMcObject SelectedObject
        {
            get { return m_SelectedObject; }
            set { m_SelectedObject = value; }
        }
        #endregion

        private void lstTargetObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelectedObjectNodes();
        }

        private void LoadSelectedObjectNodes()
        {
            if (lstTargetObject.SelectedItem != null)
            {
                lstTargetNode.Items.Clear();
                m_lstSchemeNodeText.Clear();
                m_lstSchemeNodeValue.Clear();

                SelectedObject = m_lstObjectValue[lstTargetObject.SelectedIndex];
                IDNMcObjectSchemeNode[] nodes = SelectedObject.GetScheme().GetNodes(DNENodeKindFlags._ENKF_ANY_NODE);

                foreach (IDNMcObjectSchemeNode node in nodes)
                {
                    string name = Manager_MCNames.GetNameByObject(node);
                    m_lstSchemeNodeText.Add(name);
                    m_lstSchemeNodeValue.Add(node);
                }

                lstTargetNode.Items.AddRange(m_lstSchemeNodeText.ToArray());
            }
            else
            {
                lstTargetNode.Items.Clear();
                m_lstSchemeNodeText.Clear();
                m_lstSchemeNodeValue.Clear();
            }
        }

        private void antxObjectStates_Validating(object sender, CancelEventArgs e)
        {
            if (antxObjectStates.Text == string.Empty)
            {
                return;
            }

            string txObjectStates = antxObjectStates.Text;
            string txRepObjectStates = txObjectStates;
            while (((txRepObjectStates = txObjectStates.Replace("  "," "))) != txObjectStates)
            {
                txObjectStates = txRepObjectStates;
            }
            string[] vals = txObjectStates.Split(" ".ToCharArray());
            foreach (string val in vals)
            {
                byte byteValue = 0;
                if (!byte.TryParse(val, out byteValue))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(antxObjectStates, "Values must be byte (0..255) separated with blank(s)");
                    return;
                }
            }
        }

        private void antxObjectStates_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(antxObjectStates, "");
        }

        private void btnSetObjState_Click(object sender, EventArgs e)
        {
            string txtObjectStates = antxObjectStates.Text;
            byte[] abyStates = null;
            if (txtObjectStates != null && txtObjectStates.Trim() != string.Empty)
            {
                string txRepObjectStates = txtObjectStates;
                while (((txRepObjectStates = txtObjectStates.Replace("  ", " "))) != txtObjectStates)
                {
                    txtObjectStates = txRepObjectStates;
                }
                string[] antxStates = txtObjectStates.Split(" ".ToCharArray());
                abyStates = new byte[antxStates.Length];
                for (int i = 0; i < antxStates.Length; i++)
                {
                    abyStates[i] = byte.Parse(antxStates[i]);
                }
            }

            try
            {
                if (lstViewports.SelectedIndex >= 0)
                {
                    if (abyStates != null && abyStates.Length == 1)
                        m_CurrentObject.SetState(abyStates[0], lViewportValue[lstViewports.SelectedIndex]);
                    else
                        m_CurrentObject.SetState(abyStates, lViewportValue[lstViewports.SelectedIndex]);
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(lViewportValue[lstViewports.SelectedIndex]);
                }
                else
                {
                    if (abyStates != null && abyStates.Length == 1)
                        m_CurrentObject.SetState(abyStates[0]);
                    else
                        m_CurrentObject.SetState(abyStates);
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetState", McEx);
            }
            try
            {
                if (lstViewports.SelectedIndex >= 0)
                {
                    string buf = "";
                    byte[] states = m_CurrentObject.GetEffectiveState(lViewportValue[lstViewports.SelectedIndex]);
                    foreach (byte state in states)
                    {
                        buf += (state.ToString() + " ");
                    }
                    txEffectiveStateInSelectedViewport.Text = buf.Trim();
                }
                else
                {
                    txEffectiveStateInSelectedViewport.Text = "";
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetEffectiveState", McEx);
            }
        }

        private void cbLocationIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbLocationIndex.SelectedIndex >=0)
            {
                mSelectedLocationIndex = (uint)cbLocationIndex.SelectedIndex;
                GetObjectLocationData();
                UpdateGridPoints();
            }
        }

        private void llNodeName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (llNodeName.Tag != null)
            {
                Control control = Parent.Parent.Parent;
                MCObjectWorldTreeViewForm mcOverlayMangerTreeView = control as MCObjectWorldTreeViewForm;
                if (mcOverlayMangerTreeView != null)
                {
                    mcOverlayMangerTreeView.SelectNodeInTreeNode((uint)llNodeName.Tag.GetHashCode());
                }
            }
        }

        private void btnGetNodeByName_Click(object sender, EventArgs e)
        {
            if (ntxGetNodeByName.Text != "")
            {
                try
                {
                    IDNMcObjectSchemeNode mcObjectSchemeNode = m_CurrentObject.GetNodeByName(ntxGetNodeByName.Text);
                    if (mcObjectSchemeNode != null)
                    {
                        llNodeName.Text = Manager_MCNames.GetNameByObject(mcObjectSchemeNode);
                        llNodeName.Tag = mcObjectSchemeNode;
                    }
                    else
                        MessageBox.Show("Not exist node with name " + ntxGetNodeByName.Text, "IDNMcObjectScheme.GetNodeByName", MessageBoxButtons.OK);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetNodeByName", McEx);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please insert node name", "Invalid Node Name", MessageBoxButtons.OK);
            }
        }

        private void btnGetNodeByID_Click(object sender, EventArgs e)
        {
            bool isShowError = false;
            string strNodeId = ntxGetNodeById.Text;
            if (strNodeId != "")
            {
                uint nodeId;
                if (UInt32.TryParse(strNodeId, out nodeId))
                {
                    try
                    {
                        IDNMcObjectSchemeNode mcObjectSchemeNode = m_CurrentObject.GetNodeByID(nodeId);
                        if (mcObjectSchemeNode != null)
                        {
                            llNodeName.Text = Manager_MCNames.GetNameByObject(mcObjectSchemeNode);
                            llNodeName.Tag = mcObjectSchemeNode;
                        }
                        else
                            MessageBox.Show("IDNMcObjectScheme.GetNodeByID", "Not exist node with id " + nodeId, MessageBoxButtons.OK);

                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetNodeByID", McEx);
                        return;
                    }
                }
                else
                    isShowError = true;
            }
            else
                isShowError = true;

            if (isShowError)
                MessageBox.Show("Node Id should be positive number", "Invalid Node Id", MessageBoxButtons.OK);

        }

        private void UpdatePoints()
        {
           // btnTALUpdateLine_Click(null, null);
        }

        private void dgvLocationPointsSelectionChanged(bool isSelectRow, bool isSelectConsistencyRows)
        {
            btnRemoveLocationPoint.Enabled = isSelectRow;
            btnUpdateLocationPoint.Enabled = isSelectRow;
            btnAddLocationPoint.Enabled = isSelectRow;

            btnUpdateLocationPoints.Enabled = isSelectConsistencyRows;

        }

        private void btnRefreshPoints_Click(object sender, EventArgs e)
        {
            UpdateGridPoints();
            ctrlLocationPoints.SelectGrid();
        }

        private void btnSetSymbologyAmplifiers_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetSymbologyGraphicalProperties(ctrlSymbologySymbolID1.GetSymbolID(),
                    ctrlAmplifiers1.GetAmplifiers());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSymbologyAmplifiers", McEx);
                return;
            }
        }

        private void btnGetSymbologyAmplifiers_Click(object sender, EventArgs e)
        {
            try
            {
                DNSKeyVariantValue[] aAmplifiers;
                m_CurrentObject.GetSymbologyGraphicalProperties(out m_SymbologySymbolID, out aAmplifiers);
                ctrlSymbologySymbolID1.SetFullSymbolID(m_SymbologySymbolID);
                ctrlAmplifiers1.SetAmplifiers(m_CurrentObject, aAmplifiers);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSymbologyAmplifiers", McEx);
                return;
            }
        }

        private void GetSymbologyAmplifiers()
        {
            try
            {
                DNSKeyVariantValue[] aAmplifiers;
                
                m_CurrentObject.GetSymbologyGraphicalProperties(out m_SymbologySymbolID, out aAmplifiers);
                ctrlSymbologySymbolID1.SetFullSymbolID(m_SymbologySymbolID);
                ctrlAmplifiers1.SetAmplifiers(m_CurrentObject, aAmplifiers);

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSymbologyAmplifiers", McEx);
                return;
            }
        }

        private void btnSetSymbologyAnchorPointsAndGeometricAmplifiers_Click(object sender, EventArgs e)
        {
            try
            {
                DNSMcVector3D[] locationPoints;
                bool result = ctrlPointsGrid1.GetPoints(out locationPoints);
                if (result)
                {
                    m_CurrentObject.SetSymbologyAnchorPointsAndGeometricAmplifiers(locationPoints, ctrlGeometricAmplifiers1.GetGeometricAmplifiers());
                    if (Manager_MCTSymbology.IsExistAnchorPoints(m_CurrentObject))
                    {
                        RemoveTempAnchorPoints();
                        btnShowAnchorPoints_Click(null, null);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSymbologyAnchorPointsAndGeometricAmplifiers", McEx);
            }
        }

        private void GetSymbologyAnchorPointsAndGeometricAmplifiers()
        {
            try
            {
                DNSMcVector3D[] aAnchorPoints;
                DNSMcKeyFloatValue[] aGeometricAmplifiers;
                m_CurrentObject.GetSymbologyAnchorPointsAndGeometricAmplifiers(out aAnchorPoints, out aGeometricAmplifiers);

                ctrlGeometricAmplifiers1.SetGeometricAmplifiers(aGeometricAmplifiers);
                ctrlPointsGrid1.SetPoints(aAnchorPoints);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSymbologyAnchorPointsAndGeometricAmplifiers", McEx);
            }
        }

        private void btnShowAnchorPoints_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetTempOverlay() == null)
                    return;

                if (!Manager_MCTSymbology.IsExistAnchorPoints(m_CurrentObject))
                {
                    Manager_MCTSymbology.ShowAnchorPoints(m_CurrentObject);
                    btnShowAnchorPoints.Text = "Hide Anchor Points";
                }
                else
                {
                    RemoveTempAnchorPoints();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Show/Hide Anchor Points", McEx);
            }
        }

        private void RemoveTempAnchorPoints()
        {
            Manager_MCTSymbology.RemoveTempAnchorPoints(m_CurrentObject);
            btnShowAnchorPoints.Text = "Show Anchor Points";
        }


        private void tcObject_VisibleChanged(object sender, EventArgs e)
        {
          
        }

        private void btnClearTravSelection_Click(object sender, EventArgs e)
        {
            lstTraversabilityMapLayers.ClearSelected();
        }

        private Form m_containerForm;
        private Form GetParentForm(Control ctr)
        {
            if (ctr.Parent is Form)
                return ctr.Parent as Form;
            else
                return GetParentForm(ctr.Parent);
        }

        Control ctrlPoint;

        void MapObjectCtrl_MouseClickEvent(object sender, Point mouseLocation)
        {
            try
            {
                MCTMapFormManager.MapForm.MapObjectCtrl.MouseClickEvent -= new MouseClickEventArgs(MapObjectCtrl_MouseClickEvent);
                IDNMcMapViewport Viewport = MCTMapFormManager.MapForm.Viewport;
                DNSMcVector3D screenPoint = new DNSMcVector3D(mouseLocation.X, mouseLocation.Y, 0);

                if (m_CurrentObject.GetScheme() != null && m_CurrentObject.GetScheme().GetObjectLocation(mSelectedLocationIndex) != null)
                {
                    DNEMcPointCoordSystem locationCoordSystem;
                    m_CurrentObject.GetScheme().GetObjectLocation(mSelectedLocationIndex).GetCoordSystem(out locationCoordSystem);

                    if (locationCoordSystem == DNEMcPointCoordSystem._EPCS_SCREEN)
                    {
                        OnMapClick(screenPoint, screenPoint, locationCoordSystem, chxIsRelativeToDTM.Checked);
                    }
                    else
                    {
                        MCTMapClick.ConvertScreenLocationToObjectLocation(Viewport, screenPoint, locationCoordSystem, m_CurrentObject.GetImageCalc(), this, chxIsRelativeToDTM.Checked);
                    }

                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Convert points", McEx);
            }
            //show the form
           // m_containerForm.Show();
        }

        private void btnLocationPointOpertions_Click(object sender, EventArgs e)
        {
            m_containerForm = GetParentForm(this);
            //hide the container form
            m_containerForm.Hide();

            if (MCTMapFormManager.MapForm != null)
            {
                ctrlPoint = ctrl3DLocationPoint;
                MCTMapFormManager.MapForm.MapObjectCtrl.MouseClickEvent += new MouseClickEventArgs(MapObjectCtrl_MouseClickEvent);
            }
        }

        public void OnMapClick(DNSMcVector3D screenLocation, DNSMcVector3D location, DNEMcPointCoordSystem locationCoordSystem, bool isRelativeToDTM)
        {
            if (ctrlPoint is DataGridView)
            {
                DataGridView DGV = ctrlPoint as DataGridView;
                int lastRow = DGV.Rows.GetLastRow(DataGridViewElementStates.None);
                DGV.Rows.Add();
                switch (locationCoordSystem)
                {
                    case DNEMcPointCoordSystem._EPCS_WORLD:
                        DGV[0, lastRow].Value = location.x;
                        DGV[1, lastRow].Value = location.y;
                        DGV[2, lastRow].Value = location.z;
                        break;
                    case DNEMcPointCoordSystem._EPCS_SCREEN:
                        DGV[0, lastRow].Value = screenLocation.x;
                        DGV[1, lastRow].Value = screenLocation.y;
                        DGV[2, lastRow].Value = 0;
                        break;
                    case DNEMcPointCoordSystem._EPCS_IMAGE:
                        DGV[0, lastRow].Value = location.x;
                        DGV[1, lastRow].Value = location.y;
                        DGV[2, lastRow].Value = 0;
                        break;
                }
            }
            else if (ctrlPoint is Controls.Ctrl3DVector)
            {

                MCTester.Controls.Ctrl3DVector control = (MCTester.Controls.Ctrl3DVector)ctrlPoint;
                switch (locationCoordSystem)
                {
                    case DNEMcPointCoordSystem._EPCS_WORLD:
                        control.SetVector3D( location);
                        break;

                    case DNEMcPointCoordSystem._EPCS_SCREEN:
                        control.SetVector3D( new DNSMcVector3D(double.Parse(screenLocation.x.ToString()), double.Parse(screenLocation.y.ToString()), 0));
                        break;

                    case DNEMcPointCoordSystem._EPCS_IMAGE:
                        control.SetVector3D( location);
                        break;
                }

            }

            //show the form
            m_containerForm.Show();
        }

        public void OnMapClickError(DNEMcErrorCode eErrorCode, string functionName)
        {
            //show the form
            m_containerForm.Show();

            MessageBox.Show(IDNMcErrors.ErrorCodeToString(eErrorCode), functionName, MessageBoxButtons.OK);
        }
    }
}
