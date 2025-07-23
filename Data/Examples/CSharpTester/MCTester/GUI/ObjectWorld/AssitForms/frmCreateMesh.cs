using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;
using MCTester.GUI.Forms;
using static MCTester.MainForm;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class frmCreateMesh : Form
    {
        private IDNMcMesh m_Mesh;
        private OpenFileDialog ofd;
        private DNSMcBColor m_TransparentColor;
        private DNEMeshType m_MeshType;
        private MainForm.FontTextureSourceForm m_meshTextureSource;
        private bool m_isNewAction = false;

        public frmCreateMesh(MainForm.FontTextureSourceForm meshTextureSource)
        {
            InitializeComponent();

            cmbDesirableMeshType.Items.AddRange(Enum.GetNames(typeof(DNEMeshType)));
            cmbMappedNameType.Items.AddRange(Enum.GetNames(typeof(DNEMappedNameType)));
            cmbMappedNameType.Text = DNEMappedNameType._EMNT_ATTACH_POINT.ToString();
             m_meshTextureSource = meshTextureSource;
       }

        public frmCreateMesh(FontTextureSourceForm meshTextureSource, IDNMcMesh mcMesh) :this(meshTextureSource)
        {
            m_Mesh = mcMesh;
            m_isNewAction = (m_meshTextureSource == FontTextureSourceForm.CreateOnMap ||
                            m_meshTextureSource == FontTextureSourceForm.CreateDialog ||
                            m_meshTextureSource == FontTextureSourceForm.Recreate);

           

            if (m_meshTextureSource == FontTextureSourceForm.CreateOnMap ||
                m_meshTextureSource == FontTextureSourceForm.CreateDialog)
            {
                this.Text = "Create New Mesh";
            }
            else
            {
                if (meshTextureSource == FontTextureSourceForm.Update)
                {
                    this.Text = "Update Existing Mesh";
                }
                else if(meshTextureSource == FontTextureSourceForm.Recreate)
                {
                    this.Text = "Re-create Mesh";
                }
                if (m_Mesh != null)
                {
                    try
                    {
                        chxUseExisting.Checked = m_Mesh.IsCreatedWithUseExisting();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("IsCreatedWithUseExisting", McEx);
                    }
                }
            }
        }

        private void SetFormGui()
        {
            //In case that mesh is not created yet
            if (m_Mesh == null)
            {
                rdbUpdateMesh.Enabled = false;
                rdbCreateMesh.Checked = true;
                //btnMeshList.Visible = true;
            }
            else
            {
                if (m_meshTextureSource == FontTextureSourceForm.Update)
                {
                    rdbUpdateMesh.Enabled = true;
                    rdbUpdateMesh.Checked = true;
                }
                else if (m_meshTextureSource == FontTextureSourceForm.Recreate)
                {
                    rdbRecreateMesh.Enabled = true;
                    rdbRecreateMesh.Checked = true;
                }
                cmbDesirableMeshType.Text = m_Mesh.MeshType.ToString();
               // btnMeshList.Visible = false;
            }
            btnMeshList.Visible = m_isNewAction;
        }
                
        private void btnSetMappedID_Click(object sender, EventArgs e)
        {
            try
            {
                if (MeshType == DNEMeshType._EMT_NATIVE_MESH_FILE || 
                    MeshType == DNEMeshType._EMT_NATIVE_LOD_MESH_FILE)
                {
                    try
                    {
                        DNEMappedNameType mappedType = (DNEMappedNameType)Enum.Parse(typeof(DNEMappedNameType), cmbMappedNameType.Text);
                        ((IDNMcNativeMesh)m_Mesh).SetMappedNameID(mappedType, 
                                                                    ntxMappedID.GetUInt32(),
                                                                    cmbMappedName.Text);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("SetMappedNameID", McEx);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Get MeshType", McEx);
            }

            RefreshDGVTable();
        }

        private void RefreshDGVTable()
        {
            if (m_Mesh != null)
            {
                DNEMappedNameType mappedType = DNEMappedNameType._EMNT_ATTACH_POINT;
                try
                {
                    if (cmbMappedNameType.Text == DNEMappedNameType._EMNT_ATTACH_POINT.ToString())
                        mappedType = DNEMappedNameType._EMNT_ATTACH_POINT;
                    else
						mappedType = DNEMappedNameType._EMNT_TEXTURE_UNIT_STATE;


                    uint[] mappedIDs = ((IDNMcNativeMesh)m_Mesh).GetMappedNamesIDs(mappedType);

                    //need to have the right amount of rows to the grid
                    dgvMappedIDs.Rows.Clear();
                    foreach (uint ui in mappedIDs)
                        dgvMappedIDs.Rows.Add();

                    for (int i = 0; i < mappedIDs.Length; i++)
                    {
                        dgvMappedIDs[0, i].Value = mappedIDs[i];

                        //placing the point name into the grid
                        string pointName = string.Empty;

                        ((IDNMcNativeMesh)m_Mesh).GetMappedNameByID(mappedType, mappedIDs[i], out pointName);
                        dgvMappedIDs[1, i].Value = pointName;
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetMappedNamesIDs", McEx);
                }

                try
                {
                    string mappedName;
                    for (int i = 0; i < dgvMappedIDs.RowCount - 1; i++)
                    {
                        uint ID = uint.Parse(dgvMappedIDs[0, i].Value.ToString());

                        ((IDNMcNativeMesh)m_Mesh).GetMappedNameByID(mappedType, ID, out mappedName);

                        dgvMappedIDs[1, i].Value = mappedName;
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetMappedNameByID", McEx);
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (rdbCreateMesh.Checked == true || rdbRecreateMesh.Checked == true)
            {
                try
                {
                    DNEMeshType desireMeshType;

                    desireMeshType = (DNEMeshType)Enum.Parse(typeof(DNEMeshType), cmbDesirableMeshType.Text);
                    bool? bExistingUsed = chxExistingUsed.Checked;

                    switch (desireMeshType)
                    {
                        case DNEMeshType._EMT_XFILE_MESH:
                            try
                            {
                                m_Mesh = DNMcXFileMesh.Create(txtMeshPath.Text,
                                                                m_TransparentColor,
                                                                chxUseExisting.Checked,
                                                                ref bExistingUsed);

                                chxExistingUsed.Checked = (bool)bExistingUsed;
                                if (bExistingUsed == false)
                                {
                                    Manager_MCMesh.AddToDictionary(m_Mesh);
                                }
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("DNMcXFileMesh.Create", McEx);
                            }
                            try
                            {
                                bool IsEnabled;

                                ((IDNMcXFileMesh)m_Mesh).GetTransparentColor(out m_TransparentColor,
                                                                            out IsEnabled);

                                btnTransparentColor.BackColor = Color.FromArgb(m_TransparentColor.a,
                                                                                m_TransparentColor.r,
                                                                                m_TransparentColor.g,
                                                                                m_TransparentColor.b);

                                chxTransparentColorEnabled.Checked = IsEnabled;
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("GetTransparentColor", McEx);
                            }
                            break;
                        case DNEMeshType._EMT_NATIVE_MESH_FILE:
                            try
                            {
                                m_Mesh = DNMcNativeMeshFile.Create(txtMeshPath.Text,
                                                                        chxUseExisting.Checked,
                                                                        ref bExistingUsed);


                                chxExistingUsed.Checked = (bool)bExistingUsed;
                                if (bExistingUsed == false)
                                {
                                    Manager_MCMesh.AddToDictionary(m_Mesh);
                                }                                
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("DNMcNativeMeshFile.Create", McEx);
                            }
                            break;
                        case DNEMeshType._EMT_NATIVE_LOD_MESH_FILE:
                            try
                            {
                                m_Mesh = DNMcNativeLODMeshFile.Create(txtMeshPath.Text,
                                                                        chxUseExisting.Checked,
                                                                        ref bExistingUsed);

                                chxExistingUsed.Checked = (bool)bExistingUsed;
                                if (bExistingUsed == false)
                                {
                                    Manager_MCMesh.AddToDictionary(m_Mesh);
                                }                                
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("DNMcNativeMeshLODFile.Create", McEx);
                            }
                            break;
                        case DNEMeshType._EMT_NONE:
                            break;
                        default:
                            break;
                    }

                    chxExistingUsed.Checked = bExistingUsed.Value;

                    m_meshTextureSource = FontTextureSourceForm.Update;
                    m_isNewAction = false;
                    SetFormGui();

                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Create Mesh", McEx);
                }
            }
            else
            {
                try
                {
                    DNEMeshType desireMeshType = (DNEMeshType)Enum.Parse(typeof(DNEMeshType), cmbDesirableMeshType.Text);

                    switch (desireMeshType)
                    {
                        case DNEMeshType._EMT_XFILE_MESH:
                            ((IDNMcXFileMesh)m_Mesh).SetXFile(txtMeshPath.Text,
                                                                m_TransparentColor);
                            break;
                        case DNEMeshType._EMT_NATIVE_MESH_FILE:
                            ((IDNMcNativeMeshFile)m_Mesh).SetMeshFile(txtMeshPath.Text);
                            break;
                        case DNEMeshType._EMT_NATIVE_LOD_MESH_FILE:
                            ((IDNMcNativeLODMeshFile)m_Mesh).SetLODMeshFile(txtMeshPath.Text);
                            break;
                        case DNEMeshType._EMT_NONE:
                            break;
                    }

                    SetFormGui();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Update Mesh", McEx);
                }
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            FillMappedNameComboBox();            
        }

        private void btnFilePath_Click(object sender, EventArgs e)
        {
            string suffix;
            DNEMeshType desireMeshType = (DNEMeshType)Enum.Parse(typeof(DNEMeshType), cmbDesirableMeshType.Text);

            switch (desireMeshType)
            {
                case DNEMeshType._EMT_XFILE_MESH:
                    suffix = "x";
                    break;
                case DNEMeshType._EMT_NATIVE_MESH_FILE:
                    suffix = "mesh";
                    break;
                case DNEMeshType._EMT_NATIVE_LOD_MESH_FILE:
                    suffix = "lodmesh";
                    break;
                default:
                    suffix = "*";
                    break;
            }            
            
            ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            //ofd.DefaultExt = "*";
            ofd.Filter = "all files *." + suffix + "|*." + suffix;

            //Display the openFile dialog.
            DialogResult result = ofd.ShowDialog();
            //OK button was pressed.
            if (result == DialogResult.OK)
            {
                //Excluding the mesh file name from the fool root name
                string fileName = ofd.FileName;
                char[] delimeters = new char[1];
                delimeters[0] = '\\';
                string[] splitedString = fileName.Split(delimeters);
                fileName = splitedString[splitedString.Length - 1];

                txtMeshPath.Text = fileName;
            }
        }

        private void btnTransparentColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
                        
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                m_TransparentColor.r = colorDialog.Color.R;
                m_TransparentColor.g = colorDialog.Color.G;
                m_TransparentColor.b = colorDialog.Color.B;
                m_TransparentColor.a = colorDialog.Color.A;

                btnTransparentColor.BackColor = colorDialog.Color;
            }
        }

        public DNEMeshType MeshType
        {
            get {return m_Mesh.MeshType;}
            set { m_MeshType = value; }
            
        }

        public IDNMcMesh CurrentMesh
        {
            get { return m_Mesh; }
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            
            this.Close();
        }

        private void dgvAttachPointIDs_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

        }

        private void btnRemoveID_Click(object sender, EventArgs e)
        {
            cmbMappedName.Text = null;
            btnSetMappedID_Click(null, null);
        }

        private void dgvAttachPointIDs_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMappedIDs.SelectedRows.Count == 0)
                return;

            if (dgvMappedIDs.SelectedRows[0].Cells[0].Value!=null &&
                dgvMappedIDs.SelectedRows[0].Cells[1].Value!=null)
            {
                ntxMappedID.Text = dgvMappedIDs.SelectedRows[0].Cells[0].Value.ToString();
                cmbMappedName.Text = dgvMappedIDs.SelectedRows[0].Cells[1].Value.ToString();
            }
            
        }

        private void rdbCreateMesh_CheckedChanged()
        {
            chxUseExisting.Enabled = m_isNewAction;
            //chxExistingUsed.Enabled = m_isNewAction;
            cmbDesirableMeshType.Enabled = m_isNewAction;
            gbAttachPointIDs.Enabled = !m_isNewAction;
            gbTransparentColor.Enabled = !m_isNewAction;
            btnMeshList.Visible = m_isNewAction;

            if (rdbCreateMesh.Checked == true)
            {
                cmbDesirableMeshType.Text = DNEMeshType._EMT_NATIVE_MESH_FILE.ToString();
                txtMeshPath.Text = "";
                txtMeshType.Text = "";
            }
            else
            {
                txtMeshPath.Text = m_Mesh.ToString();
                cmbDesirableMeshType.Text = m_Mesh.MeshType.ToString();

                RefreshDGVTable();
            }
        }

        private void rdbRecreateMesh_CheckedChanged(object sender, EventArgs e)
        {
            rdbCreateMesh_CheckedChanged();
        }

        private void rdbCreateMesh_CheckedChanged(object sender, EventArgs e)
        {
            rdbCreateMesh_CheckedChanged();
        }

        private void frmCreateMesh_Load(object sender, EventArgs e)
        {
            SetFormGui();
            rdbCreateMesh_CheckedChanged(sender, e);
        }

        private void btnMeshList_Click(object sender, EventArgs e)
        {
            DNEMeshType type = (DNEMeshType)Enum.Parse(typeof(DNEMeshType), cmbDesirableMeshType.Text);
            ExistingMeshListForm frmExistingMeshList = new ExistingMeshListForm(type);

            if (frmExistingMeshList.ShowDialog() == DialogResult.OK)
            {
                txtMeshPath.Text = frmExistingMeshList.SelectedMesh.ToString();
            }
        }

        private void cmbMappedNameType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMappedNameType.Text == DNEMappedNameType._EMNT_ATTACH_POINT.ToString())
                dgvMappedIDs.Columns[1].HeaderText = "Attach Point Name";
            else
                dgvMappedIDs.Columns[1].HeaderText = "Texture Name";

            RefreshDGVTable();
            FillMappedNameComboBox();
        }

        private void FillMappedNameComboBox()
        {
            cmbMappedName.Items.Clear();
            //cmbMappedName.SelectedIndex = -1;
            cmbMappedName.Text = "";

            if (m_Mesh != null)
            {
                if (cmbMappedNameType.Text == DNEMappedNameType._EMNT_ATTACH_POINT.ToString())
                {
                    try
                    {
						if (m_Mesh.MeshType == DNEMeshType._EMT_NATIVE_MESH_FILE)
						{
							IDNMcNativeMeshFile pNativeMeshFile = m_Mesh as IDNMcNativeMeshFile;
							cmbMappedName.Items.AddRange(pNativeMeshFile.GetAttachPointsNames());
						}
						else if (m_Mesh.MeshType == DNEMeshType._EMT_NATIVE_LOD_MESH_FILE)
						{
							IDNMcNativeLODMeshFile pNativeMeshFile = m_Mesh as IDNMcNativeLODMeshFile;
							cmbMappedName.Items.AddRange(pNativeMeshFile.GetAttachPointsNames());
						}
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetAttachPointsNames", McEx);
                    }
                }
                if (cmbMappedNameType.Text == DNEMappedNameType._EMNT_TEXTURE_UNIT_STATE.ToString())
                {
                    try
                    {
						if (m_Mesh.MeshType == DNEMeshType._EMT_NATIVE_MESH_FILE)
						{
							IDNMcNativeMeshFile pNativeMeshFile = m_Mesh as IDNMcNativeMeshFile;
							cmbMappedName.Items.AddRange(pNativeMeshFile.GetTextureUnitStatesNames());
						}
						else if (m_Mesh.MeshType == DNEMeshType._EMT_NATIVE_LOD_MESH_FILE)
						{
							IDNMcNativeLODMeshFile pNativeMeshFile = m_Mesh as IDNMcNativeLODMeshFile;
							cmbMappedName.Items.AddRange(pNativeMeshFile.GetTextureUnitStatesNames());
						}
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetTextureUnitStatesNames", McEx);
                    }
                }
            }
        }

        private void cmbMappedName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMappedNameType.Text == DNEMappedNameType._EMNT_ATTACH_POINT.ToString())
            {
                try
                {
                    ntxAttachPointIndex.SetUInt32(((IDNMcNativeMesh)CurrentMesh).GetAttachPointIndexByName(cmbMappedName.Text));
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetAttachPointIndexByName", McEx);
                }
            }
        }

        private void ntxAttachPointIndex_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbMappedName.Text = ((IDNMcNativeMesh)CurrentMesh).GetAttachPointNameByIndex(ntxAttachPointIndex.GetUInt32());                
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetAttachPointNameByIndex", McEx);
            }
        }

        private void ntxAttachPointIndex_EnterKeyPress(object sender, EventArgs e)
        {
            ntxAttachPointIndex_Leave(sender, e);
        }

        private void btnGetAttachPointChildren_Click(object sender, EventArgs e)
        {
            try
            {
                if (ntxParentIndex.Text != "")
                {
                    uint result;
                    bool parseSuccess = uint.TryParse(ntxParentIndex.Text, out result);

                    if (parseSuccess == true)
                    {
                        lstAttachPointChildren.Items.Clear();
                        lstAttachPointChildren.Text = "";

                        uint [] children = ((IDNMcNativeMesh)CurrentMesh).GetAttachPointChildren(result);

                        foreach (uint child in children)
                        {
                            string currName = ((IDNMcNativeMesh)CurrentMesh).GetAttachPointNameByIndex(child);
                            lstAttachPointChildren.Items.Add(currName + " - " + child.ToString());
                        }
                    }                    
                }	
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetAttachPointChildren", McEx);
            }
        }

        private void btnGetNumAttachPints_Click(object sender, EventArgs e)
        {
            try
            {
                ntxNumAttachPoints.SetUInt32(((IDNMcNativeMesh)CurrentMesh).GetNumAttachPoints());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetNumAttachPoints", McEx);
            }
        }

       
    }
}