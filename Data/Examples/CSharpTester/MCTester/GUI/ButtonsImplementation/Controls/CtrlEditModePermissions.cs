using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;
using MapCore.Common;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers.MapWorld;
using UnmanagedWrapper;

namespace MCTester.Controls
{
    public partial class CtrlEditModePermissions : UserControl
    {
        private IDNMcEditMode m_EditMode;
        private DNSObjectOperationsParams m_SObjectOperationsParams;
        int m_lastPermissionIndex = -1;
        List<DNSPermissionHiddenIcons> m_PermissionHiddenIcons = new List<DNSPermissionHiddenIcons>();
        List<DNSPermissionHiddenIcons> m_EditModePermissionHiddenIcons = new List<DNSPermissionHiddenIcons>();
        //Array m_EPermissionNames;
        bool m_IsLoadIcons = false;
        ParentFormType m_ParentFormType;
        bool m_IsCameFromObjectScheme = false;

        List<IDNMcObject> dTempPoints = new List<IDNMcObject>();

        public enum ParentFormType { editmode, objectoperations };

        public CtrlEditModePermissions()
        {
            InitializeComponent();

            List<string> listPermission = Enum.GetNames(typeof(DNEPermission)).ToList();
            listPermission.Remove(DNEPermission._EEMP_FINISH_TEXT_STRING_BY_KEY.ToString());
            listPermission.Remove(DNEPermission._EEMP_NONE.ToString());
            cmbPermissionType.Items.Add("");
            cmbPermissionType.Items.AddRange(listPermission.ToArray());
        }

        public ParentFormType ParentFormTypeName
        {
            get { return m_ParentFormType; }
            set
            {
                m_ParentFormType = value;
                if (ParentFormTypeName == ParentFormType.editmode)
                {
                    chxIsFieldEnablePermissions.Visible = false;
                    chxIsFieldEnableHiddenIcons.Visible = false;
                    label6.Visible = false;
                }
                else
                {
                    btnSetHiddenIcons.Visible = false;
                }
            }
        }

