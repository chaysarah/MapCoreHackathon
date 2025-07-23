using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;
using MapCore;
using MCTester.Managers.ObjectWorld;
using MCTester.GUI.Map;
using MCTester.Managers.MapWorld;
using MCTester.Managers;

namespace MCTester.ButtonsImplementation
{
    public partial class btnAnimationStateForm : Form
    {
        private List<string> m_ObjectText = new List<string>();
        private List<IDNMcObject> m_ObjectValue = new List<IDNMcObject>();

        private List<string> m_MeshItemText = new List<string>();
        private List<IDNMcMeshItem> m_MeshItemValue = new List<IDNMcMeshItem>();

        private List<string> m_AnimationStateText = new List<string>();
        private List<IDNMcAnimationState> m_AnimationStateValue = new List<IDNMcAnimationState>();

        public btnAnimationStateForm()
        {
            InitializeComponent();
        }

        private void btnAnimationStateForm_Load(object sender, EventArgs e)
        {
            cmbObjects.DisplayMember = "ObjectText";
            cmbObjects.ValueMember = "ObjectValue";

            cmbMeshItems.DisplayMember = "MeshItemText";
            cmbMeshItems.ValueMember = "MeshItemValue";

            cmbMeshItems.DisplayMember = "AnimationStateText";
            cmbMeshItems.ValueMember = "AnimationStateValue";


            if (Manager_MCOverlayManager.ActiveOverlayManager != null)
            {
                IDNMcObjectScheme[] objectSchemes = Manager_MCOverlayManager.ActiveOverlayManager.GetObjectSchemes();

                foreach (IDNMcObjectScheme objectScheme in objectSchemes)
                {
                    if (!Manager_MCObjectScheme.IsTempObjectScheme(objectScheme))
                    {
                        IDNMcObject[] objects = objectScheme.GetObjects();

                        foreach (IDNMcObject obj in objects)
                        {
                            ObjectText.Add(Manager_MCNames.GetNameByObject(obj, "Object"));
                            ObjectValue.Add(obj);
                        }
                    }
                }

                cmbObjects.Items.AddRange(ObjectText.ToArray());
            }            
        }

        private void cmbObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            IDNMcObject selectedObj = ObjectValue[cmbObjects.SelectedIndex];
            IDNMcObjectSchemeNode [] items = selectedObj.GetScheme().GetNodes(DNENodeKindFlags._ENKF_PHYSICAL_ITEM);
            
            MeshItemText.Clear();
            MeshItemValue.Clear();
            cmbMeshItems.Items.Clear();
            cmbMeshItems.Text = "";

            foreach (IDNMcObjectSchemeNode item in items)
            {
                if (item.GetNodeType() == DNEObjectSchemeNodeType._MESH_ITEM)
                {
                    MeshItemText.Add(Manager_MCNames.GetNameByObject(item,"Mesh Item"));
                    MeshItemValue.Add((IDNMcMeshItem)item);
                }                
            }

            cmbMeshItems.Items.AddRange(MeshItemText.ToArray());

            FillAttachAnimationNameComboBox();
        }

        private void cmbMeshItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            ntxMeshItemID.SetUInt32((MeshItemValue[((ComboBox)sender).SelectedIndex]).GetID());

            IDNMcAnimationState [] animationStates = (MeshItemValue[((ComboBox)sender).SelectedIndex]).GetAnimationStates(ObjectValue[cmbObjects.SelectedIndex]);
            AddNewAnimationState(animationStates);

