using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.ObjectWorld.Assit_Forms;
using MCTester.General_Forms;
using MCTester.Managers;
using MapCore.Common;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ucSymbolicItem : CtrlObjectSchemeItem, IUserControlItem
    {
        private IDNMcSymbolicItem m_CurrentObject;
        private IDNMcObjectSchemeNode[] parentsNode;
        private bool m_IsLoadAttachPointsData;
     
        public ucSymbolicItem():base()
        {
            InitializeComponent();

            m_IsLoadAttachPointsData = false;

            ctrlObjStatePropertySegmentType.SetEnumList(typeof(DNESegmentType));

            cmbOffsetType.Items.AddRange(Enum.GetNames(typeof(DNEItemGeometryType)));
            cmbOffsetType.Text = DNEItemGeometryType._EGT_GEOMETRIC_IN_OVERLAY_MANAGER.ToString();
                      
            cmbCoordSysConversion.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            cmbAlignToCoordSys.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));

            cmbMinificationFilter.Items.AddRange(Enum.GetNames(typeof(DNETextureFilter)));
            cmbMagnificationFilter.Items.AddRange(Enum.GetNames(typeof(DNETextureFilter)));
            cmbMipmapDeterminationFilter.Items.AddRange(Enum.GetNames(typeof(DNETextureFilter)));
        }

        #region IUserControlItem Members
        
        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcSymbolicItem)aItem;

            ctrlObjStatePropertyEAttachPointType.HideObjStateTab();

            parentsNode = m_CurrentObject.GetParents();
            int index = 0;
            foreach (IDNMcObjectSchemeNode node in parentsNode)
            {
                cmbAPTypeParentIndex.Items.Add(index + " : " + Manager_MCNames.GetNameByObject(node, node.GetNodeType().ToString()));
                index++;
            }
            if (parentsNode.Length > 0)
                cmbAPTypeParentIndex.SelectedIndex = 0;
            base.LoadItem(aItem);

          
            try
            {
                DNETextureFilter minFilter, magFilter, mipmapFilter;
                m_CurrentObject.GetTextureFiltering(out minFilter, out magFilter, out mipmapFilter);

                cmbMinificationFilter.Text = minFilter.ToString();
                cmbMagnificationFilter.Text = magFilter.ToString();
                cmbMipmapDeterminationFilter.Text = mipmapFilter.ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTextureFiltering", McEx);
            }

            try
            {
                ctrlObjStatePropertyDrawPriority.Load(m_CurrentObject.GetDrawPriority);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetDrawPriority", McEx);
            }

            try
            {
                ctrlObjStatePropertyCoplanar3DPriority.Load(m_CurrentObject.GetCoplanar3DPriority);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCoplanar3DPriority", McEx);
            }


            try
            {
                ctrlObjStatePropertyDrawPriorityGroup.Load(m_CurrentObject.GetDrawPriorityGroup);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetDrawPriorityGroup", McEx);
            }
           
            try
            {
                ctrlObjStatePropertyMoveIfBlockedMaxChange.Load(m_CurrentObject.GetMoveIfBlockedMaxChange);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetMoveIfBlockedMaxChange", McEx);
            }

            try
            {
                ctrlObjStatePropertyMoveIfBlockedHeightAboveObstacle.Load(m_CurrentObject.GetMoveIfBlockedHeightAboveObstacle);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetMoveIfBlockedHeightAboveObstacle", McEx);
            }

            try
            {
                ctrlObjStatePropertySubItemData.Load(m_CurrentObject.GetSubItemsData);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSubItemsData", McEx);
            }

            try
            {
                ctrlObjStatePropertyTransparency.Load(m_CurrentObject.GetTransparency);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTransparency", McEx);
            }

            GetTransforms();

            //Get only if node connected to at least one parent 
            //if (cmbAPTypeParentIndex.Text != "")
            //{
                
            //    GetAttachedPoints();                
            //}
        }

        //Set attached points parameters
        private void SetAttachedPoints()
        {
            try
            {
                ctrlObjStatePropertyEAttachPointType.Save(m_CurrentObject.SetAttachPointType, AttachPointParentIndex);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetAttachPointType", McEx);
            }

            try
            {
                ctrlObjStatePropertyAPIndex.Save(m_CurrentObject.SetAttachPointIndex, AttachPointParentIndex);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetAttachPointIndex", McEx);
            }

            try
            {
                ctrlObjStatePropertyNumAttachPoints.Save(m_CurrentObject.SetNumAttachPoints, AttachPointParentIndex);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetNumAttachPoints", McEx);
            }

            try
            {
                ctrlObjStatePropertyAPPositionValue.Save(m_CurrentObject.SetAttachPointPositionValue, AttachPointParentIndex);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetAttachPointPositionValue", McEx);
            }

            try
            {
                ctrlObjStatePropertyEBoundingBoxPointFlags1.Save(m_CurrentObject.SetBoundingBoxAttachPointType, AttachPointParentIndex);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetBoundingBoxAttachPointType", McEx);
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        //Get attached Points parameters
        private void GetAttachedPoints()
        {
            if (m_IsLoadAttachPointsData == false)
            {
                try
                {
                    ctrlObjStatePropertyEAttachPointType.Load(m_CurrentObject.GetAttachPointType, AttachPointParentIndex);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetAttachPointType", McEx);
                }

                try
                {
                    ctrlObjStatePropertyAPIndex.Load(m_CurrentObject.GetAttachPointIndex, AttachPointParentIndex);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetAttachPointIndex", McEx);
                }

                try
                {
                    ctrlObjStatePropertyNumAttachPoints.Load(m_CurrentObject.GetNumAttachPoints, AttachPointParentIndex);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetNumAttachPoints", McEx);
                }

                try
                {
                    ctrlObjStatePropertyAPPositionValue.Load(m_CurrentObject.GetAttachPointPositionValue, AttachPointParentIndex);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetAttachPointPositionValue", McEx);
                }

                try
                {
                    ctrlObjStatePropertyEBoundingBoxPointFlags1.Load(m_CurrentObject.GetBoundingBoxAttachPointType, AttachPointParentIndex);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetBoundingBoxAttachPointType", McEx);
                }
            }
            m_IsLoadAttachPointsData = true;
        }

        //Set Transform parameters
        private void SetTransforms()
        {
            try
            {
                m_CurrentObject.SetVectorTransformParentIndex(ntxVectorTransParentIndex.GetUInt32());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetVectorTransformParentIndex", McEx);
            }

            try
            {
                ctrlObjStatePropertySegmentType.Save(m_CurrentObject.SetVectorTransformSegment);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetVectorTransformSegment", McEx);
            }


            try
            {
                ctrlObjStatePropertyEVectorOffsetCalc.Save(m_CurrentObject.SetVectorOffsetCalc);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetVectorOffsetCalc", McEx);
            }

            try
            {
                ctrlObjStatePropertyEOffsetOrientation.Save(m_CurrentObject.SetOffsetOrientation);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetOffsetOrientation", McEx);
            }

            try
            {
                ctrlObjStatePropertyVectorOffsetValue.Save(m_CurrentObject.SetVectorOffsetValue);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetVectorOffsetValue", McEx);
            }

            try
            {
                ctrlObjStatePropertyPointsDuplication.Save(m_CurrentObject.SetPointsDuplication);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetPointsDuplication", McEx);
            }

            try
            {
                ctrlObjStatePropertyPointsDuplicationOffsets.Save(m_CurrentObject.SetPointsDuplicationOffsets);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetPointsDuplicationOffsets", McEx);
            }

            try
            {
                DNEMcPointCoordSystem coordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbAlignToCoordSys.Text);

                m_CurrentObject.SetRotationAlignment(coordSys,
                                                        chxAlignYaw.Checked,
                                                        chxAlignPitch.Checked,
                                                        chxAlignRoll.Checked);

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetRotationAlignment", McEx);
            }

            try
            {
                ctrlObjStatePropertyRotationYaw.Save(m_CurrentObject.SetRotationYaw);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetRotationYaw", McEx);
            }

            try
            {
                ctrlObjStatePropertyRotationPitch.Save(m_CurrentObject.SetRotationPitch);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetRotationPitch", McEx);
            }

            try
            {
                ctrlObjStatePropertyRotationRoll.Save(m_CurrentObject.SetRotationRoll);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetRotationRoll", McEx);
            }

            try
            {
                ctrlPropertyOffset.Save(m_CurrentObject.SetOffset);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetOffset", McEx);
            }

            try
            {
                ctrlObjStatePropertyVectorRotation.Save(m_CurrentObject.SetVectorRotation);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetVectorRotation", McEx);
            }

            try
            {
                DNEMcPointCoordSystem PtCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbCoordSysConversion.Text);
                m_CurrentObject.SetCoordinateSystemConversion(PtCoordSys,
                                                                chxCoordSysEnabled.Checked);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetCoordinateSystemConversion", McEx);
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        //Load methods in Transform tab controls
        private void GetTransforms()
        {
            try
            {
                ntxVectorTransParentIndex.SetUInt32(m_CurrentObject.GetVectorTransformParentIndex());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVectorTransformParentIndex", McEx);
            }

            try
            {
                ctrlObjStatePropertySegmentType.Load(m_CurrentObject.GetVectorTransformSegment);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVectorTransformSegment", McEx);
            }

            try
            {
                cmbOffsetType.Text = m_CurrentObject.GetOffsetType().ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetOffsetType", McEx);
            }

            try
            {
                ctrlObjStatePropertyEVectorOffsetCalc.Load(m_CurrentObject.GetVectorOffsetCalc);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVectorOffsetCalc", McEx);
            }

            try
            {
                ctrlObjStatePropertyEOffsetOrientation.Load(m_CurrentObject.GetOffsetOrientation);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetOffsetOrientation", McEx);
            }

            try
            {
                ctrlObjStatePropertyVectorOffsetValue.Load(m_CurrentObject.GetVectorOffsetValue);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVectorOffsetValue", McEx);
            }

            try
            {
                ctrlObjStatePropertyPointsDuplication.Load(m_CurrentObject.GetPointsDuplication);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetPointsDuplication", McEx);
            }

            try
            {
                ctrlObjStatePropertyPointsDuplicationOffsets.Load(m_CurrentObject.GetPointsDuplicationOffsets);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetPointsDuplicationOffsets", McEx);
            }

            try
            {
                ctrlPropertyOffset.Load(m_CurrentObject.GetOffset);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetOffset", McEx);
            }

            try
            {
                DNEMcPointCoordSystem alignToCoordSys;
                bool yaw, pitch, roll;

                m_CurrentObject.GetRotationAlignment(out alignToCoordSys,
                                                        out yaw,
                                                        out pitch,
                                                        out roll);

                cmbAlignToCoordSys.Text = alignToCoordSys.ToString();
                chxAlignYaw.Checked = yaw;
                chxAlignPitch.Checked = pitch;
                chxAlignRoll.Checked = roll;

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetRotationAlignment", McEx);
            }

            try
            {
                ctrlObjStatePropertyRotationYaw.Load(m_CurrentObject.GetRotationYaw);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetRotationYaw", McEx);
            }

            try
            {
                ctrlObjStatePropertyRotationPitch.Load(m_CurrentObject.GetRotationPitch);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetRotationPitch", McEx);
            }

            try
            {
                ctrlObjStatePropertyRotationRoll.Load(m_CurrentObject.GetRotationRoll);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetRotationRoll", McEx);
            }
            
            try
            {
                ctrlObjStatePropertyVectorRotation.Load(m_CurrentObject.GetVectorRotation);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVectorRotation", McEx);
            }

            try
            {
                DNEMcPointCoordSystem coordSystem;
                bool bEnable;

                m_CurrentObject.GetCoordinateSystemConversion(out coordSystem,
                                                                out bEnable);

                cmbCoordSysConversion.Text = coordSystem.ToString();
                chxCoordSysEnabled.Checked = bEnable;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCoordinateSystemConversion", McEx);
            }


        }

        #endregion

        
        #region Public Property

        public uint AttachPointParentIndex
        {
            get { return (uint)cmbAPTypeParentIndex.SelectedIndex; }
            set { cmbAPTypeParentIndex.SelectedIndex = int.Parse(value.ToString()); }
        }
        #endregion

        protected override void SaveItem()
        {
            base.SaveItem();
            
            try
            {
                DNETextureFilter minFilter = (DNETextureFilter)Enum.Parse(typeof(DNETextureFilter), cmbMinificationFilter.Text);
                DNETextureFilter magFilter = (DNETextureFilter)Enum.Parse(typeof(DNETextureFilter), cmbMagnificationFilter.Text);
                DNETextureFilter mipmapFilter = (DNETextureFilter)Enum.Parse(typeof(DNETextureFilter), cmbMipmapDeterminationFilter.Text);

                m_CurrentObject.SetTextureFiltering(minFilter, magFilter, mipmapFilter);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetTextureFiltering", McEx);
            }

            try
            {
                ctrlObjStatePropertyDrawPriority.Save(m_CurrentObject.SetDrawPriority);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetDrawPriority", McEx);
            }

            try
            {
                ctrlObjStatePropertyCoplanar3DPriority.Save(m_CurrentObject.SetCoplanar3DPriority);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetCoplanar3DPriority", McEx);
            }

            try
            {
                ctrlObjStatePropertyDrawPriorityGroup.Save(m_CurrentObject.SetDrawPriorityGroup); 
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetDrawPriorityGroup", McEx);
            }

            try
            {
                ctrlObjStatePropertyMoveIfBlockedMaxChange.Save(m_CurrentObject.SetMoveIfBlockedMaxChange);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetMoveIfBlockedMaxChange", McEx);
            }

            try
            {
                ctrlObjStatePropertyMoveIfBlockedHeightAboveObstacle.Save(m_CurrentObject.SetMoveIfBlockedHeightAboveObstacle);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetMoveIfBlockedHeightAboveObstacle", McEx);
            }

            try
            {
                ctrlObjStatePropertySubItemData.Save(m_CurrentObject.SetSubItemsData);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSubItemsData", McEx);
            }

            try
            {
                ctrlObjStatePropertyTransparency.Save(m_CurrentObject.SetTransparency);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetTransparency", McEx);
            }

            SetTransforms();

            //set only if node connected to at least one parent 
            if (cmbAPTypeParentIndex.Text != "")
            {
                SetAttachedPoints();                
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            
        }

        private void cmbAPTypeParentIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAPTypeParentIndex.Text != "")
            {
                try
                {
                    m_IsLoadAttachPointsData = false;
                    GetAttachedPoints();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetAttachPointType", McEx);
                }
            }   
        }

        private void ucSymbolicItem_Load(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnShowCalculatedPointsCoordinates_Click(object sender, EventArgs e)
        {
            frmCalculatedCoordinates CalculatedCoordinates = new frmCalculatedCoordinates(m_CurrentObject);
            CalculatedCoordinates.ShowDialog();
        }
    }
}
