/*Documentation Date ý13:27 ý12/ý02/ý2007

Documented By Tzahi Nemet

Form Name:CreateMeshForm

Parent: Edit button (upper toolbar) - Mesh button (lower toolbar) - Create button

Description:in this form the user defines the mesh file that should be used for mesh placement in 3d screen, mesh values can be set like transperancy.*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;
using MapCore.Common;
using MapCore;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class CreateMeshForm : CreateFabricForm
    {
        #region Private Data Members
        IDNMcMesh m_currentMesh;
        #endregion

        #region C-tors
        public CreateMeshForm() : this(null, "")
        {
        }

        public CreateMeshForm(IDNMcMesh currentMesh, string meshName) : base ()
        {
            InitializeComponent();
            m_currentMesh = currentMesh;

            if (currentMesh == null)
                m_btnCreateMesh.Text = "Create";
            else
            {
                m_btnCreateMesh.Text = "Update";
                txtXFile.Text = ((IDNMcXFileMesh)currentMesh).GetXFile();
                
                DNSMcBColor pTransparentColor;
                bool pbIsTransparentColorEnabled;
                ((IDNMcXFileMesh)currentMesh).GetTransparentColor(out pTransparentColor, out pbIsTransparentColorEnabled);

                if (pbIsTransparentColorEnabled)
                    m_pbxTransparentColor.BackColor = Utilities.ToColor(pTransparentColor);
                else
                    m_pbxTransparentColor.BackColor = Color.Transparent;

               /* // TBD: remove control
                m_pbxColorToSub.BackColor = Color.Transparent;
                m_pbxSubColor.BackColor = Color.Transparent;*/
            }
        }
        #endregion

        #region Public Properties
        public IDNMcMesh CurrentMesh
        {
            get
            {
                return m_currentMesh;
            }
        }
        #endregion

        #region Private Methods
        private void m_btnCreateMesh_Click(object sender, EventArgs e)
        {
            if (txtXFile.Text != "")
            {
                try
                {
                    m_currentMesh = DNMcXFileMesh.Create(txtXFile.Text);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcXFileMesh.Create", McEx);
                }                
            }
            else
            {
                if (txtNativeMeshFile.Text != "")
                {
                    try
                    {
                        m_currentMesh = DNMcNativeMeshFile.Create(txtNativeMeshFile.Text);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcNativeMeshFile.Create", McEx);
                    }
                }
                else
                {
                    if (txtNativeLODFile.Text != "")
                    {
                        try
                        {
                            m_currentMesh = DNMcNativeLODMeshFile.Create(txtNativeLODFile.Text);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcNativeMeshLODFile.Create", McEx);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You must choose mesh file first", "MCTester", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }                
            }

            this.Close();

            //DNSMcBColor? pTransparentColor;
            //DNSMcBColor? pColorToSub;
            //DNSMcBColor? pSubColor;

            ////init the texture colors
            //InitColors(out pTransparentColor, out pColorToSub, out pSubColor);
            
            //if (m_currentMesh == null)
            //{
            //    // Create Mesh
            //    bool? pbExistingUsed = false;
            //    m_currentMesh = (DNMcMesh)DNMcXFileMesh.Create(txtXFile.Text, pTransparentColor,
            //        pColorToSub, pSubColor, m_chkUseExisting.Checked, ref pbExistingUsed);

            //    if (m_imcEditMode != null)
            //    {
            //        m_imcEditMode.MeshAProperties.MeshFile = m_currentMesh;
            //        m_imcEditMode.StatusBarManager.UpdateMessage("Mesh file created");
            //    }
            //}
            //else
            //{
            //    // Update Mesh
            //    ((IDNMcXFileMesh)m_currentMesh).SetXFile(txtXFile.Text, pTransparentColor,
            //        pColorToSub, pSubColor);
            //}
            
        }

        private void m_btnFile_Click(object sender, EventArgs e)
        {
            string suffix = ((Button)sender).Tag.ToString();
            ofd = new OpenFileDialog();
            //ofd.DefaultExt = "X";
            ofd.Filter = "all files *." + suffix + "|*." + suffix;

            //Display the openFile dialog.
            ofd.RestoreDirectory = true;
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


                switch (tcMeshTypes.SelectedTab.Text)
                {
                    case "X File":
                        txtXFile.Text = fileName;
                        txtNativeMeshFile.Text = "";
                        txtNativeLODFile.Text = "";
                        break;
                    case "Native Mesh":
                        txtNativeMeshFile.Text = fileName;
                        txtXFile.Text = "";
                        txtNativeLODFile.Text = "";
                        break;
                    case "Native LOD":
                        txtNativeLODFile.Text = fileName;
                        txtXFile.Text = "";
                        txtNativeMeshFile.Text = "";
                        break;
                }
            }   
        }

        private void txtXfile(object sender, EventArgs e)
        {
            txtXFile.Text = ((IDNMcXFileMesh)m_currentMesh).GetXFile();
        }
        #endregion

    }
}