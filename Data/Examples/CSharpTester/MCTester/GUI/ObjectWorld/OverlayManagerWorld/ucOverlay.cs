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
using MCTester.GUI.Trees;
using MCTester.GUI.Map;
using MCTester.ObjectWorld.Assit_Forms;
using MCTester.General_Forms;
using MCTester.Managers.MapWorld;
using MCTester.Managers;
using MapCore.Common;
using MCTester.Controls;
using System.IO;
using MCTester.MapWorld.Assist_Forms;
using MCTester.Managers.ObjectWorld;
using System.Security;

namespace MCTester.ObjectWorld.OverlayManagerWorld
{
	public partial class ucOverlay : UserControl,IUserControlItem, IOnMapClick
    {
		private IDNMcOverlay m_CurrentObject;
        private OpenFileDialog OFD;
        private SaveFileDialog SFD;
        private FolderSelectDialog FSD;
        private byte[] m_Buffer;
        private List<string> m_lViewportText;
        private List<IDNMcMapViewport> m_lViewportValue;
        private List<string> m_lObjectsText;
        private List<IDNMcObject> m_lObjectsValue;

        private List<string> m_lstOverlayText = new List<string>();
        private List<IDNMcOverlay> m_lstOverlayValue = new List<IDNMcOverlay>();

        private List<IDNMcMapViewport> m_lstOverlayViewportValue = new List<IDNMcMapViewport>();
        private List<string> m_lstOverlayViewportText = new List<string>();
        private DNSTilingScheme mcTilingScheme = null;
        private IDNOverlayManagerAsyncOperationCallback m_LastAsyncOperationCallback;

        public ucOverlay()
		{
			InitializeComponent();
            OFD = new OpenFileDialog();
            SFD = new SaveFileDialog();
            FSD = new FolderSelectDialog();
            FSD.Title = "What to select";
            FSD.InitialDirectory = @"c:\";

            cmbVisibility.Items.AddRange(Enum.GetNames(typeof(DNEActionOptions)));
            cmbStorageFormat.Items.AddRange(Enum.GetNames(typeof(DNEStorageFormat)));
            cmbStorageFormat.Text = MCTMapForm.m_eOStorageFormat.ToString();

            cmbGeometryFilter.Items.AddRange(Enum.GetNames(typeof(DNEGeometry)));
            cmbGeometryFilter.Text = DNEGeometry._UnSupportedGeometry.ToString();

            m_lViewportText = new List<string>();
            m_lViewportValue = new List<IDNMcMapViewport>();
            m_lObjectsText = new List<string>();
            m_lObjectsValue = new List<IDNMcObject>();

            lstViewports.DisplayMember = "lViewportText";
            lstViewports.ValueMember = "lViewportValue";

            lstObjects.DisplayMember = "lObjectsText";
            lstObjects.ValueMember = "lObjectsValue";

        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcOverlay)aItem;
            ctrlConditionalselectorOverlay.LoadItem(aItem);
            LoadViewportsToListBox();
            LoadObjectsToListBox();
            LoadViewportList();
            FillColorOverridingDGV();
            LoadObjectListToDGV();
            LoadObjectListToCombo();

            try
            {
                ntxOverlayID.SetUInt32(m_CurrentObject.GetID());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetID", McEx);
            }

            try
            {
                IDNMcCollection [] collections = m_CurrentObject.GetCollections();
                foreach (IDNMcCollection col in collections)
                {
                    lstCollection.Items.Add(Manager_MCNames.GetNameByObject(col));
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCollections", McEx);
            }

            try
            {
                lstOverlayManager.Items.Add(Manager_MCNames.GetNameByObject(m_CurrentObject.GetOverlayManager()));
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetOverlayManager", McEx);
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

            btnClearViewportSelection_Click(null, null);

        }

        private void LoadObjectListToCombo()
        {
            cmbObjects.Items.AddRange(m_lObjectsText.ToArray());
        }

      
        private void LoadObjectListToDGV()
        {
			if (m_lObjectsText.Count > 0)
			{
                dgvSetEachObjectLocationPoint.Rows.Add(m_lObjectsText.Count);
			}

            for (int i = 0; i < m_lObjectsText.Count; i++)
            {
                dgvSetEachObjectLocationPoint[0, i].Value = m_lObjectsText[i];
                dgvSetEachObjectLocationPoint[0, i].Tag = m_lObjectsValue[i];
            }
        }

		#endregion

        private string SaveFileDlg()
        {
            SFD.Title = "Save Object To File";
            SFD.Filter = "MapCore Object Files (*.mcobj, *.mcobj.json , *.m, *.json) |*.mcobj; *.mcobj.json; *.m; *.json;|" +
                "MapCore Object Binary Files (*.mcobj,*.m)|*.mcobj;*.m;|" +
                "MapCore Object Json Files (*.mcobj.json, *.json)|*.mcobj.json;*.json;|" +
                "All Files|*.*";
            SFD.RestoreDirectory = true;
            if (SFD.ShowDialog() == DialogResult.OK)
            {
                return SFD.FileName;
            }
            else
                return "";
        }

        private string OpenFileDlg()
        {
            OFD.Title = "Load Object From File";
            OFD.Filter = "MapCore Object Files (*.mcobj, *.mcobj.json,, *.m *.json) |*.mcobj; *.mcobj.json; *.m; *.json;|" +
                "MapCore Object Binary Files (*.mcobj,*.m)|*.mcobj;*.m;|" +
                "MapCore Object Json Files (*.mcobj.json, *.json)|*.mcobj.json; *.json;|" +
                "All Files|*.*";
            OFD.RestoreDirectory = true;
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                return OFD.FileName;
            }
            else
                return "";
        }

        private string SaveFolderDialog()
        {                   
            if (FSD.ShowDialog(IntPtr.Zero))
            {
                return FSD.FileName;
            }
            return "";
        }

