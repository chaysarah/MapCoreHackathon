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
using MCTester.Managers.MapWorld;
using MapCore.Common;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ucMeshItem : ucPhysicalItem, IUserControlItem
    {
        private IDNMcMeshItem m_CurrentObject;
        private IDNMcObject m_SubPartTransformObject;

        public ucMeshItem()
        {
            InitializeComponent();
            rdbPropertyID.CheckedChanged += new EventHandler(rdbPropertyID_CheckedChanged);
            rdbPropertyID.Checked = true;

            ctrlObjStatePropertyAnimation.HideObjStateTab();

        }

       
        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcMeshItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.LoadItem(aItem);
            ctrlObjStatePropertyTextureScroll.SetCurrentMesh(m_CurrentObject);
            ctrlObjStatePropertySubPartRotation.SetCurrentMesh(m_CurrentObject);
            ctrlObjStateSubPartInheritsParentRotation.SetCurrentMesh(m_CurrentObject);
            ctrlObjStatePropertySubPartOffset.SetCurrentMesh(m_CurrentObject);

            try
            {
                ctrlObjStatePropertyMesh.Load(m_CurrentObject.GetMesh);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetMesh", McEx);
            }

            try
            {
                ctrlObjStatePropertyBasePointAlignment.Load(m_CurrentObject.GetBasePointAlignment);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetBasePointAlignment", McEx);
            }

            try
            {
                ctrlObjStatePropertyAnimation.Load(m_CurrentObject.GetAnimation);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetAnimation", McEx);
            }

            try
            {
                ctrlObjStatePropertyCastShadows.Load(m_CurrentObject.GetCastShadows);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCastShadows", McEx);
            }

            try
            {
                chxParticipationInTerrainHeight.Checked = m_CurrentObject.GetParticipationInTerrainHeight();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetParticipationInTerrainHeight", McEx);
            }
            try
            {
                chxDisplayItemsAttachedToTerrain.Checked = m_CurrentObject.GetDisplayingItemsAttachedToTerrain();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetDisplayingItemsAttachedToTerrain", McEx);
            }
            try
            {
                chxStatic.Checked = m_CurrentObject.GetStatic();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetStatic", McEx);
            }
        }

        #endregion

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.SaveItem();

            try
            {
                ctrlObjStatePropertyMesh.Save(m_CurrentObject.SetMesh);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetMesh", McEx);
            }

            try
            {
                ctrlObjStatePropertyBasePointAlignment.Save(m_CurrentObject.SetBasePointAlignment);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetBasePointAlignment", McEx);
            }

            try
            {
                ctrlObjStatePropertyAnimation.Save(m_CurrentObject.SetAnimation);
                                                
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetAnimation", McEx);
            }

            try
            {
                ctrlObjStatePropertyCastShadows.Save(m_CurrentObject.SetCastShadows);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetCastShadows", McEx);
            }


            try
            {
                ctrlObjStatePropertyTextureScroll.Save(m_CurrentObject.SetTextureScrollSpeed);

                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetTextureScrollSpeed", McEx);
            }

            try
            {
                ctrlObjStatePropertySubPartRotation.Save(m_CurrentObject.SetSubPartRotation);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSubPartRotation", McEx);
            }

            try
            {
                ctrlObjStateSubPartInheritsParentRotation.Save(m_CurrentObject.SetSubPartInheritsParentRotation);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSubPartInheritsParentRotation", McEx);
            }

            try
            {
                ctrlObjStatePropertySubPartOffset.Save(m_CurrentObject.SetSubPartOffset);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSubPartOffset", McEx);
            }

            // turn on all viewports render needed flags
            Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        private void btnOpenObjList_Click(object sender, EventArgs e)
        {
            frmCurrRotationObjectList SubPartCurrRotationObjectList = new frmCurrRotationObjectList(m_CurrentObject);
            if (SubPartCurrRotationObjectList.ShowDialog() == DialogResult.OK)
            {
                m_SubPartTransformObject = SubPartCurrRotationObjectList.SelectedObject;
                lblSelectedObject.Text = "Selected";
                ntxPropertyIDSubPart.Enabled = true;
                ntxAttachPointID.Enabled = true;
                btnGetSubPartTransformCurrentRotation.Enabled = true;
            }

            
        }
        
        void rdbPropertyID_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPropertyID.Checked == true)
            {
                ntxPropertyIDSubPart.Enabled = true;
                ntxAttachPointID.Enabled = false;
            }
            else
            {
                ntxPropertyIDSubPart.Enabled = false;
                ntxAttachPointID.Enabled = true;
            }
        }

        private void btnGetSubPartTransformCurrentRotation_Click(object sender, EventArgs e)
        {
            DNSMcRotation outputRotation = new DNSMcRotation();

            if (rdbPropertyID.Checked)
            {
                try
                {
                    DNMcMeshItem.GetSubPartCurrRotation(MCTMapFormManager.MapForm.Viewport,
                                                            ntxPropertyIDSubPart.GetUInt32(),
                                                            m_SubPartTransformObject, 
                                                            chxRelativeToParentSubPart.Checked, 
                                                            ref outputRotation);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNMcMeshItem.GetSubPartCurrRotation", McEx);
                }   
            }
            
            if (rdbAttachPointID.Checked)
            {
                try
                {
                    m_CurrentObject.GetSubPartCurrRotation(MCTMapFormManager.MapForm.Viewport, m_SubPartTransformObject,
                        ntxAttachPointID.GetUInt32(), chxRelativeToParentSubPart.Checked, out outputRotation);
                }
                catch (MapCoreException McEx)
                {
            	    Utilities.ShowErrorMessage("GetSubPartCurrRotation", McEx);
                }   
            }
            
            ctrl3DOrientationSubPartCurrRotation.Yaw = outputRotation.fYaw;
            ctrl3DOrientationSubPartCurrRotation.Pitch = outputRotation.fPitch;
            ctrl3DOrientationSubPartCurrRotation.Roll = outputRotation.fRoll;
        }

        private void ucMeshItem_Load(object sender, EventArgs e)
        {

        }        
    }
}
