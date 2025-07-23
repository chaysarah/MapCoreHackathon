using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.MapWorld.Assist_Forms;
using MCTester.General_Forms;
using MCTester.Managers;
using MapCore.Common;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers.MapWorld;
using MCTester.GUI.Trees;
using MCTester.Controls;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucVectorLayer : ucLayer, IUserControlItem
    {
        private IDNMcVectorMapLayer m_CurrentObject;
        private List<IDNMcOverlayManager> m_lOverlayManager = new List<IDNMcOverlayManager>();

        private List<string> m_lstViewportText = new List<string>();

        private List<IDNMcMapViewport> m_lstViewportValue = new List<IDNMcMapViewport>();

        private List<string> m_lstGeneralViewportText = new List<string>();
        private List<IDNMcMapViewport> m_lstGeneralViewportValue = new List<IDNMcMapViewport>();

        private MCTAsyncOperationCallback mctAsyncOperationCallback = null;

        public List<string> LstViewportText
        {
            get { return m_lstViewportText; }
            set { m_lstViewportText = value; }
        }

        public List<IDNMcMapViewport> LstViewportValue
        {
            get { return m_lstViewportValue; }
            set { m_lstViewportValue = value; }
        }

        public ucVectorLayer()
        {
            InitializeComponent();
        }

        protected override void SaveItem()
        {
            base.SaveItem();

            try
            {
                m_CurrentObject.SetCollisionPrevention(chxCollisionPrevention.Checked);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetCollisionPrevention", McEx);
            }

        }

        #region IUserControlItem Members

       /* Dictionary<IDNMcMapViewport, IDNMcMapLayer> mapViewports = new Dictionary<IDNMcMapViewport, IDNMcMapLayer>();
        List<IDNMcOverlayManager> overlayManagers = new List<IDNMcOverlayManager>();*/

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcVectorMapLayer)aItem;
            base.LoadItem(aItem);
            bool isLayerInAnyVp = false;

            foreach (IDNMcMapViewport vp in Manager_MCViewports.AllParams.Keys)
            {
                IDNMcMapTerrain[] terrains = vp.GetTerrains();
                foreach(IDNMcMapTerrain terrain in terrains)
                {
                    IDNMcMapLayer[] layers = terrain.GetLayers();
                    List<IDNMcMapLayer> lstLayers = new List<IDNMcMapLayer>(layers);
                    if(lstLayers.Contains(m_CurrentObject))
                    {
                        isLayerInAnyVp = true;
                        break;
                      /*  mapViewports.Add(vp, m_CurrentObject);
                        if (vp.OverlayManager != null && !overlayManagers.Contains(vp.OverlayManager))
                            overlayManagers.Add(vp.OverlayManager);*/
                    }
                    if (isLayerInAnyVp)
                        break;
                }
            }

            pnlQueryPaint.Enabled = gbVectorItemsPaint.Enabled = isLayerInAnyVp /*mapViewports.Count > 0*/;

            ctrlVectorItemID1.SetVectorMapLayer(m_CurrentObject);
            ctrlVectorItemID2.SetVectorMapLayer(m_CurrentObject);

            rbGetValidFieldsByVectorItemID.Checked = true;

            mctAsyncOperationCallback = MCTAsyncOperationCallback.GetInstance();

            mctAsyncOperationCallback.vectorLayerForm = this;
            bool isExistOverlay = Manager_MCOverlay.GetOverlayOfLayer(m_CurrentObject.GetHashCode()) == null;
            CheckIsExistOverlayVectorItem();

            btnRemoveLastPaintedQuery.Enabled = btnRemove.Enabled = !isExistOverlay;
            btnRemoveAllPaintedQuery.Enabled = btnRemoveFromAllLayer.Enabled = !isExistOverlay;

            Dictionary<object, uint> viewports = Manager_MCViewports.AllParams;
            try
            {
                foreach (IDNMcMapViewport currViewport in viewports.Keys)
                {
                    IDNMcMapTerrain[] terrains = currViewport.GetTerrains();
                    foreach (IDNMcMapTerrain currTerrain in terrains)
                    {
                        IDNMcMapLayer[] layersInViewport = currTerrain.GetLayers();
                        foreach (IDNMcMapLayer layer in layersInViewport)
                        {
                            if (layer == m_CurrentObject)
                            {
                                m_lstViewportText.Add(Manager_MCNames.GetNameByObject(currViewport, "Viewport"));
                                m_lstViewportValue.Add(currViewport);
                            }
                        }
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTerrains/GetLayers", McEx);
            }

            lbOverlayStateViewport.Items.AddRange(m_lstViewportText.ToArray());

            // fill AddObstacleObjects viewport list
            foreach (IDNMcMapViewport vp in viewports.Keys)
            {
                m_lstGeneralViewportText.Add(Manager_MCNames.GetNameByObject(vp, "Viewport"));
                m_lstGeneralViewportValue.Add(vp);
            }

            // create a unique overlay manager list
            foreach (IDNMcMapViewport vp in viewports.Keys)
            {
                try
                {
                    if (!m_lOverlayManager.Contains(vp.OverlayManager))
                        m_lOverlayManager.Add(vp.OverlayManager);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("OverlayManager", McEx);
                }
            }

            try
            {
                bool enabled;
                short priority;
                m_CurrentObject.GetOverlayDrawPriority(out enabled, out priority);

                chxOverlayDrawPriorityEnable.Checked = enabled;
                ntxOverlayDrawPriority.SetShort(priority);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetOverlayDrawPriority", McEx);
            }

            try
            {
                chxDrawPriorityConsistency.Checked = m_CurrentObject.GetDrawPriorityConsistency();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetDrawPriorityConsistency", McEx);
            }

            try
            {
                ntxToleranceForPoint.SetInt(m_CurrentObject.GetToleranceForPoint());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ToleranceForPoint", McEx);
            }
            try
            {
                chxCollisionPrevention.Checked = m_CurrentObject.GetCollisionPrevention();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetCollisionPrevention", McEx);
            }

            try
            {
                chxIsLiteVectorLayer.Checked = m_CurrentObject.IsLiteVectorLayer();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Is Lite Vector Layer", McEx);
            }

            try
            {
                float fMin, fMax;
                m_CurrentObject.GetMinMaxSizeFactor(out fMin, out fMax);
                tbMinSizeFactor.SetFloat(fMin);
                tbMaxSizeFactor.SetFloat(fMax);

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Get Size Factor", McEx);
            }

            lblNoReturn.Text = "";
        }

        #endregion

        private void btnGetNumFields_Click(object sender, EventArgs e)
        {
            try
            {
                ntxGetNumFields.SetUInt32(m_CurrentObject.GetNumFields());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetNumFields", McEx);
            }
        }

        private void btnGetFieldsData_Click(object sender, EventArgs e)
        {
            string strName;
            DNEFieldType fieldType;

            try
            {
                if (CheckNegativeValue(ntxFieldId, "Field Id"))
                    return;
                m_CurrentObject.GetFieldData(ntxFieldId.GetUInt32(),
                                                out strName,
                                                out fieldType);

                if (strName != "")
                    txtStrName.Text = strName;

                txtFieldType.Text = fieldType.ToString();

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetFieldData", McEx);
            }
        }

        private bool CheckNegativeValue(NumericTextBox numericTextBox, string fieldName)
        {
            int value = numericTextBox.GetInt32();
            if (value < 0)
            {
                MessageBox.Show(fieldName + " cannot be negative", "Invalid value");
                numericTextBox.Focus();
                return true;
            }
            return false;
        }

        private void btnGetGeometryType_Click(object sender, EventArgs e)
        {
            try
            {
                txtGeometryType.Text = m_CurrentObject.GetGeometryType().ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetGeometryType", McEx);
            }
        }

        private void btnGetVectorItemsCount_Click(object sender, EventArgs e)
        {
            try
            {
                txtVectorItemsCount.Text = m_CurrentObject.GetVectorItemsCount().ToString();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVectorItemsCount", McEx);
            }
        }

        private void btnGetVectorItemFieldValuesAsInt_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckNegativeValue(ntxVectorItemFieldId, "Field Id"))
                    return;
                if (ctrlVectorItemID1.CheckNegativeValue())
                    return;

                if (chxGetVectorAsync.Checked)
                {
                    m_CurrentObject.GetVectorItemFieldValueAsInt(ctrlVectorItemID1.GetVectorItemID(),
                             ntxVectorItemFieldId.GetUInt32(),
                             mctAsyncOperationCallback);
                }
                else
                {
                    int res = m_CurrentObject.GetVectorItemFieldValueAsInt(ctrlVectorItemID1.GetVectorItemID(),
                            ntxVectorItemFieldId.GetUInt32());
                    GetVectorItemFieldValuesAsInt(res);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVectorItemFieldValueAsInt", McEx);
            }
            catch (InvalidCastException McEx)
            {
                MessageBox.Show(McEx.Message, "Error");
            }
        }

        public void GetVectorItemFieldValuesAsInt(int value)
        {
            ntxVectorItemValue.SetInt(value);
        }

        public void GetVectorItemFieldValuesAsDouble(double value)
        {
            ntxVectorItemValue.SetDouble(value);
        }

        public void GetVectorItemFieldValuesAsString(string value)
        {
            if (value != null)
                ntxVectorItemValue.SetString(value);
        }

        private void btnGetVectorItemFieldValuesAsDouble_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckNegativeValue(ntxVectorItemFieldId, "Field Id"))
                    return;

                if (ctrlVectorItemID1.CheckNegativeValue())
                    return;

                if (chxGetVectorAsync.Checked)
                {
                    m_CurrentObject.GetVectorItemFieldValueAsDouble(ctrlVectorItemID1.GetVectorItemID(),
                                                                              ntxVectorItemFieldId.GetUInt32(),
                                                                              mctAsyncOperationCallback);
                }
                else
                {
                    double res = m_CurrentObject.GetVectorItemFieldValueAsDouble(ctrlVectorItemID1.GetVectorItemID(),
                                                                           ntxVectorItemFieldId.GetUInt32());
                    GetVectorItemFieldValuesAsDouble(res);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVectorItemFieldValueAsDouble", McEx);
            }
            catch (InvalidCastException McEx)
            {
                MessageBox.Show(McEx.Message, "Error");
            }
        }

        private void btnGetVectorItemFieldValuesAsString_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckNegativeValue(ntxVectorItemFieldId, "Field Id"))
                    return;
                if (ctrlVectorItemID1.CheckNegativeValue())
                    return;

                if (chxGetVectorAsync.Checked)
                {
                    m_CurrentObject.GetVectorItemFieldValueAsString(ctrlVectorItemID1.GetVectorItemID(),
                                                                                ntxVectorItemFieldId.GetUInt32(),
                                                                                mctAsyncOperationCallback);
                }
                else
                {
                    string res = m_CurrentObject.GetVectorItemFieldValueAsString(ctrlVectorItemID1.GetVectorItemID(),
                                                                                ntxVectorItemFieldId.GetUInt32());
                    GetVectorItemFieldValuesAsString(res);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVectorItemFieldValueAsString", McEx);
            }
            catch (InvalidCastException McEx)
            {
                MessageBox.Show(McEx.Message, "Error");
            }
        }

        private void btnGetVectorItemFieldValuesAsWString_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckNegativeValue(ntxVectorItemFieldId, "Field Id"))
                    return;
                if (ctrlVectorItemID1.CheckNegativeValue())
                    return;

                if (chxGetVectorAsync.Checked)
                {
                    m_CurrentObject.GetVectorItemFieldValueAsWString(ctrlVectorItemID1.GetVectorItemID(),
                                                                                ntxVectorItemFieldId.GetUInt32(),
                                                                                mctAsyncOperationCallback);
                }
                else
                {
                    string res = m_CurrentObject.GetVectorItemFieldValueAsWString(ctrlVectorItemID1.GetVectorItemID(),
                                                                            ntxVectorItemFieldId.GetUInt32());
                    GetVectorItemFieldValuesAsString(res);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetVectorItemFieldValueAsWString", McEx);
            }
            catch (InvalidCastException McEx)
            {
                MessageBox.Show(McEx.Message, "Error");
            }
        }

        public void GetFieldUniqueValuesAsInt(int[] uniqueValues)
        {
            if (uniqueValues != null)
            {
                foreach (int val in uniqueValues)
                    lstFieldUniqueValues.Items.Add(val);
            }
        }

        public void GetFieldUniqueValuesAsDouble(double[] uniqueValues)
        {
            if (uniqueValues != null)
            {
                foreach (double val in uniqueValues)
                    lstFieldUniqueValues.Items.Add(val);
            }
        }

        public void GetFieldUniqueValuesAsString(string[] uniqueValues)
        {
            if (uniqueValues != null)
            {
                foreach (string val in uniqueValues)
                    lstFieldUniqueValues.Items.Add(val);
            }
        }


        private void btnGetFieldUniqueValuesAsInt_Click(object sender, EventArgs e)
        {
            lstFieldUniqueValues.Items.Clear();
            int[] uniqueValues;

            try
            {
                if (CheckNegativeValue(ntxFieldUniqueValuesId, "Field Id"))
                    return;
                if (chxGetUniqueAsync.Checked)
                {
                    m_CurrentObject.GetFieldUniqueValuesAsInt(ntxFieldUniqueValuesId.GetUInt32(), mctAsyncOperationCallback);
                }
                else
                {
                    uniqueValues = m_CurrentObject.GetFieldUniqueValuesAsInt(ntxFieldUniqueValuesId.GetUInt32());
                    GetFieldUniqueValuesAsInt(uniqueValues);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetFieldUniqueValuesAsInt", McEx);
            }
        }


        private void btnGetFieldUniqueValuesAsDouble_Click(object sender, EventArgs e)
        {
            lstFieldUniqueValues.Items.Clear();
            double[] uniqueValues;

            try
            {
                if (CheckNegativeValue(ntxFieldUniqueValuesId, "Field Id"))
                    return;
                if (chxGetUniqueAsync.Checked)
                {
                    m_CurrentObject.GetFieldUniqueValuesAsDouble(ntxFieldUniqueValuesId.GetUInt32(), mctAsyncOperationCallback);
                }
                else
                {
                    uniqueValues = m_CurrentObject.GetFieldUniqueValuesAsDouble(ntxFieldUniqueValuesId.GetUInt32());
                    GetFieldUniqueValuesAsDouble(uniqueValues);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetFieldUniqueValuesAsDouble", McEx);
            }
        }

        private void btnGetFieldUniqueValuesAsString_Click(object sender, EventArgs e)
        {
            lstFieldUniqueValues.Items.Clear();
            string[] uniqueValues;

            try
            {
                if (CheckNegativeValue(ntxFieldUniqueValuesId, "Field Id"))
                    return;
                if (chxGetUniqueAsync.Checked)
                {
                    m_CurrentObject.GetFieldUniqueValuesAsString(ntxFieldUniqueValuesId.GetUInt32(), mctAsyncOperationCallback);
                }
                else
                {
                    uniqueValues = m_CurrentObject.GetFieldUniqueValuesAsString(ntxFieldUniqueValuesId.GetUInt32());

                    GetFieldUniqueValuesAsString(uniqueValues);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetFieldUniqueValuesAsString", McEx);
            }
        }

        private void btnGetFieldUniqueValuesAsWString_Click(object sender, EventArgs e)
        {
            lstFieldUniqueValues.Items.Clear();
            string[] uniqueValues;

            try
            {
                if (CheckNegativeValue(ntxFieldUniqueValuesId, "Field Id"))
                    return;
                if (chxGetUniqueAsync.Checked)
                {
                    (m_CurrentObject).GetFieldUniqueValuesAsWString(ntxFieldUniqueValuesId.GetUInt32(), mctAsyncOperationCallback);
                }
                else
                {
                    uniqueValues = (m_CurrentObject).GetFieldUniqueValuesAsWString(ntxFieldUniqueValuesId.GetUInt32());
                    GetFieldUniqueValuesAsString(uniqueValues);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetFieldUniqueValuesAsWString", McEx);
            }
        }

        private void btnClearGetFieldUniqueValueList_Click(object sender, EventArgs e)
        {
            lstFieldUniqueValues.Items.Clear();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            lstVectorItemsId.Items.Clear();
            try
            {
                if (chxQueryAsync.Checked)
                {
                    m_CurrentObject.Query(txtAttributeFilter.Text, mctAsyncOperationCallback);
                }
                else
                {
                    UInt64[] vectorItemsId = m_CurrentObject.Query(txtAttributeFilter.Text);
                    GetQuery(vectorItemsId);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Query", McEx);
            }
        }

        public void GetQuery(UInt64[] vectorItemsId)
        {
            if (vectorItemsId != null)
            {
                foreach (UInt64 vectorItem in vectorItemsId)
                    lstVectorItemsId.Items.Add(vectorItem);
            }
        }

        private void btnOverlayDrawPriority_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetOverlayDrawPriority(chxOverlayDrawPriorityEnable.Checked, ntxOverlayDrawPriority.GetShort());

                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetOverlayDrawPriority", McEx);
            }
        }

        private void btnDrawPriorityConsistency_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetDrawPriorityConsistency(chxDrawPriorityConsistency.Checked);

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetDrawPriorityConsistency", McEx);
            }
        }

        private void btnToleranceForPoint_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentObject.SetToleranceForPoint(ntxToleranceForPoint.GetInt32());

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetToleranceForPoint", McEx);
            }
        }

        private void btnOverlayStateSet_Click(object sender, EventArgs e)
        {
            bool isSeccus = false;
            byte[] abyStates = ConvertStringToByteArray(tbStates.Text, out isSeccus);
            if (isSeccus)
            {
                try
                {
                    int index = lbOverlayStateViewport.SelectedIndex;
                    if (index >= 0)
                    {
                        if (abyStates != null && abyStates.Length == 1)
                            m_CurrentObject.SetOverlayState(abyStates[0], SelectedViewport);
                        else
                            m_CurrentObject.SetOverlayState(abyStates, SelectedViewport);
                        Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(SelectedViewport);
                    }
                    else
                    {
                        if (abyStates != null && abyStates.Length == 1)
                            m_CurrentObject.SetOverlayState(abyStates[0]);
                        else
                            m_CurrentObject.SetOverlayState(abyStates);
                        Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetState", McEx);
                }
            }
        }

        public IDNMcMapViewport SelectedViewport
        {
            get
            {
                IDNMcMapViewport vp = null;
                int index = lbOverlayStateViewport.SelectedIndex;
                if (index >= 0)
                    vp = LstViewportValue[index];
                return vp;
            }
        }

        public float Brightness
        {
            get
            {
                return tbBrightness.GetFloat();
            }
            set
            {
                tbBrightness.SetFloat(value);
            }
        }

        private void GetOverlayState()
        {
            try
            {
                int index = lbOverlayStateViewport.SelectedIndex;
                if (index >= 0)
                {
                    tbStates.Text = ConvertByteArrayToString(m_CurrentObject.GetOverlayState(SelectedViewport), " ");
                }
                else
                    tbStates.Text = ConvertByteArrayToString(m_CurrentObject.GetOverlayState(), " ");
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetOverlayState", McEx);
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

        private byte[] ConvertStringToByteArray(string input, out bool isSeccus)
        {

            string txtTemp = input;
            byte[] output = null;
            try
            {
                if (txtTemp != null && txtTemp.Trim() != string.Empty)
                {
                    string txRepObjectStates = txtTemp;
                    while (((txRepObjectStates = txtTemp.Trim().Replace("  ", " "))) != txtTemp)
                    {
                        txtTemp = txRepObjectStates;
                    }
                    string[] arrInput = txtTemp.Split(" ".ToCharArray());
                    output = new byte[arrInput.Length];
                    for (int i = 0; i < arrInput.Length; i++)
                    {
                        output[i] = byte.Parse(arrInput[i]);
                    }
                    isSeccus = true;
                    return output;
                }
            }
            catch (FormatException)
            {
                ConvertStringToByteArrayMsg();
            }
            catch (ArgumentNullException)
            {
                ConvertStringToByteArrayMsg();
            }
            catch (OverflowException)
            {
                ConvertStringToByteArrayMsg();
            }
            isSeccus = false;
            return null;
        }

        private void ConvertStringToByteArrayMsg(string msg = "")
        {
            MessageBox.Show("State value should be separated by space ' '", "Convert state string to byte array", MessageBoxButtons.OK);
        }

        private void lbOverlayStateViewport_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*GetOverlayState();
            GetBrightness();*/
        }

        private void lbOverlayStateViewport_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > lbOverlayStateViewport.ItemHeight * lbOverlayStateViewport.Items.Count)
                lbOverlayStateViewport.ClearSelected();
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

        private void btnBrightness_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedViewport != null)
                    m_CurrentObject.SetBrightness(Brightness, SelectedViewport);
                else
                    m_CurrentObject.SetBrightness(Brightness);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetBrightness", McEx);
            }
        }

        private void GetBrightness()
        {
            try
            {
                if (SelectedViewport != null)
                    Brightness = m_CurrentObject.GetBrightness(SelectedViewport);
                else
                    Brightness = m_CurrentObject.GetBrightness();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetBrightness", McEx);
            }
        }

        private void btnGetPoints_Click(object sender, EventArgs e)
        {
           
            if (!ctrlVectorItemID2.CheckEmpty())
            {
                try
                {
                    if (ctrlVectorItemID2.CheckNegativeValue())
                        return;

                    GetVectorItemPoints(chxAsyncGetPoints.Checked, ctrlVectorItemID2.GetVectorItemID());
                   
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("IDNMcVectorMapLayer.GetVectorItemPoints", McEx);
                    return;
                }
                catch (InvalidCastException McEx)
                {
                    MessageBox.Show(McEx.Message, "Error");
                }
            }
        }

        private void GetVectorItemPoints(bool isAsync, ulong nVectorItemId)
        {
            DNSMcVector3D[][] points;
            if (isAsync)
            {
                m_CurrentObject.GetVectorItemPoints(nVectorItemId, mctAsyncOperationCallback);
            }
            else
            {
                points = m_CurrentObject.GetVectorItemPoints(nVectorItemId);
                GetVectorPoints(points, m_CurrentObject);
            }
        }

        public void GetVectorPoints(DNSMcVector3D[][] points, IDNMcVectorMapLayer mcVectorLayer)
        {
            IDNMcObject obj;
            IDNMcObject firstObject = null;
            IDNMcOverlay m_overlay;
            DNSMcVector3D[] itemFound;
            byte colorDelta;
            IDNMcSymbolicItem unifiedItem;
            IDNMcMapViewport mcMapViewport = null;
            int layerId = mcVectorLayer.GetHashCode();
            m_overlay = Manager_MCOverlay.GetOverlayOfLayer(layerId);
            IDNMcOverlayManager mcOverlayManager = null;
            if (MCMapWorldTreeViewForm.CurrentViewportOfSelectedLayer != null)
            {
                mcMapViewport = MCMapWorldTreeViewForm.CurrentViewportOfSelectedLayer;
                mcOverlayManager = mcMapViewport.OverlayManager;
            }
            if (mcOverlayManager == null)
            {
                MessageBox.Show("Missing overlay manager");
                return;
            }
            if (m_overlay == null)
            {
                try
                {
                    m_overlay = DNMcOverlay.Create(mcOverlayManager);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNMcOverlay.Create", McEx);
                    return;
                }
                Manager_MCOverlay.AddOverlayToVectoryItemOverlays(layerId, m_overlay);
            }

            colorDelta = 0;
            lblNoReturn.Text = "";
            if (points != null)
            {
                lblNoReturn.Text = "Return " + points.Length + " items";
                for (int j = 0; j < points.Length; j++)
                {
                    itemFound = points[j];

                    if (itemFound != null && itemFound.Length != 0)
                    {
                        if (itemFound.Length > 1)
                        {
                            try
                            {
                                unifiedItem = DNMcLineItem.Create(
                                    DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                    DNELineStyle._ELS_SOLID, new DNSMcBColor(0, 0, 255, 255), 2);
                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("DNMcLineItem.Create", McEx);
                                return;
                            }
                        }
                        else
                        {
                            try
                            {
                                unifiedItem = DNMcEllipseItem.Create(
                                    DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                    DNEMcPointCoordSystem._EPCS_SCREEN, DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT, 10, 10,
                                    0, 360, 0, DNELineStyle._ELS_SOLID, new DNSMcBColor(0, 0, 255, 255), 2, null,
                                    new DNSMcFVector2D(0, -1), 1, DNEFillStyle._EFS_NONE);
                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("DNMcEllipseItem.Create", McEx);
                                return;
                            }
                        }

                        try
                        {
                            unifiedItem.SetDrawPriority(1, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false); 
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("IDNMcSymbolicItem.SetDrawPriority", McEx);
                            return;
                        }

                        try
                        {
                            obj = DNMcObject.Create(m_overlay, unifiedItem, DNEMcPointCoordSystem._EPCS_WORLD, itemFound, false);
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                            return;
                        }
                        btnRemove.Enabled = btnRemoveFromAllLayer.Enabled = true;
                        btnRemoveLastPaintedQuery.Enabled = btnRemoveAllPaintedQuery.Enabled = true;

                        if (j == 0)
                            firstObject = obj;

                        if (itemFound != null && itemFound.Length != 0)
                        {
                            DNSMcBColor color = new DNSMcBColor((byte)(255 - colorDelta), colorDelta, colorDelta, 255);
                            try
                            {
                                IDNMcLineItem line = DNMcLineItem.Create(
                                    DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                    DNELineStyle._ELS_SOLID, color, 10);
                                    
                                colorDelta = (byte)(((uint)colorDelta + 64) % 256);
                                line.Connect(unifiedItem);
                                line.SetAttachPointType(0, DNEAttachPointType._EAPT_INDEX_POINTS, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID);
                                line.SetAttachPointIndex(0, 0, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                                line.SetNumAttachPoints(0, itemFound.Length, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                                return;
                            }
                        }
                    }
                }

                if (firstObject != null)
                    Manager_MCObject.MoveToLocation(firstObject, 0, mcMapViewport);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Manager_MCOverlay.RemoveOverlaysFromVectoryItemOverlays(m_CurrentObject.GetHashCode());
            btnRemoveLastPaintedQuery.Enabled = btnRemove.Enabled = false;
            CheckIsExistOverlayVectorItem();
            lblNoReturn.Text = "";
        }

        private void btnRemoveFromAllLayer_Click(object sender, EventArgs e)
        {
            Manager_MCOverlay.RemoveAllOverlaysFromVectorItemsOverlay();
            btnRemoveLastPaintedQuery.Enabled = btnRemoveAllPaintedQuery.Enabled = false;
            btnRemoveFromAllLayer.Enabled = btnRemove.Enabled = false;
            lblNoReturn.Text = "";
        }

        private void CheckIsExistOverlayVectorItem()
        {
             btnRemoveAllPaintedQuery.Enabled = btnRemoveFromAllLayer.Enabled = Manager_MCOverlay.IsExistOverlayVectorItem();
        }

        private void btnGetLayerAttributes_Click(object sender, EventArgs e)
        {
            try
            {
                DNMcNullableOut<string[]> namesOut = new DNMcNullableOut<string[]>();
                DNMcNullableOut<string[]> valuesOut = new DNMcNullableOut<string[]>();
               
                m_CurrentObject.GetLayerAttributes(namesOut, valuesOut);

                string[] names = namesOut.Value;
                string[] values = valuesOut.Value;

                dgvLayerAttributes.Rows.Clear();
                int length = 0;
                if (names != null)
                    length = names.Length;
                else if (values != null)
                    length = values.Length;

                if (length > 0)
                {
                    for (int i = 0; i < length; i++)
                    {
                        dgvLayerAttributes.Rows.Add();
                        dgvLayerAttributes[0, i].Value = i;
                        if (names != null)
                            dgvLayerAttributes[1, i].Value = names[i];
                        if (values != null)
                            dgvLayerAttributes[2, i].Value = values[i];
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcVectorLayer.GetLayerAttributes", McEx);
                return;
            }
        }

        private void btnGetLayerDataSources_Click(object sender, EventArgs e)
        {
            try
            {
                DNMcNullableOut<string[]> namesOut = null;
                if (chxGetLayerDataSourceNames.Checked)
                    namesOut = new DNMcNullableOut<string[]>();

                DNMcNullableOut<uint[]> valuesOut = null;
                if (chxGetLayerDataSourceIDs.Checked)
                    valuesOut = new DNMcNullableOut<uint[]>();
                if(valuesOut == null)
                    m_CurrentObject.GetLayerDataSources(namesOut);
                else
                    m_CurrentObject.GetLayerDataSources(namesOut, valuesOut);

                dgvLayersDataSources.Rows.Clear();
                int length = 0;
                if (namesOut != null)
                    length = namesOut.Value.Length;
                else if (valuesOut != null)
                    length = valuesOut.Value.Length;

                if (length > 0)
                {
                    for (int i = 0; i < length; i++)
                    {
                        dgvLayersDataSources.Rows.Add();
                        if (namesOut != null)
                            dgvLayersDataSources[0, i].Value = namesOut.Value[i];
                        if (valuesOut != null)
                            dgvLayersDataSources[1, i].Value = valuesOut.Value[i];
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcVectorLayer.GetLayerDataSources", McEx);
            }
        }

        private void btnVectorItemIDToOriginalID_Click(object sender, EventArgs e)
        {
            try
            {
                DNMcNullableOut<UInt32> puDataSourceID = new DNMcNullableOut<uint>();
                DNMcNullableOut<String> pstrDataSourceName = new DNMcNullableOut<string>();
                UInt32 puOriginalID = 0;
                m_CurrentObject.VectorItemIDToOriginalID(ntxVectorItemId.GetUInt64(), out puOriginalID, pstrDataSourceName, puDataSourceID);

                ntxOrgVectorItemId.SetUInt32(puOriginalID);
                ntxDataSourceAsId.SetUInt32(puDataSourceID.Value);
                ntxDataSourceAsName.Text = pstrDataSourceName.Value;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IDNMcVectorLayer.VectorItemIDToOriginalID", McEx);
            }
        }

        private void btnVectorItemIDFromOriginalID_Click(object sender, EventArgs e)
        {
            try
            {
                UInt64 value = 0;

                if (rbDataSourceAsID.Checked)
                {
                    value = m_CurrentObject.VectorItemIDFromOriginalID(ntxVectorItemId.GetUInt32(), "", ntxDataSourceAsId.GetUInt32());
                }
                else
                {
                    value = m_CurrentObject.VectorItemIDFromOriginalID(ntxVectorItemId.GetUInt32(), ntxDataSourceAsName.Text);
                }
                ntxVectorItemId.SetUInt64(value);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IDNMcVectorLayer.VectorItemIDFromOriginalID", McEx);
            }
        }

        private void btnGetValidFieldsPerDataSource_Click(object sender, EventArgs e)
        {
            try
            {
                uint[] validField = null;
                if (rbGetValidFieldsByVectorItemID.Checked)
                {
                    ulong vectorItemId;
                    if (!ulong.TryParse(ntxGetValidFieldsData.Text, out vectorItemId))
                    {
                        ntxGetValidFieldsData.Focus();
                        MessageBox.Show("Vector Item ID should be as uint 64 number", "Error");
                        return;
                    }
                    validField = m_CurrentObject.GetValidFieldsPerDataSource(ntxGetValidFieldsData.GetUInt64());
                }
                else if (rbGetValidFieldsDataSourceId.Checked)
                {
                    uint vectorItemId;
                    if (!uint.TryParse(ntxGetValidFieldsData.Text, out vectorItemId))
                    {
                        ntxGetValidFieldsData.Focus();
                        MessageBox.Show("Vector Item Id should be as uint 32 number", "Error");
                        return;
                    }
                    validField = m_CurrentObject.GetValidFieldsPerDataSource(0, "", ntxGetValidFieldsData.GetUInt32());
                }
                else
                    validField = m_CurrentObject.GetValidFieldsPerDataSource(0, ntxGetValidFieldsData.Text);

                InitValidFieldsData(validField);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IDNMcVectorLayer.GetValidFieldsPerDataSource", McEx);
            }
        }

        private void InitValidFieldsData(uint[] validField)
        {
            dgvValidFields.Rows.Clear();
            for (int i = 0; i < validField.Length; i++)
            {
                dgvValidFields.Rows.Add();
                dgvValidFields[0, i].Value = validField[i].ToString();
            }
        }

        private void btnGetVectorItemObjectState_Click(object sender, EventArgs e)
        {
            try
            {
                ulong uVectorItemID = 0;
                if (CheckVectorItemID(out uVectorItemID))
                {
                    int index = lbOverlayStateViewport.SelectedIndex;
                    if (index >= 0)
                    {
                        tbVectorItemObjectState.Text = ConvertByteArrayToString(m_CurrentObject.GetVectorItemObjectState(uVectorItemID, SelectedViewport), " ");
                    }
                    else
                        tbVectorItemObjectState.Text = ConvertByteArrayToString(m_CurrentObject.GetVectorItemObjectState(uVectorItemID), " ");
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetVectorItemObjectState", McEx);
            }
        }

        private bool CheckVectorItemID(out ulong uVectorItemID)
        {
            uVectorItemID = 0;
            if (tbVectorItemID.Text == "" || !UInt64.TryParse(tbVectorItemID.Text, out uVectorItemID))
            {
                MessageBox.Show("Vector Item ID should be as uint 64 number", "Invalid Vector Item ID");
                tbVectorItemID.Focus();
                return false;
            }
            return true;
        }

        private void btnSetVectorItemObjectState_Click(object sender, EventArgs e)
        {
            ulong uVectorItemID = 0;
            if (CheckVectorItemID(out uVectorItemID))
            {
                bool isSeccus = false;

                byte[] abyStates = ConvertStringToByteArray(tbVectorItemObjectState.Text, out isSeccus);
                if (isSeccus)
                {
                    try
                    {
                        int index = lbOverlayStateViewport.SelectedIndex;
                        if (index >= 0)
                        {
                            if (abyStates != null && abyStates.Length == 1)
                                m_CurrentObject.SetVectorItemObjectState(uVectorItemID, abyStates[0], SelectedViewport);
                            else
                                m_CurrentObject.SetVectorItemObjectState(uVectorItemID, abyStates, SelectedViewport);
                            Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(SelectedViewport);
                        }
                        else
                        {
                            if (abyStates != null && abyStates.Length == 1)
                                m_CurrentObject.SetVectorItemObjectState(uVectorItemID, abyStates[0]);
                            else
                                m_CurrentObject.SetVectorItemObjectState(uVectorItemID, abyStates);
                            Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("SetVectorItemObjectState", McEx);
                    }
                }
            }
        }

        private void btnGetBrightness_Click(object sender, EventArgs e)
        {
            GetBrightness();
        }

        private void btnGetState_Click(object sender, EventArgs e)
        {
            GetOverlayState();
        }

        private void btnPaintQuery_Click(object sender, EventArgs e)
        {
            if(IsPaintQuaryEnabled())
            {
                foreach(int index in lstVectorItemsId.SelectedIndices)
                    GetVectorItemPoints(chxQueryAsync.Checked, (ulong)lstVectorItemsId.Items[index]);
            }
        }
       
        private void lstVectorItemsId_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPaintQuery.Enabled = IsPaintQuaryEnabled();

        }

        private bool IsPaintQuaryEnabled()
        {
            return (lstVectorItemsId.Items != null && lstVectorItemsId.Items.Count > 0 && lstVectorItemsId.SelectedIndex >= 0 && lstVectorItemsId.SelectedIndex < lstVectorItemsId.Items.Count); 
        }
    }
}