        private void btnSaveAllToFile_Click(object sender, EventArgs e)
        {
            try
            {
                bool isAllSchemeHasSameVersion;
                IDNMcObject[] mcObject = m_CurrentObject.GetObjects();
                DNESavingVersionCompatibility SavingVersionCompatibility = Manager_MCTObjectSchemeData.GetSavingVersionCompatibility(mcObject, out isAllSchemeHasSameVersion);

                if (!isAllSchemeHasSameVersion)
                {
                    DialogResult dialogResult = MessageBox.Show(Manager_MCTObjectSchemeData.GetMsgText(SavingVersionCompatibility.ToString(), false), "Save All To File", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                        isAllSchemeHasSameVersion = true;
                }

                if (isAllSchemeHasSameVersion)
                {
                    string fileName = SaveFileDlg();
                    if (fileName != "")
                    {
                       DNEStorageFormat format = GetStorageFormatByFileName(fileName);
                       m_CurrentObject.SaveAllObjects(fileName,
                                                    format,
                                                    SavingVersionCompatibility);

                        if (mcObject != null)
                        {
                            for (int i = 0; i < mcObject.Length; i++)
                                Manager_MCTObjectSchemeData.AddObjectSchemeData(mcObject[i], (uint)SavingVersionCompatibility, format);

                            LoadObjectsToListBox();
                        }
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SaveAllObjects - to a file", McEx);
            }
        }

        private void btnSaveAllToBuffer_Click(object sender, EventArgs e)
        {
            try
            {
                bool isAllSchemeHasSameVersion;
                IDNMcObject[] mcObjects = m_CurrentObject.GetObjects();
                DNEStorageFormat storageFormat = GetStorageFormatByUser();
                DNESavingVersionCompatibility SavingVersionCompatibility = Manager_MCTObjectSchemeData.GetSavingVersionCompatibility(mcObjects, out isAllSchemeHasSameVersion);
                if (!isAllSchemeHasSameVersion)
                {
                    DialogResult dialogResult = MessageBox.Show(Manager_MCTObjectSchemeData.GetMsgText(SavingVersionCompatibility.ToString(), false), "Save All To Buffer", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                        isAllSchemeHasSameVersion = true;
                }
                if (isAllSchemeHasSameVersion)
                {
                    m_Buffer = m_CurrentObject.SaveAllObjects(storageFormat, SavingVersionCompatibility);
                    MCTMapForm.m_dicBuffer[1] = m_Buffer;

                    for (int i = 0; i < mcObjects.Length; i++)
                        Manager_MCTObjectSchemeData.AddObjectSchemeData(mcObjects[i], (uint)SavingVersionCompatibility, storageFormat);

                    LoadObjectsToListBox();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SaveAllObjects - to a buffer", McEx);
            }
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            if (lstObjects.SelectedItem != null)
            {
                try
                {
                    List<IDNMcObject> selectedObjs = GetSelectedObjects();
                    bool isAllSchemeHasSameVersion;
                    if (selectedObjs != null)
                    {
                        DNESavingVersionCompatibility SavingVersionCompatibility = Manager_MCTObjectSchemeData.GetSavingVersionCompatibility(selectedObjs.ToArray(), out isAllSchemeHasSameVersion);
                        if (!isAllSchemeHasSameVersion)
                        {
                            DialogResult dialogResult = MessageBox.Show(Manager_MCTObjectSchemeData.GetMsgText(SavingVersionCompatibility.ToString(), false), "Save To File", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.OK)
                                isAllSchemeHasSameVersion = true;
                        }
                        if (isAllSchemeHasSameVersion)
                        {
                            string fileName = SaveFileDlg();
                            if (fileName != "")
                            {
                                DNEStorageFormat storageFormat = GetStorageFormatByFileName(fileName);

                                m_CurrentObject.SaveObjects(selectedObjs.ToArray(),
                                                        fileName,
                                                        storageFormat,
                                                        SavingVersionCompatibility);
                                
                                for (int i = 0; i < selectedObjs.Count; i++)
                                    Manager_MCTObjectSchemeData.AddObjectSchemeData(selectedObjs[i], (uint)SavingVersionCompatibility, storageFormat, selectedObjs.Count == 1 && chxSaveFilePath.Checked ? fileName : "");

                                LoadObjectsToListBox();
                            }
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SaveObjects - to a file", McEx);
                }
            }
            else
                MessageBox.Show("You need to choose at least one object");
        }

        private List<IDNMcObject> GetSelectedObjects()
        {
            List<IDNMcObject> selectedObjs = new List<IDNMcObject>();

            ListBox.SelectedIndexCollection indices = lstObjects.SelectedIndices;
            int[] ArrSelectedIndices = new int[indices.Count];
            indices.CopyTo(ArrSelectedIndices, 0);

            for (int idx = 0; idx < ArrSelectedIndices.Length; idx++)
            {
                selectedObjs.Add(lObjectsValue[ArrSelectedIndices[idx]]);
            }

            return selectedObjs;
        }

        private void btnSaveToBuffer_Click(object sender, EventArgs e)
        {
            try
            {
                List<IDNMcObject> selectedObjs = GetSelectedObjects();
                bool isAllSchemeHasSameVersion;
                DNESavingVersionCompatibility SavingVersionCompatibility = Manager_MCTObjectSchemeData.GetSavingVersionCompatibility(selectedObjs.ToArray(), out isAllSchemeHasSameVersion);
                if (!isAllSchemeHasSameVersion)
                {
                    DialogResult dialogResult = MessageBox.Show(Manager_MCTObjectSchemeData.GetMsgText(SavingVersionCompatibility.ToString(), false), "Save To Buffer", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                        isAllSchemeHasSameVersion = true;
                }

                if (isAllSchemeHasSameVersion)
                {
                    DNEStorageFormat storageFormat = GetStorageFormatByUser();
                    m_Buffer = m_CurrentObject.SaveObjects(selectedObjs.ToArray(),
                                                        storageFormat,
                                                        SavingVersionCompatibility);

                    MCTMapForm.m_dicBuffer[1] = m_Buffer;

                    for (int i = 0; i < selectedObjs.Count; i++)
                        Manager_MCTObjectSchemeData.AddObjectSchemeData(selectedObjs[i], (uint)SavingVersionCompatibility, storageFormat);

                    LoadObjectsToListBox();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SaveObjects - to a buffer", McEx);
            }
        }

        private void btnGetObjByID_Click(object sender, EventArgs e)
        {
            lstObjects.Items.Clear();

            try
            {
                IDNMcObject obj = m_CurrentObject.GetObjectByID(ntxObjID.GetUInt32());
                LoadObjectsToListBox(new IDNMcObject[1] { obj });

                // lstObjects.Items.Add(obj);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjectByID", McEx);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            btnApply_Click(null, null);
            GeneralFuncs.CloseParentForm(this);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetID(ntxOverlayID.GetUInt32());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetID", McEx);
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
                ctrlConditionalselectorOverlay.Save();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetConditionalSelector", McEx);
            }
        }

        private DNEStorageFormat GetStorageFormatByFileName(string fileName)
        {
            DNEStorageFormat dNEStorageFormat = DNEStorageFormat._ESF_MAPCORE_BINARY;
            string fileExtension = Path.GetExtension(fileName);
            if (fileExtension.ToLower() == ".json")
                dNEStorageFormat = DNEStorageFormat._ESF_JSON;
            return dNEStorageFormat;
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            IDNMcObject[] objects = null;

            try
            {
                string fileName = OpenFileDlg();
                if (fileName != "")
                {
                    UserDataFactory UDF = new UserDataFactory();
                    uint uVersion;
                    DNEStorageFormat storageFormat = DNEStorageFormat._ESF_JSON;
                    objects = m_CurrentObject.LoadObjects(fileName, UDF,out storageFormat, out uVersion);
                    if (objects != null && objects.Length > 0)
                    {
                        foreach (IDNMcObject obj in objects)
                        {
                            Manager_MCTObjectSchemeData.AddObjectSchemeData(obj,                                
                                uVersion,
                                storageFormat,
                                (objects.Length == 1 && chxSaveFilePath.Checked) ? fileName : "");
                        }
                    }
                }                
            }   
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("LoadObjects", McEx);
            }
            
            try
            {
                if (objects != null)
                {
                    foreach (IDNMcObject obj in objects)
                    {
                        //Add private properties to dictionaries
                        IDNMcObjectScheme scheme = obj.GetScheme();
                        DNSVariantProperty[] properties = obj.GetProperties();

                        if (properties.Length != 0)
                        {
                            for (int i = 0; i < properties.Length; i++)
                            {
                                DNEPropertyType propType = properties[i].eType;
                                uint currPropId;

                                switch (propType)
                                {
                                    case DNEPropertyType._EPT_FONT:
                                        currPropId = properties[i].uID;
                                        IDNMcFont font = obj.GetFontProperty(currPropId);
                                        if(font != null)
                                            Managers.ObjectWorld.Manager_MCFont.AddToDictionary(font);
                                	    break;
                                    case DNEPropertyType._EPT_TEXTURE:
                                        currPropId = properties[i].uID;
                                        IDNMcTexture texture = obj.GetTextureProperty(currPropId);
                                        if (texture != null)
                                            Managers.ObjectWorld.Manager_MCTextures.AddToDictionary(texture);                                        
                                        break;
                                    case DNEPropertyType._EPT_MESH:
                                        currPropId = properties[i].uID;
                                        IDNMcMesh mesh = obj.GetMeshProperty(currPropId);
                                        if(mesh != null)
                                            Managers.ObjectWorld.Manager_MCMesh.AddToDictionary(mesh);
                                        break;
                                   /* case DNEPropertyType._EPT_CONDITIONALSELECTOR:
                                        currPropId = properties[i].uID;
                                        IDNMcConditionalSelector condSelector = obj.GetConditionalSelectorProperty(currPropId);
                                        break;*/
                                }
                            }
                        }

                        Manager_MCObjectSchemeItem.LoadStadaloneItems(scheme);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetConditionalSelector", McEx);
            }

            MainForm.RebuildObjectWorldTree(true, 1);

            // turn on relevant viewports render needed flags
            Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
            
        }

        private DNEStorageFormat GetStorageFormatByUser()
        {
            return (DNEStorageFormat)Enum.Parse(typeof(DNEStorageFormat), cmbStorageFormat.Text);
        }

        private void btnLoadFromBuffer_Click(object sender, EventArgs e)
        {
            UserDataFactory UDF = new UserDataFactory();

            try
            {
                uint uVersion;
                DNEStorageFormat storageFormat = DNEStorageFormat._ESF_JSON;

                IDNMcObject[] objects = m_CurrentObject.LoadObjects(MCTMapForm.m_dicBuffer[1], UDF,out storageFormat, out uVersion);
                
                // turn on relevant viewports render needed flags
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                if (objects != null && objects.Length > 0)
                {
                    foreach (IDNMcObject obj in objects)
                    {
                        Manager_MCTObjectSchemeData.AddObjectSchemeData(obj,
                            uVersion,
                            storageFormat);  
                    }
                    MainForm.RebuildObjectWorldTree(true, 1);
                }

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("LoadObjects", McEx);
            }
        }

        private void btnClearObjList_Click(object sender, EventArgs e)
        {
            lstObjects.ClearSelected();
        }

        private void LoadViewportsToListBox()
        {
            try
            {
                IDNMcOverlayManager currOM = m_CurrentObject.GetOverlayManager();
                Dictionary<object, uint> dViewports = Managers.MapWorld.Manager_MCViewports.AllParams;
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

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetViewportsIDs", McEx);
            }
        }


        private void LoadObjectsToListBox()
        {
            try
            {
                IDNMcObject[] objects = m_CurrentObject.GetObjects();
                LoadObjectsToListBox(objects);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjects", McEx);
            }
        }

        private void LoadObjectsToListBox(IDNMcObject[] objects)
        {
            try
            {
                lObjectsText.Clear();
                lObjectsValue.Clear();
                lstObjects.Items.Clear();

                foreach (IDNMcObject obj in objects)
                {
                    string name = Manager_MCNames.GetNameByObject(obj);
                    string version = "";
                    DNEStorageFormat storageFormat = DNEStorageFormat._ESF_MAPCORE_BINARY;
                    Manager_MCTObjectSchemeData.GetSchemeData(obj, out version, out storageFormat);
                    if (version != "")
                        name = name + "(" + version.Replace("_ESVC_", "") + ", " + storageFormat.ToString().Replace("_ESF_", "") + ")";
                    lObjectsText.Add(name);
                    lstObjects.Items.Add(name);
                    lObjectsValue.Add(obj);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjects", McEx);
            }
        }

        private void LoadViewportList()
        {
            IDNMcOverlayManager currOM = m_CurrentObject.GetOverlayManager();
            Dictionary<object, uint> m_Viewports = Manager_MCViewports.AllParams;
            IDNMcMapViewport[] viewportsArr = new IDNMcMapViewport[m_Viewports.Count];
            m_Viewports.Keys.CopyTo(viewportsArr, 0);

            for (int i = 0; i < viewportsArr.Length; i++)
            {
                if (((IDNMcMapViewport)viewportsArr[i]).OverlayManager == currOM)
                {
                    m_lstOverlayViewportValue.Add(viewportsArr[i]);
                    m_lstOverlayViewportText.Add(Manager_MCNames.GetNameByObject(viewportsArr[i]));
                    
                }
            }
            lstColorOverridingViewports.Items.AddRange(m_lstOverlayViewportText.ToArray());
            lstSaveAsRawViewports.Items.AddRange(m_lstOverlayViewportText.ToArray());
        }

        private void btnVisibilityApply_Click(object sender, EventArgs e)
        {
            try
            {
                DNEActionOptions actionOption = (DNEActionOptions)Enum.Parse(typeof(DNEActionOptions), cmbVisibility.Text);
                if (lstViewports.SelectedIndex >= 0)
                {
                    m_CurrentObject.SetVisibilityOption(actionOption, lViewportValue[lstViewports.SelectedIndex]);
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


        private void btnSaveAllObjectsAsNativeVectorMapLayer_Click(object sender, EventArgs e)
        {
            frmViewportsListContainSpecificOverlay ViewportsListContainSpecificOverlayForm = new frmViewportsListContainSpecificOverlay(m_CurrentObject);
            if (ViewportsListContainSpecificOverlayForm.ShowDialog() == DialogResult.OK)
            {
                try
                {    
                    string fileName = SaveFolderDialog();
                    if (fileName != "")
                    {                
                        m_CurrentObject.SaveAllObjectsAsNativeVectorMapLayer(fileName,
                                                                                ViewportsListContainSpecificOverlayForm.SelectedViewport,
                                                                                ViewportsListContainSpecificOverlayForm.MinScale,
                                                                                ViewportsListContainSpecificOverlayForm.MaxScale,
                                                                                1,
                                                                                tbMinSizeFactor.GetFloat(),
                                                                                tbMaxSizeFactor.GetFloat(),
                                                                                mcTilingScheme,
                                                                                ntxNumTilesInFileEdge.GetUInt32(),
                                                                                ctrlSaveParams1.GetSavingVersionCompatibility());
                    }
                    else
                        MessageBox.Show("Empty folder name", "Action aborted as a result of empty folder name", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                catch (MapCoreException McEx)
                {
            	    Utilities.ShowErrorMessage("SaveAllObjectsAsNativeVectorMapLayer", McEx);
                }
            }
            else
                MessageBox.Show("Action Aborted", "Action aborted as a result of unselected viewport", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //private bool CheckSizeFactor(NumericTextBox tbFactor)
        //{
        //    bool result = true;
        //    float value = tbFactor.GetFloat();
        //    if(value <= 0)
        //    {
        //        result = false;
        //        MessageBox.Show("Factor Value Invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        tbFactor.Select();
        //    }
        //    return result
        //}

        private void btnSaveObjectsAsNativeVectorMapLayer_Click(object sender, EventArgs e)
        {
            frmViewportsListContainSpecificOverlay ViewportsListContainSpecificOverlayForm = new frmViewportsListContainSpecificOverlay(m_CurrentObject);
            if (ViewportsListContainSpecificOverlayForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = SaveFolderDialog();
                    if (fileName != "")
                    {
                        List<IDNMcObject> selectedObjs = new List<IDNMcObject>();

                        ListBox.SelectedIndexCollection indices = lstObjects.SelectedIndices;
                        int[] ArrSelectedIndices = new int[indices.Count];
                        indices.CopyTo(ArrSelectedIndices, 0);

                        for (int idx = 0; idx < ArrSelectedIndices.Length; idx++)
                        {
                            selectedObjs.Add(lObjectsValue[ArrSelectedIndices[idx]]);
                        }
                        
                        m_CurrentObject.SaveObjectsAsNativeVectorMapLayer(selectedObjs.ToArray(),
                                                                            fileName,
                                                                            ViewportsListContainSpecificOverlayForm.SelectedViewport,
                                                                            ViewportsListContainSpecificOverlayForm.MinScale,
                                                                            ViewportsListContainSpecificOverlayForm.MaxScale,
                                                                            1,
                                                                            tbMinSizeFactor.GetFloat(), 
                                                                            tbMaxSizeFactor.GetFloat(),
                                                                            mcTilingScheme,
                                                                            ntxNumTilesInFileEdge.GetUInt32(),
                                                                            ctrlSaveParams1.GetSavingVersionCompatibility());
                    }
                    else
                        MessageBox.Show("Empty folder name", "Action aborted as a result of empty folder name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SaveObjectsAsNativeVectorMapLayer", McEx);
                }
            }
            else
                MessageBox.Show("Action Aborted", "Action aborted as a result of unselected viewport", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnColorOverridnigOK_Click(object sender, EventArgs e)
        {
            DNSColorPropertyOverriding[] propertyOverriding = new DNSColorPropertyOverriding[dgvColorOverriding.Rows.Count];

            for (int i = 0; i < (int)DNEColorPropertyType._ECPT_NUM; i++ )
            {
                propertyOverriding[i].bEnabled = (bool)dgvColorOverriding[1, i].Value;
                propertyOverriding[i].Color.r = dgvColorOverriding[2, i].Style.BackColor.R;
                propertyOverriding[i].Color.g = dgvColorOverriding[2, i].Style.BackColor.G;
                propertyOverriding[i].Color.b = dgvColorOverriding[2, i].Style.BackColor.B;
                propertyOverriding[i].Color.a = byte.Parse(dgvColorOverriding[3, i].Value.ToString());

                DNEColorComponentFlags compFlags = 0;

                if ((bool)dgvColorOverriding[4, i].Value == true)
                    compFlags |= DNEColorComponentFlags._ECCF_REPLACE_RGB;

                if ((bool)dgvColorOverriding[5, i].Value == true)
                    compFlags |= DNEColorComponentFlags._ECCF_REPLACE_ALPHA;

                if ((bool)dgvColorOverriding[6, i].Value == true)
                    compFlags |= DNEColorComponentFlags._ECCF_MODULATE_RGB;

                if ((bool)dgvColorOverriding[7, i].Value == true)
                    compFlags |= DNEColorComponentFlags._ECCF_MODULATE_ALPHA;

                if ((bool)dgvColorOverriding[8, i].Value == true)
                    compFlags |= DNEColorComponentFlags._ECCF_ADD_RGB;
               
                if ((bool)dgvColorOverriding[9, i].Value == true)
                    compFlags |= DNEColorComponentFlags._ECCF_ADD_ALPHA;
                
                if ((bool)dgvColorOverriding[10, i].Value == true)
                    compFlags |= DNEColorComponentFlags._ECCF_SUB_RGB;
                
                if ((bool)dgvColorOverriding[11, i].Value == true)
                    compFlags |= DNEColorComponentFlags._ECCF_SUB_ALPHA;

                if ((bool)dgvColorOverriding[12, i].Value == true)
                    compFlags |= DNEColorComponentFlags._ECCF_POSTPROCESS_ADD_RGB;

                if ((bool)dgvColorOverriding[13, i].Value == true)
                    compFlags |= DNEColorComponentFlags._ECCF_POSTPROCESS_SUB_RGB;

                propertyOverriding[i].eColorComponentsBitField = compFlags;
            }

            try
            {
                IDNMcMapViewport viewport = null;
                if(lstColorOverridingViewports.SelectedIndex>=0)
                    viewport = m_lstOverlayViewportValue[lstColorOverridingViewports.SelectedIndex];
                m_CurrentObject.SetColorOverriding(propertyOverriding, viewport);

                if (viewport != null)
                {
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(viewport);
                }
                else
                {
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetColorOverriding", McEx);
            }
        }

        private void FillColorOverridingDGV()
        {
            //Set DGV row headers
            dgvColorOverriding.Rows.Add((int)DNEColorPropertyType._ECPT_NUM);
            string [] enumNames = Enum.GetNames(typeof(DNEColorPropertyType));
            for (int i = 0; i < enumNames.Length-1; i++ )
            {
                dgvColorOverriding[2, i].Style.SelectionBackColor = Color.White;
                dgvColorOverriding[0, i].Value = enumNames[i];
            }

            try
            {
                IDNMcMapViewport vp = null;
                if (lstColorOverridingViewports.SelectedIndex >= 0)
                    vp = m_lstOverlayViewportValue[lstColorOverridingViewports.SelectedIndex];
                DNSColorPropertyOverriding[] propertyOverriding = m_CurrentObject.GetColorOverriding(vp);

                for (int i = 0; i < propertyOverriding.Length; i++)
                {
                    dgvColorOverriding[1, i].Value = propertyOverriding[i].bEnabled;
                  
                    dgvColorOverriding[2, i].Style.BackColor = Color.FromArgb(255, (int)propertyOverriding[i].Color.r, (int)propertyOverriding[i].Color.g, (int)propertyOverriding[i].Color.b);
                    dgvColorOverriding[3, i].Value = propertyOverriding[i].Color.a;

                    DNEColorComponentFlags compFlag = propertyOverriding[i].eColorComponentsBitField;

                    if ((compFlag & DNEColorComponentFlags._ECCF_REPLACE_RGB) == DNEColorComponentFlags._ECCF_REPLACE_RGB)
                        dgvColorOverriding[4, i].Value = true;
                    else
                        dgvColorOverriding[4, i].Value = false;

                    if ((compFlag & DNEColorComponentFlags._ECCF_REPLACE_ALPHA) == DNEColorComponentFlags._ECCF_REPLACE_ALPHA)
                        dgvColorOverriding[5, i].Value = true;
                    else
                        dgvColorOverriding[5, i].Value = false;

                    if ((compFlag & DNEColorComponentFlags._ECCF_MODULATE_RGB) == DNEColorComponentFlags._ECCF_MODULATE_RGB)
                        dgvColorOverriding[6, i].Value = true;
                    else
                        dgvColorOverriding[6, i].Value = false;

                    if ((compFlag & DNEColorComponentFlags._ECCF_MODULATE_ALPHA) == DNEColorComponentFlags._ECCF_MODULATE_ALPHA)
                        dgvColorOverriding[7, i].Value = true;
                    else
                        dgvColorOverriding[7, i].Value = false;

                    if ((compFlag & DNEColorComponentFlags._ECCF_ADD_RGB) == DNEColorComponentFlags._ECCF_ADD_RGB)
                        dgvColorOverriding[8, i].Value = true;
                    else
                        dgvColorOverriding[8, i].Value = false;

                    if ((compFlag & DNEColorComponentFlags._ECCF_ADD_ALPHA) == DNEColorComponentFlags._ECCF_ADD_ALPHA)
                        dgvColorOverriding[9, i].Value = true;
                    else
                        dgvColorOverriding[9, i].Value = false;

                    if ((compFlag & DNEColorComponentFlags._ECCF_SUB_RGB) == DNEColorComponentFlags._ECCF_SUB_RGB)
                        dgvColorOverriding[10, i].Value = true;
                    else
                        dgvColorOverriding[10, i].Value = false;

                    if ((compFlag & DNEColorComponentFlags._ECCF_SUB_ALPHA) == DNEColorComponentFlags._ECCF_SUB_ALPHA)
                        dgvColorOverriding[11, i].Value = true;
                    else
                        dgvColorOverriding[11, i].Value = false;

                    if ((compFlag & DNEColorComponentFlags._ECCF_POSTPROCESS_ADD_RGB) == DNEColorComponentFlags._ECCF_POSTPROCESS_ADD_RGB)
                        dgvColorOverriding[12, i].Value = true;
                    else
                        dgvColorOverriding[12, i].Value = false;

                    if ((compFlag & DNEColorComponentFlags._ECCF_POSTPROCESS_SUB_RGB) == DNEColorComponentFlags._ECCF_POSTPROCESS_SUB_RGB)
                        dgvColorOverriding[13, i].Value = true;
                    else
                        dgvColorOverriding[13, i].Value = false;

                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetColorOverriding", McEx);
            }
        }

        private void btnColorOverridingGet_Click(object sender, EventArgs e)
        {
            ColorOverridingGet();
        }


        private void ColorOverridingGet()
        {
            dgvColorOverriding.Rows.Clear();
            dgvColorOverriding.Refresh();
            dgvColorOverriding.RefreshEdit();

            FillColorOverridingDGV();
        }

        private void btnClearViewportList_Click(object sender, EventArgs e)
        {
            lstColorOverridingViewports.ClearSelected();
            ColorOverridingGet();
            lstColorOverridingViewportsSelectedIndex = -2;
        }

        int lstColorOverridingViewportsSelectedIndex = -2;
        private void lstColorOverridingViewports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstColorOverridingViewports.SelectedIndex != -1 && lstColorOverridingViewportsSelectedIndex != lstColorOverridingViewports.SelectedIndex)
            {
                ColorOverridingGet();
                lstColorOverridingViewportsSelectedIndex = lstColorOverridingViewports.SelectedIndex;
            }
        }

        private void dgvColorOverriding_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (e.ColumnIndex == 2)
            {
                ColorDialog colorDialog = new ColorDialog();

                colorDialog.Color = ((DataGridView)sender)[2, rowIndex].Style.BackColor;

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    dgvColorOverriding[2, rowIndex].Style.BackColor = colorDialog.Color;
                }
                dgvColorOverriding.RefreshEdit();
                dgvColorOverriding.Refresh();
                dgvColorOverriding.CurrentCell = null;
            }

            
        }

        private void dgvColorOverriding_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                int rowIndex = e.RowIndex;
                dgvColorOverriding[2, rowIndex].Value = int.Parse(dgvColorOverriding[3, rowIndex].Value.ToString());
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

        public List<string> lObjectsText
        {
            get { return m_lObjectsText; }
            set { m_lObjectsText = value; }
        }

        public List<IDNMcObject> lObjectsValue
        {
            get { return m_lObjectsValue; }
            set { m_lObjectsValue = value; }
        }

        public List<string> OverlaysTextList
        {
            get { return m_lstOverlayText; }
            set { m_lstOverlayText = value; }
        }

        public List<IDNMcOverlay> OverlaysValueList
        {
            get { return m_lstOverlayValue; }
            set { m_lstOverlayValue = value; }
        }

        #endregion

        private void lstViewports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstViewports.SelectedIndex != -1)
            {
                try
                {
                    chkDetectibility.Checked = m_CurrentObject.GetDetectibility(lViewportValue[lstViewports.SelectedIndex]);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetDetectibility", McEx);
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
                    cmbVisibility.Text = m_CurrentObject.GetVisibilityOption(lViewportValue[lstViewports.SelectedIndex]).ToString();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetVisibilityOption", McEx);
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

                GetOverlayState(lViewportValue[lstViewports.SelectedIndex]);

            }
        }

        private void GetOverlayState()
        {
            try
            {
                tbState.Text = ConvertByteArrayToString(m_CurrentObject.GetState(), " ");
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetState", McEx);
            }
        }

        private void GetOverlayState(IDNMcMapViewport viewport)
        {
            try
            {
                tbState.Text = ConvertByteArrayToString(m_CurrentObject.GetState(viewport), " ");
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetState", McEx);
            }
        }

        private string ConvertByteArrayToString(byte[] arr, string delimeted)
        {
            string buf = "";
            if (arr != null)
                foreach (byte value in arr)
                {
                    buf += (value.ToString() + delimeted);
                }
            return buf.Trim();
        }

        private byte[] ConvertStringToByteArray(string input)
        {
            string txtTemp = input;
            byte[] output = null;
            if (txtTemp != null && txtTemp.Trim() != string.Empty)
            {
                string txRepObjectStates = txtTemp;
                while (((txRepObjectStates = txtTemp.Replace("  ", " "))) != txtTemp)
                {
                    txtTemp = txRepObjectStates;
                }
                string[] arrInput = txtTemp.Split(" ".ToCharArray());
                output = new byte[arrInput.Length];
                for (int i = 0; i < arrInput.Length; i++)
                {
                    output[i] = byte.Parse(arrInput[i]);
                }
            }
            return output;
        }

        private void btnClearViewportSelection_Click(object sender, EventArgs e)
        {
            lstViewports.ClearSelected();
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
                cmbVisibility.Text = m_CurrentObject.GetVisibilityOption().ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVisibilityOption", McEx);
            }

            try
            {
                chkDetectibility.Checked = m_CurrentObject.GetDetectibility();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetDetectibility", McEx);
            }

            chxEffectiveVisibilityInViewport.CheckState = CheckState.Indeterminate;

            GetOverlayState();
        }

        private void btnDetectibility_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstViewports.SelectedIndex != -1)
                {
                    m_CurrentObject.SetDetectibility(chkDetectibility.Checked, lViewportValue[lstViewports.SelectedIndex]);
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(lViewportValue[lstViewports.SelectedIndex]);
                }
                else
                {
                    m_CurrentObject.SetDetectibility(chkDetectibility.Checked, null);
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

        private void btnGetSubItemsVisibility_Click(object sender, EventArgs e)
        {
            frmGetSpecificViewportList ViewportObjectListsForm = new frmGetSpecificViewportList(m_CurrentObject);
            if (ViewportObjectListsForm.ShowDialog() == DialogResult.OK)
            {
                uint[] subItemsIDs = null;
                bool visibility = false;
                try
                {
                    m_CurrentObject.GetSubItemsVisibility(out subItemsIDs, out visibility, ViewportObjectListsForm.SelectedViewport);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetSubItemsVisibility", McEx);
                }

                txtSubItemsVisibilityIds.Text = ConvertUintArrToString(subItemsIDs);
                chxSubItemsVisibility.Checked = visibility;
            }
        }

        private void btnSetSubItemsVisibility_Click(object sender, EventArgs e)
        {
            frmGetSpecificViewportList GetSpecificViewportListForm = new frmGetSpecificViewportList(m_CurrentObject);
            if (GetSpecificViewportListForm.ShowDialog() == DialogResult.OK)
            {
                uint[] subItemsIDs = ConvertStringToUintArr(txtSubItemsVisibilityIds.Text);
                try
                {
                    IDNMcMapViewport viewport = GetSpecificViewportListForm.SelectedViewport;
                    m_CurrentObject.SetSubItemsVisibility(subItemsIDs, chxSubItemsVisibility.Checked, viewport);

                    if (viewport != null)
                    {
                        Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(viewport);
                    }
                    else
                    {
                        Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject.GetOverlayManager());
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetSubItemsVisibility", McEx);
                }
            }
        }

        private string ConvertUintArrToString(uint[] values)
        {
            string Ret = "";

            if (values != null)
            {
                foreach (uint val in values)
                    Ret += val + " ";
            }

            // remove last comma
            if (Ret.Length > 0)
                Ret.Remove(Ret.Length - 1, 1);

            return Ret;
        }

        private uint[] ConvertStringToUintArr(string values)
        {
            List<uint> Ret = new List<uint>();

            char[] separator = new char[1];
            separator[0] = ' ';

            string[] splitedValues = values.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            uint shortVal;
            bool isParse = false;
            for (int i = 0; i < splitedValues.Length; i++)
            {
                isParse = uint.TryParse(splitedValues[i], out shortVal);
                Ret.Add((isParse == true) ? shortVal : (uint)0);
            }

            return Ret.ToArray();
        }

        private Form m_containerForm;
        private int rowIndex;
        
      
        private Form GetParentForm(Control ctr)
        {
            if (ctr.Parent is Form)
                return ctr.Parent as Form;
            else
                return GetParentForm(ctr.Parent);
        }

        void MapObjectCtrl_MouseClickEvent(object sender, Point mouseLocation)
        {
            try
            {
                MCTMapFormManager.MapForm.MapObjectCtrl.MouseClickEvent -= new MouseClickEventArgs(MapObjectCtrl_MouseClickEvent);
                IDNMcMapViewport Viewport = MCTMapFormManager.MapForm.Viewport;
                DNSMcVector3D screenPoint = new DNSMcVector3D(mouseLocation.X, mouseLocation.Y, 0);

                if (rowIndex >= 0)
                {
                    IDNMcObject mcObj = m_lObjectsValue[rowIndex];
                    if (mcObj != null && mcObj.GetScheme() != null)
                    {
                        IDNMcObjectLocation mcObjectLocation = null;

                        try
                        {
                            mcObjectLocation = mcObj.GetScheme().GetObjectLocation(0);
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("GetObjectLocation", McEx);
                        }

                        if (mcObjectLocation != null)
                        {
                            DNEMcPointCoordSystem locationCoordSystem;
                            mcObjectLocation.GetCoordSystem(out locationCoordSystem);
                            bool isRelativeToDtm = false;

                            try
                            {
                                bool m_bParam;
                                uint m_PropId;
                                mcObjectLocation.GetRelativeToDTM(out m_bParam, out m_PropId);

                                if (m_PropId == DNMcConstants._MC_EMPTY_ID) // shared
                                    isRelativeToDtm = m_bParam;
                                else
                                    isRelativeToDtm = mcObj.GetBoolProperty(m_PropId);
                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("GetRelativeToDTM", McEx);
                            }


                            if (locationCoordSystem == DNEMcPointCoordSystem._EPCS_SCREEN)
                            {
                                OnMapClick(screenPoint, screenPoint, locationCoordSystem, isRelativeToDtm);
                            }
                            else
                            {
                                MCTMapClick.ConvertScreenLocationToObjectLocation(Viewport, screenPoint, locationCoordSystem, mcObj.GetImageCalc(), this, isRelativeToDtm);
                            }
                           
                        }
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Convert points", McEx);
            }

            //show the form
            //m_containerForm.Show();
        }

        private void dgvSetEachObjectLocationPoint_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                rowIndex = e.RowIndex;
                m_containerForm = GetParentForm(this);
                //hide the container form
                m_containerForm.Hide();

                if (MCTMapFormManager.MapForm != null)
                {
                    MCTMapFormManager.MapForm.MapObjectCtrl.MouseClickEvent += new MouseClickEventArgs(MapObjectCtrl_MouseClickEvent);
                }
            }
        }

        private void btnSetEachObjectLocationPoint_Click(object sender, EventArgs e)
        {
            IDNMcObject rowObject;
            DNSMcVector3D rowPoint;
            object objPoint;

            List<IDNMcObject> lstObject = new List<IDNMcObject>();
            List<DNSMcVector3D> lstPoints = new List<DNSMcVector3D>();

            foreach (DataGridViewRow row in dgvSetEachObjectLocationPoint.Rows)
            {
                rowObject = (IDNMcObject)row.Cells[0].Tag;
                objPoint = row.Cells[1].Tag;
                
                if (objPoint != null)
                {
                    rowPoint = (DNSMcVector3D)objPoint;
                
                    lstObject.Add(rowObject);
                    lstPoints.Add(rowPoint);
                }
            }
            if (lstPoints.Count > 0)
            {
                try
                {
                    DNMcObject.SetEachObjectLocationPoint(lstObject.ToArray(), lstPoints.ToArray());
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetEachObjectLocationPoint", McEx);
                }
            }
        }

        private void btnSetEachObjectProperty_Click(object sender, EventArgs e)
        {
            List<IDNMcObject> objects = new List<IDNMcObject>();
            List<DNSVariantProperty> properties = new List<DNSVariantProperty>();

            if (dgvObjectProperties.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvObjectProperties.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        objects.Add((IDNMcObject)row.Cells[0].Tag);
                        properties.Add((DNSVariantProperty)row.Cells[1].Tag);
                    }
                }

                try
                {
                    DNMcObject.SetEachObjectProperty(objects.ToArray(), properties.ToArray());
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetEachObjectProperty", McEx);
                }
            }
        }

        private void btnOpenPrivatePropertiesForm_Click(object sender, EventArgs e)
        {
            if (cmbObjects.SelectedIndex < 0)
            {
                MessageBox.Show("Select Object");
                return;
            }

            IDNMcObject selectedObject = m_lObjectsValue[cmbObjects.SelectedIndex];
            frmPropertiesIDList PropertyIDListForm = new frmPropertiesIDList(selectedObject);
            PropertyIDListForm.IsCloseWithoutSave = true;

            if (PropertyIDListForm.ShowDialog() == DialogResult.OK)
            {
                AddObjectProperties(m_lObjectsText[cmbObjects.SelectedIndex], selectedObject, PropertyIDListForm.ChangedProperties);
            }
        }


        private void AddObjectProperties(string objectText, IDNMcObject selectedObject, List<DNSVariantProperty> ChangedProperties)
        {
            foreach (DNSVariantProperty property in ChangedProperties)
            {
                dgvObjectProperties.Rows.Add(objectText, property.uID, property.eType, property.Value);

                DataGridViewRow row = dgvObjectProperties.Rows[dgvObjectProperties.Rows.Count - 2];
                row.Cells[0].Tag = selectedObject;
                row.Cells[1].Tag = property;
            }
        }

        private void btnStateApply_Click(object sender, EventArgs e)
        {
            string txtObjectStates = tbState.Text;
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
                    if (abyStates!= null && abyStates.Length == 1)
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
        }

        private void tbState_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(tbState, "");
        }

        private void tbState_Validating(object sender, CancelEventArgs e)
        {
            if (tbState.Text == string.Empty)
            {
                return;
            }

            string txObjectStates = tbState.Text;
            string txRepObjectStates = txObjectStates;
            while (((txRepObjectStates = txObjectStates.Replace("  ", " "))) != txObjectStates)
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
                    errorProvider1.SetError(tbState, "Values must be byte (0..255) separated with blank(s)");
                    return;
                }
            }
        }

        private void cmbStorageFormat_TextChanged(object sender, EventArgs e)
        {
            MCTMapForm.m_eOStorageFormat = GetStorageFormatByUser();
        }

        private void btnLoadObjectsFromRawVectorData_Click(object sender, EventArgs e)
        {
            frmLoadObjectsFromRawVectorData frmLoadObjectsFromRawVectorData = new frmLoadObjectsFromRawVectorData(m_CurrentObject);
            if (frmLoadObjectsFromRawVectorData.ShowDialog() == DialogResult.OK)
            {
                if (frmLoadObjectsFromRawVectorData.LoadedObjects != null && frmLoadObjectsFromRawVectorData.LoadedObjects.Length > 0)
                {
                    LoadObjectsToListBox(frmLoadObjectsFromRawVectorData.LoadedObjects);
                    MainForm.RebuildObjectWorldTree();
                }
            }
        }

        private void btnTilingScheme_Click(object sender, EventArgs e)
        {
            frmSTilingSchemeParams frmSTilingSchemeParams = new frmSTilingSchemeParams();

            frmSTilingSchemeParams.STilingScheme = mcTilingScheme;
            if (frmSTilingSchemeParams.ShowDialog() == DialogResult.OK)
                mcTilingScheme = frmSTilingSchemeParams.STilingScheme;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GeneralFuncs.CloseParentForm(this);
        }

        private enum SaveAsRawOptions
        {
            SaveToFile,
            SaveToMemory,
            SaveAllToFile,
            SaveAllToMemory
        }

        private void btnSaveAsRawVectorMapLayer_Click(object sender, EventArgs e)
        {
            if(cbAsMemoryBuffer.Checked)
                SaveRaw(SaveAsRawOptions.SaveToMemory);
            else
                SaveRaw(SaveAsRawOptions.SaveToFile);
        }

        private void btnSaveAllAsRawVectorMapLayer_Click(object sender, EventArgs e)
        {
            if (cbAsMemoryBuffer.Checked)
                SaveRaw(SaveAsRawOptions.SaveAllToMemory);
            else
                SaveRaw(SaveAsRawOptions.SaveAllToFile);
        }

        private void SaveRaw(SaveAsRawOptions saveAsRawOption)
        {
            if ((saveAsRawOption == SaveAsRawOptions.SaveToFile || saveAsRawOption == SaveAsRawOptions.SaveToMemory) &&
                lstObjects.SelectedItem == null)
            {
                MessageBox.Show("You need to choose at least one object", "Save Object As Raw Vector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        
            DNEGeometry eGeometry = (DNEGeometry)Enum.Parse(typeof(DNEGeometry), cmbGeometryFilter.Text);
            float fCameraYawAngle = ntbCameraYawAngle.GetFloat();
            float fCameraScale = ntbCameraScale.GetFloat();
            IDNMcMapViewport viewport = null;
            if (lstSaveAsRawViewports.SelectedIndex >= 0)
                viewport = m_lstOverlayViewportValue[lstSaveAsRawViewports.SelectedIndex];

            string fileName = "";
            string layerName = txtLayerName.Text;

            lbAdditionalFiles.Items.Clear();

            SaveFileDialog SFD1 = new SaveFileDialog();
            SFD1.Title = "Save Object As Raw Vector";
            SFD1.Filter = "Raw Vector Formats With Styling Support (*.kmz, *.kml, *.dxf)|*.kmz;*.kml;*.dxf;|" +
                "All Files|*.*";
            SFD1.RestoreDirectory = true;

            if (SFD1.ShowDialog() == DialogResult.OK)
            {
                fileName = SFD1.FileName;
                if (fileName != "")
                {
                    string fullPath = "";
                    string strFileName = "";
                    string[] paAdditionalFiles = null;
                    DNSMcFileInMemory[] paAdditionalFilesInMemory;
                    IDNOverlayManagerAsyncOperationCallback overlayManagerAsyncOperationCallback = null;

                    if (saveAsRawOption == SaveAsRawOptions.SaveAllToMemory || saveAsRawOption == SaveAsRawOptions.SaveToMemory)
                    {
                        try
                        {
                            strFileName = Path.GetFileName(fileName);
                            fullPath = fileName.Replace(strFileName, "").Trim();
                        }
                        catch (ArgumentException ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }

                    if (chxSaveObjectsAsRawVectorAsync.Checked)
                    {
                        overlayManagerAsyncOperationCallback = new MCTOverlayManagerAsyncOperationCallback(this, fullPath);
                    }
                    m_LastAsyncOperationCallback = overlayManagerAsyncOperationCallback;

                    if (saveAsRawOption == SaveAsRawOptions.SaveToFile || saveAsRawOption == SaveAsRawOptions.SaveToMemory)
                    {

                        List<IDNMcObject> selectedObjs = GetSelectedObjects();

                        if (saveAsRawOption == SaveAsRawOptions.SaveToFile)
                        {
                            try
                            {
                                m_CurrentObject.SaveObjectsAsRawVectorData(selectedObjs.ToArray(),
                                    viewport, fCameraYawAngle, fCameraScale, layerName, fileName, out paAdditionalFiles, overlayManagerAsyncOperationCallback, eGeometry);
                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("SaveObjectsAsRawVectorData", McEx);
                            }
                        }
                        else
                        {
                            byte[] pauMemoryBuffer;
                            try
                            {
                                m_CurrentObject.SaveObjectsAsRawVectorData(selectedObjs.ToArray(),
                                    viewport, fCameraYawAngle, fCameraScale, layerName, strFileName,
                                    out pauMemoryBuffer, out paAdditionalFilesInMemory, overlayManagerAsyncOperationCallback, eGeometry);
                                if (!chxSaveObjectsAsRawVectorAsync.Checked)
                                    SaveByteArrayToFile(fileName, pauMemoryBuffer, paAdditionalFilesInMemory, fullPath);
                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("SaveObjectsAsRawVectorData as memory buffer", McEx);
                            }
                        }
                    }
                    else if (saveAsRawOption == SaveAsRawOptions.SaveAllToFile)
                    {
                        try
                        {
                            m_CurrentObject.SaveAllObjectsAsRawVectorData(viewport, fCameraYawAngle, fCameraScale, layerName, fileName, out paAdditionalFiles, overlayManagerAsyncOperationCallback, eGeometry);
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("SaveAllObjectsAsRawVectorData", McEx);
                        }
                    }
                    else if (saveAsRawOption == SaveAsRawOptions.SaveAllToMemory)
                    {
                        try
                        {
                            byte[] pauMemoryBuffer;
                            //DNSMcFileInMemory[] paAdditionalFilesInMemory;

                            m_CurrentObject.SaveAllObjectsAsRawVectorData(viewport, fCameraYawAngle, fCameraScale, layerName, strFileName,
                                        out pauMemoryBuffer, out paAdditionalFilesInMemory, overlayManagerAsyncOperationCallback, eGeometry);
                            if (!chxSaveObjectsAsRawVectorAsync.Checked)
                                SaveByteArrayToFile(fileName, pauMemoryBuffer, paAdditionalFilesInMemory, fullPath);
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("SaveAllObjectsAsRawVectorData as memory buffer", McEx);
                        }
                    }

                   
                    if ((!chxSaveObjectsAsRawVectorAsync.Checked) && paAdditionalFiles != null)
                    {
                        ShowAdditionalFiles(paAdditionalFiles);
                    }
                }
            }
        }

        public void ShowAdditionalFiles(string[] aAdditionalFiles)
        {
            lbAdditionalFiles.Items.AddRange(aAdditionalFiles);
        }

        public void SaveByteArrayToFile(string fileName, byte[] pauMemoryBuffer, DNSMcFileInMemory[] paAdditionalFilesInMemory, string fullPath)
        {
            SaveByteArrayToFile(fileName, pauMemoryBuffer);

            if(paAdditionalFilesInMemory!= null)
            {
                foreach(DNSMcFileInMemory fileInMemory in paAdditionalFilesInMemory)
                {
                    lbAdditionalFiles.Items.Add(fileInMemory.strFileName);
                    //if(fileInMemory.auMemoryBuffer != null)
                        SaveByteArrayToFile(fileInMemory.strFileName, fileInMemory.auMemoryBuffer, fullPath);
                }
            }
        }

        private void SaveByteArrayToFile(string fileName, byte[] pauMemoryBuffer, string fullPath = "")
        {
            try
            {
                string path = "";
                if (fileName.Contains("/"))
                {
                    string[] folders = fileName.Split('/');

                    for (int i = 0; i < folders.Length - 1; i++)
                    {
                        string folder = folders[i];
                        path = Path.Combine(fullPath, folder);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        fullPath = path;
                    }
                    path = Path.Combine(fullPath, folders[folders.Length - 1]);
                    File.WriteAllBytes(@path, pauMemoryBuffer);
                }
                else if (fullPath != "")
                {
                    path = Path.Combine(fullPath, fileName);
                    File.WriteAllBytes(@path, pauMemoryBuffer);
                }
                else
                {
                    File.WriteAllBytes(@fileName, pauMemoryBuffer);
                }
            }
            catch(ArgumentException ex)
            { MessageBox.Show(ex.Message, "Save memory buffer to file"); }
            catch (PathTooLongException ex)
            { MessageBox.Show(ex.Message, "Save memory buffer to file"); }
            catch (DirectoryNotFoundException ex)
            { MessageBox.Show(ex.Message, "Save memory buffer to file"); }
            catch (IOException ex)
            { MessageBox.Show(ex.Message, "Save memory buffer to file"); }
            catch (UnauthorizedAccessException ex)
            { MessageBox.Show(ex.Message, "Save memory buffer to file"); }
            catch (NotSupportedException ex)
            { MessageBox.Show(ex.Message, "Save memory buffer to file"); }
            catch (SecurityException ex)
            { MessageBox.Show(ex.Message, "Save memory buffer to file"); }
        }

        private void lstSaveAsRawViewports_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                IDNMcMapViewport viewport = null;
                if (lstSaveAsRawViewports.SelectedIndex >= 0)
                {
                    viewport = m_lstOverlayViewportValue[lstSaveAsRawViewports.SelectedIndex];
                    if (viewport != null && viewport.MapType == DNEMapType._EMT_2D)
                    {
                        float scale = viewport.GetCameraScale();
                        float yaw = 0;
                        viewport.GetCameraOrientation(out yaw);

                        ntbCameraScale.SetFloat(scale);
                        ntbCameraYawAngle.SetFloat(yaw);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCameraScale/GetCameraOrientation", McEx);
            }

        }

        private void chxIsShowVersion_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelAsyncOperation_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_LastAsyncOperationCallback != null)
                    m_CurrentObject.CancelAsyncSavingObjects(m_LastAsyncOperationCallback);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CancelAsyncSavingObjects", McEx);
            }
        }

        public void OnMapClick(DNSMcVector3D screenLocation, DNSMcVector3D location, DNEMcPointCoordSystem locationCoordSystem, bool isRelativeToDtm)
        {
            if (isRelativeToDtm)
                location.z = 0;
            dgvSetEachObjectLocationPoint[1, rowIndex].Tag = location;
            dgvSetEachObjectLocationPoint[2, rowIndex].Value = location.x;
            dgvSetEachObjectLocationPoint[3, rowIndex].Value = location.y;
            dgvSetEachObjectLocationPoint[4, rowIndex].Value = location.z;

            //show the form
            m_containerForm.Show();
        }

        public void OnMapClickError(DNEMcErrorCode eErrorCode, string functionName)
        {
            //show the form
            m_containerForm.Show();

            dgvSetEachObjectLocationPoint[1, rowIndex].Tag = null;
            dgvSetEachObjectLocationPoint[2, rowIndex].Value = "";
            dgvSetEachObjectLocationPoint[3, rowIndex].Value = "";
            dgvSetEachObjectLocationPoint[4, rowIndex].Value = "";

            MessageBox.Show(IDNMcErrors.ErrorCodeToString(eErrorCode), functionName, MessageBoxButtons.OK);
        }

        private void lstObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            chxSaveFilePath.Enabled = lstObjects.SelectedIndices.Count == 1;
        }
    }
}
