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

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ucPhysicalItem : CtrlObjectSchemeItem , IUserControlItem
    {
        private IDNMcPhysicalItem m_CurrentObject;
        private uint m_PropID;
        private bool m_bParam;
        //private uint m_uParam;
        private DNSMcRotation m_Rotation;
        //private DNSMcFVector3D m_FVector3DParam;
        private IDNMcObject m_objectCurrRotation;
                
        public ucPhysicalItem()
        {
            InitializeComponent();
            //Selection option does not exist in this properties.
            ctrlPropertyRotation.HideSelectionTab();
            ctrlPropertyInheritsParentRotation.HideSelectionTab(); 
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcPhysicalItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            ctrlComboBoxObjectListColorModulate.InitializeControlParams(m_CurrentObject);
            ctrlComboBoxObjectListWireFrame.InitializeControlParams(m_CurrentObject);
            ctrlComboBoxObjectListColorModulate.cmbObjectsList.SelectedIndexChanged +=new EventHandler(cmbObjectsList_SelectedIndexChanged);
            ctrlComboBoxObjectListWireFrame.cmbObjectsList.SelectedIndexChanged +=new EventHandler(cmbWireFrameObjectList_SelectedIndexChanged);

            base.LoadItem(aItem);

            try
            {
                ctrlObjStatePropertyParallelToTerrain.Load(m_CurrentObject.GetParallelToTerrain);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetParallelToTerrain", McEx);
            }

            if (m_CurrentObject.GetParents().Length > 0)
            {
                try
                {
                    ctrlObjStatePropertyAttachPoint.Load(m_CurrentObject.GetAttachPoint);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetAttachPoint", McEx);
                }
            }

            try
            {
                m_CurrentObject.GetRotation(out m_Rotation,
                                                out m_PropID);

                ctrlPropertyRotation.SetRegRotationVal(m_Rotation);
                ctrlPropertyRotation.RegPropertyID = m_PropID;

                ctrlPropertyRotation.GetCtrlGui();

                ctrlPropertyRotation.PhisicalItem = m_CurrentObject;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRotation", McEx);
            }

            try
            {
                ctrlPropertyPhysicalScale.Load(m_CurrentObject.GetScale);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetScale", McEx);
            }
            try
            {
                ctrlPropertyPhysicalOffset.Load(m_CurrentObject.GetOffset);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetOffset", McEx);
            }

            try
            {
                m_CurrentObject.GetInheritsParentRotation(out m_bParam,
                                                            out m_PropID);

                ctrlPropertyInheritsParentRotation.RegBoolVal = m_bParam;
                ctrlPropertyInheritsParentRotation.RegPropertyID = m_PropID;

                ctrlPropertyInheritsParentRotation.GetCtrlGui();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetInheritsParentRotation", McEx);
            }
        }

        #endregion

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.SaveItem();

            try
            {
                ctrlObjStatePropertyParallelToTerrain.Save(m_CurrentObject.SetParallelToTerrain);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetParallelToTerrain", McEx);
            }

            //Set attach point only if there are at least one parent
            if (m_CurrentObject.GetParents().Length > 0)
            {
                try
                {
                    ctrlObjStatePropertyAttachPoint.Save(m_CurrentObject.SetAttachPoint);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetAttachPoint", McEx);
                }
            }

            try
            {
                ctrlPropertyPhysicalScale.Save(m_CurrentObject.SetScale);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetScale", McEx);
            }

            try
            {
                ctrlPropertyPhysicalOffset.Save(m_CurrentObject.SetOffset);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetOffset", McEx);
            }

            try
            {
                m_CurrentObject.SetInheritsParentRotation(ctrlPropertyInheritsParentRotation.RegBoolVal,
                                                            ctrlPropertyInheritsParentRotation.RegPropertyID);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetInheritsParentRotation", McEx);
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        private void btnCurrRotationObjectList_Click(object sender, EventArgs e)
        {
            frmCurrRotationObjectList objectList = new frmCurrRotationObjectList(m_CurrentObject);
            if (objectList.ShowDialog() == DialogResult.OK)
            {
                m_objectCurrRotation = objectList.SelectedObject;
                lblSelectedObj.Text = "Selected";
                btnGetCurrRotation.Enabled = true;
            }
        }

        private void btnGetCurrRotation_Click(object sender, EventArgs e)
        {
            if (rdbRotationBaseOnItem.Checked == true)
            {
                try
                {
                    if (m_objectCurrRotation != null)
                    {
                        m_CurrentObject.GetCurrRotation(MCTMapFormManager.MapForm.Viewport,
                                                        m_objectCurrRotation,
                                                        chxRelativeToParent.Checked,
                                                        out m_Rotation);

                        ctrl3DOrientationCurrRotation.Yaw = m_Rotation.fYaw;
                        ctrl3DOrientationCurrRotation.Pitch = m_Rotation.fPitch;
                        ctrl3DOrientationCurrRotation.Roll = m_Rotation.fRoll;
                    }
                    else
                        MessageBox.Show("choose object first!!!");
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCurrRotation", McEx);
                }
            }
            else
            {
                try
                {
                    if (m_objectCurrRotation != null)
                    {
                        DNMcPhysicalItem.GetCurrRotation(MCTMapFormManager.MapForm.Viewport,
                                                            ntxPropertyID.GetUInt32(),
                                                            m_objectCurrRotation,
                                                            chxRelativeToParent.Checked,
                                                            ref m_Rotation);

                        ctrl3DOrientationCurrRotation.Yaw = m_Rotation.fYaw;
                        ctrl3DOrientationCurrRotation.Pitch = m_Rotation.fPitch;
                        ctrl3DOrientationCurrRotation.Roll = m_Rotation.fRoll;
                    }
                    else
                        MessageBox.Show("choose object first!!!");
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCurrRotation", McEx);
                }
            }
        }

        void cmbObjectsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            IDNMcObject selectedObj = ctrlComboBoxObjectListColorModulate.GetSelectedObject();

            if (selectedObj != null)
            {
                try
                {
                    bool bEnable;
                    DNSMcFColor FColor = new DNSMcFColor();
                    float fFadeTimeMs;

                    m_CurrentObject.GetColorModulateEffect(selectedObj,
                                                            out bEnable,
                                                            out FColor,
                                                            out fFadeTimeMs);

                    chxColorModulateEffectEnable.Checked = bEnable;
                    picbColor.BackColor = Color.FromArgb((int)FColor.a, (int)FColor.r, (int)FColor.g, (int)FColor.b);
                    numUDAlphaColor.Value = (decimal)FColor.a;
                    ntxFadeTimeMS.SetFloat(fFadeTimeMs);
                }
                catch (MapCoreException McEx)
                {
                	MapCore.Common.Utilities.ShowErrorMessage("GetColorModulateEffect", McEx);
                }
            }            
        }

        void cmbWireFrameObjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            IDNMcObject selectedObj = ctrlComboBoxObjectListWireFrame.GetSelectedObject();

            if (selectedObj != null)
            {
                try
                {
                    bool bEnable;
                    DNSMcFColor FColor = new DNSMcFColor();
                    float fFadeTimeMs;
                    bool wireFrameOnly;

                    m_CurrentObject.GetWireFrameEffect(selectedObj,
                                                            out bEnable,
                                                            out FColor,
                                                            out fFadeTimeMs,
                                                            out wireFrameOnly);

                    chxWireFrameEffectEnabled.Checked = bEnable;
                    picWireFrameEffectColor.BackColor = Color.FromArgb((int)FColor.a, (int)FColor.r, (int)FColor.g, (int)FColor.b);
                    numUDWireFrameEffect.Value = (decimal)FColor.a;
                    ntxWireFrameEffectFadeTimeMS.SetFloat(fFadeTimeMs);
                    chxWireFrameEffectOnly.Checked = wireFrameOnly;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetWireFrameEffect", McEx);
                }
            }    
        }

        private void btnColorDlg_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            colorDialog.Color = picbColor.BackColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                picbColor.BackColor = colorDialog.Color;
                //numUDAlphaColor.Value = colorDialog.Color.A;
            }
        }

        private void btnWireFrameEffectColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            colorDialog.Color = picWireFrameEffectColor.BackColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                picWireFrameEffectColor.BackColor = colorDialog.Color;
                //numUDWireFrameEffect.Value = colorDialog.Color.A;
            }
        }        

        private void btnColorModulateEffectApply_Click(object sender, EventArgs e)
        {
            try
            {
                DNSMcFColor modulateEffectColor = new DNSMcFColor(picbColor.BackColor.R, picbColor.BackColor.G, picbColor.BackColor.B, (float)numUDAlphaColor.Value);
                m_CurrentObject.SetColorModulateEffect(ctrlComboBoxObjectListColorModulate.GetSelectedObject(),
                                                        modulateEffectColor,
                                                        ntxFadeTimeMS.GetFloat());

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetColorModulateEffect", McEx);
            }
        }

        private void btnColorModulateEffectRemove_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.RemoveColorModulateEffect(ctrlComboBoxObjectListColorModulate.GetSelectedObject());

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("RemoveColorModulateEffect", McEx);
            }
        }

        private void btnWireFrameEffectApply_Click(object sender, EventArgs e)
        {
            try
            {
                DNSMcFColor wireFrameEffectColor = new DNSMcFColor(picWireFrameEffectColor.BackColor.R, picWireFrameEffectColor.BackColor.G, picWireFrameEffectColor.BackColor.B, (float)numUDWireFrameEffect.Value);
                m_CurrentObject.SetWireFrameEffect(ctrlComboBoxObjectListWireFrame.GetSelectedObject(),
                                                        wireFrameEffectColor,
                                                        ntxWireFrameEffectFadeTimeMS.GetFloat(),
                                                        chxWireFrameEffectOnly.Checked);

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetWireFrameEffect", McEx);
            }
        }

        private void btnWireFrameEffectRemove_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.RemoveWireFrameEffect(ctrlComboBoxObjectListWireFrame.GetSelectedObject());

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("RemoveWireFrameEffect", McEx);
            }
        }

        private void ucPhysicalItem_Load(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
