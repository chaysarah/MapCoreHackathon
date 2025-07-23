using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.GUI.Trees;
using MCTester.GUI.Map;
using MCTester.MapWorld.Assist_Forms;
using System.IO;
using MCTester.Managers.MapWorld;
using MCTester.Managers;
using MCTester.ObjectWorld.ObjectsUserControls;
using MapCore.Common;
using MCTester.Managers.ObjectWorld;
using MCTester.Automation;
using System.Globalization;
using System.Windows;
using MessageBox = System.Windows.Forms.MessageBox;

namespace MCTester.ObjectWorld.OverlayManagerWorld
{
	public partial class ucOverlayManager : UserControl,IUserControlItem, IOnMapClick
    {
		private IDNMcOverlayManager m_CurrentObject;
        private byte[] m_Buffer;
        private string m_FileName;
        private OpenFileDialog OFD;
        private SaveFileDialog SFD;
        private List<string> m_lViewportText;
        private List<IDNMcMapViewport> m_lViewportTag;
        private List<string> m_lObjectText;
        private List<IDNMcObject> m_lObjectTag;
        
        private List<string> m_lstSchemesText = new List<string>();
        private List<IDNMcObjectScheme> m_lstSchemesValue = new List<IDNMcObjectScheme>();
        
        private uint MASK0 = 1;
        private uint MASK1 = 2;
        private uint MASK2 = 4;
        private uint MASK3 = 8;
        private uint MASK4 = 16;
        private uint MASK5 = 32;
        private uint MASK6 = 64;
        private uint MASK7 = 128;
        private uint MASK8 = 256;
        private uint MASK9 = 512;

		public ucOverlayManager()
		{
			InitializeComponent();
            OFD = new OpenFileDialog();
            SFD = new SaveFileDialog();
            cmbCollectionMode.Items.AddRange(Enum.GetNames(typeof(DNECollectionsMode)));
            cmbStorageFormat.Items.AddRange(Enum.GetNames(typeof(DNEStorageFormat)));
            cmbStorageFormat.Text = MCTMapForm.m_eOMStorageFormat.ToString();

            m_lObjectText = new List<string>();
            m_lObjectTag = new List<IDNMcObject>();
            m_lViewportText = new List<string>();
            m_lViewportTag = new List<IDNMcMapViewport>();

            lstViewports.DisplayMember = "lViewportText";
            lstViewports.ValueMember = "lViewportTag";
            lstScreenArrangementObjects.DisplayMember = "lObjectText";
            lstScreenArrangementObjects.ValueMember = "lObjectTag";

        }
		
        private void GetSymbologyStandardFlags(DNESymbologyStandard symbologyStandard)
        {
            Manager_MCTSymbology.SymbologyStandardFlags symbologyStandardFlags = Manager_MCTSymbology.GetSymbologyStandardFlags(m_CurrentObject, symbologyStandard);
            cbIgnoreNonExistentAmplifiers.Checked = symbologyStandardFlags.IgnoreNonExistentAmplifiers;
            cbShowGeoInMetricProportionOM.Checked = symbologyStandardFlags.ShowGeoInMetricProportion;
        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcOverlayManager)aItem;

            Manager_MCTSymbology.InitSymbologyStandardFlags(m_CurrentObject);

            cmbSymbologyStandard.Items.AddRange(Enum.GetNames(typeof(DNESymbologyStandard)));
            cmbSymbologyStandard.Text = Manager_MCTSymbology.GetSymbologyStandard(m_CurrentObject).ToString();

            GetSymbologyStandardFlags(Manager_MCTSymbology.GetSymbologyStandard(m_CurrentObject));

            try
            {            
                Dictionary<object, uint> dViewports = Manager_MCViewports.AllParams;
                foreach (IDNMcMapViewport vp in dViewports.Keys)
                {
                    if (vp.OverlayManager == m_CurrentObject)
                    {
                        string name = Manager_MCNames.GetNameByObject(vp, "Viewport");
                        lViewportText.Add(name);
                        lViewportTag.Add(vp);
                    }                    
                }

                lstViewports.Items.AddRange(lViewportText.ToArray());
                lstSizeFactorViewports.Items.AddRange(lViewportText.ToArray());

                lstScreenArrangementViewports.Items.AddRange(lViewportText.ToArray());                 
            }
            catch (MapCoreException McEx)
            {
            	Utilities.ShowErrorMessage("GetViewportsIDs", McEx);
            }

            try
            {
                ctrlGridCoordinateSystemDetails.LoadData(m_CurrentObject.GetCoordinateSystemDefinition());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GridCoordinateSystem", McEx);
            }

            LoadSchemesToList();
       
          

            lstViewports_SelectedIndexChanged(null, null);

            string[] enumNames = Enum.GetNames(typeof(DNESizePropertyType));
            dgvObjectsSizeFactor.Rows.Add(enumNames.Length - 1);
            dgvVectorItemsSizeFactor.Rows.Add(enumNames.Length - 1);
            FillSizeFactor();

        }
		#endregion

        private void SaveParams()
        {
            
        }

        private void SaveSizeFactor()
        {
            SaveSizeFactorDGVs(dgvObjectsSizeFactor, false);
            SaveSizeFactorDGVs(dgvVectorItemsSizeFactor, true);
        }

        private void FillSizeFactor()
        {
            FillSizeFactorDGV(dgvObjectsSizeFactor, false);
            FillSizeFactorDGV(dgvVectorItemsSizeFactor, true);
        }

