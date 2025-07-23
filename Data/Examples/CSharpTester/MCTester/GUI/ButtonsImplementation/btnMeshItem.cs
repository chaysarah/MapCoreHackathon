using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCTester.Managers.ObjectWorld;
using UnmanagedWrapper;
using MapCore;
using MCTester.Managers.MapWorld;

namespace MCTester.ButtonsImplementation
{
    public class btnMeshItem
    {
        private string m_MeshType;
        private IDNMcMesh m_MeshItem;
        
        public btnMeshItem(string meshType)
        {
            m_MeshType = meshType;
        }

        public void ExecuteAction()
        {
            try
            {
                string fileName;
                DialogResult dlgResult;
                switch (m_MeshType)
                {
                    case "Native Mesh":
                        fileName = OpenFileDlg("mesh", out dlgResult);
                        if (dlgResult == DialogResult.OK)
                        {
                            MeshItem = DNMcNativeMeshFile.Create(fileName,ObjectPropertiesBase.MeshUseExisting);
                            Manager_MCMesh.AddToDictionary(MeshItem);
                            DrawMeshOnMap();
                        }
                        break;
                    case "Native LOD":
                        fileName = OpenFileDlg("lodmesh", out dlgResult);
                        if (dlgResult == DialogResult.OK)
                        {
                            MeshItem = DNMcNativeLODMeshFile.Create(fileName);
                            Manager_MCMesh.AddToDictionary(MeshItem);
                            DrawMeshOnMap();
                        }
                        break;
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcXFileMesh.Create\\DNMcNativeMeshFile.Create\\DNMcNativeLODMeshFile.Create", McEx);
            }
        }

        private void DrawMeshOnMap()
        {
            try
            {
                if (Manager_MCOverlayManager.ActiveOverlayManager != null)
                {
                    IDNMcOverlay activeOverlay = Manager_MCOverlayManager.ActiveOverlay;

                    if (activeOverlay != null)
                    {
                        DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];

                        IDNMcObjectSchemeItem ObjSchemeItem = DNMcMeshItem.Create(MeshItem,
                                                                                    ObjectPropertiesBase.MeshRotateToTerrain,
                                                                                    ObjectPropertiesBase.MeshAlignment,
                                                                                    ObjectPropertiesBase.MeshParticipatesInTerrainHeight,
                                                                                    ObjectPropertiesBase.Meshcastshadows,
                                                                                    ObjectPropertiesBase.MeshIsStatic,
                                                                                    ObjectPropertiesBase.MeshDisplayItemsAttachedToTerrain);
                        IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                            ObjSchemeItem,
                                                            ObjectPropertiesBase.LocationCoordSys,
                                                            locationPoints,
                                                            ObjectPropertiesBase.LocationRelativeToDtm);


                        if (ObjectPropertiesBase.ImageCalc != null)
                            obj.SetImageCalc(ObjectPropertiesBase.ImageCalc);

                        MCTMapFormManager.MapForm.EditMode.StartInitObject(obj, ObjSchemeItem);

                        // turn on all viewports render needed flags
                        Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(activeOverlay.GetOverlayManager());
                    }
                    else
                        MessageBox.Show("There is no active overlay");
                }
                else
                    MessageBox.Show("There is no active overlay manager");
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating mesh item", McEx);
            }
        }

        private string OpenFileDlg(string suffix, out DialogResult dlgResult)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "all files *." + suffix + "|*." + suffix;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("aborted");
                dlgResult = DialogResult.Abort;
                return null;
            }

            //Excluding the mesh file name from the fool root name
            string filename = ofd.FileName;
            char[] delimeters = new char[1];
            delimeters[0] = '\\';
            string[] splitedString = filename.Split(delimeters);
            filename = splitedString[splitedString.Length - 1];

            dlgResult = DialogResult.OK;
            return filename;
        }

        public IDNMcMesh MeshItem
        {
            get { return m_MeshItem; }
            set { m_MeshItem = value; }
        }
    }
}