        public IDNMcEditMode EditMode
        {
            get { return m_EditMode; }
            set
            {
                InitControl();
                m_EditMode = value;
                try
                {
                    if (m_EditMode != null)
                    {
                        PermissionsBitArray = m_EditMode.GetPermissions();
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetPermissions", McEx);
                }
            }
        }

        public void SetSObjectOperationsParams(DNSObjectOperationsParams sObjectOperationsParams, bool isObjectScheme)
        {
            InitControl();
            m_SObjectOperationsParams = sObjectOperationsParams;
            m_IsCameFromObjectScheme = isObjectScheme;

            if (m_SObjectOperationsParams != null)
            {
                PermissionsBitArray = (DNEPermission)m_SObjectOperationsParams.uPermissions;
                if (m_SObjectOperationsParams.aPermissionsWithHiddenIcons != null)
                    m_EditModePermissionHiddenIcons = m_SObjectOperationsParams.aPermissionsWithHiddenIcons.ToList();
                if (m_IsCameFromObjectScheme)
                {
                    if (PermissionsBitArray != DNEPermission._EEMP_NONE)
                        chxIsFieldEnablePermissions.Checked = true;
                    m_PermissionHiddenIcons = m_EditModePermissionHiddenIcons;
                }
            }
            chxShowIconsScreenPositions.Visible = false;
            cmbPermissionType.Items.Remove("");
        }


        public DNSObjectOperationsParams GetSObjectOperationsParams()
        {
            return m_SObjectOperationsParams;
        }

        public DNEPermission PermissionsBitArray
        {
            get
            {
                DNEPermission permissionsBitArray = 0;
                if (chxMoveVertex.Checked == true)
                    permissionsBitArray |= DNEPermission._EEMP_MOVE_VERTEX;
                if (chxBreakEdge.Checked == true)
                    permissionsBitArray |= DNEPermission._EEMP_BREAK_EDGE;
                if (chxResize.Checked == true)
                    permissionsBitArray |= DNEPermission._EEMP_RESIZE;
                if (chxRotate.Checked == true)
                    permissionsBitArray |= DNEPermission._EEMP_ROTATE;
                if (chxDrag.Checked == true)
                    permissionsBitArray |= DNEPermission._EEMP_DRAG;
                if (chxFinishTextStringByKey.Checked == true)
                    permissionsBitArray |= DNEPermission._EEMP_FINISH_TEXT_STRING_BY_KEY;

                return permissionsBitArray;
            }
            set
            {
                chxMoveVertex.Checked = ((value & DNEPermission._EEMP_MOVE_VERTEX) == DNEPermission._EEMP_MOVE_VERTEX);
                chxBreakEdge.Checked = ((value & DNEPermission._EEMP_BREAK_EDGE) == DNEPermission._EEMP_BREAK_EDGE);
                chxResize.Checked = ((value & DNEPermission._EEMP_RESIZE) == DNEPermission._EEMP_RESIZE);
                chxRotate.Checked = ((value & DNEPermission._EEMP_ROTATE) == DNEPermission._EEMP_ROTATE);
                chxDrag.Checked = ((value & DNEPermission._EEMP_DRAG) == DNEPermission._EEMP_DRAG);
                chxFinishTextStringByKey.Checked = ((value & DNEPermission._EEMP_FINISH_TEXT_STRING_BY_KEY) == DNEPermission._EEMP_FINISH_TEXT_STRING_BY_KEY);
            }
        }

        public void InitControl()
        {
            cmbPermissionType.SelectedText = "";
            cmbPermissionType.SelectedIndex = -1;
            m_lastPermissionIndex = -1;
            m_PermissionHiddenIcons.Clear();
        }

        public bool IsUserCheckedPermissions { get { return chxIsFieldEnablePermissions.Checked; } }

        public void SetEnableButtons(bool isEnable)
        {
            chxIsFieldEnablePermissions.Visible = true;
            chxIsFieldEnableHiddenIcons.Visible = true;

            gbPermissions.Enabled = isEnable;
            txtHiddenIconPerPermission.Enabled = isEnable;
            label16.Enabled = isEnable;
            chxIsFieldEnableHiddenIcons.Checked = false;
            chxIsFieldEnablePermissions.Checked = false;
        }

        public List<DNSPermissionHiddenIcons> SaveCurrentPermissionHiddenIcons()
        {
            // check if save the item in the list
            if (m_lastPermissionIndex >= 0 && chxIsFieldEnableHiddenIcons.Checked)
            {
                DNEPermission permission = (DNEPermission)Enum.Parse(typeof(DNEPermission), cmbPermissionType.Items[m_lastPermissionIndex].ToString());  
                DNSPermissionHiddenIcons permissionHiddenIcon;
                if (m_PermissionHiddenIcons.Exists(x => x.ePermission == permission))
                {
                    permissionHiddenIcon = m_PermissionHiddenIcons.Find(x => x.ePermission == permission);
                    m_PermissionHiddenIcons.Remove(permissionHiddenIcon);
                }
                else
                {
                    permissionHiddenIcon = new DNSPermissionHiddenIcons();
                    permissionHiddenIcon.ePermission = permission;
                }
                permissionHiddenIcon.auIconIndices = IconIndices;
                m_PermissionHiddenIcons.Add(permissionHiddenIcon);
            }
            if (!m_IsCameFromObjectScheme)
            {
                if (m_PermissionHiddenIcons.Count == 0)
                    return null;
                else
                    return m_PermissionHiddenIcons;
            }
            else
            {
                if (m_PermissionHiddenIcons.Count == 0 && m_EditModePermissionHiddenIcons.Count == 0)
                    return null;
                else
                 return m_PermissionHiddenIcons;

                /*else if (m_PermissionHiddenIcons.Count != 0 && m_EditModePermissionHiddenIcons.Count == 0)
                    return m_PermissionHiddenIcons;
                else if (m_PermissionHiddenIcons.Count == 0 && m_EditModePermissionHiddenIcons.Count != 0)
                    return m_EditModePermissionHiddenIcons;
                else
                {
                    List<DNSPermissionHiddenIcons> merges = new List<DNSPermissionHiddenIcons>();
                    foreach(DNEPermission permission in Enum.GetValues(typeof(DNEPermission)))
                    {
                        if (m_PermissionHiddenIcons.Exists(x => x.ePermission == permission))
                            merges.Add(m_PermissionHiddenIcons.Find(x => x.ePermission == permission));
                        else if(m_EditModePermissionHiddenIcons.Exists(x => x.ePermission == permission))
                            merges.Add(m_EditModePermissionHiddenIcons.Find(x => x.ePermission == permission));
                    }
                    return merges;
                }*/
            }
        }

        private DNEPermission GetCurrentPermission() {
            return (DNEPermission) Enum.Parse(typeof(DNEPermission), cmbPermissionType.Text);
        }

        private void cmbPermissionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveCurrentPermissionHiddenIcons();

            uint[] hiddenIcons = null;
            txtHiddenIconPerPermission.Text = "";

            m_lastPermissionIndex = cmbPermissionType.SelectedIndex;
            
            m_IsLoadIcons = true;
            chxIsFieldEnableHiddenIcons.Checked = false;

            if (cmbPermissionType.Text != "")
            {
                try
                {
                    DNEPermission permission = GetCurrentPermission();
                    if (m_EditMode != null)
                    {
                        hiddenIcons = m_EditMode.GetHiddenIconsPerPermission(permission);
                    }
                    else
                    {
                        if (m_PermissionHiddenIcons != null)
                        {
                            if (m_PermissionHiddenIcons.Any(x => x.ePermission == permission))
                            {
                                hiddenIcons = m_PermissionHiddenIcons.First(x => x.ePermission == permission).auIconIndices;
                                chxIsFieldEnableHiddenIcons.Checked = true;
                            }
                            else if (m_EditModePermissionHiddenIcons.Any(x => x.ePermission == permission))
                            {
                                hiddenIcons = m_EditModePermissionHiddenIcons.First(x => x.ePermission == permission).auIconIndices;
                                if(m_IsCameFromObjectScheme)
                                    chxIsFieldEnableHiddenIcons.Checked = true;
                            }
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetHiddenIconsPerPermission", McEx);
                }

                if (hiddenIcons != null)
                {
                    foreach (uint icon in hiddenIcons)
                        txtHiddenIconPerPermission.Text += icon.ToString() + " ";
                }

               
            }
            ShowIconsScreenPositions();
            m_IsLoadIcons = false;
        }

        public uint[] IconIndices
        {
            get
            {
                if (cmbPermissionType.Text != "")
                {
                    string[] hiddenIcons;
                    string[] delimeter = null;

                    hiddenIcons = txtHiddenIconPerPermission.Text.Trim().Split(delimeter, StringSplitOptions.RemoveEmptyEntries);

                    uint[] auIconIndices = new uint[hiddenIcons.Length];
                    for (int i = 0; i < auIconIndices.Length; i++)
                        auIconIndices[i] = uint.Parse(hiddenIcons[i]);

                    return auIconIndices;
                }
                return null;
            }
        }
        private void btnSetHiddenIcons_Click(object sender, EventArgs e)
        {
            if (cmbPermissionType.Text != "")
            {
                DNEPermission permission = (DNEPermission)Enum.Parse(typeof(DNEPermission), cmbPermissionType.Text);
                try
                {
                    m_EditMode.SetHiddenIconsPerPermission(permission, IconIndices);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetHiddenIconsPerPermission", McEx);
                }
            }
            else
                MessageBox.Show("Permission cannot be null, fix it and try again", "Set Hidden Icons Per Permission");
        }

        private void chxIsFieldEnablePermissions_CheckedChanged(object sender, EventArgs e)
        {
            foreach(Control control in gbPermissions.Controls)
            {
                control.Enabled = chxIsFieldEnablePermissions.Checked;
            }
            gbPermissions.Enabled = chxIsFieldEnablePermissions.Checked;
        }

        private void chxIsFieldEnableHiddenIcons_CheckedChanged(object sender, EventArgs e)
        {
            //txtHiddenIconPerPermission.Text = "";
            txtHiddenIconPerPermission.Enabled = chxIsFieldEnableHiddenIcons.Checked;
            label16.Enabled = chxIsFieldEnableHiddenIcons.Checked;
            if (m_IsLoadIcons == false && chxIsFieldEnableHiddenIcons.Checked == false)
            {
                txtHiddenIconPerPermission.Text = "";
                m_PermissionHiddenIcons.Remove(m_PermissionHiddenIcons.Find(x => x.ePermission == GetCurrentPermission()));
            }
        }

        private void SetCheckBoxValueByLabel(CheckBox checkBox)
        {
            checkBox.Checked = !checkBox.Checked;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            SetCheckBoxValueByLabel(chxMoveVertex);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            SetCheckBoxValueByLabel(chxBreakEdge);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            SetCheckBoxValueByLabel(chxResize);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            SetCheckBoxValueByLabel(chxRotate);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            SetCheckBoxValueByLabel(chxDrag);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            SetCheckBoxValueByLabel(chxFinishTextStringByKey);
        }

        private void chxShowIconsScreenPositions_CheckedChanged(object sender, EventArgs e)
        {
            ShowIconsScreenPositions();
        }

        Dictionary< string, DNSMcBColor> m_DicPermissionColors = null;
       
        private void ShowIconsScreenPositions()
        {
            List<string> names = Enum.GetNames(typeof(DNEPermission)).ToList();
            int sizeArray = names.Count;
            if (m_DicPermissionColors == null)
            {
                m_DicPermissionColors = new Dictionary<string, DNSMcBColor>();
                Random rand = new Random();
                m_DicPermissionColors.Add(DNEPermission._EEMP_MOVE_VERTEX.ToString(), new DNSMcBColor(0, 255, 0, 255));
                m_DicPermissionColors.Add(DNEPermission._EEMP_BREAK_EDGE.ToString(), new DNSMcBColor(255, 0, 0, 255));
                m_DicPermissionColors.Add(DNEPermission._EEMP_RESIZE.ToString(), new DNSMcBColor(0, 0, 255, 255));
                m_DicPermissionColors.Add(DNEPermission._EEMP_ROTATE.ToString(), new DNSMcBColor(255, 255, 0, 255));
                m_DicPermissionColors.Add(DNEPermission._EEMP_DRAG.ToString(), new DNSMcBColor(255, 0, 255, 255));
                m_DicPermissionColors.Add(DNEPermission._EEMP_FINISH_TEXT_STRING_BY_KEY.ToString(), new DNSMcBColor(0, 255, 255, 255));
            }

            RemoveTempPoints();
            if (chxShowIconsScreenPositions.Checked && MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport.OverlayManager != null && m_EditMode != null)
            {
                try
                {
                    IDNMcOverlayManager pOverlayManager = MCTMapFormManager.MapForm.Viewport.OverlayManager;
                    DNSIconPosition[] dNSIconPositions = m_EditMode.GetIconsScreenPositions();
                    if (dNSIconPositions != null)
                    {
                        List<DNSIconPosition> iconPositions = dNSIconPositions.ToList();
                        if (cmbPermissionType.Text != "")
                        {
                            iconPositions = dNSIconPositions.ToList().FindAll(x => x.ePermission == GetCurrentPermission());
                            names.Clear();
                            names.Add(GetCurrentPermission().ToString());
                        }
                        
                        if (iconPositions.Count > 0)
                        {
                            IDNMcObjectScheme mcObjectScheme = Manager_MCObject.GetTempObjectSchemeScreenPoints(MCTMapFormManager.MapForm.Viewport);

                          //  for (int i = 0;i< names.Count;i++) 
                            {

                                foreach (DNSIconPosition iconPosition in iconPositions)
                                {
                                    string permission = iconPosition.ePermission.ToString();
                                    DNSMcBColor textColor = names.Count == 1 ? (iconPosition.bIsActive ? new DNSMcBColor(0, 255, 0, 255) : new DNSMcBColor(255, 0, 0, 255)) : m_DicPermissionColors[permission];

                                    DNSMcVector3D[] locationPoints = new DNSMcVector3D[1] { iconPosition.ScreenPosition };
                                    IDNMcObject obj = DNMcObject.Create(Manager_MCTSymbology.GetTempOverlay(pOverlayManager), mcObjectScheme, locationPoints);
                                    obj.SetStringProperty(Manager_MCObject.TEXT_PROPERTY_ID, new DNMcVariantString(iconPosition.uIndex.ToString(), true));
                                    obj.SetBColorProperty(Manager_MCObject.COLOR_PROPERTY_ID, textColor);

                                    dTempPoints.Add(obj);

                                }
                            }
                        }
                    }

                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetIconsScreenPositions", McEx);
                }
            }
            
        }

        public void RemoveTempPoints()
        {
            try
            {
                foreach (IDNMcObject dNMcObject in dTempPoints)
                {
                    dNMcObject.Remove();
                    dNMcObject.Dispose();
                }
                dTempPoints.Clear();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("RemoveTempPoints", McEx);
            }
        }


    }
}