        private void LoadSchemesToList()
        {
            try
            {
                m_lstSchemesText.Clear();
                m_lstSchemesValue.Clear();
                lstSchemes.Items.Clear();

                IDNMcObjectScheme[] schemes = Manager_MCObjectScheme.GetSchemesWithoutTempSchemes(m_CurrentObject.GetObjectSchemes());
                string name = string.Empty;
                foreach (IDNMcObjectScheme scheme in schemes)
                {
                    name = Manager_MCNames.GetNameByObject(scheme, "Scheme");
                    string version = "";
                    DNEStorageFormat storageFormat = DNEStorageFormat._ESF_MAPCORE_BINARY;
                    Manager_MCTObjectSchemeData.GetSchemeData(scheme, out version, out storageFormat);
                    if (version != "")
                        name = name + "(" + version.Replace("_ESVC_", "") + ", " + storageFormat.ToString().Replace("_ESF_", "") + ")";
                    m_lstSchemesText.Add(name);
                    m_lstSchemesValue.Add(scheme);
                    lstSchemes.Items.Add(name);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjectSchemes", McEx);
            }
           
        }

        private List<float> GetScreenTerrainItemsConsistencyScaleSteps()
        {
            List <float> Ret = new List<float>();
            string[] ScaleSteps;

            if (txtScreenTerrainItemsConsistencyScaleSteps.Text != "")
            {
                string[] delimeter = null;
                ScaleSteps = txtScreenTerrainItemsConsistencyScaleSteps.Text.Trim().Split(delimeter, StringSplitOptions.RemoveEmptyEntries);

                if (ScaleSteps.Length != 0)
                {
                    float result;
                    foreach (string step in ScaleSteps)
                    {
                        if (float.TryParse(step, out result) == true)
                            Ret.Add(result);
                    }
                }
            }

            return Ret;
        }

        private void btnSaveAllToFile_Click(object sender, EventArgs e)
        {
            try
            {

                bool isAllSchemeHasSameVersion;
                IDNMcObjectScheme[] mcObjectSchemes = m_CurrentObject.GetObjectSchemes();
                DNESavingVersionCompatibility SavingVersionCompatibility = Manager_MCTObjectSchemeData.GetSavingVersionCompatibility(mcObjectSchemes, out isAllSchemeHasSameVersion);

                if (!isAllSchemeHasSameVersion)
                {
                    DialogResult dialogResult = MessageBox.Show(Manager_MCTObjectSchemeData.GetMsgText(SavingVersionCompatibility.ToString()), "Save All To File", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                        isAllSchemeHasSameVersion = true;
                }

                if (isAllSchemeHasSameVersion)
                {
                    m_FileName = SaveFileDlg();

                    if (m_FileName != "")
                    {
                        DNEStorageFormat format = GetStorageFormatByFileName(m_FileName);
                        m_CurrentObject.SaveAllObjectSchemes(m_FileName,
                                                        format,
                                                        SavingVersionCompatibility);

                        if (mcObjectSchemes != null)
                        {
                            for (int i = 0; i < mcObjectSchemes.Length; i++)
                                Manager_MCTObjectSchemeData.AddObjectSchemeData(mcObjectSchemes[i], (uint)SavingVersionCompatibility, format);

                            LoadSchemesToList();
                        }

                    }

                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SaveAllObjectSchemes", McEx);
            }
        }

        

        private void btnSaveAllToBuffer_Click(object sender, EventArgs e)
        {
            try
            {
                bool isAllSchemeHasSameVersion;
                IDNMcObjectScheme[] mcObjectSchemes = m_CurrentObject.GetObjectSchemes();
                DNEStorageFormat storageFormat = GetStorageFormatByUser();
                DNESavingVersionCompatibility SavingVersionCompatibility = Manager_MCTObjectSchemeData.GetSavingVersionCompatibility(mcObjectSchemes, out isAllSchemeHasSameVersion);
                if (!isAllSchemeHasSameVersion)
                {
                    DialogResult dialogResult = MessageBox.Show(Manager_MCTObjectSchemeData.GetMsgText(SavingVersionCompatibility.ToString()), "Save All To Buffer", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                        isAllSchemeHasSameVersion = true;
                }
                if (isAllSchemeHasSameVersion)
                {
                    m_Buffer = m_CurrentObject.SaveAllObjectSchemes(storageFormat, SavingVersionCompatibility);

                    MCTMapForm.m_dicBuffer[1] = m_Buffer;

                    for (int i = 0; i < mcObjectSchemes.Length; i++)
                        Manager_MCTObjectSchemeData.AddObjectSchemeData(mcObjectSchemes[i], (uint)SavingVersionCompatibility, storageFormat);

                    LoadSchemesToList();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SaveAllObjectSchemes", McEx);
            }
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            if (lstSchemes.SelectedItem != null)
            {
                IDNMcObjectScheme[] selectedSchemes = new IDNMcObjectScheme[lstSchemes.SelectedItems.Count];

                try
                {

                    selectedSchemes = GetUserSelectedSchemes();
                    bool isAllSchemeHasSameVersion;
                    DNESavingVersionCompatibility SavingVersionCompatibility = Manager_MCTObjectSchemeData.GetSavingVersionCompatibility(selectedSchemes, out isAllSchemeHasSameVersion);
                    if (!isAllSchemeHasSameVersion)
                    {
                        DialogResult dialogResult = MessageBox.Show(Manager_MCTObjectSchemeData.GetMsgText(SavingVersionCompatibility.ToString()), "Save To File", MessageBoxButtons.OKCancel);
                        if (dialogResult == DialogResult.OK)
                            isAllSchemeHasSameVersion = true;
                    }
                    if (isAllSchemeHasSameVersion)
                    {
                        m_FileName = SaveFileDlg();
                        if (m_FileName != "")
                        {
                            DNEStorageFormat storageFormat = GetStorageFormatByFileName(m_FileName);
                            m_CurrentObject.SaveObjectSchemes(selectedSchemes,
                                                                m_FileName,
                                                                storageFormat,
                                                                SavingVersionCompatibility);
                            for (int i = 0; i < selectedSchemes.Length; i++)
                                Manager_MCTObjectSchemeData.AddObjectSchemeData(selectedSchemes[i], (uint)SavingVersionCompatibility, storageFormat, selectedSchemes.Length == 1 && chxSaveFilePath.Checked ? m_FileName : "");
                            
                            LoadSchemesToList(); 
                            SaveParamsData.SavePropertiesCSV(m_FileName, selectedSchemes[0]);

                        }
                    }

                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SaveObjectSchemes", McEx);
                }

            }
            else
                MessageBox.Show("You need to choose at least one object scheme");            
        }

     

       
        private void btnSaveToBuffer_Click(object sender, EventArgs e)
        {
            try
            {
                IDNMcObjectScheme[] selectedSchemes = GetUserSelectedSchemes();
                bool isAllSchemeHasSameVersion;
                DNESavingVersionCompatibility SavingVersionCompatibility = Manager_MCTObjectSchemeData.GetSavingVersionCompatibility(selectedSchemes, out isAllSchemeHasSameVersion);
                if (!isAllSchemeHasSameVersion)
                {
                    DialogResult dialogResult = MessageBox.Show(Manager_MCTObjectSchemeData.GetMsgText(SavingVersionCompatibility.ToString()), "Save To Buffer", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                        isAllSchemeHasSameVersion = true;
                }

                if (isAllSchemeHasSameVersion) {
                    DNEStorageFormat storageFormat = GetStorageFormatByUser();
                    m_Buffer = m_CurrentObject.SaveObjectSchemes(selectedSchemes,
                                                                 GetStorageFormatByUser(),
                                                                 SavingVersionCompatibility);

                    MCTMapForm.m_dicBuffer[1] = m_Buffer;

                    for (int i = 0; i < selectedSchemes.Length; i++)
                        Manager_MCTObjectSchemeData.AddObjectSchemeData(selectedSchemes[i], (uint)SavingVersionCompatibility, storageFormat);
                   
                    LoadSchemesToList();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SaveObjectSchemes", McEx);
            }
        }
        private IDNMcObjectScheme[] GetUserSelectedSchemes()
        {
            IDNMcObjectScheme[] selectedSchemes = new IDNMcObjectScheme[lstSchemes.SelectedItems.Count];

            for (int i = 0; i < lstSchemes.SelectedIndices.Count; i++)
            {
                selectedSchemes[i] = m_lstSchemesValue[lstSchemes.SelectedIndices[i]];
            }

            return selectedSchemes;

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
            IDNMcObjectScheme [] ObjectSchemes  = null;
            
            try
            {
                m_FileName = OpenFileDlg();

                if (m_FileName != "")
                {    
                    UserDataFactory UDF = new UserDataFactory();
                    bool pbObjectDataDetected = false;
                    uint uVersion;
                    DNEStorageFormat storageFormat = DNEStorageFormat._ESF_JSON;
                    ObjectSchemes = m_CurrentObject.LoadObjectSchemes(m_FileName, UDF, out pbObjectDataDetected, out storageFormat, out uVersion);

                    if (ObjectSchemes != null && ObjectSchemes.Length > 0)
                    {
                        foreach (IDNMcObjectScheme scheme in ObjectSchemes)
                        {
                            m_CurrentObject.SetObjectSchemeLock(scheme, true);
                            Manager_MCObjectSchemeItem.LoadStadaloneItems(scheme);

                            Manager_MCTObjectSchemeData.AddObjectSchemeData(scheme,
                                uVersion,
                                storageFormat,
                                (ObjectSchemes.Length == 1 && chxSaveFilePath.Checked) ? m_FileName : "");
                        }
                       

                        // in case user want to load scheme csv file as well
                        if (SaveParamsData.IsSaveLoadPropertiesCSV == true )
                        {
                            string csvPath = Path.GetDirectoryName(m_FileName) + "\\" + Path.GetFileNameWithoutExtension(m_FileName) + ".csv";
                            if (System.IO.File.Exists(csvPath))
                            {
                                StreamReader STR = new StreamReader(csvPath);

                                List<string> lSourceLines = new List<string>();
                                char[] trimChar = new char[1];
                                trimChar[0] = ',';

                                while (!STR.EndOfStream)
                                {
                                    string line = STR.ReadLine();
                                    lSourceLines.Add(line.TrimEnd(trimChar));
                                }

                                STR.Close();

                                for (int i = 0; i < lSourceLines.Count; i++)
                                {
                                    string[] lineValues = lSourceLines[i].Split(',');

                                    if (lineValues.Length == 3)
                                    {
                                        try
                                        {
                                            ObjectSchemes[0].SetPropertyName(lineValues[2], uint.Parse(lineValues[0]));
                                        }
                                        catch (MapCoreException McEx)
                                        {
                                            Utilities.ShowErrorMessage("SetPropertyName", McEx);
                                        }
                                    }
                                }
                            }
                            else
                                MessageBox.Show("Properties id's CSV file was not found", "CSV file was not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        MainForm.RebuildObjectWorldTree(true);
                    }

                    if (pbObjectDataDetected)
                        MessageBox.Show("Warning: the file contains objects, their schemes only were loaded", "Load object schemes from file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("LoadObjectSchemes", McEx);
                return;
            }
        }

        private DNEStorageFormat GetStorageFormatByUser()
        {
            return (DNEStorageFormat)Enum.Parse(typeof(DNEStorageFormat), cmbStorageFormat.Text);
        }

        private void btnLoadFromBuffer_Click(object sender, EventArgs e)
        {
            try
            {
                UserDataFactory UDF = new UserDataFactory();

                bool pbObjectDataDetected = false;
                uint uVersion;
                DNEStorageFormat storageFormat = DNEStorageFormat._ESF_JSON;

                IDNMcObjectScheme[] schemesFromBuffer = m_CurrentObject.LoadObjectSchemes(MCTMapForm.m_dicBuffer[1], UDF, out pbObjectDataDetected, out storageFormat, out uVersion);

                if (schemesFromBuffer != null && schemesFromBuffer.Length > 0)
                {
                    foreach (IDNMcObjectScheme scheme in schemesFromBuffer)
                    {
                        m_CurrentObject.SetObjectSchemeLock(scheme, true);
                        Manager_MCObjectSchemeItem.LoadStadaloneItems(scheme);

                        Manager_MCTObjectSchemeData.AddObjectSchemeData(scheme,
                            uVersion,
                            storageFormat);
                    }
                    MainForm.RebuildObjectWorldTree(true);
                }
                if (pbObjectDataDetected)
                    MessageBox.Show("Warning: the memory buffer contains objects, their schemes only were loaded", "Load object schemes from buffer", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("LoadObjectSchemes", McEx);
            }
        }

        private string SaveFileDlg()
        {
            SFD.Title = "Save Object Scheme To File";
            SFD.Filter = "MapCore Scheme Files (*.mcsch, *.mcsch.json, *.m, *.json) |*.mcsch; *.mcsch.json; *.m; *.json;|" +
                "MapCore Scheme Binary Files (*.mcsch,*.m)|*.mcsch;*.m;|" +
                "MapCore Scheme Json files (*.mcsch.json, *.json)|*.mcsch.json; *.json;|" +
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
            OFD.Title = "Load Object Scheme From Scheme/Object File";
            OFD.Filter = "MapCore Scheme/Object Files (*.mcsch, *.mcsch.json, *.mcobj, *.mcobj.json, *.m, *.json) |*.mcsch; *.mcsch.json; *.mcobj; *.mcobj.json; *.m; *.json;|" +
                "MapCore Scheme Files (*.mcsch, *.mcsch.json, *.m, *.json) |*.mcsch; *.mcsch.json; *.m; *.json;|" + 
                "MapCore Scheme Binary Files (*.mcsch, *.m)|*.mcsch;*.m;|" +
                "MapCore Scheme Json Files (*.mcsch.json, *.json)|*.mcsch.json; *.json;|" +
                "MapCore Object Files (*.mcobj, *.mcobj.json, *.m, *.json) |*.mcobj; *.mcobj.json; *.m; *.json;|" +
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

        private void btnClearList_Click(object sender, EventArgs e)
        {
            lstSchemes.ClearSelected();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            btnApply_Click(sender, e);
            GeneralFuncs.CloseParentForm(this);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveParams();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GeneralFuncs.CloseParentForm(this);
        }

        #region Public Properties
        public List<string> lViewportText
        {
            get { return m_lViewportText; }
            set { m_lViewportText = value; }
        }

        public List<IDNMcMapViewport> lViewportTag
        {
            get { return m_lViewportTag; }
            set { m_lViewportTag = value; }
        }

        public List<string> lObjectText
        {
            get { return m_lObjectText; }
            set { m_lObjectText = value; }
        }

        public List<IDNMcObject> lObjectTag
        {
            get { return m_lObjectTag; }
            set { m_lObjectTag = value; }
        }
        #endregion

        private void btnTopMostMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstViewports.SelectedIndex != -1)
                    m_CurrentObject.SetTopMostMode(chkTopMostMode.Checked, lViewportTag[lstViewports.SelectedIndex]);
                else
                    m_CurrentObject.SetTopMostMode(chkTopMostMode.Checked, null);

                // turn on relevant viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetTopMostMode", McEx);
            }
        }

        private void btnMoveIfBlockMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstViewports.SelectedIndex != -1)
                    m_CurrentObject.SetMoveIfBlockedMode(chkMoveIfBlockMode.Checked, lViewportTag[lstViewports.SelectedIndex]);
                else
                    m_CurrentObject.SetMoveIfBlockedMode(chkMoveIfBlockMode.Checked, null);

                // turn on relevant viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetMoveIfBlockedMode", McEx);
            }
        }

        private void btnBlockedTransparencyMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstViewports.SelectedIndex != -1)
                    m_CurrentObject.SetBlockedTransparencyMode(chkBlockedTransparencyMode.Checked, lViewportTag[lstViewports.SelectedIndex]);
                else
                    m_CurrentObject.SetBlockedTransparencyMode(chkBlockedTransparencyMode.Checked, null);

                // turn on relevant viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetBlockedTransparencyMode", McEx);
            }
        }

        private void btnCancelScaleMode_Click(object sender, EventArgs e)
        {
            uint scaleMode = 0;

            scaleMode += (chxCancelScale0.Checked == true) ? MASK0 : 0;
            scaleMode += (chxCancelScale1.Checked == true) ? MASK1 : 0;
            scaleMode += (chxCancelScale2.Checked == true) ? MASK2 : 0;
            scaleMode += (chxCancelScale3.Checked == true) ? MASK3 : 0;
            scaleMode += (chxCancelScale4.Checked == true) ? MASK4 : 0;
            scaleMode += (chxCancelScale5.Checked == true) ? MASK5 : 0;
            scaleMode += (chxCancelScale6.Checked == true) ? MASK6 : 0;
            scaleMode += (chxCancelScale7.Checked == true) ? MASK7 : 0;
            scaleMode += (chxCancelScale8.Checked == true) ? MASK8 : 0;
            scaleMode += (chxCancelScale9.Checked == true) ? MASK9 : 0;

            try
            {
                if (lstViewports.SelectedIndex != -1)
                    m_CurrentObject.SetCancelScaleMode(scaleMode, lViewportTag[lstViewports.SelectedIndex]);
                else
                    m_CurrentObject.SetCancelScaleMode(scaleMode, null);

                // turn on relevant viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetCancelScaleMode", McEx);
            }
        }

        private void btnCollectionMode_Click(object sender, EventArgs e)
        {
            DNECollectionsMode collectionMode = (DNECollectionsMode)Enum.Parse(typeof(DNECollectionsMode), cmbCollectionMode.Text);

            try
            {
                if (lstViewports.SelectedIndex != -1)
                    m_CurrentObject.SetCollectionsMode(collectionMode, lViewportTag[lstViewports.SelectedIndex]);
                else
                    m_CurrentObject.SetCollectionsMode(collectionMode, null);

                // turn on relevant viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetCollectionsMode", McEx);
            }
        }

        private void btnClearViewportSelection_Click(object sender, EventArgs e)
        {
            lstViewports.ClearSelected();
        }

        private void lstViewports_SelectedIndexChanged(object sender, EventArgs e)
        {
            IDNMcMapViewport selectedViewport = null;
            if (lstViewports.SelectedIndex != -1)
            {
                selectedViewport = lViewportTag[lstViewports.SelectedIndex];
            }

            try
            {
                if (selectedViewport == null)
                    chkTopMostMode.Checked = m_CurrentObject.GetTopMostMode();
                else
                    chkTopMostMode.Checked = m_CurrentObject.GetTopMostMode(selectedViewport);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTopMostMode", McEx);
            }

            try
            {
                if (selectedViewport == null)
                chkMoveIfBlockMode.Checked = m_CurrentObject.GetMoveIfBlockedMode();
                else
                chkMoveIfBlockMode.Checked = m_CurrentObject.GetMoveIfBlockedMode(selectedViewport);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetMoveIfBlockedMode", McEx);
            }

            try
            {
                if (selectedViewport == null)
                chkBlockedTransparencyMode.Checked = m_CurrentObject.GetBlockedTransparencyMode();
                else
                chkBlockedTransparencyMode.Checked = m_CurrentObject.GetBlockedTransparencyMode(selectedViewport);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetBlockedTransparencyMode", McEx);
            }

            try
            {
                uint scaleMode;
                if (selectedViewport == null)
                 scaleMode= m_CurrentObject.GetCancelScaleMode();
                else
                    scaleMode = m_CurrentObject.GetCancelScaleMode(selectedViewport);
                chxCancelScale0.Checked = ((scaleMode & MASK0) > 0) ? true : false;
                chxCancelScale1.Checked = ((scaleMode & MASK1) > 0) ? true : false;
                chxCancelScale2.Checked = ((scaleMode & MASK2) > 0) ? true : false;
                chxCancelScale3.Checked = ((scaleMode & MASK3) > 0) ? true : false;
                chxCancelScale4.Checked = ((scaleMode & MASK4) > 0) ? true : false;
                chxCancelScale5.Checked = ((scaleMode & MASK5) > 0) ? true : false;
                chxCancelScale6.Checked = ((scaleMode & MASK6) > 0) ? true : false;
                chxCancelScale7.Checked = ((scaleMode & MASK7) > 0) ? true : false;
                chxCancelScale8.Checked = ((scaleMode & MASK8) > 0) ? true : false;
                chxCancelScale9.Checked = ((scaleMode & MASK9) > 0) ? true : false;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCancelScaleMode", McEx);
            }

            try
            {
                if (selectedViewport == null)
                    cmbCollectionMode.Text = m_CurrentObject.GetCollectionsMode().ToString();
                else
                    cmbCollectionMode.Text = m_CurrentObject.GetCollectionsMode(selectedViewport).ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCollectionsMode", McEx);
            }

            try
            {
                if (selectedViewport == null)
                    ntxScaleFactor.SetFloat(m_CurrentObject.GetScaleFactor());
                else
                    ntxScaleFactor.SetFloat(m_CurrentObject.GetScaleFactor(selectedViewport));
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetScaleFactor", McEx);
            }

            try
            {
                if (selectedViewport == null)
                    ntxEquidistantAttachPointsMinScale.SetFloat(m_CurrentObject.GetEquidistantAttachPointsMinScale());
                else
                    ntxEquidistantAttachPointsMinScale.SetFloat(m_CurrentObject.GetEquidistantAttachPointsMinScale(selectedViewport));
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetEquidistantAttachPointsMinScale", McEx);
            }

            try
            {
                txtScreenTerrainItemsConsistencyScaleSteps.Clear();
                float[] scaleSteps = null;

                if (selectedViewport == null)
                    scaleSteps = m_CurrentObject.GetScreenTerrainItemsConsistencyScaleSteps();
                else
                    scaleSteps = m_CurrentObject.GetScreenTerrainItemsConsistencyScaleSteps(selectedViewport);

                foreach (float step in scaleSteps)
                    txtScreenTerrainItemsConsistencyScaleSteps.Text += step.ToString() + " ";
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetScreenAttachedConsistencyScaleSteps", McEx);
            }
             
            try
            {
                if (selectedViewport == null)
                    ntxEquidistantAttachPointsMinScale.SetFloat(m_CurrentObject.GetEquidistantAttachPointsMinScale());
                else
                    ntxEquidistantAttachPointsMinScale.SetFloat(m_CurrentObject.GetEquidistantAttachPointsMinScale(selectedViewport));
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetEquidistantAttachPointsMinScale", McEx);
            }

            try
            {
                 byte[] states;
                if (selectedViewport == null)
                    states = m_CurrentObject.GetState();
                else
                    states = m_CurrentObject.GetState(selectedViewport);
                string strState = "";
                if (states != null)
                {
                    foreach (byte state in states)
                    {
                        strState += (state.ToString() + " ");
                    }
                }
                tbStates.Text = strState.Trim();

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetState", McEx);
            }
        }

        private void btnLockSchemeList_Click(object sender, EventArgs e)
        {
            frmOMLockList OMLockListForm = new frmOMLockList(m_CurrentObject, "LockSchemeList");
            OMLockListForm.ShowDialog();
            MainForm.RebuildObjectWorldTree(true);
        }

        private void btnLockCSList_Click(object sender, EventArgs e)
        {
            frmOMLockList OMLockListForm = new frmOMLockList(m_CurrentObject, "LockCSList");
            OMLockListForm.ShowDialog();
            MainForm.RebuildObjectWorldTree(true);
        }

        private void lstScreenArrangementViewports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstScreenArrangementViewports.SelectedIndex != -1)
            {
                lstScreenArrangementObjects.Items.Clear();
                lObjectText.Clear();
                lObjectTag.Clear();

                IDNMcOverlay[] overlays = (lViewportTag[lstScreenArrangementViewports.SelectedIndex]).OverlayManager.GetOverlays();
                foreach (IDNMcOverlay overlay in overlays)
                {
                    IDNMcObject [] objects = overlay.GetObjects();
                    foreach (IDNMcObject obj in objects)
                    {
                        string name = Manager_MCNames.GetNameByObject(obj, "Object");
                        lObjectText.Add(name);
                        lObjectTag.Add(obj);
                    }
                }

                lstScreenArrangementObjects.Items.AddRange(lObjectText.ToArray());
            }
        }

        private void btnSetScreenArrangement_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstScreenArrangementViewports.SelectedIndex != -1)
                {
                    IDNMcObject [] ScreenArrangementSelectedObjects =  new IDNMcObject[lstScreenArrangementObjects.SelectedIndices.Count];
                    int selectedIdx = 0;
                    for (int i=0; i<lstScreenArrangementObjects.Items.Count; i++)
                    {
                        if (lstScreenArrangementObjects.GetSelected(i))
                        {
                            ScreenArrangementSelectedObjects[selectedIdx] = lObjectTag[i];
                            selectedIdx++;
                        }
                    }
                    m_CurrentObject.SetScreenArrangement(lViewportTag[lstScreenArrangementViewports.SelectedIndex], ScreenArrangementSelectedObjects);

                    // turn on relevant viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetScreenArrangement", McEx);
            }
        }