            FillAttachAnimationNameComboBox();
        }

        private void ntxMeshItemID_TextChanged(object sender, EventArgs e)
        {
            foreach (IDNMcMeshItem mesh in MeshItemValue)
            {
                if (mesh.GetID() == ntxMeshItemID.GetUInt32())
                {
                    cmbMeshItems.SelectedIndex = MeshItemValue.IndexOf(mesh);
                    IDNMcAnimationState[] animationStates = (MeshItemValue[cmbMeshItems.SelectedIndex]).GetAnimationStates(ObjectValue[cmbObjects.SelectedIndex]);
                    AddNewAnimationState(animationStates);
                    return;
                }
            }
        }

        private void AddNewAnimationState(IDNMcAnimationState [] animationStates)
        {
            lstAnimationState.Items.Clear();
            AnimationStateText.Clear();
            AnimationStateValue.Clear();
            
            foreach (IDNMcAnimationState state in animationStates)
            {
                AnimationStateText.Add("(" + state.GetHashCode().ToString() + ") Animation State");
                AnimationStateValue.Add(state);
            }

            lstAnimationState.Items.AddRange(AnimationStateText.ToArray());
        }

        #region Public Properties
        public List<string> MeshItemText
        {
            get { return m_MeshItemText; }
            set { m_MeshItemText = value; }
        }

        public List<IDNMcMeshItem> MeshItemValue
        {
            get { return m_MeshItemValue; }
            set { m_MeshItemValue = value; }
        }

        public List<string> ObjectText
        {
            get { return m_ObjectText; }
            set { m_ObjectText = value; }
        }

        public List<IDNMcObject> ObjectValue
        {
            get { return m_ObjectValue; }
            set { m_ObjectValue = value; }
        }

        public List<string> AnimationStateText
        {
            get { return m_AnimationStateText; }
            set { m_AnimationStateText = value; }
        }

        public List<IDNMcAnimationState> AnimationStateValue
        {
            get { return m_AnimationStateValue; }
            set { m_AnimationStateValue = value; }
        }

        public IDNMcAnimationState SelectedAnimationState
        {
            get
            {
                if (lstAnimationState.SelectedIndex != -1)
                    return AnimationStateValue[lstAnimationState.SelectedIndex];                    
                else
                {
                    MessageBox.Show("Please choose animation state from the list", "Unchosen Animation State", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
            }
        }
        
        #endregion

        private void btnCreateAnimationStateUsingMeshItem_Click(object sender, EventArgs e)
        {
            DialogResult dlgRes = MessageBox.Show("Do you wish to create with default values?", "Create Options", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dlgRes == DialogResult.Yes)
            {
                cmbAttachedAnimationName.Text = "";
                chxEnabled.Checked = false;
                ntxTimePoint.SetFloat(0);
                ntxSpeedFactor.SetFloat(1);
                ntxWeight.SetFloat(1);
                ntxLength.SetFloat(0);
                chxLoop.Checked = true;
                ntxTimeDelay.SetFloat(0);
            }

            try
            {
                if (cmbObjects.SelectedIndex >= 0 && cmbMeshItems.SelectedIndex >= 0)
                {
                    IDNMcAnimationState animationState = DNMcAnimationState.Create(ObjectValue[cmbObjects.SelectedIndex],
                                                                                    MeshItemValue[cmbMeshItems.SelectedIndex],
                                                                                    PullOutRealAnimationName(),
                                                                                    chxLoop.Checked,
                                                                                    ntxTimePoint.GetFloat(),
                                                                                    ntxTimeDelay.GetFloat(),
                                                                                    ntxSpeedFactor.GetFloat(),
                                                                                    ntxWeight.GetFloat(),
                                                                                    ntxLength.GetFloat());

                    AnimationStateText.Add("(" + animationState.GetHashCode().ToString() + ") Animation State");
                    AnimationStateValue.Add(animationState);

                    lstAnimationState.Items.Clear();
                    lstAnimationState.Items.AddRange(AnimationStateText.ToArray());

                    lstAnimationState.SelectedIndex = lstAnimationState.Items.Count - 1;
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Create", McEx);
            }
        }

        private void btnCreateAnimationStateUsingMeshID_Click(object sender, EventArgs e)
        {
            DialogResult dlgRes = MessageBox.Show("Do you wish to create with default values?", "Create Options", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dlgRes == DialogResult.Yes)
            {
                cmbAttachedAnimationName.Text = "";
                chxEnabled.Checked = false;
                ntxTimePoint.SetFloat(0);
                ntxSpeedFactor.SetFloat(1);
                ntxWeight.SetFloat(1);
                ntxLength.SetFloat(0);
                chxLoop.Checked = true;
                ntxTimeDelay.SetFloat(0);
            }

            try
            {
                IDNMcAnimationState animationState = DNMcAnimationState.Create(ObjectValue[cmbObjects.SelectedIndex],
                                                                                    ntxMeshItemID.GetUInt32(),
                                                                                    PullOutRealAnimationName(),
                                                                                    chxLoop.Checked,
                                                                                    ntxTimePoint.GetFloat(),
																					ntxTimeDelay.GetFloat(),
																					ntxSpeedFactor.GetFloat(),
                                                                                    ntxWeight.GetFloat(),
                                                                                    ntxLength.GetFloat());

                AnimationStateText.Add("(" + animationState.GetHashCode().ToString() + ") Animation State");
                AnimationStateValue.Add(animationState);

                lstAnimationState.Items.Clear();
                lstAnimationState.Items.AddRange(AnimationStateText.ToArray());

                lstAnimationState.SelectedIndex = lstAnimationState.Items.Count - 1;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Create", McEx);
            }            
        }

        private void lstAnimationState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAnimationState.SelectedIndex != -1)
            {
                FillAttachAnimationNameComboBox();

                string attachedAnimationName = "";
                try
                {
                    attachedAnimationName = SelectedAnimationState.GetAttachedAnimation();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetAttachedAnimation", McEx);
                }

                if (attachedAnimationName != "")
                {
                    cmbAttachedAnimationName.SelectedText = attachedAnimationName;

                    try
                    {
                        chxEnabled.Checked = SelectedAnimationState.GetEnabled();                        
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetEnabled", McEx);
                    }

                    try
                    {
                        ntxTimePoint.SetFloat(SelectedAnimationState.GetTimePoint());
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetTimePoint", McEx);
                    }

                    try
                    {
                        ntxWeight.SetFloat(SelectedAnimationState.GetWeight());
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetWeight", McEx);
                    }

                    try
                    {
                        ntxLength.SetFloat(SelectedAnimationState.GetLength());
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetLength", McEx);
                    }

                    try
                    {
                        ntxSpeedFactor.SetFloat(SelectedAnimationState.GetSpeedFactor());	
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetFloat", McEx);
                    }

                    try
                    {
                        chxLoop.Checked = SelectedAnimationState.GetLoop();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetLoop", McEx);
                    }

                    try
                    {
                        chxHasEnded.Checked = SelectedAnimationState.HasEnded();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("HasEnded", McEx);
                    }

                    try
                    {
                        dgvAttachPointsWeights.Rows.Clear();

                        IDNMcMeshItem currMeshItem = MeshItemValue[cmbMeshItems.SelectedIndex];
                        IDNMcMesh dnMesh;
                        uint puPropID;

                        if (currMeshItem != null)
                        {
                            IDNMcObject animationStateObj = ObjectValue[cmbObjects.SelectedIndex];

                            currMeshItem.GetMesh(out dnMesh, out puPropID, false);
                            
                            uint numAttachPoints = ((IDNMcNativeMesh)dnMesh).GetNumAttachPoints();
                            float [] apWeights = SelectedAnimationState.GetAttachPointsWeights();
                            
                            if (apWeights.Length > 0)
                            {
                                int apIndex = 0;
                                foreach (float weight in apWeights)
                                {
                                    dgvAttachPointsWeights.Rows.Add();
                                    string apName = ((IDNMcNativeMesh)dnMesh).GetAttachPointNameByIndex((uint)apIndex);

                                    dgvAttachPointsWeights[0, apIndex].Value = apName;
                                    dgvAttachPointsWeights[1, apIndex].Value = weight;

                                    apIndex++;
                                } 
                            }
                            else
                            {
                                for (int i = 0; i < numAttachPoints; i++ )
                                {
                                    dgvAttachPointsWeights.Rows.Add();
                                    string apName = ((IDNMcNativeMesh)dnMesh).GetAttachPointNameByIndex((uint)i);

                                    dgvAttachPointsWeights[0, i].Value = apName;
                                    dgvAttachPointsWeights[1, i].Value = 1;
                                }
                            }                          
                        }                        	
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetAttachPointsWeights", McEx);
                    }
                }
                else
                {
                    chxEnabled.Checked = false;
                    ntxTimePoint.SetFloat(0);
                    ntxWeight.SetFloat(1);
                    ntxLength.SetFloat(0);
                    ntxSpeedFactor.SetFloat(1);
                    chxLoop.Checked = true;
                    chxHasEnded.Checked = false;
                    ntxTimeDelay.SetFloat(0);
                }
            }
        }

        private void FillAttachAnimationNameComboBox()
        {
            try
            {
                cmbAttachedAnimationName.Items.Clear();
                cmbAttachedAnimationName.Text = "";

                if (cmbMeshItems.SelectedIndex != -1)
                {
                    IDNMcMeshItem currMeshItem = MeshItemValue[cmbMeshItems.SelectedIndex];
                    IDNMcMesh dnMesh;
                    uint puPropID;

                    if (currMeshItem != null)
                    {
                        IDNMcObject animationStateObj = ObjectValue[cmbObjects.SelectedIndex];
                        
                        currMeshItem.GetMesh(out dnMesh, out puPropID, true);

                        if (!(puPropID == (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID))
                        {
                        	if (puPropID == (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID)
                            {
                                string[] names = AddObjectSelectionIndication(((IDNMcNativeMesh)dnMesh).GetAnimationsNames(), true);
                                cmbAttachedAnimationName.Items.AddRange(names);
                            	
                            }
                            else
                            {
                                dnMesh = animationStateObj.GetMeshProperty(puPropID);
                                string[] names = AddObjectSelectionIndication(((IDNMcNativeMesh)dnMesh).GetAnimationsNames(), true);
                                cmbAttachedAnimationName.Items.AddRange(names);
                            }                        	
                        }

                        currMeshItem.GetMesh(out dnMesh, out puPropID, false);

                        if (puPropID == (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID)
                        {
                            string[] names = AddObjectSelectionIndication(((IDNMcNativeMesh)dnMesh).GetAnimationsNames(), false);
                            cmbAttachedAnimationName.Items.AddRange(names);
                        }   
                        else
                        {
                            dnMesh = animationStateObj.GetMeshProperty(puPropID);
                            string[] names = AddObjectSelectionIndication(((IDNMcNativeMesh)dnMesh).GetAnimationsNames(), false);
                            cmbAttachedAnimationName.Items.AddRange(((IDNMcNativeMesh)dnMesh).GetAnimationsNames());
                        }
                    }  
                }             
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetAnimationsNames", McEx);
            }
        }

        private string[] AddObjectSelectionIndication(string[] names, bool IsSelectedNames)
        {
            bool objSelectedState = false;
           
            byte[] states = (ObjectValue[cmbObjects.SelectedIndex]).GetState();
            if (states != null && states.Length > 0)
                objSelectedState = true;
            
            string additionString = "";
            if (IsSelectedNames == true)
                additionString = " - (Selected)";
            else
                additionString = " - (Regular)";

            if (objSelectedState != IsSelectedNames)
            {
                for (int i = 0; i < names.Length; i++)
                    names[i] += additionString;
            }
            
            return names;
        }

        private string PullOutRealAnimationName()
        {
            string name = cmbAttachedAnimationName.Text;
            int index = name.IndexOf(" - (", StringComparison.Ordinal);
            if ( index != -1)
                name = name.Substring(0, index);

            return name;
        }

        private void btnAttachToAnimationOK_Click(object sender, EventArgs e)
        {
            if (SelectedAnimationState != null)
            {
                try
                {
                    SelectedAnimationState.AttachToAnimation(PullOutRealAnimationName(),
                                                                chxLoop.Checked,
                                                                ntxTimePoint.GetFloat(),
																ntxTimeDelay.GetFloat(),
																ntxSpeedFactor.GetFloat(),
                                                                ntxWeight.GetFloat(),
                                                                ntxLength.GetFloat());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("AttachToAnimation", McEx);                    
                }

                try
                {
                    cmbAttachedAnimationName.Text = SelectedAnimationState.GetAttachedAnimation();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetAttachedAnimation", McEx);
                }

                if (cmbAttachedAnimationName.Text != "")
                {
                    try
                    {
                        chxEnabled.Checked = SelectedAnimationState.GetEnabled();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetEnabled", McEx);
                    }

                    try
                    {
                        ntxTimePoint.SetFloat(SelectedAnimationState.GetTimePoint());
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetTimePoint", McEx);
                    }

                    try
                    {
                        ntxWeight.SetFloat(SelectedAnimationState.GetWeight());
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetWeight", McEx);
                    }

                    try
                    {
                        ntxLength.SetFloat(SelectedAnimationState.GetLength());
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetLength", McEx);
                    }

                    try
                    {
                        ntxSpeedFactor.SetFloat(SelectedAnimationState.GetSpeedFactor());
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetSpeedFactor", McEx);
                    }

                    try
                    {
                        chxLoop.Checked = SelectedAnimationState.GetLoop();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetLoop", McEx);
                    }

                    try
                    {
                        chxHasEnded.Checked = SelectedAnimationState.HasEnded();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("HasEnded", McEx);
                    }
                }
            }
        }

        private void btnEnabledOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedAnimationState != null)
                    SelectedAnimationState.SetEnabled(chxEnabled.Checked);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetEnabled", McEx);
            }
        }

        private void btnTimePointOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedAnimationState != null)
                    SelectedAnimationState.SetTimePoint(ntxTimePoint.GetFloat());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTimePoint", McEx);
            }
        }

        private void btnWeightOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedAnimationState != null)
                    SelectedAnimationState.SetWeight(ntxWeight.GetFloat(), ntxDuration.GetFloat());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetWeight", McEx);
            }
        }

        private void btnLengthOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedAnimationState != null)
                    SelectedAnimationState.SetLength(ntxLength.GetFloat());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetLength", McEx);
            }
        }

        private void btnSpeedFactorOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedAnimationState != null)
                    SelectedAnimationState.SetSpeedFactor(ntxSpeedFactor.GetFloat());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSpeedFactor", McEx);
            }
        }  

        private void btnLoopOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedAnimationState != null)
                    SelectedAnimationState.SetLoop(chxLoop.Checked);	
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetLoop", McEx);
            }
        }

        private void btnRemoveAnimationState_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedAnimationState != null)
                {
                    SelectedAnimationState.Remove();
                    AnimationStateValue.RemoveAt(lstAnimationState.SelectedIndex);
                    AnimationStateText.RemoveAt(lstAnimationState.SelectedIndex);
                    lstAnimationState.Items.RemoveAt(lstAnimationState.SelectedIndex);
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Remove", McEx);
            }
        }

        private void btnAnimationStateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (IDNMcAnimationState states in AnimationStateValue)
                states.Dispose();
        }

        private void rdbCreateUsingMeshItem_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCreateUsingMeshItem.Checked == true)
            {
                btnCreateAnimationStateUsingMeshItem.Enabled = true;
                btnCreateAnimationStateUsingMeshID.Enabled = false;
            }
            else
            {
                btnCreateAnimationStateUsingMeshItem.Enabled = false;
                btnCreateAnimationStateUsingMeshID.Enabled = true;
            }
        }

        private void btnAttachPointsWeights_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedAnimationState != null)
                {
                    IDNMcObject animationStateObj = ObjectValue[cmbObjects.SelectedIndex];

                    float [] apWeights = new float [dgvAttachPointsWeights.RowCount];
                    for (int i = 0; i < dgvAttachPointsWeights.RowCount; i++)
                        apWeights[i] = float.Parse(dgvAttachPointsWeights[1, i].Value.ToString());

                    SelectedAnimationState.SetAttachPointsWeights(apWeights, ntxAPDuration.GetFloat());                    
                }  
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetAttachPointsWeights", McEx);
            }
        }  
    }
}