        private void btnCancelScreenArrangements_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstScreenArrangementViewports.SelectedIndex != -1)
                {
                    m_CurrentObject.CancelScreenArrangements(lViewportTag[lstScreenArrangementViewports.SelectedIndex]);

                    // turn on relevant viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CancelScreenArrangements", McEx);
            }
        }

        private void lstScreenArrangementObjects_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > lstScreenArrangementObjects.ItemHeight * lstScreenArrangementObjects.Items.Count)
                lstScreenArrangementObjects.ClearSelected();
        }

        private void btnScaleFactorApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstViewports.SelectedIndex != -1)
                    m_CurrentObject.SetScaleFactor(ntxScaleFactor.GetFloat(), lViewportTag[lstViewports.SelectedIndex]);
                else
                    m_CurrentObject.SetScaleFactor(ntxScaleFactor.GetFloat(), null);

                // turn on relevant viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetScaleFactor", McEx);
            }
        }
        private void btnEquidistantAttachPointsMinScale_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstViewports.SelectedIndex != -1)
                    m_CurrentObject.SetEquidistantAttachPointsMinScale(ntxEquidistantAttachPointsMinScale.GetFloat(), lViewportTag[lstViewports.SelectedIndex]);
                else
                    m_CurrentObject.SetEquidistantAttachPointsMinScale(ntxEquidistantAttachPointsMinScale.GetFloat());

                // turn on relevant viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetEquidistantAttachPointsMinScale", McEx);
            }

        }

        private void btnScreenTerrainItemsConsistencyScaleSteps_Click(object sender, EventArgs e)
        {
            try
            {
                float[] scaleSteps = GetScreenTerrainItemsConsistencyScaleSteps().ToArray();

                if (lstViewports.SelectedIndex != -1)
                    m_CurrentObject.SetScreenTerrainItemsConsistencyScaleSteps(scaleSteps, lViewportTag[lstViewports.SelectedIndex]);
                else
                    m_CurrentObject.SetScreenTerrainItemsConsistencyScaleSteps(scaleSteps);

                // turn on relevant viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetScreenTerrainItemsConsistencyScaleSteps", McEx);
            }
        }

        private void btnStateApply_Click(object sender, EventArgs e)
        {
           
            string txtObjectStates = tbStates.Text;
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
                        m_CurrentObject.SetState(abyStates[0], lViewportTag[lstViewports.SelectedIndex]);
                    else
                        m_CurrentObject.SetState(abyStates, lViewportTag[lstViewports.SelectedIndex]);
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(lViewportTag[lstViewports.SelectedIndex]);
                }
                else
                {
                    if (abyStates != null && abyStates.Length == 1)
                        m_CurrentObject.SetState(abyStates[0]);
                    else
                        m_CurrentObject.SetState(abyStates);
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrentObject);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetState", McEx);
            }
        }

        private void tbStates_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(tbStates, "");
        }

        private void tbStates_Validating(object sender, CancelEventArgs e)
        {
            if (tbStates.Text == string.Empty)
            {
                return;
            }

            string txObjectStates = tbStates.Text;
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
                    errorProvider1.SetError(tbStates, "Values must be byte (0..255) separated with blank(s)");
                    return;
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        public IDNMcMapViewport SelectedSizeFactorViewport
        { 
            get
            { 
                IDNMcMapViewport vp = null;
                if (lstSizeFactorViewports.SelectedIndex >= 0)
                    vp = lViewportTag[lstSizeFactorViewports.SelectedIndex];
                return vp;
            }
        }

        private void FillSizeFactorDGV(DataGridView dgv, bool isVectorItem)
        {
            try
            {
                IDNMcMapViewport vp = SelectedSizeFactorViewport;

                Array enumNames = Enum.GetValues(typeof(DNESizePropertyType));
                for (int i = 0; i < enumNames.Length - 1; i++)
                {
                    
                   // dgv[0, i].Value = false;
                    dgv[1, i].Value = enumNames.GetValue(i).ToString();
                    try
                    {
                        dgv[2, i].Value = m_CurrentObject.GetItemSizeFactor((DNESizePropertyType)enumNames.GetValue(i), vp, isVectorItem);
                    }
                    catch (MapCoreException McEx)
                    {
                        if (McEx.ErrorCode != DNEMcErrorCode.INVALID_ARGUMENT)
                            Utilities.ShowErrorMessage("GetItemSizeFactor", McEx);
                        dgv[2, i].Value = "";
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                if(McEx.ErrorCode != DNEMcErrorCode.INVALID_ARGUMENT)
                    Utilities.ShowErrorMessage("GetItemSizeFactor", McEx);
            }
        }

        private void SaveSizeFactorDGVs(DataGridView dgv, bool isVectorItem)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                try
                {
                    object objEnumName = dgv[1, i].Value;
                    if (objEnumName != null)
                    {
                        string enumName = objEnumName.ToString();
                        DNESizePropertyType enumValue = (DNESizePropertyType)Enum.Parse(typeof(DNESizePropertyType), enumName);

                        float factor = 0;
                        object objFactorText = dgv[2, i].Value;
                        if (objFactorText != null && objFactorText.ToString() != "")
                        {
                            factor = float.Parse(objFactorText.ToString());
                            if (factor > 0)
                                m_CurrentObject.SetItemSizeFactors(enumValue, factor, SelectedSizeFactorViewport, isVectorItem);
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    if (McEx.ErrorCode != DNEMcErrorCode.INVALID_ARGUMENT)
                        Utilities.ShowErrorMessage("SetItemSizeFactors", McEx);
                }
            }
        }

        private void lstSizeFactorViewports_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSizeFactor();
        }

        private void SetSelectedSizeFactor(TextBox textBox, DataGridView dgv, bool isVectorItem)
        {
            DNESizePropertyType enumValues = 0;
            // get selected rows from grid
            float factor = 0;
            string textFactor = textBox.Text;
            if (textFactor != "")
            {
                bool bTryParse;
                bTryParse = float.TryParse(textFactor, out factor);
                if (bTryParse == true)
                {
                    try
                    {
                        for (int i = 0; i < dgv.Rows.Count; i++)
                        {
                            object objIsSelected = dgv[0, i].Value;
                            if (objIsSelected != null)
                            {
                                bool isSelected = (bool)((DataGridViewCheckBoxCell)dgv[0, i]).Value;
                                if (isSelected)
                                {
                                    object objEnumName = dgv[1, i].Value;
                                    if (objEnumName != null)
                                    {
                                        string enumName = objEnumName.ToString();
                                        DNESizePropertyType enumValue = (DNESizePropertyType)Enum.Parse(typeof(DNESizePropertyType), enumName);
                                        enumValues |= enumValue;
                                    }
                                }
                            }
                        }
                        if (enumValues != 0)
                        {
                            m_CurrentObject.SetItemSizeFactors(enumValues, factor, SelectedSizeFactorViewport,isVectorItem);
                        }
                        else
                        {
                            MessageBox.Show("No Selected Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    catch (MapCoreException McEx)
                    {
                        if (McEx.ErrorCode != DNEMcErrorCode.INVALID_ARGUMENT)
                            Utilities.ShowErrorMessage("SetItemSizeFactors", McEx);
                    }
                }
                else
                {
                    MessageBox.Show("Factor Value Invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Select();
                }
            }
            else
            {
                MessageBox.Show("Missing Factor Value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Select();
            }

            if (isVectorItem == false)
                FillSizeFactorDGV(dgvObjectsSizeFactor, false);
            else
                FillSizeFactorDGV(dgvVectorItemsSizeFactor, true);
        }

        private void btnSetSelectedSizeFactor_Click(object sender, EventArgs e)
        {
            SetSelectedSizeFactor(tbSizeFactorObjects, dgvObjectsSizeFactor, false);
        }

        private void btnSetSelectedSizeFactorVectorItems_Click(object sender, EventArgs e)
        {
            SetSelectedSizeFactor(tbSizeFactorVectorItems, dgvVectorItemsSizeFactor, true);
        }

        private void btnRemoveSelectionObjects_Click(object sender, EventArgs e)
        {
            ChangeSelection(dgvObjectsSizeFactor,false);
        }

        private void btnRemoveSelectionVectorItems_Click(object sender, EventArgs e)
        {
            ChangeSelection(dgvVectorItemsSizeFactor,false);
        }

        private void ChangeSelection(DataGridView dgv ,bool bSelectValue)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv[0, i].Value = bSelectValue;
            }
        }

        private void btnSizeFactorVectorItemsSelectAll_Click(object sender, EventArgs e)
        {
            ChangeSelection(dgvVectorItemsSizeFactor, true);
        }

        private void btnSizeFactorObjectsSelectAll_Click(object sender, EventArgs e)
        {
            ChangeSelection(dgvObjectsSizeFactor, true);
        }

        private void btnClearViewportList_Click(object sender, EventArgs e)
        {
            lstSizeFactorViewports.ClearSelected();
        }

        private void btnSizeFactorObjectsApply_Click(object sender, EventArgs e)
        {
            SaveSizeFactorDGVs(dgvObjectsSizeFactor, false);
            FillSizeFactorDGV(dgvObjectsSizeFactor, false);
        }

        private void btnSizeFactorVectorItemsApply_Click(object sender, EventArgs e)
        {
            SaveSizeFactorDGVs(dgvVectorItemsSizeFactor, true);
            FillSizeFactorDGV(dgvVectorItemsSizeFactor, true);
        }

        private void cmbStorageFormat_TextChanged(object sender, EventArgs e)
        {
            MCTMapForm.m_eOMStorageFormat = GetStorageFormatByUser();
        }

        private void btnGetObjectSchemeByID_Click(object sender, EventArgs e)
        {
            bool isShowError = false;
            string strNodeId = ntxGetObjectSchemeById.Text;
            if (strNodeId != "")
            {
                uint nodeId;
                if (UInt32.TryParse(strNodeId, out nodeId))
                {
                    try
                    {
                        IDNMcObjectScheme mcObjectScheme = m_CurrentObject.GetObjectSchemeByID(nodeId);
                        if (mcObjectScheme != null)
                        {
                            lnkObjectScheme.Text = Manager_MCNames.GetNameByObject(mcObjectScheme);
                            lnkObjectScheme.Tag = mcObjectScheme;
                        }
                        else
                            MessageBox.Show("IDNMcOverlayManager.GetObjectSchemeByID", "Not exists object scheme with id " + nodeId, MessageBoxButtons.OK);

                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetObjectSchemeByID", McEx);
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

        private void btnGetObjectSchemeByName_Click(object sender, EventArgs e)
        {
            if (ntxGetObjectSchemeByName.Text != "")
            {
                try
                {
                    IDNMcObjectScheme mcObjectSchemeNode = m_CurrentObject.GetObjectSchemeByName(ntxGetObjectSchemeByName.Text);
                    if (mcObjectSchemeNode != null)
                    {
                        lnkObjectScheme.Text = Manager_MCNames.GetNameByObject(mcObjectSchemeNode);
                        lnkObjectScheme.Tag = mcObjectSchemeNode;
                    }
                    else
                        MessageBox.Show("Not exists node with name " + ntxGetObjectSchemeByName.Text, "IDNMcOverlayManager.GetObjectSchemeByName", MessageBoxButtons.OK);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetObjectSchemeByName", McEx);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please insert node name", "Invalid Node Name", MessageBoxButtons.OK);
            }

        }

        private void btnGetCSByID_Click(object sender, EventArgs e)
        {
            bool isShowError = false;
            string strNodeId = ntxGetConditionalSelectorById.Text;
            if (strNodeId != "")
            {
                uint nodeId;
                if (UInt32.TryParse(strNodeId, out nodeId))
                {
                    try
                    {
                        IDNMcConditionalSelector mcConditionalSelector = m_CurrentObject.GetConditionalSelectorByID(nodeId);
                        if (mcConditionalSelector != null)
                        {
                            lnkConditionalSelector.Text = Manager_MCNames.GetNameByObject(mcConditionalSelector);
                            lnkConditionalSelector.Tag = mcConditionalSelector;
                        }
                        else
                            MessageBox.Show("IDNMcOverlayManager.GetConditionalSelectorByID", "Not exists conditional selector with id " + nodeId, MessageBoxButtons.OK);

                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetConditionalSelectorByID", McEx);
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

        private void btnGetCSByName_Click(object sender, EventArgs e)
        {
            if (ntxGetConditionalSelectorByName.Text != "")
            {
                try
                {
                    IDNMcConditionalSelector mcConditionalSelectorNode = m_CurrentObject.GetConditionalSelectorByName(ntxGetConditionalSelectorByName.Text);
                    if (mcConditionalSelectorNode != null)
                    {
                        lnkConditionalSelector.Text = Manager_MCNames.GetNameByObject(mcConditionalSelectorNode);
                        lnkConditionalSelector.Tag = mcConditionalSelectorNode;
                    }
                    else
                        MessageBox.Show("Not exists node with name " + ntxGetConditionalSelectorByName.Text, "IDNMcOverlayManager.GetConditionalSelectorByName", MessageBoxButtons.OK);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetConditionalSelectorByName", McEx);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please insert node name", "Invalid Node Name", MessageBoxButtons.OK);
            }
        }

        private void lnkConditionalSelector_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClickOnLink(lnkConditionalSelector.Tag);
        }

        private void lnkObjectScheme_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClickOnLink(lnkObjectScheme.Tag);
        }

        private void ClickOnLink(object nodeTag)
        {
            if (nodeTag != null)
            {
                Control control = Parent.Parent.Parent;
                MCObjectWorldTreeViewForm mcOverlayMangerTreeView = control as MCObjectWorldTreeViewForm;
                if (mcOverlayMangerTreeView != null)
                {
                    mcOverlayMangerTreeView.SelectNodeInTreeNode((uint)nodeTag.GetHashCode());
                }
            }
        }

        private void SetGridData(DataGridView dgvLocationPoints, DNSMcVector3D[] locationPointsArray)
        {
            dgvLocationPoints.Rows.Clear();

            if (locationPointsArray != null)
            {
                for (int i = 0; i < locationPointsArray.Length; i++)
                {
                    dgvLocationPoints.Rows.Add();
                    dgvLocationPoints[0, i].Value = locationPointsArray[i].x;
                    dgvLocationPoints[1, i].Value = locationPointsArray[i].y;
                    dgvLocationPoints[2, i].Value = locationPointsArray[i].z;
                }
            }
        }

        private DNSMcVector3D[] GetGridData(DataGridView dgvLocationPoints)
        {
            DNSMcVector3D[] NewLocationPoints;

            if (dgvLocationPoints.RowCount > 1)
            {
                NewLocationPoints = new DNSMcVector3D[dgvLocationPoints.RowCount - 1];
            }
            else
            {
                if (dgvLocationPoints.Rows[0].IsNewRow)
                    NewLocationPoints = new DNSMcVector3D[0];
                else
                    NewLocationPoints = new DNSMcVector3D[1];
            }

            for (int i = 0; i < dgvLocationPoints.RowCount; i++)
            {
                bool isParseSucc;
                double result = 0;
                if (!dgvLocationPoints.Rows[i].IsNewRow)
                {
                    if (dgvLocationPoints[0, i].Value != null)
                    {
                        isParseSucc = double.TryParse(dgvLocationPoints[0, i].Value.ToString(), out result);
                        NewLocationPoints[i].x = (isParseSucc == true) ? result : 0;
                    }
                    else
                        NewLocationPoints[i].x = 0;

                    if (dgvLocationPoints[1, i].Value != null)
                    {
                        isParseSucc = double.TryParse(dgvLocationPoints[1, i].Value.ToString(), out result);
                        NewLocationPoints[i].y = (isParseSucc == true) ? result : 0;
                    }
                    else
                        NewLocationPoints[i].y = 0;

                    if (dgvLocationPoints[2, i].Value != null)
                    {
                        isParseSucc = double.TryParse(dgvLocationPoints[2, i].Value.ToString(), out result);
                        NewLocationPoints[i].z = (isParseSucc == true) ? result : 0;
                    }
                    else
                        NewLocationPoints[i].z = 0;
                }
            }
            return NewLocationPoints;
        }

        private void btnConvertWorldToImage_Click(object sender, EventArgs e)
        {
            txtIntersectionStatus.Text = "";
            try
            {
                DNMcNullableOut<DNEMcErrorCode> peIntersectionStatus = new DNMcNullableOut<DNEMcErrorCode>();
                
                ctrlImageCalcPoint.SetVector3D( m_CurrentObject.ConvertWorldToImage(ctrlOverlayManagerPoint.GetVector3D(), 
                    ctrlImageCalc.ImageCalc,
                    chxRequestSeparateIntersectionStatus.Checked ? peIntersectionStatus : null));

                if (chxRequestSeparateIntersectionStatus.Checked && peIntersectionStatus.Value != DNEMcErrorCode.SUCCESS)
                    txtIntersectionStatus.Text = IDNMcErrors.ErrorCodeToString(peIntersectionStatus.Value);
                // MessageBox.Show(IDNMcErrors.ErrorCodeToString(peIntersectionStatus), "ConvertWorldToImage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ConvertWorldToImage", McEx);
            }
        }

        private void btnConvertImageToWorld_Click(object sender, EventArgs e)
        {
            txtIntersectionStatus.Text = "";
            try
            {
                if (chxConvertImageToWorldAsync.Checked)
                    MCTMapClick.ConvertImageToWorld(m_CurrentObject, ctrlImageCalcPoint.GetVector3D(), ctrlImageCalc.ImageCalc, this, chxRequestSeparateIntersectionStatus.Checked);
                else
                {
                    DNMcNullableOut<DNEMcErrorCode> peIntersectionStatus = new DNMcNullableOut<DNEMcErrorCode>();
                    ctrlOverlayManagerPoint.SetVector3D( m_CurrentObject.ConvertImageToWorld(ctrlImageCalcPoint.GetVector3D(), 
                        ctrlImageCalc.ImageCalc, 
                        chxRequestSeparateIntersectionStatus.Checked ? peIntersectionStatus : null));

                    if (chxRequestSeparateIntersectionStatus.Checked && peIntersectionStatus.Value != DNEMcErrorCode.SUCCESS)
                        txtIntersectionStatus.Text = IDNMcErrors.ErrorCodeToString(peIntersectionStatus.Value);
                        //MessageBox.Show(IDNMcErrors.ErrorCodeToString(peIntersectionStatus), "ConvertImageToWorld", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ConvertWorldToImage", McEx);
            }
        }

        private DNESymbologyStandard GetSymbologyStandard()
        {
            return (DNESymbologyStandard)Enum.Parse(typeof(DNESymbologyStandard), cmbSymbologyStandard.Text);
        }

        private void btnSetSymbology_Click(object sender, EventArgs e)
        {
            try
            {
                DNESymbologyStandard symbologyStandard = GetSymbologyStandard();
                m_CurrentObject.InitializeSymbologyStandardSupport(symbologyStandard, cbShowGeoInMetricProportionOM.Checked, cbIgnoreNonExistentAmplifiers.Checked);

                Manager_MCTSymbology.SetSymbologyStandardFlags(m_CurrentObject, symbologyStandard, cbShowGeoInMetricProportionOM.Checked, cbIgnoreNonExistentAmplifiers.Checked);
                Manager_MCTSymbology.SetSymbologyStandard(m_CurrentObject, symbologyStandard);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("InitializeSymbologyStandardSupport", McEx);
                return;
            }
        }

        private void cmbSymbologyStandard_SelectedIndexChanged(object sender, EventArgs e)
        {
            DNESymbologyStandard symbologyStandard = GetSymbologyStandard();
            GetSymbologyStandardFlags(symbologyStandard);
        }

        public void OnMapClick(DNSMcVector3D screenLocation, DNSMcVector3D location, DNEMcPointCoordSystem locationCoordSystem, bool isRelativeToDTM)
        {
            ctrlOverlayManagerPoint.SetVector3D( location);
        }

        public void OnMapClickError(DNEMcErrorCode eErrorCode, string functionName)
        {
            MessageBox.Show(IDNMcErrors.ErrorCodeToString(eErrorCode), "Error Convert Points", MessageBoxButtons.OK);
        }

        private void lstSchemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            chxSaveFilePath.Enabled = lstSchemes.SelectedIndices.Count == 1;
        }
    }

    public class PrivatePropertyTable
    {
        private DNSPropertyNameIDType [] m_ArrPropertyId;
        private string [] m_ArrPropertyName;

        public PrivatePropertyTable(int tableSize)
        {
            m_ArrPropertyId = new DNSPropertyNameIDType [tableSize];
            m_ArrPropertyName = new string[tableSize];
        }

        #region  Public Property
        public DNSPropertyNameIDType[] ArrPropertyId
        {
            get { return m_ArrPropertyId; }
            set { m_ArrPropertyId = value; }
        }

        public string [] ArrPropertyName
        {
            get { return m_ArrPropertyName; }
            set { m_ArrPropertyName = value; }
        }
        #endregion

    }
}
